namespace NoteFly
{
    partial class FrmUpdater
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
            this.progressbarDownload = new System.Windows.Forms.ProgressBar();
            this.backgroundWorkerDownloader = new System.ComponentModel.BackgroundWorker();
            this.lblStatusUpdate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressbarDownload
            // 
            this.progressbarDownload.BackColor = System.Drawing.Color.DimGray;
            this.progressbarDownload.ForeColor = System.Drawing.SystemColors.ControlText;
            this.progressbarDownload.Location = new System.Drawing.Point(2, 3);
            this.progressbarDownload.MarqueeAnimationSpeed = 200;
            this.progressbarDownload.Name = "progressbarDownload";
            this.progressbarDownload.Size = new System.Drawing.Size(350, 22);
            this.progressbarDownload.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressbarDownload.TabIndex = 1;
            this.progressbarDownload.Value = 2;
            // 
            // backgroundWorkerDownloader
            // 
            this.backgroundWorkerDownloader.WorkerReportsProgress = true;
            this.backgroundWorkerDownloader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorkerDownloader.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorkerDownloader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // lblStatusUpdate
            // 
            this.lblStatusUpdate.BackColor = System.Drawing.Color.DarkGray;
            this.lblStatusUpdate.Location = new System.Drawing.Point(142, 28);
            this.lblStatusUpdate.Name = "lblStatusUpdate";
            this.lblStatusUpdate.Size = new System.Drawing.Size(210, 19);
            this.lblStatusUpdate.TabIndex = 2;
            this.lblStatusUpdate.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblStatusUpdate.UseCompatibleTextRendering = true;
            // 
            // FrmUpdater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(354, 56);
            this.Controls.Add(this.lblStatusUpdate);
            this.Controls.Add(this.progressbarDownload);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = new System.Drawing.Point(100, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUpdater";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "updating";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressbarDownload;
        private System.ComponentModel.BackgroundWorker backgroundWorkerDownloader;
        private System.Windows.Forms.Label lblStatusUpdate;
    }
}

