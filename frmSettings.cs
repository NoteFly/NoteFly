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
    public partial class frmSettings : Form
    {
		#region Fields (2)
        private Notes notes;
        private RegistryKey key;
        private xmlHandler xmlsettings;
		#endregion Fields 

		#region Constructors (1) 

        public frmSettings(Notes notes, bool transparecy)
        {
            InitializeComponent();

            #if DEBUG
            btnCrash.Visible = true;
            #endif
            xmlsettings = new xmlHandler(true);
            
            //read setting and display them correctly.            
            cbxTransparecy.Checked = transparecy;
            cbxConfirmExit.Checked = getConfirmExit();
            numProcTransparency.Value = getTransparecylevel();            
            cbxDefaultColor.SelectedIndex = getDefaultColor();
            cbxActionLeftClick.SelectedIndex = getActionLeftClick();
            cbxConfirmLink.Checked = getAskUrl();
            tbNotesSavePath.Text = getNotesSavePath();
            tbTwitterUser.Text = getTwitterusername();
            tbTwitterPass.Text = getTwitterpassword();
            tbDefaultEmail.Text = getDefaultEmail();
            cbxStartOnBootWindows.Checked = getStatusStartlogin();
            
            this.notes = notes;
            DrawCbxFonts();
        }

		#endregion Constructors 

		#region Methods (11) 

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {        
            if (String.IsNullOrEmpty(cbxFontNoteContent.Text)==true)
            {
                MessageBox.Show("Select a font.");
                tabAppearance.Select();                
            }
            else if ((numFontSize.Value < 1) || (numFontSize.Value > 100))
            {
                MessageBox.Show("Font size invalid.");
            }
            else if (tbTwitterUser.Text.Length > 16)
            {
                MessageBox.Show("Settings Twitter: username is too long.");
                tabTwitter.Select();
            }
            else if ((tbTwitterPass.Text.Length < 6) && (cbxRememberTwPass.Checked == true))
            {
                MessageBox.Show("Settings Twitter: password is too short.");
                tabTwitter.Select();
            }
            else if (!Directory.Exists(tbNotesSavePath.Text))
            {
                MessageBox.Show("Settings advance: Invalid folder note save folder.");
                tabAdvance.Select();
            }
            else if (!tbDefaultEmail.Text.Contains("@"))
            {
                MessageBox.Show("Settings advance: default emailadres not valid.");
                tabAdvance.Select();
            }
            else if (!Directory.Exists(tbNotesSavePath.Text))
            {
                MessageBox.Show("Settings advance: note save path does not exist.");
                tabAdvance.Select();
            }
            //everything looks okay            
            else
            {               
                string oldnotesavepath = getNotesSavePath();
                if (tbNotesSavePath.Text != oldnotesavepath)
                {
                    MoveNotes(tbNotesSavePath.Text);
                }
                xmlsettings.WriteSettings(cbxTransparecy.Checked, numProcTransparency.Value, cbxDefaultColor.SelectedIndex, cbxActionLeftClick.SelectedIndex, cbxConfirmLink.Checked, cbxFontNoteContent.Text, numFontSize.Value, tbNotesSavePath.Text, tbDefaultEmail.Text, cbxSyntaxHighlight.Checked, cbxConfirmExit.Checked, tbTwitterUser.Text, tbTwitterPass.Text);

                #if win32
                key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (key != null)
                {
                    if (cbxStartOnBootWindows.Checked == true)
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
                    else if (cbxStartOnBootWindows.Checked == false)
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

        /// <summary>
        /// Enable password editbox on checking remember password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Enable nummericupdown control if transparecy is checked.        
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private int getActionLeftClick()
        {
            return xmlsettings.getXMLnodeAsInt("actionleftclick");            
        }

        private bool getAskUrl()
        {
            return xmlsettings.getXMLnodeAsBool("askurl");
        }

        private string getDefaultEmail()
        {
            return xmlsettings.getXMLnode("defaultemail");            
        }

        private string getNotesSavePath()
        {
            return xmlsettings.getXMLnode("notesavepath");
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
                    MessageBox.Show("Error: Directory does not exist. Please choice a valid directory.");                    
                }                                               
            }            
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
            }
        }

        private bool getConfirmExit()
        {
            return xmlsettings.getXMLnodeAsBool("confirmexit");
        }

        private void btnCrash_Click(object sender, EventArgs e)
        {
            #if DEBUG        
            throw new CustomExceptions("This is a crash test, to test if exceptions are thrown correctly.");
            #endif
        }


        

        #endregion Methods
    }
}
