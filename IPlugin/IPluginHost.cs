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
        int CountNotes
        {
            get;
        }

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
        /// <param name="skinname"></param>
        /// <returns></returns>
        int GetSkinNr(string skinname);

        /// <summary>
        /// Get the skinname belonging to a particulair skin position.
        /// </summary>
        /// <param name="skinnr"></param>
        /// <returns></returns>
        string GetSkinName(int skinnr);

        /// <summary>
        /// Get the primary color of a skin.
        /// </summary>
        /// <param name="skinnr"></param>
        /// <returns></returns>
        Color GetPrimaryClr(int skinnr);

        /// <summary>
        /// Get the select color of a skin.
        /// </summary>
        /// <param name="skinnr"></param>
        /// <returns></returns>
        Color GetSelectClr(int skinnr);

        /// <summary>
        /// Get the highligh color of a skin.
        /// </summary>
        /// <param name="skinnr"></param>
        /// <returns></returns>
        Color GetHighlightClr(int skinnr);

        /// <summary>
        /// Get the text color of a skin.
        /// </summary>
        /// <param name="skinnr"></param>
        /// <returns></returns>
        Color GetTextClr(int skinnr);

        /// <summary>
        /// Get the texture bitmap of a skin
        /// </summary>
        /// <param name="skinnr"></param>
        /// <returns></returns>
        Bitmap GetPrimaryTexture(int skinnr);

        /// <summary>
        /// Get the texture filepath of a skin.
        /// </summary>
        /// <param name="skinnr"></param>
        /// <returns></returns>
        string GetPrimaryTextureFile(int skinnr);

        /// <summary>
        /// Get the texture layout of a skin
        /// </summary>
        /// <param name="skinnr"></param>
        /// <returns></returns>
        ImageLayout GetPrimaryTextureLayout(int skinnr);        

        /// <summary>
        /// Gets filepath to the settings file of NoteFly.
        /// </summary>
        /// <returns></returns>
        string GetSettingsFile();

        /// <summary>
        /// Gets filepath to the skins file of NoteFly.
        /// </summary>
        /// <returns></returns>
        string GetSkinsFile();

        /// <summary>
        /// Get the notes save path of NoteFly
        /// </summary>
        /// <returns></returns>
        string GetNotesSavepath();

        /// <summary>
        /// Log info level information for a plugin to NoteFly logfile.
        /// </summary>
        /// <param name="infomsg"></param>
        void LogPluginInfo(string infomsg);

        /// <summary>
        /// Log error level information for a plugin to NoteFly login
        /// </summary>
        /// <param name="errormsg"></param>
        void LogPluginError(string errormsg);

    }
}
