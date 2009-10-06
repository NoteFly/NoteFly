/* Copyright (C) 2009
 * 
 * This program is free software; you can redistribute it and/or modify it
 * Free Software Foundation; either version 2, or (at your option) any
 * later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml;

namespace SimplePlainNote
{
    /// <summary>
    /// Class to create new note.
    /// </summary>
    public partial class FrmNewNote : Form
    {
		#region Fields (5) 

        private bool editnote = false;
        private int editnoteid = -1;
        private Int16 notecolor;
        private Notes notes;
        private Skin skin;
        private TextHighlight highlight;

		#endregion Fields 

		#region Constructors (2) 

        public FrmNewNote(Notes notes, Int16 notecolor, int editnoteid, string editnotetitle, string editnotecontent)
        {
            InitializeComponent();
            this.editnote = true;
            this.notecolor = notecolor;
            this.skin = new Skin(notecolor);
            this.editnoteid = editnoteid;
            this.notes = notes;            
            ResetNewNoteForm(editnotetitle, editnotecontent);
            this.tbTitle.Focus();
            this.tbTitle.Select();
            if (editnote)
            {
                this.Text = "edit note";
            }
        }

        public FrmNewNote(Notes notes, Int16 notecolor)
        {
            InitializeComponent();
            this.editnote = false;
            this.notes = notes;
            this.notecolor = notecolor;
            this.skin = new Skin(notecolor);
            ResetNewNoteForm("", "");           
            this.tbTitle.Focus();
            this.tbTitle.Select();            
        }

		#endregion Constructors 

		#region Methods (18) 

		// Private Methods (18) 

        private void btnAddNote_Click(object sender, EventArgs e)
        {            
            if (String.IsNullOrEmpty(tbTitle.Text))
            {
                tbTitle.BackColor = skin.getObjColor(false, false, true);
                tbTitle.Text = DateTime.Now.ToString();
            }
            else if (String.IsNullOrEmpty(rtbNote.Text))
            {
                rtbNote.BackColor = skin.getObjColor(false, false, true);
                rtbNote.Text = "Please type any note content, like this for example.";
            }
            else
            {
                if (editnote)
                {
                    notes.UpdateNote(editnoteid, this.tbTitle.Text, this.rtbNote.Text, true);
                }
                else
                {  
                    notes.DrawNewNote(tbTitle.Text, rtbNote.Text, notecolor);                    
                }
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            int posnotelst = editnoteid - 1;
            if ((this.editnote) && (posnotelst >= 0) && (posnotelst < notes.NumNotes))
            {                
                notes.GetNotes[posnotelst].Show();                                
            }
            this.Close();
        }

        /// <summary>
        /// syntax highlighting
        /// </summary>
        private void checksyntax(object sender, EventArgs e)
        {
            if ((notes.HighlightHTML == true) || (notes.HighlightC == true))
            {
                if (highlight == null)
                {
                    highlight = new TextHighlight(notes.HighlightHTML, notes.HighlightC, this.rtbNote);                    
                }
                else if ((highlight != null) && (!String.IsNullOrEmpty(rtbNote.Text)))
                {
                    int curselstart = rtbNote.SelectionStart;
                    if (curselstart >= rtbNote.Text.Length - 1)
                    {
                        highlight.CheckSyntaxQuick();
                    }
                    else
                    {
                        highlight.CheckSyntaxFull();
                    }
                }
            }
            
        }

        private void copyTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(rtbNote.Text);
        }

        private void copyTextToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                pastTextToolStripMenuItem.Enabled = true;
            }
            else
            {
                pastTextToolStripMenuItem.Enabled = false;
            }
        }

        private void frmNewNote_Activated(object sender, EventArgs e)
        {
            if (notes.Transparency)
            {
                this.Opacity = 1.0;                
            }
        }

        private void frmNewNote_Deactivate(object sender, EventArgs e)
        {
            if ((notes.Transparency) && (skin != null))
            {
                this.Opacity = skin.getTransparencylevel();
                this.Refresh();
            }
        }

        private void pastTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                rtbNote.Text = rtbNote.Text + Clipboard.GetText();
            }
            else
            {
                MessageBox.Show("clipboard is empty/no text in it.");
            }
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

        private void pnlHeadNewNote_MouseDown(object sender, MouseEventArgs e)
        {
            if (skin != null)
            {
                pnlHeadNewNote.BackColor = skin.getObjColor(true);
                #if win32
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();                    
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                    pnlHeadNewNote.BackColor = skin.getObjColor(false);
                }
                #endif
            }
        }

        private void pnlNoteEdit_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStripTextActions.Show(this.Location.X + e.X, this.Location.X + e.Y);
            }
        }

        /// <summary>
        /// Redraw reset FrmNewNote.
        /// </summary>
        private void ResetNewNoteForm(string title, string content)
        {
            if (skin == null) return;

            rtbNote.Font = skin.getFontNoteContent();

            Color normalcolor = skin.getObjColor(false);            
            pnlNoteEdit.BackColor = normalcolor;
            rtbNote.BackColor = normalcolor;
            pnlHeadNewNote.BackColor = normalcolor;

            if (notes.TextDirection == 0)
            {
                this.tbTitle.TextAlign = HorizontalAlignment.Left;
                this.rtbNote.SelectionAlignment = HorizontalAlignment.Left;
            }
            else if (notes.TextDirection == 1)
            {
                this.tbTitle.TextAlign = HorizontalAlignment.Right;
                this.rtbNote.SelectionAlignment = HorizontalAlignment.Right;
            }

            pnlNoteEdit.Refresh();
            rtbNote.Refresh();
            pnlHeadNewNote.Refresh();

            tbTitle.Text = title;
            rtbNote.Text = content;
        }

        private void rtbNote_KeyDown(object sender, KeyEventArgs e)
        {

        }

        /// <summary>
        /// A hyperlink is clicked, check settings to see if confirm launch dialog have
        /// to be showed, if not then directly launch the URL.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rtbNote_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            xmlHandler getSettings = new xmlHandler(true);
            if (getSettings.getXMLnodeAsBool("askurl"))
            {
                DialogResult result = MessageBox.Show(this, "Are you sure you want to visted: " + e.LinkText, "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(e.LinkText);
                }
            }
            else
            {
                System.Diagnostics.Process.Start(e.LinkText);
            }
        }

        /// <summary>
        /// Force context menu to show up.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rtbNote_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {                
                contextMenuStripTextActions.Show(this.Location.X + e.X, this.Location.X + e.Y);
            }            
        }

        private void setTextDirection()
        {
            if (notes.TextDirection == 0)
            {
                tbTitle.TextAlign = HorizontalAlignment.Left;                
            }
            else if (notes.TextDirection == 1)
            {
                tbTitle.TextAlign = HorizontalAlignment.Right;                
            }
        }
        /*
        /// <summary>
        /// Move to rtbNote
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbTitle_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Tab))
            {
                this.rtbNote.Focus();
                this.rtbNote.Select();             
            }                        
        }
         */

		#endregion Methods 

        #if win32
        public const int HT_CAPTION = 0x2;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
        int Msg, int wParam, int lParam);
        #endif


        #region highlight controls
        private void tbTitle_Enter(object sender, EventArgs e)
        {
            if (skin != null)
            {
                tbTitle.BackColor = skin.getObjColor(false, true, false);
            }
        }
        private void tbTitle_Leave(object sender, EventArgs e)
        {
            if (skin != null)
            {
                tbTitle.BackColor = skin.getObjColor(false);
            }
        }
        private void rtbNote_Enter(object sender, EventArgs e)
        {
            if (skin != null)
            {
                rtbNote.BackColor = skin.getObjColor(false, true, false);
            }
        }

        private void rtbNote_Leave(object sender, EventArgs e)
        {
            if (skin != null)
            {
                rtbNote.BackColor = skin.getObjColor(false);
            }
        }
        #endregion
    } 
}
