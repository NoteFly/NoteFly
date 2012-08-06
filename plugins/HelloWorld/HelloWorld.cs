namespace HelloWorld
{
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// HelloWorld plugin
    /// </summary>
    public class HelloWorld : IPlugin.PluginBase
    {
        /// <summary>
        /// Create the ToolStripMenuItem in the share submenu.
        /// </summary>
        /// <returns>A ToolStripMenuItem with Try hello world text.</returns>
        public override ToolStripMenuItem InitFrmNoteShareMenu()
        {
            ToolStripMenuItem toolstripmenu = new ToolStripMenuItem("Try hello world");
            toolstripmenu.Name = "menuShareHelloWorld";
            return toolstripmenu;
        }

        /// <summary>
        /// Menu clicked
        /// </summary>
        /// <param name="rtbnote">Use the content of rtbnote for the note content, 
        /// this way note content does not have to be read again from disk.</param>
        /// <param name="title">The title of the note</param>
        public override void ShareMenuClicked(System.Windows.Forms.RichTextBox rtbnote, string title)
        {
            MessageBox.Show("Hello World!");
        }

        /// <summary>
        /// Load tab
        /// </summary>
        /// <returns>Tabpage with components</returns>
        public override TabPage InitShareSettingsTab()
        {
            TabPage newtab = new TabPage("Hello World plugin");
            newtab.BackColor = Color.White;

            Label lblhello = new Label();
            lblhello.Text = "Hello world demo plugin settings page.";
            lblhello.AutoSize = true;
            lblhello.Location = new Point(30, 40);
            lblhello.Visible = true;
            lblhello.Parent = newtab;

            return newtab;
        }
    }
}
