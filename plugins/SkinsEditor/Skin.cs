//-----------------------------------------------------------------------
// <copyright file="Skin.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2013  Tom
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
namespace SkinsEditor
{
    using System.Drawing;

    /// <summary>
    /// Skin data class.
    /// </summary>
    public sealed class Skin
    {
        #region Fields (5) 

        /// <summary>
        /// The highlight color of the skin.
        /// </summary>
        private Color highlightclr;

        /// <summary>
        /// The name of the skin.
        /// </summary>
        private string name;

        /// <summary>
        /// The primary color of the skin.
        /// </summary>
        private Color primaryclr;

        /// <summary>
        /// Backgound image of the skin.
        /// </summary>
        private string primarytexture = null;

        /// <summary>
        /// Backgound image layout of the skin
        /// </summary>
        private System.Windows.Forms.ImageLayout primarytexturelayout = System.Windows.Forms.ImageLayout.Tile;

        /// <summary>
        /// The selection color of the skin.
        /// </summary>
        private Color selectclr;

        /// <summary>
        /// The text color of the skin.
        /// </summary>
        private Color textclr;

        #endregion Fields 

        #region Properties (5) 

        /// <summary>
        /// Gets or sets the highlight color of the skin.
        /// </summary>
        public Color HighlightClr
        {
            get
            {
                return this.highlightclr;
            }

            set
            {
                this.highlightclr = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the skin.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// Gets or sets the primary color of the skin.
        /// </summary>
        public Color PrimaryClr
        {
            get
            {
                return this.primaryclr;
            }

            set
            {
                this.primaryclr = value;
            }
        }

        /// <summary>
        /// Gets or sets the background image of the skin.
        /// </summary>
        public string PrimaryTexture
        {
            get
            {
                return this.primarytexture;
            }

            set
            {
                this.primarytexture = value;
            }
        }

        /// <summary>
        /// Gets or sets the background image layout of the skin.
        /// </summary>
        public System.Windows.Forms.ImageLayout PrimaryTextureLayout
        {
            get
            {
                return this.primarytexturelayout;
            }

            set
            {
                this.primarytexturelayout = value;
            }
        }

        /// <summary>
        /// Gets or sets the selection color of the skin.
        /// </summary>
        public Color SelectClr
        {
            get
            {
                return this.selectclr;
            }

            set
            {
                this.selectclr = value;
            }
        }

        /// <summary>
        /// Gets or sets the text color of the skin.
        /// </summary>
        public Color TextClr
        {
            get
            {
                return this.textclr;
            }

            set
            {
                this.textclr = value;
            }
        }

        #endregion Properties 
    }
}
