using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiscreteEventSimulator
{
    /// <summary>
    /// Creates Calls to be consumed by other classes
    /// </summary>
    public class CallFactory
    {
        //-------------------------------------------------------------------
        //- FIELDS                                                          -
        //-------------------------------------------------------------------
        private uint lastId;

        //-------------------------------------------------------------------
        //- PROPERTIES                                                      -
        //-------------------------------------------------------------------

        /// <summary>
        /// Gets the lastID value that was given to a call
        /// </summary>
        public uint LastId
        {
            get { return lastId; }
        }

        //-------------------------------------------------------------------
        //- METHODS                                                         -
        //-------------------------------------------------------------------
        /// <summary>
        /// Creates a new instance of the CallFactory class and a new set of ID's
        /// </summary>
        public CallFactory()
        {
            lastId = 0;
        }

        /// <summary>
        /// Creates and returns a new Call with an auto-incrementing ID
        /// </summary>
        /// <returns></returns>
        public Call CreateCall()
        {
            Call call = new Call(lastId);
            lastId++;
            return call;
        }
    }
}
