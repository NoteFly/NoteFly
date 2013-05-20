//-----------------------------------------------------------------------
// <copyright file="SkinsEditor.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2011-2013  Tom
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
    using System.Windows.Forms;

    /// <summary>
    /// Skin Editor plugin
    /// </summary>
    public class SkinsEditor : IPlugin.PluginBase
    {
        /// <summary>
        /// Reference to the skineditor form.
        /// </summary>
        private FrmSkinsEditor skineditor;

        /// <summary>
        /// Create the ToolStripMenuItem for the skin editor.
        /// </summary>
        /// <returns>A toolstripitem</returns>
        public override ToolStripItem InitTrayIconMenu()
        {
            ToolStripItem menutrayicon = new ToolStripMenuItem();
            menutrayicon.Text = "Skins Editor";
            menutrayicon.Name = "menuSkinsEditor";
            menutrayicon.Click += new EventHandler(this.menutrayicon_Click);
            return menutrayicon;
        }

        /// <summary>
        /// Open skin editor.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void menutrayicon_Click(object sender, EventArgs e)
        {
            if (this.skineditor == null || this.skineditor.IsDisposed)
            {
                this.skineditor = new FrmSkinsEditor(this.Host);
            }
            else
            {
                this.skineditor.BringToFront();
            }

            this.skineditor.Show();
        }
    }
}