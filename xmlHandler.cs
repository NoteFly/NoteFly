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
using System.Xml;
using System.IO;
using System.Web;
using System.Net;
using System.Windows.Forms;

namespace SimplePlainNote
{
    class xmlHandler
    {
        #region datavelden
        private string filenm;
        private string appdatafolder;
        private XmlTextReader objXmlTextReader;
        private XmlTextWriter objXmlTextWriter;

        //used twitter ip to prevented dns lookup, against dns attacks.
        protected const string TwitterBaseUrlFormat = "http://168.143.162.68/{0}/{1}.{2}";
        #endregion

        #region constructor
        public xmlHandler(bool issetting)
        {
            if (issetting == true)
            {
                CheckSettings();
            }
            else
            {
                MessageBox.Show("Filename expected.");
            }
        }

        public xmlHandler(bool issetting, string filenm)
        {
            if (issetting == false)
            {
                appdatafolder = System.Environment.GetEnvironmentVariable("APPDATA") + "\\.simpleplainnote\\";
                if (Directory.Exists(appdatafolder) == false) { Directory.CreateDirectory(appdatafolder); }
                this.filenm = filenm;
            }
            else
            {
                CheckSettings();
            }
        }

        #endregion

        #region properties
        public string AppDataFolder
        {
            get { return this.appdatafolder; }
        }

        private string source = null;

        private string twitterClient = "spn";
        private string twitterClientVersion = "0.5.0";
        private string twitterClientUrl = "http://code.google.com/p/simpleplainnote/";

        /// <summary>
        /// Sets the version of the Twitter client.
        /// According to the Twitter Fan Wiki at http://twitter.pbwiki.com/API-Docs and supported by
        /// the Twitter developers, this will be used in the future (hopefully near) to set more information
        /// in Twitter about the client posting the information as well as future usage in a clients directory.
        /// </summary>
        public string TwitterClientVersion
        {
            get { return twitterClientVersion; }
            set { twitterClientVersion = value; }
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
        #endregion

        #region methoden
        /// <summary>
        /// Write settings
        /// </summary>
        /// <param name="transparecy"></param>
        /// <param name="transparecylevel"></param>
        /// <param name="numcolor"></param>
        /// <returns>true if succeed.</returns>
        /// 

        private void CheckSettings()
        {
            appdatafolder = System.Environment.GetEnvironmentVariable("APPDATA") + "\\.simpleplainnote\\";
            if (Directory.Exists(appdatafolder) == false) { Directory.CreateDirectory(appdatafolder); }
            this.filenm = "settings.xml";
            if (File.Exists(appdatafolder + filenm) == false)
            {
                WriteSettings(true, 95, 0, appdatafolder, "Microsoft Sans Serif", "adres@domain.com", true, "", "");
            }         
        }

        public bool WriteSettings(bool transparecy, decimal transparecylevel, int numcolor, string fontcontent, string notesavepath, string defaultemail, bool syntaxhighlight, string twitteruser, string twitterpass)
        {
            try
            {
                if (CheckFile())
                {
                    return false;
                }

                objXmlTextWriter = new XmlTextWriter(appdatafolder + filenm, null);
                objXmlTextWriter.Formatting = Formatting.Indented;

                objXmlTextWriter.WriteStartDocument();
                objXmlTextWriter.WriteStartElement("settings");
                
                objXmlTextWriter.WriteStartElement("transparecy");
                if (transparecy == true)
                {                   
                    objXmlTextWriter.WriteString("1");                    
                }
                else
                {                    
                    objXmlTextWriter.WriteString("0");                    
                }
                objXmlTextWriter.WriteEndElement();

                objXmlTextWriter.WriteStartElement("transparecylevel");
                objXmlTextWriter.WriteString(Convert.ToString(transparecylevel));
                objXmlTextWriter.WriteEndElement();

                if (numcolor < 0) { throw new Exception("Impossible selection"); }

                objXmlTextWriter.WriteStartElement("defaultcolor");
                objXmlTextWriter.WriteString(Convert.ToString(numcolor));
                objXmlTextWriter.WriteEndElement();

                objXmlTextWriter.WriteStartElement("fontcontent");
                objXmlTextWriter.WriteString(fontcontent);
                objXmlTextWriter.WriteEndElement();   

                if (Directory.Exists(notesavepath))
                {
                    objXmlTextWriter.WriteStartElement("notesavepath");
                    objXmlTextWriter.WriteString(notesavepath);
                    objXmlTextWriter.WriteEndElement();
                }
                else { throw new Exception("dir not exist"); }

                objXmlTextWriter.WriteStartElement("syntaxhighlight");
                if (syntaxhighlight == true)
                {                    
                    objXmlTextWriter.WriteString("1");
                    
                }
                else
                {                    
                    objXmlTextWriter.WriteString("0");                    
                }
                objXmlTextWriter.WriteEndElement();

                objXmlTextWriter.WriteStartElement("defaultemail");
                objXmlTextWriter.WriteString(defaultemail);
                objXmlTextWriter.WriteEndElement();

                objXmlTextWriter.WriteStartElement("twitter");

                if (twitteruser.Length > 15) { throw new Exception("twitter username too long."); }
                objXmlTextWriter.WriteStartElement("twitteruser");
                objXmlTextWriter.WriteString(Convert.ToString(twitteruser));
                objXmlTextWriter.WriteEndElement();

                if ((twitterpass.Length < 6) && (twitterpass != "")) { throw new Exception("twitter password too short."); }
                if (twitterpass.Length > 255) { throw new Exception("twitter password too long."); }
                objXmlTextWriter.WriteStartElement("twitterpass");
                if (twitterpass != "")
                {
                    objXmlTextWriter.WriteString(twitterpass);
                }
                objXmlTextWriter.WriteEndElement();

                objXmlTextWriter.WriteEndElement();

                objXmlTextWriter.WriteEndElement();
                objXmlTextWriter.WriteEndDocument();

                objXmlTextWriter.Flush();
                objXmlTextWriter.Close();

                if (CheckFile())
                {
                    return false;
                }

                return true;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
            catch (IOException)
            {
                return false;
            }
            catch (XmlException)
            {
                return false;
            }
        }

        public bool WriteNote(bool visible, string numcolor, string title, string content, int locX, int locY, int notewidth, int noteheight)
        {
            try
            {
                if (Directory.Exists(appdatafolder) == false)
                {
                    try
                    {
                        Directory.CreateDirectory(appdatafolder);
                    }
                    catch (DirectoryNotFoundException exc)
                    {
                        System.Windows.Forms.MessageBox.Show("error " + exc.Message);
                    }
                }
                objXmlTextWriter = new XmlTextWriter(appdatafolder + filenm, null);

                objXmlTextWriter.Formatting = Formatting.Indented;

                objXmlTextWriter.WriteStartDocument();

                objXmlTextWriter.WriteStartElement("note");
                
                if (visible == true)
                {
                    objXmlTextWriter.WriteElementString("visible", "1");
                }
                else
                {
                    objXmlTextWriter.WriteElementString("visible", "0");
                }                

                objXmlTextWriter.WriteStartElement("color");
                objXmlTextWriter.WriteString(numcolor);
                objXmlTextWriter.WriteEndElement();

                objXmlTextWriter.WriteStartElement("title");
                objXmlTextWriter.WriteString(title);
                objXmlTextWriter.WriteEndElement();

                objXmlTextWriter.WriteStartElement("content");
                objXmlTextWriter.WriteString(content);
                objXmlTextWriter.WriteEndElement();

                objXmlTextWriter.WriteStartElement("location");
                objXmlTextWriter.WriteStartElement("x");
                objXmlTextWriter.WriteString(Convert.ToString(locX));
                objXmlTextWriter.WriteEndElement();
                objXmlTextWriter.WriteStartElement("y");
                objXmlTextWriter.WriteString(Convert.ToString(locY));
                objXmlTextWriter.WriteEndElement();
                objXmlTextWriter.WriteEndElement();

                objXmlTextWriter.WriteStartElement("size");
                objXmlTextWriter.WriteStartElement("width");
                objXmlTextWriter.WriteString(Convert.ToString(notewidth));
                objXmlTextWriter.WriteEndElement();
                objXmlTextWriter.WriteStartElement("heigth");
                objXmlTextWriter.WriteString(Convert.ToString(noteheight));
                objXmlTextWriter.WriteEndElement();
                objXmlTextWriter.WriteEndElement();
                objXmlTextWriter.WriteEndElement();

                objXmlTextWriter.WriteEndDocument();

                objXmlTextWriter.Flush();
                objXmlTextWriter.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Does some checks on the file
        /// - Is the file empty?
        /// - Is the file too large?
        /// </summary>
        /// <returns>false if no errors</returns>
        private bool CheckFile()
        {
            if (File.Exists(appdatafolder + filenm) == true)
            {
                FileInfo checkfile = new FileInfo(appdatafolder + filenm);
                if (checkfile.Length == 0)
                {
                    MessageBox.Show("File empty.");

                    //create backup copy, just in case.
                    string bakfile = appdatafolder + filenm + ".bak";
                    int num = 1;
                    while (File.Exists(bakfile) == true)
                    {
                        num++;
                        if (num > 99) { return true; }
                        bakfile = appdatafolder + filenm + ".bak" + num;
                    }
                    if (File.Exists(bakfile) == false)
                    {
                        try
                        {
                            checkfile.MoveTo(bakfile);
                            return true;
                        }
                        catch (IOException)
                        {
                            MessageBox.Show("Failed making backup copy.");
                            return true;
                        }
                    }
                    return false;
                }
                else if (checkfile.Length > 32768)
                {
                    MessageBox.Show("File is unusual big. >32kb");
                    return true;
                }
                //File looks okay.
                else
                {
                    return false;
                }
            }
            //File does not exist yet, so it okay.
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get a xml node
        /// </summary>
        /// <param name="nodename"></param>
        /// <returns>return node as string</returns>
        public String getXMLnode(string nodename)
        {
            try
            {
                objXmlTextReader = new XmlTextReader(appdatafolder + filenm);
            }
            catch (FileLoadException fileloadexc)
            {
                MessageBox.Show("Error: " + fileloadexc.Message);
                return "";
            }
            catch (FileNotFoundException filenotfoundexc)
            {
                MessageBox.Show("Error: " + filenotfoundexc.Message);
                return "";
            }

            if (objXmlTextReader == null)
            {
                MessageBox.Show("Error: objXmlTextReader is null.");
            }
            while (objXmlTextReader.Read())
            {
                if (objXmlTextReader.Name == nodename)
                {
                    string s = objXmlTextReader.ReadElementContentAsString();
                    objXmlTextReader.Close();
                    return s;
                }
            }
            //error node not found.
            return "";
        }

        /// <summary>
        /// Get a xml node
        /// </summary>
        /// <param name="nodename"></param>
        /// <returns>return node as integer, -1 if error</returns>
        public int getXMLnodeAsInt(string nodename)
        {            
            objXmlTextReader = new XmlTextReader(appdatafolder + filenm);

            while (objXmlTextReader.Read())
            {
                if (objXmlTextReader.Name == nodename)
                {
                    try
                    {
                        int n = objXmlTextReader.ReadElementContentAsInt();
                        objXmlTextReader.Close();
                        return n;
                    }
                    catch (InvalidCastException castexc)
                    {
                        objXmlTextReader.Close();
                        MessageBox.Show("Error casting. "+castexc.Message);
                    }
                    catch (FormatException formatexc)
                    {
                        MessageBox.Show("Error format. "+formatexc.Message);
                    }
                }
            }
            objXmlTextReader.Close();
            return -1;
        }

        public bool getXMLnodeAsBool(string nodename)
        {
            objXmlTextReader = new XmlTextReader(appdatafolder + filenm);

            while (objXmlTextReader.Read())
            {
                if (objXmlTextReader.Name == nodename)
                {
                    try
                    {
                        bool nodevalue = objXmlTextReader.ReadElementContentAsBoolean();
                        objXmlTextReader.Close();
                        return nodevalue;
                    }
                    catch (InvalidCastException castexc)
                    {
                        objXmlTextReader.Close();
                        MessageBox.Show("Error casting. " + castexc.Message);
                    }
                    catch (FormatException formatexc)
                    {
                        MessageBox.Show("Error format. " + formatexc.Message);
                    }
                }
            }
            objXmlTextReader.Close();
            return false;
        }

        #region twitter support (TODO: move to seperated class)
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

        public string Update(string userName, string password, string status)
        {
            //Important: "statuses", "update" and "xml" must be lower case.
            string url = string.Format(TwitterBaseUrlFormat, "statuses", "update", "xml");
            string data = string.Format("status={0}", HttpUtility.UrlEncode(status));

            return ExecutePostCommand(url, userName, password, data);
        }

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
                if (!string.IsNullOrEmpty(twitterClient))
                {
                    request.Headers.Add("X-Twitter-Client", twitterClient);
                }
                if (!string.IsNullOrEmpty(TwitterClientVersion))
                {
                    request.Headers.Add("X-Twitter-Version", TwitterClientVersion);
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
                        MessageBox.Show("Error: cannot send POST message:\r\n "+notsupexc.Message);
                    }

                }
            }
            else
            {
                MessageBox.Show("Twitter username or/and password not filled in.");
            }
            return null;
        }
        #endregion
        #endregion
    }
}