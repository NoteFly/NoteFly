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
        #region Fields ()
        /// <summary>
        /// flag is redraw is busy
        /// </summary>
        private bool redrawbusy = false;

        /// <summary>
        /// value indicating wether this form is moving.
        /// </summary>
        private bool moving = false;

        /// <summary>
        /// notes
        /// </summary>
        private Notes notes;

        /// <summary>
        /// Delta point
        /// </summary>
        private Point oldp;

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

            this.SetDataGridViewColumsWidth();
            this.notes = notes;
            this.DrawNotesGrid();
        }

        #endregion Constructors

        #region Methods (10)

        // Private Methods (10) 

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
        /// Sets every colom of the datagridview to a reasonable width.
        /// </summary>
        private void SetDataGridViewColumsWidth()
        {
            if (this.dataGridView1.Width <= 0) { return; }
            const int colidfixedwidth = 30;
            int partunit = ((this.dataGridView1.Width - colidfixedwidth) / 10);
            this.dataGridView1.Columns["colid"].Width = colidfixedwidth;
            this.dataGridView1.Columns["coltitle"].Width = 5 * partunit;
            this.dataGridView1.Columns["colvisible"].Width = 2 * partunit;
            this.dataGridView1.Columns["colskin"].Width = 3 * partunit;
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
                int nr = Convert.ToInt32(selrows[r].Cells["nr"]);
                string filename = this.notes.GetNote(nr).Filename;
                try
                {
                    string filepath = Path.Combine(Settings.NotesSavepath, filename);
                    File.Delete(filepath);
                    if (Settings.ProgramLogInfo)
                    {
                        Log.Write(LogType.info, filepath + " deleted.");
                    }
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
                //deletedids[r] = id;
                this.notes.RemoveNote(nr);
            }

            string[] files = Directory.GetFiles(Settings.NotesSavepath, "*.nfn");
            //reorder filenames
            for (int i = 0; i < deletedids.Length; i++)
            {
            }

            //        //reorder filenames
            //        for (int id = noteid; id < this.notes.CountNotes; id++)
            //        {
            //            string orgfile = Path.Combine(this.GetNotesSavePath(), Convert.ToString(id + 1) + ".xml");
            //            string newfile = Path.Combine(this.GetNotesSavePath(), Convert.ToString(id) + ".xml");
            //            if (!File.Exists(newfile))
            //            {
            //                File.Move(orgfile, newfile);
            //            }
            //            try
            //            {
            //                //this.notes.GetNotes[id].NoteID = Convert.ToInt16(id);
            //            }
            //            catch
            //            {
            //            }
            //        }
            //    }
            //    catch (FileNotFoundException filenotfoundexc)
            //    {
            //        throw new CustomException(filenotfoundexc.Message);
            //    }
            //    catch (UnauthorizedAccessException)
            //    {
            //        string msgaccessdenied = "Access denied. Delete note " + noteid + ".xml manually with proper premission.";
            //        Log.Write(LogType.error, msgaccessdenied);
            //        MessageBox.Show(msgaccessdenied);
            //    }
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
            }
        }

        /// <summary>
        /// Set a note visible or unvisible
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void cbxNoteVisible_Click(object sender, EventArgs e)
        {
            CheckBox cbx = (CheckBox)sender;
            int noteid = Convert.ToInt32(cbx.Name);
            if ((noteid <= this.notes.CountNotes) && (noteid >= 0))
            {
                if (this.notes.GetNote(noteid).Visible)
                {
                    this.notes.GetNote(noteid).DestroyForm(); //sets visible false
                }
                else
                {
                    this.notes.GetNote(noteid).CreateForm(); //sets visible true
                }
            }
            else
            {
                throw new CustomException("Note not found. Looking for noteid:" + noteid);
            }
        }

        /// <summary>
        /// Draw a list of all notes.
        /// </summary>
        private void DrawNotesGrid()
        {
            for (int i = 0; i < this.notes.CountNotes; i++)
            {
                string visible = "False";
                if (this.notes.GetNote(i).Visible)
                {
                    visible = "True";
                }
                string[] rowArray = new string[] { (i+1).ToString(), this.notes.GetNote(i).Title, visible, this.notes.GetNote(i).SkinNr.ToString() };
                //dr["ID"] = this.notes.GetNote(id).Id;
                //dr["Title"] = this.notes.GetNote(id).Title;
                //dr["Showed"] = this.notes.GetNote(id).Visible;
                //dr["Color"] = this.notes.GetNote(id).SkinNr;
                this.dataGridView1.Rows.Add(rowArray[0]);
            }
        }

        /// <summary>
        /// Limit the title.
        /// </summary>
        /// <param name="curnote">The note id.</param>
        /// <param name="newlentitle">The maximum lenght to limit the title to.</param>
        /// <returns>A shorter title.</returns>
        //private string ShortenTitle(int curnote, int newlentitle)
        //{
        //    int reallen = this.notes.GetNotes[curnote].NoteTitle.Length;
        //    if (newlentitle < 4)
        //    {
        //        return this.notes.GetNotes[curnote].NoteTitle.Substring(0, 4) + ".. (ID:" + this.notes.GetNotes[curnote].NoteID + ")";
        //    }
        //    else if (reallen > newlentitle)
        //    {
        //        return this.notes.GetNotes[curnote].NoteTitle.Substring(0, newlentitle) + ".. (ID:" + this.notes.GetNotes[curnote].NoteID + ")";
        //    }
        //    else
        //    {
        //        return this.notes.GetNotes[curnote].NoteTitle + "(ID:" + this.notes.GetNotes[curnote].NoteID + ")";
        //    }
        //}

        /// <summary>
        /// FrmManageNotes is activated.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void frmManageNotes_Activated(object sender, EventArgs e)
        {
            if (Settings.NotesTransparencyEnabled)
            {
                this.Opacity = 1.0;
                this.Refresh();
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
                    this.Opacity = (double)Settings.NotesTransparencyLevel;
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
                this.moving = true;
                this.pnlHead.BackColor = Color.OrangeRed;
                this.oldp = e.Location;
            }
        }

        /// <summary>
        /// Timer updated the list of notes.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void timerUpdateNotesList_Tick(object sender, EventArgs e)
        {
            if (!this.redrawbusy)
            {
                this.redrawbusy = true;
                this.DrawNotesGrid();
                this.redrawbusy = false;
            }
        }

        /// <summary>
        /// Move note if pnlHead is being left clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlHead_MouseMove(object sender, MouseEventArgs e)
        {
            if ((this.moving) && (e.Button == MouseButtons.Left))
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
        /// End moving note.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlHead_MouseUp(object sender, MouseEventArgs e)
        {
            this.moving = false;
            //this.pnlHead.BackColor = Color.Orange;
        }

        #endregion Methods
    }
}
