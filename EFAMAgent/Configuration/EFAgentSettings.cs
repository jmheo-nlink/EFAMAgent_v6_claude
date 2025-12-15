using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;
// .NET용 개발 라이브러리
using Link.DLK.Security.Cryptography;

namespace Link.EFAM.Agent.Configuration
{
    internal class EFAgentSettings : ApplicationSettingsBase
    {
        private const string CryptoPassword = "EFAM_Agent_Cfg";     // 비밀 키를 파생시킬 암호
        private const string CryptoKeySalt = "SettingS";            // 비밀 키를 파생시키는데 사용할 키 솔트

        private static SymmetricCrypto m_crypto = null;

        #region 정적 속성

        private static EFAgentSettings m_instance = null;

        /// <summary>
        /// 캐시된 <see cref="EFAgentSettings"/> 인스턴스를 반환한다.
        /// </summary>
        /// <value>캐시된 <see cref="EFAgentSettings"/> 개체</value>
        public static EFAgentSettings Default
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = (EFAgentSettings)
                        ApplicationSettingsBase.Synchronized(new EFAgentSettings());
                }

                return m_instance;
            } // get
        }

        #endregion

        #region 속성

        /// <summary>
        /// 응용 프로그램이 처음으로 실행된 것인지 여부를 나타내는 값을 가져오거나 설정한다.
        /// </summary>
        /// <value>현재 응용 프로그램 버전이 이전에 실행된 적이 없으면 <b>true</b>, 그렇지 않으면 <b>false</b></value>
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool IsFirstRun
        {
            get { return ((bool)this["IsFirstRun"]); }
            set { this["IsFirstRun"] = value; }
        }

        /// <summary>
        /// 사용자 ID를 가져오거나 설정한다.
        /// </summary>
        /// <value>사용자 ID</value>
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string UserId
        {
            get 
            {
                string encryptedValue = (string)this["UserId"];
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
                this["UserId"]
                    = String.IsNullOrEmpty(value) ? "" : m_crypto.Encrypt(value);
            }
        }

        /// <summary>
        /// 사용자의 비밀번호를 가져오거나 설정한다.
        /// </summary>
        /// <value>사용자의 비밀번호</value>
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string Password
        {
            get
            {
                string encryptedValue = (string)this["Password"];
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
                this["Password"]
                    = String.IsNullOrEmpty(value) ? "" : m_crypto.Encrypt(value);
            }
        }

        /// <summary>
        /// 응용 프로그램이 자동으로 실행되는지 여부를 나타내는 값을 가져오거나 설정한다.
        /// </summary>
        /// <value>응용 프로그램이 자동으로 실행되면 <b>true</b>, 그렇지 않으면 <b>false</b></value>
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool AutoRun
        {
            get { return ((bool)this["AutoRun"]); }
            set { this["AutoRun"] = value; }
        }

        /// <summary>
        /// 응용 프로그램이 실행될 때 자동으로 로그인할지 여부를 나타내는 값을 가져오거나 설정한다.
        /// </summary>
        /// <value>응용 프로그램이 실행될 때 자동으로 로그인해야 하면 <b>true</b>, 그렇지 않으면 <b>false</b></value>
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool AutoLogin
        {
            get { return ((bool)this["AutoLogin"]); }
            set { this["AutoLogin"] = value; }
        }

        [global::System.Configuration.UserScopedSettingAttribute()]
        public global::System.Collections.Specialized.StringCollection NetworkDrives
        {
            get
            {
                return ((global::System.Collections.Specialized.StringCollection)(this["NetworkDrives"]));
            }
            set
            {
                this["NetworkDrives"] = value;
            }
        }

        /// <summary>
        /// 네트워크 드라이브 설정들을 가져오거나 설정한다.
        /// </summary>
        /// <value>네트워크 드라이브 설정을 나타내는 <see cref="NetworkDriveSetting"/>의 목록</value>
        public List<NetworkDriveSetting> NetworkDriveSettings
        {
            get
            {
                StringCollection encryptedValueColl = (StringCollection)this["NetworkDrives"];
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
                            // 사용 여부, 드라이브 이름과 공유 네트워크 폴더를 분리한다.
                            // ex)
                            // "True|C:|\\127.0.0.1\shared_folder" => "True", "C:", "\\127.0.0.1\shared_folder"
                            // ex) old
                            // "C:\\127.0.0.1\shared_folder" => "C:", "\\127.0.0.1\shared_folder"
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
                        // 사용 여부, 드라이브 이름과 공유 네트워크 폴더를 연결한다.
                        // ex)
                        // "True", "C:", "\\127.0.0.1\shared_folder" => "True|C:|\\127.0.0.1\shared_folder"
                        settingValue = setting.UseDrive + "|"
                                     + setting.DriveName + "|" + setting.SharedFolder;

                        valueColl.Add(m_crypto.Encrypt(settingValue));
                    } // foreach ( NetworkDriveSetting )
                } // if (value != null)

                this["NetworkDrives"] = valueColl;
            } // set
        }

        /// <summary>
        /// 연결할 E-FAM 서버의 URL을 가져오거나 설정한다. 
        /// </summary>
        /// <value>연결할 E-FAM 서버의 URL</value>
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://10.4.99.26/EFAMServer5")]
        public string ServerUrl
        {
            get { return ((string)this["ServerUrl"]); }
            set { this["ServerUrl"] = value; }
        }

        /// <summary>
        /// 업데이트 응용 프로그램을 가져온다.
        /// </summary>
        /// <value>업데이트 응용 프로그램의 이름 (응용 프로그램의 실행 파일 이름)</value>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Update.exe")]
        public string EFAMUpdater
        {
            get { return ((string)this["EFAMUpdater"]); }
        }

        /// <summary>
        /// 로컬 컴퓨터가 NAS와 같은 도메인에 있는지 여부를 나타내는 값을 가져온다.
        /// </summary>
        /// <value>로컬 컴퓨터가 NAS와 같은 도메인에 있으면 <b>true</b>, 그렇지 않으면 <b>false</b></value>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool IsInSameDomainWithNAS
        {
            get { return ((bool)this["IsInSameDomainWithNAS"]); }
        }

        /// <summary>
        /// 로컬 컴퓨터가 NAS와 같은 도메인에 있을 때
        /// 로그인 폼, 로드/언로드 폼 및 알림 영역 아이콘을 숨겨야 하는지 여부를 나타내는 값을 가져온다.
        /// </summary>
        /// <value>
        /// 로그인 폼, 로드/언로드 폼 및 알림 영역 아이콘을 숨겨야 하면 <b>true</b>, 그렇지 않으면 <b>false</b>
        /// </value>
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool HideFormInSameDomainWithNAS
        {
            get { return ((bool)this["HideFormInSameDomainWithNAS"]); }
        }

        #endregion

        #region 클래스 생성자

        /// <summary>
        /// 클래스의 정적 멤버를 초기화한다.
        /// </summary>
        static EFAgentSettings()
        {
            m_crypto = new SymmetricCrypto(CryptoPassword, CryptoKeySalt);
        }

        #endregion

        public EFAgentSettings()
            : base("Link.EFAM.Agent.Properties.Settings")
        {
        }
    }
}
