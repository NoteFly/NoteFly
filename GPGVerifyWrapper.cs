//-----------------------------------------------------------------------
// <copyright file="GPGVerifWrapper.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2011-2014  Tom
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
    public sealed class GPGVerifyWrapper
    {
        /// <summary>
        /// The GnuPG clearsign signature file extension.
        /// </summary>
        public const string GPGSIGNATUREEXTENSION = ".asc";

        private const string OPENPGPNOTEFLYLONGKEYID = "C42F9FC093304722";
        private const string OPENPGPNOTEFLYFINGERPRINT = "A656130C5FAF88FA2B089080C42F9FC093304722";
        private const string OPENPGPNOTEFLYNAME = "NoteFly";
        private const string OPENPGPNOTEFLYEMAIL = "releases@notefly.org";
        //private const int OPENPGPNOTEFLYSTRENGTH = 4096;

        /// <summary>
        /// GnuPG process reference.
        /// </summary>
        private Process gpgproc;

        /// <summary>
        /// Output stream.
        /// </summary>
        private string gpgoutput;

        /// <summary>
        /// Error stream.
        /// </summary>
        private string gpgerror;

        /// <summary>
        /// Verify a file.
        /// </summary>
        /// <param name="file">The path to the local file that was downloaded</param>
        /// <param name="sigfile">The path to the signature file.</param>
        /// <returns>True if user allows install, signature valid.</returns>
        public bool VerifyDownload(string file, string sigfile)
        {
            bool allowlaunch = false;
            bool publickeyadded = false;
            System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo(Settings.UpdatecheckGPGPath, " --fingerprint --with-colons --list-keys " + OPENPGPNOTEFLYLONGKEYID);
            procStartInfo.CreateNoWindow = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.RedirectStandardInput = true;
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.RedirectStandardError = true;
            this.gpgproc = System.Diagnostics.Process.Start(procStartInfo);
            int gpgprocexitcode = this.StartGPGReadingThreads();
            if (gpgprocexitcode == 0 && string.IsNullOrEmpty(this.gpgerror))
            {
                int gpgresult = this.IsKeyInKeyring(OPENPGPNOTEFLYLONGKEYID, OPENPGPNOTEFLYFINGERPRINT, OPENPGPNOTEFLYNAME, OPENPGPNOTEFLYEMAIL);
                if (gpgresult > 0) 
                {
                    publickeyadded = true;
                }
            }

            if (!publickeyadded)
            {
                // import NoteFly public key if needed.
                Log.Write(LogType.error, "NoteFly OpenPGP public key missing. Trying to import now from keyserver with GPG.");
                this.DownloadPublicKey(OPENPGPNOTEFLYLONGKEYID);
                if (!string.IsNullOrEmpty(this.gpgerror))
                {
                    if (this.gpgerror.StartsWith("gpg: no keyserver known."))
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
                System.Diagnostics.ProcessStartInfo procInfo = new System.Diagnostics.ProcessStartInfo(Settings.UpdatecheckGPGPath, " --verify-files " + sigfile);
                procInfo.UseShellExecute = false;
                procInfo.RedirectStandardInput = true;
                procInfo.RedirectStandardOutput = true;
                procInfo.RedirectStandardError = true;
                this.gpgproc = System.Diagnostics.Process.Start(procInfo);
                int gpgprocexitcodeverifyfile = this.StartGPGReadingThreads();
                if (gpgprocexitcodeverifyfile != 0)
                {
                    throw new ApplicationException("GnuPG did not return exitcode 0.");
                }

                // Currently display GPG result via messagebox, and user required to press yes to launch install.
                StringBuilder sbmsg = new StringBuilder(this.gpgoutput);
                sbmsg.AppendLine(this.gpgerror);
                sbmsg.AppendLine(Strings.T("Do you want to install the update?"));
                string msgtitle = Strings.T("GnuPG signature check result");
                System.Windows.Forms.DialogResult dlgsigres = System.Windows.Forms.MessageBox.Show(sbmsg.ToString(), msgtitle, System.Windows.Forms.MessageBoxButtons.YesNo);
                if (dlgsigres == System.Windows.Forms.DialogResult.Yes)
                {
                    allowlaunch = true;
                }
            }
            catch (Exception exc)
            {
                Log.Write(LogType.exception, "Verify with GPG failed: " + exc.Message);
            }

            return allowlaunch;
        }

        /// <summary>
        /// Try to find the path to gpg.exe or gpg.
        /// </summary>
        /// <returns>The full path to GnuPG process</returns>
        public string GetGPGPath()
        {
            string gpgpath = string.Empty;
            if (Program.CurrentOS == Program.OS.WINDOWS)
            {
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
            }
            else if (Program.CurrentOS == Program.OS.LINUX)
            {
                gpgpath = FindGPGexecutables("/usr/bin/gpg");
            }

            return gpgpath;
        }

        /// <summary>
        /// Try to find the gpg binary in a dirctory.
        /// </summary>
        /// <param name="gpginstallpath">The installation directory</param>
        /// <returns>The full path to the gpg executable or a empty string if gpg binary is not found.</returns>
        private string FindGPGexecutables(string gpginstallpath)
        {
            string[] gpgfilenames = null;
            switch (Program.CurrentOS)
            {
                case Program.OS.LINUX:
                    gpgfilenames = new string[] { "gpg" };
                    break;
                case Program.OS.WINDOWS:
                    gpgfilenames = new string[] { "gpg.exe", "gpg2.exe" };
                    break;
                case Program.OS.MACOS:
                    gpgfilenames = new string[] { "gpg" };
                    break;
            }

            if (gpgfilenames != null)
            {
                for (int i = 0; i < gpgfilenames.Length; i++)
                {
                    if (File.Exists(Path.Combine(gpginstallpath, gpgfilenames[i])))
                    {
                        return Path.Combine(gpginstallpath, gpgfilenames[i]);
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Get the path to program files, for 32bits applications.
        /// </summary>
        /// <returns>The path to the Program files folder.</returns>
        private string GetProgramFilesx86()
        {
            if (IntPtr.Size == 8 || (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }

            return Environment.GetEnvironmentVariable("ProgramFiles");
        }

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
        /// Check the GNU Privay Guard keyring if it contains a OpenPGP key.
        /// </summary>
        /// <param name="keyid">The OpenPGP long key-id.</param>
        /// <param name="keyfingerprint">The key fingerprint.</param>
        /// <param name="keyname">The name of the key.</param>
        /// <param name="keyemail">The e-mail address of the key.</param>
        /// <returns>Returns 0 if key not in keyring, returns 1 if in keyring and no errors, returns 2 if key in keyring but untrusted (never or unknown trust set), return 3 if key in keyring but expired. return 4 if key in keyring but used before valid time.</returns>
        public int IsKeyInKeyring(string longkeyid, string keyfingerprint, string keyname, string keyemail)
        {
            StringReader reader = new StringReader(this.gpgoutput);
            char[] splitdelimiterchars = { ':' };
            bool foundpub = false;
            bool foundfpr = false;
            bool founduid = false;
            //int currentkeystrength = 0;
            string currentkeyid;
            int currentkeyvalidfrom = 0;
            int currentkeyvalidtill = 0;
            char currentkeyownertrust = 'q'; // f (full trust), m (marginal trust), n (never trust), q (unknown trust), - (unset)
            string currentkeyfingerprint;
            //int currentkeycreationtime;
            string currentkeynameandemail;
            string line;
            while (!String.IsNullOrEmpty(line = reader.ReadLine()))
            {
                if (line.Length < 4)
                {
                    continue;
                }

                string linestart = line.Substring(0, 4);
                string[] lineparts = line.Split(splitdelimiterchars, StringSplitOptions.None);
                switch (linestart)
                {
                    case "pub:":
                        //Int32.TryParse(lineparts[2], out currentkeystrength);
                        currentkeyid = lineparts[4];
                        if (!currentkeyid.Equals(longkeyid, StringComparison.Ordinal))
                        {
                            foundpub = true;
                        }

                        if (!Int32.TryParse(lineparts[5], out currentkeyvalidfrom))
                        {
                            Log.Write(LogType.error, "Cannot parse key validfrom.");
                        }

                        if (!Int32.TryParse(lineparts[6], out currentkeyvalidtill))
                        {
                            Log.Write(LogType.error, "Cannot parse key validtill.");
                        }

  

                        break;
                    case "fpr:":
                        currentkeyfingerprint = lineparts[9];
                        if (currentkeyfingerprint.Equals(keyfingerprint, StringComparison.Ordinal))
                        {
                            foundfpr = true;
                        }

                        break;
                    case "uid:":
                        //Int32.TryParse(lineparts[5], out currentkeycreationtime);
                        currentkeynameandemail = lineparts[9];
                        if (currentkeynameandemail.Equals(String.Format("{0} <{1}>", keyname, keyemail), StringComparison.Ordinal))
                        {
                            founduid = true;
                        }

                        break;
                }

                if (foundpub && foundfpr && founduid)
                {
                    reader.Close();
                    if (currentkeyownertrust == 'n' || currentkeyownertrust == 'q')
                    {
                        return 2;
                    }

                    int currentts = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    if (currentts > currentkeyvalidtill || currentts < currentkeyvalidfrom)
                    {
                        return 3;
                    }

                    return 1;
                }
            }

            reader.Close();
            return 0;
        }

        /// <summary>
        /// Get a OpenPGP Public Key from a key server.
        /// </summary>
        /// <param name="longkeyid">The OpenPGP long key-id.</param>
        private void DownloadPublicKey(string longkeyid)
        {
            StringBuilder gpgrecvkeycommandarg = new StringBuilder();
            gpgrecvkeycommandarg.Append(" --recv-keys " + longkeyid);
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