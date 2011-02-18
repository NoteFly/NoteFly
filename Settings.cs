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
        public static bool notesTransparencyEnabled;

        public static double notesTransparencyLevel;

        public static bool notesDefaultRandomSkin;

        public static int notesDefaultSkinnr;

        public static bool notesTooltipsEnabled;

        public static bool notesClosebtnHidenotepermanently;

        public static bool notesDeleteRecyclebin;

        public static int notesWarnLimit;

        public static string notesSavepath;

        public static int trayiconLeftclickaction;

        public static bool trayiconCreatenotebold;

        public static bool trayiconManagenotesbold;

        public static bool trayiconSettingsbold;

        public static bool trayiconExitbold;

        public static int fontTextdirection;

        public static string fontTitleFamily; 

        public static float fontTitleSize; 

        public static bool fontTitleStylebold; 

        public static string fontContentFamily; 

        public static float fontContentSize; 

        public static bool highlightHTML; 

        public static string highlightHTMLColorInvalid; 

        public static string highlightHTMLColorValid; 

        public static string highlightHTMLColorString; 

        public static bool highlightHyperlinks; 

        public static bool highlightPHP; 

        public static string highlightPHPColorComment; 

        public static string highlightPHPColorDocumentstartend; 

        public static string highlightPHPColorValidfunctions;

        public static string highlightPHPColorInvalidfunctions;

        public static bool highlightSQL; 

        public static bool confirmLinkclick; 

        public static bool confirmExit; 

        public static bool confirmDeletenote; 

        public static bool socialEmailEnabled; 

        public static string socialEmailDefaultadres;

        /*
        public static bool socialTwitterEnabled; 
        public static string socialTwitterUsername; 
        public static bool socialTwitterUseSSL;  //on by default and should not be changable with gui.
        public static bool socialFacebookEnabled;
        public static string socialFacebookEmail;
        public static bool socialFacebookUseSSL;  //on by default and should not be changable with gui.
        */

        public static int networkConnectionTimeout; 

        public static bool networkConnectionForceipv6;

        public static bool networkProxyEnabled; 

        public static string networkProxyAddress; 

        public static int updatecheckEverydays; 

        public static string updatecheckLastDate; 

        public static bool programFirstrun; 

        public static bool programLogInfo; 

        public static bool programLogError; 

        public static bool programLogException; 
    }
}
