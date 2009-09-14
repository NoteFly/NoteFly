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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SimplePlainNote
{
    public partial class frmNote : Form
    {
		#region Fields (12)  
        public Notes notes;
        private Skin skin;        
        private UInt16 id;
        private int locX;
        private int locY;
        private string note;
        private int notecolor = 0;
        private bool notelock = false;
        private bool notevisible = true;
        private string title;
        //private bool transparency = false;
        private string twpass;
        public const int HT_CAPTION = 0x2;
        public const int WM_NCLBUTTONDOWN = 0xA1;

		#endregion Fields 

		#region Constructors (2) 

        public frmNote(Notes notes, UInt16 id, bool visible, bool ontop, string title, string note, int notecolor, int locX, int locY, int notewidth, int noteheight)
        {
            this.notes = notes;
            this.skin = new Skin(notecolor);
            if (visible == true)
            {
                notevisible = true;
            }
            this.id = id;
            this.title = title;            
            this.note = note;            
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

            notes.NotesUpdated = true;

            if (notevisible == true)
            {
                InitializeComponent();

                lblTitle.Text = title;
                rtbNote.Text = note;

                SetSizeNote(notewidth, noteheight);
                SetPosNote();
                PaintColorNote();
                checkthings();
            }
            else
            {
                this.Hide();
            }

            if (ontop)
            {
                OnTopToolStripMenuItem.Checked = true;
                this.TopMost = true;
            }
            else
            {
                OnTopToolStripMenuItem.Checked = false;
                this.TopMost = false;
            }
        }

        public frmNote(Notes notes, UInt16 id, string title, string note, int notecolor)
        {
            this.skin = new Skin(notecolor);
            this.id = id;
            this.title = title;
            this.note = note;
            //this.transparency = transparency;
            this.notecolor = notecolor;            
            //set default location note
            this.locX = 10;
            this.locY = 10;
            //set width and height to default
            this.Width = 240;
            this.Height = 240;
            this.notes = notes;
            InitializeComponent();            
            lblTitle.Text = title;
            rtbNote.Text = note;

            PaintColorNote();
            SetPosNote();
            checkthings();
            notes.NotesUpdated = true;
        }

		#endregion Constructors 

		#region Properties (5) 
       
        public UInt16 NoteID
        {
            get { return id; }
            set { this.id = value; }
        }        

        public int NoteColor
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

        public string NoteTitle
        {
            get { return this.title; }
            set
            {
                title = value;
                lblTitle.Text = title;
            }
        }

        public string NoteContent
        {
            get { return this.note; }
            set
            {
                note = value;
                rtbNote.Text = note;                
            }
        }

		#endregion Properties 

		#region Methods (29) 
        
        
        #if win32
        /// <summary>
        /// Check internet state.
        /// </summary>
        /// <returns></returns>
        private static bool IsConnectedToInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }        
                
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int msg, int wParam, int lParam);

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        #endif

        /// <summary>
        /// Check if twitter is enabled and check Syntax.
        /// </summary>
        public void checkthings()
        {
            CheckTwitter(notes.TwitterEnabled);
            notes.CheckSyntax(notes.SyntaxHighlightEnabled, rtbNote);
        }

        /// <summary>
        /// check if twitter is enabled.
        /// </summary>
        /// <param name="twitterenabled"></param>
        private void CheckTwitter(bool twitterenabled)
        {
            if (twitterenabled)
            {
                TwitterToolStripMenuItem.Enabled = true;
            }
            else
            {
                TwitterToolStripMenuItem.Enabled = false;
            }
        }

        /// <summary>
        /// Find what password is entered.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        private void askpassok(object obj, EventArgs e)
        {
            Button btnobj = (Button)obj;
            Form frmAskpass = btnobj.FindForm();

            Control[] passctr = frmAskpass.Controls.Find("tbPassword", true);
            twpass = passctr[0].Text;            
            frmAskpass.Close();
            tweetnote();                         
        }

        private void contextMenuStripNoteOptions_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            if (skin != null)
            {
                pnlHead.BackColor = skin.getObjColor(false);
            }
        }

        /// <summary>
        /// copy note content to clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(note);                
        }

        /// <summary>
        /// copy title to clipboard.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(title);
        }

        /// <summary>
        /// Edit note
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
                     
            if (this.NoteID >  notes.NumNotes)
            {
                MessageBox.Show("Error: cannot find note.");
            }
            notes.EditNewNote(this.NoteID);            
             
        }

        /// <summary>
        /// Create an e-mail of a note.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void emailNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string emailnote = "";

            #if win32
            emailnote = note.Replace("\r\n", "%0D%0A");
            #elif mac
            emailnote = note.Replace("\r", "%0D%0A");
            #elif linux                        
            emailnote = note.Replace("\n", "%0D%0A");
            #endif                        

            xmlHandler xmlsettings = new xmlHandler(true);
            string defaultemail = xmlsettings.getXMLnode("defaultemail");

            if ((!String.IsNullOrEmpty(title)) && (String.IsNullOrEmpty(emailnote)))
            {
                System.Diagnostics.Process.Start("mailto:" + defaultemail + "?subject=" + title + "&body=" + emailnote);
            }
            else if (!String.IsNullOrEmpty(title))
            {
                System.Diagnostics.Process.Start("mailto:" + defaultemail + "?subject=" + title);
            }
            else
            {
                MessageBox.Show("Error: note has no title and content");
            }
        }

        /// <summary>
        /// Hide note
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCloseNote_Click(object sender, EventArgs e)
        {            
            this.notevisible = false;
            notes.NotesUpdated = true;
            this.Hide();
        }

        private void frmNote_Activated(object sender, EventArgs e)
        {
            if ((notes.Transparency) && (skin!=null))
            {
                this.Opacity = 1.0;                
            }
        }

        private void frmNote_Deactivate(object sender, EventArgs e)
        {
            if ((notes.Transparency) && (skin!=null))
            {
                this.Opacity = skin.getTransparencylevel();
                this.Refresh();
            }
        }    

        /// <summary>
        /// Lock a note
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Make a note on top
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            if (!notelock && !SavePos.IsBusy)
            {
                SavePos.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Get the color of the note and paint it.
        /// </summary>
        public void PaintColorNote()
        {                
            skin = new Skin(notecolor);
            Color normalcolor = skin.getObjColor(false);            

            this.BackColor = normalcolor;
            this.pnlHead.BackColor = normalcolor;
            this.pnlNote.BackColor = normalcolor;
            this.rtbNote.BackColor = normalcolor;

            rtbNote.Font = skin.getFontNoteContent();                        
        }

        /// <summary>
        /// Resize note
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Save resized note.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbResizeGrip_MouseUp(object sender, MouseEventArgs e)
        {
            if (!notelock && !SavePos.IsBusy)
            {                        
                SavePos.RunWorkerAsync();
            }
        }

        private void pnlHead_MouseDown(object sender, MouseEventArgs e)
        {
            if (skin != null)
            {
                pnlHead.BackColor = skin.getObjColor(true);
            }

            if (e.Button == MouseButtons.Left)
            {       
                #if win32
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                #endif

                if (skin != null)
                {
                    pnlHead.BackColor = skin.getObjColor(false);
                }                

                this.locX = this.Location.X;
                this.locY = this.Location.Y;                                                
            }
            
            if (SavePos.IsBusy == false)
            {
                SavePos.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Resize note
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlResizeWindow_MouseDown(object sender, MouseEventArgs e)
        {            
            Cursor = Cursors.SizeNWSE;            
        }

        private void SavePos_DoWork(object sender, DoWorkEventArgs e)
        {                      
            #if DEBUG
            DateTime starttime = DateTime.Now;
            #endif         

            try
            {
                this.locX = this.Location.X;
                this.locY = this.Location.Y;
                string numcolor = Convert.ToString(this.notecolor);
                if ((this.locX >= 0) && (this.locY >= 0))
                {
                    string notefile = System.IO.Path.Combine(notes.NoteSavePath, this.id + ".xml");
                    xmlHandler updateposnote = new xmlHandler(notefile);                    
                    updateposnote.WriteNote(notevisible,this.TopMost,numcolor, this.title, this.note, this.locX, this.locY, this.Width, this.Height);
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

            #if DEBUG
            DateTime endtime = DateTime.Now;
            TimeSpan debugtime = endtime - starttime;
            MessageBox.Show("taken: "+debugtime.Milliseconds+" ms\r\n "+debugtime.Ticks+" ticks");            
            #endif

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
                    string notefile = System.IO.Path.Combine(notes.NoteSavePath, this.id + ".xml");
                    xmlHandler savenotecolor = new xmlHandler(notefile);
                    savenotecolor.WriteNote(notevisible, OnTopToolStripMenuItem.Checked, Convert.ToString(notecolor), this.title, this.note, this.locX, this.locY, this.Width, this.Height);                    
                }
                else
                {
                    curitem.Checked = false;
                }
                i++;
            }

            PaintColorNote();
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

        private void tweetnote()
        {
            xmlHandler xmlSettings = new xmlHandler(true);
            string twitteruser = xmlSettings.getXMLnode("twitteruser");
            string twitterpass = xmlSettings.getXMLnode("twitterpass");
            if ((twpass != "") && (twpass != null))
            {
                twitterpass = twpass;
            }

            if (String.IsNullOrEmpty(twitteruser))
            {
                MessageBox.Show("Twitter settings not set.");
                return;
            }
            else if (String.IsNullOrEmpty(twpass))
            {
                Form askpass = new Form();
                askpass.Height = 80;
                askpass.Width = 280;
                askpass.Text = "Twitter password needed";
                askpass.Show();
                TextBox tbpass = new TextBox();
                tbpass.Location = new Point(10, 10);
                tbpass.Width = 160;
                tbpass.Name = "tbPassword";
                tbpass.PasswordChar = Convert.ToChar("*"); ;
                Button btnOk = new Button();
                btnOk.Location = new Point(180, 10);
                btnOk.Text = "Ok";
                btnOk.Width = 80;
                btnOk.Name = "btnOk";
                btnOk.Click += askpassok;
                askpass.Controls.Add(tbpass);
                askpass.Controls.Add(btnOk);
            }
            else
            {
                Twitter twitter = new Twitter();
                if (twitter.UpdateAsXML(twitteruser, twitterpass, note) != null)
                {
                    MessageBox.Show("Your note is Tweeted.");
                }
                else
                {
                    MessageBox.Show("Sending note to twitter failed.");
                }
            }            
        }

        private void rtbNote_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            xmlHandler getSettings = new xmlHandler(true);
            if (getSettings.getXMLnodeAsBool("askurl"))
            {
                DialogResult result = MessageBox.Show(this, "Are you sure you want to visted: " + e.LinkText, "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(e.LinkText);
                }
            }
            else
            {
                System.Diagnostics.Process.Start(e.LinkText);
            }
        }


        private void TwitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #if win32
            if (IsConnectedToInternet())
            {
            #endif
                if ((String.IsNullOrEmpty(note) ==false) && (note.Length < 140))
                {
                    tweetnote();
                }
                else if (note.Length > 140)
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
            #if win32
            }
            else
            {
                MessageBox.Show("No network connection.");
            }
            #endif
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

		#endregion Methods 
    }
}