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
    using System.Drawing;
    using System.Collections.Generic;

    /// <summary>
    /// A transparent version of the RichTextBox control.
    /// </summary>
    public class TransparentRichTextBox : RichTextBox //internal partial 
    {
        private const string COLORTBLTAG = "{\\colortbl ";  
        private List<Color> colortblitems = new List<Color>();

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newclr"></param>
        public void SetColorInRTF(Color newclr, int textpos, int sellentext)
        {
            //int sellentext = this.SelectionLength;
            //int textpos = this.SelectionStart;
            string rtf = this.Rtf;
            if (string.IsNullOrEmpty(rtf))
            {
                MessageBox.Show("error: rtf empty.", "error rtf", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            int prevnrcoloritem = 1;
            int posstartbody = rtf.IndexOf(@"\viewkind");
            if (posstartbody < 0)
            {
                const string rtfstart = @"{\rtf1";
                if (rtf.StartsWith(rtfstart))
                {
                    posstartbody = rtfstart.Length;
                }
                else
                {
                    MessageBox.Show("error: rtf content body not found.", "error rtf", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            string newrtf = string.Empty;
            int nrtextchar = 0;
            bool rtfformat = true;
            int drtflen = 0;
            string insertcoloritemrtf = string.Empty; ;
            bool overridecoloritem = false;
            int rtflevel = 0;
            for (int i = posstartbody; i < rtf.Length; i++)
            {
                if (rtf[i] == '{')
                {
                    if (rtflevel < int.MaxValue)
                    {
                        rtflevel++;
                    }
                }
                else if (rtf[i] == '}')
                {
                    if (rtflevel > int.MinValue)
                    {
                        rtflevel--;
                    }
                }
                else if (rtflevel == 0)
                {

                    if (rtf[i] == '\\')
                    {
                        rtfformat = true;
                        if (i + 4 < rtf.Length)
                        {
                            const string coloritemrtfcode = @"\cf";
                            if (rtf.Substring(i, 3).Equals(coloritemrtfcode, StringComparison.Ordinal))
                            {                                
                                int startnumpos = i + coloritemrtfcode.Length;
                                int numlen = this.GetLenDigit(rtf, startnumpos);

                                string snr = rtf.Substring(startnumpos, numlen);
                                try
                                {
                                    prevnrcoloritem = Convert.ToInt32(snr);
                                }
                                catch (FormatException formatexc)
                                {
                                    MessageBox.Show(formatexc.Message);
                                }

                                if (overridecoloritem)
                                {
                                    newrtf = newrtf.Remove(i + drtflen + insertcoloritemrtf.Length, coloritemrtfcode.Length + numlen + 1); // +1 for space
                                    drtflen = drtflen - (coloritemrtfcode.Length + numlen + 1);
                                    overridecoloritem = false;
                                }
                            }
                        }
                    }


                    if (!rtfformat)
                    {
                        nrtextchar++;
                    }

                    if (rtf[i] == ' ' || rtf[i] == '\r' || rtf[i] == '\n')
                    {
                        rtfformat = false;
                    }

                    if (textpos == nrtextchar)
                    {
                        int prevrtflen = rtf.Length;
                        int nrcoloritem = this.GetColoritemPosColortblRTF(rtf, newclr);
                        if (nrcoloritem < 0)
                        {
                            // create coloritem
                            newrtf = this.AddColorToColortblRTF(rtf, newclr.R, newclr.G, newclr.B);
                            nrcoloritem = this.GetColoritemPosColortblRTF(newrtf, newclr);
                            if (nrcoloritem < 0)
                            {
                                // Their is no colortbl.
                                // TODO create colortbl
                                return;
                            }
                        }
                        else
                        {
                            newrtf = rtf;
                        }

                        drtflen = newrtf.Length - prevrtflen;

                        insertcoloritemrtf = "\\cf" + nrcoloritem + " ";
                        int poscoloritem = i + drtflen + 1;
                        newrtf = newrtf.Insert(poscoloritem, insertcoloritemrtf);
                        overridecoloritem = true;
                    }
                    else if (textpos + sellentext == nrtextchar)
                    {
                        string previnsertcoloritemrtf = "\\cf" + prevnrcoloritem + " ";
                        int prevposcoloritem = i + drtflen + 1 + insertcoloritemrtf.Length;
                        newrtf = newrtf.Insert(prevposcoloritem, previnsertcoloritemrtf);
                        overridecoloritem = false;
                        break;
                    }
                }
            }

            this.Rtf = newrtf;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rtf"></param>
        /// <param name="clr"></param>
        /// <returns>-1 if not found</returns>
        private int GetColoritemPosColortblRTF(string rtf, Color clr)
        {
            this.ParserColorTbl(rtf);
            for (int i = 0; i < this.colortblitems.Count; i++)
            {
                if (this.colortblitems[i].R == clr.R && this.colortblitems[i].G == clr.G && this.colortblitems[i].B == clr.B)
                {
                    return i + 1;
                }
            }

            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="colortblitemraw"></param>
        /// <returns></returns>
        private Color ParserColorItem(string colortblitemraw)
        {
            const string redpropertie = "red";
            const string greenpropertie = "green";
            const string bluepropertie = "blue";
            int red = 0;
            int green = 0;
            int blue = 0;
            string[] colorprop = colortblitemraw.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            for (int n = 0; n < colorprop.Length; n++)
            {
                if (colorprop[n].StartsWith(redpropertie))
                {
                    string redstr = colorprop[n].Substring(redpropertie.Length, colorprop[n].Length - redpropertie.Length);
                    red = Convert.ToInt32(redstr);
                }
                else if (colorprop[n].StartsWith(greenpropertie))
                {
                    string greenstr = colorprop[n].Substring(greenpropertie.Length, colorprop[n].Length - greenpropertie.Length);
                    green = Convert.ToInt32(greenstr);
                }
                else if (colorprop[n].StartsWith(bluepropertie))
                {
                    string bluestr = colorprop[n].Substring(bluepropertie.Length, colorprop[n].Length - bluepropertie.Length);
                    blue = Convert.ToInt32(bluestr);
                }
            }

            Color clr = Color.FromArgb(red, green, blue);
            return clr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rtf"></param>
        private void ParserColorTbl(string rtf)
        {
            colortblitems.Clear();
            int startcolortbl = FindPosStartColortbl(rtf);
            int startcoloritem = startcolortbl + COLORTBLTAG.Length + 1;
            for (int i = startcoloritem; rtf[i] != '}'; i++)
            {
                if (rtf[i] == ';')
                {
                    string colortblitemraw = rtf.Substring(startcoloritem, i - startcoloritem);
                    startcoloritem = i + 1;
                    Color coloritem = this.ParserColorItem(colortblitemraw);
                    this.colortblitems.Add(coloritem);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rtf"></param>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <returns></returns>
        private string AddColorToColortblRTF(string rtf, int red, int green, int blue)
        {
            int startcolortbl = FindPosStartColortbl(rtf);
            if (startcolortbl > 0)
            {
                int p = startcolortbl;
                while (rtf[p] != '}')
                {
                    p++;
                }

                string valeau = "\\red" + red + "\\green" + green + "\\blue" + blue + ";";
                rtf = rtf.Insert(p, valeau); // FIXME colortbl is sorted, find out how.
            }

            return rtf;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rtf"></param>
        /// <returns></returns>
        private int FindPosStartColortbl(string rtf)
        {      
            int startcolortbl = rtf.IndexOf(COLORTBLTAG, 0, StringComparison.Ordinal);
            return startcolortbl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="startpos"></param>
        /// <returns></returns>
        private int GetLenDigit(string text, int startpos)
        {
            int numlen = 0;
            while (Char.IsDigit(text, (startpos + numlen)) && (startpos + numlen) < text.Length && numlen <= Int32.MaxValue.ToString().Length)
            {
                numlen++;
            }

            return numlen;
        }

    }
}
