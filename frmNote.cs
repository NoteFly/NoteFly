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
    }
}
