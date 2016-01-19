using SolvencyII.Data.CRT.ETL.DataConnectors;
using SolvencyII.Data.CRT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.DBcontrollers
{
    /// <summary>
    /// Marks facts that were not mapped in internal ETL
    /// </summary>
    public class NotMappedFactsMarker
    {
        IDataConnector dataConnector;
        bool? isColumn = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotMappedFactsMarker"/> class.
        /// </summary>
        /// <param name="dataConnector">The data connector.</param>
        public NotMappedFactsMarker(IDataConnector dataConnector)
        {
            this.dataConnector = dataConnector;
        }

        /// <summary>
        /// Marks the facts.
        /// </summary>
        /// <param name="factIDs">The fact i ds.</param>
        public void MarkFacts(HashSet<int> factIDs)
        {
            checkWasNotMappedColumn();

            HashSet<int> queryFactIds = new HashSet<int>();
            foreach (int factId in factIDs)
            {
                queryFactIds.Add(factId);
                if(queryFactIds.Count()%999 == 0)
                {
                    markAsNotMapped(queryFactIds);
                    queryFactIds = new HashSet<int>();
                }
            }

            if (queryFactIds.Count() > 0)
                markAsNotMapped(queryFactIds);

            queryFactIds.Clear();
            queryFactIds = null;
        }

        /// <summary>
        /// Marks as not mapped.
        /// </summary>
        /// <param name="queryFactIds">The query fact ids.</param>
        private void markAsNotMapped(HashSet<int> queryFactIds)
        {
            StringBuilder sb = new StringBuilder("update dFact set WasNotMapped = 1 where FactID in ( ");
            bool first = true;
            foreach (int factId in queryFactIds)
            {
                if (first) first = false;
                else sb.Append(" , ");

                sb.Append(factId);
            }

            string query = sb.Append(" )").ToString();
            this.dataConnector.executeNonQuery(query);
        }

        /// <summary>
        /// Checks the was not mapped column.
        /// </summary>
        private void checkWasNotMappedColumn()
        {
            if(isColumn == null)
            {
                foreach (DataRow dr in this.dataConnector.executeQuery("PRAGMA table_info(dFact)").Rows)
                    if (dr["name"].ToString().Equals("WasNotMapped"))
                        isColumn = true;
                if(isColumn == null) isColumn = false;
            }

            if (isColumn == false) this.dataConnector.executeNonQuery("alter table dFact add column WasNotMapped BOOLEAN");
        }


        /// <summary>
        /// Inserts the dmessage.
        /// </summary>
        /// <param name="instanceID">The instance identifier.</param>
        internal void InsertDmessage(int instanceID)
        {
            this.dataConnector.executeNonQuery(string.Format(@"insert into dMessage (InstanceID, SequenceInReport, MessageCode, MessageLevel, Value)
select f.InstanceID, f.FactID, 'EtlError', 'error', 'Fact ' || ifnull(f.DataPointSignature,'') || ' definition was not expected and it hasn’t been processed. Fact value is '||
ifnull(f.NumericValue, ifnull(f.DateTimeValue, ifnull(f.TextValue, ifnull(f.BooleanValue, 'null'))))
from dFact f where f.InstanceID = {0} and f.WasNotMapped = 1", instanceID));

            this.dataConnector.executeNonQuery(string.Format(@"insert into dMessageReference (MessageID, DataPointSignature)
select m.MessageID, f.DataPointSignature
from dFact f
    inner join dMessage m on m.InstanceID = f.InstanceID and m.MessageCode = 'EtlError' and m.SequenceInReport = f.FactID
where f.WasNotMapped = 1 and f.InstanceID = {0}", instanceID));
        }
    }
}
