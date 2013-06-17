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
        private System.Windows.Forms.Timer tmrUpdate;

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
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanelAboutWindow = new System.Windows.Forms.TableLayoutPanel();
            this.pnlHead = new System.Windows.Forms.Panel();
            this.lbTextWindowTitle = new System.Windows.Forms.Label();
            this.pnlAuthors = new System.Windows.Forms.Panel();
            this.tableLayoutPanelAboutWindow.SuspendLayout();
            this.pnlHead.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCloseAbout
            // 
            this.btnCloseAbout.BackColor = System.Drawing.Color.Gray;
            this.tableLayoutPanelAboutWindow.SetColumnSpan(this.btnCloseAbout, 2);
            this.btnCloseAbout.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCloseAbout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCloseAbout.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCloseAbout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.btnCloseAbout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnCloseAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnCloseAbout.ForeColor = System.Drawing.Color.White;
            this.btnCloseAbout.Location = new System.Drawing.Point(170, 237);
            this.btnCloseAbout.Margin = new System.Windows.Forms.Padding(0, 2, 6, 6);
            this.btnCloseAbout.Name = "btnCloseAbout";
            this.btnCloseAbout.Size = new System.Drawing.Size(162, 30);
            this.btnCloseAbout.TabIndex = 25;
            this.btnCloseAbout.Text = "&Close";
            this.btnCloseAbout.UseCompatibleTextRendering = true;
            this.btnCloseAbout.UseVisualStyleBackColor = false;
            this.btnCloseAbout.Click += new System.EventHandler(this.okButton_Click);
            // 
            // lblProductName
            // 
            this.lblProductName.AutoEllipsis = true;
            this.lblProductName.AutoSize = true;
            this.lblProductName.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelAboutWindow.SetColumnSpan(this.lblProductName, 2);
            this.lblProductName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProductName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblProductName.Font = new System.Drawing.Font("Impact", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductName.ForeColor = System.Drawing.Color.Gold;
            this.lblProductName.Location = new System.Drawing.Point(3, 40);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(221, 80);
            this.lblProductName.TabIndex = 26;
            this.lblProductName.UseCompatibleTextRendering = true;
            this.lblProductName.Click += new System.EventHandler(this.lblProductName_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.AutoEllipsis = true;
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanelAboutWindow.SetColumnSpan(this.lblVersion, 2);
            this.lblVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblVersion.Location = new System.Drawing.Point(12, 120);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(12, 0, 3, 0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(212, 58);
            this.lblVersion.TabIndex = 27;
            // 
            // linklblWebsite
            // 
            this.linklblWebsite.AutoSize = true;
            this.linklblWebsite.BackColor = System.Drawing.Color.Transparent;
            this.linklblWebsite.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.linklblWebsite.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.linklblWebsite.Location = new System.Drawing.Point(10, 239);
            this.linklblWebsite.Margin = new System.Windows.Forms.Padding(10, 4, 3, 0);
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
            this.tableLayoutPanelAboutWindow.SetColumnSpan(this.lblTextLicense, 2);
            this.lblTextLicense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTextLicense.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextLicense.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblTextLicense.Location = new System.Drawing.Point(8, 178);
            this.lblTextLicense.Margin = new System.Windows.Forms.Padding(8, 0, 3, 0);
            this.lblTextLicense.Name = "lblTextLicense";
            this.lblTextLicense.Size = new System.Drawing.Size(216, 57);
            this.lblTextLicense.TabIndex = 30;
            this.lblTextLicense.Text = "This programme is released under the terms of Lesser GNU General Public License v" +
    "ersion3\r\n";
            this.lblTextLicense.UseCompatibleTextRendering = true;
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Enabled = true;
            this.tmrUpdate.Interval = 17;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmpUpdateLblProductEffect_Tick);
            // 
            // tableLayoutPanelAboutWindow
            // 
            this.tableLayoutPanelAboutWindow.ColumnCount = 3;
            this.tableLayoutPanelAboutWindow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.66666F));
            this.tableLayoutPanelAboutWindow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.33333F));
            this.tableLayoutPanelAboutWindow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanelAboutWindow.Controls.Add(this.lblTextLicense, 0, 3);
            this.tableLayoutPanelAboutWindow.Controls.Add(this.lblProductName, 0, 1);
            this.tableLayoutPanelAboutWindow.Controls.Add(this.btnCloseAbout, 1, 4);
            this.tableLayoutPanelAboutWindow.Controls.Add(this.lblVersion, 0, 2);
            this.tableLayoutPanelAboutWindow.Controls.Add(this.linklblWebsite, 0, 4);
            this.tableLayoutPanelAboutWindow.Controls.Add(this.pnlHead, 0, 0);
            this.tableLayoutPanelAboutWindow.Controls.Add(this.pnlAuthors, 2, 1);
            this.tableLayoutPanelAboutWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelAboutWindow.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelAboutWindow.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelAboutWindow.Name = "tableLayoutPanelAboutWindow";
            this.tableLayoutPanelAboutWindow.RowCount = 5;
            this.tableLayoutPanelAboutWindow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelAboutWindow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.97102F));
            this.tableLayoutPanelAboutWindow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.02898F));
            this.tableLayoutPanelAboutWindow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tableLayoutPanelAboutWindow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanelAboutWindow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanelAboutWindow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelAboutWindow.Size = new System.Drawing.Size(338, 273);
            this.tableLayoutPanelAboutWindow.TabIndex = 31;
            // 
            // pnlHead
            // 
            this.pnlHead.BackColor = System.Drawing.Color.Gray;
            this.tableLayoutPanelAboutWindow.SetColumnSpan(this.pnlHead, 3);
            this.pnlHead.Controls.Add(this.lbTextWindowTitle);
            this.pnlHead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHead.Location = new System.Drawing.Point(1, 1);
            this.pnlHead.Margin = new System.Windows.Forms.Padding(1);
            this.pnlHead.Name = "pnlHead";
            this.pnlHead.Size = new System.Drawing.Size(336, 38);
            this.pnlHead.TabIndex = 31;
            this.pnlHead.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseDown);
            this.pnlHead.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseMove);
            // 
            // lbTextWindowTitle
            // 
            this.lbTextWindowTitle.AutoSize = true;
            this.lbTextWindowTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTextWindowTitle.Location = new System.Drawing.Point(11, 8);
            this.lbTextWindowTitle.Name = "lbTextWindowTitle";
            this.lbTextWindowTitle.Size = new System.Drawing.Size(0, 20);
            this.lbTextWindowTitle.TabIndex = 0;
            // 
            // pnlAuthors
            // 
            this.pnlAuthors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAuthors.Location = new System.Drawing.Point(230, 43);
            this.pnlAuthors.Name = "pnlAuthors";
            this.tableLayoutPanelAboutWindow.SetRowSpan(this.pnlAuthors, 2);
            this.pnlAuthors.Size = new System.Drawing.Size(105, 132);
            this.pnlAuthors.TabIndex = 32;
            // 
            // FrmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.CancelButton = this.btnCloseAbout;
            this.ClientSize = new System.Drawing.Size(338, 273);
            this.Controls.Add(this.tableLayoutPanelAboutWindow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAbout";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.tableLayoutPanelAboutWindow.ResumeLayout(false);
            this.tableLayoutPanelAboutWindow.PerformLayout();
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelAboutWindow;
        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Label lbTextWindowTitle;
        private System.Windows.Forms.Panel pnlAuthors;
    }
}
