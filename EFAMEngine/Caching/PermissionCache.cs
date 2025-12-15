#region 변경 이력
/*
 * Author : Link mskoo (2011. 4. 6)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-04-06   mskoo           최초 작성.
 * 
 * 2011-09-23   mskoo           5.0 버전 릴리즈. (변경 이력 정리)
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
// .NET용 개발 라이브러리
using Link.DLK.Caching;
// E-FAM 관련
using Link.EFAM.Common;

namespace Link.EFAM.Engine.Caching
{
    /// <summary>
    /// 액세스 권한 캐시를 구현한다.
    /// </summary>
    /// <remarks>
    /// 이 형식은 스레드로부터 안전하다.
    /// </remarks>
    public class PermissionCache : CacheBase
    {
        private const double TimerInterval = (600 * 1000);      // 타이머 이벤트의 시간 간격(초)

        private object m_syncObject = null;
        private Timer m_timer = null;

        #region 속성

        /// <summary>
        /// 지정한 키에 있는 캐시된 액세스 권한을 가져오거나 설정한다.
        /// </summary>
        /// <param name="path">캐시된 액세스 권한에 대한 키인 경로</param>
        /// <value>지정한 액세스 권한을 나타내는 <see cref="FileAccessRights"/> 개체</value>
        /// 
        /// <exception cref="ArgumentNullException">path 또는 value가 null인 경우</exception>
        public FileAccessRights this[string path]
        {
            get { return Get(path); }
            set { Add(path, value); }
        }

        #endregion

        #region 생성자

        /// <summary>
        /// <see cref="PermissionCache"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        private PermissionCache()
        {
            this.CacheDuration = new TimeSpan(1, 0, 0);

            m_syncObject = ((System.Collections.ICollection)this).SyncRoot;
            /*
            m_timer = new Timer(TimerInterval);
            m_timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
            m_timer.Start();
             */
        }

        #endregion

        #region 소멸자

        /// <summary>
        /// 타이머를 중지한다.
        /// </summary>
        ~PermissionCache()
        {
            //m_timer.Stop();
        }

        #endregion

        #region 메소드
        #region Singleton 인스턴스

        private static PermissionCache m_instance = null;

        /// <summary>
        /// 캐시된 <see cref="PermissionCache"/> 인스턴스를 반환한다.
        /// </summary>
        /// <returns>캐시된 <see cref="PermissionCache"/> 개체</returns>
        public static PermissionCache GetPermissionCache()
        {
            if (m_instance == null) m_instance = new PermissionCache();

            return m_instance;
        }

        #endregion

        /// <summary>
        /// 지정한 액세스 권한을 액세스 권한 캐시에 추가한다.
        /// </summary>
        /// <param name="path">액세스 권한을 참조하는데 사용되는 캐시 키인 경로</param>
        /// <param name="fileRights">
        /// 액세스 권한 캐시에 추가할 액세스 권한을 나타내는 <see cref="FileAccessRights"/> 개체
        /// </param>
        /// <remarks>
        /// 캐시 키가 path 매개 변수와 일치하는 기존 캐시된 액세스 권한을 덮어쓴다.
        /// </remarks>
        /// 
        /// <exception cref="ArgumentNullException">path 또는 fileRights가 null인 경우</exception>
        public void Add(string path, FileAccessRights fileRights)
        {
            CacheEntry cache = null;

            // 캐시를 생성한다.
            cache = new CacheEntry(path, fileRights.Clone());
            cache.ExpireDate = DateTime.Now.Add(this.CacheDuration);

            lock (m_syncObject)
            {
                this.BaseAdd(cache);
            } // lock
        }

        /// <summary>
        /// 액세스 권한 캐시에서 모든 액세스 권한을 제거한다.
        /// </summary>
        public void Clear()
        {
            lock (m_syncObject)
            {
                this.BaseClear();
            } // lock
        }

        /// <summary>
        /// 액세스 권한 캐시에서 지정한 액세스 권한을 검색한다.
        /// </summary>
        /// <param name="path">검색할 액세스 권한에 대한 캐시 키인 경로</param>
        /// <returns>
        /// 검색된 액세스 권한을 나타내는 <see cref="FileAccessRights"/> 개체.
        /// 캐시된 액세스 권한이 없으면 null
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException">path가 null인 경우</exception>
        public FileAccessRights Get(string path)
        {
            if (path == null) throw new ArgumentNullException("path");

            CacheEntry cache = null;
            FileAccessRights fileRights = null;

            //
            // 액세스 권한을 가져오고, 만료 날짜와 시간을 업데이트한다.
            //
            lock (m_syncObject)
            {
                cache = (CacheEntry)this.BaseGet(path);
            } // lock
            if (cache != null)
            {
                fileRights = cache.AccessRights.Clone();
                cache.ExpireDate = DateTime.Now.Add(this.CacheDuration);
            }

            return fileRights;
        }

        /// <summary>
        /// 액세스 권한 캐시에서 지정한 액세스 권한을 제거한다.
        /// </summary>
        /// <param name="path">제거할 액세스 권한에 대한 캐시 키인 경로</param>
        /// 
        /// <exception cref="ArgumentNullException">path가 null인 경우</exception>
        public void Remove(string path)
        {
            if (path == null) throw new ArgumentNullException("path");

            lock (m_syncObject)
            {
                this.BaseRemove(path);
            } // lock
        }

        #endregion

        #region 이벤트 핸들러

        /// <summary>
        /// 간격이 경과하면 발생할 때 필요한 작업을 진행한다.
        /// </summary>
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.ClearExpiredCaches();
        }

        #endregion
    }
}
