//-----------------------------------------------------------------------
// <copyright file="FrmNewNote.Designer.cs" company="NoteFly">
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
    /// Creating a new note window.
    /// </summary>
    public partial class FrmNewNote
    {
        /// <summary>
        /// TextBox tbTitle
        /// </summary>
        private System.Windows.Forms.TextBox tbTitle;

        /// <summary>
        /// Label lbTextTitle
        /// </summary>
        private System.Windows.Forms.Label lbTextTitle;

        /// <summary>
        /// Button btnAddNote
        /// </summary>
        private System.Windows.Forms.Button btnAddNote;

        /// <summary>
        /// Panel pnlHeadNewNote
        /// </summary>
        private System.Windows.Forms.Panel pnlHeadNewNote;

        /// <summary>
        /// Button btnCancel
        /// </summary>
        private System.Windows.Forms.Button btnCancel;

        /// <summary>
        /// ContextMenuStrip contextMenuStripTextActions
        /// </summary>
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTextActions;

        /// <summary>
        /// ToolStripMenuItem menuCopyContent
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem menuCopy;

        /// <summary>
        /// ToolStripMenuItem menuSaveNewNote
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem menuSaveNewNote;

        /// <summary>
        /// ToolStripMenuItem menuCancelNewNote
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem menuCancelNewNote;

        /// <summary>
        /// Tooltip toolTip
        /// </summary>
        private System.Windows.Forms.ToolTip toolTip;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// ToolStripMenuItem menuStickyOnTop
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem menuStickyOnTop;

        /// <summary>
        /// RichTextBox rtbNewNote
        /// </summary>
        private System.Windows.Forms.RichTextBox rtbNewNote;

        /// <summary>
        /// ToolStripMenuItem importToolStripMenuItem
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem menuImportfile;

        /// <summary>
        /// PictureBox pbResizeGrip
        /// </summary>
        private System.Windows.Forms.PictureBox pbResizeGrip;

        /// <summary>
        /// ToolStripMenuItem menuShowtoolbar
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem menuShowtoolbar;

        /// <summary>
        /// ToolStripMenuItem menuWordWarp
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem menuWordWarp;

        /// <summary>
        /// Button btnTextBold
        /// </summary>
        private System.Windows.Forms.Button btnTextBold;

        /// <summary>
        /// Button btnTextItalic
        /// </summary>
        private System.Windows.Forms.Button btnTextItalic;

        /// <summary>
        /// Button btnTextStriketrough
        /// </summary>
        private System.Windows.Forms.Button btnTextStriketrough;

        /// <summary>
        /// Button btnTextUnderline
        /// </summary>
        private System.Windows.Forms.Button btnTextUnderline;

        /// <summary>
        /// Button btnFontSmaller
        /// </summary>
        private System.Windows.Forms.Button btnFontSmaller;

        /// <summary>
        /// Button btnFontBigger
        /// </summary>
        private System.Windows.Forms.Button btnFontBigger;

        /// <summary>
        /// Button btnTextBulletlist
        /// </summary>
        private System.Windows.Forms.Button btnTextBulletlist;

        /// <summary>
        /// OpenFileDialog openNoteFileDialog
        /// </summary>
        private System.Windows.Forms.OpenFileDialog openNoteFileDialog;

        /// <summary>
        /// TableLayoutPanel tableLayoutPanelFormatbtn
        /// </summary>
        private System.Windows.Forms.TableLayoutPanel tlpnlFormatbtn;

        /// <summary>
        /// ToolStripMenuItem menuCopyContent
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem menuCopyContent;
        
        /// <summary>
        /// ToolStripMenuItem menuCopyTitle
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem menuCopyTitle;
        
        /// <summary>
        /// ToolStripMenuItem menuPasteTo
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem menuPasteTo;
        
        /// <summary>
        /// ToolStripMenuItem contentToolStripMenuItem
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem contentToolStripMenuItem;
        
        /// <summary>
        /// ToolStripMenuItem titleToolStripMenuItem
        /// </summary>
        private System.Windows.Forms.ToolStripMenuItem titleToolStripMenuItem;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNewNote));
            this.contextMenuStripTextActions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuSaveNewNote = new System.Windows.Forms.ToolStripMenuItem();
            this.menuShowtoolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuWordWarp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStickyOnTop = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopyContent = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopyTitle = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPasteTo = new System.Windows.Forms.ToolStripMenuItem();
            this.contentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.titleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuImportfile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCancelNewNote = new System.Windows.Forms.ToolStripMenuItem();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.lbTextTitle = new System.Windows.Forms.Label();
            this.pnlHeadNewNote = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddNote = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnFontBigger = new System.Windows.Forms.Button();
            this.btnFontSmaller = new System.Windows.Forms.Button();
            this.btnTextBulletlist = new System.Windows.Forms.Button();
            this.btnTextStriketrough = new System.Windows.Forms.Button();
            this.btnTextUnderline = new System.Windows.Forms.Button();
            this.btnTextItalic = new System.Windows.Forms.Button();
            this.btnTextBold = new System.Windows.Forms.Button();
            this.rtbNewNote = new System.Windows.Forms.RichTextBox();
            this.pbResizeGrip = new System.Windows.Forms.PictureBox();
            this.openNoteFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tlpnlFormatbtn = new System.Windows.Forms.TableLayoutPanel();
            this.contextMenuStripTextActions.SuspendLayout();
            this.pnlHeadNewNote.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResizeGrip)).BeginInit();
            this.tlpnlFormatbtn.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStripTextActions
            // 
            this.contextMenuStripTextActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSaveNewNote,
            this.menuShowtoolbar,
            this.menuWordWarp,
            this.menuStickyOnTop,
            this.menuCopy,
            this.menuPasteTo,
            this.menuImportfile,
            this.menuCancelNewNote});
            this.contextMenuStripTextActions.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStripTextActions, "contextMenuStripTextActions");
            this.contextMenuStripTextActions.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripTextActions_Opening);
            // 
            // menuSaveNewNote
            // 
            this.menuSaveNewNote.Image = global::NoteFly.Properties.Resources.accept;
            this.menuSaveNewNote.Name = "menuSaveNewNote";
            resources.ApplyResources(this.menuSaveNewNote, "menuSaveNewNote");
            this.menuSaveNewNote.Click += new System.EventHandler(this.btnAddNote_Click);
            // 
            // menuShowtoolbar
            // 
            this.menuShowtoolbar.Checked = true;
            this.menuShowtoolbar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuShowtoolbar.Name = "menuShowtoolbar";
            resources.ApplyResources(this.menuShowtoolbar, "menuShowtoolbar");
            this.menuShowtoolbar.Click += new System.EventHandler(this.menuShowtoolbar_Click);
            // 
            // menuWordWarp
            // 
            this.menuWordWarp.Checked = true;
            this.menuWordWarp.CheckOnClick = true;
            this.menuWordWarp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuWordWarp.Name = "menuWordWarp";
            resources.ApplyResources(this.menuWordWarp, "menuWordWarp");
            this.menuWordWarp.Click += new System.EventHandler(this.menuWordWarp_Click);
            // 
            // menuStickyOnTop
            // 
            this.menuStickyOnTop.CheckOnClick = true;
            this.menuStickyOnTop.Name = "menuStickyOnTop";
            resources.ApplyResources(this.menuStickyOnTop, "menuStickyOnTop");
            this.menuStickyOnTop.Click += new System.EventHandler(this.menuStickyOnTop_Click);
            // 
            // menuCopy
            // 
            this.menuCopy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCopyContent,
            this.menuCopyTitle});
            this.menuCopy.Name = "menuCopy";
            resources.ApplyResources(this.menuCopy, "menuCopy");
            this.menuCopy.DropDownOpening += new System.EventHandler(this.copyTextToolStripMenuItem_DropDownOpening);
            this.menuCopy.Click += new System.EventHandler(this.copyTextToolStripMenuItem_Click);
            // 
            // menuCopyContent
            // 
            this.menuCopyContent.Name = "menuCopyContent";
            resources.ApplyResources(this.menuCopyContent, "menuCopyContent");
            // 
            // menuCopyTitle
            // 
            this.menuCopyTitle.Name = "menuCopyTitle";
            resources.ApplyResources(this.menuCopyTitle, "menuCopyTitle");
            // 
            // menuPasteTo
            // 
            this.menuPasteTo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentToolStripMenuItem,
            this.titleToolStripMenuItem});
            this.menuPasteTo.Name = "menuPasteTo";
            resources.ApplyResources(this.menuPasteTo, "menuPasteTo");
            // 
            // contentToolStripMenuItem
            // 
            this.contentToolStripMenuItem.Name = "contentToolStripMenuItem";
            resources.ApplyResources(this.contentToolStripMenuItem, "contentToolStripMenuItem");
            this.contentToolStripMenuItem.Click += new System.EventHandler(this.pastTextToolStripMenuItem_Click);
            // 
            // titleToolStripMenuItem
            // 
            this.titleToolStripMenuItem.Name = "titleToolStripMenuItem";
            resources.ApplyResources(this.titleToolStripMenuItem, "titleToolStripMenuItem");
            this.titleToolStripMenuItem.Click += new System.EventHandler(this.titleToolStripMenuItem_Click);
            // 
            // menuImportfile
            // 
            this.menuImportfile.Name = "menuImportfile";
            resources.ApplyResources(this.menuImportfile, "menuImportfile");
            this.menuImportfile.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // menuCancelNewNote
            // 
            this.menuCancelNewNote.Image = global::NoteFly.Properties.Resources.cancel;
            this.menuCancelNewNote.Name = "menuCancelNewNote";
            resources.ApplyResources(this.menuCancelNewNote, "menuCancelNewNote");
            this.menuCancelNewNote.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tbTitle
            // 
            resources.ApplyResources(this.tbTitle, "tbTitle");
            this.tbTitle.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.tbTitle.BackColor = System.Drawing.Color.Khaki;
            this.tbTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbTitle.CausesValidation = false;
            this.tbTitle.ContextMenuStrip = this.contextMenuStripTextActions;
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Leave += new System.EventHandler(this.tbTitle_Leave);
            this.tbTitle.Enter += new System.EventHandler(this.tbTitle_Enter);
            // 
            // lbTextTitle
            // 
            resources.ApplyResources(this.lbTextTitle, "lbTextTitle");
            this.lbTextTitle.Name = "lbTextTitle";
            this.lbTextTitle.UseCompatibleTextRendering = true;
            this.lbTextTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHeadNewNote_MouseMove);
            this.lbTextTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHeadNewNote_MouseDown);
            this.lbTextTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlHeadNewNote_MouseUp);
            // 
            // pnlHeadNewNote
            // 
            this.pnlHeadNewNote.BackColor = System.Drawing.Color.Gold;
            this.pnlHeadNewNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHeadNewNote.Controls.Add(this.btnCancel);
            this.pnlHeadNewNote.Controls.Add(this.lbTextTitle);
            this.pnlHeadNewNote.Controls.Add(this.tbTitle);
            this.pnlHeadNewNote.Controls.Add(this.btnAddNote);
            resources.ApplyResources(this.pnlHeadNewNote, "pnlHeadNewNote");
            this.pnlHeadNewNote.Name = "pnlHeadNewNote";
            this.pnlHeadNewNote.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHeadNewNote_MouseMove);
            this.pnlHeadNewNote.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHeadNewNote_MouseDown);
            this.pnlHeadNewNote.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlHeadNewNote_MouseUp);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImage = global::NoteFly.Properties.Resources.cancel;
            this.btnCancel.CausesValidation = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnCancel.Name = "btnCancel";
            this.toolTip.SetToolTip(this.btnCancel, resources.GetString("btnCancel.ToolTip"));
            this.btnCancel.UseMnemonic = false;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAddNote
            // 
            this.btnAddNote.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnAddNote, "btnAddNote");
            this.btnAddNote.BackColor = System.Drawing.Color.Transparent;
            this.btnAddNote.BackgroundImage = global::NoteFly.Properties.Resources.accept;
            this.btnAddNote.CausesValidation = false;
            this.btnAddNote.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnAddNote.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnAddNote.FlatAppearance.BorderSize = 0;
            this.btnAddNote.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnAddNote.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnAddNote.Name = "btnAddNote";
            this.toolTip.SetToolTip(this.btnAddNote, resources.GetString("btnAddNote.ToolTip"));
            this.btnAddNote.UseMnemonic = false;
            this.btnAddNote.UseVisualStyleBackColor = false;
            this.btnAddNote.Click += new System.EventHandler(this.btnAddNote_Click);
            this.btnAddNote.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnAddNote_KeyPress);
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 100;
            // 
            // btnFontBigger
            // 
            this.btnFontBigger.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFontBigger.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnFontBigger, "btnFontBigger");
            this.btnFontBigger.CausesValidation = false;
            this.btnFontBigger.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnFontBigger.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.btnFontBigger.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnFontBigger.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnFontBigger.Name = "btnFontBigger";
            this.btnFontBigger.TabStop = false;
            this.toolTip.SetToolTip(this.btnFontBigger, resources.GetString("btnFontBigger.ToolTip"));
            this.btnFontBigger.UseCompatibleTextRendering = true;
            this.btnFontBigger.UseMnemonic = false;
            this.btnFontBigger.UseVisualStyleBackColor = false;
            this.btnFontBigger.Click += new System.EventHandler(this.btnFontBigger_Click);
            // 
            // btnFontSmaller
            // 
            this.btnFontSmaller.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFontSmaller.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnFontSmaller, "btnFontSmaller");
            this.btnFontSmaller.CausesValidation = false;
            this.btnFontSmaller.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnFontSmaller.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.btnFontSmaller.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnFontSmaller.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnFontSmaller.Name = "btnFontSmaller";
            this.btnFontSmaller.TabStop = false;
            this.toolTip.SetToolTip(this.btnFontSmaller, resources.GetString("btnFontSmaller.ToolTip"));
            this.btnFontSmaller.UseCompatibleTextRendering = true;
            this.btnFontSmaller.UseMnemonic = false;
            this.btnFontSmaller.UseVisualStyleBackColor = false;
            this.btnFontSmaller.Click += new System.EventHandler(this.btnFontSmaller_Click);
            // 
            // btnTextBulletlist
            // 
            this.btnTextBulletlist.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTextBulletlist.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnTextBulletlist, "btnTextBulletlist");
            this.btnTextBulletlist.CausesValidation = false;
            this.btnTextBulletlist.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnTextBulletlist.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.btnTextBulletlist.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnTextBulletlist.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnTextBulletlist.Name = "btnTextBulletlist";
            this.btnTextBulletlist.TabStop = false;
            this.toolTip.SetToolTip(this.btnTextBulletlist, resources.GetString("btnTextBulletlist.ToolTip"));
            this.btnTextBulletlist.UseCompatibleTextRendering = true;
            this.btnTextBulletlist.UseMnemonic = false;
            this.btnTextBulletlist.UseVisualStyleBackColor = false;
            this.btnTextBulletlist.Click += new System.EventHandler(this.btnTextBulletlist_Click);
            // 
            // btnTextStriketrough
            // 
            this.btnTextStriketrough.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTextStriketrough.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnTextStriketrough, "btnTextStriketrough");
            this.btnTextStriketrough.CausesValidation = false;
            this.btnTextStriketrough.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnTextStriketrough.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.btnTextStriketrough.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnTextStriketrough.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnTextStriketrough.Name = "btnTextStriketrough";
            this.btnTextStriketrough.TabStop = false;
            this.toolTip.SetToolTip(this.btnTextStriketrough, resources.GetString("btnTextStriketrough.ToolTip"));
            this.btnTextStriketrough.UseCompatibleTextRendering = true;
            this.btnTextStriketrough.UseMnemonic = false;
            this.btnTextStriketrough.UseVisualStyleBackColor = false;
            this.btnTextStriketrough.Click += new System.EventHandler(this.btnTextStriketrough_Click);
            // 
            // btnTextUnderline
            // 
            this.btnTextUnderline.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTextUnderline.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnTextUnderline, "btnTextUnderline");
            this.btnTextUnderline.CausesValidation = false;
            this.btnTextUnderline.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnTextUnderline.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.btnTextUnderline.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnTextUnderline.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnTextUnderline.Name = "btnTextUnderline";
            this.btnTextUnderline.TabStop = false;
            this.toolTip.SetToolTip(this.btnTextUnderline, resources.GetString("btnTextUnderline.ToolTip"));
            this.btnTextUnderline.UseCompatibleTextRendering = true;
            this.btnTextUnderline.UseMnemonic = false;
            this.btnTextUnderline.UseVisualStyleBackColor = false;
            this.btnTextUnderline.Click += new System.EventHandler(this.btnTextUnderline_Click);
            // 
            // btnTextItalic
            // 
            this.btnTextItalic.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTextItalic.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnTextItalic, "btnTextItalic");
            this.btnTextItalic.CausesValidation = false;
            this.btnTextItalic.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnTextItalic.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.btnTextItalic.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnTextItalic.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnTextItalic.Name = "btnTextItalic";
            this.btnTextItalic.TabStop = false;
            this.toolTip.SetToolTip(this.btnTextItalic, resources.GetString("btnTextItalic.ToolTip"));
            this.btnTextItalic.UseCompatibleTextRendering = true;
            this.btnTextItalic.UseMnemonic = false;
            this.btnTextItalic.UseVisualStyleBackColor = false;
            this.btnTextItalic.Click += new System.EventHandler(this.btnTextItalic_Click);
            // 
            // btnTextBold
            // 
            this.btnTextBold.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTextBold.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnTextBold, "btnTextBold");
            this.btnTextBold.CausesValidation = false;
            this.btnTextBold.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnTextBold.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.btnTextBold.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnTextBold.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnTextBold.Name = "btnTextBold";
            this.btnTextBold.TabStop = false;
            this.toolTip.SetToolTip(this.btnTextBold, resources.GetString("btnTextBold.ToolTip"));
            this.btnTextBold.UseCompatibleTextRendering = true;
            this.btnTextBold.UseMnemonic = false;
            this.btnTextBold.UseVisualStyleBackColor = false;
            this.btnTextBold.Click += new System.EventHandler(this.btnTextBold_Click);
            // 
            // rtbNewNote
            // 
            this.rtbNewNote.AcceptsTab = true;
            resources.ApplyResources(this.rtbNewNote, "rtbNewNote");
            this.rtbNewNote.BackColor = System.Drawing.Color.Khaki;
            this.rtbNewNote.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbNewNote.ContextMenuStrip = this.contextMenuStripTextActions;
            this.rtbNewNote.DetectUrls = false;
            this.rtbNewNote.Name = "rtbNewNote";
            this.rtbNewNote.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbNote_LinkClicked);
            this.rtbNewNote.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rtbNote_MouseClick);
            this.rtbNewNote.Enter += new System.EventHandler(this.rtbNote_Enter);
            this.rtbNewNote.Leave += new System.EventHandler(this.rtbNote_Leave);
            // 
            // pbResizeGrip
            // 
            resources.ApplyResources(this.pbResizeGrip, "pbResizeGrip");
            this.pbResizeGrip.BackColor = System.Drawing.Color.Transparent;
            this.pbResizeGrip.Image = global::NoteFly.Properties.Resources.hoekje;
            this.pbResizeGrip.Name = "pbResizeGrip";
            this.pbResizeGrip.TabStop = false;
            this.pbResizeGrip.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbResizeGrip_MouseMove);
            // 
            // openNoteFileDialog
            // 
            resources.ApplyResources(this.openNoteFileDialog, "openNoteFileDialog");
            // 
            // tlpnlFormatbtn
            // 
            resources.ApplyResources(this.tlpnlFormatbtn, "tlpnlFormatbtn");
            this.tlpnlFormatbtn.BackColor = System.Drawing.Color.Transparent;
            this.tlpnlFormatbtn.Controls.Add(this.btnTextBold, 0, 0);
            this.tlpnlFormatbtn.Controls.Add(this.btnTextItalic, 1, 0);
            this.tlpnlFormatbtn.Controls.Add(this.btnTextUnderline, 2, 0);
            this.tlpnlFormatbtn.Controls.Add(this.btnTextStriketrough, 3, 0);
            this.tlpnlFormatbtn.Controls.Add(this.btnTextBulletlist, 4, 0);
            this.tlpnlFormatbtn.Controls.Add(this.btnFontSmaller, 6, 0);
            this.tlpnlFormatbtn.Controls.Add(this.btnFontBigger, 5, 0);
            this.tlpnlFormatbtn.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
            this.tlpnlFormatbtn.Name = "tlpnlFormatbtn";
            // 
            // FrmNewNote
            // 
            this.AcceptButton = this.btnAddNote;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.Gold;
            this.CancelButton = this.btnCancel;
            this.CausesValidation = false;
            this.ContextMenuStrip = this.contextMenuStripTextActions;
            this.ControlBox = false;
            this.Controls.Add(this.pbResizeGrip);
            this.Controls.Add(this.rtbNewNote);
            this.Controls.Add(this.tlpnlFormatbtn);
            this.Controls.Add(this.pnlHeadNewNote);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmNewNote";
            this.TransparencyKey = System.Drawing.Color.LightPink;
            this.Deactivate += new System.EventHandler(this.frmNewNote_Deactivate);
            this.Activated += new System.EventHandler(this.frmNewNote_Activated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmNewNote_KeyDown);
            this.contextMenuStripTextActions.ResumeLayout(false);
            this.pnlHeadNewNote.ResumeLayout(false);
            this.pnlHeadNewNote.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResizeGrip)).EndInit();
            this.tlpnlFormatbtn.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion               
    }
}
