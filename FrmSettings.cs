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
using System.Drawing;
using System.Windows.Forms;
using System.IO;

#if win32
using Microsoft.Win32;

#endif

namespace NoteFly
{
    public partial class FrmSettings : Form
    {
		#region Fields (3) 

        private IPTextBox ipproxy;
        private Notes notes;
        private xmlHandler xmlsettings;

		#endregion Fields 

		#region Constructors (1) 

        public FrmSettings(Notes notes, bool transparecy)
        {
            InitializeComponent();
            xmlsettings = new xmlHandler(true);
            //read setting and display them correctly.
            chxTransparecy.Checked = transparecy;
            chxConfirmExit.Checked = getConfirmExit();
            numProcTransparency.Value = getTransparecylevel();
            cbxDefaultColor.SelectedIndex = getDefaultColor();
            cbxActionLeftClick.SelectedIndex = getActionLeftClick();
            chxConfirmLink.Checked = getAskUrl();
            tbNotesSavePath.Text = getNotesSavePath();
            tbTwitterUser.Text = getTwitterusername();
            tbTwitterPass.Text = getTwitterpassword();
            tbDefaultEmail.Text = getDefaultEmail();
            chxStartOnBootWindows.Checked = getStatusStartlogin();
            chxSyntaxHighlightHTML.Checked = getHighlightHTML();
            chxSyntaxHighlightC.Checked = getHighlightC();
            cbxTextDirection.SelectedIndex = getTextDirection();
            chxLogInfo.Checked = getLogDebugInfo();
            chxUseProxy.Checked = getUseProxy();

            if (String.IsNullOrEmpty(tbDefaultEmail.Text))
            {
                tbDefaultEmail.Enabled = false;
                cbxDefaultEmailToBlank.Checked = true;
            }
            else
            {
                tbDefaultEmail.Enabled = true;
                cbxDefaultEmailToBlank.Checked = false;
            }
            
            this.notes = notes;
            DrawCbxFonts();

            ipproxy = new IPTextBox();
            ipproxy.Location = new Point(24, 50);
            this.tabNetwerk.Controls.Add(ipproxy);
            if (getUseProxy())
            {
                ipproxy.Enabled = true;
            }
#if DEBUG
            btnCrash.Visible = true;
#endif
        }

		#endregion Constructors 

		#region Methods (24) 

		// Private Methods (24) 

        private void btnBrowse_Click(object sender, EventArgs e)
        {            
            DialogResult dlgresult = folderBrowserDialog1.ShowDialog();
            if (dlgresult == DialogResult.OK)
            {
                string newpathsavenotes = folderBrowserDialog1.SelectedPath;
                
                if (Directory.Exists(newpathsavenotes))
                {
                    this.tbNotesSavePath.Text = folderBrowserDialog1.SelectedPath;
                }
                else
                {
                    String dirnotexist = "Directory does not exist.\r\nPlease choice a valid directory.";
                    MessageBox.Show(dirnotexist);
                    Log.write(LogType.info, dirnotexist);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCrash_Click(object sender, EventArgs e)
        {
            #if DEBUG
            throw new CustomException("This is a crash test, to test if exceptions are thrown correctly.");
            #endif
        }

        /// <summary>
        /// Check the form input. If everything is okay
        /// call xmlHandler class to save the xml setting file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(tbNotesSavePath.Text))
            {
                String invalidfoldersavenote = "Invalid folder for saving notes folder.";
                MessageBox.Show(invalidfoldersavenote);
                tabControlSettings.SelectedTab = tabGeneral;
                Log.write(LogType.info, invalidfoldersavenote);
            }
            else if (String.IsNullOrEmpty(cbxFontNoteContent.Text) == true)
            {
                String nofont = "Select a font.";
                MessageBox.Show(nofont);
                tabControlSettings.SelectedTab = this.tabAppearance;
                Log.write(LogType.info, nofont);
            }
            else if ((numFontSize.Value < 4) || (numFontSize.Value > 128))
            {
                String invalidfontsize = "Font size invalid. minmal 4pt maximal 128pt";
                MessageBox.Show(invalidfontsize);
                tabControlSettings.SelectedTab = this.tabAppearance;
                Log.write(LogType.info, invalidfontsize);
            }
            else if (cbxTextDirection.SelectedIndex > 1)
            {
                String noknowtextdir = "Settings text direction unknow.";
                MessageBox.Show(noknowtextdir);
                tabControlSettings.SelectedTab = this.tabAppearance;
                Log.write(LogType.error, noknowtextdir);
            }
            else if ((chxSyntaxHighlightHTML.CheckState == CheckState.Indeterminate) || (chxSyntaxHighlightC.CheckState == CheckState.Indeterminate) ||
                (chxStartOnBootWindows.CheckState == CheckState.Indeterminate) || (chxConfirmExit.CheckState == CheckState.Indeterminate) || (chxLogErrors.CheckState == CheckState.Indeterminate) || (chxLogInfo.CheckState == CheckState.Indeterminate))
            {
                String notallowcheckstate = "checkstate not allowed.";
                MessageBox.Show(notallowcheckstate);
                tabControlSettings.SelectedTab = this.tabAppearance;
                Log.write(LogType.error, notallowcheckstate);
            }
            else if (tbTwitterUser.Text.Length > 16)
            {
                String twnametoolong = "Settings Twitter: username is too long.";
                MessageBox.Show(twnametoolong);
                Log.write(LogType.error, twnametoolong);
            }
            else if ((tbTwitterPass.Text.Length < 6) && (chxRememberTwPass.Checked == true))
            {
                String twpaswtooshort = "Settings Twitter: password is too short.";
                MessageBox.Show(twpaswtooshort);
                Log.write(LogType.error, twpaswtooshort);
            }
            else if ((!tbDefaultEmail.Text.Contains("@") || !tbDefaultEmail.Text.Contains(".")) && (!cbxDefaultEmailToBlank.Checked))
            {
                String emailnotvalid = "Settings advance: default emailadres not valid.";
                MessageBox.Show(emailnotvalid);
                Log.write(LogType.error, emailnotvalid);
            }
            //everything looks okay 
            else
            {
                string oldnotesavepath = getNotesSavePath();
                if (tbNotesSavePath.Text != oldnotesavepath)
                {
                    MoveNotes(tbNotesSavePath.Text);
                }
                xmlsettings.WriteSettings(chxTransparecy.Checked, numProcTransparency.Value, cbxDefaultColor.SelectedIndex, cbxActionLeftClick.SelectedIndex, chxConfirmLink.Checked, cbxFontNoteContent.Text, 
                    numFontSize.Value, cbxTextDirection.SelectedIndex, tbNotesSavePath.Text, tbDefaultEmail.Text, chxSyntaxHighlightHTML.Checked, chxSyntaxHighlightC.Checked, chxConfirmExit.Checked, tbTwitterUser.Text,
                    tbTwitterPass.Text, chxLogErrors.Checked, chxLogInfo.Checked, chxUseProxy.Checked, ipproxy.GetIPAddress());
#if win32
                RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (key != null)
                {
                    if (chxStartOnBootWindows.Checked == true)
                    {
                        try
                        {
                            key.SetValue(TrayIcon.AssemblyTitle, "\"" + Application.ExecutablePath + "\"");
                        }
                        catch (UnauthorizedAccessException unauthexc)
                        {
                            MessageBox.Show(unauthexc.Message);
                            Log.write(LogType.exception, unauthexc.Message);
                        }
                        catch (Exception exc)
                        {
                            throw new CustomException(exc.Message + " " + exc.StackTrace);
                        }
                    }
                    else if (chxStartOnBootWindows.Checked == false)
                    {
                        if (key.GetValue(TrayIcon.AssemblyTitle, null) != null)
                        {
                            key.DeleteValue(TrayIcon.AssemblyTitle, false);
                        }
                    }
                }
                else
                {
                    String regkeynotexistfound = "Run subkey in registery does not exist. Or it cannot be found.";
                    MessageBox.Show(regkeynotexistfound);
                    Log.write(LogType.error, regkeynotexistfound);
                }
#endif
                notes.SetSettings();
                notes.UpdateAllFonts();
                Log.write(LogType.info, "settings updated");
                this.Close();
            }
        }

        private void btnResetSettings_Click(object sender, EventArgs e)
        {
            DialogResult dlgres = MessageBox.Show("Are you sure, you want to reset your settings?", "reset settings?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);            
            if (dlgres == DialogResult.Yes)
            {
                String settingsfile = Path.Combine(xmlsettings.AppDataFolder, "settings.xml");
                if (File.Exists(settingsfile))
                {
                    File.Delete(settingsfile);
                    xmlsettings = new xmlHandler(true);
                }
                else
                {
                    throw new Exception("Could not find settings file in application directory.");
                }
                this.Close();
            }
        }

        private void cbxDefaultEmailToBlank_CheckedChanged(object sender, EventArgs e)
        {
            tbDefaultEmail.Enabled = !cbxDefaultEmailToBlank.Checked;
            if (cbxDefaultEmailToBlank.Checked) { tbDefaultEmail.Text = ""; }
        }

        private void cbxProxy_Click(object sender, EventArgs e)
        {
            ipproxy.Enabled = chxUseProxy.Checked;
        }

        /// <summary>
        /// Enable password editbox on checking remember password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRememberTwPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chxRememberTwPass.Checked == true)
            {
                tbTwitterPass.Enabled = true;
            }
            else
            {
                tbTwitterPass.Enabled = false;
                tbTwitterPass.Text = "";
            }
        }

        /// <summary>
        /// Enable nummericupdown control if transparecy is checked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxTransparecy_CheckedChanged(object sender, EventArgs e)
        {
            if (chxTransparecy.Checked == false)
            {
                numProcTransparency.Enabled = false;
            }
            else if (chxTransparecy.Checked == true)
            {
                numProcTransparency.Enabled = true;
            }
        }

        /// <summary>
        /// Fill combobox list with fonts
        /// </summary>
        private void DrawCbxFonts()
        {
            foreach (FontFamily oneFontFamily in FontFamily.Families)
            {
                cbxFontNoteContent.Items.Add(oneFontFamily.Name);
            }
            string curfont = xmlsettings.getXMLnode("fontcontent");
            if (String.IsNullOrEmpty(curfont))
            {
                String fontnotfound = "Error: Current font not found.";
                MessageBox.Show(fontnotfound);
                Log.write(LogType.info, fontnotfound);
            }
            else
            {
                cbxFontNoteContent.Text = curfont;
            }
        }

        private int getActionLeftClick()
        {
            return xmlsettings.getXMLnodeAsInt("actionleftclick");
        }

        private bool getAskUrl()
        {
            return xmlsettings.getXMLnodeAsBool("askurl");
        }

        private bool getConfirmExit()
        {
            return xmlsettings.getXMLnodeAsBool("confirmexit");
        }

        private int getDefaultColor()
        {
            return xmlsettings.getXMLnodeAsInt("defaultcolor");
        }

        private string getDefaultEmail()
        {
            return xmlsettings.getXMLnode("defaultemail");
        }

        private bool getHighlightC()
        {
            return xmlsettings.getXMLnodeAsBool("highlightC");
        }

        private bool getHighlightHTML()
        {
            return xmlsettings.getXMLnodeAsBool("highlightHTML");
        }

        private string getNotesSavePath()
        {
            return xmlsettings.getXMLnode("notesavepath");
        }

        private bool getUseProxy()
        {
            return xmlsettings.getXMLnodeAsBool("useproxy");
        }

        private string getProxyAddr()
        {
            return xmlsettings.getXMLnode("proxyaddr");
        }

        private bool getStatusStartlogin()
        {
            #if win32
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (key != null)
            {
                if (key.GetValue("simpleplainnote", null)!=null)
                {
                    return true;
                } 
                else 
                {
                    return false;
                }
            }
            #endif
            return false;
        }

        private int getTextDirection()
        {
            return xmlsettings.getXMLnodeAsInt("textdirection");
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
            if (String.IsNullOrEmpty(twpass))
            {
                chxRememberTwPass.Checked = false;
                tbTwitterPass.Enabled = false;
            }
            return twpass;
        }

        private string getTwitterusername()
        {
            return xmlsettings.getXMLnode("twitteruser");
        }

        private Boolean getLogDebugInfo()
        {
            return xmlsettings.getXMLnodeAsBool("loginfo");
        }

        /// <summary>
        /// Move note files.
        /// </summary>
        /// <param name="newpathsavenotes"></param>
        private void MoveNotes(string newpathsavenotes)
        {            
            bool errorshowed = false;
            string oldpathsavenotes = getNotesSavePath();
            int id = 1;
            while (File.Exists(Path.Combine(oldpathsavenotes, id + ".xml")) == true)
            {
                if (Directory.Exists(newpathsavenotes))
                {
                    string oldfile = Path.Combine(oldpathsavenotes, id + ".xml");
                    string newfile = Path.Combine(newpathsavenotes, id + ".xml");
                    if (!File.Exists(newfile))
                    {
                        FileInfo fi = new FileInfo(oldfile);
                        if (fi.Attributes != FileAttributes.System)
                        {
                            File.Move(oldfile, newfile);
                        }
                        else throw new CustomException("File is marked as system file. Did not move.");
                    }
                    else
                    {
                        if (!errorshowed)
                        {
                            String fileexist = "File " + id + ".xml already exist in new folder.";
                            MessageBox.Show(fileexist);
                            errorshowed = true;
                            Log.write(LogType.error, fileexist);
                        }
                    }
                }
                id++;
            }
        }

		#endregion Methods 
    }
}
