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

    static class xmlUtil
    {
        #region Fields (2) 

        static public XmlTextReader xmlread = null;
        static public XmlTextWriter xmlwrite = null;
        const String SETTINGSFILE = "settings.xml";

        #endregion Fields 


		#region Methods (11) 

		// Public Methods (8) 

        /// <summary>
        /// Loads the settings file and set the settings in the
        /// static Settings class in memory.
        /// </summary>
        static public void LoadSettings()
        {
            try
            {
                xmlread = new XmlTextReader(SETTINGSFILE);
                if (xmlread.Encoding != System.Text.Encoding.UTF8)
                {
                    //wrong encoding 
                    throw new CustomException("Xml setting file has the wrong encoding. It should be " + System.Text.Encoding.UTF8.BodyName);
                }
                if (xmlread.CanReadBinaryContent == true)
                {
                    //we dont want that.
                    throw new CustomException("xmlTextReader shouldnt read binary.");
                }
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
                        case "SocialFacebookSavesession":
                            Settings.SocialFacebookSavesession = xmlread.ReadElementContentAsBoolean();
                            break;
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
                        case "UpdatecheckTodaydone":
                            Settings.UpdatecheckTodaydone = xmlread.ReadElementContentAsBoolean();
                            break;
                        case "UpdatescheckEnabled":
                            Settings.UpdatescheckEnabled = xmlread.ReadElementContentAsBoolean();
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
                            Settings.NotesDefaultColor = xmlread.ReadElementContentAsInt();
                            break;
                        case "NotesTransparencyLevel":
                            Settings.NotesTransparencyLevel = xmlread.ReadElementContentAsInt(); ;
                            break;
                        case "TrayiconLeftclickaction":
                            Settings.TrayiconLeftclickaction = xmlread.ReadElementContentAsInt();
                            break;
                        case "UpdatecheckDay":
                            Settings.UpdatecheckDay = xmlread.ReadElementContentAsInt();
                            break;
                        case "UpdatecheckEverydays":
                            Settings.UpdatecheckEverydays = xmlread.ReadElementContentAsInt();
                            break;
                        case "UpdatecheckYear":
                            Settings.UpdatecheckYear = xmlread.ReadElementContentAsInt();
                            break;
                        case "UpdateheckMonth":
                            Settings.UpdateheckMonth = xmlread.ReadElementContentAsInt();
                            break;

                        //strings (put at bottom in the settings file for more performance because then there are less characters to skip)
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
                        case "SocialTwitterpassword":
                            Settings.SocialTwitterpassword = xmlread.ReadElementContentAsString();
                            break;
                        case "SocialTwitterUsername":
                            Settings.SocialTwitterUsername = xmlread.ReadElementContentAsString();
                            break;
                    }
                }
            }
            finally
            {
                xmlread.Close();

            }
        }

        /// <summary>
        /// Get a xml node and return the valuae as string.
        /// </summary>
        /// <param name="nodename"></param>
        /// <returns>return node as string</returns>
        static public String getcontentstring(string filename, string nodename)
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
        static public bool getcontentbool(string filename, string nodename)
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
        static public int getcontentint(string filename, string nodename)
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
        static public bool WriteNote(string filename, Note note)
        {
            xmlwrite = new XmlTextWriter(filename, System.Text.Encoding.UTF8);
            try
            {
                //xmlwrite.Formatting = Formatting.Indented;

                xmlwrite.WriteStartDocument();

                xmlwrite.WriteStartElement("note");

                WriteXMLBool("visible", visible);

                WriteXMLBool("ontop", ontop);

                xmlwrite.WriteElementString("color", Convert.ToString(numcolor, CultureInfo.InvariantCulture.NumberFormat));

                xmlwrite.WriteElementString("title", title);

                xmlwrite.WriteElementString("content", content);

                xmlwrite.WriteStartElement("location");
                xmlwrite.WriteElementString("x", Convert.ToString(locX, CultureInfo.InvariantCulture.NumberFormat));
                xmlwrite.WriteElementString("y", Convert.ToString(locY, CultureInfo.InvariantCulture.NumberFormat));
                xmlwrite.WriteEndElement();

                xmlwrite.WriteStartElement("size");
                xmlwrite.WriteElementString("width", Convert.ToString(notewidth));
                xmlwrite.WriteElementString("heigth", Convert.ToString(noteheight));
                xmlwrite.WriteEndElement();

                xmlwrite.WriteEndElement();

                xmlwrite.WriteEndDocument();
            }
            finally
            {
                xmlwrite.Flush();
                xmlwrite.Close();
            }
            CheckFile();

            return true;
        }

        /// <summary>
        /// Write settings file.
        /// </summary>
        /// <returns>true if succeed.</returns>
        static public bool WriteSettings()
        {
            try
            {
                xmlwrite = new XmlTextWriter(SETTINGFILE, System.Text.Encoding.UTF8);
                xmlwrite.Formatting = Formatting.Indented;

                xmlwrite.WriteStartDocument();
                xmlwrite.WriteStartElement("settings");

                WriteXMLBool("transparecy", Settings.NotesTransparencyEnabled);

                xmlwrite.WriteElementString("transparecylevel", Convert.ToString(Settings.NotesTransparencyLevel, CultureInfo.InvariantCulture.NumberFormat));

                if (Settings.NotesDefaultColor < 0) { throw new CustomException("Impossible selection"); }
                else
                {
                    xmlwrite.WriteElementString("defaultcolor", Convert.ToString(Settings.NotesDefaultColor, CultureInfo.InvariantCulture.NumberFormat));
                }
                if ((Settings.TrayiconLeftclickaction < 0) || (Settings.TrayiconLeftclickaction > 3)) { throw new CustomException("action left click unknow"); }
                else
                {
                    xmlwrite.WriteElementString("actionleftclick", Convert.ToString(Settings.TrayiconLeftclickaction));
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
                else { throw new CustomException("Directory " + notesavepath + " does not exist."); }

                WriteXMLBool("logerror", logerror);

                WriteXMLBool("loginfo", loginfo);

                objXmlTextWriter.WriteStartElement("syntaxhighlight");

                WriteXMLBool("highlightHTML", highlightHTML);

                objXmlTextWriter.WriteEndElement();

                WriteXMLBool("confirmexit", confirmexit);

                WriteXMLBool("confirmdelete", confirmdelete);

                objXmlTextWriter.WriteElementString("defaultemail", defaultemail);

                WriteXMLBool("useproxy", useproxy);

                objXmlTextWriter.WriteElementString("proxyaddr", proxyaddr);

                objXmlTextWriter.WriteElementString("networktimeout", Convert.ToString(timeout, CultureInfo.InvariantCulture.NumberFormat));

                WriteXMLBool("firstrun", firstrun);

                objXmlTextWriter.WriteStartElement("facebook");

                WriteXMLBool("savesession", savefacebooksession);

                if (savefacebooksession && !String.IsNullOrEmpty(FacebookSettings.Uid) && FacebookSettings.Sessionsecret!=null && !String.IsNullOrEmpty(FacebookSettings.Sessionkey) && FacebookSettings.Sesionexpires != 0)
                {
                    objXmlTextWriter.WriteElementString("uid", FacebookSettings.Uid);
                    objXmlTextWriter.WriteElementString("sesionexpires", Convert.ToString(FacebookSettings.Sesionexpires, CultureInfo.InvariantCulture.NumberFormat));
                    if (FacebookSettings.Sessionsecret.Length == 24)
                    {
                        objXmlTextWriter.WriteElementString("sessionsecret", FacebookSettings.Sessionsecret.ToString());
                    }
                    else
                    {
                        objXmlTextWriter.WriteElementString("sessionsecret", String.Empty);
                    }
                    objXmlTextWriter.WriteElementString("sessionkey", FacebookSettings.Sessionkey);
                }
                else
                {
                    objXmlTextWriter.WriteElementString("uid", String.Empty);
                    objXmlTextWriter.WriteElementString("sesionexpires", String.Empty);
                    objXmlTextWriter.WriteElementString("sessionsecret", String.Empty);
                    objXmlTextWriter.WriteElementString("sessionkey", String.Empty);
                }
                objXmlTextWriter.WriteEndElement();

                objXmlTextWriter.WriteStartElement("twitter");
                if (twitteruser.Length > 15) { throw new CustomException("Twitter username too long."); }
                else
                {
                    objXmlTextWriter.WriteElementString("twitteruser", twitteruser);
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
		// Private Methods (3) 

        /// <summary>
        /// Does some checks on the file
        /// - Is the file empty?
        /// - Is the file too large?
        /// </summary>
        static private void CheckFile(string filenm)
        {
            if (File.Exists(filenm) == true)
            {
                FileInfo checkfile = new FileInfo(filenm);
#if windows
                if (checkfile.Attributes == FileAttributes.System)
                {
                    throw new CustomException("File " + filenm + " is a system file.");
                }
                else if (checkfile.Attributes == FileAttributes.ReadOnly)
                {
                    Log.Write(LogType.error, filenm + " is readonly and should not be readonly.");
                }
#endif
                if (checkfile.Length == 0)
                {
                    throw new CustomException("File " + filenm + " is empty");
                }
                //check if larger that 10 MB
                else if (checkfile.Length > 10485760)
                {
                    throw new CustomException("File " + filenm + " is way too big.");
                }

            }
        }

        /// <summary>
        /// write 1 for true and 0 for false.
        /// </summary>
        /// <param name="checknode"></param>
        static private void WriteXMLBool(String element, bool checknode)
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