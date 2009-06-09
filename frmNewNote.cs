using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SimplePlainNote
{
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
        public List<frmNote> getNotes
        {
            get { return this.notes; }
        }
        /*
        public String FormState
        {
            get { return this.WindowState; }
        }
         * */

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
                //Console.Beep();
                rtbNote.Text = "Please type any text.";
            }
            else
            {
                if (editmode)
                {

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
            this.tbTitle.Text = notes[id].Title;
            this.rtbNote.Text = notes[id].Note;
        }

        private void tbTitle_Enter(object sender, EventArgs e)
        {
            if (tbTitle.Focused)
            {
                tbTitle.BackColor = Color.LightYellow;
                rtbNote.BackColor = Color.Gold;
            }
        }

        private void rtbNote_Enter(object sender, EventArgs e)
        {
            if (rtbNote.Focused)
            {
                rtbNote.BackColor = Color.LightYellow;
                tbTitle.BackColor = Color.Gold;
            }
        }

        private void tbTitle_Leave(object sender, EventArgs e)
        {
            tbTitle.BackColor = Color.Gold;
        }

        private void Trayicon_Click(object sender, EventArgs e)
        {
            //todo
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
        }

        public void CreateNote(string title, string text)
        {
            try
            {
                int newid = notes.Count + 1;
                frmNote frmNote = new frmNote(newid, title, text, this);
                notes.Add(frmNote);
                frmNote.Show();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: creating note. \r\n" + exc.Message);
            }
        }

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
                notes[n].ID = n - 1;
            }
             */
        }

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
            tbTitle.Focus();
            tbTitle.BackColor = Color.LightYellow;
            rtbNote.BackColor = Color.Gold;
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
    }
}
