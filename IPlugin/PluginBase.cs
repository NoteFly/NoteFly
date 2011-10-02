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

    /// <summary>
    /// PluginBase class.
    /// </summary>
    [CLSCompliant(true)]    
    public abstract class PluginBase : IPlugin
    {
        /// <summary>
        /// A value indicating wheter this plugin is enabled.
        /// </summary>
        private bool enabled = false;

        /// <summary>
        /// The filename of this plugin.
        /// </summary>
        private string file;

        // Properties (2) 

        /// <summary>
        /// Gets or sets a value indicating whether the plugin is enabled.
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

        // Methods (15) 

        /// <summary>
        /// Register the plugin
        /// string name, string author, string description, string version, 
        /// </summary>
        /// <param name="enabled">Is the plugin enabled.</param>
        /// <param name="file">The plugin file.</param>
        public void Register(bool enabled, string file)
        {
            this.enabled = enabled;
            this.file = file;
        }

        /// <summary>
        /// Adds ToolStripItem to the right click submenu share on FrmNote.
        /// </summary>
        /// <returns>The ToolStripMenuItem to add to the Share submenu</returns>
        public virtual ToolStripMenuItem InitFrmNoteShareMenu()
        {
            return null;
        }

        /// <summary>
        /// Executed if share menu clicked.
        /// </summary>
        /// <param name="rtbnote">The richedit component with the note content in memory.</param>
        /// <param name="title">The title of the note</param>
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
        /// <returns>Array with buttons, return null by default</returns>
        public virtual Button[] InitNoteFormatBtns()
        {
            return null;
        }

        /// <summary>
        /// Adds ToolStripItem to the right click menu on FrmNewNote.
        /// </summary>
        /// <returns>A ToolStripItem to be add to the contextmenu on FrmNewNote.</returns>
        public virtual ToolStripItem InitFrmNewNoteMenu()
        {
            return null;
        }

        /// <summary>
        /// Adds contextmenustrip item to the right click menu on FrmNote.
        /// </summary>
        /// <returns>A ToolStripItem item to add to FrmNote contextmenustrip</returns>
        public virtual ToolStripItem InitFrmNoteMenu()
        {
            return null;
        }

        /// <summary>
        /// Adds contextmenustrip item to the right click menu on the trayicon.
        /// </summary>
        /// <returns>A ToolStripItem to be add to the trayicon menu.</returns>
        public virtual ToolStripItem InitTrayIconMenu()
        {
            return null;
        }

        /// <summary>
        /// Create button(s) in the top FrmManageNotes window.
        /// </summary>
        /// <returns>Array with the button or buttons to create.</returns>
        public virtual Button[] InitFrmManageNotesBtns()
        {
            return null;
        }

        /// <summary>
        /// A format button clicked.
        /// </summary>
        /// <param name="rtbnote">The richtextbox</param>
        /// <param name="btn">The button that is clicked</param>
        /// <returns>The rtf text of the note, stays the same.</returns>
        public virtual string NoteFormatBtnClicked(System.Windows.Forms.RichTextBox rtbnote, Button btn)
        {
            return rtbnote.Rtf;
        }

        /// <summary>
        /// Menu item in right click menu FrmNewNote is clicked.d
        /// </summary>
        /// <param name="rtbnote">The RichTextbox.</param>
        /// <param name="menuitem">The button is clicked.</param>
        /// <returns>new content</returns>
        public virtual string MenuFrmNewNoteClicked(System.Windows.Forms.RichTextBox rtbnote, ToolStripItem menuitem)
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
        /// Executed if a note is saved.
        /// </summary>
        /// <param name="content">A note object with details.</param>
        /// <param name="title">The note title.</param>
        public virtual void SavingNote(string content, string title)
        {
            // by default do nothing, override this to do someting.
        }

        /// <summary>
        /// Executed if a note is made visible.
        /// </summary>
        /// <param name="content">The note content.</param>
        /// <param name="title">The note title.</param>
        public virtual void ShowingNote(string content, string title)
        {
            // by default do nothing, override this to do someting.
        }

        /// <summary>
        /// Executed if a note is being hiden.
        /// </summary>
        /// <param name="content">The note content.</param>
        /// <param name="title">The note title.</param>
        public virtual void HidingNote(string content, string title)
        {
            // by default do nothing, override this to do someting.
        }
    }
}
