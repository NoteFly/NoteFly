using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NoteFly
{
    public enum LogType
    {
        exception, //Application crash, unexpected/bug
        error, //User did something wrong, application handled it okay.
        info //something important happend, for instance note got deleted.
    }

    public static class Log
    {
        /// <summary>
        /// Write and append a message to the logfile.
        /// </summary>
        /// <param name="appdatafolder"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static void write(LogType typemsg, string message)
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
            String errorlog = Path.Combine("%TEMP%", "debug.log");
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
        /// <returns></returns>
        private static bool CheckFileSize(String file)
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
