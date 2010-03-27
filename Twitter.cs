//-----------------------------------------------------------------------
// <copyright file="Twitter.cs" company="GNU">
// 
// This program is free software; you can redistribute it and/or modify it
// Free Software Foundation; either version 2, 
// or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// </copyright>
//-----------------------------------------------------------------------

namespace NoteFly
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Windows.Forms;
    using System.Xml;

    /// <summary>
    /// sends a note to twitter
    /// </summary>
    public class Twitter
    {
        #region Fields (2)

        /// <summary>
        /// The url of twitter.
        /// </summary>
        protected const string TwitterBaseUrlFormat = "http://168.143.162.68/{0}/{1}.{2}";

        /// <summary>
        /// Gets the URL of the Twitter client.
        /// Must be in the XML format documented in the "Request Headers" section at:
        /// http://twitter.pbwiki.com/API-Docs.
        /// According to the Twitter Fan Wiki at http://twitter.pbwiki.com/API-Docs and supported by
        /// the Twitter developers, this will be used in the future (hopefully near) to set more information
        /// in Twitter about the client posting the information as well as future usage in a clients directory.
        /// </summary>
        private string twitterClientUrl = "http://www.notefly.tk/";

        ////private string source = null;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the Twitter class.
        /// </summary>
        public Twitter()
        {
        }
        #endregion Constructors

        #region Methods (3)

        // Public Methods (2) 

        /// <summary>
        /// Send a tweet to Twitter.
        /// </summary>
        /// <param name="userName">The Twitter username</param>
        /// <param name="password">The Twitter password</param>
        /// <param name="status">The tweet, limited to 140 chars.</param>
        /// <returns>The return valeau.</returns>
        public string Update(string userName, char[] password, string status)
        {
            //Important: "statuses", "update" and "xml" must be lower case.
            string url = string.Format(TwitterBaseUrlFormat, "statuses", "update", "xml");
            string data = string.Format("status={0}", HttpUtility.UrlEncode(status));

            return this.ExecutePostCommand(url, userName, password, data);
        }

        /// <summary>
        /// Update the twitter status with XML.
        /// </summary>
        /// <param name="userName">The twitter username.</param>
        /// <param name="password">The twitter password.</param>
        /// <param name="text">The message.</param>
        /// <returns>A xml document as response from the server.</returns>
        public XmlDocument UpdateAsXML(string userName, char[] password, string text)
        {
            string output = this.Update(userName, password, text);
            if (!string.IsNullOrEmpty(output))
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(output);

                return xmlDocument;
            }

            return null;
        }
        // Protected Methods (1) 

        /// <summary>
        /// Executes an HTTP POST command and retrives the information.
        /// This function will automatically include a "source" parameter if the "Source" property is set.
        /// </summary>
        /// <param name="url">The URL to perform the POST operation</param>
        /// <param name="userName">The username to use with the request</param>
        /// <param name="password">The password to use with the request</param>
        /// <param name="data">The data to post</param> 
        /// <returns>The response of the request, or null if we got 404 or nothing.</returns>
        protected string ExecutePostCommand(string url, string userName, char[] password, string data)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            WebRequest request = WebRequest.Create(url);
            if (!string.IsNullOrEmpty(userName) && password.Length>0)
            {
                StringBuilder sbpass = new StringBuilder();
                for (int i = 0; i < password.Length; i++)
                {
                    sbpass.Append(password[i]);
                }
                request.Credentials = new NetworkCredential(userName, sbpass.ToString());
                sbpass.Remove(0, password.Length);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                if (!string.IsNullOrEmpty(TrayIcon.AssemblyTitle))
                {
                    request.Headers.Add("X-Twitter-Client", TrayIcon.AssemblyTitle.Trim());
                }

                if (!string.IsNullOrEmpty(TrayIcon.AssemblyVersion))
                {
                    request.Headers.Add("X-Twitter-Version", TrayIcon.AssemblyVersion.Trim());
                }

                if (!string.IsNullOrEmpty(this.twitterClientUrl))
                {
                    request.Headers.Add("X-Twitter-URL", this.twitterClientUrl);
                }

                if (!string.IsNullOrEmpty(this.twitterClientUrl))
                {
                    data += "&source=" + HttpUtility.UrlEncode(this.twitterClientUrl);
                }

                byte[] bytes = Encoding.UTF8.GetBytes(data);
                xmlHandler getsettting = new xmlHandler(true);
                if (getsettting.getXMLnodeAsBool("useproxy") == true)
                {
                    string addr = getsettting.getXMLnode("proxyaddr");
                    if (String.IsNullOrEmpty(addr) || addr == "0.0.0.0")
                    {
                        string novalidproxy = "Proxy address is not given/not valid.";
                        MessageBox.Show(novalidproxy);
                        Log.Write(LogType.error, novalidproxy);
                        return null;
                    }
                    else
                    {
                        request.Proxy = new WebProxy(getsettting.getXMLnode("proxyaddr"));
                    }
                }

                request.Timeout = getsettting.getXMLnodeAsInt("timeout");
                request.ContentLength = bytes.Length;
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);

                    try
                    {
                        using (WebResponse response = request.GetResponse())
                        {
                            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                            {
                                return reader.ReadToEnd();
                            }
                        }
                    }
                    catch (TimeoutException)
                    {
                        MessageBox.Show("Error: connection timeout.");
                    }
                    catch (NotSupportedException notsupexc)
                    {
                        MessageBox.Show("Error: cannot send POST message:\r\n " + notsupexc.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Twitter username or/and password not filled in.");
            }

            return null;
        }

        #endregion Methods
    }
}
