//-----------------------------------------------------------------------
// <copyright file="TextHighlight.cs" company="GNU">
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
        #region Fields (9)

        /// <summary>
        /// The php end document keyword.
        /// </summary>
        private static string phpendkeyword = "?>";

        /// <summary>
        /// The php start document keyword.
        /// </summary>
        private static string phpstartkeyword = "<?php";

        /// <summary>
        /// Keywords used for HTML highlighting.
        /// </summary>
        private static string[] keywordshtml;

        /// <summary>
        /// Keywords used for PHP highlighting.
        /// </summary>
        private static string[] keywordsphp;

        /// <summary>
        /// Keywords used for SQL highlighting.
        /// </summary>
        private static string[] keywordssql;

        /// <summary>
        /// Are keyword initilized
        /// </summary>
        private static bool keywordsinit = false;

        /// <summary>
        /// The comment line.
        /// </summary>
        private static string phpcommentline;

        /// <summary>
        /// The php comment start.
        /// </summary>
        private static string phpcommentstart;

        /// <summary>
        /// The php comment end.
        /// </summary>
        private static string phpcommentend;

        /// <summary>
        /// html comment start.
        /// </summary>
        private static string htmlcommentstart;

        /// <summary>
        /// html comment end.
        /// </summary>
        private static string htmlcommentend;

        /// <summary>
        /// sql comment start.
        /// </summary>
        private static string sqlcommentstart;

        /// <summary>
        /// sql comment end.
        /// </summary>
        private static string sqlcommentend;

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

        // Public Methods (3)

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
            if (Settings.HighlightHTML || Settings.HighlightPHP || Settings.HighlightSQL)
            {
                if (!keywordsinit)
                {
                    Log.Write(LogType.error, "Keywords not initialized as they should already have. Hotfixing this, watchout memory use.");
                    InitHighlighter();
                }

                int posstarthtmltag = 0;
                int posstartphp = int.MaxValue;
                int posendphp = int.MaxValue;
                int posphpcommentstart = int.MaxValue;
                bool isphpmultilinecomment = false;
                int lencommentline = 0;
                int poslastkeyword = 0;
                bool tagopen = false;
                for (int i = 0; i < rtb.TextLength; i++)
                {
                    switch (rtb.Text[i])
                    {
                        case '<':
                            if (Settings.HighlightPHP)
                            {
                                if (i + 5 <= rtb.TextLength)
                                {
                                    if (rtb.Text.Substring(i, 5) == phpstartkeyword)
                                    {
                                        // start php part
                                        posstartphp = i;
                                        ColorText(rtb, i, phpstartkeyword.Length, xmlUtil.ConvToClr(Settings.HighlightPHPColorDocumentstartend));
                                        poslastkeyword = posstartphp + phpstartkeyword.Length;
                                    }
                                }
                            }

                            if (Settings.HighlightHTML)
                            {
                                if ((i < posstartphp || i > posendphp) && !tagopen)
                                {
                                    posstarthtmltag = i;
                                }
                            }

                            tagopen = true;
                            break;
                        case '>':
                            tagopen = false;
                            if (Settings.HighlightPHP)
                            {
                                if (i > 6)
                                {
                                    if (rtb.Text.Substring(i - 1, phpendkeyword.Length) == phpendkeyword)
                                    {
                                        // end php part
                                        posendphp = i + phpendkeyword.Length;
                                        ColorText(rtb, (posendphp - phpendkeyword.Length - 1), phpendkeyword.Length, xmlUtil.ConvToClr(Settings.HighlightPHPColorDocumentstartend));
                                    }
                                }
                            }

                            if (Settings.HighlightHTML)
                            {
                                if ((i < posstartphp || i > posendphp) && i > 0)
                                {
                                    int lenhtmltag = i - posstarthtmltag + 1;
                                    string ishtml = rtb.Text.Substring(posstarthtmltag, lenhtmltag);
                                    ValidatingHtmlTag(ishtml, rtb, posstarthtmltag, lenhtmltag);
                                }
                            }

                            break;
                        case '/':
                            if (Settings.HighlightPHP)
                            {
                                if (i > posstartphp && i < posendphp)
                                {
                                    if (i < rtb.TextLength - 1)
                                    {
                                        if (rtb.Text.Substring(i, phpcommentline.Length) == phpcommentline)
                                        {
                                            if (posphpcommentstart > i)
                                            {
                                                isphpmultilinecomment = false;
                                                posphpcommentstart = i;
                                            }
                                        }
                                        else if (rtb.Text.Substring(i, phpcommentstart.Length) == phpcommentstart)
                                        {
                                            isphpmultilinecomment = true;
                                            posphpcommentstart = i;
                                        }
                                    }
                                    else if (i > 1)
                                    {
                                        if (rtb.Text.Substring(i - phpcommentline.Length, phpcommentline.Length) == phpcommentend)
                                        {
                                            if (isphpmultilinecomment)
                                            {
                                                int lencomment = i - posphpcommentstart;
                                                ColorText(rtb, posphpcommentstart, lencomment, xmlUtil.ConvToClr(Settings.HighlightPHPColorComment));
                                                posphpcommentstart = int.MaxValue;
                                            }
                                        }
                                    }
                                }
                            }

                            break;
                        case '\n':
                            if (Settings.HighlightPHP)
                            {
                                if (posphpcommentstart != int.MaxValue)
                                {
                                    if (!isphpmultilinecomment)
                                    {
                                        lencommentline = i - posphpcommentstart;
                                        ColorText(rtb, posphpcommentstart, lencommentline, xmlUtil.ConvToClr(Settings.HighlightPHPColorComment));
                                        posphpcommentstart = int.MaxValue;
                                    }
                                }
                            }

                            if (Settings.HighlightSQL)
                            {
                                int lensqlkeyword = i - poslastkeyword;
                                if (lensqlkeyword > 0)
                                {
                                    string issql = rtb.Text.Substring(poslastkeyword, lensqlkeyword);
                                    if (ValidatingSql(issql))
                                    {
                                        ColorText(rtb, poslastkeyword, lensqlkeyword, Color.Purple);
                                    }
                                }
                            }

                            poslastkeyword = i + 1;
                            break;
                        case ' ':
                            if (Settings.HighlightPHP)
                            {
                                if (i > posstartphp && i < posendphp - phpendkeyword.Length)
                                {
                                    int lenphpkeyword = i - poslastkeyword;
                                    if (lenphpkeyword > 0)
                                    {
                                        string isphp = rtb.Text.Substring(poslastkeyword, lenphpkeyword);
                                        int resnode = ValidatingPhp(isphp);
                                        if (resnode == 1)
                                        {
                                            ColorText(rtb, poslastkeyword, lenphpkeyword, xmlUtil.ConvToClr(Settings.HighlightPHPColorValidfunctions));
                                        }
                                        else if (resnode == 3)
                                        {
                                            ColorText(rtb, poslastkeyword, lenphpkeyword, xmlUtil.ConvToClr(Settings.HighlightPHPColorComment));
                                        }
                                        else if (resnode == 2)
                                        {
                                            int poslastquote = int.MaxValue;
                                            bool isendquote = false;
                                            for (int n = 0; n < isphp.Length; n++)
                                            {
                                                if (isphp[n] == '"')
                                                {
                                                    if (isendquote)
                                                    {
                                                        int len = (poslastkeyword + n) - poslastquote + 1;
                                                        ColorText(rtb, poslastquote, len, xmlUtil.ConvToClr(Settings.HighlightPHPColorComment));
                                                        poslastquote = int.MaxValue;
                                                        isendquote = false;
                                                    }
                                                    else
                                                    {
                                                        poslastquote = poslastkeyword + n;
                                                        isendquote = true;
                                                    }
                                                }
                                            }
                                        }
                                        else if (resnode == 0)
                                        {
                                            ColorText(rtb, poslastkeyword, lenphpkeyword, xmlUtil.ConvToClr(Settings.HighlightPHPColorInvalidfunctions));
                                        }
                                    }
                                }
                            }

                            if (Settings.HighlightSQL)
                            {
                                int lensqlkeyword = i - poslastkeyword;
                                if (lensqlkeyword > 0)
                                {
                                    string issql = rtb.Text.Substring(poslastkeyword, lensqlkeyword);
                                    if (ValidatingSql(issql))
                                    {
                                        ColorText(rtb, poslastkeyword, lensqlkeyword, Color.Purple);
                                    }
                                }
                            }

                            poslastkeyword = i + 1;
                            break;
                    }
                }
            }

            rtb.SelectionStart = cursorpos;
        }

        /// <summary>
        /// Clear the keywords list.
        /// </summary>
        public static void DeinitHighlighter()
        {
            if (keywordshtml != null)
            {
                keywordshtml = null;
            }

            if (keywordsphp != null)
            {
                keywordsphp = null;
            }

            if (keywordssql != null)
            {
                keywordssql = null;
            }

            keywordsinit = false;
            GC.Collect();
        }

        /// <summary>
        /// Initializes TextHighlighter fill the keywords lists.
        /// </summary>
        public static void InitHighlighter()
        {
            string[] langcomments;
            if (Settings.HighlightHTML)
            {
                keywordshtml = xmlUtil.ParserLanguageLexical("langs.xml", "html", out langcomments);
                htmlcommentstart = langcomments[1];
                htmlcommentend = langcomments[2];
            }

            if (Settings.HighlightPHP)
            {
                keywordsphp = xmlUtil.ParserLanguageLexical("langs.xml", "php", out langcomments);
                phpcommentline = langcomments[0];
                phpcommentstart = langcomments[1];
                phpcommentend = langcomments[2];
            }

            if (Settings.HighlightSQL)
            {
                keywordssql = xmlUtil.ParserLanguageLexical("langs.xml", "sql", out langcomments);
                sqlcommentstart = langcomments[1];
                sqlcommentend = langcomments[2];
            }

            keywordsinit = true;
        }

        // Private Methods (5) 

        /// <summary>
        /// Color some part of the rich edit text.
        /// </summary>
        /// <param name="rtb">The richtextbox contain the rtf text to apply coloring on.</param>
        /// <param name="posstart">The start position in the text to start coloring from.</param>
        /// <param name="len">The lenght of text to color.</param>
        /// <param name="color">The color the text should get.</param>
        private static void ColorText(RichTextBox rtb, int posstart, int len, Color color)
        {
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
        /// Highlight known tag and tag attributes.
        /// </summary>
        /// <param name="ishtml">Is it html to check.</param>
        /// <param name="rtb">The richtextbox.</param>
        /// <param name="posstarthtmltag">the start position in the richtextbox.</param>
        /// <param name="lenhtmltag">The length of the compleet tag.</param>
        private static void ValidatingHtmlTag(string ishtml, RichTextBox rtb, int posstarthtmltag, int lenhtmltag)
        {
            bool isquotestring = false;
            int posstartquotestring = int.MaxValue;
            int posendquotestring = 0;
            bool endtag = false;
            int lenhighlight = 0;
            if (ishtml[1] == '/')
            {
                endtag = true;
                ishtml = ishtml.Remove(1, 1); // e.g. "</title>" becomes "<title>"
            }

            if (ishtml.Length > 2)
            {
                // finds <br />
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

            ishtml = ishtml.ToLower(); // e.g. "<BR>" becomes "<br>"
            int lastpos = 1;
            for (int pos = 1; pos < ishtml.Length; pos++)
            {
                if (ishtml[pos] == '"' || ishtml[pos] == '\'')
                {
                    if (isquotestring)
                    {
                        ColorText(rtb, posstarthtmltag + posstartquotestring, (pos - posstartquotestring + 1), xmlUtil.ConvToClr(Settings.HighlightHTMLColorString));
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
                        for (int n = 0; n < keywordshtml.Length; n++)
                        {
                            if (curattributename.Equals(keywordshtml[n], StringComparison.InvariantCultureIgnoreCase))
                            {
                                attributefound = true;
                                ColorText(rtb, posstarthtmltag + lastpos, lenhighlight, xmlUtil.ConvToClr(Settings.HighlightHTMLColorValid));
                                break;
                            }
                        }

                        if (!attributefound)
                        {
                            ColorText(rtb, posstarthtmltag + lastpos, lenhighlight, xmlUtil.ConvToClr(Settings.HighlightHTMLColorInvalid));
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
        /// <returns>true if a keyword matches isphp part.</returns>
        private static int ValidatingPhp(string isphp)
        {
            isphp = isphp.ToLower();
            if (keywordsphp == null)
            {
                InitHighlighter();
            }

            // is var.
            if (isphp.StartsWith("$") && isphp.Length > 1)
            {
                char c = isphp[1];
                if (c > 58)
                {
                    return 3;
                }
            }

            // is assign:
            if (isphp == "=")
            {
                return 4;
            }

            // is know function:
            for (int i = 0; i < keywordsphp.Length; i++)
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
        private static bool ValidatingSql(string issql)
        {
            issql = issql.ToLower();
            if (keywordssql == null)
            {
                InitHighlighter();
            }

            for (int i = 0; i < keywordssql.Length; i++)
            {
                if (issql == keywordssql[i])
                {
                    return true;
                }
            }

            return false;
        }

        #endregion Methods
    }
}
