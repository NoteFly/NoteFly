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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;
using IWshRuntimeLibrary;

namespace SimplePlainNote
{
    public partial class frmSettings : Form
    {
		#region Fields (2) 
        private WshShellClass WshShell;
        private xmlHandler xmlsettings;
		#endregion Fields 

		#region Constructors (1) 

        public frmSettings()
        {
            InitializeComponent();

            xmlsettings = new xmlHandler(true);

            //read setting and display them correctly.            
            cbxTransparecy.Checked = getTransparecy();
            numProcTransparency.Value = getTransparecylevel();
            cbxDefaultColor.SelectedIndex = getDefaultColor();
            tbNotesSavePath.Text = getNotesSavePath();
            tbTwitterUser.Text = getTwitterusername();
            tbTwitterPass.Text = getTwitterpassword();
            tbDefaultEmail.Text = getDefaultEmail();
            cbxStartOnBootWindows.Checked = getStatusStartlogin();
        }

		#endregion Constructors 

		#region Methods (11) 

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(tbNotesSavePath.Text))
            {
                MessageBox.Show("Settings advance: Invalide folder note save folder.");
                tabAdvance.Select();
                return;
            }
            else if (!tbDefaultEmail.Text.Contains("@"))
            {
                MessageBox.Show("Settings advance: default email adres not valide.");
                tabAdvance.Select();
                return;
            }            
            else if (tbTwitterUser.Text.Length > 16)
            {
                MessageBox.Show("Settings Twitter: username is too long.");
                tabTwitter.Select();
                return;
            }
            else if ((tbTwitterPass.Text.Length < 6) && (cbxRememberTwPass.Checked == true))
            {
                MessageBox.Show("Settings Twitter: password is too short.");
                tabTwitter.Select();
                return;
            }
            //everything looks okay            
            else
            {
                xmlsettings.WriteSettings(cbxTransparecy.Checked,                     
                    numProcTransparency.Value,
                    cbxDefaultColor.SelectedIndex,
                    tbNotesSavePath.Text,
                    tbDefaultEmail.Text,
                    cbxSyntaxHighlight.Checked,
                    tbTwitterUser.Text,
                    tbTwitterPass.Text);

                string startmenustartup = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

                if (cbxStartOnBootWindows.Checked == true)
                {                    
                    if (Directory.Exists(startmenustartup))
                    {                        
                        try
                        {
                            WshShell = new WshShellClass();
                            IWshRuntimeLibrary.IWshShortcut MyShortcut;

                            MyShortcut = (IWshRuntimeLibrary.IWshShortcut)WshShell.CreateShortcut(startmenustartup + @"\simpleplainnote.lnk");

                            MyShortcut.TargetPath = Application.ExecutablePath;

                            MyShortcut.Description = "launch simpleplainnote on logon.";
                            MyShortcut.IconLocation = Application.ExecutablePath + ",0";
                            MyShortcut.Save();
                        }
                        catch (UnauthorizedAccessException)
                        {
                            MessageBox.Show("Need admin credentials to store shortcut.");                            
                        }

                    }
                    else
                    {
                        MessageBox.Show("Error: Folder not found: " + startmenustartup.ToString());
                    }
                }
                else if (cbxStartOnBootWindows.Checked == false)
                {
                    if (Directory.Exists(startmenustartup))
                    {
                        if (System.IO.File.Exists(startmenustartup + @"\simpleplainnote.lnk"))
                        {
                            FileInfo checkshortcut = new FileInfo(startmenustartup + @"\simpleplainnote.lnk");
                            if (checkshortcut.Length > 4096) { return; }
                            try
                            {
                                System.IO.File.Delete("simpleplainnote.lnk");
                            }
                            catch (UnauthorizedAccessException)
                            {
                                MessageBox.Show("Need admin credentials to delete shortcut.");
                            }
                        }
                    }
                }
            }
            this.Close();
        }

        private void cbxRememberTwPass_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxRememberTwPass.Checked == true)
            {
                tbTwitterPass.Enabled = true;
            }
            else
            {
                tbTwitterPass.Enabled = false;
                tbTwitterPass.Text = "";
            }
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

        private int getDefaultColor()
        {
            return xmlsettings.getXMLnodeAsInt("defaultcolor");
        }

        private string getDefaultEmail()
        {
            return xmlsettings.getXMLnode("defaultemail");            
        }

        private string getNotesSavePath()
        {
            return xmlsettings.getXMLnode("notesavepath");
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

        private string getTwitterpassword()
        {
            string twpass = xmlsettings.getXMLnode("twitterpass");
            if (twpass == "")
            {
                cbxRememberTwPass.Checked = false;
                tbTwitterPass.Enabled = false;                
            }
            return twpass;
        }

        private string getTwitterusername()
        {
            return xmlsettings.getXMLnode("twitteruser");
        }

        private bool getStatusStartlogin()
        {
            string startmenustartup = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
            if (System.IO.File.Exists(startmenustartup + @"\simpleplainnote.lnk"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		#endregion Methods 
    }
}
