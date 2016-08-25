using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscreteEventSimulator
{
    /// <summary>
    /// Defines the EventProcessArgs child class that provides access to data for the SwitchCompletedEvent.Process method
    /// </summary>
    public class SwitchCompleteProcessArgs : EventProcessArgs
    {
        //-------------------------------------------------------------------
        //- FIELDS                                                          -
        //-------------------------------------------------------------------
        private SalesForceManager salesManager;

        //-------------------------------------------------------------------
        //- PROPERTIES                                                      -
        //-------------------------------------------------------------------
        /// <summary>
        /// Gets the SalesManager instance
        /// </summary>
        public SalesForceManager SalesManager
        {
            get { return salesManager; }
        }

        //-------------------------------------------------------------------
        //- METHODS                                                         -
        //-------------------------------------------------------------------
        /// <summary>
        /// Creates a new instance of the SwitchCompleteProcessArgs class
        /// </summary>
        /// <param name="eventFactory">The instance of the EventFactory class</param>
        /// <param name="queueManager">The instance of the QueueManager class</param>
        /// <param name="salesManager">The instance of the SalesManager class</param>
        /// <exception cref="System.ArgumentNullException">Thrown when any of the given arguments are null</exception>
        public SwitchCompleteProcessArgs(EventFactory eventFactory, QueueManager queueManager, SalesForceManager salesManager)
            :base (eventFactory, queueManager)
        {
            //Check that eventFactory is not null
            if (eventFactory != null)
            {
                //Check that queue manager is not null
                if (queueManager != null)
                {
                    //check that sales manager is not null
                    if (salesManager != null)
                    {
                        this.salesManager = salesManager;
                    }
                    else //The given salesManager
                    {
                        throw new ArgumentNullException("salesManager", "Attempted to pass null SalesManager to SwitchCompleteProcessArgs.Process");
                    }
                }
                else // The given QueueManager was null
                {
                    throw new ArgumentNullException("queueManager", "Attempted to pass null QueueManager to SwitchCompleteProcessArgs constructor");
                }
            }
            else // The given EventFactory was null
            {
                throw new ArgumentNullException("eventFactory", "Attempted to pass null EventFactory to SwitchCompleteProcessArgs constructor");
            }
        }
    }
}
