/* Copyright (C) 2009
 * 
 * This program is free software; you can redistribute it and/or modify it
 * Free Software Foundation; either version 2, or (at your option) any
 * later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA
 */
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NoteFly
{
    /// <summary>
    /// helper class for text highlight in notes.
    /// </summary>
    public class TextHighlight
    {
        #region Fields (7)

        private bool highlightC = false;
        private bool highlightHTML = false;
        private int posstarttag = 0;
        private RichTextBox rtbcode;
        private Regex SyntaxC = new Regex("if|else|for|while|{|}|do|define|#if|break|goto|continue|switch|case|default:|try|catch|throw|static");
        private Regex SyntaxCdatatype = new Regex("int|short|double|float|long|string|bool|char");
        //IngoreCase options is set.
        //todo
        private Regex SyntaxHTML = new Regex("<?HTML|<?HEAD|<?BODY^|<?A^|<?P|<?BR|<?SPAN^|<?I|<?U|<?B|<?OL|<?UL|<?IL|<?FONT^|<?TITLE|<?BLOCKQUOTE|<?META ^|<?LINK ^|<?CODE|<?DD|<?TABLE ^|<?DL|<?TD^|<?TR^|<?FORM ^|<?IMG ^|<?FRAME ^|<?STRONG|<?FRAMESET ^|<?IFRAME^|<?APPLET^|<?TH^|<?PRE^|<?HEAD|<?TFOOT|<?INPUT^|<?OPTION^|<?LABEL^|<?LEGEND^|<?SELECT^|<?TEXTAREA^|<?SCRIPT^|<?NOSCRIPT|<?S|<?STRIKE|<?TT|<?BIG|<?SMALL|<?BASEFONT^|<?DIV^|<?H1|<?H2|<?H3|<?H4|<?H5|<?H6|<?ADRESS|<?HR|<?EM", RegexOptions.IgnoreCase);

        #endregion Fields

        #region Constructors (1)

        public TextHighlight(bool highlightHTML, bool highlightC, RichTextBox temprtbcode)
        {
            this.highlightHTML = highlightHTML;
            this.highlightC = highlightC;
            this.rtbcode = temprtbcode;
        }

        #endregion Constructors

        #region Methods (4)

        // Public Methods (2) 

        /// <summary>
        /// Check syntax of the whole text
        /// </summary>
        /// <param name="rtb"></param>
        public bool CheckSyntaxFull()
        {
            int oldpos = rtbcode.SelectionStart;
            ResetHighlighting(rtbcode);
            int beginword = 0;
            int lenword = 0;
            for (int i = 0; i < rtbcode.TextLength; i++)
            {
                if (this.highlightHTML)
                {
                    if (rtbcode.Text[i] == '<')
                    {
                        posstarttag = i;
                    }
                    else if (rtbcode.Text[i] == '>')
                    {
                        int lengthtillendtag = i - posstarttag;
                        if (lengthtillendtag > 0)
                        {
                            if (ValidingHTMLNode(posstarttag, lengthtillendtag))
                            {
                                ColorText(posstarttag + lengthtillendtag + 1, 0, Color.Blue);
                            }
                            else
                            {
                                ColorText(posstarttag + lengthtillendtag + 1, 0, Color.Red);
                            }

                            ColorText(posstarttag + lengthtillendtag + 1, 0, Color.Black);
                        }
                    }
                    if (rtbcode.TextLength >= i)
                    {
                        ColorText(i + 1, 0, Color.Black);
                    }
                    if (this.highlightC)
                    {
                        lenword++;
                        if ((rtbcode.Text[i] == ' ') || (rtbcode.Text[i] == '\n'))
                        {
                            String iscode = rtbcode.Text.Substring(beginword, lenword);
                            if (SyntaxC.IsMatch(iscode))
                            {
                                ColorText(beginword, lenword, Color.Green);
                            }
                            else if (SyntaxCdatatype.IsMatch(iscode))
                            {
                                ColorText(beginword, lenword, Color.Gray);
                            }
                            beginword = i;
                            lenword = 0;
                        }
                    }

                }

            }
            rtbcode.SelectionStart = oldpos;
            return true;
        }

        /// <summary>
        /// This does only finds the last node for highlighting.
        /// The rest stays the same.
        /// </summary>
        public void CheckSyntaxQuick(int pos)
        {
            Boolean foundendtag = false;
            for (int i = pos; ((i > 0) && (!foundendtag)); i--)
            {
                if (this.highlightHTML)
                {
                    try
                    {
                        if (rtbcode.Text[i - 1] == '<')
                        {
                            for (int n = i; ((n < rtbcode.TextLength) && (!foundendtag)); n++)
                            {

                                if (rtbcode.Text[n] == '>')
                                {
                                    foundendtag = true;
                                    posstarttag = i - 1;
                                    if (ValidingHTMLNode(posstarttag, n - posstarttag))
                                    {
                                        ColorText(posstarttag, (n + 1 - posstarttag), Color.Blue);
                                    }
                                    else
                                    {
                                        ColorText(posstarttag, (n + 1 - posstarttag), Color.Red);
                                    }
                                }

                            }
                            ColorText(rtbcode.TextLength, 0, Color.Black);
                            //rtbcode.SelectionStart = rtbcode.TextLength;
                            //rtbcode.SelectionColor = Color.Black;
                        }
                    }
                    catch (ArgumentOutOfRangeException arg)
                    {
                        throw new CustomExceptions("Quick TextHighlighter out of range: " + arg.Source);
                    }
                }

            }
        }
        // Private Methods (2) 

        /// <summary>
        /// Make everything black again.
        /// </summary>
        /// <param name="rtb"></param>
        private void ResetHighlighting(RichTextBox rtb)
        {
            rtb.SelectAll();
            rtb.SelectionColor = Color.Black;
        }

        private Boolean ValidingHTMLNode(int posstarttag, int lengthtillendtag)
        {
            string ishtmlnode = rtbcode.Text.Substring(posstarttag, lengthtillendtag);
            if (ishtmlnode.Length > 1)
            {
                if (SyntaxHTML.IsMatch(ishtmlnode))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void ColorText(int posstart, int len, Color syncolor)
        {
            try
            {
                rtbcode.Select(posstart, len);
                rtbcode.SelectionColor = syncolor;
            }
            catch (ArgumentOutOfRangeException arg)
            {
                throw new CustomExceptions("TextHighlighter out of range: " + arg.Source);
            }
        }

        #endregion Methods
    }
}
