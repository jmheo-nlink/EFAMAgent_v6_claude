#region 변경 이력
/*
 * Author : Link mskoo (2011. 3. 27)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-03-27   mskoo           최초 작성.
 *                              
 * 2011-09-23   mskoo           5.0 버전 릴리즈. (변경 이력 정리)
 * 
 * 2012-09-20   mskoo           액세스 권한의 종류를 추가.
 *                              - AllowListDirectory
 *                              - AllowMove
 *                              - Clone()
 *                              - ToString()
 * ====================================================================================================================
 */
#endregion

using System.Text;

namespace Link.EFAM.Common
{
    /// <summary>
    /// 파일이나 디렉토리에 적용할 수 있는 액세스 권한을 나타낸다.
    /// </summary>
    public sealed class FileAccessRights
    {
        private bool m_read = false;
        private bool m_write = false;
        private bool m_copyFiles = false;
        private bool m_listDir = false;
        private bool m_createDirs = false;
        private bool m_delete = false;
        private bool m_rename = false;
        private bool m_move = false;

        #region 속성

        /// <summary>
        /// 파일을 열 수 있는지 여부를 나타내는 값을 가져오거나 설정한다.
        /// </summary>
        /// <value>파일을 열 수 있으면 true, 그렇지 않으면 false. 기본값은 false</value>
        public bool AllowRead
        {
            get { return m_read; }
            set { m_read = value; }
        }

        /// <summary>
        /// 파일을 생성하고 파일에 데이터를 추가하고 제거할 수 있는지 여부를 나타내는 값을 가져오거나 설정한다.
        /// </summary>
        /// <value>
        /// 파일을 생성하고 파일에 데이터를 추가하고 제거할 수 있으면 true, 그렇지 않으면 false. 기본값은 false
        /// </value>
        public bool AllowWrite
        {
            get { return m_write; }
            set { m_write = value; }
        }

        /// <summary>
        /// 파일을 관리하는 네트워크 공유가 아닌 위치(로컬 디스크, 네트워크 공유 등)로
        /// 복사할 수 있는지 여부를 나타내는 값을 가져오거나 설정한다.
        /// </summary>
        /// <value>파일을 복사할 수 있으면 true, 그렇지 않으면 false. 기본값은 false</value>
        public bool AllowCopyFiles
        {
            get { return m_copyFiles; }
            set { m_copyFiles = value; }
        }

        /// <summary>
        /// 디렉토리의 내용을 읽을 수 있는지 여부를 나타내는 값을 가져오거나 설정한다.
        /// </summary>
        /// <value>디렉토리의 내용을 읽을 수 있으면 true, 그렇지 않으면 false. 기본값은 false</value>
        public bool AllowListDirectory
        {
            get { return m_listDir; }
            set { m_listDir = value; }
        }

        /// <summary>
        /// 디렉토리를 생성할 수 있는지 여부를 나타내는 값을 가져오거나 설정한다.
        /// </summary>
        /// <value>디렉토리를 생성할 수 있으면 true, 그렇지 않으면 false. 기본값은 false</value>
        public bool AllowCreateDirectories
        {
            get { return m_createDirs; }
            set { m_createDirs = value; }
        }

        /// <summary>
        /// 파일이나 디렉토리를 삭제할 수 있는지 여부를 나타내는 값을 가져오거나 설정한다.
        /// </summary>
        /// <value>파일이나 디렉토리를 삭제할 수 있으면 true, 그렇지 않으면 false. 기본값은 false</value>
        public bool AllowDelete
        {
            get { return m_delete; }
            set { m_delete = value; }
        }

        /// <summary>
        /// 파일이나 디렉토리의 이름을 바꿀 수 있는지 여부를 나타내는 값을 가져오거나 설정한다.
        /// </summary>
        /// <value>파일이나 디렉토리의 이름을 바꿀 수 있으면 true, 그렇지 않으면 false. 기본값은 false</value>
        public bool AllowRename
        {
            get { return m_rename; }
            set { m_rename = value; }
        }

        /// <summary>
        /// 파일이나 디렉토리를 새 위치로 이동할 수 있는지 여부를 나타내는 값을 가져오거나 설정한다.
        /// </summary>
        /// <value>파일이나 디렉토리를 새 위치로 이동할 수 있으면 true, 그렇지 않으면 false. 기본값은 falase</value>
        public bool AllowMove
        {
            get { return m_move; }
            set { m_move = value; }
        }

        #endregion

        #region 생성자

        /// <summary>
        /// <see cref="FileAccessRights"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        public FileAccessRights()
        {
        }

        #endregion 생성자

        #region 메소드

        /// <summary>
        /// 현재 인스턴스의 복사본인 새 <see cref="FileAccessRights"/> 개체를 만든다.
        /// </summary>
        /// <returns>현재 인스턴스의 복사본인 새 <see cref="FileAccessRights"/> 개체</returns>
        public FileAccessRights Clone()
        {
            FileAccessRights clone = new FileAccessRights();

            // 복사본의 속성을 설정한다.
            clone.AllowRead = m_read;
            clone.AllowWrite = m_write;
            clone.AllowCopyFiles = m_copyFiles;
            clone.AllowListDirectory = m_listDir;
            clone.AllowCreateDirectories = m_createDirs;
            clone.AllowDelete = m_delete;
            clone.AllowRename = m_rename;
            clone.AllowMove = m_move;

            return clone;
        }

        #endregion

        #region Object 멤버

        /// <summary>
        /// 허용된 액세스 권한을 나타내는 문자열을 반환한다.
        /// </summary>
        /// <returns>허용된 액세스 권한을 나타내는 문자열</returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append("Rights: ");
            if (m_read) builder.Append("'Read' ");
            if (m_write) builder.Append("'Write' ");
            if (m_copyFiles) builder.Append("'CopyFiles' ");
            if (m_listDir) builder.Append("'ListDirectory' ");
            if (m_createDirs) builder.Append("'CreateDirectories' ");
            if (m_delete) builder.Append("'Delete' ");
            if (m_rename) builder.Append("'Rename' ");
            if (m_move) builder.Append("'Move' ");

            return builder.ToString();
        }

        #endregion
    }
}
