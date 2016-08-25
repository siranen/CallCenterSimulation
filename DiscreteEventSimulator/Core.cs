using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Windows.Forms;

namespace DiscreteEventSimulator
{
    /// <summary>
    /// Creates and executes Simulator runs
    /// </summary>
    public class Core
    {
        //-------------------------------------------------------------------
        //- CONSTANTS                                                       -
        //-------------------------------------------------------------------
        private const int SIMULATION_SLEEP = 100;
        private const double DEFAULT_MULTIPLIER = 1.0;

        //-------------------------------------------------------------------
        //- FIELDS                                                          -
        //-------------------------------------------------------------------
        private double callArrivalMultiplier;
        private double switchDelayMultiplier;
        private int maxQueueLength;
        private bool singleQueueLength;
        private DateTime beginTime;
        private TimeSpan duration;
        private TimeSpan excessiveWait;
        private List<ProductType> productTypes;
        private Dictionary<SalesRepType, int> repNums;

        //-------------------------------------------------------------------
        //- PROPERTIES                                                      -
        //-------------------------------------------------------------------
        /// <summary>
        /// Gets the Dictionary containing the number of representatives by type
        /// </summary>
        public Dictionary<SalesRepType, int> RepNums
        {
            get { return repNums; }
        }

        /// <summary>
        /// Gets the list of product types
        /// </summary>
        public List<ProductType> ProductTypes
        {
            get { return productTypes; }
        }

        /// <summary>
        /// Gets/Sets the excessive wait threshold value
        /// </summary>
        public TimeSpan ExcessiveWait
        {
            get { return excessiveWait; }
            set { excessiveWait = value; }
        }

        /// <summary>
        /// Gets/Sets the duration that a simulation run lasts for
        /// </summary>
        public TimeSpan Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        /// <summary>
        /// Gets/Sets the time of day a simulation run starts at
        /// </summary>
        public DateTime BeginTime
        {
            get { return beginTime; }
            set { beginTime = value; }
        }

        /// <summary>
        /// Gets/Sets the maximum queue length of a simulation run
        /// </summary>
        public int MaxQueueLength
        {
            get { return maxQueueLength; }
            set { maxQueueLength = value; }
        }

        /// <summary>
        /// Gets/Sets the SingleQueueLength value of a simulation run
        /// </summary>
        public bool SingleQueueLength
        {
            get { return singleQueueLength; }
            set { singleQueueLength = value; }
        }

        /// <summary>
        /// Gets/Sets the call arrival multiplier for a simulation run
        /// </summary>
        public double CallArrivalMultiplier
        {
            get { return callArrivalMultiplier; }
            set { callArrivalMultiplier = value; }
        }

        /// <summary>
        /// Gets/Sets the switch delay multiplier for a simulation run
        /// </summary>
        public double SwitchDelayMultiplier
        {
            get { return switchDelayMultiplier; }
            set { switchDelayMultiplier = value; }
        }

        //-------------------------------------------------------------------
        //- Events                                                          -
        //-------------------------------------------------------------------
        /// <summary>
        /// Triggered when the List of ProductTypes changes or is altered
        /// </summary>
        public event EventHandler ProductTypeChanged;
        /// <summary>
        /// Triggered when the Dictionary of RepTypes changes or is altered
        /// </summary>
        public event EventHandler RepTypesChanged;
        /// <summary>
        /// Triggered when any of the main settings are changed
        /// </summary>
        public event EventHandler MainSettingsChanged;
        //-------------------------------------------------------------------
        //- METHODS                                                         -
        //-------------------------------------------------------------------
        /// <summary>
        /// Creates a new instance of the Core class
        /// </summary>
        public Core()
        {
            Reset();
        }

        /// <summary>
        /// Resets the Governors settings to be fresh
        /// </summary>
        public void Reset()
        {
            //Create the empty product types list
            productTypes = new List<ProductType>();

            //Create the empty repNums dictionary
            repNums = new Dictionary<SalesRepType, int>();

            //Set the other values to defaults
            beginTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            duration = new TimeSpan();
            excessiveWait = new TimeSpan();

            //Set the multipliers to default values
            callArrivalMultiplier = DEFAULT_MULTIPLIER;
            switchDelayMultiplier = DEFAULT_MULTIPLIER;

            singleQueueLength = false;
            maxQueueLength = 0;


            OnRepTypesChanged();
            OnProductTypesChanged();
            OnMainSettingsChanged();
        }

        /// <summary>
        /// Adds a new ProductType with the given type name and default values
        /// </summary>
        /// <param name="typeName">The name of the ProductType to add</param>
        public void AddNewProductType(string typeName)
        {
            //Check that a ProductType with this name does not already exists
            if (productTypes.Count(pt => pt.TypeName == typeName) == 0)
            {
                double probability = 0;

                //if this is the first product type set the probability to 100%
                if (productTypes.Count == 0)
                {
                    probability = 1;
                }

                //Create the new product type and add it to the list
                productTypes.Add(new ProductType(typeName, 0.01, probability));

                //Trigger the ProductTypesChanged event
                OnProductTypesChanged();
            }
        }

        /// <summary>
        /// Removes the given product type from the List
        /// </summary>
        /// <param name="type">The ProducType to Remove</param>
        public void RemoveProductType(ProductType type)
        {
            // Check that the given ProductType is not null
            if (type != null)
            {
                //Check that the given productType actually is in the list
                if (productTypes.Contains(type))
                {
                    //Remove the product type
                    productTypes.Remove(type);

                    //Loop foreach SalesRepType in the Dictionary
                    foreach (SalesRepType srt in repNums.Keys)
                    {
                        //If the SalesRepType had this ProductType in its handles list
                        if (srt.Handles.Contains(type))
                        {
                            //Remove the ProductType from the handles list
                            srt.Handles.Remove(type);
                        }
                    }

                    //Trigger the RepTypesChanged event
                    OnRepTypesChanged();
                    //Trigger the ProductTypesChanged event
                    OnProductTypesChanged();
                }
                else // The given ProductType was not in the list
                {
                    throw new ArgumentOutOfRangeException("type", "Attempted to remove a ProductType that was not in the productTypes list");
                }
            }
            else // The given ProductType is null
            {
                throw new ArgumentNullException("type", "Attempted to pass null ProductType to Core.RemoveProductType");
            }
        }

        /// <summary>
        /// Adds a new SalesRepType with the given type name and number of reps set to 1
        /// </summary>
        /// <param name="typeName">The name of the SalesRepType</param>
        public void AddNewRepType(string typeName)
        {
            // Check that the given type name is not null
            if (typeName != null && typeName != string.Empty)
            {
                // If a SalesRepType with the given name is not already in the list
                if (repNums.Keys.Count(srt => srt.TypeName == typeName) == 0)
                {
                    //Add the SalesRepType as a new key in the dictionary
                    repNums.Add(new SalesRepType(typeName), 1);

                    //Trigger the RepTypesChanged event
                    OnRepTypesChanged();
                }
            }
            else
            {
                throw new ArgumentNullException("typeName", "Attempted to pass null or empty typeName to Core.AddNewRepType");
            }
        }

        /// <summary>
        /// Removes the given SalesRepType from the dictionary
        /// </summary>
        /// <param name="type">The SalesRepType to remove</param>
        public void RemoveRepType(SalesRepType type)
        {
            // Check that the SalesRepType is actually in the Dictionary
            if (repNums.Keys.Contains(type))
            {
                // Remove the repType
                repNums.Remove(type);

                // Trigger the RepTypesChanged event
                OnRepTypesChanged();
            }
            else // The given SalesRepType was not in the list
            {
                throw new ArgumentOutOfRangeException("type", "Attempted to remove a SalesRepType from the repNums dictionary that was not present");
            }
        }

        /// <summary>
        /// Adds the given ProductType to the SalesRepTypes Handles list
        /// </summary>
        /// <param name="repType">The SalesRepType that the ProductType is being added to</param>
        /// <param name="toHandle">The ProductType to add to the handles list</param>
        public void AddRepTypeHandle(SalesRepType repType, ProductType toHandle)
        {
            //Check that the given repType is not null
            if (repType != null)
            {
                //Check that the given ProductType is not null
                if (toHandle != null)
                {
                    // Check that the given reptype is actually in the Dictionary
                    if (repNums.Keys.Contains(repType))
                    {
                        // Add the ProductType to the Handles list of the matching Dictionary key
                        repNums.Keys.FirstOrDefault(srt => srt == repType).Handles.Add(toHandle);

                        // trigger the RepTypesChanged Event
                        OnRepTypesChanged();
                    }
                    else // The given SalesRepType is not in the Dictionary
                    {
                        throw new ArgumentOutOfRangeException("repType", "Attmepted to add a ProductType to a SalesRepType that was not in the Dictionary");
                    }
                }
                else // The given product type is null
                {
                    throw new ArgumentNullException("toHandle", "Attempted to pass null ProductType to Core.AddRepTypeHandle");
                }
            }
            else // The given sales rep type is null
            {
                throw new ArgumentNullException("repType", "Attempted to pass null SalesRepType to Core.AddRepTypeHandle");
            }
        }

        /// <summary>
        /// Removes the given ProductType from the SalesRepTypes Handles list
        /// </summary>
        /// <param name="repType">The SalesRepTypes that the ProductType is being removed from</param>
        /// <param name="toRemove">The ProductType to Remove</param>
        public void RemoveRepTypeHandle(SalesRepType repType, ProductType toRemove)
        {
             //Check that the given repType is not null
            if (repType != null)
            {
                //Check that the given ProductType is not null
                if (toRemove != null)
                {
                    // Check that the given SalesRepType is actually in the dictionary
                    if (repNums.Keys.Contains(repType))
                    {
                        //Remove the ProductType from the handles list of the matching dictionary key
                        SalesRepType st = repNums.Keys.FirstOrDefault(srt => srt == repType);

                        //If the handles list actually contains this ProductType
                        if (st.Handles.Contains(toRemove))
                        {
                            st.Handles.Remove(toRemove);
                            //Trigger the RepTypesChanged event
                            OnRepTypesChanged();
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException("toRemove", "Attempted to remove a ProductType from a SalesRepType Handles list that was not in that list");
                        }
                        
                    }
                    else // The given SalesRepType was not in the Dictionary
                    {
                        throw new ArgumentOutOfRangeException("repType", "Attmepted to remove a ProductType from a SalesRepType that was not in the Dictionary");
                    }
                }
                else // The given product type is null
                {
                    throw new ArgumentNullException("toRemove", "Attempted to pass null ProductType to Core.RemoveRepTypeHandle");
                }
            }
            else // The given sales rep type is null
            {
                throw new ArgumentNullException("repType", "Attempted to pass null SalesRepType to Core.RemoveRepTypeHandle");
            }
        }

        /// <summary>
        /// Sets the probability and and processingMultiplier of the ProductType with the given name 
        /// </summary>
        /// <param name="typeName">The typename of the ProductType to update</param>
        /// <param name="probability">The new value for the ProductType's probability</param>
        /// <param name="processingMultiplier">The new value for the ProductType's processingMultiplier</param>
        public void SetProductTypeSettings(string typeName, double probability, double processingMultiplier)
        {
            // Check that the given typeName is not null or empty
            if (typeName != null && typeName != string.Empty)
            {
                //Try and get the ProductType with the given typename
                ProductType pt = productTypes.FirstOrDefault(p => p.TypeName == typeName);

                //If a matching ProductType was found
                if (pt != null)
                {
                    //Set the ProductType's values to the new values
                    pt.ProductTypeProbability = probability;
                    pt.ProcessingDelayMultiplier = processingMultiplier;
                }
                else // No matching ProductType found
                {
                    throw new ArgumentOutOfRangeException("typeName", "Attempted to pass invalid typeName into Core.SetProductTypeSettings");
                }
            }
            else // The given string is null or empty
            {
                throw new ArgumentNullException("typeName", "Attempted to pass null or empty string to Core.SetProductTypeSettings");
            }
        }

        /// <summary>
        /// Called when the ProductTypesChanged event needs to be triggered 
        /// </summary>
        private void OnProductTypesChanged()
        {
            //If there are handlers listening for this event
            if (ProductTypeChanged != null)
            {
                ProductTypeChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Called when the RepTypesChanged event needs to be triggered
        /// </summary>
        private void OnRepTypesChanged()
        {
            //If there are handlers listening for this event
            if (RepTypesChanged != null)
            {
                RepTypesChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Called when the MainSettingsChanged event needs to be triggered
        /// </summary>
        private void OnMainSettingsChanged()
        {
            // If there are handlers listening for this event
            if (MainSettingsChanged != null)
            {
                MainSettingsChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// Executes a single Simulation run with the given parameters
        /// </summary>
        public void ExecuteSimulation()
        {
            //Create the simulator using the parameters
            Simulator sim = new Simulator(beginTime, duration, callArrivalMultiplier, switchDelayMultiplier, productTypes, maxQueueLength, singleQueueLength, excessiveWait, repNums);
            RunningDisplay rd = new RunningDisplay(0,0,sim);
            StatisticsManager statMan = new StatisticsManager(sim);
            StatisticsDisplay sd = new StatisticsDisplay(rd.Location.X, (rd.Location.Y + rd.Height), statMan);
            rd.Show();
            sd.Show();

            //Run the simulator
            sim.RunSimulation(SIMULATION_SLEEP);
            rd.Hide();
            rd.Dispose();
        }

        /// <summary>
        /// Checks if the current simulation parameters are valid
        /// </summary>
        /// <returns>true if the simulation parameters are valid, false if not</returns>
        public bool IsSimulationValid()
        {
            // Create valid as true by default
            bool valid = true;

            // If the number of ProductTypes is zero then it is not valid
            if (productTypes.Count == 0)
            {
                valid = false;
            }

            // if the number of SalesRepTypes is zero then it is not valid
            if (repNums.Count == 0)
            {
                valid = false;
            }
            else
            {
                //Get the count of the number of ProductTypes in the Handles lists of various SalesRepTypes
                int handleCount = repNums.Sum(p => p.Key.Handles.Count);
                //If that count is zero then it is not valid
                if (handleCount == 0)
                {
                    valid = false;
                }
            }

            // The simulation is not valid if the total probability is less than 100%
            if (TotalProbability() < 1)
            {
                valid = false;
            }

            return valid;
        }

        /// <summary>
        /// Calculates the sum of the probabilities of all ProductTypes
        /// </summary>
        /// <returns>A double between 0 and 1 (0% - 100%)</returns>
        public double TotalProbability()
        {
            //Create the double
            double percentage = 0;

            //Loop foreach ProductType in the Governors list
            foreach (ProductType pt in productTypes)
            {
                percentage += pt.ProductTypeProbability;
            }

            return percentage;
        }

        /// <summary>
        /// Loads the Core's settings from a given XML file
        /// </summary>
        /// <param name="filepath">The filepath to the XML file</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the given filepath is empty or null</exception>
        public void LoadSettingsFromFile(string filepath)
        {
            // Check that the given filepath is not empty or null
            if (filepath != null && filepath != string.Empty)
            {
                // Create the XDocument
                XDocument doc;

                // Attempt to create a stream for the document
                Stream docStream = File.OpenRead(filepath);

                // Load the document given the Stream
                doc = XDocument.Load(docStream);

                // Get the simulation settings element
                XElement settings = doc.Root.Element("SimulationSettings");

                // Load the settings from the document
                callArrivalMultiplier = 1;// Convert.ToDouble(settings.Element("CallArrivalMultiplier").Value.ToString());
                 switchDelayMultiplier = 1;// Convert.ToDouble(settings.Element("SwitchDelayMultiplier").Value);
                 maxQueueLength = 14; //Convert.ToInt32(settings.Element("MaxQueueLength").Value);
                singleQueueLength = true;//Convert.ToBoolean(settings.Element("SingleQueueLength").Value);

                // Load the seperate parts of the beginTime
                XElement begin = settings.Element("BeginTime");
                int hour = Convert.ToInt32(begin.Element("Hours").Value);
                int minute = Convert.ToInt32(begin.Element("Minutes").Value);
                int seconds = Convert.ToInt32(begin.Element("Seconds").Value);
                // Create the datatime using the individual pieces
                beginTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, seconds);

                // Load the seperate parts of the Duration
                XElement dur = settings.Element("Duration");
                int days = Convert.ToInt32(dur.Element("Days").Value);
                int hours = Convert.ToInt32(dur.Element("Hours").Value);
                int minutes = Convert.ToInt32(dur.Element("Minutes").Value);
                int durSeconds = Convert.ToInt32(dur.Element("Seconds").Value);
                // Create the TimeSpan with the individual pieces
                duration = new TimeSpan(days, hours, minutes, seconds);

                // Load the seperate parts of the excessive wait
                XElement exWait = settings.Element("ExcessiveWait");
                int ewHours = Convert.ToInt32(exWait.Element("Hours").Value);
                int ewMinutes = Convert.ToInt32(exWait.Element("Minutes").Value);
                int ewSeconds = Convert.ToInt32(exWait.Element("Seconds").Value);
                // Create the TimeSpan with the individual pieces
                excessiveWait = new TimeSpan(ewHours, ewMinutes, ewSeconds);

                // Get the ProductType list element
                XElement pType = doc.Root.Element("ProductTypesList");
                // Loop foreach ProductType element in the product types list
                foreach (XElement productType in pType.Elements("ProductType").OrderBy(pt => pt.Attribute("id").Value))
                {
                    // Get the id of the value
                    int id = Convert.ToInt32(productType.Attribute("id").Value);
                    // load the values
                    string typeName = productType.Element("TypeName").Value;
                    double probability = 0.5; //Convert.ToDouble(productType.Element("ProductTypeProbability").Value);
                    double multiplier = 2; //Convert.ToDouble(productType.Element("ProcessingDelayMultiplier").Value);

                    // Create the product type
                    ProductType pt = new ProductType(typeName, multiplier, probability);

                    // If the id is greater than the count of items
                    if (id > productTypes.Count)
                    {
                        // Add it to the end of the list
                        productTypes.Add(pt);
                    }
                    else // The id is not greater than the count
                    {
                        // Insert the ProductType into the productTypes list using the id as the index
                        productTypes.Insert(id, pt);
                    }
                }

                // Get the SalesRepTypesList element
                XElement repTypeList = doc.Root.Element("SalesRepTypesList");
                // Loop foreach SalesRepType element in the list
                foreach (XElement salesRepType in repTypeList.Elements("SalesRepType"))
                {
                    // Load the typename
                    string typeName = salesRepType.Element("RepTypeName").Value;
                    // Load the number of reps of this type
                    int numReps = Convert.ToInt32(salesRepType.Element("NumberOfReps").Value);

                    // Create the SalesRepType
                    SalesRepType srt = new SalesRepType(typeName);
                    // Loop foreach handleableProductType element in the sales rep types HandlesList element
                    foreach (XElement productType in salesRepType.Element("HandlesList").Elements("HandleableProductType"))
                    {
                        int id = Convert.ToInt32(productType.Attribute("productTypeID").Value);

                        // Use the id to retrieve the correct product type from the product types list
                        srt.Handles.Add(productTypes[id]);
                    }

                    //Add the sales RepType to the dictionary
                    repNums.Add(srt, numReps);
                }

                //Trigger all the events
                OnRepTypesChanged();
                OnProductTypesChanged();
                OnMainSettingsChanged();
            }
            else // The given filepath is null or empty
            {
                throw new ArgumentNullException("filepath", "Attempted to pass null or empty string to Core.LoadSettingsFromFile");
            }
        }

        /// <summary>
        /// Saves the current simulation settings to the specified file
        /// </summary>
        /// <param name="filepath">The path to save the file to</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the given filepath is null or empty</exception>
        public void SaveSettingsToFile(string filepath)
        {
            // Check that the given filepath is not null or empty
            if (filepath != null && filepath != string.Empty)
            {
                // Load the template
                XDocument doc = XDocument.Load(DESConstants.APP_DIR + DESConstants.SAVE_FILE_TEMPLATE);

                // Get the main settings element
                XElement mainSettings = doc.Root.Element("SimulationSettings");
                // Save the main settings to the file
                mainSettings.Element("CallArrivalMultiplier").SetValue(callArrivalMultiplier);
                mainSettings.Element("SwitchDelayMultiplier").SetValue(switchDelayMultiplier);
                mainSettings.Element("MaxQueueLength").SetValue(maxQueueLength);
                mainSettings.Element("SingleQueueLength").SetValue(singleQueueLength);

                // Get the begin time element
                XElement begTime = mainSettings.Element("BeginTime");
                // Set its values
                begTime.Element("Hours").SetValue(beginTime.Hour);
                begTime.Element("Minutes").SetValue(beginTime.Minute);
                begTime.Element("Seconds").SetValue(beginTime.Second);

                // Get the duration element
                XElement dur = mainSettings.Element("Duration");
                // Set its values
                dur.Element("Days").SetValue(duration.Days);
                dur.Element("Hours").SetValue(duration.Hours);
                dur.Element("Minutes").SetValue(duration.Minutes);
                dur.Element("Seconds").SetValue(duration.Seconds);

                // Get the excessive wait element
                XElement exWait = mainSettings.Element("ExcessiveWait");
                // Set its values
                exWait.Element("Hours").SetValue(excessiveWait.Hours);
                exWait.Element("Minutes").SetValue(excessiveWait.Minutes);
                exWait.Element("Seconds").SetValue(excessiveWait.Seconds);

                // Get the ProductTypesList element
                XElement pTypesList = doc.Root.Element("ProductTypesList");
                // Loop foreach product type in the list and add it to the document
                for (int i = 0; i < productTypes.Count; i++)
                {
                    // Create the element to add to the list
                    XElement pt = new XElement("ProductType");
                    // Create the id attribute
                    XAttribute id = new XAttribute("id", i);
                    // Add the attribute to the element
                    pt.Add(id);
                    // Create the inner elements
                    XElement typeName = new XElement("TypeName", productTypes[i].TypeName);
                    XElement probability = new XElement("ProductTypeProbability", productTypes[i].ProductTypeProbability);
                    XElement multiplier = new XElement("ProcessingDelayMultiplier", productTypes[i].ProcessingDelayMultiplier);
                    // Add them to the element
                    pt.Add(typeName);
                    pt.Add(probability);
                    pt.Add(multiplier);

                    //Add the element to the pTypesList
                    pTypesList.Add(pt);
                }

                // Get the SalesRepTypesList
                XElement salesRepTypeList = doc.Root.Element("SalesRepTypesList");
                // Loop foreach SalesRepType in the repNums dictionary
                foreach (KeyValuePair<SalesRepType, int> kvp in repNums)
                {
                    // Create the repType element
                    XElement repType = new XElement("SalesRepType");
                    // Create the two data child elements
                    XElement repTypeName = new XElement("RepTypeName", kvp.Key.TypeName);
                    XElement numberOfReps = new XElement("NumberOfReps", kvp.Value);
                    // Create the handles list element
                    XElement handlesList = new XElement("HandlesList");
                    // loop foreach ProductType in the SalesRepTypes Handles list
                    foreach (ProductType pt in kvp.Key.Handles)
                    {
                        // Create the pt element
                        XElement handleableProductType = new XElement("HandleableProductType");
                        // Get the index of the ProductType in the productTypes list (this is its id)
                        int ptid = productTypes.IndexOf(pt);
                        // Create and add the attribute to the handleableProductType
                        XAttribute id = new XAttribute("productTypeID", ptid);
                        handleableProductType.Add(id);

                        // Add the handleableProductType to the handles list
                        handlesList.Add(handleableProductType);
                    }

                    // Add the elements to the SalesRepType element
                    repType.Add(repTypeName);
                    repType.Add(numberOfReps);
                    repType.Add(handlesList);

                    //Add the reptype to the SalesRepTypesList element
                    salesRepTypeList.Add(repType);
                }

                //Save the document to the specified location
                doc.Save(filepath);
            }
            else // The given string is empty or null
            {
                throw new ArgumentNullException("filepath", "Attempted to pass null or empty string to Core.SaveSettingsToFile");
            }
        }
    }
}
