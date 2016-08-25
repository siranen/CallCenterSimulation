using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscreteEventSimulator
{
    /// <summary>
    /// Defines the class for holding data to be based to Event.Process methods
    /// </summary>
    public abstract class EventProcessArgs
    {
        //-------------------------------------------------------------------
        //- FIELDS                                                          -
        //-------------------------------------------------------------------
        private EventFactory eventFactory;
        private QueueManager queueManager;

        //-------------------------------------------------------------------
        //- PROPERTIES                                                      -
        //-------------------------------------------------------------------
        /// <summary>
        /// Gets the QueueManager field for this EventProcessArgs
        /// </summary>
        public QueueManager QueueManager
        {
            get { return queueManager; }
        }

        /// <summary>
        /// Gets the EventFactory for this EventProcessArgs
        /// </summary>
        public EventFactory EventFactory
        {
            get { return eventFactory; }
        }

        //-------------------------------------------------------------------
        //- METHODS                                                         -
        //-------------------------------------------------------------------
        /// <summary>
        /// Creates a new instance of the EventProcessArgs class
        /// </summary>
        /// <param name="eventFactory">The instance of the EventFactory class</param>
        /// <param name="queueManager">The instance of the QueueManager class</param>
        public EventProcessArgs(EventFactory eventFactory,  QueueManager queueManager)
        {
            this.eventFactory = eventFactory;
            this.queueManager = queueManager;
        }
    }
}
