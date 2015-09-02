namespace NoteFly
{
    public partial class SearchTextBox
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// TextBox tbKeywords
        /// </summary>
        private System.Windows.Forms.TextBox tbKeywords;

        /// <summary>
        /// Label lblTextSearch
        /// </summary>
        private System.Windows.Forms.Label lblTextSearch;

        /// <summary>
        /// Timer timerStartAutoSearch
        /// </summary>
        private System.Windows.Forms.Timer timerStartAutoSearch;

        /// <summary>
        /// TableLayoutPanel tableLayoutPnlSearchbox
        /// </summary>
        private System.Windows.Forms.TableLayoutPanel tableLayoutPnlSearchbox;

        /// <summary>
        /// Button btnKeywordClear
        /// </summary>
        private System.Windows.Forms.Button btnKeywordClear;

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
            this.tableLayoutPnlSearchbox = new System.Windows.Forms.TableLayoutPanel();
            this.btnKeywordClear = new System.Windows.Forms.Button();
            this.tableLayoutPnlSearchbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbKeywords
            // 
            this.tbKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbKeywords.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbKeywords.Location = new System.Drawing.Point(54, 3);
            this.tbKeywords.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.tbKeywords.Name = "tbKeywords";
            this.tbKeywords.Size = new System.Drawing.Size(135, 22);
            this.tbKeywords.TabIndex = 0;
            this.tbKeywords.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbKeywords_KeyUp);
            // 
            // lblTextSearch
            // 
            this.lblTextSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTextSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextSearch.Location = new System.Drawing.Point(0, 0);
            this.lblTextSearch.Margin = new System.Windows.Forms.Padding(0);
            this.lblTextSearch.Name = "lblTextSearch";
            this.lblTextSearch.Padding = new System.Windows.Forms.Padding(1, 6, 1, 1);
            this.lblTextSearch.Size = new System.Drawing.Size(54, 28);
            this.lblTextSearch.TabIndex = 1;
            this.lblTextSearch.Text = "search:";
            // 
            // timerStartAutoSearch
            // 
            this.timerStartAutoSearch.Interval = 600;
            this.timerStartAutoSearch.Tick += new System.EventHandler(this.timerStartSearch_Tick);
            // 
            // tableLayoutPnlSearchbox
            // 
            this.tableLayoutPnlSearchbox.ColumnCount = 3;
            this.tableLayoutPnlSearchbox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 54F));
            this.tableLayoutPnlSearchbox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPnlSearchbox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPnlSearchbox.Controls.Add(this.lblTextSearch, 0, 0);
            this.tableLayoutPnlSearchbox.Controls.Add(this.tbKeywords, 1, 0);
            this.tableLayoutPnlSearchbox.Controls.Add(this.btnKeywordClear, 2, 0);
            this.tableLayoutPnlSearchbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPnlSearchbox.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
            this.tableLayoutPnlSearchbox.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPnlSearchbox.Name = "tableLayoutPnlSearchbox";
            this.tableLayoutPnlSearchbox.RowCount = 1;
            this.tableLayoutPnlSearchbox.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPnlSearchbox.Size = new System.Drawing.Size(219, 28);
            this.tableLayoutPnlSearchbox.TabIndex = 3;
            // 
            // btnKeywordClear
            // 
            this.btnKeywordClear.BackColor = System.Drawing.Color.White;
            this.btnKeywordClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnKeywordClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeywordClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKeywordClear.Location = new System.Drawing.Point(189, 3);
            this.btnKeywordClear.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnKeywordClear.Name = "btnKeywordClear";
            this.btnKeywordClear.Size = new System.Drawing.Size(30, 22);
            this.btnKeywordClear.TabIndex = 2;
            this.btnKeywordClear.Text = "X";
            this.btnKeywordClear.UseMnemonic = false;
            this.btnKeywordClear.UseVisualStyleBackColor = false;
            this.btnKeywordClear.Visible = false;
            this.btnKeywordClear.Click += new System.EventHandler(this.btnKeywordClear_Click);
            // 
            // SearchTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPnlSearchbox);
            this.Name = "SearchTextBox";
            this.Size = new System.Drawing.Size(219, 28);
            this.tableLayoutPnlSearchbox.ResumeLayout(false);
            this.tableLayoutPnlSearchbox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
