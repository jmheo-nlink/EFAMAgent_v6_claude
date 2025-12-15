#region 변경 이력
/*
 * Author : Link mskoo (2011. 4. 18)
 * 
 * ====================================================================================================================
 * Date         Name            Description of Change
 * --------------------------------------------------------------------------------------------------------------------
 * 2011-04-18   mskoo           최초 작성.
 * 
 * 2011-09-23   mskoo           5.0 버전 릴리즈. (변경 이력 정리)
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
// log4net 라이브러리
using log4net;
// "Krypton Toolkit" 라이브러리
using ComponentFactory.Krypton.Toolkit;
// .NET용 개발 라이브러리
using Link.DLK.Security;
// E-FAM 관련
using Link.EFAM.Engine.Services;
using Link.EFAM.Agent.Properties;

namespace Link.EFAM.Agent.UI
{
    public partial class ChangePasswordDialog : KryptonForm
    {
        //
        // 추적/로그
        //
        private static BooleanSwitch m_tracing = new BooleanSwitch("traceSwitch", "Agent Module");
        private ILog m_logger = LogManager.GetLogger(typeof(ChangePasswordDialog));

        private PasswordStrengthChecker m_passwordChecker = null;
        private bool m_changed = false;

        #region 속성

        /// <summary>
        /// 비밀번호가 변경되었는지 여부를 나타내는 값을 가져온다.
        /// </summary>
        /// <value>비밀번호가 변경되었으면 true, 그렇지 않으면 false</value>
        public bool HasChanged
        {
            get { return m_changed; }
        }

        /// <summary>
        /// 입력 컨트롤들이 사용자 상호 작용에 응답할 수 있는지 여부를 나타내는 값을 설정한다.
        /// </summary>
        /// <value>입력 컨트롤들이 사용자 상호 작용에 응답할 수 있으면 true, 그렇지 않으면 false</value>
        private bool EnableInputControls
        {
            set
            {
                txtOldPassword.Enabled = value;
                txtNewPassword.Enabled = value;
                txtConfirmPassword.Enabled = value;
                btnOK.Enabled = value;
                btnCancel.Enabled = value;
            } // set
        }

        #endregion

        #region 생성자

        /// <summary>
        /// <see cref="ChangePasswordDialog"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        public ChangePasswordDialog()
        {
            InitializeComponent();
            SetLocalizedText();

            m_passwordChecker = new PasswordStrengthChecker(8);
        }

        #endregion

        #region 메소드

        /// <summary>
        /// 각 컨트롤에 지역화된 텍스트를 설정한다.
        /// </summary>
        private void SetLocalizedText()
        {
            //
            // 폼
            //
            this.Text = Resources.ChangePasswordDialog_Text;
            lblOldPassword.Text = Resources.Label_OldPassword;
            lblNewPassword.Text = Resources.Label_NewPassword;
            lblConfirmPassword.Text = Resources.Label_ConfirmPassword;
            btnOK.Text = Resources.Button_OK;
            btnCancel.Text = Resources.Button_Cancel;
        }

        #endregion

        #region 이벤트 핸들러
        #region 백그라운드 작업

        /// <summary>
        /// 백그라운드 작업이 시작될 때 다른 스레드에서 필요한 작업을 진행한다.
        /// </summary>
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            AgentStore store = AgentStore.Store;
            string oldPassword = txtOldPassword.Text;
            string newPassword = txtNewPassword.Text;

            if (oldPassword != store.InputedPassword)
            {
                throw new LoginErrorException(LoginErrorCode.WrongPassword);
            }

            //
            // 사용자의 비밀번호를 변경한다.
            //
            if (store.IsAuthenticated)
            {
                EFAMService service = new EFAMService(store.UserCredential);

                e.Result = service.ChangeUserPassword(newPassword);
            }
            else
            {
                e.Result = AuthenticationService.ChangePassword(
                                Settings.Default.ServerUrl, store.InputedUserId, newPassword);
            }
        }

        /// <summary>
        /// 백그라운드 작업자가 완료(성공, 실패 또는 취소)되었을 때 필요한 작업을 진행한다.
        /// </summary>
        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.UseWaitCursor = false;
            this.EnableInputControls = true;

            //
            // 오류가 발생했을 경우
            //
            if (e.Error != null)
            {
                Exception exception = e.Error;
                string message = null;

                if (exception is LoginErrorException)
                {
                    message = Resources.Error_WrongOldPassword;
                    exception = null;
                }
                else if (exception is System.Net.WebException)
                {
                    message = Resources.Error_WebExceptionThrowed;
                }
                else if (exception is System.Web.Services.Protocols.SoapException)
                {
                    message = Resources.Error_SoapExceptionThrowed;
                }
                else message = Resources.Error_ExceptionThrowed;

                KryptonMessageBox.Show(message, Resources.MsgBox_Caption,
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);

                //
                // 오류 로그를 기록하고 추적 메시지를 쓴다.
                //
                if (exception != null)
                {
                    string logMessage = String.Format("ChangePasswordDialog.bgWorker_DoWork() - {0}\n{1}",
                                            AgentStore.Store.InputedUserId, exception);

                    if (m_logger.IsErrorEnabled) m_logger.Error(logMessage);
                    if (m_tracing.Enabled) Trace.WriteLine("[EFAM.Error] " + logMessage);
                } // if (exception != null)
            } // if (e.Error != null)
            //
            // 백그라운드 작업이 완료되었을 경우
            //
            else if (!e.Cancelled)
            {
                m_changed = (bool)e.Result;

                //
                // 성공했을 경우
                //
                if (m_changed)
                {
                    // 변경된 비밀번호를 데이터 저장소에 설정한다.
                    AgentStore.Store.InputedPassword = txtNewPassword.Text;

                    KryptonMessageBox.Show(Resources.Info_ChangePasswordSuccess, 
                                           Resources.MsgBox_Caption);
                    this.Close();
                } // if (m_changed)
                //
                // 실패했을 경우
                //
                else
                {
                    KryptonMessageBox.Show(Resources.Error_ChangePasswordFailure,
                        Resources.MsgBox_Caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } // else if (!e.Cancelled)

            txtOldPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
        }

        #endregion

        /// <summary>
        /// 폼을 로드할 때 필요한 작업을 진행한다.
        /// </summary>
        private void ChangePasswordDialog_Load(object sender, EventArgs e)
        {
            m_changed = false;
        }

        /// <summary>
        /// 폼을 닫을 때마다 또는 폼이 닫히기 전에 필요한 작업을 진행한다.
        /// </summary>
        private void ChangePasswordDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = bgWorker.IsBusy;
            }
        }

        #region 버튼 이벤트

        /// <summary>
        /// "OK" 버튼을 클릭할 때 필요한 작업을 진행한다.
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            string oldPassword = txtOldPassword.Text;
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            ///////////////////////////////////////////////////////////////////////////////////////
            // 입력한 새 비밀번호가 유효한 값(안전한 비밀번호)인지 확인한다.
            //
            if (oldPassword.Length == 0)
            {
                KryptonMessageBox.Show(Resources.Warn_InputOldPassword, Resources.MsgBox_Caption, 
                                       MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtOldPassword.Focus();
                return;
            }
            if (newPassword.Length == 0)
            {
                KryptonMessageBox.Show(Resources.Warn_InputNewPassword, Resources.MsgBox_Caption, 
                                       MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtNewPassword.Focus();
                return;
            }
            if (confirmPassword.Length == 0)
            {
                KryptonMessageBox.Show(Resources.Warn_InputConfirmPassword, Resources.MsgBox_Caption, 
                                       MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtConfirmPassword.Focus();
                return;
            }
            if (newPassword != confirmPassword)
            {
                KryptonMessageBox.Show(Resources.Warn_NewPwdAndConfirmPwdMismatch, 
                    Resources.MsgBox_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtConfirmPassword.Focus();
                return;
            }
            if (newPassword == oldPassword)
            {
                KryptonMessageBox.Show(Resources.Warn_NewPwdIsEqualToOldPwd,
                    Resources.MsgBox_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtNewPassword.Focus();
                return;
            }
            if (newPassword.ToUpper() == AgentStore.Store.InputedUserId.ToUpper())
            {
                KryptonMessageBox.Show(Resources.Warn_NewPwdIsEqualToUserId,
                    Resources.MsgBox_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtNewPassword.Focus();
                return;
            }
            if (!m_passwordChecker.CheckLength(newPassword)
                || !m_passwordChecker.CheckComplexity(newPassword))
            {
                string text = String.Format(Resources.Warn_InputStrongPassword, 
                                            m_passwordChecker.MinRequiredPasswordLength);
                KryptonMessageBox.Show(text, Resources.MsgBox_Caption, 
                                       MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtNewPassword.Focus();
                return;
            }
            
            ///////////////////////////////////////////////////////////////////////////////////////
            // 백그라운드 작업으로 사용자의 비밀번호를 변경한다.
            //
            this.UseWaitCursor = true;
            this.EnableInputControls = false;

            // 백그라운드 작업의 실행을 시작한다.
            bgWorker.RunWorkerAsync();
        }

        #endregion

        /// <summary>
        /// TextBox 컨트롤이 폼의 활성 컨트롤이 될 때 필요한 작업을 진행한다. 
        /// </summary>
        private void TextBox_Enter(object sender, EventArgs e)
        {
            ((KryptonTextBox)sender).SelectAll();
        }

        /// <summary>
        /// TextBox 컨트롤에 포커스가 있을 때 사용자가 키를 누르면 필요한 작업을 진행한다.
        /// </summary>
        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;

                if (sender == txtOldPassword) txtNewPassword.Focus();
                else if (sender == txtNewPassword) txtConfirmPassword.Focus();
                else if (sender == txtConfirmPassword) btnOK.PerformClick();
            } // if (e.KeyChar == (char)Keys.Enter)
        }

        #endregion

        #region Form 멤버

        /// <summary>
        /// 폼을 현재 활성 창이 소유자로 설정된 모달 대화 상자로 표시합니다.
        /// </summary>
        /// <returns>비밀번호가 변경되었으면 true, 그렇지 않으면 false</returns>
        public new bool ShowDialog()
        {
            base.ShowDialog();

            return m_changed;
        }

        /// <summary>
        /// 폼을 지정된 소유자가 있는 모달 대화 상자로 표시합니다.
        /// </summary>
        /// <param name="owner">
        /// 모달 대화 상자를 소유할 최상위 창을 나타내는 <see cref="IWin32Window"/>를 구현하는 개체
        /// </param>
        /// <returns>비밀번호가 변경되었으면 true, 그렇지 않으면 false</returns>
        public new bool ShowDialog(IWin32Window owner)
        {
            base.ShowDialog(owner);

            return m_changed;
        }

        #endregion
    }
}
