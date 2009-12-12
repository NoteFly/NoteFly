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
using System.Drawing;

namespace NoteFly
{
    /// <summary>
    /// Class that provide skin options to notes.
    /// </summary>
    class Skin
    {
        #region Fields (1)

        private int notecolor = 0;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// itializes a new instance of the skin class.
        /// </summary>
        /// <param name="numcolor">color number</param>
        public Skin(int numcolor)
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
                if (this.notecolor == 6)
                {
                    return Color.Orange;
                }
                return Color.Red;
            }
            else
            {
                return getObjColor(selected);
            }
        }

        public double getTransparencylevel()
        {
            xmlHandler getSettings = new xmlHandler(true);
            double transparecylevel = Convert.ToDouble(getSettings.getXMLnodeAsInt("transparecylevel")) / 100;
            if (transparecylevel > 0 && transparecylevel <= 100)
            {
                return transparecylevel;
            }
            else
            {
                //error
                throw new CustomException("invalid transparencylevel");
            }
        }

        /// <summary>
        /// Get the font for the note.
        /// </summary>
        /// <returns>null if error.</returns>
        public Font getFontNoteContent()
        {
            xmlHandler getSettings = new xmlHandler(true);
            string fontname = getSettings.getXMLnode("fontcontent");
            
            foreach (FontFamily curfont in FontFamily.Families)
            {
                if (curfont.Name.ToString() == fontname)
                {
                    Font font = new Font(curfont, getFontNoteSize(getSettings));
                    return font;
                }
            }
            return null;
        }

        private float getFontNoteSize(xmlHandler getSettings)
        {
            getSettings = new xmlHandler(true);
            int fontsize = getSettings.getXMLnodeAsInt("fontsize");
            if ((fontsize > 1) && (fontsize <= 96))
            {
                return fontsize;
            }
            else
            {
                return 10;
            }
        }

        #endregion
    }
}