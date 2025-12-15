#region 변경 이력
/*
 * Author : Link mskoo (2011. 9. 17)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-09-17   mskoo           최초 작성.
 * 
 * 2011-09-23   mskoo           5.0 버전 릴리즈.
 * ====================================================================================================================
 */
#endregion

using System;

namespace Link.EFAM.Common
{
    /// <summary>
    /// 파일이나 디렉토리에 대한 액세스 규칙(허용되거나 거부된 액세스 권한의 조합)을 나타낸다.
    /// </summary>
    public sealed class FileAccessRule
    {
        private string m_path = null;
        private FileAccessRights m_fileRights = null;

        #region 속성

        /// <summary>
        /// 규칙을 적용할 파일이나 디렉토리의 경로를 가져온다.
        /// </summary>
        /// <value>규칙을 적용할 파일이나 디렉토리의 경로</value>
        public string Path
        {
            get { return m_path; }
        }

        /// <summary>
        /// 액세스 규칙에 의해 허용되거나 거부된 액세스 권한을 가져오거나 설정한다.
        /// </summary>
        /// <value>액세스 규칙에 의해 허용되거나 거부된 액세스 권한을 나타내는 <see cref="FileAccessRights"/> 개체</value>
        public FileAccessRights AccessRights
        {
            get { return m_fileRights; }
            set 
            {
                m_fileRights = (value != null) ? value : (new FileAccessRights());
            }
        }

        #endregion

        #region 생성자

        /// <summary>
        /// <see cref="FileAccessRule"/> 클래스의 새 인스턴스를 초기화하여 
        /// 규칙을 적용할 파일이나 디렉토리의 경로를 지정한다.
        /// </summary>
        /// <param name="path">규칙을 적용할 파일이나 디렉토리의 경로</param>
        /// 
        /// <exception cref="ArgumentNullException">path가 null인 경우</exception>
        public FileAccessRule(string path)
            : this(path, null)
        {
        }

        /// <summary>
        /// <see cref="FileAccessRule"/> 클래스의 새 인스턴스를 초기화하여 
        /// 규칙을 적용할 파일이나 디렉토리의 경로, 허용하거나 거부할 액세스 권한을 지정한다.
        /// </summary>
        /// <param name="path">규칙을 적용할 파일이나 디렉토리의 경로</param>
        /// <param name="fileRights">허용하거나 거부할 액세스 권한을 나타내는 <see cref="FileAccessRights"/> 개체</param>
        /// 
        /// <exception cref="ArgumentNullException">path가 null인 경우</exception>
        public FileAccessRule(string path, FileAccessRights fileRights)
        {
            if (path == null) throw new ArgumentNullException("path");

            m_path = path;
            this.AccessRights = fileRights;
        }

        #endregion

        #region Object 멤버

        /// <summary>
        /// 현재 인스턴스를 나타내는 문자열을 반환한다.
        /// </summary>
        /// <returns>현재 인스턴스를 나타내는 문자열</returns>
        /// <remarks>
        /// <see cref="Path"/> 및 <see cref="AccessRights"/> 속성 문자열의 조합이다.
        /// </remarks>
        public override string ToString()
        {
            string toStr = null;

            toStr = String.Format("Path: {0}  {1}", m_path, m_fileRights);

            return toStr;
        }

        #endregion
    }
}
