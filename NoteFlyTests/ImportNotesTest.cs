

namespace NoteFlyTests
{
    using NoteFly;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;
    using System.Windows.Forms;
    
    /// <summary>
    ///This is a test class for ImportNotesTest and is intended
    ///to contain all ImportNotesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ImportNotesTest
    {
        private TestContext testContextInstance;

        private Notes notes = new Notes(false);

        private static string testnote1;

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
        public static void ImportNotesTestInitialize(TestContext testContext)
        {
            testnote1 = Path.Combine(Program.InstallFolder, "7c119b7d-00ba-4573-85e4-dda22f3be4ab.note");
            System.IO.StreamWriter testfile1 = new System.IO.StreamWriter(testnote1, false, System.Text.Encoding.UTF8);
            testfile1.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?>\n");
            testfile1.Write("<note version=\"0.3\" xmlns:link=\"http://beatniksoftware.com/tomboy/link\" xmlns:size=\"http://beatniksoftware.com/tomboy/size\" xmlns=\"http://beatniksoftware.com/tomboy\">\n");
            testfile1.Write("  <title>test note1</title>\n");
            testfile1.Write("  <text xml:space=\"preserve\"><note-content version=\"0.1\">test note1\n");
            testfile1.Write("\n");
            testfile1.Write("This is a <bold>test</bold> tomboy <strikethrough>file</strikethrough> <italic>note</italic>.</note-content></text>\n");
            testfile1.Write("  <last-change-date>2012-07-21T02:26:47.0330000+02:00</last-change-date>\n");
            testfile1.Write("  <last-metadata-change-date>2012-07-21T02:26:47.0330000+02:00</last-metadata-change-date>\n");
            testfile1.Write("  <create-date>2001-02-03T01:23:09.2150000+02:00</create-date>\n");
            testfile1.Write("  <cursor-position>44</cursor-position>\n");
            testfile1.Write("  <selection-bound-position>44</selection-bound-position>\n");
            testfile1.Write("  <width>450</width>\n");
            testfile1.Write("  <height>360</height>\n");
            testfile1.Write("  <x>50</x>\n");
            testfile1.Write("  <y>80</y>\n");
            testfile1.Write("  <open-on-startup>False</open-on-startup>\n");
            testfile1.Write("</note>\n");

            testfile1.Close();
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
        //
        #endregion


        /// <summary>
        ///A test for ReadTomboyfile
        ///</summary>
        [TestMethod()]
        public void ReadTomboyfileTest()
        {
            ImportNotes importnotes = new ImportNotes(notes);
            StreamReader reader = new StreamReader(testnote1, true); 
           
            TextBox tbtitle = new TextBox();
            RichTextBox rtbNewNote = new RichTextBox();

            string exceptedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}
\viewkind4\uc1\pard\f0\fs17 test note1\par
\par
This is a\b test\b0 tomboy \strike file\strike0 \i note\i0.\par
}";
            importnotes.ReadTomboyfile(reader, testnote1, tbtitle, rtbNewNote);

            Assert.AreEqual(exceptedrtf, rtbNewNote.Rtf);
        }
    }
}
