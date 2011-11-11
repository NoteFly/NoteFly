//-----------------------------------------------------------------------
// <copyright file="PluginGrid.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2011  Tom
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
        /// Array with all enable/disable buttons for every plugin.
        /// </summary>
        private Button[] btnPluginsStatus;

        /// <summary>
        /// All the tablelayouts panel for every plugin.
        /// </summary>
        private TableLayoutPanel[] tlpnlPlugins;

        /// <summary>
        /// All the plugin
        /// </summary>
        private IPlugin.IPlugin[] allplugins;

        /// <summary>
        /// Label lblTextNoplugins
        /// </summary>
        private Label lblTextNoplugins;

        /// <summary>
        /// Initializes a new instance of the PluginGrid class.
        /// </summary>
        public PluginGrid()
        {
            const int DEFAULTWITH = 415;
            this.DrawAllPluginsDetails(DEFAULTWITH);
        }

        /// <summary>
        /// Draw all plugins in the plugingrid.
        /// </summary>
        public void DrawAllPluginsDetails(int gridwidth)
        {
            this.SuspendLayout();
            this.Controls.Clear();
            this.allplugins = Program.GetPlugins(false);
            if (this.allplugins != null)
            {
                if (this.allplugins.Length == 0)
                {
                    this.lblTextNoplugins = new Label();
                    this.lblTextNoplugins.Text = "Their are no plugins installed.";
                    this.lblTextNoplugins.SetBounds(15, 10, 200, 40);
                    this.lblTextNoplugins.AutoSize = true;
                    this.lblTextNoplugins.Visible = true;
                    this.Controls.Add(this.lblTextNoplugins);
                }
                else if (this.lblTextNoplugins != null)
                {
                    this.lblTextNoplugins.Visible = false;
                    this.Controls.Remove(this.lblTextNoplugins);
                }

                this.btnPluginsStatus = new Button[this.allplugins.Length];
                this.tlpnlPlugins = new TableLayoutPanel[this.allplugins.Length];
                for (int i = 0; i < this.allplugins.Length; i++)
                {
                    this.DrawPluginDetails(i, this.allplugins[i].Enabled, this.allplugins[i].Filename, gridwidth);
                }
            }

            this.ResumeLayout();
        }

        /// <summary>
        /// Save the enabled plugin settings.
        /// </summary>
        public void SavePluginSettings()
        {
            bool first = true;
            Settings.ProgramPluginsEnabled = string.Empty;
            if (this.allplugins != null)
            {
                for (int i = 0; i < this.allplugins.Length; i++)
                {
                    if (this.allplugins[i].Enabled)
                    {
                        if (first)
                        {
                            first = false;
                        }
                        else
                        {
                            Settings.ProgramPluginsEnabled += "|";
                        }

                        Settings.ProgramPluginsEnabled += this.allplugins[i].Filename;
                    }
                }

                Program.pluginsenabled = Program.GetPlugins(true);
            }
        }

        /// <summary>
        /// Draw details of a plugin.
        /// </summary>
        /// <param name="pluginpos">The positio of the plugin in allplugins array</param>
        /// <param name="pluginenabled">Is the plugin enabled</param>
        /// <param name="filename">The filename of the plugin assebly</param>
        private void DrawPluginDetails(int pluginpos, bool pluginenabled, string filename, int gridwith)
        {            
            System.Reflection.Assembly pluginassembly = System.Reflection.Assembly.LoadFrom(Path.Combine(Settings.ProgramPluginsFolder, filename));
            if (pluginassembly == null)
            {
                return;
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
            this.tlpnlPlugins[pluginpos].ColumnCount = 3;
            this.tlpnlPlugins[pluginpos].ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0000F));
            this.tlpnlPlugins[pluginpos].ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0000F));
            this.tlpnlPlugins[pluginpos].ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0000F));
            this.tlpnlPlugins[pluginpos].Controls.Add(lblPluginTitle, 0, 0);
            this.tlpnlPlugins[pluginpos].Controls.Add(this.btnPluginsStatus[pluginpos], 1, 0);
            this.tlpnlPlugins[pluginpos].Controls.Add(lblTextPluginVersion, 0, 1);
            this.tlpnlPlugins[pluginpos].Controls.Add(lblPluginVersion, 1, 1);
            this.tlpnlPlugins[pluginpos].Controls.Add(lblTextPluginAuthor, 0, 2);
            this.tlpnlPlugins[pluginpos].Controls.Add(lblPluginAuthor, 1, 2);
            this.tlpnlPlugins[pluginpos].Controls.Add(lblTextPluginDescription, 0, 3);
            this.tlpnlPlugins[pluginpos].Controls.Add(lblPluginDescription, 1, 3);
            this.tlpnlPlugins[pluginpos].Location = new System.Drawing.Point(3, (pluginpos * 100));
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
            lblPluginTitle.Size = new System.Drawing.Size(232, 25);
            lblPluginTitle.TabIndex = 1;
            lblPluginTitle.Text = Program.GetPluginName(pluginassembly);
 
            // lblTextPluginVersion
            lblTextPluginVersion.AutoSize = true;
            lblTextPluginVersion.Location = new System.Drawing.Point(3, 37);
            lblTextPluginVersion.Name = "lblTextPluginVersion";
            lblTextPluginVersion.Size = new System.Drawing.Size(44, 13);
            lblTextPluginVersion.TabIndex = 6;
            lblTextPluginVersion.Text = "version:";

            // lblPluginVersion
            lblPluginVersion.AutoSize = true;
            this.tlpnlPlugins[pluginpos].SetColumnSpan(lblPluginVersion, 2);
            lblPluginVersion.Location = new System.Drawing.Point(102, 37);
            lblPluginVersion.Name = "lblPluginVersion";
            lblPluginVersion.TabIndex = 7;
            lblPluginVersion.Text = Program.GetPluginVersion(pluginassembly);
            
            // lblTextPluginAuthor
            lblTextPluginAuthor.AutoSize = true;
            lblTextPluginAuthor.Location = new System.Drawing.Point(3, 53);
            lblTextPluginAuthor.Name = "lblTextPluginAuthor";
            lblTextPluginAuthor.Size = new System.Drawing.Size(40, 13);
            lblTextPluginAuthor.TabIndex = 8;
            lblTextPluginAuthor.Text = "author:";

            // lblPluginAuthor
            lblPluginAuthor.AutoSize = true;
            this.tlpnlPlugins[pluginpos].SetColumnSpan(lblPluginAuthor, 2);
            lblPluginAuthor.Location = new System.Drawing.Point(102, 53);
            lblPluginAuthor.Name = "lblPluginAuthor";
            lblPluginAuthor.TabIndex = 9;
            lblPluginAuthor.Text = Program.GetPluginAuthor(pluginassembly);

            // lblTextPluginDescription
            lblTextPluginDescription.AutoSize = true;
            lblTextPluginDescription.Location = new System.Drawing.Point(3, 70);
            lblTextPluginDescription.Name = "lblTextPluginDescription";
            lblTextPluginDescription.Size = new System.Drawing.Size(61, 13);
            lblTextPluginDescription.TabIndex = 10;
            lblTextPluginDescription.Text = "description:";

            // lblPluginDescription
            lblPluginDescription.AutoSize = true;
            this.tlpnlPlugins[pluginpos].SetColumnSpan(lblPluginDescription, 2);
            lblPluginDescription.Location = new System.Drawing.Point(102, 70);
            lblPluginDescription.Name = "lblPluginDescription";
            lblPluginDescription.TabIndex = 11;
            lblPluginDescription.Text = Program.GetPluginDescription(pluginassembly);

            this.btnPluginsStatus[pluginpos].Location = new System.Drawing.Point(230, 20);
            this.btnPluginsStatus[pluginpos].Name = "btnTogglePluginStatus" + pluginpos;
            this.btnPluginsStatus[pluginpos].Tag = pluginpos;
            this.btnPluginsStatus[pluginpos].Size = new System.Drawing.Size(148, 23);
            this.btnPluginsStatus[pluginpos].TabIndex = 0;
            this.btnPluginsStatus[pluginpos].UseVisualStyleBackColor = true;
            this.btnPluginsStatus[pluginpos].Click += new EventHandler(this.PluginGrid_Click);
            Controls.Add(this.tlpnlPlugins[pluginpos]);

            this.SetPluginStatusDetail(pluginpos);
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
            int pluginpos = (int)btn.Tag;
            this.allplugins[pluginpos].Enabled = !this.allplugins[pluginpos].Enabled;
            this.SetPluginStatusDetail(pluginpos);      
        }

        /// <summary>
        /// Update the plugin status if it enabled or disabled.
        /// </summary>
        /// <param name="pluginpos">The position in the array of allplugins</param>
        private void SetPluginStatusDetail(int pluginpos)
        {
            if (this.allplugins[pluginpos].Enabled)
            {
                this.tlpnlPlugins[pluginpos].BackColor = System.Drawing.Color.WhiteSmoke;
                this.btnPluginsStatus[pluginpos].Text = "disable";                
            }
            else
            {
                this.tlpnlPlugins[pluginpos].BackColor = System.Drawing.Color.LightGray;
                this.btnPluginsStatus[pluginpos].Text = "enable";
            }
        }
    }
}