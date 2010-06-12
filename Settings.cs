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
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The setting class.
    /// </summary>
    class Settings
    {
        public Boolean Transparency { get; set; }

        public int ProcTransparency { get; set; }

        public int DefaultColor { get; set; }

        public int ActionLeftClick { get; set; }

        public Boolean ConfirmLink { get; set; }

        public String fontcontent { get; set; }

        public Decimal fontsize { get; set; }

        public int textdirection { get; set; }

        public String notesavepath { get; set; }

        public String defaultemail { get; set; }

        public Boolean highlightHTML { get; set; }

        public Boolean confirmexit { get; set; }

        public Boolean confirmdelete { get; set; }

        public String twitteruser { get; set; }

        public String twitterpassword { get; set; }

        public Boolean logerror { get; set; }

        public Boolean loginfo { get; set; }

        public Boolean useproxy { get; set; }

        public String proxyddr { get; set; }

        public int timeout { get; set; }

        public Boolean firstrun { get; set; }

        public Boolean savefacebooksession { get; set; }
    }
}
