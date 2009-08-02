/*
Copyright (C) 2009

This program is free software; you can redistribute it and/or modify it
under the terms of the GNU General Public License as published by the
Free Software Foundation; either version 2, or (at your option) any
later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SimplePlainNote
{
    class TextHighlight
    {
        
        public Regex syntaxCsharp = new Regex("abstract|as|base|bool|break|byte|case|catch|char|checked|"+
        "class|const|continue|decimal|default|delegate|do|double|else|enum|event|explicit|extern|"+
        "false|finally|fixed|float|for|foreach|goto|if|implicit|in|int|interface|internal|is|lock|"+
        "long|namespace|new|null|object|operator|out|override|params|private|protected|public|"+
        "readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|"+
        "throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|virtual|volatile|void|while|");


        /*
int selPos = rtbNote.SelectionStart;                
foreach (Match keyWordMatch in syntaxCsharp.Matches(rtbNote.Text))
{
    rtbNote.Select(keyWordMatch.Index, keyWordMatch.Length);
    rtbNote.SelectionColor = Color.Blue;
    rtbNote.SelectionStart = selPos;
    rtbNote.SelectionColor = Color.Black;
}
 */
    }
}
