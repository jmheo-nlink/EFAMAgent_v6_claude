#region 변경 이력
/*
 * Author : Link mskoo (2013. 3. 13)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2013-03-13   mskoo           최초 작성.
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
// "Krypton Suite" 라이브러리
using ComponentFactory.Krypton.Toolkit;

namespace Link.EFAM.Agent.UI
{
    using Link.EFAM.Agent.Properties;
    using Link.EFAM.Engine.Services;
    using UserInfo = Link.EFAM.Engine.InternalServices.UserInfo;

    public partial class ManagerInfoDialog : KryptonForm
    {
        private static BooleanSwitch m_tracing = new BooleanSwitch("traceSwitch", "Agent Module");

        private UserInfo[] m_managerList = new UserInfo[0];

        #region 속성

        /// <summary>
        /// 관리자 목록을 가져오거나 설정한다.
        /// </summary>
        /// <value>관리자를 나타내는 <see cref="UserInfo"/> 개체의 배열</value>
        public UserInfo[] ManagerList
        {
            get { return m_managerList; }
            set 
            {
                m_managerList = (value != null) ? value : new UserInfo[0];
            }
        }

        #endregion

        #region 생성자

        /// <summary>
        /// <see cref="ManagerInfoDialog"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        /// 
        public ManagerInfoDialog()
        {
            InitializeComponent();
            SetLocalizedText();
        }

        #endregion

        #region 메소드

        /// <summary>
        /// 각 컨트롤에 지역화된 텍스트를 설정한다.
        /// </summary>
        private void SetLocalizedText()
        {
            // 폼
            this.Text = Resources.ManagerInfoDialog_Text;
            grpContent.Text = Resources.Label_Manager;
            lblName.Text = Resources.Label_Name;
            lblJobTitle.Text = Resources.Label_JobTitle;
            lblDepartment.Text = Resources.Label_Department;
            lblPhoneNumber.Text = Resources.Label_PhoneNumber;
            lblEmail.Text = Resources.Label_Email;
            btnOK.Text = Resources.Button_OK;
        }

        #endregion

        #region 이벤트 핸들러

        /// <summary>
        /// 폼을 로드할 때 필요한 작업을 진행한다.
        /// </summary>
        private void ManagerInfoDialog_Load(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
            lstManagerList.Enabled = false;

            foreach (UserInfo userInfo in m_managerList)
            {
                lstManagerList.Items.Add(userInfo);
            }
            if (m_managerList.Length != 0) lstManagerList.SelectedIndex = 0;

            lstManagerList.Enabled = true;
            this.UseWaitCursor = false;
        }

        private void lstManagerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UserInfo manager = lstManagerList.SelectedItem as UserInfo;

            if (manager != null)
            {
                grpContent.Text = manager.Description;
                txtName.Text = manager.UserName;
                txtJobTitle.Text = manager.JobTitle;
                txtDepartment.Text = manager.Department;
                txtPhoneNumber.Text = manager.PhoneNumber;
                txtEmail.Text = manager.Email;
            }
        }

        #endregion
    }
}
