using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace NoteFly
{
    /// <summary>
    /// Skin data class.
    /// </summary>
    public class Skin
    {
        #region Fields (5) 

        public string Name;
        public Color PrimaryClr; //non nullable type.
        public Color SelectClr;
        public Color HighlightClr;
        public Color TextClr;

        #endregion Fields
    }
}
