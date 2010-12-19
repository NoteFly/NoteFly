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
        #region Fields (1)

        /// <summary>
        /// The list with all notes.
        /// </summary>
        private List<Note> notes;

        #endregion Fields

        #region Constructors (1)

        //private List<FrmNote> notesfrms;
        /// <summary>
        /// Initializes a new instance of the Notes class.
        /// </summary>
        /// <param name="firstrun">Is this appliction to run for the first time, with /firstrun parameter.</param>
        public Notes(bool forcefirstrun)
        {
            this.notes = new List<Note>();
            bool firstrun = Settings.ProgramFirstrun;

            if (forcefirstrun)
            {
                firstrun = true;
            }
            this.LoadNotes(firstrun);
        }

        #endregion Constructors

        #region Properties (1)

        public int CountNotes
        {
            get
            {
                return this.notes.Count;
            }
        }

        #endregion Properties

        #region Methods (8)

        // Public Methods (5) 

        /// <summary>
        /// Create a new note object with some default settings.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="color"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public Note CreateNote(String title, short color, int x, int y, int width, int height)
        {
            Note newnote = new Note(this, DateTime.Now);
            newnote.Id = this.NextId();
            newnote.Title = title;
            newnote.Color = color;
            newnote.Visible = true;
            newnote.Locked = false;
            newnote.RolledUp = false;
            newnote.Ontop = false;
            newnote.X = x;
            newnote.Y = y;
            newnote.Width = width;
            newnote.Height = height;
            return newnote;
        }


        /// <summary>
        /// Save an note.
        /// </summary>
        /// <param name="note">the note (with all settings).</param>
        /// <param name="content">The note content</param>
        public void SaveNote(Note note, string content)
        {
            try
            {
                string notefile = this.NewNoteFilename(note.Id, note.Title);

                if (String.IsNullOrEmpty(notefile))
                {
                    throw new CustomException("cannot create filename.");
                }
                if (xmlUtil.WriteNote(notefile, note, content))
                {
                    Log.Write(LogType.info, "note created: " + notefile);
                }
            }
            catch (Exception exc)
            {
                throw new CustomException(exc.Message + " " + exc.StackTrace);
            }
        }

        /// <summary>
        /// Gets if note is visible.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool GetNoteVisible(int id)
        {
            return this.notes[id].Visible;
        }

        /// <summary>
        /// Sets visiblitly note.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newvisible"></param>
        public void SetNoteVisible(int id, bool newvisible)
        {
            this.notes[id].Visible = newvisible;
        }

        /// <summary>
        /// Create a string used for filename of the note based on the note id and 
        /// title of the note limited to the first 16 characters.
        /// </summary>
        /// <param name="noteid"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public string NewNoteFilename(int id, string title)
        {
            if (title.Length > 16)
            {
                return Path.Combine(Settings.NotesSavepath, id + title.Substring(0, 16) + ".nfn");
            }
            else
            {
                return Path.Combine(Settings.NotesSavepath, id + title + ".nfn");
            }
        }

        /// <summary>
        /// Update all fonts settings for all notes.
        /// </summary>
        public void UpdateAllFonts()
        {
            foreach (Note curnote in this.notes)
            {
                //todo
            }
        }

        /// <summary>
        /// Gets the next new noteid.
        /// </summary>
        /// <returns></returns>
        private int NextId()
        {
            return this.CountNotes + 1;
        }

        /// <summary>
        /// This method set a limit on how many notes can be loaded before a 
        /// </summary>
        /// <param name="id">the note id to check.</param>
        /// <returns>true when limit is reached, and a warning about too many notes should be showed.</returns>
        private bool CheckLimitNotes(int noteid)
        {
            if (noteid > Settings.NotesWarnLimit)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

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
                    Settings.NotesSavepath = TrayIcon.AppDataFolder;
                }
            }

            int id = 1;

            string[] notefiles = Directory.GetFiles(Settings.NotesSavepath, "*.nfn"); //nfn, stands for: NoteFly Note

            if (CheckLimitNotes(notefiles.Length))
            {
                MessageBox.Show("Test");
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
                //this.noteslst.Add(this.CreateNote(true, false, "Example", notecontent.ToString(), 0, tipnoteposx, tipnoteposy, tipnotewidth, tipnoteheight));

                //xmlUtil.

                Log.Write(LogType.info, "firstrun occur");
            }

#if DEBUG
#warning Stress test enabled
            Log.Write(LogType.info, "start stress test");
            this.LoadNotesStressTest(10);
            Log.Write(LogType.info, "finished stress test");
#endif
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
