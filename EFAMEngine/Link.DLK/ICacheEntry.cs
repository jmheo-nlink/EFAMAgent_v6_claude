#region 변경 이력
/*
 * Author : Link mskoo (2011. 4. 11)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-04-11   mskoo           최초 작성.
 * 
 * 2011-07-03   mskoo           1.0 버전 릴리즈.
 * ====================================================================================================================
 */
#endregion

using System;

namespace Link.DLK.Caching
{
    /// <summary>
    /// 캐시의 엔트리를 정의한다.
    /// </summary>
    public interface ICacheEntry
    {
        #region 속성

        /// <summary>
        /// 캐시 항목을 참조하는데 사용되는 캐시 키를 가져온다.
        /// </summary>
        /// <value>캐시 항목을 참조하는데 사용되는 캐시 키</value>
        string Key
        { get; }

        /// <summary>
        /// 캐시가 만료되는 날짜와 시간을 가져온다.
        /// </summary>
        /// <value>캐시가 만료되는 <see cref="DateTime"/> 값</value>
        DateTime ExpireDate
        { get; }

        #endregion
    }
}
