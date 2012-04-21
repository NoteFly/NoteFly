//-----------------------------------------------------------------------
// <copyright file="FrmDownloader.cs" company="NoteFly">
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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.IO.Compression;
    using System.Net;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// FrmDownloader window, provides multifile download functions.
    /// </summary>
    public sealed partial class FrmDownloader : Form
    {
        /// <summary>
        /// GZip extension
        /// </summary>
        private const string GZIPEXTENSION = ".gz";

        /// <summary>
        /// Zip extension
        /// </summary>
        private const string ZIPEXTENSION = ".zip";

        /// <summary>
        /// Webclient object
        /// </summary>
        private WebClient webclient;

        /// <summary>
        /// number of downloads that FrmDownloader has compleeted.
        /// </summary>
        private int numdownloadscompleet = 0;

        /// <summary>
        /// The urls of all downloads to download.
        /// </summary>
        private string[] downloads;

        /// <summary>
        /// List of all downloaded full file locations.
        /// </summary>
        private List<string> files = new List<string>();

        /// <summary>
        /// The folder to save downloads to.
        /// </summary>
        private string storefolder;

        /// <summary>
        /// List of file extension to unzip.
        /// </summary>
        private string[] unzipextensions = new string[] { ".dll" };

        /// <summary>
        /// Initializes a new instance of the FrmDownloader class.
        /// </summary>
        /// <param name="title">Title of the window</param>
        public FrmDownloader(string title)
        {
            this.DoubleBuffered = Settings.ProgramFormsDoublebuffered;
            this.InitializeComponent();
            this.SetFormTitle(title);
            if (!this.CreateWebclient())
            {
                Log.Write(LogType.exception, "Cannot create webclient");
            }

            this.Refresh();
        }

        /// <summary>
        /// DownloadCompleet delegate
        /// </summary>
        /// <param name="newfiles">String array with all new files locations.</param>
        public delegate void DownloadCompleetHandler(string[] newfiles);

        /// <summary>
        /// Download is compleet event.
        /// </summary>
        [Description("Download is compleet.")]
        public event DownloadCompleetHandler AllDownloadsCompleted;

        /// <summary>
        /// Begin downloading a file.
        /// </summary>
        /// <param name="download">The url of the file to download.</param>
        /// <param name="storefolder">The folder to save the file to.</param>
        /// <returns>True if started succesfully.</returns>
        public bool BeginDownload(string download, string storefolder)
        {
            string[] downloads = new string[1];
            downloads[0] = download;
            return this.BeginDownload(downloads, storefolder);
        }

        /// <summary>
        /// Begin downloading files.
        /// </summary>
        /// <param name="downloads">List of url of files to download.</param>
        /// <param name="storefolder">The folder to save all the files to.</param>
        /// <returns>True if downloading succesfully started.</returns>
        public bool BeginDownload(string[] downloads, string storefolder)
        {            
            DirectoryInfo pluginsdirinfo = new DirectoryInfo(storefolder);
            if (pluginsdirinfo.Attributes == FileAttributes.System)
            {
                Log.Write(LogType.exception, "The plugin folder is not allowed to be a system directory.");               
            }
            else
            {
                this.storefolder = storefolder;
            }

            this.downloads = downloads;
            this.numdownloadscompleet = 0;
            string downloadurl = Program.ChangeUrlIPVersion(downloads[0]);
            Uri firstdownload = new Uri(downloadurl);
            return this.DownloadWebclient(firstdownload);
        }

        /// <summary>
        /// Create a new webclient.
        /// </summary>
        /// <returns>True if created succesfully.</returns>
        private bool CreateWebclient()
        {
            this.webclient = new WebClient();
            this.webclient.Encoding = Encoding.UTF8;
            this.webclient.UseDefaultCredentials = false;
            this.webclient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
            if (Settings.NetworkProxyEnabled && !string.IsNullOrEmpty(Settings.NetworkProxyAddress))
            {
                this.webclient.Proxy = new WebProxy(Settings.NetworkProxyAddress, Settings.NetworkProxyPort);
            }
            else
            {
                // set proxy to nothing, otherwise HttpWebRequest has connection issues, thanks you -> https://holyhoehle.wordpress.com/2010/01/12/webrequest-slow/ 
                this.webclient.Proxy = null;
            }

            this.webclient.Headers["User-Agent"] = Program.AssemblyTitle + " " + Program.AssemblyVersionAsString;
            if (Settings.NetworkUseGzip)
            {
                this.webclient.Headers["Accept-Encoding"] = "gzip"; // use gzip
            }

            this.webclient.DownloadFileCompleted += new AsyncCompletedEventHandler(this.DownloadCompleted);
            this.webclient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.DownloadProcessChanged);
            return true;
        }

        /// <summary>
        /// Let the webclient start asynchronized downloading of a file.
        /// </summary>
        /// <param name="uri">The uri of the file to download.</param>
        /// <returns>True if download started succesfully.</returns>
        private bool DownloadWebclient(Uri uri)
        {
            string newfile = this.GetStoreFilepath(this.storefolder, uri.ToString());
            this.files.Add(newfile);
            try
            {
                this.webclient.DownloadFileAsync(uri, newfile);
            }
            catch (WebException webexc)
            {
                Log.Write(LogType.exception, webexc.Message);
                return false;
            }
            catch (InvalidOperationException invopexc)
            {
                Log.Write(LogType.exception, invopexc.Message);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Set the title of this form.
        /// </summary>
        /// <param name="title">The new title prefix of this form</param>
        private void SetFormTitle(string title)
        {
            StringBuilder sbtitle = new StringBuilder(title);
            sbtitle.Append(" - ");
            sbtitle.Append(Program.AssemblyTitle);
            this.Text = sbtitle.ToString();
        }

        /// <summary>
        /// Update download process.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">ProgressChangedEventArgs arguments</param>
        private void DownloadProcessChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressbarDownload.Value = e.ProgressPercentage;
            this.lblStatusUpdate.Text = e.ProgressPercentage + " %";
        }

        /// <summary>
        /// Webclient finished downloading a file.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="e">The async. completed event arguments.</param>
        private void DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (!e.Cancelled)
                {
                    this.DecompressFileIfNeeded(this.files[this.numdownloadscompleet]);

                    this.numdownloadscompleet++;
                    if (this.numdownloadscompleet < this.downloads.Length)
                    {
                        string downloadurl = Program.ChangeUrlIPVersion(this.downloads[this.numdownloadscompleet]);
                        Uri download = new Uri(downloadurl);
                        this.CreateWebclient();
                        this.DownloadWebclient(download);
                    }
                    else
                    {
                        this.lblStatusUpdate.Text = Strings.T("Downloads completed");
                        if (this.AllDownloadsCompleted != null)
                        {
                            this.AllDownloadsCompleted(this.files.ToArray());
                        }

                        this.Close();
                    }
                }
                else
                {
                    this.lblStatusUpdate.Text = Strings.T("Download canceled.");
                    this.Close();
                }
            }
            else
            {
                StringBuilder sberror = new StringBuilder(Strings.T("Download error:"));
                sberror.Append(" ");
                sberror.Append(e.Error.Message);
                this.lblStatusUpdate.Text = sberror.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void DecompressFileIfNeeded(string file)
        {
            if (file.EndsWith(ZIPEXTENSION, StringComparison.OrdinalIgnoreCase))
            {
                if (this.DecompressZipFile(file, this.unzipextensions))
                {
                    Log.Write(LogType.info, "Decompressed zip archive: " + file);
                    if (File.GetAttributes(file) != FileAttributes.System)
                    {
                        try
                        {
                            File.Delete(file);
                            Log.Write(LogType.info, "Delete zip archive: " + file);
                        }
                        catch (ArgumentException argexc)
                        {
                            Log.Write(LogType.exception, argexc.Message);
                        }
                        catch (IOException ioexc)
                        {
                            Log.Write(LogType.exception, ioexc.Message);
                        }
                    }
                }
                else
                {
                    Log.Write(LogType.exception, "Decompressing zip file " + file + " failed.");
                }
            }
            else if (file.EndsWith(GZIPEXTENSION, StringComparison.OrdinalIgnoreCase))
            {
                string currentfilename = Path.GetFileName(file);
                string newfilename = currentfilename.Substring(0, currentfilename.Length - GZIPEXTENSION.Length);
                if (this.DecompressGZipFile(file, Path.Combine(this.storefolder, newfilename)))
                {
                    Log.Write(LogType.info, "Decompressed GZip file: " + file);
                    if (File.GetAttributes(file) != FileAttributes.System)
                    {
                        try
                        {
                            File.Delete(file);
                            Log.Write(LogType.info, "Delete GZip file: " + file);
                        }
                        catch (ArgumentException argexc)
                        {
                            Log.Write(LogType.exception, argexc.Message);
                        }
                        catch (IOException ioexc)
                        {
                            Log.Write(LogType.exception, ioexc.Message);
                        }
                    }
                }
                else
                {
                    Log.Write(LogType.exception, "Decompressing GZip file " + file + " failed.");
                }
            }
        }

        /// <summary>
        /// Figure out the new file location based on the url and the folder where to save the file in.
        /// </summary>
        /// <param name="storefolder">The folder where to save the file in.</param>
        /// <param name="url">The url of the file to download.</param>
        /// <returns>The store path</returns>
        private string GetStoreFilepath(string storefolder, string url)
        {
            string filename = Path.GetFileName(url);
            if (filename.Contains("="))
            {
                int posparm = filename.LastIndexOf('=') + 1;
                if (posparm < filename.Length)
                {
                    filename = filename.Substring(posparm, filename.Length - posparm);
                }
            }

            // is new filename properly extracted from url
            if (string.IsNullOrEmpty(filename) || filename.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
#if windows
                filename = "NoteFly_update.exe";
#elif linux
                filename = "NoteFly_update.deb";
#endif
            }

            // does storefolder exists
            if (!Directory.Exists(storefolder))
            {
                storefolder = System.Environment.GetEnvironmentVariable("TEMP");
            }

            return Path.Combine(storefolder, filename);
        }

        /// <summary>
        /// Decompress zip archive file.
        /// </summary>
        /// <param name="zipfilename">The zip file filename</param>
        /// <param name="extensions">The extension of file in the zipfile that are unzipped</param>
        private bool DecompressZipFile(string zipfile, string[] extensions)
        {
            bool succeeded = false;
            if (Directory.Exists(this.storefolder) && File.Exists(zipfile))
            {
                ZipStorer zip = null;
                try
                {
                    zip = ZipStorer.Open(zipfile, FileAccess.Read);
                    List<ZipStorer.ZipFileEntry> dir = zip.ReadCentralDir();
                    foreach (ZipStorer.ZipFileEntry entry in dir)
                    {
                        for (int i = 0; i < extensions.Length; i++)
                        {
                            if (entry.FilenameInZip.EndsWith(extensions[i]))
                            {
                                // disable plugin for as we are updating.
                                PluginsManager.DisablePlugin(entry.FilenameInZip);
                                // extract file
                                zip.ExtractFile(entry, Path.Combine(this.storefolder, entry.FilenameInZip));                                                                
                            }
                        }
                    }

                    succeeded = true;
                }
                catch (Exception)
                {
                    succeeded = false;
                }
                finally
                {
                    if (zip != null)
                    {
                        zip.Close();
                    }
                }                                
            }

            return succeeded;
        }

        /// <summary>
        /// Decompress GZip single file.
        /// </summary>
        private bool DecompressGZipFile(string compressedfile, string decompressedfile)
        {
            bool succeeded = false;
            if (File.Exists(compressedfile))
            {
                using (FileStream inputfilestream = File.Open(compressedfile, FileMode.Open), outputfilestream = File.Create(decompressedfile))
                {
                    byte[] outputbuffer;
                    using (GZipStream alg = new GZipStream(inputfilestream, CompressionMode.Decompress))
                    {
                        outputbuffer = new byte[inputfilestream.Length];
                        int counter;
                        while ((counter = alg.Read(outputbuffer, 0, outputbuffer.Length)) != 0)
                        {
                            outputfilestream.Write(outputbuffer, 0, counter);
                        }
                    }

                    outputbuffer = null;
                    succeeded = true;
                }
            }

            return succeeded;
        }
    }
}
