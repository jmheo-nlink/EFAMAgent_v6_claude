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

namespace Link.EFAM.Security.AccessControl
{
    using Link.EFAM.Security.Principal;
    using AppResource = Link.EFAM.AppResources.AppResource;

    /// <summary>
    /// 파일이나 디렉토리에 적용할 수 있는 액세스 권한을 지정한다.<br/>
    /// 이 열거형에는 멤버 값을 비트로 조합할 수 있는 <see cref="FlagsAttribute"/> 특성이 있다.
    /// </summary>
    [Flags]
    public enum FileSystemRights
    {
        /// <summary>
        /// 파일을 읽기 전용으로 열 수 있는 권한이다.
        /// </summary>
        Read = 0x00000001,
        /// <summary>
        /// 파일을 만들고, 파일에서 데이터를 추가하고 제거할 수 있는 권한이다.
        /// </summary>
        Write = 0x00000002,
        /// <summary>
        /// 파일을 복사할 수 있는 권한이다.
        /// </summary>
        CopyFiles = 0x00000004,
        /// <summary>
        /// 디렉토리의 내용을 읽을 수 있는 권한이다.
        /// </summary>
        ListDirectory = 0x00000010,
        /// <summary>
        /// 디렉토리를 만들 수 있는 권한이다.
        /// </summary>
        CreateDirectories = 0x00000020,
        /// <summary>
        /// 파일이나 디렉토리를 삭제할 수 있는 권한이다.
        /// </summary>
        Delete = 0x00000100,
        /// <summary>
        /// 파일이나 디렉토리의 이름을 바꿀 수 있는 권한이다.
        /// </summary>
        Rename = 0x00000200,
        /// <summary>
        /// 파일이나 디렉토리를 새 위치로 이동할 수 있는 권한이다.<br/>
        /// 이동하는 새 위치가 같은 네트워크 공유(또는 볼륨)인 경우에만 지정할 수 있다.
        /// </summary>
        Move = 0X00000400,
        /// <summary>
        /// 파일이나 디렉토리에 대해 모든 작업을 할 수 있는 권한이다.<br/>
        /// 이 값은 이 열거형의 모든 권한을 조합한 것이다.
        /// </summary>
        FullControl
            = (Read | Write | CopyFiles | ListDirectory | CreateDirectories | Delete | Rename | Move),
    }

    /// <summary>
    /// 파일이나 디렉토리에 대한 액세스 규칙을 나타낸다.<br/>
    /// 사용자 또는 그룹에 대해 허용된 액세스 권한 집합을 나타낸다.
    /// </summary>
    public class FileSystemAccessRule
    {
        private Account m_identity = null;
        private int m_accMask = 0;
        private bool m_noInherit = false;
        private bool m_noPropagate = false;

        #region 속성

        /// <summary>
        /// 이 규칙을 적용할 <see cref="Account"/>를 가져온다.
        /// </summary>
        /// <value>이 규칙을 적용할 <see cref="Account"/></value>
        public Account Identity
        {
            get { return m_identity; }
        }

        /// <summary>
        /// 이 규칙의 액세스 마스크를 가져온다.
        /// </summary>
        /// <value>이 규칙의 액세스 마스크</value>
        internal int AccessMask
        {
            get { return m_accMask; }
        }

        /// <summary>
        /// 이 규칙에 의해 허용된 액세스 권한을 가져온다.
        /// </summary>
        /// <value>이 규칙에 의해 허용된 액세스 권한을 나타내는 <see cref="FileSystemRights"/> 값의 비트 조합</value>
        public FileSystemRights Rights
        {
            get { return (FileSystemRights)m_accMask; }
        }

        /// <summary>
        /// 액세스 규칙을 부모 컨테이너 개체에서 상속할 것인지 여부를 나타내는 값을 가져온다.
        /// </summary>
        /// <value>액세스 규칙을 부모 컨테이너 개체에서 상속하지 않으면 <b>true</b></value>
        public bool NoInherit
        {
            get { return m_noInherit; }
        }

        /// <summary>
        /// 액세스 규칙이 자식 컨테이너 개체로 전파되는지 여부를 나타내는 값을 가져온다.
        /// </summary>
        /// <value>액세스 규칙이 자식 컨테이너 개체로 전파되지 않으면 <b>true</b></value>
        public bool NoPropagate
        {
            get { return m_noPropagate; }
        }

        #endregion

        #region 생성자

        /// <summary>
        /// 지정한 값을 사용하여 <see cref="FileSystemAccessRule"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        /// <param name="identity">규칙을 적용할 사용자나 그룹</param>
        /// <param name="rights">허용 권한을 지정하는 <see cref="FileSystemRights"/> 값의 비트 조합</param>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="identity"/>가 <b>null</b>인 경우</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="rights"/>가 잘못된 값을 지정하는 경우</exception>
        public FileSystemAccessRule(Account identity, FileSystemRights rights)
            : this(identity, rights, false, false)
        {
        }
        
        /// <summary>
        /// 지정한 값을 사용하여 <see cref="FileSystemAccessRule"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        /// <param name="identity">규칙을 적용할 사용자나 그룹</param>
        /// <param name="rights">허용 권한을 지정하는 <see cref="FileSystemRights"/> 값의 비트 조합</param>
        /// <param name="noInherit">액세스 규칙을 부모 컨테이너 개체에서 상속하지 않으면 <b>true</b></param>
        /// <param name="noPropagate">액세스 규칙이 자식 컨테이너 개체로 전파되지 않으면 <b>true</b></param>
        /// 
        /// <exception cref="ArgumentNullException"><paramref name="identity"/>가 <b>null</b>인 경우</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="rights"/>가 잘못된 값을 지정하는 경우</exception>
        public FileSystemAccessRule(Account identity, FileSystemRights rights, bool noInherit, bool noPropagate)
        {
            if (identity == null) throw new ArgumentNullException("identity");
            if (rights < (FileSystemRights)0 || rights > FileSystemRights.FullControl)
            {
                throw new ArgumentOutOfRangeException("rights", 
                    AppResource.GetString("Argument_InvalidEnumValue", rights, "FileSystemRights"));
            }

            m_identity = identity;
            m_accMask = (int)rights;
            m_noInherit = noInherit;
            m_noPropagate = noPropagate;
        }

        #endregion

    }
}
