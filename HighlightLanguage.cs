using System;
using System.Collections.Generic;
using System.Text;

namespace NoteFly
{
    public class HighlightLanguage
    {
        /// <summary>
        /// The name of the language
        /// </summary>
        private string name;

        /// <summary>
        /// 
        /// </summary>
        private string commentline;

        /// <summary>
        /// 
        /// </summary>
        private string commentstart;

        /// <summary>
        /// 
        /// </summary>
        private string commentend;

        /// <summary>
        /// A string array of keywords used in this language
        /// </summary>
        private string[] keywords;

        /// <summary>
        /// Initializes a new instance of the HighlightLanguage class.
        /// </summary>
        /// <param name="name">The name of the language</param>
        /// <param name="commentline"></param>
        /// <param name="commentstart"></param>
        /// <param name="commentend"></param>
        /// <param name="keywords">The keywords to highlight on</param>
        public HighlightLanguage(string name, string commentline, string commentstart, string commentend, string[] keywords)
        {
            this.name = name;
            this.commentline = commentline;
            this.commentstart = commentstart;
            this.commentend = commentend;
            this.keywords = keywords;
        }

        /// <summary>
        /// The name of the language
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Get the comment line characters
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
                return this.keywords.Length;
            }
        }

        /// <summary>
        /// Get a keyword at a position.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public string GetKeyword(int n)
        {
            return this.keywords[n];
        }
    }
}
