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

        /// <summary>
        /// Creating a new instance of FrmUpdater class.
        /// </summary>
        /// <param name="downloadurl"></param>
        public FrmUpdater(string downloadurl)
        {
            InitializeComponent();
            int locx = (Screen.PrimaryScreen.WorkingArea.Width / 2) - 180;
            int locy = 10;
            this.Location = new System.Drawing.Point(locx, locy);
            this.downloadurl = downloadurl;
            backgroundWorkerDownloader.RunWorkerAsync();
        }

        /// <summary>
        /// Downloading update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            downloadfilepath = Path.Combine(System.Environment.GetEnvironmentVariable("TEMP"), Path.GetFileName(downloadurl));
            if (CheckValidPath(downloadfilepath))
            {
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
            else
            {
                const string INVLAIDFILENAMEORPATH = "Invalid filepath/filename downloaded file";
                Log.Write(LogType.exception, INVLAIDFILENAMEORPATH);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// update download process.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressbarDownload.Value = e.ProgressPercentage; 
            this.lblStatusUpdate.Text = e.ProgressPercentage + " %";
        }

        /// <summary>
        /// run download if download is compleet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
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
            else
            {
                this.lblStatusUpdate.Text = "aborted.";
            }
        }
        
        /// <summary>
        /// Check if the path does not contain forbidden filename/filepath characters
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool CheckValidPath(string path)
        {
            char[] forbiddenchars = "?<>:*|\"".ToCharArray();
            if (downloadfilepath.IndexOfAny(forbiddenchars) < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
