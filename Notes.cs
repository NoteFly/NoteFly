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

		#endregion Fields 

		#region Constructors (1) 

        public Notes()
        {
            noteslst = new List<frmNote>();
            loadNotes();
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

		#region Methods (1) 

		// Private Methods (1) 

        private void loadNotes()
        {
            #if DEBUG
            DateTime starttime = DateTime.Now;
            #endif

            xmlHandler getSettings = new xmlHandler(true);
            string notesavepath = getSettings.getXMLnode("notesavepath");

            int id = 1;

            string curnotefile = notesavepath + id + ".xml";

            while (File.Exists(@curnotefile) == true)
            {
                xmlHandler parserNote = new xmlHandler(false, id + ".xml");

                bool visible = parserNote.getXMLnodeAsBool("visible");
                string title = parserNote.getXMLnode("title");
                string content = parserNote.getXMLnode("content");
                int notecolor = parserNote.getXMLnodeAsInt("color");
                int noteLocX = parserNote.getXMLnodeAsInt("x");
                int noteLocY = parserNote.getXMLnodeAsInt("y");
                int notewidth = parserNote.getXMLnodeAsInt("width");
                int noteheight = parserNote.getXMLnodeAsInt("heigth");

                noteslst.Add(CreateNote(visible, title, content, notecolor, noteLocX, noteLocY, notewidth, noteheight));

                id++;
                curnotefile = notesavepath + id + ".xml";
                if (id > 1000) { MessageBox.Show("Error: Too many notes"); return; }
            }            

            #if DEBUG
            DateTime endtime = DateTime.Now;
            TimeSpan debugtime = endtime - starttime;
            MessageBox.Show("taken: " + debugtime.Milliseconds + " ms\r\n " + debugtime.Ticks + " ticks");
            #endif         
        }


        /// <summary>
        /// Create a note GUI.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="notecolor"></param>        
        public frmNote CreateNote(bool visible, string title, string content, int notecolor, int locX, int locY, int notewith, int noteheight)
        {
            try
            {
                int newid = noteslst.Count + 1;

                frmNote newnote;
                if (visible == true)
                {
                    newnote = new frmNote(true, newid, title, content, notecolor, locX, locY, notewith, noteheight);
                    newnote.Show();
                }
                else
                {
                    newnote = new frmNote(false, newid, title, content, notecolor, locX, locY, notewith, noteheight);
                }
                return newnote;

            }
            catch (IndexOutOfRangeException indexexc)
            {
                MessageBox.Show("Fout: " + indexexc.Message);
                return null;
            }
        }

        public void CreateDefaultNote(string title, string content, int notecolor)
        {
            try
            {
                xmlHandler getXmlSettings = new xmlHandler(true);
                string defaultcolor = getXmlSettings.getXMLnode("defaultcolor");

                int newid = noteslst.Count + 1;
                string notefilenm = SaveNewNote(newid, title, content, defaultcolor);
                if (String.IsNullOrEmpty(notefilenm)) { return; }
                frmNote newnote = new frmNote(newid, title, content, notecolor);
                noteslst.Add(newnote);
                newnote.Show();
            }
            catch (IndexOutOfRangeException indexexc)
            {
                MessageBox.Show("Fout: " + indexexc.Message);
            }
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
            if (xmlnote.WriteNote(true, numcolor, title, text, 10, 10, 240, 240) == false)
            {
                MessageBox.Show("Error writing note.");
                return null;
            }
            return notefile;
        }
		#endregion Methods 
    }
}
