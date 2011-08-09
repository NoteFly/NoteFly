namespace HelloWorld
{
    using System.Drawing;
    using System.Windows.Forms;

    public sealed class HelloWorld : PluginBase
    {
        // Properties (2) 

        public override string SettingsTabTitle
        {
            get
            {
                return "Hello World plugin";
            }
        }

        public override string ShareMenuText
        {
            get
            {
                return "Try hello world";
            }
        }

        // Methods (2) 

        /// <summary>
        /// Menu clicked
        /// </summary>
        /// <param name="rtbnote">use the content of rtbnote for the note content, 
        /// this way note content does not have to be read again from disk.</param>
        /// <param name="note">The note settings</param>
        public override void ShareMenuClicked(RichTextBox rtbnote, NoteFly.Note note)
        {
            MessageBox.Show("Hello World!");
        }

        /// <summary>
        /// Load tab
        /// </summary>
        /// <returns></returns>
        public override TabPage InitShareSettingsTab()
        {
            TabPage newtab = new TabPage(this.SettingsTabTitle);
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
