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

namespace Link.EFAM.Security.Principal
{
    /// <summary>
    /// ID를 나타내며 <see cref="Account"/> 및 <see cref="ApplicationIdentifier"/> 클래스의 기본 클래스이다.
    /// </summary>
    public abstract class IdentityReference
    {
        /*
         * System.Security.Principal.IdentityReference 클래스를 참고.
         */

        #region 연산자

        /// <summary>
        /// 두 <see cref="IdentityReference"/> 개체가 동일한지 비교한다.<br/>
        /// 두 개체가 <see cref="Value"/> 속성에 의해 반환된 것과 동일한 정식 이름 표현을 가지거나 
        /// 둘 다 <b>null</b>인 경우 같은 것으로 간주된다.
        /// </summary>
        /// <param name="left">
        /// 같은지 비교할 때 사용할 왼쪽 <see cref="IdentityReference"/> 피연산자.<br/>
        /// 이 매개 변수는 <b>null</b>일 수 있다.
        /// </param>
        /// <param name="right">
        /// 같은지 비교할 때 사용할 오른쪽 <see cref="IdentityReference"/> 피연산자.<br/>
        /// 이 매개 변수는 <b>null</b>일 수 있다.
        /// </param>
        /// <returns>
        /// <paramref name="left"/>와 <paramref name="right"/>가 같으면 <b>true</b>, 그렇지 않으면 <b>false</b>
        /// </returns>
        public static bool operator ==(IdentityReference left, IdentityReference right)
        {
            object leftObj = left;
            object rightObj = right;

            if (leftObj == null && rightObj == null) return true;
            if (leftObj == null || rightObj == null) return false;

            return left.Equals(right);
        }

        /// <summary>
        /// 두 <see cref="IdentityReference"/> 개체가 동일하지 않은지 비교한다.<br/>
        /// 두 개체가 <see cref="Value"/> 속성에 의해 반환된 것과 다른 정식 이름 표현을 가지거나 
        /// 개체 중 하나는 <b>null</b>이고 나머지는 그렇지 않을 경우 다른 것으로 간주된다.
        /// </summary>
        /// <param name="left">
        /// 다른지 비교할 때 사용할 왼쪽 <see cref="IdentityReference"/> 피연산자.<br/>
        /// 이 매개 변수는 <b>null</b>일 수 있다.
        /// </param>
        /// <param name="right">
        /// 다른지 비교할 때 사용할 오른쪽 <see cref="IdentityReference"/> 피연산자.<br/>
        /// 이 매개 변수는 <b>null</b>일 수 있다.
        /// </param>
        /// <returns>
        /// <paramref name="left"/>와 <paramref name="right"/>가 같지 않으면 <b>true</b>, 같으면 <b>false</b>
        /// </returns>
        public static bool operator !=(IdentityReference left, IdentityReference right)
        {
            return !(left == right);
        }

        #endregion

        #region 속성

        /// <summary>
        /// <see cref="IdentityReference"/> 개체로 표시한 ID의 문자열 값을 가져온다.
        /// </summary>
        /// <value><see cref="IdentityReference"/> 개체로 표시한 ID의 문자열 값</value>
        public abstract string Value 
        {
            get;
        }

        #endregion

        #region 생성자

        /// <summary>
        /// <see cref="IdentityReference"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        internal IdentityReference()
        {
        }

        #endregion

        #region Object 멤버

        /// <summary>
        /// 지정한 개체가 이 <see cref="IdentityReference"/> 개체와 같은지 여부를 확인한다.
        /// </summary>
        /// <param name="obj">이 <see cref="IdentityReference"/> 개체와 비교할 개체</param>
        /// <returns>
        /// <paramref name="obj"/>가 이 <see cref="IdentityReference"/> 개체와 
        /// 같은 값을 가지는 개체이면 <b>true</b>, 그렇지 않으면 <b>false</b>
        /// </returns>
        public override abstract bool Equals(object obj);

        /// <summary>
        /// 이 <see cref="IdentityReference"/> 개체의 해시 코드를 가져온다.
        /// </summary>
        /// <returns>이 <see cref="IdentityReference"/> 개체의 해시 코드</returns>
        public override abstract int GetHashCode();

        /// <summary>
        /// <see cref="IdentityReference"/> 개체로 표시한 ID의 문자열 표현을 반환한다.
        /// </summary>
        /// <returns>문자열 형식의 ID</returns>
        public override abstract string ToString();

        #endregion
    }
}
