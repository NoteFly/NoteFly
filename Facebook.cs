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
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Web;
//using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Globalization;

namespace NoteFly
{
    public class Facebook
    {
        public const String fbcancelurl = "http://www.facebook.com/connect/login_failure.html";
        public const String fbsuccessurl = "http://www.facebook.com/connect/login_success.html";
        private const String restserverurl = "http://api.facebook.com/restserver.php";

        private const String appkey = "cced88bcd1585fa3862e7fd17b2f6986";
        private const String apiversion = "1.0";
        
        private String p_session_key;
        private String p_uid;
        private String p_expires;
        private String p_secret;
        private String p_sig;

        public String CreateLoginURL()
        {
            //http://www.facebook.com/login.php?api_key=cced88bcd1585fa3862e7fd17b2f6986&connect_display=popup&v=1.0&fbconnect=true&session_key_only=true&return_session=true&next=http://www.facebook.com/connect/login_success.html&cancel_url=http://www.facebook.com/connect/login_failure.html
            return "http://www.facebook.com/login.php?api_key=" + appkey + "&connect_display=popup&v=" + apiversion + "&fbconnect=true&session_key_only=true&return_session=true&next=" + fbsuccessurl + "&cancel_url=" + fbcancelurl;
        }

        /// <summary>
        /// Post a message to the stream.
        /// </summary>
        /// <param name="message">the message</param>
        /// <returns>response code (as json or xml?)</returns>
        public String PostStream(String message)
        {
            WebRequest request = WebRequest.Create(restserverurl);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Timeout = 10000; //10secs

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
                catch (Exception exc)
                {
                    MessageBox.Show("Error: " + exc.Message);
                }
            }
            return null;
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
            data += "&session_key=" + p_session_key;
            data += "&ss=1";
            data += "&uid=" + p_uid;
            data += "&v=" + apiversion;                                    

            data += "&sig=" + GenerateSignature(p_expires, callid, message, p_session_key, "1", p_uid);
            return data;
        }

        /// <summary>
        /// Create a signature.. if this works, come on..
        /// </summary>
        /// <param name="expires"></param>
        /// <param name="session_key"></param>
        /// <param name="ss"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        private String GenerateSignature(String expires, String call_id, String message, String session_key, String ss_bool, String uid)
        {
            var md5 = MD5.Create();
            String methode_s = "facebook.stream.publish";
            String data = "api_key=" + appkey + "call_id=" + call_id + "message=" + message + "method="+methode_s+"session_key=" + p_session_key + "ss=" + ss_bool + "uid=" + uid + "v=" + apiversion + p_secret;                        
            String hash = MakeMD5(data);
            if (hash.Length == 32) return hash;
            else throw new CustomExceptions("error: cannot generating MD5 hash.");

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
        /// parser xml respons, throw error on error code return.
        /// </summary>
        public void CheckResponse(String responsestream)
        {
            //todo
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
