#region 변경 이력
/*
 * Author : Link mskoo (2012. 2. 26)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2012-02-26   mskoo           최초 작성.
 * 
 * 2012-06-27   mskoo           속성 추가.
 *                              - IsInSameDomainWithNAS
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;
// .NET용 개발 라이브러리
//using Link.DLK.Security.Cryptography;
using Link.Core.Security.Cryptography;

using Link.EFAM.Agent.Properties;

namespace Link.EFAM.Agent.Configuration
{
    /// <summary>
    /// E-FAM Agent 응용 프로그램의 응용 프로그램 설정을 래핑한다.
    /// </summary>
    class AgentSettings
    {
        private const string CryptoPassword = "EFAM_Agent_Cfg";     // 비밀 키를 파생시킬 암호
        private const string CryptoKeySalt = "SettingS";            // 비밀 키를 파생시키는데 사용할 키 솔트

        private static SymmetricCryptographer m_crypto = null;

        private Settings m_appSettings = null;

        #region 정적 속성

        private static AgentSettings m_instance = null;

        /// <summary>
        /// 캐시된 <see cref="AgentSettings"/> 인스턴스를 반환한다.
        /// </summary>
        /// <value>캐시된 <see cref="AgentSettings"/> 개체</value>
        public static AgentSettings Default
        {
            get 
            {
                if (m_instance == null) m_instance = new AgentSettings();

                return m_instance; 
            }
        }

        #endregion

        #region 속성

        /// <summary>
        /// 사용자 ID를 가져오거나 설정한다.
        /// </summary>
        /// <value>사용자 ID</value>
        /// <remarks>
        /// 응용 프로그램 설정에서 "UserId" 설정 속성의 값을 가져오거나 설정한다.
        /// </remarks>
        public string UserId
        {
            get
            {
                string encryptedValue = m_appSettings.UserId;
                string userId = String.Empty;

                try
                {
                    userId = String.IsNullOrEmpty(encryptedValue) 
                           ? "" : m_crypto.Decrypt(encryptedValue);
                }
                catch (System.Security.Cryptography.CryptographicException) { }

                return userId;
            } // get
            set
            {
                m_appSettings.UserId 
                    = String.IsNullOrEmpty(value) ? "" : m_crypto.Encrypt(value);
            }
        }

        /// <summary>
        /// 사용자의 비밀번호를 가져오거나 설정한다.
        /// </summary>
        /// <value>사용자의 비밀번호</value>
        /// <remarks>
        /// 응용 프로그램 설정에서 "Password" 설정 속성의 값을 가져오거나 설정한다.
        /// </remarks>
        public string Password
        {
            get
            {
                string encryptedValue = m_appSettings.Password;
                string password = String.Empty;

                try
                {
                    password = String.IsNullOrEmpty(encryptedValue) 
                             ? "" : m_crypto.Decrypt(encryptedValue);
                }
                catch (System.Security.Cryptography.CryptographicException) { }

                return password;
            } // get
            set
            {
                m_appSettings.Password 
                    = String.IsNullOrEmpty(value) ? "" : m_crypto.Encrypt(value);
            }
        }

        /// <summary>
        /// 응용 프로그램이 자동으로 실행되는지 여부를 나타내는 값을 가져오거나 설정한다.
        /// </summary>
        /// <value>응용 프로그램이 자동으로 실행되면 true, 그렇지 않으면 false</value>
        /// <remarks>
        /// 응용 프로그램 설정에서 "AutoRun" 설정 속성의 값을 가져오거나 설정한다.
        /// </remarks>
        public bool AutoRun
        {
            get { return m_appSettings.AutoRun; }
            set { m_appSettings.AutoRun = value; }
        }

        /// <summary>
        /// 응용 프로그램이 실행될 때 자동으로 로그인할지 여부를 나타내는 값을 가져오거나 설정한다.
        /// </summary>
        /// <value>응용 프로그램이 실행될 때 자동으로 로그인해야 하면 true, 그렇지 않으면 false</value>
        /// <remarks>
        /// 응용 프로그램 설정에서 "AutoLogin" 설정 속성의 값을 가져오거나 설정한다.
        /// </remarks>
        public bool AutoLogin
        {
            get { return m_appSettings.AutoLogin; }
            set { m_appSettings.AutoLogin = value; }
        }

        /// <summary>
        /// 네트워크 드라이브 설정들을 가져오거나 설정한다.
        /// </summary>
        /// <value>네트워크 드라이브 설정을 나타내는 <see cref="NetworkDriveSetting"/>의 목록</value>
        /// <remarks>
        /// 응용 프로그램 설정에서 "NetworkDrives" 설정 속성의 값을 가져오거나 설정한다.
        /// </remarks>
        public List<NetworkDriveSetting> NetworkDriveSettings
        {
            get
            {
                StringCollection encryptedValueColl = m_appSettings.NetworkDrives;
                List<NetworkDriveSetting> settingList = new List<NetworkDriveSetting>();

                if (encryptedValueColl != null)
                {
                    NetworkDriveSetting setting = null;
                    string[] values = null;
                    string settingValue = null;
                    char[] separator = { '|' };

                    foreach (string encryptedValue in encryptedValueColl)
                    {
                        try
                        {
                            //
                            // 사용 여부, 드라이브 이름과 공유 네트워크 폴더를 분리한다.
                            // ex)
                            // "True|C:|\\127.0.0.1\shared_folder" => "True", "C:", "\\127.0.0.1\shared_folder"
                            // ex) old
                            // "C:\\127.0.0.1\shared_folder" => "C:", "\\127.0.0.1\shared_folder"
                            //
                            settingValue = m_crypto.Decrypt(encryptedValue);
                            values = settingValue.Split(separator);

                            // 사용 여부가 없는 설정값일 경우
                            if (values.Length < 3)
                            {
                                if (settingValue.Length < 3) continue;

                                setting = new NetworkDriveSetting();
                                setting.UseDrive = true;
                                setting.DriveName = settingValue.Substring(0, 2);
                                setting.SharedFolder = settingValue.Substring(2);
                            }
                            // 사용 여부가 추가된 설정값일 경우
                            else
                            {
                                setting = new NetworkDriveSetting();
                                setting.UseDrive = bool.Parse(values[0]);
                                setting.DriveName = values[1];
                                setting.SharedFolder = values[2];
                            }

                            settingList.Add(setting);
                        } // try
                        catch (System.Security.Cryptography.CryptographicException) { }
                        catch (FormatException) { }
                    } // foreach ( string )
                } // if (encryptedValueColl != null)

                return settingList;
            } // get
            set
            {
                StringCollection valueColl = new StringCollection();
                string settingValue = null;

                if (value != null)
                {
                    foreach (NetworkDriveSetting setting in value)
                    {
                        //
                        // 사용 여부, 드라이브 이름과 공유 네트워크 폴더를 연결한다.
                        // ex)
                        // "True", "C:", "\\127.0.0.1\shared_folder" => "True|C:|\\127.0.0.1\shared_folder"
                        //
                        settingValue = setting.UseDrive + "|" 
                                     + setting.DriveName + "|" + setting.SharedFolder;

                        valueColl.Add(m_crypto.Encrypt(settingValue));
                    } // foreach ( NetworkDriveSetting )
                } // if (value != null)

                m_appSettings.NetworkDrives = valueColl;
            } // set
        }

        /// <summary>
        /// 연결할 E-FAM 서버의 URL을 가져오거나 설정한다. 
        /// </summary>
        /// <value>연결할 E-FAM 서버의 URL</value>
        /// <remarks>
        /// 응용 프로그램 설정에서 "ServerUrl" 설정 속성의 값을 가져오거나 설정한다.
        /// </remarks>
        public string ServerUrl
        {
            get { return m_appSettings.ServerUrl; }
            set { m_appSettings.ServerUrl = value; }
        }

        /// <summary>
        /// 업데이트 응용 프로그램을 가져온다.
        /// </summary>
        /// <value>업데이트 응용 프로그램의 이름 (응용 프로그램의 실행 파일 이름)</value>
        /// <remarks>
        /// 응용 프로그램 설정에서 "EFAMUpdater" 설정 속성의 값을 가져온다.
        /// </remarks>
        public string EFAMAgentUpdater
        {
            get { return m_appSettings.EFAMUpdater; }
        }

        /// <summary>
        /// 로컬 컴퓨터가 NAS와 같은 도메인에 있는지 여부를 나타내는 값을 가져온다.
        /// </summary>
        /// <value>로컬 컴퓨터가 NAS와 같은 도메인에 있으면 true, 그렇지 않으면 false</value>
        /// <remarks>
        /// 응용 프로그램 설정에서 "IsInSameDomainWithNAS" 설정 속성의 값을 가져온다.
        /// </remarks>
        public bool IsInSameDomainWithNAS
        {
            get { return m_appSettings.IsInSameDomainWithNAS; }
        }

        /// <summary>
        /// 로컬 컴퓨터가 NAS와 같은 도메인에 있을 때
        /// 로그인 폼, 로드/언로드 폼 및 알림 영역 아이콘을 숨겨야 하는지 여부를 나타내는 값을 가져온다.
        /// </summary>
        /// <value>로그인 폼, 로드/언로드 폼 및 알림 영역 아이콘을 숨겨야 하면 true, 그렇지 않으면 false</value>
        /// <remarks>
        /// 응용 프로그램 설정에서 "HideFormInSameDomainWithNAS" 설정 속성의 값을 가져온다.
        /// </remarks>
        public bool HideFormInSameDomainWithNAS
        {
            get { return m_appSettings.HideFormInSameDomainWithNAS; }
        }

        #endregion

        #region 클래스 생성자

        /// <summary>
        /// 클래스의 정적 멤버를 초기화한다.
        /// </summary>
        static AgentSettings()
        {
            m_crypto = new SymmetricCryptographer(CryptoPassword, CryptoKeySalt);
        }

        #endregion

        #region 생성자

        /// <summary>
        /// <see cref="AgentSettings"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        public AgentSettings()
        {
            m_appSettings = Settings.Default;
        }

        #endregion

        #region 메소드

        /// <summary>
        /// 영구 저장소에서 응용 프로그램 설정 속성 값을 새로 고친다.
        /// </summary>
        public void Reload()
        {
            m_appSettings.Reload();
        }

        /// <summary>
        /// 응용 프로그램 설정 속성의 현재 값을 저장한다.
        /// </summary>
        public void Save()
        {
            m_appSettings.Save();
        }

        #endregion
    }
}
