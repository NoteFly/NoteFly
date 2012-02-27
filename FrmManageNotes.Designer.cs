//-----------------------------------------------------------------------
// <copyright file="FrmManageNotes.Designer.cs" company="NoteFly">
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
    /// Manage note window
    /// </summary>
    public partial class FrmManageNotes
    {
        /// <summary>
        /// The close Button of FrmMangeNotes
        /// </summary>
        private System.Windows.Forms.Button btnClose;

        /// <summary>
        /// An panel the titlebar of the form.
        /// </summary>
        private System.Windows.Forms.Panel pnlHead;

        /// <summary>
        /// An picturebox for resizing form.
        /// </summary>
        private System.Windows.Forms.PictureBox pbResizeGrip;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Button btnRestoreAllNotes
        /// </summary>
        private System.Windows.Forms.Button btnRestoreAllNotes;

        /// <summary>
        /// Button btnShowSelectedNotes
        /// </summary>
        private System.Windows.Forms.Button btnShowSelectedNotes;

        /// <summary>
        /// Button btnNoteDelete
        /// </summary>
        private System.Windows.Forms.Button btnNoteDelete;

        /// <summary>
        /// Button Button
        /// </summary>
        private System.Windows.Forms.Button btnBackAllNotes;

        /// <summary>
        /// Label lbTextWindowTitle
        /// </summary>
        private System.Windows.Forms.Label lbTextWindowTitle;

        /// <summary>
        /// DataGridView dataGridView1
        /// </summary>
        private System.Windows.Forms.DataGridView dataGridViewNotes;

        /// <summary>
        /// Panel pnlContent
        /// </summary>
        private System.Windows.Forms.Panel pnlContent;

        /// <summary>
        /// Tooltip tooltip
        /// </summary>
        private System.Windows.Forms.ToolTip toolTip;

        /// <summary>
        /// SaveFileDialog saveExportFileDialog
        /// </summary>
        private System.Windows.Forms.SaveFileDialog saveExportFileDialog;

        /// <summary>
        /// OpenFileDialog openImportFileDialog
        /// </summary>
        private System.Windows.Forms.OpenFileDialog openImportFileDialog;

        /// <summary>
        /// TableLayoutPanel tableLayoutPanelButtons
        /// </summary>
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelButtons;

        /// <summary>
        /// SearchTextBox searchTextBoxNotes
        /// </summary>
        private SearchTextBox searchTextBoxNotes;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManageNotes));
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlHead = new System.Windows.Forms.Panel();
            this.lbTextWindowTitle = new System.Windows.Forms.Label();
            this.pbResizeGrip = new System.Windows.Forms.PictureBox();
            this.btnRestoreAllNotes = new System.Windows.Forms.Button();
            this.btnShowSelectedNotes = new System.Windows.Forms.Button();
            this.btnNoteDelete = new System.Windows.Forms.Button();
            this.btnBackAllNotes = new System.Windows.Forms.Button();
            this.dataGridViewNotes = new System.Windows.Forms.DataGridView();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.tableLayoutPanelButtons = new System.Windows.Forms.TableLayoutPanel();
            this.searchTextBoxNotes = new NoteFly.SearchTextBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.saveExportFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openImportFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.pnlHead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResizeGrip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNotes)).BeginInit();
            this.pnlContent.SuspendLayout();
            this.tableLayoutPanelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.AccessibleName = "close";
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(564, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(32, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "X";
            this.toolTip.SetToolTip(this.btnClose, "Close");
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
            this.pnlHead.Size = new System.Drawing.Size(600, 30);
            this.pnlHead.TabIndex = 8;
            this.pnlHead.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseMove);
            this.pnlHead.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseDown);
            this.pnlHead.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseUp);
            // 
            // lbTextWindowTitle
            // 
            this.lbTextWindowTitle.AutoSize = true;
            this.lbTextWindowTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lbTextWindowTitle.Location = new System.Drawing.Point(8, 5);
            this.lbTextWindowTitle.Name = "lbTextWindowTitle";
            this.lbTextWindowTitle.Size = new System.Drawing.Size(111, 20);
            this.lbTextWindowTitle.TabIndex = 1;
            this.lbTextWindowTitle.Text = "Manage notes";
            this.lbTextWindowTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseMove);
            this.lbTextWindowTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseDown);
            this.lbTextWindowTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseUp);
            // 
            // pbResizeGrip
            // 
            this.pbResizeGrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbResizeGrip.BackColor = System.Drawing.Color.Transparent;
            this.pbResizeGrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbResizeGrip.Image = global::NoteFly.Properties.Resources.hoekje;
            this.pbResizeGrip.Location = new System.Drawing.Point(583, 353);
            this.pbResizeGrip.Margin = new System.Windows.Forms.Padding(0);
            this.pbResizeGrip.Name = "pbResizeGrip";
            this.pbResizeGrip.Size = new System.Drawing.Size(16, 16);
            this.pbResizeGrip.TabIndex = 9;
            this.pbResizeGrip.TabStop = false;
            this.pbResizeGrip.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbResizeGrip_MouseMove);
            this.pbResizeGrip.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbResizeGrip_MouseUp);
            // 
            // btnRestoreAllNotes
            // 
            this.btnRestoreAllNotes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRestoreAllNotes.BackColor = System.Drawing.Color.Transparent;
            this.btnRestoreAllNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRestoreAllNotes.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnRestoreAllNotes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestoreAllNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnRestoreAllNotes.Location = new System.Drawing.Point(221, 3);
            this.btnRestoreAllNotes.Name = "btnRestoreAllNotes";
            this.btnRestoreAllNotes.Size = new System.Drawing.Size(103, 28);
            this.btnRestoreAllNotes.TabIndex = 15;
            this.btnRestoreAllNotes.Text = "&import";
            this.toolTip.SetToolTip(this.btnRestoreAllNotes, "Restore notes from a backup file");
            this.btnRestoreAllNotes.UseCompatibleTextRendering = true;
            this.btnRestoreAllNotes.UseVisualStyleBackColor = false;
            this.btnRestoreAllNotes.Click += new System.EventHandler(this.btnRestoreAllNotes_Click);
            // 
            // btnShowSelectedNotes
            // 
            this.btnShowSelectedNotes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnShowSelectedNotes.BackColor = System.Drawing.Color.Transparent;
            this.btnShowSelectedNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnShowSelectedNotes.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnShowSelectedNotes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowSelectedNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnShowSelectedNotes.Location = new System.Drawing.Point(3, 3);
            this.btnShowSelectedNotes.Name = "btnShowSelectedNotes";
            this.btnShowSelectedNotes.Size = new System.Drawing.Size(103, 28);
            this.btnShowSelectedNotes.TabIndex = 16;
            this.btnShowSelectedNotes.Text = "&show selected";
            this.toolTip.SetToolTip(this.btnShowSelectedNotes, "Show or hide the selected notes");
            this.btnShowSelectedNotes.UseCompatibleTextRendering = true;
            this.btnShowSelectedNotes.UseVisualStyleBackColor = false;
            this.btnShowSelectedNotes.Click += new System.EventHandler(this.btnShowSelectedNotes_Click);
            // 
            // btnNoteDelete
            // 
            this.btnNoteDelete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnNoteDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnNoteDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNoteDelete.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnNoteDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNoteDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnNoteDelete.Location = new System.Drawing.Point(112, 3);
            this.btnNoteDelete.Name = "btnNoteDelete";
            this.btnNoteDelete.Size = new System.Drawing.Size(103, 28);
            this.btnNoteDelete.TabIndex = 17;
            this.btnNoteDelete.Text = "&delete selected";
            this.toolTip.SetToolTip(this.btnNoteDelete, "Delete the selected notes");
            this.btnNoteDelete.UseCompatibleTextRendering = true;
            this.btnNoteDelete.UseVisualStyleBackColor = false;
            this.btnNoteDelete.Click += new System.EventHandler(this.btnNoteDelete_Click);
            // 
            // btnBackAllNotes
            // 
            this.btnBackAllNotes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBackAllNotes.BackColor = System.Drawing.Color.Transparent;
            this.btnBackAllNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBackAllNotes.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnBackAllNotes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackAllNotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnBackAllNotes.Location = new System.Drawing.Point(330, 3);
            this.btnBackAllNotes.Name = "btnBackAllNotes";
            this.btnBackAllNotes.Size = new System.Drawing.Size(103, 28);
            this.btnBackAllNotes.TabIndex = 18;
            this.btnBackAllNotes.Text = "&export all";
            this.toolTip.SetToolTip(this.btnBackAllNotes, "Backup all notes to a single backup file");
            this.btnBackAllNotes.UseCompatibleTextRendering = true;
            this.btnBackAllNotes.UseVisualStyleBackColor = false;
            this.btnBackAllNotes.Click += new System.EventHandler(this.btnBackAllNotes_Click);
            // 
            // dataGridViewNotes
            // 
            this.dataGridViewNotes.AllowUserToAddRows = false;
            this.dataGridViewNotes.AllowUserToDeleteRows = false;
            this.dataGridViewNotes.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridViewNotes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewNotes.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGridViewNotes.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dataGridViewNotes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridViewNotes.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridViewNotes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewNotes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewNotes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewNotes.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewNotes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.dataGridViewNotes.GridColor = System.Drawing.Color.Silver;
            this.dataGridViewNotes.Location = new System.Drawing.Point(3, 37);
            this.dataGridViewNotes.Name = "dataGridViewNotes";
            this.dataGridViewNotes.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewNotes.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewNotes.RowHeadersVisible = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridViewNotes.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewNotes.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridViewNotes.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridViewNotes.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dataGridViewNotes.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dataGridViewNotes.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridViewNotes.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewNotes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewNotes.Size = new System.Drawing.Size(590, 320);
            this.dataGridViewNotes.TabIndex = 19;
            this.dataGridViewNotes.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridView1_Scroll);
            this.dataGridViewNotes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridViewNotes.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
            this.dataGridViewNotes.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            this.dataGridViewNotes.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewNotes_CellMouseEnter);
            this.dataGridViewNotes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // pnlContent
            // 
            this.pnlContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContent.BackColor = System.Drawing.Color.Transparent;
            this.pnlContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContent.Controls.Add(this.tableLayoutPanelButtons);
            this.pnlContent.Controls.Add(this.dataGridViewNotes);
            this.pnlContent.Controls.Add(this.pbResizeGrip);
            this.pnlContent.Location = new System.Drawing.Point(0, 29);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(600, 371);
            this.pnlContent.TabIndex = 20;
            // 
            // tableLayoutPanelButtons
            // 
            this.tableLayoutPanelButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanelButtons.ColumnCount = 5;
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 163F));
            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelButtons.Controls.Add(this.searchTextBoxNotes, 4, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.btnShowSelectedNotes, 0, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.btnRestoreAllNotes, 2, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.btnNoteDelete, 1, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.btnBackAllNotes, 3, 0);
            this.tableLayoutPanelButtons.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
            this.tableLayoutPanelButtons.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelButtons.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
            this.tableLayoutPanelButtons.RowCount = 1;
            this.tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelButtons.Size = new System.Drawing.Size(599, 34);
            this.tableLayoutPanelButtons.TabIndex = 21;
            // 
            // searchTextBoxNotes
            // 
            this.searchTextBoxNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchTextBoxNotes.Location = new System.Drawing.Point(439, 3);
            this.searchTextBoxNotes.Name = "searchTextBoxNotes";
            this.searchTextBoxNotes.Size = new System.Drawing.Size(157, 28);
            this.searchTextBoxNotes.TabIndex = 22;
            this.searchTextBoxNotes.SearchStart += new NoteFly.SearchTextBox.SearchStartHandler(this.searchTextBoxNotes_SearchStart);
            this.searchTextBoxNotes.SearchStop += new NoteFly.SearchTextBox.SearchStopHandler(this.searchTextBoxNotes_SearchStop);
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 100;
            this.toolTip.AutoPopDelay = 500;
            this.toolTip.InitialDelay = 200;
            this.toolTip.ReshowDelay = 20;
            // 
            // saveExportFileDialog
            // 
            this.saveExportFileDialog.Filter = "NoteFly backup|*.nfbak|Stickies CSV stored notes|*.csv|PNotes full backup|*.pnfb";
            // 
            // openImportFileDialog
            // 
            this.openImportFileDialog.Filter = "NoteFly backup|*.nfbak|Stickies CSV stored notes|*.csv|PNotes full backup|*.pnfb|" +
                "CintaNotes xml export|*.xml";
            // 
            // FrmManageNotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Orange;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.ControlBox = false;
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHead);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(280, 80);
            this.Name = "FrmManageNotes";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Deactivate += new System.EventHandler(this.frmManageNotes_Deactivate);
            this.Activated += new System.EventHandler(this.frmManageNotes_Activated);
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResizeGrip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNotes)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.tableLayoutPanelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}