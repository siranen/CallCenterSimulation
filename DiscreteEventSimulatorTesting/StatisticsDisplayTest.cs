using DiscreteEventSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DiscreteEventSimulatorTesting
{
    
    
    /// <summary>
    ///This is a test class for StatisticsDisplayTest and is intended
    ///to contain all StatisticsDisplayTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StatisticsDisplayTest
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
        ///A test for StatisticsDisplay Constructor under normal conditions
        ///</summary>
        [TestMethod()]
        public void StatisticsDisplayConstructorTestNormal()
        {
            int windowX = 200;
            int windowY = 100;
            Dictionary<SalesRepType, int> repNums = new Dictionary<SalesRepType, int>();
            repNums.Add(new SalesRepType("Test"), 2);
            Simulator sim = new Simulator(DateTime.Now, new TimeSpan(2, 0, 0), 0.1, 0.1, new List<ProductType> { new ProductType("Test", 0.1, 0.1) }, 10, true, new TimeSpan(0, 2, 0), repNums);
            StatisticsManager statsMan = new StatisticsManager(sim);
            StatisticsDisplay_Accessor target = new StatisticsDisplay_Accessor(windowX, windowY, statsMan);
            
            //Assert the manager constructed correctly
            Assert.AreEqual(statsMan, target.statsMan);
            Assert.AreEqual(windowX, ((StatisticsDisplay)(target.Target)).Location.X);
            Assert.AreEqual(windowY, ((StatisticsDisplay)(target.Target)).Location.Y);
        }

        /// <summary>
        ///A test for StatisticsDisplay Constructor given null stats man
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StatisticsDisplayConstructorTestNullStatsMan()
        {
            int windowX = 200;
            int windowY = 100;
            StatisticsDisplay_Accessor target = new StatisticsDisplay_Accessor(windowX, windowY, null);

            // No Assertions we expect an exception
        }
    }
}
