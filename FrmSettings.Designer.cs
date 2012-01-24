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
        private System.Windows.Forms.TabControl tabctrlAppearance;

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
            this.chxLoadPlugins = new System.Windows.Forms.CheckBox();
            this.chxConfirmDeletenote = new System.Windows.Forms.CheckBox();
            this.cbxActionLeftclick = new System.Windows.Forms.ComboBox();
            this.chxConfirmExit = new System.Windows.Forms.CheckBox();
            this.chxStartOnLogin = new System.Windows.Forms.CheckBox();
            this.lblTextActionLeftClicktTrayicon = new System.Windows.Forms.Label();
            this.tabAppearance = new System.Windows.Forms.TabPage();
            this.tabctrlAppearance = new System.Windows.Forms.TabControl();
            this.tabAppearanceGeneral = new System.Windows.Forms.TabPage();
            this.chxShowTooltips = new System.Windows.Forms.CheckBox();
            this.chxTransparecy = new System.Windows.Forms.CheckBox();
            this.numProcTransparency = new System.Windows.Forms.NumericUpDown();
            this.lblTextTransparentProcVisible = new System.Windows.Forms.Label();
            this.tabPageNewNote = new System.Windows.Forms.TabPage();
            this.chxUseDateAsDefaultTitle = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numNotesDefaultHeight = new System.Windows.Forms.NumericUpDown();
            this.numNotesDefaultWidth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.chxUseRandomDefaultNote = new System.Windows.Forms.CheckBox();
            this.lblDefaultNewNoteColor = new System.Windows.Forms.Label();
            this.cbxDefaultSkin = new System.Windows.Forms.ComboBox();
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
            this.tabAppereanceManagenotes = new System.Windows.Forms.TabPage();
            this.chxCaseSentiveSearch = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numManagenotesFont = new System.Windows.Forms.NumericUpDown();
            this.lbTextManagesnotesFontSize = new System.Windows.Forms.Label();
            this.lblTextSkinManagenotes = new System.Windows.Forms.Label();
            this.cbxManageNotesSkin = new System.Windows.Forms.ComboBox();
            this.cbxManagenotesTooltipContent = new System.Windows.Forms.CheckBox();
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
            this.chxForceUseIPv6 = new System.Windows.Forms.CheckBox();
            this.lblTextMiliseconds = new System.Windows.Forms.Label();
            this.numTimeout = new System.Windows.Forms.NumericUpDown();
            this.chxProxyEnabled = new System.Windows.Forms.CheckBox();
            this.lblTextNetworkTimeout = new System.Windows.Forms.Label();
            this.iptbProxy = new NoteFly.IPTextBox();
            this.lblTextNetworkMiliseconds = new System.Windows.Forms.Label();
            this.tabAdvance = new System.Windows.Forms.TabPage();
            this.numWarnLimitVisible = new System.Windows.Forms.NumericUpDown();
            this.lblTextVisibleNotesWarnLimit = new System.Windows.Forms.Label();
            this.lblTextTotalNotesWarnLimit = new System.Windows.Forms.Label();
            this.numWarnLimitTotal = new System.Windows.Forms.NumericUpDown();
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
            this.tabControlSettings.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.tabAppearance.SuspendLayout();
            this.tabctrlAppearance.SuspendLayout();
            this.tabAppearanceGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numProcTransparency)).BeginInit();
            this.tabPageNewNote.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNotesDefaultHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNotesDefaultWidth)).BeginInit();
            this.tabPageFonts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSizeTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSizeContent)).BeginInit();
            this.tabPageTrayicon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTrayiconFontsize)).BeginInit();
            this.tabAppereanceManagenotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numManagenotesFont)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.numWarnLimitVisible)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWarnLimitTotal)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.AccessibleDescription = null;
            this.btnOK.AccessibleName = null;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.BackColor = System.Drawing.Color.LightGray;
            this.btnOK.BackgroundImage = null;
            this.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnOK.Name = "btnOK";
            this.btnOK.UseCompatibleTextRendering = true;
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleDescription = null;
            this.btnCancel.AccessibleName = null;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.BackColor = System.Drawing.Color.LightGray;
            this.btnCancel.BackgroundImage = null;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseCompatibleTextRendering = true;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.AccessibleDescription = null;
            this.tabControlSettings.AccessibleName = null;
            resources.ApplyResources(this.tabControlSettings, "tabControlSettings");
            this.tabControlSettings.BackgroundImage = null;
            this.tabControlSettings.Controls.Add(this.tabGeneral);
            this.tabControlSettings.Controls.Add(this.tabAppearance);
            this.tabControlSettings.Controls.Add(this.tabHighlight);
            this.tabControlSettings.Controls.Add(this.tabSharing);
            this.tabControlSettings.Controls.Add(this.tabNetwork);
            this.tabControlSettings.Controls.Add(this.tabAdvance);
            this.tabControlSettings.HotTrack = true;
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            this.tabControlSettings.SelectedIndexChanged += new System.EventHandler(this.tabControlSettings_SelectedIndexChanged);
            // 
            // tabGeneral
            // 
            this.tabGeneral.AccessibleDescription = null;
            this.tabGeneral.AccessibleName = null;
            resources.ApplyResources(this.tabGeneral, "tabGeneral");
            this.tabGeneral.BackgroundImage = null;
            this.tabGeneral.Controls.Add(this.chxNotesDeleteRecyclebin);
            this.tabGeneral.Controls.Add(this.chxLoadPlugins);
            this.tabGeneral.Controls.Add(this.chxConfirmDeletenote);
            this.tabGeneral.Controls.Add(this.cbxActionLeftclick);
            this.tabGeneral.Controls.Add(this.chxConfirmExit);
            this.tabGeneral.Controls.Add(this.chxStartOnLogin);
            this.tabGeneral.Controls.Add(this.lblTextActionLeftClicktTrayicon);
            this.tabGeneral.Font = null;
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // chxNotesDeleteRecyclebin
            // 
            this.chxNotesDeleteRecyclebin.AccessibleDescription = null;
            this.chxNotesDeleteRecyclebin.AccessibleName = null;
            resources.ApplyResources(this.chxNotesDeleteRecyclebin, "chxNotesDeleteRecyclebin");
            this.chxNotesDeleteRecyclebin.BackgroundImage = null;
            this.chxNotesDeleteRecyclebin.Name = "chxNotesDeleteRecyclebin";
            this.chxNotesDeleteRecyclebin.UseCompatibleTextRendering = true;
            this.chxNotesDeleteRecyclebin.UseVisualStyleBackColor = true;
            // 
            // chxLoadPlugins
            // 
            resources.ApplyResources(this.chxLoadPlugins, "chxLoadPlugins");
            this.chxLoadPlugins.AccessibleName = null;
            this.chxLoadPlugins.BackgroundImage = null;
            this.chxLoadPlugins.Checked = true;
            this.chxLoadPlugins.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxLoadPlugins.Font = null;
            this.chxLoadPlugins.Name = "chxLoadPlugins";
            this.chxLoadPlugins.UseCompatibleTextRendering = true;
            this.chxLoadPlugins.UseVisualStyleBackColor = true;
            // 
            // chxConfirmDeletenote
            // 
            this.chxConfirmDeletenote.AccessibleDescription = null;
            this.chxConfirmDeletenote.AccessibleName = null;
            resources.ApplyResources(this.chxConfirmDeletenote, "chxConfirmDeletenote");
            this.chxConfirmDeletenote.BackgroundImage = null;
            this.chxConfirmDeletenote.Checked = true;
            this.chxConfirmDeletenote.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxConfirmDeletenote.Name = "chxConfirmDeletenote";
            this.chxConfirmDeletenote.UseCompatibleTextRendering = true;
            this.chxConfirmDeletenote.UseVisualStyleBackColor = true;
            // 
            // cbxActionLeftclick
            // 
            this.cbxActionLeftclick.AccessibleDescription = null;
            this.cbxActionLeftclick.AccessibleName = null;
            resources.ApplyResources(this.cbxActionLeftclick, "cbxActionLeftclick");
            this.cbxActionLeftclick.AutoCompleteCustomSource.AddRange(new string[] {
            resources.GetString("cbxActionLeftclick.AutoCompleteCustomSource"),
            resources.GetString("cbxActionLeftclick.AutoCompleteCustomSource1"),
            resources.GetString("cbxActionLeftclick.AutoCompleteCustomSource2"),
            resources.GetString("cbxActionLeftclick.AutoCompleteCustomSource3")});
            this.cbxActionLeftclick.BackgroundImage = null;
            this.cbxActionLeftclick.CausesValidation = false;
            this.cbxActionLeftclick.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxActionLeftclick.Font = null;
            this.cbxActionLeftclick.FormattingEnabled = true;
            this.cbxActionLeftclick.Items.AddRange(new object[] {
            resources.GetString("cbxActionLeftclick.Items"),
            resources.GetString("cbxActionLeftclick.Items1"),
            resources.GetString("cbxActionLeftclick.Items2")});
            this.cbxActionLeftclick.Name = "cbxActionLeftclick";
            // 
            // chxConfirmExit
            // 
            this.chxConfirmExit.AccessibleDescription = null;
            this.chxConfirmExit.AccessibleName = null;
            resources.ApplyResources(this.chxConfirmExit, "chxConfirmExit");
            this.chxConfirmExit.BackgroundImage = null;
            this.chxConfirmExit.Name = "chxConfirmExit";
            this.chxConfirmExit.UseCompatibleTextRendering = true;
            this.chxConfirmExit.UseVisualStyleBackColor = true;
            // 
            // chxStartOnLogin
            // 
            this.chxStartOnLogin.AccessibleDescription = null;
            this.chxStartOnLogin.AccessibleName = null;
            resources.ApplyResources(this.chxStartOnLogin, "chxStartOnLogin");
            this.chxStartOnLogin.BackgroundImage = null;
            this.chxStartOnLogin.Name = "chxStartOnLogin";
            this.chxStartOnLogin.UseCompatibleTextRendering = true;
            this.chxStartOnLogin.UseVisualStyleBackColor = true;
            // 
            // lblTextActionLeftClicktTrayicon
            // 
            this.lblTextActionLeftClicktTrayicon.AccessibleDescription = null;
            this.lblTextActionLeftClicktTrayicon.AccessibleName = null;
            resources.ApplyResources(this.lblTextActionLeftClicktTrayicon, "lblTextActionLeftClicktTrayicon");
            this.lblTextActionLeftClicktTrayicon.Name = "lblTextActionLeftClicktTrayicon";
            this.lblTextActionLeftClicktTrayicon.UseCompatibleTextRendering = true;
            // 
            // tabAppearance
            // 
            this.tabAppearance.AccessibleDescription = null;
            this.tabAppearance.AccessibleName = null;
            resources.ApplyResources(this.tabAppearance, "tabAppearance");
            this.tabAppearance.BackgroundImage = null;
            this.tabAppearance.Controls.Add(this.tabctrlAppearance);
            this.tabAppearance.Font = null;
            this.tabAppearance.Name = "tabAppearance";
            this.tabAppearance.UseVisualStyleBackColor = true;
            // 
            // tabctrlAppearance
            // 
            this.tabctrlAppearance.AccessibleDescription = null;
            this.tabctrlAppearance.AccessibleName = null;
            resources.ApplyResources(this.tabctrlAppearance, "tabctrlAppearance");
            this.tabctrlAppearance.BackgroundImage = null;
            this.tabctrlAppearance.Controls.Add(this.tabAppearanceGeneral);
            this.tabctrlAppearance.Controls.Add(this.tabPageNewNote);
            this.tabctrlAppearance.Controls.Add(this.tabPageFonts);
            this.tabctrlAppearance.Controls.Add(this.tabPageTrayicon);
            this.tabctrlAppearance.Controls.Add(this.tabAppereanceManagenotes);
            this.tabctrlAppearance.Font = null;
            this.tabctrlAppearance.MinimumSize = new System.Drawing.Size(80, 21);
            this.tabctrlAppearance.Name = "tabctrlAppearance";
            this.tabctrlAppearance.SelectedIndex = 0;
            this.tabctrlAppearance.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            // 
            // tabAppearanceGeneral
            // 
            this.tabAppearanceGeneral.AccessibleDescription = null;
            this.tabAppearanceGeneral.AccessibleName = null;
            resources.ApplyResources(this.tabAppearanceGeneral, "tabAppearanceGeneral");
            this.tabAppearanceGeneral.BackgroundImage = null;
            this.tabAppearanceGeneral.Controls.Add(this.chxShowTooltips);
            this.tabAppearanceGeneral.Controls.Add(this.chxTransparecy);
            this.tabAppearanceGeneral.Controls.Add(this.numProcTransparency);
            this.tabAppearanceGeneral.Controls.Add(this.lblTextTransparentProcVisible);
            this.tabAppearanceGeneral.Font = null;
            this.tabAppearanceGeneral.Name = "tabAppearanceGeneral";
            this.tabAppearanceGeneral.UseVisualStyleBackColor = true;
            // 
            // chxShowTooltips
            // 
            this.chxShowTooltips.AccessibleDescription = null;
            this.chxShowTooltips.AccessibleName = null;
            resources.ApplyResources(this.chxShowTooltips, "chxShowTooltips");
            this.chxShowTooltips.BackgroundImage = null;
            this.chxShowTooltips.Name = "chxShowTooltips";
            this.chxShowTooltips.UseCompatibleTextRendering = true;
            this.chxShowTooltips.UseVisualStyleBackColor = true;
            // 
            // chxTransparecy
            // 
            this.chxTransparecy.AccessibleDescription = null;
            this.chxTransparecy.AccessibleName = null;
            resources.ApplyResources(this.chxTransparecy, "chxTransparecy");
            this.chxTransparecy.BackColor = System.Drawing.Color.Transparent;
            this.chxTransparecy.BackgroundImage = null;
            this.chxTransparecy.CausesValidation = false;
            this.chxTransparecy.Checked = true;
            this.chxTransparecy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxTransparecy.Name = "chxTransparecy";
            this.chxTransparecy.UseCompatibleTextRendering = true;
            this.chxTransparecy.UseVisualStyleBackColor = false;
            this.chxTransparecy.CheckedChanged += new System.EventHandler(this.chxTransparecy_CheckedChanged);
            // 
            // numProcTransparency
            // 
            this.numProcTransparency.AccessibleDescription = null;
            this.numProcTransparency.AccessibleName = null;
            resources.ApplyResources(this.numProcTransparency, "numProcTransparency");
            this.numProcTransparency.BackColor = System.Drawing.Color.LightGray;
            this.numProcTransparency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numProcTransparency.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numProcTransparency.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numProcTransparency.Name = "numProcTransparency";
            this.numProcTransparency.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // lblTextTransparentProcVisible
            // 
            this.lblTextTransparentProcVisible.AccessibleDescription = null;
            this.lblTextTransparentProcVisible.AccessibleName = null;
            resources.ApplyResources(this.lblTextTransparentProcVisible, "lblTextTransparentProcVisible");
            this.lblTextTransparentProcVisible.Name = "lblTextTransparentProcVisible";
            this.lblTextTransparentProcVisible.UseCompatibleTextRendering = true;
            // 
            // tabPageNewNote
            // 
            this.tabPageNewNote.AccessibleDescription = null;
            this.tabPageNewNote.AccessibleName = null;
            resources.ApplyResources(this.tabPageNewNote, "tabPageNewNote");
            this.tabPageNewNote.BackgroundImage = null;
            this.tabPageNewNote.Controls.Add(this.chxUseDateAsDefaultTitle);
            this.tabPageNewNote.Controls.Add(this.label3);
            this.tabPageNewNote.Controls.Add(this.label2);
            this.tabPageNewNote.Controls.Add(this.numNotesDefaultHeight);
            this.tabPageNewNote.Controls.Add(this.numNotesDefaultWidth);
            this.tabPageNewNote.Controls.Add(this.label1);
            this.tabPageNewNote.Controls.Add(this.chxUseRandomDefaultNote);
            this.tabPageNewNote.Controls.Add(this.lblDefaultNewNoteColor);
            this.tabPageNewNote.Controls.Add(this.cbxDefaultSkin);
            this.tabPageNewNote.Font = null;
            this.tabPageNewNote.Name = "tabPageNewNote";
            this.tabPageNewNote.UseVisualStyleBackColor = true;
            // 
            // chxUseDateAsDefaultTitle
            // 
            this.chxUseDateAsDefaultTitle.AccessibleDescription = null;
            this.chxUseDateAsDefaultTitle.AccessibleName = null;
            resources.ApplyResources(this.chxUseDateAsDefaultTitle, "chxUseDateAsDefaultTitle");
            this.chxUseDateAsDefaultTitle.BackgroundImage = null;
            this.chxUseDateAsDefaultTitle.Checked = true;
            this.chxUseDateAsDefaultTitle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxUseDateAsDefaultTitle.Font = null;
            this.chxUseDateAsDefaultTitle.Name = "chxUseDateAsDefaultTitle";
            this.chxUseDateAsDefaultTitle.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AccessibleDescription = null;
            this.label3.AccessibleName = null;
            resources.ApplyResources(this.label3, "label3");
            this.label3.Font = null;
            this.label3.Name = "label3";
            this.label3.UseCompatibleTextRendering = true;
            // 
            // label2
            // 
            this.label2.AccessibleDescription = null;
            this.label2.AccessibleName = null;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Font = null;
            this.label2.Name = "label2";
            this.label2.UseCompatibleTextRendering = true;
            // 
            // numNotesDefaultHeight
            // 
            this.numNotesDefaultHeight.AccessibleDescription = null;
            this.numNotesDefaultHeight.AccessibleName = null;
            resources.ApplyResources(this.numNotesDefaultHeight, "numNotesDefaultHeight");
            this.numNotesDefaultHeight.Font = null;
            this.numNotesDefaultHeight.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numNotesDefaultHeight.Maximum = new decimal(new int[] {
            900,
            0,
            0,
            0});
            this.numNotesDefaultHeight.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numNotesDefaultHeight.Name = "numNotesDefaultHeight";
            this.numNotesDefaultHeight.Value = new decimal(new int[] {
            360,
            0,
            0,
            0});
            // 
            // numNotesDefaultWidth
            // 
            this.numNotesDefaultWidth.AccessibleDescription = null;
            this.numNotesDefaultWidth.AccessibleName = null;
            resources.ApplyResources(this.numNotesDefaultWidth, "numNotesDefaultWidth");
            this.numNotesDefaultWidth.Font = null;
            this.numNotesDefaultWidth.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numNotesDefaultWidth.Maximum = new decimal(new int[] {
            1200,
            0,
            0,
            0});
            this.numNotesDefaultWidth.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numNotesDefaultWidth.Name = "numNotesDefaultWidth";
            this.numNotesDefaultWidth.Value = new decimal(new int[] {
            420,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Font = null;
            this.label1.Name = "label1";
            this.label1.UseCompatibleTextRendering = true;
            // 
            // chxUseRandomDefaultNote
            // 
            this.chxUseRandomDefaultNote.AccessibleDescription = null;
            this.chxUseRandomDefaultNote.AccessibleName = null;
            resources.ApplyResources(this.chxUseRandomDefaultNote, "chxUseRandomDefaultNote");
            this.chxUseRandomDefaultNote.BackgroundImage = null;
            this.chxUseRandomDefaultNote.Name = "chxUseRandomDefaultNote";
            this.chxUseRandomDefaultNote.UseCompatibleTextRendering = true;
            this.chxUseRandomDefaultNote.UseVisualStyleBackColor = true;
            this.chxUseRandomDefaultNote.CheckedChanged += new System.EventHandler(this.chxUseRandomDefaultNote_CheckedChanged);
            // 
            // lblDefaultNewNoteColor
            // 
            this.lblDefaultNewNoteColor.AccessibleDescription = null;
            this.lblDefaultNewNoteColor.AccessibleName = null;
            resources.ApplyResources(this.lblDefaultNewNoteColor, "lblDefaultNewNoteColor");
            this.lblDefaultNewNoteColor.Name = "lblDefaultNewNoteColor";
            this.lblDefaultNewNoteColor.UseCompatibleTextRendering = true;
            // 
            // cbxDefaultSkin
            // 
            resources.ApplyResources(this.cbxDefaultSkin, "cbxDefaultSkin");
            this.cbxDefaultSkin.AccessibleName = null;
            this.cbxDefaultSkin.AccessibleRole = System.Windows.Forms.AccessibleRole.ComboBox;
            this.cbxDefaultSkin.BackColor = System.Drawing.Color.LightGray;
            this.cbxDefaultSkin.BackgroundImage = null;
            this.cbxDefaultSkin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDefaultSkin.FormattingEnabled = true;
            this.cbxDefaultSkin.Name = "cbxDefaultSkin";
            // 
            // tabPageFonts
            // 
            this.tabPageFonts.AccessibleDescription = null;
            this.tabPageFonts.AccessibleName = null;
            resources.ApplyResources(this.tabPageFonts, "tabPageFonts");
            this.tabPageFonts.BackgroundImage = null;
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
            this.tabPageFonts.Font = null;
            this.tabPageFonts.Name = "tabPageFonts";
            this.tabPageFonts.UseVisualStyleBackColor = true;
            // 
            // cbxFontNoteTitleBold
            // 
            this.cbxFontNoteTitleBold.AccessibleDescription = null;
            this.cbxFontNoteTitleBold.AccessibleName = null;
            resources.ApplyResources(this.cbxFontNoteTitleBold, "cbxFontNoteTitleBold");
            this.cbxFontNoteTitleBold.BackgroundImage = null;
            this.cbxFontNoteTitleBold.Checked = true;
            this.cbxFontNoteTitleBold.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxFontNoteTitleBold.Font = null;
            this.cbxFontNoteTitleBold.Name = "cbxFontNoteTitleBold";
            this.cbxFontNoteTitleBold.UseCompatibleTextRendering = true;
            this.cbxFontNoteTitleBold.UseVisualStyleBackColor = true;
            // 
            // lblTextFontTitlePoints
            // 
            resources.ApplyResources(this.lblTextFontTitlePoints, "lblTextFontTitlePoints");
            this.lblTextFontTitlePoints.AccessibleName = null;
            this.lblTextFontTitlePoints.Font = null;
            this.lblTextFontTitlePoints.Name = "lblTextFontTitlePoints";
            this.lblTextFontTitlePoints.UseCompatibleTextRendering = true;
            // 
            // numFontSizeTitle
            // 
            this.numFontSizeTitle.AccessibleDescription = null;
            this.numFontSizeTitle.AccessibleName = null;
            resources.ApplyResources(this.numFontSizeTitle, "numFontSizeTitle");
            this.numFontSizeTitle.Font = null;
            this.numFontSizeTitle.Maximum = new decimal(new int[] {
            96,
            0,
            0,
            0});
            this.numFontSizeTitle.Minimum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numFontSizeTitle.Name = "numFontSizeTitle";
            this.numFontSizeTitle.Value = new decimal(new int[] {
            14,
            0,
            0,
            0});
            // 
            // lblTextFontTitleSize
            // 
            this.lblTextFontTitleSize.AccessibleDescription = null;
            this.lblTextFontTitleSize.AccessibleName = null;
            resources.ApplyResources(this.lblTextFontTitleSize, "lblTextFontTitleSize");
            this.lblTextFontTitleSize.Font = null;
            this.lblTextFontTitleSize.Name = "lblTextFontTitleSize";
            this.lblTextFontTitleSize.UseCompatibleTextRendering = true;
            // 
            // cbxFontNoteTitle
            // 
            resources.ApplyResources(this.cbxFontNoteTitle, "cbxFontNoteTitle");
            this.cbxFontNoteTitle.AccessibleName = null;
            this.cbxFontNoteTitle.AccessibleRole = System.Windows.Forms.AccessibleRole.ComboBox;
            this.cbxFontNoteTitle.BackgroundImage = null;
            this.cbxFontNoteTitle.DropDownHeight = 140;
            this.cbxFontNoteTitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFontNoteTitle.Font = null;
            this.cbxFontNoteTitle.Name = "cbxFontNoteTitle";
            // 
            // lblTextFontTitleFamily
            // 
            this.lblTextFontTitleFamily.AccessibleDescription = null;
            this.lblTextFontTitleFamily.AccessibleName = null;
            resources.ApplyResources(this.lblTextFontTitleFamily, "lblTextFontTitleFamily");
            this.lblTextFontTitleFamily.Font = null;
            this.lblTextFontTitleFamily.Name = "lblTextFontTitleFamily";
            this.lblTextFontTitleFamily.UseCompatibleTextRendering = true;
            // 
            // lblTextDirection
            // 
            resources.ApplyResources(this.lblTextDirection, "lblTextDirection");
            this.lblTextDirection.AccessibleName = null;
            this.lblTextDirection.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.lblTextDirection.Font = null;
            this.lblTextDirection.Name = "lblTextDirection";
            this.lblTextDirection.UseCompatibleTextRendering = true;
            // 
            // cbxTextDirection
            // 
            resources.ApplyResources(this.cbxTextDirection, "cbxTextDirection");
            this.cbxTextDirection.AccessibleName = null;
            this.cbxTextDirection.AccessibleRole = System.Windows.Forms.AccessibleRole.ComboBox;
            this.cbxTextDirection.BackgroundImage = null;
            this.cbxTextDirection.CausesValidation = false;
            this.cbxTextDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTextDirection.Font = null;
            this.cbxTextDirection.FormattingEnabled = true;
            this.cbxTextDirection.Items.AddRange(new object[] {
            resources.GetString("cbxTextDirection.Items"),
            resources.GetString("cbxTextDirection.Items1")});
            this.cbxTextDirection.Name = "cbxTextDirection";
            // 
            // lblTextFontContentPoints
            // 
            resources.ApplyResources(this.lblTextFontContentPoints, "lblTextFontContentPoints");
            this.lblTextFontContentPoints.AccessibleName = null;
            this.lblTextFontContentPoints.Font = null;
            this.lblTextFontContentPoints.Name = "lblTextFontContentPoints";
            this.lblTextFontContentPoints.UseCompatibleTextRendering = true;
            // 
            // numFontSizeContent
            // 
            this.numFontSizeContent.AccessibleDescription = null;
            this.numFontSizeContent.AccessibleName = null;
            resources.ApplyResources(this.numFontSizeContent, "numFontSizeContent");
            this.numFontSizeContent.Font = null;
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
            this.numFontSizeContent.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblTextFontContentSize
            // 
            this.lblTextFontContentSize.AccessibleDescription = null;
            this.lblTextFontContentSize.AccessibleName = null;
            resources.ApplyResources(this.lblTextFontContentSize, "lblTextFontContentSize");
            this.lblTextFontContentSize.Font = null;
            this.lblTextFontContentSize.Name = "lblTextFontContentSize";
            this.lblTextFontContentSize.UseCompatibleTextRendering = true;
            // 
            // lblTextNoteFont
            // 
            this.lblTextNoteFont.AccessibleDescription = null;
            this.lblTextNoteFont.AccessibleName = null;
            resources.ApplyResources(this.lblTextNoteFont, "lblTextNoteFont");
            this.lblTextNoteFont.Font = null;
            this.lblTextNoteFont.Name = "lblTextNoteFont";
            this.lblTextNoteFont.UseCompatibleTextRendering = true;
            // 
            // cbxFontNoteContent
            // 
            resources.ApplyResources(this.cbxFontNoteContent, "cbxFontNoteContent");
            this.cbxFontNoteContent.AccessibleName = null;
            this.cbxFontNoteContent.AccessibleRole = System.Windows.Forms.AccessibleRole.ComboBox;
            this.cbxFontNoteContent.BackgroundImage = null;
            this.cbxFontNoteContent.DropDownHeight = 140;
            this.cbxFontNoteContent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFontNoteContent.Font = null;
            this.cbxFontNoteContent.Name = "cbxFontNoteContent";
            // 
            // tabPageTrayicon
            // 
            this.tabPageTrayicon.AccessibleDescription = null;
            this.tabPageTrayicon.AccessibleName = null;
            resources.ApplyResources(this.tabPageTrayicon, "tabPageTrayicon");
            this.tabPageTrayicon.BackgroundImage = null;
            this.tabPageTrayicon.Controls.Add(this.chxUseAlternativeTrayicon);
            this.tabPageTrayicon.Controls.Add(this.lblFontsizePoints);
            this.tabPageTrayicon.Controls.Add(this.lblTextFontsizeMenu);
            this.tabPageTrayicon.Controls.Add(this.numTrayiconFontsize);
            this.tabPageTrayicon.Controls.Add(this.chxTrayiconBoldExit);
            this.tabPageTrayicon.Controls.Add(this.chxTrayiconBoldSettings);
            this.tabPageTrayicon.Controls.Add(this.chxTrayiconBoldManagenotes);
            this.tabPageTrayicon.Controls.Add(this.chxTrayiconBoldNewnote);
            this.tabPageTrayicon.Font = null;
            this.tabPageTrayicon.Name = "tabPageTrayicon";
            this.tabPageTrayicon.UseVisualStyleBackColor = true;
            // 
            // chxUseAlternativeTrayicon
            // 
            this.chxUseAlternativeTrayicon.AccessibleDescription = null;
            this.chxUseAlternativeTrayicon.AccessibleName = null;
            resources.ApplyResources(this.chxUseAlternativeTrayicon, "chxUseAlternativeTrayicon");
            this.chxUseAlternativeTrayicon.BackgroundImage = null;
            this.chxUseAlternativeTrayicon.Font = null;
            this.chxUseAlternativeTrayicon.Name = "chxUseAlternativeTrayicon";
            this.chxUseAlternativeTrayicon.UseCompatibleTextRendering = true;
            this.chxUseAlternativeTrayicon.UseVisualStyleBackColor = true;
            // 
            // lblFontsizePoints
            // 
            this.lblFontsizePoints.AccessibleDescription = null;
            this.lblFontsizePoints.AccessibleName = null;
            resources.ApplyResources(this.lblFontsizePoints, "lblFontsizePoints");
            this.lblFontsizePoints.Font = null;
            this.lblFontsizePoints.Name = "lblFontsizePoints";
            this.lblFontsizePoints.UseCompatibleTextRendering = true;
            // 
            // lblTextFontsizeMenu
            // 
            this.lblTextFontsizeMenu.AccessibleDescription = null;
            this.lblTextFontsizeMenu.AccessibleName = null;
            resources.ApplyResources(this.lblTextFontsizeMenu, "lblTextFontsizeMenu");
            this.lblTextFontsizeMenu.Font = null;
            this.lblTextFontsizeMenu.Name = "lblTextFontsizeMenu";
            this.lblTextFontsizeMenu.UseCompatibleTextRendering = true;
            // 
            // numTrayiconFontsize
            // 
            this.numTrayiconFontsize.AccessibleDescription = null;
            this.numTrayiconFontsize.AccessibleName = null;
            resources.ApplyResources(this.numTrayiconFontsize, "numTrayiconFontsize");
            this.numTrayiconFontsize.Font = null;
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
            this.numTrayiconFontsize.Value = new decimal(new int[] {
            1000,
            0,
            0,
            131072});
            // 
            // chxTrayiconBoldExit
            // 
            this.chxTrayiconBoldExit.AccessibleDescription = null;
            this.chxTrayiconBoldExit.AccessibleName = null;
            resources.ApplyResources(this.chxTrayiconBoldExit, "chxTrayiconBoldExit");
            this.chxTrayiconBoldExit.BackgroundImage = null;
            this.chxTrayiconBoldExit.Font = null;
            this.chxTrayiconBoldExit.Name = "chxTrayiconBoldExit";
            this.chxTrayiconBoldExit.UseCompatibleTextRendering = true;
            this.chxTrayiconBoldExit.UseVisualStyleBackColor = true;
            // 
            // chxTrayiconBoldSettings
            // 
            this.chxTrayiconBoldSettings.AccessibleDescription = null;
            this.chxTrayiconBoldSettings.AccessibleName = null;
            resources.ApplyResources(this.chxTrayiconBoldSettings, "chxTrayiconBoldSettings");
            this.chxTrayiconBoldSettings.BackgroundImage = null;
            this.chxTrayiconBoldSettings.Font = null;
            this.chxTrayiconBoldSettings.Name = "chxTrayiconBoldSettings";
            this.chxTrayiconBoldSettings.UseCompatibleTextRendering = true;
            this.chxTrayiconBoldSettings.UseVisualStyleBackColor = true;
            // 
            // chxTrayiconBoldManagenotes
            // 
            this.chxTrayiconBoldManagenotes.AccessibleDescription = null;
            this.chxTrayiconBoldManagenotes.AccessibleName = null;
            resources.ApplyResources(this.chxTrayiconBoldManagenotes, "chxTrayiconBoldManagenotes");
            this.chxTrayiconBoldManagenotes.BackgroundImage = null;
            this.chxTrayiconBoldManagenotes.Font = null;
            this.chxTrayiconBoldManagenotes.Name = "chxTrayiconBoldManagenotes";
            this.chxTrayiconBoldManagenotes.UseCompatibleTextRendering = true;
            this.chxTrayiconBoldManagenotes.UseVisualStyleBackColor = true;
            // 
            // chxTrayiconBoldNewnote
            // 
            this.chxTrayiconBoldNewnote.AccessibleDescription = null;
            this.chxTrayiconBoldNewnote.AccessibleName = null;
            resources.ApplyResources(this.chxTrayiconBoldNewnote, "chxTrayiconBoldNewnote");
            this.chxTrayiconBoldNewnote.BackgroundImage = null;
            this.chxTrayiconBoldNewnote.Checked = true;
            this.chxTrayiconBoldNewnote.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxTrayiconBoldNewnote.Font = null;
            this.chxTrayiconBoldNewnote.Name = "chxTrayiconBoldNewnote";
            this.chxTrayiconBoldNewnote.UseCompatibleTextRendering = true;
            this.chxTrayiconBoldNewnote.UseVisualStyleBackColor = true;
            // 
            // tabAppereanceManagenotes
            // 
            this.tabAppereanceManagenotes.AccessibleDescription = null;
            this.tabAppereanceManagenotes.AccessibleName = null;
            resources.ApplyResources(this.tabAppereanceManagenotes, "tabAppereanceManagenotes");
            this.tabAppereanceManagenotes.BackgroundImage = null;
            this.tabAppereanceManagenotes.Controls.Add(this.chxCaseSentiveSearch);
            this.tabAppereanceManagenotes.Controls.Add(this.label4);
            this.tabAppereanceManagenotes.Controls.Add(this.numManagenotesFont);
            this.tabAppereanceManagenotes.Controls.Add(this.lbTextManagesnotesFontSize);
            this.tabAppereanceManagenotes.Controls.Add(this.lblTextSkinManagenotes);
            this.tabAppereanceManagenotes.Controls.Add(this.cbxManageNotesSkin);
            this.tabAppereanceManagenotes.Controls.Add(this.cbxManagenotesTooltipContent);
            this.tabAppereanceManagenotes.Font = null;
            this.tabAppereanceManagenotes.Name = "tabAppereanceManagenotes";
            this.tabAppereanceManagenotes.UseVisualStyleBackColor = true;
            // 
            // chxCaseSentiveSearch
            // 
            this.chxCaseSentiveSearch.AccessibleDescription = null;
            this.chxCaseSentiveSearch.AccessibleName = null;
            resources.ApplyResources(this.chxCaseSentiveSearch, "chxCaseSentiveSearch");
            this.chxCaseSentiveSearch.BackgroundImage = null;
            this.chxCaseSentiveSearch.Checked = true;
            this.chxCaseSentiveSearch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxCaseSentiveSearch.Font = null;
            this.chxCaseSentiveSearch.Name = "chxCaseSentiveSearch";
            this.chxCaseSentiveSearch.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AccessibleDescription = null;
            this.label4.AccessibleName = null;
            resources.ApplyResources(this.label4, "label4");
            this.label4.Font = null;
            this.label4.Name = "label4";
            // 
            // numManagenotesFont
            // 
            this.numManagenotesFont.AccessibleDescription = null;
            this.numManagenotesFont.AccessibleName = null;
            resources.ApplyResources(this.numManagenotesFont, "numManagenotesFont");
            this.numManagenotesFont.Font = null;
            this.numManagenotesFont.Maximum = new decimal(new int[] {
            48,
            0,
            0,
            0});
            this.numManagenotesFont.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numManagenotesFont.Name = "numManagenotesFont";
            this.numManagenotesFont.Value = new decimal(new int[] {
            1000,
            0,
            0,
            131072});
            // 
            // lbTextManagesnotesFontSize
            // 
            this.lbTextManagesnotesFontSize.AccessibleDescription = null;
            this.lbTextManagesnotesFontSize.AccessibleName = null;
            resources.ApplyResources(this.lbTextManagesnotesFontSize, "lbTextManagesnotesFontSize");
            this.lbTextManagesnotesFontSize.Font = null;
            this.lbTextManagesnotesFontSize.Name = "lbTextManagesnotesFontSize";
            // 
            // lblTextSkinManagenotes
            // 
            this.lblTextSkinManagenotes.AccessibleDescription = null;
            this.lblTextSkinManagenotes.AccessibleName = null;
            resources.ApplyResources(this.lblTextSkinManagenotes, "lblTextSkinManagenotes");
            this.lblTextSkinManagenotes.Font = null;
            this.lblTextSkinManagenotes.Name = "lblTextSkinManagenotes";
            this.lblTextSkinManagenotes.UseCompatibleTextRendering = true;
            // 
            // cbxManageNotesSkin
            // 
            this.cbxManageNotesSkin.AccessibleDescription = null;
            this.cbxManageNotesSkin.AccessibleName = null;
            resources.ApplyResources(this.cbxManageNotesSkin, "cbxManageNotesSkin");
            this.cbxManageNotesSkin.BackgroundImage = null;
            this.cbxManageNotesSkin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxManageNotesSkin.Font = null;
            this.cbxManageNotesSkin.FormattingEnabled = true;
            this.cbxManageNotesSkin.Name = "cbxManageNotesSkin";
            // 
            // cbxManagenotesTooltipContent
            // 
            this.cbxManagenotesTooltipContent.AccessibleDescription = null;
            this.cbxManagenotesTooltipContent.AccessibleName = null;
            resources.ApplyResources(this.cbxManagenotesTooltipContent, "cbxManagenotesTooltipContent");
            this.cbxManagenotesTooltipContent.BackgroundImage = null;
            this.cbxManagenotesTooltipContent.Font = null;
            this.cbxManagenotesTooltipContent.Name = "cbxManagenotesTooltipContent";
            this.cbxManagenotesTooltipContent.UseCompatibleTextRendering = true;
            this.cbxManagenotesTooltipContent.UseVisualStyleBackColor = true;
            // 
            // tabHighlight
            // 
            this.tabHighlight.AccessibleDescription = null;
            this.tabHighlight.AccessibleName = null;
            resources.ApplyResources(this.tabHighlight, "tabHighlight");
            this.tabHighlight.BackgroundImage = null;
            this.tabHighlight.Controls.Add(this.chxHighlightSQL);
            this.tabHighlight.Controls.Add(this.chxHighlightPHP);
            this.tabHighlight.Controls.Add(this.chxHighlightHyperlinks);
            this.tabHighlight.Controls.Add(this.chxConfirmLink);
            this.tabHighlight.Controls.Add(this.chxHighlightHTML);
            this.tabHighlight.Font = null;
            this.tabHighlight.Name = "tabHighlight";
            this.tabHighlight.UseVisualStyleBackColor = true;
            // 
            // chxHighlightSQL
            // 
            this.chxHighlightSQL.AccessibleDescription = null;
            this.chxHighlightSQL.AccessibleName = null;
            resources.ApplyResources(this.chxHighlightSQL, "chxHighlightSQL");
            this.chxHighlightSQL.BackgroundImage = null;
            this.chxHighlightSQL.Name = "chxHighlightSQL";
            this.chxHighlightSQL.UseCompatibleTextRendering = true;
            this.chxHighlightSQL.UseVisualStyleBackColor = true;
            // 
            // chxHighlightPHP
            // 
            this.chxHighlightPHP.AccessibleDescription = null;
            this.chxHighlightPHP.AccessibleName = null;
            resources.ApplyResources(this.chxHighlightPHP, "chxHighlightPHP");
            this.chxHighlightPHP.BackgroundImage = null;
            this.chxHighlightPHP.Name = "chxHighlightPHP";
            this.chxHighlightPHP.UseCompatibleTextRendering = true;
            this.chxHighlightPHP.UseVisualStyleBackColor = true;
            // 
            // chxHighlightHyperlinks
            // 
            this.chxHighlightHyperlinks.AccessibleDescription = null;
            this.chxHighlightHyperlinks.AccessibleName = null;
            resources.ApplyResources(this.chxHighlightHyperlinks, "chxHighlightHyperlinks");
            this.chxHighlightHyperlinks.BackgroundImage = null;
            this.chxHighlightHyperlinks.Name = "chxHighlightHyperlinks";
            this.chxHighlightHyperlinks.UseCompatibleTextRendering = true;
            this.chxHighlightHyperlinks.UseVisualStyleBackColor = true;
            // 
            // chxConfirmLink
            // 
            this.chxConfirmLink.AccessibleDescription = null;
            this.chxConfirmLink.AccessibleName = null;
            resources.ApplyResources(this.chxConfirmLink, "chxConfirmLink");
            this.chxConfirmLink.BackgroundImage = null;
            this.chxConfirmLink.Checked = true;
            this.chxConfirmLink.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxConfirmLink.Name = "chxConfirmLink";
            this.chxConfirmLink.UseCompatibleTextRendering = true;
            this.chxConfirmLink.UseVisualStyleBackColor = true;
            // 
            // chxHighlightHTML
            // 
            this.chxHighlightHTML.AccessibleDescription = null;
            this.chxHighlightHTML.AccessibleName = null;
            resources.ApplyResources(this.chxHighlightHTML, "chxHighlightHTML");
            this.chxHighlightHTML.BackgroundImage = null;
            this.chxHighlightHTML.Name = "chxHighlightHTML";
            this.chxHighlightHTML.UseCompatibleTextRendering = true;
            this.chxHighlightHTML.UseVisualStyleBackColor = true;
            // 
            // tabSharing
            // 
            this.tabSharing.AccessibleDescription = null;
            this.tabSharing.AccessibleName = null;
            resources.ApplyResources(this.tabSharing, "tabSharing");
            this.tabSharing.BackgroundImage = null;
            this.tabSharing.Controls.Add(this.tabControlSharing);
            this.tabSharing.Font = null;
            this.tabSharing.Name = "tabSharing";
            this.tabSharing.UseVisualStyleBackColor = true;
            // 
            // tabControlSharing
            // 
            this.tabControlSharing.AccessibleDescription = null;
            this.tabControlSharing.AccessibleName = null;
            resources.ApplyResources(this.tabControlSharing, "tabControlSharing");
            this.tabControlSharing.BackgroundImage = null;
            this.tabControlSharing.Controls.Add(this.tabEmail);
            this.tabControlSharing.Font = null;
            this.tabControlSharing.Name = "tabControlSharing";
            this.tabControlSharing.SelectedIndex = 0;
            // 
            // tabEmail
            // 
            this.tabEmail.AccessibleDescription = null;
            this.tabEmail.AccessibleName = null;
            resources.ApplyResources(this.tabEmail, "tabEmail");
            this.tabEmail.BackgroundImage = null;
            this.tabEmail.Controls.Add(this.chxSocialEmailEnabled);
            this.tabEmail.Controls.Add(this.chxSocialEmailDefaultaddressSet);
            this.tabEmail.Controls.Add(this.tbDefaultEmail);
            this.tabEmail.Font = null;
            this.tabEmail.Name = "tabEmail";
            this.tabEmail.UseVisualStyleBackColor = true;
            // 
            // chxSocialEmailEnabled
            // 
            this.chxSocialEmailEnabled.AccessibleDescription = null;
            this.chxSocialEmailEnabled.AccessibleName = null;
            resources.ApplyResources(this.chxSocialEmailEnabled, "chxSocialEmailEnabled");
            this.chxSocialEmailEnabled.BackgroundImage = null;
            this.chxSocialEmailEnabled.Name = "chxSocialEmailEnabled";
            this.chxSocialEmailEnabled.UseCompatibleTextRendering = true;
            this.chxSocialEmailEnabled.UseVisualStyleBackColor = true;
            // 
            // chxSocialEmailDefaultaddressSet
            // 
            this.chxSocialEmailDefaultaddressSet.AccessibleDescription = null;
            this.chxSocialEmailDefaultaddressSet.AccessibleName = null;
            resources.ApplyResources(this.chxSocialEmailDefaultaddressSet, "chxSocialEmailDefaultaddressSet");
            this.chxSocialEmailDefaultaddressSet.BackgroundImage = null;
            this.chxSocialEmailDefaultaddressSet.Checked = true;
            this.chxSocialEmailDefaultaddressSet.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxSocialEmailDefaultaddressSet.Name = "chxSocialEmailDefaultaddressSet";
            this.chxSocialEmailDefaultaddressSet.UseCompatibleTextRendering = true;
            this.chxSocialEmailDefaultaddressSet.UseVisualStyleBackColor = true;
            this.chxSocialEmailDefaultaddressSet.CheckedChanged += new System.EventHandler(this.chxSocialEmailDefaultaddressBlank_CheckedChanged);
            // 
            // tbDefaultEmail
            // 
            resources.ApplyResources(this.tbDefaultEmail, "tbDefaultEmail");
            this.tbDefaultEmail.AccessibleName = null;
            this.tbDefaultEmail.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.tbDefaultEmail.BackgroundImage = null;
            this.tbDefaultEmail.Name = "tbDefaultEmail";
            // 
            // tabNetwork
            // 
            this.tabNetwork.AccessibleDescription = null;
            this.tabNetwork.AccessibleName = null;
            resources.ApplyResources(this.tabNetwork, "tabNetwork");
            this.tabNetwork.BackgroundImage = null;
            this.tabNetwork.Controls.Add(this.tabControlNetwork);
            this.tabNetwork.Controls.Add(this.lblTextNetworkMiliseconds);
            this.tabNetwork.Font = null;
            this.tabNetwork.Name = "tabNetwork";
            this.tabNetwork.UseVisualStyleBackColor = true;
            // 
            // tabControlNetwork
            // 
            this.tabControlNetwork.AccessibleDescription = null;
            this.tabControlNetwork.AccessibleName = null;
            resources.ApplyResources(this.tabControlNetwork, "tabControlNetwork");
            this.tabControlNetwork.BackgroundImage = null;
            this.tabControlNetwork.Controls.Add(this.tabUpdates);
            this.tabControlNetwork.Controls.Add(this.tabProxy);
            this.tabControlNetwork.Font = null;
            this.tabControlNetwork.Name = "tabControlNetwork";
            this.tabControlNetwork.SelectedIndex = 0;
            // 
            // tabUpdates
            // 
            this.tabUpdates.AccessibleDescription = null;
            this.tabUpdates.AccessibleName = null;
            resources.ApplyResources(this.tabUpdates, "tabUpdates");
            this.tabUpdates.BackgroundImage = null;
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
            this.tabUpdates.Font = null;
            this.tabUpdates.Name = "tabUpdates";
            this.tabUpdates.UseVisualStyleBackColor = true;
            // 
            // chxUpdateSilentInstall
            // 
            this.chxUpdateSilentInstall.AccessibleDescription = null;
            this.chxUpdateSilentInstall.AccessibleName = null;
            resources.ApplyResources(this.chxUpdateSilentInstall, "chxUpdateSilentInstall");
            this.chxUpdateSilentInstall.BackgroundImage = null;
            this.chxUpdateSilentInstall.Name = "chxUpdateSilentInstall";
            this.chxUpdateSilentInstall.UseCompatibleTextRendering = true;
            this.chxUpdateSilentInstall.UseVisualStyleBackColor = true;
            // 
            // btnGPGPathBrowse
            // 
            this.btnGPGPathBrowse.AccessibleDescription = null;
            this.btnGPGPathBrowse.AccessibleName = null;
            resources.ApplyResources(this.btnGPGPathBrowse, "btnGPGPathBrowse");
            this.btnGPGPathBrowse.BackColor = System.Drawing.Color.LightGray;
            this.btnGPGPathBrowse.BackgroundImage = null;
            this.btnGPGPathBrowse.Font = null;
            this.btnGPGPathBrowse.Name = "btnGPGPathBrowse";
            this.btnGPGPathBrowse.UseCompatibleTextRendering = true;
            this.btnGPGPathBrowse.UseVisualStyleBackColor = false;
            this.btnGPGPathBrowse.Click += new System.EventHandler(this.btnGPGPathBrowse_Click);
            // 
            // tbGPGPath
            // 
            this.tbGPGPath.AccessibleDescription = null;
            this.tbGPGPath.AccessibleName = null;
            resources.ApplyResources(this.tbGPGPath, "tbGPGPath");
            this.tbGPGPath.BackgroundImage = null;
            this.tbGPGPath.Name = "tbGPGPath";
            // 
            // lblTextGPGPath
            // 
            this.lblTextGPGPath.AccessibleDescription = null;
            this.lblTextGPGPath.AccessibleName = null;
            resources.ApplyResources(this.lblTextGPGPath, "lblTextGPGPath");
            this.lblTextGPGPath.Name = "lblTextGPGPath";
            this.lblTextGPGPath.UseCompatibleTextRendering = true;
            // 
            // chxCheckUpdatesSignature
            // 
            this.chxCheckUpdatesSignature.AccessibleDescription = null;
            this.chxCheckUpdatesSignature.AccessibleName = null;
            resources.ApplyResources(this.chxCheckUpdatesSignature, "chxCheckUpdatesSignature");
            this.chxCheckUpdatesSignature.BackgroundImage = null;
            this.chxCheckUpdatesSignature.Name = "chxCheckUpdatesSignature";
            this.chxCheckUpdatesSignature.UseCompatibleTextRendering = true;
            this.chxCheckUpdatesSignature.UseVisualStyleBackColor = true;
            this.chxCheckUpdatesSignature.CheckedChanged += new System.EventHandler(this.chxCheckUpdatesSignature_CheckedChanged);
            // 
            // chxCheckUpdates
            // 
            this.chxCheckUpdates.AccessibleDescription = null;
            this.chxCheckUpdates.AccessibleName = null;
            resources.ApplyResources(this.chxCheckUpdates, "chxCheckUpdates");
            this.chxCheckUpdates.BackgroundImage = null;
            this.chxCheckUpdates.Name = "chxCheckUpdates";
            this.chxCheckUpdates.UseCompatibleTextRendering = true;
            this.chxCheckUpdates.UseVisualStyleBackColor = true;
            this.chxCheckUpdates.CheckedChanged += new System.EventHandler(this.cbxCheckUpdates_CheckedChanged);
            // 
            // numUpdateCheckDays
            // 
            this.numUpdateCheckDays.AccessibleDescription = null;
            this.numUpdateCheckDays.AccessibleName = null;
            resources.ApplyResources(this.numUpdateCheckDays, "numUpdateCheckDays");
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
            this.numUpdateCheckDays.Value = new decimal(new int[] {
            14,
            0,
            0,
            0});
            // 
            // lblTextCheckforupdatesevery
            // 
            this.lblTextCheckforupdatesevery.AccessibleDescription = null;
            this.lblTextCheckforupdatesevery.AccessibleName = null;
            resources.ApplyResources(this.lblTextCheckforupdatesevery, "lblTextCheckforupdatesevery");
            this.lblTextCheckforupdatesevery.Name = "lblTextCheckforupdatesevery";
            this.lblTextCheckforupdatesevery.UseCompatibleTextRendering = true;
            // 
            // lblLatestUpdateCheck
            // 
            this.lblLatestUpdateCheck.AccessibleDescription = null;
            this.lblLatestUpdateCheck.AccessibleName = null;
            resources.ApplyResources(this.lblLatestUpdateCheck, "lblLatestUpdateCheck");
            this.lblLatestUpdateCheck.Name = "lblLatestUpdateCheck";
            // 
            // lblTextDayAtStartup
            // 
            this.lblTextDayAtStartup.AccessibleDescription = null;
            this.lblTextDayAtStartup.AccessibleName = null;
            resources.ApplyResources(this.lblTextDayAtStartup, "lblTextDayAtStartup");
            this.lblTextDayAtStartup.Name = "lblTextDayAtStartup";
            this.lblTextDayAtStartup.UseCompatibleTextRendering = true;
            // 
            // lblTextLatestUpdateCheck
            // 
            this.lblTextLatestUpdateCheck.AccessibleDescription = null;
            this.lblTextLatestUpdateCheck.AccessibleName = null;
            resources.ApplyResources(this.lblTextLatestUpdateCheck, "lblTextLatestUpdateCheck");
            this.lblTextLatestUpdateCheck.Name = "lblTextLatestUpdateCheck";
            this.lblTextLatestUpdateCheck.UseCompatibleTextRendering = true;
            // 
            // btnCheckUpdates
            // 
            this.btnCheckUpdates.AccessibleDescription = null;
            this.btnCheckUpdates.AccessibleName = null;
            resources.ApplyResources(this.btnCheckUpdates, "btnCheckUpdates");
            this.btnCheckUpdates.BackColor = System.Drawing.Color.LightGray;
            this.btnCheckUpdates.BackgroundImage = null;
            this.btnCheckUpdates.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCheckUpdates.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btnCheckUpdates.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnCheckUpdates.Name = "btnCheckUpdates";
            this.btnCheckUpdates.UseCompatibleTextRendering = true;
            this.btnCheckUpdates.UseVisualStyleBackColor = false;
            this.btnCheckUpdates.Click += new System.EventHandler(this.btnCheckUpdates_Click);
            // 
            // tabProxy
            // 
            this.tabProxy.AccessibleDescription = null;
            this.tabProxy.AccessibleName = null;
            resources.ApplyResources(this.tabProxy, "tabProxy");
            this.tabProxy.BackgroundImage = null;
            this.tabProxy.Controls.Add(this.chxForceUseIPv6);
            this.tabProxy.Controls.Add(this.lblTextMiliseconds);
            this.tabProxy.Controls.Add(this.numTimeout);
            this.tabProxy.Controls.Add(this.chxProxyEnabled);
            this.tabProxy.Controls.Add(this.lblTextNetworkTimeout);
            this.tabProxy.Controls.Add(this.iptbProxy);
            this.tabProxy.Font = null;
            this.tabProxy.Name = "tabProxy";
            this.tabProxy.UseVisualStyleBackColor = true;
            // 
            // chxForceUseIPv6
            // 
            this.chxForceUseIPv6.AccessibleDescription = null;
            this.chxForceUseIPv6.AccessibleName = null;
            resources.ApplyResources(this.chxForceUseIPv6, "chxForceUseIPv6");
            this.chxForceUseIPv6.BackgroundImage = null;
            this.chxForceUseIPv6.Font = null;
            this.chxForceUseIPv6.Name = "chxForceUseIPv6";
            this.chxForceUseIPv6.UseVisualStyleBackColor = true;
            // 
            // lblTextMiliseconds
            // 
            resources.ApplyResources(this.lblTextMiliseconds, "lblTextMiliseconds");
            this.lblTextMiliseconds.AccessibleName = null;
            this.lblTextMiliseconds.Font = null;
            this.lblTextMiliseconds.Name = "lblTextMiliseconds";
            this.lblTextMiliseconds.UseCompatibleTextRendering = true;
            // 
            // numTimeout
            // 
            this.numTimeout.AccessibleDescription = null;
            this.numTimeout.AccessibleName = null;
            resources.ApplyResources(this.numTimeout, "numTimeout");
            this.numTimeout.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
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
            this.numTimeout.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // chxProxyEnabled
            // 
            this.chxProxyEnabled.AccessibleDescription = null;
            this.chxProxyEnabled.AccessibleName = null;
            resources.ApplyResources(this.chxProxyEnabled, "chxProxyEnabled");
            this.chxProxyEnabled.BackgroundImage = null;
            this.chxProxyEnabled.Name = "chxProxyEnabled";
            this.chxProxyEnabled.UseCompatibleTextRendering = true;
            this.chxProxyEnabled.UseVisualStyleBackColor = true;
            this.chxProxyEnabled.CheckedChanged += new System.EventHandler(this.chxUseProxy_CheckedChanged);
            // 
            // lblTextNetworkTimeout
            // 
            this.lblTextNetworkTimeout.AccessibleDescription = null;
            this.lblTextNetworkTimeout.AccessibleName = null;
            resources.ApplyResources(this.lblTextNetworkTimeout, "lblTextNetworkTimeout");
            this.lblTextNetworkTimeout.Name = "lblTextNetworkTimeout";
            this.lblTextNetworkTimeout.UseCompatibleTextRendering = true;
            // 
            // iptbProxy
            // 
            this.iptbProxy.AccessibleDescription = null;
            this.iptbProxy.AccessibleName = null;
            resources.ApplyResources(this.iptbProxy, "iptbProxy");
            this.iptbProxy.BackgroundImage = null;
            this.iptbProxy.Font = null;
            this.iptbProxy.Name = "iptbProxy";
            // 
            // lblTextNetworkMiliseconds
            // 
            this.lblTextNetworkMiliseconds.AccessibleDescription = null;
            this.lblTextNetworkMiliseconds.AccessibleName = null;
            resources.ApplyResources(this.lblTextNetworkMiliseconds, "lblTextNetworkMiliseconds");
            this.lblTextNetworkMiliseconds.Font = null;
            this.lblTextNetworkMiliseconds.Name = "lblTextNetworkMiliseconds";
            // 
            // tabAdvance
            // 
            this.tabAdvance.AccessibleDescription = null;
            this.tabAdvance.AccessibleName = null;
            resources.ApplyResources(this.tabAdvance, "tabAdvance");
            this.tabAdvance.BackgroundImage = null;
            this.tabAdvance.Controls.Add(this.numWarnLimitVisible);
            this.tabAdvance.Controls.Add(this.lblTextVisibleNotesWarnLimit);
            this.tabAdvance.Controls.Add(this.lblTextTotalNotesWarnLimit);
            this.tabAdvance.Controls.Add(this.numWarnLimitTotal);
            this.tabAdvance.Controls.Add(this.chxLogExceptions);
            this.tabAdvance.Controls.Add(this.lblTextLogging);
            this.tabAdvance.Controls.Add(this.chxLogDebug);
            this.tabAdvance.Controls.Add(this.btnResetSettings);
            this.tabAdvance.Controls.Add(this.chxLogErrors);
            this.tabAdvance.Controls.Add(this.btnBrowse);
            this.tabAdvance.Controls.Add(this.lblTextNoteLocation);
            this.tabAdvance.Controls.Add(this.tbNotesSavePath);
            this.tabAdvance.Font = null;
            this.tabAdvance.Name = "tabAdvance";
            this.tabAdvance.UseVisualStyleBackColor = true;
            // 
            // numWarnLimitVisible
            // 
            this.numWarnLimitVisible.AccessibleDescription = null;
            this.numWarnLimitVisible.AccessibleName = null;
            resources.ApplyResources(this.numWarnLimitVisible, "numWarnLimitVisible");
            this.numWarnLimitVisible.Font = null;
            this.numWarnLimitVisible.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.numWarnLimitVisible.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWarnLimitVisible.Name = "numWarnLimitVisible";
            this.numWarnLimitVisible.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // lblTextVisibleNotesWarnLimit
            // 
            this.lblTextVisibleNotesWarnLimit.AccessibleDescription = null;
            this.lblTextVisibleNotesWarnLimit.AccessibleName = null;
            resources.ApplyResources(this.lblTextVisibleNotesWarnLimit, "lblTextVisibleNotesWarnLimit");
            this.lblTextVisibleNotesWarnLimit.Font = null;
            this.lblTextVisibleNotesWarnLimit.Name = "lblTextVisibleNotesWarnLimit";
            this.lblTextVisibleNotesWarnLimit.UseCompatibleTextRendering = true;
            // 
            // lblTextTotalNotesWarnLimit
            // 
            this.lblTextTotalNotesWarnLimit.AccessibleDescription = null;
            this.lblTextTotalNotesWarnLimit.AccessibleName = null;
            resources.ApplyResources(this.lblTextTotalNotesWarnLimit, "lblTextTotalNotesWarnLimit");
            this.lblTextTotalNotesWarnLimit.Font = null;
            this.lblTextTotalNotesWarnLimit.Name = "lblTextTotalNotesWarnLimit";
            this.lblTextTotalNotesWarnLimit.UseCompatibleTextRendering = true;
            // 
            // numWarnLimitTotal
            // 
            this.numWarnLimitTotal.AccessibleDescription = null;
            this.numWarnLimitTotal.AccessibleName = null;
            resources.ApplyResources(this.numWarnLimitTotal, "numWarnLimitTotal");
            this.numWarnLimitTotal.Font = null;
            this.numWarnLimitTotal.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numWarnLimitTotal.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWarnLimitTotal.Name = "numWarnLimitTotal";
            this.numWarnLimitTotal.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // chxLogExceptions
            // 
            this.chxLogExceptions.AccessibleDescription = null;
            this.chxLogExceptions.AccessibleName = null;
            resources.ApplyResources(this.chxLogExceptions, "chxLogExceptions");
            this.chxLogExceptions.BackgroundImage = null;
            this.chxLogExceptions.Checked = true;
            this.chxLogExceptions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxLogExceptions.Name = "chxLogExceptions";
            this.chxLogExceptions.UseCompatibleTextRendering = true;
            this.chxLogExceptions.UseVisualStyleBackColor = true;
            // 
            // lblTextLogging
            // 
            this.lblTextLogging.AccessibleDescription = null;
            this.lblTextLogging.AccessibleName = null;
            resources.ApplyResources(this.lblTextLogging, "lblTextLogging");
            this.lblTextLogging.Name = "lblTextLogging";
            this.lblTextLogging.UseCompatibleTextRendering = true;
            // 
            // chxLogDebug
            // 
            this.chxLogDebug.AccessibleDescription = null;
            this.chxLogDebug.AccessibleName = null;
            resources.ApplyResources(this.chxLogDebug, "chxLogDebug");
            this.chxLogDebug.BackgroundImage = null;
            this.chxLogDebug.Name = "chxLogDebug";
            this.chxLogDebug.UseCompatibleTextRendering = true;
            this.chxLogDebug.UseVisualStyleBackColor = true;
            // 
            // btnResetSettings
            // 
            this.btnResetSettings.AccessibleDescription = null;
            this.btnResetSettings.AccessibleName = null;
            resources.ApplyResources(this.btnResetSettings, "btnResetSettings");
            this.btnResetSettings.BackColor = System.Drawing.Color.LightGray;
            this.btnResetSettings.BackgroundImage = null;
            this.btnResetSettings.Name = "btnResetSettings";
            this.btnResetSettings.UseCompatibleTextRendering = true;
            this.btnResetSettings.UseVisualStyleBackColor = false;
            this.btnResetSettings.Click += new System.EventHandler(this.btnResetSettings_Click);
            // 
            // chxLogErrors
            // 
            this.chxLogErrors.AccessibleDescription = null;
            this.chxLogErrors.AccessibleName = null;
            resources.ApplyResources(this.chxLogErrors, "chxLogErrors");
            this.chxLogErrors.BackgroundImage = null;
            this.chxLogErrors.Checked = true;
            this.chxLogErrors.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxLogErrors.Name = "chxLogErrors";
            this.chxLogErrors.UseCompatibleTextRendering = true;
            this.chxLogErrors.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.AccessibleDescription = null;
            this.btnBrowse.AccessibleName = null;
            resources.ApplyResources(this.btnBrowse, "btnBrowse");
            this.btnBrowse.BackColor = System.Drawing.Color.LightGray;
            this.btnBrowse.BackgroundImage = null;
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.UseCompatibleTextRendering = true;
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblTextNoteLocation
            // 
            this.lblTextNoteLocation.AccessibleDescription = null;
            this.lblTextNoteLocation.AccessibleName = null;
            resources.ApplyResources(this.lblTextNoteLocation, "lblTextNoteLocation");
            this.lblTextNoteLocation.Name = "lblTextNoteLocation";
            this.lblTextNoteLocation.UseCompatibleTextRendering = true;
            // 
            // tbNotesSavePath
            // 
            this.tbNotesSavePath.AccessibleDescription = null;
            this.tbNotesSavePath.AccessibleName = null;
            resources.ApplyResources(this.tbNotesSavePath, "tbNotesSavePath");
            this.tbNotesSavePath.BackgroundImage = null;
            this.tbNotesSavePath.Name = "tbNotesSavePath";
            // 
            // chxSettingsExpertEnabled
            // 
            resources.ApplyResources(this.chxSettingsExpertEnabled, "chxSettingsExpertEnabled");
            this.chxSettingsExpertEnabled.AccessibleName = null;
            this.chxSettingsExpertEnabled.BackgroundImage = null;
            this.chxSettingsExpertEnabled.Checked = true;
            this.chxSettingsExpertEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxSettingsExpertEnabled.Name = "chxSettingsExpertEnabled";
            this.chxSettingsExpertEnabled.UseCompatibleTextRendering = true;
            this.chxSettingsExpertEnabled.UseVisualStyleBackColor = true;
            this.chxSettingsExpertEnabled.CheckedChanged += new System.EventHandler(this.cbxShowExpertSettings_CheckedChanged);
            // 
            // folderBrowseDialogNotessavepath
            // 
            resources.ApplyResources(this.folderBrowseDialogNotessavepath, "folderBrowseDialogNotessavepath");
            this.folderBrowseDialogNotessavepath.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // openFileDialogBrowseGPG
            // 
            this.openFileDialogBrowseGPG.AddExtension = false;
            this.openFileDialogBrowseGPG.DefaultExt = "exe";
            this.openFileDialogBrowseGPG.FileName = "gpg.exe";
            resources.ApplyResources(this.openFileDialogBrowseGPG, "openFileDialogBrowseGPG");
            // 
            // FrmSettings
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BackgroundImage = null;
            this.Controls.Add(this.chxSettingsExpertEnabled);
            this.Controls.Add(this.tabControlSettings);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSettings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.tabControlSettings.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.tabAppearance.ResumeLayout(false);
            this.tabctrlAppearance.ResumeLayout(false);
            this.tabAppearanceGeneral.ResumeLayout(false);
            this.tabAppearanceGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numProcTransparency)).EndInit();
            this.tabPageNewNote.ResumeLayout(false);
            this.tabPageNewNote.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numNotesDefaultHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNotesDefaultWidth)).EndInit();
            this.tabPageFonts.ResumeLayout(false);
            this.tabPageFonts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSizeTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSizeContent)).EndInit();
            this.tabPageTrayicon.ResumeLayout(false);
            this.tabPageTrayicon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTrayiconFontsize)).EndInit();
            this.tabAppereanceManagenotes.ResumeLayout(false);
            this.tabAppereanceManagenotes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numManagenotesFont)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.numWarnLimitVisible)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWarnLimitTotal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion        

        private System.Windows.Forms.NumericUpDown numWarnLimitVisible;
        private System.Windows.Forms.Label lblTextVisibleNotesWarnLimit;
        private System.Windows.Forms.Label lblTextTotalNotesWarnLimit;
        private System.Windows.Forms.NumericUpDown numWarnLimitTotal;
        private System.Windows.Forms.TabPage tabAppearanceGeneral;
        private System.Windows.Forms.CheckBox chxShowTooltips;
        private System.Windows.Forms.CheckBox chxTransparecy;
        private System.Windows.Forms.NumericUpDown numProcTransparency;
        private System.Windows.Forms.Label lblTextTransparentProcVisible;
        private System.Windows.Forms.TabPage tabPageNewNote;
        private System.Windows.Forms.CheckBox chxUseRandomDefaultNote;
        private System.Windows.Forms.Label lblDefaultNewNoteColor;
        private System.Windows.Forms.ComboBox cbxDefaultSkin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numNotesDefaultHeight;
        private System.Windows.Forms.NumericUpDown numNotesDefaultWidth;
        private IPTextBox iptbProxy;
        private System.Windows.Forms.TabPage tabAppereanceManagenotes;
        private System.Windows.Forms.CheckBox cbxManagenotesTooltipContent;
        private System.Windows.Forms.ComboBox cbxManageNotesSkin;
        private System.Windows.Forms.Label lblTextSkinManagenotes;
        private System.Windows.Forms.CheckBox chxUseDateAsDefaultTitle;
        private System.Windows.Forms.NumericUpDown numManagenotesFont;
        private System.Windows.Forms.Label lbTextManagesnotesFontSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chxCaseSentiveSearch;
        private System.Windows.Forms.CheckBox chxForceUseIPv6;
    }
}