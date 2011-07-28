namespace HelloWorld
{
    using System;
    using System.Text;
    using System.IO;
    using System.Windows.Forms;

    public class HelloWorld : PluginBase
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

		// Methods (1) 

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
    }
}
