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
        private bool enabled = false;
        private string file;

        // Properties (2) 

        /// <summary>
        /// Is the plugin enabled.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return this.enabled;
            }
            set
            {
                this.enabled = value;
            }
        }

        /// <summary>
        /// Gets the plugin filename.
        /// </summary>
        public string Filename
        {
            get
            {
                return this.file;
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

        /// <summary>
        /// Register the plugin
        /// string name, string author, string description, string version, 
        /// </summary>
        /// <param name="enabled"></param>
        /// <param name="file"></param>
        public void Register(bool enabled, string file)
        {
            this.enabled = enabled;
            this.file = file;
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
            // by default return nocontrols, override this to create settings share tab controls
            return null;
        }

        /// <summary>
        /// Executed on opening FrmNewNote.
        /// Create a button in the bottom in FrmNewNote.
        /// </summary>
        /// <returns></returns>
        public virtual Button[] InitFrmNewNoteFormatTools()
        {
            return null;
        }

        /// <summary>
        /// A format button clicked.
        /// </summary>
        /// <param name="rtbnote"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
        public virtual string FormatBtnClicked(System.Windows.Forms.RichTextBox rtbnote, Button sender)
        {
            return rtbnote.Rtf;
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
        /// Executed if a note is being hiden.
        /// </summary>
        public virtual void HidingNote(string content, string title)
        {
            // by default do nothing, override this to do someting.
        }
    }
}
