//-----------------------------------------------------------------------
// <copyright file="FrmSkinEditor.cs" company="NoteFly">
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
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;
    using System.IO;

    public sealed partial class FrmSkinEditor : Form
    {
        private skineditormode skinaction;
        private IPlugin.IPluginHost host;

        /// <summary>
        /// Creating a new instance of the FrmSkinsEditor.
        /// </summary>
        /// <param name="host"></param>
        public FrmSkinEditor(IPlugin.IPluginHost host)
        {
            InitializeComponent();
            this.host = host;
            this.skinaction = skineditormode.browseskins;
            this.lbxSkins.Items.AddRange(this.host.GetSkinsNames());
        }

        private enum skineditormode { browseskins, editskin, newskin };

        /// <summary>
        /// Closed the skin editor form.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Set a textbox with the selected color from the colordialog.
        /// </summary>
        /// <param name="tb"></param>
        private void SetTbHexcolor(TextBox tb)
        {
            DialogResult dlgres = this.colordlg.ShowDialog();
            if (dlgres == DialogResult.OK)
            {
                tb.Text = this.ClrToHtmlHexClr(this.colordlg.Color);
            }
        }

        /// <summary>
        /// Convert color object to HTML hex color.
        /// </summary>
        /// <param name="clr"></param>
        /// <returns></returns>
        private string ClrToHtmlHexClr(Color clr)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}", clr.R, clr.G, clr.B);
        }

        /// <summary>
        /// Select primary color.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e"></param>
        private void btnPickPrimaryColor_Click(object sender, EventArgs e)
        {
            this.SetTbHexcolor(this.tbPrimaryColor);
        }

        /// <summary>
        /// Select selecting color.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnSelectingColor_Click(object sender, EventArgs e)
        {
            this.SetTbHexcolor(this.tbSelectingColor);
        }

        /// <summary>
        /// Select highlight color.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnHighlightColor_Click(object sender, EventArgs e)
        {
            this.SetTbHexcolor(this.tbHighlightingColor);
        }

        /// <summary>
        /// Select text color.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnTextColor_Click(object sender, EventArgs e)
        {
            this.SetTbHexcolor(this.tbTextColor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skinnr">The skin position</param>
        private void SetFieldBySelectSkin(int skinnr)
        {
            this.tbSkinName.Text = this.host.GetSkinName(skinnr);
            this.tbPrimaryColor.Text = this.ClrToHtmlHexClr(this.host.GetPrimaryClr(skinnr));
            this.tbSelectingColor.Text = this.ClrToHtmlHexClr(this.host.GetSelectClr(skinnr));
            this.tbHighlightingColor.Text = this.ClrToHtmlHexClr(this.host.GetHighlightClr(skinnr));
            this.tbTextColor.Text = this.ClrToHtmlHexClr(this.host.GetTextClr(skinnr));
        }

        /// <summary>
        /// Skin in skin list selected.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void lbxSkins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lbxSkins.SelectedIndex >= 0)
            {
                this.skinaction = skineditormode.browseskins;
                this.btnEditskin.Text = "&edit skins";
                this.btnNewSkin.Text = "&new skin";
                this.btnEditskin.Enabled = true;
                this.btnNewSkin.Enabled = true;
                this.SetFieldsEnabled(false);
                this.SetFieldBySelectSkin(this.lbxSkins.SelectedIndex);
            }
            else
            {
                this.btnEditskin.Enabled = false;
            }
        }

        /// <summary>
        /// Set field enabled or disabled them.
        /// </summary>
        /// <param name="enabled">Set all fields enabled or disabled</param>
        private void SetFieldsEnabled(bool enabled)
        {
            this.tbSkinName.Enabled = enabled;
            this.tbPrimaryColor.Enabled = enabled;
            this.tbSelectingColor.Enabled = enabled;
            this.tbHighlightingColor.Enabled = enabled;
            this.tbTextColor.Enabled = enabled;

            this.btnPickPrimaryColor.Enabled = enabled;
            this.btnPickSelectingColor.Enabled = enabled;
            this.btnPickHighlightColor.Enabled = enabled;
            this.btnPickTextColor.Enabled = enabled;

            this.btnSaveSkin.Enabled = enabled;
        }

        /// <summary>
        /// Clear all fields content.
        /// </summary>
        private void ClearFields()
        {
            this.tbSkinName.Clear();
            this.tbPrimaryColor.Clear();
            this.tbSelectingColor.Clear();
            this.tbHighlightingColor.Clear();
            this.tbTextColor.Clear();

            this.pnlClrPrimary.BackColor = Color.White;
            this.pnlClrSelecting.BackColor = Color.White;
            this.pnlClrHighlight.BackColor = Color.White;
            this.pnlClrText.BackColor = Color.White;
        }

        /// <summary>
        /// Set all fields background back to normal (no error).
        /// </summary>
        private void SetAllFieldBackgroundNormal()
        {
            this.tbPrimaryColor.BackColor = SystemColors.Window;
            this.tbSelectingColor.BackColor = SystemColors.Window;
            this.tbHighlightingColor.BackColor = SystemColors.Window;
            this.tbTextColor.BackColor = SystemColors.Window;
        }

        /// <summary>
        /// Requested to edit a skin
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnEditskin_Click(object sender, EventArgs e)
        {
            this.SetFieldsEnabled(true);
            if (this.skinaction == skineditormode.editskin)
            {
                this.SetFieldsEnabled(false);
                this.btnEditskin.Text = "&edit skin";
                this.SetAllFieldBackgroundNormal();
                this.skinaction = skineditormode.browseskins;
            }
            else
            {
                this.btnEditskin.Text = "cancel &edit skin";
                this.skinaction = skineditormode.editskin;
            }
        }

        /// <summary>
        /// requested to create new skin.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnNewSkin_Click(object sender, EventArgs e)
        {
            if (this.skinaction == skineditormode.newskin)
            {
                this.btnEditskin.Enabled = true;
                this.btnNewSkin.Text = "&new skin";
                this.SetFieldsEnabled(false);
                this.ClearFields();
                this.SetAllFieldBackgroundNormal();
                this.skinaction = skineditormode.browseskins;
                this.lbxSkins_SelectedIndexChanged(null, null);
            }
            else
            {
                this.lbxSkins.ClearSelected();
                this.btnEditskin.Enabled = false;
                this.btnEditskin.Text = "&edit skin";
                this.btnNewSkin.Text = "cancel &new skin";
                this.SetFieldsEnabled(true);
                this.ClearFields();
                this.skinaction = skineditormode.newskin;
            }
        }

        /// <summary>
        /// Request to save skin
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnSaveSkin_Click(object sender, EventArgs e)
        {
            if (this.skinaction == skineditormode.newskin || this.skinaction == skineditormode.editskin)
            {
                if (this.CheckProperSkinnameTb())
                {
                    this.tbSkinName.BackColor = SystemColors.Window;
                    if (this.CheckAllTbColorsAndSetErrors())
                    {

                        if (this.skinaction == skineditormode.newskin)
                        {
                            // TODO add new skin.
                            this.lbxSkins.Items.Add(this.tbSkinName.Text);
                            this.btnNewSkin.Text = "&new skin";
                        }

                        this.skinaction = skineditormode.browseskins;
                        this.lbxSkins_SelectedIndexChanged(null, null);
                    }
                }
                else
                {
                    this.tbSkinName.BackColor = Color.Red;
                }
            }
        }

        /// <summary>
        /// Writes the skins file to disk to with all NoteFly skins.
        /// </summary>
        /// <returns></returns>
        private bool WriteSkinsFile()
        {
            bool succeed = false;
            string skinsfilepath = this.host.GetSkinsFile();
            XmlTextWriter xmlwrite = null;
            try
            {
                xmlwrite = new XmlTextWriter(skinsfilepath, Encoding.UTF8);
                xmlwrite.Formatting = Formatting.Indented;
                xmlwrite.WriteStartDocument(true);
                xmlwrite.WriteStartElement("skins");
                xmlwrite.WriteStartElement("skin");
                for (int i = 0; i < this.host.CountNotes; i++)
                {
                    xmlwrite.WriteElementString("Name", this.host.GetSkinName(i));
                    xmlwrite.WriteStartElement("PrimaryClr");                    
                    if (!string.IsNullOrEmpty(this.host.GetPrimaryTextureFile(i)))
                    {
                        xmlwrite.WriteAttributeString("texture", this.host.GetPrimaryTextureFile(i));
                        ImageLayout layout = this.host.GetPrimaryTextureLayout(i);
                        switch (layout)
                        {
                            case ImageLayout.Tile:
                                xmlwrite.WriteAttributeString("texturelayout", "tile");
                                break;
                            case ImageLayout.Stretch:
                                xmlwrite.WriteAttributeString("texturelayout", "stretch");
                                break;
                            case ImageLayout.Center:
                                xmlwrite.WriteAttributeString("texturelayout", "center");
                                break; 
                            default:
                                xmlwrite.WriteAttributeString("texturelayout", "tile");
                                break;
                        }
                    }

                    xmlwrite.WriteString(this.ClrToHtmlHexClr(this.host.GetPrimaryClr(i)));
                    xmlwrite.WriteEndElement();
                    xmlwrite.WriteElementString("SelectClr", this.ClrToHtmlHexClr(this.host.GetSelectClr(i)));
                    xmlwrite.WriteElementString("HighlightClr", this.ClrToHtmlHexClr(this.host.GetHighlightClr(i)));
                    xmlwrite.WriteElementString("TextClr", this.ClrToHtmlHexClr(this.host.GetTextClr(i)));
                }

                xmlwrite.WriteEndElement();
                xmlwrite.WriteEndDocument();
                succeed = true;
            }
            finally
            {
                if (xmlwrite != null)
                {
                    xmlwrite.Close();
                }
            }

            return succeed;
        }

        /// <summary>
        /// Check all fields for proper html hex color if not highlight the error(s).
        /// </summary>
        /// <returns></returns>
        private bool CheckAllTbColorsAndSetErrors()
        {
            bool allproper = true;
            allproper = CheckProperHTMLColorTbSetErrors(this.tbPrimaryColor, allproper);
            allproper = CheckProperHTMLColorTbSetErrors(this.tbSelectingColor, allproper);
            allproper = CheckProperHTMLColorTbSetErrors(this.tbHighlightingColor, allproper);
            allproper = CheckProperHTMLColorTbSetErrors(this.tbTextColor, allproper);
            return allproper;
        }

        /// <summary>
        /// Check if proper skinname in textbox.
        /// </summary>
        /// <returns></returns>
        private bool CheckProperSkinnameTb()
        {
            if (this.tbSkinName.TextLength < 1)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if proper html hex color in textbox, 
        /// if not make textbox highlight that their is an error.
        /// </summary>
        /// <param name="tb"></param>
        /// <param name="allproper"></param>
        /// <returns></returns>
        private bool CheckProperHTMLColorTbSetErrors(TextBox tb, bool allproper)
        {
            if (!CheckProperHTMLColorTb(tb))
            {
                tb.BackColor = Color.Red;
                allproper = false;
            }
            else
            {
                tb.BackColor = SystemColors.Window;
            }

            return allproper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        private bool CheckProperHTMLColorTb(TextBox tb)
        {
            if (tb.TextLength == 7)
            {
                if (tb.Text[0] == '#')
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e"></param>
        private void ParserAsPreviewColor(object sender, EventArgs e)
        {
            TextBox tbclr = (TextBox)sender;
            if (CheckProperHTMLColorTb(tbclr))
            {
                int what = Convert.ToInt32(tbclr.Tag);
                Color clr = System.Drawing.ColorTranslator.FromHtml(tbclr.Text);
                switch (what)
                {
                    case 1:
                        this.pnlClrPrimary.BackColor = clr;
                        break;
                    case 2:
                        this.pnlClrSelecting.BackColor = clr;
                        break;
                    case 3:
                        this.pnlClrHighlight.BackColor = clr;
                        break;
                    case 4:
                        this.pnlClrText.BackColor = clr;
                        break;
                }
            }
        }

    }
}
