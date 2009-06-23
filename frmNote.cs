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

        //private Point mouse_offset;

        public frmNote(int id, string title, string note, frmNewNote fcn)
        {            
            this.id = id;
            this.title = title;
            this.fcn = fcn;
            this.note = note;
            InitializeComponent();            
            lblTitle.Text = title;
            rtbNote.Text = note;
            DrawDefaultColor();
        }

        private void DrawDefaultColor()
        {
            try
            {
                //String inifile = System.Environment.GetEnvironmentVariable("APPDATA") + "\\.simpleplainnote\\settings.ini";
                //notecolor = Convert.ToInt32(frmSettings.GetIniValue("main", "defaultcolor", inifile));
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error code: 100 - "+exc.Message);             
            }

            paintColorNote();
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

        private void setColorNote(object sender, EventArgs e)
        {
            int i =0;
            foreach (ToolStripMenuItem curitem in menuNoteColors.DropDownItems)
            {                
                if (curitem == sender) 
                {
                    curitem.Checked = true;
                    notecolor = i;
                }
                else
                {
                    curitem.Checked = false;
                }
                i++;
            }

            paintColorNote();
        }

        private void paintColorNote()
        {
            Color normalcolor = getObjColor(false);
            //Color highlightcolor = getObjColor(true);

            this.BackColor = normalcolor;
            this.pnlHead.BackColor = normalcolor;
            this.pnlNote.BackColor = normalcolor;
            this.rtbNote.BackColor = normalcolor;
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
                    if (selected) return Color.Green;
                    else return Color.LightGreen;
                case 4:
                    if (selected) return Color.Blue;
                    else return Color.CornflowerBlue;
                default:
                    return Color.Gold;
            }            
        }

        private void updateMenuNoteColor(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem curitem in menuNoteColors.DropDownItems)
            {                
                curitem.Checked = false;
            }

            switch (this.notecolor)
            {
                case 0:
                    yellowToolStripMenuItem.Checked = true;
                    break;
                case 1:
                    orangeToolStripMenuItem.Checked = true;
                    break;
                case 2:
                    whiteToolStripMenuItem.Checked = true;
                    break;
                case 3:
                    greenToolStripMenuItem.Checked = true;
                    break;
                case 4:
                    blueToolStripMenuItem.Checked = true;
                    break;                
            }
        }

        private void editTToolStripMenuItem_Click(object sender, EventArgs e)
        {                                                            
            fcn.WindowState = FormWindowState.Normal;
            fcn.EditNote(ID);            
        }

        private void pbResizeGrip_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Cursor = Cursors.SizeNWSE;
                this.Size = new Size(this.PointToClient(MousePosition).X, this.PointToClient(MousePosition).Y);                
            }
            this.Cursor = Cursors.Default;
        }
    }
}
