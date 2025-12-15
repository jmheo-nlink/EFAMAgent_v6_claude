#region 변경 이력
/*
 * Author : Link mskoo (2012. 4. 20)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2012-04-20   mskoo           최초 작성.
 * ====================================================================================================================
 */
#endregion

using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Link.EFAM.Engine
{
    /// <summary>
    /// 네트워크와 관련된 유틸리티 메소드들을 제공한다.
    /// </summary>
    internal static class NetworkUtility
    {
        #region 정적 메소드

        /// <summary>
        /// 로컬 컴퓨터의 IP(인터넷 프로토콜) 주소와 MAC(Media Access Control) 주소를 가져온다.
        /// </summary>
        /// <returns>로컬 컴퓨터의 IP(인터넷 프로토콜) 주소와 MAC(Media Access Control) 주소를 나타내는 문자열</returns>
        /// <remarks>
        /// IP 버전 4에 대한 주소를 반환한다.
        /// </remarks>
        /// 
        /// <exception cref="NetworkInformationException">Windows 시스템 함수 호출이 실패한 경우</exception>
        public static string GetIPAddress()
        {
            string ipAddr = null;
            string macAddr = null;

            foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
            {
                // 작동 중인 어댑터가 아닌 경우
                if (adapter.OperationalStatus != OperationalStatus.Up) continue;

                ipAddr = GetIPAddress(adapter);
                macAddr = GetMACAddress(adapter);

                if (ipAddr.Length != 0)
                {
                    return (ipAddr + " (" + macAddr + ")");
                }
            } // foreach ( NetworkInterface )

            return "";
        }

        // 어댑터에 할당된 IP 주소(IP 버전 4)를 가져온다.
        private static string GetIPAddress(NetworkInterface adapter)
        {
            IPInterfaceProperties adapterProps = adapter.GetIPProperties();

            // IP 버전 4를 지원하지 않을 경우
            if (!adapter.Supports(NetworkInterfaceComponent.IPv4)) return "";
            if (adapterProps.GetIPv4Properties() == null) return "";

            foreach (UnicastIPAddressInformation ipAddrInfo in adapterProps.UnicastAddresses)
            {
                // IP 버전 4에 대한 주소일 경우
                if (ipAddrInfo.Address.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ipAddrInfo.Address.ToString();
                }
            } // foreach ( UnicastIPAddressInformation )

            return "";
        }

        // 어댑터에 대한 MAC 주소를 가져온다.
        private static string GetMACAddress(NetworkInterface adapter)
        {
            string macAddr = String.Empty;
            byte[] addrBytes = null;
            int lastIndex = 0;

            addrBytes = adapter.GetPhysicalAddress().GetAddressBytes();
            lastIndex = addrBytes.Length - 1;

            // MAC 주소를 문자열 표현으로 변환한다.
            for (int index = 0; index < addrBytes.Length; index++)
            {
                macAddr += addrBytes[index].ToString("X2");
                if (index != lastIndex) macAddr += "-";
            }

            return macAddr;
        }
        
        #endregion
    }
}
