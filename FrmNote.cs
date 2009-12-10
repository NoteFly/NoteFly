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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


#if win32
using System.Runtime.InteropServices;

#endif

namespace NoteFly
{
    public partial class FrmNote : Form
    {
        #region Fields (15)

        public Notes notes;
        private Form frmLoginFb = null;
        private TextHighlight highlight;
        private Int16 id;
        private int locX;
        private int locY;
        private String note;
        private Int16 notecolor = 0;
        private Boolean notelock = false;
        private Boolean notevisible = true;
        private Skin skin;
        private String title;
        private String twpass;

        public const int HT_CAPTION = 0x2;
        public const int WM_NCLBUTTONDOWN = 0xA1;

        #endregion Fields

        #region Constructors (2)

        public FrmNote(Notes notes, Int16 id, bool visible, bool ontop, string title, string note, Int16 notecolor, int locX, int locY, int notewidth, int noteheight)
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
                CheckThings();
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

        public FrmNote(Notes notes, Int16 id, string title, string note, Int16 notecolor)
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

            PaintColorNote();
            lblTitle.Text = title;
            rtbNote.Text = note;
            SetPosNote();
            CheckThings();
            notes.NotesUpdated = true;
        }

        #endregion Constructors

        #region Properties (4)

        public Int16 NoteColor
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

        public string NoteContent
        {
            get { return this.note; }
            set
            {
                note = value;
                rtbNote.Text = note;
            }
        }

        public Int16 NoteID
        {
            get { return id; }
            set { this.id = value; }
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

        #endregion Properties

        #region Methods (30)

        // Public Methods (2) 

        /// <summary>
        /// Check if twitter is enabled and check Syntax.
        /// </summary>
        public void CheckThings()
        {
            CheckTwitter(notes.TwitterEnabled);

            PaintColorNote();

            if ((notes.HighlightHTML == true) || (notes.HighlightC == true))
            {
                if (highlight == null)
                {
                    highlight = new TextHighlight(notes.HighlightHTML, notes.HighlightC, this.rtbNote);
                    highlight.CheckSyntaxFull();
                }
                else if (highlight != null)
                {
                    highlight.CheckSyntaxFull();
                }
            }
        }

        public void UpdateThisNote()
        {
            SavePos.RunWorkerAsync();
        }
        // Private Methods (28) 

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
            foreach (Control cntrl in frmAskpass.Controls)
            {
                cntrl.Dispose();
            }
            frmAskpass.Dispose();

            tweetnote();
        }

        /// <summary>
        /// Hide note
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseNote_Click(object sender, EventArgs e)
        {
            if (frmLoginFb != null)
            {
                try
                {
                    frmLoginFb.Close();
                }
                catch (Exception exc)
                {
                    throw new CustomExceptions(exc.Message);
                }
                
            }
            this.notevisible = false;
            notes.NotesUpdated = true;
            this.Hide();
        }

        /// <summary>
        /// check if twitter is enabled.
        /// </summary>
        /// <param name="twitterenabled"></param>
        private void CheckTwitter(bool twitterenabled)
        {
            if (twitterenabled)
            {
                tsmenuSendToTwitter.Enabled = true;
            }
            else
            {
                tsmenuSendToTwitter.Enabled = false;
            }
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

            if (this.NoteID > notes.NumNotes)
            {
                MessageBox.Show("Error: cannot find note.");
            }
            notes.EditNewNote(this.NoteID);
        }

        /// <summary>
        /// E-mail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void emailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string emailnote;

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

        private void FbWeb_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            Facebook fb = new Facebook();
            if (fb.ParserURL(e.Url.ToString()))
            {
                int responsecode = fb.CheckResponse(fb.PostStream(this.note));
                switch (responsecode)
                {
                    case 0:
                        MessageBox.Show("Note is posted on your facebook wall.");
                        break;
                    case 1:
                        MessageBox.Show("Error: unknow error occurred");
                        break;
                    case 100:
                        throw new CustomExceptions("Invalid paramters");
                        break;
                    case 200:
                        MessageBox.Show("Error: no proper primisiion to post on your wall.");
                        break;
                    case 210:
                        MessageBox.Show("Error: User not visible. The user doesn't have permission to act on that object. ");
                        break;
                    case 340:
                        MessageBox.Show("Error: Feed action request limit reached.");
                        break;

                }
            }

            if (frmLoginFb != null) { frmLoginFb.Close(); }
        }

        /// <summary>
        /// Form got focus, remove transparency
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmNote_Activated(object sender, EventArgs e)
        {
            if ((notes.Transparency) && (skin != null))
            {
                this.Opacity = 1.0;
            }
        }

        /// <summary>
        /// Form is not active anymore, make transparent if allowed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmNote_Deactivate(object sender, EventArgs e)
        {
            if ((notes.Transparency) && (skin != null))
            {
                this.Opacity = skin.getTransparencylevel();
                this.Refresh();
            }
        }

        private void hideNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnCloseNote_Click(sender, e);
        }

        /// <summary>
        /// Lock note
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void locknoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!notelock)
            {
                notelock = true;
                pbShowLock.Visible = true;
                pbShowLock.Location = new Point(btnCloseNote.Location.X - 24, 8);
                pbShowLock.Size = new Size(16, 16);
                locknoteToolStripMenuItem.Text = "lock note (click again to unlock)";
                this.menuNoteColors.Enabled = false;
                this.editTToolStripMenuItem.Enabled = false;
                this.OnTopToolStripMenuItem.Enabled = false;
            }
            else
            {
                notelock = false;
                pbShowLock.Visible = false;
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
        private void PaintColorNote()
        {
            skin = new Skin(notecolor);
            Color normalcolor = skin.getObjColor(false);

            this.BackColor = normalcolor;
            this.pnlHead.BackColor = normalcolor;
            this.pnlNote.BackColor = normalcolor;
            this.rtbNote.BackColor = normalcolor;

            if (notes.TextDirection == 0)
            {
                lblTitle.TextAlign = ContentAlignment.TopLeft;
                rtbNote.SelectionAlignment = HorizontalAlignment.Left;
            }
            else if (notes.TextDirection == 1)
            {
                lblTitle.TextAlign = ContentAlignment.TopRight;

                rtbNote.SelectAll();
                rtbNote.SelectionAlignment = HorizontalAlignment.Right;
            }
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

        /// <summary>
        /// hyperlink clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Thread to save note settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SavePos_DoWork(object sender, DoWorkEventArgs e)
        {
            this.locX = this.Location.X;
            this.locY = this.Location.Y;

            if ((this.locX + this.Width > 0) && (this.locY + this.Height > 0) && (notecolor >= 0))
            {
                string notefile = System.IO.Path.Combine(notes.NoteSavePath, this.id + ".xml");
                xmlHandler updateposnote = new xmlHandler(notefile);
                updateposnote.WriteNote(notevisible, this.TopMost, notecolor, this.title, this.note, this.locX, this.locY, this.Width, this.Height);
            }
            else if (notecolor >= 0)
            {
                MessageBox.Show("Error: note location out of screen.");
            }
            else
            {
                MessageBox.Show("Error: notecolor unknow.");
            }
        }

        /// <summary>
        /// Set the color of the note.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setColorNote(object sender, EventArgs e)
        {
            Int16 i = 0;
            foreach (ToolStripMenuItem curitem in menuNoteColors.DropDownItems)
            {
                if (curitem == sender)
                {
                    curitem.Checked = true;
                    notecolor = i;
                    string notefile = System.IO.Path.Combine(notes.NoteSavePath, this.id + ".xml");
                    xmlHandler savenotecolor = new xmlHandler(notefile);
                    savenotecolor.WriteNote(notevisible, OnTopToolStripMenuItem.Checked, notecolor, this.title, this.note, this.locX, this.locY, this.Width, this.Height);
                }
                else
                {
                    curitem.Checked = false;
                }
                i++;
            }

            PaintColorNote();
        }

        /// <summary>
        /// Set the position of frmNote
        /// </summary>
        private void SetPosNote()
        {
            this.Location = new Point(locX, locY);
        }

        /// <summary>
        /// Set the size of frmNote
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private void SetSizeNote(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Send note to Facebook.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmenuSendToFacebook_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;

            Facebook fb = new Facebook();

            //grant access.form
            frmLoginFb = new Form();
            frmLoginFb.Text = "Post note on FaceBook";
            frmLoginFb.Width = 640;
            frmLoginFb.Height = 480;
            WebBrowser FbWeb = new WebBrowser();
            FbWeb.Name = "FbWeb";
            FbWeb.Location = new System.Drawing.Point(10, 10);
            FbWeb.Dock = DockStyle.Fill;
            FbWeb.Navigated += new WebBrowserNavigatedEventHandler(FbWeb_Navigated);
            frmLoginFb.Controls.Add(FbWeb);
            frmLoginFb.Show();

            FbWeb.Navigate(fb.CreateLoginURL());
        }

        /// <summary>
        /// Request to tweet note. Check if allow, if so call tweetnote() methode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmenuSendToTwitter_Click(object sender, EventArgs e)
        {
            if (!CheckConnection()) return;

            if ((String.IsNullOrEmpty(note) == false) && (note.Length < 140))
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
                MessageBox.Show("Error: Your note is empty.");
            }
        }

        /// <summary>
        /// Save the note to a plain textfile.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmenuSendToTextfile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfdlg = new SaveFileDialog();
            sfdlg.DefaultExt = "txt";
            sfdlg.AddExtension = true;
            sfdlg.ValidateNames = true;
            sfdlg.CheckPathExists = true;
            sfdlg.OverwritePrompt = true;
            if (title.Length > 100)
            {
                sfdlg.FileName = title.Substring(0, 100);
            }
            else
            {
                sfdlg.FileName = title;
            }
            sfdlg.Title = "Save note to textfile";
            sfdlg.Filter = "Textfile (*.txt)|*.txt|Webpage (*.htm)|*.htm";
            if (sfdlg.ShowDialog() == DialogResult.OK)
            {
                new Textfile(true, sfdlg.FileName, this.title, this.note);
            }
            
        }

        /// <summary>
        /// Check if there is internet connection, if not warn user.
        /// </summary>
        /// <returns></returns>
        private bool CheckConnection()
        {
            #if win32
            if (IsConnectedToInternet() == true)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Error: there is no network connection.");
                return false;
            }
            #elif !win32
            return true;
            #endif
        }

        /// <summary>
        /// Tweet a note.
        /// </summary>
        private void tweetnote()
        {
            xmlHandler getSettings = new xmlHandler(true);
            string twitteruser = getSettings.getXMLnode("twitteruser");
            string twitterpass = getSettings.getXMLnode("twitterpass");
            if (!String.IsNullOrEmpty(twpass))
            {
                twitterpass = twpass;
            }

            if (String.IsNullOrEmpty(twitteruser))
            {
                MessageBox.Show("Error: you haven't set your twitter username yet.\r\nSettings window will now open.");
                FrmSettings settings = new FrmSettings(notes, notes.Transparency);
                settings.Show();
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
                tbpass.PasswordChar = Convert.ToChar("X"); ;
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
                if (!CheckConnection()) return;

                Twitter twitter = new Twitter();
                if (twitter.UpdateAsXML(twitteruser, twitterpass, note) != null)
                {
                    MessageBox.Show("Your note is Tweeted.");
                }
                else
                {
                    MessageBox.Show("Error: Sending note to twitter failed.");
                }
                twpass.Remove(0);
            }
        }

        /// <summary>
        /// Change check in menu colors
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
    }
}