using DiscreteEventSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;

namespace DiscreteEventSimulatorTesting
{
    
    
    /// <summary>
    ///This is a test class for RunningDisplayTest and is intended
    ///to contain all RunningDisplayTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RunningDisplayTest
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
        ///A test for RunningDisplay Constructor normal
        ///</summary>
        [TestMethod()]
        public void RunningDisplayConstructorTest()
        {
            int windowX = 0;
            int windowY = 0;
            DateTime beginTime = DateTime.Now;
            TimeSpan runningTime = new TimeSpan(2, 0, 0);
            double callArriveMultiplier = 0.1;
            double switchDelayMultiplier = 0.1;
            List<ProductType> productTypes = new List<ProductType> { new ProductType("Test", 0.1, 0.1) };
            int maxQueueLength = 10;
            bool singleQueueLength = true;
            TimeSpan excessiveWaitTime = new TimeSpan(0, 1, 0);
            Dictionary<SalesRepType, int> repNums = new Dictionary<SalesRepType, int>();
            repNums.Add(new SalesRepType("Test"), 2);
            Simulator sim = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);
            RunningDisplay target = new RunningDisplay(windowX, windowY, sim);
            Assert.IsInstanceOfType(target, typeof(RunningDisplay));
        }

        /// <summary>
        ///A test for RunningDisplay Constructor null simulator
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RunningDisplayConstructorTestNullSimulator()
        {
            int windowX = 0;
            int windowY = 0;
            Simulator sim = null;
            RunningDisplay target = new RunningDisplay(windowX, windowY, sim);
        }

        /// <summary>
        ///A test for CreateTabPageFor
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        public void CreateTabPageForTest()
        {
            int windowX = 0;
            int windowY = 0;
            DateTime beginTime = DateTime.Now;
            TimeSpan runningTime = new TimeSpan(2, 0, 0);
            double callArriveMultiplier = 0.1;
            double switchDelayMultiplier = 0.1;
            List<ProductType> productTypes = new List<ProductType> { new ProductType("Test", 0.1, 0.1) };
            int maxQueueLength = 10;
            bool singleQueueLength = true;
            TimeSpan excessiveWaitTime = new TimeSpan(0, 1, 0);
            Dictionary<SalesRepType, int> repNums = new Dictionary<SalesRepType, int>();
            repNums.Add(new SalesRepType("Test"), 2);
            Simulator sim = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);
            RunningDisplay_Accessor target = new RunningDisplay_Accessor(windowX, windowY, sim);
            ProductType pt = new ProductType("Test", 0.1, 0.1);
            
            TabPage actual;
            actual = target.CreateTabPageFor(pt);
            Assert.AreEqual("Test", actual.Text);
        }

        /// <summary>
        ///A test for CreateTabPageFor null product type
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateTabPageForTestNullProductType()
        {
            int windowX = 0;
            int windowY = 0;
            DateTime beginTime = DateTime.Now;
            TimeSpan runningTime = new TimeSpan(2, 0, 0);
            double callArriveMultiplier = 0.1;
            double switchDelayMultiplier = 0.1;
            List<ProductType> productTypes = new List<ProductType> { new ProductType("Test", 0.1, 0.1) };
            int maxQueueLength = 10;
            bool singleQueueLength = true;
            TimeSpan excessiveWaitTime = new TimeSpan(0, 1, 0);
            Dictionary<SalesRepType, int> repNums = new Dictionary<SalesRepType, int>();
            repNums.Add(new SalesRepType("Test"), 2);
            Simulator sim = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);
            RunningDisplay_Accessor target = new RunningDisplay_Accessor(windowX, windowY, sim);
            ProductType pt = null;

            TabPage actual;
            actual = target.CreateTabPageFor(pt);
        }
    }
}
