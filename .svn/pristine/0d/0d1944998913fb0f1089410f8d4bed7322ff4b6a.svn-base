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

using AppResource = Link.EFAM.AppResources.AppResource;

namespace Link.EFAM.Security.Principal
{
    /// <summary>
    /// 응용 프로그램 ID를 나타낸다.
    /// </summary>
    public sealed class ApplicationIdentifier : IdentityReference, IEquatable<ApplicationIdentifier>
    {
        private string m_fileName = null;

        #region 연산자

        /// <summary>
        /// 두 <see cref="ApplicationIdentifier"/> 개체가 동일한지 비교한다.<br/>
        /// 두 개체가 <see cref="Value"/> 속성에 의해 반환된 것과 동일한 ID 표현을 가지거나 
        /// 둘 다 <b>null</b>인 경우 같은 것으로 간주된다.
        /// </summary>
        /// <param name="left">
        /// 같은지 비교할 때 사용할 왼쪽 피연산자.<br/>
        /// 이 매개 변수는 <b>null</b>일 수 있다.
        /// </param>
        /// <param name="right">
        /// 같은지 비교할 때 사용할 오른쪽 피연산자.<br/>
        /// 이 매개 변수는 <b>null</b>일 수 있다.
        /// </param>
        /// <returns>
        /// <paramref name="left"/>와 <paramref name="right"/>가 같으면 <b>true</b>, 그렇지 않으면 <b>false</b>
        /// </returns>
        public static bool operator ==(ApplicationIdentifier left, ApplicationIdentifier right)
        {
            object leftObj = left;
            object rightObj = right;

            if (leftObj == null && rightObj == null) return true;
            if (leftObj == null || rightObj == null) return false;

            return String.Equals(left.ToString(), right.ToString(), 
                                 StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 두 <see cref="ApplicationIdentifier"/> 개체가 동일하지 않은지 비교한다.<br/>
        /// 두 개체가 <see cref="Value"/> 속성에 의해 반환된 것과 다른 ID 표현을 가지거나 
        /// 개체 중 하나는 <b>null</b>이고 나머지는 그렇지 않을 경우 다른 것으로 간주된다.
        /// </summary>
        /// <param name="left">
        /// 다른지 비교할 때 사용할 왼쪽 피연산자.<br/>
        /// 이 매개 변수는 <b>null</b>일 수 있다.
        /// </param>
        /// <param name="right">
        /// 다른지 비교할 때 사용할 오른쪽 피연산자.<br/>
        /// 이 매개 변수는 <b>null</b>일 수 있다.
        /// </param>
        /// <returns>
        /// <paramref name="left"/>와 <paramref name="right"/>가 같지 않으면 <b>true</b>, 같으면 <b>false</b>
        /// </returns>
        public static bool operator !=(ApplicationIdentifier left, ApplicationIdentifier right)
        {
            return !(left == right);
        }

        #endregion

        #region 정적 속성

        /// <summary>
        /// 모든 응용 프로그램을 나타내는 <see cref="ApplicationIdentifier"/> 인스턴스를 가져온다.
        /// </summary>
        /// <value>모든 응용 프로그램을 나타내는 <see cref="ApplicationIdentifier"/> 개체</value>
        public static ApplicationIdentifier All
        {
            get { return (new ApplicationIdentifier()); }
        }

        #endregion

        #region 속성

        /// <summary>
        /// 모든 응용 프로그램을 나타내는 ID로 식별되는지 여부를 나타내는 값을 가져온다.
        /// </summary>
        /// <value>모든 응용 프로그램을 나타내는 ID이면 <b>true</b>, 그렇지 않으면 <b>false</b></value>
        public bool IsAll
        {
            get
            {
                return (m_fileName == null || m_fileName == "*" || m_fileName == "*.*");
            }
        }

        #endregion

        #region 생성자

        /// <summary>
        /// <see cref="ApplicationIdentifier"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        private ApplicationIdentifier()
        {
        }

        /// <summary>
        /// 지정한 실행 파일 이름을 사용하여 <see cref="ApplicationIdentifier"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        /// <param name="fileName">실행 파일 이름</param>
        /// 
        /// <exception cref="ArgumentException">
        /// <paramref name="fileName"/>이 <b>null</b>이거나 빈 문자열("")인 경우
        /// </exception>
        public ApplicationIdentifier(string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException(AppResource.GetString("Argument_NullOrEmptyString"), "fileName");
            }

            m_fileName = fileName;
        }

        #endregion

        #region IdentityReference 멤버

        /// <summary>
        /// 이 <see cref="ApplicationIdentifier"/> 개체로 표시된 ID에 대한 대문자 문자열을 가져온다.
        /// </summary>
        /// <value>이 <see cref="ApplicationIdentifier"/> 개체로 표시된 ID에 대한 대문자 문자열</value>
        public override string Value
        {
            get { return ToString().ToUpperInvariant(); }
        }

        #region 메소드

        /// <summary>
        /// 지정한 개체가 이 <see cref="ApplicationIdentifier"/> 개체와 같은지 여부를 확인한다.
        /// </summary>
        /// <param name="obj">이 <see cref="ApplicationIdentifier"/> 개체와 비교할 개체</param>
        /// <returns>
        /// <paramref name="obj"/>가 이 <see cref="ApplicationIdentifier"/> 개체와 
        /// 같은 값을 가지는 개체이면 <b>true</b>, 그렇지 않으면 <b>false</b>
        /// </returns>
        public override bool Equals(object obj)
        {
            ApplicationIdentifier other = obj as ApplicationIdentifier;

            if (other == null) return false;

            return (this == other);
        }

        /// <summary>
        /// 이 <see cref="ApplicationIdentifier"/> 개체의 해시 코드를 가져온다.
        /// </summary>
        /// <returns>이 <see cref="ApplicationIdentifier"/> 개체의 해시 코드</returns>
        public override int GetHashCode()
        {
            return StringComparer.InvariantCultureIgnoreCase.GetHashCode(m_fileName);
        }

        /// <summary>
        /// 이 <see cref="ApplicationIdentifier"/> 개체로 표시된 ID에 대한 문자열 표현을 반환한다.
        /// </summary>
        /// <returns>이 <see cref="ApplicationIdentifier"/> 개체로 표시된 ID에 대한 문자열 표현</returns>
        public override string ToString()
        {
            return m_fileName;
        }

        #endregion
        #endregion

        #region IEquatable<T> 멤버

        /// <summary>
        /// 지정한 <see cref="ApplicationIdentifier"/> 개체가 
        /// 현재 <see cref="ApplicationIdentifier"/> 개체와 같은지 여부를 나타낸다.
        /// </summary>
        /// <param name="other">현재 개체와 비교할 개체</param>
        /// <returns>
        /// <paramref name="other"/>의 값이 현재 <see cref="ApplicationIdentifier"/> 개체의 값과 
        /// 같으면 <b>true</b>, 그렇지 않으면 <b>false</b>
        /// </returns>
        public bool Equals(ApplicationIdentifier other)
        {
            if (other == null) return false;

            return (this == other);
        }

        #endregion
    }
}
