using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace SimplePlainNote
{
    public partial class frmSettings : Form
    {
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString")]
        private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        private String inifile;

        public frmSettings()
        {
            InitializeComponent();

            string folder = System.Environment.GetEnvironmentVariable("APPDATA") + "\\.simpleplainnote\\";
            if (Directory.Exists(folder) == false) { Directory.CreateDirectory(folder); }
            inifile = System.Environment.GetEnvironmentVariable("APPDATA") + "\\.simpleplainnote\\settings.ini";

            cbxTransparecy.Checked = getTransparecy();
            cbxDefaultColor.SelectedIndex = getDefaultColor();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {            
            if (cbxTransparecy.Checked == true)
            {
                WriteIniValue("main", "transparecy", "1", inifile);
                WriteIniValue("main", "translevel", Convert.ToString(numProcTransparency.Value), inifile);
            }
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
                       
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool getTransparecy()
        {
            if (GetIniValue("main", "transparecy", inifile) == "1") return true;
            else return false;

        }

        private int getDefaultColor()
        {
            int defaultcol = Convert.ToInt32(GetIniValue("main", "defaultcolor", inifile));
            if (defaultcol >= 0 && defaultcol<5) return defaultcol;
            else return 0;
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


        public static string GetIniValue(string section, string key, string filename)
        {
            int chars = 256;
            StringBuilder buffer = new StringBuilder(chars);
            string sDefault = "";
            if (GetPrivateProfileString(section, key, sDefault,
              buffer, chars, filename) != 0)
            {
                return buffer.ToString();
            }
            else
            {
                return null;
            }
        }
        public static bool WriteIniValue(string section, string key, string value, string filename)
        {
            return WritePrivateProfileString(section, key, value, filename);
        }

    }
}
