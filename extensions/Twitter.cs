//-----------------------------------------------------------------------
// <copyright file="Twitter.cs" company="GNU">
//  NoteFly a note application.
//  Copyright (C) 2010  Tom
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

        private OAuth.OAuthBase oauth;

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
            if (!string.IsNullOrEmpty(userName) && password.Length > 0)
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
                if (!string.IsNullOrEmpty(Program.AssemblyTitle))
                {
                    request.Headers.Add("X-Twitter-Client", Program.AssemblyTitle.Trim());
                }

                if (!string.IsNullOrEmpty(Program.AssemblyVersion))
                {
                    request.Headers.Add("X-Twitter-Version", Program.AssemblyVersion.Trim());
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
                //xmlHandler getsettting = new xmlHandler(true);
                if (Settings.NetworkProxyEnabled)
                {
                    string addr = Settings.NetworkProxyAddress;
                    if (String.IsNullOrEmpty(addr) || addr == "0.0.0.0")
                    {
                        string novalidproxy = "Proxy address is not given/not valid.";
                        MessageBox.Show(novalidproxy);
                        Log.Write(LogType.error, novalidproxy);
                        return null;
                    }
                    else
                    {
                        request.Proxy = new WebProxy(Settings.NetworkProxyAddress);
                    }
                }

                request.Timeout = Settings.NetworkConnectionTimeout;
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


        //if (this.rtbNote.Text.Length <= 140)
        //{
        //    this.Tweetnote();
        //}
        //else if (this.rtbNote.Text.Length > 140)
        //{
        //    DialogResult result;
        //    string shrttweet = this.note.Substring(0, 140);
        //    result = MessageBox.Show("Your note is more than the 140 chars.\r\nDo you want to publish only the first 140 characters? ", "Too long", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //    if (result == DialogResult.Yes)
        //    {
        //        this.Tweetnote();
        //        Log.Write(LogType.info, "Shorted note send to twitter.");
        //    }
        //}

        //string twitteruser = getSettings.getXMLnode("twitteruser");

        //if (String.IsNullOrEmpty(twitteruser))
        //{
        //    string notwusername = "You haven't set your twitter username yet.\r\nSettings window will now open.";
        //    Log.Write(LogType.error, notwusername.Replace("\r\n", ""));
        //    MessageBox.Show(notwusername);
        //    FrmSettings settings = new FrmSettings(this.notes);
        //    settings.Show();
        //    return;
        //}

        //if (this.twpass == null)
        //{
        //    this.twpass = getSettings.getXMLnode("twitterpass").ToCharArray();
        //}

        //if ((this.twpass == null) || (this.twpass.Length <= 0))
        //{
        //    Form askpass = new Form();
        //    askpass.ShowIcon = false;
        //    askpass.Height = 80;
        //    askpass.Width = 280;
        //    askpass.Text = "Twitter password needed";
        //    askpass.Show();
        //    TextBox tbpass = new TextBox();
        //    tbpass.Location = new Point(10, 10);
        //    tbpass.Width = 160;
        //    tbpass.Name = "tbPassword";
        //    tbpass.PasswordChar = 'X';
        //    Button btnOk = new Button();
        //    btnOk.Location = new Point(180, 10);
        //    btnOk.Text = "Ok";
        //    btnOk.Width = 80;
        //    btnOk.Name = "btnOk";
        //    btnOk.Click += this.Askpassok;
        //    askpass.Controls.Add(tbpass);
        //    askpass.Controls.Add(btnOk);
        //}
        //else
        //{
        //    Twitter twitter = new Twitter();
        //    const string twitterleadmgs = "Sending note to twitter ";
        //    if (twitter.UpdateAsXML(twitteruser, this.twpass, this.note) != null)
        //    {
        //        string sendtwsucces = twitterleadmgs + "succeded.";
        //        Log.Write(LogType.info, sendtwsucces);
        //        MessageBox.Show(sendtwsucces);
        //    }
        //    else
        //    {
        //        string sendtwfail = twitterleadmgs + "failed.";
        //        Log.Write(LogType.error, sendtwfail);
        //        MessageBox.Show(sendtwfail);
        //    }
        //}
    }
}
