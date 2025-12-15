#region 변경 이력
/*
 * Author : Link mskoo (2011. 5. 27)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-05-27   mskoo           최초 작성.
 * 
 * 2011-09-23   mskoo           5.0 버전 릴리즈.
 * 
 * 2013-11-16   jake.9          네임스페이스를 Link.EFAM.Engine.Filter 에서 Link.EFAM.Core 로 변경.
 * ====================================================================================================================
 */
#endregion

using System;

namespace Link.EFAM.Core
{
    using Link.EFAM.Common;

    /// <summary>
    /// ProcessCreated 이벤트에 대한 데이터를 제공한다.
    /// </summary>
    public class ProcessCreatedEventArgs : EventArgs
    {
        private ProcessKind m_kind;
        private int m_processId;

        #region 속성

        /// <summary>
        /// 생성된 프로세스에 할당된 프로세스 ID(식별자)를 가져온다.
        /// </summary>
        /// <value>생성된 프로세스에 할당된 프로세스 ID</value>
        public int ProcessId
        {
            get { return m_processId; }
        }

        /// <summary>
        /// 프로세스의 종류를 가져오거나 설정한다.
        /// </summary>
        /// <value>프로세스의 종류를 나타내는 <see cref="ProcessKind"/> 값 중 하나.</value>
        public ProcessKind Kind
        {
            get { return m_kind; }
            set { m_kind = value; }
        }

        #endregion

        #region 생성자

        /// <summary>
        /// 생성된 프로세스에 할당된 프로세스 ID(식별자)를 사용하여 
        /// <see cref="ProcessCreatedEventArgs"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        /// <param name="processId">프로세스에 할당된 프로세스 ID</param>
        public ProcessCreatedEventArgs(int processId)
        {
            m_processId = processId;
            m_kind = ProcessKind.NormalProcess;
        }

        #endregion
    }
}
