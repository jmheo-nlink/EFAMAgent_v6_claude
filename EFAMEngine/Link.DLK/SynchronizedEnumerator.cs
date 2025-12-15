#region 변경 이력
/*
 * Author : Link mskoo (2011. 9. 19)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-09-19   mskoo           최초 작성.
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections;
using System.Collections.Generic;

namespace Link.DLK.Collections.Generic
{
    /// <summary>
    /// 제네릭 컬렉션을 스레드로부터 안전하게 반복할 수 있도록 지원한다.
    /// </summary>
    /// <typeparam name="T">열거할 개체의 형식</typeparam>
    /// <remarks>
    /// 동기화된 제네릭 컬렉션에서 획득한 열거자가 스레드로부터 안전하도록 열거자를 래핑한다.
    /// </remarks>
    public class SynchronizedEnumerator<T> : IEnumerator<T>, IEnumerator
    {
        private IEnumerator<T> m_enumerator = null;
        private object m_syncObject = null;

        #region 생성자

        /// <summary>
        /// 지정한 동기화 개체와 열거자를 사용하여 
        /// <see cref="SynchronizedEnumerator&lt;T&gt;"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        /// <param name="syncRoot">액세스를 동기화하는데 사용되는 개체</param>
        /// <param name="enumerator">제네릭 컬렉션의 요소를 열거하는 <see cref="IEnumerator&lt;T&gt;"/></param>
        /// 
        /// <exception cref="ArgumentNullException">syncRoot 또는 enumerator가 null인 경우</exception>
        public SynchronizedEnumerator(object syncRoot, IEnumerator<T> enumerator)
        {
            if (syncRoot == null) throw new ArgumentNullException("syncRoot");
            if (enumerator == null) throw new ArgumentNullException("enumerator");

            m_enumerator = enumerator;
            m_syncObject = syncRoot;
        }

        #endregion

        #region IEnumerator<T> 멤버

        /// <summary>
        /// 컬렉션에서 열거자의 현재 위치에 있는 요소를 가져온다.
        /// </summary>
        /// <value>컬렉션에서 열거자의 현재 위치에 있는 요소</value>
        public T Current
        {
            get 
            {
                lock (m_syncObject)
                {
                    return m_enumerator.Current;
                } // lock
            } // get
        }

        #endregion

        #region IDisposable 멤버

        /// <summary>
        /// <see cref="SynchronizedEnumerator&lt;T&gt;"/> 개체에서 사용하는 모든 리소스를 해제한다.
        /// </summary>
        public void Dispose()
        {
            m_enumerator.Dispose();
        }

        #endregion

        #region IEnumerator 멤버

        /// <summary>
        /// 컬렉션의 현재 요소를 가져온다.
        /// </summary>
        /// <value>컬렉션의 현재 요소</value>
        /// 
        /// <exception cref="InvalidOperationException">
        /// 열거자가 컬렉션의 첫 번째 요소 앞 또는 마지막 요소 뒤에 배치되는 경우
        /// </exception>
        object IEnumerator.Current
        {
            get { return this.Current; }
        }

        /// <summary>
        /// 열거자를 컬렉션의 다음 요소로 이동한다.
        /// </summary>
        /// <returns>열거자가 다음 요소로 이동한 경우 true, 컬렉션의 끝을 지난 경우 false</returns>
        /// 
        /// <exception cref="InvalidOperationException">열거자가 만들어진 후 컬렉션이 수정된 경우</exception>
        public bool MoveNext()
        {
            lock (m_syncObject)
            {
                return m_enumerator.MoveNext();
            } // lock
        }

        /// <summary>
        /// 컬렉션의 첫 번째 요소 앞의 초기 위치에 열거자를 설정한다.
        /// </summary>
        /// 
        /// <exception cref="InvalidOperationException">열거자가 만들어진 후 컬렉션이 수정된 경우</exception>
        public void Reset()
        {
            lock (m_syncObject)
            {
                m_enumerator.Reset();
            } // lock
        }

        #endregion
    }
}
