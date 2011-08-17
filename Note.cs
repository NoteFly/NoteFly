//-----------------------------------------------------------------------
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
    using System.IO;

    /// <summary>
    /// Note data class, holds setting note.
    /// </summary>
    public class Note
    {
        #region Fields (14)

        /// <summary>
        /// The filename of this note.
        /// </summary>
        private string filename;

        /// <summary>
        /// The form of this note.
        /// </summary>
        private FrmNote frmnote;

        /// <summary>
        /// The height of the note.
        /// </summary>
        private int height;

        /// <summary>
        /// Is the note locked.
        /// </summary>
        private bool locked;

        /// <summary>
        /// reference to notes class.
        /// </summary>
        private Notes notes;

        /// <summary>
        /// Is the note displayed ontop of all windows.
        /// </summary>
        private bool ontop;

        /// <summary>
        /// Is the note rolled up.
        /// </summary>
        private bool rolledUp;

        /// <summary>
        /// The note skin.
        /// </summary>
        private int skinNr;

        /// <summary>
        /// temporary content.
        /// </summary>
        private string tempcontent;

        /// <summary>
        /// The note title.
        /// </summary>
        private string title;

        /// <summary>
        /// Visibility note.
        /// </summary>
        private bool visible;

        /// <summary>
        /// The width of the note.
        /// </summary>
        private int width;

        /// <summary>
        /// The X position of the note.
        /// </summary>
        private int x;

        /// <summary>
        ///  The Y position of the note.
        /// </summary>
        private int y;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the Note class.
        /// Completly new note.
        /// </summary>
        /// <param name="notes">Reference to notes.</param>
        /// <param name="filename">The note filename.</param>
        public Note(Notes notes, string filename)
        {
            this.notes = notes;
            this.filename = filename;
        }

        #endregion Constructors

        #region Properties (12)

        /// <summary>
        /// Gets or sets the filename of this note.
        /// </summary>
        public string Filename
        {
            get
            {
                return this.filename;
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.filename = value;
                }
                else
                {
                    throw new ApplicationException("Filename is null or empty.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the height of the note.
        /// </summary>
        public int Height
        {
            get
            {
                return this.height;
            }

            set
            {
                this.height = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the note is locked.
        /// </summary>
        public bool Locked
        {
            get
            {
                return this.locked;
            }

            set
            {
                this.locked = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the note displayed ontop of all windows.
        /// </summary>
        public bool Ontop
        {
            get
            {
                return this.ontop;
            }

            set
            {
                this.ontop = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the note rolled up.
        /// </summary>
        public bool RolledUp
        {
            get
            {
                return this.rolledUp;
            }

            set
            {
                this.rolledUp = value;
            }
        }

        /// <summary>
        /// Gets or sets the note skin.
        /// </summary>
        public int SkinNr
        {
            get
            {
                return this.skinNr;
            }

            set
            {
                this.skinNr = value;
            }
        }

        /// <summary>
        /// Gets or sets temporary content note content store.
        /// </summary>
        public string Tempcontent
        {
            get
            {
                return this.tempcontent;
            }

            set
            {
                this.tempcontent = value;
            }
        }

        /// <summary>
        /// Gets or sets the note title.
        /// </summary>
        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the note is visible.
        /// </summary>
        public bool Visible
        {
            get
            {
                return this.visible;
            }

            set
            {
                this.visible = value;
            }
        }

        /// <summary>
        /// Gets or sets the width of the note.
        /// </summary>
        public int Width
        {
            get
            {
                return this.width;
            }

            set
            {
                this.width = value;
            }
        }

        /// <summary>
        /// Gets or sets the X position of the note on the screen.
        /// </summary>
        public int X
        {
            get
            {
                return this.x;
            }

            set
            {
                this.x = value;
            }
        }

        /// <summary>
        /// Gets or sets the Y position of the note on the screen.
        /// </summary>
        public int Y
        {
            get
            {
                return this.y;
            }

            set
            {
                this.y = value;
            }
        }

        #endregion Properties

        #region Methods (5)

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
        /// Create a new frmNote with this note.
        /// visible is set to true.
        /// </summary>
        public void CreateForm(bool wordwarp)
        {
            this.frmnote = new FrmNote(this.notes, this, wordwarp);
            this.visible = true;
            this.frmnote.Show();
            for (int i = 0; i < Program.plugins.Length; i++)
            {
                Program.plugins[i].ShowingNote(this);
            }
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
            this.frmnote = null;

            for (int i = 0; i < Program.plugins.Length; i++)
            {
                Program.plugins[i].ShowingNote(this);
            }

            GC.Collect();
        }

        /// <summary>
        /// Gets the content of the note from the file.
        /// </summary>
        /// <returns>The rich text formatted note content</returns>
        public string GetContent()
        {
            if (this.frmnote == null)
            {
                string notefilepath = Path.Combine(Settings.NotesSavepath, this.Filename);
                if (File.Exists(notefilepath))
                {
                    return xmlUtil.GetContentString(notefilepath, "content");
                }
                else
                {
                    throw new ApplicationException("Cannot read note content, note file not found: " + notefilepath);
                }
            }
            else
            {
                return this.frmnote.GetContentRTF;
            }
        }

        /// <summary>
        /// Update the form.
        /// </summary>
        public void UpdateNoteForm()
        {
            if (this.frmnote != null)
            {
                this.frmnote.UpdateForm(false);
                this.frmnote.UpdateForm(true);
            }
        }

        #endregion Methods
    }
}
