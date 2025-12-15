#region 변경 이력
/*
 * Author : Link mskoo (2011. 6. 11)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-06-11   mskoo           최초 작성.
 * 
 * 2011-09-23   mskoo           5.0 버전 릴리즈.
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.Text;
// E-FAM 관련
using Link.EFAM.Common;

namespace Link.EFAM.Engine
{
    /// <summary>
    /// 키와 프로세스 종류의 컬렉션을 나타낸다.<br/>
    /// 비교자는 <see cref="StringComparer.OrdinalIgnoreCase"/>로 두 키가 같은지 여부를 확인한다.
    /// (문자열의 대/소문자를 무시하고 두 개체를 비교)
    /// </summary>
    /// <remarks>
    /// 프로세스를 시작한 실행 파일을 만들 때 사용한 이름을 키로 사용한다.
    /// </remarks>
    public class ProcessKindDictionary : Dictionary<string, ProcessKind>
    {
        #region 생성자

        /// <summary>
        /// <see cref="ProcessKindDictionary"/> 클래스의 인스턴스를 초기화한다.
        /// </summary>
        public ProcessKindDictionary()
            : base(128, StringComparer.OrdinalIgnoreCase)
        {
        }

        #endregion
    }
}
