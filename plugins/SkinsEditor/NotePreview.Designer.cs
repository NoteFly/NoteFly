namespace SkinsEditor
{
    partial class NoteSkinPreview
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbxPreviewNote = new System.Windows.Forms.GroupBox();
            this.pnlPreviewNoteWindow = new System.Windows.Forms.Panel();
            this.pnlPreviewNoteHead = new System.Windows.Forms.Panel();
            this.btnPreviewNoteBtnClose = new System.Windows.Forms.Button();
            this.lblPreviewNoteTitle = new System.Windows.Forms.Label();
            this.pnlPreviewNoteContent = new System.Windows.Forms.Panel();
            this.lblPreviewNoteContent = new System.Windows.Forms.Label();
            this.picboxPreviewNoteResizegrid = new System.Windows.Forms.PictureBox();
            this.gbxPreviewNote.SuspendLayout();
            this.pnlPreviewNoteWindow.SuspendLayout();
            this.pnlPreviewNoteHead.SuspendLayout();
            this.pnlPreviewNoteContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picboxPreviewNoteResizegrid)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxPreviewNote
            // 
            this.gbxPreviewNote.Controls.Add(this.pnlPreviewNoteWindow);
            this.gbxPreviewNote.Location = new System.Drawing.Point(3, 3);
            this.gbxPreviewNote.Name = "gbxPreviewNote";
            this.gbxPreviewNote.Size = new System.Drawing.Size(206, 189);
            this.gbxPreviewNote.TabIndex = 59;
            this.gbxPreviewNote.TabStop = false;
            this.gbxPreviewNote.Text = "preview";
            this.gbxPreviewNote.Visible = false;
            // 
            // pnlPreviewNoteWindow
            // 
            this.pnlPreviewNoteWindow.BackColor = System.Drawing.Color.Transparent;
            this.pnlPreviewNoteWindow.Controls.Add(this.pnlPreviewNoteHead);
            this.pnlPreviewNoteWindow.Controls.Add(this.pnlPreviewNoteContent);
            this.pnlPreviewNoteWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPreviewNoteWindow.Location = new System.Drawing.Point(3, 16);
            this.pnlPreviewNoteWindow.Name = "pnlPreviewNoteWindow";
            this.pnlPreviewNoteWindow.Size = new System.Drawing.Size(200, 170);
            this.pnlPreviewNoteWindow.TabIndex = 2;
            // 
            // pnlPreviewNoteHead
            // 
            this.pnlPreviewNoteHead.BackColor = System.Drawing.Color.Transparent;
            this.pnlPreviewNoteHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPreviewNoteHead.Controls.Add(this.btnPreviewNoteBtnClose);
            this.pnlPreviewNoteHead.Controls.Add(this.lblPreviewNoteTitle);
            this.pnlPreviewNoteHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPreviewNoteHead.Location = new System.Drawing.Point(0, 0);
            this.pnlPreviewNoteHead.Margin = new System.Windows.Forms.Padding(0);
            this.pnlPreviewNoteHead.Name = "pnlPreviewNoteHead";
            this.pnlPreviewNoteHead.Size = new System.Drawing.Size(200, 31);
            this.pnlPreviewNoteHead.TabIndex = 1;
            this.pnlPreviewNoteHead.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlPreviewNoteHead_MouseDown);
            this.pnlPreviewNoteHead.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlPreviewNoteHead_MouseUp);
            // 
            // btnPreviewNoteBtnClose
            // 
            this.btnPreviewNoteBtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPreviewNoteBtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreviewNoteBtnClose.Location = new System.Drawing.Point(164, 3);
            this.btnPreviewNoteBtnClose.Name = "btnPreviewNoteBtnClose";
            this.btnPreviewNoteBtnClose.Size = new System.Drawing.Size(32, 24);
            this.btnPreviewNoteBtnClose.TabIndex = 1;
            this.btnPreviewNoteBtnClose.Text = "X";
            this.btnPreviewNoteBtnClose.UseVisualStyleBackColor = true;
            // 
            // lblPreviewNoteTitle
            // 
            this.lblPreviewNoteTitle.AutoSize = true;
            this.lblPreviewNoteTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreviewNoteTitle.Location = new System.Drawing.Point(3, 5);
            this.lblPreviewNoteTitle.Name = "lblPreviewNoteTitle";
            this.lblPreviewNoteTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.lblPreviewNoteTitle.Size = new System.Drawing.Size(67, 18);
            this.lblPreviewNoteTitle.TabIndex = 0;
            this.lblPreviewNoteTitle.Text = "example";
            // 
            // pnlPreviewNoteContent
            // 
            this.pnlPreviewNoteContent.BackColor = System.Drawing.Color.Transparent;
            this.pnlPreviewNoteContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPreviewNoteContent.Controls.Add(this.lblPreviewNoteContent);
            this.pnlPreviewNoteContent.Controls.Add(this.picboxPreviewNoteResizegrid);
            this.pnlPreviewNoteContent.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPreviewNoteContent.Location = new System.Drawing.Point(0, 30);
            this.pnlPreviewNoteContent.Name = "pnlPreviewNoteContent";
            this.pnlPreviewNoteContent.Size = new System.Drawing.Size(200, 140);
            this.pnlPreviewNoteContent.TabIndex = 0;
            // 
            // lblPreviewNoteContent
            // 
            this.lblPreviewNoteContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPreviewNoteContent.Location = new System.Drawing.Point(6, 9);
            this.lblPreviewNoteContent.Name = "lblPreviewNoteContent";
            this.lblPreviewNoteContent.Size = new System.Drawing.Size(182, 118);
            this.lblPreviewNoteContent.TabIndex = 61;
            this.lblPreviewNoteContent.Text = "Test test test test test test test test test test test  test test test test test " +
    "test test test test test \r\n";
            // 
            // picboxPreviewNoteResizegrid
            // 
            this.picboxPreviewNoteResizegrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.picboxPreviewNoteResizegrid.BackColor = System.Drawing.Color.Transparent;
            this.picboxPreviewNoteResizegrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picboxPreviewNoteResizegrid.Image = global::SkinsEditor.Properties.Resources.hoekje;
            this.picboxPreviewNoteResizegrid.Location = new System.Drawing.Point(185, 123);
            this.picboxPreviewNoteResizegrid.Margin = new System.Windows.Forms.Padding(0);
            this.picboxPreviewNoteResizegrid.Name = "picboxPreviewNoteResizegrid";
            this.picboxPreviewNoteResizegrid.Size = new System.Drawing.Size(17, 18);
            this.picboxPreviewNoteResizegrid.TabIndex = 60;
            this.picboxPreviewNoteResizegrid.TabStop = false;
            // 
            // NoteSkinPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxPreviewNote);
            this.Name = "NoteSkinPreview";
            this.Size = new System.Drawing.Size(212, 197);
            this.gbxPreviewNote.ResumeLayout(false);
            this.pnlPreviewNoteWindow.ResumeLayout(false);
            this.pnlPreviewNoteHead.ResumeLayout(false);
            this.pnlPreviewNoteHead.PerformLayout();
            this.pnlPreviewNoteContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picboxPreviewNoteResizegrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxPreviewNote;
        private System.Windows.Forms.Panel pnlPreviewNoteWindow;
        private System.Windows.Forms.Panel pnlPreviewNoteHead;
        private System.Windows.Forms.Button btnPreviewNoteBtnClose;
        private System.Windows.Forms.Label lblPreviewNoteTitle;
        private System.Windows.Forms.Panel pnlPreviewNoteContent;
        private System.Windows.Forms.Label lblPreviewNoteContent;
        private System.Windows.Forms.PictureBox picboxPreviewNoteResizegrid;

    }
}
