namespace NoteFly
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Text;
    using System.Windows.Forms;
    using System.Drawing;

    internal partial class EmailTextBox : TextBox
    {
        bool setuptext = false;

        protected override void OnTextChanged(System.EventArgs e)
        {
            if (setuptext && this.Enabled)
            {                
                if (IsValidEmailAddress())
                {
                    this.BackColor = Color.LightGreen;
                }
                else
                {
                    this.BackColor = Color.LightSalmon;
                }
            }

            setuptext = true;
        }

        /// <summary>
        /// Check if the text is a valid e-mail address.
        /// </summary>
        /// <returns></returns>
        public bool IsValidEmailAddress()
        {
            // contains @ and only one and is not the first character.
            if (this.Text.Contains("@") && this.Text.LastIndexOf("@") == this.Text.IndexOf("@") && this.Text.IndexOf("@") != 0)
            {
                string domain = this.Text.Substring(this.Text.IndexOf("@"), this.TextLength-this.Text.IndexOf("@"));
                // contains dot after @ and only one
                if (domain.Contains(".") && domain.LastIndexOf(".") == domain.IndexOf("."))
                {
                    // tld is at least 2 character long
                    const int MINTLDLENGTH = 2;
                    if ((domain.IndexOf(".") + MINTLDLENGTH) < domain.Length)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
