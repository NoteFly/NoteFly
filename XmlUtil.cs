//-----------------------------------------------------------------------
// <copyright file="XmlUtil.cs" company="NoteFly">
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
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// XmlUtil class, for saving and parsering xml.
    /// </summary>
    public sealed class xmlUtil
    {
        #region Fields (5)
        /// <summary>
        /// Skin file.
        /// </summary>
        public const string SKINFILE = "skins.xml";

        /// <summary>
        /// Settings file.
        /// </summary>
        public const string SETTINGSFILE = "settings.xml";

        /// <summary>
        /// The note version
        /// </summary>
        private const string NOTEVERSION = "3";

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
        /// <returns>Return node content as string, empty if not found.</returns>
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

            xmlread.ProhibitDtd = true;
            xmlread.XmlResolver = new XmlSecureResolver(new XmlUrlResolver(), filename);

            try
            {
                while (xmlread.Read())
                {
                    if (xmlread.Name == nodename)
                    {
                        string xmlnodecontent;
                        xmlnodecontent = xmlread.ReadElementContentAsString();
                        /*
                        const int charbuffersize = 32;
                        StringBuilder sbxmlnodecontent = new StringBuilder();
                        int countchr = 0;
                        char[] buffer = new Char[charbuffersize];
                        xmlread.MoveToContent();
                        while ((countchr = xmlread.ReadChars(buffer, 0, charbuffersize)) > 0)
                        {
                            sbxmlnodecontent.Append(new string(buffer, 0, countchr));
                            Array.Clear(buffer, 0, charbuffersize);
                        }
                         */

#if DEBUG
                        stopwatch.Stop();
                        Log.Write(LogType.info, "Read content time:  " + stopwatch.ElapsedTicks + " ticks"); // blocking display time ~200ms/7
#endif
                        //return sbxmlnodecontent.ToString();
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

            xmlread.ProhibitDtd = true;
            xmlread.XmlResolver = new XmlSecureResolver(new XmlUrlResolver(), filename);
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
        /// Parser the listing of the plugins
        /// </summary>
        /// <param name="response">The server response.</param>
        /// <param name="ipluginversionparts">The Iplugin version as array with major, minor, release numbers</param>
        /// <param name="lbxAvailablePlugins">ListBox with availible plugins</param>
        /// <returns>True if parsering of response succeeded.</returns>
        public static bool ParserListPlugins(string response, short[] ipluginversionparts, System.Windows.Forms.ListBox lbxAvailablePlugins)
        {
            if (string.IsNullOrEmpty(response))
            {
                return false;
            }

            bool succeeded = false;
            XmlTextReader xmlreader = new XmlTextReader(new System.IO.StringReader(response));
            xmlreader.ProhibitDtd = true;
            const int MAXSEARCHRESULTSPLUGINS = 50;
            int numsearchresultsplugins = 0;
            try
            {
                while (xmlreader.Read())
                {
                    if (xmlreader.Name == "plugin")
                    {
                        numsearchresultsplugins++;
                        if (numsearchresultsplugins > MAXSEARCHRESULTSPLUGINS)
                        {
                            break;
                        }

                        string pluginname = null;
                        string curpluginminversioniplugin = null;
                        XmlReader xmlplugin = xmlreader.ReadSubtree();
                        while (xmlplugin.Read())
                        {
                            switch (xmlplugin.Name)
                            {
                                case "name":
                                    pluginname = xmlplugin.ReadElementContentAsString();
                                    break;
                                case "minversioniplugin":
                                    curpluginminversioniplugin = xmlplugin.ReadElementContentAsString();
                                    break;
                            }
                        }

                        short[] curpluginminveripluginpart = Program.ParserVersionString(curpluginminversioniplugin);
                        int compversionsresults = Program.CompareVersions(ipluginversionparts, curpluginminveripluginpart);

                        if (!string.IsNullOrEmpty(pluginname) && compversionsresults >= 0)
                        {
                            lbxAvailablePlugins.Items.Add(pluginname);
                        }
                    }
                }

                succeeded = true;
            }
            catch (WebException webexc)
            {
                succeeded = false;
                Log.Write(LogType.error, webexc.Message);
            }
            finally
            {
                if (xmlreader != null)
                {
                    xmlreader.Close();
                }
            }

            return succeeded;
        }

        /// <summary>
        /// Parser the details of the plugin detail response
        /// </summary>
        /// <param name="response">Parser the xml response</param>
        /// <param name="installedpluginnames">The names of the plugins installed.</param>
        /// <param name="alreadyinstalled">Is the plugin to get details from already installed.</param>
        /// <param name="updateavailable">Is a update availible based on parsering of the serverresponse</param>
        /// <returns>Array of strings with details from the plugin.</returns>
        public static string[] ParserDetailsPlugin(string response, string[] installedpluginnames, out bool alreadyinstalled, out bool updateavailable)
        {
            alreadyinstalled = false;
            updateavailable = false;
            if (string.IsNullOrEmpty(response))
            {
                return null;
            }

            string[] detailsplugin = new string[6];
            XmlTextReader xmlreader = null;
            try
            {
                xmlreader = new XmlTextReader(new System.IO.StringReader(response));
                xmlreader.ProhibitDtd = true;
                while (xmlreader.Read())
                {
                    if (xmlreader.Name == "plugin")
                    {
                        XmlReader xmlplugin = xmlreader.ReadSubtree();
                        while (xmlplugin.Read())
                        {
                            switch (xmlplugin.Name)
                            {
                                case "name":
                                    detailsplugin[0] = xmlplugin.ReadElementContentAsString();
                                    if (!string.IsNullOrEmpty(detailsplugin[0]))
                                    {
                                        for (int i = 0; i < installedpluginnames.Length; i++)
                                        {
                                            if (detailsplugin[0].Equals(installedpluginnames[i], StringComparison.OrdinalIgnoreCase))
                                            {
                                                alreadyinstalled = true;
                                            }
                                        }
                                    }

                                    break;
                                case "version":
                                    detailsplugin[1] = xmlplugin.ReadElementContentAsString();
                                    break;
                                case "license":
                                    detailsplugin[2] = xmlplugin.ReadElementContentAsString();
                                    break;
                                case "description":
                                    detailsplugin[3] = xmlplugin.ReadElementContentAsString();
                                    break;
                                case "downloadurl":
                                    detailsplugin[4] = xmlplugin.ReadElementContentAsString();
                                    break;
                                case "signature":
                                    detailsplugin[5] = xmlplugin.ReadElementContentAsString();
                                    break;
                            }
                        }
                    }
                }
            }
            finally
            {
                if (xmlreader != null)
                {
                    xmlreader.Close();
                }
            }

            if (!string.IsNullOrEmpty(detailsplugin[0]) && !string.IsNullOrEmpty(detailsplugin[1]))
            {
                short[] installedpluginversion = PluginsManager.GetPluginVersionByName(detailsplugin[0]);
                short[] availablepluginversion = Program.ParserVersionString(detailsplugin[1]);
                if (Program.CompareVersions(availablepluginversion, installedpluginversion) > 0)
                {
                    // 1 if availablepluginversion is higher than installedpluginversion
                    updateavailable = true;
                }
            }

            return detailsplugin;
        }

        /// <summary>
        /// Load a note file.
        /// </summary>
        /// <param name="notes">Reference to notes class.</param>
        /// <param name="notefilename">The note filename.</param>
        /// <returns>An note object.</returns>
        public static Note LoadNoteFile(Notes notes, string notefilename)
        {
            if (notefilename.Length > 255)
            {
                Log.Write(LogType.exception, "Filename for note file too long.");
                return null;
            }

            Note note = new Note(notes, notefilename);
            string notefilepath = Path.Combine(Settings.NotesSavepath, notefilename);
            xmlread = new XmlTextReader(notefilepath);
            xmlread.ProhibitDtd = true;
            xmlread.XmlResolver = new XmlSecureResolver(new XmlUrlResolver(), notefilepath);
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
        /// <returns>True if file settings exists.</returns>
        public static bool LoadSettings()
        {
            if (!Directory.Exists(Program.AppDataFolder))
            {
                Directory.CreateDirectory(Program.AppDataFolder);
            }

            string settingsfilepath = Path.Combine(Program.AppDataFolder, SETTINGSFILE);
            if (!File.Exists(settingsfilepath))
            {
                return false;
            }

            try
            {
                const int MAXDEPTHSETTINGSFILE = 4;
                xmlread = new XmlTextReader(settingsfilepath);
                xmlread.EntityHandling = EntityHandling.ExpandCharEntities;
                xmlread.ProhibitDtd = true; // gives decreated warning in vs2010.
                xmlread.XmlResolver = new XmlSecureResolver(new XmlUrlResolver(), settingsfilepath);
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
                        case "HotkeysNewNoteEnabled":
                            Settings.HotkeysNewNoteEnabled = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "HotkeysNewNoteAltInsteadShift":
                            Settings.HotkeysNewNoteAltInsteadShift = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "HotkeysManageNotesEnabled":
                            Settings.HotkeysManageNotesEnabled = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "HotkeysManageNotesAltInsteadShift":
                            Settings.HotkeysManageNotesAltInsteadShift = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "HotkeysNotesToFrontEnabled":
                            Settings.HotkeysNotesToFrontEnabled = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "HotkeysNotesToFrontAltInsteadShift":
                            Settings.HotkeysNotesToFrontAltInsteadShift = xmlread.ReadElementContentAsBoolean();
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
                        case "NotesDefaultTitleDate":
                            Settings.NotesDefaultTitleDate = xmlread.ReadElementContentAsBoolean();
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
                        case "ProgramFirstrun": // legacy
                            Settings.ProgramFirstrunned = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "ProgramFirstrunned":
                            Settings.ProgramFirstrunned = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "ProgramFormsDoublebuffered":
                            Settings.ProgramFormsDoublebuffered = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "ProgramLanguage":
                            Settings.ProgramLanguage = xmlread.ReadElementContentAsString();
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
                        case "SharingEmailEnabled":
                            Settings.SharingEmailEnabled = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "SettingsExpertEnabled":
                            Settings.SettingsExpertEnabled = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "TrayiconAlternateIcon":
                            Settings.TrayiconAlternateIcon = xmlread.ReadElementContentAsBoolean();
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
                        case "UpdateSilentInstall":
                            Settings.UpdateSilentInstall = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "ManagenotesTooltip":
                            Settings.ManagenotesTooltip = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "ManagenotesSearchCasesentive":
                            Settings.ManagenotesSearchCasesentive = xmlread.ReadElementContentAsBoolean();
                            break;

                        // ints and doubles
                        case "HotkeysNewNoteKeycode":
                            Settings.HotkeysNewNoteKeycode = xmlread.ReadElementContentAsInt();
                            break;
                        case "HotkeysManageNotesKeycode":
                            Settings.HotkeysManageNotesKeycode = xmlread.ReadElementContentAsInt();
                            break;
                        case "HotkeysNotesToFrontKeycode":
                            Settings.HotkeysNotesToFrontKeycode = xmlread.ReadElementContentAsInt();
                            break;
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
                        case "NetworkIPversion":
                            Settings.NetworkIPversion = xmlread.ReadElementContentAsInt();
                            break;
                        case "NetworkProxyPort":
                            Settings.NetworkProxyPort = xmlread.ReadElementContentAsInt();
                            break;
                        case "NotesDefaultSkinnr":
                            Settings.NotesDefaultSkinnr = xmlread.ReadElementContentAsInt();
                            break;
                        case "NotesDefaultWidth":
                            Settings.NotesDefaultWidth = xmlread.ReadElementContentAsInt();
                            break;
                        case "NotesDefaultHeight":
                            Settings.NotesDefaultHeight = xmlread.ReadElementContentAsInt();
                            break;
                        case "NotesTitlepanelMaxHeight":
                            Settings.NotesTitlepanelMaxHeight = xmlread.ReadElementContentAsInt();
                            break;
                        case "NotesTitlepanelMinHeight":
                            Settings.NotesTitlepanelMinHeight = xmlread.ReadElementContentAsInt();
                            break;
                        case "NotesTooltipPreviewlength":
                            Settings.NotesTooltipPreviewlength = xmlread.ReadElementContentAsInt();
                            break;
                        case "NotesWarnlimitTotal":
                            Settings.NotesWarnlimitTotal = xmlread.ReadElementContentAsInt();
                            break;
                        case "NotesWarnlimitVisible":
                            Settings.NotesWarnlimitVisible = xmlread.ReadElementContentAsInt();
                            break;
                        case "NotesTransparencyLevel":
                            Settings.NotesTransparencyLevel = xmlread.ReadElementContentAsDouble();
                            break;
                        case "ManagenotesFontsize":
                            Settings.ManagenotesFontsize = xmlread.ReadElementContentAsFloat();
                            break;
                        case "ManagenotesSkinnr":
                            Settings.ManagenotesSkinnr = xmlread.ReadElementContentAsInt();
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
                        case "SettingsLastTab":
                            Settings.SettingsLastTab = xmlread.ReadElementContentAsInt();
                            break;

                        // strings
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
                        case "FontTitleFamily":
                            Settings.FontTitleFamily = xmlread.ReadElementContentAsString();
                            break;
                        case "FontContentFamily":
                            Settings.FontContentFamily = xmlread.ReadElementContentAsString();
                            break;
                        case "FontTrayicon":
                            Settings.FontTrayicon = xmlread.ReadElementContentAsString();
                            break;
                        case "ProgramLastrunVersion":
                            Settings.ProgramLastrunVersion = xmlread.ReadElementContentAsString();
                            break;
                        case "ProgramPluginsFolder":
                            string readpluginfolder = xmlread.ReadElementContentAsString();
                            if (Directory.Exists(readpluginfolder))
                            {
                                Settings.ProgramPluginsFolder = Path.GetFullPath(readpluginfolder);
                            }

                            break;
                        case "ProgramPluginsEnabled":
                            Settings.ProgramPluginsEnabled = xmlread.ReadElementContentAsString();
                            break;
                        case "ProgramPluginsDllexclude":
                            Settings.ProgramPluginsDllexclude = xmlread.ReadElementContentAsString();
                            break;
                        case "SharingEmailDefaultadres":
                            Settings.SharingEmailDefaultadres = xmlread.ReadElementContentAsString();
                            break;
                        case "NetworkProxyAddress":
                            Settings.NetworkProxyAddress = xmlread.ReadElementContentAsString();
                            break;
                        case "NotesSavepath":
                            Settings.NotesSavepath = xmlread.ReadElementContentAsString();
                            break;
                        case "UpdatecheckGPGKeyserver":
                            Settings.UpdatecheckGPGKeyserver = xmlread.ReadElementContentAsString();
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

                    if (xmlread.Depth > MAXDEPTHSETTINGSFILE)
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
                Log.Write(LogType.info, "Writing default skins.xml");
                WriteDefaultSkins(skinfilepath);
            }

            List<Skin> skins = new List<Skin>();
            try
            {
                xmlread = new XmlTextReader(skinfilepath);
                xmlread.ProhibitDtd = true;
                xmlread.XmlResolver = new XmlSecureResolver(new XmlUrlResolver(), skinfilepath);
                Skin curskin = null;
                int numskins = 0;
                bool endtag = false;
                const int MAXNUMSKIN = 255;
                const int MAXLENSKINNAME = 200;
                while (xmlread.Read())
                {
                    switch (xmlread.Name)
                    {
                        case "skins":
                            if (xmlread.HasAttributes)
                            {
                                int count = Convert.ToInt32(xmlread.GetAttribute("count"));
                                if (count > 0)
                                {
                                    skins.Capacity = count;
                                }
                            }

                            break;
                        case "skin":
                            if (endtag)
                            {
                                if (curskin != null && numskins < MAXNUMSKIN)
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

                            string skinname = xmlread.ReadElementContentAsString();
                            if (skinname.Length < MAXLENSKINNAME)
                            {
                                curskin.Name = skinname;
                            }
                            else
                            {
                                curskin.Name = skinname.Substring(0, MAXLENSKINNAME);
                            }

                            break;
                        case "PrimaryClr":
                            if (xmlread.HasAttributes)
                            {
                                string filepathtexture = xmlread.GetAttribute("texture");
                                bool texturefpexist = System.IO.File.Exists(filepathtexture);
                                if (texturefpexist || System.IO.File.Exists(Path.Combine(Program.InstallFolder, filepathtexture)))
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
                                            curskin.PrimaryTexture = filepathtexture;
                                        }
                                        else
                                        {
                                            curskin.PrimaryTexture = Path.Combine(Program.InstallFolder, filepathtexture);
                                        }
                                    }
                                    else
                                    {
                                        Log.Write(LogType.error, "Texture image format not supported.");
                                    }
                                }
                                else
                                {
                                    Log.Write(LogType.error, "Texture image not be found, looking for: " + filepathtexture);
                                }

                                string texturelayout = xmlread.GetAttribute("texturelayout");
                                if (!string.IsNullOrEmpty(texturelayout))
                                {
                                    texturelayout = texturelayout.ToLowerInvariant();
                                    switch (texturelayout)
                                    {
                                        case "tile":
                                            curskin.PrimaryTextureLayout = System.Windows.Forms.ImageLayout.Tile;
                                            break;
                                        case "stretch":
                                            curskin.PrimaryTextureLayout = System.Windows.Forms.ImageLayout.Stretch;
                                            break;
                                        case "center":
                                            curskin.PrimaryTextureLayout = System.Windows.Forms.ImageLayout.Center;
                                            break;
                                    }
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
                        throw new ApplicationException("Skin file corrupted: " + SKINFILE);
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
            Settings.FontTrayicon = "Arial";
#elif linux
            Settings.FontContentFamily = "FreeMono";
            Settings.FontTitleFamily = "FreeMono";
            Settings.FontTrayicon = "FreeMono";
#else
            Settings.FontContentFamily = "FreeMono";
            Settings.FontTitleFamily = "FreeMono";
            Settings.FontTrayicon = "FreeMono";
#endif
            Settings.FontContentSize = 11;
            Settings.FontTextdirection = 0;
            Settings.FontTitleSize = 14;
            Settings.FontTitleStylebold = true;
            Settings.HighlightMaxchars = 30000;
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
            Settings.HotkeysNewNoteEnabled = true;
            Settings.HotkeysNewNoteAltInsteadShift = true;
            Settings.HotkeysNewNoteKeycode = 78; // N
            Settings.HotkeysManageNotesEnabled = true;
            Settings.HotkeysManageNotesAltInsteadShift = true;
            Settings.HotkeysManageNotesKeycode = 77; // M
            Settings.HotkeysNotesToFrontEnabled = true;
            Settings.HotkeysNotesToFrontAltInsteadShift = true;
            Settings.HotkeysNotesToFrontKeycode = 70; // F
            Settings.NetworkConnectionTimeout = 8000;
            Settings.NetworkIPversion = 0;
            Settings.NetworkProxyAddress = string.Empty;
            Settings.NetworkProxyEnabled = false;
            Settings.NotesTooltipsEnabled = true;
            Settings.NotesClosebtnHidenotepermanently = true;
            Settings.NotesDefaultRandomSkin = false;
            Settings.NotesDefaultSkinnr = 0; // default first skin, normally yellow
            Settings.NotesDefaultHeight = 240;
            Settings.NotesDefaultWidth = 280;
            Settings.NotesDefaultTitleDate = true;
            Settings.NotesTitlepanelMaxHeight = 64;
            Settings.NotesTitlepanelMinHeight = 32;
            Settings.NotesSavepath = Program.GetDefaultNotesFolder();
            Settings.NotesTransparencyEnabled = true;
            Settings.NotesTransparentRTB = true;
            Settings.NotesTransparencyLevel = 0.9;
            Settings.NotesWarnlimitTotal = 5000;
            Settings.NotesWarnlimitVisible = 50;
            Settings.ProgramFirstrunned = false;
            Settings.ProgramFormsDoublebuffered = false; // ProgramFormsDoublebuffered=true creates blank windows on windows 8 pre-beta.
            Settings.ProgramLogError = true;
            Settings.ProgramLogException = true;
            Settings.ProgramLogInfo = false;
            Settings.ProgramPluginsAllEnabled = true;
            Settings.ProgramPluginsDllexclude = "SQLite3.dll|System.Data.SQLite.DLL|Interop.SpeechLib.dll";
            Settings.ProgramPluginsFolder = Program.GetDefaultPluginFolder();
            Settings.ProgramSuspressWarnAdmin = false;
            Settings.SettingsLastTab = 0;
            Settings.SettingsExpertEnabled = false;
            Settings.SharingEmailEnabled = true;
            Settings.SharingEmailDefaultadres = string.Empty;
            Settings.TrayiconAlternateIcon = false;
            Settings.TrayiconFontsize = 10.00f; // default .net: 8.25f; but made bigger (and more) for a little more readablity
            Settings.TrayiconLeftclickaction = 1;
            Settings.TrayiconCreatenotebold = true;
            Settings.TrayiconExitbold = false;
            Settings.TrayiconManagenotesbold = false;
            Settings.TrayiconSettingsbold = false;
            Settings.UpdateSilentInstall = false;
            Settings.UpdatecheckEverydays = 14; // 0 is disabled.
            Settings.UpdatecheckLastDate = DateTime.Now.ToString();
            Settings.UpdatecheckURL = "http://update.notefly.org/latestversion.xml";
            GPGVerifyWrapper gpgverif = new GPGVerifyWrapper();
            if (!string.IsNullOrEmpty(gpgverif.GetGPGPath()) && gpgverif != null)
            {
                Settings.UpdatecheckGPGPath = gpgverif.GetGPGPath();
                Settings.UpdatecheckUseGPG = true;
            }
            else
            {
                Settings.UpdatecheckUseGPG = false;
            }

            xmlUtil.WriteSettings();
            xmlUtil.CheckFile(Path.Combine(Program.AppDataFolder, SETTINGSFILE));
            return true;
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
                WriteNoteBody(xmlwrite, note, skinname, content);
                xmlwrite.WriteEndDocument();
                succeeded = true;
            }
            catch
            {
                succeeded = false;
            }
            finally
            {
                if (xmlwrite != null)
                {
                    xmlwrite.Flush();
                    xmlwrite.Close();
                }
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
                ////xmlread.XmlResolver = new XmlSecureResolver(new XmlUrlResolver(), filepath);
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
                ////xmlread.XmlResolver = new XmlSecureResolver(new XmlUrlResolver(), filepath);
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
            lock (typeof(xmlUtil))
            {
                NumberFormatInfo numfmtinfo = CultureInfo.InvariantCulture.NumberFormat;
                try
                {
                    if (!Directory.Exists(Program.AppDataFolder))
                    {
                        Directory.CreateDirectory(Program.AppDataFolder);
                    }

                    xmlwrite = new XmlTextWriter(Path.Combine(Program.AppDataFolder, SETTINGSFILE), System.Text.Encoding.UTF8);
                    xmlwrite.Formatting = Formatting.Indented;
                    xmlwrite.WriteStartDocument(true); // standalone document
                    xmlwrite.WriteStartElement("settings");

                    // booleans
                    WriteXMLBool(xmlwrite, "ConfirmDeletenote", Settings.ConfirmDeletenote);
                    WriteXMLBool(xmlwrite, "ConfirmExit", Settings.ConfirmExit);
                    WriteXMLBool(xmlwrite, "ConfirmLinkclick", Settings.ConfirmLinkclick);
                    WriteXMLBool(xmlwrite, "FontTitleStylebold", Settings.FontTitleStylebold);
                    WriteXMLBool(xmlwrite, "HighlightHTML", Settings.HighlightHTML);
                    WriteXMLBool(xmlwrite, "HighlightHyperlinks", Settings.HighlightHyperlinks);
                    WriteXMLBool(xmlwrite, "HighlightPHP", Settings.HighlightPHP);
                    WriteXMLBool(xmlwrite, "HighlightSQL", Settings.HighlightSQL);
                    WriteXMLBool(xmlwrite, "HotkeysNewNoteEnabled", Settings.HotkeysNewNoteEnabled);
                    WriteXMLBool(xmlwrite, "HotkeysNewNoteAltInsteadShift", Settings.HotkeysNewNoteAltInsteadShift);
                    WriteXMLBool(xmlwrite, "HotkeysManageNotesEnabled", Settings.HotkeysManageNotesEnabled);
                    WriteXMLBool(xmlwrite, "HotkeysManageNotesAltInsteadShift", Settings.HotkeysManageNotesAltInsteadShift);
                    WriteXMLBool(xmlwrite, "HotkeysNotesToFrontEnabled", Settings.HotkeysNotesToFrontEnabled);
                    WriteXMLBool(xmlwrite, "HotkeysNotesToFrontAltInsteadShift", Settings.HotkeysNotesToFrontAltInsteadShift);
                    WriteXMLBool(xmlwrite, "NetworkProxyEnabled", Settings.NetworkProxyEnabled);
                    WriteXMLBool(xmlwrite, "NotesTooltipEnabled", Settings.NotesTooltipsEnabled);
                    WriteXMLBool(xmlwrite, "NotesClosebtnHidenotepermanently", Settings.NotesClosebtnHidenotepermanently);
                    WriteXMLBool(xmlwrite, "NotesDeleteRecyclebin", Settings.NotesDeleteRecyclebin);
                    WriteXMLBool(xmlwrite, "NotesTransparencyEnabled", Settings.NotesTransparencyEnabled);
                    WriteXMLBool(xmlwrite, "NotesTransparentRTB", Settings.NotesTransparentRTB);
                    WriteXMLBool(xmlwrite, "NotesDefaultRandomSkin", Settings.NotesDefaultRandomSkin);
                    WriteXMLBool(xmlwrite, "NotesDefaultTitleDate", Settings.NotesDefaultTitleDate);
                    WriteXMLBool(xmlwrite, "ProgramFirstrunned", Settings.ProgramFirstrunned);
                    WriteXMLBool(xmlwrite, "ProgramFormsDoublebuffered", Settings.ProgramFormsDoublebuffered);
                    WriteXMLBool(xmlwrite, "ProgramLogError", Settings.ProgramLogError);
                    WriteXMLBool(xmlwrite, "ProgramLogException", Settings.ProgramLogException);
                    WriteXMLBool(xmlwrite, "ProgramLogInfo", Settings.ProgramLogInfo);
                    WriteXMLBool(xmlwrite, "ProgramPluginsAllEnabled", Settings.ProgramPluginsAllEnabled);
                    WriteXMLBool(xmlwrite, "ProgramSuspressWarnAdmin", Settings.ProgramSuspressWarnAdmin);
                    WriteXMLBool(xmlwrite, "SharingEmailEnabled", Settings.SharingEmailEnabled);
                    WriteXMLBool(xmlwrite, "SettingsExpertEnabled", Settings.SettingsExpertEnabled);
                    WriteXMLBool(xmlwrite, "TrayiconAlternateIcon", Settings.TrayiconAlternateIcon);
                    WriteXMLBool(xmlwrite, "TrayiconCreatenotebold", Settings.TrayiconCreatenotebold);
                    WriteXMLBool(xmlwrite, "TrayiconExitbold", Settings.TrayiconExitbold);
                    WriteXMLBool(xmlwrite, "TrayiconManagenotesbold", Settings.TrayiconManagenotesbold);
                    WriteXMLBool(xmlwrite, "TrayiconSettingsbold", Settings.TrayiconSettingsbold);
                    WriteXMLBool(xmlwrite, "UpdateSilentInstall", Settings.UpdateSilentInstall);
                    WriteXMLBool(xmlwrite, "UpdatecheckUseGPG", Settings.UpdatecheckUseGPG);
                    WriteXMLBool(xmlwrite, "ManagenotesSearchCasesentive", Settings.ManagenotesSearchCasesentive);
                    WriteXMLBool(xmlwrite, "ManagenotesTooltip", Settings.ManagenotesTooltip);

                    // integers
                    xmlwrite.WriteElementString("HotkeysNewNoteKeycode", Settings.HotkeysNewNoteKeycode.ToString(numfmtinfo));
                    xmlwrite.WriteElementString("HotkeysManageNotesKeycode", Settings.HotkeysManageNotesKeycode.ToString(numfmtinfo));
                    xmlwrite.WriteElementString("HotkeysNotesToFrontKeycode", Settings.HotkeysNotesToFrontKeycode.ToString(numfmtinfo));
                    xmlwrite.WriteElementString("FontTextdirection", Settings.FontTextdirection.ToString(numfmtinfo));
                    xmlwrite.WriteElementString("FontContentSize", Settings.FontContentSize.ToString(numfmtinfo));
                    xmlwrite.WriteElementString("FontTitleSize", Settings.FontTitleSize.ToString(numfmtinfo));
                    xmlwrite.WriteElementString("NetworkConnectionTimeout", Settings.NetworkConnectionTimeout.ToString(numfmtinfo));
                    xmlwrite.WriteElementString("NetworkIPversion", Settings.NetworkIPversion.ToString(numfmtinfo));
                    xmlwrite.WriteElementString("NetworkProxyPort", Settings.NetworkProxyPort.ToString(numfmtinfo));
                    xmlwrite.WriteElementString("NotesDefaultSkinnr", Settings.NotesDefaultSkinnr.ToString(numfmtinfo));
                    xmlwrite.WriteElementString("NotesTransparencyLevel", Settings.NotesTransparencyLevel.ToString(numfmtinfo));
                    xmlwrite.WriteElementString("NotesDefaultWidth", Settings.NotesDefaultWidth.ToString(numfmtinfo));
                    xmlwrite.WriteElementString("NotesDefaultHeight", Settings.NotesDefaultHeight.ToString(numfmtinfo));
                    xmlwrite.WriteElementString("NotesTitlepanelMaxHeight", Settings.NotesTitlepanelMaxHeight.ToString(numfmtinfo));
                    xmlwrite.WriteElementString("NotesTitlepanelMinHeight", Settings.NotesTitlepanelMinHeight.ToString(numfmtinfo));
                    xmlwrite.WriteElementString("NotesTooltipPreviewlength", Settings.NotesTooltipPreviewlength.ToString(numfmtinfo));
                    xmlwrite.WriteElementString("NotesWarnlimitTotal", Settings.NotesWarnlimitTotal.ToString(numfmtinfo));
                    xmlwrite.WriteElementString("NotesWarnlimitVisible", Settings.NotesWarnlimitVisible.ToString(numfmtinfo));
                    xmlwrite.WriteElementString("TrayiconFontsize", Settings.TrayiconFontsize.ToString(numfmtinfo));
                    xmlwrite.WriteElementString("TrayiconLeftclickaction", Settings.TrayiconLeftclickaction.ToString(numfmtinfo));
                    xmlwrite.WriteElementString("UpdatecheckEverydays", Settings.UpdatecheckEverydays.ToString(numfmtinfo));
                    xmlwrite.WriteElementString("HighlightMaxchars", Settings.HighlightMaxchars.ToString(numfmtinfo));
                    xmlwrite.WriteElementString("SettingsLastTab", Settings.SettingsLastTab.ToString(numfmtinfo));
                    xmlwrite.WriteElementString("ManagenotesFontsize", Settings.ManagenotesFontsize.ToString(numfmtinfo));
                    xmlwrite.WriteElementString("ManagenotesSkinnr", Settings.ManagenotesSkinnr.ToString(numfmtinfo));

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
                    xmlwrite.WriteElementString("UpdatecheckGPGKeyserver", Settings.UpdatecheckGPGKeyserver);
                    xmlwrite.WriteElementString("UpdatecheckGPGPath", Settings.UpdatecheckGPGPath);
                    xmlwrite.WriteElementString("UpdatecheckLastDate", Settings.UpdatecheckLastDate.ToString());
                    xmlwrite.WriteElementString("UpdatecheckURL", Settings.UpdatecheckURL.ToString());
                    xmlwrite.WriteElementString("FontContentFamily", Settings.FontContentFamily);
                    xmlwrite.WriteElementString("FontTitleFamily", Settings.FontTitleFamily);
                    xmlwrite.WriteElementString("FontTrayicon", Settings.FontTrayicon);
                    xmlwrite.WriteElementString("ProgramLanguage", Settings.ProgramLanguage);
                    xmlwrite.WriteElementString("ProgramLastrunVersion", Settings.ProgramLastrunVersion);
                    xmlwrite.WriteElementString("ProgramPluginsFolder", Settings.ProgramPluginsFolder);
                    xmlwrite.WriteElementString("ProgramPluginsEnabled", Settings.ProgramPluginsEnabled);
                    xmlwrite.WriteElementString("ProgramPluginsDllexclude", Settings.ProgramPluginsDllexclude);
                    xmlwrite.WriteElementString("NetworkProxyAddress", Settings.NetworkProxyAddress);
                    xmlwrite.WriteElementString("SharingEmailDefaultadres", Settings.SharingEmailDefaultadres);
                    xmlwrite.WriteElementString("NotesSavepath", Settings.NotesSavepath);
                    xmlwrite.WriteEndElement();
                    xmlwrite.WriteEndDocument();
                }
                catch (AccessViolationException)
                {
                    Log.Write(LogType.exception, "Permission problem writing: " + Path.Combine(Program.AppDataFolder, SETTINGSFILE));
                    return false;
                }
                finally
                {
                    xmlwrite.Close();
                }

                return true;
            }
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
            ////        throw new ApplicationException("Cannot parser: " + colorstring);
            ////    }
            ////    else
            ////    {
            ////        throw new ApplicationException("Cannot parser: " + colorstring.Substring(0, 100)+" ..");
            ////    }
            ////}
        }

        /// <summary>
        /// Get the new version as a integer array with first major
        /// second valeau the minor version and the third valeau being the release version.
        /// </summary>
        /// <param name="serverresponse">The server reponse stream.</param>
        /// <param name="versionquality">The latest version quality, e.g: alpha, beta, rc or nothing for final.</param>
        /// <param name="downloadurl">The download url found.</param>
        /// <param name="rsasignature">RSA signature.</param>
        /// <returns>The newest version as integer array, 
        /// any negative valeau(-1 by default) considered as error.</returns>
        public static short[] ParserLatestVersion(string serverresponse, out string versionquality, out string downloadurl, out string rsasignature)
        {
            short[] version = new short[3];
            version[0] = -1;
            version[1] = -1;
            version[2] = -1;
            versionquality = Program.AssemblyVersionQuality;
            downloadurl = string.Empty;
            rsasignature = string.Empty;
            if (string.IsNullOrEmpty(serverresponse))
            {
                return version;
            }

            try
            {
                xmlread = new XmlTextReader(new System.IO.StringReader(serverresponse));
                xmlread.ProhibitDtd = true;
                xmlread.XmlResolver = new XmlSecureResolver(new XmlUrlResolver(), "http://*");
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
                            const int VERQUALITYMAXLEN = 16;
                            if (getquality.Length <= VERQUALITYMAXLEN)
                            {
                                versionquality = getquality;
                            }

                            break;
                        case "downloadurl":
                            string downloadurlraw = xmlread.ReadElementContentAsString().Trim();
                            const int DOWNLOADURLMINLEN = 10; // "http://a.b".Length = 10
                            const int DOWNLOADURLMAXLEN = 512;
                            if ((downloadurlraw.Length >= DOWNLOADURLMINLEN) && (downloadurlraw.Length <= DOWNLOADURLMAXLEN))
                            {
                                downloadurl = downloadurlraw;
                            }

                            break;
                        case "signature":
                            rsasignature = xmlread.ReadElementContentAsString();

                            break;
                        default:

                            break;
                    }

                    if (xmlread.Depth > 3)
                    {
                        xmlread.Close();
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

            Log.Write(LogType.info, "update check done.");
            return version;
        }

        /// <summary>
        /// Return a array of keywords used for the prgramming language we are doing a syntax check on.
        /// </summary>
        /// <param name="file">The file to parser.</param>
        /// <param name="name">The language to lookup.</param>
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
        /// Get the content node a note node
        /// </summary>
        /// <param name="notefilepath">The file to load as xml file.</param>
        /// <param name="limittextchars">The limit on how many characters to read at most with the content node of the xmlfile.</param>
        /// <returns>The valeau of the content node in the xml file limited to the amount of characters given by limittextchars.</returns>
        public static string GetContentStringLimited(string notefilepath, int limittextchars)
        {
            try
            {
                xmlread = new XmlTextReader(notefilepath);
            }
            catch (FileLoadException fileloadexc)
            {
                throw new ApplicationException(fileloadexc.Message);
            }
            catch (FileNotFoundException filenotfoundexc)
            {
                throw new ApplicationException(filenotfoundexc.Message);
            }

            StringBuilder sbcontent = new StringBuilder();
            const int BUFFERSIZE = 10;
            try
            {
                while (xmlread.Read())
                {
                    if (xmlread.Name == "content")
                    {
                        xmlread.MoveToContent();
                        int countchr = 0;
                        char[] buf = new char[BUFFERSIZE];
                        bool rtftagopen = false;
                        int rtflevel = 0;
                        bool stopread = false;
                        while ((countchr = xmlread.ReadChars(buf, 0, BUFFERSIZE)) > 0 && !stopread)
                        {
                            for (int i = 0; i < buf.Length; i++)
                            {
                                if (buf[i] == '\\')
                                {
                                    rtftagopen = true;
                                }

                                if (!rtftagopen && rtflevel == 1 && buf[i] != '{' && buf[i] != '}')
                                {
                                    if (i >= 1)
                                    {
                                        if (buf[i] != '\n' && buf[i - 1] != '}')
                                        {
                                            sbcontent.Append(buf[i]);
                                        }
                                    }
                                    else
                                    {
                                        sbcontent.Append(buf[i]);
                                    }

                                    if (sbcontent.Length >= limittextchars)
                                    {
                                        stopread = true;
                                    }
                                }

                                if (buf[i] == ' ' || buf[i] == '\n' || buf[i] == '\r')
                                {
                                    rtftagopen = false;
                                }
                                else if (buf[i] == '{')
                                {
                                    rtflevel++;
                                }
                                else if (buf[i] == '}')
                                {
                                    rtflevel--;
                                }
                            }
                        }
                    }
                }
            }
            finally
            {
                xmlread.Close();
            }

            return sbcontent.ToString();
        }

        /// <summary>
        /// Write the note node with properties.
        /// </summary>
        /// <param name="xmlwrite">The XmlTextWriter object to write with.</param>
        /// <param name="note">The note object.</param>
        /// <param name="skinname">The skinname used by this note.</param>
        /// <param name="content">The note content.</param>
        public static void WriteNoteBody(XmlTextWriter xmlwrite, Note note, string skinname, string content)
        {
            xmlwrite.WriteStartElement("note");
            xmlwrite.WriteAttributeString("version", NOTEVERSION);
            WriteXMLBool(xmlwrite, "visible", note.Visible);
            WriteXMLBool(xmlwrite, "ontop", note.Ontop);
            WriteXMLBool(xmlwrite, "locked", note.Locked);
            WriteXMLBool(xmlwrite, "rollup", note.RolledUp);
            WriteXMLBool(xmlwrite, "wordwarp", note.Wordwarp);
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

        /// <summary>
        /// Parser a note node in a xml file, 
        /// readnotenum is the number of note node to be parser and returned as note object.
        /// </summary>
        /// <param name="notes">Reference to notes class.</param>
        /// <param name="note">The note object to set.</param>
        /// <param name="readnotenum">The number occurance of the note node to be parser (first, sencod etc.)</param>
        /// <param name="setallcontent">Force to set the note temporary content variable in the note class even if not visible.</param>
        /// <returns>A note object</returns>
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
                        case "wordwarp":
                            note.Wordwarp = xmlread.ReadElementContentAsBoolean();
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
                string[] name = new string[] { "yellow", "orange", "white", "green", "blue", "purple", "red", "dark", "softwhite", "contrastblue", "grass", "colordrops", "nyancat" };
                string[] primaryclr = new string[] { "FFEF14", "FFA700", "FFFFFF", "6FE200", "5A86D5", "FF1AFF", "FF1A1A", "002626", "FFF4C6", "3B47EF", "6FE200", "7D0C9A", "013567" };
                string[] selectclr = new string[] { "E0D616", "C17D00", "E0E0E0", "008000", "1A1AFF", "8B1A8B", "7A1515", "000624", "333366", "00137F", "008000", "000624", "0019A8" };
                string[] highlightclr = new string[] { "FFED7C", "FFD46D", "E5E5E5", "DADBD9", "C6CBD3", "FFC1FF", "FF6F6F", "494949", "FFFFFF", "0026FF", "DADBD9", "494949", "000000" };
                string[] textclr = new string[] { "000000", "000000", "000000", "000000", "000000", "000000", "000000", "FFFFFF", "3B47EF", "FFDF23", "000000", "FFFFFF", "FFFFFF" };
                string[] textures = new string[] { null, null, null, null, null, null, null, null, null, null, "grass.jpg", "colordrops.jpg", "nyancat.jpg" };
                string[] textureslayout = new string[] { null, null, null, null, null, null, null, null, null, null, "tile", "stretch", "center" };
                xmlwrite.WriteAttributeString("count", name.Length.ToString()); // for performance set list Capacity, not required. 
                for (ushort i = 0; i < name.Length; i++)
                {
                    xmlwrite.WriteStartElement("skin");
                    xmlwrite.WriteElementString("Name", name[i]);
                    xmlwrite.WriteStartElement("PrimaryClr");
                    if (textures[i] != null)
                    {
                        xmlwrite.WriteAttributeString("texture", textures[i]);
                        if (textureslayout[i] != null)
                        {
                            xmlwrite.WriteAttributeString("texturelayout", textureslayout[i]);
                        }
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
        /// <param name="xmlwrite">XmlTextWriter object.</param>
        /// <param name="element">The element name</param>
        /// <param name="checknode">The node to check</param>
        private static void WriteXMLBool(XmlTextWriter xmlwrite, string element, bool checknode)
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

        #endregion Methods
    }
}