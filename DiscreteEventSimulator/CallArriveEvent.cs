using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscreteEventSimulator
{
    /// <summary>
    /// Defines a Call Arrival Event
    /// </summary>
    public class CallArriveEvent : Event
    {
        //-------------------------------------------------------------------
        //- FIELDS                                                          -
        //-------------------------------------------------------------------
        private bool hangUp;

        //-------------------------------------------------------------------
        //- PROPERTIES                                                      -
        //-------------------------------------------------------------------
        /// <summary>
        /// Gets whether or not the processing of this event resulted in a hang up
        /// </summary>
        public bool HangUp
        {
            get { return hangUp; }
        }

        //-------------------------------------------------------------------
        //- METHODS                                                         -
        //-------------------------------------------------------------------
        /// <summary>
        /// Creates a new instance of the CallArrive class
        /// </summary>
        /// <param name="entity">The call this event is bound to</param>
        /// <param name="eventTime">The time this event is set to occur</param>
        public CallArriveEvent(Call entity, DateTime eventTime)
            : base(entity, eventTime)
        {
            eventType = "Arrive at Call Centre";
        }

        /// <summary>
        /// Processes the event
        /// </summary>
        /// <param name="args">An instance of the CallArriveProcessArgs class</param>
        /// <returns>A list of events that processing this event spawned</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the given args is null</exception>
        /// /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the given args is not the correct EventProcessArgs child type</exception>
        public override List<Event> Process(EventProcessArgs args)
        {
            // if the given args is not null
            if (args != null)
            {
                // Attempt to cast the args to the correct EventProcessArgs child type
                CallArriveProcessArgs pArgs = args as CallArriveProcessArgs;

                //If the cast worked
                if (pArgs != null)
                {
                    //Create the list of events to return
                    List<Event> spawnedEvents = new List<Event>();

                    //Set up the call
                    entity.StartTime = eventTime;
                    entity.ProductType = ChooseProductType(pArgs.ProductTypes);

                    //Check if there are to many calls waiting for this product type
                    if (args.QueueManager.IsQueueTooLong(entity.ProductType) == false)
                    {
                        //Calculate the time of the new SwitchCompletedEvent
                        DateTime switchCompleteTime = eventTime;
                        int roll = NormalDistributor.Roll();
                        double timespanInMins = roll * pArgs.SwitchDelayMultiplier;
                        //Add the minutes to the switchCompleteTime
                        switchCompleteTime = switchCompleteTime.AddMinutes(timespanInMins);
                        //Use the event factory to create a switch complete event
                        Event switchComplete = args.EventFactory.CreateEvent(EEventType.SwitchCompleted, switchCompleteTime, entity);
                        spawnedEvents.Add(switchComplete);
                        hangUp = false;
                    }
                    else
                    {
                        hangUp = true;
                    }

                    //Create the next CallArrive event
                    DateTime nextCallArriveTime = eventTime;
                    int rollNewCall = NormalDistributor.Roll();
                    double timespanInMinsNewCall = rollNewCall * pArgs.CallArriveMultiplier;
                    nextCallArriveTime = nextCallArriveTime.AddMinutes(timespanInMinsNewCall);
                    //GetProcessableProductTypes a new blank call for the next event
                    Call newCall = pArgs.CallFactory.CreateCall();
                    //Use the event factory to create a call arrive event
                    Event callArrive = args.EventFactory.CreateEvent(EEventType.CallArrive, nextCallArriveTime, newCall);
                    spawnedEvents.Add(callArrive);

                    //Return all spawned events
                    return spawnedEvents;
                }
                else // The given args could not be cast to the correct type
                {
                    throw new ArgumentOutOfRangeException("args", "Attempted to pass invalid args to CallArriveEvent.Process");
                }
            }
            else // The given args are null
            {
                throw new ArgumentNullException("args", "Attempted to pass null CallArriveProcessArgs to CallArriveEvent.Process");
            }
        }

        /// <summary>
        /// Returns a ProductType from the given list based on its probability
        /// </summary>
        /// <param name="pTypes">The list of possible product types</param>
        /// <returns>A ProductType instance from the given list, null by default</returns>
        private ProductType ChooseProductType(List<ProductType> pTypes)
        {
            //Create a Psuedo-random number generator
            Random rand = new Random();

            //Get a random value
            double percentage = rand.NextDouble();

            //Create the return value
            ProductType chosenType = null;

            //Create a count of the percentage taken so far
            double percentageSoFar = 0;

            //Loop foreach product type in the given list
            foreach (ProductType pt in pTypes.OrderByDescending(p => p.ProductTypeProbability))
            {
                //If the generated percentage is greater than the percentage consumed so far and less than the highest percentage for this product type
                if (percentage > percentageSoFar && (percentage <= (pt.ProductTypeProbability + percentageSoFar)))
                {
                    //Set the chosen type
                    chosenType = pt;
                }
                //Add the product types probability to the percentageSoFar
                percentageSoFar += pt.ProductTypeProbability;
                
            }

            //Return the selected product type
            return chosenType;
        }
    }
}
