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
        /// Get the IP address
        /// </summary>
        /// <returns></returns>
        public string getIPAddress()
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
        /// Character entered.
        /// Allow only IPv4 and IPv6 characters to be added.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            int k = e.KeyValue;
            // 'a'-'f'
            // ':'
            // '.'
            // '0'-'9'
            // shift, backspace, left, right, delete key
            // /*
            if ((k >= 65 && k <= 70) || (k == 186 && e.Shift) || (k == 190 && !e.Shift) || (k >= 48 && k <= 57) || (k == 8 || k == 16 || k == 37 || k == 39 || k == 46))
            {
                e.SuppressKeyPress = false;
            }
            else
            {
                e.SuppressKeyPress = true;
            }
            // */
            base.OnKeyDown(e);
        }

        /// <summary>
        /// Validate ip address on key up.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            bool validipaddr = System.Net.IPAddress.TryParse(this.Text, out this.ipaddr);

            if (validipaddr && (this.getnumofdots() == 3 || this.Text.Contains(":")))
            {
                this.BackColor = System.Drawing.Color.LightGreen;                
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

            base.OnKeyUp(e);
        }

        /// <summary>
        /// Get the number of dots in the Text content.
        /// </summary>
        /// <returns></returns>
        private int getnumofdots()
        {
            int numdots = 0;
            for (int i = 0; i < this.Text.Length; i++)
            {
                if (this.Text[i] == '.')
                {
                    numdots++;
                }
            }

            return numdots;
        }
        
    }
}
