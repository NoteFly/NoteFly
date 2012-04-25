//-----------------------------------------------------------------------
// <copyright file="RTFDirectEdit.cs" company="NoteFly">
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
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    /// <summary>
    /// Allows RTF text to be edited if it's normal text without formatting.
    /// </summary>
    public class RTFDirectEdit
    {
        /// <summary>
        /// The RTF start tag
        /// </summary>
        private const string RTF1DOCTAG = @"{\rtf1";

        /// <summary>
        /// The RTF document viewkind tag
        /// </summary>
        private const string VIEWKINDTAG = @"\viewkind";

        /// <summary>
        /// The RTF colortable tag
        /// </summary>
        private const string COLORTBLTAG = @"{\colortbl ";

        /// <summary>
        /// The RTF color format tag
        /// </summary>
        private const string COLORITEMTAG = @"\cf";

        /// <summary>
        /// RTF tab character
        /// </summary>
        private const string RTFTABTAG = @"\tab";

        /// <summary>
        /// List with colortable coloritems
        /// </summary>
        private List<Color> colortblitems = new List<Color>();

        /// <summary>
        /// Are we at current position in the RTF document buzy with RTF code format or text content.
        /// </summary>
        private bool rtfformat = true;

        /// <summary>
        /// Array with hexcharacters (lowercase alpha).
        /// </summary>
        private char[] hexchars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newclr"></param>
        /// <returns></returns>
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

            const int EXTRACAP = 40; // tune me
            StringBuilder newrtf = new StringBuilder(rtf, rtf.Length + EXTRACAP);
            int nrtextchar = 0;
            int drtflen = 0;
            string insertcoloritemrtf = null;
            bool overridecoloritem = false;
            int rtflevel = 0;
            this.rtfformat = true;
            bool textposdone = false;
            bool isspecchar = false;
            int speccharrtfpos = 0;
            int posstartcolortbl = this.FindPosStartColortbl(newrtf.ToString());
            if (posstartcolortbl < 0)
            {
                // create colortbl, because it doesn't exist.
                newrtf = this.CreateColortbl(newrtf, newclr);
                posstartcolortbl = this.FindPosStartColortbl(newrtf.ToString());
                if (posstartcolortbl < 0)
                {
                    Log.Write(LogType.exception, "Cannot create colortbl.");
                    return newrtf.ToString();
                }
            }

            this.ParserColorTbl(newrtf.ToString(), posstartcolortbl);
            for (int i = posstartbody; i < rtf.Length; i++)
            {
                if (this.CheckRTFLevel(rtf, i, rtflevel) == 0)
                {
                    if (rtf[i] == '\\')
                    {
                        this.rtfformat = true;
                        bool istabtag = false;
                        if (i + RTFTABTAG.Length < rtf.Length)
                        {
                            if (rtf.Substring(i, RTFTABTAG.Length).Equals(RTFTABTAG, StringComparison.Ordinal))
                            {
                                // tab is only 1 character in text.
                                nrtextchar += 1;
                                istabtag = true;
                            }
                        }

                        if (!isspecchar && !istabtag)
                        {
                            isspecchar = this.IsSpecialCharRTF(rtf, i);
                        }

                        if (i + COLORITEMTAG.Length < rtf.Length && !istabtag && !isspecchar)
                        {
                            if (rtf.Substring(i, COLORITEMTAG.Length).Equals(COLORITEMTAG, StringComparison.Ordinal))
                            {
                                int numlen = this.GetLenDigit(rtf, i + COLORITEMTAG.Length);
                                if (numlen <= 0)
                                {
                                    return newrtf.ToString();
                                }

                                if (!overridecoloritem)
                                {
                                    string snr = rtf.Substring(i + COLORITEMTAG.Length, numlen);
                                    prevnrcoloritem = this.IntParseFast(snr);
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
                                        newrtf.Remove(posstartremove, totallencftag);
                                    }
                                    catch (ArgumentOutOfRangeException argoutrangexc)
                                    {
                                        Log.Write(LogType.exception, argoutrangexc.Message);
                                        return newrtf.ToString();
                                    }
                                }
                            }
                        }
                    }

                    if (!this.rtfformat)
                    {
                        textposdone = false;
                        if (nrtextchar < int.MaxValue)
                        {
                            nrtextchar++;
                        }
                    }

                    if (isspecchar)
                    {
                        if (speccharrtfpos == 0)
                        {
                            nrtextchar++;
                        }
                        else if (speccharrtfpos >= 3)
                        {
                            this.rtfformat = false;
                            isspecchar = false;
                            speccharrtfpos = 0;
                        }

                        speccharrtfpos++;
                    }

                    if (rtf[i] == ' ' || rtf[i] == '\r' || rtf[i] == '\n')
                    {
                        this.rtfformat = false;
                    }

                    if (textpos == nrtextchar && !textposdone)
                    {
                        textposdone = true;
                        int nrcoloritem = 1;
                        nrcoloritem = this.GetNrcoloritem(newrtf.ToString(), newclr);
                        if (nrcoloritem < 0)
                        {
                            // newclr does not exist as coloritem in colortbl.
                            newrtf = this.AddColorItem(newrtf, posstartcolortbl, newclr);
                            // new nr coloritem
                            nrcoloritem = this.GetNrcoloritem(newrtf.ToString(), newclr);
                            if (nrcoloritem < 0)
                            {
                                Log.Write(LogType.exception, "Can't add coloritem to colortbl.");
                                return newrtf.ToString();
                            }
                        }

                        drtflen = newrtf.Length - rtf.Length;
                        insertcoloritemrtf = COLORITEMTAG + nrcoloritem + " ";
                        int poscoloritem = i + drtflen + 1; // +1 for space
                        try
                        {
                            newrtf.Insert(poscoloritem, insertcoloritemrtf);
                        }
                        catch (ArgumentOutOfRangeException argoutrangeexc)
                        {
                            Log.Write(LogType.exception, argoutrangeexc.Message);
                            return newrtf.ToString();
                        }

                        overridecoloritem = true;
                    }
                    else if (textpos + sellentext == nrtextchar)
                    {
                        string previnsertcoloritemrtf = COLORITEMTAG + prevnrcoloritem + " ";
                        int prevposcoloritem = i + drtflen + 1 + insertcoloritemrtf.Length;
                        try
                        {
                            newrtf.Insert(prevposcoloritem, previnsertcoloritemrtf);
                        }
                        catch (ArgumentOutOfRangeException argoutrangeexc)
                        {
                            Log.Write(LogType.exception, argoutrangeexc.Message);
                            return newrtf.ToString();
                        }

                        overridecoloritem = false;
                        break;
                    }
                }
                else
                {
                    this.rtfformat = false;
                }
            }

            return newrtf.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rtf"></param>
        /// <param name="newclr"></param>
        /// <returns></returns>
        public string SetColorAllRTF(string rtf, Color newclr)
        {
            return this.SetColorInRTF(rtf, newclr, 0, rtf.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>StringBuilder of colortbl.</returns>
        private StringBuilder CreateColortbl(StringBuilder newrtf, Color textcolor)
        {
            // Create color table.
            const string ENDFONTTBL = "}}\r\n";
            int posendfonttbl = newrtf.ToString().IndexOf(ENDFONTTBL);
            string valeau = "\\red" + textcolor.R + "\\green" + textcolor.G + "\\blue" + textcolor.B + ";";
            newrtf.Insert(posendfonttbl + ENDFONTTBL.Length, COLORTBLTAG + ";" + valeau + "}\r\n");
            return newrtf;
        }

        /// <summary>
        /// Check if rtf contains a new level or depth document level has been gone up.
        /// Escape \ characters are not a new rtf level.
        /// </summary>
        /// <param name="rtf">The RTF text</param>
        /// <param name="i">THe position in RTF</param>
        /// <param name="rtflevel">Current RTF depth level.</param>
        /// <returns>New RTF depth level.</returns>
        private int CheckRTFLevel(string rtf, int i, int rtflevel)
        {
            if (rtf[i] == '{')
            {
                if (i - 1 >= 0)
                {
                    if (rtf[i - 1] != '\\')
                    {
                        if (rtflevel < int.MaxValue)
                        {
                            rtflevel++;
                        }
                    }
                    else
                    {
                        this.rtfformat = false;
                    }
                }
                else
                {
                    if (rtflevel < int.MaxValue)
                    {
                        rtflevel++;
                    }
                }
            }
            else if (rtf[i] == '}')
            {
                if (i - 1 >= 0)
                {
                    if (rtf[i - 1] != '\\')
                    {
                        if (rtflevel > 0)
                        {
                            rtflevel--;
                        }
                    }
                    else
                    {
                        this.rtfformat = false;
                    }
                }
                else
                {
                    if (rtflevel > 0)
                    {
                        rtflevel--;
                    }
                }
            }

            return rtflevel;
        }

        /// <summary>
        /// Is the RTF part at a position a RTF special chracter escaping.
        /// </summary>
        /// <param name="rtf">The RTF check</param>
        /// <param name="i">The RTF position in where the special character starts</param>
        /// <returns></returns>
        private bool IsSpecialCharRTF(string rtf, int i)
        {
            bool isspecchar = false;
            const int speccharlen = 4;
            if (i + speccharlen < rtf.Length)
            {
                if (rtf.Substring(i, 2).Equals(@"\'", StringComparison.Ordinal))
                {
                    if (rtf.Substring(i + 2, 1).IndexOfAny(this.hexchars) >= 0 && rtf.Substring(i + 3, 1).IndexOfAny(this.hexchars) >= 0)
                    {
                        isspecchar = true;
                    }
                }
                else if (rtf.Substring(i, 2).Equals(@"\u", StringComparison.Ordinal))
                {
                    // check if not unicode control character \uc0 or \uc1
                    if (rtf[i + 2] != 'c')
                    {   
                        // check if at least 4 digits
                        for (int n = 2; n < 6; n++)
                        {
                            char c = rtf[i + n];
                            isspecchar = char.IsDigit(c);
                            if (!isspecchar)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return isspecchar;
        }

        /// <summary>
        /// Converts a string to integer, optimization.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private int IntParseFast(string value)
        {
            int result = 0;
            int length = value.Length;
            for (int i = 0; i < length; i++)
            {
                result = 10 * result + (value[i] - 48);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rtf"></param>
        /// <param name="clr"></param>
        /// <returns>-1 if not found</returns>
        private int GetNrcoloritem(string rtf, Color clr)
        {
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
        private Color ParserColorItem(string colortblraw)
        {
            const string REDPROPERTIE = "red";
            const string GREENPROPERTIE = "green";
            const string BLUEPROPERTIE = "blue";
            int red = 0;
            int green = 0;
            int blue = 0;
            string[] colorprop = colortblraw.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            for (int n = 0; n < colorprop.Length; n++)
            {
                if (colorprop[n].StartsWith(REDPROPERTIE))
                {
                    string redstr = colorprop[n].Substring(REDPROPERTIE.Length, colorprop[n].Length - REDPROPERTIE.Length);
                    red = this.IntParseFast(redstr);
                }
                else if (colorprop[n].StartsWith(GREENPROPERTIE))
                {
                    string greenstr = colorprop[n].Substring(GREENPROPERTIE.Length, colorprop[n].Length - GREENPROPERTIE.Length);
                    green = this.IntParseFast(greenstr);
                }
                else if (colorprop[n].StartsWith(BLUEPROPERTIE))
                {
                    string bluestr = colorprop[n].Substring(BLUEPROPERTIE.Length, colorprop[n].Length - BLUEPROPERTIE.Length);
                    blue = this.IntParseFast(bluestr);
                }
            }

            Color clr = Color.FromArgb(red, green, blue);
            return clr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rtf"></param>
        private void ParserColorTbl(string rtf, int posstartcolortbl)
        {
            this.colortblitems.Clear();
            int startcoloritem = posstartcolortbl + COLORTBLTAG.Length + 1; // +1 for space
            for (int i = startcoloritem; rtf[i] != '}'; i++)
            {
                if (rtf[i] == ';')
                {
                    string colortblraw = rtf.Substring(startcoloritem, i - startcoloritem);
                    startcoloritem = i + 1;
                    Color coloritem = this.ParserColorItem(colortblraw);
                    this.colortblitems.Add(coloritem);
                }
            }
        }

        /// <summary>
        /// Add a color to the RTF colortbl.
        /// </summary>
        /// <param name="rtf">The RTF</param>
        /// <param name="posstartcolortbl">The position of start of colortbl in RTF.</param>
        /// <param name="red">Red valeau (0-255)</param>
        /// <param name="green">Green valeau (0-255)</param>
        /// <param name="blue">Blue valeau (0-255)</param>
        /// <returns>The new RTF with the color add to the colortbl.</returns>
        private StringBuilder AddColorItem(StringBuilder newrtf, int posstartcolortbl, Color newclr)
        {
            string valeau = "\\red" + newclr.R + "\\green" + newclr.G + "\\blue" + newclr.B + ";";
            int p = posstartcolortbl;
            while (newrtf[p] != '}' && p < newrtf.Length)
            {
                p++;
            }

            newrtf.Insert(p, valeau);
            this.colortblitems.Add(newclr);
            return newrtf;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rtf"></param>
        /// <returns></returns>
        private int FindPosStartColortbl(string rtf)
        {
            return rtf.IndexOf(COLORTBLTAG, RTF1DOCTAG.Length, StringComparison.Ordinal);
        }

        /// <summary>
        /// Get the length of the digits in a string at a position
        /// </summary>
        /// <param name="text">Text with digits at startpos</param>
        /// <param name="startpos">The position where the digit are</param>
        /// <returns>The length of the digits</returns>
        private int GetLenDigit(string text, int startpos)
        {
            int numlen = 0;
            int pos = startpos + numlen;
            int maxlen = int.MaxValue.ToString().Length;
            while (char.IsDigit(text, pos) && (pos < text.Length && numlen < maxlen))
            {
                numlen++;
                pos = startpos + numlen;
            }

            return numlen;
        }
    }
}
