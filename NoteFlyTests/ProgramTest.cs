//-----------------------------------------------------------------------
// <copyright file="ProgramTest.cs" company="NoteFly">
//  NoteFly a note application.
//  Copyright (C) 2013  Tom
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
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NoteFly;

    /// <summary>
    /// Summary description for ProgramTest
    /// </summary>
    [TestClass]
    public class ProgramTest
    {

        private TestContext testContextInstance;

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        #endregion

        [TestMethod]
        public void ChangeUrlIPVersionTest()
        {
            string exceptedurl = "http://ipv4.test123.test";
            NoteFly.Settings.NetworkIPversion = 1; // force IPv4
            string currenturl = "http://update.test123.test";
            currenturl = NoteFly.Program.ChangeUrlIPVersion(currenturl);
            Assert.AreEqual(exceptedurl, currenturl);

            exceptedurl = "http://ipv6.test123.test";
            NoteFly.Settings.NetworkIPversion = 2; // force IPv6
            currenturl = "http://update.test123.test";
            currenturl = NoteFly.Program.ChangeUrlIPVersion(currenturl);
            Assert.AreEqual(exceptedurl, currenturl);
        }

        [TestMethod]
        public void ParserVersionStringTest()
        {
            short[] exceptedversion = new short[3];
            exceptedversion[0] = 1;
            exceptedversion[1] = 2;
            exceptedversion[2] = 3;
            short[] actualversion = Program.ParserVersionString("1.2.3");
            Assert.AreEqual(exceptedversion.Length, actualversion.Length, "Wrong number of version number parts.");
            for (int i = 0; i < exceptedversion.Length; i++)
            {
                Assert.AreEqual(exceptedversion[i], actualversion[i]);
            }
        }

        [TestMethod]
        public void CompareVersionsTest()
        {
            int exceptedresult = -1;
            short[] version100 = NoteFly.Program.ParserVersionString("1.0.0");
            short[] version101 = NoteFly.Program.ParserVersionString("1.0.1");
            int currentresult = NoteFly.Program.CompareVersions(version100, version101);
            Assert.AreEqual(exceptedresult, currentresult);

            exceptedresult = 1;
            short[] version099 = NoteFly.Program.ParserVersionString("0.9.9");
            currentresult = NoteFly.Program.CompareVersions(version100, version099);
            Assert.AreEqual(exceptedresult, currentresult);

            exceptedresult = 0;
            currentresult = NoteFly.Program.CompareVersions(version100, version100);
            Assert.AreEqual(exceptedresult, currentresult);
        }
    }
}
