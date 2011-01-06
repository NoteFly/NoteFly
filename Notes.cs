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
    using System.Collections;

    /// <summary>
    /// This class has a list of all notes.
    /// </summary>
    public class Notes
    {
		#region Fields (3) 

        /// <summary>
        /// EXTENSION
        /// </summary>
        public const string NOTEEXTENSION = ".nfn";
        /// <summary>
        /// The list with all notes.
        /// </summary>
        private List<Note> notes;
        /// <summary>
        /// List with all skins for notes.
        /// </summary>
        private List<Skin> skins;

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

		#endregion Properties 

		#region Methods (18) 

		// Public Methods (14) 

        /// <summary>
        /// Add a new note the the notes list.
        /// </summary>
        /// <param name="note">The note to be added.</param>
        public void AddNote(Note note)
        {
            this.notes.Add(note);
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

        //public int CountSkins
        //{
        //    get
        //    {
        //        return this.skins.Count;
        //    }
        //}
        /// <summary>
        /// Create a new note object with some default settings.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="skinnr"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public Note CreateNote(String title, int skinnr, int x, int y, int width, int height)
        {
            Note newnote = new Note(this, this.GetNoteFilename(title));
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
        /// Gets the background color.
        /// </summary>
        /// <param name="skinnr"></param>
        /// <returns></returns>
        public System.Drawing.Color GetBackgroundColor(int skinnr)
        {
            return GetColor(2, skinnr);
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
        /// Gets the highlight color.
        /// </summary>
        /// <param name="skinnr"></param>
        /// <returns></returns>
        public System.Drawing.Color GetHighlightColor(int skinnr)
        {
            return GetColor(3, skinnr);
        }

        /// <summary>
        /// Gets a note, by position in list
        /// </summary>
        /// <param name="pos">note position in list notes</param>
        /// <returns>The Note object</returns>
        public Note GetNote(int pos)
        {
            return this.notes[pos];
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
        /// Get the skinnr that belongs by a name.
        /// If not found return -1.
        /// </summary>
        /// <param name="skinname"></param>
        /// <returns></returns>
        public int GetSkinNr(string skinname)
        {
            for (int i = 0; i < this.skins.Count; i++)
            {
                if (this.skins[i].Name == skinname)
                {
                    return i;
                }
            }
            Log.Write(LogType.error, "SkinNr not found for skinname:" + skinname);
            return -1;
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
        /// Close a note form and remove a note from the notes list.
        /// </summary>
        /// <param name="pos">The noteId starts at 1</param>
        public void RemoveNote(int pos)
        {
            if (this.notes[pos].frmnote != null)
            {
                this.notes[pos].DestroyForm();
            }
            this.notes.RemoveAt(pos);
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
                string notefile = this.GetNoteFilename(note.Title);
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
        /// Update all fonts settings for all notes.
        /// </summary>
        public void UpdateAllFonts()
        {
            foreach (Note curnote in this.notes)
            {
                //todo
            }
        }
		// Private Methods (4) 

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

        private System.Drawing.Color GetColor(int type, int skinnr)
        {
            switch (type)
            {
                case 1:
                    return this.skins[skinnr].ForegroundClr;
                case 2:
                    return this.skins[skinnr].BackgroundClr;
                case 3:
                    return this.skins[skinnr].HighlightClr;
            }
            Log.Write(LogType.error, "cant get color. type:" + type + " skinnr" + skinnr);
            return Color.White;
        }

        /// <summary>
        /// Create a string used for filename of the note based on the
        /// title of the note limited to the first xx characters.
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        private string GetNoteFilename(string title)
        {
            string title2 = StripForbiddenFilenameChars(title);
            const int limitlenfile = 8;
            if (title2.Length > limitlenfile)
            {
                return title2.Substring(0, limitlenfile) + NOTEEXTENSION;
            }
            else
            {
                return title2 + NOTEEXTENSION;
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
            /*
            string[] notefiles = Directory.GetFiles(Settings.NotesSavepath, "*" + NOTEEXTENSION, SearchOption.TopDirectoryOnly);
            for (int i = 0; i < notefiles.Length; i++)
            {
                notefiles[i] = Path.GetFileName(notefiles[i]); //memory gaps
            }
            */
            // /*
            string[] notefiles = new string[65533]; //FAT32 files per directory limit, -4 files: settings.xml, skins.xml, debug.log and debug.log.old
            int n =0;
            IEnumerator notefilesenumerator = (Directory.GetFiles(Settings.NotesSavepath, "*" + NOTEEXTENSION, SearchOption.TopDirectoryOnly).GetEnumerator());
            while (notefilesenumerator.MoveNext())
            {
                string filepath = (string)notefilesenumerator.Current; //slow cast
                notefiles[n] = (Path.GetFileName(filepath));
                n++;
            }
            Array.Resize(ref notefiles, n); //not NETCF v2.0 
            // */
            if (CheckLimitNotes(notefiles.Length))
            {
                DialogResult dlgres = MessageBox.Show("Their are many notes loading can take a while, do you want continu?", "contine?", MessageBoxButtons.YesNo);
                if (dlgres == DialogResult.No)
                {
                    return;
                }
            }
            else
            {
                this.notes.Capacity = notefiles.Length;
            }

            for (int i = 0; i < notefiles.Length; i++)
            {
                Note note = xmlUtil.LoadNote(this, notefiles[i]);
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

		#endregion Methods 

#if DEBUG
        /// <summary>
        /// Methode that creates some notes with a hardcoded text and random color 
        /// for stress testing this application.
        /// </summary>
        /// <param name="maxnotes">How many notes to create.</param>
        private void LoadNotesStressTest(int maxnotes)
        {
            Random rnd = new Random();

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
    }
}
