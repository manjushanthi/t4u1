using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL
{
    /// <summary>
    /// Cleans error messages
    /// </summary>
    class dMessageCleaner
    {
        private DataConnectors.IDataConnector dataConnector;

        /// <summary>
        /// Initializes a new instance of the <see cref="dMessageCleaner"/> class.
        /// </summary>
        /// <param name="dataConnector">The data connector.</param>
        public dMessageCleaner(DataConnectors.IDataConnector dataConnector)
        {            
            this.dataConnector = dataConnector;
        }

        /// <summary>
        /// Cleans the dmessage.
        /// </summary>
        /// <param name="instanceID">The instance identifier.</param>
        internal void CleanDmessage(int instanceID)
        {
            this.dataConnector.executeNonQuery(string.Format("delete from dMessage where InstanceID = {0} and MessageCode in ('DP Duplication', 'CrtError', 'EtlError')", instanceID));
        }
    }
}
