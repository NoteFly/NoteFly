//-----------------------------------------------------------------------
// <copyright file="ImportNotesTest.cs" company="NoteFly">
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
    using System;
    using System.IO;
    using System.Windows.Forms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NoteFly;

    /// <summary>
    /// This is a test class for ImportNotesTest and is intended
    /// to contain all ImportNotesTest Unit Tests
    /// </summary>
    [TestClass()]
    public class ImportNotesTest
    {
        /// <summary>
        /// NoteFly test note file
        /// </summary>
        private static string testnote1;

        /// <summary>
        /// CSV test import file 
        /// </summary>
        private static string testcsvfile;

        /// <summary>
        /// 
        /// </summary>
        private TestContext testContextInstance;

        /// <summary>
        /// 
        /// </summary>
        private Notes notes = new Notes(false);

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
        /// <summary>
        /// Initialize unit tests for ImportNotes class.
        /// </summary>
        /// <param name="testContext"></param>
        [ClassInitialize()]
        public static void ImportNotesTestInitialize(TestContext testContext)
        {
            testnote1 = Path.Combine(Program.InstallFolder, "7c119b7d-00ba-4573-85e4-dda22f3be4ab.note");
            StreamWriter testfile1 = new System.IO.StreamWriter(testnote1, false, System.Text.Encoding.UTF8);
            try
            {
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
            }
            finally
            {
                testfile1.Close();
            }

            testcsvfile = Path.Combine(Program.InstallFolder, "test.csv");
            ////if (!File.Exists(testcsvfile))
            ////{
                StreamWriter stickiescsvfilewriter = new StreamWriter(testcsvfile, false, System.Text.Encoding.ASCII);
                try
                {
                    stickiescsvfilewriter.Write("\"Title\",\"Date/Time\",\"Colour\",\"Width\",\"RTF\"\n");
                    stickiescsvfilewriter.Write("\"00740065007300740031\",\"1312669528\",\"11862015\",\"300\",\"{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang" + Properties.Settings.Default.rtflangused + "{\\fonttbl{\\f0\\fnil\\fcharset0 Verdana;}}{\\colortbl ;\\red0\\green0\\blue0;}\\viewkind4\\uc1\\pard\\qr\\cf1\\f0\\fs20 test1\\par}\"\n");
                    stickiescsvfilewriter.Write("\"00740065007300740032\",\"1312669528\",\"33023\",\"-99\",\"{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang" + Properties.Settings.Default.rtflangused + "{\\fonttbl{\\f0\\fnil Verdana;}{\\f1\\fnil\\fcharset0 Verdana;}}{\\colortbl ;\\red0\\green0\\blue0;}\\viewkind4\\uc1\\pard\\cf1\\f0\\fs20  \\f1 test\\b test\\b0 test2\\par\\f0}\"\n");
                }
                finally
                {
                    stickiescsvfilewriter.Close();
                }
            ////}
        }

        #endregion

        /// <summary>
        /// A test for ReadTomboyfile
        /// </summary>
        [TestMethod()]
        public void ReadTomboyfileTest()
        {
            ImportNotes importnotes = new ImportNotes(this.notes);
            StreamReader reader = new StreamReader(testnote1, true);

            TextBox tbtitle = new TextBox();
            RichTextBox rtbNewNote = new RichTextBox();

            string exceptedrtf = @"{\rtf1\ansi\ansicpg1252\deff0\deflang" + Properties.Settings.Default.rtflangused + @"{\fonttbl{\f0\fnil\fcharset0 Microsoft Sans Serif;}}
\viewkind4\uc1\pard\f0\fs17 test note1\par
\par
This is a \b test\b0  tomboy \strike file\strike0  \i note\i0 .\par
}
";
            importnotes.ReadTomboyfile(reader, testnote1, tbtitle, rtbNewNote);

            Assert.AreEqual(exceptedrtf, rtbNewNote.Rtf);
        }

        [TestMethod()]
        public void ReadCSVfileTest()
        {
            ImportNotes importnotes = new ImportNotes(this.notes);
            while (this.notes.CountNotes > 0)
            {
                this.notes.RemoveNote(0);
            }

            importnotes.ReadStickiesCSVFile(testcsvfile);
            Assert.AreEqual(2, this.notes.CountNotes, "Not excepted number of notes imported.");

            string exceptedrtfnote1 = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang" + Properties.Settings.Default.rtflangused + "{\\fonttbl{\\f0\\fnil\\fcharset0 Verdana;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Msftedit 5.41.21.2510;}\\viewkind4\\uc1\\pard\\qr\\cf1\\f0\\fs20 test1\\par\r\n}\r\n";
            string currentrtfnote1 = this.notes.GetNote(0).GetContent();
            Assert.AreEqual(currentrtfnote1, exceptedrtfnote1, "RTF import went wrong.");

            string exceptedrtfnote2 = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang" + Properties.Settings.Default.rtflangused + "{\\fonttbl{\\f0\\fnil Verdana;}{\\f1\\fnil\\fcharset0 Verdana;}}\r\n{\\colortbl ;\\red0\\green0\\blue0;}\r\n{\\*\\generator Msftedit 5.41.21.2510;}\\viewkind4\\uc1\\pard\\cf1\\f0\\fs20  \\f1 test\\b test\\b0 test2\\par\r\n}\r\n"; // \\f0
            string currentrtfnote2 = this.notes.GetNote(1).GetContent();
            Assert.AreEqual(currentrtfnote2, exceptedrtfnote2, "RTF import note 2 went wrong.");

        }
    }
}
