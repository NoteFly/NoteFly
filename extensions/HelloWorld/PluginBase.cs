//-----------------------------------------------------------------------
// <copyright file="PluginBase.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2011  Tom
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
namespace HelloWorld
{
    using System;
    using System.Reflection;
    using System.Windows.Forms;

    public abstract class PluginBase : NoteFly.IPlugin
    {
		// Properties (6) 

        /// <summary>
        /// The name of this plugin
        /// </summary>
        public string Name
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        /// <summary>
        /// The author of this plugin
        /// </summary>
        public string Author
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        /// <summary>
        /// The description of this plugin
        /// </summary>
        public string Description
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        /// <summary>
        /// The version of this plugin
        /// </summary>
        public string Version
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.Major + "." + Assembly.GetExecutingAssembly().GetName().Version.Minor;
            }
        }

        /// <summary>
        /// Settings share tab title.
        /// Tab not created if null.
        /// </summary>
        public virtual string SettingsTabTitle
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Note share to menu text.
        /// MenuItem not created if null.
        /// </summary>
        public virtual string ShareMenuText
        {
            get
            {
                return null;
            }
        }

		// Methods (2) 

        public virtual void ShareMenuClicked(System.Windows.Forms.RichTextBox rtbnote, NoteFly.Note note)
        {
            // by default  do nothing, override this to do someting.
        }

        public virtual TabPage InitShareSettingsTab()
        {
            // by default return nocontrols, override this to create settings share tab contriols
            return null;
        }
    }
}
