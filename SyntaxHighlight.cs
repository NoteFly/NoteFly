//-----------------------------------------------------------------------
// <copyright file="SyntaxHighlight.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2010-2012  Tom
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
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Highlight class, provides highlighting to richtext.
    /// </summary>
    public sealed class SyntaxHighlight
    {
        #region Fields (9)

        /// <summary>
        /// The filename with xml description of languages keywords / lexicon 
        /// </summary>
        private const string LANGFILE = "langs.xml";

        /// <summary>
        /// List with languages description how to highlight them.
        /// </summary>
        private static List<HighlightLanguage> langs;

        /// <summary>
        /// Is the highlighter in a comment part.
        /// </summary>
        private static bool comment = false;

        /// <summary>
        /// Is the highlighter on a comment line.
        /// </summary>
        private static bool commentline = false;

        /// <summary>
        /// Is the highlighter outside of a HTML part in mixed document.
        /// </summary>
        private static bool outerhtml = true;

        /// <summary>
        /// Is ths highlighter on a HTML string part.
        /// </summary>
        private static bool htmlstringpart = false;

        /// <summary>
        /// Is the highlighter on a PHP string part. 
        /// </summary>
        private static bool phpstringpart = false;

        /// <summary>
        /// What is the character (usually quote) used to start the string part with.
        /// </summary>
        private static char currentstringquote = '"';

        /// <summary>
        /// Are keyword initilized
        /// </summary>
        private static bool keywordsinit = false;

        #endregion Fields

        #region Properties (1)

        /// <summary>
        /// Gets a value indicating whether highlighting keywords are initialized.
        /// </summary>
        public static bool KeywordsInitialized
        {
            get
            {
                return keywordsinit;
            }
        }

        #endregion Properties

        #region Methods (8)

        /// <summary>
        /// Clear the keywords list.
        /// </summary>
        public static void DeinitHighlighter()
        {
            keywordsinit = false;
            GC.Collect();
        }

        /// <summary>
        /// Initializes TextHighlighter fill the keywords lists.
        /// </summary>
        public static void InitHighlighter()
        {
            langs = new List<HighlightLanguage>();
            if (Settings.HighlightHTML)
            {
                langs.Add(xmlUtil.ParserLanguageLexical(LANGFILE, "html"));
            }

            if (Settings.HighlightPHP)
            {
                langs.Add(xmlUtil.ParserLanguageLexical(LANGFILE, "php"));
            }

            if (Settings.HighlightSQL)
            {
                langs.Add(xmlUtil.ParserLanguageLexical(LANGFILE, "sql"));
            }

            keywordsinit = true;
        }

        /// <summary>
        /// Check the syntax of alle set languages on the RichTextbox RTF content.
        /// </summary>
        /// <param name="rtb">The richttextbox with RTF note content.</param>
        /// <param name="skinnr">The skin number</param>
        /// <param name="notes">Pointer to notes class.</param>
        public static void CheckSyntaxFull(RichTextBox rtb, int skinnr, Notes notes)
        {
            int cursorpos = rtb.SelectionStart;
            int sellen = rtb.SelectionLength;
            ResetHighlighting(rtb, skinnr, notes);

            if (!keywordsinit)
            {
                Log.Write(LogType.error, "Keywords not initialized as they should already have. Hotfixing this, watchout memory use.");
                InitHighlighter();
            }

            // check if highlighting is enabled at all.
            if (langs.Count > 0)
            {
                int lastpos = 0;
                int maxpos = Settings.HighlightMaxchars;
                if (rtb.TextLength < Settings.HighlightMaxchars)
                {
                    maxpos = rtb.TextLength;
                }

                foreach (HighlightLanguage lang in langs)
                {
                    lang.PosDocumentStart = int.MaxValue;
                    lang.PosDocumentEnd = int.MaxValue;
                }

                for (int curpos = 0; curpos < maxpos; curpos++)
                {
#if !macos
                    if (rtb.Text[curpos] == ' ' || rtb.Text[curpos] == '\n' || curpos == rtb.Text.Length - 1)
#elif macos
                    if (rtb.Text[curpos] == ' ' || rtb.Text[curpos] == '\r' || curpos == rtb.Text.Length-1)
#endif
                    {
                        string bufcheck = rtb.Text.Substring(lastpos, curpos - lastpos);
                        if (bufcheck.Length > 0)
                        {
                            for (int i = 0; i < langs.Count; i++)
                            {
                                langs[i].CheckSetDocumentPos(bufcheck, curpos);
                                if (curpos >= langs[i].PosDocumentStart && curpos <= langs[i].PosDocumentEnd)
                                {
                                    switch (langs[i].Name)
                                    {
                                        case "html":
                                            ValidatingHtmlPart(bufcheck, rtb, lastpos, langs[i]);
                                            break;
                                        case "php":
                                            ValidatingPhpPart(bufcheck, rtb, lastpos, langs[i]);
                                            break;
                                        case "sql":
                                            ValidatingSqlPart(bufcheck, rtb, lastpos, langs[i]);
                                            break;
                                    }
                                }
                            }
                        }

                        lastpos = curpos + 1; // without space or linefeed
                    }

#if !macos
                    if (rtb.Text[curpos] == '\n')
#elif macos
                    if (rtb.Text[curpos] == '\r')
#endif
                    {
                        commentline = false;
                    }
                }
            }

            rtb.SelectionStart = cursorpos;
            rtb.SelectionLength = sellen;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="skinnr"></param>
        /// <param name="notes"></param>
        public static void CheckSyntaxQuick(RichTextBox rtb, int skinnr, Notes notes)
        {
            int cursorpos = rtb.SelectionStart;
            //int sellen = rtb.SelectionLength;

            if (!keywordsinit)
            {
                Log.Write(LogType.error, "Keywords not initialized as they should already have. Hotfixing this, watchout memory use.");
                InitHighlighter();
            }

            // check if highlighting is enabled at all.
            if (langs.Count > 0)
            {
                int maxpos = Settings.HighlightMaxchars;
                if (rtb.TextLength < Settings.HighlightMaxchars)
                {
                    maxpos = rtb.TextLength;
                }

                int lastspace = int.MaxValue;
                for (int i = cursorpos - 2; i > 0; i--)
                {
                    if (rtb.Text[i] == ' ' || rtb.Text[i] == '\n')
                    {
                        lastspace = i + 1;
                        break;
                    }
                }
        
                for (int curpos = lastspace; curpos < maxpos; curpos++)
                {
#if !macos
                    if (rtb.Text[curpos] == ' ' || rtb.Text[curpos] == '\n' || curpos == rtb.Text.Length - 1)
#elif macos
                    if (rtb.Text[curpos] == ' ' || rtb.Text[curpos] == '\r' || curpos == rtb.Text.Length-1)
#endif
                    {
                        string bufcheck = rtb.Text.Substring(lastspace, curpos - lastspace);
                        if (bufcheck.Length > 0)
                        {
                            for (int i = 0; i < langs.Count; i++)
                            {
                                langs[i].CheckSetDocumentPos(bufcheck, curpos);
                                if (curpos >= langs[i].PosDocumentStart && curpos <= langs[i].PosDocumentEnd)
                                {
                                    switch (langs[i].Name)
                                    {
                                        case "html":
                                            ValidatingHtmlPart(bufcheck, rtb, lastspace, langs[i]);
                                            break;
                                        case "php":
                                            ValidatingPhpPart(bufcheck, rtb, lastspace, langs[i]);
                                            break;
                                        case "sql":
                                            ValidatingSqlPart(bufcheck, rtb, lastspace, langs[i]);
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Color some part of the rich edit text.
        /// </summary>
        /// <param name="rtb">The richtextbox contain the rtf text to apply coloring on.</param>
        /// <param name="posstart">The start position in the text to start coloring from.</param>
        /// <param name="len">The lenght of text to color.</param>
        /// <param name="hexcolor">The color the text should get.</param>
        private static void ColorText(RichTextBox rtb, int posstart, int len, string hexcolor)
        {
            Color color = xmlUtil.ConvToClr(hexcolor);
            try
            {
                rtb.Select(posstart, len);
                rtb.SelectionColor = color;
            }
            catch (ArgumentOutOfRangeException arg)
            {
                throw new ApplicationException("TextHighlighter out of range: " + arg.Source);
            }
        }

        /// <summary>
        /// Make the whole text the default font color.
        /// </summary>
        /// <param name="rtb">The richedit control that hold the note content.</param>
        /// <param name="skinnr">The skin number of the current note.</param>
        /// <param name="notes">Reference to notes class.</param>
        private static void ResetHighlighting(RichTextBox rtb, int skinnr, Notes notes)
        {
            rtb.SelectAll();
            rtb.SelectionColor = notes.GetTextClr(skinnr);
            rtb.Select(0, 0);
            rtb.ForeColor = notes.GetTextClr(skinnr);
            comment = false;
            commentline = false;
            outerhtml = true;
            htmlstringpart = false;
            phpstringpart = false;
            currentstringquote = '"';
        }

        /// <summary>
        /// Highlight some text part on html split by spaces.
        /// </summary>
        /// <param name="ishtml">string without spaces. length needs to be >0</param>
        /// <param name="rtb">The richtextbox.</param>
        /// <param name="posstartpart">the start position in the richtextbox.</param>
        /// <param name="langhtml">The html language description.</param>
        private static void ValidatingHtmlPart(string ishtml, RichTextBox rtb, int posstartpart, HighlightLanguage langhtml)
        {
            ishtml = ishtml.ToLower();
            List<string> attributes = new List<string>(); // these attributes are within this part.
            List<int> attributesstartpos = new List<int>();
            int attrstartpos = posstartpart;
            int attrlen = 0;
            bool attrstartposset = false;

            if (ishtml.StartsWith(langhtml.Commentstart, StringComparison.Ordinal))
            {
                comment = true;
            }
            ////else if (ishtml.Equals(langhtml.Commentline))
            ////{
            ////    commentline = true;
            ////}

            if (!comment)
            {
                for (int c = 0; c < ishtml.Length; c++)
                {
                    if (htmlstringpart)
                    {
                        ColorText(rtb, posstartpart + c, 1, Settings.HighlightHTMLColorString); // '"' or '\'' itself too.
                        if (ishtml[c] == currentstringquote)
                        {
                            htmlstringpart = false;
                        }
                    }
                    else
                    {
                        if (ishtml[c] == '<')
                        {
                            if (!attrstartposset)
                            {
                                attrstartpos = posstartpart + c + 1; // without <
                                attributesstartpos.Add(attrstartpos);
                                attrstartposset = true;
                            }

                            outerhtml = false;
                        }
                        else if (ishtml[c] == '>')
                        {
                            if (!outerhtml)
                            {
                                if (attrlen > 0)
                                {
                                    attributes.Add(rtb.Text.Substring(attrstartpos, attrlen));
                                    attrlen = 0;
                                }

                                attrstartposset = false;
                            }

                            outerhtml = true;
                        }
                        else
                        {
                            if (!outerhtml)
                            {
                                if (!attrstartposset)
                                {
                                    attrstartpos = posstartpart + c;
                                    attributesstartpos.Add(attrstartpos);
                                    attrstartposset = true;
                                }

                                attrlen++;

                                if (c == ishtml.Length - 1)
                                {
                                    attributes.Add(rtb.Text.Substring(attrstartpos, attrlen));
                                    attrlen = 0;
                                }
                            }
                        }
                    }
                }

                for (int nattr = 0; nattr < attributes.Count; nattr++)
                {
                    for (int i = 0; i < nattr; i++)
                    {
                        attrstartpos += attributes[i].Length;
                    }

                    if (nattr < attributesstartpos.Count)
                    {
                        ValidateHTMLAttribute(attributes[nattr], rtb, attributesstartpos[nattr], langhtml);
                    }
                }
            }
            else
            {
                // is comment
                ColorText(rtb, posstartpart, ishtml.Length, Settings.HighlightHTMLColorComment);
            }

            if (ishtml.EndsWith(langhtml.Commentend, StringComparison.Ordinal))
            {
                comment = false;
            }
        }

        /// <summary>
        /// Validate HTML attribute
        /// </summary>
        /// <param name="htmlattribute">Attribute of the HTML node to validate</param>
        /// <param name="rtb">Richtextbox with note content</param>
        /// <param name="attributestartpos">Startposition of the attribute within the htmlpart.</param>
        /// <param name="langhtml">The html language description.</param>
        private static void ValidateHTMLAttribute(string htmlattribute, RichTextBox rtb, int attributestartpos, HighlightLanguage langhtml)
        {
            if (htmlattribute == "/" || htmlattribute.Length < 1)
            {
                return;
            }

            char[] chrs = new char[] { '=' };
            string[] attrsepnamevaleau = htmlattribute.Split(chrs, 2);
            bool knowattr = false;
            string attrname = attrsepnamevaleau[0];

            if (attrname.Length > 0)
            {
                if (attrname[0] == '/')
                {
                    attrname = attrname.Remove(0, 1);
                }
            }
            else
            {
                return;
            }

            if (langhtml.FindKeyword(attrname.ToLower()))
            {
                knowattr = true;
            }

            if (!knowattr)
            {
                // Wrong
                ColorText(rtb, attributestartpos, attrsepnamevaleau[0].Length, Settings.HighlightHTMLColorInvalid);
            }
            else
            {
                for (int i = 0; i < attrsepnamevaleau.Length; i++)
                {
                    if (i == 0)
                    {
                        if (knowattr)
                        {
                            // Good
                            ColorText(rtb, attributestartpos, attrsepnamevaleau[0].Length, Settings.HighlightHTMLColorValid);
                        }
                    }
                    else if (i == 1)
                    {
                        if (attrsepnamevaleau[1].StartsWith("\"") || attrsepnamevaleau[1].StartsWith("'"))
                        {
                            // is string
                            htmlstringpart = true;
                            int posstartstring = attributestartpos + attrsepnamevaleau[0].Length + 1; // +1 for '=' 
                            ColorText(rtb, posstartstring, attrsepnamevaleau[1].Length, Settings.HighlightHTMLColorString);
                            currentstringquote = attrsepnamevaleau[1][0];
                            int lastcharpos = attrsepnamevaleau[1].Length - 1;
                            if (attrsepnamevaleau[1][lastcharpos] == currentstringquote)
                            {
                                htmlstringpart = false;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Find out if it is a php keyword.
        /// </summary>
        /// <param name="isphp">A part to be check if this a php keyword.</param>
        /// <param name="rtb">the richtextbox</param>
        /// <param name="posstart">the position in rtb where this keyword starts</param>
        /// <param name="langphp">The PHP language description.</param>
        private static void ValidatingPhpPart(string isphp, RichTextBox rtb, int posstart, HighlightLanguage langphp)
        {
            int posvar = -1;
            if (isphp.StartsWith(langphp.Commentstart))
            {
                comment = true;
            }

            if (commentline || comment)
            {
                ColorText(rtb, posstart, isphp.Length, Settings.HighlightPHPColorComment);
            }
            else
            {
                for (int curchr = 0; curchr < isphp.Length; curchr++)
                {
                    if (isphp[curchr] == '$')
                    {
                        // is variable
                        posvar = curchr;
                    }
                    else if (isphp[curchr] == '/')
                    {
                        if (curchr > 0)
                        {
                            if (isphp[curchr - 1] == '/')
                            {
                                // is commentline
                                commentline = true;
                                ColorText(rtb, posstart + curchr - 1, isphp.Length - curchr + 1, Settings.HighlightPHPColorComment);
                            }
                        }
                    }
                    else if (isphp[curchr] == '"' || isphp[curchr] == '\'')
                    {
                        bool escaped = false;
                        if (curchr > 0)
                        {
                            if (isphp[curchr - 1] == '\\')
                            {
                                escaped = true;
                            }
                        }

                        if (!escaped)
                        {
                            phpstringpart = !phpstringpart;
                            if (!phpstringpart)
                            {
                                ColorText(rtb, posstart + curchr, 1, Settings.HighlightPHPColorString);
                                foreach (HighlightLanguage lng in langs)
                                {
                                    if (lng.Name == "sql")
                                    {
                                        lng.PosDocumentEnd = posstart + curchr + 1;
                                    }
                                }
                            }
                            else
                            {
                                foreach (HighlightLanguage lng in langs)
                                {
                                    if (lng.Name == "sql")
                                    {
                                        lng.PosDocumentStart = posstart;
                                    }
                                }
                            }
                        }
                    }
                    else if ((isphp[curchr] < 48 || isphp[curchr] > 57) && (isphp[curchr] < 65 || isphp[curchr] > 122))
                    {
                        if (isphp[curchr] != '\"' && isphp[curchr] != '\"')
                        {
                            if (posvar >= 0)
                            {
                                ColorText(rtb, posstart + posvar, curchr - posvar, Settings.HighlightPHPColorDocumentstartend); // is variable
                                posvar = -1;
                            }
                        }
                    }
                    else if (curchr == isphp.Length - 1)
                    {
                        if (posvar >= 0)
                        {
                            // is variable
                            ColorText(rtb, posstart + posvar, isphp.Length, Settings.HighlightPHPColorDocumentstartend);
                        }
                    }

                    if (phpstringpart)
                    {
                        ColorText(rtb, posstart + curchr, 1, Settings.HighlightPHPColorString);
                    }
                }

                // check if keyword is known php function
                if (langphp.FindKeyword(isphp))
                {
                    ColorText(rtb, posstart, isphp.Length, Settings.HighlightPHPColorValidfunctions);
                }
            }

            if (isphp.EndsWith(langphp.Commentend))
            {
                comment = false;
            }
        }

        /// <summary>
        /// Find out if it is a sql keyword.
        /// </summary>
        /// <param name="issql">The part to be check.</param>
        /// <param name="rtb">The richtextbox</param>
        /// <param name="posstart">Position where the keyword starts in the richtextbox.</param>
        /// <param name="langsql">The sql language description</param>
        private static void ValidatingSqlPart(string issql, RichTextBox rtb, int posstart, HighlightLanguage langsql)
        {
            string sqlkeyword;
            if (issql.Length > 0)
            {
                // check sql field
                if (issql[0] == '`')
                {
                    for (int i = 1; i < issql.Length; i++)
                    {
                        if (issql[i] == '`')
                        {
                            ColorText(rtb, posstart, i + 1, Settings.HighlightSQLColorField);
                        }
                    }
                }

                if (issql[0] == '"')
                {
                    if (issql.IndexOf('"', 1) > 0)
                    {
                        return;
                    }

                    sqlkeyword = issql.Substring(1, issql.Length - 1);
                }
                else
                {
                    sqlkeyword = issql;
                }
            }
            else
            {
                return;
            }

            // check if keyword is known SQL statement
            if (langsql.FindKeyword(sqlkeyword.ToLower()))
            {
                ColorText(rtb, posstart, issql.Length, Settings.HighlightSQLColorValidstatement);
            }
        }

        #endregion Methods
    }
}
