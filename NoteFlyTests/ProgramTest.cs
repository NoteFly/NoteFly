namespace NoteFlyTests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using NoteFly;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Summary description for ProgramTest
    /// </summary>
    [TestClass]
    public class ProgramTest
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
