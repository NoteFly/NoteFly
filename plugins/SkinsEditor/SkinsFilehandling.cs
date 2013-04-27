//-----------------------------------------------------------------------
// <copyright file="SkinsFilehandling.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2013  Tom
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
namespace SkinsEditor
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Xml;

    public static class SkinsFilehandling
    {

        /// <summary>
        /// Write the skins xml file begin.
        /// </summary>
        /// <param name="skinsfilepath">Full filepath to skins file.</param>
        /// <returns>XmlTextWriter object</returns>
        private static XmlTextWriter WriteSkinFileHeader(string skinsfilepath)
        {
            if (!File.Exists(skinsfilepath))
            {
                new ApplicationException("Skins file not found.");
            }

            XmlTextWriter xmltextwriter = null;
            xmltextwriter = new XmlTextWriter(skinsfilepath, Encoding.UTF8);
            xmltextwriter.Formatting = Formatting.Indented;
            xmltextwriter.WriteStartDocument(true);
            xmltextwriter.WriteStartElement("skins");
            return xmltextwriter;
        }

        /// <summary>
        /// Write the skins xml file end.
        /// </summary>
        /// <param name="xmltextwriter">XmlTextWriter object</param>
        /// <returns>XmlTextWriter object</returns>
        private static XmlTextWriter WriteSkinFileFooter(XmlTextWriter xmltextwriter)
        {
            xmltextwriter.WriteEndElement();
            xmltextwriter.WriteEndDocument();
            return xmltextwriter;
        }

        /// <summary>
        /// Writes the skins file to disk to with all NoteFly skins.
        /// </summary>
        /// <param name="host"></param>
        /// <param name="editskinnr"></param>
        /// <param name="editskin"></param>
        /// <returns>true if writing skins file was succesfull.</returns>
        public static bool WriteSkinsFileEditSkin(IPlugin.IPluginHost host, int editskinnr, Skin editskin)
        {
            XmlTextWriter xmlwriter = null;
            bool succeed = false;
            try
            {
                xmlwriter = WriteSkinFileHeader(host.GetSkinsFile());
                for (int i = 0; i < host.CountSkins; i++)
                {
                    Skin currentskin = null;
                    if (editskinnr == i)
                    {
                        currentskin = editskin;
                    }
                    else
                    {
                        currentskin = SkinFactory.GetSkin(host, i);
                    }

                    WriteSkinsFileSkin(xmlwriter, currentskin);
                }

                xmlwriter = WriteSkinFileFooter(xmlwriter);
                succeed = true;
            }
            finally
            {
                if (xmlwriter != null)
                {
                    xmlwriter.Close();
                }
            }

            return succeed;
        }

        /// <summary>
        /// Write a new skin to NoteFly skins file.
        /// </summary>
        /// <param name="host"></param>
        /// <param name="newskin"></param>
        /// <returns></returns>
        public static bool WriteSkinsFileNewSkin(IPlugin.IPluginHost host, Skin newskin)
        {
            XmlTextWriter xmlwriter = null;
            bool succeed = false;
            try
            {
                xmlwriter = WriteSkinFileHeader(host.GetSkinsFile());
                for (int i = 0; i < host.CountSkins; i++)
                {
                    Skin currentskin = SkinFactory.GetSkin(host, i);
                    WriteSkinsFileSkin(xmlwriter, currentskin);
                }

                WriteSkinsFileSkin(xmlwriter, newskin);
                xmlwriter = WriteSkinFileFooter(xmlwriter);
                succeed = true;
            }
            finally
            {
                if (xmlwriter != null)
                {
                    xmlwriter.Close();
                }
            }

            return succeed;
        }

        /// <summary>
        /// Write skins file without a skin.
        /// </summary>
        /// <param name="host"></param>
        /// <param name="skindeletenr"></param>
        /// <returns></returns>
        public static bool WriteSkinsFileDeleteSkin(IPlugin.IPluginHost host, int skindeletenr)
        {
            XmlTextWriter xmlwriter = null;
            bool succeed = false;
            try
            {
                xmlwriter = WriteSkinFileHeader(host.GetSkinsFile());
                for (int i = 0; i < host.CountSkins; i++)
                {
                    if (skindeletenr != i)
                    {
                        Skin currentskin = SkinFactory.GetSkin(host, i);
                        WriteSkinsFileSkin(xmlwriter, currentskin);
                    }
                }

                xmlwriter = WriteSkinFileFooter(xmlwriter);
                succeed = true;
            }
            finally
            {
                if (xmlwriter != null)
                {
                    xmlwriter.Close();
                }
            }

            return succeed;
        }

        /// <summary>
        /// Write a skin xml element
        /// </summary>
        /// <param name="xmlwriter">The xmlwriter</param>
        /// <param name="currentskin"></param>
        private static void WriteSkinsFileSkin(XmlWriter xmlwriter, Skin currentskin)
        {
            xmlwriter.WriteStartElement("skin");
            xmlwriter.WriteElementString("Name", currentskin.Name);
            xmlwriter.WriteStartElement("PrimaryClr");
            if (!string.IsNullOrEmpty(currentskin.PrimaryTexture))
            {
                xmlwriter.WriteAttributeString("texture", currentskin.PrimaryTexture);
                string texturelayout;
                try
                {
                    texturelayout = Enum.GetName(currentskin.PrimaryTextureLayout.GetType(), currentskin.PrimaryTextureLayout);
                }
                catch (Exception)
                {
                    texturelayout = "tile";
                }
                
                xmlwriter.WriteAttributeString("texturelayout", texturelayout);
            }

            xmlwriter.WriteString(SkinFactory.ClrObjToHtmlHexClr(currentskin.PrimaryClr));
            xmlwriter.WriteEndElement();
            xmlwriter.WriteElementString("SelectClr", SkinFactory.ClrObjToHtmlHexClr(currentskin.SelectClr));
            xmlwriter.WriteElementString("HighlightClr", SkinFactory.ClrObjToHtmlHexClr(currentskin.HighlightClr));
            xmlwriter.WriteElementString("TextClr", SkinFactory.ClrObjToHtmlHexClr(currentskin.TextClr));
            xmlwriter.WriteEndElement();
        }
    }
}
