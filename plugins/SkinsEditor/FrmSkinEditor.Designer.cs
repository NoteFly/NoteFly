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
            this.pbResizeGrip = new System.Windows.Forms.PictureBox();
            this.splitContainerContent = new System.Windows.Forms.SplitContainer();
            this.gbxPreview = new System.Windows.Forms.GroupBox();
            this.pnlPreviewNoteWindow = new System.Windows.Forms.Panel();
            this.pnlPreviewNoteHead = new System.Windows.Forms.Panel();
            this.btnPreviewNoteBtnClose = new System.Windows.Forms.Button();
            this.lblPreviewNoteTitle = new System.Windows.Forms.Label();
            this.pnlPreviewNoteContent = new System.Windows.Forms.Panel();
            this.lblPreviewNoteContent = new System.Windows.Forms.Label();
            this.picboxPreviewNoteResizegrid = new System.Windows.Forms.PictureBox();
            this.pnlHead.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResizeGrip)).BeginInit();
            this.splitContainerContent.Panel1.SuspendLayout();
            this.splitContainerContent.Panel2.SuspendLayout();
            this.splitContainerContent.SuspendLayout();
            this.gbxPreview.SuspendLayout();
            this.pnlPreviewNoteWindow.SuspendLayout();
            this.pnlPreviewNoteHead.SuspendLayout();
            this.pnlPreviewNoteContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picboxPreviewNoteResizegrid)).BeginInit();
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
            this.pnlClrText.MouseLeave += new System.EventHandler(this.BackNormalCusors);
            this.pnlClrText.MouseMove += new System.Windows.Forms.MouseEventHandler(this.HandEnabled);
            this.pnlClrText.Click += new System.EventHandler(this.pnlClrText_Click);
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
            this.pnlClrHighlight.MouseLeave += new System.EventHandler(this.BackNormalCusors);
            this.pnlClrHighlight.MouseMove += new System.Windows.Forms.MouseEventHandler(this.HandEnabled);
            this.pnlClrHighlight.Click += new System.EventHandler(this.pnlClrHighlight_Click);
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
            this.pnlClrSelecting.MouseLeave += new System.EventHandler(this.BackNormalCusors);
            this.pnlClrSelecting.MouseMove += new System.Windows.Forms.MouseEventHandler(this.HandEnabled);
            this.pnlClrSelecting.Click += new System.EventHandler(this.pnlClrSelecting_Click);
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
            this.pnlClrPrimary.MouseLeave += new System.EventHandler(this.BackNormalCusors);
            this.pnlClrPrimary.MouseMove += new System.Windows.Forms.MouseEventHandler(this.HandEnabled);
            this.pnlClrPrimary.Click += new System.EventHandler(this.pnlClrPrimary_Click);
            // 
            // lblTextSkinname
            // 
            this.lblTextSkinname.AutoSize = true;
            this.lblTextSkinname.Location = new System.Drawing.Point(21, 28);
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
            this.tbSkinName.Location = new System.Drawing.Point(128, 25);
            this.tbSkinName.MaxLength = 200;
            this.tbSkinName.Name = "tbSkinName";
            this.tbSkinName.Size = new System.Drawing.Size(145, 22);
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
            this.lblTextPrimartTextureLayout.Location = new System.Drawing.Point(21, 86);
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
            this.cbxPrimaryTextureLayout.Location = new System.Drawing.Point(128, 83);
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
            this.pnlContent.Controls.Add(this.pbResizeGrip);
            this.pnlContent.Controls.Add(this.splitContainerContent);
            this.pnlContent.Controls.Add(this.btnNewSkin);
            this.pnlContent.Controls.Add(this.btnDeleteSkin);
            this.pnlContent.Controls.Add(this.btnEditskin);
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
            this.splitContainerContent.Panel2.Controls.Add(this.gbxPreview);
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
            // gbxPreview
            // 
            this.gbxPreview.Controls.Add(this.pnlPreviewNoteWindow);
            this.gbxPreview.Location = new System.Drawing.Point(267, 86);
            this.gbxPreview.Name = "gbxPreview";
            this.gbxPreview.Size = new System.Drawing.Size(197, 189);
            this.gbxPreview.TabIndex = 58;
            this.gbxPreview.TabStop = false;
            this.gbxPreview.Text = "preview";
            this.gbxPreview.Visible = false;
            // 
            // pnlPreviewNoteWindow
            // 
            this.pnlPreviewNoteWindow.BackColor = System.Drawing.Color.Transparent;
            this.pnlPreviewNoteWindow.Controls.Add(this.pnlPreviewNoteHead);
            this.pnlPreviewNoteWindow.Controls.Add(this.pnlPreviewNoteContent);
            this.pnlPreviewNoteWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPreviewNoteWindow.Location = new System.Drawing.Point(3, 16);
            this.pnlPreviewNoteWindow.Name = "pnlPreviewNoteWindow";
            this.pnlPreviewNoteWindow.Size = new System.Drawing.Size(191, 170);
            this.pnlPreviewNoteWindow.TabIndex = 2;
            // 
            // pnlPreviewNoteHead
            // 
            this.pnlPreviewNoteHead.BackColor = System.Drawing.Color.Transparent;
            this.pnlPreviewNoteHead.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPreviewNoteHead.Controls.Add(this.btnPreviewNoteBtnClose);
            this.pnlPreviewNoteHead.Controls.Add(this.lblPreviewNoteTitle);
            this.pnlPreviewNoteHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPreviewNoteHead.Location = new System.Drawing.Point(0, 0);
            this.pnlPreviewNoteHead.Margin = new System.Windows.Forms.Padding(0);
            this.pnlPreviewNoteHead.Name = "pnlPreviewNoteHead";
            this.pnlPreviewNoteHead.Size = new System.Drawing.Size(191, 30);
            this.pnlPreviewNoteHead.TabIndex = 1;
            this.pnlPreviewNoteHead.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlPreviewNoteHead_MouseDown);
            this.pnlPreviewNoteHead.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlPreviewNoteHead_MouseUp);
            // 
            // btnPreviewNoteBtnClose
            // 
            this.btnPreviewNoteBtnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPreviewNoteBtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreviewNoteBtnClose.Location = new System.Drawing.Point(152, 3);
            this.btnPreviewNoteBtnClose.Name = "btnPreviewNoteBtnClose";
            this.btnPreviewNoteBtnClose.Size = new System.Drawing.Size(32, 23);
            this.btnPreviewNoteBtnClose.TabIndex = 1;
            this.btnPreviewNoteBtnClose.Text = "X";
            this.btnPreviewNoteBtnClose.UseVisualStyleBackColor = true;
            // 
            // lblPreviewNoteTitle
            // 
            this.lblPreviewNoteTitle.AutoSize = true;
            this.lblPreviewNoteTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreviewNoteTitle.Location = new System.Drawing.Point(3, 5);
            this.lblPreviewNoteTitle.Name = "lblPreviewNoteTitle";
            this.lblPreviewNoteTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.lblPreviewNoteTitle.Size = new System.Drawing.Size(67, 18);
            this.lblPreviewNoteTitle.TabIndex = 0;
            this.lblPreviewNoteTitle.Text = "example";
            this.lblPreviewNoteTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlPreviewNoteHead_MouseDown);
            this.lblPreviewNoteTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlPreviewNoteHead_MouseUp);
            // 
            // pnlPreviewNoteContent
            // 
            this.pnlPreviewNoteContent.BackColor = System.Drawing.Color.Transparent;
            this.pnlPreviewNoteContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPreviewNoteContent.Controls.Add(this.lblPreviewNoteContent);
            this.pnlPreviewNoteContent.Controls.Add(this.picboxPreviewNoteResizegrid);
            this.pnlPreviewNoteContent.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPreviewNoteContent.Location = new System.Drawing.Point(0, 30);
            this.pnlPreviewNoteContent.Name = "pnlPreviewNoteContent";
            this.pnlPreviewNoteContent.Size = new System.Drawing.Size(191, 140);
            this.pnlPreviewNoteContent.TabIndex = 0;
            // 
            // lblPreviewNoteContent
            // 
            this.lblPreviewNoteContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPreviewNoteContent.Location = new System.Drawing.Point(6, 9);
            this.lblPreviewNoteContent.Name = "lblPreviewNoteContent";
            this.lblPreviewNoteContent.Size = new System.Drawing.Size(173, 118);
            this.lblPreviewNoteContent.TabIndex = 61;
            this.lblPreviewNoteContent.Text = "Test test test test test test test test test test test  test test test test test " +
                "test test test test test \r\n";
            // 
            // picboxPreviewNoteResizegrid
            // 
            this.picboxPreviewNoteResizegrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.picboxPreviewNoteResizegrid.BackColor = System.Drawing.Color.Transparent;
            this.picboxPreviewNoteResizegrid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picboxPreviewNoteResizegrid.Image = global::SkinsEditor.Properties.Resources.hoekje;
            this.picboxPreviewNoteResizegrid.Location = new System.Drawing.Point(176, 123);
            this.picboxPreviewNoteResizegrid.Margin = new System.Windows.Forms.Padding(0);
            this.picboxPreviewNoteResizegrid.Name = "picboxPreviewNoteResizegrid";
            this.picboxPreviewNoteResizegrid.Size = new System.Drawing.Size(17, 18);
            this.picboxPreviewNoteResizegrid.TabIndex = 60;
            this.picboxPreviewNoteResizegrid.TabStop = false;
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
            ((System.ComponentModel.ISupportInitialize)(this.pbResizeGrip)).EndInit();
            this.splitContainerContent.Panel1.ResumeLayout(false);
            this.splitContainerContent.Panel1.PerformLayout();
            this.splitContainerContent.Panel2.ResumeLayout(false);
            this.splitContainerContent.Panel2.PerformLayout();
            this.splitContainerContent.ResumeLayout(false);
            this.gbxPreview.ResumeLayout(false);
            this.pnlPreviewNoteWindow.ResumeLayout(false);
            this.pnlPreviewNoteHead.ResumeLayout(false);
            this.pnlPreviewNoteHead.PerformLayout();
            this.pnlPreviewNoteContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picboxPreviewNoteResizegrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHead;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pbResizeGrip;
        private System.Windows.Forms.SplitContainer splitContainerContent;
        private System.Windows.Forms.GroupBox gbxPreview;
        private System.Windows.Forms.Panel pnlPreviewNoteContent;
        private System.Windows.Forms.Panel pnlPreviewNoteHead;
        private System.Windows.Forms.Label lblPreviewNoteTitle;
        private System.Windows.Forms.Button btnPreviewNoteBtnClose;
        private System.Windows.Forms.Panel pnlPreviewNoteWindow;
        private System.Windows.Forms.PictureBox picboxPreviewNoteResizegrid;
        private System.Windows.Forms.Label lblPreviewNoteContent;
    }
}