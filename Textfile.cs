/* Copyright (C) 2009-2010
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
