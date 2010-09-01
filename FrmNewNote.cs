//-----------------------------------------------------------------------
// <copyright file="FrmNewNote.cs" company="GNU">
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
#define linux //platform can be: windows, linux, macos

namespace NoteFly
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    /// <summary>
    /// Class to create new note.
    /// </summary>
    public partial class FrmNewNote : Form
    {
        #region Fields (5)
        private bool editnote = false, setupfirsthighlight = false, moving = false;
        private int editnoteid = -1;
        private short notecolor;
        private Notes notes;
        private Skin skin;
        //private TextHighlight highlight;
        private Point oldp;
        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the FrmNewNote class.
        /// </summary>
        /// <param name="notes">The class with access to all notes.</param>
        /// <param name="notecolor">the default note color.</param>
        /// <param name="editnoteid">The noteid to edit.</param>
        /// <param name="editnotetitle">The title of the note to edit.</param>
        /// <param name="editnotecontent">The content of the note to edit.</param>
        public FrmNewNote(Notes notes, short notecolor, int editnoteid, string editnotetitle, string editnotecontent)
        {
            this.InitializeComponent();
            this.editnote = true;
            this.notecolor = notecolor;
            this.skin = new Skin(notecolor);
            this.editnoteid = editnoteid;
            this.notes = notes;
            this.ResetNewNoteForm(editnotetitle, editnotecontent);
            this.rtbNote.Focus();
            this.rtbNote.Select();
            if (this.editnote)
            {
                this.Text = "edit note";
            }
            this.BringToFront(); //default is this, but this forces it. bug: #0000014
        }

        /// <summary>
        /// Initializes a new instance of the FrmNewNote class.
        /// </summary>
        /// <param name="notes">The class with access to all notes.</param>
        /// <param name="notecolor">The default note color.</param>
        public FrmNewNote(Notes notes, short notecolor)
        {
            this.InitializeComponent();
            this.editnote = false;
            this.notes = notes;
            this.notecolor = notecolor;
            this.skin = new Skin(notecolor);
            this.ResetNewNoteForm(String.Empty, String.Empty);
            this.tbTitle.Text = DateTime.Now.ToString();
            this.rtbNote.Focus();
            this.rtbNote.Select();
            this.BringToFront();
        }

        #endregion Constructors

        #region Methods (18)

        // Private Methods (18) 

        /// <summary>
        /// User pressed the accept note button. Note will now be saved.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnAddNote_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.tbTitle.Text))
            {
                this.tbTitle.BackColor = this.skin.GetObjColor(false, false, true);
                this.tbTitle.Text = DateTime.Now.ToString();
            }
            else if (String.IsNullOrEmpty(this.rtbNote.Text))
            {
                this.rtbNote.BackColor = this.skin.GetObjColor(false, false, true);
                this.rtbNote.Text = "Please enter some content.";
            }
            else
            {
                if (this.editnote)
                {
                    this.notes.UpdateNote(this.editnoteid, this.tbTitle.Text, this.rtbNote.Text, true);
                }
                else
                {
                    this.notes.DrawNewNote(this.tbTitle.Text, this.rtbNote.Text, this.notecolor);
                }

                this.Close();
            }
        }

        /// <summary>
        /// User pressed the cancel button, all things typed in FrmNewNote window will be lost.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            int posnotelst = this.editnoteid - 1;
            if (this.editnote && posnotelst >= 0 && posnotelst < this.notes.NumNotes)
            {
                this.notes.GetNotes[posnotelst].Show();
            }

            this.Close();
        }

        /// <summary>
        /// Copy the note content.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void copyTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(rtbNote.Text))
            {
                Log.Write(LogType.error, "No content to copy.");
            }
            else
            {
                Clipboard.SetText(this.rtbNote.Text);
            }
        }

        /// <summary>
        /// Check whether pastTextToolStripMenuItem should be enabled.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void copyTextToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                this.menuPasteToContent.Enabled = true;
            }
            else
            {
                this.menuPasteToContent.Enabled = false;
            }
        }

        /// <summary>
        /// Form got focus, remove transparency.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void frmNewNote_Activated(object sender, EventArgs e)
        {
            if (Settings.NotesTransparencyEnabled)
            {
                this.Opacity = 1.0;
            }
        }

        /// <summary>
        /// Form lost focus, make transparent.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void frmNewNote_Deactivate(object sender, EventArgs e)
        {
            if (Settings.NotesTransparencyEnabled && this.skin != null)
            {
                this.Opacity = this.skin.GetTransparencylevel();
                this.Refresh();
            }
        }

        /// <summary>
        /// Pasting text as note content.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void pastTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                this.rtbNote.Text = this.rtbNote.Text + Clipboard.GetText();
            }
            else
            {
                const string emptyclipboard = "There is no text on the clipboard.";
                MessageBox.Show(emptyclipboard);
                Log.Write(LogType.error, emptyclipboard);
            }

        }

        /// <summary>
        /// Resizing the note.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void pbResizeGrip_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Cursor = Cursors.SizeNWSE;
                this.Size = new Size(this.PointToClient(MousePosition).X, this.PointToClient(MousePosition).Y);
            }

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Moving the note.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void pnlHeadNewNote_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.moving = true;
                this.oldp = e.Location;
                if (this.skin != null)
                {
                    this.pnlHeadNewNote.BackColor = this.skin.GetObjColor(true);
                }
            }
        }

        /// <summary>
        /// Show context menu.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void pnlNoteEdit_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStripTextActions.Show(this.Location.X + e.X, this.Location.X + e.Y);
            }
        }

        /// <summary>
        /// Redraw reset FrmNewNote.
        /// </summary>
        /// <param name="title">The new note title.</param>
        /// <param name="content">The new note content.</param>
        private void ResetNewNoteForm(string title, string content)
        {
            if (this.skin == null)
            {
                return;
            }

            this.rtbNote.Font = this.skin.GetFontNoteContent();

            Color normalcolor = this.skin.GetObjColor(false);
            this.pnlNoteEdit.BackColor = normalcolor;
            this.rtbNote.BackColor = normalcolor;
            this.pnlHeadNewNote.BackColor = normalcolor;

            if (this.notes.TextDirection == 0)
            {
                this.tbTitle.TextAlign = HorizontalAlignment.Left;
                this.rtbNote.SelectionAlignment = HorizontalAlignment.Left;
            }
            else if (this.notes.TextDirection == 1)
            {
                this.tbTitle.TextAlign = HorizontalAlignment.Right;
                this.rtbNote.SelectionAlignment = HorizontalAlignment.Right;
            }

            this.tbTitle.Text = title;
            this.rtbNote.Text = content;
        }

        /// <summary>
        /// A hyperlink is clicked, check settings to see if confirm launch dialog have
        /// to be showed, if not then directly launch the URL.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void rtbNote_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            xmlHandler getSettings = new xmlHandler(true);
            if (getSettings.getXMLnodeAsBool("askurl"))
            {
                DialogResult result = MessageBox.Show(this, "Are you sure you want to visted: " + e.LinkText, "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.LoadUrl(e.LinkText);
                }
            }
            else
            {
                this.LoadUrl(e.LinkText);
            }
        }

        /// <summary>
        /// Load a url
        /// </summary>
        /// <param name="url">the url to load.</param>
        private void LoadUrl(string url)
        {
            System.Diagnostics.Process.Start(url.Trim());
            Log.Write(LogType.info, "Link clicked.");
        }

        /// <summary>
        /// Force context menu to show up.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void rtbNote_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStripTextActions.Show(this.Location.X + e.X, this.Location.X + e.Y);
            }
        }

        /// <summary>
        /// Set the text direction of the note content.
        /// </summary>
        private void SetTextDirection()
        {
            if (this.notes.TextDirection == 0)
            {
                this.tbTitle.TextAlign = HorizontalAlignment.Left;
            }
            else if (this.notes.TextDirection == 1)
            {
                this.tbTitle.TextAlign = HorizontalAlignment.Right;
            }
        }

        /// <summary>
        /// User entered the title box.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event arguments</param>
        private void tbTitle_Enter(object sender, EventArgs e)
        {
            if (this.skin != null)
            {
                this.tbTitle.BackColor = this.skin.GetObjColor(false, true, false);
            }
        }

        /// <summary>
        /// User leaved the title box
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event arguments</param>
        private void tbTitle_Leave(object sender, EventArgs e)
        {
            if (this.skin != null)
            {
                this.tbTitle.BackColor = this.skin.GetObjColor(false);
            }
        }

        /// <summary>
        /// User entered the note content box
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event arguments</param>
        private void rtbNote_Enter(object sender, EventArgs e)
        {
            if (this.skin != null)
            {
                this.rtbNote.BackColor = this.skin.GetObjColor(false, true, false);
            }
        }

        /// <summary>
        /// User leaved the note content box.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event arguments</param>
        private void rtbNote_Leave(object sender, EventArgs e)
        {
            if (this.skin != null)
            {
                this.rtbNote.BackColor = this.skin.GetObjColor(false);
            }
        }

        /// <summary>
        /// Move note if pnlHead is being left clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlHeadNewNote_MouseMove(object sender, MouseEventArgs e)
        {
            if ((this.moving) && (e.Button == MouseButtons.Left))
            {
                if (this.skin != null)
                {
                    this.pnlHeadNewNote.BackColor = this.skin.GetObjColor(true);
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
                this.pnlHeadNewNote.BackColor = this.skin.GetObjColor(false);
            }
        }

        /// <summary>
        ///  End moving note.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlHeadNewNote_MouseUp(object sender, MouseEventArgs e)
        {
            this.moving = false;
        }

        /// <summary>
        /// Set this note ontop, CheckOnClick is set to true.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuStickyOnTop_Click(object sender, EventArgs e)
        {
            this.TopMost = this.menuStickyOnTop.Checked;
        }

        /// <summary>
        /// Avoid that if there is no content the user select to copy the content.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripTextActions_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.rtbNote.TextLength == 0)
            {
                this.menuCopyContent.Enabled = false;
            }
            else
            {
                this.menuCopyContent.Enabled = true;
            }
        }
        #endregion

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfiledlg = new OpenFileDialog();
            openfiledlg.Title = "open file";
            openfiledlg.Multiselect = false;
            openfiledlg.Filter = "text file (*.txt)|testerdetest.";
            openfiledlg.ShowDialog();
            
        }
    }
}
