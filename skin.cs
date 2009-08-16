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
using System.Drawing;

namespace SimplePlainNote
{
    /// <summary>
    /// Class that provide skin options to notes.
    /// </summary>
    class skin
    {
        #region Fields (1)

        private int notecolor = 0;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// itializes a new instance of the skin class.
        /// </summary>
        /// <param name="numcolor">color number</param>
        public skin(int numcolor)
        {
            this.notecolor = numcolor;
        }

        #endregion Constructors

        #region Methods (2)

        /// <summary>
        /// get the skin color based on the skin color number.
        /// </summary>
        /// <param name="selected">is it selected?</param>
        /// <returns>color</returns>
        public Color getObjColor(bool selected)
        {
            switch (this.notecolor)
            {
                case 0:
                    if (selected) { return Color.Orange; }
                    else { return Color.Gold; }
                case 1:
                    if (selected) { return Color.DarkOrange; }
                    else { return Color.Orange; }
                case 2:
                    if (selected) { return Color.Gray; }
                    else { return Color.White; }
                case 3:
                    if (selected) { return Color.Green; }
                    else { return Color.LawnGreen; }
                case 4:
                    if (selected) return Color.Blue;
                    else return Color.CornflowerBlue;
                case 5:
                    if (selected) { return Color.Purple; }
                    else { return Color.Magenta; }
                case 6:
                    if (selected) { return Color.DarkRed; }
                    else { return Color.Red; }
                default:
                    return Color.Gold;
            }
        }

        /// <summary>
        /// Get the skin color
        /// </summary>
        /// <param name="selected">is it selected?</param>
        /// <param name="highlight">is it a highlight?</param>
        /// <param name="warn">is it a warning?</param>
        /// <returns></returns>
        public Color getObjColor(bool selected, bool highlight, bool warn)
        {
            if (highlight)
            {
                return Color.LightYellow;
            }
            else if (warn)
            {
                return Color.Red;
            }
            else
            {
                return getObjColor(selected);
            }
        }

        #endregion
    }
}