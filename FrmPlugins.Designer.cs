﻿namespace NoteFly
{
    partial class FrmPlugins
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPlugins));
            this.splitContainerAvailablePlugins = new System.Windows.Forms.SplitContainer();
            this.lblTextNoInternetConnection = new System.Windows.Forms.Label();
            this.chlbxAvailiblePlugins = new System.Windows.Forms.CheckedListBox();
            this.lblLicense = new System.Windows.Forms.Label();
            this.lblPluginVersion = new System.Windows.Forms.Label();
            this.lblPluginName = new System.Windows.Forms.Label();
            this.lblPluginDescription = new System.Windows.Forms.Label();
            this.btnPluginDownload = new System.Windows.Forms.Button();
            this.tabControlPlugins = new System.Windows.Forms.TabControl();
            this.tabPagePluginsInstalled = new System.Windows.Forms.TabPage();
            this.tabPagePluginsAvailable = new System.Windows.Forms.TabPage();
            this.pluginGrid = new NoteFly.PluginGrid();
            this.searchtbPlugins = new NoteFly.SearchTextBox();
            this.splitContainerAvailablePlugins.Panel1.SuspendLayout();
            this.splitContainerAvailablePlugins.Panel2.SuspendLayout();
            this.splitContainerAvailablePlugins.SuspendLayout();
            this.tabControlPlugins.SuspendLayout();
            this.tabPagePluginsInstalled.SuspendLayout();
            this.tabPagePluginsAvailable.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerAvailablePlugins
            // 
            this.splitContainerAvailablePlugins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerAvailablePlugins.Location = new System.Drawing.Point(4, 4);
            this.splitContainerAvailablePlugins.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainerAvailablePlugins.Name = "splitContainerAvailablePlugins";
            // 
            // splitContainerAvailablePlugins.Panel1
            // 
            this.splitContainerAvailablePlugins.Panel1.Controls.Add(this.searchtbPlugins);
            this.splitContainerAvailablePlugins.Panel1.Controls.Add(this.lblTextNoInternetConnection);
            this.splitContainerAvailablePlugins.Panel1.Controls.Add(this.chlbxAvailiblePlugins);
            this.splitContainerAvailablePlugins.Panel1MinSize = 0;
            // 
            // splitContainerAvailablePlugins.Panel2
            // 
            this.splitContainerAvailablePlugins.Panel2.Controls.Add(this.lblLicense);
            this.splitContainerAvailablePlugins.Panel2.Controls.Add(this.lblPluginVersion);
            this.splitContainerAvailablePlugins.Panel2.Controls.Add(this.lblPluginName);
            this.splitContainerAvailablePlugins.Panel2.Controls.Add(this.lblPluginDescription);
            this.splitContainerAvailablePlugins.Panel2.Controls.Add(this.btnPluginDownload);
            this.splitContainerAvailablePlugins.Panel2MinSize = 0;
            this.splitContainerAvailablePlugins.Size = new System.Drawing.Size(472, 357);
            this.splitContainerAvailablePlugins.SplitterDistance = 221;
            this.splitContainerAvailablePlugins.SplitterWidth = 5;
            this.splitContainerAvailablePlugins.TabIndex = 0;
            // 
            // lblTextNoInternetConnection
            // 
            this.lblTextNoInternetConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.lblTextNoInternetConnection.Location = new System.Drawing.Point(4, 36);
            this.lblTextNoInternetConnection.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTextNoInternetConnection.Name = "lblTextNoInternetConnection";
            this.lblTextNoInternetConnection.Size = new System.Drawing.Size(213, 207);
            this.lblTextNoInternetConnection.TabIndex = 2;
            this.lblTextNoInternetConnection.Text = "Internet connection required to view available plugins here. \r\n\r\nCheck if you hav" +
                "e a internet connection,\r\n if you do have a stable internet connection, try agai" +
                "n later.";
            this.lblTextNoInternetConnection.Visible = false;
            // 
            // chlbxAvailiblePlugins
            // 
            this.chlbxAvailiblePlugins.Dock = System.Windows.Forms.DockStyle.Top;
            this.chlbxAvailiblePlugins.Enabled = false;
            this.chlbxAvailiblePlugins.FormattingEnabled = true;
            this.chlbxAvailiblePlugins.Items.AddRange(new object[] {
            "loading..."});
            this.chlbxAvailiblePlugins.Location = new System.Drawing.Point(0, 0);
            this.chlbxAvailiblePlugins.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chlbxAvailiblePlugins.Name = "chlbxAvailiblePlugins";
            this.chlbxAvailiblePlugins.Size = new System.Drawing.Size(221, 310);
            this.chlbxAvailiblePlugins.TabIndex = 1;
            this.chlbxAvailiblePlugins.SelectedIndexChanged += new System.EventHandler(this.chlbxAvailiblePlugins_SelectedIndexChanged);
            // 
            // lblLicense
            // 
            this.lblLicense.ForeColor = System.Drawing.Color.Black;
            this.lblLicense.Location = new System.Drawing.Point(12, 79);
            this.lblLicense.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLicense.Name = "lblLicense";
            this.lblLicense.Size = new System.Drawing.Size(219, 27);
            this.lblLicense.TabIndex = 3;
            // 
            // lblPluginVersion
            // 
            this.lblPluginVersion.ForeColor = System.Drawing.Color.DimGray;
            this.lblPluginVersion.Location = new System.Drawing.Point(12, 50);
            this.lblPluginVersion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPluginVersion.Name = "lblPluginVersion";
            this.lblPluginVersion.Size = new System.Drawing.Size(245, 28);
            this.lblPluginVersion.TabIndex = 2;
            // 
            // lblPluginName
            // 
            this.lblPluginName.AutoSize = true;
            this.lblPluginName.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.lblPluginName.Location = new System.Drawing.Point(9, 15);
            this.lblPluginName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPluginName.Name = "lblPluginName";
            this.lblPluginName.Size = new System.Drawing.Size(112, 29);
            this.lblPluginName.TabIndex = 1;
            this.lblPluginName.Text = "Loading..";
            // 
            // lblPluginDescription
            // 
            this.lblPluginDescription.Location = new System.Drawing.Point(11, 106);
            this.lblPluginDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPluginDescription.Name = "lblPluginDescription";
            this.lblPluginDescription.Size = new System.Drawing.Size(220, 186);
            this.lblPluginDescription.TabIndex = 0;
            this.lblPluginDescription.Text = "Getting plugin details from the internet.";
            // 
            // btnPluginDownload
            // 
            this.btnPluginDownload.Location = new System.Drawing.Point(4, 322);
            this.btnPluginDownload.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPluginDownload.Name = "btnPluginDownload";
            this.btnPluginDownload.Size = new System.Drawing.Size(268, 28);
            this.btnPluginDownload.TabIndex = 0;
            this.btnPluginDownload.Text = "download";
            this.btnPluginDownload.UseCompatibleTextRendering = true;
            this.btnPluginDownload.UseVisualStyleBackColor = true;
            this.btnPluginDownload.Visible = false;
            this.btnPluginDownload.Click += new System.EventHandler(this.btnPluginDownload_Click);
            // 
            // tabControlPlugins
            // 
            this.tabControlPlugins.Controls.Add(this.tabPagePluginsInstalled);
            this.tabControlPlugins.Controls.Add(this.tabPagePluginsAvailable);
            this.tabControlPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPlugins.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlPlugins.Location = new System.Drawing.Point(0, 0);
            this.tabControlPlugins.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControlPlugins.Name = "tabControlPlugins";
            this.tabControlPlugins.SelectedIndex = 0;
            this.tabControlPlugins.Size = new System.Drawing.Size(488, 394);
            this.tabControlPlugins.TabIndex = 0;
            this.tabControlPlugins.SelectedIndexChanged += new System.EventHandler(this.tabControlPlugins_SelectedIndexChanged);
            // 
            // tabPagePluginsInstalled
            // 
            this.tabPagePluginsInstalled.Controls.Add(this.pluginGrid);
            this.tabPagePluginsInstalled.Location = new System.Drawing.Point(4, 25);
            this.tabPagePluginsInstalled.Margin = new System.Windows.Forms.Padding(53, 4, 53, 4);
            this.tabPagePluginsInstalled.Name = "tabPagePluginsInstalled";
            this.tabPagePluginsInstalled.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPagePluginsInstalled.Size = new System.Drawing.Size(480, 365);
            this.tabPagePluginsInstalled.TabIndex = 1;
            this.tabPagePluginsInstalled.Text = "Installed";
            this.tabPagePluginsInstalled.UseVisualStyleBackColor = true;
            // 
            // tabPagePluginsAvailable
            // 
            this.tabPagePluginsAvailable.Controls.Add(this.splitContainerAvailablePlugins);
            this.tabPagePluginsAvailable.Location = new System.Drawing.Point(4, 25);
            this.tabPagePluginsAvailable.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPagePluginsAvailable.Name = "tabPagePluginsAvailable";
            this.tabPagePluginsAvailable.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPagePluginsAvailable.Size = new System.Drawing.Size(480, 365);
            this.tabPagePluginsAvailable.TabIndex = 0;
            this.tabPagePluginsAvailable.Text = "available";
            this.tabPagePluginsAvailable.UseVisualStyleBackColor = true;
            // 
            // pluginGrid
            // 
            this.pluginGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pluginGrid.Location = new System.Drawing.Point(4, 4);
            this.pluginGrid.Margin = new System.Windows.Forms.Padding(4);
            this.pluginGrid.Name = "pluginGrid";
            this.pluginGrid.Size = new System.Drawing.Size(472, 357);
            this.pluginGrid.TabIndex = 0;
            // 
            // searchtbPlugins
            // 
            this.searchtbPlugins.Enabled = false;
            this.searchtbPlugins.Location = new System.Drawing.Point(0, 316);
            this.searchtbPlugins.Margin = new System.Windows.Forms.Padding(4);
            this.searchtbPlugins.Name = "searchtbPlugins";
            this.searchtbPlugins.Size = new System.Drawing.Size(217, 34);
            this.searchtbPlugins.TabIndex = 4;
            this.searchtbPlugins.SearchStart += new NoteFly.SearchTextBox.SearchStartHandler(this.searchtbPlugins_SearchStart);
            this.searchtbPlugins.SearchStop += new NoteFly.SearchTextBox.SearchStopHandler(this.searchtbPlugins_SearchStop);
            // 
            // FrmPlugins
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(488, 394);
            this.Controls.Add(this.tabControlPlugins);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPlugins";
            this.Text = "Plugins";
            this.splitContainerAvailablePlugins.Panel1.ResumeLayout(false);
            this.splitContainerAvailablePlugins.Panel2.ResumeLayout(false);
            this.splitContainerAvailablePlugins.Panel2.PerformLayout();
            this.splitContainerAvailablePlugins.ResumeLayout(false);
            this.tabControlPlugins.ResumeLayout(false);
            this.tabPagePluginsInstalled.ResumeLayout(false);
            this.tabPagePluginsAvailable.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlPlugins;
        private System.Windows.Forms.TabPage tabPagePluginsAvailable;
        private System.Windows.Forms.TabPage tabPagePluginsInstalled;
        private PluginGrid pluginGrid;
        private System.Windows.Forms.SplitContainer splitContainerAvailablePlugins;
        private System.Windows.Forms.CheckedListBox chlbxAvailiblePlugins;
        private System.Windows.Forms.Button btnPluginDownload;
        private System.Windows.Forms.Label lblPluginDescription;
        private System.Windows.Forms.Label lblPluginName;
        private System.Windows.Forms.Label lblPluginVersion;
        private System.Windows.Forms.Label lblTextNoInternetConnection;
        private System.Windows.Forms.Label lblLicense;
        private SearchTextBox searchtbPlugins;
    }
}