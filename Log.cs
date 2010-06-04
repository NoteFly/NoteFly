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
#define linux //platform can be: windows, linux, macos

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
        /// Something happend that is worth nothing, for instance an note got deleted.
        /// </summary>
        info
    }

    /// <summary>
    /// This class write messages to a logfile.
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// Write and append a message to the logfile.
        /// </summary>
        /// <param name="typemsg">The type of the log message</param>
        /// <param name="message">The message to log</param>
        public static void Write(LogType typemsg, string message)
        {
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
            bool logerror = false;
            bool loginfo = false;
#if window
            string errorlog = Path.Combine(System.Environment.GetEnvironmentVariable("TEMP"), "debug.log");
#elif linux
			string errorlog = "/tmp/debug.log";
#endif
            try
            {
                xmlHandler getsettings = new xmlHandler(true);
                errorlog = Path.Combine(getsettings.AppDataFolder, "debug.log");
                logerror = getsettings.getXMLnodeAsBool("logerror");
                loginfo = getsettings.getXMLnodeAsBool("loginfo");
            }
            catch (Exception)
            {
                line.AppendLine(DateTime.Now.ToString() + " EXCEPTION: cannot get log settings.");
                logerror = true;
            }

            if ((typemsg == LogType.exception) || (logerror && typemsg == LogType.error) || (loginfo && typemsg == LogType.info))
            {
                if (CheckFileSize(errorlog))
                {
                    File.Move(errorlog, errorlog + ".old");
                }

                new Textfile(false, errorlog, null, line.ToString());
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
