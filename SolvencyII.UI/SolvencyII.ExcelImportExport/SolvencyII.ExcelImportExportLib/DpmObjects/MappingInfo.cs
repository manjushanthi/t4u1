using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SolvencyII.Domain;
using SolvencyII.Domain.Interfaces;

namespace SolvencyII.ExcelImportExportLib.DpmObjects
{
    public class MappingInfo
    {
        public IEnumerable<MAPPING> GetMappingInfo(ISolvencyData dpmConn, long tableID)
        {
            string mappingQuery = string.Format("select * from mapping where table_version_id = {0} ", tableID);

            return dpmConn.Query<MAPPING>(mappingQuery).ToList<MAPPING>();
        }
    }
}
