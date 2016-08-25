using DiscreteEventSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DiscreteEventSimulatorTesting
{
    
    
    /// <summary>
    ///This is a test class for ProcessArgsFactoryTest and is intended
    ///to contain all ProcessArgsFactoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProcessArgsFactoryTest
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
        ///A test for ProcessArgsFactory Constructor under normal conditions
        ///</summary>
        [TestMethod()]
        public void ProcessArgsFactoryConstructorTestNormal()
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
            Simulator sim = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);
            ProcessArgsFactory_Accessor target = new ProcessArgsFactory_Accessor(sim);

            Assert.IsInstanceOfType(target.Target, typeof(ProcessArgsFactory));
            Assert.AreEqual(sim, target.sim);
        }

        /// <summary>
        ///A test for ProcessArgsFactory Constructor with a null simulator
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProcessArgsFactoryConstructorTestNullSimulator()
        {
            Simulator sim = null;
            ProcessArgsFactory target = new ProcessArgsFactory(sim);

        }

        /// <summary>
        ///A test for CreateCallArriveArgs
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        public void CreateCallArriveArgsTest()
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
            Simulator sim = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);
            ProcessArgsFactory_Accessor target = new ProcessArgsFactory_Accessor(sim);
            EventProcessArgs actual;
            actual = target.CreateCallArriveArgs();

            Assert.IsInstanceOfType(actual, typeof(CallArriveProcessArgs));
        }

        /// <summary>
        ///A test for CreateCompleteServiceArgs
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        public void CreateCompleteServiceArgsTest()
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
            Simulator sim = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);
            ProcessArgsFactory_Accessor target = new ProcessArgsFactory_Accessor(sim);

            EventProcessArgs actual;
            actual = target.CreateCompleteServiceArgs();

            Assert.IsInstanceOfType(actual, typeof(CompletedServiceProcessArgs));
        }

        /// <summary>
        ///A test for CreateEndReplicationArgs
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        public void CreateEndReplicationArgsTest()
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
            Simulator sim = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);
            ProcessArgsFactory_Accessor target = new ProcessArgsFactory_Accessor(sim);

            EventProcessArgs actual;
            actual = target.CreateEndReplicationArgs();
            Assert.IsInstanceOfType(actual, typeof(EndReplicationProcessArgs));
        }

        /// <summary>
        ///A test for CreateProcessArgsFor using a CallArriveEvent
        ///</summary>
        [TestMethod()]
        public void CreateProcessArgsForTestCallArrive()
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
            Simulator sim = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);
            ProcessArgsFactory_Accessor target = new ProcessArgsFactory_Accessor(sim);

            Event e = new CallArriveEvent(null, DateTime.Now);
             
            EventProcessArgs actual;
            actual = target.CreateProcessArgsFor(e);
            Assert.IsInstanceOfType(actual, typeof(CallArriveProcessArgs));
        }

        /// <summary>
        ///A test for CreateProcessArgsFor using a SwitchCompleted
        ///</summary>
        [TestMethod()]
        public void CreateProcessArgsForTestSwitchComplete()
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
            Simulator sim = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);
            ProcessArgsFactory_Accessor target = new ProcessArgsFactory_Accessor(sim);

            Event e = new SwitchCompletedEvent(null, DateTime.Now);

            EventProcessArgs actual;
            actual = target.CreateProcessArgsFor(e);
            Assert.IsInstanceOfType(actual, typeof(SwitchCompleteProcessArgs));
        }

        /// <summary>
        ///A test for CreateProcessArgsFor using a CompletedServiceEvent
        ///</summary>
        [TestMethod()]
        public void CreateProcessArgsForTestCompleteService()
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
            Simulator sim = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);
            ProcessArgsFactory_Accessor target = new ProcessArgsFactory_Accessor(sim);

            Event e = new CompletedServiceEvent(null, DateTime.Now);

            EventProcessArgs actual;
            actual = target.CreateProcessArgsFor(e);
            Assert.IsInstanceOfType(actual, typeof(CompletedServiceProcessArgs));
        }

        /// <summary>
        ///A test for CreateProcessArgsFor using a EndReplication
        ///</summary>
        [TestMethod()]
        public void CreateProcessArgsForTestEndReplication()
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
            Simulator sim = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);
            ProcessArgsFactory_Accessor target = new ProcessArgsFactory_Accessor(sim);

            Event e = new EndReplicationEvent(DateTime.Now);

            EventProcessArgs actual;
            actual = target.CreateProcessArgsFor(e);
            Assert.IsInstanceOfType(actual, typeof(EndReplicationProcessArgs));
        }

        /// <summary>
        ///A test for CreateProcessArgsFor using a null event throws exception
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateProcessArgsForTestNullEventThrowsException()
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
            Simulator sim = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);
            ProcessArgsFactory_Accessor target = new ProcessArgsFactory_Accessor(sim);

            Event e = null;

            EventProcessArgs actual;
            actual = target.CreateProcessArgsFor(e);

            // Should throw exception
        }

        /// <summary>
        ///A test for CreateSwitchCompleteArgs
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        public void CreateSwitchCompleteArgsTest()
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
            Simulator sim = new Simulator(beginTime, runningTime, callArriveMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWaitTime, repNums);
            ProcessArgsFactory_Accessor target = new ProcessArgsFactory_Accessor(sim);

            EventProcessArgs actual;
            actual = target.CreateSwitchCompleteArgs();
            Assert.IsInstanceOfType(actual, typeof(SwitchCompleteProcessArgs));
        }
    }
}
