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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.btnClose.AccessibleDescription = null;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = null;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Font = null;
            this.btnClose.Name = "btnClose";
            this.toolTip.SetToolTip(this.btnClose, resources.GetString("btnClose.ToolTip"));
            this.btnClose.UseCompatibleTextRendering = true;
            this.btnClose.UseMnemonic = false;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlHead
            // 
            this.pnlHead.AccessibleDescription = null;
            this.pnlHead.AccessibleName = null;
            resources.ApplyResources(this.pnlHead, "pnlHead");
            this.pnlHead.BackColor = System.Drawing.Color.Orange;
            this.pnlHead.BackgroundImage = null;
            this.pnlHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHead.Controls.Add(this.lbTextWindowTitle);
            this.pnlHead.Controls.Add(this.btnClose);
            this.pnlHead.Font = null;
            this.pnlHead.Name = "pnlHead";
            this.toolTip.SetToolTip(this.pnlHead, resources.GetString("pnlHead.ToolTip"));
            this.pnlHead.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseMove);
            this.pnlHead.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseDown);
            this.pnlHead.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseUp);
            // 
            // lbTextWindowTitle
            // 
            this.lbTextWindowTitle.AccessibleDescription = null;
            this.lbTextWindowTitle.AccessibleName = null;
            resources.ApplyResources(this.lbTextWindowTitle, "lbTextWindowTitle");
            this.lbTextWindowTitle.Name = "lbTextWindowTitle";
            this.toolTip.SetToolTip(this.lbTextWindowTitle, resources.GetString("lbTextWindowTitle.ToolTip"));
            this.lbTextWindowTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseMove);
            this.lbTextWindowTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseDown);
            this.lbTextWindowTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseUp);
            // 
            // pbResizeGrip
            // 
            this.pbResizeGrip.AccessibleDescription = null;
            this.pbResizeGrip.AccessibleName = null;
            resources.ApplyResources(this.pbResizeGrip, "pbResizeGrip");
            this.pbResizeGrip.BackColor = System.Drawing.Color.Transparent;
            this.pbResizeGrip.BackgroundImage = null;
            this.pbResizeGrip.Font = null;
            this.pbResizeGrip.Image = global::NoteFly.Properties.Resources.hoekje;
            this.pbResizeGrip.ImageLocation = null;
            this.pbResizeGrip.Name = "pbResizeGrip";
            this.pbResizeGrip.TabStop = false;
            this.toolTip.SetToolTip(this.pbResizeGrip, resources.GetString("pbResizeGrip.ToolTip"));
            this.pbResizeGrip.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbResizeGrip_MouseMove);
            this.pbResizeGrip.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbResizeGrip_MouseUp);
            // 
            // btnRestoreAllNotes
            // 
            this.btnRestoreAllNotes.AccessibleDescription = null;
            this.btnRestoreAllNotes.AccessibleName = null;
            resources.ApplyResources(this.btnRestoreAllNotes, "btnRestoreAllNotes");
            this.btnRestoreAllNotes.BackColor = System.Drawing.Color.Transparent;
            this.btnRestoreAllNotes.BackgroundImage = null;
            this.btnRestoreAllNotes.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnRestoreAllNotes.Name = "btnRestoreAllNotes";
            this.toolTip.SetToolTip(this.btnRestoreAllNotes, resources.GetString("btnRestoreAllNotes.ToolTip"));
            this.btnRestoreAllNotes.UseCompatibleTextRendering = true;
            this.btnRestoreAllNotes.UseVisualStyleBackColor = false;
            this.btnRestoreAllNotes.Click += new System.EventHandler(this.btnRestoreAllNotes_Click);
            // 
            // btnShowSelectedNotes
            // 
            this.btnShowSelectedNotes.AccessibleDescription = null;
            this.btnShowSelectedNotes.AccessibleName = null;
            resources.ApplyResources(this.btnShowSelectedNotes, "btnShowSelectedNotes");
            this.btnShowSelectedNotes.BackColor = System.Drawing.Color.Transparent;
            this.btnShowSelectedNotes.BackgroundImage = null;
            this.btnShowSelectedNotes.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnShowSelectedNotes.Name = "btnShowSelectedNotes";
            this.toolTip.SetToolTip(this.btnShowSelectedNotes, resources.GetString("btnShowSelectedNotes.ToolTip"));
            this.btnShowSelectedNotes.UseCompatibleTextRendering = true;
            this.btnShowSelectedNotes.UseVisualStyleBackColor = false;
            this.btnShowSelectedNotes.Click += new System.EventHandler(this.btnShowSelectedNotes_Click);
            // 
            // btnNoteDelete
            // 
            this.btnNoteDelete.AccessibleDescription = null;
            this.btnNoteDelete.AccessibleName = null;
            resources.ApplyResources(this.btnNoteDelete, "btnNoteDelete");
            this.btnNoteDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnNoteDelete.BackgroundImage = null;
            this.btnNoteDelete.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnNoteDelete.Name = "btnNoteDelete";
            this.toolTip.SetToolTip(this.btnNoteDelete, resources.GetString("btnNoteDelete.ToolTip"));
            this.btnNoteDelete.UseCompatibleTextRendering = true;
            this.btnNoteDelete.UseVisualStyleBackColor = false;
            this.btnNoteDelete.Click += new System.EventHandler(this.btnNoteDelete_Click);
            // 
            // btnBackAllNotes
            // 
            this.btnBackAllNotes.AccessibleDescription = null;
            this.btnBackAllNotes.AccessibleName = null;
            resources.ApplyResources(this.btnBackAllNotes, "btnBackAllNotes");
            this.btnBackAllNotes.BackColor = System.Drawing.Color.Transparent;
            this.btnBackAllNotes.BackgroundImage = null;
            this.btnBackAllNotes.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.btnBackAllNotes.Name = "btnBackAllNotes";
            this.toolTip.SetToolTip(this.btnBackAllNotes, resources.GetString("btnBackAllNotes.ToolTip"));
            this.btnBackAllNotes.UseCompatibleTextRendering = true;
            this.btnBackAllNotes.UseVisualStyleBackColor = false;
            this.btnBackAllNotes.Click += new System.EventHandler(this.btnBackAllNotes_Click);
            // 
            // dataGridViewNotes
            // 
            this.dataGridViewNotes.AccessibleDescription = null;
            this.dataGridViewNotes.AccessibleName = null;
            this.dataGridViewNotes.AllowUserToAddRows = false;
            this.dataGridViewNotes.AllowUserToDeleteRows = false;
            this.dataGridViewNotes.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridViewNotes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.dataGridViewNotes, "dataGridViewNotes");
            this.dataGridViewNotes.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGridViewNotes.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dataGridViewNotes.BackgroundImage = null;
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
            this.dataGridViewNotes.Font = null;
            this.dataGridViewNotes.GridColor = System.Drawing.Color.Silver;
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
            this.dataGridViewNotes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.toolTip.SetToolTip(this.dataGridViewNotes, resources.GetString("dataGridViewNotes.ToolTip"));
            this.dataGridViewNotes.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridView1_Scroll);
            this.dataGridViewNotes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridViewNotes.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
            this.dataGridViewNotes.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            this.dataGridViewNotes.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewNotes_CellMouseEnter);
            this.dataGridViewNotes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // pnlContent
            // 
            this.pnlContent.AccessibleDescription = null;
            this.pnlContent.AccessibleName = null;
            resources.ApplyResources(this.pnlContent, "pnlContent");
            this.pnlContent.BackColor = System.Drawing.Color.Transparent;
            this.pnlContent.BackgroundImage = null;
            this.pnlContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContent.Controls.Add(this.tableLayoutPanelButtons);
            this.pnlContent.Controls.Add(this.dataGridViewNotes);
            this.pnlContent.Controls.Add(this.pbResizeGrip);
            this.pnlContent.Font = null;
            this.pnlContent.Name = "pnlContent";
            this.toolTip.SetToolTip(this.pnlContent, resources.GetString("pnlContent.ToolTip"));
            // 
            // tableLayoutPanelButtons
            // 
            this.tableLayoutPanelButtons.AccessibleDescription = null;
            this.tableLayoutPanelButtons.AccessibleName = null;
            resources.ApplyResources(this.tableLayoutPanelButtons, "tableLayoutPanelButtons");
            this.tableLayoutPanelButtons.BackgroundImage = null;
            this.tableLayoutPanelButtons.Controls.Add(this.searchTextBoxNotes, 4, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.btnShowSelectedNotes, 0, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.btnRestoreAllNotes, 2, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.btnNoteDelete, 1, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.btnBackAllNotes, 3, 0);
            this.tableLayoutPanelButtons.Font = null;
            this.tableLayoutPanelButtons.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
            this.tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
            this.toolTip.SetToolTip(this.tableLayoutPanelButtons, resources.GetString("tableLayoutPanelButtons.ToolTip"));
            // 
            // searchTextBoxNotes
            // 
            this.searchTextBoxNotes.AccessibleDescription = null;
            this.searchTextBoxNotes.AccessibleName = null;
            resources.ApplyResources(this.searchTextBoxNotes, "searchTextBoxNotes");
            this.searchTextBoxNotes.BackgroundImage = null;
            this.searchTextBoxNotes.Font = null;
            this.searchTextBoxNotes.Name = "searchTextBoxNotes";
            this.toolTip.SetToolTip(this.searchTextBoxNotes, resources.GetString("searchTextBoxNotes.ToolTip"));
            this.searchTextBoxNotes.SearchStart += new NoteFly.SearchTextBox.SearchStartHandler(this.searchTextBoxNotes_SearchStart);
            this.searchTextBoxNotes.SearchStop += new NoteFly.SearchTextBox.SearchStopHandler(this.searchTextBoxNotes_SearchStop);
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 100;
            // 
            // saveExportFileDialog
            // 
            resources.ApplyResources(this.saveExportFileDialog, "saveExportFileDialog");
            // 
            // openImportFileDialog
            // 
            resources.ApplyResources(this.openImportFileDialog, "openImportFileDialog");
            // 
            // FrmManageNotes
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Orange;
            this.BackgroundImage = null;
            this.CausesValidation = false;
            this.ControlBox = false;
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHead);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmManageNotes";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.toolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
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

        private SearchTextBox searchTextBoxNotes;
    }
}