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
    using System.Windows.Forms;
    using System.Text;

    public partial class FrmException : Form
    {
        public FrmException(String excmgs, String excstrace)
        {
            InitializeComponent();
            this.Text = "oh no.. " + Program.AssemblyTitle + " crashed.";
            StringBuilder sbexc = new StringBuilder(excmgs);
            sbexc.AppendLine();
            sbexc.AppendLine();
            sbexc.AppendLine("Stacktrace:");
            sbexc.AppendLine(excstrace);
            this.tbExceptionMessage.Text = sbexc.ToString();
            if (!Settings.programLogException)
            {
                this.lblTextStacktrace.Visible = false;
            }
        }

        private void btnContinu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnShutdown_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
