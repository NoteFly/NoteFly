//-----------------------------------------------------------------------
// <copyright file="FrmManageNotes.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2010-2012  Tom
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
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// Manage notes window
    /// </summary>
    public sealed partial class FrmManageNotes : Form
    {
        #region Fields (5)

        /// <summary>
        /// Constant for the fixed width of the number note colum in datagridview1.
        /// </summary>
        private const int COLNOTENRFIXEDWIDTH = 30;

        /// <summary>
        /// Column index of the nr column.
        /// </summary>
        private const int COLINDEXNR = 0;

        /// <summary>
        /// Column index of the title column.
        /// </summary>
        private const int COLINDEXTITLE = 1;

        /// <summary>
        /// Column index of the visible column.
        /// </summary>
        private const int COLINDEXVISIBLE = 2;

        /// <summary>
        /// Column index of the skin column.
        /// </summary>
        private const int COLINDEXSKIN = 3;

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
            this.DoubleBuffered = Settings.ProgramFormsDoublebuffered;
            this.InitializeComponent();
            this.SetFormTitle();
            Strings.TranslateForm(this);
            this.notes = notes;
            this.SetSkin();
            this.DrawNotesGrid();
            this.SetDataGridViewColumsWidth();

            if (this.dataGridViewNotes.RowCount > 0)
            {
                if ((bool)this.dataGridViewNotes.Rows[0].Cells[COLINDEXVISIBLE].Value == true)
                {
                    this.btnShowSelectedNotes.Text = Strings.T("&hide selected");
                }
                else
                {
                    this.btnShowSelectedNotes.Text = Strings.T("&show selected");
                }
            }
            else
            {
                this.btnShowSelectedNotes.Text = Strings.T("&show selected");
            }

            if (PluginsManager.EnabledPlugins != null)
            {
                for (int p = 0; p < PluginsManager.EnabledPlugins.Count; p++)
                {
                    if (PluginsManager.EnabledPlugins[p].InitFrmManageNotesBtns() != null)
                    {
                        Button[] buttons = PluginsManager.EnabledPlugins[p].InitFrmManageNotesBtns();
                        for (int i = 0; i < buttons.Length; i++)
                        {
                            this.tableLayoutPanelButtons.ColumnCount += 1;
                            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize, 80));
                            this.tableLayoutPanelButtons.Controls.Add(buttons[i], this.tableLayoutPanelButtons.ColumnCount - 1, 0);
                        }
                    }
                }
            }
        }

        #endregion Constructors

#if windows
        /// <summary>
        /// Possible flags for the SHFileOperation method.
        /// </summary>
        [Flags]
        public enum FileOperationFlags : ushort
        {
            /// <summary>
            /// Do not show a dialog during the process
            /// </summary>
            FOF_SILENT = 0x0004,

            /// <summary>
            /// Do not ask the user to confirm selection
            /// </summary>
            FOF_NOCONFIRMATION = 0x0010,

            /// <summary>
            /// Delete the file to the recycle bin.  (Required flag to send a file to the bin
            /// </summary>
            FOF_ALLOWUNDO = 0x0040,

            /// <summary>
            /// Do not show the names of the files or folders that are being recycled.
            /// </summary>
            FOF_SIMPLEPROGRESS = 0x0100,

            /// <summary>
            /// Surpress errors, if any occur during the process.
            /// </summary>
            FOF_NOERRORUI = 0x0400,

            /// <summary>
            /// Warn if files are too big to fit in the recycle bin and will need
            /// to be deleted completely.
            /// </summary>
            FOF_WANTNUKEWARNING = 0x4000,
        }

        /// <summary>
        /// File Operation Function Type for SHFileOperation
        /// </summary>
        public enum FileOperationType : uint
        {
            /// <summary>
            /// Move the objects
            /// </summary>
            FO_MOVE = 0x0001,

            /// <summary>
            /// Copy the objects
            /// </summary>
            FO_COPY = 0x0002,

            /// <summary>
            /// Delete (or recycle) the objects
            /// </summary>
            FO_DELETE = 0x0003,

            /// <summary>
            /// Rename the object(s)
            /// </summary>
            FO_RENAME = 0x0004,
        }
#endif


        #region Methods (20)

        /// <summary>
        /// Reset the previous drawed row numbers in datagridview1.
        /// </summary>
        public void Resetdatagrid()
        {
            this.prevrownr = -1;
            this.secondprevrownr = -2;
        }

#if windows
        [DllImport("shell32.dll", CharSet = CharSet.Auto, EntryPoint = "SHFileOperation")]
        private static extern int SHFileOperation_x86(ref SHFILEOPSTRUCT_x86 FileOp);

        [DllImport("shell32.dll", CharSet = CharSet.Auto, EntryPoint = "SHFileOperation")]
        private static extern int SHFileOperation_x64(ref SHFILEOPSTRUCT_x64 FileOp);

        /// <summary>
        /// Check whether this is a 64bit operating system
        /// </summary>
        /// <returns>true if this is a 64bit operating system</returns>
        private static bool IsWOW64Process()
        {
            return IntPtr.Size == 8;
        }
#endif

        /// <summary>
        /// Set the skin for the FrmManageNotes form.
        /// </summary>
        private void SetSkin()
        {
            this.BackColor = this.notes.GetPrimaryClr(Settings.ManagenotesSkinnr);
            this.pnlHead.BackColor = this.notes.GetPrimaryClr(Settings.ManagenotesSkinnr);
            this.ForeColor = this.notes.GetTextClr(Settings.ManagenotesSkinnr);

            this.btnShowSelectedNotes.FlatAppearance.MouseOverBackColor = this.notes.GetHighlightClr(Settings.ManagenotesSkinnr);
            this.btnNoteDelete.FlatAppearance.MouseOverBackColor = this.notes.GetHighlightClr(Settings.ManagenotesSkinnr);
            this.btnRestoreAllNotes.FlatAppearance.MouseOverBackColor = this.notes.GetHighlightClr(Settings.ManagenotesSkinnr);
            this.btnBackAllNotes.FlatAppearance.MouseOverBackColor = this.notes.GetHighlightClr(Settings.ManagenotesSkinnr);

            this.btnShowSelectedNotes.ForeColor = this.notes.GetTextClr(Settings.ManagenotesSkinnr);
            this.btnNoteDelete.ForeColor = this.notes.GetTextClr(Settings.ManagenotesSkinnr);
            this.btnRestoreAllNotes.ForeColor = this.notes.GetTextClr(Settings.ManagenotesSkinnr);
            this.btnBackAllNotes.ForeColor = this.notes.GetTextClr(Settings.ManagenotesSkinnr);

            if (this.notes.GetPrimaryTexture(Settings.ManagenotesSkinnr) != null)
            {
                this.BackgroundImage = this.notes.GetPrimaryTexture(Settings.ManagenotesSkinnr);
                this.BackgroundImageLayout = this.notes.GetPrimaryTextureLayout(Settings.ManagenotesSkinnr);
                this.pnlHead.BackColor = Color.Transparent;
            }
        }

        /// <summary>
        /// Set the title of this form.
        /// </summary>
        private void SetFormTitle()
        {
            this.Text = Strings.T("Manage notes") + " - " + Program.AssemblyTitle;
        }

        /// <summary>
        /// Request to backup all notes to a file.
        /// Ask where to save then do it.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnBackAllNotes_Click(object sender, EventArgs e)
        {
            DialogResult savebackupdlgres = this.saveExportFileDialog.ShowDialog();
            if (savebackupdlgres == DialogResult.OK)
            {
                ExportNotes exportnotes = new ExportNotes(this.notes);
                switch (this.saveExportFileDialog.FilterIndex)
                {
                    case 1:
                        exportnotes.WriteNoteFlyNotesBackupFile(this.saveExportFileDialog.FileName);
                        break;
                    case 2:
                        exportnotes.WriteStickiesCSVBackupfile(this.saveExportFileDialog.FileName);
                        break;
                    case 3:
                        exportnotes.WritePNotesBackupfile(this.saveExportFileDialog.FileName);
                        break;
                }
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
            if (this.dataGridViewNotes.SelectedRows.Count <= 0)
            {
                string managenotes_nothingselected = Strings.T("Nothing selected.");
                Log.Write(LogType.info, managenotes_nothingselected);
                MessageBox.Show(managenotes_nothingselected);
            }
            else
            {
                if (Settings.ConfirmDeletenote)
                {
                    StringBuilder sbdeleteselectednotes = new StringBuilder();
                    sbdeleteselectednotes.AppendLine(Strings.T("Are you sure you want to delete the selected note(s)?"));

                    const int MAXNUMTITLESHOW = 15;
                    for (int i = 0; i < this.dataGridViewNotes.SelectedRows.Count && i < MAXNUMTITLESHOW; i++)
                    {
                        int rowindex = this.dataGridViewNotes.SelectedRows[i].Index;
                        string title = this.notes.GetNote(this.GetNoteposBySelrow(rowindex)).Title;

                        sbdeleteselectednotes.Append("- ");
                        sbdeleteselectednotes.AppendLine(title);
                        if (i == MAXNUMTITLESHOW - 1)
                        {
                            sbdeleteselectednotes.AppendLine(Strings.T("And more notes."));
                        }
                    }

                    DialogResult deleteres = MessageBox.Show(sbdeleteselectednotes.ToString(), Strings.T("delete?"), MessageBoxButtons.YesNo);
                    if (deleteres == DialogResult.Yes)
                    {
                        this.DeleteNotesSelectedRowsGrid(this.dataGridViewNotes.SelectedRows);
                    }
                }
                else
                {
                    this.DeleteNotesSelectedRowsGrid(this.dataGridViewNotes.SelectedRows);
                }

                this.Resetdatagrid();
                this.DrawNotesGrid();
                this.SetDataGridViewColumsWidth();
                this.btnNoteDelete.Enabled = false;
                if (this.notes.CountNotes > 0)
                {
                    this.btnNoteDelete.Enabled = true;
                }
            }

            this.Resetdatagrid();
            Program.Formmanager.FrmManageNotesNeedUpdate = true;
            Application.DoEvents();
        }

        /// <summary>
        /// Request to restore all notes from a backup file.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnRestoreAllNotes_Click(object sender, EventArgs e)
        {
            DialogResult openbackupdlgres = this.openImportFileDialog.ShowDialog();
            if (openbackupdlgres == DialogResult.OK)
            {
                ImportNotes importnote = new ImportNotes(this.notes);
                switch (this.openImportFileDialog.FilterIndex)
                {
                    case 1:
                        importnote.ReadNoteFlyBackupFile(this.openImportFileDialog.FileName);
                        break;
                    case 2:
                        importnote.ReadStickiesCSVFile(this.openImportFileDialog.FileName);
                        break;
                    case 3:
                        importnote.ReadPNotesBackupFile(this.openImportFileDialog.FileName);
                        break;
                    case 4:
                        importnote.ReadCintaNotesXMLFile(this.openImportFileDialog.FileName);
                        break;
                    case 5:
                        importnote.ReadDeskNotesXmlFile(this.openImportFileDialog.FileName);
                        break;
                }

                this.Resetdatagrid();
                this.DrawNotesGrid();
                this.SetDataGridViewColumsWidth();

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
            DataGridViewSelectedRowCollection selectedrows = this.dataGridViewNotes.SelectedRows;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                foreach (DataGridViewRow selrow in selectedrows)
                {
                    int notepos = this.GetNoteposBySelrow(selrow.Index);
                    if (notepos >= 0)
                    {
                        selrow.Cells[COLINDEXVISIBLE].Value = !this.notes.GetNote(notepos).Visible;
                        this.notes.GetNote(notepos).Visible = !this.notes.GetNote(notepos).Visible;
                        if (this.notes.GetNote(notepos).Visible)
                        {
                            this.notes.GetNote(notepos).CreateForm();
                            this.btnShowSelectedNotes.Text = Strings.T("&hide selected");
                        }
                        else
                        {
                            this.notes.GetNote(notepos).DestroyForm();
                            this.btnShowSelectedNotes.Text = Strings.T("&show selected");
                        }

                        this.Resetdatagrid();
                        Application.DoEvents();
                        xmlUtil.WriteNote(this.notes.GetNote(notepos), this.notes.GetSkinName(this.notes.GetNote(notepos).SkinNr), this.notes.GetNote(notepos).GetContent());
                    }
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

            Program.Formmanager.FrmManageNotesNeedUpdate = true;
        }

        /// <summary>
        /// Cell clicked in dataGridViewNotes, set hide/show note button text.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">DataGridViewCell event arguments</param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if ((bool)this.dataGridViewNotes.Rows[e.RowIndex].Cells[COLINDEXVISIBLE].Value == true)
                {
                    this.btnShowSelectedNotes.Text = Strings.T("&hide selected");
                }
                else
                {
                    this.btnShowSelectedNotes.Text = Strings.T("&show selected");
                }

                if (e.ColumnIndex == 2)
                {
                    this.ToggleVisibilityNote(e.RowIndex);
                }
            }
        }

        /// <summary>
        /// Toggle the visibility of a note.
        /// </summary>
        /// <param name="row">The row number in dataGridView1 that is double clicked.</param>
        private void ToggleVisibilityNote(int row)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.TopMost = true;
                int notepos = this.GetNoteposBySelrow(row);
                if (notepos >= 0)
                {
                    this.notes.GetNote(notepos).Visible = !this.notes.GetNote(notepos).Visible;
                    this.dataGridViewNotes.Rows[row].Cells[COLINDEXVISIBLE].Value = !(bool)this.dataGridViewNotes.Rows[row].Cells[COLINDEXVISIBLE].Value;
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
            catch (ArgumentOutOfRangeException argoutrangeexc)
            {
                Log.Write(LogType.exception, argoutrangeexc.Message);
            }
            finally
            {
                this.TopMost = false;
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// A column is sorted, make sure backgroundcolor skin colum get painted again.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">DataGridViewCellMouse event arguments</param>
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.Resetdatagrid();
            Program.Formmanager.FrmManageNotesNeedUpdate = true;
            this.dataGridViewNotes.Refresh();
        }

        /// <summary>
        /// Color the skin cell with the foreground color of the skin in this cell.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">DataGridViewRowPostPaint event arguments</param>
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            // detect and update add/delete notes, then redraw all notes
            if (Program.Formmanager.FrmManageNotesNeedUpdate)
            {
                if (this.dataGridViewNotes.RowCount != this.notes.CountNotes && !this.searchTextBoxNotes.IsKeywordEntered)
                {
                    this.DrawNotesGrid();
                    this.SetDataGridViewColumsWidth();
                }

                int notepos = this.GetNoteposBySelrow(e.RowIndex);
                if (notepos >= 0)
                {
                    this.dataGridViewNotes.Rows[e.RowIndex].Cells[COLINDEXTITLE].Value = this.notes.GetNote(notepos).Title;
                    this.dataGridViewNotes.Rows[e.RowIndex].Cells[COLINDEXSKIN].Style.BackColor = this.notes.GetPrimaryClr(this.notes.GetNote(notepos).SkinNr);
                    this.dataGridViewNotes.Rows[e.RowIndex].Cells[COLINDEXSKIN].Style.ForeColor = this.notes.GetTextClr(this.notes.GetNote(notepos).SkinNr);
                    if (this.dataGridViewNotes.Rows[e.RowIndex].Cells[COLINDEXSKIN].Value.ToString() != this.notes.GetSkinName(this.notes.GetNote(notepos).SkinNr))
                    {
                        this.dataGridViewNotes.Rows[e.RowIndex].Cells[COLINDEXSKIN].Value = this.notes.GetSkinName(this.notes.GetNote(notepos).SkinNr);
                    }

                    this.dataGridViewNotes.Rows[e.RowIndex].Cells[COLINDEXVISIBLE].Value = this.notes.GetNote(notepos).Visible;

                    if ((e.RowIndex == this.dataGridViewNotes.RowCount - 1) || (this.prevrownr < this.secondprevrownr))
                    {
                        Program.Formmanager.FrmManageNotesNeedUpdate = false;
                    }
                    else
                    {
                        this.secondprevrownr = this.prevrownr;
                        this.prevrownr = e.RowIndex;
                    }
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
                Program.Formmanager.FrmManageNotesNeedUpdate = true;
                this.Resetdatagrid();
                Application.DoEvents();
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
                int notepos = this.GetNoteposBySelrow(selrows[i].Index);
                if (notepos >= 0)
                {
                    deletenotepos.Add(notepos);
                }
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
                        if (IsWOW64Process())
                        {
                            SHFILEOPSTRUCT_x64 fs = new SHFILEOPSTRUCT_x64();
                            fs.wFunc = FileOperationType.FO_DELETE;
                            // Important to double-terminate the string.
                            fs.pFrom = filepath + '\0' + '\0';
                            fs.fFlags = FileOperationFlags.FOF_ALLOWUNDO | FileOperationFlags.FOF_NOCONFIRMATION | FileOperationFlags.FOF_WANTNUKEWARNING;
                            SHFileOperation_x64(ref fs);
                        }
                        else
                        {
                            SHFILEOPSTRUCT_x86 fs = new SHFILEOPSTRUCT_x86();
                            fs.wFunc = FileOperationType.FO_DELETE;
                            // important to double-terminate the string.
                            fs.pFrom = filepath + '\0' + '\0';
                            fs.fFlags = FileOperationFlags.FOF_ALLOWUNDO | FileOperationFlags.FOF_NOCONFIRMATION | FileOperationFlags.FOF_WANTNUKEWARNING;
                            SHFileOperation_x86(ref fs);
                        }
#elif linux
                        // move file to trash folder, located: ~/.local/share/Trash/files
                        string trashfolder = System.Environment.GetEnvironmentVariable("HOME") +"/.local/share/Trash/files/";
                        if (!Directory.Exists(trashfolder))
                        {
                            Directory.CreateDirectory(trashfolder);
                            Log.Write(LogType.info, "Trash folder created: " + trashfolder);
                        }
                        
                        try
                        {
                            File.Move(filepath, Path.Combine(trashfolder, filename));
                        }
                        catch (IOException ioexc)
                        {
                            Log.Write(LogType.exception, ioexc.Message);
                        }
#endif

                        if (!File.Exists(filepath))
                        {
                            Log.Write(LogType.info, "Moved note to Recyclebin: " + filepath);
                        }
                        else
                        {
                            const string OLDFILEEXTENSION = ".old";
                            Log.Write(LogType.exception, string.Format("Could not move note to recyclebin. Trying renaming with {0} appended to note filename.", OLDFILEEXTENSION));

                            if (!File.Exists(filepath + OLDFILEEXTENSION))
                            {
                                try
                                {
                                    File.Move(filepath, filepath + OLDFILEEXTENSION);
                                }
                                catch (IOException ioexc)
                                {
                                    Log.Write(LogType.exception, ioexc.Message);
                                }
                            }
                        }
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
                    string managenotes_msgaccessdenied = Strings.T("Access denied. delete note {0} manually with proper premission.", filename);
                    Log.Write(LogType.error, managenotes_msgaccessdenied);
                    MessageBox.Show(managenotes_msgaccessdenied);
                }
            }

            this.Resetdatagrid();
            this.DrawNotesGrid();
            GC.Collect();
        }

        /// <summary>
        /// Draw a list of all notes.
        /// Sets FrmManageNotesNeedUpdate to true.
        /// </summary>
        private void DrawNotesGrid()
        {
            ////int vertscrolloffset = this.dataGridViewNotes.VerticalScrollingOffset;
            this.Resetdatagrid();
            Program.Formmanager.FrmManageNotesNeedUpdate = true;
            this.toolTip.Active = Settings.NotesTooltipsEnabled;
            if (!this.searchTextBoxNotes.IsKeywordEntered)
            {
                DataTable datatable = this.CreateDatatable();
                int numnotefiles = Directory.GetFiles(Settings.NotesSavepath, "*" + Notes.NOTEEXTENSION, SearchOption.TopDirectoryOnly).Length;
                if (this.notes.CountNotes > numnotefiles)
                {
                    Log.Write(LogType.exception, "Note file(s) deleted while programme was running. Reloading all note files.");
                    while (this.notes.CountNotes > 0)
                    {
                        this.notes.GetNote(0).DestroyForm();
                        this.notes.RemoveNote(0);
                    }
                    this.notes.LoadNotes(true, false);
                }

                for (int i = 0; i < this.notes.CountNotes; i++)
                {
                    datatable = this.AddDatatableNoteRow(datatable, i);
                }
            }

            // VerticalScrollingOffset is readonly, so we need a bit of 'hacking' to set it.
            // disabled causes System.RuntimeMethodHandle._InvokeMethodFast to throw exception
            /*
            if (vertscrolloffset > 0)
            {
                try
                {
                    System.Reflection.PropertyInfo verticalOffset = this.dataGridViewNotes.GetType().GetProperty("VerticalOffset", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                    verticalOffset.SetValue(this.dataGridViewNotes, vertscrolloffset, null);
                }
                catch (System.Reflection.TargetInvocationException tiexc)
                {
                    Log.Write(LogType.exception, tiexc.Message + " stacktrace: " + tiexc.StackTrace);
                }
            }
            */
        }

        /// <summary>
        /// Create a new datatable object with translated columns for notes overview.
        /// </summary>
        /// <returns>A new datatable object.</returns>
        private DataTable CreateDatatable()
        {
            DataTable datatable = new DataTable();
            this.dataGridViewNotes.DataSource = datatable;
            string colnr = "nr";
            string coltitle = "title";
            string colvisible = "visible";
            string colskin = "skin";
            try
            {
                colnr = Strings.T("nr");
                coltitle = Strings.T("title");
                colvisible = Strings.T("visible");
                colskin = Strings.T("skin");
            }
            catch (Exception exc)
            {
                Log.Write(LogType.exception, exc.Message);
            }

            datatable.Columns.Add(colnr, typeof(string)); // colindexnr
            datatable.Columns[COLINDEXNR].AutoIncrement = true;
            datatable.Columns[COLINDEXNR].Unique = true;
            datatable.Columns.Add(coltitle, typeof(string)); // colindextitle
            datatable.Columns.Add(colvisible, typeof(bool)); //colindexvisible
            datatable.Columns.Add(colskin, typeof(string)); // colindexskin
            datatable.DefaultView.AllowEdit = true;
            datatable.DefaultView.AllowNew = false;
            if (this.dataGridViewNotes.Columns[COLINDEXNR] != null)
            {
                this.dataGridViewNotes.Columns[COLINDEXNR].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (this.dataGridViewNotes.Columns[COLINDEXVISIBLE] != null)
            {
                this.dataGridViewNotes.Columns[COLINDEXVISIBLE].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            this.dataGridViewNotes.Font = new Font(Settings.ManagenotesFontFamily, Settings.ManagenotesFontsize);
            this.dataGridViewNotes.RowPostPaint += new DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            return datatable;
        }

        /// <summary>
        /// Add a row with note information to a databasetable
        /// </summary>
        /// <param name="datatable">The database to add a row to.</param>
        /// <param name="notepos">The position of the note in the notes list in Notes class.</param>
        /// <returns>The databasetable with a extra row</returns>
        private DataTable AddDatatableNoteRow(DataTable datatable, int notepos)
        {
            DataRow dr = datatable.NewRow();
            dr[COLINDEXNR] = notepos + 1; // enduser numbering, start at 1 instead of 0.
            dr[COLINDEXTITLE] = this.notes.GetNote(notepos).Title;
            dr[COLINDEXVISIBLE] = this.notes.GetNote(notepos).Visible;
            dr[COLINDEXSKIN] = this.notes.GetSkinName(this.notes.GetNote(notepos).SkinNr);
            datatable.Rows.Add(dr);
            return datatable;
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
                    string managenotes_invalidtransparencylvl = Strings.T("Transparency level not a integer or double.");
                    throw new ApplicationException(managenotes_invalidtransparencylvl);
                }
            }
        }

        /// <summary>
        /// Form not active, make tranparent if set.
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
                    string managenotes_invalidtransparencylvl = Strings.T("Transparency level not a integer or double.");
                    throw new ApplicationException(managenotes_invalidtransparencylvl);
                }
            }
        }

        /// <summary>
        /// Get the note position in the list by looking up the nr colom with at the partialer row.
        /// </summary>
        /// <param name="rowindex">The selected row index in datagridview1.</param>
        /// <returns>The position of the note in the list. -1 if error.</returns>
        private int GetNoteposBySelrow(int rowindex)
        {
            if (rowindex >= 0)
            {
                return Convert.ToInt32(this.dataGridViewNotes.Rows[rowindex].Cells[COLINDEXNR].Value) - 1;
            }
            else
            {
                return -1;
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
            this.DrawNotesGrid();
            this.SetDataGridViewColumsWidth();
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
                this.pnlHead.BackColor = this.notes.GetSelectClr(Settings.ManagenotesSkinnr);
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
                int dpx = e.Location.X - this.oldp.X;
                int dpy = e.Location.Y - this.oldp.Y;
#if linux
                // limit the moving of this note under mono/linux so this note cannot move uncontrolled a lot.
                const int movelimit = 8;
                if (dpx > movelimit)
                {
                    dpx = movelimit;
                }
                else if (dpx < -movelimit)
                {
                    dpx = -movelimit;
                }

                if (dpy > movelimit)
                {
                    dpy = movelimit;
                }
                else if (dpy < -movelimit)
                {
                    dpy = -movelimit;
                }
#endif
                this.Location = new Point(this.Location.X + dpx, this.Location.Y + dpy);
            }
        }

        /// <summary>
        /// End moving FrmManageNotes.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Mouse event arguments</param>
        private void pnlHead_MouseUp(object sender, MouseEventArgs e)
        {
            this.pnlHead.BackColor = this.notes.GetPrimaryClr(Settings.ManagenotesSkinnr);
            if (this.BackgroundImage != null)
            {
                this.pnlHead.BackColor = Color.Transparent;
            }
        }

        /// <summary>
        /// Sets every colom of the datagridview to a reasonable width.
        /// </summary>
        private void SetDataGridViewColumsWidth()
        {
            if ((this.dataGridViewNotes.Width <= 0) || (this.dataGridViewNotes == null))
            {
                return;
            }

            int partunit = (this.dataGridViewNotes.Width - COLNOTENRFIXEDWIDTH) / 10;
            if (this.dataGridViewNotes.Columns[COLINDEXNR] != null)
            {
                this.dataGridViewNotes.Columns[COLINDEXNR].Width = 1 * COLNOTENRFIXEDWIDTH;
            }

            if (this.dataGridViewNotes.Columns[COLINDEXTITLE] != null)
            {
                this.dataGridViewNotes.Columns[COLINDEXTITLE].Width = 6 * partunit;
            }

            if (this.dataGridViewNotes.Columns[COLINDEXVISIBLE] != null)
            {
                this.dataGridViewNotes.Columns[COLINDEXVISIBLE].Width = 1 * partunit;
            }

            if (this.dataGridViewNotes.Columns[COLINDEXSKIN] != null)
            {
                this.dataGridViewNotes.Columns[COLINDEXSKIN].Width = 3 * partunit;
            }
        }

        /// <summary>
        /// Double click a row in dataGridView toggle the visibility of a note.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">DataGridViewCellEvent Arguments</param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.ToggleVisibilityNote(e.RowIndex);
        }

        /// <summary>
        /// Display a tooltip with the note content for the hovered note in that row.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">DataGridViewCell event arguments</param>
        private void dataGridViewNotes_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (Settings.NotesTooltipsEnabled && Settings.ManagenotesTooltip)
            {
                if (e.ColumnIndex == 1)
                {
                    string contentpreview = null;
                    if (e.RowIndex >= 0)
                    {
                        int notepos = this.GetNoteposBySelrow(e.RowIndex);
                        if (notepos >= 0)
                        {
                            // todo GetContent() is not good because it can take long to read all content but it works for now.
                            RichTextBox rtb = new RichTextBox();
                            rtb.Rtf = this.notes.GetNote(notepos).GetContent();
                            //xmlUtil.GetContentStringLimited(
                            string content = rtb.Text;
                            rtb.Dispose();
                            GC.Collect();
                            int startpos = 0;
                            string startcontentplainhint = string.Empty;
                            try
                            {
                                int lencontentpreview = content.Length - startpos - startcontentplainhint.Length;
                                const int MAXLENCONTENTPREVIEW = 100;
                                if (lencontentpreview > MAXLENCONTENTPREVIEW)
                                {
                                    contentpreview = content.Substring(startpos + startcontentplainhint.Length, MAXLENCONTENTPREVIEW);
                                    contentpreview += "..";
                                }
                                else
                                {
                                    contentpreview = content.Substring(startpos + startcontentplainhint.Length, lencontentpreview);
                                }

                                content = null;
                            }
                            catch (ArgumentOutOfRangeException argoutrange)
                            {
                                throw new ApplicationException(argoutrange.Message);
                            }

                            int tooltiplocx = Cursor.Position.X - this.Location.X;
                            int tooltiplocy = Cursor.Position.Y - this.Location.Y;
                            if (!string.IsNullOrEmpty(contentpreview))
                            {
                                this.toolTip.InitialDelay = 200;
                                this.toolTip.Show(contentpreview, this, new Point(tooltiplocx, tooltiplocy), 2000);
                            }
                        }
                    }
                }
                else
                {
                    this.toolTip.Hide(this);
                }
            }
        }

        /// <summary>
        /// Start searching on a keyword.
        /// </summary>
        /// <param name="keywords">The keyword to search on.</param>
        private void searchTextBoxNotes_SearchStart(string keywords)
        {
            this.Resetdatagrid();
            DataTable dt = this.CreateDatatable();
            for (int i = 0; i < this.notes.CountNotes; i++)
            {
                string title = this.notes.GetNote(i).Title;
                if (!Settings.ManagenotesSearchCasesentive)
                {
                    title = this.notes.GetNote(i).Title.ToLowerInvariant();
                    keywords = keywords.ToLowerInvariant();
                }

                if (title.Contains(keywords))
                {
                    this.AddDatatableNoteRow(dt, i);
                }
            }

            Program.Formmanager.FrmManageNotesNeedUpdate = true;
            this.dataGridViewNotes.DataSource = dt;
            this.SetDataGridViewColumsWidth();
        }

        /// <summary>
        /// Searching in notes stopped, show all notes again.
        /// </summary>
        private void searchTextBoxNotes_SearchStop()
        {
            this.DrawNotesGrid();
            this.SetDataGridViewColumsWidth();
        }

        /// <summary>
        /// The mouse leaves the datagridviewnotes control,
        /// hide the tooltip with note content preview.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void dataGridViewNotes_MouseLeave(object sender, EventArgs e)
        {
            this.toolTip.Hide(this);
        }

        #endregion Methods

#if windows
        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
        private struct SHFILEOPSTRUCT_x86
        {
            /// <summary>
            /// 
            /// </summary>
            public IntPtr hwnd;

            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public FileOperationType wFunc;

            /// <summary>
            /// 
            /// </summary>
            public string pFrom;

            /// <summary>
            /// 
            /// </summary>
            public string pTo;

            /// <summary>
            /// 
            /// </summary>
            public FileOperationFlags fFlags;

            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.Bool)]
            public bool fAnyOperationsAborted;

            /// <summary>
            /// 
            /// </summary>
            public IntPtr hNameMappings;

            /// <summary>
            /// 
            /// </summary>
            public string lpszProgressTitle;
        }

        /// <summary>
        /// 
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct SHFILEOPSTRUCT_x64
        {
            /// <summary>
            /// 
            /// </summary>
            public IntPtr hwnd;

            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.U4)]
            public FileOperationType wFunc;

            /// <summary>
            /// 
            /// </summary>
            public string pFrom;

            /// <summary>
            /// 
            /// </summary>
            public string pTo;

            /// <summary>
            /// 
            /// </summary>
            public FileOperationFlags fFlags;

            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.Bool)]
            public bool fAnyOperationsAborted;

            /// <summary>
            /// 
            /// </summary>
            public IntPtr hNameMappings;

            /// <summary>
            /// 
            /// </summary>
            public string lpszProgressTitle;
        }
#endif
    }
}