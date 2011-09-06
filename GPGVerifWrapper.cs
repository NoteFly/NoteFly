namespace NoteFly
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.IO;

    public class GPGVerifWrapper
    {
        private const string GPGSIGNATUREEXTENSION = ".sig";
        private System.Diagnostics.Process gpgproc;
        private string gpgoutput;
        private string gpgerror;
        private const int gpgtimeout = 6000; // 6 seconds

        /// <summary>
        /// Verif a file.
        /// </summary>
        /// <param name="downloadfilepath"></param>
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
                    // currently display GPG result via messagebox..
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
        /// <param name="downloadfile"></param>
        /// <returns></returns>
        public string GetSignature(string downloadfile)
        {
            return downloadfile + GPGSIGNATUREEXTENSION;
        }

        /// <summary>
        /// Try to find the path to gpg.exe or gpg on linux
        /// TODO make it work under windows x64 editions
        /// </summary>
        public string GetGPGPath()
        {
            string gpgpath = string.Empty;
#if windows
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("HKEY_LOCAL_MACHINE\\SOFTWARE\\GNU\\GnuPG", false);
            if (key != null)
            {
                string gpginstallpath = (string)key.GetValue("Install Directory");
                if (File.Exists(Path.Combine(gpginstallpath, "gpg.exe")))
                {
                    gpgpath = Path.Combine(gpginstallpath, "gpg.exe");
                }
                else if (File.Exists(Path.Combine(gpginstallpath, "gpg2.exe")))
                {
                    gpgpath = Path.Combine(gpginstallpath, "gpg2.exe");
                }
            }
#elif linux
            gpgpath = "gpg";
#endif

            return gpgpath;
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
        /// Get the NoteFly public GPG key from a key server
        /// </summary>
        private void GetNoteFlyPublicKey()
        {
            //"gpg --recv-keys 2F9532C8"
        }
    }
}
