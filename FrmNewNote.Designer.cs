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
            this.contextMenuStripTextActions.Size = new System.Drawing.Size(181, 180);
            this.contextMenuStripTextActions.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripTextActions_Opening);
            // 
            // menuSaveNewNote
            // 
            this.menuSaveNewNote.Image = global::NoteFly.Properties.Resources.accept;
            this.menuSaveNewNote.Name = "menuSaveNewNote";
            this.menuSaveNewNote.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuSaveNewNote.Size = new System.Drawing.Size(180, 22);
            this.menuSaveNewNote.Text = "&Save note";
            this.menuSaveNewNote.Click += new System.EventHandler(this.btnAddNote_Click);
            // 
            // menuShowtoolbar
            // 
            this.menuShowtoolbar.Checked = true;
            this.menuShowtoolbar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuShowtoolbar.Name = "menuShowtoolbar";
            this.menuShowtoolbar.Size = new System.Drawing.Size(180, 22);
            this.menuShowtoolbar.Text = "Show format tools";
            this.menuShowtoolbar.Click += new System.EventHandler(this.menuShowtoolbar_Click);
            // 
            // menuWordWarp
            // 
            this.menuWordWarp.Checked = true;
            this.menuWordWarp.CheckOnClick = true;
            this.menuWordWarp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuWordWarp.Name = "menuWordWarp";
            this.menuWordWarp.Size = new System.Drawing.Size(180, 22);
            this.menuWordWarp.Text = "Word wrap";
            this.menuWordWarp.Click += new System.EventHandler(this.menuWordWarp_Click);
            // 
            // menuStickyOnTop
            // 
            this.menuStickyOnTop.CheckOnClick = true;
            this.menuStickyOnTop.Name = "menuStickyOnTop";
            this.menuStickyOnTop.Size = new System.Drawing.Size(180, 22);
            this.menuStickyOnTop.Text = "Sticky on &top";
            this.menuStickyOnTop.Click += new System.EventHandler(this.menuStickyOnTop_Click);
            // 
            // menuCopy
            // 
            this.menuCopy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuCopyContent,
            this.menuCopyTitle});
            this.menuCopy.Name = "menuCopy";
            this.menuCopy.Size = new System.Drawing.Size(180, 22);
            this.menuCopy.Text = "&Copy";
            this.menuCopy.DropDownOpening += new System.EventHandler(this.copyTextToolStripMenuItem_DropDownOpening);
            this.menuCopy.Click += new System.EventHandler(this.copyTextToolStripMenuItem_Click);
            // 
            // menuCopyContent
            // 
            this.menuCopyContent.Name = "menuCopyContent";
            this.menuCopyContent.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.C)));
            this.menuCopyContent.Size = new System.Drawing.Size(189, 22);
            this.menuCopyContent.Text = "content";
            // 
            // menuCopyTitle
            // 
            this.menuCopyTitle.Name = "menuCopyTitle";
            this.menuCopyTitle.Size = new System.Drawing.Size(189, 22);
            this.menuCopyTitle.Text = "title";
            // 
            // menuPasteTo
            // 
            this.menuPasteTo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentToolStripMenuItem,
            this.titleToolStripMenuItem});
            this.menuPasteTo.Name = "menuPasteTo";
            this.menuPasteTo.Size = new System.Drawing.Size(180, 22);
            this.menuPasteTo.Text = "&Paste to";
            // 
            // contentToolStripMenuItem
            // 
            this.contentToolStripMenuItem.Name = "contentToolStripMenuItem";
            this.contentToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.contentToolStripMenuItem.Text = "content";
            this.contentToolStripMenuItem.Click += new System.EventHandler(this.pastTextToolStripMenuItem_Click);
            // 
            // titleToolStripMenuItem
            // 
            this.titleToolStripMenuItem.Name = "titleToolStripMenuItem";
            this.titleToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.titleToolStripMenuItem.Text = "title";
            this.titleToolStripMenuItem.Click += new System.EventHandler(this.titleToolStripMenuItem_Click);
            // 
            // menuImportfile
            // 
            this.menuImportfile.Name = "menuImportfile";
            this.menuImportfile.Size = new System.Drawing.Size(180, 22);
            this.menuImportfile.Text = "Import..";
            this.menuImportfile.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // menuCancelNewNote
            // 
            this.menuCancelNewNote.Image = global::NoteFly.Properties.Resources.cancel;
            this.menuCancelNewNote.Name = "menuCancelNewNote";
            this.menuCancelNewNote.ShortcutKeyDisplayString = "Escape";
            this.menuCancelNewNote.Size = new System.Drawing.Size(180, 22);
            this.menuCancelNewNote.Text = "Canc&el note";
            this.menuCancelNewNote.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tbTitle
            // 
            this.tbTitle.AccessibleDescription = "input title note";
            this.tbTitle.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.tbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTitle.BackColor = System.Drawing.Color.Khaki;
            this.tbTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbTitle.CausesValidation = false;
            this.tbTitle.ContextMenuStrip = this.contextMenuStripTextActions;
            this.tbTitle.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTitle.ImeMode = System.Windows.Forms.ImeMode.On;
            this.tbTitle.Location = new System.Drawing.Point(38, 6);
            this.tbTitle.MaxLength = 255;
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(176, 23);
            this.tbTitle.TabIndex = 0;
            this.tbTitle.WordWrap = false;
            this.tbTitle.Leave += new System.EventHandler(this.tbTitle_Leave);
            this.tbTitle.Enter += new System.EventHandler(this.tbTitle_Enter);
            // 
            // lbTextTitle
            // 
            this.lbTextTitle.AutoSize = true;
            this.lbTextTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTextTitle.Location = new System.Drawing.Point(0, 9);
            this.lbTextTitle.Name = "lbTextTitle";
            this.lbTextTitle.Size = new System.Drawing.Size(34, 20);
            this.lbTextTitle.TabIndex = 9;
            this.lbTextTitle.Text = "Title:";
            this.lbTextTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            this.pnlHeadNewNote.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeadNewNote.Location = new System.Drawing.Point(0, 0);
            this.pnlHeadNewNote.Name = "pnlHeadNewNote";
            this.pnlHeadNewNote.Size = new System.Drawing.Size(284, 40);
            this.pnlHeadNewNote.TabIndex = 4;
            this.pnlHeadNewNote.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHeadNewNote_MouseMove);
            this.pnlHeadNewNote.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHeadNewNote_MouseDown);
            this.pnlHeadNewNote.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlHeadNewNote_MouseUp);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BackgroundImage = global::NoteFly.Properties.Resources.cancel;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.CausesValidation = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(253, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(26, 24);
            this.btnCancel.TabIndex = 3;
            this.toolTip.SetToolTip(this.btnCancel, "cancel new note (escape)");
            this.btnCancel.UseMnemonic = false;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAddNote
            // 
            this.btnAddNote.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddNote.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnAddNote.BackColor = System.Drawing.Color.Transparent;
            this.btnAddNote.BackgroundImage = global::NoteFly.Properties.Resources.accept;
            this.btnAddNote.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddNote.CausesValidation = false;
            this.btnAddNote.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btnAddNote.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnAddNote.FlatAppearance.BorderSize = 0;
            this.btnAddNote.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnAddNote.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnAddNote.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNote.Location = new System.Drawing.Point(220, 6);
            this.btnAddNote.Name = "btnAddNote";
            this.btnAddNote.Size = new System.Drawing.Size(27, 24);
            this.btnAddNote.TabIndex = 2;
            this.toolTip.SetToolTip(this.btnAddNote, "accept new note (Ctrl+S)");
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
            this.btnFontBigger.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFontBigger.CausesValidation = false;
            this.btnFontBigger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFontBigger.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnFontBigger.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.btnFontBigger.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnFontBigger.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnFontBigger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFontBigger.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFontBigger.Location = new System.Drawing.Point(163, 3);
            this.btnFontBigger.Name = "btnFontBigger";
            this.btnFontBigger.Size = new System.Drawing.Size(26, 24);
            this.btnFontBigger.TabIndex = 17;
            this.btnFontBigger.TabStop = false;
            this.btnFontBigger.Text = "A";
            this.toolTip.SetToolTip(this.btnFontBigger, "Bigger text (Ctrl+shift+>)");
            this.btnFontBigger.UseCompatibleTextRendering = true;
            this.btnFontBigger.UseMnemonic = false;
            this.btnFontBigger.UseVisualStyleBackColor = false;
            this.btnFontBigger.Click += new System.EventHandler(this.btnFontBigger_Click);
            // 
            // btnFontSmaller
            // 
            this.btnFontSmaller.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFontSmaller.BackColor = System.Drawing.Color.Transparent;
            this.btnFontSmaller.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFontSmaller.CausesValidation = false;
            this.btnFontSmaller.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnFontSmaller.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnFontSmaller.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.btnFontSmaller.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnFontSmaller.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnFontSmaller.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFontSmaller.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFontSmaller.Location = new System.Drawing.Point(195, 3);
            this.btnFontSmaller.Name = "btnFontSmaller";
            this.btnFontSmaller.Size = new System.Drawing.Size(26, 24);
            this.btnFontSmaller.TabIndex = 16;
            this.btnFontSmaller.TabStop = false;
            this.btnFontSmaller.Text = "A";
            this.toolTip.SetToolTip(this.btnFontSmaller, "Smaller text (Ctrl+shift+<)");
            this.btnFontSmaller.UseCompatibleTextRendering = true;
            this.btnFontSmaller.UseMnemonic = false;
            this.btnFontSmaller.UseVisualStyleBackColor = false;
            this.btnFontSmaller.Click += new System.EventHandler(this.btnFontSmaller_Click);
            // 
            // btnTextBulletlist
            // 
            this.btnTextBulletlist.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTextBulletlist.BackColor = System.Drawing.Color.Transparent;
            this.btnTextBulletlist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTextBulletlist.CausesValidation = false;
            this.btnTextBulletlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTextBulletlist.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnTextBulletlist.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.btnTextBulletlist.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnTextBulletlist.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnTextBulletlist.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTextBulletlist.Font = new System.Drawing.Font("Microsoft Sans Serif", 3.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTextBulletlist.Location = new System.Drawing.Point(131, 3);
            this.btnTextBulletlist.Name = "btnTextBulletlist";
            this.btnTextBulletlist.Size = new System.Drawing.Size(26, 24);
            this.btnTextBulletlist.TabIndex = 18;
            this.btnTextBulletlist.TabStop = false;
            this.btnTextBulletlist.Text = "• aaa\r\n• bbb";
            this.btnTextBulletlist.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.toolTip.SetToolTip(this.btnTextBulletlist, "Bullit list (Ctrl+shift+L)");
            this.btnTextBulletlist.UseCompatibleTextRendering = true;
            this.btnTextBulletlist.UseMnemonic = false;
            this.btnTextBulletlist.UseVisualStyleBackColor = false;
            this.btnTextBulletlist.Click += new System.EventHandler(this.btnTextBulletlist_Click);
            // 
            // btnTextStriketrough
            // 
            this.btnTextStriketrough.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTextStriketrough.BackColor = System.Drawing.Color.Transparent;
            this.btnTextStriketrough.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTextStriketrough.CausesValidation = false;
            this.btnTextStriketrough.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTextStriketrough.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnTextStriketrough.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.btnTextStriketrough.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnTextStriketrough.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnTextStriketrough.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTextStriketrough.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTextStriketrough.Location = new System.Drawing.Point(99, 3);
            this.btnTextStriketrough.Name = "btnTextStriketrough";
            this.btnTextStriketrough.Size = new System.Drawing.Size(26, 24);
            this.btnTextStriketrough.TabIndex = 12;
            this.btnTextStriketrough.TabStop = false;
            this.btnTextStriketrough.Text = "S";
            this.toolTip.SetToolTip(this.btnTextStriketrough, "Striketrough text (Ctrl+T)");
            this.btnTextStriketrough.UseCompatibleTextRendering = true;
            this.btnTextStriketrough.UseMnemonic = false;
            this.btnTextStriketrough.UseVisualStyleBackColor = false;
            this.btnTextStriketrough.Click += new System.EventHandler(this.btnTextStriketrough_Click);
            // 
            // btnTextUnderline
            // 
            this.btnTextUnderline.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTextUnderline.BackColor = System.Drawing.Color.Transparent;
            this.btnTextUnderline.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTextUnderline.CausesValidation = false;
            this.btnTextUnderline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTextUnderline.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnTextUnderline.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.btnTextUnderline.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnTextUnderline.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnTextUnderline.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTextUnderline.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTextUnderline.Location = new System.Drawing.Point(67, 3);
            this.btnTextUnderline.Name = "btnTextUnderline";
            this.btnTextUnderline.Size = new System.Drawing.Size(26, 24);
            this.btnTextUnderline.TabIndex = 14;
            this.btnTextUnderline.TabStop = false;
            this.btnTextUnderline.Text = "U";
            this.toolTip.SetToolTip(this.btnTextUnderline, "Underline text (Ctrl+U)");
            this.btnTextUnderline.UseCompatibleTextRendering = true;
            this.btnTextUnderline.UseMnemonic = false;
            this.btnTextUnderline.UseVisualStyleBackColor = false;
            this.btnTextUnderline.Click += new System.EventHandler(this.btnTextUnderline_Click);
            // 
            // btnTextItalic
            // 
            this.btnTextItalic.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTextItalic.BackColor = System.Drawing.Color.Transparent;
            this.btnTextItalic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTextItalic.CausesValidation = false;
            this.btnTextItalic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTextItalic.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnTextItalic.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.btnTextItalic.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnTextItalic.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnTextItalic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTextItalic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTextItalic.Location = new System.Drawing.Point(35, 3);
            this.btnTextItalic.Name = "btnTextItalic";
            this.btnTextItalic.Size = new System.Drawing.Size(26, 24);
            this.btnTextItalic.TabIndex = 11;
            this.btnTextItalic.TabStop = false;
            this.btnTextItalic.Text = "i";
            this.toolTip.SetToolTip(this.btnTextItalic, "Italic text (Ctrl+I)");
            this.btnTextItalic.UseCompatibleTextRendering = true;
            this.btnTextItalic.UseMnemonic = false;
            this.btnTextItalic.UseVisualStyleBackColor = false;
            this.btnTextItalic.Click += new System.EventHandler(this.btnTextItalic_Click);
            // 
            // btnTextBold
            // 
            this.btnTextBold.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTextBold.BackColor = System.Drawing.Color.Transparent;
            this.btnTextBold.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTextBold.CausesValidation = false;
            this.btnTextBold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTextBold.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnTextBold.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.btnTextBold.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Yellow;
            this.btnTextBold.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnTextBold.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTextBold.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTextBold.Location = new System.Drawing.Point(3, 3);
            this.btnTextBold.Name = "btnTextBold";
            this.btnTextBold.Size = new System.Drawing.Size(26, 24);
            this.btnTextBold.TabIndex = 10;
            this.btnTextBold.TabStop = false;
            this.btnTextBold.Text = "B";
            this.toolTip.SetToolTip(this.btnTextBold, "Bold text (Ctrl+B)");
            this.btnTextBold.UseCompatibleTextRendering = true;
            this.btnTextBold.UseMnemonic = false;
            this.btnTextBold.UseVisualStyleBackColor = false;
            this.btnTextBold.Click += new System.EventHandler(this.btnTextBold_Click);
            // 
            // rtbNewNote
            // 
            this.rtbNewNote.AcceptsTab = true;
            this.rtbNewNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbNewNote.BackColor = System.Drawing.Color.Khaki;
            this.rtbNewNote.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbNewNote.ContextMenuStrip = this.contextMenuStripTextActions;
            this.rtbNewNote.DetectUrls = false;
            this.rtbNewNote.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbNewNote.ImeMode = System.Windows.Forms.ImeMode.On;
            this.rtbNewNote.Location = new System.Drawing.Point(2, 41);
            this.rtbNewNote.MaxLength = 999999;
            this.rtbNewNote.Name = "rtbNewNote";
            this.rtbNewNote.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtbNewNote.Size = new System.Drawing.Size(280, 166);
            this.rtbNewNote.TabIndex = 1;
            this.rtbNewNote.Text = "";
            this.rtbNewNote.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbNote_LinkClicked);
            this.rtbNewNote.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rtbNote_MouseClick);
            this.rtbNewNote.Enter += new System.EventHandler(this.rtbNote_Enter);
            this.rtbNewNote.Leave += new System.EventHandler(this.rtbNote_Leave);
            // 
            // pbResizeGrip
            // 
            this.pbResizeGrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbResizeGrip.BackColor = System.Drawing.Color.Transparent;
            this.pbResizeGrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbResizeGrip.Image = global::NoteFly.Properties.Resources.hoekje;
            this.pbResizeGrip.Location = new System.Drawing.Point(267, 222);
            this.pbResizeGrip.Margin = new System.Windows.Forms.Padding(0);
            this.pbResizeGrip.Name = "pbResizeGrip";
            this.pbResizeGrip.Size = new System.Drawing.Size(16, 16);
            this.pbResizeGrip.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbResizeGrip.TabIndex = 15;
            this.pbResizeGrip.TabStop = false;
            this.pbResizeGrip.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbResizeGrip_MouseMove);
            // 
            // openNoteFileDialog
            // 
            this.openNoteFileDialog.Filter = "Plain text file (*.txt)|*.txt|PNotes note(*.pnote)|*.pnote|KeyNote NF note (*.knt" +
                ")|*.knt|TomBoy note(*.note)|*.note|MicroSE Note(*.not)|*.not";
            this.openNoteFileDialog.Title = "import single (note)file";
            // 
            // tlpnlFormatbtn
            // 
            this.tlpnlFormatbtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpnlFormatbtn.AutoSize = true;
            this.tlpnlFormatbtn.BackColor = System.Drawing.Color.Transparent;
            this.tlpnlFormatbtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tlpnlFormatbtn.ColumnCount = 8;
            this.tlpnlFormatbtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpnlFormatbtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpnlFormatbtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpnlFormatbtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpnlFormatbtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpnlFormatbtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpnlFormatbtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpnlFormatbtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpnlFormatbtn.Controls.Add(this.btnTextBold, 0, 0);
            this.tlpnlFormatbtn.Controls.Add(this.btnTextItalic, 1, 0);
            this.tlpnlFormatbtn.Controls.Add(this.btnTextUnderline, 2, 0);
            this.tlpnlFormatbtn.Controls.Add(this.btnTextStriketrough, 3, 0);
            this.tlpnlFormatbtn.Controls.Add(this.btnTextBulletlist, 4, 0);
            this.tlpnlFormatbtn.Controls.Add(this.btnFontSmaller, 6, 0);
            this.tlpnlFormatbtn.Controls.Add(this.btnFontBigger, 5, 0);
            this.tlpnlFormatbtn.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
            this.tlpnlFormatbtn.Location = new System.Drawing.Point(2, 208);
            this.tlpnlFormatbtn.Margin = new System.Windows.Forms.Padding(0);
            this.tlpnlFormatbtn.Name = "tlpnlFormatbtn";
            this.tlpnlFormatbtn.RowCount = 1;
            this.tlpnlFormatbtn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpnlFormatbtn.Size = new System.Drawing.Size(278, 30);
            this.tlpnlFormatbtn.TabIndex = 19;
            // 
            // FrmNewNote
            // 
            this.AcceptButton = this.btnAddNote;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.Gold;
            this.CancelButton = this.btnCancel;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(284, 239);
            this.ContextMenuStrip = this.contextMenuStripTextActions;
            this.ControlBox = false;
            this.Controls.Add(this.pbResizeGrip);
            this.Controls.Add(this.rtbNewNote);
            this.Controls.Add(this.tlpnlFormatbtn);
            this.Controls.Add(this.pnlHeadNewNote);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(143, 90);
            this.Name = "FrmNewNote";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
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
