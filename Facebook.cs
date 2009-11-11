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

//Comment out, because I gonna make it work without the FB dev. Toolkit. 
//So no overhead of things this app never is gona use is added.
//using Facebook;
//using Facebook.Schema;
//using Facebook.Winforms.Components;

namespace SimplePlainNote
{
    class Facebook
    {
        //private FacebookService fbservice; 

        public Facebook()
        {

            //fbservice = new FacebookService();
            //fbservice.ApplicationKey = FbAppKey;            
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



        /*                   
        public bool Update(string note)
        {
            try
            {
                fbservice.ConnectToFacebook(new List<Enums.ExtendedPermissions>() { Enums.ExtendedPermissions.publish_stream });
                fbservice.Stream.Publish(note);
            }
            catch (Exception)
            {
                return false;
            }           
            return true;
        }
         */
    }
}
