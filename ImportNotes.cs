namespace NoteFly
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    /// <summary>
    /// Class for importing from several different note formats.
    /// </summary>
    public class ImportNotes
    {
        /// <summary>
        /// The width of a imported note by default.
        /// </summary>
        private const int DEFAULTIMPORTNOTEWIDTH = 240;

        /// <summary>
        /// The height of a imported note by default.
        /// </summary>
        private const int DEFAULTIMPORTNOTEHEIGHT = 200;

        /// <summary>
        /// Reference to notes 
        /// </summary>
        private Notes notes;

        #region Constructors (1)

        /// <summary>
        /// Initializes new instance of ImportNote class.
        /// </summary>
        /// <param name="notes">Reference to notes class</param>
        public ImportNotes(Notes notes)
        {
            this.notes = notes;
        }

        #endregion Constructors

        /// <summary>
        /// Import a textfile as note content for a new note.
        /// </summary>
        /// <param name="reader">A streamreader to read the note content with</param>
        /// <param name="rtbNewNote">Reference to the richtedittextbox that gets the imported note content.</param>
        public void ReadTextfile(StreamReader reader, RichTextBox rtbNewNote)
        {
            rtbNewNote.Text = reader.ReadToEnd();
        }

        /// <summary>
        /// Import a QuickPad note file.
        /// </summary>
        /// <param name="reader">The StreamReader</param>
        /// <param name="tbTitle">The textbox to set title in.</param>
        /// <param name="rtbNewNote">The richteditextbox to set note content in.</param>
        public void ReadQuickpadFile(StreamReader reader, TextBox tbTitle, RichTextBox rtbNewNote)
        {
            string firstline = reader.ReadLine();
            if (firstline.Length > 255)
            {
                firstline = firstline.Substring(0, 255);
            }

            tbTitle.Text = firstline;
            rtbNewNote.Text = reader.ReadToEnd();
        }

        /// <summary>
        /// Import a rtf file as note content for a new note.
        /// </summary>
        /// <param name="reader">Streamreader to read the note content.</param>
        /// <param name="rtbNewNote">Reference to the richtedittextbox that gets the imported note content.</param>
        public void ReadRTFfile(StreamReader reader, RichTextBox rtbNewNote)
        {
            rtbNewNote.Rtf = reader.ReadToEnd();
            this.SetDefaultFontFamilyAndSize(rtbNewNote);
        }

        /// <summary>
        /// Import a KeyNote note file as note content for a new note.
        /// </summary>
        /// <param name="reader">The streamreader to read the KeyNote.</param>
        /// <param name="rtbNewNote">Reference to the richtedittextbox that gets the imported note content.</param>
        public void ReadKeyNotefile(StreamReader reader, RichTextBox rtbNewNote)
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

                rtbNewNote.Rtf = sb.ToString();
                this.SetDefaultFontFamilyAndSize(rtbNewNote);
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
        /// <param name="tbTitle">The textbox to set the title in.</param>
        /// <param name="rtbNewNote">The richedittextbox to set the content in.</param>
        public void ReadTomboyfile(StreamReader reader, string tomboynotefile, TextBox tbTitle, RichTextBox rtbNewNote)
        {
            tbTitle.Text = xmlUtil.GetContentString(tomboynotefile, "title");
            rtbNewNote.Clear();
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
                    ////List<int> formatstartpos = new List<int>();
                    ////List<string> formattype = new List<string>();
                    ////List<int> formatlen = new List<int>();

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
                            }
                            else
                            {
                                startcontentnode = rtbNewNote.TextLength;
                            }

                            innode = false;
                        }
                        else if (!innode)
                        {
                            rtbNewNote.Text = rtbNewNote.Text + tomboycontent[i];
                        }
                    }

                    // todo import TomBoy note formatting, currently NoteFly only imports it as plain text.
                }
            }
        }

        /// <summary>
        /// Import a MicroSE note file as note content for a new note.
        /// </summary>
        /// <param name="reader">The streamreader to read the MicroSE note file with.</param>
        /// <param name="tbTitle">The textbox to set title in.</param>
        /// <param name="rtbNewNote">The richedittextbox to set content in.</param>
        public void ReadMicroSENotefile(StreamReader reader, TextBox tbTitle, RichTextBox rtbNewNote)
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
                    tbTitle.Text = title;
                }

                if (line.StartsWith(@"{\rtf1") || contentstarted)
                {
                    sbcontent.AppendLine(line);
                    if (line.Equals("}"))
                    {
                        rtbNewNote.Rtf = sbcontent.ToString();
                        contentstarted = false;
                    }
                    else
                    {
                        contentstarted = true;
                    }
                }
            }
        }
        
        /// <summary>
        /// Read a NoteFly backup file.
        /// </summary>
        /// <param name="file">The full path and filename of the notefly backup file.</param>
        public void ReadNoteFlyBackupFile(string file)
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
        /// <param name="file">The stickies CSV file.</param>
        public void ReadStickiesCSVFile(string file)
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

            Log.Write(LogType.info, "Imported stickies csv file: " + file);
        }

        /// <summary>
        /// Read a PNotes full backup file.
        /// </summary>
        /// <param name="file">The file</param>
        public void ReadPNotesBackupFile(string file)
        {
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(file, Encoding.ASCII); // ANSI
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
                                ////
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

            Log.Write(LogType.info, "Imported PNotes full backup file: " + file);
        }

        /// <summary>
        /// Read CintaNotes xml file exported notes.
        /// </summary>
        /// <param name="file">The CintaNotes exported notes xml full file path.</param>
        public void ReadCintaNotesXMLFile(string file)
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
        /// Read a DeskNotes Data XML file.
        /// </summary>
        /// <param name="file">The DeskNote file.</param>
        public void ReadDeskNotesXmlFile(string file)
        {
            XmlTextReader reader = null;
            try
            {
                reader = new XmlTextReader(file);
                int notenr = 0;
                while (reader.Read())
                {
                    if (reader.Name == "DeskNote")
                    {
                        notenr++;
                        bool ontop = false;
                        bool visible = true;
                        try
                        {
                            ontop = Convert.ToBoolean(reader.GetAttribute("d2p1:onTop"));
                            visible = !Convert.ToBoolean(reader.GetAttribute("d2p1:hidden"));
                        }
                        catch
                        {
                            Log.Write(LogType.exception, "Cannot read or convert d2p1:onTop and/or d2p1:hidden.");
                        }

                        Note newnote = new Note(this.notes, this.notes.GetNoteFilename("DeskNotes" + notenr));
                        newnote.X = 10;
                        newnote.Y = 10;
                        newnote.Width = DEFAULTIMPORTNOTEWIDTH;
                        newnote.Height = DEFAULTIMPORTNOTEHEIGHT;
                        newnote.Title = "DeskNotes" + notenr;
                        newnote.Visible = visible;
                        newnote.Ontop = ontop;

                        XmlReader notepartreader = reader.ReadSubtree();
                        while (notepartreader.Read())
                        {
                            switch (notepartreader.Name)
                            {
                                case "x":
                                    newnote.X = notepartreader.ReadElementContentAsInt();
                                    break;
                                case "y":
                                    newnote.Y = notepartreader.ReadElementContentAsInt();
                                    break;
                                case "width":
                                    newnote.Width = notepartreader.ReadElementContentAsInt();
                                    break;
                                case "height":
                                    newnote.Height = notepartreader.ReadElementContentAsInt();
                                    break;
                                case "LastModificationTime":
                                    if (Settings.NotesDefaultTitleDate)
                                    {
                                        newnote.Title = notepartreader.ReadElementContentAsString();
                                        try
                                        {
                                            DateTime dt = DateTime.Parse(newnote.Title);
                                            newnote.Title = dt.ToShortDateString() + " " + dt.ToShortTimeString();
                                        }
                                        catch
                                        {
                                            Log.Write(LogType.exception, "Cannot figure out LastModificationTime as DateTime in DeskNotes note");
                                        }
                                    }

                                    break;
                                case "text":
                                    newnote.Tempcontent = notepartreader.ReadElementContentAsString();
                                    break;
                            }
                        }

                        xmlUtil.WriteNote(newnote, this.notes.GetSkinName(Settings.NotesDefaultSkinnr), newnote.Tempcontent);                        
                    }
                }

                this.notes.LoadNotes(true, false);
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
        /// Read a note file from NoteFly 1.0.x and save as a current NoteFly note.
        /// </summary>
        /// <param name="nf1notefile">THe filename of the NoteFly 1.0.x note to import.</param>
        public void ReadNoteFly1Note(string nf1notefile)
        {
            string nf1note_title = xmlUtil.GetContentString(nf1notefile, "title");
            int nf1note_skinnr = xmlUtil.GetContentInt(nf1notefile, "color");
            if (nf1note_skinnr >= this.notes.CountSkins)
            {
                nf1note_skinnr = 0;
            }

            bool nf1note_visible = false;
            if (xmlUtil.GetContentInt(nf1notefile, "visible") == 1)
            {
                nf1note_visible = true;
            }

            Note importnf1note = new Note(this.notes, this.notes.GetNoteFilename(nf1note_title));
            importnf1note.Visible = nf1note_visible;
            importnf1note.Title = nf1note_title;
            importnf1note.SkinNr = nf1note_skinnr;
            importnf1note.Ontop = false;
            importnf1note.Locked = false;
            importnf1note.Wordwarp = true;
            importnf1note.X = xmlUtil.GetContentInt(nf1notefile, "x");
            importnf1note.Y = xmlUtil.GetContentInt(nf1notefile, "y");
            importnf1note.Width = xmlUtil.GetContentInt(nf1notefile, "width");
            importnf1note.Height = xmlUtil.GetContentInt(nf1notefile, "heigth");
            string content = xmlUtil.GetContentString(nf1notefile, "content");
            string newcontentrtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0{\\fonttbl{\\f0\\fnil\\fcharset0 Verdana;}}\r\n\\viewkind4\\uc1\\pard\\f0\\fs20" + content + "\\ulnone\\par\r\n}\r\n";
            xmlUtil.WriteNote(importnf1note, this.notes.GetSkinName(nf1note_skinnr), newcontentrtf);
        }

        /// <summary>
        /// Decode stickies title from UTF32 to UTF16
        /// </summary>
        /// <param name="title_enc">Title encoded as UTF-32</param>
        /// <returns>Title string as UTF-16</returns>
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
        /// Set font family and size.
        /// </summary>
        /// <param name="rtbNewNote">Reference to the richtedittextbox that gets the imported note content.</param>
        private void SetDefaultFontFamilyAndSize(RichTextBox rtbNewNote)
        {
            rtbNewNote.SelectAll();
            rtbNewNote.Font = new System.Drawing.Font(Settings.FontContentFamily, (float)Settings.FontContentSize);
            rtbNewNote.Select(0, 0);
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
    }
}
