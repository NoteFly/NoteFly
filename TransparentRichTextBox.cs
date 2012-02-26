//-----------------------------------------------------------------------
// <copyright file="TransparentRichTextBox.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2011-2012  Tom
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
    using System.Text;

    /// <summary>
    /// A transparent version of the RichTextBox control.
    /// </summary>
    public class TransparentRichTextBox : RichTextBox //internal partial 
    {
        private const string RTF1DOCTAG = @"{\rtf1";
        private const string VIEWKINDTAG = @"\viewkind";
        private const string COLORTBLTAG = @"{\colortbl ";
        private const string COLORITEMTAG = @"\cf";
        private List<Color> colortblitems = null; // = new List<Color>();

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
        public string SetColorInRTF(string rtf, Color newclr, int textpos, int sellentext)
        {
            int prevnrcoloritem = 1;
            int posstartbody = rtf.IndexOf(VIEWKINDTAG);
            if (posstartbody < 0)
            {                
                if (rtf.StartsWith(RTF1DOCTAG))
                {
                    posstartbody = RTF1DOCTAG.Length;
                }
                else
                {
                    Log.Write(LogType.exception, "rtf content body not found.");
                    return rtf;
                }
            }
            else
            {
                posstartbody += VIEWKINDTAG.Length;
            }

            //StringBuilder newrtf = new StringBuilder(rtf);
            string newrtf = rtf;
            int nrtextchar = 0;
            bool rtfformat = true;
            int drtflen = 0;
            string insertcoloritemrtf = null;
            bool overridecoloritem = false;
            int rtflevel = 0;
            for (int i = posstartbody; i < rtf.Length; i++)
            {
                if (rtf[i] == '{')
                {
                    rtfformat = false;
                    if (rtflevel < int.MaxValue)
                    {
                        rtflevel++;
                    }
                }
                else if (rtf[i] == '}')
                {
                    rtfformat = false;
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
                        int startnumpos = i + COLORITEMTAG.Length;
                        if (startnumpos < rtf.Length)
                        {
                            if (rtf.Substring(i, COLORITEMTAG.Length).Equals(COLORITEMTAG, StringComparison.Ordinal))
                            {
                                
                                int numlen = this.GetLenDigit(rtf, startnumpos);
                                if (numlen <= 0 || numlen > Int32.MaxValue.ToString().Length)
                                {
                                    return newrtf;
                                }

                                if (!overridecoloritem)
                                {
                                    string snr = rtf.Substring(startnumpos, numlen);
                                    try
                                    {
                                        prevnrcoloritem = Convert.ToInt32(snr);
                                    }
                                    catch (FormatException formatexc)
                                    {
                                        Log.Write(LogType.exception, formatexc.Message);
                                        return newrtf;
                                    }
                                }
                                else if (overridecoloritem)
                                {
                                    int posstartremove = i + drtflen + insertcoloritemrtf.Length;
                                    int totallencftag = COLORITEMTAG.Length + numlen;
                                    if (newrtf[posstartremove + totallencftag] == ' ')
                                    {
                                        totallencftag += 1; // +1 for space
                                    }

                                    drtflen -= totallencftag;
                                    
                                    try
                                    {
                                        newrtf = newrtf.Remove(posstartremove, totallencftag);
                                    }
                                    catch (ArgumentOutOfRangeException argoutrangexc)
                                    {
                                        Log.Write(LogType.exception, argoutrangexc.Message);
                                        return newrtf;
                                    }
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
                        int nrcoloritem = this.GetColoritemPosColortblRTF(this.Rtf, newclr);
                        if (nrcoloritem < 0)
                        {
                            // create coloritem
                            newrtf = this.AddColorToColortblRTF(this.Rtf, newclr.R, newclr.G, newclr.B);
                            //newrtf = new StringBuilder(this.AddColorToColortblRTF(rtf, newclr.R, newclr.G, newclr.B));
                            nrcoloritem = this.GetColoritemPosColortblRTF(newrtf, newclr);
                            if (nrcoloritem < 0)
                            {
                                // Their is no colortbl.
                                // TODO create colortbl
                                //return;
                            }
                        }
                        else
                        {
                            newrtf = rtf;
                            //newrtf = new StringBuilder(rtf);
                        }

                        drtflen = newrtf.Length - rtf.Length;

                        insertcoloritemrtf = COLORITEMTAG + nrcoloritem + " ";
                        int poscoloritem = i + drtflen + 1; // +1 for space
                        try
                        {
                            newrtf = newrtf.Insert(poscoloritem, insertcoloritemrtf);
                        }
                        catch (ArgumentOutOfRangeException argoutrangeexc)
                        {
                            Log.Write(LogType.exception, argoutrangeexc.Message);
                            return newrtf;
                        }

                        overridecoloritem = true;
                    }
                    else if (textpos + sellentext == nrtextchar)
                    {
                        string previnsertcoloritemrtf = COLORITEMTAG + prevnrcoloritem + " ";
                        int prevposcoloritem = i + drtflen + 1 + insertcoloritemrtf.Length;
                        try
                        {
                            newrtf = newrtf.Insert(prevposcoloritem, previnsertcoloritemrtf);
                        }
                        catch (ArgumentOutOfRangeException argoutrangeexc)
                        {
                            Log.Write(LogType.exception, argoutrangeexc.Message);
                            return newrtf;
                        }

                        overridecoloritem = false;
                        break;
                    }
                }
            }

            return newrtf;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rtf"></param>
        /// <param name="clr"></param>
        /// <returns>-1 if not found</returns>
        private int GetColoritemPosColortblRTF(string rtf, Color clr)
        {
            //if (this.colortblitems == null)
            //{
                this.ParserColorTbl(rtf);
            //}

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
            this.colortblitems = new List<Color>();
            this.colortblitems.Clear();
            int startcolortbl = FindPosStartColortbl(rtf);
            int startcoloritem = startcolortbl + COLORTBLTAG.Length + 1; // +1 for space
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
            else
            {
                // todo create color table.
                int posendfonttbl = rtf.IndexOf("}}");
                rtf = rtf.Insert(posendfonttbl + 2, "\n"+COLORTBLTAG + ";\\red" + red + "\\green" + green + "\\blue" + blue + ";}");
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
            int startcolortbl = rtf.IndexOf(COLORTBLTAG, RTF1DOCTAG.Length, StringComparison.Ordinal);
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
            while (Char.IsDigit(text, (startpos + numlen)) && (startpos + numlen) < text.Length && numlen < Int32.MaxValue.ToString().Length)
            {
                numlen++;
            }

            return numlen;
        }

    }
}
