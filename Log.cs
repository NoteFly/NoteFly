//-----------------------------------------------------------------------
// <copyright file="Log.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2010-2015  Tom
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
    /// The logtype
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// Application crash, unexpected/bug
        /// </summary>
        exception,

        /// <summary>
        /// User did something wrong, application handled it okay.
        /// </summary>
        error,

        /// <summary>
        /// Something happend that is worth logging for instance an note got deleted.
        /// </summary>
        info
    }

    /// <summary>
    /// Log class, for logging.
    /// </summary>
    public sealed class Log
    {
        private const int MAXREPEATEDMESSAGE = 16;
        private static int repeatedmessage = 0;
        private static int prevmessagehashcode;

        /// <summary>
        /// The debug log filename.
        /// </summary>
        private const string DEBUGLOGFILENAME = "debug.log";

        /// <summary>
        /// Write and append a message to the logfile.
        /// </summary>
        /// <param name="typemsg">The type of the log message</param>
        /// <param name="message">The message to log</param>
        public static void Write(LogType typemsg, string message)
        {
            if (!Settings.ProgramLogInfo && typemsg == LogType.info)
            {
                return;
            }
            else if (!Settings.ProgramLogError && typemsg == LogType.error)
            {
                return;
            }
            else if (!Settings.ProgramLogException && typemsg == LogType.exception)
            {
                return;
            }

            if (message.GetHashCode() == prevmessagehashcode)
            {
                repeatedmessage++;
                if (repeatedmessage > Log.MAXREPEATEDMESSAGE)
                {
                    System.Windows.Forms.MessageBox.Show("logging aborted due to repeating messages.");
                    return;
                }
            }
            else
            {
                repeatedmessage = 0;
                prevmessagehashcode = message.GetHashCode();
            }

            const string DATETIMEW3CFORMAT = "yyyy-MM-dd hh:mm:ss";
            StringBuilder line = new StringBuilder(DateTime.Now.ToString(DATETIMEW3CFORMAT));
            while (line.Length < 19)
            {
                line.Append(" ");
            }

            switch (typemsg)
            {
                case LogType.exception:
                    line.Append(" EXCEPTION: ");
                    break;
                case LogType.error:
                    line.Append(" error:     ");
                    break;
                case LogType.info:
                    line.Append(" info:      ");
                    break;
            }

            const int MSGMAXLEN = 2048;
            if (message.Length < MSGMAXLEN)
            {
                line.AppendLine(message);
            }
            else
            {
                line.AppendLine(message.Substring(0, MSGMAXLEN));
            }

            string errorlog = string.Empty;
            if (Program.CurrentOS == Program.OS.WINDOWS) 
            {
                errorlog = Path.Combine(System.Environment.GetEnvironmentVariable("TEMP"), DEBUGLOGFILENAME);
            }
            else if (Program.CurrentOS == Program.OS.LINUX)
            {
                errorlog = "/tmp/" + DEBUGLOGFILENAME;
            }

            try
            {
                errorlog = Path.Combine(Program.AppDataFolder, DEBUGLOGFILENAME);
            }
            catch (Exception)
            {
                line.AppendLine(DateTime.Now.ToString() + " EXCEPTION: cannot set log path."); // was error but now exception because error could not be logged.
                Settings.ProgramLogError = true;
            }

            if ((Settings.ProgramLogException || typemsg == LogType.exception) || (Settings.ProgramLogError && typemsg == LogType.error) || (Settings.ProgramLogInfo && typemsg == LogType.info))
            {
                if (CheckFileSize(errorlog))
                {
                    File.Move(errorlog, errorlog + ".old");
                }

                new Textfile(TextfileWriteType.log, errorlog, null, line.ToString());
            }
        }

        /// <summary>
        /// Check if logfile larger than 512KB.
        /// </summary>
        /// <param name="file">The filename and path.</param>
        /// <returns>True if it is larger than 512KB.</returns>
        private static bool CheckFileSize(string file)
        {
            if (File.Exists(file))
            {
                const int LOGMAXSIZEKB = 512;
                FileInfo fileinfo = new FileInfo(file);
                if (fileinfo.Length > 1024 * LOGMAXSIZEKB && (fileinfo.Attributes != FileAttributes.System))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
    }
}
