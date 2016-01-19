using SolvencyII.Data.CRT.ETL.DataConnectors;
using SolvencyII.Data.CRT.ETL.MappingControllers;
using SolvencyII.Data.CRT.ETL.Model;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL
{
    /// <summary>
    /// SQL mapping rovider that filters them by large dimensions members of instance
    /// </summary>
    public class FilteredSQLiteMappingProvider : SQLiteMappingProvider
    {
        private int instanceID;

        Dictionary<string, HashSet<string>> dimToMem = null;

        string bigDimQuery = @"select d.DimensionXBRLCode, m.MemberXBRLCode
from dInstanceLargeDimensionMember ild
    inner join mDimension d on d.DimensionID = ild.DimensionID
    inner join mMember m on m.MemberID = ild.MemberID
where ild.InstanceID = @InstanceID";

        /// <summary>
        /// Initializes a new instance of the <see cref="FilteredSQLiteMappingProvider"/> class.
        /// </summary>
        /// <param name="InstanceId">The instance identifier.</param>
        /// <param name="_dataConnector">The _data connector.</param>
        /// <param name="mappingAnalyzer">The mapping analyzer.</param>
        public FilteredSQLiteMappingProvider(int InstanceId, IDataConnector _dataConnector, IMappingAnalyzer mappingAnalyzer) 
            : base(_dataConnector, mappingAnalyzer)
        {
            this.instanceID = InstanceId;
        }

        /// <summary>
        /// Gets all mapping hash set.
        /// </summary>
        /// <param name="mTableIds">The m table ids.</param>
        public override void getAllMappingHashSet(List<string> mTableIds)
        {
            if(dimToMem == null) populateDims();

            List<CrtMapping> mappings = new List<CrtMapping>();
            HashSet<string> members;
            string dimCode;
            foreach (CrtMapping map in getMappings(mTableIds.ToArray()))
            {
                if (string.IsNullOrEmpty(map.DIM_CODE))
                {
                    mappings.Add(map);
                    continue;
                }
                dimCode = map.DIM_CODE.Substring(0, map.DIM_CODE.IndexOf("("));
                if (dimToMem.TryGetValue(dimCode, out members))
                {
                    if (members.Contains(map.MEM_CODE))
                    {
                        mappings.Add(map);
                        continue;
                    }                        
                    continue;
                }
                else mappings.Add(map);
            }

            base._allMappingsHashSet = new HashSet<CrtMapping>(mappings);
            _mappingAnalyzer.SetMappingsSet(_allMappingsHashSet);
            mappings = null;            
        }

        /// <summary>
        /// Populates the dims.
        /// </summary>
        private void populateDims()
        {
            IDbCommand comm = _dataConnector.createCommand();
            comm.CommandText = this.bigDimQuery;
            IDbDataParameter param = comm.CreateParameter();
            param.ParameterName = "@InstanceID";
            param.Value = this.instanceID;
            comm.Parameters.Add(param);

            dimToMem = new Dictionary<string,HashSet<string>>();
            DataTable dt = _dataConnector.executeQuery(comm);
            foreach (DataRow dr in dt.Rows)
            {
                string dimCode = dr["DimensionXBRLCode"].ToString();
                string memCode = dr["MemberXBRLCode"].ToString();

                if (dimToMem.ContainsKey(dimCode))
                    dimToMem[dimCode].Add(memCode);
                else
                    dimToMem.Add(dimCode, new HashSet<string> { memCode });                
            }
        }
    }
}
