//-----------------------------------------------------------------------
// <copyright file="RTFDirectEditTest.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2012-2013  Tom
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
namespace NoteFlyTests
{
    using System.Drawing;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NoteFly;
    
    /// <summary>
    /// This is a test class for RTFDirectEditTest and is intended
    /// to contain all RTFDirectEditTest Unit Tests
    /// </summary>
    [TestClass()]
    public class RTFDirectEditTest
    {
        /// <summary>
        /// The current rtf stream
        /// </summary>
        private string rtf;

        /// <summary>
        /// Reference to the RTFDirectEdit class
        /// </summary>
        private RTFDirectEdit rtfdirectedit = new RTFDirectEdit();

        /// <summary>
        /// run code before running each test 
        /// </summary>
        [TestInitialize()]
        public void RTFDirextEditTestInitialize()
        {
            this.rtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 testtesttest\par
}
";
        }

        /// <summary>
        /// subject  : AddBoldTagInRTF
        /// scenario : AddBoldMiddle
        /// result   : MiddleBold
        /// </summary>
        [TestMethod]
        public void AddBoldTagInRTF_AddBoldMiddle_MiddleBold()
        {
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 test\b test\b0 test\par
}
";
            this.rtf = this.rtfdirectedit.AddBoldTagInRTF(this.rtf, 4, 4);
            Assert.AreEqual(expectedrtf, this.rtf, "failed on: AddBoldTagInRTF(rtf, 4, 4)");
        }

        /// <summary>
        /// subject  : AddBoldTagInRTF
        /// scenario : AddAllBold
        /// result   : AllBold
        /// </summary>
        [TestMethod]
        public void AddBoldTagInRTF_AddAllBold_AllBold()
        {
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 \b testtesttest\b0\par
}
";
            this.rtf = this.rtfdirectedit.AddBoldTagInRTF(this.rtf, 0, 12);
            Assert.AreEqual(expectedrtf, this.rtf, "failed on: AddBoldTagInRTF(rtf, 0, 12)");
        }

        /// <summary>
        /// subject  : AddBoldTagInRTFT
        /// scenario : AddEvenPositionLettersBold
        /// result   : EvenBoldOddNormal
        /// </summary>
        [TestMethod]
        public void AddBoldTagInRTFT_AddEvenPositionLettersBold_EvenBoldOddNormal()
        {
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 \b t\b0 e\b s\b0 t\b t\b0 e\b s\b0 t\b t\b0 e\b s\b0 t\par
}
";
            this.rtf = this.rtfdirectedit.AddBoldTagInRTF(this.rtf, 0, 1);
            this.rtf = this.rtfdirectedit.AddBoldTagInRTF(this.rtf, 2, 1);
            this.rtf = this.rtfdirectedit.AddBoldTagInRTF(this.rtf, 4, 1);
            this.rtf = this.rtfdirectedit.AddBoldTagInRTF(this.rtf, 6, 1);
            this.rtf = this.rtfdirectedit.AddBoldTagInRTF(this.rtf, 8, 1);
            this.rtf = this.rtfdirectedit.AddBoldTagInRTF(this.rtf, 10, 1);
            Assert.AreEqual(expectedrtf, rtf, "failed on: AddBoldTagInRTF(rtf, 0, 1), AddBoldTagInRTF(rtf, 2, 1), AddBoldTagInRTF(rtf, 4, 1), AddBoldTagInRTF(rtf, 6, 1), AddBoldTagInRTF(rtf, 8, 1 and AddBoldTagInRTF(rtf, 10, 1)");
        }

        /// <summary>
        /// subject  : AddBoldTagInRTF
        /// scenario : AddPairBold
        /// result   : PairBoldPairNormal
        /// </summary>
        [TestMethod]
        public void AddBoldTagInRTF_AddPairBold_PairBoldPairNormal()
        {
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 \b te\b0 st\b te\b0 st\b te\b0 st\par
}
";
            this.rtf = this.rtfdirectedit.AddBoldTagInRTF(this.rtf, 0, 2);
            this.rtf = this.rtfdirectedit.AddBoldTagInRTF(this.rtf, 4, 2);
            this.rtf = this.rtfdirectedit.AddBoldTagInRTF(this.rtf, 8, 2);
            Assert.AreEqual(expectedrtf, this.rtf, "failed on: AddBoldTagInRTF(rtf, 0, 2), AddBoldTagInRTF(rtf, 4, 2) and AddBoldTagInRTF(rtf, 8, 2)");
        }

        /// <summary>
        /// subject  : AddItalicTagInRTF
        /// scenario : AddMiddleItalic
        /// result   : MiddleItalic
        /// </summary>
        [TestMethod]
        public void AddItalicTagInRTF_AddMiddleItalic_MiddleItalic()
        {
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 test\i test\i0 test\par
}
";
            this.rtf = this.rtfdirectedit.AddItalicTagInRTF(this.rtf, 4, 4);
            Assert.AreEqual(expectedrtf, this.rtf, "failed on: AddItalicTagInRTF(rtf, 4, 4)");
        }

        /// <summary>
        /// subject  : AddItalicTagInRTF
        /// scenario : AddAllItalic
        /// result   : AllItalic
        /// </summary>
        [TestMethod]
        public void AddItalicTagInRTF_AddAllItalic_AllItalic()
        {
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 \i testtesttest\i0\par
}
";
            this.rtf = this.rtfdirectedit.AddItalicTagInRTF(this.rtf, 0, 12);
            Assert.AreEqual(expectedrtf, this.rtf, "failed on: AddItalicTagInRTF(rtf, 0, 12)");
        }

        /// <summary>
        /// subject  : AddStrikeTagInRTFTest
        /// scenario : AddMiddleStriketrough
        /// result   : MiddleStriketrough
        /// </summary>
        [TestMethod]
        public void AddStrikeTagInRTFTest_AddMiddleStriketrough_Striketroughmiddle()
        {
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 test\strike test\strike0 test\par
}
";
            this.rtf = this.rtfdirectedit.AddStrikeTagInRTF(this.rtf, 4, 4);
            Assert.AreEqual(expectedrtf, this.rtf, "failed on: AddStrikeTagInRTF(rtf, 4, 4)");
        }

        /// <summary>
        /// subject  : AddStrikeTagInRTFTest
        /// scenario : AddAllStriketrough
        /// result   : AllStriketrough
        /// </summary>
        [TestMethod]
        public void AddStrikeTagInRTF_AddAllStriketrough_AllStriketrough()
        {
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 \strike testtesttest\strike0\par
}
";
            this.rtf = this.rtfdirectedit.AddStrikeTagInRTF(this.rtf, 0, 12);
            Assert.AreEqual(expectedrtf, this.rtf, "failed on: AddStrikeTagInRTF(rtf, 0, 12)");
        }

        /// <summary>
        /// subject  : AddUnderlineTagInRTF
        /// scenario : AddMiddleUnderline
        /// result   : MiddleUnderline
        /// </summary>
        [TestMethod]
        public void AddUnderlineTagInRTF_AddMiddleUnderline_MiddleUnderline()
        {
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 test\ul test\ulnone test\par
}
";
            this.rtf = this.rtfdirectedit.AddUnderlineTagInRTF(this.rtf, 4, 4);
            Assert.AreEqual(expectedrtf, this.rtf, "failed on: AddUnderlineTagInRTF(rtf, 4, 4)");
        }

        /// <summary>
        /// subject  : AddUnderlineTagInRTF
        /// scenario : AddAllUnderline
        /// result   : AllUnderline
        /// </summary>
        [TestMethod]
        public void AddUnderlineTagInRTF_AddAllUnderline_AllUnderline()
        {
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 \ul testtesttest\ulnone\par
}
";
            this.rtf = this.rtfdirectedit.AddUnderlineTagInRTF(this.rtf, 0, 12);
            Assert.AreEqual(expectedrtf, this.rtf, "failed on: AddUnderlineTagInRTF(rtf, 0, 12)");
        }

        /// <summary>
        /// subject  : SetColorInRTF
        /// scenario : AddMiddleBlue
        /// result   : MiddleBlue
        /// </summary>
        [TestMethod]
        public void SetColorInRTFTest_middleblue()
        {
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
{\colortbl ;\red0\green0\blue255;}
\viewkind4\uc1\pard\fs24 test\cf1 test\cf1 test\par
}
";
            this.rtf = this.rtfdirectedit.SetColorInRTF(this.rtf, Color.Blue, 4, 4);
            Assert.AreEqual(expectedrtf, this.rtf, "failed on: SetColorInRTF(rtf, Color.Blue, 4, 4)");
        }

        /// <summary>
        /// subject  : SetColorAllRTF
        /// scenario : AddAllGreen
        /// result   : AllGreen
        /// </summary>
        [TestMethod]
        public void SetColorAllRTF_AddAllGreen_AllGreen()
        {
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
{\colortbl ;\red0\green128\blue0;}
\viewkind4\cf1 \uc1\pard\fs24 testtesttest\cf1 \par
}
";
            this.rtf = this.rtfdirectedit.SetColorAllRTF(this.rtf, Color.Green, 12);
            Assert.AreEqual(expectedrtf, this.rtf, "failed on: SetColorAllRTF(rtf, Color.Green, 12)");
        }

        /// <summary>
        /// subject  : SetColorInRTF
        /// scenario : AddBlueAroundLinebreak
        /// result   : MiddleBlue
        /// </summary>
        public void SetColorInRTF_AddBlueAroundLinebreak_MiddleBlue()
        {
            this.rtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1043{\fonttbl{\f0\fnil\fcharset0 Arial;}}
{\colortbl ;\red0\green0\blue0;}
{\*\generator Msftedit 5.41.21.2510;}\viewkind4\uc1\pard\cf1\fs24 testtesttestktest\par
testtesttesttest\par
}
";
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1043{\fonttbl{\f0\fnil\fcharset0 Arial;}}
{\colortbl ;\red0\green0\blue0;\red0\green0\blue255;}
{\*\generator Msftedit 5.41.21.2510;}\viewkind4\uc1\pard\cf1\fs24 testtesttest\cf2 test\par
testtest\cf1 testtest\par
}
";
            this.rtf = this.rtfdirectedit.SetColorInRTF(this.rtf, Color.Blue, 12, 12);
            Assert.AreEqual(expectedrtf, this.rtf, "failed on: SetColorAllRTF(rtf, Color.Green, 12)");
        }

        /// <summary>
        /// subject  : RemoveItalicTagsInRTF
        /// scenario : AddItalicRemoveMiddlePartItalic
        /// result   : PartsItalic
        /// </summary>
        [TestMethod]
        public void RemoveItalicTagsInRTF_AddItalicRemoveMiddlePartItalic_PartsItalic()
        {
            string expectedrtf1 = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 te\i sttestte\i0 st\par
}
";
            this.rtf = this.rtfdirectedit.AddItalicTagInRTF(this.rtf, 2, 8); // italic: ..sttestte..
            Assert.AreEqual(expectedrtf1, this.rtf, "failed on: AddItalicTagInRTF(rtf, 2, 8)");

            string expectedrtf2 = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 te\i st\i0 test\i te\i0 st\par
}
";
            this.rtf = this.rtfdirectedit.RemoveItalicTagsInRTF(this.rtf, 4, 4);
            Assert.AreEqual(expectedrtf2, this.rtf, "failed on: RemoveItalicTagsInRTF(rtf, 4, 4)");
        }

        /// <summary>
        /// subject  : RemoveBoldTagsInRTF
        /// scenario : AddBoldRemoveMiddlePartBold
        /// result   : PartsBold
        /// </summary>
        [TestMethod]
        public void RemoveBoldTagsInRTF_AddBoldRemoveMiddlePartBold_PartsBold()
        {
            string expectedrtf1 = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 test\b test\b0 test\par
}
";
            this.rtf = this.rtfdirectedit.AddBoldTagInRTF(this.rtf, 4, 4);
            Assert.AreEqual(expectedrtf1, this.rtf, "failed on: AddBoldTagInRTF(rtf, 4, 4)");

            string expectedrtf2 = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 testtesttest\par
}
";
            this.rtf = this.rtfdirectedit.RemoveBoldTagsInRTF(this.rtf, 4, 4);
            Assert.AreEqual(expectedrtf2, this.rtf, "failed on: RemoveBoldTagsInRTF(rtf, 4, 4)");
        }

        /// <summary>
        /// subject  : RemoveBoldTagsInRTF
        /// scenario : RemoveBoldEnd
        /// result   : EndNoBold
        /// </summary>
        [TestMethod]
        public void RemoveBoldTagsInRTF_RemoveBoldEnd_EndNoBold()
        {
            this.rtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 \b testtesttest\b0\par
}
";
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 \b testtestt\b0 est\par
}
";
            this.rtf = this.rtfdirectedit.RemoveBoldTagsInRTF(rtf, 9, 3); // now only testtestte.. should be bold.
            Assert.AreEqual(expectedrtf, rtf, "failed on: RemoveBoldTagsInRTF(rtf, 9, 3)");
        }

        /// <summary>
        /// subject  : RemoveBoldTagsInRTF
        /// scenario : RemoveBoldAlmostEnd
        /// result   : NoBoldAlmostEnd
        /// </summary>
        [TestMethod]
        public void RemoveBoldTagsInRTF_RemoveBoldAlmostEnd_NoBoldAlmostEnd()
        {
            this.rtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 \b testtesttest\b0\par
}
";
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 \b testtestt\b0 es\b t\b0\par
}
";
            this.rtf = this.rtfdirectedit.RemoveBoldTagsInRTF(rtf, 9, 2); // now only testtestt..t should be bold.
            Assert.AreEqual(expectedrtf, rtf, "failed on: RemoveBoldTagsInRTF(rtf, 9, 2)");
        }

        /// <summary>
        /// subject  : AddUnderlineTagInRTF
        /// scenario : RemoveUnderlineEnd
        /// result   : NoUnderlineEnd
        /// </summary>
        [TestMethod]
        public void AddUnderlineTagInRTF_RemoveUnderlineEnd_NoUnderlineEnd()
        {
            this.rtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 \b testtesttest\b0\par
}
";
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 \b testtest\ul test\ulnone\b0\par
}
";
            this.rtf = this.rtfdirectedit.AddUnderlineTagInRTF(rtf, 8, 4);
            Assert.AreEqual(expectedrtf, rtf, "failed on: AddUnderlineTagInRTF(rtf, 8, 4)");
        }

        /// <summary>
        /// integration test, no unit test.
        /// </summary>
        [TestMethod]
        public void AddBoldMixed()
        {
            this.rtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
{\colortbl ;\red0\green0\blue0;}
{\*\generator Msftedit 5.41.21.2510;}\viewkind4\uc1\pard\cf1\i\fs24 test\b test\ul test\b0 test\ulnone\i0\par
}
";
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
{\colortbl ;\red0\green0\blue0;}
{\*\generator Msftedit 5.41.21.2510;}\viewkind4\uc1\pard\cf1\i\fs24 test\b test\ul test\b0 test\ulnone\i0\par
}
";

            this.rtf = this.rtfdirectedit.AddBoldTagInRTF(rtf, 8, 4);
            Assert.AreEqual(expectedrtf, rtf, "failed on: AddBoldTagInRTF(rtf, 8, 4)");
        }

        /// <summary>
        /// integration test, no unit test.
        /// </summary>
        [TestMethod]
        public void RemoveBoldOfBoldAndItalic()
        {
            this.rtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
{\colortbl ;\red0\green0\blue0;}
{\*\generator Msftedit 5.41.21.2510;}\viewkind4\uc1\pard\cf1\i\fs24 test\b\i testtest\i0\b0 test\par
}
";
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
{\colortbl ;\red0\green0\blue0;}
{\*\generator Msftedit 5.41.21.2510;}\viewkind4\uc1\pard\cf1\i\fs24 test\i testtest\i0 test\par
}
";
            this.rtf = this.rtfdirectedit.RemoveBoldTagsInRTF(rtf, 4, 8);
            Assert.AreEqual(expectedrtf, rtf, "failed on: RemoveBoldTagsInRTF(rtf, 4, 8)");
        }

        /// <summary>
        /// integration test, no unit test.
        /// </summary>
        [TestMethod]
        public void UnderlineAndBold()
        {
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 \ul test\b test\ulnone test\b0\par
}
";
            this.rtf = this.rtfdirectedit.AddUnderlineTagInRTF(this.rtf, 0, 8);
            this.rtf = this.rtfdirectedit.AddBoldTagInRTF(this.rtf, 4, 8);
            Assert.AreEqual(expectedrtf, this.rtf, "failed on: AddUnderlineTagInRTF(rtf, 0, 8) AddBoldTagInRTF(rtf, 4, 8)");
        }

        /// <summary>
        /// integration test, no unit test.
        /// </summary>
        [TestMethod]
        public void BoldUnderlineAndBoldOff()
        {
            string expectedrtf1 = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 \b test\ul testte\ulnone st\b0\par
}
";
            this.rtf = this.rtfdirectedit.AddBoldTagInRTF(this.rtf, 0, 12);
            this.rtf = this.rtfdirectedit.AddUnderlineTagInRTF(this.rtf, 4, 6);
            Assert.AreEqual(expectedrtf1, this.rtf, "failed on: AddBoldTagInRTF(rtf, 0, 12) AddUnderlineTagInRTF(rtf, 4, 6)");

            string expectedrtf2 = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1043{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 \b te\b0 st\ul testte\ulnone st\par
}
";
            this.rtf = this.rtfdirectedit.RemoveBoldTagsInRTF(this.rtf, 2, 12);
            Assert.AreEqual(expectedrtf2, this.rtf, "failed on: RemoveBoldTagsInRTF(rtf, 2, 12)");

            string expectedrtf3 = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 \b te\b0 stte\ul stte\ulnone st\par
}
";
            this.rtf = this.rtfdirectedit.RemoveUnderlineTagsInRTF(this.rtf, 4, 2); // now only ......stte.. should be underlined.
            Assert.AreEqual(expectedrtf3, this.rtf, "failed on: RemoveUnderlineTagsInRTF(rtf, 4, 2)");
        }
    }
}
