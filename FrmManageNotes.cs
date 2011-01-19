//-----------------------------------------------------------------------
// <copyright file="FrmManageNotes.cs" company="GNU">
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
#define windows //platform can be: windows, linux, macos

namespace NoteFly
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Data;

    /// <summary>
    /// Manage notes class
    /// </summary>
    public partial class FrmManageNotes : Form
    {
        #region Fields (4)

        /// <summary>
        /// notes
        /// </summary>
        private Notes notes;
        /// <summary>
        /// Delta point
        /// </summary>
        private Point oldp;
        /// <summary>
        /// flag is redraw is busy
        /// </summary>
        private bool redrawbusy = false;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the FrmManageNotes class.
        /// </summary>
        /// <param name="notes">The class notes, with access to all the notes.</param>
        /// <param name="transparency">Is transparency enabled</param>
        /// <param name="notecolor">The default note color.</param>
        public FrmManageNotes(Notes notes)
        {
            this.InitializeComponent();
            this.notes = notes;
            this.DrawNotesGrid();
            this.SetDataGridViewColumsWidth();
        }

        #endregion Constructors

        #region Methods (13)

        // Private Methods (13) 

        /// <summary>
        /// Request to backup all notes to a file.
        /// Ask where to save then do it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackAllNotes_Click(object sender, EventArgs e)
        {
            SaveFileDialog savebackupdlg = new SaveFileDialog();
            savebackupdlg.CheckPathExists = true;
            savebackupdlg.OverwritePrompt = true;
            savebackupdlg.DefaultExt = "nfbak"; //noteflybackup
            savebackupdlg.Title = "Where to save the backup of all NoteFly notes.";
            savebackupdlg.Filter = "NoteFly notes backup (*.nfbak)|*.nfbak";
            DialogResult savebackupdlgres = savebackupdlg.ShowDialog();
            if (savebackupdlgres == DialogResult.OK)
            {
                xmlUtil.WriteNotesBackupFile(savebackupdlg.FileName, notes);
            }
        }

        /// <summary>
        /// Close form
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// The user pressed the delete button for a note.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnNoteDelete_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nothing selected.");
            }
            else
            {
                if (Settings.ConfirmDeletenote)
                {
                    DialogResult deleteres = MessageBox.Show("Are you sure you want to delete the selected note(s)?", "delete?", MessageBoxButtons.YesNo);
                    if (deleteres == DialogResult.Yes)
                    {
                        this.DeleteNotesSelectedRowsGrid(this.dataGridView1.SelectedRows);
                    }
                }
                else
                {
                    this.DeleteNotesSelectedRowsGrid(this.dataGridView1.SelectedRows);
                }

                if (this.notes.CountNotes > 0)
                {
                    this.btnNoteDelete.Enabled = true;
                }
                else
                {
                    this.btnNoteDelete.Enabled = false;
                }

                this.DrawNotesGrid();
                this.SetDataGridViewColumsWidth();
            }
        }

        /// <summary>
        /// Deletes the notes in memory and files that are selected in a Gridview.
        /// </summary>
        /// <param name="id"></param>
        private void DeleteNotesSelectedRowsGrid(DataGridViewSelectedRowCollection selrows)
        {
            int[] deletedids = new int[selrows.Count];
            for (int r = 0; r < selrows.Count; r++)
            {
                int nr = selrows[r].Index;
                string filename = this.notes.GetNote(nr).Filename;
                try
                {
                    this.notes.GetNote(nr).DestroyForm();
                    string filepath = Path.Combine(Settings.NotesSavepath, filename);
                    File.Delete(filepath);
                    if (Settings.ProgramLogInfo)
                    {
                        Log.Write(LogType.info, "Deleted note: "+filepath);
                    }
                    this.notes.RemoveNote(nr);
                }
                catch (FileNotFoundException filenotfoundexc)
                {
                    throw new CustomException(filenotfoundexc.Message);
                }
                catch (UnauthorizedAccessException)
                {
                    string msgaccessdenied = "Access denied. delete note " + filename + " manually with proper premission.";
                    Log.Write(LogType.error, msgaccessdenied);
                    MessageBox.Show(msgaccessdenied);
                }
            }
        }

        /// <summary>
        /// Draw a list of all notes.
        /// </summary>
        private void DrawNotesGrid()
        {
            DataTable datatable = new DataTable();
            this.dataGridView1.DataSource = datatable;
            datatable.Columns.Add("nr", typeof(String));
            datatable.Columns["nr"].AutoIncrement = true;
            datatable.Columns["nr"].Unique = true;
            datatable.Columns.Add("title", typeof(String));
            datatable.Columns.Add("visible", typeof(Boolean));
            datatable.Columns.Add("skin", typeof(String));
            datatable.DefaultView.AllowEdit = false;
            datatable.DefaultView.AllowNew = false;
            this.dataGridView1.Columns["nr"].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.Columns["visible"].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridView1_RowPostPaint);
            for (int i = 0; i < this.notes.CountNotes; i++)
            {
                DataRow dr = datatable.NewRow();
                dr[0] = i + 1; //enduser counting ;)
                dr[1] = this.notes.GetNote(i).Title;
                dr[2] = this.notes.GetNote(i).Visible;
                dr[3] = notes.GetSkinName(this.notes.GetNote(i).SkinNr);
                datatable.Rows.Add(dr);
            }
        }

        /// <summary>
        /// Color the skin cell with the foreground color of the skin in this cell.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells["skin"].Style.BackColor = notes.GetForegroundColor(notes.GetNote(e.RowIndex).SkinNr); //notes.GetForegroundColor(notes.GetNote(e.RowIndex).SkinNr);
        }

        /// <summary>
        /// FrmManageNotes is activated.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void frmManageNotes_Activated(object sender, EventArgs e)
        {
            if (Settings.NotesTransparencyEnabled)
            {
                try
                {
                    this.Opacity = 1.0;
                    this.Refresh();
                }
                catch (InvalidCastException)
                {
                    throw new CustomException("Transparency level not a integer or double.");
                }
            }
        }

        /// <summary>
        /// form not active, make tranparent if set.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void frmManageNotes_Deactivate(object sender, EventArgs e)
        {
            if (Settings.NotesTransparencyEnabled)
            {
                try
                {
                    this.Opacity = Settings.NotesTransparencyLevel;
                    this.Refresh();
                }
                catch (InvalidCastException)
                {
                    throw new CustomException("Transparency level not a integer or double.");
                }
            }
        }

        /// <summary>
        /// The manage note form is beening resized.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void pbResizeGrip_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Cursor = Cursors.SizeNWSE;
                this.Size = new Size(this.PointToClient(MousePosition).X, this.PointToClient(MousePosition).Y);
            }

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Moving note
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void pnlHead_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.pnlHead.BackColor = Color.OrangeRed;
                this.oldp = e.Location;
            }
        }

        /// <summary>
        /// Move note if pnlHead is being left clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlHead_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.pnlHead.BackColor = Color.OrangeRed;

                int dpx = e.Location.X - oldp.X;
                int dpy = e.Location.Y - oldp.Y;
#if linux
                if (dpx > 8)
                {
                    dpx = 8;
                }
                else if (dpx < -8)
                {
                    dpx = -8;
                }

                if (dpy > 8)
                {
                    dpy = 8;
                }
                else if (dpy < -8)
                {
                    dpy = -8;
                }
#endif
                this.Location = new Point(this.Location.X + dpx, this.Location.Y + dpy);
            }
            else
            {
                this.pnlHead.BackColor = Color.Orange;
            }
        }

        /// <summary>
        /// End moving FrmManageNotes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlHead_MouseUp(object sender, MouseEventArgs e)
        {
            this.pnlHead.BackColor = Color.Orange;
        }

        /// <summary>
        /// Sets every colom of the datagridview to a reasonable width.
        /// </summary>
        private void SetDataGridViewColumsWidth()
        {
            if (this.dataGridView1.Width <= 0) { return; }
            const int colidfixedwidth = 30;
            int partunit = ((this.dataGridView1.Width - colidfixedwidth) / 10);
            this.dataGridView1.Columns["nr"].Width = 1 * colidfixedwidth;
            this.dataGridView1.Columns["title"].Width = 6 * partunit;
            this.dataGridView1.Columns["visible"].Width = 1 * partunit;
            this.dataGridView1.Columns["skin"].Width = 3 * partunit;
        }

        /// <summary>
        /// Request to restore all notes from a backup file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestoreAllNotes_Click(object sender, EventArgs e)
        {
            OpenFileDialog openbackupdlg = new OpenFileDialog();
            openbackupdlg.CheckPathExists = true;
            openbackupdlg.CheckFileExists = true;
            openbackupdlg.Multiselect = false;
            openbackupdlg.DefaultExt = "nfbak"; //noteflybackup
            openbackupdlg.Filter = "NoteFly notes backup (*.nfbak)|*.nfbak";
            openbackupdlg.Title = "Restore all notes";
            DialogResult openbackupdlgres = openbackupdlg.ShowDialog();
            if (openbackupdlgres == DialogResult.OK)
            {
                if (openbackupdlg.FilterIndex == 1)
                {
                    if (this.notes.CountNotes > 0)
                    {
                        DialogResult eraseres = MessageBox.Show("Erase all current notes?", "Are you sure?", MessageBoxButtons.YesNoCancel);
                        if (eraseres == DialogResult.Yes)
                        {
                            Log.Write(LogType.info, "Erased all notes for restoring notes backup.");
                            for (int i = 0; i < this.notes.CountNotes; i++)
                            {
                                this.notes.GetNote(i).DestroyForm();
                                File.Delete(Path.Combine(Settings.NotesSavepath, this.notes.GetNote(i).Filename));
                                this.notes.RemoveNote(i);
                            }
                        }
                        else if (eraseres == DialogResult.Cancel)
                        {
                            Log.Write(LogType.info, "Cancelled restore notes backup.");
                            return;
                        }
                    }
                    Log.Write(LogType.info, "Imported notes backup file: " + openbackupdlg.FileName);
                    xmlUtil.LoadNotesBackup(this.notes, openbackupdlg.FileName);
                    this.notes.LoadNotes(false);
                    this.DrawNotesGrid();
                    this.SetDataGridViewColumsWidth();
                }
            }
        }

        /// <summary>
        /// Toggle visibility selected notes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowSelectedNotes_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedrows = this.dataGridView1.SelectedRows;
            foreach (DataGridViewRow selrow in selectedrows)
            {
                selrow.Cells["visible"].Value = !this.notes.GetNote(selrow.Index).Visible;
                this.notes.GetNote(selrow.Index).Visible = !this.notes.GetNote(selrow.Index).Visible;
                if (this.notes.GetNote(selrow.Index).Visible)
                {
                    this.notes.GetNote(selrow.Index).CreateForm();
                }
                else
                {
                    this.notes.GetNote(selrow.Index).DestroyForm();
                }
            }
        }

        #endregion Methods
    }
}
