/* Copyright (C) 2009
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
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;

namespace NoteFly
{
    partial class FrmAbout : Form
    {
        private int curletter = -1;

        public FrmAbout()
        {
            InitializeComponent();
            this.Text = "About";
            this.lblProductName.Text = TrayIcon.AssemblyTitle;
            this.lblVersion.Text = String.Format("Version {0}", TrayIcon.AssemblyVersion);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linklblWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://code.google.com/p/simpleplainnote/");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {            
            if (curletter < this.richTextBox1.Text.Length)
            {
                curletter++;
            } else { 
                curletter = 0;
            }
            if (curletter > 2)
            {
                this.richTextBox1.Select(curletter-3, 1);
                this.richTextBox1.SelectionColor = Color.Red;
            }
            else if (curletter <= 2)
            {
                this.richTextBox1.Select(richTextBox1.Text.Length - curletter, 1);
                this.richTextBox1.SelectionColor = Color.Red;
            }
            this.richTextBox1.Select(curletter, 1);
            this.richTextBox1.SelectionColor = Color.Green;
            this.richTextBox1.Select(0, 0);
        }
    }
}
