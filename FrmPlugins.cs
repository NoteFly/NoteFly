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
    using System.ComponentModel;
    using System.Windows.Forms;

    /// <summary>
    /// FrmPlugins window
    /// </summary>
    public partial class FrmPlugins : Form
    {
        private const string RESTAPIPLUGINSLIST = "http://update.notefly.org/REST/plugins/list.php";
        private const string RESTAPIPLUGINDETAILS = "http://update.notefly.org/REST/plugins/details.php?name=";
        private const string RESTAPIPLUGINSSEARCH = "http://update.notefly.org/REST/plugins/search.php?keyword=";

        /// <summary>
        /// 
        /// </summary>
        private string currentplugindownloadurl = null;

        /// <summary>
        /// Refence to FrmDownload window.
        /// </summary>
        private FrmDownloader frmdownloader;

        /// <summary>
        /// Initialize a new instance of FrmPlugins class.
        /// </summary>
        public FrmPlugins()
        {
            this.DoubleBuffered = Settings.ProgramFormsDoublebuffered;
            this.InitializeComponent();
            this.SetFormTitle();
            Strings.TranslateForm(this);
            this.pluginGrid.Enabled = Settings.ProgramPluginsAllEnabled;
        }

        /// <summary>
        /// Set the title of this form.
        /// </summary>
        private void SetFormTitle()
        {
            this.Text = Strings.T("Plugins") + " - " + Program.AssemblyTitle;            
        }

        /// <summary>
        /// Check if tabPagePluginsAvailable is selected, 
        /// if it is then get a list with available all plugins.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControlPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControlPlugins.SelectedTab == this.tabPagePluginsAvailable)
            {
                this.lblTextNoInternetConnection.Visible = false;
                this.splitContainerAvailablePlugins.Panel2Collapsed = true;
                HttpUtil httputil_allplugins = new HttpUtil(RESTAPIPLUGINSLIST, System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                if (!httputil_allplugins.Start(new System.ComponentModel.RunWorkerCompletedEventHandler(this.httputil_allplugins_DownloadCompleet)))
                {
                    this.lblTextNoInternetConnection.Visible = true;
                    //Log.Write(LogType.exception, "error request, by tabControlPlugins_SelectedIndexChanged.");
                }
                else
                {
                    this.lblTextNoInternetConnection.Visible = false;
                }
            }
        }  

        /// <summary>
        /// Downloading of a list of allplugins is compleet, parser results and display items in chlbxAvailiblePlugins.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void httputil_allplugins_DownloadCompleet(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            string response = (string)e.Result;
            this.lbxAvailablePlugins.Items.Clear();
            this.lbxAvailablePlugins.Enabled = true;
            if (!xmlUtil.ParserListPlugins(response, PluginsManager.GetIPluginVersion(), this.lbxAvailablePlugins))
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
        /// A plugin is selected, get the details from the selected plugin.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbxAvailablePlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.splitContainerAvailablePlugins.Panel2Collapsed = false;
            this.btnPluginDownload.Visible = false;
            if (this.lbxAvailablePlugins.SelectedIndex >= 0)
            {
                string pluginname = this.lbxAvailablePlugins.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(pluginname))
                {
                    HttpUtil httputil_plugindetail = new HttpUtil(RESTAPIPLUGINDETAILS + System.Web.HttpUtility.UrlEncode(pluginname), System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
                    if (!httputil_plugindetail.Start(new RunWorkerCompletedEventHandler(this.httputil_plugindetail_DownloadCompleet)))
                    {
                        this.lblTextNoInternetConnection.Visible = true;
                    }
                    else
                    {
                        this.lblTextNoInternetConnection.Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// Downloading of plugin details data is compleet, parser data and display plugin details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            if (!string.IsNullOrEmpty(this.currentplugindownloadurl))
            {
                this.frmdownloader = new FrmDownloader(Strings.T("Downloading plugin.."));
                this.frmdownloader.AllDownloadsCompleted += new FrmDownloader.DownloadCompleetHandler(this.downloader_DownloadCompleet);
                this.frmdownloader.Show();
                this.frmdownloader.BeginDownload(this.currentplugindownloadurl, Settings.ProgramPluginsFolder);                
            }
        }

        /// <summary>
        /// Downloading of plugin(s) compleet.
        /// </summary>
        /// <param name="newfiles"></param>
        private void downloader_DownloadCompleet(string[] newfiles)
        {
            this.pluginGrid.DrawAllPluginsDetails(PluginGrid.DEFAULTWITH);            
        }

        /// <summary>
        /// The user request to search, make request for search results.
        /// </summary>
        /// <param name="keywords"></param>
        private void searchtbPlugins_SearchStart(string keywords)
        {
            this.lbxAvailablePlugins.Items.Clear();
            HttpUtil httputil_searchplugins = new HttpUtil(RESTAPIPLUGINSSEARCH + System.Web.HttpUtility.UrlEncode(keywords), System.Net.Cache.RequestCacheLevel.Default);
            if (!httputil_searchplugins.Start(new System.ComponentModel.RunWorkerCompletedEventHandler(this.httputil_searchplugins_DownloadCompleet)))
            {
                this.lblTextNoInternetConnection.Visible = true;
            }
            else
            {
                this.lblTextNoInternetConnection.Visible = false;
            }
        }

        /// <summary>
        /// Downloading of search results data is compleet, parser searchresult data and list search results
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void httputil_searchplugins_DownloadCompleet(object sender, RunWorkerCompletedEventArgs e)
        {
            string response = (string)e.Result;
            this.lbxAvailablePlugins.BeginUpdate();
            if (!xmlUtil.ParserListPlugins(response, PluginsManager.GetIPluginVersion(), this.lbxAvailablePlugins))
            {
                this.searchtbPlugins.Enabled = false;
            }

            this.lbxAvailablePlugins.EndUpdate();
        }

        /// <summary>
        /// The user wants to sto searching, get the list of all plugins again.
        /// </summary>
        private void searchtbPlugins_SearchStop()
        {
            HttpUtil httputil_allplugins = new HttpUtil(RESTAPIPLUGINSLIST, System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
            if (!httputil_allplugins.Start(new RunWorkerCompletedEventHandler(this.httputil_allplugins_DownloadCompleet)))
            {
                this.lblTextNoInternetConnection.Visible = true;
            }
            else
            {
                this.lblTextNoInternetConnection.Visible = false;
            }
        }
    }
}
