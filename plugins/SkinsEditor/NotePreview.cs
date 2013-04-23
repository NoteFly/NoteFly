//-----------------------------------------------------------------------
// <copyright file="NotePreview.cs" company="NoteFly">
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
using System.Windows.Forms;
using System.Drawing;

namespace SkinsEditor
{
    /// <summary>
    /// NoteSkinPreview control
    /// </summary>
    public partial class NoteSkinPreview : UserControl
    {
        /// <summary>
        /// The interface to let this plugin talk back to NoteFly.
        /// </summary>
        private IPlugin.IPluginHost host;

        /// <summary>
        /// 
        /// </summary>
        public NoteSkinPreview()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        public IPlugin.IPluginHost Host
        {
            set 
            {
                this.host = value;
            }
        }

        /// <summary>
        /// Show a preview of a note with a skin given by the skinnr.
        /// </summary>
        public void DrawNoteSkinPreview(Skin skin)
        {
            FontStyle titlestyle = FontStyle.Regular;
            if (this.host.GetSettingBool("FontTitleStylebold"))
            {
                titlestyle = FontStyle.Bold;
            }

            this.lblPreviewNoteTitle.ForeColor = skin.TextClr;
            this.lblPreviewNoteTitle.Font = new Font(this.host.GetSettingString("FontTitleFamily"), this.host.GetSettingFloat("FontTitleSize"), titlestyle);
            this.lblPreviewNoteContent.ForeColor = skin.TextClr;
            this.lblPreviewNoteContent.Font = new Font(this.host.GetSettingString("FontContentFamily"), this.host.GetSettingFloat("FontContentSize"));
            if (!string.IsNullOrEmpty(skin.PrimaryTexture))
            {
                this.pnlPreviewNoteWindow.BackgroundImage = Image.FromFile(skin.PrimaryTexture);
                this.pnlPreviewNoteWindow.BackgroundImageLayout = skin.PrimaryTextureLayout;
                this.pnlPreviewNoteHead.BackColor = Color.Transparent;
                this.pnlPreviewNoteContent.BackColor = Color.Transparent;
            }
            else
            {
                this.pnlPreviewNoteWindow.BackgroundImage = null;
                this.pnlPreviewNoteHead.BackColor = skin.PrimaryClr;
                this.pnlPreviewNoteContent.BackColor = skin.PrimaryClr;
            }

            if (this.lblPreviewNoteTitle.Height + this.lblPreviewNoteTitle.Location.Y >= this.pnlPreviewNoteHead.Height)
            {
                if (this.lblPreviewNoteTitle.Height < this.host.GetSettingInt("NotesTitlepanelMaxHeight"))
                {
                    this.pnlPreviewNoteHead.Height = this.lblPreviewNoteTitle.Height;
                }
                else
                {
                    this.pnlPreviewNoteHead.Height = this.host.GetSettingInt("NotesTitlepanelMaxHeight");
                }
            }
            else
            {
                this.pnlPreviewNoteHead.Height = this.host.GetSettingInt("NotesTitlepanelMinHeight");
            }

            this.pnlPreviewNoteContent.Location = new Point(0, this.pnlPreviewNoteHead.Height);
            this.pnlPreviewNoteContent.Size = new Size(200, 172 - this.pnlPreviewNoteHead.Height);
            this.gbxPreviewNote.Visible = true;
        }
    }
}