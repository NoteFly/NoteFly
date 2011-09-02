namespace NoteFly
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Windows.Forms;

    public partial class FrmUpdater : Form
    {
        private string downloadfilepath;
        private string downloadurl;

        public FrmUpdater(string downloadurl)
        {
            InitializeComponent();
            int locx = (Screen.PrimaryScreen.WorkingArea.Width / 2) - 180;
            int locy = 10;
            this.Location = new System.Drawing.Point(locx, locy);
            this.downloadurl = downloadurl;
            backgroundWorkerDownloader.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //if (downloadurl.Contains("?file="))
            //{                
            //    string filename = Path.GetFileName(downloadurl);
            //    int posparm = filename.LastIndexOf('=')+1;
            //    downloadfilepath = Path.Combine(System.Environment.GetEnvironmentVariable("TEMP"), filename.Substring(posparm, filename.Length-posparm));
            //}
            //else
            //{
                downloadfilepath = Path.Combine(System.Environment.GetEnvironmentVariable("TEMP"), Path.GetFileName(downloadurl));
            //}
           
            // first, we need to get the exact size (in bytes) of the file we are downloading
            Uri url = new Uri(downloadurl);
            
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            request.Timeout = 8000;
            request.UserAgent = Program.AssemblyTitle + " " + Program.AssemblyVersionAsString;
            request.Method = "GET";

            //request.AutomaticDecompression
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
            response.Close();

            // gets the size of the file in bytes
            long filesize = response.ContentLength;

            // keeps track of the total bytes downloaded so we can update the progress bar
            long downloadedsize = 0;

            // use the webclient object to download the file
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                client.Headers["user-agent"] = Program.AssemblyTitle + " " + Program.AssemblyVersionAsString;
                // open the file at the remote URL for reading
                using (System.IO.Stream streamRemote = client.OpenRead(new Uri(this.downloadurl)))
                {
                    // using the FileStream object, we can write the downloaded bytes to the file system
                    using (Stream streamLocal = new FileStream(downloadfilepath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        // loop the stream and get the file into the byte buffer
                        int iByteSize = 0;
                        byte[] byteBuffer = new byte[filesize];
                        while ((iByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                        {
                            // write the bytes to the file system at the file path specified
                            streamLocal.Write(byteBuffer, 0, iByteSize);
                            downloadedsize += iByteSize;

                            // calculate the progress out of a base "100"
                            double dIndex = (double)(downloadedsize);
                            double dTotal = (double)byteBuffer.Length;
                            double dProgressPercentage = (dIndex / dTotal);
                            int iProgressPercentage = (int)(dProgressPercentage * 100);

                            // update the progress bar
                            backgroundWorkerDownloader.ReportProgress(iProgressPercentage);
                        }

                        // clean up the file stream
                        streamLocal.Close();
                    }

                    // close the connection to the remote server
                    streamRemote.Close();
                }
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressbarDownload.Value = e.ProgressPercentage; 
            this.lblStatusUpdate.Text = e.ProgressPercentage + " %";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.lblStatusUpdate.Text = "download compleet, installing.. ";

            System.Threading.Thread.Sleep(100);

            System.Diagnostics.ProcessStartInfo procstartinfo = new System.Diagnostics.ProcessStartInfo(downloadfilepath);
            procstartinfo.ErrorDialog = true;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.AppStarting;
            try
            {
                System.Diagnostics.Process.Start(procstartinfo);
            }
            catch (System.ComponentModel.Win32Exception w32exc)
            {
                Log.Write(LogType.exception, w32exc.Message);
                this.Close();
            }

            Application.Exit();
        }

    }
}
