#region 변경 이력
/*
 * Author : Link mskoo (2011. 5. 11)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-05-11   mskoo           최초 작성.
 *                              VC++.NET으로 작성된 열거형을 C#으로 마이그레이션.
 *                              
 * 2011-09-23   mskoo           5.0 버전 릴리즈.
 * ====================================================================================================================
 */
#endregion

namespace Link.EFAM.Common
{
    /// <summary>
    /// 프로세스의 종류를 지정한다.
    /// </summary>
    public enum ProcessKind
    {
        /*
         * FileAccCtrl.h 헤더 파일에 정의된 _NAC_PROCESS_KIND 열거형과 열거자 값이 일치되게 정의한다.
         */

        /// <summary>
        /// 일반적인 프로세스를 나타낸다.
        /// </summary>
        NormalProcess = 0,

        /// <summary>
        /// 시스템 프로세스를 나타낸다.
        /// </summary>
        SystemProcess,

        /// <summary>
        /// "Windows 탐색기" 프로세스를 나타낸다.
        /// </summary>
        WindowsExplorer,

        /// <summary>
        /// "Windows 명령 처리기" 프로세스를 나타낸다.
        /// </summary>
        WindowsCommand,

        /// <summary>
        /// "Microsoft Office" 응용 프로그램의 프로세스를 나타낸다.
        /// </summary>
        MsOffice,

        /// <summary>
        /// 백신 응용 프로그램의 프로세스를 나타낸다.
        /// </summary>
        AntiVirus,

        /// <summary>
        /// 이 열거형의 마지막 열거자 값을 나타낸다.
        /// </summary>
        MaxProcessKind      // MaxProcessKind는 항상 마지막 열거자 값이어야 한다.
    }
}
