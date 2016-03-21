using SolvencyII.Data.CRT.ETL.DataConnectors;
using SolvencyII.Data.CRT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.MappingControllers.DataPoints
{
    class DataPointsSerializer
    {
        private IDataConnector dataConnector;
        Dictionary<string, int> trcToCellId = null;

        string[] ddls = new string[]{@"CREATE TABLE mDataPoint (DataPointId INTEGER PRIMARY KEY, DimensionId INTEGER REFERENCES mDimension (DimensionID), MemberId INTEGER REFERENCES mMember (MemberID), ParentDataPointId INTEGER, DPCode text);"
            //, @"CREATE TABLE mLocation (LocationId INTEGER PRIMARY KEY, TRCode TEXT);" 
            ,@"CREATE TABLE mDataPointTableCell (DataPointId INTEGER REFERENCES mDataPoint (DataPointId), CellId INTEGER REFERENCES mTableCell (CellID), PRIMARY KEY (DataPointId, CellId));"
            //,@"CREATE TABLE mDataPointLocation (DataPointId INTEGER REFERENCES mDataPoint (DataPointId), LocationId INTEGER REFERENCES mLocation (LocationId), PRIMARY KEY (DataPointId, LocationId));"
        };

        string dataPointInsert = @"insert into mDataPoint (DataPointId,
DimensionId,
MemberId,
ParentDataPointId, DPCode) values
(@DataPointId,
@DimensionId,
@MemberId,
@ParentDataPointId, @DPCode);";
        //string locationInsert = @"insert into mLocation (LocationId, TRCode) values (@LocationId, @TRCode)";
        string dataPointLocationInsert = @"insert into mDataPointTableCell (DataPointId, CellID) values (@DataPointId, @LocationId);";

        int lastDpId = 0;
        int lastLocId = 0;

        public DataPointsSerializer(IDataConnector dataConnector)
        {
            this.dataConnector = dataConnector;
        }

        internal void SerializeDataPoints(Dictionary<DataPoint, DataPoint> dictionary)
        {
            CreateTables();

            if (this.trcToCellId == null)
                populateTrcToCellDictionary();

            int i = 0;
            int total = dictionary.Count();
            foreach (var kvp in dictionary)
            {
                SerializeDataPoint(kvp.Key);
                if(++i%10000==0)
                    ProgressHandler.EtlProgress(i, total, " loaded");
            }
                
        }

        private void CreateTables()
        {
            foreach (var ddl in ddls)
            {
                try
                {
                    dataConnector.executeNonQuery(ddl);
                }
                catch (Exception)
                {
                    
                }
            }
        }

        private void SerializeDataPoint(DataPoint dataPoint)
        {
            int dpid = ++lastDpId;
            dataPoint.DbId = dpid;
            IDbCommand comm = dataConnector.createCommand();
            comm.CommandText = dataPointInsert;
            addParameter(comm, "DataPointId", dpid);
            addParameter(comm, "DimensionId", dataPoint.dimMemId.dimId);
            addParameter(comm, "MemberId", dataPoint.dimMemId.memId);
            addParameter(comm, "DPCode", dataPoint.DPCode);

            if (dataPoint.parent != null) 
                addParameter(comm, "ParentDataPointId", dataPoint.parent.DbId);
            else
                addParameter(comm, "ParentDataPointId", null);

            this.dataConnector.executeNonQuery(comm);

            foreach (var location in dataPoint.ColumnMappings)
                SerializeLocation(location, dataPoint.DbId);

            foreach (var child in dataPoint.children)
                SerializeDataPoint(child.Key);
        }

        private void SerializeLocation(ColumnMapping location, int dpId)
        {
            string trc = new StringBuilder().Append(location.DYN_TABLE_NAME).Append("|").Append(location.DYN_TAB_COLUMN_NAME).ToString();
            int locDbId = 0;
            
            if (!this.trcToCellId.TryGetValue(trc, out locDbId))
            {
                //locDbId = ++this.lastLocId;
                //trcToCellId.Add(trc, locDbId);
                //insertLocation(trc, locDbId);
                ProgressHandler.EtlError(new EtlException(trc + " cell for data point "+dpId+" not found "));
                return;
            }
            insertDataPointLocation(dpId, locDbId);
        }

        string trcQuery = @"select distinct tc.CellID, 
    replace(TableCode , '.','_')
    || '__' || TaxonomyCode || '__' || 
    replace(Version , '.','_') as DynTableName
    , qx.OrdinateCode Ccode
    , qy.OrdinateCode Rcode
    , qx.IsOpenAxis xopen
    , qy.IsOpenAxis yopen
from mTable t 
inner join mTaxonomyTable tt on tt.TableID = t.TableID 
inner join mTaxonomy tax on tax.TaxonomyID = tt.TaxonomyID
inner join mTableCell tc on tc.TableID = t.TableID
left outer join (select * from mCellPosition cpx 
                inner join mAxisOrdinate aox on aox.OrdinateID = cpx.OrdinateID and (aox.IsAbstractHeader = 0 or aox.IsAbstractHeader is null)
                inner join mAxis ax on ax.AxisID = aox.AxisID and ax.AxisOrientation = 'X' 
                and (ax.IsOpenAxis <> 1)
                ) qx 
    on qx.CellID = tc.CellID
left outer join (select * from mCellPosition cpy 
                inner join mAxisOrdinate aoy on aoy.OrdinateID = cpy.OrdinateID and (aoy.IsAbstractHeader = 0 or aoy.IsAbstractHeader is null)
                inner join mAxis ay on ay.AxisID = aoy.AxisID and ay.AxisOrientation = 'Y'
                and (ay.IsOpenAxis <> 1)
                ) qy 
    on qy.CellID = tc.CellID
left outer join mTableAxis ta on (ta.AxisID = qx.AxisID or ta.AxisID = qy.AxisID) and ta.TableID = t.TableID
order by 1 asc";

        private void populateTrcToCellDictionary()
        {
            this.trcToCellId = new Dictionary<string, int>();
            IDbCommand comm = this.dataConnector.createCommand();
            comm.CommandText = trcQuery;
            DataTable dt = this.dataConnector.executeQuery(comm);
            int dbId = 0;
            string tabCode = "";
            string rcode = "";
            string ccode = "";
            string trc = "";
            foreach (DataRow dr in dt.Rows)
            {
                dbId = int.Parse(dr["CellID"].ToString());
                tabCode = dr["DynTableName"].ToString();
                rcode = dr["Rcode"].ToString();
                ccode = dr["Ccode"].ToString();
                trc = new StringBuilder().Append(tabCode).Append("|").Append(dynColName(rcode, ccode)).ToString();

                if (!this.trcToCellId.ContainsKey(trc))
                    this.trcToCellId.Add(trc, dbId);
            }
        }

        private string dynColName(string rcode, string ccode)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(rcode))
                sb.Append(rcode);
            if (!string.IsNullOrWhiteSpace(ccode))
                sb.Append(ccode);
            return sb.ToString();
        }

        private void insertDataPointLocation(int dpId, int locDbId)
        {
            IDbCommand comm = dataConnector.createCommand();
            comm.CommandText = this.dataPointLocationInsert;
            addParameter(comm, "LocationId", locDbId);
            addParameter(comm, "DataPointId", dpId);
            this.dataConnector.executeNonQuery(comm);
        }

        //private void insertLocation(string trc, int locDbId)
        //{
        //    IDbCommand comm = dataConnector.createCommand();
        //    comm.CommandText = locationInsert;
        //    addParameter(comm, "LocationId", locDbId);
        //    addParameter(comm, "TRCode", trc);
        //    this.dataConnector.executeNonQuery(comm);
        //}

        private void addParameter(IDbCommand comm, string name, object value)
        {
            name = name.StartsWith("@") ? name : "@" + name;
            IDbDataParameter param = comm.CreateParameter();
            param.ParameterName = name;
            param.Value = value;
            comm.Parameters.Add(param);
        }
    }
}
