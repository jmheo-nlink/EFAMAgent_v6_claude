#region 변경 이력
/*
 * Author : Link mskoo (2011. 9. 6)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-09-06   mskoo           최초 작성.
 *                              
 * 2011-09-23   mskoo           5.0 버전 릴리즈. (변경 이력 정리)
 * 
 * 2011-09-29   mskoo           메소드 추가.
 *                              - Login(string, string, string, string)
 *                              
 * 2011-11-02   mskoo           로그인에 실패한 경우 예외를 throw하는 로직을 수정.
 *                              - Login(string, string, string, string)
 *                              
 * 2011-11-03   mskoo           메소드 추가.
 *                              - ChangePassword(string, string, string)
 *                              
 * 2012-04-20   mskoo           로컬 컴퓨터의 IP 주소와 MAC 주소를 사용하도록 수정.
 *                              - Login(string, string, string, string)
 *                              - Logout(Credential)
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
// log4net 라이브러리
using log4net;
// .NET용 개발 라이브러리
//using Link.DLK.Net;
//using Link.DLK.Security.Cryptography;
using Link.Core.Security.Cryptography;
// E-FAM 관련
using Link.EFAM.Common;
using Link.EFAM.Engine.InternalServices;

using Resource = Link.EFAM.Engine.Properties.Resources;

namespace Link.EFAM.Engine.Services
{
    /// <summary>
    /// 웹 서비스로 E-FAM 사용자의 인증을 처리한다.
    /// </summary>
    public static class AuthenticationService
    {
        //
        // 추적/로그
        //
        private static BooleanSwitch m_tracing = new BooleanSwitch("traceSwitch", "Engine Module");
        private static ILog m_logger = LogManager.GetLogger(typeof(AuthenticationService));

        #region 메소드

        /// <summary>
        /// 서버에 로그인한다.
        /// </summary>
        /// <param name="url">E-FAM 서버의 URL</param>
        /// <param name="userId">로그인할 사용자의 ID</param>
        /// <param name="password">로그인할 사용자의 비밀번호</param>
        /// <returns>로그인한 사용자의 자격 증명을 나타내는 <see cref="Credential"/> 개체</returns>
        /// 
        /// <exception cref="ArgumentNullException">url, userId 또는 password가 null인 경우</exception>
        /// <exception cref="ArgumentException">userId가 길이가 0인 문자열이거나 공백만 포함한 경우</exception>
        /// <exception cref="UriFormatException">url이 잘못된 URL 형식인 경우</exception>
        /// <exception cref="LoginErrorException">로그인에 실패한 경우</exception>
        /// <exception cref="System.Net.WebException">HTTP 상태 오류가 발생한 경우</exception>
        /// <exception cref="System.Web.Services.Protocols.SoapException">
        /// 서버 컴퓨터에 요청이 도달했지만 성공적으로 처리되지 않은 경우
        /// </exception>
        public static Credential Login(string url, string userId, string password)
        {
            return Login(url, null, userId, password);
        }

        /// <summary>
        /// 서버에 로그인한다.
        /// </summary>
        /// <param name="url">E-FAM 서버의 URL</param>
        /// <param name="domainName">로그인할 사용자와 관련된 네트워크 도메인 이름</param>
        /// <param name="userId">로그인할 사용자의 ID</param>
        /// <param name="password">로그인할 사용자의 비밀번호</param>
        /// <returns>로그인한 사용자의 자격 증명을 나타내는 <see cref="Credential"/> 개체</returns>
        /// 
        /// <exception cref="ArgumentNullException">url, userId 또는 password가 null인 경우</exception>
        /// <exception cref="ArgumentException">userId가 길이가 0인 문자열이거나 공백만 포함한 경우</exception>
        /// <exception cref="UriFormatException">url이 잘못된 URL 형식인 경우</exception>
        /// <exception cref="LoginErrorException">로그인에 실패한 경우</exception>
        /// <exception cref="System.Net.WebException">HTTP 상태 오류가 발생한 경우</exception>
        /// <exception cref="System.Web.Services.Protocols.SoapException">
        /// 서버 컴퓨터에 요청이 도달했지만 성공적으로 처리되지 않은 경우
        /// </exception>
        public static Credential Login(string url, string domainName, string userId, string password)
        {
            if (url == null) throw new ArgumentNullException("url");
            if (userId == null) throw new ArgumentNullException("userId");
            if (userId.Trim().Length == 0)
            {
                throw new ArgumentException(Resource.Argument_WhiteSpaceString, "userId");
            }
            if (password == null) throw new ArgumentNullException("password");

            Credential credential = null;
            EFAMAgentWebService webService = null;
            EFAMLoginStatus loginStatus = null;
            SymmetricCryptographer crypto = new SymmetricCryptographer(Constants.CryptoPassword, Constants.CryptoKeySalt);
            Uri serverUrl = url.EndsWith("/") ? new Uri(url) : new Uri(url + "/");

            webService = WSClientProxyFactory.CreateAgentWSClientProxy(serverUrl);
            //
            // 웹 서비스 메소드를 호출하여 로그인한다.
            //
            if (String.IsNullOrEmpty(domainName))
            {
                loginStatus = webService.Login(crypto.Encrypt(userId), crypto.Encrypt(password),
                                               NetworkUtility.GetIPAddress());
            }
            else
            {
                loginStatus = webService.LoginAsDomainComputer(domainName,
                    crypto.Encrypt(userId), crypto.Encrypt(password), NetworkUtility.GetIPAddress());
            }

            //
            // 로그인에 성공할 경우
            //
            if (loginStatus.IsLoggedIn)
            {
                credential = new Credential(userId);
                credential.IsAuthenticated = true;
                credential.IsManager = loginStatus.IsManager;
                credential.ServerUrl = serverUrl;
                // 사용자 데이터를 설정한다.
                credential.UseRecycleBin = loginStatus.UseRecycleBin;
                credential.AllowSaveAs = loginStatus.AllowSaveAs;
            } // if (loginStatus.IsLoggedIn)
            //
            // 로그인에 실패할 경우
            //
            else
            {
                throw new LoginErrorException((LoginErrorCode)loginStatus.ErrorMsg);
            }

            return credential;
        }

        /// <summary>
        /// 서버에서 로그아웃한다.
        /// </summary>
        /// <param name="credential">사용자 자격 증명을 나타내는 <see cref="Credential"/> 개체</param>
        /// 
        /// <exception cref="ArgumentNullException">credential이 null인 경우</exception>
        public static void Logout(Credential credential)
        {
            if (credential == null) throw new ArgumentNullException("credential");
            if (!credential.IsAuthenticated) return;

            EFAMAgentWebService webService = null;

            try
            {
                credential.IsAuthenticated = false;

                webService = WSClientProxyFactory.CreateAgentWSClientProxy(credential.ServerUrl);
                // 웹 서비스 메소드를 호출하여 로그를 저장한다.
                webService.WriteLog(credential.UserId, "", "", "LOGOUT", 
                                    NetworkUtility.GetIPAddress());
            } // try
            catch (Exception exc)
            {
                // WebException - HTTP 상태 오류가 발생한 경우
                // SoapException - SOAP를 통해 XML Web services 메서드를 호출했지만 예외가 발생한 경우
                //                 서버 컴퓨터에 요청이 도달했지만 성공적으로 처리되지 않은 경우
                string message = String.Format("AuthenticationService.Logout() - {0}\n{1}", 
                                               credential.UserId, exc);

                if (m_logger.IsErrorEnabled) m_logger.Error(message);
                if (m_tracing.Enabled) Trace.WriteLine("[EFAM.Error] " + message);
            } // catch
        }

        /// <summary>
        /// 지정한 사용자의 비밀번호를 변경한다.
        /// </summary>
        /// <param name="url">E-FAM 서버의 URL</param>
        /// <param name="userId">비밀번호를 변경할 사용자의 ID</param>
        /// <param name="newPassword">지정한 사용자의 새 비밀번호</param>
        /// <returns>비밀번호가 성공적으로 변경되었으면 true, 그렇지 않으면 false</returns>
        /// 
        /// <exception cref="ArgumentNullException">
        /// url, userId 또는 newPassword가 null인 경우
        /// </exception>
        /// <exception cref="ArgumentException">userId가 길이가 0인 문자열이거나 공백만 포함한 경우</exception>
        /// <exception cref="UriFormatException">url이 잘못된 URL 형식인 경우</exception>
        /// <exception cref="System.Net.WebException">HTTP 상태 오류가 발생한 경우</exception>
        /// <exception cref="System.Web.Services.Protocols.SoapException">
        /// 서버 컴퓨터에 요청이 도달했지만 성공적으로 처리되지 않은 경우
        /// </exception>
        public static bool ChangePassword(string url, string userId, string newPassword)
        {
            if (url == null) throw new ArgumentNullException("url");
            if (userId == null) throw new ArgumentNullException("userId");
            if (userId.Trim().Length == 0)
            {
                throw new ArgumentException(Resource.Argument_WhiteSpaceString, "userId");
            }
            if (newPassword == null) throw new ArgumentNullException("newPassword");

            EFAMAgentWebService webService = null;
            SymmetricCryptographer crypto = new SymmetricCryptographer(Constants.CryptoPassword, Constants.CryptoKeySalt);
            Uri serverUrl = url.EndsWith("/") ? new Uri(url) : new Uri(url + "/");
            bool changed = false;

            webService = WSClientProxyFactory.CreateAgentWSClientProxy(serverUrl);
            // 웹 서비스 메소드를 호출하여 비밀번호를 변경한다.
            changed = webService.ChangePwd(crypto.Encrypt(userId), crypto.Encrypt(newPassword));

            return changed;
        }

        #endregion
    }
}
