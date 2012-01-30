// <copyright file="Strings.cs" company="NoteFly">
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
namespace NoteFly
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using System.Threading;

    public class Strings
    {
        private static Object resourceManLock = new Object();
        private static System.Resources.ResourceManager resourceMan;
        private static System.Globalization.CultureInfo resourceCulture;

        public const string ResourceName = "Strings";

        private static string resourcesDir = GetSetting("ResourcesDir", "translations");
        private static string fileFormat = GetSetting("ResourcesFileFormat", "{{culture}}/{{resource}}.po");

        private static string GetSetting(string setting, string defaultValue)
        {
            System.Collections.Specialized.NameValueCollection section = (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationManager.GetSection("appSettings");
            if (section == null) return defaultValue;
            else return section[setting] ?? defaultValue;
        }


        /// <summary>
        /// Resources directory used to retrieve files from.
        /// </summary>
        public static string ResourcesDirectory
        {
            get { return resourcesDir; }
            set { resourcesDir = value; }
        }

        /// <summary>
        /// Format of the file based on culture and resource name.
        /// </summary>
        public static string FileFormat
        {
            get { return fileFormat; }
            set { fileFormat = value; }
        }



        /// <summary>
        /// Returns the cached ResourceManager instance used by this class.
        /// </summary>
        public static System.Resources.ResourceManager ResourceManager
        {
            get
            {

                if (object.ReferenceEquals(resourceMan, null))
                {
                    lock (resourceManLock)
                    {
                        if (object.ReferenceEquals(resourceMan, null))
                        {
                            string directory = resourcesDir;
                            global::Gettext.Cs.GettextResourceManager mgr = new global::Gettext.Cs.GettextResourceManager(ResourceName, directory, fileFormat);
                            resourceMan = mgr;
                        }
                    }
                }

                return resourceMan;
            }
        }

        /// <summary>
        /// Overrides the current thread's CurrentUICulture property for all
        /// resource lookups using this strongly typed resource class.
        /// </summary>
        public static System.Globalization.CultureInfo Culture
        {
            get { return resourceCulture; }
            set { resourceCulture = value; }
        }

        /// <summary>
        /// Looks up a localized string; used to mark string for translation as well.
        /// </summary>
        public static string T(string t)
        {
            return T(null, t);
        }

        /// <summary>
        /// Looks up a localized string; used to mark string for translation as well.
        /// </summary>
        public static string T(CultureInfo info, string t)
        {
            if (String.IsNullOrEmpty(t)) return t;
            string translated = ResourceManager.GetString(t, info ?? resourceCulture);
            return String.IsNullOrEmpty(translated) ? t : translated;
        }

        /// <summary>
        /// Looks up a localized string and formats it with the parameters provided; used to mark string for translation as well.
        /// </summary>
        public static string T(string t, params object[] parameters)
        {
            return T(null, t, parameters);
        }

        /// <summary>
        /// Looks up a localized string and formats it with the parameters provided; used to mark string for translation as well.
        /// </summary>
        public static string T(CultureInfo info, string t, params object[] parameters)
        {
            if (String.IsNullOrEmpty(t)) return t;
            return String.Format(T(info, t), parameters);
        }

        /// <summary>
        /// Marks a string for future translation, does not translate it now.
        /// </summary>
        public static string M(string t)
        {
            return t;
        }

        /// <summary>
        /// Returns the resource set available for the specified culture.
        /// </summary>
        public static System.Resources.ResourceSet GetResourceSet(CultureInfo culture)
        {
            return ResourceManager.GetResourceSet(culture, true, true);
        }

        /// <summary>
        /// Translate all controls and toolstrip contextmenu of the given form.
        /// </summary>
        /// <param name="form"></param>
        public static void TranslateForm(System.Windows.Forms.Form form)
        {
            int controlnestedlevel = 1;
            TranslateControlCollection(form.Controls, controlnestedlevel); // main level in form
            if (form.ContextMenuStrip != null)
            {
                if (form.ContextMenuStrip.Items.Count > 0)
                {
                    TranslateToolStripItemCollection(form.ContextMenuStrip.Items);
                }
            }
        }
        
        /// <summary>
        /// Recusive method that translate all control in a ControlCollection.
        /// </summary>
        private static void TranslateControlCollection(System.Windows.Forms.Control.ControlCollection controlscollection, int controlnestedlevel)
        {
            const int MAXNESTEDCONTROL = 10;
            for (int i = 0; i < controlscollection.Count; i++)
            {
                if (IsTranslatableControl(controlscollection[i]))
                {
                    if (controlscollection[i].HasChildren)
                    {
                        // Just in case: recusive method is going mad, throw exception.                        
                        if (controlnestedlevel < MAXNESTEDCONTROL)
                        {
                            controlnestedlevel++;
                            TranslateControlCollection(controlscollection[i].Controls, controlnestedlevel);
                        }
                        else
                        {
                            throw new ApplicationException("Translateting controls more than " + MAXNESTEDCONTROL + " nested levels deep not allowed.");
                        }
                    }
                    else
                    {
                        string text = controlscollection[i].Text;
                        text = text.Replace("\"", "\\\""); // todo for performance, dont use quotes at all
                        if (!String.IsNullOrEmpty(text))
                        {
                            string translation = Strings.T(text);
                            if (!String.IsNullOrEmpty(translation))
                            {
                                controlscollection[i].Text = translation;
                            }

#if DEBUG
                            AddToPOT(text, controlscollection[i].Name);
#endif
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Translate the toolstripitemcollection.
        /// </summary>
        /// <param name="collection"></param>
        private static void TranslateToolStripItemCollection(System.Windows.Forms.ToolStripItemCollection toolstripitemcollection)
        {
            for (int i = 0; i < toolstripitemcollection.Count; i++)
            {
                string text = toolstripitemcollection[i].Text;
                //text = text.Replace("\"", "\\\""); // todo for performance, dont use quotes at all
                if (!String.IsNullOrEmpty(text))
                {
                    string translation = Strings.T(text);
                    if (!String.IsNullOrEmpty(translation))
                    {
                        toolstripitemcollection[i].Text = translation;
                    }

#if DEBUG
                    AddToPOT(text, toolstripitemcollection[i].Name);
#endif
                }
            }
        }

        /// <summary>
        /// Get if the control should be translated.
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        private static bool IsTranslatableControl(System.Windows.Forms.Control control)
        {
            bool translatecontrol = false;
            Type controltype = control.GetType();
            // whitelist type control
            if (
                controltype != typeof(System.Windows.Forms.NumericUpDown) &&
                controltype != typeof(System.Windows.Forms.TextBox) &&
                controltype != typeof(System.Windows.Forms.ComboBox) &&
                controltype != typeof(System.Windows.Forms.DataGridView) &&
                controltype != typeof(System.Windows.Forms.PictureBox) &&
                controltype != typeof(System.Windows.Forms.CheckedListBox) &&
                controltype != typeof(NoteFly.TransparentRichTextBox) &&
                controltype != typeof(NoteFly.PluginGrid) 
                )
            {
                // blacklist name control
                if (control.Name != "btnKeywordClear" && 
                    control.Name != "btnClose" &&
                    control.Name != "btnTextBold" &&
                    control.Name != "btnTextItalic" &&
                    control.Name != "btnTextUnderline" &&
                    control.Name != "btnTextStriketrough" &&
                    control.Name != "btnTextBulletlist" &&
                    control.Name != "btnFontBigger" &&
                    control.Name != "btnFontSmaller" &&
                    control.Name != "btnHideNote" &&
                    control.Name != "lblTitle"
                    )
                {
                    translatecontrol = true;
                }
            }

            return translatecontrol;
        }

#if DEBUG
        private static void AddToPOT(string text, string controlname)
        {
            string filepathpot = Path.Combine(Path.Combine(Program.InstallFolder, @".\..\"), "Strings.pot");
            text = text.Replace("\r\n", "\"\r\nmsgid \""); // multiple lines are split is multiple msgid's.
            string msgid = new StringBuilder("msgid \"").Append(text).Append("\"").ToString();
            bool isalreadyadded = false;
            if (File.Exists(filepathpot))
            {
                StreamReader reader = null;
                try
                {
                    reader = new StreamReader(filepathpot, Encoding.UTF8);
                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        if (line == msgid)
                        {
                            isalreadyadded = true;
                            break;
                        }

                        line = reader.ReadLine();
                    }
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                }


                if (!isalreadyadded)
                {
                    StreamWriter writer = null;
                    try
                    {
                        writer = new StreamWriter(filepathpot, true, Encoding.UTF8);
                        StringBuilder potfilepart = new StringBuilder();
                        potfilepart.AppendLine();
                        potfilepart.Append("# ");
                        potfilepart.AppendLine(controlname);
                        potfilepart.AppendLine(msgid);
                        potfilepart.AppendLine("msgstr \"\"");
                        writer.Write(potfilepart.ToString());
                    }
                    finally
                    {
                        if (writer != null)
                        {
                            writer.Close();
                        }
                    }
                }
            }
            else
            {
                throw new ApplicationException("Please run build_translationfile.bat first.");
            }

        }
#endif
    }
}

