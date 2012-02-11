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
    using System.Text;

    /// <summary>
    /// FrmUpdater window.
    /// </summary>
    public sealed partial class FrmDownloader : Form
    {
        /// <summary>
        /// Download is compleet event.
        /// </summary>
        [Description("Download is compleet.")]
        public event DownloadCompleetHandler DownloadCompleetSuccesfull;

        /// <summary>
        /// 
        /// </summary>
        private WebClient webclient;

        /// <summary>
        /// 
        /// </summary>
        private Uri url;

        /// <summary>
        /// 
        /// </summary>
        private string storefilepath;

        /// <summary>
        /// Initializes a new instance of the FrmDownloader class.
        /// </summary>
        /// <param name="downloadurl">The url to the file to download.</param>
        /// <param name="verifsignature">Verif download with GPG</param>
        /// <param name="runandexit">Launch download and exit this programme</param>
        /// <param name="title">Title of the window</param>
        public FrmDownloader(string title, string url, string storefolder)
        {
            this.DoubleBuffered = Settings.ProgramFormsDoublebuffered;
            this.InitializeComponent();
            this.SetFormTitle(title);

            if (Settings.NetworkConnectionForceipv6)
            {
                // use dns ipv6 AAAA record to force the use of IPv6.
                url = url.Replace("://update.", "://ipv6."); // not replacing "http", "https", "ftp"
                url = url.Replace("://www.", "://ipv6.");
                url = url.Replace("://ipv4.", "://ipv6.");
            }

            this.url = new Uri(url);
            this.storefilepath = GetStoreFilepath(storefolder, url);
            this.webclient = new WebClient();
        }

        /// <summary>
        /// 
        /// </summary>
        public delegate void DownloadCompleetHandler(string storefilepath);

        /// <summary>
        /// 
        /// </summary>
        public bool StartDownload()
        {
            this.webclient.Encoding = Encoding.UTF8;
            this.webclient.UseDefaultCredentials = false;
            this.webclient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
            if (Settings.NetworkProxyEnabled)
            {
                if (!String.IsNullOrEmpty(Settings.NetworkProxyAddress) && Settings.NetworkProxyPort > 0 && Settings.NetworkProxyPort <= 65535)
                {
                    webclient.Proxy = new WebProxy(Settings.NetworkProxyAddress, Settings.NetworkProxyPort);
                }
            }

            this.webclient.Headers["User-Agent"] = Program.AssemblyTitle + " " + Program.AssemblyVersionAsString;
            if (Settings.NetworkUseGzip)
            {
                webclient.Headers["Accept-Encoding"] = "gzip"; // use gzip
            }

            this.webclient.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadCompleted);
            this.webclient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProcessChanged);
            try
            {
                this.webclient.DownloadFileAsync(this.url, this.storefilepath);
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
        /// <param name="title"></param>
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
        /// Download is compleet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (!e.Cancelled)
                {
                    this.lblStatusUpdate.Text = Strings.T("Download completed");
                    if (this.DownloadCompleetSuccesfull != null)
                    {
                        this.DownloadCompleetSuccesfull(this.storefilepath);
                    }

                    this.Close();
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
        /// <param name="downloadurl"></param>
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
    }
}
