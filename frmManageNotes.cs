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
        //private List<frmNote> notes;
        //counted notes
        private int numnotes = 0;
        //is transparent
        private bool transparency = false;
        //for moving
        public const int HT_CAPTION = 0x2;
        //for moving
        public const int WM_NCLBUTTONDOWN = 0xA1;

		#endregion Fields 

		#region Constructors (1) 

        /// <summary>
        /// New instance of frmManageNotes
        /// </summary>
        /// <param name="fcn"></param>        
        public frmManageNotes()
        {
            InitializeComponent();            
            // DrawNotesOverview();
            transparency = getTransparency();
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
            if (numnotes != 0)
            {
                for (int i = 1; i <= numnotes; i++)
                {
                    if (btn.Name == "btnNoteDel"+i)
                    {
                        try
                        {                            
                            File.Delete(getNotesSavePath() + Convert.ToString(i) + ".xml");                            
                        }
                        catch (FileNotFoundException)
                        {
                            MessageBox.Show("Note is already gone.");
                        }
                        catch (UnauthorizedAccessException)
                        {
                            MessageBox.Show("Access denied. Delete note "+i+".xml manualy with premission.");
                        }
                        //notes.RemoveAt(i - 1);
                        // DrawNotesOverview();
                        numnotes--;                                             
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
            if ((n <= numnotes) && (n>=0))
            {
                //notes[n].NoteVisible = !notes[n].NoteVisible;
            }
        }

        /*
        private void DrawNotesOverview()
        {
            pnlNotes.Controls.Clear();
            int ypos = 10;
            //numnotes = notes.Count;
            for (int curnote = 0; curnote < numnotes; curnote++)
            {
                Label lblNoteTitle = new Label();
                lblNoteTitle.Text = notes[curnote].Title;
                lblNoteTitle.Name = "lbNote"+Convert.ToString(curnote+1);
                lblNoteTitle.Location = new Point(10, ypos);
                pnlNotes.Controls.Add(lblNoteTitle);
                                
                CheckBox cbxNoteVisible = new CheckBox();
                cbxNoteVisible.Text = "visible";
                cbxNoteVisible.Name = Convert.ToString(curnote+1);
                
                if (notes[curnote].NoteVisible == true)
                {
                    cbxNoteVisible.CheckState = CheckState.Checked;
                }
                else
                {
                    cbxNoteVisible.CheckState = CheckState.Unchecked;
                }
                cbxNoteVisible.Location = new Point(175, ypos);
                cbxNoteVisible.AutoEllipsis = true;
                cbxNoteVisible.AutoSize = true;
                cbxNoteVisible.Click += new EventHandler(cbxNoteVisible_Click);
                pnlNotes.Controls.Add(cbxNoteVisible);

                Button btnNoteDelete = new Button();
                btnNoteDelete.Text = "delete";
                btnNoteDelete.Name = "btnNoteDel" + Convert.ToString(curnote+1);
                btnNoteDelete.BackColor = Color.Orange;
                btnNoteDelete.Location = new Point(240, ypos);
                btnNoteDelete.Width = 60;
                btnNoteDelete.Click += new EventHandler(btnNoteDelete_Click);

                pnlNotes.Controls.Add(btnNoteDelete);
                
                ypos = ypos + 30;
            }
        }
         */

        private void frmManageNotes_Activated(object sender, EventArgs e)
        {
            if (transparency)
            {
                this.Opacity = 1.0;
                this.Refresh();
            }
        }

        /// <summary>
        /// form not active make tranparent if needed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmManageNotes_Deactivate(object sender, EventArgs e)
        {
            if (transparency)
            {
                this.Opacity = 0.9;
                this.Refresh();
            }
            Thread.Sleep(20);
        }

        private string getNotesSavePath()
        {
            xmlHandler xmlsettings = new xmlHandler(true);
            return xmlsettings.getXMLnode("notesavepath");
        }

        private bool getTransparency()
        {
            xmlHandler xmlSettings = new xmlHandler(true);
            if (xmlSettings.getXMLnode("transparecy") == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
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
                //ReleaseCapture();
                //SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                pnlHead.BackColor = Color.Orange;
            }
        }

		#endregion Methods 
    }
}
