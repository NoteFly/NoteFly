//-----------------------------------------------------------------------
// <copyright file="FrmAbout.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2010-2012  Tom
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
    using System.Windows.Forms;

    /// <summary>
    /// About window.
    /// </summary>
    public sealed partial class FrmAbout : Form
    {
        #region Fields (1)

        /// <summary>
        /// constant project website uri.
        /// </summary>
        private const string NOTEFLYWEBSITEURI = "http://www.notefly.org/";

        #endregion Fields 

        #region Constructors (1) 

        /// <summary>
        /// Initializes a new instance of the FrmAbout class.
        /// </summary>
        public FrmAbout()
        {
            this.InitializeComponent();
            this.SetFormTitle();
            this.lblProductName.Text = Program.AssemblyTitle;
            this.lblVersion.Text = string.Format(Strings.T("Version ") + Program.AssemblyVersionAsString + " " + Program.AssemblyVersionQuality);
        }

        #endregion Constructors 

        #region Methods (2)

        /// <summary>
        /// Set the title of this form.
        /// </summary>
        private void SetFormTitle()
        {
            this.Text = Strings.T("About") + " - " + Program.AssemblyTitle;
        }

        /// <summary>
        /// The Website link is clicked in the about dialog.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void linklblWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Program.LoadLink(NOTEFLYWEBSITEURI, false);
            this.Close();
        }

        /// <summary>
        /// An OK button is clicked, close FrmAbout.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void okButton_Click(object sender, EventArgs e)
        {
            this.tmpUpdateLblProductEffect.Stop();
            this.Close();
        }

        /// <summary>
        /// Start color effect on lblProductName.
        /// Bonus / easter egg.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblProductName_Click(object sender, EventArgs e)
        {
            this.tmpUpdateLblProductEffect.Start();
            this.DoubleBuffered = true;
        }

        /// <summary>
        /// Update color effect on lblProductName.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmpUpdateLblProductEffect_Tick(object sender, EventArgs e)
        {
            const int MAXDARK = 250;
            byte red = this.lblProductName.ForeColor.R;
            byte blue = this.lblProductName.ForeColor.B;
            byte green = this.lblProductName.ForeColor.G;
            if (red < MAXDARK && green <= 5)
            {
                red += 5;
                if (blue > 5)
                {
                    blue -= 5;
                }
            }
            else if (green < MAXDARK && blue <= 5)
            {
                green += 5;
                if (red > 5)
                {
                    red -= 5;
                }
            }
            else if (blue < MAXDARK && red <= 5)
            {
                blue += 5;
                if (green > 5)
                {
                    green -= 5;
                }
            }

            this.lblProductName.ForeColor = Color.FromArgb(red, green, blue);
            //this.BackColor = Color.FromArgb(128, 250 - green, 250 - blue);
        }

        #endregion Methods
    }
}