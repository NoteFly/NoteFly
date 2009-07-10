using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace SimplePlainNote
{
    public partial class frmNote : Form
    {        
        private int id;
        private string title;
        private string note;
        private int notecolor = 0;
        private string twpass;
        private bool transparency = false;
        private bool notelock = false;
        private bool notevisable = true;
        private int locX;
        private int locY;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, 
                         int Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        public Regex syntaxKeywords = new Regex("abstract|as|base|bool|break|byte|case|catch|char|checked|"+
            "class|const|continue|decimal|default|delegate|do|double|else|enum|event|explicit|extern|"+
            "false|finally|fixed|float|for|foreach|goto|if|implicit|in|int|interface|internal|is|lock|"+
            "long|namespace|new|null|object|operator|out|override|params|private|protected|public|"+
            "readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|"+
            "throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|virtual|volatile|void|while|");               

        public frmNote(int id, string title, string note, int notecolor)
        {            
            this.id = id;
            this.title = title;            
            this.note = note;
            this.transparency = getTransparency();
            InitializeComponent();            
            
            lblTitle.Text = title;
            rtbNote.Text = note;

            this.notecolor = notecolor;            
            paintColorNote();
            //SetPosNote();
        }

        #region properties
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
        public bool NoteVisible
        {
            get
            {
                return this.notevisable; 
            }
            set
            {
                notevisable = value;                
            }
        }
        #endregion


        private bool getTransparency()
        {
            xmlHandler xmlSettings = new xmlHandler(true, "settings.xml");
            if (xmlSettings.getXMLnode("transparecy") == "1")
            {
                return true;
            }
            else
            {
                return false;
            }            
        }

        private void frmDeleteNote_Click(object sender, EventArgs e)
        {
            transparency = false;
            this.notevisable = false;
            //fcn.DeleteNote(this.id);            
            this.Close();            
        }

        private void pnlHead_MouseDown(object sender, MouseEventArgs e)
        {
            skin getskin = new skin(notecolor);
            pnlHead.BackColor = getskin.getObjColor(true);

            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                pnlHead.BackColor = getskin.getObjColor(false);
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
                    xmlHandler savenotecolor = new xmlHandler(false, this.id + ".xml");
                    savenotecolor.WriteNote(Convert.ToString(notecolor), this.title, this.note);                    
                }
                else
                {
                    curitem.Checked = false;
                }
                i++;
            }

            paintColorNote();
        }

        /// <summary>
        /// Get the color of the note and paint it.
        /// </summary>
        private void paintColorNote()
        {
            skin getskin = new skin(notecolor);
            Color normalcolor = getskin.getObjColor(false);            

            this.BackColor = normalcolor;
            this.pnlHead.BackColor = normalcolor;
            this.pnlNote.BackColor = normalcolor;
            this.rtbNote.BackColor = normalcolor;
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
                case 5:
                    purpleToolStripMenuItem.Checked = true;                    
                    break;
                case 6:
                    redToolStripMenuItem.Checked = true;                    
                    break;                
            }
        }

        private void editTToolStripMenuItem_Click(object sender, EventArgs e)
        {                                                            
            //fcn.WindowState = FormWindowState.Normal;
            //fcn.EditNote(ID);            
            base.WindowState = FormWindowState.Normal;
            //base.EditNote(ID);
        }

        private void pbResizeGrip_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!notelock)
                {
                    this.Cursor = Cursors.SizeNWSE;
                    this.Size = new Size(this.PointToClient(MousePosition).X, this.PointToClient(MousePosition).Y);
                }
            }
            this.Cursor = Cursors.Default;
        }

        private void TwitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsConnectedToInternet())
            {
                if ((note != "") && (note.Length < 140))
                {
                    tweetnote();
                }
                else if (note.Length >= 140)
                {
                    DialogResult result;
                    result = MessageBox.Show("Your note is more than the 140 chars. Do you want to publish only the first part?", "too long note", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        tweetnote();
                    }
                }
                else
                {
                    MessageBox.Show("Note is empty.");
                }
            }
            else
            {
                MessageBox.Show("No network connection.");
            }
        }

        private void tweetnote()
        {
            xmlHandler xmlSettings = new xmlHandler(false, "settings.xml");
            string twitteruser = xmlSettings.getXMLnode("twitteruser");
            string twitterpass = xmlSettings.getXMLnode("twitterpass");
            if ((twpass != "") && (twpass != null))
            {
                twitterpass = twpass;
            }

            if ((twitteruser == "") || (twitteruser == null))
            {
                MessageBox.Show("Error: Twitter settings not set.");
                return;
            }
            else if ((twitterpass == null) || (twitterpass == ""))
            {
                Form askpass = new Form();
                askpass.Height = 80;
                askpass.Width = 250;
                askpass.Text = "Twitter password needed";
                askpass.Show();
                TextBox tbpass = new TextBox();
                tbpass.Location = new Point(10, 10);
                tbpass.Width = 180;
                tbpass.Name = "tbPassword";
                Button btnOk = new Button();
                btnOk.Location = new Point(190, 10);
                btnOk.Text = "Ok";
                btnOk.Width = 50;
                btnOk.Name = "btnOk";
                btnOk.Click += askpassok;
                askpass.Controls.Add(tbpass);
                askpass.Controls.Add(btnOk);
            }
            else
            {
                if (xmlSettings.UpdateAsXML(twitteruser, twitterpass, note) != null)
                {
                    MessageBox.Show("Your note is Tweeted.");
                }
                else
                {
                    MessageBox.Show("Sending note to twitter failed.");
                }
            }
            
        }

        private void askpassok(object obj, EventArgs e)
        {
            Button btnobj = (Button)obj;
            Form frmAskpass = btnobj.FindForm();

            Control[] passctr = frmAskpass.Controls.Find("tbPassword", true);
            twpass = passctr[0].Text;
            MessageBox.Show(twpass);
            frmAskpass.Close();
            tweetnote();  
        }

        /// <summary>
        /// Check internet state.
        /// </summary>
        /// <returns></returns>
        public static bool IsConnectedToInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }

        private void frmNote_Shown(object sender, EventArgs e)
        {

            xmlHandler xmlSettings = new xmlHandler(false, "settings.xml");

            if (xmlSettings.getXMLnodeAsInt("syntaxhighlight") == 1)
            {
                int selPos = rtbNote.SelectionStart;                
                foreach (Match keyWordMatch in syntaxKeywords.Matches(rtbNote.Text))
                {
                    rtbNote.Select(keyWordMatch.Index, keyWordMatch.Length);
                    rtbNote.SelectionColor = Color.Blue;
                    rtbNote.SelectionStart = selPos;
                    rtbNote.SelectionColor = Color.Black;
                }
            }

            
            if (xmlSettings.getXMLnode("twitteruser") == "")
            {
                TwitterToolStripMenuItem.Enabled = false;
            }
            else
            {
                TwitterToolStripMenuItem.Enabled = true;
            }

        }

        private void OnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OnTopToolStripMenuItem.Checked == true)
            {
                this.TopMost = true;
            }
            else
            {
                this.TopMost = false;
            }
        }

        private void copyTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(note);                
        }

        private void copyTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(title);
        }

        private void locknoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!notelock)
            {
                notelock = true;
                locknoteToolStripMenuItem.Text = "lock note (click again to unlock)";
                this.menuNoteColors.Enabled = false;
                this.editTToolStripMenuItem.Enabled = false;
                this.OnTopToolStripMenuItem.Enabled = false;                
            }
            else
            {
                notelock = false;
                locknoteToolStripMenuItem.Text = "lock note";
                this.menuNoteColors.Enabled = true;
                this.editTToolStripMenuItem.Enabled = true;
                this.OnTopToolStripMenuItem.Enabled = true;
            }
        }

        private void SetPosNote()
        {
            //xmlHandler setNote = new xmlHandler(false, id + ".xml");
            //setNote.WriteNote(notecolor, title, ?);
            throw new NotImplementedException();
        }

    }
}