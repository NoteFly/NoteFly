//-----------------------------------------------------------------------
// <copyright file="GPGVerifWrapper.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2011-2012  Tom
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
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// Class to verify files with GnuPG
    /// </summary>
    public sealed class GPGVerifWrapper
    {
        /// <summary>
        /// The GnuPG signature file extension
        /// </summary>
        private const string GPGSIGNATUREEXTENSION = ".sig";

        /// <summary>
        /// GnuPG process
        /// </summary>
        private Process gpgproc;

        /// <summary>
        /// output stream
        /// </summary>
        private string gpgoutput;

        /// <summary>
        /// error stream
        /// </summary>
        private string gpgerror;

        /// <summary>
        /// Verify a file.
        /// </summary>
        /// <param name="downloadfilepath">The path to the local file that was downloaded</param>
        /// <returns>True if user allows install.</returns>
        public bool VerifDownload(string downloadfilepath)
        {
            bool allowlaunch = false;

            // import NoteFly public key if needed.
            bool publickeyadded = this.IsGPGNoteFlyPublicKeyAdded();
            if (!publickeyadded)
            {
                Log.Write(LogType.error, "NoteFly OpenPGP public key missing. Trying to import now.");
                this.GetGPGNoteFlyPublicKey();
                if (!string.IsNullOrEmpty(this.gpgerror))
                {
                    if (this.gpgerror.StartsWith("gpg: no keyserver known"))
                    {
                        Log.Write(LogType.error, "Cannot import NoteFly OpenPGP public key no keyserver in UpdatecheckGPGKeyserver in settings.xml specified.");
                    }
                }
                else
                {
                    Log.Write(LogType.info, "NoteFly OpenPGP public key imported.");
                }
            }         

            try
            {
                System.Diagnostics.ProcessStartInfo procInfo = new System.Diagnostics.ProcessStartInfo(Settings.UpdatecheckGPGPath, " --verify-files " + downloadfilepath + GPGSIGNATUREEXTENSION);
                procInfo.CreateNoWindow = true;
                procInfo.UseShellExecute = false;
                procInfo.RedirectStandardInput = true;
                procInfo.RedirectStandardOutput = true;
                procInfo.RedirectStandardError = true;
                this.gpgproc = System.Diagnostics.Process.Start(procInfo);
                int gpgprocexitcode = this.StartGPGReadingThreads();
                if (gpgprocexitcode == 0)
                {
                    // Currently display GPG result via messagebox, and user required to press yes to launch install.
                    string gpgverifwrapper_installupdate = Gettext.Strings.T("Do you want to install the update?");
                    string gpgverifwrapper_gpgsigresult = Gettext.Strings.T(" GPG signature check result");
                    System.Windows.Forms.DialogResult dlgres = System.Windows.Forms.MessageBox.Show(this.gpgoutput + this.gpgerror + System.Environment.NewLine + gpgverifwrapper_installupdate, Program.AssemblyTitle + " GPG signature check result", System.Windows.Forms.MessageBoxButtons.YesNo);
                    if (dlgres == System.Windows.Forms.DialogResult.Yes)
                    {
                        allowlaunch = true;
                    }
                }
                else
                {
                    throw new ApplicationException("GnuPG did not return exitcode 0.");
                }
            }
            catch (Exception exc)
            {
                Log.Write(LogType.exception, "Verify with GPG failed: " + exc.Message);
            }

            return allowlaunch;
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
        /// Try to find the path to gpg.exe or gpg.
        /// </summary>
        /// <returns>The full path to GnuPG process</returns>
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
                if (!string.IsNullOrEmpty(this.GetProgramFilesx86()))
                {
                    string gpginstallpath = Path.Combine(Path.Combine(this.GetProgramFilesx86(), "GNU"), "GnuPG");
                    if (Directory.Exists(gpginstallpath))
                    {
                        gpgpath = this.FindGPGexecutables(gpginstallpath);
                    }
                }
            }
#elif linux
			gpgpath = FindGPGexecutables("usr/bin/");
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
        /// <returns>THe path to the Program files folder.</returns>
        private string GetProgramFilesx86()
        {
            if (8 == IntPtr.Size || (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
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
        /// Check if the NoteFly public key is added to the GPG keyring.
        /// </summary>
        /// <returns>True if notefly public key is added.</returns>
        private bool IsGPGNoteFlyPublicKeyAdded()
        {
            System.Diagnostics.ProcessStartInfo procInfo = new System.Diagnostics.ProcessStartInfo(Settings.UpdatecheckGPGPath, " --list-public-keys --with-colons");
            procInfo.CreateNoWindow = true;
            procInfo.UseShellExecute = false;
            procInfo.RedirectStandardInput = true;
            procInfo.RedirectStandardOutput = true;
            procInfo.RedirectStandardError = true;
            this.gpgproc = System.Diagnostics.Process.Start(procInfo);
            int gpgprocexitcode = this.StartGPGReadingThreads();
            if (gpgprocexitcode == 0 && string.IsNullOrEmpty(this.gpgerror))
            {
                int column = 0;
                int line = 0;
                int posstartlinerecord = int.MaxValue;
                bool ispubrecord = false;
                int posfingerprinthalfstart = int.MaxValue;
                ////bool fingerprintmatch = false;
                ////int posstartpubdesc = int.MaxValue;
                const string PUBKEYRECORD = "pub";
                const string FINGERPRINTPUBNOTEFLY = "78987488B43F047E";
                ////const string PUBKEYNOTEFLYDESCRIPTION = "NoteFly <releases@notefly.org>";
                // parser the output
                for (int i = 0; i < this.gpgoutput.Length; i++)
                {
                    if (this.gpgoutput[i] == '\r')
                    {
                        // new line
                        line++;
                        column = 0;
                        posstartlinerecord = i + 2; // +2 for \r and \n
                    }

                    if (this.gpgoutput[i] == ':')
                    {
                        // new column
                        column++;
                        if (column == 2 && line != 0)
                        {
                            int lenrecline = i - posstartlinerecord - 2;
                            if (lenrecline > 0)
                            {
                                string recordline = this.gpgoutput.Substring(posstartlinerecord, lenrecline);
                                if (recordline == PUBKEYRECORD) // note: ordinal string compare, not pointer
                                {
                                    ispubrecord = true;
                                }
                                else
                                {
                                    ispubrecord = false;
                                }
                            }
                        }

                        if (ispubrecord)
                        {
                            if (column == 4)
                            {
                                posfingerprinthalfstart = i + 1; // without colon
                            }
                            else if (column == 5)
                            {
                                int lenfingerprinthalf = i - posfingerprinthalfstart;
                                if (lenfingerprinthalf > 0)
                                {
                                    string fingerprinthalf = this.gpgoutput.Substring(posfingerprinthalfstart, lenfingerprinthalf);
                                    if (fingerprinthalf == FINGERPRINTPUBNOTEFLY)
                                    {
                                        ////fingerprintmatch = true;
                                        return true; // except it's added then, FIXME: collision possible, but small change
                                    }
                                    ////else
                                    ////{
                                    ////    fingerprintmatch = false;
                                    ////}
                                }
                            }

                            ////else if (column == 9)
                            ////{
                            ////    posstartpubdesc = i;
                            ////}
                            ////else if (column == 10 && fingerprintmatch)
                            ////{
                            ////    string pubkeydesc = gpgoutput.Substring(posstartpubdesc, i - posstartpubdesc -1 );
                            ////    if (pubkeydesc.Contains("PUBKEYNOTEFLYDESCRIPTION"))
                            ////    {
                            ////        return true;
                            ////    }
                            ////}
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Get the NoteFly OpenPGP Public Key from a key server
        /// </summary>
        private void GetGPGNoteFlyPublicKey()
        {
            StringBuilder gpgrecvkeycommandarg = new StringBuilder();
            gpgrecvkeycommandarg.Append(" --recv-keys 2F9532C8");
            if (!string.IsNullOrEmpty(Settings.UpdatecheckGPGKeyserver.Trim()))
            {
                gpgrecvkeycommandarg.Append(" --keyserver ");
                gpgrecvkeycommandarg.Append(Settings.UpdatecheckGPGKeyserver.Trim());
            }

            System.Diagnostics.ProcessStartInfo procInfo = new System.Diagnostics.ProcessStartInfo(Settings.UpdatecheckGPGPath, gpgrecvkeycommandarg.ToString());
            procInfo.CreateNoWindow = true;
            procInfo.UseShellExecute = false;
            procInfo.RedirectStandardInput = true;
            procInfo.RedirectStandardOutput = true;
            procInfo.RedirectStandardError = true;
            this.gpgproc = System.Diagnostics.Process.Start(procInfo);

            this.StartGPGReadingThreads();
        }

        /// <summary>
        /// Start reading the output normal and errors from created threads and
        ///  then copy the output to gpgoutput and gpgerror.
        /// </summary>
        /// <returns>GPG process exit code</returns>
        private int StartGPGReadingThreads()
        {
            if (Settings.UpdatecheckTimeoutGPG < 20)
            {
                Settings.UpdatecheckTimeoutGPG = 20; // set bare minimum, likely not enough for network with e.g: --recv-keys
            }

            this.gpgproc.StandardInput.Flush();
            this.gpgproc.StandardInput.Close();
            this.gpgoutput = string.Empty;
            ThreadStart outputEntry = new ThreadStart(this.StandardOutputReader);
            Thread gpgoutputthread = new Thread(outputEntry);
            gpgoutputthread.Start();

            this.gpgerror = string.Empty;
            ThreadStart errorEntry = new ThreadStart(this.StandardErrorReader);
            Thread gpgerrorthread = new Thread(errorEntry);
            gpgerrorthread.Start();

            if (this.gpgproc.WaitForExit(Settings.UpdatecheckTimeoutGPG))
            {
                // Process exited before timeout.
                // Wait for the threads to complete reading output/error (but use a timeout)
                if (!gpgoutputthread.Join(Settings.UpdatecheckTimeoutGPG / 2))
                {
                    gpgoutputthread.Abort();
                }

                if (!gpgerrorthread.Join(Settings.UpdatecheckTimeoutGPG / 2))
                {
                    gpgoutputthread.Abort();
                }
            }
            else
            {
                this.gpgproc.Kill();
                if (gpgoutputthread.IsAlive)
                {
                    gpgoutputthread.Abort();
                }

                if (gpgerrorthread.IsAlive)
                {
                    gpgerrorthread.Abort();
                }
            }

            return this.gpgproc.ExitCode;
        }
    }
}