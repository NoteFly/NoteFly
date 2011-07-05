//-----------------------------------------------------------------------
// <copyright file="SyntaxHighlight.cs" company="GNU">
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
    using System.Text;

    /// <summary>
    /// Highlight class, provides highlighting to richtext.
    /// </summary>
    public class SyntaxHighlight
    {
        #region Fields (3)

        private const string LANGFILE = "langs.xml";

        private static HighlightLanguage langphp;
        private static HighlightLanguage langhtml;
        private static HighlightLanguage langsql;

        private static int htmlnodestartpos = 0;
        private static int htmlnodeendpos = Int32.MaxValue;

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
                        if (lenbufcheck >= 1 && bufcheck.Length >= 1)
                        {

                            if (Settings.HighlightSQL)
                            {
                                if (i > langsql.PosDocumentStart && i < langsql.PosDocumentEnd)
                                {
                                    ValidatingSqlPart(bufcheck, rtb, lastpos, lenbufcheck);
                                }
                            }

                            if (Settings.HighlightPHP)
                            {
                                if (i > langphp.PosDocumentStart && i < langphp.PosDocumentEnd)
                                {
                                    ValidatingPhpPart(bufcheck, rtb, lastpos, lenbufcheck);
                                }
                            }

                            if (Settings.HighlightHTML)
                            {
                                if (i > langhtml.PosDocumentStart && i < langhtml.PosDocumentEnd)
                                {
                                    ValidatingHtmlPart(bufcheck, rtb, lastpos);
                                }
                            }

                        }
                        lastpos = i+1; // without space or linefeed
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
            langhtml = null;
            langphp = null;
            langsql = null;
            keywordsinit = false;
            GC.Collect();
        }

        /// <summary>
        /// Initializes TextHighlighter fill the keywords lists.
        /// </summary>
        public static void InitHighlighter()
        {
            if (Settings.HighlightHTML)
            {
                langhtml = xmlUtil.ParserLanguageLexical(LANGFILE, "html");
            }

            if (Settings.HighlightPHP)
            {
                langphp = xmlUtil.ParserLanguageLexical(LANGFILE, "php");
            }

            if (Settings.HighlightSQL)
            {
                langsql = xmlUtil.ParserLanguageLexical(LANGFILE, "sql");
            }

            keywordsinit = true;
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
            rtb.SelectAll();
            rtb.SelectionColor = notes.GetTextClr(skinnr);
            rtb.Select(0, 0);

            rtb.ForeColor = notes.GetTextClr(skinnr);

            htmlnodestartpos = 0;
            htmlnodeendpos = Int32.MaxValue;
        }

        /// <summary>
        /// Remove characters not used for checking keyword.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string RemoveEnterAndTabChars(string text)
        {
            text = text.Trim(new char[] { '\n', '\r', '\t' });
            text = text.ToLower();
            return text;
        }

        /// <summary>
        /// Highlight some text part on html split by spaces.
        /// </summary>
        /// <param name="ishtml">string without spaces.</param>
        /// <param name="rtb">The richtextbox.</param>
        /// <param name="posstarthtmltag">the start position in the richtextbox.</param>
        /// <param name="lenhtmltag">The length of the compleet tag.</param>
        private static void ValidatingHtmlPart(string ishtml, RichTextBox rtb, int posstartpart)
        {
            //ishtml = RemoveEnterChars(ishtml);

            // e.g.: <body color="#333333" lang="en"> 
            // ishtml 1: <body
            // ishtml 2: color="#333333"
            // ishtml 3: lang="en">
            //
            // e.g.: <p>test</p><br>
            // ishtml 1: <p>test</p><br>
            //

            ishtml = ishtml.ToLower();

            StringBuilder sbnode = new StringBuilder();
            List<String> attributes = new List<string>(); // these attributes are within this part.
            bool outerhtml = false;

            for (int c = 0; c < ishtml.Length; c++)
            {
                if (ishtml[c] == '<')
                {
                    if (htmlnodeendpos > htmlnodestartpos)
                    {
                        htmlnodestartpos = posstartpart + c + 1; // without <
                    }
                    outerhtml = false;
                    //else
                    //{
                        //ColorText(rtb, htmlnodestartpos, posstartpart + c, Settings.HighlightHTMLColorInvalid); // e.g. <saf<asfd -> red: <saf<
                    //}
                }
                else if (ishtml[c] == '>')
                {
                    //if (htmlnodestartpos < htmlnodeendpos)
                    //{
                    if (!outerhtml)
                    {
                        htmlnodeendpos = posstartpart + c;
                        attributes.Add(sbnode.ToString());
                        sbnode = new StringBuilder();
                        outerhtml = true;
                    }
                }
                else
                {
                    if (ishtml[c] != '/')
                    {
                        sbnode.Append(ishtml[c]);
                    }

                    if (c == ishtml.Length - 1)
                    {
                        if (!outerhtml)
                        {
                            attributes.Add(sbnode.ToString());
                        }
                    }
                }
            }

            for (int nattr = 0; nattr < attributes.Count; nattr++)
            {
                int attributestartpos = posstartpart;
                if (ishtml[0] == '<')
                {
                    attributestartpos += 1;
                }
                
                for (int i = 0; i < nattr; i++)
                {
                    attributestartpos += attributes[i].Length;
                }
                ValidateHTMLAttribute(attributes[nattr], rtb, attributestartpos);
            }

        }

        /// <summary>
        /// Validate HTML attribute
        /// </summary>
        /// <param name="htmltag"></param>
        /// <returns></returns>
        private static void ValidateHTMLAttribute(string htmlattribute, RichTextBox rtb, int attributestartpos)
        {
            String[] attr = htmlattribute.Split('=');
            for (int i = 0; i < langhtml.NumKeywords; i++)
            {
                if (langhtml.GetKeyword(i) == attr[0])
                {
                    // Right
                    ColorText(rtb, attributestartpos, attr[0].Length, Settings.HighlightHTMLColorValid);
                }
            }

            if (attr.Length > 1)
            {
                if (attr[1].StartsWith("="))
                {
                    // TODO string
                }
            }
        }

        /// <summary>
        /// Find out if it is a php keyword.
        /// </summary>
        /// <param name="isphp">A part to be check if this a php keyword.</param>
        /// <returns></returns>
        private static void ValidatingPhpPart(string isphp, RichTextBox rtb, int posstart, int len)
        {
            isphp = RemoveEnterAndTabChars(isphp);

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
            for (int i = 0; i < langphp.NumKeywords; i++)
            {
                if (isphp == langphp.GetKeyword(i))
                {
                    //todo
                }
            }

            // has string:
            for (int n = 0; n < isphp.Length; n++)
            {
                if (isphp[n] == '"')
                {
                    //todo
                }
            }

        }

        /// <summary>
        /// Find out if it is a sql keyword.
        /// </summary>
        /// <param name="issql">The part to be check.</param>
        /// <returns>true if a keyword matches issql part.</returns>
        private static void ValidatingSqlPart(string issql, RichTextBox rtb, int posstart, int len)
        {
            issql = RemoveEnterAndTabChars(issql);

            //for (int i = 0; i < keywordssql.Length; i++)
            //{
            //    if (issql == keywordssql[i])
            //    {
            //        return true;
            //    }
            //}

            //return false;
        }

        #endregion Methods
    }
}
