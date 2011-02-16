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
    /// New and edit note window.
    /// </summary>
    public partial class FrmNewNote : Form
    {
        #region Fields (4) 

        /// <summary>
        /// Indicated if the form is being moved.
        /// </summary>
        private bool moving = false;

        /// <summary>
        /// Reference to a new or editing note,
        /// </summary>
        private Note note;

        /// <summary>
        /// Pointer to the notes class.
        /// </summary>
        private Notes notes;

        /// <summary>
        /// The old position of the mouse while resizing.
        /// </summary>
        private Point oldp;

        #endregion Fields 

        #region Constructors (2) 

        /// <summary>
        /// Initializes a new instance of the FrmNewNote class for editing a exist note.
        /// </summary>
        /// <param name="notes">The class with access to all notes.</param>
        /// <param name="note">the note to edit.</param>
        public FrmNewNote(Notes notes, Note note, Point locfrmnewnote)
        {
            this.ConstructFrmNewNote(notes);
            this.Location = locfrmnewnote;
            this.note = note;
            this.Text = "edit note";
            this.SetColorsForm(this.note.skinNr);
            this.tbTitle.Text = note.title;
            if (String.IsNullOrEmpty(this.note.tempcontent))
            {
                this.rtbNewNote.Rtf = note.GetContent();
            }
            else
            {
                this.rtbNewNote.Rtf = this.note.tempcontent;
                //clear memory:
                this.note.tempcontent = String.Empty;
                this.note.tempcontent = null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the FrmNewNote class for a new note.
        /// </summary>
        /// <param name="notes">The class with access to all notes.</param>
        public FrmNewNote(Notes notes)
        {
            this.ConstructFrmNewNote(notes);
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - this.Width/2, Screen.PrimaryScreen.WorkingArea.Height / 2 - this.Height/2);
            this.note = null;
            this.Text = "new note";
            if (Settings.notesDefaultRandomSkin)
            {
                Settings.notesDefaultSkinnr = notes.GenerateRandomSkinnr();
            }

            this.SetColorsForm(Settings.notesDefaultSkinnr);
            this.tbTitle.Text = DateTime.Now.ToString();
        }

        #endregion Constructors 

        #region Methods (30) 

        // Private Methods (30) 

        /// <summary>
        /// Initialize components FrmNewNote, set font, tooltip and richtextbox settings
        /// </summary>
        /// <param name="notes">Reference to notes class.</param>
        private void ConstructFrmNewNote(Notes notes)
        {
            this.InitializeComponent();
            this.notes = notes;
            this.SetFontSettings();
            this.toolTip.Active = Settings.notesTooltipsEnabled;
            this.rtbNewNote.DetectUrls = Settings.highlightHyperlinks;
            this.tbTitle.Select();
        }

        /// <summary>
        /// Set all the form colors by the skinnr.
        /// </summary>
        /// <param name="skinnr">Skin number</param>
        private void SetColorsForm(int skinnr)
        {
            this.BackColor = this.notes.GetPrimaryClr(skinnr);
            this.pnlHeadNewNote.BackColor = this.notes.GetPrimaryClr(skinnr);
            this.lbTextTitle.ForeColor = this.notes.GetTextClr(skinnr);
            this.tbTitle.ForeColor = this.notes.GetTextClr(skinnr);
            this.rtbNewNote.ForeColor = this.notes.GetTextClr(skinnr);
            this.tbTitle.BackColor = this.notes.GetHighlightClr(skinnr);
            this.rtbNewNote.BackColor = this.notes.GetPrimaryClr(skinnr);

            this.btnTextBold.ForeColor = this.notes.GetTextClr(skinnr);
            this.btnTextItalic.ForeColor = this.notes.GetTextClr(skinnr);
            this.btnTextStriketrough.ForeColor = this.notes.GetTextClr(skinnr);
            this.btnTextUnderline.ForeColor = this.notes.GetTextClr(skinnr);
            this.btnFontBigger.ForeColor = this.notes.GetTextClr(skinnr);
            this.btnFontSmaller.ForeColor = this.notes.GetTextClr(skinnr);

            this.btnTextBold.FlatAppearance.MouseOverBackColor = this.notes.GetSelectClr(skinnr);
            this.btnTextItalic.FlatAppearance.MouseOverBackColor = this.notes.GetSelectClr(skinnr);
            this.btnTextStriketrough.FlatAppearance.MouseOverBackColor = this.notes.GetSelectClr(skinnr);
            this.btnTextUnderline.FlatAppearance.MouseOverBackColor = this.notes.GetSelectClr(skinnr);
            this.btnFontBigger.FlatAppearance.MouseOverBackColor = this.notes.GetSelectClr(skinnr);
            this.btnFontSmaller.FlatAppearance.MouseOverBackColor = this.notes.GetSelectClr(skinnr);
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
                    this.note = this.notes.CreateNote(this.tbTitle.Text, Settings.notesDefaultSkinnr, this.Location.X, this.Location.Y, this.Width, this.Height);
                }

                this.note.title = this.tbTitle.Text;
                this.note.visible = true;
                if (String.IsNullOrEmpty(this.note.Filename))
                {
                    this.note.Filename = this.notes.GetNoteFilename(this.note.title);
                }

                if (xmlUtil.WriteNote(this.note, this.notes.GetSkinName(this.note.skinNr), this.rtbNewNote.Rtf))
                {
                    if (newnote)
                    {
                        this.notes.AddNote(this.note);
                    }

                    Highlight.InitHighlighter();
                    this.note.tempcontent = this.rtbNewNote.Rtf;
                    this.note.CreateForm();
                    if (this.note.tempcontent != null)
                    {
                        this.note.tempcontent = null;
                    }

                    Highlight.DeinitHighlighter();
                    this.notes.frmmangenotesneedupdate = true;
                    TrayIcon.RefreshFrmManageNotes();
                    this.Close();
                    GC.Collect();
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
            if (this.note != null)
            {
                this.note.CreateForm();
            }

            this.Close();
        }

        /// <summary>
        /// Make note content text bold, or if the selected text is already bold
        /// then remove the bold style.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnTextBold_Click(object sender, EventArgs e)
        {
            if (this.checksellen())
            {
                if (this.rtbNewNote.SelectionFont.Bold)
                {
                    this.rtbNewNote.SelectionFont = new System.Drawing.Font(this.rtbNewNote.SelectionFont.FontFamily, this.rtbNewNote.SelectionFont.SizeInPoints, this.removestyle(this.rtbNewNote.SelectionFont.Style, FontStyle.Bold));
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
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnTextItalic_Click(object sender, EventArgs e)
        {
            if (this.checksellen())
            {
                if (this.rtbNewNote.SelectionFont.Italic)
                {
                    this.rtbNewNote.SelectionFont = new System.Drawing.Font(this.rtbNewNote.SelectionFont.FontFamily, this.rtbNewNote.SelectionFont.SizeInPoints, this.removestyle(this.rtbNewNote.SelectionFont.Style, FontStyle.Italic));
                }
                else
                {
                    this.rtbNewNote.SelectionFont = new System.Drawing.Font(this.rtbNewNote.SelectionFont.FontFamily, this.rtbNewNote.SelectionFont.SizeInPoints, (this.rtbNewNote.SelectionFont.Style | System.Drawing.FontStyle.Italic));
                }
            }
        }

        /// <summary>
        /// Striketrough text
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnTextStriketrough_Click(object sender, EventArgs e)
        {
            if (this.checksellen())
            {
                if (this.rtbNewNote.SelectionFont.Strikeout)
                {
                    this.rtbNewNote.SelectionFont = new System.Drawing.Font(this.rtbNewNote.SelectionFont.FontFamily, this.rtbNewNote.SelectionFont.SizeInPoints, this.removestyle(this.rtbNewNote.SelectionFont.Style, FontStyle.Strikeout));
                }
                else
                {
                    this.rtbNewNote.SelectionFont = new System.Drawing.Font(this.rtbNewNote.SelectionFont.FontFamily, this.rtbNewNote.SelectionFont.SizeInPoints, (this.rtbNewNote.SelectionFont.Style | System.Drawing.FontStyle.Strikeout));
                }
            }
        }

        /// <summary>
        /// Underline text
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnTextUnderline_Click(object sender, EventArgs e)
        {
            if (this.checksellen())
            {
                if (this.rtbNewNote.SelectionFont.Underline)
                {
                    this.rtbNewNote.SelectionFont = new System.Drawing.Font(this.rtbNewNote.SelectionFont.FontFamily, this.rtbNewNote.SelectionFont.SizeInPoints, this.removestyle(this.rtbNewNote.SelectionFont.Style, FontStyle.Underline));
                }
                else
                {
                    this.rtbNewNote.SelectionFont = new System.Drawing.Font(this.rtbNewNote.SelectionFont.FontFamily, this.rtbNewNote.SelectionFont.SizeInPoints, (this.rtbNewNote.SelectionFont.Style | System.Drawing.FontStyle.Underline));
                }
            }
        }

        /// <summary>
        /// Make text bigger.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnFontBigger_Click(object sender, EventArgs e)
        {
            if (this.checksellen())
            {
                this.ChangeFontSizeSelected(this.rtbNewNote.SelectionFont.SizeInPoints + 1);
            }
        }

        /// <summary>
        /// Make text smaller.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnFontSmaller_Click(object sender, EventArgs e)
        {
            if (this.checksellen())
            {
                this.ChangeFontSizeSelected(this.rtbNewNote.SelectionFont.SizeInPoints - 1);
            }
        }

        /// <summary>
        /// Change the fontsize of the selected text limited from 6pt to 108pt.
        /// </summary>
        /// <param name="newsize">Event arguments</param>
        private void ChangeFontSizeSelected(float newsize)
        {
            if ((newsize < 6) || (newsize > 108))
            {
                return;
            }
            else {
                this.rtbNewNote.SelectionFont = new System.Drawing.Font(this.rtbNewNote.SelectionFont.FontFamily, newsize);
            }
        }

        /// <summary>
        /// Check if selection length of rtbNote is larger than zero.
        /// </summary>
        /// <returns>true if length is larger than 0.</returns>
        private bool checksellen()
        {
            if (this.rtbNewNote.SelectedText.Length > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Avoid that if there is no content the user select to copy the content.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
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
        /// Copy the note content.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void copyTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.rtbNewNote.Text))
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
        /// <param name="sender">Sender object</param>
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
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void frmNewNote_Activated(object sender, EventArgs e)
        {
            if (Settings.notesTransparencyEnabled)
            {
                this.Opacity = 1.0;
            }
        }

        /// <summary>
        /// Form lost focus, make transparent.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void frmNewNote_Deactivate(object sender, EventArgs e)
        {
            if (Settings.notesTransparencyEnabled)
            {
                this.Opacity = Settings.notesTransparencyLevel;
                this.Refresh();
            }
        }

        /// <summary>
        /// Import a file as note.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
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
                            this.rtbNewNote.Text = reader.ReadToEnd();
                        }
                        else if (openfiledlg.FilterIndex == 2)
                        {
                            this.rtbNewNote.Rtf = reader.ReadToEnd();
                            this.SetDefaultFontFamilyAndSize();
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
                                    if (linenum > 50) //should normally be except %: around line 42.
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
                                    if (linenum > 8000) //limit to 8000 lines.
                                    {
                                        break;
                                    }
                                }

                                this.rtbNewNote.Rtf = sb.ToString();
                                this.SetDefaultFontFamilyAndSize();
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

        /// <summary>
        /// Set this note ontop, CheckOnClick is set to true.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void menuStickyOnTop_Click(object sender, EventArgs e)
        {
            this.TopMost = this.menuStickyOnTop.Checked;
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
        /// Resizing the FtmNewNote form.
        /// </summary>
        /// <param name="sender">Sender object</param>
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
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void pnlHeadNewNote_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.moving = true;
                this.oldp = e.Location;
                if (this.note != null)
                {
                    this.pnlHeadNewNote.BackColor = this.notes.GetSelectClr(this.note.skinNr);
                }
                else
                {
                    this.pnlHeadNewNote.BackColor = this.notes.GetSelectClr(Settings.notesDefaultSkinnr);
                }
            }
        }

        /// <summary>
        /// Move note if pnlHead is being left clicked.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Mouse event arguments</param>
        private void pnlHeadNewNote_MouseMove(object sender, MouseEventArgs e)
        {
            if ((this.moving) && (e.Button == MouseButtons.Left))
            {
                if (this.note != null)
                {
                    this.pnlHeadNewNote.BackColor = this.notes.GetSelectClr(this.note.skinNr);
                }
                else
                {
                    this.pnlHeadNewNote.BackColor = this.notes.GetSelectClr(Settings.notesDefaultSkinnr);
                }

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
                this.Location = new Point(this.Location.X + dpx, this.Location.Y + dpy); //bug fix: #0000011
            }
            /*
            else
            {
                this.pnlHeadNewNote.BackColor = notes.GetPrimaryClr(Settings.NotesDefaultSkinnr);
            }
            */
        }

        /// <summary>
        /// End moving note.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void pnlHeadNewNote_MouseUp(object sender, MouseEventArgs e)
        {
            this.moving = false;
            if (this.note != null)
            {
                this.pnlHeadNewNote.BackColor = this.notes.GetPrimaryClr(this.note.skinNr);
            }
            else
            {
                this.pnlHeadNewNote.BackColor = this.notes.GetPrimaryClr(Settings.notesDefaultSkinnr);
            }
        }

        /// <summary>
        /// Show context menu.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void pnlNoteEdit_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStripTextActions.Show(this.Location.X + e.X, this.Location.X + e.Y);
            }
        }

        /// <summary>
        /// Removes 1 fontsyle from the fontsyles of the checkstyle rtb text.
        /// This methode does not check if selection lenght is okay.
        /// </summary>
        /// <param name="checkstyles">The FontStyle apply operations on.</param>
        /// <param name="removestyle">The FontStyle to remove.</param>
        /// <returns>The new fontstyle</returns>
        private FontStyle removestyle(FontStyle checkstyles, FontStyle removestyle)
        {
            FontStyle newstyles = checkstyles;
            newstyles -= removestyle;
            return newstyles;
        }

        /// <summary>
        /// User entered the note content box.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void rtbNote_Enter(object sender, EventArgs e)
        {
            if (this.note != null)
            {
                this.rtbNewNote.BackColor = this.notes.GetHighlightClr(this.note.skinNr);
            }
            else
            {
                this.rtbNewNote.BackColor = this.notes.GetHighlightClr(Settings.notesDefaultSkinnr);
            }

            this.SetToolbarEnabled(true);
        }

        /// <summary>
        /// User leaved the note content box.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void rtbNote_Leave(object sender, EventArgs e)
        {
            if (this.note != null)
            {
                this.rtbNewNote.BackColor = this.notes.GetSelectClr(this.note.skinNr);
            }
            else
            {
                this.rtbNewNote.BackColor = this.notes.GetSelectClr(Settings.notesDefaultSkinnr);
            }
        }

        /// <summary>
        /// A hyperlink is clicked, check settings to see if confirm launch dialog have
        /// to be showed, if not then directly launch the URL.
        /// </summary>
        /// <param name="sender">Sender object</param>
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
        /// Set font family and size.
        /// </summary>
        private void SetDefaultFontFamilyAndSize()
        {
            this.rtbNewNote.SelectAll();
            this.rtbNewNote.Font = new Font(Settings.fontContentFamily, (float)Settings.fontContentSize);
            this.rtbNewNote.Select(0, 0);
        }

        /// <summary>
        /// Set the font and textdirection FrmNewNote.
        /// </summary>
        /// <param name="title">The new note title.</param>
        /// <param name="content">The new note content.</param>
        private void SetFontSettings()
        {
            this.tbTitle.Font = new Font(Settings.fontTitleFamily, 11);
            this.rtbNewNote.Font = new Font(Settings.fontContentFamily, this.rtbNewNote.Font.Size);
            switch (Settings.fontTextdirection)
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
        /// Toggle toolbar buttons.
        /// </summary>
        /// <param name="enabled">true if toolbar should be enabled.</param>
        private void SetToolbarEnabled(bool enabled)
        {
            this.btnTextBold.Enabled = enabled;
            this.btnTextItalic.Enabled = enabled;
            this.btnTextStriketrough.Enabled = enabled;
            this.btnTextUnderline.Enabled = enabled;
            this.btnFontBigger.Enabled = enabled;
            this.btnFontSmaller.Enabled = enabled;
        }

        /// <summary>
        /// User entered the title box.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void tbTitle_Enter(object sender, EventArgs e)
        {
            if (this.note != null)
            {
                this.tbTitle.BackColor = this.notes.GetHighlightClr(this.note.skinNr);
            }
            else
            {
                this.tbTitle.BackColor = this.notes.GetHighlightClr(Settings.notesDefaultSkinnr);
            }

            this.SetToolbarEnabled(false);
        }

        /// <summary>
        /// User leaved the title box
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void tbTitle_Leave(object sender, EventArgs e)
        {
            if (this.note != null)
            {
                this.tbTitle.BackColor = this.notes.GetSelectClr(this.note.skinNr);
            }
            else
            {
                this.tbTitle.BackColor = this.notes.GetSelectClr(Settings.notesDefaultSkinnr);
            }

            this.SetToolbarEnabled(true);
        }

        #endregion Methods
    }
}
