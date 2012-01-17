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
            this.lblTextMiliseconds = new System.Windows.Forms.Label();
            this.numTimeout = new System.Windows.Forms.NumericUpDown();
            this.chxProxyEnabled = new System.Windows.Forms.CheckBox();
            this.lblTextNetworkTimeout = new System.Windows.Forms.Label();
            this.lblTextNetworkMiliseconds = new System.Windows.Forms.Label();
            this.tabAdvance = new System.Windows.Forms.TabPage();
            this.numWarnLimitVisible = new System.Windows.Forms.NumericUpDown();
            this.chxLoadPlugins = new System.Windows.Forms.CheckBox();
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
            this.iptbProxy = new NoteFly.IPTextBox();
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
            this.btnOK.UseCompatibleTextRendering = true;
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
            this.btnCancel.UseCompatibleTextRendering = true;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.Controls.Add(this.tabGeneral);
            this.tabControlSettings.Controls.Add(this.tabAppearance);
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
            this.tabGeneral.Controls.Add(this.chxLoadPlugins);
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
            this.chxNotesDeleteRecyclebin.Size = new System.Drawing.Size(226, 21);
            this.chxNotesDeleteRecyclebin.TabIndex = 24;
            this.chxNotesDeleteRecyclebin.Text = "Move deleted notes to recycle bin.";
            this.chxNotesDeleteRecyclebin.UseCompatibleTextRendering = true;
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
            this.chxConfirmDeletenote.Size = new System.Drawing.Size(161, 21);
            this.chxConfirmDeletenote.TabIndex = 23;
            this.chxConfirmDeletenote.Text = "Confirm deleting notes.";
            this.chxConfirmDeletenote.UseCompatibleTextRendering = true;
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
            this.cbxActionLeftclick.Location = new System.Drawing.Point(165, 167);
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
            this.chxConfirmExit.Size = new System.Drawing.Size(196, 21);
            this.chxConfirmExit.TabIndex = 20;
            this.chxConfirmExit.Text = "Confirm shutdown of NoteFly";
            this.chxConfirmExit.UseCompatibleTextRendering = true;
            this.chxConfirmExit.UseVisualStyleBackColor = true;
            // 
            // chxStartOnLogin
            // 
            this.chxStartOnLogin.AutoSize = true;
            this.chxStartOnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxStartOnLogin.Location = new System.Drawing.Point(21, 25);
            this.chxStartOnLogin.Name = "chxStartOnLogin";
            this.chxStartOnLogin.Size = new System.Drawing.Size(160, 21);
            this.chxStartOnLogin.TabIndex = 10;
            this.chxStartOnLogin.Text = "Start NoteFly on logon.";
            this.chxStartOnLogin.UseCompatibleTextRendering = true;
            this.chxStartOnLogin.UseVisualStyleBackColor = true;
            // 
            // lblTextActionLeftClicktTrayicon
            // 
            this.lblTextActionLeftClicktTrayicon.AutoSize = true;
            this.lblTextActionLeftClicktTrayicon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextActionLeftClicktTrayicon.Location = new System.Drawing.Point(11, 168);
            this.lblTextActionLeftClicktTrayicon.Name = "lblTextActionLeftClicktTrayicon";
            this.lblTextActionLeftClicktTrayicon.Size = new System.Drawing.Size(148, 20);
            this.lblTextActionLeftClicktTrayicon.TabIndex = 15;
            this.lblTextActionLeftClicktTrayicon.Text = "Action left click trayicon:";
            this.lblTextActionLeftClicktTrayicon.UseCompatibleTextRendering = true;
            // 
            // tabAppearance
            // 
            this.tabAppearance.Controls.Add(this.tabctrlAppearance);
            this.tabAppearance.Location = new System.Drawing.Point(4, 25);
            this.tabAppearance.Name = "tabAppearance";
            this.tabAppearance.Padding = new System.Windows.Forms.Padding(3);
            this.tabAppearance.Size = new System.Drawing.Size(447, 333);
            this.tabAppearance.TabIndex = 0;
            this.tabAppearance.Text = "Appearance";
            this.tabAppearance.UseVisualStyleBackColor = true;
            // 
            // tabctrlAppearance
            // 
            this.tabctrlAppearance.Controls.Add(this.tabAppearanceGeneral);
            this.tabctrlAppearance.Controls.Add(this.tabPageNewNote);
            this.tabctrlAppearance.Controls.Add(this.tabPageFonts);
            this.tabctrlAppearance.Controls.Add(this.tabPageTrayicon);
            this.tabctrlAppearance.Controls.Add(this.tabAppereanceManagenotes);
            this.tabctrlAppearance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabctrlAppearance.ItemSize = new System.Drawing.Size(85, 21);
            this.tabctrlAppearance.Location = new System.Drawing.Point(3, 3);
            this.tabctrlAppearance.MinimumSize = new System.Drawing.Size(80, 21);
            this.tabctrlAppearance.Name = "tabctrlAppearance";
            this.tabctrlAppearance.SelectedIndex = 0;
            this.tabctrlAppearance.Size = new System.Drawing.Size(441, 327);
            this.tabctrlAppearance.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabctrlAppearance.TabIndex = 28;
            // 
            // tabAppearanceGeneral
            // 
            this.tabAppearanceGeneral.Controls.Add(this.chxShowTooltips);
            this.tabAppearanceGeneral.Controls.Add(this.chxTransparecy);
            this.tabAppearanceGeneral.Controls.Add(this.numProcTransparency);
            this.tabAppearanceGeneral.Controls.Add(this.lblTextTransparentProcVisible);
            this.tabAppearanceGeneral.Location = new System.Drawing.Point(4, 25);
            this.tabAppearanceGeneral.Name = "tabAppearanceGeneral";
            this.tabAppearanceGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabAppearanceGeneral.Size = new System.Drawing.Size(433, 298);
            this.tabAppearanceGeneral.TabIndex = 0;
            this.tabAppearanceGeneral.Text = "Everywhere";
            this.tabAppearanceGeneral.UseVisualStyleBackColor = true;
            // 
            // chxShowTooltips
            // 
            this.chxShowTooltips.AutoSize = true;
            this.chxShowTooltips.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxShowTooltips.Location = new System.Drawing.Point(26, 75);
            this.chxShowTooltips.Name = "chxShowTooltips";
            this.chxShowTooltips.Size = new System.Drawing.Size(129, 21);
            this.chxShowTooltips.TabIndex = 13;
            this.chxShowTooltips.Text = "Show tooltip hints";
            this.chxShowTooltips.UseCompatibleTextRendering = true;
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
            this.chxTransparecy.Location = new System.Drawing.Point(26, 32);
            this.chxTransparecy.Name = "chxTransparecy";
            this.chxTransparecy.Size = new System.Drawing.Size(157, 21);
            this.chxTransparecy.TabIndex = 8;
            this.chxTransparecy.Text = "Enable transparency";
            this.chxTransparecy.UseCompatibleTextRendering = true;
            this.chxTransparecy.UseVisualStyleBackColor = false;
            this.chxTransparecy.CheckedChanged += new System.EventHandler(this.chxTransparecy_CheckedChanged);
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
            this.numProcTransparency.Location = new System.Drawing.Point(189, 32);
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
            this.lblTextTransparentProcVisible.Location = new System.Drawing.Point(241, 34);
            this.lblTextTransparentProcVisible.Name = "lblTextTransparentProcVisible";
            this.lblTextTransparentProcVisible.Size = new System.Drawing.Size(58, 20);
            this.lblTextTransparentProcVisible.TabIndex = 12;
            this.lblTextTransparentProcVisible.Text = "% visible";
            this.lblTextTransparentProcVisible.UseCompatibleTextRendering = true;
            // 
            // tabPageNewNote
            // 
            this.tabPageNewNote.Controls.Add(this.chxUseDateAsDefaultTitle);
            this.tabPageNewNote.Controls.Add(this.label3);
            this.tabPageNewNote.Controls.Add(this.label2);
            this.tabPageNewNote.Controls.Add(this.numNotesDefaultHeight);
            this.tabPageNewNote.Controls.Add(this.numNotesDefaultWidth);
            this.tabPageNewNote.Controls.Add(this.label1);
            this.tabPageNewNote.Controls.Add(this.chxUseRandomDefaultNote);
            this.tabPageNewNote.Controls.Add(this.lblDefaultNewNoteColor);
            this.tabPageNewNote.Controls.Add(this.cbxDefaultSkin);
            this.tabPageNewNote.Location = new System.Drawing.Point(4, 25);
            this.tabPageNewNote.Name = "tabPageNewNote";
            this.tabPageNewNote.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNewNote.Size = new System.Drawing.Size(433, 298);
            this.tabPageNewNote.TabIndex = 3;
            this.tabPageNewNote.Text = "New note";
            this.tabPageNewNote.UseVisualStyleBackColor = true;
            // 
            // chxUseDateAsDefaultTitle
            // 
            this.chxUseDateAsDefaultTitle.AutoSize = true;
            this.chxUseDateAsDefaultTitle.Checked = true;
            this.chxUseDateAsDefaultTitle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxUseDateAsDefaultTitle.Location = new System.Drawing.Point(23, 104);
            this.chxUseDateAsDefaultTitle.Name = "chxUseDateAsDefaultTitle";
            this.chxUseDateAsDefaultTitle.Size = new System.Drawing.Size(297, 20);
            this.chxUseDateAsDefaultTitle.TabIndex = 23;
            this.chxUseDateAsDefaultTitle.Text = "Use current date as default title for a new note.";
            this.chxUseDateAsDefaultTitle.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 193);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 20);
            this.label3.TabIndex = 22;
            this.label3.Text = "Height:";
            this.label3.UseCompatibleTextRendering = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 20);
            this.label2.TabIndex = 21;
            this.label2.Text = "Width:";
            this.label2.UseCompatibleTextRendering = true;
            // 
            // numNotesDefaultHeight
            // 
            this.numNotesDefaultHeight.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numNotesDefaultHeight.Location = new System.Drawing.Point(86, 191);
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
            this.numNotesDefaultHeight.Size = new System.Drawing.Size(55, 22);
            this.numNotesDefaultHeight.TabIndex = 20;
            this.numNotesDefaultHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numNotesDefaultHeight.Value = new decimal(new int[] {
            360,
            0,
            0,
            0});
            // 
            // numNotesDefaultWidth
            // 
            this.numNotesDefaultWidth.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numNotesDefaultWidth.Location = new System.Drawing.Point(86, 163);
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
            this.numNotesDefaultWidth.Size = new System.Drawing.Size(55, 22);
            this.numNotesDefaultWidth.TabIndex = 19;
            this.numNotesDefaultWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numNotesDefaultWidth.Value = new decimal(new int[] {
            420,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 20);
            this.label1.TabIndex = 18;
            this.label1.Text = "Default size new note:";
            this.label1.UseCompatibleTextRendering = true;
            // 
            // chxUseRandomDefaultNote
            // 
            this.chxUseRandomDefaultNote.AutoSize = true;
            this.chxUseRandomDefaultNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxUseRandomDefaultNote.Location = new System.Drawing.Point(22, 30);
            this.chxUseRandomDefaultNote.Name = "chxUseRandomDefaultNote";
            this.chxUseRandomDefaultNote.Size = new System.Drawing.Size(228, 21);
            this.chxUseRandomDefaultNote.TabIndex = 17;
            this.chxUseRandomDefaultNote.Text = "Use a random skin as default skin.";
            this.chxUseRandomDefaultNote.UseCompatibleTextRendering = true;
            this.chxUseRandomDefaultNote.UseVisualStyleBackColor = true;
            this.chxUseRandomDefaultNote.CheckedChanged += new System.EventHandler(this.chxUseRandomDefaultNote_CheckedChanged);
            // 
            // lblDefaultNewNoteColor
            // 
            this.lblDefaultNewNoteColor.AutoSize = true;
            this.lblDefaultNewNoteColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDefaultNewNoteColor.Location = new System.Drawing.Point(19, 56);
            this.lblDefaultNewNoteColor.Name = "lblDefaultNewNoteColor";
            this.lblDefaultNewNoteColor.Size = new System.Drawing.Size(143, 20);
            this.lblDefaultNewNoteColor.TabIndex = 15;
            this.lblDefaultNewNoteColor.Text = "Default skin new notes:";
            this.lblDefaultNewNoteColor.UseCompatibleTextRendering = true;
            // 
            // cbxDefaultSkin
            // 
            this.cbxDefaultSkin.AccessibleDescription = "Defaul color for new note.";
            this.cbxDefaultSkin.AccessibleRole = System.Windows.Forms.AccessibleRole.ComboBox;
            this.cbxDefaultSkin.BackColor = System.Drawing.Color.LightGray;
            this.cbxDefaultSkin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDefaultSkin.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbxDefaultSkin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxDefaultSkin.FormattingEnabled = true;
            this.cbxDefaultSkin.Location = new System.Drawing.Point(168, 53);
            this.cbxDefaultSkin.MaxDropDownItems = 5;
            this.cbxDefaultSkin.Name = "cbxDefaultSkin";
            this.cbxDefaultSkin.Size = new System.Drawing.Size(228, 24);
            this.cbxDefaultSkin.TabIndex = 16;
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
            this.tabPageFonts.Text = "Notes";
            this.tabPageFonts.UseVisualStyleBackColor = true;
            // 
            // cbxFontNoteTitleBold
            // 
            this.cbxFontNoteTitleBold.AutoSize = true;
            this.cbxFontNoteTitleBold.Checked = true;
            this.cbxFontNoteTitleBold.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxFontNoteTitleBold.Location = new System.Drawing.Point(235, 50);
            this.cbxFontNoteTitleBold.Name = "cbxFontNoteTitleBold";
            this.cbxFontNoteTitleBold.Size = new System.Drawing.Size(49, 21);
            this.cbxFontNoteTitleBold.TabIndex = 40;
            this.cbxFontNoteTitleBold.Text = "bold";
            this.cbxFontNoteTitleBold.UseCompatibleTextRendering = true;
            this.cbxFontNoteTitleBold.UseVisualStyleBackColor = true;
            // 
            // lblTextFontTitlePoints
            // 
            this.lblTextFontTitlePoints.AccessibleDescription = "points";
            this.lblTextFontTitlePoints.AutoSize = true;
            this.lblTextFontTitlePoints.Location = new System.Drawing.Point(202, 51);
            this.lblTextFontTitlePoints.Name = "lblTextFontTitlePoints";
            this.lblTextFontTitlePoints.Size = new System.Drawing.Size(20, 20);
            this.lblTextFontTitlePoints.TabIndex = 39;
            this.lblTextFontTitlePoints.Text = "pt.";
            this.lblTextFontTitlePoints.UseCompatibleTextRendering = true;
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
            6,
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
            this.lblTextFontTitleSize.Size = new System.Drawing.Size(87, 20);
            this.lblTextFontTitleSize.TabIndex = 37;
            this.lblTextFontTitleSize.Text = "Font size title:";
            this.lblTextFontTitleSize.UseCompatibleTextRendering = true;
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
            this.lblTextFontTitleFamily.Size = new System.Drawing.Size(60, 20);
            this.lblTextFontTitleFamily.TabIndex = 35;
            this.lblTextFontTitleFamily.Text = "Font title:";
            this.lblTextFontTitleFamily.UseCompatibleTextRendering = true;
            // 
            // lblTextDirection
            // 
            this.lblTextDirection.AccessibleDescription = "";
            this.lblTextDirection.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.lblTextDirection.AutoSize = true;
            this.lblTextDirection.Location = new System.Drawing.Point(21, 183);
            this.lblTextDirection.Name = "lblTextDirection";
            this.lblTextDirection.Size = new System.Drawing.Size(125, 20);
            this.lblTextDirection.TabIndex = 34;
            this.lblTextDirection.Text = "Text direction notes:";
            this.lblTextDirection.UseCompatibleTextRendering = true;
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
            this.lblTextFontContentPoints.Size = new System.Drawing.Size(20, 20);
            this.lblTextFontContentPoints.TabIndex = 32;
            this.lblTextFontContentPoints.Text = "pt.";
            this.lblTextFontContentPoints.UseCompatibleTextRendering = true;
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
            this.lblTextFontContentSize.Size = new System.Drawing.Size(150, 20);
            this.lblTextFontContentSize.TabIndex = 30;
            this.lblTextFontContentSize.Text = "default font size content:";
            this.lblTextFontContentSize.UseCompatibleTextRendering = true;
            // 
            // lblTextNoteFont
            // 
            this.lblTextNoteFont.AutoSize = true;
            this.lblTextNoteFont.Location = new System.Drawing.Point(30, 96);
            this.lblTextNoteFont.Name = "lblTextNoteFont";
            this.lblTextNoteFont.Size = new System.Drawing.Size(122, 20);
            this.lblTextNoteFont.TabIndex = 29;
            this.lblTextNoteFont.Text = "default font content:";
            this.lblTextNoteFont.UseCompatibleTextRendering = true;
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
            this.chxUseAlternativeTrayicon.Size = new System.Drawing.Size(296, 21);
            this.chxUseAlternativeTrayicon.TabIndex = 7;
            this.chxUseAlternativeTrayicon.Text = "Use alternative windows7 style/white trayicon.";
            this.chxUseAlternativeTrayicon.UseCompatibleTextRendering = true;
            this.chxUseAlternativeTrayicon.UseVisualStyleBackColor = true;
            // 
            // lblFontsizePoints
            // 
            this.lblFontsizePoints.AutoSize = true;
            this.lblFontsizePoints.Location = new System.Drawing.Point(185, 27);
            this.lblFontsizePoints.Name = "lblFontsizePoints";
            this.lblFontsizePoints.Size = new System.Drawing.Size(20, 20);
            this.lblFontsizePoints.TabIndex = 6;
            this.lblFontsizePoints.Text = "pt.";
            this.lblFontsizePoints.UseCompatibleTextRendering = true;
            // 
            // lblTextFontsizeMenu
            // 
            this.lblTextFontsizeMenu.AutoSize = true;
            this.lblTextFontsizeMenu.Location = new System.Drawing.Point(23, 27);
            this.lblTextFontsizeMenu.Name = "lblTextFontsizeMenu";
            this.lblTextFontsizeMenu.Size = new System.Drawing.Size(96, 20);
            this.lblTextFontsizeMenu.TabIndex = 5;
            this.lblTextFontsizeMenu.Text = "Fontsize  menu";
            this.lblTextFontsizeMenu.UseCompatibleTextRendering = true;
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
            1000,
            0,
            0,
            131072});
            // 
            // chxTrayiconBoldExit
            // 
            this.chxTrayiconBoldExit.AutoSize = true;
            this.chxTrayiconBoldExit.Location = new System.Drawing.Point(26, 129);
            this.chxTrayiconBoldExit.Name = "chxTrayiconBoldExit";
            this.chxTrayiconBoldExit.Size = new System.Drawing.Size(150, 21);
            this.chxTrayiconBoldExit.TabIndex = 3;
            this.chxTrayiconBoldExit.Text = "Display \"Exit\" in bold.";
            this.chxTrayiconBoldExit.UseCompatibleTextRendering = true;
            this.chxTrayiconBoldExit.UseVisualStyleBackColor = true;
            // 
            // chxTrayiconBoldSettings
            // 
            this.chxTrayiconBoldSettings.AutoSize = true;
            this.chxTrayiconBoldSettings.Location = new System.Drawing.Point(25, 106);
            this.chxTrayiconBoldSettings.Name = "chxTrayiconBoldSettings";
            this.chxTrayiconBoldSettings.Size = new System.Drawing.Size(176, 21);
            this.chxTrayiconBoldSettings.TabIndex = 2;
            this.chxTrayiconBoldSettings.Text = "Display \"Settings\" in bold.";
            this.chxTrayiconBoldSettings.UseCompatibleTextRendering = true;
            this.chxTrayiconBoldSettings.UseVisualStyleBackColor = true;
            // 
            // chxTrayiconBoldManagenotes
            // 
            this.chxTrayiconBoldManagenotes.AutoSize = true;
            this.chxTrayiconBoldManagenotes.Location = new System.Drawing.Point(25, 83);
            this.chxTrayiconBoldManagenotes.Name = "chxTrayiconBoldManagenotes";
            this.chxTrayiconBoldManagenotes.Size = new System.Drawing.Size(212, 21);
            this.chxTrayiconBoldManagenotes.TabIndex = 1;
            this.chxTrayiconBoldManagenotes.Text = "Display \"Manage notes\" in bold.";
            this.chxTrayiconBoldManagenotes.UseCompatibleTextRendering = true;
            this.chxTrayiconBoldManagenotes.UseVisualStyleBackColor = true;
            // 
            // chxTrayiconBoldNewnote
            // 
            this.chxTrayiconBoldNewnote.AutoSize = true;
            this.chxTrayiconBoldNewnote.Checked = true;
            this.chxTrayiconBoldNewnote.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxTrayiconBoldNewnote.Location = new System.Drawing.Point(25, 60);
            this.chxTrayiconBoldNewnote.Name = "chxTrayiconBoldNewnote";
            this.chxTrayiconBoldNewnote.Size = new System.Drawing.Size(237, 21);
            this.chxTrayiconBoldNewnote.TabIndex = 0;
            this.chxTrayiconBoldNewnote.Text = "Display \"Create a new note\" in bold.";
            this.chxTrayiconBoldNewnote.UseCompatibleTextRendering = true;
            this.chxTrayiconBoldNewnote.UseVisualStyleBackColor = true;
            // 
            // tabAppereanceManagenotes
            // 
            this.tabAppereanceManagenotes.Controls.Add(this.chxCaseSentiveSearch);
            this.tabAppereanceManagenotes.Controls.Add(this.label4);
            this.tabAppereanceManagenotes.Controls.Add(this.numManagenotesFont);
            this.tabAppereanceManagenotes.Controls.Add(this.lbTextManagesnotesFontSize);
            this.tabAppereanceManagenotes.Controls.Add(this.lblTextSkinManagenotes);
            this.tabAppereanceManagenotes.Controls.Add(this.cbxManageNotesSkin);
            this.tabAppereanceManagenotes.Controls.Add(this.cbxManagenotesTooltipContent);
            this.tabAppereanceManagenotes.Location = new System.Drawing.Point(4, 25);
            this.tabAppereanceManagenotes.Name = "tabAppereanceManagenotes";
            this.tabAppereanceManagenotes.Padding = new System.Windows.Forms.Padding(3);
            this.tabAppereanceManagenotes.Size = new System.Drawing.Size(433, 298);
            this.tabAppereanceManagenotes.TabIndex = 4;
            this.tabAppereanceManagenotes.Text = "Manage notes";
            this.tabAppereanceManagenotes.UseVisualStyleBackColor = true;
            // 
            // chxCaseSentiveSearch
            // 
            this.chxCaseSentiveSearch.AutoSize = true;
            this.chxCaseSentiveSearch.Checked = true;
            this.chxCaseSentiveSearch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxCaseSentiveSearch.Location = new System.Drawing.Point(19, 159);
            this.chxCaseSentiveSearch.Name = "chxCaseSentiveSearch";
            this.chxCaseSentiveSearch.Size = new System.Drawing.Size(178, 20);
            this.chxCaseSentiveSearch.TabIndex = 6;
            this.chxCaseSentiveSearch.Text = "Use case sentive search ";
            this.chxCaseSentiveSearch.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(237, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "pt.";
            // 
            // numManagenotesFont
            // 
            this.numManagenotesFont.DecimalPlaces = 2;
            this.numManagenotesFont.Location = new System.Drawing.Point(170, 120);
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
            this.numManagenotesFont.Size = new System.Drawing.Size(61, 22);
            this.numManagenotesFont.TabIndex = 4;
            this.numManagenotesFont.Value = new decimal(new int[] {
            1000,
            0,
            0,
            131072});
            // 
            // lbTextManagesnotesFontSize
            // 
            this.lbTextManagesnotesFontSize.AutoSize = true;
            this.lbTextManagesnotesFontSize.Location = new System.Drawing.Point(16, 122);
            this.lbTextManagesnotesFontSize.Name = "lbTextManagesnotesFontSize";
            this.lbTextManagesnotesFontSize.Size = new System.Drawing.Size(145, 16);
            this.lbTextManagesnotesFontSize.TabIndex = 3;
            this.lbTextManagesnotesFontSize.Text = "Manage notes font size";
            // 
            // lblTextSkinManagenotes
            // 
            this.lblTextSkinManagenotes.AutoSize = true;
            this.lblTextSkinManagenotes.Location = new System.Drawing.Point(19, 42);
            this.lblTextSkinManagenotes.Name = "lblTextSkinManagenotes";
            this.lblTextSkinManagenotes.Size = new System.Drawing.Size(186, 20);
            this.lblTextSkinManagenotes.TabIndex = 2;
            this.lblTextSkinManagenotes.Text = "Skin of Manage notes window:";
            this.lblTextSkinManagenotes.UseCompatibleTextRendering = true;
            // 
            // cbxManageNotesSkin
            // 
            this.cbxManageNotesSkin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxManageNotesSkin.FormattingEnabled = true;
            this.cbxManageNotesSkin.Location = new System.Drawing.Point(211, 39);
            this.cbxManageNotesSkin.Name = "cbxManageNotesSkin";
            this.cbxManageNotesSkin.Size = new System.Drawing.Size(145, 24);
            this.cbxManageNotesSkin.TabIndex = 1;
            // 
            // cbxManagenotesTooltipContent
            // 
            this.cbxManagenotesTooltipContent.AutoSize = true;
            this.cbxManagenotesTooltipContent.Location = new System.Drawing.Point(19, 82);
            this.cbxManagenotesTooltipContent.Name = "cbxManagenotesTooltipContent";
            this.cbxManagenotesTooltipContent.Size = new System.Drawing.Size(303, 21);
            this.cbxManagenotesTooltipContent.TabIndex = 0;
            this.cbxManagenotesTooltipContent.Text = "Show a tooltip with preview of the note content.";
            this.cbxManagenotesTooltipContent.UseCompatibleTextRendering = true;
            this.cbxManagenotesTooltipContent.UseVisualStyleBackColor = true;
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
            this.chxHighlightSQL.Size = new System.Drawing.Size(233, 21);
            this.chxHighlightSQL.TabIndex = 16;
            this.chxHighlightSQL.Text = "Highlight SQL text between quotes.";
            this.chxHighlightSQL.UseCompatibleTextRendering = true;
            this.chxHighlightSQL.UseVisualStyleBackColor = true;
            // 
            // chxHighlightPHP
            // 
            this.chxHighlightPHP.AutoSize = true;
            this.chxHighlightPHP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxHighlightPHP.Location = new System.Drawing.Point(23, 136);
            this.chxHighlightPHP.Name = "chxHighlightPHP";
            this.chxHighlightPHP.Size = new System.Drawing.Size(276, 21);
            this.chxHighlightPHP.TabIndex = 15;
            this.chxHighlightPHP.Text = "Highlight PHP text between <?php and ?>.";
            this.chxHighlightPHP.UseCompatibleTextRendering = true;
            this.chxHighlightPHP.UseVisualStyleBackColor = true;
            // 
            // chxHighlightHyperlinks
            // 
            this.chxHighlightHyperlinks.AutoSize = true;
            this.chxHighlightHyperlinks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxHighlightHyperlinks.Location = new System.Drawing.Point(23, 42);
            this.chxHighlightHyperlinks.Name = "chxHighlightHyperlinks";
            this.chxHighlightHyperlinks.Size = new System.Drawing.Size(179, 21);
            this.chxHighlightHyperlinks.TabIndex = 14;
            this.chxHighlightHyperlinks.Text = "Make hyperlinks clickable.";
            this.chxHighlightHyperlinks.UseCompatibleTextRendering = true;
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
            this.chxConfirmLink.Size = new System.Drawing.Size(292, 21);
            this.chxConfirmLink.TabIndex = 18;
            this.chxConfirmLink.Text = "Ask before launching URL, on click hyperlink.";
            this.chxConfirmLink.UseCompatibleTextRendering = true;
            this.chxConfirmLink.UseVisualStyleBackColor = true;
            // 
            // chxHighlightHTML
            // 
            this.chxHighlightHTML.AutoSize = true;
            this.chxHighlightHTML.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxHighlightHTML.Location = new System.Drawing.Point(23, 113);
            this.chxHighlightHTML.Name = "chxHighlightHTML";
            this.chxHighlightHTML.Size = new System.Drawing.Size(145, 21);
            this.chxHighlightHTML.TabIndex = 13;
            this.chxHighlightHTML.Text = "Highlight HTML text.";
            this.chxHighlightHTML.UseCompatibleTextRendering = true;
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
            this.chxSocialEmailEnabled.Size = new System.Drawing.Size(195, 21);
            this.chxSocialEmailEnabled.TabIndex = 25;
            this.chxSocialEmailEnabled.Text = "Enable E-mail in share menu";
            this.chxSocialEmailEnabled.UseCompatibleTextRendering = true;
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
            this.chxSocialEmailDefaultaddressSet.Size = new System.Drawing.Size(255, 21);
            this.chxSocialEmailDefaultaddressSet.TabIndex = 24;
            this.chxSocialEmailDefaultaddressSet.Text = "Set a default email address to send to: ";
            this.chxSocialEmailDefaultaddressSet.UseCompatibleTextRendering = true;
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
            this.chxUpdateSilentInstall.Size = new System.Drawing.Size(188, 21);
            this.chxUpdateSilentInstall.TabIndex = 35;
            this.chxUpdateSilentInstall.Text = "Install update setup silently.";
            this.chxUpdateSilentInstall.UseCompatibleTextRendering = true;
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
            this.btnGPGPathBrowse.UseCompatibleTextRendering = true;
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
            this.lblTextGPGPath.Size = new System.Drawing.Size(110, 20);
            this.lblTextGPGPath.TabIndex = 33;
            this.lblTextGPGPath.Text = "Location gpg.exe:";
            this.lblTextGPGPath.UseCompatibleTextRendering = true;
            // 
            // chxCheckUpdatesSignature
            // 
            this.chxCheckUpdatesSignature.AutoSize = true;
            this.chxCheckUpdatesSignature.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxCheckUpdatesSignature.Location = new System.Drawing.Point(11, 109);
            this.chxCheckUpdatesSignature.Name = "chxCheckUpdatesSignature";
            this.chxCheckUpdatesSignature.Size = new System.Drawing.Size(284, 21);
            this.chxCheckUpdatesSignature.TabIndex = 25;
            this.chxCheckUpdatesSignature.Text = "Verify the signature of downloaded updates.";
            this.chxCheckUpdatesSignature.UseCompatibleTextRendering = true;
            this.chxCheckUpdatesSignature.UseVisualStyleBackColor = true;
            this.chxCheckUpdatesSignature.CheckedChanged += new System.EventHandler(this.chxCheckUpdatesSignature_CheckedChanged);
            // 
            // chxCheckUpdates
            // 
            this.chxCheckUpdates.AutoSize = true;
            this.chxCheckUpdates.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxCheckUpdates.Location = new System.Drawing.Point(11, 18);
            this.chxCheckUpdates.Name = "chxCheckUpdates";
            this.chxCheckUpdates.Size = new System.Drawing.Size(132, 21);
            this.chxCheckUpdates.TabIndex = 26;
            this.chxCheckUpdates.Text = "Check for updates";
            this.chxCheckUpdates.UseCompatibleTextRendering = true;
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
            this.lblTextCheckforupdatesevery.Size = new System.Drawing.Size(153, 20);
            this.lblTextCheckforupdatesevery.TabIndex = 27;
            this.lblTextCheckforupdatesevery.Text = "Check for updates every ";
            this.lblTextCheckforupdatesevery.UseCompatibleTextRendering = true;
            // 
            // lblLatestUpdateCheck
            // 
            this.lblLatestUpdateCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLatestUpdateCheck.Location = new System.Drawing.Point(223, 193);
            this.lblLatestUpdateCheck.Name = "lblLatestUpdateCheck";
            this.lblLatestUpdateCheck.Size = new System.Drawing.Size(192, 16);
            this.lblLatestUpdateCheck.TabIndex = 32;
            // 
            // lblTextDayAtStartup
            // 
            this.lblTextDayAtStartup.AutoSize = true;
            this.lblTextDayAtStartup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextDayAtStartup.Location = new System.Drawing.Point(232, 43);
            this.lblTextDayAtStartup.Name = "lblTextDayAtStartup";
            this.lblTextDayAtStartup.Size = new System.Drawing.Size(96, 20);
            this.lblTextDayAtStartup.TabIndex = 29;
            this.lblTextDayAtStartup.Text = "days, at startup";
            this.lblTextDayAtStartup.UseCompatibleTextRendering = true;
            // 
            // lblTextLatestUpdateCheck
            // 
            this.lblTextLatestUpdateCheck.AutoSize = true;
            this.lblTextLatestUpdateCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextLatestUpdateCheck.Location = new System.Drawing.Point(10, 193);
            this.lblTextLatestUpdateCheck.Name = "lblTextLatestUpdateCheck";
            this.lblTextLatestUpdateCheck.Size = new System.Drawing.Size(213, 20);
            this.lblTextLatestUpdateCheck.TabIndex = 31;
            this.lblTextLatestUpdateCheck.Text = "Last update check is performed on:";
            this.lblTextLatestUpdateCheck.UseCompatibleTextRendering = true;
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
            this.btnCheckUpdates.UseCompatibleTextRendering = true;
            this.btnCheckUpdates.UseVisualStyleBackColor = false;
            this.btnCheckUpdates.Click += new System.EventHandler(this.btnCheckUpdates_Click);
            // 
            // tabProxy
            // 
            this.tabProxy.Controls.Add(this.iptbProxy);
            this.tabProxy.Controls.Add(this.lblTextMiliseconds);
            this.tabProxy.Controls.Add(this.numTimeout);
            this.tabProxy.Controls.Add(this.chxProxyEnabled);
            this.tabProxy.Controls.Add(this.lblTextNetworkTimeout);
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
            this.lblTextMiliseconds.Size = new System.Drawing.Size(23, 20);
            this.lblTextMiliseconds.TabIndex = 25;
            this.lblTextMiliseconds.Text = "ms";
            this.lblTextMiliseconds.UseCompatibleTextRendering = true;
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
            this.chxProxyEnabled.Size = new System.Drawing.Size(130, 21);
            this.chxProxyEnabled.TabIndex = 1;
            this.chxProxyEnabled.Text = "Use socked proxy";
            this.chxProxyEnabled.UseCompatibleTextRendering = true;
            this.chxProxyEnabled.UseVisualStyleBackColor = true;
            this.chxProxyEnabled.CheckedChanged += new System.EventHandler(this.chxUseProxy_CheckedChanged);
            // 
            // lblTextNetworkTimeout
            // 
            this.lblTextNetworkTimeout.AutoSize = true;
            this.lblTextNetworkTimeout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextNetworkTimeout.Location = new System.Drawing.Point(15, 113);
            this.lblTextNetworkTimeout.Name = "lblTextNetworkTimeout";
            this.lblTextNetworkTimeout.Size = new System.Drawing.Size(150, 20);
            this.lblTextNetworkTimeout.TabIndex = 24;
            this.lblTextNetworkTimeout.Text = "connection timeout time:";
            this.lblTextNetworkTimeout.UseCompatibleTextRendering = true;
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
            this.tabAdvance.Location = new System.Drawing.Point(4, 25);
            this.tabAdvance.Name = "tabAdvance";
            this.tabAdvance.Size = new System.Drawing.Size(447, 333);
            this.tabAdvance.TabIndex = 2;
            this.tabAdvance.Text = "Advance";
            this.tabAdvance.UseVisualStyleBackColor = true;
            // 
            // numWarnLimitVisible
            // 
            this.numWarnLimitVisible.Location = new System.Drawing.Point(182, 104);
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
            this.numWarnLimitVisible.Size = new System.Drawing.Size(49, 22);
            this.numWarnLimitVisible.TabIndex = 28;
            this.numWarnLimitVisible.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // chxLoadPlugins
            // 
            this.chxLoadPlugins.AccessibleDescription = "Allow NoteFly to load plugins";
            this.chxLoadPlugins.AutoSize = true;
            this.chxLoadPlugins.Checked = true;
            this.chxLoadPlugins.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxLoadPlugins.Location = new System.Drawing.Point(20, 121);
            this.chxLoadPlugins.Name = "chxLoadPlugins";
            this.chxLoadPlugins.Size = new System.Drawing.Size(146, 21);
            this.chxLoadPlugins.TabIndex = 25;
            this.chxLoadPlugins.Text = "Allow to load plugins";
            this.chxLoadPlugins.UseCompatibleTextRendering = true;
            this.chxLoadPlugins.UseVisualStyleBackColor = true;
            // 
            // lblTextVisibleNotesWarnLimit
            // 
            this.lblTextVisibleNotesWarnLimit.AutoSize = true;
            this.lblTextVisibleNotesWarnLimit.Location = new System.Drawing.Point(13, 106);
            this.lblTextVisibleNotesWarnLimit.Name = "lblTextVisibleNotesWarnLimit";
            this.lblTextVisibleNotesWarnLimit.Size = new System.Drawing.Size(163, 20);
            this.lblTextVisibleNotesWarnLimit.TabIndex = 27;
            this.lblTextVisibleNotesWarnLimit.Text = "Visible notes warning limit:";
            this.lblTextVisibleNotesWarnLimit.UseCompatibleTextRendering = true;
            // 
            // lblTextTotalNotesWarnLimit
            // 
            this.lblTextTotalNotesWarnLimit.AutoSize = true;
            this.lblTextTotalNotesWarnLimit.Location = new System.Drawing.Point(13, 83);
            this.lblTextTotalNotesWarnLimit.Name = "lblTextTotalNotesWarnLimit";
            this.lblTextTotalNotesWarnLimit.Size = new System.Drawing.Size(153, 20);
            this.lblTextTotalNotesWarnLimit.TabIndex = 26;
            this.lblTextTotalNotesWarnLimit.Text = "Total notes warning limit:";
            this.lblTextTotalNotesWarnLimit.UseCompatibleTextRendering = true;
            // 
            // numWarnLimitTotal
            // 
            this.numWarnLimitTotal.Location = new System.Drawing.Point(182, 81);
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
            this.numWarnLimitTotal.Size = new System.Drawing.Size(49, 22);
            this.numWarnLimitTotal.TabIndex = 25;
            this.numWarnLimitTotal.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            // 
            // chxLogExceptions
            // 
            this.chxLogExceptions.AutoSize = true;
            this.chxLogExceptions.Checked = true;
            this.chxLogExceptions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxLogExceptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxLogExceptions.Location = new System.Drawing.Point(16, 233);
            this.chxLogExceptions.Name = "chxLogExceptions";
            this.chxLogExceptions.Size = new System.Drawing.Size(299, 21);
            this.chxLogExceptions.TabIndex = 24;
            this.chxLogExceptions.Text = "Log exceptions (recommeded: leave enabled).";
            this.chxLogExceptions.UseCompatibleTextRendering = true;
            this.chxLogExceptions.UseVisualStyleBackColor = true;
            // 
            // lblTextLogging
            // 
            this.lblTextLogging.AutoSize = true;
            this.lblTextLogging.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextLogging.Location = new System.Drawing.Point(13, 168);
            this.lblTextLogging.Name = "lblTextLogging";
            this.lblTextLogging.Size = new System.Drawing.Size(56, 20);
            this.lblTextLogging.TabIndex = 23;
            this.lblTextLogging.Text = "Logging:";
            this.lblTextLogging.UseCompatibleTextRendering = true;
            // 
            // chxLogDebug
            // 
            this.chxLogDebug.AutoSize = true;
            this.chxLogDebug.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxLogDebug.Location = new System.Drawing.Point(16, 187);
            this.chxLogDebug.Name = "chxLogDebug";
            this.chxLogDebug.Size = new System.Drawing.Size(116, 21);
            this.chxLogDebug.TabIndex = 22;
            this.chxLogDebug.Text = "Log debug info.";
            this.chxLogDebug.UseCompatibleTextRendering = true;
            this.chxLogDebug.UseVisualStyleBackColor = true;
            // 
            // btnResetSettings
            // 
            this.btnResetSettings.BackColor = System.Drawing.Color.LightGray;
            this.btnResetSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetSettings.Location = new System.Drawing.Point(126, 272);
            this.btnResetSettings.Name = "btnResetSettings";
            this.btnResetSettings.Size = new System.Drawing.Size(195, 25);
            this.btnResetSettings.TabIndex = 21;
            this.btnResetSettings.Text = "&Reset all settings to default";
            this.btnResetSettings.UseCompatibleTextRendering = true;
            this.btnResetSettings.UseVisualStyleBackColor = false;
            this.btnResetSettings.Click += new System.EventHandler(this.btnResetSettings_Click);
            // 
            // chxLogErrors
            // 
            this.chxLogErrors.AutoSize = true;
            this.chxLogErrors.Checked = true;
            this.chxLogErrors.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxLogErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chxLogErrors.Location = new System.Drawing.Point(16, 210);
            this.chxLogErrors.Name = "chxLogErrors";
            this.chxLogErrors.Size = new System.Drawing.Size(118, 21);
            this.chxLogErrors.TabIndex = 19;
            this.chxLogErrors.Text = "Log user errors.";
            this.chxLogErrors.UseCompatibleTextRendering = true;
            this.chxLogErrors.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.LightGray;
            this.btnBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowse.Location = new System.Drawing.Point(369, 46);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(68, 25);
            this.btnBrowse.TabIndex = 15;
            this.btnBrowse.Text = "browse";
            this.btnBrowse.UseCompatibleTextRendering = true;
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblTextNoteLocation
            // 
            this.lblTextNoteLocation.AutoSize = true;
            this.lblTextNoteLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextNoteLocation.Location = new System.Drawing.Point(13, 23);
            this.lblTextNoteLocation.Name = "lblTextNoteLocation";
            this.lblTextNoteLocation.Size = new System.Drawing.Size(89, 20);
            this.lblTextNoteLocation.TabIndex = 16;
            this.lblTextNoteLocation.Text = "Save notes in:";
            this.lblTextNoteLocation.UseCompatibleTextRendering = true;
            // 
            // tbNotesSavePath
            // 
            this.tbNotesSavePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNotesSavePath.Location = new System.Drawing.Point(15, 46);
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
            this.chxSettingsExpertEnabled.Size = new System.Drawing.Size(112, 21);
            this.chxSettingsExpertEnabled.TabIndex = 25;
            this.chxSettingsExpertEnabled.Text = "E&xpert settings";
            this.chxSettingsExpertEnabled.UseCompatibleTextRendering = true;
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
            // iptbProxy
            // 
            this.iptbProxy.Enabled = false;
            this.iptbProxy.Location = new System.Drawing.Point(16, 57);
            this.iptbProxy.Name = "iptbProxy";
            this.iptbProxy.Size = new System.Drawing.Size(238, 22);
            this.iptbProxy.TabIndex = 26;
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
    }
}