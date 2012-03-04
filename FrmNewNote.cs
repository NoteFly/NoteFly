//-----------------------------------------------------------------------
// <copyright file="FrmNewNote.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2010-2012  Tom
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
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// New and edit note window.
    /// </summary>
    public sealed partial class FrmNewNote : Form
    {
        #region Fields (4)

        /// <summary>
        /// Margin between format buttons and content.
        /// </summary>
        private const int MARGIN = 5;

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
        /// <param name="locfrmnewnote">The location of the FrmNewNote should get.</param>
        /// <param name="sizefrmnewnote">The size of the FrnNewNote should get.</param>
        /// <param name="wordwrap">Wrap words that exceeded the width of the richedittext control.</param>
        public FrmNewNote(Notes notes, Note note, Point locfrmnewnote, Size sizefrmnewnote, bool wordwrap)
        {
            this.ConstructFrmNewNote(notes);
            this.SetFormTitle(true);
            this.SetTooltipAddCancelBtns(true);
            this.Location = locfrmnewnote;
            this.Size = sizefrmnewnote;
            this.rtbNewNote.WordWrap = wordwrap;
            this.menuWordWarp.Checked = wordwrap;
            this.note = note;
            this.SetColorsForm(this.note.SkinNr);
            this.tbTitle.Text = note.Title;
            if (string.IsNullOrEmpty(this.note.Tempcontent))
            {
                this.rtbNewNote.Rtf = note.GetContent();
            }
            else
            {
                this.rtbNewNote.Rtf = this.note.Tempcontent;

                // clear memory:
                this.note.Tempcontent = string.Empty;
                this.note.Tempcontent = null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the FrmNewNote class for a new note.
        /// </summary>
        /// <param name="notes">The class with access to all notes.</param>
        public FrmNewNote(Notes notes, int deltaX, int deltaY)
        {
            this.ConstructFrmNewNote(notes);
            this.SetFormTitle(false);
            this.SetTooltipAddCancelBtns(false);
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width / 2) - (this.Width / 2) + deltaX, (Screen.PrimaryScreen.WorkingArea.Height / 2) - (this.Height / 2) + deltaY);
            this.note = null;
            if (Settings.NotesDefaultRandomSkin)
            {
                Settings.NotesDefaultSkinnr = notes.GenerateRandomSkinnr();
            }

            this.SetColorsForm(Settings.NotesDefaultSkinnr);
            if (Settings.NotesDefaultTitleDate)
            {
                // The string returned by the ToShortDateString method is culture-sensitive.
                this.tbTitle.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            }
        }

        #endregion Constructors

        #region Methods (30)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="editnote"></param>
        private void SetTooltipAddCancelBtns(bool editnote)
        {
            if (editnote)
            {
                this.toolTip.SetToolTip(this.btnAddNote, Strings.T("Save editing note (Ctrl+S)"));
                this.toolTip.SetToolTip(this.btnCancel, Strings.T("Cancel editing note (escape)"));
            }
            else
            {
                this.toolTip.SetToolTip(this.btnAddNote, Strings.T("Save new note (Ctrl+S)"));
                this.toolTip.SetToolTip(this.btnCancel, Strings.T("Cancel new note (escape)"));
            }
        }

        /// <summary>
        /// Set the form title.
        /// </summary>
        /// <param name="editnote"></param>
        private void SetFormTitle(bool editnote)
        {
            StringBuilder sbtitle = new StringBuilder();
            if (editnote)
            {
                sbtitle.Append(Strings.T("edit note"));
            }
            else 
            {
                sbtitle.Append(Strings.T("new note"));
            }

            sbtitle.Append(" - ");
            sbtitle.Append(Program.AssemblyTitle);
            this.Text = sbtitle.ToString();
        }

        /// <summary>
        /// Initialize components FrmNewNote, set font, tooltip and richtextbox settings
        /// </summary>
        /// <param name="notes">Reference to notes class.</param>
        private void ConstructFrmNewNote(Notes notes)
        {
            this.DoubleBuffered = Settings.ProgramFormsDoublebuffered;
            this.InitializeComponent();
            Strings.TranslateForm(this);
            this.Size = new Size(Settings.NotesDefaultWidth, Settings.NotesDefaultHeight);            
            this.notes = notes;
            this.SetFontSettings();
            this.toolTip.Active = Settings.NotesTooltipsEnabled;
            this.rtbNewNote.DetectUrls = Settings.HighlightHyperlinks;
            this.tbTitle.Select();
        }

        /// <summary>
        /// Plugin format button clicked.
        /// </summary>
        /// <param name="sender">The button clicked</param>
        /// <param name="e">Event arguments</param>
        private void btnPluginFormatBtn_Click(object sender, EventArgs e)
        {
            this.rtbNewNote.EnableAutoDragDrop = true;
            for (int p = 0; p < PluginsManager.pluginsenabled.Length; p++)
            {
                this.rtbNewNote.Rtf = PluginsManager.pluginsenabled[p].NoteFormatBtnClicked(this.rtbNewNote, (Button)sender);
            }
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
            this.rtbNewNote.BackColor = this.notes.GetSelectClr(skinnr);

            this.btnTextBold.ForeColor = this.notes.GetTextClr(skinnr);
            this.btnTextItalic.ForeColor = this.notes.GetTextClr(skinnr);
            this.btnTextStriketrough.ForeColor = this.notes.GetTextClr(skinnr);
            this.btnTextUnderline.ForeColor = this.notes.GetTextClr(skinnr);
            this.btnTextBulletlist.ForeColor = this.notes.GetTextClr(skinnr);
            this.btnFontBigger.ForeColor = this.notes.GetTextClr(skinnr);
            this.btnFontSmaller.ForeColor = this.notes.GetTextClr(skinnr);

            this.btnTextBold.FlatAppearance.MouseOverBackColor = this.notes.GetSelectClr(skinnr);
            this.btnTextItalic.FlatAppearance.MouseOverBackColor = this.notes.GetSelectClr(skinnr);
            this.btnTextStriketrough.FlatAppearance.MouseOverBackColor = this.notes.GetSelectClr(skinnr);
            this.btnTextUnderline.FlatAppearance.MouseOverBackColor = this.notes.GetSelectClr(skinnr);
            this.btnTextBulletlist.FlatAppearance.MouseOverBackColor = this.notes.GetSelectClr(skinnr);
            this.btnFontBigger.FlatAppearance.MouseOverBackColor = this.notes.GetSelectClr(skinnr);
            this.btnFontSmaller.FlatAppearance.MouseOverBackColor = this.notes.GetSelectClr(skinnr);

            if (this.notes.GetPrimaryTexture(skinnr) != null)
            {
                this.BackgroundImageLayout = this.notes.GetPrimaryTextureLayout(skinnr);
                this.BackgroundImage = this.notes.GetPrimaryTexture(skinnr);
                this.pnlHeadNewNote.BackColor = Color.Transparent;
                this.lbTextTitle.BackColor = Color.Transparent;
                this.lbTextTitle.BackColor = Color.Transparent;
            }

            this.CreatePluginButtons(skinnr);
        }

        /// <summary>
        /// Create plugin buttons
        /// </summary>
        /// <param name="skinnr">The skin position</param>
        private void CreatePluginButtons(int skinnr)
        {
            for (int p = 0; p < PluginsManager.pluginsenabled.Length; p++)
            {
                if (PluginsManager.pluginsenabled[p].InitNoteFormatBtns() != null)
                {
                    foreach (Button btnPluginFormatBtn in PluginsManager.pluginsenabled[p].InitNoteFormatBtns())
                    {
                        this.tlpnlFormatbtn.ColumnCount += 1;
                        this.tlpnlFormatbtn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize, 32));
                        btnPluginFormatBtn.Click += new EventHandler(this.btnPluginFormatBtn_Click);
                        btnPluginFormatBtn.FlatStyle = FlatStyle.Flat;
                        btnPluginFormatBtn.FlatAppearance.BorderColor = Color.Black;
                        btnPluginFormatBtn.ForeColor = this.notes.GetTextClr(skinnr);
                        btnPluginFormatBtn.FlatAppearance.MouseOverBackColor = this.notes.GetSelectClr(skinnr);
                        btnPluginFormatBtn.TabStop = false;
                        this.tlpnlFormatbtn.Controls.Add(btnPluginFormatBtn, this.tlpnlFormatbtn.ColumnCount - 1, 0);
                    }
                }
            }
        }

        /// <summary>
        /// User pressed the accept note button. Note will now be saved.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnAddNote_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbTitle.Text))
            {
                this.tbTitle.Text = DateTime.Now.ToString();
            }
            else if (string.IsNullOrEmpty(this.rtbNewNote.Text))
            {
                string newnote_entercontent = Strings.T("Please enter some content.");
                this.rtbNewNote.Text = newnote_entercontent;
                this.rtbNewNote.Focus();
                this.rtbNewNote.SelectAll();
            }
            else
            {
                if (this.note == null)
                {
                    // new note
                    this.notes.AddNoteDefaultSettings(this.tbTitle.Text, Settings.NotesDefaultSkinnr, this.Location.X, this.Location.Y, this.Width, this.Height, this.rtbNewNote.Rtf, this.rtbNewNote.WordWrap);
                }
                else
                {
                    // editing note, update note
                    this.note.Title = this.tbTitle.Text;
                    if (!xmlUtil.WriteNote(this.note, this.notes.GetSkinName(this.note.SkinNr), this.rtbNewNote.Rtf))
                    {
                        string newnote_exccantwritenote = Strings.T("Could not write note.");
                        throw new ApplicationException(newnote_exccantwritenote);
                    }

                    this.note.Tempcontent = this.rtbNewNote.Rtf;
                    this.note.Wordwarp = this.rtbNewNote.WordWrap;
                    this.note.CreateForm();
                    if (this.note.Tempcontent != null)
                    {
                        this.note.Tempcontent = null;
                    }
                }

                if (PluginsManager.pluginsenabled != null)
                {
                    for (int i = 0; i < PluginsManager.pluginsenabled.Length; i++)
                    {
                        PluginsManager.pluginsenabled[i].SavingNote(this.rtbNewNote.Rtf, this.tbTitle.Text);
                    }
                }

                Program.Formmanager.Frmneweditnoteopen = false;
                SyntaxHighlight.DeinitHighlighter();
                Program.Formmanager.FrmManageNotesNeedUpdate = true;
                //this.notes.FrmManageNotesNeedUpdate = true;
                Program.Formmanager.RefreshFrmManageNotes();
                this.Close();
                GC.Collect();
            }
        }

        /// <summary>
        /// User pressed the cancel button, all things typed in FrmNewNote window will be lost.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Program.Formmanager.Frmneweditnoteopen = false;
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

            this.rtbNewNote.Focus();
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

            this.rtbNewNote.Focus();
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

            this.rtbNewNote.Focus();
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

            this.rtbNewNote.Focus();
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

            this.rtbNewNote.Focus();
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

            this.rtbNewNote.Focus();
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
            else
            {
                this.rtbNewNote.SelectionFont = new System.Drawing.Font(this.rtbNewNote.SelectionFont.FontFamily, newsize, this.rtbNewNote.SelectionFont.Style);
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
        /// Check if menuCopyContent and menuCopyTitle should be enabled
        /// and add plugin contenxt menu's
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void contextMenuStripTextActions_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.menuCopyContent.Enabled = false;
            if (this.rtbNewNote.TextLength > 0)
            {
                this.menuCopyContent.Enabled = true;
            }

            this.menuCopyTitle.Enabled = false;
            if (this.tbTitle.TextLength > 0)
            {
                this.menuCopyTitle.Enabled = true;
            }

            while (this.contextMenuStripTextActions.Items.Count > 8)
            {
                this.contextMenuStripTextActions.Items.RemoveAt(8);
            }

            for (int i = 0; i < PluginsManager.pluginsenabled.Length; i++)
            {
                if (PluginsManager.pluginsenabled[i].InitFrmNewNoteMenu() != null)
                {
                    ToolStripItem menuplugin = PluginsManager.pluginsenabled[i].InitFrmNewNoteMenu();
                    menuplugin.Click += new EventHandler(this.menumain_Click);
                    this.contextMenuStripTextActions.Items.Add(menuplugin);
                }
            }
        }

        /// <summary>
        /// Plugin menu in main contextmenu clicked
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void menumain_Click(object sender, EventArgs e)
        {
            if (PluginsManager.pluginsenabled != null)
            {
                for (int i = 0; i < PluginsManager.pluginsenabled.Length; i++)
                {
                    ToolStripItem toolstripitemplugin = (ToolStripItem)sender;
                    this.rtbNewNote.Rtf = PluginsManager.pluginsenabled[i].MenuFrmNewNoteClicked(this.rtbNewNote, toolstripitemplugin);
                }
            }
        }

        /// <summary>
        /// Copy the note content.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void copyTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.rtbNewNote.Text))
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
                this.menuPasteTo.Enabled = true;
            }
            else
            {
                this.menuPasteTo.Enabled = false;
            }
        }

        /// <summary>
        /// Form got focus, remove transparency.
        /// </summary>
        /// <param name="sender">Sender object</param>
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
        /// <param name="sender">Sender object</param>
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
        /// Import a file as note.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dlgresopennote = this.openNoteFileDialog.ShowDialog();
            if (dlgresopennote == DialogResult.OK)
            {
                StreamReader reader = null;
                try
                {
                    if (File.Exists(this.openNoteFileDialog.FileName))
                    {
                        reader = new StreamReader(this.openNoteFileDialog.FileName, true); // detect encoding
                        ImportNotes importnote = new ImportNotes(this.notes);                        
                        switch (this.openNoteFileDialog.FilterIndex)
                        {
                            case 1:
                                importnote.ReadTextfile(reader, this.rtbNewNote);
                                break;
                            case 2:
                                importnote.ReadRTFfile(reader, this.rtbNewNote);
                                break;
                            case 3:
                                importnote.ReadKeyNotefile(reader, this.rtbNewNote);
                                break;
                            case 4:
                                importnote.ReadTomboyfile(reader, this.openNoteFileDialog.FileName, this.tbTitle, this.rtbNewNote);
                                break;
                            case 5:
                                importnote.ReadMicroSENotefile(reader, this.tbTitle, this.rtbNewNote);
                                break;
                        }
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
        }

        /*
        /// <summary>
        /// Import a textfile as note content for a new note.
        /// </summary>
        /// <param name="reader">A streamreader to read the note content with</param>
        private void ReadTextfile(StreamReader reader)
        {
            this.rtbNewNote.Text = reader.ReadToEnd();
        }
         

        /// <summary>
        /// Import a rtf file as note content for a new note.
        /// </summary>
        /// <param name="reader">Streamreader to read the note content.</param>
        private void ReadRTFfile(StreamReader reader)
        {
            this.rtbNewNote.Rtf = reader.ReadToEnd();
            this.SetDefaultFontFamilyAndSize();
        }
        

        /// <summary>
        /// Import a KeyNote note file as note content for a new note.
        /// </summary>
        /// <param name="reader">The streamreader to read the KeyNote.</param>
        private void ReadKeyNotefile(StreamReader reader)
        {
            uint linenum = 0;
            string curline = reader.ReadLine(); // no CR+LF characters
            string newnote_importerror = Strings.T("import error");
            if (curline == "#!GFKNT 2.0")
            {
                while (curline != "%:")
                {
                    curline = reader.ReadLine();
                    linenum++;

                    // should normally be except %: around line 42.
                    if (linenum > 50)
                    {
                        string newnote_cannotfindkeynotecontent = Strings.T("Cannot find KeyNote NF note content.");
                        MessageBox.Show(newnote_cannotfindkeynotecontent, newnote_importerror, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Log.Write(LogType.error, newnote_cannotfindkeynotecontent);
                    }
                }

                curline = reader.ReadLine();
                StringBuilder sb = new StringBuilder(curline);
                while (curline != "%%")
                {
                    curline = reader.ReadLine();
                    sb.Append(curline);
                    linenum++;

                    // limit to 16000 lines
                    if (linenum > 16000)
                    {
                        break;
                    }
                }

                this.rtbNewNote.Rtf = sb.ToString();
                this.SetDefaultFontFamilyAndSize();
            }
            else
            {
                string newnote_notkeynotefile = Strings.T("Not a KeyNote NF note.");
                MessageBox.Show(newnote_notkeynotefile, newnote_importerror, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Write(LogType.error, newnote_notkeynotefile);
            }
        }

        /// <summary>
        ///  Import a Tomboy note file as note content for a new note.
        ///  And set the new note title.
        /// </summary>
        /// <param name="reader">The streamreader to read the Tomboy note with.</param>
        /// <param name="tomboynotefile">The Tomboy note file full filepath and filename.</param>
        private void ReadTomboyfile(StreamReader reader, string tomboynotefile)
        {
            this.tbTitle.Text = xmlUtil.GetContentString(tomboynotefile, "title");
            this.rtbNewNote.Clear();
            System.Xml.XmlTextReader xmlreader = new System.Xml.XmlTextReader(reader);
            xmlreader.ProhibitDtd = true;
            while (xmlreader.Read())
            {
                if (xmlreader.Name == "note-content")
                {
                    string tomboycontent = xmlreader.ReadInnerXml();
                    bool innode = false;
                    int startnodepos = 0;
                    int startcontentnode = int.MaxValue;
                    List<int> formatstartpos = new List<int>();
                    List<string> formattype = new List<string>();
                    List<int> formatlen = new List<int>();

                    for (int i = 0; i < tomboycontent.Length; i++)
                    {
                        if (tomboycontent[i] == '<')
                        {
                            startnodepos = i;
                            innode = true;
                        }
                        else if (tomboycontent[i] == '>')
                        {
                            string nodenameatt = tomboycontent.Substring(startnodepos, i - startnodepos);
                            string nodename = string.Empty;
                            if (nodenameatt.StartsWith("</"))
                            {
                                for (int c = 0; c < nodenameatt.Length; c++)
                                {
                                    if (nodenameatt[c] == ' ')
                                    {
                                        nodename = nodenameatt.Substring(2, c - 2);
                                        break;
                                    }
                                    else if (c == nodenameatt.Length - 1)
                                    {
                                        nodename = nodenameatt.Substring(2, nodenameatt.Length - 2);
                                        break;
                                    }
                                }

                                formattype.Add(nodename);
                                formatstartpos.Add(startcontentnode);
                                formatlen.Add(this.rtbNewNote.TextLength - startcontentnode);
                            }
                            else
                            {
                                startcontentnode = this.rtbNewNote.TextLength;
                            }

                            innode = false;
                        }
                        else if (!innode)
                        {
                            this.rtbNewNote.Text = this.rtbNewNote.Text + tomboycontent[i];
                        }
                    }

                    for (int i = 0; i < formattype.Count; i++)
                    {
                        this.rtbNewNote.Select(formatstartpos[i], formatlen[i]);
                        switch (formattype[i])
                        {
                            case "bold":
                                this.btnTextBold_Click(null, null);
                                break;
                            case "italic":
                                this.btnTextItalic_Click(null, null);
                                break;
                            case "strikethrough":
                                this.btnTextStriketrough_Click(null, null);
                                break;
                            case "list":
                                this.btnTextBulletlist_Click(null, null);
                                break;
                            case "size:huge":
                                this.btnFontBigger_Click(null, null);
                                this.btnFontBigger_Click(null, null);
                                break;
                            case "size:small":
                                this.btnFontSmaller_Click(null, null);
                                this.btnFontSmaller_Click(null, null);
                                break;
                            case "link:internal":
                                // broken link because notefly does not have linking between notes but still make it underlined as reference that is was there.
                                this.btnTextUnderline_Click(null, null);
                                break;
                        }
                    }

                    this.rtbNewNote.Select(0, 0);
                }
            }
        }

        /// <summary>
        /// Import a MicroSE note file as note content for a new note.
        /// </summary>
        /// <param name="reader">The streamreader to read the MicroSE note file with.</param>
        private void ReadMicroSENotefile(StreamReader reader)
        {
            bool contentstarted = false;
            StringBuilder sbcontent = new StringBuilder();
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line.StartsWith("title;"))
                {
                    int possep = line.LastIndexOf(';') + 1; // without ';' itself
                    string title = line.Substring(possep, line.Length - possep);
                    this.tbTitle.Text = title;
                }

                if (line.StartsWith(@"{\rtf1") || contentstarted)
                {
                    sbcontent.AppendLine(line);
                    if (line.Equals("}"))
                    {
                        this.rtbNewNote.Rtf = sbcontent.ToString();
                        contentstarted = false;
                    }
                    else
                    {
                        contentstarted = true;
                    }
                }
            }
        }
        */

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
                if (SyntaxHighlight.KeywordsInitialized)
                {
                    SyntaxHighlight.InitHighlighter();
                }

                SyntaxHighlight.CheckSyntaxFull(this.rtbNewNote, this.GetSkinnr(), this.notes);
            }
            else
            {
                string newnote_emptyclipboard = Strings.T("There is no text on the clipboard.");
                MessageBox.Show(newnote_emptyclipboard);
                Log.Write(LogType.error, newnote_emptyclipboard);
            }
        }

        /// <summary>
        /// Resizing the FtmNewNote form.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Mouse event arguments</param>
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
        /// <param name="e">Mouse event arguments</param>
        private void pnlHeadNewNote_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.moving = true;
                this.oldp = e.Location;
                if (this.note != null)
                {
                    this.pnlHeadNewNote.BackColor = this.notes.GetSelectClr(this.note.SkinNr);
                }
                else
                {
                    this.pnlHeadNewNote.BackColor = this.notes.GetSelectClr(Settings.NotesDefaultSkinnr);
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
            if (this.moving && e.Button == MouseButtons.Left)
            {
                if (this.note != null)
                {
                    this.pnlHeadNewNote.BackColor = this.notes.GetSelectClr(this.note.SkinNr);
                }
                else
                {
                    this.pnlHeadNewNote.BackColor = this.notes.GetSelectClr(Settings.NotesDefaultSkinnr);
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
                this.Location = new Point(this.Location.X + dpx, this.Location.Y + dpy);
            }
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
                this.pnlHeadNewNote.BackColor = this.notes.GetPrimaryClr(this.note.SkinNr);
                if (this.BackgroundImage != null)
                {
                    this.pnlHeadNewNote.BackColor = Color.Transparent;
                }
            }
            else
            {
                this.pnlHeadNewNote.BackColor = this.notes.GetPrimaryClr(Settings.NotesDefaultSkinnr);
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
                this.rtbNewNote.BackColor = this.notes.GetHighlightClr(this.note.SkinNr);
            }
            else
            {
                this.rtbNewNote.BackColor = this.notes.GetHighlightClr(Settings.NotesDefaultSkinnr);
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
                this.rtbNewNote.BackColor = this.notes.GetSelectClr(this.note.SkinNr);
            }
            else
            {
                this.rtbNewNote.BackColor = this.notes.GetSelectClr(Settings.NotesDefaultSkinnr);
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
            Program.LoadLink(e.LinkText, true);
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
        /// Set the font and textdirection FrmNewNote.
        /// </summary>
        private void SetFontSettings()
        {
            this.tbTitle.Font = new Font(Settings.FontTitleFamily, 11);
            this.rtbNewNote.Font = new Font(Settings.FontContentFamily, this.rtbNewNote.Font.Size);
            switch (Settings.FontTextdirection)
            {
                case 0:
                    this.tbTitle.RightToLeft = RightToLeft.No;
                    this.rtbNewNote.RightToLeft = RightToLeft.No;
                    break;
                case 1:
                    this.tbTitle.RightToLeft = RightToLeft.Yes;
                    this.rtbNewNote.RightToLeft = RightToLeft.Yes;
                    break;
                default:
                    this.tbTitle.RightToLeft = RightToLeft.No;
                    this.rtbNewNote.RightToLeft = RightToLeft.No;
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
            this.btnTextBulletlist.Enabled = enabled;
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
                this.tbTitle.BackColor = this.notes.GetHighlightClr(this.note.SkinNr);
            }
            else
            {
                this.tbTitle.BackColor = this.notes.GetHighlightClr(Settings.NotesDefaultSkinnr);
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
                this.tbTitle.BackColor = this.notes.GetSelectClr(this.note.SkinNr);
            }
            else
            {
                this.tbTitle.BackColor = this.notes.GetSelectClr(Settings.NotesDefaultSkinnr);
            }

            this.SetToolbarEnabled(true);
        }

        /// <summary>
        /// Handle keyboard shortcuts
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">KeyEvent arguments</param>
        private void FrmNewNote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.B:
                        this.btnTextBold_Click(null, EventArgs.Empty);
                        break;
                    case Keys.I:
                        this.btnTextItalic_Click(null, EventArgs.Empty);
                        break;
                    case Keys.U:
                        this.btnTextUnderline_Click(null, EventArgs.Empty);
                        break;
                    case Keys.T:
                        this.btnTextStriketrough_Click(null, EventArgs.Empty);
                        break;
                }
            }
        }

        /// <summary>
        /// Make buttet item of current selected line(s).
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnTextBulletlist_Click(object sender, EventArgs e)
        {
            if (this.checksellen())
            {
                this.rtbNewNote.SelectionBullet = !this.rtbNewNote.SelectionBullet;
            }
        }

        /// <summary>
        /// Show or hide buttons for formatting.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void menuShowtoolbar_Click(object sender, EventArgs e)
        {
            this.menuShowtoolbar.Checked = !this.menuShowtoolbar.Checked;
            if (this.menuShowtoolbar.Checked)
            {
                this.rtbNewNote.Height = this.Height - this.rtbNewNote.Location.Y - (this.Height - this.tlpnlFormatbtn.Location.Y + MARGIN);
            }
            else
            {
                this.rtbNewNote.Height = this.Height - this.rtbNewNote.Location.Y - (this.Height - (this.tlpnlFormatbtn.Location.Y + this.tlpnlFormatbtn.Height));
            }

            this.SetToolbarEnabled(this.menuShowtoolbar.Checked);
        }

        /// <summary>
        /// Toggle to wrap lines in the richedit control
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void menuWordWarp_Click(object sender, EventArgs e)
        {
            this.rtbNewNote.WordWrap = this.menuWordWarp.Checked;
        }

        /// <summary>
        /// Paste text to title if clipboard contains any text.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void titleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                this.tbTitle.Text += Clipboard.GetText();
            }
            else
            {
                string newnote_emptyclipboard = Strings.T("There is no text on the clipboard.");
                MessageBox.Show(newnote_emptyclipboard);
                Log.Write(LogType.error, newnote_emptyclipboard);
            }
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rtbNewNote_KeyUp(object sender, KeyEventArgs e)
        {
            if (!SyntaxHighlight.KeywordsInitialized)
            {
                SyntaxHighlight.InitHighlighter();
            }

            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter || e.KeyValue == 190)
            {
                SyntaxHighlight.CheckSyntaxQuick(this.rtbNewNote, this.GetSkinnr(), this.notes);
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                SyntaxHighlight.CheckSyntaxFull(this.rtbNewNote, this.GetSkinnr(), this.notes);               
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int GetSkinnr()
        {
            int skinnr = Settings.NotesDefaultSkinnr;
            if (this.note != null)
            {
                // edit note
                skinnr = this.note.SkinNr;
            }

            return skinnr;
        }

        #endregion Methods
    }
}