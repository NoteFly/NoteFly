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
using System.Drawing.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;
#if win32
using Microsoft.Win32;
#endif

namespace SimplePlainNote
{
    public partial class FrmSettings : Form
    {
		#region Fields (2) 

        private Notes notes;
        private xmlHandler xmlsettings;
#if win32
        private RegistryKey key;
#endif

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
            
            this.notes = notes;
            DrawCbxFonts();
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
                    MessageBox.Show("Error: Directory does not exist.\r\nPlease choice a valid directory.");                    
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
            throw new CustomExceptions("This is a crash test, to test if exceptions are thrown correctly.");
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
                MessageBox.Show("Invalid folder for saving notes folder.");                
                tabControlSettings.SelectedTab = tabGeneral;
            }
            else if (String.IsNullOrEmpty(cbxFontNoteContent.Text) == true)
            {
                MessageBox.Show("Select a font.");
                tabControlSettings.SelectedTab = this.tabAppearance;
            }
            else if ((numFontSize.Value < 4) || (numFontSize.Value > 128))
            {
                MessageBox.Show("Font size invalid. minmal 4pt maximal 128pt");
                tabControlSettings.SelectedTab = this.tabAppearance;
            }
            else if (cbxTextDirection.SelectedIndex > 1)
            {
                MessageBox.Show("Settings text direction unknow.");
            }
            else if ((chxSyntaxHighlightHTML.CheckState == CheckState.Indeterminate) || (chxSyntaxHighlightC.CheckState == CheckState.Indeterminate) ||
                (chxStartOnBootWindows.CheckState == CheckState.Indeterminate) || (chxConfirmExit.CheckState == CheckState.Indeterminate) || (chxLogErrors.CheckState == CheckState.Indeterminate))
            {
                MessageBox.Show("Not allowed.");
                tabControlSettings.SelectedTab = this.tabAppearance;
            }
            else if (tbTwitterUser.Text.Length > 16)
            {
                MessageBox.Show("Settings Twitter: username is too long.");
            }
            else if ((tbTwitterPass.Text.Length < 6) && (chxRememberTwPass.Checked == true))
            {
                MessageBox.Show("Settings Twitter: password is too short.");
            }
            else if (!tbDefaultEmail.Text.Contains("@"))
            {
                MessageBox.Show("Settings advance: default emailadres not valid.");
            }
            //everything looks okay            
            else
            {
                string oldnotesavepath = getNotesSavePath();
                if (tbNotesSavePath.Text != oldnotesavepath)
                {
                    MoveNotes(tbNotesSavePath.Text);
                }
                xmlsettings.WriteSettings(chxTransparecy.Checked, numProcTransparency.Value, cbxDefaultColor.SelectedIndex, cbxActionLeftClick.SelectedIndex, chxConfirmLink.Checked, cbxFontNoteContent.Text, numFontSize.Value, cbxTextDirection.SelectedIndex, tbNotesSavePath.Text, tbDefaultEmail.Text, chxSyntaxHighlightHTML.Checked, chxSyntaxHighlightC.Checked, chxConfirmExit.Checked, tbTwitterUser.Text, tbTwitterPass.Text, chxLogErrors.Checked);
                

#if win32
                key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (key != null)
                {
                    if (chxStartOnBootWindows.Checked == true)
                    {
                        try
                        {
                            key.SetValue("simpleplainnote", "\"" + Application.ExecutablePath + "\"");
                        }
                        catch (UnauthorizedAccessException exc)
                        {
                            MessageBox.Show("Error: no registery access." + exc.Message);
                        }
                    }
                    else if (chxStartOnBootWindows.Checked == false)
                    {
                        if (key.GetValue("simpleplainnote", null) != null)
                        {
                            key.DeleteValue("simpleplainnote", false);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Error: Run subkey in registery does not exist. Or it cannot be found.");
                }
#endif                
                notes.SetSettings();
                notes.UpdateAllFonts();
                this.Close();
            }                                        
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
    
        /*        
        private void chxSyntaxHighlightC_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chxSyntaxHighlightHTML_CheckedChanged(object sender, EventArgs e)
        {

        }
         */

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
                MessageBox.Show("Error: Current font not found.");                
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

        private bool getStatusStartlogin()
        {
            #if win32
            key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
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
            if (twpass == "")
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
                        File.Move(oldfile, newfile);
                    }
                    else
                    {
                        if (!errorshowed)
                        {
                            MessageBox.Show("Error: File " + id + ".xml already exist in new folder.");
                            errorshowed = true;
                        }
                    }
                }                
                id++; //bug fix #23.
            }
        }

        private void btnResetSettings_Click(object sender, EventArgs e)
        {
            DialogResult dlgres = MessageBox.Show("Are you sure, you want to reset your settings?", "reset settings?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //this.Hide();
            if (dlgres == DialogResult.Yes)
            {                
                xmlsettings.WriteSettings(true, 95, 0, 1, true, "Verdana", 10, 0, xmlsettings.AppDataFolder, "adres@domain.com", false, false, false, "", "", true);
                this.Close();
            }            
        }

		#endregion Methods 


    }
}
