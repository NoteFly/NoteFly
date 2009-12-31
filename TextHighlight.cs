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
using System.Windows.Forms;

namespace NoteFly
{
    /// <summary>
    /// helper class for text highlight in notes.
    /// </summary>
    public class TextHighlight
    {
		#region Fields (4) 

        private Boolean checkhtml;
        private String[] htmlnodes;
        private int posstarttag = 0;
        private RichTextBox rtbcode;

		#endregion Fields 

		#region Constructors (1) 

        public TextHighlight(RichTextBox rtb, Boolean checkhtml)
        {
            this.rtbcode = rtb;
            this.checkhtml = checkhtml;
            if (checkhtml)
            {
                htmlnodes = new String[91];
                htmlnodes[0] = "A"; htmlnodes[1] = "ABBR";
                htmlnodes[2] = "ACRONYM"; htmlnodes[3] = "ADDRESS";
                htmlnodes[4] = "APPLET"; htmlnodes[5] = "B";
                htmlnodes[6] = "BASE"; htmlnodes[7] = "BASEFONT";
                htmlnodes[8] = "BDO"; htmlnodes[9] = "BIG";
                htmlnodes[10] = "BLOCKQUOTE"; htmlnodes[11] = "BODY";
                htmlnodes[12] = "BR"; htmlnodes[13] = "BUTTON";
                htmlnodes[14] = "CAPTION"; htmlnodes[15] = "CENTER";
                htmlnodes[16] = "CITE"; htmlnodes[17] = "CODE";
                htmlnodes[18] = "COL"; htmlnodes[19] = "COLGROUP";
                htmlnodes[20] = "DD"; htmlnodes[21] = "DEL";
                htmlnodes[22] = "DFN"; htmlnodes[23] = "DIR";
                htmlnodes[24] = "DIV"; htmlnodes[25] = "DL";
                htmlnodes[26] = "DT"; htmlnodes[27] = "EM";
                htmlnodes[28] = "FIELDSET"; htmlnodes[29] = "FONT";
                htmlnodes[30] = "FORM"; htmlnodes[31] = "FRAME";
                htmlnodes[32] = "FRAMESET"; htmlnodes[33] = "HEAD";
                htmlnodes[34] = "HR"; htmlnodes[35] = "HTML";
                htmlnodes[36] = "Hx"; htmlnodes[37] = "I";
                htmlnodes[38] = "IFRAME"; htmlnodes[39] = "IMG";
                htmlnodes[40] = "INPUT"; htmlnodes[41] = "INS";
                htmlnodes[42] = "ISINDEX"; htmlnodes[43] = "KBD";
                htmlnodes[44] = "LABEL"; htmlnodes[45] = "LEGEND";
                htmlnodes[46] = "LI"; htmlnodes[47] = "LINK";
                htmlnodes[48] = "MAP"; htmlnodes[49] = "MENU";
                htmlnodes[50] = "META"; htmlnodes[51] = "NOFRAMES";
                htmlnodes[52] = "NOSCRIPT"; htmlnodes[53] = "OBJECT";
                htmlnodes[54] = "OL"; htmlnodes[55] = "OPTGROUP";
                htmlnodes[56] = "OPTION"; htmlnodes[57] = "P";
                htmlnodes[58] = "PARAM"; htmlnodes[59] = "PRE";
                htmlnodes[60] = "Q"; htmlnodes[61] = "S";
                htmlnodes[62] = "SAMP"; htmlnodes[63] = "SCRIPT";
                htmlnodes[64] = "SELECT"; htmlnodes[65] = "SMALL";
                htmlnodes[66] = "SPAN"; htmlnodes[67] = "STRIKE";
                htmlnodes[68] = "STRONG"; htmlnodes[69] = "STYLE";
                htmlnodes[70] = "SUB"; htmlnodes[71] = "SUP";
                htmlnodes[72] = "TABLE"; htmlnodes[73] = "TBODY";
                htmlnodes[74] = "TD"; htmlnodes[75] = "TEXTAREA";
                htmlnodes[76] = "TFOOT"; htmlnodes[77] = "TH";
                htmlnodes[78] = "THEAD"; htmlnodes[79] = "TITLE";
                htmlnodes[80] = "TR"; htmlnodes[81] = "TT";
                htmlnodes[82] = "U"; htmlnodes[83] = "UL";
                htmlnodes[84] = "VAR"; htmlnodes[85] = "H1";
                htmlnodes[86] = "H2"; htmlnodes[87] = "H3";
                htmlnodes[88] = "H4"; htmlnodes[89] = "H5";
                htmlnodes[90] = "H6";
            }
        }

		#endregion Constructors 

		#region Methods (2) 

		// Public Methods (2) 

        /// <summary>
        /// Check syntax of the whole text
        /// </summary>
        /// <param name="rtb"></param>
        public void CheckSyntaxFull()
        {
            int cursorpos = rtbcode.SelectionStart;
            ResetHighlighting(rtbcode);

            bool htmlendnode = false;
            for (int i = 0; i < rtbcode.TextLength; i++)
            {
                if (checkhtml)
                {
                    if (rtbcode.Text[i] == '<')
                    {
                        posstarttag = i;
                        if (i + 1 < rtbcode.TextLength)
                        {
                            if (rtbcode.Text[i+1] == '/')
                            {
                                htmlendnode = true;
                            }
                        }
                    }
                    else if (rtbcode.Text[i] == '>')
                    {
                        int lengthtillendtag = i - posstarttag;
                        if (lengthtillendtag > 1)
                        {
                            string htmlnodename;
                            if (htmlendnode == true)
                            {
                                htmlnodename = rtbcode.Text.Substring(posstarttag + 2, lengthtillendtag - 2);
                                htmlendnode = false;
                            } else
                            {
                                htmlnodename  = rtbcode.Text.Substring(posstarttag + 1, lengthtillendtag - 1);
                            }
                            if (ValidingHTMLNode(htmlnodename))
                            {
                                ColorText(posstarttag, lengthtillendtag + 1, Color.Blue);
                            }
                            else
                            {
                                ColorText(posstarttag, lengthtillendtag + 1, Color.Red);
                            }
                        }
                    }
                }
            }
            rtbcode.SelectionStart = cursorpos;
            rtbcode.SelectionLength = 0;
        }

        /// <summary>
        /// highlight the change.
        /// </summary>
        public void CheckSyntaxQuick(int newcharpos)
        {
            bool htmlstarttag = false;
            if (newcharpos > 0)
            {
                MessageBox.Show("u tikte: " + rtbcode.Text[newcharpos]);
            }
            /*
            for (int i = newcharpos; ((i > 0) && (!foundendtag)); i--)
            {

                /*
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
                        throw new CustomException("Quick TextHighlighter out of range: " + arg.Source);
                    }
                }
                 */
        }

        /// <summary>
        /// Make the whole text black.
        /// </summary>
        /// <param name="rtb"></param>
        private void ResetHighlighting(RichTextBox rtb)
        {
            rtb.SelectAll();
            rtb.SelectionColor = Color.Black;
        }

        /// <summary>
        /// Find out if it is a html node between < or </ and > 
        /// </summary>
        /// <returns>true if it is html</returns>
        private Boolean ValidingHTMLNode(string ishtml)
        {
            for (int i = 1; i < ishtml.Length; i++)
            {
                if (ishtml[i] == ' ')
                {
                    ishtml = ishtml.Substring(0, i);
                    break;
                }
            }
            for (int n = 0; n < htmlnodes.Length; n++)
            {
                if (ishtml == htmlnodes[n] || ishtml == htmlnodes[n].ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        private void ColorText(int posstart, int len, Color color)
        {
            try
            {
                rtbcode.Select(posstart, len);
                rtbcode.SelectionColor = color;
            }
            catch (ArgumentOutOfRangeException arg)
            {
                throw new CustomException("TextHighlighter out of range: " + arg.Source);
            }
        }

    }

		#endregion Methods 
}
