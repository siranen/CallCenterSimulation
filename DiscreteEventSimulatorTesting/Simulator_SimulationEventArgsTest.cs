using DiscreteEventSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DiscreteEventSimulatorTesting
{
    
    
    /// <summary>
    ///This is a test class for Simulator_SimulationEventArgsTest and is intended
    ///to contain all Simulator_SimulationEventArgsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class Simulator_SimulationEventArgsTest
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
        ///A test for SimulationEventArgs Constructor
        ///</summary>
        [TestMethod()]
        public void Simulator_SimulationEventArgsConstructorTest()
        {
            Event test = new EndReplicationEvent(DateTime.Now);
            Simulator.SimulationEventArgs target = new Simulator.SimulationEventArgs { ProcessedEvent = test };

            Assert.IsInstanceOfType(target, typeof(Simulator.SimulationEventArgs));
            Assert.AreEqual(test, target.ProcessedEvent);
        }

        /// <summary>
        ///A test for ProcessedEvent Normal settings
        ///</summary>
        [TestMethod()]
        public void ProcessedEventTestNormal()
        {
            Simulator.SimulationEventArgs target = new Simulator.SimulationEventArgs();
            Event expected = new EndReplicationEvent(DateTime.Now);
            Event actual;
            target.ProcessedEvent = expected;
            actual = target.ProcessedEvent;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ProcessedEvent null Event
        ///</summary>
        [TestMethod()]
        public void ProcessedEventTestNullEvent()
        {
            Simulator.SimulationEventArgs target = new Simulator.SimulationEventArgs(); 
            Event expected = null;
            Event actual;
            target.ProcessedEvent = expected;
            actual = target.ProcessedEvent;
            Assert.AreEqual(expected, actual);
        }
    }
}
