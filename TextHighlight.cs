//-----------------------------------------------------------------------
// <copyright file="TextHighlight.cs" company="GNU">
// 
// This program is free software; you can redistribute it and/or modify it
// Free Software Foundation; either version 2, 
// or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// </copyright>
//-----------------------------------------------------------------------
namespace NoteFly
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// helper class for text highlight in notes.
    /// </summary>
    public class TextHighlight
    {
        #region Fields (4)

        /// <summary>
        /// Boolean that tells if html is being checked.
        /// </summary>
        private bool checkhtml;

        /// <summary>
        /// A array of possible HTML nodes.
        /// </summary>
        private string[] htmlnodes;

        /// <summary>
        /// The start position of a html tag.
        /// </summary>
        private int posstarttag = 0;

        /// <summary>
        /// The rich content of the note.
        /// </summary>
        private RichTextBox rtbcode;

        private bool[] htmlstructure;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the TextHighlight class.
        /// </summary>
        /// <param name="rtb">The richedit note content</param>
        /// <param name="checkhtml">indicteds wheter html is gonna be checked</param>
        public TextHighlight(RichTextBox rtb, bool checkhtml)
        {
            this.rtbcode = rtb;
            this.checkhtml = checkhtml;

            if (checkhtml)
            {
                this.htmlnodes = new string[92];
                this.htmlnodes[0] = "A";
                this.htmlnodes[1] = "ABBR";
                this.htmlnodes[2] = "ACRONYM";
                this.htmlnodes[3] = "ADDRESS";
                this.htmlnodes[4] = "APPLET";
                this.htmlnodes[5] = "B";
                this.htmlnodes[6] = "BASE";
                this.htmlnodes[7] = "BASEFONT";
                this.htmlnodes[8] = "BDO";
                this.htmlnodes[9] = "BIG";
                this.htmlnodes[10] = "BLOCKQUOTE";
                this.htmlnodes[11] = "BODY";
                this.htmlnodes[12] = "BR";
                this.htmlnodes[13] = "BUTTON";
                this.htmlnodes[14] = "CAPTION";
                this.htmlnodes[15] = "CENTER";
                this.htmlnodes[16] = "CITE";
                this.htmlnodes[17] = "CODE";
                this.htmlnodes[18] = "COL";
                this.htmlnodes[19] = "COLGROUP";
                this.htmlnodes[20] = "DD";
                this.htmlnodes[21] = "DEL";
                this.htmlnodes[22] = "DFN";
                this.htmlnodes[23] = "DIR";
                this.htmlnodes[24] = "DIV";
                this.htmlnodes[25] = "DL";
                this.htmlnodes[26] = "DT";
                this.htmlnodes[27] = "EM";
                this.htmlnodes[28] = "FIELDSET";
                this.htmlnodes[29] = "FONT";
                this.htmlnodes[30] = "FORM";
                this.htmlnodes[31] = "FRAME";
                this.htmlnodes[32] = "FRAMESET";
                this.htmlnodes[33] = "H1";
                this.htmlnodes[34] = "H2";
                this.htmlnodes[35] = "H3";
                this.htmlnodes[36] = "H4";
                this.htmlnodes[37] = "H5";
                this.htmlnodes[38] = "H6";
                this.htmlnodes[39] = "HEAD";
                this.htmlnodes[40] = "HR";
                this.htmlnodes[41] = "HTML";
                this.htmlnodes[42] = "Hx";
                this.htmlnodes[43] = "I";
                this.htmlnodes[44] = "IFRAME";
                this.htmlnodes[45] = "IMG";
                this.htmlnodes[46] = "INPUT";
                this.htmlnodes[47] = "INS";
                this.htmlnodes[48] = "ISINDEX";
                this.htmlnodes[49] = "KBD";
                this.htmlnodes[50] = "LABEL";
                this.htmlnodes[51] = "LEGEND";
                this.htmlnodes[52] = "LI";
                this.htmlnodes[53] = "LINK";
                this.htmlnodes[54] = "MAP";
                this.htmlnodes[55] = "MENU";
                this.htmlnodes[56] = "META";
                this.htmlnodes[57] = "NOFRAMES";
                this.htmlnodes[58] = "NOSCRIPT";
                this.htmlnodes[59] = "OBJECT";
                this.htmlnodes[60] = "OL";
                this.htmlnodes[61] = "OPTGROUP";
                this.htmlnodes[62] = "OPTION";
                this.htmlnodes[63] = "P";
                this.htmlnodes[64] = "PARAM";
                this.htmlnodes[65] = "PRE";
                this.htmlnodes[66] = "Q";
                this.htmlnodes[67] = "S";
                this.htmlnodes[68] = "SAMP";
                this.htmlnodes[69] = "SCRIPT";
                this.htmlnodes[70] = "SELECT";
                this.htmlnodes[71] = "SMALL";
                this.htmlnodes[72] = "SPAN";
                this.htmlnodes[73] = "STRIKE";
                this.htmlnodes[74] = "STRONG";
                this.htmlnodes[75] = "STYLE";
                this.htmlnodes[76] = "SUB";
                this.htmlnodes[77] = "SUP";
                this.htmlnodes[78] = "TABLE";
                this.htmlnodes[79] = "TBODY";
                this.htmlnodes[80] = "TD";
                this.htmlnodes[81] = "TEXTAREA";
                this.htmlnodes[82] = "TFOOT";
                this.htmlnodes[83] = "TH";
                this.htmlnodes[84] = "THEAD";
                this.htmlnodes[85] = "TITLE";
                this.htmlnodes[86] = "TR";
                this.htmlnodes[87] = "TT";
                this.htmlnodes[88] = "U";
                this.htmlnodes[89] = "UL";
                this.htmlnodes[90] = "VAR";
                this.htmlnodes[91] = "!--";

                this.htmlstructure = new bool[91];
            }
        }

        #endregion Constructors

        #region Methods (2)

        // Public Methods (2) 

        /// <summary>
        /// Check syntax of the whole text
        /// </summary>
        public void CheckSyntaxFull()
        {
            int cursorpos = this.rtbcode.SelectionStart;
            this.ResetHighlighting(this.rtbcode);

            bool htmlendnode = false;
            for (int i = 0; i < this.rtbcode.TextLength; i++)
            {
                if (this.checkhtml)
                {
                    if (this.rtbcode.Text[i] == '<')
                    {
                        this.posstarttag = i;
                    }
                    else if (this.rtbcode.Text[i] == '>')
                    {
                        if (this.rtbcode.Text[posstarttag + 1] == '/')
                        {
                            htmlendnode = true;
                        }
                        else
                        {
                            htmlendnode = false;
                        }

                        int lengthtillendtag = i - this.posstarttag;
                        if (lengthtillendtag > 1)
                        {
                            string htmlnodename;
                            if (htmlendnode == true)
                            {
                                htmlnodename = this.rtbcode.Text.Substring(this.posstarttag + 2, lengthtillendtag - 2);
                            }
                            else
                            {
                                htmlnodename = this.rtbcode.Text.Substring(this.posstarttag + 1, lengthtillendtag - 1);
                            }

                            if (this.ValidingHTMLNode(htmlnodename, htmlendnode))
                            {
                                this.ColorText(this.posstarttag, lengthtillendtag + 1, Color.Blue);
                            }
                            else
                            {
                                this.ColorText(this.posstarttag, lengthtillendtag + 1, Color.Red);
                            }
                        }
                    }
                }
            }

            this.rtbcode.SelectionStart = cursorpos;
            this.rtbcode.SelectionLength = 0;
        }

        /// <summary>
        /// highlight the change.
        /// </summary>
        /// <param name="newcharpos">The position where the new charcter is typed.</param>
        public void CheckSyntaxQuick(int newcharpos)
        {
            if (newcharpos > 0)
            {
                int cursorpos = this.rtbcode.SelectionStart;
                if (this.checkhtml)
                {
                    if (newcharpos < 0)
                    {
                        throw new CustomException("negative character location.");
                    }

                    if (this.rtbcode.Text[newcharpos] == '<')
                    {
                        for (int i = newcharpos; i > 0; i--)
                        {
                            if (this.rtbcode.Text[i] == '<')
                            {
                                this.ColorText(newcharpos, 1, Color.Red);
                                break;
                            }
                            else if (this.rtbcode.Text[i] == '>')
                            {
                                this.ColorText(newcharpos, 1, Color.Black);
                                break;
                            }
                        }
                    }
                    else if (this.rtbcode.Text[newcharpos] == '>')
                    {
                        string htmlnodename = String.Empty;
                        int htmlnodestartpos = -1;
                        bool htmlendnode = false;
                        for (int i = newcharpos; i >= 0; i--)
                        {
                            try
                            {
                                int chkpos = i - 1;
                                if (chkpos < 0)
                                {
                                    chkpos = 0;
                                }

                                if (this.rtbcode.Text[i] == '<')
                                {
                                    htmlnodestartpos = i;
                                    htmlnodename = this.rtbcode.Text.Substring(i + 1, newcharpos - 1 - i);
                                    break;
                                }
                                else if (this.rtbcode.Text[i] == '/' && this.rtbcode.Text[chkpos] == '<')
                                {
                                    htmlnodestartpos = i - 1;
                                    htmlnodename = this.rtbcode.Text.Substring(i + 1, newcharpos - 1 - i);
                                    htmlendnode = true;
                                    break;
                                }
                            }
                            catch (IndexOutOfRangeException outofrangeexc)
                            {
                                throw new CustomException(outofrangeexc.Message + " " + outofrangeexc.StackTrace);
                            }
                        }

                        if ((!String.IsNullOrEmpty(htmlnodename)) && (htmlnodestartpos != -1))
                        {
                            if (this.ValidingHTMLNode(htmlnodename, htmlendnode))
                            {
                                this.ColorText(htmlnodestartpos, newcharpos - htmlnodestartpos + 1, Color.Blue);
                            }
                            else
                            {
                                this.ColorText(htmlnodestartpos, newcharpos - htmlnodestartpos + 1, Color.Red);
                            }
                        }
                        else
                        {
                            this.ColorText(newcharpos, newcharpos + 1, Color.Black);
                        }
                    }
                    else
                    {
                        this.ColorText(newcharpos, 1, Color.Black);
                    }
                }

                this.rtbcode.SelectionStart = cursorpos;
                this.rtbcode.SelectionLength = 0;
            }
        }

        /// <summary>
        /// Make the whole text black.
        /// </summary>
        /// <param name="rtb">The richedit control that hold the note content.</param>
        private void ResetHighlighting(RichTextBox rtb)
        {
            rtb.SelectAll();
            rtb.SelectionColor = Color.Black;
            for (int i = 0; i < this.htmlstructure.Length; i++)
            {
                this.htmlstructure[i] = false;
            }
        }

        /// <summary>
        /// Find out if it is a html node.
        /// </summary>
        /// <param name="ishtml">Is it html to check.</param>
        /// <returns>true if it is html</returns>
        private bool ValidingHTMLNode(string ishtml, bool endnode)
        {
            for (int i = 1; i < ishtml.Length; i++)
            {
                if (ishtml[i] == ' ')
                {
                    ishtml = ishtml.Substring(0, i);
                    break;
                }
            }

            for (int n = 0; n < this.htmlnodes.Length; n++)
            {
                if (ishtml.ToUpper() == this.htmlnodes[n])
                {
                    if (endnode)
                    {
                        this.htmlstructure[n] = false;
                    }
                    else
                    {
                        if (!this.htmlstructure[n])
                        {
                            this.htmlstructure[n] = true;
                        }
                        else
                        {
                            //could be a structure problem.
                            //TODO check if endtag ommiting is allowed before returning false.
                            
                            //return false;
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Color some part of the rich edit text.
        /// </summary>
        /// <param name="posstart">The start position</param>
        /// <param name="len">The lenght</param>
        /// <param name="color">The color the text should get.</param>
        private void ColorText(int posstart, int len, Color color)
        {
            try
            {
                this.rtbcode.Select(posstart, len);
                this.rtbcode.SelectionColor = color;
            }
            catch (ArgumentOutOfRangeException arg)
            {
                throw new CustomException("TextHighlighter out of range: " + arg.Source);
            }
        }
    }

        #endregion Methods
}
