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
        public static bool notesTransparencyEnabled; //{ get; set; }

        public static double notesTransparencyLevel;//{ get; set; }

        public static int notesDefaultSkinnr;//{ get; set; }

        public static bool notesTooltipsEnabled;//{ get; set; }

        public static bool notesClosebtnHidenotepermanently;//{ get; set; }

        public static int notesWarnLimit;//{ get; set; }

        public static string notesSavepath; //{ get; set; }

        public static int trayiconLeftclickaction;//{ get; set; }

        public static bool trayiconCreatenotebold;//{ get; set; }

        public static bool trayiconManagenotesbold;//{ get; set; }

        public static bool trayiconSettingsbold;//{ get; set; }

        public static bool trayiconExitbold;//{ get; set; }

        public static int fontTextdirection;//{ get; set; }

        public static string fontTitleFamily; //{ get; set; }

        public static float fontTitleSize; //{ get; set; }

        public static bool fontTitleStylebold; //{ get; set; }

        public static string fontContentFamily; //{ get; set; }

        public static float fontContentSize; //{ get; set; }

        public static bool highlightHTML; //{ get; set; }

        public static string highlightHTMLColorInvalid; //{ get; set; }

        public static string highlightHTMLColorValid; //{ get; set; }

        public static string highlightHTMLColorString; //{ get; set; }

        public static bool highlightHyperlinks; //{ get; set; }

        public static bool highlightPHP; //{ get; set; }

        public static string highlightPHPColorComment; //{ get; set; }

        public static string highlightPHPColorDocumentstartend; //{ get; set; }

        public static string highlightPHPColorValidfunctions; //{ get; set; }

        public static bool highlightSQL; //{ get; set; }

        public static bool confirmLinkclick; //{ get; set; }

        public static bool confirmExit; //{ get; set; }

        public static bool confirmDeletenote; //{ get; set; }

        public static bool socialEmailEnabled; //{ get; set; }

        public static string socialEmailDefaultadres; //{ get; set; }

        public static bool socialTwitterEnabled; //{ get; set; }

        public static string socialTwitterUsername; //{ get; set; }

        public static bool socialTwitterUseSSL; //{ get; set; } //on by default and should not be changable with gui.

        public static bool socialFacebookEnabled; //{ get; set; }

        public static bool socialFacebookUseSSL; //{ get; set; } //on by default and should not be changable with gui.

        //public static string SocialFacebookSessionKey; //{ get; set; } //Decreated, use OAuth instead.
        //public static string SocialFacebookSessionSecret; //{ get; set; } //Decreated
        //public static string SocialFacebookSessionExpires; //{ get; set; } //Decreated

        public static int networkConnectionTimeout; //{ get; set; }

        public static bool networkConnectionForceipv6; //{ get; set; } //e.g. set ipv6.facebook.com

        public static bool networkProxyEnabled; //{ get; set; }

        public static string networkProxyAddress; //{ get; set; }

        public static int  updatecheckEverydays; //{ get; set; }

        public static string  updatecheckLastDate; //{ get; set; }

        public static bool programFirstrun; //{ get; set; }

        public static bool programLogInfo; //{ get; set; }

        public static bool programLogError; //{ get; set; }

        public static bool programLogException; //{ get; set; }
    }
}
