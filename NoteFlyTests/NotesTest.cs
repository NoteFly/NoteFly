//-----------------------------------------------------------------------
// <copyright file="Notes.cs" company="GNU">
//  NoteFly a note application.
//  Copyright (C) 2010-2011  Tom
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
    using System;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NoteFly;
    
    /// <summary>
    /// This is a test class for NotesTest and is intended
    /// to contain all NotesTest Unit Tests
    /// </summary>
    [TestClass()]
    public class NotesTest
    {
        /// <summary>
        /// The TestContext instance.
        /// </summary>
        private TestContext testContextInstance;

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return this.testContextInstance;
            }

            set
            {
                this.testContextInstance = value;
            }
        }

        #region Additional test attributes
        // You can use the following additional attributes as you write your tests:

        /// <summary>
        /// ClassInitialize to run code before running the first test in the class
        /// </summary>
        /// <param name="testContext">TestContext</param>
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            Settings.NotesWarnLimit = 1000;
            Settings.NotesSavepath = Program.AppDataFolder;
            Settings.ProgramFirstrun = true;
        }
        #endregion

        /// <summary>
        /// A test for StripForbiddenFilenameChars
        /// </summary>
        [TestMethod]
        public void StripForbiddenFilenameCharsTest()
        {
            Notes target = new Notes(false);
            string orgname = "a?b<c>d:e*|f\\g/h";
            string expected = "abcdefgh";
            string actual = target.StripForbiddenFilenameChars(orgname);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for RemoveNote
        /// </summary>
        [TestMethod]
        public void RemoveNoteTest()
        {
            Notes target = new Notes(false);
            Note newnote = target.CreateNote("test note", 1, 90, 90, 100, 100);
            target.AddNote(newnote);
            int notelastpos = target.CountNotes;
            target.RemoveNote(notelastpos - 1);
            int exceptedcountnotesnow = notelastpos - 1;
            if (target.CountNotes != exceptedcountnotesnow)
            {
                Assert.Fail("Number of notes: " + target.CountNotes + ", different then excepted: " + exceptedcountnotesnow);
            }
        }

        /// <summary>
        /// A test for GetSkinsNames
        /// Only passes for default unmodified skins.xml file.
        /// </summary>
        [TestMethod]
        public void GetSkinsNamesTest()
        {
            Notes target = new Notes(false);
            string[] expected = new string[10];
            expected[0] = "yellow";
            expected[1] = "orange";
            expected[2] = "white";
            expected[3] = "green";
            expected[4] = "blue";
            expected[5] = "purple";
            expected[6] = "red";
            expected[7] = "dark";
            expected[8] = "softwhite";
            expected[9] = "contrastblue";
            string[] actual = target.GetSkinsNames();
            if (actual.Length == 0)
            {
                Assert.Fail("Could not get skin names.");
            }

            if (actual.Length > expected.Length)
            {
                Assert.Inconclusive("Skins.xml can be modified or test failed because more skins are returned.");
            }

            for (int i = 0; i < actual.Length; i++)
            {
                if (expected[i].ToString() != actual[i].ToString())
                {
                    Assert.Fail("GetSkinsNamesTest did not produce the excepted result.");
                }
            }
        }

        /// <summary>
        /// A test for GetSkinName
        /// </summary>
        [TestMethod]
        public void GetSkinNameTest()
        {
            Notes target = new Notes(false);
            int skinnr = 0;
            string expected = "yellow";
            string actual = target.GetSkinName(skinnr);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for GetForegroundColor
        /// </summary>
        [TestMethod]
        public void GetPrimaryColorTest()
        {
            Notes target = new Notes(false);
            int skinnr = 0;
            string hexforegroundcolor = "#FFEF14";
            Color expected = System.Drawing.ColorTranslator.FromHtml(hexforegroundcolor);
            Color actual;
            actual = target.GetPrimaryClr(skinnr);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for GetBackgroundColor
        /// </summary>
        [TestMethod]
        public void GetSelectColorTest()
        {
            Notes target = new Notes(false);
            int skinnr = 0;
            string hexbackgroundcolor = "#E0D616"; // was #F7A90E
            Color expected = System.Drawing.ColorTranslator.FromHtml(hexbackgroundcolor);
            Color actual;
            actual = target.GetSelectClr(skinnr);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for GetHighlightColor
        /// </summary>
        [TestMethod]
        public void GetHighlightColorTest()
        {
            Notes target = new Notes(false);
            int skinnr = 0; // yellow skin
            string hexhighlightcolor = "#FFED7C";
            Color expected = System.Drawing.ColorTranslator.FromHtml(hexhighlightcolor);
            Color actual = target.GetHighlightClr(skinnr);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for CreateNote
        /// </summary>
        [TestMethod]
        public void CreateNoteTest()
        {
            Notes target = new Notes(false);
            string title = "123456789abc";
            int skinnr = 0;
            int x = 400;
            int y = 300;
            int width = 200;
            int height = 100;
            Note actual = target.CreateNote(title, skinnr, x, y, width, height);

            Assert.IsNotNull(actual, "CreateNoteTest failed to create Note object.");
            if (actual.Title != "123456789abc")
            {
                Assert.Fail("Note title not good.");
            }

            if (actual.X != 400 && actual.Y != 300)
            {
                Assert.Fail("Note has wrong X,Y coordinates.");
            }

            if (actual.Width != 200 && actual.Height != 100)
            {
                Assert.Fail("Note has wrong size");
            }
        }

        /// <summary>
        /// A test for CreateNote
        /// </summary>
        [TestMethod]
        public void GenerateRandomSkinnrTest()
        {
            Notes target = new Notes(false);
            int n = target.GenerateRandomSkinnr();
            if (n < 0)
            {
                Assert.Fail("Can't smaller than zero.");
            }

            if (n > target.GetSkinsNames().Length)
            {
                Assert.Fail("Can't choice a skin outside the range. (or if GetSkinsNamesTest failed then this one fails too.)");
            }
        }
    }
}
