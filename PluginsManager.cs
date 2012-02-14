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
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// 
    /// </summary>
    public static class PluginsManager
    {
        /// <summary>
        /// All the enabled plugins
        /// FIXME: Make CLS-compliant
        /// </summary>
        public static IPlugin.IPlugin[] pluginsenabled;

        /// <summary>
        /// dll files
        /// </summary>
        private static string[] plugindllexcluded;

        /// <summary>
        /// 
        /// </summary>
        static PluginsManager()
        {
            if (Settings.ProgramPluginsAllEnabled)
            {
                pluginsenabled = GetPlugins(true);
            }
        }

        /*
        public void TriggerEnabledPlugins(Delegate m)
        {
                    if (PluginsManager.pluginsenabled != null)
            {
                for (int i = 0; i < PluginsManager.pluginsenabled.Length; i++)
                {
                    
                    //PluginsManager.pluginsenabled[i].
                }
            }
        }
         */

        /// <summary>
        /// Load plugin .dll files from pluginfolder
        /// FIXME: Make CLS-compliant
        /// </summary>
        /// <param name="onlyenabled">True to only get the the plugins that are enable</param>
        /// <returns>A array with plugins</returns>
        public static IPlugin.IPlugin[] GetPlugins(bool onlyenabled)
        {
            System.Collections.Generic.List<IPlugin.IPlugin> pluginslist = new System.Collections.Generic.List<IPlugin.IPlugin>();
            if (Directory.Exists(Settings.ProgramPluginsFolder))
            {
                string[] pluginfiles = GetFilesPluginDir();
                string[] pluginsenabled = Settings.ProgramPluginsEnabled.Split('|'); // | is illegal as filename.
                plugindllexcluded = Settings.ProgramPluginsDllexclude.Split('|');
                for (int i = 0; i < pluginfiles.Length; i++)
                {
                    if (!IsPluginFilesExcluded(pluginfiles[i]))
                    {
                        try
                        {
                            // Get if plugin is enabled.
                            bool pluginenabled = IsPluginEnabled(pluginsenabled, pluginfiles[i]);
                            if ((pluginenabled && onlyenabled) || !onlyenabled)
                            {
                                System.Reflection.Assembly pluginassembly = null;
                                pluginassembly = System.Reflection.Assembly.LoadFrom(Path.Combine(Settings.ProgramPluginsFolder, pluginfiles[i]));
                                if (pluginassembly != null)
                                {
                                    foreach (Type curplugintype in pluginassembly.GetTypes())
                                    {
                                        if (curplugintype.IsPublic && !curplugintype.IsAbstract && !curplugintype.IsSealed)
                                        {
                                            Type plugintype = pluginassembly.GetType(curplugintype.ToString(), false, true);
                                            if (plugintype != null)
                                            {
                                                IPlugin.IPlugin iplugin = (IPlugin.IPlugin)Activator.CreateInstance(pluginassembly.GetType(curplugintype.ToString()));
                                                iplugin.Host = NoteFly.Program.notes;
                                                iplugin.Register(pluginenabled, pluginfiles[i]);
                                                pluginslist.Add(iplugin);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Write(LogType.exception, "Can't load plugin: " + pluginfiles[i] + " " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                Log.Write(LogType.info, "Plugin folder does not exist.");
            }

            return pluginslist.ToArray();
        }

        /// <summary>
        /// Get the name of the plugin.
        /// </summary>
        /// <param name="pluginassembly">The plugin assembly</param>
        /// <returns>The name of the plugin</returns>
        public static string GetPluginName(Assembly pluginassembly)
        {
            string pluginname = "untitled";
            object[] atttitle = pluginassembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            if (atttitle.Length > 0)
            {
                AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)atttitle[0];
                if (titleAttribute.Title != string.Empty)
                {
                    if (titleAttribute.Title.Length > 150)
                    {
                        pluginname = titleAttribute.Title.Substring(0, 150);
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
            string pluginauthor = "unknown";
            object[] attributes = pluginassembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            if (attributes.Length != 0)
            {
                if (!string.IsNullOrEmpty(((AssemblyCompanyAttribute)attributes[0]).Company))
                {
                    if (((AssemblyCompanyAttribute)attributes[0]).Company.Length > 150)
                    {
                        pluginauthor = ((AssemblyCompanyAttribute)attributes[0]).Company.Substring(0, 150);
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
        /// Get description of the plugin.
        /// </summary>
        /// <param name="pluginassembly">The plugin assembly</param>
        /// <returns>The description of the plugin</returns>
        public static string GetPluginDescription(Assembly pluginassembly)
        {
            string plugindescription = string.Empty;
            object[] attdesc = pluginassembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            if (attdesc.Length != 0)
            {
                if (((AssemblyDescriptionAttribute)attdesc[0]).Description.Length > 255)
                {
                    plugindescription = ((AssemblyDescriptionAttribute)attdesc[0]).Description.Substring(0, 255);
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
                return "unknown";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetIPluginVersion()
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
                Log.Write(LogType.exception, "File iplugin.dll not found.");
            }
            catch (FileLoadException)
            {
                Log.Write(LogType.exception, "Cannot load iplugin.dll");
            }

            return versionipluginstring;
        }
        /// <summary>
        /// Is the dll files excluded as plugin in the plugin directory.
        /// </summary>
        /// <param name="dllfilename">The dll filename without path</param>
        /// <returns>True if it's excluded</returns>
        private static bool IsPluginFilesExcluded(string dllfilename)
        {
            if (plugindllexcluded != null)
            {
                for (int i = 0; i < plugindllexcluded.Length; i++)
                {
                    if (dllfilename == plugindllexcluded[i])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Get all dll files in the plugin directory
        /// </summary>
        /// <returns>All dll filenames as string array</returns>
        private static string[] GetFilesPluginDir()
        {
            string[] pluginfilepaths = Directory.GetFiles(Settings.ProgramPluginsFolder, "*.dll", SearchOption.TopDirectoryOnly);
            string[] pluginfiles = new string[pluginfilepaths.Length];
            for (int i = 0; i < pluginfilepaths.Length; i++)
            {
                pluginfiles[i] = Path.GetFileName(pluginfilepaths[i]);
            }

            pluginfilepaths = null;
            return pluginfiles;
        }

        /// <summary>
        /// Get if plugin is enabled.
        /// </summary>
        /// <param name="pluginsenabled">A comma seperated list of enabled plugin assemblies</param>
        /// <param name="pluginfile">The filename without path of the plugin to be check if it's enabled.</param>
        /// <returns>true if pluginfile is enabled.</returns>
        private static bool IsPluginEnabled(string[] pluginsenabled, string pluginfile)
        {
            for (int p = 0; p < pluginsenabled.Length; p++)
            {
                if (pluginsenabled[p] == pluginfile)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
