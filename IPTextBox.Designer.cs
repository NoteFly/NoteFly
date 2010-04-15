//-----------------------------------------------------------------------
// <copyright file="IPTextBox.Designer.cs" company="GNU">
// 
// This program is free software; you can redistribute it and/or modify it
// Free Software Foundation; either version 2, 
// or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// </copyright>
//-----------------------------------------------------------------------
namespace NoteFly
{
    /// <summary>
    /// IPTextBox class.
    /// </summary>
    public partial class IPTextBox
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// ip address box
        /// </summary>
        private System.Windows.Forms.TextBox tbIPaddress;

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
            this.tbIPaddress = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbIPaddress
            // 
            this.tbIPaddress.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbIPaddress.Location = new System.Drawing.Point(-1, -1);
            this.tbIPaddress.Name = "tbIPaddress";
            this.tbIPaddress.Size = new System.Drawing.Size(227, 22);
            this.tbIPaddress.TabIndex = 0;
            this.tbIPaddress.Text = "0.0.0.0";
            this.tbIPaddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbIPaddress_KeyDown);
            // 
            // IPTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tbIPaddress);
            this.Name = "IPTextBox";
            this.Size = new System.Drawing.Size(226, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
