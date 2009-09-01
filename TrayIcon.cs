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
            // Create new instance of UnhandledExceptionDlg
            UnhandledExceptionDlg exDlg = new UnhandledExceptionDlg();            
            exDlg.RestartApp = false;
            exDlg.OnShowErrorReport += delegate(object sender, SendExceptionClickEventArgs ar)
            {
                System.Windows.Forms.MessageBox.Show("Handle OnShowErrorReport event to show what you are going to send.\n" +
                    "For example:\n" + ar.UnhandledException.Message + "\n" + ar.UnhandledException.StackTrace +
                    "\n" + (ar.RestartApp ? "This App will be restarted." : "This App will be terminated!"));
            };
            // Implement your sending protocol here. You can use any information from System.Exception
            exDlg.OnSendExceptionClick += delegate(object sender, SendExceptionClickEventArgs ar)
            {
                // User clicked on "Send Error Report" button:
                if (ar.SendExceptionDetails)
                {
                    System.Windows.Forms.MessageBox.Show("paste error on new issue on http://code.google.com/p/simpleplainnote/issues/ Thanks for your effort.");
                    Clipboard.SetText(ar.UnhandledException.StackTrace);
                }
                // User wants to restart the App:
                if (ar.RestartApp)
                {
                    Console.WriteLine("The App will be restarted...");                    
                    System.Diagnostics.Process.Start(System.Windows.Forms.Application.ExecutablePath);
                }
            };
                     
            components = new System.ComponentModel.Container();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);

            transparency = true;

            if (System.Environment.GetCommandLineArgs().Length > 1 && System.Environment.GetCommandLineArgs()[1] == "/disabletransparency")
            {
                transparency = false;
            }
            //start loading notes.
            notes = new Notes();                        

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
            MenuExit} );            
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
        static int getDefaultColor()
        {
            xmlHandler xmlSettings = new xmlHandler(true);
            int color = xmlSettings.getXMLnodeAsInt("defaultcolor");
            return color;
        }

		#endregion Methods 

        #region menu events
        static void Icon_Click(object sender, EventArgs e)
        {
            //todo: make it configurable, advance option.
        }

        static void MenuNewNote_Click(object sender, EventArgs e)
        {

            frmNewNote newnote = new frmNewNote(notes, transparency, getDefaultColor());
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
    }
}
