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
using System.Net;
using System.Web;
using System.Security.Cryptography;
using System.IO;

namespace NoteDesk
{
    class Facebook
    {                              

        public Facebook()
        {
            //todo            
        }



        public string AppKey
        {
            get
            {
                return "cced88bcd1585fa3862e7fd17b2f6986";
            }
        }

        public string ApiVer
        {
            get
            {
                return "1.0";
            }
        }        
    }
}
