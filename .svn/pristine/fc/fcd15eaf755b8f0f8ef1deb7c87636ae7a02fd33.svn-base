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
 * 
 * 2012-04-12   mskoo           KryptonDataGridView 컨트롤은 비활성화하지 않도록 수정.
 *                              - EnableInputControls
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
// E-FAM 관련
using Link.EFAM.Engine.InternalServices;
using Link.EFAM.Engine.Services;

using AsmblyResource = Link.EFAM.Agent.Properties.Resources;

namespace Link.EFAM.Agent.UI
{
    public partial class ViewLogDialog : KryptonForm
    {
        //
        // 추적/로그
        //
        private static BooleanSwitch m_tracing = new BooleanSwitch("traceSwitch", "Agent Module");
        private ILog m_logger = LogManager.GetLogger(typeof(ViewLogDialog));

        #region 속성

        /// <summary>
        /// 입력 컨트롤들이 사용자 상호 작용에 응답할 수 있는지 여부를 나타내는 값을 설정한다.
        /// </summary>
        /// <value>입력 컨트롤들이 사용자 상호 작용에 응답할 수 있으면 true, 그렇지 않으면 false</value>
        private bool EnableInputControls
        {
            set
            {
                txtFileName.Enabled = value;
                cboAction.Enabled = value;
                dtpkrBegin.Enabled = value;
                dtpkrEnd.Enabled = value;
                btnSearch.Enabled = value;
                //grdLogList.Enabled = value;
            } // set
        }

        #endregion

        #region 생성자

        /// <summary>
        /// <see cref="ViewLogDialog"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        public ViewLogDialog()
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
            //
            // 폼
            //
            this.Text = AsmblyResource.ViewLogDialog_Text;
            columnPath.HeaderText = AsmblyResource.Column_FilePath;
            columnNewPath.HeaderText = AsmblyResource.Column_NewFilePath;
            columnAction.HeaderText = AsmblyResource.Column_FileAction;
            columnDate.HeaderText = AsmblyResource.Column_Date;
            //
            // 검색 조건
            //
            lblFileName.Text = AsmblyResource.Label_FileName;
            lblAction.Text = AsmblyResource.Label_FileAction;
            lblBeginDate.Text = AsmblyResource.Label_BeginDate;
            lblEndDate.Text = AsmblyResource.Label_EndDate;
            dtpkrBegin.CalendarTodayText = AsmblyResource.Calendar_Today;
            dtpkrEnd.CalendarTodayText = AsmblyResource.Calendar_Today;
            btnSearch.Text = AsmblyResource.Button_Search;
            btnClose.Text = AsmblyResource.Button_Close;
        }

        #endregion

        #region 이벤트 핸들러
        #region 백그라운드 작업

        /// <summary>
        /// 백그라운드 작업이 시작될 때 다른 스레드에서 필요한 작업을 진행한다.
        /// </summary>
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorkerArgument bgwkArg = e.Argument as BackgroundWorkerArgument;
            EFAMService service = new EFAMService(AgentStore.Store.UserCredential);

            // 로그를 검색한다.
            e.Result = service.SearchLog(
                bgwkArg.FileNameValue, bgwkArg.ActionValue, bgwkArg.BeginDateValue, bgwkArg.EndDateValue);
        }

        /// <summary>
        /// 백그라운드 작업자가 완료(성공, 실패 또는 취소)되었을 때 필요한 작업을 진행한다.
        /// </summary>
        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //
            // 오류가 발생했을 경우
            //
            if (e.Error != null)
            {
                Exception exception = e.Error;
                string message = null;

                this.UseWaitCursor = false;
                this.EnableInputControls = true;

                if (exception is System.Net.WebException)
                {
                    message = AsmblyResource.Error_WebExceptionThrowed;
                }
                else if (exception is System.Web.Services.Protocols.SoapException)
                {
                    message = AsmblyResource.Error_SoapExceptionThrowed;
                }
                else message = AsmblyResource.Error_ExceptionThrowed;

                KryptonMessageBox.Show(message, AsmblyResource.MsgBox_Caption,
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);

                //
                // 오류 로그를 기록하고 추적 메시지를 쓴다.
                //
                string logMessage = "ViewLogDialog.bgWorker_DoWork() \n" + exception;

                if (m_logger.IsErrorEnabled) m_logger.Error(logMessage);
                if (m_tracing.Enabled) Trace.WriteLine("[EFAM.Error] " + logMessage);
            } // if (e.Error != null)
            //
            // 백그라운드 작업이 완료되었을 경우
            //
            else if (!e.Cancelled)
            {
                FileLog[] searchedLogs = e.Result as FileLog[];
                DataGridViewRow addedRow = null;

                foreach (FileLog log in searchedLogs)
                {
                    addedRow = grdLogList.Rows[grdLogList.Rows.Add()];

                    addedRow.Cells[0].Value = log.Src;
                    addedRow.Cells[1].Value = log.Dest;
                    addedRow.Cells[2].Value = log.Action;
                    addedRow.Cells[3].Value = log.Date;
                } // foreach ( FileLog )

                this.UseWaitCursor = false;
                this.EnableInputControls = true;
            } // else if (!e.Cancelled)
        }

        #endregion

        /// <summary>
        /// 폼을 로드할 때 필요한 작업을 진행한다.
        /// </summary>
        private void ViewLogDialog_Load(object sender, EventArgs e)
        {
            string[] items = { AsmblyResource.Label_All, 
                                 "OPEN", "MODIFY", "CREATE", "DELETE", "MOVE", "RENAME", "COPY" };

            cboAction.Items.Clear();
            cboAction.Items.AddRange(items);
            cboAction.SelectedIndex = 0;
        }

        /// <summary>
        /// 폼을 닫을 때마다 또는 폼이 닫히기 전에 필요한 작업을 진행한다.
        /// </summary>
        private void ViewLogDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = bgWorker.IsBusy;
            }
        }

        #region 버튼 이벤트

        /// <summary>
        /// "Search" 버튼을 클릭할 때 필요한 작업을 진행한다.
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            grdLogList.Rows.Clear();

            ///////////////////////////////////////////////////////////////////////////////////////
            // 백그라운드 작업으로 로그를 조회한다.
            //
            BackgroundWorkerArgument argument = new BackgroundWorkerArgument();

            this.UseWaitCursor = true;
            this.EnableInputControls = false;

            argument.FileNameValue = txtFileName.Text;
            argument.ActionValue = (cboAction.SelectedIndex == 0) ? "" : (string)cboAction.SelectedItem;
            argument.BeginDateValue = dtpkrBegin.Checked ? dtpkrBegin.Value.ToString("yyyy-MM-dd") : "";
            argument.EndDateValue = dtpkrEnd.Checked ? dtpkrEnd.Value.ToString("yyyy-MM-dd") : "";

            // 백그라운드 작업의 실행을 시작한다.
            bgWorker.RunWorkerAsync(argument);
        }

        /// <summary>
        /// "Close" 버튼을 클릭할 때 필요한 작업을 진행한다.
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
        #endregion

        #region INNER 클래스

        /// <summary>
        /// 백그라운드 작업에 전달할 인수를 나타낸다.
        /// </summary>
        private class BackgroundWorkerArgument
        {
            public string FileNameValue = null;
            public string ActionValue = null;
            public string BeginDateValue = null;
            public string EndDateValue = null;
        }

        #endregion
    }
}
