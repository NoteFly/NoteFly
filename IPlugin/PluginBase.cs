//-----------------------------------------------------------------------
// <copyright file="PluginBase.cs" company="NoteFly">
// Copyright 2011-2012 Tom
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------
namespace IPlugin
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// PluginBase class.
    /// </summary>
    [CLSCompliant(true)]
    public abstract class PluginBase : IPlugin
    {
        /// <summary>
        /// The filename of this plugin.
        /// </summary>
        private string file;

        /// <summary>
        /// Access to NoteFly
        /// </summary>
        private IPluginHost host;

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
        /// Gets the interface to let the plugin talk to NoteFly.
        /// </summary>
        public IPluginHost Host
        {
            get
            {
                return this.host;
            }
        }

        /// <summary>
        /// Register the plugin
        /// string name, string author, string description, string version, 
        /// </summary>
        /// <param name="file">The plugin file.</param>
        /// <param name="host">Reference to the IPluginHost interface.</param>
        public void Register(string file, IPluginHost host)
        {
            this.file = file;
            this.host = host;
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
        /// <returns>A tabpage with all components to draw.</returns>
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
        /// <returns>The new rtf note content</returns>
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
