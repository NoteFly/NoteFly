//-----------------------------------------------------------------------
// <copyright file="ExportNotes.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2012  Tom
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
    using System.Text;
    using System.Xml;
    using System.IO;
    using System.Drawing;
    using System.Globalization;

    /// <summary>
    /// 
    /// </summary>
    public class ExportNotes
    {
        /// <summary>
        /// Reference to notes class.
        /// </summary>
        private Notes notes;

        public ExportNotes(Notes notes)
        {
            this.notes = notes;
        }

        /// <summary>
        /// Write a file with all notes as backup.
        /// </summary>
        /// <param name="filenamepath">The filename and path of the notes backup</param>
        /// <param name="notes">Reference to the notes class.</param>
        /// <returns>True if writing backup succeeded otherwise false.</returns>
        public bool WriteNoteFlyNotesBackupFile(string filenamepath)
        {
            bool succeeded = false;
            XmlTextWriter xmlwrite = null;
            try
            {
                xmlwrite = new XmlTextWriter(filenamepath, System.Text.Encoding.UTF8);
                xmlwrite.Formatting = Formatting.Indented;
                xmlwrite.WriteStartDocument(true); // standalone xml file.
                xmlwrite.WriteStartElement("backupnotes");
                xmlwrite.WriteAttributeString("number", this.notes.CountNotes.ToString());
                for (int i = 0; i < this.notes.CountNotes; i++)
                {
                    string skinname = this.notes.GetSkinName(notes.GetNote(i).SkinNr);
                    string content = this.notes.GetNote(i).GetContent();
                    xmlUtil.WriteNoteBody(this.notes.GetNote(i), skinname, content);
                }

                xmlwrite.WriteEndElement();
                xmlwrite.WriteEndDocument();
                succeeded = true;
            }
            finally
            {
                if (xmlwrite != null)
                {
                    xmlwrite.Close();
                }
            }

            return succeeded;
        }

        /// <summary>
        /// Write a stickies CSV backup file.
        /// </summary>
        /// <param name="filename">The full path and filename to write the CSV formatted stickies
        /// compatible file format.</param>
        public void WriteStickiesCSVBackupfile(string filename)
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
        public void WritePNotesBackupfile(string filename)
        {
            FileStream fs = null;
            StreamWriter writer = null;
            try
            {
                fs = new FileStream(filename, FileMode.Create);
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
    }
}
