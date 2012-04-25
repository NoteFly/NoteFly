//-----------------------------------------------------------------------
// <copyright file="FrmSettings.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2010-2012  Tom
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
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
#if windows
    using Microsoft.Win32;
#endif

    /// <summary>
    /// Setting window.
    /// </summary>
    public sealed partial class FrmSettings : Form
    {
        #region Fields (3)

        /// <summary>
        /// Reference to notes class.
        /// </summary>
        private Notes notes;

        /// <summary>
        /// In which folder notes are saved.
        /// </summary>
        private string oldnotesavepath;

        /// <summary>
        /// Array with languagescodes related to every language name in the cbxLanguage control.
        /// </summary>
        private string[] languagecodes;

        /// <summary>
        /// Last key from hotkey for new note.
        /// </summary>
        private int hotkeysnewnotekeycode;

        /// <summary>
        /// Last key from hotkey for manage notes.
        /// </summary>
        private int hotkeysmanagenoteskeycode;

        /// <summary>
        /// Last key from hotkey for notes to front.
        /// </summary>
        private int hotkeysnotestofront;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the FrmSettings class.
        /// </summary>
        /// <param name="notes">The notes class.</param>
        public FrmSettings(Notes notes)
        {
            this.DoubleBuffered = Settings.ProgramFormsDoublebuffered;
            this.oldnotesavepath = Settings.NotesSavepath;

            this.InitializeComponent();
            this.notes = notes;
            Strings.TranslateForm(this);
            this.SetFormTitle(Settings.SettingsExpertEnabled);
            this.tabControlSettings_SelectedIndexChanged(null, null);
            this.LoadCbxLanguage();
            this.LoadCbxActionLeftclick();
            this.LoadCbxFonts();
            this.LoadCbxSkins();
            this.SetControlsBySettings();
        }

        #endregion Constructors

        #region Methods (34)

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
                    string settings_dirdoesnotexist = Strings.T("Directory does not exist.\nPlease choice a valid directory.");
                    string settings_dirdoesnotexisttitle = Strings.T("Directory does not exist.\nPlease choice a valid directory.");
                    Log.Write(LogType.info, settings_dirdoesnotexist);
                    MessageBox.Show(settings_dirdoesnotexist, settings_dirdoesnotexisttitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            StringBuilder sbtitle = new StringBuilder();
            if (expertsettings)
            {
                sbtitle.Append(Strings.T("Expert settings"));
            }
            else
            {
                sbtitle.Append(Strings.T("Settings"));
            }

            sbtitle.Append(" - ");
            sbtitle.Append(Program.AssemblyTitle);
            this.Text = sbtitle.ToString();
        }

        /// <summary>
        /// Loads cbxActionLeftclick
        /// </summary>
        private void LoadCbxActionLeftclick()
        {
            this.cbxActionLeftclick.Items.Clear();
            this.cbxActionLeftclick.Items.Add(Strings.T("Do nothing"));
            this.cbxActionLeftclick.Items.Add(Strings.T("Bring notes to front"));
            this.cbxActionLeftclick.Items.Add(Strings.T("New note"));
        }

        /// <summary>
        /// Check the form input. If everything is okay
        /// call xmlHandler class to save the xml setting file.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.CheckAllSettingValid()) {

                // everything looks okay now
                // tab: General
                Settings.ConfirmExit = this.chxConfirmExit.Checked;
                Settings.ConfirmDeletenote = this.chxConfirmDeletenote.Checked;
                Settings.NotesDeleteRecyclebin = this.chxNotesDeleteRecyclebin.Checked;
                Settings.ProgramPluginsAllEnabled = this.chxLoadPlugins.Checked;
                Settings.TrayiconLeftclickaction = this.cbxActionLeftclick.SelectedIndex;
                Settings.SettingsExpertEnabled = this.chxSettingsExpertEnabled.Checked;
                Settings.ProgramLanguage = this.GetLanguageCode(this.cbxLanguage.SelectedIndex);

                // tab: Hotkeys
                Settings.HotkeysNewNoteEnabled = this.chxHotkeyNewNoteEnabled.Checked;
                Settings.HotkeysNewNoteAltInsteadShift = this.shortcutTextBoxNewNote.UseAltInsteadofShift;
                Settings.HotkeysNewNoteKeycode = this.shortcutTextBoxNewNote.ShortcutKeyposition;
                Settings.HotkeysManageNotesEnabled = this.chxHotkeyManageNotesEnabled.Checked;
                Settings.HotkeysManageNotesAltInsteadShift = this.shortcutTextBoxManageNotes.UseAltInsteadofShift;
                Settings.HotkeysManageNotesKeycode = this.shortcutTextBoxManageNotes.ShortcutKeyposition;
                Settings.HotkeysNotesToFrontEnabled = this.chxHotkeyNotesFrontEnabled.Checked;
                Settings.HotkeysNotesToFrontAltInsteadShift = this.shortcutTextBoxNotesToFront.UseAltInsteadofShift;
                Settings.HotkeysNotesToFrontKeycode = this.shortcutTextBoxNotesToFront.ShortcutKeyposition;

                // tab: Appearance, notes
                Settings.NotesTransparencyEnabled = this.chxTransparecy.Checked;
                Settings.NotesTransparencyLevel = Convert.ToDouble(this.numProcTransparency.Value / 100);
                Settings.NotesTooltipsEnabled = this.chxShowTooltips.Checked;

                // tab: Appearance, new note
                Settings.NotesDefaultRandomSkin = this.chxUseRandomDefaultNote.Checked;
                Settings.NotesDefaultSkinnr = this.cbxDefaultSkin.SelectedIndex;
                Settings.NotesDefaultWidth = Convert.ToInt32(this.numNotesDefaultWidth.Value);
                Settings.NotesDefaultHeight = Convert.ToInt32(this.numNotesDefaultHeight.Value);
                Settings.NotesDefaultTitleDate = this.chxUseDateAsDefaultTitle.Checked;

                // tab: Appearance, fonts
                Settings.FontContentFamily = this.cbxFontNoteContent.SelectedItem.ToString();
                Settings.FontContentSize = (float)this.numFontSizeContent.Value;
                Settings.FontTitleStylebold = this.cbxFontNoteTitleBold.Checked;
                Settings.FontTitleFamily = this.cbxFontNoteTitle.SelectedItem.ToString();
                Settings.FontTitleSize = (float)this.numFontSizeTitle.Value;
                Settings.FontTextdirection = this.cbxTextDirection.SelectedIndex;

                // tab: Appearance, trayicon
                Settings.FontTrayicon = this.cbxFontTrayicon.SelectedItem.ToString();
                Settings.TrayiconFontsize = (float)this.numTrayiconFontsize.Value;
                Settings.TrayiconCreatenotebold = this.chxTrayiconBoldNewnote.Checked;
                Settings.TrayiconManagenotesbold = this.chxTrayiconBoldManagenotes.Checked;
                Settings.TrayiconSettingsbold = this.chxTrayiconBoldSettings.Checked;
                Settings.TrayiconExitbold = this.chxTrayiconBoldExit.Checked;
                Settings.TrayiconAlternateIcon = this.chxUseAlternativeTrayicon.Checked;

                // tab: Appearance, manage notes
                Settings.ManagenotesSkinnr = this.cbxManageNotesSkin.SelectedIndex;
                Settings.ManagenotesTooltip = this.chxManagenotesTooltipContent.Checked;
                Settings.ManagenotesFontsize = (float)this.numManagenotesFont.Value;
                Settings.ManagenotesSearchCasesentive = this.chxCaseSentiveSearch.Checked;

                // tab: Highlight
                Settings.HighlightHyperlinks = this.chxHighlightHyperlinks.Checked;
                Settings.ConfirmLinkclick = this.chxConfirmLink.Checked;
                Settings.HighlightHTML = this.chxHighlightHTML.Checked;
                Settings.HighlightPHP = this.chxHighlightPHP.Checked;
                Settings.HighlightSQL = this.chxHighlightSQL.Checked;

                // tab: Actions
                Settings.SharingEmailEnabled = this.chxActionsEmailEnabled.Checked;
                Settings.SharingEmailDefaultadres = string.Empty;
                if (this.chxActionsEmailDefaultaddressSet.Checked)
                {
                    Settings.SharingEmailDefaultadres = this.tbDefaultEmail.Text;
                }

                // tab: Network, updates
                if (this.chxCheckUpdates.Checked)
                {
                    Settings.UpdatecheckEverydays = Convert.ToInt32(this.numUpdateCheckDays.Value);
                }
                else
                {
                    Settings.UpdatecheckEverydays = 0;
                }

                Settings.UpdateSilentInstall = this.chxUpdateSilentInstall.Checked;

                // tab: Network, Proxy
                Settings.NetworkProxyEnabled = this.chxProxyEnabled.Checked;
                Settings.NetworkProxyAddress = this.iptbProxy.GetIPAddress();
                Settings.NetworkConnectionTimeout = Convert.ToInt32(this.numTimeout.Value);
                Settings.NetworkIPversion = this.cbxNetworkIPversion.SelectedIndex; 

                // tab: Network, GnuPG
                Settings.UpdatecheckUseGPG = this.chxCheckUpdatesSignature.Checked;
                Settings.UpdatecheckGPGPath = this.tbGPGPath.Text;

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
                            string nowriterightsregistery = Strings.T("No rights to add key to registry.");
                            Log.Write(LogType.exception, nowriterightsregistery + unauthexc.Message);
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
                    string settings_regkeynotexist = Strings.T("Run key in registery does not exist.");
                    string settings_regkeynotexisttitle = Strings.T("Error run key registery missing");
                    Log.Write(LogType.error, settings_regkeynotexist);
                    MessageBox.Show(settings_regkeynotexist, settings_regkeynotexisttitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                        Thread movenotesthread = new Thread(new ParameterizedThreadStart(this.MoveNotesThread));
                        string[] args = new string[2];
                        args[0] = this.oldnotesavepath;
                        args[1] = Settings.NotesSavepath;
                        movenotesthread.Start(args);

                        movenotesthread.Join(300); // if finished within 300ms don't display buzy moving notes message.
                        this.ShowWaitOnThread(movenotesthread, 300, Strings.T("NoteFly is buzy moving your notes"));

                        this.notes.LoadNotes(true, false);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                    }

                    Program.Formmanager.FrmManageNotesNeedUpdate = true;
                }

                SyntaxHighlight.InitHighlighter();
                this.notes.UpdateAllNoteForms();
                Program.RestartTrayicon();
                if (SyntaxHighlight.KeywordsInitialized)
                {
                    // clean memory
                    SyntaxHighlight.DeinitHighlighter();
                }

                Log.Write(LogType.info, "Settings updated");
                this.Close();
            }
        }

        /// <summary>
        /// Check if all settings are valid, if a setting in this form is not correct display a error about it.
        /// </summary>
        /// <returns></returns>
        private bool CheckAllSettingValid()
        {
            bool allsettingsvalid = false;
            string settings_invalidfontsize = Strings.T("Font size invalid. Minimal 4pt maximum 128pt allowed.");
            string settings_invalidfontsizetitle = Strings.T("Error invalid fontsize");
            string settings_nofont = Strings.T("Please select a font.");
            string settings_nofonttitle = Strings.T("Error no font.");
            if (!Directory.Exists(this.tbNotesSavePath.Text))
            {
                string settings_invalidfoldersavenote = Strings.T("Invalid folder for saving notes folder.");
                string settings_invalidfoldersavenotetitle = Strings.T("Error invalid notes folder");
                Log.Write(LogType.info, settings_invalidfoldersavenote);
                MessageBox.Show(settings_invalidfoldersavenote, settings_invalidfoldersavenotetitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.tabControlSettings.SelectedTab = this.tabGeneral;
            }
            else if (string.IsNullOrEmpty(this.cbxFontNoteContent.Text) == true)
            {
                Log.Write(LogType.info, settings_nofont);
                MessageBox.Show(settings_nofont, settings_nofonttitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.tabControlSettings.SelectedTab = this.tabAppearance;
            }
            else if (string.IsNullOrEmpty(this.cbxFontNoteTitle.Text) == true)
            {
                Log.Write(LogType.info, settings_nofont);
                MessageBox.Show(settings_nofont, settings_nofonttitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.tabControlSettings.SelectedTab = this.tabAppearance;
            }
            else if ((this.numFontSizeContent.Value < 4) || (this.numFontSizeContent.Value > 128))
            {
                Log.Write(LogType.info, settings_invalidfontsize);
                MessageBox.Show(settings_invalidfontsize, settings_invalidfontsizetitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.tabControlSettings.SelectedTab = this.tabAppearance;
            }
            else if ((this.numFontSizeTitle.Value < 4) || (this.numFontSizeTitle.Value > 128))
            {
                Log.Write(LogType.info, settings_invalidfontsize);
                MessageBox.Show(settings_invalidfontsize, settings_invalidfontsizetitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.tabControlSettings.SelectedTab = this.tabAppearance;
            }
            else if (this.cbxTextDirection.SelectedIndex > 1)
            {
                string settings_noknowtextdir = Strings.T("Settings text direction invalid.");
                string settings_noknowtextdirtitle = Strings.T("Error text direction");
                Log.Write(LogType.error, settings_noknowtextdir);
                MessageBox.Show(settings_noknowtextdir, settings_noknowtextdirtitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.tabControlSettings.SelectedTab = this.tabAppearance;
            }
            else if (!this.tbDefaultEmail.IsValidEmailAddress() && this.chxActionsEmailDefaultaddressSet.Checked)
            {
                string settings_emailnotvalid = Strings.T("Given default emailadres is not valid.");
                string settings_emailnotvalidtitle = Strings.T("Email adres no valid");
                Log.Write(LogType.error, settings_emailnotvalid);
                MessageBox.Show(settings_emailnotvalid, settings_emailnotvalidtitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.tabControlSettings.SelectedTab = this.tabSharing;
            }
            else if (!File.Exists(this.tbGPGPath.Text) && this.chxCheckUpdatesSignature.Checked)
            {
                string settings_gpgpathinvalid = Strings.T("The path to gpg.exe is not valid.");
                string settings_gpgpathinvalidtitle = Strings.T("Error not valid gpg path.");
                Log.Write(LogType.info, settings_gpgpathinvalid);
                MessageBox.Show(settings_gpgpathinvalid, settings_gpgpathinvalidtitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.tabControlSettings.SelectedTab = this.tabNetwork;
            }
            else
            {
                if (PluginsManager.EnabledPlugins != null)
                {
                    // check plugin settings
                    for (int i = 0; i < PluginsManager.EnabledPlugins.Count; i++)
                    {
                        if (!PluginsManager.EnabledPlugins[i].SaveSettingsTab())
                        {
                            this.tabControlSettings.SelectedTab = this.tabSharing;
                            return false;
                        }
                    }
                }

                allsettingsvalid = true;
            }

            return allsettingsvalid;
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
                    frmmgs.Text = Strings.T("Please wait");
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
            string settings_sureresetdefault = Strings.T("Are you sure, you want to reset all the settings to default?");
            string settings_sureresetdefaulttitle = Strings.T("Reset settings?");
            DialogResult dlgres = MessageBox.Show(settings_sureresetdefault, settings_sureresetdefaulttitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
            this.tbDefaultEmail.Enabled = this.chxActionsEmailDefaultaddressSet.Checked;
        }

        /// <summary>
        /// Toggle iptbProxyAddress enabled.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void chxUseProxy_CheckedChanged(object sender, EventArgs e)
        {
            this.iptbProxy.Enabled = this.chxProxyEnabled.Checked;
            this.numProxyPort.Enabled = this.chxProxyEnabled.Checked;
        }

        /// <summary>
        /// Toggle cbxDefaultColor enabled.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void chxUseRandomDefaultNote_CheckedChanged(object sender, EventArgs e)
        {
            this.cbxDefaultSkin.Enabled = !this.chxUseRandomDefaultNote.Checked;
        }

        /// <summary>
        /// Fill combobox that are use to select a font with fontnames.
        /// </summary>
        private void LoadCbxFonts()
        {
            this.cbxFontNoteTitle.Items.Clear();
            this.cbxFontNoteContent.Items.Clear();
            this.cbxFontTrayicon.Items.Clear();
            foreach (FontFamily oneFontFamily in FontFamily.Families)
            {
                this.cbxFontNoteTitle.Items.Add(oneFontFamily.Name);
                this.cbxFontNoteContent.Items.Add(oneFontFamily.Name);
                this.cbxFontTrayicon.Items.Add(oneFontFamily.Name);
            }
        }

        /// <summary>
        /// Fill the combobox that are use to select a skin with skinnames.
        /// </summary>
        private void LoadCbxSkins()
        {
            this.cbxDefaultSkin.Items.AddRange(this.notes.GetSkinsNames());
            this.cbxManageNotesSkin.Items.AddRange(this.notes.GetSkinsNames());
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
                        string settings_excsystemfilenotmoved = Strings.T("File is marked as system file. Did not move.");
                        throw new ApplicationException(settings_excsystemfilenotmoved);
                    }
                }
                else
                {
                    if (!errorshowed)
                    {
                        string settings_filealreadyexisttitle = Strings.T("Error moving note(s)");
                        string settings_filealreadyexist = Strings.T("Note file(s) already exist.");
                        Log.Write(LogType.error, settings_filealreadyexist);
                        MessageBox.Show(settings_filealreadyexist, settings_filealreadyexisttitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            this.chxLoadPlugins.Checked = Settings.ProgramPluginsAllEnabled;
            this.SetComboBoxSelectedIndex(this.cbxActionLeftclick, Settings.TrayiconLeftclickaction);

            // tab: Hotkeys
            this.chxHotkeyNewNoteEnabled.Checked = Settings.HotkeysNewNoteEnabled;
            this.shortcutTextBoxNewNote.UseAltInsteadofShift = Settings.HotkeysNewNoteAltInsteadShift;
            this.shortcutTextBoxNewNote.ShortcutKeyposition = Settings.HotkeysNewNoteKeycode;
            this.chxHotkeyManageNotesEnabled.Checked = Settings.HotkeysManageNotesEnabled;
            this.shortcutTextBoxManageNotes.UseAltInsteadofShift = Settings.HotkeysManageNotesAltInsteadShift;
            this.shortcutTextBoxManageNotes.ShortcutKeyposition = Settings.HotkeysManageNotesKeycode;
            this.chxHotkeyNotesFrontEnabled.Checked = Settings.HotkeysNotesToFrontEnabled;
            this.shortcutTextBoxNotesToFront.UseAltInsteadofShift = Settings.HotkeysNotesToFrontAltInsteadShift;
            this.shortcutTextBoxNotesToFront.ShortcutKeyposition = Settings.HotkeysNotesToFrontKeycode;

            // tab: Appearance, notes
            this.chxTransparecy.Checked = Settings.NotesTransparencyEnabled;
            this.SetUpDownSpinnerValue(this.numProcTransparency, (Settings.NotesTransparencyLevel * 100));
            this.chxShowTooltips.Checked = Settings.NotesTooltipsEnabled;

            // tab: Appearance, new note
            this.chxUseRandomDefaultNote.Checked = Settings.NotesDefaultRandomSkin;
            this.SetComboBoxSelectedIndex(this.cbxDefaultSkin, Settings.NotesDefaultSkinnr);
            this.SetUpDownSpinnerValue(this.numNotesDefaultWidth, Settings.NotesDefaultWidth);
            this.SetUpDownSpinnerValue(this.numNotesDefaultHeight, Settings.NotesDefaultHeight);
            this.chxUseDateAsDefaultTitle.Checked = Settings.NotesDefaultTitleDate;

            // tab: Appearance, fonts
            this.SetUpDownSpinnerValue(this.numFontSizeTitle, Settings.FontTitleSize);            
            this.SetUpDownSpinnerValue(this.numFontSizeContent, Settings.FontContentSize);
            this.SetComboBoxSelectedIndex(this.cbxTextDirection, Settings.FontTextdirection);
            this.cbxFontNoteContent.Text = Settings.FontContentFamily;
            this.cbxFontNoteTitle.Text = Settings.FontTitleFamily;
            this.cbxFontNoteTitleBold.Checked = Settings.FontTitleStylebold;

            // tab: Appearance, trayicon
            this.SetUpDownSpinnerValue(this.numTrayiconFontsize, Settings.TrayiconFontsize);
            this.cbxFontTrayicon.Text = Settings.FontTrayicon;
            this.chxTrayiconBoldNewnote.Checked = Settings.TrayiconCreatenotebold;
            this.chxTrayiconBoldManagenotes.Checked = Settings.TrayiconManagenotesbold;
            this.chxTrayiconBoldSettings.Checked = Settings.TrayiconSettingsbold;
            this.chxTrayiconBoldExit.Checked = Settings.TrayiconExitbold;
            this.chxUseAlternativeTrayicon.Checked = Settings.TrayiconAlternateIcon;

            // tab: Appearance, manage notes
            this.SetComboBoxSelectedIndex(this.cbxManageNotesSkin, Settings.ManagenotesSkinnr);
            this.chxManagenotesTooltipContent.Checked = Settings.ManagenotesTooltip;
            this.SetUpDownSpinnerValue(this.numManagenotesFont, Settings.ManagenotesFontsize);
            this.chxCaseSentiveSearch.Checked = Settings.ManagenotesSearchCasesentive;

            // tab: Highlight
            this.chxHighlightHyperlinks.Checked = Settings.HighlightHyperlinks;
            this.chxHighlightHTML.Checked = Settings.HighlightHTML;
            this.chxHighlightPHP.Checked = Settings.HighlightPHP;
            this.chxHighlightSQL.Checked = Settings.HighlightSQL;
            this.chxConfirmLink.Checked = Settings.ConfirmLinkclick;

            // tab: Sharing
            this.tbDefaultEmail.Text = Settings.SharingEmailDefaultadres;
            this.chxActionsEmailEnabled.Checked = Settings.SharingEmailEnabled;
            this.chxActionsEmailDefaultaddressSet.Checked = false;
            if (!string.IsNullOrEmpty(Settings.SharingEmailDefaultadres))
            {
                this.chxActionsEmailDefaultaddressSet.Checked = true;
            }

            // tab: Network, Updates
            if (Settings.UpdatecheckEverydays > 0)
            {
                this.chxCheckUpdates.Checked = true;
                this.SetUpDownSpinnerValue(this.numUpdateCheckDays, Settings.UpdatecheckEverydays);
                this.numUpdateCheckDays.Enabled = true;
            }
            else
            {
                this.chxCheckUpdates.Checked = false;
                this.numUpdateCheckDays.Enabled = false;
            }

            this.chxUpdateSilentInstall.Checked = Settings.UpdateSilentInstall;
            this.SetLastUpdatecheckDate(Settings.SettingsExpertEnabled);

            // tab: Network, Proxy
            this.chxProxyEnabled.Checked = Settings.NetworkProxyEnabled;
            this.iptbProxy.Text = Settings.NetworkProxyAddress;
            this.SetUpDownSpinnerValue(this.numTimeout, Settings.NetworkConnectionTimeout);
            this.SetComboBoxSelectedIndex(this.cbxNetworkIPversion, Settings.NetworkIPversion);

            // tab: Network, GnuPG
            this.chxCheckUpdatesSignature.Checked = Settings.UpdatecheckUseGPG;
            this.tbGPGPath.Enabled = Settings.UpdatecheckUseGPG;
            this.tbGPGPath.Text = Settings.UpdatecheckGPGPath;

            // tab: Advance
            this.tbNotesSavePath.Text = Settings.NotesSavepath;
            this.SetUpDownSpinnerValue(this.numWarnLimitTotal, Settings.NotesWarnlimitTotal);
            this.SetUpDownSpinnerValue(this.numWarnLimitVisible, Settings.NotesWarnlimitVisible);
            this.chxLogDebug.Checked = Settings.ProgramLogInfo;
            this.chxLogErrors.Checked = Settings.ProgramLogError;
            this.chxLogExceptions.Checked = Settings.ProgramLogException;

            // set last tab as active
            this.tabControlSettings.SelectedIndex = Settings.SettingsLastTab;
        }

        /// <summary>
        /// Set a updownspinner valeau with a double valeau
        /// </summary>
        /// <param name="numupdownctrl"></param>
        /// <param name="valeau"></param>
        private void SetUpDownSpinnerValue(System.Windows.Forms.NumericUpDown numupdownctrl, double valeau)
        {
            try
            {
                decimal valeaudec = Convert.ToDecimal(valeau);
                this.SetUpDownSpinnerValue(numupdownctrl, valeaudec);
            }
            catch (OverflowException overexc)
            {
                Log.Write(LogType.error, overexc.Message);
            }
        }

        /// <summary>
        /// Set a updownspinner valeau with a integer valeau
        /// </summary>
        /// <param name="numupdownctrl"></param>
        /// <param name="value"></param>
        private void SetUpDownSpinnerValue(System.Windows.Forms.NumericUpDown numupdownctrl, int value)
        {
            decimal valeaudec = Convert.ToDecimal(value);
            this.SetUpDownSpinnerValue(numupdownctrl, valeaudec);
        }

        /// <summary>
        /// Set a updownspinner valeau with a decimal valeau, directly.
        /// Checks if it does not exceed the minimum and maximum value.
        /// </summary>
        /// <param name="numupdownctrl"></param>
        /// <param name="valuedec"></param>
        private void SetUpDownSpinnerValue(System.Windows.Forms.NumericUpDown numupdownctrl, decimal valuedec)
        {
            if (valuedec < numupdownctrl.Minimum)
            {
                numupdownctrl.Value = numupdownctrl.Minimum;
            }
            else if (valuedec > numupdownctrl.Maximum)
            {
                numupdownctrl.Value = numupdownctrl.Maximum;
            }
            else
            {
                numupdownctrl.Value = valuedec;
            }
        }

        /// <summary>
        /// Set a combobox selected index.
        /// Check if selectedindex parameter is not bigger than the availible items in the combobox and not negative.
        /// </summary>
        /// <param name="cbxctrl"></param>
        /// <param name="selectedindexvalue"></param>
        private void SetComboBoxSelectedIndex(System.Windows.Forms.ComboBox cbxctrl, int selectedindexvalue)
        {
            if (selectedindexvalue < cbxctrl.Items.Count && selectedindexvalue >= 0)
            {
                cbxctrl.SelectedIndex = selectedindexvalue;
            }
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
            Settings.UpdatecheckLastDate = Program.UpdateGetLatestVersion();
            xmlUtil.WriteSettings();
            if (!string.IsNullOrEmpty(Settings.UpdatecheckLastDate))
            {
                this.SetLastUpdatecheckDate(this.chxSettingsExpertEnabled.Checked);
            }

            this.btnCheckUpdates.Enabled = false;
        }

        /// <summary>
        /// Show and hide expert settings.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event argument</param>
        private void chxShowExpertSettings_CheckedChanged(object sender, EventArgs e)
        {
            bool expertsettings = this.chxSettingsExpertEnabled.Checked;
            this.SetFormTitle(expertsettings);
            this.chxConfirmDeletenote.Visible = expertsettings;
            this.chxLoadPlugins.Visible = expertsettings;
            this.chxNotesDeleteRecyclebin.Visible = expertsettings;
            this.chxUseAlternativeTrayicon.Visible = expertsettings;
            this.chxConfirmLink.Visible = expertsettings;
            this.chxUpdateSilentInstall.Visible = expertsettings;
            this.lblTextGPGPath.Visible = expertsettings;
            this.tbGPGPath.Visible = expertsettings;
            this.btnGPGPathBrowse.Visible = expertsettings;
            this.chxCheckUpdatesSignature.Visible = expertsettings;
            this.lblTextNetworkTimeout.Visible = expertsettings;
            this.numTimeout.Visible = expertsettings;            
            this.lblTextMiliseconds.Visible = expertsettings;
            this.lblTextNetworkMiliseconds.Visible = expertsettings;
            this.cbxFontNoteTitleBold.Visible = expertsettings;
            this.lblTextTotalNotesWarnLimit.Visible = expertsettings;
            this.numWarnLimitTotal.Visible = expertsettings;
            this.lblTextVisibleNotesWarnLimit.Visible = expertsettings;
            this.numWarnLimitVisible.Visible = expertsettings;
            this.chxCaseSentiveSearch.Visible = expertsettings;
            this.chxManagenotesTooltipContent.Visible = expertsettings;
            this.chxLogErrors.Visible = expertsettings;
            this.chxLogExceptions.Visible = expertsettings;
            this.btnOpenSettingsFolder.Visible = expertsettings;
            this.SetLastUpdatecheckDate(expertsettings);
            this.SetTabPageGPGVisible(expertsettings);
        }

        /// <summary>
        /// Display the last update date.
        /// Show also the time of the last update if expert settings is enabled.
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
        /// Set the TabPageGPG visible if expertsetting is true otherwise make TabPageGPG not visible.
        /// </summary>
        /// <param name="expertsettings"></param>
        private void SetTabPageGPGVisible(bool expertsettings)
        {
            if (expertsettings)
            {
                if (!this.tabControlNetwork.TabPages.Contains(this.tabPageGPG))
                {
                    this.tabControlNetwork.TabPages.Add(this.tabPageGPG);
                }
            }
            else
            {
                if (this.tabControlNetwork.TabPages.Contains(this.tabPageGPG))
                {
                    this.tabControlNetwork.TabPages.Remove(this.tabPageGPG);
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
                if (PluginsManager.EnabledPlugins != null)
                {
                    while (this.tabControlSharing.TabCount > 1)
                    {
                        this.tabControlSharing.Controls.RemoveAt(1);
                    }

                    for (int i = 0; i < PluginsManager.EnabledPlugins.Count; i++)
                    {
                        if (PluginsManager.EnabledPlugins[i].InitShareSettingsTab() != null)
                        {
                            this.tabControlSharing.Controls.Add(PluginsManager.EnabledPlugins[i].InitShareSettingsTab());
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

        /// <summary>
        /// If chxShowTooltips is unchecked then chxManagenotesTooltipContent get disabled and
        /// if chxShowTooltips is checked then chxManagenotesTooltipContent get enabled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chxShowTooltips_CheckStateChanged(object sender, EventArgs e)
        {
            this.chxManagenotesTooltipContent.Enabled = this.chxShowTooltips.Checked;
        }

        /// <summary>
        /// Load CbxLanguage combobox with available languages.s
        /// </summary>
        private void LoadCbxLanguage()
        {
            this.cbxLanguage.Items.Clear();
            string translatefolderpath = Path.Combine(Program.InstallFolder, Strings.ResourcesDirectory);
            if (Directory.Exists(translatefolderpath))
            {
                DirectoryInfo dir = new DirectoryInfo(translatefolderpath);
                DirectoryInfo[] culturefolders = dir.GetDirectories("*", SearchOption.TopDirectoryOnly);
                this.languagecodes = new string[culturefolders.Length];
                for (int i = 0; i < culturefolders.Length; i++)
                {
                    try
                    {
                        CultureInfo cultureinfo = CultureInfo.GetCultureInfo(culturefolders[i].Name);
                        this.cbxLanguage.Items.Add(cultureinfo.EnglishName);
                        this.languagecodes[i] = culturefolders[i].Name;
                    }
                    catch (ArgumentException)
                    {
                        Log.Write(LogType.exception, culturefolders[i].Name + " not a culture.");
                    }

                    culturefolders[i] = null;
                }

                culturefolders = null;
                GC.Collect();
            }
            else
            {
                Log.Write(LogType.exception, Strings.ResourcesDirectory + " folder is missing.");
            }

            this.cbxLanguage.SelectedItem = System.Threading.Thread.CurrentThread.CurrentUICulture.EnglishName;
        }

        /// <summary>
        /// Get the languagecode from the selected index in cbxLanguage.
        /// </summary>
        /// <param name="cbxLanguageSelectedIndex"></param>
        /// <returns>The languagecode return en for english if not found.</returns>
        private string GetLanguageCode(int cbxLanguageSelectedIndex)
        {
            if (cbxLanguageSelectedIndex < this.languagecodes.Length && cbxLanguageSelectedIndex >= 0)
            {
                string languageisocode = this.languagecodes[cbxLanguageSelectedIndex];
                if (!string.IsNullOrEmpty(languageisocode))
                {
                    return languageisocode;
                }
            }

            Log.Write(LogType.exception, "Selected language in cbxLanguage not found, used default.");
            return "en";
        }

        /// <summary>
        /// The selected language in chxLanguage is changed,
        /// change the language of the programme.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxLanguage.SelectedIndex >= 0)
            {
                Program.SetCulture(this.GetLanguageCode(this.cbxLanguage.SelectedIndex));
            }
            else
            {
                Log.Write(LogType.exception, "No language selected.");
            }
        }

        /// <summary>
        /// While changing hotkey ingore hotkeys pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ingore_hotkeys(object sender, KeyEventArgs e)
        {
            this.hotkeysnewnotekeycode = Settings.HotkeysNewNoteKeycode;
            this.hotkeysmanagenoteskeycode = Settings.HotkeysManageNotesKeycode;
            this.hotkeysnotestofront = Settings.HotkeysNotesToFrontKeycode;
            // hotkey does not work.
            Settings.HotkeysNewNoteKeycode = 0;
            Settings.HotkeysManageNotesKeycode = 0;
            Settings.HotkeysNotesToFrontKeycode = 0;
        }

        /// <summary>
        /// Changing hotkey ended, allow NoteFly hotkeys again.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Allow_hotkeys(object sender, KeyEventArgs e)
        {
            Settings.HotkeysNewNoteKeycode = this.hotkeysnewnotekeycode;
            Settings.HotkeysManageNotesKeycode = this.hotkeysmanagenoteskeycode;
            Settings.HotkeysNotesToFrontKeycode = this.hotkeysnotestofront;
        }

        /// <summary>
        /// Open explorer with the NoteFly application data folder.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenSettingsFolder_Click(object sender, EventArgs e)
        {
            this.btnOpenSettingsFolder.Enabled = false;
            System.Diagnostics.ProcessStartInfo procInfo = new System.Diagnostics.ProcessStartInfo(Program.AppDataFolder);
            procInfo.CreateNoWindow = false;
            procInfo.UseShellExecute = true;
            System.Diagnostics.Process.Start(procInfo);
            this.btnOpenSettingsFolder.Enabled = true;
        }

        /// <summary>
        /// If chxHotkeyNewNoteEnabled is checked then enable shortcutTextBoxNewNote and lblTextHotkeyNewNote.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chxHotkeyNewNoteEnabled_CheckedChanged(object sender, EventArgs e)
        {
            this.shortcutTextBoxNewNote.Enabled = this.chxHotkeyNewNoteEnabled.Checked;
            this.lblTextHotkeyNewNote.Enabled = this.chxHotkeyNewNoteEnabled.Checked;
        }

        /// <summary>
        /// If chxHotkeyManageNotesEnabled is checked then enable shortcutTextBoxManageNotes and lblTextHotkeyManageNotes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chxHotkeyManageNotesEnabled_CheckedChanged(object sender, EventArgs e)
        {
            this.shortcutTextBoxManageNotes.Enabled = this.chxHotkeyManageNotesEnabled.Checked;
            this.lblTextHotkeyManageNotes.Enabled = this.chxHotkeyManageNotesEnabled.Checked;
        }

        /// <summary>
        /// If chxHotkeyNotesFrontEnabled is checked then enable shortcutTextBoxNotesToFront and lblTextHotkeyNotesToFront.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chxHotkeyNotesFrontEnabled_CheckedChanged(object sender, EventArgs e)
        {
            this.shortcutTextBoxNotesToFront.Enabled = this.chxHotkeyNotesFrontEnabled.Checked;
            this.lblTextHotkeyNotesToFront.Enabled = this.chxHotkeyNotesFrontEnabled.Checked;
        }

        /// <summary>
        /// Check if iptbProxy is allowed to have a IPv4 and/or IPv6 address.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxNetworkIPversion_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbxNetworkIPversion.SelectedIndex)
            {
                case 0:
                    this.iptbProxy.UseIPv4addr = true;
                    this.iptbProxy.UseIPv6addr = true;
                    break;
                case 1:
                    this.iptbProxy.UseIPv4addr = true;
                    this.iptbProxy.UseIPv6addr = false;
                    break;
                case 2:
                    this.iptbProxy.UseIPv4addr = false;
                    this.iptbProxy.UseIPv6addr = true;
                    break;
            }

            this.iptbProxy.ValidateIP();
        }

        #endregion Methods
    }
}