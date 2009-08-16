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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;

namespace SimplePlainNote
{
    /// <summary>
    /// Class to create new note.
    /// </summary>
    public partial class frmNewNote : Form
    {
		#region Fields (5) 

        private bool editmode = false;
        public const int HT_CAPTION = 0x2;
                private List<FrmNote> notes;
        private bool transparency = true;
        //if (Program.PLATFORM == "win32")
        //{
        public const int WM_NCLBUTTONDOWN = 0xA1;

		#endregion Fields 

		#region Constructors (1) 

        //}
        public frmNewNote()
        {
            InitializeComponent();
            notes = new List<FrmNote>();
            loadNotes();
        }

		#endregion Constructors 

		#region Properties (1) 

                public List<FrmNote> GetNotes
        {
            get { return this.notes; }
        }

		#endregion Properties 

		#region Methods (24) 

		// Public Methods (5) 

        public void CreateDefaultNote(string title, string content, int notecolor)
        {
            try
            {
                int newid = notes.Count + 1;
                string notefilenm = SaveNoteDefault(newid, title, content);
                if (String.IsNullOrEmpty(notefilenm)) { return; }
                FrmNote newnote = new FrmNote(newid, title, content, notecolor);
                notes.Add(newnote);
                newnote.Show();
            }
            catch (IndexOutOfRangeException indexexc)
            {
                MessageBox.Show("Fout: " + indexexc.Message);
            }
        }

        /// <summary>
        /// Create a note GUI.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="notecolor"></param>
        public void CreateNote(string visible, string title, string content, int notecolor, int locX, int locY, int notewith, int noteheight)
        {
            try
            {                
                int newid = notes.Count + 1;

                FrmNote newnote;
                if (visible == "true")
                {
                    newnote = new FrmNote(true, newid, title, content, notecolor, locX, locY, notewith, noteheight);
                    newnote.Show();
                }
                else
                {
                    newnote = new FrmNote(false, newid, title, content, notecolor, locX, locY, notewith, noteheight);
                }
                notes.Add(newnote);
                
            }
            catch (IndexOutOfRangeException indexexc)
            {
                MessageBox.Show("Fout: " + indexexc.Message);
            }
        }

        /// <summary>
        /// Edit a note
        /// </summary>
        /// <param name="noteID">id number</param>
        public void EditNote(int noteID)
        {
            int notePos = noteID - 1;
            try
            {
                this.tbTitle.Text = notes[notePos].Title;
                this.rtbNote.Text = notes[notePos].Note;
            }
            catch (ArgumentOutOfRangeException ExcID)
            {                
                MessageBox.Show("Note not found. "+ExcID.Source);
            }
            editmode = true;
        }

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

                        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
		// Private Methods (19) 

        private void btnAddNote_Click(object sender, EventArgs e)
        {
            skin Skin = getSkin();
            if (tbTitle.Text == "")
            {
                tbTitle.BackColor = Skin.getObjColor(false, false, true);
                tbTitle.Text = DateTime.Now.ToString();
            }
            else if (rtbNote.Text == "")
            {
                rtbNote.BackColor = Skin.getObjColor(false, false, true);
                rtbNote.Text = "Please type any text.";
            }
            else
            {
                if (editmode)
                {
                    //todo
                }
                else
                {
                    xmlHandler getSettings = new xmlHandler(true);
                    int notecolordefault = getSettings.getXMLnodeAsInt("defaultcolor");
                    CreateDefaultNote(tbTitle.Text, rtbNote.Text, notecolordefault);
                }
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
                CancelNote();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            CancelNote();
        }

        /// <summary>
        /// Redraw newnote.
        /// </summary>
        private void CancelNote()
        {            

            tbTitle.Text = "";
            rtbNote.Text = "";            
                       
            skin Skin = getSkin();
            Color normalcolor = Skin.getObjColor(false);            

            pnlNoteEdit.BackColor = normalcolor;
            rtbNote.BackColor = normalcolor;
            pnlHeadNewNote.BackColor = normalcolor;

            pnlNoteEdit.Refresh();
            rtbNote.Refresh();
            pnlHeadNewNote.Refresh();

            tbTitle.BackColor = Skin.getObjColor(true);
            tbTitle.Focus();            
        }

        private void createANewNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CancelNote(); 
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            this.Show();
        }

        private void editNote(int id)
        {
            try
            {
                this.tbTitle.Text = notes[id].Title;
                this.rtbNote.Text = notes[id].Note;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error code: 200 - "+exc.Message);                
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trayicon.Dispose();
            Application.Exit();
        }

        private void frmNewNote_Activated(object sender, EventArgs e)
        {
            if (transparency)
            {
                this.Opacity = 1.0;
                this.Refresh();
            }            
        }

        private void frmNewNote_Deactivate(object sender, EventArgs e)
        {
            if (transparency)
            {
                this.Opacity = 0.9;
                this.Refresh();
            }
        }

        private void frmNewNote_Shown(object sender, EventArgs e)
        {
            //MessageBox.Show("fired.");
            //redraw
            CancelNote();
        }

        private skin getSkin()
        {
            int numcolor = 0;
            xmlHandler getSettings = new xmlHandler(true);
            numcolor = Convert.ToInt32(getSettings.getXMLnode("defaultcolor"));
            skin getSkin = new skin(numcolor);
            return getSkin;
        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageNotes managenotes = new frmManageNotes(this, false);
            managenotes.Show();
            /*
            string allnotes ="";
            for (int i = 0; i < notes.Count; i++)
            {
                allnotes += notes[i].ID + " - " + notes[i].Title + " \r\n";
            }
            allnotes += "---------------------\r\nNumber notes: " + notes.Count;
            MessageBox.Show(allnotes);
            */
        }

        private void loadNotes()
        {
            xmlHandler getSettings = new xmlHandler(true);
            string notesavepath = getSettings.getXMLnode("notesavepath");

            int id = 1;

            string curnotefile = notesavepath+id+".xml";

            while (File.Exists(@curnotefile) == true)
            {
                xmlHandler parserNote = new xmlHandler(false, id+".xml");
                string visible = parserNote.getXMLnode("visible");
                string title = parserNote.getXMLnode("title");
                string content = parserNote.getXMLnode("content");

                int notecolor = parserNote.getXMLnodeAsInt("color");

                int noteLocX = parserNote.getXMLnodeAsInt("x");
                int noteLocY = parserNote.getXMLnodeAsInt("y");
                int notewidth = parserNote.getXMLnodeAsInt("width");
                int noteheight = parserNote.getXMLnodeAsInt("heigth");
                CreateNote(visible, title, content, notecolor, noteLocX, noteLocY, notewidth, noteheight);
                
                id++;
                curnotefile = notesavepath + id + ".xml";
                if (id > 1000) { MessageBox.Show("Error: Too many notes"); return; }
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

        private void pnlHeadNewNote_MouseDown(object sender, MouseEventArgs e)
        {
            skin Skin = getSkin();
            pnlHeadNewNote.BackColor = Skin.getObjColor(true); 
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                pnlHeadNewNote.BackColor = Skin.getObjColor(false);
            }
        }

        private void rtbNote_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "Are you sure you want to visted: " + e.LinkText, "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(e.LinkText);
            }
        }

        /// <summary>
        /// Save the note to xml file
        /// </summary>
        /// <param name="id">number</param>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <returns>filepath of the created note.</returns>
        private string SaveNoteDefault(int id, string title, string text)
        {
            xmlHandler getXmlSettings = new xmlHandler(true);            
            string notefile = id + ".xml";
            xmlHandler xmlnote = new xmlHandler(false, notefile);
            
            string defaultcolor = getXmlSettings.getXMLnode("defaultcolor");
            if (xmlnote.WriteNote(true,defaultcolor, title, text, 10, 10, 240, 240) == false)
                {
                    MessageBox.Show("Error writing note.");
                    return null;
                }         
            return notefile;            
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSettings settings = new frmSettings();
            settings.Show();
        }

        private void tbTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                rtbNote.Focus();
            }
        }

        private void Trayicon_Click(object sender, EventArgs e)
        {
            //todo, make this configurable what the action is.
        }

		#endregion Methods 

        #region highlight controls
        private void tbTitle_Enter(object sender, EventArgs e)
        {
            skin Skin = getSkin();
            tbTitle.BackColor = Skin.getObjColor(false, true, false);
        }
        private void tbTitle_Leave(object sender, EventArgs e)
        {
            skin Skin = getSkin();
            tbTitle.BackColor = Skin.getObjColor(false);
        }
        private void rtbNote_Enter(object sender, EventArgs e)
        {
            skin Skin = getSkin();
            rtbNote.BackColor = Skin.getObjColor(false, true, false);
        }
        private void rtbNote_Leave(object sender, EventArgs e)
        {
            skin Skin = getSkin();
            rtbNote.BackColor = Skin.getObjColor(false);
        }
        #endregion
            }
}
