#region 변경 이력
/*
 * Author : Link mskoo (2011. 6. 14)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-06-14   mskoo           최초 작성.
 * 
 * 2011-09-23   mskoo           5.0 버전 릴리즈. (변경 이력 정리)
 * 
 * 2011-11-25   mskoo           메소드 추가.
 *                              - ParseCommandLineArguments(ReadOnlyCollection<string>)
 *                              
 * 2011-11-25   mskoo           업데이터 응용 프로그램을 실행하는 로직을 추가.
 *                              - OnInitialize(ReadOnlyCollection<string>)
 *                              
 * 2012-09-27   mskoo           네트워크 드라이브들을 다시 연결하는 '/remount' 명령줄 인수를 추가.
 * 
 * 2013-01-27   mskoo           SoftCamp S-Work와 연동하기 위한 코드를 추가.
 *                              - ParseCommandLineArguments(ReadOnlyCollection<string>)
 *                              - OnStartup(StartupEventArgs)
 *                              - OnStartupNextInstance(StartupNextInstanceEventArgs)
 *                              
 * 2018-08-29   DJJUNG          지원하는 OS 여부를 확인하기 위한 코드를 추가
 *                              - OnStartup(StartupEventArgs)
 *                              
 * 2021-11-05   DJJUNG          지원하는 OS 여부를 확인하기 위한 코드를 추가 (Windows 11)
 *                              - OnStartup(StartupEventArgs)
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Win32;
// "Krypton Toolkit" 라이브러리
using ComponentFactory.Krypton.Toolkit;
// .NET용 개발 라이브러리
//using Link.DLK.Net;

namespace Link.EFAM.Agent
{
    using Link.EFAM.Core;
    using Link.EFAM.Engine;
    using Link.EFAM.Agent.Configuration;
    using Link.EFAM.Agent.UI;
    using Link.EFAM.Agent.Properties;
#if (INTEROP_SWORK || INTEROP_SWORK_HHISB)  // ====================================================
    using Link.EFAM.Agent.Interop.SWork;
#endif  // INTEROP_SWORK || INTEROP_SWORK_HHISB ===================================================
    using Application = System.Windows.Forms.Application;
    using Path = System.IO.Path;

    /// <summary>
    /// 단일 인스턴스 형태의 E-FAM Agent 응용 프로그램과 관련된 속성, 메소드 및 이벤트를 제공한다.
    /// </summary>
    class AgentApplication : WindowsFormsApplicationBase
    {
        //
        // 추적/로그
        //
        private static BooleanSwitch m_tracing = new BooleanSwitch("traceSwitch", "Agent Module");

        private IAccessControlBehavior m_behavior = null;
        internal AgentCommands startupCommands = null;

        #region 속성
        #region Singleton 인스턴스

        private static AgentApplication m_instance = null;

        /// <summary>
        /// 캐시된 <see cref="AgentApplication"/> 인스턴스를 반환한다.
        /// </summary>
        /// <value>캐시된 <see cref="AgentApplication"/> 개체</value>
        public static AgentApplication Instance
        {
            get
            {
                if (m_instance == null) m_instance = new AgentApplication();

                return m_instance;
            } // get
        }

        #endregion
        #endregion

        #region 생성자

        /// <summary>
        /// <see cref="AgentApplication"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        private AgentApplication()
        {
            this.IsSingleInstance = true;
            this.ShutdownStyle = ShutdownMode.AfterMainFormCloses;
        }

        #endregion

        #region 메소드

        /// <summary>
        /// 액세스 제어에 연결할 동작을 가져온다.
        /// </summary>
        /// <returns>액세스 제어에 연결할 <see cref="IAccessControlBehavior"/> 개체</returns>
        public IAccessControlBehavior GetAccessControlBehavior()
        {
            Assembly asmbly = null;

            try
            {
                string path = Path.Combine(Application.StartupPath, "EFAM.dll");
                string typeName = "Link.EFAM.Agent.Custom.AccessControlBehavior";

                asmbly = Assembly.LoadFile(path);
                m_behavior = (IAccessControlBehavior)asmbly.CreateInstance(typeName);
            }
            catch (Exception)
            {
                m_behavior = new AccessControlBehavior();
            }

            return m_behavior;
        }

        #endregion

        #region WindowsFormsApplicationBase 멤버

        /// <summary>
        /// 응용 프로그램에서 Windows 인증을 사용하는 경우 기본 응용 프로그램 스레드의 비주얼 스타일, 텍스트 표시 스타일 및 
        /// 현재 보안 주체를 설정하고 시작 화면이 정의되어 있는 경우 이 시작 화면을 초기화한다.
        /// </summary>
        /// <param name="commandLineArgs">
        /// 현재 응용 프로그램의 문자열을 명령줄 인수로 포함하는 <see cref="String"/>의
        /// <see cref="System.Collections.ObjectModel.ReadOnlyCollection"/>
        /// </param>
        /// <returns>응용 프로그램 시작을 계속해야 하는지 여부를 나타내는 <see cref="Boolean"/></returns>
        protected override bool OnInitialize(System.Collections.ObjectModel.ReadOnlyCollection<string> commandLineArgs)
        {
            this.startupCommands = AgentCommands.ParseCommandLineArguments(commandLineArgs);
            
            // 응용 프로그램을 종료한다.
            if (startupCommands.Exit) return false;
#if INTEROP_SWORK   // ============================================================================
            if (startupCommands.Remount ||
                startupCommands.Permissions ||
                startupCommands.Log ||
                startupCommands.RecycleBin ||
                startupCommands.Invalid) return false;
#else   // INTEROP_SWORK ==========================================================================
            // 업데이터 응용 프로그램을 실행시키고 응용 프로그램을 종료한다.
            //if (!m_haveUpdateCheckedArg)
            if (!startupCommands.UpdateChecked)
            {
                string updaterApp = Settings.Default.EFAMUpdater;

                try
                {
                    if (!String.IsNullOrEmpty(updaterApp))
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo();

                        startInfo.WorkingDirectory
                            = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                        startInfo.FileName = updaterApp;

                        Process.Start(startInfo);
                        return false;
                    } // if (!String.IsNullOrEmpty(updaterApp))
                } // try
                catch (Exception exc)
                {
                    if (m_tracing.Enabled)
                    {
                        Trace.WriteLine("[EFAM.Error] AgentApplication.OnInitialize()\n" + exc);
                    }
                }
            } // if (!startupCommands.UpdateChecked)
#endif  // INTEROP_SWORK ==========================================================================

            return base.OnInitialize(commandLineArgs);
        }

        /// <summary>
        /// 응용 프로그램이 시작될 때 코드가 실행될 수 있도록 한다.
        /// </summary>
        /// <param name="eventArgs">이벤트 데이터가 포함된 <see cref="StartupEventArgs"/> 개체</param>
        /// <returns>응용 프로그램을 계속 시작해야 하는지 여부를 나타내는 <see cref="Boolean"/></returns>
        protected override bool OnStartup(StartupEventArgs eventArgs)
        {
            // 지원하는 OS 여부 체크
            {
                Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion"); // HKEY_LOCAL_MACHINE
                string productName = registryKey.GetValue("ProductName").ToString();

                Version efamVersion = new Version(Assembly.GetExecutingAssembly().GetName().Version.ToString());
                Version compareVersionForWindows10 = new Version("5.2.0.0");
                Version compareVersionForWindows11 = new Version("6.1.0.0");

                // Windows 7, 8, 8.1, 10, 11 아닐 때
                if (
                    !productName.Contains("Windows 7") 
                    && !productName.Contains("Windows 8") 
                    && !productName.Contains("Windows 8.1") 
                    && !productName.Contains("Windows 10") 
                    //&& !productName.Contains("Windows 11")
                    )
                {
                    KryptonMessageBox.Show("설치 된 E-FAM 버전은 현재 OS를 지원하지 않습니다.\n\n" + "- E-FAM Version : " + efamVersion.ToString() + "\n" + "- OS Version : " + productName,
                        Resources.MsgBox_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return false;
                }
                // Windows 10이고 E-FAM 버전이 5.2.0.0 보다 낮을 때 (Agent 5.2.0.0 버전 부터 Windows 10 지원)
                else if (productName.Contains("Windows 10") && (efamVersion < compareVersionForWindows10))
                {
                    KryptonMessageBox.Show("설치 된 E-FAM 버전은 현재 OS를 지원하지 않습니다.\n\n" + "- E-FAM Version : " + efamVersion.ToString() + "\n" + "- OS Version : " + productName,
                        Resources.MsgBox_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return false;
                }
                //// Windows 11이고 E-FAM 버전이 6.1.0.0 보다 낮을 때 (Agent 6.1.0.0 버전 부터 지원 Windows 11 지원)
                //else if (productName.Contains("Windows 11") && (efamVersion < compareVersionForWindows11))
                //{
                //    KryptonMessageBox.Show("설치 된 E-FAM 버전은 현재 OS를 지원하지 않습니다.\n\n" + "- E-FAM Version : " + efamVersion.ToString() + "\n" + "- OS Version : " + productName,
                //        Resources.MsgBox_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                //    return false;
                //}
            }

            Settings appSettings = Settings.Default;

            // log4net 환경 설정을 로드한다.
            log4net.Config.XmlConfigurator.Configure();

            //
            // 최신 설치를 반영하기 위해 응용 프로그램 설정을 업데이트한다.
            //
            if (appSettings.IsFirstRun)
            {
                appSettings.Upgrade();
                appSettings.IsFirstRun = false;

                /* ================================================================================
                 * 오류가 수정된 버전을 적용하기 위해 필요한 코드 
                 * (업데이터 프로그램을 업데이트하기 전까지 필요한 임시 코드)
                 */
                // 5.0.8 업데이트 적용 부분
                if (appSettings.AutoRun)
                {
                    Configuration.ConfigHelper.ApplyAutoRunSetting(false);
                    Configuration.ConfigHelper.ApplyAutoRunSetting(true);
                }
                // 5.0.9 업데이트 적용 부분 (HHI 플랜트사업부인 경우)
                if (String.Equals(appSettings.ServerUrl, "http://pnd.hhi.co.kr/EFAMServer",
                                  StringComparison.OrdinalIgnoreCase))
                {
                    appSettings.NetworkDrives = null;

                    // 로컬 그룹 정책을 변경한다.
                    // "숨겨진 thumbs.db 파일에서 미리 보기 캐싱 해제" 설정을 활성화한다.
                    string keyName = @"Software\Policies\Microsoft\Windows\Explorer";
                    RegistryKey regKey = null;

                    regKey = Registry.CurrentUser.OpenSubKey(keyName, true);
                    if (regKey == null)
                    {
                        regKey = Registry.CurrentUser.CreateSubKey(keyName);
                    }
                    regKey.SetValue("DisableThumbsDBOnNetworkFolders", 1, RegistryValueKind.DWord);
                } // appSettings.ServerUrl == "http://pnd.hhi.co.kr/EFAMServer"
                /* ============================================================================= */

                appSettings.Save();
            } // if (appSettings.IsFirstRun)

#if INTEROP_SWORK   // ============================================================================
            if (startupCommands.NoIntegration) return base.OnStartup(eventArgs);

            if (!SWorkHelper.IsInstalled)
            {
                if (m_tracing.Enabled) Trace.WriteLine("[EFAM.Warning] " + Resources.SCSWork_NotInstalled);
                return false;
            }
            if (!SWorkHelper.IsLoggedIn)
            {
                if (m_tracing.Enabled) Trace.WriteLine("[EFAM.Warning] " + Resources.SCSWork_NotLoggedIn);
                return false;
            }
            
            if (startupCommands.UserId.Length == 0)
            {
                startupCommands.UserId = SWorkHelper.GetLoggedInUserId();
            }
#elif INTEROP_SWORK_HHISB   // ====================================================================
            if (SWorkHelper.IsInstalled && !SWorkHelper.IsLoggedIn)
            {
                KryptonMessageBox.Show(Resources.SCSWork_NotLoggedIn, Resources.MsgBox_Caption,
                                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
#endif  // INTEROP_SWORK_HHISB ====================================================================

            return base.OnStartup(eventArgs);
        }

        /// <summary>
        /// 디자이너에서 시작 화면과 기본 폼을 구성하는 코드를 내보낼 수 있도록 한다.
        /// </summary>
        protected override void OnCreateMainForm()
        {
            this.MainForm = new MainForm();
        }

        /// <summary>
        /// 단일 인스턴스 응용 프로그램의 후속 인스턴스가 시작될 때 코드가 실행될 수 있도록 한다.
        /// </summary>
        /// <param name="eventArgs">이벤트 데이터가 포함된 <see cref="StartupNextInstanceEventArgs"/> 개체</param>
        protected override void OnStartupNextInstance(StartupNextInstanceEventArgs eventArgs)
        {
            base.OnStartupNextInstance(eventArgs);

            AgentCommands commands = AgentCommands.ParseCommandLineArguments(eventArgs.CommandLine);

            if (commands.Exit)
            {
                ((MainForm)this.MainForm).mnuExit_Click(null, EventArgs.Empty);
            }
#if (INTEROP_SWORK || INTEROP_SWORK_HHISB)  // ====================================================
            else if (commands.Remount && AgentStore.Store.IsAuthenticated)
            {
                SharedDriveHelper.DisconnectSharedDrives(AgentStore.Store.SharedDrives);
                SharedDriveHelper.ConnectSharedDrives(AgentStore.Store.SharedDrives);
            }
#if INTEROP_SWORK   // ============================================================================
            #region v5.1.7 업데이트
            else if (commands.Permissions)
            {
                ((MainForm)this.MainForm).mnuViewPerms_Click(null, EventArgs.Empty);
            }
            else if (commands.Log)
            {
                ((MainForm)this.MainForm).mnuViewLog_Click(null, EventArgs.Empty);
            }
            else if (commands.RecycleBin)
            {
                ((MainForm)this.MainForm).mnuRecycleBin_Click(null, EventArgs.Empty);
            }
            else if (commands.Preferences)
            {
                ((MainForm)this.MainForm).mnuConfig_Click(null, EventArgs.Empty);
            }
            #endregion
#endif  // INTEROP_SWORK ==========================================================================
#endif  // INTEROP_SWORK || INTEROP_SWORK_HHISB ===================================================
            else
            {
                ((MainForm)this.MainForm).mnuLogin_Click(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 응용 프로그램에서 처리되지 않은 예외가 발생할 때 코드가 실행될 수 있도록 한다.
        /// </summary>
        /// <param name="e">
        /// 이벤트 데이터가 포함된 <see cref="Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs"/> 개체
        /// </param>
        /// <returns>
        /// <see cref="WindowsFormsApplicationBase.UnhandledException"/> 이벤트가 발생했는지 여부를 나타내는 <see cref="Boolean"/>
        /// </returns>
        protected override bool OnUnhandledException(Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs e)
        {
            ((MainForm)this.MainForm).mnuLogout_Click(null, EventArgs.Empty);

            return base.OnUnhandledException(e);
        }

        #endregion
    }

    /// <summary>
    /// 명령줄 인수로 전달된 명령어와 옵션을 포함하고 있다.
    /// </summary>
    internal class AgentCommands
    {
        public bool UpdateChecked = false;
        public bool Exit = false;
#if (INTEROP_SWORK || INTEROP_SWORK_HHISB)  // ====================================================
        public bool Remount = false;
#if INTEROP_SWORK // ==============================================================================
        #region v5.1.7 업데이트
        public bool NoIntegration = false;
        public bool Permissions = false;
        public bool Log = false;
        public bool RecycleBin = false;
        public bool Preferences = false;
        public bool Invalid = false;
        public string UserId = String.Empty;
        #endregion
#endif  // INTEROP_SWORK ==========================================================================
#endif  // INTEROP_SWORK || INTEROP_SWORK_HHISB ===================================================

        #region 정적 메소드

        /// <summary>
        /// 명령줄 인수에 포함된 명령어와 옵션을 추출한다.
        /// </summary>
        /// <param name="commandLineArgs">
        /// 현재 응용 프로그램의 문자열을 명령줄 인수로 포함하는 <see cref="String"/>의
        /// <see cref="System.Collections.ObjectModel.ReadOnlyCollection"/>
        /// </param>
        /// <returns>명령어와 옵션이 포함된 <see cref="AgentCommands"/> 개체</returns>
        internal static AgentCommands ParseCommandLineArguments(System.Collections.ObjectModel.ReadOnlyCollection<string> commandLineArgs)
        {
            Debug.Assert(commandLineArgs != null, "commandLineArgs cannot be null.");

            const string argumentUserId = "/userid:";
            AgentCommands commands = new AgentCommands();

            foreach (string commandArg in commandLineArgs)
            {
                switch (commandArg.ToLower())
                {
                    case "/updatechecked":
                        commands.UpdateChecked = true;
                        continue;

                    case "/exit":
                        commands.Exit = true;
                        break;

#if (INTEROP_SWORK || INTEROP_SWORK_HHISB)  // ====================================================
                    case "/remount":
                        commands.Remount = true;
                        break;

#if INTEROP_SWORK   // ============================================================================
                    #region v5.1.7 업데이트
                    case "/permissions":
                        commands.Permissions = true;
                        break;

                    case "/log":
                        commands.Log = true;
                        break;

                    case "/recyclebin":
                        commands.RecycleBin = true;
                        break;

                    case "/pref":
                    case "/preferences":
                        commands.Preferences = true;
                        break;

                    case "/noint":
                    case "/nointegration":
                        commands.NoIntegration = true;
                        break;

                    default:
                        if (commandArg.StartsWith(argumentUserId, StringComparison.OrdinalIgnoreCase))
                        {
                            if (commandArg.Length > argumentUserId.Length)
                            {
                                commands.UserId = commandArg.Substring(argumentUserId.Length);
                            }
                        } // if (commandArg.StartsWith(argumentUserId, StringComparison.OrdinalIgnoreCase))
                        else
                        {
                            commands.Invalid = true;
                        }
                        break;
                    #endregion
#endif  // INTEROP_SWORK ==========================================================================
#endif  // INTEROP_SWORK || INTEROP_SWORK_HHISB ===================================================
                } // switch (commandArg.ToLower())

                break;
            } // foreach ( string )

            return commands;
        }

        #endregion
    }
}
