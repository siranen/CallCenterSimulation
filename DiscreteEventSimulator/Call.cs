using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscreteEventSimulator
{
    /// <summary>
    /// Defines a call in the simulation
    /// </summary>
    public class Call
    {
        //-------------------------------------------------------------------
        //- FIELDS                                                          -
        //-------------------------------------------------------------------
        private ProductType productType;
        private uint callId;
        private DateTime startTime;
        private DateTime beginWait;
        private DateTime finishTime;
        private DateTime beganProcessing;
        private SalesRepresentative processedBy;

        //-------------------------------------------------------------------
        //- PROPERTIES                                                      -
        //-------------------------------------------------------------------
        /// <summary>
        /// Gets/Sets the type of product this call is for
        /// </summary>
        public ProductType ProductType
        {
            get { return productType; }
            set { productType = value; }
        }

        /// <summary>
        /// Gets the ID of this call
        /// </summary>
        public uint CallId
        {
            get { return callId; }
        }

        /// <summary>
        /// Gets/Sets the time in the simulation that this call entered the system
        /// </summary>
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        /// <summary>
        /// Gets/Sets the time in the simulation that this call began waiting in a queue
        /// </summary>
        public DateTime BeginWait
        {
            get { return beginWait; }
            set { beginWait = value; }
        }

        /// <summary>
        /// Gets/Sets the time in the simulation that this call was finished
        /// </summary>
        public DateTime FinishTime
        {
            get { return finishTime; }
            set { finishTime = value; }
        }

        /// <summary>
        /// Gets/Sets the time that the Call began being processed
        /// </summary>
        public DateTime BeganProcessing
        {
            get { return beganProcessing; }
            set { beganProcessing = value; }
        }

        /// <summary>
        /// Gets/Sets the SalesRepresentative that processed this call
        /// </summary>
        public SalesRepresentative ProcessedBy
        {
            get { return processedBy; }
            set { processedBy = value; }
        }

        //-------------------------------------------------------------------
        //- METHODS                                                         -
        //-------------------------------------------------------------------
        /// <summary>
        /// Creates a new instance of the Call class that is essentially blank
        /// </summary>
        /// <param name="callId">The Id of the call in the system</param>
        public Call(uint callId)
        {
            this.callId = callId;
        }

        /// <summary>
        /// Returns a System.String that represents the Call
        /// </summary>
        /// <returns>A string</returns>
        public override string ToString()
        {
            //Create the stringbuilder
            StringBuilder sb = new StringBuilder();

            sb.Append("Call ID: " + callId.ToString());
            sb.Append(", ");

            sb.Append("StartTime: ");
            //if the call starttime is set
            if (startTime != null)
            {
                //Add the time to the StringBuilder
                sb.Append(startTime.ToShortTimeString());
            }
            else //Start time was not set
            {
                sb.Append("Not Set");
            }

            sb.Append(", ");
            sb.Append("BeginWaitTime: ");
            //If the beginWaitTime is set
            if (beginWait != null)
            {
                sb.Append(beginWait.ToShortTimeString());
            }
            else //BeginWait was not set
            {
                sb.Append("Not Set");
            }

            sb.Append(", ");
            sb.Append("FinishTime: ");
            //if the finishTime is set
            if (finishTime != null)
            {
                sb.Append(finishTime.ToShortTimeString());
            }
            else //FinishTime was not set
            {
                sb.Append("Not Set");
            }

            sb.Append(", ");
            sb.Append("Product Type: ");
            //If the product type has been set
            if (productType != null)
            {
                sb.Append(productType.ToString());
            }
            else //The product type was not set
            {
                sb.Append("Not Set");
            }

            //Return the built up string
            return sb.ToString();
        }
    }
}
