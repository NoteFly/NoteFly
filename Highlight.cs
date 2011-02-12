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
        #region Fields (4)

        private static bool keywordsinit = false;
        private static string[] keywords_html;
        private static string[] keywords_php;
        private static string[] keywords_sql;
        private const string phpstartkeyword = "<?php";
        private const string phpendkeyword = "?>";
        private static string phpcommentline = "//";
        private static string phpcommentstart = "/*";
        private static string phpcommentend = "*/";

        #endregion Fields

        public static bool KeywordsInitialized
        {
            get
            {
                return keywordsinit;
            }
        }

        #region Methods (3)

        // Public Methods (2) 

        /// <summary>
        /// Initializes TextHighlighter fill the keywords lists.
        /// </summary>
        /// <param name="rtb">The richedit note content</param>
        /// <param name="checkhtml">indicteds wheter html is gonna be checked</param>
        public static void InitHighlighter()
        {
            if (Settings.highlightHTML)
            {
                keywords_html = xmlUtil.ParserLanguageLexical("langs.xml", "html");
            }

            if (Settings.highlightPHP)
            {
                keywords_php = xmlUtil.ParserLanguageLexical("langs.xml", "php");
            }

            if (Settings.highlightSQL)
            {
                keywords_sql = xmlUtil.ParserLanguageLexical("langs.xml", "sql");
            }

            keywordsinit = true;
        }

        /// <summary>
        /// Clear the keywords list.
        /// </summary>
        public static void DeinitHighlighter()
        {
            if (keywords_html != null)
            {
                keywords_html = null;
            }
            if (keywords_php != null)
            {
                keywords_php = null;
            }
            if (keywords_sql != null)
            {
                keywords_sql = null;
            }
            keywordsinit = false;
            GC.Collect();
        }

        /// <summary>
        /// Check the syntax of alle set languages on the RichTextbox RTF content.
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="skinnr"></param>
        /// <param name="notes"></param>
        public static void CheckSyntaxFull(RichTextBox rtb, int skinnr, Notes notes)
        {
            int cursorpos = rtb.SelectionStart;
            ResetHighlighting(rtb, skinnr, notes);
            if (Settings.highlightHTML || Settings.highlightPHP || Settings.highlightSQL)
            {
                if (!keywordsinit)
                {
                    Log.Write(LogType.error, "Keywords not initialized as they should already have. Hotfixing this, watchout memory use.");
                    InitHighlighter();
                }

                bool ishtmlendtag = false;
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
                            if (Settings.highlightPHP)
                            {
                                if (i + 5 <= rtb.TextLength)
                                {
                                    if (rtb.Text.Substring(i, 5) == phpstartkeyword)
                                    {
                                        //start php part
                                        posstartphp = i;
                                        ColorText(rtb, i, phpstartkeyword.Length, Color.Green);
                                        poslastkeyword = posstartphp + phpstartkeyword.Length;
                                    }
                                }
                            }
                            if (Settings.highlightHTML)
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
                            if (Settings.highlightPHP)
                            {
                                if (i > 6)
                                {
                                    if (rtb.Text.Substring(i - 1, phpendkeyword.Length) == phpendkeyword)
                                    {
                                        //end php part
                                        posendphp = i + phpendkeyword.Length;
                                        ColorText(rtb, posendphp - phpendkeyword.Length - 1, phpendkeyword.Length, xmlUtil.ConvToClr(Settings.highlightPHPColorValidfunctions) );
                                    }
                                }
                            }
                            if (Settings.highlightHTML)
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
                            if (Settings.highlightPHP)
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
                                                ColorText(rtb, posphpcommentstart, lencomment, xmlUtil.ConvToClr(Settings.highlightPHPColorComment) );
                                                posphpcommentstart = int.MaxValue;
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        case '\n':
                            if (Settings.highlightPHP)
                            {
                                if (posphpcommentstart != int.MaxValue)
                                {
                                    if (!isphpmultilinecomment)
                                    {
                                        lencommentline = i - posphpcommentstart;
                                        ColorText(rtb, posphpcommentstart, lencommentline, xmlUtil.ConvToClr(Settings.highlightPHPColorComment) );
                                        posphpcommentstart = int.MaxValue;
                                    }
                                }
                            }
                            if (Settings.highlightSQL)
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
                            poslastkeyword = i + 1; //+1 for '\n'
                            break;
                        case ' ':
                            if (Settings.highlightPHP)
                            {
                                if (i > posstartphp && i < posendphp - phpendkeyword.Length)
                                {
                                    int lenphpkeyword = i - poslastkeyword;
                                    if (lenphpkeyword > 0)
                                    {
                                        string isphp = rtb.Text.Substring(poslastkeyword, lenphpkeyword);
                                        if (ValidatingPhp(isphp))
                                        {
                                            ColorText(rtb, poslastkeyword, lenphpkeyword, Color.DarkCyan);
                                        }
                                        else
                                        {
                                            ColorText(rtb, poslastkeyword, lenphpkeyword, Color.DarkRed);
                                        }
                                    }
                                    
                                }
                            }
                            if (Settings.highlightSQL)
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
                            poslastkeyword = i + 1; //+1 for ' '
                            break;
                    }
                }
            }
            rtb.SelectionStart = cursorpos;
        }

        /// <summary>
        /// Color some part of the rich edit text.
        /// </summary>
        /// <param name="posstart">The start position</param>
        /// <param name="len">The lenght</param>
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
                throw new CustomException("TextHighlighter out of range: " + arg.Source);
            }
        }

        /// <summary>
        /// Make the whole text the default font color.
        /// </summary>
        /// <param name="rtb">The richedit control that hold the note content.</param>
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
        /// <returns>true if it is html</returns>
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
                ishtml = ishtml.Remove(1, 1); //e.g. "</title>" becomes "<title>"
            }
            if (ishtml.Length > 2)
            {
                if (ishtml[ishtml.Length - 2] == '/') //finds <br />
                {
                    endtag = true;
                    if (ishtml[ishtml.Length - 3] == ' ')
                    {
                        ishtml = ishtml.Remove(ishtml.Length - 2, 2); //e.g. <br /> becomes <br>
                    }
                    else
                    {
                        ishtml = ishtml.Remove(ishtml.Length - 2, 1); //e.g. <wrong/> becomes <wrong>
                    }
                }
            }
            ishtml = ishtml.ToLower(); //e.g. "<BR>" becomes "<br>"

            int lastpos = 1;
            int posendattributevalue = int.MaxValue;
            for (int pos = 1; pos < ishtml.Length; pos++)
            {
                if (ishtml[pos] == '"' || ishtml[pos] == '\'')
                {
                    if (isquotestring)
                    {
                        ColorText(rtb, posstarthtmltag + posstartquotestring, (pos - posstartquotestring + 1), xmlUtil.ConvToClr(Settings.highlightHTMLColorString) ); //+1 for quote itself
                        posendquotestring = pos +1; //+1 for quote itself counts.
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
                        string[] curattributeparts = curattribute.Split('='); //split atribute name and valeau.
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
                        for (int n = 0; n < keywords_html.Length; n++)
                        {
                            if (curattributename.Equals(keywords_html[n], StringComparison.InvariantCultureIgnoreCase))
                            {
                                attributefound = true;
                                ColorText(rtb, posstarthtmltag + lastpos, lenhighlight, xmlUtil.ConvToClr(Settings.highlightHTMLColorValid) );
                                break; 
                            }
                        }
                        if (!attributefound)
                        {
                             ColorText(rtb, posstarthtmltag + lastpos, lenhighlight, xmlUtil.ConvToClr(Settings.highlightHTMLColorInvalid));
                        }
                        lastpos = pos + 1; //+1 for ' ' or '>'
                    }
                }
            }
        }

        /// <summary>
        /// Find out if it is a php keyword.
        /// </summary>
        /// <param name="isphp"></param>
        /// <returns></returns>
        private static bool ValidatingPhp(string isphp)
        {
            isphp = isphp.ToLower();
            if (keywords_php == null)
            {
                InitHighlighter();
            }
            for (int i = 0; i < keywords_php.Length; i++)
            {
                if (isphp == keywords_php[i])
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Find out if it is a sql keyword.
        /// </summary>
        /// <param name="isphp"></param>
        /// <returns></returns>
        private static bool ValidatingSql(string issql)
        {
            issql = issql.ToLower();
            if (keywords_sql == null)
            {
                InitHighlighter();
            }
            for (int i = 0; i < keywords_sql.Length; i++)
            {
                if (issql == keywords_sql[i])
                {
                    return true;
                }
            }
            return false;
        }

        #endregion Methods
    }

}
