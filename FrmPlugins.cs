using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Net;
using System.IO;

namespace NoteFly
{
    public partial class FrmPlugins : Form
    {
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
            this.lblTextNoInternetConnection.Visible = false;
            this.chlbxAvailiblePlugins.Items.Clear();            
            if (this.tabControlPlugins.SelectedTab == this.tabPagePluginsAvailable)
            {
                this.splitContainerAvailablePlugins.Panel2Collapsed = true;
                XmlTextReader xmlreader = null;
                Stream responsestream;
                WebRequest request = this.CreateRequest("http://www.notefly.org/listplugins.xml", System.Net.Cache.RequestCacheLevel.Revalidate);
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
                                string pluginname = null;
                                XmlReader xmlplugin = xmlreader.ReadSubtree();
                                while (xmlplugin.Read())
                                {
                                    switch (xmlplugin.Name)
                                    {
                                        case "name":
                                            pluginname = xmlplugin.ReadElementContentAsString();
                                            break;
                                    }

                                }

                                if (!String.IsNullOrEmpty(pluginname))
                                {
                                    this.chlbxAvailiblePlugins.Items.Add(pluginname, false);
                                }
                            }
                        }
                    }
                }
                catch (WebException webexc)
                {
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

            XmlTextReader xmlreader = null;
            Stream responsestream;
            WebRequest request = this.CreateRequest("http://www.notefly.org/plugindetails.xml", System.Net.Cache.RequestCacheLevel.Revalidate);
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
                                        this.lblPluginVersion.Text = xmlplugin.ReadElementContentAsString();
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
}
