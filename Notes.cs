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
    using System.Drawing;

    /// <summary>
    /// This class has a list of all notes.
    /// </summary>
    public class Notes
    {
        #region Fields (2)

        /// <summary>
        /// The list with all notes.
        /// </summary>
        private List<Note> notes;

        /// <summary>
        /// List with all skins for notes.
        /// </summary>
        private List<Skin> skins;

        //private const Color unknowclr = Color.Yellow;

        private const string fileextension = ".nfn";

        #endregion Fields

        #region Constructors (1)

        //private List<FrmNote> notesfrms;
        /// <summary>
        /// Initializes a new instance of the Notes class.
        /// </summary>
        public Notes()
        {
            this.notes = new List<Note>();
            this.skins = new List<Skin>();
            this.skins = xmlUtil.LoadSkins();
            this.LoadNotes(Settings.ProgramFirstrun);
        }

        #endregion Constructors

        #region Properties (1)

        /// <summary>
        /// The number of notes there are.
        /// </summary>
        public int CountNotes
        {
            get
            {
                return this.notes.Count;
            }
        }

        //public int CountSkins
        //{
        //    get
        //    {
        //        return this.skins.Count;
        //    }
        //}

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
        public Note CreateNote(String title, int skinnr, int x, int y, int width, int height)
        {
            Note newnote = new Note(this, DateTime.Now);
            newnote.Id = this.NextId();
            newnote.Title = title;
            newnote.SkinNr = skinnr;
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
        /// Gets a note, by the noteid
        /// Index starts at 1.
        /// </summary>
        /// <param name="nr"></param>
        /// <returns></returns>
        public Note GetNote(int noteid)
        {
            int nr = noteid - 1;
            return this.notes[nr];
        }

        /// <summary>
        /// Save an note.
        /// </summary>
        /// <param name="note">the note (with all settings except large content).</param>
        /// <param name="content">The note content</param>
        /// <returns>true on succeed.</returns>
        public bool SaveNote(Note note, string content)
        {
            try
            {
                string notefile = this.GetNoteFilename(note.Id, note.Title);
                if (String.IsNullOrEmpty(notefile))
                {
                    throw new CustomException("cannot create filename.");
                }
                else if (xmlUtil.WriteNote(notefile, note, content))
                {
                    Log.Write(LogType.info, "note created: " + notefile);
                    return true;
                }
            }
            catch (Exception exc)
            {
                throw new CustomException(exc.Message + " " + exc.StackTrace);
            }
            return false;
        }

        /// <summary>
        /// Add a new note the the notes list.
        /// </summary>
        /// <param name="note">The note to be added.</param>
        public void AddNote(Note note)
        {
            this.notes.Add(note);
        }

        /// <summary>
        /// Strip forbidden filename characters
        /// </summary>
        /// <param name="orgname"></param>
        /// <returns></returns>
        public string StripForbiddenFilenameChars(String orgname)
        {
            System.Text.StringBuilder newfilename = new System.Text.StringBuilder();
            char[] forbiddenchars = "?<>:*|\\/".ToCharArray();
            bool isforbiddenchar = false;
            for (int pos = 0; (pos < orgname.Length); pos++)
            {
                isforbiddenchar = false;
                for (int fc = 0; fc < forbiddenchars.Length; fc++)
                {
                    if (orgname[pos] == forbiddenchars[fc])
                    {
                        isforbiddenchar = true;
                    }
                }
                if (!isforbiddenchar)
                {
                    newfilename.Append(orgname[pos]);
                }
            }
            return newfilename.ToString();
        }

        /// <summary>
        /// Create a string used for filename of the note based on the note id and 
        /// title of the note limited to the first 16 characters.
        /// </summary>
        /// <param name="noteid"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public string GetNoteFilename(int id, string title)
        {
            string title2 = StripForbiddenFilenameChars(title);
            if (title.Length > 16)
            {
                return Path.Combine(Settings.NotesSavepath, id + "-" + title2.Substring(0, 16) + fileextension);
            }
            else
            {
                return Path.Combine(Settings.NotesSavepath, id + "-" + title2 + fileextension);
            }
        }

        /// <summary>
        /// Remove a note with a particalur noteid from the notes list.
        /// </summary>
        /// <param name="noteid">The noteId starts at 1</param>
        public void RemoveNote(int noteid)
        {
            for (int i = 0; i < this.notes.Count; i++)
            {
                if (this.notes[i].Id == noteid)
                {
                    if (this.notes[i].frmnote != null)
                    {
                        this.notes[i].DestroyForm();
                    }
                    this.notes.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Get the skinnr that belongs by a name.
        /// If not found return -1.
        /// </summary>
        /// <param name="skinname"></param>
        /// <returns></returns>
        public int GetSkinNr(string skinname)
        {
            foreach (Skin curskin in this.skins)
            {
                if (curskin.Name == skinname)
                {
                    return curskin.Nr;
                }
            }
            Log.Write(LogType.error, "SkinNr not found for skinname:" + skinname);
            return -1;
        }

        /// <summary>
        /// Get the name of a skin by the skinnr.
        /// </summary>
        /// <param name="skinnr">The skin number (starts at index 0)</param>
        /// <returns>The name of the skin, e.g. Yellow</returns>
        public string GetSkinName(int skinnr)
        {
            return this.skins[skinnr].Name;
        }

        /// <summary>
        /// Gets a string array with all the skin names.
        /// </summary>
        /// <returns></returns>
        public string[] GetSkinsNames()
        {
            string[] skinnames = new string[this.skins.Count];
            for (int i = 0; i < skinnames.Length; i++)
            {
                skinnames[i] = this.skins[i].Name;
            }
            return skinnames;
        }

        /// <summary>
        /// Bring all notes to front of all other windows.
        /// </summary>
        public void BringToFrontNotes()
        {
            for (int i = 0; i < this.notes.Count; i++)
            {
                if (this.notes[i].Visible)
                {
                    try
                    {
                        this.notes[i].frmnote.BringToFront();
                    }
                    catch (NullReferenceException)
                    {
                        throw new CustomException("Visible is set true but form is null.");
                    }
                }
            }
        }

        /// <summary>
        /// Gets the foreground color.
        /// </summary>
        /// <param name="skinnr"></param>
        /// <returns></returns>
        public System.Drawing.Color GetForegroundColor(int skinnr)
        {
            return GetColor(1, skinnr);
        }

        /// <summary>
        /// Gets the background color.
        /// </summary>
        /// <param name="skinnr"></param>
        /// <returns></returns>
        public System.Drawing.Color GetBackgroundColor(int skinnr)
        {
            return GetColor(2, skinnr);
        }

        /// <summary>
        /// Gets the highlight color.
        /// </summary>
        /// <param name="skinnr"></param>
        /// <returns></returns>
        public System.Drawing.Color GetHighlightColor(int skinnr)
        {
            return GetColor(3, skinnr);
        }

        private System.Drawing.Color GetColor(int type, int skinnr)
        {
            foreach (Skin skin in this.skins)
            {
                if (skin.Nr == skinnr)
                {
                    switch (type)
                    {
                        case 1:
                            return skin.ForegroundClr;
                        case 2:
                            return skin.BackgroundClr;
                        case 3:
                            return skin.HighlightClr;
                    }
                }
            }
            Log.Write(LogType.error, "cant get color. type:"+type+" skinnr"+skinnr);
            return Color.White;
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
        private bool CheckLimitNotes(int number)
        {
            if (number > Settings.NotesWarnLimit)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Loads all notes files.
        /// </summary>
        /// <param name="firstrun">true if it is the first run</param>
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
                    Settings.NotesSavepath = Program.AppDataFolder;
                }
            }

            string[] notefiles = Directory.GetFiles(Settings.NotesSavepath, "*.nfn"); //nfn, stands for: NoteFly Note
            if (CheckLimitNotes(notefiles.Length))
            {
                MessageBox.Show("Too many notes,");
            }
            else
            {
                this.notes.Capacity = notefiles.Length;
            }
            for (int i = 0; i < notefiles.Length; i++)
            {
                Note note = xmlUtil.LoadNote(this, notefiles[i]);
                note.Id = NextId();
                this.AddNote(note);
                if (note.Visible)
                {
                    note.CreateForm();
                }
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
                Settings.ProgramFirstrun = false;
                Log.Write(LogType.info, "firstrun occur");
                xmlUtil.WriteSettings();
            }

#if DEBUG
#warning Stress test enabled
            Log.Write(LogType.info, "start stress test");
            this.LoadNotesStressTest(5);
            Log.Write(LogType.info, "finished stress test");
#endif
        }

#if DEBUG
        /// <summary>
        /// Methode that creates some notes with a hardcoded text and random color 
        /// for stress testing this application.
        /// </summary>
        /// <param name="maxnotes">How many notes to create.</param>
        private void LoadNotesStressTest(int maxnotes)
        {
            Random rnd = new Random();

            if (this.CheckLimitNotes(maxnotes))
            {
                const string maxnoteslimit = "";
                MessageBox.Show(maxnoteslimit);
                Log.Write(LogType.error, maxnoteslimit);
                return;
            }

            for (int id = 1; id <= maxnotes; id++)
            {
                string title = "test nr." + id + " testalongtitlesoiteasytoseeifresizingofmanagenoteisdonecorrectlyblablabla";
                int skinnr = rnd.Next(0, 6);
                int noteLocX = rnd.Next(0, 360);
                int noteLocY = rnd.Next(0, 240);
                int notewidth = 180;
                int noteheight = 180;
                Note testnote = this.CreateNote(title, skinnr, noteLocX, noteLocY, notewidth, noteheight);
                //testnote.CreateForm();
                //testnote.frmnote.rtbNote.Text = "This is a stress test creating a lot of notes, to see how fast or slow it loads.\r\n" +
                //     "warning: To prevent this note from saving don't move or touch it!";
                
                this.notes.Add(testnote);
            }
        }
    #endif

    #endregion Methods

    }
}
