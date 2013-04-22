using System.Windows.Forms;
using System.Drawing;

namespace SkinsEditor
{
    /// <summary>
    /// 
    /// </summary>
    public partial class NoteSkinPreview : UserControl
    {
        /// <summary>
        /// The interface to let this plugin talk back to NoteFly.
        /// </summary>
        private IPlugin.IPluginHost host;

        /// <summary>
        /// 
        /// </summary>
        public NoteSkinPreview()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        public IPlugin.IPluginHost Host
        {
            set 
            {
                this.host = value;
            }
        }

        /// <summary>
        /// Show a preview of a note with a skin given by the skinnr.
        /// </summary>
        public void DrawNoteSkinPreview(Skin skin)
        {
            FontStyle titlestyle = FontStyle.Regular;
            if (this.host.GetSettingBool("FontTitleStylebold"))
            {
                titlestyle = FontStyle.Bold;
            }

            this.lblPreviewNoteTitle.ForeColor = skin.TextClr;
            this.lblPreviewNoteTitle.Font = new Font(this.host.GetSettingString("FontTitleFamily"), this.host.GetSettingFloat("FontTitleSize"), titlestyle);
            this.lblPreviewNoteContent.ForeColor = skin.TextClr;
            this.lblPreviewNoteContent.Font = new Font(this.host.GetSettingString("FontContentFamily"), this.host.GetSettingFloat("FontContentSize"));
            if (!string.IsNullOrEmpty(skin.PrimaryTexture))
            {
                this.pnlPreviewNoteWindow.BackgroundImage = Image.FromFile(skin.PrimaryTexture);
                this.pnlPreviewNoteWindow.BackgroundImageLayout = skin.PrimaryTextureLayout;
                this.pnlPreviewNoteHead.BackColor = Color.Transparent;
                this.pnlPreviewNoteContent.BackColor = Color.Transparent;
            }
            else
            {
                this.pnlPreviewNoteWindow.BackgroundImage = null;
                this.pnlPreviewNoteHead.BackColor = skin.PrimaryClr;
                this.pnlPreviewNoteContent.BackColor = skin.PrimaryClr;
            }

            if (this.lblPreviewNoteTitle.Height + this.lblPreviewNoteTitle.Location.Y >= this.pnlPreviewNoteHead.Height)
            {
                if (this.lblPreviewNoteTitle.Height < this.host.GetSettingInt("NotesTitlepanelMaxHeight"))
                {
                    this.pnlPreviewNoteHead.Height = this.lblPreviewNoteTitle.Height;
                }
                else
                {
                    this.pnlPreviewNoteHead.Height = this.host.GetSettingInt("NotesTitlepanelMaxHeight");
                }
            }
            else
            {
                this.pnlPreviewNoteHead.Height = this.host.GetSettingInt("NotesTitlepanelMinHeight");
            }

            this.pnlPreviewNoteContent.Location = new Point(0, this.pnlPreviewNoteHead.Height);
            this.pnlPreviewNoteContent.Size = new Size(200, 172 - this.pnlPreviewNoteHead.Height);
            this.gbxPreviewNote.Visible = true;
        }
    }
}