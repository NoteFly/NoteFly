//-----------------------------------------------------------------------
// <copyright file="PluginGrid.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2011-2015  Tom
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
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// PluginGrid gui object class 
    /// </summary>
    public sealed partial class PluginGrid : UserControl
    {
        /// <summary>
        /// The minimum width of the control.
        /// </summary>
        private const int MINWITH = 50;

        /// <summary>
        /// Array with all enable/disable buttons for every plugin.
        /// </summary>
        private Button[] btnPluginsStatus;

        /// <summary>
        /// All the tablelayouts panel for every plugin.
        /// </summary>
        private TableLayoutPanel[] tlpnlPlugins;

        /// <summary>
        /// The tooltip.
        /// </summary>
        private ToolTip tooltip;

        /// <summary>
        /// Initializes a new instance of the PluginGrid class.
        /// </summary>
        public PluginGrid()
        {
        }

        /// <summary>
        /// Draw all plugins in the plugingrid.
        /// </summary>
        public void DrawAllPluginsDetails(int width)
        {
            this.SuspendLayout();
            this.Controls.Clear();
            this.InitializeComponent();

            if (PluginsManager.InstalledPlugins != null)
            {
                if (PluginsManager.InstalledPlugins.Length == 0)
                {
                    this.lblTextNopluginsinstalled.Visible = true;
                }
                else
                {
                    this.lblTextNopluginsinstalled.Visible = false;
                }

                this.btnPluginsStatus = new Button[PluginsManager.InstalledPlugins.Length];
                this.tlpnlPlugins = new TableLayoutPanel[PluginsManager.InstalledPlugins.Length];
                width -= 150;
                if (width < MINWITH)
                {
                    width = MINWITH;
                }

                for (int i = 0; i < PluginsManager.InstalledPlugins.Length; i++)
                {
                    this.DrawPluginDetails(i, PluginsManager.InstalledPlugins[i], width);
                }
            }
            else
            {
                this.lblTextNopluginsinstalled.Text = Strings.T("Their are no plugins installed.");
            }

            this.ResumeLayout();
        }

        /// <summary>
        /// Set the tooltips for this control
        /// </summary>
        public void SetControlTooltip(ToolTip tooltip)
        {
            this.tooltip = tooltip;
            if (this.btnPluginsStatus.Length != PluginsManager.InstalledPlugins.Length)
            {
                Log.Write(LogType.exception, "Number of buttons plugins not equal to number of installed buttons, abort SetButtonTooltip");
                return;
            }

            for (int i = 0; i < PluginsManager.InstalledPlugins.Length; i++)
            {
                this.SetButtonTooltip(this.btnPluginsStatus[i], PluginsManager.IsPluginEnabled(PluginsManager.InstalledPlugins[i]));
            }
        }

        /// <summary>
        /// Draw details of a plugin.
        /// </summary>
        /// <param name="pluginpos">The position of the plugin in allplugins array.</param>
        /// <param name="dllfilename">The dll filename of the plugin assemble.</param>
        /// <param name="gridwith">The width of the plugingrid control.</param>
        private void DrawPluginDetails(int pluginpos, string dllfilename, int gridwith)
        {
            System.Reflection.Assembly pluginassembly = null;
            try
            {
                pluginassembly = System.Reflection.Assembly.LoadFrom(Path.Combine(Settings.ProgramPluginsFolder, dllfilename));
            }
            catch (BadImageFormatException badimgformat)
            {
                Log.Write(LogType.exception, badimgformat.Message);
            }

            this.tlpnlPlugins[pluginpos] = new System.Windows.Forms.TableLayoutPanel();
            Label lblPluginTitle = new System.Windows.Forms.Label();
            Label lblTextPluginVersion = new System.Windows.Forms.Label();
            Label lblPluginVersion = new System.Windows.Forms.Label();
            Label lblTextPluginAuthor = new System.Windows.Forms.Label();
            Label lblPluginAuthor = new System.Windows.Forms.Label();
            Label lblTextPluginDescription = new System.Windows.Forms.Label();
            Label lblPluginDescription = new System.Windows.Forms.Label();
            this.btnPluginsStatus[pluginpos] = new Button();
            this.tlpnlPlugins[pluginpos].SuspendLayout();
            this.tlpnlPlugins[pluginpos].Padding = new Padding(0);
            this.tlpnlPlugins[pluginpos].Margin = new Padding(0);
            this.tlpnlPlugins[pluginpos].ColumnCount = 3;
            this.tlpnlPlugins[pluginpos].ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.0000F));
            this.tlpnlPlugins[pluginpos].ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.0000F));
            this.tlpnlPlugins[pluginpos].ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.0000F));
            this.tlpnlPlugins[pluginpos].Controls.Add(lblPluginTitle, 0, 0);
            this.tlpnlPlugins[pluginpos].Controls.Add(this.btnPluginsStatus[pluginpos], 1, 0);
            this.tlpnlPlugins[pluginpos].Controls.Add(lblTextPluginVersion, 0, 1);
            this.tlpnlPlugins[pluginpos].Controls.Add(lblPluginVersion, 1, 1);
            this.tlpnlPlugins[pluginpos].Controls.Add(lblTextPluginAuthor, 0, 2);
            this.tlpnlPlugins[pluginpos].Controls.Add(lblPluginAuthor, 1, 2);
            this.tlpnlPlugins[pluginpos].Controls.Add(lblTextPluginDescription, 0, 3);
            this.tlpnlPlugins[pluginpos].Controls.Add(lblPluginDescription, 1, 3);
            this.tlpnlPlugins[pluginpos].Location = new System.Drawing.Point(3, pluginpos * 100);
            this.tlpnlPlugins[pluginpos].Name = "tlpnlPlugin";
            this.tlpnlPlugins[pluginpos].RowCount = 4;
            this.tlpnlPlugins[pluginpos].RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpnlPlugins[pluginpos].RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tlpnlPlugins[pluginpos].RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tlpnlPlugins[pluginpos].RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tlpnlPlugins[pluginpos].Size = new System.Drawing.Size(gridwith, 99);
            this.tlpnlPlugins[pluginpos].TabIndex = 4;

            // lblPluginTitle
            lblPluginTitle.AutoSize = true;
            this.tlpnlPlugins[pluginpos].SetColumnSpan(lblPluginTitle, 2);
            lblPluginTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.00F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (byte)0);
            lblPluginTitle.Location = new System.Drawing.Point(3, 0);
            lblPluginTitle.Name = "lblPluginTitle";
            lblPluginTitle.Size = new System.Drawing.Size(240, 25);
            lblPluginTitle.TabIndex = 1;
            lblPluginTitle.UseCompatibleTextRendering = true;
            if (pluginassembly != null)
            {
                lblPluginTitle.Text = PluginsManager.GetPluginName(pluginassembly);
            }
            else
            {
                lblPluginTitle.Text = dllfilename;
            }
 
            // lblTextPluginVersion
            lblTextPluginVersion.AutoSize = true;
            lblTextPluginVersion.Location = new System.Drawing.Point(3, 37);
            lblTextPluginVersion.Name = "lblTextPluginVersion";
            lblTextPluginVersion.Size = new System.Drawing.Size(44, 13);
            lblTextPluginVersion.TabIndex = 6;
            lblTextPluginVersion.UseCompatibleTextRendering = true;
            lblTextPluginVersion.Text = Strings.T("version:");

            // lblPluginVersion
            lblPluginVersion.AutoSize = true;
            this.tlpnlPlugins[pluginpos].SetColumnSpan(lblPluginVersion, 2);
            lblPluginVersion.Location = new System.Drawing.Point(102, 37);
            lblPluginVersion.Name = "lblPluginVersion";
            lblPluginVersion.TabIndex = 7;
            lblTextPluginVersion.UseCompatibleTextRendering = true;
            if (pluginassembly != null)
            {
                lblPluginVersion.Text = PluginsManager.GetPluginVersion(pluginassembly);
            }

            if (File.Exists(Path.Combine(Program.GetNewPluginFolder(false), dllfilename)))
            {
                lblPluginVersion.Text += " " + Strings.T("(restart {0} to update)", Program.AssemblyTitle);
            }

            // lblTextPluginAuthor
            lblTextPluginAuthor.AutoSize = true;
            lblTextPluginAuthor.Location = new System.Drawing.Point(3, 53);
            lblTextPluginAuthor.Name = "lblTextPluginAuthor";
            lblTextPluginAuthor.Size = new System.Drawing.Size(40, 13);
            lblTextPluginAuthor.TabIndex = 8;
            lblTextPluginAuthor.UseCompatibleTextRendering = true;
            lblTextPluginAuthor.Text = Strings.T("author:");

            // lblPluginAuthor
            lblPluginAuthor.AutoSize = true;
            this.tlpnlPlugins[pluginpos].SetColumnSpan(lblPluginAuthor, 2);
            lblPluginAuthor.Location = new System.Drawing.Point(102, 53);
            lblPluginAuthor.Name = "lblPluginAuthor";
            lblPluginAuthor.TabIndex = 9;
            lblPluginAuthor.UseCompatibleTextRendering = true;
            if (pluginassembly != null)
            {
                lblPluginAuthor.Text = PluginsManager.GetPluginAuthor(pluginassembly);
            }

            // lblTextPluginDescription
            lblTextPluginDescription.AutoSize = true;
            lblTextPluginDescription.Location = new System.Drawing.Point(3, 70);
            lblTextPluginDescription.Name = "lblTextPluginDescription";
            lblTextPluginDescription.Size = new System.Drawing.Size(68, 13);
            lblTextPluginDescription.TabIndex = 10;
            lblTextPluginDescription.UseCompatibleTextRendering = true;
            lblTextPluginDescription.Text = Strings.T("description:");

            // lblPluginDescription
            lblPluginDescription.AutoSize = true;
            this.tlpnlPlugins[pluginpos].SetColumnSpan(lblPluginDescription, 2);
            lblPluginDescription.Location = new System.Drawing.Point(102, 70);
            lblPluginDescription.Name = "lblPluginDescription";
            lblPluginDescription.TabIndex = 11;
            lblPluginDescription.UseCompatibleTextRendering = true;
            if (pluginassembly != null)
            {
                lblPluginDescription.Text = PluginsManager.GetPluginDescription(pluginassembly);
            }
            else 
            {
                lblPluginDescription.Text = Strings.T("Failed Loading the {0} file.", dllfilename);
            }

            this.btnPluginsStatus[pluginpos].Location = new System.Drawing.Point(230, 20);
            this.btnPluginsStatus[pluginpos].Name = "btnTogglePluginStatus" + pluginpos;
            this.btnPluginsStatus[pluginpos].Tag = dllfilename;
            this.btnPluginsStatus[pluginpos].Size = new System.Drawing.Size(148, 23);
            this.btnPluginsStatus[pluginpos].TabIndex = 0;
            this.btnPluginsStatus[pluginpos].UseVisualStyleBackColor = true;
            this.btnPluginsStatus[pluginpos].Click += new EventHandler(this.PluginGrid_Click);
            Controls.Add(this.tlpnlPlugins[pluginpos]);

            this.SetPluginStatus(pluginpos, dllfilename);
            this.tlpnlPlugins[pluginpos].ResumeLayout(false);
            this.tlpnlPlugins[pluginpos].PerformLayout();
        }

        /// <summary>
        /// Plugin toggle enabled.
        /// </summary>
        /// <param name="sender">Sender object</param>
        /// <param name="e">Event argument</param>
        private void PluginGrid_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string dllfilename = (string)btn.Tag;

            if (PluginsManager.IsPluginEnabled(dllfilename))
            {
                PluginsManager.DisablePlugin(dllfilename);
            }
            else
            {
                PluginsManager.EnablePlugin(dllfilename);
            }

            int pos = this.FindPos(dllfilename);
            if (pos >= 0)
            {
                this.SetPluginStatus(pos, dllfilename);
            }

            PluginsManager.SaveEnabledPlugins();
            Program.RestartTrayicon();
        }

        /// <summary>
        /// Update the plugin status if it enabled or disabled.
        /// </summary>
        /// <param name="pluginpos">The position in tablelayout.</param>
        /// <param name="dllfilename">The dll filename to check if it's enabled.</param>
        private void SetPluginStatus(int pluginpos, string dllfilename)
        {
            bool pluginupdateavailable = File.Exists(Path.Combine(Program.GetNewPluginFolder(false), dllfilename));
            if (PluginsManager.IsPluginEnabled(dllfilename))
            {
                if (pluginupdateavailable)
                {
                    this.tlpnlPlugins[pluginpos].BackColor = System.Drawing.Color.Gold;
                }
                else
                {
                    this.tlpnlPlugins[pluginpos].BackColor = System.Drawing.Color.WhiteSmoke;
                }
                
                this.btnPluginsStatus[pluginpos].Text = Strings.T("disable");
                this.SetButtonTooltip(this.btnPluginsStatus[pluginpos], true);
            }
            else
            {
                if (pluginupdateavailable)
                {
                    this.tlpnlPlugins[pluginpos].BackColor = System.Drawing.Color.SandyBrown;
                }
                else
                {
                    this.tlpnlPlugins[pluginpos].BackColor = System.Drawing.Color.LightGray;
                }
                
                this.btnPluginsStatus[pluginpos].Text = Strings.T("enable");
                this.SetButtonTooltip(this.btnPluginsStatus[pluginpos], false);
            }
        }

        /// <summary>
        /// Set the tooltip of the enable and disable plugin button.
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="pluginenabled"></param>
        private void SetButtonTooltip(Button btn, bool pluginenabled)
        {
            if (Settings.NotesTooltipsEnabled)
            {
                if (this.tooltip == null)
                {
                    return;
                }

                this.tooltip.Active = true;
                if (pluginenabled)
                {
                    this.tooltip.SetToolTip(btn, Strings.T("Click to disable this plugin."));
                }
                else
                {
                    this.tooltip.SetToolTip(btn, Strings.T("Click to enable this plugin."));
                }
            }
            else if (this.tooltip != null)
            {
                this.tooltip.Active = false;
            }
        }

        /// <summary>
        /// Find the position of a plugin dll filename in the tlpnlPlugins
        /// </summary>
        /// <param name="dllfilename">The dll filename</param>
        /// <returns>The position in tlpnlPlugins (counted from the top).</returns>
        private int FindPos(string dllfilename)
        {
            for (int i = 0; i < this.btnPluginsStatus.Length; i++)
            {
                if (Convert.ToString(this.btnPluginsStatus[i].Tag).Equals(dllfilename, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}