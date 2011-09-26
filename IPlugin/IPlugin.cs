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
    /// 
    /// warning: DRAFT
    /// Subject to change.
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Is this plugin enabled
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        /// Get the filename of this plugin
        /// </summary>
        string Filename { get; }

        /// <summary>
        /// Share menu text, if any
        /// </summary>
        string ShareMenuText { get; }

        /// <summary>
        /// Share settings tab title, if any
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
        /// <param name="note">note object</param>
        void ShareMenuClicked(System.Windows.Forms.RichTextBox rtbnote, string title);

        /// <summary>
        /// Executed if settings tab loaded.
        /// </summary>
        /// <returns>a Tabpage with all components to draw</returns>
        TabPage InitShareSettingsTab();

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
