//-----------------------------------------------------------------------
// <copyright file="Notes.cs" company="NoteFly">
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
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// Notes class, holds all notes
    /// </summary>
    public class Notes : IPlugin.IPluginHost
    {
        #region Fields (4)

        /// <summary>
        /// The note extension
        /// </summary>
        public const string NOTEEXTENSION = ".nfn";

        /// <summary>
        /// The maximum length of the filename.
        /// </summary>
        private const int LIMITLENFILE = 8;

        /// <summary>
        /// Empty file that hints notefly2 that notefly1 notes folder has been imported.
        /// </summary>
        private const string IMPORTEDFLAGFILE = "impnf20.flg";

        /// <summary>
        /// boolean indication whether FrmManageNotes datagridview needs to be redrawn.
        /// </summary>
        private bool frmmanagenotesneedupdate = false;

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

        /// <summary>
        /// Initializes a new instance of the Notes class.
        /// </summary>
        /// <param name="resetpositions">Boolean true for reseting all notes positions on loading.</param>
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
        /// Gets the number of notes there are.
        /// </summary>
        public int CountNotes
        {
            get
            {
                return this.notes.Count;
            }
        }

        /// <summary>
        /// Gets the number of skins there are.
        /// </summary>
        public int CountSkins
        {
            get
            {
                return this.skins.Count;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whetherr FrmManageNotes datagridview needs to be redrawn.
        /// </summary>
        public bool FrmManageNotesNeedUpdate
        {
            get
            {
                return this.frmmanagenotesneedupdate;
            }

            set
            {
                this.frmmanagenotesneedupdate = value;
            }
        }

        #endregion Properties

        #region Methods (21)

        /// <summary>
        /// Add a new default note to the notes list
        /// </summary>
        /// <param name="title">The title the note.</param>
        /// <param name="skinnr">The skinnr</param>
        /// <param name="x">The X location of the note.</param>
        /// <param name="y">The Y location of the note.</param>
        /// <param name="width">The width of the note.</param>
        /// <param name="height">The height of the note.</param>
        /// <param name="content">The note rtf content.</param>
        /// <param name="wordwarp">Is the note content word warped.</param>
        public void AddNoteDefaultSettings(string title, int skinnr, int x, int y, int width, int height, string content, bool wordwarp)
        {
            Note note = this.CreateNoteDefaultSettings(title, skinnr, x, y, width, height, wordwarp);
            if (note != null)
            {
                this.AddNote(note);
                xmlUtil.WriteNote(note, this.GetSkinName(skinnr), content);
                note.CreateForm();
            }
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
                    if (this.notes[i] == null)
                    {
                        throw new ApplicationException("Note object is null.");
                    }
                    else
                    {
                        this.notes[i].BringNoteToFront();
                    }
                }
            }
        }

        /// <summary>
        /// Create a new note object with some default settings 
        /// The default settings are note that can't be changed while editing a note.
        /// </summary>
        /// <param name="title">The title of the note.</param>
        /// <param name="skinnr">The skinnr</param>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="width">Width of the note</param>
        /// <param name="height">Height of the note</param>
        /// <param name="wordwarp">Is the note content word warped.</param>
        /// <returns>Note object</returns>
        public Note CreateNoteDefaultSettings(string title, int skinnr, int x, int y, int width, int height, bool wordwarp)
        {
            Note newnote = new Note(this, this.GetNoteFilename(title)); // set filename based on title
            newnote.Locked = false; // default
            newnote.RolledUp = false; // default
            newnote.Ontop = false; // default
            newnote.Visible = true; // default            
            newnote.Wordwarp = wordwarp;
            newnote.Title = title;
            newnote.SkinNr = skinnr;  
            newnote.X = x;
            newnote.Y = y;
            newnote.Width = width;
            newnote.Height = height;
            return newnote;
        }

        /// <summary>
        /// Generate a random skinnummer.
        /// </summary>
        /// <returns>A random skin number/position</returns>
        public int GenerateRandomSkinnr()
        {
            Random rndgen = new Random();
            return rndgen.Next(0, this.skins.Count);
        }

        /// <summary>
        /// Gets the highlight color.
        /// </summary>
        /// <param name="skinnr">The skin number</param>
        /// <returns>Color object</returns>
        public System.Drawing.Color GetHighlightClr(int skinnr)
        {
            return this.GetColor(3, skinnr);
        }

        /// <summary>
        /// Gets a note, by position in list
        /// </summary>
        /// <param name="pos">note position in list notes</param>
        /// <returns>The Note object</returns>
        public Note GetNote(int pos)
        {
            try
            {
                return this.notes[pos];
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ApplicationException("Can't get this note position: " + pos);
            }
        }

        /// <summary>
        /// Create a string used for filename of the note based on the
        /// title of the note limited to the first xx characters.
        /// </summary>
        /// <param name="title">The title of the note</param>
        /// <returns>A safe filename based on the title.</returns>
        public string GetNoteFilename(string title)
        {
            string title2 = this.StripForbiddenFilenameChars(title);
            string newfile;
            if (title2.Length > LIMITLENFILE)
            {
                newfile = title2.Substring(0, LIMITLENFILE) + NOTEEXTENSION;
            }
            else
            {
                newfile = title2 + NOTEEXTENSION;
            }

            if (File.Exists(Path.Combine(Settings.NotesSavepath, newfile)))
            {
                newfile = this.Checknewfilename(newfile, LIMITLENFILE, '#');
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
            return this.GetColor(1, skinnr);
        }

        /// <summary>
        /// Gets the selected color.
        /// </summary>
        /// <param name="skinnr">The skin number</param>
        /// <returns>The selected skin color.</returns>
        public System.Drawing.Color GetSelectClr(int skinnr)
        {
            return this.GetColor(2, skinnr);
        }

        /// <summary>
        /// Gets the primary texture for a skin.
        /// </summary>
        /// <param name="skinnr">The skin number</param>
        /// <returns>A bitmap</returns>
        public Bitmap GetPrimaryTexture(int skinnr)
        {
            Bitmap bitmaptexture = null;
            if (!string.IsNullOrEmpty(this.skins[skinnr].PrimaryTexture))
            {
                bitmaptexture = new Bitmap(this.skins[skinnr].PrimaryTexture);
            }

            return bitmaptexture;            
        }

        /// <summary>
        /// Get the primary texture image layout for a skin.
        /// </summary>
        /// <param name="skinnr">The skin number</param>
        /// <returns>The image layout how the note texture is used.</returns>
        public System.Windows.Forms.ImageLayout GetPrimaryTextureLayout(int skinnr)
        {
            return this.skins[skinnr].PrimaryTextureLayout;
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
        /// Gets the text color.
        /// </summary>
        /// <param name="skinnr">The skin number</param>
        /// <returns>Color object in text color.</returns>
        public System.Drawing.Color GetTextClr(int skinnr)
        {
            return this.GetColor(4, skinnr);
        }

        /// <summary>
        /// Loads all note files in the NotesSavepath.
        /// </summary>
        /// <param name="hasbeenfirstrun">true if it is the first run</param>
        /// <param name="resetpositions">true for reseting all the notes positions</param>
        public void LoadNotes(bool hasbeenfirstrun, bool resetpositions)
        {
            if (!Directory.Exists(Settings.NotesSavepath))
            {
                string notes_notefolderdoesnotexist = Gettext.Strings.T("Folder with notes does not exist.\r\nDo want to try loading notes from default application data folder?");
                string notes_notefolderdoesnotexisttitle = Gettext.Strings.T("Notes folder doesn't exist");
                DialogResult result = MessageBox.Show(notes_notefolderdoesnotexist, notes_notefolderdoesnotexisttitle, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.No)
                {
                    Log.Write(LogType.error, (notes_notefolderdoesnotexist + " No"));
                    return;
                }
                else
                {
                    Log.Write(LogType.error, (notes_notefolderdoesnotexist + " Yes"));
                    Settings.NotesSavepath = Program.AppDataFolder;
                }
            }

#if DEBUG
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
#endif
            string[] notefilespath = Directory.GetFiles(Settings.NotesSavepath, "*" + NOTEEXTENSION, SearchOption.TopDirectoryOnly);
            string[] notefiles = new string[notefilespath.Length];
            for (int i = 0; i < notefilespath.Length; i++)
            {
                notefiles[i] = Path.GetFileName(notefilespath[i]);
            }

            notefilespath = null;
#if DEBUG
            stopwatch.Stop();
            Log.Write(LogType.info, "Notes search time:  " + stopwatch.ElapsedTicks.ToString() + " ticks");
#endif
            int numloadingnotes = this.CheckLimitNotesTotal(notefiles.Length);
            this.notes.Capacity = numloadingnotes;
#if DEBUG
            stopwatch.Reset();
            stopwatch.Start();
#endif

            int numvisiblenotes = 0;
            bool warnlimitvisiblenotesshowed = false;
            const int MARGINBETWEENNOTES = 4;
            for (int i = 0; i < numloadingnotes; i++)
            {
                Note note = xmlUtil.LoadNoteFile(this, notefiles[i]);
                if (resetpositions)
                {
                    note.Y = 10;
                    note.X = 10 + (i * MARGINBETWEENNOTES);
                    if (note.X > System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height)
                    {
                        note.X = 10 + (i * MARGINBETWEENNOTES) - System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
                    }

                }

                this.AddNote(note);
                if (note.Visible)
                {
                    if (!warnlimitvisiblenotesshowed) 
                    {
                        warnlimitvisiblenotesshowed = this.CheckLimitNotesVisible(numvisiblenotes);
                    }

                    note.CreateForm();
                }
            }

#if DEBUG
            stopwatch.Stop();
            Log.Write(LogType.info, "Notes load time: " + stopwatch.ElapsedMilliseconds.ToString() + " ms");
#endif
            if (!hasbeenfirstrun)
            {
                this.ImportingNotesNoteFly1();
                this.CreateFirstrunNote();
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
                throw new ApplicationException("Cannot find note to remove.");
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
        public string StripForbiddenFilenameChars(string orgname)
        {
            System.Text.StringBuilder newfilename = new System.Text.StringBuilder();
            char[] forbiddenchars = Path.GetInvalidFileNameChars(); // "?<>:*|\\/\"".ToCharArray();
            bool isforbiddenchar = false;
            for (int pos = 0; pos < orgname.Length; pos++)
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
                curnote.UpdateNoteForm();
            }
        }

        /// <summary>
        /// Get the primary texture full file path.
        /// </summary>
        /// <param name="skinnr">The skin position</param>
        /// <returns>The full file path the texture file.</returns>
        public string GetPrimaryTextureFile(int skinnr)
        {
            return this.skins[skinnr].PrimaryTexture;
        }

        /// <summary>
        /// Reload all skins again.
        /// </summary>
        public void ReloadAllSkins()
        {
            this.skins.Clear();
            this.skins = xmlUtil.LoadSkins();
        }

        /// <summary>
        /// Gets the notes save directory.
        /// </summary>
        /// <returns>The directory where notes are saved.</returns>
        public string GetNotesSavepath()
        {
            return Settings.NotesSavepath;
        }

        /// <summary>
        /// Gets the full file path to the settings file.
        /// </summary>
        /// <returns>Settings file</returns>
        public string GetSettingsFile()
        {
            return Path.Combine(Program.AppDataFolder, xmlUtil.SETTINGSFILE);
        }

        /// <summary>
        /// Gets the skins file path skins file.
        /// </summary>
        /// <returns>The skins file full path.</returns>
        public string GetSkinsFile()
        {
            return Path.Combine(Program.AppDataFolder, xmlUtil.SKINFILE);
        }

        /// <summary>
        /// Log plugin info.
        /// </summary>
        /// <param name="infomsg">Info message</param>
        public void LogPluginInfo(string infomsg)
        {
            Log.Write(LogType.info, "plugin, " + infomsg);
        }

        /// <summary>
        /// Log plugin error.
        /// </summary>
        /// <param name="errormsg">Error message</param>
        public void LogPluginError(string errormsg)
        {
            Log.Write(LogType.error, "plugin, " + errormsg);
        }

        /// <summary>
        /// Ask and import notes from NoteFly 1.0.x if application data folder of NoteFly 1.0.x exist.
        /// </summary>
        private void ImportingNotesNoteFly1()
        {
#if windows
            string nf1appdata = Path.Combine(System.Environment.GetEnvironmentVariable("APPDATA"), ".NoteFly");
#elif linux
            string nf1appdata = Path.Combine(System.Environment.GetEnvironmentVariable("HOME"), ".NoteFly");
#endif
            if (Directory.Exists(nf1appdata) && (!File.Exists(Path.Combine(nf1appdata, IMPORTEDFLAGFILE))))
            {
                string notes_importtonf2 = Gettext.Strings.T("NoteFly 1.0.x detected.\nDo you want to import the notes from NoteFly 1.0.x to NoteFly 2.0.x?\nPress cancel to ask this again next time.");
                string notes_importtonf2title = Gettext.Strings.T("Import out NoteFly 1.0.x");
                DialogResult resdoimport = MessageBox.Show(notes_importtonf2, notes_importtonf2title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (resdoimport == DialogResult.Yes)
                {
                    string nf1settingsfile = Path.Combine(nf1appdata, "settings.xml");
                    string nf1notesavepath = xmlUtil.GetContentString(nf1settingsfile, "notesavepath");
                    int noteid = 1;
                    string nf1notefile = Path.Combine(nf1notesavepath, noteid + ".xml");
                    while (File.Exists(nf1notefile))
                    {
                        string nf1note_title = xmlUtil.GetContentString(nf1notefile, "title");
                        int nf1note_skinnr = xmlUtil.GetContentInt(nf1notefile, "color");
                        if (nf1note_skinnr >= this.skins.Count)
                        {
                            nf1note_skinnr = 0;
                        }

                        bool nf1note_visible = false;
                        if (xmlUtil.GetContentInt(nf1notefile, "visible") == 1)
                        {
                            nf1note_visible = true;
                        }

                        Note importnf1note = new Note(this, this.GetNoteFilename(nf1note_title));
                        importnf1note.Visible = nf1note_visible;
                        importnf1note.Title = nf1note_title;
                        importnf1note.SkinNr = nf1note_skinnr;
                        importnf1note.Ontop = false;
                        importnf1note.Locked = false;
                        importnf1note.X = xmlUtil.GetContentInt(nf1notefile, "x");
                        importnf1note.Y = xmlUtil.GetContentInt(nf1notefile, "y");
                        importnf1note.Width = xmlUtil.GetContentInt(nf1notefile, "width");
                        importnf1note.Height = xmlUtil.GetContentInt(nf1notefile, "heigth");
                        string content = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1043{\\fonttbl{\\f0\\fnil\\fcharset0 Verdana;}}\r\n\\viewkind4\\uc1\\pard\\f0\\fs20" + xmlUtil.GetContentString(nf1notefile, "content") + "\\ulnone\\par\r\n}\r\n";
                        xmlUtil.WriteNote(importnf1note, this.GetSkinName(nf1note_skinnr), content);
                        noteid++;
                        nf1notefile = Path.Combine(nf1notesavepath, noteid + ".xml");
                    }

                    this.CreateNF1NotesImportedFlagfile(nf1appdata);
                    Log.Write(LogType.info, "Notes from NoteFly 1.0.x imported.");
                }
                else if (resdoimport == DialogResult.No)
                {
                    this.CreateNF1NotesImportedFlagfile(nf1appdata);
                    Log.Write(LogType.info, "Notes from NoteFly 1.0.x have not been imported.");
                }
            }
        }

        /// <summary>
        /// Create a empty file so this newer NoteFly version knows the application data of NoteFly 1.0.x is imported.
        /// </summary>
        /// <param name="nf1path">Path to NoteFly1 application data folder.</param>
        private void CreateNF1NotesImportedFlagfile(string nf1appdata)
        {
            try
            {
                File.Create(Path.Combine(nf1appdata, IMPORTEDFLAGFILE));
            }
            catch
            {
                Log.Write(LogType.exception, "Could not set import flag in old notes directory.");
            }
        }

        /// <summary>
        /// This method set a limit on how many notes total can be loaded.
        /// </summary>
        /// <param name="number">The total number of notes.</param>
        /// <returns>The number of notes to load.</returns>
        private int CheckLimitNotesTotal(int totanumbernotes)
        {
            if (totanumbernotes > Settings.NotesWarnlimitTotal)
            {
                string notes_manynotesloadall = Gettext.Strings.T("There are many notes loading this can take a while, do you want to load them all?");
                string notes_manynotesloadalltitle = Gettext.Strings.T("Contine loading many notes?");
                DialogResult dlgres = MessageBox.Show(notes_manynotesloadall, notes_manynotesloadalltitle, MessageBoxButtons.YesNo);
                if (dlgres == DialogResult.No)
                {
                    totanumbernotes = Settings.NotesWarnlimitTotal;
                }
            }

            return totanumbernotes;
        }

        /// <summary>
        /// Check if NotesWarnlimitVisible is reached and display warning then.
        /// </summary>
        /// <param name="currentnumber">The number of notes that are currently visible.</param>
        /// <returns></returns>
        private bool CheckLimitNotesVisible(int currentnumber)
        {
            if (currentnumber > Settings.NotesWarnlimitVisible)
            {
                string notes_manynotesvisible = Gettext.Strings.T("There are many notes visible. Hide some notes to make loading faster.");
                string notes_manynotesvisibletitle = Gettext.Strings.T("Many notes visible");
                MessageBox.Show(notes_manynotesvisible, notes_manynotesvisibletitle, MessageBoxButtons.YesNo);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check if filename already exist if it is then generate a new one.
        /// </summary>
        /// <param name="newfile">The suggested new filename.</param>
        /// <param name="usedlimitlengthfile">The used file length limit.</param>
        /// <param name="sepchar">Seperator character.</param>
        /// <returns>Empty string on all used.</returns>
        private string Checknewfilename(string newfile, int usedlimitlengthfile, char sepchar)
        {
            int num = 1;
            int lenfilecounter = 3;
            int numlen = num.ToString(CultureInfo.InvariantCulture.NumberFormat).Length;
            while (File.Exists(Path.Combine(Settings.NotesSavepath, newfile)) && (numlen <= lenfilecounter))
            {
                numlen = num.ToString(CultureInfo.InvariantCulture.NumberFormat).Length;
                newfile = newfile.Substring(0, usedlimitlengthfile - numlen - 1) + sepchar + num + NOTEEXTENSION;
                num++;
            }

            if (numlen > lenfilecounter)
            {
                sepchar++;
                if (sepchar < 47)
                {
                    return this.Checknewfilename(newfile, usedlimitlengthfile, sepchar);
                }
                else
                {
                    Log.Write(LogType.exception, "All suggested filenames to save this note based on title seems to be taken.");
                    return string.Empty;
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
            const int DEMONOTEWIDTH = 260;
            const int DEMONOTEHEIGHT = 220;
            int noteposx = (Screen.PrimaryScreen.WorkingArea.Width / 2) - (DEMONOTEWIDTH / 2);
            int noteposy = (Screen.PrimaryScreen.WorkingArea.Height / 2) - (DEMONOTEHEIGHT / 2);
            string notecontent = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1043{\\fonttbl{\\f0\\fnil\\fcharset0 Verdana;}}\r\n\\viewkind4\\uc1\\pard\\f0\\fs20 This is a demo note.\\par\r\nPressing the [X] on a note\\par\r\n will \\b hide \\b0 that note.\\par\r\nTo actually \\i delete \\i0 it, use \\par\r\nthe \\i manage notes \\i0 windows\\par\r\n from the \\i trayicon\\i0 .\\ul\\par\r\n\\par\r\nThanks for using NoteFly!\\ulnone\\par\r\n}\r\n";
            Note demonote = this.CreateNoteDefaultSettings(Program.AssemblyTitle + " " + Program.AssemblyVersionAsString, 0, noteposx, noteposy, DEMONOTEWIDTH, DEMONOTEHEIGHT, true);
            xmlUtil.WriteNote(demonote, this.GetSkinName(demonote.SkinNr), notecontent);
            this.AddNote(demonote);
            demonote.CreateForm();
            demonote.BringNoteToFront();
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

            Log.Write(LogType.error, "Can't get color. type:" + type + " skinnr:" + skinnr);
            return Color.White;
        }

        /// <summary>
        /// Add a new note the the notes list.
        /// </summary>
        /// <param name="note">The note to be added.</param>
        private bool AddNote(Note note)
        {
            bool addsucceeded = false;
            int prevnumnotes = this.CountNotes;
            this.notes.Add(note);
            if (prevnumnotes != this.CountNotes)
            {
                addsucceeded = true;
            }

            return addsucceeded;
        }

        #endregion Methods
    }
}