using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscreteEventSimulator
{
    /// <summary>
    /// Creates Events for the simulation
    /// </summary>
    public class EventFactory
    {
        //-------------------------------------------------------------------
        //- METHODS                                                         -
        //-------------------------------------------------------------------
        /// <summary>
        /// Creates an instance of the EventFactory class
        /// </summary>
        public EventFactory()
        {

        }

        /// <summary>
        /// Creates an Event of the designated type, to occure at a specified time, and bound to a given entity
        /// </summary>
        /// <param name="eventType">The type of event to create</param>
        /// <param name="eventTime">The time the event will occur</param>
        /// <param name="entity">The Call this event is bound to (Can be null)</param>
        /// <returns></returns>
        public Event CreateEvent(EEventType eventType, DateTime eventTime, Call entity)
        {
            //Create an event to return
            Event eve = null;

            //Switch on the possible event types
            switch (eventType)
            {
                case EEventType.CallArrive:
                    eve = new CallArriveEvent(entity, eventTime);
                    break;
                case EEventType.SwitchCompleted:
                    eve = new SwitchCompletedEvent(entity, eventTime);
                    break;
                case EEventType.CompletedService:
                    eve = new CompletedServiceEvent(entity, eventTime);
                    break;
                case EEventType.EndReplication:
                    eve = new EndReplicationEvent(eventTime);
                    break;
            }

            return eve;
        }
    }
}
