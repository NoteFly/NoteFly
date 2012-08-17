//-----------------------------------------------------------------------
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
//-----------------------------------------------------------------------
namespace NoteFly
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Strings class, used to lookup translations with GNU Gettext
    /// </summary>
    public static class Strings
    {
        /// <summary>
        /// 
        /// </summary>
        private static object resourceManLock = new object();

        /// <summary>
        /// Resource manager
        /// </summary>
        private static System.Resources.ResourceManager resourceMan;

        /// <summary>
        /// 
        /// </summary>
        private static System.Globalization.CultureInfo resourceCulture;

        /// <summary>
        /// The po filename without extension
        /// </summary>
        public const string ResourceName = "Strings";

        /// <summary>
        /// The folder where all translations are stored in.
        /// </summary>
        public const string RESOURCESDIR = "translations";

        /// <summary>
        /// Path to resource within the transation folder.
        /// </summary>
        private const string FILEFORMAT = "{{culture}}/{{resource}}.po";

        /*
        /// <summary>
        /// Gets or sets Resources directory used to retrieve files from.
        /// </summary>
        public static string ResourcesDirectory
        {
            get { return resourcesDir; }
            set { resourcesDir = value; }
        }

        /// <summary>
        /// Gets or sets Format of the file based on culture and resource name.
        /// </summary>
        public static string FileFormat
        {
            get { return fileFormat; }
            set { fileFormat = value; }
        }
        */

        /// <summary>
        /// Gets or sets the current thread's CurrentUICulture property for all
        /// resource lookups using this strongly typed resource class.
        /// </summary>
        public static System.Globalization.CultureInfo Culture
        {
            get { return resourceCulture; }
            set { resourceCulture = value; }
        }

        /// <summary>
        /// Gets the cached ResourceManager instance used by this class.
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
                            string directory = RESOURCESDIR;
                            global::Gettext.Cs.GettextResourceManager mgr = new global::Gettext.Cs.GettextResourceManager(ResourceName, directory, FILEFORMAT);
                            resourceMan = mgr;
                        }
                    }
                }

                return resourceMan;
            }
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
            if (string.IsNullOrEmpty(t)) return t;
            string translated = ResourceManager.GetString(t, info ?? resourceCulture);
            return string.IsNullOrEmpty(translated) ? t : translated;
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
            if (string.IsNullOrEmpty(t)) return t;
            return string.Format(T(info, t), parameters);
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
            const int MAXNESTEDCONTROL = 12;
            for (int i = 0; i < controlscollection.Count; i++)
            {
                if (IsTranslatableControl(controlscollection[i]))
                {
                    if (controlscollection[i].HasChildren)
                    {
                        if (controlscollection[i].GetType() == typeof(System.Windows.Forms.TabPage))
                        {
                            controlscollection[i].Text = GetTranslationControl(controlscollection[i].Text, controlscollection[i].Name);
                        }

                        // Just in case: recusive method is going mad, throw an exception.
                        if (controlnestedlevel <= MAXNESTEDCONTROL)
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
                        controlscollection[i].Text = GetTranslationControl(controlscollection[i].Text, controlscollection[i].Name);
                    }
                }
            }
        }

        /// <summary>
        /// Translate the toolstripitemcollection.
        /// </summary>
        /// <param name="toolstripitemcollection"></param>
        private static void TranslateToolStripItemCollection(System.Windows.Forms.ToolStripItemCollection toolstripitemcollection)
        {
            for (int i = 0; i < toolstripitemcollection.Count; i++)
            {
                toolstripitemcollection[i].Text = GetTranslationControl(toolstripitemcollection[i].Text, toolstripitemcollection[i].Name);
            }
        }

        /// <summary>
        /// Get a translation for a particulair control.
        /// </summary>
        /// <param name="text">Orginele text</param>
        /// <param name="ctrlname">Control name</param>
        /// <returns></returns>
        private static string GetTranslationControl(string text, string ctrlname)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = text.Replace("\"", "\\\"");
                string translation = Strings.T(text);
#if DEBUG
                AddToPOT(text, ctrlname);
#endif
                if (!string.IsNullOrEmpty(translation))
                {
                    return translation;
                }
            }

            return text;
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

            // whitelist control type
            if (controltype == typeof(System.Windows.Forms.Label) ||
                controltype == typeof(System.Windows.Forms.ToolStripMenuItem) ||
                controltype == typeof(System.Windows.Forms.Button) ||
                controltype == typeof(System.Windows.Forms.CheckBox) ||
                controltype == typeof(System.Windows.Forms.TabPage) ||
                controltype == typeof(System.Windows.Forms.ToolTip) ||
                controltype == typeof(System.Windows.Forms.LinkLabel) ||
                controltype == typeof(System.Windows.Forms.TabControl) ||
                controltype == typeof(SearchTextBox))
            {
                // blacklist control name
                if (control.Name != "btnKeywordClear" && 
                    control.Name != "btnClose" &&
                    control.Name != "btnTextBold" &&
                    control.Name != "btnTextItalic" &&
                    control.Name != "btnTextUnderline" &&
                    control.Name != "btnTextStriketrough" &&
                    control.Name != "btnTextBulletlist" &&
                    control.Name != "btnFontBigger" &&
                    control.Name != "btnFontSmaller" &&
                    control.Name != "btnHideNote")
                {
                    translatecontrol = true;
                }
            }

            return translatecontrol;
        }

#if DEBUG
        /// <summary>
        /// Add translation with control comment to pot file / translation template.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="controlname"></param>
        private static void AddToPOT(string text, string controlname)
        {
            string filepathpot = System.IO.Path.Combine(System.IO.Path.Combine(Program.InstallFolder, @".\..\"), "Strings.pot");
            text = text.Replace("\r\n", "\"\r\nmsgid \""); // multiple lines are split is multiple msgid's.
            string msgid = new System.Text.StringBuilder("msgid \"").Append(text).Append("\"").ToString();
            bool isalreadyadded = false;
            if (System.IO.File.Exists(filepathpot))
            {
                System.IO.StreamReader reader = null;
                try
                {
                    reader = new System.IO.StreamReader(filepathpot, System.Text.Encoding.UTF8);
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
                    System.IO.StreamWriter writer = null;
                    try
                    {
                        writer = new System.IO.StreamWriter(filepathpot, true, System.Text.Encoding.UTF8);
                        System.Text.StringBuilder potfilepart = new System.Text.StringBuilder();
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