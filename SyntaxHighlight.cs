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
    /// Highlight class, provides highlighting of a richedittextbox.
    /// </summary>
    public sealed class SyntaxHighlight
    {
        #region Fields (10)

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

        /// <summary>
        /// Reference to RTFDirectEdit class for directly editing RTF used for coloring in the RichTextBox.
        /// </summary>
        private static RTFDirectEdit rtfdirectedit;

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
            rtfdirectedit = null;
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

            rtfdirectedit = new RTFDirectEdit();
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
            System.Diagnostics.Stopwatch stopwatch = null;
            if (Settings.ProgramLogInfo)
            {
                stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Start();
            }

            string rtf = rtb.Rtf;
            if (string.IsNullOrEmpty(rtf))
            {
                Log.Write(LogType.exception, "Empty note rtf");
                return;
            }

            if (!keywordsinit)
            {
                Log.Write(LogType.error, "Keywords not initialized as they should already have. Hotfixing this, watchout memory use.");
                InitHighlighter();
            }

            rtf = ResetHighlighting(rtb, rtf, skinnr, notes);

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
                    // curpos == rtb.TextLength - 1 for checking last part
                    if (rtb.Text[curpos] == ' ' || rtb.Text[curpos] == '\n' || rtb.Text[curpos] == '\r' || rtb.Text[curpos] == '\t' || curpos == rtb.TextLength - 1)
                    {
                        string part = rtb.Text.Substring(lastpos, curpos - lastpos);
                        if (part.Length > 0)
                        {
                            rtf = CheckSyntaxPart(rtb, rtf, part, curpos, lastpos);
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

            if (!string.IsNullOrEmpty(rtf))
            {
                int prevtextlen = rtb.TextLength;
                string prevrtf = rtb.Rtf;
                rtb.Rtf = rtf;
                if (rtb.TextLength != prevtextlen)
                {
                    Log.Write(LogType.exception, "rtf editing failed. rtb.TextLength=" + rtb.TextLength + " prevtextlen=" + prevtextlen);
                    rtb.Rtf = prevrtf;
                }
            }

            if (Settings.ProgramLogInfo)
            {
                stopwatch.Stop();
                Log.Write(LogType.info, "Note highlight time: " + stopwatch.ElapsedMilliseconds.ToString() + " ms");
            }
        }

        /// <summary>
        /// Do a quick syntax check of the last added part of text.
        /// </summary>
        /// <param name="rtb">RichEditTextbox</param>
        /// <param name="skinnr">The skinnr</param>
        /// <param name="notes">Reference to notes</param>
        public static void CheckSyntaxQuick(RichTextBox rtb, int skinnr, Notes notes)
        {
            if (!keywordsinit)
            {
                Log.Write(LogType.error, "Keywords not initialized as they should already have. Hotfixing this.");
                InitHighlighter();
            }

            // check if highlighting is enabled at all.
            if (langs.Count > 0)
            {
                for (int i = 0; i < langs.Count; i++)
                {
                    langs[i].PosDocumentStart = int.MaxValue;
                    langs[i].PosDocumentEnd = int.MaxValue;

                    // find out start position of language
                    int langstartpos = rtb.Text.IndexOf(langs[i].DocumentStartStr);
                    if (langstartpos >= 0)
                    {
                        langs[i].PosDocumentStart = langstartpos;
                    }

                    // find out end position of language
                    int langendpos = rtb.Text.LastIndexOf(langs[i].DocumentEndStr);
                    if (langendpos >= 0)
                    {
                        langs[i].PosDocumentEnd = langendpos;
                    }
                }

                int cursorpos = rtb.SelectionStart;
                string rtf = rtb.Rtf;
                int maxpos = Settings.HighlightMaxchars;
                if (rtb.TextLength < Settings.HighlightMaxchars)
                {
                    maxpos = rtb.TextLength;
                } 

                int lastpos = 0;
                // -2 for line ending length
                for (int i = cursorpos - 2; i > 0; i--) 
                {
                    if (rtb.Text[i] == ' ' || rtb.Text[i] == '\n' || rtb.Text[i] == '\r')
                    {
                        lastpos = i + 1; // after ' ', '\n' or '\r'
                        break;
                    }
                }

                int partlen = cursorpos - lastpos;
                if (partlen > 0 && lastpos + partlen <= rtb.TextLength)
                {
                    string part = rtb.Text.Substring(lastpos, cursorpos - lastpos);
                    int s = part.LastIndexOf('<');
                    if (s > 0)
                    {
                        lastpos += s;
                        part = rtb.Text.Substring(lastpos, cursorpos - lastpos);
                    }

                    rtf = CheckSyntaxPart(rtb, rtf, part, cursorpos, lastpos);
                }

                if (!string.IsNullOrEmpty(rtf))
                {
                    int prevtextlen = rtb.TextLength;
                    string prevrtf = rtb.Rtf;
                    rtb.Rtf = rtf;
                    if (rtb.TextLength != prevtextlen)
                    {
                        Log.Write(LogType.exception, "rtf editing failed. rtb.TextLength=" + rtb.TextLength + " prevtextlen=" + prevtextlen);
                        rtb.Rtf = prevrtf;
                    }
                }

                rtb.SelectionStart = cursorpos;
            }
        }

        /// <summary>
        /// Check the syntax of a part of the text with the enabled languages.
        /// </summary>
        /// <param name="rtb">The RichTextBox control</param>
        /// <param name="rtf">The RTF text</param>
        /// <param name="part">The part of text to check</param>
        /// <param name="curpos">The current position in the text</param>
        /// <param name="lastpos">The position of where the part started in the text of the RichTextBox</param>
        /// <returns>The new RTF text syntax highlighted</returns>
        private static string CheckSyntaxPart(RichTextBox rtb, string rtf, string part, int curpos, int lastpos)
        {
            for (int i = 0; i < langs.Count; i++)
            {
                langs[i].CheckSetDocumentPos(part, curpos);
                if (curpos >= langs[i].PosDocumentStart && curpos <= langs[i].PosDocumentEnd)
                {
                    switch (langs[i].Name)
                    {
                        case "html":
                            rtf = ValidatingHtmlPart(part, rtb, rtf, lastpos, langs[i]);
                            break;
                        case "php":
                            rtf = ValidatingPhpPart(part, rtb, rtf, lastpos, langs[i]);
                            break;
                        case "sql":
                            rtf = ValidatingSqlPart(part, rtb, rtf, lastpos, langs[i]);
                            break;
                    }
                }
            }

            return rtf;
        }

        /// <summary>
        /// Color some part of the rich edit text.
        /// </summary>
        /// <param name="rtb">The richtextbox contain the rtf text to apply coloring on.</param>
        /// <param name="rtf">The RTF text</param>
        /// <param name="posstart">The start position in the text to start coloring from.</param>
        /// <param name="len">The lenght of text to color.</param>
        /// <param name="hexcolor">The color the text should get.</param>
        private static string ColorText(RichTextBox rtb, string rtf, int posstart, int len, string hexcolor)
        {
            Color color = xmlUtil.ConvToClr(hexcolor);
            if (!string.IsNullOrEmpty(rtf))
            {
                return rtfdirectedit.SetColorInRTF(rtf, color, posstart, len);
            }
            else
            {
                Log.Write(LogType.exception, "rtf is empty, rtb.TextLength=" + rtb.TextLength + ", posstart=" + posstart + ", len=" + len);
                return rtf;
            }
        }

        /// <summary>
        /// Make the whole text the default font color.
        /// </summary>
        /// <param name="rtb">The richedit control that hold the note content.</param>
        /// <param name="rtf">The RTF text</param>
        /// <param name="skinnr">The skin number of the current note.</param>
        /// <param name="notes">Reference to notes class.</param>
        /// <returns>The new rtf stream</returns>
        private static string ResetHighlighting(RichTextBox rtb, string rtf, int skinnr, Notes notes)
        {
            comment = false;
            commentline = false;
            outerhtml = true;
            htmlstringpart = false;
            phpstringpart = false;
            currentstringquote = '"';
            return rtfdirectedit.SetColorAllRTF(rtf, notes.GetTextClr(skinnr), rtb.TextLength);
        }

        /// <summary>
        /// Highlight some text part on html split by spaces.
        /// </summary>
        /// <param name="ishtml">String without spaces. length needs to be >0</param>
        /// <param name="rtb">The richtextbox.</param>
        /// <param name="rtf">The rtf stream.</param>
        /// <param name="posstartpart">The start position in the richtextbox.</param>
        /// <param name="langhtml">The html language description.</param>
        /// <returns>The new rtf stream</returns>
        private static string ValidatingHtmlPart(string ishtml, RichTextBox rtb, string rtf, int posstartpart, HighlightLanguage langhtml)
        {
            ishtml = ishtml.ToLowerInvariant();
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
                        if (ishtml[c] == currentstringquote)
                        {
                            rtf = ColorText(rtb, rtf, posstartpart, c + 1, Settings.HighlightHTMLColorString); // +1 for '"' or '\'' itself too.
                            htmlstringpart = false;
                        }
                        else if (c == ishtml.Length - 1)
                        {
                            rtf = ColorText(rtb, rtf, posstartpart, c + 1, Settings.HighlightHTMLColorString); // +1 for space/enter
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
                        rtf = ValidateHTMLAttribute(attributes[nattr], rtb, rtf, attributesstartpos[nattr], langhtml);
                    }
                }
            }
            else
            {
                // is comment
                rtf = ColorText(rtb, rtf, posstartpart, ishtml.Length, Settings.HighlightHTMLColorComment);
            }

            if (ishtml.EndsWith(langhtml.Commentend, StringComparison.Ordinal))
            {
                comment = false;
            }

            return rtf;
        }

        /// <summary>
        /// Validate HTML attribute
        /// </summary>
        /// <param name="htmlattribute">Attribute of the HTML node to validate</param>
        /// <param name="rtb">Richtextbox with note content</param>
        /// <param name="rtf">The rtf stream.</param>
        /// <param name="attributestartpos">Startposition of the attribute within the htmlpart.</param>
        /// <param name="langhtml">The html language description.</param>
        /// <returns>The new rtf stream</returns>
        private static string ValidateHTMLAttribute(string htmlattribute, RichTextBox rtb, string rtf, int attributestartpos, HighlightLanguage langhtml)
        {
            if (htmlattribute == "/" || htmlattribute.Length < 1)
            {
                return rtf;
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
                return rtf;
            }

            if (langhtml.FindKeyword(attrname.ToLower()))
            {
                knowattr = true;
            }

            if (!knowattr)
            {
                // Wrong
                rtf = ColorText(rtb, rtf, attributestartpos, attrsepnamevaleau[0].Length, Settings.HighlightHTMLColorInvalid);
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
                            rtf = ColorText(rtb, rtf, attributestartpos, attrsepnamevaleau[0].Length, Settings.HighlightHTMLColorValid);
                        }
                    }
                    else if (i == 1)
                    {
                        if (attrsepnamevaleau[1].StartsWith("\"") || attrsepnamevaleau[1].StartsWith("'"))
                        {
                            // is string
                            htmlstringpart = true;
                            int posstartstring = attributestartpos + attrsepnamevaleau[0].Length + 1; // +1 for '=' 
                            rtf = ColorText(rtb, rtf, posstartstring, attrsepnamevaleau[1].Length, Settings.HighlightHTMLColorString);
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

            return rtf;
        }

        /// <summary>
        /// Find out if it is a php keyword.
        /// </summary>
        /// <param name="isphp">A part to be check if this a php keyword.</param>
        /// <param name="rtb">The richtextbox.</param>
        /// <param name="rtf">The rtf stream.</param>
        /// <param name="posstart">The position in rtb where this keyword starts.</param>
        /// <param name="langphp">The PHP language description.</param>
        /// <returns>The new rtf stream.</returns>
        private static string ValidatingPhpPart(string isphp, RichTextBox rtb, string rtf, int posstart, HighlightLanguage langphp)
        {
            int posvar = -1;
            if (isphp.StartsWith(langphp.Commentstart))
            {
                comment = true;
            }

            if (commentline || comment)
            {
                rtf = ColorText(rtb, rtf, posstart, isphp.Length, Settings.HighlightPHPColorComment);
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
                                rtf = ColorText(rtb, rtf, posstart + curchr - 1, isphp.Length - curchr + 1, Settings.HighlightPHPColorComment);
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
                                rtf = ColorText(rtb, rtf, posstart + curchr, 1, Settings.HighlightPHPColorString);
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
                                rtf = ColorText(rtb, rtf, posstart + posvar, curchr - posvar, Settings.HighlightPHPColorDocumentstartend); // is variable
                                posvar = -1;
                            }
                        }
                    }
                    else if (curchr == isphp.Length - 1)
                    {
                        if (posvar >= 0)
                        {
                            // is variable
                            rtf = ColorText(rtb, rtf, posstart + posvar, isphp.Length, Settings.HighlightPHPColorDocumentstartend);
                        }
                    }

                    if (phpstringpart)
                    {
                        rtf = ColorText(rtb, rtf, posstart + curchr, 1, Settings.HighlightPHPColorString);
                    }
                }

                // check if keyword is known php function
                if (langphp.FindKeyword(isphp))
                {
                    rtf = ColorText(rtb, rtf, posstart, isphp.Length, Settings.HighlightPHPColorValidfunctions);
                }
            }

            if (isphp.EndsWith(langphp.Commentend))
            {
                comment = false;
            }

            return rtf;
        }

        /// <summary>
        /// Find out if it is a sql keyword.
        /// </summary>
        /// <param name="issql">The part to be check.</param>
        /// <param name="rtb">The richtextbox.</param>
        /// <param name="rtf">The rtf stream.</param>
        /// <param name="posstart">Position where the keyword starts in the richtextbox.</param>
        /// <param name="langsql">The sql language description.</param>
        /// <returns>The new rtf stream.</returns>
        private static string ValidatingSqlPart(string issql, RichTextBox rtb, string rtf, int posstart, HighlightLanguage langsql)
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
                            rtf = ColorText(rtb, rtf, posstart, i + 1, Settings.HighlightSQLColorField);
                        }
                    }
                }

                if (issql[0] == '"')
                {
                    if (issql.IndexOf('"', 1) > 0)
                    {
                        return rtf;
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
                return rtf;
            }

            // check if keyword is known SQL statement
            if (langsql.FindKeyword(sqlkeyword.ToLower()))
            {
                rtf = ColorText(rtb, rtf, posstart, issql.Length, Settings.HighlightSQLColorValidstatement);
            }

            return rtf;
        }

        #endregion Methods
    }
}
