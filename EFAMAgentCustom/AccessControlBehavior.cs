#region 변경 이력
/*
 * Author : Link mskoo (2011. 10. 6)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-10-06   mskoo           최초 작성.
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.Text;
// E-FAM 관련
using Link.EFAM.Common;
using Link.EFAM.Engine;

namespace Link.EFAM.Agent.Custom
{
    /// <summary>
    /// 액세스 제어와 관련된 작업에 대한 런타임 동작을 제어한다.
    /// </summary>
    public class AccessControlBehavior : IAccessControlBehavior
    {
        #region 생성자

        /// <summary>
        /// <see cref="AccessControlBehavior"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        public AccessControlBehavior()
        {
        }

        #endregion

        #region IAccessControlBehavior 멤버

        /// <summary>
        /// 원격 파일 및 디렉토리에 대한 액세스 제어를 활성화하기 전에 추가로 사용자 지정 프로세스를 수행한다.
        /// </summary>
        /// <param name="shareNames">액세스를 제어할 네트워크 공유의 목록</param>
        public void OnActivate(List<string> shareNames)
        {
        }

        /// <summary>
        /// 지정한 경로에 대해 허용되거나 거부된 액세스 권한을 반환하기 전에 추가로 사용자 지정 프로세스를 수행한다.
        /// </summary>
        /// <param name="fileRights">반환할 액세스 권한을 나타내는 <see cref="FileAccessRights"/> 개체</param>
        /// <param name="processInfo">
        /// 액세스 권한을 요청한 프로세스에 대한 정보를 나타내는 <see cref="ProcessInfo"/> 개체
        /// </param>
        /// <param name="path">파일 또는 디렉토리의 경로</param>
        /// <param name="isDir">디렉토리의 경로이면 true, 파일의 경로이면 false</param>
        public void OnGetPermissions(FileAccessRights fileRights, ProcessInfo processInfo, string path, bool isDir)
        {
            switch (processInfo.Kind)
            {
                case ProcessKind.MsOffice:
                    if (fileRights.AllowWrite) fileRights.AllowRename = true;
                    if (!fileRights.AllowDelete) fileRights.AllowDelete = true;
                    break;
            } // switch (processInfo.Kind)
        }

        /// <summary>
        /// 파일 또는 디렉토리에 대해 수행된 작업의 로그를 기록하기 전에 추가로 사용자 지정 프로세스를 수행한다.
        /// </summary>
        /// <param name="processInfo">작업을 수행한 프로세스에 대한 정보를 나타내는 <see cref="ProcessInfo"/> 개체</param>
        /// <param name="path">파일 또는 디렉토리의 경로</param>
        /// <param name="newPath">파일 또는 디렉토리의 새 경로</param>
        /// <param name="action">
        /// 파일 또는 디렉토리에 대해 수행된 작업을 지정하는 <see cref="FileAction"/> 값 중 하나
        /// </param>
        /// <returns>파일 또는 디렉토리에 대해 수행된 작업의 로그를 기록하려면 true, 그렇지 않으면 false</returns>
        /// <remarks>
        /// action 매개 변수가 다음의 값 중 하나일 경우 newPath 매개 변수는 다음과 같은 의미를 갖는다.<br/>
        /// FileDeleted - 휴지통 디렉토리로 백업된 파일의 경로.<br/>
        /// FileRenamed - 새 파일 이름.<br/>
        /// FileMoved - 파일의 새 위치에 대한 경로.<br/>
        /// FileCopied - 관리하는 네트워크 공유가 아닌 위치(로컬 디스크, 네트워크 공유 등)로 복사된 파일의 새 경로.<br/>
        /// DirectoryRenamed - 새 디렉토리 이름.<br/>
        /// DirectoryMoved - 디렉토리의 새 위치에 대한 경로.
        /// </remarks>
        public bool OnWriteLog(ProcessInfo processInfo, string path, string newPath, FileAction action)
        {
            if (action == FileAction.FileOpened) return false;

            return true;
        }

        #endregion
    }
}
