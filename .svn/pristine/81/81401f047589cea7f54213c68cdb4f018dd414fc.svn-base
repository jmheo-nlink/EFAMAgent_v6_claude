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
 *                              
 * 2012-10-12   mskoo           휴지통에 있는 파일들을 모두 삭제하는 '휴지통 비우기' 기능을 추가.
 *                              - SetLocalizedText()
 *                              - btnEmpty_Click(object, EventArgs)
 *                              - bgwkEmpty_DoWork(object, DoWorkEventArgs)
 *                              - bgwkEmpty_ProgressChanged(object, ProgressChangedEventArgs)
 *                              - BackgroundWorker_RunWorkerCompleted(object, RunWorkerCompletedEventArgs)
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
// log4net 라이브러리
using log4net;
// "Krypton Toolkit" 라이브러리
using ComponentFactory.Krypton.Toolkit;
// E-FAM 관련
using Link.EFAM.Engine;
using Link.EFAM.Engine.InternalServices;
using Link.EFAM.Engine.Services;

using AsmblyResource = Link.EFAM.Agent.Properties.Resources;

namespace Link.EFAM.Agent.UI
{
    public partial class RecycleBinDialog : KryptonForm
    {
        //
        // 추적/로그
        //
        private static BooleanSwitch m_tracing = new BooleanSwitch("traceSwitch", "Agent Module");
        private ILog m_logger = LogManager.GetLogger(typeof(RecycleBinDialog));

        #region 속성

        /// <summary>
        /// 입력 컨트롤들이 사용자 상호 작용에 응답할 수 있는지 여부를 나타내는 값을 설정한다.
        /// </summary>
        /// <value>입력 컨트롤들이 사용자 상호 작용에 응답할 수 있으면 true, 그렇지 않으면 false</value>
        private bool EnableInputControls
        {
            set
            {
                dtpkrBegin.Enabled = value;
                dtpkrEnd.Enabled = value;
                btnSearch.Enabled = value;
                btnRestore.Enabled = value;
                btnDelete.Enabled = value;
                //grdItemList.Enabled = value;
            } // set
        }

        #endregion

        #region 생성자

        /// <summary>
        /// <see cref="RecycleBinDialog"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        public RecycleBinDialog()
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
            this.Text = AsmblyResource.RecycleBinDialog_Text;
            columnFileName.HeaderText = AsmblyResource.Column_FileName;
            columnOriginalDir.HeaderText = AsmblyResource.Column_OriginalDirectory;
            columnDeletedDate.HeaderText = AsmblyResource.Column_DateDeleted;
            //
            // 검색 조건
            //
            lblPeriod.Text = AsmblyResource.Label_Period;
            dtpkrBegin.CalendarTodayText = AsmblyResource.Calendar_Today;
            dtpkrEnd.CalendarTodayText = AsmblyResource.Calendar_Today;
            btnSearch.Text = AsmblyResource.Button_Search;
            btnRestore.Text = AsmblyResource.Button_Restore;
            btnDelete.Text = AsmblyResource.Button_Delete;
            btnEmpty.Text = AsmblyResource.Button_Empty;
            btnClose.Text = AsmblyResource.Button_Close;
        }

        #endregion

        #region 이벤트 핸들러
        #region 백그라운드 작업
        #region 휴지통 항목 검색
        /// <summary>
        /// 백그라운드 작업이 시작될 때 다른 스레드에서 필요한 작업을 진행한다.
        /// </summary>
        private void bgwkSearch_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgWorker = sender as BackgroundWorker;
            BackgroundWorkerArgument bgwkArg = e.Argument as BackgroundWorkerArgument;
            EFAMService service = new EFAMService(AgentStore.Store.UserCredential);

            // 휴지통 항목의 목록을 가져온다.
            e.Result = service.GetRecycleBinItems();
        }

        /// <summary>
        /// 백그라운드 작업자가 완료(성공, 실패 또는 취소)되었을 때 필요한 작업을 진행한다.
        /// </summary>
        private void bgwkSearch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
                string logMessage = "RecycleBinDialog.bgwkSearch_DoWork() \n" + exception;

                if (m_logger.IsErrorEnabled) m_logger.Error(logMessage);
                if (m_tracing.Enabled) Trace.WriteLine("[EFAM.Error] " + logMessage);
            } // if (e.Error != null)
            //
            // 백그라운드 작업이 완료되었을 경우
            //
            else if (!e.Cancelled)
            {
                List<RecycleBinItem> itemList = e.Result as List<RecycleBinItem>;
                DataGridViewRow addedRow = null;

                foreach (RecycleBinItem item in itemList)
                {
                    addedRow = grdItemList.Rows[grdItemList.Rows.Add()];

                    addedRow.Cells[0].Value = item.Name;
                    addedRow.Cells[1].Value = item.DirectoryName;
                    addedRow.Cells[2].Value = item.DeletedDate.ToString("g");
                    addedRow.Tag = item;
                } // foreach ( RecycleBinItem )

                this.UseWaitCursor = false;
                this.EnableInputControls = true;

                btnRestore.Enabled = btnDelete.Enabled = btnEmpty.Enabled = (itemList.Count != 0);
            } // else if (!e.Cancelled)
        }

        #endregion

        #region 휴지통 항목 복원

        /// <summary>
        /// 백그라운드 작업이 시작될 때 다른 스레드에서 필요한 작업을 진행한다.
        /// </summary>
        private void bgwkRestore_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgWorker = sender as BackgroundWorker;
            DataGridViewSelectedRowCollection selectedRowColl = 
                e.Argument as DataGridViewSelectedRowCollection;
            RecycleBinItem item = null;
            string destFilePath = null;

            foreach (DataGridViewRow row in selectedRowColl)
            {
                item = row.Tag as RecycleBinItem;
                destFilePath = Path.Combine(item.DirectoryName, item.Name);

                if (File.Exists(destFilePath))
                {
                    /*
                    text = String.Format(AsmblyResource.Warn_FileAlreadyExists, item.FileName)
                         + "\n\n" + AsmblyResource.Quest_OverwriteFile;

                    if (KryptonMessageBox.Show(text, AsmblyResource.Text_MoveFile, MessageBoxButtons.YesNo)
                        == DialogResult.Yes)
                    {
                        File.Delete(destFilePath);
                    }
                    else continue;
                     */
                    File.Delete(destFilePath);
                } // if (File.Exists(destFilePath))

                //
                // 파일을 복원한다.
                //
                try
                {
                    item.Restore();
                }
                catch (ArgumentException) { }

                bgWorker.ReportProgress(0, row);
            } // foreach ( DataGridViewRow )
        }

        /// <summary>
        /// 백그라운드 작업자 스레드에서 일부 진행되었음을 나타낼 때 필요한 작업을 진행한다.
        /// </summary>
        private void bgwkRestore_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            grdItemList.Rows.Remove((DataGridViewRow)e.UserState);
        }

        #endregion

        #region 휴지통 항목 삭제

        /// <summary>
        /// 백그라운드 작업이 시작될 때 다른 스레드에서 필요한 작업을 진행한다.
        /// </summary>
        private void bgwkDelete_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgWorker = sender as BackgroundWorker;
            DataGridViewSelectedRowCollection selectedRowColl = 
                e.Argument as DataGridViewSelectedRowCollection;
            EFAMService service = new EFAMService(AgentStore.Store.UserCredential);
            RecycleBinItem item = null;

            foreach (DataGridViewRow row in selectedRowColl)
            {
                item = row.Tag as RecycleBinItem;

                //
                // 파일을 삭제한다.
                //
                try
                {
                    item.Delete();
                    service.WriteDeleteLogForRecycleBin(item.InternalFilePath);
                } catch (ArgumentException) { }

                bgWorker.ReportProgress(0, row);
            } // foreach ( DataGridViewRow )
        }

        /// <summary>
        /// 백그라운드 작업자 스레드에서 일부 진행되었음을 나타낼 때 필요한 작업을 진행한다.
        /// </summary>
        private void bgwkDelete_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            grdItemList.Rows.Remove((DataGridViewRow)e.UserState);
        }

        #endregion

        #region 휴지통 비우기

        /// <summary>
        /// 백그라운드 작업이 시작될 때 다른 스레드에서 필요한 작업을 진행한다.
        /// </summary>
        private void bgwkEmpty_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bgWorker = sender as BackgroundWorker;
            DataGridViewRowCollection rowColl = e.Argument as DataGridViewRowCollection;
            EFAMService service = new EFAMService(AgentStore.Store.UserCredential);
            RecycleBinItem item = null;

            // 휴지통에 있는 파일들을 삭제한다.
            //service.EmptyRecycleBinByUser();

            foreach (DataGridViewRow row in rowColl)
            {
                item = row.Tag as RecycleBinItem;

                // 파일을 삭제한다.
                try
                {
                    item.Delete();
                    service.WriteDeleteLogForRecycleBin(item.InternalFilePath);
                } catch (ArgumentException) { }

                bgWorker.ReportProgress(0, row);
            } // foreach ( DataGridViewRow )

        }

        /// <summary>
        /// 백그라운드 작업자 스레드에서 일부 진행되었음을 나타낼 때 필요한 작업을 진행한다.
        /// </summary>
        private void bgwkEmpty_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            grdItemList.Rows.Remove((DataGridViewRow)e.UserState);
        }

        #endregion

        /// <summary>
        /// 백그라운드 작업자가 완료(성공, 실패 또는 취소)되었을 때 필요한 작업을 진행한다.
        /// </summary>
        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
                string methodName = null;

                if (sender == bgwkRestore)
                {
                    methodName = "bgwkRestore_DoWork()";
                    message = String.Format(AsmblyResource.Error_ExceptionThrowedInProgress,
                                            AsmblyResource.RecycleBin_RestoringFile);
                }
                else
                {
                    methodName = (sender == bgwkDelete) ? "bgwkDelete_DoWork()" : "bgwkEmpty_DoWork()";
                    message = String.Format(AsmblyResource.Error_ExceptionThrowedInProgress,
                                            AsmblyResource.RecycleBin_DeletingFile);
                }

                KryptonMessageBox.Show(message, AsmblyResource.MsgBox_Caption,
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);

                //
                // 오류 로그를 기록하고 추적 메시지를 쓴다.
                //
                message = String.Format("RecycleBinDialog.{0} \n{1}", methodName, exception);
                if (m_logger.IsErrorEnabled) m_logger.Error(message);
                if (m_tracing.Enabled) Trace.WriteLine("[EFAM.Error] " + message);
            } // if (e.Error != null)
            //
            // 백그라운드 작업이 완료되었을 경우
            //
            else if (!e.Cancelled)
            {
                btnRestore.Enabled = btnDelete.Enabled = btnEmpty.Enabled 
                    = (grdItemList.Rows.Count != 0);
            } // else if (!e.Cancelled)
        }

        #endregion

        /// <summary>
        /// 폼을 로드할 때 필요한 작업을 진행한다.
        /// </summary>
        private void ViewLogDialog_Load(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
            this.EnableInputControls = false;

            // 백그라운드 작업의 실행을 시작한다.
            bgwkSearch.RunWorkerAsync();
        }

        /// <summary>
        /// 폼을 닫을 때마다 또는 폼이 닫히기 전에 필요한 작업을 진행한다.
        /// </summary>
        private void ViewLogDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = bgwkSearch.IsBusy;
            }
        }

        #region 버튼 이벤트

        /// <summary>
        /// "Search" 버튼을 클릭할 때 필요한 작업을 진행한다.
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            grdItemList.Rows.Clear();

            ///////////////////////////////////////////////////////////////////////////////////////
            // 백그라운드 작업으로 모든 휴지통 항목을 검색한다.
            //
            BackgroundWorkerArgument argument = new BackgroundWorkerArgument();

            this.UseWaitCursor = true;
            this.EnableInputControls = false;

            argument.BeginDateValue = dtpkrBegin.Checked ? dtpkrBegin.Value.ToString("yyyy-MM-dd") : "";
            argument.EndDateValue = dtpkrEnd.Checked ? dtpkrEnd.Value.ToString("yyyy-MM-dd") : "";

            // 백그라운드 작업의 실행을 시작한다.
            bgwkSearch.RunWorkerAsync(argument);
        }

        /// <summary>
        /// "Restore" 또는 "Delete" 버튼을 클릭할 때 필요한 작업을 진행한다.
        /// </summary>
        private void RestoreOrDeleteButton_Click(object sender, EventArgs e)
        {
            if (grdItemList.SelectedRows.Count == 0) return;

            ///////////////////////////////////////////////////////////////////////////////////////
            // 백그라운드 작업으로 휴지통 항목을 복원하거나 삭제한다.
            //
            this.UseWaitCursor = true;
            this.EnableInputControls = false;

            // 백그라운드 작업의 실행을 시작한다.
            if (sender == btnRestore) bgwkRestore.RunWorkerAsync(grdItemList.SelectedRows);
            else if (sender == btnDelete) bgwkDelete.RunWorkerAsync(grdItemList.SelectedRows);
        }

        /// <summary>
        /// "Empty" 버튼을 클릭할 때 필요한 작업을 진행한다.
        /// </summary>
        private void btnEmpty_Click(object sender, EventArgs e)
        {
            if (grdItemList.SelectedRows.Count == 0) return;

            ///////////////////////////////////////////////////////////////////////////////////////
            // 백그라운드 작업으로 휴지통 항목을 모두 삭제한다.
            //
            this.UseWaitCursor = true;
            this.EnableInputControls = false;

            // 백그라운드 작업의 실행을 시작한다.
            bgwkEmpty.RunWorkerAsync(grdItemList.Rows);
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
            public string BeginDateValue = null;
            public string EndDateValue = null;
        }

        #endregion
    }
}
