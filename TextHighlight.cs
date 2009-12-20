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
        private Regex Ckeywords  = new Regex("auto|break|case|char|catch|const|continue|default|define|do|else|extern|for|goto|if|return|sizeof|static|switch|throw|try|typedef|union|void|volatile|while");
        private Regex Cdatatypes = new Regex("bool|char|double|enum|float|int|long|short|signed|string|unsigned");

        private Regex CfunctionsStdlib = new Regex("abort|abs|atexit|atof|atoi|atol|atoll|bsearch|calloc|div|exit|free|getenv|labs|ldiv|llabs|lldiv|malloc|mblen|mbstowcs|mbtowc|qsort|rand|realloc|srand|strtod|strtof|strtol|strtold|strtoll|strtoul|strtoull|system");
        private Regex CfunctionsStdio  = new Regex("clearerr|fclose|feof|ferror|fflush|fgetc|fgetpos|fgets|fopen|fprintf|fputc|fputs|fread|freopen|fscanf|fseek|fsetpos|fseek|fsetpos|ftell|fwrite|getc|getchar|gets|perror|printf|putc|putchar|puts|remove|rename|rewind|scanf|setbuf|setvbuf|snprintf|sprintf|sscanf|tmpfile|tmpnam|ungetc|vprintf|vfprintf|vsprintf|vsnprintf|vfscanf|vsscanf");
        private Regex CfunctionsString = new Regex("memchr|memcmp|memcpy|memmove|memset|strcat|strchr|strcmp|strcoll|strcpy|strcspn|strerror|strlen|strncat|strncmp|strncpy|strpbrk|strrchr|strspn|strstr|strtok|strxfrm");
        private Regex CfunctionsType   = new Regex("isalnum|isblank|iscntrl|isdigit|isgraph|islower|isprint|ispunct|isspace|isupper|isxdigit|tolower|toupper|printf|scanf");
        private Regex CfunctionsMath   = new Regex("acos|acosh|asin|asinh|atan2|atan|atanh|cbrt|ceil|copysign|cos|cosh|erf|erfc|exp2|exp|expm1|fabs|fdim|floor|fma|fmax|fmin|fmod|fpclassify|frexp|hypot|ilogb|isfinite|isgreater|isgreaterequal|isinf|isless|islessequal|islessgreater|isnan|isnormal|isunordered|ldexp|lgamma|llrint|llround|log1p|log2|log10|log|logb|lrint|lround|modf|nan|nearbyint|nextafter|nexttoward|pow|remainder|remquo|rint|round|scalbln|scalbn|signbit|sin|sinh|sqrt|tan|tanh|tgamma|trunc");
        private Regex CfunctionsTime   = new Regex("'*'asctime|'*'ctime|difftime|'*'gmtime|'*'localeconv|'*'localtime|mktime|'*'setlocale|strftime|time");
        
        
        //IngoreCase options is set.
        private Regex SyntaxHTML = new Regex("<?HTML|<?HEAD|<?BODY^|<?A^|<?P|<?BR|<?SPAN^|<?I|<?U|<?B|<?OL|<?UL|<?IL|<?FONT^|<?TITLE|<?BLOCKQUOTE|<?META ^|<?LINK ^|<?CODE|<?DD|<?TABLE ^|<?DL|<?TD^|<?TR^|<?FORM ^|<?IMG ^|<?FRAME ^|<?STRONG|<?FRAMESET ^|<?IFRAME^|<?APPLET^|<?TH^|<?PRE^|<?HEAD|<?TFOOT|<?INPUT^|<?OPTION^|<?LABEL^|<?LEGEND^|<?SELECT^|<?TEXTAREA^|<?SCRIPT^|<?NOSCRIPT|<?S|<?STRIKE|<?TT|<?BIG|<?SMALL|<?BASEFONT^|<?DIV^|<?H1|<?H2|<?H3|<?H4|<?H5|<?H6|<?ADRESS|<?HR|<?EM", RegexOptions.IgnoreCase);
        /*
A
ABBR
ACRONYM
ADDRESS
APPLET
AREA
B
BASE
BASEFONT
BDO
BIG
BLOCKQUOTE
BODY
BR
BUTTON
CAPTION
CENTER
CITE
CODE
COL
COLGROUP
DD
DEL
DFN
DIR
DIV
DL
DT
EM
FIELDSET
FONT
FORM
FRAME
FRAMESET
HEAD
HR
HTML
Hx
I
IFRAME
IMG
INPUT
INS
ISINDEX
KBD
LABEL
LEGEND
LI
LINK
MAP
MENU
META
NOFRAMES
NOSCRIPT
OBJECT
OL
OPTGROUP
OPTION
P
PARAM
PRE
Q
S
SAMP
SCRIPT
SELECT
SMALL
SPAN
STRIKE
STRONG
STYLE
SUB
SUP
TABLE
TBODY
TD
TEXTAREA
TFOOT
TH
THEAD
TITLE
TR
TT
U
UL
VAR
         */


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
                            if (Ckeywords.IsMatch(iscode))
                            {
                                ColorText(beginword, lenword, Color.Green);
                            }
                            else if (Cdatatypes.IsMatch(iscode))
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
                        throw new CustomException("Quick TextHighlighter out of range: " + arg.Source);
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
                throw new CustomException("TextHighlighter out of range: " + arg.Source);
            }
        }

        #endregion Methods
    }
}
