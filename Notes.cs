//-----------------------------------------------------------------------
// <copyright file="Notes.cs" company="GNU">
//  NoteFly a note application.
//  Copyright (C) 2010  Tom
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
        /// The list with all notes.
        /// </summary>
        private List<Note> notes;
        private List<FrmNote> notesfrms;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the Notes class.
        /// </summary>
        /// <param name="firstrun">Is this appliction to run for the first time, with /firstrun parameter.</param>
        public Notes(bool forcefirstrun)
        {
            this.notes = new List<Note>();
            this.notesfrms = new List<FrmNote>();

            bool firstrun = Settings.ProgramFirstrun;
            if (forcefirstrun)
            {
                firstrun = true;
            }
            this.LoadNotes(firstrun);
        }

        #endregion Constructors

        #region Properties (7)

        /// <summary>
        /// Gets the number of notes.
        /// </summary>
        public short NumNotes
        {
            get
            {
                short numnotes = Convert.ToInt16(this.notes.Count);
                if (numnotes > 255)
                {
                    throw new Exception("Too many notes.");
                }

                return numnotes;
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
                short newid = Convert.ToInt16(this.notes.Count + 1);
                string notefilenm = this.SaveNewNote(newid, title, content, Convert.ToInt16(Settings.NotesDefaultColor));
                Log.Write(LogType.info, "note created: " + notefilenm);
                if (String.IsNullOrEmpty(notefilenm))
                {
                    throw new CustomException("cannot create filename.");
                }
//todo
                //FrmNote newnote = new FrmNote(this, newid, title, content, notecolor);
                //this.noteslst.Add(newnote);
                //newnote.StartPosition = FormStartPosition.Manual;
                //newnote.Show();
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
                //string title = this.noteslst[noteslistpos].NoteTitle;
                //string content = this.noteslst[noteslistpos].NoteContent;
                //short color = this.noteslst[noteslistpos].NoteColor;
                //FrmNewNote newnote = new FrmNewNote(this, color, noteid, title, content);
                //newnote.Show();
            }
            else
            {
                throw new CustomException("Note not found in memory.");
            }
        }

        /// <summary>
        /// Update all fonts (family/size etc.) for all notes.
        /// </summary>
        public void UpdateAllFonts()
        {
            //foreach (FrmNote curfrmnote in this.notes)
            //{
            //    curfrmnote.CheckThings();
            //}
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
            //this.noteslst[notelstpos].NoteTitle = title;
            //this.noteslst[notelstpos].NoteContent = content;
            //this.noteslst[notelstpos].Visible = visible;
            if (visible)
            {
                this.notesfrm[notelstpos].Show();
            }

            this.notes[notelstpos].CheckThings();
            this.notes[notelstpos].UpdateThisNote();

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
        //private FrmNote CreateNote(bool visible, bool ontop, string title, string content, short notecolor, int locX, int locY, int notewidth, int noteheight)
        //{
        //    try
        //    {
        //        short newid = Convert.ToInt16(this.noteslst.Count + 1);
        //        //FrmNote newnote = new FrmNote(this, newid, visible, ontop, title, content, notecolor, locX, locY, notewidth, noteheight);
        //        //return newnote;
        //    }
        //    catch (Exception exc)
        //    {
        //        throw new CustomException(exc.Message + " " + exc.StackTrace);
        //    }
        //}

        /// <summary>
        /// Loads all notes.
        /// </summary>
        /// <param name="firstrun">is it the first run?</param>
        private void LoadNotes(bool firstrun)
        {
            if (!Directory.Exists(Settings.NotesSavepath))
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
                    //xmlHandler getAppdata = new xmlHandler(true);
                    Settings.NotesSavepath = TrayIcon.AppDataFolder;
                }
            }

            ushort id = 1;
            string[] notefiles = Directory.GetFiles(Settings.NotesSavepath, "*.");
            /*
            string notefile = Path.Combine(Settings.NotesSavepath, id + ".note");
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
            */

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

                //xmlUtil.
                
                Log.Write(LogType.info, "firstrun occurre");
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
            //xmlHandler xmlnote = new xmlHandler(notefile);
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
