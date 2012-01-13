namespace NoteFly
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
            this.tabControlPlugins = new System.Windows.Forms.TabControl();
            this.tabPagePluginsInstalled = new System.Windows.Forms.TabPage();
            this.tabPagePluginsAvailable = new System.Windows.Forms.TabPage();
            this.splitContainerAvailablePlugins = new System.Windows.Forms.SplitContainer();
            this.lblTextNoInternetConnection = new System.Windows.Forms.Label();
            this.chlbxAvailiblePlugins = new System.Windows.Forms.CheckedListBox();
            this.lblLicense = new System.Windows.Forms.Label();
            this.lblPluginVersion = new System.Windows.Forms.Label();
            this.lblPluginName = new System.Windows.Forms.Label();
            this.lblPluginDescription = new System.Windows.Forms.Label();
            this.btnPluginDownload = new System.Windows.Forms.Button();
            this.pluginGrid = new NoteFly.PluginGrid();
            this.searchtbPlugins = new NoteFly.SearchTextBox();
            this.tabControlPlugins.SuspendLayout();
            this.tabPagePluginsInstalled.SuspendLayout();
            this.tabPagePluginsAvailable.SuspendLayout();
            this.splitContainerAvailablePlugins.Panel1.SuspendLayout();
            this.splitContainerAvailablePlugins.Panel2.SuspendLayout();
            this.splitContainerAvailablePlugins.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlPlugins
            // 
            this.tabControlPlugins.Controls.Add(this.tabPagePluginsInstalled);
            this.tabControlPlugins.Controls.Add(this.tabPagePluginsAvailable);
            this.tabControlPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPlugins.Location = new System.Drawing.Point(0, 0);
            this.tabControlPlugins.Name = "tabControlPlugins";
            this.tabControlPlugins.SelectedIndex = 0;
            this.tabControlPlugins.Size = new System.Drawing.Size(424, 320);
            this.tabControlPlugins.TabIndex = 0;
            this.tabControlPlugins.SelectedIndexChanged += new System.EventHandler(this.tabControlPlugins_SelectedIndexChanged);
            // 
            // tabPagePluginsInstalled
            // 
            this.tabPagePluginsInstalled.Controls.Add(this.pluginGrid);
            this.tabPagePluginsInstalled.Location = new System.Drawing.Point(4, 22);
            this.tabPagePluginsInstalled.Margin = new System.Windows.Forms.Padding(40, 3, 40, 3);
            this.tabPagePluginsInstalled.Name = "tabPagePluginsInstalled";
            this.tabPagePluginsInstalled.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePluginsInstalled.Size = new System.Drawing.Size(416, 294);
            this.tabPagePluginsInstalled.TabIndex = 1;
            this.tabPagePluginsInstalled.Text = "Installed";
            this.tabPagePluginsInstalled.UseVisualStyleBackColor = true;
            // 
            // tabPagePluginsAvailable
            // 
            this.tabPagePluginsAvailable.Controls.Add(this.splitContainerAvailablePlugins);
            this.tabPagePluginsAvailable.Location = new System.Drawing.Point(4, 22);
            this.tabPagePluginsAvailable.Name = "tabPagePluginsAvailable";
            this.tabPagePluginsAvailable.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePluginsAvailable.Size = new System.Drawing.Size(416, 294);
            this.tabPagePluginsAvailable.TabIndex = 0;
            this.tabPagePluginsAvailable.Text = "available";
            this.tabPagePluginsAvailable.UseVisualStyleBackColor = true;
            // 
            // splitContainerAvailablePlugins
            // 
            this.splitContainerAvailablePlugins.Location = new System.Drawing.Point(6, 6);
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
            this.splitContainerAvailablePlugins.Size = new System.Drawing.Size(402, 302);
            this.splitContainerAvailablePlugins.SplitterDistance = 191;
            this.splitContainerAvailablePlugins.TabIndex = 0;
            // 
            // lblTextNoInternetConnection
            // 
            this.lblTextNoInternetConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextNoInternetConnection.Location = new System.Drawing.Point(3, 20);
            this.lblTextNoInternetConnection.Name = "lblTextNoInternetConnection";
            this.lblTextNoInternetConnection.Size = new System.Drawing.Size(185, 111);
            this.lblTextNoInternetConnection.TabIndex = 2;
            this.lblTextNoInternetConnection.Text = "Internet connection required to view available plugin here.";
            this.lblTextNoInternetConnection.Visible = false;
            // 
            // chlbxAvailiblePlugins
            // 
            this.chlbxAvailiblePlugins.Dock = System.Windows.Forms.DockStyle.Top;
            this.chlbxAvailiblePlugins.FormattingEnabled = true;
            this.chlbxAvailiblePlugins.Items.AddRange(new object[] {
            "loading..."});
            this.chlbxAvailiblePlugins.Location = new System.Drawing.Point(0, 0);
            this.chlbxAvailiblePlugins.Name = "chlbxAvailiblePlugins";
            this.chlbxAvailiblePlugins.Size = new System.Drawing.Size(191, 259);
            this.chlbxAvailiblePlugins.TabIndex = 1;
            this.chlbxAvailiblePlugins.SelectedIndexChanged += new System.EventHandler(this.chlbxAvailiblePlugins_SelectedIndexChanged);
            // 
            // lblLicense
            // 
            this.lblLicense.ForeColor = System.Drawing.Color.Black;
            this.lblLicense.Location = new System.Drawing.Point(9, 64);
            this.lblLicense.Name = "lblLicense";
            this.lblLicense.Size = new System.Drawing.Size(164, 22);
            this.lblLicense.TabIndex = 3;
            // 
            // lblPluginVersion
            // 
            this.lblPluginVersion.ForeColor = System.Drawing.Color.DimGray;
            this.lblPluginVersion.Location = new System.Drawing.Point(9, 41);
            this.lblPluginVersion.Name = "lblPluginVersion";
            this.lblPluginVersion.Size = new System.Drawing.Size(184, 23);
            this.lblPluginVersion.TabIndex = 2;
            // 
            // lblPluginName
            // 
            this.lblPluginName.AutoSize = true;
            this.lblPluginName.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPluginName.Location = new System.Drawing.Point(7, 12);
            this.lblPluginName.Name = "lblPluginName";
            this.lblPluginName.Size = new System.Drawing.Size(112, 29);
            this.lblPluginName.TabIndex = 1;
            this.lblPluginName.Text = "Loading..";
            // 
            // lblPluginDescription
            // 
            this.lblPluginDescription.Location = new System.Drawing.Point(9, 86);
            this.lblPluginDescription.Name = "lblPluginDescription";
            this.lblPluginDescription.Size = new System.Drawing.Size(195, 151);
            this.lblPluginDescription.TabIndex = 0;
            this.lblPluginDescription.Text = "Getting plugin details from the internet.";
            // 
            // btnPluginDownload
            // 
            this.btnPluginDownload.Location = new System.Drawing.Point(3, 262);
            this.btnPluginDownload.Name = "btnPluginDownload";
            this.btnPluginDownload.Size = new System.Drawing.Size(201, 23);
            this.btnPluginDownload.TabIndex = 0;
            this.btnPluginDownload.Text = "download";
            this.btnPluginDownload.UseCompatibleTextRendering = true;
            this.btnPluginDownload.UseVisualStyleBackColor = true;
            this.btnPluginDownload.Visible = false;
            this.btnPluginDownload.Click += new System.EventHandler(this.btnPluginDownload_Click);
            // 
            // pluginGrid
            // 
            this.pluginGrid.Location = new System.Drawing.Point(0, 18);
            this.pluginGrid.Name = "pluginGrid";
            this.pluginGrid.Size = new System.Drawing.Size(414, 290);
            this.pluginGrid.TabIndex = 0;
            // 
            // searchtbPlugins
            // 
            this.searchtbPlugins.Location = new System.Drawing.Point(0, 260);
            this.searchtbPlugins.Name = "searchtbPlugins";
            this.searchtbPlugins.Size = new System.Drawing.Size(188, 28);
            this.searchtbPlugins.TabIndex = 4;
            this.searchtbPlugins.DoSearch += new NoteFly.SearchTextBox.DoSearchHandler(this.searchtbPlugins_DoSearch);
            // 
            // FrmPlugins
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 320);
            this.Controls.Add(this.tabControlPlugins);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmPlugins";
            this.Text = "Plugins";
            this.tabControlPlugins.ResumeLayout(false);
            this.tabPagePluginsInstalled.ResumeLayout(false);
            this.tabPagePluginsAvailable.ResumeLayout(false);
            this.splitContainerAvailablePlugins.Panel1.ResumeLayout(false);
            this.splitContainerAvailablePlugins.Panel2.ResumeLayout(false);
            this.splitContainerAvailablePlugins.Panel2.PerformLayout();
            this.splitContainerAvailablePlugins.ResumeLayout(false);
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