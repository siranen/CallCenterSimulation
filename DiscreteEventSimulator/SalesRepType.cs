using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscreteEventSimulator
{
    /// <summary>
    /// Defines a type of SalesRepresentative. Only one instance of a certain type should exist in the simulation at any one time
    /// </summary>
    public class SalesRepType
    {
        //-------------------------------------------------------------------
        //- FIELDS                                                          -
        //-------------------------------------------------------------------
        private string typeName;
        private List<ProductType> handles;

        //-------------------------------------------------------------------
        //- PROPERTIES                                                      -
        //-------------------------------------------------------------------
        /// <summary>
        /// Gets the string name of this type, may match a ProductType's name (i.e. "Car Sterro") or may be more generic (i.e "Electronics Specialist")
        /// </summary>
        public string TypeName
        {
            get { return typeName; }
            set { typeName = value; }
        }

        /// <summary>
        /// Get/Sets the list of ProductTypes that this SalesRepType can handle calls for
        /// </summary>
        public List<ProductType> Handles
        {
            get { return handles; }
            set { handles = value; }
        }

        //-------------------------------------------------------------------
        //- METHODS                                                         -
        //-------------------------------------------------------------------
        /// <summary>
        /// Creates a new instance of the SalesRepType class with the given typename
        /// </summary>
        /// <param name="typeName">The name for this SalesRepType</param>
        public SalesRepType(string typeName)
        {
            // Check that the given typename is not null  or empty
            if (typeName != null && typeName != string.Empty)
            {
                this.typeName = typeName;
                this.handles = new List<ProductType>();
            }
            else // The given typeName was null or empty
            {
                throw new ArgumentNullException("typeName", "Attempted to pass null or empty string to SalesRepType Constructor");
            }
        }

        /// <summary>
        /// Returns a System.String representing the current SalesRepType
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            //Create the stringbuilder
            StringBuilder sb = new StringBuilder();

            sb.Append("Type Name: " + typeName);

            sb.Append(", Handles: ");

            //Loop foreach ProductType in the Handles list
            foreach (ProductType pt in handles)
            {
                sb.Append(pt.TypeName + ", ");
            }
            
            //Return the string and trim off the extra comma and space at the end
            return sb.ToString().TrimEnd(',', ' ');
        }
    }
}
