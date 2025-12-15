#region 변경 이력
/*
 * Author : Link mskoo (2011. 4. 11)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-04-11   mskoo           최초 작성.
 * 
 * 2011-04-15   mskoo           메소드 추가.
 *                              - BaseClear()
 * 
 * 2011-05-31   mskoo           동기화 개체로 멤버 변수를 사용하도록 수정.
 * 
 * 2011-06-03   mskoo           내부 데이터를 저장하는 클래스를 Dictionary(TKey, TValue) 클래스로 변경.
 * 
 * 2011-06-05   mskoo           ICollection, IEnumerable 인터페이스를 구현.
 * 
 * 2011-07-03   mskoo           1.0 버전 릴리즈.
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections;
using System.Collections.Generic;

using Link.DLK.Collections;
//using Link.DLK.Properties;

namespace Link.DLK.Caching
{
    using Resources = Link.EFAM.Engine.Properties.Resources;

    /// <summary>
    /// 키를 사용하여 액세스할 수 있는 캐시에 대한 abstract 기본 클래스를 제공한다.<br/>
    /// 비교자는 <see cref="StringComparer.OrdinalIgnoreCase"/>로 두 키가 같은지 여부를 확인한다.
    /// (문자열의 대/소문자를 무시하고 두 개체를 비교)
    /// </summary>
    public abstract class CacheBase : ICollection, IEnumerable
    {
        private Dictionary<string, ICacheEntry> m_data = null;
        private TimeSpan m_duration = TimeSpan.Zero;
        private object m_syncObject = null;

        #region 속성

        /// <summary>
        /// <see cref="CacheBase"/> 인스턴스에 캐시된 항목을 보관해야 하는 기간을 가져오거나 설정한다.
        /// </summary>
        /// <value>항목을 <see cref="CacheBase"/> 인스턴스에 보관할 시간을 나타내는 <see cref="TimeSpan"/></value>
        /// 
        /// <exception cref="ArgumentOutOfRangeException">
        /// 속성 값이 <see cref="TimeSpan.Zero"/> 보다 작은 경우
        /// </exception>
        public virtual TimeSpan CacheDuration
        {
            get { return m_duration; }
            set
            {
                if (value < TimeSpan.Zero)
                {
                    throw new ArgumentOutOfRangeException("value",
                        String.Format(Resources.Error_OutOfRangeProperty_GtOrEq, "CacheDuration", "TimeSpan.Zero"));
                }

                m_duration = value;
            } // get
        }

        #endregion

        #region 생성자

        /// <summary>
        /// 기본 초기 용량을 갖고 있는 <see cref="CacheBase"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        protected CacheBase()
            : this(128)
        {
        }

        /// <summary>
        /// 지정한 초기 용량을 갖고 있는 <see cref="CacheBase"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        /// <param name="capacity"><see cref="CacheBase"/> 개체가 처음에 포함할 수 있는 대략적인 엔트리 수</param>
        /// 
        /// <exception cref="ArgumentOutOfRangeException">capacity가 0보다 작은 경우</exception>
        protected CacheBase(int capacity)
        {
            //m_data = new Dictionary<string, ICacheEntry>(capacity, new CaseInsensitiveEqualityComparer());
            m_data = new Dictionary<string, ICacheEntry>(capacity, StringComparer.OrdinalIgnoreCase);
            m_syncObject = ((ICollection)m_data).SyncRoot;
        }

        #endregion

        #region 메소드

        /// <summary>
        /// 지정한 항목을 <see cref="CacheBase"/> 인스턴스에 추가한다.
        /// </summary>
        /// <param name="entry">캐시에 추가되는 항목</param>
        /// <remarks>
        /// 캐시 키가 일치하는 기존 캐시된 항목을 덮어쓴다.
        /// </remarks>
        /// 
        /// <exception cref="ArgumentNullException">
        /// entry가 null인 경우<br/>
        /// - 또는 -<br/>
        /// 캐시 항목의 캐시 키가 null인 경우
        /// </exception>
        protected void BaseAdd(ICacheEntry entry)
        {
            if (entry == null) throw new ArgumentNullException("entry");

            m_data[entry.Key] = entry;
        }

        /// <summary>
        /// <see cref="CacheBase"/> 인스턴스에서 지정한 항목을 검색한다.
        /// </summary>
        /// <param name="key">검색할 캐시 항목에 대한 캐시 키</param>
        /// <returns>검색된 캐시 항목. 키를 찾지 못한 경우에는 null</returns>
        /// <remarks>
        /// 만료된 캐시 항목은 반환하지 않는다.
        /// </remarks>
        /// 
        /// <exception cref="ArgumentNullException">key가 null인 경우</exception>
        protected ICacheEntry BaseGet(string key)
        {
            if (key == null) throw new ArgumentNullException("key");

            ICacheEntry foundEntry = null;

            m_data.TryGetValue(key, out foundEntry);
            if (foundEntry != null && foundEntry.ExpireDate <= DateTime.Now)
            {
                m_data.Remove(key);
                foundEntry = null;
            }

            return foundEntry;
        }

        /// <summary>
        /// <see cref="CacheBase"/> 인스턴스에서 지정한 항목을 제거한다.
        /// </summary>
        /// <param name="key">제거할 캐시 항목에 대한 캐시 키</param>
        /// <returns><see cref="CacheBase"/> 인스턴스에서 제거되는 항목</returns>
        /// 
        /// <exception cref="ArgumentNullException">key가 null인 경우</exception>
        protected ICacheEntry BaseRemove(string key)
        {
            if (key == null) throw new ArgumentNullException("key");

            ICacheEntry removedEntry = null;

            if (m_data.TryGetValue(key, out removedEntry))
            {
                m_data.Remove(key);
            }

            return removedEntry;
        }

        /// <summary>
        /// <see cref="CacheBase"/> 인스턴스에서 모든 항목을 제거한다.
        /// </summary>
        protected void BaseClear()
        {
            m_data.Clear();
        }

        /// <summary>
        /// <see cref="CacheBase"/> 인스턴스에서 만료된 캐시 항목들을 제거한다.
        /// </summary>
        protected void ClearExpiredCaches()
        {
            List<string> removeKeyList = new List<string>(m_data.Count);
            DateTime dtNow = DateTime.Now;

            foreach (ICacheEntry entry in m_data.Values)
            {
                if (entry.ExpireDate <= dtNow)
                {
                    removeKeyList.Add(entry.Key);
                }
            } // foreach ( ICacheEntry )

            foreach (string key in removeKeyList)
            {
                m_data.Remove(key);
            }
        }

        #endregion

        #region ICollection 멤버
        #region 속성

        /// <summary>
        /// <see cref="CacheBase"/> 인스턴스에 포함된 항목의 수를 가져온다.
        /// </summary>
        /// <value><see cref="CacheBase"/> 인스턴스에 포함된 항목의 수</value>
        public int Count
        {
            get { return m_data.Count; }
        }

        /// <summary>
        /// <see cref="CacheBase"/> 개체에 대한 액세스가 동기화되어
        /// 스레드로부터 안전하게 보호되는지 여부를 나타내는 값을 가져온다.
        /// </summary>
        /// <value>
        /// <see cref="CacheBase"/> 개체에 대한 액세스가 동기화되어
        /// 스레드로부터 안전하게 보호되면 true, 그렇지 않으면 false. 기본값은 false
        /// </value>
        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        /// <summary>
        /// <see cref="CacheBase"/> 개체에 대한 액세스를 동기화하는 데 사용할 수 있는 개체를 가져온다.
        /// </summary>
        /// <value><see cref="CacheBase"/> 개체에 대한 액세스를 동기화하는 데 사용할 수 있는 개체</value>
        object ICollection.SyncRoot
        {
            get { return m_syncObject; }
        }

        #endregion

        #region 메소드

        /// <summary>
        /// 지정한 인덱스에서 1차원 <see cref="Array"/>에 캐시 항목을 복사한다.
        /// </summary>
        /// <param name="array">
        /// <see cref="CacheBase"/> 인스턴스에서 복사한 캐시 항목의 대상인 1차원 <see cref="Array"/>.
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
        /// 소스 <see cref="CacheBase"/>의 캐시 항목 수가 index에서 대상 array 끝까지 사용 가능한 공간보다 큰 경우
        /// </exception>
        /// <exception cref="ArgumentException">
        /// 소스 <see cref="CacheBase"/>의 캐시 항목 형식을 대상 array 형식으로 자동 캐스팅할 수 없는 경우
        /// </exception>
        public void CopyTo(Array array, int index)
        {
            ((ICollection)m_data.Values).CopyTo(array, index);
        }

        #endregion
        #endregion

        #region IEnumerable 멤버

        /// <summary>
        /// <see cref="CacheBase"/> 인스턴스를 반복하는 열거자를 반환한다.
        /// </summary>
        /// <returns><see cref="CacheBase"/> 인스턴스의 <see cref="IEnumerator"/></returns>
        /// <remarks>
        /// 이 열거자는 <see cref="CacheBase"/> 인스턴스의 캐시 항목을 반환한다.
        /// </remarks>
        public virtual IEnumerator GetEnumerator()
        {
            return m_data.Values.GetEnumerator();
        }

        #endregion
    }
}
