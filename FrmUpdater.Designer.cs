//-----------------------------------------------------------------------
// <copyright file="FrmUpdater.Designer.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2011  Tom
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
// </copyright>
//-----------------------------------------------------------------------
namespace NoteFly
{
    /// <summary>
    /// Class to download and launch the update.
    /// </summary>
    public partial class FrmUpdater
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// ProgressBar progressbarDownload
        /// </summary>
        private System.Windows.Forms.ProgressBar progressbarDownload;
        
        /// <summary>
        /// BackgroundWorker backgroundWorkerDownloader
        /// </summary>
        private System.ComponentModel.BackgroundWorker backgroundWorkerDownloader;
        
        /// <summary>
        /// Label lblStatusUpdate
        /// </summary>
        private System.Windows.Forms.Label lblStatusUpdate;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            this.backgroundWorkerDownloader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerDownloader_DoWork);
            this.backgroundWorkerDownloader.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerDownloader_ProgressChanged);
            this.backgroundWorkerDownloader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerDownloader_RunWorkerCompleted);
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
    }
}