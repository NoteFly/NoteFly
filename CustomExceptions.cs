using System;
using System.Collections.Generic;
using System.Text;

namespace SimplePlainNote
{
    /// <summary>
    /// wel een voordeel van deze class is dat je nog bijv. nog een logfile kunt schrijven.
    /// </summary>
    class CustomExceptions: ApplicationException
    {
      public CustomExceptions() : base() { }
      public CustomExceptions(String message) : base("Exception: "+message) {          
      }    
    }
}
