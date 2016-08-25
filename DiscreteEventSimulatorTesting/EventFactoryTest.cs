using DiscreteEventSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DiscreteEventSimulatorTesting
{
    
    
    /// <summary>
    ///This is a test class for EventFactoryTest and is intended
    ///to contain all EventFactoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EventFactoryTest
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
        ///A test for CreateEvent CompleteService
        ///</summary>
        [TestMethod()]
        public void CreateEventTestCompleteService()
        {
            EventFactory target = new EventFactory();
            EEventType eventType = EEventType.CompletedService;
            DateTime eventTime = new DateTime();
            Call entity = null;
            string eventTypeS = "Completed Service";
            Event actual;
            actual = target.CreateEvent(eventType, eventTime, entity);
            Assert.AreEqual(eventTypeS, actual.EventType);
        }

        /// <summary>
        ///A test for CreateEvent CallArrive
        ///</summary>
        [TestMethod()]
        public void CreateEventTestCallArrive()
        {
            EventFactory target = new EventFactory();
            EEventType eventType = EEventType.CallArrive;
            DateTime eventTime = new DateTime();
            Call entity = null;
            string eventTypeS = "Arrive at Call Centre";
            Event actual;
            actual = target.CreateEvent(eventType, eventTime, entity);
            Assert.AreEqual(eventTypeS, actual.EventType);
        }

        /// <summary>
        ///A test for CreateEvent SwitchComplete
        ///</summary>
        [TestMethod()]
        public void CreateEventTestSwitchComplete()
        {
            EventFactory target = new EventFactory();
            EEventType eventType = EEventType.SwitchCompleted;
            DateTime eventTime = new DateTime();
            Call entity = null;
            string eventTypeS = "Completed Switch Processing";
            Event actual;
            actual = target.CreateEvent(eventType, eventTime, entity);
            Assert.AreEqual(eventTypeS, actual.EventType);
        }

        /// <summary>
        ///A test for CreateEvent EndReplication
        ///</summary>
        [TestMethod()]
        public void CreateEventTestEndReplication()
        {
            EventFactory target = new EventFactory();
            EEventType eventType = EEventType.EndReplication;
            DateTime eventTime = new DateTime();
            Call entity = null;
            string eventTypeS = "End Replication";
            Event actual;
            actual = target.CreateEvent(eventType, eventTime, entity);
            Assert.AreEqual(eventTypeS, actual.EventType);
        }

        /// <summary>
        ///A test for EventFactory Constructor
        ///</summary>
        [TestMethod()]
        public void EventFactoryConstructorTest()
        {
            EventFactory target = new EventFactory();

            Assert.IsInstanceOfType(target, typeof(EventFactory));
        }
    }
}
