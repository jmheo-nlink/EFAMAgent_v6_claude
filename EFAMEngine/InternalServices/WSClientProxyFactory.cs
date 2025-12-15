#region 변경 이력
/*
 * Author : Link mskoo (2011. 5. 18)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-05-18   mskoo           최초 작성.
 * 
 * 2011-09-23   mskoo           5.0 버전 릴리즈. (변경 이력 정리)
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace Link.EFAM.Engine.InternalServices
{
    /// <summary>
    /// 웹 서비스를 호출하기 위한 클라이언트 프록시 개체를 제공한다.
    /// </summary>
    internal static class WSClientProxyFactory
    {
        #region 메소드

        /// <summary>
        /// 에이전트 웹 서비스를 호출하기 위한 클라이언트 프록시 개체를 반환한다.
        /// </summary>
        /// <param name="baseUrl">E-FAM 서버의 웹 서비스들이 위치한 기본 URL</param>
        /// <returns>에이전트 웹 서비스를 호출하기 위한 클라이언트 프록시 개체</returns>
        /// 
        /// <exception cref="ArgumentNullException">url이 null인 경우</exception>
        public static EFAMAgentWebService CreateAgentWSClientProxy(Uri baseUrl)
        {
            if (baseUrl == null) throw new ArgumentNullException("baseUrl");

            EFAMAgentWebService webService = null;
            Uri serviceUrl = null;

            //
            // 에이전트 웹 서비스를 호출하기 위한 클라이언트 프록시 개체를 생성한다.
            // http://./EFAMServer/EFAMAgentWebService.asmx
            //
            serviceUrl = new Uri(baseUrl, "EFAMAgentWebService.asmx");
            webService = new EFAMAgentWebService(serviceUrl.AbsoluteUri);
            webService.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap12;

            return webService;
        }

        #endregion
    }
}
