//-----------------------------------------------------------------------
// <copyright file="TrayIcon.cs" company="GNU">
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
#define windows //platform can be: windows, linux, macos //platform can be: windows, linux, macos

using System;
[assembly: CLSCompliant(true)]
namespace NoteFly
{
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;

    /// <summary>
    /// Startup class.
    /// </summary>
    public class TrayIcon
    {
        #region Fields (15)

        /// <summary>
        /// container that holds some objects.
        /// </summary>
        private static System.ComponentModel.IContainer components = null;
        /// <summary>
        /// indicated wheter confirm exit is showed.
        /// </summary>
        private static bool confirmexitshowed = false;

        /// <summary>
        /// Is the creation of a new note being showed.
        /// </summary>
        private static bool frmnewnoteshowed = false;

        private static FrmAbout frmabout;
        private static FrmManageNotes frmmanagenotes;
        private static FrmNewNote frmnewnote;
        private static FrmSettings frmsettings;

        /// <summary>
        /// Notes class has a list an methodes for accessing notes.
        /// </summary>
        private static Notes notes;

        /// <summary>
        /// The trayicon itself.
        /// </summary>
        private static NotifyIcon icon;

        /// <summary>
        /// The trayicon contextmenu
        /// </summary>
        private static ContextMenuStrip menuTrayIcon;

        /// <summary>
        /// New note menu option
        /// </summary>
        private static ToolStripMenuItem menuNewNote;

        /// <summary>
        /// Manage notes menu option
        /// </summary>
        private static ToolStripMenuItem menuManageNotes;

        /// <summary>
        /// Settings application menu option
        /// </summary>
        private static ToolStripMenuItem menuSettings;

        /// <summary>
        /// About menu option
        /// </summary>
        private static ToolStripMenuItem menuAbout;

        /// <summary>
        /// Exit menu option
        /// </summary>
        private static ToolStripMenuItem menuExit;

        #endregion Fields

        #region Properties (3)

        /// <summary>
        /// Gets the application data folder.
        /// </summary>
        public static string AppDataFolder
        {
            get
            {
#if windows
                string appdatafolder = System.Environment.GetEnvironmentVariable("APPDATA") + "\\." + TrayIcon.AssemblyTitle + "\\";
#elif linux
                string appdatafolder = System.Environment.GetEnvironmentVariable("HOME") + "/.NoteFly/";
#elif macos
                string appdatafolder = "???"
#endif
                return appdatafolder;
            }
        }

        /// <summary>
        /// Gets the application title.
        /// </summary>
        public static string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (!String.IsNullOrEmpty(titleAttribute.Title))
                    {
                        return titleAttribute.Title;
                    }
                }

                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        /// <summary>
        /// Gets the application version number.
        /// </summary>
        /// <returns>a string containing the version number of this application in the form of major.minur.build number</returns>
        public static string AssemblyVersion
        {
            get
            {
                int majorver = Assembly.GetExecutingAssembly().GetName().Version.Major;
                int minorver = Assembly.GetExecutingAssembly().GetName().Version.Minor;
                int buildver = Assembly.GetExecutingAssembly().GetName().Version.Build;
                return majorver + "." + minorver + "." + buildver;
            }
        }

        #endregion Properties

        #region Methods (2)

        // Private Methods (2) 

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            components = new System.ComponentModel.Container();
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(true);
            xmlUtil.LoadSettings();
            bool forcefirstrun = false;
            //override settings with supported parameters
            if (System.Environment.GetCommandLineArgs().Length > 1)
            {
                foreach (string arg in args)
                {
                    switch (arg)
                    {
                        //Forces the programme to setup the first run notefly info again.
                        case "/forcefirstrun":
                            forcefirstrun = true;
                            //Settings.ProgramFirstrun = true;
                            break;

                        //disabletransparency parameter is for OS that don't support transparency, so they can still show notes.
                        //because transparency is on by default.
                        case "/disabletransparency":
                            Settings.NotesTransparencyEnabled = false;
                            break;

                        //Turn off all highlighting functions in case highlighting was turned on and it let NoteFly crash on startup.
                        case "/disablehighlighting":
                            Settings.HighlightHyperlinks = false;
                            Settings.HighlightHTML = false;
                            Settings.HighlightPHP = false;
                            Settings.HighlightSQL = false;
                            break;

                        //Turn off all social functions on startup.
                        case "/disablesocial":
                            Settings.SocialEmailEnabled = false;
                            Settings.SocialFacebookEnabled = false;
                            Settings.SocialTwitterEnabled = false;
                            break;

                        //Turn all logging features on at startup. 
                        //Handy in case NoteFly crashes at startup and logging was turned off.
                        case "/logall":
                            Settings.ProgramLogException = true;
                            Settings.ProgramLogError = true;
                            Settings.ProgramLogInfo = true;
                            break;
                    }
                }
            }

            /*
            #if windows
                                System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
                                System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
                                bool IsAdmin = principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
                                if (IsAdmin)
                                {
                                    MessageBox.Show("You are now running notefly as elevated Administrator, which is not necessary.", "(Elevated) administrator");
                                }
            #endif
            */

            //start loading notes.
            notes = new Notes(forcefirstrun);

            //start building icon and icon contextmenu
            icon = new System.Windows.Forms.NotifyIcon(components);
            menuTrayIcon = new System.Windows.Forms.ContextMenuStrip(components);
            menuTrayIcon.AllowDrop = false;
            menuNewNote = new System.Windows.Forms.ToolStripMenuItem();
            menuManageNotes = new System.Windows.Forms.ToolStripMenuItem();
            menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            menuExit = new System.Windows.Forms.ToolStripMenuItem();

            icon = new NotifyIcon(components);
            icon.ContextMenuStrip = menuTrayIcon;
            Assembly assembly = Assembly.GetExecutingAssembly();
            icon.Icon = new Icon(assembly.GetManifestResourceStream("NoteFly.Resources.trayicon.ico"));

            icon.MouseClick += new MouseEventHandler(Icon_Click);
            icon.Visible = true;

            icon.ContextMenuStrip.Name = "MenuTrayIcon";
            icon.ContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripMenuItem[] 
            {
            menuNewNote,
            menuManageNotes,
            menuSettings,
            menuAbout,
            menuExit});
            icon.ContextMenuStrip.ShowImageMargin = false;
            icon.ContextMenuStrip.Size = new System.Drawing.Size(145, 114);

            FontStyle menufontstyle = FontStyle.Regular;

            // MenuNewNote
            menuNewNote.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            menuNewNote.Name = "MenuNewNote";
            menuNewNote.Size = new System.Drawing.Size(144, 22);
            menuNewNote.Text = "&Create a new note";
            if (Settings.TrayiconCreatenotebold)
            {
                menufontstyle = FontStyle.Bold;
            }
            menuNewNote.Font = new Font("Microsoft Sans Serif", 8.25f, menufontstyle);
            menuNewNote.Click += new System.EventHandler(MenuNewNote_Click);
            // MenuManageNotes
            menuManageNotes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            menuManageNotes.Name = "listToolStripMenuItem";
            menuManageNotes.Size = new System.Drawing.Size(144, 22);
            menuManageNotes.Text = "&Manage notes";
            if (Settings.TrayiconManagenotesbold)
            {
                menufontstyle = FontStyle.Bold;
            }
            else
            {
                menufontstyle = FontStyle.Regular;
            }
            menuManageNotes.Font = new Font("Microsoft Sans Serif", 8.25f, menufontstyle);
            menuManageNotes.Click += new System.EventHandler(MenuManageNotes_Click);
            // MenuSettings
            menuSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            menuSettings.Name = "MenuSettings";
            menuSettings.Size = new System.Drawing.Size(144, 22);
            menuSettings.Text = "&Settings";
            if (Settings.TrayiconSettingsbold)
            {
                menufontstyle = FontStyle.Bold;
            }
            else
            {
                menufontstyle = FontStyle.Regular;
            }
            menuSettings.Font = new Font("Microsoft Sans Serif", 8.25f, menufontstyle);
            menuSettings.Click += new System.EventHandler(MenuSettings_Click);
            // MenuAbout
            menuAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            menuAbout.Name = "MenuAbout";
            menuAbout.Size = new System.Drawing.Size(144, 22);
            menuAbout.Text = "About";
            menuSettings.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular);
            menuAbout.Click += new System.EventHandler(MenuAbout_Click);
            // MenuExit
            menuExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            menuExit.Name = "MenuExit";
            menuExit.Size = new System.Drawing.Size(144, 22);
            menuExit.Text = "E&xit";
            if (Settings.TrayiconExitbold)
            {
                menufontstyle = FontStyle.Bold;
            }
            else
            {
                menufontstyle = FontStyle.Regular;
            }
            menuExit.Font = new Font("Microsoft Sans Serif", 8.25f, menufontstyle);
            menuExit.Click += new System.EventHandler(MenuExit_Click);

            if (Settings.ProgramFirstrun || forcefirstrun)
            {
                icon.ShowBalloonTip(5000, "NoteFly", "You can access NoteFly functions via this trayicon.", ToolTipIcon.Info);
            }
            Application.Run();
        }

        #endregion Methods



        #region menu events
        /// <summary>
        /// There is left clicked on the icon.
        /// If actionleftclick is 0 do nothing.
        /// If actionleftclick is 1 actived all notes.
        /// If actionleftclick is 2 create a new note.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private static void Icon_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Settings.TrayiconLeftclickaction == 1)
                {
                    notes.BringToFrontNotes();
                }
                else if (Settings.TrayiconLeftclickaction == 2)
                {
                    if (!frmnewnoteshowed)
                    {
                        FrmNewNote frmnewnote = new FrmNewNote(notes);
                        frmnewnote.Show();
                        frmnewnoteshowed = true;
                    }
                    else
                    {
                        frmnewnoteshowed = false;
                    }
                }
            }
        }

        /// <summary>
        /// Open new note window.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event argument</param>
        private static void MenuNewNote_Click(object sender, EventArgs e)
        {
            FrmNewNote newnotefrm = new FrmNewNote(notes);
            newnotefrm.Show();
        }

        /// <summary>
        /// open manage notes window
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event argument</param>
        private static void MenuManageNotes_Click(object sender, EventArgs e)
        {
            FrmManageNotes frmmanagenotes = new FrmManageNotes(notes);
            frmmanagenotes.Show();
        }

        /// <summary>
        /// Open settings window.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event argument</param>
        private static void MenuSettings_Click(object sender, EventArgs e)
        {
            FrmSettings settings = new FrmSettings(notes);
            settings.Show();
        }

        /// <summary>
        /// Open about window.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event argument</param>
        private static void MenuAbout_Click(object sender, EventArgs e)
        {
            FrmAbout frmabout = new FrmAbout();
            frmabout.Show();
        }

        /// <summary>
        /// User request to shutdown application.
        /// Check if confirm box is needed. 
        /// if confirm box is still open then shutdown anyway.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event argument</param>
        private static void MenuExit_Click(object sender, EventArgs e)
        {
            if (Settings.ConfirmExit)
            {
                // two times exit in contextmenu systray icon will always exit.
                if (!confirmexitshowed)
                {
                    confirmexitshowed = true;
                    string assemblyProduct;
                    object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                    if (attributes.Length == 0)
                    {
                        assemblyProduct = String.Empty;
                    }

                    assemblyProduct = ((AssemblyProductAttribute)attributes[0]).Product;
                    DialogResult resdlgconfirmexit = MessageBox.Show("Are sure you want to exit " + assemblyProduct + "?", "confirm exit", MessageBoxButtons.YesNo);
                    if (resdlgconfirmexit == DialogResult.No)
                    {
                        confirmexitshowed = false;
                        return;
                    }
                }
            }

            ExitApplication();
        }

        /// <summary>
        /// Terminate application
        /// </summary>
        private static void ExitApplication()
        {
            components.Dispose();
            Application.Exit();
        }

        #endregion
    }
}
