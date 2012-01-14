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
    /// 
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
        /// Create a new HTTP webrequest.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cachesettings"></param>
        /// <param name="usegzip"></param>
        /// <returns></returns>
        public HttpUtil(string url, System.Net.Cache.RequestCacheLevel cachesettings, bool usegzip)
        {
            if (Settings.NetworkConnectionForceipv6)
            {
                // use dns ipv6 AAAA record to force the use of IPv6
                url = url.Replace("://update.", "://ipv6."); // not replacing "http", "https", "ftp"
                url = url.Replace("://www.", "://ipv6.");
            }

            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                this.url = url;
                this.cachesettings = cachesettings;
                this.usegzip = usegzip;
                this.httpthread = new Thread(HttpThread);
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
            httpthread.Join(Settings.NetworkConnectionTimeout);

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
        /// 
        /// </summary>
        public void ForceStopHttpThread()
        {
            if (this.httpthread != null)
            {
                this.httpthread.Abort();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void HttpThread()
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


    }
}