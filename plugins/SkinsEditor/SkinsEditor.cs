//-----------------------------------------------------------------------
// <copyright file="SkinsEditor.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2011  Tom
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
    using System.Text;
    using System.Windows.Forms;

    public class SkinsEditor : IPlugin.PluginBase
    {
        /// <summary>
        /// Create the ToolStripMenuItem for the skin editor.
        /// </summary>
        /// <returns></returns>
        public override ToolStripItem InitTrayIconMenu()
        {
            ToolStripItem menutrayicon = new ToolStripMenuItem("Skin editor");
            menutrayicon.Name = "menuSkinEditor";
            menutrayicon.Click += new EventHandler(menutrayicon_Click);
            return menutrayicon;
        }

        /// <summary>
        /// Open skin editor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menutrayicon_Click(object sender, EventArgs e)
        {
            FrmSkinEditor skineditor = new FrmSkinEditor(this.Host);
            skineditor.Show();
        }


    }
}
