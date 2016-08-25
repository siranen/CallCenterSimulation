using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscreteEventSimulator
{
    /// <summary>
    /// Defines the EventProcessArgs child class that provides access to data for the CompletedServiceEvent.Process method
    /// </summary>
    public class CompletedServiceProcessArgs : EventProcessArgs
    {
        //-------------------------------------------------------------------
        //- METHODS                                                         -
        //-------------------------------------------------------------------
        /// <summary>
        /// Creates a new instance of the CompletedServiceProcessArgs
        /// </summary>
        /// <param name="eventFactory">The instance of the EventFactory class</param>
        /// <param name="queueManager">The instance of the QueueManager class</param>
        /// <exception cref="System.ArgumentNullException">Thrown when either of the given arguments are null</exception>
        public CompletedServiceProcessArgs(EventFactory eventFactory, QueueManager queueManager)
            :base(eventFactory, queueManager)
        {
            //Throw excpetions if either of the parameters are null
            if (eventFactory == null)
            {
                throw new ArgumentNullException("eventFactory", "Attempted to pass null EventFactory to CompletedServiceProcessArgs constructor");
            }
            else
            {
                if (queueManager == null)
                {
                    throw new ArgumentNullException("queueManager", "Attempted to pass null QueueManager to CompletedServiceProcessArgs constructor");
                }
            }
        }
    }
}
