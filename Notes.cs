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
    using System.Globalization;
#if DEBUG
    using System.Diagnostics;

#endif
    /// <summary>
    /// Notes class, holds all notes
    /// </summary>
    public class Notes
    {
		#region Fields (3) 

        /// <summary>
        /// The note extension
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

        public bool frmmangenotesneedupdate = false;

		#endregion Fields 

		#region Constructors (1) 

        //private List<FrmNote> notesfrms;
        /// <summary>
        /// Initializes a new instance of the Notes class.
        /// </summary>
        public Notes(bool resetpositions)
        {
            this.notes = new List<Note>();
            this.skins = new List<Skin>();
            this.skins = xmlUtil.LoadSkins();
            this.LoadNotes(Settings.ProgramFirstrun, resetpositions);
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

		#region Methods (20) 

		// Public Methods (16) 

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
                    if (notes[i] == null)
                    {
                        throw new CustomException("Note object is null.");
                    }
                    else
                    {
                        this.notes[i].BringNoteToFront();
                    }
                }
            }
        }

        /// <summary>
        /// Create a new note object with some default settings.
        /// </summary>
        /// <param name="title">The title of the note.</param>
        /// <param name="skinnr">The skinnr</param>
        /// <param name="x">X-coordinate</param>
        /// <param name="y">Y-coordinate</param>
        /// <param name="height">Height of the note</param>
        /// <param name="width">Width of the note</param>
        /// <returns>Note object</returns>
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
        /// Gets a note, by position in list
        /// </summary>
        /// <param name="pos">note position in list notes</param>
        /// <returns>The Note object</returns>
        public Note GetNote(int pos)
        {
            //try
            //{
                return this.notes[pos];
            //}
            //catch (IndexOutOfRangeException)
            //{
            //    throw new CustomException("Can't get this note position: " + pos);
            //}
        }

        /// <summary>
        /// Create a string used for filename of the note based on the
        /// title of the note limited to the first xx characters.
        /// </summary>
        /// <param name="title">The title of the note</param>
        /// <returns>A safe filename based on the title.</returns>
        public string GetNoteFilename(string title)
        {
            string title2 = StripForbiddenFilenameChars(title);
            const int limitlenfile = 8;
            string newfile;
            if (title2.Length > limitlenfile)
            {
                newfile = title2.Substring(0, limitlenfile) + NOTEEXTENSION;
            }
            else
            {
                newfile = title2 + NOTEEXTENSION;
            }

            if (File.Exists(Path.Combine(Settings.NotesSavepath, newfile)))
            {
                newfile = Checknewfilename(newfile, limitlenfile, '#');
                return newfile;
            }
            else
            {
                return newfile;
            }
        }

        /// <summary>
        /// Gets the primary color.
        /// </summary>
        /// <param name="skinnr">The skin number</param>
        /// <returns>The primary color</returns>
        public System.Drawing.Color GetPrimaryClr(int skinnr)
        {
            return GetColor(1, skinnr);
        }

        /// <summary>
        /// Gets the selected color.
        /// </summary>
        /// <param name="skinnr">The skin number</param>
        /// <returns>The selected skin color.</returns>
        public System.Drawing.Color GetSelectClr(int skinnr)
        {
            return GetColor(2, skinnr);
        }

        /// <summary>
        /// Gets the highlight color.
        /// </summary>
        /// <param name="skinnr">The skin number</param>
        /// <returns></returns>
        public System.Drawing.Color GetHighlightClr(int skinnr)
        {
            return GetColor(3, skinnr);
        }

        /// <summary>
        /// Gets the text color.
        /// </summary>
        /// <param name="skinnr">The skin number</param>
        /// <returns></returns>
        public System.Drawing.Color GetTextClr(int skinnr)
        {
            return GetColor(4, skinnr);
        }

        /// <summary>
        /// Get the name of a skin by the skinnr.
        /// </summary>
        /// <param name="skinnr">The skin number</param>
        /// <returns>The name of the skin, e.g. Yellow</returns>
        public string GetSkinName(int skinnr)
        {
            return this.skins[skinnr].Name;
        }

        /// <summary>
        /// Get the skinnr that belongs by a name.
        /// If not found return -1.
        /// </summary>
        /// <param name="skinname">The skin name.</param>
        /// <returns>The skinnr, if not found then -1 is returned.</returns>
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
        /// <returns>An array with skin names.</returns>
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
        /// Loads all note files in the NotesSavepath.
        /// </summary>
        /// <param name="firstrun">true if it is the first run</param>
        public void LoadNotes(bool firstrun, bool resetpositions)
        {
            if (!Directory.Exists(Settings.NotesSavepath))
            {
                const string notefoldernoteexist = "Folder with notes does not exist.\r\nDo want to try loading notes from default application data folder?";
                DialogResult result = MessageBox.Show(notefoldernoteexist, "Notes folder doesn't exist", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
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
#if DEBUG
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
#endif
            /*
            //loops twice, but fastest with 7 notes.
            string[] notefilespath = Directory.GetFiles(Settings.NotesSavepath, "*" + NOTEEXTENSION, SearchOption.TopDirectoryOnly);
            string[] notefiles = new string[notefilespath.Length];
            for (int i = 0; i < notefilespath.Length; i++)
            {
                notefiles[i] = Path.GetFileName(notefilespath[i]);
            }
            */
            /*
            //loops twice
            DirectoryInfo notessavedirinfo = new DirectoryInfo(Settings.NotesSavepath);
            FileInfo[] notefilesinfo = notessavedirinfo.GetFiles("*" + NOTEEXTENSION);
            string[] notefiles = new string[notefilesinfo.Length];
            for (int i = 0; i < notefiles.Length; i++)
            {
                notefiles[i] = notefilesinfo[i].Name;
            }
            */
            // /* 
            //loops once, but slowest with 7 notes.
            string[] notefiles = new string[65533]; //FAT32 files per directory limit, -4 files: settings.xml, skins.xml, debug.log and debug.log.old
            int n = 0;
            IEnumerator notefilesenumerator = Directory.GetFiles(Settings.NotesSavepath, "*" + NOTEEXTENSION, SearchOption.TopDirectoryOnly).GetEnumerator();
            while (notefilesenumerator.MoveNext())
            {
                string filepath = notefilesenumerator.Current.ToString();
                notefiles[n] = (Path.GetFileName(filepath));
                n++;
            }
            Array.Resize(ref notefiles, n); //not in NET CF v2.0 
            // */
#if DEBUG
            stopwatch.Stop();
            Settings.ProgramLogInfo = true;
            Log.Write(LogType.info, "Notes search time:  " + stopwatch.ElapsedMilliseconds.ToString() + " ms");
#endif
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
#if DEBUG
            stopwatch.Reset();
            stopwatch.Start();
#endif
            for (int i = 0; i < notefiles.Length; i++)
            {
                Note note = xmlUtil.LoadNoteFile(this, notefiles[i]);
                if (resetpositions)
                {
                    note.X = 10;
                    note.Y = 10;
                }
                this.AddNote(note);
            }
#if DEBUG
            stopwatch.Stop();
            Log.Write(LogType.info, "Notes read time:    " + stopwatch.ElapsedMilliseconds.ToString() + " ms");
            stopwatch.Reset();
            stopwatch.Start();
#endif
            for (int i = 0; i < notefiles.Length; i++)
            {
                if (this.notes[i].Visible)
                {
                    this.notes[i].CreateForm();
                }
            }

#if DEBUG
            stopwatch.Stop();
            Log.Write(LogType.info, "Notes display time: " + stopwatch.ElapsedMilliseconds.ToString() + " ms");
#endif
            if (firstrun)
            {
                CreateFirstrunNote();
            }
        }

        /// <summary>
        /// Remove a note from the notes list.
        /// </summary>
        /// <param name="pos">The note position in the list.</param>
        public void RemoveNote(int pos)
        {
            if (pos >= this.notes.Count || pos < 0)
            {
                throw new CustomException("Cannot find note to remove.");
            }
            else
            {
                this.notes.RemoveAt(pos);
            }
        }

        /// <summary>
        /// Strip forbidden filename characters of a string.
        /// </summary>
        /// <param name="orgname">the string to strip forbidden filecharacters from.</param>
        /// <returns>The filename safe string</returns>
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
        /// Update all note forms.
        /// </summary>
        public void UpdateAllNoteForms()
        {
            foreach (Note curnote in this.notes)
            {
                curnote.UpdateForm();
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

        /// <summary>
        /// Check if filename already exist if it is then generate a new one.
        /// </summary>
        /// <returns>Empty string on all used.</returns>
        private string Checknewfilename(string newfile, int limitlenfile, char sepchar)
        {
            int num = 1;
            int lenfilecounter = 3;
            int numlen = num.ToString(CultureInfo.InvariantCulture.NumberFormat).Length;
            while (File.Exists(Path.Combine(Settings.NotesSavepath, newfile)) && (numlen <= lenfilecounter))
            {
                numlen = num.ToString(CultureInfo.InvariantCulture.NumberFormat).Length;
                newfile = newfile.Substring(0, limitlenfile - numlen - 1) + sepchar + num + NOTEEXTENSION;
                num++;
            }
            if (numlen > lenfilecounter)
            {
                sepchar++;
                if (sepchar < 47)
                {
                    return Checknewfilename(newfile, limitlenfile, sepchar);
                }
                else
                {
                    Log.Write(LogType.exception, "All suggested filenames to save this note based on title seems to be taken.");
                    return "";
                }
            }
            else
            {
                return newfile;
            }
        }

        /// <summary>
        /// Create a demo note with instruction 
        /// (Should be the first time that NoteFly is runned displayed only.)
        /// </summary>
        private void CreateFirstrunNote()
        {
            const int notewidth = 260;
            const int noteheight = 220;
            int noteposx = ((Screen.PrimaryScreen.WorkingArea.Width / 2) - (notewidth / 2));
            int noteposy = ((Screen.PrimaryScreen.WorkingArea.Height / 2) - (noteheight / 2));
            string notecontent = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1043{\\fonttbl{\\f0\\fnil\\fcharset0 Verdana;}}\r\n\\viewkind4\\uc1\\pard\\f0\\fs20 This is a demo note.\\par\r\nPressing the [X] on a note\\par\r\n will \\b hide \\b0 that note.\\par\r\nTo actually \\i delete \\i0 it, use \\par\r\nthe \\i manage notes \\i0 windows\\par\r\n from the \\i trayicon\\i0 .\\ul\\par\r\n\\par\r\nThanks for using NoteFly!\\ulnone\\par\r\n}\r\n";

            Note demonote = this.CreateNote("NoteFly2.0.0", 0, noteposx, noteposy, notewidth, noteheight);
            xmlUtil.WriteNote(demonote, this.GetSkinName(demonote.SkinNr), notecontent);
            this.AddNote(demonote);
            demonote.CreateForm();

            //this.noteslst.Add(this.CreateNote(true, false, "Example", notecontent.ToString(), 0, tipnoteposx, tipnoteposy, tipnotewidth, tipnoteheight));
            Settings.ProgramFirstrun = false;
            Log.Write(LogType.info, "firstrun occur");
            xmlUtil.WriteSettings();
        }

        /// <summary>
        /// Get the color.
        /// </summary>
        /// <param name="type">What part of the skin 1 primary-, 2 selected-, 3 hightlight-, 4 text color</param>
        /// <param name="skinnr">The skin nummer in the skinslist.</param>
        /// <returns>The request color</returns>
        private System.Drawing.Color GetColor(int type, int skinnr)
        {
            switch (type)
            {
                case 1:
                    return this.skins[skinnr].PrimaryClr;
                case 2:
                    return this.skins[skinnr].SelectClr;
                case 3:
                    return this.skins[skinnr].HighlightClr;
                case 4:
                    return this.skins[skinnr].TextClr;
            }
            Log.Write(LogType.error, "Can't get color. type:" + type + " skinnr" + skinnr);
            return Color.White;
        }

		#endregion Methods 
    }
}