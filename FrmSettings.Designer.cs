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
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox tbTwitterUser;
        private System.Windows.Forms.Label lbUsername;
        private System.Windows.Forms.TextBox tbTwitterPass;
        private System.Windows.Forms.Label lbPassword;
        private System.Windows.Forms.CheckBox chxRememberTwPass;
        private System.Windows.Forms.TabControl tabControlSettings;
        private System.Windows.Forms.TabPage tabAppearance;
        private System.Windows.Forms.TabPage tabSocialNetworks;
        private System.Windows.Forms.TabPage tabAdvance;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblTextNoteLocation;
        private System.Windows.Forms.TextBox tbNotesSavePath;
        private System.Windows.Forms.Label lbWarningTwitterPassword;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ComboBox cbxActionLeftclick;
        private System.Windows.Forms.Label lbText;
        private System.Windows.Forms.TabPage tabGeneral;
#if windows
        private System.Windows.Forms.CheckBox chxStartOnLogin;
#endif
        private System.Windows.Forms.CheckBox chxConfirmLink;
        private System.Windows.Forms.CheckBox chxConfirmExit;
        private System.Windows.Forms.CheckBox chxLogErrors;
        private System.Windows.Forms.Button btnResetSettings;
        private System.Windows.Forms.TabPage tabNetwerk;
        private System.Windows.Forms.CheckBox chxProxyEnabled;
        private System.Windows.Forms.CheckBox chxLogDebug;
        private System.Windows.Forms.TabPage tabHighlight;
        private System.Windows.Forms.CheckBox chxHighlightHTML;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chxSaveFBSession;
        private IPTextBox iptbProxyAddress;
        private System.Windows.Forms.Label lbTextTwMs;
        private System.Windows.Forms.Label lbTextTwTimeout;
        private System.Windows.Forms.NumericUpDown numTimeout;
        private System.Windows.Forms.CheckBox chxConfirmDeletenote;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.tbTwitterUser = new System.Windows.Forms.TextBox();
            this.lbUsername = new System.Windows.Forms.Label();
            this.tbTwitterPass = new System.Windows.Forms.TextBox();
            this.lbPassword = new System.Windows.Forms.Label();
            this.chxRememberTwPass = new System.Windows.Forms.CheckBox();
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.chxConfirmDeletenote = new System.Windows.Forms.CheckBox();
            this.cbxActionLeftclick = new System.Windows.Forms.ComboBox();
            this.chxConfirmExit = new System.Windows.Forms.CheckBox();
            this.chxStartOnLogin = new System.Windows.Forms.CheckBox();
            this.lbText = new System.Windows.Forms.Label();
            this.tabAppearance = new System.Windows.Forms.TabPage();
            this.tabAppearanceColors = new System.Windows.Forms.TabControl();
            this.tabPageLooks = new System.Windows.Forms.TabPage();
            this.cbxShowTooltips = new System.Windows.Forms.CheckBox();
            this.chxTransparecy = new System.Windows.Forms.CheckBox();
            this.lbDefaultNewNoteColor = new System.Windows.Forms.Label();
            this.cbxDefaultColor = new System.Windows.Forms.ComboBox();
            this.numProcTransparency = new System.Windows.Forms.NumericUpDown();
            this.lbTextProc = new System.Windows.Forms.Label();
            this.tabPageFonts = new System.Windows.Forms.TabPage();
            this.cbxFontNoteTitleBold = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numFontSizeTitle = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxFontNoteTitle = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lbTextDirection = new System.Windows.Forms.Label();
            this.cbxTextDirection = new System.Windows.Forms.ComboBox();
            this.lblTextpt = new System.Windows.Forms.Label();
            this.numFontSizeContent = new System.Windows.Forms.NumericUpDown();
            this.lblTextFontSize = new System.Windows.Forms.Label();
            this.lbTextNoteFont = new System.Windows.Forms.Label();
            this.cbxFontNoteContent = new System.Windows.Forms.ComboBox();
            this.tabHighlight = new System.Windows.Forms.TabPage();
            this.chxHighlightSQL = new System.Windows.Forms.CheckBox();
            this.chxHighlightPHP = new System.Windows.Forms.CheckBox();
            this.chxHighlightHyperlinks = new System.Windows.Forms.CheckBox();
            this.chxHighlightHTML = new System.Windows.Forms.CheckBox();
            this.tabSocialNetworks = new System.Windows.Forms.TabPage();
            this.tabControlSocialNetworks = new System.Windows.Forms.TabControl();
            this.tabEmail = new System.Windows.Forms.TabPage();
            this.chxSocialEmailEnabled = new System.Windows.Forms.CheckBox();
            this.chxSocialEmailDefaultaddressBlank = new System.Windows.Forms.CheckBox();
            this.lbTextDefaultEmail = new System.Windows.Forms.Label();
            this.tbDefaultEmail = new System.Windows.Forms.TextBox();
            this.tabTwitter = new System.Windows.Forms.TabPage();
            this.chxSocialTwitterEnabled = new System.Windows.Forms.CheckBox();
            this.lbWarningTwitterPassword = new System.Windows.Forms.Label();
            this.tabFacebook = new System.Windows.Forms.TabPage();
            this.chxSocialFacebookEnabled = new System.Windows.Forms.CheckBox();
            this.chxSaveFBSession = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabNetwerk = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.numUpdateCheckDays = new System.Windows.Forms.NumericUpDown();
            this.lbTextCheckforupdatesevery = new System.Windows.Forms.Label();
            this.chxCheckUpdates = new System.Windows.Forms.CheckBox();
            this.lbTextTwMs = new System.Windows.Forms.Label();
            this.chxProxyEnabled = new System.Windows.Forms.CheckBox();
            this.numTimeout = new System.Windows.Forms.NumericUpDown();
            this.lbTextTwTimeout = new System.Windows.Forms.Label();
            this.chxConfirmLink = new System.Windows.Forms.CheckBox();
            this.iptbProxyAddress = new NoteFly.IPTextBox();
            this.tabAdvance = new System.Windows.Forms.TabPage();
            this.chxLogDebug = new System.Windows.Forms.CheckBox();
            this.btnResetSettings = new System.Windows.Forms.Button();
            this.chxLogErrors = new System.Windows.Forms.CheckBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblTextNoteLocation = new System.Windows.Forms.Label();
            this.tbNotesSavePath = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControlSettings.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.tabAppearance.SuspendLayout();
            this.tabAppearanceColors.SuspendLayout();
            this.tabPageLooks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numProcTransparency)).BeginInit();
            this.tabPageFonts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSizeTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSizeContent)).BeginInit();
            this.tabHighlight.SuspendLayout();
            this.tabSocialNetworks.SuspendLayout();
            this.tabControlSocialNetworks.SuspendLayout();
            this.tabEmail.SuspendLayout();
            this.tabTwitter.SuspendLayout();
            this.tabFacebook.SuspendLayout();
            this.tabNetwerk.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpdateCheckDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).BeginInit();
            this.tabAdvance.SuspendLayout();
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
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.BackColor = System.Drawing.Color.LightGray;
            this.btnCancel.Location = new System.Drawing.Point(4, 313);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(188, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tbTwitterUser
            // 
            this.tbTwitterUser.AccessibleDescription = "Editbox twitter username";
            this.tbTwitterUser.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.tbTwitterUser.Location = new System.Drawing.Point(103, 56);
            this.tbTwitterUser.MaxLength = 16;
            this.tbTwitterUser.Name = "tbTwitterUser";
            this.tbTwitterUser.Size = new System.Drawing.Size(153, 20);
            this.tbTwitterUser.TabIndex = 8;
            this.tbTwitterUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbUsername
            // 
            this.lbUsername.AutoSize = true;
            this.lbUsername.Location = new System.Drawing.Point(11, 59);
            this.lbUsername.Name = "lbUsername";
            this.lbUsername.Size = new System.Drawing.Size(87, 13);
            this.lbUsername.TabIndex = 9;
            this.lbUsername.Text = "twitter username:";
            // 
            // tbTwitterPass
            // 
            this.tbTwitterPass.AccessibleDescription = "Editbox twitter password";
            this.tbTwitterPass.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.tbTwitterPass.Enabled = false;
            this.tbTwitterPass.Location = new System.Drawing.Point(103, 103);
            this.tbTwitterPass.MaxLength = 255;
            this.tbTwitterPass.Name = "tbTwitterPass";
            this.tbTwitterPass.PasswordChar = 'X';
            this.tbTwitterPass.Size = new System.Drawing.Size(153, 20);
            this.tbTwitterPass.TabIndex = 10;
            // 
            // lbPassword
            // 
            this.lbPassword.AutoSize = true;
            this.lbPassword.Location = new System.Drawing.Point(11, 106);
            this.lbPassword.Name = "lbPassword";
            this.lbPassword.Size = new System.Drawing.Size(86, 13);
            this.lbPassword.TabIndex = 11;
            this.lbPassword.Text = "twitter password:";
            // 
            // chxRememberTwPass
            // 
            this.chxRememberTwPass.AutoSize = true;
            this.chxRememberTwPass.Location = new System.Drawing.Point(12, 82);
            this.chxRememberTwPass.Name = "chxRememberTwPass";
            this.chxRememberTwPass.Size = new System.Drawing.Size(273, 17);
            this.chxRememberTwPass.TabIndex = 12;
            this.chxRememberTwPass.Text = "Remember twitter password (warning: not encrypted)";
            this.chxRememberTwPass.UseVisualStyleBackColor = true;
            this.chxRememberTwPass.CheckedChanged += new System.EventHandler(this.cbxRememberTwPass_CheckedChanged);
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.Controls.Add(this.tabGeneral);
            this.tabControlSettings.Controls.Add(this.tabAppearance);
            this.tabControlSettings.Controls.Add(this.tabHighlight);
            this.tabControlSettings.Controls.Add(this.tabSocialNetworks);
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
            this.tabGeneral.Controls.Add(this.chxConfirmDeletenote);
            this.tabGeneral.Controls.Add(this.cbxActionLeftclick);
            this.tabGeneral.Controls.Add(this.chxConfirmExit);
            this.tabGeneral.Controls.Add(this.chxStartOnLogin);
            this.tabGeneral.Controls.Add(this.lbText);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(386, 281);
            this.tabGeneral.TabIndex = 3;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
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
            this.cbxActionLeftclick.Location = new System.Drawing.Point(145, 109);
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
            // lbText
            // 
            this.lbText.AutoSize = true;
            this.lbText.Location = new System.Drawing.Point(17, 112);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(122, 13);
            this.lbText.TabIndex = 15;
            this.lbText.Text = "Action left click trayicon:";
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
            this.tabAppearanceColors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabAppearanceColors.Location = new System.Drawing.Point(3, 3);
            this.tabAppearanceColors.Name = "tabAppearanceColors";
            this.tabAppearanceColors.SelectedIndex = 0;
            this.tabAppearanceColors.Size = new System.Drawing.Size(380, 275);
            this.tabAppearanceColors.TabIndex = 28;
            // 
            // tabPageLooks
            // 
            this.tabPageLooks.Controls.Add(this.cbxShowTooltips);
            this.tabPageLooks.Controls.Add(this.chxTransparecy);
            this.tabPageLooks.Controls.Add(this.lbDefaultNewNoteColor);
            this.tabPageLooks.Controls.Add(this.cbxDefaultColor);
            this.tabPageLooks.Controls.Add(this.numProcTransparency);
            this.tabPageLooks.Controls.Add(this.lbTextProc);
            this.tabPageLooks.Location = new System.Drawing.Point(4, 22);
            this.tabPageLooks.Name = "tabPageLooks";
            this.tabPageLooks.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageLooks.Size = new System.Drawing.Size(372, 249);
            this.tabPageLooks.TabIndex = 0;
            this.tabPageLooks.Text = "Looks";
            this.tabPageLooks.UseVisualStyleBackColor = true;
            // 
            // cbxShowTooltips
            // 
            this.cbxShowTooltips.AutoSize = true;
            this.cbxShowTooltips.Location = new System.Drawing.Point(18, 133);
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
            // 
            // lbDefaultNewNoteColor
            // 
            this.lbDefaultNewNoteColor.AutoSize = true;
            this.lbDefaultNewNoteColor.Location = new System.Drawing.Point(15, 80);
            this.lbDefaultNewNoteColor.Name = "lbDefaultNewNoteColor";
            this.lbDefaultNewNoteColor.Size = new System.Drawing.Size(118, 13);
            this.lbDefaultNewNoteColor.TabIndex = 9;
            this.lbDefaultNewNoteColor.Text = "Default skin new notes:";
            // 
            // cbxDefaultColor
            // 
            this.cbxDefaultColor.AccessibleDescription = "Defaul color for new note.";
            this.cbxDefaultColor.AccessibleRole = System.Windows.Forms.AccessibleRole.ComboBox;
            this.cbxDefaultColor.BackColor = System.Drawing.Color.LightGray;
            this.cbxDefaultColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDefaultColor.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbxDefaultColor.FormattingEnabled = true;
            this.cbxDefaultColor.Location = new System.Drawing.Point(154, 77);
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
            this.numProcTransparency.Location = new System.Drawing.Point(182, 26);
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
            // lbTextProc
            // 
            this.lbTextProc.AutoSize = true;
            this.lbTextProc.Location = new System.Drawing.Point(234, 30);
            this.lbTextProc.Name = "lbTextProc";
            this.lbTextProc.Size = new System.Drawing.Size(47, 13);
            this.lbTextProc.TabIndex = 12;
            this.lbTextProc.Text = "% visible";
            // 
            // tabPageFonts
            // 
            this.tabPageFonts.Controls.Add(this.cbxFontNoteTitleBold);
            this.tabPageFonts.Controls.Add(this.label5);
            this.tabPageFonts.Controls.Add(this.numFontSizeTitle);
            this.tabPageFonts.Controls.Add(this.label4);
            this.tabPageFonts.Controls.Add(this.cbxFontNoteTitle);
            this.tabPageFonts.Controls.Add(this.label3);
            this.tabPageFonts.Controls.Add(this.lbTextDirection);
            this.tabPageFonts.Controls.Add(this.cbxTextDirection);
            this.tabPageFonts.Controls.Add(this.lblTextpt);
            this.tabPageFonts.Controls.Add(this.numFontSizeContent);
            this.tabPageFonts.Controls.Add(this.lblTextFontSize);
            this.tabPageFonts.Controls.Add(this.lbTextNoteFont);
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(182, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 13);
            this.label5.TabIndex = 39;
            this.label5.Text = "pt";
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(61, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "Font size title:";
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(82, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 35;
            this.label3.Text = "Font title:";
            // 
            // lbTextDirection
            // 
            this.lbTextDirection.AccessibleDescription = "";
            this.lbTextDirection.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.lbTextDirection.AutoSize = true;
            this.lbTextDirection.Location = new System.Drawing.Point(50, 183);
            this.lbTextDirection.Name = "lbTextDirection";
            this.lbTextDirection.Size = new System.Drawing.Size(71, 13);
            this.lbTextDirection.TabIndex = 34;
            this.lbTextDirection.Text = "Text direction";
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
            // lblTextpt
            // 
            this.lblTextpt.AutoSize = true;
            this.lblTextpt.Location = new System.Drawing.Point(182, 130);
            this.lblTextpt.Name = "lblTextpt";
            this.lblTextpt.Size = new System.Drawing.Size(16, 13);
            this.lblTextpt.TabIndex = 32;
            this.lblTextpt.Text = "pt";
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
            // lblTextFontSize
            // 
            this.lblTextFontSize.AutoSize = true;
            this.lblTextFontSize.Location = new System.Drawing.Point(9, 125);
            this.lblTextFontSize.Name = "lblTextFontSize";
            this.lblTextFontSize.Size = new System.Drawing.Size(123, 13);
            this.lblTextFontSize.TabIndex = 30;
            this.lblTextFontSize.Text = "default font size content:";
            // 
            // lbTextNoteFont
            // 
            this.lbTextNoteFont.AutoSize = true;
            this.lbTextNoteFont.Location = new System.Drawing.Point(30, 101);
            this.lbTextNoteFont.Name = "lbTextNoteFont";
            this.lbTextNoteFont.Size = new System.Drawing.Size(102, 13);
            this.lbTextNoteFont.TabIndex = 29;
            this.lbTextNoteFont.Text = "default font content:";
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
            // tabHighlight
            // 
            this.tabHighlight.Controls.Add(this.chxHighlightSQL);
            this.tabHighlight.Controls.Add(this.chxHighlightPHP);
            this.tabHighlight.Controls.Add(this.chxHighlightHyperlinks);
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
            this.chxHighlightSQL.Location = new System.Drawing.Point(16, 137);
            this.chxHighlightSQL.Name = "chxHighlightSQL";
            this.chxHighlightSQL.Size = new System.Drawing.Size(148, 17);
            this.chxHighlightSQL.TabIndex = 16;
            this.chxHighlightSQL.Text = "Highlight SQL codesyntax";
            this.chxHighlightSQL.UseVisualStyleBackColor = true;
            // 
            // chxHighlightPHP
            // 
            this.chxHighlightPHP.AutoSize = true;
            this.chxHighlightPHP.Location = new System.Drawing.Point(16, 102);
            this.chxHighlightPHP.Name = "chxHighlightPHP";
            this.chxHighlightPHP.Size = new System.Drawing.Size(152, 17);
            this.chxHighlightPHP.TabIndex = 15;
            this.chxHighlightPHP.Text = "Highlight PHP code syntax";
            this.chxHighlightPHP.UseVisualStyleBackColor = true;
            // 
            // chxHighlightHyperlinks
            // 
            this.chxHighlightHyperlinks.AutoSize = true;
            this.chxHighlightHyperlinks.Location = new System.Drawing.Point(16, 34);
            this.chxHighlightHyperlinks.Name = "chxHighlightHyperlinks";
            this.chxHighlightHyperlinks.Size = new System.Drawing.Size(108, 17);
            this.chxHighlightHyperlinks.TabIndex = 14;
            this.chxHighlightHyperlinks.Text = "Detect hyperlinks";
            this.chxHighlightHyperlinks.UseVisualStyleBackColor = true;
            // 
            // chxHighlightHTML
            // 
            this.chxHighlightHTML.AutoSize = true;
            this.chxHighlightHTML.Location = new System.Drawing.Point(16, 66);
            this.chxHighlightHTML.Name = "chxHighlightHTML";
            this.chxHighlightHTML.Size = new System.Drawing.Size(162, 17);
            this.chxHighlightHTML.TabIndex = 13;
            this.chxHighlightHTML.Text = "Highlight HTML syntax notes";
            this.chxHighlightHTML.UseVisualStyleBackColor = true;
            // 
            // tabSocialNetworks
            // 
            this.tabSocialNetworks.Controls.Add(this.tabControlSocialNetworks);
            this.tabSocialNetworks.Location = new System.Drawing.Point(4, 22);
            this.tabSocialNetworks.Name = "tabSocialNetworks";
            this.tabSocialNetworks.Padding = new System.Windows.Forms.Padding(3);
            this.tabSocialNetworks.Size = new System.Drawing.Size(386, 281);
            this.tabSocialNetworks.TabIndex = 1;
            this.tabSocialNetworks.Text = "Social networks";
            this.tabSocialNetworks.UseVisualStyleBackColor = true;
            // 
            // tabControlSocialNetworks
            // 
            this.tabControlSocialNetworks.Controls.Add(this.tabEmail);
            this.tabControlSocialNetworks.Controls.Add(this.tabTwitter);
            this.tabControlSocialNetworks.Controls.Add(this.tabFacebook);
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
            this.tabEmail.Controls.Add(this.lbTextDefaultEmail);
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
            this.chxSocialEmailEnabled.Size = new System.Drawing.Size(162, 17);
            this.chxSocialEmailEnabled.TabIndex = 25;
            this.chxSocialEmailEnabled.Text = "Show E-mail in send to menu";
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
            // 
            // lbTextDefaultEmail
            // 
            this.lbTextDefaultEmail.AutoSize = true;
            this.lbTextDefaultEmail.Location = new System.Drawing.Point(11, 60);
            this.lbTextDefaultEmail.Name = "lbTextDefaultEmail";
            this.lbTextDefaultEmail.Size = new System.Drawing.Size(162, 13);
            this.lbTextDefaultEmail.TabIndex = 22;
            this.lbTextDefaultEmail.Text = "default email address to send to: ";
            // 
            // tbDefaultEmail
            // 
            this.tbDefaultEmail.AccessibleDescription = "Editbox default email address";
            this.tbDefaultEmail.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.tbDefaultEmail.Enabled = false;
            this.tbDefaultEmail.Location = new System.Drawing.Point(69, 80);
            this.tbDefaultEmail.Name = "tbDefaultEmail";
            this.tbDefaultEmail.Size = new System.Drawing.Size(200, 20);
            this.tbDefaultEmail.TabIndex = 23;
            // 
            // tabTwitter
            // 
            this.tabTwitter.Controls.Add(this.chxSocialTwitterEnabled);
            this.tabTwitter.Controls.Add(this.lbUsername);
            this.tabTwitter.Controls.Add(this.chxRememberTwPass);
            this.tabTwitter.Controls.Add(this.lbWarningTwitterPassword);
            this.tabTwitter.Controls.Add(this.lbPassword);
            this.tabTwitter.Controls.Add(this.tbTwitterPass);
            this.tabTwitter.Controls.Add(this.tbTwitterUser);
            this.tabTwitter.Location = new System.Drawing.Point(4, 22);
            this.tabTwitter.Name = "tabTwitter";
            this.tabTwitter.Padding = new System.Windows.Forms.Padding(3);
            this.tabTwitter.Size = new System.Drawing.Size(372, 249);
            this.tabTwitter.TabIndex = 0;
            this.tabTwitter.Text = "Twitter";
            this.tabTwitter.UseVisualStyleBackColor = true;
            // 
            // chxSocialTwitterEnabled
            // 
            this.chxSocialTwitterEnabled.AutoSize = true;
            this.chxSocialTwitterEnabled.Location = new System.Drawing.Point(12, 24);
            this.chxSocialTwitterEnabled.Name = "chxSocialTwitterEnabled";
            this.chxSocialTwitterEnabled.Size = new System.Drawing.Size(126, 17);
            this.chxSocialTwitterEnabled.TabIndex = 14;
            this.chxSocialTwitterEnabled.Text = "Show send to Twitter";
            this.chxSocialTwitterEnabled.UseVisualStyleBackColor = true;
            // 
            // lbWarningTwitterPassword
            // 
            this.lbWarningTwitterPassword.Location = new System.Drawing.Point(9, 134);
            this.lbWarningTwitterPassword.Name = "lbWarningTwitterPassword";
            this.lbWarningTwitterPassword.Size = new System.Drawing.Size(278, 36);
            this.lbWarningTwitterPassword.TabIndex = 13;
            this.lbWarningTwitterPassword.Text = "Never ever enter your twitter password here on a public computer.";
            // 
            // tabFacebook
            // 
            this.tabFacebook.Controls.Add(this.chxSocialFacebookEnabled);
            this.tabFacebook.Controls.Add(this.chxSaveFBSession);
            this.tabFacebook.Controls.Add(this.label1);
            this.tabFacebook.Location = new System.Drawing.Point(4, 22);
            this.tabFacebook.Name = "tabFacebook";
            this.tabFacebook.Padding = new System.Windows.Forms.Padding(3);
            this.tabFacebook.Size = new System.Drawing.Size(372, 249);
            this.tabFacebook.TabIndex = 1;
            this.tabFacebook.Text = "Facebook";
            this.tabFacebook.UseVisualStyleBackColor = true;
            // 
            // chxSocialFacebookEnabled
            // 
            this.chxSocialFacebookEnabled.AutoSize = true;
            this.chxSocialFacebookEnabled.Location = new System.Drawing.Point(11, 25);
            this.chxSocialFacebookEnabled.Name = "chxSocialFacebookEnabled";
            this.chxSocialFacebookEnabled.Size = new System.Drawing.Size(142, 17);
            this.chxSocialFacebookEnabled.TabIndex = 2;
            this.chxSocialFacebookEnabled.Text = "Show send to Facebook";
            this.chxSocialFacebookEnabled.UseVisualStyleBackColor = true;
            // 
            // chxSaveFBSession
            // 
            this.chxSaveFBSession.AutoSize = true;
            this.chxSaveFBSession.Checked = true;
            this.chxSaveFBSession.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxSaveFBSession.Location = new System.Drawing.Point(11, 59);
            this.chxSaveFBSession.Name = "chxSaveFBSession";
            this.chxSaveFBSession.Size = new System.Drawing.Size(232, 17);
            this.chxSaveFBSession.TabIndex = 0;
            this.chxSaveFBSession.Text = "Save Facebook session.(gives 24h access)";
            this.chxSaveFBSession.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Do not enable this on a public computer.";
            // 
            // tabNetwerk
            // 
            this.tabNetwerk.Controls.Add(this.label2);
            this.tabNetwerk.Controls.Add(this.numUpdateCheckDays);
            this.tabNetwerk.Controls.Add(this.lbTextCheckforupdatesevery);
            this.tabNetwerk.Controls.Add(this.chxCheckUpdates);
            this.tabNetwerk.Controls.Add(this.lbTextTwMs);
            this.tabNetwerk.Controls.Add(this.chxProxyEnabled);
            this.tabNetwerk.Controls.Add(this.numTimeout);
            this.tabNetwerk.Controls.Add(this.lbTextTwTimeout);
            this.tabNetwerk.Controls.Add(this.chxConfirmLink);
            this.tabNetwerk.Controls.Add(this.iptbProxyAddress);
            this.tabNetwerk.Location = new System.Drawing.Point(4, 22);
            this.tabNetwerk.Name = "tabNetwerk";
            this.tabNetwerk.Padding = new System.Windows.Forms.Padding(3);
            this.tabNetwerk.Size = new System.Drawing.Size(386, 281);
            this.tabNetwerk.TabIndex = 4;
            this.tabNetwerk.Text = "Network";
            this.tabNetwerk.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(221, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 29;
            this.label2.Text = "days, at startup";
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
            7,
            0,
            0,
            0});
            // 
            // lbTextCheckforupdatesevery
            // 
            this.lbTextCheckforupdatesevery.AutoSize = true;
            this.lbTextCheckforupdatesevery.Location = new System.Drawing.Point(25, 51);
            this.lbTextCheckforupdatesevery.Name = "lbTextCheckforupdatesevery";
            this.lbTextCheckforupdatesevery.Size = new System.Drawing.Size(126, 13);
            this.lbTextCheckforupdatesevery.TabIndex = 27;
            this.lbTextCheckforupdatesevery.Text = "Check for updates every ";
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
            // lbTextTwMs
            // 
            this.lbTextTwMs.AutoSize = true;
            this.lbTextTwMs.Location = new System.Drawing.Point(158, 194);
            this.lbTextTwMs.Name = "lbTextTwMs";
            this.lbTextTwMs.Size = new System.Drawing.Size(20, 13);
            this.lbTextTwMs.TabIndex = 25;
            this.lbTextTwMs.Text = "ms";
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
            // lbTextTwTimeout
            // 
            this.lbTextTwTimeout.AutoSize = true;
            this.lbTextTwTimeout.Location = new System.Drawing.Point(22, 194);
            this.lbTextTwTimeout.Name = "lbTextTwTimeout";
            this.lbTextTwTimeout.Size = new System.Drawing.Size(66, 13);
            this.lbTextTwTimeout.TabIndex = 24;
            this.lbTextTwTimeout.Text = "timeout time:";
            // 
            // chxConfirmLink
            // 
            this.chxConfirmLink.AutoSize = true;
            this.chxConfirmLink.Checked = true;
            this.chxConfirmLink.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxConfirmLink.Location = new System.Drawing.Point(25, 160);
            this.chxConfirmLink.Name = "chxConfirmLink";
            this.chxConfirmLink.Size = new System.Drawing.Size(194, 17);
            this.chxConfirmLink.TabIndex = 18;
            this.chxConfirmLink.Text = "Ask before launching URL, on click";
            this.chxConfirmLink.UseVisualStyleBackColor = true;
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
            // tabAdvance
            // 
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
            // chxLogDebug
            // 
            this.chxLogDebug.AutoSize = true;
            this.chxLogDebug.Location = new System.Drawing.Point(14, 135);
            this.chxLogDebug.Name = "chxLogDebug";
            this.chxLogDebug.Size = new System.Drawing.Size(93, 17);
            this.chxLogDebug.TabIndex = 22;
            this.chxLogDebug.Text = "log debug info";
            this.chxLogDebug.UseVisualStyleBackColor = true;
            // 
            // btnResetSettings
            // 
            this.btnResetSettings.BackColor = System.Drawing.Color.LightGray;
            this.btnResetSettings.Location = new System.Drawing.Point(14, 172);
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
            this.chxLogErrors.Location = new System.Drawing.Point(14, 112);
            this.chxLogErrors.Name = "chxLogErrors";
            this.chxLogErrors.Size = new System.Drawing.Size(123, 17);
            this.chxLogErrors.TabIndex = 19;
            this.chxLogErrors.Text = "log application errors";
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
            this.lblTextNoteLocation.Location = new System.Drawing.Point(11, 32);
            this.lblTextNoteLocation.Name = "lblTextNoteLocation";
            this.lblTextNoteLocation.Size = new System.Drawing.Size(73, 13);
            this.lblTextNoteLocation.TabIndex = 16;
            this.lblTextNoteLocation.Text = "save notes in:";
            // 
            // tbNotesSavePath
            // 
            this.tbNotesSavePath.Location = new System.Drawing.Point(14, 48);
            this.tbNotesSavePath.Name = "tbNotesSavePath";
            this.tbNotesSavePath.Size = new System.Drawing.Size(294, 20);
            this.tbNotesSavePath.TabIndex = 14;
            this.tbNotesSavePath.Text = "?";
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
            this.tabHighlight.ResumeLayout(false);
            this.tabHighlight.PerformLayout();
            this.tabSocialNetworks.ResumeLayout(false);
            this.tabControlSocialNetworks.ResumeLayout(false);
            this.tabEmail.ResumeLayout(false);
            this.tabEmail.PerformLayout();
            this.tabTwitter.ResumeLayout(false);
            this.tabTwitter.PerformLayout();
            this.tabFacebook.ResumeLayout(false);
            this.tabFacebook.PerformLayout();
            this.tabNetwerk.ResumeLayout(false);
            this.tabNetwerk.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpdateCheckDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTimeout)).EndInit();
            this.tabAdvance.ResumeLayout(false);
            this.tabAdvance.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chxHighlightHyperlinks;
        private System.Windows.Forms.CheckBox chxHighlightSQL;
        private System.Windows.Forms.CheckBox chxHighlightPHP;
        private System.Windows.Forms.TabControl tabControlSocialNetworks;
        private System.Windows.Forms.TabPage tabTwitter;
        private System.Windows.Forms.TabPage tabFacebook;
        private System.Windows.Forms.TabPage tabEmail;
        private System.Windows.Forms.CheckBox chxSocialEmailDefaultaddressBlank;
        private System.Windows.Forms.Label lbTextDefaultEmail;
        private System.Windows.Forms.TextBox tbDefaultEmail;
        private System.Windows.Forms.CheckBox chxSocialEmailEnabled;
        private System.Windows.Forms.CheckBox chxSocialTwitterEnabled;
        private System.Windows.Forms.CheckBox chxSocialFacebookEnabled;
        private System.Windows.Forms.CheckBox chxCheckUpdates;
        private System.Windows.Forms.Label lbTextCheckforupdatesevery;
        private System.Windows.Forms.NumericUpDown numUpdateCheckDays;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabAppearanceColors;
        private System.Windows.Forms.TabPage tabPageLooks;
        private System.Windows.Forms.CheckBox chxTransparecy;
        private System.Windows.Forms.Label lbDefaultNewNoteColor;
        private System.Windows.Forms.ComboBox cbxDefaultColor;
        private System.Windows.Forms.NumericUpDown numProcTransparency;
        private System.Windows.Forms.Label lbTextProc;
        private System.Windows.Forms.TabPage tabPageFonts;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numFontSizeTitle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxFontNoteTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbTextDirection;
        private System.Windows.Forms.ComboBox cbxTextDirection;
        private System.Windows.Forms.Label lblTextpt;
        private System.Windows.Forms.NumericUpDown numFontSizeContent;
        private System.Windows.Forms.Label lblTextFontSize;
        private System.Windows.Forms.Label lbTextNoteFont;
        private System.Windows.Forms.ComboBox cbxFontNoteContent;
        private System.Windows.Forms.CheckBox cbxFontNoteTitleBold;
        private System.Windows.Forms.CheckBox cbxShowTooltips;
    }
}