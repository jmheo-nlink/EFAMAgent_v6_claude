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
 * 2012-02-11   mskoo           도메인에 가입된 컴퓨터이면 일반 설정 항목들을 설정할 수 없도록 수정.
 *                              - SaveCurrentSettings()
 *                              - MainForm_Load(object, EventArgs)
 *                              
 * 2012-02-12   mskoo           변경된 메소드를 사용하여 네트워크 드라이브 설정을 응용 프로그램 설정에 저장하도록 수정.
 *                              - SaveCurrentSettings()
 *                              
 * 2012-02-18   mskoo           도메인에 가입된 컴퓨터이면 '로그아웃' 메뉴 및 '종료' 메뉴 이벤트가 발생하지 않도록 수정.
 *                              - RebuildContextMenu()
 *                              - mnuLogout_Click(object, EventArgs)
 *                              - mnuExit_Click(object, EventArgs)
 *                              
 * 2012-02-18   mskoo           도메인에 가입된 컴퓨터이면 "윈도우 시작 시 자동 실행"을 적용하도록 수정.
 *                              - MainForm_Load(object, EventArgs)
 *                              
 * 2012-02-26   mskoo           응용 프로그램 설정을 래핑한 AgentSettings 클래스를 사용하여 설정 속성을 액세스하도록 수정.
 *                              - SaveCurrentSettings()
 *                              - ChangePassword()
 *                              - bgWorker_DoWork(object, DoWorkEventArgs)
 *                              - MainForm_Load(object, EventArgs)
 *                              - btnLogin_Click(object, EventArgs)
 *                              - mnuLogout_Click(object, EventArgs)
 *                              - mnuManagePerms_Click(object, EventArgs)
 *                              
 * 2012-02-26   mskoo           "HideOnDomainComputer" 설정이 활성화되어 있는 경우 폼을 숨기도록 수정.
 *                              - MainForm()
 *                              
 * 2012-03-30   mskoo           도메인에 가입된 컴퓨터이면 "윈도우 시작 시 자동 실행"을 해제하고 다시 적용하도록 수정.
 *                              - MainForm_Load(object, EventArgs)
 *                              
 * 2013-01-27   mskoo           SoftCamp S-Work와 연동하기 위한 코드를 추가.
 *                              - MainForm()
 *                              - SaveCurrentSettings()
 *                              - MainForm_Load(object, EventArgs)
 *                              - mnuLogout_Click(object, EventArgs)
 *                              - mnuExit_Click(object, EventArgs)
 *                              
 * 2013-05-22   mskoo           응용 프로그램 설정을 저장하도록 수정.
 *                              - ChangePassword()
 *                              
 * 2013-05-29   mskoo           연결 드라이브의 개수가 초과되지 않아도 경고 메시지가 표시되던 오류를 수정.
 *                              - bgWorker_RunWorkerCompleted(object, RunWorkerCompletedEventArgs)
 *                              
 * 2019-09-25   DJJUNG          로그인 시 ID와 Password가 같을 때 Password 변경 창 띄움 (S-Work 제외, 조선사업부 제외)
 *                              - bgWorker_RunWorkerCompleted(object, RunWorkerCompletedEventArgs)
 *                              
 * 2020-10-07   DJJUNG          로그인 성공 시 알림 메시지 띄우기 (현대중공업 특수선)
 *                              - bgWorker_RunWorkerCompleted(object, RunWorkerCompletedEventArgs)
 *                              
 * 2021-11-05   DJJUNG          로고 이미지 변경 코드 추가 (국방과학연구소)
 *                              - MainForm()
 *                              
 * 2021-11-05   DJJUNG          자동 로그아웃 기능 추가 (국방과학연구소)
 *                              - bgWorker_RunWorkerCompleted(object, RunWorkerCompletedEventArgs)
 *                              - timerAutoLogout_Elapsed(object, System.Timers.ElapsedEventArgs)
 *                              - mnuLogout_Click(object, EventArgs)
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
// log4net 라이브러리
using log4net;
// "Krypton Toolkit" 라이브러리
using ComponentFactory.Krypton.Toolkit;

namespace Link.EFAM.Agent.UI
{
    using Link.EFAM.Core;
    using Link.EFAM.Engine;
    using Link.EFAM.Engine.Caching;
    using Link.EFAM.Engine.Services;
    using Link.EFAM.Agent.Properties;
    using Link.EFAM.Agent.Configuration;
    using System.Net;
    using System.IO;
    using Link.EFAM.Agent.Helpers;
#if (INTEROP_SWORK || INTEROP_SWORK_HHISB)  // ====================================================
    using Link.EFAM.Agent.Interop.SWork;
#endif  // INTEROP_SWORK || INTEROP_SWORK_HHISB ===================================================

    public partial class MainForm : KryptonForm
    {
        //
        // 추적/로그
        //
        private static BooleanSwitch m_tracing = new BooleanSwitch("traceSwitch", "Agent Module");
        private ILog m_logger = LogManager.GetLogger(typeof(MainForm));

        private AboutBox m_aboutBox = null;
        private ViewPermissionsDialog m_dlgViewPerms = null;
        private ViewLogDialog m_dlgViewLog = null;
        private RecycleBinDialog m_dlgRecycleBin = null;
        private FileSearchDialog m_dlgFileSearch = null;
        private ConfigDialog m_dlgConfig = null;
        private AgentCommands m_startupCommands = null;

#if ADD
        private static System.Timers.Timer timerAutoLogout = new System.Timers.Timer();
        private static string idleTime = "0";
#endif

        #region 속성

        /// <summary>
        /// 입력 컨트롤들이 사용자 상호 작용에 응답할 수 있는지 여부를 나타내는 값을 설정한다.
        /// </summary>
        /// <value>입력 컨트롤들이 사용자 상호 작용에 응답할 수 있으면 true, 그렇지 않으면 false</value>
        private bool EnableInputControls
        {
            set
            {
#if !INTEROP_SWORK  // ============================================================================
                if (!AgentStore.Store.IsInSameDomainWithNAS)
                {
                    txtUserId.Enabled = value;
                    txtPassword.Enabled = value;
                }
#endif  // !INTEROP_SWORK =========================================================================
                chkAutoRun.Enabled = value;
                chkAutoLogin.Enabled = value;
                btnLogin.Enabled = value;
                trayCtxMenu.Enabled = value;
            } // set
        }

        #endregion

        #region 생성자

        /// <summary>
        /// <see cref="MainForm"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            SetLocalizedText();

#if ADD
            this.picLogo.Location = new Point(7, 7);

            // Set the size of the PictureBox control.
            this.picLogo.Size = new System.Drawing.Size(363, 106);

            // Set the SizeMode to center the image.
            this.picLogo.SizeMode = PictureBoxSizeMode.Zoom;

            // Set the image property.
            this.picLogo.Image = Properties.Resources.Login_Logo_ADD;
#else
            this.picLogo.Location = new Point(7, 7);

            // Set the size of the PictureBox control.
            this.picLogo.Size = new System.Drawing.Size(363, 106);

            // Set the SizeMode to center the image.
            this.picLogo.SizeMode = PictureBoxSizeMode.CenterImage;

            // Set the image property.
            this.picLogo.Image = Properties.Resources.Login_Logo;
#endif

            m_aboutBox = new AboutBox();
            m_startupCommands = AgentApplication.Instance.startupCommands;

            mnuHelpDesk.Visible = false;

            // 아이콘을 설정한다.
            this.Icon = Resources.EFAM_Icon;
            trayIcon.Icon = Resources.Tray_Icon;
            chkAutoLogin.Visible = true;
#if INTEROP_SWORK   // ============================================================================
            trayIcon.Visible = m_startupCommands.NoIntegration;
#else   // INTEROP_SWORK ==========================================================================
            if (AgentStore.Store.HideFormInSameDomainWithNAS)
            {
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                trayIcon.Visible = false;
            }
#endif  // INTEROP_SWORK ==========================================================================

            AgentStore.Store.PolicyReloader = new PolicyAutoReloader(this);
        }

        #endregion 생성자

        #region 소멸자

        /// <summary>
        /// 사용중인 리소스를 해제한다.
        /// </summary>
        ~MainForm()
        {
            if (m_aboutBox != null) m_aboutBox.Dispose();
        }

        #endregion

        #region 메소드

        /// <summary>
        /// 각 컨트롤에 지역화된 텍스트를 설정한다.
        /// </summary>
        private void SetLocalizedText()
        {
            //
            // 폼
            //
            this.Text = Resources.MainForm_Text;
            lblUserId.Text = Resources.Label_UserId;
            lblPassword.Text = Resources.Label_Password;
            btnLogin.Text = Resources.Button_Login;
            chkAutoRun.Text = Resources.Check_AutoRun;
            chkAutoLogin.Text = Resources.Check_AutoLogin;
            //
            // 컨텍스트 메뉴
            //
            mnuLogin.Text = Resources.Menu_Login;
            mnuLogout.Text = Resources.Menu_Logout;
            mnuChangePassword.Text = Resources.Menu_ChangePassword;
            mnuManagePerms.Text = Resources.Menu_ManagePermissions;
            mnuViewPerms.Text = Resources.Menu_ViewPermissions;
            mnuViewLog.Text = Resources.Menu_ViewLog;
            mnuRecycleBin.Text = Resources.Menu_RecycleBin;
            mnuConfig.Text = Resources.Menu_Config;
            mnuHelpDesk.Text = Resources.Menu_HelpDesk;
            mnuAbout.Text = Resources.Menu_About;
            mnuExit.Text = Resources.Menu_Exit;
            trayIcon.Text = Resources.NotifyText_LoggedOut;
        }

        /// <summary>
        /// 컨텍스트 메뉴를 재구성한다.
        /// </summary>
        private void RebuildContextMenu()
        {
            Credential credential = AgentStore.Store.UserCredential;
            bool isInSameDomain = AgentStore.Store.IsInSameDomainWithNAS;
            bool authed = (credential != null && credential.IsAuthenticated);

            mnuLogin.Visible = !authed;
            mnuLogout.Visible = isInSameDomain ? false : authed;
            mnuSeparator1.Visible = isInSameDomain ? !authed : true;
#if INTEROP_SWORK   // ============================================================================
            mnuChangePassword.Visible = false;
#else   // INTEROP_SWORK ==========================================================================
            mnuChangePassword.Visible = isInSameDomain ? false : authed;
#endif  // INTEROP_SWORK ==========================================================================
            mnuManagePerms.Visible = (authed && credential != null && credential.IsManager);
            mnuViewPerms.Visible = authed;
            mnuViewLog.Visible = authed;
            mnuRecycleBin.Visible = (authed && credential != null && credential.UseRecycleBin);
            mnuFileSearch.Visible = authed;
            mnuSeparator2.Visible = authed;
            mnuSeparator4.Visible = !isInSameDomain;
            mnuExit.Visible = !isInSameDomain;

            trayIcon.Text = authed ? Resources.NotifyText_LoggedIn : Resources.NotifyText_LoggedOut;
        }

        /// <summary>
        /// 트레이(Tray) 아이콘에 알림 메시지를 표시한다.
        /// </summary>
        /// <param name="authed">사용자가 인증되었으면 true, 그렇지 않으면 false</param>
        private void ShowBalloonTip(bool authed)
        {
            trayIcon.BalloonTipTitle = authed ? Resources.NotifyTipTitle_LoggedIn 
                                              : Resources.NotifyTipTitle_LoggedOut;
            trayIcon.BalloonTipText = authed ? Resources.NotifyTip_LoggedIn
                                             : Resources.NotifyTip_LoggedOut;
            trayIcon.ShowBalloonTip(1000);
        }

        /// <summary>
        /// 현재 선택된 설정값들을 응용 프로그램 설정에 저장한다.
        /// </summary>
        private void SaveCurrentSettings()
        {
            AgentSettings appSettings = AgentSettings.Default;
            List<NetworkDriveSetting> settingList = new List<NetworkDriveSetting>();
            NetworkDriveSettingSet settingSet 
                = new NetworkDriveSettingSet(appSettings.NetworkDriveSettings);
            NetworkDriveSetting netwkdrvSetting = null;

            foreach (SharedDrive drive in AgentStore.Store.SharedDrives)
            {
                if (drive.DriveName.Length == 0) continue;

                netwkdrvSetting = settingSet[drive.ShareName];

                if (netwkdrvSetting == null)
                {
                    netwkdrvSetting = new NetworkDriveSetting();
                    netwkdrvSetting.UseDrive = true;
                }
                netwkdrvSetting.DriveName = (drive.DriveName == "*") ? "Z:" : drive.DriveName;
                netwkdrvSetting.SharedFolder = drive.ShareName;

                settingList.Add(netwkdrvSetting);
            } // foreach ( SharedDrive )

#if INTEROP_SWORK   // ============================================================================
            appSettings.UserId = null;
#else   // INTEROP_SWORK ==========================================================================
            if (!AgentStore.Store.IsInSameDomainWithNAS)
            {
                if (chkAutoRun.Checked != appSettings.AutoRun)
                {
                    ConfigHelper.ApplyAutoRunSetting(chkAutoRun.Checked);
                }
                ConfigHelper.ApplyAutoLoginSetting(
                    chkAutoLogin.Checked, txtUserId.Text, txtPassword.Text);
            } // if (!AgentStore.Store.IsInSameDomainWithNAS)
            appSettings.UserId = txtUserId.Text;
#endif  // INTEROP_SWORK ==========================================================================
            appSettings.NetworkDriveSettings = settingList;

            // 설정값들을 저장한다.
            appSettings.Save();
        }

        /// <summary>
        /// 비밀번호를 변경할 수 있는 대화 상자를 표시하고, 사용자의 비밀번호를 변경한다.
        /// </summary>
        /// <returns>비밀번호가 변경되었으면 true, 그렇지 않으면 false</returns>
        private bool ChangePassword()
        {
            ChangePasswordDialog dialog = new ChangePasswordDialog();
            bool changed = false;

            changed = dialog.ShowDialog(this);
            dialog.Dispose();

            if (changed)
            {
                txtPassword.Text = AgentStore.Store.InputedPassword;

                // 변경된 비밀번호를 응용 프로그램 설정에 저장한다.
                if (AgentSettings.Default.AutoLogin)
                {
                    AgentSettings.Default.Password = txtPassword.Text;
                    AgentSettings.Default.Save();
                }
            } // if (changed)

            return changed;
        }

        #endregion

        #region 이벤트 핸들러
        #region 백그라운드 작업

        /// <summary>
        /// 백그라운드 작업이 시작될 때 다른 스레드에서 필요한 작업을 진행한다.
        /// </summary>
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            AgentStore store = AgentStore.Store;
            string domainName = store.IsInSameDomainWithNAS ? Environment.UserDomainName : null;

            //
            // 서버에 로그인한다.
            // 로그인된 사용자의 자격 증명을 데이터 저장소에 설정한다.
            //
            store.UserCredential = AuthenticationService.Login(AgentSettings.Default.ServerUrl, 
                                        domainName, store.InputedUserId, store.InputedPassword);
            store.FileAccessControl = new FileAccessControl(store.UserCredential);
        }

        /// <summary>
        /// 백그라운드 작업자가 완료(성공, 실패 또는 취소)되었을 때 필요한 작업을 진행한다.
        /// </summary>
        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            AgentStore store = AgentStore.Store;
            LoaderDialog dlgLoader = new LoaderDialog();

            //
            // 오류가 발생했을 경우
            //
            if (e.Error != null)
            {
                Exception exception = e.Error;
                string message = null;

#if INTEROP_SWORK   // ============================================================================
                message = "MainForm.bgWorker_DoWork() - " + store.InputedUserId + "\n" + exception;

                if (m_logger.IsErrorEnabled) m_logger.Error(message);
                if (m_tracing.Enabled) Trace.WriteLine("[EFAM.Error] " + message);

                // 응용 프로그램을 종료한다.
                Application.Exit();
#else   // INTEROP_SWORK ==========================================================================
                if (exception is LoginErrorException)
                {
                    LoginErrorCode errorCode = ((LoginErrorException)exception).ErrorCode;

                    if (errorCode == LoginErrorCode.PasswordExpired)
                    {
                        this.UseWaitCursor = false;

                        KryptonMessageBox.Show(Resources.Error_PasswordExpired, Resources.MsgBox_Caption,
                                               MessageBoxButtons.OK, MessageBoxIcon.Error);

                        if (ChangePassword())
                        {
                            btnLogin_Click(btnLogin, EventArgs.Empty);
                            return;
                        }
                    } // if (errorCode == LoginErrorCode.PasswordExpired)
                    else message = exception.Message;
                    exception = null;
                } // else if (LoginErrorException)
                else if (exception is UriFormatException)
                {
                    message = Resources.Error_InvalidUrlFormat;
                    exception = null;
                }
                else if (exception is System.Net.WebException)
                {
                    message = Resources.Error_WebExceptionThrowed;
                }
                else if (exception is System.Web.Services.Protocols.SoapException)
                {
                    message = Resources.Error_SoapExceptionThrowed;
                }
                else message = Resources.Error_ExceptionThrowed;

                this.UseWaitCursor = false;
                this.EnableInputControls = true;

                //
                // 오류 로그를 기록하고 추적 메시지를 쓴다.
                if (exception != null)
                {
                    string logMessage = "MainForm.bgWorker_DoWork() - " + store.InputedUserId + "\n" + exception;

                    if (m_logger.IsErrorEnabled) m_logger.Error(logMessage);
                    if (m_tracing.Enabled) Trace.WriteLine("[EFAM.Error] " + logMessage);
                } // if (exception != null)

                if (message != null)
                {
                    KryptonMessageBox.Show(message, Resources.MsgBox_Caption,
                                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
#endif  // INTEROP_SWORK ==========================================================================
            } // if (e.Error != null)
            //
            // 백그라운드 작업이 완료되었을 경우
            //
            else if (!e.Cancelled)
            {
                this.Hide();

                //
                // E-FAM을 로드한다.
                dlgLoader.SetAsLoader();
                dlgLoader.ShowDialog(this);

                this.UseWaitCursor = false;
                this.EnableInputControls = true;
            } // else if (!e.Cancelled)

            if (dlgLoader.IsFatalError)
            {
                mnuExit_Click(null, EventArgs.Empty);
            }
            else if (dlgLoader.HasSucceeded)
            {
                tmrKeepNetwkConn.Start();

                RebuildContextMenu();
                ShowBalloonTip(true);

                SaveCurrentSettings();

                //
                Random rand = new Random();

                tmrStartAutoReloader.Interval = 1000 * 30 * rand.Next(1, 30);
                tmrStartAutoReloader.Start();

#if ADD
                //
                try
                {
                    string serverUrl = Settings.Default.ServerUrl ?? ""; // Get Server URL
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serverUrl + "/Home/GetAutoLogoutTime");
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    idleTime = new StreamReader(response.GetResponseStream()).ReadToEnd();
                }
                catch (Exception ex) // Error 발생 시 Continue 되어짐
                {
                    idleTime = "0";
                }

                if (idleTime != "0" && idleTime != "")
                {
                    timerAutoLogout.AutoReset = true;
                    timerAutoLogout.Interval = 1000 * 30; // 30초
                    timerAutoLogout.Elapsed += timerAutoLogout_Elapsed;
                    timerAutoLogout.Start();
                }
                //
#endif

                // 연결할 수 있는 공유 드라이브의 개수를 초과한 경우
                if (dlgLoader.IsExceededDriveCount)
                {
                    ShowConfigDialog(1);
                }

#if (!INTEROP_SWORK && !INTEROP_SWORK_HHISB) // S-Work 아니고 조선사업부도 아닐 때
                dlgLoader.Dispose();

                if (store.UserCredential.IsAuthenticated) // 사용자가 인증 되었을 때
                {
                    string id = store.InputedUserId;
                    string password = store.InputedPassword;

                    if (id.ToUpper() == password.ToUpper())
                    {
                        ChangePassword();
                    }

#if HHISPECIAL // 현대중공업 특수선
                    AlertDialog alertDialog = new AlertDialog();
                    alertDialog.ShowDialog(this);
                    alertDialog.Dispose();
#endif // HHISPECIAL
                }
#endif // !INTEROP_SWORK && !INTEROP_SWORK_HHISB
            } // if (loadSuccess)
            else
            {
                store.UserCredential = null;
                store.FileAccessControl = null;
                //chkAutoLogin.Checked = false;
                txtPassword.Text = "";

                this.Show();

                dlgLoader.Dispose();
            }

            //dlgLoader.Dispose();
        }

#if ADD
        private void timerAutoLogout_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (GetLastUserInput.GetIdleTickCount() >= Convert.ToInt32(idleTime))
            {
                mnuLogout_Click(null, EventArgs.Empty);
            }
        }
#endif
        
        #endregion

        /// <summary>
        /// 폼을 로드할 때 필요한 작업을 진행한다.
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            AgentSettings appSettings = AgentSettings.Default;
            bool isInSameDomain = AgentStore.Store.IsInSameDomainWithNAS;

            RebuildContextMenu();

            
#if HHI_SPECIAL_SHIP   // ============================================================================            
            chkAutoLogin.Visible = false;
            chkAutoLogin.Checked = false;
#endif


#if INTEROP_SWORK   // ============================================================================
            txtUserId.Enabled = m_startupCommands.NoIntegration;
            txtPassword.Enabled = false;
            chkAutoRun.Visible = false;
            chkAutoLogin.Visible = false;

            chkAutoLogin.Checked = !m_startupCommands.NoIntegration;
            //chkAutoLogin.Checked = true;

            // S-Work 사용자 ID와 비밀번호를 설정한다.
            txtUserId.Text = m_startupCommands.UserId; //appSettings.UserId;
            txtPassword.Text = "";
            //appSettings.UserId = null;

            // 자동으로 로그인한다.
            if (m_startupCommands.Preferences)
            //if (AgentApplication.Instance.m_havePrefsArg)
            {
                // 백그라운드 작업의 실행을 시작한다.
                this.WindowState = FormWindowState.Minimized;
                bgCmdWorker.RunWorkerAsync();
                //mnuConfig.PerformClick();
            }
            else if (!m_startupCommands.NoIntegration)
            {
                btnLogin.PerformClick();
            }
            //else btnLogin.PerformClick();
#else   // ========================================================================================
            if (isInSameDomain)
            {
                txtUserId.Enabled = false;
                txtPassword.Enabled = false;
                chkAutoRun.Visible = false;
                chkAutoLogin.Visible = false;

#if INTEROP_SWORK_HHISB // ========================================================================
                if (SWorkHelper.IsInstalled)
                {
                    ConfigHelper.ApplyAutoRunSetting(false);
                }
                else
                {
                    ConfigHelper.ApplyAutoRunSetting(false);
                    ConfigHelper.ApplyAutoRunSetting(true);
                }
#else   // INTEROP_SWORK_HHISB ====================================================================
                ConfigHelper.ApplyAutoRunSetting(false);
                ConfigHelper.ApplyAutoRunSetting(true);
#endif  // INTEROP_SWORK_HHISB ====================================================================
            } // if (isInSameDomain)
            else
            {
                chkAutoRun.Checked = appSettings.AutoRun;
                chkAutoLogin.Checked = appSettings.AutoLogin;
            }

            // 응용 프로그램 설정에 저장된 사용자 ID와 비밀번호를 설정한다.
            txtUserId.Text = isInSameDomain ? Environment.UserName : appSettings.UserId;
            txtPassword.Text = (isInSameDomain || !chkAutoLogin.Checked) ? "" : appSettings.Password;

            // 자동으로 로그인한다.
            if (isInSameDomain || chkAutoLogin.Checked) btnLogin.PerformClick();
#endif  // INTEROP_SWORK ==========================================================================
        }

        /// <summary>
        /// 폼을 닫을 때마다 또는 폼이 닫히기 전에 필요한 작업을 진행한다.
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;

                if (!bgWorker.IsBusy)
                {
                    //chkAutoLogin.Checked = false;
                    this.Hide();
                }
            } // if (e.CloseReason == CloseReason.UserClosing)
            else if (!AgentStore.Store.IsInSameDomainWithNAS)
            {
                mnuLogout.PerformClick();
            }
        }

        #region 버튼 이벤트

        /// <summary>
        /// "Login" 버튼을 클릭할 때 필요한 작업을 진행한다.
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            txtUserId.Text = txtUserId.Text.Trim();

            if (txtUserId.Text.Length == 0)
            {
                KryptonMessageBox.Show(Resources.Warn_InputUserId, Resources.MsgBox_Caption,
                                       MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtUserId.Focus();
                return;
            }
            if (AgentSettings.Default.ServerUrl.Length == 0)
            {
                mnuConfig.PerformClick();
                return;
            }

            ///////////////////////////////////////////////////////////////////////////////////////
            // 백그라운드 작업으로 서버에 로그인한다.
            //
            this.UseWaitCursor = true;
            this.EnableInputControls = false;
            
            //
            // 입력한 사용자 ID와 비밀번호를 데이터 저장소에 설정한다.
            //
            AgentStore.Store.InputedUserId = txtUserId.Text;
            AgentStore.Store.InputedPassword = txtPassword.Text;

            // 백그라운드 작업의 실행을 시작한다.
            bgWorker.RunWorkerAsync();
        }

        #endregion

        #region 컨텍스트 메뉴 이벤트

        /// <summary>
        /// "Login" 메뉴를 클릭할 때 필요한 작업을 진행한다.
        /// </summary>
        internal void mnuLogin_Click(object sender, EventArgs e)
        {
            if (AgentStore.Store.IsAuthenticated) return;

            if (!this.Visible) this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();

            //
            // 자동으로 로그인한다.
            //
            if ((AgentStore.Store.IsInSameDomainWithNAS || chkAutoLogin.Checked)
                && txtUserId.Text.Trim().Length > 0)
            {
                btnLogin.PerformClick();
            }
        }

        /// <summary>
        /// "Logout" 메뉴를 클릭할 때 필요한 작업을 진행한다.
        /// </summary>
        internal void mnuLogout_Click(object sender, EventArgs e)
        {
#if ADD
            timerAutoLogout.Stop();
#endif

#if INTEROP_SWORK   // ============================================================================

#elif INTEROP_SWORK_HHISB   // ====================================================================
            if (!SWorkHelper.IsInstalled)
            {
                if (AgentStore.Store.IsInSameDomainWithNAS) return;
            }
#else   // INTEROP_SWORK_HHISB ====================================================================
            if (AgentStore.Store.IsInSameDomainWithNAS) return;
#endif  // INTEROP_SWORK ==========================================================================
            if (!AgentStore.Store.IsAuthenticated) return;

            this.UseWaitCursor = true;
            this.EnableInputControls = false;
            tmrKeepNetwkConn.Stop();

            // 열려있는 자식 폼을 모두 닫는다.
            foreach (Form childForm in this.OwnedForms)
            {
                childForm.Close();
            }

            //
            // E-FAM을 언로드한다.
            //
            LoaderDialog dialog = new LoaderDialog();

            dialog.SetAsUnloader();
            dialog.ShowDialog(this);
            dialog.Dispose();

            // 서버에서 로그아웃한다.
            AuthenticationService.Logout(AgentStore.Store.UserCredential);

            // 액세스 권한 캐시를 비운다.
            PermissionCache.GetPermissionCache().Clear();
            AgentStore.Store.PolicyReloader.Stop();
            // 데이터 저장소를 비운다.
            AgentStore.Store.Clear();

            this.UseWaitCursor = false;
            this.EnableInputControls = true;
            RebuildContextMenu();
            ShowBalloonTip(false);

            chkAutoRun.Checked = AgentSettings.Default.AutoRun;
            chkAutoLogin.Checked = AgentSettings.Default.AutoLogin;
            if (!chkAutoLogin.Checked) txtPassword.Text = "";
        }

        /// <summary>
        /// "ChangePassword" 메뉴를 클릭할 때 필요한 작업을 진행한다.
        /// </summary>
        private void mnuChangePassword_Click(object sender, EventArgs e)
        {
            mnuChangePassword.Enabled = false;

            if (AgentStore.Store.IsAuthenticated) ChangePassword();

            mnuChangePassword.Enabled = true;
        }

        /// <summary>
        /// "ManagePermissions" 메뉴를 클릭할 때 필요한 작업을 진행한다.
        /// </summary>
        private void mnuManagePerms_Click(object sender, EventArgs e)
        {
            if (!AgentStore.Store.IsAuthenticated) return;

            //
            // E-FAM 서버(웹)를 표시한다.
            //
            Process.Start(AgentSettings.Default.ServerUrl);
        }

        /// <summary>
        /// "ViewPermissions" 메뉴를 클릭할 때 필요한 작업을 진행한다.
        /// </summary>
        internal void mnuViewPerms_Click(object sender, EventArgs e)
        {
            if (!AgentStore.Store.IsAuthenticated) return;

            if (m_dlgViewPerms == null || m_dlgViewPerms.IsDisposed)
            {
                m_dlgViewPerms = new ViewPermissionsDialog();
                m_dlgViewPerms.Icon = Resources.EFAM_Icon;
                m_dlgViewPerms.ShowInTaskbar = true;
                m_dlgViewPerms.Show(this);
            }
            if (m_dlgViewPerms.CanFocus) m_dlgViewPerms.Activate();
        }

        /// <summary>
        /// "ViewLog" 메뉴를 클릭할 때 필요한 작업을 진행한다.
        /// </summary>
        internal void mnuViewLog_Click(object sender, EventArgs e)
        {
            if (!AgentStore.Store.IsAuthenticated) return;

            if (m_dlgViewLog == null || m_dlgViewLog.IsDisposed)
            {
                m_dlgViewLog = new ViewLogDialog();
                m_dlgViewLog.Icon = Resources.EFAM_Icon;
                m_dlgViewLog.ShowInTaskbar = true;
                m_dlgViewLog.Show(this);
            }
            if (m_dlgViewLog.CanFocus) m_dlgViewLog.Activate();
        }

        /// <summary>
        /// "RecycleBin" 메뉴를 클릭할 때 필요한 작업을 진행한다.
        /// </summary>
        internal void mnuRecycleBin_Click(object sender, EventArgs e)
        {
            if (!AgentStore.Store.IsAuthenticated) return;

            if (m_dlgRecycleBin == null || m_dlgRecycleBin.IsDisposed)
            {
                m_dlgRecycleBin = new RecycleBinDialog();
                m_dlgRecycleBin.Icon = Resources.EFAM_Icon;
                m_dlgRecycleBin.ShowInTaskbar = true;
                m_dlgRecycleBin.Show(this);
            }
            if (m_dlgRecycleBin.CanFocus) m_dlgRecycleBin.Activate();
        }

        /// <summary>
        /// "FileSearch" 메뉴를 클릭할 때 필요한 작업을 진행한다.
        /// </summary>
        internal void mnuFileSearch_Click(object sender, EventArgs e)
        {
            if (!AgentStore.Store.IsAuthenticated) return;

            if (m_dlgFileSearch == null || m_dlgFileSearch.IsDisposed)
            {
                m_dlgFileSearch = new FileSearchDialog();
                m_dlgFileSearch.Icon = Resources.EFAM_Icon;
                m_dlgFileSearch.ShowInTaskbar = true;
                m_dlgFileSearch.Show(this);
            }
            if (m_dlgFileSearch.CanFocus) m_dlgFileSearch.Activate();
        }

        /// <summary>
        /// "Config" 메뉴를 클릭할 때 필요한 작업을 진행한다.
        /// </summary>
        internal void mnuConfig_Click(object sender, EventArgs e)
        {
            ShowConfigDialog(0);
        }

        private void ShowConfigDialog(int tabIndex)
        {
            if (m_dlgConfig == null || m_dlgConfig.IsDisposed)
            {
                m_dlgConfig = new ConfigDialog();
                m_dlgConfig.StartupTabIndex = tabIndex;
                m_dlgConfig.Icon = Resources.EFAM_Icon;
                m_dlgConfig.StartPosition = FormStartPosition.CenterScreen;
                m_dlgConfig.ShowInTaskbar = true;
                m_dlgConfig.ShowDialog(this);
                m_dlgConfig.Dispose();
            }
            if (m_dlgConfig.CanFocus) m_dlgConfig.Activate();
        }

        /// <summary>
        /// "HelpDesk" 메뉴를 클릭할 때 필요한 작업을 진행한다.
        /// </summary>
        private void mnuHelpDesk_Click(object sender, EventArgs e)
        {
            //
            // "E-FAM" 사이트를 표시한다.
            //
            Process.Start("http://www.n-link.co.kr");
        }

        /// <summary>
        /// "About" 메뉴를 클릭할 때 필요한 작업을 진행한다.
        /// </summary>
        private void mnuAbout_Click(object sender, EventArgs e)
        {
            if (m_aboutBox.Visible) m_aboutBox.Activate();
            else m_aboutBox.ShowDialog(this);
        }

        /// <summary>
        /// "Exit" 메뉴를 클릭할 때 필요한 작업을 진행한다.
        /// </summary>
        internal void mnuExit_Click(object sender, EventArgs e)
        {
#if INTEROP_SWORK   // ============================================================================

#elif INTEROP_SWORK_HHISB   // ====================================================================
            if (!SWorkHelper.IsInstalled)
            {
                if (AgentStore.Store.IsInSameDomainWithNAS) return;
            }
#else   // INTEROP_SWORK_HHISB ====================================================================
            if (AgentStore.Store.IsInSameDomainWithNAS) return;
#endif  // INTEROP_SWORK ==========================================================================

            mnuLogout_Click(null, EventArgs.Empty);
            // 응용 프로그램을 종료한다.
            Application.Exit();
        }

        #endregion

        /// <summary>
        /// TextBox 컨트롤이 폼의 활성 컨트롤이 될 때 필요한 작업을 진행한다. 
        /// </summary>
        private void TextBox_Enter(object sender, EventArgs e)
        {
            ((KryptonTextBox)sender).SelectAll();
        }

        /// <summary>
        /// TextBox 컨트롤에 포커스가 있을 때 사용자가 키를 누르면 필요한 작업을 진행한다.
        /// </summary>
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;

                if (sender == txtUserId) txtPassword.Focus();
                else if (sender == txtPassword) btnLogin.PerformClick();
            }
        }

        /// <summary>
        /// 마우스로 트레이(Tray) 아이콘을 두 번 클릭할 때 필요한 작업을 진행한다.
        /// </summary>
        private void trayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            mnuLogin.PerformClick();
        }

        /// <summary>
        /// 지정한 시간 간격이 경과될 때마다 필요한 작업을 진행한다.
        /// </summary>
        private void tmrKeepNetwkConn_Tick(object sender, EventArgs e)
        {
            foreach (SharedDrive drive in AgentStore.Store.SharedDrives)
            {
                if (!drive.IsConnected) continue;

                System.IO.Directory.Exists(drive.ShareName);
            } // foreach ( NetworkDrive )
        }

        /// <summary>
        /// 지정한 시간 간격이 경과될 때마다 필요한 작업을 진행한다.
        /// </summary>
        private void tmrStartAutoReloader_Tick(object sender, EventArgs e)
        {
            tmrStartAutoReloader.Stop();

            AgentStore.Store.PolicyReloader.Start();
        }

        #endregion

        private void bgCmdWorker_DoWork(object sender, DoWorkEventArgs e)
        {
        }

        private void bgCmdWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
#if INTEROP_SWORK   // ============================================================================
            this.Hide();
            mnuConfig.PerformClick();
#endif  // INTEROP_SWORK ==========================================================================
        }

        
    }
}
