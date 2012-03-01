namespace NoteFly
{
    using System;
    using System.Text;
    using System.Windows.Forms;

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
        /// 
        /// </summary>
        private int newnotedeltaX = 0;

        /// <summary>
        /// 
        /// </summary>
        private int newnotedeltaY = 0;

        /// <summary>
        /// Is the creation of a new note being showed, so double left clicking isnt creating two notes at once.
        /// </summary>
        //private bool frmnewnoteshowed = false;

        /// <summary>
        /// Used for warning if new note is still open on shutdown application.
        /// </summary>
        private bool frmneweditnoteopen = false;

        /// <summary>
        /// boolean indication whether FrmManageNotes datagridview needs to be redrawn.
        /// </summary>
        private bool frmmanagenotesneedupdate = false;

#if windows
        /// <summary>
        /// 
        /// </summary>
        private KeyboardListener keylister;

        /// <summary>
        /// 
        /// </summary>
        private bool controlpressed = false;

        /// <summary>
        /// 
        /// </summary>
        private bool shiftpressed = false;

        /// <summary>
        /// 
        /// </summary>
        private bool altpressed = false;
#endif

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notes"></param>
        public FormManager(Notes notes)
        {
            this.notes = notes;

#if windows
            this.keylister = new KeyboardListener();
            this.keylister.s_KeyEventHandler += new EventHandler(this.KeyboardListener_s_KeyEventHandler);
#endif
        }

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
        /// Gets or sets a value indicating whetherr FrmManageNotes datagridview needs to be redrawn.
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
        public void OpenNewNote()
        {
            FrmNewNote frmnewnote = new FrmNewNote(this.notes, this.newnotedeltaX, this.newnotedeltaY);
            this.ChangeDeltaPositionNewNote();
            frmnewnote.Show();
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
                this.frmmanagenotes.Activate();
            }
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
                this.frmsettings.Activate();
            }
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
                this.frmplugins.Activate();
            }
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
                this.frmabout.Activate();
            }
        }

        public void BringToFrontNotes()
        {
            this.notes.BringToFrontNotes();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="form"></param>
        private bool AllowCreateForm(Form form)
        {
            if ((form == null) || (form.IsDisposed))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

#if windows
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
                        if (eventArgs.KeyValue == Settings.HotkeysNewNoteKeycode && Settings.HotkeysNewNoteAltInsteadShift)
                        {
                            this.OpenNewNote();
                            this.ResetAllModifierKeys();
                        }
                        else if (eventArgs.KeyValue == Settings.HotkeysManageNotesKeycode && Settings.HotkeysManageNotesAltInsteadShift)
                        {
                            this.OpenFrmManageNotes();
                            this.ResetAllModifierKeys();
                        }
                        else if (eventArgs.KeyValue == Settings.HotkeysNotesToFrontKeycode && Settings.HotkeysNotesToFrontAltInsteadShift)
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
                        if (eventArgs.KeyValue == Settings.HotkeysNewNoteKeycode && !Settings.HotkeysNewNoteAltInsteadShift)
                        {
                            this.OpenNewNote();
                            this.ResetAllModifierKeys();
                        }
                        else if (eventArgs.KeyValue == Settings.HotkeysManageNotesKeycode && !Settings.HotkeysManageNotesAltInsteadShift)
                        {
                            this.OpenFrmManageNotes();
                            this.ResetAllModifierKeys();
                        }
                        else if (eventArgs.KeyValue == Settings.HotkeysNotesToFrontKeycode && !Settings.HotkeysNotesToFrontAltInsteadShift)
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
        /// 
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
            if (this.newnotedeltaX < 100 && this.newnotedeltaY < 100)
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
