using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private bool transparency = true;
        private bool editmode = false;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        
        private List<frmNote> notes;

        public frmNewNote()
        {
            InitializeComponent();
            notes = new List<frmNote>();
        }
        public List<frmNote> GetNotes
        {
            get { return this.notes; }
        }

        private void btnAddNote_Click(object sender, EventArgs e)
        {
            if (tbTitle.Text == "")
            {
                tbTitle.BackColor = Color.Red;
                tbTitle.Text = DateTime.Now.ToString();
            }
            else if (rtbNote.Text == "")
            {
                rtbNote.BackColor = Color.Red;             
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
                    CreateNote(tbTitle.Text, rtbNote.Text);
                }
                CancelNote();
            }
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

        #region highlight controls
        private void tbTitle_Enter(object sender, EventArgs e)
        {            
            tbTitle.BackColor = Color.LightYellow;
        }                
        private void tbTitle_Leave(object sender, EventArgs e)
        {
            tbTitle.BackColor = Color.Gold;
        }
        private void rtbNote_Enter(object sender, EventArgs e)
        {
            rtbNote.BackColor = Color.LightYellow;
        }
        private void rtbNote_Leave(object sender, EventArgs e)
        {
            rtbNote.BackColor = Color.Gold;
        }
        #endregion

        private void Trayicon_Click(object sender, EventArgs e)
        {
            //todo, make this configurable what the action is.
        }

        private void createANewNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trayicon.Dispose();
            Application.Exit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelNote();
        }

        private void CancelNote()
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            tbTitle.Text = "";
            rtbNote.Text = "";
            tbTitle.Focus();            
            tbTitle.BackColor = Color.LightYellow;
            rtbNote.BackColor = Color.Gold;
        }

        /// <summary>
        /// Create a new note interface.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="text"></param>
        public void CreateNote(string title, string text)
        {
            try
            {
                int newid = notes.Count + 1;
                string notefilenm = SaveNote(newid, title, text);
                if ((notefilenm == "") || (notefilenm == null)) { return; }
                frmNote frmNote = new frmNote(newid, title, text);
                notes.Add(frmNote);
                frmNote.Show();
            }
            catch (IndexOutOfRangeException indexexc)
            {
                MessageBox.Show("Fout: " + indexexc.Message);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Fout: " + exc.Message);
            }
        }

        /// <summary>
        /// Save the note to xml file
        /// </summary>
        /// <param name="id">number</param>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <returns>filepath of the created note.</returns>
        private string SaveNote(int id, string title, string text)
        {
            xmlHandler getXmlSettings = new xmlHandler(true, "settings.xml");
            string defaultcolor = getXmlSettings.getXMLnode("defaultcolor");
            string notefile = id + ".xml";
            xmlHandler xmlnote = new xmlHandler(false, notefile);
            if (xmlnote.WriteNote(defaultcolor, title, text) == false)
                {
                    MessageBox.Show("Error writing note.");
                    return null;
                }            
            return notefile;
            
        }

        /// <summary>
        /// Verwijder een note uit de lijst met noten.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteNote(int id)
        {
            int m = 0;
            for (int i = 0; i < notes.Count; i++)
            {
                if (id == notes[i].ID)
                {
                    notes.RemoveAt(i);
                    m = i;
                    break;
                }
            }
            /*
            for (int n=m+1; n <= notes.Count; n++)
            {
                if (n>=1)
                {
                    notes[n].ID = n - 1;
                }
            }
            */
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

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageNotes managenotes = new frmManageNotes(this);
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

        private void tbTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                rtbNote.Focus();
            }
        }

        private void pnlHeadNewNote_MouseDown(object sender, MouseEventArgs e)
        {
            pnlHeadNewNote.BackColor = Color.Orange;
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                pnlHeadNewNote.BackColor = Color.Gold;
            }
        }

        private void frmNewNote_Shown(object sender, EventArgs e)
        {
            CancelNote();
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

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSettings settings = new frmSettings();
            settings.Show();
        }

        private void rtbNote_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            DialogResult result = MessageBox.Show(this, "Are you sure you want to visted: " + e.LinkText, "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(e.LinkText);
            }
        }

        private void frmNewNote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (tbTitle.Focused == true) { MessageBox.Show("Test1"); }
                else if (rtbNote.Focused == true) { MessageBox.Show("Test2"); }                
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
    }
}
