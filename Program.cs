﻿//-----------------------------------------------------------------------
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
    using System.Collections.Generic;
    using System.Text;
    using System.Reflection;
#if DEBUG
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
#endif

    /// <summary>
    /// Program class, main entry application.
    /// </summary>
    public class Program
    {
        #region Fields (2)

        private static Notes notes;
        private static TrayIcon trayicon;
        private const string DOWNLOADPAGE = "http://www.notefly.tk/downloads.php";

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
        /// The folder where NoteFly is installed.
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
        /// Get the version of this programme as a formatted string.
        /// </summary>
        public static string AssemblyVersionAsString
        {
            get
            {
                Int16[] version = GetVersion();
                return version[0] + "." + version[1] + "." + version[2];
            }
        }

        #endregion Properties

        #region Methods (1)

        // Public Methods (1) 

        #if windows
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        public static extern bool SetDllDirectory(string pathName);
        #endif

        /// <summary>
        /// Main entry point programme.
        /// load settings, parser parameters, create notes list and trayicon.
        /// </summary>
        /// <param name="args">parameters</param>
        [ STAThread ]
        public static void Main(string[] args)
        {
#if windows
            //a suggestion to "protect" against insecure Library Loading Vulnerabilities.
            //it does not fix it, it makes it harder to exploit if insecure dll loading exist.
            //NoteFly uses APPDATA and TEMP variables and systemroot is required for opening link.
            //This is OS specific 
            // /*
            SetDllDirectory(""); //removes notefly folder as ddl search path
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
            //override settings with supported parameters
            if (System.Environment.GetCommandLineArgs().Length > 1)
            {
                foreach (string arg in args)
                {
                    switch (arg)
                    {
                        //Forces the programme to setup the first run notefly info again.
                        case "/forcefirstrun":
                            Settings.ProgramFirstrun = true;
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
                        //turn off xp visual style.
                        case "/disablevisualstyles":
                            visualstyle = false; //about ~400ms slower on my system on display time notes.
                            break;
                        //rescue option for notes loading out of screen.
                        case "/resetpositions":
                            resetpositions = true;
                            break;
                       //overwrite settings file with default settings.
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

            if (Settings.UpdatecheckEverydays > 0)
            {
                DateTime lastupdate = DateTime.Parse(Settings.UpdatecheckLastDate);
                if (lastupdate.AddDays(Settings.UpdatecheckEverydays) <= DateTime.Now)
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
            Settings.UpdatecheckLastDate = DateTime.Now.ToString();
            xmlUtil.WriteSettings();
            Int16[] thisversion = GetVersion();
            Int16[] latestversion = xmlUtil.GetLatestVersion();
            bool updateavailible = false;
            for (int i = 0; i < thisversion.Length; i++)
            {
                if (thisversion[i] < latestversion[i] && latestversion[i] >=0)
                {
                    updateavailible = true;
                    break;
                }
            }

            if (updateavailible)
            {
                System.Windows.Forms.DialogResult updres = System.Windows.Forms.MessageBox.Show("There's a new version availible.\r\nDo you want to go to the download page now?", "update availible", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Asterisk);
                if (updres == System.Windows.Forms.DialogResult.Yes)
                {
                    Program.LoadURI(DOWNLOADPAGE);
                    //Thread.Sleep(10);
                    //System.Windows.Forms.Application.Exit();
                }
            }
        }

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
        /// <param name="url">the uniform resource location</param>
        public static void LoadLink(string uri_text)
        {
            if (Settings.ConfirmLinkclick)
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
        /// Actual loads the url.
        /// Also provide some cursor feedback.
        /// </summary>
        /// <param name="uri">The uri to load</param>
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
                //procstartinfo.UseShellExecute = true;
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.AppStarting;
                try
                {
                    System.Diagnostics.Process.Start(procstartinfo);
                }
                catch (System.ComponentModel.Win32Exception w32exc)
                {
                    Log.Write(LogType.exception, w32exc.Message);
                    //ErrorDialog is already showed.
                    return;
                }
            }
            finally
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

        #endregion Methods
    }
}
