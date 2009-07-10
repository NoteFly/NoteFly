using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SimplePlainNote
{
    class skin
    {
        private int notecolor = 0;

        #region constructor
        public skin(int numcolor)
        {
            this.notecolor = numcolor;
        }
        #endregion


        public Color getObjColor(bool selected)
        {
            switch (this.notecolor)
            {
                case 0:
                    if (selected) return Color.Orange;
                    else return Color.Gold;
                case 1:
                    if (selected) return Color.DarkOrange;
                    else return Color.Orange;
                case 2:
                    if (selected) return Color.Gray;
                    else return Color.White;
                case 3:
                    if (selected) return Color.Green;
                    else return Color.LightGreen;
                case 4:
                    if (selected) return Color.Blue;
                    else return Color.CornflowerBlue;
                case 5:
                    if (selected) return Color.Purple;
                    else return Color.Magenta;
                case 6:
                    if (selected) return Color.DarkRed;
                    else return Color.Red;
                default:
                    return Color.Gold;
            }
        }

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
    }
}
