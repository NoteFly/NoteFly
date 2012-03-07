namespace NoteFly
{
    /// <summary>
    /// PluginGrid component
    /// </summary>
    public partial class PluginGrid
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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginGrid));
            this.lblTextNopluginsinstalled = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTextNopluginsinstalled
            // 
            resources.ApplyResources(this.lblTextNopluginsinstalled, "lblTextNopluginsinstalled");
            this.lblTextNopluginsinstalled.Name = "lblTextNopluginsinstalled";
            // 
            // PluginGrid
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTextNopluginsinstalled);
            this.MinimumSize = new System.Drawing.Size(200, 100);
            this.Name = "PluginGrid";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTextNopluginsinstalled;
    }
}