//-----------------------------------------------------------------------
// <copyright file="RTFDirectEdit.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2012-2015  Tom
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
        /// RTF bold character
        /// </summary>
        private const string RTFBOLDTAG = @"\b";

        /// <summary>
        /// RTF bold end character
        /// </summary>
        private const string RTFBOLDENDTAG = @"\b0";

        /// <summary>
        /// RTF italic tag
        /// </summary>
        private const string RTFITALICTAG = @"\i";

        /// <summary>
        /// RTF italic endtag
        /// </summary>
        private const string RTFITALICENDTAG = @"\i0";

        /// <summary>
        /// RTF strike tag
        /// </summary>
        private const string RTFSTRIKETAG = @"\strike";

        /// <summary>
        /// RTF strike endtag
        /// </summary>
        private const string RTFSTRIKEENDTAG = @"\strike0";

        /// <summary>
        /// RTF underline tag
        /// </summary>
        private const string RTFUNDERLINETAG = @"\ul";

        /// <summary>
        /// RTF enderline endtag
        /// </summary>
        private const string RTFUNDERLINEENDTAG = @"\ulnone";

        /// <summary>
        /// Maximum length of a RTF tag
        /// </summary>
        private const int MAXLENRTFTTAG = 10;

        /// <summary>
        /// List with colortable coloritems
        /// </summary>
        private List<Color> colortblitems = new List<Color>();

        /// <summary>
        /// Are we at current position in the RTF document buzy with RTF code format or text content.
        /// </summary>
        private bool rtfformat = true;

        /// <summary>
        /// Different length between orginal RTF and new RTF code length.
        /// </summary>
        private int drtflen = 0;

        /// <summary>
        /// Array with hexcharacters (lowercase alpha).
        /// </summary>
        private char[] hexchars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };

        /// <summary>
        /// Make text bold.
        /// </summary>
        /// <param name="rtf">The rtf stream.</param>
        /// <param name="textpos">The text position.</param>
        /// <param name="sellentext">The selected length to make bold.</param>
        /// <returns>The rtf stream with new bold formatting.</returns>
        public string AddBoldTagInRTF(string rtf, int textpos, int sellentext)
        {
            return this.SetTagInRTF(rtf, textpos, sellentext, RTFBOLDTAG, RTFBOLDENDTAG);
        }

        /// <summary>
        /// Make text italic.
        /// </summary>
        /// <param name="rtf">The rtf stream.</param>
        /// <param name="textpos">The text position.</param>
        /// <param name="sellentext">The selected length to make italic.</param>
        /// <returns>The rtf stream with new italic formatting.</returns>
        public string AddItalicTagInRTF(string rtf, int textpos, int sellentext)
        {
            return this.SetTagInRTF(rtf, textpos, sellentext, RTFITALICTAG, RTFITALICENDTAG);
        }

        /// <summary>
        /// Make text underline.
        /// </summary>
        /// <param name="rtf">The rtf stream.</param>
        /// <param name="textpos">The text position.</param>
        /// <param name="sellentext">The selected length to make underline.</param>
        /// <returns>The rtf stream with new underline formatting.</returns>
        public string AddUnderlineTagInRTF(string rtf, int textpos, int sellentext)
        {
            return this.SetTagInRTF(rtf, textpos, sellentext, RTFUNDERLINETAG, RTFUNDERLINEENDTAG);
        }

        /// <summary>
        /// Make text striketought.
        /// </summary>
        /// <param name="rtf">The rtf stream.</param>
        /// <param name="textpos">The text position.</param>
        /// <param name="sellentext">The selected length to make striketought.</param>
        /// <returns>The rtf stream with new striketought formatting.</returns>
        public string AddStrikeTagInRTF(string rtf, int textpos, int sellentext)
        {
            return this.SetTagInRTF(rtf, textpos, sellentext, RTFSTRIKETAG, RTFSTRIKEENDTAG);
        }

        /// <summary>
        /// Remove text bold.
        /// </summary>
        /// <param name="rtf">The rtf stream.</param>
        /// <param name="textpos">The text position.</param>
        /// <param name="sellentext">The selected length to remove bold formatting from.</param>
        /// <returns>The rtf stream without bold formatting.</returns>
        public string RemoveBoldTagsInRTF(string rtf, int textpos, int sellentext)
        {
            return this.RemoveTagsInRTF(rtf, textpos, sellentext, RTFBOLDTAG, RTFBOLDENDTAG);
        }

        /// <summary>
        /// Remove italic text.
        /// </summary>
        /// <param name="rtf">The rtf stream.</param>
        /// <param name="textpos">The text position.</param>
        /// <param name="sellentext">The selected length to remove italic formatting from.</param>
        /// <returns>The new rtf stream without italic formatting.</returns>
        public string RemoveItalicTagsInRTF(string rtf, int textpos, int sellentext)
        {
            return this.RemoveTagsInRTF(rtf, textpos, sellentext, RTFITALICTAG, RTFITALICENDTAG);
        }

        /// <summary>
        /// Remove striketought text.
        /// </summary>
        /// <param name="rtf">The rtf stream.</param>
        /// <param name="textpos">The text position.</param>
        /// <param name="sellentext">The select text length.</param>
        /// <returns>The new rtf stream without striketought formatting.</returns>
        public string RemoveStrikeTagsInRTF(string rtf, int textpos, int sellentext)
        {
            return this.RemoveTagsInRTF(rtf, textpos, sellentext, RTFSTRIKETAG, RTFSTRIKEENDTAG);
        }

        /// <summary>
        /// Remove underline text.
        /// </summary>
        /// <param name="rtf">The rtf stream.</param>
        /// <param name="textpos">The text position.</param>
        /// <param name="sellentext">The selected text length.</param>
        /// <returns>The new rtf stream.</returns>
        public string RemoveUnderlineTagsInRTF(string rtf, int textpos, int sellentext)
        {
            return this.RemoveTagsInRTF(rtf, textpos, sellentext, RTFUNDERLINETAG, RTFUNDERLINEENDTAG);
        }

        /// <summary>
        /// Set all rtf text to a particulair color.
        /// </summary>
        /// <param name="rtf">The rtf stream.</param>
        /// <param name="newclr">The color to use for all text in the rtf stream.</param>
        /// <param name="textlength">The length of the text.</param>
        /// <returns>The new rtf stream.</returns>
        public string SetColorAllRTF(string rtf, Color newclr, int textlength)
        {
            return this.SetColorInRTF(rtf, newclr, 0, textlength);
        }

        /// <summary>
        /// Set the font size in RTF.
        /// </summary>
        /// <param name="rtf">The RTF stream.</param>
        /// <param name="textpos">The text position.</param>
        /// <param name="seltextlen">The selected text length.</param>
        /// <param name="fontsize">The new font size.</param>
        /// <returns></returns>
        public string SetSizeTagInRtf(string rtf, int textpos, int seltextlen, int fontsize)
        {
            string fontsizestarttag = "\fs" + fontsize.ToString();
            string fontsizeendtag = "\fs12"; // TODO find the font size of before and set that back.
            this.SetTagInRTF(rtf, textpos, seltextlen, fontsizestarttag, fontsizeendtag);
            return rtf;
        }

        /// <summary>
        /// The color of text in the RTF stream,
        /// </summary>
        /// <param name="rtf">The rtf stream.</param>
        /// <param name="newclr">The color to give the text.</param>
        /// <param name="textpos">The text position.</param>
        /// <param name="textselectedlength">The text length to color.</param>
        /// <returns>The new RTF stream.</returns>
        public string SetColorInRTF(string rtf, Color newclr, int textpos, int textselectedlength)
        {
            const int INITEXTRACAP = 32;
            StringBuilder sbrtfnew = new StringBuilder(rtf, rtf.Length + INITEXTRACAP);
            this.rtfformat = true;
            this.drtflen = 0;
            int prevnrcoloritem = 1;
            int currenttextpos = 0;
            string insertcoloritemrtf = null;
            bool overridecoloritem = false;
            int rtflevel = 0;
            bool textposdone = false;
            bool isspecchar = false;
            int speccharrtfpos = 0;
            int posstartcolortbl = this.FindPosStartColortbl(sbrtfnew.ToString());
            if (posstartcolortbl < 0)
            {
                // create colortbl, because it doesn't exist.
                sbrtfnew = this.CreateColortbl(sbrtfnew, newclr);
                posstartcolortbl = this.FindPosStartColortbl(sbrtfnew.ToString());
                if (posstartcolortbl < 0)
                {
                    Log.Write(LogType.exception, "Cannot create colortbl.");
                    return sbrtfnew.ToString();
                }
            }

            this.ParserColorTbl(sbrtfnew.ToString(), posstartcolortbl);
            for (int i = this.FindStartPosDoc(rtf); i < rtf.Length; ++i)
            {
                if (this.CheckRTFNestedLevel(rtf, i, rtflevel) != 0)
                {
                    this.rtfformat = false;
                    continue;
                }

                if (rtf[i] == '\\')
                {
                    speccharrtfpos = 0;
                    this.rtfformat = true;
                    bool isescapedslash = this.IsRTFSlash(rtf, i);
                    bool istabtag = this.IsRTFTab(rtf, i);
                    isspecchar = false;
                    isspecchar = this.IsRTFCodepointChar(rtf, i);
                    if (!isspecchar)
                    {
                        isspecchar = this.IsRTFUnicodeChar(rtf, i);
                    }

                    if (istabtag || isescapedslash)
                    {
                        // A tab is 1 character in text and is 4 characters "\tab" in the RTF stream.
                        // An escaped slash is 1 charater in text and is 2 characters "\\" in the RTF stream.
                        currenttextpos += 1;
                    }
                    else if (i + COLORITEMTAG.Length < rtf.Length && !isspecchar)
                    {
                        if (rtf.Substring(i, COLORITEMTAG.Length).Equals(COLORITEMTAG, StringComparison.Ordinal))
                        {
                            int numlen = this.GetLenDigit(rtf, i + COLORITEMTAG.Length);
                            if (numlen <= 0)
                            {
                                Log.Write(LogType.exception, "RTF: \\cf without number.");
                                return sbrtfnew.ToString();
                            }

                            if (!overridecoloritem)
                            {
                                string snr = rtf.Substring(i + COLORITEMTAG.Length, numlen);
                                prevnrcoloritem = this.IntParseFast(snr);
                            }
                            else if (overridecoloritem)
                            {
                                int posstartremove = i + this.drtflen + insertcoloritemrtf.Length;
                                int totallencftag = COLORITEMTAG.Length + numlen;
                                if (sbrtfnew[posstartremove + totallencftag] == ' ')
                                {
                                    totallencftag += 1; // +1 for space
                                }

                                drtflen -= totallencftag;

                                try
                                {
                                    sbrtfnew.Remove(posstartremove, totallencftag);
                                }
                                catch (ArgumentOutOfRangeException argoutrangexc)
                                {
                                    Log.Write(LogType.exception, argoutrangexc.Message);
                                    return sbrtfnew.ToString();
                                }
                            }
                        }
                    }
                }

                currenttextpos = this.NextTextChar(currenttextpos);
                if (isspecchar)
                {
                    if (speccharrtfpos == 0)
                    {
                        currenttextpos++;
                    }

                    speccharrtfpos++;
                }

                this.rtfformat = this.InRTFFormat(rtf, i, this.rtfformat);
                if (textpos == currenttextpos && !textposdone)
                {
                    // add begintag \cfX
                    textposdone = true;
                    int nrcoloritem = 1;
                    nrcoloritem = this.GetNrcoloritem(sbrtfnew.ToString(), newclr);
                    if (nrcoloritem < 0)
                    {
                        // newclr does not exist as coloritem in colortbl.
                        sbrtfnew = this.AddColorItem(sbrtfnew, posstartcolortbl, newclr);

                        // new nr coloritem
                        nrcoloritem = this.GetNrcoloritem(sbrtfnew.ToString(), newclr);
                        if (nrcoloritem < 0)
                        {
                            Log.Write(LogType.exception, "Can't add coloritem to colortbl.");
                            return sbrtfnew.ToString();
                        }
                    }

                    insertcoloritemrtf = COLORITEMTAG + nrcoloritem + " ";
                    this.drtflen = sbrtfnew.Length - rtf.Length;
                    int poscoloritem = i + this.drtflen + 1; // / +1 for space required
                    try
                    {
                        sbrtfnew.Insert(poscoloritem, insertcoloritemrtf);
                    }
                    catch (ArgumentOutOfRangeException argoutrangeexc)
                    {
                        Log.Write(LogType.exception, argoutrangeexc.Message);
                        return sbrtfnew.ToString();
                    }

                    //Log.Write(LogType.info, "this.drtflen=" + this.drtflen);
                    overridecoloritem = true;
                }
                else if (textpos + textselectedlength == currenttextpos)
                {
                    // add endtag \cfX
                    string rtfprevinsertcoloritem = COLORITEMTAG + prevnrcoloritem + " ";
                    this.drtflen = sbrtfnew.Length - rtf.Length;
                    int possetbackcoloritem = i + this.drtflen + 1 + 2; // +1 for space, +2 for enters
                    try
                    {
                        sbrtfnew.Insert(possetbackcoloritem, rtfprevinsertcoloritem);
                    }
                    catch (ArgumentOutOfRangeException argoutrangeexc)
                    {
                        Log.Write(LogType.exception, argoutrangeexc.Message);
                        return sbrtfnew.ToString();
                    }

                    overridecoloritem = false;
                    break;
                }
            }

            return sbrtfnew.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nrtextchar"></param>
        /// <returns></returns>
        private int NextTextChar(int nrtextchar) {
            if (!this.rtfformat)
            {
                if (nrtextchar < int.MaxValue)
                {
                    nrtextchar++;
                }
            }

            return nrtextchar;
        }

        /// <summary>
        /// Remove all RTF tags in selected text.
        /// </summary>
        /// <param name="rtf">The rtf stream.</param>
        /// <param name="textpos">The text position.</param>
        /// <param name="sellentext">The selected text length.</param>
        /// <param name="starttag">The RTF starttag to remove.</param>
        /// <param name="endtag">The RTF endtag to remove. Should be of the same kind as RTF starttag.</param>
        /// <returns>The new rtf stream.</returns>
        private string RemoveTagsInRTF(string rtf, int textpos, int sellentext, string starttag, string endtag)
        {
            this.rtfformat = true;
            this.drtflen = 0;
            StringBuilder newrtf = new StringBuilder(rtf);
            int rtflevel = 0;
            int currenttextpos = 0;
            bool tagopen = false;
            bool donebeginpos = false;
            bool doneendpos = false;
            bool isspecchar = false;
            int speccharrtfpos = 0;

            for (int i = this.FindStartPosDoc(rtf); i < rtf.Length && currenttextpos <= (textpos + sellentext + 1); ++i)
            {
                if (this.CheckRTFNestedLevel(rtf, i, rtflevel) == 0)
                {
                    if (rtf[i] == '\\')
                    {
                        speccharrtfpos = 0;
                        this.rtfformat = true;
                        isspecchar = this.IsRTFCodepointChar(rtf, i);
                        if (!isspecchar)
                        {
                            isspecchar = this.IsRTFUnicodeChar(rtf, i);
                        }

                        if (this.IsRTFTab(rtf, i))
                        {
                            currenttextpos++;
                        }

                        tagopen = this.CheckTagOpened(tagopen, rtf, i, starttag, endtag);
                    }
                }
                else
                {
                    this.rtfformat = false;
                }

                if (!this.rtfformat)
                {
                    if (currenttextpos < int.MaxValue)
                    {
                        currenttextpos++;
                    }
                }

                if (isspecchar)
                {
                    if (speccharrtfpos == 0)
                    {
                        currenttextpos++;
                    }

                    speccharrtfpos++;
                }

                this.rtfformat = this.InRTFFormat(rtf, i, this.rtfformat);
                if (rtf.Length > (i + starttag.Length) && rtf.Length > (i + endtag.Length))
                {
                    if (currenttextpos >= textpos && currenttextpos <= (textpos + sellentext))
                    {
                        newrtf = this.RemoveTag(newrtf, rtf, i, starttag, endtag);
                        tagopen = this.CheckTagOpened(tagopen, rtf, i, starttag, endtag);
                    }

                    // is begin?
                    if (currenttextpos == textpos && !donebeginpos)
                    {
                        if (tagopen)
                        {
                            if (rtf[i] == '\\')
                            {
                                newrtf.Insert(i + this.drtflen + 1, endtag);
                                this.drtflen += endtag.Length;
                            }
                            else
                            {
                                newrtf.Insert(i + this.drtflen + 1, endtag + " ");
                                this.drtflen += endtag.Length + 1;
                            }
                        }

                        donebeginpos = true;
                    }
                    else if (currenttextpos == (textpos + sellentext + 1) && !doneendpos)
                    {
                        // need to be last charcter if has RTFformat on this nrtextchar, that why i + this.drtflen here below and: nrtextchar == (textpos + sellentext + 1)
                        if (tagopen)
                        {
                            if (rtf[i] == '\\')
                            {
                                newrtf.Insert(i + this.drtflen, starttag);
                                this.drtflen += starttag.Length;
                            }
                            else
                            {
                                newrtf.Insert(i + this.drtflen, starttag + " ");
                                this.drtflen += starttag.Length + 1;
                            }
                        }

                        doneendpos = true;
                    }
                }
            }

            return newrtf.ToString();
        }

        /// <summary>
        /// Check if the RTF tag is open.
        /// </summary>
        /// <param name="tagopen">True if tag open.</param>
        /// <param name="rtf">The rtf stream.</param>
        /// <param name="i">Position in the rtf stream.</param>
        /// <param name="starttag">RTF start tag.</param>
        /// <param name="endtag">RTF end tag.</param>
        /// <returns>True if tag still in open state.</returns>
        private bool CheckTagOpened(bool tagopen, string rtf, int i, string starttag, string endtag)
        {
            if (rtf.Length > (i + starttag.Length) && rtf.Length > (i + endtag.Length))
            {
                if (rtf.Substring(i, endtag.Length).Equals(endtag, StringComparison.Ordinal))
                {
                    tagopen = false;
                }
                else if (rtf.Substring(i, starttag.Length).Equals(starttag, StringComparison.Ordinal))
                {
                    tagopen = true;
                }
            }

            return tagopen;
        }

        /// <summary>
        /// Remove RTF tags, between position i for length of sellentext.
        /// </summary>
        /// <param name="newrtf">The new rtf stream.</param>
        /// <param name="rtf">The rtf stream.</param>
        /// <param name="i">Position the rtf stream.</param>
        /// <param name="starttag">RTF start tag.</param>
        /// <param name="endtag">RTF end tag.</param>
        /// <returns>The new rtf stream as stringbuilder.</returns>
        private StringBuilder RemoveTag(StringBuilder newrtf, string rtf, int i, string starttag, string endtag)
        {
            int postag = i + this.drtflen;
            if (rtf.Substring(i, endtag.Length).Equals(endtag, StringComparison.Ordinal))
            {
                if (rtf[i + endtag.Length] == ' ')
                {
                    bool rtfformatbefore = false;
                    int n = i - 1;
                    while (rtf[n] != ' ' && rtf[n] != ' ' && rtf[n] != '\r')
                    {
                        if (rtf[n] == '\\')
                        {
                            rtfformatbefore = true;
                        }

                        n--;
                        if (n < i - MAXLENRTFTTAG)
                        {
                            break;
                        }
                    }

                    if (!rtfformatbefore)
                    {
                        newrtf.Remove(postag, endtag.Length + 1); // +1 for space
                        this.drtflen -= endtag.Length + 1;
                    }
                    else
                    {
                        newrtf.Remove(postag, endtag.Length); // +1 for space
                        this.drtflen -= endtag.Length;
                    }
                }
                else
                {
                    newrtf.Remove(postag, endtag.Length);
                    this.drtflen -= endtag.Length;
                }
            }
            else if (rtf.Substring(i, starttag.Length).Equals(starttag, StringComparison.Ordinal))
            {
                if (rtf[i + starttag.Length] == ' ')
                {
                    newrtf.Remove(postag, starttag.Length + 1); // +1 for space
                    this.drtflen -= starttag.Length + 1;
                }
                else
                {
                    newrtf.Remove(postag, starttag.Length);
                    this.drtflen -= starttag.Length;
                }
            }

            return newrtf;
        }

        /// <summary>
        /// Add a RTF start- and endtag to the RTF stream
        /// </summary>
        /// <param name="rtf">The rtf stream.</param>
        /// <param name="textpos">The text position where to add the starttag</param>
        /// <param name="sellentext">The length of the text after the starttag to add the RTF end tag.</param>
        /// <param name="starttag">The RTF start tag to add.</param>
        /// <param name="endtag">The RTF end tag to add.</param>
        /// <returns>The new rtf stream.</returns>
        private string SetTagInRTF(string rtf, int textpos, int sellentext, string starttag, string endtag)
        {
            this.rtfformat = true;
            this.drtflen = 0;
            StringBuilder newrtf = new StringBuilder(rtf, rtf.Length + starttag.Length + endtag.Length);
            int rtflevel = 0;
            int nrtextchar = 0;
            bool isspecchar = false;
            bool textstarttagdone = false;
            int speccharrtfpos = 0;
            bool tagopen = false;
            for (int i = this.FindStartPosDoc(rtf); i < rtf.Length; ++i)
            {
                if (this.CheckRTFNestedLevel(rtf, i, rtflevel) == 0)
                {
                    if (rtf[i] == '\\')
                    {
                        this.rtfformat = true;
                        isspecchar = this.IsRTFUnicodeChar(rtf, i);
                        if (this.IsRTFTab(rtf, i))
                        {
                            nrtextchar++;
                        }

                        if (rtf.Length > i + starttag.Length && rtf.Length > i + endtag.Length)
                        {
                            if (nrtextchar >= textpos && nrtextchar < (textpos + sellentext))
                            {
                                newrtf = this.RemoveTag(newrtf, rtf, i, starttag, endtag);
                            }
                        }
                    }
                }
                else
                {
                    this.rtfformat = false;
                }

                nrtextchar = this.NextTextChar(nrtextchar);
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

                this.rtfformat = this.InRTFFormat(rtf, i, this.rtfformat);

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

                tagopen = this.CheckTagOpened(tagopen, rtf, i, starttag, endtag);

                if (textstarttagdone)
                {
                    if ((textpos + sellentext) == nrtextchar)
                    {
                        textstarttagdone = false;

                        // add end
                        int postag = i + this.drtflen + 1; // +1 for space
                        if (i + 1 < rtf.Length)
                        {
                            if (rtf[i + 1] == '\\')
                            {
                                newrtf.Insert(postag, endtag);
                            }
                            else
                            {
                                newrtf.Insert(postag, endtag + " ");
                                this.drtflen += 1;
                            }
                        }
                        else
                        {
                            newrtf.Insert(postag, endtag + " ");
                            this.drtflen += 1;
                        }
                    }
                }
                else
                {
                    if (textpos == nrtextchar)
                    {
                        if (!this.rtfformat)
                        {
                            if (!tagopen)
                            {
                                textstarttagdone = true;

                                // add begin
                                newrtf.Insert(i + this.drtflen + 1, starttag + " ");
                                this.drtflen = newrtf.Length - rtf.Length;
                                tagopen = true;
                            }
                        }
                    }
                }
            }

            return newrtf.ToString();
        }

        /// <summary>
        /// Figure out if the RTF stream is at position pos still in RTF format mode or
        /// is displaying the text from the RTF stream.
        /// </summary>
        /// <param name="rtf">The rtf stream.</param>
        /// <param name="pos">The position to check.</param>
        /// <param name="rtfformat">The current rtf status, in rtf formating if true</param>
        /// <returns>True if document at pos is in rtf formating mode.</returns>
        private bool InRTFFormat(string rtf, int pos, bool rtfformat)
        {
            if (rtf[pos] == ' ' || rtf[pos] == '\r' || rtf[pos] == '\n')
            {
                rtfformat = false;
            }

            return rtfformat;
        }

        /// <summary>
        /// Is there a RTF tab code at a position in a RTF stream.
        /// </summary>
        /// <param name="rtf">The rtf stream.</param>
        /// <param name="i">Position with the rtf stream to look for a tab rtf character.</param>
        /// <returns>True if rtf at position i is RTF tab code.</returns>
        private bool IsRTFTab(string rtf, int i)
        {
            if (i + RTFTABTAG.Length < rtf.Length)
            {
                if (rtf.Substring(i, RTFTABTAG.Length).Equals(RTFTABTAG, StringComparison.Ordinal))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rtf"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool IsRTFSlash(string rtf, int pos)
        {
            const string ESCAPEDSLASH = "\\\\";
            if (pos + ESCAPEDSLASH.Length > rtf.Length)
            {
                return false;
            }

            if (!rtf.Substring(pos, ESCAPEDSLASH.Length).Equals(ESCAPEDSLASH, StringComparison.Ordinal))
            { 
                return false;
            }

            int slashesbeforepos = 0;
            for (int i = pos; rtf[i] == '\\' && i >= 0; --i)
            {
                slashesbeforepos++;
            }

            if (slashesbeforepos != 0 && slashesbeforepos % 2 != 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Find the start of the RTF document content.
        /// </summary>
        /// <param name="rtf">The rtf stream.</param>
        /// <returns>The position after viewkind rtftag in the rtf stream.</returns>
        private int FindStartPosDoc(string rtf)
        {
            int posstartbody = rtf.IndexOf(VIEWKINDTAG);
            if (posstartbody < 0)
            {
                if (rtf.StartsWith(RTF1DOCTAG))
                {
                    posstartbody = RTF1DOCTAG.Length;
                }
                else
                {
                    Log.Write(LogType.exception, "RTF content body not found.");
                    return 0;
                }
            }
            else
            {
                posstartbody += VIEWKINDTAG.Length;
            }

            return posstartbody;
        }

        /// <summary>
        /// Create a colortable in the RTF code stream.
        /// </summary>
        /// <param name="newrtf">The current new RTF code.</param>
        /// <param name="color">The color to add to the color table RTF stream.</param>
        /// <returns>StringBuilder of colortbl.</returns>
        private StringBuilder CreateColortbl(StringBuilder newrtf, Color color)
        {
            const string ENDFONTTBL = "}}\r\n";
            string rtfcolorvalue = "\\red" + color.R + "\\green" + color.G + "\\blue" + color.B + ";";
            int posendfonttbl = newrtf.ToString().IndexOf(ENDFONTTBL);
            newrtf.Insert(posendfonttbl + ENDFONTTBL.Length, COLORTBLTAG + ";" + rtfcolorvalue + "}\r\n");
            return newrtf;
        }

        /// <summary>
        /// Check if rtf contains a new level or depth document level has been gone up.
        /// Escaped { and } characters are not a new RTF depth level.
        /// </summary>
        /// <param name="rtf">The rtf stream.</param>
        /// <param name="pos">The position in RTF</param>
        /// <param name="rtfcurrentnestedlevel">Current RTF depth level.</param>
        /// <returns>New RTF depth level.</returns>
        private int CheckRTFNestedLevel(string rtf, int pos, int rtfcurrentnestedlevel)
        {
            if (rtf[pos] == '{')
            {
                if (pos - 1 >= 0)
                {
                    if (rtf[pos - 1] != '\\')
                    {
                        if (rtfcurrentnestedlevel < int.MaxValue)
                        {
                            rtfcurrentnestedlevel++;
                        }
                    }
                }
                else
                {
                    if (rtfcurrentnestedlevel < int.MaxValue)
                    {
                        rtfcurrentnestedlevel++;
                    }
                }
            }
            else if (rtf[pos] == '}')
            {
                if (pos - 1 >= 0)
                {
                    if (rtf[pos - 1] != '\\')
                    {
                        if (rtfcurrentnestedlevel > 0)
                        {
                            rtfcurrentnestedlevel--;
                        }
                    }
                }
                else
                {
                    if (rtfcurrentnestedlevel > 0)
                    {
                        rtfcurrentnestedlevel--;
                    }
                }
            }

            return rtfcurrentnestedlevel;
        }

        /// <summary>
        /// Is the RTF part at current position a unicode character in RTF escaping.
        /// </summary>
        /// <param name="rtf">The rtf stream.</param>
        /// <param name="pos">The RTF position in where the special character starts</param>
        /// <returns>True if there is a special character RTF code at position i.</returns>
        private bool IsRTFUnicodeChar(string rtf, int pos)
        {
            const int RTFUNICODECHARMINLEN = 5; // \u519?
            if (pos + RTFUNICODECHARMINLEN > rtf.Length)
            {
                return false;
            }

            bool isunicodechar = false;
            if (rtf.Substring(pos, 2).Equals(@"\u", StringComparison.Ordinal) && rtf[pos + 2] != 'c')
            {
                // An 16 bits unicode character and NOT RTF unicode control character e.g. \uc1.
                // \uc1 keyword represents the number of bytes(1) corresponding to a given \uN Unicode character. 
                isunicodechar = true;
                int i = 2;
                char rtfchar = rtf[pos + i];
                while (i < RTFUNICODECHARMINLEN || rtfchar != '?')
                {
                    if (!Char.IsDigit(rtfchar))
                    {
                        Log.Write(LogType.info, "rtfchar not digit.");
                        isunicodechar = false;
                        break;
                    }

                    ++i;
                    rtfchar = rtf[pos + i];
                }
            }

            return isunicodechar;
        }

        /// <summary>
        /// Is the RTF part at current position a codepoint in RTF escaping.
        /// </summary>
        /// <param name="rtf"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool IsRTFCodepointChar(string rtf, int pos)
        {
            const int RTFCODEPOINTMAXHEXLEN = 4;
            if (pos + RTFCODEPOINTMAXHEXLEN > rtf.Length)
            {
                return false;
            }

            bool isrtfcodepoint = false;
            if (rtf.Substring(pos, 2).Equals(@"\'", StringComparison.Ordinal))
            {
                // An 8 bits special character.
                isrtfcodepoint = true;
                for (int n = 2; n < 4; ++n)
                {
                    string c = rtf[pos + n].ToString();
                    if (c.IndexOfAny(this.hexchars) < 0)
                    {
                        isrtfcodepoint = false;
                        break;
                    }
                }
            }

            return isrtfcodepoint;
        }


        /// <summary>
        /// Converts a string to integer, optimization.
        /// </summary>
        /// <param name="value">An nummeric string.</param>
        /// <returns>An integer.</returns>
        private int IntParseFast(string value)
        {
            int result = 0;
            int length = value.Length;
            for (int i = 0; i < length; ++i)
            {
                result = (10 * result) + (value[i] - 48);
            }

            return result;
        }

        /// <summary>
        /// Get the position of a color in the current colortbl.
        /// </summary>
        /// <param name="rtf">The rtf stream.</param>
        /// <param name="clr">Color object</param>
        /// <returns>Returns -1 if coloritem in colortbl is not found.</returns>
        private int GetNrcoloritem(string rtf, Color clr)
        {
            for (int i = 0; i < this.colortblitems.Count; ++i)
            {
                if (this.colortblitems[i].R == clr.R && this.colortblitems[i].G == clr.G && this.colortblitems[i].B == clr.B)
                {
                    return i + 1;
                }
            }

            return -1;
        }

        /// <summary>
        /// Parser a string with a coloritem from the colortbl in the RTF stream as a color object.
        /// </summary>
        /// <param name="colortblraw">An string with a coloritem for the RTF colortbl.</param>
        /// <returns>An color object.</returns>
        private Color ParserColorItem(string colortblraw)
        {
            const string REDPROPERTIE = "red";
            const string GREENPROPERTIE = "green";
            const string BLUEPROPERTIE = "blue";
            int red = 0;
            int green = 0;
            int blue = 0;
            string[] colorprop = colortblraw.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            for (int n = 0; n < colorprop.Length; ++n)
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
        /// Parser the colortbl in the RTF stream at position posstartcolortbl. 
        /// Put all coloritems in the list colortblitems.
        /// </summary>
        /// <param name="rtf">The rtf stream.</param>
        /// <param name="posstartcolortbl">Position of where the colortbl starts.</param>
        private void ParserColorTbl(string rtf, int posstartcolortbl)
        {
            this.colortblitems.Clear();
            int startcoloritem = posstartcolortbl + COLORTBLTAG.Length + 1; // +1 for space
            for (int i = startcoloritem; rtf[i] != '}'; ++i)
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
        /// <param name="sbrtfnew">The RTF stream.</param>
        /// <param name="posstartcolortbl">The position of start of \colortbl in the RTF stream.</param>
        /// <param name="newclr">The new color to add to the colortbl.</param>
        /// <returns>The new RTF stream with the color added to the colortbl.</returns>
        private StringBuilder AddColorItem(StringBuilder sbrtfnew, int posstartcolortbl, Color newclr)
        {
            string valeau = "\\red" + newclr.R + "\\green" + newclr.G + "\\blue" + newclr.B + ";";
            int p = posstartcolortbl;
            while (sbrtfnew[p] != '}' && p < sbrtfnew.Length)
            {
                p++;
            }

            sbrtfnew.Insert(p, valeau);
            this.colortblitems.Add(newclr);
            return sbrtfnew;
        }

        /// <summary>
        /// Find the start position of the colortbl in the RTF stream.
        /// </summary>
        /// <param name="rtf">The rtf stream.</param>
        /// <returns>The position in the RTF.</returns>
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
