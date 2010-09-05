//-----------------------------------------------------------------------
// <copyright file="FrmNote.cs" company="GNU">
//  NoteFly a note application.
//  Copyright (C) 2010  Tom
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
// </copyright>
//-----------------------------------------------------------------------
#define windows //platform can be: windows, linux, macos

namespace NoteFly
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
#if windows
    using System.Runtime.InteropServices;
#endif
    /// <summary>
    /// The note class.
    /// </summary>
    public partial class FrmNote : Form
    {
        #region Fields (10)

        private const int MINVISIBLESIZE = 5;

        //private string note, title;
        private char[] twpass;
        //private short id, notecolor = 0;
        private bool moving = false;
        private Skin skin;
        //private int locX, locY;
        //private ushort noteWidth, noteHeight;
        private PictureBox pbShowLock;
        private Point oldp;
        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the FrmNote class.
        /// </summary>
        /// <param name="note">note data class.</param>
        public FrmNote(Note note)
        {
            if (!note.Visible)
            {
                throw new CustomException("Form should not be created. visible is set false.");
                //return;
            }

            this.FormBorderStyle = FormBorderStyle.None;
            this.lblTitle.Text = note.Title;
            this.rtbNote.Text = note.Content;
            this.skin = new Skin(note.Color);
            this.InitializeComponent();
            this.PaintColorNote();
            this.SetSizeNote(note.Width, note.Height);
            this.SetPosNote();
            this.TopMost = note.Ontop;
            this.CheckThings();
        }

        #endregion Constructors

        #region Methods (32)

        // Public Methods (2) 

        /// <summary>
        /// Check if twitter is enabled and check Syntax.
        /// </summary>
        public void CheckThings()
        {
            this.PaintColorNote();

            this.SetTextMenuTwitter(Settings.SocialTwitterEnabled);

            TextHighlight.CheckSyntaxFull(rtbNote);

            if (this.TopMost)
            {
                this.menuOnTop.Checked = true;
            }
            else
            {
                this.menuOnTop.Checked = false;
            }

            this.rtbNote.DetectUrls = true;
            this.rtbNote.Text += ""; //causes TextChanged event so rescan for URL's happens
        }

        /// <summary>
        /// Save the setting of the note.
        /// </summary>
        public void UpdateThisNote()
        {
            this.SavePos.RunWorkerAsync();
        }
        // Private Methods (30) 

#if windows
        /// <summary>
        /// Check internet state.
        /// </summary>
        /// <returns>True is connected to internet.</returns>
        private static bool IsConnectedToInternet()
        {
            int desc;
            return InternetGetConnectedState(out desc, 0);
        }

        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int description, int ReservedValue);
#endif

        /// <summary>
        /// Find what password is entered.
        /// Make sure the memory gets cleared.
        /// </summary>
        /// <param name="obj">The button user clicked on.</param>
        /// <param name="e">Event arguments</param>
        private void Askpassok(object obj, EventArgs e)
        {
            Button btnobj = (Button)obj;
            Form frmAskpass = btnobj.FindForm();

            Control[] passwctrl = frmAskpass.Controls.Find("tbPassword", false);
            if (String.IsNullOrEmpty(passwctrl[0].Text))
            {
                passwctrl[0].BackColor = Color.Red;
                return;
            }
            else
            {
                this.twpass = new char[passwctrl[0].Text.Length];
                for (int n = 0; n < passwctrl[0].Text.Length; n++)
                {
                    this.twpass[n] = passwctrl[0].Text[n];
                }
                if (this.twpass.Length <= 0 || this.twpass == null)
                {
                    throw new CustomException("buffer underflow");
                }
                if (this.twpass.Length > 255)
                {
                    throw new CustomException("password too long.");
                }
                passwctrl[0].Name = new Random().Next().ToString();
                passwctrl[0].Text.Remove(0);
                frmAskpass.Close();
                foreach (Control cntrl in frmAskpass.Controls)
                {
                    cntrl.Dispose();
                }
                frmAskpass.Dispose();
                GC.Collect();
                this.Tweetnote();
            }
        }

        /// <summary>
        /// The user pressed the cross on the note,
        /// Hide the note.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnCloseNote_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            //this.notes.NotesUpdated = true;
            this.Hide();
        }

        /// <summary>
        /// Check if there is internet connection, if not warn user.
        /// </summary>
        /// <returns>true if there is a coonection, otherwise return false</returns>
        private bool CheckConnection()
        {
#if windows
            if (IsConnectedToInternet() == true)
            {
                return true;
            }
            else
            {
                string msgNoNetwork = "There is no network connection.";
                Log.Write(LogType.error, msgNoNetwork);
                MessageBox.Show(msgNoNetwork);
                return false;
            }
#elif !windows
            return true;
#endif
        }

        /// <summary>
        /// Set text tsmenuSendToTwitter based on if twitter is enabled.
        /// </summary>
        /// <param name="twitterenabled">Is twitter enabled.</param>
        private void SetTextMenuTwitter(bool twitterenabled)
        {
            const string STWITTER = "twitter";
            if (twitterenabled)
            {
                this.menuSendToTwitter.Text = STWITTER;
            }
            else
            {
                this.menuSendToTwitter.Text = STWITTER + " (not setup)";
            }
        }

        /// <summary>
        /// contextMenuStripNoteOptions is closed.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void contextMenuStripNoteOptions_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            if (this.skin != null)
            {
                this.pnlHead.BackColor = this.skin.GetObjColor(false);
            }
        }

        /// <summary>
        /// Copy note content to clipboard.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void copyTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.note))
            {
                Clipboard.SetText(this.note);
            }
        }

        /// <summary>
        /// Copy note title to clipboard.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void copyTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.title))
            {
                Clipboard.SetText(this.title);
            }
        }

        /// <summary>
        /// Edit note is clicked.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void editTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            if (this.NoteID > this.notes.NumNotes)
            {
                string cannotfindnote = "Cannot find note.";
                Log.Write(LogType.error, cannotfindnote);
                MessageBox.Show(cannotfindnote);
            }

            this.notes.EditNewNote(this.NoteID);
        }

        /// <summary>
        /// E-mail an note. Start default mail client with subject and content, if possible.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void emailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string emailnote = System.Web.HttpUtility.UrlEncode(this.note).Replace("+", " ");
            string emailtitle = System.Web.HttpUtility.UrlEncode(this.title);

            xmlHandler xmlsettings = new xmlHandler(true);
            string defaultemail = xmlsettings.getXMLnode("defaultemail");

            if (!String.IsNullOrEmpty(emailtitle) && (!String.IsNullOrEmpty(emailnote))) //bugfix #0000008
            {
                System.Diagnostics.Process.Start("mailto:" + defaultemail + "?subject=" + this.title + "&body=" + emailnote);
            }
            else if (!String.IsNullOrEmpty(emailtitle))
            {
                System.Diagnostics.Process.Start("mailto:" + defaultemail + "?subject=" + this.title);
            }
            else
            {
                string msgNoTitleContent = "Note has no title+content.";
                Log.Write(LogType.error, msgNoTitleContent);
                MessageBox.Show(msgNoTitleContent);
            }
        }

        /// <summary>
        /// Form got focus, remove transparency.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void frmNote_Activated(object sender, EventArgs e)
        {
            if (this.notes.Transparency && this.skin != null)
            {
                this.Opacity = 1.0;
            }
        }

        /// <summary>
        /// Form is not active anymore, make transparent if allowed.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void frmNote_Deactivate(object sender, EventArgs e)
        {
            if (this.notes.Transparency && this.skin != null)
            {
                this.Opacity = this.skin.GetTransparencylevel();
            }
        }

        /// <summary>
        /// Hide note.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments</param>
        private void hideNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btnCloseNote_Click(sender, e);
        }

        /// <summary>
        /// Lock the note and show a lock.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void locknoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string locknotemsg = "&Lock note";
            if (this.notelock)
            {
                this.notelock = false;
                this.DestroyPbLock();
                this.menuLockNote.Text = locknotemsg;
                this.pbResizeGrip.Visible = true;
            }
            else
            {
                this.notelock = true;
                this.CreatePbLock();
                this.menuLockNote.Text = locknotemsg + " (click again to unlock)";
                this.pbResizeGrip.Visible = false;
            }

            this.menuNoteColors.Enabled = !this.notelock;
            this.menuEditNote.Enabled = !this.notelock;
            this.menuOnTop.Enabled = !this.notelock;
            this.menuRollUp.Enabled = !this.notelock;
        }

        /// <summary>
        /// Create a PictureBox with lock picture.
        /// </summary>
        private void CreatePbLock()
        {
            this.pbShowLock = new PictureBox();
            this.pbShowLock.Name = "pbShowLock";
            this.pbShowLock.Size = new Size(16, 16);
            this.pbShowLock.Location = new Point((this.btnCloseNote.Location.X - 24), 8);
            this.pbShowLock.Image = new Bitmap(NoteFly.Properties.Resources.locknote);
            this.pbShowLock.Visible = true;
            this.pnlHead.Controls.Add(this.pbShowLock);
            this.pbShowLock.BringToFront();
        }

        /// <summary>
        /// Removes and freese memory lock picture.
        /// </summary>
        private void DestroyPbLock()
        {
            this.pbShowLock.Visible = false;
            this.pbShowLock.Dispose();
        }

        /// <summary>
        /// Roll the note up and down.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void menuRollUp_Click(object sender, EventArgs e)
        {
            this.rolledup = !this.rolledup;
            this.menuRollUp.Checked = this.rolledup;
            if (this.rolledup)
            {
                this.menuRollUp.Text = this.menuRollUp.Text + "(click again to Roll Down)";
                this.MinimumSize = new Size(this.MinimumSize.Width, this.pnlHead.Height);
                this.Height = this.Height - this.pnlNote.Height;
            }
            else
            {
                this.menuRollUp.Text = this.menuRollUp.Text.Substring(0, 8);
                this.MinimumSize = new Size(this.MinimumSize.Width, this.pnlHead.Height + this.pbResizeGrip.Height);
                this.Height = this.noteHeight;
            }
        }

        /// <summary>
        /// Make a note on top (and save note) CheckOnClick is set to true.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments</param>
        private void OnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = this.menuOnTop.Checked;

            if (!this.notelock && !this.SavePos.IsBusy)
            {
                this.SavePos.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Get the color of the note and paint it.
        /// </summary>
        private void PaintColorNote()
        {
            this.skin = new Skin(this.notecolor);
            Color normalcolor = this.skin.GetObjColor(false);

            this.BackColor = normalcolor;
            this.pnlHead.BackColor = normalcolor;
            this.pnlNote.BackColor = normalcolor;
            this.rtbNote.BackColor = normalcolor;

            if (this.notes.TextDirection == 0)
            {
                this.lblTitle.TextAlign = ContentAlignment.TopLeft;
                this.rtbNote.SelectAll(); //fix bug: #0000012
                this.rtbNote.SelectionAlignment = HorizontalAlignment.Left;
                //this.rtbNote.RightToLeft = RightToLeft.No;
            }
            else if (this.notes.TextDirection == 1)
            {
                this.lblTitle.TextAlign = ContentAlignment.TopRight;
                this.rtbNote.SelectAll();
                this.rtbNote.SelectionAlignment = HorizontalAlignment.Right;
                //this.rtbNote.RightToLeft = RightToLeft.Yes; //will make the contextmenu act not right.
            }
            this.rtbNote.SelectionStart = 0;

            this.rtbNote.Font = this.skin.GetFontNoteContent();
        }

        /// <summary>
        /// Resize note
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void pbResizeGrip_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!this.notelock)
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
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void pbResizeGrip_MouseUp(object sender, MouseEventArgs e)
        {
            if (!this.notelock && !this.SavePos.IsBusy)
            {
                this.SavePos.RunWorkerAsync();
            }
        }

        /// <summary>
        /// pnlHead the grab area is selected.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void pnlHead_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.moving = true;
                this.oldp = e.Location;

                if (this.skin != null)
                {
                    this.pnlHead.BackColor = this.skin.GetObjColor(true);
                }

                this.locX = this.Location.X;
                this.locY = this.Location.Y;
            }
            else if (this.skin != null)
            {
                this.pnlHead.BackColor = this.skin.GetObjColor(false);
            }

            if (this.SavePos.IsBusy == false)
            {
                this.SavePos.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Resize note.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments</param>
        private void pnlResizeWindow_MouseDown(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.SizeNWSE;
        }

        /// <summary>
        /// Hyperlink clicked.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void rtbNote_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            xmlHandler getSettings = new xmlHandler(true);
            if (getSettings.getXMLnodeAsBool("askurl"))
            {
                DialogResult result = MessageBox.Show(this, "Are you sure you want to visted:\r\n" + e.LinkText, "url pressed", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
        /// Thread to save note settings.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void SavePos_DoWork(object sender, DoWorkEventArgs e)
        {
            this.locX = this.Location.X;
            this.locY = this.Location.Y;

            if (!this.rolledup)
            {
                try
                {
                    this.noteWidth = Convert.ToUInt16(this.Width);
                }
                catch (InvalidCastException)
                {
                    new CustomException("noteWidth cannot be negative value.");
                }
                try
                {
                    this.noteHeight = Convert.ToUInt16(this.Height);
                }
                catch (InvalidCastException)
                {
                    new CustomException("noteHeight cannot be negative value.");
                }
            }

            if ((this.locX + this.Width > MINVISIBLESIZE) && (this.locY + this.Height > MINVISIBLESIZE) && (this.notecolor >= 0) && this.notecolor <= this.skin.MaxNotesColors)
            {
                string notefile = System.IO.Path.Combine(this.notes.NoteSavePath, this.id + ".xml");
                xmlHandler updateposnote = new xmlHandler(notefile);
                updateposnote.WriteNote(this.Visible, this.TopMost, this.notecolor, this.title, this.note, this.locX, this.locY, this.noteWidth, this.noteHeight);
            }
            else if (this.notecolor < 0 || this.notecolor > this.skin.MaxNotesColors)
            {
                throw new CustomException("Note color unknow.");
            }
            else
            {
                string msgOutOfScreen = "Position note (ID:" + this.NoteID + ") is out of screen.";
                Log.Write(LogType.error, msgOutOfScreen);
                MessageBox.Show(msgOutOfScreen);
            }

        }

        /// <summary>
        /// Set the color of the note.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void SetColorNote(object sender, EventArgs e)
        {
            ToolStripMenuItem selectedmenuitem = (ToolStripMenuItem)sender;
            short i = 0;
            foreach (ToolStripMenuItem curitem in this.menuNoteColors.DropDownItems)
            {
                if (curitem == selectedmenuitem)
                {
                    curitem.Checked = true;
                    this.notecolor = i;
                    string notefile = System.IO.Path.Combine(this.notes.NoteSavePath, this.id + ".xml");
                    xmlHandler savenotecolor = new xmlHandler(notefile);
                    savenotecolor.WriteNote(this.Visible, this.menuOnTop.Checked, this.notecolor, this.title, this.note, this.locX, this.locY, this.Width, this.Height);
                    Log.Write(LogType.info, "Color note (ID:" + this.NoteID + ") changed.");
                }
                else
                {
                    curitem.Checked = false;
                }

                i++;
            }

            this.PaintColorNote();
        }

        /// <summary>
        /// Set the position of frmNote
        /// </summary>
        private void SetPosNote()
        {
            this.Location = new Point(this.locX, this.locY);
        }

        /// <summary>
        /// Set the size of FrmNote.
        /// </summary>
        /// <param name="width">The new width of FrmNote.</param>
        /// <param name="height">The new height of FrmNote.</param>
        private void SetSizeNote(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        /// <summary>
        /// Send note to Facebook.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void tsmenuSendToFacebook_Click(object sender, EventArgs e)
        {
            if (!this.CheckConnection())
            {
                return;
            }

            Facebook fb = new Facebook();
            fb.StartPostingNote(this.note);
        }

        /// <summary>
        /// Save the note to a plain textfile.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void tsmenuSendToTextfile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfdlg = new SaveFileDialog();
            sfdlg.DefaultExt = "txt";
            sfdlg.AddExtension = true;
            sfdlg.ValidateNames = true;
            sfdlg.CheckPathExists = true;
            sfdlg.OverwritePrompt = true;

            //strip forbidden filename characters:
            System.Text.StringBuilder suggestfilenamesafe = new System.Text.StringBuilder();
            char[] forbiddenchars = "?<>:*|\\/".ToCharArray();
            for (int pos = 0; (pos < this.title.Length) && (pos<=100); pos++)
            {
                bool isforbiddenchar = false;
                for (int fc = 0; fc < forbiddenchars.Length; fc++)
                {
                    if (this.title[pos] == forbiddenchars[fc])
                    {
                        isforbiddenchar = true;
                    }
                }
                if (!isforbiddenchar)
                {
                    suggestfilenamesafe.Append(this.title[pos]);
                }
            }
            sfdlg.FileName = suggestfilenamesafe.ToString();

            sfdlg.Title = "Save note to textfile";
            sfdlg.Filter = "Textfile (*.txt)|*.txt|Webpage (*.htm)|*.htm";
            if (sfdlg.ShowDialog() == DialogResult.OK)
            {
                new Textfile(true, sfdlg.FileName, this.title, this.note);
                string logmsg = "Note (ID:" + this.NoteID + ") saved to ";
                switch (sfdlg.FilterIndex)
                {
                    case 0:
                        Log.Write(LogType.info, logmsg + "textfile.");
                        break;
                    case 1:
                        Log.Write(LogType.info, logmsg + "htmlfile.");
                        break;
                    default:
                        Log.Write(LogType.info, logmsg + "some file.");
                        break;
                }
            }
        }

        /// <summary>
        /// Request to tweet note. Check if allow, if so call tweetnote() methode.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void tsmenuSendToTwitter_Click(object sender, EventArgs e)
        {
            if (!this.CheckConnection())
            {
                return;
            }

            if ((String.IsNullOrEmpty(this.note) == false) && (this.note.Length <= 140))
            {
                this.Tweetnote();
            }
            else if (this.note.Length > 140)
            {
                DialogResult result;
                string shrttweet = this.note.Substring(0, 140);
                result = MessageBox.Show("Your note is more than the 140 chars.\r\nDo you want to publish only the first 140 characters? ", "Too long", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.Tweetnote();
                    Log.Write(LogType.info, "Shorted note send to twitter.");
                }
            }
            else
            {
                string emptynote = "Note is empty.";
                Log.Write(LogType.error, emptynote);
                MessageBox.Show(emptynote);
            }
        }

        /// <summary>
        /// Tweet a note.
        /// </summary>
        private void Tweetnote()
        {
            if (!this.CheckConnection())
            {
                return;
            }

            xmlHandler getSettings = new xmlHandler(true);
            string twitteruser = getSettings.getXMLnode("twitteruser");

            if (String.IsNullOrEmpty(twitteruser))
            {
                string notwusername = "You haven't set your twitter username yet.\r\nSettings window will now open.";
                Log.Write(LogType.error, notwusername.Replace("\r\n", ""));
                MessageBox.Show(notwusername);
                FrmSettings settings = new FrmSettings(this.notes);
                settings.Show();
                return;
            }

            if (this.twpass == null)
            {
                this.twpass = getSettings.getXMLnode("twitterpass").ToCharArray();
            }

            if ((this.twpass == null) || (this.twpass.Length <= 0))
            {
                Form askpass = new Form();
                askpass.ShowIcon = false;
                askpass.Height = 80;
                askpass.Width = 280;
                askpass.Text = "Twitter password needed";
                askpass.Show();
                TextBox tbpass = new TextBox();
                tbpass.Location = new Point(10, 10);
                tbpass.Width = 160;
                tbpass.Name = "tbPassword";
                tbpass.PasswordChar = 'X';
                Button btnOk = new Button();
                btnOk.Location = new Point(180, 10);
                btnOk.Text = "Ok";
                btnOk.Width = 80;
                btnOk.Name = "btnOk";
                btnOk.Click += this.Askpassok;
                askpass.Controls.Add(tbpass);
                askpass.Controls.Add(btnOk);
            }
            else
            {
                Twitter twitter = new Twitter();
                const string twitterleadmgs = "Sending note to twitter ";
                if (twitter.UpdateAsXML(twitteruser, this.twpass, this.note) != null)
                {
                    string sendtwsucces = twitterleadmgs + "succeded.";
                    Log.Write(LogType.info, sendtwsucces);
                    MessageBox.Show(sendtwsucces);
                }
                else
                {
                    string sendtwfail = twitterleadmgs + "failed.";
                    Log.Write(LogType.error, sendtwfail);
                    MessageBox.Show(sendtwfail);
                }
            }
        }

        /// <summary>
        /// Change check in menu colors.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void updateMenuNoteColor(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem curitem in this.menuNoteColors.DropDownItems)
            {
                curitem.Checked = false;
            }

            switch (this.notecolor)
            {
                case 0:
                    this.yellowToolStripMenuItem.Checked = true;
                    break;
                case 1:
                    this.orangeToolStripMenuItem.Checked = true;
                    break;
                case 2:
                    this.whiteToolStripMenuItem.Checked = true;
                    break;
                case 3:
                    this.greenToolStripMenuItem.Checked = true;
                    break;
                case 4:
                    this.blueToolStripMenuItem.Checked = true;
                    break;
                case 5:
                    this.purpleToolStripMenuItem.Checked = true;
                    break;
                case 6:
                    this.redToolStripMenuItem.Checked = true;
                    break;
                default:
                    new CustomException("unknow color selected.");
                    break;
            }
        }

        private void pnlHead_MouseUp(object sender, MouseEventArgs e)
        {
            this.moving = false;
        }

        private void pnlHead_MouseMove(object sender, MouseEventArgs e)
        {
            if ((this.moving) && (e.Button == MouseButtons.Left))
            {
                if (this.skin != null)
                {
                    this.pnlHead.BackColor = this.skin.GetObjColor(true);
                }

                int dpx = e.Location.X - oldp.X;
                int dpy = e.Location.Y - oldp.Y;
#if linux
                if (dpx > 8)
                {
                    dpx = 8;
                } 
                else if (dpx < -8)
                {
                    dpx = -8;
                }
                if (dpy > 8)
                {
                    dpy = 8;
                }
                else if (dpy < -8)
                {
                    dpy = -8;
                }
#endif
                this.Location = new Point(this.Location.X + dpx, this.Location.Y + dpy); //bug fix: #0000011
            }
            else if (this.skin != null)
            {
                this.pnlHead.BackColor = this.skin.GetObjColor(false);
            }
        }

        #endregion Methods
    }
}