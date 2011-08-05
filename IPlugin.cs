namespace NoteFly
{
    using System.Windows.Forms;

    public interface IPlugin
    {
        /// <summary>
        /// Gets the name of the plugin
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the description of the plugin
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets author of the plugin
        /// </summary>
        string Author { get; }

        /// <summary>
        /// Gets the version of the plugin
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Share menu text, if any
        /// </summary>
        string ShareMenuText { get; }

        /// <summary>
        /// Share settings tab title, if any
        /// </summary>
        string SettingsTabTitle { get; }

        /// <summary>
        /// Executed if share menu clicked.
        /// </summary>
        /// <param name="rtbnote">The richedit component with the note content in memory.</param>
        /// <param name="note">note object</param>
        void ShareMenuClicked(System.Windows.Forms.RichTextBox rtbnote, NoteFly.Note note);

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
        /// <param name="note">A note object with details</param>
        void SavingNote(Note note);

        /// <summary>
        /// Executed if a note is made visible
        /// </summary>
        void ShowingNote(Note note);

        /// <summary>
        /// Executed if a note is being hiden.
        /// </summary>
        void HidingNote(Note note);
    }
}
