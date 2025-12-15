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
using System.Text.RegularExpressions;

namespace Link.EFAM.Security.AccessControl
{
    using AppResource = Link.EFAM.AppResources.AppResource;

    /// <summary>
    /// 파일이나 디렉토리에 대한 명명 규칙을 나타낸다.
    /// </summary>
    public class NamingRule
    {
        private Regex m_regex = null;

        #region 속성

        /// <summary>
        /// 이 규칙의 이름 패턴을 가져온다.
        /// </summary>
        /// <value>이 규칙의 이름 패턴을 나타내는 정규식 표현</value>
        public string Pattern
        {
            get { return m_regex.ToString(); }
        }

        #endregion

        #region 생성자

        /// <summary>
        /// 지정한 이름 패턴을 사용하여 <see cref="NamingRule"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        /// <param name="pattern">이름 패턴을 나타내는 정규식 표현</param>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="pattern"/>이 <b>null</b>인 경우</exception>
        /// <paramref name="pattern"/>이 빈 문자열("")이거나 모두 공백 문자로 이루어진 경우<br/>
        /// - 또는 -<br/>
        /// 정규식 구문 분석 오류가 발생한 경우
        public NamingRule(string pattern)
        {
            if (pattern == null) throw new ArgumentNullException("pattern");
            if (pattern.Trim().Length == 0)
            {
                throw new ArgumentException(AppResource.GetString("Argument_NamePattern"), "pattern");
            }

            m_regex = new Regex(pattern, RegexOptions.IgnoreCase);
        }

        #endregion

        #region 메소드

        /// <summary>
        /// 지정한 이름이 이름 패턴과 일치하는지 여부를 확인한다.
        /// </summary>
        /// <param name="name">일치하는지 확인할 파일 이름이나 디렉토리 이름</param>
        /// <returns>지정한 이름이 이름 패턴과 일치하면 <b>true</b>, 그렇지 않으면 <b>false</b></returns>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="name"/>이 <b>null</b>인 경우</exception>
        public bool IsMatch(string name)
        {
            if (name == null) throw new ArgumentNullException("name");

            return m_regex.IsMatch(name);
        }

        #endregion
    }
}
