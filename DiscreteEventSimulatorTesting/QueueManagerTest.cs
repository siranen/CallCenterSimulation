using DiscreteEventSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DiscreteEventSimulatorTesting
{
    
    
    /// <summary>
    ///This is a test class for QueueManagerTest and is intended
    ///to contain all QueueManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class QueueManagerTest
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
        ///A test for QueueManager Constructor
        ///</summary>
        [TestMethod()]
        public void QueueManagerConstructorTest()
        {
            int maxQueueLength = 10; 
            List<ProductType> productTypes = new List<ProductType> { new ProductType("Test", 0.1, 0.1) }; 
            bool singleQueueLength = false; 
            QueueManager target = new QueueManager(maxQueueLength, productTypes, singleQueueLength);

            Assert.IsInstanceOfType(target, typeof(QueueManager));
        }

        /// <summary>
        ///A test for QueueManager Constructor null productTypes
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void QueueManagerConstructorTestNullProductType()
        {
            int maxQueueLength = 10;
            List<ProductType> productTypes = null;
            bool singleQueueLength = false;
            QueueManager target = new QueueManager(maxQueueLength, productTypes, singleQueueLength);
        }

        /// <summary>
        ///A test for QueueManager Constructor empty productTypes
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void QueueManagerConstructorTestEmptyProductType()
        {
            int maxQueueLength = 10;
            List<ProductType> productTypes = new List<ProductType>();
            bool singleQueueLength = false;
            QueueManager target = new QueueManager(maxQueueLength, productTypes, singleQueueLength);
        }

        /// <summary>
        ///A test for AddToQueue
        ///</summary>
        [TestMethod()]
        public void AddToQueueTest()
        {
            int maxQueueLength = 10;
            ProductType pt = new ProductType("Test", 0.1, 0.1);
            List<ProductType> productTypes = new List<ProductType> { pt };
            bool singleQueueLength = false; 
            QueueManager target = new QueueManager(maxQueueLength, productTypes, singleQueueLength);
            Call entity = new Call((uint)1);
            entity.ProductType = pt;
            target.AddToQueue(entity);

            Assert.AreEqual(1, target.GetQueueLength(pt));
        }

        /// <summary>
        ///A test for AddToQueue null entity
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddToQueueTestNullEntity()
        {
            int maxQueueLength = 10;
            ProductType pt = new ProductType("Test", 0.1, 0.1);
            List<ProductType> productTypes = new List<ProductType> { pt };
            bool singleQueueLength = false;
            QueueManager target = new QueueManager(maxQueueLength, productTypes, singleQueueLength);
            Call entity = null;
            target.AddToQueue(entity);
        }

        /// <summary>
        ///A test for AddToQueue No queue of type
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddToQueueTestNoQueueForType()
        {
            int maxQueueLength = 10;
            ProductType pt = new ProductType("Test", 0.1, 0.1);
            List<ProductType> productTypes = new List<ProductType> { pt };
            bool singleQueueLength = false;
            QueueManager target = new QueueManager(maxQueueLength, productTypes, singleQueueLength);
            Call entity = new Call((uint)1);
            entity.ProductType = new ProductType("Cheese", 0.1, 0.1);
            target.AddToQueue(entity);
        }

        /// <summary>
        ///A test for GetCallForRepType
        ///</summary>
        [TestMethod()]
        public void GetCallForRepTypeTest()
        {
            int maxQueueLength = 10;
            ProductType pt = new ProductType("Test", 0.1, 0.1);
            List<ProductType> productTypes = new List<ProductType> { pt };
            bool singleQueueLength = false;
            QueueManager target = new QueueManager(maxQueueLength, productTypes, singleQueueLength);
            SalesRepType repType = new SalesRepType("Test");
            repType.Handles.Add(pt);
            Call newCall = new Call((uint)1);
            newCall.ProductType = pt;
            target.AddToQueue(newCall);
            Call expected = newCall;
            Call actual;
            actual = target.GetCallForRepType(repType);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetCallForRepType null repType
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetCallForRepTypeTestNullRepType()
        {
            int maxQueueLength = 10;
            ProductType pt = new ProductType("Test", 0.1, 0.1);
            List<ProductType> productTypes = new List<ProductType> { pt };
            bool singleQueueLength = false;
            QueueManager target = new QueueManager(maxQueueLength, productTypes, singleQueueLength);
            SalesRepType repType = null;
            Call newCall = new Call((uint)1);
            newCall.ProductType = pt;
            target.AddToQueue(newCall);
            Call expected = newCall;
            Call actual;
            actual = target.GetCallForRepType(repType);
        }

        /// <summary>
        ///A test for GetCallForRepType for a reptype that cannot handle any of the queues
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetCallForRepTypeTestNoQueueFound()
        {
            int maxQueueLength = 10;
            ProductType pt = new ProductType("Test", 0.1, 0.1);
            List<ProductType> productTypes = new List<ProductType> { pt };
            bool singleQueueLength = false;
            QueueManager target = new QueueManager(maxQueueLength, productTypes, singleQueueLength);
            SalesRepType repType = new SalesRepType("Test");
            repType.Handles.Add(new ProductType("Cheese", 0.1, 0.1));
            Call newCall = new Call((uint)1);
            newCall.ProductType = pt;
            target.AddToQueue(newCall);
            Call expected = newCall;
            Call actual;
            actual = target.GetCallForRepType(repType);
        }

        /// <summary>
        ///A test for GetQueueLength
        ///</summary>
        [TestMethod()]
        public void GetQueueLengthTest()
        {
            int maxQueueLength = 10;
            ProductType pt = new ProductType("Test", 0.1, 0.1);
            List<ProductType> productTypes = new List<ProductType> { pt };
            bool singleQueueLength = false;
            QueueManager target = new QueueManager(maxQueueLength, productTypes, singleQueueLength);
            ProductType productType = pt;
            target.AddToQueue(new Call ((uint)1) { ProductType = pt });
            int expected = 1; 
            int actual;
            actual = target.GetQueueLength(productType);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetQueueLength null ProductType
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetQueueLengthTestNullProductType()
        {
            int maxQueueLength = 10;
            ProductType pt = new ProductType("Test", 0.1, 0.1);
            List<ProductType> productTypes = new List<ProductType> { pt };
            bool singleQueueLength = false;
            QueueManager target = new QueueManager(maxQueueLength, productTypes, singleQueueLength);
            ProductType productType = null;
            target.AddToQueue(new Call((uint)1) { ProductType = pt });
            int actual;
            actual = target.GetQueueLength(productType);
        }

        /// <summary>
        ///A test for GetQueueLength no queue found
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetQueueLengthTestNoQueueFound()
        {
            int maxQueueLength = 10;
            ProductType pt = new ProductType("Test", 0.1, 0.1);
            List<ProductType> productTypes = new List<ProductType> { pt };
            bool singleQueueLength = false;
            QueueManager target = new QueueManager(maxQueueLength, productTypes, singleQueueLength);
            ProductType productType = new ProductType("Dave", 0.1, 0.1);
            target.AddToQueue(new Call((uint)1) { ProductType = pt });
            int actual;
            actual = target.GetQueueLength(productType);
        }

        /// <summary>
        ///A test for IsQueueTooLong false
        ///</summary>
        [TestMethod()]
        public void IsQueueTooLongTestFalse()
        {
            int maxQueueLength = 10;
            ProductType pt = new ProductType("Test", 0.1, 0.1);
            List<ProductType> productTypes = new List<ProductType> { pt };
            bool singleQueueLength = false;
            QueueManager target = new QueueManager(maxQueueLength, productTypes, singleQueueLength); 
            ProductType productType = pt;
            Call newCall = new Call((uint)1);
            newCall.ProductType = pt;
            target.AddToQueue(newCall);
            bool expected = false; 
            bool actual;
            actual = target.IsQueueTooLong(productType);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsQueueTooLong true
        ///</summary>
        [TestMethod()]
        public void IsQueueTooLongTestTrue()
        {
            int maxQueueLength = 0;
            ProductType pt = new ProductType("Test", 0.1, 0.1);
            List<ProductType> productTypes = new List<ProductType> { pt };
            bool singleQueueLength = false;
            QueueManager target = new QueueManager(maxQueueLength, productTypes, singleQueueLength);
            ProductType productType = pt;
            Call newCall = new Call((uint)1);
            newCall.ProductType = pt;
            target.AddToQueue(newCall);
            bool expected = true;
            bool actual;
            actual = target.IsQueueTooLong(productType);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsQueueTooLong null product type
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsQueueTooLongTestNullProductType()
        {
            int maxQueueLength = 10;
            ProductType pt = new ProductType("Test", 0.1, 0.1);
            List<ProductType> productTypes = new List<ProductType> { pt };
            bool singleQueueLength = false;
            QueueManager target = new QueueManager(maxQueueLength, productTypes, singleQueueLength);
            ProductType productType = null;
            Call newCall = new Call((uint)1);
            newCall.ProductType = pt;
            target.AddToQueue(newCall);
            bool actual;
            actual = target.IsQueueTooLong(productType);
        }

        /// <summary>
        ///A test for IsQueueTooLong no queue found
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IsQueueTooLongTestNoQueueFound()
        {
            int maxQueueLength = 10;
            ProductType pt = new ProductType("Test", 0.1, 0.1);
            List<ProductType> productTypes = new List<ProductType> { pt };
            bool singleQueueLength = false;
            QueueManager target = new QueueManager(maxQueueLength, productTypes, singleQueueLength);
            ProductType productType = new ProductType("Dave", 0.1, 0.1);
            Call newCall = new Call((uint)1);
            newCall.ProductType = pt;
            target.AddToQueue(newCall);
            bool actual;
            actual = target.IsQueueTooLong(productType);
        }
    }
}
