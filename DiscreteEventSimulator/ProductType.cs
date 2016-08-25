using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscreteEventSimulator
{
    /// <summary>
    /// Defines a type of product in the simulation. At anytime there should only be one instance of a 
    /// </summary>
    public class ProductType
    {
        //-------------------------------------------------------------------
        //- FIELDS                                                          -
        //-------------------------------------------------------------------
        private string typeName;
        private double processingDelayMultiplier;
        private double productTypeProbability;

        //-------------------------------------------------------------------
        //- PROPERTIES                                                      -
        //-------------------------------------------------------------------
        /// <summary>
        /// Gets the name of the product type
        /// </summary>
        public string TypeName
        {
            get { return typeName; }
        }
        
        /// <summary>
        /// Gets/Sets the multiplier used to calculate the time this product type spends being processed
        /// </summary>
        public double ProcessingDelayMultiplier
        {
            get { return processingDelayMultiplier; }
            set { processingDelayMultiplier = value; }
        }
        
        /// <summary>
        /// Gets/Sets the probaility of this product type occuring
        /// </summary>
        public double ProductTypeProbability
        {
            get { return productTypeProbability; }
            set { productTypeProbability = value; }
        }

        //-------------------------------------------------------------------
        //- METHODS                                                         -
        //-------------------------------------------------------------------
        /// <summary>
        /// Creates a new instance of the ProductType class
        /// </summary>
        /// <param name="typeName">The string name of the type i.e "Car Stereo"</param>
        /// <param name="processingDelayMultiplier">The multiplier for calculating the amount of time this product type spends in processing</param>
        /// <param name="productTypeProbability">The probability of a call being for this product type</param>
        public ProductType(string typeName, double processingDelayMultiplier, double productTypeProbability)
        {
            // Check that the given typename is not null
            if (typeName != null && typeName != string.Empty)
            {
                this.typeName = typeName;
                this.processingDelayMultiplier = processingDelayMultiplier;
                this.productTypeProbability = productTypeProbability;
            }
            else // The type name is null
            {
                throw new ArgumentNullException("typeName", "Attempted to pass null typename to ProductType constructor");
            }
        }

        /// <summary>
        /// Returns a System.String representing the current ProductType
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Name: " + typeName);
            sb.Append(", PDM: " + processingDelayMultiplier);
            sb.Append(", PTP: " + productTypeProbability);

            return sb.ToString();
        }
    }
}
