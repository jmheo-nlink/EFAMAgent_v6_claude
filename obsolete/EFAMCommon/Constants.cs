#region 변경 이력
/*
 * Author : Link mskoo (2011. 8. 8)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-08-08   mskoo           최초 작성.
 * 
 * 2011-09-23   mskoo           5.0 버전 릴리즈.
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace Link.EFAM.Common
{
    /// <summary>
    /// 상수 값들을 제공한다.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// 액세스 제어 미니필터 드라이버의 이름
        /// </summary>
        public const string FacFilterName = "EfamFacFlt";

        /// <summary>
        /// 비밀 키를 파생시키는데 사용할 암호
        /// </summary>
        public const string CryptoPassword = "Link_EFAM";
        /// <summary>
        /// 비밀 키를 파생시키는데 사용할 키 솔트
        /// </summary>
        public const string CryptoKeySalt = "EFAM_WebService";
    }
}
