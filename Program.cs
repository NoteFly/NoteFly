//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="GNU">
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
        #region Fields (3)

        /// <summary>
        /// All the enabled plugins
        /// FIXME: Make CLS-compliant
        /// </summary>
        public static IPlugin.IPlugin[] pluginsenabled;

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
                return string.Empty;
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
        /// Gets the application version number as an array.
        /// </summary>
        /// <returns>a string containing the version number of this application in the form of major.minor.build number</returns>
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
             * NoteFly uses APPDATA and TEMP variables and systemroot.
             * Systemroot is required by the LinkLabel control.
             * Plugins should not use these environment variables
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
                        case "-forcefirstrun":
                            Settings.ProgramFirstrun = true;
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

#if DEBUG
                // Import a note file.
                // Copy note file to note save path.
                // this is used for fuzzing right now
                if (args[1] == "-importnote")
                {
                    if (args.Length == 2) 
                    {
                        if (File.Exists(args[2]))
                        {
                            string newnotefile = Path.Combine(Settings.NotesSavepath, notes.GetNoteFilename("argimp"));
                            Directory.Move(args[2], newnotefile);
                        }
                    }
                }
#endif
            }

#if windows
            if (!Settings.ProgramSuspressWarnAdmin)
            {
                // Security measure, show warning if runned with dangerous administrator rights.
                System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
                System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
                if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
                {
                    System.Windows.Forms.DialogResult dlganswer = System.Windows.Forms.MessageBox.Show("You are now running " + Program.AssemblyTitle + " as elevated Administrator.\r\nWhich is not recommended because of security.\r\nPress OK if your understand the risks and want to hide this message in the future.", "Elevated administrator", System.Windows.Forms.MessageBoxButtons.OKCancel);
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
                System.Windows.Forms.DialogResult dlgres = System.Windows.Forms.MessageBox.Show("The programme is already running.\nLoad an other instance? (not recommeded)", "already running", System.Windows.Forms.MessageBoxButtons.YesNo);
                if (dlgres == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }

            SyntaxHighlight.InitHighlighter();
            notes = new Notes(resetpositions);
            if (Settings.ProgramPluginsAllEnabled)
            {
                Program.pluginsenabled = GetPlugins(true);
            }

            trayicon = new TrayIcon(notes);
            if (!Settings.ProgramFirstrun)
            {
                // disable the firstrun the next time.
                Settings.ProgramFirstrun = true;
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

            if (Settings.UpdatecheckEverydays > 0)
            {
                DateTime lastupdate = DateTime.Parse(Settings.UpdatecheckLastDate);
                if (lastupdate.AddDays(Settings.UpdatecheckEverydays) <= DateTime.Now)
                {
                    Thread updatethread = new Thread(UpdateCheckThread);
                    updatethread.Start();
                }
            }

            SyntaxHighlight.DeinitHighlighter();
            System.Windows.Forms.Application.Run();
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
        public static string UpdateCheck()
        {
            xmlUtil.WriteSettings();
            string downloadurl;
            bool updatehigherversion = false;
            bool updatesameversion = true;
            short[] thisversion = GetVersion();
            string latestversionquality = Program.AssemblyVersionQuality;
            if (IsNetworkConnected())
            {
                short[] latestversion = xmlUtil.GetLatestVersion(out latestversionquality, out downloadurl);
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
                        sbmsg.AppendLine("There's a new version availible.");
                        sbmsg.Append("Your version: ");
                        sbmsg.AppendLine(Program.AssemblyVersionAsString + " " + Program.AssemblyVersionQuality);
                        sbmsg.Append("New version: ");
                        sbmsg.AppendLine(latestversion[0] + "." + latestversion[1] + "." + latestversion[2] + " " + latestversionquality);
                        sbmsg.Append("Do you want to download and install the new version now?");
                        System.Windows.Forms.DialogResult updres = System.Windows.Forms.MessageBox.Show(sbmsg.ToString(), "update available", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Asterisk);
                        if (updres == System.Windows.Forms.DialogResult.Yes)
                        {
                            FrmUpdater frmupdater = new FrmUpdater(downloadurl);
                            frmupdater.Show();
                        }
                    }
                    else
                    {
                        throw new ApplicationException("Downloadurl is unexcepted unknown.");
                    }
                }
            }
            else
            {
                Log.Write(LogType.info, "Update check aborted, no network connection.");
            }

            return DateTime.Now.ToString();
        }

        /// <summary>
        /// Load plugin .dll files from pluginfolder
        /// FIXME: Make CLS-compliant
        /// </summary>
        /// <param name="onlyenabled">True to only get the the plugins that are enable</param>
        /// <returns>A array with plugins</returns>
        public static IPlugin.IPlugin[] GetPlugins(bool onlyenabled)
        {
            System.Collections.Generic.List<IPlugin.IPlugin> pluginslist = new System.Collections.Generic.List<IPlugin.IPlugin>();
            if (Directory.Exists(Settings.ProgramPluginsFolder))
            {
                string[] pluginfiles = GetFilesPluginDir();
                string[] pluginsenabled = Settings.ProgramPluginsEnabled.Split('|'); // | is illegal as filename.
                for (int i = 0; i < pluginfiles.Length; i++)
                {
                    try
                    {
                        // blacklist sqllite drivers not to load as notefly plugin, so sqllite drivers could be used by plugins.
                        if (pluginfiles[i] == "System.Data.SQLite.DLL" || pluginfiles[i] == "SQLite3.dll")
                        {
                            break;
                        }

                        // Get if plugin is enabled.
                        bool pluginenabled = IsPluginEnabled(pluginsenabled, pluginfiles[i]);
                        if ((pluginenabled && onlyenabled) || !onlyenabled)
                        {
                            System.Reflection.Assembly pluginassembly = null;
                            pluginassembly = System.Reflection.Assembly.LoadFrom(Path.Combine(Settings.ProgramPluginsFolder, pluginfiles[i]));
                            if (pluginassembly != null)
                            {
                                foreach (Type curplugintype in pluginassembly.GetTypes())
                                {
                                    if (curplugintype.IsPublic && !curplugintype.IsAbstract && !curplugintype.IsSealed)
                                    {
                                        Type plugintype = pluginassembly.GetType(curplugintype.ToString(), false, true);
                                        if (plugintype != null)
                                        {
                                            IPlugin.IPlugin iplugin = (IPlugin.IPlugin)Activator.CreateInstance(pluginassembly.GetType(curplugintype.ToString()));
                                            iplugin.Host = NoteFly.Program.notes; // FIXME this call class only
                                            iplugin.Register(pluginenabled, pluginfiles[i]);
                                            pluginslist.Add(iplugin);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        const string CANTLOADPLUGIN = "Can't load plugin: ";
                        Log.Write(LogType.exception, CANTLOADPLUGIN + pluginfiles[i] + " " + ex.Message);
                    }
                }
            }
            else
            {
                const string PLUGINFOLDERNOTEXIST = "Plugin folder does not exist.";
                Log.Write(LogType.info, PLUGINFOLDERNOTEXIST);
            }

            return pluginslist.ToArray();
        }

        /// <summary>
        /// Get the name of the plugin.
        /// </summary>
        /// <param name="pluginassembly">The plugin assembly</param>
        /// <returns>The name of the plugin</returns>
        public static string GetPluginName(Assembly pluginassembly)
        {
            string pluginname = "untitled";
            object[] atttitle = pluginassembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (atttitle.Length > 0)
            {
                AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)atttitle[0];
                if (titleAttribute.Title != string.Empty)
                {
                    if (titleAttribute.Title.Length > 150)
                    {
                        pluginname = titleAttribute.Title.Substring(0, 150);
                    }
                    else
                    {
                        pluginname = titleAttribute.Title;
                    }
                }
            }

            return pluginname;
        }

        /// <summary>
        /// Get author or author company of the plugin.
        /// </summary>
        /// <param name="pluginassembly">The plugin assembly</param>
        /// <returns>The author or company of the plugin</returns>
        public static string GetPluginAuthor(Assembly pluginassembly)
        {
            string pluginauthor = "unknown";
            object[] attributes = pluginassembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            if (attributes.Length != 0)
            {
                if (((AssemblyCompanyAttribute)attributes[0]).Company.Length > 150)
                {
                    pluginauthor = ((AssemblyCompanyAttribute)attributes[0]).Company.Substring(0, 150);
                }
                else
                {
                    pluginauthor = ((AssemblyCompanyAttribute)attributes[0]).Company;
                }
            }

            return pluginauthor;
        }

        /// <summary>
        /// Get description of the plugin.
        /// </summary>
        /// <param name="pluginassembly">The plugin assembly</param>
        /// <returns>The description of the plugin</returns>
        public static string GetPluginDescription(Assembly pluginassembly)
        {
            string plugindescription = string.Empty;
            object[] attdesc = pluginassembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if (attdesc.Length != 0)
            {
                if (((AssemblyDescriptionAttribute)attdesc[0]).Description.Length > 255)
                {
                    plugindescription = ((AssemblyDescriptionAttribute)attdesc[0]).Description.Substring(0, 255);
                }
                else
                {
                    plugindescription = ((AssemblyDescriptionAttribute)attdesc[0]).Description;
                }
            }

            return plugindescription;
        }

        /// <summary>
        /// Get version of the plugin as string.
        /// </summary>
        /// <param name="pluginassembly">The plugin assembly</param>
        /// <returns>The version of the plugin as string</returns>
        public static string GetPluginVersion(Assembly pluginassembly)
        {
            if (pluginassembly.GetName().Version != null)
            {
                return pluginassembly.GetName().Version.ToString();
            }
            else
            {
                return "unknown";
            }
        }

        /// <summary>
        /// Waits 1000ms before doing a update check.
        /// </summary>
        private static void UpdateCheckThread()
        {
            Thread.Sleep(1000);
            Settings.UpdatecheckLastDate = UpdateCheck();
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

        /// <summary>
        /// Get all dll files in the plugin directory
        /// </summary>
        /// <returns>All dll filenames as string array</returns>
        private static string[] GetFilesPluginDir()
        {
            string[] pluginfilepaths = Directory.GetFiles(Settings.ProgramPluginsFolder, "*.dll", SearchOption.TopDirectoryOnly);
            string[] pluginfiles = new string[pluginfilepaths.Length];
            for (int i = 0; i < pluginfilepaths.Length; i++)
            {
                pluginfiles[i] = Path.GetFileName(pluginfilepaths[i]);
            }

            pluginfilepaths = null;
            return pluginfiles;
        }

        /// <summary>
        /// Get if plugin is enabled.
        /// </summary>
        /// <param name="pluginsenabled">A comma seperated list of enabled plugin assemblies</param>
        /// <param name="pluginfile">The filename without path of the plugin to be check if it's enabled.</param>
        /// <returns>true if pluginfile is enabled.</returns>
        private static bool IsPluginEnabled(string[] pluginsenabled, string pluginfile)
        {
            for (int p = 0; p < pluginsenabled.Length; p++)
            {
                if (pluginsenabled[p] == pluginfile)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check if there is internet connection, if not warn user.
        /// Uses windows API, other platforms return always true at the moment.
        /// </summary>
        /// <remarks>Decreated, used for send to twitter/facebook</remarks>
        /// <returns>true if there is a connection, otherwise return false</returns>
        private static bool IsNetworkConnected()
        {
#if windows
            int desc;
            if (InternetGetConnectedState(out desc, 0))
            {
                return true;
            }
            else
            {
                return false;
            }
#elif !windows
            return true;
#endif
        }

#if windows
        // get network status
        [System.Runtime.InteropServices.DllImport("wininet.dll", EntryPoint = "InternetGetConnectedState")]
        private static extern bool InternetGetConnectedState(out int description, int ReservedValue);

        // change working directory as dll search path
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool SetDllDirectory(string pathName);

        // global hotkey
        ////[System.Runtime.InteropServices.DllImport("user32.dll")]
        ////private static extern int RegisterHotKey(IntPtr hwnd, int id, int fsModifiers, int vk);

        // unregister global hotkey, always unregister before shutdown.
        ////[System.Runtime.InteropServices.DllImport("user32.dll")]
        ////private static extern int UnregisterHotKey(IntPtr hwnd, int id);
#endif

        #endregion Methods
    }
}