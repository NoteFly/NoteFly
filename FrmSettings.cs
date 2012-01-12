//-----------------------------------------------------------------------
// <copyright file="FrmSettings.cs" company="NoteFly">
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
namespace NoteFly
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using System.Threading;
#if windows
    using Microsoft.Win32;
#endif

    /// <summary>
    /// Setting window.
    /// </summary>
    public sealed partial class FrmSettings : Form
    {
        #region Fields (2)

        /// <summary>
        /// Reference to notes class.
        /// </summary>
        private Notes notes;

        /// <summary>
        /// In which folder notes are saved.
        /// </summary>
        private string oldnotesavepath;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the FrmSettings class.
        /// </summary>
        /// <param name="notes">The notes class.</param>
        public FrmSettings(Notes notes)
        {
            this.DoubleBuffered = Settings.ProgramFormsDoublebuffered;
            this.InitializeComponent();
            this.oldnotesavepath = Settings.NotesSavepath;
            this.notes = notes;
            this.DrawCbxFonts();
            this.SetFormTitle(Settings.SettingsExpertEnabled);
            this.SetControlsBySettings();
            this.tabControlSettings_SelectedIndexChanged(null, null);
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
            DialogResult dlgresult = this.folderBrowseDialogNotessavepath.ShowDialog();
            if (dlgresult == DialogResult.OK)
            {
                string newpathsavenotes = this.folderBrowseDialogNotessavepath.SelectedPath;

                if (Directory.Exists(newpathsavenotes))
                {
                    this.tbNotesSavePath.Text = this.folderBrowseDialogNotessavepath.SelectedPath;
                }
                else
                {
                    const string SETTINGS_DIRDOESNOTEXIST = "Directory does not exist.\r\nPlease choice a valid directory.";
                    const string SETTINGS_DIRDOESNOTEXISTTITLE = "Directory not found";
                    Log.Write(LogType.info, SETTINGS_DIRDOESNOTEXIST);
                    MessageBox.Show(SETTINGS_DIRDOESNOTEXIST, SETTINGS_DIRDOESNOTEXISTTITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// Set the title of this form.
        /// </summary>
        /// <param name="expertsettings">Is showing expert settings enabled.</param>
        private void SetFormTitle(bool expertsettings)
        {
            const string SETTINGSTITLE = "settings";
            if (expertsettings)
            {
                this.Text = "Expert " + SETTINGSTITLE;
            }
            else
            {
                this.Text = SETTINGSTITLE;
            }
        }

        /// <summary>
        /// Check the form input. If everything is okay
        /// call xmlHandler class to save the xml setting file.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnOK_Click(object sender, EventArgs e)
        {            
            const string SETTINGS_INVALIDFONTSIZE = "Font size invalid. Minimal 4pt maximum 128pt allowed.";
            const string SETTINGS_INVALIDFONTSIZETITLE = "Error invalid fontsize";
            const string SETTINGS_NOFONT = "Please select a font.";
            const string SETTINGS_NOFONTTITLE = "Error no font.";
            if (!Directory.Exists(this.tbNotesSavePath.Text))
            {
                const string SETTINGS_INVALIDFOLDERSAVENOTE = "Invalid folder for saving notes folder.";
                const string SETTINGS_INVALIDFOLDERSAVENOTETITLE = "Error invalid notes folder";
                Log.Write(LogType.info, SETTINGS_INVALIDFOLDERSAVENOTE);
                MessageBox.Show(SETTINGS_INVALIDFOLDERSAVENOTE, SETTINGS_INVALIDFOLDERSAVENOTETITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.tabControlSettings.SelectedTab = this.tabGeneral;
            }
            else if (string.IsNullOrEmpty(this.cbxFontNoteContent.Text) == true)
            {
                Log.Write(LogType.info, SETTINGS_NOFONT);
                MessageBox.Show(SETTINGS_NOFONT, SETTINGS_NOFONTTITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.tabControlSettings.SelectedTab = this.tabAppearance;
            }
            else if (string.IsNullOrEmpty(this.cbxFontNoteTitle.Text) == true)
            {
                Log.Write(LogType.info, SETTINGS_NOFONT);
                MessageBox.Show(SETTINGS_NOFONT, SETTINGS_NOFONTTITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.tabControlSettings.SelectedTab = this.tabAppearance;
            }
            else if ((this.numFontSizeContent.Value < 4) || (this.numFontSizeContent.Value > 128))
            {
                Log.Write(LogType.info, SETTINGS_INVALIDFONTSIZE);
                MessageBox.Show(SETTINGS_INVALIDFONTSIZE, SETTINGS_INVALIDFONTSIZETITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.tabControlSettings.SelectedTab = this.tabAppearance;
            }
            else if ((this.numFontSizeTitle.Value < 4) || (this.numFontSizeTitle.Value > 128))
            {
                Log.Write(LogType.info, SETTINGS_INVALIDFONTSIZE);
                MessageBox.Show(SETTINGS_INVALIDFONTSIZE, SETTINGS_INVALIDFONTSIZETITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.tabControlSettings.SelectedTab = this.tabAppearance;
            }
            else if (this.cbxTextDirection.SelectedIndex > 1)
            {
                const string SETTINGS_NOKNOWTEXTDIR = "Settings text direction invalid.";
                const string SETTINGS_NOKNOWTEXTDIRTITLE = "Error text direction";
                Log.Write(LogType.error, SETTINGS_NOKNOWTEXTDIR);
                MessageBox.Show(SETTINGS_NOKNOWTEXTDIR, SETTINGS_NOKNOWTEXTDIRTITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.tabControlSettings.SelectedTab = this.tabAppearance;
            }
            else if ((!this.tbDefaultEmail.Text.Contains("@") || !this.tbDefaultEmail.Text.Contains(".")) && this.chxSocialEmailDefaultaddressSet.Checked)
            {
                const string SETTINGS_EMAILNOTVALID = "Given default emailadres is not valid.";
                const string SETTINGS_EMAILNOTVALIDTITLE = "Email adres no valid";
                Log.Write(LogType.error, SETTINGS_EMAILNOTVALID);
                MessageBox.Show(SETTINGS_EMAILNOTVALID, SETTINGS_EMAILNOTVALIDTITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.tabControlSettings.SelectedTab = this.tabSharing;
            }
            else if (!File.Exists(this.tbGPGPath.Text) && this.chxCheckUpdatesSignature.Checked)
            {
                const string SETTINGS_GPGPATHINVALID = "The path to gpg.exe is not valid.";
                const string SETTINGS_GPGPATHINVALIDTITLE = "Error not valid gpg path.";
                Log.Write(LogType.info, SETTINGS_GPGPATHINVALID);
                MessageBox.Show(SETTINGS_GPGPATHINVALID, SETTINGS_GPGPATHINVALIDTITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.tabControlSettings.SelectedTab = this.tabNetwork;
            }
            else
            {
                if (Program.pluginsenabled != null)
                {
                    // check plugin settings
                    for (int i = 0; i < Program.pluginsenabled.Length; i++)
                    {
                        if (!Program.pluginsenabled[i].SaveSettingsTab())
                        {
                            this.tabControlSettings.SelectedTab = this.tabSharing;
                            return;
                        }
                    }
                }

                // everything looks okay now
                // tab: General
                Settings.ConfirmExit = this.chxConfirmExit.Checked;
                Settings.ConfirmDeletenote = this.chxConfirmDeletenote.Checked;
                Settings.NotesDeleteRecyclebin = this.chxNotesDeleteRecyclebin.Checked;
                Settings.TrayiconLeftclickaction = this.cbxActionLeftclick.SelectedIndex;
                Settings.SettingsExpertEnabled = this.chxSettingsExpertEnabled.Checked;

                // tab: Appearance, notes
                Settings.NotesTransparencyEnabled = this.chxTransparecy.Checked;
                Settings.NotesTransparencyLevel = Convert.ToDouble(this.numProcTransparency.Value / 100);                
                Settings.NotesTooltipsEnabled = this.chxShowTooltips.Checked;

                // tab: Appearance, new note
                Settings.NotesDefaultRandomSkin = this.chxUseRandomDefaultNote.Checked;
                Settings.NotesDefaultSkinnr = this.cbxDefaultColor.SelectedIndex;
                Settings.NotesDefaultWidth = Convert.ToInt32(this.numNotesDefaultWidth.Value);
                Settings.NotesDefaultHeight = Convert.ToInt32(this.numNotesDefaultHeight.Value);

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
                Settings.TrayiconAlternateIcon = this.chxUseAlternativeTrayicon.Checked;

                // tab: Highlight
                Settings.HighlightHyperlinks = this.chxHighlightHyperlinks.Checked;
                Settings.HighlightHTML = this.chxHighlightHTML.Checked;
                Settings.HighlightPHP = this.chxHighlightPHP.Checked;
                Settings.HighlightSQL = this.chxHighlightSQL.Checked;

                // tab: Sharing
                Settings.SharingEmailEnabled = this.chxSocialEmailEnabled.Checked;
                Settings.SharingEmailDefaultadres = string.Empty;
                if (this.chxSocialEmailDefaultaddressSet.Checked)
                {
                    Settings.SharingEmailDefaultadres = this.tbDefaultEmail.Text;
                }

                // tab: Network
                if (this.chxCheckUpdates.Checked)
                {
                    Settings.UpdatecheckEverydays = Convert.ToInt32(this.numUpdateCheckDays.Value);
                }
                else
                {
                    Settings.UpdatecheckEverydays = 0;
                }

                Settings.UpdateSilentInstall = this.chxUpdateSilentInstall.Checked;
                Settings.UpdatecheckUseGPG = this.chxCheckUpdatesSignature.Checked;
                Settings.UpdatecheckGPGPath = this.tbGPGPath.Text;
                Settings.NetworkConnectionTimeout = Convert.ToInt32(this.numTimeout.Value);
                Settings.NetworkProxyEnabled = this.chxProxyEnabled.Checked;
                Settings.NetworkProxyAddress = this.iptbProxy.getIPAddress();
                Settings.ConfirmLinkclick = this.chxConfirmLink.Checked;

                // tab: Advance
                if (Directory.Exists(this.tbNotesSavePath.Text))
                {
                    Settings.NotesSavepath = this.tbNotesSavePath.Text;
                }

                Settings.NotesWarnlimitTotal = Convert.ToInt32(this.numWarnLimitTotal.Value);
                Settings.NotesWarnlimitVisible = Convert.ToInt32(this.numWarnLimitVisible.Value);
                Settings.ProgramLogError = this.chxLogErrors.Checked;
                Settings.ProgramLogInfo = this.chxLogDebug.Checked;
                Settings.ProgramLogException = this.chxLogExceptions.Checked;
                Settings.SettingsLastTab = this.tabControlSettings.SelectedIndex;
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
                            const string NOWRITERIGHTSREGISTERY = "Not enough right to write logon start key to registry.";
                            Log.Write(LogType.exception, NOWRITERIGHTSREGISTERY + unauthexc.Message);
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
                    const string SETTINGS_REGKEYNOTEXIST = "Run key in registery does not exist.";
                    const string SETTINGS_REGKEYNOTEXISTTITLE = "Error run key registery missing";
                    Log.Write(LogType.error, SETTINGS_REGKEYNOTEXIST);
                    MessageBox.Show(SETTINGS_REGKEYNOTEXIST, SETTINGS_REGKEYNOTEXISTTITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
#endif
                xmlUtil.WriteSettings();

                if (Settings.NotesSavepath != this.oldnotesavepath)
                {
                    for (int i = 0; i < this.notes.CountNotes; i++)
                    {
                        this.notes.GetNote(i).DestroyForm();
                    }

                    while (this.notes.CountNotes > 0)
                    {
                        this.notes.RemoveNote(0);
                    }

                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        
                        Thread movenotesthread = new Thread(new ParameterizedThreadStart(MoveNotesThread));
                        string[] args = new string[2];
                        args[0] = this.oldnotesavepath;
                        args[1] = Settings.NotesSavepath;
                        movenotesthread.Start(args);

                        movenotesthread.Join(300); // if finished within 300ms don't display buzy moving notes message.
                        this.ShowWaitOnThread(movenotesthread, 300, "NoteFly is buzy moving your notes");

                        this.notes.LoadNotes(true, false);                        
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }

                    this.notes.FrmManageNotesNeedUpdate = true;
                }

                SyntaxHighlight.InitHighlighter();
                this.notes.UpdateAllNoteForms();
                Program.RestartTrayicon();
                if (SyntaxHighlight.KeywordsInitialized)
                {
                    // clean memory
                    SyntaxHighlight.DeinitHighlighter();
                }

                const string SETTINGS_INFOUPDATED = "Settings updated";
                Log.Write(LogType.info, SETTINGS_INFOUPDATED);
                this.Close();
            }
        }

        /// <summary>
        /// Show a message form while thread is buzy.
        /// And auto close while done.
        /// </summary>
        /// <param name="worktread">The thread that is doing work while message being showed</param>
        /// <param name="checktimems">miliseconds to check if workthread is done, is also the minimum show time of the message, if being showed</param>
        /// <param name="message">The message to show</param>
        private void ShowWaitOnThread(Thread worktread, int checktimems, string message)
        {            
            Form frmmgs = null;
            bool mgsshowed = false;
            while (worktread.ThreadState == ThreadState.Running)
            {
                if (!mgsshowed)
                {
                    mgsshowed = true;
                    frmmgs = new Form();
                    frmmgs.StartPosition = FormStartPosition.CenterScreen;
                    frmmgs.Size = new Size(240, 80);
                    frmmgs.ShowIcon = false;
                    frmmgs.ShowInTaskbar = false;
                    frmmgs.MinimizeBox = false;
                    frmmgs.MaximizeBox = false;
                    frmmgs.Text = "Please wait";
                    Label lblmgs = new Label();
                    lblmgs.Text = message;
                    lblmgs.SetBounds(10, 10, 200, 40);
                    frmmgs.Controls.Add(lblmgs);
                    frmmgs.Show();                    
                }

                Thread.Sleep(checktimems);
            }

            if (frmmgs != null)
            {
                frmmgs.Close();
            }
        }

        /// <summary>
        /// reset button clicked.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnResetSettings_Click(object sender, EventArgs e)
        {
            const string SETTINGS_SURERESETDEFAULT = "Are you sure, you want to reset all the settings to default?";
            const string SETTINGS_SURERESETDEFAULTTITLE = "Reset settings?";
            DialogResult dlgres = MessageBox.Show(SETTINGS_SURERESETDEFAULT, SETTINGS_SURERESETDEFAULTTITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
        /// Toggle tbDefaultEmail enabled.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void chxSocialEmailDefaultaddressBlank_CheckedChanged(object sender, EventArgs e)
        {
            this.tbDefaultEmail.Enabled = this.chxSocialEmailDefaultaddressSet.Checked;
        }

        /// <summary>
        /// Toggle iptbProxyAddress enabled.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void chxUseProxy_CheckedChanged(object sender, EventArgs e)
        {
            this.iptbProxy.Enabled = this.chxProxyEnabled.Checked;
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
        /// 
        /// </summary>
        private void MoveNotesThread(object args)
        {
            string[] pathsargs = (string[])args;
            this.MoveNotes(pathsargs[0], pathsargs[1]);
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

            string[] notefilespath = Directory.GetFiles(oldsavenotespath, "*" + Notes.NOTEEXTENSION, SearchOption.TopDirectoryOnly);
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
                        const string SETTINGS_EXCSYSTEMFILENOTMOVED = "File is marked as system file. Did not move.";
                        throw new ApplicationException(SETTINGS_EXCSYSTEMFILENOTMOVED);
                    }
                }
                else
                {
                    if (!errorshowed)
                    {
                        const string SETTINGS_FILEALREADYEXIST = "Note file(s) already exist.";
                        const string SETTINGS_FILEALREADYEXISTTITLE = "Error movind note(s)";
                        Log.Write(LogType.error, SETTINGS_FILEALREADYEXIST);
                        MessageBox.Show(SETTINGS_FILEALREADYEXIST, SETTINGS_FILEALREADYEXISTTITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            this.chxSettingsExpertEnabled.Checked = Settings.SettingsExpertEnabled;

            // tab: General
#if windows
            this.chxStartOnLogin.Checked = this.GetStartOnLogon();
#endif
            this.chxConfirmExit.Checked = Settings.ConfirmExit;
            this.chxConfirmDeletenote.Checked = Settings.ConfirmDeletenote;
            this.chxNotesDeleteRecyclebin.Checked = Settings.NotesDeleteRecyclebin;
            this.cbxActionLeftclick.SelectedIndex = Settings.TrayiconLeftclickaction;            

            // tab: Appearance, notes
            this.chxTransparecy.Checked = Settings.NotesTransparencyEnabled;
            this.numProcTransparency.Value = Convert.ToDecimal(Settings.NotesTransparencyLevel * 100);
            this.chxShowTooltips.Checked = Settings.NotesTooltipsEnabled;

            // tab: Appearance, new note
            this.chxUseRandomDefaultNote.Checked = Settings.NotesDefaultRandomSkin;
            this.cbxDefaultColor.SelectedIndex = Settings.NotesDefaultSkinnr;
            this.numNotesDefaultWidth.Value = Convert.ToDecimal(Settings.NotesDefaultWidth);
            this.numNotesDefaultHeight.Value = Convert.ToDecimal(Settings.NotesDefaultHeight);

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
            this.chxUseAlternativeTrayicon.Checked = Settings.TrayiconAlternateIcon;

            // tab: Highlight
            this.chxHighlightHyperlinks.Checked = Settings.HighlightHyperlinks;
            this.chxHighlightHTML.Checked = Settings.HighlightHTML;
            this.chxHighlightPHP.Checked = Settings.HighlightPHP;
            this.chxHighlightSQL.Checked = Settings.HighlightSQL;

            // tab: Sharing
            this.tbDefaultEmail.Text = Settings.SharingEmailDefaultadres;
            this.chxSocialEmailEnabled.Checked = Settings.SharingEmailEnabled;
            this.chxSocialEmailDefaultaddressSet.Checked = false;
            if (!string.IsNullOrEmpty(Settings.SharingEmailDefaultadres))
            {
                this.chxSocialEmailDefaultaddressSet.Checked = true;
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

            this.chxUpdateSilentInstall.Checked = Settings.UpdateSilentInstall;
            this.chxCheckUpdatesSignature.Checked = Settings.UpdatecheckUseGPG;
            this.tbGPGPath.Enabled = Settings.UpdatecheckUseGPG;
            this.tbGPGPath.Text = Settings.UpdatecheckGPGPath;
            this.chxProxyEnabled.Checked = Settings.NetworkProxyEnabled;
            this.iptbProxy.Text = Settings.NetworkProxyAddress;
            this.chxConfirmLink.Checked = Settings.ConfirmLinkclick;
            this.numTimeout.Value = Settings.NetworkConnectionTimeout;
            this.SetLastUpdatecheckDate(Settings.SettingsExpertEnabled);                        

            // tab: Advance
            this.chxLoadPlugins.Checked = Settings.ProgramPluginsAllEnabled;
            this.tbNotesSavePath.Text = Settings.NotesSavepath;
            this.numWarnLimitTotal.Value = Convert.ToDecimal(Settings.NotesWarnlimitTotal);
            this.numWarnLimitVisible.Value = Convert.ToDecimal(Settings.NotesWarnlimitVisible);
            this.chxLogDebug.Checked = Settings.ProgramLogInfo;
            this.chxLogErrors.Checked = Settings.ProgramLogError;
            this.chxLogExceptions.Checked = Settings.ProgramLogException;

            // set last tab as active
            this.tabControlSettings.SelectedIndex = Settings.SettingsLastTab;
        }

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
        /// <param name="sender">sender object</param>
        /// <param name="e">event argument</param>
        private void btnCheckUpdates_Click(object sender, EventArgs e)
        {
            Settings.UpdatecheckLastDate = Program.UpdateCheck();
            if (!string.IsNullOrEmpty(Settings.UpdatecheckLastDate))
            {
                this.lblLatestUpdateCheck.Text = Settings.UpdatecheckLastDate;
            }

            this.btnCheckUpdates.Enabled = false;
        }

        /// <summary>
        /// Show and hide expert settings.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event argument</param>
        private void cbxShowExpertSettings_CheckedChanged(object sender, EventArgs e)
        {
            bool expertsettings = this.chxSettingsExpertEnabled.Checked;
            this.SetFormTitle(expertsettings);
            this.chxConfirmDeletenote.Visible = expertsettings;
            this.chxNotesDeleteRecyclebin.Visible = expertsettings;
            this.chxShowTooltips.Visible = expertsettings;
            this.chxUseAlternativeTrayicon.Visible = expertsettings;
            this.chxUpdateSilentInstall.Visible = expertsettings;
            this.lblTextGPGPath.Visible = expertsettings;
            this.tbGPGPath.Visible = expertsettings;
            this.btnGPGPathBrowse.Visible = expertsettings;
            this.chxCheckUpdatesSignature.Visible = expertsettings;
            this.lblTextNetworkTimeout.Visible = expertsettings;
            this.numTimeout.Visible = expertsettings;
            this.lblTextNetworkMiliseconds.Visible = expertsettings;
            this.cbxFontNoteTitleBold.Visible = expertsettings;
            this.chxLogErrors.Visible = expertsettings;
            this.chxLogExceptions.Visible = expertsettings;
            this.lblTextTotalNotesWarnLimit.Visible = expertsettings;
            this.numWarnLimitTotal.Visible = expertsettings;
            this.lblTextVisibleNotesWarnLimit.Visible = expertsettings;
            this.numWarnLimitVisible.Visible = expertsettings;
            this.SetLastUpdatecheckDate(expertsettings);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expertsettings"></param>
        private void SetLastUpdatecheckDate(bool expertsettings)
        {
            if (expertsettings)
            {
                this.lblLatestUpdateCheck.Text = Settings.UpdatecheckLastDate;
            }
            else
            {
                DateTime dt;
                if (DateTime.TryParse(Settings.UpdatecheckLastDate, out dt))
                {
                    this.lblLatestUpdateCheck.Text = dt.ToShortDateString();
                }
                else
                {
                    this.lblLatestUpdateCheck.Text = Settings.UpdatecheckLastDate;
                }                
            }
        }

        /// <summary>
        /// Load share tab plugins
        /// </summary>
        /// <param name="sender">The selected tab</param>
        /// <param name="e">Event arguments</param>
        private void tabControlSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControlSettings.SelectedTab == this.tabSharing)
            {
                if (Program.pluginsenabled != null)
                {
                    while (this.tabControlSharing.TabCount > 1)
                    {
                        this.tabControlSharing.Controls.RemoveAt(1);
                    }

                    for (int i = 0; i < Program.pluginsenabled.Length; i++)
                    {
                        if (Program.pluginsenabled[i].InitShareSettingsTab() != null)
                        {
                            this.tabControlSharing.Controls.Add(Program.pluginsenabled[i].InitShareSettingsTab());
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Toggle setting path to GPG.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void chxCheckUpdatesSignature_CheckedChanged(object sender, EventArgs e)
        {
            this.tbGPGPath.Enabled = this.chxCheckUpdatesSignature.Checked;
        }

        /// <summary>
        /// Open browse dialog to gpg.exe
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnGPGPathBrowse_Click(object sender, EventArgs e)
        {
            DialogResult dlggpgresult = this.openFileDialogBrowseGPG.ShowDialog();
            if (dlggpgresult == DialogResult.OK)
            {
                this.tbGPGPath.Text = this.openFileDialogBrowseGPG.FileName;
            }
        }

        #endregion Methods
    }
}