//-----------------------------------------------------------------------
// <copyright file="FrmAbout.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2010-2013  Tom
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
        #region Fields (3)

        /// <summary>
        /// Constant project website uri.
        /// </summary>
        private const string NOTEFLYWEBSITEURI = "http://www.notefly.org/";

        /// <summary>
        /// All the moving authors labels.
        /// </summary>
        private MovingAuthorLabel[] movinglabels;

        /// <summary>
        /// Delta point
        /// </summary>
        private Point oldp;

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

            this.movinglabels = new MovingAuthorLabel[] {
                 new MovingAuthorLabel(Strings.T("Developed\nby") + " D9ping", 100),
                 new MovingAuthorLabel(Strings.T("Greek translation\nby") + " geogeo.gr", 160),
                 new MovingAuthorLabel(Strings.T("Korea translation\nby") + " zest", 220),
                 new MovingAuthorLabel("May your notes,\ncome in handy..", 300)
            };
            for (int i = 0; i < this.movinglabels.Length; i++)
            {
                this.pnlAuthors.Controls.Add(this.movinglabels[i]);
            }
        }

        #endregion Constructors 

        #region Methods (2)

        /// <summary>
        /// Set the title of this form.
        /// </summary>
        private void SetFormTitle()
        {
            this.Text = Strings.T("About") + " - " + Program.AssemblyTitle;
            this.lbTextWindowTitle.Text = Strings.T("About");
        }

        /// <summary>
        /// The Website link is clicked in the about dialog.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void linklblWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Program.LoadLink(NOTEFLYWEBSITEURI, false);
            this.Close();
        }

        /// <summary>
        /// An OK button is clicked, close FrmAbout.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void okButton_Click(object sender, EventArgs e)
        {
            this.tmrUpdate.Stop();
            this.Close();
        }

        /// <summary>
        /// Start color effect on lblProductName.
        /// Bonus / easter egg.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void lblProductName_Click(object sender, EventArgs e)
        {
            this.tmrUpdate.Start();
            this.DoubleBuffered = true;
        }

        /// <summary>
        /// A moving label.
        /// </summary>
        public class MovingAuthorLabel : Label
        {
            private const int speed = 1;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="text"></param>
            /// <param name="startposy"></param>
            public MovingAuthorLabel(string text, int startposy)
            {
                this.Text = text;
                this.Location = new System.Drawing.Point(0, startposy);
                this.AutoSize = true;
                this.AutoEllipsis = true;
            }

            /// <summary>
            /// 
            /// </summary>
            public void MoveUp() {
                if (this.Location.Y > speed)
                {
                    this.Location = new System.Drawing.Point(this.Location.X, this.Location.Y - speed);
                }
                else
                {
                    this.Visible = false;
                }
            }
        }

        /// <summary>
        /// Update moving labels.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void tmpUpdateLblProductEffect_Tick(object sender, EventArgs e)
        {
            
            for (int i = 0; i < this.movinglabels.Length; i++)
            {
                this.movinglabels[i].MoveUp();
            }

            /*
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
            */
        }

        /// <summary>
        /// The start of dragging this window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlHead_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.oldp = e.Location;
            }
        }

        /// <summary>
        /// Dragging this window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlHead_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int dpx = e.Location.X - this.oldp.X;
                int dpy = e.Location.Y - this.oldp.Y;
#if linux
                // limit the moving of this note under mono/linux so this note cannot move uncontrolled a lot.
                const int movelimit = 8;
                if (dpx > movelimit)
                {
                    dpx = movelimit;
                }
                else if (dpx < -movelimit)
                {
                    dpx = -movelimit;
                }

                if (dpy > movelimit)
                {
                    dpy = movelimit;
                }
                else if (dpy < -movelimit)
                {
                    dpy = -movelimit;
                }
#endif
                this.Location = new Point(this.Location.X + dpx, this.Location.Y + dpy);
            }
        }

        #endregion Methods
    }
}