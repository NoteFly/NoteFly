using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SimplePlainNote
{
    public partial class frmNote : Form
    {
        private bool notemoving = false;

        public frmNote(string title, string note)
        {
            InitializeComponent();
            lblTitle.Text = title;
            rtbNote.Text = note;
        }

        private void frmDeleteNote_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pnlHead_MouseDown(object sender, MouseEventArgs e)
        {
            pnlHead.BackColor = Color.Goldenrod;
            notemoving = true;
        }

        private void pnlHead_MouseLeave(object sender, EventArgs e)
        {
            pnlHead.BackColor = Color.Gold;
            notemoving = false;
        }

        private void pnlHead_MouseMove(object sender, MouseEventArgs e)
        {
            if (notemoving)
            {
                this.Location = e.Location;
            }
        }
    }
}
