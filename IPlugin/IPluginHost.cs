namespace IPlugin
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Plugin interface to NoteFly
    /// status: DRAFT (Subject to change)
    /// revision: 4
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
