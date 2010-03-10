/* Copyright (C) 2009-2010
 * 
 * This program is free software; you can redistribute it and/or modify it
 * Free Software Foundation; either version 2, or (at your option) any
 * later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA
 */
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NoteFly
{
    /// <summary>
    /// Manage notes class
    /// </summary>
    public partial class FrmManageNotes : Form
    {
        #region Fields (4)

        //list of notes
        private Notes notes;
        //flag is redraw is busy
        private bool redrawbusy = false;
        //skin colors etc.
        private Skin skin;
        //is transparent
        private bool transparency = false;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// New instance of frmManageNotes
        /// </summary>
        /// <param name="fcn"></param>        
        public FrmManageNotes(Notes notes, bool transparency, int notecolor)
        {
            InitializeComponent();
            skin = new Skin(notecolor);
            this.notes = notes;
            this.transparency = transparency;
            notes.NotesUpdated = false;
            DrawNotesOverview();
        }

        #endregion Constructors

        #region Methods (10)

        // Private Methods (10) 

        /// <summary>
        /// Close form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNoteDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Int16 numbernotes = notes.NumNotes;
            if (numbernotes != 0)
            {
                Int16 curnote = 0;
                try
                {
                    curnote = Convert.ToInt16(btn.Name.Substring(10, btn.Name.Length - 10));
                    //curnote = Convert.ToInt16(btn.Tag);
                }
                catch (InvalidCastException invexc)
                {
                    throw new CustomException(invexc.Message + " " + invexc.StackTrace);
                }

                if (btn.Name == "btnNoteDel" + curnote)
                {
                    int noteid = Convert.ToInt32(curnote) - 1;

                    xmlHandler settings = new xmlHandler(true);
                    if (settings.getXMLnodeAsBool("confirmdelete"))
                    {
                        DialogResult deleteres = MessageBox.Show("Are you sure you want to delete note (ID:" + noteid + ") ?", "delete note?", MessageBoxButtons.YesNo);
                        if (deleteres == DialogResult.No)
                        {
                            return;
                        }
                    }
                    notes.GetNotes[noteid].Close();

                    try
                    {
                        File.Delete(Path.Combine(getNotesSavePath(), Convert.ToString(curnote) + ".xml"));
                        Log.Write(LogType.info, Convert.ToString(curnote) + ".xml deleted.");

                        //reorder filenames
                        for (Int16 n = curnote; n < numbernotes; n++)
                        {
                            string orgfile = Path.Combine(getNotesSavePath(), Convert.ToString(n + 1) + ".xml");
                            string newfile = Path.Combine(getNotesSavePath(), Convert.ToString(n) + ".xml");
                            if (!File.Exists(newfile))
                            {
                                File.Move(orgfile, newfile);
                            }
                            if (n < numbernotes)
                            {
                                notes.GetNotes[n].NoteID = n;
                            }
                        }
                        notes.GetNotes.RemoveAt(noteid);
                    }
                    catch (FileNotFoundException filenotfoundexc)
                    {
                        throw new CustomException(filenotfoundexc.Message);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        String msgaccessdenied = "Access denied. Delete note " + curnote + ".xml manualy with proper premission.";
                        MessageBox.Show(msgaccessdenied);
                        Log.Write(LogType.error, msgaccessdenied);
                    }

                    DrawNotesOverview();
                }
                else
                {
                    throw new CustomException("Note to delete not found.");
                }
            }
        }

        /// <summary>
        /// Set a note visible or unvisible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void cbxNoteVisible_Click(object sender, EventArgs e)
        {
            CheckBox cbx = (CheckBox)sender;
            int n = Convert.ToInt32(cbx.Name) - 1;
            if ((n <= notes.NumNotes) && (n >= 0))
            {
                if (notes.GetNotes[n].Visible == true)
                {
                    notes.GetNotes[n].Hide();
                    notes.GetNotes[n].NoteVisible = false;
                }
                else
                {
                    notes.GetNotes[n].Show();
                    notes.GetNotes[n].NoteVisible = true;
                }
                notes.GetNotes[n].UpdateThisNote();
            }
            else
            {
                throw new CustomException("note not found.");
            }
        }

        /// <summary>
        /// Draw a list of all notes.
        /// </summary>
        private void DrawNotesOverview()
        {
            pnlNotes.Controls.Clear();
            CleanUp();

            int ypos = 10;
            int newlentitle = ((this.Width - 280) / 4);

            for (Int16 curnote = 0; curnote < notes.NumNotes; curnote++)
            {
                Label lblNoteTitle = new Label();
                CheckBox cbxNoteVisible = new CheckBox();
                Button btnNoteDelete = new Button();

                int titlelength = notes.GetNotes[curnote].NoteTitle.Length;
                lblNoteTitle.AutoSize = true;

                lblNoteTitle.Text = ShortenTitle(curnote, newlentitle);

                lblNoteTitle.Name = "lbNote" + Convert.ToString(curnote + 1);
                lblNoteTitle.Location = new Point(2, ypos);
                lblNoteTitle.Anchor = (AnchorStyles.Left | AnchorStyles.Top);

                cbxNoteVisible.Text = "visible";
                cbxNoteVisible.Name = Convert.ToString(curnote + 1);

                if (notes.GetNotes[curnote].Visible == true)
                {
                    cbxNoteVisible.CheckState = CheckState.Checked;
                }
                else
                {
                    cbxNoteVisible.CheckState = CheckState.Unchecked;
                }
                cbxNoteVisible.Location = new Point(this.Width - 200, ypos);
                cbxNoteVisible.AutoEllipsis = true;
                cbxNoteVisible.AutoSize = true;
                cbxNoteVisible.Click += new EventHandler(cbxNoteVisible_Click);
                cbxNoteVisible.Anchor = (AnchorStyles.Right | AnchorStyles.Top);

                btnNoteDelete.Text = "delete";
                btnNoteDelete.Name = "btnNoteDel" + Convert.ToString(curnote + 1);
                btnNoteDelete.BackColor = Color.Orange;
                btnNoteDelete.Location = new Point(this.Width - 90, ypos - 3);
                btnNoteDelete.Width = 60;
                btnNoteDelete.Click += new EventHandler(btnNoteDelete_Click);
                btnNoteDelete.Anchor = (AnchorStyles.Right | AnchorStyles.Top);
                btnNoteDelete.Tag = curnote;

                pnlNotes.Controls.Add(lblNoteTitle);
                pnlNotes.Controls.Add(cbxNoteVisible);
                pnlNotes.Controls.Add(btnNoteDelete);

                ypos += 30;
            }
        }

        /// <summary>
        /// Limit the title
        /// </summary>
        /// <param name="curnote"></param>
        /// <param name="lengte"></param>
        /// <returns></returns>
        private string ShortenTitle(int curnote, int newlentitle)
        {
            int reallen = notes.GetNotes[curnote].NoteTitle.Length;
            if (newlentitle < 4)
            {
                return notes.GetNotes[curnote].NoteTitle.Substring(0, 4) + ".. (ID:" + notes.GetNotes[curnote].NoteID + ")";
            }
            else if (reallen > newlentitle)
            {
                return notes.GetNotes[curnote].NoteTitle.Substring(0, newlentitle) + ".. (ID:" + notes.GetNotes[curnote].NoteID + ")";
            }
            else
            {
                return notes.GetNotes[curnote].NoteTitle + "(ID:" + notes.GetNotes[curnote].NoteID + ")";
            }
        }

        /// <summary>
        /// Dispose all children controls of pnlNotes control.
        /// </summary>
        private void CleanUp()
        {
            int ctrlnum = pnlNotes.Controls.Count;
            for (int i = 0; i < ctrlnum; i++)
            {
                pnlNotes.Controls[i].Dispose();
            }
        }

        /// <summary>
        /// frmManage notes is activated.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmManageNotes_Activated(object sender, EventArgs e)
        {
            if ((transparency) && (this.skin != null))
            {
                this.Opacity = 1.0;
            }
        }

        /// <summary>
        /// form not active, make tranparent if set.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmManageNotes_Deactivate(object sender, EventArgs e)
        {
            if ((transparency) && (this.skin != null))
            {
                this.Opacity = skin.GetTransparencylevel();
                this.Refresh();
            }
        }

        /// <summary>
        /// Get the full path of the note folder.
        /// </summary>
        /// <returns></returns>
        private string getNotesSavePath()
        {
            xmlHandler xmlsettings = new xmlHandler(true);
            return xmlsettings.getXMLnode("notesavepath");
        }

        /// <summary>
        /// The manage note form is beening resized.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlHead_MouseDown(object sender, MouseEventArgs e)
        {
            pnlHead.BackColor = Color.OrangeRed;
            if (e.Button == MouseButtons.Left)
            {
#if win32
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
#endif
                pnlHead.BackColor = Color.Orange;
            }
        }

        private void timerUpdateNotesList_Tick(object sender, EventArgs e)
        {
            if ((!redrawbusy) && (notes.NotesUpdated))
            {
                redrawbusy = true;
                DrawNotesOverview();
                redrawbusy = false;
                notes.NotesUpdated = false;
            }
        }

#if win32
        //for moving
        public const int HT_CAPTION = 0x2;
        //for moving
        public const int WM_NCLBUTTONDOWN = 0xA1;
        //for moving form 
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        //for moving form 
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);

        private void pbResizeGrip_MouseUp(object sender, MouseEventArgs e)
        {
            if (!redrawbusy)
            {
                redrawbusy = true;
                DrawNotesOverview();
                redrawbusy = false;
            }
        }
#endif
        #endregion Methods
    }
}
