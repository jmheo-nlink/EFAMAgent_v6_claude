#region 변경 이력
/*
 * Author : Link mskoo (2013. 2. 13)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2013-02-13   mskoo           최초 작성.
 * 
 * 2013-05-20   mskoo           
 *                              - ReloadPolicyInternal()
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.Timers;

namespace Link.EFAM.Agent
{
    using Link.EFAM.Security;
    using Link.EFAM.Security.Principal;
    using Link.EFAM.Security.AccessControl;
    using Link.EFAM.Engine.Caching;
    using Link.EFAM.Engine.Services;
    using Link.EFAM.Agent.Interop.SWork;
    using Debug = System.Diagnostics.Debug;
    using SharedDrive = Link.EFAM.Core.SharedDrive;

    /// <summary>
    /// 지정된 간격으로 서버에서 보안 정책을 다시 로드한다.
    /// </summary>
    internal sealed class PolicyAutoReloader
    {
        private readonly static double Minute = 1000.0 * 60.0;

        private AgentStore m_store = AgentStore.Store;
        private Timer m_timer = null;

        #region 속성

        /// <summary>
        /// 보안 정책을 다시 로드할 간격을 가져오거나 설정한다.
        /// </summary>
        /// <value>다시 로드하는 사이의 시간(분). 기본값은 30분.</value>
        /// 
        /// <exception cref="ArgumentException">간격이 0 이하인 경우</exception>
        public int Interval
        {
            get
            {
                return (int)(m_timer.Interval / Minute);
            }
            set
            {
                m_timer.Interval = Minute * value;
            }
        }

        #endregion

        #region 생성자

        /// <summary>
        /// <see cref="PolicyAutoReloader"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        public PolicyAutoReloader()
            : this(null)
        {
        }

        /// <summary>
        /// <see cref="PolicyAutoReloader"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        /// <param name="control">
        /// 이벤트 처리기 호출을 마샬링하는데 사용되는 <see cref="System.Windows.Forms.Control"/>
        /// </param>
        public PolicyAutoReloader(System.Windows.Forms.Control control)
        {
            m_timer = new Timer();
            m_timer.AutoReset = true;
            m_timer.Interval = Minute * 30;
            m_timer.SynchronizingObject = control;
            m_timer.Elapsed += Timer_Elapsed;
        }

        #endregion

        #region 정적 메소드

        /// <summary>
        /// 서버에서 보안 정책을 다시 로드한다(새로 고친다).
        /// </summary>
        /// <remarks>
        /// 서버에서 보안 정책을 가져온 후 다시 적용한다.<br/>
        /// 보안 정책에 변경된 내용이 있으면 액세스 권한 캐시를 초기화한다.
        /// </remarks>
        public static void ReloadPolicy()
        {
            new PolicyAutoReloader().ReloadPolicyInternal();
        }

        #endregion

        #region 메소드

        /// <summary>
        /// 타이밍을 시작한다.
        /// </summary>
        /// <remarks>
        /// 시간 간격이 경과될 때마다 보안 정책이 다시 로드되는 것을 시작한다.
        /// </remarks>
        public void Start()
        {
            m_timer.Start();
        }

        /// <summary>
        /// 타이밍을 중지한다.
        /// </summary>
        /// <remarks>
        /// 보안 정책이 다시 로드되는 것을 중지한다.
        /// </remarks>
        public void Stop()
        {
            m_timer.Stop();
        }

        /// <summary>
        /// 서버에서 보안 정책을 다시 로드한다(새로 고친다).
        /// </summary>
        /// <remarks>
        /// 서버에서 보안 정책을 가져온 후 다시 적용한다.<br/>
        /// 보안 정책에 변경된 내용이 있으면 액세스 권한 캐시를 초기화한다.
        /// </remarks>
        private void ReloadPolicyInternal()
        {
            if (!m_store.IsAuthenticated) return;

            EFAMService service = new EFAMService(m_store.UserCredential);
            AccessControlPolicy policy = null;
            bool applied = false;

            // 서버에서 보안 정책을 가져와서 적용한다.
            policy = service.GetSecurityPolicy();

#if INTEROP_SWORK_HHISB // ========================================================================
            if (!SWorkHelper.IsInstalled)
            {
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;
                DirectorySecurity security = null;
                Account account = new Account(m_store.UserCredential.UserId);
                string shareName = null;

                //
                // 액세스가 거부된 공유 드라이브에 대한 ACL 권한을 제거한다.
                foreach (string path in policy.Directories)
                {
                    foreach (SharedDrive drive in m_store.DeniedSharedDrives)
                    {
                        if (comparer.Equals(path, drive.ShareName))
                        {
                            policy.GetAccessControl(path).RemoveAccessRuleAll();
                            break;
                        }
                        else if (path.Length > drive.ShareName.Length)
                        {
                            shareName = drive.ShareName.EndsWith("\\") 
                                      ? drive.ShareName : (drive.ShareName + "\\");

                            if (path.StartsWith(shareName, StringComparison.OrdinalIgnoreCase))
                            {
                                policy.GetAccessControl(path).RemoveAccessRuleAll();
                                break;
                            }
                        } // else if (...)
                    } // foreach ( SharedDrive )
                } // foreach ( string )

                foreach (SharedDrive drive in m_store.DeniedSharedDrives)
                {
                    security = new DirectorySecurity();
                    security.SetAccessRule(
                        new FileSystemAccessRule(account, (FileSystemRights)0, true, false));

                    policy.SetAccessControl(drive.ShareName, security);
                }
            } // if (!SWorkHelper.IsInstalled)
#endif  // INTEROP_SWORK_HHISB ====================================================================

            applied = AccessManager.GetManager().ApplyPolicy(policy, m_store.HostNameCollection);
            if (applied)
            {
                // 액세스 권한 캐시를 비운다.
                PermissionCache.GetPermissionCache().Clear();
            }
            // 서버에서 고정 폴더 목록을 가져와서 적용한다.
            //AccessManager.GetManager().SetFixedDirectories(service.GetFixedFolders());
            AccessManager.GetManager().FixedDirectories = service.GetFixedFolders();
        }

        public static string AppendBackslash(string path)
        {
            if (path == null) throw new ArgumentNullException("path");

            if (path.Length > 0 && path[path.Length - 1] != System.IO.Path.DirectorySeparatorChar)
            {
                path += System.IO.Path.DirectorySeparatorChar;
            }

            return path;
        }


        #endregion

        #region 이벤트 핸들러

        /// <summary>
        /// 시간 간격이 경과될 때마다 필요한 작업을 진행한다.
        /// </summary>
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Debug.WriteLine("[EFAM.Debug] Reload policy. " + e.SignalTime.ToLocalTime());

            try
            {
                ReloadPolicyInternal();
            }
            catch (Exception exc)
            {
                Debug.WriteLine("[EFAM.Debug] Reload policy. " + exc);
            }
        }

        #endregion
    }
}
