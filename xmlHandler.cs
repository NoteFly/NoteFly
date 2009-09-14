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
                    WriteSettings(true, 95, 0, true, "Verdana", 10, appdatafolder, "adres@domain.com", true, "", "");
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

        #region Methods (6)

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
                System.Windows.Forms.MessageBox.Show("error: " + fileloadexc.Message);
                return "";
            }
            catch (FileNotFoundException filenotfoundexc)
            {
                System.Windows.Forms.MessageBox.Show("error: " + filenotfoundexc.Message);
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
                    catch (InvalidCastException invalidcastexc)
                    {
                        System.Windows.Forms.MessageBox.Show("Error: " + invalidcastexc.Message);
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
                    catch (InvalidCastException invalidcastexc)
                    {
                        System.Windows.Forms.MessageBox.Show("error: " + invalidcastexc.Message);
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
                    catch (InvalidCastException invalidcastexc)
                    {
                        System.Windows.Forms.MessageBox.Show("error: " + invalidcastexc.Message);
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

        public bool WriteNote(bool visible, bool ontop, string numcolor, string title, string content, int locX, int locY, int notewidth, int noteheight)
        {
            if (issetting) { throw new Exception("This is a settings file, cannot write a note of it."); }
            
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

            if (ontop == true)
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

            CheckFile();

            return true;
        }

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

            CheckFile();

            return true;
        }
        // Private Methods (1) 

        /// <summary>
        /// find where the application data folder for this programme is.
        /// </summary>
        private void SetAppdataFolder()
        {
#if win32
            appdatafolder = System.Environment.GetEnvironmentVariable("APPDATA") + "\\.simpleplainnote\\";
#elif linux
            appdatafolder = "~\\.simpleplainnote\\"
#elif mac
            appdatafolder = "????"
#endif
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
                    throw new Exception("File "+filenm+" is empty");                   
                }
                else if (issetting)
                {
                    //check if larger that 32 KB
                    if (checkfile.Length > 32768)
                    {
                        throw new Exception("Setting file " + filenm + " is too big.");
                    }
                }
                //check if larger that 10 MB
                else if (checkfile.Length > 10485760)
                {
                    throw new Exception("File " + filenm + " is way too big.");
                }
            }
        }

        #endregion Methods
    }
}