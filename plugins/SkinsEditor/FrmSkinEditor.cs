﻿//-----------------------------------------------------------------------
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
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    /// <summary>
    /// Skin editor form
    /// </summary>
    public sealed partial class FrmSkinEditor : Form
    {
        /// <summary>
        /// The skin position that is currently being edited.
        /// </summary>
        private int editskinnr = -1;

        private int deleteskinnr = -1;

        /// <summary>
        /// The action that skin editor is performing
        /// </summary>
        private skineditormode skinaction;

        /// <summary>
        /// The interface to let this plugin talk back to NoteFly.
        /// </summary>
        private IPlugin.IPluginHost host;

        /// <summary>
        /// Creating a new instance of the FrmSkinsEditor.
        /// </summary>
        /// <param name="host">The interface to talk let this plugin talk to NoteFly.</param>
        public FrmSkinEditor(IPlugin.IPluginHost host)
        {
            this.InitializeComponent();
            this.host = host;
            this.skinaction = skineditormode.browseskins;
            this.LoadAllSkinNames();
        }

        /// <summary>
        /// The current mode of the skin editor.
        /// </summary>
        private enum skineditormode
        {
            /// <summary>
            /// Selecting/viewing a skin.
            /// </summary>
            browseskins,

            /// <summary>
            /// A skin is being edited.
            /// </summary>
            editskin,

            /// <summary>
            /// A new skin is created.
            /// </summary>
            newskin
        }

        /// <summary>
        /// Load all skin names into lbxSkins.
        /// </summary>
        private void LoadAllSkinNames()
        {
            this.btnDeleteSkin.Enabled = false;
            this.lbxSkins.Items.Clear();
            this.lbxSkins.Items.AddRange(this.host.GetSkinsNames());
            if (this.lbxSkins.Items.Count > 1)
            {
                this.btnDeleteSkin.Enabled = true;
            }
        }

        /// <summary>
        /// Closed the skin editor form.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Set a textbox with the selected color from the colordialog.
        /// </summary>
        /// <param name="tb">The TextBox</param>
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
        /// <param name="clr">A color object.</param>
        /// <returns>A HTML hex color as string.</returns>
        private string ClrToHtmlHexClr(Color clr)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}", clr.R, clr.G, clr.B);
        }

        /// <summary>
        /// Select primary color.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
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
        /// Fill in fields by from a skin from selected skin position.
        /// </summary>
        /// <param name="skinnr">The skin position</param>
        private void SetFieldBySelectSkin(int skinnr)
        {
            this.tbSkinName.Text = this.host.GetSkinName(skinnr);
            this.tbPrimaryColor.Text = this.ClrToHtmlHexClr(this.host.GetPrimaryClr(skinnr));
            this.tbSelectingColor.Text = this.ClrToHtmlHexClr(this.host.GetSelectClr(skinnr));
            this.tbHighlightingColor.Text = this.ClrToHtmlHexClr(this.host.GetHighlightClr(skinnr));
            this.tbTextColor.Text = this.ClrToHtmlHexClr(this.host.GetTextClr(skinnr));
            this.tbPrimaryTexture.Text = this.host.GetPrimaryTextureFile(skinnr);
            ImageLayout imglayout = this.host.GetPrimaryTextureLayout(skinnr);
            switch (imglayout)
            {
                case ImageLayout.Tile:
                    this.cbxPrimaryTextureLayout.SelectedIndex = 0;
                    break;
                case ImageLayout.Center:
                    this.cbxPrimaryTextureLayout.SelectedIndex = 1;
                    break;
                case ImageLayout.Stretch:
                    this.cbxPrimaryTextureLayout.SelectedIndex = 2;
                    break;
                default:
                    this.cbxPrimaryTextureLayout.SelectedIndex = 0;
                    break;
            }
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
            this.tbPrimaryTexture.Enabled = enabled;
            this.cbxPrimaryTextureLayout.Enabled = enabled;
            this.btnBrowsePrimaryTexture.Enabled = enabled;
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
            this.tbPrimaryTexture.Clear();
            this.cbxPrimaryTextureLayout.SelectedIndex = -1;
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
                this.editskinnr = this.lbxSkins.SelectedIndex;
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
                    if (!string.IsNullOrEmpty(this.tbPrimaryTexture.Text))
                    {
                        if (this.cbxPrimaryTextureLayout.SelectedIndex < 0)
                        {
                            this.lblTextPrimartTextureLayout.ForeColor = Color.DarkRed;
                            this.cbxPrimaryTextureLayout.ForeColor = Color.DarkRed;
                            return;
                        }
                    }

                    this.lblTextPrimartTextureLayout.ForeColor = SystemColors.WindowText;
                    this.cbxPrimaryTextureLayout.ForeColor = SystemColors.WindowText;
                    if (this.CheckAllTbColorsAndSetErrors())
                    {
                        if (!this.WriteSkinsFile(this.tbSkinName.Text, this.tbPrimaryTexture.Text, this.tbPrimaryColor.Text, this.tbSelectingColor.Text, this.tbHighlightingColor.Text, this.tbTextColor.Text))
                        {
                            const string COULDNOTWRITESKINS = "Could not write skins file.";
                            MessageBox.Show(COULDNOTWRITESKINS);
                        }

                        this.host.ReloadAllSkins();
                        this.host.UpdateAllNoteForms();

                        if (this.skinaction == skineditormode.newskin)
                        {
                            this.lbxSkins.Items.Add(this.tbSkinName.Text);
                            this.btnNewSkin.Text = "&new skin";
                        }
                        else if (this.skinaction == skineditormode.editskin)
                        {
                            this.btnEditskin.Text = "&edit skin";
                        }

                        this.skinaction = skineditormode.browseskins;
                        this.lbxSkins_SelectedIndexChanged(null, null);
                        this.LoadAllSkinNames();
                        this.ClearFields();
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
        /// <returns>true if writing skins file was succesfull.</returns>
        private bool WriteSkinsFile(string skinname, string skinprimarytexture, string skinprimary, string skinselect, string skinhighlight, string skintext)
        {
            bool succeed = false;
            string skinsfilepath = this.host.GetSkinsFile();
            XmlTextWriter xmlwriter = null;
            try
            {
                xmlwriter = new XmlTextWriter(skinsfilepath, Encoding.UTF8);
                xmlwriter.Formatting = Formatting.Indented;
                xmlwriter.WriteStartDocument(true);
                xmlwriter.WriteStartElement("skins");

                for (int i = 0; i < this.host.CountSkins; i++)
                {
                    if (this.skinaction == skineditormode.editskin && this.editskinnr == i)
                    {
                        ImageLayout newimglayout = ImageLayout.Tile;
                        switch (this.cbxPrimaryTextureLayout.SelectedIndex)
                        {
                            // 0 is tiled
                            case 1:
                                newimglayout = ImageLayout.Center;
                                break;
                            case 2:
                                newimglayout = ImageLayout.Stretch;
                                break;
                            // unknown is tiled
                        }

                        this.WriteSkinBody(xmlwriter, skinname, skinprimary, skinprimarytexture, newimglayout, skinselect, skinhighlight, skintext);
                    }
                    else if (i != this.deleteskinnr)
                    {
                        string name = this.host.GetSkinName(i);
                        string primaryclr = this.ClrToHtmlHexClr(this.host.GetPrimaryClr(i));
                        string primarytexture = this.host.GetPrimaryTextureFile(i);
                        ImageLayout primarytexturelayout = this.host.GetPrimaryTextureLayout(i);
                        string selectclr = this.ClrToHtmlHexClr(this.host.GetSelectClr(i));
                        string highlightclr = this.ClrToHtmlHexClr(this.host.GetHighlightClr(i));
                        string textclr = this.ClrToHtmlHexClr(this.host.GetTextClr(i));
                        this.WriteSkinBody(xmlwriter, name, primaryclr, primarytexture, primarytexturelayout, selectclr, highlightclr, textclr);
                    }
                }

                if (this.skinaction == skineditormode.newskin)
                {
                    // write new skin
                    this.WriteSkinBody(xmlwriter, skinname, skinprimary, skinprimarytexture, ImageLayout.Tile, skinselect, skinhighlight, skintext);
                }

                xmlwriter.WriteEndElement();
                xmlwriter.WriteEndDocument();
                succeed = true;
            }
            finally
            {
                if (xmlwriter != null)
                {
                    xmlwriter.Close();
                }
            }

            return succeed;
        }

        /// <summary>
        /// Write a skin xml element
        /// </summary>
        /// <param name="xmlwriter">The xmlwriter</param>
        /// <param name="name">The name of the skin</param>
        /// <param name="primaryclr">The primary color of the skin.</param>
        /// <param name="primarytexture">The primary texture of the skin.</param>
        /// <param name="primarytexturelayout">The layout of the primary texture of the skin.</param>
        /// <param name="selectclr">The select color of the skin.</param>
        /// <param name="highlightclr">The highlight color of the skin.</param>
        /// <param name="textclr">The text color of the skin.</param>
        private void WriteSkinBody(XmlWriter xmlwriter, string name, string primaryclr, string primarytexture, ImageLayout primarytexturelayout, string selectclr, string highlightclr, string textclr)
        {
            xmlwriter.WriteStartElement("skin");
            xmlwriter.WriteElementString("Name", name);
            xmlwriter.WriteStartElement("PrimaryClr");
            if (!string.IsNullOrEmpty(primarytexture))
            {
                xmlwriter.WriteAttributeString("texture", primarytexture);
                switch (primarytexturelayout)
                {
                    case ImageLayout.Tile:
                        xmlwriter.WriteAttributeString("texturelayout", "tile");
                        break;
                    case ImageLayout.Stretch:
                        xmlwriter.WriteAttributeString("texturelayout", "stretch");
                        break;
                    case ImageLayout.Center:
                        xmlwriter.WriteAttributeString("texturelayout", "center");
                        break;
                    default:
                        xmlwriter.WriteAttributeString("texturelayout", "tile");
                        break;
                }
            }

            xmlwriter.WriteString(primaryclr);
            xmlwriter.WriteEndElement();
            xmlwriter.WriteElementString("SelectClr", selectclr);
            xmlwriter.WriteElementString("HighlightClr", highlightclr);
            xmlwriter.WriteElementString("TextClr", textclr);
            xmlwriter.WriteEndElement();
        }

        /// <summary>
        /// Check all fields for proper html hex color if not highlight the error(s).
        /// </summary>
        /// <returns>True if all textboxs are valid html hex colors</returns>
        private bool CheckAllTbColorsAndSetErrors()
        {
            bool allproper = true;
            allproper = this.CheckProperHTMLColorTbSetErrors(this.tbPrimaryColor, allproper);
            allproper = this.CheckProperHTMLColorTbSetErrors(this.tbSelectingColor, allproper);
            allproper = this.CheckProperHTMLColorTbSetErrors(this.tbHighlightingColor, allproper);
            allproper = this.CheckProperHTMLColorTbSetErrors(this.tbTextColor, allproper);
            return allproper;
        }

        /// <summary>
        /// Check if proper skinname in textbox.
        /// Skin name should be at least 1 character long and not contain forbidden xml characters.
        /// </summary>
        /// <returns>True if the skin name is valid</returns>
        private bool CheckProperSkinnameTb()
        {
            if (this.tbSkinName.TextLength < 1 || this.tbSkinName.Text.Contains("<") || this.tbSkinName.Text.Contains(">") || this.tbSkinName.Text.Contains("/"))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if proper html hex color in textbox, 
        /// if not make textbox highlight that their is an error.
        /// </summary>
        /// <param name="tb">The TextBox to check.</param>
        /// <param name="allproper">Are previous textboxes valid.</param>
        /// <returns>True if textbox is valid HTML hex.</returns>
        private bool CheckProperHTMLColorTbSetErrors(TextBox tb, bool allproper)
        {
            if (!this.CheckProperHTMLColorTb(tb))
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
        /// Check if their is a proper HTML hex color in a textbox.
        /// </summary>
        /// <param name="tb">The textbox to check.</param>
        /// <returns>True if textbox contains a proper HTML hex color.</returns>
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
        /// Parser the textbox HTML hex color content as a color object and set
        /// panels right as preview of the color.
        /// </summary>
        /// <param name="sender">Sender object, should be a textbox</param>
        /// <param name="e">Event arguments</param>
        private void ParserAsPreviewColor(object sender, EventArgs e)
        {
            TextBox tbclr = (TextBox)sender;
            if (this.CheckProperHTMLColorTb(tbclr))
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

        /// <summary>
        /// Selecting a primary texture file.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnBrowsePrimaryTexture_Click(object sender, EventArgs e)
        {
            if (this.openFileTextureDialog.ShowDialog() == DialogResult.OK)
            {
                FileInfo fitexture = new FileInfo(this.openFileTextureDialog.FileName);
                const int NUMKBWARN = 100;
                if (fitexture.Length > (1024 * NUMKBWARN))
                {
                    const string ATEXTURELARGER = "A texture larger than ";
                    const string NOTRECOMMENDEDPERFORMANCE = " KiloBytes is not recommended for performance reasons.";
                    const string PERFORMANCEWARNING = "Performance warning";
                    MessageBox.Show(ATEXTURELARGER + NUMKBWARN + NOTRECOMMENDEDPERFORMANCE, PERFORMANCEWARNING);
                }

                this.tbPrimaryTexture.Text = this.openFileTextureDialog.FileName;
            }
        }

        /// <summary>
        /// Delete a skin
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnDeleteSkin_Click(object sender, EventArgs e)
        {
            if (this.lbxSkins.SelectedIndex >= 0)
            {
                if (this.lbxSkins.Items.Count > 1)
                {
                    this.btnDeleteSkin.Enabled = true;
                    DialogResult res = MessageBox.Show("Do you want to delete this skin?", "delete skin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        this.skinaction = skineditormode.browseskins;
                        this.deleteskinnr = this.lbxSkins.SelectedIndex;
                        this.WriteSkinsFile(null, null, null, null, null, null);
                        this.lbxSkins.Items.RemoveAt(this.deleteskinnr);
                        this.deleteskinnr = -1;
                        this.host.ReloadAllSkins();
                    }

                    if (this.lbxSkins.Items.Count <= 1)
                    {
                        this.btnDeleteSkin.Enabled = false;
                    }
                }
                else
                {
                    this.btnDeleteSkin.Enabled = false;
                }
            }
        }
    }
}
