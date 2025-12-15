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

using Debug = System.Diagnostics.Debug;

namespace Link.EFAM.Security.AccessControl
{
    /// <summary>
    /// <see cref="FileSystemAccessRule"/> 개체의 컬렉션을 나타낸다.
    /// </summary>
    public sealed class RuleCollection : System.Collections.ReadOnlyCollectionBase
    {
        #region 속성

        /// <summary>
        /// 컬렉션의 지정한 인덱스에서 <see cref="FileSystemAccessRule"/> 개체를 가져온다.
        /// </summary>
        /// <param name="index">가져올 <see cref="FileSystemAccessRule"/> 개체의 인덱스(0부터 시작)</param>
        /// <returns>지정한 인덱스에 있는 <see cref="FileSystemAccessRule"/> 개체</returns>
        public FileSystemAccessRule this[int index]
        {
            get 
            {
                return base.InnerList[index] as FileSystemAccessRule;
            }
        }

        #endregion

        #region 생성자

        /// <summary>
        /// <see cref="RuleCollection"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        internal RuleCollection()
        {
        }

        /// <summary>
        /// <see cref="RuleCollection"/> 클래스의 새 인스턴스를 지정한 컬렉션의 래퍼로 초기화한다.
        /// </summary>
        /// <param name="collection">새 컬렉션이 래핑하는 컬렉션</param>
        internal RuleCollection(System.Collections.IEnumerable collection)
        {
            Debug.Assert(collection != null, "collection cannot be null.");

            foreach (FileSystemAccessRule rule in collection)
            {
                base.InnerList.Add(rule);
            }
        }

        #endregion

        #region 메소드

        /// <summary>
        /// <see cref="FileSystemAccessRule"/> 개체를 컬렉션의 끝 부분에 추가한다.
        /// </summary>
        /// <param name="rule">컬렉션의 끝에 추가할 <see cref="FileSystemAccessRule"/> 개체</param>
        internal void AddRule(FileSystemAccessRule rule)
        {
            Debug.Assert(rule != null, "rule cannot be null.");

            base.InnerList.Add(rule);
        }

        /// <summary>
        /// 컬렉션의 내용을 배열에 복사한다.
        /// </summary>
        /// <param name="rules">컬렉션의 내용을 복사할 배열</param>
        /// <param name="index"><paramref name="rules"/>에서 복사가 시작되는 인덱스(0부터 시작)</param>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="rules"/>가 <b>null</b>인 경우</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/>가 0보다 작은 경우</exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="index"/>가 <paramref name="rules"/>의 길이보다 크거나 같은 경우<br/>
        /// - 또는 -<br/>
        /// 컬렉션의 요소 수가 <paramref name="index"/>에서 <paramref name="rules"/> 끝까지 사용 가능한 공간보다 큰 경우 
        /// </exception>
        public void CopyTo(FileSystemAccessRule[] rules, int index)
        {
            base.InnerList.CopyTo(rules, index);
        }

        #endregion
    }
}
