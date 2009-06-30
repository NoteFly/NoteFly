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
//using XMLReadWrite.Classes;

namespace SimplePlainNote
{
    public partial class frmSettings : Form
    {
        #region datavelde
        private XmlTextReader objXmlTextReader;
        private String settingsfile;
        #endregion

        #region constructor
        public frmSettings()
        {
            InitializeComponent();

            string appdatafolder = System.Environment.GetEnvironmentVariable("APPDATA") + "\\.simpleplainnote\\";
            if (Directory.Exists(appdatafolder) == false) { Directory.CreateDirectory(appdatafolder); }
            settingsfile = appdatafolder + @"settings.xml";
            if (File.Exists(settingsfile) == false)
            {
                WriteSettings(true, 95, 0);                
            }
            //validate xml file.
            clsSValidator objclsSValidator = new clsSValidator(settingsfile, Application.StartupPath + @"\settings.xsd");
            if (objclsSValidator.ValidateXMLFile()) return;
            //read setting and display set them correctly.
            objXmlTextReader = new XmlTextReader(settingsfile);
            cbxTransparecy.Checked = getTransparecy();
            numProcTransparency.Value = getTransparecylevel();
            cbxDefaultColor.SelectedIndex = getDefaultColor();            
            objXmlTextReader.Close();
        }
        #endregion

        #region methoden
        private void btnOK_Click(object sender, EventArgs e)
        {
            WriteSettings(cbxTransparecy.Checked, numProcTransparency.Value, cbxDefaultColor.SelectedIndex);

            this.Close();
        }


        private void WriteSettings(bool transparecy, decimal transparecylevel, int numcolor)
        {
            try
            {
                XmlTextWriter objXmlTextWriter = new XmlTextWriter(settingsfile, null);
                objXmlTextWriter.Formatting = Formatting.Indented;

                objXmlTextWriter.WriteStartDocument();
                objXmlTextWriter.WriteStartElement("settings");
                if (transparecy == true)
                {
                    objXmlTextWriter.WriteStartElement("transparecy");
                        objXmlTextWriter.WriteString("1");
                    objXmlTextWriter.WriteEndElement();
                }
                else
                {
                    objXmlTextWriter.WriteStartElement("transparecy");
                        objXmlTextWriter.WriteString("0");
                    objXmlTextWriter.WriteEndElement();
                }
                objXmlTextWriter.WriteStartElement("transparecylevel");
                    objXmlTextWriter.WriteString(Convert.ToString(transparecylevel));
                objXmlTextWriter.WriteEndElement();
                               
                if ((numcolor < 0) || (numcolor>cbxDefaultColor.Items.Count)) { throw new Exception("Impossible selection"); }
                
                objXmlTextWriter.WriteStartElement("defaultcolor");
                    objXmlTextWriter.WriteString(Convert.ToString(numcolor));
                objXmlTextWriter.WriteEndElement();

                objXmlTextWriter.WriteEndElement();
                objXmlTextWriter.WriteEndDocument();

                objXmlTextWriter.Flush();
                objXmlTextWriter.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private bool getTransparecy()
        {
            if (getXMLnode("transparecy") == "1") return true;
            else return false;
        }

        private Decimal getTransparecylevel()
        {

            Decimal transparecylvl = Convert.ToDecimal(getXMLnode("transparecylevel"));
            if ((transparecylvl < 1) || (transparecylvl > 100)) { MessageBox.Show("transparecylevel out of range."); return 95; }
            else return transparecylvl;
        }

        private int getDefaultColor()
        {
            return getXMLnodeAsInt("defaultcolor");
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

        private String getXMLnode(string nodename)
        {
            while (objXmlTextReader.Read())
            {
                if (objXmlTextReader.Name == nodename)
                {
                    string s = objXmlTextReader.ReadElementContentAsString();
                    return s;
                }
            }
            //error
            return null;
        }

        private int getXMLnodeAsInt(string nodename)
        {
            while (objXmlTextReader.Read())
            {
                if (objXmlTextReader.Name == nodename)
                {                    
                    try
                    {
                        int n = objXmlTextReader.ReadElementContentAsInt();
                        return n;
                    }
                    catch (Exception)
                    {
                        //error
                        return 0;
                    }
                }
            }
            //error
            return 0;
        }       


        #endregion
    }
}
