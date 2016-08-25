using DiscreteEventSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DiscreteEventSimulatorTesting
{
    
    
    /// <summary>
    ///This is a test class for StatisticsManagerTest and is intended
    ///to contain all StatisticsManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StatisticsManagerTest
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
        ///A test for StatisticsManager Constructor under normal conditions
        ///</summary>
        [TestMethod()]
        public void StatisticsManagerConstructorTestNormalConditions()
        {
            Dictionary<SalesRepType, int> repNums = new Dictionary<SalesRepType, int>();
            repNums.Add(new SalesRepType("Test"), 2);
            Simulator sim = new Simulator(DateTime.Now, new TimeSpan(2,0,0), 0.1, 0.1, new List<ProductType> { new ProductType("Test", 0.1, 0.1) }, 10, true, new TimeSpan(0,2,0), repNums);
            StatisticsManager_Accessor target = new StatisticsManager_Accessor(sim);

            //Assert that it constructed correctly
            Assert.AreEqual(sim, target.sim);
        }

        /// <summary>
        ///A test for StatisticsManager Constructor null simulator
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StatisticsManagerConstructorTestNullSimulator()
        {
            Simulator sim = null;
            StatisticsManager target = new StatisticsManager(sim);

            // No Assert an exception should have been thrown
        }
      
    }
}
