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
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Windows.Forms;

namespace NoteFly
{
    public class Facebook
    {
        public const String fbcancelurl = "http://www.facebook.com/connect/login_failure.html";
        public const String fbsuccessurl = "http://www.facebook.com/connect/login_success.html";

        private const String appkey = "cced88bcd1585fa3862e7fd17b2f6986";
        private const String apiversion = "1.0";

        private String p_session_key;
        private String p_uid;
        private String p_expires;
        private String p_secret;
        private String p_sig;

        public String CreateLoginURL()
        {
            return "http://www.facebook.com/login.php?api_key=" + appkey + "&connect_display=popup&v=" + apiversion + "&fbconnect=true&session_key_only=true&return_session=true&next=" + fbsuccessurl + "&cancel_url=" + fbcancelurl;
        }

        /// <summary>
        /// Post a message to the stream.
        /// </summary>
        /// <param name="message">the message</param>
        /// <returns>response code (as json or xml?)</returns>
        public String PostStream(String message)
        {
            String fburl = "http://api.facebook.com/restserver.php";
            WebRequest request = WebRequest.Create(fburl);
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            request.Timeout = 6000;

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
                    MessageBox.Show("Error: connection timeout. ");
                }
                catch (NotSupportedException notsupexc)
                {
                    MessageBox.Show("Error: cannot send POST message:\r\n " + notsupexc.Message);
                }
            }
            return null;
        }

        private String CreatePostData(String message)
        {
            string data = "method=facebook.stream.publish&message=" + message;
            data += "&uid="+p_uid;
            data += "&session_key=" + p_session_key;
            data += "&api_key=" + appkey;
            data += "&v=" + apiversion;
            data += "&ss=1";
            //todo
            data += "&call_id=" + "???????????????"; //find out..
            data += "????????????????????????????????"; //generate one...
            
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns>True if succeded</returns>
        public Boolean ParserURL(string url)
        {
            if (url.StartsWith(fbsuccessurl) == true)
            {
                String parm = url.ToString().Substring(60, url.Length - 61);
                String[] parms = parm.Split(',');                                

                if (!String.IsNullOrEmpty(parms[0]))
                {

                    foreach (String curparm in parms)
                    {
                        if (curparm.StartsWith("\"session_key\":"))
                        {
                            p_session_key = curparm.Substring(15, curparm.Length - 16);
                        }
                        else if (curparm.StartsWith("\"uid\":"))
                        {
                            p_uid = curparm.Substring(7, curparm.Length - 8);
                        }
                        else if (curparm.StartsWith("\"expires\":"))
                        {
                            p_expires = curparm.Substring(10, curparm.Length - 10);
                        }
                        else if (curparm.StartsWith("\"secret\":"))
                        {
                            p_secret = curparm.Substring(10, curparm.Length - 11);
                        }
                        else if (curparm.StartsWith("\"sig\":"))
                        {
                            p_sig = curparm.Substring(7, curparm.Length - 8);
                        }
                    }
                    MessageBox.Show("session_key=" + p_session_key);
                    MessageBox.Show("uid=" + p_uid);
                    MessageBox.Show("expires=" + p_expires);
                    MessageBox.Show("secret=" + p_secret);
                    MessageBox.Show("sig=" + p_sig);
                    return true;
                }
                else
                {
                    throw new CustomExceptions("error parsering url parameters");
                }
            }
            else if (url.StartsWith(fbcancelurl) == true)
            {
                return false;                                  
            }
            else
            { throw new CustomExceptions("error parsering url page"); }
        }
    }
}
