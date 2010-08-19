//-----------------------------------------------------------------------
// <copyright file="Notes.cs" company="GNU">
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
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// This class has a list of all notes.
    /// </summary>
    public class Notes
    {
        #region Fields (9)

        /// <summary>
        /// The default number color.
        /// </summary>
        private short defaultcolor = 1;

        /// <summary>
        /// Path to where notes are saved.
        /// </summary>
        private string notesavepath;

        /// <summary>
        /// The list with all notes.
        /// </summary>
        private List<FrmNote> noteslst;

        /// <summary>
        /// Are notes updated.
        /// </summary>
        private bool notesupdated = false;

        /// <summary>
        /// Transparecy enabled.
        /// </summary>
        private bool transparecy = false;

        /// <summary>
        /// Twitter enabled.
        /// </summary>
        private bool twitterenabled = false;

        /// <summary>
        /// The textdirection, 0 is left to right, 1 is right to left
        /// </summary>
        private short textdirection = 0;

        /// <summary>
        /// Is hightlight html enabled.
        /// </summary>
        private bool highlighthtml = false;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the Notes class.
        /// </summary>
        /// <param name="firstrun">Is this appliction to run for the first time, with /firstrun parameter.</param>
        public Notes(bool forcefirstrun)
        {
            this.noteslst = new List<FrmNote>();
            bool firstrun = this.SetSettings();
            if (forcefirstrun)
            {
                firstrun = true;
            }
            this.LoadNotes(firstrun);
        }

        #endregion Constructors

        #region Properties (7)

        /// <summary>
        /// Gets or sets a value indicating whether notes HTML note content is highlighted.
        /// </summary>
        public bool HighlightHTML
        {
            get
            {
                return this.highlighthtml;
            }

            set
            {
                this.highlighthtml = value;
            }
        }

        /// <summary>
        /// Gets a note.
        /// </summary>
        public List<FrmNote> GetNotes
        {
            get
            {
                return this.noteslst;
            }
        }

        /// <summary>
        /// Gets the path where notes are saved.
        /// </summary>
        public string NoteSavePath
        {
            get
            {
                return this.notesavepath;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether notes are updated.
        /// </summary>
        public bool NotesUpdated
        {
            get
            {
                return this.notesupdated;
            }

            set
            {
                this.notesupdated = value;
            }
        }

        /// <summary>
        /// Gets the number of notes.
        /// </summary>
        public short NumNotes
        {
            get
            {
                short numnotes = Convert.ToInt16(this.noteslst.Count);
                if (numnotes > 255)
                {
                    throw new Exception("Too many notes.");
                }

                return numnotes;
            }
        }

        /// <summary>
        /// Gets a value indicating whether transparency is enabled.
        /// </summary>
        public bool Transparency
        {
            get
            {
                return this.transparecy;
            }
        }

        /// <summary>
        /// Gets a value indicating whether twitter is enabled.
        /// </summary>
        public bool TwitterEnabled
        {
            get
            {
                return this.twitterenabled;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether textdirection is left to right(0) or right to left(1).
        /// </summary>
        public short TextDirection
        {
            get
            {
                return this.textdirection;
            }

            set
            {
                this.textdirection = value;
            }
        }

        #endregion Properties

        #region Methods (10)
        /// <summary>
        /// Draws a new note and saves the xml note file.(call to SaveNewNote)
        /// </summary>
        /// <param name="title">The title of the note.</param>
        /// <param name="content">The note content</param>
        /// <param name="notecolor">Note color number</param>
        public void DrawNewNote(string title, string content, short notecolor)
        {
            try
            {
                short newid = Convert.ToInt16(this.noteslst.Count + 1);
                string notefilenm = this.SaveNewNote(newid, title, content, this.defaultcolor);
                Log.Write(LogType.info, "note created: " + notefilenm);
                if (String.IsNullOrEmpty(notefilenm))
                {
                    throw new CustomException("cannot create filename.");
                }

                FrmNote newnote = new FrmNote(this, newid, title, content, notecolor);
                this.noteslst.Add(newnote);
                newnote.StartPosition = FormStartPosition.Manual;
                newnote.Show();
            }
            catch (Exception exc)
            {
                throw new CustomException(exc.Message + " " + exc.StackTrace);
            }
        }

        /// <summary>
        /// Edit a note.
        /// </summary>
        /// <param name="noteid">The note id number of the note to edit.</param>
        public void EditNewNote(int noteid)
        {
            int noteslistpos = noteid - 1;
            if ((noteslistpos >= 0) && (noteslistpos <= this.NumNotes))
            {
                string title = this.noteslst[noteslistpos].NoteTitle;
                string content = this.noteslst[noteslistpos].NoteContent;
                short color = this.noteslst[noteslistpos].NoteColor;
                FrmNewNote newnote = new FrmNewNote(this, color, noteid, title, content);
                newnote.Show();
            }
            else
            {
                throw new CustomException("Note not found in memory.");
            }
        }

        /// <summary>
        /// check settings and set variables
        /// </summary>
        /// <returns>true if first time started.</returns>
        public bool SetSettings()
        {
            xmlHandler getSettings = new xmlHandler(true);

            this.defaultcolor = Convert.ToInt16(getSettings.getXMLnodeAsInt("defaultcolor"));
            if (getSettings.getXMLnodeAsBool("transparecy") == true)
            {
                this.transparecy = true;
            }
            else
            {
                this.transparecy = false;
            }

            if (getSettings.getXMLnodeAsBool("highlightHTML") == true)
            {
                this.HighlightHTML = true;
            }
            else
            {
                this.HighlightHTML = false;
            }

            this.notesavepath = getSettings.getXMLnode("notesavepath");
            this.textdirection = Convert.ToInt16(getSettings.getXMLnodeAsInt("textdirection"));
            this.twitterenabled = !String.IsNullOrEmpty(getSettings.getXMLnode("twitteruser"));
            if (getSettings.getXMLnodeAsBool("savesession") == true)
            {
                FacebookSettings.Uid = getSettings.getXMLnode("uid");
                string strSessionExpires = getSettings.getXMLnode("sesionexpires");
                if (!String.IsNullOrEmpty(strSessionExpires))
                {
                    try
                    {
                        FacebookSettings.Sesionexpires = Convert.ToDouble(strSessionExpires);
                    }
                    catch (InvalidCastException invcastexc)
                    {
                        throw new CustomException("sessionexpires not valid. "+invcastexc.Message);
                    }
                }

                FacebookSettings.Sessionsecret = getSettings.getXMLnode("sessionsecret");
                FacebookSettings.Sessionkey = getSettings.getXMLnode("sessionkey");
            }

            if (getSettings.getXMLnodeAsBool("firstrun"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Update all fonts (family/size etc.) for all notes.
        /// </summary>
        public void UpdateAllFonts()
        {
            foreach (FrmNote curfrmnote in this.noteslst)
            {
                curfrmnote.CheckThings();
            }
        }

        /// <summary>
        /// Update a note
        /// </summary>
        /// <param name="noteid">id of note</param>
        /// <param name="title">the new title</param>
        /// <param name="content">new content</param>
        /// <param name="visible">is the note visible</param>
        public void UpdateNote(int noteid, string title, string content, bool visible)
        {
            int notelstpos = noteid - 1;
            this.noteslst[notelstpos].NoteTitle = title;
            this.noteslst[notelstpos].NoteContent = content;
            this.noteslst[notelstpos].Visible = visible;
            if (visible)
            {
                this.noteslst[notelstpos].Show();
            }

            this.noteslst[notelstpos].CheckThings();
            this.noteslst[notelstpos].UpdateThisNote();
            this.notesupdated = true;
            Log.Write(LogType.info, ("Update note ID:" + noteid));
        }

        // Private Methods (4) 

        /// <summary>
        /// This method set a limit to how many notes can be loaded.
        /// This is to prevent a hang.
        /// </summary>
        /// <param name="id">the note id to check.</param>
        /// <returns>true when limit is reached.</returns>
        private bool CheckLimitNotes(int id)
        {
            if (id > 255)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Create a new FrmNote.
        /// </summary>
        /// <param name="visible">Is the note visible</param>
        /// <param name="ontop">Is the note on top</param>
        /// <param name="title">The note title</param>
        /// <param name="content">The note content</param>
        /// <param name="notecolor">Color note, 0 Gold, 1 orange, 2 White, 3 LawnGreen, 4 CornflowerBlue, 5 Magenta, 6 Red</param>
        /// <param name="locX">X coordinate</param>
        /// <param name="locY">Y coordinate</param>
        /// <param name="notewidth">The note width</param>
        /// <param name="noteheight">The note height</param>
        /// <returns>A FrmNote object</returns>
        private FrmNote CreateNote(bool visible, bool ontop, string title, string content, short notecolor, int locX, int locY, int notewidth, int noteheight)
        {
            try
            {
                short newid = Convert.ToInt16(this.noteslst.Count + 1);
                FrmNote newnote = new FrmNote(this, newid, visible, ontop, title, content, notecolor, locX, locY, notewidth, noteheight);
                return newnote;
            }
            catch (Exception exc)
            {
                throw new CustomException(exc.Message + " " + exc.StackTrace);
            }
        }

        /// <summary>
        /// Loads all notes.
        /// </summary>
        /// <param name="firstrun">is it the first run?</param>
        private void LoadNotes(bool firstrun)
        {
            if (!Directory.Exists(this.notesavepath))
            {
                const string notefoldernoteexist = "Folder with notes does not exist.\r\nDo want to try loading notes from default application data folder?";
                DialogResult result = MessageBox.Show(notefoldernoteexist, "notefolder doesn't exist", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.No)
                {
                    Log.Write(LogType.error, (notefoldernoteexist + " No"));
                    return;
                }
                else
                {
                    Log.Write(LogType.error, (notefoldernoteexist + " Yes"));
                    xmlHandler getAppdata = new xmlHandler(true);
                    this.notesavepath = getAppdata.AppDataFolder;
                }
            }

            ushort id = 1;
            string notefile = Path.Combine(this.notesavepath, id + ".xml");
            while (File.Exists(notefile) == true)
            {
                xmlHandler parserNote = new xmlHandler(notefile);
                bool[] noteSettingBool;
                string title, content;
                int[] noteSettingsInt;
                short notecolor;
                try
                {
                    noteSettingBool = parserNote.ParserNoteBools();
                    title = parserNote.getXMLnode("title");
                    content = parserNote.getXMLnode("content");
                    noteSettingsInt = parserNote.ParserNoteInts();
                    notecolor = Convert.ToInt16(noteSettingsInt[0]);
                }
                catch (Exception exc)
                {
                    throw new CustomException("Note parser error, " + exc.Message);
                }

                this.noteslst.Add(this.CreateNote(noteSettingBool[0], noteSettingBool[1], title, content, notecolor, noteSettingsInt[1], noteSettingsInt[2], noteSettingsInt[3], noteSettingsInt[4]));
                id++;
                if (this.CheckLimitNotes(id))
                {
                    const string toomanynotes = "Too many notes to load.";
                    Log.Write(LogType.error, toomanynotes);
                    MessageBox.Show(toomanynotes);
                    return;
                }

                notefile = Path.Combine(this.notesavepath, id + ".xml");
            }

            id++;
            notefile = Path.Combine(this.notesavepath, id + ".xml");
            if (File.Exists(notefile))
            {
                string notemissing = this.notesavepath + Convert.ToString(id - 1) + ".xml is missing.";
                MessageBox.Show(notemissing);
                Log.Write(LogType.error, notemissing);
            }

            if (firstrun)
            {
                int tipnotewidth = 320;
                int tipnoteheight = 280;
                int tipnoteposx = ((Screen.PrimaryScreen.WorkingArea.Width / 2) - (tipnotewidth / 2));
                int tipnoteposy = ((Screen.PrimaryScreen.WorkingArea.Height / 2) - (tipnoteheight / 2));
                StringBuilder notecontent = new StringBuilder();
                notecontent.AppendLine("This is a example note.");
                notecontent.AppendLine("You can chance colour of this note by rightclicking on this note.");
                notecontent.AppendLine("You can delete this note, by rightclicking the systray icon choice manage notes");
                notecontent.AppendLine("and then press delete note button for this particuler note.");
                notecontent.AppendLine("By clicking on the cross on this note this note will be hidden.");
                notecontent.AppendLine("You can get it back with the manage notes window.");
                this.noteslst.Add(this.CreateNote(true, false, "Example", notecontent.ToString(), 0, tipnoteposx, tipnoteposy, tipnotewidth, tipnoteheight));

                xmlHandler settting = new xmlHandler(true);
                bool[] boolsettings = settting.ParserSettingsBool();
                settting.WriteSettings(
                    boolsettings[0],
                    Convert.ToDecimal(settting.getXMLnodeAsInt("transparecylevel")),
                    settting.getXMLnodeAsInt("defaultcolor"),
                    settting.getXMLnodeAsInt("actionleftclick"),
                    boolsettings[1],
                    settting.getXMLnode("fontcontent"),
                    Convert.ToDecimal(settting.getXMLnodeAsInt("fontsize")),
                    settting.getXMLnodeAsInt("textdirection"),
                    settting.getXMLnode("notesavepath"),
                    settting.getXMLnode("defaultemail"),
                    boolsettings[4],
                    boolsettings[5],
                    boolsettings[6],
                    settting.getXMLnode("twitteruser"),
                    settting.getXMLnode("twitterpass"),
                    boolsettings[2],
                    boolsettings[3],
                    boolsettings[7],
                    settting.getXMLnode("proxyaddr"),
                    settting.getXMLnodeAsInt("timeout"),
                    true,
                    boolsettings[8]);

                Log.Write(LogType.info, "firstrun occurre"); //by default not logged.
            }

#if DEBUG
#warning Stress test enabled
            Log.Write(LogType.info, "start stress test");
            this.LoadNotesStressTest(10);
            Log.Write(LogType.info, "finished stress test");
#endif
        }

        /// <summary>
        /// Save the note to xml file.
        /// </summary>
        /// <param name="id">note id number.</param>
        /// <param name="title">the title of the note.</param>
        /// <param name="text">the content of the note.</param>
        /// <param name="numcolor">the color number.</param>
        /// <returns>filepath of the created note.</returns>
        private string SaveNewNote(int id, string title, string text, short numcolor)
        {
            string notefile = Path.Combine(this.notesavepath, id + ".xml");
            xmlHandler xmlnote = new xmlHandler(notefile);
            if (xmlnote.WriteNote(true, false, numcolor, title, text, 10, 10, 240, 240) == false)
            {
                throw new CustomException("Cannot write note.");
            }

            return notefile;
        }

        #endregion Methods

#if DEBUG
        /// <summary>
        /// Methode that creates some notes with a hardcoded text and random color 
        /// for stress testing this application.
        /// </summary>
        /// <param name="maxnotes">How many notes to create.</param>
        private void LoadNotesStressTest(int maxnotes)
        {
            Random ran = new Random();

            for (int id = 1; id <= maxnotes; id++)
            {
                bool visible = true;
                bool ontop = false;
                string title = "test nr." + id + " testalongtitlesoiteasytoseeifresizingofmanagenoteisdonecorrectlyblablabla";
                string content = "This is a stress test creating a lot of notes, to see how fast or slow it loads.\r\n" +
                                 "warning: To prevent this note from saving don't move or touch it!";
                short notecolor = Convert.ToInt16(ran.Next(0, 6));
                int noteLocX = ran.Next(0, 360);
                int noteLocY = ran.Next(0, 240);
                int notewidth = 180;
                int noteheight = 180;

                this.noteslst.Add(this.CreateNote(visible, ontop, title, content, notecolor, noteLocX, noteLocY, notewidth, noteheight));

                if (this.CheckLimitNotes(id))
                {
                    string maxnoteslimit = "Maximum notes limit reached.";
                    MessageBox.Show(maxnoteslimit);
                    Log.Write(LogType.error, maxnoteslimit);
                    return;
                }
            }
        }
#endif
    }
}
