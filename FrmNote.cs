//-----------------------------------------------------------------------
// <copyright file="FrmNote.cs" company="GNU">
//  NoteFly a note application.
//  Copyright (C) 2010-2011  Tom
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
    using System.IO;
#endif
    /// <summary>
    /// The note class.
    /// </summary>
    public partial class FrmNote : Form
    {
        #region Fields (6)

        private const int MINVISIBLESIZE = 5;
        private bool moving = false;
        private Note note;
        private Notes notes;
        private Point oldp;
        private PictureBox pbShowLock;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the FrmNote class.
        /// </summary>
        /// <param name="notes">notes class</param>
        /// <param name="note">note data class.</param>
        public FrmNote(Notes notes, Note note)
        {
            this.notes = notes;
            this.note = note;
            this.InitializeComponent();

            //this.SuspendLayout();
            this.UpdateForm(false);

            this.lblTitle.Text = note.Title;
            this.BackColor = notes.GetPrimaryClr(note.SkinNr);
            this.pnlHead.BackColor = notes.GetPrimaryClr(note.SkinNr);
            this.rtbNote.BackColor = notes.GetPrimaryClr(note.SkinNr);
            this.rtbNote.DetectUrls = Settings.HighlightHyperlinks;
            if (String.IsNullOrEmpty(this.note.tempcontent))
            {
                this.rtbNote.Rtf = note.GetContent();
            }
            else
            {
                this.rtbNote.Rtf = this.note.tempcontent;
                //clear memory:
                this.note.tempcontent = String.Empty;
                this.note.tempcontent = null;
            }
            this.TopMost = note.Ontop;
            this.menuOnTop.Checked = note.Ontop;
            this.SetRollupNote();
            this.SetLockedNote();
            this.SetBounds(note.X, note.Y, note.Width, note.Height);
            string[] skinnames = notes.GetSkinsNames();
            for (int i = 0; i < skinnames.Length; i++)
            {
                ToolStripMenuItem tsi = new ToolStripMenuItem();
                tsi.Name = "menuSkin" + skinnames[i];
                tsi.Text = skinnames[i];
                if (note.SkinNr == i)
                {
                    tsi.Checked = true;
                }
                else
                {
                    tsi.Checked = false;
                }
                tsi.BackColor = notes.GetPrimaryClr(i);
                tsi.ForeColor = notes.GetTextClr(i);
                tsi.Click += new EventHandler(menuNoteSkins_skin_Click);
                this.menuNoteSkins.DropDownItems.Add(tsi);
            }

            this.UpdateForm(true);
        }

        #endregion Constructors

        #region Methods (28)

        /// <summary>
        /// Set some settings
        /// </summary>
        public void UpdateForm(bool contentset)
        {
            if (!contentset)
            {
                this.rtbNote.ForeColor = notes.GetTextClr(this.note.SkinNr);
                //this.rtbNote.Font = new Font(Settings.FontContentFamily, Settings.FontContentSize);
                this.lblTitle.ForeColor = notes.GetTextClr(this.note.SkinNr);
                if (Settings.FontTitleStylebold)
                {
                    this.lblTitle.Font = new Font(Settings.FontTitleFamily, Settings.FontTitleSize, FontStyle.Bold);
                }
                else
                {
                    if (Settings.FontTitleSize < 6)
                    {
                        Settings.FontTitleSize = 6;
                    }
                    this.lblTitle.Font = new Font(Settings.FontTitleFamily, Settings.FontTitleSize, FontStyle.Regular);
                }
                if (Settings.FontTextdirection == 0)
                {
                    this.rtbNote.RightToLeft = RightToLeft.No;
                }
                else if (Settings.FontTextdirection == 1)
                {
                    this.rtbNote.RightToLeft = RightToLeft.Yes;
                }
                this.menuSendToEmail.Enabled = Settings.SocialEmailEnabled;
                this.menuSendToTwitter.Enabled = Settings.SocialTwitterEnabled;
                this.menuSendToFacebook.Enabled = Settings.SocialFacebookEnabled;
                this.toolTip.Active = Settings.NotesTooltipsEnabled;
            }
            else
            {
                if (this.lblTitle.Height + this.lblTitle.Location.Y > pnlHead.Height)
                {
                    const int maxheightpnlhead = 64;
                    if (this.lblTitle.Height < maxheightpnlhead)
                    {
                        this.pnlHead.Height = this.lblTitle.Height;
                    }
                    else
                    {
                        this.pnlHead.Height = maxheightpnlhead;
                    }
                }
                else
                {
                    const int defaulftminheight = 32;
                    this.pnlHead.Height = defaulftminheight;
                }
                this.pnlNote.Location = new Point(0, pnlHead.Height-1);
                this.pnlNote.Size = new Size(this.Width, this.Height - pnlHead.Height+1);
                if (Settings.HighlightHTML || Settings.HighlightPHP || Settings.HighlightSQL)
                {
                    //Highlight.CheckSyntaxFull(rtbNote, note.SkinNr, notes);
                }
            }
        }

        // Private Methods (28) 

        /// <summary>
        /// The user pressed the cross on the note,
        /// Hide the note.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnCloseNote_Click(object sender, EventArgs e)
        {
            if (Settings.NotesClosebtnHidenotepermanently)
            {
                this.note.Visible = false;
                xmlUtil.WriteNote(this.note, this.notes.GetSkinName(this.note.SkinNr), this.rtbNote.Rtf); //save.
            }
            this.note.DestroyForm();
        }

        /// <summary>
        /// Check if there is internet connection, if not warn user.
        /// Uses windows API, other platforms return always true at the moment.
        /// </summary>
        /// <returns>true if there is a connection, otherwise return false</returns>
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
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void copyTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.lblTitle.Text))
            {
                Clipboard.SetText(this.lblTitle.Text);
            }
        }

        /// <summary>
        /// Requested to edit this note.
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
        /// <param name="sender">Sender object</param>
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
            if (Settings.NotesTransparencyEnabled)
            {
                this.Opacity = 1.0;
            }
        }

        /// <summary>
        /// Form is not active anymore, make transparent if allowed.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void frmNote_Deactivate(object sender, EventArgs e)
        {
            if (Settings.NotesTransparencyEnabled)
            {
                this.Opacity = Settings.NotesTransparencyLevel;
            }
        }

        /// <summary>
        /// Requested to hide this note form.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments</param>
        private void hideNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btnCloseNote_Click(sender, e);
        }

        /// <summary>
        /// A new skin is selected for this note.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void menuNoteSkins_skin_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem curtsi in this.menuNoteSkins.DropDownItems)
            {
                curtsi.Checked = false;
            }
            ToolStripMenuItem tsi = (ToolStripMenuItem)sender;
            tsi.Checked = true;
            this.note.SkinNr = notes.GetSkinNr(tsi.Text);
            this.BackColor = notes.GetPrimaryClr(this.note.SkinNr);
            this.rtbNote.BackColor = notes.GetPrimaryClr(this.note.SkinNr);
            this.pnlHead.BackColor = notes.GetPrimaryClr(this.note.SkinNr);
            this.lblTitle.ForeColor = notes.GetTextClr(this.note.SkinNr);
            this.rtbNote.ForeColor = notes.GetTextClr(this.note.SkinNr);
            if (!this.SavePos.IsBusy)
            {
                this.SavePos.RunWorkerAsync(this.rtbNote.Rtf);
            }
            Log.Write(LogType.info, "Note " + this.note.Filename + " skin changed to "+this.notes.GetSkinName(this.note.SkinNr));
        }

        /// <summary>
        /// Lock the note and show a lock.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void locknoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.note.Locked = !this.note.Locked;

            this.SetLockedNote();

            if (!this.SavePos.IsBusy)
            {
                this.SavePos.RunWorkerAsync(this.rtbNote.Rtf);
            }
        }

        /// <summary>
        /// Lock the note by disabling serval parts of the form.
        /// Set the menuLockNote contextmenu to display correctly.
        /// </summary>
        private void SetLockedNote()
        {
            this.menuLockNote.Checked = note.Locked;
            const string locknotemsg = "&Lock note";
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
            this.menuNoteSkins.Enabled = !note.Locked;
            this.menuEditNote.Enabled = !note.Locked;
            this.menuOnTop.Enabled = !note.Locked;
            this.menuRollUp.Enabled = !note.Locked;
        }

        /// <summary>
        /// Requested to rollup or rolldown the note form.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void menuRollUp_Click(object sender, EventArgs e)
        {
            this.note.RolledUp = !this.note.RolledUp;

            this.SetRollupNote();

            if (!this.SavePos.IsBusy)
            {
                this.SavePos.RunWorkerAsync(this.rtbNote.Rtf);
            }
        }

        /// <summary>
        /// Roll up the note, by setting the height of the form.
        /// Set the menuRollUp contextmenu to display correctly.
        /// </summary>
        private void SetRollupNote()
        {
            this.menuRollUp.Checked = this.note.RolledUp;
            const string rollupmsg = "&Roll up";
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
                this.Height = note.Height;
            }
        }

        /// <summary>
        /// Make a note on top (and save note) CheckOnClick is set to true.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments</param>
        private void OnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.note.Ontop = !this.note.Ontop;
            this.menuOnTop.Checked = this.note.Ontop;

            this.TopMost = this.note.Ontop;
            if (!this.SavePos.IsBusy)
            {
                this.SavePos.RunWorkerAsync(this.rtbNote.Rtf);
            }
        }

        /// <summary>
        /// Resize the note form because dragging.
        /// </summary>
        /// <param name="sender">Sender object</param>
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
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void pbResizeGrip_MouseUp(object sender, MouseEventArgs e)
        {
            this.note.Width = this.Width;
            this.note.Height = this.Height;

            if (!this.note.Locked && !this.SavePos.IsBusy)
            {
                this.SavePos.RunWorkerAsync(this.rtbNote.Rtf);
            }
        }

        /// <summary>
        /// pnlHead the grab area of the note is selected.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void pnlHead_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.pnlHead.BackColor = notes.GetSelectClr(note.SkinNr);
                this.moving = true;
                this.oldp = e.Location;
            }
        }

        /// <summary>
        /// The note is dragged with pnlHead.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void pnlHead_MouseMove(object sender, MouseEventArgs e)
        {
            if ((this.moving) && (e.Button == MouseButtons.Left))
            {
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
                this.Location = new Point(this.Location.X + dpx, this.Location.Y + dpy);
            }
        }

        /// <summary>
        /// Stoped moving note, save position.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void pnlHead_MouseUp(object sender, MouseEventArgs e)
        {
            this.note.X = this.Location.X;
            this.note.Y = this.Location.Y;

            this.moving = false;
            if (!this.SavePos.IsBusy)
            {
                this.SavePos.RunWorkerAsync(this.rtbNote.Rtf);
            }
            this.pnlHead.BackColor = notes.GetPrimaryClr(note.SkinNr);
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
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void rtbNote_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Program.LoadLink(e.LinkText);
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
                string notefilepath = Path.Combine(Settings.NotesSavepath, this.note.Filename);
                this.note.X = this.Location.X;
                this.note.Y = this.Location.Y;
                string rtf = (string)e.Argument;
                xmlUtil.WriteNote(this.note, notes.GetSkinName(this.note.SkinNr), rtf);
            }
            else
            {
                const string msgOutOfScreen = "Note not saved. Position note is out of screen.";
                Log.Write(LogType.error, msgOutOfScreen);
            }
        }

        /// <summary>
        /// Send note to Facebook.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void tsmenuSendToFacebook_Click(object sender, EventArgs e)
        {
            if (this.CheckConnection())
            {
                Facebook facebook = new Facebook();
                //TODO: call windows from here.
            }
        }


        /// <summary>
        /// Send note to twitter.
        /// </summary>
        /// <param name="sender">Sender object</param>
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
        /// Save the note to a plain textfile.
        /// </summary>
        /// <param name="sender">Sender object</param>
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
                string logmsg = "Note saved to ";
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
            this.pbShowLock.MouseDown += new MouseEventHandler(pnlHead_MouseDown);
            this.pbShowLock.MouseMove += new MouseEventHandler(pnlHead_MouseMove);
            this.pbShowLock.MouseUp += new MouseEventHandler(pnlHead_MouseUp);
            this.pnlHead.Controls.Add(this.pbShowLock);
            this.pbShowLock.BringToFront();
        }

        /// <summary>
        /// Removes the lock picture and free the memory of the picture.
        /// </summary>
        private void DestroyPbLock()
        {
            if (pbShowLock != null)
            {
                this.pbShowLock.Visible = false;
                this.pbShowLock.Dispose();
            }
            GC.Collect();
        }

        /// <summary>
        /// Check if some text is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFrmNoteOptions_Opening(object sender, CancelEventArgs e)
        {
            if (rtbNote.SelectedText.Length >= 1)
            {
                this.menuCopySelected.Enabled = true;
            }
            else
            {
                this.menuCopySelected.Enabled = false;
            }
        }

        /// <summary>
        /// Copy the selected text in the rtbNote control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuCopySelected_Click(object sender, EventArgs e)
        {
            if (rtbNote.SelectedText.Length >= 1)
            {
                Clipboard.SetText(this.rtbNote.SelectedText);
            }
        }

        #endregion Methods

#if windows
        [DllImport("wininet.dll", EntryPoint = "InternetGetConnectedState")] // C:\windows\wininet.dll
        private static extern bool InternetGetConnectedState(out int description, int ReservedValue);
#endif
    }

}