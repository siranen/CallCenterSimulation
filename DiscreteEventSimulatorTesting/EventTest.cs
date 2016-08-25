using DiscreteEventSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DiscreteEventSimulatorTesting
{
    
    
    /// <summary>
    ///This is a test class for EventTest and is intended
    ///to contain all EventTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EventTest
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


        internal virtual Event CreateEvent()
        {
            
            Event target = new CallArriveEvent(null, DateTime.Now);
            return target;
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            Event target = CreateEvent();
            string expected = "EventTime: " + target.EventTime.ToShortTimeString(); 
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Entity
        ///</summary>
        [TestMethod()]
        public void EntityTest()
        {
            Event target = new CallArriveEvent(new Call(1), DateTime.Now);
            Call actual;
            actual = target.Entity;
            Assert.AreEqual((uint)1, actual.CallId);
        }

        /// <summary>
        ///A test for EventTime
        ///</summary>
        [TestMethod()]
        public void EventTimeTest()
        {
            DateTime now = DateTime.Now;
            Event target = new CallArriveEvent(new Call(1), now);
            DateTime actual;
            actual = target.EventTime;
            Assert.AreEqual(now, actual);
        }

        /// <summary>
        ///A test for EventType
        ///</summary>
        [TestMethod()]
        public void EventTypeTest()
        {
            Event target = CreateEvent();
            string actual;
            actual = target.EventType;
            Assert.AreEqual("Arrive at Call Centre", actual);
        }
    }
}
