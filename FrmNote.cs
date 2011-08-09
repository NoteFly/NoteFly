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
namespace NoteFly
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
#if windows
    using System.Runtime.InteropServices;
#endif
    using System.Windows.Forms;

    /// <summary>
    /// Note window.
    /// </summary>
    public partial class FrmNote : Form
    {
        #region Fields (6)

        /// <summary>
        /// Constant for minimal visible
        /// </summary>
        private const int MINVISIBLESIZE = 5;

        /// <summary>
        /// is form movind
        /// </summary>
        private bool moving = false;

        /// <summary>
        /// reference to note object
        /// </summary>
        private Note note;

        /// <summary>
        /// reference to notes object
        /// </summary>
        private Notes notes;

        /// <summary>
        /// the old position of the note.
        /// </summary>
        private Point oldp;

        /// <summary>
        /// Lock icon picturebox
        /// </summary>
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
            this.InitializeComponent();
            this.notes = notes;
            this.note = note;
            this.UpdateForm(false);
            this.lblTitle.Text = note.Title;
            this.BackColor = notes.GetPrimaryClr(note.SkinNr);
            if (notes.GetPrimaryTexture(note.SkinNr) != null)
            {
                this.BackgroundImageLayout = ImageLayout.Tile;
                this.BackgroundImage = notes.GetPrimaryTexture(note.SkinNr);
            }
            else
            {
                this.BackgroundImage = null;
            }

            this.pnlHead.BackColor = Color.Transparent;
            //this.pnlHead.BackColor = notes.GetPrimaryClr(note.SkinNr);
            this.rtbNote.BackColor = notes.GetPrimaryClr(note.SkinNr);
            try
            {
                if (string.IsNullOrEmpty(this.note.Tempcontent))
                {
                    this.rtbNote.Rtf = note.GetContent();
                }
                else
                {
                    this.rtbNote.Rtf = this.note.Tempcontent;
                    this.note.Tempcontent = string.Empty;
                    this.note.Tempcontent = null;
                }
            }
            catch (ArgumentException argexc)
            {
                Log.Write(LogType.exception, "note " + note.Filename + ": " + argexc.Message);
            }

            this.TopMost = note.Ontop;
            this.menuOnTop.Checked = note.Ontop;
            this.SetBounds(note.X, note.Y, note.Width, note.Height);
            this.SetLockedNote();
            this.SetRollupNote();
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

#if windows
                // FIXME: Setting backcolor of a ToolStripMenuItem did not work under Mono.
                tsi.BackColor = this.notes.GetPrimaryClr(i);
                tsi.ForeColor = this.notes.GetTextClr(i);
#endif
                tsi.Click += new EventHandler(this.menuNoteSkins_skin_Click);
                this.menuNoteSkins.DropDownItems.Add(tsi);
            }

            this.UpdateForm(true);
        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// Gets the note content as rich text.
        /// </summary>
        public string GetContentRTF
        {
            get
            {
                return this.rtbNote.Rtf;
            }
        }

        #endregion Properties

        #region Methods (30)

        /// <summary>
        /// Set some settings
        /// </summary>
        /// <param name="contentset">boolean if the note rtf content already set.</param>
        public void UpdateForm(bool contentset)
        {
            if (!contentset)
            {
                if (!Settings.NotesTransparencyEnabled)
                {
                    this.Opacity = 1.0;
                }

                this.lblTitle.ForeColor = this.notes.GetTextClr(this.note.SkinNr);
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
                    this.lblTitle.RightToLeft = RightToLeft.No;
                    this.rtbNote.RightToLeft = RightToLeft.No;
                }
                else if (Settings.FontTextdirection == 1)
                {
                    this.lblTitle.RightToLeft = RightToLeft.Yes;
                    this.rtbNote.RightToLeft = RightToLeft.Yes;
                }

                this.menuSendToEmail.Enabled = Settings.SocialEmailEnabled;
                this.toolTip.Active = Settings.NotesTooltipsEnabled;
            }
            else
            {
                if (this.lblTitle.Height + this.lblTitle.Location.Y > this.pnlHead.Height)
                {
                    const int MAXHEIGHTPNLHEAD = 64;
                    if (this.lblTitle.Height < MAXHEIGHTPNLHEAD)
                    {
                        this.pnlHead.Height = this.lblTitle.Height;
                    }
                    else
                    {
                        this.pnlHead.Height = MAXHEIGHTPNLHEAD;
                    }
                }
                else
                {
                    const int DEFAULFTMINHEIGHT = 32;
                    this.pnlHead.Height = DEFAULFTMINHEIGHT;
                }

#if windows
                this.pnlNote.Location = new Point(0, this.pnlHead.Height - 1);
                this.pnlNote.Size = new Size(this.Width, (this.Height - this.pnlHead.Height + 1));
#elif linux
                this.pnlNote.Location = new Point(0, this.pnlHead.Height - 1);
                this.pnlNote.Size = new Size(this.Width - 6, (this.Height - this.pnlHead.Height - 5));
#endif
                this.rtbNote.DetectUrls = Settings.HighlightHyperlinks;
                if (!SyntaxHighlight.KeywordsInitialized)
                {
                    SyntaxHighlight.InitHighlighter();
                }

                SyntaxHighlight.CheckSyntaxFull(this.rtbNote, this.note.SkinNr, this.notes);
            }
        }

#if windows
        [DllImport("wininet.dll", EntryPoint = "InternetGetConnectedState")] // C:\windows\wininet.dll
        private static extern bool InternetGetConnectedState(out int description, int ReservedValue);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        private const int WM_SETREDRAW = 0x0b;
#endif

        /// <summary>
        /// The user pressed the cross on the note,
        /// Hide the note.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnHideNote_Click(object sender, EventArgs e)
        {
            if (Settings.NotesClosebtnHidenotepermanently)
            {
                this.note.Visible = false;
                xmlUtil.WriteNote(this.note, this.notes.GetSkinName(this.note.SkinNr), this.rtbNote.Rtf);
            }

            this.note.DestroyForm();
        }

        /// <summary>
        /// Menu plugin clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuPluginClicked(Object sender, System.EventArgs e)
        {
            try
            {
                ToolStripMenuItem menuitem = (ToolStripMenuItem)sender;
                int p = (int)menuitem.Tag;
                Program.plugins[p].ShareMenuClicked(this.rtbNote, this.note);
            }
            catch (Exception exc)
            {
                Log.Write(LogType.exception, exc.Message);
            }
        }

        /// <summary>
        /// Check if there is internet connection, if not warn user.
        /// Uses windows API, other platforms return always true at the moment.
        /// </summary>
        /// <remarks>Decreated, used for send to twitter/facebook</remarks>
        /// <returns>true if there is a connection, otherwise return false</returns>
        /*
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
                const string MSGNONETWORK = "There is no network connection.";
                Log.Write(LogType.error, MSGNONETWORK);
                MessageBox.Show(MSGNONETWORK);
                return false;
            }
#elif !windows
            return true;
#endif
        }
        */

        /// <summary>
        /// Copy note content to clipboard.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void menuCopyContent_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.rtbNote.Text))
            {
                Clipboard.SetText(this.rtbNote.Text);
            }
        }

        /// <summary>
        /// Copy note title to clipboard.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void menuCopyTitle_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.lblTitle.Text))
            {
                Clipboard.SetText(this.lblTitle.Text);
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
            this.pbShowLock.Location = new Point((this.btnHideNote.Location.X - 24), 8);
            this.pbShowLock.Image = new Bitmap(NoteFly.Properties.Resources.locknote);
            this.pbShowLock.Visible = true;
            this.pbShowLock.MouseDown += new MouseEventHandler(this.pnlHead_MouseDown);
            this.pbShowLock.MouseMove += new MouseEventHandler(this.pnlHead_MouseMove);
            this.pbShowLock.MouseUp += new MouseEventHandler(this.pnlHead_MouseUp);
            this.pnlHead.Controls.Add(this.pbShowLock);
            this.pbShowLock.BringToFront();
        }

        /// <summary>
        /// Removes the lock picture and free the memory of the picture.
        /// </summary>
        private void DestroyPbLock()
        {
            if (this.pbShowLock != null)
            {
                this.pbShowLock.Visible = false;
                this.pbShowLock.Dispose();
            }

            GC.Collect();
        }

        /// <summary>
        /// Requested to edit this note.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void menuEditNote_Click(object sender, EventArgs e)
        {
            FrmNewNote frmnewnote = new FrmNewNote(this.notes, this.note, this.Location, this.Size, this.rtbNote.WordWrap);
            frmnewnote.Show();
            this.note.DestroyForm();
        }

        /// <summary>
        /// E-mail an note. Start default mail client with subject and content, if possible.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void menuSendToEmail_Click(object sender, EventArgs e)
        {
            string emailnote = System.Web.HttpUtility.UrlEncode(this.rtbNote.Text).Replace("+", " ");
            string emailtitle = System.Web.HttpUtility.UrlEncode(this.lblTitle.Text);

            try
            {
                if (!string.IsNullOrEmpty(emailtitle) && (!string.IsNullOrEmpty(emailnote)))
                {
                    System.Diagnostics.Process.Start("mailto:" + Settings.SocialEmailDefaultadres + "?subject=" + this.lblTitle.Text + "&body=" + emailnote);
                }
                else if (!string.IsNullOrEmpty(emailtitle))
                {
                    System.Diagnostics.Process.Start("mailto:" + Settings.SocialEmailDefaultadres + "?subject=" + this.lblTitle.Text);
                }
                else
                {
                    const string MSGNOTITLECONTENT = "Note has no title and content.";
                    Log.Write(LogType.error, MSGNOTITLECONTENT);
                    MessageBox.Show(MSGNOTITLECONTENT);
                }
            }
            catch (AccessViolationException accexc)
            {
                const string MSGCANTLAUNCHEMAILPROTOCOLHANDLER = "Access denied. Can't lauch email client by protocol handler";
                Log.Write(LogType.exception, accexc.Message);
                MessageBox.Show(MSGCANTLAUNCHEMAILPROTOCOLHANDLER);
            }
        }

        /// <summary>
        /// Form got focus, remove transparency.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void FrmNote_Activated(object sender, EventArgs e)
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
        private void FrmNote_Deactivate(object sender, EventArgs e)
        {
            if (Settings.NotesTransparencyEnabled)
            {
                this.Opacity = Settings.NotesTransparencyLevel;
            }
        }

        /// <summary>
        /// FrmNote is closed.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">FormClosedEvent arguments</param>
        private void FrmNote_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.notes.FrmManageNotesNeedUpdate = true;
            TrayIcon.RefreshFrmManageNotes();
        }

        /// <summary>
        /// FrmNote is closing
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">FormClosingEvent arguments</param>
        private void FrmNote_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!e.Cancel)
            {
                this.note.Visible = false;
                this.note.Tempcontent = null;
            }
        }

        /// <summary>
        /// Requested to hide this note form.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments</param>
        private void menuHideNote_Click(object sender, EventArgs e)
        {
            this.btnHideNote_Click(sender, e);
        }

        /// <summary>
        /// Lock the note and show a lock.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void menuLockNote_Click(object sender, EventArgs e)
        {
            this.note.Locked = !this.note.Locked;

            this.SetLockedNote();

            if (!this.saveWorker.IsBusy)
            {
                this.saveWorker.RunWorkerAsync(this.rtbNote.Rtf);
            }
        }

        /// <summary>
        /// Copy the selected text in the rtbNote control
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void menuCopySelected_Click(object sender, EventArgs e)
        {
            if (this.rtbNote.SelectedText.Length >= 1)
            {
                Clipboard.SetText(this.rtbNote.SelectedText);
            }
        }

        /// <summary>
        /// Check if some text is selected.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Cancel event arguments</param>
        private void menuFrmNoteOptions_Opening(object sender, CancelEventArgs e)
        {
            if (this.rtbNote.SelectedText.Length >= 1)
            {
                this.menuCopySelected.Enabled = true;
            }
            else
            {
                this.menuCopySelected.Enabled = false;
            }
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
            this.note.SkinNr = this.notes.GetSkinNr(tsi.Text);
            this.SuspendLayout();
            this.BackColor = this.notes.GetPrimaryClr(this.note.SkinNr);
            if (notes.GetPrimaryTexture(note.SkinNr) != null)
            {
                this.BackgroundImageLayout = ImageLayout.Tile;
                this.BackgroundImage = notes.GetPrimaryTexture(note.SkinNr);
            }
            else
            {
                this.BackgroundImage = null;
            }
            //this.pnlHead.BackColor = this.notes.GetPrimaryClr(this.note.SkinNr);
            this.pnlHead.BackColor = Color.Transparent;
            this.rtbNote.BackColor = this.notes.GetPrimaryClr(this.note.SkinNr);
            this.lblTitle.ForeColor = this.notes.GetTextClr(this.note.SkinNr);
            this.notes.FrmManageNotesNeedUpdate = true;
            TrayIcon.RefreshFrmManageNotes();
            if (!SyntaxHighlight.KeywordsInitialized)
            {
                SyntaxHighlight.InitHighlighter();
            }
#if DEBUG
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
#endif
#if windows
            SendMessage(this.Handle, WM_SETREDRAW, (IntPtr)0, IntPtr.Zero); // about ~900 ms advantage
#endif
            SyntaxHighlight.CheckSyntaxFull(this.rtbNote, this.note.SkinNr, this.notes);
            this.ResumeLayout();
#if windows
            SendMessage(this.Handle, WM_SETREDRAW, (IntPtr)1, IntPtr.Zero);
            this.Refresh();
#endif
#if DEBUG
            stopwatch.Stop();
            Log.Write(LogType.info, "Note highlight time: " + stopwatch.ElapsedMilliseconds.ToString() + " ms");
#endif
            SyntaxHighlight.DeinitHighlighter();
            if (!this.saveWorker.IsBusy)
            {
                this.saveWorker.RunWorkerAsync(this.rtbNote.Rtf);
            }
            Log.Write(LogType.info, "Note " + this.note.Filename + " skin changed to " + this.notes.GetSkinName(this.note.SkinNr));
        }

        /// <summary>
        /// Requested to rollup or rolldown the note form.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void menuRollUp_Click(object sender, EventArgs e)
        {
            this.note.RolledUp = !this.note.RolledUp;

            this.SetRollupNote();

            if (!this.saveWorker.IsBusy)
            {
                this.saveWorker.RunWorkerAsync(this.rtbNote.Rtf);
            }
        }

        /// <summary>
        /// Make a note on top (and save note) CheckOnClick is set to true.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments</param>
        private void menuOnTop_Click(object sender, EventArgs e)
        {
            this.note.Ontop = !this.note.Ontop;
            this.menuOnTop.Checked = this.note.Ontop;

            this.TopMost = this.note.Ontop;
            if (!this.saveWorker.IsBusy)
            {
                this.saveWorker.RunWorkerAsync(this.rtbNote.Rtf);
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

            if (!(this.note.Locked && this.saveWorker.IsBusy))
            {
                this.saveWorker.RunWorkerAsync(this.rtbNote.Rtf);
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
                this.pnlHead.BackColor = this.notes.GetSelectClr(this.note.SkinNr);
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
            if (this.moving && e.Button == MouseButtons.Left)
            {
                int dpx = e.Location.X - this.oldp.X;
                int dpy = e.Location.Y - this.oldp.Y;
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
            if (!this.saveWorker.IsBusy)
            {
                this.saveWorker.RunWorkerAsync(this.rtbNote.Rtf);
            }

            this.pnlHead.BackColor = Color.Transparent;
            //this.notes.GetPrimaryClr(this.note.SkinNr);
        }

        /// <summary>
        /// Hyperlink clicked.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void rtbNote_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Program.LoadLink(e.LinkText, true);
        }

        /// <summary>
        /// Thread to save note settings.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void SavePos_DoWork(object sender, DoWorkEventArgs e)
        {
            if ((this.Location.X + this.Width > MINVISIBLESIZE) && (this.Location.Y + this.Height > MINVISIBLESIZE))
            {
                this.note.X = this.Location.X;
                this.note.Y = this.Location.Y;
                string rtf = (string)e.Argument;
                xmlUtil.WriteNote(this.note, this.notes.GetSkinName(this.note.SkinNr), rtf);
            }
            else
            {
                Log.Write(LogType.error, "Note " + this.note.Filename + " not saved. Position note is out of screen.");
            }
        }

        /// <summary>
        /// Lock the note by disabling serval parts of the form.
        /// Set the menuLockNote contextmenu to display correctly.
        /// </summary>
        private void SetLockedNote()
        {
            this.menuLockNote.Checked = this.note.Locked;
            const string LOCKNOTEMSG = "&Lock note";
            const string CLICKTOUNLOCK = " (click again to unlock)";
            if (this.note.Locked)
            {
                this.CreatePbLock();
                this.menuLockNote.Text = LOCKNOTEMSG + CLICKTOUNLOCK;
            }
            else
            {
                this.DestroyPbLock();
                this.menuLockNote.Text = LOCKNOTEMSG;
            }

            this.pbResizeGrip.Visible = !this.note.Locked;
            this.menuNoteSkins.Enabled = !this.note.Locked;
            this.menuEditNote.Enabled = !this.note.Locked;
            this.menuOnTop.Enabled = !this.note.Locked;
            this.menuRollUp.Enabled = !this.note.Locked;
        }

        /// <summary>
        /// Roll up the note, by setting the height of the form.
        /// Set the menuRollUp contextmenu to display correctly.
        /// </summary>
        private void SetRollupNote()
        {
            this.menuRollUp.Checked = this.note.RolledUp;
            const string ROLLUPMSG = "&Roll up";
            const string CLICKTOROLLUP = "(click again to Roll Down)";
            if (this.note.RolledUp)
            {
                this.menuRollUp.Text = ROLLUPMSG + CLICKTOROLLUP;
                this.MinimumSize = new Size(this.MinimumSize.Width, this.pnlHead.Height);
                this.Height = this.pnlHead.Height;
            }
            else
            {
                this.menuRollUp.Text = ROLLUPMSG;
                this.MinimumSize = new Size(this.MinimumSize.Width, this.pnlHead.Height + this.pbResizeGrip.Height);
                this.Height = this.note.Height;
            }
        }

        /// <summary>
        /// Save the note to a file.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void menuSendToFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfdlg = new SaveFileDialog();
            sfdlg.DefaultExt = "txt";
            sfdlg.AddExtension = true;
            sfdlg.ValidateNames = true;
            sfdlg.CheckPathExists = true;
            sfdlg.OverwritePrompt = true;
            sfdlg.FileName = this.notes.StripForbiddenFilenameChars(this.note.Title);
            sfdlg.Title = "Save note to file";
            sfdlg.Filter = "Textfile (*.txt)|*.txt|RichTextFormat file (*.rtf)|*.rtf|Webpage (*.htm)|*.htm|PHP file (*.php)|*.php";
            if (sfdlg.ShowDialog() == DialogResult.OK)
            {
                string logmsg = "Note saved to ";
                switch (sfdlg.FilterIndex)
                {
                    case 1:
                        new Textfile(TextfileWriteType.exporttext, sfdlg.FileName, this.note.Title, this.rtbNote.Text);
                        Log.Write(LogType.info, logmsg + "textfile.");
                        break;
                    case 2:
                        new Textfile(TextfileWriteType.exportrtf, sfdlg.FileName, this.note.Title, this.GetContentRTF);
                        Log.Write(LogType.info, logmsg + "rtf file.");
                        break;
                    case 3:
                        new Textfile(TextfileWriteType.exporthtml, sfdlg.FileName, this.note.Title, this.rtbNote.Text);
                        Log.Write(LogType.info, logmsg + "htmlfile.");
                        break;
                    case 4:
                        new Textfile(TextfileWriteType.exportphp, sfdlg.FileName, this.note.Title, this.rtbNote.Text);
                        Log.Write(LogType.info, logmsg + "phpfile.");
                        break;
                }
            }
        }

        /// <summary>
        /// Toggle to wrap words in the note content.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void menuWordWrap_Click(object sender, EventArgs e)
        {
            this.rtbNote.WordWrap = this.menuWordWrap.Checked;
        }

        /// <summary>
        /// Create plugin sendto menu's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuSendTo_DropDownOpening(object sender, EventArgs e)
        {
            bool giveup = false;
            while (this.menuSendTo.DropDownItems.Count > 2 && !giveup)
            {
                try
                {
                    this.menuSendTo.DropDownItems.RemoveAt(2);
                }
                catch (Exception ex)
                {
                    giveup = true;
                    Log.Write(LogType.exception, ex.Message);
                }
            }

            if (Program.plugins != null)
            {
                for (int i = 0; i < Program.plugins.Length; i++)
                {
                    if (!String.IsNullOrEmpty(Program.plugins[i].ShareMenuText))
                    {
                        ToolStripMenuItem menuitem = new ToolStripMenuItem(Program.plugins[i].ShareMenuText, null, new EventHandler(MenuPluginClicked));
                        menuitem.Name = "menuPlugin" + Program.plugins[i].Name;
                        menuitem.Tag = i;
                        this.menuSendTo.DropDownItems.Add(menuitem);
                    }
                }
            }
        }

        #endregion Methods
    }
}