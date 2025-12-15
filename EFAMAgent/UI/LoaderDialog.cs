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
 * 2012-02-12   mskoo           사용하는 네트워크 드라이브만 연결하도록 코드를 수정.
 *                              - bgwkLoader_DoWork(object, DoWorkEventArgs)
 *                              
 * 2012-02-26   mskoo           응용 프로그램 설정을 래핑한 AgentSettings 클래스를 사용하여 설정 속성을 액세스하도록 수정.
 *                              - bgwkLoader_DoWork(object, DoWorkEventArgs)
 *                              
 * 2012-02-26   mskoo           "HideOnDomainComputer" 설정이 활성화되어 있는 경우 폼을 숨기도록 수정.
 *                              - LoaderDialog()
 *                              
 * 2013-05-14   mskoo           연결할 공유 드라이브의 개수가 초과되었는지 확인하는 코드를 추가.
 *                              - bgwkLoader_DoWork(object, DoWorkEventArgs)
 *                              - bgWorker_RunWorkerCompleted(object, RunWorkerCompletedEventArgs)
 *                              
 * 2013-05-29   mskoo           속성 추가.
 *                              - IsExceededDriveCount
 *                              
 * 2014-05-06   jake.9          서버에서 지정한 드라이브 이름을 사용하도록 수정.
 *                              - bgwkLoader_DoWork(object, DoWorkEventArgs)
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
// "Krypton Toolkit" 라이브러리
using ComponentFactory.Krypton.Toolkit;
using Link.Core;
using Link.Core.IO;
using Link.Core.Net;
// log4net 라이브러리
using log4net;

namespace Link.EFAM.Agent.UI
{
    using Link.EFAM.Agent.Configuration;
    using Link.EFAM.Agent.Interop.SWork;
    using Link.EFAM.Core;
    using Link.EFAM.Engine;
    using Link.EFAM.Engine.Services;
    using Link.EFAM.Security;
    using Link.EFAM.Security.AccessControl;
    using Link.EFAM.Security.Principal;
    using Resources = Link.EFAM.Agent.Properties.Resources;

    public partial class LoaderDialog : KryptonForm
    {
        //
        // 추적/로그
        //
        private static BooleanSwitch m_tracing = new BooleanSwitch("traceSwitch", "Agent Module");
        private ILog m_logger = LogManager.GetLogger(typeof(LoaderDialog));

        private BackgroundWorker m_bgWorker = null;
        private ProgressStep m_step = ProgressStep.None;
        private bool m_succeeded = false;
        private bool m_exceededDriveCount = false;
        private bool m_isFatalError = false;

        #region 속성

        /// <summary>
        /// E-FAM을 성공적으로 로드/언로드했는지 여부를 나타내는 값을 가져온다.
        /// </summary>
        /// <value>E-FAM을 성공적으로 로드/언로드했으면 true, 그렇지 않으면 false</value>
        public bool HasSucceeded
        {
            get { return m_succeeded; }
        }

        /// <summary>
        /// 연결할 수 있는 공유 드라이브의 개수가 초과되었는지 여부는 나타내는 값을 가져온다.
        /// </summary>
        /// <value>연결할 수 있는 공유 드라이브의 개수가 초과되었으면 <b>true</b>, 그렇지 않으면 <b>false</b></value>
        public bool IsExceededDriveCount
        {
            get { return m_exceededDriveCount; }
        }

        public bool IsFatalError
        {
            get { return m_isFatalError; }
        }

        #endregion

        #region 생성자

        /// <summary>
        /// <see cref="LoaderDialog"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        public LoaderDialog()
        {
            InitializeComponent();

            if (AgentStore.Store.HideFormInSameDomainWithNAS)
            //if (AgentStore.Store.IsDomainComputer 
            //    && AgentSettings.Default.NeedsHideOnDomainComputer)
            {
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
            }
        }

        #endregion

        #region 메소드

        /// <summary>
        /// E-FAM을 로드하는 "Loader"로 설정한다.
        /// </summary>
        public void SetAsLoader()
        {
            m_bgWorker = bgwkLoader;
        }

        /// <summary>
        /// E-FAM을 언로드하는 "Unloader"로 설정한다.
        /// </summary>
        public void SetAsUnloader()
        {
            m_bgWorker = bgwkUnloader;
        }

        /// <summary>
        /// 호스트 파일(hosts)에 있는 IP 주소와 호스트 이름들을 로드하여 반환한다.
        /// </summary>
        /// <returns>IP 주소와 호스트 이름의 컬렉션</returns>
        private NameValueCollection LoadHostsFile()
        {
            NameValueCollection hostColl = new NameValueCollection();
            string path = Environment.SystemDirectory + "\\drivers\\etc\\hosts";
            string[] texts = null;
            char[] separators = { ' ', '\t' };

            if (!File.Exists(path)) return hostColl;
            
            foreach (string line in File.ReadAllLines(path))
            {
                // 주석은 제외한다.
                if (line.TrimStart(separators).StartsWith("#")) continue;

                texts = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (texts.Length >= 2)
                {
                    // IP 주소와 호스트 이름을 컬렉션에 추가한다.
                    hostColl.Add(texts[0], texts[1]);
                }
            } // foreach ( string )

            return hostColl;
        }

        #endregion

        #region 이벤트 핸들러
        #region 백그라운드 작업

        /// <summary>
        /// 백그라운드 작업이 시작될 때 다른 스레드에서 필요한 작업을 진행한다.
        /// </summary>
        private void bgwkLoader_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgWorker = sender as BackgroundWorker;
            ProgressState state = new ProgressState();

            EFAMService service = new EFAMService(AgentStore.Store.UserCredential);
            FileAccessControl accCtrl = AgentStore.Store.FileAccessControl;
            List<SharedDrive> sharedDriveList = null;

            ///////////////////////////////////////////////////////////////////////////////////
            // 서버에서 필요한 정보(원격 경로, 네트워크 드라이브)들을 가져온다.
            //

            Credential credential = AgentStore.Store.UserCredential;
            Link.EFAM.Engine.InternalServices.ServerProfile agtProfile = null;
            List<string> shareNameList = new List<string>();
            string[] shareNames = null;

            //
            // 진행 상태를 표시한다.
            //
            state.StepValue = ProgressStep.GetData;
            state.StepTextValue = Resources.Step_GettingDataFromServer;
            bgWorker.ReportProgress(0, state);
            
            //
            // 서버에서 정보들을 가져온다.
            //
            agtProfile = service.GetAgentProfile();
            shareNames = service.GetNetworkShareNames();
            sharedDriveList = service.GetSharedDrives();

            //
            // 호스트 파일(hosts)을 이용하여 호스트 이름으로 된 네트워크 공유들을 
            // 액세스를 제어할 목록에 추가한다.
            //
            NameValueCollection hostNameColl = LoadHostsFile();
            string[] hostNames = null;
            string[] texts = null;
            char[] separators = { '\\' };

#if HHISB   // ====================================================================================
            /*
            \\10.100.77.11   >  \\NAS2
            \\10.100.77.23   >  \\NAS1
            \\10.100.77.51   >  \\NAS3
            \\10.100.77.53   >  \\NAS4
            */

            hostNameColl.Add("10.100.77.53", "NAS4");
            hostNameColl.Add("10.100.77.53", "F6240D");
            hostNameColl.Add("10.100.77.53", "AMDBD");

            hostNameColl.Add("10.100.77.51", "NAS3");
            hostNameColl.Add("10.100.77.51", "F6240C");
            hostNameColl.Add("10.100.77.51", "AMDBC");

            hostNameColl.Add("10.100.77.50", "F3240A");
            hostNameColl.Add("10.100.77.50", "F3240");
            hostNameColl.Add("10.100.77.50", "AMBACKUP");

            hostNameColl.Add("10.100.77.23", "NAS1");
            hostNameColl.Add("10.100.77.23", "F6240B");
            hostNameColl.Add("10.100.77.23", "F6030B6");
            hostNameColl.Add("10.100.77.23", "F6030B5");
            hostNameColl.Add("10.100.77.23", "F6030B4");
            hostNameColl.Add("10.100.77.23", "F6030B3");
            hostNameColl.Add("10.100.77.23", "F6030B2");
            hostNameColl.Add("10.100.77.23", "F6030B1");
            hostNameColl.Add("10.100.77.23", "F6030B");
            hostNameColl.Add("10.100.77.23", "AMDBB");

            hostNameColl.Add("10.100.77.16", "F6030A6");

            hostNameColl.Add("10.100.77.15", "F6030A5");

            hostNameColl.Add("10.100.77.14", "F6030A4");

            hostNameColl.Add("10.100.77.13", "F6030A3");

            hostNameColl.Add("10.100.77.12", "F6030A2");

            hostNameColl.Add("10.100.77.11", "NAS2");
            hostNameColl.Add("10.100.77.11", "F6240A");
            hostNameColl.Add("10.100.77.11", "F6030A1");
            hostNameColl.Add("10.100.77.11", "F6030A");
            hostNameColl.Add("10.100.77.11", "AMDBA");
#endif // HHISB ===================================================================================

            Dictionary<string, string> nbNameMapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            string nbName;
            string shareName = null;
            string ipAddr = null;
            string shareString = null;
            int foundIdx = -1;

            foreach (string value in shareNames)
            {
                shareName = NtPath.NormalizePath(value);

                // 네트워크 공유의 UNC 이름에서 IP 주소를 가져온다.
                texts = shareName.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                ipAddr = (texts.Length != 0) ? texts[0] : "";
                // 네트워크 공유의 UNC 이름에서 공유 이름을 가져온다.
                foundIdx = shareName.StartsWith("\\")
                         ? shareName.IndexOf('\\', 2) : shareName.IndexOf('\\');
                shareString = (foundIdx != -1) ? shareName.Substring(foundIdx) : "";

                shareNameList.Add("\\\\" + ipAddr + shareString);
                shareNameList.Add("\\\\" + ipAddr + "." + shareString);

                // IP 주소와 매핑된 호스트 이름들을 가져온다.
                hostNames = hostNameColl.GetValues(ipAddr);
                if (hostNames != null)
                {
                    // IP 주소를 호스트 이름으로 변경하여 목록에 추가한다.
                    foreach (string hostName in hostNames)
                    {
                        shareNameList.Add("\\\\" + hostName + shareString);
                        shareNameList.Add("\\\\" + hostName + "." + shareString);
                    }
                } // if (hostNames != null)

                //// IP 주소를 NetBIOS 이름으로 변경하여 목록에 추가한다.
                //try
                //{
                //    nbName = Dns.GetHostEntry(ipAddr).HostName;
                //    shareNameList.Add(@"\\" + nbName + shareString);
                //    shareNameList.Add(@"\\" + nbName + "." + shareString);

                //    nbNameMapping[@"\\" + nbName] = @"\\" + ipAddr;
                //} // tr
                //catch (Exception ex1)
                //{
                //    Debug.WriteLine("[EFAM.nbName] " + Dns.GetHostEntry(ipAddr).HostName);
                //    Debug.WriteLine("[EFAM.ErrorMessage] " + ex1.Message);
                //    Debug.WriteLine("[EFAM.ErrorInnerException] " + ex1.InnerException);

                //}                
                //
                /**
                try
                {
                    foreach (string ipAddr in DnsHelper.Lookup(ipString))
                    {
                        if (String.Equals(ipAddr, ipString, StringComparison.OrdinalIgnoreCase)) continue;

                        shareNameList.Add("\\\\" + ipAddr + shareString);
                        shareNameList.Add("\\\\" + ipAddr + "." + shareString);
                    }
                } // try
                catch { }
                 */
            } // foreach ( string )

            //AccessManager.GetManager().

#if DEBUG
            Debug.WriteLine("[EFAM.Agent] target share names:");
            foreach (string value in shareNameList)
            {
                Debug.WriteLine("[EFAM.Agent] " + value);
            }
#endif // DEBUG

            ///////////////////////////////////////////////////////////////////////////////////
            // 원격 파일 및 디렉토리에 대한 액세스 제어를 활성화한다.
            //

            //
            // 진행 상태를 표시한다.
            //
            state.StepValue = ProgressStep.LoadEFAM;
            state.StepTextValue = Resources.Step_LoadingEFAM;
            bgWorker.ReportProgress(0, state);

            // 액세스 제어 미니필터 드라이터를 로드한다.
            AgentUtility.UnloadFacFilter();
            AgentUtility.LoadFacFilter();

            accCtrl.Behavior = AgentApplication.Instance.GetAccessControlBehavior();
            accCtrl.Prepare();
            accCtrl.Activate(shareNameList);

            ///////////////////////////////////////////////////////////////////////////////////
            // 사용자에게 허용된 네트워크 드라이브들을 연결한다.
            //

            //
            // 진행 상태를 표시한다.
            //
            state.StepValue = ProgressStep.ConnectNetworkDrive;
            state.StepTextValue = Resources.Step_ConnectingNetworkDrive;
            bgWorker.ReportProgress(0, state);

            //
            // 응용 프로그램 설정에 설정된 드라이브 이름을 설정하고 연결한다.
            //
            NetworkDriveSettingSet settingSet
                = new NetworkDriveSettingSet(AgentSettings.Default.NetworkDriveSettings);
            NetworkDriveSetting netwkdrvSetting = null;
            List<string> reservedDriveNameList = new List<string>();
#if (INTEROP_SWORK || INTEROP_SWORK_HHISB)  // ====================================================
            List<string> usableDriveNameList = new List<string>(NtEnvironment.GetUsableLogicalDrives());
#endif  // INTEROP_SWORK || INTEROP_SWORK_HHISB ===================================================

            foreach (SharedDrive drive in sharedDriveList)
            {
                if (drive.IsFixedDriveName) reservedDriveNameList.Add(drive.DriveName);
            }

            foreach (SharedDrive drive in sharedDriveList)
            {
                netwkdrvSetting = settingSet[drive.ShareName];

                if (netwkdrvSetting != null)
                {
#if (INTEROP_SWORK || INTEROP_SWORK_HHISB)  // ====================================================
                    drive.Usable = drive.IsSecure ? true : netwkdrvSetting.UseDrive;
#else   // ========================================================================================
                    drive.Usable = netwkdrvSetting.UseDrive;
#endif  // INTEROP_SWORK || INTEROP_SWORK_HHISB ===================================================
                    if (drive.DriveName.Length != 0 && !drive.IsFixedDriveName)
                    {
                        drive.DriveName = reservedDriveNameList.Contains(netwkdrvSetting.DriveName) 
                                        ? "*" : netwkdrvSetting.DriveName;
                    }
                } // if (netwkdrvSetting != null)

#if (INTEROP_SWORK || INTEROP_SWORK_HHISB)  // ====================================================
                if (drive.Usable) usableDriveNameList.Remove(drive.DriveName);
#endif  // INTEROP_SWORK || INTEROP_SWORK_HHISB ===================================================
            } // foreach ( SharedDrive )

#if (INTEROP_SWORK || INTEROP_SWORK_HHISB)  // ====================================================
            // 보안 드라이브에 사용할 드라이브 이름을 지정한다.
            foreach (SharedDrive drive in sharedDriveList)
            {
                if (usableDriveNameList.Count == 0) continue;
                
                if (!drive.Usable || !drive.IsSecure) continue;
                if (drive.DriveName == "*")
                {
                    drive.DriveName = usableDriveNameList[0];
                    usableDriveNameList.RemoveAt(0);
                }
            } //  foreach ( SharedDrive )

#if INTEROP_SWORK_HHISB // ========================================================================
            if (SWorkHelper.IsInstalled)
#else   // INTEROP_SWORK_HHISB ====================================================================
            if (!AgentApplication.Instance.startupCommands.NoIntegration)
#endif  // INTEROP_SWORK_HHISB ====================================================================
            {
                state.StepValue = ProgressStep.SetSecureDriveInfo;
                state.StepTextValue = null;
                bgWorker.ReportProgress(0, state);

                // 연결된 네트워크 드라이브들을 S-Work 보안 드라이브로 설정한다.
                SWorkHelper.DeleteSecureDriveInfos(sharedDriveList);
                if (!SWorkHelper.SetSecureDriveInfos(sharedDriveList))
                {
                    e.Result = false;
                    return;
                }
                
                state.StepValue = ProgressStep.ConnectNetworkDrive;
                state.StepTextValue = null;
                bgWorker.ReportProgress(0, state);
            } // if (SWorkHelper.IsInstalled)
#if INTEROP_SWORK_HHISB // ========================================================================
            else
            {
                List<SharedDrive> deniedDriveList = new List<SharedDrive>();
                
                foreach (SharedDrive drive in sharedDriveList)
                {
                    if (drive.IsSecure) deniedDriveList.Add(drive);
                }
                
                if (deniedDriveList.Count != 0)
                {
                    foreach (SharedDrive drive in deniedDriveList)
                    {
                        sharedDriveList.Remove(drive);
                    }
                    
                    AgentStore.Store.DeniedSharedDrives = deniedDriveList;
                    
                    SharedDriveHelper.CancelExistingNetworkConnections(deniedDriveList);
                } // if (deniedDriveList.Count != 0)
            } // else
#endif  // INTEROP_SWORK_HHISB ====================================================================
#endif  // INTEROP_SWORK || INTEROP_SWORK_HHISB ===================================================

            SharedDriveHelper.CancelExistingNetworkConnections(sharedDriveList);
            e.Result = SharedDriveHelper.IsExceeded(sharedDriveList)
                     ? false : SharedDriveHelper.ConnectSharedDrives(sharedDriveList);

            //
            // IP 주소와 호스트 이름의 컬렉션과 네트워크 드라이브 컬렉션을 데이터 저장소에 설정한다.
            AgentStore.Store.HostNameCollection = hostNameColl;
            AgentStore.Store.SharedDrives = sharedDriveList;
            //
            AgentStore.Store.PolicyReloader.Interval = agtProfile.RefreshPerMinutes;

            PolicyAutoReloader.ReloadPolicy();
        }

        /// <summary>
        /// 백그라운드 작업이 시작될 때 다른 스레드에서 필요한 작업을 진행한다.
        /// </summary>
        private void bgwkUnloader_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgWorker = sender as BackgroundWorker;
            ProgressState state = new ProgressState();

            FileAccessControl accCtrl = AgentStore.Store.FileAccessControl;

            ///////////////////////////////////////////////////////////////////////////////////////
            // 연결된 네트워크 드라이브의 연결을 끊는다.
            //

            //
            // 진행 상태를 표시한다.
            //
            state.StepValue = ProgressStep.DisconnectNetworkDrive;
            state.StepTextValue = Resources.Step_DisconnectingNetworkDrive;
            bgWorker.ReportProgress(0, state);

            SharedDriveHelper.DisconnectSharedDrives(AgentStore.Store.SharedDrives);

#if (INTEROP_SWORK || INTEROP_SWORK_HHISB)  // ====================================================
#if INTEROP_SWORK_HHISB // ========================================================================
            if (SWorkHelper.IsInstalled)
#else   // INTEROP_SWORK_HHISB ====================================================================
            if (!AgentApplication.Instance.startupCommands.NoIntegration)
#endif  // INTEROP_SWORK_HHISB ====================================================================
            {
                // S-Work 보안 드라이브 정보를 삭제한다.
                SWorkHelper.DeleteSecureDriveInfos(AgentStore.Store.SharedDrives);
            }
#endif  // INTEROP_SWORK || INTEROP_SWORK_HHISB ===================================================

            ///////////////////////////////////////////////////////////////////////////////////////
            // 원격 파일 및 디렉토리에 대한 액세스 제어를 비활성화한다.
            //

            //
            // 진행 상태를 표시한다.
            //
            state.StepValue = ProgressStep.UnloadEFAM;
            state.StepTextValue = Resources.Step_UnloadingEFAM;
            bgWorker.ReportProgress(0, state);

            accCtrl.Deactivate();

            // 액세스 제어 미니필터 드라이터를 언로드한다.
            //AgentUtils.UnloadFacFilter();
        }

        /// <summary>
        /// 백그라운드 작업자 스레드에서 일부 진행되었음을 나타낼 때 필요한 작업을 진행한다.
        /// </summary>
        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressState state = e.UserState as ProgressState;

            //
            // 진행 상태를 표시한다.
            //
            if (state.StepValue != ProgressStep.None) m_step = state.StepValue;
            if (state.StepTextValue != null)
            {
                lblMessage.Text = String.Format(Resources.Info_InProgress, state.StepTextValue);
            }
        }

        /// <summary>
        /// 백그라운드 작업자가 완료(성공, 실패 또는 취소)되었을 때 필요한 작업을 진행한다.
        /// </summary>
        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //
            // 오류가 발생했을 경우
            //
            if (e.Error != null)
            {
                Exception exception = e.Error;
                string message = null;
                string methodName = 
                    (sender == bgwkLoader) ? "bgwkLoader_DoWork()" : "bgwkUnloader_DoWork()";

                if (exception is System.Net.WebException)
                {
                    message = Resources.Error_WebExceptionThrowed;
                }
                else if (exception is System.Web.Services.Protocols.SoapException)
                {
                    message = Resources.Error_SoapExceptionThrowed;
                }
                else
                {
                    switch (m_step)
                    {
                        case ProgressStep.GetData:
                            message = String.Format(Resources.Error_ExceptionThrowedInProgress,
                                                    Resources.Step_GettingDataFromServer);
                            break;

                        case ProgressStep.LoadEFAM:
                            message = String.Format(Resources.Error_ExceptionThrowedInProgress,
                                                    Resources.Step_LoadingEFAM);
                            break;

                        case ProgressStep.UnloadEFAM:
                            message = String.Format(Resources.Error_ExceptionThrowedInProgress,
                                                    Resources.Step_UnloadingEFAM);
                            break;

                        case ProgressStep.ConnectNetworkDrive:
                            message = String.Format(Resources.Error_ExceptionThrowedInProgress,
                                                    Resources.Step_ConnectingNetworkDrive);
                            break;

                        case ProgressStep.DisconnectNetworkDrive:
                            message = String.Format(Resources.Error_ExceptionThrowedInProgress,
                                                    Resources.Step_DisconnectingNetworkDrive);
                            break;

                        case ProgressStep.SetSecureDriveInfo:
                            message = Resources.SCSWork_FailedToSetSecureDriveInfo;
                            m_isFatalError = true;
                            break;
                    } // switch (m_step)
                } // else

                //
                // 오류 로그를 기록하고 추적 메시지를 쓴다.
                message = String.Format("LoaderDialog.{0} - Step: {1}\n{2}", methodName, m_step, exception);

                if (m_logger.IsErrorEnabled) m_logger.Error(message);
                if (m_tracing.Enabled) Trace.WriteLine("[EFAM.Error] " + message);

#if INTEROP_SWORK   // ============================================================================
                m_isFatalError = true;
#else   // INTEROP_SWORK ==========================================================================
                KryptonMessageBox.Show(message, Resources.MsgBox_Caption,
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif  // INTEROP_SWORK ==========================================================================
            } // if (e.Error != null)
            //
            // 백그라운드 작업이 완료되었을 경우
            //
            else if (!e.Cancelled)
            {
                m_succeeded = true;

                if (sender == bgwkLoader && !(bool)e.Result)
                {
                    if (m_step == ProgressStep.SetSecureDriveInfo)
                    {
                        m_isFatalError = true;
#if INTEROP_SWORK   // ============================================================================
                        if (m_tracing.Enabled) Trace.WriteLine("[EFAM.Error] " + Resources.SCSWork_FailedToSetSecureDriveInfo);
#else   // INTEROP_SWORK ==========================================================================
                        KryptonMessageBox.Show(Resources.SCSWork_FailedToSetSecureDriveInfo, Resources.MsgBox_Caption,
                                               MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif  // INTEROP_SWORK ==========================================================================
                    }
                    else
                    {
                        string message = Resources.Warn_SomeNetwkDrvIsNotConnected;

                        if (SharedDriveHelper.IsExceeded(AgentStore.Store.SharedDrives))
                        {
                            m_exceededDriveCount = true;
                            message = String.Format(Resources.Warn_TooManySharedDrives,
                                                    NtEnvironment.GetUsableLogicalDrives().Length);
                        }
                        
#if INTEROP_SWORK   // ============================================================================
                        m_isFatalError = true;
                        if (m_tracing.Enabled) Trace.WriteLine("[EFAM.Error] " + message);
#else   // INTEROP_SWORK ==========================================================================
                        KryptonMessageBox.Show(message, Resources.MsgBox_Caption);
#endif  // INTEROP_SWORK ==========================================================================
                    } // else
                } // if (sender == bgwkLoader && !(bool)e.Result)
            } // else if (!e.Cancelled)

            this.Close();
        }

        #endregion

        /// <summary>
        /// 폼을 로드할 때 필요한 작업을 진행한다.
        /// </summary>
        private void LoaderDialog_Load(object sender, EventArgs e)
        {
            m_succeeded = false;
            m_exceededDriveCount = false;
            m_isFatalError = false;

            // 백그라운드 작업의 실행을 시작한다.
            if (m_bgWorker != null) m_bgWorker.RunWorkerAsync();
        }

        /// <summary>
        /// 폼을 닫을 때마다 또는 폼이 닫히기 전에 필요한 작업을 진행한다.
        /// </summary>
        private void LoaderDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_bgWorker == null) return;
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = m_bgWorker.IsBusy;
            }
        }

        #endregion

        #region Form 멤버

        /// <summary>
        /// 폼을 현재 활성 창이 소유자로 설정된 모달 대화 상자로 표시합니다.
        /// </summary>
        /// <returns>E-FAM을 성공적으로 로드/언로드했으면 true, 그렇지 않으면 false</returns>
        public new bool ShowDialog()
        {
            base.ShowDialog();

            return m_succeeded;
        }

        /// <summary>
        /// 폼을 지정된 소유자가 있는 모달 대화 상자로 표시합니다.
        /// </summary>
        /// <param name="owner">
        /// 모달 대화 상자를 소유할 최상위 창을 나타내는 <see cref="IWin32Window"/>를 구현하는 개체
        /// </param>
        /// <returns>E-FAM을 성공적으로 로드/언로드했으면 true, 그렇지 않으면 false</returns>
        public new bool ShowDialog(IWin32Window owner)
        {
            base.ShowDialog(owner);

            return m_succeeded;
        }

        #endregion

        #region INNER 클래스

        /// <summary>
        /// 백그라운드 작업의 진행 상태를 나타낸다.
        /// </summary>
        private class ProgressState
        {
            public ProgressStep StepValue = ProgressStep.None;
            public string StepTextValue = String.Empty;
        }

        /// <summary>
        /// 백그라운드 작업의 진행 단계를 지정한다.
        /// </summary>
        private enum ProgressStep
        {
            None,
            GetData,
            LoadEFAM,
            UnloadEFAM,
            ConnectNetworkDrive,
            DisconnectNetworkDrive,
            SetSecureDriveInfo,
        }

        #endregion
    }
}
