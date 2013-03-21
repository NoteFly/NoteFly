//-----------------------------------------------------------------------
// <copyright file="HighlightLanguage.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2011-2013 Tom
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
// </copyright>
//-----------------------------------------------------------------------
namespace NoteFly
{
    using System.Collections.Generic;

    /// <summary>
    /// HighlightLanguage class, a data class that holds lexicon of a language to highlight.
    /// </summary>
    public sealed class HighlightLanguage
    {
        /// <summary>
        /// The name of the language
        /// </summary>
        private string name;

        /// <summary>
        /// The characters to make it a line comment.
        /// </summary>
        private string commentline;

        /// <summary>
        /// The characters to start commenting of multiple lines
        /// </summary>
        private string commentstart;

        /// <summary>
        /// The characters to end comment of multiple lines
        /// </summary>
        private string commentend;

        /// <summary>
        /// The characters that tell where start of the document is.
        /// </summary>
        private string docstartstr;

        /// <summary>
        /// The characters that tell where the documents end.
        /// </summary>
        private string docendstr;

        /// <summary>
        /// The position where the document starts.
        /// </summary>
        private int posdocstart = int.MaxValue;

        /// <summary>
        /// The position where the documents ends.
        /// </summary>
        private int posdocend = int.MaxValue;

        /// <summary>
        /// Dictornary with all keywords used in this language
        /// </summary>
        private Dictionary<string, int> keywordsdic;
        
        /// <summary>
        /// Initializes a new instance of the HighlightLanguage class.
        /// </summary>
        /// <param name="name">The name of the language</param>
        /// <param name="commentline">How a comment line is marked in this language</param>
        /// <param name="commentstart">How comments start is marked in this language</param>
        /// <param name="commentend">How comments end is marked in this language</param>
        /// <param name="docstartstr">How the document of this language start</param>
        /// <param name="docendstr">How the document of this language end</param>
        /// <param name="keywords">The keywords to highlight on</param>
        public HighlightLanguage(string name, string commentline, string commentstart, string commentend, string docstartstr, string docendstr, string[] keywords)
        {
            this.name = name;
            this.commentline = commentline;
            this.commentstart = commentstart;
            this.commentend = commentend;
            this.docstartstr = docstartstr;
            this.docendstr = docendstr;
            this.keywordsdic = new Dictionary<string, int>(keywords.Length);
            for (int i = 0; i < keywords.Length; i++)
            {
                try
                {
                    this.keywordsdic.Add(keywords[i], i);
                }
                catch (System.ArgumentException)
                {
                    Log.Write(LogType.error, "Dublicated keyword " + keywords[i] + " in highlight lexicon file.");
                }
            }
        }

        /// <summary>
        /// Gets the name of the language
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets the comment line characters
        /// </summary>
        public string Commentline
        {
            get
            {
                return this.commentline;
            }
        }

        /// <summary>
        /// Gets the comment start characters
        /// </summary>
        public string Commentstart
        {
            get
            {
                return this.commentstart;
            }
        }

        /// <summary>
        /// Gets the comment end characters
        /// </summary>
        public string Commentend
        {
            get
            {
                return this.commentend;
            }
        }

        /// <summary>
        /// Gets the number of keywords.
        /// </summary>
        public int NumKeywords
        {
            get
            {
                return this.keywordsdic.Count;
            }
        }

        /// <summary>
        /// Gets the characters that tell where the document starts.
        /// </summary>
        public string DocumentStartStr
        {
            get
            {
                return this.docstartstr;
            }
        }

        /// <summary>
        /// Gets the characters that tell where the document ends.
        /// </summary>
        public string DocumentEndStr
        {
            get
            {
                return this.docendstr;
            }
        }

        /// <summary>
        /// Gets or sets the position where the document starts
        /// </summary>
        public int PosDocumentStart
        {
            get
            {
                return this.posdocstart;
            }

            set
            {
                this.posdocstart = value;
            }
        }

        /// <summary>
        /// Gets or sets the position where the document ends
        /// </summary>
        public int PosDocumentEnd
        {
            get
            {
                return this.posdocend;
            }

            set
            {
                this.posdocend = value;
            }
        }

        /// <summary>
        /// Lookup if keyword exists. (use fast dicnary lookup)
        /// </summary>
        /// <param name="keyword">The keyword to lookup</param>
        /// <returns>True if exist in this highlightlanguage</returns>
        public bool FindKeyword(string keyword)
        {
            return this.keywordsdic.ContainsKey(keyword);
        }

        /// <summary>
        /// Check if keyword is used to set document end or start and then set length of document.
        /// </summary>
        /// <param name="keyword">The keyword to check if the is used by this language as document start or end.</param>
        /// <param name="curpos">The position in the richtextbox of the keyword.</param>
        public void CheckSetDocumentPos(string keyword, int curpos)
        {
            keyword = keyword.ToLowerInvariant();
            if (keyword.StartsWith(this.docstartstr))
            {
                this.posdocstart = curpos;
                this.posdocend = int.MaxValue;
            }
            else if (this.DocumentEndStr == keyword)
            {
                this.posdocend = curpos;
            }
        }
    }
}
