#pragma region 변경 이력
/**
 Author : Link mskoo (2011. 3. 23)

***********************************************************************************************************************
 Date           Name            Description of Change
-----------------------------------------------------------------------------------------------------------------------
 2011-03-23     mskoo           최초 작성.

 2011-09-23     mskoo           5.0 버전 릴리즈. (변경 이력 정리)

 2016-01-14     mskoo           FILE_DELETE_ON_CLOSE 플래그를 이용한 I/O 요청도 제어할 수 있도록 수정.
**********************************************************************************************************************/
#pragma endregion

#pragma comment(lib, "fltLib.lib")

#include "StdAfx.h"
#include <fltUser.h>
#include "FacFilterTypes.h"
#include "FacFilterWorker.h"
#include "AssemblyResource.h"

// copied from ntstatus.h
#define STATUS_SUCCESS                  ((NTSTATUS)0x00000000L)
#define STATUS_UNSUCCESSFUL             ((NTSTATUS)0xC0000001L)

using namespace System::Diagnostics;
using namespace System::IO;
using namespace msclr::interop;
// .NET용 개발 라이브러리
using namespace Link::Core::Runtime::InteropServices;
// E-FAM 관련
using namespace Link::EFAM::Common;
using namespace Link::EFAM::Engine::Filter;
using namespace Link::Core::IO;

#pragma region 생성자

/// <summary>
/// <see cref="FacFilterWorker"/> 클래스의 새 인스턴스를 초기화한다.
/// </summary>
/// <param name="commPort">통신 포트에 대한 핸들</param>
/// <param name="completionPort">I/O 완료 포트에 대한 핸들</param>
FacFilterWorker::FacFilterWorker(IntPtr commPort, IntPtr completionPort)
{
    m_logger = LogManager::GetLogger(FacFilterWorker::typeid);
    m_waitEvent = gcnew ManualResetEvent(false);

    m_completionPort = completionPort;
    this->CommunicationPort = commPort;
}

#pragma endregion

#pragma region 메소드
#pragma region 스레드 메소드

/// <summary>
/// 액세스 제어 미니필터 드라이버와 통신하면서 미니필터 드라이버에서 요청한 작업을 처리한다.
/// </summary>
/// 
/// <exception cref="InvalidOperationException">
/// 미니필터 드라이버에서 요청하는 작업을 처리하는 어댑터를 지정하지 않은 경우
/// </exception>
/// <exception cref="System::ComponentModel::Win32Exception">
/// 시스템 API에 액세스하는 동안 오류가 발생한 경우
/// </exception>
/// <exception cref="System::Runtime::InteropServices::COMException">
/// 인식할 수 없는 HRESULT가 WDK API 함수 호출에서 반환된 경우
/// </exception>
void FacFilterWorker::DoWork()
{
    if (m_adapter == nullptr)
    {
        throw gcnew InvalidOperationException(
            String::Format(AssemblyResource::GetString("Error_PropertyNotSpecified"), "Adapter"));
    }

    AllocatedMemory^ replyBuffer = gcnew AllocatedMemory(ReplyMessageSize);
    IntPtr messagePtr;

    try
    {
        HANDLE hCompletionPort = m_completionPort.ToPointer();      // Native 포인터
        LPOVERLAPPED lpOvlp = NULL;     // Native 포인터
        DWORD bytesTransferred = 0;
        ULONG_PTR key;
        BOOL dequeued;
        bool needsReply;

        Exception^ throwedExc;

// LOG - 스레드 시작

        for (;; lpOvlp = NULL)
        {
            //
            //  Obtain the message: note that the message we sent down via FltGetMessage() may NOT be
            //  the one dequeued off the completion queue: this is solely because there are multiple
            //  threads per single port handle. Any of the FilterGetMessage() issued messages can be
            //  completed in random order - and we will just dequeue a random one.
            //

            //
            // 큐에서 미니필터 드라이버가 전송한 메시지를 꺼낸다. (Native 함수 호출)
            // [I/O 완료 포트에서 I/O 완료 패킷을 꺼낸다.]
            //
            dequeued = GetQueuedCompletionStatus(hCompletionPort,
                                                 &bytesTransferred, &key, &lpOvlp, INFINITE);
            messagePtr = IntPtr(CONTAINING_RECORD(lpOvlp, FILEACCCTRL_MESSAGE, Overlapped));

            if (dequeued)
            {
                //
                // 메시지의 내용을 처리하고, 미니필터 드라이버에 응답 메시지를 전송한다.
                //
                try
                {
                    needsReply = ProcessMessage(messagePtr, replyBuffer->Pointer);
                    if (needsReply)
                    {
                        this->ReplyMessage(replyBuffer->Pointer, ReplyMessageSize);
                    }
                } // try
                catch (InterOp::COMException^ comExc)
                {
                    if (comExc->ErrorCode == Error_InvalidHandle) break;

                    throwedExc = comExc;
                }
                catch (Exception^ exc) { throwedExc = exc; }
                finally
                {
                    if (throwedExc != nullptr)
                    {
                        if (m_logger->IsErrorEnabled) m_logger->Error("FacFilterWorker.DoWork()", throwedExc);
                        if (m_tracing->Enabled) Trace::WriteLine("[EFAM.Error] FacWorker\n" + throwedExc);
                        throwedExc = nullptr;
                    }
                } // finally
            } // if (dequeued)
            else
            {
                System::ComponentModel::Win32Exception^ w32Exc 
                    = gcnew System::ComponentModel::Win32Exception(GetLastError());

                if (w32Exc->NativeErrorCode == ERROR_INVALID_HANDLE) break;
                else
                {
                    /*
                    요청한 스레드 또는 프로세스가 종료되었을 경우
                    ERROR_NOACCESS, ERROR_OPERATION_ABORTED
                    */
                    String^ message = String::Format("GetQueuedCompletionStatus() failed. error {0}: {1}", 
                                                     w32Exc->NativeErrorCode, w32Exc->Message);

                    if (m_logger->IsErrorEnabled) m_logger->Error("FacFilterWorker.DoWork() - " + message);
                    if (m_tracing->Enabled) Trace::WriteLine("[EFAM.Error] FacWorker - " + message);
                } // else

                if (lpOvlp == NULL) continue;
            } // else

            //
            // 미니필터 드라이버에서 전송된 메시지를 비동기로(asynchronous) 가져온다.
            //
            try
            {
                this->GetMessageAsync(messagePtr);
            }
            catch (InterOp::COMException^ comExc)
            {
                if (comExc->ErrorCode == Error_InvalidHandle) break;

                throwedExc = comExc;
            }
            catch (Exception^ exc) { throwedExc = exc; }
            finally
            {
                if (throwedExc != nullptr)
                {
                    if (m_logger->IsErrorEnabled) m_logger->Error("FacFilterWorker.DoWork()", throwedExc);
                    if (m_tracing->Enabled) Trace::WriteLine("[EFAM.Error] FacWorker\n" + throwedExc);
                    throwedExc = nullptr;
                }
            } // finally
        } // for (;; lpOvlp = NULL)

        if (m_tracing->Enabled)
        {
		    Trace::WriteLine(
                "[EFAM] Worker thread - Port is disconnected, probably due to minifilter driver unloading.");
        }
    } // try
    catch (ThreadAbortException^)
    {
        //
        // Thread::Abort 메소드가 호출되어 스레드가 종료될 경우
        //
        if (m_tracing->Enabled) Trace::WriteLine("[EFAM] FacWorker is exited by User.");
    }
    catch (Exception^ exc)
    {
        if (m_logger->IsErrorEnabled) m_logger->Error("FacFilterWorker.DoWork() - FacWorker is exited by error.", exc);
        if (m_tracing->Enabled)
        {
            Trace::WriteLine("[EFAM.Error] FacWorker is exited by error.\n" + exc);
        }
    } // catch (Exception)
    finally
    {
        m_waitEvent->Set();
    }
}

#pragma endregion

/// <summary>
/// 미니필터 드라이버에서 전송된 메시지의 내용을 처리하고,
/// 미니필터 드라이버에 전송할 응답 메시지를 반환한다.
/// </summary>
/// <param name="messagePtr">전송된 메시지를 포함하고 있는 버퍼에 대한 포인터</param>
/// <param name="replyPtr">전송할 응답 메시지를 받을 버퍼에 대한 포인터</param>
/// <returns>전송할 응답 메시지가 있으면 true, 그렇지 않으면 false</returns>
bool FacFilterWorker::ProcessMessage(IntPtr messagePtr, IntPtr replyPtr)
{
#define SetFlag(_F, _SF)        ((_F) |= (_SF))

    PFILEACCCTRL_MESSAGE lpMessage = (PFILEACCCTRL_MESSAGE)messagePtr.ToPointer();      // Native 포인터
    FAC_ACCESS_MASK accessMask = FAC_RIGHTS_NONE;
    FAC_PROCESS_KIND procKind = FAC_NORMAL_PROCESS;
    NTSTATUS replyStatus = STATUS_SUCCESS;
    bool needsReply = true;

    String^ errorMessage;

    ///////////////////////////////////////////////////////////////////////////////////////////
    // 메시지의 내용을 처리한다.
    //
    PFILEACCCTRL_MESSAGE_BODY lpMsgBody = &lpMessage->Body;
    FILEACCCTRL_MESSAGE_TYPE messageType = lpMsgBody->MessageType;
    int procId = lpMsgBody->ProcessId;

    //
    // 생성되거나 종료된 프로세스를 통보한다.
    //
    if (messageType == NOTIFY_PROCESS)
    {
        try
        {
            // 새 프로세스가 생성되었을 경우
            if (lpMsgBody->NotifyProcess.Created == TRUE)
            {
                ProcessCreatedEventArgs^ eventArgs = gcnew ProcessCreatedEventArgs(procId);

                m_adapter->OnProcessCreated(eventArgs);
                procKind = (FAC_PROCESS_KIND)eventArgs->Kind;
            }
        } // try
        catch (Exception^ exc)
        {
            replyStatus = STATUS_UNSUCCESSFUL;
            errorMessage = String::Format("'NOTIFY_PROCESS' - PID {0}\n{1}", procId, exc);
        }
    } // if ( NOTIFY_PROCESS )
    //
    // 액세스 권한을 검색한다.
    //
    else if (messageType == GET_PERMISSIONS)
    {
        String^ path = marshal_as<String^>(lpMsgBody->GetPermissions.FullPath);
        bool isDir = (lpMsgBody->GetPermissions.IsDirectory == TRUE);

        try
        {
            FileAccessRights^ fileRights = nullptr;

            fileRights = m_adapter->GetPermissions(procId, path, isDir);
            procKind = (FAC_PROCESS_KIND)m_adapter->GetProcessKind(procId);

            if (fileRights != nullptr)
            {
                if (fileRights->AllowRead) SetFlag(accessMask, FAC_RIGHTS_READ);
                if (fileRights->AllowWrite) SetFlag(accessMask, FAC_RIGHTS_WRITE);
                if (fileRights->AllowCopyFiles) SetFlag(accessMask, FAC_RIGHTS_COPY_FILES);
                if (fileRights->AllowListDirectory) SetFlag(accessMask, FAC_RIGHTS_LIST_DIR);
                if (fileRights->AllowCreateDirectories) SetFlag(accessMask, FAC_RIGHTS_CREATE_DIRS);
                if (fileRights->AllowDelete) SetFlag(accessMask, FAC_RIGHTS_DELETE);
                if (fileRights->AllowRename) SetFlag(accessMask, FAC_RIGHTS_RENAME);
                if (fileRights->AllowMove) SetFlag(accessMask, FAC_RIGHTS_MOVE);
            } // if (fileRights != nullptr)
        } // try
        catch (Exception^ exc)
        {
            replyStatus = STATUS_UNSUCCESSFUL;
            errorMessage 
                = String::Format("'GET_PERMISSIONS' - PID {0}, {1}\n{2}", procId, path, exc);
        }
    } // else if ( GET_PERMISSIONS )
    //
    // 로그를 기록한다.
    //
    else if (messageType == WRITE_LOG)
    {
        FileAction action = FileAction::FileOpened;
        String^ path = marshal_as<String^>(lpMsgBody->WriteLog.FullPath);
        String^ newPath = marshal_as<String^>(lpMsgBody->WriteLog.NewFullPath);
        bool isDir = (lpMsgBody->WriteLog.IsDirectory == TRUE);
        bool needsLogging = true;

        try
        {
            switch (lpMsgBody->WriteLog.ActionType)
            {
                case FAC_FILE_OPENED:
                    action = FileAction::FileOpened;
                    break;

                case FAC_FILE_MODIFIED:
                    action = FileAction::FileModified;
                    break;

                case FAC_FILE_CREATED:
                    action = isDir ? FileAction::DirectoryCreated : FileAction::FileCreated;
                    break;

                case FAC_FILE_DELETED:
                    action = isDir ? FileAction::DirectoryDeleted : FileAction::FileDeleted;
                    break;

                case FAC_FILE_RENAMED:
                    action = isDir ? FileAction::DirectoryRenamed : FileAction::FileRenamed;
                    break;

                case FAC_FILE_MOVED:
                    action = isDir ? FileAction::DirectoryMoved : FileAction::FileMoved;
                    break;

                case FAC_FILE_COPIED:
                    action = FileAction::FileCopied;
                    break;

                case FAC_FILE_READING:
                case FAC_FILE_WRITING:
                    needsLogging = false;
    //              procInfo->Status = ProcessStatus::Processing;
                    break;
            } // switch (lpMsgBody->WriteLog.ActionType)

            if (needsLogging) m_adapter->WriteLog(procId, path, newPath, action);
        } // try
        catch (Exception^ exc)
        {
            if (String::IsNullOrEmpty(newPath))
            {
                errorMessage = String::Format("'WRITE_LOG' - PID {0}, {1}: {2}\n{3}", 
                                              procId, action, path, exc);
            }
            else
            {
                errorMessage = String::Format("'WRITE_LOG' - PID {0}, {1}: {2} => {3}\n{4}", 
                                              procId, action, path, newPath, exc);
            }
        } // catch

        needsReply = false;
    } // else if ( WRITE_LOG )
    //
    // 파일을 삭제한다. (파일을 휴지통 디렉토리로 이동)
    //
    else if (messageType == DELETE_FILE)
    {
        String^ path = marshal_as<String^>(lpMsgBody->DeleteFile.FilePath);
        String^ movedPath = L"";

        try
        {
            // 파일을 휴지통으로 이동한다.
            movedPath = SendFileToRecycleBin(path);

            // 로그를 기록한다.
            m_adapter->WriteLog(procId, path, movedPath, FileAction::FileDeleted);
        }
        catch (Exception^ exc)
        {
            errorMessage = String::Format("'DELETE_FILE' - PID {0}, {1} => {2}\n{3}", 
                                          procId, path, movedPath, exc);
        }

        needsReply = false;
    } // else if ( DELETE_FILE )

    ///////////////////////////////////////////////////////////////////////////////////////////////
    // 응답 메시지의 내용을 설정한다.
    //
    if (needsReply)
    {
        PFILEACCCTRL_REPLY lpReply = (PFILEACCCTRL_REPLY)replyPtr.ToPointer();        // Native 포인터

        lpReply->Header.MessageId = lpMessage->Header.MessageId;
        lpReply->Header.Status = replyStatus;
        lpReply->Body.AccessRights = accessMask;
        lpReply->Body.ProcessKind = procKind;
    } // if (needsReply)

    //
    // 오류 로그와 추적 로그를 기록한다.
    //
    if (errorMessage != nullptr)
    {
        if (m_logger->IsErrorEnabled) m_logger->Error("FacFilterWorker.ProcessMessage() " + errorMessage);
        if (m_tracing->Enabled) Trace::WriteLine("[EFAM.Error] FacWorker " + errorMessage);
    }

    return needsReply;
}

/// <summary>
/// 지정한 파일을 휴지통으로 보낸다.
/// </summary>
/// <param name="path">휴지통으로 보낼 파일의 경로</param>
/// <returns>휴지통으로 이동된 파일의 경로</returns>
/// 
/// <exception cref="ArgumentException">
/// <paramref name="path"/>가 길이가 0인 문자열이거나, 공백만 포함하거나, 잘못된 문자를 포함하는 경우
/// </exception>
/// <exception cref="FileNotFoundException"><paramref name="path"/>에 설명된 파일을 찾을 수 없는 경우</exception>
/// <exception cref="DirectoryNotFoundException"><paramref name="path"/>에 지정한 경로가 잘못된 경우</exception>
/// <exception cref="IOException">I/O 오류가 발생한 경우</exception>
/// <exception cref="UnauthorizedAccessException">파일에 대한 액세스가 거부된 경우</exception>
String^ FacFilterWorker::SendFileToRecycleBin(String^ path)
{
    String^ destDir;
    String^ destFile;
    String^ fileName = Path::GetFileName(path);

    // 파일을 이동할 휴지통 디렉토리를 만든다.
    destDir = Path::Combine(NtPath::GetPathRoot(path), marshal_as<String^>(RecycleBinName));
    destDir = Path::Combine(destDir, DateTime::Today.ToString("yyyy-MM-dd"));
    if (!Directory::Exists(destDir))
    {
        Directory::CreateDirectory(destDir);
    }

    // 파일을 휴지통 디렉토리로 이동한다.
    for (; ; )
    {
        destFile = Path::Combine(destDir, Guid::NewGuid() + "-" + fileName);
        if (!File::Exists(destFile))
        {
            File::Move(path, destFile);
            break;
        }
    } // for (; ; )

    return destFile;
}

#pragma endregion
