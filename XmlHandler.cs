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
using System.Xml;

namespace NoteFly
{
    class xmlHandler
    {
        #region Fields (5)

        private string appdatafolder = "";
        private string filenm;
        private bool issetting;
        private XmlTextReader objXmlTextReader;
        private XmlTextWriter objXmlTextWriter;

        #endregion Fields

        #region Constructors (2)

        public xmlHandler(bool issetting)
        {
            SetAppdataFolder();
            this.issetting = issetting;
            if (issetting)
            {
                if (Directory.Exists(appdatafolder) == false) { Directory.CreateDirectory(appdatafolder); }
                this.filenm = Path.Combine(appdatafolder, "settings.xml");
                if (File.Exists(filenm) == false)
                {
                    //write default settings.
                    WriteSettings(true, 95, 0, 1, true, "Verdana", 10, 0, appdatafolder, "", false, false, "", "", true, false, false, "", true);
                }
            }
        }

        public xmlHandler(string filenm)
        {
            SetAppdataFolder();
            this.filenm = filenm;
        }

        #endregion Constructors

        #region Properties (1)

        public string AppDataFolder
        {
            get
            {
                return this.appdatafolder;
            }
        }

        #endregion Properties

        #region Methods (7)

        // Public Methods (5) 

        /// <summary>
        /// Get a xml node
        /// </summary>
        /// <param name="nodename"></param>
        /// <returns>return node as string</returns>
        public String getXMLnode(string nodename)
        {
            try
            {
                objXmlTextReader = new XmlTextReader(filenm);
            }
            catch (FileLoadException fileloadexc)
            {
                throw new CustomException(fileloadexc.Message);
            }
            catch (FileNotFoundException filenotfoundexc)
            {
                throw new CustomException(filenotfoundexc.Message);
            }

            if (objXmlTextReader == null)
            {
                throw new CustomException("objXmlTextReader is null.");
            }
            while (objXmlTextReader.Read())
            {
                if (objXmlTextReader.Name == nodename)
                {
                    string s = "";
                    try
                    {
                        s = objXmlTextReader.ReadElementContentAsString();
                    }
                    finally
                    {
                        objXmlTextReader.Close();
                    }
                    return s;
                }
            }
            //error node not found.
            return "";
        }

        /// <summary>
        /// get xml node boolean valaue
        /// </summary>
        /// <param name="nodename"></param>
        /// <returns></returns>
        public bool getXMLnodeAsBool(string nodename)
        {
            try
            {
                objXmlTextReader = new XmlTextReader(filenm);

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
                        catch (InvalidCastException invalidcastexc)
                        {
                            throw new CustomException(invalidcastexc.Message);
                        }
                        finally
                        {
                            objXmlTextReader.Close();
                        }
                    }
                }
            }
            finally
            {
                objXmlTextReader.Close();
            }
            return false;
        }

        /// <summary>
        /// Get a xml node
        /// </summary>
        /// <param name="nodename"></param>
        /// <returns>return node as integer, -1 if error</returns>
        public int getXMLnodeAsInt(string nodename)
        {
            try
            {
                objXmlTextReader = new XmlTextReader(filenm);

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
                        catch (InvalidCastException invalidcastexc)
                        {
                            throw new CustomException(invalidcastexc.Message);
                        }
                        finally
                        {
                            objXmlTextReader.Close();
                        }
                    }
                }
            }
            finally
            {
                objXmlTextReader.Close();

            }
            return -1;
        }

        public Int32[] ParserNoteInts()
        {
            objXmlTextReader = new XmlTextReader(filenm);

            Int32[] settings = new Int32[5];

            while (objXmlTextReader.Read())
            {
                switch (objXmlTextReader.Name)
                {
                    case "color":
                        settings[0] = objXmlTextReader.ReadElementContentAsInt();
                        break;
                    case "x":
                        settings[1] = objXmlTextReader.ReadElementContentAsInt();
                        break;
                    case "y":
                        settings[2] = objXmlTextReader.ReadElementContentAsInt();
                        break;
                    case "width":
                        settings[3] = objXmlTextReader.ReadElementContentAsInt();
                        break;
                    case "heigth":
                        settings[4] = objXmlTextReader.ReadElementContentAsInt();
                        break;
                }
            }
            objXmlTextReader.Close();
            return settings;
        }

        /// <summary>
        /// Write a note xml file.
        /// </summary>
        /// <param name="visible"></param>
        /// <param name="ontop"></param>
        /// <param name="numcolor"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="locX"></param>
        /// <param name="locY"></param>
        /// <param name="notewidth"></param>
        /// <param name="noteheight"></param>
        /// <returns></returns>
        public bool WriteNote(bool visible, bool ontop, Int16 numcolor, string title, string content, int locX, int locY, int notewidth, int noteheight)
        {
            if (issetting)
            {
                throw new CustomException("This is a settings file, cannot write a note of it.");
            }

            try
            {
                objXmlTextWriter = new XmlTextWriter(this.filenm, null);

                objXmlTextWriter.Formatting = Formatting.Indented;

                objXmlTextWriter.WriteStartDocument();

                objXmlTextWriter.WriteStartElement("note");

                WriteXMLBool("visible", visible);

                WriteXMLBool("ontop", ontop);

                objXmlTextWriter.WriteElementString("color", Convert.ToString(numcolor));

                objXmlTextWriter.WriteElementString("title", title);

                objXmlTextWriter.WriteElementString("content", content); 

                objXmlTextWriter.WriteStartElement("location");
                    objXmlTextWriter.WriteElementString("x", Convert.ToString(locX));
                    objXmlTextWriter.WriteElementString("y", Convert.ToString(locY));
                objXmlTextWriter.WriteEndElement();

                objXmlTextWriter.WriteStartElement("size");
                    objXmlTextWriter.WriteElementString("width", Convert.ToString(notewidth));
                    objXmlTextWriter.WriteElementString("heigth", Convert.ToString(noteheight));
                objXmlTextWriter.WriteEndElement();

                objXmlTextWriter.WriteEndElement();

                objXmlTextWriter.WriteEndDocument();
            }
            finally
            {
                objXmlTextWriter.Flush();
                objXmlTextWriter.Close();
            }
            CheckFile();

            return true;
        }

        /// <summary>
        /// Write settings file
        /// </summary>
        /// <returns>true if succeed.</returns>
        /// 
        public bool WriteSettings(bool transparecy, decimal transparecylevel, int numcolor, int actionleftclick, bool askurl, string fontcontent, decimal fontsize, int textdirection, string notesavepath, string defaultemail, bool highlightHTML, bool confirmexit, string twitteruser, string twitterpass, bool logerror, bool loginfo, bool useproxy, string proxyaddr, Boolean savefacebooksession)
        {
            if (!this.issetting)
            {
                throw new CustomException("not settings file");
            }

            try
            {
                objXmlTextWriter = new XmlTextWriter(filenm, null);
                objXmlTextWriter.Formatting = Formatting.Indented;

                objXmlTextWriter.WriteStartDocument();
                objXmlTextWriter.WriteStartElement("settings");

                WriteXMLBool("transparecy", transparecy);

                objXmlTextWriter.WriteElementString("transparecylevel", Convert.ToString(transparecylevel));

                if (numcolor < 0) { throw new CustomException("Impossible selection"); }
                else
                {
                    objXmlTextWriter.WriteElementString("defaultcolor", Convert.ToString(numcolor));
                }
                if ((actionleftclick < 0) || (actionleftclick > 3)) { throw new CustomException("action left click unknow"); }
                else
                {
                    objXmlTextWriter.WriteElementString("actionleftclick", Convert.ToString(actionleftclick));
                }
                if (numcolor >= 8) { throw new CustomException("default color unknow"); }
                else
                {
                    objXmlTextWriter.WriteElementString("defaultcolor", Convert.ToString(numcolor));
                }

                WriteXMLBool("askurl", askurl);

                if (String.IsNullOrEmpty(fontcontent)) { throw new CustomException("No font"); }
                else
                {
                    objXmlTextWriter.WriteElementString("fontcontent", fontcontent);
                }
                objXmlTextWriter.WriteElementString("fontsize", Convert.ToString(fontsize));

                if ((textdirection < 0) || (textdirection > 2)) { throw new CustomException("Invalid text direction"); }
                else
                {
                    objXmlTextWriter.WriteElementString("textdirection", Convert.ToString(textdirection));
                }
                if (Directory.Exists(notesavepath))
                {
                    objXmlTextWriter.WriteElementString("notesavepath", notesavepath);
                }
                else { throw new CustomException("Directory does not exist"); }

                WriteXMLBool("logerror", logerror);

                WriteXMLBool("loginfo", logerror);

                objXmlTextWriter.WriteStartElement("syntaxhighlight");
                    WriteXMLBool("highlightHTML", highlightHTML);
                    //WriteXMLBool("highlightC", highlightC);
                objXmlTextWriter.WriteEndElement();

                WriteXMLBool("confirmexit", confirmexit);

                objXmlTextWriter.WriteElementString("defaultemail", defaultemail);

                WriteXMLBool("useproxy", useproxy);

                objXmlTextWriter.WriteElementString("proxyaddr", proxyaddr);

                objXmlTextWriter.WriteStartElement("facebook");

                WriteXMLBool("savesession", savefacebooksession);

                if (savefacebooksession && !String.IsNullOrEmpty(FacebookSettings.uid) && !String.IsNullOrEmpty(FacebookSettings.sessionsecret) && !String.IsNullOrEmpty(FacebookSettings.sessionkey) && FacebookSettings.sesionexpires != 0)
                {
                    objXmlTextWriter.WriteElementString("uid", FacebookSettings.uid);
                    objXmlTextWriter.WriteElementString("sesionexpires", Convert.ToString(FacebookSettings.sesionexpires));
                    objXmlTextWriter.WriteElementString("sessionsecret", FacebookSettings.sessionsecret);
                    objXmlTextWriter.WriteElementString("sessionkey", FacebookSettings.sessionkey);
                }
                else
                {
                    objXmlTextWriter.WriteElementString("uid", "");
                    objXmlTextWriter.WriteElementString("sesionexpires", "");
                    objXmlTextWriter.WriteElementString("sessionsecret", "");
                    objXmlTextWriter.WriteElementString("sessionkey", "");
                }
                objXmlTextWriter.WriteEndElement();

                objXmlTextWriter.WriteStartElement("twitter");
                if (twitteruser.Length > 15) { throw new CustomException("Twitter username too long."); }
                else
                {
                    objXmlTextWriter.WriteElementString("twitteruser", Convert.ToString(twitteruser));
                }
                if ((twitterpass.Length < 6) && (twitterpass != "")) { throw new CustomException("Twitter password too short."); }
                else
                {
                    if (twitterpass.Length > 255) { throw new CustomException("Twitter password too long."); }

                    objXmlTextWriter.WriteElementString("twitterpass", twitterpass);
                }

                objXmlTextWriter.WriteEndElement();

                objXmlTextWriter.WriteEndElement();
                objXmlTextWriter.WriteEndDocument();
            }
            finally
            {
                objXmlTextWriter.Flush();
                objXmlTextWriter.Close();
            }

            CheckFile();

            return true;
        }

        // Private Methods (2) 

        /// <summary>
        /// write 1 for true and 0 for false.
        /// </summary>
        /// <param name="checknode"></param>
        private void WriteXMLBool(String element, bool checknode)
        {
            objXmlTextWriter.WriteStartElement(element);
            if (checknode == true)
            {
                objXmlTextWriter.WriteString("1");
            }
            else
            {
                objXmlTextWriter.WriteString("0");
            }
            objXmlTextWriter.WriteEndElement();
        }

        /// <summary>
        /// Does some checks on the file
        /// - Is the file empty?
        /// - Is the file too large?
        /// </summary>        
        private void CheckFile()
        {
            if (File.Exists(filenm) == true)
            {
                FileInfo checkfile = new FileInfo(filenm);
                if (checkfile.Length == 0)
                {
                    throw new CustomException("File " + filenm + " is empty");
                }
                else if (issetting)
                {
                    //check if larger that 32 KB
                    if (checkfile.Length > 32768)
                    {
                        throw new CustomException("Setting file " + filenm + " is too big.");
                    }
                }
                //check if larger that 10 MB
                else if (checkfile.Length > 10485760)
                {
                    throw new CustomException("File " + filenm + " is way too big.");
                }
            }
        }

        /// <summary>
        /// find where the application data folder for this programme is.
        /// </summary>
        private void SetAppdataFolder()
        {
#if win32
            appdatafolder = System.Environment.GetEnvironmentVariable("APPDATA") + "\\."+TrayIcon.AssemblyTitle+"\\";
#elif linux
            appdatafolder = "~\\.NoteFly\\";
#elif mac
            appdatafolder = "???"
#endif
        }

        #endregion Methods
    }
}