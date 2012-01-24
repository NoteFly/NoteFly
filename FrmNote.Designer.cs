//-----------------------------------------------------------------------
// <copyright file="FrmNote.Designer.cs" company="NoteFly">
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
    /// The Note form class
    /// </summary>
    public partial class FrmNote
    {
        /// <summary>
        /// TransparentRichTextBox rtbNote
        /// </summary>
        private TransparentRichTextBox rtbNote;

        /// <summary>
        /// ToolTip toolTip
        /// </summary>
        private System.Windows.Forms.ToolTip toolTip;

        /// <summary>
        /// Label lblTitle
        /// </summary>
        private System.Windows.Forms.Label lblTitle;

        /// <summary>
        /// Panel pnlHead
        /// </summary>
        private System.Windows.Forms.Panel pnlHead;

        /// <summary>
        /// Button btnCloseNote
        /// </summary>
        private System.Windows.Forms.Button btnHideNote;

        /// <summary>
        /// Panel pnlNote
        /// </summary>
        private System.Windows.Forms.Panel pnlNote;

        /// <summary>
        /// ContextMenuStrip menuFrmNoteOptions
        /// </summary>
        private System.Windows.Forms.ContextMenuStrip menuFrmNoteOptions;

        /// <summary>
        /// ToolStripMenuItem menuEditNote
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem menuEditNote;

        /// <summary>
        /// ToolStripMenuItem menuNoteSkins
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem menuNoteSkins;

        /// <summary>
        /// PictureBox pbResizeGrip
        /// </summary>
        private System.Windows.Forms.PictureBox pbResizeGrip;

        /// <summary>
        /// ToolStripMenuItem menuOnTop
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem menuOnTop;

        /// <summary>
        /// ToolStripMenuItem menuLockNote
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem menuLockNote;

        /// <summary>
        /// ToolStripMenuItem menuHideNote
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem menuHideNote;

        /// <summary>
        /// ToolStripMenuItem menuRollUp
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem menuRollUp;

        /// <summary>
        /// BackgroundWorker SaveWorker
        /// </summary>
        private System.ComponentModel.BackgroundWorker saveWorker;

        /// <summary>
        /// ToolStripMenuItem menuSendTo
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem menuSendTo;

        /// <summary>
        /// ToolStripMenuItem menuSendToEmail
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem menuSendToEmail;

        /// <summary>
        /// ToolStripMenuItem menuSendToTextfile
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem menuSendToTextfile;

        /// <summary>
        /// ToolStripMenuItem menuCopy 
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem menuCopy;

        /// <summary>
        /// ToolStripMenuItem menuCopySelected
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem menuCopySelected;

        /// <summary>
        /// ToolStripMenuItem menuCopyContent
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem menuCopyContent;

        /// <summary>
        /// ToolStripMenuItem menuCopyTitle
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem menuCopyTitle;

        /// <summary>
        /// ToolStripMenuItem menuWordWrap
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem menuWordWrap;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNote));
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlHead = new System.Windows.Forms.Panel();
            this.menuFrmNoteOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuEditNote = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNoteSkins = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSendTo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSendToEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSendToTextfile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOnTop = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLockNote = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWordWrap = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopySelected = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopyContent = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopyTitle = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRollUp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHideNote = new System.Windows.Forms.ToolStripMenuItem();
            this.btnHideNote = new System.Windows.Forms.Button();
            this.pnlNote = new System.Windows.Forms.Panel();
            this.rtbNote = new NoteFly.TransparentRichTextBox();
            this.pbResizeGrip = new System.Windows.Forms.PictureBox();
            this.saveWorker = new System.ComponentModel.BackgroundWorker();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlHead.SuspendLayout();
            this.menuFrmNoteOptions.SuspendLayout();
            this.pnlNote.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResizeGrip)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.UseCompatibleTextRendering = true;
            this.lblTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseMove);
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseDown);
            this.lblTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseUp);
            // 
            // pnlHead
            // 
            this.pnlHead.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.pnlHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHead.ContextMenuStrip = this.menuFrmNoteOptions;
            this.pnlHead.Controls.Add(this.btnHideNote);
            this.pnlHead.Controls.Add(this.lblTitle);
            resources.ApplyResources(this.pnlHead, "pnlHead");
            this.pnlHead.Name = "pnlHead";
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
            this.menuOnTop,
            this.menuLockNote,
            this.menuWordWrap,
            this.menuCopy,
            this.menuRollUp,
            this.menuHideNote});
            this.menuFrmNoteOptions.Name = "contextMenuStripNoteOptions";
            resources.ApplyResources(this.menuFrmNoteOptions, "menuFrmNoteOptions");
            this.menuFrmNoteOptions.Opening += new System.ComponentModel.CancelEventHandler(this.menuFrmNoteOptions_Opening);
            // 
            // menuEditNote
            // 
            this.menuEditNote.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.menuEditNote, "menuEditNote");
            this.menuEditNote.Name = "menuEditNote";
            this.menuEditNote.Click += new System.EventHandler(this.menuEditNote_Click);
            // 
            // menuNoteSkins
            // 
            this.menuNoteSkins.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuNoteSkins.Name = "menuNoteSkins";
            resources.ApplyResources(this.menuNoteSkins, "menuNoteSkins");
            this.menuNoteSkins.DropDownOpening += new System.EventHandler(this.menuNoteSkins_DropDownOpening);
            // 
            // menuSendTo
            // 
            this.menuSendTo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSendToEmail,
            this.menuSendToTextfile});
            this.menuSendTo.Name = "menuSendTo";
            resources.ApplyResources(this.menuSendTo, "menuSendTo");
            this.menuSendTo.DropDownOpening += new System.EventHandler(this.menuSendTo_DropDownOpening);
            // 
            // menuSendToEmail
            // 
            this.menuSendToEmail.Name = "menuSendToEmail";
            resources.ApplyResources(this.menuSendToEmail, "menuSendToEmail");
            this.menuSendToEmail.Click += new System.EventHandler(this.menuSendToEmail_Click);
            // 
            // menuSendToTextfile
            // 
            this.menuSendToTextfile.Name = "menuSendToTextfile";
            resources.ApplyResources(this.menuSendToTextfile, "menuSendToTextfile");
            this.menuSendToTextfile.Click += new System.EventHandler(this.menuSendToFile_Click);
            // 
            // menuOnTop
            // 
            this.menuOnTop.CheckOnClick = true;
            this.menuOnTop.Name = "menuOnTop";
            resources.ApplyResources(this.menuOnTop, "menuOnTop");
            this.menuOnTop.Click += new System.EventHandler(this.menuOnTop_Click);
            // 
            // menuLockNote
            // 
            this.menuLockNote.CheckOnClick = true;
            this.menuLockNote.Name = "menuLockNote";
            resources.ApplyResources(this.menuLockNote, "menuLockNote");
            this.menuLockNote.Click += new System.EventHandler(this.menuLockNote_Click);
            // 
            // menuWordWrap
            // 
            this.menuWordWrap.CheckOnClick = true;
            this.menuWordWrap.Name = "menuWordWrap";
            resources.ApplyResources(this.menuWordWrap, "menuWordWrap");
            this.menuWordWrap.Click += new System.EventHandler(this.menuWordWrap_Click);
            // 
            // menuCopy
            // 
            this.menuCopy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCopySelected,
            this.menuCopyContent,
            this.menuCopyTitle});
            this.menuCopy.Name = "menuCopy";
            resources.ApplyResources(this.menuCopy, "menuCopy");
            // 
            // menuCopySelected
            // 
            this.menuCopySelected.Name = "menuCopySelected";
            resources.ApplyResources(this.menuCopySelected, "menuCopySelected");
            this.menuCopySelected.Click += new System.EventHandler(this.menuCopySelected_Click);
            // 
            // menuCopyContent
            // 
            this.menuCopyContent.Name = "menuCopyContent";
            resources.ApplyResources(this.menuCopyContent, "menuCopyContent");
            this.menuCopyContent.Click += new System.EventHandler(this.menuCopyContent_Click);
            // 
            // menuCopyTitle
            // 
            this.menuCopyTitle.Name = "menuCopyTitle";
            resources.ApplyResources(this.menuCopyTitle, "menuCopyTitle");
            this.menuCopyTitle.Click += new System.EventHandler(this.menuCopyTitle_Click);
            // 
            // menuRollUp
            // 
            this.menuRollUp.Name = "menuRollUp";
            resources.ApplyResources(this.menuRollUp, "menuRollUp");
            this.menuRollUp.Click += new System.EventHandler(this.menuRollUp_Click);
            // 
            // menuHideNote
            // 
            this.menuHideNote.Name = "menuHideNote";
            resources.ApplyResources(this.menuHideNote, "menuHideNote");
            this.menuHideNote.Click += new System.EventHandler(this.menuHideNote_Click);
            // 
            // btnHideNote
            // 
            resources.ApplyResources(this.btnHideNote, "btnHideNote");
            this.btnHideNote.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHideNote.Name = "btnHideNote";
            this.btnHideNote.TabStop = false;
            this.toolTip.SetToolTip(this.btnHideNote, resources.GetString("btnHideNote.ToolTip"));
            this.btnHideNote.UseVisualStyleBackColor = true;
            this.btnHideNote.Click += new System.EventHandler(this.btnHideNote_Click);
            // 
            // pnlNote
            // 
            resources.ApplyResources(this.pnlNote, "pnlNote");
            this.pnlNote.BackColor = System.Drawing.Color.Transparent;
            this.pnlNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNote.ContextMenuStrip = this.menuFrmNoteOptions;
            this.pnlNote.Controls.Add(this.rtbNote);
            this.pnlNote.Controls.Add(this.pbResizeGrip);
            this.pnlNote.Name = "pnlNote";
            // 
            // rtbNote
            // 
            resources.ApplyResources(this.rtbNote, "rtbNote");
            this.rtbNote.AccessibleRole = System.Windows.Forms.AccessibleRole.Document;
            this.rtbNote.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbNote.CausesValidation = false;
            this.rtbNote.ContextMenuStrip = this.menuFrmNoteOptions;
            this.rtbNote.Cursor = System.Windows.Forms.Cursors.Default;
            this.rtbNote.DetectUrls = false;
            this.rtbNote.ForeColor = System.Drawing.Color.Black;
            this.rtbNote.Name = "rtbNote";
            this.rtbNote.ReadOnly = true;
            this.rtbNote.ShortcutsEnabled = false;
            this.rtbNote.TabStop = false;
            this.rtbNote.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbNote_LinkClicked);
            // 
            // pbResizeGrip
            // 
            resources.ApplyResources(this.pbResizeGrip, "pbResizeGrip");
            this.pbResizeGrip.Image = global::NoteFly.Properties.Resources.hoekje;
            this.pbResizeGrip.Name = "pbResizeGrip";
            this.pbResizeGrip.TabStop = false;
            this.pbResizeGrip.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbResizeGrip_MouseMove);
            this.pbResizeGrip.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbResizeGrip_MouseUp);
            // 
            // saveWorker
            // 
            this.saveWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SavePos_DoWork);
            // 
            // toolTip
            // 
            this.toolTip.Active = false;
            this.toolTip.AutomaticDelay = 100;
            this.toolTip.AutoPopDelay = 1000;
            this.toolTip.InitialDelay = 200;
            this.toolTip.ReshowDelay = 20;
            // 
            // FrmNote
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnHideNote;
            resources.ApplyResources(this, "$this");
            this.ContextMenuStrip = this.menuFrmNoteOptions;
            this.ControlBox = false;
            this.Controls.Add(this.pnlNote);
            this.Controls.Add(this.pnlHead);
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmNote";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.Deactivate += new System.EventHandler(this.FrmNote_Deactivate);
            this.Activated += new System.EventHandler(this.FrmNote_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmNote_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmNote_FormClosing);
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.menuFrmNoteOptions.ResumeLayout(false);
            this.pnlNote.ResumeLayout(false);
            this.pnlNote.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResizeGrip)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}