//-----------------------------------------------------------------------
// <copyright file="ShortcutTextBox.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2012  Tom
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
    using System.ComponentModel;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// 
    /// </summary>
    [Category("Custom")]
    public partial class ShortcutTextBox : TextBox
    {
        /// <summary>
        /// 
        /// </summary>
        private bool altinsteadofshift = false;

        /// <summary>
        /// 
        /// </summary>
        private Keys key = Keys.F1;

        /// <summary>
        /// 
        /// </summary>
        private bool prev_altinsteadofshift = false;

        /// <summary>
        /// 
        /// </summary>
        private Keys prev_key = Keys.F1;

        /// <summary>
        /// Creating a new instance of ShortcutTextBox class.
        /// </summary>
        public ShortcutTextBox()
        {
            this.TextAlign = HorizontalAlignment.Center;            
            //this.setcontent();
        }

        /// <summary>
        /// 
        /// </summary>
        [Description("The final key")]
        public int ShortcutKeyposition
        {
            get
            {
                int keypos = -1;
                try
                {
                    keypos = (int)this.key;
                }
                catch
                {
                    Log.Write(LogType.exception, "Error: technically, converting Keys enum to key position failed.");
                }

                return keypos;
            }

            set
            {
                try
                {
                    this.key = (Keys)value;                    
                }
                catch
                {
                    Log.Write(LogType.exception, "Error: technically, converting key position to Keys enum item failed.");
                }

                this.setcontent();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Description("The second key")]
        public bool UseAltInsteadofShift
        {
            get
            {
                return this.altinsteadofshift;
            }

            set
            {
                this.altinsteadofshift = value;
                this.setcontent();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            e.SuppressKeyPress = true; // prevent typing
            this.Clear();
            
            this.key = e.KeyCode;
            if (!e.Control && !e.Shift && !e.Alt)
            {
                this.altinsteadofshift = false; // set to default

                // no modifiers pressed
                this.setcontent();
            }
            else
            {
                this.altinsteadofshift = e.Alt;
                this.setcontent();                
            }

            this.SelectionStart = this.TextLength;
            base.OnKeyDown(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyUp(KeyEventArgs e)
        {            
            this.key = this.prev_key;
            this.altinsteadofshift = this.prev_altinsteadofshift;
            this.setcontent();
            base.OnKeyUp(e);
        }

        /// <summary>
        /// 
        /// </summary>
        private void setcontent()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("CTRL + ");
            
            if (this.altinsteadofshift)  
            {
                sb.Append("ALT + ");                
            }
            else
            {
                sb.Append("SHIFT + ");
            }

            if (this.IsModifierKey(this.key))
            {
                this.BackColor = System.Drawing.Color.LightYellow;
                sb.Append("?");                
            }
            else
            {
                sb.Append(this.key.ToString());
                this.BackColor = System.Drawing.Color.White;

                this.prev_altinsteadofshift = this.altinsteadofshift;
                this.prev_key = this.key;
            }
            
            this.Text = sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private bool IsModifierKey(Keys key)
        {
            if (key == Keys.ControlKey || key == Keys.ShiftKey || key == Keys.Alt || key == Keys.Menu)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
