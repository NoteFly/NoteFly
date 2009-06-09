using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SimplePlainNote
{
    public partial class frmNote : Form
    {
        private bool transparency = true;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, 
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        
        private int id;
        private string title;
        private string note;
        private int notecolor = 0;
        private frmNewNote fcn;
        

        public frmNote(int id, string title, string note, frmNewNote fcn)
        {            
            this.id = id;
            this.title = title;
            this.fcn = fcn;
            this.note = note;
            InitializeComponent();            
            lblTitle.Text = title;
            rtbNote.Text = note;            
        }

        public int ID
        {
            get { return id; }
            set { this.id = value; }
        }
        public string Title
        {
            get { return this.title; }
        }
        public int ColorNote
        {
            get
            {
                return this.notecolor;
            }
            set
            {
                value = notecolor;
            }
        }
        public string Note
        {
            get { return this.note; }
            set
            {
                note = value;
                rtbNote.Text = note;
            }
        }

        private void frmDeleteNote_Click(object sender, EventArgs e)
        {
            transparency = false;
            fcn.DeleteNote(this.id);            
            this.Close();
            
        }

        private void pnlHead_MouseDown(object sender, MouseEventArgs e)
        {
            pnlHead.BackColor = getObjColor(true);

            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                pnlHead.BackColor = getObjColor(false);
            }

        }

        /// <summary>
        /// Resize note
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlResizeWindow_MouseDown(object sender, MouseEventArgs e)
        {            
            Cursor = Cursors.SizeNWSE;            
            //e.Location
        }

        private void frmNote_Deactivate(object sender, EventArgs e)
        {
            if (transparency)
            {
                this.Opacity = 0.9;
                this.Refresh();
            }
        }

        private void frmNote_Activated(object sender, EventArgs e)
        {
            if (transparency)
            {
                this.Opacity = 1.0;
                this.Refresh();
            }
        }

        private void changeColorToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            /*
            if (notecolor == Color.Gold) yellowToolStripMenuItem.Checked = true;
            else if (notecolor == Color.Orange) orangeToolStripMenuItem.Checked = true;
            else if (notecolor == Color.White) whiteToolStripMenuItem.Checked = true;
            else if (notecolor == Color.Green) greenToolStripMenuItem.Checked = true;
            else if (notecolor == Color.Blue) blueToolStripMenuItem.Checked = true;
             */
        }

        private void setColorNote(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem curitem in menuNoteColors.DropDownItems)
            {
                if (curitem == sender) 
                {
                    curitem.Checked = true;
                }
                else
                {
                    curitem.Checked = false;
                }
                
            }                      


            pnlHead.BackColor = Color.Gold;
        }

        private Color getObjColor(bool selected)
        {
            switch (this.notecolor)
            {
                case 0:
                    if (selected) return Color.Orange;
                    else return Color.Gold;
                case 1:
                    if (selected) return Color.DarkOrange;
                    else return Color.Orange;
                case 2:
                    if (selected) return Color.Gray;
                    else return Color.White;
                case 3:
                    if (selected) return Color.DarkGreen;
                    else return Color.Green;
                case 4:
                    if (selected) return Color.DarkBlue;
                    else return Color.Blue;
                default:
                    return Color.Gold;

            }            
        }

        private void editTToolStripMenuItem_Click(object sender, EventArgs e)
        {                                                

            //fcn.Show();
            fcn.WindowState = FormWindowState.Normal;
            fcn.EditNote(ID);
            

        }
    }
}
