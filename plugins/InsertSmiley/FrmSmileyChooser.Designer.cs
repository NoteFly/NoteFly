namespace InsertSmiley
{
    public partial class FrmSmileyChooser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPnlSmileys;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Button btnSmileySmile;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Button btnSmileySad;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Button btnSmileyOh;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Button btnSmileyConfuzzed;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Button btnSmileyMad;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Button btnSmileyLol;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Button btnSmileyCool;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Button btnSmileyCry;

        /// <summary>
        /// 
        /// </summary>
        private System.Windows.Forms.Button btnSmileyWink;

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
            this.flowLayoutPnlSmileys = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSmileyOh = new System.Windows.Forms.Button();
            this.btnSmileyCool = new System.Windows.Forms.Button();
            this.btnSmileyCry = new System.Windows.Forms.Button();
            this.btnSmileyConfuzzed = new System.Windows.Forms.Button();
            this.btnSmileySmile = new System.Windows.Forms.Button();
            this.btnSmileySad = new System.Windows.Forms.Button();
            this.btnSmileyMad = new System.Windows.Forms.Button();
            this.btnSmileyLol = new System.Windows.Forms.Button();
            this.btnSmileyWink = new System.Windows.Forms.Button();
            this.flowLayoutPnlSmileys.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPnlSmileys
            // 
            this.flowLayoutPnlSmileys.BackColor = System.Drawing.Color.White;
            this.flowLayoutPnlSmileys.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPnlSmileys.Controls.Add(this.btnSmileyOh);
            this.flowLayoutPnlSmileys.Controls.Add(this.btnSmileyCool);
            this.flowLayoutPnlSmileys.Controls.Add(this.btnSmileyCry);
            this.flowLayoutPnlSmileys.Controls.Add(this.btnSmileyConfuzzed);
            this.flowLayoutPnlSmileys.Controls.Add(this.btnSmileySmile);
            this.flowLayoutPnlSmileys.Controls.Add(this.btnSmileySad);
            this.flowLayoutPnlSmileys.Controls.Add(this.btnSmileyMad);
            this.flowLayoutPnlSmileys.Controls.Add(this.btnSmileyLol);
            this.flowLayoutPnlSmileys.Controls.Add(this.btnSmileyWink);
            this.flowLayoutPnlSmileys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPnlSmileys.ForeColor = System.Drawing.Color.Black;
            this.flowLayoutPnlSmileys.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPnlSmileys.Margin = new System.Windows.Forms.Padding(1);
            this.flowLayoutPnlSmileys.Name = "flowLayoutPnlSmileys";
            this.flowLayoutPnlSmileys.Size = new System.Drawing.Size(168, 120);
            this.flowLayoutPnlSmileys.TabIndex = 0;
            // 
            // btnSmileyOh
            // 
            this.btnSmileyOh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSmileyOh.Image = global::InsertSmiley.Properties.Resources.smiley_oh;
            this.btnSmileyOh.Location = new System.Drawing.Point(1, 1);
            this.btnSmileyOh.Margin = new System.Windows.Forms.Padding(1);
            this.btnSmileyOh.Name = "btnSmileyOh";
            this.btnSmileyOh.Size = new System.Drawing.Size(53, 37);
            this.btnSmileyOh.TabIndex = 0;
            this.btnSmileyOh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSmileyOh.UseCompatibleTextRendering = true;
            this.btnSmileyOh.UseMnemonic = false;
            this.btnSmileyOh.UseVisualStyleBackColor = true;
            this.btnSmileyOh.Click += new System.EventHandler(this.btnSmileyChoice_Click);
            // 
            // btnSmileyCool
            // 
            this.btnSmileyCool.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSmileyCool.Image = global::InsertSmiley.Properties.Resources.smiley_cool;
            this.btnSmileyCool.Location = new System.Drawing.Point(56, 1);
            this.btnSmileyCool.Margin = new System.Windows.Forms.Padding(1);
            this.btnSmileyCool.Name = "btnSmileyCool";
            this.btnSmileyCool.Size = new System.Drawing.Size(53, 37);
            this.btnSmileyCool.TabIndex = 1;
            this.btnSmileyCool.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSmileyCool.UseCompatibleTextRendering = true;
            this.btnSmileyCool.UseMnemonic = false;
            this.btnSmileyCool.UseVisualStyleBackColor = true;
            this.btnSmileyCool.Click += new System.EventHandler(this.btnSmileyChoice_Click);
            // 
            // btnSmileyCry
            // 
            this.btnSmileyCry.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSmileyCry.Image = global::InsertSmiley.Properties.Resources.smiley_cry;
            this.btnSmileyCry.Location = new System.Drawing.Point(111, 1);
            this.btnSmileyCry.Margin = new System.Windows.Forms.Padding(1);
            this.btnSmileyCry.Name = "btnSmileyCry";
            this.btnSmileyCry.Size = new System.Drawing.Size(53, 37);
            this.btnSmileyCry.TabIndex = 2;
            this.btnSmileyCry.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSmileyCry.UseCompatibleTextRendering = true;
            this.btnSmileyCry.UseMnemonic = false;
            this.btnSmileyCry.UseVisualStyleBackColor = true;
            this.btnSmileyCry.Click += new System.EventHandler(this.btnSmileyChoice_Click);
            // 
            // btnSmileyConfuzzed
            // 
            this.btnSmileyConfuzzed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSmileyConfuzzed.Image = global::InsertSmiley.Properties.Resources.smiley_confuzzed;
            this.btnSmileyConfuzzed.Location = new System.Drawing.Point(1, 40);
            this.btnSmileyConfuzzed.Margin = new System.Windows.Forms.Padding(1);
            this.btnSmileyConfuzzed.Name = "btnSmileyConfuzzed";
            this.btnSmileyConfuzzed.Size = new System.Drawing.Size(53, 37);
            this.btnSmileyConfuzzed.TabIndex = 3;
            this.btnSmileyConfuzzed.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSmileyConfuzzed.UseCompatibleTextRendering = true;
            this.btnSmileyConfuzzed.UseMnemonic = false;
            this.btnSmileyConfuzzed.UseVisualStyleBackColor = true;
            this.btnSmileyConfuzzed.Click += new System.EventHandler(this.btnSmileyChoice_Click);
            // 
            // btnSmileySmile
            // 
            this.btnSmileySmile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSmileySmile.Image = global::InsertSmiley.Properties.Resources.smiley_smile;
            this.btnSmileySmile.Location = new System.Drawing.Point(56, 40);
            this.btnSmileySmile.Margin = new System.Windows.Forms.Padding(1);
            this.btnSmileySmile.Name = "btnSmileySmile";
            this.btnSmileySmile.Size = new System.Drawing.Size(53, 37);
            this.btnSmileySmile.TabIndex = 4;
            this.btnSmileySmile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSmileySmile.UseCompatibleTextRendering = true;
            this.btnSmileySmile.UseMnemonic = false;
            this.btnSmileySmile.UseVisualStyleBackColor = true;
            this.btnSmileySmile.Click += new System.EventHandler(this.btnSmileyChoice_Click);
            // 
            // btnSmileySad
            // 
            this.btnSmileySad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSmileySad.Image = global::InsertSmiley.Properties.Resources.smiley_sad;
            this.btnSmileySad.Location = new System.Drawing.Point(111, 40);
            this.btnSmileySad.Margin = new System.Windows.Forms.Padding(1);
            this.btnSmileySad.Name = "btnSmileySad";
            this.btnSmileySad.Size = new System.Drawing.Size(53, 37);
            this.btnSmileySad.TabIndex = 5;
            this.btnSmileySad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSmileySad.UseCompatibleTextRendering = true;
            this.btnSmileySad.UseMnemonic = false;
            this.btnSmileySad.UseVisualStyleBackColor = true;
            this.btnSmileySad.Click += new System.EventHandler(this.btnSmileyChoice_Click);
            // 
            // btnSmileyMad
            // 
            this.btnSmileyMad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSmileyMad.Image = global::InsertSmiley.Properties.Resources.smiley_mad;
            this.btnSmileyMad.Location = new System.Drawing.Point(1, 79);
            this.btnSmileyMad.Margin = new System.Windows.Forms.Padding(1);
            this.btnSmileyMad.Name = "btnSmileyMad";
            this.btnSmileyMad.Size = new System.Drawing.Size(53, 37);
            this.btnSmileyMad.TabIndex = 6;
            this.btnSmileyMad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSmileyMad.UseCompatibleTextRendering = true;
            this.btnSmileyMad.UseMnemonic = false;
            this.btnSmileyMad.UseVisualStyleBackColor = true;
            this.btnSmileyMad.Click += new System.EventHandler(this.btnSmileyChoice_Click);
            // 
            // btnSmileyLol
            // 
            this.btnSmileyLol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSmileyLol.Image = global::InsertSmiley.Properties.Resources.smiley_lol;
            this.btnSmileyLol.Location = new System.Drawing.Point(56, 79);
            this.btnSmileyLol.Margin = new System.Windows.Forms.Padding(1);
            this.btnSmileyLol.Name = "btnSmileyLol";
            this.btnSmileyLol.Size = new System.Drawing.Size(53, 37);
            this.btnSmileyLol.TabIndex = 7;
            this.btnSmileyLol.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSmileyLol.UseCompatibleTextRendering = true;
            this.btnSmileyLol.UseMnemonic = false;
            this.btnSmileyLol.UseVisualStyleBackColor = true;
            this.btnSmileyLol.Click += new System.EventHandler(this.btnSmileyChoice_Click);
            // 
            // btnSmileyWink
            // 
            this.btnSmileyWink.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSmileyWink.Image = global::InsertSmiley.Properties.Resources.smiley_wink;
            this.btnSmileyWink.Location = new System.Drawing.Point(111, 79);
            this.btnSmileyWink.Margin = new System.Windows.Forms.Padding(1);
            this.btnSmileyWink.Name = "btnSmileyWink";
            this.btnSmileyWink.Size = new System.Drawing.Size(53, 37);
            this.btnSmileyWink.TabIndex = 8;
            this.btnSmileyWink.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSmileyWink.UseCompatibleTextRendering = true;
            this.btnSmileyWink.UseMnemonic = false;
            this.btnSmileyWink.UseVisualStyleBackColor = true;
            this.btnSmileyWink.Click += new System.EventHandler(this.btnSmileyChoice_Click);
            // 
            // FrmSmileyChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(168, 120);
            this.Controls.Add(this.flowLayoutPnlSmileys);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.Name = "FrmSmileyChooser";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FrmSmileyChooser";
            this.Deactivate += new System.EventHandler(this.FrmSmileyChooser_Deactivate);
            this.flowLayoutPnlSmileys.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion




    }
}