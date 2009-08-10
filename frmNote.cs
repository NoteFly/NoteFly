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
using System.Threading;

namespace SimplePlainNote
{
    public partial class FrmNote : Form
    {        
        private int id;
        private string title;
        private string note;
        private int notecolor = 0;
        private string twpass;
        private bool transparency = false;
        private bool notelock = false;
        private bool notevisible = true;
        private int locX;
        private int locY;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, 
                         int msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        #region constructor
        public FrmNote(bool visible, int id, string title, string note, int notecolor, int locX, int locY, int notewidth, int noteheight)
        {
            if (visible == true)
            {
                notevisible = true;
            }
            this.id = id;
            this.title = title;            
            this.note = note;
            this.transparency = getTransparency();
            this.notecolor = notecolor;

            if ((locX >= 0) && (locY >= 0))
            {
                this.locX = locX;
                this.locY = locY;
            }
            else
            {
                this.locX = 10;
                this.locY = 10;
            }

            if (notevisible == true)
            {
                InitializeComponent();

                lblTitle.Text = title;
                rtbNote.Text = note;

                SetSizeNote(notewidth, noteheight);
                SetPosNote();
                paintColorNote();
            }
            else
            {
                this.Close();
            }
        }

        public FrmNote(int id, string title, string note, int notecolor)
        {
            this.id = id;
            this.title = title;
            this.note = note;
            this.transparency = getTransparency();
            this.notecolor = notecolor;
            //set default location note
            this.locX = 10;
            this.locY = 10;
            //set width and height to default
            this.Width = 240;
            this.Height = 240;
            InitializeComponent();

            lblTitle.Text = title;
            rtbNote.Text = note;

            paintColorNote();
            SetPosNote();
        }
        #endregion

        #region properties
        public int ID
        {
            get { return id; }
            set { this.id = value; }
        }
        public string Title
        {
            get { return this.title; }
        }
        public int ColorNote
        {
            get
            {
                return this.notecolor;
            }
            set
            {
                value = notecolor;
            }
        }
        public string Note
        {
            get { return this.note; }
            set
            {
                note = value;
                rtbNote.Text = note;
            }
        }
        public bool NoteVisible
        {
            get
            {
                return this.notevisible; 
            }
            set
            {
                notevisible = value;                
            }
        }
        #endregion


        private bool getTransparency()
        {
            xmlHandler xmlSettings = new xmlHandler(true, "settings.xml");
            if (xmlSettings.getXMLnode("transparecy") == "1")
            {
                return true;
            }
            else
            {
                return false;
            }            
        }

        private void frmDeleteNote_Click(object sender, EventArgs e)
        {
            transparency = false;
            this.notevisible = false;                      
            this.Close();            
        }

        private void pnlHead_MouseDown(object sender, MouseEventArgs e)
        {
            skin getskin = new skin(notecolor);
            pnlHead.BackColor = getskin.getObjColor(true);            

            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                this.locX = this.Location.X;
                this.locY = this.Location.Y;                
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                pnlHead.BackColor = getskin.getObjColor(false);                
            }

            SavePos.RunWorkerAsync();
        }

        /// <summary>
        /// Resize note
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlResizeWindow_MouseDown(object sender, MouseEventArgs e)
        {            
            Cursor = Cursors.SizeNWSE;            
            //e.Location
        }

        private void frmNote_Deactivate(object sender, EventArgs e)
        {
            if (transparency)
            {
                this.Opacity = 0.9;
                this.Refresh();
            }
        }

        private void frmNote_Activated(object sender, EventArgs e)
        {
            if (transparency)
            {
                this.Opacity = 1.0;
                this.Refresh();
            }
        }

        private void setColorNote(object sender, EventArgs e)
        {
            int i =0;
            foreach (ToolStripMenuItem curitem in menuNoteColors.DropDownItems)
            {                
                if (curitem == sender) 
                {
                    curitem.Checked = true;
                    notecolor = i;
                    xmlHandler savenotecolor = new xmlHandler(false, this.id + ".xml");
                    savenotecolor.WriteNote(notevisible, Convert.ToString(notecolor), this.title, this.note, this.locX, this.locY, this.Width, this.Height);                    
                }
                else
                {
                    curitem.Checked = false;
                }
                i++;
            }

            paintColorNote();
        }

        /// <summary>
        /// Get the color of the note and paint it.
        /// </summary>
        private void paintColorNote()
        {
            skin getskin = new skin(notecolor);
            Color normalcolor = getskin.getObjColor(false);            

            this.BackColor = normalcolor;
            this.pnlHead.BackColor = normalcolor;
            this.pnlNote.BackColor = normalcolor;
            this.rtbNote.BackColor = normalcolor;
        }

        private void updateMenuNoteColor(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem curitem in menuNoteColors.DropDownItems)
            {                
                curitem.Checked = false;
            }

            switch (this.notecolor)
            {
                case 0:
                    yellowToolStripMenuItem.Checked = true;
                    break;
                case 1:
                    orangeToolStripMenuItem.Checked = true;
                    break;
                case 2:
                    whiteToolStripMenuItem.Checked = true;
                    break;
                case 3:
                    greenToolStripMenuItem.Checked = true;
                    break;
                case 4:
                    blueToolStripMenuItem.Checked = true;
                    break;
                case 5:
                    purpleToolStripMenuItem.Checked = true;                    
                    break;
                case 6:
                    redToolStripMenuItem.Checked = true;                    
                    break;                
            }
        }

        private void editTToolStripMenuItem_Click(object sender, EventArgs e)
        {                                                                      
            base.WindowState = FormWindowState.Normal;
            //base.EditNote(ID);
        }

        private void pbResizeGrip_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!notelock)
                {
                    this.Cursor = Cursors.SizeNWSE;
                    this.Size = new Size(this.PointToClient(MousePosition).X, this.PointToClient(MousePosition).Y);                    
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void TwitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsConnectedToInternet())
            {
                if ((String.IsNullOrEmpty(note) ==false) && (note.Length < 140))
                {
                    tweetnote();
                }
                else if (note.Length >= 140)
                {
                    DialogResult result;
                    string shrttweet = note.Substring(0, 140);
                    result = MessageBox.Show("Your note is more than the 140 chars. Do you want to publish only the first part? " + shrttweet, "too long note", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        tweetnote();
                    }
                }
                else
                {
                    MessageBox.Show("Note is empty.");
                }
            }
            else
            {
                MessageBox.Show("No network connection.");
            }
        }

        private void tweetnote()
        {
            xmlHandler xmlSettings = new xmlHandler(false, "settings.xml");
            string twitteruser = xmlSettings.getXMLnode("twitteruser");
            string twitterpass = xmlSettings.getXMLnode("twitterpass");
            if ((twpass != "") && (twpass != null))
            {
                twitterpass = twpass;
            }

            if ((twitteruser == "") || (twitteruser == null))
            {
                MessageBox.Show("Error: Twitter settings not set.");
                return;
            }
            else if ((twitterpass == null) || (twitterpass == ""))
            {
                Form askpass = new Form();
                askpass.Height = 80;
                askpass.Width = 250;
                askpass.Text = "Twitter password needed";
                askpass.Show();
                TextBox tbpass = new TextBox();
                tbpass.Location = new Point(10, 10);
                tbpass.Width = 180;
                tbpass.Name = "tbPassword";
                Button btnOk = new Button();
                btnOk.Location = new Point(190, 10);
                btnOk.Text = "Ok";
                btnOk.Width = 50;
                btnOk.Name = "btnOk";
                btnOk.Click += askpassok;
                askpass.Controls.Add(tbpass);
                askpass.Controls.Add(btnOk);
            }
            else
            {
                if (xmlSettings.UpdateAsXML(twitteruser, twitterpass, note) != null)
                {
                    MessageBox.Show("Your note is Tweeted.");
                }
                else
                {
                    MessageBox.Show("Sending note to twitter failed.");
                }
            }
            
        }

        private void askpassok(object obj, EventArgs e)
        {
            Button btnobj = (Button)obj;
            Form frmAskpass = btnobj.FindForm();

            Control[] passctr = frmAskpass.Controls.Find("tbPassword", true);
            twpass = passctr[0].Text;
            MessageBox.Show(twpass);
            frmAskpass.Close();
            tweetnote();  
        }

        /// <summary>
        /// Check internet state.
        /// </summary>
        /// <returns></returns>
        public static bool IsConnectedToInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }

        private void frmNote_Shown(object sender, EventArgs e)
        {

            xmlHandler xmlSettings = new xmlHandler(false, "settings.xml");

            if (xmlSettings.getXMLnodeAsInt("syntaxhighlight") == 1)
            {
                

            }


            if (String.IsNullOrEmpty(xmlSettings.getXMLnode("twitteruser"))==true)
            {
                TwitterToolStripMenuItem.Enabled = false;
            }
            else
            {
                TwitterToolStripMenuItem.Enabled = true;
            }

        }

        private void OnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnTopToolStripMenuItem.Checked == true)
            {
                this.TopMost = true;
            }
            else
            {
                this.TopMost = false;
            }
        }

        private void copyTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(note);                
        }

        private void copyTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(title);
        }

        private void locknoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!notelock)
            {
                notelock = true;
                locknoteToolStripMenuItem.Text = "lock note (click again to unlock)";
                this.menuNoteColors.Enabled = false;
                this.editTToolStripMenuItem.Enabled = false;
                this.OnTopToolStripMenuItem.Enabled = false;                
            }
            else
            {
                notelock = false;
                locknoteToolStripMenuItem.Text = "lock note";
                this.menuNoteColors.Enabled = true;
                this.editTToolStripMenuItem.Enabled = true;
                this.OnTopToolStripMenuItem.Enabled = true;
            }
        }

        private void SetPosNote()
        {
            this.Location = new Point(locX, locY);            
        }

        private void SetSizeNote(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        /*
        /// <summary>
        /// Timer let's note settings save.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void timerSavePos_Tick(object sender, EventArgs e)
        {
            try
            {
                string numcolor = Convert.ToString(this.notecolor);
                if ((this.locX >= 0) && (this.locY >= 0))
                {
                    xmlHandler updateposnote = new xmlHandler(false, ID + ".xml");
                    updateposnote.WriteNote(numcolor, this.title, this.note, this.locX, this.locY, this.Width, this.Height);                    
                    timerSavePos.Enabled = false;
                    timerSavePos.Stop();                    
                }
                else
                {
                    MessageBox.Show("Error: note location out of screen.");
                }
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("internal error");
            }
            finally
            {
                timerSavePos.Enabled = false;
            }            
        }
         */

        private void pbResizeGrip_MouseUp(object sender, MouseEventArgs e)
        {
            if (!notelock)
            {                
                SavePos.RunWorkerAsync();
            }
        }

        private void emailNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string emailnote = "";

            if (Program.PLATFORM=="macx")
            {
                emailnote = note.Replace("\r", "%0D%0A");
            }
            else if (Program.PLATFORM == "win32")
            {
                emailnote = note.Replace("\r\n", "%0D%0A");
                emailnote = emailnote.Replace(".exe", "");
            }
            else if (Program.PLATFORM == "linux")
            {
                emailnote = note.Replace("\n", "%0D%0A");
            }
            //preventing a possible security issue here.
            emailnote = emailnote.Replace("\x00", "");

            xmlHandler xmlsettings = new xmlHandler(true, "settings.xml");
            string defaultemail = xmlsettings.getXMLnode("defaultemail");

            if (!String.IsNullOrEmpty(emailnote))
            {
                System.Diagnostics.Process.Start("mailto:\\" + defaultemail.Replace("\x00", "") + "?subject=" + title.Replace("\x00", "") + "&body=" + emailnote);
            }
            else if (!String.IsNullOrEmpty(title))
            {
                System.Diagnostics.Process.Start("mailto:\\" + defaultemail.Replace("\x00", "") + "?subject=" + title.Replace("\x00", ""));
            }
            else
            {
                MessageBox.Show("Error: note has no title and content");
            }
        }

        private void SavePos_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(50);
            //DateTime starttime = DateTime.Now;
            try
            {
                this.locX = this.Location.X;
                this.locY = this.Location.Y;
                string numcolor = Convert.ToString(this.notecolor);
                if ((this.locX >= 0) && (this.locY >= 0))
                {
                    xmlHandler updateposnote = new xmlHandler(false, ID + ".xml");
                    updateposnote.WriteNote(notevisible,numcolor, this.title, this.note, this.locX, this.locY, this.Width, this.Height);                    
                }
                else
                {
                    MessageBox.Show("Error: note location out of screen.");
                }
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("internal error");
            }
            //DateTime endtime = DateTime.Now;
            //TimeSpan debugtime = endtime - starttime;
            //MessageBox.Show("taken "+debugtime.Milliseconds);
        }

        private void contextMenuStripNoteOptions_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            skin getskin = new skin(notecolor);
            pnlHead.BackColor = getskin.getObjColor(false); 
        }

    }
}