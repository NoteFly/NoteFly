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
    using System;

    /// <summary>
    /// Settings struct.
    /// </summary>
    public struct Settings
    {
        /// <summary>
        /// Are notes transparent.
        /// </summary>
        public static bool NotesTransparencyEnabled;

        /// <summary>
        /// The level of transparency.
        /// </summary>
        public static double NotesTransparencyLevel;

        /// <summary>
        /// Use random skin as default new note.
        /// </summary>
        public static bool NotesDefaultRandomSkin;

        /// <summary>
        /// The default skin on new note.
        /// </summary>
        public static int NotesDefaultSkinnr;

        /// <summary>
        /// Are tooltip in programme enabled.
        /// </summary>
        public static bool NotesTooltipsEnabled;

        /// <summary>
        /// Hide notes permanently on closing them.
        /// </summary>
        public static bool NotesClosebtnHidenotepermanently;

        /// <summary>
        /// Move notes to recycle bin on delete.
        /// </summary>
        public static bool NotesDeleteRecyclebin;

        /// <summary>
        /// Number of notes a warning shows up that are many notes loading.
        /// </summary>
        public static int NotesWarnLimit;

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
        public static int TrayiconLeftclickaction;

        /// <summary>
        /// The trayicon fontsize.
        /// </summary>
        public static float TrayiconFontsize = 8.25f;

        /// <summary>
        /// Display "Create new note" in bold.
        /// </summary>
        public static bool TrayiconCreatenotebold;

        /// <summary>
        /// Display "Manage notes" in bold.
        /// </summary>
        public static bool TrayiconManagenotesbold;

        /// <summary>
        /// Display "Settings" in bold.
        /// </summary>
        public static bool TrayiconSettingsbold;

        /// <summary>
        /// Display "Exit" in bold.
        /// </summary>
        public static bool TrayiconExitbold;

        /// <summary>
        /// The text direction in notes.
        /// 0 is left to right.
        /// 1 is right to left.
        /// </summary>
        public static int FontTextdirection;

        /// <summary>
        /// The font family of the title of notes.
        /// </summary>
        public static string FontTitleFamily; 

        /// <summary>
        /// The notes title font size.
        /// </summary>
        public static float FontTitleSize; 

        /// <summary>
        /// Display the notes title in bold.
        /// </summary>
        public static bool FontTitleStylebold; 

        /// <summary>
        /// The default font family of notes content.
        /// </summary>
        public static string FontContentFamily; 

        /// <summary>
        /// The default font size of notes content.
        /// </summary>
        public static float FontContentSize; 

        /// <summary>
        /// Do HTML highlighting on notes.
        /// </summary>
        public static bool HighlightHTML; 

        /// <summary>
        /// The HTML invalid tag color.
        /// </summary>
        public static string HighlightHTMLColorInvalid; 

        /// <summary>
        /// The HTML valid tag color.
        /// </summary>
        public static string HighlightHTMLColorValid; 

        /// <summary>
        /// The HTML string content color.
        /// </summary>
        public static string HighlightHTMLColorString; 

        /// <summary>
        /// Do detect hyperlinks in notes.
        /// </summary>
        public static bool HighlightHyperlinks; 

        /// <summary>
        /// Do PHP highlighting.
        /// </summary>
        public static bool HighlightPHP; 

        /// <summary>
        /// The PHP comment color
        /// </summary>
        public static string HighlightPHPColorComment; 

        /// <summary>
        /// The PHP document start and end keyword colors.
        /// </summary>
        public static string HighlightPHPColorDocumentstartend; 

        /// <summary>
        /// The PHP valid function color.
        /// </summary>
        public static string HighlightPHPColorValidfunctions;

        /// <summary>
        /// The PHP invalid/unknow function color.
        /// </summary>
        public static string HighlightPHPColorInvalidfunctions;

        /// <summary>
        /// Do SQL highlighting
        /// </summary>
        public static bool HighlightSQL; 

        /// <summary>
        /// Confirm the launch of hyperlink.
        /// </summary>
        public static bool ConfirmLinkclick; 

        /// <summary>
        /// Confirm the shutdown of this application.
        /// </summary>
        public static bool ConfirmExit; 

        /// <summary>
        /// Confirm the deleting on notes.
        /// </summary>
        public static bool ConfirmDeletenote; 

        /// <summary>
        /// Is email sharing enabled.
        /// </summary>
        public static bool SocialEmailEnabled; 

        /// <summary>
        /// The default email adres to send to.
        /// </summary>
        public static string SocialEmailDefaultadres;

        /// <summary>
        /// The time a connection is considered not working.
        /// </summary>
        public static int NetworkConnectionTimeout; 

        /// <summary>
        /// Force to use of IPv6
        /// </summary>
        //public static bool networkConnectionForceipv6;

        /// <summary>
        /// Connect via a proxy.
        /// </summary>
        public static bool NetworkProxyEnabled; 

        /// <summary>
        /// The address of the proxy server.
        /// </summary>
        public static string NetworkProxyAddress; 

        /// <summary>
        /// The update check interval in days.
        /// 0 for disabled update checking.
        /// </summary>
        public static int UpdatecheckEverydays; 

        /// <summary>
        /// The datatime of last update check.
        /// </summary>
        public static string UpdatecheckLastDate;

        /// <summary>
        /// Has this programme been runned before.
        /// If not the demo note is created, balloontip is showed
        /// and NoteFly version 1.0 notes are ask to are imported if they exist.
        /// </summary>
        public static bool ProgramFirstrun;

        /// <summary>
        /// Log debug info.
        /// </summary>
        public static bool ProgramLogInfo;

        /// <summary>
        /// Log errors user makes
        /// </summary>
        public static bool ProgramLogError;

        /// <summary>
        /// Log exceptions/errors this programme has.
        /// If undefined then always true.
        /// </summary>
        public static bool ProgramLogException = true;
    }
}
