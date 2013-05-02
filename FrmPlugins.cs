//-----------------------------------------------------------------------
// <copyright file="FrmPlugins.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2010-2013  Tom
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
    using System.IO;
    using System.Windows.Forms;
    using System.Threading;
    using System.Text;
    using System.Xml;
    using System.Collections.Generic;

    /// <summary>
    /// FrmPlugins window
    /// </summary>
    public partial class FrmPlugins : Form
    {
        /// <summary>
        /// REST server domain
        /// </summary>
        private const string RESTAPIDOMAIN = "http://update.notefly.org";

        /// <summary>
        /// REST url where to get a list of plugins.
        /// </summary>
        private const string RESTAPIPLUGINSLIST = "/REST/plugins/list.php";

        /// <summary>
        /// REST url where to get the detail of a partialer plugin 
        /// </summary>
        private const string RESTAPIPLUGINDETAILS = "/REST/plugins/details.php?name=";

        /// <summary>
        /// REST url where to get a list of matching pluginnames searched by a partialer keyword.
        /// </summary>
        private const string RESTAPIPLUGINSSEARCH = "/REST/plugins/search.php?keyword=";

        /// <summary>
        /// REST url where to get several versions of plugins in xml (with a http POST request).
        /// </summary>
        private const string RESTAPIPLUGINVERSION = "/REST/plugins/version.php"; 

        /// <summary>
        /// The url to download plugin of the current plugin being selected.
        /// </summary>
        private string currentplugindownloadurl = null;

        /// <summary>
        /// Names of plugins to update.
        /// </summary>
        private List<string> pluginsupdatenames;
        
        /// <summary>
        /// Download urls of plugins.
        /// </summary>
        private Dictionary<string, string> pluginupdatedownloads;

        /// <summary>
        /// Reference to RSAverify class
        /// </summary>
        private RSAVerify rsaverify;

        /// <summary>
        /// Refence to FrmDownload window.
        /// </summary>
        private FrmDownloader frmdownloader;

        /// <summary>
        /// 
        /// </summary>
        private int textloadingdots = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmPlugins" /> class.
        /// </summary>
        public FrmPlugins()
        {
            this.DoubleBuffered = Settings.ProgramFormsDoublebuffered;
            this.InitializeComponent();
            this.SetTabPageUpdatesVisible(false);
            this.pluginGrid.DrawAllPluginsDetails(this.tabPagePluginsInstalled.ClientRectangle.Width);
            this.SetFormTitle();
            Strings.TranslateForm(this);
            this.pluginGrid.Enabled = Settings.ProgramPluginsAllEnabled;
            this.CheckPluginsUpdates();
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
                if (!this.searchtbPlugins.IsKeywordEntered)
                {
                    HttpUtil httputil_allplugins = new HttpUtil(RESTAPIDOMAIN + RESTAPIPLUGINSLIST, System.Net.Cache.RequestCacheLevel.Revalidate, null);
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
                    HttpUtil httputil_plugindetail = new HttpUtil(RESTAPIDOMAIN + RESTAPIPLUGINDETAILS + System.Web.HttpUtility.UrlEncode(pluginname), System.Net.Cache.RequestCacheLevel.Revalidate);
                    this.ClearPluginDetails();

                    this.timerTextUpdater.Start();
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
            this.timerTextUpdater.Stop();
            bool alreadyinstalled = false;
            bool updateavailable = false;
            string[] detailsplugin = xmlUtil.ParserDetailsPlugin(response, PluginsManager.GetInstalledPlugins(), out alreadyinstalled, out updateavailable);
            this.ClearPluginDetails();
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

                this.rsaverify = new RSAVerify(detailsplugin[5]);
            }
            else
            {
                this.lblPluginDescription.Text = Strings.T("Could not load plugins details, please check if your network connection is still working.");
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
            if (this.rsaverify != null)
            {
                if (!this.rsaverify.CheckFileSignatureAndDisplayErrors(newfiles[0]) || !File.Exists(newfiles[0]))
                {
                    return;
                }
            }

            if (this.frmdownloader.GetFileCompressedkind(newfiles[0]) == 1)
            {
                string[] unzipextensions = new string[1] { ".dll" };
                if (this.frmdownloader.DecompressZipFile(newfiles[0], unzipextensions))
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
        /// <param name="logdesc">Log mesage</param>
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
            HttpUtil httputil_searchplugins = new HttpUtil(RESTAPIDOMAIN + RESTAPIPLUGINSSEARCH + System.Web.HttpUtility.UrlEncode(keywords), System.Net.Cache.RequestCacheLevel.Revalidate, null);
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
        /// The user wants to stop searching, get the list of all plugins again.
        /// </summary>
        private void searchtbPlugins_SearchStop()
        {
            HttpUtil httputil_allplugins = new HttpUtil(RESTAPIDOMAIN + RESTAPIPLUGINSLIST, System.Net.Cache.RequestCacheLevel.Revalidate);
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
        /// <param name="isconnected">True if their is a network connection.</param>
        private void SetAvailablePluginsNetwork(bool isconnected)
        {
            this.lblTextNoInternetConnection.Visible = !isconnected;
            this.searchtbPlugins.Enabled = isconnected;
            if (!isconnected)
            {
                string nointernetconnectionmessage = Strings.T("Could not load list with plugins. Internet connection failed.");
                if (Settings.NetworkProxyEnabled)
                {
                    nointernetconnectionmessage += Environment.NewLine + Strings.T("A proxy is being used.");
                }

                this.lblTextNoInternetConnection.Text = nointernetconnectionmessage;
                Log.Write(LogType.info, nointernetconnectionmessage);
                this.lbxAvailablePlugins.Items.Clear();
            }
        }

        /// <summary>
        /// Check plugins on updates.
        /// </summary>
        private void CheckPluginsUpdates()
        {
            string[] installedpluginsnames = PluginsManager.GetInstalledPlugins();
            StringBuilder sbpluginspostsparam = new StringBuilder("plugins=");
            for (int i = 0; i < installedpluginsnames.Length; i++)
            {
                if (i != 0)
                {
                    sbpluginspostsparam.Append(",");
                }

                sbpluginspostsparam.Append(System.Web.HttpUtility.UrlEncode(installedpluginsnames[i]));
            }

            if (sbpluginspostsparam.Length < 1)
            {
                return;
            }

            HttpUtil httputil = new HttpUtil(RESTAPIDOMAIN + RESTAPIPLUGINVERSION, System.Net.Cache.RequestCacheLevel.Revalidate, sbpluginspostsparam.ToString());
            httputil.Start(new RunWorkerCompletedEventHandler(this.httputil_pluginsversions_compleet));
        }

        /// <summary>
        /// Parser results of versions requested plugins, and create tabpage if plugin update(s) are available.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void httputil_pluginsversions_compleet(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            string result = (string)e.Result;
            if (result == null)
            {
                return;
            }

            if (result.Length < 1)
            {
                return;
            }

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(result);
            this.pluginsupdatenames = new List<string>();
            this.pluginupdatedownloads = new Dictionary<string, string>();
            XmlNodeList xmlnodelist = xmldoc.SelectNodes("/plugins/plugin");
            foreach (XmlNode xmlnode in xmlnodelist) 
            {
                if (xmlnode.ChildNodes.Count > 1)
                {
                    string pluginname = string.Empty;
                    string downloadurl = string.Empty;
                    short[] pluginversion = new short[3];
                    for (int i = 0; i < xmlnode.ChildNodes.Count; i++)
                    {
                        if (xmlnode.ChildNodes[i].Name == "name")
                        {
                            pluginname = xmlnode.ChildNodes[i].InnerText;
                        }
                        else if (xmlnode.ChildNodes[i].Name == "version")
                        {
                            pluginversion = Program.ParserVersionString(xmlnode.ChildNodes[i].InnerText);
                        }
                        else if (xmlnode.ChildNodes[i].Name == "downloadurl")
                        {
                            downloadurl = xmlnode.ChildNodes[i].InnerText;
                        }
                    }

                    short[] currentpluginversion = PluginsManager.GetPluginVersionByName(pluginname);
                    if (Program.CompareVersions(pluginversion, currentpluginversion) > 0)
                    {
                        if (!String.IsNullOrEmpty(downloadurl))
                        {
                            this.pluginsupdatenames.Add(pluginname);
                            this.pluginupdatedownloads.Add(pluginname, downloadurl);
                        }
                    }
                }
                else
                {
                    if (xmlnode.ChildNodes.Count == 1)
                    {
                        const int MAXLENMESSAGE = 500;
                        string errormessage = xmlnode.ChildNodes[0].InnerText;
                        if (errormessage.Length > MAXLENMESSAGE)
                        {
                            errormessage = errormessage.Substring(0, MAXLENMESSAGE);
                        }

                        Log.Write(LogType.error, errormessage);
                    }
                }
            }

            if (this.pluginupdatedownloads.Count > 0)
            {
                this.SetTabPageUpdatesVisible(true);
                for (int i = 0; i < this.pluginsupdatenames.Count; i++)
                {
                    this.chxlbxPluginUpdates.Items.Add(pluginsupdatenames[i], true);
                }

                this.tabControlPlugins.SelectedTab = this.tabPagePluginsUpdates;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnupdateplugins_Click(object sender, EventArgs e)
        {
            this.frmdownloader = new FrmDownloader(Strings.T("Updating plugins.."));
            List<string> newupdateplugindownloads = new List<string>();
            for (int i = 0; i < this.chxlbxPluginUpdates.Items.Count; i++)
            {
                if (this.chxlbxPluginUpdates.GetItemChecked(i))
                {
                    string pluginname = this.chxlbxPluginUpdates.Items[i].ToString();
                    string plugindownload = this.pluginupdatedownloads[pluginname];
                    newupdateplugindownloads.Add(plugindownload);
                }
            }

            this.frmdownloader.Show();
            this.frmdownloader.AllDownloadsCompleted += new FrmDownloader.DownloadCompleetHandler(this.downloader_DownloadCompleet);
            if (!Directory.Exists(Path.Combine(Settings.ProgramPluginsFolder,"new"))) {
                Directory.CreateDirectory(Path.Combine(Settings.ProgramPluginsFolder,"new"));
            }

            this.frmdownloader.BeginDownload(newupdateplugindownloads.ToArray(), Path.Combine(Settings.ProgramPluginsFolder,"new"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerTextUpdater_Tick(object sender, EventArgs e)
        {
            const int MAXTEXTLOADINGDOTS = 5;
            StringBuilder text = new StringBuilder(Strings.T("Loading"));
            for (int i = 0; i < this.textloadingdots; i++)
            {
                text.Append(".");
            }

            this.lblPluginName.Text = text.ToString();
            this.textloadingdots++;
            if (this.textloadingdots > MAXTEXTLOADINGDOTS)
            {
                this.textloadingdots = 0;
            }
        }

        /// <summary>
        /// Set the this.tabPagePluginsUpdates visible or invisible.
        /// </summary>
        /// <param name="showupdatetab">showupdatetab</param>
        private void SetTabPageUpdatesVisible(bool showupdatetab)
        {
            if ((showupdatetab) && (!this.tabControlPlugins.TabPages.Contains(this.tabPagePluginsUpdates)))
            {
                this.tabControlPlugins.TabPages.Add(this.tabPagePluginsUpdates);    
            }
            else if (this.tabControlPlugins.TabPages.Contains(this.tabPagePluginsUpdates)) 
            {
                this.tabControlPlugins.TabPages.Remove(this.tabPagePluginsUpdates);
            }
        }
    }
}
