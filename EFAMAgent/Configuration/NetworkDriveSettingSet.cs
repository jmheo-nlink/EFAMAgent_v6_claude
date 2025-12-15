#region 변경 이력
/*
 * Author : Link mskoo (2012. 2. 11)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2012-02-11   mskoo           최초 작성.
 * 
 * 2012-02-26   mskoo           생성자 추가.
 *                              - NetworkDriveSettingSet(IList<NetworkDriveSetting>)
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace Link.EFAM.Agent.Configuration
{
    /// <summary>
    /// <see cref="NetworkDriveSetting"/> 개체와 관련 공유 네트워크 폴더의 UNC 이름의 집합을 나타낸다.
    /// </summary>
    class NetworkDriveSettingSet : Dictionary<string, NetworkDriveSetting>
    {
        #region 속성

        /// <summary>
        /// 지정한 키와 연결된 값을 가져오거나 설정한다.
        /// </summary>
        /// <param name="key">가져오거나 설정할 값의 키</param>
        /// <returns>
        /// 지정한 키와 연결된 값.<br/>
        /// 지정한 키가 없으면 get 작업에서 null을 반환하고 set 작업에서 지정한 키가 있는 새 요소를 만든다.
        /// </returns>
        public new NetworkDriveSetting this[string key]
        {
            get
            {
                NetworkDriveSetting setting = null;

                return (TryGetValue(key, out setting) ? setting : null);
            }
            set
            {
                base[key] = value;
            }
        }

        #endregion

        #region 생성자

        /// <summary>
        /// <see cref="NetworkDriveSettingSet"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        public NetworkDriveSettingSet()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        /// <summary>
        /// <see cref="NetworkDriveSettingSet"/> 클래스의 새 인스턴스를 지정한 목록의 래퍼로 초기화한다.
        /// </summary>
        /// <param name="list">래핑하는 목록</param>
        /// 
        /// <exception cref="ArgumentNullException">list가 null인 경우</exception>
        public NetworkDriveSettingSet(IList<NetworkDriveSetting> list)
            : this()
        {
            if (list == null) throw new ArgumentNullException("list");

            foreach (NetworkDriveSetting setting in list)
            {
                base[setting.SharedFolder] = setting;
            }
        }

        #endregion
    }
}
