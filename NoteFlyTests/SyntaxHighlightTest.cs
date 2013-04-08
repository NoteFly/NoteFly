//-----------------------------------------------------------------------
// <copyright file="SyntaxHighlightTest.cs" company="NoteFly">
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
    using NoteFly;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Windows.Forms;
    
    /// <summary>
    /// This is a test class for SyntaxHighlightTest and is intended
    /// to contain all SyntaxHighlightTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SyntaxHighlightTest
    {
        /// <summary>
        /// Run code before running the first test in the class.
        /// </summary>
        /// <param name="testContext"></param>
        [ClassInitialize()]
        public static void SyntaxHighlighTestInitialize(TestContext testContext)
        {
            SyntaxHighlight.InitHighlighter();
        }
        
        /// <summary>
        /// Run code after all tests in a class have run.
        /// </summary>
        [ClassCleanup()]
        public static void SyntaxHighlighTestCleanup()
        {
            SyntaxHighlight.DeinitHighlighter();
        }

        /// <summary>
        /// Test syntax highlighting with CheckSyntaxFull of a PHP for loop with echo 'test'; statement.
        /// </summary>
        [TestMethod()]
        public void PHPForLoopAndEchoTest()
        {
            RichTextBox rtb = new RichTextBox();

            // input
            rtb.Rtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Arial;}}
{\colortbl ;\red255\green223\blue35;}
\viewkind4\uc1\pard\cf1\fs24 &lt;?php\par
for ($i = 0; $i &lt; 10; $i++) \{\par
echo 'test';\par
\}\par
\par
?&gt;\par
}";

            string exceptedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang1043{\fonttbl{\f0\fnil\fcharset0 Arial;}}
{\colortbl ;\red0\green0\blue0;\red65\green216\blue123;\red18\green150\blue18;\red64\green64\blue64;}
\viewkind4\uc1\pard\cf1\fs24 &lt;?php\par
\cf2 for\cf1  (\cf3 $i\cf1  = 0; \cf3 $i\cf1  &lt; 10; \cf3 $i\cf1 ++) \{\par
\cf2 echo\cf1 \cf4 'test'\cf1 ;\par
\}\par
\par
?&gt;\par
}";
            int skinnr = 1;
            Settings.HighlightPHP = true;
            Notes notes = new Notes(false);
            SyntaxHighlight.InitHighlighter();
            SyntaxHighlight.CheckSyntaxFull(rtb, skinnr, notes);
            Assert.AreEqual(exceptedrtf, rtb.Rtf, "PHPForLoopAndEchoTest failed.");
        }
    }
}
