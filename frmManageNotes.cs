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
#define win32

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;

namespace SimplePlainNote
{
    /// <summary>
    /// Manage notes class
    /// </summary>
    public partial class frmManageNotes : Form
    {
		#region Fields (5) 
        //list of notes
        private Notes notes;
        //skin colors etc.
        private Skin skin;
        //is transparent
        private bool transparency = false;
        //flag is redraw is busy
        private bool redrawbusy = false;
        #if win32
        //for moving
        public const int HT_CAPTION = 0x2;
        //for moving
        public const int WM_NCLBUTTONDOWN = 0xA1;
        #endif
		#endregion Fields 

		#region Constructors (1) 

        /// <summary>
        /// New instance of frmManageNotes
        /// </summary>
        /// <param name="fcn"></param>        
        public frmManageNotes(Notes notes, bool transparency, int notecolor)
        {
            InitializeComponent();

            skin = new Skin(notecolor);

            this.notes = notes;
            this.transparency = transparency;            
            DrawNotesOverview();
        }

		#endregion Constructors 

		#region Methods (12) 
        #if win32
        //for moving form 
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        //for moving form 
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);		
        #endif

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
            int numbernotes = notes.NumNotes;
            if (numbernotes != 0)
            {                
                for (int i = 1; i <= numbernotes; i++)
                {
                    if (btn.Name == "btnNoteDel"+i)
                    {
                        int noteid = i - 1;
                        notes.GetNotes[noteid].Close();

                        try
                        {                            
                            File.Delete(Path.Combine(getNotesSavePath(), Convert.ToString(i) + ".xml"));
                            
                            //reorder filenames
                            for (int n = i; n < numbernotes; n++)
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
                            MessageBox.Show("Access denied. Delete note "+i+".xml manualy with premission.");
                        }                        
                        
                        DrawNotesOverview();
                        Thread.Sleep(100);
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
            if ((n <= notes.NumNotes) && (n>=0))
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
        }
        
        private void DrawNotesOverview()
        {
            pnlNotes.Controls.Clear();
            int ypos = 10;            
            for (int curnote = 0; curnote < notes.NumNotes; curnote++)
            {
                Label lblNoteTitle = new Label();
                int titlelength = notes.GetNotes[curnote].NoteTitle.Length;
                if (titlelength >= 20)
                {
                    lblNoteTitle.Text = notes.GetNotes[curnote].NoteTitle.Substring(0, 20) + " (ID:" + notes.GetNotes[curnote].NoteID + ")";
                }
                else
                {
                    lblNoteTitle.Text = notes.GetNotes[curnote].NoteTitle + " (ID:" + notes.GetNotes[curnote].NoteID + ")";
                }

                lblNoteTitle.Name = "lbNote"+Convert.ToString(curnote+1);
                lblNoteTitle.Location = new Point(2, ypos);
                lblNoteTitle.Size = new Size(199, 16);
                pnlNotes.Controls.Add(lblNoteTitle);
                                
                CheckBox cbxNoteVisible = new CheckBox();
                cbxNoteVisible.Text = "visible";
                cbxNoteVisible.Name = Convert.ToString(curnote+1);
                
                if (notes.GetNotes[curnote].Visible == true)
                {
                    cbxNoteVisible.CheckState = CheckState.Checked;
                }
                else
                {
                    cbxNoteVisible.CheckState = CheckState.Unchecked;
                }
                cbxNoteVisible.Location = new Point(201, ypos);
                cbxNoteVisible.AutoEllipsis = true;
                cbxNoteVisible.AutoSize = true;
                cbxNoteVisible.Click += new EventHandler(cbxNoteVisible_Click);
                pnlNotes.Controls.Add(cbxNoteVisible);

                Button btnNoteDelete = new Button();
                btnNoteDelete.Text = "delete";
                btnNoteDelete.Name = "btnNoteDel" + Convert.ToString(curnote+1);
                btnNoteDelete.BackColor = Color.Orange;
                btnNoteDelete.Location = new Point(260, ypos);
                btnNoteDelete.Width = 60;
                btnNoteDelete.Click += new EventHandler(btnNoteDelete_Click);

                pnlNotes.Controls.Add(btnNoteDelete);
                
                ypos = ypos + 30;
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

        #endregion Methods
    }
}
