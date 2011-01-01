using NoteFly;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using System.Text;
using System.IO;

namespace NoteFlyTests
{
    
    
    /// <summary>
    ///This is a test class for NotesTest and is intended
    ///to contain all NotesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NotesTest
    {


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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            Settings.NotesWarnLimit = 1000;
            Settings.NotesSavepath = TrayIcon.AppDataFolder;
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        
        #endregion

        /// <summary>
        ///A test for StripForbiddenFilenameChars
        ///</summary>
        [TestMethod()]
        public void StripForbiddenFilenameCharsTest()
        {
            Notes target = new Notes();
            string orgname = "a?b<c>d:e*|f\\g/h";
            string expected = "abcdefgh";
            string actual = target.StripForbiddenFilenameChars(orgname);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SaveNote
        ///</summary>
        [TestMethod()]
        public void SaveNoteTest()
        {
            Notes target = new Notes(); // TODO: Initialize to an appropriate value
            Note note = new Note(target);
            note.Title = "testnote";
            note.Id = 999;
            note.RolledUp = false;;
            note.X = 80;
            note.Y = 80;
            note.Height = 100;
            note.Width = 100;
            note.RolledUp = false;
            note.Ontop = false;
            note.Locked = false;
            string content = "this is a test note.";
            target.SaveNote(note, content);
            string notefilepath = target.NewNoteFilename(note.Id, note.Title);
            if (!File.Exists(notefilepath))
            {
                Assert.Fail("Note file doesnt exist.");
            }
            else
            {
                //test done, delete test note file.
                File.Delete(notefilepath);
            }
        }

        /// <summary>
        ///A test for RemoveNote
        ///</summary>
        [TestMethod()]
        public void RemoveNoteTest()
        {
            Notes_Accessor target = new Notes_Accessor();
            target.LoadNotes(false); //create at least demo note with firstrun.
            int noteid = 1;
            Note notebefore = target.GetNote(noteid);
            target.RemoveNote(noteid);
            if (target.notes.Contains(notebefore))
            {
                Assert.Fail(" - ");
            }
        }

        /// <summary>
        ///A test for NewNoteFilename
        ///</summary>
        [TestMethod()]
        public void NewNoteFilenameTest()
        {
            Notes target = new Notes(); 
            int id = 1;
            string title = "test";
            string expected = Settings.NotesSavepath+"1-test.nfn";
            string actualfinamepath = target.NewNoteFilename(id, title);
            Assert.AreEqual(expected, actualfinamepath, "excepted filename and path are incorrect.");
        }

        /// <summary>
        ///A test for GetSkinsNames
        ///</summary>
        [TestMethod()]
        public void GetSkinsNamesTest()
        {
            Notes target = new Notes(); // TODO: Initialize to an appropriate value
            string[] expected = new string[2];
            expected[0] = "yellow";
            expected[1] = "orange";
            //expected[2] = "white";
            string[] actual = target.GetSkinsNames();
            StringBuilder skinnames = new StringBuilder();
            for (int i = 0; i < actual.Length; i++)
            {
                skinnames.Append(actual[i]+",");
            }
            Assert.AreEqual(expected, actual, "skins names doesn't match, actual:"+skinnames.ToString());
        }

        /// <summary>
        ///A test for GetSkinName
        ///</summary>
        [TestMethod()]
        public void GetSkinNameTest()
        {
            Notes target = new Notes(); // TODO: Initialize to an appropriate value
            int skinnr = 0; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GetSkinName(skinnr);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetHighlightColor
        ///</summary>
        [TestMethod()]
        public void GetHighlightColorTest()
        {
            Notes target = new Notes();
            int skinnr = 0; //yellow skin
            
            ColorConverter expected = new ColorConverter();

            Color actual = target.GetHighlightColor(skinnr);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetForegroundColor
        ///</summary>
        [TestMethod()]
        public void GetForegroundColorTest()
        {
            Notes target = new Notes(); // TODO: Initialize to an appropriate value
            int skinnr = 0; // TODO: Initialize to an appropriate value
            Color expected = new Color(); // TODO: Initialize to an appropriate value
            Color actual;
            actual = target.GetForegroundColor(skinnr);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetBackgroundColor
        ///</summary>
        [TestMethod()]
        public void GetBackgroundColorTest()
        {
            Notes target = new Notes(); // TODO: Initialize to an appropriate value
            int skinnr = 0; // TODO: Initialize to an appropriate value
            Color expected = new Color(); // TODO: Initialize to an appropriate value
            Color actual;
            actual = target.GetBackgroundColor(skinnr);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreateNote
        ///</summary>
        [TestMethod()]
        public void CreateNoteTest()
        {
            Notes target = new Notes(); // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            int skinnr = 0; // TODO: Initialize to an appropriate value
            int x = 0; // TODO: Initialize to an appropriate value
            int y = 0; // TODO: Initialize to an appropriate value
            int width = 0; // TODO: Initialize to an appropriate value
            int height = 0; // TODO: Initialize to an appropriate value
            Note expected = null; // TODO: Initialize to an appropriate value
            Note actual;
            actual = target.CreateNote(title, skinnr, x, y, width, height);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
