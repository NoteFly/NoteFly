//-----------------------------------------------------------------------
// <copyright file="PluginGrid.cs" company="GNU">
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
    using System.Windows.Forms;

    public partial class PluginGrid : UserControl
    {
        private Button[] btnPluginsStatus;
        private TableLayoutPanel[] tlpnlPlugins;
        private IPlugin.IPlugin[] allplugins;

        /// <summary>
        /// Creating a new instance of PluginGrid.
        /// </summary>
        public PluginGrid()
        {
            this.DrawAllPluginsDetails();
        }

        public void DrawAllPluginsDetails()
        {
            this.SuspendLayout();
            this.Controls.Clear();
            this.allplugins = Program.GetPlugins(false);
            if (allplugins != null)
            {
                this.btnPluginsStatus = new Button[this.allplugins.Length];
                this.tlpnlPlugins = new TableLayoutPanel[this.allplugins.Length];
                for (int i = 0; i < this.allplugins.Length; i++)
                {
                    DrawPluginDetails(i, this.allplugins[i].Name, this.allplugins[i].Version, this.allplugins[i].Author, this.allplugins[i].Description, this.allplugins[i].Enabled);
                }
            }

            this.ResumeLayout();
        }

        /// <summary>
        /// Draw details of a plugin.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="plugintitle"></param>
        /// <param name="pluginversion"></param>
        /// <param name="pluginauthor"></param>
        /// <param name="plugindescription"></param>
        private void DrawPluginDetails(int pluginpos, string plugintitle, string pluginversion, string pluginauthor, string plugindescription, bool pluginenabled)
        {
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
            this.SetPluginStatusDetail(pluginpos);

            this.tlpnlPlugins[pluginpos].ColumnCount = 3;
            this.tlpnlPlugins[pluginpos].ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3333F));
            this.tlpnlPlugins[pluginpos].ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3333F));
            this.tlpnlPlugins[pluginpos].ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.3333F));
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
            this.tlpnlPlugins[pluginpos].RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpnlPlugins[pluginpos].RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpnlPlugins[pluginpos].RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpnlPlugins[pluginpos].Size = new System.Drawing.Size(340, 98);
            this.tlpnlPlugins[pluginpos].TabIndex = 4;
            // 
            // lblPluginTitle
            // 
            lblPluginTitle.AutoSize = true;
            this.tlpnlPlugins[pluginpos].SetColumnSpan(lblPluginTitle, 2);
            lblPluginTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.00F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblPluginTitle.Location = new System.Drawing.Point(3, 0);
            lblPluginTitle.Name = "lblPluginTitle";
            lblPluginTitle.Size = new System.Drawing.Size(232, 25);
            lblPluginTitle.TabIndex = 1;
            lblPluginTitle.Text = plugintitle;
            // 
            // lblTextPluginVersion
            // 
            lblTextPluginVersion.AutoSize = true;
            lblTextPluginVersion.Location = new System.Drawing.Point(3, 37);
            lblTextPluginVersion.Name = "lblTextPluginVersion";
            lblTextPluginVersion.Size = new System.Drawing.Size(44, 13);
            lblTextPluginVersion.TabIndex = 6;
            lblTextPluginVersion.Text = "version:";
            // 
            // lblPluginVersion
            // 
            lblPluginVersion.AutoSize = true;
            this.tlpnlPlugins[pluginpos].SetColumnSpan(lblPluginVersion, 2);
            lblPluginVersion.Location = new System.Drawing.Point(102, 37);
            lblPluginVersion.Name = "lblPluginVersion";           
            lblPluginVersion.TabIndex = 7;
            lblPluginVersion.Text = pluginversion;
            // 
            // lblTextPluginAuthor
            // 
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
            lblPluginAuthor.Text = pluginauthor;
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
            lblPluginDescription.Text = plugindescription;

            this.btnPluginsStatus[pluginpos].Location = new System.Drawing.Point(230, 20);
            this.btnPluginsStatus[pluginpos].Name = "btnTogglePluginStatus" + pluginpos;
            this.btnPluginsStatus[pluginpos].Tag = pluginpos;
            this.btnPluginsStatus[pluginpos].Size = new System.Drawing.Size(148, 23);
            this.btnPluginsStatus[pluginpos].TabIndex = 0;
            this.btnPluginsStatus[pluginpos].UseVisualStyleBackColor = true;
            this.btnPluginsStatus[pluginpos].Click += new EventHandler(PluginGrid_Click);
            Controls.Add(this.tlpnlPlugins[pluginpos]);
            this.tlpnlPlugins[pluginpos].ResumeLayout(false);
            this.tlpnlPlugins[pluginpos].PerformLayout();
        }

        /// <summary>
        /// Plugin toggle enabled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PluginGrid_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int pluginpos = (int)btn.Tag;
            this.allplugins[pluginpos].Enabled = !this.allplugins[pluginpos].Enabled;
            SetPluginStatusDetail(pluginpos);      
        }

        /// <summary>
        /// Update the plugin status if it enabled or disabled.
        /// </summary>
        /// <param name="pluginpos"></param>
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

                Program.enabledplugins = Program.GetPlugins(true);
            }
        }
    }
}
