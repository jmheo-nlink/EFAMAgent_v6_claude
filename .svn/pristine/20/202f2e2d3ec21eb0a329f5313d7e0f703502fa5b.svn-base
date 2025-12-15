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
using System.Collections.ObjectModel;

namespace Link.EFAM.Security.AccessControl
{
    using Link.EFAM.Security.Principal;
    using AppResource = Link.EFAM.AppResources.AppResource;

    /// <summary>
    /// 디렉토리에 대한 액세스 제어 및 파일 명명을 나타낸다.
    /// </summary>
    public sealed class DirectorySecurity
    {
        /*
         * 액세스 규칙은 사용자, 그룹 및 응용 프로그램 ID별로 1개씩만 유지한다.
         */

        private List<AuthorizationRule> m_authRuleList = null;
        private List<FileSystemAccessRule> m_accRuleList = null;
        private List<NamingRule> m_namingRuleList = null;

        #region 생성자

        /// <summary>
        /// <see cref="DirectorySecurity"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        public DirectorySecurity()
        {
            m_authRuleList = new List<AuthorizationRule>();
            m_accRuleList = new List<FileSystemAccessRule>();
            m_namingRuleList = new List<NamingRule>();
        }

        #endregion

        #region 메소드
        #region 액세스 규칙

        /// <summary>
        /// 액세스 규칙 컬렉션을 가져온다.
        /// </summary>
        /// <returns>액세스 규칙 컬렉션</returns>
        public Collection<FileSystemAccessRule> GetAccessRules()
        {
            return new Collection<FileSystemAccessRule>(m_accRuleList);
        }

        /// <summary>
        /// 지정한 ACL(액세스 제어 목록) 권한을 현재 디렉토리에 설정한다.
        /// </summary>
        /// <param name="rule">
        /// 디렉토리에 설정할 ACL(액세스 제어 목록) 권한을 나타내는 <see cref="FileSystemAccessRule"/> 개체
        /// </param>
        /// <remarks>
        /// <see cref="SetAccessRule"/> 메소드는 지정한 ACL 규칙을 추가하거나, 
        /// <paramref name="rule"/>의 <see cref="FileSystemAccessRule.Identity"/>와 일치하는 ACL 규칙을 덮어쓴다.
        /// </remarks>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="rule"/>이 <b>null</b>인 경우</exception>
        public void SetAccessRule(FileSystemAccessRule rule)
        {
            if (rule == null) throw new ArgumentNullException("rule");

            RemoveAccessRule(rule);

            m_accRuleList.Add(rule);
        }

        /// <summary>
        /// 현재 디렉토리에서 일치하는 ACL(액세스 제어 목록) 권한을 제거한다.
        /// </summary>
        /// <param name="rule">
        /// 디렉토리에서 제거할 ACL(액세스 제어 목록) 권한을 나타내는 <see cref="FileSystemAccessRule"/> 개체
        /// </param>
        /// <remarks>
        /// <see cref="RemoveAccessRule"/> 메소드는 현재 <see cref="DirectorySecurity"/> 개체에서 
        /// <see cref="FileSystemAccessRule.Identity"/>가 일치하는 ACL 규칙을 제거한다.
        /// </remarks>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="rule"/>이 <b>null</b>인 경우</exception>
        public void RemoveAccessRule(FileSystemAccessRule rule)
        {
            if (rule == null) throw new ArgumentNullException("rule");

            m_accRuleList.RemoveAll(//new Predicate<FileSystemAccessRule>(
                delegate(FileSystemAccessRule item)
                {
                    return (item.Identity == rule.Identity);
                }
                );//));
        }

        /// <summary>
        /// 현재 디렉토리에서 모든 ACL(액세스 제어 목록) 권한을 제거한다.
        /// </summary>
        public void RemoveAccessRuleAll()
        {
            m_accRuleList.Clear();
        }

        #endregion

        #region 명명 규칙

        /// <summary>
        /// 지정한 파일 명명 규칙을 현재 디렉토리에 설정한다.
        /// </summary>
        /// <param name="rule">디렉토리에 설정할 파일 명명 규칙을 나타내는 <see cref="NamingRule"/> 개체</param>
        /// <remarks>
        /// <see cref="SetNamingRule"/> 메소드는 지정한 파일 명명 규칙을 추가하거나, 
        /// <paramref name="rule"/>의 <see cref="NamingRule.Pattern"/>과 일치하는 파일 명명 규칙을 덮어쓴다.
        /// </remarks>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="rule"/>이 <b>null</b>인 경우</exception>
        public void SetNamingRule(NamingRule rule)
        {
            if (rule == null) throw new ArgumentNullException("rule");

            RemoveNamingRule(rule);

            m_namingRuleList.Add(rule);
        }

        /// <summary>
        /// 파일 명명 규칙 컬렉션을 가져온다.
        /// </summary>
        /// <returns>파일 명명 규칙 컬렉션</returns>
        public Collection<NamingRule> GetNamingRules()
        {
            return (new Collection<NamingRule>(m_namingRuleList));
        }

        /// <summary>
        /// 현재 디렉토리에서 일치하는 파일 명명 규칙을 제거한다.
        /// </summary>
        /// <param name="rule">디렉토리에서 제거할 파일 명명 규칙을 나타내는 <see cref="NamingRule"/> 개체</param>
        /// <remarks>
        /// <see cref="RemoveNamingRule"/> 메소드는 현재 <see cref="DirectorySecurity"/> 개체에서 
        /// <see cref="NamingRule.Pattern"/>이 일치하는 파일 명명 규칙을 제거한다.
        /// </remarks>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="rule"/>이 <b>null</b>인 경우</exception>
        public void RemoveNamingRule(NamingRule rule)
        {
            if (rule == null) throw new ArgumentNullException("rule");

            m_namingRuleList.RemoveAll(//new Predicate<FileSystemNamingRule>(
                delegate(NamingRule item)
                {
                    return String.Equals(item.Pattern, rule.Pattern, 
                                         StringComparison.OrdinalIgnoreCase);
                }
                );//));
        }

        /// <summary>
        /// 현재 디렉토리에서 모든 파일 명명 규칙을 제거한다.
        /// </summary>
        public void RemoveNamingRuleAll()
        {
            m_namingRuleList.Clear();
        }

        #endregion
        #endregion
    }
}
