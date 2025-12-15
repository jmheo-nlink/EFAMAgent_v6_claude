#region 변경 이력
/*
 * Author : Link mskoo (2012. . )
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2012--   mskoo           최초 작성.
 * 
 * 2012--   mskoo           5. 버전 릴리즈. (변경 이력 정리)
 * ====================================================================================================================
 */
#endregion

using System;

namespace Link.EFAM.Security
{
    /// <summary>
    /// 파일이나 폴더에 대한 사용 권한을 지정한다.<br/>
    /// 이 열거형에는 멤버 값을 비트로 조합할 수 있는 <see cref="FlagsAttribute"/> 특성이 있다.
    /// </summary>
    [Serializable]
    [Flags]
    public enum FileIOPermissionAccess // FileIOPermissionFlags
    {
        /// <summary>
        /// 파일이나 폴더에 대한 권한이 없다.
        /// </summary>
        NoAccess = 0x00000000,
        /// <summary>
        /// 파일에 대한 데이터 읽기 권한이다.
        /// </summary>
        Read = 0x00000001,
        /// <summary>
        /// 파일에 대한 만들기/데이터 쓰기 권한이다.
        /// </summary>
        Write = 0x00000002,
        /// <summary>
        /// 파일에 대한 복사 권한이다.
        /// </summary>
        CopyFiles = 0x00000004,
        /// <summary>
        /// 폴더에 대한 내용 보기 권한이다.
        /// </summary>
        ListDirectory = 0x00000010,
        /// <summary>
        /// 폴더에 대한 만들기 권한이다.
        /// </summary>
        CreateDirectories = 0x00000020,
        /// <summary>
        /// 파일이나 폴더에 대한 삭제 권한이다.
        /// </summary>
        Delete = 0x00000100,
        /// <summary>
        /// 파일이나 폴더에 대한 이름 바꾸기 권한이다.
        /// </summary>
        Rename = 0x00000200,
        /// <summary>
        /// 파일이나 폴더에 대한 이동 권한이다.<br/>
        /// 이동하는 새 위치가 같은 네트워크 공유(또는 볼륨)인 경우에만 지정할 수 있다.
        /// </summary>
        Move = 0X00000400,
        /// <summary>
        /// 파일이나 폴더에 대한 모든 권한이 있다.
        /// </summary>
        AllAccess 
            = (Read | Write | CopyFiles | ListDirectory | CreateDirectories | Delete | Rename | Move),
    }
}
