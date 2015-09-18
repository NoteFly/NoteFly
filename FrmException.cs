//-----------------------------------------------------------------------
// <copyright file="FrmException.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2010-2015  Tom
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
// </copyright>
//-----------------------------------------------------------------------
namespace NoteFly
{
    using System;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// Exception window
    /// </summary>
    public sealed partial class FrmException : Form
    {
        private const string URLBASEREPORTISSUE = "https://www.notefly.org/bugs/bug_report_page.php";

        private const int URLMAXLEN = 2083;

        private const string URISTACKTRACEPARAM = "?st=";

        private string urlEncodedStackTrace = "";

        /// <summary>
        /// Initializes a new instance of the FrmException class.
        /// </summary>
        /// <param name="excmgs">Exception message</param>
        /// <param name="excstacktrace">Exception stacktrace</param>
        public FrmException(string excmgs, string excstacktrace)
        {
            this.InitializeComponent();
            this.Text = Strings.T("Oh no.. {0} crashed.", Program.AssemblyTitle);
            const string SEPERATORTEXTANDCONTENT = ": ";
            excstacktrace = excstacktrace.Trim();
            StringBuilder sbexc = new StringBuilder(excmgs);
            sbexc.AppendLine();
            sbexc.AppendLine("Stacktrace" + SEPERATORTEXTANDCONTENT);
            sbexc.AppendLine(excstacktrace);
            sbexc.AppendLine();
            sbexc.Append(Strings.T("{0} version" + SEPERATORTEXTANDCONTENT, Program.AssemblyTitle)).Append(Program.AssemblyVersionAsString).Append(" ").AppendLine(Program.AssemblyVersionQuality);
            sbexc.Append(Strings.T("OS" + SEPERATORTEXTANDCONTENT) + Enum.GetName(typeof(Program.OS), Program.CurrentOS));
            this.tbExceptionMessage.Text = sbexc.ToString();
            if (!Settings.ProgramLogException)
            {
                this.lblTextStacktrace.Visible = false;
            }

            if (URLBASEREPORTISSUE.Length + URISTACKTRACEPARAM.Length + excstacktrace.Length < URLMAXLEN) 
            {
                this.urlEncodedStackTrace = System.Web.HttpUtility.UrlEncodeUnicode(excstacktrace);
            }
        }

        /// <summary>
        /// The continu button is clicked, try to continu to run.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnContinu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// The shutdown button is clicked, shutdown this application.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnShutdown_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Open bug tracker webpage on report bug page
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">LinkLabelLink Clicked event arguments</param>
        private void linklblCreateBugReport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Program.LoadLink(URLBASEREPORTISSUE + URISTACKTRACEPARAM + this.urlEncodedStackTrace, false);
        }
    }
}
