using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscreteEventSimulator
{
    /// <summary>
    /// Defines a factory for creating EventProcessArgs instances for use in Event processing
    /// </summary>
    public class ProcessArgsFactory
    {
        //-------------------------------------------------------------------
        //- FIELDS                                                          -
        //-------------------------------------------------------------------
        private Simulator sim;

        //-------------------------------------------------------------------
        //- METHODS                                                         -
        //-------------------------------------------------------------------

        /// <summary>
        /// Creates a new instance of the ProcessArgsFactory classes
        /// </summary>
        /// <param name="sim">The simulator this factory creates EventProcessArgs for</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the given Simulator is null</exception>
        public ProcessArgsFactory(Simulator sim)
        {
            // Check that the given Simulator instance is not none
            if (sim != null)
            {
                this.sim = sim;
            }
            else // The given Simulator was null
            {
                throw new ArgumentNullException("sim", "Attempted to pass null Simulator to ProgressArgsFactory constructor");
            }
        }

        /// <summary>
        /// Creates and returns an the correct EventProcessArgs child class for the given event type
        /// </summary>
        /// <param name="e">The event to create the EventProcessArgs for</param>
        /// <returns>An instance of an EventProcessArgs child classes</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the given Event is null</exception>
        public EventProcessArgs CreateProcessArgsFor(Event e)
        {
            //Check that e is not null
            if (e != null)
            {
                // Create the EventProcessArgs return variable
                EventProcessArgs epa = null;

                // Check if e is a CallArriveEvent
                if (e is CallArriveEvent)
                {
                    epa = CreateCallArriveArgs();
                }
                else // e is not a CallArrive
                {
                    // Check if e is a SwitchCompleteEvent
                    if (e is SwitchCompletedEvent)
                    {
                        epa = CreateSwitchCompleteArgs();
                    }
                    else // e is not a SwitchCompleteEvent
                    {
                        // Check if e is a CompletedServiceEvent 
                        if (e is CompletedServiceEvent)
                        {
                            epa = CreateCompleteServiceArgs();
                        }
                        else // e is not a CompletedServiceEvent
                        {
                            // Check if e is an EndReplicationEvent
                            if (e is EndReplicationEvent)
                            {
                                epa = CreateEndReplicationArgs();
                            }
                            // else leave epa null
                        }
                    }
                }

                return epa;
            }
            else // e is null
            {
                throw new ArgumentNullException("e", "Attempted to pass null Event to ProcessArgsFactory.CreateProcessArgsFor");
            }
        }

        /// <summary>
        /// Creates and returns a new instance of the EndReplicationProcessArgs class
        /// </summary>
        /// <returns>An instance of EndReplicationProcessArgs</returns>
        private EventProcessArgs CreateEndReplicationArgs()
        {
            return new EndReplicationProcessArgs();
        }

        /// <summary>
        /// Creates and returns a new instance of the CompletedServiceProcessArgs class
        /// </summary>
        /// <returns>An instance of the CompletedServiceProcessArgs</returns>
        private EventProcessArgs CreateCompleteServiceArgs()
        {
            return new CompletedServiceProcessArgs(sim.EventFactory, sim.QueueManager);
        }

        /// <summary>
        /// Creates and returns a new instance of the SwitchCompletedProcessArgs class
        /// </summary>
        /// <returns>An instance of the SwitchCompletedProcessArgs class</returns>
        private EventProcessArgs CreateSwitchCompleteArgs()
        {
            return new SwitchCompleteProcessArgs(sim.EventFactory, sim.QueueManager, sim.SalesManager);
        }

        /// <summary>
        /// Creates and returns an instance of the CallArriveProcessArgs class
        /// </summary>
        /// <returns>An instance of the CallArriveProcessArgs class</returns>
        private EventProcessArgs CreateCallArriveArgs()
        {
            return new CallArriveProcessArgs(sim.EventFactory, sim.QueueManager, sim.CallFactory, sim.ProductTypes, sim.CallArriveMultiplier, sim.SwitchDelayMultiplier);
        }

    }
}
