/**
 *                  UnhandledExceptionDlg Class v. 1.1
 * 
 *                      Copyright (c)2006 Vitaly Zayko
 * 
 * History:
 * September 26, 2006 - Added "ThreadException" handler, "SetUnhandledExceptionMode", OnShowErrorReport event 
 *                      and updated the Demo and code comments;
 * August 29, 2006 - Updated information about Microsoft Windows Error Reporting service and its link;
 * July 18, 2006 - Initial release.
 * 
 */

/** More info on MSDN: 
 * http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnbda/html/exceptdotnet.asp
 * http://msdn2.microsoft.com/en-us/library/system.windows.forms.application.threadexception.aspx
 * http://msdn2.microsoft.com/en-us/library/system.appdomain.unhandledexception.aspx
 * http://msdn2.microsoft.com/en-us/library/system.windows.forms.unhandledexceptionmode.aspx
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SimplePlainNote
{
    internal class SendExceptionClickEventArgs: System.EventArgs
    {
        public bool SendExceptionDetails;
        public Exception UnhandledException;
        public bool RestartApp;

        public SendExceptionClickEventArgs(bool SendDetailsArg, Exception ExceptionArg, bool RestartAppArg)
        {
            this.SendExceptionDetails = SendDetailsArg;     // TRUE if user clicked on "Send Error Report" button and FALSE if on "Don't Send"
            this.UnhandledException = ExceptionArg;         // Used to store captured exception
            this.RestartApp = RestartAppArg;                // Contains user's request: should the App to be restarted or not
        }
    }

    /// <summary>
    /// Class for catching unhandled exception with UI dialog.
    /// </summary>
    class UnhandledExceptionDlg
    {
        private bool _dorestart = true;

        /// <summary>
        /// Set to true if you want to restart your App after falure
        /// </summary>
        public bool RestartApp
        {
            get { return _dorestart; }
            set { _dorestart = value; }
        }

        public delegate void SendExceptionClickHandler(object sender, SendExceptionClickEventArgs args);

        //public delegate void ShowErrorReportHandler(object sender, System.EventArgs args);

        /// <summary>
        /// Occurs when user clicks on "Send Error report" button
        /// </summary>
        public event SendExceptionClickHandler OnSendExceptionClick;

        /// <summary>
        /// Occurs when user clicks on "click here" link lable to get data that will be send
        /// </summary>
        public event SendExceptionClickHandler OnShowErrorReport;

        /// <summary>
        /// Default constructor
        /// </summary>
        public UnhandledExceptionDlg()
        {
            // Add the event handler for handling UI thread exceptions to the event:
            Application.ThreadException += new ThreadExceptionEventHandler(ThreadExceptionFunction);

            // Set the unhandled exception mode to force all Windows Forms errors to go through our handler:
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Add the event handler for handling non-UI thread exceptions to the event:
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionFunction);
        }

        /// <summary>
        /// Handle the UI exceptions by showing a dialog box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadExceptionFunction(Object sender, ThreadExceptionEventArgs e)
        {
            // Suppress the Dialog in Debug mode:
            #if !DEBUG
            ShowUnhandledExceptionDlg(e.Exception);
            #endif
        }

        /// <summary>
        /// Handle the UI exceptions by showing a dialog box
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="args">Passing arguments: original exception etc.</param>
        private void UnhandledExceptionFunction(Object sender, UnhandledExceptionEventArgs args)
        {
            // Suppress the Dialog in Debug mode:
            #if !DEBUG
            ShowUnhandledExceptionDlg((Exception)args.ExceptionObject);
            #endif
        }

        /// <summary>
        /// Raise Exception Dialog box for both UI and non-UI Unhandled Exceptions
        /// </summary>
        /// <param name="e">Catched exception</param>
        private void ShowUnhandledExceptionDlg(Exception e)
        {
            Exception unhandledException = e;

            if(unhandledException == null)
                unhandledException = new Exception("Unknown unhandled Exception was occurred!");

            UnhandledExDlgForm exDlgForm = new UnhandledExDlgForm();
            try
            {
                string appName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                exDlgForm.Text = appName;
                exDlgForm.labelTitle.Text = String.Format(exDlgForm.labelTitle.Text, appName);
                exDlgForm.checkBoxRestart.Text = String.Format(exDlgForm.checkBoxRestart.Text, appName);
                exDlgForm.checkBoxRestart.Checked = this.RestartApp;

                // Do not show link label if OnShowErrorReport is not handled
                exDlgForm.labelLinkTitle.Visible = (OnShowErrorReport != null);
                exDlgForm.linkLabelData.Visible = (OnShowErrorReport != null);

                // Disable the Button if OnSendExceptionClick event is not handled
                exDlgForm.buttonSend.Enabled = (OnSendExceptionClick != null);

                // Attach reflection to checkbox checked status
                exDlgForm.checkBoxRestart.CheckedChanged += delegate(object o, EventArgs ev)
                {
                    this._dorestart = exDlgForm.checkBoxRestart.Checked;
                };

                // Handle clicks on report link label
                exDlgForm.linkLabelData.LinkClicked += delegate(object o, LinkLabelLinkClickedEventArgs ev)
                {
                    if(OnShowErrorReport != null)
                    {
                        SendExceptionClickEventArgs ar = new SendExceptionClickEventArgs(true, unhandledException, _dorestart);
                        OnShowErrorReport(this, ar);
                    }
                };

                // Show the Dialog box:
                bool sendDetails = (exDlgForm.ShowDialog() == System.Windows.Forms.DialogResult.Yes);

                if(OnSendExceptionClick != null)
                {
                    SendExceptionClickEventArgs ar = new SendExceptionClickEventArgs(sendDetails, unhandledException, _dorestart);
                    OnSendExceptionClick(this, ar);
                }
            }
            finally
            {
                exDlgForm.Dispose();
            }
        }
        
    }
}
