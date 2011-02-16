//-----------------------------------------------------------------------
// <copyright file="Log.cs" company="GNU">
// 
// This program is free software; you can redistribute it and/or modify it
// Free Software Foundation; either version 2, 
// or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// </copyright>
//-----------------------------------------------------------------------
#define windows //platform can be: windows, linux, macos

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
    public class Log
    {
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
            if (!Settings.programLogInfo && typemsg == LogType.info)
            {
                return;
            }
            else if (!Settings.programLogError && typemsg == LogType.error)
            {
                return;
            }
            else if (!Settings.programLogException && typemsg == LogType.exception)
            {
                return;
            }

            StringBuilder line = new StringBuilder(DateTime.Now.ToString());
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

            line.AppendLine(message);
#if windows
            string errorlog = Path.Combine(System.Environment.GetEnvironmentVariable("TEMP"), DEBUGLOGFILENAME);
#elif linux
            string errorlog = "/tmp/"+debuglogfilename;
#endif
            try
            {
                errorlog = Path.Combine(Program.AppDataFolder, DEBUGLOGFILENAME);
            }
            catch (Exception)
            {
                line.AppendLine(DateTime.Now.ToString() + " EXCEPTION: cannot set log path."); //was error but now exception because error could not be logged.
                Settings.programLogError = true;
            }

            if ((Settings.programLogException || typemsg == LogType.exception) || (Settings.programLogError && typemsg == LogType.error) || (Settings.programLogInfo && typemsg == LogType.info))
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
        /// <param name="file">the filename and path</param>
        /// <returns>true if it is larger than 512KB</returns>
        private static bool CheckFileSize(string file)
        {
            if (File.Exists(file))
            {
                FileInfo fileinfo = new FileInfo(file);
                if (fileinfo.Length > 1024 * 512 && (fileinfo.Attributes != FileAttributes.System))
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
