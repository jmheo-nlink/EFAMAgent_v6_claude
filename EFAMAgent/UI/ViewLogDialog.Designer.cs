namespace Link.EFAM.Agent.UI
{
    partial class ViewLogDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.pnlContainerBack = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.tlpnlContainer = new System.Windows.Forms.TableLayoutPanel();
            this.grpSearch = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.flpnlButton = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSearch = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.dtpkrEnd = new ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker();
            this.lblEndDate = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.dtpkrBegin = new ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker();
            this.cboAction = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.txtFileName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblBeginDate = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblAction = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblFileName = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.grpLogListBack = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.grdLogList = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.columnPath = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.columnNewPath = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.columnAction = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.columnDate = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.btnClose = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlContainerBack)).BeginInit();
            this.pnlContainerBack.SuspendLayout();
            this.tlpnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpSearch)).BeginInit();
            this.grpSearch.Panel.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.flpnlButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboAction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpLogListBack)).BeginInit();
            this.grpLogListBack.Panel.SuspendLayout();
            this.grpLogListBack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLogList)).BeginInit();
            this.SuspendLayout();
            // 
            // bgWorker
            // 
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // pnlContainerBack
            // 
            this.pnlContainerBack.Controls.Add(this.tlpnlContainer);
            this.pnlContainerBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainerBack.Location = new System.Drawing.Point(0, 0);
            this.pnlContainerBack.Name = "pnlContainerBack";
            this.pnlContainerBack.Size = new System.Drawing.Size(974, 526);
            this.pnlContainerBack.TabIndex = 1;
            // 
            // tlpnlContainer
            // 
            this.tlpnlContainer.BackColor = System.Drawing.Color.Transparent;
            this.tlpnlContainer.ColumnCount = 1;
            this.tlpnlContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpnlContainer.Controls.Add(this.grpSearch, 0, 0);
            this.tlpnlContainer.Controls.Add(this.grpLogListBack, 0, 1);
            this.tlpnlContainer.Controls.Add(this.btnClose, 0, 2);
            this.tlpnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpnlContainer.Location = new System.Drawing.Point(0, 0);
            this.tlpnlContainer.Name = "tlpnlContainer";
            this.tlpnlContainer.RowCount = 3;
            this.tlpnlContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpnlContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpnlContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tlpnlContainer.Size = new System.Drawing.Size(974, 526);
            this.tlpnlContainer.TabIndex = 0;
            // 
            // grpSearch
            // 
            this.grpSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSearch.Location = new System.Drawing.Point(3, 3);
            this.grpSearch.Name = "grpSearch";
            // 
            // grpSearch.Panel
            // 
            this.grpSearch.Panel.Controls.Add(this.flpnlButton);
            this.grpSearch.Panel.Controls.Add(this.dtpkrEnd);
            this.grpSearch.Panel.Controls.Add(this.lblEndDate);
            this.grpSearch.Panel.Controls.Add(this.dtpkrBegin);
            this.grpSearch.Panel.Controls.Add(this.cboAction);
            this.grpSearch.Panel.Controls.Add(this.txtFileName);
            this.grpSearch.Panel.Controls.Add(this.lblBeginDate);
            this.grpSearch.Panel.Controls.Add(this.lblAction);
            this.grpSearch.Panel.Controls.Add(this.lblFileName);
            this.grpSearch.Size = new System.Drawing.Size(968, 44);
            this.grpSearch.TabIndex = 0;
            // 
            // flpnlButton
            // 
            this.flpnlButton.BackColor = System.Drawing.Color.Transparent;
            this.flpnlButton.Controls.Add(this.btnSearch);
            this.flpnlButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.flpnlButton.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpnlButton.Location = new System.Drawing.Point(850, 0);
            this.flpnlButton.Name = "flpnlButton";
            this.flpnlButton.Size = new System.Drawing.Size(116, 42);
            this.flpnlButton.TabIndex = 8;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(16, 8);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 8, 10, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(90, 25);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Values.Text = "Button_Search";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dtpkrEnd
            // 
            this.dtpkrEnd.AlwaysActive = false;
            this.dtpkrEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpkrEnd.InputControlStyle = ComponentFactory.Krypton.Toolkit.InputControlStyle.Ribbon;
            this.dtpkrEnd.Location = new System.Drawing.Point(632, 10);
            this.dtpkrEnd.Name = "dtpkrEnd";
            this.dtpkrEnd.ShowCheckBox = true;
            this.dtpkrEnd.Size = new System.Drawing.Size(112, 21);
            this.dtpkrEnd.TabIndex = 7;
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = false;
            this.lblEndDate.Location = new System.Drawing.Point(560, 10);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(72, 20);
            this.lblEndDate.StateCommon.LongText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.lblEndDate.StateCommon.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.lblEndDate.TabIndex = 6;
            this.lblEndDate.Values.Text = "Label_EndDate";
            // 
            // dtpkrBegin
            // 
            this.dtpkrBegin.AlwaysActive = false;
            this.dtpkrBegin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpkrBegin.InputControlStyle = ComponentFactory.Krypton.Toolkit.InputControlStyle.Ribbon;
            this.dtpkrBegin.Location = new System.Drawing.Point(448, 10);
            this.dtpkrBegin.Name = "dtpkrBegin";
            this.dtpkrBegin.ShowCheckBox = true;
            this.dtpkrBegin.Size = new System.Drawing.Size(112, 21);
            this.dtpkrBegin.TabIndex = 5;
            // 
            // cboAction
            // 
            this.cboAction.AlwaysActive = false;
            this.cboAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAction.DropDownWidth = 96;
            this.cboAction.InputControlStyle = ComponentFactory.Krypton.Toolkit.InputControlStyle.Ribbon;
            this.cboAction.Items.AddRange(new object[] {
            ""});
            this.cboAction.Location = new System.Drawing.Point(288, 10);
            this.cboAction.Name = "cboAction";
            this.cboAction.Size = new System.Drawing.Size(88, 21);
            this.cboAction.TabIndex = 3;
            // 
            // txtFileName
            // 
            this.txtFileName.AlwaysActive = false;
            this.txtFileName.InputControlStyle = ComponentFactory.Krypton.Toolkit.InputControlStyle.Ribbon;
            this.txtFileName.Location = new System.Drawing.Point(80, 10);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(136, 20);
            this.txtFileName.TabIndex = 1;
            // 
            // lblBeginDate
            // 
            this.lblBeginDate.AutoSize = false;
            this.lblBeginDate.Location = new System.Drawing.Point(376, 10);
            this.lblBeginDate.Name = "lblBeginDate";
            this.lblBeginDate.Size = new System.Drawing.Size(72, 20);
            this.lblBeginDate.StateCommon.LongText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.lblBeginDate.StateCommon.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.lblBeginDate.TabIndex = 4;
            this.lblBeginDate.Values.Text = "Label_BeginDate";
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = false;
            this.lblAction.Location = new System.Drawing.Point(216, 10);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(72, 20);
            this.lblAction.StateCommon.LongText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.lblAction.StateCommon.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.lblAction.TabIndex = 2;
            this.lblAction.Values.Text = "Label_FileAction";
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = false;
            this.lblFileName.Location = new System.Drawing.Point(8, 10);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(72, 20);
            this.lblFileName.StateCommon.LongText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.lblFileName.StateCommon.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.lblFileName.TabIndex = 0;
            this.lblFileName.Values.Text = "Label_FileName";
            // 
            // grpLogListBack
            // 
            this.grpLogListBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpLogListBack.Location = new System.Drawing.Point(3, 53);
            this.grpLogListBack.Name = "grpLogListBack";
            // 
            // grpLogListBack.Panel
            // 
            this.grpLogListBack.Panel.Controls.Add(this.grdLogList);
            this.grpLogListBack.Size = new System.Drawing.Size(968, 431);
            this.grpLogListBack.TabIndex = 4;
            // 
            // grdLogList
            // 
            this.grdLogList.AllowUserToAddRows = false;
            this.grdLogList.AllowUserToDeleteRows = false;
            this.grdLogList.AllowUserToOrderColumns = true;
            this.grdLogList.AllowUserToResizeRows = false;
            this.grdLogList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnPath,
            this.columnNewPath,
            this.columnAction,
            this.columnDate});
            this.grdLogList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLogList.HideOuterBorders = true;
            this.grdLogList.Location = new System.Drawing.Point(0, 0);
            this.grdLogList.Name = "grdLogList";
            this.grdLogList.ReadOnly = true;
            this.grdLogList.RowHeadersVisible = false;
            this.grdLogList.RowTemplate.Height = 23;
            this.grdLogList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdLogList.Size = new System.Drawing.Size(966, 429);
            this.grdLogList.TabIndex = 0;
            // 
            // columnPath
            // 
            this.columnPath.HeaderText = "Column_FilePath";
            this.columnPath.Name = "columnPath";
            this.columnPath.ReadOnly = true;
            this.columnPath.Width = 380;
            // 
            // columnNewPath
            // 
            this.columnNewPath.HeaderText = "Column_NewFilePath";
            this.columnNewPath.Name = "columnNewPath";
            this.columnNewPath.ReadOnly = true;
            this.columnNewPath.Width = 360;
            // 
            // columnAction
            // 
            this.columnAction.HeaderText = "Column_FileAction";
            this.columnAction.Name = "columnAction";
            this.columnAction.ReadOnly = true;
            this.columnAction.Width = 80;
            // 
            // columnDate
            // 
            this.columnDate.HeaderText = "Column_Date";
            this.columnDate.Name = "columnDate";
            this.columnDate.ReadOnly = true;
            this.columnDate.Width = 120;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(878, 495);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 8, 6, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 25);
            this.btnClose.TabIndex = 1;
            this.btnClose.Values.Text = "Button_Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ViewLogDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(974, 526);
            this.Controls.Add(this.pnlContainerBack);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(890, 160);
            this.Name = "ViewLogDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewLogDialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ViewLogDialog_FormClosing);
            this.Load += new System.EventHandler(this.ViewLogDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlContainerBack)).EndInit();
            this.pnlContainerBack.ResumeLayout(false);
            this.tlpnlContainer.ResumeLayout(false);
            this.grpSearch.Panel.ResumeLayout(false);
            this.grpSearch.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpSearch)).EndInit();
            this.grpSearch.ResumeLayout(false);
            this.flpnlButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboAction)).EndInit();
            this.grpLogListBack.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpLogListBack)).EndInit();
            this.grpLogListBack.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdLogList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bgWorker;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel pnlContainerBack;
        private System.Windows.Forms.TableLayoutPanel tlpnlContainer;
        private System.Windows.Forms.FlowLayoutPanel flpnlButton;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnClose;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSearch;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup grpSearch;
        private ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker dtpkrEnd;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblEndDate;
        private ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker dtpkrBegin;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cboAction;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtFileName;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblBeginDate;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblAction;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblFileName;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup grpLogListBack;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView grdLogList;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn columnPath;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn columnNewPath;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn columnAction;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn columnDate;
    }
}