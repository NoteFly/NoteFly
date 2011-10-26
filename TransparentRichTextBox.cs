//-----------------------------------------------------------------------
// <copyright file="TransparentRichTextBox.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2011  Tom
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
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    /// <summary>
    /// A transparent version of the RichTextBox control.
    /// </summary>
    internal class TransparentRichTextBox : RichTextBox
    {
#if windows
        /// <summary>
        /// Override createParams to add support for a transparant background image.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams prams = base.CreateParams;
                if (Settings.NotesTransparentRTB)
                {                    
                    if (LoadLibrary("msftedit.dll") != IntPtr.Zero)
                    {
                        prams.ExStyle |= 0x020; // transparent
                        prams.ClassName = "RICHEDIT50W";
                    }                    
                }

                return prams;
            }
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr LoadLibrary(string lpFileName);
#endif
    }
}
