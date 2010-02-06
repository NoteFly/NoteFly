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
