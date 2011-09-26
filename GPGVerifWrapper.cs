//-----------------------------------------------------------------------
// <copyright file="TrayIcon.cs" company="GNU">
//  NoteFly a note application.
//  Copyright (C) 2011  Tom
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
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.IO;
    using System.Diagnostics;

    /// <summary>
    /// Class to verify files with GnuPG
    /// </summary>
    public class GPGVerifWrapper
    {
        private const string GPGSIGNATUREEXTENSION = ".sig";
        private const int gpgtimeout = 6000; // 6 seconds
        private Process gpgproc;
        private string gpgoutput;
        private string gpgerror;

        /// <summary>
        /// Verify a file.
        /// </summary>
        /// <param name="downloadfilepath">The path to the local file that was downloaded</param>
        public void VerifDownload(string downloadfilepath)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo procInfo = new System.Diagnostics.ProcessStartInfo(Settings.UpdatecheckGPGPath, " --verify-files " + downloadfilepath + GPGSIGNATUREEXTENSION);
                procInfo.CreateNoWindow = true;
                procInfo.UseShellExecute = false;
                procInfo.RedirectStandardInput = true;
                procInfo.RedirectStandardOutput = true;
                procInfo.RedirectStandardError = true;
                this.gpgproc = System.Diagnostics.Process.Start(procInfo);
                this.gpgproc.StandardInput.Flush();
                this.gpgproc.StandardInput.Close();

                this.gpgoutput = string.Empty;
                ThreadStart outputEntry = new ThreadStart(StandardOutputReader);
                Thread gpgoutputthread = new Thread(outputEntry);
                gpgoutputthread.Start();

                this.gpgerror = string.Empty;
                ThreadStart errorEntry = new ThreadStart(StandardErrorReader);
                Thread gpgerrorthread = new Thread(errorEntry);
                gpgerrorthread.Start();

                if (this.gpgproc.WaitForExit(gpgtimeout))
                {
                    // Process exited before timeout.
                    // Wait for the threads to complete reading output/error (but use a timeout)
                    if (!gpgoutputthread.Join(gpgtimeout / 2))
                    {
                        gpgoutputthread.Abort();
                    }
                    if (!gpgerrorthread.Join(gpgtimeout / 2))
                    {
                        gpgoutputthread.Abort();
                    }
                }
                else
                {
                    gpgproc.Kill();
                    if (gpgoutputthread.IsAlive)
                    {
                        gpgoutputthread.Abort();
                    }
                    if (gpgerrorthread.IsAlive)
                    {
                        gpgerrorthread.Abort();
                    }
                }

                int gpgprocexitcode = gpgproc.ExitCode;
                if (gpgprocexitcode == 0)
                {
                    // Currently display GPG result via messagebox..
                    System.Windows.Forms.MessageBox.Show(this.gpgoutput + System.Environment.NewLine + this.gpgerror, Program.AssemblyTitle + " signature check result");
                }

            }
            catch (Exception exc)
            {
                Log.Write(LogType.exception, "Verify with GPG failed: " + exc.Message);
            }
        }

        /// <summary>
        /// Get the NoteFly setup signature file location.
        /// </summary>
        /// <param name="file">The url of the file to verify.</param>
        /// <returns>The url of the signature file to verify file with.</returns>
        public string GetSignature(string file)
        {
            return file + GPGSIGNATUREEXTENSION;
        }

        /// <summary>
        /// Try to find the path to gpg.exe or gpg on linux
        /// </summary>
        public string GetGPGPath()
        {
            string gpgpath = string.Empty;
#if windows
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("HKEY_LOCAL_MACHINE\\SOFTWARE\\GNU\\GnuPG", false);
            if (key != null)
            {
                string gpginstallpath = (string)key.GetValue("Install Directory");
                gpgpath = this.FindGPGexecutables(gpginstallpath);
            }
            else
            {
                if (!String.IsNullOrEmpty(this.GetProgramFilesx86()))
                {
                    string gpginstallpath = Path.Combine(Path.Combine(this.GetProgramFilesx86(), "GNU"), "GnuPG");
                    if (Directory.Exists(gpginstallpath))
                    {
                        gpgpath = this.FindGPGexecutables(gpginstallpath);
                    }
                }
            }
#elif linux
            gpgpath = "gpg";
#endif

            return gpgpath;
        }

        /// <summary>
        /// Find the gpg executable in the installation directory.
        /// </summary>
        /// <param name="gpginstallpath">The installation directory</param>
        /// <returns>The full path to the gpg executable, empty string if gpg executable not found.</returns>
        private string FindGPGexecutables(string gpginstallpath)
        {
            string[] gpgfilenames = new string[] { "gpg.exe", "gpg2.exe" };
            for (int i = 0; i < gpgfilenames.Length; i++)
            {
                if (File.Exists(Path.Combine(gpginstallpath, gpgfilenames[i])))
                {
                    return Path.Combine(gpginstallpath, gpgfilenames[i]);
                }
            }

            return string.Empty;
        }

#if windows
        /// <summary>
        /// Get the path to program files, for 32bits applications.
        /// </summary>
        /// <returns></returns>
        private string GetProgramFilesx86()
        {
            if (8 == IntPtr.Size
                || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }

            return Environment.GetEnvironmentVariable("ProgramFiles");
        }
#endif

        /// <summary>
        /// Reader thread for standard output of gpg.exe
        /// </summary>
        private void StandardOutputReader()
        {
            string output = this.gpgproc.StandardOutput.ReadToEnd();
            lock (this)
            {
                this.gpgoutput = output;
            }
        }

        /// <summary>
        /// Reader thread for standard error
        /// </summary>
        private void StandardErrorReader()
        {
            string error = this.gpgproc.StandardError.ReadToEnd();
            lock (this)
            {
                this.gpgerror = error;
            }
        }

        /// <summary>
        /// Get the NoteFly OpenPGP Public Key from a key server
        /// </summary>
        private void GetNoteFlyPublicKey()
        {
            // fingerprint: 9968 3F36 7B60 4F21 ED55 A0CC 7898 7488 B43F 047E
            const string keyserver = ""; // find a good one
            System.Diagnostics.ProcessStartInfo procInfo = new System.Diagnostics.ProcessStartInfo(Settings.UpdatecheckGPGPath, " --recv-keys 2F9532C8 --keyserver "+keyserver); 
            procInfo.CreateNoWindow = true;
            procInfo.UseShellExecute = false;
            procInfo.RedirectStandardInput = true;
            procInfo.RedirectStandardOutput = true;
            procInfo.RedirectStandardError = true;
            this.gpgproc = System.Diagnostics.Process.Start(procInfo);
            if (this.gpgproc.WaitForExit(gpgtimeout))
            {
                gpgproc.Kill();
            }
        }

    }
}
