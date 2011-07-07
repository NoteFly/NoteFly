
namespace NoteFly
{
    public class HighlightLanguage
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
        private int posdocstart;

        /// <summary>
        /// The position where the documents ends.
        /// </summary>
        private int posdocend = int.MaxValue;

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
