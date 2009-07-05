using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;

namespace SimplePlainNote
{
    public partial class frmSettings : Form
    {
        #region datavelde                
        private xmlHandler xmlsettings;
        #endregion

        #region constructor
        public frmSettings()
        {
            InitializeComponent();

            xmlsettings = new xmlHandler(true, "settings.xml");

            //read setting and display them correctly.            
            cbxTransparecy.Checked = getTransparecy();
            numProcTransparency.Value = getTransparecylevel();
            cbxDefaultColor.SelectedIndex = getDefaultColor();
            tbTwitterUser.Text = getTwitterusername();
            tbTwitterPass.Text = getTwitterpassword();
        }
        #endregion

        #region methoden
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (xmlsettings.WriteSettings(cbxTransparecy.Checked, 
                numProcTransparency.Value,
                cbxDefaultColor.SelectedIndex,
                tbTwitterUser.Text,
                tbTwitterPass.Text
                )==false)
            {
                MessageBox.Show("Error writing settings.");
            }

            this.Close();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private bool getTransparecy()
        {
            if (xmlsettings.getXMLnode("transparecy") == "1") return true;
            else return false;
        }

        private Decimal getTransparecylevel()
        {

            Decimal transparecylvl = Convert.ToDecimal(xmlsettings.getXMLnode("transparecylevel"));
            if ((transparecylvl < 1) || (transparecylvl > 100)) { MessageBox.Show("transparecylevel out of range."); return 95; }
            else return transparecylvl;
        }

        private int getDefaultColor()
        {
            return xmlsettings.getXMLnodeAsInt("defaultcolor");
        }

        private string getTwitterusername()
        {
            return xmlsettings.getXMLnode("twitteruser");
        }

        private string getTwitterpassword()
        {
            return xmlsettings.getXMLnode("twitterpass"); ;
        }

        private void cbxTransparecy_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxTransparecy.Checked == false)
            {
                numProcTransparency.Enabled = false;
            }
            else if (cbxTransparecy.Checked == true)
            {
                numProcTransparency.Enabled = true;
            }
        }




        #endregion
    }
}
