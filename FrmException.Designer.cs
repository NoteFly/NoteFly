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
        /// LinkLabel linklblCreateBugReport
        /// </summary>
        private System.Windows.Forms.LinkLabel linklblCreateBugReport;

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
            this.lblTextExceptionMessage.AutoSize = true;
            this.lblTextExceptionMessage.Location = new System.Drawing.Point(2, 9);
            this.lblTextExceptionMessage.Name = "lblTextExceptionMessage";
            this.lblTextExceptionMessage.Size = new System.Drawing.Size(333, 13);
            this.lblTextExceptionMessage.TabIndex = 0;
            this.lblTextExceptionMessage.Text = "NoteFly has crashed, an exception with the following message occur:";
            // 
            // btnContinu
            // 
            this.btnContinu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnContinu.Location = new System.Drawing.Point(185, 132);
            this.btnContinu.Name = "btnContinu";
            this.btnContinu.Size = new System.Drawing.Size(191, 23);
            this.btnContinu.TabIndex = 1;
            this.btnContinu.Text = "try to continu";
            this.btnContinu.UseVisualStyleBackColor = true;
            this.btnContinu.Click += new System.EventHandler(this.btnContinu_Click);
            // 
            // btnShutdown
            // 
            this.btnShutdown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnShutdown.Location = new System.Drawing.Point(5, 132);
            this.btnShutdown.Name = "btnShutdown";
            this.btnShutdown.Size = new System.Drawing.Size(174, 23);
            this.btnShutdown.TabIndex = 2;
            this.btnShutdown.Text = "shutdown";
            this.btnShutdown.UseVisualStyleBackColor = true;
            this.btnShutdown.Click += new System.EventHandler(this.btnShutdown_Click);
            // 
            // tbExceptionMessage
            // 
            this.tbExceptionMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbExceptionMessage.Location = new System.Drawing.Point(5, 25);
            this.tbExceptionMessage.Multiline = true;
            this.tbExceptionMessage.Name = "tbExceptionMessage";
            this.tbExceptionMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbExceptionMessage.Size = new System.Drawing.Size(371, 69);
            this.tbExceptionMessage.TabIndex = 7;
            // 
            // lblTextStacktrace
            // 
            this.lblTextStacktrace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTextStacktrace.Location = new System.Drawing.Point(5, 97);
            this.lblTextStacktrace.Name = "lblTextStacktrace";
            this.lblTextStacktrace.Size = new System.Drawing.Size(361, 32);
            this.lblTextStacktrace.TabIndex = 8;
            this.lblTextStacktrace.Text = "The exception is written to the logfile for bug reporting.\r\nOptions:\r\n";
            // 
            // linklblCreateBugReport
            // 
            this.linklblCreateBugReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linklblCreateBugReport.AutoSize = true;
            this.linklblCreateBugReport.Location = new System.Drawing.Point(291, 116);
            this.linklblCreateBugReport.Name = "linklblCreateBugReport";
            this.linklblCreateBugReport.Size = new System.Drawing.Size(85, 13);
            this.linklblCreateBugReport.TabIndex = 9;
            this.linklblCreateBugReport.TabStop = true;
            this.linklblCreateBugReport.Text = "create bugreport";
            this.linklblCreateBugReport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklblCreateBugReport_LinkClicked);
            // 
            // FrmException
            // 
            this.AcceptButton = this.btnContinu;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(384, 162);
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
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}