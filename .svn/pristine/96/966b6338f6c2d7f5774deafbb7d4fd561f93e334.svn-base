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
using System.Collections.ObjectModel;

namespace Link.DLK.Collections.Generic
{
    /// <summary>
    /// 제네릭 매개 변수를 통해 지정한 형식의 개체가 요소로 포함된, 스레드로부터 안전한 컬렉션을 제공한다.
    /// </summary>
    /// <typeparam name="T">스레드로부터 안전한 컬렉션에 요소로 포함된 개체의 형식</typeparam>
    public class SynchronizedCollection<T> 
        : IList<T>, ICollection<T>, IEnumerable<T>, IList, ICollection, IEnumerable
    {
        private Collection<T> m_data = null;
        private object m_syncObject = null;

        #region 생성자

        /// <summary>
        /// <see cref="SynchronizedCollection&lt;T&gt;"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        public SynchronizedCollection()
        {
            m_data = new Collection<T>();
            m_syncObject = ((ICollection)m_data).SyncRoot;
        }

        #endregion

        #region IList<T> 멤버

        /// <summary>
        /// <see cref="SynchronizedCollection&lt;T&gt;"/>에서 맨 처음 발견되는 지정한 요소의 인덱스를 반환한다.
        /// </summary>
        /// <param name="item">
        /// <see cref="SynchronizedCollection&lt;T&gt;"/>에서 찾을 요소. 참조 형식에 대해 값은 null이 될 수 있다.
        /// </param>
        /// <returns>
        /// <see cref="SynchronizedCollection&lt;T&gt;"/>에서 처음 일치하는 요소의 인덱스(0부터 시작), 그렇지 않으면 -1
        /// </returns>
        public int IndexOf(T item)
        {
            lock (m_syncObject)
            {
                return m_data.IndexOf(item);
            } // lock
        }

        /// <summary>
        /// <see cref="SynchronizedCollection&lt;T&gt;"/>의 지정한 인덱스에 요소를 삽입한다.
        /// </summary>
        /// <param name="index">요소를 삽입할 인덱스(0부터 시작)</param>
        /// <param name="item">삽입할 요소. 참조 형식에 대해 값은 null이 될 수 있다.</param>
        /// <remarks>
        /// <see cref="SynchronizedCollection&lt;T&gt;"/>은 null을 참조 형식에 대해 유효한 값으로 받아들이며 
        /// 중복 요소를 허용한다.
        /// </remarks>
        /// 
        /// <exception cref="ArgumentOutOfRangeException">index가 0보다 작거나 컬렉션의 요소 수보다 큰 경우</exception>
        public void Insert(int index, T item)
        {
            lock (m_syncObject)
            {
                m_data.Insert(index, item);
            } // lock
        }

        /// <summary>
        /// <see cref="SynchronizedCollection&lt;T&gt;"/>의 지정한 인덱스에서 요소를 제거한다.
        /// </summary>
        /// <param name="index">제거할 요소의 인덱스(0부터 시작)</param>
        /// 
        /// <exception cref="ArgumentOutOfRangeException">index가 0보다 작거나 컬렉션의 요소 수보다 큰 경우</exception>
        public void RemoveAt(int index)
        {
            lock (m_syncObject)
            {
                m_data.RemoveAt(index);
            } // lock
        }

        /// <summary>
        /// 지정한 인덱스에 있는 요소를 가져오거나 설정한다.
        /// </summary>
        /// <param name="index">가져오거나 설정할 요소의 인덱스(0부터 시작)</param>
        /// <value>지정한 인덱스의 요소</value>
        /// <remarks>
        /// <see cref="SynchronizedCollection&lt;T&gt;"/>은 null을 참조 형식에 대해 유효한 값으로 받아들이며 
        /// 중복 요소를 허용한다.
        /// </remarks>
        /// 
        /// <exception cref="ArgumentOutOfRangeException">index가 0보다 작거나 컬렉션의 요소 수보다 큰 경우</exception>
        public T this[int index]
        {
            get
            {
                lock (m_syncObject)
                {
                    return m_data[index];
                } // lock
            } // get
            set
            {
                lock (m_syncObject)
                {
                    m_data[index] = value;
                } // lock
            } // set
        }

        #endregion

        #region ICollection<T> 멤버

        /// <summary>
        /// 요소를 <see cref="SynchronizedCollection&lt;T&gt;"/>에 추가한다.
        /// </summary>
        /// <param name="item">
        /// <see cref="SynchronizedCollection&lt;T&gt;"/>에 추가할 요소. 참조 형식에 대해 값은 null이 될 수 있다.
        /// </param>
        /// <remarks>
        /// <see cref="SynchronizedCollection&lt;T&gt;"/>은 null을 참조 형식에 대해 유효한 값으로 받아들이며 
        /// 중복 요소를 허용한다.
        /// </remarks>
        public void Add(T item)
        {
            lock (m_syncObject)
            {
                m_data.Add(item);
            } // lock
        }

        /// <summary>
        /// <see cref="SynchronizedCollection&lt;T&gt;"/>에서 요소를 모두 제거한다.
        /// </summary>
        public void Clear()
        {
            lock (m_syncObject)
            {
                m_data.Clear();
            } // lock
        }

        /// <summary>
        /// 요소가 <see cref="SynchronizedCollection&lt;T&gt;"/>에 있는지 여부를 확인한다.
        /// </summary>
        /// <param name="item">
        /// <see cref="SynchronizedCollection&lt;T&gt;"/>에서 찾을 요소. 참조 형식에 대해 값은 null이 될 수 있다.
        /// </param>
        /// <returns>요소가 <see cref="SynchronizedCollection&lt;T&gt;"/>에 있으면 true, 그렇지 않으면 false</returns>
        public bool Contains(T item)
        {
            lock (m_syncObject)
            {
                return m_data.Contains(item);
            } // lock
        }

        /// <summary>
        /// 대상 배열의 지정한 인덱스에서 시작하여 전체 <see cref="SynchronizedCollection&lt;T&gt;"/>을 
        /// 호환되는 1차원 <see cref="Array"/>에 복사한다.
        /// </summary>
        /// <param name="array">
        /// <see cref="SynchronizedCollection&lt;T&gt;"/>에서 복사한 요소의 대상인 1차원 <see cref="Array"/>.
        /// <see cref="Array"/>의 인덱스는 0부터 시작해야 한다.
        /// </param>
        /// <param name="index">array에서 복사가 시작되는 인덱스(0부터 시작)</param>
        /// 
        /// <exception cref="ArgumentNullException">array가 null인 경우</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// index가 array의 길이보다 크거나 같은 경우<br/>
        /// - 또는 - <br/>
        /// index가 0보다 작은 경우
        /// </exception>
        /// <exception cref="ArgumentException">
        /// 소스 <see cref="SynchronizedCollection&lt;T&gt;"/>의 요소 수가 
        /// index에서 대상 array 끝까지 사용 가능한 공간보다 큰 경우
        /// </exception>
        public void CopyTo(T[] array, int index)
        {
            lock (m_syncObject)
            {
                m_data.CopyTo(array, index);
            } // lock
        }

        /// <summary>
        /// <see cref="SynchronizedCollection&lt;T&gt;"/>에 포함된 요소의 수를 가져온다.
        /// </summary>
        /// <value><see cref="SynchronizedCollection&lt;T&gt;"/>에 포함된 요소의 수</value>
        public int Count
        {
            get
            {
                lock (m_syncObject)
                {
                    return m_data.Count;
                } // lock
            } // get
        }

        /// <summary>
        /// <see cref="ICollection&lt;T&gt;"/>이 읽기 전용인지 여부를 나타내는 값을 가져온다.
        /// </summary>
        /// <value>
        /// <see cref="ICollection&lt;T&gt;"/>이 읽기 전용이면 true, 그렇지 않으면 false. 
        /// <see cref="SynchronizedCollection&lt;T&gt;"/>의 기본 구현에서 이 속성은 항상 false를 반환한다.
        /// </value>
        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// <see cref="SynchronizedCollection&lt;T&gt;"/>에서 맨 처음 발견되는 특정 요소를 제거한다.
        /// </summary>
        /// <param name="item"><see cref="SynchronizedCollection&lt;T&gt;"/>에서 제거할 요소</param>
        /// <returns>
        /// 요소가 성공적으로 제거되면 true, 그렇지 않으면 false. 
        /// item이 <see cref="SynchronizedCollection&lt;T&gt;"/>에 없는 경우에도 false를 반환한다.
        /// </returns>
        public bool Remove(T item)
        {
            lock (m_syncObject)
            {
                return m_data.Remove(item);
            } // lock
        }

        #endregion

        #region IEnumerable<T> 멤버

        /// <summary>
        /// <see cref="SynchronizedCollection&lt;T&gt;"/>을 반복하는 열거자를 반환한다.
        /// </summary>
        /// <returns><see cref="SynchronizedCollection&lt;T&gt;"/>에 대한 <see cref="IEnumerator&lt;T&gt;"/></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return (new SynchronizedEnumerator<T>(m_syncObject, m_data.GetEnumerator()));
        }

        #endregion

        #region IList 멤버

        /// <summary>
        /// <see cref="IList"/>에 요소를 추가한다.
        /// </summary>
        /// <param name="value"><see cref="IList"/>에 추가할 개체</param>
        /// <returns>새 요소가 추가된 위치</returns>
        /// 
        /// <exception cref="ArgumentException">value의 형식은 <see cref="IList"/>에 할당할 수 없다.</exception>
        int IList.Add(object value)
        {
            lock (m_syncObject)
            {
                return ((IList)m_data).Add(value);
            } // lock
        }

        /// <summary>
        /// <see cref="IList"/>에 특정 요소가 있는지 여부를 확인한다.
        /// </summary>
        /// <param name="value"><see cref="IList"/>에서 찾을 개체</param>
        /// <returns>개체가 <see cref="IList"/>에 있으면 true, 그렇지 않으면 false</returns>
        /// 
        /// <exception cref="ArgumentException">value의 형식은 <see cref="IList"/>에 할당할 수 없다.</exception>
        bool IList.Contains(object value)
        {
            lock (m_syncObject)
            {
                return ((IList)m_data).Contains(value);
            } // lock
        }

        /// <summary>
        /// <see cref="IList"/>에서 특정 요소의 인덱스를 확인한다.
        /// </summary>
        /// <param name="value"><see cref="IList"/>에서 찾을 개체</param>
        /// <returns>개체가 <see cref="IList"/>에 있으면 요소의 인덱스, 그렇지 않으면 -1</returns>
        /// 
        /// <exception cref="ArgumentException">value의 형식은 <see cref="IList"/>에 할당할 수 없다.</exception>
        int IList.IndexOf(object value)
        {
            lock (m_syncObject)
            {
                return ((IList)m_data).IndexOf(value);
            } // lock
        }

        /// <summary>
        /// 요소를 <see cref="IList"/>의 지정한 인덱스에 삽입한다.
        /// </summary>
        /// <param name="index">요소를 삽입할 인덱스(0부터 시작)</param>
        /// <param name="value"><see cref="IList"/>에 삽입할 개체</param>
        /// 
        /// <exception cref="ArgumentOutOfRangeException">
        /// index가 <see cref="IList"/>의 유효한 인덱스가 아닌 경우
        /// </exception>
        /// <exception cref="ArgumentException">value의 형식은 <see cref="IList"/>에 할당할 수 없다.</exception>
        void IList.Insert(int index, object value)
        {
            lock (m_syncObject)
            {
                ((IList)m_data).Insert(index, value);
            } // lock
        }

        /// <summary>
        /// <see cref="IList"/>의 크기가 고정되어 있는지 여부를 나타내는 값을 가져온다.
        /// </summary>
        /// <value>
        /// <see cref="IList"/>의 크기가 고정되어 있으면 true, 그렇지 않으면 false.
        /// <see cref="SynchronizedCollection&lt;T&gt;"/>의 기본 구현에서 이 속성은 항상 false를 반환한다.
        /// </value>
        bool IList.IsFixedSize
        {
            get { return false; }
        }

        /// <summary>
        /// <see cref="IList"/>가 읽기 전용인지 여부를 나타내는 값을 가져온다.
        /// </summary>
        /// <value>
        /// <see cref="IList"/>가 읽기 전용이면 true, 그렇지 않으면 false.
        /// <see cref="SynchronizedCollection&lt;T&gt;"/>의 기본 구현에서 이 속성은 항상 false를 반환한다.
        /// </value>
        bool IList.IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// <see cref="IList"/>에서 맨 처음 발견되는 특정 요소를 제거한다.
        /// </summary>
        /// <param name="value"><see cref="IList"/>에서 제거할 개체</param>
        /// 
        /// <exception cref="ArgumentException">value의 형식은 <see cref="IList"/>에 할당할 수 없다.</exception>
        void IList.Remove(object value)
        {
            lock (m_syncObject)
            {
                ((IList)m_data).Remove(value);
            } // lock
        }

        /// <summary>
        /// 지정한 인덱스에 있는 요소를 가져오거나 설정한다.
        /// </summary>
        /// <param name="index">가져오거나 설정할 요소의 인덱스(0부터 시작)</param>
        /// <returns>지정한 인덱스의 요소</returns>
        /// 
        /// <exception cref="ArgumentOutOfRangeException">
        /// index가 <see cref="IList"/>의 유효한 인덱스가 아닌 경우
        /// </exception>
        /// <exception cref="ArgumentException">
        /// 속성이 설정되어 있고 value의 형식을 <see cref="IList"/>에 할당할 수 없는 경우
        /// </exception>
        object IList.this[int index]
        {
            get
            {
                lock (m_syncObject)
                {
                    return ((IList)m_data)[index];
                } // lock
            } // get
            set
            {
                lock (m_syncObject)
                {
                    ((IList)m_data)[index] = value;
                } // lock
            } // set
        }

        #endregion

        #region ICollection 멤버

        /// <summary>
        /// 특정 <see cref="Array"/> 인덱스부터 시작하여 
        /// <see cref="ICollection"/>의 요소를 <see cref="Array"/>에 복사한다.
        /// </summary>
        /// <param name="array">
        /// <see cref="ICollection"/>에서 복사한 요소의 대상인 1차원 <see cref="Array"/>. 
        /// <see cref="Array"/>의 인덱스는 0부터 시작해야 한다.
        /// </param>
        /// <param name="index">array에서 복사가 시작되는 인덱스(0부터 시작)</param>
        /// 
        /// <exception cref="ArgumentNullException">array가 null인 경우</exception>
        /// <exception cref="ArgumentOutOfRangeException">index가 0보다 작은 경우</exception>
        /// <exception cref="ArgumentException">
        /// array가 다차원 배열인 경우<br/>
        /// - 또는 -<br/>
        /// array에 0부터 시작하는 인덱스가 없다.<br/>
        /// - 또는 -<br/>
        /// index가 array의 길이보다 크거나 같은 경우<br/>
        /// - 또는 -<br/>
        /// 소스 <see cref="ICollection"/>의 요소 수가 index에서 대상 array 끝까지 사용 가능한 공간보다 큰 경우<br/>
        /// - 또는 -<br/>
        /// 소스 <see cref="ICollection"/> 형식을 대상 array 형식으로 자동 캐스팅할 수 없는 경우
        /// </exception>
        void ICollection.CopyTo(Array array, int index)
        {
            lock (m_syncObject)
            {
                ((ICollection)m_data).CopyTo(array, index);
            } // lock
        }

        /// <summary>
        /// <see cref="ICollection"/>에 대한 액세스가 동기화되어 
        /// 스레드로부터 안전하게 보호되는지 여부를 나타내는 값을 가져온다.
        /// </summary>
        /// <value>
        /// <see cref="ICollection"/>에 대한 액세스가 동기화되어 스레드로부터 안전하게 보호되면 true, 그렇지 않으면 false.
        /// <see cref="SynchronizedCollection&lt;T&gt;"/>의 기본 구현에서 이 속성은 항상 true를 반환한다.
        /// </value>
        bool ICollection.IsSynchronized
        {
            get { return true; }
        }

        /// <summary>
        /// <see cref="ICollection"/>에 대한 액세스를 동기화하는데 사용할 수 있는 개체를 가져온다.
        /// </summary>
        /// <value><see cref="ICollection"/>에 대한 액세스를 동기화하는데 사용할 수 있는 개체</value>
        object ICollection.SyncRoot
        {
            get { return m_syncObject; }
        }

        #endregion

        #region IEnumerable 멤버

        /// <summary>
        /// 컬렉션을 반복하는 열거자를 반환한다.
        /// </summary>
        /// <returns>컬렉션에서 반복하는 데 사용할 수 있는 <see cref="IEnumerator"/></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
