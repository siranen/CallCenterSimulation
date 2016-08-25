using DiscreteEventSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DiscreteEventSimulatorTesting
{
    
    
    /// <summary>
    ///This is a test class for SalesRepresentativeTest and is intended
    ///to contain all SalesRepresentativeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SalesRepresentativeTest
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
        ///A test for SalesRepresentative Constructor
        ///</summary>
        [TestMethod()]
        public void SalesRepresentativeConstructorTest()
        {
            SalesRepType repType = new SalesRepType("Test"); 
            SalesRepresentative target = new SalesRepresentative(repType);

            Assert.IsInstanceOfType(target, typeof(SalesRepresentative));
        }

        /// <summary>
        ///A test for SalesRepresentative Constructor null reptype
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SalesRepresentativeConstructorTestNullRepType()
        {
            SalesRepType repType = null;
            SalesRepresentative target = new SalesRepresentative(repType);
        }

        /// <summary>
        ///A test for GetProcessableProductTypes
        ///</summary>
        [TestMethod()]
        public void GetProcessableProductTypesTest()
        {
            SalesRepType repType = new SalesRepType("Test");
            List<ProductType> expected = new List<ProductType>();
            ProductType pt1 = new ProductType("Test1", 0.1, 0.1);
            ProductType pt2 = new ProductType("Test2", 0.1, 0.1);
            expected.Add(pt1);
            expected.Add(pt2);
            repType.Handles.Add(pt1);
            repType.Handles.Add(pt2);

            SalesRepresentative target = new SalesRepresentative(repType);

            List<ProductType> actual;
            actual = target.GetProcessableProductTypes();

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
    }
}
