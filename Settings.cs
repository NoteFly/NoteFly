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
    /// Settings data class.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Are notes transparent.
        /// </summary>
        public static bool notesTransparencyEnabled;

        /// <summary>
        /// The level of transparency.
        /// </summary>
        public static double notesTransparencyLevel;

        /// <summary>
        /// Use random skin as default new note.
        /// </summary>
        public static bool notesDefaultRandomSkin;

        /// <summary>
        /// The default skin on new note.
        /// </summary>
        public static int notesDefaultSkinnr;

        /// <summary>
        /// Are tooltip in programme enabled.
        /// </summary>
        public static bool notesTooltipsEnabled;

        /// <summary>
        /// Hide notes permanently on closing them.
        /// </summary>
        public static bool notesClosebtnHidenotepermanently;

        /// <summary>
        /// Move notes to recycle bin on delete.
        /// </summary>
        public static bool notesDeleteRecyclebin;

        /// <summary>
        /// Number of notes a warning shows up that are many notes loading.
        /// </summary>
        public static int notesWarnLimit;

        /// <summary>
        /// The folder where to save all notes.
        /// </summary>
        public static string notesSavepath;

        /// <summary>
        /// The action on left clicking on trayicon.
        /// 0 is do nothing.
        /// 1 is bring all notes to front.
        /// 2 is create a new note.
        /// </summary>
        public static int trayiconLeftclickaction;

        /// <summary>
        /// Display "Create new note" in bold.
        /// </summary>
        public static bool trayiconCreatenotebold;

        /// <summary>
        /// Display "Manage notes" in bold.
        /// </summary>
        public static bool trayiconManagenotesbold;

        /// <summary>
        /// Display "Settings" in bold.
        /// </summary>
        public static bool trayiconSettingsbold;

        /// <summary>
        /// Display "Exit" in bold.
        /// </summary>
        public static bool trayiconExitbold;

        /// <summary>
        /// The text direction in notes.
        /// 0 is left to right.
        /// 1 is right to left.
        /// </summary>
        public static int fontTextdirection;

        /// <summary>
        /// The font family of the title of notes.
        /// </summary>
        public static string fontTitleFamily; 

        /// <summary>
        /// The notes title font size.
        /// </summary>
        public static float fontTitleSize; 

        /// <summary>
        /// Display the notes title in bold.
        /// </summary>
        public static bool fontTitleStylebold; 

        /// <summary>
        /// The default font family of notes content.
        /// </summary>
        public static string fontContentFamily; 

        /// <summary>
        /// The default font size of notes content.
        /// </summary>
        public static float fontContentSize; 

        /// <summary>
        /// Do HTML highlighting on notes.
        /// </summary>
        public static bool highlightHTML; 

        /// <summary>
        /// The HTML invalid tag color.
        /// </summary>
        public static string highlightHTMLColorInvalid; 

        /// <summary>
        /// The HTML valid tag color.
        /// </summary>
        public static string highlightHTMLColorValid; 

        /// <summary>
        /// The HTML string content color.
        /// </summary>
        public static string highlightHTMLColorString; 

        /// <summary>
        /// Do detect hyperlinks in notes.
        /// </summary>
        public static bool highlightHyperlinks; 

        /// <summary>
        /// Do PHP highlighting.
        /// </summary>
        public static bool highlightPHP; 

        /// <summary>
        /// The PHP comment color
        /// </summary>
        public static string highlightPHPColorComment; 

        /// <summary>
        /// The PHP document start and end keyword colors.
        /// </summary>
        public static string highlightPHPColorDocumentstartend; 

        /// <summary>
        /// The PHP valid function color.
        /// </summary>
        public static string highlightPHPColorValidfunctions;

        /// <summary>
        /// The PHP invalid/unknow function color.
        /// </summary>
        public static string highlightPHPColorInvalidfunctions;

        /// <summary>
        /// Do SQL highlighting
        /// </summary>
        public static bool highlightSQL; 

        /// <summary>
        /// Confirm the launch of hyperlink.
        /// </summary>
        public static bool confirmLinkclick; 

        /// <summary>
        /// Confirm the shutdown of this application.
        /// </summary>
        public static bool confirmExit; 

        /// <summary>
        /// Confirm the deleting on notes.
        /// </summary>
        public static bool confirmDeletenote; 

        /// <summary>
        /// Is email sharing enabled.
        /// </summary>
        public static bool socialEmailEnabled; 

        /// <summary>
        /// The default email adres to send to.
        /// </summary>
        public static string socialEmailDefaultadres;

        /*
        public static bool socialTwitterEnabled; 
        public static string socialTwitterUsername; 
        public static bool socialTwitterUseSSL;  //on by default and should not be changable with gui.
        public static bool socialFacebookEnabled;
        public static string socialFacebookEmail;
        public static bool socialFacebookUseSSL;  //on by default and should not be changable with gui.
        */
        /// <summary>
        /// The time a connection is considered not working.
        /// </summary>
        public static int networkConnectionTimeout; 

        /// <summary>
        /// Force to use of IPv6
        /// </summary>
        public static bool networkConnectionForceipv6;

        /// <summary>
        /// Connect via a proxy.
        /// </summary>
        public static bool networkProxyEnabled; 

        /// <summary>
        /// The address of the proxy server.
        /// </summary>
        public static string networkProxyAddress; 

        /// <summary>
        /// The update check interval in days.
        /// 0 for disabled update checking.
        /// </summary>
        public static int updatecheckEverydays; 

        /// <summary>
        /// The datatime of last update check.
        /// </summary>
        public static string updatecheckLastDate; 

        /// <summary>
        /// Has this programme been running before.
        /// </summary>
        public static bool programFirstrun; 

        /// <summary>
        /// Log debug info.
        /// </summary>
        public static bool programLogInfo; 

        /// <summary>
        /// Log errors user makes
        /// </summary>
        public static bool programLogError; 

        /// <summary>
        /// Log exceptions/errors this programme has.
        /// </summary>
        public static bool programLogException; 
    }
}
