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

        private RichTextBox rtbcode;
        private bool highlightC = false;
        private bool highlightHTML = false;
        private int posstarttag = 0;
        private Regex SyntaxC = new Regex("if|else|for|while|{|}|do|define|#if|break|goto|continue|switch|case|default:|try|catch|throw|static");
        private Regex SyntaxCdatatype = new Regex("int|short|double|float|long|string|bool|char");
        //IngoreCase options is set.
        private Regex SyntaxHTML = new Regex("<HTML|<HEAD|<BODY^|<A^|<P|<BR|<SPAN^|<I|<U|<B|<OL|<UL|<IL|<FONT^|<TITLE|<BLOCKQUOTE|<META^|<LINK^|<CODE|<DD|<TABLE^|<DL|<TD^|<TR^|<FORM^|<IMG^|<FRAME^|<STRONG|<FRAMESET^|<IFRAME^|<APPLET^|<TH^|<PRE^|<HEAD|<TFOOT|<INPUT^|<OPTION^|<LABEL^|<LEGEND^|<SELECT^|<TEXTAREA^|<SCRIPT^|<NOSCRIPT|<S|<STRIKE|<TT|<BIG|<SMALL|<BASEFONT^|<DIV^|<H1|<H2|<H3|<H4|<H5|<H6|<ADRESS|<HR|<EM", RegexOptions.IgnoreCase);        

        #endregion Fields

        #region Constructors (1)

        public TextHighlight(bool highlightHTML, bool highlightC, RichTextBox temprtbcode)
        {
            this.highlightHTML = highlightHTML;
            this.highlightC = highlightC;
            this.rtbcode = temprtbcode;
        }

        #endregion Constructors

        #region Methods (1)

        // Public Methods (1) 

        /// <summary>
        /// Check syntax of the whole text
        /// </summary>
        /// <param name="rtb"></param>
        public bool CheckSyntaxFull()
        {
            int oldpos = rtbcode.SelectionStart;
            ResetHighlighting(rtbcode);            
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
                            try
                            {
                                if (ValidingHTMLNode(posstarttag, lengthtillendtag))
                                {
                                    rtbcode.Select(posstarttag, posstarttag + lengthtillendtag);
                                    rtbcode.SelectionColor = Color.Blue;
                                }
                                else
                                {
                                    rtbcode.Select(posstarttag, lengthtillendtag + 1);
                                    rtbcode.SelectionColor = System.Drawing.Color.Red;
                                }
                                rtbcode.SelectionStart = posstarttag + lengthtillendtag + 1;
                                rtbcode.SelectionColor = Color.Black;
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                return false;
                            }
                        }
                    }
                    if (rtbcode.TextLength >= i)
                    {
                        try
                        {
                            rtbcode.SelectionStart = i + 1;
                            rtbcode.SelectionColor = System.Drawing.Color.Black;
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
                    int selPos = rtbcode.SelectionStart;
                    foreach (Match keyWordMatch in SyntaxC.Matches(rtbcode.Text))
                    {
                        rtbcode.Select(keyWordMatch.Index, keyWordMatch.Length);
                        rtbcode.SelectionColor = Color.Green;
                        rtbcode.SelectionStart = selPos;
                        rtbcode.SelectionColor = Color.Black;
                    }
                    foreach (Match keyWordMatch in SyntaxCdatatype.Matches(rtbcode.Text))
                    {
                        rtbcode.Select(keyWordMatch.Index, keyWordMatch.Length);
                        rtbcode.SelectionColor = Color.Gray;
                        rtbcode.SelectionStart = selPos;
                        rtbcode.SelectionColor = Color.Black;
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
        public void CheckSyntaxQuick()
        {
            Boolean foundendtag = false;
            for (int i = rtbcode.TextLength; ((i > 0) && (!foundendtag)); i--)
            {
                if (this.highlightHTML)
                {
                    if (rtbcode.Text[i-1] == '<')
                    {                        
                        for (int n = i; ((n < rtbcode.TextLength) && (!foundendtag)); n++)
                        {
                            if (rtbcode.Text[n] == '>')
                            {
                                foundendtag = true;
                                posstarttag = i - 1;
                                if (ValidingHTMLNode(posstarttag, n - posstarttag))
                                {
                                    rtbcode.Select(posstarttag, (n+1 - posstarttag));
                                    rtbcode.SelectionColor = Color.Blue;
                                }
                                else
                                {
                                    rtbcode.Select(posstarttag, (n+1 - posstarttag));
                                    rtbcode.SelectionColor = System.Drawing.Color.Red;
                                }                                
                            }
                        }
                        rtbcode.SelectionStart = rtbcode.TextLength;
                        rtbcode.SelectionColor = Color.Black;
                    }
                }
            }            
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
