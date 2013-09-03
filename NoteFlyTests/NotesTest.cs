//-----------------------------------------------------------------------
// <copyright file="NotesTest.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2010-2013  Tom
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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NoteFly;
    
    /// <summary>
    /// This is a test class for Notes and is intended
    /// to contain all Notes Unit Tests
    /// </summary>
    [TestClass]
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
        /// <param name="testContext">The testContext</param>
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            Settings.NotesWarnlimitVisible = 1000;
            ////Settings.ProgramFirstrunned = true;
        }
        #endregion

        /// <summary>
        /// A test for StripForbiddenFilenameChars
        /// </summary>
        [TestMethod]
        public void StripForbiddenFilenameCharsTest()
        {
            Notes notes = new Notes(false);
            string orgname = "a?b<c>d:e*|f\\g/h";
            string expectedfilename = "abcdefgh";
            string actualfilename = notes.StripForbiddenFilenameChars(orgname);
            Assert.AreEqual(expectedfilename, actualfilename);
        }

        /// <summary>
        /// A test for RemoveNote
        /// </summary>
        [TestMethod]
        public void RemoveNoteTest()
        {
            Notes notes = new Notes(false);
            notes.AddNoteDefaultSettings("test note", 1, 10, 10, 200, 300, "test", true);
            int notelastpos = notes.CountNotes;
            notes.RemoveNote(notelastpos - 1);
            int exceptedcountnotesnow = notelastpos - 1;
            if (notes.CountNotes != exceptedcountnotesnow)
            {
                Assert.Fail("Number of notes: " + notes.CountNotes + ", different then excepted: " + exceptedcountnotesnow);
            }
        }

        /// <summary>
        /// A test for GetSkinsNames
        /// Only passes for default unmodified skins.xml file.
        /// </summary>
        [TestMethod]
        public void GetSkinsNamesTest()
        {
            Notes notes = new Notes(false);
            string[] expected = new string[13];
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
            expected[10] = "grass";
            expected[11] = "colordrops";
            expected[12] = "nyancat";
            string[] actualskins = notes.GetSkinsNames();
            if (actualskins.Length == 0)
            {
                Assert.Fail("Could not get skin names.");
            }

            if (actualskins.Length > expected.Length)
            {
                Assert.Inconclusive("Skins.xml can be modified or test failed because more skins are returned.");
            }

            for (int i = 0; i < actualskins.Length; i++)
            {
                if (expected[i].ToString() != actualskins[i].ToString())
                {
                    Assert.Fail("GetSkinsNamesTest did not produce the excepted result. excepted skin name: " + expected[i].ToString() + " actual skin name: " + actualskins[i].ToString());
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
            Notes notes = new Notes(false);
            int skinnr = 0;
            string hexforegroundcolor = "#FFEF14";
            Color expectedprimarycolor = System.Drawing.ColorTranslator.FromHtml(hexforegroundcolor);
            Color actualprimarycolor = notes.GetPrimaryClr(skinnr);
            Assert.AreEqual(expectedprimarycolor, actualprimarycolor);
        }

        /// <summary>
        /// A test for GetBackgroundColor
        /// </summary>
        [TestMethod]
        public void GetSelectColorTest()
        {
            Notes notes = new Notes(false);
            int skinnr = 0;
            string hexbackgroundcolor = "#E0D616";
            Color expectedselectcolor = System.Drawing.ColorTranslator.FromHtml(hexbackgroundcolor);
            Color actualselectcolor = notes.GetSelectClr(skinnr);
            Assert.AreEqual(expectedselectcolor, actualselectcolor);
        }

        /// <summary>
        /// A test for GetHighlightColor
        /// </summary>
        [TestMethod]
        public void GetHighlightColorTest()
        {
            Notes notes = new Notes(false);
            int skinnr = 0; // yellow skin
            string hexhighlightcolor = "#FFED7C";
            Color expected = System.Drawing.ColorTranslator.FromHtml(hexhighlightcolor);
            Color actual = notes.GetHighlightClr(skinnr);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for CreateNote
        /// </summary>
        [TestMethod]
        public void AddNoteDefaultSettingsTest()
        {
            Notes target = new Notes(false);
            string title = "123456789abc";
            int skinnr = 0;
            int x = 400;
            int y = 300;
            int width = 200;
            int height = 100;
            string content = "some content";
            bool wordwarp = true;
            target.AddNoteDefaultSettings(title, skinnr, x, y, width, height, content, wordwarp);
            Note actualnote = target.GetNote(target.CountNotes - 1);
            Assert.IsNotNull(actualnote, "CreateNoteTest failed to create Note object.");
            Assert.AreEqual(title, actualnote.Title, "Note title not good.");
            Assert.AreEqual(x, actualnote.X, "Wrong X coordinates");
            Assert.AreEqual(y, actualnote.Y, "Wrong Y coordinates");
            Assert.AreEqual(width, actualnote.Width, "Wrong width size");
            Assert.AreEqual(height, actualnote.Height, "Wrong height size");
            Assert.AreEqual(wordwarp, actualnote.Wordwarp, "Note wordwarp differs.");
        }

        /// <summary>
        /// A test for CreateNote
        /// </summary>
        [TestMethod]
        public void GenerateRandomSkinnrTest()
        {
            Notes notes = new Notes(false);
            int rndskinnr = notes.GenerateRandomSkinnr();
            if (rndskinnr < 0)
            {
                Assert.Fail("Can't smaller than zero.");
            }

            if (rndskinnr > notes.GetSkinsNames().Length)
            {
                Assert.Fail("Can't choice a skin outside the range. (or if GetSkinsNamesTest failed then this one fails too.)");
            }
        }

        /// <summary>
        /// Test GetBoolSetting method.
        /// </summary>
        [TestMethod]
        public void GetBoolSettingTest()
        {
            Notes notes = new Notes(false);
            bool exceptedsetting = true;
            Settings.ConfirmExit = exceptedsetting;
            bool actualsetting = notes.GetSettingBool("ConfirmExit");
            Assert.AreEqual(exceptedsetting, actualsetting, "GetBoolSetting(\"ConfirmExit\") failed");
        }

        /// <summary>
        /// Test GetIntSetting method.
        /// </summary>
        [TestMethod]
        public void GetIntSettingTest()
        {
            Notes notes = new Notes(false);
            int exceptedsetting = 1;
            Settings.ManagenotesSkinnr = exceptedsetting;
            int actualsetting = notes.GetSettingInt("ManagenotesSkinnr");
            Assert.AreEqual(exceptedsetting, actualsetting, "GetIntSetting(\"ManagenotesSkinnr\") failed.");
        }

        /// <summary>
        /// Test GetStringSetting method.
        /// </summary>
        public void GetStringSettingTest()
        {
            Notes notes = new Notes(false);
            string exceptedsetting = "#B16DFF";
            Settings.HighlightSQLColorField = exceptedsetting;
            string actualsetting = notes.GetSettingString("HighlightSQLColorField");
            Assert.AreEqual(exceptedsetting, actualsetting, "GetStringSetting(\"HighlightSQLColorField\") failed.");
        }
    }
}
