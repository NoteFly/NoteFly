//-----------------------------------------------------------------------
// <copyright file="CustomException.cs" company="GNU">
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
    /// CustomException class
    /// </summary>
    public class CustomException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the CustomException class.
        /// </summary>
        /// <param name="message">the message to log</param>
        public CustomException(string message)
            : base("Exception: " + message)
        {
            // check if exception logging is enabled
            if (Settings.programLogException)
            {
                Log.Write(LogType.exception, message);
            }
        }
    }
}
