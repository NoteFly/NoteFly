using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace SimplePlainNote
{
    public class Notes
    {
		#region Fields (6) 

        private List<frmNote> noteslst;
        private int defaultcolor = 1;
        private string notesavepath;

        private bool transparecy = false;
        private bool syntaxhighlight = false;
        private bool twitterenabled = false;        
       
		#endregion Fields 

		#region Constructors (1) 

        public Notes()
        {
            noteslst = new List<frmNote>();            
            SetSettings();
            LoadNotes();            
        }

		#endregion Constructors 

		#region Properties (5) 

        public List<frmNote> GetNotes
        {
            get
            {
                return this.noteslst;
            }
        }

        public int numnotes
        {
            get
            {
                return this.noteslst.Count;
            }
        }

        public bool Transparency
        {
            get
            {
                return this.transparecy;
            }
        }

        public bool SyntaxHighlightEnabled
        {
            get 
            {
                return this.syntaxhighlight;
            }
        }

        public bool TwitterEnabled
        {
            get
            {
                return this.twitterenabled;
            }
        }

		#endregion Properties 

		#region Methods (8) 

		// Public Methods (4) 

        /// <summary>
        /// Draws a new note and saves the xml note file.(call to SaveNewNote)
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="notecolor"></param>
        public void DrawNewNote(string title, string content, int notecolor)
        {
            try
            {           
                int newid = noteslst.Count + 1;
                string notefilenm = SaveNewNote(newid, title, content, defaultcolor.ToString());
                if (String.IsNullOrEmpty(notefilenm)) { return; }
                frmNote newnote = new frmNote(this, newid, title, content, notecolor);
                noteslst.Add(newnote);
                newnote.StartPosition = FormStartPosition.Manual;                
                newnote.Show();
            }
            catch (IndexOutOfRangeException indexexc)
            {
                MessageBox.Show("Fout: " + indexexc.Message);
            }
        }

        public void CheckSyntax(bool syntaxhighlight, RichTextBox rtb)
        {
            if (syntaxhighlight == true)
            {
                TextHighlight texthighlight = new TextHighlight();

                int selPos = rtb.SelectionStart;
                foreach (Match keyWordMatch in texthighlight.getRegexHTML.Matches(rtb.Text))
                {
                    rtb.Select(keyWordMatch.Index, keyWordMatch.Length);
                    rtb.SelectionColor = System.Drawing.Color.Blue;
                    rtb.SelectionStart = selPos;
                    rtb.SelectionColor = System.Drawing.Color.Black;
                }
            }
        }
        /// <summary>
        /// Edit a note.
        /// </summary>
        /// <param name="noteid"></param>
        public void EditNewNote(int noteid)
        {
            int noteslistpos = noteid - 1;
            if ((noteslistpos >= 0) && (noteslistpos <= numnotes))
            {
                string title = noteslst[noteid - 1].NoteTitle;
                string content = noteslst[noteid - 1].NoteContent;
                int color = noteslst[noteid - 1].NoteColor;                
                frmNewNote createnewnote = new frmNewNote(this, color, noteid, title, content);
                createnewnote.Show();
            }
            else
            {
                MessageBox.Show("Fout: note not found in memory.");
            }

        }

        public void UpdateAllFonts()
        {            
            
            foreach (frmNote curfrmnote in noteslst)
            {
                curfrmnote.PaintColorNote();
            }
        }

        public void UpdateNote(int noteid, string title, string content, bool visible)
        {
            int notelstpos = noteid - 1;
            noteslst[notelstpos].NoteTitle = title;
            noteslst[notelstpos].NoteContent = content;
            noteslst[notelstpos].Visible = visible;
            if (visible) { 
                noteslst[notelstpos].Show(); 
            }
            noteslst[notelstpos].checkthings();
            //noteslst[notelstpos].CheckSyntax(syntaxhighlight);
            //noteslst[notelstpos].CheckTwitter(twitterenabled);
        }

        /// <summary>
        /// check settings and set variables
        /// </summary>
        public void SetSettings()
        {
            xmlHandler getSettings = new xmlHandler(true);
            this.defaultcolor = getSettings.getXMLnodeAsInt("defaultcolor");
            if (getSettings.getXMLnodeAsBool("transparecy") == true)
            {
                this.transparecy = true;
            }
            if (getSettings.getXMLnodeAsBool("syntaxhighlight") == true)
            {
                this.syntaxhighlight = true;
            }
            this.notesavepath = getSettings.getXMLnode("notesavepath");
            this.twitterenabled = !String.IsNullOrEmpty(getSettings.getXMLnode("twitteruser"));
        }

		// Private Methods (4)

        /// <summary>
        /// Create a note GUI.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="notecolor"></param>        
        private frmNote CreateNote(bool visible, bool ontop, string title, string content, int notecolor, int locX, int locY, int notewith, int noteheight)
        {
            try
            {                
                int newid = noteslst.Count + 1;
                frmNote newnote = new frmNote(this, newid, visible, ontop, title, content, notecolor, locX, locY, notewith, noteheight);
                if (visible)
                {
                    newnote.Show();
                }
                return newnote;
            }
            catch (IndexOutOfRangeException indexexc)
            {
                MessageBox.Show("Fout: " + indexexc.Message);
                return null;
            }
        }

        private void LoadNotes()
        {
            #if DEBUG
            DateTime starttime = DateTime.Now;
            #endif                        
            
            int id = 1;            
            while (File.Exists( Path.Combine(notesavepath, id+".xml") ) == true)
            {                
                xmlHandler parserNote = new xmlHandler(false, id + ".xml");

                bool visible = parserNote.getXMLnodeAsBool("visible");
                bool ontop = parserNote.getXMLnodeAsBool("ontop");
                string title = parserNote.getXMLnode("title");
                string content = parserNote.getXMLnode("content");
                int notecolor = parserNote.getXMLnodeAsInt("color");
                int noteLocX = parserNote.getXMLnodeAsInt("x");
                int noteLocY = parserNote.getXMLnodeAsInt("y");
                int notewidth = parserNote.getXMLnodeAsInt("width");
                int noteheight = parserNote.getXMLnodeAsInt("heigth");

                noteslst.Add(CreateNote(visible, ontop, title, content, notecolor, noteLocX, noteLocY, notewidth, noteheight));

                id++;                
                if (id > 500) { MessageBox.Show("Error: Too many notes"); return; }
            }            

            #if DEBUG
            DateTime endtime = DateTime.Now;
            TimeSpan debugtime = endtime - starttime;
            MessageBox.Show("loading notes time: " + debugtime.Milliseconds + " ms\r\n " + debugtime.Ticks + " ticks");
            #endif         
        }

        /// <summary>
        /// Save the note to xml file
        /// </summary>
        /// <param name="id">number</param>
        /// <param name="title"></param>
        /// <param name="text"></param>
        /// <returns>filepath of the created note.</returns>
        private string SaveNewNote(int id, string title, string text, string numcolor)
        {            
            string notefile = id + ".xml";
            xmlHandler xmlnote = new xmlHandler(false, notefile);            
            if (xmlnote.WriteNote(true, false, numcolor, title, text, 10, 10, 240, 240) == false)
            {
                MessageBox.Show("Error writing note.");
                return null;
            }
            return notefile;
        }

		#endregion Methods 
    }
}
