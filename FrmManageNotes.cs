﻿//-----------------------------------------------------------------------
// <copyright file="FrmManageNotes.cs" company="GNU">
// 
// This program is free software; you can redistribute it and/or modify it
// Free Software Foundation; either version 2, 
// or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// </copyright>
//-----------------------------------------------------------------------
namespace NoteFly
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    /// <summary>
    /// Manage notes class
    /// </summary>
    public partial class FrmManageNotes : Form
    {
        #region Fields (4)

        /// <summary>
        /// for transparency
        /// </summary>
        private const int HTCAPTION = 0x2;

        /// <summary>
        /// for transparency
        /// </summary>
        private const int WMNCLBUTTONDOWN = 0xA1;

        /// <summary>
        /// list of notes
        /// </summary>
        private Notes notes;

        /// <summary>
        /// flag is redraw is busy
        /// </summary>
        private bool redrawbusy = false;

        /// <summary>
        /// skin colors etc.
        /// </summary>
        private Skin skin;

        /// <summary>
        /// is transparent
        /// </summary>
        private bool transparency = false;

        #endregion Fields

        #region Constructors (1)

        /// <summary>
        /// Initializes a new instance of the FrmManageNotes class.
        /// </summary>
        /// <param name="notes">The class notes, with access to all the notes.</param>
        /// <param name="transparency">Is transparency enabled</param>
        /// <param name="notecolor">The default note color.</param>
        public FrmManageNotes(Notes notes, bool transparency, int notecolor)
        {
            this.InitializeComponent();
            this.skin = new Skin(notecolor);
            this.notes = notes;
            this.transparency = transparency;
            notes.NotesUpdated = false;
            this.DrawNotesOverview();
        }

        #endregion Constructors

        #region Methods (10)

#if win32
        /// <summary>
        /// for moving form 
        /// </summary>
        /// <returns>A boolean if mouse is released from dragging.</returns>
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        /// <summary>
        /// For moving form.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns>unsure.</returns>
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

#endif
        // Private Methods (10) 

        /// <summary>
        /// Close form
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// The user pressed the delete button for a note.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void btnNoteDelete_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            short numbernotes = this.notes.NumNotes;
            if (numbernotes != 0)
            {
                short curnote = 0;
                try
                {
                    curnote = Convert.ToInt16(btn.Name.Substring(10, btn.Name.Length - 10));
                    ////curnote = Convert.ToInt16(btn.Tag);
                }
                catch (InvalidCastException invexc)
                {
                    throw new CustomException(invexc.Message + " " + invexc.StackTrace);
                }

                if (btn.Name == "btnNoteDel" + curnote)
                {
                    int noteid = Convert.ToInt32(curnote) - 1;

                    xmlHandler settings = new xmlHandler(true);
                    if (settings.getXMLnodeAsBool("confirmdelete"))
                    {
                        DialogResult deleteres = MessageBox.Show("Are you sure you want to delete note (ID:" + noteid + ") ?", "delete note?", MessageBoxButtons.YesNo);
                        if (deleteres == DialogResult.No)
                        {
                            return;
                        }
                    }

                    this.notes.GetNotes[noteid].Close();

                    try
                    {
                        File.Delete(Path.Combine(this.GetNotesSavePath(), Convert.ToString(curnote) + ".xml"));
                        Log.Write(LogType.info, Convert.ToString(curnote) + ".xml deleted.");

                        //reorder filenames
                        for (short n = curnote; n < numbernotes; n++)
                        {
                            string orgfile = Path.Combine(this.GetNotesSavePath(), Convert.ToString(n + 1) + ".xml");
                            string newfile = Path.Combine(this.GetNotesSavePath(), Convert.ToString(n) + ".xml");
                            if (!File.Exists(newfile))
                            {
                                File.Move(orgfile, newfile);
                            }

                            if (n < numbernotes)
                            {
                                this.notes.GetNotes[n].NoteID = n;
                            }
                        }

                        this.notes.GetNotes.RemoveAt(noteid);
                    }
                    catch (FileNotFoundException filenotfoundexc)
                    {
                        throw new CustomException(filenotfoundexc.Message);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        string msgaccessdenied = "Access denied. Delete note " + curnote + ".xml manualy with proper premission.";
                        MessageBox.Show(msgaccessdenied);
                        Log.Write(LogType.error, msgaccessdenied);
                    }

                    this.DrawNotesOverview();
                }
                else
                {
                    throw new CustomException("Note to delete not found.");
                }
            }
        }

        /// <summary>
        /// Set a note visible or unvisible
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void cbxNoteVisible_Click(object sender, EventArgs e)
        {
            CheckBox cbx = (CheckBox)sender;
            int n = Convert.ToInt32(cbx.Name) - 1;
            if ((n <= this.notes.NumNotes) && (n >= 0))
            {
                if (this.notes.GetNotes[n].Visible == true)
                {
                    this.notes.GetNotes[n].Hide();
                    this.notes.GetNotes[n].Visible = false;
                }
                else
                {
                    this.notes.GetNotes[n].Show();
                    this.notes.GetNotes[n].Visible = true;
                }

                this.notes.GetNotes[n].UpdateThisNote();
            }
            else
            {
                throw new CustomException("note not found.");
            }
        }

        /// <summary>
        /// Draw a list of all notes.
        /// </summary>
        private void DrawNotesOverview()
        {
            this.pnlNotes.Controls.Clear();
            this.CleanUp();

            int ypos = 10;
            int newlentitle = ((this.Width - 280) / 4);

            for (short curnote = 0; curnote < this.notes.NumNotes; curnote++)
            {
                Label lblNoteTitle = new Label();
                CheckBox cbxNoteVisible = new CheckBox();
                Button btnNoteDelete = new Button();

                int titlelength = this.notes.GetNotes[curnote].NoteTitle.Length;
                lblNoteTitle.AutoSize = true;

                lblNoteTitle.Text = this.ShortenTitle(curnote, newlentitle);

                lblNoteTitle.Name = "lbNote" + Convert.ToString(curnote + 1);
                lblNoteTitle.Location = new Point(2, ypos);
                lblNoteTitle.Anchor = (AnchorStyles.Left | AnchorStyles.Top);

                cbxNoteVisible.Text = "visible";
                cbxNoteVisible.Name = Convert.ToString(curnote + 1);

                if (this.notes.GetNotes[curnote].Visible == true)
                {
                    cbxNoteVisible.CheckState = CheckState.Checked;
                }
                else
                {
                    cbxNoteVisible.CheckState = CheckState.Unchecked;
                }

                cbxNoteVisible.Location = new Point(this.Width - 200, ypos);
                cbxNoteVisible.AutoEllipsis = true;
                cbxNoteVisible.AutoSize = true;
                cbxNoteVisible.Click += new EventHandler(this.cbxNoteVisible_Click);
                cbxNoteVisible.Anchor = (AnchorStyles.Right | AnchorStyles.Top);

                btnNoteDelete.Text = "delete";
                btnNoteDelete.Name = "btnNoteDel" + Convert.ToString(curnote + 1);
                btnNoteDelete.BackColor = Color.Orange;
                btnNoteDelete.Location = new Point(this.Width - 90, ypos - 3);
                btnNoteDelete.Width = 60;
                btnNoteDelete.Click += new EventHandler(this.btnNoteDelete_Click);
                btnNoteDelete.Anchor = (AnchorStyles.Right | AnchorStyles.Top);
                btnNoteDelete.Tag = curnote;

                this.pnlNotes.Controls.Add(lblNoteTitle);
                this.pnlNotes.Controls.Add(cbxNoteVisible);
                this.pnlNotes.Controls.Add(btnNoteDelete);

                ypos += 30;
            }
        }

        /// <summary>
        /// Limit the title.
        /// </summary>
        /// <param name="curnote">The note id.</param>
        /// <param name="newlentitle">The maximum lenght to limit the title to.</param>
        /// <returns>A shorter title.</returns>
        private string ShortenTitle(int curnote, int newlentitle)
        {
            int reallen = this.notes.GetNotes[curnote].NoteTitle.Length;
            if (newlentitle < 4)
            {
                return this.notes.GetNotes[curnote].NoteTitle.Substring(0, 4) + ".. (ID:" + this.notes.GetNotes[curnote].NoteID + ")";
            }
            else if (reallen > newlentitle)
            {
                return this.notes.GetNotes[curnote].NoteTitle.Substring(0, newlentitle) + ".. (ID:" + this.notes.GetNotes[curnote].NoteID + ")";
            }
            else
            {
                return this.notes.GetNotes[curnote].NoteTitle + "(ID:" + this.notes.GetNotes[curnote].NoteID + ")";
            }
        }

        /// <summary>
        /// Dispose all children controls of pnlNotes control.
        /// </summary>
        private void CleanUp()
        {
            int ctrlnum = this.pnlNotes.Controls.Count;
            for (int i = 0; i < ctrlnum; i++)
            {
                this.pnlNotes.Controls[i].Dispose();
            }
        }

        /// <summary>
        /// FrmManageNotes is activated.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void frmManageNotes_Activated(object sender, EventArgs e)
        {
            if (this.transparency && this.skin != null)
            {
                this.Opacity = 1.0;
            }
        }

        /// <summary>
        /// form not active, make tranparent if set.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void frmManageNotes_Deactivate(object sender, EventArgs e)
        {
            if (this.transparency && this.skin != null)
            {
                this.Opacity = this.skin.GetTransparencylevel();
                this.Refresh();
            }
        }

        /// <summary>
        /// Get the full path of the note folder.
        /// </summary>
        /// <returns>The path where to save notes.</returns>
        private string GetNotesSavePath()
        {
            xmlHandler xmlsettings = new xmlHandler(true);
            return xmlsettings.getXMLnode("notesavepath");
        }

        /// <summary>
        /// The manage note form is beening resized.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void pbResizeGrip_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Cursor = Cursors.SizeNWSE;
                this.Size = new Size(this.PointToClient(MousePosition).X, this.PointToClient(MousePosition).Y);
            }

            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Moving note
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void pnlHead_MouseDown(object sender, MouseEventArgs e)
        {
            this.pnlHead.BackColor = Color.OrangeRed;
            if (e.Button == MouseButtons.Left)
            {
#if win32
                ReleaseCapture();
                SendMessage(Handle, WMNCLBUTTONDOWN, HTCAPTION, 0);
#endif
                this.pnlHead.BackColor = Color.Orange;
            }
        }

        /// <summary>
        /// Timer updated the list of notes.
        /// </summary>
        /// <param name="sender">sender object</param>
        /// <param name="e">Event arguments</param>
        private void timerUpdateNotesList_Tick(object sender, EventArgs e)
        {
            if (!this.redrawbusy && this.notes.NotesUpdated)
            {
                this.redrawbusy = true;
                this.DrawNotesOverview();
                this.redrawbusy = false;
                this.notes.NotesUpdated = false;
            }
        }

        /// <summary>
        /// End resizing the window. Now redraw it.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">Event argument</param>
        private void pbResizeGrip_MouseUp(object sender, MouseEventArgs e)
        {
            if (!this.redrawbusy)
            {
                this.redrawbusy = true;
                this.DrawNotesOverview();
                this.redrawbusy = false;
            }
        }

        #endregion Methods
    }
}
