//-----------------------------------------------------------------------
// <copyright file="IPTextBox.cs" company="NoteFly">
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
    /// IP address textbox.
    /// With background color ip address validation hint.
    /// </summary>
    internal partial class IPTextBox : TextBox
    {
        /// <summary>
        /// IPAddress object.
        /// </summary>
        private System.Net.IPAddress ipaddr;

        /// <summary>
        /// Use a IPv4 address.
        /// </summary>
        private bool useipv4addr = true;

        /// <summary>
        /// Use a IPv6 address
        /// </summary>
        private bool useipv6addr = true;

        /// <summary>
        /// Gets or sets a value indicating whether a IPv4 address is used.
        /// </summary>
        public bool UseIPv4addr
        {
            get
            {
                return this.useipv4addr;
            }

            set
            {
                this.useipv4addr = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a IPv6 address is used.
        /// </summary>
        public bool UseIPv6addr
        {
            get
            {
                return this.useipv6addr;
            }

            set
            {
                this.useipv6addr = value;
            }
        }

        /// <summary>
        /// Get the IP address
        /// </summary>
        /// <returns>The IP address as string or empty string if no address entered</returns>
        public string GetIPAddress()
        {
            if (this.ipaddr != null)
            {
                return this.ipaddr.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Check if the ip address in the text propertie is valid.
        /// And set the backgroundcolor of this control if text contains a valid ip address to LightGreen.
        /// And if the text contains an invalid ip address to Salmon.
        /// </summary>
        public void ValidateIP()
        {
            bool validipaddr = System.Net.IPAddress.TryParse(this.Text, out this.ipaddr);
            if (validipaddr)
            {
                if (this.getnumofdots() == 3 && this.getnumofdash() == 0)
                {
                    // valid IPv4 address
                    if (this.useipv4addr)
                    {
                        this.BackColor = System.Drawing.Color.LightGreen;
                    }
                    else
                    {
                        // IPv4 address not allowed, but ip valid, orange for "warning".
                        this.BackColor = System.Drawing.Color.Orange;
                    }
                }
                else if (this.getnumofdots() == 0 && this.getnumofdash() >= 2)
                {
                    // valid IPv6 address
                    if (this.useipv6addr)
                    {
                        this.BackColor = System.Drawing.Color.LightGreen;
                    }
                    else
                    {
                        // IPv6 address not allowed, but ip valid, orange for "warning".
                        this.BackColor = System.Drawing.Color.Orange;
                    }
                }
            }
            else
            {
                if (this.TextLength > 0)
                {
                    this.BackColor = System.Drawing.Color.Salmon;
                }
                else
                {
                    this.BackColor = System.Drawing.SystemColors.Window;
                }
            }
        }

        /// <summary>
        /// Character entered.
        /// Allow only IPv4 and IPv6 characters to be added.
        /// </summary>
        /// <param name="e">Keyevent arguments</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            int k = e.KeyValue;

            // 'a'-'f'
            // ':'
            // '.'
            // '0'-'9'
            // shift, backspace, left, right, delete, end and home key
            if ((k >= 65 && k <= 70) || (k == 186 && e.Shift) || (k == 190 && !e.Shift) || (k >= 48 && k <= 57) || (k == 8 || k == 16 || k == 37 || k == 39 || k == 46 || k == 35 || k == 36))
            {
                e.SuppressKeyPress = false;
            }
            else
            {
                e.SuppressKeyPress = true;
            }

            base.OnKeyDown(e);
        }

        /// <summary>
        /// Validate ip address on key up.
        /// </summary>
        /// <param name="e">Keyboard event arguments</param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            this.ValidateIP();
            base.OnKeyUp(e);
        }

        /// <summary>
        /// Revalidate ip address on enable/disable change of this control.
        /// </summary>
        /// <param name="e">Event arguments</param>
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
                    this.ValidateIP();
                }
            }

            base.OnEnabledChanged(e);
        }

        /// <summary>
        /// Get the number of dots in the Text content.
        /// </summary>
        /// <returns>The number of dots</returns>
        private int getnumofdots()
        {
            return this.getnumofchar('.');
        }

        /// <summary>
        /// Get the number of dashes in the Text content.
        /// </summary>
        /// <returns>The number of dashes</returns>
        private int getnumofdash()
        {
            return this.getnumofchar(':');
        }

        /// <summary>
        /// Get the number of occurance of a particular character in the Text content.
        /// </summary>
        /// <param name="c">The characters to count</param>
        /// <returns>The number of chracters</returns>
        private int getnumofchar(char c)
        {
            int numdots = 0;
            for (int i = 0; i < this.Text.Length; i++)
            {
                if (this.Text[i] == c)
                {
                    numdots++;
                }
            }

            return numdots;
        }
    }
}
