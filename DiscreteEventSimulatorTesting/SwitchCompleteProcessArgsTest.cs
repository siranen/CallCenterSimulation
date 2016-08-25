using DiscreteEventSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DiscreteEventSimulatorTesting
{
    
    
    /// <summary>
    ///This is a test class for SwitchCompleteProcessArgsTest and is intended
    ///to contain all SwitchCompleteProcessArgsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SwitchCompleteProcessArgsTest
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
        ///A test for SwitchCompleteProcessArgs Constructor under normal conditions
        ///</summary>
        [TestMethod()]
        public void SwitchCompleteProcessArgsConstructorTestNormalConditions()
        {
            EventFactory eventFactory = new EventFactory();
            QueueManager queueManager = new QueueManager(10, new System.Collections.Generic.List<ProductType> { new ProductType("Test", 0.1, 0.1)}, true);
            Dictionary<SalesRepType, int> repNums = new Dictionary<SalesRepType, int>();
            repNums.Add(new SalesRepType("Test"), 2);
            SalesForceManager salesManager = new SalesForceManager(repNums);
            SwitchCompleteProcessArgs target = new SwitchCompleteProcessArgs(eventFactory, queueManager, salesManager);
            
            // Assert that all of the fields are equal to what was passed in
            Assert.AreEqual(eventFactory, target.EventFactory);
            Assert.AreEqual(queueManager, target.QueueManager);
            Assert.AreEqual(salesManager, target.SalesManager);
        }

        /// <summary>
        ///A test for SwitchCompleteProcessArgs Constructor with all null arguments
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SwitchCompleteProcessArgsConstructorTestNullAll()
        {
            EventFactory eventFactory = null;
            QueueManager queueManager = null;
            SalesForceManager salesManager = null;
            SwitchCompleteProcessArgs target = new SwitchCompleteProcessArgs(eventFactory, queueManager, salesManager);

            // No Assert we expect an exception
        }

        /// <summary>
        ///A test for SwitchCompleteProcessArgs Constructor with a null SalesManager
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SwitchCompleteProcessArgsConstructorTestNullSalesManager()
        {
            EventFactory eventFactory = new EventFactory();
            QueueManager queueManager = new QueueManager(10, new System.Collections.Generic.List<ProductType> { new ProductType("Test", 0.1, 0.1) }, true);
            
            SalesForceManager salesManager = null;
            SwitchCompleteProcessArgs target = new SwitchCompleteProcessArgs(eventFactory, queueManager, salesManager);

            // No Assert we expect an exception
        }

        /// <summary>
        ///A test for SwitchCompleteProcessArgs Constructor with a null QueueManager
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SwitchCompleteProcessArgsConstructorTestNullQueueManager()
        {
            EventFactory eventFactory = new EventFactory();
            QueueManager queueManager = null;
            Dictionary<SalesRepType, int> repNums = new Dictionary<SalesRepType, int>();
            repNums.Add(new SalesRepType("Test"), 2);
            SalesForceManager salesManager = new SalesForceManager(repNums);
            SwitchCompleteProcessArgs target = new SwitchCompleteProcessArgs(eventFactory, queueManager, salesManager);

            // No Assert we expect an exception
        }

        /// <summary>
        ///A test for SwitchCompleteProcessArgs Constructor with a null eventFactory
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SwitchCompleteProcessArgsConstructorTestNullEventFactory()
        {
            EventFactory eventFactory = null;
            QueueManager queueManager = new QueueManager(10, new System.Collections.Generic.List<ProductType> { new ProductType("Test", 0.1, 0.1) }, true);
            Dictionary<SalesRepType, int> repNums = new Dictionary<SalesRepType, int>();
            repNums.Add(new SalesRepType("Test"), 2);
            SalesForceManager salesManager = new SalesForceManager(repNums);
            SwitchCompleteProcessArgs target = new SwitchCompleteProcessArgs(eventFactory, queueManager, salesManager);

            // No Assert we expect an exception
        }

        /// <summary>
        ///A test for SalesManager under normal conditions
        ///</summary>
        [TestMethod()]
        public void SalesManagerTestNormalConditions()
        {
            EventFactory eventFactory = new EventFactory();
            QueueManager queueManager = new QueueManager(10, new System.Collections.Generic.List<ProductType> { new ProductType("Test", 0.1, 0.1) }, true);
            Dictionary<SalesRepType, int> repNums = new Dictionary<SalesRepType, int>();
            repNums.Add(new SalesRepType("Test"), 2);
            SalesForceManager salesManager = new SalesForceManager(repNums);
            SwitchCompleteProcessArgs target = new SwitchCompleteProcessArgs(eventFactory, queueManager, salesManager);
            SalesForceManager actual;
            actual = target.SalesManager;
            Assert.AreEqual(salesManager, actual);
        }
    }
}
