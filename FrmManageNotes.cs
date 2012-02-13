//-----------------------------------------------------------------------
// <copyright file="FrmManageNotes.cs" company="NoteFly">
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
    using System.Data;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    /// <summary>
    /// Manage notes window
    /// </summary>
    public sealed partial class FrmManageNotes : Form
    {
        #region Fields (8)

        /// <summary>
        /// Constant for the fixed width of the number note colum in datagridview1.
        /// </summary>
        private const int COLNOTENRFIXEDWIDTH = 30;

        /// <summary>
        /// The width of a imported note by default.
        /// </summary>
        private const int DEFAULTIMPORTNOTEWIDTH = 240;

        /// <summary>
        /// The height of a imported note by default.
        /// </summary>
        private const int DEFAULTIMPORTNOTEHEIGHT = 200;

        /// <summary>
        /// Rereference to notes
        /// </summary>
        private Notes notes;

        /// <summary>
        /// Delta point
        /// </summary>
        private Point oldp;

        /// <summary>
        /// The previous painted row number.
        /// </summary>
        private int prevrownr = -1;

        /// <summary>
        /// The previous of previous painted row number.
        /// </summary>
        private int secondprevrownr = -2;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the FrmManageNotes class.
        /// </summary>
        /// <param name="notes">The class notes, with access to all the notes.</param>
        public FrmManageNotes(Notes notes)
        {
            this.DoubleBuffered = Settings.ProgramFormsDoublebuffered;
            this.InitializeComponent();
            this.SetFormTitle();
            Strings.TranslateForm(this);
            this.notes = notes;
            this.SetSkin();
            this.DrawNotesGrid();
            this.SetDataGridViewColumsWidth();

            if (this.dataGridViewNotes.RowCount > 0)
            {
                if ((bool)this.dataGridViewNotes.Rows[0].Cells["visible"].Value == true)
                {
                    this.btnShowSelectedNotes.Text = Strings.T("&hide selected");
                }
                else
                {
                    this.btnShowSelectedNotes.Text = Strings.T("&show selected");
                }
            }
            else
            {
                this.btnShowSelectedNotes.Text = Strings.T("&show selected");
            }

            if (PluginsManager.pluginsenabled != null)
            {
                for (int p = 0; p < PluginsManager.pluginsenabled.Length; p++)
                {
                    if (PluginsManager.pluginsenabled[p].InitFrmManageNotesBtns() != null)
                    {
                        Button[] buttons = PluginsManager.pluginsenabled[p].InitFrmManageNotesBtns();
                        for (int i = 0; i < buttons.Length; i++)
                        {
                            this.tableLayoutPanelButtons.ColumnCount += 1;
                            this.tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize, 80));
                            this.tableLayoutPanelButtons.Controls.Add(buttons[i], this.tableLayoutPanelButtons.ColumnCount - 1, 0);
                        }
                    }
                }
            }
        }

        #endregion Constructors

        #region Methods (20)

        /// <summary>
        /// Reset the previous drawed row numbers in datagridview1.
        /// </summary>
        public void Resetdatagrid()
        {
            this.prevrownr = -1;
            this.secondprevrownr = -2;
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetSkin()
        {
            this.BackColor = this.notes.GetPrimaryClr(Settings.ManagenotesSkinnr);
            this.pnlHead.BackColor = this.notes.GetPrimaryClr(Settings.ManagenotesSkinnr);
            this.ForeColor = this.notes.GetTextClr(Settings.ManagenotesSkinnr);

            this.btnShowSelectedNotes.FlatAppearance.MouseOverBackColor = this.notes.GetHighlightClr(Settings.ManagenotesSkinnr);
            this.btnNoteDelete.FlatAppearance.MouseOverBackColor = this.notes.GetHighlightClr(Settings.ManagenotesSkinnr);
            this.btnRestoreAllNotes.FlatAppearance.MouseOverBackColor = this.notes.GetHighlightClr(Settings.ManagenotesSkinnr);
            this.btnBackAllNotes.FlatAppearance.MouseOverBackColor = this.notes.GetHighlightClr(Settings.ManagenotesSkinnr);

            this.btnShowSelectedNotes.ForeColor = this.notes.GetTextClr(Settings.ManagenotesSkinnr);
            this.btnNoteDelete.ForeColor = this.notes.GetTextClr(Settings.ManagenotesSkinnr);
            this.btnRestoreAllNotes.ForeColor = this.notes.GetTextClr(Settings.ManagenotesSkinnr);
            this.btnBackAllNotes.ForeColor = this.notes.GetTextClr(Settings.ManagenotesSkinnr);

            if (this.notes.GetPrimaryTexture(Settings.ManagenotesSkinnr) != null)
            {
                this.BackgroundImage = this.notes.GetPrimaryTexture(Settings.ManagenotesSkinnr);
                this.BackgroundImageLayout = this.notes.GetPrimaryTextureLayout(Settings.ManagenotesSkinnr);
                this.pnlHead.BackColor = Color.Transparent;
            }
        }

        /// <summary>
        /// Set the title of this form.
        /// </summary>
        private void SetFormTitle()
        {
            this.Text = Strings.T("Manage notes") + " - " + Program.AssemblyTitle;
        }


        /// <summary>
        /// Request to backup all notes to a file.
        /// Ask where to save then do it.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnBackAllNotes_Click(object sender, EventArgs e)
        {
            DialogResult savebackupdlgres = this.saveExportFileDialog.ShowDialog();
            if (savebackupdlgres == DialogResult.OK)
            {
                switch (this.saveExportFileDialog.FilterIndex)
                {
                    case 1:
                        xmlUtil.WriteNoteFlyNotesBackupFile(this.saveExportFileDialog.FileName, this.notes);
                        break;
                    case 2:
                        this.WriteStickiesCSVBackupfile(this.saveExportFileDialog.FileName);
                        break;
                    case 3:
                        this.WritePNotesBackupfile(this.saveExportFileDialog.FileName);
                        break;
                }
            }
        }

        /// <summary>
        /// Write a stickies CSV backup file.
        /// </summary>
        /// <param name="filename">The full path and filename to write the CSV formatted stickies
        /// compatible file format.</param>
        private void WriteStickiesCSVBackupfile(string filename)
        {
            FileStream fs = null;
            StreamWriter writer = null;
            try
            {
                fs = new FileStream(filename, FileMode.Create);
                writer = new StreamWriter(fs, System.Text.Encoding.ASCII);
                writer.WriteLine("\"Title\",\"Date/Time\",\"Colour\",\"Width\",\"RTF\"");
                for (int i = 0; i < this.notes.CountNotes; i++)
                {
                    Note curnote = this.notes.GetNote(i);
                    string content = curnote.GetContent();
                    for (int c = content.Length - 1; c > 0; c--)
                    {
                        if (content[c] == '\n' || content[c] == '\r')
                        {
                            content = content.Remove(c, 1);
                        }
                    }

                    Color primaryclr = this.notes.GetPrimaryClr(curnote.SkinNr);
                    int colornum = System.Drawing.ColorTranslator.ToWin32(primaryclr);
                    FileInfo notefile = new FileInfo(Path.Combine(Settings.NotesSavepath, curnote.Filename));
                    TimeSpan ts = notefile.CreationTime - new DateTime(1970, 1, 1, 0, 0, 0);
                    string unixtimestr = Convert.ToString(ts.TotalSeconds);
                    int poscomma = unixtimestr.IndexOf(',');
                    if (poscomma > 0)
                    {
                        unixtimestr = unixtimestr.Substring(0, poscomma);
                    }

                    writer.Write("\"");
                    writer.Write(this.encode_title(curnote.Title));
                    writer.Write("\",\"");
                    writer.Write(unixtimestr);
                    writer.Write("\",\"");
                    writer.Write(colornum);
                    writer.Write("\",\"");
                    writer.Write(curnote.Width);
                    writer.Write("\",\"");
                    writer.Write(content.ToString());
                    writer.WriteLine("\"");
                }
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// Write a PNotes full backup file.
        /// Currently without working title, position, last change.
        /// </summary>
        /// <param name="filename">Filename of the file to write</param>
        private void WritePNotesBackupfile(string filename)
        {
            FileStream fs = null;
            StreamWriter writer = null;
            try
            {
                fs = new FileStream(this.saveExportFileDialog.FileName, FileMode.Create);
                writer = new StreamWriter(fs, System.Text.Encoding.ASCII);
                char chrstartdoc = (char)2;
                char chrstartnotefilename = (char)3;
                char chrendnotefilename = (char)4;
                const string PNOTESEXTENSION = ".pnote";
                char chrenddoc = (char)0;
                string[] pnotesfilenames = new string[this.notes.CountNotes];
                writer.Write(chrstartdoc);
                for (int i = 0; i < this.notes.CountNotes; i++)
                {
                    DateTime dtnotenow = DateTime.Now;
                    writer.Write("[");
                    StringBuilder pnotesfilenamenote = new StringBuilder(dtnotenow.Year.ToString(CultureInfo.InvariantCulture.NumberFormat));
                    pnotesfilenamenote.Append(dtnotenow.Month.ToString(CultureInfo.InvariantCulture.NumberFormat));
                    pnotesfilenamenote.Append(dtnotenow.Day.ToString(CultureInfo.InvariantCulture.NumberFormat));
                    pnotesfilenamenote.Append(dtnotenow.Hour.ToString(CultureInfo.InvariantCulture.NumberFormat));
                    pnotesfilenamenote.Append(dtnotenow.Minute.ToString(CultureInfo.InvariantCulture.NumberFormat));
                    int ms = dtnotenow.Millisecond + i;
                    if (ms >= 1000)
                    {
                        ms -= 1000;
                    }

                    pnotesfilenamenote.Append(ms.ToString());
                    writer.Write(pnotesfilenamenote.ToString());
                    writer.Write("]\r\n");
                    string title;
                    if (this.notes.GetNote(i).Title.Length > 127)
                    {
                        title = this.notes.GetNote(i).Title.Substring(0, 127);
                    }
                    else
                    {
                        title = this.notes.GetNote(i).Title;
                    }

                    writer.Write("data=");

                    // FIXME data= ise ingored and datetime is choicen as title instead.
                    for (int c = 0; c < title.Length; c++)
                    {
                        int titlechr = title[c];
                        writer.Write(titlechr.ToString("X") + "00");
                    }

                    int restchar = 127 - title.Length;
                    while (restchar > 0)
                    {
                        writer.Write("0000");
                        restchar--;
                    }

                    writer.Write("0000DB0708000500130016000C001B00790100000000000000000000F7FC01000000F6010000060100000A0300001A0200000000000000000000000000000000000013\r\n");

                    // TODO figure out rel_position
                    writer.Write("rel_position=9A9999999979E53F0AD7A3703D0ABF3F40010000DC000000F1\r\n");
                    writer.Write("add_appearance=00000000000000000000000000\r\n");
                    string hexyear = this.fillstrleadzeros(dtnotenow.Year.ToString("X"), 4).Substring(2, 2) + this.fillstrleadzeros(dtnotenow.Year.ToString("X"), 4).Substring(0, 2);
                    string hexmonth = this.fillstrleadzeros(dtnotenow.Month.ToString("X"), 2);
                    string hexday = this.fillstrleadzeros(dtnotenow.Day.ToString("X"), 2);
                    string hexhour = this.fillstrleadzeros(dtnotenow.Hour.ToString("X"), 2);
                    string hexmin = this.fillstrleadzeros(dtnotenow.Minute.ToString("X"), 2);
                    writer.Write("creation=" + hexyear + hexmonth + "000400" + hexday + "00" + hexhour + "00" + hexmin + "00040068018A\r\n");

                    pnotesfilenames[i] = pnotesfilenamenote.ToString();
                }

                for (int i = 0; i < this.notes.CountNotes; i++)
                {
                    writer.Write(chrstartnotefilename);
                    writer.Write(pnotesfilenames[i] + PNOTESEXTENSION);
                    writer.Write(chrendnotefilename);
                    writer.Write(this.notes.GetNote(i).GetContent());
                    writer.Write(chrenddoc);
                }
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        /// <summary>
        /// Add leadings zero to string till string is a fixed length
        /// </summary>
        /// <param name="str">The string to add leading zero's to</param>
        /// <param name="len">The length</param>
        /// <returns>A string of with the given length</returns>
        private string fillstrleadzeros(string str, int len)
        {
            while (str.Length < len)
            {
                str = "0" + str;
            }

            return str;
        }

        /// <summary>
        /// Close form
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// The user pressed the delete button for a note.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnNoteDelete_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewNotes.SelectedRows.Count <= 0)
            {
                string managenotes_nothingselected = Strings.T("Nothing selected.");
                Log.Write(LogType.info, managenotes_nothingselected);
                MessageBox.Show(managenotes_nothingselected);
            }
            else
            {
                if (Settings.ConfirmDeletenote)
                {
                    string managenotes_deleteselectednotes = Strings.T("Are you sure you want to delete the selected note(s)?");
                    string managenotes_deleteselectednotestitle = Strings.T("delete?");
                    DialogResult deleteres = MessageBox.Show(managenotes_deleteselectednotes, managenotes_deleteselectednotestitle, MessageBoxButtons.YesNo);
                    if (deleteres == DialogResult.Yes)
                    {
                        this.DeleteNotesSelectedRowsGrid(this.dataGridViewNotes.SelectedRows);
                    }
                }
                else
                {
                    this.DeleteNotesSelectedRowsGrid(this.dataGridViewNotes.SelectedRows);
                }

                this.Resetdatagrid();
                this.DrawNotesGrid();
                this.SetDataGridViewColumsWidth();
                this.btnNoteDelete.Enabled = false;
                if (this.notes.CountNotes > 0)
                {
                    this.btnNoteDelete.Enabled = true;
                }
            }

            this.Resetdatagrid();
            this.notes.FrmManageNotesNeedUpdate = true;
            Application.DoEvents();
        }

        /// <summary>
        /// Request to restore all notes from a backup file.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnRestoreAllNotes_Click(object sender, EventArgs e)
        {
            DialogResult openbackupdlgres = this.openImportFileDialog.ShowDialog();
            if (openbackupdlgres == DialogResult.OK)
            {
                switch (this.openImportFileDialog.FilterIndex)
                {
                    case 1:
                        this.ReadNoteFlyBackupFile(this.openImportFileDialog.FileName);
                        break;
                    case 2:
                        this.ReadStickiesCSVFile(this.openImportFileDialog.FileName);
                        break;
                    case 3:
                        this.ReadPNotesBackupFile(this.openImportFileDialog.FileName);
                        break;
                    case 4:
                        this.ReadCintaNotesXMLFile(this.openImportFileDialog.FileName);
                        break;
                }

                this.Resetdatagrid();
                this.DrawNotesGrid();
                this.SetDataGridViewColumsWidth();

                if (this.notes.CountNotes > 0)
                {
                    this.btnNoteDelete.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Read a NoteFly backup file.
        /// </summary>
        /// <param name="file">The full path and filename of the notefly backup file.</param>
        private void ReadNoteFlyBackupFile(string file)
        {
            if (this.notes.CountNotes > 0)
            {
                string managenotes_deleteallcurrentnotes = Strings.T("Do you want to delete all current notes?");
                string managenotes_deleteallcurrentnotestitle = Strings.T("Are you sure?");
                DialogResult eraseres = MessageBox.Show(managenotes_deleteallcurrentnotes, managenotes_deleteallcurrentnotestitle, MessageBoxButtons.YesNoCancel);
                if (eraseres == DialogResult.Yes)
                {
                    for (int i = 0; i < this.notes.CountNotes; i++)
                    {
                        File.Delete(Path.Combine(Settings.NotesSavepath, this.notes.GetNote(i).Filename));
                    }

                    Log.Write(LogType.info, "Deleted all notes for restoring notes backup.");
                }
                else if (eraseres == DialogResult.Cancel)
                {
                    Log.Write(LogType.info, "Cancelled restore notes backup.");
                    return;
                }

                for (int i = 0; i < this.notes.CountNotes; i++)
                {
                    this.notes.GetNote(i).DestroyForm();
                }

                while (this.notes.CountNotes > 0)
                {
                    try
                    {
                        this.notes.RemoveNote(0);
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
            }

            xmlUtil.ReadNoteFlyNotesBackupFile(this.notes, file);
            this.notes.LoadNotes(true, false);
        }

        /// <summary>
        /// Read a Stickies notes CSV file.
        /// </summary>
        /// <param name="file">The stickies CSV file</param>
        private void ReadStickiesCSVFile(string file)
        {
            StreamReader reader = null;
            try
            {
                int linenr = 0;
                int postitle = int.MinValue;
                int poscolour = int.MinValue;
                int poswidth = int.MinValue;
                int poscontent = int.MinValue;
                reader = new StreamReader(file, true);
                while (!reader.EndOfStream)
                {
                    linenr++;
                    string line = reader.ReadLine();
                    string[] parts = line.Split(',');
                    line = null;
                    if (linenr == 1 && parts.Length == 5)
                    {
                        for (int i = 0; i < parts.Length; i++)
                        {
                            switch (parts[i])
                            {
                                case "\"Title\"":
                                    postitle = i;
                                    break;
                                case "\"Colour\"":
                                    poscolour = i;
                                    break;
                                case "\"Width\"":
                                    poswidth = i;
                                    break;
                                case "\"RTF\"":
                                    poscontent = i;
                                    break;
                            }
                        }
                    }

                    if (parts.Length == 5 && linenr > 1)
                    {
                        if (postitle >= 0 && poscolour >= 0 && poswidth >= 0 && poscontent >= 0)
                        {
                            string title_enc = this.RemoveQuotes(parts[postitle]);
                            string title = this.decode_title(title_enc);
                            int width;
                            try
                            {
                                width = Convert.ToInt32(this.RemoveQuotes(parts[poswidth]));
                            }
                            catch (InvalidCastException)
                            {
                                width = 200;
                            }

                            if (width <= 0)
                            {
                                width = 200;
                            }

                            string content = this.RemoveQuotes(parts[poscontent]);
                            this.notes.AddNoteDefaultSettings(title, Settings.NotesDefaultSkinnr, 10, 10, DEFAULTIMPORTNOTEWIDTH, DEFAULTIMPORTNOTEHEIGHT, content, true);
                        }
                        else
                        {
                            string managenotes_notstickies = Strings.T("CVS file does not seems to be in the Stickies format.");
                            Log.Write(LogType.error, managenotes_notstickies);
                            MessageBox.Show(managenotes_notstickies);
                        }
                    }
                    else if (linenr != 1)
                    {
                        string managenotes_notstickies = Strings.T("CVS file does not seems to be in the Stickies format, excepting 5 columns.");
                        Log.Write(LogType.error, managenotes_notstickies);
                        MessageBox.Show(managenotes_notstickies);
                    }
                }
            }
            finally
            {
                reader.Close();
            }

            Log.Write(LogType.info, "Imported stickies csv file: " + this.openImportFileDialog.FileName);
        }

        /// <summary>
        /// Read a PNotes full backup file.
        /// </summary>
        /// <param name="file">The file</param>
        private void ReadPNotesBackupFile(string file)
        {
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(this.openImportFileDialog.FileName, Encoding.ASCII); // ANSI
                char chrstartdoc = (char)2;
                char chrstartnotefilename = (char)3;
                char chrendnotefilename = (char)4;
                char[] startchar = new char[1];
                reader.Read(startchar, 0, 1);
                if (startchar[0] == chrstartdoc)
                {
                    bool notecontents = false;
                    int notenr = 0;
                    List<string> notetitles = new List<string>();
                    StringBuilder notecontent = new StringBuilder();
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (!notecontents)
                        {
                            if (line.StartsWith("data="))
                            {
                                int posstartdata = line.IndexOf('=') + 1;
                                string data = line.Substring(posstartdata, line.Length - posstartdata);

                                StringBuilder title = new StringBuilder();
                                for (int c = 0; c < 508; c += 4)
                                {
                                    int chartitle = int.Parse(data.Substring(c, 2), System.Globalization.NumberStyles.HexNumber);
                                    if (chartitle > 0 && chartitle != chrendnotefilename && chartitle != chrstartnotefilename && chartitle != chrstartdoc)
                                    {
                                        title.Append((char)chartitle);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                notetitles.Add(title.ToString());
                            }
                            else if (line.StartsWith("rel_position="))
                            {
                                int poseq = line.IndexOf('=') + 1;
                                string positionenc = line.Substring(poseq, line.Length - poseq);
                                Log.Write(LogType.info, "pnote rel_position=" + positionenc);
                                // TODO figure out how to get the position of the pnote, pnotes sourcecode:
                                ////sz = GetScreenMetrics();
                                ////save current relational position
                                ////nrp.left = (double)rcNote.left / (double)sz.cx;
                                ////nrp.top = (double)rcNote.top / (double)sz.cy;
                                ////nrp.width = rcNote.right - rcNote.left;
                                ////nrp.height = rcNote.bottom - rcNote.top;
                                ////WritePrivateProfileStructW(pNote->pFlags->id, IK_RELPOSITION, &nrp, sizeof(nrp), g_NotePaths.DataFile);
                                ////double d = getDouble(0xAAB1726AAC9CDA3F);
                                ////MessageBox.Show("test: "+d);
                                ////double test600 = DoubleFromHexString("AAB1726AAC9CDA3F");
                                ////MessageBox.Show("result = " + test600); // 600? / 0,439238?
                                ////double test500 = DoubleFromHexString("64DF04D93741D63F");
                                ////MessageBox.Show("result = " + test500); // 500? / 0,36603?
                                ////string hex = "AAB1726AAC9CDA3F";
                                ////byte[] b = new byte[hex.Length / 2];
                                ////for (int i = (hex.Length - 2), j = 0; i >= 0; i -= 2, j++)
                                ////{
                                ////    b[j] = byte.Parse(hex.Substring(i, 2),
                                ////    System.Globalization.NumberStyles.HexNumber);
                                ////}
                                ////double d = BitConverter.ToDouble(b, 0);
                                ////MessageBox.Show("result: "+d);    
                            }
                            else if (line.StartsWith("creation="))
                            {
                                int poseq = line.IndexOf('=') + 1;
                                string creationenc = line.Substring(poseq, line.Length - poseq);
                                Log.Write(LogType.info, "pnote creation=" + creationenc);

                                int year = int.Parse(creationenc.Substring(2, 2) + creationenc.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                                int month = int.Parse(creationenc.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
                                int dd = int.Parse(creationenc.Substring(12, 2), System.Globalization.NumberStyles.HexNumber);
                                int hh = int.Parse(creationenc.Substring(16, 2), System.Globalization.NumberStyles.HexNumber);
                                int mm = int.Parse(creationenc.Substring(20, 2), System.Globalization.NumberStyles.HexNumber);
                                Log.Write(LogType.info, "Cannot use pnote creation properie: " + year + "-" + month + "-" + dd + " " + hh + ":" + mm);
                            }
                            else if (line.Contains(chrstartnotefilename.ToString()))
                            {
                                notecontents = true;
                                int pos = line.IndexOf(chrendnotefilename) + 1;
                                notecontent.AppendLine(line.Substring(pos, line.Length - pos));
                            }
                        }
                        else
                        {
                            if (line.Contains("\0"))
                            {
                                this.notes.AddNoteDefaultSettings(notetitles[notenr], Settings.NotesDefaultSkinnr, 10, 10, DEFAULTIMPORTNOTEWIDTH, DEFAULTIMPORTNOTEHEIGHT, notecontent.ToString(), true);

                                notecontent = null;
                                notecontent = new StringBuilder();
                                if (line.Contains(chrendnotefilename.ToString()))
                                {
                                    int posendfilename = line.IndexOf(chrendnotefilename) + 1;
                                    notecontent.AppendLine(line.Substring(posendfilename, line.Length - posendfilename));
                                }

                                notenr++;
                            }
                            else
                            {
                                notecontent.AppendLine(line);
                            }
                        }
                    }
                }
                else
                {
                    string managenotes_errorreadpnotesbackup = Strings.T("Error reading PNotes backup file, incorrect format.");
                    Log.Write(LogType.error, managenotes_errorreadpnotesbackup);
                    MessageBox.Show(managenotes_errorreadpnotesbackup);                    
                    return;
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            Log.Write(LogType.info, "Imported PNotes full backup file: " + this.openImportFileDialog.FileName);
        }

        /// <summary>
        /// Read CintaNotes xml file exported notes.
        /// </summary>
        /// <param name="file">The CintaNotes exported notes xml full file path.</param>
        private void ReadCintaNotesXMLFile(string file)
        {
            XmlTextReader reader = null;
            try
            {
                reader = new XmlTextReader(file);
                while (reader.Read())
                {
                    if (reader.Name == "note")
                    {
                        string title = reader.GetAttribute("title");                        
                        string content = reader.ReadInnerXml();
                        int posstartcontent = content.IndexOf("<![CDATA[") + 9;
                        int posendcontent = content.IndexOf("]]>");                       
                        string plaincontent = content.Substring(posstartcontent, posendcontent - posstartcontent);
                        string notecontent = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1043{\\fonttbl{\\f0\\fnil\\fcharset0 Verdana;}}{\\*\\generator Msftedit 5.41.21.2510;}\\viewkind4\\uc1\\pard\\f0\\fs20" + plaincontent + "\\par}";
                        this.notes.AddNoteDefaultSettings(title, Settings.NotesDefaultSkinnr, 10, 10, DEFAULTIMPORTNOTEWIDTH, DEFAULTIMPORTNOTEHEIGHT, notecontent, true);
                    }
                }
            }
            catch (InvalidOperationException invopexc)
            {
                Log.Write(LogType.exception, invopexc.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// decode stickies title from UTF32 to UTF8
        /// </summary>
        /// <param name="title_enc">title encoded as UTF-32</param>
        /// <returns>title string as UTF-8</returns>
        private string decode_title(string title_enc)
        {
            StringBuilder title = new StringBuilder();
            for (int i = 0; i < title_enc.Length; i += 4)
            {
                string strchar = title_enc.Substring(i, 4);
                int charcode = int.Parse(strchar, System.Globalization.NumberStyles.HexNumber);
                title.Append(char.ConvertFromUtf32(charcode));
            }

            return title.ToString();
        }

        /// <summary>
        /// The create a title encoded.
        /// </summary>
        /// <param name="title">The title of the note encoded</param>
        /// <returns>The title encoded as hexdecimal</returns>
        private string encode_title(string title)
        {
            StringBuilder title_enc = new StringBuilder();
            for (int i = 0; i < title.Length; i++)
            {
                int c = char.ConvertToUtf32(title, i);
                string hexchar = c.ToString("X");
                if (hexchar.Length < 4)
                {
                    while (hexchar.Length < 4)
                    {
                        hexchar = hexchar.Insert(0, "0");
                    }
                }

                title_enc.Append(hexchar);
            }

            return title_enc.ToString();
        }

        /// <summary>
        /// Removes the quote from the begining and the end of the orgstring.
        /// </summary>
        /// <param name="orgstring">The orginal string with quotes</param>
        /// <returns>A string without quotes.</returns>
        private string RemoveQuotes(string orgstring)
        {
            orgstring = orgstring.Remove(0, 1);
            return orgstring.Remove(orgstring.Length - 1, 1);
        }

        /// <summary>
        /// Toggle visibility selected notes.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnShowSelectedNotes_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selectedrows = this.dataGridViewNotes.SelectedRows;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                foreach (DataGridViewRow selrow in selectedrows)
                {
                    int notepos = this.GetNoteposBySelrow(selrow.Index);
                    if (notepos >= 0)
                    {
                        selrow.Cells["visible"].Value = !this.notes.GetNote(notepos).Visible;
                        this.notes.GetNote(notepos).Visible = !this.notes.GetNote(notepos).Visible;
                        if (this.notes.GetNote(notepos).Visible)
                        {
                            this.notes.GetNote(notepos).CreateForm();
                            this.btnShowSelectedNotes.Text = Strings.T("&hide selected");
                        }
                        else
                        {
                            this.notes.GetNote(notepos).DestroyForm();
                            this.btnShowSelectedNotes.Text = Strings.T("&show selected");
                        }

                        this.Resetdatagrid();
                        Application.DoEvents();
                        xmlUtil.WriteNote(this.notes.GetNote(notepos), this.notes.GetSkinName(this.notes.GetNote(notepos).SkinNr), this.notes.GetNote(notepos).GetContent());
                    }
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

            this.notes.FrmManageNotesNeedUpdate = false;
        }

        /// <summary>
        /// Cell clicked in dataGridViewNotes, set hide/show note button text.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">DataGridViewCell event arguments</param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if ((bool)this.dataGridViewNotes.Rows[e.RowIndex].Cells["visible"].Value == true)
                {
                    this.btnShowSelectedNotes.Text = Strings.T("&hide selected");
                }
                else
                {
                    this.btnShowSelectedNotes.Text = Strings.T("&show selected");
                }

                if (e.ColumnIndex == 2)
                {
                    this.ToggleVisibilityNote(e.RowIndex);
                }
            }
        }

        /// <summary>
        /// Toggle the visibility of a note.
        /// </summary>
        /// <param name="row">The row number in dataGridView1 that is double clicked.</param>
        private void ToggleVisibilityNote(int row)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.TopMost = true;
                int notepos = this.GetNoteposBySelrow(row);
                if (notepos >= 0)
                {
                    this.notes.GetNote(notepos).Visible = !this.notes.GetNote(notepos).Visible;
                    this.dataGridViewNotes.Rows[row].Cells[2].Value = !(bool)this.dataGridViewNotes.Rows[row].Cells[2].Value;
                    if (this.notes.GetNote(notepos).Visible)
                    {
                        this.notes.GetNote(notepos).CreateForm();
                    }
                    else
                    {
                        this.notes.GetNote(notepos).DestroyForm();
                    }

                    xmlUtil.WriteNote(this.notes.GetNote(notepos), this.notes.GetSkinName(this.notes.GetNote(notepos).SkinNr), this.notes.GetNote(notepos).GetContent());
                }
            }
            catch (ArgumentOutOfRangeException argoutrangeexc)
            {
                Log.Write(LogType.exception, argoutrangeexc.Message);
            }
            finally
            {
                this.TopMost = false;
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// A column is sorted, make sure backgroundcolor skin colum get painted again.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">DataGridViewCellMouse event arguments</param>
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.Resetdatagrid();
            this.notes.FrmManageNotesNeedUpdate = true;
            this.dataGridViewNotes.Refresh();
        }

        /// <summary>
        /// Color the skin cell with the foreground color of the skin in this cell.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">DataGridViewRowPostPaint event arguments</param>
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            if (this.notes.FrmManageNotesNeedUpdate)
            {
                // detect and update add/delete notes, then redraw all notes
                if (this.dataGridViewNotes.RowCount != this.notes.CountNotes && !this.searchTextBoxNotes.IsKeywordEntered)
                { 
                    this.DrawNotesGrid();
                    this.SetDataGridViewColumsWidth();
                }

                int notepos = this.GetNoteposBySelrow(e.RowIndex);
                if (notepos >= 0)
                {
                    this.dataGridViewNotes.Rows[e.RowIndex].Cells["title"].Value = this.notes.GetNote(notepos).Title;
                    this.dataGridViewNotes.Rows[e.RowIndex].Cells["skin"].Style.BackColor = this.notes.GetPrimaryClr(this.notes.GetNote(notepos).SkinNr);
                    this.dataGridViewNotes.Rows[e.RowIndex].Cells["skin"].Style.ForeColor = this.notes.GetTextClr(this.notes.GetNote(notepos).SkinNr);
                    if (this.dataGridViewNotes.Rows[e.RowIndex].Cells["skin"].Value.ToString() != this.notes.GetSkinName(this.notes.GetNote(notepos).SkinNr))
                    {
                        this.dataGridViewNotes.Rows[e.RowIndex].Cells["skin"].Value = this.notes.GetSkinName(this.notes.GetNote(notepos).SkinNr);
                    }

                    this.dataGridViewNotes.Rows[e.RowIndex].Cells["visible"].Value = this.notes.GetNote(notepos).Visible;

                    if (e.RowIndex == this.dataGridViewNotes.RowCount - 1)
                    {
                        this.notes.FrmManageNotesNeedUpdate = false;
                    }
                    else if (this.prevrownr < this.secondprevrownr)
                    {
                        this.notes.FrmManageNotesNeedUpdate = false;
                    }
                    else
                    {
                        this.secondprevrownr = this.prevrownr;
                        this.prevrownr = e.RowIndex;
                    }
                }
            }
        }

        /// <summary>
        /// Scrolling the datagridview.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Scroll event arguments</param>
        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                this.notes.FrmManageNotesNeedUpdate = true;
                Application.DoEvents();
            }
        }

#if windows
        /// <summary>
        /// Possible flags for the SHFileOperation method.
        /// </summary>
        [Flags]
        public enum FileOperationFlags : ushort
        {
            /// <summary>
            /// Do not show a dialog during the process
            /// </summary>
            FOF_SILENT = 0x0004,
            /// <summary>
            /// Do not ask the user to confirm selection
            /// </summary>
            FOF_NOCONFIRMATION = 0x0010,
            /// <summary>
            /// Delete the file to the recycle bin.  (Required flag to send a file to the bin
            /// </summary>
            FOF_ALLOWUNDO = 0x0040,
            /// <summary>
            /// Do not show the names of the files or folders that are being recycled.
            /// </summary>
            FOF_SIMPLEPROGRESS = 0x0100,
            /// <summary>
            /// Surpress errors, if any occur during the process.
            /// </summary>
            FOF_NOERRORUI = 0x0400,
            /// <summary>
            /// Warn if files are too big to fit in the recycle bin and will need
            /// to be deleted completely.
            /// </summary>
            FOF_WANTNUKEWARNING = 0x4000,
        }

        /// <summary>
        /// File Operation Function Type for SHFileOperation
        /// </summary>
        public enum FileOperationType : uint
        {
            /// <summary>
            /// Move the objects
            /// </summary>
            FO_MOVE = 0x0001,
            /// <summary>
            /// Copy the objects
            /// </summary>
            FO_COPY = 0x0002,
            /// <summary>
            /// Delete (or recycle) the objects
            /// </summary>
            FO_DELETE = 0x0003,
            /// <summary>
            /// Rename the object(s)
            /// </summary>
            FO_RENAME = 0x0004,
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
        private struct SHFILEOPSTRUCT_x86
        {
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.U4)]
            public FileOperationType wFunc;
            public string pFrom;
            public string pTo;
            public FileOperationFlags fFlags;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fAnyOperationsAborted;
            public IntPtr hNameMappings;
            public string lpszProgressTitle;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct SHFILEOPSTRUCT_x64
        {
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.U4)]
            public FileOperationType wFunc;
            public string pFrom;
            public string pTo;
            public FileOperationFlags fFlags;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fAnyOperationsAborted;
            public IntPtr hNameMappings;
            public string lpszProgressTitle;
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto, EntryPoint = "SHFileOperation")]
        private static extern int SHFileOperation_x86(ref SHFILEOPSTRUCT_x86 FileOp);

        [DllImport("shell32.dll", CharSet = CharSet.Auto, EntryPoint = "SHFileOperation")]
        private static extern int SHFileOperation_x64(ref SHFILEOPSTRUCT_x64 FileOp);

        private static bool IsWOW64Process()
        {
            return IntPtr.Size == 8;
        }
#endif

        /// <summary>
        /// Deletes the notes in memory and the files that are selected in a Gridview.
        /// In reverse order, so updating datagridview goes well.
        /// </summary>
        /// <param name="selrows">The selected rows in datagridview1</param>
        private void DeleteNotesSelectedRowsGrid(DataGridViewSelectedRowCollection selrows)
        {
            List<int> deletenotepos = new List<int>();
            for (int i = 0; i < selrows.Count; i++)
            {
                int notepos = this.GetNoteposBySelrow(selrows[i].Index);
                if (notepos >= 0)
                {
                    deletenotepos.Add(notepos);
                }
            }

            deletenotepos.Sort();

            for (int r = deletenotepos.Count - 1; r >= 0; r--)
            {
                string filename = this.notes.GetNote(deletenotepos[r]).Filename;
                try
                {
                    this.notes.GetNote(deletenotepos[r]).DestroyForm();
                    string filepath = Path.Combine(Settings.NotesSavepath, filename);
                    if (Settings.NotesDeleteRecyclebin)
                    {
                        // On older FAT file systems (typically Windows 98 and prior), it is located in Drive:\RECYCLED.
                        // In the NTFS filesystem (Windows 2000, XP, NT) it is Drive:\RECYCLER. 
                        // On Windows Vista and Windows 7 it is Drive:\$Recycle.Bin folder.
                        // The actual location of the Recycle Bin is in another hidden folder, also at the system root, called RECYCLER
#if windows
                        if (IsWOW64Process())
                        {
                            SHFILEOPSTRUCT_x64 fs = new SHFILEOPSTRUCT_x64();
                            fs.wFunc = FileOperationType.FO_DELETE;
                            // important to double-terminate the string.
                            fs.pFrom = filepath + '\0' + '\0';
                            fs.fFlags = FileOperationFlags.FOF_ALLOWUNDO | FileOperationFlags.FOF_NOCONFIRMATION | FileOperationFlags.FOF_WANTNUKEWARNING;
                            SHFileOperation_x64(ref fs);
                        }
                        else
                        {
                            SHFILEOPSTRUCT_x86 fs = new SHFILEOPSTRUCT_x86();
                            fs.wFunc = FileOperationType.FO_DELETE;
                            // important to double-terminate the string.
                            fs.pFrom = filepath + '\0' + '\0';
                            fs.fFlags = FileOperationFlags.FOF_ALLOWUNDO | FileOperationFlags.FOF_NOCONFIRMATION | FileOperationFlags.FOF_WANTNUKEWARNING;
                            SHFileOperation_x86(ref fs);
                        }
#elif linux
                        // trashfolder = ~/.local/share/Trash/files
                        string trashfolder = System.Environment.GetEnvironmentVariable("HOME") +"/.local/share/Trash/files/";
                        if (!Directory.Exists(trashfolder))
                        {
                            Directory.CreateDirectory(trashfolder);
                        }
                        
                        File.Move(filepath, Path.Combine(trashfolder, filename));
#endif
                        if (!File.Exists(filepath))
                        {
                            Log.Write(LogType.info, "Moved note to Recyclebin: " + filepath);
                        }
                        else
                        {
                            Log.Write(LogType.exception, "Could not move note to recyclebin. Try renaming with appending .old to note filename.");
                            if (!File.Exists(filepath + ".old"))
                            {
                                File.Move(filepath, filepath + ".old");                                
                            }
                        }
                        
                    }
                    else
                    {
                        File.Delete(filepath);
                        Log.Write(LogType.info, "Deleted note: " + filepath);
                    }

                    this.notes.RemoveNote(deletenotepos[r]);
                }
                catch (FileNotFoundException filenotfoundexc)
                {
                    throw new ApplicationException(filenotfoundexc.Message);
                }
                catch (UnauthorizedAccessException)
                {
                    string managenotes_msgaccessdenied = Strings.T("Access denied. delete note {0} manually with proper premission.", filename);
                    Log.Write(LogType.error, managenotes_msgaccessdenied);
                    MessageBox.Show(managenotes_msgaccessdenied);
                }
            }

            this.Resetdatagrid();
            this.DrawNotesGrid();
            GC.Collect();
        }

        /// <summary>
        /// Draw a list of all notes.
        /// Sets FrmManageNotesNeedUpdate to true.
        /// </summary>
        private void DrawNotesGrid()
        {
            int vertscrolloffset = this.dataGridViewNotes.VerticalScrollingOffset;
            this.Resetdatagrid();
            this.notes.FrmManageNotesNeedUpdate = true;
            this.toolTip.Active = Settings.NotesTooltipsEnabled;
            if (!this.searchTextBoxNotes.IsKeywordEntered)
            {
                DataTable datatable = this.CreateDatatable();
                for (int i = 0; i < this.notes.CountNotes; i++)
                {
                    datatable = this.AddDatatableNoteRow(datatable, i);
                }
            }

            // VerticalScrollingOffset is readonly, so we need a bit of 'hacking' to set it.
            if (vertscrolloffset > 0)
            {
                System.Reflection.PropertyInfo verticalOffset = this.dataGridViewNotes.GetType().GetProperty("VerticalOffset", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                verticalOffset.SetValue(this.dataGridViewNotes, vertscrolloffset, null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DataTable CreateDatatable()
        {
            DataTable datatable = new DataTable();
            this.dataGridViewNotes.DataSource = datatable;
            datatable.Columns.Add(Strings.T("nr"), typeof(string)); // col 0
            datatable.Columns[0].AutoIncrement = true;
            datatable.Columns[0].Unique = true;
            datatable.Columns.Add(Strings.T("title"), typeof(string)); // col 1
            datatable.Columns.Add(Strings.T("visible"), typeof(bool)); // col 2
            datatable.Columns.Add(Strings.T("skin"), typeof(string)); // col 3
            datatable.DefaultView.AllowEdit = true;
            datatable.DefaultView.AllowNew = false;
            if (this.dataGridViewNotes.Columns[0] != null)
            {
                this.dataGridViewNotes.Columns[0].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (this.dataGridViewNotes.Columns[2] != null)
            {
                this.dataGridViewNotes.Columns[2].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            this.dataGridViewNotes.Font = new Font(Settings.ManagenotesFontFamily, Settings.ManagenotesFontsize);
            this.dataGridViewNotes.RowPostPaint += new DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            return datatable;
        }

        /// <summary>
        /// Add a row with note information to a databasetable
        /// </summary>
        /// <param name="datatable"></param>
        /// <param name="notepos"></param>
        /// <returns>The databasetable with a extra row</returns>
        private DataTable AddDatatableNoteRow(DataTable datatable, int notepos)
        {
            DataRow dr = datatable.NewRow();
            dr[0] = notepos + 1; // enduser numbering
            dr[1] = this.notes.GetNote(notepos).Title;
            dr[2] = this.notes.GetNote(notepos).Visible;
            dr[3] = this.notes.GetSkinName(this.notes.GetNote(notepos).SkinNr);
            datatable.Rows.Add(dr);
            return datatable;
        }

        /// <summary>
        /// FrmManageNotes is activated.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void frmManageNotes_Activated(object sender, EventArgs e)
        {
            if (Settings.NotesTransparencyEnabled)
            {
                try
                {
                    this.Opacity = 1.0;
                }
                catch (InvalidCastException)
                {
                    string managenotes_invalidtransparencylvl = Strings.T("Transparency level not a integer or double.");
                    throw new ApplicationException(managenotes_invalidtransparencylvl);
                }
            }
        }

        /// <summary>
        /// form not active, make tranparent if set.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void frmManageNotes_Deactivate(object sender, EventArgs e)
        {
            if (Settings.NotesTransparencyEnabled)
            {
                try
                {
                    this.Opacity = Settings.NotesTransparencyLevel;
                }
                catch (InvalidCastException)
                {
                    string managenotes_invalidtransparencylvl = Strings.T("Transparency level not a integer or double.");
                    throw new ApplicationException(managenotes_invalidtransparencylvl);
                }
            }
        }

        /// <summary>
        /// Get the note position in the list by looking up the nr colom with at the partialer row.
        /// </summary>
        /// <param name="rowindex">The selected row index in datagridview1.</param>
        /// <returns>The position of the note in the list. -1 if error.</returns>
        private int GetNoteposBySelrow(int rowindex)
        {
            if (rowindex >= 0)
            {
                return Convert.ToInt32(this.dataGridViewNotes.Rows[rowindex].Cells[0].Value) - 1; // col: nr
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// The manage note form is beening resized.
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
        /// Resize ended, set column width
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void pbResizeGrip_MouseUp(object sender, MouseEventArgs e)
        {
            this.SetDataGridViewColumsWidth();
            this.DrawNotesGrid();
            this.SetDataGridViewColumsWidth();
        }

        /// <summary>
        /// Moving frmManageNotes
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void pnlHead_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.pnlHead.BackColor = this.notes.GetSelectClr(Settings.ManagenotesSkinnr);
                this.oldp = e.Location;
            }
        }

        /// <summary>
        /// Move FrmManageNotes if pnlHead is being left clicked.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Mouse event arguments</param>
        private void pnlHead_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int dpx = e.Location.X - this.oldp.X;
                int dpy = e.Location.Y - this.oldp.Y;
#if linux
                // limit the moving of this note under mono/linux so this note cannot move uncontrolled a lot.
                const int movelimit = 8;
                if (dpx > movelimit)
                {
                    dpx = movelimit;
                }
                else if (dpx < -movelimit)
                {
                    dpx = -movelimit;
                }

                if (dpy > movelimit)
                {
                    dpy = movelimit;
                }
                else if (dpy < -movelimit)
                {
                    dpy = -movelimit;
                }
#endif
                this.Location = new Point(this.Location.X + dpx, this.Location.Y + dpy);
            }
        }

        /// <summary>
        /// End moving FrmManageNotes.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Mouse event arguments</param>
        private void pnlHead_MouseUp(object sender, MouseEventArgs e)
        {
            this.pnlHead.BackColor = this.notes.GetPrimaryClr(Settings.ManagenotesSkinnr);
            if (this.BackgroundImage != null)
            {
                this.pnlHead.BackColor = Color.Transparent;
            }
        }

        /// <summary>
        /// Sets every colom of the datagridview to a reasonable width.
        /// </summary>
        private void SetDataGridViewColumsWidth()
        {
            if ((this.dataGridViewNotes.Width <= 0) || (this.dataGridViewNotes == null))
            {
                return;
            }

            int partunit = (this.dataGridViewNotes.Width - COLNOTENRFIXEDWIDTH) / 10;
            if (this.dataGridViewNotes.Columns[0] != null)
            {
                this.dataGridViewNotes.Columns[0].Width = 1 * COLNOTENRFIXEDWIDTH;
            }

            if (this.dataGridViewNotes.Columns[1] != null)
            {
                this.dataGridViewNotes.Columns[1].Width = 6 * partunit;
            }

            if (this.dataGridViewNotes.Columns[2] != null)
            {
                this.dataGridViewNotes.Columns[2].Width = 1 * partunit;
            }

            if (this.dataGridViewNotes.Columns[3] != null)
            {
                this.dataGridViewNotes.Columns[3].Width = 3 * partunit;
            }
        }

        /// <summary>
        /// Double click a row in dataGridView toggle the visibility of a note.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">DataGridViewCellEvent Arguments</param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.ToggleVisibilityNote(e.RowIndex);
        }

        /// <summary>
        /// Display a tooltip with the note content for the hovered note in that row.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridViewNotes_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (Settings.NotesTooltipsEnabled && Settings.ManagenotesTooltip) 
            {
                if (e.ColumnIndex == 1)
                {
                    string contentpreview = null;
                    if (e.RowIndex >= 0)
                    {
                        int notepos = this.GetNoteposBySelrow(e.RowIndex);
                        if (notepos > 0)
                        {
                            // todo GetContent() is wrong for this but works for now. Add GetContentPreview() that has limited disk read.
                            string content = this.notes.GetNote(notepos).GetContent();
                            const string startcontentplainhint = @"\fs24 ";
                            int startpos = content.IndexOf(startcontentplainhint);

                            for (int i = content.Length - 1; i > startpos; i--)
                            {
                                if (content[i] == '\\')
                                {
                                    for (int p = 0; p < 20; p++)
                                    {
                                        int endpos = i + p;
                                        if (endpos >= content.Length - 1)
                                        {
                                            break;
                                        }

                                        // || content[endpos] == '\r' || content[endpos] == '\n'
                                        if (content[endpos] == ' ')
                                        {
                                            content.Remove(i, p);
                                        }
                                    }
                                }
                            }

                            try
                            {
                                int lencontentpreview = content.Length - startpos - startcontentplainhint.Length;
                                const int MAXLENCONTENTPREVIEW = 100;
                                if (lencontentpreview > MAXLENCONTENTPREVIEW)
                                {
                                    contentpreview = content.Substring(startpos + startcontentplainhint.Length, MAXLENCONTENTPREVIEW);
                                    contentpreview += "..";
                                }
                                else
                                {
                                    contentpreview = content.Substring(startpos + startcontentplainhint.Length, lencontentpreview); 
                                }

                                content = null;
                            }
                            catch (ArgumentOutOfRangeException argoutrange)
                            {
                                throw new ApplicationException(argoutrange.Message);
                            }

                            int tooltiplocx = Cursor.Position.X - this.Location.X;
                            int tooltiplocy = Cursor.Position.Y - this.Location.Y;
                            if (!String.IsNullOrEmpty(contentpreview))
                            {
                                this.toolTip.InitialDelay = 200;
                                this.toolTip.Show(contentpreview, this, new Point(tooltiplocx, tooltiplocy), 2000);                                
                            }
                        }
                    }
                }
                else
                {
                    this.toolTip.Hide(this);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keywords"></param>
        private void searchTextBoxNotes_SearchStart(string keywords)
        {
            this.Resetdatagrid();
            DataTable dt = this.CreateDatatable();
            for (int i = 0; i < this.notes.CountNotes; i++)
            {
                string title = this.notes.GetNote(i).Title;
                if (!Settings.ManagenotesSearchCasesentive)
                {
                    title = this.notes.GetNote(i).Title.ToLowerInvariant();
                    keywords = keywords.ToLowerInvariant();
                }
                
                if (title.Contains(keywords))
                {
                    this.AddDatatableNoteRow(dt, i);
                }
            }

            this.notes.FrmManageNotesNeedUpdate = true;
            this.dataGridViewNotes.DataSource = dt;
            this.SetDataGridViewColumsWidth();
            
        }
        
        /// <summary>
        /// 
        /// </summary>
        private void searchTextBoxNotes_SearchStop()
        { 
            this.DrawNotesGrid(); 
            this.SetDataGridViewColumsWidth();
        }

        #endregion Methods
    }
}