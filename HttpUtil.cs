//-----------------------------------------------------------------------
// <copyright file="HttpUtil.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2012  Tom
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
    using System.Net;
    using System.IO;
    using System.Threading;

    /// <summary>
    /// Http
    /// </summary>
    public class HttpUtil
    {
        /// <summary>
        /// thread
        /// </summary>
        private Thread httpthread;

        /// <summary>
        /// The response
        /// </summary>
        private string response; 

        /// <summary>
        /// immutable
        /// </summary>
        private readonly string url;

        /// <summary>
        /// immutable
        /// </summary>
        private readonly System.Net.Cache.RequestCacheLevel cachesettings;

        /// <summary>
        /// immutable
        /// </summary>
        private readonly bool usegzip;

        /// <summary>
        /// Initializes a new instance of the HttpUtil class.
        /// Create a new Http request.
        /// </summary>
        /// <param name="url">The url of the request to make</param>
        /// <param name="cachesettings">The cache settings (important note: this is always NoCacheNoStore under Mono)</param>
        /// <param name="usegzip">Use gzip compression</param>
        /// <returns></returns>
        public HttpUtil(string url, System.Net.Cache.RequestCacheLevel cachesettings, bool usegzip)
        {
            if (Settings.NetworkConnectionForceipv6)
            {
                // use dns ipv6 AAAA record to force the use of IPv6.
                url = url.Replace("://update.", "://ipv6."); // not replacing "http", "https", "ftp"
                url = url.Replace("://www.", "://ipv6.");
            }

            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                this.url = url;
                this.cachesettings = cachesettings;
                this.usegzip = usegzip;
                this.httpthread = new Thread(this.StartHttpThread);
                this.httpthread.Start();
            }
            else
            {
                Log.Write(LogType.exception, "Invalid url.");
            }

        }

        /// <summary>
        /// Get the response stream
        /// </summary>
        /// <returns>Empty string if error getting response</returns>
        public string GetResponse()
        {
            this.httpthread.Join(Settings.NetworkConnectionTimeout);

            if (!String.IsNullOrEmpty(this.response))
            {
                return this.response;
            }
            else
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Instantly abort the http thread, if still running.
        /// </summary>
        public void StopHttpThread()
        {
            if (this.httpthread != null)
            {
                this.httpthread.Abort();
            }
        }

        /// <summary>
        /// Start http worker thread to make request and wait for response.
        /// </summary>
        private void StartHttpThread()
        {
            HttpWebRequest request = this.CreateHttpWebRequest(this.url, this.cachesettings, this.usegzip);
            if (request != null)
            {
                WebResponse response = null;
                try
                {
                    response = request.GetResponse();
                    using (Stream responsestream = response.GetResponseStream())
                    {
                        using (StreamReader streamreader = new StreamReader(responsestream))
                        {
                            this.response = streamreader.ReadToEnd();
                        }
                    }
                }
                catch (WebException webexc)
                {
                    Log.Write(LogType.exception, webexc.Message);
                }
                finally
                {
                    if (response != null)
                    {
                        response.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Create a WebRequest object
        /// </summary>
        private HttpWebRequest CreateHttpWebRequest(string url, System.Net.Cache.RequestCacheLevel cachesettings, bool usegzip)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            System.Net.ServicePointManager.DefaultConnectionLimit = 4;
            HttpWebRequest request = null;
            try
            {
                request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "text/xml";
                request.UserAgent = Program.AssemblyTitle + " " + Program.AssemblyVersionAsString;
                request.Timeout = Settings.NetworkConnectionTimeout;
                if (Settings.NetworkProxyEnabled && !string.IsNullOrEmpty(Settings.NetworkProxyAddress))
                {
                    request.Proxy = new WebProxy(Settings.NetworkProxyAddress);
                }

                if (usegzip)
                {
                    request.Headers["Accept-Encoding"] = "gzip";
                }

                request.CachePolicy = new System.Net.Cache.RequestCachePolicy(cachesettings);
                request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.None;
            }
            catch (System.Net.WebException webexc)
            {
                Log.Write(LogType.exception, webexc.Message);
            }

            return request;
        }

        /// <summary>
        /// Check if there is internet connection, if not warn user.
        /// Uses windows API, other platforms return always true at the moment.
        /// </summary>
        /// <remarks>Decreated, used for send to twitter/facebook</remarks>
        /// <returns>true if there is a connection, otherwise return false</returns>
        private static bool IsNetworkConnected()
        {
#if windows
            int desc;
            if (InternetGetConnectedState(out desc, 0))
            {
                return true;
            }
            else
            {
                return false;
            }
#elif !windows
            return true;
#endif
        }

#if windows
        // get network status
        [System.Runtime.InteropServices.DllImport("wininet.dll", EntryPoint = "InternetGetConnectedState")]
        private static extern bool InternetGetConnectedState(out int description, int ReservedValue);
#endif

    }
}