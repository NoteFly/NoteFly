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

        /// <summary>
        /// The number of HTML nodes NoteFly knows.
        /// </summary>
        private const int NUMHTMLNODES = 94;

        private bool[] htmlstructure;

        /// <summary>
        /// 0 is endtag not required
        /// 1 is endtag required
        /// 2 is endtag forbidden
        /// </summary>
        private short[] htmlendtagpolicy;

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
                this.htmlnodes = new string[NUMHTMLNODES];
                this.htmlendtagpolicy = new short[NUMHTMLNODES];

                this.htmlnodes[0] = "A";            this.htmlendtagpolicy[0] = 1; 
                this.htmlnodes[1] = "ABBR";         this.htmlendtagpolicy[1] = 1;
                this.htmlnodes[2] = "ACRONYM";      this.htmlendtagpolicy[2] = 1;
                this.htmlnodes[3] = "ADDRESS";      this.htmlendtagpolicy[3] = 1;
                this.htmlnodes[4] = "APPLET";       this.htmlendtagpolicy[4] = 1;
                this.htmlnodes[5] = "AREA";         this.htmlendtagpolicy[5] = 2;
                this.htmlnodes[6] = "B";            this.htmlendtagpolicy[6] = 1;
                this.htmlnodes[7] = "BASE";         this.htmlendtagpolicy[7] = 2;
                this.htmlnodes[8] = "BASEFONT";     this.htmlendtagpolicy[8] = 2;
                this.htmlnodes[9] = "BDO";          this.htmlendtagpolicy[9] = 1;
                this.htmlnodes[10] = "BIG";         this.htmlendtagpolicy[10] = 1;
                this.htmlnodes[11] = "BLOCKQUOTE";  this.htmlendtagpolicy[11] = 1;
                this.htmlnodes[12] = "BODY";        this.htmlendtagpolicy[12] = 0;
                this.htmlnodes[13] = "BR";          this.htmlendtagpolicy[13] = 2;
                this.htmlnodes[14] = "BUTTON";      this.htmlendtagpolicy[14] = 1;
                this.htmlnodes[15] = "CAPTION";     this.htmlendtagpolicy[15] = 1;
                this.htmlnodes[16] = "CENTER";      this.htmlendtagpolicy[16] = 1;
                this.htmlnodes[17] = "CITE";        this.htmlendtagpolicy[17] = 1;
                this.htmlnodes[18] = "CODE";        this.htmlendtagpolicy[18] = 1;
                this.htmlnodes[19] = "COL";         this.htmlendtagpolicy[19] = 2;
                this.htmlnodes[20] = "COLGROUP";    this.htmlendtagpolicy[20] = 0;
                this.htmlnodes[21] = "DD";          this.htmlendtagpolicy[21] = 0;
                this.htmlnodes[22] = "DEL";         this.htmlendtagpolicy[22] = 1;
                this.htmlnodes[23] = "DFN";         this.htmlendtagpolicy[23] = 1;
                this.htmlnodes[24] = "DIR";         this.htmlendtagpolicy[24] = 1;
                this.htmlnodes[25] = "DIV";         this.htmlendtagpolicy[25] = 1;
                this.htmlnodes[26] = "DL";          this.htmlendtagpolicy[26] = 1;
                this.htmlnodes[27] = "DT";          this.htmlendtagpolicy[27] = 0;
                this.htmlnodes[28] = "EM";          this.htmlendtagpolicy[28] = 1;
                this.htmlnodes[29] = "EMBED";       this.htmlendtagpolicy[29] = 1;
                this.htmlnodes[30] = "FIELDSET";    this.htmlendtagpolicy[30] = 1;
                this.htmlnodes[31] = "FONT";        this.htmlendtagpolicy[31] = 1;
                this.htmlnodes[32] = "FORM";        this.htmlendtagpolicy[32] = 1;
                this.htmlnodes[33] = "FRAME";       this.htmlendtagpolicy[33] = 0;
                this.htmlnodes[34] = "FRAMESET";    this.htmlendtagpolicy[34] = 1;
                this.htmlnodes[35] = "H1";          this.htmlendtagpolicy[35] = 1;
                this.htmlnodes[36] = "H2";          this.htmlendtagpolicy[36] = 1;
                this.htmlnodes[37] = "H3";          this.htmlendtagpolicy[37] = 1;
                this.htmlnodes[38] = "H4";          this.htmlendtagpolicy[38] = 1;
                this.htmlnodes[39] = "H5";          this.htmlendtagpolicy[39] = 1;
                this.htmlnodes[40] = "H6";          this.htmlendtagpolicy[40] = 1;
                this.htmlnodes[41] = "HEAD";        this.htmlendtagpolicy[41] = 0;
                this.htmlnodes[42] = "HR";          this.htmlendtagpolicy[42] = 2;
                this.htmlnodes[43] = "HTML";        this.htmlendtagpolicy[43] = 0;
                this.htmlnodes[44] = "Hx";          this.htmlendtagpolicy[44] = 1;
                this.htmlnodes[45] = "I";           this.htmlendtagpolicy[45] = 1;
                this.htmlnodes[46] = "IFRAME";      this.htmlendtagpolicy[46] = 1;
                this.htmlnodes[47] = "IMG";         this.htmlendtagpolicy[47] = 2;
                this.htmlnodes[48] = "INPUT";       this.htmlendtagpolicy[48] = 2;
                this.htmlnodes[49] = "INS";         this.htmlendtagpolicy[49] = 1;
                this.htmlnodes[50] = "ISINDEX";     this.htmlendtagpolicy[50] = 2;
                this.htmlnodes[51] = "KBD";         this.htmlendtagpolicy[51] = 1;
                this.htmlnodes[52] = "LABEL";       this.htmlendtagpolicy[52] = 1;
                this.htmlnodes[53] = "LEGEND";      this.htmlendtagpolicy[53] = 1;
                this.htmlnodes[54] = "LI";          this.htmlendtagpolicy[54] = 0;
                this.htmlnodes[55] = "LINK";        this.htmlendtagpolicy[55] = 2;
                this.htmlnodes[56] = "MAP";         this.htmlendtagpolicy[56] = 1;
                this.htmlnodes[57] = "MENU";        this.htmlendtagpolicy[57] = 1;
                this.htmlnodes[58] = "META";        this.htmlendtagpolicy[58] = 2;
                this.htmlnodes[59] = "NOFRAMES";    this.htmlendtagpolicy[59] = 1;
                this.htmlnodes[60] = "NOSCRIPT";    this.htmlendtagpolicy[60] = 1;
                this.htmlnodes[61] = "OBJECT";      this.htmlendtagpolicy[61] = 1;
                this.htmlnodes[62] = "OL";          this.htmlendtagpolicy[62] = 1;
                this.htmlnodes[63] = "OPTGROUP";    this.htmlendtagpolicy[63] = 1;
                this.htmlnodes[64] = "OPTION";      this.htmlendtagpolicy[64] = 0;
                this.htmlnodes[65] = "P";           this.htmlendtagpolicy[65] = 0;
                this.htmlnodes[66] = "PARAM";       this.htmlendtagpolicy[66] = 2;
                this.htmlnodes[67] = "PRE";         this.htmlendtagpolicy[67] = 1;
                this.htmlnodes[68] = "Q";           this.htmlendtagpolicy[68] = 1;
                this.htmlnodes[69] = "S";           this.htmlendtagpolicy[69] = 1;
                this.htmlnodes[70] = "SAMP";        this.htmlendtagpolicy[70] = 1;
                this.htmlnodes[71] = "SCRIPT";      this.htmlendtagpolicy[71] = 1;
                this.htmlnodes[72] = "SELECT";      this.htmlendtagpolicy[72] = 1;
                this.htmlnodes[73] = "SMALL";       this.htmlendtagpolicy[73] = 1;
                this.htmlnodes[74] = "SPAN";        this.htmlendtagpolicy[74] = 1;
                this.htmlnodes[75] = "STRIKE";      this.htmlendtagpolicy[75] = 1;
                this.htmlnodes[76] = "STRONG";      this.htmlendtagpolicy[76] = 1;
                this.htmlnodes[77] = "STYLE";       this.htmlendtagpolicy[77] = 1;
                this.htmlnodes[78] = "SUB";         this.htmlendtagpolicy[78] = 1;
                this.htmlnodes[79] = "SUP";         this.htmlendtagpolicy[79] = 1;
                this.htmlnodes[80] = "TABLE";       this.htmlendtagpolicy[80] = 1;
                this.htmlnodes[81] = "TBODY";       this.htmlendtagpolicy[81] = 0;
                this.htmlnodes[82] = "TD";          this.htmlendtagpolicy[82] = 0;
                this.htmlnodes[83] = "TEXTAREA";    this.htmlendtagpolicy[83] = 1;
                this.htmlnodes[84] = "TFOOT";       this.htmlendtagpolicy[84] = 0;
                this.htmlnodes[85] = "TH";          this.htmlendtagpolicy[85] = 0;
                this.htmlnodes[86] = "THEAD";       this.htmlendtagpolicy[86] = 0;
                this.htmlnodes[87] = "TITLE";       this.htmlendtagpolicy[87] = 1;
                this.htmlnodes[88] = "TR";          this.htmlendtagpolicy[88] = 0;
                this.htmlnodes[89] = "TT";          this.htmlendtagpolicy[89] = 1;
                this.htmlnodes[90] = "U";           this.htmlendtagpolicy[90] = 1;
                this.htmlnodes[91] = "UL";          this.htmlendtagpolicy[91] = 1;
                this.htmlnodes[92] = "VAR";         this.htmlendtagpolicy[92] = 1;
                this.htmlnodes[93] = "!--";         this.htmlendtagpolicy[93] = 0;

                this.htmlstructure = new bool[NUMHTMLNODES];
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
            this.rtbcode.Select(0, 0);
            this.rtbcode.SelectionColor = Color.Black;
            int cursorpos = 0;
            this.ResetHighlighting(this.rtbcode);

            bool htmlendnode = false;
            for (int i = 0; i < this.rtbcode.TextLength; i++)
            {
                if (this.checkhtml)
                {
                    switch (this.rtbcode.Text[i])
                    {
                        case '<':
                            this.posstarttag = i;
                            break;
                        case '>':
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
                            break;
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
                            switch (this.rtbcode.Text[i])
                            {
                                case '<':
                                    this.ColorText(newcharpos, 1, Color.Red);
                                    break;
                                case '>':
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
                else if (i>11)
                {
                    break;
                }
            }

            int startsearch = 0;
            if (((ishtml[0] > 76) && (ishtml[0] < 91)) || ((ishtml[0] > 108) && (ishtml[0] < 123)))
            {
                startsearch = 55;
            }

            for (int n = startsearch; n < this.htmlnodes.Length; n++)
            {
                if (ishtml.ToUpper() == this.htmlnodes[n])
                {
                    if (endnode)
                    {
                        this.htmlstructure[n] = false;
                        if (this.htmlendtagpolicy[n] == 2)
                        {
                            //forbidden endtag
                            return false;
                        }
                    }
                    else
                    {
                        if (!this.htmlstructure[n])
                        {
                            this.htmlstructure[n] = true;
                        }
                        else
                        {
                            //Check if endtag ommiting is allowed before returning false.
                            if (this.htmlendtagpolicy[n] == 1)
                            {
                                return false;
                            }
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
