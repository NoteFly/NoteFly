//-----------------------------------------------------------------------
// <copyright file="IPluginHost.cs" company="NoteFly">
// Copyright 2011 Tom
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
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
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Plugin interface to NoteFly
    /// status: DRAFT (Subject to change)
    /// revision: 6
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
        /// <param name="wordwarp">Is the note content word warped.</param>
        void AddNoteDefaultSettings(string title, int skinnr, int x, int y, int width, int height, string content, bool wordwarp);

        /// <summary>
        /// Update all notes forms, in case some notefly 
        /// application wide settings are changed.
        /// </summary>
        void UpdateAllNoteForms();

        /// <summary>
        /// Get a list of skin names
        /// </summary>
        /// <returns>A string array of all skin names loaded.</returns>
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
        /// Remove all skins and load them all again from skins.xml.
        /// </summary>
        void ReloadAllSkins();

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

        /// <summary>
        /// Get the name of the program.
        /// </summary>
        /// <returns>The assembly title.</returns>
        string GetAssemblyTitle();

        /// <summary>
        /// Get the version of the program as string, in the form: major.minor.release version numbers
        /// </summary>
        /// <returns>The assembly version as string</returns>
        string GetAssemblyVersionAsString();

        /// <summary>
        /// Get a NoteFly boolean setting.
        /// </summary>
        /// <param name="settingname">The setting name to look the value for.</param>
        /// <returns>Boolean setting value.</returns>
        bool GetSettingBool(string settingname);

        /// <summary>
        /// Get a NoteFly integer setting.
        /// </summary>
        /// <param name="settingsname">The setting name to look the value for.</param>
        /// <returns>Integer setting value.</returns>
        int GetSettingInt(string settingsname);

        /// <summary>
        /// Get a NoteFly float setting.
        /// </summary>
        /// <param name="settingsname">The setting name to look the value for.</param>
        /// <returns>Float setting value.</returns>
        float GetSettingFloat(string settingsname);

        /// <summary>
        /// Get a NoteFly string setting.
        /// </summary>
        /// <param name="settingsname">The setting name to look the value for.</param>
        /// <returns>String setting value.</returns>
        string GetSettingString(string settingsname);
    }
}