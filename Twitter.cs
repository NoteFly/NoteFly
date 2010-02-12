/* Copyright (C) 2009-2010
 * 
 * This program is free software; you can redistribute it and/or modify it
 * Free Software Foundation; either version 2, or (at your option) any
 * later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *  
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA
 */
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;
using System.Xml;

namespace NoteFly
{
    /// <summary>
    /// sends a note to twitter
    /// </summary>
    class Twitter
    {
		#region Fields (5) 

        private string source = null;
        //used twitter ip to prevented dns lookup, against dns attacks.
        protected const string TwitterBaseUrlFormat = "http://168.143.162.68/{0}/{1}.{2}";
        private const string twitterClientUrl = "http://code.google.com/p/simpleplainnote/";

		#endregion Fields 

		#region Constructors (1) 

        /// <summary>
        /// Creating a new instance of Twitter class.
        /// </summary>
        public Twitter()
        {

        }

		#endregion Constructors 

		#region Properties (3) 

        /// <summary>
        /// Source is an additional parameters that will be used to fill the "From" field.
        /// Currently you must talk to the developers of Twitter at:
        /// http://groups.google.com/group/twitter-development-talk/
        /// Otherwise, Twitter will simply ignore this parameter and set the "From" field to "web".
        /// </summary>
        public string Source
        {
            get { return source; }
            set { source = value; }
        }

        /// <summary>
        /// Sets the URL of the Twitter client.
        /// Must be in the XML format documented in the "Request Headers" section at:
        /// http://twitter.pbwiki.com/API-Docs.
        /// According to the Twitter Fan Wiki at http://twitter.pbwiki.com/API-Docs and supported by
        /// the Twitter developers, this will be used in the future (hopefully near) to set more information
        /// in Twitter about the client posting the information as well as future usage in a clients directory.		
        /// </summary>
        public string TwitterClientUrl
        {
            get { return twitterClientUrl; }
        }

		#endregion Properties 

		#region Methods (3) 

		// Public Methods (2) 

        public string Update(string userName, string password, string status)
        {
            //Important: "statuses", "update" and "xml" must be lower case.
            string url = string.Format(TwitterBaseUrlFormat, "statuses", "update", "xml");
            string data = string.Format("status={0}", HttpUtility.UrlEncode(status));

            return ExecutePostCommand(url, userName, password, data);
        }

        /// <summary>
        /// Update the twitter status with XML
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="text"></param>
        /// <returns>xml document</returns>
        public XmlDocument UpdateAsXML(string userName, string password, string text)
        {
            string output = Update(userName, password, text);
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
        protected string ExecutePostCommand(string url, string userName, string password, string data)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            WebRequest request = WebRequest.Create(url);
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                request.Credentials = new NetworkCredential(userName, password);
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
                if (!string.IsNullOrEmpty(TwitterClientUrl))
                {
                    request.Headers.Add("X-Twitter-URL", TwitterClientUrl);
                }
                if (!string.IsNullOrEmpty(Source))
                {
                    data += "&source=" + HttpUtility.UrlEncode(Source);
                }
                byte[] bytes = Encoding.UTF8.GetBytes(data);

                xmlHandler getsettting = new xmlHandler(true);
                if (getsettting.getXMLnodeAsBool("useproxy") == true)
                {
                    String addr = getsettting.getXMLnode("proxyaddr");
                    if (String.IsNullOrEmpty(addr) || addr == "0.0.0.0")
                    {
                        String novalidproxy = "Proxy address is not given/not valid";
                        MessageBox.Show(novalidproxy);
                        Log.write(LogType.error, novalidproxy);
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
