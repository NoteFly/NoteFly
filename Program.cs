using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SimplePlainNote
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //public String inifile;
            //String inifile = System.Environment.GetEnvironmentVariable("APPDATA") + "\\.simpleplainnote\\settings.ini";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new frmNewNote());
        }
    }
}
