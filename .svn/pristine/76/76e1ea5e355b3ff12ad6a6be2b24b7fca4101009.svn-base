#region 변경 이력
/*
 * Author : jake.9 (2013. 11. 17)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2013-11-17   jake.9          최초 작성.
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace Link.EFAM.Core
{
    public interface IFacFilterOperator
    {
        #region 속성

        /// <summary>
        /// 생성할 작업 스레드의 개수를 가져오거나 설정한다.
        /// </summary>
        /// <value>생성할 작업 스레드의 개수</value>
        int ThreadCount { get; set; }

        /// <summary>
        /// 각 작업 스레드에서 처리할 요청의 개수를 가져오거나 설정한다.
        /// </summary>
        /// <value>각 작업 스레드에서 처리할 요청의 개수</value>
        int RequestCount { get; set; }

        /// <summary>
        /// 파일을 로컬 디스크에 저장할 수 있는지 여부를 나타내는 값을 가져오거나 설정한다.
        /// </summary>
        /// <value>파일을 로컬 디스크에 저장할 수 있으면 <b>true</b>, 그렇지 않으면 <b>false</b></value>
        bool AllowSaveAs { get; set; }

        /// <summary>
        /// 휴지통 기능을 사용할지 여부를 나타내는 값을 가져오거나 설정한다.
        /// </summary>
        /// <value>휴지통 기능을 사용하려면 <b>true</b>, 그렇지 않으면 <b>false</b></value>
        bool UseRecycleBin { get; set; }

        /// <summary>
        /// 휴지통 디렉토리의 이름을 가져온다.
        /// </summary>
        /// <value>휴지통 디렉토리의 이름</value>
        string RecycleBinDirectoryName { get; }
        
        /// <summary>
        /// 미니필터 드라이버에서 요청하는 작업을 처리하는 어댑터를 가져오거나 설정한다.
        /// </summary>
        /// <value>미니필터 드라이버에서 요청하는 작업을 처리하는 <see cref="IFacFilterAdapter"/></value>
        IFacFilterAdapter Adapter { get; set; }

        #endregion

        #region 메소드

        /// <summary>
        /// 액세스 제어 미니필터 드라이버에 연결한다.
        /// </summary>
        void ConnectFilter();

        /// <summary>
        /// 액세스 제어 미니필터 드라이버와 연결을 해제한다.
        /// </summary>
        void DisconnectFilter();

        /// <summary>
        /// 원격 파일 및 디렉토리에 대한 액세스 제어를 활성화한다.
        /// </summary>
        void ActivateFilter();
        
        /// <summary>
        /// 원격 파일 및 디렉토리에 대한 액세스 제어를 비활성화한다.
        /// </summary>
        void DeactivateFilter();
        
        /// <summary>
        /// 원격 파일 및 디렉토리에 대한 액세스를 제어할 원격 경로들을 설정한다.
        /// </summary>
        /// <param name="paths">액세스를 제어할 원격 경로들의 배열</param>
        void SetRemotePaths(string[] paths);
        
        /// <summary>
        /// "Microsoft Office" 응용 프로그램의 캐시 디렉토리의 경로를 설정한다.
        /// </summary>
        void SetCacheDirectoryOfMsOffice();
        
        /// <summary>
        /// 응용 프로그램들의 임시 파일이 생성되는 경로들을 설정한다.
        /// </summary>
        /// <param name="paths">임시 파일이 생성되는 경로들의 배열</param>
        void SetTemporaryPaths(string[] paths);

        #endregion
    }
}
