namespace NoteFly
{
    public interface IPlugin
    {
        string Name { get; }
        string Description { get; }
        string Author { get; }
        string Version { get; }

        string ShareMenuText { get; }
        string SettingsTabTitle { get; }

        void ShareMenuClicked(System.Windows.Forms.RichTextBox rtbnote, NoteFly.Note note);
    }
}
