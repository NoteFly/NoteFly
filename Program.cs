//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2010-2013  Tom
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

/// <summary>
/// Main assembly
/// </summary>
namespace NoteFly
{
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// Program class, main entry application.
    /// </summary>
    public sealed class Program
    {
        #region Fields (5)

        /// <summary>
        /// The default folder name in which notes are stored in application data folder.
        /// </summary>
        private const string DEFAULTNOTESFOLDERNAME = "notes";

        /// <summary>
        /// The default folder name in which plugins are stored in application data folder.
        /// </summary>
        private const string DEFAULTPLUGINSFOLDERNAME = "plugins";

        /// <summary>
        /// Reference to notes class.
        /// </summary>
        private static Notes notes;

        /// <summary>
        /// Reference to the FormManager class.
        /// </summary>
        private static FormManager formmanager;

        /// <summary>
        /// Reference to trayicon
        /// </summary>
        private static TrayIcon trayicon;

        /// <summary>
        /// The current operating system running this program
        /// </summary>
        private static OS currentos;

        /// <summary>
        /// 
        /// </summary>
        private static string updateprogramrsasignature;

        /// <summary>
        /// A more simplified enumeration of PlatformID enum,
        /// not targetting versions of a operating system.
        /// </summary>
        public enum OS { WINDOWS, MACOS, LINUX, UNKNOWN };

        #endregion Fields

        #region Properties (7)

        /// <summary>
        /// Get the current operating system running this program
        /// </summary>
        public static OS CurrentOS
        {
            get
            {
                return currentos;
            }
        }

        /// <summary>
        /// Gets the application data folder.
        /// </summary>
        public static string AppDataFolder
        {
            get
            {
                Program.LoadPlatformOs();
                const string APPDATAFOLDER = "NoteFly";
                if (Program.currentos == OS.WINDOWS)
                {
                    return Path.Combine(System.Environment.GetEnvironmentVariable("APPDATA"), APPDATAFOLDER);
                }
                else if (Program.currentos == OS.LINUX || Program.currentos == OS.MACOS)
                {
                    if (System.Environment.GetEnvironmentVariable("HOME") != null)
                    {
                        return Path.Combine(System.Environment.GetEnvironmentVariable("HOME"), ("." + APPDATAFOLDER));
                    }
                    else
                    {
                        throw new ApplicationException("Can't find home folder for storing notefly settings.");
                    }
                }
                else
                {
                    throw new ApplicationException("unsupported platform.");
                }
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
        /// "b" quick rerelease of current version to address a version number issue only.  
        /// </summary>
        public static string AssemblyVersionQuality
        {
            get
            {
                return string.Empty;
                //return "b";
                //return "rc";
                //return "beta";
                //return "alpha";
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

        /// <summary>
        /// Gets a reference to Notes class
        /// </summary>
        public static Notes Notes
        {
            get
            {
                return notes;
            }
        }

        /// <summary>
        /// Gets a reference to FormManger class
        /// </summary>
        public static FormManager Formmanager
        {
            get
            {
                return formmanager;
            }
        }

        #endregion Properties

        #region Methods (21)

        /// <summary>
        /// Gets the application version numbers, without the .net 'revision' number, as an array 
        /// <remarks>The .NET 'build' number is actally called a release number because it needs to be manually increased.
        /// And the .NET 'revision' number is actaully the build number, because this number is changed every build/compile of the program.
        /// (I know this is confuzzing blame the .NET creators.)</remarks>
        /// </summary>
        /// <returns>A string containing the version number of this application in the form of major.minor.release/'dotNET build' number.</returns>
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
        /// <param name="urlwithoutprotocolhandler">The uniform resource location.</param>
        /// <param name="askforvisit">Allow to ask user if it wants to visit a url.</param>
        public static void LoadLink(string url, bool askforvisit)
        {
            String protocolhandler = "";
            if (!url.Contains("://")) {
                protocolhandler = "https:";
                if (!Settings.ProgramHttpsLinks)
                {
                    protocolhandler = "http:";
                }
            }

            String fullurl = protocolhandler + url;
            if (Settings.ConfirmLinkclick && askforvisit)
            {
                string confirmlinkvisittext = Strings.T("Are you sure you want to visit: {0} ?", fullurl);
                string confirmlinkvisittitle = Strings.T("Are you sure?");
                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(confirmlinkvisittext, confirmlinkvisittitle, System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
                if (result != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }
            }

            try
            {
                System.Diagnostics.ProcessStartInfo procstartinfo = new System.Diagnostics.ProcessStartInfo(url);
                procstartinfo.ErrorDialog = true;
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.AppStarting;
                try
                {
                    System.Diagnostics.Process.Start(procstartinfo);
                }
                catch (System.ComponentModel.Win32Exception w32exc)
                {
                    Log.Write(LogType.exception, w32exc.Message);
                }
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
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
             * a suggestion to "protect" against insecure Dynamic Library Loading vulnerability in windows
             * it does not fix it, it makes it harder to exploit insecure dll loading.
             * NoteFly uses APPDATA, TEMP and SystemRoot variables.
             * A subfolder in %APPDATA% is where NoteFly stores it program settings, skins settings, log etc.
             * %TEMP% is needs for logging if appdatafolder is not found.
             * %SystemRoot% is required by the LinkLabel control to work properly.
             * %SystemDrive% is required by NET framework.
             * Plugin developers should not rely on environment variables. 
             */
            Program.LoadPlatformOs();
            if (Program.CurrentOS == OS.WINDOWS)
            {
                System.Collections.IDictionary environmentVariables = Environment.GetEnvironmentVariables();
                foreach (System.Collections.DictionaryEntry de in environmentVariables)
                {
                    string currentvariable = de.Key.ToString();
                    if (!currentvariable.Equals("APPDATA", StringComparison.OrdinalIgnoreCase) &&
                        !currentvariable.Equals("SystemRoot", StringComparison.OrdinalIgnoreCase) &&
                        !currentvariable.Equals("SystemDrive", StringComparison.OrdinalIgnoreCase) &&
                        !currentvariable.Equals("TEMP", StringComparison.OrdinalIgnoreCase))
                    {
                        Environment.SetEnvironmentVariable(de.Key.ToString(), null);
                    }
                }

                SetDllDirectory(string.Empty); // removes current working directory as dll search path, but requires kernel32.dll by itself to be looked up.
            }

#if DEBUG
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
#endif

            System.Windows.Forms.Application.ThreadException += new ThreadExceptionEventHandler(UnhanledThreadExceptionHandler);
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
            bool newnote = false;
            string newnotetitle;
            string newnotecontent;
            ParserArguments(args, out visualstyle, out resetpositions, out newnote, out newnotetitle, out newnotecontent);
            if (Program.CurrentOS == OS.WINDOWS)
            {
                if (!Settings.ProgramSuspressWarnAdmin)
                {
                    // Security measure, show warning if runned with dangerous administrator rights.
                    System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
                    System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
                    if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
                    {
                        string program_runasadministrator = Strings.T("You are now running {0} as elevated Administrator.\nWhich is not recommended for security.\nPress OK if your understand the risks of running as administrator and want to hide this message in the future.", Program.AssemblyTitle);
                        string program_runasadministratortitle = Strings.T("Elevated administrator");
                        System.Windows.Forms.DialogResult dlganswer = System.Windows.Forms.MessageBox.Show(program_runasadministrator, program_runasadministratortitle, System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Warning);
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
            }

            if (visualstyle)
            {
                if (Program.CurrentOS == OS.WINDOWS)
                {
                    System.Windows.Forms.Application.EnableVisualStyles();
                }
            }

            bool pluginsupdated = false;
            if (Settings.ProgramPluginsAllEnabled)
            {
                pluginsupdated = PluginsManager.UpdatePluginReplaceFiles();
            }

            if (!pluginsupdated)
            {
                if (Program.CheckInstancesRunning() > 1)
                {
                    string program_alreadyrunning = Strings.T("{0} is already running.\nLoad another instance? (not recommended)", Program.AssemblyTitle);
                    string program_alreadyrunningtitle = Strings.T("already running");
                    System.Windows.Forms.DialogResult dlgres = System.Windows.Forms.MessageBox.Show(program_alreadyrunning, program_alreadyrunningtitle, System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    if (dlgres == System.Windows.Forms.DialogResult.No)
                    {
                        // shutdown by don't continue this Main method
                        return;
                    }
                }
            }

            SyntaxHighlight.InitHighlighter();
            Program.notes = new Notes(resetpositions);
            if (Settings.ProgramPluginsAllEnabled)
            {
                PluginsManager.LoadPlugins();
            }

            Program.notes.ShowNotesVisible();
            formmanager = new FormManager(notes);
            trayicon = new TrayIcon(formmanager);

            if (!Settings.ProgramFirstrunned)
            {
                // disable the firstrun the next time.
                Settings.ProgramFirstrunned = true;
                Settings.UpdatecheckUseGPG = false;
                GPGVerifyWrapper gpgverif = new GPGVerifyWrapper();
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

                if (PluginsManager.EnabledPlugins != null)
                {
                    for (int p = 0; p < PluginsManager.EnabledPlugins.Count; p++)
                    {
                        PluginsManager.EnabledPlugins[p].ProgramUpgraded();
                    }
                }
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

            if (newnote)
            {
                formmanager.OpenNewNote(newnotetitle, newnotecontent);
            }
            else
            {
                newnotetitle = null;
                newnotecontent = null;
            }

            SyntaxHighlight.DeinitHighlighter();
            System.Windows.Forms.Application.Run();
        }

        /// <summary>
        /// Get the default notes folder for this programme.
        /// If the folder does not exists create it.
        /// </summary>
        /// <returns>The default notes save path as string.</returns>
        public static string GetDefaultNotesFolder()
        {
            string notesfolder = Path.Combine(Program.AppDataFolder, DEFAULTNOTESFOLDERNAME);
            if (!Directory.Exists(notesfolder))
            {
                Directory.CreateDirectory(notesfolder);
            }

            return notesfolder;
        }

        /// <summary>
        /// Get the default plugins folder for this programme.
        /// If the folder does not exists create it.
        /// </summary>
        /// <returns>The default plugin folder.</returns>
        public static string GetDefaultPluginFolder()
        {
            string pluginsfolder = Path.Combine(Program.AppDataFolder, DEFAULTPLUGINSFOLDERNAME);
            if (!Directory.Exists(pluginsfolder))
            {
                Directory.CreateDirectory(pluginsfolder);
            }

            return pluginsfolder;
        }

        /// <summary>
        /// Set the culture of this programme with a languagecode.
        /// Use english if languagecode is unknown.
        /// </summary>
        /// <param name="languagecode">The languagecode</param>
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
                if (languagecode.Length > 4)
                {
                    Log.Write(LogType.error, string.Format("Langecode not recognised."));
                }
                else
                {
                    Log.Write(LogType.error, string.Format("Langecode {0} not recognised.", languagecode));
                }
            }

            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
        }

        /// <summary>
        /// Dispose the trayicon and create a new one.
        /// </summary>
        public static void RestartTrayicon()
        {
            Program.DisposeTrayicon();
            trayicon = new TrayIcon(formmanager);
        }

        /// <summary>
        ///  Dispose the trayicon
        /// </summary>
        public static void DisposeTrayicon()
        {
            trayicon.Dispose();
        }

        /// <summary>
        /// Do update check.
        /// </summary>
        /// <returns>Datetime aof latest update check as string</returns>
        public static string UpdateGetLatestVersion()
        {
            string postparam = "os=";
            postparam += System.Web.HttpUtility.UrlEncode(Enum.GetName(typeof(Program.OS), Program.currentos));
            HttpUtil http_updateversion = new HttpUtil(Settings.UpdatecheckURL, System.Net.Cache.RequestCacheLevel.NoCacheNoStore, postparam);
            if (!http_updateversion.Start(new System.ComponentModel.RunWorkerCompletedEventHandler(UpdateCompareVersion)))
            {
                Log.Write(LogType.error, "No network connection");
            }

            return DateTime.Now.ToString();
        }

        /// <summary>
        /// Find out if the version numbers given as array is higher
        /// than the required version numbers given as an array.
        /// </summary>
        /// <param name="versionA">A version as array with major, minor and release numbers</param>
        /// <param name="versionB">A version as array with major, minor and release numbers</param>
        /// <returns>Returns -3 if versionB is not valid.
        /// -2 if versionA is not valid.
        /// -1 if versionA is lower than versionB, 
        /// 0 if versionA is equal with versionB,
        /// 1 if versionA is higher than versionB.</returns>
        public static int CompareVersions(short[] versionA, short[] versionB)
        {
            bool continu = true;
            for (int i = 0; i < versionA.Length && continu; i++)
            {
                if (versionA[i] < 0)
                {
                    return -2;
                }
                else if (versionB[i] < 0)
                {
                    return -3;
                }

                if (versionA[i] != versionB[i])
                {
                    continu = false;
                    if (versionA[i] > versionB[i])
                    {
                        return 1;
                    }
                    else if (versionA[i] < versionB[i])
                    {
                        return -1;
                    }
                }
            }

            return 0;
        }

        /// <summary>
        /// Parser a string as a version number array with major, minor, release numbers
        /// </summary>
        /// <param name="versionstring">An string with a version.</param>
        /// <returns>Array with version numbers shorts.
        /// First element is major version number,
        /// second element is minor version number,
        /// third element is release version number.</returns>
        public static short[] ParserVersionString(string versionstring)
        {
            short[] versionparts = new short[3];
            char[] splitchr = new char[1];
            splitchr[0] = '.';
            if (!string.IsNullOrEmpty(versionstring))
            {
                string[] stringversionparts = versionstring.Split(splitchr, StringSplitOptions.None);
                try
                {
                    for (int i = 0; i < versionparts.Length; i++)
                    {
                        versionparts[i] = Convert.ToInt16(stringversionparts[i]);
                    }
                }
                catch (InvalidCastException invcastexc)
                {
                    Log.Write(LogType.exception, invcastexc.Message);
                }
            }
            else
            {
                Log.Write(LogType.exception, "No version string to parser.");
            }

            return versionparts;
        }

        /// <summary>
        /// Figure out the operating system that is running this program.
        /// </summary>
        private static void LoadPlatformOs()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                    Program.currentos = OS.WINDOWS;
                    break;
                case PlatformID.MacOSX:
                    Program.currentos = OS.MACOS;
                    break;
                case PlatformID.Unix:
                    // For historical reasons, Mono reports OSX as Unix, not MacOSX.so additional checks are needed.
                    if (Program.IsRunningOnMac())
                    {
                        Program.currentos = OS.MACOS;
                    }
                    else
                    {
                        Program.currentos = OS.LINUX;
                    }

                    break;
                default:
                    Program.currentos = OS.UNKNOWN;
                    break;
            }
        }

        //From Managed.Windows.Forms/XplatUI
        [DllImport("libc")]
        private static extern int uname(IntPtr buf);

        /// <summary>
        /// Figure out if this is a MacOS operating system running this programme by using uname.
        /// </summary>
        /// <returns></returns>
        private static bool IsRunningOnMac()
        {
            IntPtr buf = IntPtr.Zero;
            try
            {
                buf = Marshal.AllocHGlobal(8192);
                // This is a hacktastic way of getting sysname from uname ()
                if (uname(buf) == 0)
                {
                    string os = Marshal.PtrToStringAnsi(buf);
                    if (os == "Darwin")
                    {
                        return true;
                    }
                }
            }
            catch
            {
                Log.Write(LogType.exception, "Problem detecting if unix platform is MacOS or not.");
            }
            finally
            {
                if (buf != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(buf);
                }
            }

            return false;
        }

        /// <summary>
        /// Parser the programme arguments
        /// </summary>
        /// <param name="arguments">An array of arguments the check</param>
        /// <param name="visualstyle">Use XP visual styles on windows.</param>
        /// <param name="resetpositions">Should all visual notes position get reset.</param>
        private static void ParserArguments(string[] arguments, out bool visualstyle, out bool resetpositions, out bool newnote, out string newnotetitle, out string newnotecontent)
        {
            visualstyle = true;
            resetpositions = false;
            newnote = false;
            newnotetitle = string.Empty;
            newnotecontent = string.Empty;
            if (System.Environment.GetCommandLineArgs().Length <= 1)
            {
                return;
            }

            bool setnewnotetitle = false;
            bool setnewnotecontent = false;
            bool setprogramlanguage = false;
            // override settings with supported parameters
            foreach (string argument in arguments)
            {
                if (setnewnotetitle)
                {
                    newnotetitle = argument;
                    setnewnotetitle = false;
                    continue;
                }
                else if (setnewnotecontent)
                {
                    newnotecontent = argument;
                    setnewnotecontent = false;
                    continue;
                }
                else if (setprogramlanguage)
                {
                    Program.SetCulture(argument);
                    continue;
                }

                switch (argument)
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

                    // Override the fontsize of the trayicon, managenotes and default size in note content to 14 pt.
                    case "-bigfonts":
                        Settings.TrayiconFontsize = 14;
                        Settings.ManagenotesFontsize = 14;
                        Settings.FontContentSize = 14;
                        Settings.FontTitleSize = 18;
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

                    // don't show a warning if notefly is running with administrator priveledges
                    case "-suspressadminwarn":
                        Settings.ProgramSuspressWarnAdmin = true;
                        break;

                    // overwrite settings file with default settings.
                    case "-resetsettings":
                        xmlUtil.WriteDefaultSettings();
                        break;

                    case "-disableupdatecheck":
                        Settings.UpdatecheckEverydays = 0;
                        break;

                    case "-setlanguage":
                        setprogramlanguage = true;
                        break;

                    case "-newnote":
                        newnote = true;
                        break;

                    case "-title":
                        setnewnotetitle = true;
                        break;

                    case "-content":
                        setnewnotecontent = true;
                        break;

                    case "-?":
                        ShowParametersHelp();
                        break;
                }
            }
        }

        /// <summary>
        /// Show help message on supported programme parameters.
        /// </summary>
        private static void ShowParametersHelp()
        {
            StringBuilder sb = new StringBuilder();
            const int COLCHARWIDTH = 22;
            sb.AppendLine(Strings.T("Parameter:".PadRight(COLCHARWIDTH, ' ')) + Strings.T("Description:"));
            sb.AppendLine("-?".PadRight(COLCHARWIDTH, ' ') + Strings.T("Show this parameters help window."));
            sb.AppendLine("-disabletransparency".PadRight(COLCHARWIDTH, ' ') + Strings.T("Disable transparency."));
            sb.AppendLine("-disableplugins".PadRight(COLCHARWIDTH, ' ') + Strings.T("Disable all plugins."));
            sb.AppendLine("-disablehighlighting".PadRight(COLCHARWIDTH, ' ') + Strings.T("Disable any highlighting in notes."));
            sb.AppendLine("-disablevisualstyles".PadRight(COLCHARWIDTH, ' ') + Strings.T("Disable XP visual style."));
            sb.AppendLine("-disablegpg".PadRight(COLCHARWIDTH, ' ') + Strings.T("Disable GnuPG signature checking."));
            sb.AppendLine("-forcefirstrun".PadRight(COLCHARWIDTH, ' ') + Strings.T("Force a first run."));
            sb.AppendLine("-logall".PadRight(COLCHARWIDTH, ' ') + Strings.T("Log exceptions, errors and debug messages."));
            sb.AppendLine("-lognone".PadRight(COLCHARWIDTH, ' ') + Strings.T("Don't log exceptions, errors and debug messages."));
            sb.AppendLine("-newnote".PadRight(COLCHARWIDTH, ' ') + Strings.T("Immediately open a new note after startup."));
            sb.AppendLine("-title".PadRight(COLCHARWIDTH, ' ') + Strings.T("Set title new note."));
            sb.AppendLine("-content".PadRight(COLCHARWIDTH, ' ') + Strings.T("Set content new note."));
            sb.AppendLine("-setlanguage".PadRight(COLCHARWIDTH, ' ') + Strings.T("Set the language(ISO 639-1) of the programme."));
            if (Program.CurrentOS == OS.WINDOWS)
            {
                sb.AppendLine("-suspressadminwarn".PadRight(COLCHARWIDTH, ' ') + "Supress the warning that NoteFly is running");
                sb.AppendLine(string.Empty.PadRight(COLCHARWIDTH, ' ') + Strings.T("with unnecessary administrator privilege."));
            }

            sb.AppendLine("-resetpositions".PadRight(COLCHARWIDTH, ' ') + Strings.T("Reset all positions of visual notes at startup."));
            sb.AppendLine("-resetsettings".PadRight(COLCHARWIDTH, ' ') + Strings.T("Reset all NoteFly settings to default."));
            System.Windows.Forms.TextBox tbtexthelp = new System.Windows.Forms.TextBox();
            tbtexthelp.Font = new System.Drawing.Font("Courier New", 10); // Courier New, every character has the same width.
            tbtexthelp.Location = new System.Drawing.Point(0, 0);
            tbtexthelp.Multiline = true;
            tbtexthelp.Dock = System.Windows.Forms.DockStyle.Fill;
            tbtexthelp.Text = sb.ToString();
            System.Windows.Forms.Form frmhelp = new System.Windows.Forms.Form();
            frmhelp.Width = 600;
            frmhelp.Height = 380;
            frmhelp.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            frmhelp.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            frmhelp.ControlBox = true;
            frmhelp.ShowIcon = false;
            frmhelp.Controls.Add(tbtexthelp);
            tbtexthelp.Select(0, 0);
            frmhelp.Text = string.Format(Strings.T("{0} parameters help", Program.AssemblyTitle));
            frmhelp.Show();
            frmhelp.BringToFront();
        }

        /// <summary>
        /// Compare the latest version and this programme version,
        /// if this is a old release ask to update.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">RunWorkerCompleted event arguments</param>
        private static void UpdateCompareVersion(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            string response = (string)e.Result;
            short[] thisversion = GetVersion();
            string downloadurl = string.Empty;
            string rsasignature = string.Empty;
            string latestversionquality = string.Empty;
            short[] latestversion = xmlUtil.ParserLatestVersion(response, out latestversionquality, out downloadurl, out rsasignature);
            Program.updateprogramrsasignature = rsasignature;

            int compareversionsresult = Program.CompareVersions(latestversion, thisversion);
            if (compareversionsresult > 0 || (compareversionsresult == 0 && Program.AssemblyVersionQuality != latestversionquality))
            {
                Log.Write(LogType.info, "Update check done. New version available.");
                if (!string.IsNullOrEmpty(downloadurl))
                {
                    StringBuilder sbmsg = new StringBuilder();
                    sbmsg.AppendLine(Strings.T("There's a new version of {0} available.", Program.AssemblyTitle));
                    sbmsg.Append(Strings.T("Your version: "));
                    sbmsg.AppendLine(Program.AssemblyVersionAsString + " " + Program.AssemblyVersionQuality);
                    sbmsg.Append(Strings.T("New version: "));
                    sbmsg.AppendLine(latestversion[0] + "." + latestversion[1] + "." + latestversion[2] + " " + latestversionquality);
                    sbmsg.Append(Strings.T("Do you want to download and install the new version now?"));
                    System.Windows.Forms.DialogResult updres = System.Windows.Forms.MessageBox.Show(sbmsg.ToString(), Strings.T("update available"), System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Asterisk);                    
                    if (updres == System.Windows.Forms.DialogResult.Yes)
                    {
                        FrmDownloader frmdownloader = new FrmDownloader(string.Format(Strings.T("Downloading {0} update"), Program.AssemblyTitle));
                        frmdownloader.AllDownloadsCompleted += new FrmDownloader.DownloadCompleetHandler(frmupdater_DownloadCompleetSuccesfull);
                        frmdownloader.Show();
                        if (Settings.UpdatecheckUseGPG)
                        {
                            string[] downloads = new string[2];
                            downloads[0] = downloadurl;
                            downloads[1] = downloadurl + GPGVerifyWrapper.GPGSIGNATUREEXTENSION;
                            frmdownloader.BeginDownload(downloads, Program.GetTempFolder());
                        }
                        else
                        {
                            frmdownloader.BeginDownload(downloadurl, Program.GetTempFolder());
                        }
                    }
                }
                else
                {
                    string networkerrortitle = Strings.T("Network error");
                    string downloadurlunset = Strings.T("Downloadurl is of new vesion is not set.");
                    Log.Write(LogType.exception, downloadurlunset);
                    System.Windows.Forms.MessageBox.Show(downloadurlunset, networkerrortitle, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            else
            {
                Log.Write(LogType.info, "Update check done. You have the latest version.");
            }
        }

        /// <summary>
        /// Download compleet, run update.
        /// This only one setup file so the first element of the array with newfiles is launched.
        /// </summary>
        /// <param name="newfiles">Array with returned new files</param>
        private static void frmupdater_DownloadCompleetSuccesfull(string[] newfiles)
        {
            if (newfiles.Length < 1)
            {
                throw new Exception("No files downloaded");
            }

            RSAVerify rsaverify = new RSAVerify();
            if (!rsaverify.CheckFileSignatureAndDisplayErrors(newfiles[0], Program.updateprogramrsasignature))
            {
                return;
            }

            if (Settings.UpdatecheckUseGPG)
            {
                GPGVerifyWrapper gpgverif = new GPGVerifyWrapper();
                if (gpgverif.VerifyDownload(newfiles[0], newfiles[1]))
                {
                    ExecDownload(newfiles[0]);
                }
            }
            else
            {
                ExecDownload(newfiles[0]);
            }
        }

        /// <summary>
        /// Launch a new process, with parameters if set.
        /// </summary>
        /// <param name="file">The file to launch</param>
        private static void ExecDownload(string file)
        {
            ProcessStartInfo procstartinfo = new System.Diagnostics.ProcessStartInfo(file);
            procstartinfo.CreateNoWindow = false;
            procstartinfo.UseShellExecute = true;
            procstartinfo.ErrorDialog = true;
            procstartinfo.RedirectStandardInput = false;
            procstartinfo.RedirectStandardOutput = false;
            procstartinfo.RedirectStandardError = false;
            if (Settings.UpdateSilentInstall)
            {
                procstartinfo.Arguments = "/S";
            }

            bool shutdown = true;
            if (procstartinfo != null)
            {
                try
                {
                    System.Diagnostics.Process.Start(procstartinfo);
                }
                catch (InvalidOperationException invopexc)
                {
                    shutdown = false;
                    Log.Write(LogType.exception, invopexc.Message);
                }
                catch (System.ComponentModel.Win32Exception win32exc)
                {
                    shutdown = false;
                    Log.Write(LogType.exception, win32exc.Message); // also UAC canceled
                }
            }
            else
            {
                shutdown = false;
            }

            if (shutdown)
            {
                trayicon.Dispose();
                System.Windows.Forms.Application.Exit();
            }
        }

        /// <summary>
        /// Unhandled exceptions occur.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="args">UnhandledExceptionEvent arguments</param>
        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            ShowExceptionDlg(e);
        }

        /// <summary>
        /// Unhandled thread exceptions occur.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="treadargs">ThreadExceptionEvent arguments</param>
        private static void UnhanledThreadExceptionHandler(object sender, ThreadExceptionEventArgs treadargs)
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
        /// Get the operating system temp folder.
        /// </summary>
        /// <returns></returns>
        private static string GetTempFolder()
        {
            string tempfolder = System.Environment.GetEnvironmentVariable("TEMP");
            switch (currentos)
            {
                case OS.WINDOWS:
                    tempfolder = System.Environment.GetEnvironmentVariable("TEMP");
                    break;
                case OS.MACOS:
                    // /tmp is a symbolic link to /private/tmp on macos.
                    tempfolder = "/private/tmp";
                    break;
                case OS.LINUX:
                    tempfolder = "/tmp";
                    break;
            }

            return tempfolder;
        }

        // change working directory as dll search path
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool SetDllDirectory(string pathName);

        /// <summary>
        /// Get the plugin folder for plugins update to be stored in temperary.
        /// </summary>
        /// <param name="createnotexist">Create plugin update folder if not exist.</param>
        /// <returns>The path to the update plugin folder.</returns>
        public static string GetNewPluginFolder(bool createnotexist)
        {
            const string NEWPLUGINSFOLDERNAME = "new";
            string newpluginfolder = Path.Combine(Settings.ProgramPluginsFolder, NEWPLUGINSFOLDERNAME);
            if (!Directory.Exists(newpluginfolder) && createnotexist)
            {
                Directory.CreateDirectory(Path.Combine(Settings.ProgramPluginsFolder, NEWPLUGINSFOLDERNAME));
            }

            return newpluginfolder;
        }

        #endregion Methods
    }
}