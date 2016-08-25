using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DiscreteEventSimulator
{
    /// <summary>
    /// Defines a Manager of queues in the simulation
    /// </summary>
    public class QueueManager
    {
        //-------------------------------------------------------------------
        //- FIELDS                                                          -
        //-------------------------------------------------------------------
        private List<Queue> queues;
        private int maxQueueLength;
        private bool singleQueueLength;

        //-------------------------------------------------------------------
        //- PROPERTIES                                                      -
        //-------------------------------------------------------------------

        /// <summary>
        /// Gets the maximum length of a queue
        /// </summary>
        public int MaxQueueLength
        {
            get { return maxQueueLength; }
        }

        /// <summary>
        /// Gets a copy of the List of queues
        /// </summary>
        public List<Queue> Queues
        {
            get { return new List<Queue>(queues); }
        }

        /// <summary>
        /// Gets whether or not the QueueManager is set to limit the number of calls in all queues to a single value
        /// </summary>
        public bool SingleQueueLength
        {
            get { return singleQueueLength; }
        }

        //-------------------------------------------------------------------
        //- METHODS                                                         -
        //-------------------------------------------------------------------
        /// <summary>
        /// Creates a new instance of the QueueManager class
        /// </summary>
        /// <param name="maxQueueLength">The maximum length of a queue</param>
        /// <param name="productTypes">The List of ProductTypes in the simulation</param>
        /// <param name="singleQueueLength">Whether or not the maxQueueLength applies to the sum of all queues or to each individual queue</param>
        public QueueManager(int maxQueueLength, List<ProductType> productTypes, bool singleQueueLength)
        {
                this.maxQueueLength = maxQueueLength;
                this.queues = new List<Queue>();
                this.singleQueueLength = singleQueueLength;

                //If the given list of product types is not null
                if (productTypes != null && productTypes.Count > 0)
                {
                    foreach (ProductType pt in productTypes)
                    {
                        //Create a queue for this product type
                        Queue ptQueue = new Queue(pt);

                        //Add the queue to the list of all queues
                        queues.Add(ptQueue);
                    }
                }
                else // The list of productTypes is null
                {
                    throw new ArgumentNullException("productTypes", "Attempted to pass null or empty List to QueueManager constructor");
                } 
            
        }

        /// <summary>
        /// Gets whether or not the Queue is too long
        /// </summary>
        /// <param name="productType">The product type of the Queue</param>
        /// <returns>true if the Queue is too long, false if it is not</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the given ProductType does not match a Queue</exception>
        /// <exception cref="System.ArgumentNull">Thrown when productType is null</exception>
        public bool IsQueueTooLong(ProductType productType)
        {
            //If the given product type is not null, or singleQueue is true
            if (productType != null)
            {
                //Check which queueLength mode the QueueManager is in
                if (!singleQueueLength)
                {
                    //Get the queue with the matched product type
                    Queue matchedQueue = queues.Find(q => q.TypeInQueue == productType);

                    //If the matched queue is not null
                    if (matchedQueue != null)
                    {
                        //If the matchedQueues length is too long
                        if (matchedQueue.Calls.Count >= maxQueueLength)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else // The given product type doesn't match any queue
                    {
                        throw new ArgumentOutOfRangeException("productType", "Passed in a ProductType that has no matching Queue");
                    }
                }
                else // singleQueueLength is true
                {
                    //Get the sum of all calls in all queues
                    int totalCallsInAllQueues = queues.Sum(q => q.Calls.Count);

                    //Check if the sum of all calls is greater than the maximum queue length
                    if (totalCallsInAllQueues >= maxQueueLength)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else // The productType was null
            {
                throw new ArgumentNullException("productType", "Attempted to pass null productType to IsQueueTooLong");
            }
        }

        /// <summary>
        /// Gets the length of the Queue for the given productType
        /// </summary>
        /// <param name="productType">The product type of the Queue</param>
        /// <returns>The number off calls in the queue for the given ProductType if singleQueueLength is false, if it is true then returns the total number of calls in all queues</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the given ProductType does not match a Queue</exception>
        /// <exception cref="System.ArgumentNull">Thrown when productType is null</exception>
        public int GetQueueLength(ProductType productType)
        {
            //If the given product type is null
            if (productType != null)
            {
                //If singleQueueLength is not set
                if (!singleQueueLength)
                {
                    //Get the queue with the matched product type
                    Queue matchedQueue = queues.Find(q => q.TypeInQueue == productType);

                    //If the matched queue is not null
                    if (matchedQueue != null)
                    {
                        return matchedQueue.Calls.Count;
                    }
                    else // The given product type doesn't match any queue
                    {
                        throw new ArgumentOutOfRangeException("productType", "Passed in a ProductType that has no matching Queue");
                    }
                }
                else // singleQueueLength is true
                {
                    //Return the sum of the counts of all calls in all queues
                    return queues.Sum(q => q.Calls.Count);
                }

            }
            else // The productType was null
            {
                throw new ArgumentNullException("productType", "Attempted to pass null productType to GetQueueLength");
            }
        }

        /// <summary>
        /// Adds a call to its appropriate Queue
        /// </summary>
        /// <param name="entity">The call to add</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the given Call's ProductType does not match a Queue</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the given Call is null</exception>
        public void AddToQueue(Call entity)
        {
            //If the given entity is null
            if (entity != null)
            {
                //Get the queue with the matched product type
                Queue matchedQueue = queues.Find(q => q.TypeInQueue == entity.ProductType);

                //If the matched queue is not null
                if (matchedQueue != null)
                {
                    matchedQueue.Enqueue(entity);
                }
                else // The given product type doesn't match any queue
                {
                    throw new ArgumentOutOfRangeException("entity", "Passed in a call with a ProductType that has no matching Queue");
                }

            }
            else // The entity was null
            {
                throw new ArgumentNullException("entity", "Attempted to pass null Call to AddToQueue");
            }
        }

        /// <summary>
        /// Gets a Call from a Queue for a Representative of the given type
        /// </summary>
        /// <param name="repType">The SalesRepType that the call is being retrieved for</param>
        /// <returns>A Call from a queue, or null if there are no calls in the queue</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when the given ProductType cannot handle any of the ProductTypes that have Queues</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the given SalesRepType is null</exception>
        public Call GetCallForRepType(SalesRepType repType)
        {
            //If the given repType is null
            if (repType != null)
            {
                //Get a list of queues that have types that match the SalesRepTypes Handleable ProductTypes
                var matchedQueues = from q in queues
                                    where repType.Handles.Contains(q.TypeInQueue)
                                    select q;

                //If at least one queue matched one of the reptypes Handleable ProductTypes
                if (matchedQueues.Count() > 0)
                {

                    //Get the list from the matchedQueues that is the longest
                    Queue longest = matchedQueues.Aggregate((q1, q2) => (q1.Calls.Count > q2.Calls.Count) ? q1 : q2);

                    //Return the popped call from the longest queue
                    return longest.Dequeue();
                }
                else // No Queues matched
                {
                    throw new ArgumentOutOfRangeException("repType", "Attempted to pass a repType couldn't handle any of the Queues productTypes");
                }
            }
            else // The repType was null
            {
                throw new ArgumentNullException("repType", "Attempted to pass null SalesRepType to GetCallForRepType");
            }
        }

        /// <summary>
        /// Draws the Queue for a ProductType to a DataGridView
        /// </summary>
        /// <param name="queueType">The ProductType of the Queue to draw</param>
        /// <param name="dgv">The DataGridView that the Queue shall draw to</param>
        /// <exception cref="System.ArgumentNullException">Thrown when either of the given arguments are null</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown when there is no Queue for the given ProductType</exception>
        public void DrawQueueToDataGridView(ProductType queueType, DataGridView dgv)
        {
            // Check that the given ProductType is not null
            if (queueType != null)
            {
                // Check that the given DataGridView is not null
                if (dgv != null)
                {
                    // Attempt to retrieve the queue for this ProductType
                    Queue matched = queues.SingleOrDefault(q => q.TypeInQueue == queueType);

                    // If a matching queue was successfully retrieved
                    if (matched != null)
                    {
                        // Get the queue to draw itself to the DataGridView
                        matched.ToDataGridView(dgv);
                    }
                    else // No matching Queue could be found
                    {
                        throw new ArgumentOutOfRangeException("queueType", "Attempted to pass a ProductType that has no matching Queue to QueueManager.DrawQueueToDataGridView");
                    }
                }
                else // The given DataGridView is null
                {
                    throw new ArgumentNullException("dgv", "Attempted to pass null DataGridView to QueueManager.DrawQueueToDataGridView");
                }
            }
            else // The given ProductType is null
            {
                throw new ArgumentNullException("queueType", "Attempted to pass null ProductType to QueueManager.DrawQueueToDataGridView");
            }
        }
    }
}
