//-----------------------------------------------------------------------
// <copyright file="TrayIcon.cs" company="GNU">
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

#define windows //platform can be: windows, linux, macos //platform can be: windows, linux, macos
using System;
[assembly: CLSCompliant(true)]
namespace NoteFly
{
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Threading;
#if DEBUG
    using System.Diagnostics;
#endif

    /// <summary>
    /// Program class, main entry application.
    /// </summary>
    public class Program
    {
        #region Fields (3)

        /// <summary>
        /// Constant download page url.
        /// </summary>
        private const string DOWNLOADPAGE = "http://www.notefly.tk/downloads.php";

        /// <summary>
        /// Reference to notes class.
        /// </summary>
        private static Notes notes;

        /// <summary>
        /// Reference to trayicon
        /// </summary>
        private static TrayIcon trayicon;

        #endregion Fields

        #region Properties (5)

        /// <summary>
        /// Gets the application data folder.
        /// </summary>
        public static string AppDataFolder
        {
            get
            {
#if windows
                return System.Environment.GetEnvironmentVariable("APPDATA") + "\\.NoteFly2\\";
#elif linux
                return System.Environment.GetEnvironmentVariable("HOME") + "/.NoteFly2/";
#elif macos
                return "???";
#else
                return "COMPILE_ERROR";
#endif
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
        /// Gets the version of this programme as a formatted string.
        /// </summary>
        public static string AssemblyVersionAsString
        {
            get
            {
                short[] version = GetVersion();
                return version[0] + "." + version[1] + "." + version[2];
            }
        }

        /// <summary>
        /// Gets the application version quality.
        /// alpha(=bugs for sure), beta(=bugs are likely), rc(=more testing still needed) or nothing for final(=ready for production)
        /// </summary>
        public static string AssemblyVersionQuality
        {
            get
            {
                return "beta3";
            }
        }

        /// <summary>
        /// Gets the folder where NoteFly is installed.
        /// Folder where assebly is located.
        /// </summary>
        public static string InstallFolder
        {
            get
            {
                return Directory.GetParent(Assembly.GetExecutingAssembly().Location).ToString();
            }
        }

        #endregion Properties

        #region Methods (5)

        // Public Methods (4) 

        /// <summary>
        /// Gets the application version number as an array.
        /// </summary>
        /// <returns>a string containing the version number of this application in the form of major.minur.build number</returns>
        public static short[] GetVersion()
        {
            short[] version = new short[3];
            version[0] = Convert.ToInt16(Assembly.GetExecutingAssembly().GetName().Version.Major);
            version[1] = Convert.ToInt16(Assembly.GetExecutingAssembly().GetName().Version.Minor);
            version[2] = Convert.ToInt16(Assembly.GetExecutingAssembly().GetName().Version.Build);
            return version;
        }

        /// <summary>
        /// If set ask the user if the want to load the link.
        /// </summary>
        /// <param name="uri_text">the uniform resource location</param>
        public static void LoadLink(string uri_text)
        {
            if (Settings.confirmLinkclick)
            {
                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Are you sure you want to visted: " + uri_text, "Are you sure?", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    Program.LoadURI(uri_text);
                }
            }
            else
            {
                Program.LoadURI(uri_text);
            }
        }

        /// <summary>
        /// Main entry point programme.
        /// load settings, parser parameters, create notes list and trayicon.
        /// </summary>
        /// <param name="args">parameters</param>
        [STAThread]
        public static void Main(string[] args)
        {
            //a suggestion to "protect" against insecure Dynamic Library Loading vulnerabilities.
            //it does not fix it, it makes it harder to exploit if insecure dll loading exist.
            //NoteFly uses APPDATA and TEMP variables and systemroot is required for LinkLabel control.
            //This is OS specific
            // /*
#if windows
            SetDllDirectory(String.Empty); //removes notefly folder as ddl search path
            Environment.SetEnvironmentVariable("PATH", String.Empty);//removes dangourse %PATH% as dll search path
            Environment.SetEnvironmentVariable("windir", String.Empty);//removes %windir%
            Environment.SetEnvironmentVariable("ProgramFiles", String.Empty);
            Environment.SetEnvironmentVariable("SystemDrive", String.Empty);
            Environment.SetEnvironmentVariable("CommonProgramFiles", String.Empty);
            Environment.SetEnvironmentVariable("TMP", String.Empty); //removes %TMP%, NoteFly uses %TEMP% instead only.
            // */
#endif
#if DEBUG
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
#endif
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(true);
            if (!xmlUtil.LoadSettings())
            {
                xmlUtil.WriteDefaultSettings();
                xmlUtil.LoadSettings();
            }
#if DEBUG
            stopwatch.Stop();
            Log.Write(LogType.info, "Settings load time: " + stopwatch.ElapsedMilliseconds + " ms");
#endif
            bool visualstyle = true;
            bool resetpositions = false;
            // override settings with supported parameters
            if (System.Environment.GetCommandLineArgs().Length > 1)
            {
                foreach (string arg in args)
                {
                    switch (arg)
                    {
                        // Forces the programme to setup the first run notefly info again.
                        case "/forcefirstrun":
                            Settings.programFirstrun = true;
                            break;
                        // disabletransparency parameter is for OS that don't support transparency, so they can still show notes.
                        // because transparency is on by default.
                        case "/disabletransparency":
                            Settings.notesTransparencyEnabled = false;
                            break;
                        // Turn off all highlighting functions in case highlighting was turned on and it let NoteFly crash on startup.
                        case "/disablehighlighting":
                            Settings.highlightHyperlinks = false;
                            Settings.highlightHTML = false;
                            Settings.highlightPHP = false;
                            Settings.highlightSQL = false;
                            break;
                        // Turn off all social functions on startup.
                        case "/disablesocial":
                            Settings.socialEmailEnabled = false;
                            ////Settings.socialFacebookEnabled = false;
                            ////Settings.socialTwitterEnabled = false;
                            break;
                        // Turn all logging features on at startup. 
                        // Handy in case NoteFly crashes at startup and logging was turned off.
                        case "/logall":
                            Settings.programLogException = true;
                            Settings.programLogError = true;
                            Settings.programLogInfo = true;
                            break;
                        // turn off xp visual style.
                        case "/disablevisualstyles":
                            visualstyle = false; // about ~400ms slower on my system on display time notes.
                            break;
                        // rescue option for notes loading out of screen.
                        case "/resetpositions":
                            resetpositions = true;
                            break;
                       // overwrite settings file with default settings.
                        case "/resetsettings":
                            xmlUtil.WriteDefaultSettings();
                            break;
                    }
                }
            }

            if (visualstyle)
            {
                System.Windows.Forms.Application.EnableVisualStyles();
            }

            Highlight.InitHighlighter();
            notes = new Notes(resetpositions);
            trayicon = new TrayIcon(notes);

            if (Settings.updatecheckEverydays > 0)
            {
                DateTime lastupdate = DateTime.Parse(Settings.updatecheckLastDate);
                if (lastupdate.AddDays(Settings.updatecheckEverydays) <= DateTime.Now)
                {
                    Thread updatethread = new Thread(UpdateCheck);
                    updatethread.Start();
                }
            }

            Highlight.DeinitHighlighter();
            System.Windows.Forms.Application.Run();
        }

        /// <summary>
        /// Do update check.
        /// </summary>
        public static void UpdateCheck()
        {
            Thread.Sleep(500);
            Settings.updatecheckLastDate = DateTime.Now.ToString();
            xmlUtil.WriteSettings();
            short[] thisversion = GetVersion();
            string latestversionquality = Program.AssemblyVersionQuality;
            short[] latestversion = xmlUtil.GetLatestVersion(out latestversionquality);
            bool updateavailible = false;
            for (int i = 0; i < thisversion.Length; i++)
            {
                if (thisversion[i] < latestversion[i] && latestversion[i] >= 0)
                {
                    updateavailible = true;
                    break;
                }
            }

            if (updateavailible || Program.AssemblyVersionQuality != latestversionquality)
            {
                StringBuilder sbmsg = new StringBuilder();
                sbmsg.AppendLine("There's a new version availible.");
                sbmsg.Append("Your version: ");
                sbmsg.AppendLine(Program.AssemblyVersionAsString + " "+Program.AssemblyVersionQuality);
                sbmsg.Append("New version: ");
                sbmsg.AppendLine(latestversion[0] + "." + latestversion[1] + "." + latestversion[2] +" "+ latestversionquality);
                sbmsg.Append("Do you want to go to the download page now?");
                System.Windows.Forms.DialogResult updres = System.Windows.Forms.MessageBox.Show(sbmsg.ToString(),"update available", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Asterisk);
                if (updres == System.Windows.Forms.DialogResult.Yes)
                {
                    Program.LoadURI(DOWNLOADPAGE);
                }
            }
        }
        // Private Methods (1) 

        /// <summary>
        /// Actual loads the url.
        /// Also provide some cursor feedback.
        /// </summary>
        /// <param name="uri_text">The uri to load</param>
        private static void LoadURI(string uri_text)
        {
            try
            {
                UriBuilder uri = null;
                try
                {
                    uri = new UriBuilder(uri_text);
                }
                catch (UriFormatException)
                {
                    return;
                }

                if (String.IsNullOrEmpty(uri.Scheme))
                {
                    uri.Scheme = "http://";
                }

                System.Diagnostics.ProcessStartInfo procstartinfo = new System.Diagnostics.ProcessStartInfo(uri.Uri.AbsoluteUri.ToString());
                procstartinfo.ErrorDialog = true;
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.AppStarting;
                try
                {
                    System.Diagnostics.Process.Start(procstartinfo);
                }
                catch (System.ComponentModel.Win32Exception w32exc)
                {
                    Log.Write(LogType.exception, w32exc.Message);
                    return;
                }
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

#if windows
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool SetDllDirectory(string pathName);
#endif

        #endregion Methods
    }
}
