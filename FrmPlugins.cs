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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

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
        /// 
        /// </summary>
        private const string PLUGINMOREINFOURL = "http://www.notefly.org/plugindetails?name=";

        /// <summary>
        /// Are plugin updates checkd by default
        /// </summary>
        private const bool PLUGINUPDATECHECKEDDEFAULT = true;

        /// <summary>
        /// The current selected plugin on the available plugins tabpage.
        /// </summary>
        private DownloadDetailsPlugin selectedplugindetails;

        /// <summary>
        /// All the plugins that can be updated.
        /// </summary>
        private List<DownloadDetailsPlugin> updatableplugins;

        /// <summary>
        /// Plugins positions in updatableplugins list that are being updated.
        /// </summary>
        private List<int> updatedplugins;

        /// <summary>
        /// Refence to FrmDownload window.
        /// </summary>
        private FrmDownloader frmdownloader;

        /// <summary>
        /// Number of dots after the loading text.
        /// </summary>
        private int textloadingdots = 0;

        /// <summary>
        /// The time the last request was made.
        /// </summary>
        private DateTime lastrequesttime;

        /// <summary>
        /// The tooltips of FrmPlugins.
        /// </summary>
        private ToolTip tooltip;

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
            this.SetFormTooltips();
            Strings.TranslateForm(this);
            this.pluginGrid.Enabled = Settings.ProgramPluginsAllEnabled;
            if (this.IsUpdatecheckPluginNeeded())
            {
                if (this.CheckPluginsUpdates()) 
                {
                    Settings.UpdatecheckPluginsLastDate = DateTime.Now.ToString();
                    xmlUtil.WriteSettings();
                }                
            }

            this.linklblPluginMoreInfo.Text = Strings.T("more info");
            this.lblPluginDescription.Text = Strings.T("Getting plugin details from the internet.");
        }

        /// <summary>
        /// Set the title of this form.
        /// </summary>
        private void SetFormTitle()
        {
            this.Text = Strings.T("Plugins") + " - " + Program.AssemblyTitle;
        }

        /// <summary>
        /// Set all form tooltips if tooltips are enabled.
        /// </summary>
        private void SetFormTooltips()
        {
            if (Settings.NotesTooltipsEnabled)
            {
                this.tooltip = new ToolTip(this.components);
                this.searchtbPlugins.SetControlTooltip(this.tooltip);
                this.pluginGrid.SetControlTooltip(this.tooltip);
            }
            else
            {
                if (this.tooltip != null)
                {
                    this.tooltip.Active = false;
                    this.tooltip.Dispose();
                }
            }
        }

        /// <summary>
        /// Check if there is a update check for the plugins needed.
        /// </summary>
        /// <returns>true if Settings.UpdatecheckPluginsLastDate + Settings.UpdatecheckPluginsEverydays more than current datetime.</returns>
        private bool IsUpdatecheckPluginNeeded()
        {
            bool pluginupdatecheckneeded = false;
            if (Settings.UpdatecheckPluginsEverydays > 0)
            {
                DateTime lastupdateplugins = DateTime.Parse(Settings.UpdatecheckPluginsLastDate);
                if (lastupdateplugins.AddDays(Settings.UpdatecheckPluginsEverydays) <= DateTime.Now)
                {
                    pluginupdatecheckneeded = true;
                }
            }

            return pluginupdatecheckneeded;
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
            if (this.lbxAvailablePlugins.SelectedIndex >= 0)
            {
                if (this.RateLimitRequest())
                {
                    return;
                }

                this.lastrequesttime = DateTime.Now;
                this.splitContainerAvailablePlugins.Panel2Collapsed = false;
                this.btnPluginDownload.Visible = false;
                string pluginname = this.lbxAvailablePlugins.SelectedItem.ToString();
                if (pluginname == "Loading...")
                {
                    Log.Write(LogType.error, "Should not select plugin yet.");
                    return;
                }

                if (string.IsNullOrEmpty(pluginname))
                {
                    Log.Write(LogType.error, "Empty plugin name.");
                    return;
                }

                HttpUtil httputil_plugindetail = new HttpUtil(RESTAPIDOMAIN + RESTAPIPLUGINDETAILS + System.Web.HttpUtility.UrlEncode(pluginname), System.Net.Cache.RequestCacheLevel.Revalidate);
                this.ClearPluginDetails();
                this.timerTextUpdaterLoading.Start();
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

        /// <summary>
        /// Check if request is not too fast after previous one.
        /// Avoid REST api silently refuses to answer.
        /// </summary>
        /// <returns>True if request is too fast.</returns>
        private bool RateLimitRequest()
        {
            const int MINDIFFREQTIME = 400;
            if (this.lastrequesttime != null)
            {
                if (this.lastrequesttime.AddMilliseconds(MINDIFFREQTIME).CompareTo(DateTime.Now) > 0)
                {
                    return true;
                }
            }

            return false;
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
            this.linklblPluginMoreInfo.Visible = false;
        }

        /// <summary>
        /// Downloading of plugin details data is compleet, parser data and display plugin details
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">RunWorkerCompleted event arguments</param>
        private void httputil_plugindetail_DownloadCompleet(object sender, RunWorkerCompletedEventArgs e)
        {
            string response = (string)e.Result;
            if (String.IsNullOrEmpty(response))
            {
                return;
            }

            this.timerTextUpdaterLoading.Stop();
            this.ClearPluginDetails();
            this.selectedplugindetails = xmlUtil.ParserDetailsPlugin(response);
            if (this.selectedplugindetails != null)
            {
                if (this.selectedplugindetails.IsInstalledPlugin())
                {
                    this.btnPluginDownload.Enabled = false;
                    if (this.selectedplugindetails.IsNewerVersion())
                    {
                        this.btnPluginDownload.Text = Strings.T("needs update");
                        if (this.updatableplugins == null)
                        {
                            this.updatableplugins = new List<DownloadDetailsPlugin>();
                        }

                        if (!this.UpdateablePluginsContainsPlugin(this.selectedplugindetails))
                        {
                            this.updatableplugins.Add(this.selectedplugindetails);
                            this.chxlbxPluginUpdates.Items.Add(this.selectedplugindetails.Name, PLUGINUPDATECHECKEDDEFAULT);
                            this.SetTabPageUpdatesVisible(true);
                        }
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

                if (!string.IsNullOrEmpty(this.selectedplugindetails.DownloadUrl) && Uri.IsWellFormedUriString(this.selectedplugindetails.DownloadUrl, UriKind.Absolute))
                {
                    this.btnPluginDownload.Visible = true;
                }

                this.lblPluginName.Text = this.selectedplugindetails.Name;
                this.lblPluginVersion.Text = Strings.T("version: ") + this.selectedplugindetails.Version;
                this.lblLicense.Text = Strings.T("license: ") + this.selectedplugindetails.LicenseType;
                this.lblPluginDescription.Text = this.selectedplugindetails.Description;
                this.linklblPluginMoreInfo.Visible = true;
                if (Settings.NotesTooltipsEnabled)
                {
                    this.tooltip.SetToolTip(this.linklblPluginMoreInfo, Strings.T("visit: {0}", PLUGINMOREINFOURL + Uri.EscapeUriString(this.selectedplugindetails.Name)));
                }
            }
            else
            {
                this.lblPluginDescription.Text = Strings.T("Could not load plugins details, please check if your network connection is still working.");
                this.linklblPluginMoreInfo.Visible = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pluginname"></param>
        /// <returns></returns>
        private bool UpdateablePluginsContainsPlugin(DownloadDetailsPlugin plugin)
        {
            for (int i = 0; i < this.updatableplugins.Count; i++)
            {
                if (this.updatableplugins[i].DownloadUrl == plugin.DownloadUrl)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Start download of plugin.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnPluginDownload_Click(object sender, EventArgs e)
        {
            if (this.selectedplugindetails != null) 
            {             
                this.frmdownloader = new FrmDownloader(Strings.T("Downloading plugin.."));
                this.frmdownloader.AllDownloadsCompleted += new FrmDownloader.DownloadCompleetHandler(this.downloader_DownloadCompleet);
                this.frmdownloader.Show();
                this.frmdownloader.BeginDownload(this.selectedplugindetails.DownloadUrl, Settings.ProgramPluginsFolder);
            }
        }

        /// <summary>
        /// Downloading of plugin compleet.
        /// </summary>
        /// <param name="newfiles">Array of files downloads.</param>
        private void downloader_DownloadCompleet(string[] newpluginfile)
        {
            RSAVerify rsaverify = new RSAVerify();
            if (!rsaverify.CheckFileSignatureAndDisplayErrors(newpluginfile[0], this.selectedplugindetails.Signature))
            {
                return;
            }

            this.DecompressDownload(newpluginfile[0]);
            PluginsManager.LoadPlugins();
            this.pluginGrid.DrawAllPluginsDetails(this.tabPagePluginsInstalled.ClientRectangle.Width);
            this.tabControlPlugins.SelectedTab = this.tabPagePluginsInstalled;
        }

        /// <summary>
        /// Downloading of plugin updates has been compleeted.
        /// </summary>
        /// <param name="newpluginfile"></param>
        private void DownloadUpdatesPluginsCompleet(string[] compressedpluginfiles)
        {
            bool updatesinstalled = false;
            RSAVerify rsaverify = new RSAVerify();
            for (int i = 0; i < compressedpluginfiles.Length; i++)
            {
                int pluginposition = this.updatedplugins[i];
                if (rsaverify.CheckFileSignatureAndDisplayErrors(compressedpluginfiles[i], this.updatableplugins[pluginposition].Signature))
                {
                    this.DecompressDownload(compressedpluginfiles[i]);
                    updatesinstalled = true;
                }
            }

            for (int p = this.updatedplugins.Count - 1; p >= 0; p--)
            {
                this.chxlbxPluginUpdates.Items.RemoveAt(p);
            }

            if (updatesinstalled)
            {
                PluginsManager.LoadPlugins();
                this.pluginGrid.DrawAllPluginsDetails(this.tabPagePluginsInstalled.ClientRectangle.Width);
                this.btnRestartProgram.Visible = true;
            }

            if (this.chxlbxPluginUpdates.Items.Count > 0)
            {
                this.btnupdateplugins.Enabled = true;
            }

            this.chxlbxPluginUpdates.Enabled = true;
        }

        /// <summary>
        /// Decompress the downloaded file.
        /// </summary>
        /// <param name="newfiles"></param>
        private void DecompressDownload(string compressedpluginfile)
        {
            if (this.frmdownloader.GetFileCompressedkind(compressedpluginfile) == 1)
            {
                string[] unzipextensions = new string[1] { ".dll" };
                if (this.frmdownloader.DecompressZipFile(compressedpluginfile, unzipextensions))
                {
                    // decompress succesfully, now delete zip file
                    this.DeleteNotsysFile(compressedpluginfile, "Delete zip archive: ");
                }
            }
            else if (this.frmdownloader.GetFileCompressedkind(compressedpluginfile) == 2)
            {
                if (this.frmdownloader.DecompressGZipFile(compressedpluginfile))
                {
                    // decompress succesfully, now delete gzip file
                    this.DeleteNotsysFile(compressedpluginfile, "Delete GZip file: ");
                }
            }
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
        private bool CheckPluginsUpdates()
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
                return true;
            }

            HttpUtil httputil = new HttpUtil(RESTAPIDOMAIN + RESTAPIPLUGINVERSION, System.Net.Cache.RequestCacheLevel.Revalidate, sbpluginspostsparam.ToString());
            if (httputil.Start(new RunWorkerCompletedEventHandler(this.httputil_pluginsversions_compleet)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Parser results of versions requested plugins, and set tabpage with plugin update visible
        /// if there are plugin update(s) are available.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
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

            this.updatableplugins = new List<DownloadDetailsPlugin>();
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(result);
            XmlNodeList xmlnodelist = xmldoc.SelectNodes("/plugins/plugin");
            foreach (XmlNode xmlnode in xmlnodelist) 
            {
                if (xmlnode.ChildNodes.Count > 1)
                {
                    bool minversionipluginokay = true;
                    DownloadDetailsPlugin downloaddetailsplugin = new DownloadDetailsPlugin();
                    for (int i = 0; (i < xmlnode.ChildNodes.Count && minversionipluginokay); i++)
                    {
                        switch (xmlnode.ChildNodes[i].Name)
                        {
                            case "name":
                                downloaddetailsplugin.Name = xmlnode.ChildNodes[i].InnerText;
                                break;
                            case "version":
                                downloaddetailsplugin.Version = xmlnode.ChildNodes[i].InnerText;
                                break;
                            case "downloadurl":
                                downloaddetailsplugin.DownloadUrl = xmlnode.ChildNodes[i].InnerText;
                                break;
                            case "signature":
                                downloaddetailsplugin.Signature = xmlnode.ChildNodes[i].InnerText;
                                break;
                            case "minversioniplugin":
                                short[] pluginipluginversionneeded = new short[] { 0,0,0};
                                try 
                                {
                                    pluginipluginversionneeded = Program.ParserVersionString(xmlnode.ChildNodes[i].InnerText);
                                } 
                                catch (Exception) 
                                {
                                    Log.Write(LogType.error, "Server returned unknown minversioniplugin.");
                                }

                                if (Program.CompareVersions(pluginipluginversionneeded, PluginsManager.GetIPluginVersion()) > 1)
                                {
                                    minversionipluginokay = false;
                                }

                                break;
                        }
                    }

                    if (downloaddetailsplugin.IsInstalledPlugin() && minversionipluginokay)
                    {
                        if (downloaddetailsplugin.IsNewerVersion())
                        {
                            this.updatableplugins.Add(downloaddetailsplugin);
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

            if (this.updatableplugins.Count > 0)
            {
                this.SetTabPageUpdatesVisible(true);
                this.tabControlPlugins.SelectedTab = this.tabPagePluginsUpdates;
            }

            for (int i = 0; i < this.updatableplugins.Count; i++)
            {
                this.chxlbxPluginUpdates.Items.Add(this.updatableplugins[i].Name, PLUGINUPDATECHECKEDDEFAULT);
            }
        }

        /// <summary>
        /// Button clicked to update plugins.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnupdateplugins_Click(object sender, EventArgs e)
        {
            this.btnupdateplugins.Enabled = false;
            this.updatedplugins = new List<int>();
            this.frmdownloader = new FrmDownloader(Strings.T("Updating plugins.."));
            this.chxlbxPluginUpdates.Enabled = false;
            for (int i = 0; i < this.chxlbxPluginUpdates.Items.Count; i++)
            {
                if (this.chxlbxPluginUpdates.GetItemChecked(i))
                {
                    // checked for update plugin
                    if (this.updatableplugins[i] == null)
                    {
                        continue;
                    }

                    this.updatedplugins.Add(i);
                }

            }

            this.frmdownloader.Show();
            this.frmdownloader.AllDownloadsCompleted += new FrmDownloader.DownloadCompleetHandler(this.DownloadUpdatesPluginsCompleet);
            string[] alldownloadurls = this.GetAllDownloadUrls(updatedplugins.ToArray());
            this.frmdownloader.BeginDownload(alldownloadurls, Program.GetNewPluginFolder(true));
        }

        /// <summary>
        /// Gets a array with all downloadurls in the updatableplugins list.
        /// </summary>
        /// <param name="updatedplugins">Array with positions in updateableplugins list to be updated.</param>
        /// <returns>Array with all download urls of plugins to be updated.</returns>
        private string[] GetAllDownloadUrls(int[] updatedplugins)
        {
            string[] alldownloadurls = new string[updatedplugins.Length];
            for (int i = 0; i < updatedplugins.Length; i++)
            {
                int pluginposition = updatedplugins[i];
                alldownloadurls[i] = this.updatableplugins[pluginposition].DownloadUrl;
            }

            return alldownloadurls;
        }

        /// <summary>
        /// Update the number of dots in after the loading text.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
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
        /// <param name="showupdatetab">True if update tab is visible.</param>
        private void SetTabPageUpdatesVisible(bool showupdatetab)
        {
            if ((showupdatetab) && (!this.tabControlPlugins.TabPages.Contains(this.tabPagePluginsUpdates)))
            {
                this.tabControlPlugins.TabPages.Add(this.tabPagePluginsUpdates);    
            }
            else if ((!showupdatetab) && this.tabControlPlugins.TabPages.Contains(this.tabPagePluginsUpdates)) 
            {
                this.tabControlPlugins.TabPages.Remove(this.tabPagePluginsUpdates);
            }
        }

        /// <summary>
        /// Restart this programme
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnRestartProgram_Click(object sender, EventArgs e)
        {
            bool continuerestart = false;
            if (Program.Formmanager.Frmneweditnoteopen)
            {
                DialogResult dlgrescontinu = MessageBox.Show(Strings.T("A note is still open for editing continue restarting {0} now? The unsaved note change will be lost.", Program.AssemblyTitle), "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (dlgrescontinu == System.Windows.Forms.DialogResult.Yes)
                {
                    continuerestart = true;
                }
            }
            else
            {
                continuerestart = true;
            }

            if (continuerestart) 
            {
                Program.DisposeTrayicon();
                Application.Restart();
            }
        }

        /// <summary>
        /// An plugin in the updatable plugin checkboxlistbox is check or unchecked.
        /// Disable the btnupdateplugins button if no plugin is checked.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void chxlbxPluginUpdates_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                this.btnupdateplugins.Enabled = true;
            } 
            else 
            {
                bool anychecked = false;
                for (int i = 0; i < this.chxlbxPluginUpdates.Items.Count; i++)
                {
                    if (this.chxlbxPluginUpdates.GetItemChecked(i) && e.Index != i)
                    {
                        anychecked = true;
                        break;
                    }
                }

                this.btnupdateplugins.Enabled = anychecked;
            }
        }

        /// <summary>
        /// More info plugin clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linklblPluginMoreInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.selectedplugindetails == null)
            {
                return;
            }

            if (!String.IsNullOrEmpty(this.selectedplugindetails.Name))
            {
                Program.LoadLink(PLUGINMOREINFOURL + Uri.EscapeUriString(this.selectedplugindetails.Name), false);
            }
        }
    }
}
