#region 변경 이력
/*
 * Author : Link mskoo (2011. 6. 25)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-06-25   mskoo           최초 작성.
 * 
 * 2011-07-03   mskoo           1.0 버전 릴리즈.
 * ====================================================================================================================
 */
#endregion

using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;

//using Link.DLK.Properties;

namespace Link.DLK.ServiceProcess
{
    using Resources = Link.EFAM.Engine.Properties.Resources;

    /// <summary>
    /// 미니필터(Minifilter) 드라이버 서비스(Windows 서비스)를 설치하거나 설치를 제거한다.
    /// </summary>
    public class MinifilterInstaller
    {
        #region P/Invoke 프로토타입

        [DllImport("Advapi32.dll", EntryPoint = "OpenSCManagerW", CharSet = CharSet.Unicode, SetLastError = true)]
        internal extern static IntPtr OpenSCManager(
            string lpMachineName, string lpDatabaseName, Int32 dwDesiredAccess);

        [DllImport("Advapi32.dll", EntryPoint = "CreateServiceW", CharSet = CharSet.Unicode, SetLastError = true)]
        internal extern static IntPtr CreateService(
            IntPtr hSCManager, string lpServiceName, string lpDisplayName,
            Int32 dwDesiredAccess, Int32 dwServiceType, Int32 dwStartType, Int32 dwErrorControl,
            string lpBinaryPathName, string lpLoadOrderGroup, [Out] IntPtr lpdwTagId, string lpDependencies,
            string lpServiceStartName, string lpPassword);

        [DllImport("Advapi32.dll", EntryPoint = "OpenServiceW", CharSet = CharSet.Unicode, SetLastError = true)]
        internal extern static IntPtr OpenService(
            IntPtr hSCManager, string lpServiceName, Int32 dwDesiredAccess);

        [DllImport("Advapi32.dll", EntryPoint = "DeleteService", SetLastError = true)]
        internal extern static bool DeleteService(IntPtr hService);

        [DllImport("Advapi32.dll", EntryPoint = "CloseServiceHandle", SetLastError = true)]
        internal extern static bool CloseServiceHandle(IntPtr hSCObject);

        //
        // 액세스 권한 for SCM (Service Control Manager)
        //
        private const int SC_MANAGER_ALL_ACCESS = 0xF003F;
        private const int SC_MANAGER_CREATE_SERVICE = 0x0002;
        private const int SC_MANAGER_CONNECT = 0x0001;
        //
        // 액세스 권한 for Service
        //
        private const int SERVICE_ALL_ACCESS = 0xF01FF;
        private const int DELETE = 0x00010000;
        //
        // Win32 오류 코드
        //
        private const int ERROR_SERVICE_DOES_NOT_EXIST = 1060;
        private const int ERROR_SERVICE_MARKED_FOR_DELETE = 1072;

        #endregion

        private IntPtr m_hSCManager = IntPtr.Zero;
        private string m_serviceName = null;
        private string m_groupName = null;
        private int m_altitude = 0;

        #region 속성

        /// <summary>
        /// 시스템에서 미니필터 드라이버 서비스를 식별하는 데 사용되는 이름을 가져오거나 설정한다.
        /// </summary>
        /// <value>설치되는 미니필터 드라이버 서비스의 이름</value>
        /// 
        /// <exception cref="ArgumentException">속성 값이 잘못된 경우</exception>
        public string ServiceName
        {
            get { return m_serviceName; }
            set
            {
                if (value == null || value.Trim().Length == 0)
                {
                    throw new ArgumentException(Resources.Error_InvalidProperty, "ServiceName");
                }

                m_serviceName = value;
            } // set
        }

        /// <summary>
        /// 미니필터 드라이버가 포함될 그룹의 이름을 가져오거나 설정한다.
        /// </summary>
        /// <value>미니필터 드라이버가 포함될 그룹의 이름</value>
        public string GroupName
        {
            get { return m_groupName; }
            set { m_groupName = value; }
        }

        /// <summary>
        /// 미니필터 드라이버에 할당할 UID(식별자) 값을 가져오거나 설정한다.
        /// </summary>
        /// <value>미니필터 드라이버에 할당할 UID(식별자) 값</value>
        /// 
        /// <exception cref="ArgumentException">속성 값이 잘못된 경우</exception>
        public int Altitude
        {
            get { return m_altitude; }
            set
            {
                if (value < 30000) throw new ArgumentException(Resources.Error_InvalidProperty, "Altitude");

                m_altitude = value;
            }
        }

        #endregion

        #region 생성자

        /// <summary>
        /// <see cref="MinifilterInstaller"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        /// 
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// 시스템 API에 액세스하는 동안 오류가 발생한 경우
        /// </exception>
        public MinifilterInstaller()
        {
            // 서비스 컨트롤 매니저에 대한 핸들을 가져온다. (Native 함수 호출)
            m_hSCManager = OpenSCManager(null, null, (SC_MANAGER_CREATE_SERVICE | SC_MANAGER_CONNECT));
            if (m_hSCManager == IntPtr.Zero) throw new System.ComponentModel.Win32Exception();
        }

        #endregion

        #region 소멸자

        /// <summary>
        /// 인스턴스가 할당한 모든 리소스를 해제한다.
        /// </summary>
        ~MinifilterInstaller()
        {
            // 핸들을 닫는다. (Native 함수 호출)
            if (m_hSCManager != IntPtr.Zero) CloseServiceHandle(m_hSCManager);
        }

        #endregion

        #region 메소드
        #region 설치

        /// <summary>
        /// 미니필터 드라이버 서비스 정보를 레지스트리에 기록하여 서비스를 설치한다.
        /// </summary>
        /// <param name="fileName">설치할 미니필터 드라이버 파일의 경로</param>
        /// <returns>서비스를 설치하는데 성공하면 true, 그렇지 않으면 false</returns>
        /// 
        /// <exception cref="ArgumentNullException">fileName이 null인 경우</exception>
        /// <exception cref="ArgumentException">fileName이 길이가 0인 문자열이거나 공백만 포함한 경우</exception>
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// 시스템 API에 액세스하는 동안 오류가 발생한 경우
        /// </exception>
        /// <exception cref="System.Security.SecurityException">
        /// 사용자에게 읽기/쓰기 모드에서 레지스트리 키에 액세스하는 데 필요한 권한이 없는 경우<br/>
        /// - 또는 -<br/>
        /// 사용자에게 레지스트리 키를 수정하는 데 필요한 권한이 없는 경우
        /// </exception>
        public bool Install(string fileName)
        {
            if (fileName == null) throw new ArgumentNullException("fileName");
            if (fileName.Trim().Length == 0)
            {
                throw new ArgumentException(Resources.Argument_WhiteSpaceString, "fileName");
            }

            IntPtr hService = IntPtr.Zero;
            bool success = false;

            try
            {
                // 서비스를 생성한다. (Native 함수 호출)
                hService = CreateService(m_hSCManager,
                    m_serviceName, m_serviceName,
                    SERVICE_ALL_ACCESS,
                    0x00000002,     // SERVICE_FILE_SYSTEM_DRIVER
                    0x00000001,     // SERVICE_SYSTEM_START
                    0x00000001,     // SERVICE_ERROR_NORMAL
                    fileName,
                    m_groupName, IntPtr.Zero, "FltMgr\0\0",
                    null, null);
                if (hService == IntPtr.Zero) throw new System.ComponentModel.Win32Exception();

                success = DefineInstance();
            } // try
            finally
            {
                // 서비스를 삭제한다. (Native 함수 호출)
                if (!success) DeleteService(hService);

                // 핸들을 닫는다. (Native 함수 호출)
                CloseServiceHandle(hService);
            } // finally

            return success;
        }

        /// <summary>
        /// 미니필터 드라이버의 인스턴스 정보를 레지스트리에 기록한다.
        /// </summary>
        /// <returns>인스턴스 정보를 레지스트리에 기록하면 true, 그렇지 않으면 false</returns>
        private bool DefineInstance()
        {
            RegistryKey instancesKey = null;
            RegistryKey defaultInstanceKey = null;
            string keyName = null;

            try
            {
                string instanceName = m_serviceName + " Instance";

                //
                // "Instances" 키와 값을 만든다.
                //
                // HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\%driver_name%\Instances
                //
                keyName = String.Format(@"SYSTEM\CurrentControlSet\services\{0}\Instances", m_serviceName);
                instancesKey = Registry.LocalMachine.CreateSubKey(keyName);
                if (instancesKey == null) return false;

                instancesKey.SetValue("DefaultInstance", instanceName, RegistryValueKind.String);

                //
                // "DefaultInstance" 키와 값을 만든다.
                //
                // HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\%driver_name%\Instances\\%instance_name%
                //
                defaultInstanceKey = instancesKey.CreateSubKey(instanceName);
                if (defaultInstanceKey == null) return false;

                defaultInstanceKey.SetValue("Altitude", m_altitude.ToString(), RegistryValueKind.String);
                defaultInstanceKey.SetValue("Flags", 0, RegistryValueKind.DWord);
            } // try
            finally
            {
                if (instancesKey != null) instancesKey.Close();
                if (defaultInstanceKey != null) defaultInstanceKey.Close();
            }

            return true;
        }

        #endregion

        #region 제거

        /// <summary>
        /// 레지스트리에서 미니필터 드라이버 서비스에 대한 정보를 제거하여 서비스를 제거한다.
        /// </summary>
        /// 
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// 시스템 API에 액세스하는 동안 오류가 발생한 경우
        /// </exception>
        public void Uninstall()
        {
            IntPtr hService = IntPtr.Zero;

            try
            {
                // 서비스에 대한 핸들을 가져온다. (Native 함수 호출)
                hService = OpenService(m_hSCManager, m_serviceName, DELETE);
                if (hService == IntPtr.Zero)
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    if (errorCode != ERROR_SERVICE_DOES_NOT_EXIST)
                    {
                        throw new System.ComponentModel.Win32Exception(errorCode);
                    }
                }

                // 서비스를 삭제한다. (Native 함수 호출)
                if (!DeleteService(hService))
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    if (errorCode != ERROR_SERVICE_MARKED_FOR_DELETE)
                    {
                        throw new System.ComponentModel.Win32Exception(errorCode);
                    }
                }
            } // try
            finally
            {
                // 핸들을 닫는다. (Native 함수 호출)
                if (hService != IntPtr.Zero) CloseServiceHandle(hService);
            }
        }

        #endregion
        #endregion
    }
}
