//-----------------------------------------------------------------------
// <copyright file="FrmNewNote.cs" company="GNU">
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
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Class to create new note.
    /// </summary>
    public partial class FrmNewNote : Form
    {
        #region Fields (5)

        private Notes notes;
        private Note note;
        private Point oldp;
        private bool moving = false;

        #endregion Fields

        #region Constructors (2)

        /// <summary>
        /// Initializes a new instance of the FrmNewNote class for a new note.
        /// </summary>
        /// <param name="notes">The class with access to all notes.</param>
        public FrmNewNote(Notes notes)
        {
            this.InitializeComponent();
            this.notes = notes;
            this.note = null;
            this.Text = "new note";
            this.SetFontSettings();
            this.tbTitle.Text = DateTime.Now.ToString();
        }

        /// <summary>
        /// Initializes a new instance of the FrmNewNote class for editing a exist note.
        /// </summary>
        /// <param name="notes">The class with access to all notes.</param>
        /// <param name="note">the note to edit.</param>
        public FrmNewNote(Notes notes, Note note)
        {
            this.InitializeComponent();
            this.notes = notes;
            this.note = note;
            this.Text = "edit note";
            this.SetFontSettings();
            this.tbTitle.Text = note.Title;
            this.rtbNewNote.Rtf = note.GetContent();
        }

        #endregion Constructors

        #region Methods (18)

        // Private Methods (18) 

        /// <summary>
        /// Set the font and textdirection FrmNewNote.
        /// </summary>
        /// <param name="title">The new note title.</param>
        /// <param name="content">The new note content.</param>
        private void SetFontSettings()
        {
            this.rtbNewNote.Font = new Font(Settings.FontContentFamily, rtbNewNote.Font.Size);
            switch (Settings.FontTextdirection)
            {
                case 1:
                    this.tbTitle.TextAlign = HorizontalAlignment.Left;
                    this.rtbNewNote.SelectionAlignment = HorizontalAlignment.Left;
                    break;
                case 2:
                    this.tbTitle.TextAlign = HorizontalAlignment.Right;
                    this.rtbNewNote.SelectionAlignment = HorizontalAlignment.Right;
                    break;
                default:
                    this.tbTitle.TextAlign = HorizontalAlignment.Left;
                    this.rtbNewNote.SelectionAlignment = HorizontalAlignment.Left;
                    break;
            }

            this.rtbNewNote.Focus();
            this.rtbNewNote.Select();
            this.BringToFront();
        }

        /// <summary>
        /// User pressed the accept note button. Note will now be saved.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnAddNote_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.tbTitle.Text))
            {
                this.tbTitle.Text = DateTime.Now.ToString();
            }
            else if (String.IsNullOrEmpty(this.rtbNewNote.Text))
            {
                this.rtbNewNote.Text = "Please enter some content.";
            }
            else
            {
                bool newnote = false;
                if (this.note == null)
                {
                    newnote = true;
                    this.note = this.notes.CreateNote(this.tbTitle.Text, Settings.NotesDefaultSkinnr, this.Location.X, this.Location.Y, this.Width, this.Height);
                }
                note.Title = this.tbTitle.Text;
                note.Visible = true;
                if (String.IsNullOrEmpty(note.Filename))
                {
                    note.Filename = this.notes.GetNoteFilename(note.Title);
                }
                if (xmlUtil.WriteNote(this.note, this.notes.GetSkinName(this.note.SkinNr), this.rtbNewNote.Rtf))
                {
                    if (newnote)
                    {
                        this.notes.AddNote(this.note);
                    }
                    this.note.CreateForm();

                    this.Close();
                }
                else
                {
                    throw new CustomException("Could not write note");
                }
            }
        }

        /// <summary>
        /// User pressed the cancel button, all things typed in FrmNewNote window will be lost.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Copy the note content.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void copyTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(rtbNewNote.Text))
            {
                Log.Write(LogType.error, "No content to copy.");
            }
            else
            {
                Clipboard.SetText(this.rtbNewNote.Text);
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
            if (Settings.NotesTransparencyEnabled)
            {
                this.Opacity = Settings.NotesTransparencyLevel;
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
                this.rtbNewNote.Text = this.rtbNewNote.Text + Clipboard.GetText();
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
                this.pnlHeadNewNote.BackColor = this.notes.GetBackgroundColor(Settings.NotesDefaultSkinnr);
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
        /// A hyperlink is clicked, check settings to see if confirm launch dialog have
        /// to be showed, if not then directly launch the URL.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void rtbNote_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Program.LoadLink(e.LinkText);
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
        /// User entered the title box.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event arguments</param>
        private void tbTitle_Enter(object sender, EventArgs e)
        {
            this.tbTitle.BackColor = notes.GetHighlightColor(Settings.NotesDefaultSkinnr);
            SetToolbarEnabled(false);
        }

        /// <summary>
        /// User leaved the title box
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event arguments</param>
        private void tbTitle_Leave(object sender, EventArgs e)
        {
            this.tbTitle.BackColor = notes.GetBackgroundColor(Settings.NotesDefaultSkinnr);
            SetToolbarEnabled(true);
        }

        /// <summary>
        /// User entered the note content box
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event arguments</param>
        private void rtbNote_Enter(object sender, EventArgs e)
        {
            this.rtbNewNote.BackColor = notes.GetHighlightColor(Settings.NotesDefaultSkinnr);
            SetToolbarEnabled(true);
        }

        /// <summary>
        /// User leaved the note content box.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event arguments</param>
        private void rtbNote_Leave(object sender, EventArgs e)
        {
            this.rtbNewNote.BackColor = notes.GetBackgroundColor(Settings.NotesDefaultSkinnr);
        }

        private void SetToolbarEnabled(bool enabled)
        {
            this.btnTextBold.Enabled = enabled;
            this.btnTextItalic.Enabled = enabled;
            this.btnTextStriketrough.Enabled = enabled;
            this.btnTextUnderline.Enabled = enabled;
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
                this.pnlHeadNewNote.BackColor = notes.GetBackgroundColor(Settings.NotesDefaultSkinnr);

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
                this.pnlHeadNewNote.BackColor = notes.GetForegroundColor(Settings.NotesDefaultSkinnr);
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
            if (this.rtbNewNote.TextLength == 0)
            {
                this.menuCopyContent.Enabled = false;
            }
            else
            {
                this.menuCopyContent.Enabled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfiledlg = new OpenFileDialog();
            openfiledlg.Title = "open file";
            openfiledlg.Multiselect = false;
            openfiledlg.Filter = "Plain text file (*.txt)|*.txt|PNotes note (*.pnote)|*.pnote|KeyNote NF note (*knt)|*knt";
            openfiledlg.CheckFileExists = true;
            openfiledlg.CheckPathExists = true;
            DialogResult res = openfiledlg.ShowDialog();
            if (res == DialogResult.OK)
            {
                StreamReader reader = null;
                try
                {
                    if (File.Exists(openfiledlg.FileName))
                    {
                        reader = new StreamReader(openfiledlg.FileName, true); //detect encoding
                        if (openfiledlg.FilterIndex == 1)
                        {
                            rtbNewNote.Text = reader.ReadToEnd();
                        }
                        else if (openfiledlg.FilterIndex == 2)
                        {
                            rtbNewNote.Rtf = reader.ReadToEnd();
                            SetDefaultFontFamilyAndSize();
                        }
                        else if (openfiledlg.FilterIndex == 3)
                        {
                            uint linenum = 0;
                            string curline = reader.ReadLine();//no CR+LF characters
                            if (curline == "#!GFKNT 2.0")
                            {
                                while (curline != "%:")
                                {
                                    curline = reader.ReadLine();
                                    linenum++;
                                    if (linenum > 100) //should normally be except %: around line 42.
                                    {
                                        MessageBox.Show("Cannot find KeyNote NF note content.");
                                    }
                                }
                                curline = reader.ReadLine();
                                StringBuilder sb = new StringBuilder(curline);
                                while (curline != "%%")
                                {
                                    curline = reader.ReadLine();
                                    sb.Append(curline);
                                    linenum++;
                                    if (linenum > 5000) //limit to 5000 lines.
                                    {
                                        break;
                                    }
                                }
                                this.rtbNewNote.Rtf = sb.ToString();
                                SetDefaultFontFamilyAndSize();
                            }
                            else
                            {
                                MessageBox.Show("Not a KeyNote NF note.");
                            }
                        }
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
        }

        private void SetDefaultFontFamilyAndSize()
        {
            rtbNewNote.SelectAll();
            rtbNewNote.Font = new Font(Settings.FontContentFamily, (float)Settings.FontContentSize);
            rtbNewNote.Select(0, 0);
        }

        /// <summary>
        /// Check if selection length of rtbNote is larger than zero.
        /// </summary>
        private bool checksellen()
        {
            if (this.rtbNewNote.SelectedText.Length > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes 1 fontsyle from the fontsyles of the checkstyle rtb text.
        /// This methode does not check if selection lenght is okay.
        /// </summary>
        private FontStyle removestyle(FontStyle checkstyles, FontStyle removestyle)
        {
            FontStyle newstyles = checkstyles;
            newstyles -= removestyle;
            return newstyles;
        }

        /// <summary>
        /// Make note content text bold, or if the selected text is already bold
        /// then remove the bold style.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTextBold_Click(object sender, EventArgs e)
        {
            if (checksellen())
            {
                if (this.rtbNewNote.SelectionFont.Bold)
                {
                    this.rtbNewNote.SelectionFont = new System.Drawing.Font(this.rtbNewNote.SelectionFont.FontFamily, this.rtbNewNote.SelectionFont.SizeInPoints, removestyle(this.rtbNewNote.SelectionFont.Style, FontStyle.Bold));
                }
                else
                {
                    this.rtbNewNote.SelectionFont = new System.Drawing.Font(this.rtbNewNote.SelectionFont.FontFamily, this.rtbNewNote.SelectionFont.SizeInPoints, (this.rtbNewNote.SelectionFont.Style | System.Drawing.FontStyle.Bold));
                }
            }
        }

        /// <summary>
        /// Italic text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTextItalic_Click(object sender, EventArgs e)
        {
            if (checksellen())
            {
                if (this.rtbNewNote.SelectionFont.Italic)
                {
                    this.rtbNewNote.SelectionFont = new System.Drawing.Font(this.rtbNewNote.SelectionFont.FontFamily, this.rtbNewNote.SelectionFont.SizeInPoints, removestyle(this.rtbNewNote.SelectionFont.Style, FontStyle.Italic));
                }
                else
                {
                    this.rtbNewNote.SelectionFont = new System.Drawing.Font(this.rtbNewNote.SelectionFont.FontFamily, this.rtbNewNote.SelectionFont.SizeInPoints, (this.rtbNewNote.SelectionFont.Style | System.Drawing.FontStyle.Italic));
                }
            }
        }

        /// <summary>
        /// Underline text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTextUnderline_Click(object sender, EventArgs e)
        {
            if (this.rtbNewNote.SelectionFont.Underline)
            {
                this.rtbNewNote.SelectionFont = new System.Drawing.Font(this.rtbNewNote.SelectionFont.FontFamily, this.rtbNewNote.SelectionFont.SizeInPoints, removestyle(this.rtbNewNote.SelectionFont.Style, FontStyle.Underline));
            }
            else
            {
                this.rtbNewNote.SelectionFont = new System.Drawing.Font(this.rtbNewNote.SelectionFont.FontFamily, this.rtbNewNote.SelectionFont.SizeInPoints, (this.rtbNewNote.SelectionFont.Style | System.Drawing.FontStyle.Underline));
            }
        }

        /// <summary>
        /// Striketrough text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTextStriketrough_Click(object sender, EventArgs e)
        {
            if (this.rtbNewNote.SelectionFont.Strikeout)
            {
                this.rtbNewNote.SelectionFont = new System.Drawing.Font(this.rtbNewNote.SelectionFont.FontFamily, this.rtbNewNote.SelectionFont.SizeInPoints, removestyle(this.rtbNewNote.SelectionFont.Style, FontStyle.Strikeout));
            }
            else
            {
                this.rtbNewNote.SelectionFont = new System.Drawing.Font(this.rtbNewNote.SelectionFont.FontFamily, this.rtbNewNote.SelectionFont.SizeInPoints, (this.rtbNewNote.SelectionFont.Style | System.Drawing.FontStyle.Strikeout));
            }
        }

        #endregion
    }
}
