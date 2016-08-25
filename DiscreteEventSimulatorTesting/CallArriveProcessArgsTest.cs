using DiscreteEventSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DiscreteEventSimulatorTesting
{
    
    
    /// <summary>
    ///This is a test class for CallArriveProcessArgsTest and is intended
    ///to contain all CallArriveProcessArgsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CallArriveProcessArgsTest
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
        ///A test for CallArriveProcessArgs Constructor
        ///</summary>
        [TestMethod()]
        public void CallArriveProcessArgsConstructorTest()
        {
            EventFactory eventFactory = new EventFactory();
            int maxQueueLength = 10;
            List<ProductType> productTypes = new List<ProductType> { new ProductType("Test", 0.1, 0.1) };
            bool singleQueueLength = false;
            QueueManager queueManager = new QueueManager(maxQueueLength, productTypes, singleQueueLength);
            CallFactory callFactory = new CallFactory();
            double callArriveMultiplier = 0.1;
            double switchDelayMultiplier = 0.1;
            CallArriveProcessArgs target = new CallArriveProcessArgs(eventFactory, queueManager, callFactory, productTypes, callArriveMultiplier, switchDelayMultiplier);

            Assert.IsInstanceOfType(target, typeof(CallArriveProcessArgs));
        }

        /// <summary>
        ///A test for CallArriveProcessArgs Constructor null event factory
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CallArriveProcessArgsConstructorTestNullEventFactory()
        {
            EventFactory eventFactory = null;
            int maxQueueLength = 10;
            List<ProductType> productTypes = new List<ProductType> { new ProductType("Test", 0.1, 0.1) };
            bool singleQueueLength = false;
            QueueManager queueManager = new QueueManager(maxQueueLength, productTypes, singleQueueLength);
            CallFactory callFactory = new CallFactory();
            double callArriveMultiplier = 0.1;
            double switchDelayMultiplier = 0.1;
            CallArriveProcessArgs target = new CallArriveProcessArgs(eventFactory, queueManager, callFactory, productTypes, callArriveMultiplier, switchDelayMultiplier);
        }

        /// <summary>
        ///A test for CallArriveProcessArgs Constructor null queueManager
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CallArriveProcessArgsConstructorTestNullQueueManager()
        {
            EventFactory eventFactory = new EventFactory();
            List<ProductType> productTypes = new List<ProductType> { new ProductType("Test", 0.1, 0.1) };
            QueueManager queueManager = null;
            CallFactory callFactory = new CallFactory();
            double callArriveMultiplier = 0.1;
            double switchDelayMultiplier = 0.1;
            CallArriveProcessArgs target = new CallArriveProcessArgs(eventFactory, queueManager, callFactory, productTypes, callArriveMultiplier, switchDelayMultiplier);
        }

        /// <summary>
        ///A test for CallArriveProcessArgs Constructor null productTypes
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CallArriveProcessArgsConstructorTestNullProductTypes()
        {
            EventFactory eventFactory = new EventFactory();
            int maxQueueLength = 10;
            List<ProductType> productTypes = new List<ProductType> { new ProductType("Test", 0.1, 0.1) };
            bool singleQueueLength = false;
            QueueManager queueManager = new QueueManager(maxQueueLength, productTypes, singleQueueLength);
            productTypes = null;
            CallFactory callFactory = new CallFactory();
            double callArriveMultiplier = 0.1;
            double switchDelayMultiplier = 0.1;
            CallArriveProcessArgs target = new CallArriveProcessArgs(eventFactory, queueManager, callFactory, productTypes, callArriveMultiplier, switchDelayMultiplier);
        }
    }
}
