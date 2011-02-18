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
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Manage notes window
    /// </summary>
    public partial class FrmManageNotes : Form
    {
        #region Fields (6) 

        /// <summary>
        /// Constant for btntoggleshownote 
        /// </summary>
        private const string BTNPRETEXTHIDENOTE = "&hide" + BTNSUFTEXTHIDENOTE;

        /// <summary>
        /// Constant for btntoggleshownote
        /// </summary>
        private const string BTNPRETEXTSHOWNOTE = "&show" + BTNSUFTEXTHIDENOTE;

        /// <summary>
        /// Constant for constructing BTNPRETEXTHIDENOTE and BTNPRETEXTSHOWNOTE constant 
        /// </summary>
        private const string BTNSUFTEXTHIDENOTE = " selected";

        /// <summary>
        /// Constant for the fixed width of the number note colum in datagridview1.
        /// </summary>
        private const int COLNOTENRFIXEDWIDTH = 30;

        /// <summary>
        /// Rereference to notes
        /// </summary>
        private Notes notes;

        /// <summary>
        /// Delta point
        /// </summary>
        private Point oldp;

        /// <summary>
        /// The previous painted row number.
        /// </summary>
        private int prevrownr = -1;

        #endregion Fields 

        #region Constructors (1) 

        /// <summary>
        /// Initializes a new instance of the FrmManageNotes class.
        /// </summary>
        /// <param name="notes">The class notes, with access to all the notes.</param>
        public FrmManageNotes(Notes notes)
        {
            this.InitializeComponent();
            this.notes = notes;
            this.DrawNotesGrid();
            this.SetDataGridViewColumsWidth();
            if (this.dataGridView1.RowCount > 0)
            {
                if ((bool)this.dataGridView1.Rows[0].Cells["visible"].Value == true)
                {
                    this.btnShowSelectedNotes.Text = BTNPRETEXTHIDENOTE;
                }
                else
                {
                    this.btnShowSelectedNotes.Text = BTNPRETEXTSHOWNOTE;
                }
            }
            else
            {
                this.btnShowSelectedNotes.Text = BTNPRETEXTSHOWNOTE;
            }
        }

        #endregion Constructors 

        #region Methods (20) 

        // Private Methods (20)

        /// <summary>
        /// Request to backup all notes to a file.
        /// Ask where to save then do it.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
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
                xmlUtil.WriteNotesBackupFile(savebackupdlg.FileName, this.notes);
            }
        }

        /// <summary>
        /// Close form
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// The user pressed the delete button for a note.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnNoteDelete_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Nothing selected.");
            }
            else
            {
                if (Settings.confirmDeletenote)
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

                this.DrawNotesGrid();
                this.SetDataGridViewColumsWidth();
                this.btnNoteDelete.Enabled = false;
                if (this.notes.CountNotes > 0)
                {
                    this.btnNoteDelete.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Request to restore all notes from a backup file.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
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
                            for (int i = 0; i <= this.notes.CountNotes; i++)
                            {
                                File.Delete(Path.Combine(Settings.notesSavepath, this.notes.GetNote(i).Filename));
                            }
                        }
                        else if (eraseres == DialogResult.Cancel)
                        {
                            Log.Write(LogType.info, "Cancelled restore notes backup.");
                            return;
                        }

                        for (int i = 0; i < this.notes.CountNotes; i++)
                        {
                            this.notes.GetNote(i).DestroyForm();
                        }

                        while (this.notes.CountNotes > 0)
                        {
                            this.notes.RemoveNote(0);
                        }

                    }

                    Log.Write(LogType.info, "Imported notes backup file: " + openbackupdlg.FileName);
                    xmlUtil.LoadNotesBackup(this.notes, openbackupdlg.FileName);
                    this.notes.LoadNotes(false, false);
                    this.DrawNotesGrid();
                    this.SetDataGridViewColumsWidth();
                }

                if (this.notes.CountNotes > 0)
                {
                    this.btnNoteDelete.Enabled = true;
                }

            }
        }

        /// <summary>
        /// Toggle visibility selected notes.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnShowSelectedNotes_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedrows = this.dataGridView1.SelectedRows;
            foreach (DataGridViewRow selrow in selectedrows)
            {
                int notepos = this.GetNoteposBySelrow(selrow.Index);
                selrow.Cells["visible"].Value = !this.notes.GetNote(notepos).visible;
                this.notes.GetNote(notepos).visible = !this.notes.GetNote(notepos).visible;
                if (this.notes.GetNote(notepos).visible)
                {
                    this.notes.GetNote(notepos).CreateForm();
                    this.btnShowSelectedNotes.Text = BTNPRETEXTHIDENOTE;
                    //this.Activate();
                }
                else
                {
                    this.notes.GetNote(notepos).DestroyForm();
                    this.btnShowSelectedNotes.Text = BTNPRETEXTSHOWNOTE;
                }

                xmlUtil.WriteNote(this.notes.GetNote(notepos), this.notes.GetSkinName(this.notes.GetNote(notepos).skinNr), this.notes.GetNote(notepos).GetContent());
            }

            this.notes.frmmangenotesneedupdate = false;
        }

        /// <summary>
        /// Cell clicked in dataGridView1, set hide/show note button text.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">DataGridViewCell event arguments</param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if ((bool)this.dataGridView1.Rows[e.RowIndex].Cells["visible"].Value == true)
                {
                    this.btnShowSelectedNotes.Text = BTNPRETEXTHIDENOTE;
                }
                else
                {
                    this.btnShowSelectedNotes.Text = BTNPRETEXTSHOWNOTE;
                }
            }
        }

        /// <summary>
        /// A column is sorted, make sure backgroundcolor skin colum get painted again.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">DataGridViewCellMouse event arguments</param>
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.notes.frmmangenotesneedupdate = true;
            this.dataGridView1.Refresh();
        }

        /// <summary>
        /// Color the skin cell with the foreground color of the skin in this cell.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">DataGridViewRowPostPaint event arguments</param>
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (this.notes.frmmangenotesneedupdate)
            {
                //detect and update add/delete notes.
                if (this.dataGridView1.RowCount != this.notes.CountNotes)
                {
                    this.DrawNotesGrid();
                    this.SetDataGridViewColumsWidth();
                }

                int notepos = this.GetNoteposBySelrow(e.RowIndex);
                this.dataGridView1.Rows[e.RowIndex].Cells["skin"].Style.BackColor = this.notes.GetPrimaryClr(this.notes.GetNote(notepos).skinNr);
                this.dataGridView1.Rows[e.RowIndex].Cells["skin"].Style.ForeColor = this.notes.GetTextClr(this.notes.GetNote(notepos).skinNr);
                if (this.dataGridView1.Rows[e.RowIndex].Cells["skin"].Value.ToString() != this.notes.GetSkinName(this.notes.GetNote(notepos).skinNr))
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells["skin"].Value = this.notes.GetSkinName(this.notes.GetNote(notepos).skinNr);
                }

                this.dataGridView1.Rows[e.RowIndex].Cells["visible"].Value = this.notes.GetNote(notepos).visible;

                if ((e.RowIndex < this.prevrownr) || (e.RowIndex >= this.dataGridView1.RowCount - 1))
                {
                    this.prevrownr = -1;
                    this.notes.frmmangenotesneedupdate = false;
                }

                this.prevrownr = e.RowIndex;
            }
        }

        /// <summary>
        /// Scrolling the datagridview.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Scroll event arguments</param>
        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            this.notes.frmmangenotesneedupdate = true;
        }

#if windows
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
        public struct SHFILEOPSTRUCT
        {
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.U4)]
            public int wFunc;
            public string pFrom;
            public string pTo;
            public short fFlags;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fAnyOperationsAborted;
            public IntPtr hNameMappings;
            public string lpszProgressTitle;
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        static extern int SHFileOperation(ref SHFILEOPSTRUCT FileOp);
        const int FO_DELETE = 3;
        const int FOF_ALLOWUNDO = 0x40;
        const int FOF_NOCONFIRMATION = 0x10;
#endif

        /// <summary>
        /// Deletes the notes in memory and the files that are selected in a Gridview.
        /// In reverse order, so updating datagridview goes well.
        /// </summary>
        /// <param name="selrows">The selected rows in datagridview1</param>
        private void DeleteNotesSelectedRowsGrid(DataGridViewSelectedRowCollection selrows)
        {
            List<int> deletenotepos = new List<int>();
            for (int i = 0; i < selrows.Count; i++)
            {
                deletenotepos.Add(this.GetNoteposBySelrow(selrows[i].Index));
            }

            deletenotepos.Sort();

            for (int r = deletenotepos.Count - 1; r >= 0; r--)
            {
                string filename = this.notes.GetNote(deletenotepos[r]).Filename;
                try
                {
                    this.notes.GetNote(deletenotepos[r]).DestroyForm();
                    string filepath = Path.Combine(Settings.notesSavepath, filename);
                    if (Settings.notesDeleteRecyclebin)
                    {
#if windows
                        SHFILEOPSTRUCT shf = new SHFILEOPSTRUCT(); 
                        shf.wFunc = FO_DELETE; 
                        shf.fFlags = FOF_ALLOWUNDO | FOF_NOCONFIRMATION; 
                        shf.pFrom = filepath+"\0"; //double null terminated
                        SHFileOperation(ref shf);
#elif linux
                        File.Move(filepath, Path.Combine(@"$HOME/.Trash/", filename) );
#endif
                        Log.Write(LogType.info, "Moved note to Recyclebin: " + filepath);
                    }
                    else
                    {
                        File.Delete(filepath);
                        Log.Write(LogType.info, "Deleted note: " + filepath);
                    }


                    this.notes.RemoveNote(deletenotepos[r]);
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

            GC.Collect();
        }

        /// <summary>
        /// Draw a list of all notes.
        /// Sets frmmangenotesneedupdate to true.
        /// </summary>
        private void DrawNotesGrid()
        {
            this.notes.frmmangenotesneedupdate = true;
            this.toolTip.Active = Settings.notesTooltipsEnabled;

            DataTable datatable = new DataTable();
            this.dataGridView1.DataSource = datatable;
            datatable.Columns.Add("nr", typeof(string));
            datatable.Columns["nr"].AutoIncrement = true;
            datatable.Columns["nr"].Unique = true;
            datatable.Columns.Add("title", typeof(string));
            datatable.Columns.Add("visible", typeof(bool));
            datatable.Columns.Add("skin", typeof(string));
            datatable.DefaultView.AllowEdit = false;
            datatable.DefaultView.AllowNew = false;
            this.dataGridView1.Columns["nr"].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.Columns["visible"].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            for (int i = 0; i < this.notes.CountNotes; i++)
            {
                DataRow dr = datatable.NewRow();
                dr[0] = i + 1; //enduser numbering
                dr[1] = this.notes.GetNote(i).title;
                dr[2] = this.notes.GetNote(i).visible;
                dr[3] = this.notes.GetSkinName(this.notes.GetNote(i).skinNr);
                datatable.Rows.Add(dr);
            }
        }

        /// <summary>
        /// FrmManageNotes is activated.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void frmManageNotes_Activated(object sender, EventArgs e)
        {
            if (Settings.notesTransparencyEnabled)
            {
                try
                {
                    this.Opacity = 1.0;
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
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void frmManageNotes_Deactivate(object sender, EventArgs e)
        {
            if (Settings.notesTransparencyEnabled)
            {
                try
                {
                    this.Opacity = Settings.notesTransparencyLevel;
                }
                catch (InvalidCastException)
                {
                    throw new CustomException("Transparency level not a integer or double.");
                }
            }
        }

        /// <summary>
        /// Get the note position in the list by looking up the nr colom with at the partialer row.
        /// </summary>
        /// <param name="rowindex">The selected row index in datagridview1.</param>
        /// <returns>The position of the note in the list.</returns>
        private int GetNoteposBySelrow(int rowindex)
        {
            if (rowindex >= 0)
            {
                return Convert.ToInt32(this.dataGridView1.Rows[rowindex].Cells["nr"].Value) - 1;
            }
            else
            {
                throw new CustomException("Negative rowindex.");
            }
        }

        /// <summary>
        /// The manage note form is beening resized.
        /// </summary>
        /// <param name="sender">Sender object</param>
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
        /// Resize ended, set column width
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void pbResizeGrip_MouseUp(object sender, MouseEventArgs e)
        {
            this.SetDataGridViewColumsWidth();
            this.prevrownr = -1;
            this.notes.frmmangenotesneedupdate = true;
        }

        /// <summary>
        /// Moving frmManageNotes
        /// </summary>
        /// <param name="sender">Sender object</param>
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
        /// Move FrmManageNotes if pnlHead is being left clicked.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Mouse event arguments</param>
        private void pnlHead_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.pnlHead.BackColor = Color.OrangeRed;

                int dpx = e.Location.X - this.oldp.X;
                int dpy = e.Location.Y - this.oldp.Y;
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
        /// <param name="sender">Sender object</param>
        /// <param name="e">Mouse event arguments</param>
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
            int partunit = ((this.dataGridView1.Width - COLNOTENRFIXEDWIDTH) / 10);
            this.dataGridView1.Columns["nr"].Width = 1 * COLNOTENRFIXEDWIDTH;
            this.dataGridView1.Columns["title"].Width = 6 * partunit;
            this.dataGridView1.Columns["visible"].Width = 1 * partunit;
            this.dataGridView1.Columns["skin"].Width = 3 * partunit;
        }

        #endregion Methods 
    }
}
