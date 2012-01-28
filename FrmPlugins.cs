//-----------------------------------------------------------------------
// <copyright file="FrmPlugins.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2010-2012  Tom
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
    using System.Drawing;
using System.ComponentModel;

    public partial class FrmPlugins : Form
    {
        private string currentplugindownloadurl = null;

        /// <summary>
        /// Initialize a new instance of FrmPlugins class.
        /// </summary>
        public FrmPlugins()
        {
            this.DoubleBuffered = Settings.ProgramFormsDoublebuffered;
            InitializeComponent();
            Strings.TranslateForm(this);
            this.pluginGrid.Enabled = Settings.ProgramPluginsAllEnabled;
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
                this.splitContainerAvailablePlugins.Panel2Collapsed = true;
                HttpUtil httputil_allplugins = new HttpUtil("http://ipv4.notefly.org/REST/plugins/list.php", System.Net.Cache.RequestCacheLevel.Default);
                httputil_allplugins.httpthread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(httputil_allplugins_DownloadCompleet);
                if (!httputil_allplugins.Start())
                {
                    MessageBox.Show("error."); //todo
                }
            }
        }  

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        private void httputil_allplugins_DownloadCompleet(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            string response = (string)e.Result;
            this.chlbxAvailiblePlugins.Items.Clear();
            this.chlbxAvailiblePlugins.Enabled = true;
            if (!xmlUtil.ParserListPlugins(response, PluginsManager.GetIPluginVersion(), this.chlbxAvailiblePlugins, this))
            {
                this.lblTextNoInternetConnection.Visible = true;
                this.searchtbPlugins.Enabled = false;
            }
            else
            {
                this.searchtbPlugins.Enabled = true;
            }
        }

        /// <summary>
        /// Parser a string as a version number array with major, minor, release numbers
        /// </summary>
        /// <returns></returns>
        public short[] ParserVersionString(string versionstring)
        {
            short[] versionparts = new short[3];
            char[] splitchr = new char[1];
            splitchr[0] = '.';
            if (!String.IsNullOrEmpty(versionstring))
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
        public bool IsHigherOrSameVersion(short[] version, short[] reqversion)
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
            }

            return higherorsame;
        }

        /// <summary>
        /// A plugin is selected, get the details from the selected plugin.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chlbxAvailiblePlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.splitContainerAvailablePlugins.Panel2Collapsed = false;
            this.btnPluginDownload.Visible = false;
            if (this.chlbxAvailiblePlugins.SelectedIndex >= 0)
            {
                string pluginname = this.chlbxAvailiblePlugins.SelectedItem.ToString();
                if (!String.IsNullOrEmpty(pluginname))
                {
                    HttpUtil httputil_plugindetail = new HttpUtil("http://ipv4.notefly.org/REST/plugins/details.php?name=" + pluginname, System.Net.Cache.RequestCacheLevel.Revalidate);
                    httputil_plugindetail.httpthread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(httputil_plugindetail_DownloadCompleet);
                    if (!httputil_plugindetail.Start())
                    {
                        MessageBox.Show("error.."); // todo error
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        private void httputil_plugindetail_DownloadCompleet(object sender, RunWorkerCompletedEventArgs e)
        {
            string response = (string)e.Result;
            string[] detailsplugin = xmlUtil.ParserDetailsPlugin(response, this.btnPluginDownload);
            if (detailsplugin != null)
            {
                this.lblPluginName.Text = detailsplugin[0];
                this.lblPluginVersion.Text = Strings.T("version: ") + detailsplugin[1];
                this.lblLicense.Text = Strings.T("license: ") + detailsplugin[2];
                this.lblPluginDescription.Text = detailsplugin[3];
                this.currentplugindownloadurl = detailsplugin[4];
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
                string plugins_downloadingplugin = Strings.T("Downloading plugin..");
                FrmDownloader downloader = new FrmDownloader(this.currentplugindownloadurl, false, false, plugins_downloadingplugin);
                downloader.Show();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keywords"></param>
        private void searchtbPlugins_SearchStart(string keywords)
        {
            this.chlbxAvailiblePlugins.Items.Clear();
            HttpUtil httputil_searchplugins = new HttpUtil("http://ipv4.notefly.org/REST/plugins/search.php?keyword=" + System.Web.HttpUtility.UrlEncode(keywords), System.Net.Cache.RequestCacheLevel.Default);
            httputil_searchplugins.httpthread.RunWorkerCompleted +=new System.ComponentModel.RunWorkerCompletedEventHandler(httputil_searchplugins_DownloadCompleet);
            if (!httputil_searchplugins.Start())
            {
                MessageBox.Show("error..."); // todo
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        private void httputil_searchplugins_DownloadCompleet(object sender, RunWorkerCompletedEventArgs e)
        {
            string response = (string)e.Result;
            if (!xmlUtil.ParserListPlugins(response, PluginsManager.GetIPluginVersion(), this.chlbxAvailiblePlugins, this))
            {
                this.searchtbPlugins.Enabled = false;
            } 
        }

        /// <summary>
        /// 
        /// </summary>
        private void searchtbPlugins_SearchStop()
        {
            HttpUtil httputil_allplugins = new HttpUtil("http://ipv4.notefly.org/REST/plugins/list.php", System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
            httputil_allplugins.httpthread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(httputil_allplugins_DownloadCompleet);
            if (!httputil_allplugins.Start())
            {
                MessageBox.Show("error"); // todo
            }
        }

    }
}
