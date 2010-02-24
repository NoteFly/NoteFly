/* Copyright (C) 2009-2010
 * 
 * This program is free software; you can redistribute it and/or modify it
 * Free Software Foundation; either version 2, or (at your option) any
 * later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *  
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA
 */
using System;
using System.Windows.Forms;

namespace NoteFly
{
    public partial class IPTextBox : UserControl
    {
        public enum IPaddrType
        {
            ipv4,
            ipv6
        }

        private IPaddrType addrtype;

        #region Constructors (1)

        public IPTextBox()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Methods (2)

        // Public Methods (2) 

        public String GetIPAddress()
        {
            return tbIPaddress.Text;
        }

        public void SetIPAddress(string addr)
        {
            this.tbIPaddress.Text = addr;
        }
        
        /// <summary>
        /// Filter out illgale charcters
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbIPaddress_KeyDown(object sender, KeyEventArgs e)
        {
            int k = e.KeyValue;
            //'0'-'9'
            if (k >= 48 && k <= 57)
            {
                MessageBox.Show("ok, ipv4 or ipv6 char");
            }
            //'a'-'f'
            else if (k >= 65 && k <= 70)
            {
                addrtype = IPaddrType.ipv6;
                MessageBox.Show("ok, ipv6 char");
            }
            //':'
            else if (k == 186 && e.Shift)
            {
                if (addrtype == IPaddrType.ipv4) { MessageBox.Show("can't mix ipv4 and ipv6 seperators."); }
                addrtype = IPaddrType.ipv6;
                MessageBox.Show("ok, ipv6 char");
            }
            //'.'
            else if (k == 190 && !e.Shift)
            {
                if (addrtype == IPaddrType.ipv6) { MessageBox.Show("can't mix ipv4 and ipv6 seperators."); }
                addrtype = IPaddrType.ipv4;
                MessageBox.Show("ok, ipv4 char");
            }
            //shift or backspace
            else if (k == 16 || k==8)
            {
                //allow
            }
            else
            {
                e.SuppressKeyPress = true;
            }
        }

        #endregion Methods




    }
}
