namespace Link.EFAM.Agent.UI
{
    partial class LoaderDialog
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
            this.bgwkLoader = new System.ComponentModel.BackgroundWorker();
            this.bgwkUnloader = new System.ComponentModel.BackgroundWorker();
            this.pnlContainer = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.grpContent = new ComponentFactory.Krypton.Toolkit.KryptonGroup();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblMessage = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pnlContainer)).BeginInit();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpContent)).BeginInit();
            this.grpContent.Panel.SuspendLayout();
            this.grpContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // bgwkLoader
            // 
            this.bgwkLoader.WorkerReportsProgress = true;
            this.bgwkLoader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwkLoader_DoWork);
            this.bgwkLoader.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            this.bgwkLoader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // bgwkUnloader
            // 
            this.bgwkUnloader.WorkerReportsProgress = true;
            this.bgwkUnloader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwkUnloader_DoWork);
            this.bgwkUnloader.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            this.bgwkUnloader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // pnlContainer
            // 
            this.pnlContainer.Controls.Add(this.grpContent);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Padding = new System.Windows.Forms.Padding(3);
            this.pnlContainer.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelRibbonInactive;
            this.pnlContainer.Size = new System.Drawing.Size(560, 66);
            this.pnlContainer.TabIndex = 0;
            this.pnlContainer.UseWaitCursor = true;
            // 
            // grpContent
            // 
            this.grpContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpContent.GroupBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.grpContent.GroupBorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ControlGroupBox;
            this.grpContent.Location = new System.Drawing.Point(3, 3);
            this.grpContent.Name = "grpContent";
            // 
            // grpContent.Panel
            // 
            this.grpContent.Panel.Controls.Add(this.progressBar);
            this.grpContent.Panel.Controls.Add(this.lblMessage);
            this.grpContent.Panel.Padding = new System.Windows.Forms.Padding(8);
            this.grpContent.Panel.UseWaitCursor = true;
            this.grpContent.Size = new System.Drawing.Size(554, 60);
            this.grpContent.TabIndex = 0;
            this.grpContent.UseWaitCursor = true;
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(8, 32);
            this.progressBar.MarqueeAnimationSpeed = 50;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(534, 16);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 1;
            this.progressBar.UseWaitCursor = true;
            // 
            // lblMessage
            // 
            this.lblMessage.Location = new System.Drawing.Point(8, 8);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(59, 20);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.UseWaitCursor = true;
            this.lblMessage.Values.Text = "Message";
            // 
            // LoaderDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 66);
            this.Controls.Add(this.pnlContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoaderDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoaderDialog";
            this.TopMost = true;
            this.UseWaitCursor = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoaderDialog_FormClosing);
            this.Load += new System.EventHandler(this.LoaderDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlContainer)).EndInit();
            this.pnlContainer.ResumeLayout(false);
            this.grpContent.Panel.ResumeLayout(false);
            this.grpContent.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpContent)).EndInit();
            this.grpContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bgwkLoader;
        private System.ComponentModel.BackgroundWorker bgwkUnloader;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel pnlContainer;
        private ComponentFactory.Krypton.Toolkit.KryptonGroup grpContent;
        private System.Windows.Forms.ProgressBar progressBar;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblMessage;
    }
}