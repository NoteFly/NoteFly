//-----------------------------------------------------------------------
// <copyright file="IPlugin.cs" company="NoteFly">
// Copyright 2011 Tom
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
using System;

[assembly: CLSCompliant(true)]

/// <summary>
/// Provides interfaces for plugins
/// </summary>
namespace IPlugin
{    
    using System.Windows.Forms;

    /// <summary>
    /// Plugin interface
    /// status: DRAFT (Subject to change)
    /// revision: 7
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Gets the filename of this plugin.
        /// </summary>
        string Filename { get; }

        /// <summary>
        /// Gets the interface to let a plugin talk to NoteFly.
        /// </summary>
        IPluginHost Host { get; }

        /// <summary>
        /// Register plugin
        /// </summary>
        /// <param name="file">The plugin file.</param>
        /// <param name="host">Interface to notefly core.</param>
        void Register(string file, IPluginHost host);

        /// <summary>
        /// Adds ToolStripItem to the right click submenu share on FrmNote.
        /// </summary>
        /// <returns>A ToolStripMenuItem</returns>
        ToolStripMenuItem InitFrmNoteShareMenu();

        /// <summary>
        /// Executed if share menu clicked.
        /// </summary>
        /// <param name="rtbnote">The richedit component with the note content in memory.</param>
        /// <param name="title">The note title</param>
        void ShareMenuClicked(System.Windows.Forms.RichTextBox rtbnote, string title);

        /// <summary>
        /// Executed if settings tab loaded.
        /// </summary>
        /// <returns>A Tabpage with all components to draw.</returns>
        TabPage InitShareSettingsTab();

        /// <summary>
        /// Create a button in the bottom in FrmNewNote.
        /// </summary>
        /// <returns>The buttons created in FrmNewNote.</returns>
        Button[] InitNoteFormatBtns();

        /// <summary>
        /// Adds ToolStripItem to the right click menu on FrmNewNote.
        /// </summary>
        /// <returns>A ToolStripItem for the contextmenu of FrmNewNote.</returns>
        ToolStripItem InitFrmNewNoteMenu();

        /// <summary>
        /// Adds ToolStripItem to the right click menu on FrmNote.
        /// </summary>
        /// <returns>A ToolStripItem to add to the contextmenu of FrmNote.</returns>
        ToolStripItem InitFrmNoteMenu();

        /// <summary>
        /// Adds ToolStripItem to the right click menu of the trayicon.
        /// </summary>
        /// <returns>A ToolStripItem to the rightclick menu of the trayicon.</returns>
        ToolStripItem InitTrayIconMenu();

        /// <summary>
        /// Create button(s) in the top FrmManageNotes window.
        /// </summary>
        /// <returns>Array with the button or buttons to create.</returns>
        Button[] InitFrmManageNotesBtns();

        /// <summary>
        /// A plugin format button is cliked.
        /// </summary>
        /// <param name="rtbnote">The RichTextbox.</param>
        /// <param name="btn">The button is clicked.</param>
        /// <returns>The new rtf note content</returns>
        string NoteFormatBtnClicked(System.Windows.Forms.RichTextBox rtbnote, Button btn);

        /// <summary>
        /// Menu item in right click menu FrmNewNote is clicked.
        /// </summary>
        /// <param name="rtbnote">The RichTextbox.</param>
        /// <param name="menuitem">The button is clicked.</param>
        /// <returns>The new rtf note content</returns>
        string MenuFrmNewNoteClicked(System.Windows.Forms.RichTextBox rtbnote, ToolStripItem menuitem);

        /// <summary>
        /// Executed if Ok on FrmSettings is pressed.
        /// </summary>
        /// <returns>True if allowed to close FrmSettings.</returns>
        bool SaveSettingsTab();

        /// <summary>
        /// Executed if a note is saved
        /// </summary>
        /// <param name="content">The note content.</param>
        /// <param name="title">The note title.</param>
        void SavingNote(string content, string title);

        /// <summary>
        /// Executed if a note is made visible.
        /// </summary>
        /// <param name="content">The note content.</param>
        /// <param name="title">The note title.</param>
        void ShowingNote(string content, string title);

        /// <summary>
        /// Executed if a note is being hiden.
        /// </summary>
        /// <param name="content">The note content.</param>
        /// <param name="title">The note title.</param>
        void HidingNote(string content, string title);
    }
}