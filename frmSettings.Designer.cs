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
            this.label1 = new System.Windows.Forms.Label();
            this.cbxDefaultColor = new System.Windows.Forms.ComboBox();
            this.numProcTransparency = new System.Windows.Forms.NumericUpDown();
            this.lbTextProc = new System.Windows.Forms.Label();
            this.tbTwitterUser = new System.Windows.Forms.TextBox();
            this.lbUsername = new System.Windows.Forms.Label();
            this.tbTwitterPass = new System.Windows.Forms.TextBox();
            this.lbPassword = new System.Windows.Forms.Label();
            this.cbxRememberTwPass = new System.Windows.Forms.CheckBox();
            this.cbxSyntaxHighlight = new System.Windows.Forms.CheckBox();
            this.tbNotesSavePath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblTextNoteLocation = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numProcTransparency)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxTransparecy
            // 
            this.cbxTransparecy.AutoSize = true;
            this.cbxTransparecy.BackColor = System.Drawing.Color.White;
            this.cbxTransparecy.Checked = true;
            this.cbxTransparecy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxTransparecy.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbxTransparecy.Location = new System.Drawing.Point(17, 12);
            this.cbxTransparecy.Name = "cbxTransparecy";
            this.cbxTransparecy.Size = new System.Drawing.Size(129, 18);
            this.cbxTransparecy.TabIndex = 1;
            this.cbxTransparecy.Text = "Enable transparency";
            this.cbxTransparecy.UseVisualStyleBackColor = false;
            this.cbxTransparecy.CheckedChanged += new System.EventHandler(this.cbxTransparecy_CheckedChanged);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.LightGray;
            this.btnOK.Location = new System.Drawing.Point(142, 208);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(133, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.LightGray;
            this.btnCancel.Location = new System.Drawing.Point(5, 208);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(131, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Defaul color new notes:";
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
            this.cbxDefaultColor.Location = new System.Drawing.Point(152, 36);
            this.cbxDefaultColor.MaxDropDownItems = 5;
            this.cbxDefaultColor.Name = "cbxDefaultColor";
            this.cbxDefaultColor.Size = new System.Drawing.Size(123, 21);
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
            this.numProcTransparency.Location = new System.Drawing.Point(152, 10);
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
            this.lbTextProc.Location = new System.Drawing.Point(204, 14);
            this.lbTextProc.Name = "lbTextProc";
            this.lbTextProc.Size = new System.Drawing.Size(15, 13);
            this.lbTextProc.TabIndex = 7;
            this.lbTextProc.Text = "%";
            // 
            // tbTwitterUser
            // 
            this.tbTwitterUser.Location = new System.Drawing.Point(106, 134);
            this.tbTwitterUser.MaxLength = 15;
            this.tbTwitterUser.Name = "tbTwitterUser";
            this.tbTwitterUser.Size = new System.Drawing.Size(166, 20);
            this.tbTwitterUser.TabIndex = 8;
            this.tbTwitterUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbUsername
            // 
            this.lbUsername.AutoSize = true;
            this.lbUsername.Location = new System.Drawing.Point(9, 137);
            this.lbUsername.Name = "lbUsername";
            this.lbUsername.Size = new System.Drawing.Size(87, 13);
            this.lbUsername.TabIndex = 9;
            this.lbUsername.Text = "twitter username:";
            // 
            // tbTwitterPass
            // 
            this.tbTwitterPass.Enabled = false;
            this.tbTwitterPass.Location = new System.Drawing.Point(106, 179);
            this.tbTwitterPass.MaxLength = 30;
            this.tbTwitterPass.Name = "tbTwitterPass";
            this.tbTwitterPass.PasswordChar = 'X';
            this.tbTwitterPass.Size = new System.Drawing.Size(166, 20);
            this.tbTwitterPass.TabIndex = 10;
            // 
            // lbPassword
            // 
            this.lbPassword.AutoSize = true;
            this.lbPassword.Location = new System.Drawing.Point(11, 182);
            this.lbPassword.Name = "lbPassword";
            this.lbPassword.Size = new System.Drawing.Size(86, 13);
            this.lbPassword.TabIndex = 11;
            this.lbPassword.Text = "twitter password:";
            // 
            // cbxRememberTwPass
            // 
            this.cbxRememberTwPass.AutoSize = true;
            this.cbxRememberTwPass.Location = new System.Drawing.Point(12, 160);
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
            this.cbxSyntaxHighlight.Location = new System.Drawing.Point(17, 64);
            this.cbxSyntaxHighlight.Name = "cbxSyntaxHighlight";
            this.cbxSyntaxHighlight.Size = new System.Drawing.Size(94, 17);
            this.cbxSyntaxHighlight.TabIndex = 13;
            this.cbxSyntaxHighlight.Text = "Highlight code";
            this.cbxSyntaxHighlight.UseVisualStyleBackColor = true;
            // 
            // tbNotesSavePath
            // 
            this.tbNotesSavePath.Location = new System.Drawing.Point(12, 105);
            this.tbNotesSavePath.Name = "tbNotesSavePath";
            this.tbNotesSavePath.Size = new System.Drawing.Size(202, 20);
            this.tbNotesSavePath.TabIndex = 14;
            this.tbNotesSavePath.Text = "?";
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.LightGray;
            this.btnBrowse.Location = new System.Drawing.Point(219, 102);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(53, 23);
            this.btnBrowse.TabIndex = 15;
            this.btnBrowse.Text = "browse";
            this.btnBrowse.UseVisualStyleBackColor = false;
            // 
            // lblTextNoteLocation
            // 
            this.lblTextNoteLocation.AutoSize = true;
            this.lblTextNoteLocation.Location = new System.Drawing.Point(9, 84);
            this.lblTextNoteLocation.Name = "lblTextNoteLocation";
            this.lblTextNoteLocation.Size = new System.Drawing.Size(73, 13);
            this.lblTextNoteLocation.TabIndex = 16;
            this.lblTextNoteLocation.Text = "save notes in:";
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(288, 243);
            this.Controls.Add(this.lblTextNoteLocation);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.tbNotesSavePath);
            this.Controls.Add(this.cbxSyntaxHighlight);
            this.Controls.Add(this.cbxRememberTwPass);
            this.Controls.Add(this.lbPassword);
            this.Controls.Add(this.tbTwitterPass);
            this.Controls.Add(this.lbUsername);
            this.Controls.Add(this.tbTwitterUser);
            this.Controls.Add(this.lbTextProc);
            this.Controls.Add(this.numProcTransparency);
            this.Controls.Add(this.cbxDefaultColor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbxTransparecy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmSettings";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.numProcTransparency)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbxTransparecy;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxDefaultColor;
        private System.Windows.Forms.NumericUpDown numProcTransparency;
        private System.Windows.Forms.Label lbTextProc;
        private System.Windows.Forms.TextBox tbTwitterUser;
        private System.Windows.Forms.Label lbUsername;
        private System.Windows.Forms.TextBox tbTwitterPass;
        private System.Windows.Forms.Label lbPassword;
        private System.Windows.Forms.CheckBox cbxRememberTwPass;
        private System.Windows.Forms.CheckBox cbxSyntaxHighlight;
        private System.Windows.Forms.TextBox tbNotesSavePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblTextNoteLocation;
    }
}