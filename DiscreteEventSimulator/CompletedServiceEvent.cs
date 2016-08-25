using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscreteEventSimulator
{
    /// <summary>
    /// Defines a processing completed event
    /// </summary>
    public class CompletedServiceEvent : Event
    {
        //-------------------------------------------------------------------
        //- METHODS                                                         -
        //-------------------------------------------------------------------

        /// <summary>
        /// Creates a new instance of the CompletedServiceEvent class
        /// </summary>
        /// <param name="entity">The Call this event is bound to</param>
        /// <param name="eventTime">The time this event is scheduled to occur</param>
        public CompletedServiceEvent(Call entity, DateTime eventTime)
            :base(entity, eventTime)
        {
            eventType = "Completed Service";
        }

        /// <summary>
        /// Processes the event
        /// </summary>
        /// <param name="args">An instance of the CompleteServiceProcessArgs class</param>
        /// <returns>A list of events that processing this event spawned</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the given args is null</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the given args is not the correct EventProcessArgs child type</exception>
        public override List<Event> Process(EventProcessArgs args)
        {
            //If the given args is not null
            if (args != null)
            {
                // Attempt to cast the args to the correct EventProcessArgs child type
                CompletedServiceProcessArgs pArgs = args as CompletedServiceProcessArgs;

                //If the cast worked
                if (pArgs != null)
                {
                    //Create the list of Events to return
                    List<Event> spawnedEvents = new List<Event>();

                    //Set this events entities finish time
                    entity.FinishTime = eventTime;

                    //GetProcessableProductTypes the salesrep that was assigned this entity
                    SalesRepresentative salesrep = entity.ProcessedBy;
                    //remove the salesRep's currentlly processing
                    salesrep.CurrentlyProcessing = null;

                    //Get a new call from the queue manager
                    Call nextCall = pArgs.QueueManager.GetCallForRepType(salesrep.RepType);

                    //If the above method returned a call
                    if (nextCall != null)
                    {
                        nextCall.BeganProcessing = eventTime;
                        //Assign them to each other
                        nextCall.ProcessedBy = salesrep;
                        salesrep.CurrentlyProcessing = nextCall;

                        //Get the next calls product type
                        ProductType ncProductType = nextCall.ProductType;

                        //create the processing complete event
                        DateTime processingTime = eventTime;
                        double timespanInMins = NormalDistributor.Roll() * ncProductType.ProcessingDelayMultiplier;
                        processingTime = processingTime.AddMinutes(timespanInMins);
                        Event processComplete = pArgs.EventFactory.CreateEvent(EEventType.CompletedService, processingTime, nextCall);
                        spawnedEvents.Add(processComplete);
                    }
                    //else No calls waiting in queue

                    //Return the list of spawned events
                    return spawnedEvents;
                }
                else // The given args could not be cast to the correct type
                {
                    throw new ArgumentOutOfRangeException("args", "Attempted to pass invalid args to CompletedServiceEvent.Process");
                }
            }
            else // The given args was null
            {
                throw new ArgumentNullException("args", "Attempted to pass null args to CompletedServiceEvent.Process");
            }
        }
    }
}
