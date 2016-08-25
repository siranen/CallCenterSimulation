using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DiscreteEventSimulator
{
    /// <summary>
    /// Defines a First In, First Out Queue of calls, restricted to a type
    /// </summary>
    public class Queue
    {
        //-------------------------------------------------------------------
        //- FIELDS                                                          -
        //-------------------------------------------------------------------
        private Queue<Call> calls;
        private ProductType typeInQueue;

        //-------------------------------------------------------------------
        //- PROPERTIES                                                      -
        //-------------------------------------------------------------------
        /// <summary>
        /// Gets the list of calls for this queue
        /// </summary>
        public Queue<Call> Calls
        {
            get { return calls; }
        }

        /// <summary>
        /// Gets the ProductType of the calls in this Queue, if null then this Queue will hold anything
        /// </summary>
        public ProductType TypeInQueue
        {
            get { return typeInQueue; }
        }

        //-------------------------------------------------------------------
        //- METHODS                                                         -
        //-------------------------------------------------------------------
        /// <summary>
        /// Creates a new instance of the Queue class, with the given ProductType
        /// </summary>
        /// <param name="typeInQueue">The ProductType of the Calls this Queue will hold, can be null</param>
        public Queue(ProductType typeInQueue)
        {
            this.typeInQueue = typeInQueue;
            this.calls = new Queue<Call>();
        }

        /// <summary>
        /// Returns the oldest Call in the queue
        /// </summary>
        /// <returns>The oldest Call</returns>
        public Call Peek()
        {
            return calls.Peek();
        }

        /// <summary>
        /// Returns the oldest call in the queue and removes it from the queue
        /// </summary>
        /// <returns>The oldest call</returns>
        public Call Dequeue()
        {
            //If there is a call in the queue
            if (calls.Count > 0)
            {
                //Dequeue the oldest call from the internal queue
                return calls.Dequeue();
            }
            else // Return null if no calls in queue
            {
                return null;
            }            
        }

        /// <summary>
        /// Adds a call to the front of the Queue. If the Call is of the correct ProductType
        /// </summary>
        /// <param name="call">The new call</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the given call is null</exception>
        /// <exception cref="System.ArgumentException">Thrown when the given Call's ProductType does not match the Queue's</exception>
        public void Enqueue(Call call)
        {
            //If the given call not null
            if (call != null)
            {
                //If the ProductType of the call matches the Queues typeInQueue or the typeInQueue is null
                if (call.ProductType == typeInQueue || typeInQueue == null)
                {
                    //Insert the given call at the first slot in the queue
                    calls.Enqueue(call);
                }
                else // The given calls ProductType matched the queues accepted ProductType
                {
                    throw new ArgumentOutOfRangeException("Attempted to add a Call of the incorrect ProductType to the Queue", "call");
                }
            }
            else // The given call was null
            {
                throw new ArgumentNullException("call", "Attempted to pass null Call to Queue.Enqeue()");
            }
        }

        /// <summary>
        /// Draws the Queue to the given DataGridView
        /// </summary>
        /// <param name="dgv">The DataGridView to alter</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the given DataGridView is null</exception>
        public void ToDataGridView(DataGridView dgv)
        {
            // Check that the given DataGridView is not null
            if (dgv != null)
            {
                // loop foreach Call in the queue
                foreach (Call call in calls)
                {
                    // Create the data to go in the row
                    string entityID = call.CallId.ToString();
                    string eventName = "Call Waiting";
                    string eventTime = "--";
                    // Create the next three fields
                    string productType = call.ProductType.TypeName;
                    string startTime = call.StartTime.ToLongTimeString();
                    string beginWait = call.BeginWait.ToLongTimeString();

                    // create the object array used to make the row
                    object[] rowVals = { entityID, eventName, eventTime, productType, startTime, beginWait };

                    //Create a grid row
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dgv, rowVals);

                    //Add the new row to the datagridview
                    dgv.Rows.Add(row);
                } // end call foreach
            }
            else // The given DataGridView is null
            {
                throw new ArgumentNullException("dgv", "Attempted to pass null DataGridView to Queue.ToDataGridView");
            }
        }
    }
}
