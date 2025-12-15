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
// .NET용 개발 라이브러리
using Link.DLK.Caching;
// E-FAM 관련
using Link.EFAM.Common;

namespace Link.EFAM.Engine.Caching
{
    /// <summary>
    /// 액세스 권한 캐시의 엔트리를 나타낸다. 
    /// </summary>
    internal class CacheEntry : ICacheEntry
    {
        private string m_path = null;
        private FileAccessRights m_fileRights = null;
        private DateTime m_date;

        #region 속성

        /// <summary>
        /// 캐시 엔트리에 포함된 액세스 권한을 가져온다.
        /// </summary>
        /// <value>캐시 엔트리에 포함된 액세스 권한을 나타내는 <see cref="FileAccessRights"/> 개체</value>
        public FileAccessRights AccessRights
        {
            get { return m_fileRights; }
        }

        #endregion

        #region 생성자

        /// <summary>
        /// 지정한 캐시 키와 액세스 권한을 사용하여 <see cref="CacheEntry"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        /// <param name="path">액세스 권한을 참조하는데 사용되는 캐시 키인 경로</param>
        /// <param name="fileRights">
        /// 캐시 엔트리에 포함할 액세스 권한을 나타내는 <see cref="FileAccessRights"/> 개체
        /// </param>
        /// 
        /// <exception cref="ArgumentNullException">path 또는 fileRights가 null인 경우</exception>
        public CacheEntry(string path, FileAccessRights fileRights)
        {
            if (path == null) throw new ArgumentNullException("path");
            if (fileRights == null) throw new ArgumentNullException("fileRights");

            m_path = path;
            m_fileRights = fileRights;
        }

        /// <summary>
        /// 지정한 캐시 키와 액세스 권한 및 캐시가 만료되는 날짜와 시간을 사용하여 
        /// <see cref="CacheEntry"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        /// <param name="path">액세스 권한을 참조하는데 사용되는 캐시 키인 경로</param>
        /// <param name="fileRights">
        /// 캐시 엔트리에 포함할 액세스 권한을 나타내는 <see cref="FileAccessRights"/> 개체
        /// </param>
        /// <param name="expireDate">캐시가 만료되는 <see cref="DateTime"/> 값</param>
        /// 
        /// <exception cref="ArgumentNullException">path 또는 fileRights가 null인 경우</exception>
        public CacheEntry(string path, FileAccessRights fileRights, DateTime expireDate)
            : this(path, fileRights)
        {
            m_date = expireDate;
        }

        #endregion

        #region Object 멤버

        /// <summary>
        /// 현재 인스턴스를 나타내는 문자열을 반환한다.
        /// </summary>
        /// <returns>현재 인스턴스를 나타내는 문자열</returns>
        /// <remarks>
        /// <see cref="Key"/> 및 <see cref="AccessRights"/>, <see cref="ExpireDate"/> 속성 문자열의 조합이다.
        /// </remarks>
        public override string ToString()
        {
            string toStr = null;

            toStr = String.Format("[Until {0}] Path: {1}  {2}", 
                    m_date.ToString("hh:mm:ss"), m_path, m_fileRights);

            return toStr;
        }

        #endregion

        #region ICacheEntry 멤버

        /// <summary>
        /// 캐시가 만료되는 날짜와 시간을 가져오거나 설정한다.
        /// </summary>
        /// <value>캐시가 만료되는 <see cref="DateTime"/> 값</value>
        public DateTime ExpireDate
        {
            get { return m_date; }
            set { m_date = value; }
        }

        /// <summary>
        /// 액세스 권한을 참조하는데 사용되는 캐시 키인 경로를 가져온다.
        /// </summary>
        /// <value>액세스 권한을 참조하는데 사용되는 캐시 키인 경로</value>
        public string Key
        {
            get { return m_path; }
        }

        #endregion
    }
}
