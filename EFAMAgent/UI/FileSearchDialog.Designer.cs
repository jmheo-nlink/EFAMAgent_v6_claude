namespace Link.EFAM.Agent.UI
{
    partial class FileSearchDialog
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
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.lblSearchKeyword = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtSearchKeyword = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.chkSearchFileName = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.chkSearchFileContent = new ComponentFactory.Krypton.Toolkit.KryptonCheckBox();
            this.btnSearch = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.lstSearchResults = new System.Windows.Forms.ListView();
            this.colFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFilePath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colFileSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colModifiedDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblStatus = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            //
            // kryptonPanel1
            //
            this.kryptonPanel1.Controls.Add(this.progressBar);
            this.kryptonPanel1.Controls.Add(this.lblStatus);
            this.kryptonPanel1.Controls.Add(this.lstSearchResults);
            this.kryptonPanel1.Controls.Add(this.btnCancel);
            this.kryptonPanel1.Controls.Add(this.btnSearch);
            this.kryptonPanel1.Controls.Add(this.chkSearchFileContent);
            this.kryptonPanel1.Controls.Add(this.chkSearchFileName);
            this.kryptonPanel1.Controls.Add(this.txtSearchKeyword);
            this.kryptonPanel1.Controls.Add(this.lblSearchKeyword);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(784, 561);
            this.kryptonPanel1.TabIndex = 0;
            //
            // lblSearchKeyword
            //
            this.lblSearchKeyword.Location = new System.Drawing.Point(12, 12);
            this.lblSearchKeyword.Name = "lblSearchKeyword";
            this.lblSearchKeyword.Size = new System.Drawing.Size(73, 20);
            this.lblSearchKeyword.TabIndex = 0;
            this.lblSearchKeyword.Values.Text = "검색어:";
            //
            // txtSearchKeyword
            //
            this.txtSearchKeyword.Location = new System.Drawing.Point(91, 12);
            this.txtSearchKeyword.Name = "txtSearchKeyword";
            this.txtSearchKeyword.Size = new System.Drawing.Size(400, 23);
            this.txtSearchKeyword.TabIndex = 1;
            //
            // chkSearchFileName
            //
            this.chkSearchFileName.Checked = true;
            this.chkSearchFileName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSearchFileName.Location = new System.Drawing.Point(91, 41);
            this.chkSearchFileName.Name = "chkSearchFileName";
            this.chkSearchFileName.Size = new System.Drawing.Size(110, 20);
            this.chkSearchFileName.TabIndex = 2;
            this.chkSearchFileName.Values.Text = "파일명 검색";
            //
            // chkSearchFileContent
            //
            this.chkSearchFileContent.Location = new System.Drawing.Point(207, 41);
            this.chkSearchFileContent.Name = "chkSearchFileContent";
            this.chkSearchFileContent.Size = new System.Drawing.Size(144, 20);
            this.chkSearchFileContent.TabIndex = 3;
            this.chkSearchFileContent.Values.Text = "파일 내용 검색 (텍스트)";
            //
            // btnSearch
            //
            this.btnSearch.Location = new System.Drawing.Point(497, 12);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(90, 25);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Values.Text = "검색";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            //
            // btnCancel
            //
            this.btnCancel.Location = new System.Drawing.Point(593, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 25);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Values.Text = "취소";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // lstSearchResults
            //
            this.lstSearchResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstSearchResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFileName,
            this.colFilePath,
            this.colFileSize,
            this.colModifiedDate});
            this.lstSearchResults.FullRowSelect = true;
            this.lstSearchResults.GridLines = true;
            this.lstSearchResults.Location = new System.Drawing.Point(12, 67);
            this.lstSearchResults.Name = "lstSearchResults";
            this.lstSearchResults.Size = new System.Drawing.Size(760, 433);
            this.lstSearchResults.TabIndex = 6;
            this.lstSearchResults.UseCompatibleStateImageBehavior = false;
            this.lstSearchResults.View = System.Windows.Forms.View.Details;
            this.lstSearchResults.DoubleClick += new System.EventHandler(this.lstSearchResults_DoubleClick);
            //
            // colFileName
            //
            this.colFileName.Text = "파일명";
            this.colFileName.Width = 200;
            //
            // colFilePath
            //
            this.colFilePath.Text = "경로";
            this.colFilePath.Width = 350;
            //
            // colFileSize
            //
            this.colFileSize.Text = "크기";
            this.colFileSize.Width = 100;
            //
            // colModifiedDate
            //
            this.colModifiedDate.Text = "수정일";
            this.colModifiedDate.Width = 150;
            //
            // lblStatus
            //
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.Location = new System.Drawing.Point(12, 506);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(88, 20);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Values.Text = "검색 대기 중";
            //
            // progressBar
            //
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(12, 532);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(760, 17);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 8;
            this.progressBar.Visible = false;
            //
            // FileSearchDialog
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.kryptonPanel1);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FileSearchDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "파일 검색";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FileSearchDialog_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblSearchKeyword;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtSearchKeyword;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkSearchFileName;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckBox chkSearchFileContent;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSearch;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCancel;
        private System.Windows.Forms.ListView lstSearchResults;
        private System.Windows.Forms.ColumnHeader colFileName;
        private System.Windows.Forms.ColumnHeader colFilePath;
        private System.Windows.Forms.ColumnHeader colFileSize;
        private System.Windows.Forms.ColumnHeader colModifiedDate;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblStatus;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}
