using System;
using System.IO;

namespace NoteFly
{
    /// <summary>
    /// Save the note as a textfile
    /// </summary>
    public class Textfile
    {
        public Textfile(string filename, string title, string note)
        {
            FileStream fs = null;
            StreamWriter writer = null;
            try
            {
                fs = new FileStream(filename, FileMode.OpenOrCreate);
                writer = new StreamWriter(fs);
                writer.Write("Title: "+title+"\r\n\r\n");
                writer.Write(note);
            }
            catch (Exception exc)
            {
                throw new CustomExceptions(exc.Message);
            }
            finally
            {                
                writer.Close();
                fs.Close();
            }
        }
    }
}
