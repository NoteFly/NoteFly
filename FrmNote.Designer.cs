//-----------------------------------------------------------------------
// <copyright file="FrmNote.Designer.cs" company="GNU">
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
#define windows //platform can be: windows, linux, macos

using System.Drawing;
namespace NoteFly
{
    /// <summary>
    /// The Note form class
    /// </summary>
    public partial class FrmNote
    {

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Button btnCloseNote;
        private System.Windows.Forms.Panel pnlNote;
        private System.Windows.Forms.ContextMenuStrip menuFrmNoteOptions;
        private System.Windows.Forms.ToolStripMenuItem menuEditNote;
        private System.Windows.Forms.ToolStripMenuItem menuCopyContent;
        private System.Windows.Forms.ToolStripMenuItem menuNoteSkins;
        private System.Windows.Forms.PictureBox pbResizeGrip;
        private System.Windows.Forms.ToolStripMenuItem menuOnTop;
        private System.Windows.Forms.ToolStripMenuItem menuCopyTitle;
        private System.Windows.Forms.ToolStripMenuItem menuLockNote;
        private System.ComponentModel.BackgroundWorker SavePos;
        private System.Windows.Forms.ToolStripMenuItem menuHideNote;
        private System.Windows.Forms.ToolStripMenuItem menuSendTo;
        private System.Windows.Forms.ToolStripMenuItem menuSendToFacebook;
        private System.Windows.Forms.ToolStripMenuItem menuSendToTwitter;
        private System.Windows.Forms.ToolStripMenuItem menuSendToEmail;
        private System.Windows.Forms.ToolStripMenuItem menuSendToTextfile;
        private System.Windows.Forms.ToolStripMenuItem menuRollUp;

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
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlHead = new System.Windows.Forms.Panel();
            this.menuFrmNoteOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuEditNote = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNoteSkins = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSendTo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSendToEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSendToTwitter = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSendToFacebook = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSendToTextfile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopySelected = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopyContent = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopyTitle = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOnTop = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRollUp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLockNote = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHideNote = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCloseNote = new System.Windows.Forms.Button();
            this.rtbNote = new System.Windows.Forms.RichTextBox();
            this.pnlNote = new System.Windows.Forms.Panel();
            this.pbResizeGrip = new System.Windows.Forms.PictureBox();
            this.SavePos = new System.ComponentModel.BackgroundWorker();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlHead.SuspendLayout();
            this.menuFrmNoteOptions.SuspendLayout();
            this.pnlNote.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResizeGrip)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AccessibleDescription = "Note title";
            this.lblTitle.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(3, 5, 60, 0);
            this.lblTitle.Size = new System.Drawing.Size(81, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "?";
            this.lblTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseMove);
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseDown);
            this.lblTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseUp);
            // 
            // pnlHead
            // 
            this.pnlHead.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.pnlHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHead.ContextMenuStrip = this.menuFrmNoteOptions;
            this.pnlHead.Controls.Add(this.btnCloseNote);
            this.pnlHead.Controls.Add(this.lblTitle);
            this.pnlHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHead.Location = new System.Drawing.Point(0, 0);
            this.pnlHead.Name = "pnlHead";
            this.pnlHead.Size = new System.Drawing.Size(240, 32);
            this.pnlHead.TabIndex = 1;
            this.pnlHead.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseMove);
            this.pnlHead.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseDown);
            this.pnlHead.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseUp);
            // 
            // menuFrmNoteOptions
            // 
            this.menuFrmNoteOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEditNote,
            this.menuNoteSkins,
            this.menuSendTo,
            this.menuCopySelected,
            this.menuCopyContent,
            this.menuCopyTitle,
            this.menuOnTop,
            this.menuRollUp,
            this.menuLockNote,
            this.menuHideNote});
            this.menuFrmNoteOptions.Name = "contextMenuStripNoteOptions";
            this.menuFrmNoteOptions.Size = new System.Drawing.Size(227, 224);
            this.menuFrmNoteOptions.Text = "-menu-";
            this.menuFrmNoteOptions.Opening += new System.ComponentModel.CancelEventHandler(this.menuFrmNoteOptions_Opening);
            // 
            // menuEditNote
            // 
            this.menuEditNote.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuEditNote.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.menuEditNote.Name = "menuEditNote";
            this.menuEditNote.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.menuEditNote.Size = new System.Drawing.Size(226, 22);
            this.menuEditNote.Text = "&Edit note";
            this.menuEditNote.Click += new System.EventHandler(this.editTToolStripMenuItem_Click);
            // 
            // menuNoteSkins
            // 
            this.menuNoteSkins.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuNoteSkins.Name = "menuNoteSkins";
            this.menuNoteSkins.Size = new System.Drawing.Size(226, 22);
            this.menuNoteSkins.Text = "&Color";
            // 
            // menuSendTo
            // 
            this.menuSendTo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSendToEmail,
            this.menuSendToTwitter,
            this.menuSendToFacebook,
            this.menuSendToTextfile});
            this.menuSendTo.Name = "menuSendTo";
            this.menuSendTo.Size = new System.Drawing.Size(226, 22);
            this.menuSendTo.Text = "&Send to";
            // 
            // menuSendToEmail
            // 
            this.menuSendToEmail.Name = "menuSendToEmail";
            this.menuSendToEmail.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.menuSendToEmail.Size = new System.Drawing.Size(166, 22);
            this.menuSendToEmail.Text = "E-&mail";
            this.menuSendToEmail.Click += new System.EventHandler(this.emailToolStripMenuItem_Click);
            // 
            // menuSendToTwitter
            // 
            this.menuSendToTwitter.Name = "menuSendToTwitter";
            this.menuSendToTwitter.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.menuSendToTwitter.Size = new System.Drawing.Size(166, 22);
            this.menuSendToTwitter.Text = "&Twitter";
            this.menuSendToTwitter.Click += new System.EventHandler(this.tsmenuSendToTwitter_Click);
            // 
            // menuSendToFacebook
            // 
            this.menuSendToFacebook.Name = "menuSendToFacebook";
            this.menuSendToFacebook.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.menuSendToFacebook.Size = new System.Drawing.Size(166, 22);
            this.menuSendToFacebook.Text = "Face&Book";
            this.menuSendToFacebook.Click += new System.EventHandler(this.tsmenuSendToFacebook_Click);
            // 
            // menuSendToTextfile
            // 
            this.menuSendToTextfile.Name = "menuSendToTextfile";
            this.menuSendToTextfile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.menuSendToTextfile.Size = new System.Drawing.Size(166, 22);
            this.menuSendToTextfile.Text = "Text&file";
            this.menuSendToTextfile.Click += new System.EventHandler(this.tsmenuSendToTextfile_Click);
            // 
            // menuCopySelected
            // 
            this.menuCopySelected.Enabled = false;
            this.menuCopySelected.Name = "menuCopySelected";
            this.menuCopySelected.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.menuCopySelected.Size = new System.Drawing.Size(226, 22);
            this.menuCopySelected.Text = "&Copy selected text";
            this.menuCopySelected.Click += new System.EventHandler(this.menuCopySelected_Click);
            // 
            // menuCopyContent
            // 
            this.menuCopyContent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuCopyContent.Name = "menuCopyContent";
            this.menuCopyContent.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
                        | System.Windows.Forms.Keys.C)));
            this.menuCopyContent.Size = new System.Drawing.Size(226, 22);
            this.menuCopyContent.Text = "Copy &all content";
            this.menuCopyContent.Click += new System.EventHandler(this.copyTextToolStripMenuItem_Click);
            // 
            // menuCopyTitle
            // 
            this.menuCopyTitle.Name = "menuCopyTitle";
            this.menuCopyTitle.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.C)));
            this.menuCopyTitle.Size = new System.Drawing.Size(226, 22);
            this.menuCopyTitle.Text = "Copy &title";
            this.menuCopyTitle.Click += new System.EventHandler(this.copyTitleToolStripMenuItem_Click);
            // 
            // menuOnTop
            // 
            this.menuOnTop.CheckOnClick = true;
            this.menuOnTop.Name = "menuOnTop";
            this.menuOnTop.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.menuOnTop.Size = new System.Drawing.Size(226, 22);
            this.menuOnTop.Text = "Sticky on &top";
            this.menuOnTop.Click += new System.EventHandler(this.OnTopToolStripMenuItem_Click);
            // 
            // menuRollUp
            // 
            this.menuRollUp.Name = "menuRollUp";
            this.menuRollUp.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.menuRollUp.Size = new System.Drawing.Size(226, 22);
            this.menuRollUp.Text = "&Roll up";
            this.menuRollUp.Click += new System.EventHandler(this.menuRollUp_Click);
            // 
            // menuLockNote
            // 
            this.menuLockNote.CheckOnClick = true;
            this.menuLockNote.Name = "menuLockNote";
            this.menuLockNote.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.menuLockNote.Size = new System.Drawing.Size(226, 22);
            this.menuLockNote.Text = "&Lock note";
            this.menuLockNote.Click += new System.EventHandler(this.locknoteToolStripMenuItem_Click);
            // 
            // menuHideNote
            // 
            this.menuHideNote.Name = "menuHideNote";
            this.menuHideNote.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.menuHideNote.Size = new System.Drawing.Size(226, 22);
            this.menuHideNote.Text = "&Hide note";
            this.menuHideNote.Click += new System.EventHandler(this.hideNoteToolStripMenuItem_Click);
            // 
            // btnCloseNote
            // 
            this.btnCloseNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCloseNote.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCloseNote.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseNote.Font = new System.Drawing.Font("Kartika", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseNote.Location = new System.Drawing.Point(200, 4);
            this.btnCloseNote.Margin = new System.Windows.Forms.Padding(0);
            this.btnCloseNote.Name = "btnCloseNote";
            this.btnCloseNote.Size = new System.Drawing.Size(31, 23);
            this.btnCloseNote.TabIndex = 1;
            this.btnCloseNote.TabStop = false;
            this.btnCloseNote.Text = "X";
            this.toolTip.SetToolTip(this.btnCloseNote, "Hide this note");
            this.btnCloseNote.UseVisualStyleBackColor = true;
            this.btnCloseNote.Click += new System.EventHandler(this.btnCloseNote_Click);
            // 
            // rtbNote
            // 
            this.rtbNote.AccessibleDescription = "Note content";
            this.rtbNote.AccessibleRole = System.Windows.Forms.AccessibleRole.Document;
            this.rtbNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbNote.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbNote.CausesValidation = false;
            this.rtbNote.ContextMenuStrip = this.menuFrmNoteOptions;
            this.rtbNote.Cursor = System.Windows.Forms.Cursors.Default;
            this.rtbNote.DetectUrls = false;
            this.rtbNote.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbNote.ForeColor = System.Drawing.Color.Black;
            this.rtbNote.ImeMode = System.Windows.Forms.ImeMode.On;
            this.rtbNote.Location = new System.Drawing.Point(7, 6);
            this.rtbNote.Margin = new System.Windows.Forms.Padding(10);
            this.rtbNote.MaxLength = 1000000;
            this.rtbNote.Name = "rtbNote";
            this.rtbNote.ReadOnly = true;
            this.rtbNote.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtbNote.ShortcutsEnabled = false;
            this.rtbNote.Size = new System.Drawing.Size(221, 184);
            this.rtbNote.TabIndex = 3;
            this.rtbNote.TabStop = false;
            this.rtbNote.Text = "?";
            this.rtbNote.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbNote_LinkClicked);
            // 
            // pnlNote
            // 
            this.pnlNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlNote.BackColor = System.Drawing.Color.Transparent;
            this.pnlNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNote.ContextMenuStrip = this.menuFrmNoteOptions;
            this.pnlNote.Controls.Add(this.pbResizeGrip);
            this.pnlNote.Controls.Add(this.rtbNote);
            this.pnlNote.Location = new System.Drawing.Point(0, 31);
            this.pnlNote.Name = "pnlNote";
            this.pnlNote.Size = new System.Drawing.Size(240, 209);
            this.pnlNote.TabIndex = 4;
            // 
            // pbResizeGrip
            // 
            this.pbResizeGrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbResizeGrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbResizeGrip.Image = global::NoteFly.Properties.Resources.hoekje;
            this.pbResizeGrip.Location = new System.Drawing.Point(223, 191);
            this.pbResizeGrip.Margin = new System.Windows.Forms.Padding(0);
            this.pbResizeGrip.Name = "pbResizeGrip";
            this.pbResizeGrip.Size = new System.Drawing.Size(16, 16);
            this.pbResizeGrip.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbResizeGrip.TabIndex = 4;
            this.pbResizeGrip.TabStop = false;
            this.pbResizeGrip.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbResizeGrip_MouseMove);
            this.pbResizeGrip.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbResizeGrip_MouseUp);
            // 
            // SavePos
            // 
            this.SavePos.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SavePos_DoWork);
            // 
            // toolTip
            // 
            this.toolTip.Active = false;
            // 
            // FrmNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCloseNote;
            this.ClientSize = new System.Drawing.Size(240, 240);
            this.ContextMenuStrip = this.menuFrmNoteOptions;
            this.ControlBox = false;
            this.Controls.Add(this.pnlNote);
            this.Controls.Add(this.pnlHead);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(100, 50);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1023, 799);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(80, 60);
            this.Name = "FrmNote";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.Deactivate += new System.EventHandler(this.frmNote_Deactivate);
            this.Activated += new System.EventHandler(this.frmNote_Activated);
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.menuFrmNoteOptions.ResumeLayout(false);
            this.pnlNote.ResumeLayout(false);
            this.pnlNote.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResizeGrip)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox rtbNote;
        private System.Windows.Forms.ToolStripMenuItem menuCopySelected;
        public System.Windows.Forms.ToolTip toolTip;
    }
}