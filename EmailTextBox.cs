//-----------------------------------------------------------------------
// <copyright file="EmailTextBox.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2012  Tom
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
// </copyright>
//-----------------------------------------------------------------------
namespace NoteFly
{
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// EmailTextBox control
    /// </summary>
    internal partial class EmailTextBox : TextBox
    {
        /// <summary>
        /// Has the EmailTextBox been initilized.
        /// </summary>
        private bool setuptext = false;

        /// <summary>
        /// Check if the text is a valid e-mail address.
        /// </summary>
        /// <returns>True if this EmailTextBox contains a valid email address.</returns>
        public bool IsValidEmailAddress()
        {
            // contains @ and only one and is not the first character.
            if (this.Text.Contains("@") && this.Text.LastIndexOf("@") == this.Text.IndexOf("@") && this.Text.IndexOf("@") != 0)
            {
                string domain = this.Text.Substring(this.Text.IndexOf("@"), this.TextLength - this.Text.IndexOf("@"));
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

        /// <summary>
        /// Validate entered email address after EmailTextBox is created and text is set.
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected override void OnTextChanged(System.EventArgs e)
        {
            if (this.setuptext && this.Enabled)
            {
                if (this.IsValidEmailAddress())
                {
                    this.BackColor = Color.LightGreen;
                }
                else
                {
                    this.BackColor = Color.LightSalmon;
                }
            }

            this.setuptext = true;
        }

        /// <summary>
        /// Change background to inactive control backgroundcolor if control becomes inactive.
        /// </summary>
        /// <param name="e">Event argurments</param>
        protected override void OnEnabledChanged(System.EventArgs e)
        {
            if (!this.Enabled)
            {
                this.BackColor = SystemColors.Control;
            }
            else
            {
                this.BackColor = SystemColors.Window;
                if (this.TextLength > 0)
                {
                    this.OnTextChanged(null);
                }
            }

            base.OnEnabledChanged(e);
        }
    }
}
