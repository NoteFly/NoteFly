/* Copyright (C) 2009
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
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace SimplePlainNote
{
    /// <summary>
    /// Own exceptions, and log what happend.
    /// </summary>
    class CustomExceptions : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the CustomExceptions class.
        /// </summary>
        /// <param name="message">the message to log</param>
        public CustomExceptions(String message)
            : base("Exception: " + message)
        {
            //check if error loging is enabled
            xmlHandler getsetting = new xmlHandler(true);
            if (getsetting.getXMLnodeAsBool("logerror") == true)
            {
                writelog(getsetting.AppDataFolder, message);
            }                        
        }

        /// <summary>
        /// Write and append a message to the logfile.
        /// </summary>
        /// <param name="appdatafolder"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private bool writelog(string appdatafolder, string message)
        {
            //log to file
            string errorlog = Path.Combine(appdatafolder, "errors.log");
            if ((!String.IsNullOrEmpty(errorlog)) && (File.Exists(errorlog)))
            {
                StreamWriter bestandsSchrijver = null;
                try
                {
                    bestandsSchrijver = new StreamWriter(errorlog, true);
                    bestandsSchrijver.WriteLine(DateTime.Now.ToString() + " exception: " + message);
                }
                finally
                {
                    bestandsSchrijver.Close();
                }
            }
            else
            {
                return false;
            }
            return true;
        }
    }


}
