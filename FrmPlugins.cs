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
    using System.IO;

    /// <summary>
    /// FrmPlugins window
    /// </summary>
    public partial class FrmPlugins : Form
    {
        /// <summary>
        /// REST url where to get a list of plugins.
        /// </summary>
        private const string RESTAPIPLUGINSLIST = "http://update.notefly.org/REST/plugins/list.php";

        /// <summary>
        /// REST url where to get the detail of a partialer plugin 
        /// </summary>
        private const string RESTAPIPLUGINDETAILS = "http://update.notefly.org/REST/plugins/details.php?name=";

        /// <summary>
        /// REST url where to get a list of matching pluginnames searched by a partialer keyword.
        /// </summary>
        private const string RESTAPIPLUGINSSEARCH = "http://update.notefly.org/REST/plugins/search.php?keyword=";

        /// <summary>
        /// The url to download plugin of the current plugin being selected.
        /// </summary>
        private string currentplugindownloadurl = null;

        /// <summary>
        /// 
        /// </summary>
        private RSAVerify rsaverif;

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
            this.pluginGrid.DrawAllPluginsDetails(this.tabPagePluginsInstalled.ClientRectangle.Width);
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
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void tabControlPlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControlPlugins.SelectedTab == this.tabPagePluginsAvailable)
            {
                this.lblTextNoInternetConnection.Visible = false;
                this.splitContainerAvailablePlugins.Panel2Collapsed = true;
                HttpUtil httputil_allplugins = new HttpUtil(RESTAPIPLUGINSLIST, System.Net.Cache.RequestCacheLevel.Revalidate);
                if (!httputil_allplugins.Start(new System.ComponentModel.RunWorkerCompletedEventHandler(this.httputil_allplugins_DownloadCompleet)))
                {
                    this.SetAvailablePluginsNetwork(false);
                }
                else
                {
                    this.SetAvailablePluginsNetwork(true);
                }
            }
        }

        /// <summary>
        /// Downloading of a list of allplugins is compleet, parser results and display items in chlbxAvailiblePlugins.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">RunWorkerCompletedEvent Arguments</param>
        private void httputil_allplugins_DownloadCompleet(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            string response = (string)e.Result;
            this.lbxAvailablePlugins.Items.Clear();
            this.lbxAvailablePlugins.Enabled = true;
            if (!xmlUtil.ParserListPlugins(response, PluginsManager.GetIPluginVersion(), this.lbxAvailablePlugins))
            {
                this.SetAvailablePluginsNetwork(false);
            }
            else
            {
                this.SetAvailablePluginsNetwork(true);
            }
        }

        /// <summary>
        /// A plugin is selected, get the details from the selected plugin.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void lbxAvailablePlugins_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.splitContainerAvailablePlugins.Panel2Collapsed = false;
            this.btnPluginDownload.Visible = false;
            if (this.lbxAvailablePlugins.SelectedIndex >= 0)
            {
                string pluginname = this.lbxAvailablePlugins.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(pluginname))
                {
                    HttpUtil httputil_plugindetail = new HttpUtil(RESTAPIPLUGINDETAILS + System.Web.HttpUtility.UrlEncode(pluginname), System.Net.Cache.RequestCacheLevel.Revalidate);
                    this.ClearPluginDetails();
                    if (!httputil_plugindetail.Start(new RunWorkerCompletedEventHandler(this.httputil_plugindetail_DownloadCompleet)))
                    {
                        this.SetAvailablePluginsNetwork(false);
                    }
                    else
                    {
                        this.SetAvailablePluginsNetwork(true);
                    }
                }
            }
        }

        /// <summary>
        /// Hide all plugins details
        /// </summary>
        private void ClearPluginDetails()
        {
            this.lblPluginName.ResetText();
            this.lblLicense.ResetText();
            this.lblPluginVersion.ResetText();
            this.lblPluginDescription.ResetText();
        }

        /// <summary>
        /// Downloading of plugin details data is compleet, parser data and display plugin details
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">RunWorkerCompleted event arguments</param>
        private void httputil_plugindetail_DownloadCompleet(object sender, RunWorkerCompletedEventArgs e)
        {
            string response = (string)e.Result;
            bool alreadyinstalled = false;
            bool updateavailable = false;
            string[] detailsplugin = xmlUtil.ParserDetailsPlugin(response, PluginsManager.GetInstalledPlugins(), out alreadyinstalled, out updateavailable);
            if (detailsplugin != null)
            {
                if (alreadyinstalled)
                {
                    this.btnPluginDownload.Enabled = false;
                    if (updateavailable)
                    {
                        this.btnPluginDownload.Text = Strings.T("update");
                        this.btnPluginDownload.Enabled = true;
                    }
                    else
                    {
                        this.btnPluginDownload.Text = Strings.T("already installed");
                    }
                }
                else
                {
                    this.btnPluginDownload.Enabled = true;
                    this.btnPluginDownload.Text = Strings.T("download");
                }

                if (!string.IsNullOrEmpty(detailsplugin[4]) && Uri.IsWellFormedUriString(detailsplugin[4], UriKind.Absolute))
                {
                    this.btnPluginDownload.Visible = true;
                }

                this.lblPluginName.Text = detailsplugin[0];
                this.lblPluginVersion.Text = Strings.T("version: ") + detailsplugin[1];
                this.lblLicense.Text = Strings.T("license: ") + detailsplugin[2];
                this.lblPluginDescription.Text = detailsplugin[3];
                this.currentplugindownloadurl = detailsplugin[4];

                this.rsaverif = new RSAVerify(detailsplugin[5]);
            }
        }

        /// <summary>
        /// Start download of plugin.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
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
        /// <param name="newfiles">Array of files downloads.</param>
        private void downloader_DownloadCompleet(string[] newfiles)
        {
            if (this.rsaverif != null)
            {
                if (!this.rsaverif.CheckFileSignatureAndDisplayErrors(newfiles[0]))
                {
                    //File.Move(newfiles[0], newfiles[0] + ".invalid");
                    return;
                }
            }

            if (this.frmdownloader.GetFileCompressedkind(newfiles[0]) == 1)
            {
                string[] unzipextensions = new string[1] { ".dll" };
                if (frmdownloader.DecompressZipFile(newfiles[0], unzipextensions))
                {
                    // decompress succesfully, now delete zip file
                    this.DeleteNotsysFile(newfiles[0], "Delete zip archive: ");
                }
            }
            else if (this.frmdownloader.GetFileCompressedkind(newfiles[0]) == 2)
            {
                if (this.frmdownloader.DecompressGZipFile(newfiles[0]))
                {
                    // decompress succesfully, now delete gzip file
                    this.DeleteNotsysFile(newfiles[0], "Delete GZip file: ");
                }
            }

            PluginsManager.LoadPlugins();
            this.pluginGrid.DrawAllPluginsDetails(this.tabPagePluginsInstalled.ClientRectangle.Width);
            this.tabControlPlugins.SelectedIndex = 0;
        }

        /// <summary>
        /// Check if file is not a system file, and if it's not then delete the file.
        /// </summary>
        /// <param name="file">The file to delete</param>
        /// <param name="logdesc">log mesage</param>
        private void DeleteNotsysFile(string file, string logdesc)
        {
            if (System.IO.File.GetAttributes(file) != System.IO.FileAttributes.System)
            {
                try
                {
                    File.Delete(file);
                    Log.Write(LogType.info, logdesc + file);
                }
                catch (ArgumentException argexc)
                {
                    Log.Write(LogType.exception, argexc.Message);
                }
                catch (IOException ioexc)
                {
                    Log.Write(LogType.exception, ioexc.Message);
                }
            }
        }

        /// <summary>
        /// The user request to search, make request for search results.
        /// </summary>
        /// <param name="keywords">The keywords</param>
        private void searchtbPlugins_SearchStart(string keywords)
        {
            this.lbxAvailablePlugins.Items.Clear();
            HttpUtil httputil_searchplugins = new HttpUtil(RESTAPIPLUGINSSEARCH + System.Web.HttpUtility.UrlEncode(keywords), System.Net.Cache.RequestCacheLevel.Revalidate);
            this.ClearPluginDetails();
            this.splitContainerAvailablePlugins.Panel2Collapsed = true;
            if (!httputil_searchplugins.Start(new System.ComponentModel.RunWorkerCompletedEventHandler(this.httputil_searchplugins_DownloadCompleet)))
            {
                this.SetAvailablePluginsNetwork(false);
            }
            else
            {
                this.SetAvailablePluginsNetwork(true);
            }
        }

        /// <summary>
        /// Downloading of search results data is compleet, parser searchresult data and list search results
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">RunWorkerCompleted Event Arguments</param>
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
            HttpUtil httputil_allplugins = new HttpUtil(RESTAPIPLUGINSLIST, System.Net.Cache.RequestCacheLevel.Revalidate);
            if (!httputil_allplugins.Start(new RunWorkerCompletedEventHandler(this.httputil_allplugins_DownloadCompleet)))
            {
                this.SetAvailablePluginsNetwork(false);
            }
            else
            {
                this.SetAvailablePluginsNetwork(true);
            }
        }

        /// <summary>
        /// Set a message if network connection failed.
        /// </summary>
        /// <param name="isconnected"></param>
        private void SetAvailablePluginsNetwork(bool isconnected)
        {
            this.lblTextNoInternetConnection.Visible = !isconnected;
            this.searchtbPlugins.Enabled = isconnected;
            if (!isconnected)
            {
                string nonetwork = Strings.T("Could not load list with plugins. Internet connection failed.");
                string proxyused = Strings.T("A proxy is being used.");
                if (Settings.NetworkProxyEnabled)
                {
                    this.lblTextNoInternetConnection.Text = nonetwork + Environment.NewLine + proxyused;
                }
                else
                {
                    this.lblTextNoInternetConnection.Text = nonetwork;
                }

                this.lbxAvailablePlugins.Items.Clear();
                Log.Write(LogType.info, "No network available.");
            }
        }
    }
}
