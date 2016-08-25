using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscreteEventSimulator
{
    /// <summary>
    /// Defines the Manager of the Sales force in the simulation
    /// </summary>
    public class SalesForceManager
    {
        //-------------------------------------------------------------------
        //- FIELDS                                                          -
        //-------------------------------------------------------------------
        private List<SalesRepType> repTypes;
        private Dictionary<SalesRepType, List<SalesRepresentative>> salesForce;
        private Dictionary<SalesRepType, int> numberOfReps;

        //-------------------------------------------------------------------
        //- PROPERTIES                                                      -
        //-------------------------------------------------------------------
        /// <summary>
        /// Gets a copy of repTypes list
        /// </summary>
        public List<SalesRepType> RepTypes
        {
            get { return new List<SalesRepType>(repTypes); }
        }
        /// <summary>
        /// Gets a copy of the salesForce dictionary
        /// </summary>
        public Dictionary<SalesRepType, List<SalesRepresentative>> SalesForce
        {
            get { return new Dictionary<SalesRepType,List<SalesRepresentative>>(salesForce); }
        }

        /// <summary>
        /// Gets a copy of the numberOfReps Dictionary
        /// </summary>
        public Dictionary<SalesRepType, int> NumberOfReps
        {
            get { return new Dictionary<SalesRepType,int>(numberOfReps); }
        }

        //-------------------------------------------------------------------
        //- METHODS                                                         -
        //-------------------------------------------------------------------
        /// <summary>
        /// Creates a new instance of the SalesForceManager class
        /// </summary>
        /// <param name="numberOfReps">A dictionary of SalesRepTypes and the number of SalesRepresentatives of that type to create</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the given Dictionary is null or contains no values</exception>
        public SalesForceManager(Dictionary<SalesRepType, int> numberOfReps)
        {
            //Check that the given Dictionary is not null AND contains values
            if (numberOfReps != null && (numberOfReps.Count > 0))
            {
                this.numberOfReps = numberOfReps;
                this.salesForce = new Dictionary<SalesRepType, List<SalesRepresentative>>();
                
                //Get the keys from the dictionary
                List<SalesRepType> keys = numberOfReps.Keys.ToList();

                //Set the repTypes variable to be the keys list
                repTypes = keys;

                //Foreach key create an entry in the salesForce dictionary
                foreach (SalesRepType srt in keys)
                {
                    salesForce.Add(srt, new List<SalesRepresentative>());
                }

                //foreach value in numberOfReps
                foreach (KeyValuePair<SalesRepType,int> numReps in numberOfReps)
                {
                    //Loop for the number of salesReps to add
                    for (int i = 0; i < numReps.Value; i++)
                    {
                        //Create a new sales rep and add it to the appropriate list
                        salesForce[numReps.Key].Add(new SalesRepresentative(numReps.Key));
                    }
                }
            }
            else //numberOfReps was null or empty
            {
                throw new ArgumentNullException("numberOfReps", "Attempted to pass empty or null Dictionary to SalesForceManager constructor");
            }
        }

        /// <summary>
        /// Adds the given SalesRepresentative to the salesforce, if the salesrep is of a known type
        /// </summary>
        /// <param name="representative">The SalesRepresentative to add</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the given SalesRepresentative has a SalesRepType that is not already in the SalesForce</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the given representative is null</exception>
        public void AddSalesRepresentative(SalesRepresentative representative)
        {
            //Check that the given SalesRepresentative is not null
            if (representative != null)
            {
                //If the salesForce contains the an entry with the given representatives reptype
                if (salesForce.ContainsKey(representative.RepType))
                {
                    //Add the representative to the correct list
                    salesForce[representative.RepType].Add(representative);
                    //Increase the number of salesReps for that type
                    numberOfReps[representative.RepType]++;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("representative", "Attempted to add a representative with a SalesRepType that was not already in the salesForce");
                }
            }
            else //representative is null throw an exception
            {
                throw new ArgumentNullException("representative", "Attempted to pass null SalesRepresentative to SalesForceManager.AddSalesRepresentative");
            }
        }

        /// <summary>
        /// Gets the number of sales representatives of a given type
        /// </summary>
        /// <param name="repType">The SalesRepType to get the count of</param>
        /// <returns>The count of that type of SalesRepresentative</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the given repType is not in the SalesForce</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the given repType is null</exception>
        public int RepresentativesOfType(SalesRepType repType)
        {
            //If repType is not null
            if (repType != null)
            {
                //If the SalesForce contains this type
                if (salesForce.ContainsKey(repType))
                {
                    //Return the number of SalesRepresentatives of this type
                    return salesForce[repType].Count;
                }
                else //This type was not contained in the dictionary
                {
                    throw new ArgumentOutOfRangeException("repType", "Attempted to pass a SalesRepType that was not already in the SalesForce to SalesForceManager.RepresentativesOfType");
                }
            }
            else //repType was null throw an exception
            {
                throw new ArgumentNullException("repType", "Attempted to pass null SalesRepType to SalesForceManager.RepresentativesOfType");
            }
        }

        /// <summary>
        /// Gets the number of sales representatives of a given type that are currently processing calls
        /// </summary>
        /// <param name="repType">The SalesRepType to get the count of</param>
        /// <returns>The count of that type of SalesRepresentative that are busy</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the given repType is not in the SalesForce</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the given repType is null</exception>
        public int RepresentativesBusy(SalesRepType repType)
        {
            //If repType is not null
            if (repType != null)
            {
                //If the SalesForce contains this type
                if (salesForce.ContainsKey(repType))
                {
                    //Return the number of SalesRepresentatives of this type that do not currently have assigned calls
                    return salesForce[repType].Where(sr => sr.CurrentlyProcessing != null).Count();
                }
                else //This type was not contained in the dictionary
                {
                    throw new ArgumentOutOfRangeException("repType", "Attempted to pass a SalesRepType that was not already in the SalesForce to SalesForceManager.RepresentativesBusy");
                }
            }
            else //repType was null throw an exception
            {
                throw new ArgumentNullException("repType", "Attempted to pass null SalesRepType to SalesForceManager.RepresentativesBusy");
            }
        }

        /// <summary>
        /// Gets a SalesRepresentative that can handle the given type of product
        /// </summary>
        /// <param name="productType">The type of product</param>
        /// <returns>A SalesRepresentative that can handle the given ProductType. Null if no SalesRep was found</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the given product type is null</exception>
        public SalesRepresentative GetRepresentativeForProductType(ProductType productType)
        {
            // If the given product type is not null
            if (productType != null)
            {
                //Get the list of SalesRepTypes that can handle this product type
                var matching = from srt in salesForce.Keys
                               where srt.Handles.Contains(productType)
                               select srt;
                //If the number of matching SalesRepTypes is at least one
                if (matching.Count() > 0)
                {
                    //Get the SalesRepType that has the least number of busy sales reps
                    SalesRepType leastUtilised = matching.Aggregate((srt1, srt2) => (RepresentativesOfType(srt1) - RepresentativesBusy(srt1)) > (RepresentativesOfType(srt2) - RepresentativesBusy(srt2)) ? srt1 : srt2);

                    return GetFreeSalesRep(leastUtilised);
                }
                else // No such SalesRepType exists that can handle this product type
                {
                    return null;
                }
            }
            else // The given productType is null
            {
                throw new ArgumentNullException("productType", "Attempted to pass null ProductType to SalesForceManager.GetRepresentativeForProductType");
            }
        }

        /// <summary>
        /// Gets the first free SalesRep of the given type, null if no SalesRep is available
        /// </summary>
        /// <param name="repType">The type of the sales rep to retrieve</param>
        /// <returns>A SalesRepresentative, null if no SalesRepresentative is available</returns>
        private SalesRepresentative GetFreeSalesRep(SalesRepType repType)
        {
            //Check if the given rep type is not null
            if (repType != null)
            {
                // Check that the given repType is a key in the dictionary
                if (salesForce.ContainsKey(repType))
                {
                    //Get the first free Sales rep of the given type
                    SalesRepresentative firstFree = salesForce[repType].FirstOrDefault(sr => sr.CurrentlyProcessing == null);

                    return firstFree;
                }
                else // The given salesRepType is not in the dictionary
                {
                    throw new ArgumentOutOfRangeException("repType", "Attempted to pass SalesRepType that is not in the dictionary to SalesForceManager.GetFreeSalesRep");
                }
            }
            else //The repType is null
            {
                throw new ArgumentNullException("repType", "Attempted to pass null SalesRepType to SalesForceManager.GetFreeSalesRep");
            }
        }

        /// <summary>
        /// Removes the given SalesRepresentative from the salesforce, if the salesrep is of a known type
        /// </summary>
        /// <param name="representative">The SalesRepresentative to remove</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the given SalesRepresentative has a SalesRepType that is not already in the SalesForce</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the given representative is null</exception>
        public void RemoveRepresentative(SalesRepresentative representativeToRemove)
        {
            //Check that the given SalesRepresentative is not null
            if (representativeToRemove != null)
            {
                //If the salesForce contains the an entry with the given representatives reptype
                if (salesForce.ContainsKey(representativeToRemove.RepType))
                {
                    //Remove the representative to the correct list
                    salesForce[representativeToRemove.RepType].Remove(representativeToRemove);
                    //Decrease the number of salesReps for that type
                    numberOfReps[representativeToRemove.RepType]--;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("representativeToRemove", "Attempted to remove a representative with a SalesRepType that was not already in the salesForce");
                }
            }
            else //representative is null throw an exception
            {
                throw new ArgumentNullException("representativeToRemove", "Attempted to pass null SalesRepresentative to SalesForceManager.RemoveRepresentative");
            }
        }
    }
}
