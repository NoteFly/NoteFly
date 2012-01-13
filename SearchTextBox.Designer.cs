namespace NoteFly
{
    partial class SearchTextBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tbKeywords = new System.Windows.Forms.TextBox();
            this.lblTextSearch = new System.Windows.Forms.Label();
            this.timerStartAutoSearch = new System.Windows.Forms.Timer(this.components);
            this.btnKeywordClear = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbKeywords
            // 
            this.tbKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbKeywords.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbKeywords.Location = new System.Drawing.Point(58, 3);
            this.tbKeywords.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.tbKeywords.Name = "tbKeywords";
            this.tbKeywords.Size = new System.Drawing.Size(131, 22);
            this.tbKeywords.TabIndex = 0;
            this.tbKeywords.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbKeywords_KeyUp);
            // 
            // lblTextSearch
            // 
            this.lblTextSearch.Location = new System.Drawing.Point(3, 7);
            this.lblTextSearch.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.lblTextSearch.Name = "lblTextSearch";
            this.lblTextSearch.Size = new System.Drawing.Size(42, 13);
            this.lblTextSearch.TabIndex = 1;
            this.lblTextSearch.Text = "search:";
            // 
            // timerStartAutoSearch
            // 
            this.timerStartAutoSearch.Interval = 600;
            this.timerStartAutoSearch.Tick += new System.EventHandler(this.timerStartSearch_Tick);
            // 
            // btnKeywordClear
            // 
            this.btnKeywordClear.BackColor = System.Drawing.Color.White;
            this.btnKeywordClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeywordClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKeywordClear.Location = new System.Drawing.Point(189, 3);
            this.btnKeywordClear.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnKeywordClear.Name = "btnKeywordClear";
            this.btnKeywordClear.Size = new System.Drawing.Size(23, 22);
            this.btnKeywordClear.TabIndex = 2;
            this.btnKeywordClear.Text = "X";
            this.btnKeywordClear.UseMnemonic = false;
            this.btnKeywordClear.UseVisualStyleBackColor = false;
            this.btnKeywordClear.Visible = false;
            this.btnKeywordClear.Click += new System.EventHandler(this.btnKeywordClear_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.10053F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.89947F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.Controls.Add(this.lblTextSearch, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnKeywordClear, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbKeywords, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(219, 28);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // SearchTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SearchTextBox";
            this.Size = new System.Drawing.Size(219, 28);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbKeywords;
        private System.Windows.Forms.Label lblTextSearch;
        private System.Windows.Forms.Timer timerStartAutoSearch;
        private System.Windows.Forms.Button btnKeywordClear;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
