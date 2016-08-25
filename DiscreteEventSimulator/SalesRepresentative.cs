using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscreteEventSimulator
{
    /// <summary>
    /// Defines a sales representitive in the simulator
    /// </summary>
    public class SalesRepresentative
    {
        //-------------------------------------------------------------------
        //- FIELDS                                                          -
        //-------------------------------------------------------------------
        private Call currentlyProcessing;
        private SalesRepType repType;

        //-------------------------------------------------------------------
        //- PROPERTIES                                                      -
        //-------------------------------------------------------------------
        /// <summary>
        /// Gets/Sets the Call that this Sales Representitive is currently processing
        /// </summary>
        public Call CurrentlyProcessing
        {
            get { return currentlyProcessing; }
            set { currentlyProcessing = value; }
        }

        /// <summary>
        /// Gets the SalesRepType of this SalesRepresentative
        /// </summary>
        public SalesRepType RepType
        {
            get { return repType; }
        }

        //-------------------------------------------------------------------
        //- METHODS                                                         -
        //-------------------------------------------------------------------
        /// <summary>
        /// Creates an new instance of the SalesRepresentative class
        /// </summary>
        /// <param name="repType">The SalesRepType of this SalesRepresentative</param>
        public SalesRepresentative(SalesRepType repType)
        {
            // Check that the given SalesRepType is not null
            if (repType != null)
            {
                this.repType = repType;
            }
            else // The given SalesRepType is null
            {
                throw new ArgumentNullException("repType", "Attempted to pass null SalesRepType to SalesRepresentative constructor");
            }
        }

        /// <summary>
        /// Gets the List of ProductTypes that this SalesRepresentative can process
        /// </summary>
        /// <returns>A List of ProductTypes</returns>
        public List<ProductType> GetProcessableProductTypes()
        {
            return repType.Handles;
        }
    }
}
