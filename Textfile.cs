//-----------------------------------------------------------------------
// <copyright file="Textfile.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2010-2013  Tom
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
    using System.Text;

    /// <summary>
    /// What file type is being written.
    /// </summary>
    public enum TextfileWriteType
    {
        /// <summary>
        /// Writing to a logfile.
        /// </summary>
        log,

        /// <summary>
        /// Writing to plain text file.
        /// </summary>
        exporttext,

        /// <summary>
        /// Writing to a rtf file.
        /// </summary>
        exportrtf,

        /// <summary>
        /// Writing to html file.
        /// </summary>
        exporthtml,

        /// <summary>
        /// Writing to php file.
        /// </summary>
        exportphp
    }

    /// <summary>
    /// Textfile saving class
    /// </summary>
    public sealed class Textfile
    {
        /// <summary>
        /// Initializes a new instance of the Textfile class.
        /// </summary>
        /// <param name="writetype">The TextfileWriteType.</param>
        /// <param name="filename">The filename</param>
        /// <param name="title">The title of the textfile</param>
        /// <param name="content">The content of the textfile</param>
        public Textfile(TextfileWriteType writetype, string filename, string title, string content)
        {
            if (File.Exists(filename))
            {
                bool filelocked = false;
                int filelockchecks = 0;
                while (filelocked)
                {
                    filelockchecks++;
                    filelocked = this.CheckFileLocked(filename);
                    if (filelockchecks > 50)
                    {
                        return;
                    }

                    if (filelocked)
                    {
                        System.Threading.Thread.Sleep(200);
                    }
                }
            }

            FileStream fs = null;
            StreamWriter writer = null;
            try
                {
                    switch (writetype)
                    {
                        case TextfileWriteType.log:
                            fs = new FileStream(filename, FileMode.Append);
                            writer = new StreamWriter(fs, Encoding.ASCII);
                            writer.Write(content);
                            break;
                        case TextfileWriteType.exporttext:
                            fs = new FileStream(filename, FileMode.OpenOrCreate);
                            writer = new StreamWriter(fs, Encoding.UTF8);
                            writer.WriteLine(Strings.T("Title: ") + title + Environment.NewLine);
                            writer.Write(content);
                            break;
                        case TextfileWriteType.exportrtf:
                            fs = new FileStream(filename, FileMode.OpenOrCreate);
                            writer = new StreamWriter(fs, Encoding.ASCII);
                            writer.Write(content);

                            // add a null character to the end.of the RTF file.
                            writer.Write((char)0);
                            break;
                        case TextfileWriteType.exporthtml:
                            fs = new FileStream(filename, FileMode.OpenOrCreate);
                            writer = new StreamWriter(fs, Encoding.UTF8);

                            // trying to make turn a incompleet html fragement into a valid html document.
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
                            writer.WriteLine("<p>" + content + "</p>");
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
                            writer = new StreamWriter(fs, Encoding.ASCII);
                            writer.Write(content);
                            break;
                    }
                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }

                    if (fs != null)
                    {
                        fs.Close();
                    }
                }
        }

        /// <summary>
        /// Attempt to open the file exclusively.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private bool CheckFileLocked(string filename)
        {
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.None, 32))
                {
                    fs.ReadByte();
                    return false;
                }
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}
