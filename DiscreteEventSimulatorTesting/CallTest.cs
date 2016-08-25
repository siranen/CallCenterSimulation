using DiscreteEventSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DiscreteEventSimulatorTesting
{
    
    
    /// <summary>
    ///This is a test class for CallTest and is intended
    ///to contain all CallTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CallTest
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
        ///A test for Call Constructor
        ///</summary>
        [TestMethod()]
        public void CallConstructorTest()
        {
            uint callId = 0;
            Call target = new Call(callId);

            Assert.IsInstanceOfType(target, typeof(Call));
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            uint callId = 0; 
            Call target = new Call(callId);
            string expected = "Call ID: 0, StartTime: 00:00, BeginWaitTime: 00:00, FinishTime: 00:00, Product Type: Not Set"; 	 
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}
