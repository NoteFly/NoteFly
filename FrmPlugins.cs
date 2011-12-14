//-----------------------------------------------------------------------
// <copyright file="FrmPlugins.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2010-2011  Tom
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
namespace NoteFly
{
    using System;
    using System.Windows.Forms;
    using System.Xml;
    using System.Net;
    using System.IO;

    public partial class FrmPlugins : Form
    {
        private string currentplugindownloadurl = null;

        /// <summary>
        /// 
        /// </summary>
        public FrmPlugins()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControlPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (this.tabControlPlugins.SelectedTab == this.tabPagePluginsAvailable)
            {
                this.lblTextNoInternetConnection.Visible = false;
                this.chlbxAvailiblePlugins.Items.Clear();
                this.splitContainerAvailablePlugins.Panel2Collapsed = true;
                XmlTextReader xmlreader = null;
                Stream responsestream;
                WebRequest request = this.CreateRequest("http://www.notefly.org/REST/plugins/list.php", System.Net.Cache.RequestCacheLevel.Default);
                try
                {
                    using (WebResponse response = request.GetResponse())
                    {
                        responsestream = response.GetResponseStream();
                        xmlreader = new XmlTextReader(responsestream);
                        xmlreader.ProhibitDtd = true;
                        System.Reflection.Assembly ipluginasm = System.Reflection.Assembly.ReflectionOnlyLoadFrom(Path.Combine(Program.InstallFolder, "IPlugin.dll"));
                        string versionipluginstring = ipluginasm.GetName().Version.Major + "." + ipluginasm.GetName().Version.Minor + "." + ipluginasm.GetName().Version.Build;
                        ipluginasm = null;

                        short[] ipluginversionparts = this.ParserVersionString(versionipluginstring);
                        while (xmlreader.Read())
                        {
                            if (xmlreader.Name == "plugin")
                            {
                                string pluginname = null;
                                string curpluginminversioniplugin = null;
                                XmlReader xmlplugin = xmlreader.ReadSubtree();
                                while (xmlplugin.Read())
                                {
                                    switch (xmlplugin.Name)
                                    {
                                        case "name":
                                            pluginname = xmlplugin.ReadElementContentAsString();
                                            break;
                                        case "minversioniplugin":
                                            curpluginminversioniplugin = xmlplugin.ReadElementContentAsString();
                                            break;
                                    }
                                }

                                short[] curpluginminveripluginpart = this.ParserVersionString(curpluginminversioniplugin);
                                bool workswithapp = IsHigherOrSameVersion(curpluginminveripluginpart, ipluginversionparts);
                                if (!String.IsNullOrEmpty(pluginname) && workswithapp)
                                {
                                    this.chlbxAvailiblePlugins.Items.Add(pluginname, false);
                                }
                            }
                        }
                    }
                }
                catch (WebException webexc)
                {
                    this.lblTextNoInternetConnection.Text = webexc.Message;
                    this.lblTextNoInternetConnection.Visible = true;
                    Log.Write(LogType.error, webexc.Message);
                }
                finally
                {
                    if (xmlreader != null)
                    {
                        xmlreader.Close();
                    }
                }

            }
        }

        /// <summary>
        /// Parser a string as a version number array with major, minor, release numbers
        /// </summary>
        /// <returns></returns>
        private short[] ParserVersionString(string versionstring)
        {
            short[] versionparts = new short[3];
            char[] splitchr = new char[1];
            splitchr[0] = '.';
            if (versionstring != null)
            {
                string[] stringversionparts = versionstring.Split(splitchr, StringSplitOptions.None);
                try
                {
                    for (int i = 0; i < versionparts.Length; i++)
                    {
                        versionparts[i] = Convert.ToInt16(stringversionparts[i]);
                    }
                }
                catch (InvalidCastException invcastexc)
                {
                    Log.Write(LogType.exception, invcastexc.Message);
                }
            }
            else
            {
                Log.Write(LogType.exception, "No versionstring to parser.");
            }
            return versionparts;
        }

        /// <summary>
        /// Find out if the version numbers given as array is higher
        /// than the required version numbers given as array.
        /// </summary>
        /// <param name="version"></param>
        /// <param name="reqversion"></param>
        /// <returns></returns>
        private bool IsHigherOrSameVersion(short[] version, short[] reqversion)
        {
            bool higherorsame = true;
            bool continu = true;
            for (int i = 0; (i < reqversion.Length && continu); i++)
            {
                if (reqversion[i] != version[i])
                {
                    continu = false;
                    if (version[i] < reqversion[i])
                    {
                        higherorsame = true;
                    }
                    else if (version[i] > reqversion[i])
                    {
                        higherorsame = false;
                    }

                    break;
                }
                //else
                //{
                //continu = true;
                //}
            }

            return higherorsame;
        }

        /// <summary>
        /// Create webrequest.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cachesettings"></param>
        /// <returns></returns>
        private WebRequest CreateRequest(string url, System.Net.Cache.RequestCacheLevel cachesettings)
        {
            HttpWebRequest request = null;
            System.Net.ServicePointManager.Expect100Continue = false;
            System.Net.ServicePointManager.DefaultConnectionLimit = 2;
            if (Settings.NetworkConnectionForceipv6)
            {
                // use dns ipv6 AAAA record.
                url = url.Replace("//update.", "//ipv6."); // not replacing "http", "https", "ftp"
                url = url.Replace("//www.", "//ipv6.");
            }

            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                Log.Write(LogType.error, "Invalid url.");
                return null;
            }

            try
            {
                request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "text/xml";
                request.UserAgent = Program.AssemblyTitle + " " + Program.AssemblyVersionAsString;
                request.Timeout = Settings.NetworkConnectionTimeout;
                if (Settings.NetworkProxyEnabled && !string.IsNullOrEmpty(Settings.NetworkProxyAddress))
                {
                    request.Proxy = new WebProxy(Settings.NetworkProxyAddress);
                }

                request.Headers["Accept-Encoding"] = "gzip";
                request.CachePolicy = new System.Net.Cache.RequestCachePolicy(cachesettings);
                request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.None;
            }
            catch (System.Net.WebException webexc)
            {
                Log.Write(LogType.exception, webexc.Message);
            }

            return request;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chlbxAvailiblePlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.splitContainerAvailablePlugins.Panel2Collapsed = false;
            this.btnPluginDownload.Visible = false;
            XmlTextReader xmlreader = null;
            Stream responsestream;
            string pluginname = this.chlbxAvailiblePlugins.SelectedItem.ToString();
            if (!String.IsNullOrEmpty(pluginname))
            {
                WebRequest request = this.CreateRequest("http://www.notefly.org/REST/plugins/details.php?name=" + pluginname, System.Net.Cache.RequestCacheLevel.Revalidate);
                try
                {
                    using (WebResponse response = request.GetResponse())
                    {
                        responsestream = response.GetResponseStream();
                        xmlreader = new XmlTextReader(responsestream);
                        xmlreader.ProhibitDtd = true;
                        while (xmlreader.Read())
                        {
                            if (xmlreader.Name == "plugin")
                            {
                                XmlReader xmlplugin = xmlreader.ReadSubtree();
                                while (xmlplugin.Read())
                                {
                                    switch (xmlplugin.Name)
                                    {
                                        case "name":
                                            this.lblPluginName.Text = xmlplugin.ReadElementContentAsString();
                                            break;
                                        case "version":
                                            this.lblPluginVersion.Text = "version: " + xmlplugin.ReadElementContentAsString();
                                            break;
                                        case "license":
                                            this.lblLicense.Text = "license: " + xmlplugin.ReadElementContentAsString();
                                            break;
                                        case "downloadurl":
                                            this.currentplugindownloadurl = xmlplugin.ReadElementContentAsString();
                                            if (!String.IsNullOrEmpty(this.currentplugindownloadurl))
                                            {
                                                this.btnPluginDownload.Visible = true;
                                            }

                                            break;
                                        case "description":
                                            this.lblPluginDescription.Text = xmlplugin.ReadElementContentAsString();
                                            break;
                                    }

                                }
                            }
                        }
                    }
                }
                finally
                {
                    if (xmlreader != null)
                    {
                        xmlreader.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Start download of plugin.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPluginDownload_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.currentplugindownloadurl))
            {
                FrmDownloader downloader = new FrmDownloader(this.currentplugindownloadurl, false, false, "Downloading plugin..");
                downloader.Show();
            }
        }
    }
}
