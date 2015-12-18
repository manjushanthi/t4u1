using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using SolvencyII.Data.Shared;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.Domain.Interfaces;

namespace SolvencyII.UI.Shared.Data
{
    /// <summary>
    /// Created to implement DataSet binding but not used.
    /// </summary>
    public static class dDataSource
    {

        public static DataSet GetOpenDataSet(Type dataType, string tableName, long instanceID, List<OpenColInfo2> columns, Dictionary<string, string> specifiedColumnsFromCombos, List<ISolvencyPageControl> specifiedColumnsNPage)
        {
            DataSet result = new DataSet("DS");
            DataTable table = new DataTable("Open");
            result.Tables.Add(table);

            foreach (OpenColInfo2 column in columns)
            {
                result.Tables["Open"].Columns.Add(column.ColName);
            }
            result.Tables["Open"].Columns.Add("PK_ID");

            List<OpenTableDataRow2> secondaryCache;
            using (GetSQLData getData = new GetSQLData())
            {
                secondaryCache = getData.GetVirtualObjectItemCache2(tableName, dataType, specifiedColumnsFromCombos, 0, -1, instanceID, columns, specifiedColumnsNPage, "", "");
            }
            foreach (OpenTableDataRow2 dataRow2 in secondaryCache)
            {
                DataRow row = result.Tables["Open"].Rows.Add();
                int i = 0;
                foreach (OpenColInfo2 column in columns)
                {
                    row[column.ColName] = dataRow2.ColValues[i];
                    i++;
                }
                row["PK_ID"] = dataRow2.PK_ID;
                row.AcceptChanges();
                // result.Tables["Open"].Rows.Add(row);
            }
            return result;
        }



    }
}
