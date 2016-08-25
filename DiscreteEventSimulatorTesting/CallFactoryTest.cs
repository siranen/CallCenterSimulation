using DiscreteEventSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DiscreteEventSimulatorTesting
{
    
    
    /// <summary>
    ///This is a test class for CallFactoryTest and is intended
    ///to contain all CallFactoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CallFactoryTest
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
        ///A test for CallFactory Constructor
        ///</summary>
        [TestMethod()]
        public void CallFactoryConstructorTest()
        {
            CallFactory target = new CallFactory();

            Assert.IsInstanceOfType(target, typeof(CallFactory));
        }

        /// <summary>
        ///A test for CreateCall
        ///</summary>
        [TestMethod()]
        public void CreateCallTest()
        {
            CallFactory target = new CallFactory();
            Call expected = new Call((uint)0);
            Call actual;
            actual = target.CreateCall();
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        /// <summary>
        ///A test for LastId
        ///</summary>
        [TestMethod()]
        public void LastIdTest()
        {
            CallFactory target = new CallFactory();
            uint actual;
            actual = target.LastId;
            Assert.AreEqual((uint)0, actual);
        }

        /// <summary>
        ///A test for LastId after creating a few calls
        ///</summary>
        [TestMethod()]
        public void LastIdTestAfterItemsCreated()
        {
            CallFactory target = new CallFactory();
            for (int i = 0; i < 5; i++)
            {
                target.CreateCall();
            }
            uint actual;
            actual = target.LastId;
            Assert.AreEqual((uint)5, actual);
        }
    }
}
