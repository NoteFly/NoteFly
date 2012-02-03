namespace NoteFly
{
    using System.IO;
    using System.Windows.Forms;
    using System.Drawing;

    internal partial class IOTextBox : TextBox
    {
        private bool setuptext = false;

        /// <summary>
        /// Avoid illegal path characters
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            char[] forbiddenpathchars = Path.GetInvalidPathChars();
            for (int i = 0; i < forbiddenpathchars.Length; i++)
			{
                if (e.KeyValue == System.Convert.ToInt32(forbiddenpathchars[i]))
                {
                    if (e.KeyData != Keys.Back && e.KeyData != Keys.Delete && e.KeyData != Keys.Shift && e.KeyData != Keys.ShiftKey && e.KeyData != Keys.CapsLock)
                    {
                        e.SuppressKeyPress = true;
                    }
                }
			}

            base.OnKeyDown(e);
            
        }

        /// <summary>
        /// Check file path or path if it exists,
        /// only after IOTextBox is been created.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnTextChanged(System.EventArgs e)
        {
            if (setuptext)
            {
                if (File.Exists(this.Text) || Directory.Exists(this.Text))
                {
                    this.BackColor = Color.LightGreen;
                }
                else
                {
                    this.BackColor = Color.LightSalmon;
                }
            }

            setuptext = true;
            base.OnTextChanged(e);
        }
    }
}
