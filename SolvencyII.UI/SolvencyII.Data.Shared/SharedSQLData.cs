using System;
using System.Collections.Generic;
using System.Linq;
using SolvencyII.Data.Entities;
using SolvencyII.Data.Shared.Extensions;
using SolvencyII.Domain;
using SolvencyII.Domain.Configuration;
using SolvencyII.Domain.Conversions;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Interfaces;

namespace SolvencyII.Data.Shared
{
    /// <summary>
    /// Business side of data tier where queries are built to be processed by ISolvencyData providers.
    /// The functionality of this class is sed by both the GetSQLData and PutSQLData.
    /// </summary>
    internal static class SharedSQLData
    {
        #region Mapping

        public static List<MAPPING> GetMapping(int tableId, ISolvencyData conn)
        {
            return conn.Query<MAPPING>(String.Format("Select * from MAPPING where TABLE_Version_ID = {0} AND IS_IN_TABLE ", tableId));
        }

        public static IEnumerable<FormDataPage> GetTemplatePageData(List<MAPPING> mappings, int tableId, ISolvencyData conn)
        {
            return GetFormDataPage(mappings, m => m.IS_PAGE_COLUMN_KEY && m.IS_IN_TABLE, tableId, conn);
        }

        private static IEnumerable<FormDataPage> GetFormDataPage(List<MAPPING> mappings, Func<MAPPING, bool> predicate, int tableId, ISolvencyData conn)
        {
            List<FormDataPage> response = new List<FormDataPage>();
            foreach (MAPPING map in mappings.Where(predicate))
            {
                long axisID = 0;

                if (map.IS_PAGE_COLUMN_KEY)
                {
                    DimXbrlCode xbrlCode = new DimXbrlCode(map.DYN_TAB_COLUMN_NAME, true);
                    axisID = xbrlCode.GetAxisIDFromXbrlCode(tableId, conn);
                }

                response.Add(new FormDataPage
                                 {
                                     DYN_TABLE_NAME = SolvencyIITableNameConversion.FullDbName(map.DYN_TABLE_NAME),
                                     DYN_TAB_COLUMN_NAME = map.DYN_TAB_COLUMN_NAME.ToUpper(),
                                     Value = map.DIM_CODE,
                                     TableID = map.TABLE_ID,
                                     AxisID = axisID,
                                     FixedDimension = !map.DIM_CODE.Contains("*"),
                                     DOM_CODE = map.DOM_CODE
                                 });
            }
            return response;
        }

        #endregion


        #region Filing Indicators

        internal static bool FilingIndicatorExists(long instanceID, int templateOrTableID, ISolvencyData conn)
        {
            int exist = conn.ExecuteScalar<int>(string.Format("Select count(*) from dFilingIndicator where InstanceID = {0} AND BusinessTemplateID = {1};", instanceID, templateOrTableID));
            return (exist != 0);
        }

        internal static bool GetFilingIndicatorFiled(long instanceID, int templateOrTableID, ISolvencyData conn)
        {
            // If no records exist then return true.
            string query = string.Format("Select * from dFilingIndicator where InstanceID = {0} AND BusinessTemplateID = {1};", instanceID, templateOrTableID);
            bool result = conn.Query<dFilingIndicator>(query).Count == 0;
            if (result) return true;

            // When the record exists return its value
            switch (StaticSettings.DataTier)
            {
                case eDataTier.SqLite:
                    query = string.Format("Select ifnull(Filed, 1) as Filed from dFilingIndicator where InstanceID = {0} AND BusinessTemplateID = {1};", instanceID, templateOrTableID);
                    break;
                case eDataTier.SqlServer:
                    query = string.Format("Select isnull(Filed, 1) as Filed from dFilingIndicator where InstanceID = {0} AND BusinessTemplateID = {1};", instanceID, templateOrTableID);
                    break;
            }
            bool exist = conn.ExecuteScalar<bool>(query);
            return exist;
        }

        #endregion

    }
}
