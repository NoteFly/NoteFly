//-----------------------------------------------------------------------
// <copyright file="IPTextBox.cs" company="GNU">
//  NoteFly a note application.
//  Copyright (C) 2010  Tom
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
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// A editbox made for typing in a IP address their IP version4 or IP version6 address.
    /// </summary>
    public partial class IPTextBox : UserControl
    {
        private IPaddrType addrtype;

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the IPTextBox class.
        /// </summary>
        public IPTextBox()
        {
            this.InitializeComponent();
        }

        #endregion Constructors

        /// <summary>
        /// The types of address
        /// </summary>
        public enum IPaddrType
        {
            /// <summary>
            /// Presenting a ip version4 address.
            /// </summary>
            ipv4,

            /// <summary>
            /// Presenting a ip version6 address.
            /// </summary>
            ipv6
        }

        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// get the ip address
        /// </summary>
        /// <returns>the ip address as string.</returns>
        public string GetIPAddress()
        {
            return this.tbIPaddress.Text;
        }

        /// <summary>
        /// sets the ip address.
        /// </summary>
        /// <param name="addr">the new ip address.</param>
        public void SetIPAddress(string addr)
        {
            this.tbIPaddress.Text = addr;
        }
        
        /// <summary>
        /// Filter out illgale characters.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments</param>
        private void tbIPaddress_KeyDown(object sender, KeyEventArgs e)
        {
            int k = e.KeyValue;
            int numpoint = 0;
            int lastpoint = -10;
            for (int i = 0; i < this.tbIPaddress.Text.Length; i++)
            {
                if (this.tbIPaddress.Text[i] == '.')
                {
                    lastpoint = i;
                    numpoint++;
                }
            }

            if (k >= 65 && k <= 70)
            {
                // 'a'-'f'
                this.addrtype = IPaddrType.ipv6;
            }
            else if (k == 186 && e.Shift)
            {
                // ':'
                if (this.addrtype == IPaddrType.ipv4)
                {
                    MessageBox.Show("can't mix ipv4 and ipv6 seperators.", "error ip address", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }

                if (this.tbIPaddress.TextLength == 0)
                {
                    e.SuppressKeyPress = true;
                    //MessageBox.Show("IP v6 adress cannot start with ':'");
                }

                this.addrtype = IPaddrType.ipv6;
            }
            else if (k == 190 && !e.Shift)
            {
                // '.'
                if (numpoint >= 3)
                {
                    e.SuppressKeyPress = true;
                }
                else if (lastpoint == this.tbIPaddress.Text.Length - 1)
                {
                    e.SuppressKeyPress = true;
                }
                else if ((lastpoint < this.tbIPaddress.Text.Length - 4) && (lastpoint != -10))
                {
                    e.SuppressKeyPress = true;
                }
                else if ((this.tbIPaddress.Text.Length == 0) && (k == 190))
                {
                    e.SuppressKeyPress = true;
                }

                if (this.addrtype == IPaddrType.ipv6) 
                {
                    MessageBox.Show("can't mix ipv4 and ipv6 seperators. Don't use dots for ip v6 adress."); 
                }

                this.addrtype = IPaddrType.ipv4;
            }
            else if (k >= 48 && k <= 57)
            {
                // '0'-'9'
                if ((lastpoint < this.tbIPaddress.Text.Length - 3) && (lastpoint != -10) && (this.addrtype == IPaddrType.ipv4))
                {
                    e.SuppressKeyPress = true;
                }
                else
                {
                    e.SuppressKeyPress = false;
                }
            }
            else if (k == 8 || k == 16 || k == 37 || k == 39 || k == 46)
            {
                //shift, backspace, left. right, delete  key
                e.SuppressKeyPress = false;
            }
            else
            {
                //MessageBox.Show("key="+k);
                e.SuppressKeyPress = true;
            }
        }

        #endregion Methods
    }
}
