#region 변경 이력
/*
 * Author : Link mskoo (2012. 2. 11)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2012-02-11   mskoo           최초 작성.
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace Link.EFAM.Agent.Configuration
{
    /// <summary>
    /// 네트워크 드라이브 설정을 나타낸다.
    /// </summary>
    class NetworkDriveSetting
    {
        private bool m_use = true;
        private string m_driveName = String.Empty;
        private string m_sharedFolder = String.Empty;

        #region 속성

        /// <summary>
        /// 네트워크 드라이브를 사용하는지 여부를 나타내는 값을 가져오거나 설정한다.
        /// </summary>
        /// <value>네트워크 드라이브를 사용하면 true, 그렇지 않으면 false</value>
        public bool UseDrive
        {
            get { return m_use; }
            set { m_use = value; }
        }

        /// <summary>
        /// 드라이브 이름을 가져오거나 설정한다.
        /// </summary>
        /// <value>드라이브 이름</value>
        public string DriveName
        {
            get { return m_driveName; }
            set
            {
                m_driveName = (value != null) ? value : String.Empty;
            }
        }

        /// <summary>
        /// 공유 네트워크 폴더의 UNC 이름을 가져오거나 설정한다.
        /// </summary>
        /// <value>공유 네트워크 폴더의 UNC 이름</value>
        public string SharedFolder
        {
            get { return m_sharedFolder; }
            set
            {
                m_sharedFolder = (value != null) ? value : String.Empty;
            }
        }

        #endregion

        #region 생성자

        /// <summary>
        /// <see cref="NetworkDriveSetting"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        public NetworkDriveSetting()
        {
        }

        /// <summary>
        /// 지정한 값들을 사용하여 <see cref="NetworkDriveSetting"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        /// <param name="useDrive">네트워크 드라이브를 사용하면 true, 그렇지 않으면 false</param>
        /// <param name="driveName">드라이브 이름</param>
        /// <param name="sharedFolder">공유 네트워크 폴더의 UNC 이름</param>
        public NetworkDriveSetting(bool useDrive, string driveName, string sharedFolder)
        {
            this.UseDrive = useDrive;
            this.DriveName = driveName;
            this.SharedFolder = sharedFolder;
        }

        #endregion
    }
}
