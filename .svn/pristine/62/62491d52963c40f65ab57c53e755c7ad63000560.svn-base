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

namespace Link.EFAM.Security.AccessControl
{
    using Link.EFAM.Security.Principal;
    using AppResource = Link.EFAM.AppResources.AppResource;

    /// <summary>
    /// 파일이나 디렉토리에 액세스할 때 적용할 권한 부여 형식을 지정한다.
    /// </summary>
    public enum AuthorizationRuleAction
    {
        /// <summary>
        /// 파일이나 디렉토리에 대한 액세스를 허용한다.
        /// </summary>
        Allow,
        /// <summary>
        /// 파일이나 디렉토리에 대한 액세스를 거부한다.
        /// </summary>
        Deny,
    }

    /// <summary>
    /// 파일이나 디렉토리에 대한 권한 부여 규칙을 나타낸다.<br/>
    /// 응용 프로그램에 대해 허용 또는 거부된 액세스를 나타낸다.
    /// </summary>
    public class AuthorizationRule
    {
        private ApplicationIdentifier m_identity = null;
        private AuthorizationRuleAction m_action;

        #region 속성

        /// <summary>
        /// 이 규칙을 적용할 <see cref="ApplicationIdentifier"/>를 가져온다.
        /// </summary>
        /// <value>이 규칙을 적용할 <see cref="ApplicationIdentifier"/></value>
        public ApplicationIdentifier Identity
        {
            get { return m_identity; }
        }

        /// <summary>
        /// 이 규칙과 관련된 권한 부여 형식을 가져온다.
        /// </summary>
        /// <value>이 규칙과 관련된 <see cref="AuthorizationRuleAction"/> 값</value>
        public AuthorizationRuleAction Action
        {
            get { return m_action; }
        }

        #endregion

        #region 생성자

        /// <summary>
        /// 지정한 값을 사용하여 <see cref="AuthorizationRule"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        /// <param name="identity">규칙을 적용할 응용 프로그램</param>
        /// <param name="action">액세스의 허용 여부를 지정하는 <see cref="AuthorizationRuleAction"/> 값 중 하나</param>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="identity"/>가 <b>null</b>인 경우</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="action"/>이 잘못된 값을 지정하는 경우</exception>
        public AuthorizationRule(ApplicationIdentifier identity, AuthorizationRuleAction action)
        {
            if (identity == null) throw new ArgumentNullException("identity");
            if (action != AuthorizationRuleAction.Allow && action != AuthorizationRuleAction.Deny)
            {
                throw new ArgumentOutOfRangeException("action", AppResource.GetString("ArgumentOutOfRange_Enum"));
            }

            m_identity = identity;
            m_action = action;
        }

        #endregion
    }
}
