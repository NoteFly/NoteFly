//-----------------------------------------------------------------------
// <copyright file="FrmSettings.cs" company="GNU">
//  NoteFly a note application.
//  Copyright (C) 2010-2011  Tom
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
#define windows // platform can be: windows, linux, macos

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

        /// <summary>
        /// Reference to notes class.
        /// </summary>
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
            this.notes = notes;
            this.DrawCbxFonts();
            this.SetControlsBySettings();
        }

        #endregion Constructors

        #region Methods (13)

        /// <summary>
        /// User want to browse for notes save path.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments</param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult dlgresult = this.folderBrowserDialogNotessavepath.ShowDialog();
            if (dlgresult == DialogResult.OK)
            {
                string newpathsavenotes = this.folderBrowserDialogNotessavepath.SelectedPath;

                if (Directory.Exists(newpathsavenotes))
                {
                    this.tbNotesSavePath.Text = this.folderBrowserDialogNotessavepath.SelectedPath;
                }
                else
                {
                    Log.Write(LogType.info, NoteFly.Properties.Resources.settings_dirdoesnotexist);
                    MessageBox.Show(NoteFly.Properties.Resources.settings_dirdoesnotexist, NoteFly.Properties.Resources.settings_dirdoesnotexisttitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// Check the form input. If everything is okay
        /// call xmlHandler class to save the xml setting file.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            // check if the set settings are possible.
            if (!Directory.Exists(this.tbNotesSavePath.Text))
            {
                Log.Write(LogType.info, NoteFly.Properties.Resources.settings_invalidfoldersavenote);
                MessageBox.Show(NoteFly.Properties.Resources.settings_invalidfoldersavenote);
                this.tabControlSettings.SelectedTab = this.tabGeneral;
            }
            else if (string.IsNullOrEmpty(this.cbxFontNoteContent.Text) == true)
            {
                Log.Write(LogType.info, NoteFly.Properties.Resources.settings_nofont);
                MessageBox.Show(NoteFly.Properties.Resources.settings_nofont);
                this.tabControlSettings.SelectedTab = this.tabAppearance;
            }
            else if ((this.numFontSizeContent.Value < 4) || (this.numFontSizeContent.Value > 128))
            {
                Log.Write(LogType.info, NoteFly.Properties.Resources.settings_invalidfontsize);
                MessageBox.Show(NoteFly.Properties.Resources.settings_invalidfontsize);
                this.tabControlSettings.SelectedTab = this.tabAppearance;
            }
            else if ((this.numFontSizeTitle.Value < 4) || (this.numFontSizeTitle.Value > 128))
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
 || (this.chxStartOnLogin.CheckState == CheckState.Indeterminate)
#endif
 || (this.chxConfirmExit.CheckState == CheckState.Indeterminate) || (this.chxLogErrors.CheckState == CheckState.Indeterminate) || (this.chxLogDebug.CheckState == CheckState.Indeterminate))
            {
                Log.Write(LogType.error, NoteFly.Properties.Resources.settings_notallowcheckstate);
                MessageBox.Show(NoteFly.Properties.Resources.settings_notallowcheckstate);
                this.tabControlSettings.SelectedTab = this.tabAppearance;
            }
            else if ((!this.tbDefaultEmail.Text.Contains("@") || !this.tbDefaultEmail.Text.Contains(".")) && (!this.chxSocialEmailDefaultaddressBlank.Checked))
            {
                Log.Write(LogType.error, NoteFly.Properties.Resources.settings_emailnotvalid);
                MessageBox.Show(NoteFly.Properties.Resources.settings_emailnotvalid);
                this.tabControlSettings.SelectedTab = this.tabAdvance;
            }
            else
            {
                // everything looks okay 
                string oldnotesavepath = Settings.NotesSavepath;
                if (this.tbNotesSavePath.Text != oldnotesavepath)
                {
                    this.MoveNotes(oldnotesavepath, this.tbNotesSavePath.Text); // TODO: put on seperate thread
                }

                // tab: General
                Settings.ConfirmExit = this.chxConfirmExit.Checked;
                Settings.ConfirmDeletenote = this.chxConfirmDeletenote.Checked;
                Settings.NotesDeleteRecyclebin = this.chxNotesDeleteRecyclebin.Checked;
                Settings.TrayiconLeftclickaction = this.cbxActionLeftclick.SelectedIndex;

                // tab: Appearance, looks
                Settings.NotesTransparencyEnabled = this.chxTransparecy.Checked;
                Settings.NotesTransparencyLevel = Convert.ToDouble(this.numProcTransparency.Value / 100);
                Settings.NotesDefaultRandomSkin = this.chxUseRandomDefaultNote.Checked;
                Settings.NotesDefaultSkinnr = this.cbxDefaultColor.SelectedIndex;
                Settings.NotesTooltipsEnabled = this.cbxShowTooltips.Checked;

                // tab: Appearance, fonts
                Settings.FontContentFamily = this.cbxFontNoteContent.SelectedItem.ToString();
                Settings.FontContentSize = (float)this.numFontSizeContent.Value;
                Settings.FontTitleStylebold = this.cbxFontNoteTitleBold.Checked;
                Settings.FontTitleFamily = this.cbxFontNoteTitle.SelectedItem.ToString();
                Settings.FontTitleSize = (float)this.numFontSizeTitle.Value;
                Settings.FontTextdirection = this.cbxTextDirection.SelectedIndex;

                // tab: Appearance, trayicon
                Settings.TrayiconFontsize = (float)this.numTrayiconFontsize.Value;
                Settings.TrayiconCreatenotebold = this.chxTrayiconBoldNewnote.Checked;
                Settings.TrayiconManagenotesbold = this.chxTrayiconBoldManagenotes.Checked;
                Settings.TrayiconSettingsbold = this.chxTrayiconBoldSettings.Checked;
                Settings.TrayiconExitbold = this.chxTrayiconBoldExit.Checked;

                // tab: Highlight
                Settings.HighlightHyperlinks = this.chxHighlightHyperlinks.Checked;
                Settings.HighlightHTML = this.chxHighlightHTML.Checked;
                Settings.HighlightPHP = this.chxHighlightPHP.Checked;
                Settings.HighlightSQL = this.chxHighlightSQL.Checked;

                // tab: Social networks
                Settings.SocialEmailEnabled = this.chxSocialEmailEnabled.Checked;
                Settings.SocialEmailDefaultadres = this.tbDefaultEmail.Text;

                // tab: Network
                if (this.chxCheckUpdates.Checked)
                {
                    Settings.UpdatecheckEverydays = Convert.ToInt32(this.numUpdateCheckDays.Value);
                }
                else
                {
                    Settings.UpdatecheckEverydays = 0;
                }

                Settings.NetworkConnectionTimeout = Convert.ToInt32(this.numTimeout.Value);
                Settings.NetworkProxyEnabled = this.chxProxyEnabled.Checked;
                Settings.NetworkProxyAddress = this.iptbProxyAddress.IPAddress;
                Settings.ConfirmLinkclick = this.chxConfirmLink.Checked;

                // tab: Advance
                Settings.NotesSavepath = this.tbNotesSavePath.Text;
                Settings.ProgramLogError = this.chxLogErrors.Checked;
                Settings.ProgramLogInfo = this.chxLogDebug.Checked;
                Settings.ProgramLogException = this.chxLogExceptions.Checked;
#if windows
                RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (key != null)
                {
                    if (this.chxStartOnLogin.Checked == true)
                    {
                        try
                        {
                            key.SetValue(Program.AssemblyTitle, "\"" + Application.ExecutablePath + "\"");
                        }
                        catch (UnauthorizedAccessException unauthexc)
                        {
                            Log.Write(LogType.exception, "Not enough right to write logon start key to registry." + unauthexc.Message);
                            MessageBox.Show(unauthexc.Message);
                        }
                        catch (Exception exc)
                        {
                            throw new ApplicationException(exc.Message + " " + exc.StackTrace);
                        }
                    }
                    else if (this.chxStartOnLogin.Checked == false)
                    {
                        if (key.GetValue(Program.AssemblyTitle, null) != null)
                        {
                            key.DeleteValue(Program.AssemblyTitle, false);
                        }
                    }
                }
                else
                {
                    Log.Write(LogType.error, NoteFly.Properties.Resources.settings_regkeynotexist);
                    MessageBox.Show(NoteFly.Properties.Resources.settings_regkeynotexist);
                }
#endif
                xmlUtil.WriteSettings();
                Log.Write(LogType.info, NoteFly.Properties.Resources.settings_infoupdated);
                if (!SyntaxHighlight.KeywordsInitialized)
                {
                    SyntaxHighlight.InitHighlighter();
                }

                this.notes.UpdateAllNoteForms();
                Program.RestartTrayicon();
                if (SyntaxHighlight.KeywordsInitialized)
                {
                    SyntaxHighlight.DeinitHighlighter();
                }

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
            DialogResult dlgres = MessageBox.Show(NoteFly.Properties.Resources.settings_sureresetdefault, NoteFly.Properties.Resources.settings_sureresetdefaulttitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgres == DialogResult.Yes)
            {
                xmlUtil.WriteDefaultSettings();
                this.SetControlsBySettings();
            }
        }

        /// <summary>
        /// The user de-/selected checking for updates.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void cbxCheckUpdates_CheckedChanged(object sender, EventArgs e)
        {
            this.numUpdateCheckDays.Enabled = this.chxCheckUpdates.Checked;
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
                this.tbDefaultEmail.Text = string.Empty;
            }
        }

        /// <summary>
        /// Toggle numProcTransparency enabled
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void cbxTransparecy_CheckedChanged(object sender, EventArgs e)
        {
            this.numProcTransparency.Enabled = this.chxTransparecy.Checked;
        }

        /// <summary>
        /// Toggle tbDefaultEmail enabled.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void chxSocialEmailDefaultaddressBlank_CheckedChanged(object sender, EventArgs e)
        {
            this.tbDefaultEmail.Enabled = !this.chxSocialEmailDefaultaddressBlank.Checked;
        }

        /// <summary>
        /// Toggle iptbProxyAddress enabled.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void chxUseProxy_CheckedChanged(object sender, EventArgs e)
        {
            this.iptbProxyAddress.Enabled = this.chxProxyEnabled.Checked;
        }

        /// <summary>
        /// Toggle cbxDefaultColor enabled.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void chxUseRandomDefaultNote_CheckedChanged(object sender, EventArgs e)
        {
            this.cbxDefaultColor.Enabled = !this.chxUseRandomDefaultNote.Checked;
        }

        /// <summary>
        /// Fill combobox list with fonts
        /// </summary>
        private void DrawCbxFonts()
        {
            foreach (FontFamily oneFontFamily in FontFamily.Families)
            {
                this.cbxFontNoteTitle.Items.Add(oneFontFamily.Name);
                this.cbxFontNoteContent.Items.Add(oneFontFamily.Name);
            }

            this.cbxDefaultColor.Items.AddRange(this.notes.GetSkinsNames());
        }

        /// <summary>
        /// Move note files.
        /// </summary>
        /// <param name="oldsavenotespath">The old path where notes are saved.</param>
        /// <param name="newsavenotespath">The new path to save the notes to.</param>
        private void MoveNotes(string oldsavenotespath, string newsavenotespath)
        {
            bool errorshowed = false;
            if (!Directory.Exists(oldsavenotespath) || !Directory.Exists(newsavenotespath))
            {
                return;
            }

            string[] notefilespath = Directory.GetFiles(Settings.NotesSavepath, "*" + Notes.NOTEEXTENSION, SearchOption.TopDirectoryOnly);
            string[] notefiles = new string[notefilespath.Length];
            for (int i = 0; i < notefilespath.Length; i++)
            {
                notefiles[i] = Path.GetFileName(notefilespath[i]);
            }

            notefilespath = null;
            for (int i = 0; i < notefiles.Length; i++)
            {
                string oldfile = Path.Combine(oldsavenotespath, notefiles[i]);
                string newfile = Path.Combine(newsavenotespath, notefiles[i]);
                if (!File.Exists(newfile))
                {
                    FileInfo fi = new FileInfo(oldfile);
                    if (fi.Attributes != FileAttributes.System)
                    {
                        try
                        {
                            File.Move(oldfile, newfile);
                        }
                        catch (UnauthorizedAccessException unauthexc)
                        {
                            Log.Write(LogType.error, unauthexc.Message);
                        }
                    }
                    else 
                    { 
                        throw new ApplicationException(NoteFly.Properties.Resources.settings_excsystemfilenotmoved); 
                    }
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
        /// Read setting and set controls to display them correctly.
        /// </summary>
        private void SetControlsBySettings()
        {
            // tab: General
#if windows
            this.chxStartOnLogin.Checked = this.GetStartOnLogon();
#endif
            this.chxConfirmExit.Checked = Settings.ConfirmExit;
            this.chxConfirmDeletenote.Checked = Settings.ConfirmDeletenote;
            this.chxNotesDeleteRecyclebin.Checked = Settings.NotesDeleteRecyclebin;
            this.cbxActionLeftclick.SelectedIndex = Settings.TrayiconLeftclickaction;

            // tab: Appearance
            this.chxTransparecy.Checked = Settings.NotesTransparencyEnabled;
            this.numProcTransparency.Value = Convert.ToDecimal(Settings.NotesTransparencyLevel * 100);
            this.chxUseRandomDefaultNote.Checked = Settings.NotesDefaultRandomSkin;
            this.cbxDefaultColor.SelectedIndex = Settings.NotesDefaultSkinnr;
            this.cbxShowTooltips.Checked = Settings.NotesTooltipsEnabled;

            // tab: Appearance, fonts
            this.numFontSizeTitle.Value = Convert.ToDecimal(Settings.FontTitleSize);
            this.cbxFontNoteContent.SelectedValue = Settings.FontContentFamily;
            this.numFontSizeContent.Value = Convert.ToDecimal(Settings.FontContentSize);
            this.cbxTextDirection.SelectedIndex = Settings.FontTextdirection;
            this.cbxFontNoteContent.Text = Settings.FontContentFamily;
            this.cbxFontNoteTitle.Text = Settings.FontTitleFamily;
            this.cbxFontNoteTitleBold.Checked = Settings.FontTitleStylebold;

            // tab: Appearance, trayicon
            this.numTrayiconFontsize.Value = Convert.ToDecimal(Settings.TrayiconFontsize);
            this.chxTrayiconBoldNewnote.Checked = Settings.TrayiconCreatenotebold;
            this.chxTrayiconBoldManagenotes.Checked = Settings.TrayiconManagenotesbold;
            this.chxTrayiconBoldSettings.Checked = Settings.TrayiconSettingsbold;
            this.chxTrayiconBoldExit.Checked = Settings.TrayiconExitbold;

            // tab: Highlight
            this.chxHighlightHyperlinks.Checked = Settings.HighlightHyperlinks;
            this.chxHighlightHTML.Checked = Settings.HighlightHTML;
            this.chxHighlightPHP.Checked = Settings.HighlightPHP;
            this.chxHighlightSQL.Checked = Settings.HighlightSQL;

            // tab: social networks
            this.tbDefaultEmail.Text = Settings.SocialEmailDefaultadres;
            this.chxSocialEmailDefaultaddressBlank.Checked = false;
            this.chxSocialEmailEnabled.Checked = Settings.SocialEmailEnabled;
            if (string.IsNullOrEmpty(Settings.SocialEmailDefaultadres))
            {
                this.chxSocialEmailDefaultaddressBlank.Checked = true;
            }

            // tab: Network
            if (Settings.UpdatecheckEverydays > 0)
            {
                this.chxCheckUpdates.Checked = true;
                this.numUpdateCheckDays.Value = Convert.ToDecimal(Settings.UpdatecheckEverydays);
                this.numUpdateCheckDays.Enabled = true;
            }
            else
            {
                this.chxCheckUpdates.Checked = false;
                this.numUpdateCheckDays.Enabled = false;
            }

            this.chxProxyEnabled.Checked = Settings.NetworkProxyEnabled;
            this.iptbProxyAddress.IPAddress = Settings.NetworkProxyAddress;
            this.chxConfirmLink.Checked = Settings.ConfirmLinkclick;
            this.numTimeout.Value = Settings.NetworkConnectionTimeout;
            this.lblLatestUpdateCheck.Text = Settings.UpdatecheckLastDate;

            // tab: Advance
            this.tbNotesSavePath.Text = Settings.NotesSavepath;
            this.chxLogDebug.Checked = Settings.ProgramLogInfo;
            this.chxLogErrors.Checked = Settings.ProgramLogError;
            this.chxLogExceptions.Checked = Settings.ProgramLogException;
        }

        #endregion Methods

#if windows
        /// <summary>
        /// Gets if notefly is used to run at logon.
        /// </summary>
        /// <returns>The boolean if it starts at logon.</returns>
        private bool GetStartOnLogon()
        {
            bool startonlogon = false;
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (key != null)
            {
                if (key.GetValue(Program.AssemblyTitle, null) != null)
                {
                    startonlogon = true;
                }
            }

            return startonlogon;
        }
#endif

        /// <summary>
        /// Toggle enabling numProcTransparency.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event argument</param>
        private void chxTransparecy_CheckedChanged(object sender, EventArgs e)
        {
            this.numProcTransparency.Enabled = this.chxTransparecy.Checked;
        }

        /// <summary>
        /// Requested to manually do an update check.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckUpdates_Click(object sender, EventArgs e)
        {
            Settings.UpdatecheckLastDate = Program.UpdateCheck();
            if (!String.IsNullOrEmpty(Settings.UpdatecheckLastDate))
            {
                this.lblLatestUpdateCheck.Text = Settings.UpdatecheckLastDate;
            }
            this.btnCheckUpdates.Enabled = false;
        }

    }
}
