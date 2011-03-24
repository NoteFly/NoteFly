//-----------------------------------------------------------------------
// <copyright file="FrmSettings.Designer.cs" company="GNU">
// 
// This program is free software; you can redistribute it and/or modify it
// Free Software Foundation; either version 2, 
// or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// </copyright>
//-----------------------------------------------------------------------
#define windows //platform can be: windows, linux, macos

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
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogNotessavepath;

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
        /// tabpage
        /// </summary>
        private System.Windows.Forms.TabPage tabNetwerk;

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
        private System.Windows.Forms.TabControl tabControlSocialNetworks;

        /// <summary>
        /// TabPage tabEmail
        /// </summary>
        private System.Windows.Forms.TabPage tabEmail;

        /// <summary>
        /// CheckBox chxSocialEmailDefaultaddressBlank
        /// </summary>
        private System.Windows.Forms.CheckBox chxSocialEmailDefaultaddressBlank;

        /// <summary>
        /// Label lblTextDefaultEmail
        /// </summary>
        private System.Windows.Forms.Label lblTextDefaultEmail;

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
        private System.Windows.Forms.CheckBox cbxShowTooltips;

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
            this.cbxShowTooltips = new System.Windows.Forms.CheckBox();
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
            this.chxTrayiconBoldExit = new System.Windows.Forms.CheckBox();
            this.chxTrayiconBoldSettings = new System.Windows.Forms.CheckBox();
            this.chxTrayiconBoldManagenotes = new System.Windows.Forms.CheckBox();
            this.chxTrayiconBoldNewnote = new System.Windows.Forms.CheckBox();
            this.tabHighlight = new System.Windows.Forms.TabPage();
            this.chxHighlightSQL = new System.Windows.Forms.CheckBox();
            this.chxHighlightPHP = new System.Windows.Forms.CheckBox();
            this.chxHighlightHyperlinks = new System.Windows.Forms.CheckBox();
            this.chxConfirmLink = new System.Windows.Forms.CheckBox();
            this.chxHighlightHTML = new System.Windows.Forms.CheckBox();
            this.tabSharing = new System.Windows.Forms.TabPage();
            this.tabControlSocialNetworks = new System.Windows.Forms.TabControl();
            this.tabEmail = new System.Windows.Forms.TabPage();
            this.chxSocialEmailEnabled = new System.Windows.Forms.CheckBox();
            this.chxSocialEmailDefaultaddressBlank = new System.Windows.Forms.CheckBox();
            this.lblTextDefaultEmail = new System.Windows.Forms.Label();
            this.tbDefaultEmail = new System.Windows.Forms.TextBox();
            this.tabNetwerk = new System.Windows.Forms.TabPage();
            this.lblTextDayAtStartup = new System.Windows.Forms.Label();
            this.numUpdateCheckDays = new System.Windows.Forms.NumericUpDown();
            this.lblTextCheckforupdatesevery = new System.Windows.Forms.Label();
            this.chxCheckUpdates = new System.Windows.Forms.CheckBox();
            this.lblTextNetworkMiliseconds = new System.Windows.Forms.Label();
            this.chxProxyEnabled = new System.Windows.Forms.CheckBox();
            this.numTimeout = new System.Windows.Forms.NumericUpDown();
            this.lblTextNetworkTimeout = new System.Windows.Forms.Label();
            this.tabAdvance = new System.Windows.Forms.TabPage();
            this.chxLogExceptions = new System.Windows.Forms.CheckBox();
            this.lblTextLogging = new System.Windows.Forms.Label();
            this.chxLogDebug = new System.Windows.Forms.CheckBox();
            this.btnResetSettings = new System.Windows.Forms.Button();
            this.chxLogErrors = new System.Windows.Forms.CheckBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblTextNoteLocation = new System.Windows.Forms.Label();
            this.tbNotesSavePath = new System.Windows.Forms.TextBox();
            this.folderBrowserDialogNotessavepath = new System.Windows.Forms.FolderBrowserDialog();
            this.numTrayiconFontsize = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFontsizePoints = new System.Windows.Forms.Label();
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
            this.tabHighlight.SuspendLayout();
            this.tabSharing.SuspendLayout();
            this.tabControlSocialNetworks.SuspendLayout();
            this.tabEmail.SuspendLayout();
            this.tabNetwerk.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpdateCheckDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).BeginInit();
            this.tabAdvance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTrayiconFontsize)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.BackColor = System.Drawing.Color.LightGray;
            this.btnOK.Location = new System.Drawing.Point(198, 313);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(184, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.BackColor = System.Drawing.Color.LightGray;
            this.btnCancel.Location = new System.Drawing.Point(0, 313);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(188, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.Controls.Add(this.tabGeneral);
            this.tabControlSettings.Controls.Add(this.tabAppearance);
            this.tabControlSettings.Controls.Add(this.tabHighlight);
            this.tabControlSettings.Controls.Add(this.tabSharing);
            this.tabControlSettings.Controls.Add(this.tabNetwerk);
            this.tabControlSettings.Controls.Add(this.tabAdvance);
            this.tabControlSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControlSettings.HotTrack = true;
            this.tabControlSettings.Location = new System.Drawing.Point(0, 0);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            this.tabControlSettings.Size = new System.Drawing.Size(394, 307);
            this.tabControlSettings.TabIndex = 17;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.chxNotesDeleteRecyclebin);
            this.tabGeneral.Controls.Add(this.chxConfirmDeletenote);
            this.tabGeneral.Controls.Add(this.cbxActionLeftclick);
            this.tabGeneral.Controls.Add(this.chxConfirmExit);
            this.tabGeneral.Controls.Add(this.chxStartOnLogin);
            this.tabGeneral.Controls.Add(this.lblTextActionLeftClicktTrayicon);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(386, 281);
            this.tabGeneral.TabIndex = 3;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // chxNotesDeleteRecyclebin
            // 
            this.chxNotesDeleteRecyclebin.AutoSize = true;
            this.chxNotesDeleteRecyclebin.Location = new System.Drawing.Point(20, 98);
            this.chxNotesDeleteRecyclebin.Name = "chxNotesDeleteRecyclebin";
            this.chxNotesDeleteRecyclebin.Size = new System.Drawing.Size(189, 17);
            this.chxNotesDeleteRecyclebin.TabIndex = 24;
            this.chxNotesDeleteRecyclebin.Text = "Move deleted notes to recycle bin.";
            this.chxNotesDeleteRecyclebin.UseVisualStyleBackColor = true;
            // 
            // chxConfirmDeletenote
            // 
            this.chxConfirmDeletenote.AutoSize = true;
            this.chxConfirmDeletenote.Checked = true;
            this.chxConfirmDeletenote.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxConfirmDeletenote.Location = new System.Drawing.Point(20, 74);
            this.chxConfirmDeletenote.Name = "chxConfirmDeletenote";
            this.chxConfirmDeletenote.Size = new System.Drawing.Size(133, 17);
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
            this.cbxActionLeftclick.Location = new System.Drawing.Point(147, 136);
            this.cbxActionLeftclick.Name = "cbxActionLeftclick";
            this.cbxActionLeftclick.Size = new System.Drawing.Size(163, 21);
            this.cbxActionLeftclick.TabIndex = 16;
            // 
            // chxConfirmExit
            // 
            this.chxConfirmExit.AutoSize = true;
            this.chxConfirmExit.Location = new System.Drawing.Point(20, 51);
            this.chxConfirmExit.Name = "chxConfirmExit";
            this.chxConfirmExit.Size = new System.Drawing.Size(167, 17);
            this.chxConfirmExit.TabIndex = 20;
            this.chxConfirmExit.Text = "Confirm shutdown application.";
            this.chxConfirmExit.UseVisualStyleBackColor = true;
            // 
            // chxStartOnLogin
            // 
            this.chxStartOnLogin.AutoSize = true;
            this.chxStartOnLogin.Location = new System.Drawing.Point(20, 28);
            this.chxStartOnLogin.Name = "chxStartOnLogin";
            this.chxStartOnLogin.Size = new System.Drawing.Size(159, 17);
            this.chxStartOnLogin.TabIndex = 10;
            this.chxStartOnLogin.Text = "Start automatically on logon.";
            this.chxStartOnLogin.UseVisualStyleBackColor = true;
            // 
            // lblTextActionLeftClicktTrayicon
            // 
            this.lblTextActionLeftClicktTrayicon.AutoSize = true;
            this.lblTextActionLeftClicktTrayicon.Location = new System.Drawing.Point(19, 139);
            this.lblTextActionLeftClicktTrayicon.Name = "lblTextActionLeftClicktTrayicon";
            this.lblTextActionLeftClicktTrayicon.Size = new System.Drawing.Size(122, 13);
            this.lblTextActionLeftClicktTrayicon.TabIndex = 15;
            this.lblTextActionLeftClicktTrayicon.Text = "Action left click trayicon:";
            // 
            // tabAppearance
            // 
            this.tabAppearance.Controls.Add(this.tabAppearanceColors);
            this.tabAppearance.Location = new System.Drawing.Point(4, 22);
            this.tabAppearance.Name = "tabAppearance";
            this.tabAppearance.Padding = new System.Windows.Forms.Padding(3);
            this.tabAppearance.Size = new System.Drawing.Size(386, 281);
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
            this.tabAppearanceColors.Size = new System.Drawing.Size(380, 275);
            this.tabAppearanceColors.TabIndex = 28;
            // 
            // tabPageLooks
            // 
            this.tabPageLooks.Controls.Add(this.chxUseRandomDefaultNote);
            this.tabPageLooks.Controls.Add(this.cbxShowTooltips);
            this.tabPageLooks.Controls.Add(this.chxTransparecy);
            this.tabPageLooks.Controls.Add(this.lblDefaultNewNoteColor);
            this.tabPageLooks.Controls.Add(this.cbxDefaultColor);
            this.tabPageLooks.Controls.Add(this.numProcTransparency);
            this.tabPageLooks.Controls.Add(this.lblTextTransparentProcVisible);
            this.tabPageLooks.Location = new System.Drawing.Point(4, 22);
            this.tabPageLooks.Name = "tabPageLooks";
            this.tabPageLooks.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLooks.Size = new System.Drawing.Size(372, 249);
            this.tabPageLooks.TabIndex = 0;
            this.tabPageLooks.Text = "Looks";
            this.tabPageLooks.UseVisualStyleBackColor = true;
            // 
            // chxUseRandomDefaultNote
            // 
            this.chxUseRandomDefaultNote.AutoSize = true;
            this.chxUseRandomDefaultNote.Location = new System.Drawing.Point(18, 120);
            this.chxUseRandomDefaultNote.Name = "chxUseRandomDefaultNote";
            this.chxUseRandomDefaultNote.Size = new System.Drawing.Size(191, 17);
            this.chxUseRandomDefaultNote.TabIndex = 14;
            this.chxUseRandomDefaultNote.Text = "use random default note skin color.";
            this.chxUseRandomDefaultNote.UseVisualStyleBackColor = true;
            this.chxUseRandomDefaultNote.CheckedChanged += new System.EventHandler(this.chxUseRandomDefaultNote_CheckedChanged);
            // 
            // cbxShowTooltips
            // 
            this.cbxShowTooltips.AutoSize = true;
            this.cbxShowTooltips.Location = new System.Drawing.Point(18, 62);
            this.cbxShowTooltips.Name = "cbxShowTooltips";
            this.cbxShowTooltips.Size = new System.Drawing.Size(89, 17);
            this.cbxShowTooltips.TabIndex = 13;
            this.cbxShowTooltips.Text = "Show tooltips";
            this.cbxShowTooltips.UseVisualStyleBackColor = true;
            // 
            // chxTransparecy
            // 
            this.chxTransparecy.AutoSize = true;
            this.chxTransparecy.BackColor = System.Drawing.SystemColors.Control;
            this.chxTransparecy.CausesValidation = false;
            this.chxTransparecy.Checked = true;
            this.chxTransparecy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxTransparecy.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chxTransparecy.Location = new System.Drawing.Point(18, 28);
            this.chxTransparecy.Name = "chxTransparecy";
            this.chxTransparecy.Size = new System.Drawing.Size(158, 18);
            this.chxTransparecy.TabIndex = 8;
            this.chxTransparecy.Text = "Enable transparency notes";
            this.chxTransparecy.UseVisualStyleBackColor = false;
            this.chxTransparecy.CheckedChanged += new System.EventHandler(this.chxTransparecy_CheckedChanged);
            // 
            // lblDefaultNewNoteColor
            // 
            this.lblDefaultNewNoteColor.AutoSize = true;
            this.lblDefaultNewNoteColor.Location = new System.Drawing.Point(15, 146);
            this.lblDefaultNewNoteColor.Name = "lblDefaultNewNoteColor";
            this.lblDefaultNewNoteColor.Size = new System.Drawing.Size(118, 13);
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
            this.cbxDefaultColor.FormattingEnabled = true;
            this.cbxDefaultColor.Location = new System.Drawing.Point(142, 143);
            this.cbxDefaultColor.MaxDropDownItems = 5;
            this.cbxDefaultColor.Name = "cbxDefaultColor";
            this.cbxDefaultColor.Size = new System.Drawing.Size(139, 21);
            this.cbxDefaultColor.TabIndex = 10;
            // 
            // numProcTransparency
            // 
            this.numProcTransparency.BackColor = System.Drawing.Color.LightGray;
            this.numProcTransparency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numProcTransparency.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numProcTransparency.Location = new System.Drawing.Point(187, 26);
            this.numProcTransparency.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numProcTransparency.Name = "numProcTransparency";
            this.numProcTransparency.Size = new System.Drawing.Size(46, 20);
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
            this.lblTextTransparentProcVisible.Location = new System.Drawing.Point(234, 30);
            this.lblTextTransparentProcVisible.Name = "lblTextTransparentProcVisible";
            this.lblTextTransparentProcVisible.Size = new System.Drawing.Size(47, 13);
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
            this.tabPageFonts.Location = new System.Drawing.Point(4, 22);
            this.tabPageFonts.Name = "tabPageFonts";
            this.tabPageFonts.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFonts.Size = new System.Drawing.Size(372, 249);
            this.tabPageFonts.TabIndex = 1;
            this.tabPageFonts.Text = "Fonts";
            this.tabPageFonts.UseVisualStyleBackColor = true;
            // 
            // cbxFontNoteTitleBold
            // 
            this.cbxFontNoteTitleBold.AutoSize = true;
            this.cbxFontNoteTitleBold.Checked = true;
            this.cbxFontNoteTitleBold.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxFontNoteTitleBold.Location = new System.Drawing.Point(215, 50);
            this.cbxFontNoteTitleBold.Name = "cbxFontNoteTitleBold";
            this.cbxFontNoteTitleBold.Size = new System.Drawing.Size(46, 17);
            this.cbxFontNoteTitleBold.TabIndex = 40;
            this.cbxFontNoteTitleBold.Text = "bold";
            this.cbxFontNoteTitleBold.UseVisualStyleBackColor = true;
            // 
            // lblTextFontTitlePoints
            // 
            this.lblTextFontTitlePoints.AutoSize = true;
            this.lblTextFontTitlePoints.Location = new System.Drawing.Point(182, 51);
            this.lblTextFontTitlePoints.Name = "lblTextFontTitlePoints";
            this.lblTextFontTitlePoints.Size = new System.Drawing.Size(16, 13);
            this.lblTextFontTitlePoints.TabIndex = 39;
            this.lblTextFontTitlePoints.Text = "pt";
            // 
            // numFontSizeTitle
            // 
            this.numFontSizeTitle.Location = new System.Drawing.Point(138, 49);
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
            this.numFontSizeTitle.Size = new System.Drawing.Size(38, 20);
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
            this.lblTextFontTitleSize.Size = new System.Drawing.Size(71, 13);
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
            this.cbxFontNoteTitle.Location = new System.Drawing.Point(138, 22);
            this.cbxFontNoteTitle.Name = "cbxFontNoteTitle";
            this.cbxFontNoteTitle.Size = new System.Drawing.Size(182, 21);
            this.cbxFontNoteTitle.TabIndex = 36;
            // 
            // lblTextFontTitleFamily
            // 
            this.lblTextFontTitleFamily.AutoSize = true;
            this.lblTextFontTitleFamily.Location = new System.Drawing.Point(82, 25);
            this.lblTextFontTitleFamily.Name = "lblTextFontTitleFamily";
            this.lblTextFontTitleFamily.Size = new System.Drawing.Size(50, 13);
            this.lblTextFontTitleFamily.TabIndex = 35;
            this.lblTextFontTitleFamily.Text = "Font title:";
            // 
            // lblTextDirection
            // 
            this.lblTextDirection.AccessibleDescription = "";
            this.lblTextDirection.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.lblTextDirection.AutoSize = true;
            this.lblTextDirection.Location = new System.Drawing.Point(50, 183);
            this.lblTextDirection.Name = "lblTextDirection";
            this.lblTextDirection.Size = new System.Drawing.Size(71, 13);
            this.lblTextDirection.TabIndex = 34;
            this.lblTextDirection.Text = "Text direction";
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
            this.cbxTextDirection.Location = new System.Drawing.Point(138, 180);
            this.cbxTextDirection.Name = "cbxTextDirection";
            this.cbxTextDirection.Size = new System.Drawing.Size(182, 21);
            this.cbxTextDirection.TabIndex = 33;
            // 
            // lblTextFontContentPoints
            // 
            this.lblTextFontContentPoints.AutoSize = true;
            this.lblTextFontContentPoints.Location = new System.Drawing.Point(182, 130);
            this.lblTextFontContentPoints.Name = "lblTextFontContentPoints";
            this.lblTextFontContentPoints.Size = new System.Drawing.Size(16, 13);
            this.lblTextFontContentPoints.TabIndex = 32;
            this.lblTextFontContentPoints.Text = "pt";
            // 
            // numFontSizeContent
            // 
            this.numFontSizeContent.Location = new System.Drawing.Point(138, 123);
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
            this.numFontSizeContent.Size = new System.Drawing.Size(38, 20);
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
            this.lblTextFontContentSize.Location = new System.Drawing.Point(9, 125);
            this.lblTextFontContentSize.Name = "lblTextFontContentSize";
            this.lblTextFontContentSize.Size = new System.Drawing.Size(123, 13);
            this.lblTextFontContentSize.TabIndex = 30;
            this.lblTextFontContentSize.Text = "default font size content:";
            // 
            // lblTextNoteFont
            // 
            this.lblTextNoteFont.AutoSize = true;
            this.lblTextNoteFont.Location = new System.Drawing.Point(30, 101);
            this.lblTextNoteFont.Name = "lblTextNoteFont";
            this.lblTextNoteFont.Size = new System.Drawing.Size(102, 13);
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
            this.cbxFontNoteContent.Location = new System.Drawing.Point(138, 93);
            this.cbxFontNoteContent.Name = "cbxFontNoteContent";
            this.cbxFontNoteContent.Size = new System.Drawing.Size(182, 21);
            this.cbxFontNoteContent.TabIndex = 28;
            // 
            // tabPageTrayicon
            // 
            this.tabPageTrayicon.Controls.Add(this.lblFontsizePoints);
            this.tabPageTrayicon.Controls.Add(this.label1);
            this.tabPageTrayicon.Controls.Add(this.numTrayiconFontsize);
            this.tabPageTrayicon.Controls.Add(this.chxTrayiconBoldExit);
            this.tabPageTrayicon.Controls.Add(this.chxTrayiconBoldSettings);
            this.tabPageTrayicon.Controls.Add(this.chxTrayiconBoldManagenotes);
            this.tabPageTrayicon.Controls.Add(this.chxTrayiconBoldNewnote);
            this.tabPageTrayicon.Location = new System.Drawing.Point(4, 22);
            this.tabPageTrayicon.Name = "tabPageTrayicon";
            this.tabPageTrayicon.Size = new System.Drawing.Size(372, 249);
            this.tabPageTrayicon.TabIndex = 2;
            this.tabPageTrayicon.Text = "Trayicon";
            this.tabPageTrayicon.UseVisualStyleBackColor = true;
            // 
            // chxTrayiconBoldExit
            // 
            this.chxTrayiconBoldExit.AutoSize = true;
            this.chxTrayiconBoldExit.Location = new System.Drawing.Point(25, 180);
            this.chxTrayiconBoldExit.Name = "chxTrayiconBoldExit";
            this.chxTrayiconBoldExit.Size = new System.Drawing.Size(127, 17);
            this.chxTrayiconBoldExit.TabIndex = 3;
            this.chxTrayiconBoldExit.Text = "Display \"Exit\" in bold.";
            this.chxTrayiconBoldExit.UseVisualStyleBackColor = true;
            // 
            // chxTrayiconBoldSettings
            // 
            this.chxTrayiconBoldSettings.AutoSize = true;
            this.chxTrayiconBoldSettings.Location = new System.Drawing.Point(25, 144);
            this.chxTrayiconBoldSettings.Name = "chxTrayiconBoldSettings";
            this.chxTrayiconBoldSettings.Size = new System.Drawing.Size(148, 17);
            this.chxTrayiconBoldSettings.TabIndex = 2;
            this.chxTrayiconBoldSettings.Text = "Display \"Settings\" in bold.";
            this.chxTrayiconBoldSettings.UseVisualStyleBackColor = true;
            // 
            // chxTrayiconBoldManagenotes
            // 
            this.chxTrayiconBoldManagenotes.AutoSize = true;
            this.chxTrayiconBoldManagenotes.Location = new System.Drawing.Point(25, 107);
            this.chxTrayiconBoldManagenotes.Name = "chxTrayiconBoldManagenotes";
            this.chxTrayiconBoldManagenotes.Size = new System.Drawing.Size(178, 17);
            this.chxTrayiconBoldManagenotes.TabIndex = 1;
            this.chxTrayiconBoldManagenotes.Text = "Display \"Manage notes\" in bold.";
            this.chxTrayiconBoldManagenotes.UseVisualStyleBackColor = true;
            // 
            // chxTrayiconBoldNewnote
            // 
            this.chxTrayiconBoldNewnote.AutoSize = true;
            this.chxTrayiconBoldNewnote.Checked = true;
            this.chxTrayiconBoldNewnote.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxTrayiconBoldNewnote.Location = new System.Drawing.Point(25, 72);
            this.chxTrayiconBoldNewnote.Name = "chxTrayiconBoldNewnote";
            this.chxTrayiconBoldNewnote.Size = new System.Drawing.Size(197, 17);
            this.chxTrayiconBoldNewnote.TabIndex = 0;
            this.chxTrayiconBoldNewnote.Text = "Display \"Create a new note\" in bold.";
            this.chxTrayiconBoldNewnote.UseVisualStyleBackColor = true;
            // 
            // tabHighlight
            // 
            this.tabHighlight.Controls.Add(this.chxHighlightSQL);
            this.tabHighlight.Controls.Add(this.chxHighlightPHP);
            this.tabHighlight.Controls.Add(this.chxHighlightHyperlinks);
            this.tabHighlight.Controls.Add(this.chxConfirmLink);
            this.tabHighlight.Controls.Add(this.chxHighlightHTML);
            this.tabHighlight.Location = new System.Drawing.Point(4, 22);
            this.tabHighlight.Name = "tabHighlight";
            this.tabHighlight.Padding = new System.Windows.Forms.Padding(3);
            this.tabHighlight.Size = new System.Drawing.Size(386, 281);
            this.tabHighlight.TabIndex = 5;
            this.tabHighlight.Text = "Highlight";
            this.tabHighlight.UseVisualStyleBackColor = true;
            // 
            // chxHighlightSQL
            // 
            this.chxHighlightSQL.AutoSize = true;
            this.chxHighlightSQL.Location = new System.Drawing.Point(37, 189);
            this.chxHighlightSQL.Name = "chxHighlightSQL";
            this.chxHighlightSQL.Size = new System.Drawing.Size(177, 17);
            this.chxHighlightSQL.TabIndex = 16;
            this.chxHighlightSQL.Text = "Highlight SQL text. (not finished)";
            this.chxHighlightSQL.UseVisualStyleBackColor = true;
            // 
            // chxHighlightPHP
            // 
            this.chxHighlightPHP.AutoSize = true;
            this.chxHighlightPHP.Location = new System.Drawing.Point(37, 156);
            this.chxHighlightPHP.Name = "chxHighlightPHP";
            this.chxHighlightPHP.Size = new System.Drawing.Size(178, 17);
            this.chxHighlightPHP.TabIndex = 15;
            this.chxHighlightPHP.Text = "Highlight PHP text. (not finished)";
            this.chxHighlightPHP.UseVisualStyleBackColor = true;
            // 
            // chxHighlightHyperlinks
            // 
            this.chxHighlightHyperlinks.AutoSize = true;
            this.chxHighlightHyperlinks.Location = new System.Drawing.Point(37, 47);
            this.chxHighlightHyperlinks.Name = "chxHighlightHyperlinks";
            this.chxHighlightHyperlinks.Size = new System.Drawing.Size(151, 17);
            this.chxHighlightHyperlinks.TabIndex = 14;
            this.chxHighlightHyperlinks.Text = "Make hyperlinks clickable.";
            this.chxHighlightHyperlinks.UseVisualStyleBackColor = true;
            // 
            // chxConfirmLink
            // 
            this.chxConfirmLink.AutoSize = true;
            this.chxConfirmLink.Checked = true;
            this.chxConfirmLink.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxConfirmLink.Location = new System.Drawing.Point(37, 70);
            this.chxConfirmLink.Name = "chxConfirmLink";
            this.chxConfirmLink.Size = new System.Drawing.Size(194, 17);
            this.chxConfirmLink.TabIndex = 18;
            this.chxConfirmLink.Text = "Ask before launching URL, on click";
            this.chxConfirmLink.UseVisualStyleBackColor = true;
            // 
            // chxHighlightHTML
            // 
            this.chxHighlightHTML.AutoSize = true;
            this.chxHighlightHTML.Location = new System.Drawing.Point(37, 118);
            this.chxHighlightHTML.Name = "chxHighlightHTML";
            this.chxHighlightHTML.Size = new System.Drawing.Size(123, 17);
            this.chxHighlightHTML.TabIndex = 13;
            this.chxHighlightHTML.Text = "Highlight HTML text.";
            this.chxHighlightHTML.UseVisualStyleBackColor = true;
            // 
            // tabSharing
            // 
            this.tabSharing.Controls.Add(this.tabControlSocialNetworks);
            this.tabSharing.Location = new System.Drawing.Point(4, 22);
            this.tabSharing.Name = "tabSharing";
            this.tabSharing.Padding = new System.Windows.Forms.Padding(3);
            this.tabSharing.Size = new System.Drawing.Size(386, 281);
            this.tabSharing.TabIndex = 1;
            this.tabSharing.Text = "Sharing";
            this.tabSharing.UseVisualStyleBackColor = true;
            // 
            // tabControlSocialNetworks
            // 
            this.tabControlSocialNetworks.Controls.Add(this.tabEmail);
            this.tabControlSocialNetworks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlSocialNetworks.Location = new System.Drawing.Point(3, 3);
            this.tabControlSocialNetworks.Name = "tabControlSocialNetworks";
            this.tabControlSocialNetworks.SelectedIndex = 0;
            this.tabControlSocialNetworks.Size = new System.Drawing.Size(380, 275);
            this.tabControlSocialNetworks.TabIndex = 14;
            // 
            // tabEmail
            // 
            this.tabEmail.Controls.Add(this.chxSocialEmailEnabled);
            this.tabEmail.Controls.Add(this.chxSocialEmailDefaultaddressBlank);
            this.tabEmail.Controls.Add(this.lblTextDefaultEmail);
            this.tabEmail.Controls.Add(this.tbDefaultEmail);
            this.tabEmail.Location = new System.Drawing.Point(4, 22);
            this.tabEmail.Name = "tabEmail";
            this.tabEmail.Size = new System.Drawing.Size(372, 249);
            this.tabEmail.TabIndex = 2;
            this.tabEmail.Text = "Email";
            this.tabEmail.UseVisualStyleBackColor = true;
            // 
            // chxSocialEmailEnabled
            // 
            this.chxSocialEmailEnabled.AutoSize = true;
            this.chxSocialEmailEnabled.Location = new System.Drawing.Point(14, 27);
            this.chxSocialEmailEnabled.Name = "chxSocialEmailEnabled";
            this.chxSocialEmailEnabled.Size = new System.Drawing.Size(159, 17);
            this.chxSocialEmailEnabled.TabIndex = 25;
            this.chxSocialEmailEnabled.Text = "Enable E-mail in share menu";
            this.chxSocialEmailEnabled.UseVisualStyleBackColor = true;
            // 
            // chxSocialEmailDefaultaddressBlank
            // 
            this.chxSocialEmailDefaultaddressBlank.AutoSize = true;
            this.chxSocialEmailDefaultaddressBlank.Checked = true;
            this.chxSocialEmailDefaultaddressBlank.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxSocialEmailDefaultaddressBlank.Location = new System.Drawing.Point(14, 83);
            this.chxSocialEmailDefaultaddressBlank.Name = "chxSocialEmailDefaultaddressBlank";
            this.chxSocialEmailDefaultaddressBlank.Size = new System.Drawing.Size(52, 17);
            this.chxSocialEmailDefaultaddressBlank.TabIndex = 24;
            this.chxSocialEmailDefaultaddressBlank.Text = "blank";
            this.chxSocialEmailDefaultaddressBlank.UseVisualStyleBackColor = true;
            this.chxSocialEmailDefaultaddressBlank.CheckedChanged += new System.EventHandler(this.chxSocialEmailDefaultaddressBlank_CheckedChanged);
            // 
            // lblTextDefaultEmail
            // 
            this.lblTextDefaultEmail.AutoSize = true;
            this.lblTextDefaultEmail.Location = new System.Drawing.Point(11, 60);
            this.lblTextDefaultEmail.Name = "lblTextDefaultEmail";
            this.lblTextDefaultEmail.Size = new System.Drawing.Size(162, 13);
            this.lblTextDefaultEmail.TabIndex = 22;
            this.lblTextDefaultEmail.Text = "default email address to send to: ";
            // 
            // tbDefaultEmail
            // 
            this.tbDefaultEmail.AccessibleDescription = "Editbox default email address";
            this.tbDefaultEmail.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.tbDefaultEmail.Enabled = false;
            this.tbDefaultEmail.Location = new System.Drawing.Point(72, 81);
            this.tbDefaultEmail.Name = "tbDefaultEmail";
            this.tbDefaultEmail.Size = new System.Drawing.Size(200, 20);
            this.tbDefaultEmail.TabIndex = 23;
            // 
            // tabNetwerk
            // 
            this.tabNetwerk.Controls.Add(this.lblTextDayAtStartup);
            this.tabNetwerk.Controls.Add(this.numUpdateCheckDays);
            this.tabNetwerk.Controls.Add(this.lblTextCheckforupdatesevery);
            this.tabNetwerk.Controls.Add(this.chxCheckUpdates);
            this.tabNetwerk.Controls.Add(this.lblTextNetworkMiliseconds);
            this.tabNetwerk.Controls.Add(this.chxProxyEnabled);
            this.tabNetwerk.Controls.Add(this.numTimeout);
            this.tabNetwerk.Controls.Add(this.lblTextNetworkTimeout);
            this.tabNetwerk.Controls.Add(this.iptbProxyAddress);
            this.tabNetwerk.Location = new System.Drawing.Point(4, 22);
            this.tabNetwerk.Name = "tabNetwerk";
            this.tabNetwerk.Padding = new System.Windows.Forms.Padding(3);
            this.tabNetwerk.Size = new System.Drawing.Size(386, 281);
            this.tabNetwerk.TabIndex = 4;
            this.tabNetwerk.Text = "Network";
            this.tabNetwerk.UseVisualStyleBackColor = true;
            // 
            // lblTextDayAtStartup
            // 
            this.lblTextDayAtStartup.AutoSize = true;
            this.lblTextDayAtStartup.Location = new System.Drawing.Point(221, 51);
            this.lblTextDayAtStartup.Name = "lblTextDayAtStartup";
            this.lblTextDayAtStartup.Size = new System.Drawing.Size(79, 13);
            this.lblTextDayAtStartup.TabIndex = 29;
            this.lblTextDayAtStartup.Text = "days, at startup";
            // 
            // numUpdateCheckDays
            // 
            this.numUpdateCheckDays.Enabled = false;
            this.numUpdateCheckDays.Location = new System.Drawing.Point(157, 49);
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
            this.numUpdateCheckDays.Size = new System.Drawing.Size(58, 20);
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
            this.lblTextCheckforupdatesevery.Location = new System.Drawing.Point(25, 56);
            this.lblTextCheckforupdatesevery.Name = "lblTextCheckforupdatesevery";
            this.lblTextCheckforupdatesevery.Size = new System.Drawing.Size(126, 13);
            this.lblTextCheckforupdatesevery.TabIndex = 27;
            this.lblTextCheckforupdatesevery.Text = "Check for updates every ";
            // 
            // chxCheckUpdates
            // 
            this.chxCheckUpdates.AutoSize = true;
            this.chxCheckUpdates.Location = new System.Drawing.Point(25, 26);
            this.chxCheckUpdates.Name = "chxCheckUpdates";
            this.chxCheckUpdates.Size = new System.Drawing.Size(113, 17);
            this.chxCheckUpdates.TabIndex = 26;
            this.chxCheckUpdates.Text = "Check for updates";
            this.chxCheckUpdates.UseVisualStyleBackColor = true;
            this.chxCheckUpdates.CheckedChanged += new System.EventHandler(this.cbxCheckUpdates_CheckedChanged);
            // 
            // lblTextNetworkMiliseconds
            // 
            this.lblTextNetworkMiliseconds.AutoSize = true;
            this.lblTextNetworkMiliseconds.Location = new System.Drawing.Point(158, 194);
            this.lblTextNetworkMiliseconds.Name = "lblTextNetworkMiliseconds";
            this.lblTextNetworkMiliseconds.Size = new System.Drawing.Size(20, 13);
            this.lblTextNetworkMiliseconds.TabIndex = 25;
            this.lblTextNetworkMiliseconds.Text = "ms";
            // 
            // chxProxyEnabled
            // 
            this.chxProxyEnabled.AutoSize = true;
            this.chxProxyEnabled.Location = new System.Drawing.Point(25, 91);
            this.chxProxyEnabled.Name = "chxProxyEnabled";
            this.chxProxyEnabled.Size = new System.Drawing.Size(111, 17);
            this.chxProxyEnabled.TabIndex = 1;
            this.chxProxyEnabled.Text = "Use socked proxy";
            this.chxProxyEnabled.UseVisualStyleBackColor = true;
            this.chxProxyEnabled.CheckedChanged += new System.EventHandler(this.chxUseProxy_CheckedChanged);
            // 
            // numTimeout
            // 
            this.numTimeout.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numTimeout.Location = new System.Drawing.Point(94, 192);
            this.numTimeout.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.numTimeout.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numTimeout.Name = "numTimeout";
            this.numTimeout.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numTimeout.Size = new System.Drawing.Size(58, 20);
            this.numTimeout.TabIndex = 23;
            this.numTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numTimeout.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // lblTextNetworkTimeout
            // 
            this.lblTextNetworkTimeout.AutoSize = true;
            this.lblTextNetworkTimeout.Location = new System.Drawing.Point(22, 194);
            this.lblTextNetworkTimeout.Name = "lblTextNetworkTimeout";
            this.lblTextNetworkTimeout.Size = new System.Drawing.Size(66, 13);
            this.lblTextNetworkTimeout.TabIndex = 24;
            this.lblTextNetworkTimeout.Text = "timeout time:";
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
            this.tabAdvance.Location = new System.Drawing.Point(4, 22);
            this.tabAdvance.Name = "tabAdvance";
            this.tabAdvance.Size = new System.Drawing.Size(386, 281);
            this.tabAdvance.TabIndex = 2;
            this.tabAdvance.Text = "Advance";
            this.tabAdvance.UseVisualStyleBackColor = true;
            // 
            // chxLogExceptions
            // 
            this.chxLogExceptions.AutoSize = true;
            this.chxLogExceptions.Checked = true;
            this.chxLogExceptions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxLogExceptions.Location = new System.Drawing.Point(14, 158);
            this.chxLogExceptions.Name = "chxLogExceptions";
            this.chxLogExceptions.Size = new System.Drawing.Size(244, 17);
            this.chxLogExceptions.TabIndex = 24;
            this.chxLogExceptions.Text = "Log exceptions (recommeded: leave enabled).";
            this.chxLogExceptions.UseVisualStyleBackColor = true;
            // 
            // lblTextLogging
            // 
            this.lblTextLogging.AutoSize = true;
            this.lblTextLogging.Location = new System.Drawing.Point(11, 96);
            this.lblTextLogging.Name = "lblTextLogging";
            this.lblTextLogging.Size = new System.Drawing.Size(48, 13);
            this.lblTextLogging.TabIndex = 23;
            this.lblTextLogging.Text = "Logging:";
            // 
            // chxLogDebug
            // 
            this.chxLogDebug.AutoSize = true;
            this.chxLogDebug.Location = new System.Drawing.Point(14, 112);
            this.chxLogDebug.Name = "chxLogDebug";
            this.chxLogDebug.Size = new System.Drawing.Size(100, 17);
            this.chxLogDebug.TabIndex = 22;
            this.chxLogDebug.Text = "Log debug info.";
            this.chxLogDebug.UseVisualStyleBackColor = true;
            // 
            // btnResetSettings
            // 
            this.btnResetSettings.BackColor = System.Drawing.Color.LightGray;
            this.btnResetSettings.Location = new System.Drawing.Point(119, 215);
            this.btnResetSettings.Name = "btnResetSettings";
            this.btnResetSettings.Size = new System.Drawing.Size(156, 26);
            this.btnResetSettings.TabIndex = 21;
            this.btnResetSettings.Text = "reset all settings to default";
            this.btnResetSettings.UseVisualStyleBackColor = false;
            this.btnResetSettings.Click += new System.EventHandler(this.btnResetSettings_Click);
            // 
            // chxLogErrors
            // 
            this.chxLogErrors.AutoSize = true;
            this.chxLogErrors.Checked = true;
            this.chxLogErrors.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxLogErrors.Location = new System.Drawing.Point(14, 135);
            this.chxLogErrors.Name = "chxLogErrors";
            this.chxLogErrors.Size = new System.Drawing.Size(76, 17);
            this.chxLogErrors.TabIndex = 19;
            this.chxLogErrors.Text = "Log errors.";
            this.chxLogErrors.UseVisualStyleBackColor = true;
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.LightGray;
            this.btnBrowse.Location = new System.Drawing.Point(314, 48);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(53, 23);
            this.btnBrowse.TabIndex = 15;
            this.btnBrowse.Text = "browse";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblTextNoteLocation
            // 
            this.lblTextNoteLocation.AutoSize = true;
            this.lblTextNoteLocation.Location = new System.Drawing.Point(11, 28);
            this.lblTextNoteLocation.Name = "lblTextNoteLocation";
            this.lblTextNoteLocation.Size = new System.Drawing.Size(75, 13);
            this.lblTextNoteLocation.TabIndex = 16;
            this.lblTextNoteLocation.Text = "Save notes in:";
            // 
            // tbNotesSavePath
            // 
            this.tbNotesSavePath.Location = new System.Drawing.Point(14, 48);
            this.tbNotesSavePath.Name = "tbNotesSavePath";
            this.tbNotesSavePath.Size = new System.Drawing.Size(294, 20);
            this.tbNotesSavePath.TabIndex = 14;
            this.tbNotesSavePath.Text = "?";
            // 
            // numTrayiconFontsize
            // 
            this.numTrayiconFontsize.DecimalPlaces = 2;
            this.numTrayiconFontsize.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numTrayiconFontsize.Location = new System.Drawing.Point(106, 29);
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
            this.numTrayiconFontsize.Size = new System.Drawing.Size(54, 20);
            this.numTrayiconFontsize.TabIndex = 4;
            this.numTrayiconFontsize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numTrayiconFontsize.Value = new decimal(new int[] {
            825,
            0,
            0,
            131072});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Fontsize  menu";
            // 
            // lblFontsizePoints
            // 
            this.lblFontsizePoints.AutoSize = true;
            this.lblFontsizePoints.Location = new System.Drawing.Point(166, 31);
            this.lblFontsizePoints.Name = "lblFontsizePoints";
            this.lblFontsizePoints.Size = new System.Drawing.Size(19, 13);
            this.lblFontsizePoints.TabIndex = 6;
            this.lblFontsizePoints.Text = "pt.";
            // 
            // iptbProxyAddress
            // 
            this.iptbProxyAddress.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.iptbProxyAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.iptbProxyAddress.Enabled = false;
            this.iptbProxyAddress.IPAddress = "0.0.0.0";
            this.iptbProxyAddress.Location = new System.Drawing.Point(25, 114);
            this.iptbProxyAddress.Name = "iptbProxyAddress";
            this.iptbProxyAddress.Size = new System.Drawing.Size(228, 20);
            this.iptbProxyAddress.TabIndex = 19;
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(394, 341);
            this.Controls.Add(this.tabControlSettings);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
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
            this.tabHighlight.ResumeLayout(false);
            this.tabHighlight.PerformLayout();
            this.tabSharing.ResumeLayout(false);
            this.tabControlSocialNetworks.ResumeLayout(false);
            this.tabEmail.ResumeLayout(false);
            this.tabEmail.PerformLayout();
            this.tabNetwerk.ResumeLayout(false);
            this.tabNetwerk.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpdateCheckDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).EndInit();
            this.tabAdvance.ResumeLayout(false);
            this.tabAdvance.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTrayiconFontsize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numTrayiconFontsize;
        private System.Windows.Forms.Label lblFontsizePoints;

    }
}