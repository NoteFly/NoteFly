namespace NoteFly
{
    /// <summary>
    /// FrmPlugins window
    /// </summary>
    public partial class FrmPlugins
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// TabControl tabControlPlugins
        /// </summary>
        private System.Windows.Forms.TabControl tabControlPlugins;

        /// <summary>
        /// TabPage tabPagePluginsAvailable
        /// </summary>
        private System.Windows.Forms.TabPage tabPagePluginsAvailable;

        /// <summary>
        /// TabPage tabPagePluginsInstalled
        /// </summary>
        private System.Windows.Forms.TabPage tabPagePluginsInstalled;

        /// <summary>
        /// PluginGrid pluginGrid
        /// </summary>
        private PluginGrid pluginGrid;

        /// <summary>
        /// SplitContainer splitContainerAvailablePlugins
        /// </summary>
        private System.Windows.Forms.SplitContainer splitContainerAvailablePlugins;

        /// <summary>
        /// Button btnPluginDownload
        /// </summary>
        private System.Windows.Forms.Button btnPluginDownload;

        /// <summary>
        /// Label lblPluginDescription
        /// </summary>
        private System.Windows.Forms.Label lblPluginDescription;

        /// <summary>
        /// Label lblPluginName
        /// </summary>
        private System.Windows.Forms.Label lblPluginName;

        /// <summary>
        /// Label lblPluginVersion
        /// </summary>
        private System.Windows.Forms.Label lblPluginVersion;

        /// <summary>
        /// Label lblTextNoInternetConnection
        /// </summary>
        private System.Windows.Forms.Label lblTextNoInternetConnection;

        /// <summary>
        /// Label lblLicense
        /// </summary>
        private System.Windows.Forms.Label lblLicense;

        /// <summary>
        /// SearchTextBox searchtbPlugins
        /// </summary>
        private SearchTextBox searchtbPlugins;

        /// <summary>
        /// ListBox lbxAvailablePlugins
        /// </summary>
        private System.Windows.Forms.ListBox lbxAvailablePlugins;

        /// <summary>
        /// Clean up any resources being used..</param>
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPlugins));
            this.tabControlPlugins = new System.Windows.Forms.TabControl();
            this.tabPagePluginsInstalled = new System.Windows.Forms.TabPage();
            this.tabPagePluginsAvailable = new System.Windows.Forms.TabPage();
            this.splitContainerAvailablePlugins = new System.Windows.Forms.SplitContainer();
            this.lblTextNoInternetConnection = new System.Windows.Forms.Label();
            this.lbxAvailablePlugins = new System.Windows.Forms.ListBox();
            this.lblLicense = new System.Windows.Forms.Label();
            this.lblPluginVersion = new System.Windows.Forms.Label();
            this.lblPluginName = new System.Windows.Forms.Label();
            this.lblPluginDescription = new System.Windows.Forms.Label();
            this.btnPluginDownload = new System.Windows.Forms.Button();
            this.timerTextUpdater = new System.Windows.Forms.Timer(this.components);
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
            this.tabControlPlugins.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlPlugins.Location = new System.Drawing.Point(0, 0);
            this.tabControlPlugins.Margin = new System.Windows.Forms.Padding(4);
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
            this.tabPagePluginsInstalled.Padding = new System.Windows.Forms.Padding(4);
            this.tabPagePluginsInstalled.Size = new System.Drawing.Size(480, 365);
            this.tabPagePluginsInstalled.TabIndex = 1;
            this.tabPagePluginsInstalled.Text = "Installed";
            this.tabPagePluginsInstalled.UseVisualStyleBackColor = true;
            // 
            // tabPagePluginsAvailable
            // 
            this.tabPagePluginsAvailable.Controls.Add(this.splitContainerAvailablePlugins);
            this.tabPagePluginsAvailable.Location = new System.Drawing.Point(4, 25);
            this.tabPagePluginsAvailable.Margin = new System.Windows.Forms.Padding(4);
            this.tabPagePluginsAvailable.Name = "tabPagePluginsAvailable";
            this.tabPagePluginsAvailable.Padding = new System.Windows.Forms.Padding(4);
            this.tabPagePluginsAvailable.Size = new System.Drawing.Size(480, 365);
            this.tabPagePluginsAvailable.TabIndex = 0;
            this.tabPagePluginsAvailable.Text = "available";
            this.tabPagePluginsAvailable.UseVisualStyleBackColor = true;
            // 
            // splitContainerAvailablePlugins
            // 
            this.splitContainerAvailablePlugins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerAvailablePlugins.Location = new System.Drawing.Point(4, 4);
            this.splitContainerAvailablePlugins.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainerAvailablePlugins.Name = "splitContainerAvailablePlugins";
            // 
            // splitContainerAvailablePlugins.Panel1
            // 
            this.splitContainerAvailablePlugins.Panel1.Controls.Add(this.lblTextNoInternetConnection);
            this.splitContainerAvailablePlugins.Panel1.Controls.Add(this.lbxAvailablePlugins);
            this.splitContainerAvailablePlugins.Panel1.Controls.Add(this.searchtbPlugins);
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
            this.lblTextNoInternetConnection.Location = new System.Drawing.Point(4, 48);
            this.lblTextNoInternetConnection.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTextNoInternetConnection.Name = "lblTextNoInternetConnection";
            this.lblTextNoInternetConnection.Size = new System.Drawing.Size(213, 129);
            this.lblTextNoInternetConnection.TabIndex = 2;
            this.lblTextNoInternetConnection.Visible = false;
            // 
            // lbxAvailablePlugins
            // 
            this.lbxAvailablePlugins.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbxAvailablePlugins.FormattingEnabled = true;
            this.lbxAvailablePlugins.ItemHeight = 16;
            this.lbxAvailablePlugins.Items.AddRange(new object[] {
            "Loading..."});
            this.lbxAvailablePlugins.Location = new System.Drawing.Point(0, 0);
            this.lbxAvailablePlugins.Name = "lbxAvailablePlugins";
            this.lbxAvailablePlugins.Size = new System.Drawing.Size(221, 324);
            this.lbxAvailablePlugins.TabIndex = 5;
            this.lbxAvailablePlugins.SelectedIndexChanged += new System.EventHandler(this.lbxAvailablePlugins_SelectedIndexChanged);
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
            this.btnPluginDownload.Location = new System.Drawing.Point(4, 329);
            this.btnPluginDownload.Margin = new System.Windows.Forms.Padding(4);
            this.btnPluginDownload.Name = "btnPluginDownload";
            this.btnPluginDownload.Size = new System.Drawing.Size(238, 28);
            this.btnPluginDownload.TabIndex = 0;
            this.btnPluginDownload.UseCompatibleTextRendering = true;
            this.btnPluginDownload.UseVisualStyleBackColor = true;
            this.btnPluginDownload.Visible = false;
            this.btnPluginDownload.Click += new System.EventHandler(this.btnPluginDownload_Click);
            // 
            // timerTextUpdater
            // 
            this.timerTextUpdater.Interval = 200;
            this.timerTextUpdater.Tick += new System.EventHandler(this.timerTextUpdater_Tick);
            // 
            // pluginGrid
            // 
            this.pluginGrid.AutoScroll = true;
            this.pluginGrid.BackColor = System.Drawing.Color.Transparent;
            this.pluginGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pluginGrid.Location = new System.Drawing.Point(4, 4);
            this.pluginGrid.Margin = new System.Windows.Forms.Padding(0);
            this.pluginGrid.MinimumSize = new System.Drawing.Size(10, 10);
            this.pluginGrid.Name = "pluginGrid";
            this.pluginGrid.Size = new System.Drawing.Size(472, 357);
            this.pluginGrid.TabIndex = 0;
            // 
            // searchtbPlugins
            // 
            this.searchtbPlugins.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.searchtbPlugins.Enabled = false;
            this.searchtbPlugins.Location = new System.Drawing.Point(0, 327);
            this.searchtbPlugins.Margin = new System.Windows.Forms.Padding(4);
            this.searchtbPlugins.MaximumSize = new System.Drawing.Size(220, 30);
            this.searchtbPlugins.Name = "searchtbPlugins";
            this.searchtbPlugins.Size = new System.Drawing.Size(220, 30);
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
            this.Margin = new System.Windows.Forms.Padding(4);
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

        private System.Windows.Forms.Timer timerTextUpdater;

    }
}