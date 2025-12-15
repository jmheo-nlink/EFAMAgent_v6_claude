#region 변경 이력
/*
 * Author : Link mskoo (2011. 3. 13)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-03-13   mskoo           최초 작성.
 * 
 * 2011-07-03   mskoo           1.0 버전 릴리즈.
 * ====================================================================================================================
 */
#endregion

using System;
using System.Runtime.InteropServices;
using System.ServiceProcess;

namespace Link.DLK.ServiceProcess
{
    /// <summary>
    /// 미니필터(Minifilter) 드라이버 서비스(Windows 서비스)를 나타내며 이 클래스를 사용하면
    /// 실행 중이거나 중지된 서비스에 연결하거나 서비스를 조작하거나 서비스 관련 정보를 가져올 수 있다.
    /// </summary>
    public class MinifilterController : ServiceController
    {
        #region P/Invoke 프로토타입
        /*
        [DllImport("fltLib.dll", EntryPoint = "FilterLoad", CharSet = CharSet.Unicode)]
        internal extern static int FilterLoad(string lpFilterName);

        [DllImport("fltLib.dll", EntryPoint = "FilterUnload", CharSet = CharSet.Unicode)]
        internal extern static int FilterUnload(string lpFilterName);

        internal const int S_OK = 0;
        */
        #endregion

        #region 속성

        /// <summary>
        /// 미니필터 드라이버 서비스가 설치된 서비스인지 여부를 나타내는 값을 가져온다.
        /// </summary>
        /// <value>설치된 미니필터 드라이버 서비스이면 true, 그렇지 않으면 false</value>
        /// 
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// 시스템 API에 액세스하는 동안 오류가 발생한 경우
        /// </exception>
        public virtual bool IsInstalled
        {
            get
            {
                try
                {
                    ServiceControllerStatus status = this.Status;
                }
                catch (InvalidOperationException)
                {
                    return false;
                }

                return true;
            } // get
        }

        #endregion

        #region 생성자

        /// <summary>
        /// 로컬 컴퓨터의 기존 미니필터 드라이버 서비스와 관련된 
        /// <see cref="MinifilterController"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        /// <param name="name">시스템에서 미니필터 드라이버 서비스를 식별하는 약식 이름</param>
        /// 
        /// <exception cref="ArgumentException">name이 null이거나 길이가 0인 문자열인 경우</exception>
        public MinifilterController(string name)
            : base(name)
        {
        }

        #endregion

        #region ServiceController 멤버
        /*
        /// <summary>
        /// 미니필터 드라이버 서비스를 시작한다.
        /// </summary>
        /// <remarks>호출자는 SeLoadDriverPrivilege (SE_LOAD_DRIVER_PRIVILEGE) 권한을 가져야 한다.</remarks>
        /// 
        /// <exception cref="COMException">인식할 수 없는 HRESULT가 WDK API 함수 호출에서 반환된 경우</exception>
        public new void Start()
        {
            int errorCode = 0;

            // 미니필터 드라이버를 로드한다. (Native 함수 호출)
            errorCode = FilterLoad(this.ServiceName);
            if (errorCode != S_OK) Marshal.ThrowExceptionForHR(errorCode);
        }

        /// <summary>
        /// 미니필터 드라이버 서비스를 중지한다.
        /// </summary>
        /// <remarks>호출자는 SeLoadDriverPrivilege (SE_LOAD_DRIVER_PRIVILEGE) 권한을 가져야 한다.</remarks>
        /// 
        /// <exception cref="COMException">인식할 수 없는 HRESULT가 WDK API 함수 호출에서 반환된 경우</exception>
        public new void Stop()
        {
            int errorCode = 0;

            // 미니필터 드라이버를 언로드한다. (Native 함수 호출)
            errorCode = FilterUnload(this.ServiceName);
            if (errorCode != S_OK) Marshal.ThrowExceptionForHR(errorCode);
        }
        */
        #endregion
    }
}
