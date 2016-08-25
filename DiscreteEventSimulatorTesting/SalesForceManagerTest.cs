using DiscreteEventSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DiscreteEventSimulatorTesting
{
    
    
    /// <summary>
    ///This is a test class for SalesForceManagerTest and is intended
    ///to contain all SalesForceManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SalesForceManagerTest
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
        ///A test for SalesForceManager Constructor
        ///</summary>
        [TestMethod()]
        public void SalesForceManagerConstructorTest()
        {
            Dictionary<SalesRepType, int> numberOfReps = new Dictionary<SalesRepType,int>();
            numberOfReps.Add(new SalesRepType("Test"), 1);
            SalesForceManager target = new SalesForceManager(numberOfReps);
            Assert.IsInstanceOfType(target, typeof(SalesForceManager));
        }

        /// <summary>
        ///A test for SalesForceManager Constructor null dictionary
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SalesForceManagerConstructorTestNullDictionary()
        {
            Dictionary<SalesRepType, int> numberOfReps = null;
            SalesForceManager target = new SalesForceManager(numberOfReps);
        }

        /// <summary>
        ///A test for SalesForceManager Constructor empty dictionary
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SalesForceManagerConstructorTestEmptyDictionary()
        {
            Dictionary<SalesRepType, int> numberOfReps = new Dictionary<SalesRepType, int>();
            SalesForceManager target = new SalesForceManager(numberOfReps);
        }

        /// <summary>
        ///A test for AddSalesRepresentative
        ///</summary>
        [TestMethod()]
        public void AddSalesRepresentativeTest()
        {
            Dictionary<SalesRepType, int> numberOfReps = new Dictionary<SalesRepType, int>();
            SalesRepType srt = new SalesRepType("Test");
            numberOfReps.Add(srt, 1);
            SalesForceManager target = new SalesForceManager(numberOfReps);
            SalesRepresentative representative = new SalesRepresentative(srt);
            target.AddSalesRepresentative(representative);

            Assert.AreEqual(representative, target.SalesForce[srt][1]);
            Assert.AreEqual(2, target.SalesForce[srt].Count);
        }

        /// <summary>
        ///A test for AddSalesRepresentative null representative
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddSalesRepresentativeTestNullRepresentative()
        {
            Dictionary<SalesRepType, int> numberOfReps = new Dictionary<SalesRepType, int>();
            SalesRepType srt = new SalesRepType("Test");
            numberOfReps.Add(srt, 1);
            SalesForceManager target = new SalesForceManager(numberOfReps);
            SalesRepresentative representative = null;
            target.AddSalesRepresentative(representative);

            Assert.AreEqual(representative, target.SalesForce[srt][1]);
        }

        /// <summary>
        ///A test for AddSalesRepresentative SalesRepType not in dictionary
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddSalesRepresentativeTestNotInDictionary()
        {
            Dictionary<SalesRepType, int> numberOfReps = new Dictionary<SalesRepType, int>();
            SalesRepType srt = new SalesRepType("Test");
            numberOfReps.Add(srt, 1);
            SalesRepType notInDict = new SalesRepType("Hello");
            SalesForceManager target = new SalesForceManager(numberOfReps);
            SalesRepresentative representative = new SalesRepresentative(notInDict);
            target.AddSalesRepresentative(representative);

        }

        /// <summary>
        ///A test for GetFreeSalesRep
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        public void GetFreeSalesRepTest()
        {
            Dictionary<SalesRepType, int> numberOfReps = new Dictionary<SalesRepType, int>();
            SalesRepType srt = new SalesRepType("Test");
            numberOfReps.Add(srt, 1);
            SalesForceManager_Accessor target = new SalesForceManager_Accessor(numberOfReps);
            SalesRepType repType = srt;
            SalesRepresentative expected = target.SalesForce[repType][0]; 
            SalesRepresentative actual;
            actual = target.GetFreeSalesRep(repType);
            Assert.AreEqual(expected, actual);

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetFreeSalesRep null repType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetFreeSalesRepTestNullRepType()
        {
            Dictionary<SalesRepType, int> numberOfReps = new Dictionary<SalesRepType, int>();
            SalesRepType srt = new SalesRepType("Test");
            numberOfReps.Add(srt, 1);
            SalesForceManager_Accessor target = new SalesForceManager_Accessor(numberOfReps);
            SalesRepType repType = null;
            SalesRepresentative expected = target.SalesForce[repType][0];
            SalesRepresentative actual;
            actual = target.GetFreeSalesRep(repType);
        }

        /// <summary>
        ///A test for GetFreeSalesRep with type not in Dictionary
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetFreeSalesRepTestTypeNotInDictionary()
        {
            Dictionary<SalesRepType, int> numberOfReps = new Dictionary<SalesRepType, int>();
            SalesRepType srt = new SalesRepType("Test");
            numberOfReps.Add(srt, 1);
            SalesRepType sType = new SalesRepType("Dave");
            SalesForceManager_Accessor target = new SalesForceManager_Accessor(numberOfReps);
            SalesRepType repType = sType;
            SalesRepresentative actual;
            actual = target.GetFreeSalesRep(repType);
        }

        /// <summary>
        ///A test for GetRepresentativeForProductType
        ///</summary>
        [TestMethod()]
        public void GetRepresentativeForProductTypeTest()
        {
            Dictionary<SalesRepType, int> numberOfReps = new Dictionary<SalesRepType, int>();
            SalesRepType srt = new SalesRepType("Test");
            ProductType ptype = new ProductType("Test", 0.1, 0.1);
            srt.Handles.Add(ptype);
            numberOfReps.Add(srt, 1);
            SalesForceManager target = new SalesForceManager(numberOfReps);
            SalesRepresentative expected = target.SalesForce[srt][0];
            SalesRepresentative actual;
            actual = target.GetRepresentativeForProductType(ptype);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for RemoveRepresentative
        ///</summary>
        [TestMethod()]
        public void RemoveRepresentativeTest()
        {
            Dictionary<SalesRepType, int> numberOfReps = new Dictionary<SalesRepType, int>();
            SalesRepType srt = new SalesRepType("Test");
            numberOfReps.Add(srt, 2);
            SalesForceManager target = new SalesForceManager(numberOfReps);
            SalesRepresentative representativeToRemove = target.SalesForce[srt][0];
            target.RemoveRepresentative(representativeToRemove);

            Assert.AreEqual(1, target.SalesForce[srt].Count);
        }

        /// <summary>
        ///A test for RemoveRepresentative null Representative
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveRepresentativeTestNullRepresentative()
        {
            Dictionary<SalesRepType, int> numberOfReps = new Dictionary<SalesRepType, int>();
            SalesRepType srt = new SalesRepType("Test");
            numberOfReps.Add(srt, 2);
            SalesForceManager target = new SalesForceManager(numberOfReps);
            SalesRepresentative representativeToRemove = null;
            target.RemoveRepresentative(representativeToRemove);
        }

        /// <summary>
        ///A test for RemoveRepresentative representative type not in dictionary
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RemoveRepresentativeTestNotInDictionary()
        {
            Dictionary<SalesRepType, int> numberOfReps = new Dictionary<SalesRepType, int>();
            SalesRepType srt = new SalesRepType("Test");
            numberOfReps.Add(srt, 2);
            SalesForceManager target = new SalesForceManager(numberOfReps);
            SalesRepresentative representativeToRemove = new SalesRepresentative(new SalesRepType("James"));
            target.RemoveRepresentative(representativeToRemove);
        }

        /// <summary>
        ///A test for RepresentativesBusy
        ///</summary>
        [TestMethod()]
        public void RepresentativesBusyTest()
        {
            Dictionary<SalesRepType, int> numberOfReps = new Dictionary<SalesRepType, int>();
            SalesRepType srt = new SalesRepType("Test");
            numberOfReps.Add(srt, 2);
            SalesForceManager target = new SalesForceManager(numberOfReps);
            target.SalesForce[srt][0].CurrentlyProcessing = new Call((uint)1);
            SalesRepType repType = srt;
            int expected = 1;
            int actual;
            actual = target.RepresentativesBusy(repType);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for RepresentativesBusy null repType
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RepresentativesBusyTestNullRepType()
        {
            Dictionary<SalesRepType, int> numberOfReps = new Dictionary<SalesRepType, int>();
            SalesRepType srt = new SalesRepType("Test");
            numberOfReps.Add(srt, 2);
            SalesForceManager target = new SalesForceManager(numberOfReps);
            target.SalesForce[srt][0].CurrentlyProcessing = new Call((uint)1);
            SalesRepType repType = null;
            int actual;
            actual = target.RepresentativesBusy(repType);
        }

        /// <summary>
        ///A test for RepresentativesBusy Not In dictionary
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RepresentativesBusyTestNotInDictionary()
        {
            Dictionary<SalesRepType, int> numberOfReps = new Dictionary<SalesRepType, int>();
            SalesRepType srt = new SalesRepType("Test");
            numberOfReps.Add(srt, 2);
            SalesForceManager target = new SalesForceManager(numberOfReps);
            target.SalesForce[srt][0].CurrentlyProcessing = new Call((uint)1);
            SalesRepType repType = new SalesRepType("Dave");
            int actual;
            actual = target.RepresentativesBusy(repType);
        }

        /// <summary>
        ///A test for RepresentativesOfType
        ///</summary>
        [TestMethod()]
        public void RepresentativesOfTypeTest()
        {
            Dictionary<SalesRepType, int> numberOfReps = new Dictionary<SalesRepType, int>();
            SalesRepType srt = new SalesRepType("Test");
            numberOfReps.Add(srt, 2);
            SalesForceManager target = new SalesForceManager(numberOfReps);
            SalesRepType repType = srt;
            int expected = 2;
            int actual;
            actual = target.RepresentativesOfType(repType);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for RepresentativesOfType null repType
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RepresentativesOfTypeTestNullRepType()
        {
            Dictionary<SalesRepType, int> numberOfReps = new Dictionary<SalesRepType, int>();
            SalesRepType srt = new SalesRepType("Test");
            numberOfReps.Add(srt, 2);
            SalesForceManager target = new SalesForceManager(numberOfReps);
            SalesRepType repType = null;
            int actual;
            actual = target.RepresentativesOfType(repType);
        }

        /// <summary>
        ///A test for RepresentativesOfType Not In dictionary
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RepresentativesOfTypeTestNotInDictionary()
        {
            Dictionary<SalesRepType, int> numberOfReps = new Dictionary<SalesRepType, int>();
            SalesRepType srt = new SalesRepType("Test");
            numberOfReps.Add(srt, 2);
            SalesForceManager target = new SalesForceManager(numberOfReps);
            SalesRepType repType = new SalesRepType("Dave");
            int actual;
            actual = target.RepresentativesOfType(repType);
        }
    }
}
