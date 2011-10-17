namespace SkinsEditor
{
    partial class FrmSkinEditor
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
            this.lbxSkins = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSkinName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPrimaryColor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbSelectingColor = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbHighlightingColor = new System.Windows.Forms.TextBox();
            this.colordlg = new System.Windows.Forms.ColorDialog();
            this.btnPickPrimaryColor = new System.Windows.Forms.Button();
            this.btnPickSelectingColor = new System.Windows.Forms.Button();
            this.btnPickHighlightColor = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tbTextColor = new System.Windows.Forms.TextBox();
            this.btnPickTextColor = new System.Windows.Forms.Button();
            this.btnSaveSkin = new System.Windows.Forms.Button();
            this.btnNewSkin = new System.Windows.Forms.Button();
            this.btnEditskin = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlClrText = new System.Windows.Forms.Panel();
            this.pnlClrHighlight = new System.Windows.Forms.Panel();
            this.pnlClrSelecting = new System.Windows.Forms.Panel();
            this.pnlClrPrimary = new System.Windows.Forms.Panel();
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
            this.lbxSkins.Size = new System.Drawing.Size(118, 212);
            this.lbxSkins.TabIndex = 1;
            this.lbxSkins.SelectedIndexChanged += new System.EventHandler(this.lbxSkins_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Skins:";
            this.label1.UseCompatibleTextRendering = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Skin name:";
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Primary color:";
            // 
            // tbPrimaryColor
            // 
            this.tbPrimaryColor.Enabled = false;
            this.tbPrimaryColor.Location = new System.Drawing.Point(141, 84);
            this.tbPrimaryColor.MaxLength = 7;
            this.tbPrimaryColor.Name = "tbPrimaryColor";
            this.tbPrimaryColor.Size = new System.Drawing.Size(59, 20);
            this.tbPrimaryColor.TabIndex = 6;
            this.tbPrimaryColor.Tag = "1";
            this.tbPrimaryColor.WordWrap = false;
            this.tbPrimaryColor.TextChanged += new System.EventHandler(this.ParserAsPreviewColor);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Selecting color:";
            // 
            // tbSelectingColor
            // 
            this.tbSelectingColor.Enabled = false;
            this.tbSelectingColor.Location = new System.Drawing.Point(141, 112);
            this.tbSelectingColor.MaxLength = 7;
            this.tbSelectingColor.Name = "tbSelectingColor";
            this.tbSelectingColor.Size = new System.Drawing.Size(59, 20);
            this.tbSelectingColor.TabIndex = 8;
            this.tbSelectingColor.Tag = "2";
            this.tbSelectingColor.WordWrap = false;
            this.tbSelectingColor.TextChanged += new System.EventHandler(this.ParserAsPreviewColor);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Highlighting color:";
            // 
            // tbHighlightingColor
            // 
            this.tbHighlightingColor.Enabled = false;
            this.tbHighlightingColor.Location = new System.Drawing.Point(141, 141);
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
            this.btnPickPrimaryColor.Location = new System.Drawing.Point(224, 82);
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
            this.btnPickSelectingColor.Location = new System.Drawing.Point(224, 109);
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
            this.btnPickHighlightColor.Location = new System.Drawing.Point(224, 139);
            this.btnPickHighlightColor.Name = "btnPickHighlightColor";
            this.btnPickHighlightColor.Size = new System.Drawing.Size(35, 23);
            this.btnPickHighlightColor.TabIndex = 13;
            this.btnPickHighlightColor.Text = "...";
            this.btnPickHighlightColor.UseCompatibleTextRendering = true;
            this.btnPickHighlightColor.UseVisualStyleBackColor = true;
            this.btnPickHighlightColor.Click += new System.EventHandler(this.btnHighlightColor_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Text color:";
            // 
            // tbTextColor
            // 
            this.tbTextColor.Enabled = false;
            this.tbTextColor.Location = new System.Drawing.Point(141, 169);
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
            this.btnPickTextColor.Location = new System.Drawing.Point(224, 167);
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
            this.btnSaveSkin.Location = new System.Drawing.Point(39, 210);
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
            this.btnClose.Location = new System.Drawing.Point(152, 210);
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
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlClrText);
            this.splitContainer1.Panel2.Controls.Add(this.pnlClrHighlight);
            this.splitContainer1.Panel2.Controls.Add(this.pnlClrSelecting);
            this.splitContainer1.Panel2.Controls.Add(this.pnlClrPrimary);
            this.splitContainer1.Panel2.Controls.Add(this.btnClose);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.btnEditskin);
            this.splitContainer1.Panel2.Controls.Add(this.tbSkinName);
            this.splitContainer1.Panel2.Controls.Add(this.btnNewSkin);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.btnSaveSkin);
            this.splitContainer1.Panel2.Controls.Add(this.tbPrimaryColor);
            this.splitContainer1.Panel2.Controls.Add(this.btnPickTextColor);
            this.splitContainer1.Panel2.Controls.Add(this.label4);
            this.splitContainer1.Panel2.Controls.Add(this.tbTextColor);
            this.splitContainer1.Panel2.Controls.Add(this.tbSelectingColor);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.btnPickHighlightColor);
            this.splitContainer1.Panel2.Controls.Add(this.tbHighlightingColor);
            this.splitContainer1.Panel2.Controls.Add(this.btnPickSelectingColor);
            this.splitContainer1.Panel2.Controls.Add(this.btnPickPrimaryColor);
            this.splitContainer1.Size = new System.Drawing.Size(413, 240);
            this.splitContainer1.SplitterDistance = 124;
            this.splitContainer1.TabIndex = 21;
            // 
            // pnlClrText
            // 
            this.pnlClrText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClrText.ForeColor = System.Drawing.Color.Black;
            this.pnlClrText.Location = new System.Drawing.Point(202, 169);
            this.pnlClrText.Name = "pnlClrText";
            this.pnlClrText.Size = new System.Drawing.Size(21, 21);
            this.pnlClrText.TabIndex = 24;
            // 
            // pnlClrHighlight
            // 
            this.pnlClrHighlight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClrHighlight.ForeColor = System.Drawing.Color.Black;
            this.pnlClrHighlight.Location = new System.Drawing.Point(202, 141);
            this.pnlClrHighlight.Name = "pnlClrHighlight";
            this.pnlClrHighlight.Size = new System.Drawing.Size(21, 21);
            this.pnlClrHighlight.TabIndex = 23;
            // 
            // pnlClrSelecting
            // 
            this.pnlClrSelecting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClrSelecting.ForeColor = System.Drawing.Color.Black;
            this.pnlClrSelecting.Location = new System.Drawing.Point(202, 112);
            this.pnlClrSelecting.Name = "pnlClrSelecting";
            this.pnlClrSelecting.Size = new System.Drawing.Size(21, 21);
            this.pnlClrSelecting.TabIndex = 22;
            // 
            // pnlClrPrimary
            // 
            this.pnlClrPrimary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlClrPrimary.ForeColor = System.Drawing.Color.Black;
            this.pnlClrPrimary.Location = new System.Drawing.Point(202, 84);
            this.pnlClrPrimary.Name = "pnlClrPrimary";
            this.pnlClrPrimary.Size = new System.Drawing.Size(21, 21);
            this.pnlClrPrimary.TabIndex = 21;
            // 
            // FrmSkinEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(413, 240);
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

        private System.Windows.Forms.ListBox lbxSkins;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbSkinName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbPrimaryColor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbSelectingColor;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbHighlightingColor;
        private System.Windows.Forms.ColorDialog colordlg;
        private System.Windows.Forms.Button btnPickPrimaryColor;
        private System.Windows.Forms.Button btnPickSelectingColor;
        private System.Windows.Forms.Button btnPickHighlightColor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbTextColor;
        private System.Windows.Forms.Button btnPickTextColor;
        private System.Windows.Forms.Button btnSaveSkin;
        private System.Windows.Forms.Button btnNewSkin;
        private System.Windows.Forms.Button btnEditskin;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnlClrText;
        private System.Windows.Forms.Panel pnlClrHighlight;
        private System.Windows.Forms.Panel pnlClrSelecting;
        private System.Windows.Forms.Panel pnlClrPrimary;
    }
}