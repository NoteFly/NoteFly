//-----------------------------------------------------------------------
// <copyright file="FrmAbout.cs" company="GNU">
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
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;

    /// <summary>
    /// About window.
    /// </summary>
    public partial class FrmAbout : Form
    {
        private const string NOTEFLYWEBSITEURI = "http://www.notefly.tk/";
        private const string NOTEFLYFAQURI = "http://www.notefly.tk/faq.php";

        #region Constructors (1) 

        /// <summary>
        /// Initializes a new instance of the FrmAbout class.
        /// </summary>
        public FrmAbout()
        {
            this.InitializeComponent();
            this.Text = "About " + Program.AssemblyTitle;
            this.lblProductName.Text = Program.AssemblyTitle;
            this.lblVersion.Text = String.Format("Version "+Program.AssemblyVersionAsString);
        }

        #endregion Constructors

        #region Methods (3)

        // Private Methods (3) 

        /// <summary>
        /// The FAQ link is clicked in the about dialog.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void linkLblFAQ_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Program.LoadLink(NOTEFLYFAQURI);
            this.Close();
        }

        /// <summary>
        /// The Website link is clicked in the about dialog.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void linklblWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Program.LoadLink(NOTEFLYWEBSITEURI);
            this.Close();
        }

        /// <summary>
        /// An OK button is clicked, close FrmAbout.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion Methods
    }
}
