using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Comment out, because I gonna make it work without the FB dev. Toolkit. 
//So no overhead of things this app never is gona use is added.
//using Facebook;
//using Facebook.Schema;
//using Facebook.Winforms.Components;

namespace SimplePlainNote
{
    /// <summary>
    /// Post a note on in the facebook stream.
    /// 
    /// 
    /// </summary>
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
