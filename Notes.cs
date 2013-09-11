//-----------------------------------------------------------------------
// <copyright file="Notes.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2010-2013  Tom
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
        #region Fields (5)
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
        private const string IMPORTEDFLAGFILE = "impnf30.flg";

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
            this.LoadNotes(Settings.ProgramFirstrunned, resetpositions);
        }

        #endregion Constructors

        #region Properties (2)

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

        #endregion Properties

        #region Methods (21)

        /// <summary>
        /// Add a new note the the notes list.
        /// </summary>
        /// <param name="note">The note to be added.</param>
        /// <returns>True if succesfull added.</returns>
        public bool AddNote(Note note)
        {
            if (note == null)
            {
                return false;
            }

            this.notes.Add(note);
            return true;
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
        public Note AddNoteDefaultSettings(string title, int skinnr, int x, int y, int width, int height, string content, bool wordwarp)
        {
            Note note = this.CreateNoteDefaultSettings(title, skinnr, x, y, width, height, wordwarp);
            if (note != null)
            {
                this.AddNote(note);
                xmlUtil.WriteNote(note, this.GetSkinName(skinnr), content);
                note.CreateForm();
            }

            return note;
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
        /// <param name="pos">Note position in list notes</param>
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
            try
            {
                return this.skins[skinnr].PrimaryTextureLayout;
            }
            catch (IndexOutOfRangeException)
            {
                return this.skins[0].PrimaryTextureLayout;
            }
        }

        /// <summary>
        /// Get the name of a skin by the skinnr.
        /// </summary>
        /// <param name="skinnr">The skin number</param>
        /// <returns>The name of the skin, e.g. Yellow</returns>
        public string GetSkinName(int skinnr)
        {
            try
            {
                return this.skins[skinnr].Name;
            }
            catch (IndexOutOfRangeException)
            {
                return this.skins[0].Name;
            }
        }

        /// <summary>
        /// Get the skinnr that belongs by a name.
        /// If not found return 0 is return so first skin is used.
        /// </summary>
        /// <param name="skinname">The skin name.</param>
        /// <returns>The skinnr.</returns>
        public int GetSkinNr(string skinname)
        {
            for (int i = 0; i < this.skins.Count; i++)
            {
                if (this.skins[i].Name == skinname)
                {
                    return i;
                }
            }

            Log.Write(LogType.error, "SkinNr not found for skinname: " + skinname);
            return 0;
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
        /// <param name="hasbeenfirstrun">True if it is the first run.</param>
        /// <param name="resetpositions">True for reseting all the notes positions.</param>
        public void LoadNotes(bool hasbeenfirstrun, bool resetpositions)
        {
            if (!Directory.Exists(Settings.NotesSavepath))
            {
                string notes_notefolderdoesnotexist = Strings.T("Folder with notes does not exist.\nDo want to load notes from the default notes folder?");
                string notes_notefolderdoesnotexisttitle = Strings.T("Notes folder doesn't exist");
                DialogResult result = MessageBox.Show(notes_notefolderdoesnotexist, notes_notefolderdoesnotexisttitle, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.No)
                {
                    Log.Write(LogType.error, notes_notefolderdoesnotexist + " No");
                    return;
                }
                else
                {
                    Log.Write(LogType.error, notes_notefolderdoesnotexist + " Yes");
                    Settings.NotesSavepath = Program.GetDefaultNotesFolder();
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
            this.notes.Clear();
            int numloadingnotes = this.CheckLimitNotesTotal(notefiles.Length);
            this.notes.Capacity = numloadingnotes;
            #if DEBUG
            stopwatch.Reset();
            stopwatch.Start();
            #endif

            const int MARGINBETWEENNOTES = 4;
            for (int i = 0; i < numloadingnotes; i++)
            {
                Note note = xmlUtil.LoadNoteFile(this, notefiles[i]);
                if (note == null)
                {
                    continue;
                }

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
            }

            #if DEBUG
            stopwatch.Stop();
            Log.Write(LogType.info, "Notes load time: " + stopwatch.ElapsedMilliseconds.ToString() + " ms");
            #endif
            if (!hasbeenfirstrun)
            {
                this.ImportingNotesNoteFly2();
                this.ImportingNotesNoteFly1();
                this.CreateFirstrunNote();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ShowNotesVisible()
        {
            int numvisiblenotes = 0;
            bool warnlimitvisiblenotesshowed = false;
            foreach (Note note in this.notes) 
            {
                if (note.Visible)
                {
                    if (!warnlimitvisiblenotesshowed)
                    {
                        warnlimitvisiblenotesshowed = this.CheckLimitNotesVisible(numvisiblenotes);
                    }

                    note.CreateForm();
                }
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
        /// <param name="orgname">The string to strip forbidden filecharacters from.</param>
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
            try
            {
                return this.skins[skinnr].PrimaryTexture;
            }
            catch (IndexOutOfRangeException)
            {
                return this.skins[0].PrimaryTexture;
            }
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
        /// Gets the programme title. (for plugins)
        /// </summary>
        /// <returns>The title of the assembly.</returns>
        public string GetAssemblyTitle()
        {
            return Program.AssemblyTitle;
        }

        /// <summary>
        /// Get programme version.
        /// Method for plugins
        /// </summary>
        /// <returns>Get the assembly version as string</returns>
        public string GetAssemblyVersionAsString()
        {
            return Program.AssemblyVersionAsString;
        }

        /// <summary>
        /// Get a setting as boolean. (for plugins)
        /// </summary>
        /// <param name="settingname">The name of the setting.</param>
        /// <returns>A boolean setting.</returns>
        public bool GetSettingBool(string settingname)
        {
            object settingsptr = this.FindSettings(settingname);
            if (settingsptr != null)
            {
                if (settingsptr is bool)
                {
                    return (bool)settingsptr;
                }
                else
                {
                    Log.Write(LogType.exception, "plugin, requested setting not a boolean, return false for " + settingname);
                    return false;
                }
            }
            else
            {
                Log.Write(LogType.exception, "plugin, requested setting not found.");
                return false;
            }
        }

        /// <summary>
        /// Get a setting as integer. (for plugins)
        /// </summary>
        /// <param name="settingname">The name of the setting.</param>
        /// <returns>An integer setting</returns>
        public int GetSettingInt(string settingname)
        {
            object settingsptr = this.FindSettings(settingname);
            if (settingsptr != null)
            {
                if (settingsptr is int)
                {
                    return (int)settingsptr;
                }
                else
                {
                    Log.Write(LogType.exception, "plugin, requested setting not a integer, return -1 for " + settingname);
                    return -1;
                }
            }
            else
            {
                Log.Write(LogType.exception, "plugin, requested setting not found.");
                return -1;
            }
        }

        /// <summary>
        /// Get a setting as float. (for plugins)
        /// </summary>
        /// <param name="settingname">The name of the setting.</param>
        /// <returns>An float setting</returns>
        public float GetSettingFloat(string settingname)
        {
            object settingsptr = this.FindSettings(settingname);
            if (settingsptr != null)
            {
                if (settingsptr is float)
                {
                    return (float)settingsptr;
                }
                else
                {
                    Log.Write(LogType.exception, "plugin, requested setting not a float, return -1 for " + settingname);
                    return -1;
                }
            }
            else
            {
                Log.Write(LogType.exception, "plugin, requested setting not found.");
                return -1;
            }
        }

        /// <summary>
        /// Get a setting as string. (for plugins)
        /// </summary>
        /// <param name="settingname">The name of the setting.</param>
        /// <returns>An string setting.</returns>
        public string GetSettingString(string settingname)
        {
            object settingsptr = this.FindSettings(settingname);
            if (settingsptr != null)
            {
                if (settingsptr is string)
                {
                    return (string)settingsptr;
                }
                else
                {
                    Log.Write(LogType.exception, "plugin, requested setting not a string, return null for " + settingname);
                    return null;
                }
            }
            else
            {
                Log.Write(LogType.exception, "plugin, requested setting not found.");
                return null;
            }
        }

        /// <summary>
        /// Find a setting in the setting class. (for plugins)
        /// </summary>
        /// <param name="settingname">The name of the setting.</param>
        /// <returns>Return the setting valeau as object</returns>
        private object FindSettings(string settingname)
        {
            Type type = typeof(Settings);
            System.Reflection.FieldInfo[] fields = type.GetFields();

            // Loop through all fields
            foreach (var field in fields) 
            {
                if (field.Name.Equals(settingname, StringComparison.OrdinalIgnoreCase))
                {
                    object settingsptr = field.GetValue(null);
                    return settingsptr;
                }
            }

            return null;
        }

        /// <summary>
        /// Ask and import notes from NoteFly 1.0.x if application data folder of NoteFly 1.0.x exist.
        /// </summary>
        /// <returns>True if imported done, false if importing notefly 1.0.x notes not nessesary.</returns>
        private bool ImportingNotesNoteFly1()
        {
            bool imported = false;
            string nf1appdata = string.Empty;
            if (Program.CurrentOS == Program.OS.WINDOWS)
            {
                nf1appdata = Path.Combine(System.Environment.GetEnvironmentVariable("APPDATA"), ".NoteFly");
            }
            else if (Program.CurrentOS == Program.OS.LINUX)
            {
                nf1appdata = Path.Combine(System.Environment.GetEnvironmentVariable("HOME"), ".NoteFly");
            }

            if (Directory.Exists(nf1appdata) && (!File.Exists(Path.Combine(nf1appdata, IMPORTEDFLAGFILE))))
            { 
                DialogResult resdoimport = MessageBox.Show(Strings.T(
                    "Do you want to import the notes from NoteFly 1.0.x?\nPress cancel to ask this again next time."),
                    Strings.T("Import from NoteFly 1.0.x"),
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);
                if (resdoimport == DialogResult.Yes)
                {
                    string nf1settingsfile = Path.Combine(nf1appdata, "settings.xml");
                    string nf1notesavepath = xmlUtil.GetContentString(nf1settingsfile, "notesavepath");
                    int noteid = 1;
                    ImportNotes importnotes = new ImportNotes(this);
                    string nf1notefile = Path.Combine(nf1notesavepath, noteid + ".xml");
                    while (File.Exists(nf1notefile))
                    {
                        nf1notefile = Path.Combine(nf1notesavepath, noteid + ".xml");
                        importnotes.ReadNoteFly1Note(nf1notefile);
                        noteid++;
                    }

                    this.CreateNotesImportedFlagfile(nf1appdata);
                    imported = true;
                    Log.Write(LogType.info, "Notes from NoteFly 1.0.x imported.");
                }
                else if (resdoimport == DialogResult.No)
                {
                    this.CreateNotesImportedFlagfile(nf1appdata);
                    Log.Write(LogType.info, "Notes from NoteFly 1.0.x will not be imported.");
                }
            }

            return imported;
        }

        /// <summary>
        /// Create a empty file so this newer NoteFly version knows the application data of NoteFly 1.0.x is imported.
        /// </summary>
        /// <param name="folder">Path to NoteFly 1.0.x application data folder.</param>
        private void CreateNotesImportedFlagfile(string folder)
        {
            try
            {
                File.Create(Path.Combine(folder, IMPORTEDFLAGFILE));
            }
            catch
            {
                Log.Write(LogType.exception, "Could not set import flag in old notes directory.");
            }
        }

        /// <summary>
        /// Clear all notes from memory of notefly.
        /// </summary>
        public void ClearAllNotes()
        {
            while (this.CountNotes > 0)
            {
                try
                {
                    this.GetNote(0).DestroyForm();
                    this.RemoveNote(0);
                }
                catch (Exception)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Import NoteFly 2.5.x/2.0.x notes.
        /// to be move to importnotes class.
        /// </summary>
        /// <returns>True if already imported.</returns>
        private bool ImportingNotesNoteFly2()
        {
            bool imported = false;
            string nf2appdata = string.Empty;
            if (Program.CurrentOS == Program.OS.WINDOWS)
            {
                nf2appdata = Path.Combine(System.Environment.GetEnvironmentVariable("APPDATA"), ".NoteFly2");
            }
            else if (Program.CurrentOS == Program.OS.LINUX)
            {
                nf2appdata = Path.Combine(System.Environment.GetEnvironmentVariable("HOME"), ".NoteFly2");
            }

            if (Directory.Exists(nf2appdata) && (!File.Exists(Path.Combine(nf2appdata, IMPORTEDFLAGFILE))))
            {
                DialogResult resdlg = MessageBox.Show(Strings.T("Do you want to import the notes from NoteFly 2.5.x?\nPress cancel to ask this again next time."), Strings.T("Import from NoteFly 2.5.x"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (resdlg == DialogResult.Yes)
                {
                    string nf2settingsfile = Path.Combine(nf2appdata, "settings.xml");
                    string nf2notessavepath = xmlUtil.GetContentString(nf2settingsfile, "NotesSavepath");
                    if (Directory.Exists(nf2notessavepath))
                    {
                        const string NF2_NOTEEXTENSION = ".nfn";
                        string[] notefilespath = Directory.GetFiles(nf2notessavepath, "*" + NF2_NOTEEXTENSION, SearchOption.TopDirectoryOnly);

                        // import notes by simple copy them over since note file format is not changed in notefly 3.0.x
                        for (int i = 0; i < notefilespath.Length; i++)
                        {
                            string filename = Path.GetFileName(notefilespath[i]);
                            string notefilepath = Path.Combine(Settings.NotesSavepath, filename);
                            if (!File.Exists(notefilepath) && !string.IsNullOrEmpty(filename))
                            {
                                File.Copy(notefilespath[i], notefilepath);
                            }
                        }

                        Log.Write(LogType.info, "Done importing notes from NoteFly 2.5.x");
                        this.LoadNotes(true, false);
                        this.CreateNotesImportedFlagfile(nf2appdata);
                    }
                }
                else if (resdlg == DialogResult.No)
                {
                    this.CreateNotesImportedFlagfile(nf2appdata);
                }
            }
            else
            {
                imported = true;
            }

            return imported;
        }

        /// <summary>
        /// This method set a limit on how many notes total can be loaded.
        /// </summary>
        /// <param name="totanumbernotes">The total number of notes.</param>
        /// <returns>The number of notes to load.</returns>
        private int CheckLimitNotesTotal(int totanumbernotes)
        {
            if (totanumbernotes > Settings.NotesWarnlimitTotal)
            {
                string notes_manynotesloadalltitle = Strings.T("many notes");
                string notes_manynotesloadall = Strings.T("There are many notes loading this can take a while, do you want to load them all?");
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
        /// <returns>True if warning limit is exceeded.</returns>
        private bool CheckLimitNotesVisible(int currentnumber)
        {
            if (currentnumber > Settings.NotesWarnlimitVisible)
            {
                string notes_manynotesvisibletitle = Strings.T("Many notes visible");
                string notes_manynotesvisible = Strings.T("There are many notes visible.\nHide some notes to make loading faster.");
                MessageBox.Show(notes_manynotesvisible, notes_manynotesvisibletitle, MessageBoxButtons.YesNo);
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
        /// Create a demo note with instruction .
        /// Don't create demo note again if a file with same filename already exist.
        /// (Should be the first time that NoteFly is runned displayed only.)
        /// </summary>
        private void CreateFirstrunNote()
        {
            string title = Program.AssemblyTitle + " " + Program.AssemblyVersionAsString;
            string filename;
            if (title.Length > LIMITLENFILE)
            {
                filename = title.Substring(0, LIMITLENFILE) + NOTEEXTENSION;
            }
            else
            {
                filename = title.Substring(0, title.Length) + NOTEEXTENSION;
            }

            if (!File.Exists(Path.Combine(Settings.NotesSavepath, filename)))
            {
                const int DEMONOTEWIDTH = 260;
                const int DEMONOTEHEIGHT = 220;
                int noteposx = (Screen.PrimaryScreen.WorkingArea.Width / 2) - (DEMONOTEWIDTH / 2);
                int noteposy = (Screen.PrimaryScreen.WorkingArea.Height / 2) - (DEMONOTEHEIGHT / 2);
                string notecontent = "{\\rtf1\\ansi\\ansicpg1252\\deff0{\\fonttbl{\\f0\\fnil\\fcharset0 Verdana;}}\r\n\\viewkind4\\uc1\\pard\\f0\\fs20 This is a demo note.\\par\r\nPressing the [X] on a note\\par\r\n will \\b hide \\b0 that note.\\par\r\nTo actually \\i delete \\i0 it, use \\par\r\nthe \\i manage notes \\i0 windows\\par\r\n from the \\i trayicon\\i0 .\\ul\\par\r\n\\par\r\nThanks for using NoteFly!\\ulnone\\par\r\n}\r\n";
                Note demonote = this.CreateNoteDefaultSettings(title, 0, noteposx, noteposy, DEMONOTEWIDTH, DEMONOTEHEIGHT, true);
                xmlUtil.WriteNote(demonote, this.GetSkinName(demonote.SkinNr), notecontent);
                this.AddNote(demonote);
            }
            else
            {
                Log.Write(LogType.info, "First demo already exist do not create.");
            }
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
        /// Get install folder of program 
        /// Wrapper method for plugins.
        /// </summary>
        /// <returns></returns>
        public string GetInstallFolder()
        {
            return Program.InstallFolder;
        }

        /// <summary>
        /// Get operating system this program is running on
        /// Wrapper method for plugins.
        /// </summary>
        /// <returns>Enum value as string.</returns>
        public string GetOS()
        {
            string os = Enum.GetName(Program.CurrentOS.GetType(), Program.CurrentOS);
            return os;
        }

        #endregion Methods
    }
}