//-----------------------------------------------------------------------
// <copyright file="FrmNewNote.Designer.cs" company="GNU">
// 
// This program is free software; you can redistribute it and/or modify it
// Free Software Foundation; either version 2, 
// or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// </copyright>
//-----------------------------------------------------------------------
namespace NoteFly
{
    /// <summary>
    /// Creating a new note window.
    /// </summary>
    public partial class FrmNewNote
    {
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.Label lbTextTitle;
        private System.Windows.Forms.Button btnAddNote;
        private System.Windows.Forms.Panel pnlHeadNewNote;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTextActions;
        private System.Windows.Forms.ToolStripMenuItem menuPasteToContent;
        private System.Windows.Forms.ToolStripMenuItem menuCopyContent;
        private System.Windows.Forms.ToolStripMenuItem menuSaveNewNote;
        private System.Windows.Forms.ToolStripMenuItem menuCancelNewNote;
        private System.Windows.Forms.ToolTip toolTip;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNewNote));
            this.contextMenuStripTextActions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuSaveNewNote = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStickyOnTop = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPasteToContent = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopyContent = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCancelNewNote = new System.Windows.Forms.ToolStripMenuItem();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.lbTextTitle = new System.Windows.Forms.Label();
            this.pnlHeadNewNote = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddNote = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.rtbNote = new System.Windows.Forms.RichTextBox();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripTextActions.SuspendLayout();
            this.pnlHeadNewNote.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStripTextActions
            // 
            this.contextMenuStripTextActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSaveNewNote,
            this.menuStickyOnTop,
            this.importToolStripMenuItem,
            this.menuPasteToContent,
            this.menuCopyContent,
            this.menuCancelNewNote});
            this.contextMenuStripTextActions.Name = "contextMenuStrip1";
            this.contextMenuStripTextActions.Size = new System.Drawing.Size(289, 158);
            this.contextMenuStripTextActions.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripTextActions_Opening);
            // 
            // menuSaveNewNote
            // 
            this.menuSaveNewNote.Image = global::NoteFly.Properties.Resources.accept;
            this.menuSaveNewNote.Name = "menuSaveNewNote";
            this.menuSaveNewNote.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuSaveNewNote.Size = new System.Drawing.Size(288, 22);
            this.menuSaveNewNote.Text = "&Save note";
            this.menuSaveNewNote.Click += new System.EventHandler(this.btnAddNote_Click);
            // 
            // menuStickyOnTop
            // 
            this.menuStickyOnTop.CheckOnClick = true;
            this.menuStickyOnTop.Name = "menuStickyOnTop";
            this.menuStickyOnTop.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.menuStickyOnTop.Size = new System.Drawing.Size(288, 22);
            this.menuStickyOnTop.Text = "Sticky on &top";
            this.menuStickyOnTop.Click += new System.EventHandler(this.menuStickyOnTop_Click);
            // 
            // menuPasteToContent
            // 
            this.menuPasteToContent.Name = "menuPasteToContent";
            this.menuPasteToContent.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.P)));
            this.menuPasteToContent.Size = new System.Drawing.Size(288, 22);
            this.menuPasteToContent.Text = "&Paste to note content";
            this.menuPasteToContent.Click += new System.EventHandler(this.pastTextToolStripMenuItem_Click);
            // 
            // menuCopyContent
            // 
            this.menuCopyContent.Name = "menuCopyContent";
            this.menuCopyContent.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.C)));
            this.menuCopyContent.Size = new System.Drawing.Size(288, 22);
            this.menuCopyContent.Text = "&Copy current note content";
            this.menuCopyContent.DropDownOpening += new System.EventHandler(this.copyTextToolStripMenuItem_DropDownOpening);
            this.menuCopyContent.Click += new System.EventHandler(this.copyTextToolStripMenuItem_Click);
            // 
            // menuCancelNewNote
            // 
            this.menuCancelNewNote.Image = global::NoteFly.Properties.Resources.cancel;
            this.menuCancelNewNote.Name = "menuCancelNewNote";
            this.menuCancelNewNote.ShortcutKeyDisplayString = "Escape";
            this.menuCancelNewNote.Size = new System.Drawing.Size(288, 22);
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
            this.tbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTitle.ImeMode = System.Windows.Forms.ImeMode.On;
            this.tbTitle.Location = new System.Drawing.Point(38, 6);
            this.tbTitle.MaxLength = 255;
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(176, 22);
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
            this.lbTextTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHeadNewNote_MouseDown);
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
            this.btnCancel.BackgroundImage = global::NoteFly.Properties.Resources.cancel;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.CausesValidation = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(252, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(27, 23);
            this.btnCancel.TabIndex = 3;
            this.toolTip.SetToolTip(this.btnCancel, "cancel new note (escape)");
            this.btnCancel.UseMnemonic = false;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAddNote
            // 
            this.btnAddNote.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddNote.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnAddNote.BackgroundImage = global::NoteFly.Properties.Resources.accept;
            this.btnAddNote.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddNote.CausesValidation = false;
            this.btnAddNote.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAddNote.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnAddNote.FlatAppearance.BorderSize = 0;
            this.btnAddNote.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnAddNote.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnAddNote.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNote.Location = new System.Drawing.Point(220, 6);
            this.btnAddNote.Name = "btnAddNote";
            this.btnAddNote.Size = new System.Drawing.Size(27, 23);
            this.btnAddNote.TabIndex = 2;
            this.toolTip.SetToolTip(this.btnAddNote, "accept new note (Ctrl+S)");
            this.btnAddNote.UseMnemonic = false;
            this.btnAddNote.UseVisualStyleBackColor = true;
            this.btnAddNote.Click += new System.EventHandler(this.btnAddNote_Click);
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 800;
            this.toolTip.AutoPopDelay = 6000;
            this.toolTip.InitialDelay = 800;
            this.toolTip.ReshowDelay = 100;
            // 
            // button1
            // 
            this.button1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.CausesValidation = false;
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(12, 214);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(27, 23);
            this.button1.TabIndex = 10;
            this.button1.TabStop = false;
            this.button1.Text = "B";
            this.toolTip.SetToolTip(this.button1, "bold");
            this.button1.UseMnemonic = false;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.CausesValidation = false;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button2.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(45, 214);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(26, 23);
            this.button2.TabIndex = 11;
            this.button2.TabStop = false;
            this.button2.Text = "i";
            this.toolTip.SetToolTip(this.button2, "Italic");
            this.button2.UseMnemonic = false;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.button3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.CausesValidation = false;
            this.button3.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button3.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Strikeout, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(109, 215);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(27, 23);
            this.button3.TabIndex = 12;
            this.button3.TabStop = false;
            this.button3.Text = "S";
            this.toolTip.SetToolTip(this.button3, "Striketrough");
            this.button3.UseMnemonic = false;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.button4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.CausesValidation = false;
            this.button4.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button4.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(167, 215);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(31, 24);
            this.button4.TabIndex = 13;
            this.button4.TabStop = false;
            this.button4.Text = "¶";
            this.toolTip.SetToolTip(this.button4, "Striketrough");
            this.button4.UseCompatibleTextRendering = true;
            this.button4.UseMnemonic = false;
            this.button4.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            this.button5.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.button5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button5.BackColor = System.Drawing.Color.Transparent;
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button5.CausesValidation = false;
            this.button5.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button5.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button5.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.button5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.button5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(77, 215);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(26, 23);
            this.button5.TabIndex = 14;
            this.button5.TabStop = false;
            this.button5.Text = "U";
            this.toolTip.SetToolTip(this.button5, "Italic");
            this.button5.UseMnemonic = false;
            this.button5.UseVisualStyleBackColor = false;
            // 
            // rtbNote
            // 
            this.rtbNote.AcceptsTab = true;
            this.rtbNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbNote.BackColor = System.Drawing.Color.Khaki;
            this.rtbNote.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbNote.ContextMenuStrip = this.contextMenuStripTextActions;
            this.rtbNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbNote.ImeMode = System.Windows.Forms.ImeMode.On;
            this.rtbNote.Location = new System.Drawing.Point(8, 46);
            this.rtbNote.MaxLength = 999999;
            this.rtbNote.Name = "rtbNote";
            this.rtbNote.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtbNote.Size = new System.Drawing.Size(264, 162);
            this.rtbNote.TabIndex = 1;
            this.rtbNote.Text = "";
            this.rtbNote.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbNote_LinkClicked);
            this.rtbNote.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rtbNote_MouseClick);
            this.rtbNote.Enter += new System.EventHandler(this.rtbNote_Enter);
            this.rtbNote.Leave += new System.EventHandler(this.rtbNote_Leave);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(288, 22);
            this.importToolStripMenuItem.Text = "Import..";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
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
            this.ClientSize = new System.Drawing.Size(284, 249);
            this.ContextMenuStrip = this.contextMenuStripTextActions;
            this.ControlBox = false;
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rtbNote);
            this.Controls.Add(this.pnlHeadNewNote);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(50, 50);
            this.Name = "FrmNewNote";
            this.Text = "New note";
            this.TransparencyKey = System.Drawing.Color.LightPink;
            this.Deactivate += new System.EventHandler(this.frmNewNote_Deactivate);
            this.Activated += new System.EventHandler(this.frmNewNote_Activated);
            this.contextMenuStripTextActions.ResumeLayout(false);
            this.pnlHeadNewNote.ResumeLayout(false);
            this.pnlHeadNewNote.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem menuStickyOnTop;
        private System.Windows.Forms.RichTextBox rtbNote;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;

    }
}

