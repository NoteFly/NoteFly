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
        private string appdatafolder;
        private XmlTextReader objXmlTextReader;
        private XmlTextWriter objXmlTextWriter;        
        #endregion

        #region constructor
        public xmlHandler(bool issettings, string filenm)
        {
            this.filenm = filenm;
            appdatafolder = System.Environment.GetEnvironmentVariable("APPDATA") + "\\.simpleplainnote\\";
            if (Directory.Exists(appdatafolder) == false) { Directory.CreateDirectory(appdatafolder); }

            if (issettings == true)
            {
                if (File.Exists(appdatafolder+filenm) == false)
                {
                    WriteSettings(true, 95, 0);
                }                
                //validate setting xmlfile.
                //clsSValidator objclsSValidator = new clsSValidator(settingsfile, Application.StartupPath + @"\settings.xsd");
                //if (objclsSValidator.ValidateXMLFile()) return;
            }            
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
        public bool WriteSettings(bool transparecy, decimal transparecylevel, int numcolor)
        {
            try
            {                
                objXmlTextWriter = new XmlTextWriter(appdatafolder + filenm, null);
                objXmlTextWriter.Formatting = Formatting.Indented;

                objXmlTextWriter.WriteStartDocument();
                objXmlTextWriter.WriteStartElement("settings");
                if (transparecy == true)
                {
                    objXmlTextWriter.WriteStartElement("transparecy");
                    objXmlTextWriter.WriteString("1");
                    objXmlTextWriter.WriteEndElement();
                }
                else
                {
                    objXmlTextWriter.WriteStartElement("transparecy");
                    objXmlTextWriter.WriteString("0");
                    objXmlTextWriter.WriteEndElement();
                }
                objXmlTextWriter.WriteStartElement("transparecylevel");
                objXmlTextWriter.WriteString(Convert.ToString(transparecylevel));
                objXmlTextWriter.WriteEndElement();

                if (numcolor < 0) { throw new Exception("Impossible selection"); }

                objXmlTextWriter.WriteStartElement("defaultcolor");
                objXmlTextWriter.WriteString(Convert.ToString(numcolor));
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

        public bool WriteNote(string numcolor, string title, string content)
        {
            try {
                objXmlTextWriter = new XmlTextWriter(appdatafolder+"notes\\"+filenm, null);

                objXmlTextWriter.Formatting = Formatting.Indented;

                objXmlTextWriter.WriteStartDocument();
                
                    objXmlTextWriter.WriteStartElement("note");

                        objXmlTextWriter.WriteStartElement("color");
                            objXmlTextWriter.WriteString(numcolor);
                        objXmlTextWriter.WriteEndElement();

                        objXmlTextWriter.WriteStartElement("title");
                            objXmlTextWriter.WriteString(title);
                        objXmlTextWriter.WriteEndElement();

                        objXmlTextWriter.WriteStartElement("content");
                            objXmlTextWriter.WriteString(content);
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
        /// Get a xml node
        /// </summary>
        /// <param name="nodename"></param>
        /// <returns>return node as string</returns>
        public String getXMLnode(string nodename)
        {
            objXmlTextReader = new XmlTextReader(appdatafolder + filenm);

            while (objXmlTextReader.Read())
            {
                if (objXmlTextReader.Name == nodename)
                {
                    string s = objXmlTextReader.ReadElementContentAsString();
                    objXmlTextReader.Close();
                    return s;
                }
            }
            //error
            return null;
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
                    catch (Exception)
                    {
                        objXmlTextReader.Close();
                        return -1;
                    }
                }
            }
            objXmlTextReader.Close();
            return -1;
        }       

        #endregion

    }
}
