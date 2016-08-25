using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscreteEventSimulator
{
    /// <summary>
    /// Defines a Switch Completion event
    /// </summary>
    public class SwitchCompletedEvent : Event
    {
        //-------------------------------------------------------------------
        //- METHODS                                                         -
        //-------------------------------------------------------------------
        /// <summary>
        /// Creates a new instance of the SwitchCompletedEvent class
        /// </summary>
        /// <param name="entity">The entity this event is bound to</param>
        /// <param name="eventTime">The time that this event will occur</param>
        public SwitchCompletedEvent(Call entity, DateTime eventTime)
            : base(entity, eventTime)
        {
            eventType = "Completed Switch Processing";
        }

        /// <summary>
        /// Processes the event
        /// </summary>
        /// <param name="args">An instance of the SwitchCompleteProcessArgs</param>
        /// <returns>A list of events that processing this event spawned</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the given args is null or not of the correct type</exception>
        /// /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the given args is not the correct EventProcessArgs child type</exception>
        public override List<Event> Process(EventProcessArgs args)
        {
            // If the given args is not null
            if (args != null)
            {
                // Attempt to cast the args to the correct EventProcessArgs child type
                SwitchCompleteProcessArgs pArgs = args as SwitchCompleteProcessArgs;

                //If the cast worked
                if (pArgs != null)
                {
                    //Create the list of events to return
                    List<Event> spawnedEvents = new List<Event>();

                    //GetProcessableProductTypes the product type from the entity
                    ProductType entityProductType = entity.ProductType;
                    //GetProcessableProductTypes a sales representative from the Simulations SalesManager if one is available
                    SalesRepresentative salesRep = pArgs.SalesManager.GetRepresentativeForProductType(entityProductType);
                    //If a SalesRepresentative was returned above, assign the entity to it
                    if (salesRep != null)
                    {
                        entity.BeganProcessing = eventTime;
                        salesRep.CurrentlyProcessing = entity;
                        entity.ProcessedBy = salesRep;
                        //create the processing complete event
                        DateTime processingTime = eventTime;
                        double timespanInMins = NormalDistributor.Roll() * entityProductType.ProcessingDelayMultiplier;
                        processingTime = processingTime.AddMinutes(timespanInMins);
                        Event processComplete = args.EventFactory.CreateEvent(EEventType.CompletedService, processingTime, entity);
                        spawnedEvents.Add(processComplete);
                    }
                    else //No representative was available
                    {
                        //Set the enitities begin wait
                        entity.BeginWait = eventTime;
                        //Add the call to the queue
                        args.QueueManager.AddToQueue(entity);
                    }

                    //Return the list of events spawned as a result of this
                    return spawnedEvents;
                }
                else // The given args could not be cast to the correct type
                {
                    throw new ArgumentOutOfRangeException("args", "Attempted to pass invalid args to SwitchCompletedEvent.Process");
                }
            }
            else // The given args was null
            {
                throw new ArgumentNullException("args", "Attempted to pass null EventProcessArgs to SwitchCompletedEvent.Process");
            }
        }
    }
}
