//-----------------------------------------------------------------------
// <copyright file="FormManager.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2012-2013  Tom
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
namespace NoteFly
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// FormManager class
    /// </summary>
    public sealed class FormManager
    {
        /// <summary>
        /// Reference to notes
        /// </summary>
        private Notes notes;

        /// <summary>
        /// Reference to FrmManageNotes window.
        /// </summary>
        private FrmManageNotes frmmanagenotes;

        /// <summary>
        /// Reference to FrmPlugins window.
        /// </summary>
        private FrmPlugins frmplugins;

        /// <summary>
        /// Reference to FrmSettings window.
        /// </summary>
        private FrmSettings frmsettings;

        /// <summary>
        /// Reference to FrmAbout window.
        /// </summary>
        private FrmAbout frmabout;

        /// <summary>
        /// The newnote window delta X position
        /// </summary>
        private int newnotedeltaX = 0;

        /// <summary>
        /// The newnote window delta Y position
        /// </summary>
        private int newnotedeltaY = 0;

        /// <summary>
        /// Used for warning if new note is still open on shutdown application.
        /// </summary>
        private bool frmneweditnoteopen = false;

        /// <summary>
        /// A boolean indication whether FrmManageNotes datagridview needs to be redrawn.
        /// </summary>
        private bool frmmanagenotesneedupdate = false;

#if windows
        /// <summary>
        /// Reference to KeyboardListener class.
        /// </summary>
        private KeyboardListener keylister;

        /// <summary>
        /// Is control being pressd.
        /// </summary>
        private bool controlpressed = false;

        /// <summary>
        /// Is shift being pressd.
        /// </summary>
        private bool shiftpressed = false;

        /// <summary>
        /// Is alt being pressd.
        /// </summary>
        private bool altpressed = false;
#endif

        /// <summary>
        /// Create a new instance of <see cref=FormManager /> class.
        /// </summary>
        /// <param name="notes">Reference to notes class.</param>
        public FormManager(Notes notes)
        {
            this.notes = notes;

#if windows
            this.keylister = new KeyboardListener();
            this.keylister.s_KeyEventHandler += new EventHandler(this.KeyboardListener_s_KeyEventHandler);
#endif
        }

        /// <summary>
        /// Gets or sets a value indicating whether a FrmNewNote window is open.
        /// </summary>
        public bool Frmneweditnoteopen
        {
            get
            {
                return this.frmneweditnoteopen;
            }

            set
            {
                this.frmneweditnoteopen = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether FrmManageNotes datagridview needs to be redrawn.
        /// </summary>
        public bool FrmManageNotesNeedUpdate
        {
            get
            {
                return this.frmmanagenotesneedupdate;
            }

            set
            {
                this.frmmanagenotesneedupdate = value;
            }
        }

        /// <summary>
        /// Do a refresh on the FrmManageNotes window if it's created.
        /// </summary>
        public void RefreshFrmManageNotes()
        {
            if (this.frmmanagenotes != null)
            {
                this.frmmanagenotes.Resetdatagrid();
                this.frmmanagenotes.Refresh();
            }
        }

        /// <summary>
        /// Create a new note.
        /// </summary>
        /// <param name="contentclipboard">Set content with text from clipboard</param>
        public void OpenNewNote(bool contentclipboard)
        {
            FrmNewNote frmnewnote = new FrmNewNote(this.notes, this.newnotedeltaX, this.newnotedeltaY, contentclipboard);
            this.ChangeDeltaPositionNewNote();
            frmnewnote.Show();
            frmnewnote.Activate();
            this.frmneweditnoteopen = true;
        }

        /// <summary>
        /// Open manage notes window, only allow one instance.
        /// </summary>
        public void OpenFrmManageNotes()
        {
            if (this.AllowCreateForm(this.frmmanagenotes))
            {
                this.frmmanagenotes = new FrmManageNotes(this.notes);
                this.frmmanagenotes.Show();
            }
            else
            {
                this.frmmanagenotes.WindowState = FormWindowState.Normal;
            }

            this.frmmanagenotes.Activate();
        }

        /// <summary>
        /// Open settings window, only allow one instance.
        /// </summary>
        public void OpenFrmSettings()
        {
            if (this.AllowCreateForm(this.frmsettings))
            {
                this.frmsettings = new FrmSettings(this.notes);
                this.frmsettings.Show();
            }
            else
            {
                this.frmsettings.WindowState = FormWindowState.Normal;
            }

            this.frmsettings.Activate();
        }

        /// <summary>
        /// Open plugins window, only allow one instance.
        /// </summary>
        public void OpenFrmPlugins()
        {
            if (this.AllowCreateForm(this.frmplugins))
            {
                this.frmplugins = new FrmPlugins();
                this.frmplugins.Show();
            }
            else
            {
                this.frmplugins.WindowState = FormWindowState.Normal;
            }

            this.frmplugins.Activate();
        }

        /// <summary>
        /// Open about window, only allow one instance.
        /// </summary>
        public void OpenFrmAbout()
        {
            if (this.AllowCreateForm(this.frmabout))
            {
                this.frmabout = new FrmAbout();
                this.frmabout.Show();
            }
            else
            {
                this.frmabout.WindowState = FormWindowState.Normal;
            }

            this.frmabout.Activate();
        }

        /// <summary>
        /// Bring all notes windows to front.
        /// </summary>
        public void BringToFrontNotes()
        {
            this.notes.BringToFrontNotes();
        }

        /// <summary>
        /// Check whether a form can be created.
        /// </summary>
        /// <param name="form">The form to check.</param>
        /// <returns>True if form may be created.</returns>
        private bool AllowCreateForm(Form form)
        {
            if (form == null || form.IsDisposed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

#if windows
        /// <summary>
        /// Handle keys that are pressed to check for system wide shortcut being pressed.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event arguments</param>
        private void KeyboardListener_s_KeyEventHandler(object sender, EventArgs e)
        {
            KeyboardListener.UniversalKeyEventArgs eventArgs = (KeyboardListener.UniversalKeyEventArgs)e;

            // is key down
            if (eventArgs.m_Msg == 256)
            {
                if (eventArgs.KeyData == Keys.ControlKey || this.controlpressed)
                {
                    this.controlpressed = true;
                    if (eventArgs.KeyData == Keys.Alt || eventArgs.KeyData == Keys.Menu || this.altpressed)
                    {
                        this.altpressed = true;

                        // Ctrl + Alt + KEY
                        if (eventArgs.KeyValue == Settings.HotkeysNewNoteKeycode && Settings.HotkeysNewNoteAltInsteadShift && Settings.HotkeysNewNoteEnabled)
                        {
                            this.OpenNewNote(false);
                            this.ResetAllModifierKeys();
                        }
                        else if (eventArgs.KeyValue == Settings.HotkeysManageNotesKeycode && Settings.HotkeysManageNotesAltInsteadShift && Settings.HotkeysManageNotesEnabled)
                        {
                            this.OpenFrmManageNotes();
                            this.ResetAllModifierKeys();
                        }
                        else if (eventArgs.KeyValue == Settings.HotkeysNotesToFrontKeycode && Settings.HotkeysNotesToFrontAltInsteadShift && Settings.HotkeysNotesToFrontEnabled)
                        {
                            this.notes.BringToFrontNotes();
                            this.notes.BringToFrontNotes();
                            this.ResetAllModifierKeys();
                        }
                    }
                    else if (eventArgs.KeyData == Keys.ShiftKey || this.shiftpressed)
                    {
                        this.shiftpressed = true;

                        // Ctrl + Shift + KEY
                        if (eventArgs.KeyValue == Settings.HotkeysNewNoteKeycode && !Settings.HotkeysNewNoteAltInsteadShift && Settings.HotkeysNewNoteEnabled)
                        {
                            this.OpenNewNote(false);
                            this.ResetAllModifierKeys();
                        }
                        else if (eventArgs.KeyValue == Settings.HotkeysManageNotesKeycode && !Settings.HotkeysManageNotesAltInsteadShift && Settings.HotkeysManageNotesEnabled)
                        {
                            this.OpenFrmManageNotes();
                            this.ResetAllModifierKeys();
                        }
                        else if (eventArgs.KeyValue == Settings.HotkeysNotesToFrontKeycode && !Settings.HotkeysNotesToFrontAltInsteadShift && Settings.HotkeysNotesToFrontEnabled)
                        {
                            this.notes.BringToFrontNotes();
                            this.notes.BringToFrontNotes();
                            this.ResetAllModifierKeys();
                        }
                    }
                    else if (eventArgs.KeyData != Keys.ControlKey && eventArgs.KeyData != Keys.ShiftKey && eventArgs.KeyData != Keys.Menu && eventArgs.KeyData != Keys.Alt)
                    {
                        this.ResetAllModifierKeys();
                    }
                }
                else
                {
                    this.ResetAllModifierKeys();
                }
            }
        }

        /// <summary>
        /// Reset all pressed modifier keys.
        /// </summary>
        private void ResetAllModifierKeys()
        {
            this.controlpressed = false;
            this.shiftpressed = false;
            this.altpressed = false;
        }
#endif

        /// <summary>
        /// Change the delta position of FrmNewNote.
        /// </summary>
        private void ChangeDeltaPositionNewNote()
        {
            const int DELTAMAXX = 100;
            const int DELTAMAXY = 100; 
            if (this.newnotedeltaX < DELTAMAXX && this.newnotedeltaY < DELTAMAXY)
            {
                this.newnotedeltaX += 10;
                this.newnotedeltaY += 10;
            }
            else
            {
                this.newnotedeltaX = 0;
                this.newnotedeltaY = 0;
            }
        }
    }
}