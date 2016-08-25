using DiscreteEventSimulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;

namespace DiscreteEventSimulatorTesting
{
    
    
    /// <summary>
    ///This is a test class for InitialDisplayTest and is intended
    ///to contain all InitialDisplayTest Unit Tests
    ///</summary>
    [TestClass()]
    public class InitialDisplayTest
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
        ///A test for InitialDisplay Constructor
        ///</summary>
        [TestMethod()]
        public void InitialDisplayConstructorTest()
        {
            InitialDisplay target = new InitialDisplay();

            Assert.IsInstanceOfType(target, typeof(InitialDisplay));
        }

        /// <summary>
        ///A test for CreatePTGroupBox
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        public void CreatePTGroupBoxTest()
        {
            InitialDisplay_Accessor target = new InitialDisplay_Accessor();
            int y = 0;
            ProductType pt = new ProductType("Test", 0.1, 0.1);
            
            GroupBox actual;
            actual = target.CreatePTGroupBox(y, pt);
            Assert.AreEqual(pt.TypeName, actual.Text);
        }

        /// <summary>
        ///A test for CreatePTGroupBox given null ProductType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatePTGroupBoxTestNullProductType()
        {
            InitialDisplay_Accessor target = new InitialDisplay_Accessor();
            int y = 0;
            ProductType pt = null;

            GroupBox actual;
            actual = target.CreatePTGroupBox(y, pt);
            

        }

        /// <summary>
        ///A test for CreatePTProbabilityLabel
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        public void CreatePTProbabilityLabelTest()
        {
            InitialDisplay_Accessor target = new InitialDisplay_Accessor();
            
            Label actual;
            actual = target.CreatePTProbabilityLabel();
            Assert.AreEqual("lblProbability", actual.Name);
        }

        /// <summary>
        ///A test for CreatePTProbabilityPercentLabel
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        public void CreatePTProbabilityPercentLabelTest()
        {
            InitialDisplay_Accessor target = new InitialDisplay_Accessor(); 
            TrackBar tbProbability = new TrackBar(); 

            Label actual;
            actual = target.CreatePTProbabilityPercentLabel(tbProbability);
            Assert.AreEqual("lblProbabilityPercent", actual.Name);
        }

        /// <summary>
        ///A test for CreatePTProbabilityPercentLabel with a null trackbar
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatePTProbabilityPercentLabelTestNullTrackbar()
        {
            InitialDisplay_Accessor target = new InitialDisplay_Accessor();
            TrackBar tbProbability = null;

            Label actual;
            actual = target.CreatePTProbabilityPercentLabel(tbProbability);
           
        }

        /// <summary>
        ///A test for CreatePTProbabilityTrackBar
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        public void CreatePTProbabilityTrackBarTest()
        {
            InitialDisplay_Accessor target = new InitialDisplay_Accessor();
            ProductType pt = new ProductType("Test", 0.1, 0.1);
            
            TrackBar actual;
            actual = target.CreatePTProbabilityTrackBar(pt);
            Assert.AreEqual("tbProbability", actual.Name);
            Assert.AreEqual(pt.ProductTypeProbability * 100, actual.Value);
        }

        /// <summary>
        ///A test for CreatePTProbabilityTrackBar null product type
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatePTProbabilityTrackBarTestNullProductType()
        {
            InitialDisplay_Accessor target = new InitialDisplay_Accessor();
            ProductType pt = null;

            TrackBar actual;
            actual = target.CreatePTProbabilityTrackBar(pt);
        }

        /// <summary>
        ///A test for CreatePTProcessDelayLabel
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        public void CreatePTProcessDelayLabelTest()
        {
            InitialDisplay_Accessor target = new InitialDisplay_Accessor(); 
            
            Label actual;
            actual = target.CreatePTProcessDelayLabel();
            Assert.AreEqual("lblProcessDelay", actual.Name);
        }

        /// <summary>
        ///A test for CreatePTProcessDelayNuD
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        public void CreatePTProcessDelayNuDTest()
        {
            InitialDisplay_Accessor target = new InitialDisplay_Accessor();
            ProductType pt = new ProductType("Test", 0.1, 0.1);
            
            NumericUpDown actual;
            actual = target.CreatePTProcessDelayNuD(pt);
            Assert.AreEqual("nudProcessDelay", actual.Name);
            Assert.AreEqual(pt.ProcessingDelayMultiplier, (double)actual.Value);
        }

        /// <summary>
        ///A test for CreatePTProcessDelayNuD null productType
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatePTProcessDelayNuDTestNullProductType()
        {
            InitialDisplay_Accessor target = new InitialDisplay_Accessor();
            ProductType pt = null;

            NumericUpDown actual;
            actual = target.CreatePTProcessDelayNuD(pt);
        }

        /// <summary>
        ///A test for CreatePTProcessDelayRangeLabel
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        public void CreatePTProcessDelayRangeLabelTest()
        {
            InitialDisplay_Accessor target = new InitialDisplay_Accessor();
            NumericUpDown nudProcessDelay = new NumericUpDown(); 
            
            Label actual;
            actual = target.CreatePTProcessDelayRangeLabel(nudProcessDelay);
            Assert.AreEqual("lblProcessRange", actual.Name);
        }

        /// <summary>
        ///A test for CreatePTProcessDelayRangeLabel with null NumericUpDown
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreatePTProcessDelayRangeLabelTestNullNuD()
        {
            InitialDisplay_Accessor target = new InitialDisplay_Accessor();
            NumericUpDown nudProcessDelay = null;

            Label actual;
            actual = target.CreatePTProcessDelayRangeLabel(nudProcessDelay);
            
        }

        /// <summary>
        ///A test for CreatePTRemoveButton
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        public void CreatePTRemoveButtonTest()
        {
            InitialDisplay_Accessor target = new InitialDisplay_Accessor();
            
            Button actual;
            actual = target.CreatePTRemoveButton();
            Assert.AreEqual("btnRemoveProductType", actual.Name);
        }
              
        /// <summary>
        ///A test for TimeSpanToString only seconds
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        public void TimeSpanToStringTestSecondsOnly()
        {
            InitialDisplay_Accessor target = new InitialDisplay_Accessor(); 
            TimeSpan ts = new TimeSpan(0,0,1); 
            string expected = "1 s"; 
            string actual;
            actual = target.TimeSpanToString(ts);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for TimeSpanToString only minutes
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        public void TimeSpanToStringTestMinutesOnly()
        {
            InitialDisplay_Accessor target = new InitialDisplay_Accessor();
            TimeSpan ts = new TimeSpan(0, 1, 0);
            string expected = "1 m ";
            string actual;
            actual = target.TimeSpanToString(ts);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for TimeSpanToString only hours
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        public void TimeSpanToStringTestHoursOnly()
        {
            InitialDisplay_Accessor target = new InitialDisplay_Accessor();
            TimeSpan ts = new TimeSpan(1, 0, 0);
            string expected = "1 h ";
            string actual;
            actual = target.TimeSpanToString(ts);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for TimeSpanToString only days
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        public void TimeSpanToStringTestDaysOnly()
        {
            InitialDisplay_Accessor target = new InitialDisplay_Accessor();
            TimeSpan ts = new TimeSpan(1, 0, 0, 0);
            string expected = "1 days ";
            string actual;
            actual = target.TimeSpanToString(ts);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for TimeSpanToString seconds and minutes
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        public void TimeSpanToStringTestMinutesSeconds()
        {
            InitialDisplay_Accessor target = new InitialDisplay_Accessor();
            TimeSpan ts = new TimeSpan(0, 1, 1);
            string expected = "1 m 1 s";
            string actual;
            actual = target.TimeSpanToString(ts);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for TimeSpanToString seconds and minutes and hours
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        public void TimeSpanToStringTestHoursMinutesSeconds()
        {
            InitialDisplay_Accessor target = new InitialDisplay_Accessor();
            TimeSpan ts = new TimeSpan(1, 1, 1);
            string expected = "1 h 1 m 1 s";
            string actual;
            actual = target.TimeSpanToString(ts);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for TimeSpanToString seconds and minutes and hours and days
        ///</summary>
        [TestMethod()]
        [DeploymentItem("DiscreteEventSimulator.exe")]
        public void TimeSpanToStringTestDaysHoursMinutesSeconds()
        {
            InitialDisplay_Accessor target = new InitialDisplay_Accessor();
            TimeSpan ts = new TimeSpan(1, 1, 1, 1);
            string expected = "1 days 1 h 1 m 1 s";
            string actual;
            actual = target.TimeSpanToString(ts);
            Assert.AreEqual(expected, actual);
        }
    }
}
