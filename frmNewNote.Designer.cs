﻿namespace SimplePlainNote
{
    partial class frmNewNote
    {
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
            if (disposing && (components != null))
            {
                components.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewNote));
            this.rtbNote = new System.Windows.Forms.RichTextBox();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.lbTextTitle = new System.Windows.Forms.Label();
            this.btnAddNote = new System.Windows.Forms.Button();
            this.Trayicon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createANewNoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlHeadNewNote = new System.Windows.Forms.Panel();
            this.pnlNoteEdit = new System.Windows.Forms.Panel();
            this.contextMenuStrip1.SuspendLayout();
            this.pnlHeadNewNote.SuspendLayout();
            this.pnlNoteEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbNote
            // 
            this.rtbNote.AutoWordSelection = true;
            this.rtbNote.BackColor = System.Drawing.Color.Gold;
            this.rtbNote.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbNote.CausesValidation = false;
            this.rtbNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbNote.EnableAutoDragDrop = true;
            this.rtbNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbNote.ImeMode = System.Windows.Forms.ImeMode.On;
            this.rtbNote.Location = new System.Drawing.Point(0, 0);
            this.rtbNote.MaxLength = 999999;
            this.rtbNote.Name = "rtbNote";
            this.rtbNote.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbNote.Size = new System.Drawing.Size(266, 190);
            this.rtbNote.TabIndex = 1;
            this.rtbNote.Text = "";
            this.rtbNote.Enter += new System.EventHandler(this.rtbNote_Enter);
            // 
            // tbTitle
            // 
            this.tbTitle.AcceptsTab = true;
            this.tbTitle.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.tbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTitle.AutoCompleteCustomSource.AddRange(new string[] {
            "TODO: ",
            "FIX: ",
            "HACK: ",
            "Go to ",
            "Meeting"});
            this.tbTitle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbTitle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbTitle.BackColor = System.Drawing.Color.LightYellow;
            this.tbTitle.CausesValidation = false;
            this.tbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTitle.ImeMode = System.Windows.Forms.ImeMode.On;
            this.tbTitle.Location = new System.Drawing.Point(43, 3);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(189, 22);
            this.tbTitle.TabIndex = 0;
            this.tbTitle.WordWrap = false;
            this.tbTitle.Leave += new System.EventHandler(this.tbTitle_Leave);
            this.tbTitle.Enter += new System.EventHandler(this.tbTitle_Enter);
            // 
            // lbTextTitle
            // 
            this.lbTextTitle.AutoSize = true;
            this.lbTextTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTextTitle.Location = new System.Drawing.Point(6, 3);
            this.lbTextTitle.Name = "lbTextTitle";
            this.lbTextTitle.Size = new System.Drawing.Size(31, 16);
            this.lbTextTitle.TabIndex = 2;
            this.lbTextTitle.Text = "title:";
            this.lbTextTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnAddNote
            // 
            this.btnAddNote.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddNote.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAddNote.BackgroundImage = global::SimplePlainNote.Properties.Resources.accept;
            this.btnAddNote.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddNote.CausesValidation = false;
            this.btnAddNote.Location = new System.Drawing.Point(236, 1);
            this.btnAddNote.Name = "btnAddNote";
            this.btnAddNote.Size = new System.Drawing.Size(27, 23);
            this.btnAddNote.TabIndex = 2;
            this.btnAddNote.UseVisualStyleBackColor = true;
            this.btnAddNote.Click += new System.EventHandler(this.btnAddNote_Click);
            // 
            // Trayicon
            // 
            this.Trayicon.ContextMenuStrip = this.contextMenuStrip1;
            this.Trayicon.Icon = ((System.Drawing.Icon)(resources.GetObject("Trayicon.Icon")));
            this.Trayicon.Text = "Simple Plain Notes";
            this.Trayicon.Visible = true;
            this.Trayicon.Click += new System.EventHandler(this.Trayicon_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createANewNoteToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(230, 48);
            // 
            // createANewNoteToolStripMenuItem
            // 
            this.createANewNoteToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.createANewNoteToolStripMenuItem.Name = "createANewNoteToolStripMenuItem";
            this.createANewNoteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.N)));
            this.createANewNoteToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.createANewNoteToolStripMenuItem.Text = "Create a new note";
            this.createANewNoteToolStripMenuItem.Click += new System.EventHandler(this.createANewNoteToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.exitToolStripMenuItem.Text = "Close simple plain notes";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // pnlHeadNewNote
            // 
            this.pnlHeadNewNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHeadNewNote.Controls.Add(this.lbTextTitle);
            this.pnlHeadNewNote.Controls.Add(this.tbTitle);
            this.pnlHeadNewNote.Controls.Add(this.btnAddNote);
            this.pnlHeadNewNote.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeadNewNote.Location = new System.Drawing.Point(0, 0);
            this.pnlHeadNewNote.Name = "pnlHeadNewNote";
            this.pnlHeadNewNote.Size = new System.Drawing.Size(268, 29);
            this.pnlHeadNewNote.TabIndex = 4;
            // 
            // pnlNoteEdit
            // 
            this.pnlNoteEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNoteEdit.Controls.Add(this.rtbNote);
            this.pnlNoteEdit.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlNoteEdit.Location = new System.Drawing.Point(0, 31);
            this.pnlNoteEdit.Name = "pnlNoteEdit";
            this.pnlNoteEdit.Size = new System.Drawing.Size(268, 192);
            this.pnlNoteEdit.TabIndex = 5;
            // 
            // frmNewNote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gold;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(268, 223);
            this.Controls.Add(this.pnlNoteEdit);
            this.Controls.Add(this.pnlHeadNewNote);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(50, 50);
            this.Name = "frmNewNote";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "New note";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.contextMenuStrip1.ResumeLayout(false);
            this.pnlHeadNewNote.ResumeLayout(false);
            this.pnlHeadNewNote.PerformLayout();
            this.pnlNoteEdit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbNote;
        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.Label lbTextTitle;
        private System.Windows.Forms.Button btnAddNote;
        private System.Windows.Forms.NotifyIcon Trayicon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createANewNoteToolStripMenuItem;
        private System.Windows.Forms.Panel pnlHeadNewNote;
        private System.Windows.Forms.Panel pnlNoteEdit;
    }
}

