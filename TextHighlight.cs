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
        #region Fields (6)

        private bool highlightC = false;
        private bool highlightHTML = false;
        private int posstarttag = 0;
        private Regex SyntaxC = new Regex("if|else|for|while|{|}|do|define|#if|break|goto|continue|switch|case|default:|try|catch|throw|static");
        private Regex SyntaxCdatatype = new Regex("int|short|double|float|long|string|bool|char");
        private Regex SyntaxHTML = new Regex("HTML|HEAD|BODY|"+
        "A|SPAN|I|U|B|UL|IL|OL|BR|P|FONT|"+
        "TITLE|BLOCKQUOTE|META|LINK|CODE|DD|"+
        "TABLE|DL|TD|TR|FORM|IMG|FRAME|STRONG|"+
        "FRAMESET|IFRAME|APPLET|TH|PRE|"+
        "HEAD|TFOOT|INPUT|OPTION|LABEL|LEGEND|"+
        "SELECT|TEXTAREA|SCRIPT|NOSCRIPT|"+
        "S|STRIKE||TT|BIG|SMALL|BASEFONT|"+
        "DIV|H1|H2|H3|H4|H5|H6|ADRESS|HR|EM", RegexOptions.IgnoreCase);

        #endregion Fields

        #region Constructors (1)

        public TextHighlight(bool highlightHTML, bool highlightC)
        {            
            this.highlightHTML = highlightHTML;
            this.highlightC = highlightC;
        }

        #endregion Constructors

        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Check syntax
        /// </summary>
        /// <param name="rtb"></param>
        public bool CheckSyntax(RichTextBox rtb)
        {         
            int oldpos = rtb.SelectionStart;
            ResetHighlighting(rtb);
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
                        int lengthtillendtag = i - posstarttag;
                        if (lengthtillendtag > 0)
                        {
                            try
                            {                                
                                string ishtmlnode = rtb.Text.Substring(posstarttag + 1, lengthtillendtag-1);
                                //FIXME: does always match..
                                foreach (Match keyWordHTML in SyntaxHTML.Matches(ishtmlnode))
                                {
                                    rtb.Select(posstarttag, lengthtillendtag+1);
                                    rtb.SelectionColor = Color.Blue;
                                    rtb.SelectionStart = lengthtillendtag + 1;
                                    rtb.SelectionColor = Color.Black;
                                }
                                /*
                                else
                                {
                                    rtb.Select(posstarttag, lengthtillendtag+1);
                                    rtb.SelectionColor = System.Drawing.Color.Red;
                                }
                                 */
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                return false;
                            }
                        }
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
                if (this.highlightC)
                {
                    //todo: make more effective only 1 loop.
                    int selPos = rtb.SelectionStart;                    
                    foreach (Match keyWordMatch in SyntaxC.Matches(rtb.Text))
                    {
                        rtb.Select(keyWordMatch.Index, keyWordMatch.Length);
                        rtb.SelectionColor = Color.Green;
                        rtb.SelectionStart = selPos;
                        rtb.SelectionColor = Color.Black;
                    }
                    foreach (Match keyWordMatch in SyntaxCdatatype.Matches(rtb.Text))
                    {
                        rtb.Select(keyWordMatch.Index, keyWordMatch.Length);
                        rtb.SelectionColor = Color.Gray;
                        rtb.SelectionStart = selPos;
                        rtb.SelectionColor = Color.Black;
                    }
                }
            }
            rtb.SelectionStart = oldpos;
            return true;
        }

        /// <summary>
        /// Make everything black again.
        /// </summary>
        /// <param name="rtb"></param>
        private void ResetHighlighting(RichTextBox rtb)
        {
            rtb.SelectAll();
            rtb.SelectionColor = Color.Black;
        }

        #endregion Methods
    }
}
