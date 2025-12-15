#region 변경 이력
/*
 * Author : Link mskoo (2011. 8. 22)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-08-22   mskoo           최초 작성.
 * ====================================================================================================================
 */
#endregion

using System;
using System.Text.RegularExpressions;

//using Link.DLK.Properties;

namespace Link.DLK.Security
{
    using Resources = Link.EFAM.Engine.Properties.Resources;

    /// <summary>
    /// 비밀번호의 강도(길이, 복잡성 등)를 확인한다.
    /// </summary>
    public class PasswordStrengthChecker
    {
        private int m_minLength = 0;

        #region 속성

        /// <summary>
        /// 비밀번호에 필요한 최소 길이를 가져오거나 설정한다.
        /// </summary>
        /// <value>비밀번호에 필요한 최소 길이</value>
        /// 
        /// <exception cref="ArgumentOutOfRangeException">속성 값이 1보다 작은 경우</exception>
        public int MinRequiredPasswordLength
        {
            get { return m_minLength; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("value",
                        String.Format(Resources.Error_OutOfRangeProperty_Gt, "MinRequiredPasswordLength", "0"));
                }

                m_minLength = value;
            } // set
        }

        #endregion

        #region 생성자

        /// <summary>
        /// 지정한 최소 길이를 사용하여 <see cref="PasswordStrengthChecker"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        /// <param name="minLength">비밀번호에 필요한 최소 길이</param>
        /// 
        /// <exception cref="ArgumentOutOfRangeException">minLength가 1보다 작은 경우</exception>
        public PasswordStrengthChecker(int minLength)
        {
            if (minLength < 1)
            {
                throw new ArgumentOutOfRangeException("minLength",
                    String.Format(Resources.Error_OutOfRangeParameter_Gt, "0"));
            }

            m_minLength = minLength;
        }

        #endregion

        #region 메소드

        /// <summary>
        /// 비밀번호의 길이를 확인한다.
        /// </summary>
        /// <param name="password">확인할 비밀번호</param>
        /// <returns>비밀번호가 최소 길이보다 길면 true, 그렇지 않으면 false</returns>
        public bool CheckLength(string password)
        {
            if (password == null) return false;

            return (password.Length >= m_minLength);
        }

        /// <summary>
        /// 비밀번호의 복잡성을 확인한다.
        /// </summary>
        /// <param name="password">확인할 비밀번호</param>
        /// <returns>유효한 비밀번호이면 true, 그렇지 않으면 false</returns>
        public virtual bool CheckComplexity(string password)
        {
            if (password == null) return false;

            // "(?=.{6,})(?=(.*\d){1,})(?=(.*\W){1,})" 
            return Regex.IsMatch(password, @"(?=(.*[a-zA-Z_]){1,})(?=(.*\d){1,})(?=(.*\W){1,})");
        }

        #endregion
    }
}
