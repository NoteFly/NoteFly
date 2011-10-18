//-----------------------------------------------------------------------
// <copyright file="IPluginHost.cs" company="NoteFly">
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
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Plugin interface to NoteFly
    /// status: DRAFT (Subject to change)
    /// revision: 5
    /// </summary>
    public interface IPluginHost
    {
        /// <summary>
        /// Gets the number of notes
        /// </summary>
        int CountNotes
        {
            get;
        }

        /// <summary>
        /// Gets the number of skins
        /// </summary>
        int CountSkins
        {
            get;
        }

        /// <summary>
        /// Add a note with some default settings:
        /// Locked = false, RolledUp = false, Ontop = false, Visible = true
        /// </summary>
        /// <param name="title">The title of the note</param>
        /// <param name="skinnr">The skin position of note skin</param>
        /// <param name="x">X coordinate of the note on the screen</param>
        /// <param name="y">Y coordinate of the note on the screen</param>
        /// <param name="width">The width of the note.</param>
        /// <param name="height">The height of the note.</param>
        /// <param name="content">The content of the note.</param>
        void AddNoteDefaultSettings(string title, int skinnr, int x, int y, int width, int height, string content);

        /// <summary>
        /// Update all notes forms, in case some notefly 
        /// application wide settings are changed.
        /// </summary>
        void UpdateAllNoteForms();

        /// <summary>
        /// Get a list of skin names
        /// </summary>
        /// <returns></returns>
        string[] GetSkinsNames();

        /// <summary>        
        /// Get the skin position in the list belong to a skinname.
        /// </summary>
        /// <param name="skinname">The name of the skin to get the position from</param>
        /// <returns>The skin position</returns>
        int GetSkinNr(string skinname);

        /// <summary>
        /// Get the skinname belonging to a particulair skin position.
        /// </summary>
        /// <param name="skinnr">Skin position</param>
        /// <returns>The name of the skin</returns>
        string GetSkinName(int skinnr);

        /// <summary>
        /// Get the primary color of a skin.
        /// </summary>
        /// <param name="skinnr">Skin position</param>
        /// <returns>The skin primary color.</returns>
        Color GetPrimaryClr(int skinnr);

        /// <summary>
        /// Get the select color of a skin.
        /// </summary>
        /// <param name="skinnr">The skin position</param>
        /// <returns>The select color of the skin.</returns>
        Color GetSelectClr(int skinnr);

        /// <summary>
        /// Get the highligh color of a skin.
        /// </summary>
        /// <param name="skinnr">The skin position</param>
        /// <returns>Get skin hightlight color.</returns>
        Color GetHighlightClr(int skinnr);

        /// <summary>
        /// Get the text color of a skin.
        /// </summary>
        /// <param name="skinnr">The skin position</param>
        /// <returns>The skin text color.</returns>
        Color GetTextClr(int skinnr);

        /// <summary>
        /// Get the texture bitmap of a skin
        /// </summary>
        /// <param name="skinnr">The skin position</param>
        /// <returns>The primary texture bitmap.</returns>
        Bitmap GetPrimaryTexture(int skinnr);

        /// <summary>
        /// Get the texture filepath of a skin.
        /// </summary>
        /// <param name="skinnr">The skin position</param>
        /// <returns>The primary texture file path.</returns>
        string GetPrimaryTextureFile(int skinnr);

        /// <summary>
        /// Get the texture layout of a skin
        /// </summary>
        /// <param name="skinnr">The skin position</param>
        /// <returns>The primart texture imagelayout</returns>
        ImageLayout GetPrimaryTextureLayout(int skinnr);        

        /// <summary>
        /// Gets filepath to the settings file of NoteFly.
        /// </summary>
        /// <returns>The settings file path</returns>
        string GetSettingsFile();

        /// <summary>
        /// Gets filepath to the skins file of NoteFly.
        /// </summary>
        /// <returns>The skins file path</returns>
        string GetSkinsFile();

        /// <summary>
        /// Get the notes save path of NoteFly
        /// </summary>
        /// <returns>The notes save directory</returns>
        string GetNotesSavepath();

        /// <summary>
        /// Log info level information for a plugin to NoteFly logfile.
        /// </summary>
        /// <param name="infomsg">The info message to log</param>
        void LogPluginInfo(string infomsg);

        /// <summary>
        /// Log error level information for a plugin to NoteFly login
        /// </summary>
        /// <param name="errormsg">The error message to log</param>
        void LogPluginError(string errormsg);

    }
}
