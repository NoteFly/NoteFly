using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SimplePlainNote
{
    public class Notes
    {
		#region Fields (1) 

        private List<frmNote> noteslst;
        private bool transparecy = false;
		#endregion Fields 

		#region Constructors (1) 

        public Notes()
        {
            noteslst = new List<frmNote>();
            transparecy = getTransparency();
            LoadNotes();            
        }

		#endregion Constructors 

		#region Properties (2) 

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

		#endregion Properties 

		#region Methods (5) 

		// Public Methods (2) 

        public void CreateNewNote(string title, string content, int notecolor)
        {
            try
            {
                xmlHandler getXmlSettings = new xmlHandler(true);
                string defaultcolor = getXmlSettings.getXMLnode("defaultcolor");

                int newid = noteslst.Count + 1;
                string notefilenm = SaveNewNote(newid, title, content, defaultcolor);
                if (String.IsNullOrEmpty(notefilenm)) { return; }
                frmNote newnote = new frmNote(newid, title, content, transparecy, notecolor);
                noteslst.Add(newnote);
                newnote.Show();
            }
            catch (IndexOutOfRangeException indexexc)
            {
                MessageBox.Show("Fout: " + indexexc.Message);
            }
        }

        public void EditNewNote(int noteid)
        {
            string title = noteslst[noteid - 1].NoteTitle;
            string content = noteslst[noteid - 1].NoteContent;
            int color = noteslst[noteid - 1].NoteColor;
            bool transparenty = getTransparency();            
            frmNewNote createnewnote = new frmNewNote(this, transparenty, color,noteid, title, content);
            createnewnote.Show();
        }

        public void UpdateNote(int noteid, string title, string content, bool visible)
        {
            noteslst[noteid - 1].NoteTitle = title;
            noteslst[noteid - 1].NoteContent = content;
            noteslst[noteid - 1].Visible = visible;
            if (visible) { 
                noteslst[noteid - 1].Show(); 
            }
        }

        public void UpdateAllFonts()
        {            
            
            foreach (frmNote curfrmnote in noteslst)
            {
                curfrmnote.PaintColorNote();
            }
        }

		// Private Methods (3) 

        private bool getTransparency()
        {
            xmlHandler xmlSettings = new xmlHandler(true);
            if (xmlSettings.getXMLnode("transparecy") == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }


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

                frmNote newnote;
                if ((visible == true) && (ontop))
                {
                    newnote = new frmNote(this,newid, true, true,title, content, transparecy, notecolor, locX, locY, notewith, noteheight);
                    newnote.Show();
                }
                else if ((visible==true) && (ontop==false))
                {
                    newnote = new frmNote(this, newid, true, false, title, content, transparecy, notecolor, locX, locY, notewith, noteheight);
                    newnote.Show();
                }
                else
                {
                    newnote = new frmNote(this,newid, false, false, title, content, transparecy, notecolor, locX, locY, notewith, noteheight);
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

            xmlHandler getSettings = new xmlHandler(true);
            string notesavepath = getSettings.getXMLnode("notesavepath");
            

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
                if (id > 1000) { MessageBox.Show("Error: Too many notes"); return; }
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
