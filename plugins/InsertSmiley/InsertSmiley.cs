namespace InsertSmiley
{
    using System.Windows.Forms;

    /// <summary>
    /// InserSmilet plugin
    /// </summary>
    public class InsertSmiley : IPlugin.PluginBase
    {
        private FrmSmileyChooser chooser;

        /// <summary>
        /// Create the buttons for formatting in FrmNewNote.
        /// </summary>
        /// <returns>A array with buttons</returns>
        public override Button[] InitNoteFormatBtns()
        {
            Button[] btns = new Button[1];

            Button btnOpenChooser = new Button();
            btnOpenChooser.Name = "smileychooser";
            btnOpenChooser.Image = global::InsertSmiley.Properties.Resources.smiley_smile;
            btnOpenChooser.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            btnOpenChooser.SetBounds(0, 0, 26, 22);
            btnOpenChooser.FlatStyle = FlatStyle.Flat;
            btnOpenChooser.UseCompatibleTextRendering = true;
            btns[0] = btnOpenChooser;
            return btns;
        }
        
        /// <summary>
        /// Insert a smiley copy into a note.
        /// (Carefull these smiley images are copies stored in notes, bad for filesize of notes.)
        /// </summary>
        /// <param name="rtbnote">The richedittextbox with the note content.</param>
        /// <param name="btn">The button clicked.</param>
        /// <returns>The new note content as RTF.</returns>
        public override string NoteFormatBtnClicked(System.Windows.Forms.RichTextBox rtbnote, Button btn)
        {
            
            if (btn.Name.Equals("smileychooser", System.StringComparison.Ordinal))
            {
                int docend = rtbnote.Rtf.LastIndexOf(@"\par");
                this.chooser = new FrmSmileyChooser(Cursor.Position.X, Cursor.Position.Y, rtbnote, docend, rtbnote.Rtf);
                this.chooser.Show();
            }

            return rtbnote.Rtf;
        }


    }
}
