namespace NoteFly
{
    partial class FrmException
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
            this.lblTextExceptionMessage = new System.Windows.Forms.Label();
            this.btnContinu = new System.Windows.Forms.Button();
            this.btnShutdown = new System.Windows.Forms.Button();
            this.lblTextOptions = new System.Windows.Forms.Label();
            this.tbExceptionMessage = new System.Windows.Forms.TextBox();
            this.lblTextStacktrace = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTextExceptionMessage
            // 
            this.lblTextExceptionMessage.AutoSize = true;
            this.lblTextExceptionMessage.Location = new System.Drawing.Point(8, 9);
            this.lblTextExceptionMessage.Name = "lblTextExceptionMessage";
            this.lblTextExceptionMessage.Size = new System.Drawing.Size(333, 13);
            this.lblTextExceptionMessage.TabIndex = 0;
            this.lblTextExceptionMessage.Text = "NoteFly has crashed, an exception with the following message occur:";
            // 
            // btnContinu
            // 
            this.btnContinu.Location = new System.Drawing.Point(191, 132);
            this.btnContinu.Name = "btnContinu";
            this.btnContinu.Size = new System.Drawing.Size(185, 23);
            this.btnContinu.TabIndex = 1;
            this.btnContinu.Text = "try to continu";
            this.btnContinu.UseVisualStyleBackColor = true;
            this.btnContinu.Click += new System.EventHandler(this.btnContinu_Click);
            // 
            // btnShutdown
            // 
            this.btnShutdown.Location = new System.Drawing.Point(11, 132);
            this.btnShutdown.Name = "btnShutdown";
            this.btnShutdown.Size = new System.Drawing.Size(174, 23);
            this.btnShutdown.TabIndex = 2;
            this.btnShutdown.Text = "shutdown";
            this.btnShutdown.UseVisualStyleBackColor = true;
            this.btnShutdown.Click += new System.EventHandler(this.btnShutdown_Click);
            // 
            // lblTextOptions
            // 
            this.lblTextOptions.AutoSize = true;
            this.lblTextOptions.Location = new System.Drawing.Point(12, 114);
            this.lblTextOptions.Name = "lblTextOptions";
            this.lblTextOptions.Size = new System.Drawing.Size(44, 13);
            this.lblTextOptions.TabIndex = 4;
            this.lblTextOptions.Text = "options:";
            // 
            // tbExceptionMessage
            // 
            this.tbExceptionMessage.Location = new System.Drawing.Point(11, 25);
            this.tbExceptionMessage.Multiline = true;
            this.tbExceptionMessage.Name = "tbExceptionMessage";
            this.tbExceptionMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbExceptionMessage.Size = new System.Drawing.Size(365, 69);
            this.tbExceptionMessage.TabIndex = 7;
            // 
            // lblTextStacktrace
            // 
            this.lblTextStacktrace.Location = new System.Drawing.Point(12, 97);
            this.lblTextStacktrace.Name = "lblTextStacktrace";
            this.lblTextStacktrace.Size = new System.Drawing.Size(364, 17);
            this.lblTextStacktrace.TabIndex = 8;
            this.lblTextStacktrace.Text = "The exception is written to the logfile for bug reporting.";
            // 
            // FrmException
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 166);
            this.Controls.Add(this.lblTextStacktrace);
            this.Controls.Add(this.tbExceptionMessage);
            this.Controls.Add(this.lblTextOptions);
            this.Controls.Add(this.btnShutdown);
            this.Controls.Add(this.btnContinu);
            this.Controls.Add(this.lblTextExceptionMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmException";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTextExceptionMessage;
        private System.Windows.Forms.Button btnContinu;
        private System.Windows.Forms.Button btnShutdown;
        private System.Windows.Forms.Label lblTextOptions;
        private System.Windows.Forms.TextBox tbExceptionMessage;
        private System.Windows.Forms.Label lblTextStacktrace;
    }
}