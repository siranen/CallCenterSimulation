using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscreteEventSimulator
{
    /// <summary>
    /// An event within the system
    /// </summary>
    public abstract class Event
    {
        //-------------------------------------------------------------------
        //- FIELDS                                                          -
        //-------------------------------------------------------------------
        protected DateTime eventTime;
        protected Call entity;
        protected string eventType;

        //-------------------------------------------------------------------
        //- PROPERTIES                                                      -
        //-------------------------------------------------------------------

        /// <summary>
        /// The time that the event is scheduled to occur
        /// </summary>
        public DateTime EventTime
        {
            get { return eventTime; }
        }

        /// <summary>
        /// The Call (If applicable) that this event is bound to
        /// </summary>
        public Call Entity
        {
            get { return entity; }
        }

        /// <summary>
        /// Gets the string name of this event
        /// </summary>
        public string EventType
        {
            get { return eventType; }
        }
        
        //-------------------------------------------------------------------
        //- METHODS                                                         -
        //-------------------------------------------------------------------
        /// <summary>
        /// Creates a new instance of the Event class, used by concrete classes for setup
        /// </summary>
        /// <param name="entity">The call that this event will be bound to</param>
        /// <param name="eventTime">The time that the event will occur</param>
        public Event(Call entity, DateTime eventTime)
        {
           
            this.entity = entity;
            this.eventTime = eventTime;
        }

        /// <summary>
        /// Processes the event
        /// </summary>
        /// <param name="args">An instance of the Simulator class</param>
        /// <returns>A List of Events spawned by the processing of this one</returns>
        public abstract List<Event> Process(EventProcessArgs args);

        /// <summary>
        /// Returns a System.String representing this Event
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            //Create a stringbuilder
            StringBuilder sb = new StringBuilder();

            sb.Append("EventTime: " + eventTime.ToShortTimeString());
            //If the Events entity is not null
            if (entity != null)
            {
                //Append the calls tostring to the stringbuilder
                sb.Append(", Entity Info: " + entity.ToString());
            }
            //else nothing to add

            return sb.ToString();
        }
    }
}
