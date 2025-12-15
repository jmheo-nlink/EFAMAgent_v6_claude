#region 변경 이력
/*
 * Author : Link mskoo (2012. 4. 6)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2012-04-06   mskoo           최초 작성.
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;

namespace Link.Core.Diagnostics
{
    using Link.Core.Runtime.InteropServices;
    //using Link.Core.Win32;
    using Win32Exception = System.ComponentModel.Win32Exception;

    ///// <summary>
    ///// 로컬 프로세스에 대한 액세스를 제공한다.
    ///// </summary>
    //public class NtProcess
    //{
    //    private static readonly bool IsVistaOrLater = (Environment.OSVersion.Version.Major >= 6);

    //    private Process m_process = null;
    //    private SecurityIdentifier m_ownerSid = null;
    //    private string m_owner = null;
    //    private string m_filePath = null;
    //    private string m_fileName = null;
    //    private object m_data = null;

    //    #region 속성

    //    /// <summary>
    //    /// 연결된 프로세스의 기본 핸들을 가져온다.
    //    /// </summary>
    //    /// <value>연결된 프로세스가 시작될 때 운영 체제에서 해당 프로세스에 할당한 핸들</value>
    //    /// 
    //    /// <exception cref="InvalidOperationException">
    //    /// 이 <see cref="NtProcess"/> 개체에 연결된 프로세스가 없는 경우<br/>
    //    /// - 또는 -<br/>
    //    /// <see cref="NtProcess"/> 개체가 실행 프로세스에 연결되었지만 
    //    /// 모든 액세스 권한을 가진 핸들을 가져오는데 필요한 사용 권한이 없는 경우
    //    /// </exception>
    //    public IntPtr Handle
    //    {
    //        get { return m_process.Handle; }
    //    }

    //    /// <summary>
    //    /// 연결된 프로세스의 고유 식별자를 가져온다.
    //    /// </summary>
    //    /// <value>
    //    /// 이 <see cref="NtProcess"/> 개체에서 참조하는 프로세스의 고유 식별자. 
    //    /// 이 식별자는 시스템에서 생성된다.
    //    /// </value>
    //    /// 
    //    /// <exception cref="InvalidOperationException">
    //    /// 이 <see cref="NtProcess"/> 개체에 연결된 프로세스가 없는 경우
    //    /// </exception>
    //    public int Id
    //    {
    //        get { return m_process.Id; }
    //    }

    //    /// <summary>
    //    /// 프로세스의 이름을 가져온다.
    //    /// </summary>
    //    /// <value>사용자가 프로세스를 식별할 수 있도록 시스템에서 사용하는 이름</value>
    //    /// 
    //    /// <exception cref="InvalidOperationException">
    //    /// 이 <see cref="NtProcess"/> 개체에 연결된 프로세스가 없는 경우<br/>
    //    /// - 또는 -<br/>
    //    /// 연결된 프로세스가 종료된 경우
    //    /// </exception>
    //    public string ProcessName
    //    {
    //        get { return m_process.ProcessName; }
    //    }

    //    /// <summary>
    //    /// 연결된 프로세스의 소유자에 대한 사용자 이름을 가져온다.
    //    /// </summary>
    //    /// <value>연결된 프로세스의 소유자에 대한 사용자 이름</value>
    //    /// <remarks>
    //    /// 소유자에 대한 도메인 계정 자격 증명은 사용자의 도메인 이름, '\' 문자 및 사용자 이름의 형식으로 지정된다.
    //    /// </remarks>
    //    public string Owner
    //    {
    //        get
    //        {
    //            if (m_owner == null)
    //            {
    //                NTAccount account = null;
    //                string[] values = null;

    //                try
    //                {
    //                    // 사용자 SID(보안 식별자)를 사용자 계정으로 변환한다.
    //                    account = (NTAccount)this.OwnerSid.Translate(typeof(NTAccount));
    //                    values = account.ToString().Split(new char[] { '\\' });

    //                    // NT 권한 계정인 경우
    //                    if (this.OwnerSid.IsAccountSid())
    //                    {
    //                        m_owner = values[1];
    //                    }
    //                    // 로컬 사용자 계정인 경우
    //                    else if (String.Equals(values[0], Environment.MachineName,
    //                             StringComparison.OrdinalIgnoreCase))
    //                    {
    //                        m_owner = values[1];
    //                    }
    //                    // 도메인 사용자 계정인 경우
    //                    else
    //                    {
    //                        m_owner = account.ToString();
    //                    }
    //                } // try
    //                catch (Win32Exception)
    //                {
    //                    return "";
    //                }
    //            } // if (m_owner == null)

    //            return m_owner;
    //        } // get
    //    }

    //    /// <summary>
    //    /// 연결된 프로세스의 소유자에 대한 SID(보안 식별자)를 가져온다.
    //    /// </summary>
    //    /// <value>
    //    /// 연결된 프로세스의 소유자에 대한 SID(보안 식별자)를 나타내는 <see cref="SecurityIdentifier"/> 개체
    //    /// </value>
    //    /// 
    //    /// <exception cref="Win32Exception">시스템 API에 액세스하는 동안 오류가 발생한 경우</exception>
    //    public SecurityIdentifier OwnerSid
    //    {
    //        get 
    //        {
    //            if (m_ownerSid == null)
    //            {
    //                m_ownerSid = GetOwnerSid(m_process.Handle);
    //            }

    //            return m_ownerSid;
    //        } // get
    //    }

    //    /// <summary>
    //    /// 프로세스에서 연 핸들 수를 가져온다.
    //    /// </summary>
    //    /// <value>프로세스에서 연 운영 체제 핸들 수</value>
    //    public int HandleCount
    //    {
    //        get { return m_process.HandleCount; }
    //    }

    //    /// <summary>
    //    /// 연결된 프로세스를 시작한 실행 파일의 경로를 가져온다.
    //    /// </summary>
    //    /// <value>연결된 프로세스를 시작한 실행 파일의 경로</value>
    //    /// 
    //    /// <exception cref="Win32Exception">시스템 API에 액세스하는 동안 오류가 발생한 경우</exception>
    //    public string ExecutablePath
    //    {
    //        get 
    //        {
    //            if (m_filePath == null)
    //            {
    //                //m_filePath = ProcessHelper.GetExecutablePath(m_process.Handle);
    //                m_filePath = m_process.MainModule.FileVersionInfo.FileName;
    //            }

    //            return m_filePath; 
    //        } // get
    //    }

    //    /// <summary>
    //    /// 연결된 프로세스를 시작한 실행 파일의 이름을 가져온다.
    //    /// </summary>
    //    /// <value>연결된 프로세스를 시작한 실행 파일의 이름</value>
    //    /// 
    //    /// <exception cref="Win32Exception">시스템 API에 액세스하는 동안 오류가 발생한 경우</exception>
    //    public string FileName
    //    {
    //        get 
    //        {
    //            if (m_fileName == null)
    //            {
    //                m_fileName = System.IO.Path.GetFileName(this.ExecutablePath);
    //            }

    //            return m_fileName;
    //        } // get
    //    }

    //    /// <summary>
    //    /// 연결된 프로세스가 종료되었는지 여부를 나타내는 값을 가져온다.
    //    /// </summary>
    //    /// <value>
    //    /// <see cref="NtProcess"/> 개체가 참조하는 프로세스가 종료되었으면 <b>true</b>, 그렇지 않으면 <b>false</b>
    //    /// </value>
    //    /// 
    //    /// <exception cref="InvalidOperationException">
    //    /// 이 <see cref="NtProcess"/> 개체에 연결된 프로세스가 없는 경우
    //    /// </exception>
    //    /// <exception cref="Win32Exception">프로세스의 종료 코드를 검색할 수 없는 경우</exception>
    //    public bool HasExited
    //    {
    //        get { return m_process.HasExited; }
    //    }

    //    /// <summary>
    //    /// 연결된 프로세스가 종료될 때 연결된 프로세스에서 지정한 값을 가져온다.
    //    /// </summary>
    //    /// <value>연결된 프로세스가 종료될 때 연결된 프로세스에서 지정한 코드</value>
    //    /// 
    //    /// <exception cref="InvalidOperationException">
    //    /// 프로세스가 종료되지 않은 경우<br/>
    //    /// - 또는 -<br/>
    //    /// 프로세스 <see cref="Handle"/>이 유효하지 않은 경우
    //    /// </exception>
    //    public int ExitCode
    //    {
    //        get { return m_process.ExitCode; }
    //    }

    //    /// <summary>
    //    /// 프로세스의 주 창에 대한 캡션을 가져온다.
    //    /// </summary>
    //    /// <value>프로세스 주 창의 제목</value>
    //    /// 
    //    /// <exception cref="InvalidOperationException">
    //    /// 프로세스가 종료되어 <see cref="MainWindowTitle"/> 속성이 정의되지 않은 경우
    //    /// </exception>
    //    public string MainWindowTitle
    //    {
    //        get { return m_process.MainWindowTitle; }
    //    }

    //    /// <summary>
    //    /// 프로세스의 사용자 인터페이스가 응답하는지 여부를 나타내는 값을 가져온다.
    //    /// </summary>
    //    /// <value>연결된 프로세스의 사용자 인터페이스가 시스템에 응답하면 <b>true</b>, 그렇지 않으면 <b>false</b></value>
    //    /// 
    //    /// <exception cref="InvalidOperationException">
    //    /// 이 <see cref="NtProcess"/> 개체에 연결된 프로세스가 없는 경우
    //    /// </exception>
    //    public bool Responding
    //    {
    //        get { return m_process.Responding; }
    //    }

    //    /// <summary>
    //    /// <see cref="NtProcess"/> 개체와 연결된 사용자 데이터를 가져오거나 설정한다.
    //    /// </summary>
    //    /// <value>사용자 데이터가 들어있는 개체. 기본값은 <b>null</b></value>
    //    public object UserData
    //    {
    //        get { return m_data; }
    //        set { m_data = value; }
    //    }

    //    #endregion

    //    #region 생성자

    //    /// <summary>
    //    /// <see cref="NtProcess"/> 클래스의 새 인스턴스를 초기화한다.
    //    /// </summary>
    //    /// <param name="process">실행 중인 프로세스 리소스에 연결된 <see cref="Process"/> 구성 요소</param>
    //    private NtProcess(Process process)
    //    {
    //        try
    //        {
    //            m_process = process;
    //            //m_filePath = ProcessHelper.GetExecutablePath(m_process.Handle);
    //            m_filePath = process.MainModule.FileVersionInfo.FileName;
    //            m_ownerSid = GetOwnerSid(m_process.Handle);
    //            //process.EnableRaisingEvents = true;
    //            //process.Exited += new EventHandler(Process_Exited);
    //        } // try
    //        catch (Win32Exception w32Exc)
    //        {
    //            Trace.WriteLine("[LKCORLIB.NtProcess] " + process.ProcessName + " (PID " + process.Id + ")");
    //            Trace.WriteLine("[LKCORLIB.NtProcess] " + w32Exc.ToString());
    //        }
    //    }

    //    #endregion

    //    #region 소멸자

    //    /// <summary>
    //    /// 인스턴스가 할당한 리소스를 모두 해제한다.
    //    /// </summary>
    //    ~NtProcess()
    //    {
    //        Close();
    //    }

    //    #endregion

    //    #region 정적 메소드

    //    /// <summary>
    //    /// 새 <see cref="NtProcess"/> 개체를 생성한 후 현재 활성화되어 있는 프로세스에 연결한다.
    //    /// </summary>
    //    /// <returns>호출하는 응용 프로그램에서 실행 중인 프로세스 리소스에 연결된 새 <see cref="NtProcess"/> 개체</returns>
    //    public static NtProcess GetCurrentProcess()
    //    {
    //        Process process = Process.GetCurrentProcess();

    //        return (new NtProcess(process));
    //    }

    //    /// <summary>
    //    /// 로컬 컴퓨터의 프로세서에 대한 식별자가 주어지면 새 <see cref="NtProcess"/> 개체를 반환한다.
    //    /// </summary>
    //    /// <param name="processId">프로세스 리소스의 시스템 고유 식별자</param>
    //    /// <returns>
    //    /// <paramref name="processId"/> 매개 변수에 의해 확인되는 로컬 프로세스 리소스에 연결된 <see cref="NtProcess"/> 개체
    //    /// </returns>
    //    /// 
    //    /// <exception cref="ArgumentException">
    //    /// <paramref name="processId"/> 매개 변수에서 지정한 프로세스가 실행되지 않는 경우
    //    /// </exception>
    //    public static NtProcess GetProcessById(int processId)
    //    {
    //        Process process = Process.GetProcessById(processId);

    //        return (new NtProcess(process));
    //    }

    //    /// <summary>
    //    /// 로컬 컴퓨터의 각 프로세스 리소스에 대해 새 <see cref="NtProcess"/> 개체를 만든다.
    //    /// </summary>
    //    /// <returns>로컬 컴퓨터에서 실행 중인 모든 프로세스 리소스를 나타내는 <see cref="NtProcess"/> 개체의 배열</returns>
    //    public static NtProcess[] GetProcesses()
    //    {
    //        List<NtProcess> list = new List<NtProcess>();

    //        foreach (Process process in Process.GetProcesses())
    //        {
    //            list.Add(new NtProcess(process));
    //        }

    //        return list.ToArray();
    //    }

    //    /// <summary>
    //    /// <see cref="NtProcess"/> 개체로 이루어진 새 배열을 만들어 
    //    /// 지정한 프로세스 이름을 공유하는 로컬 컴퓨터의 모든 프로세스 리소스에 연결한다.
    //    /// </summary>
    //    /// <param name="processName">프로세스의 이름</param>
    //    /// <returns>
    //    /// 지정한 응용 프로그램 또는 파일을 실행 중인 프로세스 리소스를 나타내는 <see cref="NtProcess"/> 개체의 배열
    //    /// </returns>
    //    /// 
    //    /// <exception cref="InvalidOperationException">
    //    /// 프로세스 정보를 얻는데 사용되는 성능 카운터 API에 액세스하는데 문제가 있는 경우.
    //    /// 이 예외는 Windows NT, Windows 2000 및 Windows XP에만 존재한다.
    //    /// </exception>
    //    public static NtProcess[] GetProcessesByName(string processName)
    //    {
    //        List<NtProcess> list = new List<NtProcess>();

    //        foreach (Process process in Process.GetProcessesByName(processName))
    //        {
    //            list.Add(new NtProcess(process));
    //        }

    //        return list.ToArray();
    //    }

    //    #endregion

    //    #region 정적 메소드

    //    /// <summary>
    //    /// 지정한 프로세스를 시작한 실행 파일의 경로를 가져온다.
    //    /// </summary>
    //    /// <param name="processHandle">실행 파일의 전체 경로를 가져올 프로세스에 대한 핸들</param>
    //    /// <returns>프로세스를 시작한 실행 파일의 경로</returns>
    //    /// <remarks>
    //    /// 프로세스 핸들은 PROCESS_QUERY_INFORMATION 액세스 권한을 가지고 있어야 한다.
    //    /// </remarks>
    //    /// 
    //    /// <exception cref="Win32Exception">시스템 API에 액세스하는 동안 오류가 발생한 경우</exception>
    //    public static string GetExecutablePath(IntPtr processHandle)
    //    {
    //        /*
    //         * Windows Vista 이상은 "Microsoft's Mup Redirector (\Device\Mup)" 파일 시스템
    //         * Windows XP 이하는 "Microsoft's LanMan Redirector (\Device\LanmanRedirector)" 파일 시스템
    //         */
    //        StringBuilder buffer = new StringBuilder(1024);
    //        string win32FilePath = null; // Win32 path format
    //        string nsFilePath = null; // native system path format
    //        string networkVolumeName = null;
    //        int length = 0;

    //        networkVolumeName = IsVistaOrLater ? @"\Device\Mup" : @"\Device\LanmanRedirector";

    //        // 실행 파일의 전체 경로를 가져온다.
    //        // \Device\HarddiskVolume1\WINDOWS\explorer.exe
    //        length = Win32Native.GetProcessImageFileName(processHandle, buffer, buffer.Capacity);
    //        if (length == 0) throw new Win32Exception();

    //        nsFilePath = buffer.ToString();
    //        System.Diagnostics.Trace.WriteLine("[LKCORLIB] " + nsFilePath);

    //        // 네트워크 볼륨에 있는 실행 파일인 경우
    //        if (nsFilePath.StartsWith(networkVolumeName))
    //        {
    //            win32FilePath = nsFilePath.Replace(networkVolumeName, "\\");
    //        }
    //        // 로컬 디스크에 있는 실행 파일인 경우
    //        else
    //        {
    //            //
    //            // Native System 경로 포맷으로 된 실행 파일의 경로를 Win32 경로 포맷으로 변환한다.
    //            // \Device\HarddiskVolume1\WINDOWS\explorer.exe => C:\WINDOWS\explorer.exe
    //            //
    //            string driveName = null;
    //            string volumeName = null;
    //            bool succeeded = false;

    //            // 볼륨 경로를 가져온다. (C:\, D:\, etc)
    //            succeeded = Win32Native.GetVolumePathName(nsFilePath, buffer, buffer.Capacity);
    //            if (!succeeded) throw new Win32Exception();

    //            driveName = buffer.ToString(0, 2);
    //            System.Diagnostics.Trace.WriteLine("[LKCORLIB] drive name: " + driveName);

    //            // 볼륨 이름을 가져온다. (\Device\HarddiskVolume1, etc)
    //            length = Win32Native.QueryDosDevice(driveName, buffer, buffer.Capacity);
    //            if (length == 0) throw new Win32Exception();

    //            volumeName = buffer.ToString();
    //            System.Diagnostics.Trace.WriteLine("[LKCORLIB] volume name: " + volumeName);

    //            win32FilePath = nsFilePath.Replace(volumeName, driveName);
    //        } // else

    //        return win32FilePath;
    //    }

    //    /// <summary>
    //    /// 지정한 프로세스의 소유자에 대한 SID(보안 식별자)를 가져온다.
    //    /// </summary>
    //    /// <param name="processHandle">소유자의 SID(보안 식별자)를 가져올 프로세스에 대한 핸들</param>
    //    /// <returns>프로세스의 소유자에 대한 SID(보안 식별자)를 나타내는 <see cref="SecurityIdentifier"/> 개체</returns>
    //    /// <remarks>
    //    /// 프로세스 핸들은 PROCESS_QUERY_INFORMATION 액세스 권한을 가지고 있어야 한다.
    //    /// </remarks>
    //    /// 
    //    /// <exception cref="Win32Exception">시스템 API에 액세스하는 동안 오류가 발생한 경우</exception>
    //    public static SecurityIdentifier GetOwnerSid(IntPtr processHandle)
    //    {
    //        AllocatedMemory buffer = new AllocatedMemory(256); // 메모리를 할당한다.
    //        SecurityIdentifier userSid = null;
    //        IntPtr procToken = Win32Native.NULL;

    //        try
    //        {
    //            Win32Native.TOKEN_USER tknUser = null;
    //            IntPtr ptrTokenInfo = buffer.Pointer;
    //            int returnLength = 0;
    //            bool succeeded = false;

    //            // 프로세스와 관련된 토큰을 연다.
    //            succeeded = Win32Native.OpenProcessToken(processHandle, TokenAccessLevels.Query,
    //                                                     out procToken);
    //            if (!succeeded) throw new Win32Exception();

    //            // 토큰의 사용자 계정에 대한 정보를 가져온다.
    //            succeeded = Win32Native.GetTokenInformation(procToken,
    //                            Win32Native.TOKEN_INFORMATION_CLASS.TokenUser, ptrTokenInfo, buffer.Size,
    //                            out returnLength);
    //            if (!succeeded) throw new Win32Exception();

    //            tknUser = (Win32Native.TOKEN_USER)
    //                      Marshal.PtrToStructure(ptrTokenInfo, typeof(Win32Native.TOKEN_USER));
    //            // 사용자 SID(보안 식별자)를 가져온다.
    //            userSid = new SecurityIdentifier(tknUser.User.Sid);

    //            /*
    //            StringBuilder name = new StringBuilder(bufferSize);
    //            StringBuilder domainName = new StringBuilder(bufferSize);
    //            SID_NAME_USE sidType;
    //            uint nameLength = (uint)name.Capacity;
    //            uint domainNameLength = (uint)domainName.Capacity;
                
    //            // 사용자 SID(보안 식별자)에 대한 계정 이름과 도메인 이름을 가져온다.
    //            succeeded = Win32Native.LookupAccountSid(null, tknUser.User.Sid, 
    //                            name, ref nameLength, domainName, ref domainNameLength, 
    //                            out sidType);
    //            if (!succeeded) throw new Win32Exception();
    //             */
    //        } // try
    //        finally
    //        {
    //            // 토큰을 닫는다.
    //            if (procToken != Win32Native.NULL) Win32Native.CloseHandle(procToken);
    //        }

    //        return userSid;
    //    }

    //    #endregion

    //    #region 메소드

    //    /// <summary>
    //    /// <see cref="NtProcess"/> 개체에 연결된 리소스를 모두 해제한다.
    //    /// </summary>
    //    public void Close()
    //    {
    //        // 프로세스 구성 요소에 연결된 리소스를 해제한다.
    //        if (m_process != null) m_process.Close();
    //    }

    //    #endregion

    //    #region Object 멤버

    //    /// <summary>
    //    /// 현재 <see cref="NtProcess"/> 개체의 문자열 표현을 반환한다.
    //    /// </summary>
    //    /// <returns>현재 <see cref="NtProcess"/> 개체의 문자열 표현</returns>
    //    public override string ToString()
    //    {
    //        string toStr = null;
    //        string fileName = null;

    //        try { fileName = this.FileName; }
    //        catch { fileName = m_process.ProcessName; }
    //        finally
    //        {
    //            toStr = fileName + " (PID " + m_process.Id + "), " + this.Owner;
    //        }

    //        return toStr;
    //    }

    //    #endregion
    //}
}
