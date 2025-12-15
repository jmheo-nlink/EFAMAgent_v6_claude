#region 변경 이력
/*
 * Author : Link mskoo (2011. 9. 8)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-09-08   mskoo           최초 작성.
 * 
 * 2011-09-23   mskoo           5.0 버전 릴리즈. (변경 이력 정리)
 * 
 * 2012-01-19   mskoo           Windows XP 64bit 운영체제에서는 Windows Server 2003 64bit 운영체제용 드라이버 파일을
 *                              사용하도록 코드를 수정.
 *                              - LoadFacFilter()
 *                              
 * 2012-01-29   mskoo           WMI 예외가 throw되면 Win32 API를 사용하여 기존 네트워크 연결들을 가져오도록 수정.
 *                              - CancelExistingNetworkConnections(Collection<NetworkDrive>)
 *                              
 * 2012-02-11   mskoo           클래스 이름을 'AgentUtils'에서 'AgentUtility'로 변경.
 * 
 * 2012-04-05   mskoo           사용 가능한 드라이브 이름을 알파벳 역순으로 가져오기 않는 오류를 수정.
 *                              - ConnectNetworkDrives(Collection<NetworkDrive>)
 *                              
 * 2012-06-15   mskoo           같은 공유 네트워크 폴더에 마운트된 네트워크 드라이브들의 연결이 끊어지지 않는 오류를 수정.
 *                              - CancelExistingNetworkConnections(Collection<NetworkDrive>)
 *                              
 * 2012-09-27   mskoo           메소드 추가.
 *                              - ReconnectNetworkDrives(Collection<NetworkDrive>)
 *                              
 * 2014-10-15   mskoo           Windows 8/8.1 용 FAC 필터를 로드하는 코드를 추가.
 *                              - LoadFacFilter()
 * 
 * 2016-08-     mskoo           Windows 10 용 FAC 필터를 로드하는 코드를 추가.
 *                              - LoadFacFilter()
 *                              
 * 2018-08-29   DJJUNG          Windows FAC 필터를 로드하는 코드를 수정
 *                              - LoadFacFilter()
 *                              
 * 2021-11-05   DJJUNG          Windows 11 용 FAC 필터를 로드하는 코드를 추가.
 *                              - LoadFacFilter()
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;
// log4net 라이브러리
using log4net;
// .NET용 개발 라이브러리
using Link.DLK.ServiceProcess;
using Link.DLK.Win32;

namespace Link.EFAM.Agent
{
    using Link.EFAM.Common;
    using CaseInsensitiveStringCollection = Link.DLK.Collections.StringCollection;
    using Path = System.IO.Path;

    /// <summary>
    /// 
    /// </summary>
    static class AgentUtility
    {
        //
        // 추적/로그
        //
        private static BooleanSwitch m_tracing = new BooleanSwitch("traceSwitch", "Agent Module");
        private static ILog m_logger = LogManager.GetLogger(typeof(AgentUtility));

        #region 메소드
        #region 미니필터 드라이버

        /// <summary>
        /// 액세스 제어 미니필터 드라이버가 설치되어 있는지 확인하고, 액세스 미니필터 드라이버를 로드한다.
        /// </summary>
        /// <remarks>
        /// 액세스 제어 미니필터 드라이버가 설치되어 있지 않으면 액세스 제어 미니필터 드라이버를 설치한다.
        /// </remarks>
        public static void LoadFacFilter()
        {
            MinifilterController filter = new MinifilterController(Constants.FacFilterName);

            try
            {
                if (!filter.IsInstalled)
                {
                    //
                    // 미니필터 드라이버를 설치한다.
                    //
                    MinifilterInstaller installer = new MinifilterInstaller();
                    //Version osVer = Environment.OSVersion.Version;
                    string fileName = "";

                    //// Windows XP
                    //if (osVer.Major == 5 && osVer.Minor == 1)
                    //{
                    //    fileName = (IntPtr.Size == 4) ? "FacFltXP.sys" : "FacFltW2K3.sys";
                    //}
                    //// Windows Server 2003, Windows Server 2003 R2
                    //else if (osVer.Major == 5 && osVer.Minor == 2) fileName = "FacFltW2K3.sys";
                    //// Windows Vista, Windows Server 2008
                    //else if (osVer.Major == 6 && osVer.Minor == 0) fileName = "FacFltVista.sys";
                    //// Windows 7, Windows Server 2008 R2
                    //else if (osVer.Major == 6 && osVer.Minor == 1) fileName = "FacFltWin7.sys";
                    //// Windows 8, Windows Server 2012
                    //else if (osVer.Major == 6 && osVer.Minor == 2) fileName = "FacFltWin8.sys";
                    //// Windows 8.1, Windows Server 2012 R2
                    //else if (osVer.Major == 6 && osVer.Minor == 3) fileName = "FacFltWin81.sys";
                    //// Windows 10, Windows Server 2016
                    //else if (osVer.Major == 10) fileName = "FacFltWin10.sys";

                    Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion"); // HKEY_LOCAL_MACHINE
                    string productName = registryKey.GetValue("ProductName").ToString();

                    // Windows 7
                    if (productName.Contains("Windows 7")) fileName = "FacFltWin7.sys";
                    // Windows 8
                    else if (productName.Contains("Windows 8") && !productName.Contains("Windows 8.1")) fileName = "FacFltWin8.sys";
                    // Windows 8.1
                    else if (productName.Contains("Windows 8.1")) fileName = "FacFltWin81.sys";
                    // Windows 10
                    else if (productName.Contains("Windows 10")) fileName = "FacFltWin10.sys";
                    //// Windows 11
                    //else if (productName.Contains("Windows 11")) fileName = "FacFltWin11.sys";
                    
                    installer.ServiceName = Constants.FacFilterName;
                    installer.GroupName = "FSFilter Activity Monitor";
                    installer.Altitude = 389200;
                    //installer.GroupName = "FSFilter Content Screener";
                    //installer.Altitude = 269900;

                    installer.Install(Path.Combine(Application.StartupPath, fileName));
                } // if (!filter.IsInstalled)

                // 속성 값을 새로 고친다.
                filter.Refresh();

                if (filter.Status == ServiceControllerStatus.Stopped)
                {
                    //Win32Utils.AdjustPrivilege("SeLoadDriverPrivilege", true);
                    filter.Start();
                    filter.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 1, 0));
                }
            } // try
            finally
            {
                filter.Close();
            }
        }

        /// <summary>
        /// 액세스 제어 미니필터 드라이버를 언로드한다.
        /// </summary>
        public static void UnloadFacFilter()
        {
            MinifilterController filter = new MinifilterController(Constants.FacFilterName);

            try
            {
                if (!filter.IsInstalled) return;

                if (filter.Status == ServiceControllerStatus.Running)
                {
                    //Win32Utils.AdjustPrivilege("SeLoadDriverPrivilege", true);
                    filter.Stop();
                    filter.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 1, 0));
                }
            } // try
            finally
            {
                filter.Close();
            }
        }

        #endregion

        #region 네트워크 연결

        //
        // Win32 오류 코드
        //
        //private const int ERROR_ALREADY_ASSIGNED = 85;
        //private const int ERROR_DEVICE_ALREADY_REMEMBERED = 1202;
        //private const int ERROR_SESSION_CREDENTIAL_CONFLICT = 1219;
        //private const int ERROR_INVALID_PASSWORD = 86;
        //private const int ERROR_LOGON_FAILURE = 1326;

        /// <summary>
        /// 지정한 네트워크 드라이브들과 관련된 공유 네트워크 폴더에 대한 연결을 끊는다.
        /// </summary>
        /// <param name="networkDrives">네트워크 드라이브의 컬렉션</param>
        /// 
        /// <exception cref="ArgumentNullException">networkDrives가 null인 경우</exception>
        //public static void CancelExistingNetworkConnections(Collection<NetworkDrive> networkDrives)
        //{
        //    if (networkDrives == null) throw new ArgumentNullException("networkDrives");

        //    NetworkConnection[] existingNetwkConns = null;

        //    // 시스템에 생성된 네트워크 연결들을 가져온다.
        //    try
        //    {
        //        existingNetwkConns = NetworkUtility.GetExistingConnections();
        //    }
        //    catch (System.ComponentModel.Win32Exception win32Exc)
        //    {
        //        string message = "AgentUtility.CancelExistingNetworkConnections()\n" + win32Exc;

        //        if (m_logger.IsErrorEnabled) m_logger.Error(message);
        //        if (m_tracing.Enabled) Trace.WriteLine("[EFAM.Error] " + message);
        //    }
        //    if (existingNetwkConns == null) return;

        //    //
        //    // 네트워크 드라이브로 연결할 공유 네트워크 폴더에 대한 연결이 있으면 연결을 끊는다.
        //    //
        //    string name = null;

        //    foreach (NetworkConnection netwkConn in existingNetwkConns)
        //    {
        //        foreach (NetworkDrive drive in networkDrives)
        //        {
        //            if (!String.Equals(netwkConn.RemotePath, drive.SharedFolderName,
        //                               StringComparison.OrdinalIgnoreCase)) continue;
                    
        //            // 네트워크 연결을 끊는다.
        //            try
        //            {
        //                name = String.IsNullOrEmpty(netwkConn.LocalName)
        //                     ? netwkConn.RemotePath : netwkConn.LocalName;
        //                NetworkUtility.CancelConnection(name, true, true);
        //            }
        //            catch { }
        //        } // foreach ( NetworkDrive )
        //    } // foreach ( NetworkConnection )
        //}

        /// <summary>
        /// 지정한 네트워크 드라이브들을 연결한다.
        /// </summary>
        /// <param name="networkDrives">연결할 네트워크 드라이브의 컬렉션</param>
        /// <returns>네트워크 드라이브들을 연결하는 동안 오류가 발생하지 않으면 true, 그렇지 않으면 false</returns>
        /// 
        /// <exception cref="ArgumentNullException">networkDrives가 null인 경우</exception>
        //public static bool ConnectNetworkDrives(Collection<NetworkDrive> networkDrives)
        //{
        //    if (networkDrives == null) throw new ArgumentNullException("networkDrives");

        //    //
        //    // 각 네트워크 드라이브에 설정된 드라이브 이름이 유효한지 확인하고,
        //    // 유효하지 않은 드라이브 이름은 사용할 수 있는 드라이브 이름으로 변경한다.
        //    // 유효한 드라이브 이름이 설정된 네트워크 드라이브를 연결한다.
        //    //
        //    CaseInsensitiveStringCollection usableDriveNameColl = WindowsUtility.GetUsableDriveNames();
        //    bool nameChecking = false;
        //    bool needRetry = false;
        //    bool noError = true;

        //    foreach (NetworkDrive drive in networkDrives)
        //    {
        //        nameChecking = (drive.DriveName.Length > 0);
        //        needRetry = false;

        //        do
        //        {
        //            //
        //            // 로컬 드라이브에 매핑할 경우 사용할 수 있는 드라이브 이름인지 확인한다.
        //            //
        //            if (nameChecking)
        //            {
        //                if (usableDriveNameColl.Count > 0)
        //                {
        //                    if (!usableDriveNameColl.Contains(drive.DriveName))
        //                    {
        //                        // 사용할 수 있는 드라이브 이름을 설정한다.
        //                        drive.DriveName = usableDriveNameColl[0];
        //                    }
        //                    usableDriveNameColl.Remove(drive.DriveName);
        //                } // if (usableDriveNameColl.Count > 0)
        //                else if (needRetry) break;

        //                nameChecking = false;
        //            } // if (nameChecking)

        //            //
        //            // 네트워크 드라이브를 연결한다.
        //            //
        //            try
        //            {
        //                drive.Connect();
        //            }
        //            catch (System.ComponentModel.Win32Exception win32Exc)
        //            {
        //                int errorCode = win32Exc.NativeErrorCode;

        //                // 로컬 드라이브 이름을 변경한다.
        //                if (errorCode == ERROR_ALREADY_ASSIGNED
        //                    || errorCode == ERROR_DEVICE_ALREADY_REMEMBERED)
        //                {
        //                    nameChecking = true;
        //                    needRetry = true;
        //                    continue;
        //                }
        //                // 네트워크 드라이브 연결을 재시도한다.
        //                else if (errorCode == ERROR_SESSION_CREDENTIAL_CONFLICT
        //                        || errorCode == ERROR_LOGON_FAILURE
        //                        || errorCode == ERROR_INVALID_PASSWORD)
        //                {
        //                    try 
        //                    {
        //                        drive.Connect();
        //                        if (drive.IsConnected) break;
        //                    }
        //                    catch { }
        //                } // else if (ERROR_SESSION_CREDENTIAL_CONFLICT || ERROR_LOGON_FAILURE || ERROR_INVALID_PASSWORD)

        //                string message = String.Format("AgentUtility.ConnectNetworkDrives() - {0}\n{1}", drive, win32Exc);
        //                /*
        //                string message = String.Format("AgentUtility.ConnectNetworkDrives() - [{2} / {3}] {0}\n{1}",
        //                    drive, win32Exc, drive.UserName, drive.Password);
        //                 */

        //                if (m_logger.IsErrorEnabled) m_logger.Error(message);
        //                if (m_tracing.Enabled) Trace.WriteLine("[EFAM.Error] " + message);

        //                needRetry = false;
        //                noError = false;
        //            } // catch (Win32Exception)
        //        } while (!drive.IsConnected && needRetry);
        //    } // foreach ( NetworkDrive )

        //    return noError;
        //}

        /// <summary>
        /// 지정한 네트워크 드라이브들의 연결을 끊는다.
        /// </summary>
        /// <param name="networkDrives">연결을 끊을 네트워크 드라이브의 컬렉션</param>
        /// <returns>네트워크 드라이브들의 연결을 끊는 동안 오류가 발생하지 않으면 true, 그렇지 않으면 false</returns>
        /// 
        /// <exception cref="ArgumentNullException">networkDrives가 null인 경우</exception>
        //public static bool DisconnectNetworkDrives(Collection<NetworkDrive> networkDrives)
        //{
        //    if (networkDrives == null) throw new ArgumentNullException("networkDrives");

        //    bool noError = true;

        //    foreach (NetworkDrive drive in networkDrives)
        //    {
        //        //
        //        // 네트워크 드라이브의 연결을 끊는다.
        //        //
        //        try
        //        {
        //            drive.Disconnect(true);
        //        }
        //        catch (System.ComponentModel.Win32Exception win32Exc)
        //        {
        //            string message = String.Format(
        //                "AgentUtility.DisconnectNetworkDrives() - {0}\n{1}", drive, win32Exc);

        //            if (m_logger.IsErrorEnabled) m_logger.Error(message);
        //            if (m_tracing.Enabled) Trace.WriteLine("[EFAM.Error] " + message);

        //            noError = false;
        //        } // catch (Win32Exception)
        //    } // foreach ( NetworkDrive )

        //    return noError;
        //}

        /// <summary>
        /// 지정한 네트워크 드라이브들을 다시 연결한다.
        /// </summary>
        /// <param name="networkDrives">다시 연결할 네트워크 드라이브의 컬렉션</param>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="networkDrives"/>가 <b>null</b>인 경우</exception>
        //public static void ReconnectNetworkDrives(Collection<NetworkDrive> networkDrives)
        //{
        //    if (networkDrives == null) throw new ArgumentNullException("networkDrives");

        //    foreach (NetworkDrive drive in networkDrives)
        //    {
        //        if (drive.DriveName.Length == 0) continue;

        //        //
        //        // 네트워크 드라이브를 다시 연결한다.
        //        //
        //        try { drive.Disconnect(true); }
        //        catch { }
        //        try
        //        {
        //            drive.Connect();
        //        }
        //        catch (System.ComponentModel.Win32Exception win32Exc)
        //        {
        //            string message = String.Format(
        //                "AgentUtility.ReconnectNetworkDrives() - {0}\n{1}", drive, win32Exc);

        //            if (m_logger.IsErrorEnabled) m_logger.Error(message);
        //            if (m_tracing.Enabled) Trace.WriteLine("[EFAM.Error] " + message);
        //        }
        //    } // foreach ( NetworkDrive )
        //}

        #endregion
        #endregion
    }
}
