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
        #region Fields (4)

        private Notes notes;
        private Note note;
        private Point oldp;
        private PictureBox pbShowLock;
        private bool moving = false;
        private const int MINVISIBLESIZE = 5;
        
        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the FrmNote class.
        /// </summary>
        /// <param name="note">note data class.</param>
        public FrmNote(Notes notes, Note note)
        {
            this.notes = notes;
            this.note = note;
            this.InitializeComponent();
            this.TopMost = note.Ontop;
            this.menuOnTop.Checked = note.Ontop;
            this.SetBounds(note.X, note.Y, note.Width, note.Height);
            this.menuSendToEmail.Enabled = Settings.SocialEmailEnabled;
            this.menuSendToTwitter.Enabled = Settings.SocialTwitterEnabled;
            this.menuSendToFacebook.Enabled = Settings.SocialFacebookEnabled;
            this.lblTitle.Text = note.Title;
            this.rtbNote.Rtf = note.GetContent();
            if (Settings.HighlightHTML || Settings.HighlightPHP || Settings.HighlightSQL)
            {
                TextHighlight.CheckSyntaxFull(rtbNote);
            }
            this.rtbNote.DetectUrls = Settings.HighlightHyperlinks;
            if (this.rtbNote.DetectUrls)
            {
                this.rtbNote.Text += "";//causes TextChanged event so there is a rescan for URL's:
            }

            string[] skinnames = notes.GetSkinsNames();
            for (int i = 0; i < skinnames.Length; i++)
            {
                ToolStripMenuItem tsi = new ToolStripMenuItem();
                tsi.Name = "menuSkin"+skinnames[i];
                tsi.Text = skinnames[i];
                if (note.SkinNr == i)
                {
                    tsi.Checked = true;
                }
                else
                {
                    tsi.Checked = false;
                }
                tsi.BackColor = notes.GetForegroundColor(i);
                this.menuNoteColors.DropDownItems.Add(tsi);
            }
        }

        #endregion Constructors

        #region Methods (32)

        // Public Methods (1) 

        /// <summary>
        /// Save the setting of the note.
        /// </summary>
        public void UpdateThisNote()
        {
            this.SavePos.RunWorkerAsync();
        }
        // Private Methods (30) 

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
                //this.twpass = new char[passwctrl[0].Text.Length];
                //for (int n = 0; n < passwctrl[0].Text.Length; n++)
                //{
                //    this.twpass[n] = passwctrl[0].Text[n];
                //}
                //if (this.twpass.Length <= 0 || this.twpass == null)
                //{
                //    throw new CustomException("buffer underflow");
                //}
                //if (this.twpass.Length > 255)
                //{
                //    throw new CustomException("password too long.");
                //}
                //passwctrl[0].Name = new Random().Next().ToString();
                //passwctrl[0].Text.Remove(0);
                //frmAskpass.Close();
                //foreach (Control cntrl in frmAskpass.Controls)
                //{
                //    cntrl.Dispose();
                //}
                //frmAskpass.Dispose();
                //GC.Collect();
                //this.Tweetnote();
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

        
#if windows
        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int description, int ReservedValue);

#endif

        /// <summary>
        /// Check if there is internet connection, if not warn user.
        /// </summary>
        /// <returns>true if there is a coonection, otherwise return false</returns>
        private bool CheckConnection()
        {
#if windows
            int desc;
            if (InternetGetConnectedState(out desc, 0))
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
        /// contextMenuStripNoteOptions is closed.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void contextMenuStripNoteOptions_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            this.pnlHead.BackColor = this.notes.GetForegroundColor(this.note.SkinNr);
        }

        /// <summary>
        /// Copy note content to clipboard.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void copyTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.rtbNote.Text))
            {
                Clipboard.SetText(this.rtbNote.Text);
            }
        }

        /// <summary>
        /// Copy note title to clipboard.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void copyTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.lblTitle.Text))
            {
                Clipboard.SetText(this.lblTitle.Text);
            }
        }

        /// <summary>
        /// Edit note is clicked.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void editTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmNewNote frmnewnote = new FrmNewNote(this.notes, this.note);
            frmnewnote.Show();
            this.note.DestroyForm();
        }

        /// <summary>
        /// E-mail an note. Start default mail client with subject and content, if possible.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void emailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string emailnote = System.Web.HttpUtility.UrlEncode(this.rtbNote.Text).Replace("+", " ");
            string emailtitle = System.Web.HttpUtility.UrlEncode(this.lblTitle.Text);

            if (!String.IsNullOrEmpty(emailtitle) && (!String.IsNullOrEmpty(emailnote)))
            {
                System.Diagnostics.Process.Start("mailto:" + Settings.SocialEmailDefaultadres + "?subject=" + this.lblTitle.Text + "&body=" + emailnote);
            }
            else if (!String.IsNullOrEmpty(emailtitle))
            {
                System.Diagnostics.Process.Start("mailto:" + Settings.SocialEmailDefaultadres + "?subject=" + this.lblTitle.Text);
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
            if (Settings.NotesTransparencyEnabled)
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
            if (Settings.NotesTransparencyEnabled)
            {
                this.Opacity = Settings.NotesTransparencyLevel;
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
        /// Lock the note and show a lock.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void locknoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string locknotemsg = "&Lock note";
            note.Locked = !note.Locked;
            if (note.Locked)
            {
                this.CreatePbLock();
                this.menuLockNote.Text = locknotemsg + " (click again to unlock)";
            }
            else
            {
                this.DestroyPbLock();
                this.menuLockNote.Text = locknotemsg;
            }

            this.pbResizeGrip.Visible = !note.Locked;
            this.menuNoteColors.Enabled = !note.Locked;
            this.menuEditNote.Enabled = !note.Locked;
            this.menuOnTop.Enabled = !note.Locked;
            this.menuRollUp.Enabled = !note.Locked;
        }

        /// <summary>
        /// Roll the note up and down.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void menuRollUp_Click(object sender, EventArgs e)
        {
            const string rollupmsg = "&Roll up";
            this.note.RolledUp = !this.note.RolledUp;
            this.menuRollUp.Checked = !this.note.RolledUp;

            if (this.note.RolledUp)
            {
                this.menuRollUp.Text = rollupmsg + "(click again to Roll Down)";
                this.MinimumSize = new Size(this.MinimumSize.Width, this.pnlHead.Height);
                this.Height = this.pnlHead.Height;
            }
            else
            {
                this.menuRollUp.Text = rollupmsg;
                this.MinimumSize = new Size(this.MinimumSize.Width, this.pnlHead.Height + this.pbResizeGrip.Height);
                this.Height = this.pnlHead.Height + this.pnlNote.Height;
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

            if (!this.note.Locked && !this.SavePos.IsBusy)
            {
                this.SavePos.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Get the color of the note and paint it.
        /// </summary>
        //private void PaintColorNote()
        //{
        //    this.skin = new Skin(this.notecolor);
        //    Color normalcolor = this.skin.GetObjColor(false);
        //    this.BackColor = normalcolor;
        //    this.pnlHead.BackColor = normalcolor;
        //    this.pnlNote.BackColor = normalcolor;
        //    this.rtbNote.BackColor = normalcolor;
        //    if (this.notes.TextDirection == 0)
        //    {
        //        this.lblTitle.TextAlign = ContentAlignment.TopLeft;
        //        this.rtbNote.SelectAll(); //fix bug: #0000012
        //        this.rtbNote.SelectionAlignment = HorizontalAlignment.Left;
        //        //this.rtbNote.RightToLeft = RightToLeft.No;
        //    }
        //    else if (this.notes.TextDirection == 1)
        //    {
        //        this.lblTitle.TextAlign = ContentAlignment.TopRight;
        //        this.rtbNote.SelectAll();
        //        this.rtbNote.SelectionAlignment = HorizontalAlignment.Right;
        //        //this.rtbNote.RightToLeft = RightToLeft.Yes; //will make the contextmenu act not right.
        //    }
        //    this.rtbNote.SelectionStart = 0;

        //    this.rtbNote.Font = this.skin.GetFontNoteContent();
        //}

        /// <summary>
        /// Resize note
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void pbResizeGrip_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!this.note.Locked)
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
            if (!this.note.Locked && !this.SavePos.IsBusy)
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
            }

            if (!this.SavePos.IsBusy)
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
            //xmlHandler getSettings = new xmlHandler(true);
            //if (getSettings.getXMLnodeAsBool("askurl"))
            if (Settings.ConfirmLinkclick)
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
            if ((this.Location.X + this.Width > MINVISIBLESIZE) && (this.Location.Y + this.Height > MINVISIBLESIZE))
            {
                string notefilepath = this.notes.NewNoteFilename(this.note.Id, this.note.Title);
                //xmlUtil.WriteNote(notefilepath, this.note, this.rtbNote.Rtf);
            }
            else
            {
                string msgOutOfScreen = "Position note (ID:" + this.note.Id + ") is out of screen.";
                Log.Write(LogType.error, msgOutOfScreen);
                MessageBox.Show(msgOutOfScreen);
            }
            int i = 2;
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
                    //this.notecolor = i;
                    //string notefile = System.IO.Path.Combine(this.notes.NoteSavePath, this.id + ".xml");
                    //xmlHandler savenotecolor = new xmlHandler(notefile);
                    //savenotecolor.WriteNote(this.Visible, this.menuOnTop.Checked, this.notecolor, this.title, this.note, this.locX, this.locY, this.Width, this.Height);
                    //Log.Write(LogType.info, "Color note (ID:" + this.NoteID + ") changed.");
                }
                else
                {
                    curitem.Checked = false;
                }

                i++;
            }
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
            sfdlg.FileName = this.notes.StripForbiddenFilenameChars(this.note.Title);
            sfdlg.Title = "Save note to textfile.";
            sfdlg.Filter = "Textfile (*.txt)|*.txt|Webpage (*.htm)|*.htm";
            if (sfdlg.ShowDialog() == DialogResult.OK)
            {
                //new Textfile(true, sfdlg.FileName, this.note.Title, this.note);
                string logmsg = "Note (ID:" + this.note.Id + ") saved to ";
                switch (sfdlg.FilterIndex)
                {
                    case 0:
                        Log.Write(LogType.info, logmsg + "textfile.");
                        break;
                    case 1:
                        Log.Write(LogType.info, logmsg + "htmlfile.");
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
            if (this.CheckConnection())
            {
                Twitter twitter = new Twitter();
                //TODO: call windows from here.
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlHead_MouseUp(object sender, MouseEventArgs e)
        {
            this.moving = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlHead_MouseMove(object sender, MouseEventArgs e)
        {
            if ((this.moving) && (e.Button == MouseButtons.Left))
            {
                this.pnlHead.BackColor = this.notes.GetBackgroundColor(this.note.SkinNr);

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
            else
            {
                this.pnlHead.BackColor = this.notes.GetForegroundColor(this.note.SkinNr);
            }
        }

        #endregion Methods
    }

}