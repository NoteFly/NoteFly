namespace InsertSmiley
{
    using System.Windows.Forms;

    /// <summary>
    /// InserSmilet plugin
    /// </summary>
    public class InsertSmileyplugin : IPlugin.PluginBase
    {
        public override string SettingsTabTitle
        {
            get
            {
                return "Insert smiley plugin";
            }
        }

        /// <summary>
        /// Create the buttons for formatting in FrmNewNote.
        /// </summary>
        /// <returns>A array with buttons</returns>
        public override Button[] InitFrmNewNoteFormatTools()
        {
            Button[] btns = new Button[3];

            Button btnSmile = new Button();
            btnSmile.Name = "smile";
            btnSmile.Text = ":-)";
            btnSmile.SetBounds(0, 0, 26, 22);
            btnSmile.FlatStyle = FlatStyle.Flat;
            btnSmile.UseCompatibleTextRendering = true;            
            btns[0] = btnSmile;

            Button btnSad = new Button();
            btnSad.Name = "sad";
            btnSad.Text = ":-(";
            btnSad.SetBounds(0, 0, 26, 22);
            btnSad.FlatStyle = FlatStyle.Flat;
            btnSad.UseCompatibleTextRendering = true;  
            btns[1] = btnSad;

            Button btnWink = new Button();
            btnWink.Name = "wink";
            btnWink.Text = ";-)";
            btnWink.SetBounds(0, 0, 26, 22);
            btnWink.FlatStyle = FlatStyle.Flat;
            btnWink.UseCompatibleTextRendering = true;
            btnWink.MinimumSize = new System.Drawing.Size(32, 10);
            btns[2] = btnWink;
            return btns;
        }
        
        /// <summary>
        /// Insert a smiley copy into a note.
        /// (Carefull these smiley images are copies stored in notes, bad for filesize of notes.)
        /// </summary>
        /// <param name="rtbnote">The richedittextbox with the note content.</param>
        /// <param name="btn">The button clicked.</param>
        /// <returns>The new note content as RTF.</returns>
        public override string FormatBtnClicked(System.Windows.Forms.RichTextBox rtbnote, Button btn)
        {
            string rtfimg = string.Empty;
            // wmetafile of smilies created with wordpad and then rtf files opened with notepad. ;)
            switch (btn.Name)
            {
                case "smile":
                    rtfimg = @"{\pict\wmetafile8\picw397\pich397\picwgoal225\pichgoal225 
010009000003a600000000007d00000000000400000003010800050000000b0200000000050000
000c020f000f00030000001e00040000000701040004000000070104007d000000410b2000cc00
0f000f00000000000f000f0000000000280000000f0000000f0000000100040000000000000000
000000000000000000000000000000000000000000ffffff0045454500ebffff00c7ffff0093fe
ff0000ceff0013fdff0000eaff00009dfe0000c9ff0000b4ff0000e5ff00000000000000000000
000000111112222211111011122bbbb922111011266aaaaa6921101266c00000a6921012680888
880a6210268088888880a920278888888888ab20258888888888ab20257880888088ab20247880
888088a92012578888888862101245888888869210112347788869211011122345562211101111
122222111110040000002701ffff030000000000
}";
                    break;
                case "sad":
                    rtfimg = @"{\pict\wmetafile8\picw397\pich397\picwgoal225\pichgoal225 
010009000003a600000000007d00000000000400000003010800050000000b0200000000050000
000c020f000f00030000001e00040000000701040004000000070104007d000000410b2000cc00
0f000f00000000000f000f0000000000280000000f0000000f0000000100040000000000000000
000000000000000000000000000000000000000000ffffff0045454500ebffff00c7ffff0093fe
ff0000ceff0013fdff0000eaff00009dfe0000c9ff0000b4ff0000e5ff00000000000000000000
000000111112222211111011122bbbb922111011266aaaaa6921101266088880a6921012688000
088a621026888888888ca920278010888010ab202501100800110b202501111811110b20247011
888110a92012570088800862101245788888869210112347788869211011122345562211101111
122222111110040000002701ffff030000000000
}";
                    break;
                case "wink":
                    rtfimg = @"{\pict\wmetafile8\picw397\pich397\picwgoal225\pichgoal225 
010009000003a600000000007d00000000000400000003010800050000000b0200000000050000
000c020f000f00030000001e00040000000701040004000000070104007d000000410b2000cc00
0f000f00000000000f000f0000000000280000000f0000000f0000000100040000000000000000
000000000000000000000000000000000000000000ffffff0045454500ebffff00c7ffff0093fe
ff0000ceff0013fdff0000eaff00009dfe0000c9ff0000b4ff0000e5ff00000000000000000000
000000111112222211111011122bbbb922111011266aa00a6921101266c80000a6921012688000
000a621026880000000ca920278888888808ab20258888888888ab20257800188088ab20247888
081088a92012578888108862101245788808869210112347788869211011122345562211101111
122222111110040000002701ffff030000000000
}";
                    break;
            }

            int docend = rtbnote.Rtf.LastIndexOf(@"\par");
            string currentrtf = rtbnote.Rtf.Insert(docend, rtfimg);
            return currentrtf;
        }

        /*
        /// <summary>
        /// Find the rtf position of a textposition
        /// very unreliable, thus disabled.
        /// </summary>
        /// <param name="postext"></param>
        /// <returns>-1 on not found</returns>
        private int TextPosToRTFPos(System.Windows.Forms.RichTextBox rtbnote, int textpos)
        {
            int rtfpos = -1;
            int textcnt = 0;
            int docstart = rtbnote.Rtf.IndexOf(@"\viewkind", System.StringComparison.Ordinal);            
            bool textstarted = false;
            for (int i = docstart; i < rtbnote.Rtf.Length; i++)
            {
                if (textstarted)
                {
                    textstarted = true;
                    textcnt++;
                    if (textpos == textcnt)
                    {                        
                        rtfpos = i + 1;
                        break;
                    }

                    if (rtbnote.Rtf[i] == '\\')
                    {
                        textstarted = false;
                    }
                }

                if (rtbnote.Rtf[i] == ' ')
                {
                    textstarted = true;
                }
            }


            return rtfpos;
        }
         */
    }
}
