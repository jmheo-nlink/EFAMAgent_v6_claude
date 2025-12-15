#region 변경 이력
/*
 * Author : Link mskoo (2011. 6. 18)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-06-18   mskoo           최초 작성.
 * 
 * 2011-06-22   mskoo           메소드 추가.
 *                              - ToArray()
 *                              
 * 2011-07-03   mskoo           1.0 버전 릴리즈.
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Link.DLK.Collections
{
    /// <summary>
    /// 문자열의 컬렉션을 나타냅니다.<br/>
    /// 문자열을 비교할 때 대/소문자를 구분하지 않는다.
    /// </summary>
    public class StringCollection 
        : ICollection<string>, IEnumerable<string>, ICollection, IEnumerable
    {
        private Collection<string> m_keyColl = null;
        private Collection<string> m_valueColl = null;
        private object m_syncObject = null;

        #region 속성

        /// <summary>
        /// 지정한 인덱스에 있는 요소를 가져오거나 설정합니다.
        /// </summary>
        /// <param name="index">가져오거나 설정할 엔트리 인덱스(0부터 시작)</param>
        /// <value>지정한 인덱스의 요소</value>
        /// 
        /// <exception cref="ArgumentOutOfRangeException">
        /// index가 0보다 작은 경우<br/>
        /// - 또는 -<br/>
        /// index가 <see cref="Count"/>보다 크거나 같은 경우
        /// </exception>
        public string this[int index]
        {
            get { return m_valueColl[index]; }
            set
            {
                m_valueColl[index] = value;
                m_keyColl[index] = GetKey(value);
            }
        }

        #endregion

        #region 생성자

        /// <summary>
        /// <see cref="StringCollection"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        public StringCollection()
        {
            m_keyColl = new Collection<string>();
            m_valueColl = new Collection<string>();
            m_syncObject = ((ICollection)m_valueColl).SyncRoot;
        }

        #endregion

        #region 메소드

        /// <summary>
        /// 문자열 배열의 요소를 <see cref="StringCollection"/>의 끝에 복사합니다.
        /// </summary>
        /// <param name="value"><see cref="StringCollection"/>의 끝에 추가할 문자열의 배열</param>
        /// 
        /// <exception cref="ArgumentNullException">value가 null인 경우</exception>
        public void AddRange(string[] value)
        {
            if (value == null) throw new ArgumentNullException("value");

            foreach (string item in value)
            {
                if (item != null) Add(item);
            }
        }

        /// <summary>
        /// 지정한 문자열을 검색하고 <see cref="StringCollection"/>에서 나타나는 인덱스를 반환한다.
        /// </summary>
        /// <param name="value">찾을 문자열</param>
        /// <returns>
        /// <see cref="StringCollection"/>에서 나타나는 value가 있으면 해당 인덱스(0부터 시작), 그렇지 않으면 -1
        /// </returns>
        public int IndexOf(string value)
        {
            return m_keyColl.IndexOf(GetKey(value));
        }
        
        /// <summary>
        /// <see cref="StringCollection"/>의 지정한 인덱스에 있는 문자열을 제거한다.
        /// </summary>
        /// <param name="index">제거할 문자열의 인덱스(0부터 시작)</param>
        /// 
        /// <exception cref="ArgumentOutOfRangeException">
        /// index가 0보다 작은 경우<br/>
        /// - 또는 -<br/>
        /// index가 <see cref="Count"/>보다 크거나 같은 경우
        /// </exception>
        public void RemoveAt(int index)
        {
            m_valueColl.RemoveAt(index);
            m_keyColl.RemoveAt(index);
        }

        /// <summary>
        /// <see cref="StringCollection"/>의 요소를 새 배열에 복사한다.
        /// </summary>
        /// <returns><see cref="StringCollection"/>에서 복사된 요소를 포함하는 새 배열</returns>
        public string[] ToArray()
        {
            string[] values = new string[m_valueColl.Count];

            m_valueColl.CopyTo(values, 0);

            return values;
        }

        /// <summary>
        /// 지정한 값에 해당하는 키를 가져온다.
        /// </summary>
        /// <param name="value">키를 가져올 값</param>
        /// <returns>관련된 키이거나, 키가 없으면 null</returns>
        private string GetKey(string value)
        {
            return ((value != null) ? value.ToLower() : null);
        }

        #endregion

        #region ICollection<T> 멤버
        #region 속성

        /// <summary>
        /// <see cref="StringCollection"/>에 포함된 문자열 수를 가져온다.
        /// </summary>
        /// <value><see cref="StringCollection"/>에 포함된 문자열 수</value>
        public int Count
        {
            get { return m_valueColl.Count; }
        }

        /// <summary>
        /// <see cref="StringCollection"/>가 읽기 전용인지 여부를 나타내는 값을 가져온다.
        /// </summary>
        /// <value>이 속성은 항상 false를 반환</value>
        public bool IsReadOnly
        {
            get { return false; }
        }

        #endregion

        #region 메소드

        /// <summary>
        /// 문자열을 <see cref="StringCollection"/>의 끝에 추가한다.
        /// </summary>
        /// <param name="value"><see cref="StringCollection"/>의 끝에 추가할 문자열</param>
        /// <remarks>
        /// 지정한 문자열이 <see cref="StringCollection"/>에 이미 있는 경우 기존 문자열을 덮어쓴다.
        /// </remarks>
        /// 
        /// <exception cref="ArgumentNullException">value가 null인 경우</exception>
        public void Add(string value)
        {
            if (value == null) throw new ArgumentNullException("value");

            string key = GetKey(value);
            int foundIndex = m_keyColl.IndexOf(key);

            // 기존 값을 덮어쓴다.
            if (foundIndex >= 0)
            {
                m_valueColl[foundIndex] = value;
            }
            // 새 값을 추가한다.
            else
            {
                m_valueColl.Add(value);
                m_keyColl.Add(key);
            }
        }

        /// <summary>
        /// <see cref="StringCollection"/>에서 문자열을 모두 제거한다.
        /// </summary>
        public void Clear()
        {
            m_valueColl.Clear();
            m_keyColl.Clear();
        }

        /// <summary>
        /// 지정한 문자열이 <see cref="StringCollection"/>에 있는지 여부를 확인한다.
        /// </summary>
        /// <param name="value"><see cref="StringCollection"/>에서 찾을 문자열</param>
        /// <returns>
        /// value가 <see cref="StringCollection"/>에 있으면 true, 그렇지 않으면 false
        /// </returns>
        public bool Contains(string value)
        {
            return m_keyColl.Contains(GetKey(value));
        }

        /// <summary>
        /// 전체 <see cref="StringCollection"/>를 지정한 배열 인덱스에서 시작하여 기존의 1차원 <see cref="Array"/>에 복사한다.
        /// </summary>
        /// <param name="array">
        /// <see cref="StringCollection"/>에서 복사한 요소의 대상인 1차원 <see cref="Array"/>.
        /// <see cref="Array"/>의 인덱스는 0부터 시작해야 한다.
        /// </param>
        /// <param name="index">array에서 복사가 시작되는 인덱스(0부터 시작)</param>
        /// 
        /// <exception cref="ArgumentNullException">array가 null인 경우</exception>
        /// <exception cref="ArgumentOutOfRangeException">index가 0보다 작은 경우</exception>
        /// <exception cref="ArgumentException">
        /// index가 array의 길이보다 크거나 같은 경우<br/>
        /// - 또는 -<br/>
        /// 소스 <see cref="StringCollection"/>의 요소 수가 index에서 대상 array 끝까지 사용 가능한 공간보다 큰 경우
        /// </exception>
        public void CopyTo(string[] array, int index)
        {
            m_valueColl.CopyTo(array, index);
        }

        /// <summary>
        /// <see cref="StringCollection"/>에서 지정한 문자열을 제거한다.
        /// </summary>
        /// <param name="value"><see cref="StringCollection"/>에서 제거할 문자열</param>
        /// <returns>
        /// 문자열을 성공적으로 찾아서 제거한 경우 true, 그렇지 않으면 false.
        /// value가 <see cref="StringCollection"/>에 없는 경우 false를 반환한다.
        /// </returns>
        public bool Remove(string value)
        {
            int foundIndex = m_keyColl.IndexOf(GetKey(value));
            bool removed = (foundIndex >= 0);

            if (removed)
            {
                m_valueColl.RemoveAt(foundIndex);
                m_keyColl.RemoveAt(foundIndex);
            }

            return removed;
        }

        #endregion
        #endregion

        #region IEnumerable<T> 멤버

        /// <summary>
        /// <see cref="StringCollection"/>을 반복하는 <see cref="IEnumerator&lt;T&gt;"/>를 반환한다.
        /// </summary>
        /// <returns><see cref="StringCollection"/>에 대한 <see cref="IEnumerator&lt;T&gt;"/></returns>
        public IEnumerator<string> GetEnumerator()
        {
            return m_valueColl.GetEnumerator();
        }

        #endregion

        #region ICollection 멤버
        #region 속성

        /// <summary>
        /// <see cref="StringCollection"/>에 대한 액세스가 동기화되어 
        /// 스레드로부터 안전하게 보호되는지 여부를 나타내는 값을 가져온다.
        /// </summary>
        /// <value>이 속성은 항상 false를 반환</value>
        public bool IsSynchronized
        {
            get { return false; }
        }

        /// <summary>
        /// <see cref="StringCollection"/>에 대한 액세스를 동기화하는 데 사용할 수 있는 개체를 가져온다.
        /// </summary>
        /// <value><see cref="StringCollection"/>에 대한 액세스를 동기화하는 데 사용할 수 있는 개체</value>
        public object SyncRoot
        {
            get { return m_syncObject; }
        }

        #endregion

        #region 메소드

        /// <summary>
        /// 대상 배열의 지정된 인덱스에서 시작하여 전체 <see cref="StringCollection"/>를 
        /// 호환되는 1차원 <see cref="Array"/>에 복사한다.
        /// </summary>
        /// <param name="array">
        /// <see cref="StringCollection"/>에서 복사한 요소의 대상인 1차원 <see cref="Array"/>.
        /// <see cref="Array"/>의 인덱스는 0부터 시작해야 한다.
        /// </param>
        /// <param name="index">array에서 복사가 시작되는 인덱스(0부터 시작)</param>
        /// 
        /// <exception cref="ArgumentNullException">array가 null인 경우</exception>
        /// <exception cref="ArgumentOutOfRangeException">index가 0보다 작은 경우</exception>
        /// <exception cref="ArgumentException">
        /// array가 다차원 배열인 경우<br/>
        /// - 또는 -<br/>
        /// index가 array의 길이보다 크거나 같은 경우<br/>
        /// - 또는 -<br/>
        /// 소스 <see cref="StringCollection"/>의 요소 수가 index에서 대상 array 끝까지 사용 가능한 공간보다 큰 경우
        /// </exception>
        /// <exception cref="ArgumentException">
        /// 소스 <see cref="StringCollection"/> 형식을 대상 array 형식으로 자동 캐스팅할 수 없는 경우
        /// </exception>
        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection)m_valueColl).CopyTo(array, index);
        }

        #endregion
        #endregion

        #region IEnumerable 멤버

        /// <summary>
        /// <see cref="StringCollection"/>을 반복하는 <see cref="IEnumerator"/>를 반환한다.
        /// </summary>
        /// <returns><see cref="StringCollection"/>에 대한 <see cref="IEnumerator"/></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_valueColl.GetEnumerator();
        }

        #endregion
    }
}
