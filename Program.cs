﻿/* Copyright (C) 2009
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
#define win32

using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;

[assembly: CLSCompliant(true)]
namespace SimplePlainNote
{    
    /// <summary>
    /// Startup class.
    /// </summary>
    static class Program
    {        
        //win32 
        //macx 
        //linux
        //public const string PLATFORM = "win32";         

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]               
        static void Main()
        {                                    
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);            
            Application.Run(new frmNewNote());
        }
    }
}
