#region 변경 이력
/*
 * Author : Link mskoo (2012. . )
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2012--   mskoo           최초 작성.
 * 
 * 2012--   mskoo           5. 버전 릴리즈. (변경 이력 정리)
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
#if (!NET20 && !NET30)
using System.Linq;
#endif

using Link.Core.IO;
using Link.EFAM.Security.AccessControl;

namespace Link.EFAM.Security
{
    /// <summary>
    /// <see cref="AccessManager"/>와 연결된 액세스 제어 정책을 나타낸다.
    /// </summary>
    public sealed class AccessControlPolicy
    {
        private Dictionary<string, DirectorySecurity> m_data = null;

        #region 속성

        /// <summary>
        /// ACL(액세스 제어 목록) 항목이 적용된 디렉토리들을 가져온다.
        /// </summary>
        /// <value>ACL(액세스 제어 목록) 항목이 적용된 디렉토리의 경로 배열</value>
        public string[] Directories
        {
            get
            {
                string[] paths = null;

#if (NET20 || NET30)
                paths = new string[m_data.Count];
                m_data.Keys.CopyTo(paths, 0);
#else // NET35, NET40
                paths = m_data.Keys.ToArray<string>();
#endif
                return paths;
            } // get
        }

        #endregion

        #region 생성자

        /// <summary>
        /// <see cref="AccessControlPolicy"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        public AccessControlPolicy()
        {
            m_data = new Dictionary<string, DirectorySecurity>(StringComparer.OrdinalIgnoreCase);
        }

        #endregion

        #region 메소드

        /// <summary>
        /// ACL(액세스 제어 목록) 항목을 지정한 디렉토리에 적용한다.
        /// </summary>
        /// <param name="path">ACL(액세스 제어 목록) 항목을 추가하거나 제거할 디렉토리의 경로</param>
        /// <param name="security">
        /// 디렉토리에 적용할 ACL(액세스 제어 목록) 항목을 설명하는 <see cref="DirectorySecurity"/> 개체
        /// </param>
        /// <remarks>
        /// <paramref name="security"/>가 <b>null</b>이면 디렉토리에 적용된 ACL(액세스 제어 목록) 항목을 제거한다.
        /// </remarks>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="path"/>가 <b>null</b>인 경우</exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/>가 빈 문자열("")인 경우<br/>
        /// - 또는 -<br/>
        /// <paramref name="path"/>에 공백만 포함된 경우<br/>
        /// - 또는 -<br/>
        /// <paramref name="path"/>에 하나 이상의 잘못된 문자가 포함된 경우
        /// </exception>
        public void SetAccessControl(string path, DirectorySecurity security)
        {
            if (path == null) throw new ArgumentNullException("path");

            path = NtPath.NormalizePath(path);

            if (security != null) m_data[path] = security;
            else m_data.Remove(path);
        }

        /// <summary>
        /// 지정한 디렉토리의 ACL(액세스 제어 목록) 항목을 가져온다.
        /// </summary>
        /// <param name="path">ACL(액세스 제어 목록) 항목을 가져올 디렉토리의 경로</param>
        /// <returns>
        /// 디렉토리의 액세스 제어 규칙을 캡슐화하는 <see cref="DirectorySecurity"/> 개체.<br/>
        /// 디렉토리에 적용된 ACL(액세스 제어 목록) 항목이 없으면 <b>null</b>이 반환된다.
        /// </returns>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="path"/>가 <b>null</b>인 경우</exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/>가 빈 문자열("")인 경우<br/>
        /// - 또는 -<br/>
        /// <paramref name="path"/>에 공백만 포함된 경우<br/>
        /// - 또는 -<br/>
        /// <paramref name="path"/>에 하나 이상의 잘못된 문자가 포함된 경우
        /// </exception>
        public DirectorySecurity GetAccessControl(string path)
        {
            if (path == null) throw new ArgumentNullException("path");

            DirectorySecurity security = null;

            path = NtPath.NormalizePath(path);
            m_data.TryGetValue(path, out security);

            return security;
        }

        #endregion
    }
}
