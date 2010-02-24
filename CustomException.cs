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
using System;
using System.IO;


namespace NoteFly
{
    /// <summary>
    /// Own exceptions, and log what happend.
    /// </summary>
    public class CustomException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the CustomExceptions class.
        /// This let the programme crash.
        /// </summary>
        /// <param name="message">the message to log</param>
        public CustomException(String message)
            : base("Exception: " + message)
        {
            //check if error loging is enabled
            xmlHandler getsetting = new xmlHandler(true);
            
            if (getsetting.getXMLnodeAsBool("logerror") == true)
            {
                Log.write(LogType.exception, message);
            } 
            
        }

    }


}
