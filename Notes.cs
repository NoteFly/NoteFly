﻿/* Copyright (C) 2009
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

        private Int16 defaultcolor = 1;
        private string notesavepath;
        private List<FrmNote> noteslst;
        private bool notesupdated = false;
        private bool highlightHTML = false;
        private bool highlightC = false;
        private bool transparecy = false;
        private bool twitterenabled = false;

        #endregion Fields

        #region Constructors (1)

        public Notes(bool firstrun)
        {
            noteslst = new List<FrmNote>();
            SetSettings();
            LoadNotes(firstrun);
        }

        #endregion Constructors

        #region Properties (7)

        public List<FrmNote> GetNotes
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

        public Int16 NumNotes
        {
            get
            {
                Int16 numnotes = Convert.ToInt16(this.noteslst.Count);
                if (numnotes > 255)
                {
                    throw new Exception("error too many notes.");
                }
                return numnotes;
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
        /// Check syntax,
        /// returns true, if no error.
        /// </summary>
        /// <param name="syntaxhighlight"></param>
        /// <param name="rtb"></param>
        public bool CheckSyntax(RichTextBox rtb)
        {
            if ((highlightHTML == true) || (highlightC == true))
            {
                TextHighlight texthighlight = new TextHighlight(highlightHTML, highlightC);
                return texthighlight.CheckSyntax(rtb);
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Draws a new note and saves the xml note file.(call to SaveNewNote)
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="notecolor"></param>
        public void DrawNewNote(string title, string content, Int16 notecolor)
        {
            try
            {
                Int16 newid = Convert.ToInt16(noteslst.Count + 1);
                string notefilenm = SaveNewNote(newid, title, content, defaultcolor);
                if (String.IsNullOrEmpty(notefilenm)) { return; }
                FrmNote newnote = new FrmNote(this, newid, title, content, notecolor);
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
                string title = noteslst[noteslistpos].NoteTitle;
                string content = noteslst[noteslistpos].NoteContent;
                Int16 color = noteslst[noteslistpos].NoteColor;
                FrmNewNote newnote = new FrmNewNote(this, color, noteid, title, content);
                newnote.Show();
            }
            else
            {
                throw new CustomExceptions("Note not found in memory.");
            }

        }

        /// <summary>
        /// check settings and set variables
        /// </summary>
        public void SetSettings()
        {
            xmlHandler getSettings = new xmlHandler(true);

            this.defaultcolor = Convert.ToInt16(getSettings.getXMLnodeAsInt("defaultcolor"));
            if (getSettings.getXMLnodeAsBool("transparecy") == true)
            {
                this.transparecy = true;
            }
            else
            {
                this.transparecy = false;
            }
            if (getSettings.getXMLnodeAsBool("highlightHTML") == true)
            {
                this.highlightHTML = true;
            }
            else
            {
                this.highlightHTML = false;
            }
            if (getSettings.getXMLnodeAsBool("highlightC") == true)
            {
                this.highlightC = true;
            }
            else
            {
                this.highlightC = false;
            }
            this.notesavepath = getSettings.getXMLnode("notesavepath");
            this.twitterenabled = !String.IsNullOrEmpty(getSettings.getXMLnode("twitteruser"));
        }

        /// <summary>
        /// Update a partialer note with noteid
        /// </summary>
        /// <param name="noteid">id of note</param>
        /// <param name="title">new title</param>
        /// <param name="content">new content</param>
        /// <param name="visible">is visible?</param>
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
            noteslst[notelstpos].CheckThings();
            this.notesupdated = true;
        }

        /// <summary>
        /// Update all fonts (family/size etc.) for all notes.
        /// </summary>
        public void UpdateAllFonts()
        {
            foreach (FrmNote curfrmnote in noteslst)
            {
                curfrmnote.PaintColorNote();
                curfrmnote.CheckThings();
            }
            
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
        private FrmNote CreateNote(bool visible, bool ontop, string title, string content, Int16 notecolor, int locX, int locY, int notewith, int noteheight)
        {
            try
            {
                Int16 newid = Convert.ToInt16(noteslst.Count + 1);
                FrmNote newnote = new FrmNote(this, newid, visible, ontop, title, content, notecolor, locX, locY, notewith, noteheight);
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
                Int16 notecolor = Convert.ToInt16(parserNote.getXMLnodeAsInt("color"));
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
                int tipnotewidth = 320;
                int tipnoteheight = 280;
                int tipnoteposx = (Screen.PrimaryScreen.WorkingArea.Width / 2) - (tipnotewidth / 2);
                int tipnoteposy = (Screen.PrimaryScreen.WorkingArea.Height / 2) - (tipnoteheight / 2);
                noteslst.Add(CreateNote(true, false, "first example note", "This is a example note.\r\nYou can change color of this note by rightclicking this note.\r\nYou can delete this note, by rightclicking the systray icon choice manage note and then press delete note.\r\nBy clicking on the cross of this note. This note will hiden.\r\nYou can get it back with the manage notes window.\r\nPlease close the installer now.", 0, tipnoteposx, tipnoteposy, tipnotewidth, tipnoteheight));
            }

            #if DEBUG
            //LoadNotesStressTest(100);

            //not really exact timing.
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
        private string SaveNewNote(int id, string title, string text, Int16 numcolor)
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
        /// Methode that creates a lot of notes for stress testing this app.
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
                Int16 notecolor = Convert.ToInt16(ran.Next(0, 6));
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
