//-----------------------------------------------------------------------
// <copyright file="FrmException.cs" company="GNU">
//  NoteFly a note application.
//  Copyright (C) 2010-2011  Tom
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
    public partial class FrmException : Form
    {
        /// <summary>
        /// Initializes a new instance of the FrmException class.
        /// </summary>
        /// <param name="excmgs">Exception message</param>
        /// <param name="excstrace">Exception stacktrace</param>
        public FrmException(string excmgs, string excstrace)
        {
            this.InitializeComponent();
            this.Text = "oh no.. " + Program.AssemblyTitle + " crashed.";
            StringBuilder sbexc = new StringBuilder(excmgs);
            sbexc.AppendLine();
            sbexc.AppendLine();
            sbexc.AppendLine("Stacktrace:");
            sbexc.AppendLine(excstrace);
            sbexc.AppendLine();
            sbexc.Append("version: ").Append(Program.AssemblyVersionAsString).Append(" ").AppendFormat(Program.AssemblyVersionQuality);
            this.tbExceptionMessage.Text = sbexc.ToString();
            if (!Settings.ProgramLogException)
            {
                this.lblTextStacktrace.Visible = false;
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
    }
}
