#region 변경 이력
/*
 * Author : Link mskoo (2011. 4. 18)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-04-18   mskoo           최초 작성.
 *                              
 * 2011-09-23   mskoo           5.0 버전 릴리즈. (변경 이력 정리)
 * 
 * 2012-02-11   mskoo           클래스 이름을 'ConfigUtils'에서 'ConfigUtility'로 변경.
 * 
 * 2012-02-12   mskoo           메소드 추가.
 *                              - GetSavedNetworkDriveSettings()
 *                              - SetNetworkDriveSettings(NetworkDriveSetting[])
 *                              
 * 2012-02-12   mskoo           메소드 제거.
 *                              - GetSavedNetworkDrives()
 *                              - SetNetworkDrives(StringDictionary)
 *                              
 * 2012-02-18   mskoo           Windows Vista 이상의 운영체제에서 Windows를 시작할 때 자동으로 응용 프로그램을 실행하는
 *                              로직을 변경.
 *                              - ApplyAutoRunSetting(bool)
 *                              
 * 2012-02-26   mskoo           응용 프로그램 설정을 래핑한 AgentSettings 클래스를 사용하여 설정 속성을 액세스하도록 수정.
 *                              - ApplyAutoRunSetting(bool)
 *                              - ApplyAutoLoginSetting(bool, string, string)
 *                              
 * 2012-02-26   mskoo           메소드 제거.
 *                              - GetSavedUserId()
 *                              - SetUserId(string)
 *                              - GetSavedPassword()
 *                              - SetPassword(string)
 *                              - GetSavedNetworkDriveSettings()
 *                              - SetNetworkDriveSettings(NetworkDriveSetting[])
 *                              
 * 2012-02-26   mskoo           클래스 이름을 'ConfigUtility'에서 'ConfigHelper'로 변경.
 * 
 * 2012-03-30   mskoo           작업 스케줄러에 새 작업을 만드는 부분의 버그를 수정.
 *                              - ApplyAutoRunSetting(bool)
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
// log4net 라이브러리
using log4net;
// .NET용 개발 라이브러리
using Link.DLK.Win32;

namespace Link.EFAM.Agent.Configuration
{
    /// <summary>
    /// 응용 프로그램 설정에 설정 속성값을 적용하는 메소드를 제공한다.
    /// </summary>
    static class ConfigHelper
    {
        //
        // 추적/로그
        //
        private static BooleanSwitch m_tracing = new BooleanSwitch("traceSwitch", "Agent Module");
        private static ILog m_logger = LogManager.GetLogger(typeof(ConfigHelper));

        #region 메소드

        /// <summary>
        /// "윈도우 시작 시 자동 실행" 설정값을 적용한다.
        /// </summary>
        /// <param name="settingChecked">설정이 선택되었으면 true, 그렇지 않으면 false</param>
        public static void ApplyAutoRunSetting(bool settingChecked)
        {
            Version osVer = Environment.OSVersion.Version;
            string appName = "E-FAM Agent";
            string command = null;

            try
            {
                //
                // 운영체제가 Windows Vista, Windows Server 2008, Windows 7 일 경우
                //
                if (osVer.Major >= 6)
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo("schtasks.exe");
                    string taskName = "EFAMAgentExecute";

                    startInfo.CreateNoWindow = true;
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;

                    if (settingChecked)
                    {
                        Process process = null;

                        //
                        // 작업 스케줄러에 작업이 없으면 작업을 생성한다.
                        //
                        startInfo.Arguments = "/Query /TN " + taskName;

                        process = Process.Start(startInfo);
                        process.WaitForExit(1000);

                        if (process.HasExited && (process.ExitCode != 0))
                        {
                            string startTime = DateTime.Now.AddMinutes(-5).ToString("HH:mm");

                            startInfo.Arguments = "/Create /SC ONCE /TN " + taskName
                                                + " /TR \"'" + Application.ExecutablePath + "'\""
                                                + " /ST " + startTime + " /RL HIGHEST";
                            
                            Process.Start(startInfo);
                        } // if (process.HasExited && (process.ExitCode != 0))

                        // 작업을 실행하기 위한 작업 스케줄러 명령을 작성한다.
                        command = "\"" + Environment.GetFolderPath(Environment.SpecialFolder.System)
                                + "\\schtasks.exe\" /Run /TN " + taskName;
                    } // if (settingChecked)
                    else
                    {
                        //
                        // 작업 스케줄러에서 작업을 삭제한다.
                        //
                        startInfo.Arguments = "/Delete /TN " + taskName + " /F";

                        Process.Start(startInfo);
                    }
                } // if (osVer.Major >= 6)
                //
                // 운영체제가 Windows XP, Windows Server 2003 일 경우
                //
                else if (settingChecked)
                {
                    command = Application.ExecutablePath;
                }

                //
                // 시작 프로그램 목록에 프로그램을 추가하거나 제거한다.
                //
                if (settingChecked) WindowsUtility.AddStartupProgram(appName, command);
                else WindowsUtility.RemoveStartupProgram(appName);

                AgentSettings.Default.AutoRun = settingChecked;
            } // try
            catch (Exception exc)
            {
                string message = String.Format(
                    "ConfigUtility.ApplyAutoRunSetting() - {0} : {1}\n{2}", appName, command, exc);

                if (m_logger.IsErrorEnabled) m_logger.Error(message);
                if (m_tracing.Enabled) Trace.WriteLine("[EFAM.Error] " + message);
            } // catch
        }

        /// <summary>
        /// "자동 로그인" 설정값을 적용한다.
        /// </summary>
        /// <param name="settingChecked">설정이 선택되었으면 true, 그렇지 않으면 false</param>
        /// <param name="userId">사용자 ID</param>
        /// <param name="password">비밀번호</param>
        public static void ApplyAutoLoginSetting(bool settingChecked, string userId, string password)
        {
            AgentSettings appSettings = AgentSettings.Default;

            if (settingChecked)
            {
                appSettings.UserId = userId;
                appSettings.Password = password;
            }
            else appSettings.Password = "";

            appSettings.AutoLogin = settingChecked;
        }

        #endregion
    }
}
