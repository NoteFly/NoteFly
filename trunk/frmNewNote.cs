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
    public partial class frmNewNote : Form
    {
        public frmNewNote()
        {
            InitializeComponent();
        }

        private void btnAddNote_Click(object sender, EventArgs e)
        {
            if (tbTitle.Text == "")
            {
                tbTitle.BackColor = Color.Red;                
                tbTitle.Text = DateTime.Now.ToString();                
            }
            else if (rtbNote.Text == "")
            {
                rtbNote.BackColor = Color.Red;
                Console.Beep();
                rtbNote.Text = "Please type any note.";
            }
            else
            {
                frmNote frmNote = new frmNote(tbTitle.Text, rtbNote.Text);
                frmNote.Show();
                this.Hide();
            }

        }

        private void frmNewNote_Shown(object sender, EventArgs e)
        {
            tbTitle.Text = DateTime.Now.ToString();
        }

        private void tbTitle_Enter(object sender, EventArgs e)
        {
            if (tbTitle.Focused)
            {
                tbTitle.BackColor = Color.LightYellow;
                rtbNote.BackColor = Color.Gold;
            }
        }

        private void rtbNote_Enter(object sender, EventArgs e)
        {
            if (rtbNote.Focused)
            {
                rtbNote.BackColor = Color.LightYellow;
                tbTitle.BackColor = Color.Gold;                
            }
        }

        private void tbTitle_Leave(object sender, EventArgs e)
        {
            tbTitle.BackColor = Color.Gold;
        }
    }
}
