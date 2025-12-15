#region 변경 이력
/*
 * Author : Link mskoo (2011. 8. 29)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-08-29   mskoo           최초 작성.
 * 
 * 2011-09-23   mskoo           5.0 버전 릴리즈.
 * 
 * 2012-01-27   mskoo           임시 파일이 생성되는 디렉토리들의 경로를 설정하는 부분의 버그를 수정.
 *                              - Activate(List<string>)
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Link.EFAM.Engine
{
    using Link.EFAM.Common;
    using Link.EFAM.Core;
    //using Link.EFAM.Engine.Filter;

    using Path = System.IO.Path;
    using Trace = System.Diagnostics.Trace;

    /// <summary>
    /// 원격 파일 및 디렉토리에 대한 액세스 제어를 나타낸다.
    /// </summary>
    public class FileAccessControl
    {
        private IFacFilterOperator m_fltOperator = null;
        private FacFilterAdapter m_fltAdapter = null;
        private IAccessControlBehavior m_behavior = null;
        private bool m_activated = false;

        #region 속성

        /// <summary>
        /// 액세스 제어와 연결된 동작을 가져오거나 설정한다.
        /// </summary>
        /// <value>액세스 제어와 연결된 <see cref="IAccessControlBehavior"/> 개체</value>
        public IAccessControlBehavior Behavior
        {
            get { return m_behavior; }
            set 
            {
                m_behavior = value;
                m_fltAdapter.Behavior = value;
            }
        }

        #endregion

        #region 생성자

        /// <summary>
        /// 지정한 사용자 자격 증명을 사용하여 <see cref="FileAccessControl"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        /// <param name="credential">사용자 자격 증명을 나타내는 <see cref="Credential"/> 개체</param>
        /// 
        /// <exception cref="ArgumentNullException">credential이 null인 경우</exception>
        public FileAccessControl(Credential credential)
        {
            m_fltAdapter = new FacFilterAdapter(credential);
            m_fltOperator = GetFacFilterOperator();
            m_fltOperator.ThreadCount = 8;
            m_fltOperator.RequestCount = 5;
            m_fltOperator.UseRecycleBin = credential.UseRecycleBin;
            m_fltOperator.AllowSaveAs = credential.AllowSaveAs;
            m_fltOperator.Adapter = m_fltAdapter;
        }

        #endregion

        #region 정적 메소드

        private static Assembly m_assembly = null;

        private static IFacFilterOperator GetFacFilterOperator()
        {
            string typeName = "Link.EFAM.Engine.Filter.FacFilterOperator";

            if (m_assembly == null)
            {
                string fileName = (IntPtr.Size == 4) ? "FltOptr32.dll" : "FltOptr64.dll";
                string path = null;

                path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                path = Path.Combine(path, fileName);

                m_assembly = Assembly.LoadFile(path);
            }

            return (IFacFilterOperator)m_assembly.CreateInstance(typeName);
        }

        #endregion

        #region 메소드

        /// <summary>
        /// 액세스 제어를 준비한다.
        /// </summary>
        public void Prepare()
        {
        }

        /// <summary>
        /// 액세스 제어를 활성화한다.
        /// </summary>
        /// <param name="shareNames">액세스를 제어할 네트워크 공유의 목록</param>
        /// 
        /// <exception cref="ArgumentNullException">shareNames가 null인 경우</exception>
        /// <exception cref="InvalidOperationException">
        /// 액세스 제어 미니필터 드라이버에 이미 연결되어 있는 경우<br/>
        /// - 또는 -<br/>
        /// 프로세스 정보를 얻는 데 사용되는 성능 카운터 API에 액세스하는 데 문제가 있는 경우
        /// </exception>
        /// <exception cref="System.Runtime.InteropServices.COMException">
        /// 인식할 수 없는 HRESULT가 WDK API 함수 호출에서 반환된 경우
        /// </exception>
        /// <exception cref="System.ComponentModel.Win32Exception">
        /// 시스템 API에 액세스하는 동안 오류가 발생한 경우
        /// </exception>
        /// <exception cref="OutOfMemoryException">
        /// 새 스레드를 시작할 충분한 메모리가 없는 경우<br/>
        /// - 또는 -<br/>
        /// 관리되지 않는 메모리에 메시지 버퍼에 할당할 충분한 메모리가 없는 경우
        /// </exception>
        public void Activate(List<string> shareNames)
        {
            if (shareNames == null) throw new ArgumentNullException("shareNames");

            List<string> tmpDirList = new List<string>();
            string tmpPath = null;
            bool connected = false;

            //
            // 사용자 지정 프로세스를 수행한다.
            //
            if (m_behavior != null) m_behavior.OnActivate(shareNames);

            try
            {
                m_fltOperator.ConnectFilter();
                connected = true;

                //
                // 필요한 경로들을 설정한다.
                //
                tmpPath = Environment.GetEnvironmentVariable("TEMP", EnvironmentVariableTarget.User);
                if (!String.IsNullOrEmpty(tmpPath)) tmpDirList.Add(tmpPath);
                //tmpDirList.Add(Environment.GetEnvironmentVariable("TEMP", EnvironmentVariableTarget.Machine));
                //tmpDirList.Add(Environment.GetEnvironmentVariable("windir"));
                tmpPath = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
                if (!String.IsNullOrEmpty(tmpPath)) tmpDirList.Add(tmpPath);
                tmpPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                if (!String.IsNullOrEmpty(tmpPath)) tmpDirList.Add(tmpPath);
                tmpPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                if (!String.IsNullOrEmpty(tmpPath)) tmpDirList.Add(tmpPath);

                m_fltOperator.SetRemotePaths(shareNames.ToArray());
                m_fltOperator.SetCacheDirectoryOfMsOffice();
                m_fltOperator.SetTemporaryPaths(tmpDirList.ToArray());

                // 액세스 제어를 활성화한다.
                m_fltOperator.ActivateFilter();
                m_activated = true;
            } // try
            finally
            {
                if (!m_activated && connected)
                {
                    try { m_fltOperator.DisconnectFilter(); }
                    catch { }
                }
            } // finally
        }

        /// <summary>
        /// 액세스 제어를 비활성화한다.
        /// </summary>
        /// 
        /// <exception cref="System.Runtime.InteropServices.COMException">
        /// 인식할 수 없는 HRESULT가 WDK API 함수 호출에서 반환된 경우
        /// </exception>
        public void Deactivate()
        {
            if (!m_activated) return;

            // 액세스 제어를 비활성화한다.
            //m_fltOperator.DeactivateFilter();
            m_fltOperator.DisconnectFilter();
            m_activated = false;
        }

        #endregion
    }
}
