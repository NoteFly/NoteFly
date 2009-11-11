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
		#region Fields (12) 

        static System.ComponentModel.IContainer components = null;
        static bool confirmexitshowed = false;
        static NotifyIcon icon;
        static ToolStripMenuItem MenuAbout;
        static ToolStripMenuItem MenuExit;
        static ToolStripMenuItem MenuManageNotes;
        static ToolStripMenuItem MenuNewNote;
        static ToolStripMenuItem MenuSettings;
        static ContextMenuStrip MenuTrayIcon;
        static bool newnoteshowed = false;
        static Notes notes;
        //static bool settingshowed = false;
        static bool transparency = true;

		#endregion Fields 

		#region Properties (1) 

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

		#endregion Properties 

		#region Methods (3) 

		// Private Methods (3) 

        /// <summary>
        /// get actionleftclick setting
        /// </summary>
        /// <returns></returns>
        static Int16 getActionLeftClick()
        {
            xmlHandler getSettings = new xmlHandler(true);
            return Convert.ToInt16(getSettings.getXMLnodeAsInt("actionleftclick"));
        }

        /// <summary>
        /// get defaultcolor setting
        /// </summary>
        /// <returns></returns>
        static Int16 getDefaultColor()
        {
            xmlHandler xmlSettings = new xmlHandler(true);
            Int16 color = Convert.ToInt16(xmlSettings.getXMLnodeAsInt("defaultcolor"));
            
            if (color == 7)
            {
                color = Convert.ToInt16(new Random().Next(0, 7));
            }
            return color;
        }

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
                if (System.Environment.GetCommandLineArgs()[1] == "/firstrun")
                {
                    firstrun = true;
                }
                //disabletransparency parameter is for OS that don't support transparency, so they still can launch this programme.
                else if (System.Environment.GetCommandLineArgs()[1] == "/disabletransparency")
                {
                    transparency = false;
                }
            }

            //start loading notes.
            notes = new Notes(firstrun);

            //start building icon and icon contextmenu
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
            icon.Icon = new Icon(assembly.GetManifestResourceStream("SimplePlainNote.Resources.trayicon.ico"));

            icon.MouseClick += new MouseEventHandler(Icon_Click);
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
		#endregion Methods 

        #region menu events
        /// <summary>
        /// There is left clicked on the icon.
        /// If actionleftclick is 0 do nothing.
        /// If actionleftclick is 1 actived all notes.
        /// If actionleftclick is 2 create a new note.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Icon_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Int16 actionleftclick = getActionLeftClick();
                if (actionleftclick == 1)
                {
                    for (int i = 0; i < notes.NumNotes; i++)
                    {
                        notes.GetNotes[i].Activate();
                    }
                }
                else if (actionleftclick == 2)
                {
                    if (!newnoteshowed)
                    {
                        FrmNewNote newnote = new FrmNewNote(notes, getDefaultColor());
                        newnote.Show();
                        newnoteshowed = true;
                    }
                    else
                    {
                        newnoteshowed = false;
                    }
                }
            }
        }

        /// <summary>
        /// open new note window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void MenuNewNote_Click(object sender, EventArgs e)
        {
            FrmNewNote newnote = new FrmNewNote(notes, getDefaultColor());
            newnote.Show();
        }

        /// <summary>
        /// open manage notes window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void MenuManageNotes_Click(object sender, EventArgs e)
        {
            FrmManageNotes managenotes = new FrmManageNotes(notes, transparency, getDefaultColor());
            managenotes.Show();
        }

        /// <summary>
        /// open settings window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void MenuSettings_Click(object sender, EventArgs e)
        {
            FrmSettings settings = new FrmSettings(notes, transparency);
            settings.Show();                                             
        }

        /// <summary>
        /// Open about window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void MenuAbout_Click(object sender, EventArgs e)
        {
            FrmAbout about = new FrmAbout();
            about.Show();
        }

        /// <summary>
        /// User request to shutdown application.
        /// Check if confirm box is needed. 
        /// if confirm box is still open then shutdown anyway.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void MenuExit_Click(object sender, EventArgs e)
        {
            xmlHandler getSetting = new xmlHandler(true);
            if (getSetting.getXMLnodeAsBool("confirmexit"))
            {
                if (!confirmexitshowed)
                {
                    confirmexitshowed = true;
                    string AssemblyProduct;
                    object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                    if (attributes.Length == 0)
                    {
                        AssemblyProduct = "";
                    }
                    AssemblyProduct = ((AssemblyProductAttribute)attributes[0]).Product;

                    DialogResult resultdialogconfirm = MessageBox.Show("Are sure you want to exit " + AssemblyProduct + "?", "confirm exit", MessageBoxButtons.YesNo);

                    if (resultdialogconfirm == DialogResult.Yes)
                    {
                        ExitApplication();
                    }
                }                
                else
                {
                    ExitApplication();
                }
            }
            else
            {
                ExitApplication();
            }
        }

        /// <summary>
        /// Terminate application
        /// </summary>
        static void ExitApplication()
        {
            components.Dispose();            
            Application.Exit();
        }

        #endregion
    }
}
