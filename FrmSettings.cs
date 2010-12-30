//-----------------------------------------------------------------------
// <copyright file="FrmSettings.cs" company="GNU">
//  NoteFly a note application.
//  Copyright (C) 2010  Tom
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
// </copyright>
//-----------------------------------------------------------------------
#define windows //platform can be: windows, linux, macos

namespace NoteFly
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
#if windows
    using Microsoft.Win32;
#endif

    /// <summary>
    /// Setting window.
    /// </summary>
    public partial class FrmSettings : Form
    {
        #region Fields (1)

        private Notes notes;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the FrmSettings class.
        /// </summary>
        /// <param name="notes">The notes class.</param>
        public FrmSettings(Notes notes)
        {
            this.InitializeComponent();

            //read setting and display them correctly.
            this.chxLogErrors.Checked = Settings.ProgramLogError;
            this.chxLogDebug.Checked = Settings.ProgramLogInfo;

            this.chxConfirmExit.Checked = Settings.ConfirmExit;
            this.chxTransparecy.Checked = Settings.NotesTransparencyEnabled;
            this.chxConfirmDeleteNote.Checked = Settings.ConfirmDeletenote;

            this.chxHighlightHyperlinks.Checked = Settings.HighlightHyperlinks;
            this.chxConfirmLink.Checked = Settings.ConfirmLinkclick;
            this.chxHighlightHTML.Checked = Settings.HighlightHTML;

            this.chxUseProxy.Checked = Settings.NetworkProxyEnabled;
            this.iptbProxyAddress.Enabled = Settings.NetworkProxyEnabled;

            this.numProcTransparency.Value = Settings.NotesTransparencyLevel;
            this.cbxDefaultColor.SelectedIndex = Settings.NotesDefaultSkinnr; //-1
            this.tbNotesSavePath.Text = Settings.NotesSavepath;
            this.cbxTextDirection.SelectedIndex = Settings.FontTextdirection;
            this.cbxActionLeftClick.SelectedIndex = Settings.TrayiconLeftclickaction;

            this.chxSocialEmailEnabled.Checked = Settings.SocialEmailEnabled;
            this.tbDefaultEmail.Text = Settings.SocialEmailDefaultadres;
            if (String.IsNullOrEmpty(Settings.SocialEmailDefaultadres))
            {
                this.chxSocialEmailDefaultaddressBlank.Checked = true;
            }
            this.tbTwitterUser.Text = Settings.SocialTwitterUsername;
            //this.tbTwitterPass.Text = Settings.SocialTwitterpassword;
            //if (String.IsNullOrEmpty(Settings.SocialTwitterpassword))
            //{
            //    this.chxRememberTwPass.Checked = true;
            //}
            this.chxSocialFacebookEnabled.Checked = Settings.SocialFacebookEnabled;
            this.chxSaveFBSession.Checked = Settings.SocialFacebookSavesession;

            this.numTimeout.Value = Settings.NetworkConnectionTimeout;
            this.chxUseProxy.Checked = Settings.NetworkProxyEnabled;
            this.iptbProxyAddress.SetIPAddress(Settings.NetworkProxyAddress);

#if windows
            this.chxStartOnBootWindows.Checked = this.GetStatusStartlogin();
#endif

            this.DrawCbxFonts();
#if DEBUG
            this.btnCrash.Visible = true;
#endif
            this.notes = notes;
        }

        #endregion Constructors

        #region Methods (24)

        // Private Methods (24) 

        /// <summary>
        /// User want to browse for notes save path.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments</param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult dlgresult = this.folderBrowserDialog1.ShowDialog();
            if (dlgresult == DialogResult.OK)
            {
                string newpathsavenotes = this.folderBrowserDialog1.SelectedPath;

                if (Directory.Exists(newpathsavenotes))
                {
                    this.tbNotesSavePath.Text = this.folderBrowserDialog1.SelectedPath;
                }
                else
                {
                    const string dirnotexist = "Directory does not exist.\r\nPlease choice a valid directory.";
                    Log.Write(LogType.info, dirnotexist);
                    MessageBox.Show(dirnotexist, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Cancel button pressed.
        /// Don't save any change made.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Test method to see how custom exceptions are handled.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event arguments</param>
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
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(this.tbNotesSavePath.Text))
            {
                Log.Write(LogType.info, NoteFly.Properties.Resources.settings_invalidfoldersavenote);
                MessageBox.Show(NoteFly.Properties.Resources.settings_invalidfoldersavenote);
                this.tabControlSettings.SelectedTab = this.tabGeneral;
            }
            else if (String.IsNullOrEmpty(this.cbxFontNoteContent.Text) == true)
            {
                Log.Write(LogType.info, NoteFly.Properties.Resources.settings_nofont);
                MessageBox.Show(NoteFly.Properties.Resources.settings_nofont);
                this.tabControlSettings.SelectedTab = this.tabAppearance;
            }
            else if ((this.numFontSize.Value < 4) || (this.numFontSize.Value > 128))
            {
                Log.Write(LogType.info, NoteFly.Properties.Resources.settings_invalidfontsize);
                MessageBox.Show(NoteFly.Properties.Resources.settings_invalidfontsize);
                this.tabControlSettings.SelectedTab = this.tabAppearance;
            }
            else if (this.cbxTextDirection.SelectedIndex > 1)
            {
                Log.Write(LogType.error, NoteFly.Properties.Resources.settings_noknowtextdir);
                MessageBox.Show(NoteFly.Properties.Resources.settings_noknowtextdir);
                this.tabControlSettings.SelectedTab = this.tabAppearance;
            }
            else if ((this.chxHighlightHTML.CheckState == CheckState.Indeterminate)
#if windows
 || (this.chxStartOnBootWindows.CheckState == CheckState.Indeterminate)
#endif
 || (this.chxConfirmExit.CheckState == CheckState.Indeterminate) || (this.chxLogErrors.CheckState == CheckState.Indeterminate) || (this.chxLogDebug.CheckState == CheckState.Indeterminate))
            {
                Log.Write(LogType.error, NoteFly.Properties.Resources.settings_notallowcheckstate);
                MessageBox.Show(NoteFly.Properties.Resources.settings_notallowcheckstate);
                this.tabControlSettings.SelectedTab = this.tabAppearance;
            }

            else if (this.tbTwitterUser.Text.Length > 16)
            {
                Log.Write(LogType.error, NoteFly.Properties.Resources.settings_twitternametoolong);
                MessageBox.Show(NoteFly.Properties.Resources.settings_twitternametoolong);
                this.tabControlSettings.SelectedTab = this.tabSocialNetworks;
            }
            /*
            else if ((this.tbTwitterPass.Text.Length < 6) && (this.chxRememberTwPass.Checked == true))
            {
                Log.Write(LogType.error, NoteFly.Properties.Resources.settings_twitterpaswtooshort);
                MessageBox.Show(NoteFly.Properties.Resources.settings_twitterpaswtooshort);
                this.tabControlSettings.SelectedTab = this.tabSocialNetworks;
            }
            */
            else if ((!this.tbDefaultEmail.Text.Contains("@") || !this.tbDefaultEmail.Text.Contains(".")) && (!this.chxSocialEmailDefaultaddressBlank.Checked))
            {
                Log.Write(LogType.error, NoteFly.Properties.Resources.settings_emailnotvalid);
                MessageBox.Show(NoteFly.Properties.Resources.settings_emailnotvalid);
                this.tabControlSettings.SelectedTab = this.tabAdvance;
            }
            else
            {
                //everything looks okay 
                string oldnotesavepath = Settings.NotesSavepath;
                if (this.tbNotesSavePath.Text != oldnotesavepath)
                {
                    this.MoveNotes(oldnotesavepath, this.tbNotesSavePath.Text); //TODO: seperate thread
                }

                Settings.ConfirmDeletenote = chxConfirmDeleteNote.Checked;
                Settings.ConfirmExit = chxConfirmExit.Checked;
                Settings.ConfirmLinkclick = chxConfirmLink.Checked;

                Settings.HighlightHyperlinks = chxHighlightHyperlinks.Checked;
                Settings.HighlightHTML = chxHighlightHTML.Checked;
                Settings.HighlightPHP = chxHighlightPHP.Checked;
                Settings.HighlightSQL = chxHighlightSQL.Checked;

                Settings.NotesTransparencyEnabled = chxTransparecy.Checked;
                Settings.NetworkProxyEnabled = chxUseProxy.Checked;

                Settings.SocialEmailEnabled = chxSocialEmailEnabled.Checked;
                Settings.SocialTwitterEnabled = chxSocialTwitterEnabled.Checked;
                Settings.SocialFacebookEnabled = chxSocialFacebookEnabled.Checked;


#if windows
                RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (key != null)
                {
                    if (this.chxStartOnBootWindows.Checked == true)
                    {
                        try
                        {
                            key.SetValue(TrayIcon.AssemblyTitle, "\"" + Application.ExecutablePath + "\"");
                        }
                        catch (UnauthorizedAccessException unauthexc)
                        {
                            Log.Write(LogType.exception, unauthexc.Message);
                            MessageBox.Show(unauthexc.Message);
                        }
                        catch (Exception exc)
                        {
                            throw new CustomException(exc.Message + " " + exc.StackTrace);
                        }
                    }
                    else if (this.chxStartOnBootWindows.Checked == false)
                    {
                        if (key.GetValue(TrayIcon.AssemblyTitle, null) != null)
                        {
                            key.DeleteValue(TrayIcon.AssemblyTitle, false);
                        }
                    }
                }
                else
                {
                    Log.Write(LogType.error, NoteFly.Properties.Resources.settings_regkeynotexist);
                    MessageBox.Show(NoteFly.Properties.Resources.settings_regkeynotexist);
                }
#endif
                //this.notes.SetSettings();
                this.notes.UpdateAllFonts();
                Log.Write(LogType.info, "settings updated.");
                this.Close();
            }
        }

        /// <summary>
        /// reset button clicked.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnResetSettings_Click(object sender, EventArgs e)
        {
            DialogResult dlgres = MessageBox.Show("Are you sure, you want to reset all the settings to default?", "reset settings?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgres == DialogResult.Yes)
            {
                string settingsfile = Path.Combine(TrayIcon.AppDataFolder, "settings.xml");
                if (File.Exists(settingsfile))
                {
                    File.Delete(settingsfile);
                    //this.xmlsettings = new xmlHandler(true);
                }
                else
                {
                    throw new Exception("Could not find settings file in application directory.");
                }

                this.Close();
            }
        }

        /// <summary>
        /// User changed if the default e-mail should be blank or not.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event arguments</param>
        private void cbxDefaultEmailToBlank_CheckedChanged(object sender, EventArgs e)
        {
            this.tbDefaultEmail.Enabled = !this.chxSocialEmailDefaultaddressBlank.Checked;
            if (this.chxSocialEmailDefaultaddressBlank.Checked)
            {
                this.tbDefaultEmail.Text = String.Empty;
            }
        }

        /// <summary>
        /// Enable password editbox on checking remember password
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void cbxRememberTwPass_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chxRememberTwPass.Checked == true)
            {
                this.tbTwitterPass.Enabled = true;
            }
            else
            {
                this.tbTwitterPass.Enabled = false;
                this.tbTwitterPass.Text = String.Empty;
            }
        }

        /// <summary>
        /// Enable nummericupdown control if transparecy is checked.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void cbxTransparecy_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chxTransparecy.Checked == false)
            {
                this.numProcTransparency.Enabled = false;
            }
            else if (this.chxTransparecy.Checked == true)
            {
                this.numProcTransparency.Enabled = true;
            }
        }

        /// <summary>
        /// Fill combobox list with fonts
        /// </summary>
        private void DrawCbxFonts()
        {
            foreach (FontFamily oneFontFamily in FontFamily.Families)
            {
                this.cbxFontNoteContent.Items.Add(oneFontFamily.Name);
            }

            if (String.IsNullOrEmpty(Settings.FontContentFamily))
            {
                Log.Write(LogType.error, NoteFly.Properties.Resources.settings_fontnotecontentnotfound);
                MessageBox.Show(NoteFly.Properties.Resources.settings_fontnotecontentnotfound, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.cbxFontNoteContent.Text = Settings.FontContentFamily;
            }
        }

        /// <summary>
        /// Gets if notefly is used to run at logon.
        /// </summary>
        /// <returns>The boolean if it starts at logon.</returns>
        private bool GetStatusStartlogin()
        {
#if windows
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (key != null)
            {
                if (key.GetValue("NoteFly", null) != null)
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

        /// <summary>
        /// Move note files.
        /// </summary>
        /// <param name="newpathsavenotes">The new path to save the notes to.</param>
        private void MoveNotes(string oldsavenotespath, string newsavenotespath)
        {
            bool errorshowed = false;
            if (!Directory.Exists(oldsavenotespath))
            {
                return;
            }
            string[] files = Directory.GetFiles(oldsavenotespath, "*.nfn");
            for (int i = 0; i < files.Length; i++)
            {
                string oldfile = Path.Combine(oldsavenotespath, files[i]);
                string newfile = Path.Combine(newsavenotespath, files[i]);
                if (!File.Exists(newfile))
                {
                    FileInfo fi = new FileInfo(oldfile);
                    if (fi.Attributes != FileAttributes.System)
                    {
                        File.Move(oldfile, newfile);
                    }
                    else { throw new CustomException("File is marked as system file. Did not move."); }
                }
                else
                {
                    if (!errorshowed)
                    {
                        Log.Write(LogType.error, NoteFly.Properties.Resources.settings_filealreadyexist);
                        MessageBox.Show(NoteFly.Properties.Resources.settings_filealreadyexist);
                        errorshowed = true;
                    }
                }
            }
        }

        /// <summary>
        /// The user changed the proxy settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chxUseProxy_CheckedChanged(object sender, EventArgs e)
        {
            this.iptbProxyAddress.Enabled = this.chxUseProxy.Checked;
        }

        #endregion Methods
    }
}
