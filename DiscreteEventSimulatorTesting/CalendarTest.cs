using DiscreteEventSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DiscreteEventSimulatorTesting
{
    
    
    /// <summary>
    ///This is a test class for CalendarTest and is intended
    ///to contain all CalendarTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CalendarTest
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
        ///A test for Calendar Constructor
        ///</summary>
        [TestMethod()]
        public void CalendarConstructorTest()
        {
            Calendar target = new Calendar();
            Assert.IsInstanceOfType(target, typeof(Calendar));
        }

        /// <summary>
        ///A test for AddEvent first in list
        ///</summary>
        [TestMethod()]
        public void AddEventTestToEmptyCalendar()
        {
            Calendar target = new Calendar();
            Event eventToAdd = new CallArriveEvent(null, DateTime.Now);

            target.AddEvent(eventToAdd);

            Assert.AreEqual(eventToAdd, target.Events[0]);
        }

        /// <summary>
        ///A test for AddEvent second in list
        ///</summary>
        [TestMethod()]
        public void AddEventTestToSecondInList()
        {
            Calendar target = new Calendar();
            Event eventToAdd = new CallArriveEvent(null, DateTime.Now);
            target.AddEvent(eventToAdd);
            Event secondEventToAdd = new CallArriveEvent(null, DateTime.Now.AddMinutes(10));
            target.AddEvent(secondEventToAdd);


            Assert.AreEqual(eventToAdd, target.Events[0]);
            Assert.AreEqual(secondEventToAdd, target.Events[1]);
        }

        /// <summary>
        ///A test for AddEvent middle of list
        ///</summary>
        [TestMethod()]
        public void AddEventTestToMiddlePosition()
        {
            Calendar target = new Calendar();
            Event eventToAdd = new CallArriveEvent(null, DateTime.Now);
            target.AddEvent(eventToAdd);
            Event secondEventToAdd = new CallArriveEvent(null, DateTime.Now.AddMinutes(10));
            target.AddEvent(secondEventToAdd);
            Event middleEvent = new CallArriveEvent(null, DateTime.Now.AddMinutes(5));
            target.AddEvent(middleEvent);


            Assert.AreEqual(eventToAdd, target.Events[0]);
            Assert.AreEqual(secondEventToAdd, target.Events[2]);
            Assert.AreEqual(middleEvent, target.Events[1]);
        }

        /// <summary>
        ///A test for AddEvent null event throws exception
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddEventTestNullEvent()
        {
            Calendar target = new Calendar();
            Event eventToAdd = null;

            target.AddEvent(eventToAdd);

        }

        /// <summary>
        ///A test for NextEvent on list with one item
        ///</summary>
        [TestMethod()]
        public void NextEventTestOneItemCalendar()
        {
            Calendar target = new Calendar();
            Event expected = new CallArriveEvent(null, DateTime.Now);
            target.AddEvent(expected);
            Event actual;
            actual = target.NextEvent();
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(0, target.Events.Count);
        }

        /// <summary>
        ///A test for NextEvent on list with multiple items
        ///</summary>
        [TestMethod()]
        public void NextEventTestMultipleItemCalendar()
        {
            Calendar target = new Calendar();
            Event padItem1 = new CallArriveEvent(null, DateTime.Now.AddMinutes(10));
            target.AddEvent(padItem1);
            Event padItem2 = new CallArriveEvent(null, DateTime.Now.AddMinutes(5));
            target.AddEvent(padItem2);
            Event expected = new CallArriveEvent(null, DateTime.Now);
            target.AddEvent(expected);
            Event actual;
            actual = target.NextEvent();
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(2, target.Events.Count);
        }

        /// <summary>
        ///A test for NextEvent on empty list
        ///</summary>
        [TestMethod()]
        public void NextEventTestEmptyCalendar()
        {
            Calendar target = new Calendar();
            Event expected = null;
            Event actual;
            actual = target.NextEvent();
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(0, target.Events.Count);
        }

        /// <summary>
        ///A test for NextEventOfType on list with item of type CallArrive
        ///</summary>
        [TestMethod()]
        public void NextEventOfTypeTestWithItemMatchCallArrive()
        {
            Calendar target = new Calendar();
            Event callArrive = new CallArriveEvent(null, DateTime.Now.AddMinutes(10));
            target.AddEvent(callArrive);
            Event switchComp = new SwitchCompletedEvent(null, DateTime.Now.AddMinutes(5));
            target.AddEvent(switchComp);
            Event serviceComp = new CompletedServiceEvent(null, DateTime.Now.AddMinutes(20));
            target.AddEvent(serviceComp);
            Event endRep = new EndReplicationEvent(DateTime.Now.AddMinutes(30));
            target.AddEvent(endRep);

            Event actual;
            actual = target.NextEventOfType(EEventType.CallArrive);
            Assert.AreEqual(callArrive, actual);
        }

        /// <summary>
        ///A test for NextEventOfType on list with item of type SwitchComplete
        ///</summary>
        [TestMethod()]
        public void NextEventOfTypeTestWithItemMatchSwitchComplete()
        {
            Calendar target = new Calendar();
            Event callArrive = new CallArriveEvent(null, DateTime.Now.AddMinutes(10));
            target.AddEvent(callArrive);
            Event switchComp = new SwitchCompletedEvent(null, DateTime.Now.AddMinutes(5));
            target.AddEvent(switchComp);
            Event serviceComp = new CompletedServiceEvent(null, DateTime.Now.AddMinutes(20));
            target.AddEvent(serviceComp);
            Event endRep = new EndReplicationEvent(DateTime.Now.AddMinutes(30));
            target.AddEvent(endRep);

            Event actual;
            actual = target.NextEventOfType(EEventType.SwitchCompleted);
            Assert.AreEqual(switchComp, actual);
        }

        /// <summary>
        ///A test for NextEventOfType on list with item of type CompleteService
        ///</summary>
        [TestMethod()]
        public void NextEventOfTypeTestWithItemMatchCompleteService()
        {
            Calendar target = new Calendar();
            Event callArrive = new CallArriveEvent(null, DateTime.Now.AddMinutes(10));
            target.AddEvent(callArrive);
            Event switchComp = new SwitchCompletedEvent(null, DateTime.Now.AddMinutes(5));
            target.AddEvent(switchComp);
            Event serviceComp = new CompletedServiceEvent(null, DateTime.Now.AddMinutes(20));
            target.AddEvent(serviceComp);
            Event endRep = new EndReplicationEvent(DateTime.Now.AddMinutes(30));
            target.AddEvent(endRep);

            Event actual;
            actual = target.NextEventOfType(EEventType.CompletedService);
            Assert.AreEqual(serviceComp, actual);
        }

        /// <summary>
        ///A test for NextEventOfType on list with item of type EndReplication
        ///</summary>
        [TestMethod()]
        public void NextEventOfTypeTestWithItemMatchEndReplication()
        {
            Calendar target = new Calendar();
            Event callArrive = new CallArriveEvent(null, DateTime.Now.AddMinutes(10));
            target.AddEvent(callArrive);
            Event switchComp = new SwitchCompletedEvent(null, DateTime.Now.AddMinutes(5));
            target.AddEvent(switchComp);
            Event serviceComp = new CompletedServiceEvent(null, DateTime.Now.AddMinutes(20));
            target.AddEvent(serviceComp);
            Event endRep = new EndReplicationEvent(DateTime.Now.AddMinutes(30));
            target.AddEvent(endRep);

            Event actual;
            actual = target.NextEventOfType(EEventType.EndReplication);
            Assert.AreEqual(endRep, actual);
        }

        /// <summary>
        ///A test for NextEventOfType on list with item that is not in the calendar
        ///</summary>
        [TestMethod()]
        public void NextEventOfTypeTestWithNoItemMatchReturnsNull()
        {
            Calendar target = new Calendar();
            Event callArrive = new CallArriveEvent(null, DateTime.Now.AddMinutes(10));
            target.AddEvent(callArrive);
            Event switchComp = new SwitchCompletedEvent(null, DateTime.Now.AddMinutes(5));
            Event serviceComp = new CompletedServiceEvent(null, DateTime.Now.AddMinutes(20));
            target.AddEvent(serviceComp);
            Event endRep = new EndReplicationEvent(DateTime.Now.AddMinutes(30));
            target.AddEvent(endRep);

            Event actual;
            actual = target.NextEventOfType(EEventType.SwitchCompleted);
            Assert.IsNull(actual);
        }

        /// <summary>
        ///A test for NextEventOfType on list with enum value greater than possible
        ///</summary>
        [TestMethod()]
        public void NextEventOfTypeTestWithInvalidEnumReturnsNull()
        {
            Calendar target = new Calendar();
            Event callArrive = new CallArriveEvent(null, DateTime.Now.AddMinutes(10));
            target.AddEvent(callArrive);
            Event switchComp = new SwitchCompletedEvent(null, DateTime.Now.AddMinutes(5));
            Event serviceComp = new CompletedServiceEvent(null, DateTime.Now.AddMinutes(20));
            target.AddEvent(serviceComp);
            Event endRep = new EndReplicationEvent(DateTime.Now.AddMinutes(30));
            target.AddEvent(endRep);

            Event actual;
            actual = target.NextEventOfType((EEventType)10);
            Assert.IsNull(actual);
        }

        /// <summary>
        ///A test for RemoveEvent on list with single item
        ///</summary>
        [TestMethod()]
        public void RemoveEventTestSingleItem()
        {
            Calendar target = new Calendar();
            Event toRemove = new CallArriveEvent(null, DateTime.Now);
            target.AddEvent(toRemove);
            target.RemoveEvent(toRemove);
            Assert.AreEqual(0, target.Events.Count);
        }

        /// <summary>
        ///A test for RemoveEvent on list with multiple items
        ///</summary>
        [TestMethod()]
        public void RemoveEventTestMultipleItems()
        {
            Calendar target = new Calendar();
            Event callArrive = new CallArriveEvent(null, DateTime.Now.AddMinutes(10));
            target.AddEvent(callArrive);
            Event switchComp = new SwitchCompletedEvent(null, DateTime.Now.AddMinutes(5));
            target.AddEvent(switchComp);
            Event serviceComp = new CompletedServiceEvent(null, DateTime.Now.AddMinutes(20));
            target.AddEvent(serviceComp);
            Event endRep = new EndReplicationEvent(DateTime.Now.AddMinutes(30));
            target.AddEvent(endRep);
            target.RemoveEvent(callArrive);
            Assert.AreEqual(3, target.Events.Count);
        }

        /// <summary>
        ///A test for RemoveEvent on list with no items
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RemoveEventTestSingleItemNotInCalendar()
        {
            Calendar target = new Calendar();
            Event toRemove = new CallArriveEvent(null, DateTime.Now);
            target.RemoveEvent(toRemove);
            Assert.AreEqual(0, target.Events.Count);
        }

        /// <summary>
        ///A test for RemoveEvent on list with null item
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveEventTestNullItem()
        {
            Calendar target = new Calendar();
            Event toRemove = new CallArriveEvent(null, DateTime.Now);
            target.AddEvent(toRemove);
            target.RemoveEvent(null);
            Assert.AreEqual(0, target.Events.Count);
        }

        /// <summary>
        ///A test for Events
        ///</summary>
        [TestMethod()]
        public void EventsTest()
        {
            Calendar target = new Calendar();
            
            Event callArrive = new CallArriveEvent(null, DateTime.Now.AddMinutes(10));
            target.AddEvent(callArrive);
            Event switchComp = new SwitchCompletedEvent(null, DateTime.Now.AddMinutes(5));
            target.AddEvent(switchComp);
            Event serviceComp = new CompletedServiceEvent(null, DateTime.Now.AddMinutes(20));
            target.AddEvent(serviceComp);
            Event endRep = new EndReplicationEvent(DateTime.Now.AddMinutes(30));
            target.AddEvent(endRep);

            List<Event> expected = new List<Event> { switchComp, callArrive, serviceComp, endRep };

            List<Event> actual;
            actual = target.Events;
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
    }
}
