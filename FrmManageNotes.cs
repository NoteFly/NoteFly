/* Copyright (C) 2009
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace SimplePlainNote
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
                for (Int16 curnote = 1; curnote <= numbernotes; curnote++)
                {
                    if (btn.Name == "btnNoteDel" + curnote)
                    {
                        if (curnote - 1 < 0)
                        {
                            throw new Exception("noteid cannot be negative.");
                        }
                        int noteid = Convert.ToInt32(curnote) - 1;
                        notes.GetNotes[noteid].Close();

                        try
                        {
                            File.Delete(Path.Combine(getNotesSavePath(), Convert.ToString(curnote) + ".xml"));

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
                        catch (FileNotFoundException)
                        {
                            MessageBox.Show("Note is already gone.");
                        }
                        catch (UnauthorizedAccessException)
                        {
                            MessageBox.Show("Access denied. Delete note " + curnote + ".xml manualy with premission.");
                        }

                        DrawNotesOverview();
                        //Thread.Sleep(50);
                    }
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
                }
                else
                {
                    notes.GetNotes[n].Show();
                }
            }
            else
            {
                throw new CustomExceptions("note not found.");
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
            for (UInt16 curnote = 0; curnote < notes.NumNotes; curnote++)
            {
                Label lblNoteTitle = new Label();
                CheckBox cbxNoteVisible = new CheckBox();
                Button btnNoteDelete = new Button();

                int titlelength = notes.GetNotes[curnote].NoteTitle.Length;                
                lblNoteTitle.AutoSize = true;
                if ((titlelength >= 20) && (this.Width < 400))
                {
                    lblNoteTitle.Text = ShortenTitle(curnote, 20);
                }
                else if ((titlelength >= 30) && (this.Width >= 400) && (this.Width < 450))
                {
                    lblNoteTitle.Text = ShortenTitle(curnote, 30);
                }
                else if ((titlelength >= 40) && (this.Width >= 400) && (this.Width < 450))
                {
                    lblNoteTitle.Text = ShortenTitle(curnote, 40);
                }
                else if ((titlelength >= 50) && (this.Width >= 450) && (this.Width < 500))
                {
                    lblNoteTitle.Text = ShortenTitle(curnote, 50);
                }
                else if ((titlelength >= 60) && (this.Width >= 500) && (this.Width < 550))
                {
                    lblNoteTitle.Text = ShortenTitle(curnote, 60);
                }
                else if ((titlelength >= 70) && (this.Width >= 550) && (this.Width < 600))
                {
                    lblNoteTitle.Text = ShortenTitle(curnote, 70);
                }
                else if ((titlelength >= 80) && (this.Width >= 600))
                {
                    lblNoteTitle.Text = ShortenTitle(curnote, 80);
                }
                else
                {
                    lblNoteTitle.Text = notes.GetNotes[curnote].NoteTitle + " (ID:" + notes.GetNotes[curnote].NoteID + ")";
                }

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
                cbxNoteVisible.Location = new Point(this.Width-200, ypos);
                cbxNoteVisible.AutoEllipsis = true;
                cbxNoteVisible.AutoSize = true;
                cbxNoteVisible.Click += new EventHandler(cbxNoteVisible_Click);
                cbxNoteVisible.Anchor = (AnchorStyles.Right | AnchorStyles.Top);

                btnNoteDelete.Text = "delete";
                btnNoteDelete.Name = "btnNoteDel" + Convert.ToString(curnote + 1);
                btnNoteDelete.BackColor = Color.Orange;
                btnNoteDelete.Location = new Point(this.Width-90, ypos-3);
                btnNoteDelete.Width = 60;
                btnNoteDelete.Click += new EventHandler(btnNoteDelete_Click);
                btnNoteDelete.Anchor = (AnchorStyles.Right | AnchorStyles.Top);

                pnlNotes.Controls.Add(lblNoteTitle);
                pnlNotes.Controls.Add(cbxNoteVisible);
                pnlNotes.Controls.Add(btnNoteDelete);

                ypos = ypos + 30;
            }
        }

        private string ShortenTitle(int curnote, int lengte)
        {
            return notes.GetNotes[curnote].NoteTitle.Substring(0, lengte) + ".. (ID:" + notes.GetNotes[curnote].NoteID + ")";
        }

        private void CleanUp()
        {
            int ctrlnum = pnlNotes.Controls.Count;
            for (int i = 0; i < ctrlnum; i++)
            {
                pnlNotes.Controls[i].Dispose();
            }
        }

        private void frmManageNotes_Activated(object sender, EventArgs e)
        {
            if ((transparency) && (this.skin != null))
            {
                this.Opacity = 1.0;
            }
        }

        /// <summary>
        /// form not active make tranparent if needed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmManageNotes_Deactivate(object sender, EventArgs e)
        {
            if ((transparency) && (this.skin != null))
            {
                this.Opacity = skin.getTransparencylevel();
                this.Refresh();
            }
        }

        private string getNotesSavePath()
        {
            xmlHandler xmlsettings = new xmlHandler(true);
            return xmlsettings.getXMLnode("notesavepath");
        }

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
