#region 변경 이력
/*
 * Author : Link mskoo (2011. 6. 11)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-06-11   mskoo           최초 작성.
 * 
 * 2011-06-23   mskoo           클래스 이름을 'Win32Utils'에서 'WindowsUtils'로 변경.
 * 
 * 2011-07-03   mskoo           1.0 버전 릴리즈.
 * 
 * 2011-07-09   mskoo           메소드 추가.
 *                              - GetExecutablePath(IntPtr)
 *                              
 * 2011-08-17   mskoo           Windows 레지스트리 기본 키를 HKEY_LOCAL_MACHINE에서 HKEY_CURRENT_USER로 변경.
 *                              - AddStartupProgram(string, string)
 *                              - RemoveStartupProgram(string)
 *                              
 * 2011-09-07   mskoo           메소드 추가.
 *                              - AddTaskToScheduler(string, string)
 *                              - RemoveTaskFromScheduler(string)
 *                              
 * 2012-01-30   mskoo           클래스 이름을 'WindowsUtils'에서 'WindowsUtility'로 변경.
 * 
 * 2012-02-12   mskoo           프로세스를 시작할 때 운영 체제 셸을 사용하도록 수정.
 *                              - AddTaskToScheduler(string, string)
 *                              - RemoveTaskFromScheduler(string)
 * ====================================================================================================================
 */
#endregion

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

using Link.DLK.Collections;
//using Link.DLK.Properties;

namespace Link.DLK.Win32
{
    using Resources2 = Link.EFAM.Engine.Properties.Resources;

    /// <summary>
    /// 윈도우즈 환경에서 사용할 수 있는 유틸리티 메소드를 제공한다.
    /// </summary>
    public static class WindowsUtility
    {
        #region P/Invoke 프로토타입

        [DllImport("Psapi.dll", EntryPoint = "GetProcessImageFileNameW", CharSet = CharSet.Unicode, SetLastError = true)]
        internal extern static int GetProcessImageFileName(
            IntPtr hProcess, [Out] StringBuilder lpImageFileName, Int32 nSize);

        [DllImport("Kernel32.dll", EntryPoint = "GetVolumePathNameW", CharSet = CharSet.Unicode, SetLastError = true)]
        internal extern static bool GetVolumePathName(
            string lpszFileName, [Out] StringBuilder lpszVolumePathName, Int32 cchBufferLength);

        [DllImport("Kernel32.dll", EntryPoint = "QueryDosDeviceW", CharSet = CharSet.Unicode, SetLastError = true)]
        internal extern static int QueryDosDevice(
            string lpDeviceName, [Out] StringBuilder lpTargetPath, Int32 ucchMax);

        #endregion

        #region 메소드
        #region 시작 프로그램

        // 시작 프로그램 목록에 대한 레지스트리 키의 이름
        private const string REGKEY_RUN = @"Software\Microsoft\Windows\CurrentVersion\Run";

        /// <summary>
        /// 시작 프로그램 목록에 프로그램을 추가한다.
        /// </summary>
        /// <param name="appName">추가할 프로그램 이름</param>
        /// <param name="command">프로그램을 실행하기 위한 명령</param>
        /// 
        /// <exception cref="ArgumentNullException">appName 또는 command가 null인 경우</exception>
        /// <exception cref="ArgumentException">
        /// appName이 길이가 0인 문자열이거나 공백만 포함한 경우<br/>
        /// - 또는 -<br/>
        /// appName이 최대 허용 길이(255자)보다 긴 경우
        /// </exception>
        /// <exception cref="System.Security.SecurityException">
        /// 사용자에게 읽기/쓰기 모드에서 레지스트리 키에 액세스하는 데 필요한 권한이 없는 경우<br/>
        /// - 또는 -<br/>
        /// 사용자에게 레지스트리 키를 수정하는 데 필요한 권한이 없는 경우
        /// </exception>
        public static void AddStartupProgram(string appName, string command)
        {
            if (appName == null) throw new ArgumentNullException("appName");
            if (appName.Trim().Length == 0)
            {
                throw new ArgumentException(Resources2.Argument_EmptyString, "appName");
            }
            if (command == null) throw new ArgumentNullException("command");

            RegistryKey regKey = null;

            try
            {
                // 시작 프로그램 목록에 대한 레지스트리 키를 가져온다.
                regKey = Registry.CurrentUser.OpenSubKey(REGKEY_RUN, true);

                // 문자열 값을 새로 만든다.
                if (regKey != null)
                {
                    regKey.SetValue(appName, command, RegistryValueKind.String);
                }
            } // try
            finally
            {
                if (regKey != null) regKey.Close();
            }
        }

        /// <summary>
        /// 시작 프로그램 목록에서 프로그램을 제거한다.
        /// </summary>
        /// <param name="appName">제거할 프로그램 이름</param>
        /// 
        /// <exception cref="ArgumentNullException">appName이 null인 경우</exception>
        /// <exception cref="ArgumentException">appName이 길이가 0인 문자열이거나 공백만 포함한 경우</exception>
        /// <exception cref="System.Security.SecurityException">
        /// 사용자에게 읽기/쓰기 모드에서 레지스트리 키에 액세스하는 데 필요한 권한이 없는 경우<br/>
        /// - 또는 -<br/>
        /// 사용자에게 값을 삭제하는 데 필요한 권한이 없는 경우
        /// </exception>
        public static void RemoveStartupProgram(string appName)
        {
            if (appName == null) throw new ArgumentNullException("appName");
            if (appName.Trim().Length == 0)
            {
                throw new ArgumentException(Resources2.Argument_WhiteSpaceString, "appName");
            }

            RegistryKey regKey = null;

            try
            {
                // 시작 프로그램 목록에 대한 레지스트리 키를 가져온다.
                regKey = Registry.CurrentUser.OpenSubKey(REGKEY_RUN, true);

                // 문자열 값을 삭제한다.
                if (regKey != null) regKey.DeleteValue(appName);
            } // try
            catch (ArgumentException) { }
            finally
            {
                if (regKey != null) regKey.Close();
            }
        }

        #endregion

        #region 작업 스케줄러

        /// <summary>
        /// 작업 스케줄러에 작업을 추가한다.
        /// </summary>
        /// <param name="taskName">추가할 작업의 이름</param>
        /// <param name="taskRun">작업이 실행할 프로그램이나 명령</param>
        /// <remarks>
        /// 사용자가 시스템에 로그온할 때 실행할 작업을 작업 스케줄러에 추가한다.
        /// </remarks>
        /// 
        /// <exception cref="ArgumentNullException">taskName 또는 taskRun이 null인 경우</exception>
        /// <exception cref="ArgumentException">
        /// taskName 또는 taskRun이 길이가 0인 문자열이거나 공백만 포함한 경우
        /// </exception>
        public static void AddTaskToScheduler(string taskName, string taskRun)
        {
            if (taskName == null) throw new ArgumentNullException("taskName");
            if (taskName.Trim().Length == 0)
            {
                throw new ArgumentException(Resources2.Argument_WhiteSpaceString, "taskName");
            }
            if (taskRun == null) throw new ArgumentNullException("taskRun");
            if (taskRun.Trim().Length == 0)
            {
                throw new ArgumentException(Resources2.Argument_WhiteSpaceString, "taskRun");
            }

            Process process = new Process();

            process.StartInfo.FileName = "schtasks";
            process.StartInfo.Arguments =
                String.Format("/create /tn {0} /tr {1} /sc ONLOGON /rl HIGHEST", taskName, taskRun);
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			process.Start();

        }

        /// <summary>
        /// 작업 스케줄러에서 작업을 제거한다. 
        /// </summary>
        /// <param name="taskName">제거할 작업의 이름</param>
        /// 
        /// <exception cref="ArgumentNullException">taskName이 null인 경우</exception>
        /// <exception cref="ArgumentException">taskName이 길이가 0인 문자열이거나 공백만 포함한 경우</exception>
        public static void RemoveTaskFromScheduler(string taskName)
        {
            if (taskName == null) throw new ArgumentNullException("taskName");
            if (taskName.Trim().Length == 0)
            {
                throw new ArgumentException(Resources2.Argument_WhiteSpaceString, "taskName");
            }

            Process process = new Process();

            process.StartInfo.FileName = "schtasks";
			process.StartInfo.Arguments = String.Format("/delete /tn {0} /f", taskName);
            process.StartInfo.UseShellExecute = true;
			process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			process.Start();
        }

        #endregion

        /// <summary>
        /// 사용할 수 있는 드라이브 이름들을 가져온다.
        /// </summary>
        /// <returns>사용할 수 있는 드라이브 이름들이 포함된 컬렉션</returns>
        /// 
        /// <exception cref="System.IO.IOException">I/O 오류가 발생하는 경우</exception>
        /// <exception cref="System.Security.SecurityException">호출자에게 필요한 권한이 없는 경우</exception>
        public static StringCollection GetUsableDriveNames()
        {
            StringCollection driveNameColl = new StringCollection();
            
            //
            // 모든 드라이브 이름을 컬렉션에 추가한다.
            //
            for (char driveLetter = 'Z'; driveLetter > 'C'; driveLetter--)
            {
                driveNameColl.Add((driveLetter + ":"));
            }

            //
            // 컴퓨터에서 사용 중인 드라이브 이름들을 제거한다.
            //
            char[] trimChars = { '\\' };

            foreach (string driveName in Environment.GetLogicalDrives())
            {
                driveNameColl.Remove(driveName.TrimEnd(trimChars));
            }

            return driveNameColl;
        }

        #region 프로세스

        private const string DEVICENAME_MUP = @"\Device\Mup";
        private const string DEVICENAME_LANMAN = @"\Device\LanmanRedirector";

        private static bool m_isVistaOrLater = (Environment.OSVersion.Version.Major >= 6);

        /// <summary>
        /// 프로세스를 시작한 실행 파일의 전체 경로를 가져온다.
        /// </summary>
        /// <param name="processHandle">실행 파일의 전체 경로를 가져올 프로세스에 대한 핸들</param>
        /// <returns>프로세스를 시작한 실행 파일의 전체 경로</returns>
        /// <remarks>
        /// 프로세스 핸들은 PROCESS_QUERY_INFORMATION 액세스 권한을 가지고 있어야 한다.
        /// </remarks>
        /// 
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// 시스템 API에 액세스하는 동안 오류가 발생한 경우
        /// </exception>
        public static string GetExecutablePath(IntPtr processHandle)
        {
            StringBuilder buffer = new StringBuilder();
            string filePath = null;
            int bufferSize = 1024;

            buffer.Capacity = bufferSize;

            //
            // 프로세스를 시작한 실행 파일의 전체 경로(디바이스 이름 형태)를 검색한다.
            // \Device\HarddiskVolume1\WINDOWS\explorer.exe
            //
            string nativeFileName = null;
            int length = 0;

            length = GetProcessImageFileName(processHandle, buffer, bufferSize);
            if (length == 0) throw new System.ComponentModel.Win32Exception();

            nativeFileName = buffer.ToString();

            //
            // Windows Vista 이상은 "Microsoft's Mup Redirector (\Device\Mup)" 파일 시스템
            // Windows XP 이하는 "Microsoft's LanMan Redirector (\Device\LanmanRedirector)" 파일 시스템
            //
            string netwkVolumeName = m_isVistaOrLater ? DEVICENAME_MUP : DEVICENAME_LANMAN;

            //
            // 네트워크 볼륨에 있는 실행 파일일 경우
            //
            if (nativeFileName.StartsWith(netwkVolumeName))
            {
                filePath = nativeFileName.Replace(netwkVolumeName, "\\");
            }
            //
            // 로컬 디스크에 있는 실행 파일일 경우
            //
            else
            {
                //
                // 디바이스 이름 형태인 실행 파일의 경로를 드라이브 문자로 시작하는 실행 파일의 경로로
                // 변경하여 반환한다.
                // \Device\HarddiskVolume1\WINDOWS\explorer.exe => C:\WINDOWS\explorer.exe
                //
                string driveName = null;
                string volumeName = null;
                bool succeeded = false;

                // 볼륨 경로를 가져온다. (C:\, D:\, etc)
                succeeded = GetVolumePathName(nativeFileName, buffer, bufferSize);
                if (!succeeded) throw new System.ComponentModel.Win32Exception();

                driveName = buffer.ToString(0, 2);

                // 볼륨 이름을 가져온다. (\Device\HarddiskVolume1, etc)
                length = QueryDosDevice(driveName, buffer, bufferSize);
                if (length == 0) throw new System.ComponentModel.Win32Exception();

                volumeName = buffer.ToString();

                filePath = nativeFileName.Replace(volumeName, driveName);
            } // else

            return filePath;
        }

        #endregion 프로세스
        #endregion
    }
}
