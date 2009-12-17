﻿namespace NoteFly
{    
    partial class FrmNewNote
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNewNote));
            this.rtbNote = new System.Windows.Forms.RichTextBox();
            this.contextMenuStripTextActions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pastTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveNewNoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelNoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbTitle = new System.Windows.Forms.TextBox();
            this.lbTextTitle = new System.Windows.Forms.Label();
            this.pnlHeadNewNote = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddNote = new System.Windows.Forms.Button();
            this.pnlNoteEdit = new System.Windows.Forms.Panel();
            this.pbResizeGrip = new System.Windows.Forms.PictureBox();
            this.contextMenuStripTextActions.SuspendLayout();
            this.pnlHeadNewNote.SuspendLayout();
            this.pnlNoteEdit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResizeGrip)).BeginInit();
            this.SuspendLayout();
            // 
            // rtbNote
            // 
            this.rtbNote.AcceptsTab = true;
            this.rtbNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbNote.BackColor = System.Drawing.Color.Gold;
            this.rtbNote.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbNote.ContextMenuStrip = this.contextMenuStripTextActions;
            this.rtbNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbNote.ImeMode = System.Windows.Forms.ImeMode.On;
            this.rtbNote.Location = new System.Drawing.Point(3, 3);
            this.rtbNote.MaxLength = 999999;
            this.rtbNote.Name = "rtbNote";
            this.rtbNote.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtbNote.Size = new System.Drawing.Size(271, 196);
            this.rtbNote.TabIndex = 1;
            this.rtbNote.Text = "";
            this.rtbNote.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbNote_LinkClicked);
            this.rtbNote.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rtbNote_MouseClick);
            this.rtbNote.Enter += new System.EventHandler(this.rtbNote_Enter);
            this.rtbNote.Leave += new System.EventHandler(this.rtbNote_Leave);
            this.rtbNote.TextChanged += new System.EventHandler(this.checksyntax);
            // 
            // contextMenuStripTextActions
            // 
            this.contextMenuStripTextActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveNewNoteToolStripMenuItem,
            this.cancelNoteToolStripMenuItem,
            this.pastTextToolStripMenuItem,
            this.copyTextToolStripMenuItem});
            this.contextMenuStripTextActions.Name = "contextMenuStrip1";
            this.contextMenuStripTextActions.Size = new System.Drawing.Size(289, 114);
            // 
            // pastTextToolStripMenuItem
            // 
            this.pastTextToolStripMenuItem.Name = "pastTextToolStripMenuItem";
            this.pastTextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.P)));
            this.pastTextToolStripMenuItem.Size = new System.Drawing.Size(288, 22);
            this.pastTextToolStripMenuItem.Text = "Past to note content";
            this.pastTextToolStripMenuItem.Click += new System.EventHandler(this.pastTextToolStripMenuItem_Click);
            // 
            // copyTextToolStripMenuItem
            // 
            this.copyTextToolStripMenuItem.Name = "copyTextToolStripMenuItem";
            this.copyTextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.C)));
            this.copyTextToolStripMenuItem.Size = new System.Drawing.Size(288, 22);
            this.copyTextToolStripMenuItem.Text = "Copy current note content";
            this.copyTextToolStripMenuItem.DropDownOpening += new System.EventHandler(this.copyTextToolStripMenuItem_DropDownOpening);
            this.copyTextToolStripMenuItem.Click += new System.EventHandler(this.copyTextToolStripMenuItem_Click);
            // 
            // saveNewNoteToolStripMenuItem
            // 
            this.saveNewNoteToolStripMenuItem.Image = global::NoteFly.Properties.Resources.accept;
            this.saveNewNoteToolStripMenuItem.Name = "saveNewNoteToolStripMenuItem";
            this.saveNewNoteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveNewNoteToolStripMenuItem.Size = new System.Drawing.Size(288, 22);
            this.saveNewNoteToolStripMenuItem.Text = "Save note";
            this.saveNewNoteToolStripMenuItem.Click += new System.EventHandler(this.btnAddNote_Click);
            // 
            // cancelNoteToolStripMenuItem
            // 
            this.cancelNoteToolStripMenuItem.Image = global::NoteFly.Properties.Resources.cancel;
            this.cancelNoteToolStripMenuItem.Name = "cancelNoteToolStripMenuItem";
            this.cancelNoteToolStripMenuItem.ShortcutKeyDisplayString = "Escape";
            this.cancelNoteToolStripMenuItem.Size = new System.Drawing.Size(288, 22);
            this.cancelNoteToolStripMenuItem.Text = "Cancel note";
            this.cancelNoteToolStripMenuItem.Click += new System.EventHandler(this.btnCancel_Click);
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
            this.pnlHeadNewNote.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHeadNewNote_MouseDown);
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
            this.btnAddNote.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnAddNote.FlatAppearance.BorderSize = 0;
            this.btnAddNote.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnAddNote.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnAddNote.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNote.Location = new System.Drawing.Point(219, 5);
            this.btnAddNote.Name = "btnAddNote";
            this.btnAddNote.Size = new System.Drawing.Size(27, 23);
            this.btnAddNote.TabIndex = 2;
            this.btnAddNote.UseMnemonic = false;
            this.btnAddNote.UseVisualStyleBackColor = true;
            this.btnAddNote.Click += new System.EventHandler(this.btnAddNote_Click);
            // 
            // pnlNoteEdit
            // 
            this.pnlNoteEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlNoteEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNoteEdit.Controls.Add(this.rtbNote);
            this.pnlNoteEdit.Controls.Add(this.pbResizeGrip);
            this.pnlNoteEdit.Location = new System.Drawing.Point(0, 38);
            this.pnlNoteEdit.Name = "pnlNoteEdit";
            this.pnlNoteEdit.Size = new System.Drawing.Size(284, 211);
            this.pnlNoteEdit.TabIndex = 0;
            this.pnlNoteEdit.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlNoteEdit_MouseClick);
            // 
            // pbResizeGrip
            // 
            this.pbResizeGrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbResizeGrip.BackColor = System.Drawing.Color.Transparent;
            this.pbResizeGrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbResizeGrip.Image = global::NoteFly.Properties.Resources.hoekje;
            this.pbResizeGrip.Location = new System.Drawing.Point(266, 193);
            this.pbResizeGrip.Margin = new System.Windows.Forms.Padding(0);
            this.pbResizeGrip.Name = "pbResizeGrip";
            this.pbResizeGrip.Size = new System.Drawing.Size(16, 16);
            this.pbResizeGrip.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbResizeGrip.TabIndex = 6;
            this.pbResizeGrip.TabStop = false;
            this.pbResizeGrip.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbResizeGrip_MouseMove);
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
            this.Controls.Add(this.pnlNoteEdit);
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
            this.pnlNoteEdit.ResumeLayout(false);
            this.pnlNoteEdit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResizeGrip)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbTitle;
        private System.Windows.Forms.Label lbTextTitle;
        private System.Windows.Forms.Button btnAddNote;
        private System.Windows.Forms.Panel pnlHeadNewNote;
        private System.Windows.Forms.Panel pnlNoteEdit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RichTextBox rtbNote;
        private System.Windows.Forms.PictureBox pbResizeGrip;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTextActions;
        private System.Windows.Forms.ToolStripMenuItem pastTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveNewNoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelNoteToolStripMenuItem;
    }
}

