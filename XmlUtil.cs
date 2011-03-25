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
#define windows //platform can be: windows, linux, macos

namespace NoteFly
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Windows.Forms;
    using System.Xml;

    /// <summary>
    /// xmlUtil class, for saving and parsering xml.
    /// </summary>
    public class xmlUtil
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
        /// The programme update check url.
        /// </summary>
        private const string UPDATEURL = "http://www.notefly.tk/latestversion.xml";

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

        // Public Methods (8) 

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
                        string xmlnodecontent = String.Empty;
                        xmlnodecontent = xmlread.ReadElementContentAsString();
#if DEBUG
                        stopwatch.Stop();
                        Log.Write(LogType.info, "Read content time:  " + stopwatch.ElapsedTicks + " ticks"); //blocking display time ~200ms/7
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
            return String.Empty;
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
        /// <param name="notefilepath">The note filename and path.</param>
        /// <returns>An note object.</returns>
        public static Note LoadNoteFile(Notes notes, string notefilename)
        {
            Note note = new Note(notes, notefilename);
            xmlread = new XmlTextReader(Path.Combine(Settings.notesSavepath, notefilename));
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
                xmlread.ProhibitDtd = true; //gives decreated warning in vs2010.
                while (xmlread.Read())
                {
                    switch (xmlread.Name)
                    {
                        //booleans
                        case "ConfirmDeletenote":
                            Settings.confirmDeletenote = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "ConfirmExit":
                            Settings.confirmExit = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "ConfirmLinkclick":
                            Settings.confirmLinkclick = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "FontTitleStylebold":
                            Settings.fontTitleStylebold = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "HighlightHTML":
                            Settings.highlightHTML = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "HighlightHyperlinks":
                            Settings.highlightHyperlinks = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "HighlightPHP":
                            Settings.highlightPHP = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "HighlightSQL":
                            Settings.highlightSQL = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "NetworkConnectionForceipv6":
                            Settings.networkConnectionForceipv6 = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "NetworkProxyEnabled":
                            Settings.networkProxyEnabled = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "NotesTooltipEnabled":
                            Settings.notesTooltipsEnabled = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "NotesClosebtnHidenotepermanently":
                            Settings.notesClosebtnHidenotepermanently = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "NotesDeleteRecyclebin":
                            Settings.notesDeleteRecyclebin = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "NotesTransparencyEnabled":
                            Settings.notesTransparencyEnabled = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "ProgramFirstrun":
                            Settings.programFirstrun = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "ProgramLogError":
                            Settings.programLogError = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "ProgramLogException":
                            Settings.programLogException = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "ProgramLogInfo":
                            Settings.programLogInfo = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "SocialEmailEnabled":
                            Settings.socialEmailEnabled = xmlread.ReadElementContentAsBoolean();
                            break;
                        /*
                        case "SocialFacebookEnabled":
                            Settings.socialFacebookEnabled = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "SocialFacebookUseSSL":
                            Settings.socialFacebookUseSSL = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "SocialTwitterEnabled":
                            Settings.socialTwitterEnabled = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "SocialTwitterUseSSL":
                            Settings.socialTwitterUseSSL = xmlread.ReadElementContentAsBoolean();
                            break;
                        */
                        case "TrayiconFontsize":
                            Settings.trayiconFontsize = xmlread.ReadElementContentAsFloat();
                            break;
                        case "TrayiconCreatenotebold":
                            Settings.trayiconCreatenotebold = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "TrayiconExitbold":
                            Settings.trayiconExitbold = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "TrayiconManagenotesbold":
                            Settings.trayiconManagenotesbold = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "TrayiconSettingsbold":
                            Settings.trayiconSettingsbold = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "NotesDefaultRandomSkin":
                            Settings.notesDefaultRandomSkin = xmlread.ReadElementContentAsBoolean();
                            break;
                        //ints / doubles
                        case "FontContentSize":
                            Settings.fontContentSize = xmlread.ReadElementContentAsInt();
                            break;
                        case "FontTextdirection":
                            Settings.fontTextdirection = xmlread.ReadElementContentAsInt();
                            break;
                        case "FontTitleSize":
                            Settings.fontTitleSize = xmlread.ReadElementContentAsInt();
                            break;
                        case "NetworkConnectionTimeout":
                            Settings.networkConnectionTimeout = xmlread.ReadElementContentAsInt();
                            break;
                        case "DefaultRandomSkin":
                            Settings.notesDefaultRandomSkin = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "NotesDefaultSkinnr":
                            Settings.notesDefaultSkinnr = xmlread.ReadElementContentAsInt();
                            break;
                        case "NotesWarnLimit":
                            Settings.notesWarnLimit = xmlread.ReadElementContentAsInt();
                            break;
                        case "NotesTransparencyLevel":
                            Settings.notesTransparencyLevel = xmlread.ReadElementContentAsDouble();
                            break;
                        case "TrayiconLeftclickaction":
                            Settings.trayiconLeftclickaction = xmlread.ReadElementContentAsInt();
                            break;
                        case "UpdatecheckEverydays":
                            Settings.updatecheckEverydays = xmlread.ReadElementContentAsInt();
                            break;
                        //strings (put at bottom in the settings file for more performance because then there are less characters to compare&skip)
                        case "HighlightHTMLColorInvalid":
                            Settings.highlightHTMLColorInvalid = xmlread.ReadElementContentAsString();
                            break;
                        case "HighlightHTMLColorValid":
                            Settings.highlightHTMLColorValid = xmlread.ReadElementContentAsString();
                            break;
                        case "HighlightHTMLColorString":
                            Settings.highlightHTMLColorString = xmlread.ReadElementContentAsString();
                            break;
                        case "HighlightPHPColorComment":
                            Settings.highlightPHPColorComment = xmlread.ReadElementContentAsString();
                            break;
                        case "HighlightPHPColorDocumentstartend":
                            Settings.highlightPHPColorDocumentstartend = xmlread.ReadElementContentAsString();
                            break;
                        case "HighlightPHPColorInvalidfunctions":
                            Settings.highlightPHPColorInvalidfunctions = xmlread.ReadElementContentAsString();
                            break;
                        case "HighlightPHPColorValidfunctions":
                            Settings.highlightPHPColorValidfunctions = xmlread.ReadElementContentAsString();
                            break;
                        case "FontContentFamily":
                            Settings.fontContentFamily = xmlread.ReadElementContentAsString();
                            break;
                        case "FontTitleFamily":
                            Settings.fontTitleFamily = xmlread.ReadElementContentAsString();
                            break;
                        case "NetworkProxyAddress":
                            Settings.networkProxyAddress = xmlread.ReadElementContentAsString();
                            break;
                        case "NotesSavepath":
                            Settings.notesSavepath = xmlread.ReadElementContentAsString();
                            break;
                        /*
                        case "SocialEmailDefaultadres":
                            Settings.socialEmailDefaultadres = xmlread.ReadElementContentAsString();
                            break;
                        case "SocialTwitterUsername":
                            Settings.socialTwitterUsername = xmlread.ReadElementContentAsString();
                            break;
                        case "SocialFacebookEmail":
                            Settings.socialFacebookEmail = xmlread.ReadElementContentAsString();
                            break;
                         */
                        case "UpdatecheckLastDate":
                            Settings.updatecheckLastDate = xmlread.ReadElementContentAsString();
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
                    throw new ApplicationException("Skin file corrupted: " + SKINFILE);
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
            Settings.confirmDeletenote = true;
            Settings.confirmExit = false;
            Settings.confirmLinkclick = true;
            Settings.fontContentFamily = "Arial";
            Settings.fontContentSize = 11;
            Settings.fontTextdirection = 0;
            Settings.fontTitleFamily = "Arial";
            Settings.fontTitleSize = 14;
            Settings.fontTitleStylebold = true;
            Settings.highlightHTML = false;
            Settings.highlightHTMLColorInvalid = "#FF0000";
            Settings.highlightHTMLColorValid = "#0026FF";
            Settings.highlightHTMLColorString = "#808080";
            Settings.highlightHyperlinks = true;
            Settings.highlightPHP = false;
            Settings.highlightPHPColorComment = "#333333";
            Settings.highlightPHPColorDocumentstartend = "#129612";
            Settings.highlightPHPColorValidfunctions = "#41D87B";
            Settings.highlightPHPColorInvalidfunctions = "#D90000";
            Settings.highlightSQL = false;
            Settings.networkConnectionForceipv6 = false;
            Settings.networkConnectionTimeout = 8000;
            Settings.networkProxyAddress = String.Empty;
            Settings.networkProxyEnabled = false;
            Settings.notesTooltipsEnabled = true;
            Settings.notesClosebtnHidenotepermanently = true;
            Settings.notesDefaultRandomSkin = false;
            Settings.notesDefaultSkinnr = 0; //default skin: yellow
            Settings.notesSavepath = Program.AppDataFolder;
            Settings.notesTransparencyEnabled = true;
            Settings.notesTransparencyLevel = 0.9;
            Settings.notesWarnLimit = 200;
            Settings.programFirstrun = true;
            Settings.programLogError = true;
            Settings.programLogException = true;
            Settings.programLogInfo = false;
            Settings.socialEmailDefaultadres = String.Empty;
            Settings.socialEmailEnabled = true;
            //Settings.socialFacebookEmail = String.Empty;
            //Settings.socialFacebookEnabled = true;
            //Settings.socialFacebookUseSSL = true;
            //Settings.socialTwitterUsername = String.Empty;
            //Settings.socialTwitterEnabled = true;
            //Settings.socialTwitterUseSSL = true;
            Settings.trayiconFontsize = 8.25f;
            Settings.trayiconLeftclickaction = 1;
            Settings.trayiconCreatenotebold = true;
            Settings.trayiconExitbold = false;
            Settings.trayiconManagenotesbold = false;
            Settings.trayiconSettingsbold = false;
            Settings.updatecheckEverydays = 14; //0 is disabled.
            Settings.updatecheckLastDate = DateTime.Now.ToString();
            try
            {
                xmlUtil.WriteSettings();
                xmlUtil.CheckFile(Path.Combine(Program.AppDataFolder, SETTINGSFILE), 2473);
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
                xmlwrite = new System.Xml.XmlTextWriter(Path.Combine(Settings.notesSavepath, note.Filename), System.Text.Encoding.UTF8);
                xmlwrite.Formatting = System.Xml.Formatting.Indented;
                xmlwrite.WriteStartDocument(true); //standalone
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
        public static bool WriteNotesBackupFile(string filenamepath, Notes notes)
        {
            bool succeeded = false;
            try
            {
                xmlwrite = new XmlTextWriter(filenamepath, System.Text.Encoding.UTF8);
                xmlwrite.Formatting = Formatting.Indented;
                xmlwrite.WriteStartDocument(true); //standalone
                xmlwrite.WriteStartElement("backupnotes");
                xmlwrite.WriteAttributeString("number", notes.CountNotes.ToString());
                for (int i = 0; i < notes.CountNotes; i++)
                {
                    string skinname = notes.GetSkinName(notes.GetNote(i).skinNr);
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
        public static void LoadNotesBackup(Notes notes, string filepath)
        {
            xmlread = new XmlTextReader(filepath);
            xmlread.ProhibitDtd = true;
            int numnotes = 0;
            bool endnode = false;
            try
            {
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
                xmlread.Close();
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
                    xmlread.Close();
                }
                string skinname = notes.GetSkinName(importnote.skinNr);
                WriteNote(importnote, skinname, importnote.tempcontent);
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
                xmlwrite.WriteStartDocument(true); //standalone
                xmlwrite.WriteStartElement("settings");
                //bools
                WriteXMLBool("ConfirmDeletenote", Settings.confirmDeletenote);
                WriteXMLBool("ConfirmExit", Settings.confirmExit);
                WriteXMLBool("ConfirmLinkclick", Settings.confirmLinkclick);
                WriteXMLBool("FontTitleStylebold", Settings.fontTitleStylebold);
                WriteXMLBool("HighlightHTML", Settings.highlightHTML);
                WriteXMLBool("HighlightHyperlinks", Settings.highlightHyperlinks);
                WriteXMLBool("HighlightPHP", Settings.highlightPHP);
                WriteXMLBool("HighlightSQL", Settings.highlightSQL);
                WriteXMLBool("NetworkConnectionForceipv6", Settings.networkConnectionForceipv6);
                WriteXMLBool("NetworkProxyEnabled", Settings.networkProxyEnabled);
                WriteXMLBool("NotesTooltipEnabled", Settings.notesTooltipsEnabled);
                WriteXMLBool("NotesClosebtnHidenotepermanently", Settings.notesClosebtnHidenotepermanently);
                WriteXMLBool("NotesDeleteRecyclebin", Settings.notesDeleteRecyclebin);
                WriteXMLBool("NotesTransparencyEnabled", Settings.notesTransparencyEnabled);
                WriteXMLBool("NotesDefaultRandomSkin", Settings.notesDefaultRandomSkin);
                WriteXMLBool("ProgramFirstrun", Settings.programFirstrun);
                WriteXMLBool("ProgramLogError", Settings.programLogError);
                WriteXMLBool("ProgramLogException", Settings.programLogException);
                WriteXMLBool("ProgramLogInfo", Settings.programLogInfo);
                WriteXMLBool("SocialEmailEnabled", Settings.socialEmailEnabled);
                //WriteXMLBool("SocialFacebookEnabled", Settings.socialFacebookEnabled);
                //WriteXMLBool("SocialFacebookUseSSL", Settings.socialFacebookUseSSL);
                //WriteXMLBool("SocialTwitterEnabled", Settings.socialTwitterEnabled);
                //WriteXMLBool("SocialTwitterUseSSL", Settings.socialTwitterUseSSL);
                WriteXMLBool("TrayiconCreatenotebold", Settings.trayiconCreatenotebold);
                WriteXMLBool("TrayiconExitbold", Settings.trayiconExitbold);
                WriteXMLBool("TrayiconManagenotesbold", Settings.trayiconManagenotesbold);
                WriteXMLBool("TrayiconSettingsbold", Settings.trayiconSettingsbold);
                //ints
                xmlwrite.WriteElementString("FontTextdirection", Settings.fontTextdirection.ToString(numfmtinfo));
                xmlwrite.WriteElementString("FontContentSize", Settings.fontContentSize.ToString(numfmtinfo));
                xmlwrite.WriteElementString("FontTitleSize", Settings.fontTitleSize.ToString(numfmtinfo));
                xmlwrite.WriteElementString("NetworkConnectionTimeout", Settings.networkConnectionTimeout.ToString(numfmtinfo));
                xmlwrite.WriteElementString("NotesDefaultSkinnr", Settings.notesDefaultSkinnr.ToString(numfmtinfo));
                xmlwrite.WriteElementString("NotesTransparencyLevel", Settings.notesTransparencyLevel.ToString(numfmtinfo));
                xmlwrite.WriteElementString("NotesWarnLimit", Settings.notesWarnLimit.ToString(numfmtinfo));
                xmlwrite.WriteElementString("TrayiconFontsize", Settings.trayiconFontsize.ToString(numfmtinfo));
                xmlwrite.WriteElementString("TrayiconLeftclickaction", Settings.trayiconLeftclickaction.ToString(numfmtinfo));
                xmlwrite.WriteElementString("UpdatecheckEverydays", Settings.updatecheckEverydays.ToString(numfmtinfo));
                //strings
                xmlwrite.WriteElementString("HighlightHTMLColorInvalid", Settings.highlightHTMLColorInvalid);
                xmlwrite.WriteElementString("HighlightHTMLColorValid", Settings.highlightHTMLColorValid);
                xmlwrite.WriteElementString("HighlightHTMLColorString", Settings.highlightHTMLColorString);
                xmlwrite.WriteElementString("HighlightPHPColorComment", Settings.highlightPHPColorComment);
                xmlwrite.WriteElementString("HighlightPHPColorDocumentstartend", Settings.highlightPHPColorDocumentstartend);
                xmlwrite.WriteElementString("HighlightPHPColorInvalidfunctions", Settings.highlightPHPColorInvalidfunctions);
                xmlwrite.WriteElementString("HighlightPHPColorValidfunctions", Settings.highlightPHPColorValidfunctions);
                xmlwrite.WriteElementString("UpdatecheckLastDate", Settings.updatecheckLastDate.ToString());
                xmlwrite.WriteElementString("FontContentFamily", Settings.fontContentFamily);
                xmlwrite.WriteElementString("FontTitleFamily", Settings.fontTitleFamily);
                xmlwrite.WriteElementString("NetworkProxyAddress", Settings.networkProxyAddress);
                xmlwrite.WriteElementString("SocialEmailDefaultadres", Settings.socialEmailDefaultadres);
                //xmlwrite.WriteElementString("SocialTwitterUsername", Settings.socialTwitterUsername);
                //xmlwrite.WriteElementString("SocialFacebookEmail", Settings.socialFacebookEmail);
                xmlwrite.WriteElementString("NotesSavepath", Settings.notesSavepath);
                xmlwrite.WriteEndElement();
                xmlwrite.WriteEndDocument();
            }
            catch (AccessViolationException)
            {
                Log.Write(LogType.exception, "Premission problem writing: "+Path.Combine(Program.AppDataFolder, SETTINGSFILE));
                return false;
            }
            finally
            {
                xmlwrite.Close();
            }
            return true;
        }

        // Private Methods (3) 

        /// <summary>
        /// Convert HEX color to color object.
        /// </summary>
        /// <param name="colorstring">Hex color as string.</param>
        /// <returns>Color object.</returns>
        public static System.Drawing.Color ConvToClr(string colorstring)
        {
            //HEX color
            return System.Drawing.ColorTranslator.FromHtml(colorstring);

            //DECIMAL color, commented out in favor of HEX notation for speed.
            //string[] parts = new string[3];
            //parts = colorstring.Split(',');
            //try
            //{
            //    UInt8 redchannel = Convert.ToUInt16(parts[0].Trim());
            //    UInt8 greenchannel = Convert.ToUInt16(parts[1].Trim());
            //    UInt8 bluechannel = Convert.ToUInt16(parts[2].Trim());
            //    return System.Drawing.Color.FromArgb(redchannel, greenchannel, bluechannel);
            //}
            //catch
            //{
            //    if (colorstring.Length < 100)
            //    {
            //        throw new CustomException("Cannot parser: " + colorstring);
            //    }
            //    else
            //    {
            //        throw new CustomException("Cannot parser: " + colorstring.Substring(0, 100)+" ..");
            //    }
            //}
        }

        /// <summary>
        /// Get the new version as a integer array with first major
        /// second valeau the minor version and the third valeau being the release version.
        /// </summary>
        /// <param name="versionquality">The latest version quality, e.g: alpha, beta, rc or nothing for final.</param>
        /// <returns>the newest version as integer array, 
        /// any negative valeau(-1 by default) considered as error.</returns>
        public static short[] GetLatestVersion(out string versionquality)
        {
            short[] version = new short[3];
            version[0] = -1;
            version[1] = -1;
            version[2] = -1;
            versionquality = Program.AssemblyVersionQuality;
            try
            {
                System.Net.ServicePointManager.Expect100Continue = false;
                System.Net.ServicePointManager.DefaultConnectionLimit = 1;
                WebRequest request = WebRequest.Create(UPDATEURL);
                request.Method = "GET";
                request.ContentType = "text/xml";
                request.Timeout = Settings.networkConnectionTimeout;
                request.Headers.Add("X-NoteFly-Version", Program.AssemblyVersionAsString); //for stats and future use.
                if (Settings.networkProxyEnabled && !String.IsNullOrEmpty(Settings.networkProxyAddress))
                {
                    request.Proxy = new WebProxy(Settings.networkProxyAddress);
                }

                request.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore); //do not cache, prevent incorrect cache result.
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

        /*
        public static string[] ParserLanguageCommentkeywords(string file, string languagename)
        {
            string[] comments = new string[3];
            try
            {
                xmlread = new XmlTextReader(Path.Combine(Program.InstallFolder, file));
                xmlread.ProhibitDtd = true;
                while (xmlread.Read())
                {
                    if (xmlread.Name == "Language")
                    {
                        if (xmlread.GetAttribute("name") == languagename)
                        {
                            comments[0] = xmlread.GetAttribute("commentLine");
                            comments[1] = xmlread.GetAttribute("commentStart");
                            comments[2] = xmlread.GetAttribute("commentEnd");
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
            }
            finally
            {
                xmlread.Close();
            }
            return comments;
        }
         */

        /// <summary>
        /// Return a array of keywords used for the prgramming language we are doing a syntax check on.
        /// </summary>
        /// <param name="file">the file to parser.</param>
        /// <param name="languagename">the language to lookup.</param>
        /// <param name="varcomments">commentline, commentstart and commentend</param>
        /// <returns>An array of keyword used for hightlighting.</returns>
        public static string[] ParserLanguageLexical(string file, string languagename, out string[] langcomments)
        {
            string[] keywords = null;
            langcomments = new string[3];
            langcomments[0] = string.Empty;
            langcomments[1] = string.Empty;
            langcomments[2] = string.Empty;
            try
            {
                xmlread = new XmlTextReader(Path.Combine(Program.InstallFolder, file));
                xmlread.ProhibitDtd = true;
                bool readsubnodes = false;
                while (xmlread.Read())
                {
                    if (xmlread.Name == "Language" || readsubnodes)
                    {
                        if (xmlread.GetAttribute("name") == languagename || readsubnodes)
                        {
                            if (!readsubnodes)
                            {
                                langcomments[0] = xmlread.GetAttribute("commentLine");
                                langcomments[1] = xmlread.GetAttribute("commentStart");
                                langcomments[2] = xmlread.GetAttribute("commentEnd");
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
                Settings.highlightHTML = false;
                Settings.highlightPHP = false;
                Settings.highlightSQL = false;
            }
            finally
            {
                xmlread.Close();
            }
            return keywords;
        }

        /// <summary>
        /// Parser a note node in a xml file, 
        /// readnotenum is the number of note node to be parser and returned as note object.
        /// </summary>
        /// <param name="notes">pointer to notes</param>
        /// <param name="note">the note object to set</param>
        /// <param name="readnotenum">The number occurance of the note node to be parser (first, sencod etc.)</param>
        /// <returns>a note object</returns>
        private static Note ParserNoteNode(Notes notes, Note note, int readnotenum, bool setallcontent)
        {
            int curnotenum = -1;
            bool endnode = false;
            while (xmlread.Read())
            {
                if (xmlread.Name != String.Empty)
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
                            note.visible = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "locked":
                            note.locked = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "ontop":
                            note.ontop = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "width":
                            note.width = xmlread.ReadElementContentAsInt();
                            break;
                        case "heigth":
                            note.height = xmlread.ReadElementContentAsInt();
                            break;
                        case "x":
                            note.x = xmlread.ReadElementContentAsInt();
                            break;
                        case "y":
                            note.y = xmlread.ReadElementContentAsInt();
                            break;
                        case "skin":
                            int skinnr = notes.GetSkinNr(xmlread.ReadElementContentAsString());
                            if (skinnr >= 0)
                            {
                                note.skinNr = skinnr;
                            }

                            break;
                        case "title":
                            note.title = xmlread.ReadElementContentAsString();
                            break;
                        case "content":
                            if ((note.visible) || (setallcontent))
                            {
                                note.tempcontent = xmlread.ReadElementContentAsString();
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
                xmlwrite.WriteStartDocument(true);//standalone
                xmlwrite.WriteStartElement("skins");
                const int NUMDEFAULTSKINS = 8;
                xmlwrite.WriteAttributeString("count", NUMDEFAULTSKINS.ToString()); //for performance predefine list Capacity, not required.
                string[] name = new string[NUMDEFAULTSKINS] { "yellow", "orange", "white", "green", "blue", "purple", "red", "dark" };
                string[] primaryclr = new string[NUMDEFAULTSKINS] { "FFEF14", "FFA700", "FFFFFF", "6FE200", "5A86D5", "FF1AFF", "FF1A1A", "002626" };
                string[] selectclr = new string[NUMDEFAULTSKINS] { "E0D616", "C17D00", "E0E0E0", "008000", "1A1AFF", "8B1A8B", "7A1515", "000624" };
                string[] highlightclr = new string[NUMDEFAULTSKINS] { "FFED7C", "FFD46D", "E5E5E5", "DADBD9", "C6CBD3", "FFC1FF", "FF6F6F", "494949" };
                string[] textclr = new string[NUMDEFAULTSKINS] { "000000", "000000", "000000", "000000", "000000", "000000", "000000", "FFFFFF" };
                for (ushort i = 0; i < NUMDEFAULTSKINS; i++)
                {
                    xmlwrite.WriteStartElement("skin");
                    xmlwrite.WriteElementString("Name", name[i]);
                    xmlwrite.WriteElementString("PrimaryClr", "#" + primaryclr[i]);
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

            xmlUtil.CheckFile(filename, 1630);
        }

        /// <summary>
        /// Checks if filesize is right for a partialer file.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="exceptedfilesize"></param>
        private static void CheckFile(string filename, long exceptedfilesize)
        {
            FileInfo fi = new FileInfo(filename);
            if (fi.Length != exceptedfilesize)
            {
                Log.Write(LogType.exception, filename + " has not excepted filesize. Check if corrupted.");
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
            WriteXMLBool("visible", note.visible);
            WriteXMLBool("ontop", note.ontop);
            WriteXMLBool("locked", note.locked);
            xmlwrite.WriteStartElement("location");
            xmlwrite.WriteElementString("x", note.x.ToString());
            xmlwrite.WriteElementString("y", note.y.ToString());
            xmlwrite.WriteEndElement();
            xmlwrite.WriteStartElement("size");
            xmlwrite.WriteElementString("width", Convert.ToString(note.width));
            xmlwrite.WriteElementString("heigth", Convert.ToString(note.height));
            xmlwrite.WriteEndElement();
            xmlwrite.WriteElementString("skin", skinname);
            xmlwrite.WriteElementString("title", note.title);
            xmlwrite.WriteElementString("content", content);
            xmlwrite.WriteEndElement();
        }

        #endregion Methods
    }
}