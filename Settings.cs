//-----------------------------------------------------------------------
// <copyright file="Settings.cs" company="NoteFly">
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
    /// <summary>
    /// Settings struct.
    /// </summary>
    public struct Settings
    {
        /// <summary>
        /// New note hotkey enabled.
        /// </summary>
        public static bool HotkeysNewNoteEnabled = true;

        /// <summary>
        /// New note hotkey use alt as second key.
        /// </summary>
        public static bool HotkeysNewNoteAltInsteadShift = true;

        /// <summary>
        /// New note hotkey final key.
        /// Default key: N
        /// </summary>
        public static int HotkeysNewNoteKeycode = 112;

        /// <summary>
        /// Manage notes hotkey enabled.
        /// </summary>
        public static bool HotkeysManageNotesEnabled = true;

        /// <summary>
        /// Manage notes hotkey use alt as second key.
        /// </summary>
        public static bool HotkeysManageNotesAltInsteadShift = true;

        /// <summary>
        /// Manage notes hotkey final key.
        /// Default key: M
        /// </summary>
        public static int HotkeysManageNotesKeycode = 113;

        /// <summary>
        /// Notes to front hotkey enabled.
        /// </summary>
        public static bool HotkeysNotesToFrontEnabled = true;

        /// <summary>
        /// Notes to front hotkey use alt as second key.
        /// </summary>
        public static bool HotkeysNotesToFrontAltInsteadShift = true;

        /// <summary>
        /// Notes to front hotkey final key.
        /// Default key: F
        /// </summary>
        public static int HotkeysNotesToFrontKeycode = 160;

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
        /// Use current date (in current culture, windows language settings) as default title for a new note.
        /// </summary>
        public static bool NotesDefaultTitleDate = true;

        /// <summary>
        /// Are tooltip in programme enabled.
        /// </summary>
        public static bool NotesTooltipsEnabled = true;

        /// <summary>
        /// Length of the tooltip preview
        /// </summary>
        public static int NotesTooltipPreviewlength = 100;

        /// <summary>
        /// Hide notes permanently on closing them.
        /// </summary>
        public static bool NotesClosebtnHidenotepermanently = true;

        /// <summary>
        /// Move notes to recycle bin on delete.
        /// </summary>
        public static bool NotesDeleteRecyclebin = false;

        /// <summary>
        /// The default width of a new note.
        /// </summary>
        public static int NotesDefaultWidth = 280;

        /// <summary>
        /// The default height of a new note.
        /// </summary>
        public static int NotesDefaultHeight = 240;

        /// <summary>
        /// The maximum number of pixels of the title panel height.
        /// </summary>
        public static int NotesTitlepanelMaxHeight = 64;

        /// <summary>
        /// The minimum number of pixels of the title panel height.
        /// </summary>
        public static int NotesTitlepanelMinHeight = 32;

        /// <summary>
        /// Number of total notes a warning shows up that are many notes loading.
        /// </summary>
        public static int NotesWarnlimitTotal = 5000;

        /// <summary>
        /// Number of visible notes a warning shows up that are many notes being displayed and it recommeded to hide some.
        /// </summary> 
        public static int NotesWarnlimitVisible = 50;

        /// <summary>
        /// The folder where to save all notes.
        /// </summary>
        public static string NotesSavepath = Program.GetDefaultNotesFolder();

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
        public static float TrayiconFontsize = 10.00f;

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
        /// Use a white trayicon instead of yellow fits beter with other default windows7 trayicons
        /// </summary>
        public static bool TrayiconAlternateIcon = false;

        /// <summary>
        /// The text direction in notes.
        /// 0 is left to right.
        /// 1 is right to left.
        /// </summary>
        public static int FontTextdirection = 0;

        /// <summary>
        /// The font family of the title of notes.
        /// </summary>
        public static string FontTitleFamily = "Arial";

        /// <summary>
        /// The notes title font size.
        /// </summary>
        public static float FontTitleSize = 14;

        /// <summary>
        /// Display the notes title in bold.
        /// </summary>
        public static bool FontTitleStylebold = true;

        /// <summary>
        /// The default font family of notes content.
        /// </summary>
        public static string FontContentFamily = "Arial";

        /// <summary>
        /// The font of the trayicon menu.
        /// </summary>
        public static string FontTrayicon = "Arial";

        /// <summary>
        /// The default font size of notes content.
        /// </summary>
        public static float FontContentSize = 11;

        /// <summary>
        /// The maximum number of characters to apply syntaxcheck on.
        /// </summary>
        public static int HighlightMaxchars = 30000;

        /// <summary>
        /// Do HTML highlighting on notes.
        /// </summary>
        public static bool HighlightHTML = false;

        /// <summary>
        /// Make highlighting slower by removing keyword language lexicon after use, but clears more memory.
        /// </summary>
        public static bool HighlightClearLexiconMemory = false;

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
        public static bool HighlightHyperlinks = false;

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
        /// Font used in the manages notes window.
        /// </summary>
        public static string ManagenotesFontFamily = "Arial";

        /// <summary>
        /// Display toolstip with note content in FrmManageNotes.
        /// </summary>
        public static bool ManagenotesTooltip = true;

        /// <summary>
        /// The skin for the manage notes window.
        /// </summary>
        public static int ManagenotesSkinnr = 1;

        /// <summary>
        /// The manage notes font size for each row.
        /// </summary>
        public static float ManagenotesFontsize = 9;

        /// <summary>
        /// Search is case senstive in manage notes window.
        /// </summary>
        public static bool ManagenotesSearchCasesentive = false;

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
        /// notice: renamed SocialEmailEnabled to SharingEmailEnabled
        /// </summary>
        public static bool SharingEmailEnabled = true;

        /// <summary>
        /// The default email adres to send to.
        /// notice: renamed SharingEmailDefaultadres to SharingEmailDefaultadres
        /// </summary>
        public static string SharingEmailDefaultadres = string.Empty;

        /// <summary>
        /// The time a connection is considered not working.
        /// </summary>
        public static int NetworkConnectionTimeout = 8000;

        /// <summary>
        /// The prefered IP version used.
        /// 0 for automatically IPv6 or IPv4.
        /// 1 for force the use of IPv4
        /// 2 for force the use of IPv6
        /// </summary>
        public static int NetworkIPversion = 0;

        /// <summary>
        /// Connect via a proxy.
        /// </summary>
        public static bool NetworkProxyEnabled = false;

        /// <summary>
        /// The address of the proxy server.
        /// </summary>
        public static string NetworkProxyAddress = string.Empty;

        /// <summary>
        /// The port used of the proxy server.
        /// </summary>
        public static int NetworkProxyPort = 80;

        /// <summary>
        /// Use Gzip for http tranfer
        /// </summary>
        public static bool NetworkUseGzip = false;

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
        /// The keyserver used to get the public key of NoteFly the first time.
        /// </summary>
        public static string UpdatecheckGPGKeyserver = string.Empty;

        /// <summary>
        /// Use GnuGP to verify the downloaded update.
        /// </summary>
        public static bool UpdatecheckUseGPG = false;

        /// <summary>
        /// The maximum amount of time in miliseconds GnuPG is given to return the result of validating a file.
        /// On a slow (embedded)system you may need to increase this.
        /// </summary>
        public static int UpdatecheckTimeoutGPG = 6000;

        /// <summary>
        /// The path to the location of where gpg.exe is located required to verif the downloaded update if enabled.
        /// </summary>
        public static string UpdatecheckGPGPath = string.Empty;

        /// <summary>
        /// Silently install updates, don't show setup window.
        /// Passes /S parameter to NSIS installer.
        /// </summary>
        public static bool UpdateSilentInstall = false;

        /// <summary>
         /// The update plugins check interval in days of all plugins.
        /// </summary>
        public static int UpdatecheckPluginsEverydays = 3;

        /// <summary>
        /// The datatime of last update check of all plugins.
        /// </summary>
        public static string UpdatecheckPluginsLastDate = "1-1-1970 00:00:00";

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

        /// <summary>
        /// Has this programme been runned before.
        /// If not the demo note is created, balloontip is showed
        /// and NoteFly version 1.0 notes are ask to are imported if they exist.
        /// </summary>
        public static bool ProgramFirstrunned = false;

        /// <summary>
        /// Use https protocol handler for all links.
        /// </summary>
        public static bool ProgramHttpsLinks = true;

        /// <summary>
        /// Are all windows double buffered.
        /// If on redrawn does not cause blanking windows.
        /// But currenlt false because it has serious issues under windows8 beta.
        /// </summary>
        public static bool ProgramFormsDoublebuffered = false;

        /// <summary>
        /// The culture code of this programme.
        /// </summary>
        public static string ProgramLanguage = "en";

        /// <summary>
        /// The version of the programme of the last run.
        /// If it's different with current version now running,
        /// it can used for updating skins.xml or langs.xml files.
        /// </summary>
        public static string ProgramLastrunVersion = Program.AssemblyVersionAsString;

        /// <summary>
        /// Suspress a warning that the programme is running with dangerous (evelated) administrator.
        /// </summary>
        public static bool ProgramSuspressWarnAdmin = false;

        /// <summary>
        /// Is loading plugins enable
        /// </summary>
        public static bool ProgramPluginsAllEnabled = true;

        /// <summary>
        /// Comma seperated list of plugin assembly filesnames without path that are enabled.
        /// </summary>
        public static string ProgramPluginsEnabled = string.Empty;

        /// <summary>
        /// Dll files in the plugin directory that are not trying to be loaded as plugin.
        /// This are dll files are used by some plugin itself and these dll files don't implement the IPlugin interface.
        /// </summary>
        public static string ProgramPluginsDllexclude = "SQLite3.dll|System.Data.SQLite.DLL|Interop.SpeechLib.dll";

        /// <summary>
        /// The relative path from the install directory to the folder where plugins are stored in.
        /// </summary>
        public static string ProgramPluginsFolder = Program.GetDefaultPluginFolder();
    }
}