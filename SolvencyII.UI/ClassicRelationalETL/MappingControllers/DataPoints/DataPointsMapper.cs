using SolvencyII.Data.CRT.ETL.DataConnectors;
using SolvencyII.Data.CRT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.MappingControllers.DataPoints
{
    public class DataPointsMapper
    {
        DataPointMappingAnalyzer dpAnalyzer = new DataPointMappingAnalyzer();
        Dictionary<DataPoint, DataPoint> rootDataPoints = new Dictionary<DataPoint, DataPoint>();
        Dictionary<string, DimMemIds> dimToId = null;        
        private IMappingProvider mapPrivider;
        string signaturesQuery = @"select distinct d.DimensionXBRLCode || '(' || case when m.memberId = 9999 or m.MemberID is null then '*' else m.MemberXBRLCode end || ')' as Signature
    , d.DimensionID
    , case when m.memberId = 9999 or m.MemberID is null then 9999 else m.MemberID end as MemberID
from mDimension d
    left outer join mMember m on m.DomainID = d.DomainID
    where d.DimensionCode not like '%MET_HD%'
    order by 1";
        private IDataConnector conn;

        private Dictionary<string, DimMemIds> dimMemToIds = null;

        public DataPointsMapper(IMappingProvider mapProvider, IDataConnector conn)
        {
            this.mapPrivider = mapProvider;
            this.conn = conn;
        }

        public void MappAllDataPoints(IEnumerable<string> tableCodes)
        {
            HashSet<CrtMapping> mappings;
            List<DimMemIds> dimToMembers;
            rootDataPoints = new Dictionary<DataPoint, DataPoint>();
            DataPoint current = null;
            int i = 0;
            foreach (string tabCode in tableCodes)
            {

                if (tabCode.Contains("S.19") || tabCode.Contains("S_19") 
                    //|| !tabCode.Contains("S_14_01_01_01")
                    )
                    continue;

                dpAnalyzer = new DataPointMappingAnalyzer();
                mappings = mapPrivider.getMappings(new string[] { tabCode });
                try
                {
                    dpAnalyzer.SetMappingsSet(mappings);
                
                    foreach (var dp in dpAnalyzer.DataPointToRowMapings)
                    {
                        dimToMembers = getDimsToMembers(dp.Key);
                        current = null;
                        bool first = true;
                        foreach (var dimToMem in dimToMembers)
                        {
                            if (first)
                            {
                                current = LookupOrCreateDataPoint(dimToMem, rootDataPoints);
                                first = false;
                            }
                            else
                                current = LookupOrCreateDataPoint(dimToMem, current.children, current);
                        }

                        current.ColumnMappings.AddRange(mapColumnMapping(dp.Value));
                        current.DPCode = dp.Key;
                    }
                    ProgressHandler.EtlProgress(tableCodes.ToList().IndexOf(tabCode), tableCodes.Count(), dpAnalyzer.DataPointToRowMapings.Count() + " in " + tabCode);                        
                    dpAnalyzer.CleanMappings();
                }
                catch (Exception ex)
                {
                    ProgressHandler.EtlError(new Exception("Could not map data points for " + tabCode, ex));
                }
                
                dpAnalyzer = null;
                if(++i%10 == 0)
                    GC.Collect();                
            }
        }

        private List<ColumnMapping> mapColumnMapping(List<ColumnMapping> list)
        {
            List<ColumnMapping> result = new List<ColumnMapping>();
            list.ForEach(x =>
                {
                    result.Add(new ColumnMapping(x.DYN_TABLE_NAME, x.DYN_TAB_COLUMN_NAME));
                });
            return result;
        }

        private DataPoint LookupOrCreateDataPoint(DimMemIds dimToMem, Dictionary<DataPoint, DataPoint> toSelfDp, DataPoint parent = null)
        {
            DataPoint dp = new DataPoint(dimToMem, parent);
            DataPoint lookedUpDp = null;
            if (toSelfDp.TryGetValue(dp, out lookedUpDp))
                dp = lookedUpDp;
            else
                toSelfDp.Add(dp, dp);

            return dp;
        }

        private List<DimMemIds> getDimsToMembers(string dpSigature)
        {   
            List<DimMemIds> result = new List<DimMemIds>();
            if(dimMemToIds == null) populateDimMemToIDs();

            List<string> dimSignatures = dpSigature.Split(new char[] { ')' }, StringSplitOptions.RemoveEmptyEntries).Select(x=>string.Format("{0})",x)).ToList();
            foreach (string dimSign in dimSignatures)            
                result.Add(this.dimMemToIds[dimSign]);

            return result;
        }

        
        private void populateDimMemToIDs()
        {            
            this.dimMemToIds = new Dictionary<string, DimMemIds>();
            DataTable dt = this.conn.executeQuery(this.signaturesQuery);
            string dim;
            foreach (DataRow dr in dt.Rows)
            {
                dim = dr["Signature"].ToString();
                dimMemToIds.Add(dim, new DimMemIds(int.Parse(dr["DimensionID"].ToString()), int.Parse(dr["MemberID"].ToString()), dim));
            }
        }

        public void SerializeDatapoints()
        {
            DataPointsSerializer dps = new DataPointsSerializer(this.conn);
            dps.SerializeDataPoints(this.rootDataPoints);
        }
    }

    public class DataPoint
    {
        public int DbId;
        public readonly DimMemIds dimMemId;
        public readonly DataPoint parent;
        public Dictionary<DataPoint, DataPoint> children = new Dictionary<DataPoint, DataPoint>();
        public List<ColumnMapping> ColumnMappings = new List<ColumnMapping>();
        public string DPCode = null;

        public DataPoint(DimMemIds dimMemIds, DataPoint parent)
        {
            this.dimMemId = dimMemIds;
            this.parent = parent;
        }

        public DataPoint AddChild(DataPoint dp)
        {
            DataPoint lookedUpDp = null;
            if (!children.TryGetValue(dp, out lookedUpDp))
                return lookedUpDp;
            
            dp = lookedUpDp;
            children.Add(dp, dp);
            return dp;
        }

        int hashCode = -1;
        public override int GetHashCode()
        {
            if (hashCode != -1) return hashCode;
 	        return (dimMemId.GetHashCode().ToString() + (parent == null ? "" : parent.GetHashCode().ToString())).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DataPoint))
                return false;
            DataPoint dp = obj as DataPoint;
            if(dp.dimMemId.Equals(this.dimMemId))
                return true;

            return false;
        }

        public override string ToString()
        {
            if(dimMemId != null && !string.IsNullOrEmpty( dimMemId.dimCode ))
                return dimMemId.dimCode;

            return base.ToString();
        }
    }

    public class DimMemIds
    {
        public readonly string dimCode;
        public readonly int dimId;
        public readonly int memId;

        public DimMemIds(int dimId, int memId, string dimCode)
        {
            this.dimId = dimId;
            this.memId = memId;
            this.dimCode = dimCode;
        }

        private int hashCode = -1;
        public override int GetHashCode()
        {
            if (hashCode != -1) return hashCode;
            
            return (dimId.GetHashCode().ToString() + memId.GetHashCode().ToString()).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DimMemIds))
                return false;
            DimMemIds dp = obj as DimMemIds;
            if (dp.dimCode.Equals(this.dimCode))
                return true;

            return false;
        }
    }
}
