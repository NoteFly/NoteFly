//-----------------------------------------------------------------------
// <copyright file="FrmSmileyChooser.cs" company="InsertSmiley">
//  NoteFly a note application.
//  Copyright (C) 2013  Tom
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
namespace InsertSmiley
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using IPlugin;

    public partial class FrmSmileyChooser : Form
    {
        /// <summary>
        /// Interface
        /// </summary>
        private IPluginHost host;

        /// <summary>
        /// The current RTF stream.
        /// </summary>
        //private string rtf;

        /// <summary>
        /// The position where to add RTF.
        /// </summary>
        private int insertposition = int.MaxValue;

        /// <summary>
        /// Pointer to richformattextbox
        /// </summary>
        private RichTextBox rtb;

        /// <summary>
        /// Creating a new instance of FrmSmileyChooser class.
        /// </summary>
        /// <param name="x">X coordinate of mouse click.</param>
        /// <param name="y">Y coordinate of mouse click.</param>
        /// <param name="rtb">The RicheditTextbox control.</param>
        /// <param name="insertposition">The position where to add RTF.</param>
        /// <param name="rtf">The current RTF stream.</param>
        public FrmSmileyChooser(IPluginHost host, int x, int y, RichTextBox rtb, int insertposition, string rtf)
        {
            this.host = host;
            //this.rtf = rtf;
            this.rtb = rtb;
            //this.insertposition = insertposition;
            InitializeComponent();
            if (x > (this.Width / 2) && y > (this.Height / 2))
            {
                this.btnSmileySmile.Select();
                if ((x > (Screen.PrimaryScreen.WorkingArea.Width - (this.Width / 2))) || (y > (Screen.PrimaryScreen.WorkingArea.Height - (this.Height / 2))))
                {
                    this.Location = new Point(x - this.Width, y - this.Height);
                }
                else
                {
                    this.Location = new Point(x - (this.Width / 2), y - (this.Height / 2));
                }
            }
            else
            {
                this.Location = new Point(x, y);
            }
        }

        /// <summary>
        /// Insert the image to the richtextbox, by pasting it.
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="img"></param>
        private void InsertImageRtb(Image img) {
            IDataObject oldclipboarddata = Clipboard.GetDataObject();
            Clipboard.Clear();
            Clipboard.SetImage(img);
            this.rtb.Paste();
            Clipboard.Clear();
            Clipboard.SetDataObject(oldclipboarddata);
        }

        /// <summary>
        /// An smiley image is choicen add the smiley image to the RTF stream of the RicheditTextBox.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnSmileyChoice_Click(object sender, EventArgs e)
        {
            Button btnsmiley = (Button)sender;
            Image smiley = btnsmiley.Image;
            if (smiley != null)
            {
                this.InsertImageRtb(smiley);
                this.Close();
            }
        }

        /// <summary>
        /// The FrmSmileyChooser lost focus, e.g. click beside it. Close the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmSmileyChooser_Deactivate(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
