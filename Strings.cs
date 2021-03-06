//-----------------------------------------------------------------------
// <copyright file="Strings.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2012-2015  Tom
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

        /// <summary>
        /// Lock object to make only one ResourceManager being accessed at the same time.
        /// </summary>
        private static object resourceManLock = new object();

        /// <summary>
        /// Resource manager
        /// </summary>
        private static System.Resources.ResourceManager resourceMan;

        /// <summary>
        /// CultureInfo object
        /// </summary>
        private static System.Globalization.CultureInfo resourceCulture;

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
                            global::Gettext.Cs.GettextResourceManager gettextresourcemgr = new global::Gettext.Cs.GettextResourceManager(ResourceName, directory, FILEFORMAT);
                            resourceMan = gettextresourcemgr;
                        }
                    }
                }

                return resourceMan;
            }
        }

        /// <summary>
        /// Looks up a localized string; used to mark string for translation as well.
        /// </summary>
        /// <param name="text">The string to translate.</param>
        /// <returns>A translated string</returns>
        public static string T(string text)
        {
            return T(null, text);
        }

        /// <summary>
        /// Looks up a localized string; used to mark string for translation as well.
        /// </summary>
        /// <param name="cultureInfo">CultureInfo object.</param>
        /// <param name="text">The string to translate.</param>
        /// <returns>An translated string.</returns>
        public static string T(CultureInfo cultureInfo, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            string translated = ResourceManager.GetString(text, cultureInfo ?? resourceCulture);
            return string.IsNullOrEmpty(translated) ? text : translated;
        }

        /// <summary>
        /// Looks up a localized string and formats it with the parameters provided; used to mark string for translation as well.
        /// </summary>
        /// <param name="text">The string to translate.</param>
        /// <param name="parameters">Parameters to be placed in the string.</param>
        /// <returns>An translated string.</returns>
        public static string T(string text, params object[] parameters)
        {
            return T(null, text, parameters);
        }

        /// <summary>
        /// Looks up a localized string and formats it with the parameters provided; used to mark string for translation as well.
        /// </summary>
        /// <param name="info">CultureInfo object.</param>
        /// <param name="text">The string to translate.</param>
        /// <param name="parameters">Parameters in the string.</param>
        /// <returns>The translated string with orginal parameters.</returns>
        public static string T(CultureInfo info, string text, params object[] parameters)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            return string.Format(T(info, text), parameters);
        }

        /// <summary>
        /// Marks a string for future translation, does not translate it now.
        /// </summary>
        /// <param name="text">The string to mark for future translation</param>
        /// <returns>Orignal string</returns>
        public static string M(string text)
        {
            return text;
        }

        /// <summary>
        /// Returns the resource set available for the specified culture.
        /// </summary>
        /// <param name="culture">CultureInfo object</param>
        /// <returns>ResourceSet of culture</returns>
        public static System.Resources.ResourceSet GetResourceSet(CultureInfo culture)
        {
            return ResourceManager.GetResourceSet(culture, true, true);
        }

        /// <summary>
        /// Translate all controls and toolstrip contextmenu of the given form.
        /// </summary>
        /// <param name="form">The form to translate</param>
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
        /// <param name="controlscollection">Collection of controls to translate.</param>
        /// <param name="controlnestedlevel">Depth level of the control on the form.</param>
        private static void TranslateControlCollection(System.Windows.Forms.Control.ControlCollection controlscollection, int controlnestedlevel)
        {
            const int MAXNESTEDCONTROL = 15;
            for (int i = 0; i < controlscollection.Count; ++i)
            {
                if (!IsTranslatableControl(controlscollection[i]))
                {
                    continue;
                }

                if (controlscollection[i].HasChildren)
                {
                    if (controlscollection[i].GetType() == typeof(System.Windows.Forms.TabPage))
                    {
                        controlscollection[i].Text = GetTranslationControl(controlscollection[i].Text, controlscollection[i].Name);
                    }

                    // Just in case: recusive method is going mad, throw an exception.
                    if (controlnestedlevel > MAXNESTEDCONTROL)
                    {
                        string excnestedtoodeep = Strings.T("translating error: cannot translate controls more than {0} nested levels deep.", MAXNESTEDCONTROL);
                        throw new ApplicationException(excnestedtoodeep);
                    }
                    else
                    {
                        controlnestedlevel++;
                        TranslateControlCollection(controlscollection[i].Controls, controlnestedlevel);
                    }
                }
                else
                {
                    controlscollection[i].Text = GetTranslationControl(controlscollection[i].Text, controlscollection[i].Name);
                }
            }
        }

        /// <summary>
        /// Translate the toolstripitemcollection.
        /// </summary>
        /// <param name="toolstripitemcollection">Collection of ToolStripItems to translate.</param>
        private static void TranslateToolStripItemCollection(System.Windows.Forms.ToolStripItemCollection toolstripitemcollection)
        {
            for (int i = 0; i < toolstripitemcollection.Count; ++i)
            {
                System.Windows.Forms.ToolStripMenuItem toolStripMenuItem = (System.Windows.Forms.ToolStripMenuItem)toolstripitemcollection[i];
                toolStripMenuItem.Text = GetTranslationControl(toolstripitemcollection[i].Text, toolstripitemcollection[i].Name);
                if (toolStripMenuItem.DropDownItems.Count > 0)
                {
                    // translate subitems:
                    for (int n = 0; n < toolStripMenuItem.DropDownItems.Count; ++n)
                    {
                        ////Log.Write(LogType.info, "found subitem: " + toolStripMenuItem.DropDownItems[n].Text);
                        toolStripMenuItem.DropDownItems[n].Text = GetTranslationControl(toolStripMenuItem.DropDownItems[n].Text, toolStripMenuItem.DropDownItems[n].Name);
                    }
                }
            }
        }

        /// <summary>
        /// Get a translation for a particulair control.
        /// </summary>
        /// <param name="untranslatedtext">Original text</param>
        /// <param name="controlname">Control name</param>
        /// <returns>The text translation of the text properties of the control.</returns>
        private static string GetTranslationControl(string untranslatedtext, string controlname)
        {
            if (string.IsNullOrEmpty(untranslatedtext))
            {
                return untranslatedtext;
            }

            string translationtext = Strings.T(untranslatedtext);
            if (string.IsNullOrEmpty(translationtext))
            {
                return untranslatedtext;
            }

#if DEBUG
            AddToPOT(untranslatedtext, controlname);
#endif
            return translationtext;
        }

        /// <summary>
        /// Get if the control should be translated.
        /// </summary>
        /// <param name="control">Control to check name and type from</param>
        /// <returns>True if control text should be translated</returns>
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
                controltype == typeof(System.Windows.Forms.TableLayoutPanel) ||
                controltype == typeof(System.Windows.Forms.Panel) ||
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
                    control.Name != "btnHideNote" &&
                    control.Name != "lblNoteTitle" &&
                    control.Name != "lblProductName" &&
                    control.Name != "lblProductVersion")
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
        /// <param name="text">Text to be translated.</param>
        /// <param name="controlname">Controlname of the control containing the text.</param>
        private static void AddToPOT(string text, string controlname)
        {
            string filepathpot = System.IO.Path.Combine(System.IO.Path.Combine(Program.InstallFolder, @".\..\"), "Strings.pot");
            text = text.Replace("\n", "\"\nmsgid \""); // multiple lines are split is multiple msgid's.
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
                        Log.Write(LogType.info, "POT add: " + msgid);
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
                throw new ApplicationException("Please run build_translationfile.bat or build_translationfile.sh first.");
            }
        }
#endif
    }
}