using DiscreteEventSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DiscreteEventSimulatorTesting
{
    
    
    /// <summary>
    ///This is a test class for SimulatorTest and is intended
    ///to contain all SimulatorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SimulatorTest
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
        ///A test for Simulator Constructor under normal conditions
        ///</summary>
        [TestMethod()]
        public void SimulatorConstructorTestNormal()
        {
            DateTime beginTime = DateTime.Now;
            TimeSpan runningTime = new TimeSpan(2,0,0);
            double callArriveMultiplier = 0.1;
            double switchDelayMultiplier = 0.1;
            List<ProductType> productTypes = new List<ProductType> { new ProductType("Test", 0.1, 0.1)};
            int maxQueueLength = 10;
            bool singleQueueLength = true;
            TimeSpan excessiveWaitTime = new TimeSpan(0, 1, 0);
            Dictionary<SalesRepType, int> repNums = new Dictionary<SalesRepType, int>();
            repNums.Add(new SalesRepType("Test"), 2);
            Simulator target = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);

            Assert.IsInstanceOfType(target, typeof(Simulator));
        }

        /// <summary>
        ///A test for Simulator Constructor with null productTypes list
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SimulatorConstructorTestNullProductTypes()
        {
            DateTime beginTime = DateTime.Now;
            TimeSpan runningTime = new TimeSpan(2, 0, 0);
            double callArriveMultiplier = 0.1;
            double switchDelayMultiplier = 0.1;
            List<ProductType> productTypes = null;
            int maxQueueLength = 10;
            bool singleQueueLength = true;
            TimeSpan excessiveWaitTime = new TimeSpan(0, 1, 0);
            Dictionary<SalesRepType, int> repNums = new Dictionary<SalesRepType, int>();
            repNums.Add(new SalesRepType("Test"), 2);
            Simulator target = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);

            // No asserts as we expect an exception
        }

        /// <summary>
        ///A test for Simulator Constructor with null repNums
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SimulatorConstructorTestNullRepNums()
        {
            DateTime beginTime = DateTime.Now;
            TimeSpan runningTime = new TimeSpan(2, 0, 0);
            double callArriveMultiplier = 0.1;
            double switchDelayMultiplier = 0.1;
            List<ProductType> productTypes = null;
            int maxQueueLength = 10;
            bool singleQueueLength = true;
            TimeSpan excessiveWaitTime = new TimeSpan(0, 1, 0);
            Dictionary<SalesRepType, int> repNums = null;
            Simulator target = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);

            // No asserts as we expect an exception
        }

        /// <summary>
        ///A test for BeginTime
        ///</summary>
        [TestMethod()]
        public void BeginTimeTest()
        {
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
            Simulator target = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);
            DateTime actual;
            actual = target.BeginTime;
            Assert.AreEqual(beginTime, actual);
        }

        /// <summary>
        ///A test for Calendar
        ///</summary>
        [TestMethod()]
        public void CalendarTest()
        {
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
            Simulator target = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);
            Calendar actual;
            actual = target.Calendar;
            Assert.IsInstanceOfType(actual, typeof(Calendar));
        }

        /// <summary>
        ///A test for CallArriveMultiplier
        ///</summary>
        [TestMethod()]
        public void CallArriveMultiplierTest()
        {
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
            Simulator target = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);
            double actual;
            actual = target.CallArriveMultiplier;
            Assert.AreEqual(callArriveMultiplier, actual);
        }

        /// <summary>
        ///A test for CallFactory
        ///</summary>
        [TestMethod()]
        public void CallFactoryTest()
        {
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
            Simulator target = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);
            CallFactory actual;
            actual = target.CallFactory;
            Assert.IsInstanceOfType(actual, typeof(CallFactory));
        }

        /// <summary>
        ///A test for Clock
        ///</summary>
        [TestMethod()]
        public void ClockTest()
        {
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
            Simulator target = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);
            DateTime actual;
            actual = target.Clock;
            Assert.AreEqual(beginTime, actual);
        }

        /// <summary>
        ///A test for EventFactory
        ///</summary>
        [TestMethod()]
        public void EventFactoryTest()
        {
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
            Simulator target = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);
            EventFactory actual;
            actual = target.EventFactory;
            Assert.IsInstanceOfType(actual, typeof(EventFactory));
        }

        /// <summary>
        ///A test for ExcessiveWaitTime
        ///</summary>
        [TestMethod()]
        public void ExcessiveWaitTimeTest()
        {
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
            Simulator target = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);
            TimeSpan actual;
            actual = target.ExcessiveWaitTime;
            Assert.AreEqual(excessiveWaitTime, actual);
        }

        /// <summary>
        ///A test for ProcessArgsFactory
        ///</summary>
        [TestMethod()]
        public void ProcessArgsFactoryTest()
        {
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
            Simulator target = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);
            ProcessArgsFactory actual;
            actual = target.ProcessArgsFactory;
            Assert.IsInstanceOfType(actual, typeof(ProcessArgsFactory));
        }

        /// <summary>
        ///A test for ProductTypes
        ///</summary>
        [TestMethod()]
        public void ProductTypesTest()
        {
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
            Simulator target = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);
            List<ProductType> actual;
            actual = target.ProductTypes;
            Assert.AreEqual(productTypes, actual);
        }

        /// <summary>
        ///A test for QueueManager
        ///</summary>
        [TestMethod()]
        public void QueueManagerTest()
        {
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
            Simulator target = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);
            QueueManager actual;
            actual = target.QueueManager;
            Assert.IsInstanceOfType(actual, typeof(QueueManager));
        }

        /// <summary>
        ///A test for SalesManager
        ///</summary>
        [TestMethod()]
        public void SalesManagerTest()
        {
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
            Simulator target = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);
            SalesForceManager actual;
            actual = target.SalesManager;
            Assert.IsInstanceOfType(actual, typeof(SalesForceManager));
        }

        /// <summary>
        ///A test for SwitchDelayMultiplier
        ///</summary>
        [TestMethod()]
        public void SwitchDelayMultiplierTest()
        {
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
            Simulator target = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);
            double actual;
            actual = target.SwitchDelayMultiplier;
            Assert.AreEqual(switchDelayMultiplier, actual);
        }
    }
}
