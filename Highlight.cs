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
            if (Settings.HighlightHTML)
            {
                keywords_html = xmlUtil.ParserLanguageLexical("langs.xml", "html");
            }

            if (Settings.HighlightPHP)
            {
                keywords_php = xmlUtil.ParserLanguageLexical("langs.xml", "php");
            }

            if (Settings.HighlightSQL)
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
            if (Settings.HighlightHTML || Settings.HighlightPHP || Settings.HighlightSQL)
            {
                if (!keywordsinit)
                {
                    Log.Write(LogType.error, "Keywords not initialized as they should already have. Hotfixing this, watchout memory use.");
                    InitHighlighter();
                }
                int posstarttag = 0;
                int poslastspace = 0;
                bool htmlendtag = false;
                int startphpdoc = 0;
                int endphpdoc = 0;
                for (int i = 0; i < rtb.TextLength; i++)
                {
                    if (Settings.HighlightHTML)
                    {
                        if (rtb.Text[i] == '<')
                        {
                            posstarttag = i;
                        }
                        else if (rtb.Text[i] == '>')
                        {
                            htmlendtag = false;
                            if (rtb.Text[posstarttag + 1] == '/')
                            {
                                htmlendtag = true;
                            }
                            int diffstarttagendtag = i - posstarttag;
                            if (diffstarttagendtag > 0)
                            {
                                string htmlnodename;
                                if (htmlendtag)
                                {
                                    htmlnodename = rtb.Text.Substring(posstarttag + 2, diffstarttagendtag - 2);
                                }
                                else
                                {
                                    htmlnodename = rtb.Text.Substring(posstarttag + 1, diffstarttagendtag - 1);
                                }

                                if (ValidatingHtmlTag(htmlnodename, htmlendtag))
                                {
                                    ColorText(rtb, posstarttag, diffstarttagendtag + 1, Color.Blue);
                                }
                                else
                                {
                                    ColorText(rtb, posstarttag, diffstarttagendtag + 1, Color.Red); //+1 for the > character
                                }
                            }
                        }
                    }
                    if ((Settings.HighlightPHP) || (Settings.HighlightSQL))
                    {
                        if (rtb.Text[i] == '<' || rtb.Text[i] == '>')
                        {
                            if (i + 3 < rtb.Text.Length)
                            {
                                string isstartphpdoc = rtb.Text.Substring(i, 5);
                                if (isstartphpdoc.ToLower() == "<?php")
                                {
                                    startphpdoc = i;
                                }
                            }
                            else if (i > 1)
                            {
                                if (rtb.Text.Substring(i - 1, 2) == "?>")
                                {
                                    endphpdoc = i;
                                }
                            }
                        }
                        else if (rtb.Text[i] == ' ' || i == rtb.Text.Length - 1)
                        {
                            if (i > 1 && i > startphpdoc && i < endphpdoc)
                            {
                                //php
                                int diffstartend = i - poslastspace;
                                string isphp = rtb.Text.Substring(poslastspace, diffstartend);
                                if (ValidatingPhp(isphp))
                                {
                                    ColorText(rtb, poslastspace, diffstartend, Color.Blue);
                                }
                                else
                                {
                                    ColorText(rtb, poslastspace, diffstartend, Color.DarkRed);
                                }
                            }

                            poslastspace = i;
                        }
                    }
                }
            }
            rtb.SelectionStart = cursorpos;
        }


        /// <summary>
        /// highlight the change.
        /// </summary>
        /// <param name="newcharpos">The position where the new charcter is typed.</param>
        //public static void CheckSyntaxQuick(RichTextBox rtbcode, int newcharpos)
        //{
        //    if (newcharpos > 0)
        //    {
        //        int cursorpos = rtbcode.SelectionStart;
        //        if (Settings.HighlightHTML)
        //        {
        //            if (newcharpos < 0)
        //            {
        //                throw new CustomException("negative character location.");
        //            }

        //            if (rtbcode.Text[newcharpos] == '<')
        //            {
        //                for (int i = newcharpos; i > 0; i--)
        //                {
        //                    if (rtbcode.Text[i] == '<')
        //                    {
        //                        ColorText(rtbcode, newcharpos, 1, Color.Red);
        //                        break;
        //                    }
        //                    else if (rtbcode.Text[i] == '>')
        //                    {
        //                        ColorText(rtbcode, newcharpos, 1, Color.Black);
        //                        break;
        //                    }
        //                }
        //            }
        //            else if (rtbcode.Text[newcharpos] == '>')
        //            {
        //                string htmlnodename = String.Empty;
        //                int htmlnodestartpos = -1;
        //                bool htmlendnode = false;
        //                for (int i = newcharpos; i >= 0; i--)
        //                {
        //                    try
        //                    {
        //                        int chkpos = i - 1;
        //                        if (chkpos < 0)
        //                        {
        //                            chkpos = 0;
        //                        }
        //                        if (rtbcode.Text[i] == '<')
        //                        {
        //                            htmlnodestartpos = i;
        //                            htmlnodename = rtbcode.Text.Substring(i + 1, newcharpos - 1 - i);
        //                            break;
        //                        }
        //                        else if (rtbcode.Text[i] == '/' && rtbcode.Text[chkpos] == '<')
        //                        {
        //                            htmlnodestartpos = i - 1;
        //                            htmlnodename = rtbcode.Text.Substring(i + 1, newcharpos - 1 - i);
        //                            htmlendnode = true;
        //                            break;
        //                        }
        //                    }
        //                    catch (IndexOutOfRangeException outofrangeexc)
        //                    {
        //                        throw new CustomException(outofrangeexc.Message + " " + outofrangeexc.StackTrace);
        //                    }
        //                }

        //                if ((!String.IsNullOrEmpty(htmlnodename)) && (htmlnodestartpos != -1))
        //                {
        //                    if (ValidingHTMLNode(htmlnodename, htmlendnode))
        //                    {
        //                        ColorText(rtbcode, htmlnodestartpos, newcharpos - htmlnodestartpos + 1, Color.Blue);
        //                    }
        //                    else
        //                    {
        //                        ColorText(rtbcode, htmlnodestartpos, newcharpos - htmlnodestartpos + 1, Color.Red);
        //                    }
        //                }
        //                else
        //                {
        //                    ColorText(rtbcode, newcharpos, newcharpos + 1, Color.Black);
        //                }
        //            }
        //            else
        //            {
        //                ColorText(rtbcode, newcharpos, 1, Color.Black);
        //            }
        //        }
        //        rtbcode.SelectionStart = cursorpos;
        //        rtbcode.SelectionLength = 0;
        //    }
        //}

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
        /// Find out if it is a html tag.
        /// </summary>
        /// <param name="ishtml">Is it html to check.</param>
        /// <returns>true if it is html</returns>
        private static bool ValidatingHtmlTag(string ishtml, bool endtag)
        {
            if (endtag)
            {
                ishtml = ishtml.TrimStart('/');
            }
            ishtml = ishtml.ToLower();
            for (int i = 0; i < keywords_html.Length; i++)
            {
                if (ishtml == keywords_html[i])
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Find out if it is a php keyword.
        /// </summary>
        /// <param name="isphp"></param>
        /// <returns></returns>
        private static bool ValidatingPhp(string isphp)
        {
            isphp.ToLower();
            for (int i = 0; i < keywords_php.Length; i++)
            {
                if (isphp == keywords_php[i])
                {
                    return true;
                }
            }
            return false;
        }


        //    int startsearch = 0;
        //    if (((ishtml[0] > 76) && (ishtml[0] < 91)) || ((ishtml[0] > 108) && (ishtml[0] < 123)))
        //    {
        //        startsearch = 55;
        //    }
        //    for (int n = startsearch; n < htmltags.Length; n++)
        //    {
        //        if (ishtml.ToUpper() == htmltags[n])
        //        {
        //            if (endnode)
        //            {
        //                htmlstructure[n] = false;
        //                if (htmlendtagpolicy[n] == 2)
        //                {
        //                    //forbidden endtag
        //                    return false;
        //                }
        //            }
        //            else
        //            {
        //                if (!htmlstructure[n])
        //                {
        //                    htmlstructure[n] = true;
        //                }
        //                else
        //                {
        //                    //Check if endtag ommiting is allowed before returning false.
        //                    if (htmlendtagpolicy[n] == 1)
        //                    {
        //                        return false;
        //                    }
        //                }
        //            }

        //            return true;
        //        }
        //    }

        //    return false;
        //}

        #endregion Methods
    }

}
