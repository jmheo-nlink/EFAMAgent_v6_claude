#region 변경 이력
/*
 * Author : Link mskoo (2011. 9. 19)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-09-19   mskoo           최초 작성.
 * 
 * 2011-09-23   mskoo           5.0 버전 릴리즈.
 * 
 * 2013-02-20   mskoo           소프트캠프 "S-WORK 보안 탐색기"의 프로세스 종류를 "Windows 탐색기"로 설정하도록 수정.
 *                              - GetProcessInfoManager()
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
// E-FAM 관련
using Link.EFAM.Common;

namespace Link.EFAM.Engine
{
    /// <summary>
    /// 실행 중인 프로세스에 대한 정보를 관리한다.
    /// </summary>
    /// <remarks>
    /// 이 형식은 스레드로부터 안전하다.
    /// </remarks>
    internal class ProcessInfoManager
    {
        //
        // 추적/로그
        //
        private static BooleanSwitch m_tracing = new BooleanSwitch("traceSwitch", "Engine Module");

        private Dictionary<int, ProcessInfo> m_data = null;
        //private ProcessKindDictionary m_kindDic = null;
        private Dictionary<string, ProcessKind> m_kindDic = null;
        private object m_syncObject = null;

        #region 속성

        /*
        /// <summary>
        /// 프로세스를 시작한 실행 파일을 만들 때 사용한 이름과 프로세스 종류의 매핑 사전을 가져온다.
        /// </summary>
        /// <value>프로세스 종류의 매핑 사전인 <see cref="ProcessKindDictionary"/> 개체</value>
        public ProcessKindDictionary ProcessKindMap
        {
            get { return m_kindDic; }
        }
         */

        #endregion

        #region 생성자

        /// <summary>
        /// <see cref="ProcessInfoManager"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        private ProcessInfoManager()
        {
            m_data = new Dictionary<int, ProcessInfo>(128);
            //m_kindDic = new ProcessKindDictionary();
            m_kindDic = new Dictionary<string, ProcessKind>(128, StringComparer.OrdinalIgnoreCase);
            m_syncObject = ((System.Collections.ICollection)m_data).SyncRoot;

            // 프로세스의 종류를 설정한다.
            //InitializeProcessKindMap();
        }

        #endregion

        #region 메소드
        #region Singleton 인스턴스

        private static ProcessInfoManager m_instance = null;

        /// <summary>
        /// 캐시된 <see cref="ProcessInfoManager"/> 인스턴스를 반환한다.
        /// </summary>
        /// <returns>캐시된 <see cref="ProcessInfoManager"/> 개체</returns>
        public static ProcessInfoManager GetProcessInfoManager()
        {
            if (m_instance == null)
            {
                m_instance = new ProcessInfoManager();

                //
                // 프로세스의 종류를 설정한다.
                //

                // 시스템 프로세스
                m_instance.m_kindDic["alg.exe"] = ProcessKind.SystemProcess; // Application Layer Gateway Service
                m_instance.m_kindDic["conime.exe"] = ProcessKind.SystemProcess; // Console IME
                m_instance.m_kindDic["ctfmon.exe"] = ProcessKind.SystemProcess; // CTF Loader
                m_instance.m_kindDic["audiodg.exe"] = ProcessKind.SystemProcess; // Windows 오디오 장치 그래프 격리
                m_instance.m_kindDic["csrss.exe"] = ProcessKind.SystemProcess; // Client Server Runtime Process
                m_instance.m_kindDic["dwm.exe"] = ProcessKind.SystemProcess; // 데스크톱 창 관리자
                m_instance.m_kindDic["lsass.exe"] = ProcessKind.SystemProcess; // Local Security Authority Process
                m_instance.m_kindDic["lsm.exe"] = ProcessKind.SystemProcess; // 로컬 세션 관리자 서비스
                //kindMap["mstsc.exe"] = ProcessKind.SystemProcess; // 원격 데스크톱 연결
                m_instance.m_kindDic["SearchIndexer.exe"] = ProcessKind.SystemProcess; // Microsoft Windows Search 인덱서
                m_instance.m_kindDic["services.exe"] = ProcessKind.SystemProcess; // 서비스 및 컨트롤러 응용 프로그램
                m_instance.m_kindDic["smss.exe"] = ProcessKind.SystemProcess; // Windows 세션 관리자
                m_instance.m_kindDic["spoolsv.exe"] = ProcessKind.SystemProcess; // Spooler SubSystem App
                m_instance.m_kindDic["svchost.exe"] = ProcessKind.SystemProcess; // Host Process for Windows Services
                m_instance.m_kindDic["rundll32.exe"] = ProcessKind.SystemProcess; // Windows 호스트 프로세스(Rundll32)
                m_instance.m_kindDic["taskhost.exe"] = ProcessKind.SystemProcess; // Windows 작업을 위한 호스트 프로세스
                m_instance.m_kindDic["wininit.exe"] = ProcessKind.SystemProcess; // Windows 시작 응용 프로그램
                m_instance.m_kindDic["winlogon.exe"] = ProcessKind.SystemProcess; // Windows 로그온 응용 프로그램
                m_instance.m_kindDic["WmiPrvSE.exe"] = ProcessKind.SystemProcess; // WMI Provider Host
                m_instance.m_kindDic["wuauclt.exe"] = ProcessKind.SystemProcess; // Windows Update
                m_instance.m_kindDic["WUDFHost.exe"] = ProcessKind.SystemProcess; // Windows 드라이버 파운데이션 
                                                                                  // - 사용자 모드 드라이버 프레임워크 호스트 프로세스

                // "Windows 탐색기" 프로세스
                m_instance.m_kindDic["explorer.exe"] = ProcessKind.WindowsExplorer;
                // "Windows 명령 처리기" 프로세스
                m_instance.m_kindDic["cmd.exe"] = ProcessKind.WindowsCommand;

                // "Microsoft Office" 응용 프로그램의 프로세스 (Excel, PowerPoint, Word)
                m_instance.m_kindDic["excel.exe"] = ProcessKind.MsOffice;
                m_instance.m_kindDic["powerpnt.exe"] = ProcessKind.MsOffice;
                m_instance.m_kindDic["winword.exe"] = ProcessKind.MsOffice;

                // "S-WORK 보안 탐색기" 프로세스
                m_instance.m_kindDic["vsdvw.exe"] = ProcessKind.WindowsExplorer;
            } // if (m_instance == null)

            return m_instance;
        }

        #endregion

        /*
        /// <summary>
        /// 프로세스 종류의 매핑 사전을 초기화한다.
        /// </summary>
        /// <remarks>
        /// 프로세스 종류의 매핑 사전에 프로세스 종류들을 설정한다.
        /// </remarks>
        private void InitializeProcessKindMap()
        {
            //
            // 시스템 프로세스
            //
            m_kindDic["alg.exe"] = ProcessKind.SystemProcess;         // Application Layer Gateway Service
            m_kindDic["conime.exe"] = ProcessKind.SystemProcess;      // Console IME
            m_kindDic["ctfmon.exe"] = ProcessKind.SystemProcess;      // CTF Loader

            m_kindDic["audiodg.exe"] = ProcessKind.SystemProcess;     // Windows 오디오 장치 그래프 격리
            m_kindDic["csrss.exe"] = ProcessKind.SystemProcess;       // Client Server Runtime Process
            m_kindDic["dwm.exe"] = ProcessKind.SystemProcess;         // 데스크톱 창 관리자
            m_kindDic["lsass.exe"] = ProcessKind.SystemProcess;       // Local Security Authority Process
            m_kindDic["lsm.exe"] = ProcessKind.SystemProcess;         // 로컬 세션 관리자 서비스
            //kindMap["mstsc.exe"] = ProcessKind.SystemProcess;       // 원격 데스크톱 연결
            m_kindDic["SearchIndexer.exe"] = ProcessKind.SystemProcess;       // Microsoft Windows Search 인덱서
            m_kindDic["services.exe"] = ProcessKind.SystemProcess;    // 서비스 및 컨트롤러 응용 프로그램
            m_kindDic["smss.exe"] = ProcessKind.SystemProcess;        // Windows 세션 관리자
            m_kindDic["spoolsv.exe"] = ProcessKind.SystemProcess;     // Spooler SubSystem App
            m_kindDic["svchost.exe"] = ProcessKind.SystemProcess;     // Host Process for Windows Services
            m_kindDic["rundll32.exe"] = ProcessKind.SystemProcess;    // Windows 호스트 프로세스(Rundll32)
            m_kindDic["taskhost.exe"] = ProcessKind.SystemProcess;    // Windows 작업을 위한 호스트 프로세스
            m_kindDic["wininit.exe"] = ProcessKind.SystemProcess;     // Windows 시작 응용 프로그램
            m_kindDic["winlogon.exe"] = ProcessKind.SystemProcess;    // Windows 로그온 응용 프로그램
            m_kindDic["WmiPrvSE.exe"] = ProcessKind.SystemProcess;    // WMI Provider Host
            m_kindDic["wuauclt.exe"] = ProcessKind.SystemProcess;     // Windows Update
            m_kindDic["WUDFHost.exe"] = ProcessKind.SystemProcess;    // Windows 드라이버 파운데이션 
            // - 사용자 모드 드라이버 프레임워크 호스트 프로세스

            // "Windows 탐색기" 프로세스
            m_kindDic["explorer.exe"] = ProcessKind.WindowsExplorer;
            // "Windows 명령 처리기" 프로세스
            m_kindDic["cmd.exe"] = ProcessKind.WindowsCommand;

            // "Microsoft Office" 응용 프로그램의 프로세스 (Excel, PowerPoint, Word)
            m_kindDic["excel.exe"] = ProcessKind.MsOffice;
            m_kindDic["powerpnt.exe"] = ProcessKind.MsOffice;
            m_kindDic["winword.exe"] = ProcessKind.MsOffice;

            // "SoftCamp 보안 탐색기" 프로세스
            m_kindDic["vsdvw.exe"] = ProcessKind.WindowsExplorer;
        }
         */

        /// <summary>
        /// 지정한 프로세스 ID의 프로세스에 대한 정보를 생성한다.
        /// </summary>
        /// <param name="processId">프로세스에 할당된 프로세스 ID</param>
        /// <returns>새 <see cref="ProcessInfo"/> 개체</returns>
        /// 
        /// <exception cref="ArgumentException">processId 매개 변수에서 지정한 프로세스가 실행되지 않는 경우</exception>
        public ProcessInfo Create(int processId)
        {
            ProcessInfo procInfo = new ProcessInfo(processId);
            ProcessKind procKind;

            if (m_kindDic.TryGetValue(procInfo.OriginalFilename, out procKind))
            {
                procInfo.Kind = procKind;
            }

            // 컬렉션에 추가한다.
            lock (m_syncObject)
            {
                m_data[processId] = procInfo;
            } // lock

            // 추적 메시지를 쓴다.
            if (m_tracing.Enabled)
            {
                Trace.WriteLine(
                    String.Format("[EFAM] {0} [{1}] process is added.", procInfo, procInfo.Kind));
            }

            return procInfo;
        }

        /// <summary>
        /// 지정한 프로세스 ID를 가지는 프로세스 정보를 가져온다.
        /// </summary>
        /// <param name="processId">가져올 프로세스 정보의 프로세스 ID</param>
        /// <returns>
        /// 지정한 프로세스 ID를 가지는 <see cref="ProcessInfo"/> 개체. 지정한 프로세스 ID의 프로세스가 없으면 null
        /// </returns>
        public ProcessInfo Get(int processId)
        {
            ProcessInfo procInfo = null;
            bool contained = false;

            try
            {
                lock (m_syncObject)
                {
                    contained = m_data.TryGetValue(processId, out procInfo);
                } // lock

                if (!contained) procInfo = Create(processId); 
            } // try
            catch (ArgumentException) { }

            return procInfo;
        }

        /// <summary>
        /// 지정한 프로세스 ID를 가지는 프로세스 정보를 제거한다.
        /// </summary>
        /// <param name="processId">제거할 프로세스 정보의 프로세스 ID</param>
        public void Remove(int processId)
        {
            ProcessInfo procInfo = null;

            lock (m_syncObject)
            {
                // 추적 메시지를 쓴다.
                if (m_tracing.Enabled 
                    && m_data.TryGetValue(processId, out procInfo))
                {
                    Trace.WriteLine(String.Format("[EFAM] {0} process is removed.", procInfo));
                }

                m_data.Remove(processId);
            } // lock
        }

        /// <summary>
        /// 모든 프로세스 정보들을 제거한다.
        /// </summary>
        public void Clear()
        {
            lock (m_syncObject)
            {
                m_data.Clear();
            } // lock
        }

        #endregion
    }
}
