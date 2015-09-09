//-----------------------------------------------------------------------
// <copyright file="HttpUtil.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2012-2015  Tom
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

    /// <summary>
    /// Http utily class
    /// </summary>
    public class HttpUtil
    {
        /// <summary>
        /// Url of http request, immutable.
        /// </summary>
        private readonly string url;

        /// <summary>
        /// Cache settings of http request, immutable.
        /// </summary>
        private readonly System.Net.Cache.RequestCacheLevel cachesettings;

        private readonly string postdata;

        /// <summary>
        /// HTTP backgroundworker thread
        /// </summary>
        private BackgroundWorker httpthread;

        /// <summary>
        /// Initializes a new instance of the HttpUtil class.
        /// </summary>
        /// <param name="url">The url of the request to make</param>
        /// <param name="cachesettings">The cache settings (important note: this is always NoCacheNoStore under Mono)</param>
        /// <param name="postdata">POST data for a POST Http request.</param>
        /// <returns></returns>
        public HttpUtil(string url, System.Net.Cache.RequestCacheLevel cachesettings, string postdata)
        {
            String protocolhandler = "";
            if (!url.Contains("://"))
            {
                protocolhandler = "https:";
                if (!Settings.ProgramHttpsLinks)
                {
                    protocolhandler = "http:";
                }
            }

            url = protocolhandler + url;
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                this.url = url;
            }
            else
            {
                Log.Write(LogType.exception, "Invalid url.");
            }

            if (!String.IsNullOrEmpty(postdata))
            {
                this.postdata = postdata;
            }
            else
            {
                this.postdata = null;
            }
            
            this.cachesettings = cachesettings;
            this.httpthread = new BackgroundWorker();
            this.httpthread.DoWork += new DoWorkEventHandler(this.httpthread_DoWork);
        }

        /// <summary>
        /// Initializes a new instance of the HttpUtil class. Using no http POST.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cachesettings"></param>
        public HttpUtil(string url, System.Net.Cache.RequestCacheLevel cachesettings) : this(url, cachesettings, null)
        {
        }

        /// <summary>
        /// Create a new Http request only if DownloadCompleet event is assigned.
        /// </summary>
        /// <param name="workcompleethandler">The RunWorkerCompletedEventHandler</param>
        /// <returns>True if http background worker succesfully started</returns>
        public bool Start(RunWorkerCompletedEventHandler workcompleethandler)
        {
            if (!this.IsNetworkConnected())
            {
                return false;
            }
            else
            {
                this.httpthread.RunWorkerCompleted += workcompleethandler;
                this.httpthread.RunWorkerAsync();
                return true;
            }
        }

        /// <summary>
        /// Instantly stop the http worker, if still running.
        /// </summary>
        public void Stop()
        {
            if (this.httpthread != null)
            {
                this.httpthread.CancelAsync();
            }
        }

        // get network status
        [System.Runtime.InteropServices.DllImport("wininet.dll", EntryPoint = "InternetGetConnectedState")]
        private static extern bool InternetGetConnectedState(out int description, int ReservedValue);

        /// <summary>
        /// Http background worker thread reading stream
        /// and writing to memory in string.
        /// </summary>
        /// <param name="sender">The sender object</param>
        /// <param name="doworkevtarg">DoWorkEvent arguments</param>
        private void httpthread_DoWork(object sender, DoWorkEventArgs doworkevtarg)
        {
            HttpWebRequest request = this.CreateHttpWebRequest(this.url, this.cachesettings);
            if (request == null)
            {
                return;
            }

            WebResponse webresponse = null;
            string response = null;
            try
            {
                webresponse = (WebResponse)request.GetResponse();
                using (BufferedStream bufferedstream = new BufferedStream(webresponse.GetResponseStream()))
                {
                    using (StreamReader streamreader = new StreamReader(bufferedstream, System.Text.Encoding.UTF8))
                    {
                        response = streamreader.ReadToEnd();
                    }
                }

                doworkevtarg.Result = response;
            }
            catch (WebException webexc)
            {
                Log.Write(LogType.exception, webexc.Message + webexc.StackTrace);
            }
            finally
            {
                if (webresponse != null)
                {
                    webresponse.Close();
                }
            }
        }

        /// <summary>
        /// Create a WebRequest object
        /// </summary>
        /// <param name="url">Http request url.</param>
        /// <param name="cachesettings">Http cache settings</param>
        /// <returns>A new httpwebrequest object.</returns>
        private HttpWebRequest CreateHttpWebRequest(string url, System.Net.Cache.RequestCacheLevel cachesettings)
        {
            HttpWebRequest request = null;
            if (String.IsNullOrEmpty(url))
            {
                Log.Write(LogType.exception, "Url is null or empty.");
                return request;
            }

            System.Net.ServicePointManager.Expect100Continue = false;
            System.Net.ServicePointManager.EnableDnsRoundRobin = true;
            System.Net.ServicePointManager.DnsRefreshTimeout = 3 * 60 * 1000; // 180000 ms = 180 s = 3 minutes
            System.Net.ServicePointManager.DefaultConnectionLimit = 8;
            Log.Write(LogType.info, "Making request to '" + url + "'");
            try
            {
                request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                if (this.postdata == null)
                {
                    request.Method = "GET";
                }
                else
                {
                    request.Method = "POST";
                }
                
                request.ContentType = "text/xml";
                request.ProtocolVersion = HttpVersion.Version11; // HTTP 1.1 is required, required sending host header for notefly.org
                request.UserAgent = Program.AssemblyTitle + " " + Program.AssemblyVersionAsString;
                request.AllowAutoRedirect = false;
                request.Timeout = Settings.NetworkConnectionTimeout;
                request.KeepAlive = true;
                
                if (Settings.NetworkProxyEnabled && !string.IsNullOrEmpty(Settings.NetworkProxyAddress))
                {
                    request.Proxy = new WebProxy(Settings.NetworkProxyAddress, Settings.NetworkProxyPort);
                }
                else
                {
                    // set proxy to nothing, otherwise HttpWebRequest has issues, details: https://holyhoehle.wordpress.com/2010/01/12/webrequest-slow/ 
                    request.Proxy = null;
                    ////request.Proxy = GlobalProxySelection.GetEmptyWebProxy();
                }

                if (Settings.NetworkUseGzip)
                {
                    request.Headers["Accept-Encoding"] = "gzip";
                    request.AutomaticDecompression = DecompressionMethods.GZip;
                }

                request.CachePolicy = new System.Net.Cache.RequestCachePolicy(cachesettings);
                request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.None;
                request.PreAuthenticate = false;
                if (this.postdata != null)
                {
                    request.ContentType = "application/x-www-form-urlencoded";
                    byte[] dataenc = System.Text.Encoding.UTF8.GetBytes(this.postdata); // utf-8 on server.
                    request.ContentLength = dataenc.Length;
                    Stream stream = request.GetRequestStream();
                    stream.Write(dataenc, 0, dataenc.Length);
                }
            }
            catch (System.Net.WebException webexc)
            {
                Log.Write(LogType.exception, webexc.Message + webexc.StackTrace);
            }

            return request;
        }

        /// <summary>
        /// Check if there is internet connection, if not warn user.
        /// Uses windows API, other platforms return always true at the moment.
        /// </summary>
        /// <remarks>Decreated, used for send to twitter/facebook</remarks>
        /// <returns>true if there is a connection, otherwise return false</returns>
        private bool IsNetworkConnected()
        {
            if (Program.CurrentOS == Program.OS.WINDOWS)
            {
                int desc;
                if (InternetGetConnectedState(out desc, 0))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }
    }
}
