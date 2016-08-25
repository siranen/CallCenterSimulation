using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DiscreteEventSimulator
{
    /// <summary>
    /// Collects statistics as the simulation runs
    /// </summary>
    public class StatisticsManager
    {
        //-------------------------------------------------------------------
        //- FIELDS                                                          -
        //-------------------------------------------------------------------
        private Simulator sim;
        private int busySignalCount;
        private Dictionary<ProductType, int> completedProductCount;
        private Dictionary<ProductType, int> excessiveWaitCounts;
        private Dictionary<SalesRepType, TimeSpan> totalTimeProcessing;
        private Dictionary<SalesRepType, double> representativeUtilisation;
        private Dictionary<ProductType, double> averageNumberWaiting;
        private TimeSpan averageWaitAllTypes;
        private TimeSpan averageSystemTime;
        private TimeSpan timeElapsed;

        //-------------------------------------------------------------------
        //- PROPERTIES                                                      -
        //-------------------------------------------------------------------
        /// <summary>
        /// Gets the number of calls that have been rejected
        /// </summary>
        public int BusySignalCount
        {
            get { return busySignalCount; }
        }

        /// <summary>
        /// Gets a copy of the Dictionary containing the number of completed calls by ProductType
        /// </summary>
        public Dictionary<ProductType, int> CompletedProductCount
        {
            get { return new Dictionary<ProductType, int>(completedProductCount); }
        }

        /// <summary>
        /// Gets a copy of the Dictionary containing the number of calls that faced excessive waits
        /// </summary>
        public Dictionary<ProductType, int> ExcessiveWaitCounts
        {
            get { return new Dictionary<ProductType, int>(excessiveWaitCounts); }
        }

        /// <summary>
        /// Gets a copy of the Dictionary containing the percentage of utilisation of representatives of different types
        /// </summary>
        public Dictionary<SalesRepType, double> RepresentativeUtilisation
        {
            get { return new Dictionary<SalesRepType, double>(representativeUtilisation); }
        }

        /// <summary>
        /// Gets a copy of the Dictionary containing the average number of calls waiting by ProductType
        /// </summary>
        public Dictionary<ProductType, double> AverageNumberWaiting
        {
            get { return new Dictionary<ProductType, double>(averageNumberWaiting); }
        }

        /// <summary>
        /// Gets the average number of calls waiting of all types
        /// </summary>
        public TimeSpan AverageWaitAllTypes
        {
            get { return averageWaitAllTypes; }
        }

        /// <summary>
        /// Gets the average amount of time spent in the system by all call types
        /// </summary>
        public TimeSpan AverageSystemTime
        {
            get { return averageSystemTime; }
        }

        /// <summary>
        /// Gets the total amount of time that the simulation has been running for
        /// </summary>
        public TimeSpan TimeElapsed
        {
            get { return timeElapsed; }
        }

        //-------------------------------------------------------------------
        //- Events                                                          -
        //-------------------------------------------------------------------
        public event EventHandler StatisticsUpdated;

        //-------------------------------------------------------------------
        //- METHODS                                                         -
        //-------------------------------------------------------------------
        /// <summary>
        /// Creates a new instance of the statistics manager that is monitoring the given Simulator
        /// </summary>
        /// <param name="args">The simulator this StatisticsManager is bound to</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the given Simulator is null</exception>
        public StatisticsManager(Simulator sim)
        {
            //Check that args is not null
            if (sim != null)
            {
                this.sim = sim;
                busySignalCount = 0;
                completedProductCount = new Dictionary<ProductType, int>();
                excessiveWaitCounts = new Dictionary<ProductType, int>();
                averageNumberWaiting = new Dictionary<ProductType, double>();
                //Loop for every product type in the simulation and create the appropriate entry in the dictionaries
                foreach (ProductType pt in sim.ProductTypes)
                {
                    completedProductCount.Add(pt, 0);
                    excessiveWaitCounts.Add(pt, 0);
                    averageNumberWaiting.Add(pt, 0);
                }

                representativeUtilisation = new Dictionary<SalesRepType, double>();
                totalTimeProcessing = new Dictionary<SalesRepType, TimeSpan>();

                foreach (SalesRepType srt in sim.SalesManager.RepTypes)
                {
                    representativeUtilisation.Add(srt, 0);
                    totalTimeProcessing.Add(srt, new TimeSpan());
                }

                averageSystemTime = new TimeSpan();
                averageWaitAllTypes = new TimeSpan();
                timeElapsed = new TimeSpan();

                //Bind the event handler for the simulators eventoccured event
                sim.EventOccured += new Simulator.SimulationEventHandler(UpdateStatistics);
            }
            else // args was null throw exception
            {
                throw new ArgumentNullException("sim", "Attempted to pass null Simulator to StatisticsManager constructor");
            }
        }

        /// <summary>
        /// Updates all the statistics that are monitored
        /// </summary>
        /// <param name="sender">The object that triggered this event, usually args</param>
        /// <param name="e">The EventArgs</param>
        void UpdateStatistics(object sender, Simulator.SimulationEventArgs e)
        {
            //Get the processed event
            Event processed = e.ProcessedEvent;

            //If the event was a CallArriveEvent
            if (processed is CallArriveEvent)
            {
                CallArrive((CallArriveEvent)processed);
            }
            else //It was a different event
            {
                //If the event was a completed service event
                if (processed is CompletedServiceEvent)
                {
                    ServiceComplete((CompletedServiceEvent)processed);
                }
                else //It was either EndReplication or something else
                {
                    //If the event was an End Replication event
                    if (processed is EndReplicationEvent)
                    {
                        //Write the results of this run to a new file
                        OutputToFile();
                    }
                }
            }

            //Update all the time based statistics
            UpdateTimeBasedStatistics(processed);

            //Trigger the StatisticsUpdated event
            OnStatisticsUpdated();            
        }

        /// <summary>
        /// Updates statistics that are effected by the passage of time
        /// </summary>
        /// <param name="e">The event that was processed</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the given Event is null</exception>
        private void UpdateTimeBasedStatistics(Event e)
        {
            //Check that the given event is not null
            if (e != null)
            {
                if (e.Entity != null)
                {
                    //Store the old time elapsed
                    TimeSpan oldTimeElapsed = new TimeSpan(timeElapsed.Ticks);
                    //Calculat the new time elapsed
                    timeElapsed = e.EventTime - sim.BeginTime;
                    //Calculate the difference between the old and new
                    TimeSpan difference = timeElapsed - oldTimeElapsed;

                    //Recalculate the averages using ((oldAvg*oldTimeElapsed)+(value*difference))/totalTimeElapsed

                    //Recalculate the averages of every product type
                    for (int i = 0; i < averageNumberWaiting.Count; i++)
                    {
                        KeyValuePair<ProductType, double> kvp = averageNumberWaiting.ElementAt(i);

                        int numberWaiting = sim.QueueManager.GetQueueLength(kvp.Key);

                        //Calculation from Ingalls and White (2009)
                        double protectedVal = (kvp.Value * oldTimeElapsed.TotalSeconds);
                        //Protect against NaN's
                        protectedVal = double.IsNaN(protectedVal) == false ? protectedVal : 0;
                        averageNumberWaiting[kvp.Key] = (protectedVal + (numberWaiting * difference.TotalSeconds)) / timeElapsed.TotalSeconds;
                    }

                    //Recalculate the average of wait of all call types
                    double old = averageWaitAllTypes.TotalSeconds;
                    //Calculate the time spent waiting if the entity has waited and began processing else return an emptyTimespan
                    TimeSpan spentWaiting = (e.Entity.BeginWait != DateTime.MinValue && e.Entity.BeganProcessing != DateTime.MinValue) ? e.Entity.BeganProcessing - e.Entity.BeginWait : new TimeSpan();
                    double newAvg = ((old * oldTimeElapsed.TotalSeconds) + (spentWaiting.TotalSeconds * difference.TotalSeconds)) / timeElapsed.TotalSeconds;
                    //protect against NaN's
                    averageWaitAllTypes = double.IsNaN(newAvg) == false ? TimeSpan.FromSeconds(newAvg) : new TimeSpan();

                    //Recalculate the representative utilization
                    foreach (KeyValuePair<SalesRepType, TimeSpan> kvp in totalTimeProcessing)
                    {
                        //Representative utilization is totalTimeProcessing/timeElapsed
                        representativeUtilisation[kvp.Key] = (kvp.Value.TotalSeconds / sim.SalesManager.RepresentativesOfType(kvp.Key)) / timeElapsed.TotalSeconds;
                    }

                    //Recalculate the average system time
                    if (e.Entity.FinishTime != DateTime.MinValue)
                    {
                        TimeSpan totalTimeInSystem = e.Entity.FinishTime - e.Entity.StartTime;
                        double newAvgTime = ((averageSystemTime.TotalSeconds * oldTimeElapsed.TotalSeconds) + (totalTimeInSystem.TotalSeconds * difference.TotalSeconds)) / timeElapsed.TotalSeconds;
                        averageSystemTime = TimeSpan.FromSeconds(newAvgTime);
                    }
                }

            }
            else //The given event is null throw exception
            {
                throw new ArgumentNullException("e", "Attempted to pass null Event to StatisticsManager.UpdateTimeBasedStatistics");
            }
        }

        /// <summary>
        /// Outputs the results of the simulation to a file
        /// </summary>
        private void OutputToFile()
        {
            //Create the results directory if it does not exist
            if (!Directory.Exists("results"))
            {
                Directory.CreateDirectory("results");
            }

            DateTime now = DateTime.Now;
            //Create the output writer
            StreamWriter output = new StreamWriter(@"results/" + now.ToString("dd-MM-yy HH-mm-dd") + ".csv");
            //Output the information about the simulation
            output.WriteLine("Execution Details:");
            output.WriteLine(",Date:," + now.ToShortDateString());
            output.WriteLine(",Time:," + now.ToShortTimeString());
            output.WriteLine(",Call Arrival Multiplier:," + sim.CallArriveMultiplier);
            output.WriteLine(",Switch Delay Multiplier:," + sim.SwitchDelayMultiplier);
            output.WriteLine(",Excessive Wait Threshold:," + sim.ExcessiveWaitTime);
            output.WriteLine(",Duration:," + (sim.Clock - sim.BeginTime).ToString(@"dd\.hh\:mm\:ss"));

            //Output details about the simulations product types
            output.WriteLine(",Product Types:");
            output.Write(",,Type Name:,");
            foreach (ProductType pt in sim.ProductTypes)
            {
                output.Write(pt.TypeName + ",");
            }
            output.WriteLine();
            output.Write(",,Processing Delay Multiplier:,");
            foreach (ProductType pt in sim.ProductTypes)
            {
                output.Write(pt.ProcessingDelayMultiplier + ",");
            }
            output.WriteLine();
            output.Write(",,Type Probability:,");
            foreach (ProductType pt in sim.ProductTypes)
            {
                output.Write(pt.ProductTypeProbability.ToString("p2") + ",");
            }
            output.WriteLine();

            //Output details about the simulations representatives
            output.WriteLine(",Representative Types:");
            output.Write(",,Type Name:,");
            foreach (SalesRepType srt in sim.SalesManager.RepTypes)
            {
                output.Write(srt.TypeName + ",");
            }
            output.WriteLine();
            output.Write(",,Number of Reps:,");
            foreach (SalesRepType srt in sim.SalesManager.RepTypes)
            {
                output.Write(sim.SalesManager.RepresentativesOfType(srt) + ",");
            }
            output.WriteLine();
            output.Write(",,Handles:,");
            foreach (SalesRepType srt in sim.SalesManager.RepTypes)
            {
                output.Write("\"");
                foreach (ProductType pt in srt.Handles)
                {
                    output.Write(pt.TypeName + ", ");
                }
                output.Write("\",");
            }
            output.WriteLine();
            output.WriteLine();
            
            //Output details about the Statistics that were gathered
            output.WriteLine("Statistics:");
            output.WriteLine(",Busy Signal Count:," + busySignalCount);
            output.WriteLine(",Average Waiting Time:," + averageWaitAllTypes.ToString(@"hh\:mm\:ss"));
            output.WriteLine(",Average System Time:," + averageSystemTime.ToString(@"hh\:mm\:ss"));
            output.WriteLine(",Product Stats:");
            output.Write(",,Type Name:,");
            foreach (ProductType pt in sim.ProductTypes)
	        {
		        output.Write(pt.TypeName + ",");
	        }
            output.WriteLine();
            output.Write(",,Completed Calls:,");
            foreach (ProductType pt in sim.ProductTypes)
	        {
		        output.Write(completedProductCount[pt] + ",");
	        }
            output.WriteLine();
            output.Write(",,Excessive Wait Counts:,");
            foreach (ProductType pt in sim.ProductTypes)
	        {
		        output.Write(excessiveWaitCounts[pt] + ",");
	        }
            output.WriteLine();
            output.Write(",,Average Number Waiting:,");
            foreach (ProductType pt in sim.ProductTypes)
	        {
		        output.Write(averageNumberWaiting[pt] + ",");
	        }
            output.WriteLine();
            output.WriteLine(",Representative Stats:");
            output.Write(",,Type Name:,");
            foreach (SalesRepType srt in sim.SalesManager.RepTypes)
            {
                output.Write(srt.TypeName + ",");
            }
            output.WriteLine();
            output.Write(",,Utilization,");
            foreach (SalesRepType srt in sim.SalesManager.RepTypes)
            {
                output.Write(representativeUtilisation[srt].ToString("p2")+ ",");
            }
            output.WriteLine();

            output.Close();
        }

        /// <summary>
        /// Updates the statistics that are affected by a Completed Service event
        /// </summary>
        /// <param name="completedServiceEvent">The event that occured</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the given event is null</exception>
        private void ServiceComplete(CompletedServiceEvent completedServiceEvent)
        {
            //Check that the given event is not null
            if (completedServiceEvent != null)
            {
                Call c = completedServiceEvent.Entity;

                //If the call spent time waiting
                if (c.BeginWait != DateTime.MinValue)
                {
                    //Calculate the time the call spent waiting
                    TimeSpan waiting = c.BeganProcessing - c.BeginWait;
                    
                    //If that time is greater than the sims threshold value
                    if (waiting >= sim.ExcessiveWaitTime)
                    {
                        //Increase the excessive wait counter for the calls product type
                        excessiveWaitCounts[c.ProductType]++;
                    }
                }

                //Increase the number of completed calls of the given product type
                completedProductCount[c.ProductType]++;

                //Calculate the time this call spent processing
                TimeSpan processingTime = completedServiceEvent.EventTime - c.BeganProcessing;

                //Add this processingTime to the totalTimeProcessing for the SalesReps type
                totalTimeProcessing[c.ProcessedBy.RepType] += processingTime;
            }
            else // given event was null throw exception
            {
                throw new ArgumentNullException("completedServiceEvent", "Attempted to pass null CompletedServiceEvent to StatisticsManager.ServiceComplete");
            }
        }


        /// <summary>
        /// Updates the statisics affected by a CallArriveEvent
        /// </summary>
        /// <param name="callArriveEvent">The Event that occured</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the given event is null</exception>
        private void CallArrive(CallArriveEvent callArriveEvent)
        {
            //Check that the given event is not null
            if (callArriveEvent != null)
            {
                //If the event ended in a hangup
                if (callArriveEvent.HangUp == true)
                {
                    //Increase the number of busy signals
                    busySignalCount++;
                }
            }
            else // given event was null throw exception
            {
                throw new ArgumentNullException("callArriveEvent", "Attempted to pass null CallArriveEvent to StatisticsManagager.CallArrive");
            }
        }

        /// <summary>
        /// Called when the StatisticsUpdated event needs to be triggered
        /// </summary>
        private void OnStatisticsUpdated()
        {
            //Create the empty event args
            EventArgs e = new EventArgs();
            
            //Check that there are any handlers registered to the StatisticsUpdated event
            if (StatisticsUpdated != null)
            {
                StatisticsUpdated(this, e);
            }
        }

        /// <summary>
        /// Returns the statistics as an object array of object arrays
        /// </summary>
        /// <returns>An array of arrays containing the statistics</returns>
        public object[][] ToArray()
        {
            //Create a list of object arrays to add arrays to
            List<object[]> returnList = new List<object[]>();

            //Add the first piece of data
            object[] rowData = { "Busy Signal Count", busySignalCount, "" };
            returnList.Add(rowData);

            //Add the rows for the completed product counts
            foreach (KeyValuePair<ProductType, int> kvp in completedProductCount)
            {
                rowData = new object[] { ("Completion <" + kvp.Key.TypeName + "> Count"), kvp.Value, "" };
                returnList.Add(rowData);
            }

            //Add the rows for the excessive wait counts
            foreach (KeyValuePair<ProductType, int> kvp in excessiveWaitCounts)
            {
                rowData = new object[] { ("Excessive Wait <" + kvp.Key.TypeName + "> Count"), kvp.Value, "" };
                returnList.Add(rowData);
            }

            //Add the rows for the representative utilization
            foreach (KeyValuePair<SalesRepType, double> kvp in representativeUtilisation)
            {
                rowData = new object[] { ("Representative Utilisation <" + kvp.Key.TypeName + ">"), kvp.Value.ToString("p2"),timeElapsed.ToString(@"hh\:mm\:ss") };
                returnList.Add(rowData);
            }

            //Add the rows for the average number waiting
            foreach (KeyValuePair<ProductType, double> kvp in averageNumberWaiting)
            {
                rowData = new object[] { ("Average Number Waiting <" + kvp.Key.TypeName + ">"), kvp.Value.ToString("F5"), timeElapsed.ToString(@"hh\:mm\:ss") };
                returnList.Add(rowData);
            }

            //Add the row for the average waiting time for all types
            rowData = new object[] { "Average Wait Time <All Types>", averageWaitAllTypes.ToString(@"hh\:mm\:ss"), timeElapsed.ToString(@"hh\:mm\:ss") };
            returnList.Add(rowData);

            //Add the row for the average time spent in the system
            rowData = new object[] { "Average System Time", averageSystemTime.ToString(@"hh\:mm\:ss"), timeElapsed.ToString(@"hh\:mm\:ss") };
            returnList.Add(rowData);

            //Collapse the List into an array of arrays
            return returnList.ToArray();
        }
    }
}
