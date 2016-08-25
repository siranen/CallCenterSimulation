using DiscreteEventSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DiscreteEventSimulatorTesting
{
    
    
    /// <summary>
    ///This is a test class for GovernorTest and is intended
    ///to contain all GovernorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GovernorTest
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
        ///A test for Core Constructor
        ///</summary>
        [TestMethod()]
        public void GovernorConstructorTest()
        {
            Core target = new Core();

            Assert.IsInstanceOfType(target, typeof(Core));
        }

        /// <summary>
        ///A test for AddNewProductType
        ///</summary>
        [TestMethod()]
        public void AddNewProductTypeTest()
        {
            Core target = new Core();
            string typeName = "Test";
            target.AddNewProductType(typeName);

            Assert.AreEqual(typeName, target.ProductTypes[0].TypeName);
        }

        /// <summary>
        ///A test for AddNewProductType with a null string
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNewProductTypeTestNullTypeName()
        {
            Core target = new Core();
            string typeName = null;
            target.AddNewProductType(typeName);

            Assert.AreEqual(typeName, target.ProductTypes[0].TypeName);
        }

        /// <summary>
        ///A test for AddNewProductType with a empty string
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNewProductTypeTestEmptyTypeName()
        {
            Core target = new Core();
            string typeName = string.Empty;
            target.AddNewProductType(typeName);

            Assert.AreEqual(typeName, target.ProductTypes[0].TypeName);
        }

        /// <summary>
        ///A test for AddNewProductType with a duplicate typename
        ///</summary>
        [TestMethod()]
        public void AddNewProductTypeTestDuplicateTypeName()
        {
            Core target = new Core();
            string typeName = "Test";
            target.AddNewProductType(typeName);
            target.AddNewProductType(typeName);

            Assert.AreEqual(typeName, target.ProductTypes[0].TypeName);
            Assert.AreEqual(1, target.ProductTypes.Count);
        }

        /// <summary>
        ///A test for AddNewRepType
        ///</summary>
        [TestMethod()]
        public void AddNewRepTypeTest()
        {
            Core target = new Core();
            string typeName = "Test"; 
            target.AddNewRepType(typeName);
            List<SalesRepType> keys = new List<SalesRepType>(target.RepNums.Keys);
            Assert.AreEqual("Test", keys[0].TypeName);
            Assert.AreEqual(1, target.RepNums[keys[0]]);
        }

        /// <summary>
        ///A test for AddNewRepType null string
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNewRepTypeTestNullString()
        {
            Core target = new Core();
            string typeName = null;
            target.AddNewRepType(typeName);
        }

        /// <summary>
        ///A test for AddNewRepType empty string
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNewRepTypeTestEmptyString()
        {
            Core target = new Core();
            string typeName = string.Empty;
            target.AddNewRepType(typeName);
        }

        /// <summary>
        ///A test for AddNewRepType duplicate string
        ///</summary>
        [TestMethod()]
        public void AddNewRepTypeTestDuplicateTypeName()
        {
            Core target = new Core();
            string typeName = "Test";
            target.AddNewRepType(typeName);
            target.AddNewRepType(typeName);
            List<SalesRepType> keys = new List<SalesRepType>(target.RepNums.Keys);
            Assert.AreEqual("Test", keys[0].TypeName);
            Assert.AreEqual(1, target.RepNums[keys[0]]);
            Assert.AreEqual(1, target.RepNums.Count);
        }

        /// <summary>
        ///A test for AddRepTypeHandle
        ///</summary>
        [TestMethod()]
        public void AddRepTypeHandleTest()
        {
            Core target = new Core();
            SalesRepType repType = new SalesRepType("Test");
            ProductType toHandle = new ProductType("Test", 0.1, 0.1);
            target.RepNums.Add(repType, 1);
            target.AddRepTypeHandle(repType, toHandle);
            List<SalesRepType> keys = new List<SalesRepType>(target.RepNums.Keys);
            Assert.AreEqual(toHandle, keys[0].Handles[0]);
        }

        /// <summary>
        ///A test for AddRepTypeHandle with a null RepType
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddRepTypeHandleTestNullSalesRepType()
        {
            Core target = new Core();
            SalesRepType repType = null;
            ProductType toHandle = new ProductType("Test", 0.1, 0.1);
            target.AddRepTypeHandle(repType, toHandle);
        }

        /// <summary>
        ///A test for AddRepTypeHandle with a null ProductType
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddRepTypeHandleTestNullProductType()
        {
            Core target = new Core();
            SalesRepType repType = new SalesRepType("Test");
            target.RepNums.Add(repType, 1);
            ProductType toHandle = null;
            target.AddRepTypeHandle(repType, toHandle);
        }

        /// <summary>
        ///A test for AddRepTypeHandle with a SalesRepType that is not in the Dictionary
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AddRepTypeHandleTestRepTypeNotInDictionary()
        {
            Core target = new Core();
            SalesRepType repType = new SalesRepType("Test");
            ProductType toHandle = new ProductType("Test", 0.1, 0.1);
            //target.RepNums.Add(repType, 1); // Dont add to dictionary
            target.AddRepTypeHandle(repType, toHandle);
        }

        /// <summary>
        ///A test for IsSimulationValid invalid
        ///</summary>
        [TestMethod()]
        public void IsSimulationValidTestInvalid()
        {
            Core target = new Core();
            bool expected = false;
            bool actual;
            actual = target.IsSimulationValid();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsSimulationValid valid
        ///</summary>
        [TestMethod()]
        public void IsSimulationValidTestValid()
        {
            Core target = new Core();
            bool expected = true;
            SalesRepType srt = new SalesRepType("Test");
            target.RepNums.Add(srt, 1);
            target.ProductTypes.Add(new ProductType("Test", 0.1, 1));
            List<SalesRepType> keys = new List<SalesRepType>(target.RepNums.Keys);
            keys[0].Handles.Add(target.ProductTypes[0]);
            bool actual;
            actual = target.IsSimulationValid();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for RemoveProductType with a product type that is in the list
        ///</summary>
        [TestMethod()]
        public void RemoveProductTypeTest()
        {
            Core target = new Core();
            ProductType type = new ProductType("Test", 0.1, 0.1);
            target.ProductTypes.Add(type);
            target.RemoveProductType(type);

            Assert.AreEqual(0, target.ProductTypes.Count);
        }

        /// <summary>
        ///A test for RemoveProductType with a null product type
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveProductTypeTestNullProductType()
        {
            Core target = new Core();
            ProductType type = null;
            target.RemoveProductType(type);
        }

        /// <summary>
        ///A test for RemoveProductType with a product type that is not in the list
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RemoveProductTypeTestProductTypeNotInList()
        {
            Core target = new Core();
            ProductType type = new ProductType("Test", 0.1, 0.1);
            target.RemoveProductType(type);
        }

        /// <summary>
        ///A test for RemoveRepType
        ///</summary>
        [TestMethod()]
        public void RemoveRepTypeTest()
        {
            Core target = new Core();
            SalesRepType type = new SalesRepType("Test");
            target.RepNums.Add(type, 1);
            target.RemoveRepType(type);
            Assert.AreEqual(0, target.RepNums.Count);
        }

        /// <summary>
        ///A test for RemoveRepType with a null SalesRepType
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveRepTypeTestNullRepType()
        {
            Core target = new Core();
            SalesRepType type = null;
            target.RemoveRepType(type);
        }

        /// <summary>
        ///A test for RemoveRepType with a SalesRepType that is not in the dictionary
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RemoveRepTypeTestSalesRepTypeNotInDictionary()
        {
            Core target = new Core();
            SalesRepType type = new SalesRepType("Test");
            target.RemoveRepType(type);
        }

        /// <summary>
        ///A test for RemoveRepTypeHandle
        ///</summary>
        [TestMethod()]
        public void RemoveRepTypeHandleTest()
        {
            Core target = new Core();
            SalesRepType repType = new SalesRepType("Test");
            ProductType toRemove = new ProductType("Test", 0.1, 0.1);
            repType.Handles.Add(toRemove);
            target.RepNums.Add(repType, 1);
            target.RemoveRepTypeHandle(repType, toRemove);

            Assert.AreEqual(0, repType.Handles.Count);
        }

        /// <summary>
        ///A test for RemoveRepTypeHandle null SalesRepType
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveRepTypeHandleTestNullSalesRepType()
        {
            Core target = new Core();
            SalesRepType repType = null;
            ProductType toRemove = new ProductType("Test", 0.1, 0.1);
            target.RemoveRepTypeHandle(repType, toRemove);
        }

        /// <summary>
        ///A test for RemoveRepTypeHandle null ProductType
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveRepTypeHandleTestNullProductType()
        {
            Core target = new Core();
            SalesRepType repType = new SalesRepType("Test");
            ProductType toRemove = null;
            target.RemoveRepTypeHandle(repType, toRemove);
        }

        /// <summary>
        ///A test for RemoveRepTypeHandle SalesRepType not in dictionary
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RemoveRepTypeHandleTestSalesRepTypeNotInDictionary()
        {
            Core target = new Core();
            SalesRepType repType = new SalesRepType("Test");
            //Dont add the reptype to the dictionary
            ProductType toRemove = new ProductType("Test", 0.1, 0.1);
            target.RemoveRepTypeHandle(repType, toRemove);
        }

        /// <summary>
        ///A test for RemoveRepTypeHandle SalesRepType not in dictionary
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RemoveRepTypeHandleTestProductTypeNotInHandlesList()
        {
            Core target = new Core();
            SalesRepType repType = new SalesRepType("Test");
            target.RepNums.Add(repType, 1);
            ProductType toRemove = new ProductType("Dave", 0.1, 0.1);
            target.RemoveRepTypeHandle(repType, toRemove);
        }

        /// <summary>
        ///A test for Reset
        ///</summary>
        [TestMethod()]
        public void ResetTest()
        {
            Core target = new Core();
            DateTime now = DateTime.Now;
            target.BeginTime = now;
            target.Reset();
            Assert.AreNotEqual(now, target.BeginTime);
        }

        /// <summary>
        ///A test for SetProductTypeSettings
        ///</summary>
        [TestMethod()]
        public void SetProductTypeSettingsTest()
        {
            Core target = new Core();
            ProductType pt = new ProductType("Test", 0.1, 0.1);
            target.ProductTypes.Add(pt);
            string typeName = "Test"; 
            double probability = 1.0; 
            double processingMultiplier = 1.0; 
            target.SetProductTypeSettings(typeName, probability, processingMultiplier);

            Assert.AreEqual(probability, pt.ProductTypeProbability);
            Assert.AreEqual(processingMultiplier, pt.ProcessingDelayMultiplier);
        }

        /// <summary>
        ///A test for SetProductTypeSettings null string
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetProductTypeSettingsTestNullString()
        {
            Core target = new Core();
            ProductType pt = new ProductType("Test", 0.1, 0.1);
            target.ProductTypes.Add(pt);
            string typeName = null;
            double probability = 1.0;
            double processingMultiplier = 1.0;
            target.SetProductTypeSettings(typeName, probability, processingMultiplier);
        }

        /// <summary>
        ///A test for SetProductTypeSettings empty string
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetProductTypeSettingsTestEmptyString()
        {
            Core target = new Core();
            ProductType pt = new ProductType("Test", 0.1, 0.1);
            target.ProductTypes.Add(pt);
            string typeName = string.Empty;
            double probability = 1.0;
            double processingMultiplier = 1.0;
            target.SetProductTypeSettings(typeName, probability, processingMultiplier);
        }

        /// <summary>
        ///A test for SetProductTypeSettings typename that has no matching type
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SetProductTypeSettingsTestNoMatchingTypeName()
        {
            Core target = new Core();
            ProductType pt = new ProductType("Test", 0.1, 0.1);
            target.ProductTypes.Add(pt);
            string typeName = "Dave";
            double probability = 1.0;
            double processingMultiplier = 1.0;
            target.SetProductTypeSettings(typeName, probability, processingMultiplier);
        }

        /// <summary>
        ///A test for TotalProbability
        ///</summary>
        [TestMethod()]
        public void TotalProbabilityTest()
        {
            Core target = new Core();
            target.ProductTypes.Add(new ProductType("Test", 0.1, 0.16));
            target.ProductTypes.Add(new ProductType("Test2", 0.2, 0.16));
            double expected = 0.32; 
            double actual;
            actual = target.TotalProbability();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for TotalProbability with no product types
        ///</summary>
        [TestMethod()]
        public void TotalProbabilityTestNoProductTypes()
        {
            Core target = new Core();
            double expected = 0;
            double actual;
            actual = target.TotalProbability();
            Assert.AreEqual(expected, actual);
        }
    }
}
