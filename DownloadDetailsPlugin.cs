using System;
using System.Collections.Generic;
using System.Text;

namespace NoteFly
{
    /// <summary>
    /// 
    /// </summary>
    public class DownloadDetailsPlugin
    {
        /// <summary>
        /// The name of the plugin
        /// </summary>
        private string name;

        /// <summary>
        /// The version of the plugin.
        /// </summary>
        private string version;

        /// <summary>
        /// The download url of the plugin to download
        /// </summary>
        private string downloadurl;

        /// <summary>
        /// The kind of license this plugin is under released.
        /// </summary>
        private string licensetype;

        /// <summary>
        /// The description of the plugin.
        /// </summary>
        private string description;

        /// <summary>
        /// The sidnature of the plugin.
        /// </summary>
        private string signature;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public DownloadDetailsPlugin()
        {
        }

        /// <summary>
        /// Get the name of the plugin.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    Log.Write(LogType.error, "No plugin name");
                }

                this.name = value;
            }
        }

        /// <summary>
        /// Get the version of this plugin as string.
        /// </summary>
        public string Version
        {
            get
            {
                return this.version;
            }

            set
            {
                this.version = value;
            }
        }

        /// <summary>
        /// Get the url where to download this plugin.
        /// </summary>
        public string DownloadUrl
        {
            get
            {
                return this.downloadurl;
            }

            set
            {
                if (Uri.IsWellFormedUriString(value, UriKind.Absolute))
                {
                    Log.Write(LogType.error, "Not well formed download url.");
                }

                this.downloadurl = value;
            }
        }

        /// <summary>
        /// Get the license type.
        /// </summary>
        public string LicenseType
        {
            get
            {
                return this.licensetype;
            }

            set
            {
                this.licensetype = value;
            }
        }

        /// <summary>
        /// Get the description
        /// </summary>
        public string Description
        {
            get
            {
                return this.description;
            }

            set
            {
                this.description = value;
            }
        }

        /// <summary>
        /// Get the signature.
        /// </summary>
        public string Signature
        {
            get
            {
                return this.signature;
            }

            set
            {
                this.signature = value;
            }
        }

        /// <summary>
        /// Get if this a plugin that is installed.
        /// </summary>
        /// <returns>True if a plugin with this name is installed.</returns>
        public bool IsInstalledPlugin()
        {
            if (string.IsNullOrEmpty(this.name))
            {
                Log.Write(LogType.error, "Cannot figure out if plugin is installed because plugin name unknow.");
                return false;
            }

            string[] installedpluginnames = PluginsManager.GetInstalledPlugins();
            for (int i = 0; i < installedpluginnames.Length; i++)
            {
                if (this.name.Equals(installedpluginnames[i], StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Get if this plugindetails is of newer version than installed.
        /// </summary>
        /// <returns>True if newer DownloadDetailsPlugin version is newer than installed.</returns>
        public bool IsNewerVersion()
        {
            if (string.IsNullOrEmpty(this.name) || string.IsNullOrEmpty(this.version))
            {
                Log.Write(LogType.error, "DownloadDetailsPlugin name and/or versions is unknown.");
                return false;
            }

            short[] installedpluginversion = PluginsManager.GetPluginVersionByName(this.name);
            short[] availablepluginversion = Program.ParserVersionString(this.version);
            if (Program.CompareVersions(availablepluginversion, installedpluginversion) > 0)
            {
                // 1 if availablepluginversion is higher than installedpluginversion
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
