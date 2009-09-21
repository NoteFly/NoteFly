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
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;

namespace SimplePlainNote
{
    /// <summary>
    /// helper class for text highlight in notes.
    /// </summary>
    public class TextHighlight
    {
        #region Fields (2)

        private Regex SyntaxCdatatype = new Regex("int|short|double|float|long|string|bool|char");

        private Regex SyntaxC = new Regex("if|else|for|while|{|}|do|define|#if|break|goto|continue|switch|case|default:|try|catch|throw|static");

        private Regex SyntaxHTML = new Regex("!DOCTYPE|HTML|/HTML|BODY|BODY|/BODY|A HREF|SPAN|I|/I|U|/U|" +
            "B|/B|UL|IL|OL|/OL|BR|BR /|P|/P|FONT|/FONT|TITLE|/TITLE|BLOCKQUOTE|/BLOCKQUOTE|META|LINK|CODE|/CODE|" +
            "DD|/DD|TABLE|/TABLE|DL|/DL|TD|/TD|TR|/TR|FORM|IMG|FRAME|STRONG|/STRONG|FRAMESET|/FRAMESET|IFRAME|" +
            "/IFRAME|APPLET|/APPLET|TH|/TH|PRE|/PRE|HEAD|/THEAD|TFOOT|/TFOOT|INPUT|OPTION|LABEL|/LABEL|LEGEND|" +
            "/LEGEND|ISINDEX|SELECT|/SELECT|TEXTAREA|/TEXTAREA|SCRIPT|</SCRIPT|NOSCRIPT|/NOSCRIPT|S|/S|" +
            "STRIKE|/STRIKE|TT|/TT|BIG|/BIG|SMALL|/SMALL|BASEFONT|/BASEFONT|DIV|/DIV|H1|/H1|H2|/H2|H3|/H3|" +
            "H4|/H4|H5|/H5|H6|/H6|HEAD|ADRESS|/ADRESS|/HEAD|HR|EM|/EM", RegexOptions.IgnoreCase);

        private bool highlightC = false;

        private bool highlightHTML = false;

        private int posstarttag = 0;

        #endregion Fields

        public TextHighlight(bool highlightC, bool highlightHTML)
        {
            this.highlightC = highlightC;
            this.highlightHTML = highlightHTML;
        }

        #region Properties (0)

        #endregion Properties

        /// <summary>
        /// Check syntax..
        /// </summary>
        /// <param name="rtb"></param>
        public bool CheckSyntax(RichTextBox rtb)
        {
            for (int i = 0; i < rtb.TextLength; i++)
            {
                if (this.highlightHTML)
                {
                    if (rtb.Text[i] == '<')
                    {
                        posstarttag = i;
                    }
                    else if (rtb.Text[i] == '>')
                    {
                        int lengthtillendtag = i - posstarttag - 1;

                        string temp = rtb.Text.Substring(posstarttag + 1, lengthtillendtag);
                        try
                        {
                            if (SyntaxHTML.IsMatch(temp, posstarttag) == true)
                            {
                                rtb.Select(posstarttag + 1, lengthtillendtag);
                                rtb.SelectionColor = System.Drawing.Color.Blue;
                            }
                            else
                            {
                                rtb.Select(posstarttag + 1, lengthtillendtag);
                                rtb.SelectionColor = System.Drawing.Color.Red;
                            }
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            return false;
                        }
                        if (rtb.TextLength >= i)
                        {
                            try
                            {
                                rtb.SelectionStart = i + 1;
                                rtb.SelectionColor = System.Drawing.Color.Black;
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            rtb.Select(rtb.TextLength, 0);
            return true;
        }
    }
}
