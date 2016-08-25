using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DiscreteEventSimulator
{
    /// <summary>
    /// Defines the Simulator, this class manages the simulation
    /// </summary>
    public class Simulator
    {
        //-------------------------------------------------------------------
        //- FIELDS                                                          -
        //-------------------------------------------------------------------
        private Calendar calendar;
        private DateTime beginTime;
        private DateTime clock;
        private EventFactory eventFactory;
        private CallFactory callFactory;
        private ProcessArgsFactory processArgsFactory;
        private double callArriveMultiplier;
        private double switchDelayMultiplier;
        private List<ProductType> productTypes;
        private QueueManager queueManager;
        private SalesForceManager salesManager;
        private TimeSpan excessiveWaitTime;


        //-------------------------------------------------------------------
        //- PROPERTIES                                                      -
        //-------------------------------------------------------------------
        /// <summary>
        /// Gets the Calendar of the simulation
        /// </summary>
        public Calendar Calendar
        {
            get { return calendar; }
        }

        /// <summary>
        /// Gets the time that the simulation began
        /// </summary>
        public DateTime BeginTime
        {
            get { return beginTime; }
        }

        /// <summary>
        /// Gets the current value of the simulator clock
        /// </summary>
        public DateTime Clock
        {
            get { return clock; }
        }

        /// <summary>
        /// Gets the simulations EventFactory
        /// </summary>
        public EventFactory EventFactory
        {
            get { return eventFactory; }
        }

        /// <summary>
        /// Gets the simulations Factory for making Calls
        /// </summary>
        public CallFactory CallFactory
        {
            get { return callFactory; }
        }

        /// <summary>
        /// Gets the simulations factory for creating EventProcessArgs instances
        /// </summary>
        public ProcessArgsFactory ProcessArgsFactory
        {
            get { return processArgsFactory; }
        }

        /// <summary>
        /// Gets the Call Arrival Multiplier of the simulation
        /// </summary>
        public double CallArriveMultiplier
        {
            get { return callArriveMultiplier; }
        }

        /// <summary>
        /// Gets the Switch Delay Multiplier of the simulation
        /// </summary>
        public double SwitchDelayMultiplier
        {
            get { return switchDelayMultiplier; }
        }

        /// <summary>
        /// Gets the list of all ProductTypes in the simulation
        /// </summary>
        public List<ProductType> ProductTypes
        {
            get { return productTypes; }
        }

        /// <summary>
        /// Gets the simulations Queue Manager
        /// </summary>
        public QueueManager QueueManager
        {
            get { return queueManager; }
        }

        /// <summary>
        /// Gets the simulations SalesManager
        /// </summary>

        public SalesForceManager SalesManager
        {
            get { return salesManager; }
        }

        /// <summary>
        /// Gets the time that a call can spend waiting before it is considered excessive
        /// </summary>
        public TimeSpan ExcessiveWaitTime
        {
            get { return excessiveWaitTime; }
        }

        //-------------------------------------------------------------------
        //- EVENTS                                                          -
        //-------------------------------------------------------------------
        /// <summary>
        /// Triggered when an a Simulation Event is processed
        /// </summary>
        public event SimulationEventHandler EventOccured;
        public delegate void SimulationEventHandler(object sender, SimulationEventArgs e);

        //-------------------------------------------------------------------
        //- METHODS                                                         -
        //-------------------------------------------------------------------
        /// <summary>
        /// Creates a new instance of the Simulator class with the given parameters
        /// </summary>
        /// <param name="beginTime">The time that simulation is set to begin</param>
        /// <param name="runningTime">The duration that the simulation will run for</param>
        /// <param name="callArriveMultiplier">The multiplier used to calculate the time between calls</param>
        /// <param name="switchDelayMultiplier">The multiplier used to calculate the time calls spend at the switch</param>
        /// <param name="productTypes">The List of all product types</param>
        /// <param name="maxQueueLength">The maximum number of calls in a queue</param>
        /// <param name="singleQueueLength">Whether or not the QueueManager is set to count all queues as one or not</param>
        /// <param name="excessiveWaitTime">The TimeSpan that beyond which a call is considered having waited to long</param>
        /// <param name="representativeNumbers">The Dictionary containing the Types and numbers of SalesRepresentatives</param>
        public Simulator(
            DateTime beginTime, 
            TimeSpan runningTime, 
            double callArriveMultiplier, 
            double switchDelayMultiplier, 
            List<ProductType> productTypes, 
            int maxQueueLength, 
            bool singleQueueLength,
            TimeSpan excessiveWaitTime,
            Dictionary<SalesRepType, int> representativeNumbers)
        {
            this.beginTime = beginTime;
            this.callArriveMultiplier = callArriveMultiplier;
            this.switchDelayMultiplier = switchDelayMultiplier;
            this.productTypes = productTypes;
            this.calendar = new Calendar();
            this.callFactory = new CallFactory();
            this.eventFactory = new EventFactory();
            this.clock = beginTime;
            this.excessiveWaitTime = excessiveWaitTime;
            this.queueManager = new QueueManager(maxQueueLength, productTypes, singleQueueLength);
            this.salesManager = new SalesForceManager(representativeNumbers);
            this.processArgsFactory = new ProcessArgsFactory(this);

            //Create the end simulation event
            DateTime finishingTime = beginTime + runningTime;
            Event endReplication = eventFactory.CreateEvent(EEventType.EndReplication, finishingTime, null);
            calendar.AddEvent(endReplication);

            //Create the first call arrive event
            Call call = callFactory.CreateCall();
            Event firstCall = eventFactory.CreateEvent(EEventType.CallArrive, beginTime, call);
            calendar.AddEvent(firstCall);
        }

        /// <summary>
        /// Executes the simulation
        /// </summary>
        /// <param name="delay">The time to pause between each event process</param>
        public void RunSimulation(uint delay)
        {
            // Create the active event variable
            Event activeEvent = null;

            //do while the active event is not the end simulation event
            do
            {
                //Retrieve the next event
                activeEvent = calendar.NextEvent();

                clock = activeEvent.EventTime;
                EventProcessArgs epa = processArgsFactory.CreateProcessArgsFor(activeEvent);
                List<Event> spawnedEvents = activeEvent.Process(epa);

                //Loop through all the spawned events and add them to the calendar
                foreach (Event e in spawnedEvents)
                {
                    calendar.AddEvent(e);
                }

                OnEventOccured(activeEvent);

                Thread.Sleep((int)delay);
            } while (!(activeEvent is EndReplicationEvent));
        }

        public void DrawCalendarToGataGridView(DataGridView dgv)
        {
            calendar.ToDataGridView(dgv);
        }

        /// <summary>
        /// Called when the EventOccured event should be fired
        /// </summary>
        /// <param name="processedEvent">The event that was processed</param>
        public void OnEventOccured(Event processedEvent)
        {
            SimulationEventArgs e = new SimulationEventArgs { ProcessedEvent = processedEvent };

            //Verify that there are actually handlers registered to the event
            if (EventOccured != null)
            {
                EventOccured(this, e);
            }
        }

        //-------------------------------------------------------------------
        //- SPECIAL                                                         -
        //-------------------------------------------------------------------
        public class SimulationEventArgs : EventArgs
        {
            public Event ProcessedEvent { get; set; }
        }
    }
}
