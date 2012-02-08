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
    using System.Text;
    using System.Windows.Forms;
    using System.ComponentModel;

    [Category("Custom")]
    public partial class ShortcutTextBox : TextBox
    {
        private bool altinsteadofshift = false;
        private int shortcutkey = 112;

        private bool prevaltinsteadofshift = true;
        private int prevshortcutkey = 112;
        private Keys prevshottcutkeycode;

        public ShortcutTextBox()
        {
            this.TextAlign = HorizontalAlignment.Center;
            this.setcontent(Keys.F1);
        }

        [Description("The final key")]
        public int ShortcutKeycode
        {
            get
            {
                return this.shortcutkey;
            }

            set
            {
                this.shortcutkey = value;
                this.prevshortcutkey = this.shortcutkey;
            }
        }

        [Description("The second key")]
        public bool UseAltInsteadofShift
        {
            get
            {
                return this.altinsteadofshift;
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            e.SuppressKeyPress = true; // prevent typing
            this.Clear();
            
            this.shortcutkey = e.KeyValue;
            if (!e.Control && !e.Shift && !e.Alt)
            {
                this.altinsteadofshift = false; // set to default

                // no modifiers pressed
                this.setcontent(e.KeyCode);
            }
            else
            {
                this.altinsteadofshift = e.Alt;
                this.setcontent(e.KeyCode);                
            }

            this.SelectionStart = this.TextLength;
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {            
            this.shortcutkey = this.prevshortcutkey;
            this.altinsteadofshift = this.prevaltinsteadofshift;
            this.setcontent(this.prevshottcutkeycode);
            base.OnKeyUp(e);
        }


        private void setcontent(Keys keycode)
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

            if (this.IsModifierKey(keycode))
            {
                this.BackColor = System.Drawing.Color.LightYellow;
                sb.Append("?");                
            }
            else
            {
                this.BackColor = System.Drawing.Color.White;
                sb.Append(keycode.ToString());

                this.prevshottcutkeycode = keycode;
                this.prevshortcutkey = this.shortcutkey;
                this.prevaltinsteadofshift = this.altinsteadofshift;
            }
            
            this.Text = sb.ToString();
        }

        private bool IsModifierKey(Keys keycode)
        {
            if (keycode == Keys.ControlKey || keycode == Keys.ShiftKey || keycode == Keys.Alt || keycode == Keys.Menu)
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
