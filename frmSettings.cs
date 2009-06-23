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
                // xml file does not exist, let's create one with default settings..

                try
                {
                    string sStartupPath = Application.StartupPath;
                    XmlTextWriter objXmlTextWriter = new XmlTextWriter(settingsfile, null);
                    objXmlTextWriter.Formatting = Formatting.Indented;
                    objXmlTextWriter.WriteStartDocument();
                    objXmlTextWriter.WriteStartElement("settings");

                        objXmlTextWriter.WriteStartElement("transparecy");                            
                                objXmlTextWriter.WriteString("1");
                        objXmlTextWriter.WriteEndElement();

                        objXmlTextWriter.WriteStartElement("transparecylevel");
                            objXmlTextWriter.WriteString("95");
                        objXmlTextWriter.WriteEndElement();

                        objXmlTextWriter.WriteStartElement("defaultcolor");
                            objXmlTextWriter.WriteString("0");
                        objXmlTextWriter.WriteEndElement();

                    objXmlTextWriter.WriteEndElement();
                    objXmlTextWriter.WriteEndDocument();
                    objXmlTextWriter.Flush();
                    objXmlTextWriter.Close();                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            //validate xml file.
            clsSValidator objclsSValidator = new clsSValidator(settingsfile, Application.StartupPath + @"\settings.xsd");

            if (objclsSValidator.ValidateXMLFile()) return;            

            objXmlTextReader = new XmlTextReader(settingsfile);

            cbxTransparecy.Checked = getTransparecy();
            cbxDefaultColor.SelectedIndex = getDefaultColor();
        }
        #endregion

        #region methoden
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cbxTransparecy.Checked == true)
            {
                //WriteIniValue("main", "transparecy", "1", inifile);
                //WriteIniValue("main", "translevel", Convert.ToString(numProcTransparency.Value), inifile);
            }
            /*
            if (cbxDefaultColor.SelectedIndex == 0) WriteIniValue("main", "defaultcolor", "0", inifile);
            else if (cbxDefaultColor.SelectedIndex == 1) WriteIniValue("main", "defaultcolor", "1", inifile);
            else if (cbxDefaultColor.SelectedIndex == 2) WriteIniValue("main", "defaultcolor", "2", inifile);
            else if (cbxDefaultColor.SelectedIndex == 3) WriteIniValue("main", "defaultcolor", "3", inifile);
            else if (cbxDefaultColor.SelectedIndex == 4) WriteIniValue("main", "defaultcolor", "4", inifile);
            else
            {
                MessageBox.Show("Error no default color selected.");
                return;
            }
             */
            this.Close();
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

        private int getTransparecylevel()
        {
            return getXMLnodeAsInt("transparecylevel");
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

                    return objXmlTextReader.Value;
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
                    string waarde = objXmlTextReader.Value;
                    try
                    {
                        int n = Convert.ToInt32(waarde);
                        return n;
                    }
                    catch (Exception)
                    {
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
