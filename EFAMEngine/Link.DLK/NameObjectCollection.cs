#region 변경 이력
/*
 * Author : Link mskoo (2011. 4. 6)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-04-06   mskoo           최초 작성.
 * 
 * 2011-05-21   mskoo           같은 키가 있는 엔트리가 이미 컬렉션에 있으면 예외를 throw하도록 수정.
 *                              - Add(string, object)
 *                                  
 * 2011-05-31   mskoo           속성 제거.
 *                              - SyncRoot
 *                              
 * 2011-07-03   mskoo           1.0 버전 릴리즈.
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections;
using System.Collections.Specialized;

//using Link.DLK.Properties;

namespace Link.DLK.Collections
{
    using Resources = Link.EFAM.Engine.Properties.Resources;

    /// <summary>
    /// 키를 사용하여 액세스할 수 있는 String 키와 Object 값의 컬렉션을 나타낸다.<br/>
    /// 기본 비교자는 <see cref="CaseInsensitiveComparer"/>로 두 키가 같은지 여부를 확인한다.
    /// (문자열의 대/소문자를 무시하고 두 개체를 비교)
    /// </summary>
    public class NameObjectCollection : NameObjectCollectionBase
    {
        #region 속성

        /// <summary>
        /// 컬렉션에서 지정한 키를 가지는 엔트리 값을 가져오거나 설정한다.
        /// </summary>
        /// <param name="name">가져오거나 설정할 엔트리 값이 있는 키</param>
        /// <value>
        /// 지정한 키를 가지는 엔트리 값.<br/>
        /// 지정한 키가 없는 경우 해당 키를 가져오려고 시도하면 null이 반환되고,
        /// 해당 키를 설정하려고 시도하면 지정한 키와 값을 가지는 새 엔트리가 추가된다.
        /// </value>
        /// 
        /// <exception cref="ArgumentNullException">name이 null인 경우</exception>
        /// <exception cref="NotSupportedException">컬렉션이 읽기 전용인 경우</exception>
        public object this[string name]
        {
            get
            {
                if (name == null) throw new ArgumentNullException("name");

                return this.BaseGet(name);
            } // get
            set
            {
                if (name == null) throw new ArgumentNullException("name");

                this.BaseSet(name, value);
            } // set
        }

        /// <summary>
        /// 컬렉션의 모든 값을 포함하는 <see cref="Object"/> 배열을 가져온다.
        /// </summary>
        /// <value>컬렉션의 모든 값을 포함하는 <see cref="Object"/> 배열</value>
        public object[] Values
        {
            get { return this.BaseGetAllValues(); }
        }

        #endregion 속성

        #region 생성자

        /// <summary>
        /// 기본 초기 용량을 갖고 있는 <see cref="NameObjectCollection"/> 클래스의 비어 있는 새 인스턴스를 초기화한다.
        /// </summary>
        public NameObjectCollection()
        {
        }

        /// <summary>
        /// 지정한 초기 용량을 갖고 있는 <see cref="NameObjectCollection"/> 클래스의 비어 있는 새 인스턴스를 초기화한다.
        /// </summary>
        /// <param name="capacity">
        /// <see cref="NameObjectCollection"/> 개체가 처음에 포함할 수 있는 대략적인 엔트리 수
        /// </param>
        /// 
        /// <exception cref="ArgumentOutOfRangeException">capacity가 0보다 작은 경우</exception>
        public NameObjectCollection(int capacity)
            : base(capacity)
        {
        }

        /// <summary>
        /// 지정한 사전의 엔트리를 새 <see cref="NameObjectCollection"/> 개체에 복사하여 
        /// <see cref="NameObjectCollection"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        /// <param name="dic">새 <see cref="NameObjectCollection"/> 개체로 복사할 <see cref="IDictionary"/> 개체</param>
        /// <param name="readOnly">읽기 전용 컬렉션으로 만들려면 true, 그렇지 않으면 false</param>
        /// 
        /// <exception cref="ArgumentNullException">dic가 null인 경우</exception>
        public NameObjectCollection(IDictionary dic, bool readOnly)
            : base(dic.Count)
        {
            if (dic == null) throw new ArgumentNullException("dic");

            string key = null;

            foreach (DictionaryEntry entry in dic)
            {
                key = entry.Key as string;
                if (key != null) this.BaseAdd(key, entry.Value);
            }

            this.IsReadOnly = readOnly;
        }

        #endregion

        #region 메소드

        /// <summary>
        /// 지정한 키와 값을 가지는 엔트리를 컬렉션에 추가한다.
        /// </summary>
        /// <param name="name">추가할 엔트리의 키</param>
        /// <param name="value">추가할 엔트리의 값</param>
        /// 
        /// <exception cref="ArgumentNullException">name이 null인 경우</exception>
        /// <exception cref="ArgumentException">
        /// 같은 키가 있는 엔트리가 이미 <see cref="NameObjectCollection"/> 개체에 있는 경우
        /// </exception>
        /// <exception cref="NotSupportedException">컬렉션이 읽기 전용인 경우</exception>
        public void Add(string name, object value)
        {
            if (name == null) throw new ArgumentNullException("name");

            if (this.BaseGet(name) != null)
            {
                throw new ArgumentException(Resources.Error_AlreadyAddedInCollection, "name");
            }

            this.BaseAdd(name, value);
        }

        /// <summary>
        /// 컬렉션에서 모든 엔트리를 제거한다.
        /// </summary>
        /// 
        /// <exception cref="NotSupportedException">컬렉션이 읽기 전용인 경우</exception>
        public void Clear()
        {
            this.BaseClear();
        }

        /// <summary>
        /// 컬렉션에서 지정한 키를 가지는 엔트리 값을 가져온다.
        /// </summary>
        /// <param name="name">가져올 엔트리의 키</param>
        /// <returns>지정한 키가 있는 엔트리 값, 지정한 키가 없는 경우 null</returns>
        /// 
        /// <exception cref="ArgumentNullException">name이 null인 경우</exception>
        public object Get(string name)
        {
            if (name == null) throw new ArgumentNullException("name");

            return this.BaseGet(name);
        }

        /// <summary>
        /// 컬렉션에서 지정한 키를 가지는 엔트리를 제거한다.
        /// </summary>
        /// <param name="name">제거할 엔트리의 키</param>
        /// 
        /// <exception cref="NotSupportedException">컬렉션이 읽기 전용인 경우</exception>
        public void Remove(string name)
        {
            this.BaseRemove(name);
        }

        /// <summary>
        /// 지정한 키를 가지는 엔트리가 컬렉션에 있으면 엔트리의 값을 설정하고,
        /// 그렇지 않으면 지정한 키와 값을 가지는 엔트리를 컬렉션에 추가한다.
        /// </summary>
        /// <param name="name">설정할 엔트리의 키</param>
        /// <param name="value">설정할 엔트리의 새 값</param>
        /// 
        /// <exception cref="ArgumentNullException">name이 null인 경우</exception>
        /// <exception cref="NotSupportedException">컬렉션이 읽기 전용인 경우</exception>
        public void Set(string name, object value)
        {
            if (name == null) throw new ArgumentNullException("name");

            this.BaseSet(name, value);
        }

        #endregion

        #region NameObjectCollectionBase 멤버

        /// <summary>
        /// 컬렉션의 모든 키를 포함하는 <see cref="String"/> 배열을 가져온다.
        /// </summary>
        /// <value>컬렉션의 모든 키를 포함하는 <see cref="String"/> 배열</value>
        public new string[] Keys
        {
            get { return this.BaseGetAllKeys(); }
        }

        #endregion
    }
}
