//-----------------------------------------------------------------------
// <copyright file="CustomException.cs" company="GNU">
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

namespace NoteFly
{
    using System;
    using System.IO;

    /// <summary>
    /// Own exceptions, and log what happend.
    /// </summary>
    public class CustomException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the CustomException class.
        /// This let the programme crash.
        /// </summary>
        /// <param name="message">the message to log</param>
        public CustomException(string message)
            : base("Exception: " + message)
        {
            // check if exception logging is enabled
            if (Settings.ProgramLogException)
            {
                Log.Write(LogType.exception, message);
            }
        }
    }
}
