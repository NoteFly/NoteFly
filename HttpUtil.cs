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

    /// <summary>
    /// 
    /// </summary>
    public class HttpUtil
    {
        /// <summary>
        /// The http request
        /// </summary>
        private HttpWebRequest request = null;

        /// <summary>
        /// Create a new HTTP webrequest.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cachesettings"></param>
        /// <param name="usegzip"></param>
        /// <returns></returns>
        public HttpUtil(string url, System.Net.Cache.RequestCacheLevel cachesettings, bool usegzip)
        {            
            System.Net.ServicePointManager.Expect100Continue = false;
            System.Net.ServicePointManager.DefaultConnectionLimit = 2;
            if (Settings.NetworkConnectionForceipv6)
            {
                // use dns ipv6 AAAA record to force the use of IPv6
                url = url.Replace("//update.", "//ipv6."); // not replacing "http", "https", "ftp"
                url = url.Replace("//www.", "//ipv6.");
            }

            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                Log.Write(LogType.error, "Invalid url.");                
            }

            try
            {
                this.request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                this.request.Method = "GET";
                this.request.ContentType = "text/xml";
                this.request.UserAgent = Program.AssemblyTitle + " " + Program.AssemblyVersionAsString;
                this.request.Timeout = Settings.NetworkConnectionTimeout;
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
        }

        /// <summary>
        /// Get the response stream
        /// </summary>
        /// <returns></returns>
        public Stream GetResponseStream()
        {
            if (this.request != null)
            {
                WebResponse webresponse = this.request.GetResponse();
                return webresponse.GetResponseStream();
            }
            else
            {
                return Stream.Null;
            }
        }
    }
}
