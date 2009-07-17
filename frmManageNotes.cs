﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SimplePlainNote
{
    /// <summary>
    /// Manage notes class
    /// </summary>
    public partial class frmManageNotes : Form
    {
        private bool transparency = true;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();


        public frmManageNotes(frmNewNote fcn)
        {
            InitializeComponent();
            DrawNotesOverview(fcn.GetNotes);
        }


        private void DrawNotesOverview(List<FrmNote> notes)
        {            
            int ypos = 10;            
            for (int i = 0; i < notes.Count; i++)
            {
                Label lblNoteTitle = new Label();
                lblNoteTitle.Text = notes[i].Title;
                lblNoteTitle.Name = "lbNote"+Convert.ToString(i);
                lblNoteTitle.Location = new Point(10, ypos);
                pnlNotes.Controls.Add(lblNoteTitle);

                CheckBox cbxNoteVisible = new CheckBox();
                cbxNoteVisible.Text = "visible";
                cbxNoteVisible.Name = "cbxNoteVisible" + Convert.ToString(i);
                if (notes[i].NoteVisible == true)
                {
                    cbxNoteVisible.CheckState = CheckState.Checked;
                }
                else
                {
                    cbxNoteVisible.CheckState = CheckState.Unchecked;
                }
                cbxNoteVisible.Location = new Point(175, ypos);
                cbxNoteVisible.AutoEllipsis = true;
                cbxNoteVisible.AutoSize = true;
                pnlNotes.Controls.Add(cbxNoteVisible);

                Button btnNoteDelete = new Button();
                btnNoteDelete.Text = "delete";
                btnNoteDelete.Name = "btnNoteDel" + Convert.ToString(i);
                btnNoteDelete.BackColor = Color.Orange;
                btnNoteDelete.Location = new Point(240, ypos);
                btnNoteDelete.Width = 60;
                pnlNotes.Controls.Add(btnNoteDelete);
                
                ypos = ypos + 30;
            }
        }

        
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManageNotes_Shown(object sender, EventArgs e)
        {
            //DrawNotesOverview(notes);
        }

        private void pnlHead_MouseDown(object sender, MouseEventArgs e)
        {
            pnlHead.BackColor = Color.OrangeRed;
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                pnlHead.BackColor = Color.Orange;
            }
        }

        private void frmManageNotes_Activated(object sender, EventArgs e)
        {
            if (transparency)
            {
                this.Opacity = 1.0;
                this.Refresh();
            }
        }

        private void frmManageNotes_Deactivate(object sender, EventArgs e)
        {
            if (transparency)
            {
                this.Opacity = 0.9;
                this.Refresh();
            }
        }
    }
}
