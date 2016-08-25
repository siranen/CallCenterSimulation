using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscreteEventSimulator
{
    /// <summary>
    /// Defines an event that signifies the end of the simulation
    /// </summary>
    public class EndReplicationEvent : Event
    {
        //-------------------------------------------------------------------
        //- METHODS                                                         -
        //-------------------------------------------------------------------
        /// <summary>
        /// Creates an instance of the EndReplicationEvent class
        /// </summary>
        /// <param name="eventTime">The time this event will occur at</param>
        public EndReplicationEvent(DateTime eventTime)
            :base(null, eventTime)
        {
            eventType = "End Replication";
        }
    
        /// <summary>
        /// Does nothing
        /// </summary>
        /// <returns>An empty List of events</returns>
        public override List<Event> Process(EventProcessArgs args)
        {
            //Create and return the empty list
            return new List<Event>();
        }
    }
}
