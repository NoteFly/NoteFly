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
#define windows // platform can be: windows, linux, macos

namespace NoteFly
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    /// <summary>
    /// Manage notes window
    /// </summary>
    public partial class FrmManageNotes : Form
    {
        #region Fields (6) 

#if windows
        /// <summary>
        /// Delete the files specified in pFrom.
        /// </summary>
        private const int FO_DELETE = 3;

        /// <summary>
        /// Preserve Undo information, if possible.
        /// If pFrom does not contain fully qualified path and file names, this flag is ignored.
        /// </summary>
        private const int FOF_ALLOWUNDO = 0x40;

        /// <summary>
        /// Respond with "Yes to All" for any dialog box that is displayed.
        /// </summary>
        private const int FOF_NOCONFIRMATION = 0x10;
#endif

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

        /// <summary>
        /// The previous of previous painted row number.
        /// </summary>
        private int secondprevrownr = -2;

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

#if windows
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern int SHFileOperation(ref SHFILEOPSTRUCT FileOp);
#endif

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
            savebackupdlg.DefaultExt = "nfbak"; // noteflybackup
            savebackupdlg.Title = "Where to save the backup of all NoteFly notes.";
            savebackupdlg.Filter = "NoteFly notes backup (*.nfbak)|*.nfbak";
            savebackupdlg.FileName = DateTime.Today.ToShortDateString() + ".nfbak";
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

                this.resetdatagrid();
                this.DrawNotesGrid();
                this.SetDataGridViewColumsWidth();
                this.btnNoteDelete.Enabled = false;
                if (this.notes.CountNotes > 0)
                {
                    this.btnNoteDelete.Enabled = true;
                }
            }

            this.resetdatagrid();
            this.notes.FrmManageNotesNeedUpdate = true;
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
            openbackupdlg.DefaultExt = "nfbak"; // noteflybackup
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
                                File.Delete(Path.Combine(Settings.NotesSavepath, this.notes.GetNote(i).Filename));
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
                    this.prevrownr = -1;
                    this.secondprevrownr = -2;
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
                selrow.Cells["visible"].Value = !this.notes.GetNote(notepos).Visible;
                this.notes.GetNote(notepos).Visible = !this.notes.GetNote(notepos).Visible;
                if (this.notes.GetNote(notepos).Visible)
                {
                    string tempcontent = this.notes.GetNote(notepos).GetContent();
                    if (tempcontent == string.Empty)
                    {
                        Log.Write(LogType.exception, "Note content is empty.");
                    }

                    this.notes.GetNote(notepos).Tempcontent = tempcontent;
                    this.notes.GetNote(notepos).CreateForm();
                    this.btnShowSelectedNotes.Text = BTNPRETEXTHIDENOTE;
                }
                else
                {
                    this.notes.GetNote(notepos).DestroyForm();
                    this.btnShowSelectedNotes.Text = BTNPRETEXTSHOWNOTE;
                }

                this.resetdatagrid();
                xmlUtil.WriteNote(this.notes.GetNote(notepos), this.notes.GetSkinName(this.notes.GetNote(notepos).SkinNr), this.notes.GetNote(notepos).GetContent());
            }
            
            this.notes.FrmManageNotesNeedUpdate = false;
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

                if (e.ColumnIndex == 2)
                {
                    int notepos = this.GetNoteposBySelrow(e.RowIndex);
                    this.notes.GetNote(notepos).Visible = !this.notes.GetNote(notepos).Visible;
                    this.dataGridView1.Rows[e.RowIndex].Cells[2].Value = !(bool)this.dataGridView1.Rows[e.RowIndex].Cells[2].Value;
                    if (this.notes.GetNote(notepos).Visible)
                    {
                        this.notes.GetNote(notepos).CreateForm();
                    }
                    else
                    {
                        this.notes.GetNote(notepos).DestroyForm();
                    }
                    
                    xmlUtil.WriteNote(this.notes.GetNote(notepos), this.notes.GetSkinName(this.notes.GetNote(notepos).SkinNr), this.notes.GetNote(notepos).GetContent());
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
            this.resetdatagrid();
            this.notes.FrmManageNotesNeedUpdate = true;
            this.dataGridView1.Refresh();
        }

        /// <summary>
        /// Color the skin cell with the foreground color of the skin in this cell.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">DataGridViewRowPostPaint event arguments</param>
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (this.notes.FrmManageNotesNeedUpdate)
            {
                // detect and update add/delete notes.
                if (this.dataGridView1.RowCount != this.notes.CountNotes)
                {
                    this.DrawNotesGrid();
                    this.SetDataGridViewColumsWidth();
                }

                int notepos = this.GetNoteposBySelrow(e.RowIndex);
                this.dataGridView1.Rows[e.RowIndex].Cells["title"].Value = this.notes.GetNote(notepos).Title;
                this.dataGridView1.Rows[e.RowIndex].Cells["skin"].Style.BackColor = this.notes.GetPrimaryClr(this.notes.GetNote(notepos).SkinNr);
                this.dataGridView1.Rows[e.RowIndex].Cells["skin"].Style.ForeColor = this.notes.GetTextClr(this.notes.GetNote(notepos).SkinNr);
                if (this.dataGridView1.Rows[e.RowIndex].Cells["skin"].Value.ToString() != this.notes.GetSkinName(this.notes.GetNote(notepos).SkinNr))
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells["skin"].Value = this.notes.GetSkinName(this.notes.GetNote(notepos).SkinNr);
                }

                this.dataGridView1.Rows[e.RowIndex].Cells["visible"].Value = this.notes.GetNote(notepos).Visible;

                if (e.RowIndex == this.dataGridView1.RowCount - 1)
                {
                    this.notes.FrmManageNotesNeedUpdate = false;
                }
                else if (this.prevrownr < this.secondprevrownr)
                {
                    this.notes.FrmManageNotesNeedUpdate = false;
                }
                else
                {
                    this.secondprevrownr = this.prevrownr;
                    this.prevrownr = e.RowIndex;
                }
            }
        }

        /// <summary>
        /// Scrolling the datagridview.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Scroll event arguments</param>
        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                this.notes.FrmManageNotesNeedUpdate = true;
            }
        }

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
                    string filepath = Path.Combine(Settings.NotesSavepath, filename);
                    if (Settings.NotesDeleteRecyclebin)
                    {
#if windows
                        SHFILEOPSTRUCT shf = new SHFILEOPSTRUCT(); 
                        shf.wFunc = FO_DELETE; 
                        shf.fFlags = FOF_ALLOWUNDO | FOF_NOCONFIRMATION; 
                        shf.pFrom = filepath + "\0"; // double null terminated
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
                    throw new ApplicationException(filenotfoundexc.Message);
                }
                catch (UnauthorizedAccessException)
                {
                    string msgaccessdenied = "Access denied. delete note " + filename + " manually with proper premission.";
                    Log.Write(LogType.error, msgaccessdenied);
                    MessageBox.Show(msgaccessdenied);
                }
            }

            this.resetdatagrid();
            this.DrawNotesGrid();
            GC.Collect();
        }

        /// <summary>
        /// Reset the previous drawed row numbers in datagridview1.
        /// </summary>
        private void resetdatagrid()
        {
            this.prevrownr = -1;
            this.secondprevrownr = -2;
        }

        /// <summary>
        /// Draw a list of all notes.
        /// Sets FrmManageNotesNeedUpdate to true.
        /// </summary>
        private void DrawNotesGrid()
        {
            this.resetdatagrid();
            this.notes.FrmManageNotesNeedUpdate = true;
            this.toolTip.Active = Settings.NotesTooltipsEnabled;
			
            DataTable datatable = new DataTable();
            this.dataGridView1.DataSource = datatable;
            datatable.Columns.Add("nr", typeof(string));
            datatable.Columns["nr"].AutoIncrement = true;
            datatable.Columns["nr"].Unique = true;
            datatable.Columns.Add("title", typeof(string));
            datatable.Columns.Add("visible", typeof(bool));
            datatable.Columns.Add("skin", typeof(string));			
            datatable.DefaultView.AllowEdit = true;
            datatable.DefaultView.AllowNew = false;
			if (this.dataGridView1.Columns["nr"]!=null) {
             this.dataGridView1.Columns["nr"].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
			}
			if (this.dataGridView1.Columns["visible"]!=null) {
                this.dataGridView1.Columns["visible"].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
			}
            this.dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            for (int i = 0; i < this.notes.CountNotes; i++)
            {
                DataRow dr = datatable.NewRow();
                dr[0] = i + 1; // enduser numbering
                dr[1] = this.notes.GetNote(i).Title;
                dr[2] = this.notes.GetNote(i).Visible;
                dr[3] = this.notes.GetSkinName(this.notes.GetNote(i).SkinNr);
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
            if (Settings.NotesTransparencyEnabled)
            {
                try
                {
                    this.Opacity = 1.0;
                }
                catch (InvalidCastException)
                {
                    throw new ApplicationException("Transparency level not a integer or double.");
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
            if (Settings.NotesTransparencyEnabled)
            {
                try
                {
                    this.Opacity = Settings.NotesTransparencyLevel;
                }
                catch (InvalidCastException)
                {
                    throw new ApplicationException("Transparency level not a integer or double.");
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
                throw new ApplicationException("Negative rowindex.");
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
            this.notes.FrmManageNotesNeedUpdate = true;
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
            if ((this.dataGridView1.Width <= 0) || (this.dataGridView1 ==null))
            {
                return;
            }
			
            int partunit = (this.dataGridView1.Width - COLNOTENRFIXEDWIDTH) / 10;
			if (this.dataGridView1.Columns["nr"] != null) {
            this.dataGridView1.Columns["nr"].Width = 1 * COLNOTENRFIXEDWIDTH;
			}
			if (this.dataGridView1.Columns["title"] != null) {
            this.dataGridView1.Columns["title"].Width = 6 * partunit;
			}
			if (this.dataGridView1.Columns["visible"]!=null) {
            this.dataGridView1.Columns["visible"].Width = 1 * partunit;
			}
			if (this.dataGridView1.Columns["skin"]!=null) {
            this.dataGridView1.Columns["skin"].Width = 3 * partunit;
			}
        }

#if windows
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]

        /// <summary>
        /// Windows SHFILEOPSTRUCT struct
        /// </summary>
        public struct SHFILEOPSTRUCT
        {
            /// <summary>
            /// A window handle to the dialog box to display information about the status of the file operation.(MSDN)
            /// </summary>
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.U4)]

            /// <summary>
            /// A value that indicates which operation to perform. One of the following values.(MSDN)
            /// </summary>
            public int wFunc;

            /// <summary>
            /// A pointer to one or more source file names. These names should be fully-qualified paths to prevent unexpected results.
            /// </summary>
            public string pFrom;

            /// <summary>
            /// A pointer to the destination file or directory name.
            /// </summary>
            public string pTo;

            /// <summary>
            /// Flags that control the file operation.
            /// </summary>
            public short fFlags;

            [MarshalAs(UnmanagedType.Bool)]

            /// <summary>
            /// When the function returns, this member contains TRUE if any file operations were aborted before they were completed; otherwise, FALSE.
            /// </summary>
            public bool fAnyOperationsAborted;

            /// <summary>
            /// When the function returns, this member contains a handle to a name mapping object that contains the old and new names of the renamed files.
            /// </summary>
            public IntPtr hNameMappings;

            /// <summary>
            /// A pointer to the title of a progress dialog box.
            /// </summary>
            public string lpszProgressTitle;
        }
#endif

        #endregion Methods 
    }
}
