﻿//-----------------------------------------------------------------------
// <copyright file="FrmNote.cs" company="GNU">
// 
// This program is free software; you can redistribute it and/or modify it
// Free Software Foundation; either version 2, 
// or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// </copyright>
//-----------------------------------------------------------------------
namespace NoteFly
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
#if win32
    using System.Runtime.InteropServices;
#endif
    /// <summary>
    /// The note class.
    /// </summary>
    public partial class FrmNote : Form
    {
        #region Fields (10)

        private const int MINVISIBLESIZE = 5;
        private const int HTCAPTION = 0x2;
        private const int WMNCLBUTTONDOWN = 0xA1;

        private Notes notes;
        private TextHighlight highlight;
        private string note, title;
        private char[] twpass;
        private short id, notecolor = 0;
        private bool rolledup = false, notelock = false,  moving = false;
        private Skin skin;
        private int locX, locY;
        private ushort noteWidth, noteHeight;
        private PictureBox pbShowLock;
        private Point dp;
        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the FrmNote class.
        /// </summary>
        /// <param name="notes">The class with access to all notes.</param>
        /// <param name="id">The id of the note.</param>
        /// <param name="visible">Is the note visible.</param>
        /// <param name="ontop">Is the note on top.</param>
        /// <param name="title">The title of the note.</param>
        /// <param name="note">the content of the note.</param>
        /// <param name="notecolor">The default note color.</param>
        /// <param name="locX">The X location on the screen.</param>
        /// <param name="locY">The Y location on the screen.</param>
        /// <param name="notewidth">The width of the note.</param>
        /// <param name="noteheight">The height of the note.</param>
        public FrmNote(Notes notes, short id, bool visible, bool ontop, string title, string note, short notecolor, int locX, int locY, int notewidth, int noteheight)
        {
            this.notes = notes;
            this.FormBorderStyle = FormBorderStyle.None;
            this.skin = new Skin(notecolor);
            this.id = id;
            this.title = title;
            this.note = note;
            this.notecolor = notecolor;

            if ((locX + notewidth > MINVISIBLESIZE) && (locY + noteheight > MINVISIBLESIZE))
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
            this.InitializeComponent();
            this.lblTitle.Text = title;
            this.rtbNote.Text = note;

            if (visible)
            {
                this.SetSizeNote(notewidth, noteheight);
                this.SetPosNote();
                this.TopMost = ontop;
                this.CheckThings();
                this.rolledup = false;
                this.notelock = false;
            }
            else
            {
                this.Hide();
            }

            this.Visible = visible;
        }

        /// <summary>
        /// Initializes a new instance of the FrmNote class.
        /// </summary>
        /// <param name="notes">The class with access to all notes</param>
        /// <param name="id">The note id.</param>
        /// <param name="title">The note title.</param>
        /// <param name="note">The note content.</param>
        /// <param name="notecolor">The color (number) of the note.</param>
        public FrmNote(Notes notes, short id, string title, string note, short notecolor)
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
            this.InitializeComponent();

            this.PaintColorNote();
            this.lblTitle.Text = title;
            this.rtbNote.Text = note;
            this.SetPosNote();
            this.CheckThings();
            notes.NotesUpdated = true;
            this.rolledup = false;
            this.notelock = false;
        }

        #endregion Constructors

        #region Properties (4)

        /// <summary>
        /// Gets or sets the color of the note.
        /// </summary>
        public short NoteColor
        {
            get
            {
                return this.notecolor;
            }

            set
            {
                this.notecolor = value;
            }
        }

        /// <summary>
        /// Gets or sets the note content.
        /// </summary>
        public string NoteContent
        {
            get
            {
                return this.note;
            }

            set
            {
                this.note = value;
                this.rtbNote.Text = this.note;
            }
        }

        /// <summary>
        /// Gets or sets the note id.
        /// </summary>
        public short NoteID
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
            }
        }

        /// <summary>
        /// Gets or sets the note title.
        /// </summary>
        public string NoteTitle
        {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value;
                this.lblTitle.Text = this.title;
            }
        }

        #endregion Properties

        #region Methods (32)

        // Public Methods (2) 
#if win32
        ////[DllImportAttribute("user32.dll")]
        ////public static extern bool ReleaseCapture();
        ////[DllImportAttribute("user32.dll")]
        ////public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
#endif
        /// <summary>
        /// Check if twitter is enabled and check Syntax.
        /// </summary>
        public void CheckThings()
        {
            this.CheckTwitter(this.notes.TwitterEnabled);

            this.PaintColorNote();

            if (this.notes.HighlightHTML == true)
            {
                if (this.highlight == null)
                {
                    this.highlight = new TextHighlight(this.rtbNote, this.notes.HighlightHTML);
                }

                this.highlight.CheckSyntaxFull();
            }

            if (this.TopMost)
            {
                this.menuOnTop.Checked = true;
            }
            else
            {
                this.menuOnTop.Checked = false;
            }
        }

        /// <summary>
        /// Save the setting of the note.
        /// </summary>
        public void UpdateThisNote()
        {
            this.SavePos.RunWorkerAsync();
        }
        // Private Methods (30) 

#if win32
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
            this.notes.NotesUpdated = true;
            this.Hide();
        }

        /// <summary>
        /// Check if there is internet connection, if not warn user.
        /// </summary>
        /// <returns>true if there is a coonection, otherwise return false</returns>
        private bool CheckConnection()
        {
#if win32
            if (IsConnectedToInternet() == true)
            {
                return true;
            }
            else
            {
                string msgNoNetwork = "There is no network connection.";
                MessageBox.Show(msgNoNetwork);
                Log.Write(LogType.error, msgNoNetwork);
                return false;
            }
#elif !win32
            return true;
#endif
        }

        /// <summary>
        /// Enabled or disable tsmenuSendToTwitter based on if twitter is enabled.
        /// </summary>
        /// <param name="twitterenabled">Is twitter enabled.</param>
        private void CheckTwitter(bool twitterenabled)
        {
            if (twitterenabled)
            {
                this.tsmenuSendToTwitter.Text = "Twitter";
            }
            else
            {
                this.tsmenuSendToTwitter.Text = this.tsmenuSendToTwitter.Text + " (not setup)";
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
            Clipboard.SetText(this.note);
        }

        /// <summary>
        /// Copy note title to clipboard.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void copyTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.title);
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
                MessageBox.Show(cannotfindnote);
                Log.Write(LogType.error, cannotfindnote);
            }

            this.notes.EditNewNote(this.NoteID);
        }

        /// <summary>
        /// E-mail note is selected.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void emailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string emailnote;

#if win32
            emailnote = this.note.Replace("\r\n", "%0D%0A");
#elif mac
            emailnote = note.Replace("\r", "%0D%0A");
#elif linux                        
            emailnote = this.note.Replace("\n", "%0D%0A");
#endif
            xmlHandler xmlsettings = new xmlHandler(true);

            string defaultemail = xmlsettings.getXMLnode("defaultemail");

            if (!String.IsNullOrEmpty(this.title) && (!String.IsNullOrEmpty(emailnote))) //bugfix #0000008
            {
                System.Diagnostics.Process.Start("mailto:" + defaultemail + "?subject=" + this.title + "&body=" + emailnote);
            }
            else if (!String.IsNullOrEmpty(this.title))
            {
                System.Diagnostics.Process.Start("mailto:" + defaultemail + "?subject=" + this.title);
            }
            else
            {
                string msgNoTitleContent = "Note has no title and content.";
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
            string locknotemsg = "lock note";
            if (!this.notelock)
            {
                this.notelock = true;
                this.menuLockNote.Text = locknotemsg+" (click again to unlock)";
                this.CreatePbLock();
                this.menuNoteColors.Enabled = false;
                this.menuEditNote.Enabled = false;
                this.menuOnTop.Enabled = false;
            }
            else
            {
                this.notelock = false;
                this.menuLockNote.Text = locknotemsg;
                this.DestroyPbLock();
                this.menuNoteColors.Enabled = true;
                this.menuEditNote.Enabled = true;
                this.menuOnTop.Enabled = true;
            }
        }

        /// <summary>
        /// Create a PictureBox with lock picture.
        /// </summary>
        private void CreatePbLock()
        {
            this.pbShowLock = new PictureBox();
            this.pbShowLock.Name = "pbShowLock";
            this.pbShowLock.Location = new Point(this.btnCloseNote.Location.X - 24, 8);
            this.pbShowLock.Size = new Size(16, 16);
            this.pbShowLock.Image = new Bitmap(NoteFly.Properties.Resources.locknote);
            this.pbShowLock.Visible = true;
            this.pnlHead.Controls.Add(this.pbShowLock);
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
                this.menuRollUp.Text = this.menuRollUp.Text.Substring(0, 7);
                this.MinimumSize = new Size(this.MinimumSize.Width, this.pnlHead.Height + this.pbResizeGrip.Height);
                this.Height = this.noteHeight;
            }
        }

        /// <summary>
        /// Make a note on top.
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
                this.rtbNote.SelectionAlignment = HorizontalAlignment.Left;
            }
            else if (this.notes.TextDirection == 1)
            {
                this.lblTitle.TextAlign = ContentAlignment.TopRight;
                this.rtbNote.SelectAll();
                this.rtbNote.SelectionAlignment = HorizontalAlignment.Right;
            }

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
                this.dp = e.Location;

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
                    new CustomException("noteWidth cannot be a negative value.");
                }
                try
                {
                    this.noteHeight = Convert.ToUInt16(this.Height);
                }
                catch (InvalidCastException)
                {
                    new CustomException("noteHeight cannot be a negative value.");
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
            if (this.title.Length > 100)
            {
                sfdlg.FileName = this.title.Substring(0, 100);
            }
            else
            {
                sfdlg.FileName = this.title;
            }

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

            if ((String.IsNullOrEmpty(this.note) == false) && (this.note.Length < 140))
            {
                this.Tweetnote();
                Log.Write(LogType.info, "Note send to twitter.");
            }
            else if (this.note.Length > 140)
            {
                DialogResult result;
                string shrttweet = this.note.Substring(0, 140);
                result = MessageBox.Show("Your note is more than the 140 chars. Do you want to publish only the first 140 characters? ", "too long note", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.Tweetnote();
                    Log.Write(LogType.info, "Shorted note send to twitter.");
                }
            }
            else
            {
                string emptynote = "Note is empty.";
                MessageBox.Show(emptynote);
                Log.Write(LogType.error, emptynote);
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
                MessageBox.Show(notwusername);
                Log.Write(LogType.error, notwusername);
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
                if (twitter.UpdateAsXML(twitteruser, this.twpass, this.note) != null)
                {
                    MessageBox.Show("Your note is Tweeted.");
                }
                else
                {
                    string sendtwfail = "Sending note to twitter failed.";
                    MessageBox.Show(sendtwfail);
                    Log.Write(LogType.error, sendtwfail);
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

                if (dp.X < e.Location.X)
                {
                    if (dp.Y < e.Location.Y)
                    {
                        this.Location = new Point(this.Location.X + 1, this.Location.Y + 1);
                    }
                    else if (dp.Y > e.Location.Y)
                    {
                        this.Location = new Point(this.Location.X + 1, this.Location.Y - 1);
                    }
                    else
                    {
                        this.Location = new Point(this.Location.X + 1, this.Location.Y);
                    }
                }
                else if (dp.X > e.Location.X)
                {
                    if (dp.Y < e.Location.Y)
                    {
                        this.Location = new Point(this.Location.X - 1, this.Location.Y + 1);
                    }
                    else if (dp.Y > e.Location.Y)
                    {
                        this.Location = new Point(this.Location.X - 1, this.Location.Y - 1);
                    }
                    else
                    {
                        this.Location = new Point(this.Location.X - 1, this.Location.Y);
                    }
                }
                else
                {
                    if (dp.Y < e.Location.Y)
                    {
                        this.Location = new Point(this.Location.X, this.Location.Y + 1);
                    }
                    else if (dp.Y > e.Location.Y)
                    {
                        this.Location = new Point(this.Location.X, this.Location.Y - 1);
                    }
                }

                // below comment out the same code, only I consider to it to be slower:
                //if ((dp.X < e.Location.X) && (dp.Y < e.Location.Y))
                //{
                //    this.Location = new Point(this.Location.X + 1, this.Location.Y + 1);
                //}
                //else if ((dp.X > e.Location.X) && (dp.Y > e.Location.Y))
                //{
                //    this.Location = new Point(this.Location.X - 1, this.Location.Y - 1);
                //}
                //else if ((dp.X > e.Location.X) && (dp.Y < e.Location.Y))
                //{
                //    this.Location = new Point(this.Location.X - 1, this.Location.Y + 1);
                //}
                //else if ((dp.X < e.Location.X) && (dp.Y > e.Location.Y))
                //{
                //    this.Location = new Point(this.Location.X + 1, this.Location.Y - 1);
                //}
                //else if (dp.X < e.Location.X)
                //{
                //    this.Location = new Point(this.Location.X + 1, this.Location.Y);
                //}
                //else if (dp.X > e.Location.X)
                //{
                //    this.Location = new Point(this.Location.X - 1, this.Location.Y);
                //}
                //else if (dp.Y < e.Location.Y)
                //{
                //    this.Location = new Point(this.Location.X, this.Location.Y + 1);
                //}
                //else if (dp.Y > e.Location.Y)
                //{
                //    this.Location = new Point(this.Location.X, this.Location.Y - 1);
                //}
            }
            else if (this.skin != null)
            {
                this.pnlHead.BackColor = this.skin.GetObjColor(false);
            }
        }

        #endregion Methods


    }
}