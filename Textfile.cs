//-----------------------------------------------------------------------
// <copyright file="Textfile.cs" company="GNU">
//  NoteFly a note application.
//  Copyright (C) 2010-2011  Tom
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
    using System;
    using System.IO;

    public enum TextfileWriteType
    {
        log,
        exporttext,
        exporthtml,
        exportphp
    }

    /// <summary>
    /// Textfile saving class
    /// </summary>
    public class Textfile
    {
        /// <summary>
        /// Initializes a new instance of the Textfile class.
        /// </summary>
        /// <param name="isnote">is a note to be saved.</param>
        /// <param name="filename">the filename</param>
        /// <param name="title">the title of the textfile</param>
        /// <param name="content">the content of the textfile</param>
        public Textfile(TextfileWriteType writetype, string filename, string title, string content)
        {
            FileStream fs = null;
            StreamWriter writer = null;
            try
            {
                switch (writetype)
                {
                    case TextfileWriteType.log:
                        fs = new FileStream(filename, FileMode.Append);
                        writer = new StreamWriter(fs);
                        writer.Write(content);
                        break;
                    case TextfileWriteType.exporttext:
                        fs = new FileStream(filename, FileMode.OpenOrCreate);
                        writer = new StreamWriter(fs);
                        writer.WriteLine("Title: " + title + Environment.NewLine);
                        writer.Write(content);
                        break;
                    case TextfileWriteType.exporthtml:
                        fs = new FileStream(filename, FileMode.OpenOrCreate);
                        writer = new StreamWriter(fs);
                        //trying to make turn a incompleet html fragement into a valid html5 document.
                        if (!content.Contains("<!DOCTYPE"))
                        {
                            writer.WriteLine("<!DOCTYPE html>");
                        }
                        if (!content.Contains("<html"))
                        {
                            writer.WriteLine("<html>");
                        }
                        if (!content.Contains("<head>"))
                        {
                            writer.WriteLine("<head>");
                            writer.WriteLine("\t<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
                            writer.WriteLine("\t<title>" + title + "</title>");
                            writer.WriteLine("</head>");
                        }
                        if (!content.Contains("<body"))
                        {
                            writer.WriteLine("<body>");
                        }
                        writer.WriteLine("<h1>" + title + "</h1>");
                        writer.WriteLine("<p>"+content+"</p>");
                        if (!content.Contains("</body>"))
                        {
                            writer.WriteLine("</body>");
                        }
                        if (!content.Contains("</html>"))
                        {
                            writer.WriteLine("</html>");
                        }
                        break;
                    case TextfileWriteType.exportphp:
                        fs = new FileStream(filename, FileMode.OpenOrCreate);
                        writer = new StreamWriter(fs);
                        writer.Write(content);
                        break;
                }
            }
            finally
            {
                writer.Close();
                fs.Close();
            }
        }
    }
}
