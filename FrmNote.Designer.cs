//-----------------------------------------------------------------------
// <copyright file="FrmNote.Designer.cs" company="GNU">
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
#define windows //platform can be: windows, linux, macos

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
        private System.Windows.Forms.RichTextBox rtbNote;
        private System.Windows.Forms.Panel pnlNote;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripNoteOptions;
        private System.Windows.Forms.ToolStripMenuItem menuEditNote;
        private System.Windows.Forms.ToolStripMenuItem menuCopyText;
        private System.Windows.Forms.ToolStripMenuItem menuNoteColors;
        private System.Windows.Forms.ToolStripMenuItem yellowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem whiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem greenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blueToolStripMenuItem;
        private System.Windows.Forms.PictureBox pbResizeGrip;
        private System.Windows.Forms.ToolStripMenuItem purpleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuOnTop;
        private System.Windows.Forms.ToolStripMenuItem menuCopyTitle;
        private System.Windows.Forms.ToolStripMenuItem menuLockNote;
        private System.ComponentModel.BackgroundWorker SavePos;
        private System.Windows.Forms.ToolStripMenuItem menuHideNote;
        private System.Windows.Forms.ToolStripMenuItem menuSendTo;
        private System.Windows.Forms.ToolStripMenuItem tsmenuSendToFacebook;
        private System.Windows.Forms.ToolStripMenuItem tsmenuSendToTwitter;
        private System.Windows.Forms.ToolStripMenuItem tsmenuSendToEmail;
        private System.Windows.Forms.ToolStripMenuItem tsmenuSendToTextfile;
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
            this.contextMenuStripNoteOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuEditNote = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNoteColors = new System.Windows.Forms.ToolStripMenuItem();
            this.yellowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.whiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purpleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSendTo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuSendToEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuSendToTwitter = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuSendToFacebook = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuSendToTextfile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopyTitle = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCopyText = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOnTop = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRollUp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLockNote = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHideNote = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCloseNote = new System.Windows.Forms.Button();
            this.rtbNote = new System.Windows.Forms.RichTextBox();
            this.pnlNote = new System.Windows.Forms.Panel();
            this.pbResizeGrip = new System.Windows.Forms.PictureBox();
            this.SavePos = new System.ComponentModel.BackgroundWorker();
            this.pnlHead.SuspendLayout();
            this.contextMenuStripNoteOptions.SuspendLayout();
            this.pnlNote.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResizeGrip)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AccessibleDescription = "Note title";
            this.lblTitle.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(3, 5, 60, 0);
            this.lblTitle.Size = new System.Drawing.Size(238, 30);
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
            this.pnlHead.ContextMenuStrip = this.contextMenuStripNoteOptions;
            this.pnlHead.Controls.Add(this.btnCloseNote);
            this.pnlHead.Controls.Add(this.lblTitle);
            this.pnlHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHead.Location = new System.Drawing.Point(0, 0);
            this.pnlHead.Name = "pnlHead";
            this.pnlHead.Size = new System.Drawing.Size(240, 32);
            this.pnlHead.TabIndex = 1;
            this.pnlHead.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseDown);
            this.pnlHead.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseUp);
            // 
            // contextMenuStripNoteOptions
            // 
            this.contextMenuStripNoteOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEditNote,
            this.menuNoteColors,
            this.menuSendTo,
            this.menuCopyTitle,
            this.menuCopyText,
            this.menuOnTop,
            this.menuRollUp,
            this.menuLockNote,
            this.menuHideNote});
            this.contextMenuStripNoteOptions.Name = "contextMenuStripNoteOptions";
            this.contextMenuStripNoteOptions.Size = new System.Drawing.Size(216, 202);
            this.contextMenuStripNoteOptions.Text = "-=menu=-";
            this.contextMenuStripNoteOptions.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.contextMenuStripNoteOptions_Closed);
            // 
            // menuEditNote
            // 
            this.menuEditNote.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuEditNote.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.menuEditNote.Name = "menuEditNote";
            this.menuEditNote.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.menuEditNote.Size = new System.Drawing.Size(215, 22);
            this.menuEditNote.Text = "&Edit note";
            this.menuEditNote.Click += new System.EventHandler(this.editTToolStripMenuItem_Click);
            // 
            // menuNoteColors
            // 
            this.menuNoteColors.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuNoteColors.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.yellowToolStripMenuItem,
            this.orangeToolStripMenuItem,
            this.whiteToolStripMenuItem,
            this.greenToolStripMenuItem,
            this.blueToolStripMenuItem,
            this.purpleToolStripMenuItem,
            this.redToolStripMenuItem});
            this.menuNoteColors.Name = "menuNoteColors";
            this.menuNoteColors.Size = new System.Drawing.Size(215, 22);
            this.menuNoteColors.Text = "&Color";
            this.menuNoteColors.DropDownOpening += new System.EventHandler(this.updateMenuNoteColor);
            // 
            // yellowToolStripMenuItem
            // 
            this.yellowToolStripMenuItem.BackColor = System.Drawing.Color.Gold;
            this.yellowToolStripMenuItem.Checked = true;
            this.yellowToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.yellowToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.yellowToolStripMenuItem.Name = "yellowToolStripMenuItem";
            this.yellowToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.yellowToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt)
                        | System.Windows.Forms.Keys.D1)));
            this.yellowToolStripMenuItem.ShowShortcutKeys = false;
            this.yellowToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.yellowToolStripMenuItem.Text = "Yellow";
            this.yellowToolStripMenuItem.Click += new System.EventHandler(this.SetColorNote);
            // 
            // orangeToolStripMenuItem
            // 
            this.orangeToolStripMenuItem.BackColor = System.Drawing.Color.Orange;
            this.orangeToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.orangeToolStripMenuItem.Name = "orangeToolStripMenuItem";
            this.orangeToolStripMenuItem.ShowShortcutKeys = false;
            this.orangeToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.orangeToolStripMenuItem.Text = "Orange";
            this.orangeToolStripMenuItem.Click += new System.EventHandler(this.SetColorNote);
            // 
            // whiteToolStripMenuItem
            // 
            this.whiteToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.whiteToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.whiteToolStripMenuItem.Name = "whiteToolStripMenuItem";
            this.whiteToolStripMenuItem.ShowShortcutKeys = false;
            this.whiteToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.whiteToolStripMenuItem.Text = "White";
            this.whiteToolStripMenuItem.Click += new System.EventHandler(this.SetColorNote);
            // 
            // greenToolStripMenuItem
            // 
            this.greenToolStripMenuItem.BackColor = System.Drawing.Color.LawnGreen;
            this.greenToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.greenToolStripMenuItem.Name = "greenToolStripMenuItem";
            this.greenToolStripMenuItem.ShowShortcutKeys = false;
            this.greenToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.greenToolStripMenuItem.Text = "Green";
            this.greenToolStripMenuItem.Click += new System.EventHandler(this.SetColorNote);
            // 
            // blueToolStripMenuItem
            // 
            this.blueToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.blueToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.blueToolStripMenuItem.Name = "blueToolStripMenuItem";
            this.blueToolStripMenuItem.ShowShortcutKeys = false;
            this.blueToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.blueToolStripMenuItem.Text = "Blue";
            this.blueToolStripMenuItem.Click += new System.EventHandler(this.SetColorNote);
            // 
            // purpleToolStripMenuItem
            // 
            this.purpleToolStripMenuItem.BackColor = System.Drawing.Color.Fuchsia;
            this.purpleToolStripMenuItem.Name = "purpleToolStripMenuItem";
            this.purpleToolStripMenuItem.ShowShortcutKeys = false;
            this.purpleToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.purpleToolStripMenuItem.Text = "Purple";
            this.purpleToolStripMenuItem.Click += new System.EventHandler(this.SetColorNote);
            // 
            // redToolStripMenuItem
            // 
            this.redToolStripMenuItem.BackColor = System.Drawing.Color.Red;
            this.redToolStripMenuItem.Name = "redToolStripMenuItem";
            this.redToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.redToolStripMenuItem.Text = "Red";
            this.redToolStripMenuItem.Click += new System.EventHandler(this.SetColorNote);
            // 
            // menuSendTo
            // 
            this.menuSendTo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmenuSendToEmail,
            this.tsmenuSendToTwitter,
            this.tsmenuSendToFacebook,
            this.tsmenuSendToTextfile});
            this.menuSendTo.Name = "menuSendTo";
            this.menuSendTo.Size = new System.Drawing.Size(215, 22);
            this.menuSendTo.Text = "&Send to";
            // 
            // tsmenuSendToEmail
            // 
            this.tsmenuSendToEmail.Name = "tsmenuSendToEmail";
            this.tsmenuSendToEmail.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.tsmenuSendToEmail.Size = new System.Drawing.Size(166, 22);
            this.tsmenuSendToEmail.Text = "E-&mail";
            this.tsmenuSendToEmail.Click += new System.EventHandler(this.emailToolStripMenuItem_Click);
            // 
            // tsmenuSendToTwitter
            // 
            this.tsmenuSendToTwitter.Name = "tsmenuSendToTwitter";
            this.tsmenuSendToTwitter.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.tsmenuSendToTwitter.Size = new System.Drawing.Size(166, 22);
            this.tsmenuSendToTwitter.Text = "&Twitter";
            this.tsmenuSendToTwitter.Click += new System.EventHandler(this.tsmenuSendToTwitter_Click);
            // 
            // tsmenuSendToFacebook
            // 
            this.tsmenuSendToFacebook.Name = "tsmenuSendToFacebook";
            this.tsmenuSendToFacebook.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.tsmenuSendToFacebook.Size = new System.Drawing.Size(166, 22);
            this.tsmenuSendToFacebook.Text = "Face&Book";
            this.tsmenuSendToFacebook.Click += new System.EventHandler(this.tsmenuSendToFacebook_Click);
            // 
            // tsmenuSendToTextfile
            // 
            this.tsmenuSendToTextfile.Name = "tsmenuSendToTextfile";
            this.tsmenuSendToTextfile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.tsmenuSendToTextfile.Size = new System.Drawing.Size(166, 22);
            this.tsmenuSendToTextfile.Text = "Text&file";
            this.tsmenuSendToTextfile.Click += new System.EventHandler(this.tsmenuSendToTextfile_Click);
            // 
            // menuCopyTitle
            // 
            this.menuCopyTitle.Name = "menuCopyTitle";
            this.menuCopyTitle.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.C)));
            this.menuCopyTitle.Size = new System.Drawing.Size(215, 22);
            this.menuCopyTitle.Text = "Copy &title";
            this.menuCopyTitle.Click += new System.EventHandler(this.copyTitleToolStripMenuItem_Click);
            // 
            // menuCopyText
            // 
            this.menuCopyText.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuCopyText.Name = "menuCopyText";
            this.menuCopyText.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.menuCopyText.Size = new System.Drawing.Size(215, 22);
            this.menuCopyText.Text = "Copy note &content";
            this.menuCopyText.Click += new System.EventHandler(this.copyTextToolStripMenuItem_Click);
            // 
            // menuOnTop
            // 
            this.menuOnTop.CheckOnClick = true;
            this.menuOnTop.Name = "menuOnTop";
            this.menuOnTop.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuOnTop.Size = new System.Drawing.Size(215, 22);
            this.menuOnTop.Text = "Sticky &on top";
            this.menuOnTop.Click += new System.EventHandler(this.OnTopToolStripMenuItem_Click);
            // 
            // menuRollUp
            // 
            this.menuRollUp.Name = "menuRollUp";
            this.menuRollUp.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.menuRollUp.Size = new System.Drawing.Size(215, 22);
            this.menuRollUp.Text = "Roll up";
            this.menuRollUp.Click += new System.EventHandler(this.menuRollUp_Click);
            // 
            // menuLockNote
            // 
            this.menuLockNote.CheckOnClick = true;
            this.menuLockNote.Name = "menuLockNote";
            this.menuLockNote.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.menuLockNote.Size = new System.Drawing.Size(215, 22);
            this.menuLockNote.Text = "&Lock note";
            this.menuLockNote.Click += new System.EventHandler(this.locknoteToolStripMenuItem_Click);
            // 
            // menuHideNote
            // 
            this.menuHideNote.Name = "menuHideNote";
            this.menuHideNote.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.menuHideNote.Size = new System.Drawing.Size(215, 22);
            this.menuHideNote.Text = "&Hide note";
            this.menuHideNote.Click += new System.EventHandler(this.hideNoteToolStripMenuItem_Click);
            // 
            // btnCloseNote
            // 
            this.btnCloseNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
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
            this.rtbNote.BackColor = System.Drawing.Color.Gold;
            this.rtbNote.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbNote.CausesValidation = false;
            this.rtbNote.ContextMenuStrip = this.contextMenuStripNoteOptions;
            this.rtbNote.Cursor = System.Windows.Forms.Cursors.Default;
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
            this.pnlNote.BackColor = System.Drawing.Color.Gold;
            this.pnlNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNote.ContextMenuStrip = this.contextMenuStripNoteOptions;
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
            // FrmNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gold;
            this.CancelButton = this.btnCloseNote;
			this.ClientSize = new System.Drawing.Size(240, 240);
            this.ContextMenuStrip = this.contextMenuStripNoteOptions;
            this.ControlBox = false;
            this.Controls.Add(this.pnlNote);
            this.Controls.Add(this.pnlHead);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Black;
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
            this.contextMenuStripNoteOptions.ResumeLayout(false);
            this.pnlNote.ResumeLayout(false);
            this.pnlNote.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResizeGrip)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}