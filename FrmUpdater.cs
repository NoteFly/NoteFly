namespace NoteFly
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Windows.Forms;
    using System.Net;
    using System.Threading;

    public partial class FrmUpdater : Form
    {
        private string downloadfilepath;
        private const string GPGSIGNATUREEXTENSION = ".sig";
        private System.Diagnostics.Process gpgproc;
        
        private string gpgoutput;
        private string gpgerror;
        private const int gpgtimeout = 5000; // 5 seconds

        /// <summary>
        /// Creating a new instance of FrmUpdater class.
        /// </summary>
        /// <param name="downloadurl"></param>
        public FrmUpdater(string downloadurl)
        {
            InitializeComponent();
            int locx = (Screen.PrimaryScreen.WorkingArea.Width / 2) - 180;
            int locy = 10;
            this.Location = new System.Drawing.Point(locx, locy);
            //this.downloadurl = downloadurl;
            backgroundWorkerDownloader.RunWorkerAsync(downloadurl);
        }

        /// <summary>
        /// Downloading update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string downloadurl = (string)e.Argument;
            string downloadfilename = Path.GetFileName(downloadurl);
            this.downloadfilepath = Path.Combine(System.Environment.GetEnvironmentVariable("TEMP"), downloadfilename);
            string sigfilename = Path.GetFileName(downloadurl + GPGSIGNATUREEXTENSION);
            string sigfilepath = Path.Combine(System.Environment.GetEnvironmentVariable("TEMP"), sigfilename);

            if (CheckValidPath(downloadfilepath))
            {
                // first, we need to get the exact size (in bytes) of the file we are downloading
                Uri url = new Uri(downloadurl);
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                request.Timeout = Settings.NetworkConnectionTimeout;
                request.UserAgent = Program.AssemblyTitle + " " + Program.AssemblyVersionAsString;
                request.Method = "GET";
                if (Settings.NetworkProxyEnabled && !string.IsNullOrEmpty(Settings.NetworkProxyAddress))
                {
                    request.Proxy = new WebProxy(Settings.NetworkProxyAddress);
                }

                request.AutomaticDecompression = System.Net.DecompressionMethods.GZip;
                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                response.Close();

                // gets the size of the file in bytes
                long filesize = response.ContentLength;
                if (filesize > 1073741824)
                {
                    Log.Write(LogType.exception, "To downloaded file too large, more than 1.0 Gb");
                    return;
                }

                // keeps track of the total bytes downloaded so we can update the progress bar
                long downloadedsize = 0;
                // use the webclient object to download the file
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    client.Headers["user-agent"] = Program.AssemblyTitle + " " + Program.AssemblyVersionAsString;

                    if (Settings.NetworkProxyEnabled && !string.IsNullOrEmpty(Settings.NetworkProxyAddress))
                    {
                        client.Proxy = new WebProxy(Settings.NetworkProxyAddress);
                    }

                    // open the file at the remote URL for reading
                    using (System.IO.Stream streamRemote = client.OpenRead(new Uri(downloadurl)))
                    {
                        // using the FileStream object, we can write the downloaded bytes to the file system
                        using (Stream streamLocal = new FileStream(downloadfilepath, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            // loop the stream and get the file into the byte buffer
                            int iByteSize = 0;
                            byte[] byteBuffer = new byte[filesize];
                            while ((iByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                            {
                                // write the bytes to the file system at the file path specified
                                streamLocal.Write(byteBuffer, 0, iByteSize);
                                downloadedsize += iByteSize;

                                // calculate the progress out of a base "100"
                                double dIndex = (double)(downloadedsize);
                                double dTotal = (double)byteBuffer.Length;
                                double dProgressPercentage = (dIndex / dTotal);
                                int iProgressPercentage;
                                if (Settings.UpdatecheckUseGPG)
                                {
                                    iProgressPercentage = (int)(dProgressPercentage * 80);
                                }
                                else
                                {
                                    iProgressPercentage = (int)(dProgressPercentage * 100);
                                }

                                // update the progress bar
                                backgroundWorkerDownloader.ReportProgress(iProgressPercentage);
                            }

                            // clean up the file stream
                            streamLocal.Close();
                        }

                        // close the connection to the remote server
                        streamRemote.Close();
                    }

                    if (Settings.UpdatecheckUseGPG)
                    {
                        System.Threading.Thread.Sleep(50);

                        // first, we need to get the exact size (in bytes) of the file we are downloading
                        Uri urlsig = new Uri(downloadurl + GPGSIGNATUREEXTENSION);
                        System.Net.HttpWebRequest requestsig = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(urlsig);
                        requestsig.Timeout = Settings.NetworkConnectionTimeout;
                        requestsig.UserAgent = Program.AssemblyTitle + " " + Program.AssemblyVersionAsString;
                        requestsig.Method = "GET";
                        requestsig.AutomaticDecompression = System.Net.DecompressionMethods.GZip;
                        if (Settings.NetworkProxyEnabled && !string.IsNullOrEmpty(Settings.NetworkProxyAddress))
                        {
                            request.Proxy = new WebProxy(Settings.NetworkProxyAddress);
                        }

                        System.Net.HttpWebResponse responsesig = (System.Net.HttpWebResponse)requestsig.GetResponse();
                        responsesig.Close();
                        // gets the size of the signature file, should be 72 bytes
                        long filesizesig = responsesig.ContentLength;
                        if (filesizesig > 1024)
                        {
                            Log.Write(LogType.exception, "To signature file too large, more than 1 kb");
                            return;
                        }

                        // open new stream for downloading of signature file
                        try
                        {
                            client.Headers["user-agent"] = Program.AssemblyTitle + " " + Program.AssemblyVersionAsString;
                            using (System.IO.Stream streamsigdownload = client.OpenRead(urlsig))
                            {
                                // using the FileStream object, we can write the downloaded bytes to the file system
                                using (Stream streamLocal = new FileStream(sigfilepath, FileMode.Create, FileAccess.Write, FileShare.None))
                                {
                                    // loop the stream and get the file into the byte buffer
                                    int iByteSize = 0;
                                    byte[] byteBuffer = new byte[filesizesig];
                                    while ((iByteSize = streamsigdownload.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                                    {
                                        // write the bytes to the file system at the file path specified
                                        streamLocal.Write(byteBuffer, 0, iByteSize);
                                        downloadedsize += iByteSize;
                                    }
                                    streamLocal.Close();
                                }
                                streamsigdownload.Close();
                            }
                        }
                        catch (WebException webexc)
                        {
                            Log.Write(LogType.exception, webexc.Message);
                        }
                        backgroundWorkerDownloader.ReportProgress(100);
                    }
                }
            }
            else
            {
                const string INVLAIDFILENAMEORPATH = "Invalid filepath/filename downloaded file";
                Log.Write(LogType.exception, INVLAIDFILENAMEORPATH);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// update download process.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressbarDownload.Value = e.ProgressPercentage;
            this.lblStatusUpdate.Text = e.ProgressPercentage + " %";
        }

        /// <summary>
        /// run download if download is compleet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                if (Settings.UpdatecheckUseGPG)
                {
                    this.lblStatusUpdate.Text = "download compleet, verif download";
                    this.lblStatusUpdate.Refresh();
                    if (File.Exists(Settings.UpdatecheckGPGPath))
                    {
                        Log.Write(LogType.info, "verif download. This is currently a unstable feature.");
                        try
                        {
                            System.Diagnostics.ProcessStartInfo procInfo = new System.Diagnostics.ProcessStartInfo(Settings.UpdatecheckGPGPath, " --verify-files " + downloadfilepath + GPGSIGNATUREEXTENSION + "");
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
                                // Process exited before timeout...
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
                                MessageBox.Show(this.gpgoutput + System.Environment.NewLine + this.gpgerror, Program.AssemblyTitle+" signature check result");
                            }                            

                        }
                        catch (Exception exc)
                        {
                            Log.Write(LogType.exception, "GPG failed: " + exc.Message);
                        }
                    }
                    else
                    {
                        Log.Write(LogType.exception, "Verif download failed, cannot find gpg.exe: " + Settings.UpdatecheckGPGPath);
                        return;
                    }
                }

                this.lblStatusUpdate.Text = "download compleet, installing.. ";
                this.lblStatusUpdate.Refresh();
                System.Threading.Thread.Sleep(50);

                System.Diagnostics.ProcessStartInfo procstartinfo = new System.Diagnostics.ProcessStartInfo(downloadfilepath);
                procstartinfo.ErrorDialog = true;
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.AppStarting;
                try
                {
                    System.Diagnostics.Process.Start(procstartinfo);
                }
                catch (System.ComponentModel.Win32Exception w32exc)
                {
                    Log.Write(LogType.exception, w32exc.Message);
                    this.Close();
                }

                Application.Exit();
            }
            else
            {
                this.lblStatusUpdate.Text = "aborted.";
            }
        }

        /// <summary>
        /// Reader thread for standard output of gpg.exe
        /// </summary>
        public void StandardOutputReader()
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
        public void StandardErrorReader()
        {
            string error = this.gpgproc.StandardError.ReadToEnd();
            lock (this)
            {
                this.gpgerror = error;
            }
        }

        /// <summary>
        /// Check if the path does not contain forbidden filename/filepath characters
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool CheckValidPath(string path)
        {
            char[] forbiddencharspath = "?<>*|\"".ToCharArray();
            if (downloadfilepath.IndexOfAny(forbiddencharspath) < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
