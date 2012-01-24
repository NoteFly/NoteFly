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
            this.pluginGrid = new NoteFly.PluginGrid();
            this.tabPagePluginsAvailable = new System.Windows.Forms.TabPage();
            this.splitContainerAvailablePlugins = new System.Windows.Forms.SplitContainer();
            this.searchtbPlugins = new NoteFly.SearchTextBox();
            this.lblTextNoInternetConnection = new System.Windows.Forms.Label();
            this.chlbxAvailiblePlugins = new System.Windows.Forms.CheckedListBox();
            this.lblLicense = new System.Windows.Forms.Label();
            this.lblPluginVersion = new System.Windows.Forms.Label();
            this.lblPluginName = new System.Windows.Forms.Label();
            this.lblPluginDescription = new System.Windows.Forms.Label();
            this.btnPluginDownload = new System.Windows.Forms.Button();
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
            this.tabControlPlugins.AccessibleDescription = null;
            this.tabControlPlugins.AccessibleName = null;
            resources.ApplyResources(this.tabControlPlugins, "tabControlPlugins");
            this.tabControlPlugins.BackgroundImage = null;
            this.tabControlPlugins.Controls.Add(this.tabPagePluginsInstalled);
            this.tabControlPlugins.Controls.Add(this.tabPagePluginsAvailable);
            this.tabControlPlugins.Font = null;
            this.tabControlPlugins.Name = "tabControlPlugins";
            this.tabControlPlugins.SelectedIndex = 0;
            this.tabControlPlugins.SelectedIndexChanged += new System.EventHandler(this.tabControlPlugins_SelectedIndexChanged);
            // 
            // tabPagePluginsInstalled
            // 
            this.tabPagePluginsInstalled.AccessibleDescription = null;
            this.tabPagePluginsInstalled.AccessibleName = null;
            resources.ApplyResources(this.tabPagePluginsInstalled, "tabPagePluginsInstalled");
            this.tabPagePluginsInstalled.BackgroundImage = null;
            this.tabPagePluginsInstalled.Controls.Add(this.pluginGrid);
            this.tabPagePluginsInstalled.Font = null;
            this.tabPagePluginsInstalled.Name = "tabPagePluginsInstalled";
            this.tabPagePluginsInstalled.UseVisualStyleBackColor = true;
            // 
            // pluginGrid
            // 
            this.pluginGrid.AccessibleDescription = null;
            this.pluginGrid.AccessibleName = null;
            resources.ApplyResources(this.pluginGrid, "pluginGrid");
            this.pluginGrid.BackgroundImage = null;
            this.pluginGrid.Font = null;
            this.pluginGrid.Name = "pluginGrid";
            // 
            // tabPagePluginsAvailable
            // 
            this.tabPagePluginsAvailable.AccessibleDescription = null;
            this.tabPagePluginsAvailable.AccessibleName = null;
            resources.ApplyResources(this.tabPagePluginsAvailable, "tabPagePluginsAvailable");
            this.tabPagePluginsAvailable.BackgroundImage = null;
            this.tabPagePluginsAvailable.Controls.Add(this.splitContainerAvailablePlugins);
            this.tabPagePluginsAvailable.Font = null;
            this.tabPagePluginsAvailable.Name = "tabPagePluginsAvailable";
            this.tabPagePluginsAvailable.UseVisualStyleBackColor = true;
            // 
            // splitContainerAvailablePlugins
            // 
            this.splitContainerAvailablePlugins.AccessibleDescription = null;
            this.splitContainerAvailablePlugins.AccessibleName = null;
            resources.ApplyResources(this.splitContainerAvailablePlugins, "splitContainerAvailablePlugins");
            this.splitContainerAvailablePlugins.BackgroundImage = null;
            this.splitContainerAvailablePlugins.Font = null;
            this.splitContainerAvailablePlugins.Name = "splitContainerAvailablePlugins";
            // 
            // splitContainerAvailablePlugins.Panel1
            // 
            this.splitContainerAvailablePlugins.Panel1.AccessibleDescription = null;
            this.splitContainerAvailablePlugins.Panel1.AccessibleName = null;
            resources.ApplyResources(this.splitContainerAvailablePlugins.Panel1, "splitContainerAvailablePlugins.Panel1");
            this.splitContainerAvailablePlugins.Panel1.BackgroundImage = null;
            this.splitContainerAvailablePlugins.Panel1.Controls.Add(this.searchtbPlugins);
            this.splitContainerAvailablePlugins.Panel1.Controls.Add(this.lblTextNoInternetConnection);
            this.splitContainerAvailablePlugins.Panel1.Controls.Add(this.chlbxAvailiblePlugins);
            this.splitContainerAvailablePlugins.Panel1.Font = null;
            // 
            // splitContainerAvailablePlugins.Panel2
            // 
            this.splitContainerAvailablePlugins.Panel2.AccessibleDescription = null;
            this.splitContainerAvailablePlugins.Panel2.AccessibleName = null;
            resources.ApplyResources(this.splitContainerAvailablePlugins.Panel2, "splitContainerAvailablePlugins.Panel2");
            this.splitContainerAvailablePlugins.Panel2.BackgroundImage = null;
            this.splitContainerAvailablePlugins.Panel2.Controls.Add(this.lblLicense);
            this.splitContainerAvailablePlugins.Panel2.Controls.Add(this.lblPluginVersion);
            this.splitContainerAvailablePlugins.Panel2.Controls.Add(this.lblPluginName);
            this.splitContainerAvailablePlugins.Panel2.Controls.Add(this.lblPluginDescription);
            this.splitContainerAvailablePlugins.Panel2.Controls.Add(this.btnPluginDownload);
            this.splitContainerAvailablePlugins.Panel2.Font = null;
            this.splitContainerAvailablePlugins.Panel2Collapsed = true;
            // 
            // searchtbPlugins
            // 
            this.searchtbPlugins.AccessibleDescription = null;
            this.searchtbPlugins.AccessibleName = null;
            resources.ApplyResources(this.searchtbPlugins, "searchtbPlugins");
            this.searchtbPlugins.BackgroundImage = null;
            this.searchtbPlugins.Font = null;
            this.searchtbPlugins.Name = "searchtbPlugins";
            this.searchtbPlugins.SearchStart += new NoteFly.SearchTextBox.SearchStartHandler(this.searchtbPlugins_SearchStart);
            this.searchtbPlugins.SearchStop += new NoteFly.SearchTextBox.SearchStopHandler(this.searchtbPlugins_SearchStop);
            // 
            // lblTextNoInternetConnection
            // 
            this.lblTextNoInternetConnection.AccessibleDescription = null;
            this.lblTextNoInternetConnection.AccessibleName = null;
            resources.ApplyResources(this.lblTextNoInternetConnection, "lblTextNoInternetConnection");
            this.lblTextNoInternetConnection.Name = "lblTextNoInternetConnection";
            // 
            // chlbxAvailiblePlugins
            // 
            this.chlbxAvailiblePlugins.AccessibleDescription = null;
            this.chlbxAvailiblePlugins.AccessibleName = null;
            resources.ApplyResources(this.chlbxAvailiblePlugins, "chlbxAvailiblePlugins");
            this.chlbxAvailiblePlugins.BackgroundImage = null;
            this.chlbxAvailiblePlugins.Font = null;
            this.chlbxAvailiblePlugins.FormattingEnabled = true;
            this.chlbxAvailiblePlugins.Items.AddRange(new object[] {
            resources.GetString("chlbxAvailiblePlugins.Items")});
            this.chlbxAvailiblePlugins.Name = "chlbxAvailiblePlugins";
            this.chlbxAvailiblePlugins.SelectedIndexChanged += new System.EventHandler(this.chlbxAvailiblePlugins_SelectedIndexChanged);
            // 
            // lblLicense
            // 
            this.lblLicense.AccessibleDescription = null;
            this.lblLicense.AccessibleName = null;
            resources.ApplyResources(this.lblLicense, "lblLicense");
            this.lblLicense.Font = null;
            this.lblLicense.ForeColor = System.Drawing.Color.Black;
            this.lblLicense.Name = "lblLicense";
            // 
            // lblPluginVersion
            // 
            this.lblPluginVersion.AccessibleDescription = null;
            this.lblPluginVersion.AccessibleName = null;
            resources.ApplyResources(this.lblPluginVersion, "lblPluginVersion");
            this.lblPluginVersion.Font = null;
            this.lblPluginVersion.ForeColor = System.Drawing.Color.DimGray;
            this.lblPluginVersion.Name = "lblPluginVersion";
            // 
            // lblPluginName
            // 
            this.lblPluginName.AccessibleDescription = null;
            this.lblPluginName.AccessibleName = null;
            resources.ApplyResources(this.lblPluginName, "lblPluginName");
            this.lblPluginName.Name = "lblPluginName";
            // 
            // lblPluginDescription
            // 
            this.lblPluginDescription.AccessibleDescription = null;
            this.lblPluginDescription.AccessibleName = null;
            resources.ApplyResources(this.lblPluginDescription, "lblPluginDescription");
            this.lblPluginDescription.Font = null;
            this.lblPluginDescription.Name = "lblPluginDescription";
            // 
            // btnPluginDownload
            // 
            this.btnPluginDownload.AccessibleDescription = null;
            this.btnPluginDownload.AccessibleName = null;
            resources.ApplyResources(this.btnPluginDownload, "btnPluginDownload");
            this.btnPluginDownload.BackgroundImage = null;
            this.btnPluginDownload.Font = null;
            this.btnPluginDownload.Name = "btnPluginDownload";
            this.btnPluginDownload.UseCompatibleTextRendering = true;
            this.btnPluginDownload.UseVisualStyleBackColor = true;
            this.btnPluginDownload.Click += new System.EventHandler(this.btnPluginDownload_Click);
            // 
            // FrmPlugins
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackgroundImage = null;
            this.CausesValidation = false;
            this.Controls.Add(this.tabControlPlugins);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPlugins";
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