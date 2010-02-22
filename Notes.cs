/* Copyright (C) 2009-2010
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
using System.IO;
using System.Windows.Forms;

namespace NoteFly
{
    public class Notes
    {
		#region Fields (9) 

        private Int16 defaultcolor = 1;
        private String notesavepath;
        private List<FrmNote> noteslst;
        private Boolean notesupdated = false;
        private Boolean transparecy = false;
        private Boolean twitterenabled = false;
        private Boolean resourcevars = false;
        private Int16 textdirection = 0;

		#endregion Fields 

		#region Constructors (1) 

        public Notes(bool firstrun)
        {
            noteslst = new List<FrmNote>();
            SetSettings();
            LoadNotes(firstrun);
        }

		#endregion Constructors 

		#region Properties (8) 

        //public Boolean HighlightC { get; set; }

        public Boolean HighlightHTML { get; set; }

        public List<FrmNote> GetNotes
        {
            get
            {
                return this.noteslst;
            }
        }

        public String NoteSavePath
        {
            get
            {
                return this.notesavepath;
            }
        }

        public Boolean NotesUpdated
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
                    throw new Exception("Too many notes.");
                }
                return numnotes;
            }
        }

        public Boolean Transparency
        {
            get
            {
                return this.transparecy;
            }
        }

        public Boolean TwitterEnabled
        {
            get
            {
                return this.twitterenabled;
            }
        }

        public Int16 TextDirection
        {
            get
            {
                return this.textdirection;
            }
            set
            {
                textdirection = value;
            }
        }

		#endregion Properties 

		#region Methods (10)
        /// <summary>
        /// Draws a new note and saves the xml note file.(call to SaveNewNote)
        /// </summary>
        /// <param name="title">the title</param>
        /// <param name="content">the note content</param>
        /// <param name="notecolor">color number</param>
        public void DrawNewNote(string title, string content, Int16 notecolor)
        {
            try
            {
                Int16 newid = Convert.ToInt16(noteslst.Count + 1);
                string notefilenm = SaveNewNote(newid, title, content, defaultcolor);
                Log.write(LogType.info, "note created: " + notefilenm);
                if (String.IsNullOrEmpty(notefilenm)) { throw new CustomException("cannot create filename."); }
                FrmNote newnote = new FrmNote(this, newid, title, content, notecolor);
                noteslst.Add(newnote);
                newnote.StartPosition = FormStartPosition.Manual;
                newnote.Show();
            }
            catch (Exception exc)
            {
                throw new CustomException(exc.Message+" "+exc.StackTrace);
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
                throw new CustomException("Note not found in memory.");
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
            if (getSettings.getXMLnodeAsBool("highlightHTML"))
            {
                HighlightHTML = true;
            }

            this.textdirection = Convert.ToInt16(getSettings.getXMLnodeAsInt("textdirection"));
            this.notesavepath = getSettings.getXMLnode("notesavepath");
            this.twitterenabled = !String.IsNullOrEmpty(getSettings.getXMLnode("twitteruser"));


            if (getSettings.getXMLnodeAsBool("savesession") == true)
            {
                FacebookSettings.uid = getSettings.getXMLnode("uid");
                String strSessionExpires = getSettings.getXMLnode("sesionexpires");
                if (!String.IsNullOrEmpty(strSessionExpires))
                {
                    try
                    {

                        FacebookSettings.sesionexpires = Convert.ToDouble(strSessionExpires);
                    }
                    catch (InvalidCastException)
                    {
                        throw new CustomException("facebook session expires is not a valid unix (double) valeau.");
                    }
                }
                FacebookSettings.sessionsecret = getSettings.getXMLnode("sessionsecret");
                FacebookSettings.sessionkey = getSettings.getXMLnode("sessionkey");
            }
        }

        /// <summary>
        /// Update all fonts (family/size etc.) for all notes.
        /// </summary>
        public void UpdateAllFonts()
        {
            foreach (FrmNote curfrmnote in noteslst)
            {
                curfrmnote.CheckThings();
            }
        }

        /// <summary>
        /// Update a note
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
            noteslst[notelstpos].UpdateThisNote();
            this.notesupdated = true;
            Log.write(LogType.info, "update note ID:"+noteid);
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
        /// Create a new FrmNote.
        /// </summary>
        /// <param name="visible">is visible</param>
        /// <param name="ontop">is on top</param>
        /// <param name="title">title</param>
        /// <param name="content">content</param>
        /// <param name="notecolor">color note, 0 Gold, 1 orange, 2 White, 3 LawnGreen, 4 CornflowerBlue, 5 Magenta, 6 Red</param>
        /// <param name="locX">X coordinate</param>
        /// <param name="locY">Y coordinate</param>
        /// <param name="notewith"></param>
        /// <param name="noteheight"></param>
        /// <returns>form</returns>
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
            catch (Exception exc)
            {
                throw new CustomException(exc.Message + " " + exc.StackTrace);
            }
        }

        /// <summary>
        /// Loads all notes.
        /// </summary>
        /// <param name="firstrun"></param>
        private void LoadNotes(bool firstrun)
        {
            if (!Directory.Exists(this.notesavepath))
            {
                String notefoldernoteexist = "Folder with notes does not exist.\r\nDo want to try loading notes from default application data folder?";
                DialogResult result = MessageBox.Show(notefoldernoteexist, "notefolder doesn't exist", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.No)
                {
                    Log.write(LogType.error, notefoldernoteexist+" No");
                    return;
                }
                else
                {
                    Log.write(LogType.error, notefoldernoteexist + " Yes");
                    xmlHandler getAppdata = new xmlHandler(true);
                    this.notesavepath = getAppdata.AppDataFolder;
                }
            }         

            UInt16 id = 1;
            string notefile = Path.Combine(this.notesavepath, id + ".xml");
            while (File.Exists(notefile) == true)
            {
                xmlHandler parserNote = new xmlHandler(notefile);

                Boolean visible = parserNote.getXMLnodeAsBool("visible");
                Boolean ontop = parserNote.getXMLnodeAsBool("ontop");
                String title = parserNote.getXMLnode("title");
                String content = parserNote.getXMLnode("content");
                Int32[] NoteSettingsInt = parserNote.ParserNoteInts();
                Int16 notecolor = Convert.ToInt16(NoteSettingsInt[0]);

                noteslst.Add(CreateNote(visible, ontop, title, content, notecolor, NoteSettingsInt[1], NoteSettingsInt[2], NoteSettingsInt[3], NoteSettingsInt[4]));
                id++;
                notefile = Path.Combine(notesavepath, id + ".xml");

                if (CheckLimitNotes(id)) { 
                    String toomanynotes = "Too many notes to load.";
                    MessageBox.Show(toomanynotes);
                    Log.write(LogType.error, toomanynotes);
                    return;
                }
            }
            id++;
            notefile = Path.Combine(this.notesavepath, id + ".xml");
            if (File.Exists(notefile)==true)
            {
                String notemissing = notesavepath + Convert.ToString(id - 1) + ".xml is missing.";
                MessageBox.Show(notemissing);
                Log.write(LogType.error, notemissing);
            }
            if (firstrun)
            {
                int tipnotewidth = 320;
                int tipnoteheight = 280;
                int tipnoteposx = (Screen.PrimaryScreen.WorkingArea.Width / 2) - (tipnotewidth / 2);
                int tipnoteposy = (Screen.PrimaryScreen.WorkingArea.Height / 2) - (tipnoteheight / 2);
                noteslst.Add(CreateNote(true, false, "Example", 
                    "This is a example note.\r\n"+
                    "Please close the installer now."+
                    "You can chance colour of this note by rightclicking on this note.\r\n" +
                    "You can delete this note, by rightclicking the systray icon choice manage notes\r\n"+
                    "and then press delete note button for this particuler note.\r\n"+
                    "By clicking on the cross on this note this note will be hidden.\r\n"+
                    "You can get it back with the manage notes window.\r\n"
                    , 0, tipnoteposx, tipnoteposy, tipnotewidth, tipnoteheight));
            }

            #if DEBUG
            Log.write(LogType.info, "start stress test");
            //LoadNotesStressTest(200);
            LoadNotesStressTest(10);
            Log.write(LogType.info, "finished stress test");
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
                throw new CustomException("Cannot write note.");
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
                string title = "test nr." + id+" testalongtitlesoiteasytoseeifresizingofmanagenoteisdonecorrectlyblablabla";
                string content = "This is a stress test creating a lot of notes, to see how fast or slow it loads.\r\n" +
                                 "warning: To prevent this note from saving don't move or touch it!";
                Int16 notecolor = Convert.ToInt16(ran.Next(0, 6));
                int noteLocX = ran.Next(0, 360);
                int noteLocY = ran.Next(0, 240);
                int notewidth = 180;
                int noteheight = 180;

                noteslst.Add(CreateNote(visible, ontop, title, content, notecolor, noteLocX, noteLocY, notewidth, noteheight));

                if (CheckLimitNotes(id)) { 
                    String maxnoteslimit = "Maximum notes limit reached.";
                    MessageBox.Show(maxnoteslimit);
                    Log.write(LogType.error, maxnoteslimit);
                    return;
                }
            }
        }
        #endif
    }
}
