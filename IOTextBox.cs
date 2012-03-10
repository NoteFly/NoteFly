//-----------------------------------------------------------------------
// <copyright file="IOTextBox.cs" company="NoteFly">
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
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// IOTextBox control
    /// </summary>
    internal partial class IOTextBox : TextBox
    {
        /// <summary>
        /// 
        /// </summary>
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
        /// <param name="e"></param>
        protected override void OnTextChanged(System.EventArgs e)
        {
            if (this.setuptext)
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

            this.setuptext = true;
            base.OnTextChanged(e);
        }
    }
}
