namespace NoteFly
{
    using System.Windows.Forms;

    /// <summary>
    /// IP address textbox.
    /// With background color ip address validation hint.
    /// </summary>
    internal partial class IPTextBox : TextBox    
    {
        /// <summary>
        /// IPAddress object.
        /// </summary>
        private System.Net.IPAddress ipaddr;

        /// <summary>
        /// Get the IP address
        /// </summary>
        /// <returns></returns>
        public string getIPAddress()
        {
            if (this.ipaddr != null)
            {
                return ipaddr.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Character entered.
        /// Allow only IPv4 and IPv6 characters to be added.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {  
            int k = e.KeyValue;
            
            // 'a'-'f'
            // ':'
            // '.'
            // '0'-'9'
            // shift, backspace, left, right, delete key
            if ((k >= 65 && k <= 70) || (k == 186 && e.Shift) || (k == 190 && !e.Shift) || (k >= 48 && k <= 57) || (k == 8 || k == 16 || k == 37 || k == 39 || k == 46))
            {
                e.SuppressKeyPress = false;
            }
            else
            {
                e.SuppressKeyPress = true;
            }

            base.OnKeyDown(e); // dont remove
        }

        /// <summary>
        /// Validate ip address on key up.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            bool validipaddr = System.Net.IPAddress.TryParse(this.Text, out this.ipaddr);

            if (validipaddr && (this.getnumofdots() == 3 || this.Text.Contains(":")))
            {
                this.BackColor = System.Drawing.Color.LightGreen;                
            }
            else
            {
                if (this.TextLength > 0)
                {
                    this.BackColor = System.Drawing.Color.Salmon;
                }
                else
                {
                    this.BackColor = System.Drawing.SystemColors.Window;
                }
            }

            base.OnKeyUp(e); // dont remove
        }

        /// <summary>
        /// Get the number of dots in the Text content.
        /// </summary>
        /// <returns></returns>
        private int getnumofdots()
        {
            int numdots = 0;
            for (int i = 0; i < this.Text.Length; i++)
            {
                if (this.Text[i] == '.')
                {
                    numdots++;
                }
            }

            return numdots;
        }
        
    }
}
