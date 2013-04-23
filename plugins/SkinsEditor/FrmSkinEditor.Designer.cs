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
        private System.Windows.Forms.TextBox tbHighlightingColor;

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
            this.lblTextSelectingcolor = new System.Windows.Forms.Label();
            this.tbTextColor = new System.Windows.Forms.TextBox();
            this.tbSelectingColor = new System.Windows.Forms.TextBox();
            this.lblTextTextcolor = new System.Windows.Forms.Label();
            this.lblTextHighlightcolor = new System.Windows.Forms.Label();
            this.tbHighlightingColor = new System.Windows.Forms.TextBox();
            this.btnDeleteSkin = new System.Windows.Forms.Button();
            this.lblTextPrimartTextureLayout = new System.Windows.Forms.Label();
            this.cbxPrimaryTextureLayout = new System.Windows.Forms.ComboBox();
            this.pnlHead = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pbResizeGrip = new System.Windows.Forms.PictureBox();
            this.splitContainerContent = new System.Windows.Forms.SplitContainer();
            this.chxUseTexture = new System.Windows.Forms.CheckBox();
            this.notePreview1 = new NoteSkinPreview();
            this.pnlHead.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResizeGrip)).BeginInit();
            this.splitContainerContent.Panel1.SuspendLayout();
            this.splitContainerContent.Panel2.SuspendLayout();
            this.splitContainerContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileTextureDialog
            // 
            this.openFileTextureDialog.Filter = "Image files|*.png;*.tif;*.tiff;*.bmp;*.gif;*.jpg;*.jpeg";
            // 
            // lbxSkins
            // 
            this.lbxSkins.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxSkins.FormattingEnabled = true;
            this.lbxSkins.Location = new System.Drawing.Point(0, 21);
            this.lbxSkins.Name = "lbxSkins";
            this.lbxSkins.Size = new System.Drawing.Size(120, 290);
            this.lbxSkins.TabIndex = 28;
            this.lbxSkins.SelectedIndexChanged += new System.EventHandler(this.lbxSkins_SelectedIndexChanged);
            // 
            // btnBrowsePrimaryTexture
            // 
            this.btnBrowsePrimaryTexture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowsePrimaryTexture.Enabled = false;
            this.btnBrowsePrimaryTexture.Location = new System.Drawing.Point(389, 55);
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
            this.lblTextSkins.Location = new System.Drawing.Point(0, 6);
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
            this.tbPrimaryTexture.Location = new System.Drawing.Point(128, 56);
            this.tbPrimaryTexture.MaxLength = 259;
            this.tbPrimaryTexture.Name = "tbPrimaryTexture";
            this.tbPrimaryTexture.Size = new System.Drawing.Size(255, 22);
            this.tbPrimaryTexture.TabIndex = 53;
            this.tbPrimaryTexture.TextChanged += new System.EventHandler(this.tbPrimaryTexture_TextChanged);
            // 
            // lblTextPrimaryTexture
            // 
            this.lblTextPrimaryTexture.AutoSize = true;
            this.lblTextPrimaryTexture.Location = new System.Drawing.Point(21, 60);
            this.lblTextPrimaryTexture.Name = "lblTextPrimaryTexture";
            this.lblTextPrimaryTexture.Size = new System.Drawing.Size(79, 13);
            this.lblTextPrimaryTexture.TabIndex = 52;
            this.lblTextPrimaryTexture.Text = "Primary texture:";
            // 
            // pnlClrText
            // 
            this.pnlClrText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClrText.CausesValidation = false;
            this.pnlClrText.Enabled = false;
            this.pnlClrText.ForeColor = System.Drawing.Color.Black;
            this.pnlClrText.Location = new System.Drawing.Point(189, 199);
            this.pnlClrText.Name = "pnlClrText";
            this.pnlClrText.Size = new System.Drawing.Size(21, 21);
            this.pnlClrText.TabIndex = 51;
            this.pnlClrText.Click += new System.EventHandler(this.pnlClrText_Click);
            this.pnlClrText.MouseLeave += new System.EventHandler(this.BackNormalCusors);
            this.pnlClrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.HandEnabled);
            // 
            // pnlClrHighlight
            // 
            this.pnlClrHighlight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClrHighlight.CausesValidation = false;
            this.pnlClrHighlight.Enabled = false;
            this.pnlClrHighlight.ForeColor = System.Drawing.Color.Black;
            this.pnlClrHighlight.Location = new System.Drawing.Point(189, 171);
            this.pnlClrHighlight.Name = "pnlClrHighlight";
            this.pnlClrHighlight.Size = new System.Drawing.Size(21, 21);
            this.pnlClrHighlight.TabIndex = 50;
            this.pnlClrHighlight.Click += new System.EventHandler(this.pnlClrHighlight_Click);
            this.pnlClrHighlight.MouseLeave += new System.EventHandler(this.BackNormalCusors);
            this.pnlClrHighlight.MouseMove += new System.Windows.Forms.MouseEventHandler(this.HandEnabled);
            // 
            // pnlClrSelecting
            // 
            this.pnlClrSelecting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClrSelecting.CausesValidation = false;
            this.pnlClrSelecting.Enabled = false;
            this.pnlClrSelecting.ForeColor = System.Drawing.Color.Black;
            this.pnlClrSelecting.Location = new System.Drawing.Point(189, 142);
            this.pnlClrSelecting.Name = "pnlClrSelecting";
            this.pnlClrSelecting.Size = new System.Drawing.Size(21, 21);
            this.pnlClrSelecting.TabIndex = 49;
            this.pnlClrSelecting.Click += new System.EventHandler(this.pnlClrSelecting_Click);
            this.pnlClrSelecting.MouseLeave += new System.EventHandler(this.BackNormalCusors);
            this.pnlClrSelecting.MouseMove += new System.Windows.Forms.MouseEventHandler(this.HandEnabled);
            // 
            // pnlClrPrimary
            // 
            this.pnlClrPrimary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClrPrimary.CausesValidation = false;
            this.pnlClrPrimary.Enabled = false;
            this.pnlClrPrimary.ForeColor = System.Drawing.Color.Black;
            this.pnlClrPrimary.Location = new System.Drawing.Point(189, 114);
            this.pnlClrPrimary.Name = "pnlClrPrimary";
            this.pnlClrPrimary.Size = new System.Drawing.Size(21, 21);
            this.pnlClrPrimary.TabIndex = 48;
            this.pnlClrPrimary.Click += new System.EventHandler(this.pnlClrPrimary_Click);
            this.pnlClrPrimary.MouseLeave += new System.EventHandler(this.BackNormalCusors);
            this.pnlClrPrimary.MouseMove += new System.Windows.Forms.MouseEventHandler(this.HandEnabled);
            // 
            // lblTextSkinname
            // 
            this.lblTextSkinname.AutoSize = true;
            this.lblTextSkinname.Location = new System.Drawing.Point(21, 11);
            this.lblTextSkinname.Name = "lblTextSkinname";
            this.lblTextSkinname.Size = new System.Drawing.Size(60, 13);
            this.lblTextSkinname.TabIndex = 30;
            this.lblTextSkinname.Text = "Skin name:";
            // 
            // btnEditskin
            // 
            this.btnEditskin.BackColor = System.Drawing.Color.Silver;
            this.btnEditskin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEditskin.Enabled = false;
            this.btnEditskin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditskin.Location = new System.Drawing.Point(401, 3);
            this.btnEditskin.Name = "btnEditskin";
            this.btnEditskin.Size = new System.Drawing.Size(194, 21);
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
            this.tbSkinName.Location = new System.Drawing.Point(128, 6);
            this.tbSkinName.MaxLength = 200;
            this.tbSkinName.Name = "tbSkinName";
            this.tbSkinName.Size = new System.Drawing.Size(145, 22);
            this.tbSkinName.TabIndex = 31;
            this.tbSkinName.TextChanged += new System.EventHandler(this.tbSkinName_TextChanged);
            // 
            // btnNewSkin
            // 
            this.btnNewSkin.BackColor = System.Drawing.Color.Silver;
            this.btnNewSkin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNewSkin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewSkin.Location = new System.Drawing.Point(202, 3);
            this.btnNewSkin.Name = "btnNewSkin";
            this.btnNewSkin.Size = new System.Drawing.Size(193, 21);
            this.btnNewSkin.TabIndex = 45;
            this.btnNewSkin.Text = "&new skin";
            this.btnNewSkin.UseCompatibleTextRendering = true;
            this.btnNewSkin.UseVisualStyleBackColor = false;
            this.btnNewSkin.Click += new System.EventHandler(this.btnNewSkin_Click);
            // 
            // lblTextPrimarycolor
            // 
            this.lblTextPrimarycolor.AutoSize = true;
            this.lblTextPrimarycolor.Location = new System.Drawing.Point(21, 117);
            this.lblTextPrimarycolor.Name = "lblTextPrimarycolor";
            this.lblTextPrimarycolor.Size = new System.Drawing.Size(70, 13);
            this.lblTextPrimarycolor.TabIndex = 32;
            this.lblTextPrimarycolor.Text = "Primary color:";
            // 
            // btnSaveSkin
            // 
            this.btnSaveSkin.BackColor = System.Drawing.Color.Silver;
            this.btnSaveSkin.Enabled = false;
            this.btnSaveSkin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveSkin.Location = new System.Drawing.Point(128, 233);
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
            this.tbPrimaryColor.Location = new System.Drawing.Point(128, 114);
            this.tbPrimaryColor.MaxLength = 7;
            this.tbPrimaryColor.Name = "tbPrimaryColor";
            this.tbPrimaryColor.Size = new System.Drawing.Size(59, 20);
            this.tbPrimaryColor.TabIndex = 33;
            this.tbPrimaryColor.Tag = "1";
            this.tbPrimaryColor.WordWrap = false;
            this.tbPrimaryColor.TextChanged += new System.EventHandler(this.ParserAsPreviewColor);
            // 
            // lblTextSelectingcolor
            // 
            this.lblTextSelectingcolor.AutoSize = true;
            this.lblTextSelectingcolor.Location = new System.Drawing.Point(21, 145);
            this.lblTextSelectingcolor.Name = "lblTextSelectingcolor";
            this.lblTextSelectingcolor.Size = new System.Drawing.Size(80, 13);
            this.lblTextSelectingcolor.TabIndex = 34;
            this.lblTextSelectingcolor.Text = "Selecting color:";
            // 
            // tbTextColor
            // 
            this.tbTextColor.Enabled = false;
            this.tbTextColor.Location = new System.Drawing.Point(128, 199);
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
            this.tbSelectingColor.Location = new System.Drawing.Point(128, 142);
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
            this.lblTextTextcolor.Location = new System.Drawing.Point(21, 202);
            this.lblTextTextcolor.Name = "lblTextTextcolor";
            this.lblTextTextcolor.Size = new System.Drawing.Size(57, 13);
            this.lblTextTextcolor.TabIndex = 41;
            this.lblTextTextcolor.Text = "Text color:";
            // 
            // lblTextHighlightcolor
            // 
            this.lblTextHighlightcolor.AutoSize = true;
            this.lblTextHighlightcolor.Location = new System.Drawing.Point(21, 174);
            this.lblTextHighlightcolor.Name = "lblTextHighlightcolor";
            this.lblTextHighlightcolor.Size = new System.Drawing.Size(91, 13);
            this.lblTextHighlightcolor.TabIndex = 36;
            this.lblTextHighlightcolor.Text = "Highlighting color:";
            // 
            // tbHighlightingColor
            // 
            this.tbHighlightingColor.Enabled = false;
            this.tbHighlightingColor.Location = new System.Drawing.Point(128, 171);
            this.tbHighlightingColor.MaxLength = 7;
            this.tbHighlightingColor.Name = "tbHighlightingColor";
            this.tbHighlightingColor.Size = new System.Drawing.Size(59, 20);
            this.tbHighlightingColor.TabIndex = 37;
            this.tbHighlightingColor.Tag = "3";
            this.tbHighlightingColor.WordWrap = false;
            this.tbHighlightingColor.TextChanged += new System.EventHandler(this.ParserAsPreviewColor);
            // 
            // btnDeleteSkin
            // 
            this.btnDeleteSkin.BackColor = System.Drawing.Color.Silver;
            this.btnDeleteSkin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDeleteSkin.Enabled = false;
            this.btnDeleteSkin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteSkin.ForeColor = System.Drawing.Color.DarkRed;
            this.btnDeleteSkin.Location = new System.Drawing.Point(3, 3);
            this.btnDeleteSkin.Name = "btnDeleteSkin";
            this.btnDeleteSkin.Size = new System.Drawing.Size(193, 21);
            this.btnDeleteSkin.TabIndex = 55;
            this.btnDeleteSkin.Text = "&delete skin";
            this.btnDeleteSkin.UseCompatibleTextRendering = true;
            this.btnDeleteSkin.UseVisualStyleBackColor = false;
            this.btnDeleteSkin.Click += new System.EventHandler(this.btnDeleteSkin_Click);
            // 
            // lblTextPrimartTextureLayout
            // 
            this.lblTextPrimartTextureLayout.AutoSize = true;
            this.lblTextPrimartTextureLayout.Location = new System.Drawing.Point(21, 91);
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
            this.cbxPrimaryTextureLayout.Location = new System.Drawing.Point(128, 88);
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
            this.pnlHead.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseDown);
            this.pnlHead.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlHead_MouseMove);
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
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
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
            this.pnlContent.Controls.Add(this.tableLayoutPanel1);
            this.pnlContent.Controls.Add(this.pbResizeGrip);
            this.pnlContent.Controls.Add(this.splitContainerContent);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 30);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(600, 349);
            this.pnlContent.TabIndex = 59;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.4F));
            this.tableLayoutPanel1.Controls.Add(this.btnDeleteSkin, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnNewSkin, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnEditskin, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(598, 27);
            this.tableLayoutPanel1.TabIndex = 60;
            // 
            // pbResizeGrip
            // 
            this.pbResizeGrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbResizeGrip.BackColor = System.Drawing.Color.Transparent;
            this.pbResizeGrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbResizeGrip.Image = global::SkinsEditor.Properties.Resources.hoekje;
            this.pbResizeGrip.Location = new System.Drawing.Point(582, 332);
            this.pbResizeGrip.Margin = new System.Windows.Forms.Padding(0);
            this.pbResizeGrip.Name = "pbResizeGrip";
            this.pbResizeGrip.Size = new System.Drawing.Size(17, 18);
            this.pbResizeGrip.TabIndex = 58;
            this.pbResizeGrip.TabStop = false;
            this.pbResizeGrip.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbResizeGrip_MouseMove);
            // 
            // splitContainerContent
            // 
            this.splitContainerContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerContent.Location = new System.Drawing.Point(7, 32);
            this.splitContainerContent.Name = "splitContainerContent";
            // 
            // splitContainerContent.Panel1
            // 
            this.splitContainerContent.Panel1.Controls.Add(this.lbxSkins);
            this.splitContainerContent.Panel1.Controls.Add(this.lblTextSkins);
            this.splitContainerContent.Panel1.Margin = new System.Windows.Forms.Padding(5, 0, 0, 5);
            // 
            // splitContainerContent.Panel2
            // 
            this.splitContainerContent.Panel2.Controls.Add(this.chxUseTexture);
            this.splitContainerContent.Panel2.Controls.Add(this.notePreview1);
            this.splitContainerContent.Panel2.Controls.Add(this.lblTextSkinname);
            this.splitContainerContent.Panel2.Controls.Add(this.btnSaveSkin);
            this.splitContainerContent.Panel2.Controls.Add(this.tbPrimaryColor);
            this.splitContainerContent.Panel2.Controls.Add(this.cbxPrimaryTextureLayout);
            this.splitContainerContent.Panel2.Controls.Add(this.lblTextPrimarycolor);
            this.splitContainerContent.Panel2.Controls.Add(this.lblTextPrimartTextureLayout);
            this.splitContainerContent.Panel2.Controls.Add(this.tbSkinName);
            this.splitContainerContent.Panel2.Controls.Add(this.lblTextSelectingcolor);
            this.splitContainerContent.Panel2.Controls.Add(this.btnBrowsePrimaryTexture);
            this.splitContainerContent.Panel2.Controls.Add(this.tbTextColor);
            this.splitContainerContent.Panel2.Controls.Add(this.tbSelectingColor);
            this.splitContainerContent.Panel2.Controls.Add(this.tbPrimaryTexture);
            this.splitContainerContent.Panel2.Controls.Add(this.pnlClrPrimary);
            this.splitContainerContent.Panel2.Controls.Add(this.lblTextTextcolor);
            this.splitContainerContent.Panel2.Controls.Add(this.lblTextPrimaryTexture);
            this.splitContainerContent.Panel2.Controls.Add(this.pnlClrSelecting);
            this.splitContainerContent.Panel2.Controls.Add(this.tbHighlightingColor);
            this.splitContainerContent.Panel2.Controls.Add(this.lblTextHighlightcolor);
            this.splitContainerContent.Panel2.Controls.Add(this.pnlClrText);
            this.splitContainerContent.Panel2.Controls.Add(this.pnlClrHighlight);
            this.splitContainerContent.Size = new System.Drawing.Size(588, 312);
            this.splitContainerContent.SplitterDistance = 117;
            this.splitContainerContent.TabIndex = 59;
            // 
            // chxUseTexture
            // 
            this.chxUseTexture.AutoSize = true;
            this.chxUseTexture.Enabled = false;
            this.chxUseTexture.Location = new System.Drawing.Point(128, 34);
            this.chxUseTexture.Name = "chxUseTexture";
            this.chxUseTexture.Size = new System.Drawing.Size(80, 17);
            this.chxUseTexture.TabIndex = 59;
            this.chxUseTexture.Text = "Use texture";
            this.chxUseTexture.UseVisualStyleBackColor = true;
            this.chxUseTexture.CheckedChanged += new System.EventHandler(this.chxUseTexture_CheckedChanged);
            // 
            // notePreview1
            // 
            this.notePreview1.Location = new System.Drawing.Point(252, 83);
            this.notePreview1.Name = "notePreview1";
            this.notePreview1.Size = new System.Drawing.Size(212, 197);
            this.notePreview1.TabIndex = 58;
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
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbResizeGrip)).EndInit();
            this.splitContainerContent.Panel1.ResumeLayout(false);
            this.splitContainerContent.Panel1.PerformLayout();
            this.splitContainerContent.Panel2.ResumeLayout(false);
            this.splitContainerContent.Panel2.PerformLayout();
            this.splitContainerContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pbResizeGrip;
        private System.Windows.Forms.SplitContainer splitContainerContent;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private NoteSkinPreview notePreview1;
        private System.Windows.Forms.CheckBox chxUseTexture;
    }
}