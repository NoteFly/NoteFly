//-----------------------------------------------------------------------
// <copyright file="RTFDirectEditTest.cs" company="NoteFly">
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
namespace NoteFlyTests
{
    using NoteFly;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Text;
    using System.Drawing;
    
    /// <summary>
    ///This is a test class for RTFDirectEditTest and is intended
    ///to contain all RTFDirectEditTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RTFDirectEditTest
    {
        private string rtf;

        private RTFDirectEdit rtfdirectedit = new RTFDirectEdit();

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //    this.rtfdirectedit = new RTFDirectEdit();
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            rtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1043{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 testtesttest\par
}
";
        }
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        [TestMethod]
        public void AddBoldTagInRTFTest_middle()
        {
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1043{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 test\b test\b0 test\par
}
";
            rtf = rtfdirectedit.AddBoldTagInRTF(rtf, 4, 4);
            if (!expectedrtf.Equals(rtf, System.StringComparison.Ordinal))
            {
                Assert.Fail("not expected rtf");
            }
        }

        [TestMethod]
        public void AddBoldTagInRTFTest_all()
        {
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1043{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 \b testtesttest\b0\par
}
";
            rtf = rtfdirectedit.AddBoldTagInRTF(rtf, 0, 12);
            if (!expectedrtf.Equals(rtf, System.StringComparison.Ordinal))
            {
                Assert.Fail("not expected rtf");
            }
        }

        [TestMethod]
        public void AddBoldTagInRTFTest_mixed1()
        {
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1043{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 \b t\b0 e\b s\b0 t\b t\b0 e\b s\b0 t\b t\b0 e\b s\b0 t\par
}
";
            rtf = rtfdirectedit.AddBoldTagInRTF(rtf, 0, 1);
            rtf = rtfdirectedit.AddBoldTagInRTF(rtf, 2, 1);
            rtf = rtfdirectedit.AddBoldTagInRTF(rtf, 4, 1);
            rtf = rtfdirectedit.AddBoldTagInRTF(rtf, 6, 1);
            rtf = rtfdirectedit.AddBoldTagInRTF(rtf, 8, 1);
            rtf = rtfdirectedit.AddBoldTagInRTF(rtf, 10, 1);
            if (!expectedrtf.Equals(rtf, System.StringComparison.Ordinal))
            {
                Assert.Fail("not expected rtf");
            }
        }

        [TestMethod]
        public void AddBoldTagInRTFTest_mixed2()
        {
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1043{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 \b te\b0 st\b te\b0 st\b te\b0 st\par
}
";
            rtf = rtfdirectedit.AddBoldTagInRTF(rtf, 0, 2);
            rtf = rtfdirectedit.AddBoldTagInRTF(rtf, 4, 2);
            rtf = rtfdirectedit.AddBoldTagInRTF(rtf, 8, 2);
            if (!expectedrtf.Equals(rtf, System.StringComparison.Ordinal))
            {
                Assert.Fail("not expected rtf");
            }
        }

        [TestMethod]
        public void AddItalicTagInRTFTest_middle()
        {
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1043{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 test\i test\i0 test\par
}
";
            rtf = rtfdirectedit.AddItalicTagInRTF(rtf, 4, 4);
            if (!expectedrtf.Equals(rtf, System.StringComparison.Ordinal))
            {
                Assert.Fail("not expected rtf");
            }
        }

        [TestMethod]
        public void AddItalicTagInRTFTest_all()
        {
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1043{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 \i testtesttest\i0\par
}
";
            rtf = rtfdirectedit.AddItalicTagInRTF(rtf, 0, 12);
            if (!expectedrtf.Equals(rtf, System.StringComparison.Ordinal))
            {
                Assert.Fail("not expected rtf");
            }
        }

        [TestMethod]
        public void AddStrikeTagInRTFTest_middle()
        {
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1043{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 test\strike test\strike0 test\par
}
";
            rtf = rtfdirectedit.AddStrikeTagInRTF(rtf, 4, 4);
            if (!expectedrtf.Equals(rtf, System.StringComparison.Ordinal))
            {
                Assert.Fail("not expected rtf");
            }
        }

        [TestMethod]
        public void AddStrikeTagInRTFTest_all()
        {
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1043{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 \strike testtesttest\strike0\par
}
";
            rtf = rtfdirectedit.AddStrikeTagInRTF(rtf, 0, 12);
            if (!expectedrtf.Equals(rtf, System.StringComparison.Ordinal))
            {
                Assert.Fail("not expected rtf");
            }
        }

        [TestMethod]
        public void AddUnderlineTagInRTFTest_middle()
        {
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1043{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 test\ul test\ulnone test\par
}
";
            rtf = rtfdirectedit.AddUnderlineTagInRTF(rtf, 4, 4);
            if (!expectedrtf.Equals(rtf, System.StringComparison.Ordinal))
            {
                Assert.Fail("not expected rtf");
            }
        }

        [TestMethod]
        public void AddUnderlineTagInRTFTest_all()
        {
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1043{\fonttbl{\f0\fnil\fcharset0 Arial;}}
\viewkind4\uc1\pard\fs24 \ul testtesttest\ulnone\par
}
";
            rtf = rtfdirectedit.AddUnderlineTagInRTF(rtf, 0, 12);
            if (!expectedrtf.Equals(rtf, System.StringComparison.Ordinal))
            {
                Assert.Fail("not expected rtf");
            }
        }

        [TestMethod]
        public void SetColorInRTFTest_middleblue()
        {
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1043{\fonttbl{\f0\fnil\fcharset0 Arial;}}
{\colortbl ;\red0\green0\blue255;}
\viewkind4\uc1\pard\fs24 test\cf1 test\cf1 test\par
}
";
            rtf = rtfdirectedit.SetColorInRTF(rtf, Color.Blue, 4, 4);
            if (!expectedrtf.Equals(rtf, System.StringComparison.Ordinal))
            {
                Assert.Fail("not expected rtf");
            }
        }

        [TestMethod]
        public void SetColorAllRTFTest_green()
        {
            string expectedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1043{\fonttbl{\f0\fnil\fcharset0 Arial;}}
{\colortbl ;\red0\green128\blue0;}
\viewkind4\cf1 \uc1\pard\fs24 testtesttest\par
}
";
            rtf = rtfdirectedit.SetColorAllRTF(rtf, Color.Green);
            if (!expectedrtf.Equals(rtf, System.StringComparison.Ordinal))
            {
                Assert.Fail("not expected rtf");
            }
        }
    }
}
