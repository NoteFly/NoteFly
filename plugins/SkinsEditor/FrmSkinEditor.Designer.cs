namespace SkinsEditor
{
    /// <summary>
    /// FrmSkinEditor form
    /// </summary>
    public partial class FrmSkinEditor
    {

        /// <summary>
        /// ColorDialog colordlg
        /// </summary>
        private System.Windows.Forms.ColorDialog colordlg;

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
            this.colordlg = new System.Windows.Forms.ColorDialog();
            this.openFileTextureDialog = new System.Windows.Forms.OpenFileDialog();
            this.lbxSkins = new System.Windows.Forms.ListBox();
            this.btnBrowsePrimaryTexture = new System.Windows.Forms.Button();
            this.lblTextSkins = new System.Windows.Forms.Label();
            this.tbPrimaryTexture = new System.Windows.Forms.TextBox();
            this.lblTextPrimaryTexture = new System.Windows.Forms.Label();
            this.pnlClrText = new System.Windows.Forms.Panel();
            this.pnlClrHighlight = new System.Windows.Forms.Panel();
            this.pnlClrSelecting = new System.Windows.Forms.Panel();
            this.pnlClrPrimary = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTextSkinname = new System.Windows.Forms.Label();
            this.btnEditskin = new System.Windows.Forms.Button();
            this.tbSkinName = new System.Windows.Forms.TextBox();
            this.btnNewSkin = new System.Windows.Forms.Button();
            this.lblTextPrimarycolor = new System.Windows.Forms.Label();
            this.btnSaveSkin = new System.Windows.Forms.Button();
            this.tbPrimaryColor = new System.Windows.Forms.TextBox();
            this.btnPickTextColor = new System.Windows.Forms.Button();
            this.lblTextSelectingcolor = new System.Windows.Forms.Label();
            this.tbTextColor = new System.Windows.Forms.TextBox();
            this.tbSelectingColor = new System.Windows.Forms.TextBox();
            this.lblTextTextcolor = new System.Windows.Forms.Label();
            this.lblTextHighlightcolor = new System.Windows.Forms.Label();
            this.btnPickHighlightColor = new System.Windows.Forms.Button();
            this.tbHighlightingColor = new System.Windows.Forms.TextBox();
            this.btnPickSelectingColor = new System.Windows.Forms.Button();
            this.btnPickPrimaryColor = new System.Windows.Forms.Button();
            this.btnDeleteSkin = new System.Windows.Forms.Button();
            this.lblTextPrimartTextureLayout = new System.Windows.Forms.Label();
            this.cbxPrimaryTextureLayout = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // openFileTextureDialog
            // 
            this.openFileTextureDialog.Filter = "Image files|*.png;*.tif;*.tiff;*.bmp;*.gif;*.jpg;*.jpeg";
            // 
            // lbxSkins
            // 
            this.lbxSkins.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lbxSkins.FormattingEnabled = true;
            this.lbxSkins.Location = new System.Drawing.Point(12, 70);
            this.lbxSkins.Name = "lbxSkins";
            this.lbxSkins.Size = new System.Drawing.Size(118, 225);
            this.lbxSkins.TabIndex = 28;
            this.lbxSkins.SelectedIndexChanged += new System.EventHandler(this.lbxSkins_SelectedIndexChanged);
            // 
            // btnBrowsePrimaryTexture
            // 
            this.btnBrowsePrimaryTexture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowsePrimaryTexture.Enabled = false;
            this.btnBrowsePrimaryTexture.Location = new System.Drawing.Point(343, 96);
            this.btnBrowsePrimaryTexture.Name = "btnBrowsePrimaryTexture";
            this.btnBrowsePrimaryTexture.Size = new System.Drawing.Size(35, 23);
            this.btnBrowsePrimaryTexture.TabIndex = 54;
            this.btnBrowsePrimaryTexture.Text = "...";
            this.btnBrowsePrimaryTexture.UseCompatibleTextRendering = true;
            this.btnBrowsePrimaryTexture.UseVisualStyleBackColor = true;
            this.btnBrowsePrimaryTexture.Click += new System.EventHandler(this.btnBrowsePrimaryTexture_Click);
            // 
            // lblTextSkins
            // 
            this.lblTextSkins.AutoSize = true;
            this.lblTextSkins.Location = new System.Drawing.Point(12, 50);
            this.lblTextSkins.Name = "lblTextSkins";
            this.lblTextSkins.Size = new System.Drawing.Size(35, 17);
            this.lblTextSkins.TabIndex = 29;
            this.lblTextSkins.Text = "Skins:";
            this.lblTextSkins.UseCompatibleTextRendering = true;
            // 
            // tbPrimaryTexture
            // 
            this.tbPrimaryTexture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPrimaryTexture.Enabled = false;
            this.tbPrimaryTexture.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPrimaryTexture.Location = new System.Drawing.Point(260, 97);
            this.tbPrimaryTexture.MaxLength = 259;
            this.tbPrimaryTexture.Name = "tbPrimaryTexture";
            this.tbPrimaryTexture.Size = new System.Drawing.Size(82, 22);
            this.tbPrimaryTexture.TabIndex = 53;
            // 
            // lblTextPrimaryTexture
            // 
            this.lblTextPrimaryTexture.AutoSize = true;
            this.lblTextPrimaryTexture.Location = new System.Drawing.Point(153, 102);
            this.lblTextPrimaryTexture.Name = "lblTextPrimaryTexture";
            this.lblTextPrimaryTexture.Size = new System.Drawing.Size(79, 13);
            this.lblTextPrimaryTexture.TabIndex = 52;
            this.lblTextPrimaryTexture.Text = "Primary texture:";
            // 
            // pnlClrText
            // 
            this.pnlClrText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClrText.ForeColor = System.Drawing.Color.Black;
            this.pnlClrText.Location = new System.Drawing.Point(321, 241);
            this.pnlClrText.Name = "pnlClrText";
            this.pnlClrText.Size = new System.Drawing.Size(21, 21);
            this.pnlClrText.TabIndex = 51;
            // 
            // pnlClrHighlight
            // 
            this.pnlClrHighlight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClrHighlight.ForeColor = System.Drawing.Color.Black;
            this.pnlClrHighlight.Location = new System.Drawing.Point(321, 213);
            this.pnlClrHighlight.Name = "pnlClrHighlight";
            this.pnlClrHighlight.Size = new System.Drawing.Size(21, 21);
            this.pnlClrHighlight.TabIndex = 50;
            // 
            // pnlClrSelecting
            // 
            this.pnlClrSelecting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClrSelecting.ForeColor = System.Drawing.Color.Black;
            this.pnlClrSelecting.Location = new System.Drawing.Point(321, 184);
            this.pnlClrSelecting.Name = "pnlClrSelecting";
            this.pnlClrSelecting.Size = new System.Drawing.Size(21, 21);
            this.pnlClrSelecting.TabIndex = 49;
            // 
            // pnlClrPrimary
            // 
            this.pnlClrPrimary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClrPrimary.ForeColor = System.Drawing.Color.Black;
            this.pnlClrPrimary.Location = new System.Drawing.Point(321, 156);
            this.pnlClrPrimary.Name = "pnlClrPrimary";
            this.pnlClrPrimary.Size = new System.Drawing.Size(21, 21);
            this.pnlClrPrimary.TabIndex = 48;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Silver;
            this.btnClose.Location = new System.Drawing.Point(260, 272);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 23);
            this.btnClose.TabIndex = 47;
            this.btnClose.Text = "close";
            this.btnClose.UseCompatibleTextRendering = true;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTextSkinname
            // 
            this.lblTextSkinname.AutoSize = true;
            this.lblTextSkinname.Location = new System.Drawing.Point(153, 70);
            this.lblTextSkinname.Name = "lblTextSkinname";
            this.lblTextSkinname.Size = new System.Drawing.Size(60, 13);
            this.lblTextSkinname.TabIndex = 30;
            this.lblTextSkinname.Text = "Skin name:";
            // 
            // btnEditskin
            // 
            this.btnEditskin.BackColor = System.Drawing.Color.Silver;
            this.btnEditskin.Enabled = false;
            this.btnEditskin.Location = new System.Drawing.Point(252, 12);
            this.btnEditskin.Name = "btnEditskin";
            this.btnEditskin.Size = new System.Drawing.Size(128, 23);
            this.btnEditskin.TabIndex = 46;
            this.btnEditskin.Text = "&edit skin";
            this.btnEditskin.UseCompatibleTextRendering = true;
            this.btnEditskin.UseVisualStyleBackColor = false;
            this.btnEditskin.Click += new System.EventHandler(this.btnEditskin_Click);
            // 
            // tbSkinName
            // 
            this.tbSkinName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSkinName.Enabled = false;
            this.tbSkinName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSkinName.Location = new System.Drawing.Point(260, 67);
            this.tbSkinName.MaxLength = 200;
            this.tbSkinName.Name = "tbSkinName";
            this.tbSkinName.Size = new System.Drawing.Size(120, 22);
            this.tbSkinName.TabIndex = 31;
            // 
            // btnNewSkin
            // 
            this.btnNewSkin.BackColor = System.Drawing.Color.Silver;
            this.btnNewSkin.Location = new System.Drawing.Point(123, 12);
            this.btnNewSkin.Name = "btnNewSkin";
            this.btnNewSkin.Size = new System.Drawing.Size(121, 23);
            this.btnNewSkin.TabIndex = 45;
            this.btnNewSkin.Text = "&new skin";
            this.btnNewSkin.UseCompatibleTextRendering = true;
            this.btnNewSkin.UseVisualStyleBackColor = false;
            this.btnNewSkin.Click += new System.EventHandler(this.btnNewSkin_Click);
            // 
            // lblTextPrimarycolor
            // 
            this.lblTextPrimarycolor.AutoSize = true;
            this.lblTextPrimarycolor.Location = new System.Drawing.Point(153, 159);
            this.lblTextPrimarycolor.Name = "lblTextPrimarycolor";
            this.lblTextPrimarycolor.Size = new System.Drawing.Size(70, 13);
            this.lblTextPrimarycolor.TabIndex = 32;
            this.lblTextPrimarycolor.Text = "Primary color:";
            // 
            // btnSaveSkin
            // 
            this.btnSaveSkin.BackColor = System.Drawing.Color.Silver;
            this.btnSaveSkin.Enabled = false;
            this.btnSaveSkin.Location = new System.Drawing.Point(147, 272);
            this.btnSaveSkin.Name = "btnSaveSkin";
            this.btnSaveSkin.Size = new System.Drawing.Size(107, 23);
            this.btnSaveSkin.TabIndex = 44;
            this.btnSaveSkin.Text = "save";
            this.btnSaveSkin.UseCompatibleTextRendering = true;
            this.btnSaveSkin.UseVisualStyleBackColor = false;
            this.btnSaveSkin.Click += new System.EventHandler(this.btnSaveSkin_Click);
            // 
            // tbPrimaryColor
            // 
            this.tbPrimaryColor.Enabled = false;
            this.tbPrimaryColor.Location = new System.Drawing.Point(260, 156);
            this.tbPrimaryColor.MaxLength = 7;
            this.tbPrimaryColor.Name = "tbPrimaryColor";
            this.tbPrimaryColor.Size = new System.Drawing.Size(59, 20);
            this.tbPrimaryColor.TabIndex = 33;
            this.tbPrimaryColor.Tag = "1";
            this.tbPrimaryColor.WordWrap = false;
            this.tbPrimaryColor.TextChanged += new System.EventHandler(this.ParserAsPreviewColor);
            // 
            // btnPickTextColor
            // 
            this.btnPickTextColor.Enabled = false;
            this.btnPickTextColor.Location = new System.Drawing.Point(343, 239);
            this.btnPickTextColor.Name = "btnPickTextColor";
            this.btnPickTextColor.Size = new System.Drawing.Size(35, 23);
            this.btnPickTextColor.TabIndex = 43;
            this.btnPickTextColor.Text = "...";
            this.btnPickTextColor.UseCompatibleTextRendering = true;
            this.btnPickTextColor.UseVisualStyleBackColor = true;
            this.btnPickTextColor.Click += new System.EventHandler(this.btnTextColor_Click);
            // 
            // lblTextSelectingcolor
            // 
            this.lblTextSelectingcolor.AutoSize = true;
            this.lblTextSelectingcolor.Location = new System.Drawing.Point(153, 187);
            this.lblTextSelectingcolor.Name = "lblTextSelectingcolor";
            this.lblTextSelectingcolor.Size = new System.Drawing.Size(80, 13);
            this.lblTextSelectingcolor.TabIndex = 34;
            this.lblTextSelectingcolor.Text = "Selecting color:";
            // 
            // tbTextColor
            // 
            this.tbTextColor.Enabled = false;
            this.tbTextColor.Location = new System.Drawing.Point(260, 241);
            this.tbTextColor.MaxLength = 7;
            this.tbTextColor.Name = "tbTextColor";
            this.tbTextColor.Size = new System.Drawing.Size(59, 20);
            this.tbTextColor.TabIndex = 42;
            this.tbTextColor.Tag = "4";
            this.tbTextColor.WordWrap = false;
            this.tbTextColor.TextChanged += new System.EventHandler(this.ParserAsPreviewColor);
            // 
            // tbSelectingColor
            // 
            this.tbSelectingColor.Enabled = false;
            this.tbSelectingColor.Location = new System.Drawing.Point(260, 184);
            this.tbSelectingColor.MaxLength = 7;
            this.tbSelectingColor.Name = "tbSelectingColor";
            this.tbSelectingColor.Size = new System.Drawing.Size(59, 20);
            this.tbSelectingColor.TabIndex = 35;
            this.tbSelectingColor.Tag = "2";
            this.tbSelectingColor.WordWrap = false;
            this.tbSelectingColor.TextChanged += new System.EventHandler(this.ParserAsPreviewColor);
            // 
            // lblTextTextcolor
            // 
            this.lblTextTextcolor.AutoSize = true;
            this.lblTextTextcolor.Location = new System.Drawing.Point(153, 244);
            this.lblTextTextcolor.Name = "lblTextTextcolor";
            this.lblTextTextcolor.Size = new System.Drawing.Size(57, 13);
            this.lblTextTextcolor.TabIndex = 41;
            this.lblTextTextcolor.Text = "Text color:";
            // 
            // lblTextHighlightcolor
            // 
            this.lblTextHighlightcolor.AutoSize = true;
            this.lblTextHighlightcolor.Location = new System.Drawing.Point(153, 216);
            this.lblTextHighlightcolor.Name = "lblTextHighlightcolor";
            this.lblTextHighlightcolor.Size = new System.Drawing.Size(91, 13);
            this.lblTextHighlightcolor.TabIndex = 36;
            this.lblTextHighlightcolor.Text = "Highlighting color:";
            // 
            // btnPickHighlightColor
            // 
            this.btnPickHighlightColor.Enabled = false;
            this.btnPickHighlightColor.Location = new System.Drawing.Point(343, 211);
            this.btnPickHighlightColor.Name = "btnPickHighlightColor";
            this.btnPickHighlightColor.Size = new System.Drawing.Size(35, 23);
            this.btnPickHighlightColor.TabIndex = 40;
            this.btnPickHighlightColor.Text = "...";
            this.btnPickHighlightColor.UseCompatibleTextRendering = true;
            this.btnPickHighlightColor.UseVisualStyleBackColor = true;
            this.btnPickHighlightColor.Click += new System.EventHandler(this.btnHighlightColor_Click);
            // 
            // tbHighlightingColor
            // 
            this.tbHighlightingColor.Enabled = false;
            this.tbHighlightingColor.Location = new System.Drawing.Point(260, 213);
            this.tbHighlightingColor.MaxLength = 7;
            this.tbHighlightingColor.Name = "tbHighlightingColor";
            this.tbHighlightingColor.Size = new System.Drawing.Size(59, 20);
            this.tbHighlightingColor.TabIndex = 37;
            this.tbHighlightingColor.Tag = "3";
            this.tbHighlightingColor.WordWrap = false;
            this.tbHighlightingColor.TextChanged += new System.EventHandler(this.ParserAsPreviewColor);
            // 
            // btnPickSelectingColor
            // 
            this.btnPickSelectingColor.Enabled = false;
            this.btnPickSelectingColor.Location = new System.Drawing.Point(343, 181);
            this.btnPickSelectingColor.Name = "btnPickSelectingColor";
            this.btnPickSelectingColor.Size = new System.Drawing.Size(35, 23);
            this.btnPickSelectingColor.TabIndex = 39;
            this.btnPickSelectingColor.Text = "...";
            this.btnPickSelectingColor.UseCompatibleTextRendering = true;
            this.btnPickSelectingColor.UseVisualStyleBackColor = true;
            this.btnPickSelectingColor.Click += new System.EventHandler(this.btnSelectingColor_Click);
            // 
            // btnPickPrimaryColor
            // 
            this.btnPickPrimaryColor.Enabled = false;
            this.btnPickPrimaryColor.Location = new System.Drawing.Point(343, 154);
            this.btnPickPrimaryColor.Name = "btnPickPrimaryColor";
            this.btnPickPrimaryColor.Size = new System.Drawing.Size(35, 23);
            this.btnPickPrimaryColor.TabIndex = 38;
            this.btnPickPrimaryColor.Text = "...";
            this.btnPickPrimaryColor.UseCompatibleTextRendering = true;
            this.btnPickPrimaryColor.UseVisualStyleBackColor = true;
            this.btnPickPrimaryColor.Click += new System.EventHandler(this.btnPickPrimaryColor_Click);
            // 
            // btnDeleteSkin
            // 
            this.btnDeleteSkin.BackColor = System.Drawing.Color.Silver;
            this.btnDeleteSkin.Enabled = false;
            this.btnDeleteSkin.ForeColor = System.Drawing.Color.DarkRed;
            this.btnDeleteSkin.Location = new System.Drawing.Point(12, 12);
            this.btnDeleteSkin.Name = "btnDeleteSkin";
            this.btnDeleteSkin.Size = new System.Drawing.Size(107, 23);
            this.btnDeleteSkin.TabIndex = 55;
            this.btnDeleteSkin.Text = "&delete skin";
            this.btnDeleteSkin.UseCompatibleTextRendering = true;
            this.btnDeleteSkin.UseVisualStyleBackColor = false;
            this.btnDeleteSkin.Click += new System.EventHandler(this.btnDeleteSkin_Click);
            // 
            // lblTextPrimartTextureLayout
            // 
            this.lblTextPrimartTextureLayout.AutoSize = true;
            this.lblTextPrimartTextureLayout.Location = new System.Drawing.Point(153, 128);
            this.lblTextPrimartTextureLayout.Name = "lblTextPrimartTextureLayout";
            this.lblTextPrimartTextureLayout.Size = new System.Drawing.Size(77, 13);
            this.lblTextPrimartTextureLayout.TabIndex = 56;
            this.lblTextPrimartTextureLayout.Text = "Texture layout:";
            // 
            // cbxPrimaryTextureLayout
            // 
            this.cbxPrimaryTextureLayout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPrimaryTextureLayout.Enabled = false;
            this.cbxPrimaryTextureLayout.FormattingEnabled = true;
            this.cbxPrimaryTextureLayout.Items.AddRange(new object[] {
            "Tiled",
            "Centred",
            "Screted"});
            this.cbxPrimaryTextureLayout.Location = new System.Drawing.Point(260, 125);
            this.cbxPrimaryTextureLayout.MaxDropDownItems = 3;
            this.cbxPrimaryTextureLayout.Name = "cbxPrimaryTextureLayout";
            this.cbxPrimaryTextureLayout.Size = new System.Drawing.Size(82, 21);
            this.cbxPrimaryTextureLayout.TabIndex = 57;
            // 
            // FrmSkinEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(394, 307);
            this.Controls.Add(this.cbxPrimaryTextureLayout);
            this.Controls.Add(this.lblTextPrimartTextureLayout);
            this.Controls.Add(this.btnDeleteSkin);
            this.Controls.Add(this.lbxSkins);
            this.Controls.Add(this.btnBrowsePrimaryTexture);
            this.Controls.Add(this.lblTextSkins);
            this.Controls.Add(this.tbPrimaryTexture);
            this.Controls.Add(this.lblTextPrimaryTexture);
            this.Controls.Add(this.pnlClrText);
            this.Controls.Add(this.pnlClrHighlight);
            this.Controls.Add(this.pnlClrSelecting);
            this.Controls.Add(this.pnlClrPrimary);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblTextSkinname);
            this.Controls.Add(this.btnEditskin);
            this.Controls.Add(this.tbSkinName);
            this.Controls.Add(this.btnNewSkin);
            this.Controls.Add(this.lblTextPrimarycolor);
            this.Controls.Add(this.btnSaveSkin);
            this.Controls.Add(this.tbPrimaryColor);
            this.Controls.Add(this.btnPickTextColor);
            this.Controls.Add(this.lblTextSelectingcolor);
            this.Controls.Add(this.tbTextColor);
            this.Controls.Add(this.tbSelectingColor);
            this.Controls.Add(this.lblTextTextcolor);
            this.Controls.Add(this.lblTextHighlightcolor);
            this.Controls.Add(this.btnPickHighlightColor);
            this.Controls.Add(this.tbHighlightingColor);
            this.Controls.Add(this.btnPickSelectingColor);
            this.Controls.Add(this.btnPickPrimaryColor);
            this.Name = "FrmSkinEditor";
            this.Text = "Skin editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileTextureDialog;
        private System.Windows.Forms.ListBox lbxSkins;
        private System.Windows.Forms.Button btnBrowsePrimaryTexture;
        private System.Windows.Forms.Label lblTextSkins;
        private System.Windows.Forms.TextBox tbPrimaryTexture;
        private System.Windows.Forms.Label lblTextPrimaryTexture;
        private System.Windows.Forms.Panel pnlClrText;
        private System.Windows.Forms.Panel pnlClrHighlight;
        private System.Windows.Forms.Panel pnlClrSelecting;
        private System.Windows.Forms.Panel pnlClrPrimary;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTextSkinname;
        private System.Windows.Forms.Button btnEditskin;
        private System.Windows.Forms.TextBox tbSkinName;
        private System.Windows.Forms.Button btnNewSkin;
        private System.Windows.Forms.Label lblTextPrimarycolor;
        private System.Windows.Forms.Button btnSaveSkin;
        private System.Windows.Forms.TextBox tbPrimaryColor;
        private System.Windows.Forms.Button btnPickTextColor;
        private System.Windows.Forms.Label lblTextSelectingcolor;
        private System.Windows.Forms.TextBox tbTextColor;
        private System.Windows.Forms.TextBox tbSelectingColor;
        private System.Windows.Forms.Label lblTextTextcolor;
        private System.Windows.Forms.Label lblTextHighlightcolor;
        private System.Windows.Forms.Button btnPickHighlightColor;
        private System.Windows.Forms.TextBox tbHighlightingColor;
        private System.Windows.Forms.Button btnPickSelectingColor;
        private System.Windows.Forms.Button btnPickPrimaryColor;
        private System.Windows.Forms.Button btnDeleteSkin;
        private System.Windows.Forms.Label lblTextPrimartTextureLayout;
        private System.Windows.Forms.ComboBox cbxPrimaryTextureLayout;
    }
}