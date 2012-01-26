//-----------------------------------------------------------------------
// <copyright file="FrmUpdater.cs" company="NoteFly">
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
    using System.ComponentModel;
    using System.IO;
    using System.Net;
    using System.Windows.Forms;

    /// <summary>
    /// FrmUpdater window.
    /// </summary>
    public sealed partial class FrmDownloader : Form
    {
        /// <summary>
        /// The download filepath
        /// </summary>
        private string downloadfilepath;

        /// <summary>
        /// Launch downloaded file after download.
        /// </summary>
        private bool runandexit = false;

        /// <summary>
        /// The GPGVerifWrapper class
        /// </summary>
        private GPGVerifWrapper gpgverif;

        /// <summary>
        /// Initializes a new instance of the FrmDownloader class.
        /// </summary>
        /// <param name="downloadurl">The url to the file to download.</param>
        /// <param name="verifsignature">Verif download with GPG</param>
        /// <param name="runandexit">Launch download and exit this programme</param>
        /// <param name="title">Title of the window</param>
        public FrmDownloader(string downloadurl, bool verifsignature, bool runandexit, string title)
        {
            this.DoubleBuffered = Settings.ProgramFormsDoublebuffered;
            this.InitializeComponent();
            this.Text = title;
            this.runandexit = runandexit;
            if (verifsignature)
            {
                this.gpgverif = new GPGVerifWrapper();
            }

            this.backgroundWorkerDownloader.RunWorkerAsync(downloadurl);
        }

        /// <summary>
        /// Downloading file
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">DoWorkEventArgs arguments</param>
        private void backgroundWorkerDownloader_DoWork(object sender, DoWorkEventArgs e)
        {
            string useragent = Program.AssemblyTitle + " " + Program.AssemblyVersionAsString;
            string downloadurl = (string)e.Argument;
            string downloadfilename = Path.GetFileName(downloadurl);
            if (downloadfilename.Contains("="))
            {
                int posparm = downloadfilename.LastIndexOf('=') + 1;
                if (posparm < downloadfilename.Length)
                {
                    downloadfilename = downloadfilename.Substring(posparm, downloadfilename.Length - posparm);
                }
            }

            try
            {
                this.downloadfilepath = Path.Combine(System.Environment.GetEnvironmentVariable("TEMP"), downloadfilename);
            }
            catch (NullReferenceException nullexc)
            {
                Log.Write(LogType.exception, nullexc.Message);
#if windows
                const string UNKNOWFILENAME = "NoteFly_update.exe";
                const string UNKNOWTEMPFOLDER = "C:\\temp";
#elif linux
                const string UNKNOWFILENAME = "NoteFly_update.deb";
                const string UNKNOWTEMPFOLDER = "/tmp";
#endif
                if ((string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("TEMP"))) && (string.IsNullOrEmpty(downloadfilename)))
                {
                    Directory.CreateDirectory(UNKNOWTEMPFOLDER);
                    this.downloadfilepath = Path.Combine(UNKNOWTEMPFOLDER, UNKNOWFILENAME);
                }
                else if (string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("TEMP")))
                {
                    Directory.CreateDirectory(UNKNOWTEMPFOLDER);
                    this.downloadfilepath = Path.Combine(UNKNOWTEMPFOLDER, downloadfilename);
                }
                else if (string.IsNullOrEmpty(downloadfilename))
                {
                    this.downloadfilepath = Path.Combine(System.Environment.GetEnvironmentVariable("TEMP"), UNKNOWFILENAME);
                }
            }

            if (this.CheckValidPath())
            {
                // first, we need to get the exact size (in bytes) of the file we are downloading
                Uri url = new Uri(downloadurl);
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                request.Timeout = Settings.NetworkConnectionTimeout;
                request.UserAgent = Program.AssemblyTitle + " " + Program.AssemblyVersionAsString;
                request.Method = "GET";
                request.AutomaticDecompression = System.Net.DecompressionMethods.GZip;
                if (Settings.NetworkProxyEnabled && !string.IsNullOrEmpty(Settings.NetworkProxyAddress))
                {
                    request.Proxy = new WebProxy(Settings.NetworkProxyAddress);
                }

                System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                response.Close();

                // gets the size of the file in bytes
                long filesize = response.ContentLength;
                const long MAXUPDATEFILESIZE = 1073741824;
                if (filesize > MAXUPDATEFILESIZE)
                {
                    Log.Write(LogType.exception, "To downloaded file too large, more than " + MAXUPDATEFILESIZE + " bytes.");
                    return;
                }

                // keeps track of the total bytes downloaded so we can update the progress bar
                long downloadedsize = 0;
                // use the webclient object to download the file
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    if (!this.CheckFileExist(this.downloadfilepath, filesize))
                    {
                        client.Headers["user-agent"] = useragent;

                        if (Settings.NetworkProxyEnabled && !string.IsNullOrEmpty(Settings.NetworkProxyAddress))
                        {
                            client.Proxy = new WebProxy(Settings.NetworkProxyAddress);
                        }

                        // open the file at the remote URL for reading
                        using (System.IO.Stream streamRemote = client.OpenRead(new Uri(downloadurl)))
                        {
                            // using the FileStream object, we can write the downloaded bytes to the file system
                            using (Stream streamLocal = new FileStream(this.downloadfilepath, FileMode.Create, FileAccess.Write, FileShare.None))
                            {
                                this.PreallocateFile(streamLocal, filesize);

                                // loop the stream and get the file into the byte buffer
                                int iByteSize = 0;
                                if (filesize > 0)
                                {
                                    byte[] byteBuffer = new byte[filesize];
                                    while ((iByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                                    {
                                        // write the bytes to the file system at the file path specified
                                        streamLocal.Write(byteBuffer, 0, iByteSize);
                                        downloadedsize += iByteSize;

                                        // calculate the progress out of a base "100"
                                        double dIndex = (double)downloadedsize;
                                        double dTotal = (double)byteBuffer.Length;
                                        double dProgressPercentage = dIndex / dTotal;
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
                                        this.backgroundWorkerDownloader.ReportProgress(iProgressPercentage);
                                    }
                                }

                                // clean up the file stream
                                streamLocal.Close();
                            } 

                            // close the connection to the remote server
                            streamRemote.Close();
                        }
                    }

                    if (this.gpgverif != null)
                    {
                        string sigfilepath = this.gpgverif.GetSignature(this.downloadfilepath);

                        // first, we need to get the exact size (in bytes) of the file we are downloading
                        Uri urlsigfileserver = new Uri(this.gpgverif.GetSignature(downloadurl));
                        System.Net.HttpWebRequest requestsig = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(urlsigfileserver);
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
                        const long MAXSIGFILESIZE = 4096;
                        if (filesizesig > MAXSIGFILESIZE)
                        {
                            Log.Write(LogType.exception, "To signature file too large, more than " + MAXSIGFILESIZE + " bytes, excepted 72bytes");
                            return;
                        }

                        // open new stream for downloading of signature file
                        try
                        {
                            client.Headers["user-agent"] = useragent;
                            using (System.IO.Stream streamsigdownload = client.OpenRead(urlsigfileserver))
                            {
                                // using the FileStream object, we can write the downloaded bytes to the file system
                                using (Stream streamLocal = new FileStream(sigfilepath, FileMode.Create, FileAccess.Write, FileShare.None))
                                {
                                    this.PreallocateFile(streamLocal, filesizesig);

                                    // loop the stream and get the file into the byte buffer
                                    int iByteSize = 0;
                                    if (filesizesig > 0)
                                    {
                                        byte[] byteBuffer = new byte[filesizesig];
                                        while ((iByteSize = streamsigdownload.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                                        {
                                            // write the bytes to the file system at the file path specified
                                            streamLocal.Write(byteBuffer, 0, iByteSize);
                                            downloadedsize += iByteSize;
                                        }
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
                    }

                    this.backgroundWorkerDownloader.ReportProgress(100);
                }
            }
            else
            {
                Log.Write(LogType.exception, "Invalid filepath/filename downloaded file");
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Check if update setup already exist
        /// </summary>
        /// <param name="file">The full path to the update file</param>
        /// <param name="filesize">The filesize of the update file</param>
        /// <returns>True if the file already exist</returns>
        private bool CheckFileExist(string file, long filesize)
        {
            bool fileexist = false;
            if (File.Exists(file))
            {
                FileInfo fi = new FileInfo(file);
                if (fi.Length == filesize)
                {
                    fileexist = true;
                }
            }

            return fileexist;
        }

        /// <summary>
        /// update download process.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">ProgressChangedEventArgs arguments</param>
        private void backgroundWorkerDownloader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressbarDownload.Value = e.ProgressPercentage;
            this.lblStatusUpdate.Text = e.ProgressPercentage + " %";
        }

        /// <summary>
        /// run download if download is compleet.
        /// </summary>
        /// <param name="sender">Sender objects</param>
        /// <param name="e">RunWorkerCompletedEventArgs arguments</param>
        private void backgroundWorkerDownloader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string downloader_installaborted = Strings.T("Aborted");
            if (!e.Cancelled)
            {
                string downloader_downloadcompleet = Strings.T("download compleet, ");
                if (Settings.UpdatecheckUseGPG && this.gpgverif != null)
                {
                    string downloader_verifdownload = Strings.T("verify download");
                    this.lblStatusUpdate.Text = downloader_downloadcompleet + downloader_verifdownload;
                    Log.Write(LogType.info, downloader_verifdownload);

                    this.lblStatusUpdate.Refresh();
                    if (File.Exists(Settings.UpdatecheckGPGPath))
                    {
                        if (!this.gpgverif.VerifDownload(this.downloadfilepath))
                        {
                            this.lblStatusUpdate.Text = downloader_installaborted;
                            return;
                        }
                    }
                    else
                    {
                        string downloader_cannotfindgpg = Strings.T("Verify download failed, cannot find gpg: ");
                        Log.Write(LogType.exception, downloader_cannotfindgpg + Settings.UpdatecheckGPGPath);
                        return;
                    }
                }

                this.lblStatusUpdate.Text = downloader_downloadcompleet;
                this.lblStatusUpdate.Refresh();

                if (this.runandexit)
                {
                    string downloader_installing = Strings.T("installing.. ");
                    this.lblStatusUpdate.Text = downloader_downloadcompleet + downloader_installing;
                    this.lblStatusUpdate.Refresh();
                    System.Threading.Thread.Sleep(100);

                    System.Diagnostics.ProcessStartInfo procstartinfo;
                    if (Settings.UpdateSilentInstall)
                    {
                        procstartinfo = new System.Diagnostics.ProcessStartInfo(this.downloadfilepath, "/S");
                    }
                    else
                    {
                        procstartinfo = new System.Diagnostics.ProcessStartInfo(this.downloadfilepath);
                    }

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
                    System.Threading.Thread.Sleep(400);
                    this.Close();
                }
            }
            else
            {
                this.lblStatusUpdate.Text = downloader_installaborted;
            }
        }

        /// <summary>
        /// Check if the path does not contain forbidden filename/filepath characters
        /// </summary>
        /// <returns>true if the path is valid, false if it contains a illegal path character.</returns>
        private bool CheckValidPath()
        {
            char[] forbiddencharspath = Path.GetInvalidPathChars(); // "?<>*|\"".ToCharArray();
            if (this.downloadfilepath.IndexOfAny(forbiddencharspath) < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Preallocate file
        /// </summary>
        /// <param name="filestream">A filestream</param>
        /// <param name="filesize">The filesize of the file to preallocate</param>
        private void PreallocateFile(Stream filestream, long filesize)
        {
            try
            {
                const long MAXFILESIZE = 10737418240; // 10 megabytes
                if (filesize > 0 && filesize <= MAXFILESIZE)
                {
                    filestream.SetLength(filesize);
                }
                else
                {
                    Log.Write(LogType.exception, "Did not preallocate file, because filesize out of range.");
                }
            }
            catch (IOException ioexc)
            {
                Log.Write(LogType.exception, ioexc.Message);
                MessageBox.Show(ioexc.Message);
            }
        }
    }
}
