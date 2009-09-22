using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace SimplePlainNote
{

    /// <summary>
    /// wel een voordeel van deze class is dat je nog bijv. nog een logfile kunt schrijven.
    /// </summary>
    class CustomExceptions : ApplicationException
    {
        public CustomExceptions() : base() { }
        public CustomExceptions(String message)
            : base("Exception: " + message)
        {
            //check if error loging is enabled
            xmlHandler getsetting = new xmlHandler(true);
            if (getsetting.getXMLnodeAsBool("logerror") == true)
            {
                writelog(getsetting.AppDataFolder, message);
            }            
            
            //shutdown        
            //Trayicon. icon.Dispose();
        }

        private bool writelog(string appdatafolder, string message)
        {
            //log to file
            string errorlog = Path.Combine(appdatafolder, "errors.log");
            if ((!String.IsNullOrEmpty(errorlog)) && (File.Exists(errorlog)))
            {
                try
                {
                    StreamWriter bestandsSchrijver = new StreamWriter(errorlog, true);
                    try
                    {
                        bestandsSchrijver.WriteLine(DateTime.Now.ToString() + " exception: " + message);
                    }
                    finally
                    {
                        bestandsSchrijver.Close();
                    }
                }
                catch (Exception)
                { //do nothing                                       
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
