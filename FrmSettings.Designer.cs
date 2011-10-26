//-----------------------------------------------------------------------
// <copyright file="FrmSettings.Designer.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2010-2011  Tom
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
namespace NoteFly
{
    /// <summary>
    /// FrmSettings class.
    /// </summary>
    public partial class FrmSettings
    {
        /// <summary>
        /// Button btnOK
        /// </summary>
        private System.Windows.Forms.Button btnOK;

        /// <summary>
        /// Button btnCancel
        /// </summary>
        private System.Windows.Forms.Button btnCancel;

        /// <summary>
        /// TabControl tabControlSettings
        /// </summary>
        private System.Windows.Forms.TabControl tabControlSettings;

        /// <summary>
        /// TabPage tabAppearance
        /// </summary>
        private System.Windows.Forms.TabPage tabAppearance;

        /// <summary>
        /// TabPage tabSharing
        /// </summary>
        private System.Windows.Forms.TabPage tabSharing;

        /// <summary>
        /// TabPage tabAdvance
        /// </summary>
        private System.Windows.Forms.TabPage tabAdvance;

        /// <summary>
        /// Button btnBrowse
        /// </summary>
        private System.Windows.Forms.Button btnBrowse;

        /// <summary>
        /// Label lblTextNoteLocation
        /// </summary>
        private System.Windows.Forms.Label lblTextNoteLocation;

        /// <summary>
        /// TextBox tbNotesSavePath
        /// </summary>
        private System.Windows.Forms.TextBox tbNotesSavePath;

        /// <summary>
        /// FolderBrowserDialog folderBrowserDialogNotessavepath
        /// </summary>
        private System.Windows.Forms.FolderBrowserDialog folderBrowseDialogNotessavepath;

        /// <summary>
        /// ComboBox cbxActionLeftclick
        /// </summary>
        private System.Windows.Forms.ComboBox cbxActionLeftclick;

        /// <summary>
        /// Label lblTextActionLeftClicktTrayicon
        /// </summary>
        private System.Windows.Forms.Label lblTextActionLeftClicktTrayicon;

        /// <summary>
        /// TabPage tabGeneral
        /// </summary>
        private System.Windows.Forms.TabPage tabGeneral;

#if windows
        /// <summary>
        /// CheckBox chxStartOnLogin
        /// </summary>
        private System.Windows.Forms.CheckBox chxStartOnLogin;
#endif
        /// <summary>
        /// CheckBox chxConfirmLink
        /// </summary>
        private System.Windows.Forms.CheckBox chxConfirmLink;

        /// <summary>
        /// CheckBox chxConfirmExit
        /// </summary>
        private System.Windows.Forms.CheckBox chxConfirmExit;

        /// <summary>
        /// CheckBox chxLogErrors
        /// </summary>
        private System.Windows.Forms.CheckBox chxLogErrors;

        /// <summary>
        /// Button btnResetSettings
        /// </summary>
        private System.Windows.Forms.Button btnResetSettings;

        /// <summary>
        /// TabPage tabNetwerk
        /// </summary>
        private System.Windows.Forms.TabPage tabNetwork;

        /// <summary>
        /// CheckBox chxProxyEnabled
        /// </summary>
        private System.Windows.Forms.CheckBox chxProxyEnabled;

        /// <summary>
        /// CheckBox chxLogDebug
        /// </summary>
        private System.Windows.Forms.CheckBox chxLogDebug;

        /// <summary>
        /// TabPage tabHighlight
        /// </summary>
        private System.Windows.Forms.TabPage tabHighlight;

        /// <summary>
        /// CheckBox chxHighlightHTML
        /// </summary>
        private System.Windows.Forms.CheckBox chxHighlightHTML;

        /// <summary>
        /// IPTextBox user component.
        /// </summary>
        private IPTextBox iptbProxyAddress;

        /// <summary>
        /// Label lblTextNetworkMiliseconds
        /// </summary>
        private System.Windows.Forms.Label lblTextNetworkMiliseconds;

        /// <summary>
        /// Label lblTextNetworkTimeout
        /// </summary>
        private System.Windows.Forms.Label lblTextNetworkTimeout;

        /// <summary>
        /// NumericUpDown numTimeout
        /// </summary>
        private System.Windows.Forms.NumericUpDown numTimeout;

        /// <summary>
        /// CheckBox chxConfirmDeletenote
        /// </summary>
        private System.Windows.Forms.CheckBox chxConfirmDeletenote;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// CheckBox chxHighlightHyperlinks
        /// </summary>
        private System.Windows.Forms.CheckBox chxHighlightHyperlinks;

        /// <summary>
        /// CheckBox chxHighlightSQL
        /// </summary>
        private System.Windows.Forms.CheckBox chxHighlightSQL;

        /// <summary>
        /// CheckBox chxHighlightPHP
        /// </summary>
        private System.Windows.Forms.CheckBox chxHighlightPHP;

        /// <summary>
        /// TabControl tabControlSocialNetworks
        /// </summary>
        private System.Windows.Forms.TabControl tabControlSharing;

        /// <summary>
        /// TabPage tabEmail
        /// </summary>
        private System.Windows.Forms.TabPage tabEmail;

        /// <summary>
        /// CheckBox chxSocialEmailDefaultaddressBlank
        /// </summary>
        private System.Windows.Forms.CheckBox chxSocialEmailDefaultaddressSet;

        /// <summary>
        /// TextBox tbDefaultEmail
        /// </summary>
        private System.Windows.Forms.TextBox tbDefaultEmail;

        /// <summary>
        /// CheckBox chxSocialEmailEnabled
        /// </summary>
        private System.Windows.Forms.CheckBox chxSocialEmailEnabled;

        /// <summary>
        /// CheckBox chxCheckUpdates
        /// </summary>
        private System.Windows.Forms.CheckBox chxCheckUpdates;

        /// <summary>
        /// Label lblTextCheckforupdatesevery
        /// </summary>
        private System.Windows.Forms.Label lblTextCheckforupdatesevery;

        /// <summary>
        /// NumericUpDown numUpdateCheckDays
        /// </summary>
        private System.Windows.Forms.NumericUpDown numUpdateCheckDays;

        /// <summary>
        /// Label lblTextDayAtStartup
        /// </summary>
        private System.Windows.Forms.Label lblTextDayAtStartup;

        /// <summary>
        /// TabControl tabAppearanceColors
        /// </summary>
        private System.Windows.Forms.TabControl tabAppearanceColors;

        /// <summary>
        /// TabPage tabPageLooks
        /// </summary>
        private System.Windows.Forms.TabPage tabPageLooks;

        /// <summary>
        /// CheckBox chxTransparecy
        /// </summary>
        private System.Windows.Forms.CheckBox chxTransparecy;

        /// <summary>
        /// Label lblDefaultNewNoteColor
        /// </summary>
        private System.Windows.Forms.Label lblDefaultNewNoteColor;

        /// <summary>
        /// ComboBox cbxDefaultColor
        /// </summary>
        private System.Windows.Forms.ComboBox cbxDefaultColor;

        /// <summary>
        /// NumericUpDown numProcTransparency
        /// </summary>
        private System.Windows.Forms.NumericUpDown numProcTransparency;

        /// <summary>
        /// Label lblTextTransparentProcVisible
        /// </summary>
        private System.Windows.Forms.Label lblTextTransparentProcVisible;

        /// <summary>
        /// TabPage tabPageFonts
        /// </summary>
        private System.Windows.Forms.TabPage tabPageFonts;

        /// <summary>
        /// Label lblTextFontTitlePoints
        /// </summary>
        private System.Windows.Forms.Label lblTextFontTitlePoints;

        /// <summary>
        /// NumericUpDown numFontSizeTitle
        /// </summary>
        private System.Windows.Forms.NumericUpDown numFontSizeTitle;

        /// <summary>
        /// Label lblTextFontTitleSize
        /// </summary>
        private System.Windows.Forms.Label lblTextFontTitleSize;

        /// <summary>
        /// ComboBox cbxFontNoteTitle
        /// </summary>
        private System.Windows.Forms.ComboBox cbxFontNoteTitle;

        /// <summary>
        /// Label lblTextFontTitleFamily
        /// </summary>
        private System.Windows.Forms.Label lblTextFontTitleFamily;

        /// <summary>
        /// Label lblTextDirection
        /// </summary>
        private System.Windows.Forms.Label lblTextDirection;

        /// <summary>
        /// ComboBox cbxTextDirection
        /// </summary>
        private System.Windows.Forms.ComboBox cbxTextDirection;

        /// <summary>
        /// Label lblTextFontContentPoints
        /// </summary>
        private System.Windows.Forms.Label lblTextFontContentPoints;

        /// <summary>
        /// NumericUpDown numFontSizeContent
        /// </summary>
        private System.Windows.Forms.NumericUpDown numFontSizeContent;

        /// <summary>
        /// Label lblTextFontContentSize
        /// </summary>
        private System.Windows.Forms.Label lblTextFontContentSize;

        /// <summary>
        /// Label lblTextNoteFont
        /// </summary>
        private System.Windows.Forms.Label lblTextNoteFont;

        /// <summary>
        /// ComboBox cbxFontNoteContent
        /// </summary>
        private System.Windows.Forms.ComboBox cbxFontNoteContent;

        /// <summary>
        /// CheckBox cbxFontNoteTitleBold
        /// </summary>
        private System.Windows.Forms.CheckBox cbxFontNoteTitleBold;

        /// <summary>
        /// CheckBox cbxShowTooltips
        /// </summary>
        private System.Windows.Forms.CheckBox chxShowTooltips;

        /// <summary>
        /// Label lblTextLogging
        /// </summary>
        private System.Windows.Forms.Label lblTextLogging;

        /// <summary>
        /// CheckBox chxLogExceptions
        /// </summary>
        private System.Windows.Forms.CheckBox chxLogExceptions;

        /// <summary>
        /// TabPage tabPageTrayicon
        /// </summary>
        private System.Windows.Forms.TabPage tabPageTrayicon;

        /// <summary>
        /// CheckBox chxTrayiconBoldExit
        /// </summary>
        private System.Windows.Forms.CheckBox chxTrayiconBoldExit;

        /// <summary>
        /// CheckBox chxTrayiconBoldSettings
        /// </summary>
        private System.Windows.Forms.CheckBox chxTrayiconBoldSettings;

        /// <summary>
        /// CheckBox chxTrayiconBoldManagenotes
        /// </summary>
        private System.Windows.Forms.CheckBox chxTrayiconBoldManagenotes;

        /// <summary>
        /// CheckBox chxTrayiconBoldNewnote
        /// </summary>
        private System.Windows.Forms.CheckBox chxTrayiconBoldNewnote;

        /// <summary>
        /// CheckBox chxUseRandomDefaultNote
        /// </summary>
        private System.Windows.Forms.CheckBox chxUseRandomDefaultNote;

        /// <summary>
        /// CheckBox chxNotesDeleteRecyclebin
        /// </summary>
        private System.Windows.Forms.CheckBox chxNotesDeleteRecyclebin;

        /// <summary>
        /// Label lblTextFontsizeMenu
        /// </summary>
        private System.Windows.Forms.Label lblTextFontsizeMenu;

        /// <summary>
        /// NumericUpDown numTrayiconFontsize
        /// </summary>
        private System.Windows.Forms.NumericUpDown numTrayiconFontsize;

        /// <summary>
        /// Label lblFontsizePoints
        /// </summary>
        private System.Windows.Forms.Label lblFontsizePoints;

        /// <summary>
        /// Button btnCheckUpdates
        /// </summary>
        private System.Windows.Forms.Button btnCheckUpdates;

        /// <summary>
        /// Label lblTextLatestUpdateCheck
        /// </summary>
        private System.Windows.Forms.Label lblTextLatestUpdateCheck;

        /// <summary>
        /// Label lblLatestUpdateCheck
        /// </summary>
        private System.Windows.Forms.Label lblLatestUpdateCheck;

        /// <summary>
        /// CheckBox chxSettingsExpertEnabled
        /// </summary>
        private System.Windows.Forms.CheckBox chxSettingsExpertEnabled;

        /// <summary>
        /// CheckBox chxLoadPlugins
        /// </summary>
        private System.Windows.Forms.CheckBox chxLoadPlugins;

        /// <summary>
        /// TabPage tabPlugins
        /// </summary>
        private System.Windows.Forms.TabPage tabPlugins;

        /// <summary>
        /// CheckBox chxCheckUpdatesSignature
        /// </summary>
        private System.Windows.Forms.CheckBox chxCheckUpdatesSignature;

        /// <summary>
        /// TabControl tabControlNetwork
        /// </summary>
        private System.Windows.Forms.TabControl tabControlNetwork;

        /// <summary>
        /// TabPage tabUpdates
        /// </summary>
        private System.Windows.Forms.TabPage tabUpdates;

        /// <summary>
        /// TabPage tabProxy
        /// </summary>
        private System.Windows.Forms.TabPage tabProxy;

        /// <summary>
        /// Label lblTextGPGPath
        /// </summary>
        private System.Windows.Forms.Label lblTextGPGPath;

        /// <summary>
        /// TextBox tbGPGPath
        /// </summary>
        private System.Windows.Forms.TextBox tbGPGPath;

        /// <summary>
        /// Button btnGPGPathBrowse
        /// </summary>
        private System.Windows.Forms.Button btnGPGPathBrowse;

        /// <summary>
        /// OpenFileDialog openFileDialogBrowseGPG
        /// </summary>
        private System.Windows.Forms.OpenFileDialog openFileDialogBrowseGPG;

        /// <summary>
        /// CheckBox chxUpdateSilentInstall
        /// </summary>
        private System.Windows.Forms.CheckBox chxUpdateSilentInstall;

        /// <summary>
        /// CheckBox chxUseAlternativeTrayicon
        /// </summary>
        private System.Windows.Forms.CheckBox chxUseAlternativeTrayicon;

        /// <summary>
        /// Label lblTextMiliseconds
        /// </summary>
        private System.Windows.Forms.Label lblTextMiliseconds;

        /// <summary>
        /// PluginGrid pluginGrid
        /// </summary>
        private PluginGrid pluginGrid;        

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSettings));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.chxNotesDeleteRecyclebin = new System.Windows.Forms.CheckBox();
            this.chxConfirmDeletenote = new System.Windows.Forms.CheckBox();
            this.cbxActionLeftclick = new System.Windows.Forms.ComboBox();
            this.chxConfirmExit = new System.Windows.Forms.CheckBox();
            this.chxStartOnLogin = new System.Windows.Forms.CheckBox();
            this.lblTextActionLeftClicktTrayicon = new System.Windows.Forms.Label();
            this.tabAppearance = new System.Windows.Forms.TabPage();
            this.tabAppearanceColors = new System.Windows.Forms.TabControl();
            this.tabPageLooks = new System.Windows.Forms.TabPage();
            this.chxUseRandomDefaultNote = new System.Windows.Forms.CheckBox();
            this.chxShowTooltips = new System.Windows.Forms.CheckBox();
            this.chxTransparecy = new System.Windows.Forms.CheckBox();
            this.lblDefaultNewNoteColor = new System.Windows.Forms.Label();
            this.cbxDefaultColor = new System.Windows.Forms.ComboBox();
            this.numProcTransparency = new System.Windows.Forms.NumericUpDown();
            this.lblTextTransparentProcVisible = new System.Windows.Forms.Label();
            this.tabPageFonts = new System.Windows.Forms.TabPage();
            this.cbxFontNoteTitleBold = new System.Windows.Forms.CheckBox();
            this.lblTextFontTitlePoints = new System.Windows.Forms.Label();
            this.numFontSizeTitle = new System.Windows.Forms.NumericUpDown();
            this.lblTextFontTitleSize = new System.Windows.Forms.Label();
            this.cbxFontNoteTitle = new System.Windows.Forms.ComboBox();
            this.lblTextFontTitleFamily = new System.Windows.Forms.Label();
            this.lblTextDirection = new System.Windows.Forms.Label();
            this.cbxTextDirection = new System.Windows.Forms.ComboBox();
            this.lblTextFontContentPoints = new System.Windows.Forms.Label();
            this.numFontSizeContent = new System.Windows.Forms.NumericUpDown();
            this.lblTextFontContentSize = new System.Windows.Forms.Label();
            this.lblTextNoteFont = new System.Windows.Forms.Label();
            this.cbxFontNoteContent = new System.Windows.Forms.ComboBox();
            this.tabPageTrayicon = new System.Windows.Forms.TabPage();
            this.chxUseAlternativeTrayicon = new System.Windows.Forms.CheckBox();
            this.lblFontsizePoints = new System.Windows.Forms.Label();
            this.lblTextFontsizeMenu = new System.Windows.Forms.Label();
            this.numTrayiconFontsize = new System.Windows.Forms.NumericUpDown();
            this.chxTrayiconBoldExit = new System.Windows.Forms.CheckBox();
            this.chxTrayiconBoldSettings = new System.Windows.Forms.CheckBox();
            this.chxTrayiconBoldManagenotes = new System.Windows.Forms.CheckBox();
            this.chxTrayiconBoldNewnote = new System.Windows.Forms.CheckBox();
            this.tabPlugins = new System.Windows.Forms.TabPage();
            this.chxLoadPlugins = new System.Windows.Forms.CheckBox();
            this.tabHighlight = new System.Windows.Forms.TabPage();
            this.chxHighlightSQL = new System.Windows.Forms.CheckBox();
            this.chxHighlightPHP = new System.Windows.Forms.CheckBox();
            this.chxHighlightHyperlinks = new System.Windows.Forms.CheckBox();
            this.chxConfirmLink = new System.Windows.Forms.CheckBox();
            this.chxHighlightHTML = new System.Windows.Forms.CheckBox();
            this.tabSharing = new System.Windows.Forms.TabPage();
            this.tabControlSharing = new System.Windows.Forms.TabControl();
            this.tabEmail = new System.Windows.Forms.TabPage();
            this.chxSocialEmailEnabled = new System.Windows.Forms.CheckBox();
            this.chxSocialEmailDefaultaddressSet = new System.Windows.Forms.CheckBox();
            this.tbDefaultEmail = new System.Windows.Forms.TextBox();
            this.tabNetwork = new System.Windows.Forms.TabPage();
            this.tabControlNetwork = new System.Windows.Forms.TabControl();
            this.tabUpdates = new System.Windows.Forms.TabPage();
            this.chxUpdateSilentInstall = new System.Windows.Forms.CheckBox();
            this.btnGPGPathBrowse = new System.Windows.Forms.Button();
            this.tbGPGPath = new System.Windows.Forms.TextBox();
            this.lblTextGPGPath = new System.Windows.Forms.Label();
            this.chxCheckUpdatesSignature = new System.Windows.Forms.CheckBox();
            this.chxCheckUpdates = new System.Windows.Forms.CheckBox();
            this.numUpdateCheckDays = new System.Windows.Forms.NumericUpDown();
            this.lblTextCheckforupdatesevery = new System.Windows.Forms.Label();
            this.lblLatestUpdateCheck = new System.Windows.Forms.Label();
            this.lblTextDayAtStartup = new System.Windows.Forms.Label();
            this.lblTextLatestUpdateCheck = new System.Windows.Forms.Label();
            this.btnCheckUpdates = new System.Windows.Forms.Button();
            this.tabProxy = new System.Windows.Forms.TabPage();
            this.lblTextMiliseconds = new System.Windows.Forms.Label();
            this.numTimeout = new System.Windows.Forms.NumericUpDown();
            this.chxProxyEnabled = new System.Windows.Forms.CheckBox();
            this.lblTextNetworkTimeout = new System.Windows.Forms.Label();
            this.lblTextNetworkMiliseconds = new System.Windows.Forms.Label();
            this.tabAdvance = new System.Windows.Forms.TabPage();
            this.chxLogExceptions = new System.Windows.Forms.CheckBox();
            this.lblTextLogging = new System.Windows.Forms.Label();
            this.chxLogDebug = new System.Windows.Forms.CheckBox();
            this.btnResetSettings = new System.Windows.Forms.Button();
            this.chxLogErrors = new System.Windows.Forms.CheckBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblTextNoteLocation = new System.Windows.Forms.Label();
            this.tbNotesSavePath = new System.Windows.Forms.TextBox();
            this.chxSettingsExpertEnabled = new System.Windows.Forms.CheckBox();
            this.folderBrowseDialogNotessavepath = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialogBrowseGPG = new System.Windows.Forms.OpenFileDialog();
            this.pluginGrid = new NoteFly.PluginGrid();
            this.iptbProxyAddress = new NoteFly.IPTextBox();
            this.tabControlSettings.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.tabAppearance.SuspendLayout();
            this.tabAppearanceColors.SuspendLayout();
            this.tabPageLooks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numProcTransparency)).BeginInit();
            this.tabPageFonts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSizeTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSizeContent)).BeginInit();
            this.tabPageTrayicon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTrayiconFontsize)).BeginInit();
            this.tabPlugins.SuspendLayout();
            this.tabHighlight.SuspendLayout();
            this.tabSharing.SuspendLayout();
            this.tabControlSharing.SuspendLayout();
            this.tabEmail.SuspendLayout();
            this.tabNetwork.SuspendLayout();
            this.tabControlNetwork.SuspendLayout();
            this.tabUpdates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpdateCheckDays)).BeginInit();
            this.tabProxy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).BeginInit();
            this.tabAdvance.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.BackColor = System.Drawing.Color.LightGray;
            this.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(227, 368);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(105, 25);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.BackColor = System.Drawing.Color.LightGray;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(338, 368);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(103, 25);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.Controls.Add(this.tabGeneral);
            this.tabControlSettings.Controls.Add(this.tabAppearance);
            this.tabControlSettings.Controls.Add(this.tabPlugins);
            this.tabControlSettings.Controls.Add(this.tabHighlight);
            this.tabControlSettings.Controls.Add(this.tabSharing);
            this.tabControlSettings.Controls.Add(this.tabNetwork);
            this.tabControlSettings.Controls.Add(this.tabAdvance);
            this.tabControlSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControlSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlSettings.HotTrack = true;
            this.tabControlSettings.Location = new System.Drawing.Point(0, 0);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            this.tabControlSettings.Size = new System.Drawing.Size(455, 362);
            this.tabControlSettings.TabIndex = 17;
            this.tabControlSettings.SelectedIndexChanged += new System.EventHandler(this.tabControlSettings_SelectedIndexChanged);
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.chxNotesDeleteRecyclebin);
            this.tabGeneral.Controls.Add(this.chxConfirmDeletenote);
            this.tabGeneral.Controls.Add(this.cbxActionLeftclick);
            this.tabGeneral.Controls.Add(this.chxConfirmExit);
            this.tabGeneral.Controls.Add(this.chxStartOnLogin);
            this.tabGeneral.Controls.Add(this.lblTextActionLeftClicktTrayicon);
            this.tabGeneral.Location = new System.Drawing.Point(4, 25);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(447, 333);
            this.tabGeneral.TabIndex = 3;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // chxNotesDeleteRecyclebin
            // 
            this.chxNotesDeleteRecyclebin.AutoSize = true;
            this.chxNotesDeleteRecyclebin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxNotesDeleteRecyclebin.Location = new System.Drawing.Point(21, 94);
            this.chxNotesDeleteRecyclebin.Name = "chxNotesDeleteRecyclebin";
            this.chxNotesDeleteRecyclebin.Size = new System.Drawing.Size(231, 20);
            this.chxNotesDeleteRecyclebin.TabIndex = 24;
            this.chxNotesDeleteRecyclebin.Text = "Move deleted notes to recycle bin.";
            this.chxNotesDeleteRecyclebin.UseVisualStyleBackColor = true;
            // 
            // chxConfirmDeletenote
            // 
            this.chxConfirmDeletenote.AutoSize = true;
            this.chxConfirmDeletenote.Checked = true;
            this.chxConfirmDeletenote.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxConfirmDeletenote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxConfirmDeletenote.Location = new System.Drawing.Point(21, 71);
            this.chxConfirmDeletenote.Name = "chxConfirmDeletenote";
            this.chxConfirmDeletenote.Size = new System.Drawing.Size(162, 20);
            this.chxConfirmDeletenote.TabIndex = 23;
            this.chxConfirmDeletenote.Text = "Confirm deleting notes.";
            this.chxConfirmDeletenote.UseVisualStyleBackColor = true;
            // 
            // cbxActionLeftclick
            // 
            this.cbxActionLeftclick.AutoCompleteCustomSource.AddRange(new string[] {
            "Bring all notes to front.",
            "Create a new note.",
            "Show manage notes.",
            "Do nothing."});
            this.cbxActionLeftclick.CausesValidation = false;
            this.cbxActionLeftclick.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxActionLeftclick.FormattingEnabled = true;
            this.cbxActionLeftclick.Items.AddRange(new object[] {
            "Do nothing",
            "Bring notes to front",
            "Create a new note"});
            this.cbxActionLeftclick.Location = new System.Drawing.Point(172, 147);
            this.cbxActionLeftclick.Name = "cbxActionLeftclick";
            this.cbxActionLeftclick.Size = new System.Drawing.Size(163, 24);
            this.cbxActionLeftclick.TabIndex = 16;
            // 
            // chxConfirmExit
            // 
            this.chxConfirmExit.AutoSize = true;
            this.chxConfirmExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxConfirmExit.Location = new System.Drawing.Point(21, 48);
            this.chxConfirmExit.Name = "chxConfirmExit";
            this.chxConfirmExit.Size = new System.Drawing.Size(195, 20);
            this.chxConfirmExit.TabIndex = 20;
            this.chxConfirmExit.Text = "Confirm shutdown of NoteFly";
            this.chxConfirmExit.UseVisualStyleBackColor = true;
            // 
            // chxStartOnLogin
            // 
            this.chxStartOnLogin.AutoSize = true;
            this.chxStartOnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxStartOnLogin.Location = new System.Drawing.Point(21, 25);
            this.chxStartOnLogin.Name = "chxStartOnLogin";
            this.chxStartOnLogin.Size = new System.Drawing.Size(162, 20);
            this.chxStartOnLogin.TabIndex = 10;
            this.chxStartOnLogin.Text = "Start NoteFly on logon.";
            this.chxStartOnLogin.UseVisualStyleBackColor = true;
            // 
            // lblTextActionLeftClicktTrayicon
            // 
            this.lblTextActionLeftClicktTrayicon.AutoSize = true;
            this.lblTextActionLeftClicktTrayicon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextActionLeftClicktTrayicon.Location = new System.Drawing.Point(18, 148);
            this.lblTextActionLeftClicktTrayicon.Name = "lblTextActionLeftClicktTrayicon";
            this.lblTextActionLeftClicktTrayicon.Size = new System.Drawing.Size(148, 16);
            this.lblTextActionLeftClicktTrayicon.TabIndex = 15;
            this.lblTextActionLeftClicktTrayicon.Text = "Action left click trayicon:";
            // 
            // tabAppearance
            // 
            this.tabAppearance.Controls.Add(this.tabAppearanceColors);
            this.tabAppearance.Location = new System.Drawing.Point(4, 25);
            this.tabAppearance.Name = "tabAppearance";
            this.tabAppearance.Padding = new System.Windows.Forms.Padding(3);
            this.tabAppearance.Size = new System.Drawing.Size(447, 333);
            this.tabAppearance.TabIndex = 0;
            this.tabAppearance.Text = "Appearance";
            this.tabAppearance.UseVisualStyleBackColor = true;
            // 
            // tabAppearanceColors
            // 
            this.tabAppearanceColors.Controls.Add(this.tabPageLooks);
            this.tabAppearanceColors.Controls.Add(this.tabPageFonts);
            this.tabAppearanceColors.Controls.Add(this.tabPageTrayicon);
            this.tabAppearanceColors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabAppearanceColors.Location = new System.Drawing.Point(3, 3);
            this.tabAppearanceColors.Name = "tabAppearanceColors";
            this.tabAppearanceColors.SelectedIndex = 0;
            this.tabAppearanceColors.Size = new System.Drawing.Size(441, 327);
            this.tabAppearanceColors.TabIndex = 28;
            // 
            // tabPageLooks
            // 
            this.tabPageLooks.Controls.Add(this.chxUseRandomDefaultNote);
            this.tabPageLooks.Controls.Add(this.chxShowTooltips);
            this.tabPageLooks.Controls.Add(this.chxTransparecy);
            this.tabPageLooks.Controls.Add(this.lblDefaultNewNoteColor);
            this.tabPageLooks.Controls.Add(this.cbxDefaultColor);
            this.tabPageLooks.Controls.Add(this.numProcTransparency);
            this.tabPageLooks.Controls.Add(this.lblTextTransparentProcVisible);
            this.tabPageLooks.Location = new System.Drawing.Point(4, 25);
            this.tabPageLooks.Name = "tabPageLooks";
            this.tabPageLooks.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLooks.Size = new System.Drawing.Size(433, 298);
            this.tabPageLooks.TabIndex = 0;
            this.tabPageLooks.Text = "Looks";
            this.tabPageLooks.UseVisualStyleBackColor = true;
            // 
            // chxUseRandomDefaultNote
            // 
            this.chxUseRandomDefaultNote.AutoSize = true;
            this.chxUseRandomDefaultNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxUseRandomDefaultNote.Location = new System.Drawing.Point(18, 120);
            this.chxUseRandomDefaultNote.Name = "chxUseRandomDefaultNote";
            this.chxUseRandomDefaultNote.Size = new System.Drawing.Size(230, 20);
            this.chxUseRandomDefaultNote.TabIndex = 14;
            this.chxUseRandomDefaultNote.Text = "Use a random skin as default skin.";
            this.chxUseRandomDefaultNote.UseVisualStyleBackColor = true;
            this.chxUseRandomDefaultNote.CheckedChanged += new System.EventHandler(this.chxUseRandomDefaultNote_CheckedChanged);
            // 
            // chxShowTooltips
            // 
            this.chxShowTooltips.AutoSize = true;
            this.chxShowTooltips.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxShowTooltips.Location = new System.Drawing.Point(18, 62);
            this.chxShowTooltips.Name = "chxShowTooltips";
            this.chxShowTooltips.Size = new System.Drawing.Size(106, 20);
            this.chxShowTooltips.TabIndex = 13;
            this.chxShowTooltips.Text = "Show tooltips";
            this.chxShowTooltips.UseVisualStyleBackColor = true;
            // 
            // chxTransparecy
            // 
            this.chxTransparecy.AutoSize = true;
            this.chxTransparecy.BackColor = System.Drawing.Color.Transparent;
            this.chxTransparecy.CausesValidation = false;
            this.chxTransparecy.Checked = true;
            this.chxTransparecy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxTransparecy.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chxTransparecy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxTransparecy.Location = new System.Drawing.Point(18, 28);
            this.chxTransparecy.Name = "chxTransparecy";
            this.chxTransparecy.Size = new System.Drawing.Size(193, 21);
            this.chxTransparecy.TabIndex = 8;
            this.chxTransparecy.Text = "Enable transparency notes";
            this.chxTransparecy.UseVisualStyleBackColor = false;
            this.chxTransparecy.CheckedChanged += new System.EventHandler(this.chxTransparecy_CheckedChanged);
            // 
            // lblDefaultNewNoteColor
            // 
            this.lblDefaultNewNoteColor.AutoSize = true;
            this.lblDefaultNewNoteColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDefaultNewNoteColor.Location = new System.Drawing.Point(15, 146);
            this.lblDefaultNewNoteColor.Name = "lblDefaultNewNoteColor";
            this.lblDefaultNewNoteColor.Size = new System.Drawing.Size(143, 16);
            this.lblDefaultNewNoteColor.TabIndex = 9;
            this.lblDefaultNewNoteColor.Text = "Default skin new notes:";
            // 
            // cbxDefaultColor
            // 
            this.cbxDefaultColor.AccessibleDescription = "Defaul color for new note.";
            this.cbxDefaultColor.AccessibleRole = System.Windows.Forms.AccessibleRole.ComboBox;
            this.cbxDefaultColor.BackColor = System.Drawing.Color.LightGray;
            this.cbxDefaultColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDefaultColor.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbxDefaultColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxDefaultColor.FormattingEnabled = true;
            this.cbxDefaultColor.Location = new System.Drawing.Point(164, 143);
            this.cbxDefaultColor.MaxDropDownItems = 5;
            this.cbxDefaultColor.Name = "cbxDefaultColor";
            this.cbxDefaultColor.Size = new System.Drawing.Size(139, 24);
            this.cbxDefaultColor.TabIndex = 10;
            // 
            // numProcTransparency
            // 
            this.numProcTransparency.BackColor = System.Drawing.Color.LightGray;
            this.numProcTransparency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numProcTransparency.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numProcTransparency.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numProcTransparency.Location = new System.Drawing.Point(217, 28);
            this.numProcTransparency.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numProcTransparency.Name = "numProcTransparency";
            this.numProcTransparency.Size = new System.Drawing.Size(46, 22);
            this.numProcTransparency.TabIndex = 11;
            this.numProcTransparency.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // lblTextTransparentProcVisible
            // 
            this.lblTextTransparentProcVisible.AutoSize = true;
            this.lblTextTransparentProcVisible.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextTransparentProcVisible.Location = new System.Drawing.Point(269, 30);
            this.lblTextTransparentProcVisible.Name = "lblTextTransparentProcVisible";
            this.lblTextTransparentProcVisible.Size = new System.Drawing.Size(62, 16);
            this.lblTextTransparentProcVisible.TabIndex = 12;
            this.lblTextTransparentProcVisible.Text = "% visible";
            // 
            // tabPageFonts
            // 
            this.tabPageFonts.Controls.Add(this.cbxFontNoteTitleBold);
            this.tabPageFonts.Controls.Add(this.lblTextFontTitlePoints);
            this.tabPageFonts.Controls.Add(this.numFontSizeTitle);
            this.tabPageFonts.Controls.Add(this.lblTextFontTitleSize);
            this.tabPageFonts.Controls.Add(this.cbxFontNoteTitle);
            this.tabPageFonts.Controls.Add(this.lblTextFontTitleFamily);
            this.tabPageFonts.Controls.Add(this.lblTextDirection);
            this.tabPageFonts.Controls.Add(this.cbxTextDirection);
            this.tabPageFonts.Controls.Add(this.lblTextFontContentPoints);
            this.tabPageFonts.Controls.Add(this.numFontSizeContent);
            this.tabPageFonts.Controls.Add(this.lblTextFontContentSize);
            this.tabPageFonts.Controls.Add(this.lblTextNoteFont);
            this.tabPageFonts.Controls.Add(this.cbxFontNoteContent);
            this.tabPageFonts.Location = new System.Drawing.Point(4, 25);
            this.tabPageFonts.Name = "tabPageFonts";
            this.tabPageFonts.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFonts.Size = new System.Drawing.Size(433, 298);
            this.tabPageFonts.TabIndex = 1;
            this.tabPageFonts.Text = "Fonts";
            this.tabPageFonts.UseVisualStyleBackColor = true;
            // 
            // cbxFontNoteTitleBold
            // 
            this.cbxFontNoteTitleBold.AutoSize = true;
            this.cbxFontNoteTitleBold.Checked = true;
            this.cbxFontNoteTitleBold.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxFontNoteTitleBold.Location = new System.Drawing.Point(235, 50);
            this.cbxFontNoteTitleBold.Name = "cbxFontNoteTitleBold";
            this.cbxFontNoteTitleBold.Size = new System.Drawing.Size(54, 20);
            this.cbxFontNoteTitleBold.TabIndex = 40;
            this.cbxFontNoteTitleBold.Text = "bold";
            this.cbxFontNoteTitleBold.UseVisualStyleBackColor = true;
            // 
            // lblTextFontTitlePoints
            // 
            this.lblTextFontTitlePoints.AccessibleDescription = "points";
            this.lblTextFontTitlePoints.AutoSize = true;
            this.lblTextFontTitlePoints.Location = new System.Drawing.Point(202, 51);
            this.lblTextFontTitlePoints.Name = "lblTextFontTitlePoints";
            this.lblTextFontTitlePoints.Size = new System.Drawing.Size(22, 16);
            this.lblTextFontTitlePoints.TabIndex = 39;
            this.lblTextFontTitlePoints.Text = "pt.";
            // 
            // numFontSizeTitle
            // 
            this.numFontSizeTitle.Location = new System.Drawing.Point(158, 49);
            this.numFontSizeTitle.Maximum = new decimal(new int[] {
            96,
            0,
            0,
            0});
            this.numFontSizeTitle.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numFontSizeTitle.Name = "numFontSizeTitle";
            this.numFontSizeTitle.Size = new System.Drawing.Size(38, 22);
            this.numFontSizeTitle.TabIndex = 38;
            this.numFontSizeTitle.Value = new decimal(new int[] {
            14,
            0,
            0,
            0});
            // 
            // lblTextFontTitleSize
            // 
            this.lblTextFontTitleSize.AutoSize = true;
            this.lblTextFontTitleSize.Location = new System.Drawing.Point(61, 51);
            this.lblTextFontTitleSize.Name = "lblTextFontTitleSize";
            this.lblTextFontTitleSize.Size = new System.Drawing.Size(87, 16);
            this.lblTextFontTitleSize.TabIndex = 37;
            this.lblTextFontTitleSize.Text = "Font size title:";
            // 
            // cbxFontNoteTitle
            // 
            this.cbxFontNoteTitle.AccessibleDescription = "Font size notes";
            this.cbxFontNoteTitle.AccessibleRole = System.Windows.Forms.AccessibleRole.ComboBox;
            this.cbxFontNoteTitle.DropDownHeight = 140;
            this.cbxFontNoteTitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFontNoteTitle.IntegralHeight = false;
            this.cbxFontNoteTitle.Location = new System.Drawing.Point(157, 22);
            this.cbxFontNoteTitle.Name = "cbxFontNoteTitle";
            this.cbxFontNoteTitle.Size = new System.Drawing.Size(182, 24);
            this.cbxFontNoteTitle.TabIndex = 36;
            // 
            // lblTextFontTitleFamily
            // 
            this.lblTextFontTitleFamily.AutoSize = true;
            this.lblTextFontTitleFamily.Location = new System.Drawing.Point(91, 25);
            this.lblTextFontTitleFamily.Name = "lblTextFontTitleFamily";
            this.lblTextFontTitleFamily.Size = new System.Drawing.Size(60, 16);
            this.lblTextFontTitleFamily.TabIndex = 35;
            this.lblTextFontTitleFamily.Text = "Font title:";
            // 
            // lblTextDirection
            // 
            this.lblTextDirection.AccessibleDescription = string.Empty;
            this.lblTextDirection.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.lblTextDirection.AutoSize = true;
            this.lblTextDirection.Location = new System.Drawing.Point(21, 183);
            this.lblTextDirection.Name = "lblTextDirection";
            this.lblTextDirection.Size = new System.Drawing.Size(127, 16);
            this.lblTextDirection.TabIndex = 34;
            this.lblTextDirection.Text = "Text direction notes:";
            // 
            // cbxTextDirection
            // 
            this.cbxTextDirection.AccessibleDescription = "Text direction";
            this.cbxTextDirection.AccessibleRole = System.Windows.Forms.AccessibleRole.ComboBox;
            this.cbxTextDirection.CausesValidation = false;
            this.cbxTextDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTextDirection.FormattingEnabled = true;
            this.cbxTextDirection.Items.AddRange(new object[] {
            "Left to right",
            "Right to left"});
            this.cbxTextDirection.Location = new System.Drawing.Point(158, 180);
            this.cbxTextDirection.Name = "cbxTextDirection";
            this.cbxTextDirection.Size = new System.Drawing.Size(182, 24);
            this.cbxTextDirection.TabIndex = 33;
            // 
            // lblTextFontContentPoints
            // 
            this.lblTextFontContentPoints.AccessibleDescription = "points";
            this.lblTextFontContentPoints.AutoSize = true;
            this.lblTextFontContentPoints.Location = new System.Drawing.Point(202, 121);
            this.lblTextFontContentPoints.Name = "lblTextFontContentPoints";
            this.lblTextFontContentPoints.Size = new System.Drawing.Size(22, 16);
            this.lblTextFontContentPoints.TabIndex = 32;
            this.lblTextFontContentPoints.Text = "pt.";
            // 
            // numFontSizeContent
            // 
            this.numFontSizeContent.Location = new System.Drawing.Point(158, 119);
            this.numFontSizeContent.Maximum = new decimal(new int[] {
            96,
            0,
            0,
            0});
            this.numFontSizeContent.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numFontSizeContent.Name = "numFontSizeContent";
            this.numFontSizeContent.Size = new System.Drawing.Size(38, 22);
            this.numFontSizeContent.TabIndex = 31;
            this.numFontSizeContent.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblTextFontContentSize
            // 
            this.lblTextFontContentSize.AutoSize = true;
            this.lblTextFontContentSize.Location = new System.Drawing.Point(3, 121);
            this.lblTextFontContentSize.Name = "lblTextFontContentSize";
            this.lblTextFontContentSize.Size = new System.Drawing.Size(148, 16);
            this.lblTextFontContentSize.TabIndex = 30;
            this.lblTextFontContentSize.Text = "default font size content:";
            // 
            // lblTextNoteFont
            // 
            this.lblTextNoteFont.AutoSize = true;
            this.lblTextNoteFont.Location = new System.Drawing.Point(30, 96);
            this.lblTextNoteFont.Name = "lblTextNoteFont";
            this.lblTextNoteFont.Size = new System.Drawing.Size(121, 16);
            this.lblTextNoteFont.TabIndex = 29;
            this.lblTextNoteFont.Text = "default font content:";
            // 
            // cbxFontNoteContent
            // 
            this.cbxFontNoteContent.AccessibleDescription = "Font size notes";
            this.cbxFontNoteContent.AccessibleRole = System.Windows.Forms.AccessibleRole.ComboBox;
            this.cbxFontNoteContent.DropDownHeight = 140;
            this.cbxFontNoteContent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFontNoteContent.IntegralHeight = false;
            this.cbxFontNoteContent.Location = new System.Drawing.Point(157, 93);
            this.cbxFontNoteContent.Name = "cbxFontNoteContent";
            this.cbxFontNoteContent.Size = new System.Drawing.Size(182, 24);
            this.cbxFontNoteContent.TabIndex = 28;
            // 
            // tabPageTrayicon
            // 
            this.tabPageTrayicon.Controls.Add(this.chxUseAlternativeTrayicon);
            this.tabPageTrayicon.Controls.Add(this.lblFontsizePoints);
            this.tabPageTrayicon.Controls.Add(this.lblTextFontsizeMenu);
            this.tabPageTrayicon.Controls.Add(this.numTrayiconFontsize);
            this.tabPageTrayicon.Controls.Add(this.chxTrayiconBoldExit);
            this.tabPageTrayicon.Controls.Add(this.chxTrayiconBoldSettings);
            this.tabPageTrayicon.Controls.Add(this.chxTrayiconBoldManagenotes);
            this.tabPageTrayicon.Controls.Add(this.chxTrayiconBoldNewnote);
            this.tabPageTrayicon.Location = new System.Drawing.Point(4, 25);
            this.tabPageTrayicon.Name = "tabPageTrayicon";
            this.tabPageTrayicon.Size = new System.Drawing.Size(433, 298);
            this.tabPageTrayicon.TabIndex = 2;
            this.tabPageTrayicon.Text = "Trayicon";
            this.tabPageTrayicon.UseVisualStyleBackColor = true;
            // 
            // chxUseAlternativeTrayicon
            // 
            this.chxUseAlternativeTrayicon.AutoSize = true;
            this.chxUseAlternativeTrayicon.Location = new System.Drawing.Point(25, 175);
            this.chxUseAlternativeTrayicon.Name = "chxUseAlternativeTrayicon";
            this.chxUseAlternativeTrayicon.Size = new System.Drawing.Size(296, 20);
            this.chxUseAlternativeTrayicon.TabIndex = 7;
            this.chxUseAlternativeTrayicon.Text = "Use alternative windows7 style/white trayicon.";
            this.chxUseAlternativeTrayicon.UseVisualStyleBackColor = true;
            // 
            // lblFontsizePoints
            // 
            this.lblFontsizePoints.AutoSize = true;
            this.lblFontsizePoints.Location = new System.Drawing.Point(185, 27);
            this.lblFontsizePoints.Name = "lblFontsizePoints";
            this.lblFontsizePoints.Size = new System.Drawing.Size(22, 16);
            this.lblFontsizePoints.TabIndex = 6;
            this.lblFontsizePoints.Text = "pt.";
            // 
            // lblTextFontsizeMenu
            // 
            this.lblTextFontsizeMenu.AutoSize = true;
            this.lblTextFontsizeMenu.Location = new System.Drawing.Point(23, 27);
            this.lblTextFontsizeMenu.Name = "lblTextFontsizeMenu";
            this.lblTextFontsizeMenu.Size = new System.Drawing.Size(97, 16);
            this.lblTextFontsizeMenu.TabIndex = 5;
            this.lblTextFontsizeMenu.Text = "Fontsize  menu";
            // 
            // numTrayiconFontsize
            // 
            this.numTrayiconFontsize.DecimalPlaces = 2;
            this.numTrayiconFontsize.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numTrayiconFontsize.Location = new System.Drawing.Point(125, 25);
            this.numTrayiconFontsize.Maximum = new decimal(new int[] {
            36,
            0,
            0,
            0});
            this.numTrayiconFontsize.Minimum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numTrayiconFontsize.Name = "numTrayiconFontsize";
            this.numTrayiconFontsize.Size = new System.Drawing.Size(54, 22);
            this.numTrayiconFontsize.TabIndex = 4;
            this.numTrayiconFontsize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numTrayiconFontsize.Value = new decimal(new int[] {
            825,
            0,
            0,
            131072});
            // 
            // chxTrayiconBoldExit
            // 
            this.chxTrayiconBoldExit.AutoSize = true;
            this.chxTrayiconBoldExit.Location = new System.Drawing.Point(26, 129);
            this.chxTrayiconBoldExit.Name = "chxTrayiconBoldExit";
            this.chxTrayiconBoldExit.Size = new System.Drawing.Size(153, 20);
            this.chxTrayiconBoldExit.TabIndex = 3;
            this.chxTrayiconBoldExit.Text = "Display \"Exit\" in bold.";
            this.chxTrayiconBoldExit.UseVisualStyleBackColor = true;
            // 
            // chxTrayiconBoldSettings
            // 
            this.chxTrayiconBoldSettings.AutoSize = true;
            this.chxTrayiconBoldSettings.Location = new System.Drawing.Point(25, 106);
            this.chxTrayiconBoldSettings.Name = "chxTrayiconBoldSettings";
            this.chxTrayiconBoldSettings.Size = new System.Drawing.Size(180, 20);
            this.chxTrayiconBoldSettings.TabIndex = 2;
            this.chxTrayiconBoldSettings.Text = "Display \"Settings\" in bold.";
            this.chxTrayiconBoldSettings.UseVisualStyleBackColor = true;
            // 
            // chxTrayiconBoldManagenotes
            // 
            this.chxTrayiconBoldManagenotes.AutoSize = true;
            this.chxTrayiconBoldManagenotes.Location = new System.Drawing.Point(25, 83);
            this.chxTrayiconBoldManagenotes.Name = "chxTrayiconBoldManagenotes";
            this.chxTrayiconBoldManagenotes.Size = new System.Drawing.Size(218, 20);
            this.chxTrayiconBoldManagenotes.TabIndex = 1;
            this.chxTrayiconBoldManagenotes.Text = "Display \"Manage notes\" in bold.";
            this.chxTrayiconBoldManagenotes.UseVisualStyleBackColor = true;
            // 
            // chxTrayiconBoldNewnote
            // 
            this.chxTrayiconBoldNewnote.AutoSize = true;
            this.chxTrayiconBoldNewnote.Checked = true;
            this.chxTrayiconBoldNewnote.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxTrayiconBoldNewnote.Location = new System.Drawing.Point(25, 60);
            this.chxTrayiconBoldNewnote.Name = "chxTrayiconBoldNewnote";
            this.chxTrayiconBoldNewnote.Size = new System.Drawing.Size(239, 20);
            this.chxTrayiconBoldNewnote.TabIndex = 0;
            this.chxTrayiconBoldNewnote.Text = "Display \"Create a new note\" in bold.";
            this.chxTrayiconBoldNewnote.UseVisualStyleBackColor = true;
            // 
            // tabPlugins
            // 
            this.tabPlugins.Controls.Add(this.pluginGrid);
            this.tabPlugins.Controls.Add(this.chxLoadPlugins);
            this.tabPlugins.Location = new System.Drawing.Point(4, 25);
            this.tabPlugins.Name = "tabPlugins";
            this.tabPlugins.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlugins.Size = new System.Drawing.Size(447, 333);
            this.tabPlugins.TabIndex = 6;
            this.tabPlugins.Text = "Plugins";
            this.tabPlugins.UseVisualStyleBackColor = true;
            // 
            // chxLoadPlugins
            // 
            this.chxLoadPlugins.AccessibleDescription = "Load plugins";
            this.chxLoadPlugins.AutoSize = true;
            this.chxLoadPlugins.Checked = true;
            this.chxLoadPlugins.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxLoadPlugins.Location = new System.Drawing.Point(20, 16);
            this.chxLoadPlugins.Name = "chxLoadPlugins";
            this.chxLoadPlugins.Size = new System.Drawing.Size(104, 20);
            this.chxLoadPlugins.TabIndex = 25;
            this.chxLoadPlugins.Text = "Load plugins";
            this.chxLoadPlugins.UseVisualStyleBackColor = true;
            this.chxLoadPlugins.CheckedChanged += new System.EventHandler(this.chxLoadPlugins_CheckedChanged);
            // 
            // tabHighlight
            // 
            this.tabHighlight.Controls.Add(this.chxHighlightSQL);
            this.tabHighlight.Controls.Add(this.chxHighlightPHP);
            this.tabHighlight.Controls.Add(this.chxHighlightHyperlinks);
            this.tabHighlight.Controls.Add(this.chxConfirmLink);
            this.tabHighlight.Controls.Add(this.chxHighlightHTML);
            this.tabHighlight.Location = new System.Drawing.Point(4, 25);
            this.tabHighlight.Name = "tabHighlight";
            this.tabHighlight.Padding = new System.Windows.Forms.Padding(3);
            this.tabHighlight.Size = new System.Drawing.Size(447, 333);
            this.tabHighlight.TabIndex = 5;
            this.tabHighlight.Text = "Highlight";
            this.tabHighlight.UseVisualStyleBackColor = true;
            // 
            // chxHighlightSQL
            // 
            this.chxHighlightSQL.AutoSize = true;
            this.chxHighlightSQL.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxHighlightSQL.Location = new System.Drawing.Point(23, 159);
            this.chxHighlightSQL.Name = "chxHighlightSQL";
            this.chxHighlightSQL.Size = new System.Drawing.Size(232, 20);
            this.chxHighlightSQL.TabIndex = 16;
            this.chxHighlightSQL.Text = "Highlight SQL text between quotes.";
            this.chxHighlightSQL.UseVisualStyleBackColor = true;
            // 
            // chxHighlightPHP
            // 
            this.chxHighlightPHP.AutoSize = true;
            this.chxHighlightPHP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxHighlightPHP.Location = new System.Drawing.Point(23, 136);
            this.chxHighlightPHP.Name = "chxHighlightPHP";
            this.chxHighlightPHP.Size = new System.Drawing.Size(273, 20);
            this.chxHighlightPHP.TabIndex = 15;
            this.chxHighlightPHP.Text = "Highlight PHP text between <?php and ?>.";
            this.chxHighlightPHP.UseVisualStyleBackColor = true;
            // 
            // chxHighlightHyperlinks
            // 
            this.chxHighlightHyperlinks.AutoSize = true;
            this.chxHighlightHyperlinks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxHighlightHyperlinks.Location = new System.Drawing.Point(23, 42);
            this.chxHighlightHyperlinks.Name = "chxHighlightHyperlinks";
            this.chxHighlightHyperlinks.Size = new System.Drawing.Size(185, 20);
            this.chxHighlightHyperlinks.TabIndex = 14;
            this.chxHighlightHyperlinks.Text = "Make hyperlinks clickable.";
            this.chxHighlightHyperlinks.UseVisualStyleBackColor = true;
            // 
            // chxConfirmLink
            // 
            this.chxConfirmLink.AutoSize = true;
            this.chxConfirmLink.Checked = true;
            this.chxConfirmLink.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxConfirmLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxConfirmLink.Location = new System.Drawing.Point(23, 65);
            this.chxConfirmLink.Name = "chxConfirmLink";
            this.chxConfirmLink.Size = new System.Drawing.Size(293, 20);
            this.chxConfirmLink.TabIndex = 18;
            this.chxConfirmLink.Text = "Ask before launching URL, on click hyperlink.";
            this.chxConfirmLink.UseVisualStyleBackColor = true;
            // 
            // chxHighlightHTML
            // 
            this.chxHighlightHTML.AutoSize = true;
            this.chxHighlightHTML.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxHighlightHTML.Location = new System.Drawing.Point(23, 113);
            this.chxHighlightHTML.Name = "chxHighlightHTML";
            this.chxHighlightHTML.Size = new System.Drawing.Size(145, 20);
            this.chxHighlightHTML.TabIndex = 13;
            this.chxHighlightHTML.Text = "Highlight HTML text.";
            this.chxHighlightHTML.UseVisualStyleBackColor = true;
            // 
            // tabSharing
            // 
            this.tabSharing.Controls.Add(this.tabControlSharing);
            this.tabSharing.Location = new System.Drawing.Point(4, 25);
            this.tabSharing.Name = "tabSharing";
            this.tabSharing.Padding = new System.Windows.Forms.Padding(3);
            this.tabSharing.Size = new System.Drawing.Size(447, 333);
            this.tabSharing.TabIndex = 1;
            this.tabSharing.Text = "Sharing";
            this.tabSharing.UseVisualStyleBackColor = true;
            // 
            // tabControlSharing
            // 
            this.tabControlSharing.Controls.Add(this.tabEmail);
            this.tabControlSharing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlSharing.Location = new System.Drawing.Point(3, 3);
            this.tabControlSharing.Name = "tabControlSharing";
            this.tabControlSharing.SelectedIndex = 0;
            this.tabControlSharing.Size = new System.Drawing.Size(441, 327);
            this.tabControlSharing.TabIndex = 14;
            // 
            // tabEmail
            // 
            this.tabEmail.Controls.Add(this.chxSocialEmailEnabled);
            this.tabEmail.Controls.Add(this.chxSocialEmailDefaultaddressSet);
            this.tabEmail.Controls.Add(this.tbDefaultEmail);
            this.tabEmail.Location = new System.Drawing.Point(4, 25);
            this.tabEmail.Name = "tabEmail";
            this.tabEmail.Size = new System.Drawing.Size(433, 298);
            this.tabEmail.TabIndex = 2;
            this.tabEmail.Text = "Email";
            this.tabEmail.UseVisualStyleBackColor = true;
            // 
            // chxSocialEmailEnabled
            // 
            this.chxSocialEmailEnabled.AutoSize = true;
            this.chxSocialEmailEnabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxSocialEmailEnabled.Location = new System.Drawing.Point(14, 27);
            this.chxSocialEmailEnabled.Name = "chxSocialEmailEnabled";
            this.chxSocialEmailEnabled.Size = new System.Drawing.Size(197, 20);
            this.chxSocialEmailEnabled.TabIndex = 25;
            this.chxSocialEmailEnabled.Text = "Enable E-mail in share menu";
            this.chxSocialEmailEnabled.UseVisualStyleBackColor = true;
            // 
            // chxSocialEmailDefaultaddressSet
            // 
            this.chxSocialEmailDefaultaddressSet.AutoSize = true;
            this.chxSocialEmailDefaultaddressSet.Checked = true;
            this.chxSocialEmailDefaultaddressSet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxSocialEmailDefaultaddressSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxSocialEmailDefaultaddressSet.Location = new System.Drawing.Point(14, 69);
            this.chxSocialEmailDefaultaddressSet.Name = "chxSocialEmailDefaultaddressSet";
            this.chxSocialEmailDefaultaddressSet.Size = new System.Drawing.Size(257, 20);
            this.chxSocialEmailDefaultaddressSet.TabIndex = 24;
            this.chxSocialEmailDefaultaddressSet.Text = "Set a default email address to send to: ";
            this.chxSocialEmailDefaultaddressSet.UseVisualStyleBackColor = true;
            this.chxSocialEmailDefaultaddressSet.CheckedChanged += new System.EventHandler(this.chxSocialEmailDefaultaddressBlank_CheckedChanged);
            // 
            // tbDefaultEmail
            // 
            this.tbDefaultEmail.AccessibleDescription = "Editbox default email address";
            this.tbDefaultEmail.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.tbDefaultEmail.Enabled = false;
            this.tbDefaultEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDefaultEmail.Location = new System.Drawing.Point(34, 92);
            this.tbDefaultEmail.Name = "tbDefaultEmail";
            this.tbDefaultEmail.Size = new System.Drawing.Size(199, 22);
            this.tbDefaultEmail.TabIndex = 23;
            // 
            // tabNetwork
            // 
            this.tabNetwork.Controls.Add(this.tabControlNetwork);
            this.tabNetwork.Controls.Add(this.lblTextNetworkMiliseconds);
            this.tabNetwork.Location = new System.Drawing.Point(4, 25);
            this.tabNetwork.Name = "tabNetwork";
            this.tabNetwork.Padding = new System.Windows.Forms.Padding(3);
            this.tabNetwork.Size = new System.Drawing.Size(447, 333);
            this.tabNetwork.TabIndex = 4;
            this.tabNetwork.Text = "Network";
            this.tabNetwork.UseVisualStyleBackColor = true;
            // 
            // tabControlNetwork
            // 
            this.tabControlNetwork.Controls.Add(this.tabUpdates);
            this.tabControlNetwork.Controls.Add(this.tabProxy);
            this.tabControlNetwork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlNetwork.Location = new System.Drawing.Point(3, 3);
            this.tabControlNetwork.Name = "tabControlNetwork";
            this.tabControlNetwork.SelectedIndex = 0;
            this.tabControlNetwork.Size = new System.Drawing.Size(441, 327);
            this.tabControlNetwork.TabIndex = 33;
            // 
            // tabUpdates
            // 
            this.tabUpdates.Controls.Add(this.chxUpdateSilentInstall);
            this.tabUpdates.Controls.Add(this.btnGPGPathBrowse);
            this.tabUpdates.Controls.Add(this.tbGPGPath);
            this.tabUpdates.Controls.Add(this.lblTextGPGPath);
            this.tabUpdates.Controls.Add(this.chxCheckUpdatesSignature);
            this.tabUpdates.Controls.Add(this.chxCheckUpdates);
            this.tabUpdates.Controls.Add(this.numUpdateCheckDays);
            this.tabUpdates.Controls.Add(this.lblTextCheckforupdatesevery);
            this.tabUpdates.Controls.Add(this.lblLatestUpdateCheck);
            this.tabUpdates.Controls.Add(this.lblTextDayAtStartup);
            this.tabUpdates.Controls.Add(this.lblTextLatestUpdateCheck);
            this.tabUpdates.Controls.Add(this.btnCheckUpdates);
            this.tabUpdates.Location = new System.Drawing.Point(4, 25);
            this.tabUpdates.Name = "tabUpdates";
            this.tabUpdates.Padding = new System.Windows.Forms.Padding(3);
            this.tabUpdates.Size = new System.Drawing.Size(433, 298);
            this.tabUpdates.TabIndex = 0;
            this.tabUpdates.Text = "Updates";
            this.tabUpdates.UseVisualStyleBackColor = true;
            // 
            // chxUpdateSilentInstall
            // 
            this.chxUpdateSilentInstall.AutoSize = true;
            this.chxUpdateSilentInstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxUpdateSilentInstall.Location = new System.Drawing.Point(11, 69);
            this.chxUpdateSilentInstall.Name = "chxUpdateSilentInstall";
            this.chxUpdateSilentInstall.Size = new System.Drawing.Size(189, 20);
            this.chxUpdateSilentInstall.TabIndex = 35;
            this.chxUpdateSilentInstall.Text = "Install update setup silently.";
            this.chxUpdateSilentInstall.UseVisualStyleBackColor = true;
            // 
            // btnGPGPathBrowse
            // 
            this.btnGPGPathBrowse.BackColor = System.Drawing.Color.LightGray;
            this.btnGPGPathBrowse.Location = new System.Drawing.Point(351, 134);
            this.btnGPGPathBrowse.Name = "btnGPGPathBrowse";
            this.btnGPGPathBrowse.Size = new System.Drawing.Size(64, 25);
            this.btnGPGPathBrowse.TabIndex = 25;
            this.btnGPGPathBrowse.Text = "browse";
            this.btnGPGPathBrowse.UseVisualStyleBackColor = false;
            this.btnGPGPathBrowse.Click += new System.EventHandler(this.btnGPGPathBrowse_Click);
            // 
            // tbGPGPath
            // 
            this.tbGPGPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbGPGPath.Location = new System.Drawing.Point(128, 135);
            this.tbGPGPath.Name = "tbGPGPath";
            this.tbGPGPath.Size = new System.Drawing.Size(217, 22);
            this.tbGPGPath.TabIndex = 34;
            // 
            // lblTextGPGPath
            // 
            this.lblTextGPGPath.AutoSize = true;
            this.lblTextGPGPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextGPGPath.Location = new System.Drawing.Point(8, 138);
            this.lblTextGPGPath.Name = "lblTextGPGPath";
            this.lblTextGPGPath.Size = new System.Drawing.Size(114, 16);
            this.lblTextGPGPath.TabIndex = 33;
            this.lblTextGPGPath.Text = "Location gpg.exe:";
            // 
            // chxCheckUpdatesSignature
            // 
            this.chxCheckUpdatesSignature.AutoSize = true;
            this.chxCheckUpdatesSignature.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxCheckUpdatesSignature.Location = new System.Drawing.Point(11, 109);
            this.chxCheckUpdatesSignature.Name = "chxCheckUpdatesSignature";
            this.chxCheckUpdatesSignature.Size = new System.Drawing.Size(287, 20);
            this.chxCheckUpdatesSignature.TabIndex = 25;
            this.chxCheckUpdatesSignature.Text = "Verify the signature of downloaded updates.";
            this.chxCheckUpdatesSignature.UseVisualStyleBackColor = true;
            this.chxCheckUpdatesSignature.CheckedChanged += new System.EventHandler(this.chxCheckUpdatesSignature_CheckedChanged);
            // 
            // chxCheckUpdates
            // 
            this.chxCheckUpdates.AutoSize = true;
            this.chxCheckUpdates.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxCheckUpdates.Location = new System.Drawing.Point(11, 18);
            this.chxCheckUpdates.Name = "chxCheckUpdates";
            this.chxCheckUpdates.Size = new System.Drawing.Size(135, 20);
            this.chxCheckUpdates.TabIndex = 26;
            this.chxCheckUpdates.Text = "Check for updates";
            this.chxCheckUpdates.UseVisualStyleBackColor = true;
            this.chxCheckUpdates.CheckedChanged += new System.EventHandler(this.cbxCheckUpdates_CheckedChanged);
            // 
            // numUpdateCheckDays
            // 
            this.numUpdateCheckDays.Enabled = false;
            this.numUpdateCheckDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numUpdateCheckDays.Location = new System.Drawing.Point(188, 41);
            this.numUpdateCheckDays.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numUpdateCheckDays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpdateCheckDays.Name = "numUpdateCheckDays";
            this.numUpdateCheckDays.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numUpdateCheckDays.Size = new System.Drawing.Size(38, 22);
            this.numUpdateCheckDays.TabIndex = 28;
            this.numUpdateCheckDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numUpdateCheckDays.Value = new decimal(new int[] {
            14,
            0,
            0,
            0});
            // 
            // lblTextCheckforupdatesevery
            // 
            this.lblTextCheckforupdatesevery.AutoSize = true;
            this.lblTextCheckforupdatesevery.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextCheckforupdatesevery.Location = new System.Drawing.Point(27, 43);
            this.lblTextCheckforupdatesevery.Name = "lblTextCheckforupdatesevery";
            this.lblTextCheckforupdatesevery.Size = new System.Drawing.Size(156, 16);
            this.lblTextCheckforupdatesevery.TabIndex = 27;
            this.lblTextCheckforupdatesevery.Text = "Check for updates every ";
            // 
            // lblLatestUpdateCheck
            // 
            this.lblLatestUpdateCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLatestUpdateCheck.Location = new System.Drawing.Point(223, 193);
            this.lblLatestUpdateCheck.Name = "lblLatestUpdateCheck";
            this.lblLatestUpdateCheck.Size = new System.Drawing.Size(122, 16);
            this.lblLatestUpdateCheck.TabIndex = 32;
            // 
            // lblTextDayAtStartup
            // 
            this.lblTextDayAtStartup.AutoSize = true;
            this.lblTextDayAtStartup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextDayAtStartup.Location = new System.Drawing.Point(232, 43);
            this.lblTextDayAtStartup.Name = "lblTextDayAtStartup";
            this.lblTextDayAtStartup.Size = new System.Drawing.Size(98, 16);
            this.lblTextDayAtStartup.TabIndex = 29;
            this.lblTextDayAtStartup.Text = "days, at startup";
            // 
            // lblTextLatestUpdateCheck
            // 
            this.lblTextLatestUpdateCheck.AutoSize = true;
            this.lblTextLatestUpdateCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextLatestUpdateCheck.Location = new System.Drawing.Point(10, 193);
            this.lblTextLatestUpdateCheck.Name = "lblTextLatestUpdateCheck";
            this.lblTextLatestUpdateCheck.Size = new System.Drawing.Size(216, 16);
            this.lblTextLatestUpdateCheck.TabIndex = 31;
            this.lblTextLatestUpdateCheck.Text = "Last update check is performed on:";
            // 
            // btnCheckUpdates
            // 
            this.btnCheckUpdates.BackColor = System.Drawing.Color.LightGray;
            this.btnCheckUpdates.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCheckUpdates.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnCheckUpdates.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCheckUpdates.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckUpdates.Location = new System.Drawing.Point(118, 224);
            this.btnCheckUpdates.Name = "btnCheckUpdates";
            this.btnCheckUpdates.Size = new System.Drawing.Size(180, 23);
            this.btnCheckUpdates.TabIndex = 30;
            this.btnCheckUpdates.Text = "Check updates now";
            this.btnCheckUpdates.UseVisualStyleBackColor = false;
            this.btnCheckUpdates.Click += new System.EventHandler(this.btnCheckUpdates_Click);
            // 
            // tabProxy
            // 
            this.tabProxy.Controls.Add(this.lblTextMiliseconds);
            this.tabProxy.Controls.Add(this.numTimeout);
            this.tabProxy.Controls.Add(this.chxProxyEnabled);
            this.tabProxy.Controls.Add(this.lblTextNetworkTimeout);
            this.tabProxy.Controls.Add(this.iptbProxyAddress);
            this.tabProxy.Location = new System.Drawing.Point(4, 25);
            this.tabProxy.Name = "tabProxy";
            this.tabProxy.Padding = new System.Windows.Forms.Padding(3);
            this.tabProxy.Size = new System.Drawing.Size(433, 298);
            this.tabProxy.TabIndex = 1;
            this.tabProxy.Text = "Proxy";
            this.tabProxy.UseVisualStyleBackColor = true;
            // 
            // lblTextMiliseconds
            // 
            this.lblTextMiliseconds.AccessibleDescription = "Miliseconds";
            this.lblTextMiliseconds.AutoSize = true;
            this.lblTextMiliseconds.Location = new System.Drawing.Point(228, 113);
            this.lblTextMiliseconds.Name = "lblTextMiliseconds";
            this.lblTextMiliseconds.Size = new System.Drawing.Size(26, 16);
            this.lblTextMiliseconds.TabIndex = 25;
            this.lblTextMiliseconds.Text = "ms";
            // 
            // numTimeout
            // 
            this.numTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numTimeout.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numTimeout.Location = new System.Drawing.Point(163, 111);
            this.numTimeout.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.numTimeout.Minimum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numTimeout.Name = "numTimeout";
            this.numTimeout.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numTimeout.Size = new System.Drawing.Size(58, 22);
            this.numTimeout.TabIndex = 23;
            this.numTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numTimeout.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // chxProxyEnabled
            // 
            this.chxProxyEnabled.AutoSize = true;
            this.chxProxyEnabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxProxyEnabled.Location = new System.Drawing.Point(16, 31);
            this.chxProxyEnabled.Name = "chxProxyEnabled";
            this.chxProxyEnabled.Size = new System.Drawing.Size(136, 20);
            this.chxProxyEnabled.TabIndex = 1;
            this.chxProxyEnabled.Text = "Use socked proxy";
            this.chxProxyEnabled.UseVisualStyleBackColor = true;
            this.chxProxyEnabled.CheckedChanged += new System.EventHandler(this.chxUseProxy_CheckedChanged);
            // 
            // lblTextNetworkTimeout
            // 
            this.lblTextNetworkTimeout.AutoSize = true;
            this.lblTextNetworkTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextNetworkTimeout.Location = new System.Drawing.Point(15, 113);
            this.lblTextNetworkTimeout.Name = "lblTextNetworkTimeout";
            this.lblTextNetworkTimeout.Size = new System.Drawing.Size(150, 16);
            this.lblTextNetworkTimeout.TabIndex = 24;
            this.lblTextNetworkTimeout.Text = "connection timeout time:";
            // 
            // lblTextNetworkMiliseconds
            // 
            this.lblTextNetworkMiliseconds.AutoSize = true;
            this.lblTextNetworkMiliseconds.Location = new System.Drawing.Point(164, 141);
            this.lblTextNetworkMiliseconds.Name = "lblTextNetworkMiliseconds";
            this.lblTextNetworkMiliseconds.Size = new System.Drawing.Size(26, 16);
            this.lblTextNetworkMiliseconds.TabIndex = 25;
            this.lblTextNetworkMiliseconds.Text = "ms";
            // 
            // tabAdvance
            // 
            this.tabAdvance.Controls.Add(this.chxLogExceptions);
            this.tabAdvance.Controls.Add(this.lblTextLogging);
            this.tabAdvance.Controls.Add(this.chxLogDebug);
            this.tabAdvance.Controls.Add(this.btnResetSettings);
            this.tabAdvance.Controls.Add(this.chxLogErrors);
            this.tabAdvance.Controls.Add(this.btnBrowse);
            this.tabAdvance.Controls.Add(this.lblTextNoteLocation);
            this.tabAdvance.Controls.Add(this.tbNotesSavePath);
            this.tabAdvance.Location = new System.Drawing.Point(4, 25);
            this.tabAdvance.Name = "tabAdvance";
            this.tabAdvance.Size = new System.Drawing.Size(447, 333);
            this.tabAdvance.TabIndex = 2;
            this.tabAdvance.Text = "Advance";
            this.tabAdvance.UseVisualStyleBackColor = true;
            // 
            // chxLogExceptions
            // 
            this.chxLogExceptions.AutoSize = true;
            this.chxLogExceptions.Checked = true;
            this.chxLogExceptions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxLogExceptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxLogExceptions.Location = new System.Drawing.Point(14, 161);
            this.chxLogExceptions.Name = "chxLogExceptions";
            this.chxLogExceptions.Size = new System.Drawing.Size(306, 20);
            this.chxLogExceptions.TabIndex = 24;
            this.chxLogExceptions.Text = "Log exceptions (recommeded: leave enabled).";
            this.chxLogExceptions.UseVisualStyleBackColor = true;
            // 
            // lblTextLogging
            // 
            this.lblTextLogging.AutoSize = true;
            this.lblTextLogging.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextLogging.Location = new System.Drawing.Point(11, 96);
            this.lblTextLogging.Name = "lblTextLogging";
            this.lblTextLogging.Size = new System.Drawing.Size(60, 16);
            this.lblTextLogging.TabIndex = 23;
            this.lblTextLogging.Text = "Logging:";
            // 
            // chxLogDebug
            // 
            this.chxLogDebug.AutoSize = true;
            this.chxLogDebug.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxLogDebug.Location = new System.Drawing.Point(14, 115);
            this.chxLogDebug.Name = "chxLogDebug";
            this.chxLogDebug.Size = new System.Drawing.Size(119, 20);
            this.chxLogDebug.TabIndex = 22;
            this.chxLogDebug.Text = "Log debug info.";
            this.chxLogDebug.UseVisualStyleBackColor = true;
            // 
            // btnResetSettings
            // 
            this.btnResetSettings.BackColor = System.Drawing.Color.LightGray;
            this.btnResetSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetSettings.Location = new System.Drawing.Point(112, 240);
            this.btnResetSettings.Name = "btnResetSettings";
            this.btnResetSettings.Size = new System.Drawing.Size(195, 25);
            this.btnResetSettings.TabIndex = 21;
            this.btnResetSettings.Text = "&Reset all settings to default";
            this.btnResetSettings.UseVisualStyleBackColor = false;
            this.btnResetSettings.Click += new System.EventHandler(this.btnResetSettings_Click);
            // 
            // chxLogErrors
            // 
            this.chxLogErrors.AutoSize = true;
            this.chxLogErrors.Checked = true;
            this.chxLogErrors.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxLogErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxLogErrors.Location = new System.Drawing.Point(14, 138);
            this.chxLogErrors.Name = "chxLogErrors";
            this.chxLogErrors.Size = new System.Drawing.Size(91, 20);
            this.chxLogErrors.TabIndex = 19;
            this.chxLogErrors.Text = "Log errors.";
            this.chxLogErrors.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.LightGray;
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(369, 50);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(68, 25);
            this.btnBrowse.TabIndex = 15;
            this.btnBrowse.Text = "browse";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblTextNoteLocation
            // 
            this.lblTextNoteLocation.AutoSize = true;
            this.lblTextNoteLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextNoteLocation.Location = new System.Drawing.Point(13, 32);
            this.lblTextNoteLocation.Name = "lblTextNoteLocation";
            this.lblTextNoteLocation.Size = new System.Drawing.Size(92, 16);
            this.lblTextNoteLocation.TabIndex = 16;
            this.lblTextNoteLocation.Text = "Save notes in:";
            // 
            // tbNotesSavePath
            // 
            this.tbNotesSavePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNotesSavePath.Location = new System.Drawing.Point(15, 53);
            this.tbNotesSavePath.Name = "tbNotesSavePath";
            this.tbNotesSavePath.Size = new System.Drawing.Size(348, 22);
            this.tbNotesSavePath.TabIndex = 14;
            // 
            // chxSettingsExpertEnabled
            // 
            this.chxSettingsExpertEnabled.AccessibleDescription = "Show expert settings";
            this.chxSettingsExpertEnabled.AutoSize = true;
            this.chxSettingsExpertEnabled.Checked = true;
            this.chxSettingsExpertEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxSettingsExpertEnabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxSettingsExpertEnabled.Location = new System.Drawing.Point(24, 371);
            this.chxSettingsExpertEnabled.Name = "chxSettingsExpertEnabled";
            this.chxSettingsExpertEnabled.Size = new System.Drawing.Size(114, 20);
            this.chxSettingsExpertEnabled.TabIndex = 25;
            this.chxSettingsExpertEnabled.Text = "E&xpert settings";
            this.chxSettingsExpertEnabled.UseVisualStyleBackColor = true;
            this.chxSettingsExpertEnabled.CheckedChanged += new System.EventHandler(this.cbxShowExpertSettings_CheckedChanged);
            // 
            // folderBrowseDialogNotessavepath
            // 
            this.folderBrowseDialogNotessavepath.Description = "Select a folder to store the all the notes files in";
            this.folderBrowseDialogNotessavepath.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // openFileDialogBrowseGPG
            // 
            this.openFileDialogBrowseGPG.AddExtension = false;
            this.openFileDialogBrowseGPG.DefaultExt = "exe";
            this.openFileDialogBrowseGPG.FileName = "gpg.exe";
            this.openFileDialogBrowseGPG.Title = "Select path to gpg.exe";
            // 
            // pluginGrid
            // 
            this.pluginGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pluginGrid.AutoScroll = true;
            this.pluginGrid.Location = new System.Drawing.Point(3, 42);
            this.pluginGrid.Name = "pluginGrid";
            this.pluginGrid.Size = new System.Drawing.Size(441, 288);
            this.pluginGrid.TabIndex = 26;
            // 
            // iptbProxyAddress
            // 
            this.iptbProxyAddress.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.iptbProxyAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.iptbProxyAddress.Enabled = false;
            this.iptbProxyAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iptbProxyAddress.IPAddress = "0.0.0.0";
            this.iptbProxyAddress.Location = new System.Drawing.Point(15, 58);
            this.iptbProxyAddress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.iptbProxyAddress.Name = "iptbProxyAddress";
            this.iptbProxyAddress.Size = new System.Drawing.Size(228, 20);
            this.iptbProxyAddress.TabIndex = 19;
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(455, 398);
            this.Controls.Add(this.chxSettingsExpertEnabled);
            this.Controls.Add(this.tabControlSettings);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(360, 240);
            this.Name = "FrmSettings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Settings";
            this.tabControlSettings.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.tabAppearance.ResumeLayout(false);
            this.tabAppearanceColors.ResumeLayout(false);
            this.tabPageLooks.ResumeLayout(false);
            this.tabPageLooks.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numProcTransparency)).EndInit();
            this.tabPageFonts.ResumeLayout(false);
            this.tabPageFonts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSizeTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSizeContent)).EndInit();
            this.tabPageTrayicon.ResumeLayout(false);
            this.tabPageTrayicon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTrayiconFontsize)).EndInit();
            this.tabPlugins.ResumeLayout(false);
            this.tabPlugins.PerformLayout();
            this.tabHighlight.ResumeLayout(false);
            this.tabHighlight.PerformLayout();
            this.tabSharing.ResumeLayout(false);
            this.tabControlSharing.ResumeLayout(false);
            this.tabEmail.ResumeLayout(false);
            this.tabEmail.PerformLayout();
            this.tabNetwork.ResumeLayout(false);
            this.tabNetwork.PerformLayout();
            this.tabControlNetwork.ResumeLayout(false);
            this.tabUpdates.ResumeLayout(false);
            this.tabUpdates.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpdateCheckDays)).EndInit();
            this.tabProxy.ResumeLayout(false);
            this.tabProxy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).EndInit();
            this.tabAdvance.ResumeLayout(false);
            this.tabAdvance.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion        
    }
}