#region 변경 이력
/*
 * Author : Link mskoo (2011. 9. 22)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-09-22   mskoo           최초 작성.
 * 
 * 2011-09-23   mskoo           5.0 버전 릴리즈.
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace Link.EFAM.Engine
{
    /// <summary>
    /// 휴지통 오류가 발생할 때 throw되는 예외를 나타낸다.
    /// </summary>
    public class RecycleBinException : Exception
    {
        #region 생성자

        /// <summary>
        /// 지정한 오류 메시지를 사용하여 <see cref="RecycleBinException"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        /// <param name="message">오류를 설명하는 메시지</param>
        public RecycleBinException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 지정한 오류 메시지와 해당 예외의 근본 원인인 내부 예외에 대한 참조를 사용하여 
        /// <see cref="RecycleBinException"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        /// <param name="message">예외에 대한 이유를 설명하는 오류 메시지</param>
        /// <param name="innerException">현재 예외의 원인인 예외이거나 내부 예외가 지정되지 않은 경우 null</param>
        public RecycleBinException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion
    }
}
