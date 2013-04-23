//-----------------------------------------------------------------------
// <copyright file="SkinFactory.cs" company="NoteFly">
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
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    public sealed class SkinFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="skinnr"></param>
        public static Skin GetSkin(IPlugin.IPluginHost host, int skinnr)
        {
            Skin skin = new Skin();
            skin.Name = host.GetSkinName(skinnr);
            skin.PrimaryClr = host.GetPrimaryClr(skinnr);
            skin.SelectClr = host.GetSelectClr(skinnr);
            skin.HighlightClr = host.GetHighlightClr(skinnr);
            skin.TextClr = host.GetTextClr(skinnr);
            skin.PrimaryTexture = host.GetPrimaryTextureFile(skinnr);
            skin.PrimaryTextureLayout = host.GetPrimaryTextureLayout(skinnr);
            return skin;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Skin CreateDefaultNewSkin()
        {
            Skin defaultskin = new Skin();
            defaultskin.Name = string.Empty;
            defaultskin.PrimaryClr = Color.White;
            defaultskin.SelectClr = Color.Gray;
            defaultskin.HighlightClr = Color.WhiteSmoke;
            defaultskin.TextClr = Color.Black;
            return defaultskin;
        }

        /// <summary>
        /// Convert color object to HTML hex color string.
        /// </summary>
        /// <param name="clr">A color object.</param>
        /// <returns>A HTML hex color as string.</returns>
        public static string ClrToHtmlHexClr(Color clr)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}", clr.R, clr.G, clr.B);
        }
    }
}
