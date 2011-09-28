//-----------------------------------------------------------------------
// <copyright file="FrmManageNotes.cs" company="GNU">
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
    using System.Collections.Generic;
    using System.Data;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// Manage notes window
    /// </summary>
    public partial class FrmManageNotes : Form
    {
        #region Fields (6)

#if windows
        /// <summary>
        /// Delete the files specified in pFrom.
        /// </summary>
        private const int FO_DELETE = 3;

        /// <summary>
        /// Preserve Undo information, if possible.
        /// If pFrom does not contain fully qualified path and file names, this flag is ignored.
        /// </summary>
        private const int FOF_ALLOWUNDO = 0x40;

        /// <summary>
        /// Respond with "Yes to All" for any dialog box that is displayed.
        /// </summary>
        private const int FOF_NOCONFIRMATION = 0x10;
#endif

        /// <summary>
        /// Constant for btntoggleshownote 
        /// </summary>
        private const string BTNPRETEXTHIDENOTE = "&hide" + BTNSUFTEXTHIDENOTE;

        /// <summary>
        /// Constant for btntoggleshownote
        /// </summary>
        private const string BTNPRETEXTSHOWNOTE = "&show" + BTNSUFTEXTHIDENOTE;

        /// <summary>
        /// Constant for constructing BTNPRETEXTHIDENOTE and BTNPRETEXTSHOWNOTE constant 
        /// </summary>
        private const string BTNSUFTEXTHIDENOTE = " selected";

        /// <summary>
        /// Constant for the fixed width of the number note colum in datagridview1.
        /// </summary>
        private const int COLNOTENRFIXEDWIDTH = 30;

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
            this.InitializeComponent();
            this.notes = notes;
            this.DrawNotesGrid();
            this.SetDataGridViewColumsWidth();
            if (this.dataGridView1.RowCount > 0)
            {
                if ((bool)this.dataGridView1.Rows[0].Cells["visible"].Value == true)
                {
                    this.btnShowSelectedNotes.Text = BTNPRETEXTHIDENOTE;
                }
                else
                {
                    this.btnShowSelectedNotes.Text = BTNPRETEXTSHOWNOTE;
                }
            }
            else
            {
                this.btnShowSelectedNotes.Text = BTNPRETEXTSHOWNOTE;
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

#if windows
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern int SHFileOperation(ref SHFILEOPSTRUCT FileOp);
#endif

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
                    TimeSpan ts = (notefile.CreationTime - new DateTime(1970, 1, 1, 0, 0, 0));
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
                    // FIXME data= seems to be ingored and datetime is choicen as title, is it done wrong?
                    // writing title, 
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

                    // TODO datetime last change and what more?
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
            if (this.dataGridView1.SelectedRows.Count <= 0)
            {
                const string NOTHINGSELECTED = "Nothing selected.";
                Log.Write(LogType.info, NOTHINGSELECTED);
                MessageBox.Show(NOTHINGSELECTED);
            }
            else
            {
                if (Settings.ConfirmDeletenote)
                {
                    const string DELETESELECTEDNOTES = "Are you sure you want to delete the selected note(s)?";
                    DialogResult deleteres = MessageBox.Show(DELETESELECTEDNOTES, "delete?", MessageBoxButtons.YesNo);
                    if (deleteres == DialogResult.Yes)
                    {
                        this.DeleteNotesSelectedRowsGrid(this.dataGridView1.SelectedRows);
                    }
                }
                else
                {
                    this.DeleteNotesSelectedRowsGrid(this.dataGridView1.SelectedRows);
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
                if (this.openImportFileDialog.FilterIndex == 1)
                {
                    this.ReadNoteFlyBackupFile(this.openImportFileDialog.FileName);
                }
                else if (this.openImportFileDialog.FilterIndex == 2)
                {
                    this.ReadStickiesCSVFile(this.openImportFileDialog.FileName);
                }
                else if (this.openImportFileDialog.FilterIndex == 3)
                {
                    this.ReadPNotesBackupFile(this.openImportFileDialog.FileName);
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
                const string DELETEALLCURRENTNOTES = "Do you want to delete all current notes?";
                DialogResult eraseres = MessageBox.Show(DELETEALLCURRENTNOTES, "Are you sure?", MessageBoxButtons.YesNoCancel);
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
                            string filenamenote = this.notes.GetNoteFilename(title);
                            Note newnote = new Note(this.notes, filenamenote);
                            newnote.Visible = false;
                            newnote.Locked = false;
                            newnote.Ontop = false;
                            newnote.RolledUp = false;
                            newnote.Height = 200;
                            newnote.Width = width;
                            newnote.X = 10;
                            newnote.Y = 10;
                            newnote.Title = title;
                            newnote.Tempcontent = content;
                            string skinname = this.notes.GetSkinName(Settings.NotesDefaultSkinnr);
                            xmlUtil.WriteNote(newnote, skinname, content);
                            this.notes.AddNote(newnote);
                        }
                        else
                        {
                            const string NOTSTICKIES = "CVS file does not seems to be in the Stickies format.";
                            Log.Write(LogType.error, NOTSTICKIES);
                            MessageBox.Show(NOTSTICKIES);
                        }
                    }
                    else if (linenr != 1)
                    {
                        const string NOTSTICKIES = "CVS file does not seems to be in the Stickies format, excepting 5 columns.";
                        Log.Write(LogType.error, NOTSTICKIES);
                        MessageBox.Show(NOTSTICKIES);
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
                                // TODO
                            }
                            else if (line.StartsWith("creation="))
                            {                              
                                int poseq = line.IndexOf('=') + 1;
                                string enccreation = line.Substring(poseq, line.Length - poseq);
                                int yyyy = int.Parse(enccreation.Substring(2, 2) + enccreation.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                                int MM = int.Parse(enccreation.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
                                int dd = int.Parse(enccreation.Substring(12, 2), System.Globalization.NumberStyles.HexNumber);
                                int hh = int.Parse(enccreation.Substring(16, 2), System.Globalization.NumberStyles.HexNumber);
                                int mm = int.Parse(enccreation.Substring(20, 2), System.Globalization.NumberStyles.HexNumber);
                                Log.Write(LogType.info, "Imported PNote creation: " + yyyy + "-" + MM + "-" + dd + " " + hh + ":" + mm);
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
                                Note newnote = new Note(this.notes, this.notes.GetNoteFilename(notetitles[notenr]));
                                newnote.Title = notetitles[notenr];
                                newnote.X = 100;
                                newnote.Y = 100;
                                newnote.Width = 240;
                                newnote.Height = 200;
                                newnote.Ontop = false;
                                newnote.Visible = false;
                                newnote.RolledUp = false;
                                newnote.Tempcontent = notecontent.ToString();
                                xmlUtil.WriteNote(newnote, this.notes.GetSkinName(Settings.NotesDefaultSkinnr), notecontent.ToString());
                                this.notes.AddNote(newnote);
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
                    const string ERRORREADPNOTESBACKUP = "Error reading PNotes backup file, incorrect format.";
                    MessageBox.Show(ERRORREADPNOTESBACKUP);
                    Log.Write(LogType.error, ERRORREADPNOTESBACKUP);
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
        /// <returns></returns>
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
            DataGridViewSelectedRowCollection selectedrows = this.dataGridView1.SelectedRows;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                foreach (DataGridViewRow selrow in selectedrows)
                {
                    int notepos = this.GetNoteposBySelrow(selrow.Index);
                    selrow.Cells["visible"].Value = !this.notes.GetNote(notepos).Visible;
                    this.notes.GetNote(notepos).Visible = !this.notes.GetNote(notepos).Visible;
                    if (this.notes.GetNote(notepos).Visible)
                    {
                        string tempcontent = this.notes.GetNote(notepos).GetContent();
                        if (tempcontent == string.Empty)
                        {
                            Log.Write(LogType.exception, "Note content is empty.");
                        }

                        this.notes.GetNote(notepos).Tempcontent = tempcontent;
                        this.notes.GetNote(notepos).CreateForm(true);
                        this.btnShowSelectedNotes.Text = BTNPRETEXTHIDENOTE;
                    }
                    else
                    {
                        this.notes.GetNote(notepos).DestroyForm();
                        this.btnShowSelectedNotes.Text = BTNPRETEXTSHOWNOTE;
                    }

                    this.Resetdatagrid();
                    Application.DoEvents();
                    xmlUtil.WriteNote(this.notes.GetNote(notepos), this.notes.GetSkinName(this.notes.GetNote(notepos).SkinNr), this.notes.GetNote(notepos).GetContent());
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }

            this.notes.FrmManageNotesNeedUpdate = false;
        }

        /// <summary>
        /// Cell clicked in dataGridView1, set hide/show note button text.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">DataGridViewCell event arguments</param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if ((bool)this.dataGridView1.Rows[e.RowIndex].Cells["visible"].Value == true)
                {
                    this.btnShowSelectedNotes.Text = BTNPRETEXTHIDENOTE;
                }
                else
                {
                    this.btnShowSelectedNotes.Text = BTNPRETEXTSHOWNOTE;
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
        private void ToggleVisibilityNote(int row)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.TopMost = true;
                int notepos = this.GetNoteposBySelrow(row);
                this.notes.GetNote(notepos).Visible = !this.notes.GetNote(notepos).Visible;
                this.dataGridView1.Rows[row].Cells[2].Value = !(bool)this.dataGridView1.Rows[row].Cells[2].Value;
                if (this.notes.GetNote(notepos).Visible)
                {
                    this.notes.GetNote(notepos).CreateForm(true);
                }
                else
                {
                    this.notes.GetNote(notepos).DestroyForm();
                }

                xmlUtil.WriteNote(this.notes.GetNote(notepos), this.notes.GetSkinName(this.notes.GetNote(notepos).SkinNr), this.notes.GetNote(notepos).GetContent());
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
            this.dataGridView1.Refresh();
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
                // detect and update add/delete notes.
                if (this.dataGridView1.RowCount != this.notes.CountNotes)
                {
                    this.DrawNotesGrid();
                    this.SetDataGridViewColumsWidth();
                }

                int notepos = this.GetNoteposBySelrow(e.RowIndex);
                this.dataGridView1.Rows[e.RowIndex].Cells["title"].Value = this.notes.GetNote(notepos).Title;
                this.dataGridView1.Rows[e.RowIndex].Cells["skin"].Style.BackColor = this.notes.GetPrimaryClr(this.notes.GetNote(notepos).SkinNr);
                this.dataGridView1.Rows[e.RowIndex].Cells["skin"].Style.ForeColor = this.notes.GetTextClr(this.notes.GetNote(notepos).SkinNr);
                if (this.dataGridView1.Rows[e.RowIndex].Cells["skin"].Value.ToString() != this.notes.GetSkinName(this.notes.GetNote(notepos).SkinNr))
                {
                    this.dataGridView1.Rows[e.RowIndex].Cells["skin"].Value = this.notes.GetSkinName(this.notes.GetNote(notepos).SkinNr);
                }

                this.dataGridView1.Rows[e.RowIndex].Cells["visible"].Value = this.notes.GetNote(notepos).Visible;

                if (e.RowIndex == this.dataGridView1.RowCount - 1)
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
                deletenotepos.Add(this.GetNoteposBySelrow(selrows[i].Index));
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
#if windows
                        SHFILEOPSTRUCT shf = new SHFILEOPSTRUCT();
                        shf.wFunc = FO_DELETE;
                        shf.fFlags = FOF_ALLOWUNDO | FOF_NOCONFIRMATION;
                        shf.pFrom = filepath + "\0"; // double null terminated
                        SHFileOperation(ref shf);
#elif linux
                        // trashfolder = ~/.local/share/Trash/files
                        string trashfolder = System.Environment.GetEnvironmentVariable("HOME") +"/.local/share/Trash/files/";
                        if (!Directory.Exists(trashfolder))
                        {
                            Directory.CreateDirectory(trashfolder);
                        }

                        File.Move(filepath, Path.Combine(trashfolder, filename));
#endif
                        Log.Write(LogType.info, "Moved note to Recyclebin: " + filepath);
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
                    string msgaccessdenied = "Access denied. delete note " + filename + " manually with proper premission.";
                    Log.Write(LogType.error, msgaccessdenied);
                    MessageBox.Show(msgaccessdenied);
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
            this.Resetdatagrid();
            this.notes.FrmManageNotesNeedUpdate = true;
            this.toolTip.Active = Settings.NotesTooltipsEnabled;
            DataTable datatable = new DataTable();
            this.dataGridView1.DataSource = datatable;
            datatable.Columns.Add("nr", typeof(string));
            datatable.Columns["nr"].AutoIncrement = true;
            datatable.Columns["nr"].Unique = true;
            datatable.Columns.Add("title", typeof(string));
            datatable.Columns.Add("visible", typeof(bool));
            datatable.Columns.Add("skin", typeof(string));
            datatable.DefaultView.AllowEdit = true;
            datatable.DefaultView.AllowNew = false;
            if (this.dataGridView1.Columns["nr"] != null)
            {
                this.dataGridView1.Columns["nr"].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (this.dataGridView1.Columns["visible"] != null)
            {
                this.dataGridView1.Columns["visible"].CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            this.dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(this.dataGridView1_RowPostPaint);
            for (int i = 0; i < this.notes.CountNotes; i++)
            {
                DataRow dr = datatable.NewRow();
                dr[0] = i + 1; // enduser numbering
                dr[1] = this.notes.GetNote(i).Title;
                dr[2] = this.notes.GetNote(i).Visible;
                dr[3] = this.notes.GetSkinName(this.notes.GetNote(i).SkinNr);
                datatable.Rows.Add(dr);
            }
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
                    throw new ApplicationException("Transparency level not a integer or double.");
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
                    throw new ApplicationException("Transparency level not a integer or double.");
                }
            }
        }

        /// <summary>
        /// Get the note position in the list by looking up the nr colom with at the partialer row.
        /// </summary>
        /// <param name="rowindex">The selected row index in datagridview1.</param>
        /// <returns>The position of the note in the list.</returns>
        private int GetNoteposBySelrow(int rowindex)
        {
            if (rowindex >= 0)
            {
                return Convert.ToInt32(this.dataGridView1.Rows[rowindex].Cells["nr"].Value) - 1;
            }
            else
            {
                throw new ApplicationException("Negative rowindex.");
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
                this.pnlHead.BackColor = Color.OrangeRed;
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
                this.pnlHead.BackColor = Color.OrangeRed;

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
            else
            {
                this.pnlHead.BackColor = Color.Orange;
            }
        }

        /// <summary>
        /// End moving FrmManageNotes.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Mouse event arguments</param>
        private void pnlHead_MouseUp(object sender, MouseEventArgs e)
        {
            this.pnlHead.BackColor = Color.Orange;
        }

        /// <summary>
        /// Sets every colom of the datagridview to a reasonable width.
        /// </summary>
        private void SetDataGridViewColumsWidth()
        {
            if ((this.dataGridView1.Width <= 0) || (this.dataGridView1 == null))
            {
                return;
            }

            int partunit = (this.dataGridView1.Width - COLNOTENRFIXEDWIDTH) / 10;
            if (this.dataGridView1.Columns["nr"] != null)
            {
                this.dataGridView1.Columns["nr"].Width = 1 * COLNOTENRFIXEDWIDTH;
            }

            if (this.dataGridView1.Columns["title"] != null)
            {
                this.dataGridView1.Columns["title"].Width = 6 * partunit;
            }

            if (this.dataGridView1.Columns["visible"] != null)
            {
                this.dataGridView1.Columns["visible"].Width = 1 * partunit;
            }

            if (this.dataGridView1.Columns["skin"] != null)
            {
                this.dataGridView1.Columns["skin"].Width = 3 * partunit;
            }
        }

        /// <summary>
        /// Double click a row in dataGridView toggle the visibility of a note.
        /// </summary>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.ToggleVisibilityNote(e.RowIndex);
        }

#if windows
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]

        /// <summary>
        /// Windows SHFILEOPSTRUCT struct
        /// </summary>
        public struct SHFILEOPSTRUCT
        {
            /// <summary>
            /// A window handle to the dialog box to display information about the status of the file operation.(MSDN)
            /// </summary>
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.U4)]

            /// <summary>
            /// A value that indicates which operation to perform. One of the following values.(MSDN)
            /// </summary>
            public int wFunc;

            /// <summary>
            /// A pointer to one or more source file names. These names should be fully-qualified paths to prevent unexpected results.
            /// </summary>
            public string pFrom;

            /// <summary>
            /// A pointer to the destination file or directory name.
            /// </summary>
            public string pTo;

            /// <summary>
            /// Flags that control the file operation.
            /// </summary>
            public short fFlags;

            [MarshalAs(UnmanagedType.Bool)]

            /// <summary>
            /// When the function returns, this member contains TRUE if any file operations were aborted before they were completed; otherwise, FALSE.
            /// </summary>
            public bool fAnyOperationsAborted;

            /// <summary>
            /// When the function returns, this member contains a handle to a name mapping object that contains the old and new names of the renamed files.
            /// </summary>
            public IntPtr hNameMappings;

            /// <summary>
            /// A pointer to the title of a progress dialog box.
            /// </summary>
            public string lpszProgressTitle;
        }

#endif

        #endregion Methods
    }
}
