using SolvencyII.Data.CRT.ETL.DataConnectors;
using SolvencyII.Data.CRT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.Repositories
{
    public class TableDimensionRepository : BaseRepository<TableDimension>, ITableDimensionRepository
    {
        static readonly string getAllQuery = @"select distinct t.TableCode as TableCode, d.DimensionXBRLCode as DimensionQname, t.TableID TableID
from mTable t
    inner join mTableAxis ta on ta.TableID = t.TableID
    inner join mAxisOrdinate ao on ao.AxisID = ta.AxisID
    inner join mOrdinateCategorisation oc on oc.OrdinateID = ao.OrdinateID
    inner join mDimension d on d.DimensionID = oc.DimensionID   
";
        public TableDimensionRepository(IDataConnector connector) : base(getAllQuery, connector)
        {

        }

        public IEnumerable<TableDimension> getTablesWithDimension(string dimensionQname)
        {
            return base.All.Where(x => x.DimensionQname.Equals(dimensionQname));
        }
    }
}
