//-----------------------------------------------------------------------
// <copyright file="PluginBase.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2011  Tom
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
namespace IPlugin
{
    using System;
    using System.Reflection;
    using System.Windows.Forms;

    [CLSCompliant(true)]
    public abstract class PluginBase : IPlugin
    {
        private string name;
        private string author;
        private string description;
        private string version;

        // Properties (6) 

        /// <summary>
        /// The name of this plugin
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// The author of this plugin
        /// </summary>
        public string Author
        {
            get
            {
                return this.author;
            }
        }

        /// <summary>
        /// The description of this plugin
        /// </summary>
        public string Description
        {
            get
            {
                return this.description;
            }
        }

        /// <summary>
        /// The version of this plugin
        /// </summary>
        public string Version
        {
            get
            {
                return this.version;
            }
        }

        /// <summary>
        /// Settings share tab title.
        /// Tab not created if null.
        /// </summary>
        public virtual string SettingsTabTitle
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Note share to menu text.
        /// MenuItem not created if null.
        /// </summary>
        public virtual string ShareMenuText
        {
            get
            {
                return null;
            }
        }

        // Methods (7) 

        public void Register(string name, string author, string description, string version)
        {
            this.name = name;
            this.author = author;
            this.description = description;
            this.version = version;
        }

        /// <summary>
        /// Executed if share menu clicked.
        /// </summary>
        /// <param name="rtbnote">The richedit component with the note content in memory.</param>
        /// <param name="note">note object</param>
        public virtual void ShareMenuClicked(System.Windows.Forms.RichTextBox rtbnote, string title)
        {
            // by default  do nothing, override this to do someting.
        }

        /// <summary>
        /// Executed if settings tab loaded.
        /// </summary>
        /// <returns>a Tabpage with all components to draw</returns>
        public virtual TabPage InitShareSettingsTab()
        {
            // by default return nocontrols, override this to create settings share tab contriols
            return null;
        }

        /// <summary>
        /// Executed if Ok on FrmSettings is pressed.
        /// </summary>
        /// <returns>true if allowed to close FrmSettings</returns>
        public virtual bool SaveSettingsTab()
        {
            // by default return true, for everything is okay, allowed to close FrmSettings of NoteFly.
            return true;
        }

        /// <summary>
        /// Executed if a note is saved
        /// </summary>
        /// <param name="note">A note object with details</param>
        public virtual void SavingNote(string content, string title)
        {
            // by default do nothing, override this to do someting.
        }

        /// <summary>
        /// Executed if a note is made visible
        /// </summary>
        public virtual void ShowingNote(string content, string title)
        {
            // by default do nothing, override this to do someting.
        }
        
        /// <summary>
        /// Executed if a note is made visible
        /// </summary>
        /// <returns>Note content</returns>
        //public virtual void ShowingNote(string content, string title, out string newcontent)
        //{
        //    // by default do nothing, override this to do someting.
        //    newcontent = String.Empty;
        //}

        /// <summary>
        /// Executed if a note is being hiden.
        /// </summary>
        public virtual void HidingNote(string content, string title)
        {
            // by default do nothing, override this to do someting.
        }
    }
}
