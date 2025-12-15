#region 변경 이력
/*
 * Author : Link mskoo (2013. 2. 12)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2013-02-12   mskoo           최초 작성.
 * 
 * 2013-04-01   mskoo           64비트 운영체제에서 64비트용 S-Work SDK 모듈을 사용하도록 수정.
 * 
 * 2013-12-04   mskoo           메소드 추가.
 *                              - IsSWorkInstall()
 *                              - GetStatusFromSW()
 * 
 * 2014-07-28   mskoo           DLL 파일의 이름을 절대 경로로 변경.
 * ====================================================================================================================
 */
#endregion

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Link.EFAM.Agent
{
    /// <summary>
    /// Win32 API의 함수를 호출하기 위한 플랫폼 호출 메소드를 포함하고 있다.
    /// </summary>
    static class NativeMethods
    {
        internal const string Kernel32 = "kernel32.dll";
        internal const string SWExternalSDK32 = @"C:\Windows\softcamp\SDK\SCSWExternalSDK.dll";
        internal const string SWExternalSDK64 = @"C:\Windows\softcamp\SDK\SCSWExternalSDK64.dll";

        // 오류 코드 WinError.h
        internal const int NO_ERROR = 0;

        private static readonly bool Is64BitOS = (IntPtr.Size == 8);

        #region KERNEL32 Functions

        [DllImport(Kernel32, EntryPoint = "SetDllDirectoryW", CharSet = CharSet.Unicode, SetLastError = true)]
        internal extern static bool SetDllDirectory(string lpPathName);

        [DllImport(Kernel32, EntryPoint = "GetPrivateProfileStringW", CharSet = CharSet.Unicode, SetLastError = true)]
        internal extern static int GetPrivateProfileString(
            [In]  string lpAppName,
            [In]  string lpKeyName,
            [In]  string lpDefault,
            [Out] StringBuilder lpReturnedString,
            [In]  /*DWORD*/ int nSize,
            [In]  string lpFileName
            );

        #endregion

        #region SCSWExternalSDK 함수

        internal const int SWORK_STATUS_NULL = 0;    // 초기화 상태 (실행 후 아무것도 안 한 상태)
        internal const int SWORK_STATUS_LOGOUT = 1;      // 로그 아웃 생태, 프로그램 실행 후 로그인 전, 시스템 로그 오프 
        internal const int SWORK_STATUS_LOGON_CHECK = 2;     // 현재 로그인 체크 중인 상태 
        internal const int SWORK_STATUS_LOGON_ONLINE = 3;    // 현재 온라인 로그인 상태
        internal const int SWORK_STATUS_LOGON_OFFLINE = 4;   // 현재 오프라인 로그인 상태 
        internal const int SWORK_STATUS_LOGON_ONLINE_FILOT = 5;  // 파일럿 모드 온라인 상태 
        internal const int SWORK_STATUS_LOGON_OFFLINE_FILOT = 6; // 파일럿 모드 오프라인 로그인 상태 
        internal const int SWORK_STATUS_LOGOUT_CHECK = 7;    // 로그 아웃 중.. 
        internal const int SWORK_STATUS_FORCE_USE_CHECK = 8;     // 강제 사용 모드 사용을 위한 시스템 구성 중..
        internal const int SWORK_STATUS_FORCE_USE = 9;       // 강제 사용 모드 실행중..  (오프라인 권한 X) 오프라인 상태에서 강제 사용권한 있을 경우
        internal const int SWORK_STATUS_LOGON_NOTUSE = 10;   // 도면 보안(문서 보안) 로그인 되고, 도면 보안 사용 권한이 없을 경우 
        internal const int SWORK_STATUS_ONLINE_SELECTED_WORK = 11;   // 도면보안 온라인 협력사 버전 - 작업 선택된 상태 (Use 상태)
        internal const int SWORK_STATUS_ONLINE_NOT_SELECTED_WORK = 12;   // 도면보안 온라인 협력사 버전 - 작업 선택 비활성 (None Use 상태)
        internal const int SWORK_STATUS_OFFLINE_SELECTED_WORK = 13;      // 도면보안 온라인 협력사 버전 - 작업 선택된 상태 (Use 상태)
        internal const int SWORK_STATUS_OFFLINE_NOT_SELECTED_WORK = 14;  // 도면보안 온라인 협력사 버전 - 작업 선택 비활성 (None Use 상태)
        internal const int SWORK_STATUS_LOGOUT_RESTORE = 15;         // 로그아웃 진행중 - 백업된 내용 복구중 (2.0)
        internal const int SWORK_STATUS_WAIT_SERVICE_ACTIVE = 16;    // init 보다 앞선 서비스 대기 상태임.
        internal const int SWORK_STATUS_LOGON_CHECK_OFFLINE = 17;    // 오프라인 로그인 체크 중인 상태	
        internal const int SWORK_STATUS_FAIL = -1;   // 오류 상태

        internal const int SWORK_SHARE_DATA_ADD = 0;
        internal const int SWORK_SHARE_DATA_DELETE = 1;
        internal const int SWORK_SHARE_DATA_MODIFY = 2;
        internal const int SWORK_SHARE_DATA_GET_LIST = 3;
        internal const int SWORK_SHARE_DATA_GET_COUNT = 4;

        public static bool IsSWorkInstall()
        {
            return (Is64BitOS ? IsSWorkInstall64() : IsSWorkInstall32());
        }

        [DllImport(SWExternalSDK32, EntryPoint = "IsSWorkInstall", SetLastError = true)]
        private static extern bool IsSWorkInstall32();

        [DllImport(SWExternalSDK64, EntryPoint = "IsSWorkInstall", SetLastError = true)]
        private static extern bool IsSWorkInstall64();

        public static int GetStatusFromSW()
        {
            return (Is64BitOS ? GetStatusFromSW64() : GetStatusFromSW32());
        }

        [DllImport(SWExternalSDK32, EntryPoint = "GetStatusFromSW", SetLastError = true)]
        private static extern int GetStatusFromSW32();

        [DllImport(SWExternalSDK64, EntryPoint = "GetStatusFromSW", SetLastError = true)]
        private static extern int GetStatusFromSW64();

        public static int QuerySecureNetworkInfo(
            int nAction, StringBuilder pRemoteName, StringBuilder pDriveLetter, StringBuilder pPolicy, StringBuilder pUserID,
            StringBuilder pServerIP, StringBuilder pServerName, int nDataIndex)
        {
            bool outputAutoConnect = false;
            int errorCode = NO_ERROR;

            if (Is64BitOS)
            {
                errorCode = QuerySecureNetworkInfo64(null, nAction, false, pRemoteName, pDriveLetter,
                                pPolicy, pUserID, pServerIP, pServerName, ref outputAutoConnect, nDataIndex);
            }
            else
            {
                errorCode = QuerySecureNetworkInfo32(null, nAction, false, pRemoteName, pDriveLetter,
                                pPolicy, pUserID, pServerIP, pServerName, ref outputAutoConnect, nDataIndex);
            }

            return errorCode;
        }

        [DllImport(SWExternalSDK32, EntryPoint = "QuerySecureNetworkInfo", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int QuerySecureNetworkInfo32(string pKeyData, int nAction, bool bAutoConnect,
                                        StringBuilder pRemoteName, StringBuilder pDriveLetter, StringBuilder pPolicy, StringBuilder pUserID,
                                        StringBuilder pServerIP, StringBuilder pServerName, ref bool bOutput_AutoConnect, int nDataIndex);

        [DllImport(SWExternalSDK64, EntryPoint = "QuerySecureNetworkInfo", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int QuerySecureNetworkInfo64(string pKeyData, int nAction, bool bAutoConnect,
                                        StringBuilder pRemoteName, StringBuilder pDriveLetter, StringBuilder pPolicy, StringBuilder pUserID,
                                        StringBuilder pServerIP, StringBuilder pServerName, ref bool bOutput_AutoConnect, int nDataIndex);

        #endregion
    }
}
