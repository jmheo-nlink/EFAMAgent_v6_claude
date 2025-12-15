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
 *                              - ViewPermissionsDialog_Load(object, EventArgs)
 *                              - bgWorker_RunWorkerCompleted(object, RunWorkerCompletedEventArgs)
 *                              
 * 2013-05-22   mskoo           S-Work 보안 드라이브로 설정된 경로는 다른 색으로 표시되도록 수정.
 *                              - bgWorker_ProgressChanged(object, ProgressChangedEventArgs)
 *                              
 * 2019         DJJUNG          권한 조회 UI 수정 (컬럼)
 *                              - ViewPermissionsDialog()
 *                              
 * 2019         DJJUNG          권한 목록 가져 오는 부분 수정 (권한 조회 시)
 *                              - bgWorker_DoWork(object, DoWorkEventArgs)
 *                              - bgWorker_ProgressChanged(object, ProgressChangedEventArgs)
 * ====================================================================================================================
 */
#endregion

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Link.EFAM.Common;
using Link.EFAM.Engine.Caching;
using Link.EFAM.Engine.Services;

using AsmblyResource = Link.EFAM.Agent.Properties.Resources;
using Link.EFAM.Security;
using Link.EFAM.Security.AccessControl;
using System.Collections.Specialized;
using Link.EFAM.Engine.InternalServices;

namespace Link.EFAM.Agent.UI
{
    public partial class ViewPermissionsDialog : KryptonForm
    {
        private static BooleanSwitch m_tracing = new BooleanSwitch("traceSwitch", "Agent Module");
        private ILog m_logger = LogManager.GetLogger(typeof(ViewPermissionsDialog));
        private static StringComparer m_strComparer = StringComparer.OrdinalIgnoreCase;

        #region 생성자

        /// <summary>
        /// <see cref="ViewPermissionsDialog"/> 클래스의 새 인스턴스를 초기화한다.
        /// </summary>
        public ViewPermissionsDialog()
        {
            InitializeComponent();
            SetLocalizedText();

            // Dialog
            ClientSize = new System.Drawing.Size(1200, 420);

            // 경로
            columnPath.Width = 400;

            // 읽기
            columnRead.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnRead.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            columnRead.Resizable = System.Windows.Forms.DataGridViewTriState.True;

            // 쓰기
            columnWrite.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnWrite.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            columnWrite.Resizable = System.Windows.Forms.DataGridViewTriState.True;

            // 삭제
            columnDelete.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnDelete.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            columnDelete.Resizable = System.Windows.Forms.DataGridViewTriState.True;

            // 이름 변경
            columnRename.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnRename.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            columnRename.Resizable = System.Windows.Forms.DataGridViewTriState.True;

            // 이동
            columnMove.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnMove.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            columnMove.Resizable = System.Windows.Forms.DataGridViewTriState.True;

            // 복사
            columnCopy.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnCopy.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            columnCopy.Resizable = System.Windows.Forms.DataGridViewTriState.True;

            // 목록
            columnListDirRights.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnListDirRights.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            columnListDirRights.Resizable = System.Windows.Forms.DataGridViewTriState.True;

            // 폴더 생성
            columnCreateDir.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnCreateDir.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            columnCreateDir.Resizable = System.Windows.Forms.DataGridViewTriState.True;

            // 상속 안함
            columnNoInherit.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnNoInherit.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            columnNoInherit.Resizable = System.Windows.Forms.DataGridViewTriState.True;

            // 전파 안함
            columnNoPropagate.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            columnNoPropagate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            columnNoPropagate.Resizable = System.Windows.Forms.DataGridViewTriState.True;

            // 관리자
            columnManager.Visible = false;
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
            this.Text = AsmblyResource.ViewPermissionsDialog_Text;
            columnPath.HeaderText = AsmblyResource.Column_DirPath;
            columnAccount.HeaderText = AsmblyResource.Column_Account;
            columnRead.HeaderText = AsmblyResource.Column_ReadRights;
            columnWrite.HeaderText = AsmblyResource.Column_WriteRights;
            columnDelete.HeaderText = AsmblyResource.Column_DeleteRights;
            columnRename.HeaderText = AsmblyResource.Column_RenameRights;
            columnMove.HeaderText = AsmblyResource.Column_MoveRights;
            columnCopy.HeaderText = AsmblyResource.Column_CopyRights;
            columnListDirRights.HeaderText = AsmblyResource.Column_ListDirRights;
            columnCreateDir.HeaderText = AsmblyResource.Column_CreateDirRights;
            columnNoInherit.HeaderText = AsmblyResource.Column_NoInherit;
            columnNoPropagate.HeaderText = AsmblyResource.Column_NoPropagate;
            columnManager.HeaderText = AsmblyResource.Column_Manager;
            btnOK.Text = AsmblyResource.Button_OK;
        }

        #endregion

        #region 이벤트 핸들러
        #region 백그라운드 작업

        /// <summary>
        /// 백그라운드 작업이 시작될 때 다른 스레드에서 필요한 작업을 진행한다.
        /// </summary>
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            EFAMService service = new EFAMService(AgentStore.Store.UserCredential);
            EFAMPermission[] permissions = null;
            UserInfo[] managers = null;

            // 설정된 액세스 규칙의 컬렉션을 가져온다.
            permissions = service.GetAccessRules();
            // 각 경로별 관리자 목록을 가져온다.
            foreach (EFAMPermission permission in permissions)
            {
                if (!permission.Seq.Contains("-List"))
                {
                    managers = service.GetManagerList(permission.DrivePath);
                    bgWorker.ReportProgress(0, new ProgressState(permission, managers));
                }
            }

            /*
            AccessControlPolicy policy = service.GetSecurityPolicy();
            AccessManager.GetManager().ApplyPolicy(policy, AgentStore.Store.HostNameCollection);
            // 액세스 권한 캐시를 비운다.
            PermissionCache.GetPermissionCache().Clear();
             */

            AgentStore.Store.PolicyReloader.Stop();
            PolicyAutoReloader.ReloadPolicy();
            AgentStore.Store.PolicyReloader.Start();
        }

        /// <summary>
        /// 백그라운드 작업자 스레드에서 일부 진행되었음을 나타낼 때 필요한 작업을 진행한다.
        /// </summary>
        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressState state = e.UserState as ProgressState;
            //FileAccessRights fileRights = null;
            DataGridViewRow addedRow = null;
            EFAMPermission permission = state.Permission;
            UserInfo manager = (state.ManagerList.Length != 0) ? state.ManagerList[0] : null;

            // 권한 정보와 관리자 정보를 표시한다.
            addedRow = grdPermsList.Rows[grdPermsList.Rows.Add()];
            //fileRights = permission.AccessRights;
            addedRow.Cells[0].Value = permission.DrivePath;
            //addedRow.Cells[1].Value = m_strComparer.Equals(permission.UserType, "AU")
            //                        ? permission.UserID + " (" + permission.UserName + ")"
            //                        : permission.UserID + " (" + permission.GroupName + ")";
            addedRow.Cells[1].Value = permission.UserID;
            addedRow.Cells[2].Value = permission.CanRead ? "V" : "";
            addedRow.Cells[3].Value = permission.CanWrite ? "V" : "";
            addedRow.Cells[4].Value = permission.CanDelete ? "V" : "";
            addedRow.Cells[5].Value = permission.CanRename ? "V" : "";
            addedRow.Cells[6].Value = permission.CanMove ? "V" : "";
            addedRow.Cells[7].Value = permission.CanCopy ? "V" : "";
            addedRow.Cells[8].Value = permission.CanList ? "V" : "";
            addedRow.Cells[9].Value = permission.CanCreateFolder ? "V" : "";
            addedRow.Cells[10].Value = permission.CanNotInherit ? "V" : "";
            addedRow.Cells[11].Value = permission.CanNotPropagate ? "V" : "";
            if (manager != null)
            {
                addedRow.Cells[12].Value = manager.UserName + " (" + manager.Department + ")";
            }
            addedRow.Cells[12].Tag = state.ManagerList;

#if (INTEROP_SWORK || INTEROP_SWORK_HHISB)  // ====================================================
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            foreach (Core.SharedDrive drive in AgentStore.Store.SharedDrives)
            {
                if (drive.IsSecure &&
                    comparer.Equals(permission.DrivePath, drive.ShareName))
                {
                    addedRow.Cells[0].Style.ForeColor = Color.Red;
                    break;
                }
            } // foreach ( SharedDrive )
#if INTEROP_SWORK_HHISB // ========================================================================
            foreach (Core.SharedDrive drive in AgentStore.Store.DeniedSharedDrives)
            {
                if (comparer.Equals(permission.DrivePath, drive.ShareName))
                {
                    addedRow.Cells[0].Style.ForeColor = Color.Red;
                    break;
                }
            } // foreach ( SharedDrive )
#endif  // INTEROP_SWORK_HHISB ====================================================================
#endif  // INTEROP_SWORK || INTEROP_SWORK_HHISB ===================================================
        }

        /// <summary>
        /// 백그라운드 작업자가 완료(성공, 실패 또는 취소)되었을 때 필요한 작업을 진행한다.
        /// </summary>
        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.UseWaitCursor = false;
            //grdPermsList.Enabled = true;

            //
            // 오류가 발생했을 경우
            //
            if (e.Error != null)
            {
                Exception exception = e.Error;
                string message = null;

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

                // 오류 로그를 기록하고 추적 메시지를 쓴다.
                string logMessage = "ViewPermissionsDialog.bgWorker_DoWork() \n" + exception;

                if (m_logger.IsErrorEnabled) m_logger.Error(logMessage);
                if (m_tracing.Enabled) Trace.WriteLine("[EFAM.Error] " + logMessage);
            } // if (e.Error != null)
            //
            // 백그라운드 작업이 완료되었을 경우
            //
            else if (!e.Cancelled)
            {
                /*
                EFAMPermission[] permList = e.Result as EFAMPermission[];
                //FileAccessRights fileRights = null;
                DataGridViewRow addedRow = null;

                foreach (EFAMPermission permission in permList)
                {
                    addedRow = grdPermsList.Rows[grdPermsList.Rows.Add()];
                    //fileRights = permission.AccessRights;

                    addedRow.Cells[0].Value = permission.DrivePath;
                    if (String.Equals(permission.UserType, "AU", StringComparison.OrdinalIgnoreCase))
                    {
                        addedRow.Cells[1].Value = permission.UserID + " (" + permission.UserName + ")";
                    }
                    else
                    {
                        addedRow.Cells[1].Value = permission.UserID + " (" + permission.GroupName + ")";
                    }
                    //
                    // "Wingdings 2" 폰트를 사용하여 특수 문자(체크 표시)를 표시한다.
                    //
                    addedRow.Cells[2].Value = permission.CanRead ? "V" : "";
                    addedRow.Cells[3].Value = permission.CanWrite ? "V" : "";
                    addedRow.Cells[4].Value = permission.CanDelete ? "V" : "";
                    addedRow.Cells[5].Value = permission.CanRename ? "V" : "";
                    addedRow.Cells[6].Value = permission.CanMove ? "V" : "";
                    addedRow.Cells[7].Value = permission.CanCopy ? "V" : "";
                    addedRow.Cells[8].Value = permission.CanList ? "V" : "";
                    addedRow.Cells[9].Value = permission.CanCreateFolder ? "V" : "";
                    addedRow.Cells[10].Value = permission.CanNotInherit ? "V" : "";
                    addedRow.Cells[11].Value = permission.CanNotPropagate ? "V" : "";
                } // foreach ( FileAccessRule )
                 */
            } // else if (!e.Cancelled)
        }

        #endregion

        /// <summary>
        /// 폼을 로드할 때 필요한 작업을 진행한다.
        /// </summary>
        private void ViewPermissionsDialog_Load(object sender, EventArgs e)
        {
            this.UseWaitCursor = true;
            //grdPermsList.Enabled = false;

            // 백그라운드 작업의 실행을 시작한다.
            bgWorker.RunWorkerAsync();
        }

        /// <summary>
        /// 폼을 닫을 때마다 또는 폼이 닫히기 전에 필요한 작업을 진행한다.
        /// </summary>
        private void ViewPermissionsDialog_FormClosing(object sender, FormClosingEventArgs e)
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
            this.Close();
        }

        #endregion

        private void grdPermsList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 12)
            {
                ManagerInfoDialog dialog = new ManagerInfoDialog();

                dialog.ManagerList 
                    = grdPermsList.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag as UserInfo[];
                dialog.ShowDialog(this);
            }
        }

        #endregion

        #region INNER 클래스

        /// <summary>
        /// 백그라운드 작업의 진행 상태를 나타낸다.
        /// </summary>
        private class ProgressState
        {
            public EFAMPermission Permission;
            public UserInfo[] ManagerList;

            /// <summary>
            /// <see cref="ProgressState"/> 클래스의 새 인스턴스를 초기화한다.
            /// </summary>
            /// <param name="permission">E-FAM 권한을 나타내는 <see cref="EFAMPermission"/> 개체</param>
            /// <param name="managers">관리자 정보를 나타내는 <see cref="UserInfo"/> 개체의 배열</param>
            public ProgressState(EFAMPermission permission, UserInfo[] managers)
            {
                this.Permission = permission;
                this.ManagerList = managers;
            }
        }

        #endregion
    }
}
