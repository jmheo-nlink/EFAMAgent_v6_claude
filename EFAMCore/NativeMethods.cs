#region 변경 이력
/*
 * Author : Link mskoo (2013. 2. 12)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2013-02-12   mskoo           최초 작성.
 * ====================================================================================================================
 */
#endregion

using System;

namespace Link.EFAM
{
    /// <summary>
    /// Win32 API의 함수를 호출하기 위한 플랫폼 호출 메소드를 포함하고 있다.
    /// </summary>
    internal static class NativeMethods
    {
        // 오류 코드 WinError.h
        internal const int NO_ERROR = 0;
        //internal const int ERROR_BAD_DEV_TYPE = 66;
        //internal const int ERROR_BAD_NET_NAME = 67;     // The network name cannot be found.
        internal const int ERROR_ALREADY_ASSIGNED = 85; // The local device name is already in use.
        internal const int ERROR_INVALID_PASSWORD = 86;
        //internal const int ERROR_INVALID_PARAMETER = 87;
        //internal const int ERROR_BUSY = 170;
        //internal const int ERROR_MORE_DATA = 234;
        //internal const int ERROR_NO_MORE_ITEMS = 259;
        //internal const int ERROR_INVALID_ADDRESS = 487;
        internal const int ERROR_BAD_DEVICE = 1200;
        internal const int ERROR_DEVICE_ALREADY_REMEMBERED = 1202;
        internal const int ERROR_NO_NET_OR_BAD_PATH = 1203; // The network path was either typed incorrectly, does not exist, or the network provider is not currently available.
        //internal const int ERROR_BAD_PROVIDER = 1204;
        //internal const int ERROR_CANNOT_OPEN_PROFILE = 1205;
        //internal const int ERROR_BAD_PROFILE = 1206;
        internal const int ERROR_SESSION_CREDENTIAL_CONFLICT = 1219;
        //internal const int ERROR_NO_NETWORK = 1222;
        //internal const int ERROR_CANCELLED = 1223;
        internal const int ERROR_LOGON_FAILURE = 1326;
        //internal const int ERROR_BAD_USERNAME = 2202;
        //internal const int ERROR_NOT_CONNECTED = 2250;
        //internal const int ERROR_OPEN_FILES = 2401;
        //internal const int ERROR_DEVICE_IN_USE = 2404;
    }
}
