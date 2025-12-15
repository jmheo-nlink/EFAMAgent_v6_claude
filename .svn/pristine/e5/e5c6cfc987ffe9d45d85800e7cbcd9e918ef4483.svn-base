#region 변경 이력
/*
 * Author : Link mskoo (2011. 9. 22)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-09-22   mskoo           최초 작성.
 * 
 * 2011-09-23   mskoo           5.0 버전 릴리즈.
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Resource = Link.EFAM.Engine.Properties.Resources;

namespace Link.EFAM.Engine
{
    /// <summary>
    /// 휴지통에 있는 휴지통 항목을 나타낸다.
    /// </summary>
    /// <remarks>
    /// 휴지통 항목은 휴지통으로 이동된 파일을 나타낸다.
    /// </remarks>
    public class RecycleBinItem
    {
        private FileInfo m_internalFile = null;
        private string m_dirName = null;
        private string m_fileName = null;
        private string m_deletedBy = null;
        private DateTime m_deletedDate;
        private bool m_restored = false;
        private bool m_deleted = false;

        #region 속성

        /// <summary>
        /// 휴지통에 있는 파일의 내부 경로를 가져온다.
        /// </summary>
        /// <value>휴지통에 있는 파일의 내부 경로</value>
        public string InternalFilePath
        {
            get { return m_internalFile.FullName; }
        }

        /// <summary>
        /// 휴지통 항목이 원래 위치했던 디렉터리의 전체 경로를 가져온다.
        /// </summary>
        /// <value>휴지통 항목이 원래 위치했던 디렉터리의 전체 경로</value>
        public string DirectoryName
        {
            get { return m_dirName; }
        }

        /// <summary>
        /// 휴지통 항목의 이름을 가져온다.
        /// </summary>
        /// <value>휴지통 항목의 이름</value>
        /// <remarks>
        /// 파일의 이름을 나타낸다.
        /// </remarks>
        public string Name
        {
            get { return m_fileName; }
        }

        /// <summary>
        /// 휴지통 항목이 휴지통으로 이동된 날짜를 가져온다.
        /// </summary>
        /// <value>휴지통 항목이 휴지통으로 이동된 <see cref="DateTime"/> 값</value>
        public DateTime DeletedDate
        {
            get { return m_deletedDate; }
        }

        /// <summary>
        /// 휴지통 항목을 삭제한 사용자의 ID를 가져온다.
        /// </summary>
        /// <value>휴지통 항목을 삭제한 사용자의 ID</value>
        public string DeletedBy
        {
            get { return m_deletedBy; }
        }

        /// <summary>
        /// 휴지통 항목이 영구적으로 삭제되었는지 여부를 나타내는 값을 가져온다.
        /// </summary>
        /// <value>휴지통 항목이 영구적으로 삭제되었으면 true, 그렇지 않으면 false</value>
        public bool IsDeleted
        {
            get { return m_deleted; }
        }

        /// <summary>
        /// 휴지통 항목이 복원되었는지 여부를 나타내는 값을 가져온다.
        /// </summary>
        /// <value>휴지통 항목이 복원되었으면 true, 그렇지 않으면 false</value>
        public bool IsRestored
        {
            get { return m_restored; }
        }

        #endregion

        #region 생성자

        /// <summary>
        /// <see cref="RecycleBinItem"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        /// <param name="internalPath">파일의 현재 위치를 나타내는 경로</param>
        /// <param name="originalPath">파일의 원래 위치를 나타내는 경로</param>
        /// <param name="deletedDate">파일이 삭제된 <see cref="DateTime"/> 값</param>
        /// <param name="deletedBy">파일을 삭제한 사용자의 ID</param>
        /// 
        /// <exception cref="ArgumentNullException">internalPath 또는 originalPath가 null인 경우</exception>
        /// <exception cref="ArgumentException">
        /// internalPath 또는 originalPath가 길이가 0인 문자열이거나 공백만 있거나 잘못된 문자가 포함되어 있는 경우
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// internalPath 또는 originalPath가 시스템에 정의된 최대 길이보다 긴 경우
        /// </exception>
        public RecycleBinItem(string internalPath, string originalPath, DateTime deletedDate, string deletedBy)
        {
            if (internalPath == null) throw new ArgumentNullException("internalPath");
            if (originalPath == null) throw new ArgumentNullException("originalPath");

            m_internalFile = new FileInfo(internalPath);
            // 디렉토리 경로와 파일 이름을 분리한다.
            m_dirName = Path.GetDirectoryName(originalPath);
            m_fileName = Path.GetFileName(originalPath);
            m_deletedDate = deletedDate;
            m_deletedBy = (deletedBy != null) ? deletedBy : String.Empty;
        }

        #endregion

        #region 메소드

        /// <summary>
        /// 휴지통 항목을 영구적으로 삭제한다.
        /// </summary>
        /// 
        /// <exception cref="ArgumentException">휴지통 항목을 찾을 수 없는 경우</exception>
        /// <exception cref="IOException">파일을 삭제하는 동안 I/O 오류가 발생한 경우</exception>
        /// <exception cref="UnauthorizedAccessException">호출자에게 필요한 권한이 없는 경우</exception>
        public void Delete()
        {
            if (m_restored || m_deleted)
            {
                throw new ArgumentException(
                    String.Format(Resource.Error_FileNotFound, m_internalFile.FullName));
            }

            //
            // 파일을 삭제한다.
            //
            try
            {
                m_internalFile.IsReadOnly = false;
                m_internalFile.Delete();
                m_deleted = true;
            } // try
            catch (FileNotFoundException)
            {
                throw new ArgumentException(
                    String.Format(Resource.Error_FileNotFound, m_internalFile.FullName));
            }
        }

        /// <summary>
        /// 휴지통 항목을 원래 위치로 복원한다. 원래 위치에 이름이 같은 파일이 있으면 엎어쓸 수 없다.
        /// </summary>
        /// 
        /// <exception cref="ArgumentException">휴지통 항목을 찾을 수 없는 경우</exception>
        /// <exception cref="IOException">
        /// 파일을 복원할 디렉토리를 만들 수 없는 경우<br/>
        /// - 또는 -<br/>
        /// 파일을 복원하는 동안 I/O 오류가 발생한 경우
        /// </exception>
        /// <exception cref="RecycleBinException">원래 위치에 이름이 같은 파일이 이미 있는 경우</exception>
        /// <exception cref="UnauthorizedAccessException">호출자에게 필요한 권한이 없는 경우</exception>
        public void Restore()
        {
            if (m_restored || m_deleted)
            {
                throw new ArgumentException(
                    String.Format(Resource.Error_FileNotFound, m_internalFile.FullName));
            }

            string destFilePath = Path.Combine(m_dirName, m_fileName);
            bool copying = false;

            //
            // 파일을 복원할 디렉토리를 생성한다.
            //
            if (!Directory.Exists(m_dirName))
            {
                Directory.CreateDirectory(m_dirName);
            }

            //
            // 파일을 원래 위치로 이동한다.
            //
            try
            {
                m_internalFile.IsReadOnly = false;
                copying = true;
                m_internalFile.MoveTo(destFilePath);
                m_restored = true;
            } // try
            catch (FileNotFoundException)
            {
                throw new ArgumentException(
                    String.Format(Resource.Error_FileNotFound, m_internalFile.FullName));
            }
            catch (IOException ioExc)
            {
                if (copying && File.Exists(destFilePath))
                {
                    throw new RecycleBinException(
                        String.Format(Resource.Error_FileAlreadyExists, destFilePath), ioExc);
                }

                throw ioExc;
            } // catch (IOException)
        }

        #endregion
    }
}
