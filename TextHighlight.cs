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
        private int starttag = 0;
        private int endtag = 0;

        #endregion Fields

        public TextHighlight(bool highlightC, bool highlightHTML)
        {
            this.highlightC = highlightC;
            this.highlightHTML = highlightHTML;
        }

        #region Properties (2)

        public Regex getRegexC
        {
            get
            {
                return this.SyntaxC;
            }
        }

        public Regex getRegexHTML
        {
            get
            {
                return this.SyntaxHTML;
            }
        }

        #endregion Properties

        /// <summary>
        /// Check syntax..
        /// </summary>
        /// <param name="rtb"></param>
        public void CheckSyntax(RichTextBox rtb)
        {
            for (int i = 0; i < rtb.TextLength; i++)
            {
                if (this.highlightHTML)
                {
                    if (rtb.Text[i] == '<')
                    {
                        starttag = i;                        
                    }
                    else if (rtb.Text[i] == '>')
                    {
                        endtag = i;
                        rtb.Select(starttag, endtag);
                        rtb.SelectionColor = System.Drawing.Color.Blue;
                        if (rtb.TextLength >= i)
                        {
                            rtb.SelectionStart = i + 1;
                            rtb.SelectionColor = System.Drawing.Color.Black;
                        }
                        
                        /*
                        foreach (Match keyWordMatch in this.getRegexHTML.Matches(rtb.Text, starttag))
                        {
                            rtb.Select(keyWordMatch.Index, keyWordMatch.Length);
                            rtb.SelectionColor = System.Drawing.Color.Blue;

                            int endpos = keyWordMatch.Length;
                            if (rtb.Text[endpos] == '>')
                            {
                                rtb.SelectionStart = endpos;
                                rtb.SelectionColor = System.Drawing.Color.Black;
                            }
                        } 
                         */
                    }
                }                              
            }

            if (this.highlightC)
            {                
                foreach (System.Text.RegularExpressions.Match keyWordMatch in getRegexC.Matches(rtb.Text))
                {
                    rtb.Select(keyWordMatch.Index, keyWordMatch.Length);
                    rtb.SelectionColor = System.Drawing.Color.GreenYellow;
                    rtb.SelectionStart = keyWordMatch.Index + keyWordMatch.Length;
                    rtb.SelectionColor = System.Drawing.Color.Black;
                }
            } 
        }
    }
}
