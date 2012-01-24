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
        private System.Windows.Forms.Button okButton;

        /// <summary>
        /// name product label
        /// </summary>
        private System.Windows.Forms.Label lblProductName;

        /// <summary>
        /// version label
        /// </summary>
        private System.Windows.Forms.Label lblVersion;

        /// <summary>
        /// link to official website
        /// </summary>
        private System.Windows.Forms.LinkLabel linklblWebsite;

        /// <summary>
        /// Label with info on how product is licesed.
        /// </summary>
        private System.Windows.Forms.Label lblTextLicense;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">is disposing</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAbout));
            this.okButton = new System.Windows.Forms.Button();
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.linklblWebsite = new System.Windows.Forms.LinkLabel();
            this.lblTextLicense = new System.Windows.Forms.Label();
            this.tmpUpdateLblProductEffect = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.AccessibleDescription = null;
            this.okButton.AccessibleName = null;
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.BackgroundImage = null;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Name = "okButton";
            this.okButton.UseCompatibleTextRendering = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // lblProductName
            // 
            this.lblProductName.AccessibleDescription = null;
            this.lblProductName.AccessibleName = null;
            resources.ApplyResources(this.lblProductName, "lblProductName");
            this.lblProductName.ForeColor = System.Drawing.Color.Black;
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.UseCompatibleTextRendering = true;
            this.lblProductName.Click += new System.EventHandler(this.lblProductName_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.AccessibleDescription = null;
            this.lblVersion.AccessibleName = null;
            resources.ApplyResources(this.lblVersion, "lblVersion");
            this.lblVersion.ForeColor = System.Drawing.Color.Black;
            this.lblVersion.Name = "lblVersion";
            // 
            // linklblWebsite
            // 
            this.linklblWebsite.AccessibleDescription = null;
            this.linklblWebsite.AccessibleName = null;
            resources.ApplyResources(this.linklblWebsite, "linklblWebsite");
            this.linklblWebsite.Name = "linklblWebsite";
            this.linklblWebsite.TabStop = true;
            this.linklblWebsite.UseCompatibleTextRendering = true;
            this.linklblWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklblWebsite_LinkClicked);
            // 
            // lblTextLicense
            // 
            this.lblTextLicense.AccessibleDescription = null;
            this.lblTextLicense.AccessibleName = null;
            resources.ApplyResources(this.lblTextLicense, "lblTextLicense");
            this.lblTextLicense.AutoEllipsis = true;
            this.lblTextLicense.ForeColor = System.Drawing.Color.Black;
            this.lblTextLicense.Name = "lblTextLicense";
            this.lblTextLicense.UseCompatibleTextRendering = true;
            // 
            // tmpUpdateLblProductEffect
            // 
            this.tmpUpdateLblProductEffect.Interval = 30;
            this.tmpUpdateLblProductEffect.Tick += new System.EventHandler(this.tmpUpdateLblProductEffect_Tick);
            // 
            // FrmAbout
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.lblTextLicense);
            this.Controls.Add(this.linklblWebsite);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblProductName);
            this.Controls.Add(this.okButton);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAbout";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmpUpdateLblProductEffect;
    }
}
