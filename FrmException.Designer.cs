//-----------------------------------------------------------------------
// <copyright file="FrmException.Designer.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2010-2011  Tom
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
    /// Exception window
    /// </summary>
    public partial class FrmException
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Label lblTextExceptionMessage
        /// </summary>
        private System.Windows.Forms.Label lblTextExceptionMessage;

        /// <summary>
        /// Button btnContinu
        /// </summary>
        private System.Windows.Forms.Button btnContinu;

        /// <summary>
        /// Button btnShutdown
        /// </summary>
        private System.Windows.Forms.Button btnShutdown;

        /// <summary>
        /// TextBox tbExceptionMessage
        /// </summary>
        private System.Windows.Forms.TextBox tbExceptionMessage;

        /// <summary>
        /// Label lblTextStacktrace
        /// </summary>
        private System.Windows.Forms.Label lblTextStacktrace;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmException));
            this.lblTextExceptionMessage = new System.Windows.Forms.Label();
            this.btnContinu = new System.Windows.Forms.Button();
            this.btnShutdown = new System.Windows.Forms.Button();
            this.tbExceptionMessage = new System.Windows.Forms.TextBox();
            this.lblTextStacktrace = new System.Windows.Forms.Label();
            this.linklblCreateBugReport = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lblTextExceptionMessage
            // 
            resources.ApplyResources(this.lblTextExceptionMessage, "lblTextExceptionMessage");
            this.lblTextExceptionMessage.Name = "lblTextExceptionMessage";
            // 
            // btnContinu
            // 
            resources.ApplyResources(this.btnContinu, "btnContinu");
            this.btnContinu.Name = "btnContinu";
            this.btnContinu.UseVisualStyleBackColor = true;
            this.btnContinu.Click += new System.EventHandler(this.btnContinu_Click);
            // 
            // btnShutdown
            // 
            resources.ApplyResources(this.btnShutdown, "btnShutdown");
            this.btnShutdown.Name = "btnShutdown";
            this.btnShutdown.UseVisualStyleBackColor = true;
            this.btnShutdown.Click += new System.EventHandler(this.btnShutdown_Click);
            // 
            // tbExceptionMessage
            // 
            resources.ApplyResources(this.tbExceptionMessage, "tbExceptionMessage");
            this.tbExceptionMessage.Name = "tbExceptionMessage";
            // 
            // lblTextStacktrace
            // 
            resources.ApplyResources(this.lblTextStacktrace, "lblTextStacktrace");
            this.lblTextStacktrace.Name = "lblTextStacktrace";
            // 
            // linklblCreateBugReport
            // 
            resources.ApplyResources(this.linklblCreateBugReport, "linklblCreateBugReport");
            this.linklblCreateBugReport.Name = "linklblCreateBugReport";
            this.linklblCreateBugReport.TabStop = true;
            this.linklblCreateBugReport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklblCreateBugReport_LinkClicked);
            // 
            // FrmException
            // 
            this.AcceptButton = this.btnContinu;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.Controls.Add(this.linklblCreateBugReport);
            this.Controls.Add(this.lblTextStacktrace);
            this.Controls.Add(this.tbExceptionMessage);
            this.Controls.Add(this.btnShutdown);
            this.Controls.Add(this.btnContinu);
            this.Controls.Add(this.lblTextExceptionMessage);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmException";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linklblCreateBugReport;
    }
}