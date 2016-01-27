using SolvencyII.Data.CRT.ETL.DataConnectors;
using SolvencyII.Data.CRT.ETL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.DBcontrollers.Extraction
{
    /// <summary>
    /// Tabel names extractor
    /// </summary>
    public class TableNamesExtractor : ITableNamesExtractor
    {
        IDataConnector _dataConnector;

        /// <summary>
        /// Initializes a new instance of the <see cref="TableNamesExtractor"/> class.
        /// </summary>
        /// <param name="dataConnector">The data connector.</param>
        public TableNamesExtractor(IDataConnector dataConnector)
        {
            _dataConnector = dataConnector;
        }

        /// <summary>
        /// Gets the table names.
        /// </summary>
        /// <param name="potentialTableIds">The potential table ids.</param>
        /// <returns></returns>
        public string[] getTableNames(IEnumerable<int> potentialTableIds)
        {
            string query = @"select t.TableCode ||'__' || tax.TaxonomyCode ||'__' || tax.Version as name
from mTable t 
    inner join mTaxonomyTable tt on t.TableID = tt.TableID
    inner join mTaxonomy tax on tax.TaxonomyID = tt.TaxonomyID
where t.TableID in ({0}) 
 and ifnull(tax.FromDate, '3000-12-31') = (select ifnull(max(et.FromDate), '3000-12-31')
                      from mTaxonomyTable ett 
                            inner join mTaxonomy et on ett.TaxonomyID = et.TaxonomyID
                      where ett.TableID = t.TableID);";

            StringBuilder condition = new StringBuilder(); bool first = true;
            foreach (int id in potentialTableIds)
            {
                if (!first) condition.Append(", ");
                condition.Append(id);
                if (first) first = false;
            }

            query = string.Format(query, condition.ToString());
            DataTable dt = _dataConnector.executeQuery(query);
            string[] tabNames = new string[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
                tabNames[i] = dt.Rows[i]["name"].ToString().Replace(".", "_").Replace(" ", "_").Replace("-", "_");

            return tabNames;
        }


        /// <summary>
        /// Gets the tables codes.
        /// </summary>
        /// <param name="tableIDs">The table i ds.</param>
        /// <returns></returns>
        public Dictionary<int, string> getTablesCodes(int[] tableIDs)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();

            if (tableIDs == null || tableIDs.Count() == 0)
                return result;

            IDbCommand comm = getTablesCodesQuery(tableIDs);
            DataTable dt = _dataConnector.executeQuery(comm);

            foreach (DataRow dr in dt.Rows)
                result.Add(int.Parse(dr["TableID"].ToString()), dr["TableCode"].ToString());

            return result;
        }

        /// <summary>
        /// Gets the tables codes query.
        /// </summary>
        /// <param name="tableIDs">The table i ds.</param>
        /// <returns></returns>
        private IDbCommand getTablesCodesQuery(int[] tableIDs)
        {
            StringBuilder sb = new StringBuilder(string.Format(@"select t.TableID, t.TableCode
from mTable t
    where t.TableID = @TableID0"));

            for (int i = 1; i < tableIDs.Count(); i++)
                sb.Append(" or t.TableID = @TableID").Append(i);

            IDbCommand comm = _dataConnector.createCommand();
            comm.CommandText = sb.ToString();

            IDbDataParameter param;
            for (int i = 0; i < tableIDs.Count(); i++)
            {
                param = comm.CreateParameter();
                param.ParameterName = string.Format("@TableID{0}", i);
                param.Value = tableIDs[i];
                comm.Parameters.Add(param);
            }
            return comm;
        }
    }
}
