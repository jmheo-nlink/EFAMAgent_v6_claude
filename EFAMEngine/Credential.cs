#region 변경 이력
/*
 * Author : Link mskoo (2011. 4. 19)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-04-19   mskoo           최초 작성.
 *                              
 * 2011-09-23   mskoo           5.0 버전 릴리즈. (변경 이력 정리)
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace Link.EFAM.Engine
{
    /// <summary>
    /// E-FAM 사용자의 자격 증명을 나타낸다.
    /// </summary>
    public sealed class Credential
    {
        private Uri m_url = null;
        private string m_userId = String.Empty;
        private bool m_authed = false;
        private bool m_isManager = false;
        private bool m_recycleBin = false;
        private bool m_saveAs = false;

        #region 속성

        /// <summary>
        /// E-FAM 서버의 URL을 가져오거나 설정한다.
        /// </summary>
        /// <value>E-FAM 서버의 URL</value>
        public Uri ServerUrl
        {
            get { return m_url; }
            internal set { m_url = value; }
        }

        /// <summary>
        /// 자격 증명과 관련된 사용자 ID를 가져온다.
        /// </summary>
        /// <value>자격 증명과 관련된 사용자 ID. 기본값은 빈 문자열("")</value>
        public string UserId
        {
            get { return m_userId; }
            internal set
            {
                m_userId = (value != null) ? value : String.Empty;
            }
        }

        /// <summary>
        /// 사용자가 인증되었는지 여부를 나타내는 값을 가져온다.
        /// </summary>
        /// <value>사용자가 인증되었으면 true, 그렇지 않으면 false</value>
        public bool IsAuthenticated
        {
            get { return m_authed; }
            internal set { m_authed = value; }
        }

        /// <summary>
        /// 사용자가 부서 관리자인지 여부를 나타내는 값을 가져온다.
        /// </summary>
        /// <value>사용자가 부서 관리자이면 true, 그렇지 않으면 false</value>
        public bool IsManager
        {
            get { return m_isManager; }
            internal set { m_isManager = value; }
        }

        #region 서버 프로파일

        /// <summary>
        /// 휴지통 기능을 사용하는지 여부를 나타내는 값을 가져온다.
        /// </summary>
        /// <value>휴지통 기능을 사용하면 true, 그렇지 않으면 false</value>
        public bool UseRecycleBin
        {
            get { return m_recycleBin; }
            internal set { m_recycleBin = value; }
        }

        /// <summary>
        /// 파일을 로컬 디스크에 저장할 수 있는지 여부는 나타내는 값을 가져온다.
        /// </summary>
        /// <value>파일을 로컬 디스크에 저장할 수 있으면 true, 그렇지 않으면 false</value>
        public bool AllowSaveAs
        {
            get { return m_saveAs; }
            internal set { m_saveAs = value; }
        }

        #endregion
        #endregion

        #region 생성자

        /// <summary>
        /// 지정한 사용자 ID를 사용하여 <see cref="Credential"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        /// <param name="userId">자격 증명과 관련된 사용자 ID</param>
        internal Credential(string userId)
        {
            this.UserId = userId;
        }

        #endregion
    }
}
