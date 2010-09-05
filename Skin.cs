//-----------------------------------------------------------------------
// <copyright file="Skin.cs" company="GNU">
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
    using System.Drawing;

    /// <summary>
    /// Class that provide skin options to notes.
    /// </summary>
    public class Skin
    {
        #region Fields (1)

        /// <summary>
        /// The color of the note.
        /// </summary>
        private int notecolor = 0;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the Skin class.
        /// </summary>
        /// <param name="numcolor">color number</param>
        public Skin(int numcolor)
        {
            this.notecolor = numcolor;
        }

        #endregion Constructors

        /// <summary>
        /// Gets the number of possible note colors. (Counting from zero.)
        /// </summary>
        public int MaxNotesColors
        {
            get
            {
                return 6;
            }
        }

        #region Methods (2)

        /// <summary>
        /// get the skin color based on the skin color number.
        /// </summary>
        /// <param name="selected">Is it selected?</param>
        /// <returns>The color valeau</returns>
        public Color GetObjColor(bool selected)
        {
            switch (this.notecolor)
            {
                case 0:
                    if (selected) 
                    {
                        return Color.Orange; 
                    }
                    else
                    { 
                        return Color.Gold; 
                    }

                case 1:
                    if (selected) 
                    {
                        return Color.DarkOrange; 
                    }
                    else
                    {
                        return Color.Orange; 
                    }

                case 2:
                    if (selected)
                    { 
                        return Color.Gray; 
                    }
                    else
                    { 
                        return Color.White; 
                    }

                case 3:
                    if (selected) 
                    {
                        return Color.Green;
                    }
                    else
                    {
                        return Color.LawnGreen;
                    }

                case 4:
                    if (selected)
                    {
                        return Color.Blue;
                    }
                    else
                    {
                        return Color.CornflowerBlue;
                    }

                case 5:
                    if (selected) 
                    {
                        return Color.Purple; 
                    }
                    else
                    {
                        return Color.Magenta;
                    }

                case 6:
                    if (selected) 
                    {
                        return Color.DarkRed;
                    }
                    else 
                    {
                        return Color.Red;
                    }

                default:
                    return Color.Gold;
            }
        }

        /// <summary>
        /// Get the skin color.
        /// </summary>
        /// <param name="selected">is it selected?</param>
        /// <param name="highlight">is it a highlight?</param>
        /// <param name="warn">is it a warning?</param>
        /// <returns>The color valeau</returns>
        public Color GetObjColor(bool selected, bool highlight, bool warn)
        {
            if (highlight)
            {
                return Color.LightYellow;
            }
            else if (warn)
            {
                if (this.notecolor == 6)
                {
                    return Color.Orange;
                }

                return Color.Red;
            }
            else
            {
                return this.GetObjColor(selected);
            }
        }

        /// <summary>
        /// Get the transparencylevel of the note.
        /// </summary>
        /// <returns>Transparencylevel as double</returns>
        public double GetTransparencylevel()
        {
            double transparecylevel = Convert.ToDouble(Settings.NotesTransparencyLevel) / 100;
            if (transparecylevel > 0 && transparecylevel <= 100)
            {
                return transparecylevel;
            }
            else
            {
                throw new CustomException("invalid transparencylevel");
            }
        }

        #endregion
    }
}