namespace SimplePlainNote
{
    partial class frmManageNotes
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
            this.btnClose = new System.Windows.Forms.Button();
            this.lbTextNotes = new System.Windows.Forms.Label();
            this.lbTextNoteOptions = new System.Windows.Forms.Label();
            this.pnlNotes = new System.Windows.Forms.Panel();
            this.pnlHead = new System.Windows.Forms.Panel();
            this.pnlHead.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.DarkOrange;
            this.btnClose.Location = new System.Drawing.Point(202, 158);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(108, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lbTextNotes
            // 
            this.lbTextNotes.AutoSize = true;
            this.lbTextNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTextNotes.Location = new System.Drawing.Point(13, 10);
            this.lbTextNotes.Name = "lbTextNotes";
            this.lbTextNotes.Size = new System.Drawing.Size(34, 16);
            this.lbTextNotes.TabIndex = 1;
            this.lbTextNotes.Text = "Title";
            // 
            // lbTextNoteOptions
            // 
            this.lbTextNoteOptions.AutoSize = true;
            this.lbTextNoteOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTextNoteOptions.Location = new System.Drawing.Point(196, 10);
            this.lbTextNoteOptions.Name = "lbTextNoteOptions";
            this.lbTextNoteOptions.Size = new System.Drawing.Size(54, 16);
            this.lbTextNoteOptions.TabIndex = 2;
            this.lbTextNoteOptions.Text = "Options";
            // 
            // pnlNotes
            // 
            this.pnlNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlNotes.AutoScroll = true;
            this.pnlNotes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.pnlNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlNotes.Location = new System.Drawing.Point(0, 25);
            this.pnlNotes.Name = "pnlNotes";
            this.pnlNotes.Size = new System.Drawing.Size(320, 127);
            this.pnlNotes.TabIndex = 7;
            // 
            // pnlHead
            // 
            this.pnlHead.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHead.BackColor = System.Drawing.Color.Orange;
            this.pnlHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHead.Controls.Add(this.lbTextNoteOptions);
            this.pnlHead.Controls.Add(this.lbTextNotes);
            this.pnlHead.Location = new System.Drawing.Point(0, 0);
            this.pnlHead.Name = "pnlHead";
            this.pnlHead.Size = new System.Drawing.Size(320, 26);
            this.pnlHead.TabIndex = 8;
            this.pnlHead.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseDown);
            // 
            // frmManageNotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Orange;
            this.ClientSize = new System.Drawing.Size(320, 185);
            this.Controls.Add(this.pnlHead);
            this.Controls.Add(this.pnlNotes);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmManageNotes";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage notes";
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.Deactivate += new System.EventHandler(this.frmManageNotes_Deactivate);
            this.Shown += new System.EventHandler(this.frmManageNotes_Shown);
            this.Activated += new System.EventHandler(this.frmManageNotes_Activated);
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lbTextNotes;
        private System.Windows.Forms.Label lbTextNoteOptions;
        private System.Windows.Forms.Panel pnlNotes;
        private System.Windows.Forms.Panel pnlHead;
    }
}