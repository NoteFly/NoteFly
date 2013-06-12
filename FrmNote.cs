//-----------------------------------------------------------------------
// <copyright file="FrmNote.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2010-2013  Tom
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
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// Note window.
    /// </summary>
    public sealed partial class FrmNote : Form
    {
        #region Fields (6)

        /// <summary>
        /// Constant for minimal visible
        /// </summary>
        private const int MINVISIBLESIZE = 5;

        /// <summary>
        /// Is form moving.
        /// </summary>
        private bool moving = false;

        /// <summary>
        /// Reference to note object.
        /// </summary>
        private Note note;

        /// <summary>
        /// Reference to notes object.
        /// </summary>
        private Notes notes;

        /// <summary>
        /// The old position of the note.
        /// </summary>
        private Point oldp;

        /// <summary>
        /// Lock icon picturebox
        /// </summary>
        private PictureBox pbShowLock;

        private ToolTip tooltip;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the FrmNote class.
        /// </summary>
        /// <param name="notes">Notes class</param>
        /// <param name="note">Note data class.</param>
        public FrmNote(Notes notes, Note note)
        {
            this.DoubleBuffered = Settings.ProgramFormsDoublebuffered;
            this.InitializeComponent();
            this.notes = notes;
            this.note = note;
            this.UpdateForm(false);
            Strings.TranslateForm(this);
            this.lblTitle.Text = note.Title;
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

            this.SetTopmostNote();
            this.SetBounds(note.X, note.Y, note.Width, note.Height);
            this.SetLockedNote();
            this.SetRollupNote();
            this.SetWordwarpNote();
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
        /// Set some application wide note settings on this note (fonts, transparency, detect hyperlinks etc.) and
        /// do full syntax check on the note content again.
        /// </summary>
        /// <param name="contentset">Boolean if the note rtf content already set.</param>
        public void UpdateForm(bool contentset)
        {
            if (!contentset)
            {
                this.BackColor = this.notes.GetPrimaryClr(this.note.SkinNr);
                if (this.notes.GetPrimaryTexture(this.note.SkinNr) != null)
                {
                    this.BackgroundImageLayout = this.notes.GetPrimaryTextureLayout(this.note.SkinNr);
                    this.BackgroundImage = this.notes.GetPrimaryTexture(this.note.SkinNr);
                }
                else
                {
                    this.BackgroundImage = null;
                }

                this.pnlHead.BackColor = Color.Transparent;
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

                this.menuSendToEmail.Enabled = Settings.SharingEmailEnabled;
            }
            else
            {
                if (this.lblTitle.Height + this.lblTitle.Location.Y >= this.pnlHead.Height)
                {
                    if (this.lblTitle.Height < Settings.NotesTitlepanelMaxHeight)
                    {
                        this.pnlHead.Height = this.lblTitle.Height;
                    }
                    else
                    {
                        this.pnlHead.Height = Settings.NotesTitlepanelMaxHeight;
                    }
                }
                else
                {
                    this.pnlHead.Height = Settings.NotesTitlepanelMinHeight;
                }

                if (Program.CurrentOS == Program.OS.WINDOWS)
                {
                    this.pnlNote.Location = new Point(0, this.pnlHead.Height - 1);
                    this.pnlNote.Size = new Size(this.Width, this.Height - this.pnlHead.Height + 1);
                }
                else if (Program.CurrentOS == Program.OS.LINUX) 
                {
                    this.pnlNote.Location = new Point(0, this.pnlHead.Height - 1);
                    this.pnlNote.Size = new Size(this.Width - 6, this.Height - this.pnlHead.Height - 5);
                }

                this.rtbNote.DetectUrls = Settings.HighlightHyperlinks;
                if (!SyntaxHighlight.KeywordsInitialized)
                {
                    SyntaxHighlight.InitHighlighter();
                }

                this.SetFormTooltips();
                this.CreateSkinsMenu(true);
                SyntaxHighlight.CheckSyntaxFull(this.rtbNote, this.note.SkinNr, this.notes);
            }
        }

        /// <summary>
        /// Set all form tooltips if tooltips are enabled.
        /// </summary>
        private void SetFormTooltips()
        {
            if (Settings.NotesTooltipsEnabled)
            {
                this.tooltip = new ToolTip(this.components);
                this.tooltip.SetToolTip(this.btnHideNote, Strings.T("Hide this note"));
            }
            else
            {
                if (this.tooltip != null)
                {
                    this.tooltip.Active = false;
                    this.tooltip.Dispose();
                }                
            }
        }

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
        /// <param name="toolstripmenuitem">The menu item clicked</param>
        /// <param name="e">Event arguments</param>
        private void menuSharePluginClicked(object toolstripmenuitem, System.EventArgs e)
        {
            try
            {
                ToolStripMenuItem menuitem = (ToolStripMenuItem)toolstripmenuitem;
                int p = (int)menuitem.Tag;
                if (PluginsManager.EnabledPlugins != null)
                {
                    PluginsManager.EnabledPlugins[p].ShareMenuClicked(this.rtbNote, this.note.Title);
                }
            }
            catch (Exception exc)
            {
                Log.Write(LogType.exception, exc.Message);
            }
        }

        /// <summary>
        /// Copy note content to clipboard.
        /// </summary>
        /// <param name="sender">Sender object</param>
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
        /// Create the skin select menu.
        /// Recreate dropdownitems if the list of skins has a different numbered.
        /// </summary>
        /// <param name="alwaysrecreate">If always recreate is true then all dropboxitems are always removed and added again.</param>
        private void CreateSkinsMenu(bool alwaysrecreate)
        {
            if (this.notes.CountSkins != this.menuNoteSkins.DropDownItems.Count || alwaysrecreate)
            {
                this.menuNoteSkins.DropDownItems.Clear();
                string[] skinnames = this.notes.GetSkinsNames();
                for (int i = 0; i < skinnames.Length; i++)
                {
                    ToolStripMenuItem tsi = new ToolStripMenuItem();
                    tsi.Name = "menuSkin" + skinnames[i];
                    tsi.Text = skinnames[i];
                    if (this.note.SkinNr == i)
                    {
                        tsi.Checked = true;
                    }
                    else
                    {
                        tsi.Checked = false;
                    }

                    if (Program.CurrentOS == Program.OS.WINDOWS)
                    {
                        // FIXME: Setting backcolor of a ToolStripMenuItem did not work under Mono.
                        tsi.BackColor = this.notes.GetPrimaryClr(i);
                        tsi.ForeColor = this.notes.GetTextClr(i);
                    }

                    tsi.Click += new EventHandler(this.menuNoteSkins_skin_Click);
                    this.menuNoteSkins.DropDownItems.Add(tsi);
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
            this.pbShowLock.Location = new Point(this.btnHideNote.Location.X - 24, 8);
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
        /// <param name="sender">Sender object</param>
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
                    System.Diagnostics.Process.Start("mailto:" + Settings.SharingEmailDefaultadres + "?subject=" + this.lblTitle.Text + "&body=" + emailnote);
                }
                else if (!string.IsNullOrEmpty(emailtitle))
                {
                    System.Diagnostics.Process.Start("mailto:" + Settings.SharingEmailDefaultadres + "?subject=" + this.lblTitle.Text);
                }
                else
                {
                    string note_msgnotitlecontent = Strings.T("Note has no title and no content.");
                    Log.Write(LogType.error, note_msgnotitlecontent);
                    MessageBox.Show(note_msgnotitlecontent);
                }
            }
            catch (Win32Exception w32exc)
            {
                string note_msgcantlaunchemailprotocolhandler = Strings.T("Can't launch email client.");
                Log.Write(LogType.exception, w32exc.Message);
                MessageBox.Show(note_msgcantlaunchemailprotocolhandler, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Form got focus, remove transparency.
        /// </summary>
        /// <param name="sender">Sender object</param>
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
            Program.Formmanager.FrmManageNotesNeedUpdate = true;
            Program.Formmanager.RefreshFrmManageNotes();
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

            while (this.menuFrmNoteOptions.Items.Count > 9)
            {
                this.menuFrmNoteOptions.Items.RemoveAt(9);
            }

            if (PluginsManager.EnabledPlugins != null)
            {
                for (int p = 0; p < PluginsManager.EnabledPlugins.Count; p++)
                {
                    if (PluginsManager.EnabledPlugins[p].InitFrmNoteMenu() != null)
                    {
                        this.menuFrmNoteOptions.Items.Add(PluginsManager.EnabledPlugins[p].InitFrmNoteMenu());
                    }
                }
            }
        }

        /// <summary>
        /// A new skin is selected for this note.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void menuNoteSkins_skin_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            foreach (ToolStripMenuItem curtsi in this.menuNoteSkins.DropDownItems)
            {
                curtsi.Checked = false;
            }

            ToolStripMenuItem tsi = null;
            try
            {
                tsi = (ToolStripMenuItem)sender;
            }
            catch (InvalidCastException invcastexc)
            {
                Log.Write(LogType.exception, invcastexc.Message);
                return;
            }

            tsi.Checked = true;
            this.note.SkinNr = this.notes.GetSkinNr(tsi.Text);
            this.BackColor = this.notes.GetPrimaryClr(this.note.SkinNr);
            if (this.notes.GetPrimaryTexture(this.note.SkinNr) != null)
            {
                this.BackgroundImageLayout = this.notes.GetPrimaryTextureLayout(this.note.SkinNr);
                this.BackgroundImage = this.notes.GetPrimaryTexture(this.note.SkinNr);
            }
            else
            {
                this.BackgroundImage = null;
            }

            this.pnlHead.BackColor = Color.Transparent;
            this.rtbNote.BackColor = this.notes.GetPrimaryClr(this.note.SkinNr);
            this.lblTitle.ForeColor = this.notes.GetTextClr(this.note.SkinNr);
            Program.Formmanager.FrmManageNotesNeedUpdate = true;
            Program.Formmanager.RefreshFrmManageNotes();
            if (!SyntaxHighlight.KeywordsInitialized)
            {
                SyntaxHighlight.InitHighlighter();
            }

            SyntaxHighlight.CheckSyntaxFull(this.rtbNote, this.note.SkinNr, this.notes);
            if (Settings.HighlightClearLexiconMemory)
            {
                SyntaxHighlight.DeinitHighlighter();
            }

            if (!this.saveWorker.IsBusy)
            {
                this.saveWorker.RunWorkerAsync(this.rtbNote.Rtf);
            }

            Cursor.Current = Cursors.Default;
            Log.Write(LogType.info, "Note " + this.note.Filename + " skin changed to " + this.notes.GetSkinName(this.note.SkinNr));
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
            this.SetTopmostNote();
            if (!this.saveWorker.IsBusy)
            {
                this.saveWorker.RunWorkerAsync(this.rtbNote.Rtf);
            }
        }

        /// <summary>
        /// Toggle to wrap words in the note content.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void menuWordWrap_Click(object sender, EventArgs e)
        {
            this.note.Wordwarp = !this.note.Wordwarp;
            this.SetWordwarpNote();
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
        /// The area in pnlHead is selected and dragged.
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
            string note_locknotemsg = Strings.T("&Lock note");
            string note_clicktounlock = Strings.T(" (click again to unlock)");
            if (this.note.Locked)
            {
                this.CreatePbLock();
                this.menuLockNote.Text = note_locknotemsg + note_clicktounlock;
            }
            else
            {
                this.DestroyPbLock();
                this.menuLockNote.Text = note_locknotemsg;
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
            string note_rollupmsg = Strings.T("&Roll up");
            string note_clicktorollup = Strings.T("(click again to Roll Down)");
            if (this.note.RolledUp)
            {
                this.menuRollUp.Text = note_rollupmsg + note_clicktorollup;
                this.MinimumSize = new Size(this.MinimumSize.Width, this.pnlHead.Height);
                this.Height = this.pnlHead.Height;
            }
            else
            {
                this.menuRollUp.Text = note_rollupmsg;
                this.MinimumSize = new Size(this.MinimumSize.Width, this.pnlHead.Height + this.pbResizeGrip.Height);
                this.Height = this.note.Height;
            }
        }

        /// <summary>
        /// Set wordwarp of the note content.
        /// </summary>
        private void SetWordwarpNote()
        {
            this.menuWordWrap.Checked = this.note.Wordwarp;
            this.rtbNote.WordWrap = this.note.Wordwarp;
        }

        /// <summary>
        /// Set note form topmost 
        /// </summary>
        private void SetTopmostNote()
        {
            this.menuOnTop.Checked = this.note.Ontop;
            this.TopMost = this.note.Ontop;
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
            sfdlg.Title = Strings.T("Save note to file");
            StringBuilder sbfilter = new StringBuilder();
            sbfilter.Append(Strings.T("Textfile (*.txt)")).Append("|*.txt|");
            sbfilter.Append(Strings.T("RichTextFormat file (*.rtf)")).Append("|*.rtf|");
            sbfilter.Append(Strings.T("Webpage (*.htm)")).Append("|*.htm|");
            sbfilter.Append(Strings.T("PHP file (*.php)")).Append("|*.php");
            sfdlg.Filter = sbfilter.ToString();
            if (PluginsManager.EnabledPlugins != null) 
            {
                for (int i = 0; i < PluginsManager.EnabledPlugins.Count; i++)
                {
                    string dlgfilter =  PluginsManager.EnabledPlugins[i].ExportNoteContentDlgFilter();
                    if (dlgfilter != null)
                    {
                        try
                        {
                            sfdlg.Filter += dlgfilter;
                        }
                        catch (ArgumentException argexc)
                        {
                            string badpluginfilter = Strings.T("Bad plugin export notecontent filter selected.");
                            MessageBox.Show(badpluginfilter);
                            Log.Write(LogType.exception, badpluginfilter + argexc.StackTrace);
                        }
                    }
                }
            }

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
                    default:
                        if (PluginsManager.EnabledPlugins != null)
                        {
                            bool handled = false;
                            for (int i = 0; i < PluginsManager.EnabledPlugins.Count; i++)
                            {
                                PluginsManager.EnabledPlugins[i].ExportNoteContent(this.rtbNote);
                                handled = true;
                            }

                            if (!handled)
                            {
                                Log.Write(LogType.exception, "No plugin handled the selected filter.");
                            }
                        }

                        break;
                }
            }
        }

        /// <summary>
        /// Create plugin sendto menu's
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event argument</param>
        private void menuSendTo_DropDownOpening(object sender, EventArgs e)
        {
            bool giveup = false;
            while (this.menuActions.DropDownItems.Count > 2 && !giveup)
            {
                try
                {
                    this.menuActions.DropDownItems.RemoveAt(2);
                }
                catch (Exception ex)
                {
                    giveup = true;
                    Log.Write(LogType.exception, ex.Message);
                }
            }

            if (PluginsManager.EnabledPlugins != null)
            {
                for (int i = 0; i < PluginsManager.EnabledPlugins.Count; i++)
                {
                    if (PluginsManager.EnabledPlugins[i].InitFrmNoteShareMenu() != null)
                    {
                        ToolStripMenuItem menuitem = PluginsManager.EnabledPlugins[i].InitFrmNoteShareMenu();
                        menuitem.Tag = i;
                        menuitem.Click += new EventHandler(this.menuSharePluginClicked);
                        this.menuActions.DropDownItems.Add(menuitem);
                    }
                }
            }
        }

        /// <summary>
        /// The skins dropdown menu is opened.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void menuNoteSkins_DropDownOpening(object sender, EventArgs e)
        {
            this.CreateSkinsMenu(false);
        }

        #endregion Methods
    }
}