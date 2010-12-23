//-----------------------------------------------------------------------
// <copyright file="Note.cs" company="GNU">
//  NoteFly a note application.
//  Copyright (C) 2010  Tom
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
// </copyright>
//-----------------------------------------------------------------------
namespace NoteFly
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.IO;

    public class Note
    {
        public FrmNote frmnote;

        private Notes notes;
        private DateTime datecreated;

        /// <summary>
        /// Creating a new note instance, completly new note.
        /// </summary>
        public Note(Notes notes)
        {
            this.notes = notes;
            datecreated = DateTime.Now;
        }

        /// <summary>
        /// Creating a new note instance, loading a note from file.
        /// </summary>
        /// <param name="created"></param>
        public Note(Notes notes, DateTime datecreated)
        {
            this.notes = notes;
            this.datecreated = datecreated;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public bool Visible { get; set; }
        public bool Ontop { get; set; }
        public bool RolledUp { get; set; }
        public bool Locked { get; set; }
        public int SkinNr { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        /// <summary>
        /// Gets date and time when this note was created.
        /// </summary>
        public DateTime DateCreated
        {
            get
            {
                return this.datecreated;
            }
        }

        /// <summary>
        /// Create a new frmNote with this note.
        /// The note now has to be visible to the user.
        /// </summary>
        public void CreateForm()
        {
            this.frmnote = new FrmNote(this.notes, this);
            this.Visible = true;
        }

        /// <summary>
        /// Cleanup frmNote resources.
        /// The note is made not visible to user and form isn't required anymore.
        /// </summary>
        public void DestroyForm()
        {
            this.frmnote.Close();
            this.frmnote = null;
            this.Visible = false;
            GC.Collect();
        }

        /// <summary>
        /// Gets the content of the note from the file.
        /// </summary>
        /// <returns></returns>
        public string GetContent()
        {
            if (this.frmnote == null)
            {
                string notefilepath = Path.Combine(Settings.NotesSavepath, this.notes.NewNoteFilename(this.Id, this.Title));
                if (File.Exists(notefilepath))
                {
                    return xmlUtil.GetContentString(notefilepath, "Content");
                }
                else
                {
                    //error
                    throw new CustomException("Cannot read note content, note not found.");
                }
            }
            else
            {
                return this.frmnote.rtbNote.Rtf;
            }
        }

        public string GetSkinName()
        {
            return this.notes.GetSkinName(this.SkinNr);
        }
    }
}
