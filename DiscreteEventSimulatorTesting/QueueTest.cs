using DiscreteEventSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DiscreteEventSimulatorTesting
{
    
    
    /// <summary>
    ///This is a test class for QueueTest and is intended
    ///to contain all QueueTest Unit Tests
    ///</summary>
    [TestClass()]
    public class QueueTest
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
        ///A test for Queue Constructor
        ///</summary>
        [TestMethod()]
        public void QueueConstructorTest()
        {
            ProductType typeInQueue = new ProductType("Test", 0.1, 0.1);
            Queue target = new Queue(typeInQueue);

            Assert.IsInstanceOfType(target, typeof(Queue));
        }

        /// <summary>
        ///A test for Queue Constructor null product type
        ///</summary>
        [TestMethod()]
        public void QueueConstructorTestNullProductType()
        {
            ProductType typeInQueue = null;
            Queue target = new Queue(typeInQueue);

            Assert.IsInstanceOfType(target, typeof(Queue));
        }

        /// <summary>
        ///A test for Dequeue
        ///</summary>
        [TestMethod()]
        public void DequeueTest()
        {
            ProductType typeInQueue = new ProductType("Test", 0.1, 0.1);
            Queue target = new Queue(typeInQueue);
            Call expected = new Call((uint)1);
            expected.ProductType = typeInQueue;
            target.Enqueue(expected);
            Call actual;
            actual = target.Dequeue();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Enqueue
        ///</summary>
        [TestMethod()]
        public void EnqueueTest()
        {
            ProductType typeInQueue = new ProductType("Test", 0.1, 0.1);
            Queue target = new Queue(typeInQueue);
            Call expected = new Call((uint)1);
            expected.ProductType = typeInQueue;
            target.Enqueue(expected);
            Assert.AreEqual(1, target.Calls.Count);
            Assert.AreEqual(expected, target.Dequeue());
        }

        /// <summary>
        ///A test for Enqueue null call
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EnqueueTestNullCall()
        {
            ProductType typeInQueue = new ProductType("Test", 0.1, 0.1);
            Queue target = new Queue(typeInQueue);
            Call expected = null;
            target.Enqueue(expected);
        }

        /// <summary>
        ///A test for Enqueue invalid product type
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EnqueueTestInvalidProductType()
        {
            ProductType typeInQueue = new ProductType("Test", 0.1, 0.1);
            Queue target = new Queue(typeInQueue);
            Call expected = new Call((uint)1);
            expected.ProductType = new ProductType("Dave", 0.1, 0.1);
            target.Enqueue(expected);
        }


        /// <summary>
        ///A test for Peek
        ///</summary>
        [TestMethod()]
        public void PeekTest()
        {
            ProductType typeInQueue = new ProductType("Test", 0.1, 0.1);
            Queue target = new Queue(typeInQueue);
            Call expected = new Call((uint)1);
            expected.ProductType = typeInQueue;
            target.Enqueue(expected);
            Call actual;
            actual = target.Peek();
            Assert.AreEqual(expected, actual);
        }
    }
}
