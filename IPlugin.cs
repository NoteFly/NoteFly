using System.Windows.Forms;
namespace NoteFly
{
    public interface IPlugin
    {
        /// <summary>
        /// Name of the plugin
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Description of the plugin
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Author of the plugin
        /// </summary>
        string Author { get; }

        /// <summary>
        /// Version of the plugin
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
        /// Event if share menu clicked, if any
        /// </summary>
        /// <param name="rtbnote"></param>
        /// <param name="note"></param>
        void ShareMenuClicked(System.Windows.Forms.RichTextBox rtbnote, NoteFly.Note note);

        /// <summary>
        /// Event if settings tab loaded, if any
        /// </summary>
        /// <returns></returns>
        TabPage InitShareSettingsTab();
    }
}
