//-----------------------------------------------------------------------
// <copyright file="TrayIcon.cs" company="NoteFly">
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
    using System.Reflection;
    using System.Windows.Forms;

    /// <summary>
    /// TrayIcon gui object class.
    /// </summary>
    public sealed class TrayIcon
    {
        /// <summary>
        /// Used for warning if new note is still open on shutdown application.
        /// </summary>
        private static bool frmneweditnoteopen = false;

        /// <summary>
        /// Reference to FrmManageNotes window.
        /// </summary>
        private static FrmManageNotes frmmanagenotes;

        /// <summary>
        /// Reference to FrmPlugins window.
        /// </summary>
        private FrmPlugins frmplugins;

        /// <summary>
        /// Reference to FrmSettings window.
        /// </summary>
        private FrmSettings frmsettings;

        /// <summary>
        /// container that holds some objects.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// indicated wheter confirm exit is showed.
        /// </summary>
        private bool confirmexitshowed = false;

        /// <summary>
        /// Is the creation of a new note being showed, so double left clicking isnt creating two notes at once.
        /// </summary>
        private bool frmnewnoteshowed = false;

        /// <summary>
        /// Notes class has a list an methodes for accessing notes.
        /// </summary>
        private Notes notes;

        /// <summary>
        /// The trayicon itself.
        /// </summary>
        private NotifyIcon icon;

        /// <summary>
        /// The trayicon contextmenu
        /// </summary>
        private ContextMenuStrip menuTrayIcon;

        /// <summary>
        /// New note menu option
        /// </summary>
        private ToolStripMenuItem menuNewNote;

        /// <summary>
        /// Manage notes menu option
        /// </summary>
        private ToolStripMenuItem menuManageNotes;

        /// <summary>
        /// Settings application menu option
        /// </summary>
        private ToolStripMenuItem menuSettings;

        /// <summary>
        /// Plugins menu option
        /// </summary>
        private ToolStripMenuItem menuPlugins;

        /// <summary>
        /// About menu option
        /// </summary>
        private ToolStripMenuItem menuAbout;

        /// <summary>
        /// Exit menu option
        /// </summary>
        private ToolStripMenuItem menuExit;

        private int deltaX = 0;

        private int deltaY = 0;

#if windows
        private KeyboardListener keylister;

        private bool controlpressed = false;
        private bool shiftpressed = false;
        private bool altpressed = false;
#endif

        /// <summary>
        /// Initializes a new instance of the TrayIcon class. 
        /// New trayicon in the systray.
        /// </summary>
        /// <param name="notes">reference to notes class.</param>
        public TrayIcon(Notes notes)
        {
            this.notes = notes;
            this.components = new System.ComponentModel.Container();

            // Start building icon and icon contextmenu
            this.icon = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuTrayIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuTrayIcon.AllowDrop = false;
            this.menuNewNote = new System.Windows.Forms.ToolStripMenuItem();
            this.menuManageNotes = new System.Windows.Forms.ToolStripMenuItem();
            this.menuPlugins = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.icon = new NotifyIcon(this.components);
            this.icon.ContextMenuStrip = this.menuTrayIcon;
            if (Settings.TrayiconAlternateIcon)
            {
                this.icon.Icon = new Icon(NoteFly.Properties.Resources.trayicon_white, NoteFly.Properties.Resources.trayicon_white.Size);
            }
            else
            {
                this.icon.Icon = new Icon(NoteFly.Properties.Resources.trayicon_yellow, NoteFly.Properties.Resources.trayicon_white.Size);
            }

            this.icon.MouseClick += new MouseEventHandler(this.Icon_Click);
            this.icon.Visible = true;
            this.icon.ContextMenuStrip.Name = "MenuTrayIcon";
            this.icon.ContextMenuStrip.ShowImageMargin = false;
            this.icon.ContextMenuStrip.Size = new System.Drawing.Size(145, 114);
            FontStyle menufontstyle = FontStyle.Regular;

            // MenuNewNote
            this.menuNewNote.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuNewNote.Name = "MenuNewNote";
            this.menuNewNote.Size = new System.Drawing.Size(144, 22);
            this.menuNewNote.Text = Strings.T("&New note");
            if (Settings.TrayiconCreatenotebold)
            {
                menufontstyle = FontStyle.Bold;
            }

            this.menuNewNote.Font = new Font("Microsoft Sans Serif", Settings.TrayiconFontsize, menufontstyle);
            this.menuNewNote.Click += new System.EventHandler(this.MenuNewNote_Click);
            this.icon.ContextMenuStrip.Items.Add(this.menuNewNote);

            // MenuManageNotes
            this.menuManageNotes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuManageNotes.Name = "listToolStripMenuItem";
            this.menuManageNotes.Size = new System.Drawing.Size(144, 22);
            this.menuManageNotes.Text = Strings.T("&Manage notes");
            if (Settings.TrayiconManagenotesbold)
            {
                menufontstyle = FontStyle.Bold;
            }
            else
            {
                menufontstyle = FontStyle.Regular;
            }

            this.menuManageNotes.Font = new Font("Microsoft Sans Serif", Settings.TrayiconFontsize, menufontstyle);
            this.menuManageNotes.Click += new System.EventHandler(this.MenuManageNotes_Click);
            this.icon.ContextMenuStrip.Items.Add(this.menuManageNotes);

            // MenuSettings
            this.menuSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuSettings.Name = "MenuSettings";
            this.menuSettings.Size = new System.Drawing.Size(144, 22);
            this.menuSettings.Text = Strings.T("&Settings");
            if (Settings.TrayiconSettingsbold)
            {
                menufontstyle = FontStyle.Bold;
            }
            else
            {
                menufontstyle = FontStyle.Regular;
            }

            this.menuSettings.Font = new Font("Microsoft Sans Serif", Settings.TrayiconFontsize, menufontstyle);
            this.menuSettings.Click += new System.EventHandler(this.MenuSettings_Click);
            this.icon.ContextMenuStrip.Items.Add(this.menuSettings);

            // menuPlugins
            this.menuPlugins.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuPlugins.Name = "MenuPlugins";
            this.menuPlugins.Size = new System.Drawing.Size(144, 22);
            this.menuPlugins.Text = Strings.T("&Plugins");
            this.menuPlugins.Font = new Font("Microsoft Sans Serif", Settings.TrayiconFontsize, FontStyle.Regular);
            this.menuPlugins.Click += new System.EventHandler(this.MenuPlugins_Click);
            this.icon.ContextMenuStrip.Items.Add(this.menuPlugins);

            // Create trayicon plugin ToolStripMenuItem items, if any.
            if (PluginsManager.pluginsenabled != null)
            {
                for (int p = 0; p < PluginsManager.pluginsenabled.Length; p++)
                {
                    if (PluginsManager.pluginsenabled[p].InitTrayIconMenu() != null)
                    {
                        ToolStripItem toolstripitem = PluginsManager.pluginsenabled[p].InitTrayIconMenu();
                        toolstripitem.Size = new System.Drawing.Size(144, 22);
                        toolstripitem.Font = new Font("Microsoft Sans Serif", Settings.TrayiconFontsize, FontStyle.Regular);
                        this.icon.ContextMenuStrip.Items.Add(toolstripitem);
                    }
                }
            }


            // MenuAbout
            this.menuAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuAbout.Name = "MenuAbout";
            this.menuAbout.Size = new System.Drawing.Size(144, 22);
            this.menuAbout.Text = Strings.T("About");
            this.menuAbout.Font = new Font("Microsoft Sans Serif", Settings.TrayiconFontsize, FontStyle.Regular);
            this.menuAbout.Click += new System.EventHandler(this.MenuAbout_Click);
            this.icon.ContextMenuStrip.Items.Add(this.menuAbout);

            // MenuExit
            this.menuExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuExit.Name = "MenuExit";
            this.menuExit.Size = new System.Drawing.Size(144, 22);
            this.menuExit.Text = Strings.T("E&xit");
            if (Settings.TrayiconExitbold)
            {
                menufontstyle = FontStyle.Bold;
            }
            else
            {
                menufontstyle = FontStyle.Regular;
            }

            this.menuExit.Font = new Font("Microsoft Sans Serif", Settings.TrayiconFontsize, menufontstyle);
            this.menuExit.Click += new System.EventHandler(this.MenuExit_Click);
            this.icon.ContextMenuStrip.Items.Add(this.menuExit);

            // Show balloontip on firstrun about trayicon how to access notefly functions.
            if (!Settings.ProgramFirstrunned)
            {
                string trayicon_trayiconaccesshint = Strings.T("You can access {0} functions with this trayicon.", Program.AssemblyTitle);
                this.icon.ShowBalloonTip(6000, Program.AssemblyTitle, trayicon_trayiconaccesshint, ToolTipIcon.Info);
            }
            
#if windows
            this.keylister = new KeyboardListener();
            this.keylister.s_KeyEventHandler += new EventHandler(KeyboardListener_s_KeyEventHandler);
#endif
            //this.components.Add(this.keylister);
        }

#if windows
        private void KeyboardListener_s_KeyEventHandler(object sender, EventArgs e)
        {
            KeyboardListener.UniversalKeyEventArgs eventArgs = (KeyboardListener.UniversalKeyEventArgs)e;

            // is key down
            if (eventArgs.m_Msg == 256)
            {
                if (eventArgs.KeyData == Keys.ControlKey || this.controlpressed)
                {
                    this.controlpressed = true;

                    if (eventArgs.KeyData == Keys.Alt || eventArgs.KeyData == Keys.Menu || this.altpressed)
                    {
                        this.altpressed = true;

                        // Ctrl + Alt + KEY
                        if (eventArgs.KeyValue == Settings.HotkeysNewNoteKeycode && Settings.HotkeysNewNoteAltInsteadShift)  // eventArgs.KeyData == Keys.F1
                        {
                            this.MenuNewNote_Click(null, null);
                            this.ResetAllModifierKeys();
                        }
                        // Ctrl + Alt + KEY
                        else if (eventArgs.KeyValue == Settings.HotkeysManageNotesKeycode && Settings.HotkeysManageNotesAltInsteadShift) // eventArgs.KeyData == Keys.F2
                        {
                            this.MenuManageNotes_Click(null, null);
                            this.ResetAllModifierKeys();
                        }
                    }
                    else if (eventArgs.KeyData == Keys.ShiftKey || this.shiftpressed)
                    {
                        this.shiftpressed = true;

                        // Ctrl + Shift + KEY
                        if (eventArgs.KeyValue == Settings.HotkeysNewNoteKeycode && !Settings.HotkeysNewNoteAltInsteadShift)
                        {
                            this.MenuNewNote_Click(null, null);
                            this.ResetAllModifierKeys();
                        }
                        // Ctrl + Shift + KEY
                        else if (eventArgs.KeyValue == Settings.HotkeysManageNotesKeycode && !Settings.HotkeysManageNotesAltInsteadShift)
                        {
                            this.MenuManageNotes_Click(null, null);
                            this.ResetAllModifierKeys();
                        }
                    }
                    else if (eventArgs.KeyData != Keys.ControlKey && eventArgs.KeyData != Keys.ShiftKey && eventArgs.KeyData != Keys.Menu && eventArgs.KeyData != Keys.Alt)
                    {
                        this.ResetAllModifierKeys();
                    }
                }
                else
                {
                    this.ResetAllModifierKeys();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetAllModifierKeys()
        {
            this.controlpressed = false;
            this.shiftpressed = false;
            this.altpressed = false;
        }
#endif

        /// <summary>
        /// Gets or sets a value indicating whether FrmNewNote is being showed.
        /// </summary>
        public static bool Frmneweditnoteopen
        {
            get
            {
                return frmneweditnoteopen;
            }

            set
            {
                frmneweditnoteopen = value;
            }
        }

        /// <summary>
        /// Do a refresh on the FrmManageNotes window if it's created.
        /// </summary>
        public static void RefreshFrmManageNotes()
        {
            if (frmmanagenotes != null)
            {
                frmmanagenotes.Resetdatagrid();
                frmmanagenotes.Refresh();
            }
        }

        /// <summary>
        /// Destroy NotifyIcon with ContextMenuStrip and ToolStripMenuItems etc.
        /// </summary>
        public void Dispose()
        {
            this.icon.Visible = false; // Mono needs Visible set to false otherwise it keeps showing the trayicon.
            this.keylister = null;
            this.components.Dispose();
        }

        /// <summary>
        /// There is left clicked on the icon.
        /// If actionleftclick is 0 do nothing.
        /// If actionleftclick is 1 actived all notes.
        /// If actionleftclick is 2 create a new note.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void Icon_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Settings.TrayiconLeftclickaction == 1)
                {
                    this.notes.BringToFrontNotes();
                }
                else if (Settings.TrayiconLeftclickaction == 2)
                {
                    if (!this.frmnewnoteshowed)
                    {
                        FrmNewNote frmnewnote = new FrmNewNote(this.notes, this.deltaX, this.deltaY);
                        this.ChangeDeltaPositionNewNote();
                        frmnewnote.Show();
                        this.frmnewnoteshowed = true;
                    }
                    else
                    {
                        this.frmnewnoteshowed = false;
                    }
                }
            }
        }

        /// <summary>
        /// Change the delta position of FrmNewNote.
        /// </summary>
        private void ChangeDeltaPositionNewNote()
        {
            if (deltaX < 100 && deltaY < 100)
            {
                this.deltaX += 10;
                this.deltaY += 10;
            }
            else
            {
                this.deltaX = 0;
                this.deltaY = 0;
            }
        }

        /// <summary>
        /// Open new note window.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event argument</param>
        private void MenuNewNote_Click(object sender, EventArgs e)
        {
            Frmneweditnoteopen = true;            
            FrmNewNote newnotefrm = new FrmNewNote(this.notes, this.deltaX, this.deltaY);
            this.ChangeDeltaPositionNewNote();
            newnotefrm.Show();
            newnotefrm.Focus();
        }

        /// <summary>
        /// open manage notes window
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event argument</param>
        private void MenuManageNotes_Click(object sender, EventArgs e)
        {
            if (frmmanagenotes == null)
            {
                frmmanagenotes = new FrmManageNotes(this.notes);
                frmmanagenotes.Show();
            }
            else if (frmmanagenotes.IsDisposed)
            {
                frmmanagenotes = new FrmManageNotes(this.notes);
                frmmanagenotes.Show();
            }
            else
            {
                frmmanagenotes.WindowState = FormWindowState.Normal;
                frmmanagenotes.Activate();
            }
        }

        /// <summary>
        /// Open settings window.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event argument</param>
        private void MenuSettings_Click(object sender, EventArgs e)
        {
            if (this.frmsettings == null)
            {
                this.frmsettings = new FrmSettings(this.notes);
                this.frmsettings.Show();
            }
            else if (this.frmsettings.IsDisposed)
            {
                this.frmsettings = new FrmSettings(this.notes);
                this.frmsettings.Show();
            }
            else
            {
                this.frmsettings.WindowState = FormWindowState.Normal;
                this.frmsettings.Activate();
            }
        }

        /// <summary>
        /// Open about window.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event argument</param>
        private void MenuAbout_Click(object sender, EventArgs e)
        {
            FrmAbout frmabout = new FrmAbout();
            frmabout.ShowDialog();
        }

        /// <summary>
        /// Open plugins window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuPlugins_Click(object sender, EventArgs e)
        {
            if (this.frmplugins == null)
            {
                this.frmplugins = new FrmPlugins();
                this.frmplugins.Show();
            }
            else if (this.frmplugins.IsDisposed)
            {
                this.frmplugins = new FrmPlugins();
                this.frmplugins.Show();
            }
            else
            {
                this.frmplugins.WindowState = FormWindowState.Normal;
                this.frmplugins.Activate();
            }
        }

        /// <summary>
        /// User request to shutdown application.
        /// Check if confirm box is needed. 
        /// If confirm box is still open and menuExit_Click event is fired then shutdown application anyway.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event argument</param>
        private void MenuExit_Click(object sender, EventArgs e)
        {
            string trayicon_sureexittitle = Strings.T("confirm exit");
            if (Settings.ConfirmExit)
            {
                // Two times exit in contextmenu systray icon will always exit.
                if (!this.confirmexitshowed)
                {
                    this.confirmexitshowed = true;
                    string trayicon_sureexit = Strings.T("Are sure you want to exit {0}?", Program.AssemblyTitle);
                    DialogResult resdlgconfirmexit = MessageBox.Show(trayicon_sureexit, trayicon_sureexittitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resdlgconfirmexit == DialogResult.No)
                    {
                        this.confirmexitshowed = false;
                        return;
                    }
                }
            }

            if (Frmneweditnoteopen)
            {
                string trayicon_notestillopen = Strings.T("A note is still open for editing.\nAre you sure you want to shutdown {0}?", Program.AssemblyTitle);
                DialogResult resdlg = MessageBox.Show(trayicon_notestillopen, trayicon_sureexittitle, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (resdlg == DialogResult.No)
                {
                    return;
                }
            }

            this.components.Dispose();
            Application.Exit();
        }
    }
}
