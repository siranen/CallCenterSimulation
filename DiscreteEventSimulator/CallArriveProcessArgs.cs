using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscreteEventSimulator
{
    /// <summary>
    /// Defines the EventProcessArgs child class that provides access to data for the CallArriveEvent.Process method
    /// </summary>
    public class CallArriveProcessArgs : EventProcessArgs
    {
        //-------------------------------------------------------------------
        //- FIELDS                                                          -
        //-------------------------------------------------------------------
        private CallFactory callFactory;
        private List<ProductType> productTypes;
        private double callArriveMultiplier;
        private double switchDelayMultiplier;

        //-------------------------------------------------------------------
        //- PROPERTIES                                                      -
        //-------------------------------------------------------------------
        /// <summary>
        /// Gets the List of ProductTypes
        /// </summary>
        public List<ProductType> ProductTypes
        {
            get { return productTypes; }
        }

        /// <summary>
        /// Gets the CallFactory
        /// </summary>
        public CallFactory CallFactory
        {
            get { return callFactory; }
        }
        
        /// <summary>
        /// Gets the multiplier used to calculate call arrival times
        /// </summary>
        public double CallArriveMultiplier
        {
            get { return callArriveMultiplier; }
        }

        /// <summary>
        /// Gets the multiplier used to calculate the switch delay times
        /// </summary>
        public double SwitchDelayMultiplier
        {
            get { return switchDelayMultiplier; }
        }

        //-------------------------------------------------------------------
        //- METHODS                                                         -
        //-------------------------------------------------------------------

        /// <summary>
        /// Creates a new instance of the CallArriveProcessArgs class
        /// </summary>
        /// <param name="eventFactory">The instance of the EventFactory class</param>
        /// <param name="queueManager">The instance of the QueueManager class</param>
        /// <param name="callFactory">The instance of the CallFactory class</param>
        /// <param name="productTypes">The list of product types</param>
        /// <param name="callArriveMultiplier">The call arrival multiplier</param>
        /// <param name="switchDelayMultiplier">The switch delay multiplier</param>
        /// <exception cref="ArgumentNullException">Thrown when any one of the arguments is null</exception>
        public CallArriveProcessArgs(EventFactory eventFactory, QueueManager queueManager, CallFactory callFactory, List<ProductType> productTypes, double callArriveMultiplier, double switchDelayMultiplier)
            :base(eventFactory, queueManager)
        {
            //Check that eventFactory is not null
            if (eventFactory != null)
            {
                //Check that queueManager is not null
                if (queueManager != null)
                {
                    //Check that callFactory is not null
                    if (callFactory != null)
                    {
                        //Check that the list of product types is not null
                        if (productTypes != null)
                        {
                            this.callFactory = callFactory;
                            this.productTypes = productTypes;
                            this.callArriveMultiplier = callArriveMultiplier;
                            this.switchDelayMultiplier = switchDelayMultiplier;
                        }
                        else // The list of ProductTypes was null
                        {
                            throw new ArgumentNullException("productTypes", "Attempted to pass null ProductType List to CallArriveProcessArgs constructor");
                        }
                    }
                    else // The given call factory was null
                    {
                        throw new ArgumentNullException("callFactory", "Attempted to pass null CallFactory to CallArriveProcessArgs constructor");
                    }
                }
                else // The given QueueManager was null
                {
                    throw new ArgumentNullException("queueManager", "Attempted to pass null QueueManager to CallArriveProcessArgs constructor");
                }
            }
            else // The given EventFactory was null
            {
                throw new ArgumentNullException("eventFactory", "Attempted to pass null EventFactory to CallArriveProcessArgs constructor");
            }
        }
    }
}
