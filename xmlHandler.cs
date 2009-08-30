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
                WriteSettings(true, 95, 0, "Verdana", appdatafolder, "adres@domain.com", true, "", "");
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

                if (String.IsNullOrEmpty(fontcontent)) { throw new Exception("No font"); }

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
        #endregion
    }
}