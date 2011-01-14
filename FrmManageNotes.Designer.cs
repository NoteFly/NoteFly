//-----------------------------------------------------------------------
// <copyright file="FrmManageNotes.Designer.cs" company="GNU">
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
    /// Manage note window
    /// </summary>
    public partial class FrmManageNotes
    {
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.PictureBox pbResizeGrip;
        private System.Windows.Forms.Timer timerUpdateNotesList;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManageNotes));
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlHead = new System.Windows.Forms.Panel();
            this.lbTextWindowTitle = new System.Windows.Forms.Label();
            this.pbResizeGrip = new System.Windows.Forms.PictureBox();
            this.timerUpdateNotesList = new System.Windows.Forms.Timer(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coltitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colvisible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colskin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button4 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnNoteDelete = new System.Windows.Forms.Button();
            this.btnBackAllNotes = new System.Windows.Forms.Button();
            this.pnlHead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResizeGrip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.AccessibleName = "close";
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.BackColor = System.Drawing.Color.DarkOrange;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(415, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(32, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "X";
            this.btnClose.UseCompatibleTextRendering = true;
            this.btnClose.UseMnemonic = false;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlHead
            // 
            this.pnlHead.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHead.BackColor = System.Drawing.Color.Orange;
            this.pnlHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHead.Controls.Add(this.lbTextWindowTitle);
            this.pnlHead.Controls.Add(this.btnClose);
            this.pnlHead.Location = new System.Drawing.Point(0, 0);
            this.pnlHead.Name = "pnlHead";
            this.pnlHead.Size = new System.Drawing.Size(451, 30);
            this.pnlHead.TabIndex = 8;
            this.pnlHead.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseMove);
            this.pnlHead.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseDown);
            this.pnlHead.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseUp);
            // 
            // lbTextWindowTitle
            // 
            this.lbTextWindowTitle.AutoSize = true;
            this.lbTextWindowTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTextWindowTitle.Location = new System.Drawing.Point(8, 5);
            this.lbTextWindowTitle.Name = "lbTextWindowTitle";
            this.lbTextWindowTitle.Size = new System.Drawing.Size(102, 18);
            this.lbTextWindowTitle.TabIndex = 1;
            this.lbTextWindowTitle.Text = "Manage notes";
            // 
            // pbResizeGrip
            // 
            this.pbResizeGrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbResizeGrip.BackColor = System.Drawing.Color.Transparent;
            this.pbResizeGrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbResizeGrip.Image = global::NoteFly.Properties.Resources.hoekje;
            this.pbResizeGrip.Location = new System.Drawing.Point(435, 306);
            this.pbResizeGrip.Margin = new System.Windows.Forms.Padding(0);
            this.pbResizeGrip.Name = "pbResizeGrip";
            this.pbResizeGrip.Size = new System.Drawing.Size(16, 16);
            this.pbResizeGrip.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbResizeGrip.TabIndex = 9;
            this.pbResizeGrip.TabStop = false;
            this.pbResizeGrip.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbResizeGrip_MouseMove);
            // 
            // timerUpdateNotesList
            // 
            this.timerUpdateNotesList.Interval = 1000;
            this.timerUpdateNotesList.Tick += new System.EventHandler(this.timerUpdateNotesList_Tick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Wheat;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colid,
            this.coltitle,
            this.colvisible,
            this.colskin});
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.Black;
            this.dataGridView1.Location = new System.Drawing.Point(12, 65);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(427, 245);
            this.dataGridView1.TabIndex = 11;
            // 
            // colid
            // 
            this.colid.HeaderText = "nr";
            this.colid.Name = "colid";
            this.colid.Width = 25;
            // 
            // coltitle
            // 
            this.coltitle.HeaderText = "title";
            this.coltitle.Name = "coltitle";
            this.coltitle.Width = 200;
            // 
            // colvisible
            // 
            this.colvisible.HeaderText = "visible";
            this.colvisible.Name = "colvisible";
            this.colvisible.Width = 50;
            // 
            // colskin
            // 
            this.colskin.HeaderText = "skin";
            this.colskin.Name = "colskin";
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Wheat;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(222, 36);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(109, 23);
            this.button4.TabIndex = 15;
            this.button4.Text = "restore all from file";
            this.button4.UseCompatibleTextRendering = true;
            this.button4.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Wheat;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(12, 36);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "show selected";
            this.button1.UseCompatibleTextRendering = true;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // btnNoteDelete
            // 
            this.btnNoteDelete.BackColor = System.Drawing.Color.Wheat;
            this.btnNoteDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNoteDelete.Location = new System.Drawing.Point(123, 36);
            this.btnNoteDelete.Name = "btnNoteDelete";
            this.btnNoteDelete.Size = new System.Drawing.Size(93, 23);
            this.btnNoteDelete.TabIndex = 17;
            this.btnNoteDelete.Text = "delete selected";
            this.btnNoteDelete.UseCompatibleTextRendering = true;
            this.btnNoteDelete.UseVisualStyleBackColor = false;
            this.btnNoteDelete.Click += new System.EventHandler(this.btnNoteDelete_Click);
            // 
            // btnBackAllNotes
            // 
            this.btnBackAllNotes.BackColor = System.Drawing.Color.Wheat;
            this.btnBackAllNotes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackAllNotes.Location = new System.Drawing.Point(334, 36);
            this.btnBackAllNotes.Name = "btnBackAllNotes";
            this.btnBackAllNotes.Size = new System.Drawing.Size(105, 23);
            this.btnBackAllNotes.TabIndex = 18;
            this.btnBackAllNotes.Text = "backup all to file";
            this.btnBackAllNotes.UseCompatibleTextRendering = true;
            this.btnBackAllNotes.UseVisualStyleBackColor = false;
            this.btnBackAllNotes.Click += new System.EventHandler(this.btnBackAllNotes_Click);
            // 
            // FrmManageNotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Orange;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(451, 322);
            this.Controls.Add(this.btnBackAllNotes);
            this.Controls.Add(this.btnNoteDelete);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.pbResizeGrip);
            this.Controls.Add(this.pnlHead);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(280, 60);
            this.Name = "FrmManageNotes";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage notes";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.Deactivate += new System.EventHandler(this.frmManageNotes_Deactivate);
            this.Activated += new System.EventHandler(this.frmManageNotes_Activated);
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResizeGrip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnNoteDelete;
        private System.Windows.Forms.Button btnBackAllNotes;
        private System.Windows.Forms.Label lbTextWindowTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colid;
        private System.Windows.Forms.DataGridViewTextBoxColumn coltitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colvisible;
        private System.Windows.Forms.DataGridViewTextBoxColumn colskin;
    }
}