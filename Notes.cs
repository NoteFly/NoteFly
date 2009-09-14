/* Copyright (C) 2009
 * 
 * This program is free software; you can redistribute it and/or modify it
 * Free Software Foundation; either version 2, or (at your option) any
 * later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SimplePlainNote
{
    public class Notes
    {
        #region Fields (7)

        private int defaultcolor = 1;
        private string notesavepath;
        private List<frmNote> noteslst;
        private bool notesupdated = false;
        private bool syntaxhighlight = false;
        private bool transparecy = false;
        private bool twitterenabled = false;

        #endregion Fields

        #region Constructors (1)

        public Notes(bool firstrun)
        {
            noteslst = new List<frmNote>();
            SetSettings();
            LoadNotes(firstrun);
        }

        #endregion Constructors

        #region Properties (7)

        public List<frmNote> GetNotes
        {
            get
            {
                return this.noteslst;
            }
        }

        public string NoteSavePath
        {
            get
            {
                return this.notesavepath;
            }
        }

        public bool NotesUpdated
        {
            get
            {
                return this.notesupdated;
            }
            set
            {
                this.notesupdated = value;
            }
        }

        public UInt16 NumNotes
        {
            get
            {
                UInt16 numnotes = Convert.ToUInt16(this.noteslst.Count);
                if (numnotes > 255)
                {
                    throw new Exception("error too many notes.");
                }
                return numnotes;
            }
        }

        public bool SyntaxHighlightEnabled
        {
            get
            {
                return this.syntaxhighlight;
            }
        }

        public bool Transparency
        {
            get
            {
                return this.transparecy;
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

        #region Methods (10)

        // Public Methods (6) 

        /// <summary>
        /// Check syntax, use TextHighlight class too.
        /// </summary>
        /// <param name="syntaxhighlight"></param>
        /// <param name="rtb"></param>
        public void CheckSyntax(bool syntaxhighlight, RichTextBox rtb)
        {
            if (syntaxhighlight == true)
            {
                TextHighlight texthighlight = new TextHighlight();

                int selPos = rtb.SelectionStart;

                foreach (System.Text.RegularExpressions.Match keyWordMatch in texthighlight.getRegexHTML.Matches(rtb.Text))
                {
                    rtb.Select(keyWordMatch.Index, keyWordMatch.Length);
                    rtb.SelectionColor = System.Drawing.Color.Blue;
                    rtb.SelectionStart = selPos;
                    rtb.SelectionColor = System.Drawing.Color.Black;
                }
                rtb.DeselectAll();
            }
        }

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
                UInt16 newid = Convert.ToUInt16(noteslst.Count + 1);
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

        /// <summary>
        /// Edit a note.
        /// </summary>
        /// <param name="noteid"></param>
        public void EditNewNote(int noteid)
        {
            int noteslistpos = noteid - 1;
            if ((noteslistpos >= 0) && (noteslistpos <= this.NumNotes))
            {
                string title = noteslst[noteid - 1].NoteTitle;
                string content = noteslst[noteid - 1].NoteContent;
                int color = noteslst[noteid - 1].NoteColor;
                frmNewNote createnewnote = new frmNewNote(this, color, noteid, title, content);
                createnewnote.Show();
            }
            else
            {
                throw new Exception("Error: note not found in memory.");
            }

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
            if (visible)
            {
                noteslst[notelstpos].Show();
            }
            noteslst[notelstpos].checkthings();
            this.notesupdated = true;
        }
        // Private Methods (4) 

        /// <summary>
        /// This method set a limit to how many notes can be loaded.
        /// This is to prevent a hang.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true when limit is reached.</returns>
        private bool CheckLimitNotes(int id)
        {
            if (id > 255)
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
                UInt16 newid = Convert.ToUInt16(noteslst.Count + 1);
                frmNote newnote = new frmNote(this, newid, visible, ontop, title, content, notecolor, locX, locY, notewith, noteheight);
                newnote.FormBorderStyle = FormBorderStyle.None;
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

        /// <summary>
        /// Loads all notes.
        /// </summary>
        /// <param name="firstrun"></param>
        private void LoadNotes(bool firstrun)
        {
            #if DEBUG
            DateTime starttime = DateTime.Now;
            #endif

            if (!Directory.Exists(this.notesavepath))
            {
                DialogResult result = MessageBox.Show("Error: Folder with notes does not exist.\r\nDo want to try loading notes from default application data folder?", "note folder doesn't exist", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.No)
                {
                    return;
                }
                else
                {
                    xmlHandler getAppdata = new xmlHandler(true);
                    this.notesavepath = getAppdata.AppDataFolder;
                    #if DEBUG
                    starttime = DateTime.Now;
                    #endif
                }
            }

            UInt16 id = 1;
            string notefile = Path.Combine(this.notesavepath, id + ".xml");
            while (File.Exists(notefile) == true)
            {
                xmlHandler parserNote = new xmlHandler(notefile);

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
                notefile = Path.Combine(notesavepath, id + ".xml");

                if (CheckLimitNotes(id)) { MessageBox.Show("Error: Too many notes to load. More than 500 notes."); return; }
            }
            if (firstrun)
            {
                int tipnotewidth = 280;
                int tipnoteheight = 240;
                int tipnoteposx = (Screen.PrimaryScreen.WorkingArea.Width / 2) - (tipnotewidth / 2);
                int tipnoteposy = (Screen.PrimaryScreen.WorkingArea.Height / 2) - (tipnoteheight / 2);
                noteslst.Add(CreateNote(true, false, "first example note", "This is a example note.\r\nYou can change color of this note by rightclicking this note.\r\nYou can delete this note, by rightclicking the systray icon choice manage note and then press delete note.\r\nBy clicking on the cross of this note. This note will hiden.\r\nYou can get it back with the manage notes window.", 0, tipnoteposx, tipnoteposy, tipnotewidth, tipnoteheight));
            }

#if DEBUG
            //LoadNotesStressTest(100);

            //no good.
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
            string notefile = Path.Combine(notesavepath, id + ".xml");
            xmlHandler xmlnote = new xmlHandler(notefile);
            if (xmlnote.WriteNote(true, false, numcolor, title, text, 10, 10, 240, 240) == false)
            {
                MessageBox.Show("Error writing note.");
                return null;
            }
            return notefile;
        }

        #endregion Methods

#if DEBUG
        /// <summary>
        /// Mthode that creates a lot of notes for stress testing this app.
        /// </summary>
        /// <param name="maxnotes"></param>
        private void LoadNotesStressTest(int maxnotes)
        {
            Random ran = new Random();

            for (int id = 1; id <= maxnotes; id++)
            {
                bool visible = true;
                bool ontop = false;
                string title = "test nr." + id;
                string content = "This is a stress test of creating a lot of notes, to see how fast or slow it loads." +
                                 "warning: To preven a note from saving don't move them!";
                int notecolor = ran.Next(0, 6);
                int noteLocX = ran.Next(0, 360);
                int noteLocY = ran.Next(0, 240);
                int notewidth = 180;
                int noteheight = 160;

                noteslst.Add(CreateNote(visible, ontop, title, content, notecolor, noteLocX, noteLocY, notewidth, noteheight));

                if (CheckLimitNotes(id)) { MessageBox.Show("Limit reached."); }
            }
        }
#endif
    }
}
