//-----------------------------------------------------------------------
// <copyright file="Highlight.cs" company="GNU">
//  NoteFly a note application.
//  Copyright (C) 2010-2011  Tom
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
#define windows

namespace NoteFly
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;

    /// <summary>
    /// Highlight class, provides highlighting to richtext.
    /// </summary>
    public class Highlight
    {
        #region Fields (3) 

        private static const string LANGFILE = "langs.xml";

        private static List<HighlightLanguage> languages;

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
        /// Check the syntax of alle set languages on the RichTextbox RTF content.
        /// </summary>
        /// <param name="rtb">The richttextbox with RTF note content.</param>
        /// <param name="skinnr">The skin number</param>
        /// <param name="notes">Pointer to notes class.</param>
        public static void CheckSyntaxFull(RichTextBox rtb, int skinnr, Notes notes)
        {
            int cursorpos = rtb.SelectionStart;
            ResetHighlighting(rtb, skinnr, notes);

            // check if highlighting is enabled at all.
            if (Settings.HighlightHTML || Settings.HighlightPHP || Settings.HighlightSQL)
            {
                if (!keywordsinit)
                {
                    Log.Write(LogType.error, "Keywords not initialized as they should already have. Hotfixing this, watchout memory use.");
                    InitHighlighter();
                }

                int lastpos = 0;
                
                int posstarthtml = 0;
                int posendhtml = int.MaxValue;

                int posstartphp = int.MaxValue;
                int posendphp = int.MaxValue;

                int posstartsql = 0;
                int posendsql = int.MaxValue;

                //int posphpcommentstart = int.MaxValue;
                //bool isphpmultilinecomment = false;
                //int lencommentline = 0;
                //int poslastkeyword = 0;
                //bool tagopen = false;

                for (int i = 0; i < rtb.TextLength; i++)
                {
#if !macos
                    if (rtb.Text[i] == ' ' || rtb.Text[i] == '\n')
#elif macos
                    if (rtb.Text[i] == ' ' || rtb.Text[i] == '\r')
#endif
                    {
                        int lenbufcheck = i - lastpos;
                        string bufcheck = rtb.Text.Substring(lastpos, lenbufcheck);
                        if (lenbufcheck >= 1 && bufcheck.Length>=1)
                        {
                            // checking order is important

                            if (Settings.HighlightSQL)
                            {
                                if (i > posstartsql && i < posendsql)
                                {
                                    ValidatingSql(bufcheck);
                                }
                            }

                            if (Settings.HighlightPHP)
                            {
                                if (i > posendphp && i < posendphp)
                                {
                                    ValidatingPhp(bufcheck, rtb, lastpos, lenbufcheck);
                                }
                            }

                            if (Settings.HighlightHTML)
                            {
                                if (i > posstarthtml && i < posendhtml)
                                {
                                    ValidatingHtmlTag(bufcheck, rtb, posstarthtml, lenbufcheck);
                                }
                            }

                        }
                    }
                }

                rtb.SelectionStart = cursorpos;
            }
        }

        /// <summary>
        /// Clear the keywords list.
        /// </summary>
        public static void DeinitHighlighter()
        {
            languages.Clear();
            keywordsinit = false;
            GC.Collect();
        }

        /// <summary>
        /// Initializes TextHighlighter fill the keywords lists.
        /// </summary>
        public static void InitHighlighter()
        {
            languages.Clear();
            if (Settings.HighlightHTML)
            {
                languages.Add(xmlUtil.ParserLanguageLexical(LANGFILE, "html"));
            }

            if (Settings.HighlightPHP)
            {
                languages.Add(xmlUtil.ParserLanguageLexical(LANGFILE, "php"));
            }

            if (Settings.HighlightSQL)
            {
                languages.Add(xmlUtil.ParserLanguageLexical(LANGFILE, "sql"));
            }

            keywordsinit = true;
        }

        /// <summary>
        /// Get the HighlightLanguage specified by name
        /// </summary>
        /// <param name="name">The name of the highlightlanguage object</param>
        /// <returns>HighlightLanguage object</returns>
        private static HighlightLanguage GetHighlightlanguage(string name)
        {
            foreach (HighlightLanguage curlang in languages)
            {
                if (curlang.Name == name)
                {
                    return curlang;
                }
            }

            return null;
        }


        /// <summary>
        /// Color some part of the rich edit text.
        /// </summary>
        /// <param name="rtb">The richtextbox contain the rtf text to apply coloring on.</param>
        /// <param name="posstart">The start position in the text to start coloring from.</param>
        /// <param name="len">The lenght of text to color.</param>
        /// <param name="color">The color the text should get.</param>
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
            rtb.ForeColor = notes.GetTextClr(skinnr);
            rtb.SelectAll();
            rtb.SelectionColor = notes.GetTextClr(skinnr);
            rtb.Select(0, 0);
        }

        /// <summary>
        /// Remove characters not used for checking keyword.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string RemoveUnusedchars(string text)
        {
            text = text.Trim(new char[] { ' ', '\n', '\r' });
            text = text.ToLower();
            return text;
        }

        /// <summary>
        /// Highlight known tag and tag attributes.
        /// </summary>
        /// <param name="ishtml">Is it html to check.</param>
        /// <param name="rtb">The richtextbox.</param>
        /// <param name="posstarthtmltag">the start position in the richtextbox.</param>
        /// <param name="lenhtmltag">The length of the compleet tag.</param>
        private static void ValidatingHtmlTag(string ishtml, RichTextBox rtb, int posstarthtmltag, int lenhtmltag)
        {
            ishtml = RemoveUnusedchars(ishtml);

            bool isquotestring = false;

            int posendquotestring = 0;
            int posstartquotestring = int.MaxValue;

            bool endtag = false;
            int lenhighlight = 0;

            if (ishtml[1] == '/')
            {
                endtag = true;
                ishtml = ishtml.Remove(1, 1); // e.g. "</title>" becomes "<title>"
            }

            if (ishtml.Length > 2)
            {
                // finds e.g. <br />
                if (ishtml[ishtml.Length - 2] == '/')
                {
                    endtag = true;

                    // e.g. <br /> becomes <br> and <wrong/> becomes <wrong>
                    if (ishtml[ishtml.Length - 3] == ' ')
                    {
                        ishtml = ishtml.Remove(ishtml.Length - 2, 2);
                    }
                    else
                    {
                        ishtml = ishtml.Remove(ishtml.Length - 2, 1);
                    }
                }
            }

            int lastpos = 1;
            for (int pos = 1; pos < ishtml.Length; pos++)
            {
                if (ishtml[pos] == '"' || ishtml[pos] == '\'')
                {
                    if (isquotestring)
                    {
                        //ColorText(rtb, posstarthtmltag + posstartquotestring, (pos - posstartquotestring + 1), xmlUtil.ConvToClr(Settings.HighlightHTMLColorString));
                        posendquotestring = pos + 1; // +1 for quote itself counts
                    }
                    else
                    {
                        posstartquotestring = pos;
                    }

                    isquotestring = !isquotestring;
                }
                else if ((ishtml[pos] == '>') || (ishtml[pos] == ' '))
                {
                    if (lastpos < posstartquotestring || lastpos > posendquotestring)
                    {
                        string curattribute = ishtml.Substring(lastpos, pos - lastpos);
                        string[] curattributeparts = curattribute.Split('='); // split atribute name and valeau.
                        string curattributename = curattributeparts[0];
                        if (endtag)
                        {
                            lenhighlight = curattributename.Length + 1;
                        }
                        else
                        {
                            lenhighlight = curattributename.Length;
                        }

                        bool attributefound = false;
                        for (int n = 0; n < GetHighlightlanguage("htm").NumKeywords; n++)
                        {
                            if (curattributename.Equals(GetHighlightlanguage("htm").GetKeyword(n), StringComparison.InvariantCultureIgnoreCase))
                            {
                                attributefound = true;
                                ColorText(rtb, posstarthtmltag + lastpos, lenhighlight, Settings.HighlightHTMLColorValid);
                                break;
                            }
                        }

                        if (!attributefound)
                        {
                            ColorText(rtb, posstarthtmltag + lastpos, lenhighlight, Settings.HighlightHTMLColorInvalid);
                        }

                        lastpos = pos + 1; // +1 for ' ' or '>'
                    }
                }
            }
        }

        /// <summary>
        /// Find out if it is a php keyword.
        /// </summary>
        /// <param name="isphp">A part to be check if this a php keyword.</param>
        /// <returns></returns>
        private static void ValidatingPhp(string isphp, RichTextBox rtb, int posstart, int len)
        {
            isphp = RemoveUnusedchars(isphp);

            if (isphp.StartsWith("$") && isphp.Length > 1)
            {
                char c = isphp[1];
                if (c > 58)
                {
                    // is valid variable
                    ColorText(rtb, posstart, len, Settings.HighlightPHPColorValidfunctions);
                }
            }

            // is assign:
            if (isphp == "=")
            {
                ColorText(rtb, posstart, len, Settings.HighlightPHPColorValidfunctions);
            }

            // is know function:
            for (int i = 0; i < languages.g .Length; i++)
            {
                if (isphp == keywordsphp[i])
                {
                    return 1;
                }
            }

            // has string:
            for (int n = 0; n < isphp.Length; n++)
            {
                if (isphp[n] == '"')
                {
                    return 2;
                }
            }

            // not valid:
            return 0;
        }

        /// <summary>
        /// Find out if it is a sql keyword.
        /// </summary>
        /// <param name="issql">The part to be check.</param>
        /// <returns>true if a keyword matches issql part.</returns>
        private static void ValidatingSql(string issql, RichTextBox rtb, int posstart, int len)
        {
            issql = RemoveUnusedchars(issql);

            //for (int i = 0; i < keywordssql.Length; i++)
            //{
            //    if (issql == keywordssql[i])
            //    {
            //        return true;
            //    }
            //}

            return false;
        }

        #endregion Methods 
    }
}
