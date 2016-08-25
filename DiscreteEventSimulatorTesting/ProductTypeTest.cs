using DiscreteEventSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DiscreteEventSimulatorTesting
{
    
    
    /// <summary>
    ///This is a test class for ProductTypeTest and is intended
    ///to contain all ProductTypeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProductTypeTest
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
        ///A test for ProductType Constructor
        ///</summary>
        [TestMethod()]
        public void ProductTypeConstructorTest()
        {
            string typeName = "Test";
            double processingDelayMultiplier = 0.1;
            double productTypeProbability = 0.1;
            ProductType target = new ProductType(typeName, processingDelayMultiplier, productTypeProbability);

            Assert.IsInstanceOfType(target, typeof(ProductType));
        }

        /// <summary>
        ///A test for ProductType Constructor null typename
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProductTypeConstructorTestNullTypeName()
        {
            string typeName = null;
            double processingDelayMultiplier = 0.1;
            double productTypeProbability = 0.1;
            ProductType target = new ProductType(typeName, processingDelayMultiplier, productTypeProbability);
        }

        /// <summary>
        ///A test for ProductType Constructor null typename
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProductTypeConstructorTestEmptyTypeName()
        {
            string typeName = string.Empty;
            double processingDelayMultiplier = 0.1;
            double productTypeProbability = 0.1;
            ProductType target = new ProductType(typeName, processingDelayMultiplier, productTypeProbability);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            string typeName = "Test";
            double processingDelayMultiplier = 0.1;
            double productTypeProbability = 0.1;
            ProductType target = new ProductType(typeName, processingDelayMultiplier, productTypeProbability);
            string expected = "Name: Test, PDM: 0.1, PTP: 0.1";
            string actual;
            actual = target.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
