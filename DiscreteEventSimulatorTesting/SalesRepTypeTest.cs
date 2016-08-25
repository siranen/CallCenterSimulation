using DiscreteEventSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DiscreteEventSimulatorTesting
{
    
    
    /// <summary>
    ///This is a test class for SalesRepTypeTest and is intended
    ///to contain all SalesRepTypeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SalesRepTypeTest
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
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
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
        ///A test for SalesRepType Constructor
        ///</summary>
        [TestMethod()]
        public void SalesRepTypeConstructorTest()
        {
            string typeName = "Test";
            SalesRepType target = new SalesRepType(typeName);

            Assert.IsInstanceOfType(target, typeof(SalesRepType));
        }

        /// <summary>
        ///A test for SalesRepType Constructor with a null typename string
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SalesRepTypeConstructorTestNullString()
        {
            string typeName = null;
            SalesRepType target = new SalesRepType(typeName);
        }

        /// <summary>
        ///A test for SalesRepType Constructor with a empty typename string
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SalesRepTypeConstructorTestEmptyString()
        {
            string typeName = string.Empty;
            SalesRepType target = new SalesRepType(typeName);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            string typeName = "Test"; 
            SalesRepType target = new SalesRepType(typeName);
            string expected = "Type Name: Test, Handles:"; 
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}
