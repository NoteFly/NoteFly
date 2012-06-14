//-----------------------------------------------------------------------
// <copyright file="PluginsManager.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2012  Tom
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
    using System.IO;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// PluginsManager class, provides plugins functions
    /// </summary>
    public static class PluginsManager
    {
        /// <summary>
        /// A list of installed plugins dll filenames
        /// </summary>
        private static List<string> installedplugins = new List<string>();

        /// <summary>
        /// All the enabled plugins
        /// </summary>
        private static List<IPlugin.IPlugin> enabledplugins = new List<IPlugin.IPlugin>();

        /// <summary>
        /// An array of dll files that are not allow to be loaded as notefly plugin in the notefly plugin folder.
        /// </summary>
        private static string[] excludedplugindlls;

        /// <summary>
        /// Gets the installed plugins as array.
        /// </summary>
        public static string[] InstalledPlugins
        {
            get
            {
                return installedplugins.ToArray();
            }
        }

        /// <summary>
        /// Gets the list with all enabled plugins
        /// </summary>
        public static List<IPlugin.IPlugin> EnabledPlugins
        {
            get
            {
                return enabledplugins;
            }
        }

        /// <summary>
        /// Load all enabled plugins
        /// </summary>
        public static void LoadPlugins()
        {
            installedplugins.Clear();
            enabledplugins.Clear();
            if (Directory.Exists(Settings.ProgramPluginsFolder))
            {
                string[] enabledpluginsfilenames = Settings.ProgramPluginsEnabled.Split('|');
                string[] dllfiles = GetDllFilesPluginFolder();
                excludedplugindlls = Settings.ProgramPluginsDllexclude.Split('|');

                for (int i = 0; i < dllfiles.Length; i++)
                {
                    if (!IsPluginFileExcluded(dllfiles[i]))
                    {
                        installedplugins.Add(dllfiles[i]);
                        foreach (string enabledpluginfilename in enabledpluginsfilenames)
                        {
                            if (enabledpluginfilename.Equals(dllfiles[i], StringComparison.Ordinal))
                            {
                                EnablePlugin(dllfiles[i]);
                            }
                        }
                    }
                }
            }
            else
            {
                Log.Write(LogType.info, "Plugin folder does not exist, recreate it.");
                Directory.CreateDirectory(Settings.ProgramPluginsFolder);
            }
        }

        /// <summary>
        /// Load a plugin.
        /// </summary>
        /// <param name="dllfile">The current list with loaded plugins</param>
        public static void EnablePlugin(string dllfilename)
        {
            try
            {
                System.Reflection.Assembly pluginassembly = null;
                pluginassembly = System.Reflection.Assembly.LoadFrom(Path.Combine(Settings.ProgramPluginsFolder, dllfilename));
                if (pluginassembly != null)
                {
                    foreach (Type curplugintype in pluginassembly.GetTypes())
                    {
                        if (curplugintype.IsPublic && !curplugintype.IsAbstract && !curplugintype.IsSealed)
                        {
                            // /*
                            if (curplugintype.FullName.Equals(curplugintype.Namespace + "." + curplugintype.Namespace))
                            {
                            // */
                                // Load this plugin class only.
                                Type plugintype = pluginassembly.GetType(curplugintype.ToString(), false, true);
                                if (plugintype != null)
                                {
                                    IPlugin.IPlugin plugin = (IPlugin.IPlugin)Activator.CreateInstance(pluginassembly.GetType(curplugintype.ToString()));
                                    plugin.Register(dllfilename, NoteFly.Program.Notes);
                                    enabledplugins.Add(plugin);
                                }
                            // /*
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write(LogType.exception, "Can't load file: " + dllfilename + " " + ex.Message);
            }
        }

        /// <summary>
        /// Disable a plugin
        /// </summary>
        /// <param name="pluginname"></param>
        /// <returns>return true if dll filename was found and plugin is disabled.</returns>
        public static bool DisablePlugin(string dllfilename)
        {
            for (int i = 0; i < enabledplugins.Count; i++)
            {
                if (enabledplugins[i].Filename.Equals(dllfilename, StringComparison.Ordinal))
                {
                    enabledplugins.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Get the names of all plugins installed.
        /// </summary>
        /// <returns></returns>
        public static string[] GetInstalledPlugins()
        {
            List<string> pluginsnames = new List<string>();
            for (int i = 0; i < installedplugins.Count; i++)
            {
                Assembly pluginassembly = System.Reflection.Assembly.LoadFrom(Path.Combine(Settings.ProgramPluginsFolder, installedplugins[i]));
                string pluginname = GetPluginName(pluginassembly);
                if (!string.IsNullOrEmpty(pluginname))
                {
                    pluginsnames.Add(pluginname);
                }
            }

            return pluginsnames.ToArray();
        }

        #region Getting plugin details

        /// <summary>
        /// Get the name of the plugin.
        /// </summary>
        /// <param name="pluginassembly">The plugin assembly</param>
        /// <returns>The name of the plugin</returns>
        public static string GetPluginName(Assembly pluginassembly)
        {
            string pluginname = Strings.T("unknown");
            object[] atttitle = pluginassembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (atttitle.Length > 0)
            {
                AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)atttitle[0];
                if (titleAttribute.Title != string.Empty)
                {
                    if (titleAttribute.Title.Length > 200)
                    {
                        pluginname = titleAttribute.Title.Substring(0, 200);
                    }
                    else
                    {
                        pluginname = titleAttribute.Title;
                    }
                }
                else
                {
                    Log.Write(LogType.exception, "Plugin " + pluginassembly.Location + " has no name.");
                }
            }

            return pluginname;
        }

        /// <summary>
        /// Get author or author company of the plugin.
        /// </summary>
        /// <param name="pluginassembly">The plugin assembly</param>
        /// <returns>The author or company of the plugin</returns>
        public static string GetPluginAuthor(Assembly pluginassembly)
        {
            string pluginauthor = Strings.T("unknown");
            object[] attributes = pluginassembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            if (attributes.Length != 0)
            {
                if (!string.IsNullOrEmpty(((AssemblyCompanyAttribute)attributes[0]).Company))
                {
                    if (((AssemblyCompanyAttribute)attributes[0]).Company.Length > 200)
                    {
                        pluginauthor = ((AssemblyCompanyAttribute)attributes[0]).Company.Substring(0, 200);
                    }
                    else
                    {
                        pluginauthor = ((AssemblyCompanyAttribute)attributes[0]).Company;
                    }
                }
                else
                {
                    Log.Write(LogType.exception, "Plugin " + pluginassembly.Location + " has no author.");
                }
            }

            return pluginauthor;
        }

        /// <summary>
        /// Get file (short)description of the plugin.
        /// </summary>
        /// <param name="pluginassembly">The plugin assembly</param>
        /// <returns>The description of the plugin</returns>
        public static string GetPluginDescription(Assembly pluginassembly)
        {
            string plugindescription = string.Empty;
            object[] attdesc = pluginassembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if (attdesc.Length != 0)
            {
                if (((AssemblyDescriptionAttribute)attdesc[0]).Description.Length > 1000)
                {
                    plugindescription = ((AssemblyDescriptionAttribute)attdesc[0]).Description.Substring(0, 1000);
                }
                else
                {
                    plugindescription = ((AssemblyDescriptionAttribute)attdesc[0]).Description;
                }
            }

            return plugindescription;
        }

        /// <summary>
        /// Get version of the plugin as string.
        /// </summary>
        /// <param name="pluginassembly">The plugin assembly</param>
        /// <returns>The version of the plugin as string</returns>
        public static string GetPluginVersion(Assembly pluginassembly)
        {
            if (pluginassembly.GetName().Version != null)
            {
                return pluginassembly.GetName().Version.ToString();
            }
            else
            {
                Log.Write(LogType.exception, "Plugin " + pluginassembly.Location + " has no version information.");
                return Strings.T("unknown");
            }
        }

        #endregion Getting plugin details

        /// <summary>
        /// Get if a plugin dll file enabled.
        /// </summary>
        /// <param name="dllfilename">The plugin dll file</param>
        /// <returns></returns>
        public static bool IsPluginEnabled(string dllfilename)
        {
            for (int i = 0; i < enabledplugins.Count; i++)
            {
                if (enabledplugins[i].Filename.Equals(dllfilename, StringComparison.Ordinal))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Get the version of a plugin by the plugin name.
        /// </summary>
        /// <param name="pluginname">The name of the plugin</param>
        /// <returns>An array with the major, minor and release version numbers.</returns>
        public static short[] GetPluginVersionByName(string pluginname)
        {
            short[] pluginversion = new short[3];
            for (int i = 0; i < installedplugins.Count; i++)
            {
                Assembly pluginassembly = System.Reflection.Assembly.LoadFrom(Path.Combine(Settings.ProgramPluginsFolder, installedplugins[i]));
                if (GetPluginName(pluginassembly).Equals(pluginname, StringComparison.Ordinal))
                {
                    string pluginversionstring = GetPluginVersion(pluginassembly);
                    pluginversion = Program.ParserVersionString(pluginversionstring);
                }
            }

            return pluginversion;
        }

        /// <summary>
        /// Get the version information from the IPlugin libery.
        /// </summary>
        /// <returns>Array with version numbers major, minor and release numbers.</returns>
        public static short[] GetIPluginVersion()
        {
            string versionipluginstring = string.Empty;
            System.Reflection.Assembly ipluginasm;
            try
            {
                ipluginasm = System.Reflection.Assembly.ReflectionOnlyLoadFrom(Path.Combine(Program.InstallFolder, "IPlugin.dll"));
                versionipluginstring = ipluginasm.GetName().Version.Major + "." + ipluginasm.GetName().Version.Minor + "." + ipluginasm.GetName().Version.Build;
            }
            catch (FileNotFoundException)
            {
                Log.Write(LogType.exception, "File IPlugin.dll not found.");
            }
            catch (FileLoadException)
            {
                Log.Write(LogType.exception, "Cannot load iplugin.dll");
            }

            short[] ipluginversion = Program.ParserVersionString(versionipluginstring);
            return ipluginversion;
        }

        /// <summary>
        /// Save the enabled plugin settings.
        /// </summary>
        public static void SaveEnabledPlugins()
        {
            StringBuilder sbenabledplugin = new StringBuilder();
            if (enabledplugins != null)
            {
                bool first = true;
                for (int i = 0; i < enabledplugins.Count; i++)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        sbenabledplugin.Append("|");
                    }

                    sbenabledplugin.Append(enabledplugins[i].Filename);
                }
            }

            Settings.ProgramPluginsEnabled = sbenabledplugin.ToString();
            xmlUtil.WriteSettings();
        }

        /// <summary>
        /// Get all the dll filenames(without full path) in the plugin directory.
        /// </summary>
        /// <returns>All dll filenames as string array</returns>
        private static string[] GetDllFilesPluginFolder()
        {
            string[] pluginfiles = Directory.GetFiles(Settings.ProgramPluginsFolder, "*.dll", SearchOption.TopDirectoryOnly);
            string[] pluginfilenames = new string[pluginfiles.Length];
            for (int i = 0; i < pluginfiles.Length; i++)
            {
                pluginfilenames[i] = Path.GetFileName(pluginfiles[i]);
            }

            pluginfiles = null;
            return pluginfilenames;
        }

        /// <summary>
        /// Is the dll files excluded as plugin in the plugin directory.
        /// </summary>
        /// <param name="dllfilename">The dll filename without path</param>
        /// <returns>True if it's excluded</returns>
        private static bool IsPluginFileExcluded(string dllfilename)
        {
            if (excludedplugindlls != null)
            {
                for (int i = 0; i < excludedplugindlls.Length; i++)
                {
                    if (dllfilename.Equals(excludedplugindlls[i], StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
