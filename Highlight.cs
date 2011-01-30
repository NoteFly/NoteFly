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
    using System.Drawing;
    using System.Windows.Forms;
    using System.IO;
    using System.Xml;
    using System.Collections.Generic;

    /// <summary>
    /// helper class for text highlight in notes.
    /// </summary>
    public static class Highlight
    {
		#region Fields (4) 

        private static bool isinit = false;
        private static string[] keywords_html;
        private static string[] keywords_php;
        private static string[] keywords_sql;

		#endregion Fields 

		#region Methods (3) 

		// Public Methods (2) 

        /// <summary>
        /// Clear the keywords list.
        /// </summary>
        public static void DeinitHighlighter()
        {
            isinit = false;
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
            GC.Collect();
        }

        /// <summary>
        /// Check syntax of the whole text
        /// </summary>
        //public static void CheckSyntaxFull(RichTextBox rtbcode, int skinnr, Notes notes)
        //{
        //    int cursorpos = rtbcode.SelectionStart;
        //    ResetHighlighting(rtbcode, skinnr, notes);
        //    bool htmlendnode = false;
        //    for (int i = 0; i < rtbcode.TextLength; i++)
        //    {
        //        if (Settings.HighlightHTML)
        //        {
        //            if (rtbcode.Text[i] == '<')
        //            {
        //                posstarttag = i;
        //            }
        //            else if (rtbcode.Text[i] == '>')
        //            {
        //                if (rtbcode.Text[posstarttag + 1] == '/')
        //                {
        //                    htmlendnode = true;
        //                }
        //                else
        //                {
        //                    htmlendnode = false;
        //                }
        //                int lengthtillendtag = i - posstarttag;
        //                if (lengthtillendtag > 1)
        //                {
        //                    string htmlnodename;
        //                    if (htmlendnode == true)
        //                    {
        //                        htmlnodename = rtbcode.Text.Substring(posstarttag + 2, lengthtillendtag - 2);
        //                    }
        //                    else
        //                    {
        //                        htmlnodename = rtbcode.Text.Substring(posstarttag + 1, lengthtillendtag - 1);
        //                    }

        //                    if (ValidingHTMLNode(htmlnodename, htmlendnode))
        //                    {
        //                        ColorText(rtbcode, posstarttag, lengthtillendtag + 1, Color.Blue);
        //                    }
        //                    else
        //                    {
        //                        ColorText(rtbcode, posstarttag, lengthtillendtag + 1, Color.Red);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    rtbcode.SelectionStart = cursorpos;
        //    rtbcode.SelectionLength = 0;
        //}

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
        /// Initializes TextHighlighter fill the keywords lists.
        /// </summary>
        /// <param name="rtb">The richedit note content</param>
        /// <param name="checkhtml">indicteds wheter html is gonna be checked</param>
        public static void InitHighlighter()
        {
            XmlTextReader xmlread = null;
            if (Settings.HighlightHTML)
            {
                keywords_html = xmlUtil.ParserLanguageLexical("lang_htm.xml");
            }
            if (Settings.HighlightPHP)
            {
                keywords_php = xmlUtil.ParserLanguageLexical("lang_htm.xml");
            }
            if (Settings.HighlightSQL)
            {
                keywords_sql = xmlUtil.ParserLanguageLexical("lang_htm.xml");
            }
            isinit = true;
        }
		// Private Methods (1) 

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
        //private static void ResetHighlighting(RichTextBox rtb, int skinnr, Notes notes)
        //{
        //    rtb.SelectAll();
        //    //rtb.SelectionColor = Color.Black;
        //    rtb.SelectionColor = notes.GetTextColor(skinnr);
        //    if (Settings.HighlightHTML)
        //    {
        //        //for (int i = 0; i < htmlstructure.Length; i++)
        //        //{
        //        //    htmlstructure[i] = false;
        //        //}
        //    }
        //}

        /// <summary>
        /// Find out if it is a html node.
        /// </summary>
        /// <param name="ishtml">Is it html to check.</param>
        /// <returns>true if it is html</returns>
        //private static bool ValidingHTMLNode(string ishtml, bool endnode)
        //{
        //    for (int i = 1; i < ishtml.Length; i++)
        //    {
        //        if (ishtml[i] == ' ')
        //        {
        //            ishtml = ishtml.Substring(0, i);
        //            break;
        //        }
        //        else if (i>11)
        //        {
        //            break;
        //        }
        //    }

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
