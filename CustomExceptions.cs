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
    class CustomExceptions: ApplicationException
    {
      public CustomExceptions() : base() { }
      public CustomExceptions(String message) : base("Exception: "+message) {

          //todo: check if logging enabled.


          //log to file
          FileStream logerrbestand = new FileStream(@"M:\Public\sourcecode\Projects_Csharp\simpleplainnote\bin\errors.txt", FileMode.Create, FileAccess.Write);
          StreamWriter bestandsSchrijver = new StreamWriter(logerrbestand);
          try
          {
              bestandsSchrijver.WriteLine(DateTime.Now.ToString() + " exception: " + message);
          }
          finally
          {
              bestandsSchrijver.Close();
              logerrbestand.Close();
          }                    


      }    
    }
}
