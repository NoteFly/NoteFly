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
    public partial class FrmDownloader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDownloader));
            this.progressbarDownload = new System.Windows.Forms.ProgressBar();
            this.backgroundWorkerDownloader = new System.ComponentModel.BackgroundWorker();
            this.lblStatusUpdate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressbarDownload
            // 
            this.progressbarDownload.AccessibleDescription = null;
            this.progressbarDownload.AccessibleName = null;
            resources.ApplyResources(this.progressbarDownload, "progressbarDownload");
            this.progressbarDownload.BackColor = System.Drawing.Color.DimGray;
            this.progressbarDownload.BackgroundImage = null;
            this.progressbarDownload.Font = null;
            this.progressbarDownload.ForeColor = System.Drawing.SystemColors.ControlText;
            this.progressbarDownload.MarqueeAnimationSpeed = 0;
            this.progressbarDownload.Name = "progressbarDownload";
            this.progressbarDownload.Value = 2;
            // 
            // backgroundWorkerDownloader
            // 
            this.backgroundWorkerDownloader.WorkerReportsProgress = true;
            this.backgroundWorkerDownloader.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerDownloader_DoWork);
            this.backgroundWorkerDownloader.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerDownloader_RunWorkerCompleted);
            this.backgroundWorkerDownloader.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerDownloader_ProgressChanged);
            // 
            // lblStatusUpdate
            // 
            this.lblStatusUpdate.AccessibleDescription = null;
            this.lblStatusUpdate.AccessibleName = null;
            resources.ApplyResources(this.lblStatusUpdate, "lblStatusUpdate");
            this.lblStatusUpdate.BackColor = System.Drawing.Color.DarkGray;
            this.lblStatusUpdate.Font = null;
            this.lblStatusUpdate.Name = "lblStatusUpdate";
            this.lblStatusUpdate.UseCompatibleTextRendering = true;
            // 
            // FrmDownloader
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.BackgroundImage = null;
            this.Controls.Add(this.lblStatusUpdate);
            this.Controls.Add(this.progressbarDownload);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDownloader";
            this.ShowIcon = false;
            this.ResumeLayout(false);

        }

        #endregion
    }
}