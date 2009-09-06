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

        private Regex SyntaxC = new Regex("if|else|for|while|{|}|do|define|#if");
        //regulaire expression for detecting some HTML code
        private Regex SyntaxHTML = new Regex("<!DOCTYPE |<HTML>|</HTML>|<BODY|</BODY>|<A HREF|<SPAN|<I>|</I>|<U>|</U>|"+
            "<B>|</B>|<UL>|<IL>|<OL|</OL>|<BR>|<BR />|<P |<P>|</P>|<FONT|</FONT>|<TITLE>|</TITLE>|<META|<LINK|<CODE>|"+
            "</CODE>|<DD>|</DD>|<TABLE|</TABLE>|<TD|</TD>|<TR|</TR>|<FORM |<IMG|<FRAME |<FRAMESET>|</FRAMESET><IFRAME |" +
            "</IFRAME>|<APPLET|</APPLET|<TH|</TH>|<THEAD|</THEAD>|<TFOOT|</TFOOT>|<INPUT|<OPTION|<LABEL|</LABEL>|<LEGEND|"+
            "</LEGEND>|<ISINDEX|<SELECT|</SELECT>|<TEXTAREA|</TEXTAREA>|<SCRIPT|</SCRIPT>|<NOSCRIPT>|</NOSCRIPT>|<S>|</S>|"+
            "<STRIKE|</STRIKE>|<TT|</TT>|<BIG|</BIG>|<SMALL>|</SMALL>|<BASEFONT|</BASEFONT>|<DIV|</DIV>|<H1>|</H1>|<H2>|</H2>|"+
            "<H3>|</H3>|<H4>|</H4>|<H5>|</H5>|<H6>|</H6>|<HEAD>|</HEAD>|<HR>|<EM|</EM>");

		#endregion Fields 

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



        #region constructor (1)
        public TextHighlight()
        {
        }
        #endregion
     }
}
