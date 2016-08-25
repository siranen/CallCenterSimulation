using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DiscreteEventSimulator
{
    /// <summary>
    /// Defines the simulation Calendar, which is sorted in by the time the events are scheduled to occur
    /// </summary>
    public class Calendar
    {
        //-------------------------------------------------------------------
        //- FIELDS                                                          -
        //-------------------------------------------------------------------
        private List<Event> events;

        //-------------------------------------------------------------------
        //- PROPERTIES                                                      -
        //-------------------------------------------------------------------
        /// <summary>
        /// Gets a copy of the Calendars event list
        /// </summary>
        public List<Event> Events
        {
            get { return new List<Event>(events); }
        }

        //-------------------------------------------------------------------
        //- METHODS                                                         -
        //-------------------------------------------------------------------
        /// <summary>
        /// Creates a new instance of the Calendar class
        /// </summary>
        public Calendar()
        {
            this.events = new List<Event>();
        }

        /// <summary>
        /// Returns the next event that will occur
        /// </summary>
        /// <returns>The First event in the list or null if the list is empty</returns>
        public Event NextEvent()
        {
            //Get the next scheduled event
            Event nextScheduled = events.FirstOrDefault();

            //If an event was returned
            if (nextScheduled != null)
            {
                //Remove the event from the calendar
                RemoveEvent(nextScheduled); 
            }

            //Return the event
            return nextScheduled;            
        }

        /// <summary>
        /// Adds the given event to the calendar at the correct chronological location
        /// </summary>
        /// <param name="eventToAdd">The event to add to the system</param>
        /// <exception cref="System.ArgumentException">Thrown when the eventToAdd's EventTime is null</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the given eventToAdd is null</exception>
        public void AddEvent(Event eventToAdd)
        {
            //If the given event is null
            if (eventToAdd != null)
            {
                //If the given events EventTime is not null
                if (eventToAdd.EventTime != null)
                {
                    //Get the index of the first event in the queue that has an EventTime greter than eventToAdd's
                    int index = events.Count;
                    Event firstGreater = null;
                    //Get the first event that meets the requirements
                    firstGreater = events.FirstOrDefault(e => e.EventTime.CompareTo(eventToAdd.EventTime) > 0);
                    //If an event was returned
                    if (firstGreater != null)
                    {
                        //Get the index of the firstGreater event
                        index = events.IndexOf(firstGreater);
                    }

                    //Insert the event at the position of index
                    events.Insert(index, eventToAdd);
                    
                }
                else //The given events EventTime is null throw an exception
                {
                    throw new ArgumentException("Attempted to pass Event with null EventTime to Calendar.AddEvent", "eventToAdd");
                }
            }
            else // The event was null throw an exception
            {
                throw new ArgumentNullException("eventToAdd", "Attempted to pass null event to Calendar.AddEvent");
            }
        }

        /// <summary>
        /// Removes a given event from the calendar
        /// </summary>
        /// <param name="toRemove">The event to be removed</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the given event is not in the Calendar</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the given event is null</exception>
        public void RemoveEvent(Event toRemove)
        {
            //Check if toRemove is null
            if (toRemove != null)
            {
                //Try and get the index of the event in the events
                int index = events.IndexOf(toRemove);

                //If IndexOf didn't return -1
                if (index >= 0)
                {
                    events.Remove(toRemove);
                }
                else //The event was not found in the calendar
                {
                    throw new ArgumentOutOfRangeException("toRemove", "Attempted to remove Event that was not contained in the Calendar");
                }
            }
            else // The given event was null
            {
                throw new ArgumentNullException("toRemove", "Attempted to pass a null Event to Calendar.RemoveEvent()");
            }
            
        }

        /// <summary>
        /// Returns the first event in the Calendar of the desired type
        /// </summary>
        /// <param name="type">The event type desired</param>
        /// <returns>An Event of the desired type or null if no events of that type are found</returns>
        public Event NextEventOfType(EEventType type)
        {
            Type t = null;

            //Switch on the given enum value
            switch (type)
            {
                case EEventType.CallArrive:
                    t = typeof(CallArriveEvent);
                    break;
                case EEventType.SwitchCompleted:
                    t = typeof(SwitchCompletedEvent);
                    break;
                case EEventType.CompletedService:
                    t = typeof(CompletedServiceEvent);
                    break;
                case EEventType.EndReplication:
                    t = typeof(EndReplicationEvent);
                    break;
            }

            //Get the first event in the events list whose type matches the desired type. If none match then toReturn will be null
            Event toReturn = events.FirstOrDefault(e => (e.GetType() == t));

            return toReturn;
        }

        /// <summary>
        /// Draws the Calendar to the given DataGridView
        /// </summary>
        /// <param name="dgv">The DataGridView to alter</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the given DataGridView object is null</exception>
        public void ToDataGridView(DataGridView dgv)
        {
            //Check that the given DataGridView is not null
            if (dgv != null)
            {
                //Loop for every event in the calendar
                foreach (Event e in events)
                {
                    //Retrieve the Events bound enitites ID if it has one
                    string entityID = e.Entity != null ? e.Entity.CallId.ToString() : "--";
                    string eventName = e.EventType;
                    string eventTime = e.EventTime.ToLongTimeString();
                    //Create the next three fields
                    string productType = "--";
                    string startTime = "--";
                    string beginWait = "--";
                    //Check that the events bounds entity is not null
                    if (e.Entity != null)
                    {
                        //If the product type is not null then retrieve its type name
                        productType = e.Entity.ProductType != null ? e.Entity.ProductType.TypeName : "--";
                        //If the startTime has been changed from the default
                        startTime = e.Entity.StartTime != DateTime.MinValue ? e.Entity.StartTime.ToLongTimeString() : "--";
                        //If the begin wait has been changed from the default
                        beginWait = e.Entity.BeginWait != DateTime.MinValue ? e.Entity.BeginWait.ToLongTimeString() : "--";
                    }

                    object[] rowVals = { entityID, eventName, eventTime, productType, startTime, beginWait };

                    //Create a grid row
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dgv, rowVals);

                    //Add the new row to the datagridview
                    dgv.Rows.Add(row);
                } // end Events foreach
            }
            else // The given DataGridView is null
            {
                throw new ArgumentNullException("dgv", "Attempted to pass null DataGridView Calendar.ToDataGridView");
            }
        }
    }
}
