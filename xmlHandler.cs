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
#define win32

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace SimplePlainNote
{
    class xmlHandler
    {
        #region datavelden
        private string filenm;
        //private string appdatafolder;
        private bool issetting;
        private XmlTextReader objXmlTextReader;
        private XmlTextWriter objXmlTextWriter;
        #endregion

        #region constructor
        public xmlHandler(bool issetting)
        {
            this.issetting = issetting;
            if (issetting)
            {                
                string appdatafolder = "";
                #if win32
                appdatafolder = System.Environment.GetEnvironmentVariable("APPDATA") + "\\.simpleplainnote\\";
                #elif linux
                appdatafolder = "~\\.simpleplainnote\\"
                #elif mac
                appdatafolder = "????"
                #endif

                if (Directory.Exists(appdatafolder) == false) { Directory.CreateDirectory(appdatafolder); }
                this.filenm = Path.Combine(appdatafolder, "settings.xml");                
                if (File.Exists(filenm) == false)
                {
                    WriteSettings(true, 95, 0, true, "Verdana", 10, appdatafolder, "adres@domain.com", true, "", "");
                }                                
            }
            else
            {
                throw new Exception("expected true for settings file.");
            }
        }

        public xmlHandler(string filenm)
        {            
            //appdatafolder = System.Environment.GetEnvironmentVariable("APPDATA") + "\\.simpleplainnote\\";
            //if (Directory.Exists(appdatafolder) == false) { Directory.CreateDirectory(appdatafolder); }
            this.filenm = filenm;            
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
        public bool WriteSettings(bool transparecy, decimal transparecylevel, int numcolor, bool askurl, string fontcontent, decimal fontsize, string notesavepath, string defaultemail, bool syntaxhighlight, string twitteruser, string twitterpass)
        {
            if (!this.issetting)
            {
                throw new Exception("not settings file");
            }

            try
            {

                objXmlTextWriter = new XmlTextWriter(filenm, null);
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

                objXmlTextWriter.WriteStartElement("defaultcolor");
                objXmlTextWriter.WriteString(Convert.ToString(numcolor));
                objXmlTextWriter.WriteEndElement();

                objXmlTextWriter.WriteStartElement("askurl");
                if (askurl == true)
                {
                    objXmlTextWriter.WriteString("1");
                }
                else
                {
                    objXmlTextWriter.WriteString("0");
                }
                objXmlTextWriter.WriteEndElement();

                if (String.IsNullOrEmpty(fontcontent)) { throw new Exception("No font"); }

                objXmlTextWriter.WriteStartElement("fontcontent");
                objXmlTextWriter.WriteString(fontcontent);
                objXmlTextWriter.WriteEndElement();

                //if (fontsize == null) { throw new Exception("fontsize not set."); }
                objXmlTextWriter.WriteStartElement("fontsize");
                objXmlTextWriter.WriteString(Convert.ToString(fontsize));
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

        public bool WriteNote(bool visible, bool ontop, string numcolor, string title, string content, int locX, int locY, int notewidth, int noteheight)
        {
            if (issetting) { throw new Exception("This is a settings file, cannot write a note of it."); }
            try
            {
                objXmlTextWriter = new XmlTextWriter(this.filenm, null);

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

                if (ontop== true)
                {
                    objXmlTextWriter.WriteElementString("ontop", "1");
                }
                else
                {
                    objXmlTextWriter.WriteElementString("ontop", "0");
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

        /*
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
                        if (num > 9) { return true; }
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
                    MessageBox.Show("Error seitting file is unusual big. >32kb");
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
         */

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
                return "";
            }
            catch (FileNotFoundException filenotfoundexc)
            {                
                return "";
            }

            if (objXmlTextReader == null)
            {
                //MessageBox.Show("Error: objXmlTextReader is null.");
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
                    catch (Exception)
                    {
                        //todo
                        s = "";
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
        /// Get a xml node
        /// </summary>
        /// <param name="nodename"></param>
        /// <returns>return node as integer, -1 if error</returns>
        public int getXMLnodeAsInt(string nodename)
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
                    catch (InvalidCastException)
                    {
                        return -1;
                    }
                    catch (FormatException)
                    {
                        return -1;                        
                    }
                    finally
                    {
                        objXmlTextReader.Close();
                    }
                }
            }
            objXmlTextReader.Close();
            return -1;
        }

        /// <summary>
        /// get xml node boolean valaue
        /// </summary>
        /// <param name="nodename"></param>
        /// <returns></returns>
        public bool getXMLnodeAsBool(string nodename)
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
                    catch (InvalidCastException castexc)
                    {
                        //todo                        
                        return false;
                    }
                    catch (FormatException formatexc)
                    {
                        //todo
                        return false;
                    }
                    finally
                    {
                        objXmlTextReader.Close();
                    }
                }
            }
            objXmlTextReader.Close();
            return false;
        }
        #endregion
    }
}