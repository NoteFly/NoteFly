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

    public class Note
    {
        public FrmNote frmnote;
        private DateTime datecreated;

        /// <summary>
        /// Creating a new note instance, completly new note.
        /// </summary>
        public Note()
        {
            datecreated = DateTime.Now;
        }

        /// <summary>
        /// Creating a new note instance, loading a note form file.
        /// </summary>
        /// <param name="created"></param>
        public Note(DateTime datecreated)
        {
            this.datecreated = datecreated;
        }

        public string Title { get; set; }
        public bool Visible { get; set; }
        public bool Ontop { get; set; }
        public bool RolledUp { get; set; }
        public bool Locked { get; set; }
        public short Id { get; set; }
        public short Color { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        /// <summary>
        /// Create a new Form with this note.
        /// </summary>
        public void CreateForm()
        {
            this.frmnote = new FrmNote(this);
        }

        /// <summary>
        /// Cleanu resources.
        /// </summary>
        public void DestroyForm()
        {
            this.frmnote.Close();
            this.frmnote = null;
            GC.Collect();
        }

        /// <summary>
        /// Gets the content of this note from the frmnote
        /// </summary>
        public string Content
        {
            get
            {
                if (frmnote != null)
                {
                    //return frmnote;
                }
                else
                {
                    return "stub.";
                }
            }
        }

        /// <summary>
        /// Gets date time created.
        /// </summary>
        public DateTime DateCreated
        {
            get
            {
                return this.datecreated;
            }
        }
    }
}
