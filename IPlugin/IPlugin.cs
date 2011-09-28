//-----------------------------------------------------------------------
// <copyright file="IPlugin.cs" company="NoteFly">
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
    using System.Windows.Forms;

    /// <summary>
    /// Plugin interface
    /// status: DRAFT (Subject to change)
    /// revision: 2
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Gets or sets a value indicating whether the plugin is enabled.
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// Gets the filename of this plugin.
        /// </summary>
        string Filename { get; }

        /// <summary>
        /// Gets the share menu text, if any.
        /// </summary>
        string ShareMenuText { get; }

        /// <summary>
        /// Gets the share settings tab title, if any
        /// </summary>
        string SettingsTabTitle { get; }

        /// <summary>
        /// Register plugin
        /// </summary>
        /// <param name="enabled">Is this plugin enabled.</param>
        /// <param name="file">The filename of the plugin</param>
        void Register(bool enabled, string file);

        /// <summary>
        /// Executed if share menu clicked.
        /// </summary>
        /// <param name="rtbnote">The richedit component with the note content in memory.</param>
        /// <param name="title">The note title</param>
        void ShareMenuClicked(System.Windows.Forms.RichTextBox rtbnote, string title);

        /// <summary>
        /// Executed if settings tab loaded.
        /// </summary>
        /// <returns>a Tabpage with all components to draw</returns>
        TabPage InitShareSettingsTab();

        /// <summary>
        /// Create a button in the bottom in FrmNewNote.
        /// </summary>
        /// <returns>The buttons created in FrmNewNote</returns>
        Button[] InitFrmNewNoteFormatTools();

        /// <summary>
        /// A plugin format button is cliked.
        /// </summary>
        /// <param name="rtbnote">The RichTextbox.</param>
        /// <param name="btn">The button is clicked.</param>
        /// <returns>new content</returns>
        string FormatBtnClicked(System.Windows.Forms.RichTextBox rtf, Button btn);

        /// <summary>
        /// Executed if Ok on FrmSettings is pressed.
        /// </summary>
        /// <returns>true if allowed to close FrmSettings</returns>
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
