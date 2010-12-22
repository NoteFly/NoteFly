//-----------------------------------------------------------------------
// <copyright file="XmlUtil.cs" company="GNU">
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
#define windows //platform can be: windows, linux, macos

namespace NoteFly
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Globalization;
    using System.Reflection;
    using System.Collections.Generic;

    public static class xmlUtil
    {
        #region Fields (2)

        private static XmlTextReader xmlread = null;
        private static XmlTextWriter xmlwrite = null;
        private const string SETTINGSFILE = "settings.xml";
        private const string SKINFILE = "skins.xml";
        #endregion Fields


        #region Methods (11)

        // Public Methods (8) 

        /// <summary>
        /// Loads the settings file and set the settings in the
        /// static Settings class in memory.
        /// </summary>
        /// <returns>true if file settings exists.</returns>
        public static bool LoadSettings()
        {
            string settingsfilepath = Path.Combine(TrayIcon.AppDataFolder, SETTINGSFILE);
            if (!File.Exists(settingsfilepath))
            {
                return false;
            }
            try
            {
                xmlread = new XmlTextReader(settingsfilepath);
                //if (xmlread.CanReadBinaryContent == true)
                //{
                //    //we dont want that.
                //    throw new CustomException("XmlTextReader should not read binary.");
                //}
                //if (xmlread.Encoding.EncodingName != "UTF-8")
                //{
                //    //wrong encoding 
                //    throw new CustomException("Xml setting file has the wrong encoding. It should be " + System.Text.Encoding.UTF8.BodyName);
                //}
                xmlread.EntityHandling = EntityHandling.ExpandCharEntities;
                xmlread.ProhibitDtd = true;

                while (xmlread.Read())
                {
                    switch (xmlread.Name)
                    {
                        //booleans
                        case "ConfirmDeletenote":
                            Settings.ConfirmDeletenote = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "ConfirmExit":
                            Settings.ConfirmExit = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "ConfirmLinkclick":
                            Settings.ConfirmLinkclick = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "FontTitleStylebold":
                            Settings.FontTitleStylebold = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "HighlightHTML":
                            Settings.HighlightHTML = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "HighlightHyperlinks":
                            Settings.HighlightHyperlinks = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "HighlightPHP":
                            Settings.HighlightPHP = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "HighlightSQL":
                            Settings.HighlightSQL = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "NetworkConnectionForceipv6":
                            Settings.NetworkConnectionForceipv6 = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "NetworkProxyEnabled":
                            Settings.NetworkProxyEnabled = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "NotesClosebtnHidenotepermanently":
                            Settings.NotesClosebtnHidenotepermanently = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "NotesClosebtnTooltipenabled":
                            Settings.NotesClosebtnTooltipenabled = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "NotesTransparencyEnabled":
                            Settings.NotesTransparencyEnabled = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "ProgramFirstrun":
                            Settings.ProgramFirstrun = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "ProgramLogError":
                            Settings.ProgramLogError = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "ProgramLogException":
                            Settings.ProgramLogException = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "ProgramLogInfo":
                            Settings.ProgramLogInfo = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "SocialEmailEnabled":
                            Settings.SocialEmailEnabled = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "SocialFacebookEnabled":
                            Settings.SocialFacebookEnabled = xmlread.ReadElementContentAsBoolean();
                            break;
                        //case "SocialFacebookSavesession":
                        //    Settings.SocialFacebookSavesession = xmlread.ReadElementContentAsBoolean();
                        //    break;
                        case "SocialFacebookUseSSL":
                            Settings.SocialFacebookUseSSL = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "SocialTwitterEnabled":
                            Settings.SocialTwitterEnabled = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "SocialTwitterUseSSL":
                            Settings.SocialTwitterUseSSL = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "TrayiconCreatenotebold":
                            Settings.TrayiconCreatenotebold = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "TrayiconExitbold":
                            Settings.TrayiconExitbold = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "TrayiconManagenotesbold":
                            Settings.TrayiconManagenotesbold = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "TrayiconSettingsbold":
                            Settings.TrayiconSettingsbold = xmlread.ReadElementContentAsBoolean();
                            break;

                        //ints
                        case "FontContentSize":
                            Settings.FontContentSize = xmlread.ReadElementContentAsInt();
                            break;
                        case "FontTextdirection":
                            Settings.FontTextdirection = xmlread.ReadElementContentAsInt();
                            break;
                        case "FontTitleSize":
                            Settings.FontTitleSize = xmlread.ReadElementContentAsInt();
                            break;
                        case "NetworkConnectionTimeout":
                            Settings.NetworkConnectionTimeout = xmlread.ReadElementContentAsInt();
                            break;
                        case "NotesDefaultColor":
                            Settings.NotesDefaultSkinnr = xmlread.ReadElementContentAsInt();
                            break;
                        case "NotesTransparencyLevel":
                            Settings.NotesTransparencyLevel = xmlread.ReadElementContentAsInt(); ;
                            break;
                        case "TrayiconLeftclickaction":
                            Settings.TrayiconLeftclickaction = xmlread.ReadElementContentAsInt();
                            break;
                        case "UpdatecheckEverydays":
                            Settings.UpdatecheckEverydays = xmlread.ReadElementContentAsInt();
                            break;

                        //strings (put at bottom in the settings file for more performance because then there are less characters to compare/skip)
                        case "FontContentFamily":
                            Settings.FontContentFamily = xmlread.ReadElementContentAsString();
                            break;
                        case "FontTitleFamily":
                            Settings.FontTitleFamily = xmlread.ReadElementContentAsString();
                            break;
                        case "NetworkProxyAddress":
                            Settings.NetworkProxyAddress = xmlread.ReadElementContentAsString();
                            break;
                        case "NotesSavepath":
                            Settings.NotesSavepath = xmlread.ReadElementContentAsString();
                            break;
                        case "SocialEmailDefaultadres":
                            Settings.SocialEmailDefaultadres = xmlread.ReadElementContentAsString();
                            break;
                        case "SocialTwitterUsername":
                            Settings.SocialTwitterUsername = xmlread.ReadElementContentAsString();
                            break;
                        case "UpdatecheckLastDate":
                            Settings.UpdatecheckLastDate = xmlread.ReadElementContentAsDateTime();
                            break;
                    }
                }
            }
            finally
            {
                xmlread.Close();
            }
            return true;
        }

        /// <summary>
        /// Get a xml node and return the valuae as string.
        /// </summary>
        /// <param name="nodename"></param>
        /// <returns>return node as string</returns>
        public static string GetContentString(string filename, string nodename)
        {
            try
            {
                xmlread = new XmlTextReader(filename);
            }
            catch (FileLoadException fileloadexc)
            {
                throw new CustomException(fileloadexc.Message);
            }
            catch (FileNotFoundException filenotfoundexc)
            {
                throw new CustomException(filenotfoundexc.Message);
            }

            if (xmlread == null)
            {
                throw new CustomException("XmlTextReader object is null.");
            }
            while (xmlread.Read())
            {
                if (xmlread.Name == nodename)
                {
                    string xmlnodecontent = "";
                    try
                    {
                        xmlnodecontent = xmlread.ReadElementContentAsString();
                    }
                    finally
                    {
                        xmlread.Close();
                    }
                    return xmlnodecontent;
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
        public static bool GetContentBool(string filename, string nodename)
        {
            xmlread = new XmlTextReader(filename);
            try
            {
                while (xmlread.Read())
                {
                    if (xmlread.Name == nodename)
                    {
                        try
                        {
                            bool nodecontent = xmlread.ReadElementContentAsBoolean();
                            xmlread.Close();
                            return nodecontent;
                        }
                        catch (InvalidCastException invalidcastexc)
                        {
                            throw new CustomException(invalidcastexc.Message);
                        }
                        finally
                        {
                            xmlread.Close();
                        }
                    }
                }
            }
            finally
            {
                xmlread.Close();
            }
            //error not found.
            return false;
        }

        /// <summary>
        /// Get a xml node
        /// </summary>
        /// <param name="nodename"></param>
        /// <returns>return node as integer, -1 if error</returns>
        static public int GetContentInt(string filename, string nodename)
        {
            xmlread = new XmlTextReader(filename);
            try
            {
                while (xmlread.Read())
                {
                    if (xmlread.Name == nodename)
                    {
                        try
                        {
                            int nodecontentinteger = xmlread.ReadElementContentAsInt();
                            xmlread.Close();
                            return nodecontentinteger;
                        }
                        catch (InvalidCastException invalidcastexc)
                        {
                            throw new CustomException(invalidcastexc.Message);
                        }
                        finally
                        {
                            xmlread.Close();
                        }
                    }
                }
            }
            finally
            {
                xmlread.Close();
            }
            //error not found
            return -1;
        }

        /// <summary>
        /// Write a note xml file.
        /// </summary>
        /// <param name="visible">note is visible</param>
        /// <param name="ontop">note is on top of other windows</param>
        /// <param name="numcolor">color number of note</param>
        /// <param name="title">the title of the note</param>
        /// <param name="content">the content of the note</param>
        /// <param name="locX">X location of note on screen</param>
        /// <param name="locY">Y location of note on screen</param>
        /// <param name="notewidth">width in pixels of the note</param>
        /// <param name="noteheight">height in pixels of the note</param>
        /// <returns></returns>
        static public bool WriteNote(string filename, Note note, string content)
        {
            xmlwrite = new XmlTextWriter(filename, System.Text.Encoding.UTF8);
            try
            {
                //xmlwrite.Formatting = Formatting.Indented;

                xmlwrite.WriteStartDocument();

                xmlwrite.WriteComment("NoteFly Note 2.x");

                xmlwrite.WriteStartElement("note");

                WriteXMLBool("visible", note.Visible);

                WriteXMLBool("ontop", note.Ontop);

                WriteXMLBool("locked", note.Locked);

                xmlwrite.WriteElementString("skin", note.SkinNr.ToString());

                xmlwrite.WriteElementString("title", note.Title);

                xmlwrite.WriteElementString("content", content);

                xmlwrite.WriteStartElement("location");
                xmlwrite.WriteElementString("x", note.X.ToString());
                xmlwrite.WriteElementString("y", note.Y.ToString());
                xmlwrite.WriteEndElement();

                xmlwrite.WriteStartElement("size");
                xmlwrite.WriteElementString("width", Convert.ToString(note.Width));
                xmlwrite.WriteElementString("heigth", Convert.ToString(note.Height));
                xmlwrite.WriteEndElement();

                xmlwrite.WriteEndElement();

                xmlwrite.WriteEndDocument();
            }
            finally
            {
                xmlwrite.Flush();
                xmlwrite.Close();
            }
            //CheckFile();
            return true;
        }

        /// <summary>
        /// Gets all skins
        /// </summary>
        /// <returns></returns>
        public static List<Skin> LoadSkins()
        {
            string skinfilepath = Path.Combine(TrayIcon.AppDataFolder, SKINFILE);
            if (!File.Exists(skinfilepath)) {
                WriteDefaultSkins(skinfilepath);
            }
            List<Skin> skins = new List<Skin>();
            xmlread = new XmlTextReader(skinfilepath);
            Skin curskin = null;
            UInt16 numskins = 0;
            while (xmlread.Read())
            {
                switch (xmlread.Name)
                {
                    case "skin":
                        if (curskin != null && numskins <255)
                        {
                            skins.Add(curskin);
                        }
                        curskin = new Skin();
                        break;
                    case "Nr":
                        numskins++;
                        curskin.Nr = numskins;
                        break;
                    case "Name":
                        curskin.Name = xmlread.ReadElementContentAsString();
                        break;
                    case "ForegroundColor":
                        curskin.ForegroundClr = ConvToClr(xmlread.ReadElementContentAsString());
                        break;
                    case "BackgroundColor":
                        curskin.BackgroundClr = ConvToClr(xmlread.ReadElementContentAsString());
                        break;
                    case "HighlightColor":
                        curskin.HighlightClr = ConvToClr(xmlread.ReadElementContentAsString());
                        break;
                }
            }
            
            return skins;
        }

        private static System.Drawing.Color ConvToClr(string colorstring)
        {
            //HEX color
            return System.Drawing.ColorTranslator.FromHtml(colorstring);

            //DECIMAL color, commented out in favor of HEX notation for speed.
            //string[] parts = new string[3];
            //parts = colorstring.Split(',');
            //try
            //{
            //    UInt16 redchannel = Convert.ToUInt16(parts[0].Trim());
            //    UInt16 greenchannel = Convert.ToUInt16(parts[1].Trim());
            //    UInt16 bluechannel = Convert.ToUInt16(parts[2].Trim());
            //    return System.Drawing.Color.FromArgb(redchannel, greenchannel, bluechannel);
            //}
            //catch
            //{
            //    if (colorstring.Length < 100)
            //    {
            //        throw new CustomException("Cannot parser: " + colorstring);
            //    }
            //    else
            //    {
            //        throw new CustomException("Cannot parser: " + colorstring.Substring(0, 100)+" ..");
            //    }
            //}
        }


        /// <summary>
        /// Write settings file.
        /// </summary>
        /// <returns>true if succeed.</returns>
        public static bool WriteSettings()
        {
            try
            {
                xmlwrite = new XmlTextWriter(Path.Combine(TrayIcon.AppDataFolder, SETTINGSFILE), System.Text.Encoding.UTF8);
                xmlwrite.Formatting = Formatting.Indented;

                xmlwrite.WriteStartDocument();
                xmlwrite.WriteStartElement("Settings");

                WriteXMLBool("ProgramFirstrun", Settings.ProgramFirstrun);
                WriteXMLBool("ProgramLogInfo", Settings.ProgramLogInfo);
                WriteXMLBool("ProgramLogError", Settings.ProgramLogError);
                WriteXMLBool("ProgramLogException", Settings.ProgramLogException);

                WriteXMLBool("NotesTransparencyEnabled", Settings.NotesTransparencyEnabled);
                xmlwrite.WriteElementString("NotesTransparencyLevel", Convert.ToString(Settings.NotesTransparencyLevel, CultureInfo.InvariantCulture.NumberFormat));
                if (Settings.NotesDefaultSkinnr < 0) { throw new CustomException("unknown defaultcolor."); }
                else
                {
                    xmlwrite.WriteElementString("NotesDefaultColor", Convert.ToString(Settings.NotesDefaultSkinnr, CultureInfo.InvariantCulture.NumberFormat));
                }
                if (!Directory.Exists(Settings.NotesSavepath)) { throw new CustomException("Directory " + Settings.NotesSavepath + " does not exist."); }
                else
                {
                    xmlwrite.WriteElementString("NotesSavepath", Settings.NotesSavepath);
                }

                if ((Settings.TrayiconLeftclickaction < 0) || (Settings.TrayiconLeftclickaction > 3)) { throw new CustomException("action left click unknow"); }
                else
                {
                    xmlwrite.WriteElementString("TrayiconLeftclickaction", Convert.ToString(Settings.TrayiconLeftclickaction));
                }


                if (String.IsNullOrEmpty(Settings.FontContentFamily)) { throw new CustomException("No font"); }
                else
                {
                    xmlwrite.WriteElementString("FontContentFamily", Settings.FontContentFamily);
                }
                xmlwrite.WriteElementString("FontContentSize", Convert.ToString(Settings.FontContentSize));
                if ((Settings.FontTextdirection < 0) || (Settings.FontTextdirection > 2)) { throw new CustomException("Unknown text direction"); }
                else
                {
                    xmlwrite.WriteElementString("FontTextdirection", Convert.ToString(Settings.FontTextdirection));
                }

                xmlwrite.WriteStartElement("Highlight"); //start subtree Highlight

                WriteXMLBool("HighlightHyperlinks", Settings.HighlightHyperlinks);
                WriteXMLBool("highlightHTML", Settings.HighlightHTML);
                WriteXMLBool("HighlightPHP", Settings.HighlightPHP);
                WriteXMLBool("HighlightSQL", Settings.HighlightSQL);

                xmlwrite.WriteEndElement(); //end subtree highlight

                WriteXMLBool("ConfirmDeletenote", Settings.ConfirmDeletenote);
                WriteXMLBool("ConfirmExit", Settings.ConfirmExit);
                WriteXMLBool("ConfirmLinkclick", Settings.ConfirmLinkclick);

                WriteXMLBool("NetworkProxyEnabled", Settings.NetworkProxyEnabled);
                xmlwrite.WriteElementString("NetworkProxyAddress", Settings.NetworkProxyAddress);
                xmlwrite.WriteElementString("NetworkConnectionTimeout", Convert.ToString(Settings.NetworkConnectionTimeout, CultureInfo.InvariantCulture.NumberFormat));

                xmlwrite.WriteElementString("SocialEmailDefaultadres", Settings.SocialEmailDefaultadres);

                xmlwrite.WriteStartElement("facebook"); //start subtree facebook
                //WriteXMLBool("SocialFacebookSavesession", Settings.SocialFacebookSavesession);
                //if (!String.IsNullOrEmpty(Settings.SocialFacebookSessionSecret) &&
                //    !String.IsNullOrEmpty(Settings.SocialFacebookSessionKey) &&
                //    Settings.SocialFacebookSavesession == true)
                //{
                //    xmlwrite.WriteElementString("SocialFacebookSessionExpires", Convert.ToString(Settings.SocialFacebookSessionExpires, CultureInfo.InvariantCulture.NumberFormat));
                //    xmlwrite.WriteElementString("SocialFacebookSessionSecret", Settings.SocialFacebookSessionSecret);
                //    xmlwrite.WriteElementString("SocialFacebookSessionKey", Settings.SocialFacebookSessionKey);
                //}
                //else
                //{
                //    xmlwrite.WriteElementString("SocialFacebookSessionExpires", String.Empty);
                //    xmlwrite.WriteElementString("SocialFacebookSessionSecret", String.Empty);
                //    xmlwrite.WriteElementString("SocialFacebookSessionKey", String.Empty);
                //}
                xmlwrite.WriteEndElement(); //end subtree facebook

                xmlwrite.WriteStartElement("twitter"); //start subtree twitter
                if (Settings.SocialTwitterUsername.Length > 15) { throw new CustomException("Twitter username too long."); }
                else
                {
                    xmlwrite.WriteElementString("SocialTwitterUsername", Settings.SocialTwitterUsername);
                }

                //if ((Settings.SocialTwitterpassword.Length < 6) && (String.IsNullOrEmpty(Settings.SocialTwitterpassword))) { throw new CustomException("Twitter password too short."); }
                //else
                //{
                //    xmlwrite.WriteElementString("SocialTwitterpassword", Settings.SocialTwitterpassword);
                //}
                xmlwrite.WriteEndElement(); //end subtree twitter.


                xmlwrite.WriteEndElement();
                xmlwrite.WriteEndDocument();
            }
            catch (CustomException)
            {
                return false;
            }
            finally
            {
                xmlwrite.Flush();
                xmlwrite.Close();
            }
            //CheckFile();

            return true;
        }

        /// <summary>
        /// Write the default settings.
        /// Used for if SETTINGSFILE is not created yet.
        /// </summary>
        /// <returns></returns>
        public static bool WriteDefaultSettings()
        {
            Settings.ConfirmDeletenote = true;
            Settings.ConfirmExit = false;
            Settings.ConfirmLinkclick = true;
            Settings.FontContentFamily = "Arial";
            Settings.FontContentSize = 11;
            Settings.FontTextdirection = 1;
            Settings.FontTitleFamily = "Arial";
            Settings.FontTitleSize = 14;
            Settings.FontTitleStylebold = true;
            Settings.HighlightHTML = false;
            Settings.HighlightHyperlinks = true;
            Settings.HighlightPHP = false;
            Settings.HighlightSQL = false;
            Settings.NetworkConnectionForceipv6 = false;
            Settings.NetworkConnectionTimeout = 8000;
            Settings.NetworkProxyAddress = "";
            Settings.NetworkProxyEnabled = false;
            Settings.NotesClosebtnHidenotepermanently = true;
            Settings.NotesClosebtnTooltipenabled = false;
            Settings.NotesDefaultSkinnr = 1;
            Settings.NotesSavepath = TrayIcon.AppDataFolder;
            Settings.NotesTransparencyEnabled = true;
            Settings.NotesTransparencyLevel = 90;
            Settings.NotesWarnLimit = 1000;
            Settings.ProgramFirstrun = true;
            Settings.ProgramLogError = true;
            Settings.ProgramLogException = true;
            Settings.ProgramLogInfo = false;
            Settings.SocialEmailDefaultadres = "";
            Settings.SocialEmailEnabled = true;
            Settings.SocialFacebookEnabled = true;
            Settings.SocialFacebookUseSSL = true;
            Settings.SocialTwitterEnabled = true;
            Settings.SocialTwitterUsername = "";
            Settings.SocialTwitterUseSSL = true;
            Settings.TrayiconCreatenotebold = true;
            Settings.TrayiconExitbold = false;
            Settings.TrayiconLeftclickaction = 1;
            Settings.TrayiconManagenotesbold = false;
            Settings.TrayiconSettingsbold = false;
            Settings.UpdatecheckEverydays = 0; //0 is disabled.
            Settings.UpdatecheckLastDate = DateTime.Today;

            try
            {
                WriteSettings();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Writes the default skins to the SKINFILE.
        /// Used for if SKINFILE is not created yet.
        /// </summary>
        /// <param name="filename"></param>
        private static void WriteDefaultSkins(string filename)
        {
            try
            {
                xmlwrite = new XmlTextWriter(filename, System.Text.Encoding.UTF8);
                xmlwrite.Formatting = Formatting.Indented;
                xmlwrite.WriteStartDocument();
                xmlwrite.WriteStartElement("skins");
                xmlwrite.WriteStartElement("skin");
                xmlwrite.WriteElementString("Name", "yellow");
                xmlwrite.WriteElementString("ForegroundColor", "#FFD800");
                xmlwrite.WriteElementString("BackgroundColor", "#E5B61B");
                xmlwrite.WriteElementString("HighlightColor", "#FFE677");
                xmlwrite.WriteEndElement();
                xmlwrite.WriteStartElement("skin");
                xmlwrite.WriteElementString("Name", "orange");
                xmlwrite.WriteElementString("ForegroundColor", "#FF6A00");
                xmlwrite.WriteElementString("BackgroundColor", "#EF6F1F");
                xmlwrite.WriteElementString("HighlightColor", "#FF6247");
                xmlwrite.WriteEndElement();
                xmlwrite.WriteEndElement();
                xmlwrite.WriteEndDocument();
            }
            finally
            {
                xmlwrite.Close();
            }
        }

        /// <summary>
        /// Write xml 1 valaue for true and 0 for false.
        /// </summary>
        /// <param name="checknode"></param>
        private static void WriteXMLBool(String element, bool checknode)
        {
            xmlwrite.WriteStartElement(element);
            if (checknode == true)
            {
                xmlwrite.WriteString("1");
            }
            else
            {
                xmlwrite.WriteString("0");
            }
            xmlwrite.WriteEndElement();
        }

        #endregion Methods
    }
}