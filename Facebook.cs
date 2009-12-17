/* Copyright (C) 2009
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
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace NoteFly
{
    /// <summary>
    /// All settings needed for a facebook session.
    /// </summary>
    public static class FacebookSettings
    {
        #region Fields (4)

        public static double expires;
        public static String secret;
        public static String sessionkey;
        public static String uid;

        #endregion Fields
    }

    /// <summary>
    /// Communicates with facebook.
    /// </summary>
    public class Facebook
    {
        #region Fields (9)

        private const String apiversion = "1.0";
        private const String appkey = "cced88bcd1585fa3862e7fd17b2f6986";
        private const String fbcancelurl = "http://www.facebook.com/connect/login_failure.html";
        private const String fbloginpage = "http://www.facebook.com/login.php";
        private const String fbrestserverurl = "http://api.facebook.com/restserver.php";
        private const String fbsuccessurl = "http://www.facebook.com/connect/login_success.html";
        private Form frmLoginFb;
        private String message;
        private String p_sig;

        #endregion Fields

        #region Constructors (1)

        public Facebook()
        {

        }

        #endregion Constructors

        #region Methods (10)

        // Public Methods (5) 

        /// <summary>
        /// parser xml response, return errorcode if it returned by facebook.
        /// </summary>
        public void CheckResponse(String responsestream)
        {
            int responsecode = 0;
            String errorcodestartnode = "<error_code>";
            String errorcodeendnode = "</error_code>";

            if (String.IsNullOrEmpty(responsestream))
            { responsecode = 1; }
            else if (responsestream.Contains(errorcodestartnode))
            {
                responsecode = 1;
                int startpos = responsestream.IndexOf(errorcodestartnode) + errorcodestartnode.Length;
                int lenvalnode = responsestream.IndexOf(errorcodeendnode) - startpos;
                string errorcode = responsestream.Substring(startpos, lenvalnode);
                try
                {
                    responsecode = Convert.ToInt32(errorcode);
                }
                catch (Exception)
                {
                    responsecode = -1;
                }
            }
            switch (responsecode)
            {
                case 0:
                    String notefbposted = "Your note is posted on your facebook wall.";
                    MessageBox.Show(notefbposted);
                    Log.write(LogType.info, notefbposted);
                    break;
                case 1:
                    String unknowfberror = "Unknow facebook error occurred";
                    MessageBox.Show(unknowfberror);
                    Log.write(LogType.error, unknowfberror);
                    break;
                case 100:
                    String fbinvalidparam = "Invalid paramters.";
                    MessageBox.Show(fbinvalidparam);
                    Log.write(LogType.error, fbinvalidparam);
                    break;
                case 104:
                    String fbinvalidsig = "Signature was invalid.";
                    MessageBox.Show(fbinvalidsig);
                    Log.write(LogType.error, fbinvalidsig);
                    break;
                case 200:
                    String fbprimission = "No proper primision to post on your wall.";
                    MessageBox.Show(fbprimission);
                    Log.write(LogType.error, fbprimission);
                    break;
                case 210:
                    String fbusernotvisible = "User not visible.\r\nThe user doesn't have permission to act on that object.";
                    MessageBox.Show(fbusernotvisible);
                    Log.write(LogType.error, fbusernotvisible);
                    break;
                case 340:
                    String fbfeedlimit = "Feed action request limit reached.";
                    MessageBox.Show(fbfeedlimit);
                    Log.write(LogType.error, fbfeedlimit);
                    break;
                case -1:
                    String notparsererrcode = "Could not parser the errorcode that was returned.";
                    MessageBox.Show(notparsererrcode);
                    Log.write(LogType.error, notparsererrcode);
                    break;
                default:
                    String errcode = "Facebook returned unknow errorcode: " + responsecode;
                    MessageBox.Show(errcode);
                    Log.write(LogType.error, errcode);
                    break;
            }
            frmLoginFb.Close();
        }

        /// <summary>
        /// Generate a md5 hash from the given string.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public String MakeMD5(String input)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// Check the url if facebook navigated to the succeed page
        /// meaning that we have a session.
        /// This also gets all parameters needed from the Url.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>True if url is on the succeed page</returns>
        public Boolean IsSucceedUrl(string url)
        {
            if (url.StartsWith(fbsuccessurl) == true)
            {
                String parm = url.ToString().Substring(60, url.Length - 61);
                String[] parms = parm.Split(',');

                if (!String.IsNullOrEmpty(parms[0]) && !String.IsNullOrEmpty(parms[4]))
                {
                    foreach (String curparm in parms)
                    {
                        if (curparm.StartsWith("\"session_key\":"))
                        {
                            FacebookSettings.sessionkey = curparm.Substring(15, curparm.Length - 16);
                        }
                        else if (curparm.StartsWith("\"uid\":"))
                        {
                            FacebookSettings.uid = curparm.Substring(7, curparm.Length - 8);
                        }
                        else if (curparm.StartsWith("\"expires\":"))
                        {
                            try
                            {
                                FacebookSettings.expires = Convert.ToDouble(curparm.Substring(10, curparm.Length - 10));
                            }
                            catch (Exception)
                            {
                                throw new CustomException("cannot parser unix time.");
                            }
                        }
                        else if (curparm.StartsWith("\"secret\":"))
                        {
                            FacebookSettings.secret = curparm.Substring(10, curparm.Length - 11);
                        }
                        else if (curparm.StartsWith("\"sig\":"))
                        {
                            p_sig = curparm.Substring(7, curparm.Length - 8);
                        }
                    }
                    return true;
                }
                else
                {
                    throw new CustomException("cannot parser url parameters.");
                }
            }
            else if (url.StartsWith(fbcancelurl) == true)
            {
                return false;
            }
            return false;
        }

        /// <summary>
        /// Post a message to the stream.
        /// </summary>
        /// <param name="message">the message</param>
        /// <returns>response code (as json or xml?)</returns>
        public String PostStream(String message)
        {
            WebRequest request = WebRequest.Create(fbrestserverurl);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Timeout = 10000; //10secs

            xmlHandler getsettting = new xmlHandler(true);
            if (getsettting.getXMLnodeAsBool("useproxy") == true)
            {
                String addr = getsettting.getXMLnode("proxyaddr");
                if (String.IsNullOrEmpty(addr) || addr == "0.0.0.0")
                {
                    String novalidproxy = "Proxy address is not given";
                    MessageBox.Show(novalidproxy);
                    Log.write(LogType.error, novalidproxy);
                    return "";
                }
                else
                {
                    request.Proxy = new WebProxy(getsettting.getXMLnode("proxyaddr"));
                }
            }

            string data = CreatePostData(message);

            byte[] bytes = Encoding.UTF8.GetBytes(data);
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
                    String contimeout = "connection timeout";
                    MessageBox.Show(contimeout);
                    Log.write(LogType.error, contimeout);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Exception: " + exc.Message);
                    Log.write(LogType.exception, exc.Message);
                }
            }
            return null;
        }

        public void StartPostingNote(string note)
        {
            this.message = note;
            if (String.IsNullOrEmpty(FacebookSettings.sessionkey) || String.IsNullOrEmpty(FacebookSettings.secret) || String.IsNullOrEmpty(FacebookSettings.uid))
            {
                ShowFBLoginForm();
            }
            else
            {
                System.DateTime dtExpiresSession = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                dtExpiresSession = dtExpiresSession.AddSeconds(FacebookSettings.expires);

                if (dtExpiresSession.Month == DateTime.Now.Month)
                {
                    if (dtExpiresSession.Day == DateTime.Now.Day + 1)
                    {
                        CheckResponse(PostStream(this.message));
                    }
                    else if ((dtExpiresSession.Day == DateTime.Now.Day) && (dtExpiresSession.Hour > DateTime.Now.Hour))
                    {
                        CheckResponse(PostStream(this.message));
                    }
                    else
                    {
                        ShowFBLoginForm();
                    }

                }
                else if ((dtExpiresSession.Month + 1 == DateTime.Now.Month) && (dtExpiresSession.Day == 1))
                {
                    CheckResponse(PostStream(this.message));
                }
                else
                {
                    ShowFBLoginForm();
                }
            }
        }
        // Private Methods (5) 

        /// <summary>
        /// Construct a right login url.
        /// </summary>
        /// <returns></returns>
        private String CreateLoginURL()
        {
            return fbloginpage + "?api_key=" + appkey + "&connect_display=popup&v=" + apiversion + "&fbconnect=true&session_key_only=true&return_session=true&next=" + fbsuccessurl + "&cancel_url=" + fbcancelurl;
        }

        /// <summary>
        /// Create the Data to post and attach the generated the signature.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private String CreatePostData(String message)
        {
            String data = "api_key=" + appkey;
            String callid = DateTime.Now.Ticks.ToString("x", CultureInfo.InvariantCulture);
            data += "&call_id=" + callid;
            data += "&message=" + message;
            data += "&method=facebook.stream.publish";
            data += "&session_key=" + FacebookSettings.sessionkey;
            String sessionsecret = "1";
            data += "&ss=" + sessionsecret;
            data += "&uid=" + FacebookSettings.uid;
            data += "&v=" + apiversion;

            String methode = "facebook.stream.publish";
            data += "&sig=" + GenerateSignature(callid, message, methode, FacebookSettings.sessionkey, sessionsecret, FacebookSettings.uid);
            return data;
        }

        private void FbWeb_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (IsSucceedUrl(e.Url.ToString()) == true)
            {
                CheckResponse(PostStream(this.message));
                frmLoginFb.Close();
            }
        }

        /// <summary>
        /// Create a signature based on serveral parameters
        /// </summary>
        /// <param name="call_id">something higher than previous call_id</param>
        /// <param name="message">the content to send</param>
        /// <param name="methode">the methode for rest api</param>
        /// <param name="session_key">session key</param>
        /// <param name="sessionsecret">session secret</param>
        /// <param name="uid">userid</param>
        /// <returns></returns>
        private String GenerateSignature(String call_id, String message, String methode, String session_key, String sessionsecret, String uid)
        {
            String data = "api_key=" + appkey + "call_id=" + call_id + "message=" + message + "method=" + methode + "session_key=" + FacebookSettings.sessionkey + "ss=" + sessionsecret + "uid=" + uid + "v=" + apiversion + FacebookSettings.secret;

            var md5 = MD5.Create();
            String hash = MakeMD5(data);
            if (hash.Length == 32) return hash;
            else throw new CustomException("Cannot generate MD5 hash.");

        }

        /// <summary>
        /// create a form with webbrowser and navigate to
        /// the facebook login page for this application.
        /// </summary>
        private void ShowFBLoginForm()
        {
            //grant access.form
            frmLoginFb = new Form();
            frmLoginFb.ShowIcon = false;
            frmLoginFb.StartPosition = FormStartPosition.CenterScreen;
            frmLoginFb.Text = "Post note on FaceBook";
            frmLoginFb.Width = 640;
            frmLoginFb.Height = 480;
            WebBrowser FbWeb = new WebBrowser();
            FbWeb.Name = "FbWeb";
            FbWeb.Location = new System.Drawing.Point(10, 10);
            FbWeb.Dock = DockStyle.Fill;
            frmLoginFb.Controls.Add(FbWeb);
            FbWeb.Navigated += new WebBrowserNavigatedEventHandler(FbWeb_Navigated);
            FbWeb.Navigate(CreateLoginURL());
            frmLoginFb.Show();
        }

        #endregion Methods
    }
}
