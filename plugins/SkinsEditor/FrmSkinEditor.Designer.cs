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
        /// 
        /// </summary>
        private System.Windows.Forms.OpenFileDialog openFileTextureDialog;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.ListBox lbxSkins;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Button btnBrowsePrimaryTexture;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Label lblTextSkins;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.TextBox tbPrimaryTexture;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Label lblTextPrimaryTexture;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Panel pnlClrText;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Panel pnlClrHighlight;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Panel pnlClrSelecting;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Panel pnlClrPrimary;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Label lblTextSkinname;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Button btnEditskin;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.TextBox tbSkinName;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Button btnNewSkin;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Label lblTextPrimarycolor;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Button btnSaveSkin;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.TextBox tbPrimaryColor;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Button btnPickTextColor;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Label lblTextSelectingcolor;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.TextBox tbTextColor;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.TextBox tbSelectingColor;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Label lblTextTextcolor;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Label lblTextHighlightcolor;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Button btnPickHighlightColor;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.TextBox tbHighlightingColor;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Button btnPickSelectingColor;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Button btnPickPrimaryColor;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Button btnDeleteSkin;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Label lblTextPrimartTextureLayout;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.ComboBox cbxPrimaryTextureLayout;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSkinEditor));
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
            this.pnlHead = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pbResizeGrip = new System.Windows.Forms.PictureBox();
            this.pnlHead.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResizeGrip)).BeginInit();
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
            this.lbxSkins.Location = new System.Drawing.Point(12, 67);
            this.lbxSkins.Name = "lbxSkins";
            this.lbxSkins.Size = new System.Drawing.Size(118, 212);
            this.lbxSkins.TabIndex = 28;
            this.lbxSkins.SelectedIndexChanged += new System.EventHandler(this.lbxSkins_SelectedIndexChanged);
            // 
            // btnBrowsePrimaryTexture
            // 
            this.btnBrowsePrimaryTexture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowsePrimaryTexture.Enabled = false;
            this.btnBrowsePrimaryTexture.Location = new System.Drawing.Point(498, 97);
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
            this.lblTextSkins.Location = new System.Drawing.Point(12, 47);
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
            this.tbPrimaryTexture.Location = new System.Drawing.Point(259, 97);
            this.tbPrimaryTexture.MaxLength = 259;
            this.tbPrimaryTexture.Name = "tbPrimaryTexture";
            this.tbPrimaryTexture.Size = new System.Drawing.Size(233, 22);
            this.tbPrimaryTexture.TabIndex = 53;
            // 
            // lblTextPrimaryTexture
            // 
            this.lblTextPrimaryTexture.AutoSize = true;
            this.lblTextPrimaryTexture.Location = new System.Drawing.Point(152, 102);
            this.lblTextPrimaryTexture.Name = "lblTextPrimaryTexture";
            this.lblTextPrimaryTexture.Size = new System.Drawing.Size(79, 13);
            this.lblTextPrimaryTexture.TabIndex = 52;
            this.lblTextPrimaryTexture.Text = "Primary texture:";
            // 
            // pnlClrText
            // 
            this.pnlClrText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClrText.ForeColor = System.Drawing.Color.Black;
            this.pnlClrText.Location = new System.Drawing.Point(320, 241);
            this.pnlClrText.Name = "pnlClrText";
            this.pnlClrText.Size = new System.Drawing.Size(21, 21);
            this.pnlClrText.TabIndex = 51;
            // 
            // pnlClrHighlight
            // 
            this.pnlClrHighlight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClrHighlight.ForeColor = System.Drawing.Color.Black;
            this.pnlClrHighlight.Location = new System.Drawing.Point(320, 213);
            this.pnlClrHighlight.Name = "pnlClrHighlight";
            this.pnlClrHighlight.Size = new System.Drawing.Size(21, 21);
            this.pnlClrHighlight.TabIndex = 50;
            // 
            // pnlClrSelecting
            // 
            this.pnlClrSelecting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClrSelecting.ForeColor = System.Drawing.Color.Black;
            this.pnlClrSelecting.Location = new System.Drawing.Point(320, 184);
            this.pnlClrSelecting.Name = "pnlClrSelecting";
            this.pnlClrSelecting.Size = new System.Drawing.Size(21, 21);
            this.pnlClrSelecting.TabIndex = 49;
            // 
            // pnlClrPrimary
            // 
            this.pnlClrPrimary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClrPrimary.ForeColor = System.Drawing.Color.Black;
            this.pnlClrPrimary.Location = new System.Drawing.Point(320, 156);
            this.pnlClrPrimary.Name = "pnlClrPrimary";
            this.pnlClrPrimary.Size = new System.Drawing.Size(21, 21);
            this.pnlClrPrimary.TabIndex = 48;
            // 
            // lblTextSkinname
            // 
            this.lblTextSkinname.AutoSize = true;
            this.lblTextSkinname.Location = new System.Drawing.Point(152, 70);
            this.lblTextSkinname.Name = "lblTextSkinname";
            this.lblTextSkinname.Size = new System.Drawing.Size(60, 13);
            this.lblTextSkinname.TabIndex = 30;
            this.lblTextSkinname.Text = "Skin name:";
            // 
            // btnEditskin
            // 
            this.btnEditskin.BackColor = System.Drawing.Color.Silver;
            this.btnEditskin.Enabled = false;
            this.btnEditskin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditskin.Location = new System.Drawing.Point(311, 3);
            this.btnEditskin.Name = "btnEditskin";
            this.btnEditskin.Size = new System.Drawing.Size(153, 23);
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
            this.tbSkinName.Location = new System.Drawing.Point(259, 67);
            this.tbSkinName.MaxLength = 200;
            this.tbSkinName.Name = "tbSkinName";
            this.tbSkinName.Size = new System.Drawing.Size(163, 22);
            this.tbSkinName.TabIndex = 31;
            // 
            // btnNewSkin
            // 
            this.btnNewSkin.BackColor = System.Drawing.Color.Silver;
            this.btnNewSkin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewSkin.Location = new System.Drawing.Point(166, 3);
            this.btnNewSkin.Name = "btnNewSkin";
            this.btnNewSkin.Size = new System.Drawing.Size(139, 23);
            this.btnNewSkin.TabIndex = 45;
            this.btnNewSkin.Text = "&new skin";
            this.btnNewSkin.UseCompatibleTextRendering = true;
            this.btnNewSkin.UseVisualStyleBackColor = false;
            this.btnNewSkin.Click += new System.EventHandler(this.btnNewSkin_Click);
            // 
            // lblTextPrimarycolor
            // 
            this.lblTextPrimarycolor.AutoSize = true;
            this.lblTextPrimarycolor.Location = new System.Drawing.Point(152, 159);
            this.lblTextPrimarycolor.Name = "lblTextPrimarycolor";
            this.lblTextPrimarycolor.Size = new System.Drawing.Size(70, 13);
            this.lblTextPrimarycolor.TabIndex = 32;
            this.lblTextPrimarycolor.Text = "Primary color:";
            // 
            // btnSaveSkin
            // 
            this.btnSaveSkin.BackColor = System.Drawing.Color.Silver;
            this.btnSaveSkin.Enabled = false;
            this.btnSaveSkin.Location = new System.Drawing.Point(259, 267);
            this.btnSaveSkin.Name = "btnSaveSkin";
            this.btnSaveSkin.Size = new System.Drawing.Size(118, 23);
            this.btnSaveSkin.TabIndex = 44;
            this.btnSaveSkin.Text = "save";
            this.btnSaveSkin.UseCompatibleTextRendering = true;
            this.btnSaveSkin.UseVisualStyleBackColor = false;
            this.btnSaveSkin.Click += new System.EventHandler(this.btnSaveSkin_Click);
            // 
            // tbPrimaryColor
            // 
            this.tbPrimaryColor.Enabled = false;
            this.tbPrimaryColor.Location = new System.Drawing.Point(259, 156);
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
            this.btnPickTextColor.Location = new System.Drawing.Point(342, 239);
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
            this.lblTextSelectingcolor.Location = new System.Drawing.Point(152, 187);
            this.lblTextSelectingcolor.Name = "lblTextSelectingcolor";
            this.lblTextSelectingcolor.Size = new System.Drawing.Size(80, 13);
            this.lblTextSelectingcolor.TabIndex = 34;
            this.lblTextSelectingcolor.Text = "Selecting color:";
            // 
            // tbTextColor
            // 
            this.tbTextColor.Enabled = false;
            this.tbTextColor.Location = new System.Drawing.Point(259, 241);
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
            this.tbSelectingColor.Location = new System.Drawing.Point(259, 184);
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
            this.lblTextTextcolor.Location = new System.Drawing.Point(152, 244);
            this.lblTextTextcolor.Name = "lblTextTextcolor";
            this.lblTextTextcolor.Size = new System.Drawing.Size(57, 13);
            this.lblTextTextcolor.TabIndex = 41;
            this.lblTextTextcolor.Text = "Text color:";
            // 
            // lblTextHighlightcolor
            // 
            this.lblTextHighlightcolor.AutoSize = true;
            this.lblTextHighlightcolor.Location = new System.Drawing.Point(152, 216);
            this.lblTextHighlightcolor.Name = "lblTextHighlightcolor";
            this.lblTextHighlightcolor.Size = new System.Drawing.Size(91, 13);
            this.lblTextHighlightcolor.TabIndex = 36;
            this.lblTextHighlightcolor.Text = "Highlighting color:";
            // 
            // btnPickHighlightColor
            // 
            this.btnPickHighlightColor.Enabled = false;
            this.btnPickHighlightColor.Location = new System.Drawing.Point(342, 211);
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
            this.tbHighlightingColor.Location = new System.Drawing.Point(259, 213);
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
            this.btnPickSelectingColor.Location = new System.Drawing.Point(342, 181);
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
            this.btnPickPrimaryColor.Location = new System.Drawing.Point(342, 154);
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
            this.btnDeleteSkin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteSkin.ForeColor = System.Drawing.Color.DarkRed;
            this.btnDeleteSkin.Location = new System.Drawing.Point(3, 3);
            this.btnDeleteSkin.Name = "btnDeleteSkin";
            this.btnDeleteSkin.Size = new System.Drawing.Size(157, 23);
            this.btnDeleteSkin.TabIndex = 55;
            this.btnDeleteSkin.Text = "&delete skin";
            this.btnDeleteSkin.UseCompatibleTextRendering = true;
            this.btnDeleteSkin.UseVisualStyleBackColor = false;
            this.btnDeleteSkin.Click += new System.EventHandler(this.btnDeleteSkin_Click);
            // 
            // lblTextPrimartTextureLayout
            // 
            this.lblTextPrimartTextureLayout.AutoSize = true;
            this.lblTextPrimartTextureLayout.Location = new System.Drawing.Point(152, 128);
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
            this.cbxPrimaryTextureLayout.Location = new System.Drawing.Point(259, 125);
            this.cbxPrimaryTextureLayout.MaxDropDownItems = 3;
            this.cbxPrimaryTextureLayout.Name = "cbxPrimaryTextureLayout";
            this.cbxPrimaryTextureLayout.Size = new System.Drawing.Size(118, 21);
            this.cbxPrimaryTextureLayout.TabIndex = 57;
            // 
            // pnlHead
            // 
            this.pnlHead.BackColor = System.Drawing.Color.DarkGray;
            this.pnlHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHead.Controls.Add(this.btnClose);
            this.pnlHead.Controls.Add(this.lblTitle);
            this.pnlHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHead.Location = new System.Drawing.Point(0, 0);
            this.pnlHead.Name = "pnlHead";
            this.pnlHead.Size = new System.Drawing.Size(600, 30);
            this.pnlHead.TabIndex = 58;
            this.pnlHead.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseMove);
            this.pnlHead.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseDown);
            this.pnlHead.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseUp);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(563, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(32, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click_1);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(3, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(86, 20);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Skin Editor";
            // 
            // pnlContent
            // 
            this.pnlContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContent.Controls.Add(this.pbResizeGrip);
            this.pnlContent.Controls.Add(this.lblTextSkins);
            this.pnlContent.Controls.Add(this.btnNewSkin);
            this.pnlContent.Controls.Add(this.cbxPrimaryTextureLayout);
            this.pnlContent.Controls.Add(this.btnDeleteSkin);
            this.pnlContent.Controls.Add(this.lblTextPrimartTextureLayout);
            this.pnlContent.Controls.Add(this.btnEditskin);
            this.pnlContent.Controls.Add(this.lbxSkins);
            this.pnlContent.Controls.Add(this.btnBrowsePrimaryTexture);
            this.pnlContent.Controls.Add(this.btnPickPrimaryColor);
            this.pnlContent.Controls.Add(this.tbPrimaryTexture);
            this.pnlContent.Controls.Add(this.btnPickSelectingColor);
            this.pnlContent.Controls.Add(this.lblTextPrimaryTexture);
            this.pnlContent.Controls.Add(this.tbHighlightingColor);
            this.pnlContent.Controls.Add(this.pnlClrText);
            this.pnlContent.Controls.Add(this.btnPickHighlightColor);
            this.pnlContent.Controls.Add(this.pnlClrHighlight);
            this.pnlContent.Controls.Add(this.lblTextHighlightcolor);
            this.pnlContent.Controls.Add(this.pnlClrSelecting);
            this.pnlContent.Controls.Add(this.lblTextTextcolor);
            this.pnlContent.Controls.Add(this.pnlClrPrimary);
            this.pnlContent.Controls.Add(this.tbSelectingColor);
            this.pnlContent.Controls.Add(this.tbTextColor);
            this.pnlContent.Controls.Add(this.lblTextSkinname);
            this.pnlContent.Controls.Add(this.lblTextSelectingcolor);
            this.pnlContent.Controls.Add(this.tbSkinName);
            this.pnlContent.Controls.Add(this.btnPickTextColor);
            this.pnlContent.Controls.Add(this.lblTextPrimarycolor);
            this.pnlContent.Controls.Add(this.tbPrimaryColor);
            this.pnlContent.Controls.Add(this.btnSaveSkin);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 30);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(600, 349);
            this.pnlContent.TabIndex = 59;
            // 
            // pbResizeGrip
            // 
            this.pbResizeGrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbResizeGrip.BackColor = System.Drawing.Color.Transparent;
            this.pbResizeGrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbResizeGrip.Image = global::SkinsEditor.Properties.Resources.hoekje;
            this.pbResizeGrip.Location = new System.Drawing.Point(582, 329);
            this.pbResizeGrip.Margin = new System.Windows.Forms.Padding(0);
            this.pbResizeGrip.Name = "pbResizeGrip";
            this.pbResizeGrip.Size = new System.Drawing.Size(17, 18);
            this.pbResizeGrip.TabIndex = 58;
            this.pbResizeGrip.TabStop = false;
            this.pbResizeGrip.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbResizeGrip_MouseMove);
            this.pbResizeGrip.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbResizeGrip_MouseDown);
            // 
            // FrmSkinEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(600, 379);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHead);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmSkinEditor";
            this.Text = "Skin editor - NoteFly";
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResizeGrip)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pbResizeGrip;
    }
}