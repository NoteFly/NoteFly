namespace SimplePlainNote
{
    partial class FrmNote
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlHead = new System.Windows.Forms.Panel();
            this.contextMenuStripNoteOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNoteColors = new System.Windows.Forms.ToolStripMenuItem();
            this.yellowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.orangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.whiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.blueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purpleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TwitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emailNoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.locknoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pbShowLock = new System.Windows.Forms.PictureBox();
            this.btnCloseNote = new System.Windows.Forms.Button();
            this.rtbNote = new System.Windows.Forms.RichTextBox();
            this.pnlNote = new System.Windows.Forms.Panel();
            this.pbResizeGrip = new System.Windows.Forms.PictureBox();
            this.SavePos = new System.ComponentModel.BackgroundWorker();
            this.pnlHead.SuspendLayout();
            this.contextMenuStripNoteOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbShowLock)).BeginInit();
            this.pnlNote.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResizeGrip)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(3, 4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(18, 19);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "?";
            this.lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseDown);
            // 
            // pnlHead
            // 
            this.pnlHead.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.pnlHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHead.ContextMenuStrip = this.contextMenuStripNoteOptions;
            this.pnlHead.Controls.Add(this.pbShowLock);
            this.pnlHead.Controls.Add(this.btnCloseNote);
            this.pnlHead.Controls.Add(this.lblTitle);
            this.pnlHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHead.Location = new System.Drawing.Point(0, 0);
            this.pnlHead.Name = "pnlHead";
            this.pnlHead.Size = new System.Drawing.Size(240, 32);
            this.pnlHead.TabIndex = 1;
            this.pnlHead.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseDown);
            // 
            // contextMenuStripNoteOptions
            // 
            this.contextMenuStripNoteOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editTToolStripMenuItem,
            this.menuNoteColors,
            this.copyTitleToolStripMenuItem,
            this.copyTextToolStripMenuItem,
            this.TwitterToolStripMenuItem,
            this.emailNoteToolStripMenuItem,
            this.OnTopToolStripMenuItem,
            this.locknoteToolStripMenuItem});
            this.contextMenuStripNoteOptions.Name = "contextMenuStripNoteOptions";
            this.contextMenuStripNoteOptions.Size = new System.Drawing.Size(200, 202);
            this.contextMenuStripNoteOptions.Text = "-=menu=-";
            this.contextMenuStripNoteOptions.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.contextMenuStripNoteOptions_Closed);
            // 
            // editTToolStripMenuItem
            // 
            this.editTToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.editTToolStripMenuItem.Name = "editTToolStripMenuItem";
            this.editTToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.editTToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.editTToolStripMenuItem.Text = "Edit note";
            this.editTToolStripMenuItem.Click += new System.EventHandler(this.editTToolStripMenuItem_Click);
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
            this.menuNoteColors.Size = new System.Drawing.Size(199, 22);
            this.menuNoteColors.Text = "Color";
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
            this.yellowToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.yellowToolStripMenuItem.Text = "Yellow";
            this.yellowToolStripMenuItem.Click += new System.EventHandler(this.setColorNote);
            // 
            // orangeToolStripMenuItem
            // 
            this.orangeToolStripMenuItem.BackColor = System.Drawing.Color.Orange;
            this.orangeToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.orangeToolStripMenuItem.Name = "orangeToolStripMenuItem";
            this.orangeToolStripMenuItem.ShowShortcutKeys = false;
            this.orangeToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.orangeToolStripMenuItem.Text = "Orange";
            this.orangeToolStripMenuItem.Click += new System.EventHandler(this.setColorNote);
            // 
            // whiteToolStripMenuItem
            // 
            this.whiteToolStripMenuItem.BackColor = System.Drawing.Color.White;
            this.whiteToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.whiteToolStripMenuItem.Name = "whiteToolStripMenuItem";
            this.whiteToolStripMenuItem.ShowShortcutKeys = false;
            this.whiteToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.whiteToolStripMenuItem.Text = "White";
            this.whiteToolStripMenuItem.Click += new System.EventHandler(this.setColorNote);
            // 
            // greenToolStripMenuItem
            // 
            this.greenToolStripMenuItem.BackColor = System.Drawing.Color.LawnGreen;
            this.greenToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.greenToolStripMenuItem.Name = "greenToolStripMenuItem";
            this.greenToolStripMenuItem.ShowShortcutKeys = false;
            this.greenToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.greenToolStripMenuItem.Text = "Green";
            this.greenToolStripMenuItem.Click += new System.EventHandler(this.setColorNote);
            // 
            // blueToolStripMenuItem
            // 
            this.blueToolStripMenuItem.BackColor = System.Drawing.Color.CornflowerBlue;
            this.blueToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.blueToolStripMenuItem.Name = "blueToolStripMenuItem";
            this.blueToolStripMenuItem.ShowShortcutKeys = false;
            this.blueToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.blueToolStripMenuItem.Text = "Blue";
            this.blueToolStripMenuItem.Click += new System.EventHandler(this.setColorNote);
            // 
            // purpleToolStripMenuItem
            // 
            this.purpleToolStripMenuItem.BackColor = System.Drawing.Color.Fuchsia;
            this.purpleToolStripMenuItem.Name = "purpleToolStripMenuItem";
            this.purpleToolStripMenuItem.ShowShortcutKeys = false;
            this.purpleToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.purpleToolStripMenuItem.Text = "Purple";
            this.purpleToolStripMenuItem.Click += new System.EventHandler(this.setColorNote);
            // 
            // redToolStripMenuItem
            // 
            this.redToolStripMenuItem.BackColor = System.Drawing.Color.Red;
            this.redToolStripMenuItem.Name = "redToolStripMenuItem";
            this.redToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.redToolStripMenuItem.Text = "Red";
            this.redToolStripMenuItem.Click += new System.EventHandler(this.setColorNote);
            // 
            // copyTitleToolStripMenuItem
            // 
            this.copyTitleToolStripMenuItem.Name = "copyTitleToolStripMenuItem";
            this.copyTitleToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.C)));
            this.copyTitleToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.copyTitleToolStripMenuItem.Text = "Copy title";
            this.copyTitleToolStripMenuItem.Click += new System.EventHandler(this.copyTitleToolStripMenuItem_Click);
            // 
            // copyTextToolStripMenuItem
            // 
            this.copyTextToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.copyTextToolStripMenuItem.Name = "copyTextToolStripMenuItem";
            this.copyTextToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyTextToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.copyTextToolStripMenuItem.Text = "Copy note text";
            this.copyTextToolStripMenuItem.Click += new System.EventHandler(this.copyTextToolStripMenuItem_Click);
            // 
            // TwitterToolStripMenuItem
            // 
            this.TwitterToolStripMenuItem.Enabled = false;
            this.TwitterToolStripMenuItem.Name = "TwitterToolStripMenuItem";
            this.TwitterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.TwitterToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.TwitterToolStripMenuItem.Text = "tweet this note";
            this.TwitterToolStripMenuItem.Click += new System.EventHandler(this.TwitterToolStripMenuItem_Click);
            // 
            // emailNoteToolStripMenuItem
            // 
            this.emailNoteToolStripMenuItem.Name = "emailNoteToolStripMenuItem";
            this.emailNoteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.emailNoteToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.emailNoteToolStripMenuItem.Text = "e-mail note";
            this.emailNoteToolStripMenuItem.Click += new System.EventHandler(this.emailNoteToolStripMenuItem_Click);
            // 
            // OnTopToolStripMenuItem
            // 
            this.OnTopToolStripMenuItem.CheckOnClick = true;
            this.OnTopToolStripMenuItem.Name = "OnTopToolStripMenuItem";
            this.OnTopToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.OnTopToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.OnTopToolStripMenuItem.Text = "note on top";
            this.OnTopToolStripMenuItem.Click += new System.EventHandler(this.OnTopToolStripMenuItem_Click);
            // 
            // locknoteToolStripMenuItem
            // 
            this.locknoteToolStripMenuItem.CheckOnClick = true;
            this.locknoteToolStripMenuItem.Name = "locknoteToolStripMenuItem";
            this.locknoteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.locknoteToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.locknoteToolStripMenuItem.Text = "Lock note";
            this.locknoteToolStripMenuItem.Click += new System.EventHandler(this.locknoteToolStripMenuItem_Click);
            // 
            // pbShowLock
            // 
            this.pbShowLock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbShowLock.Image = global::SimplePlainNote.Properties.Resources.locknote;
            this.pbShowLock.Location = new System.Drawing.Point(180, 8);
            this.pbShowLock.Name = "pbShowLock";
            this.pbShowLock.Size = new System.Drawing.Size(16, 16);
            this.pbShowLock.TabIndex = 2;
            this.pbShowLock.TabStop = false;
            this.pbShowLock.Visible = false;
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
            this.btnCloseNote.Text = "X";
            this.btnCloseNote.UseVisualStyleBackColor = true;
            this.btnCloseNote.Click += new System.EventHandler(this.frmCloseNote_Click);
            // 
            // rtbNote
            // 
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
            this.pbResizeGrip.Image = global::SimplePlainNote.Properties.Resources.hoekje;
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
            // frmNote
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
            this.Name = "frmNote";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.Deactivate += new System.EventHandler(this.frmNote_Deactivate);
            this.Activated += new System.EventHandler(this.frmNote_Activated);
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.contextMenuStripNoteOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbShowLock)).EndInit();
            this.pnlNote.ResumeLayout(false);
            this.pnlNote.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResizeGrip)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Button btnCloseNote;
        private System.Windows.Forms.RichTextBox rtbNote;
        private System.Windows.Forms.Panel pnlNote;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripNoteOptions;
        private System.Windows.Forms.ToolStripMenuItem editTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuNoteColors;
        private System.Windows.Forms.ToolStripMenuItem yellowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem whiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem greenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem blueToolStripMenuItem;
        private System.Windows.Forms.PictureBox pbResizeGrip;
        private System.Windows.Forms.ToolStripMenuItem purpleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TwitterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OnTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyTitleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem locknoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emailNoteToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker SavePos;
        private System.Windows.Forms.PictureBox pbShowLock;
    }
}