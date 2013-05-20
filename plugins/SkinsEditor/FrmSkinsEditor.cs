//-----------------------------------------------------------------------
// <copyright file="FrmSkinEditor.cs" company="NoteFly">
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
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// Skins editor form
    /// </summary>
    public sealed partial class FrmSkinsEditor : Form
    {
        /// <summary>
        /// The skin position that is currently being edited.
        /// </summary>
        private int editskinnr = -1;

        /// <summary>
        /// The current skin being created or edited.
        /// </summary>
        private Skin skin;

        /// <summary>
        /// The action that skins editor is performing
        /// </summary>
        private skineditormode skinaction;

        /// <summary>
        /// The interface to let this plugin talk back to NoteFly.
        /// </summary>
        private IPlugin.IPluginHost host;

        /// <summary>
        /// Delta point
        /// </summary>
        private Point oldp;

        /// <summary>
        /// Creating a new instance of the FrmSkinsEditor.
        /// </summary>
        /// <param name="host">The interface to talk let this plugin talk to NoteFly.</param>
        public FrmSkinsEditor(IPlugin.IPluginHost host)
        {
            this.host = host;
            this.skinaction = skineditormode.browseskins;
            this.InitializeComponent();
            this.SetSkin(this.host.GetSettingInt("ManagenotesSkinnr"));
            this.notePreview1.Host = this.host;
            this.LoadAllSkinNames();            
        }

        /// <summary>
        /// Set the skin for the FrmSkinEditor form.
        /// </summary>
        /// <param name="skinnr">The skin number position.</param>
        private void SetSkin(int skinnr)
        {
            if (skinnr < 0)
            {
                throw new Exception("skinnr has too be positive.");
            }

            this.BackColor = this.host.GetPrimaryClr(skinnr);
            this.pnlHead.BackColor = this.host.GetPrimaryClr(skinnr);
            this.pnlHead.BackColor = Color.Transparent;
            this.ForeColor = this.host.GetTextClr(skinnr);
            this.btnClose.ForeColor = this.host.GetTextClr(skinnr);
            this.btnDeleteSkin.ForeColor = this.host.GetTextClr(skinnr);
            this.btnDeleteSkin.FlatAppearance.MouseDownBackColor = this.host.GetHighlightClr(skinnr);
            this.btnNewSkin.ForeColor = this.host.GetTextClr(skinnr);
            this.btnNewSkin.FlatAppearance.MouseDownBackColor = this.host.GetHighlightClr(skinnr);
            this.btnEditSkin.ForeColor = this.host.GetTextClr(skinnr);
            this.btnEditSkin.FlatAppearance.MouseDownBackColor = this.host.GetHighlightClr(skinnr);
            this.btnSaveSkin.ForeColor = this.host.GetTextClr(skinnr);
            this.btnSaveSkin.FlatAppearance.MouseDownBackColor = this.host.GetHighlightClr(skinnr);
            if (this.host.GetPrimaryTexture(skinnr) != null)
            {
                this.BackgroundImage = this.host.GetPrimaryTexture(skinnr);
                this.BackgroundImageLayout = this.host.GetPrimaryTextureLayout(skinnr);
                this.pnlContent.BackColor = Color.Transparent;
            }
            else
            {
                this.BackgroundImage = null;
            }
        }

        /// <summary>
        /// The current mode of the skins editor.
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
        }

        /// <summary>
        /// Set the mode of the editor.
        /// </summary>
        /// <param name="newmode">The new mode of the skinseditor.</param>
        private void setEditorMode(skineditormode newmode)
        {
            this.skinaction = newmode;
            switch (newmode)
            {
                case skineditormode.browseskins:
                    this.btnEditSkin.Text = "&edit skin";
                    this.btnNewSkin.Text = "&new skin";
                    this.btnNewSkin.Enabled = true;
                    this.btnEditSkin.Enabled = true;
                    this.btnDeleteSkin.Enabled = true;
                    this.SetFieldsEnabled(false);
                    if (this.lbxSkins.SelectedIndex >= 0)
                    {
                        this.skin = SkinFactory.GetSkin(this.host, this.lbxSkins.SelectedIndex);
                        this.SetFieldsCurrentSkin();
                    }
                    break;
                case skineditormode.editskin:
                    this.btnEditSkin.Text = "cancel &edit skin";
                    this.btnNewSkin.Text = "&new skin";
                    this.btnEditSkin.Enabled = true;
                    this.btnNewSkin.Enabled = false;
                    this.btnDeleteSkin.Enabled = false;
                    this.editskinnr = this.lbxSkins.SelectedIndex;
                    this.skin = SkinFactory.GetSkin(this.host, this.editskinnr);
                    this.SetFieldsEnabled(true);
                    this.SetFieldsCurrentSkin();
                    break;
                case skineditormode.newskin:
                    this.btnEditSkin.Text = "&edit skin";
                    this.btnNewSkin.Text = "cancel &new skin";
                    this.btnNewSkin.Enabled = true;
                    this.btnEditSkin.Enabled = false;
                    this.btnDeleteSkin.Enabled = false;
                    this.ClearFields();
                    this.skin = SkinFactory.CreateDefaultNewSkin();
                    this.SetFieldsEnabled(true);
                    break;
            }
        }

        /// <summary>
        /// Closed the skins editor form.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Fill in fields by from a skin from selected skin position.
        /// </summary>
        /// <param name="skinnr">The skin position</param>
        private void SetFieldsCurrentSkin()
        {
            this.tbSkinName.Text = this.skin.Name;
            this.tbPrimaryColor.Text = SkinFactory.ClrObjToHtmlHexClr(this.skin.PrimaryClr);
            this.tbSelectingColor.Text = SkinFactory.ClrObjToHtmlHexClr(this.skin.SelectClr);
            this.tbHighlightingColor.Text = SkinFactory.ClrObjToHtmlHexClr(this.skin.HighlightClr);
            this.tbTextColor.Text = SkinFactory.ClrObjToHtmlHexClr(this.skin.TextClr);
            this.tbPrimaryTexture.Text = this.skin.PrimaryTexture;
            this.chxUseTexture.Checked = !string.IsNullOrEmpty(this.skin.PrimaryTexture);
            if (this.skinaction != skineditormode.browseskins)
            {
                this.chxUseTexture_CheckedChanged(null, null);
            }

            string texturelayout = Enum.GetName(this.skin.PrimaryTextureLayout.GetType(), this.skin.PrimaryTextureLayout);
            for (int i = 0; i < this.cbxPrimaryTextureLayout.Items.Count; i++)
            {
                if (this.cbxPrimaryTextureLayout.Items[i].ToString().Equals(texturelayout, StringComparison.Ordinal))
                {
                    this.cbxPrimaryTextureLayout.SelectedIndex = i;
                    break;
                }
            }  

            this.notePreview1.Visible = true;
            this.notePreview1.DrawNoteSkinPreview(this.skin);
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
                this.setEditorMode(skineditormode.browseskins);
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
            this.pnlClrPrimary.Enabled = enabled;
            this.pnlClrSelecting.Enabled = enabled;
            this.pnlClrHighlight.Enabled = enabled;
            this.pnlClrText.Enabled = enabled;
            this.btnSaveSkin.Enabled = enabled;
            this.chxUseTexture.Enabled = enabled;
            if (!enabled)
            {
                this.tbPrimaryTexture.Enabled = false;
                this.btnBrowsePrimaryTexture.Enabled = false;
                this.cbxPrimaryTextureLayout.Enabled = false;
            }

            this.lbxSkins.Enabled = !enabled;
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
            this.chxUseTexture.Checked = false;
            this.pnlClrPrimary.BackColor = Color.White;
            this.pnlClrSelecting.BackColor = Color.White;
            this.pnlClrHighlight.BackColor = Color.White;
            this.pnlClrText.BackColor = Color.White;
            this.tbPrimaryColor.BackColor = SystemColors.Window;
            this.tbSelectingColor.BackColor = SystemColors.Window;
            this.tbHighlightingColor.BackColor = SystemColors.Window;
            this.tbTextColor.BackColor = SystemColors.Window;
            this.cbxPrimaryTextureLayout.SelectedIndex = -1;
            this.btnEditSkin.Enabled = false;
            this.btnDeleteSkin.Enabled = false;
            this.notePreview1.Visible = false;
        }

        /// <summary>
        /// Requested to edit a skin
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnEditskin_Click(object sender, EventArgs e)
        {
            if (this.skinaction == skineditormode.editskin)
            {
                // cancel edit skin
                this.lbxSkins.SelectedIndex = this.editskinnr;
                this.setEditorMode(skineditormode.browseskins);
            }
            else
            {
                this.setEditorMode(skineditormode.editskin);
            }
        }

        /// <summary>
        /// Requested to create new skin.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnNewSkin_Click(object sender, EventArgs e)
        {
            if (this.skinaction == skineditormode.newskin)
            {
                // cancel new skin
                this.setEditorMode(skineditormode.browseskins);
                this.tbSkinName.BackColor = SystemColors.Window;
            }
            else
            {
                this.setEditorMode(skineditormode.newskin);
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
                        if (this.skinaction == skineditormode.newskin)
                        {
                            this.lbxSkins.Items.Add(this.tbSkinName.Text);
                            this.btnNewSkin.Text = "&new skin";
                            this.skin.Name = this.tbSkinName.Text;
                            this.skin.PrimaryClr = SkinFactory.HtmlHexClrToClrObj(this.host, this.tbPrimaryColor.Text);
                            this.skin.SelectClr = SkinFactory.HtmlHexClrToClrObj(this.host, this.tbSelectingColor.Text);
                            this.skin.HighlightClr = SkinFactory.HtmlHexClrToClrObj(this.host, this.tbHighlightingColor.Text);
                            this.skin.TextClr = SkinFactory.HtmlHexClrToClrObj(this.host, this.tbTextColor.Text);
                            this.skin.PrimaryTexture = this.tbPrimaryTexture.Text;
                            this.cbxPrimaryTextureLayout_SelectedIndexChanged(null, null);
                            if (!SkinsFilehandling.WriteSkinsFileNewSkin(this.host, this.skin))
                            {
                                this.host.LogPluginError("Could not write new skin.");
                            }
                        }
                        else if (this.skinaction == skineditormode.editskin)
                        {
                            this.btnEditSkin.Text = "&edit skin";
                            if (!SkinsFilehandling.WriteSkinsFileEditSkin(this.host, this.editskinnr, this.skin))
                            {
                                this.host.LogPluginError("Could not save edited skin.");
                            }
                        }

                        this.host.ReloadAllSkins();
                        this.host.UpdateAllNoteForms();
                        this.LoadAllSkinNames();
                        this.lbxSkins.SelectedIndex = this.editskinnr;
                        this.setEditorMode(skineditormode.browseskins);
                    }
                }
                else
                {
                    this.tbSkinName.BackColor = Color.Red;
                    this.tbSkinName.Select();
                }
            }
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

            if (this.skinaction == skineditormode.newskin)
            {
                string[] skinnames = this.host.GetSkinsNames();
                for (int i = 0; i < skinnames.Length; i++)
                {
                    if (this.tbSkinName.Text.Equals(skinnames[i], StringComparison.Ordinal))
                    {
                        return false;
                    }
                }
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
        /// <param name="sender">Sender object, textbox</param>
        /// <param name="e">Event arguments</param>
        private void ParserAsPreviewColor(object sender, EventArgs e)
        {
            TextBox tbclr = (TextBox)sender;
            if (this.CheckProperHTMLColorTb(tbclr))
            {
                int tbnr = Convert.ToInt32(tbclr.Tag);
                bool invalidcolor = false;
                Color clr = SkinFactory.HtmlHexClrToClrObj(this.host, tbclr.Text);
                if (clr == Color.Transparent)
                {
                    invalidcolor = true;
                }

                switch (tbnr)
                {
                    case 1:
                        this.setControlColorExample(invalidcolor, clr, this.tbPrimaryColor, this.pnlClrPrimary);
                        this.skin.PrimaryClr = clr;
                        break;
                    case 2:
                        this.setControlColorExample(invalidcolor, clr, this.tbSelectingColor, this.pnlClrSelecting);
                        this.skin.SelectClr = clr;
                        break;
                    case 3:
                        this.setControlColorExample(invalidcolor, clr, this.tbHighlightingColor, this.pnlClrHighlight);
                        this.skin.HighlightClr = clr;
                        break;
                    case 4:
                        this.setControlColorExample(invalidcolor, clr, this.tbTextColor, this.pnlClrText);
                        this.skin.TextClr = clr;
                        break;
                }
            }
        }

        /// <summary>
        /// Set the panel with a example of the HEX color set in colorinputtextbox.
        /// </summary>
        /// <param name="invalidcolor">The color to set the textbox in case it's invalid HEX color.</param>
        /// <param name="clr">The color to set the panel with the example of the HEX color in the colorinputtextbox</param>
        /// <param name="colorinputtextbox">The colorinputtextbox with a HEX color valeu</param>
        /// <param name="exmplepanel">The panel to show the color example in.</param>
        private void setControlColorExample(bool invalidcolor, Color clr, TextBox colorinputtextbox, Panel exmplepanel)
        {
            if (invalidcolor)
            {
                colorinputtextbox.BackColor = Color.LightSalmon;
            }
            else
            {
                colorinputtextbox.BackColor = SystemColors.Window;
                exmplepanel.BackColor = clr;
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

                string skinfolder = Directory.GetParent(this.openFileTextureDialog.FileName).ToString();
                string defaulttexturefolder = this.host.GetInstallFolder();
                StringComparison strcomp = StringComparison.Ordinal;
                if (this.host.GetOS().Equals("WINDOWS", StringComparison.Ordinal))
                {
                    strcomp = StringComparison.OrdinalIgnoreCase;
                }

                if (skinfolder.Equals(defaulttexturefolder, strcomp))
                {
                    try
                    {
                        FileInfo fileinfotexture = new FileInfo(this.openFileTextureDialog.FileName);
                        // only filename
                        this.tbPrimaryTexture.Text = fileinfotexture.Name;
                        this.skin.PrimaryTexture = fileinfotexture.Name;
                    }
                    catch (Exception)
                    {
                        this.tbPrimaryTexture.BackColor = Color.Red;
                    }
                }
                else
                {
                    if (!File.Exists(this.openFileTextureDialog.FileName))
                    {
                        this.tbPrimaryTexture.BackColor = Color.Red;
                    }

                    // full file path
                    this.tbPrimaryTexture.Text = this.openFileTextureDialog.FileName;
                    this.skin.PrimaryTexture = this.openFileTextureDialog.FileName;
                }

                this.notePreview1.DrawNoteSkinPreview(this.skin);
            }
        }

        /// <summary>
        /// Delete a skin.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnDeleteSkin_Click(object sender, EventArgs e)
        {
            int skinnr = this.lbxSkins.SelectedIndex;
            if (skinnr >= 0)
            {
                if (this.lbxSkins.Items.Count > 1)
                {
                    this.btnDeleteSkin.Enabled = true;
                    string skinname = this.host.GetSkinName(skinnr);
                    DialogResult res = MessageBox.Show("Do you want to delete the " + skinname + " skin?", "delete skin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        this.skinaction = skineditormode.browseskins;
                        if (SkinsFilehandling.WriteSkinsFileDeleteSkin(this.host, skinnr))
                        {
                            this.lbxSkins.Items.RemoveAt(skinnr);
                        }
                        else
                        {
                            this.host.LogPluginError("Could not delete skin.");
                        }

                        this.host.ReloadAllSkins();
                        this.ClearFields();
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

        /// <summary>
        /// Start dragging the FrmSkinsEditor.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Mouse event arguments</param>
        private void pnlHead_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int skinnr = this.host.GetSettingInt("ManagenotesSkinnr");
                this.pnlHead.BackColor = this.host.GetSelectClr(skinnr);
                this.oldp = e.Location;
            }
        }

        /// <summary>
        /// Dragging the FrmSkinsEditor.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Mouse event arguments</param>
        private void pnlHead_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int dpx = e.Location.X - this.oldp.X;
                int dpy = e.Location.Y - this.oldp.Y;

                // Limit the moving of this note under mono/linux so this note cannot move uncontrolled a lot.
                const int movelimit = 8;
                if (dpx > movelimit)
                {
                    dpx = movelimit;
                }
                else if (dpx < -movelimit)
                {
                    dpx = -movelimit;
                }

                if (dpy > movelimit)
                {
                    dpy = movelimit;
                }
                else if (dpy < -movelimit)
                {
                    dpy = -movelimit;
                }

                this.Location = new Point(this.Location.X + dpx, this.Location.Y + dpy);
            }
        }

        /// <summary>
        /// 'Titlebar' releases.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Mouse event arguments</param>
        private void pnlHead_MouseUp(object sender, MouseEventArgs e)
        {
            int skinnr = this.host.GetSettingInt("ManagenotesSkinnr");
            if (this.BackgroundImage != null)
            {
                this.pnlHead.BackColor = Color.Transparent;
            }
            else
            {
                this.pnlHead.BackColor = this.host.GetPrimaryClr(skinnr);
            }
            
        }

        /// <summary>
        /// Resizing skineditor.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Mouse event arguments</param>
        private void pbResizeGrip_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Cursor = Cursors.SizeNWSE;
                this.Size = new Size(this.PointToClient(MousePosition).X, this.PointToClient(MousePosition).Y);
            }

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Open a color dialog for selecing a color.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void ShowColorDlg(object sender, EventArgs e)
        {
            DialogResult dlgres = this.colordlg.ShowDialog();
            if (dlgres == DialogResult.OK)
            {
                Panel pnl = (Panel)sender;
                int pnlnr = Convert.ToInt32(pnl.Tag);
                switch (pnlnr)
                {
                    case 1:
                        this.skin.PrimaryClr = this.colordlg.Color;
                        break;
                    case 2:
                        this.skin.SelectClr = this.colordlg.Color;
                        break;
                    case 3:
                        this.skin.HighlightClr = this.colordlg.Color;
                        break;
                    case 4:
                        this.skin.TextClr = this.colordlg.Color;
                        break;
                }

                this.SetFieldsCurrentSkin();
            }
        }

        /// <summary>
        /// Set the cursor back to normal.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Mouse event arguments</param>
        private void BackNormalCusors(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// Set the cursor to hand.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Mouse event arguments</param>
        private void HandEnabled(object sender, MouseEventArgs e)
        {
            if (Cursor.Current == Cursors.Hand)
            {
                return;
            }

            Control ctrl = (Control)sender;
            if (ctrl.Enabled)
            {
                Cursor.Current = Cursors.Hand;
            }
        }

        /// <summary>
        /// Set the current skin name.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void tbSkinName_TextChanged(object sender, EventArgs e)
        {
            if (this.CheckProperSkinnameTb())
            {
                this.skin.Name = tbSkinName.Text;
            }
        }

        /// <summary>
        /// New primary texture set.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void tbPrimaryTexture_TextChanged(object sender, EventArgs e)
        {
            if (this.CheckProperHTMLColorTb(this.tbPrimaryColor))
            {
                this.skin.PrimaryTexture = this.tbPrimaryTexture.Text;
            }                
        }

        /// <summary>
        /// chxUseTexture is checked or unchecked.
        /// Enable or disable tbPrimaryTexture, cbxPrimaryTextureLayout and btnBrowsePrimaryTexture.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void chxUseTexture_CheckedChanged(object sender, EventArgs e)
        {
            if (this.skinaction != skineditormode.browseskins)
            {
                this.btnBrowsePrimaryTexture.Enabled = this.chxUseTexture.Checked;
                this.tbPrimaryTexture.Enabled = this.chxUseTexture.Checked;
                this.cbxPrimaryTextureLayout.Enabled = this.chxUseTexture.Checked;
            }

            if (this.chxUseTexture.Checked)
            {
                this.skin.PrimaryTexture = this.tbPrimaryTexture.Text;
            }
            else
            {
                this.skin.PrimaryTexture = null;
            }

            this.notePreview1.DrawNoteSkinPreview(this.skin);
        }

        /// <summary>
        /// Other texture image layout selected.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void cbxPrimaryTextureLayout_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxPrimaryTextureLayout.SelectedIndex >= 0)
            {
                try
                {
                    this.skin.PrimaryTextureLayout = (ImageLayout)Enum.Parse(typeof(ImageLayout), this.cbxPrimaryTextureLayout.Items[this.cbxPrimaryTextureLayout.SelectedIndex].ToString());
                }
                catch (ArgumentException argexc)
                {
                    this.host.LogPluginError(argexc.Message);
                    this.skin.PrimaryTextureLayout = ImageLayout.Tile;
                }

                this.notePreview1.DrawNoteSkinPreview(this.skin);
            }
        }
    }
}
