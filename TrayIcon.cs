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
#define win32

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;
using System.Reflection;

[assembly: CLSCompliant(true)]
namespace SimplePlainNote
{
    /// <summary>
    /// Startup class.
    /// </summary>
    public class TrayIcon
    {
        #region Fields (9)

        static System.ComponentModel.IContainer components = null;
        static NotifyIcon icon;
        static ToolStripMenuItem MenuNewNote;
        static ToolStripMenuItem MenuManageNotes;
        static ToolStripMenuItem MenuSettings;
        static ToolStripMenuItem MenuAbout;
        static ToolStripMenuItem MenuExit;
        static ContextMenuStrip MenuTrayIcon;
        static Notes notes;
        static bool transparency = true;

        #endregion Fields

        #region Methods (2)

        // Public Methods (1) 

        public bool getTransparency
        {
            get
            {
                xmlHandler xmlSettings = new xmlHandler(true);
                if (xmlSettings.getXMLnode("transparecy") == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        // Private Methods (1) 

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {            
            components = new System.ComponentModel.Container();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);

            transparency = true;

            bool firstrun = false;

            if (System.Environment.GetCommandLineArgs().Length > 1)
            {
                if (System.Environment.GetCommandLineArgs()[1] == "/disabletransparency")
                {
                    transparency = false;
                }
                else if (System.Environment.GetCommandLineArgs()[1] == "/firstrun")
                {
                    firstrun = true;                    
                }
            }

            //start loading notes.
            notes = new Notes(firstrun);

            icon = new System.Windows.Forms.NotifyIcon(components);
            MenuTrayIcon = new System.Windows.Forms.ContextMenuStrip(components);
            MenuTrayIcon.AllowDrop = false;
            MenuNewNote = new System.Windows.Forms.ToolStripMenuItem();
            MenuManageNotes = new System.Windows.Forms.ToolStripMenuItem();
            MenuSettings = new System.Windows.Forms.ToolStripMenuItem();
            MenuAbout = new System.Windows.Forms.ToolStripMenuItem();
            MenuExit = new System.Windows.Forms.ToolStripMenuItem();

            icon = new NotifyIcon(components);
            icon.ContextMenuStrip = MenuTrayIcon;
            Assembly assembly = Assembly.GetExecutingAssembly();
            //assembly.GetManifestResourceStream("SimplePlainNote.Resources.trayicon.ico");
            icon.Icon = new Icon(assembly.GetManifestResourceStream("SimplePlainNote.Resources.trayicon.ico"));


            icon.Click += new EventHandler(Icon_Click);
            icon.Visible = true;

            icon.ContextMenuStrip.Name = "MenuTrayIcon";
            icon.ContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripMenuItem[] {
            MenuNewNote,
            MenuManageNotes,
            MenuSettings,
            MenuAbout,
            MenuExit});
            icon.ContextMenuStrip.ShowImageMargin = false;
            icon.ContextMenuStrip.Size = new System.Drawing.Size(145, 114);

            // MenuNewNote            
            MenuNewNote.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            MenuNewNote.Name = "MenuNewNote";
            MenuNewNote.Size = new System.Drawing.Size(144, 22);
            MenuNewNote.Text = "&Create a new note";
            MenuNewNote.Click += new System.EventHandler(MenuNewNote_Click);
            // MenuManageNotes            
            MenuManageNotes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            MenuManageNotes.Name = "listToolStripMenuItem";
            MenuManageNotes.Size = new System.Drawing.Size(144, 22);
            MenuManageNotes.Text = "&Manage notes";
            MenuManageNotes.Click += new System.EventHandler(MenuManageNotes_Click);
            // MenuSettings            
            MenuSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            MenuSettings.Name = "MenuSettings";
            MenuSettings.Size = new System.Drawing.Size(144, 22);
            MenuSettings.Text = "Settings";
            MenuSettings.Click += new System.EventHandler(MenuSettings_Click);
            // MenuAbout
            MenuAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            MenuAbout.Name = "MenuAbout";
            MenuAbout.Size = new System.Drawing.Size(144, 22);
            MenuAbout.Text = "About";
            MenuAbout.Click += new System.EventHandler(MenuAbout_Click);
            // MenuExit
            MenuExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            MenuExit.Name = "MenuExit";
            MenuExit.Size = new System.Drawing.Size(144, 22);
            MenuExit.Text = "E&xit";
            MenuExit.Click += new System.EventHandler(MenuExit_Click);

            Application.Run();
        }

        static Int16 getDefaultColor()
        {
            xmlHandler xmlSettings = new xmlHandler(true);
            Int16 color = Convert.ToInt16(xmlSettings.getXMLnodeAsInt("defaultcolor"));
            return color;
        }

        #region menu events
        static void Icon_Click(object sender, EventArgs e)
        {
            //todo: make it configurable, advance option.
        }

        static void MenuNewNote_Click(object sender, EventArgs e)
        {

            frmNewNote newnote = new frmNewNote(notes, getDefaultColor());
            newnote.Show();
        }

        static void MenuManageNotes_Click(object sender, EventArgs e)
        {            
            frmManageNotes managenotes = new frmManageNotes(notes, transparency, getDefaultColor());            
            managenotes.Show();
        }

        static void MenuSettings_Click(object sender, EventArgs e)
        {
            frmSettings settings = new frmSettings(notes, transparency);
            settings.Show();
        }

        static void MenuAbout_Click(object sender, EventArgs e)
        {
            FrmAbout about = new FrmAbout();
            about.Show();
        }

        static void MenuExit_Click(object sender, EventArgs e)
        {
            components.Dispose();
            Application.Exit();
        }
        #endregion

        #endregion Methods
    }
}
