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
