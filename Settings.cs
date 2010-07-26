//-----------------------------------------------------------------------
// <copyright file="Settings.cs" company="GNU">
// 
// This program is free software; you can redistribute it and/or modify it
// Free Software Foundation; either version 2, 
// or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// </copyright>
//-----------------------------------------------------------------------
namespace NoteFly
{
    using System;

    /// <summary>
    /// The setting class.
    /// </summary>
    public static class Settings
    {
        public bool NotesTransparencyEnabled { get; set; }

        public int NotesTransparencyLevel { get; set; }

        public int NotesDefaultColor { get; set; }

        public bool NotesClosebtnTooltipenabled { get; set; }

        public bool NotesClosebtnHidenotepermanently { get; set; }

        public string NotesSavepath { get; set; }

        public int TrayiconLeftclickaction { get; set; }

        public bool TrayiconCreatenotebold { get; set; }

        public bool TrayiconManagenotesbold { get; set; }

        public bool TrayiconSettingsbold { get; set; }

        public bool TrayiconExitbold { get; set; }

        public int FontTextdirection { get; set; }

        public string FontTitleFamily { get; set; }

        public int FontTitleSize { get; set; }

        public bool FontTitleStylebold { get; set; }

        public string FontContentFamily { get; set; }

        public Decimal FontContentSize { get; set; }

        public bool HighlightHTML { get; set; }

        public bool HighlightHyperlinks { get; set; }

        public bool HighlightPHP { get; set; }

        public bool HighlightSQL { get; set; }

        public bool ConfirmLinkclick { get; set; }

        public bool ConfirmExit { get; set; }

        public bool ConfirmDeletenote { get; set; }

        public bool SocialEmailEnabled { get; set; }

        public string SocialEmailDefaultadres { get; set; }

        public bool SocialTwitterEnabled { get; set; }

        public string SocialTwitterUsername { get; set; }

        public string SocialTwitterpassword { get; set; }

        public bool SocialTwitterUseSSL { get; set; } //default on and not changed throw gui.

        public bool SocialFacebookEnabled { get; set; }

        public bool SocialFacebookSavesession { get; set; }

        public bool SocialFacebookUseSSL { get; set; }

        public int NetworkConnectionTimeout { get; set; }

        public bool NetworkConnectionForceipv6 { get; set; } //e.g. set ipv6.facebook.com

        public bool NetworkProxyEnabled { get; set; }

        public string NetworkProxyAddress { get; set; }

        public bool ProgramFirstrun { get; set; }

        public bool ProgramLogError { get; set; }

        public bool ProgramLogException { get; set; }

        public bool ProgramLogInfo { get; set; }
    }
}
