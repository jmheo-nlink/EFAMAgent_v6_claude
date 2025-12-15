#region 변경 이력
/*
 * Author : Link mskoo (2011. 7. 22)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-07-22   mskoo           최초 작성.
 * 
 * 2011-09-23   mskoo           5.0 버전 릴리즈.
 * 
 * 2011-11-02   mskoo           LoginErrorCode 혈거형 추가.
 * 
 * 2011-11-02   mskoo           메소드 추가.
 *                              - GetMessage(LoginErrorCode)
 *                              
 * 2011-11-25   mskoo           LoginErrorCode 열거형에 값을 추가.
 *                              - GetMessage(LoginErrorCode)
 * 
 * 2012-04-20   mskoo           LoginErrorCode 열거형에 값을 추가.
 *                              - GetMessage(LoginErrorCode)
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.Text;

using Resource = Link.EFAM.Engine.Properties.Resources;

namespace Link.EFAM.Engine.Services
{
    /// <summary>
    /// 로그인 실패에 대한 이유를 설명한다.
    /// </summary>
    public enum LoginErrorCode
    {
        /// <summary>
        /// 로그인에 성공했습니다.
        /// </summary>
        NoError,
        /// <summary>
        /// 사용자 ID가 등록되어 있지 않습니다.
        /// </summary>
        UserNotFound,
        /// <summary>
        /// 활성화된 사용자가 아닙니다.
        /// </summary>
        InactiveUser,
        /// <summary>
        /// 비밀번호가 틀렸습니다.
        /// </summary>
        WrongPassword,
        /// <summary>
        /// LDAP의 경로가 등록되어 있지 않습니다.
        /// </summary>
        LDAPNotFound,
        /// <summary>
        /// 차단된 IP 주소입니다.
        /// </summary>
        RestrictedIPAddress,
        /// <summary>
        /// 서버에 등록된 도메인 이름과 일치하지 않습니다.
        /// </summary>
        DomainNameMismatch,
        /// <summary>
        /// 비밀번호가 만료되었습니다.
        /// </summary>
        PasswordExpired,
        /// <summary>
        /// AD 계정이 만료되었습니다.
        /// </summary>
        ADAccountExpired,
        /// <summary>
        /// 차단된 MAC 주소입니다.
        /// </summary>
        RestrictedMACAddress,
    }

    /// <summary>
    /// 로그인에 실패할 때 throw되는 예외를 나타낸다.
    /// </summary>
    public class LoginErrorException : Exception
    {
        private LoginErrorCode m_errorCode;

        #region 속성

        /// <summary>
        /// 예외의 이유에 대한 오류 코드를 가져온다.
        /// </summary>
        /// <value>예외에 대한 이유를 설명하는 <see cref="LoginErrorCode"/> 열거형 값</value>
        public LoginErrorCode ErrorCode
        {
            get { return m_errorCode; }
        }

        #endregion

        #region 생성자

        /// <summary>
        /// 지정한 <see cref="ErrorCode"/> 값을 사용하여 
        /// <see cref="LoginErrorException"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        /// <param name="errorCode">예외에 대한 이유를 설명하는 <see cref="LoginErrorCode"/> 열거형 값</param>
        public LoginErrorException(LoginErrorCode errorCode)
            : base(GetMessage(errorCode))
        {
            m_errorCode = errorCode;
        }

        #endregion

        #region 메소드

        /// <summary>
        /// 지정한 오류 코드에 대한 오류 메시지를 가져온다.
        /// </summary>
        /// <param name="errorCode">로그인 실패에 대한 이유를 설명하는 <see cref="LoginErrorCode"/> 열거형 값</param>
        /// <returns>오류 메시지</returns>
        private static string GetMessage(LoginErrorCode errorCode)
        {
            switch (errorCode)
            {
                case LoginErrorCode.UserNotFound:
                    return Resource.Error_UserNotFound;

                case LoginErrorCode.InactiveUser:
                    return Resource.Error_InactiveUser;

                case LoginErrorCode.WrongPassword:
                    return Resource.Error_InvalidPassword;

                case LoginErrorCode.LDAPNotFound:
                    return Resource.Error_LDAPNotFound;

                case LoginErrorCode.RestrictedIPAddress:
                    return Resource.Error_RestrictedIPAddress;

                case LoginErrorCode.RestrictedMACAddress:
                    return Resource.Error_RestrictedMACAddress;

                case LoginErrorCode.DomainNameMismatch:
                    return Resource.Error_DomainNameMismatch;

                case LoginErrorCode.PasswordExpired:
                    return Resource.Error_PasswordExpired;

                case LoginErrorCode.ADAccountExpired:
                    return Resource.Error_ADAccountExpired;
            } // switch (errorCode)

            return String.Empty;
        }

        #endregion
    }
}
