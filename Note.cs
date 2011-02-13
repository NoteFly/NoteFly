﻿//-----------------------------------------------------------------------
// <copyright file="Note.cs" company="GNU">
//  NoteFly a note application.
//  Copyright (C) 2010-2011  Tom
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

    /// <summary>
    /// Note data class, holds setting note.
    /// </summary>
    public class Note
    {
        public string tempcontent;

        private FrmNote frmnote;
        private Notes notes;
        private string filename;

        /// <summary>
        /// Creating a new note instance, completly new note.
        /// </summary>
        public Note(Notes notes, string filename)
        {
            this.notes = notes;
            this.filename = filename;
        }

        /// <summary>
        /// The filename of this note.
        /// </summary>
        public string Filename
        {
            get
            {
                return this.filename;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    this.filename = value;
                }
                else
                {
                    throw new CustomException("Filename is null or empty.");
                }
            }
        }

        /// <summary>
        /// The note title.
        /// </summary>
        public string title;
        /// <summary>
        /// Visablity note.
        /// </summary>
        public bool visible;
        /// <summary>
        /// The note skin.
        /// </summary>
        public int skinNr;
        /// <summary>
        /// Is the note displayed ontop of all windows.
        /// </summary>
        public bool ontop;
        /// <summary>
        /// Is the note rolled up.
        /// </summary>
        public bool rolledUp;
        /// <summary>
        /// Is the note locked.
        /// </summary>
        public bool locked;
        /// <summary>
        /// The X position of the note.
        /// </summary>
        public int x;
        /// <summary>
        ///  The Y position of the note.
        /// </summary>
        public int y;
        /// <summary>
        /// The width of the note.
        /// </summary>
        public int width;
        /// <summary>
        /// The height of the note.
        /// </summary>
        public int height;

        /// <summary>
        /// Create a new frmNote with this note.
        /// visible is set to true.
        /// </summary>
        public void CreateForm()
        {
            this.frmnote = new FrmNote(this.notes, this);
            this.visible = true;
            this.frmnote.Show();
        }

        /// <summary>
        /// Cleanup frmNote resources.
        /// The note is made not visible to user and form isn't required anymore.
        /// </summary>
        public void DestroyForm()
        {
            if (this.frmnote != null)
            {
                this.frmnote.Close();
            }
            this.visible = false;
            this.tempcontent = null;
            this.frmnote = null;
            this.notes.frmmangenotesneedupdate = true;
            TrayIcon.RefreshFrmManageNotes();
            GC.Collect();
        }

        /// <summary>
        /// Display this frmnote to the foreground.
        /// </summary>
        public void BringNoteToFront()
        {
            if (this.frmnote != null)
            {
                this.frmnote.BringToFront();
                this.frmnote.Activate();
            }
        }

        /// <summary>
        /// Gets the content of the note from the file.
        /// </summary>
        /// <returns></returns>
        public string GetContent()
        {
            if (this.frmnote == null)
            {
                string notefilepath = Path.Combine(Settings.notesSavepath, this.Filename);
                if (File.Exists(notefilepath))
                {
                    return xmlUtil.GetContentString(notefilepath, "content");
                }
                else
                {
                    //error
                    throw new CustomException("Cannot read note content, note not found: " + notefilepath);
                }
            }
            else
            {
                return this.frmnote.rtbNote.Rtf;
            }
        }

        /// <summary>
        /// Update the form.
        /// </summary>
        public void UpdateForm()
        {
            if (frmnote != null)
            {
                frmnote.UpdateForm(false);
                frmnote.UpdateForm(true);
            }
        }

    }
}