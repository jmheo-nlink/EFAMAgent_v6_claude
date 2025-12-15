namespace Link.EFAM.Agent.UI
{
    partial class ManagerInfoDialog
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpnlContainer = new System.Windows.Forms.TableLayoutPanel();
            this.grpButtonBack = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.flpnlButton = new System.Windows.Forms.FlowLayoutPanel();
            this.btnOK = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.pnlManagerInfoBack = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.grpContent = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.tlpnlManagerInfo = new System.Windows.Forms.TableLayoutPanel();
            this.txtEmail = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtPhoneNumber = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblEmail = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblPhoneNumber = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtDepartment = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtJobTitle = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblName = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblDepartment = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblJobTitle = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.pnlManagerListBack = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.lstManagerList = new ComponentFactory.Krypton.Toolkit.KryptonListBox();
            this.tlpnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpButtonBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpButtonBack.Panel)).BeginInit();
            this.grpButtonBack.Panel.SuspendLayout();
            this.grpButtonBack.SuspendLayout();
            this.flpnlButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlManagerInfoBack)).BeginInit();
            this.pnlManagerInfoBack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpContent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grpContent.Panel)).BeginInit();
            this.grpContent.Panel.SuspendLayout();
            this.grpContent.SuspendLayout();
            this.tlpnlManagerInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlManagerListBack)).BeginInit();
            this.pnlManagerListBack.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpnlContainer
            // 
            this.tlpnlContainer.ColumnCount = 2;
            this.tlpnlContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.tlpnlContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpnlContainer.Controls.Add(this.grpButtonBack, 0, 1);
            this.tlpnlContainer.Controls.Add(this.pnlManagerInfoBack, 1, 0);
            this.tlpnlContainer.Controls.Add(this.pnlManagerListBack, 0, 0);
            this.tlpnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpnlContainer.Location = new System.Drawing.Point(0, 0);
            this.tlpnlContainer.Name = "tlpnlContainer";
            this.tlpnlContainer.RowCount = 2;
            this.tlpnlContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpnlContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tlpnlContainer.Size = new System.Drawing.Size(464, 230);
            this.tlpnlContainer.TabIndex = 1;
            // 
            // grpButtonBack
            // 
            this.tlpnlContainer.SetColumnSpan(this.grpButtonBack, 2);
            this.grpButtonBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpButtonBack.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.grpButtonBack.Location = new System.Drawing.Point(0, 184);
            this.grpButtonBack.Margin = new System.Windows.Forms.Padding(0);
            this.grpButtonBack.Name = "grpButtonBack";
            // 
            // grpButtonBack.Panel
            // 
            this.grpButtonBack.Panel.Controls.Add(this.flpnlButton);
            this.grpButtonBack.Size = new System.Drawing.Size(464, 46);
            this.grpButtonBack.StateCommon.Border.DrawBorders = ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top;
            this.grpButtonBack.TabIndex = 1;
            // 
            // flpnlButton
            // 
            this.flpnlButton.BackColor = System.Drawing.Color.Transparent;
            this.flpnlButton.Controls.Add(this.btnOK);
            this.flpnlButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.flpnlButton.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flpnlButton.Location = new System.Drawing.Point(245, 0);
            this.flpnlButton.Name = "flpnlButton";
            this.flpnlButton.Size = new System.Drawing.Size(219, 45);
            this.flpnlButton.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(122, 10);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 10, 7, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 25);
            this.btnOK.TabIndex = 0;
            this.btnOK.Values.Text = "Button_OK";
            // 
            // pnlManagerInfoBack
            // 
            this.pnlManagerInfoBack.Controls.Add(this.grpContent);
            this.pnlManagerInfoBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlManagerInfoBack.Location = new System.Drawing.Point(112, 0);
            this.pnlManagerInfoBack.Margin = new System.Windows.Forms.Padding(0);
            this.pnlManagerInfoBack.Name = "pnlManagerInfoBack";
            this.pnlManagerInfoBack.Padding = new System.Windows.Forms.Padding(4, 8, 8, 8);
            this.pnlManagerInfoBack.Size = new System.Drawing.Size(352, 184);
            this.pnlManagerInfoBack.TabIndex = 0;
            // 
            // grpContent
            // 
            this.grpContent.CaptionOverlap = 0.7D;
            this.grpContent.CaptionStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.grpContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpContent.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ControlClient;
            this.grpContent.Location = new System.Drawing.Point(4, 8);
            this.grpContent.Name = "grpContent";
            // 
            // grpContent.Panel
            // 
            this.grpContent.Panel.Controls.Add(this.tlpnlManagerInfo);
            this.grpContent.Size = new System.Drawing.Size(340, 168);
            this.grpContent.TabIndex = 0;
            this.grpContent.Text = "Caption";
            // 
            // tlpnlManagerInfo
            // 
            this.tlpnlManagerInfo.BackColor = System.Drawing.Color.Transparent;
            this.tlpnlManagerInfo.ColumnCount = 2;
            this.tlpnlManagerInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.tlpnlManagerInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpnlManagerInfo.Controls.Add(this.txtEmail, 1, 5);
            this.tlpnlManagerInfo.Controls.Add(this.txtPhoneNumber, 1, 4);
            this.tlpnlManagerInfo.Controls.Add(this.lblEmail, 0, 5);
            this.tlpnlManagerInfo.Controls.Add(this.lblPhoneNumber, 0, 4);
            this.tlpnlManagerInfo.Controls.Add(this.txtName, 1, 1);
            this.tlpnlManagerInfo.Controls.Add(this.txtDepartment, 1, 3);
            this.tlpnlManagerInfo.Controls.Add(this.txtJobTitle, 1, 2);
            this.tlpnlManagerInfo.Controls.Add(this.lblName, 0, 1);
            this.tlpnlManagerInfo.Controls.Add(this.lblDepartment, 0, 3);
            this.tlpnlManagerInfo.Controls.Add(this.lblJobTitle, 0, 2);
            this.tlpnlManagerInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpnlManagerInfo.Location = new System.Drawing.Point(0, 0);
            this.tlpnlManagerInfo.Name = "tlpnlManagerInfo";
            this.tlpnlManagerInfo.RowCount = 6;
            this.tlpnlManagerInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 4F));
            this.tlpnlManagerInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpnlManagerInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpnlManagerInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpnlManagerInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tlpnlManagerInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpnlManagerInfo.Size = new System.Drawing.Size(336, 146);
            this.tlpnlManagerInfo.TabIndex = 0;
            // 
            // txtEmail
            // 
            this.txtEmail.AlwaysActive = false;
            this.txtEmail.InputControlStyle = ComponentFactory.Krypton.Toolkit.InputControlStyle.Ribbon;
            this.txtEmail.Location = new System.Drawing.Point(112, 116);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.ReadOnly = true;
            this.txtEmail.Size = new System.Drawing.Size(200, 20);
            this.txtEmail.TabIndex = 11;
            this.txtEmail.TabStop = false;
            // 
            // txtPhoneNumber
            // 
            this.txtPhoneNumber.AlwaysActive = false;
            this.txtPhoneNumber.InputControlStyle = ComponentFactory.Krypton.Toolkit.InputControlStyle.Ribbon;
            this.txtPhoneNumber.Location = new System.Drawing.Point(112, 88);
            this.txtPhoneNumber.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.txtPhoneNumber.Name = "txtPhoneNumber";
            this.txtPhoneNumber.ReadOnly = true;
            this.txtPhoneNumber.Size = new System.Drawing.Size(144, 20);
            this.txtPhoneNumber.TabIndex = 9;
            this.txtPhoneNumber.TabStop = false;
            // 
            // lblEmail
            // 
            this.lblEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEmail.AutoSize = false;
            this.lblEmail.Location = new System.Drawing.Point(5, 116);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(104, 20);
            this.lblEmail.StateCommon.LongText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.lblEmail.StateCommon.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.lblEmail.TabIndex = 10;
            this.lblEmail.Values.Text = "Label_Email";
            // 
            // lblPhoneNumber
            // 
            this.lblPhoneNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPhoneNumber.AutoSize = false;
            this.lblPhoneNumber.Location = new System.Drawing.Point(5, 88);
            this.lblPhoneNumber.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblPhoneNumber.Name = "lblPhoneNumber";
            this.lblPhoneNumber.Size = new System.Drawing.Size(104, 20);
            this.lblPhoneNumber.StateCommon.LongText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.lblPhoneNumber.StateCommon.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.lblPhoneNumber.TabIndex = 8;
            this.lblPhoneNumber.Values.Text = "Label_PhoneNumber";
            // 
            // txtName
            // 
            this.txtName.AlwaysActive = false;
            this.txtName.InputControlStyle = ComponentFactory.Krypton.Toolkit.InputControlStyle.Ribbon;
            this.txtName.Location = new System.Drawing.Point(112, 4);
            this.txtName.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(144, 20);
            this.txtName.TabIndex = 3;
            this.txtName.TabStop = false;
            // 
            // txtDepartment
            // 
            this.txtDepartment.AlwaysActive = false;
            this.txtDepartment.InputControlStyle = ComponentFactory.Krypton.Toolkit.InputControlStyle.Ribbon;
            this.txtDepartment.Location = new System.Drawing.Point(112, 60);
            this.txtDepartment.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.ReadOnly = true;
            this.txtDepartment.Size = new System.Drawing.Size(144, 20);
            this.txtDepartment.TabIndex = 7;
            this.txtDepartment.TabStop = false;
            // 
            // txtJobTitle
            // 
            this.txtJobTitle.AlwaysActive = false;
            this.txtJobTitle.InputControlStyle = ComponentFactory.Krypton.Toolkit.InputControlStyle.Ribbon;
            this.txtJobTitle.Location = new System.Drawing.Point(112, 32);
            this.txtJobTitle.Margin = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.txtJobTitle.Name = "txtJobTitle";
            this.txtJobTitle.ReadOnly = true;
            this.txtJobTitle.Size = new System.Drawing.Size(144, 20);
            this.txtJobTitle.TabIndex = 5;
            this.txtJobTitle.TabStop = false;
            // 
            // lblName
            // 
            this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName.AutoSize = false;
            this.lblName.Location = new System.Drawing.Point(5, 4);
            this.lblName.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(104, 20);
            this.lblName.StateCommon.LongText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.lblName.StateCommon.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.lblName.TabIndex = 2;
            this.lblName.Values.Text = "Label_Name";
            // 
            // lblDepartment
            // 
            this.lblDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDepartment.AutoSize = false;
            this.lblDepartment.Location = new System.Drawing.Point(5, 60);
            this.lblDepartment.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(104, 20);
            this.lblDepartment.StateCommon.LongText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.lblDepartment.StateCommon.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.lblDepartment.TabIndex = 6;
            this.lblDepartment.Values.Text = "Label_Department";
            // 
            // lblJobTitle
            // 
            this.lblJobTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblJobTitle.AutoSize = false;
            this.lblJobTitle.Location = new System.Drawing.Point(5, 32);
            this.lblJobTitle.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.lblJobTitle.Name = "lblJobTitle";
            this.lblJobTitle.Size = new System.Drawing.Size(104, 20);
            this.lblJobTitle.StateCommon.LongText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.lblJobTitle.StateCommon.ShortText.TextH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.lblJobTitle.TabIndex = 4;
            this.lblJobTitle.Values.Text = "Label_JobTitle";
            // 
            // pnlManagerListBack
            // 
            this.pnlManagerListBack.Controls.Add(this.lstManagerList);
            this.pnlManagerListBack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlManagerListBack.Location = new System.Drawing.Point(0, 0);
            this.pnlManagerListBack.Margin = new System.Windows.Forms.Padding(0);
            this.pnlManagerListBack.Name = "pnlManagerListBack";
            this.pnlManagerListBack.Padding = new System.Windows.Forms.Padding(8, 8, 4, 8);
            this.pnlManagerListBack.Size = new System.Drawing.Size(112, 184);
            this.pnlManagerListBack.TabIndex = 2;
            // 
            // lstManagerList
            // 
            this.lstManagerList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstManagerList.Location = new System.Drawing.Point(8, 8);
            this.lstManagerList.Name = "lstManagerList";
            this.lstManagerList.Size = new System.Drawing.Size(100, 168);
            this.lstManagerList.TabIndex = 0;
            this.lstManagerList.SelectedIndexChanged += new System.EventHandler(this.lstManagerList_SelectedIndexChanged);
            // 
            // ManagerInfoDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 230);
            this.Controls.Add(this.tlpnlContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ManagerInfoDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ManagerInfoDialog";
            this.Load += new System.EventHandler(this.ManagerInfoDialog_Load);
            this.tlpnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpButtonBack.Panel)).EndInit();
            this.grpButtonBack.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpButtonBack)).EndInit();
            this.grpButtonBack.ResumeLayout(false);
            this.flpnlButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlManagerInfoBack)).EndInit();
            this.pnlManagerInfoBack.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpContent.Panel)).EndInit();
            this.grpContent.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpContent)).EndInit();
            this.grpContent.ResumeLayout(false);
            this.tlpnlManagerInfo.ResumeLayout(false);
            this.tlpnlManagerInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlManagerListBack)).EndInit();
            this.pnlManagerListBack.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpnlContainer;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup grpButtonBack;
        private System.Windows.Forms.FlowLayoutPanel flpnlButton;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnOK;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel pnlManagerInfoBack;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox grpContent;
        private System.Windows.Forms.TableLayoutPanel tlpnlManagerInfo;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtName;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtDepartment;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtJobTitle;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblName;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblDepartment;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblJobTitle;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel pnlManagerListBack;
        private ComponentFactory.Krypton.Toolkit.KryptonListBox lstManagerList;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblEmail;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblPhoneNumber;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtEmail;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtPhoneNumber;
    }
}

