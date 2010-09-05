//-----------------------------------------------------------------------
// <copyright file="Textfile.cs" company="GNU">
//  NoteFly a note application.
//  Copyright (C) 2010  Tom
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

    /// <summary>
    /// Save the note as a textfile
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
        public Textfile(bool isnote, string filename, string title, string content)
        {
            FileStream fs = null;
            StreamWriter writer = null;
            try
            {
                if (isnote)
                {
                    fs = new FileStream(filename, FileMode.OpenOrCreate);
                }
                else
                {
                    fs = new FileStream(filename, FileMode.Append);
                }

                writer = new StreamWriter(fs);
                if (isnote)
                {
                    writer.WriteLine("Title: " + title + "\r\n");
                    writer.Write(content);
                }
                else
                {
                    writer.Write(content);
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
