//-----------------------------------------------------------------------
// <copyright file="Settings.cs" company="GNU">
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
    /// <summary>
    /// Settings struct.
    /// </summary>
    public struct Settings
    {
        /// <summary>
        /// Are notes transparent.
        /// </summary>
        public static bool NotesTransparencyEnabled = true;

        /// <summary>
        /// Is RichEditTextbox transparent.
        /// </summary>
        public static bool NotesTransparentRTB = true;

        /// <summary>
        /// The level of transparency.
        /// </summary>
        public static double NotesTransparencyLevel = 0.9;

        /// <summary>
        /// Use random skin as default new note.
        /// </summary>
        public static bool NotesDefaultRandomSkin = false;

        /// <summary>
        /// The default skin on new note.
        /// </summary>
        public static int NotesDefaultSkinnr = 0;

        /// <summary>
        /// Are tooltip in programme enabled.
        /// </summary>
        public static bool NotesTooltipsEnabled = true;

        /// <summary>
        /// Hide notes permanently on closing them.
        /// </summary>
        public static bool NotesClosebtnHidenotepermanently = true;

        /// <summary>
        /// Move notes to recycle bin on delete.
        /// </summary>
        public static bool NotesDeleteRecyclebin = false;

        /// <summary>
        /// Number of notes a warning shows up that are many notes loading.
        /// </summary>
        public static int NotesWarnLimit = 250;

        /// <summary>
        /// The folder where to save all notes.
        /// </summary>
        public static string NotesSavepath;

        /// <summary>
        /// The action on left clicking on trayicon.
        /// 0 is do nothing.
        /// 1 is bring all notes to front.
        /// 2 is create a new note.
        /// </summary>
        public static int TrayiconLeftclickaction = 1;

        /// <summary>
        /// The trayicon fontsize.
        /// </summary>
        public static float TrayiconFontsize = 8.25f;

        /// <summary>
        /// Display "Create new note" in bold.
        /// </summary>
        public static bool TrayiconCreatenotebold = true;

        /// <summary>
        /// Display "Manage notes" in bold.
        /// </summary>
        public static bool TrayiconManagenotesbold = false;

        /// <summary>
        /// Display "Settings" in bold.
        /// </summary>
        public static bool TrayiconSettingsbold = false;

        /// <summary>
        /// Display "Exit" in bold.
        /// </summary>
        public static bool TrayiconExitbold = false;

        /// <summary>
        /// The text direction in notes.
        /// 0 is left to right.
        /// 1 is right to left.
        /// </summary>
        public static int FontTextdirection = 0;
#if windows
        /// <summary>
        /// The font family of the title of notes.
        /// </summary>
        public static string FontTitleFamily = "Arial";
#elif linux
        /// <summary>
        /// The default font family of notes content.
        /// </summary>
        public static string FontTitleFamily = "FreeMono";
#else
		/// <summary>
        /// The default font family of notes content.
        /// </summary>
        public static string FontTitleFamily = "?";
#endif
        /// <summary>
        /// The notes title font size.
        /// </summary>
        public static float FontTitleSize = 14;

        /// <summary>
        /// Display the notes title in bold.
        /// </summary>
        public static bool FontTitleStylebold = true;
#if windows
        /// <summary>
        /// The default font family of notes content.
        /// </summary>
        public static string FontContentFamily = "Arial";
#elif linux
        /// <summary>
        /// The default font family of notes content.
        /// </summary>
        public static string FontContentFamily = "FreeMono";
#else
        /// <summary>
        /// The default font family of notes content.
        /// </summary>
        public static string FontContentFamily = "?";
#endif
        /// <summary>
        /// The default font size of notes content.
        /// </summary>
        public static float FontContentSize = 11;

        /// <summary>
        /// The maximum number of characters to apply syntaxcheck on.
        /// </summary>
        public static int HighlightMaxchars = 10000;

        /// <summary>
        /// Do HTML highlighting on notes.
        /// </summary>
        public static bool HighlightHTML = false;

        /// <summary>
        /// The HTML comment color
        /// </summary>
        public static string HighlightHTMLColorComment = "#B200FF";

        /// <summary>
        /// The HTML invalid tag color.
        /// </summary>
        public static string HighlightHTMLColorInvalid = "#FF0000";

        /// <summary>
        /// The HTML valid tag color.
        /// </summary>
        public static string HighlightHTMLColorValid = "#0026FF";

        /// <summary>
        /// The HTML string content color.
        /// </summary>
        public static string HighlightHTMLColorString = "#808080"; 

        /// <summary>
        /// Do detect hyperlinks in notes.
        /// </summary>
        public static bool HighlightHyperlinks = true;

        /// <summary>
        /// Do PHP highlighting.
        /// </summary>
        public static bool HighlightPHP = false;

        /// <summary>
        /// The PHP comment color
        /// </summary>
        public static string HighlightPHPColorComment = "#686868";

        /// <summary>
        /// The PHP document start and end keyword colors.
        /// </summary>
        public static string HighlightPHPColorDocumentstartend = "#129612";

        /// <summary>
        /// The HTML string content color.
        /// </summary>
        public static string HighlightPHPColorString = "#404040";

        /// <summary>
        /// The PHP valid function color.
        /// </summary>
        public static string HighlightPHPColorValidfunctions = "#41D87B";

        /// <summary>
        /// The PHP invalid/unknow function color.
        /// </summary>
        public static string HighlightPHPColorInvalidfunctions = "#D90000";

        /// <summary>
        /// Do SQL highlighting
        /// </summary>
        public static bool HighlightSQL = false;

        /// <summary>
        /// A valid SQL statement color.
        /// </summary>
        public static string HighlightSQLColorValidstatement = "#7FCE35";

        /// <summary>
        /// A SQL field color.
        /// </summary>
        public static string HighlightSQLColorField = "#B16DFF";

        /// <summary>
        /// Confirm the launch of hyperlink.
        /// </summary>
        public static bool ConfirmLinkclick = true;

        /// <summary>
        /// Confirm the shutdown of this application.
        /// </summary>
        public static bool ConfirmExit = false; 

        /// <summary>
        /// Confirm the deleting on notes.
        /// </summary>
        public static bool ConfirmDeletenote = true;

        /// <summary>
        /// Show expert settings in FrmSettings.
        /// </summary>
        public static bool SettingsExpertEnabled = false;

        /// <summary>
        /// The last tab selected on FrmSettings before closing it.
        /// </summary>
        public static int SettingsLastTab = 0;

        /// <summary>
        /// Is email sharing enabled.
        /// </summary>
        public static bool SocialEmailEnabled = true;

        /// <summary>
        /// The default email adres to send to.
        /// </summary>
        public static string SocialEmailDefaultadres = string.Empty;

        /// <summary>
        /// The time a connection is considered not working.
        /// </summary>
        public static int NetworkConnectionTimeout = 8000;

        /// <summary>
        /// Force to use of IPv6
        /// </summary>
        public static bool NetworkConnectionForceipv6 = false;

        /// <summary>
        /// Connect via a proxy.
        /// </summary>
        public static bool NetworkProxyEnabled = false;

        /// <summary>
        /// The address of the proxy server.
        /// </summary>
        public static string NetworkProxyAddress = string.Empty;

        /// <summary>
        /// The update check interval in days.
        /// 0 for disabled update checking.
        /// </summary>
        public static int UpdatecheckEverydays = 14;

        /// <summary>
        /// The datatime of last update check.
        /// If node not found then default will be start of unix time.
        /// </summary>
        public static string UpdatecheckLastDate = "1-1-1970 00:00:00";

        /// <summary>
        /// The location where notefly checks for update.
        /// </summary>
        public static string UpdatecheckURL = "http://update.notefly.org/latestversion.xml";

        /// <summary>
        /// Use GnuGP to verif the downloaded update.
        /// </summary>
        public static bool UpdatecheckUseGPG = false;

        /// <summary>
        /// The path to the location of where gpg.exe is located required to verif the downloaded update if enabled.
        /// </summary>
        public static string UpdatecheckGPGPath = string.Empty;

        /// <summary>
        /// Has this programme been runned before.
        /// If not the demo note is created, balloontip is showed
        /// and NoteFly version 1.0 notes are ask to are imported if they exist.
        /// </summary>
        public static bool ProgramFirstrun = false;

        /// <summary>
        /// Suspress a warning that the programme is running with dangerous (evelated) administrator.
        /// </summary>
        public static bool ProgramSuspressWarnAdmin = false;

        /// <summary>
        /// Is loading plugins enable
        /// </summary>
        public static bool ProgramPluginsAllEnabled = true;

        /// <summary>
        /// 
        /// </summary>
        public static string ProgramPluginsEnabled = string.Empty;

        /// <summary>
        /// The relative path from the install directory to the folder where plugins are stored in.
        /// </summary>
        public static string ProgramPluginsFolder = System.IO.Path.Combine(Program.InstallFolder, "plugins");

        /// <summary>
        /// Log debug info.
        /// </summary>
        public static bool ProgramLogInfo = false;

        /// <summary>
        /// Log errors user makes
        /// </summary>
        public static bool ProgramLogError = true;

        /// <summary>
        /// Log exceptions/errors this programme has.
        /// If undefined then always true.
        /// </summary>
        public static bool ProgramLogException = true;
    }
}