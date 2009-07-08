namespace SimplePlainNote
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
            this.Trayicon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ContextMenuTrayicon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createANewNoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlHeadNewNote = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAddNote = new System.Windows.Forms.Button();
            this.pnlNoteEdit = new System.Windows.Forms.Panel();
            this.pbResizeGrip = new System.Windows.Forms.PictureBox();
            this.ContextMenuTrayicon.SuspendLayout();
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
            this.rtbNote.AutoWordSelection = true;
            this.rtbNote.BackColor = System.Drawing.Color.Gold;
            this.rtbNote.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbNote.CausesValidation = false;
            this.rtbNote.EnableAutoDragDrop = true;
            this.rtbNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbNote.ImeMode = System.Windows.Forms.ImeMode.On;
            this.rtbNote.Location = new System.Drawing.Point(3, 3);
            this.rtbNote.MaxLength = 999999;
            this.rtbNote.Name = "rtbNote";
            this.rtbNote.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbNote.Size = new System.Drawing.Size(271, 196);
            this.rtbNote.TabIndex = 1;
            this.rtbNote.Text = "";
            this.rtbNote.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbNote_LinkClicked);
            this.rtbNote.Enter += new System.EventHandler(this.rtbNote_Enter);
            this.rtbNote.Leave += new System.EventHandler(this.rtbNote_Leave);
            this.rtbNote.TextChanged += new System.EventHandler(this.rtbNote_Enter);
            // 
            // tbTitle
            // 
            this.tbTitle.AcceptsTab = true;
            this.tbTitle.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.tbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tbTitle.AutoCompleteCustomSource.AddRange(new string[] {
            "TODO: ",
            "FIX: ",
            "HACK: ",
            "Go to ",
            "Meeting"});
            this.tbTitle.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tbTitle.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbTitle.BackColor = System.Drawing.Color.Khaki;
            this.tbTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbTitle.CausesValidation = false;
            this.tbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbTitle.ImeMode = System.Windows.Forms.ImeMode.On;
            this.tbTitle.Location = new System.Drawing.Point(38, 6);
            this.tbTitle.Name = "tbTitle";
            this.tbTitle.Size = new System.Drawing.Size(176, 22);
            this.tbTitle.TabIndex = 0;
            this.tbTitle.WordWrap = false;
            this.tbTitle.TextChanged += new System.EventHandler(this.tbTitle_Enter);
            this.tbTitle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbTitle_KeyDown);
            this.tbTitle.Leave += new System.EventHandler(this.tbTitle_Leave);
            this.tbTitle.Enter += new System.EventHandler(this.tbTitle_Enter);
            // 
            // lbTextTitle
            // 
            this.lbTextTitle.AutoSize = true;
            this.lbTextTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTextTitle.Location = new System.Drawing.Point(0, 9);
            this.lbTextTitle.Name = "lbTextTitle";
            this.lbTextTitle.Size = new System.Drawing.Size(37, 16);
            this.lbTextTitle.TabIndex = 2;
            this.lbTextTitle.Text = "Title:";
            this.lbTextTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Trayicon
            // 
            this.Trayicon.ContextMenuStrip = this.ContextMenuTrayicon;
            this.Trayicon.Icon = ((System.Drawing.Icon)(resources.GetObject("Trayicon.Icon")));
            this.Trayicon.Text = "Simple Plain Notes";
            this.Trayicon.Visible = true;
            this.Trayicon.Click += new System.EventHandler(this.Trayicon_Click);
            // 
            // ContextMenuTrayicon
            // 
            this.ContextMenuTrayicon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createANewNoteToolStripMenuItem,
            this.listToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.ContextMenuTrayicon.Name = "contextMenuStrip1";
            this.ContextMenuTrayicon.ShowImageMargin = false;
            this.ContextMenuTrayicon.Size = new System.Drawing.Size(140, 92);
            // 
            // createANewNoteToolStripMenuItem
            // 
            this.createANewNoteToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.createANewNoteToolStripMenuItem.Name = "createANewNoteToolStripMenuItem";
            this.createANewNoteToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.createANewNoteToolStripMenuItem.Text = "&Create a new note";
            this.createANewNoteToolStripMenuItem.Click += new System.EventHandler(this.createANewNoteToolStripMenuItem_Click);
            // 
            // listToolStripMenuItem
            // 
            this.listToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.listToolStripMenuItem.Name = "listToolStripMenuItem";
            this.listToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.listToolStripMenuItem.Text = "&Manage notes";
            this.listToolStripMenuItem.Click += new System.EventHandler(this.listToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
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
            this.btnCancel.BackgroundImage = global::SimplePlainNote.Properties.Resources.cancel;
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
            this.btnAddNote.BackgroundImage = global::SimplePlainNote.Properties.Resources.accept;
            this.btnAddNote.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddNote.CausesValidation = false;
            this.btnAddNote.DialogResult = System.Windows.Forms.DialogResult.OK;
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
            this.pnlNoteEdit.TabIndex = 5;
            // 
            // pbResizeGrip
            // 
            this.pbResizeGrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbResizeGrip.BackColor = System.Drawing.Color.Transparent;
            this.pbResizeGrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbResizeGrip.Image = global::SimplePlainNote.Properties.Resources.hoekje;
            this.pbResizeGrip.Location = new System.Drawing.Point(266, 193);
            this.pbResizeGrip.Margin = new System.Windows.Forms.Padding(0);
            this.pbResizeGrip.Name = "pbResizeGrip";
            this.pbResizeGrip.Size = new System.Drawing.Size(16, 16);
            this.pbResizeGrip.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbResizeGrip.TabIndex = 6;
            this.pbResizeGrip.TabStop = false;
            this.pbResizeGrip.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbResizeGrip_MouseMove);
            // 
            // frmNewNote
            // 
            this.AcceptButton = this.btnAddNote;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gold;
            this.CancelButton = this.btnCancel;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(284, 249);
            this.Controls.Add(this.pnlNoteEdit);
            this.Controls.Add(this.pnlHeadNewNote);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(50, 50);
            this.Name = "frmNewNote";
            this.ShowInTaskbar = false;
            this.Text = "New note";
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Deactivate += new System.EventHandler(this.frmNewNote_Deactivate);
            this.Shown += new System.EventHandler(this.frmNewNote_Shown);
            this.Activated += new System.EventHandler(this.frmNewNote_Activated);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmNewNote_KeyDown);
            this.ContextMenuTrayicon.ResumeLayout(false);
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
        private System.Windows.Forms.NotifyIcon Trayicon;
        private System.Windows.Forms.ContextMenuStrip ContextMenuTrayicon;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Panel pnlHeadNewNote;
        private System.Windows.Forms.Panel pnlNoteEdit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolStripMenuItem listToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createANewNoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.RichTextBox rtbNote;
        private System.Windows.Forms.PictureBox pbResizeGrip;
    }
}

