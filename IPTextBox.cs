using System;
using System.Windows.Forms;

namespace NoteFly
{
    public partial class IPTextBox : UserControl
    {
		#region Constructors (1) 

        public IPTextBox()
        {
            InitializeComponent();
        }

		#endregion Constructors 

		#region Properties (4) 

        public string IPpart1
        {
            get
            {
                return this.tbIPpart1.Text;
            }
            set
            {
                tbIPpart1.Text = value;
            }
        }

        public string IPpart2
        {
            get
            {
                return this.tbIPpart2.Text;
            }
            set
            {
                tbIPpart2.Text = value;
            }
        }

        public string IPpart3
        {
            get
            {
                return this.tbIPpart3.Text;
            }
            set
            {
                tbIPpart3.Text = value;
            }
        }

        public string IPpart4
        {
            get
            {
                return this.tbIPpart4.Text;
            }
            set
            {
                tbIPpart4.Text = value;
            }
        }

		#endregion Properties 

		#region Methods (2) 

		// Public Methods (2) 

        public String GetIPAddress()
        {
            return IPpart1 + "." + IPpart2 + "." + IPpart3 + "." + IPpart4;
        }

        public void SetIPAddress(string addr)
        {
            String[] ipv4 = new String[4];
            int partnum = 0, startpos = 0;
            for (int i = 0; i < addr.Length; i++)
            {
                if (addr[i] == '.')
                {
                    String curpart = addr.Substring(startpos, i - startpos);
                    partnum++;
                    startpos = i+1;
                    switch (partnum)
                    {
                        case 1:
                            IPpart1 = curpart;
                            break;
                        case 2:
                            IPpart2 = curpart;
                            break;
                        case 3:
                            IPpart3 = curpart;
                            break;
                        case 4:
                            IPpart4 = curpart;
                            break;
                    }
                }
            }
        }

		#endregion Methods 
       }
}
