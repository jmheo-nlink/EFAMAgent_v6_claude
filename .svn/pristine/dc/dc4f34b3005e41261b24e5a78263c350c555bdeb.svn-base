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
 * 2011-09-23   mskoo           5.0 버전 릴리즈. (변경 이력 정리)
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
// .NET용 개발 라이브러리
using Link.DLK.Collections.Generic;
using Link.DLK.Win32;
// E-FAM 관련
using Link.EFAM.Common;

namespace Link.EFAM.Engine
{
    /// <summary>
    /// 현재 실행 중인 프로세스에 대한 정보를 제공한다.
    /// </summary>
    public class ProcessInfo
    {
        private Process m_process = null;       // 연결된 프로세스 구성 요소
        private ProcessKind m_kind = ProcessKind.NormalProcess;
        private int m_processId = 0;
        private string m_filePath = String.Empty;
        private string m_fileName = null;
        private string m_orgFileName = null;
        private bool m_exited = false;
        private CreatedFileCollection m_createdFileColl = null;

        #region 속성

        /// <summary>
        /// 프로세스에 할당된 프로세스 ID(식별자)를 가져온다.
        /// </summary>
        /// <value>시스템에서 할당하는 프로세스의 ID</value>
        public int ProcessId
        {
            get { return m_processId; }
        }

        /// <summary>
        /// 프로세스를 시작한 실행 파일의 전체 경로를 가져온다.
        /// </summary>
        /// <value>프로세스를 시작한 실행 파일의 전체 경로</value>
        public string ExecutablePath
        {
            get { return m_filePath; }
        }

        /// <summary>
        /// 프로세스를 시작한 실행 파일의 이름을 가져온다.
        /// </summary>
        /// <value>프로세스를 시작한 실행 파일의 이름</value>
        public string FileName
        {
            get { return m_fileName; }
        }

        /// <summary>
        /// 프로세스를 시작한 실행 파일을 만들 때 사용한 이름을 가져온다.
        /// </summary>
        /// <value>프로세스를 시작한 실행 파일을 만들 때 사용한 이름</value>
        public string OriginalFilename
        {
            get { return m_orgFileName; }
        }

        /// <summary>
        /// 프로세스가 종료되었는지 여부를 나타내는 값을 가져온다.
        /// </summary>
        /// <value>프로세스가 종료되었으면 true, 그렇지 않으면 false</value>
        public bool HasExited
        {
            get { return m_exited; }
        }

        /// <summary>
        /// 프로세스의 종류를 가져오거나 설정한다.
        /// </summary>
        /// <value>
        /// 프로세스의 종류를 나타내는 <see cref="ProcessKind"/> 값 중 하나.
        /// 기본값은 <see cref="ProcessKind.NormalProcess"/>
        /// </value>
        /// 
        /// <exception cref="System.ComponentModel.InvalidEnumArgumentException">
        /// 지정한 값이 유효한 값 범위를 벗어난 경우
        /// </exception>
        public ProcessKind Kind
        {
            get { return m_kind; }
            set
            {
                int intValue = (int)value;

                if (intValue < 0 || intValue >= (int)ProcessKind.MaxProcessKind)
                {
                    throw new System.ComponentModel.InvalidEnumArgumentException(
                        "value", intValue, typeof(ProcessKind));
                }

                m_kind = value;
            } // set
        }

        /// <summary>
        /// 프로세스에서 생성한 파일의 컬렉션을 가져온다.
        /// </summary>
        /// <value>프로세스에서 생성한 파일의 컬렉션을 나타내는 <see cref="CreatedFileCollection"/> 개체</value>
        public CreatedFileCollection CreatedFiles
        {
            get { return m_createdFileColl; }
        }

        #endregion

        #region 이벤트

        /// <summary>
        /// 프로세스가 종료될 때 발생한다.
        /// </summary>
        public event EventHandler ProcessExited;

        #endregion

        #region 생성자

        /// <summary>
        /// 프로세스 ID(식별자)를 사용하여 <see cref="ProcessInfo"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        /// <param name="processId">프로세스에 할당된 프로세스 ID</param>
        /// 
        /// <exception cref="ArgumentException">processId 매개 변수에서 지정한 프로세스가 실행되지 않는 경우</exception>
        public ProcessInfo(int processId)
        {
            m_createdFileColl = new CreatedFileCollection();
            m_process = Process.GetProcessById(processId);

            //if (!String.Equals(m_process.ProcessName, "V3SP", StringComparison.OrdinalIgnoreCase) &&
            //    !String.Equals(m_process.ProcessName, "V3Svc", StringComparison.OrdinalIgnoreCase))
            //{
                try
                {
                    // 이벤트 핸들러를 설정한다.
                    m_process.EnableRaisingEvents = true;
                    m_process.Exited += new EventHandler(Process_Exited);
                } // try
                catch { }
            //}

            SetAll();
        }

        #endregion

        #region 소멸자

        /// <summary>
        /// 인스턴스가 할당한 리소스를 모두 해제한다.
        /// </summary>
        ~ProcessInfo()
        {
            // 프로세스 구성 요소에 연결된 리소스를 해제한다.
            if (m_process != null) m_process.Close();
        }

        #endregion

        #region 메소드

        /// <summary>
        /// 프로세스에 대한 정보를 설정한다.
        /// </summary>
        private void SetAll()
        {
            ///////////////////////////////////////////////////////////////////////////////////////
            // 기본 정보들을 설정한다.
            //
            m_processId = m_process.Id;

            //
            // 유휴 프로세스 및 시스템 프로세스
            // System Idle Process, PID 0
            // System (NT Kernel & System), PID 4
            //
            if (m_processId == 0 || m_processId == 4)
	        {
                m_fileName = (m_processId == 0) ? String.Empty : m_process.ProcessName;
                m_orgFileName = m_fileName;
                m_kind = ProcessKind.SystemProcess;
            }
            else
            {
                string exePath = null;
                string originalName = null;

                try
                {
                    exePath = WindowsUtility.GetExecutablePath(m_process.Handle);
                    originalName = FileVersionInfo.GetVersionInfo(exePath).OriginalFilename;
                }
                catch { }

                m_filePath = (exePath != null) ? exePath : String.Empty;
                m_fileName = 
                    (exePath != null) ? System.IO.Path.GetFileName(exePath) : m_process.ProcessName;

                //
                // 파일을 만들 때 사용한 이름을 설정한다.
                //
                if (originalName == null) m_orgFileName = m_fileName;
                else
                {
                    int foundIndex = 0;

                    foundIndex = 
                        originalName.LastIndexOf(".MUI", StringComparison.CurrentCultureIgnoreCase);
                    m_orgFileName = 
                        (foundIndex == -1) ? originalName : originalName.Remove(foundIndex, 4);
                } // else
            } // else
        }

        #endregion

        #region 이벤트 핸들러

        /// <summary>
        /// 프로세스가 종료될 때 필요한 작업을 진행한다.
        /// </summary>
        private void Process_Exited(object sender, EventArgs e)
        {
            m_process.Close();
            m_exited = true;
            m_createdFileColl.Clear();

            if (ProcessExited != null) ProcessExited(this, e);
        }

        #endregion

        #region Object 멤버

        /// <summary>
        /// 현재 인스턴스를 나타내는 문자열을 반환한다.
        /// </summary>
        /// <returns>현재 인스턴스를 나타내는 문자열</returns>
        /// <remarks>
        /// <see cref="FileName"/> 및 <see cref="ProcessId"/> 속성 문자열의 조합이다.
        /// </remarks>
        public override string ToString()
        {
            string toStr = null;

            toStr = String.Format("{0} (PID {1})", m_fileName, m_processId);

            return toStr;
        }

        #endregion

        #region INNER 클래스

        /// <summary>
        /// 프로세스에서 생성한 파일의 컬렉션을 나타낸다.
        /// </summary>
        public class CreatedFileCollection : SynchronizedCollection<string>
        {
        }

        #endregion
    }
}
