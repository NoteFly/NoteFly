//-----------------------------------------------------------------------
// <copyright file="TrayIcon.cs" company="GNU">
// 
// This program is free software; you can redistribute it and/or modify it
// Free Software Foundation; either version 2, 
// or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// </copyright>
//-----------------------------------------------------------------------
#define linux //platform can be: windows, linux, macos

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
        #region Fields (12)

        /// <summary>
        /// container that holds some objects.
        /// </summary>
        private static System.ComponentModel.IContainer components = null;

        /// <summary>
        /// indicated wheter confirm exit is showed.
        /// </summary>
        private static bool confirmexitshowed = false;

        /// <summary>
        /// The trayicon itself.
        /// </summary>
        private static NotifyIcon icon;

        /// <summary>
        /// About menu option
        /// </summary>
        private static ToolStripMenuItem menuAbout;

        /// <summary>
        /// Exit menu option
        /// </summary>
        private static ToolStripMenuItem menuExit;

        /// <summary>
        /// Manage notes menu option
        /// </summary>
        private static ToolStripMenuItem menuManageNotes;

        /// <summary>
        /// New note menu option
        /// </summary>
        private static ToolStripMenuItem menuNewNote;

        /// <summary>
        /// Settings application menu option
        /// </summary>
        private static ToolStripMenuItem menuSettings;

        /// <summary>
        /// The trayicon contextmenu
        /// </summary>
        private static ContextMenuStrip menuTrayIcon;

        /// <summary>
        /// Is the creation of a new note being showed.
        /// </summary>
        private static bool newnoteshowed = false;

        /// <summary>
        /// Notes class has a list an methodes for accessing notes.
        /// </summary>
        private static Notes notes;

        /// <summary>
        /// Is transparecy on.
        /// </summary>
        private static bool transparency = true;

        #endregion Fields

        #region Properties (1)

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

        #region Methods (3)

        // Private Methods (3) 

        /// <summary>
        /// get actionleftclick setting
        /// </summary>
        /// <returns>The action setting as short.</returns>
        private static short GetActionLeftClick()
        {
            xmlHandler getSettings = new xmlHandler(true);
            return Convert.ToInt16(getSettings.getXMLnodeAsInt("actionleftclick"));
        }

        /// <summary>
        /// get defaultcolor setting
        /// </summary>
        /// <returns>The default color for note setting as short.</returns>
        private static short GetDefaultColor()
        {
            xmlHandler xmlSettings = new xmlHandler(true);
            short color = Convert.ToInt16(xmlSettings.getXMLnodeAsInt("defaultcolor"));

            if (color == 7)
            {
                color = Convert.ToInt16(new Random().Next(0, 7));
            }

            return color;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            components = new System.ComponentModel.Container();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            transparency = true;
            bool firstrun = false;

            //disabletransparency parameter is for OS that don't support transparency, so they still can launch this programme.
            if (System.Environment.GetCommandLineArgs().Length > 1)
            {
                if (System.Environment.GetCommandLineArgs()[1] == "/forcefirstrun")
                {
                    firstrun = true;
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
                }
                else if (System.Environment.GetCommandLineArgs()[1] == "/disabletransparency")
                {
                    transparency = false;
                }
            }

            //start loading notes.
            notes = new Notes(firstrun);

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
            menuExit} );
            icon.ContextMenuStrip.ShowImageMargin = false;
            icon.ContextMenuStrip.Size = new System.Drawing.Size(145, 114);

            // MenuNewNote
            menuNewNote.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            menuNewNote.Name = "MenuNewNote";
            menuNewNote.Size = new System.Drawing.Size(144, 22);
            menuNewNote.Text = "&Create a new note";
            menuNewNote.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
            menuNewNote.Click += new System.EventHandler(MenuNewNote_Click);
            // MenuManageNotes
            menuManageNotes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            menuManageNotes.Name = "listToolStripMenuItem";
            menuManageNotes.Size = new System.Drawing.Size(144, 22);
            menuManageNotes.Text = "&Manage notes";
            menuManageNotes.Click += new System.EventHandler(MenuManageNotes_Click);
            // MenuSettings
            menuSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            menuSettings.Name = "MenuSettings";
            menuSettings.Size = new System.Drawing.Size(144, 22);
            menuSettings.Text = "&Settings";
            menuSettings.Click += new System.EventHandler(MenuSettings_Click);
            // MenuAbout
            menuAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            menuAbout.Name = "MenuAbout";
            menuAbout.Size = new System.Drawing.Size(144, 22);
            menuAbout.Text = "About";
            menuAbout.Click += new System.EventHandler(MenuAbout_Click);
            // MenuExit
            menuExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            menuExit.Name = "MenuExit";
            menuExit.Size = new System.Drawing.Size(144, 22);
            menuExit.Text = "E&xit";
            menuExit.Click += new System.EventHandler(MenuExit_Click);

            if (firstrun)
            {
                icon.ShowBalloonTip(5000, "NoteFly", "You can access NoteFly functions via this systray icon.", ToolTipIcon.Info);
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
                short actionleftclick = GetActionLeftClick();
                if (actionleftclick == 1)
                {
                    for (int i = 0; i < notes.NumNotes; i++)
                    {
                        notes.GetNotes[i].Activate();
                    }
                }
                else if (actionleftclick == 2)
                {
                    if (!newnoteshowed)
                    {
                        FrmNewNote newnote = new FrmNewNote(notes, GetDefaultColor());
                        newnote.Show();
                        newnoteshowed = true;
                    }
                    else
                    {
                        newnoteshowed = false;
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
            FrmNewNote newnote = new FrmNewNote(notes, GetDefaultColor());
            newnote.Show();
        }

        /// <summary>
        /// open manage notes window
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">event argument</param>
        private static void MenuManageNotes_Click(object sender, EventArgs e)
        {
            FrmManageNotes managenotes = new FrmManageNotes(notes, transparency, GetDefaultColor());
            managenotes.Show();
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
            FrmAbout about = new FrmAbout();
            about.Show();
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
            xmlHandler getSetting = new xmlHandler(true);

            if (getSetting.getXMLnodeAsBool("confirmexit"))
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
