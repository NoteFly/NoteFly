//-----------------------------------------------------------------------
// <copyright file="FrmAbout.Designer.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2010-2012  Tom
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
    /// About window.
    /// </summary>
    public partial class FrmAbout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// The OK button
        /// </summary>
        private System.Windows.Forms.Button btnCloseAbout;

        /// <summary>
        /// Name product label
        /// </summary>
        private System.Windows.Forms.Label lblProductName;

        /// <summary>
        /// Version label
        /// </summary>
        private System.Windows.Forms.Label lblVersion;

        /// <summary>
        /// Link to official website
        /// </summary>
        private System.Windows.Forms.LinkLabel linklblWebsite;

        /// <summary>
        /// Label with info on how product is licesed.
        /// </summary>
        private System.Windows.Forms.Label lblTextLicense;

        /// <summary>
        /// Timer tmpUpdateLblProductEffect 
        /// </summary>
        private System.Windows.Forms.Timer tmrUpdateLblProductEffect;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">Is disposing</param>
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
            this.components = new System.ComponentModel.Container();
            this.btnCloseAbout = new System.Windows.Forms.Button();
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.linklblWebsite = new System.Windows.Forms.LinkLabel();
            this.lblTextLicense = new System.Windows.Forms.Label();
            this.tmrUpdateLblProductEffect = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnCloseAbout
            // 
            this.btnCloseAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseAbout.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCloseAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnCloseAbout.Location = new System.Drawing.Point(112, 135);
            this.btnCloseAbout.Name = "btnCloseAbout";
            this.btnCloseAbout.Size = new System.Drawing.Size(111, 26);
            this.btnCloseAbout.TabIndex = 25;
            this.btnCloseAbout.Text = "&Close";
            this.btnCloseAbout.UseCompatibleTextRendering = true;
            this.btnCloseAbout.Click += new System.EventHandler(this.okButton_Click);
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.BackColor = System.Drawing.Color.Transparent;
            this.lblProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold);
            this.lblProductName.ForeColor = System.Drawing.Color.Black;
            this.lblProductName.Location = new System.Drawing.Point(6, 5);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(0, 45);
            this.lblProductName.TabIndex = 26;
            this.lblProductName.UseCompatibleTextRendering = true;
            this.lblProductName.Click += new System.EventHandler(this.lblProductName_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lblVersion.ForeColor = System.Drawing.Color.Black;
            this.lblVersion.Location = new System.Drawing.Point(12, 47);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(0, 16);
            this.lblVersion.TabIndex = 27;
            // 
            // linklblWebsite
            // 
            this.linklblWebsite.AutoSize = true;
            this.linklblWebsite.BackColor = System.Drawing.Color.Transparent;
            this.linklblWebsite.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.linklblWebsite.Location = new System.Drawing.Point(10, 140);
            this.linklblWebsite.Name = "linklblWebsite";
            this.linklblWebsite.Size = new System.Drawing.Size(61, 22);
            this.linklblWebsite.TabIndex = 28;
            this.linklblWebsite.TabStop = true;
            this.linklblWebsite.Text = "Website";
            this.linklblWebsite.UseCompatibleTextRendering = true;
            this.linklblWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklblWebsite_LinkClicked);
            // 
            // lblTextLicense
            // 
            this.lblTextLicense.AutoEllipsis = true;
            this.lblTextLicense.BackColor = System.Drawing.Color.Transparent;
            this.lblTextLicense.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.lblTextLicense.ForeColor = System.Drawing.Color.Black;
            this.lblTextLicense.Location = new System.Drawing.Point(10, 74);
            this.lblTextLicense.Name = "lblTextLicense";
            this.lblTextLicense.Size = new System.Drawing.Size(222, 58);
            this.lblTextLicense.TabIndex = 30;
            this.lblTextLicense.Text = "This programme is released under the terms of Lesser GNU General Public License v" +
    "ersion3\r\n";
            this.lblTextLicense.UseCompatibleTextRendering = true;
            // 
            // tmrUpdateLblProductEffect
            // 
            this.tmrUpdateLblProductEffect.Interval = 30;
            this.tmrUpdateLblProductEffect.Tick += new System.EventHandler(this.tmpUpdateLblProductEffect_Tick);
            // 
            // FrmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCloseAbout;
            this.ClientSize = new System.Drawing.Size(235, 171);
            this.Controls.Add(this.lblTextLicense);
            this.Controls.Add(this.linklblWebsite);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblProductName);
            this.Controls.Add(this.btnCloseAbout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAbout";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
