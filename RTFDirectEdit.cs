namespace NoteFly
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Drawing;

    /// <summary>
    /// Allows RTF text to be edited if it's normal text without formatting.
    /// </summary>
    class RTFDirectEdit
    {
        private const string RTF1DOCTAG = @"{\rtf1";
        private const string VIEWKINDTAG = @"\viewkind";
        private const string COLORTBLTAG = @"{\colortbl ";
        private const string COLORITEMTAG = @"\cf";
        private const string RTFTABTAG = @"\tab";
        private List<Color> colortblitems = new List<Color>();
        private bool rtfformat = true;
        private char[] hexchars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };

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
                                    prevnrcoloritem = IntParseFast(snr);
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

                    if (!rtfformat)
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
                            rtfformat = false;
                            isspecchar = false;
                            speccharrtfpos = 0;
                        }

                        speccharrtfpos++;
                    }

                    if (rtf[i] == ' ' || rtf[i] == '\r' || rtf[i] == '\n')
                    {
                        rtfformat = false;
                    }

                    if (textpos == nrtextchar && !textposdone)
                    {
                        textposdone = true;
                        int nrcoloritem = 1;
                        nrcoloritem = this.GetNrcoloritem(newrtf.ToString(), newclr);
                        if (nrcoloritem < 0)
                        {
                            // newclr does not exist as coloritem in colortbl.
                            newrtf = this.AddColorItem(newrtf, posstartcolortbl, newclr); //newclr.R, newclr.G, newclr.B
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
                    rtfformat = false;
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
            return SetColorInRTF(rtf, newclr, 0, rtf.Length);
        }

        /// <summary>
        /// 
        /// </summary>
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
        /// <param name="rtf"></param>
        /// <param name="i"></param>
        /// <param name="rtflevel"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="rtf"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private bool IsSpecialCharRTF(string rtf, int i)
        {
            bool isspecchar = false;
            const int speccharlen = 4;
            if (i + speccharlen < rtf.Length)
            {
                if (rtf.Substring(i, 2).Equals(@"\'", StringComparison.Ordinal))
                {
                    // todo check if two character are HEX characters in a better way
                    if (rtf.Substring(i + 2, 1).IndexOfAny(this.hexchars) >= 0 && rtf.Substring(i + 3, 1).IndexOfAny(this.hexchars) >= 0)
                    {
                        isspecchar = true;
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
            const string redpropertie = "red";
            const string greenpropertie = "green";
            const string bluepropertie = "blue";
            int red = 0;
            int green = 0;
            int blue = 0;
            string[] colorprop = colortblraw.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            for (int n = 0; n < colorprop.Length; n++)
            {
                if (colorprop[n].StartsWith(redpropertie))
                {
                    string redstr = colorprop[n].Substring(redpropertie.Length, colorprop[n].Length - redpropertie.Length);
                    red = this.IntParseFast(redstr);
                }
                else if (colorprop[n].StartsWith(greenpropertie))
                {
                    string greenstr = colorprop[n].Substring(greenpropertie.Length, colorprop[n].Length - greenpropertie.Length);
                    green = this.IntParseFast(greenstr);
                }
                else if (colorprop[n].StartsWith(bluepropertie))
                {
                    string bluestr = colorprop[n].Substring(bluepropertie.Length, colorprop[n].Length - bluepropertie.Length);
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
        /// 
        /// </summary>
        /// <param name="rtf"></param>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <returns></returns>
        private StringBuilder AddColorItem(StringBuilder newrtf, int posstartcolortbl, Color newclr) // int red, int green, int blue
        {

            string valeau = "\\red" + newclr.R + "\\green" + newclr.G + "\\blue" + newclr.B + ";";
            //string valeau = "\\red" + red + "\\green" + green + "\\blue" + blue + ";";
            int p = posstartcolortbl;
            while (newrtf[p] != '}' && p < newrtf.Length)
            {
                p++;
            }

            newrtf.Insert(p, valeau);
            this.colortblitems.Add(newclr);
            // parser colortbl again.
            //this.ParserColorTbl(newrtf.ToString(), posstartcolortbl);
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
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="startpos"></param>
        /// <returns></returns>
        private int GetLenDigit(string text, int startpos)
        {
            int numlen = 0;
            int pos = startpos + numlen;
            int maxlen = Int32.MaxValue.ToString().Length;
            while (Char.IsDigit(text, pos) && (pos < text.Length && numlen < maxlen))
            {
                numlen++;
                pos = startpos + numlen;
            }

            return numlen;
        }
    }
}
