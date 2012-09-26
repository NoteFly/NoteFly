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
    /// ShortCutTextBox control
    /// </summary>
    [Category("Custom")]
    public partial class ShortcutTextBox : TextBox
    {
        /// <summary>
        /// Is alt used instead of shift key for shortcut as second key of the shortcut.
        /// </summary>
        private bool altinsteadofshift = false;

        /// <summary>
        /// The last key of the shortcut.
        /// </summary>
        private Keys key = Keys.F1;

        /// <summary>
        /// Is alt key used previously instead of shift key as second key of the shortcut.
        /// </summary>
        private bool previousaltinsteadofshift = false;

        /// <summary>
        /// The previously last key of the shortcut.
        /// </summary>
        private Keys previouskey = Keys.F1;

        /// <summary>
        /// Creating a new instance of ShortcutTextBox class.
        /// </summary>
        public ShortcutTextBox()
        {
            this.TextAlign = HorizontalAlignment.Center;
            //this.setcontent();
        }

        /// <summary>
        /// Gets or sets the last/final key
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
        /// Gets or sets a value indicating whether alt is used instead of shift as second key for the hotkey combination.
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
        /// Key pressed down in this control.
        /// </summary>
        /// <param name="e">Key eveny arguments</param>
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
        /// Key released in this control.
        /// </summary>
        /// <param name="e">Key event arguments</param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            this.key = this.previouskey;
            this.altinsteadofshift = this.previousaltinsteadofshift;
            this.setcontent();
            base.OnKeyUp(e);
        }

        /// <summary>
        /// Set the text in this control based on the pressed shortcut.
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

                this.previousaltinsteadofshift = this.altinsteadofshift;
                this.previouskey = this.key;
            }
            
            this.Text = sb.ToString();
        }

        /// <summary>
        /// Check if the key is a modifier key.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns>True if it's a modifier key.</returns>
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
