#pragma region 변경 이력
/**
 Author : Link mskoo (2011. 3. 20)

***********************************************************************************************************************
 Date           Name            Description of Change
-----------------------------------------------------------------------------------------------------------------------
 2011-03-20     mskoo           최초 작성.

 2011-09-23     mskoo           5.0 버전 릴리즈. (변경 이력 정리)

 2011-10-04     mskoo           메소드 제거.
                                - BlockAccess()
                                - UnblockAccess()
                                - EnableBlockingAccess(bool)

 2013-11-17     jake            IFacFilterOperator 인터페이스를 구현.
**********************************************************************************************************************/
#pragma endregion

#pragma comment(lib, "fltLib.lib")

#include "StdAfx.h"
#include "FacFilterTypes.h"
#include "FacFilterOperator.h"
#include "AssemblyResource.h"
#include "Utils.h"

using namespace System::Diagnostics;
using namespace System::Text;
using namespace msclr::interop;
// E-FAM 관련
using namespace Link::EFAM::Common;
using namespace Link::EFAM::Engine::Filter;

#pragma region 생성자

/// <summary>
/// <see cref="FacFilterOperator"/> 클래스의 새 인스턴스를 초기화한다.
/// </summary>
FacFilterOperator::FacFilterOperator()
    : m_threadCount(2)
    , m_requestCount(5)
{
    InitializeMemberVariables();
}

/*
/// <summary>
/// <see cref="FacFilterOperator"/> 클래스의 새 인스턴스를 초기화한다.
/// </summary>
/// <param name="filterName">액세스 제어 미니필터 드라이버의 이름</param>
/// 
/// <exception cref="ArgumentNullException">filterName이 nullptr인 경우</exception>
FacFilterOperator::FacFilterOperator(String^ filterName)
    //: m_filterName(filterName)
    : m_threadCount(2)
    , m_requestCount(5)
{
    if (filterName == nullptr) throw gcnew ArgumentNullException("filterName");

    InitializeMemberVariables();
}

/// <summary>
/// 지정한 작업 스레드의 개수와 요청의 개수를 사용하는
/// <see cref="FacFilterOperator"/> 클래스의 새 인스턴스를 초기화한다.
/// </summary>
/// <param name="filterName">액세스 제어 미니필터 드라이버의 이름</param>
/// <param name="threadCount">생성할 작업 스레드의 개수</param>
/// <param name="requestCount">각 작업 스레드에서 처리할 요청의 개수</param>
/// 
/// <exception cref="ArgumentNullException">filterName이 nullptr인 경우</exception>
/// <exception cref="ArgumentOutOfRangeException">
/// threadCount가 1보다 작거나 64보다 큰 경우<br/>
/// - 또는 -<br/>
/// requestCount가 0보다 작거나 같은 경우
/// </exception>
FacFilterOperator::FacFilterOperator(String^ filterName, int threadCount, int requestCount)
    //: m_filterName(filterName)
    : m_threadCount(threadCount)
    , m_requestCount(requestCount)
{
    if (filterName == nullptr) throw gcnew ArgumentNullException("filterName");
    if (threadCount < 1 || threadCount > 64)
    {
        throw gcnew ArgumentOutOfRangeException("threadCount",
            String::Format(AssemblyResource::GetString("Error_OutOfRangeParameter_Between"), "1", "64"));
    }
    if (requestCount <= 0)
    {
        throw gcnew ArgumentOutOfRangeException("requestCount",
            String::Format(AssemblyResource::GetString("Error_OutOfRangeParameter_Gt"), "0"));
    }

    InitializeMemberVariables();
}
*/

#pragma endregion

#pragma region 소멸자

/// <summary>
/// 인스턴스가 할당한 리소스를 모두 해제한다.
/// </summary>
FacFilterOperator::~FacFilterOperator()
{
	// 관리되지 않는 메모리에서 할당된 메모리들을 해제한다.
    if (m_bufferList != nullptr) m_bufferList->Clear();
}

#pragma endregion

#pragma region 메소드

/// <summary>
/// 멤버 변수들을 초기화한다.
/// </summary>
inline void FacFilterOperator::InitializeMemberVariables()
{
    m_waitEvtList = gcnew List<ManualResetEvent^>();
    m_bufferList = gcnew List<AllocatedMemory^>();

    this->CommunicationPort = IntPtr::Zero;
    m_completionPort = IntPtr::Zero;
    m_useRecycleBin = true;
    m_allowSaveAs = false;
    m_connected = false;

    m_recycleBinName = msclr::interop::marshal_as<String^>(RecycleBinName);
}

/// <summary>
/// 미니필터 드라이버와 통신하기 위해 생성한 통신 포트와 I/O 완료 포트를 닫는다.
/// </summary>
inline void FacFilterOperator::CloseConnectionPorts()
{
    //
    // 통신 포트와 I/O 완료 포트를 닫는다.
    //
    Utils::CloseObjectHandle(this->CommunicationPort);
    Utils::CloseObjectHandle(m_completionPort);

    this->CommunicationPort = IntPtr::Zero;
    m_completionPort = IntPtr::Zero;
    m_connected = false;
}

/// <summary>
/// 액세스 제어 미니필터 드라이버에 연결한다.
/// </summary>
/// <remarks>미니필터 드라이버에서 생성된 서버 포트에 연결한다.</remarks>
/// 
/// <exception cref="InvalidOperationException">
/// 액세스 제어 미니필터 드라이버에 이미 연결되어 있는 경우<br/>
/// - 또는 -<br/>
/// 미니필터 드라이버에서 요청하는 작업을 처리하는 어댑터를 지정하지 않은 경우
/// </exception>
/// <exception cref="System::Runtime::InteropServices::COMException">
/// 인식할 수 없는 HRESULT가 WDK API 함수 호출에서 반환된 경우
/// </exception>
/// <exception cref="System::ComponentModel::Win32Exception">
/// 시스템 API에 액세스하는 동안 오류가 발생한 경우
/// </exception>
/// <exception cref="OutOfMemoryException">
/// 새 스레드를 시작할 충분한 메모리가 없는 경우<br/>
/// - 또는 -<br/>
/// 관리되지 않는 메모리에 메시지 버퍼에 할당할 충분한 메모리가 없는 경우
/// </exception>
void FacFilterOperator::ConnectFilter()
{
    if (m_connected)
    {
        throw gcnew InvalidOperationException(AssemblyResource::GetString("Error_FilterAlreadyConnected"));
            //String::Format(AssemblyResource::GetString("Error_FilterAlreadyConnected"), m_filterName));
    }

    HANDLE hCommPort = INVALID_HANDLE_VALUE;    // Native 포인터
    HANDLE hCompletionPort = NULL;              // Native 포인터
    HRESULT hResult;

    try
    {
        //
        // 미니필터 드라이버와 통신하는데 필요한 포트들을 생성한다.
        //

        // 미니필터 드라이버에서 생성된 통신 서버 포트에 연결한다. (Native 함수 호출)
        hResult = FilterConnectCommunicationPort(FileAccCtrlPortName, 0, NULL, 0, NULL, &hCommPort);
        if (hResult != S_OK) InterOp::Marshal::ThrowExceptionForHR(hResult);

        // I/O 완료 포트를 생성한다. (Native 함수 호출)
        hCompletionPort = CreateIoCompletionPort(hCommPort, NULL, 0, m_threadCount);
        if (hCompletionPort == NULL) throw gcnew System::ComponentModel::Win32Exception(GetLastError());

        this->CommunicationPort = IntPtr(hCommPort);
        m_completionPort = IntPtr(hCompletionPort);

        // 작업 스레드와 모니터 스레드를 시작한다.
        PrepareWorkersAndMonitor();

        m_connected = true;
    } // try
    finally
    {
        if (!m_connected)
        {
            //
            // 생성한 포트들을 닫는다. (Native 함수 호출)
            //
            if (hCommPort != INVALID_HANDLE_VALUE) CloseHandle(hCommPort);
            if (hCompletionPort != NULL) CloseHandle(hCompletionPort);

            this->CommunicationPort = IntPtr::Zero;
            m_completionPort = IntPtr::Zero;

            m_bufferList->Clear();
            m_monitor = nullptr;
        } // if (!m_connected)
    } // finally 
}

/// <summary>
/// 액세스 제어 미니필터 드라이버와 연결을 해제한다.
/// </summary>
/// <remarks>미니필터 드라이버의 서버 포트와 연결된 통신 포트를 닫아서 연결을 끊는다.</remarks>
void FacFilterOperator::DisconnectFilter()
{
    try
    {
        CloseConnectionPorts();

        // 모니터 스레드가 종료될 때까지 대기한다.
        if (m_monitor != nullptr) m_monitor->Join();
    }
    catch (ThreadStateException^) { }
    catch (ThreadInterruptedException^) { }
}

/// <summary>
/// 작업 스레드와 모니터 스레드를 시작한다.
/// </summary>
/// 
/// <exception cref="InvalidOperationException">
/// 드라이버에서 요청하는 작업을 처리하는 어댑터를 지정하지 않은 경우
/// </exception>
/// <exception cref="OutOfMemoryException">
/// 새 스레드를 시작할 충분한 메모리가 없는 경우<br/>
/// - 또는 -<br/>
/// 관리되지 않는 메모리에 메시지 버퍼에 할당할 충분한 메모리가 없는 경우
/// </exception>
void FacFilterOperator::PrepareWorkersAndMonitor()
{
    if (m_adapter == nullptr)
    {
        throw gcnew InvalidOperationException(
            String::Format(AssemblyResource::GetString("Error_PropertyNotSpecified"), "Adapter"));
    }

    FacFilterWorker^ worker;
    Thread^ thread;

    m_waitEvtList->Clear();
    m_bufferList->Clear();

    ///////////////////////////////////////////////////////////////////////////////////////////
    // 미니필터 드라이버와 통신하면서 미니필터 드라이버에서 요청한 작업을 처리할
    // 작업 스레드들을 시작한다.
    //
    AllocatedMemory^ buffer;

    for (int count = 0; count < m_threadCount; count++)
    {
        //
        // 미니필터 드라이버에서 전송된 메시지를 비동기로(asynchronous) 가져온다.
        //
        for (int reqCount = 0; reqCount < m_requestCount; reqCount++)
        {
            buffer = gcnew AllocatedMemory(ReceiveMessageSize);
            m_bufferList->Add(buffer);

            this->GetMessageAsync(buffer->Pointer);
        }

        worker = gcnew FacFilterWorker(this->CommunicationPort, m_completionPort);
        worker->Adapter = m_adapter;

        m_waitEvtList->Add(worker->WaitHandle);

        //
        // 작업 스레드를 시작한다.
        //
        thread = gcnew Thread(gcnew ThreadStart(worker, &FacFilterWorker::DoWork));
        thread->Start();
    } // for (int count = 0; count < m_threadCount; count++)

    ///////////////////////////////////////////////////////////////////////////////////////////
    // 모니터 스레드를 시작한다.
    //
    m_monitor = gcnew Thread(gcnew ThreadStart(this, &FacFilterOperator::Monitor_DoWork));
    m_monitor->Start();

    if (m_tracing->Enabled)
    {
        Trace::WriteLine(
            String::Format("[EFAM] The monitor thread is created. Thread ID= {0}", m_monitor->ManagedThreadId));
    }

    m_adapter->OnWorkersStarted();
}

#pragma region 명령 메시지 전송

/// <summary>
/// 원격 파일 및 디렉토리에 대한 액세스 제어를 활성화한다.
/// </summary>
/// 
/// <exception cref="InvalidOperationException">
/// 액세스 제어 미니필터 드라이버에 연결되어 있지 않은 경우<br/>
/// - 또는 -<br/>
/// 프로세스 정보를 얻는 데 사용되는 성능 카운터 API에 액세스하는 데 문제가 있는 경우
/// </exception>
/// <exception cref="System::Runtime::InteropServices::COMException">
/// 인식할 수 없는 HRESULT가 WDK API 함수 호출에서 반환된 경우
/// </exception>
void FacFilterOperator::ActivateFilter()
{
    if (!m_connected)
    {
        throw gcnew InvalidOperationException(AssemblyResource::GetString("Error_FilterNotConnected"));
            //String::Format(AssemblyResource::GetString("Error_FilterNotConnected"), m_filterName));
    }

    List<int>^ bypassPidList = gcnew List<int>();
    ProcessKind kind;
    int explorer;
    int bufferSize;

    AllocatedMemory^ messageBuffer;
    PFILEACCCTRL_COMMAND_MESSAGE lpMessage;      // Native 포인터

    //
    // "Windows 탐색기" 프로세스와 우회할 프로세스들의 프로세스 ID를 가져온다.
    //
    if (m_tracing->Enabled) Trace::WriteLine("[EFAM] Running system processes...");

    for each (Process^ process in Process::GetProcesses())
    {
        kind = m_adapter->GetProcessKind(process->Id);

        if (kind == ProcessKind::SystemProcess)
        {
            bypassPidList->Add(process->Id);

            if (m_tracing->Enabled)
            {
                Trace::WriteLine(
                    String::Format("[EFAM] {0} (PID {1})", process->ProcessName, process->Id));
            }
        } // if (kind == ProcessKind::SystemProcess)
        else if (kind == ProcessKind::WindowsExplorer) explorer = process->Id;

        process->Close();
    } // for each (...)

    //
    // 메시지에 사용할 메모리를 할당한다.
    //
    bufferSize = CommandMessageSize + (bypassPidList->Count * sizeof(ULONG));
    messageBuffer = gcnew AllocatedMemory(bufferSize);
    lpMessage = (PFILEACCCTRL_COMMAND_MESSAGE)messageBuffer->Pointer.ToPointer();

    //
    // 메시지의 내용을 설정한다.
    //
    lpMessage->Command = ACTIVATE;
    lpMessage->Activate.UseRecycleBin = m_useRecycleBin ? TRUE : FALSE;
    lpMessage->Activate.AllowSaveAs = m_allowSaveAs ? TRUE : FALSE;
    lpMessage->Activate.WinExplorer = explorer;
    lpMessage->Activate.Count = bypassPidList->Count;
    for (int index = 0; index < bypassPidList->Count; index++)
    {
        lpMessage->Activate.PidArray[index] = bypassPidList[index];
    }

    this->SendMessage(messageBuffer->Pointer, bufferSize);
}

/// <summary>
/// 원격 파일 및 디렉토리에 대한 액세스 제어를 비활성화한다.
/// </summary>
/// 
/// <exception cref="InvalidOperationException">
/// 액세스 제어 미니필터 드라이버에 연결되어 있지 않은 경우
/// </exception>
/// <exception cref="System::Runtime::InteropServices::COMException">
/// 인식할 수 없는 HRESULT가 WDK API 함수 호출에서 반환된 경우
/// </exception>
void FacFilterOperator::DeactivateFilter()
{
    if (!m_connected)
    {
        throw gcnew InvalidOperationException(AssemblyResource::GetString("Error_FilterNotConnected"));
            //String::Format(AssemblyResource::GetString("Error_FilterNotConnected"), m_filterName));
    }

    FILEACCCTRL_COMMAND_MESSAGE message;

    //
    // 메시지의 내용을 설정한다.
    //
    message.Command = DEACTIVATE;

    this->SendMessage(IntPtr((void *)&message), CommandMessageSize);
}

/// <summary>
/// 원격 파일 및 디렉토리에 대한 액세스를 제어할 원격 경로들을 설정한다.
/// </summary>
/// <param name="paths">액세스를 제어할 원격 경로들의 배열</param>
/// <remarks>
/// Example : "\\192.168.0.19\Shared"<br/>
///           "\\192.168.0.45\Shared2\doc_folder"<br/>
///           "\\192.168.0.50"
/// </remarks>
/// 
/// <exception cref="ArgumentNullException">paths가 nullptr인 경우</exception>
/// <exception cref="InvalidOperationException">
/// 액세스 제어 미니필터 드라이버에 연결되어 있지 않은 경우
/// </exception>
/// <exception cref="System::Runtime::InteropServices::COMException">
/// 인식할 수 없는 HRESULT가 WDK API 함수 호출에서 반환된 경우
/// </exception>
void FacFilterOperator::SetRemotePaths(array<String^>^ paths)
{
    if (paths == nullptr) throw gcnew ArgumentNullException("paths");

    String^ concatPaths = Utils::ConcatenateRemotePaths(paths, L'\0');

    SetPathStrings(SET_REMOTE_PATHS, concatPaths);
}

/// <summary>
/// "Microsoft Office" 응용 프로그램의 캐시 디렉토리의 경로를 설정한다.
/// </summary>
/// 
/// <exception cref="InvalidOperationException">
/// 액세스 제어 미니필터 드라이버에 연결되어 있지 않은 경우
/// </exception>
/// <exception cref="System::Runtime::InteropServices::COMException">
/// 인식할 수 없는 HRESULT가 WDK API 함수 호출에서 반환된 경우
/// </exception>
void FacFilterOperator::SetCacheDirectoryOfMsOffice()
{
    ///////////////////////////////////////////////////////////////////////////////////////////////
    // 임시 인터넷 파일들이 저장되는 시스템 디렉토리를 가져와서 "Microsoft Office" 응용 프로그램의
    // 캐시 디렉토리의 경로를 작성한다.
    //
    // C:\Documents and Settings\username\Local Settings\Temporary Internet Files\Content.MSO
    //
    StringBuilder^ cacheDirPath = gcnew StringBuilder();

    cacheDirPath->Append(Environment::GetFolderPath(Environment::SpecialFolder::InternetCache));
    cacheDirPath->Append("\\Content.MSO");
    cacheDirPath->Append(L'\0');

    SetPathStrings(SET_MSO_CACHE_DIR, cacheDirPath->ToString());
}

/// <summary>
/// 응용 프로그램들의 임시 파일이 생성되는 경로들을 설정한다.
/// </summary>
/// <param name="paths">임시 파일이 생성되는 경로들의 배열</param>
/// 
/// <exception cref="ArgumentNullException">paths가 nullptr인 경우</exception>
/// <exception cref="InvalidOperationException">
/// 액세스 제어 미니필터 드라이버에 연결되어 있지 않은 경우
/// </exception>
/// <exception cref="System::Runtime::InteropServices::COMException">
/// 인식할 수 없는 HRESULT가 WDK API 함수 호출에서 반환된 경우
/// </exception>
void FacFilterOperator::SetTemporaryPaths(array<String^>^ paths)
{
    if (paths == nullptr) throw gcnew ArgumentNullException("paths");

    StringBuilder^ temporaryPaths = gcnew StringBuilder();
    array<wchar_t>^ driveName = { L'_', L':', L'\0' };
    String^ volumeName;

    //
    // 경로의 드라이브 이름을 볼륨 이름으로 변경하고, 
    // NULL 문자로 구분된 경로 문자열들을 작성한다.
    //
    AllocatedMemory^ buffer = gcnew AllocatedMemory(256 * sizeof(WCHAR));
    LPWSTR lpVolumeName = (LPWSTR)buffer->Pointer.ToPointer();      // Native 포인터
    DWORD length;

    for each (String^ path in paths)
    {
        driveName[0] = path[0];

        pin_ptr<wchar_t> lpDeviceName = &driveName[0];
        // 볼륨 이름을 가져온다 (\Device\HarddiskVolume1, etc). (Native 함수 호출)
        length = QueryDosDeviceW(lpDeviceName, lpVolumeName, 256);
        if (length == 0) continue;

        volumeName = marshal_as<String^>(lpVolumeName);

        temporaryPaths->Append(volumeName);
        temporaryPaths->Append(path->Substring(2));
        temporaryPaths->Append(L'\0');
    } // for each (...)

    SetPathStrings(SET_TEMPORARY_PATHS, temporaryPaths->ToString());
}

/// <summary>
/// NULL 문자로 구분된 경로 문자열들을 설정한다.
/// </summary>
/// <param name="command">명령의 종류를 나타내는 NETACCCTRL_COMMAND 값 중 하나</param>
/// <param name="pathStrings">NULL 문자로 구분된 경로 문자열들</param>
/// 
/// <exception cref="ArgumentNullException">pathStrings가 nullptr인 경우</exception>
/// <exception cref="InvalidOperationException">
/// 액세스 제어 미니필터 드라이버에 연결되어 있지 않은 경우
/// </exception>
/// <exception cref="System::Runtime::InteropServices::COMException">
/// 인식할 수 없는 HRESULT가 WDK API 함수 호출에서 반환된 경우
/// </exception>
void FacFilterOperator::SetPathStrings(int command, String^ pathStrings)
{
    if (pathStrings == nullptr) throw gcnew ArgumentNullException("pathStrings");
    if (!m_connected)
    {
        throw gcnew InvalidOperationException(AssemblyResource::GetString("Error_FilterNotConnected"));
            //String::Format(AssemblyResource::GetString("Error_FilterNotConnected"), m_filterName));
    }

    array<wchar_t>^ nullStrings = pathStrings->ToCharArray();
    int pathLength = nullStrings->Length * sizeof(wchar_t);
    int bufferSize;

    AllocatedMemory^ messageBuffer;
    PFILEACCCTRL_COMMAND_MESSAGE lpMessage;      // Native 포인터

    //
    // 메시지에 사용할 메모리를 할당한다.
    //
    bufferSize = CommandMessageSize + pathLength;
    messageBuffer = gcnew AllocatedMemory(bufferSize);
    lpMessage = (PFILEACCCTRL_COMMAND_MESSAGE)messageBuffer->Pointer.ToPointer();

    //
    // 메시지의 내용을 설정한다.
    //
    lpMessage->Command = (FILEACCCTRL_COMMAND)command;
    lpMessage->SetRemotePaths.Length = pathLength;
    lpMessage->SetRemotePaths.PathBuffer[0] = L'\0';
    if (pathLength > 0)
    {
        pin_ptr<wchar_t> lpNullStrings = &nullStrings[0];

        // 경로 문자열들을 복사한다. (Native 함수 호출)
        CopyMemory(lpMessage->SetRemotePaths.PathBuffer, lpNullStrings, pathLength);
    }
    
    this->SendMessage(messageBuffer->Pointer, bufferSize);
}

#pragma endregion

#pragma region 모니터 스레드

/// <summary>
/// 실행 중인 모든 작업 스레드들이 종료될 때까기 대기한다.
/// </summary>
void FacFilterOperator::Monitor_DoWork()
{
    array<ManualResetEvent^>^ waitEvents = m_waitEvtList->ToArray();

    // 모든 작업 스레드가 종료될 때까지 대기한다.
    WaitHandle::WaitAll(waitEvents);

    //
    // 오류가 발생하여 모든 작업 스레드가 종료되었을 경우
    //
    if (m_connected)
    {
        CloseConnectionPorts();

        if (m_tracing->Enabled) Trace::WriteLine("[EFAM] All worker threads are terminated.");
    }

    m_adapter->OnWorkersExited();

    m_bufferList->Clear();
    m_monitor = nullptr;
}

#pragma endregion
#pragma endregion
