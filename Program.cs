//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="NoteFly">
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
using System;

[assembly: CLSCompliant(true)]

namespace NoteFly
{
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// Program class, main entry application.
    /// </summary>
    public sealed class Program
    {
        #region Fields (2)

        /// <summary>
        /// Reference to notes class.
        /// </summary>
        public static Notes notes;

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
                return Path.Combine(System.Environment.GetEnvironmentVariable("APPDATA"), ".NoteFly2");
#elif linux
                if (System.Environment.GetEnvironmentVariable("HOME") != null)
                {
                    return Path.Combine(System.Environment.GetEnvironmentVariable("HOME"), ".NoteFly2");
                }
                else
                {
                    throw new ApplicationException("Can't find home folder for storing notefly settings.\nRunning on wrong platform?");
                }
#elif macos
                return "???";
#else
                return "COMPILE_ERROR_UNKNOWN_PLATFORM";
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
                    if (!string.IsNullOrEmpty(titleAttribute.Title))
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
                return "alpha1";
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

        /// <summary>
        /// Gets the application version numbers, without the .net 'revision' number, as an array 
        /// <remarks>The .NET 'build' number is actally called a release number because it needs to be manually increased.
        /// And the .NET 'revision' number is actaully the build number, because this number is changed every build/compile of the program.
        /// (I know this is confuzzing blame the .NET creators.)</remarks>
        /// </summary>
        /// <returns>a string containing the version number of this application in the form of major.minor.release/'build' number</returns>
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
        /// <param name="allow_ask">Allow to ask user if it wants to visit a url.</param>
        public static void LoadLink(string uri_text, bool allow_ask)
        {
            if (Settings.ConfirmLinkclick && allow_ask)
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
        /// <param name="args">The programme parameters</param>
        [STAThread]
        public static void Main(string[] args)
        {
            /*
             * a suggestion to "protect" against insecure Dynamic Library Loading vulnerabilities in windows
             * it does not fix it, it makes it harder to exploit if insecure dll loading exist.
             * NoteFly uses APPDATA, TEMP and systemroot variables.
             * Systemroot is required by the LinkLabel control.
             * Plugin developers should not rely on environment variables
             */
#if windows
            SetDllDirectory(string.Empty);                                     // removes notefly current working directory as ddl search path
            Environment.SetEnvironmentVariable("PATH", string.Empty);          // removes dangourse %PATH% as dll search path
            Environment.SetEnvironmentVariable("windir", string.Empty);        // removes %windir%
            Environment.SetEnvironmentVariable("SystemDrive", string.Empty);   // removes %SystemDrive%
            Environment.SetEnvironmentVariable("CommonProgramFiles", string.Empty); // removes %CommonProgramFiles%
            Environment.SetEnvironmentVariable("USERPROFILE", string.Empty);   // removes %USERPROFILE%
            Environment.SetEnvironmentVariable("TMP", string.Empty);           // removes %TMP%, Do not remove %TEMP% NoteFly needs this for logging if appdata is wrong.
#endif

#if DEBUG
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
#endif

            System.Windows.Forms.Application.ThreadException += new ThreadExceptionEventHandler(UnhanledThreadExceptionHanhler);
            System.Windows.Forms.Application.SetUnhandledExceptionMode(System.Windows.Forms.UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionHandler);
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(true);
            if (!xmlUtil.LoadSettings())
            {
                // settings.xml does not exist create default settings.xml file
                xmlUtil.WriteDefaultSettings();
                xmlUtil.LoadSettings();
            }

            Program.SetCulture(Settings.ProgramLanguage);
#if DEBUG
            stopwatch.Stop();
            Log.Write(LogType.info, "Settings load time: " + stopwatch.ElapsedMilliseconds + " ms");
#endif
            bool visualstyle;
            bool resetpositions;
            ParserArguments(args, out visualstyle, out resetpositions);

#if windows
            if (!Settings.ProgramSuspressWarnAdmin)
            {
                // Security measure, show warning if runned with dangerous administrator rights.
                System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
                System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
                if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
                {
                    string program_runasadministrator = Strings.T("You are now running {0} as elevated Administrator.\nWhich is not recommended for security.\nPress OK if your understand the risks of running as administrator and want to hide this message in the future.", Program.AssemblyTitle);
                    string program_runasadministratortitle = Strings.T("Elevated administrator");
                    System.Windows.Forms.DialogResult dlganswer = System.Windows.Forms.MessageBox.Show(program_runasadministrator, program_runasadministratortitle, System.Windows.Forms.MessageBoxButtons.OKCancel);
                    if (dlganswer == System.Windows.Forms.DialogResult.OK)
                    {
                        Settings.ProgramSuspressWarnAdmin = true;
                        if (!System.IO.Directory.Exists(Program.AppDataFolder))
                        {
                            Directory.CreateDirectory(Program.AppDataFolder);
                        }

                        xmlUtil.WriteSettings();
                    }
                }
            }
#endif

            if (visualstyle)
            {
#if windows
                System.Windows.Forms.Application.EnableVisualStyles();
#endif
            }

            if (Program.CheckInstancesRunning() > 1)
            {
                string program_alreadyrunning = Strings.T("The programme is already running.\nLoad an other instance? (not recommeded)");
                string program_alreadyrunningtitle = Strings.T("already running");
                System.Windows.Forms.DialogResult dlgres = System.Windows.Forms.MessageBox.Show(program_alreadyrunning, program_alreadyrunningtitle, System.Windows.Forms.MessageBoxButtons.YesNo);
                if (dlgres == System.Windows.Forms.DialogResult.No)
                {
                    // shutdown
                    return;
                }
            }

            SyntaxHighlight.InitHighlighter();
            notes = new Notes(resetpositions);

            trayicon = new TrayIcon(notes);
            if (!Settings.ProgramFirstrunned)
            {
                // disable the firstrun the next time.
                Settings.ProgramFirstrunned = true;
                Settings.UpdatecheckUseGPG = false;
                GPGVerifWrapper gpgverif = new GPGVerifWrapper();
                if (!string.IsNullOrEmpty(gpgverif.GetGPGPath()) && gpgverif != null)
                {
                    Settings.UpdatecheckGPGPath = gpgverif.GetGPGPath();
                    Settings.UpdatecheckUseGPG = true;
                }

                gpgverif = null;
                Log.Write(LogType.info, "firstrun occur");
                xmlUtil.WriteSettings();
            }

            if (!Settings.ProgramLastrunVersion.Equals(Program.AssemblyVersionAsString, StringComparison.Ordinal))
            {
                Settings.ProgramLastrunVersion = Program.AssemblyVersionAsString;
                xmlUtil.WriteSettings();
                Log.Write(LogType.info, "Updated ProgramLastrunVersion setting.");
            }

            if (Settings.UpdatecheckEverydays > 0)
            {
                DateTime lastupdate = DateTime.Parse(Settings.UpdatecheckLastDate);
                if (lastupdate.AddDays(Settings.UpdatecheckEverydays) <= DateTime.Now)
                {
                    Settings.UpdatecheckLastDate = UpdateGetLatestVersion();
                    xmlUtil.WriteSettings();
                }
            }

            SyntaxHighlight.DeinitHighlighter();
            System.Windows.Forms.Application.Run();
        }       

        /// <summary>
        /// Parser the programme arguments
        /// </summary>
        /// <param name="args"></param>
        private static void ParserArguments(string[] args, out bool visualstyle, out bool resetpositions)
        {
            visualstyle = true;
            resetpositions = false;

            // override settings with supported parameters
            if (System.Environment.GetCommandLineArgs().Length > 1)
            {
                foreach (string arg in args)
                {
                    switch (arg)
                    {
                        // Forces the programme to setup the first run notefly info again.
                        case "-forcefirstrun":
                            Settings.ProgramFirstrunned = false;
                            break;

                        // disabletransparency parameter is for OS that don't support transparency, so they can still show notes.
                        // because transparency is on by default.
                        case "-disabletransparency":
                            Settings.NotesTransparencyEnabled = false;
                            break;

                        // Turn off all highlighting functions in case highlighting was turned on and it let NoteFly crash on startup.
                        case "-disablehighlighting":
                            Settings.HighlightHyperlinks = false;
                            Settings.HighlightHTML = false;
                            Settings.HighlightPHP = false;
                            Settings.HighlightSQL = false;
                            break;

                        // Turn off loading of plugins
                        case "-disableplugins":
                            Settings.ProgramPluginsAllEnabled = false;
                            break;

                        // Turn all logging features on at startup. 
                        // Handy in case NoteFly crashes at startup and logging was turned off.
                        case "-logall":
                            Settings.ProgramLogException = true;
                            Settings.ProgramLogError = true;
                            Settings.ProgramLogInfo = true;
                            break;

                        // Turn all logging features off at startup. 
                        case "-lognone":
                            Settings.ProgramLogException = false;
                            Settings.ProgramLogError = false;
                            Settings.ProgramLogInfo = false;
                            break;

                        // turn off xp visual style.
                        case "-disablevisualstyles":
                            visualstyle = false; // about ~400ms slower on my system on display time notes.
                            break;

                        // rescue option for notes loading out of screen.
                        case "-resetpositions":
                            resetpositions = true;
                            break;

                        case "-disablegpg":
                            Settings.UpdatecheckUseGPG = false;
                            break;

#if windows
                        // don't show a warning if notefly is running with administrator priveledges
                        case "-suspressadminwarn":
                            Settings.ProgramSuspressWarnAdmin = true;
                            break;
#endif

                        // overwrite settings file with default settings.
                        case "-resetsettings":
                            xmlUtil.WriteDefaultSettings();
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Set the culture of this programme with a languagecode.
        /// Use english if languagecode is unknown.
        /// </summary>
        /// <param name="culturecode"></param>
        public static void SetCulture(string languagecode)
        {
            System.Globalization.CultureInfo culture;
            try
            {
                culture = System.Globalization.CultureInfo.GetCultureInfo(languagecode);                
            }
            catch (ArgumentException)
            {
                culture = System.Globalization.CultureInfo.GetCultureInfo("en");
                Log.Write(LogType.error, String.Format("Langecode {0} not recognised.", languagecode));
            }

            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
        }

        /// <summary>
        /// Dispose the trayicon and create a new one.
        /// </summary>
        public static void RestartTrayicon()
        {
            trayicon.Dispose();
            trayicon = new TrayIcon(notes);
        }

        /// <summary>
        /// Do update check.
        /// </summary>
        /// <returns>Datetime aof latest update check as string</returns>
        public static string UpdateGetLatestVersion()
        {
            HttpUtil http_updateversion = new HttpUtil(Settings.UpdatecheckURL, System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
            http_updateversion.httpthread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(UpdateCompareVersion);
            if (!http_updateversion.Start())
            {
                System.Windows.Forms.MessageBox.Show("error...."); // todo
            }

            return DateTime.Now.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        private static void UpdateCompareVersion(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            string response = (string)e.Result;
            short[] thisversion = GetVersion();
            string downloadurl = string.Empty;
            string latestversionquality = Program.AssemblyVersionQuality;
            short[] latestversion = xmlUtil.ParserLatestVersion(response, out latestversionquality, out downloadurl);

            bool updatehigherversion = false;
            bool updatesameversion = true;
            for (int i = 0; i < thisversion.Length; i++)
            {
                // check if latestversion[i] (major,minor,release) is bigger and is positive number
                if (thisversion[i] < latestversion[i] && latestversion[i] >= 0)
                {
                    updatehigherversion = true;
                    break;
                }

                if (thisversion[i] != latestversion[i])
                {
                    updatesameversion = false;
                }
            }

            if (updatehigherversion || (updatesameversion && Program.AssemblyVersionQuality != latestversionquality))
            {
                if (!string.IsNullOrEmpty(downloadurl))
                {
                    StringBuilder sbmsg = new StringBuilder();
                    sbmsg.AppendLine(Strings.T("There's a new version availible."));
                    sbmsg.Append(Strings.T("Your version:"));
                    sbmsg.AppendLine(" " + Program.AssemblyVersionAsString + " " + Program.AssemblyVersionQuality);
                    sbmsg.Append(Strings.T("New version:"));
                    sbmsg.AppendLine(" " + latestversion[0] + "." + latestversion[1] + "." + latestversion[2] + " " + latestversionquality);
                    sbmsg.Append(Strings.T("Do you want to download and install the new version now?"));
                    System.Windows.Forms.DialogResult updres = System.Windows.Forms.MessageBox.Show(sbmsg.ToString(), "update available", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Asterisk);
                    if (updres == System.Windows.Forms.DialogResult.Yes)
                    {
                        FrmDownloader frmupdater = new FrmDownloader(downloadurl, Settings.UpdatecheckUseGPG, true, "Downloading update..");
                        frmupdater.Show();
                    }
                }
                else
                {
                    throw new ApplicationException("Downloadurl is unexcepted unknown.");
                }
            }
        }

        /// <summary>
        /// Unhandled exceptions occur
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="args">UnhandledExceptionEvent arguments</param>
        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            ShowExceptionDlg(e);
        }

        /// <summary>
        /// Unhandled thread exceptions occur
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="treadargs">ThreadExceptionEvent arguments</param>
        private static void UnhanledThreadExceptionHanhler(object sender, ThreadExceptionEventArgs treadargs)
        {
            Exception e = treadargs.Exception;
            ShowExceptionDlg(e);
        }

        /// <summary>
        /// Log exception and show exception dialog.
        /// </summary>
        /// <param name="e">Exception object</param>
        private static void ShowExceptionDlg(Exception e)
        {
            if (e == null)
            {
                e = new Exception("Unknown unhandled Exception occurred.");
            }

            if (Settings.ProgramLogException)
            {
                Log.Write(LogType.exception, e.Message + " stacktrace: " + e.StackTrace);
            }

            FrmException frmexc = new FrmException(e.Message, e.StackTrace);
            frmexc.ShowDialog();
        }

        /// <summary>
        /// Check number of instance of this application.
        /// </summary>
        /// <returns>The number of instance running</returns>
        private static int CheckInstancesRunning()
        {
            int instances = 0;
            foreach (Process proc in Process.GetProcesses())
            {
                if (proc.ProcessName.Equals(Program.AssemblyTitle))
                {
                    instances++;
                }
            }

            return instances;
        }

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

                if (string.IsNullOrEmpty(uri.Scheme))
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
        // change working directory as dll search path
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool SetDllDirectory(string pathName);
#endif

        #endregion Methods
    }
}