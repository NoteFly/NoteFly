﻿namespace SimplePlainNote
{
    partial class FrmSettings
    {
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
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.chxTransparecy = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbDefaultNewNoteColor = new System.Windows.Forms.Label();
            this.cbxDefaultColor = new System.Windows.Forms.ComboBox();
            this.numProcTransparency = new System.Windows.Forms.NumericUpDown();
            this.lbTextProc = new System.Windows.Forms.Label();
            this.tbTwitterUser = new System.Windows.Forms.TextBox();
            this.lbUsername = new System.Windows.Forms.Label();
            this.tbTwitterPass = new System.Windows.Forms.TextBox();
            this.lbPassword = new System.Windows.Forms.Label();
            this.chxRememberTwPass = new System.Windows.Forms.CheckBox();
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.cbxActionLeftClick = new System.Windows.Forms.ComboBox();
            this.chxConfirmExit = new System.Windows.Forms.CheckBox();
            this.chxStartOnBootWindows = new System.Windows.Forms.CheckBox();
            this.lbText = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.tbNotesSavePath = new System.Windows.Forms.TextBox();
            this.lblTextNoteLocation = new System.Windows.Forms.Label();
            this.tabAppearance = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.chxSyntaxHighlightC = new System.Windows.Forms.CheckBox();
            this.lblTextpt = new System.Windows.Forms.Label();
            this.numFontSize = new System.Windows.Forms.NumericUpDown();
            this.chxSyntaxHighlightHTML = new System.Windows.Forms.CheckBox();
            this.lblTextFontSize = new System.Windows.Forms.Label();
            this.lbTextNoteFont = new System.Windows.Forms.Label();
            this.cbxFontNoteContent = new System.Windows.Forms.ComboBox();
            this.tabTwitter = new System.Windows.Forms.TabPage();
            this.lbWarningTwitterPassword = new System.Windows.Forms.Label();
            this.tabAdvance = new System.Windows.Forms.TabPage();
            this.btnCrash = new System.Windows.Forms.Button();
            this.tbDefaultEmail = new System.Windows.Forms.TextBox();
            this.lbTextDefaultEmail = new System.Windows.Forms.Label();
            this.chxConfirmLink = new System.Windows.Forms.CheckBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.chxLogErrors = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numProcTransparency)).BeginInit();
            this.tabControlSettings.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.tabAppearance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).BeginInit();
            this.tabTwitter.SuspendLayout();
            this.tabAdvance.SuspendLayout();
            this.SuspendLayout();
            // 
            // chxTransparecy
            // 
            this.chxTransparecy.AutoSize = true;
            this.chxTransparecy.BackColor = System.Drawing.Color.White;
            this.chxTransparecy.Checked = true;
            this.chxTransparecy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxTransparecy.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chxTransparecy.Location = new System.Drawing.Point(18, 25);
            this.chxTransparecy.Name = "chxTransparecy";
            this.chxTransparecy.Size = new System.Drawing.Size(129, 18);
            this.chxTransparecy.TabIndex = 1;
            this.chxTransparecy.Text = "Enable transparency";
            this.chxTransparecy.UseVisualStyleBackColor = false;
            this.chxTransparecy.CheckedChanged += new System.EventHandler(this.cbxTransparecy_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnOK.BackColor = System.Drawing.Color.LightGray;
            this.btnOK.Location = new System.Drawing.Point(173, 280);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(157, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnCancel.BackColor = System.Drawing.Color.LightGray;
            this.btnCancel.Location = new System.Drawing.Point(6, 280);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(161, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lbDefaultNewNoteColor
            // 
            this.lbDefaultNewNoteColor.AutoSize = true;
            this.lbDefaultNewNoteColor.Location = new System.Drawing.Point(16, 65);
            this.lbDefaultNewNoteColor.Name = "lbDefaultNewNoteColor";
            this.lbDefaultNewNoteColor.Size = new System.Drawing.Size(119, 13);
            this.lbDefaultNewNoteColor.TabIndex = 4;
            this.lbDefaultNewNoteColor.Text = "Defaul color new notes:";
            // 
            // cbxDefaultColor
            // 
            this.cbxDefaultColor.BackColor = System.Drawing.Color.LightGray;
            this.cbxDefaultColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDefaultColor.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbxDefaultColor.FormattingEnabled = true;
            this.cbxDefaultColor.Items.AddRange(new object[] {
            "Yellow",
            "Orange",
            "White",
            "Green",
            "Blue",
            "Purple",
            "Red"});
            this.cbxDefaultColor.Location = new System.Drawing.Point(153, 62);
            this.cbxDefaultColor.MaxDropDownItems = 5;
            this.cbxDefaultColor.Name = "cbxDefaultColor";
            this.cbxDefaultColor.Size = new System.Drawing.Size(139, 21);
            this.cbxDefaultColor.TabIndex = 5;
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
            this.numProcTransparency.Location = new System.Drawing.Point(153, 23);
            this.numProcTransparency.Maximum = new decimal(new int[] {
            95,
            0,
            0,
            0});
            this.numProcTransparency.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numProcTransparency.Name = "numProcTransparency";
            this.numProcTransparency.Size = new System.Drawing.Size(46, 20);
            this.numProcTransparency.TabIndex = 6;
            this.numProcTransparency.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // lbTextProc
            // 
            this.lbTextProc.AutoSize = true;
            this.lbTextProc.Location = new System.Drawing.Point(205, 27);
            this.lbTextProc.Name = "lbTextProc";
            this.lbTextProc.Size = new System.Drawing.Size(15, 13);
            this.lbTextProc.TabIndex = 7;
            this.lbTextProc.Text = "%";
            // 
            // tbTwitterUser
            // 
            this.tbTwitterUser.Location = new System.Drawing.Point(113, 43);
            this.tbTwitterUser.MaxLength = 16;
            this.tbTwitterUser.Name = "tbTwitterUser";
            this.tbTwitterUser.Size = new System.Drawing.Size(153, 20);
            this.tbTwitterUser.TabIndex = 8;
            this.tbTwitterUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbUsername
            // 
            this.lbUsername.AutoSize = true;
            this.lbUsername.Location = new System.Drawing.Point(16, 46);
            this.lbUsername.Name = "lbUsername";
            this.lbUsername.Size = new System.Drawing.Size(87, 13);
            this.lbUsername.TabIndex = 9;
            this.lbUsername.Text = "twitter username:";
            // 
            // tbTwitterPass
            // 
            this.tbTwitterPass.Enabled = false;
            this.tbTwitterPass.Location = new System.Drawing.Point(113, 122);
            this.tbTwitterPass.MaxLength = 255;
            this.tbTwitterPass.Name = "tbTwitterPass";
            this.tbTwitterPass.PasswordChar = 'X';
            this.tbTwitterPass.Size = new System.Drawing.Size(153, 20);
            this.tbTwitterPass.TabIndex = 10;
            // 
            // lbPassword
            // 
            this.lbPassword.AutoSize = true;
            this.lbPassword.Location = new System.Drawing.Point(16, 125);
            this.lbPassword.Name = "lbPassword";
            this.lbPassword.Size = new System.Drawing.Size(86, 13);
            this.lbPassword.TabIndex = 11;
            this.lbPassword.Text = "twitter password:";
            // 
            // chxRememberTwPass
            // 
            this.chxRememberTwPass.AutoSize = true;
            this.chxRememberTwPass.Location = new System.Drawing.Point(19, 85);
            this.chxRememberTwPass.Name = "chxRememberTwPass";
            this.chxRememberTwPass.Size = new System.Drawing.Size(242, 17);
            this.chxRememberTwPass.TabIndex = 12;
            this.chxRememberTwPass.Text = "Remember password (warning: not encrypted)";
            this.chxRememberTwPass.UseVisualStyleBackColor = true;
            this.chxRememberTwPass.CheckedChanged += new System.EventHandler(this.cbxRememberTwPass_CheckedChanged);
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.Controls.Add(this.tabGeneral);
            this.tabControlSettings.Controls.Add(this.tabAppearance);
            this.tabControlSettings.Controls.Add(this.tabTwitter);
            this.tabControlSettings.Controls.Add(this.tabAdvance);
            this.tabControlSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControlSettings.Location = new System.Drawing.Point(0, 0);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            this.tabControlSettings.Size = new System.Drawing.Size(334, 274);
            this.tabControlSettings.TabIndex = 17;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.cbxActionLeftClick);
            this.tabGeneral.Controls.Add(this.chxConfirmExit);
            this.tabGeneral.Controls.Add(this.chxStartOnBootWindows);
            this.tabGeneral.Controls.Add(this.lbText);
            this.tabGeneral.Controls.Add(this.btnBrowse);
            this.tabGeneral.Controls.Add(this.tbNotesSavePath);
            this.tabGeneral.Controls.Add(this.lblTextNoteLocation);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(326, 248);
            this.tabGeneral.TabIndex = 3;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // cbxActionLeftClick
            // 
            this.cbxActionLeftClick.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxActionLeftClick.FormattingEnabled = true;
            this.cbxActionLeftClick.Items.AddRange(new object[] {
            "Do nothing",
            "Bring notes to front",
            "Create a new note"});
            this.cbxActionLeftClick.Location = new System.Drawing.Point(145, 97);
            this.cbxActionLeftClick.Name = "cbxActionLeftClick";
            this.cbxActionLeftClick.Size = new System.Drawing.Size(163, 21);
            this.cbxActionLeftClick.TabIndex = 16;
            // 
            // chxConfirmExit
            // 
            this.chxConfirmExit.AutoSize = true;
            this.chxConfirmExit.Location = new System.Drawing.Point(20, 57);
            this.chxConfirmExit.Name = "chxConfirmExit";
            this.chxConfirmExit.Size = new System.Drawing.Size(167, 17);
            this.chxConfirmExit.TabIndex = 20;
            this.chxConfirmExit.Text = "Confirm shutdown application.";
            this.chxConfirmExit.UseVisualStyleBackColor = true;
            // 
            // chxStartOnBootWindows
            // 
            this.chxStartOnBootWindows.AutoSize = true;
            this.chxStartOnBootWindows.Location = new System.Drawing.Point(20, 34);
            this.chxStartOnBootWindows.Name = "chxStartOnBootWindows";
            this.chxStartOnBootWindows.Size = new System.Drawing.Size(159, 17);
            this.chxStartOnBootWindows.TabIndex = 10;
            this.chxStartOnBootWindows.Text = "Start automatically on logon.";
            this.chxStartOnBootWindows.UseVisualStyleBackColor = true;
            // 
            // lbText
            // 
            this.lbText.AutoSize = true;
            this.lbText.Location = new System.Drawing.Point(17, 100);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(122, 13);
            this.lbText.TabIndex = 15;
            this.lbText.Text = "Action left click trayicon:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.LightGray;
            this.btnBrowse.Location = new System.Drawing.Point(257, 167);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(53, 23);
            this.btnBrowse.TabIndex = 15;
            this.btnBrowse.Text = "browse";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // tbNotesSavePath
            // 
            this.tbNotesSavePath.Location = new System.Drawing.Point(8, 170);
            this.tbNotesSavePath.Name = "tbNotesSavePath";
            this.tbNotesSavePath.Size = new System.Drawing.Size(243, 20);
            this.tbNotesSavePath.TabIndex = 14;
            this.tbNotesSavePath.Text = "?";
            // 
            // lblTextNoteLocation
            // 
            this.lblTextNoteLocation.AutoSize = true;
            this.lblTextNoteLocation.Location = new System.Drawing.Point(8, 154);
            this.lblTextNoteLocation.Name = "lblTextNoteLocation";
            this.lblTextNoteLocation.Size = new System.Drawing.Size(73, 13);
            this.lblTextNoteLocation.TabIndex = 16;
            this.lblTextNoteLocation.Text = "save notes in:";
            // 
            // tabAppearance
            // 
            this.tabAppearance.Controls.Add(this.label1);
            this.tabAppearance.Controls.Add(this.chxSyntaxHighlightC);
            this.tabAppearance.Controls.Add(this.lblTextpt);
            this.tabAppearance.Controls.Add(this.numFontSize);
            this.tabAppearance.Controls.Add(this.chxSyntaxHighlightHTML);
            this.tabAppearance.Controls.Add(this.lblTextFontSize);
            this.tabAppearance.Controls.Add(this.lbTextNoteFont);
            this.tabAppearance.Controls.Add(this.cbxFontNoteContent);
            this.tabAppearance.Controls.Add(this.chxTransparecy);
            this.tabAppearance.Controls.Add(this.lbDefaultNewNoteColor);
            this.tabAppearance.Controls.Add(this.cbxDefaultColor);
            this.tabAppearance.Controls.Add(this.numProcTransparency);
            this.tabAppearance.Controls.Add(this.lbTextProc);
            this.tabAppearance.Location = new System.Drawing.Point(4, 22);
            this.tabAppearance.Name = "tabAppearance";
            this.tabAppearance.Padding = new System.Windows.Forms.Padding(3);
            this.tabAppearance.Size = new System.Drawing.Size(326, 248);
            this.tabAppearance.TabIndex = 0;
            this.tabAppearance.Text = "Appearance";
            this.tabAppearance.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 173);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "warning experimental features:";
            // 
            // chxSyntaxHighlightC
            // 
            this.chxSyntaxHighlightC.AutoSize = true;
            this.chxSyntaxHighlightC.Location = new System.Drawing.Point(22, 215);
            this.chxSyntaxHighlightC.Name = "chxSyntaxHighlightC";
            this.chxSyntaxHighlightC.Size = new System.Drawing.Size(133, 17);
            this.chxSyntaxHighlightC.TabIndex = 19;
            this.chxSyntaxHighlightC.Text = "Highlight C code notes";
            this.chxSyntaxHighlightC.UseVisualStyleBackColor = true;
            // 
            // lblTextpt
            // 
            this.lblTextpt.AutoSize = true;
            this.lblTextpt.Location = new System.Drawing.Point(151, 129);
            this.lblTextpt.Name = "lblTextpt";
            this.lblTextpt.Size = new System.Drawing.Size(16, 13);
            this.lblTextpt.TabIndex = 14;
            this.lblTextpt.Text = "pt";
            // 
            // numFontSize
            // 
            this.numFontSize.Location = new System.Drawing.Point(107, 125);
            this.numFontSize.Maximum = new decimal(new int[] {
            96,
            0,
            0,
            0});
            this.numFontSize.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numFontSize.Name = "numFontSize";
            this.numFontSize.Size = new System.Drawing.Size(38, 20);
            this.numFontSize.TabIndex = 13;
            this.numFontSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // chxSyntaxHighlightHTML
            // 
            this.chxSyntaxHighlightHTML.AutoSize = true;
            this.chxSyntaxHighlightHTML.Location = new System.Drawing.Point(22, 192);
            this.chxSyntaxHighlightHTML.Name = "chxSyntaxHighlightHTML";
            this.chxSyntaxHighlightHTML.Size = new System.Drawing.Size(156, 17);
            this.chxSyntaxHighlightHTML.TabIndex = 13;
            this.chxSyntaxHighlightHTML.Text = "Highlight HTML code notes";
            this.chxSyntaxHighlightHTML.UseVisualStyleBackColor = true;
            // 
            // lblTextFontSize
            // 
            this.lblTextFontSize.AutoSize = true;
            this.lblTextFontSize.Location = new System.Drawing.Point(16, 127);
            this.lblTextFontSize.Name = "lblTextFontSize";
            this.lblTextFontSize.Size = new System.Drawing.Size(52, 13);
            this.lblTextFontSize.TabIndex = 12;
            this.lblTextFontSize.Text = "Font size:";
            // 
            // lbTextNoteFont
            // 
            this.lbTextNoteFont.AutoSize = true;
            this.lbTextNoteFont.Location = new System.Drawing.Point(16, 95);
            this.lbTextNoteFont.Name = "lbTextNoteFont";
            this.lbTextNoteFont.Size = new System.Drawing.Size(55, 13);
            this.lbTextNoteFont.TabIndex = 9;
            this.lbTextNoteFont.Text = "Font note:";
            // 
            // cbxFontNoteContent
            // 
            this.cbxFontNoteContent.DropDownHeight = 140;
            this.cbxFontNoteContent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFontNoteContent.IntegralHeight = false;
            this.cbxFontNoteContent.Location = new System.Drawing.Point(110, 92);
            this.cbxFontNoteContent.Name = "cbxFontNoteContent";
            this.cbxFontNoteContent.Size = new System.Drawing.Size(182, 21);
            this.cbxFontNoteContent.TabIndex = 8;
            // 
            // tabTwitter
            // 
            this.tabTwitter.Controls.Add(this.lbWarningTwitterPassword);
            this.tabTwitter.Controls.Add(this.lbUsername);
            this.tabTwitter.Controls.Add(this.tbTwitterUser);
            this.tabTwitter.Controls.Add(this.tbTwitterPass);
            this.tabTwitter.Controls.Add(this.lbPassword);
            this.tabTwitter.Controls.Add(this.chxRememberTwPass);
            this.tabTwitter.Location = new System.Drawing.Point(4, 22);
            this.tabTwitter.Name = "tabTwitter";
            this.tabTwitter.Padding = new System.Windows.Forms.Padding(3);
            this.tabTwitter.Size = new System.Drawing.Size(326, 248);
            this.tabTwitter.TabIndex = 1;
            this.tabTwitter.Text = "Twitter";
            this.tabTwitter.UseVisualStyleBackColor = true;
            // 
            // lbWarningTwitterPassword
            // 
            this.lbWarningTwitterPassword.AutoSize = true;
            this.lbWarningTwitterPassword.Location = new System.Drawing.Point(16, 151);
            this.lbWarningTwitterPassword.Name = "lbWarningTwitterPassword";
            this.lbWarningTwitterPassword.Size = new System.Drawing.Size(318, 13);
            this.lbWarningTwitterPassword.TabIndex = 13;
            this.lbWarningTwitterPassword.Text = "Never ever enter your twitter password here on a public computer.";
            // 
            // tabAdvance
            // 
            this.tabAdvance.Controls.Add(this.chxLogErrors);
            this.tabAdvance.Controls.Add(this.btnCrash);
            this.tabAdvance.Controls.Add(this.tbDefaultEmail);
            this.tabAdvance.Controls.Add(this.lbTextDefaultEmail);
            this.tabAdvance.Controls.Add(this.chxConfirmLink);
            this.tabAdvance.Location = new System.Drawing.Point(4, 22);
            this.tabAdvance.Name = "tabAdvance";
            this.tabAdvance.Size = new System.Drawing.Size(326, 248);
            this.tabAdvance.TabIndex = 2;
            this.tabAdvance.Text = "Advance";
            this.tabAdvance.UseVisualStyleBackColor = true;
            // 
            // btnCrash
            // 
            this.btnCrash.Location = new System.Drawing.Point(14, 177);
            this.btnCrash.Name = "btnCrash";
            this.btnCrash.Size = new System.Drawing.Size(101, 47);
            this.btnCrash.TabIndex = 17;
            this.btnCrash.Text = "DEBUG ONLY Crash test!";
            this.btnCrash.UseVisualStyleBackColor = true;
            this.btnCrash.Visible = false;
            this.btnCrash.Click += new System.EventHandler(this.btnCrash_Click);
            // 
            // tbDefaultEmail
            // 
            this.tbDefaultEmail.Location = new System.Drawing.Point(14, 106);
            this.tbDefaultEmail.Name = "tbDefaultEmail";
            this.tbDefaultEmail.Size = new System.Drawing.Size(200, 20);
            this.tbDefaultEmail.TabIndex = 18;
            // 
            // lbTextDefaultEmail
            // 
            this.lbTextDefaultEmail.AutoSize = true;
            this.lbTextDefaultEmail.Location = new System.Drawing.Point(12, 90);
            this.lbTextDefaultEmail.Name = "lbTextDefaultEmail";
            this.lbTextDefaultEmail.Size = new System.Drawing.Size(151, 13);
            this.lbTextDefaultEmail.TabIndex = 17;
            this.lbTextDefaultEmail.Text = "default email adres to send to: ";
            // 
            // chxConfirmLink
            // 
            this.chxConfirmLink.AutoSize = true;
            this.chxConfirmLink.Checked = true;
            this.chxConfirmLink.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxConfirmLink.Location = new System.Drawing.Point(14, 28);
            this.chxConfirmLink.Name = "chxConfirmLink";
            this.chxConfirmLink.Size = new System.Drawing.Size(194, 17);
            this.chxConfirmLink.TabIndex = 18;
            this.chxConfirmLink.Text = "Ask before launching URL, on click";
            this.chxConfirmLink.UseVisualStyleBackColor = true;
            // 
            // chxLogErrors
            // 
            this.chxLogErrors.AutoSize = true;
            this.chxLogErrors.Checked = true;
            this.chxLogErrors.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chxLogErrors.Location = new System.Drawing.Point(14, 52);
            this.chxLogErrors.Name = "chxLogErrors";
            this.chxLogErrors.Size = new System.Drawing.Size(123, 17);
            this.chxLogErrors.TabIndex = 19;
            this.chxLogErrors.Text = "log application errors";
            this.chxLogErrors.UseVisualStyleBackColor = true;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(334, 309);
            this.Controls.Add(this.tabControlSettings);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmSettings";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.numProcTransparency)).EndInit();
            this.tabControlSettings.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.tabAppearance.ResumeLayout(false);
            this.tabAppearance.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).EndInit();
            this.tabTwitter.ResumeLayout(false);
            this.tabTwitter.PerformLayout();
            this.tabAdvance.ResumeLayout(false);
            this.tabAdvance.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chxTransparecy;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbDefaultNewNoteColor;
        private System.Windows.Forms.ComboBox cbxDefaultColor;
        private System.Windows.Forms.NumericUpDown numProcTransparency;
        private System.Windows.Forms.Label lbTextProc;
        private System.Windows.Forms.TextBox tbTwitterUser;
        private System.Windows.Forms.Label lbUsername;
        private System.Windows.Forms.TextBox tbTwitterPass;
        private System.Windows.Forms.Label lbPassword;
        private System.Windows.Forms.CheckBox chxRememberTwPass;
        private System.Windows.Forms.TabControl tabControlSettings;
        private System.Windows.Forms.TabPage tabAppearance;
        private System.Windows.Forms.TabPage tabTwitter;
        private System.Windows.Forms.TabPage tabAdvance;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblTextNoteLocation;
        private System.Windows.Forms.TextBox tbNotesSavePath;
        private System.Windows.Forms.TextBox tbDefaultEmail;
        private System.Windows.Forms.Label lbTextDefaultEmail;
        private System.Windows.Forms.Label lbTextNoteFont;
        private System.Windows.Forms.ComboBox cbxFontNoteContent;
        private System.Windows.Forms.Label lbWarningTwitterPassword;
        private System.Windows.Forms.NumericUpDown numFontSize;
        private System.Windows.Forms.Label lblTextFontSize;
        private System.Windows.Forms.Label lblTextpt;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ComboBox cbxActionLeftClick;
        private System.Windows.Forms.Label lbText;
        private System.Windows.Forms.Button btnCrash;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.CheckBox chxStartOnBootWindows;
        private System.Windows.Forms.CheckBox chxConfirmLink;
        private System.Windows.Forms.CheckBox chxConfirmExit;
        private System.Windows.Forms.CheckBox chxSyntaxHighlightC;
        private System.Windows.Forms.CheckBox chxSyntaxHighlightHTML;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chxLogErrors;
    }
}