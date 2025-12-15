#region 변경 이력
/*
 * Author : Link mskoo (2013. 2. 12)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2013-02-12   mskoo           최초 작성.
 * 
 * 2013-05-14   mskoo           메소드 추가.
 *                              - IsExceeded(IEnumerable<SharedDrive>)
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
// log4net 라이브러리
using log4net;
// .NET 라이브러리
using Link.Core;
using Link.Core.IO;
using Link.Core.Net;

namespace Link.EFAM.Core
{
    using Win32Exception = System.ComponentModel.Win32Exception;

    /// <summary>
    /// 공유 드라이브들을 연결하거나 연결을 끊는 메소드를 제공한다.
    /// </summary>
    public static class SharedDriveHelper
    {
        private static BooleanSwitch m_traceSwitch = new BooleanSwitch("traceSwitch", "SharedDriveHelper");
        private static ILog m_logger = LogManager.GetLogger(typeof(SharedDriveHelper));

        #region 정적 메소드

        /// <summary>
        /// 지정한 공유 드라이브들과 관련된 네트워크 공유에 대한 연결들을 끊는다.
        /// </summary>
        /// <param name="drives">공유 드라이브의 컬렉션</param>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="drives"/>가 <b>null</b>인 경우</exception>
        public static void CancelExistingNetworkConnections(IEnumerable<SharedDrive> drives)
        {
            if (drives == null) throw new ArgumentNullException("drives");

            NetworkConnection[] netwkConnList = null;
            string uncPathRoot = null;

            try
            {
                // 컴퓨터의 네트워크 연결들을 가져온다.
                netwkConnList = NetworkConnection.GetNetworkConnections(NetworkResourceType.Disk);

                foreach (NetworkConnection netwkConn in netwkConnList)
                {
                    uncPathRoot = NtPath.GetPathRoot(netwkConn.RemoteName);

                    // 공유 드라이브로 연결할 네트워크 공유에 대한 연결이 있으면 연결을 끊는다.
                    try
                    {
                        foreach (SharedDrive drive in drives)
                        {
                            if (String.Equals(uncPathRoot, NtPath.GetPathRoot(drive.ShareName),
                                              StringComparison.OrdinalIgnoreCase))
                            {
                                if (m_traceSwitch.Enabled) Trace.WriteLine("[EFAM.Trace] SharedDriveHelper.CancelExistingNetworkConnections() cancel network connection. " + netwkConn);

                                netwkConn.Cancel(true, true);
                                break;
                            }
                        } // foreach ( SharedDrive )
                    } // try
                    catch (Exception innerExc)
                    {
                        if (m_logger.IsErrorEnabled)
                        {
                            m_logger.Error("SharedDriveHelper.CancelExistingNetworkConnections()\n" + innerExc);
                        }
                        if (m_traceSwitch.Enabled) Trace.WriteLine("[EFAM.Trace] SharedDriveHelper.CancelExistingNetworkConnections()\n" + innerExc);
                    } // catch
                } // foreach ( NetworkConnection )
            } // try
            catch (Exception exc)
            {
                if (m_logger.IsErrorEnabled) m_logger.Error("SharedDriveHelper.CancelExistingNetworkConnections()\n" + exc);
            }
        }

        /// <summary>
        /// 지정한 공유 드라이브들을 연결한다.
        /// </summary>
        /// <param name="drives">연결할 공유 드라이브의 컬렉션</param>
        /// <returns>공유 드라이브들을 성공적으로 연결한 경우 <b>true</b>, 그렇지 않으면 <b>false</b></returns>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="drives"/>가 <b>null</b>인 경우</exception>
        public static bool ConnectSharedDrives(IEnumerable<SharedDrive> drives)
        {
            if (drives == null) throw new ArgumentNullException("drives");

            List<SharedDrive> retryList = new List<SharedDrive>();
            bool success = true;

            //
            // 특정 드라이브 이름을 지정하거나 빈 문자열로 지정한 공유 드라이브들을 연결한다.
            // 드라이브 이름과 관련된 예외가 throw되면 드라이브 이름이 자동으로 지정되도록 한다.
            //
            foreach (SharedDrive drive in drives)
            {
                if (!drive.Usable) continue;

                try
                {
                    if (drive.DriveName != "*")
                    {
                        if (m_traceSwitch.Enabled) Trace.WriteLine("[EFAM.Trace] Connect network drive. " + drive.UserName + ", " + drive);
                        drive.Connect();
                    }
                } // try
                catch (InvalidOperationException opExc)
                {
                    object value = opExc.Data["Win32ErrorCode"];
                    int errorCode = (value != null) ? (int)value : 0;

                    if (m_traceSwitch.Enabled) Trace.WriteLine("[EFAM.Error] Connecting network drive is failed. Error " + errorCode + "; " + drive);

                    // 드라이브 이름이 자동으로 지정되도록 한다.
                    if (errorCode == NativeMethods.ERROR_BAD_DEVICE ||
                        errorCode == NativeMethods.ERROR_ALREADY_ASSIGNED ||
                        errorCode == NativeMethods.ERROR_DEVICE_ALREADY_REMEMBERED)
                    {
                        if (m_traceSwitch.Enabled) Trace.WriteLine("[EFAM.Trace] Set auto assign. " + drive);
                        drive.DriveName = "*";
                    }
                } // catch
                catch (Exception) { }
            } // foreach ( SharedDrive )

            //
            // 연결되지 않은 공유 드라이브들을 연결한다.
            //
            foreach (SharedDrive drive in drives)
            {
                if (!drive.Usable) continue;

                try
                {
                    if (!drive.IsConnected)
                    {
                        if (m_traceSwitch.Enabled) Trace.WriteLine("[EFAM.Trace] Connect network drive. " + drive.UserName + ", " + drive);
                        drive.Connect();
                    }
                }
                catch (Exception exc)
                {
                    object value = exc.Data["Win32ErrorCode"];
                    int errorCode = (value != null) ? (int)value : 0;

                    // 공유 드라이브 연결을 재시도한다.
                    if (errorCode == NativeMethods.ERROR_SESSION_CREDENTIAL_CONFLICT ||
                        errorCode == NativeMethods.ERROR_LOGON_FAILURE ||
                        errorCode == NativeMethods.ERROR_INVALID_PASSWORD)
                    {
                        try { drive.Connect(); }
                        catch { }
                        if (drive.IsConnected) continue;
                    }

                    string format = "SharedDriveHelper.ConnectSharedDrives() - {0}\n{1}";

                    if (m_logger.IsErrorEnabled) m_logger.ErrorFormat(format, drive, exc);
                    if (m_traceSwitch.Enabled) Trace.WriteLine("[EFAM.Error] Connecting network drive is failed. " + drive);
                    success = false;
                } // catch
            } // foreach ( SharedDrive )

            return success;
        }

        /// <summary>
        /// 지정한 공유 드라이브들의 연결을 끊는다.
        /// </summary>
        /// <param name="drives">연결을 끊을 공유 드라이브의 컬렉션</param>
        /// <returns>공유 드라이브들의 연결을 성공적으로 끊은 경우 <b>true</b>, 그렇지 않으면 <b>false</b></returns>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="drives"/>가 <b>null</b>인 경우</exception>
        public static bool DisconnectSharedDrives(IEnumerable<SharedDrive> drives)
        {
            if (drives == null) throw new ArgumentNullException("drives");

            bool success = true;

            foreach (SharedDrive drive in drives)
            {
                // 공유 드라이브의 연결을 끊는다.
                try
                {
                    if (drive.IsConnected) drive.Disconnect(true);
                    //if (drive.Usable) drive.Disconnect(true);
                }
                catch (Exception exc)
                {
                    string format = "SharedDriveHelper.DisconnectSharedDrives() - {0}\n{1}";

                    if (m_logger.IsErrorEnabled) m_logger.ErrorFormat(format, drive, exc);
                    success = false;
                } // catch
            } // foreach ( SharedDrive )

            return success;
        }

        /// <summary>
        /// 연결할 수 있는 공유 드라이브의 개수가 초과되었는지 여부를 확인한다.
        /// </summary>
        /// <param name="drives">공유 드라이브의 컬렉션</param>
        /// <returns>
        /// 연결할 수 있는 공유 드라이브의 개수가 초과되었으면 <b>true</b>, 그렇지 않으면 <b>false</b>
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="drives"/>가 <b>null</b>인 경우</exception>
        public static bool IsExceeded(IEnumerable<SharedDrive> drives)
        {
            if (drives == null) throw new ArgumentNullException("drives");

            int usableCount = NtEnvironment.GetUsableLogicalDrives().Length;
            int mountCount = 0;

            foreach (SharedDrive drive in drives)
            {
                if (drive.Usable && drive.DriveName.Length != 0)
                {
                    mountCount++;
                }
            } // foreach ( SharedDrive )

            return (mountCount > usableCount);
        }

        #endregion
    }
}
