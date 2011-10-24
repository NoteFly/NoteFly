namespace SkinsEditor
{
    /// <summary>
    /// FrmSkinEditor form
    /// </summary>
    public partial class FrmSkinEditor
    {
        /// <summary>
        /// ListBox lbxSkins
        /// </summary>
        private System.Windows.Forms.ListBox lbxSkins;

        /// <summary>
        /// Label lbTextSkins
        /// </summary>
        private System.Windows.Forms.Label lblTextSkins;

        /// <summary>
        /// Label lblTextSkinname
        /// </summary>
        private System.Windows.Forms.Label lblTextSkinname;

        /// <summary>
        /// TextBox  tbSkinName
        /// </summary>
        private System.Windows.Forms.TextBox tbSkinName;

        /// <summary>
        /// Label lblTextPrimarycolor
        /// </summary>
        private System.Windows.Forms.Label lblTextPrimarycolor;

        /// <summary>
        /// TextBox tbPrimaryColor
        /// </summary>
        private System.Windows.Forms.TextBox tbPrimaryColor;

        /// <summary>
        /// Label lblTextSelectingcolor
        /// </summary>
        private System.Windows.Forms.Label lblTextSelectingcolor;

        /// <summary>
        /// TextBox tbSelectingColor
        /// </summary>
        private System.Windows.Forms.TextBox tbSelectingColor;

        /// <summary>
        /// Label lblTextHighlightcolor
        /// </summary>
        private System.Windows.Forms.Label lblTextHighlightcolor;

        /// <summary>
        /// TextBox tbHighlightingColor
        /// </summary>
        private System.Windows.Forms.TextBox tbHighlightingColor;

        /// <summary>
        /// ColorDialog colordlg
        /// </summary>
        private System.Windows.Forms.ColorDialog colordlg;

        /// <summary>
        /// Button btnPickPrimaryColor
        /// </summary>
        private System.Windows.Forms.Button btnPickPrimaryColor;

        /// <summary>
        /// Button btnPickSelectingColor
        /// </summary>
        private System.Windows.Forms.Button btnPickSelectingColor;

        /// <summary>
        /// Button btnPickHighlightColor
        /// </summary>
        private System.Windows.Forms.Button btnPickHighlightColor;

        /// <summary>
        /// Label lblTextTextcolor
        /// </summary>
        private System.Windows.Forms.Label lblTextTextcolor;

        /// <summary>
        /// TextBox tbTextColor
        /// </summary>
        private System.Windows.Forms.TextBox tbTextColor;

        /// <summary>
        /// Button btnPickTextColor
        /// </summary>
        private System.Windows.Forms.Button btnPickTextColor;

        /// <summary>
        /// Button btnSaveSkin
        /// </summary>
        private System.Windows.Forms.Button btnSaveSkin;

        /// <summary>
        /// Button btnNewSkin
        /// </summary>
        private System.Windows.Forms.Button btnNewSkin;

        /// <summary>
        /// Button btnEditskin
        /// </summary>
        private System.Windows.Forms.Button btnEditskin;

        /// <summary>
        /// Button btnClose
        /// </summary>
        private System.Windows.Forms.Button btnClose;

        /// <summary>
        /// SplitContainer splitContainer1
        /// </summary>
        private System.Windows.Forms.SplitContainer splitContainer1;

        /// <summary>
        /// Panel pnlClrText
        /// </summary>
        private System.Windows.Forms.Panel pnlClrText;

        /// <summary>
        /// Panel pnlClrHighlight
        /// </summary>
        private System.Windows.Forms.Panel pnlClrHighlight;

        /// <summary>
        /// Panel pnlClrSelecting
        /// </summary>
        private System.Windows.Forms.Panel pnlClrSelecting;

        /// <summary>
        /// Panel pnlClrPrimary
        /// </summary>
        private System.Windows.Forms.Panel pnlClrPrimary;

        private System.Windows.Forms.Button btnBrowsePrimaryTexture;

        private System.Windows.Forms.TextBox tbPrimaryTexture;

        private System.Windows.Forms.Label lblTextPrimaryTexture;

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
            this.lbxSkins = new System.Windows.Forms.ListBox();
            this.lblTextSkins = new System.Windows.Forms.Label();
            this.lblTextSkinname = new System.Windows.Forms.Label();
            this.tbSkinName = new System.Windows.Forms.TextBox();
            this.lblTextPrimarycolor = new System.Windows.Forms.Label();
            this.tbPrimaryColor = new System.Windows.Forms.TextBox();
            this.lblTextSelectingcolor = new System.Windows.Forms.Label();
            this.tbSelectingColor = new System.Windows.Forms.TextBox();
            this.lblTextHighlightcolor = new System.Windows.Forms.Label();
            this.tbHighlightingColor = new System.Windows.Forms.TextBox();
            this.colordlg = new System.Windows.Forms.ColorDialog();
            this.btnPickPrimaryColor = new System.Windows.Forms.Button();
            this.btnPickSelectingColor = new System.Windows.Forms.Button();
            this.btnPickHighlightColor = new System.Windows.Forms.Button();
            this.lblTextTextcolor = new System.Windows.Forms.Label();
            this.tbTextColor = new System.Windows.Forms.TextBox();
            this.btnPickTextColor = new System.Windows.Forms.Button();
            this.btnSaveSkin = new System.Windows.Forms.Button();
            this.btnNewSkin = new System.Windows.Forms.Button();
            this.btnEditskin = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnBrowsePrimaryTexture = new System.Windows.Forms.Button();
            this.tbPrimaryTexture = new System.Windows.Forms.TextBox();
            this.lblTextPrimaryTexture = new System.Windows.Forms.Label();
            this.pnlClrText = new System.Windows.Forms.Panel();
            this.pnlClrHighlight = new System.Windows.Forms.Panel();
            this.pnlClrSelecting = new System.Windows.Forms.Panel();
            this.pnlClrPrimary = new System.Windows.Forms.Panel();
            this.openFileTextureDialog = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbxSkins
            // 
            this.lbxSkins.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxSkins.FormattingEnabled = true;
            this.lbxSkins.Location = new System.Drawing.Point(3, 28);
            this.lbxSkins.Name = "lbxSkins";
            this.lbxSkins.Size = new System.Drawing.Size(118, 225);
            this.lbxSkins.TabIndex = 1;
            this.lbxSkins.SelectedIndexChanged += new System.EventHandler(this.lbxSkins_SelectedIndexChanged);
            // 
            // lblTextSkins
            // 
            this.lblTextSkins.AutoSize = true;
            this.lblTextSkins.Location = new System.Drawing.Point(3, 5);
            this.lblTextSkins.Name = "lblTextSkins";
            this.lblTextSkins.Size = new System.Drawing.Size(35, 17);
            this.lblTextSkins.TabIndex = 2;
            this.lblTextSkins.Text = "Skins:";
            this.lblTextSkins.UseCompatibleTextRendering = true;
            // 
            // lblTextSkinname
            // 
            this.lblTextSkinname.AutoSize = true;
            this.lblTextSkinname.Location = new System.Drawing.Point(34, 50);
            this.lblTextSkinname.Name = "lblTextSkinname";
            this.lblTextSkinname.Size = new System.Drawing.Size(60, 13);
            this.lblTextSkinname.TabIndex = 3;
            this.lblTextSkinname.Text = "Skin name:";
            // 
            // tbSkinName
            // 
            this.tbSkinName.Enabled = false;
            this.tbSkinName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSkinName.Location = new System.Drawing.Point(141, 47);
            this.tbSkinName.MaxLength = 255;
            this.tbSkinName.Name = "tbSkinName";
            this.tbSkinName.Size = new System.Drawing.Size(120, 22);
            this.tbSkinName.TabIndex = 4;
            // 
            // lblTextPrimarycolor
            // 
            this.lblTextPrimarycolor.AutoSize = true;
            this.lblTextPrimarycolor.Location = new System.Drawing.Point(34, 109);
            this.lblTextPrimarycolor.Name = "lblTextPrimarycolor";
            this.lblTextPrimarycolor.Size = new System.Drawing.Size(70, 13);
            this.lblTextPrimarycolor.TabIndex = 5;
            this.lblTextPrimarycolor.Text = "Primary color:";
            // 
            // tbPrimaryColor
            // 
            this.tbPrimaryColor.Enabled = false;
            this.tbPrimaryColor.Location = new System.Drawing.Point(141, 106);
            this.tbPrimaryColor.MaxLength = 7;
            this.tbPrimaryColor.Name = "tbPrimaryColor";
            this.tbPrimaryColor.Size = new System.Drawing.Size(59, 20);
            this.tbPrimaryColor.TabIndex = 6;
            this.tbPrimaryColor.Tag = "1";
            this.tbPrimaryColor.WordWrap = false;
            this.tbPrimaryColor.TextChanged += new System.EventHandler(this.ParserAsPreviewColor);
            // 
            // lblTextSelectingcolor
            // 
            this.lblTextSelectingcolor.AutoSize = true;
            this.lblTextSelectingcolor.Location = new System.Drawing.Point(34, 137);
            this.lblTextSelectingcolor.Name = "lblTextSelectingcolor";
            this.lblTextSelectingcolor.Size = new System.Drawing.Size(80, 13);
            this.lblTextSelectingcolor.TabIndex = 7;
            this.lblTextSelectingcolor.Text = "Selecting color:";
            // 
            // tbSelectingColor
            // 
            this.tbSelectingColor.Enabled = false;
            this.tbSelectingColor.Location = new System.Drawing.Point(141, 134);
            this.tbSelectingColor.MaxLength = 7;
            this.tbSelectingColor.Name = "tbSelectingColor";
            this.tbSelectingColor.Size = new System.Drawing.Size(59, 20);
            this.tbSelectingColor.TabIndex = 8;
            this.tbSelectingColor.Tag = "2";
            this.tbSelectingColor.WordWrap = false;
            this.tbSelectingColor.TextChanged += new System.EventHandler(this.ParserAsPreviewColor);
            // 
            // lblTextHighlightcolor
            // 
            this.lblTextHighlightcolor.AutoSize = true;
            this.lblTextHighlightcolor.Location = new System.Drawing.Point(34, 166);
            this.lblTextHighlightcolor.Name = "lblTextHighlightcolor";
            this.lblTextHighlightcolor.Size = new System.Drawing.Size(91, 13);
            this.lblTextHighlightcolor.TabIndex = 9;
            this.lblTextHighlightcolor.Text = "Highlighting color:";
            // 
            // tbHighlightingColor
            // 
            this.tbHighlightingColor.Enabled = false;
            this.tbHighlightingColor.Location = new System.Drawing.Point(141, 163);
            this.tbHighlightingColor.MaxLength = 7;
            this.tbHighlightingColor.Name = "tbHighlightingColor";
            this.tbHighlightingColor.Size = new System.Drawing.Size(59, 20);
            this.tbHighlightingColor.TabIndex = 10;
            this.tbHighlightingColor.Tag = "3";
            this.tbHighlightingColor.WordWrap = false;
            this.tbHighlightingColor.TextChanged += new System.EventHandler(this.ParserAsPreviewColor);
            // 
            // btnPickPrimaryColor
            // 
            this.btnPickPrimaryColor.Enabled = false;
            this.btnPickPrimaryColor.Location = new System.Drawing.Point(224, 104);
            this.btnPickPrimaryColor.Name = "btnPickPrimaryColor";
            this.btnPickPrimaryColor.Size = new System.Drawing.Size(35, 23);
            this.btnPickPrimaryColor.TabIndex = 11;
            this.btnPickPrimaryColor.Text = "...";
            this.btnPickPrimaryColor.UseCompatibleTextRendering = true;
            this.btnPickPrimaryColor.UseVisualStyleBackColor = true;
            this.btnPickPrimaryColor.Click += new System.EventHandler(this.btnPickPrimaryColor_Click);
            // 
            // btnPickSelectingColor
            // 
            this.btnPickSelectingColor.Enabled = false;
            this.btnPickSelectingColor.Location = new System.Drawing.Point(224, 131);
            this.btnPickSelectingColor.Name = "btnPickSelectingColor";
            this.btnPickSelectingColor.Size = new System.Drawing.Size(35, 23);
            this.btnPickSelectingColor.TabIndex = 12;
            this.btnPickSelectingColor.Text = "...";
            this.btnPickSelectingColor.UseCompatibleTextRendering = true;
            this.btnPickSelectingColor.UseVisualStyleBackColor = true;
            this.btnPickSelectingColor.Click += new System.EventHandler(this.btnSelectingColor_Click);
            // 
            // btnPickHighlightColor
            // 
            this.btnPickHighlightColor.Enabled = false;
            this.btnPickHighlightColor.Location = new System.Drawing.Point(224, 161);
            this.btnPickHighlightColor.Name = "btnPickHighlightColor";
            this.btnPickHighlightColor.Size = new System.Drawing.Size(35, 23);
            this.btnPickHighlightColor.TabIndex = 13;
            this.btnPickHighlightColor.Text = "...";
            this.btnPickHighlightColor.UseCompatibleTextRendering = true;
            this.btnPickHighlightColor.UseVisualStyleBackColor = true;
            this.btnPickHighlightColor.Click += new System.EventHandler(this.btnHighlightColor_Click);
            // 
            // lblTextTextcolor
            // 
            this.lblTextTextcolor.AutoSize = true;
            this.lblTextTextcolor.Location = new System.Drawing.Point(34, 194);
            this.lblTextTextcolor.Name = "lblTextTextcolor";
            this.lblTextTextcolor.Size = new System.Drawing.Size(57, 13);
            this.lblTextTextcolor.TabIndex = 14;
            this.lblTextTextcolor.Text = "Text color:";
            // 
            // tbTextColor
            // 
            this.tbTextColor.Enabled = false;
            this.tbTextColor.Location = new System.Drawing.Point(141, 191);
            this.tbTextColor.MaxLength = 7;
            this.tbTextColor.Name = "tbTextColor";
            this.tbTextColor.Size = new System.Drawing.Size(59, 20);
            this.tbTextColor.TabIndex = 15;
            this.tbTextColor.Tag = "4";
            this.tbTextColor.WordWrap = false;
            this.tbTextColor.TextChanged += new System.EventHandler(this.ParserAsPreviewColor);
            // 
            // btnPickTextColor
            // 
            this.btnPickTextColor.Enabled = false;
            this.btnPickTextColor.Location = new System.Drawing.Point(224, 189);
            this.btnPickTextColor.Name = "btnPickTextColor";
            this.btnPickTextColor.Size = new System.Drawing.Size(35, 23);
            this.btnPickTextColor.TabIndex = 16;
            this.btnPickTextColor.Text = "...";
            this.btnPickTextColor.UseCompatibleTextRendering = true;
            this.btnPickTextColor.UseVisualStyleBackColor = true;
            this.btnPickTextColor.Click += new System.EventHandler(this.btnTextColor_Click);
            // 
            // btnSaveSkin
            // 
            this.btnSaveSkin.BackColor = System.Drawing.Color.Silver;
            this.btnSaveSkin.Enabled = false;
            this.btnSaveSkin.Location = new System.Drawing.Point(28, 228);
            this.btnSaveSkin.Name = "btnSaveSkin";
            this.btnSaveSkin.Size = new System.Drawing.Size(107, 23);
            this.btnSaveSkin.TabIndex = 17;
            this.btnSaveSkin.Text = "save";
            this.btnSaveSkin.UseCompatibleTextRendering = true;
            this.btnSaveSkin.UseVisualStyleBackColor = false;
            this.btnSaveSkin.Click += new System.EventHandler(this.btnSaveSkin_Click);
            // 
            // btnNewSkin
            // 
            this.btnNewSkin.BackColor = System.Drawing.Color.Silver;
            this.btnNewSkin.Location = new System.Drawing.Point(152, 5);
            this.btnNewSkin.Name = "btnNewSkin";
            this.btnNewSkin.Size = new System.Drawing.Size(107, 23);
            this.btnNewSkin.TabIndex = 18;
            this.btnNewSkin.Text = "&new skin";
            this.btnNewSkin.UseCompatibleTextRendering = true;
            this.btnNewSkin.UseVisualStyleBackColor = false;
            this.btnNewSkin.Click += new System.EventHandler(this.btnNewSkin_Click);
            // 
            // btnEditskin
            // 
            this.btnEditskin.BackColor = System.Drawing.Color.Silver;
            this.btnEditskin.Enabled = false;
            this.btnEditskin.Location = new System.Drawing.Point(39, 5);
            this.btnEditskin.Name = "btnEditskin";
            this.btnEditskin.Size = new System.Drawing.Size(107, 23);
            this.btnEditskin.TabIndex = 19;
            this.btnEditskin.Text = "&edit skin";
            this.btnEditskin.UseCompatibleTextRendering = true;
            this.btnEditskin.UseVisualStyleBackColor = false;
            this.btnEditskin.Click += new System.EventHandler(this.btnEditskin_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Silver;
            this.btnClose.Location = new System.Drawing.Point(141, 228);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(107, 23);
            this.btnClose.TabIndex = 20;
            this.btnClose.Text = "close";
            this.btnClose.UseCompatibleTextRendering = true;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lbxSkins);
            this.splitContainer1.Panel1.Controls.Add(this.lblTextSkins);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnBrowsePrimaryTexture);
            this.splitContainer1.Panel2.Controls.Add(this.tbPrimaryTexture);
            this.splitContainer1.Panel2.Controls.Add(this.lblTextPrimaryTexture);
            this.splitContainer1.Panel2.Controls.Add(this.pnlClrText);
            this.splitContainer1.Panel2.Controls.Add(this.pnlClrHighlight);
            this.splitContainer1.Panel2.Controls.Add(this.pnlClrSelecting);
            this.splitContainer1.Panel2.Controls.Add(this.pnlClrPrimary);
            this.splitContainer1.Panel2.Controls.Add(this.btnClose);
            this.splitContainer1.Panel2.Controls.Add(this.lblTextSkinname);
            this.splitContainer1.Panel2.Controls.Add(this.btnEditskin);
            this.splitContainer1.Panel2.Controls.Add(this.tbSkinName);
            this.splitContainer1.Panel2.Controls.Add(this.btnNewSkin);
            this.splitContainer1.Panel2.Controls.Add(this.lblTextPrimarycolor);
            this.splitContainer1.Panel2.Controls.Add(this.btnSaveSkin);
            this.splitContainer1.Panel2.Controls.Add(this.tbPrimaryColor);
            this.splitContainer1.Panel2.Controls.Add(this.btnPickTextColor);
            this.splitContainer1.Panel2.Controls.Add(this.lblTextSelectingcolor);
            this.splitContainer1.Panel2.Controls.Add(this.tbTextColor);
            this.splitContainer1.Panel2.Controls.Add(this.tbSelectingColor);
            this.splitContainer1.Panel2.Controls.Add(this.lblTextTextcolor);
            this.splitContainer1.Panel2.Controls.Add(this.lblTextHighlightcolor);
            this.splitContainer1.Panel2.Controls.Add(this.btnPickHighlightColor);
            this.splitContainer1.Panel2.Controls.Add(this.tbHighlightingColor);
            this.splitContainer1.Panel2.Controls.Add(this.btnPickSelectingColor);
            this.splitContainer1.Panel2.Controls.Add(this.btnPickPrimaryColor);
            this.splitContainer1.Size = new System.Drawing.Size(414, 254);
            this.splitContainer1.SplitterDistance = 124;
            this.splitContainer1.TabIndex = 21;
            // 
            // btnBrowsePrimaryTexture
            // 
            this.btnBrowsePrimaryTexture.Enabled = false;
            this.btnBrowsePrimaryTexture.Location = new System.Drawing.Point(224, 76);
            this.btnBrowsePrimaryTexture.Name = "btnBrowsePrimaryTexture";
            this.btnBrowsePrimaryTexture.Size = new System.Drawing.Size(35, 23);
            this.btnBrowsePrimaryTexture.TabIndex = 27;
            this.btnBrowsePrimaryTexture.Text = "...";
            this.btnBrowsePrimaryTexture.UseCompatibleTextRendering = true;
            this.btnBrowsePrimaryTexture.UseVisualStyleBackColor = true;
            this.btnBrowsePrimaryTexture.Click += new System.EventHandler(this.btnBrowsePrimaryTexture_Click);
            // 
            // tbPrimaryTexture
            // 
            this.tbPrimaryTexture.Enabled = false;
            this.tbPrimaryTexture.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPrimaryTexture.Location = new System.Drawing.Point(141, 77);
            this.tbPrimaryTexture.MaxLength = 255;
            this.tbPrimaryTexture.Name = "tbPrimaryTexture";
            this.tbPrimaryTexture.Size = new System.Drawing.Size(82, 22);
            this.tbPrimaryTexture.TabIndex = 26;
            // 
            // lblTextPrimaryTexture
            // 
            this.lblTextPrimaryTexture.AutoSize = true;
            this.lblTextPrimaryTexture.Location = new System.Drawing.Point(34, 82);
            this.lblTextPrimaryTexture.Name = "lblTextPrimaryTexture";
            this.lblTextPrimaryTexture.Size = new System.Drawing.Size(79, 13);
            this.lblTextPrimaryTexture.TabIndex = 25;
            this.lblTextPrimaryTexture.Text = "Primary texture:";
            // 
            // pnlClrText
            // 
            this.pnlClrText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClrText.ForeColor = System.Drawing.Color.Black;
            this.pnlClrText.Location = new System.Drawing.Point(202, 191);
            this.pnlClrText.Name = "pnlClrText";
            this.pnlClrText.Size = new System.Drawing.Size(21, 21);
            this.pnlClrText.TabIndex = 24;
            // 
            // pnlClrHighlight
            // 
            this.pnlClrHighlight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClrHighlight.ForeColor = System.Drawing.Color.Black;
            this.pnlClrHighlight.Location = new System.Drawing.Point(202, 163);
            this.pnlClrHighlight.Name = "pnlClrHighlight";
            this.pnlClrHighlight.Size = new System.Drawing.Size(21, 21);
            this.pnlClrHighlight.TabIndex = 23;
            // 
            // pnlClrSelecting
            // 
            this.pnlClrSelecting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClrSelecting.ForeColor = System.Drawing.Color.Black;
            this.pnlClrSelecting.Location = new System.Drawing.Point(202, 134);
            this.pnlClrSelecting.Name = "pnlClrSelecting";
            this.pnlClrSelecting.Size = new System.Drawing.Size(21, 21);
            this.pnlClrSelecting.TabIndex = 22;
            // 
            // pnlClrPrimary
            // 
            this.pnlClrPrimary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClrPrimary.ForeColor = System.Drawing.Color.Black;
            this.pnlClrPrimary.Location = new System.Drawing.Point(202, 106);
            this.pnlClrPrimary.Name = "pnlClrPrimary";
            this.pnlClrPrimary.Size = new System.Drawing.Size(21, 21);
            this.pnlClrPrimary.TabIndex = 21;
            // 
            // openFileTextureDialog
            // 
            this.openFileTextureDialog.Filter = "Image files|*.png;*.tif;*.tiff;*.bmp;*.gif;*.jpg;*.jpeg";
            // 
            // FrmSkinEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(414, 254);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FrmSkinEditor";
            this.Text = "Skin editor";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileTextureDialog;
    }
}