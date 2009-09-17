namespace SimplePlainNote
{
    partial class frmSettings
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
            this.cbxTransparecy = new System.Windows.Forms.CheckBox();
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
            this.cbxRememberTwPass = new System.Windows.Forms.CheckBox();
            this.cbxSyntaxHighlight = new System.Windows.Forms.CheckBox();
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabAppearance = new System.Windows.Forms.TabPage();
            this.btnCrash = new System.Windows.Forms.Button();
            this.cbxActionLeftClick = new System.Windows.Forms.ComboBox();
            this.lbText = new System.Windows.Forms.Label();
            this.lblTextpt = new System.Windows.Forms.Label();
            this.numFontSize = new System.Windows.Forms.NumericUpDown();
            this.lblTextFontSize = new System.Windows.Forms.Label();
            this.cbxStartOnBootWindows = new System.Windows.Forms.CheckBox();
            this.lbTextNoteFont = new System.Windows.Forms.Label();
            this.cbxFontNoteContent = new System.Windows.Forms.ComboBox();
            this.tabTwitter = new System.Windows.Forms.TabPage();
            this.lbWarningTwitterPassword = new System.Windows.Forms.Label();
            this.tabAdvance = new System.Windows.Forms.TabPage();
            this.cbxHighlightC = new System.Windows.Forms.CheckBox();
            this.cbxConfirmLink = new System.Windows.Forms.CheckBox();
            this.tbDefaultEmail = new System.Windows.Forms.TextBox();
            this.lbTextDefaultEmail = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblTextNoteLocation = new System.Windows.Forms.Label();
            this.tbNotesSavePath = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.numProcTransparency)).BeginInit();
            this.tabControlSettings.SuspendLayout();
            this.tabAppearance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).BeginInit();
            this.tabTwitter.SuspendLayout();
            this.tabAdvance.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxTransparecy
            // 
            this.cbxTransparecy.AutoSize = true;
            this.cbxTransparecy.BackColor = System.Drawing.Color.White;
            this.cbxTransparecy.Checked = true;
            this.cbxTransparecy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxTransparecy.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbxTransparecy.Location = new System.Drawing.Point(18, 25);
            this.cbxTransparecy.Name = "cbxTransparecy";
            this.cbxTransparecy.Size = new System.Drawing.Size(129, 18);
            this.cbxTransparecy.TabIndex = 1;
            this.cbxTransparecy.Text = "Enable transparency";
            this.cbxTransparecy.UseVisualStyleBackColor = false;
            this.cbxTransparecy.CheckedChanged += new System.EventHandler(this.cbxTransparecy_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnOK.BackColor = System.Drawing.Color.LightGray;
            this.btnOK.Location = new System.Drawing.Point(185, 268);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(141, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnCancel.BackColor = System.Drawing.Color.LightGray;
            this.btnCancel.Location = new System.Drawing.Point(4, 268);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(175, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lbDefaultNewNoteColor
            // 
            this.lbDefaultNewNoteColor.AutoSize = true;
            this.lbDefaultNewNoteColor.Location = new System.Drawing.Point(16, 52);
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
            this.cbxDefaultColor.Location = new System.Drawing.Point(153, 49);
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
            this.tbTwitterUser.Location = new System.Drawing.Point(113, 29);
            this.tbTwitterUser.MaxLength = 16;
            this.tbTwitterUser.Name = "tbTwitterUser";
            this.tbTwitterUser.Size = new System.Drawing.Size(153, 20);
            this.tbTwitterUser.TabIndex = 8;
            this.tbTwitterUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbUsername
            // 
            this.lbUsername.AutoSize = true;
            this.lbUsername.Location = new System.Drawing.Point(16, 32);
            this.lbUsername.Name = "lbUsername";
            this.lbUsername.Size = new System.Drawing.Size(87, 13);
            this.lbUsername.TabIndex = 9;
            this.lbUsername.Text = "twitter username:";
            // 
            // tbTwitterPass
            // 
            this.tbTwitterPass.Enabled = false;
            this.tbTwitterPass.Location = new System.Drawing.Point(113, 110);
            this.tbTwitterPass.MaxLength = 255;
            this.tbTwitterPass.Name = "tbTwitterPass";
            this.tbTwitterPass.PasswordChar = 'X';
            this.tbTwitterPass.Size = new System.Drawing.Size(153, 20);
            this.tbTwitterPass.TabIndex = 10;
            // 
            // lbPassword
            // 
            this.lbPassword.AutoSize = true;
            this.lbPassword.Location = new System.Drawing.Point(21, 113);
            this.lbPassword.Name = "lbPassword";
            this.lbPassword.Size = new System.Drawing.Size(86, 13);
            this.lbPassword.TabIndex = 11;
            this.lbPassword.Text = "twitter password:";
            // 
            // cbxRememberTwPass
            // 
            this.cbxRememberTwPass.AutoSize = true;
            this.cbxRememberTwPass.Location = new System.Drawing.Point(24, 93);
            this.cbxRememberTwPass.Name = "cbxRememberTwPass";
            this.cbxRememberTwPass.Size = new System.Drawing.Size(242, 17);
            this.cbxRememberTwPass.TabIndex = 12;
            this.cbxRememberTwPass.Text = "Remember password (warning: not encrypted)";
            this.cbxRememberTwPass.UseVisualStyleBackColor = true;
            this.cbxRememberTwPass.CheckedChanged += new System.EventHandler(this.cbxRememberTwPass_CheckedChanged);
            // 
            // cbxSyntaxHighlight
            // 
            this.cbxSyntaxHighlight.AutoSize = true;
            this.cbxSyntaxHighlight.Checked = true;
            this.cbxSyntaxHighlight.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxSyntaxHighlight.Location = new System.Drawing.Point(10, 102);
            this.cbxSyntaxHighlight.Name = "cbxSyntaxHighlight";
            this.cbxSyntaxHighlight.Size = new System.Drawing.Size(156, 17);
            this.cbxSyntaxHighlight.TabIndex = 13;
            this.cbxSyntaxHighlight.Text = "Highlight HTML code notes";
            this.cbxSyntaxHighlight.UseVisualStyleBackColor = true;
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlSettings.Controls.Add(this.tabAppearance);
            this.tabControlSettings.Controls.Add(this.tabTwitter);
            this.tabControlSettings.Controls.Add(this.tabAdvance);
            this.tabControlSettings.Location = new System.Drawing.Point(0, 0);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            this.tabControlSettings.Size = new System.Drawing.Size(338, 262);
            this.tabControlSettings.TabIndex = 17;
            // 
            // tabAppearance
            // 
            this.tabAppearance.Controls.Add(this.btnCrash);
            this.tabAppearance.Controls.Add(this.cbxActionLeftClick);
            this.tabAppearance.Controls.Add(this.lbText);
            this.tabAppearance.Controls.Add(this.lblTextpt);
            this.tabAppearance.Controls.Add(this.numFontSize);
            this.tabAppearance.Controls.Add(this.lblTextFontSize);
            this.tabAppearance.Controls.Add(this.cbxStartOnBootWindows);
            this.tabAppearance.Controls.Add(this.lbTextNoteFont);
            this.tabAppearance.Controls.Add(this.cbxFontNoteContent);
            this.tabAppearance.Controls.Add(this.cbxTransparecy);
            this.tabAppearance.Controls.Add(this.lbDefaultNewNoteColor);
            this.tabAppearance.Controls.Add(this.cbxDefaultColor);
            this.tabAppearance.Controls.Add(this.numProcTransparency);
            this.tabAppearance.Controls.Add(this.lbTextProc);
            this.tabAppearance.Location = new System.Drawing.Point(4, 22);
            this.tabAppearance.Name = "tabAppearance";
            this.tabAppearance.Padding = new System.Windows.Forms.Padding(3);
            this.tabAppearance.Size = new System.Drawing.Size(330, 236);
            this.tabAppearance.TabIndex = 0;
            this.tabAppearance.Text = "Appearance";
            this.tabAppearance.UseVisualStyleBackColor = true;
            // 
            // btnCrash
            // 
            this.btnCrash.Location = new System.Drawing.Point(247, 6);
            this.btnCrash.Name = "btnCrash";
            this.btnCrash.Size = new System.Drawing.Size(75, 23);
            this.btnCrash.TabIndex = 17;
            this.btnCrash.Text = "Crash test!";
            this.btnCrash.UseVisualStyleBackColor = true;
            this.btnCrash.Visible = false;
            this.btnCrash.Click += new System.EventHandler(this.btnCrash_Click);
            // 
            // cbxActionLeftClick
            // 
            this.cbxActionLeftClick.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxActionLeftClick.FormattingEnabled = true;
            this.cbxActionLeftClick.Items.AddRange(new object[] {
            "Do nothing",
            "Bring notes to front",
            "Create a new note"});
            this.cbxActionLeftClick.Location = new System.Drawing.Point(153, 141);
            this.cbxActionLeftClick.Name = "cbxActionLeftClick";
            this.cbxActionLeftClick.Size = new System.Drawing.Size(139, 21);
            this.cbxActionLeftClick.TabIndex = 16;
            // 
            // lbText
            // 
            this.lbText.AutoSize = true;
            this.lbText.Location = new System.Drawing.Point(16, 144);
            this.lbText.Name = "lbText";
            this.lbText.Size = new System.Drawing.Size(122, 13);
            this.lbText.TabIndex = 15;
            this.lbText.Text = "Action left click trayicon:";
            // 
            // lblTextpt
            // 
            this.lblTextpt.AutoSize = true;
            this.lblTextpt.Location = new System.Drawing.Point(131, 113);
            this.lblTextpt.Name = "lblTextpt";
            this.lblTextpt.Size = new System.Drawing.Size(16, 13);
            this.lblTextpt.TabIndex = 14;
            this.lblTextpt.Text = "pt";
            // 
            // numFontSize
            // 
            this.numFontSize.Location = new System.Drawing.Point(92, 111);
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
            // lblTextFontSize
            // 
            this.lblTextFontSize.AutoSize = true;
            this.lblTextFontSize.Location = new System.Drawing.Point(19, 113);
            this.lblTextFontSize.Name = "lblTextFontSize";
            this.lblTextFontSize.Size = new System.Drawing.Size(52, 13);
            this.lblTextFontSize.TabIndex = 12;
            this.lblTextFontSize.Text = "Font size:";
            // 
            // cbxStartOnBootWindows
            // 
            this.cbxStartOnBootWindows.AutoSize = true;
            this.cbxStartOnBootWindows.Location = new System.Drawing.Point(18, 177);
            this.cbxStartOnBootWindows.Name = "cbxStartOnBootWindows";
            this.cbxStartOnBootWindows.Size = new System.Drawing.Size(245, 17);
            this.cbxStartOnBootWindows.TabIndex = 10;
            this.cbxStartOnBootWindows.Text = "Start simple plain notes automatically on logon.";
            this.cbxStartOnBootWindows.UseVisualStyleBackColor = true;
            // 
            // lbTextNoteFont
            // 
            this.lbTextNoteFont.AutoSize = true;
            this.lbTextNoteFont.Location = new System.Drawing.Point(16, 79);
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
            this.cbxFontNoteContent.Location = new System.Drawing.Point(110, 76);
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
            this.tabTwitter.Controls.Add(this.cbxRememberTwPass);
            this.tabTwitter.Location = new System.Drawing.Point(4, 22);
            this.tabTwitter.Name = "tabTwitter";
            this.tabTwitter.Padding = new System.Windows.Forms.Padding(3);
            this.tabTwitter.Size = new System.Drawing.Size(330, 236);
            this.tabTwitter.TabIndex = 1;
            this.tabTwitter.Text = "Twitter";
            this.tabTwitter.UseVisualStyleBackColor = true;
            // 
            // lbWarningTwitterPassword
            // 
            this.lbWarningTwitterPassword.AutoSize = true;
            this.lbWarningTwitterPassword.Location = new System.Drawing.Point(3, 133);
            this.lbWarningTwitterPassword.Name = "lbWarningTwitterPassword";
            this.lbWarningTwitterPassword.Size = new System.Drawing.Size(318, 13);
            this.lbWarningTwitterPassword.TabIndex = 13;
            this.lbWarningTwitterPassword.Text = "Never ever enter your twitter password here on a public computer.";
            // 
            // tabAdvance
            // 
            this.tabAdvance.Controls.Add(this.cbxHighlightC);
            this.tabAdvance.Controls.Add(this.cbxConfirmLink);
            this.tabAdvance.Controls.Add(this.tbDefaultEmail);
            this.tabAdvance.Controls.Add(this.lbTextDefaultEmail);
            this.tabAdvance.Controls.Add(this.btnBrowse);
            this.tabAdvance.Controls.Add(this.lblTextNoteLocation);
            this.tabAdvance.Controls.Add(this.cbxSyntaxHighlight);
            this.tabAdvance.Controls.Add(this.tbNotesSavePath);
            this.tabAdvance.Location = new System.Drawing.Point(4, 22);
            this.tabAdvance.Name = "tabAdvance";
            this.tabAdvance.Size = new System.Drawing.Size(330, 236);
            this.tabAdvance.TabIndex = 2;
            this.tabAdvance.Text = "Advance";
            this.tabAdvance.UseVisualStyleBackColor = true;
            // 
            // cbxHighlightC
            // 
            this.cbxHighlightC.AutoSize = true;
            this.cbxHighlightC.Location = new System.Drawing.Point(10, 125);
            this.cbxHighlightC.Name = "cbxHighlightC";
            this.cbxHighlightC.Size = new System.Drawing.Size(145, 17);
            this.cbxHighlightC.TabIndex = 19;
            this.cbxHighlightC.Text = "Highlight C++ code notes";
            this.cbxHighlightC.UseVisualStyleBackColor = true;
            // 
            // cbxConfirmLink
            // 
            this.cbxConfirmLink.AutoSize = true;
            this.cbxConfirmLink.Checked = true;
            this.cbxConfirmLink.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxConfirmLink.Location = new System.Drawing.Point(10, 12);
            this.cbxConfirmLink.Name = "cbxConfirmLink";
            this.cbxConfirmLink.Size = new System.Drawing.Size(194, 17);
            this.cbxConfirmLink.TabIndex = 18;
            this.cbxConfirmLink.Text = "Ask before launching URL, on click";
            this.cbxConfirmLink.UseVisualStyleBackColor = true;
            // 
            // tbDefaultEmail
            // 
            this.tbDefaultEmail.Location = new System.Drawing.Point(115, 164);
            this.tbDefaultEmail.Name = "tbDefaultEmail";
            this.tbDefaultEmail.Size = new System.Drawing.Size(200, 20);
            this.tbDefaultEmail.TabIndex = 18;
            // 
            // lbTextDefaultEmail
            // 
            this.lbTextDefaultEmail.AutoSize = true;
            this.lbTextDefaultEmail.Location = new System.Drawing.Point(10, 168);
            this.lbTextDefaultEmail.Name = "lbTextDefaultEmail";
            this.lbTextDefaultEmail.Size = new System.Drawing.Size(98, 13);
            this.lbTextDefaultEmail.TabIndex = 17;
            this.lbTextDefaultEmail.Text = "default email adres:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.LightGray;
            this.btnBrowse.Location = new System.Drawing.Point(262, 61);
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
            this.lblTextNoteLocation.Location = new System.Drawing.Point(10, 46);
            this.lblTextNoteLocation.Name = "lblTextNoteLocation";
            this.lblTextNoteLocation.Size = new System.Drawing.Size(73, 13);
            this.lblTextNoteLocation.TabIndex = 16;
            this.lblTextNoteLocation.Text = "save notes in:";
            // 
            // tbNotesSavePath
            // 
            this.tbNotesSavePath.Location = new System.Drawing.Point(8, 64);
            this.tbNotesSavePath.Name = "tbNotesSavePath";
            this.tbNotesSavePath.Size = new System.Drawing.Size(248, 20);
            this.tbNotesSavePath.TabIndex = 14;
            this.tbNotesSavePath.Text = "?";
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(338, 293);
            this.Controls.Add(this.tabControlSettings);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmSettings";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.numProcTransparency)).EndInit();
            this.tabControlSettings.ResumeLayout(false);
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

        private System.Windows.Forms.CheckBox cbxTransparecy;
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
        private System.Windows.Forms.CheckBox cbxRememberTwPass;
        private System.Windows.Forms.CheckBox cbxSyntaxHighlight;
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
        private System.Windows.Forms.CheckBox cbxStartOnBootWindows;
        private System.Windows.Forms.NumericUpDown numFontSize;
        private System.Windows.Forms.Label lblTextFontSize;
        private System.Windows.Forms.Label lblTextpt;
        private System.Windows.Forms.CheckBox cbxConfirmLink;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.CheckBox cbxHighlightC;
        private System.Windows.Forms.ComboBox cbxActionLeftClick;
        private System.Windows.Forms.Label lbText;
        private System.Windows.Forms.Button btnCrash;
    }
}