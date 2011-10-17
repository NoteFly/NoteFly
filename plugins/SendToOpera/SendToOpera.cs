//-----------------------------------------------------------------------
// <copyright file="SendToOpera.cs" company="NoteFly">
// NoteFly a note application.
// Copyright (C) 2011  Tom
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
namespace SendToOpera
{
    using System;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// SenderToOpera class.
    /// </summary>
    public class SendToOpera : IPlugin.PluginBase
    {
        // Fields (2) 

        /// <summary>
        /// Opera notes filename.
        /// </summary>
        private const string OPERANOTESFILE = "notes.adr";

        /// <summary>
        /// The path to the opera notes file.
        /// </summary>
        private string operanotesfilepath = string.Empty;

        // Methods (7) 

        /// <summary>
        /// Menu clicked
        /// </summary>
        /// <param name="rtbnote">use the content of rtbnote for the note content, 
        /// this way note content does not have to be read again from disk.</param>
        /// <param name="title">The note settings</param>
        public override void ShareMenuClicked(System.Windows.Forms.RichTextBox rtbnote, string title)
        {
            if (this.WriteNote(rtbnote.Text))
            {
                MessageBox.Show("Note added to Opera®");
            }
            else
            {
                MessageBox.Show("Could not add note to Opera®.");
            }
        }

        /// <summary>
        /// Create share menu
        /// </summary>
        /// <returns></returns>
        public override ToolStripMenuItem InitFrmNoteShareMenu()
        {
            ToolStripMenuItem toolstripmenu = new ToolStripMenuItem("Send to Opera");
            toolstripmenu.Name = "menuShareSendToOpera";
            return toolstripmenu;
        }

        /// <summary>
        /// Get a char array with all hexadecimal characters.
        /// </summary>
        /// <returns>A array of chars with hexidecimal chars</returns>
        private char[] GetHexdecChars()
        {
            char[] hexdecchars = new char[16];
            int acsiipos = 48;
            for (int c = 0; c < 16; c++)
            {
                hexdecchars[c] = Convert.ToChar(acsiipos);
                acsiipos++;
                if (acsiipos == 58)
                {
                    acsiipos = 65;
                }
            }

            return hexdecchars;
        }

        /// <summary>
        /// Read the notes file to find out which NoteID a new note should get.
        /// </summary>
        /// <returns>The new noteid for the note in Opera.</returns>
        private int GetNewNoteID()
        {
            int newnoteid = -1;
            if (!string.IsNullOrEmpty(this.operanotesfilepath))
            {
                StreamReader reader = null;
                try
                {
                    reader = new StreamReader(this.GetOperaNoteFile(), Encoding.UTF8);
                    while (reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (line.StartsWith("\tID="))
                        {
                            int possep = line.IndexOf('=');
                            string idstr = line.Substring(possep, line.Length - possep);
                            int id;
                            if (int.TryParse(idstr, out id))
                            {
                                if (newnoteid < id)
                                {
                                    newnoteid = id;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                }
            }

            return newnoteid;
        }

        /// <summary>
        /// Get the file where Opera(tm) saves the notes.
        /// </summary>
        /// <returns>The path to the opera notes files</returns>
        private string GetOperaNoteFile()
        {
            string opera_notesfilepath = null;
            string opera_appdata = Path.Combine(System.Environment.GetEnvironmentVariable("APPDATA"), @"Opera\Opera\");
            if (Directory.Exists(opera_appdata))
            {
                string checknotesfile = Path.Combine(opera_appdata, OPERANOTESFILE);
                if (File.Exists(checknotesfile))
                {
                    FileInfo opera_fileinfo = new FileInfo(checknotesfile);
                    if (opera_fileinfo.Attributes != FileAttributes.System)
                    {
                        opera_notesfilepath = checknotesfile;
                    }
                }
            }

            return opera_notesfilepath;
        }

        /// <summary>
        /// Generate a uniqueid string 32 hexadecimal characters long.
        /// </summary>
        /// <returns>The GUID</returns>
        private string GetUniqueID()
        {
            Random rnd = new Random();
            StringBuilder uniqueid = new StringBuilder();
            char[] hexdecchars = this.GetHexdecChars();

            for (int i = 0; i < 32; i++)
            {
                int pos = rnd.Next(0, 16);
                uniqueid.Append(hexdecchars[pos]);
            }

            return uniqueid.ToString();
        }

        /// <summary>
        /// Get the unix time since 1 jan. 1970 0:00 GMT+0
        /// </summary>
        /// <returns>Unixtime as string</returns>
        private string GetUnixtime()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
            string unixtimestr = Convert.ToString(ts.TotalSeconds);
            int poscomma = unixtimestr.IndexOf(',');
            if (poscomma > 0)
            {
                unixtimestr = unixtimestr.Substring(0, poscomma);
            }

            return unixtimestr;
        }

        /// <summary>
        /// Write a note to Opera(tm) webbrowser.
        /// </summary>
        /// <param name="notecontent">The note content to write</param>
        /// <returns>True if the note is writen succesfull</returns>
        private bool WriteNote(string notecontent)
        {
            if (!string.IsNullOrEmpty(this.operanotesfilepath))
            {
                string noteid = string.Empty;
                if (this.GetNewNoteID() >= 0)
                {
                    noteid = this.GetNewNoteID().ToString();
                }

                string uniqueid = this.GetUniqueID();
                char controlchar = Convert.ToChar(2);
                notecontent = notecontent.Replace('\n', controlchar).Replace('\r', controlchar);
                string createdatetime = this.GetUnixtime();
                StreamWriter steamwriter = null;
                try
                {
                    steamwriter = new StreamWriter(this.operanotesfilepath, true, Encoding.UTF8);
                    steamwriter.AutoFlush = false;
                    steamwriter.WriteLine("#NOTE");
                    steamwriter.WriteLine("\tID=" + noteid);
                    steamwriter.WriteLine("\tUNIQUEID=" + uniqueid);
                    steamwriter.WriteLine("\tNAME=" + notecontent);
                    steamwriter.WriteLine("\tCREATED=" + createdatetime);
                    steamwriter.WriteLine(string.Empty);
                    steamwriter.Flush();
                }
                catch (Exception ioexc)
                {
                    MessageBox.Show("Write error: " + ioexc.Message);
                    return false;
                }
                finally
                {
                    if (steamwriter != null)
                    {
                        steamwriter.Close();
                    }
                }

                return true;
            }

            return false;
        }
    }
}
