//-----------------------------------------------------------------------
// <copyright file="XmlUtil.cs" company="GNU">
//  NoteFly a note application.
//  Copyright (C) 2010-2011  Tom
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
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Xml;

    /// <summary>
    /// xmlUtil class, for saving and parsering xml.
    /// </summary>
    public sealed class xmlUtil
    {
        #region Fields (5)

        /// <summary>
        /// The note version
        /// </summary>
        private const string NOTEVERSION = "2";

        /// <summary>
        /// Settings file.
        /// </summary>
        private const string SETTINGSFILE = "settings.xml";

        /// <summary>
        /// Skin file.
        /// </summary>
        private const string SKINFILE = "skins.xml";

        /// <summary>
        /// XmlTextReader object.
        /// </summary>
        private static XmlTextReader xmlread = null;

        /// <summary>
        /// XmlTextWriter object.
        /// </summary>
        private static XmlTextWriter xmlwrite = null;

        #endregion Fields

        #region Methods (11)

        /// <summary>
        /// Get a xml node and return the value as string.
        /// </summary>
        /// <param name="filename">The filename and path to search in.</param>
        /// <param name="nodename">The node name to lookup.</param>
        /// <returns>return node content as string, empty if not found</returns>
        public static string GetContentString(string filename, string nodename)
        {
#if DEBUG
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
#endif
            try
            {
                xmlread = new XmlTextReader(filename);
            }
            catch (FileLoadException fileloadexc)
            {
                throw new ApplicationException(fileloadexc.Message);
            }
            catch (FileNotFoundException filenotfoundexc)
            {
                throw new ApplicationException(filenotfoundexc.Message);
            }

            if (xmlread == null)
            {
                throw new ApplicationException("XmlTextReader object is null.");
            }

            try
            {
                while (xmlread.Read())
                {
                    if (xmlread.Name == nodename)
                    {
                        string xmlnodecontent = string.Empty;
                        xmlnodecontent = xmlread.ReadElementContentAsString();
#if DEBUG
                        stopwatch.Stop();
                        Log.Write(LogType.info, "Read content time:  " + stopwatch.ElapsedTicks + " ticks"); // blocking display time ~200ms/7
#endif
                        return xmlnodecontent;
                    }
                }
            }
            finally
            {
                xmlread.Close();
            }
#if DEBUG
            stopwatch.Stop();
#endif
            return string.Empty;
        }

        /// <summary>
        /// Get a xml node and return the value as integer.
        /// </summary>
        /// <param name="filename">The filename and path to search in.</param>
        /// <param name="nodename">The node name to lookup.</param>
        /// <returns>A integer value</returns>
        public static int GetContentInt(string filename, string nodename)
        {
            try
            {
                xmlread = new XmlTextReader(filename);
            }
            catch (FileLoadException fileloadexc)
            {
                throw new ApplicationException(fileloadexc.Message);
            }
            catch (FileNotFoundException filenotfoundexc)
            {
                throw new ApplicationException(filenotfoundexc.Message);
            }

            if (xmlread == null)
            {
                throw new ApplicationException("XmlTextReader object is null.");
            }

            try
            {
                while (xmlread.Read())
                {
                    if (xmlread.Name == nodename)
                    {
                        int xmlnodecontent = xmlread.ReadElementContentAsInt();
                        return xmlnodecontent;
                    }
                }
            }
            finally
            {
                xmlread.Close();
            }

            return 0;
        }

        /// <summary>
        /// Load a note file.
        /// </summary>
        /// <param name="notes">reference to notes class.</param>
        /// <param name="notefilename">The note filename.</param>
        /// <returns>An note object.</returns>
        public static Note LoadNoteFile(Notes notes, string notefilename)
        {
            Note note = new Note(notes, notefilename);
            xmlread = new XmlTextReader(Path.Combine(Settings.NotesSavepath, notefilename));
            xmlread.ProhibitDtd = true;
            try
            {
                note = ParserNoteNode(notes, note, 0, false);
            }
            finally
            {
                xmlread.Close();
            }

            return note;
        }

        /// <summary>
        /// Loads the settings file and set the settings in the
        /// static Settings class in memory.
        /// </summary>
        /// <returns>true if file settings exists.</returns>
        public static bool LoadSettings()
        {
            string settingsfilepath = Path.Combine(Program.AppDataFolder, SETTINGSFILE);
            if (!File.Exists(settingsfilepath))
            {
                return false;
            }

            try
            {
                xmlread = new XmlTextReader(settingsfilepath);
                xmlread.EntityHandling = EntityHandling.ExpandCharEntities;
                xmlread.ProhibitDtd = true; // gives decreated warning in vs2010.
                while (xmlread.Read())
                {
                    switch (xmlread.Name)
                    {
                        // booleans
                        case "ConfirmDeletenote":
                            Settings.ConfirmDeletenote = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "ConfirmExit":
                            Settings.ConfirmExit = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "ConfirmLinkclick":
                            Settings.ConfirmLinkclick = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "FontTitleStylebold":
                            Settings.FontTitleStylebold = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "HighlightHTML":
                            Settings.HighlightHTML = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "HighlightHyperlinks":
                            Settings.HighlightHyperlinks = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "HighlightPHP":
                            Settings.HighlightPHP = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "HighlightSQL":
                            Settings.HighlightSQL = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "NetworkProxyEnabled":
                            Settings.NetworkProxyEnabled = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "NotesTooltipEnabled":
                            Settings.NotesTooltipsEnabled = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "NotesClosebtnHidenotepermanently":
                            Settings.NotesClosebtnHidenotepermanently = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "NotesDefaultRandomSkin":
                            Settings.NotesDefaultRandomSkin = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "NotesDeleteRecyclebin":
                            Settings.NotesDeleteRecyclebin = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "NotesTransparencyEnabled":
                            Settings.NotesTransparencyEnabled = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "NotesTransparentRTB":
                            Settings.NotesTransparentRTB = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "ProgramFirstrun":
                            Settings.ProgramFirstrun = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "ProgramLogError":
                            Settings.ProgramLogError = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "ProgramLogException":
                            Settings.ProgramLogException = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "ProgramLogInfo":
                            Settings.ProgramLogInfo = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "ProgramPluginsAllEnabled":
                            Settings.ProgramPluginsAllEnabled = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "ProgramSuspressWarnAdmin":
                            Settings.ProgramSuspressWarnAdmin = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "SocialEmailEnabled":
                            Settings.SocialEmailEnabled = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "TrayiconCreatenotebold":
                            Settings.TrayiconCreatenotebold = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "TrayiconExitbold":
                            Settings.TrayiconExitbold = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "TrayiconManagenotesbold":
                            Settings.TrayiconManagenotesbold = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "TrayiconSettingsbold":
                            Settings.TrayiconSettingsbold = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "UpdatecheckUseGPG":
                            Settings.UpdatecheckUseGPG = xmlread.ReadElementContentAsBoolean();
                            break;

                        // ints and doubles
                        case "TrayiconFontsize":
                            Settings.TrayiconFontsize = xmlread.ReadElementContentAsFloat();
                            break;
                        case "FontContentSize":
                            Settings.FontContentSize = xmlread.ReadElementContentAsInt();
                            break;
                        case "FontTextdirection":
                            Settings.FontTextdirection = xmlread.ReadElementContentAsInt();
                            break;
                        case "FontTitleSize":
                            Settings.FontTitleSize = xmlread.ReadElementContentAsInt();
                            break;
                        case "NetworkConnectionTimeout":
                            Settings.NetworkConnectionTimeout = xmlread.ReadElementContentAsInt();
                            break;
                        case "NotesDefaultSkinnr":
                            Settings.NotesDefaultSkinnr = xmlread.ReadElementContentAsInt();
                            break;
                        case "NotesWarnLimit":
                            Settings.NotesWarnLimit = xmlread.ReadElementContentAsInt();
                            break;
                        case "NotesTransparencyLevel":
                            Settings.NotesTransparencyLevel = xmlread.ReadElementContentAsDouble();
                            break;
                        case "TrayiconLeftclickaction":
                            Settings.TrayiconLeftclickaction = xmlread.ReadElementContentAsInt();
                            break;
                        case "UpdatecheckEverydays":
                            Settings.UpdatecheckEverydays = xmlread.ReadElementContentAsInt();
                            break;
                        case "HighlightMaxchars":
                            Settings.HighlightMaxchars = xmlread.ReadElementContentAsInt();
                            break;

                        // strings (put at bottom in the settings file for more performance because then there are less characters to compare&skip)
                        case "HighlightHTMLColorComment":
                            Settings.HighlightHTMLColorComment = xmlread.ReadElementContentAsString();
                            break;
                        case "HighlightHTMLColorInvalid":
                            Settings.HighlightHTMLColorInvalid = xmlread.ReadElementContentAsString();
                            break;
                        case "HighlightHTMLColorValid":
                            Settings.HighlightHTMLColorValid = xmlread.ReadElementContentAsString();
                            break;
                        case "HighlightHTMLColorString":
                            Settings.HighlightHTMLColorString = xmlread.ReadElementContentAsString();
                            break;
                        case "HighlightPHPColorComment":
                            Settings.HighlightPHPColorComment = xmlread.ReadElementContentAsString();
                            break;
                        case "HighlightPHPColorDocumentstartend":
                            Settings.HighlightPHPColorDocumentstartend = xmlread.ReadElementContentAsString();
                            break;
                        case "HighlightPHPColorInvalidfunctions":
                            Settings.HighlightPHPColorInvalidfunctions = xmlread.ReadElementContentAsString();
                            break;
                        case "HighlightPHPColorValidfunctions":
                            Settings.HighlightPHPColorValidfunctions = xmlread.ReadElementContentAsString();
                            break;
                        case "HighlightSQLColorValidstatement":
                            Settings.HighlightSQLColorValidstatement = xmlread.ReadElementContentAsString();
                            break;
                        case "HighlightSQLColorField":
                            Settings.HighlightSQLColorField = xmlread.ReadElementContentAsString();
                            break;
                        case "FontContentFamily":
                            Settings.FontContentFamily = xmlread.ReadElementContentAsString();
                            break;
                        case "ProgramPluginsFolder":
                            Settings.ProgramPluginsFolder = xmlread.ReadElementContentAsString();
                            break;
                        case "ProgramPluginsEnabled":
                            Settings.ProgramPluginsEnabled = xmlread.ReadElementContentAsString();
                            break;
                        case "FontTitleFamily":
                            Settings.FontTitleFamily = xmlread.ReadElementContentAsString();
                            break;
                        case "NetworkProxyAddress":
                            Settings.NetworkProxyAddress = xmlread.ReadElementContentAsString();
                            break;
                        case "NotesSavepath":
                            Settings.NotesSavepath = xmlread.ReadElementContentAsString();
                            break;
                        case "UpdatecheckGPGPath":
                            Settings.UpdatecheckGPGPath = xmlread.ReadElementContentAsString();
                            break;
                        case "UpdatecheckLastDate":
                            Settings.UpdatecheckLastDate = xmlread.ReadElementContentAsString();
                            break;
                        case "UpdatecheckURL":
                            Settings.UpdatecheckURL = xmlread.ReadElementContentAsString();
                            break;
                    }

                    if (xmlread.Depth > 8)
                    {
                        break;
                    }
                }
            }
            finally
            {
                xmlread.Close();
            }

            return true;
        }

        /// <summary>
        /// Gets all skins from skin file.
        /// create Application data folder if does not exist.
        /// Create default SKINFILE if not exist.
        /// </summary>
        /// <returns>A list of skins objects</returns>
        public static List<Skin> LoadSkins()
        {
            if (!Directory.Exists(Program.AppDataFolder))
            {
                Directory.CreateDirectory(Program.AppDataFolder);
            }

            string skinfilepath = Path.Combine(Program.AppDataFolder, SKINFILE);
            if (!File.Exists(skinfilepath))
            {
                Log.Write(LogType.info, "writing default skins.xml");
                WriteDefaultSkins(skinfilepath);
            }

            List<Skin> skins = new List<Skin>();
            xmlread = new XmlTextReader(skinfilepath);
            xmlread.ProhibitDtd = true;
            Skin curskin = null;
            int numskins = 0;
            bool endtag = false;
            while (xmlread.Read())
            {
                switch (xmlread.Name)
                {
                    case "skins":
                        if (xmlread.HasAttributes)
                        {
                            skins.Capacity = Convert.ToInt32(xmlread.GetAttribute("count"));
                        }

                        break;
                    case "skin":
                        if (endtag)
                        {
                            if (curskin != null && numskins < 255)
                            {
                                skins.Add(curskin);
                            }
                        }
                        else if (!endtag)
                        {
                            numskins++;
                            curskin = new Skin();
                        }

                        endtag = !endtag;
                        break;
                    case "Name":
                        curskin.Name = xmlread.ReadElementContentAsString();
                        break;
                    case "PrimaryClr":
                        if (xmlread.HasAttributes)
                        {
                            string filepathtexture = xmlread.GetAttribute("texture");
                            if (System.IO.File.Exists(filepathtexture) || System.IO.File.Exists(Path.Combine(Program.InstallFolder,filepathtexture)) )
                            {
                                string extension = filepathtexture.Substring(filepathtexture.LastIndexOf('.'), filepathtexture.Length - filepathtexture.LastIndexOf('.')).ToLower();
                                string[] supportedimageformats = new string[] { ".png", ".tif", ".tiff", ".bmp", ".gif", ".jpg", ".jpeg" };
                                bool imagesupported = false;
                                for (int i = 0; i < supportedimageformats.Length; i++)
                                {
                                    if (extension == supportedimageformats[i])
                                    {
                                        imagesupported = true;
                                        break;
                                    }
                                }

                                if (imagesupported)
                                {
                                    if (System.IO.File.Exists(filepathtexture))
                                    {
                                        curskin.PrimaryTexture = new System.Drawing.Bitmap(filepathtexture);
                                    }
                                    else
                                    {
                                        curskin.PrimaryTexture = new System.Drawing.Bitmap(Path.Combine(Program.InstallFolder, filepathtexture));
                                    }
                                }
                                else
                                {
                                    const string TEXTUREIMGFORMUNSUPPORTED = "Texture image format not supported.";
                                    Log.Write(LogType.error, TEXTUREIMGFORMUNSUPPORTED);
                                }
                            }
                            else
                            {
                                Log.Write(LogType.error, "texture not be found " + filepathtexture + "");
                            }
                        }

                        curskin.PrimaryClr = ConvToClr(xmlread.ReadElementContentAsString());
                        break;
                    case "SelectClr":
                        curskin.SelectClr = ConvToClr(xmlread.ReadElementContentAsString());
                        break;
                    case "HighlightClr":
                        curskin.HighlightClr = ConvToClr(xmlread.ReadElementContentAsString());
                        break;
                    case "TextClr":
                        curskin.TextClr = ConvToClr(xmlread.ReadElementContentAsString());
                        break;
                }

                if (xmlread.Depth > 3)
                {
                    const string SKINFILECORRUPT = "Skin file corrupted: ";
                    throw new ApplicationException(SKINFILECORRUPT + SKINFILE);
                }
            }

            return skins;
        }

        /// <summary>
        /// Write the default settings.
        /// Used for if SETTINGSFILE is not created yet.
        /// </summary>
        /// <returns>True if writing settings succeeded otherwise false.</returns>
        public static bool WriteDefaultSettings()
        {
            Settings.ConfirmDeletenote = true;
            Settings.ConfirmExit = false;
            Settings.ConfirmLinkclick = true;
#if windows
            Settings.FontContentFamily = "Arial";
            Settings.FontTitleFamily = "Arial";
#elif linux
            Settings.FontContentFamily = "FreeMono";
            Settings.FontTitleFamily = "FreeMono";
#else
            Settings.FontContentFamily = "?";
            Settings.FontTitleFamily = "?";
#endif
            Settings.FontContentSize = 11;
            Settings.FontTextdirection = 0;
            Settings.FontTitleSize = 14;
            Settings.FontTitleStylebold = true;
            Settings.HighlightMaxchars = 10000;
            Settings.HighlightHTML = false;
            Settings.HighlightHTMLColorComment = "#B200FF";
            Settings.HighlightHTMLColorInvalid = "#FF0000";
            Settings.HighlightHTMLColorValid = "#0026FF";
            Settings.HighlightHTMLColorString = "#808080";
            Settings.HighlightHyperlinks = true;
            Settings.HighlightPHP = false;
            Settings.HighlightPHPColorComment = "#686868";
            Settings.HighlightPHPColorDocumentstartend = "#129612";
            Settings.HighlightPHPColorValidfunctions = "#41D87B";
            Settings.HighlightPHPColorInvalidfunctions = "#D90000";
            Settings.HighlightSQL = false;
            Settings.HighlightSQLColorValidstatement = "#7FCE35";
            Settings.HighlightSQLColorField = "#B16DFF";
            Settings.NetworkConnectionTimeout = 8000;
            Settings.NetworkConnectionForceipv6 = false;
            Settings.NetworkProxyAddress = string.Empty;
            Settings.NetworkProxyEnabled = false;
            Settings.NotesTooltipsEnabled = true;
            Settings.NotesClosebtnHidenotepermanently = true;
            Settings.NotesDefaultRandomSkin = false;
            Settings.NotesDefaultSkinnr = 0; // default skin: yellow
            Settings.NotesSavepath = Program.AppDataFolder;
            Settings.NotesTransparencyEnabled = true;
            Settings.NotesTransparentRTB = true;
            Settings.NotesTransparencyLevel = 0.9;
            Settings.NotesWarnLimit = 250;
            Settings.ProgramFirstrun = true;
            Settings.ProgramLogError = true;
            Settings.ProgramLogException = true;
            Settings.ProgramLogInfo = false;
            Settings.ProgramPluginsAllEnabled = true;
            Settings.ProgramSuspressWarnAdmin = false;
            Settings.ProgramPluginsFolder = Path.Combine(Program.InstallFolder, "plugins");
            Settings.SocialEmailEnabled = true;
            Settings.SocialEmailDefaultadres = string.Empty;
            Settings.TrayiconFontsize = 8.25f;
            Settings.TrayiconLeftclickaction = 1;
            Settings.TrayiconCreatenotebold = true;
            Settings.TrayiconExitbold = false;
            Settings.TrayiconManagenotesbold = false;
            Settings.TrayiconSettingsbold = false;
            Settings.UpdatecheckEverydays = 14; // 0 is disabled.
            Settings.UpdatecheckLastDate = DateTime.Now.ToString();
            Settings.UpdatecheckURL = "http://update.notefly.org/latestversion.xml";
            try
            {
                xmlUtil.WriteSettings();
                xmlUtil.CheckFile(Path.Combine(Program.AppDataFolder, SETTINGSFILE));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Write a note xml file.
        /// </summary>
        /// <param name="note">The note object.</param>
        /// <param name="skinname">The skin name of the note.</param>
        /// <param name="content">The note content.</param>
        /// <returns>True on succeeded otherwise false.</returns>
        public static bool WriteNote(Note note, string skinname, string content)
        {
            bool succeeded = false;
            try
            {
                xmlwrite = new System.Xml.XmlTextWriter(Path.Combine(Settings.NotesSavepath, note.Filename), System.Text.Encoding.UTF8);
                xmlwrite.Formatting = System.Xml.Formatting.Indented;
                xmlwrite.WriteStartDocument(true); // standalone xml file.
                WriteNoteBody(note, skinname, content);
                xmlwrite.WriteEndDocument();
                succeeded = true;
            }
            catch
            {
                succeeded = false;
            }
            finally
            {
                xmlwrite.Flush();
                xmlwrite.Close();
            }

            return succeeded;
        }

        /// <summary>
        /// Write a file with all notes as backup.
        /// </summary>
        /// <param name="filenamepath">The filename and path of the notes backup</param>
        /// <param name="notes">Reference to the notes class.</param>
        /// <returns>True if writing backup succeeded otherwise false.</returns>
        public static bool WriteNoteFlyNotesBackupFile(string filenamepath, Notes notes)
        {
            bool succeeded = false;
            try
            {
                xmlwrite = new XmlTextWriter(filenamepath, System.Text.Encoding.UTF8);
                xmlwrite.Formatting = Formatting.Indented;
                xmlwrite.WriteStartDocument(true); // standalone xml file.
                xmlwrite.WriteStartElement("backupnotes");
                xmlwrite.WriteAttributeString("number", notes.CountNotes.ToString());
                for (int i = 0; i < notes.CountNotes; i++)
                {
                    string skinname = notes.GetSkinName(notes.GetNote(i).SkinNr);
                    string content = notes.GetNote(i).GetContent();
                    WriteNoteBody(notes.GetNote(i), skinname, content);
                }

                xmlwrite.WriteEndElement();
                xmlwrite.WriteEndDocument();
                succeeded = true;
            }
            finally
            {
                xmlwrite.Close();
            }

            return succeeded;
        }

        /// <summary>
        /// Read all notes from a notes backup file and return a array with all the notes.
        /// </summary>
        /// <param name="notes">Reference to notes class.</param>
        /// <param name="filepath">The notes backup filename and path.</param>
        public static void ReadNoteFlyNotesBackupFile(Notes notes, string filepath)
        {
            int numnotes = 0;
            xmlread = null;
            try
            {
                xmlread = new XmlTextReader(filepath);
                xmlread.ProhibitDtd = true;
                bool endnode = false;
                while (xmlread.Read())
                {
                    if (xmlread.Name == "note")
                    {
                        if (!endnode)
                        {
                            numnotes++;
                        }

                        endnode = !endnode;
                    }
                }
            }
            finally
            {
                if (xmlread != null)
                {
                    xmlread.Close();
                }
            }

            for (int i = 0; i < numnotes; i++)
            {
                xmlread = new XmlTextReader(filepath);
                xmlread.ProhibitDtd = true;
                Note importnote = new Note(notes, notes.GetNoteFilename("import" + i));
                try
                {
                    importnote = ParserNoteNode(notes, importnote, i, true);
                }
                finally
                {
                    if (xmlread != null)
                    {
                        xmlread.Close();
                    }
                }

                string skinname = notes.GetSkinName(importnote.SkinNr);
                WriteNote(importnote, skinname, importnote.Tempcontent);
            }
        }

        /// <summary>
        /// Write settings file.
        /// </summary>
        /// <returns>true if succeed.</returns>
        public static bool WriteSettings()
        {
            NumberFormatInfo numfmtinfo = CultureInfo.InvariantCulture.NumberFormat;
            try
            {
                xmlwrite = new XmlTextWriter(Path.Combine(Program.AppDataFolder, SETTINGSFILE), System.Text.Encoding.UTF8);
                xmlwrite.Formatting = Formatting.Indented;
                xmlwrite.WriteStartDocument(true); // standalone document
                xmlwrite.WriteStartElement("settings");

                // booleans
                WriteXMLBool("ConfirmDeletenote", Settings.ConfirmDeletenote);
                WriteXMLBool("ConfirmExit", Settings.ConfirmExit);
                WriteXMLBool("ConfirmLinkclick", Settings.ConfirmLinkclick);
                WriteXMLBool("FontTitleStylebold", Settings.FontTitleStylebold);
                WriteXMLBool("HighlightHTML", Settings.HighlightHTML);
                WriteXMLBool("HighlightHyperlinks", Settings.HighlightHyperlinks);
                WriteXMLBool("HighlightPHP", Settings.HighlightPHP);
                WriteXMLBool("HighlightSQL", Settings.HighlightSQL);
                WriteXMLBool("NetworkProxyEnabled", Settings.NetworkProxyEnabled);
                WriteXMLBool("NotesTooltipEnabled", Settings.NotesTooltipsEnabled);
                WriteXMLBool("NotesClosebtnHidenotepermanently", Settings.NotesClosebtnHidenotepermanently);
                WriteXMLBool("NotesDeleteRecyclebin", Settings.NotesDeleteRecyclebin);
                WriteXMLBool("NotesTransparencyEnabled", Settings.NotesTransparencyEnabled);
                WriteXMLBool("NotesTransparentRTB", Settings.NotesTransparentRTB);
                WriteXMLBool("NotesDefaultRandomSkin", Settings.NotesDefaultRandomSkin);
                WriteXMLBool("ProgramFirstrun", Settings.ProgramFirstrun);
                WriteXMLBool("ProgramLogError", Settings.ProgramLogError);
                WriteXMLBool("ProgramLogException", Settings.ProgramLogException);
                WriteXMLBool("ProgramLogInfo", Settings.ProgramLogInfo);
                WriteXMLBool("ProgramPluginsAllEnabled", Settings.ProgramPluginsAllEnabled);
                WriteXMLBool("ProgramSuspressWarnAdmin", Settings.ProgramSuspressWarnAdmin);
                WriteXMLBool("SocialEmailEnabled", Settings.SocialEmailEnabled);
                WriteXMLBool("TrayiconCreatenotebold", Settings.TrayiconCreatenotebold);
                WriteXMLBool("TrayiconExitbold", Settings.TrayiconExitbold);
                WriteXMLBool("TrayiconManagenotesbold", Settings.TrayiconManagenotesbold);
                WriteXMLBool("TrayiconSettingsbold", Settings.TrayiconSettingsbold);
                WriteXMLBool("UpdatecheckUseGPG", Settings.UpdatecheckUseGPG);
                
                // integers
                xmlwrite.WriteElementString("FontTextdirection", Settings.FontTextdirection.ToString(numfmtinfo));
                xmlwrite.WriteElementString("FontContentSize", Settings.FontContentSize.ToString(numfmtinfo));
                xmlwrite.WriteElementString("FontTitleSize", Settings.FontTitleSize.ToString(numfmtinfo));
                xmlwrite.WriteElementString("NetworkConnectionTimeout", Settings.NetworkConnectionTimeout.ToString(numfmtinfo));
                xmlwrite.WriteElementString("NotesDefaultSkinnr", Settings.NotesDefaultSkinnr.ToString(numfmtinfo));
                xmlwrite.WriteElementString("NotesTransparencyLevel", Settings.NotesTransparencyLevel.ToString(numfmtinfo));
                xmlwrite.WriteElementString("NotesWarnLimit", Settings.NotesWarnLimit.ToString(numfmtinfo));
                xmlwrite.WriteElementString("TrayiconFontsize", Settings.TrayiconFontsize.ToString(numfmtinfo));
                xmlwrite.WriteElementString("TrayiconLeftclickaction", Settings.TrayiconLeftclickaction.ToString(numfmtinfo));
                xmlwrite.WriteElementString("UpdatecheckEverydays", Settings.UpdatecheckEverydays.ToString(numfmtinfo));
                xmlwrite.WriteElementString("HighlightMaxchars", Settings.HighlightMaxchars.ToString(numfmtinfo));

                // strings
                xmlwrite.WriteElementString("HighlightHTMLColorComment", Settings.HighlightHTMLColorComment);
                xmlwrite.WriteElementString("HighlightHTMLColorInvalid", Settings.HighlightHTMLColorInvalid);
                xmlwrite.WriteElementString("HighlightHTMLColorValid", Settings.HighlightHTMLColorValid);
                xmlwrite.WriteElementString("HighlightHTMLColorString", Settings.HighlightHTMLColorString);
                xmlwrite.WriteElementString("HighlightPHPColorComment", Settings.HighlightPHPColorComment);
                xmlwrite.WriteElementString("HighlightPHPColorDocumentstartend", Settings.HighlightPHPColorDocumentstartend);
                xmlwrite.WriteElementString("HighlightPHPColorInvalidfunctions", Settings.HighlightPHPColorInvalidfunctions);
                xmlwrite.WriteElementString("HighlightPHPColorValidfunctions", Settings.HighlightPHPColorValidfunctions);
                xmlwrite.WriteElementString("HighlightSQLColorValidstatement", Settings.HighlightSQLColorValidstatement);
                xmlwrite.WriteElementString("HighlightSQLColorField", Settings.HighlightSQLColorField);
                xmlwrite.WriteElementString("UpdatecheckGPGPath", Settings.UpdatecheckGPGPath);
                xmlwrite.WriteElementString("UpdatecheckLastDate", Settings.UpdatecheckLastDate.ToString());
                xmlwrite.WriteElementString("UpdatecheckURL", Settings.UpdatecheckURL.ToString());
                xmlwrite.WriteElementString("FontContentFamily", Settings.FontContentFamily);
                xmlwrite.WriteElementString("FontTitleFamily", Settings.FontTitleFamily);
                xmlwrite.WriteElementString("ProgramPluginsFolder", Settings.ProgramPluginsFolder);
                xmlwrite.WriteElementString("ProgramPluginsEnabled", Settings.ProgramPluginsEnabled);
                xmlwrite.WriteElementString("NetworkProxyAddress", Settings.NetworkProxyAddress);
                xmlwrite.WriteElementString("SocialEmailDefaultadres", Settings.SocialEmailDefaultadres);
                xmlwrite.WriteElementString("NotesSavepath", Settings.NotesSavepath);
                xmlwrite.WriteEndElement();
                xmlwrite.WriteEndDocument();
            }
            catch (AccessViolationException)
            {
                Log.Write(LogType.exception, "Premission problem writing: " + Path.Combine(Program.AppDataFolder, SETTINGSFILE));
                return false;
            }
            finally
            {
                xmlwrite.Close();
            }

            return true;
        }

        /// <summary>
        /// Convert HEX color to color object.
        /// </summary>
        /// <param name="colorstring">Hex color as string.</param>
        /// <returns>Color object.</returns>
        public static System.Drawing.Color ConvToClr(string colorstring)
        {
            // HEX color
            return System.Drawing.ColorTranslator.FromHtml(colorstring);

            // DECIMAL color, commented out in favor of HEX notation for speed.
            ////string[] parts = new string[3];
            ////parts = colorstring.Split(',');
            ////try
            ////{
            ////    UInt8 redchannel = Convert.ToUInt16(parts[0].Trim());
            ////    UInt8 greenchannel = Convert.ToUInt16(parts[1].Trim());
            ////    UInt8 bluechannel = Convert.ToUInt16(parts[2].Trim());
            ////    return System.Drawing.Color.FromArgb(redchannel, greenchannel, bluechannel);
            ////}
            ////catch
            ////{
            ////    if (colorstring.Length < 100)
            ////    {
            ////        throw new CustomException("Cannot parser: " + colorstring);
            ////    }
            ////    else
            ////    {
            ////        throw new CustomException("Cannot parser: " + colorstring.Substring(0, 100)+" ..");
            ////    }
            ////}
        }

        /// <summary>
        /// Get the new version as a integer array with first major
        /// second valeau the minor version and the third valeau being the release version.
        /// </summary>
        /// <param name="versionquality">The latest version quality, e.g: alpha, beta, rc or nothing for final.</param>
        /// <param name="downloadurl">the download url found</param>
        /// <returns>the newest version as integer array, 
        /// any negative valeau(-1 by default) considered as error.</returns>
        public static short[] GetLatestVersion(out string versionquality, out string downloadurl)
        {
            short[] version = new short[3];
            version[0] = -1;
            version[1] = -1;
            version[2] = -1;
            versionquality = Program.AssemblyVersionQuality;
            downloadurl = "http://www.notefly.org/"; // default url if none is provided.
            try
            {
                System.Net.ServicePointManager.Expect100Continue = false;
                System.Net.ServicePointManager.DefaultConnectionLimit = 1;
                if (string.IsNullOrEmpty(Settings.UpdatecheckURL))
                {
                    Log.Write(LogType.exception, "No UpdatecheckURL found in settings");
                    return version;
                }

                if (Settings.NetworkConnectionForceipv6)
                {
                    Settings.UpdatecheckURL = Settings.UpdatecheckURL.Replace("//update.", "//ipv6."); // not replacing "http", "https", "ftp"
                    Settings.UpdatecheckURL = Settings.UpdatecheckURL.Replace("//www.", "//ipv6.");
                }

                if (!Uri.IsWellFormedUriString(Settings.UpdatecheckURL, UriKind.Absolute))
                {
                    Log.Write(LogType.error, "Invalid update uri.");
                }


                HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(Settings.UpdatecheckURL);
                request.Method = "GET";
                request.ContentType = "text/xml";
                request.UserAgent = Program.AssemblyTitle + " " + Program.AssemblyVersionAsString;
                request.Timeout = Settings.NetworkConnectionTimeout;
                if (Settings.NetworkProxyEnabled && !string.IsNullOrEmpty(Settings.NetworkProxyAddress))
                {
                    request.Proxy = new WebProxy(Settings.NetworkProxyAddress);
                }

                request.Headers["Accept-Encoding"] = "gzip";
                request.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore); // do not cache, prevent incorrect cache result.
                request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.None;
                Stream responsestream;
                using (WebResponse response = request.GetResponse())
                {
                    responsestream = response.GetResponseStream();
                    xmlread = new XmlTextReader(responsestream);
                    xmlread.ProhibitDtd = true;
                    while (xmlread.Read())
                    {
                        switch (xmlread.Name)
                        {
                            case "major":
                                try
                                {
                                    version[0] = Convert.ToInt16(xmlread.ReadElementContentAsInt());
                                }
                                catch (OverflowException)
                                {
                                    version[0] = -1;
                                }

                                break;
                            case "minor":
                                try
                                {
                                    version[1] = Convert.ToInt16(xmlread.ReadElementContentAsInt());
                                }
                                catch (OverflowException)
                                {
                                    version[1] = -1;
                                }

                                break;
                            case "release":
                                try
                                {
                                    version[2] = Convert.ToInt16(xmlread.ReadElementContentAsInt());
                                }
                                catch (OverflowException)
                                {
                                    version[2] = -1;
                                }

                                break;
                            case "quality":
                                string getquality = xmlread.ReadElementContentAsString().Trim();
                                if (getquality.Length < 16)
                                {
                                    versionquality = getquality;
                                }

                                break;
                            case "downloadurl":
                                string downloadurlraw = xmlread.ReadElementContentAsString().Trim();
                                if ((downloadurlraw.Length > 10) && (downloadurlraw.Length < 512))
                                {
                                    downloadurl = downloadurlraw;
                                }

                                break;
                            default:

                                break;
                        }

                        if (xmlread.Depth > 3)
                        {
                            xmlread.Close();
                        }
                    }

                    responsestream.Close();
                }
            }
            catch (System.Net.WebException webexc)
            {
                Log.Write(LogType.exception, "update check, " + webexc.Message);
            }
            finally
            {
                if (xmlread != null)
                {
                    xmlread.Close();
                }
            }

            Log.Write(LogType.info, "update check done.");
            return version;
        }

        /// <summary>
        /// Return a array of keywords used for the prgramming language we are doing a syntax check on.
        /// </summary>
        /// <param name="file">the file to parser.</param>
        /// <param name="name">the language to lookup.</param>
        /// <returns>HighlightLanguage object.</returns>
        public static HighlightLanguage ParserLanguageLexical(string file, string name)
        {
            string[] keywords = null;
            string commentline = null;
            string commentstart = null;
            string commentend = null;
            string documentstart = null;
            string documentend = null;
            try
            {
                xmlread = new XmlTextReader(Path.Combine(Program.InstallFolder, file));
                xmlread.ProhibitDtd = true;
                bool readsubnodes = false;
                while (xmlread.Read())
                {
                    if (xmlread.Name == "Language" || readsubnodes)
                    {
                        if (xmlread.GetAttribute("name") == name || readsubnodes)
                        {
                            if (!readsubnodes)
                            {
                                commentline = xmlread.GetAttribute("commentLine");
                                commentstart = xmlread.GetAttribute("commentStart");
                                commentend = xmlread.GetAttribute("commentEnd");
                                documentstart = xmlread.GetAttribute("documentStart");
                                documentend = xmlread.GetAttribute("documentEnd");
                            }

                            readsubnodes = true;
                            if (xmlread.Name == "Keywords")
                            {
                                keywords = xmlread.ReadElementContentAsString().Split(' ');
                                readsubnodes = false;
                                break;
                            }
                        }
                        else
                        {
                            xmlread.Skip();
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Log.Write(LogType.exception, "File " + file + " not found.");
                Settings.HighlightHTML = false;
                Settings.HighlightPHP = false;
                Settings.HighlightSQL = false;
            }
            finally
            {
                xmlread.Close();
            }

            HighlightLanguage language = new HighlightLanguage(name, commentline, commentstart, commentend, documentstart, documentend, keywords);
            return language;
        }

        /// <summary>
        /// Parser a note node in a xml file, 
        /// readnotenum is the number of note node to be parser and returned as note object.
        /// </summary>
        /// <param name="notes">pointer to notes</param>
        /// <param name="note">the note object to set</param>
        /// <param name="readnotenum">The number occurance of the note node to be parser (first, sencod etc.)</param>
        /// <param name="setallcontent">Force to set the note temporary content variable in the note class even if not visible.</param>
        /// <returns>a note object</returns>
        private static Note ParserNoteNode(Notes notes, Note note, int readnotenum, bool setallcontent)
        {
            int curnotenum = -1;
            bool endnode = false;
            while (xmlread.Read())
            {
                if (xmlread.Name != string.Empty)
                {
                    switch (xmlread.Name)
                    {
                        case "note":
                            if (!endnode)
                            {
                                curnotenum++;
                            }

                            if (curnotenum < readnotenum)
                            {
                                xmlread.Skip();
                            }
                            else if (curnotenum > readnotenum)
                            {
                                return note;
                            }
                            else
                            {
                                endnode = !endnode;
                            }

                            break;
                        case "visible":
                            note.Visible = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "locked":
                            note.Locked = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "ontop":
                            note.Ontop = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "rollup":
                            note.RolledUp = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "width":
                            note.Width = xmlread.ReadElementContentAsInt();
                            break;
                        case "heigth":
                            note.Height = xmlread.ReadElementContentAsInt();
                            break;
                        case "x":
                            note.X = xmlread.ReadElementContentAsInt();
                            break;
                        case "y":
                            note.Y = xmlread.ReadElementContentAsInt();
                            break;
                        case "skin":
                            int skinnr = notes.GetSkinNr(xmlread.ReadElementContentAsString());
                            if (skinnr >= 0)
                            {
                                note.SkinNr = skinnr;
                            }

                            break;
                        case "title":
                            note.Title = xmlread.ReadElementContentAsString();
                            break;
                        case "content":
                            if (note.Visible || setallcontent)
                            {
                                note.Tempcontent = xmlread.ReadElementContentAsString();
                            }

                            break;
                    }

                    if (xmlread.Depth > 5)
                    {
                        throw new ApplicationException("note file corrupted");
                    }
                }
            }

            return note;
        }

        /// <summary>
        /// Writes the default skins to the SKINFILE.
        /// Used for if SKINFILE is not created yet.
        /// </summary>
        /// <param name="filename">The skins file filename and path</param>
        private static void WriteDefaultSkins(string filename)
        {
            try
            {
                xmlwrite = new XmlTextWriter(filename, System.Text.Encoding.UTF8);
                xmlwrite.Formatting = Formatting.Indented;
                xmlwrite.WriteStartDocument(true); // standalone xml file
                xmlwrite.WriteStartElement("skins");
                const int NUMDEFAULTSKINS = 15;
                xmlwrite.WriteAttributeString("count", NUMDEFAULTSKINS.ToString()); // for performance predefine list Capacity, not required.
                string[] name = new string[NUMDEFAULTSKINS] { "yellow", "orange", "white", "green", "blue", "purple", "red", "dark", "softwhite", "contrastblue", "nyancat", "hellokitty", "grass", "blackhorse", "colordrops" };
                string[] primaryclr = new string[NUMDEFAULTSKINS] { "FFEF14", "FFA700", "FFFFFF", "6FE200", "5A86D5", "FF1AFF", "FF1A1A", "002626", "FFF4C6", "3B47EF", "0019A8", "FF359A", "6FE200", "7E2603", "002626" };
                string[] selectclr = new string[NUMDEFAULTSKINS] { "E0D616", "C17D00", "E0E0E0", "008000", "1A1AFF", "8B1A8B", "7A1515", "000624", "333366", "00137F", "0019A8", "FF35F0", "008000", "000624", "000624" };
                string[] highlightclr = new string[NUMDEFAULTSKINS] { "FFED7C", "FFD46D", "E5E5E5", "DADBD9", "C6CBD3", "FFC1FF", "FF6F6F", "494949", "FFFFFF", "0026FF", "000000", "FFFFFF", "DADBD9", "494949", "494949" };
                string[] textclr = new string[NUMDEFAULTSKINS] { "000000", "000000", "000000", "000000", "000000", "000000", "000000", "FFFFFF", "3B47EF", "FFDF23", "FFFFFF", "000000", "000000", "FFFFFF", "FFFFFF" };
                string[] textures = new string[NUMDEFAULTSKINS] { null, null, null, null, null, null, null, null, null, null, "nyancat.jpg", "hellokitty.jpg", "grass.jpg", "blackhorse.jpg", "colordrops.jpg" };
                for (ushort i = 0; i < NUMDEFAULTSKINS; i++)
                {
                    xmlwrite.WriteStartElement("skin");
                    xmlwrite.WriteElementString("Name", name[i]);
                    xmlwrite.WriteStartElement("PrimaryClr");
                    if (textures[i] != null)
                    {
                        xmlwrite.WriteAttributeString("texture", textures[i]);
                    }
                    xmlwrite.WriteString("#" + primaryclr[i]);
                    xmlwrite.WriteEndElement();
                    xmlwrite.WriteElementString("SelectClr", "#" + selectclr[i]);
                    xmlwrite.WriteElementString("HighlightClr", "#" + highlightclr[i]);
                    xmlwrite.WriteElementString("TextClr", "#" + textclr[i]);
                    xmlwrite.WriteEndElement();
                }

                xmlwrite.WriteEndElement();
                xmlwrite.WriteEndDocument();
            }
            finally
            {
                xmlwrite.Close();
            }

            xmlUtil.CheckFile(filename);
        }

        /// <summary>
        /// Checks if filesize is not 0 bytes for a filename 
        /// </summary>
        /// <param name="filename">The filename to check</param>
        private static void CheckFile(string filename)
        {
            FileInfo fi = new FileInfo(filename);
            if (fi.Length <= 0)
            {
                Log.Write(LogType.exception, filename + " is an empty file. File can be corrupted.");
            }
        }

        /// <summary>
        /// Write 1 value for true and 0 for false.
        /// </summary>
        /// <param name="element">The element name</param>
        /// <param name="checknode">The node to check</param>
        private static void WriteXMLBool(string element, bool checknode)
        {
            xmlwrite.WriteStartElement(element);
            if (checknode)
            {
                xmlwrite.WriteString("1");
            }
            else
            {
                xmlwrite.WriteString("0");
            }

            xmlwrite.WriteEndElement();
        }

        /// <summary>
        /// Write the note node with properties.
        /// </summary>
        /// <param name="note">The note object.</param>
        /// <param name="skinname">The skinname used by this note.</param>
        /// <param name="content">The note content.</param>
        private static void WriteNoteBody(Note note, string skinname, string content)
        {
            xmlwrite.WriteStartElement("note");
            xmlwrite.WriteAttributeString("version", NOTEVERSION);
            WriteXMLBool("visible", note.Visible);
            WriteXMLBool("ontop", note.Ontop);
            WriteXMLBool("locked", note.Locked);
            WriteXMLBool("rollup", note.RolledUp);
            xmlwrite.WriteStartElement("location");
            xmlwrite.WriteElementString("x", note.X.ToString());
            xmlwrite.WriteElementString("y", note.Y.ToString());
            xmlwrite.WriteEndElement();
            xmlwrite.WriteStartElement("size");
            xmlwrite.WriteElementString("width", Convert.ToString(note.Width));
            xmlwrite.WriteElementString("heigth", Convert.ToString(note.Height));
            xmlwrite.WriteEndElement();
            xmlwrite.WriteElementString("skin", skinname);
            xmlwrite.WriteElementString("title", note.Title);
            xmlwrite.WriteElementString("content", content);
            xmlwrite.WriteEndElement();
        }

        #endregion Methods
    }
}