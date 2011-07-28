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
    using System.Collections.Generic;
    using System.Text;
    using System.Reflection;

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

        public abstract string SettingsTabTitle { get; }

        public abstract string ShareMenuText { get; }

		// Methods (1) 

        public abstract void ShareMenuClicked(System.Windows.Forms.RichTextBox rtbnote, NoteFly.Note note);
    }
}
