using AT2DPM.DAL;
using AT2DPM.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.DataTypeValidation
{
    public static class DataTypeValidationDALHelper
    {

        private static long DefaultTaxonomyID = 1;

        private static DPMdbConnection DPMdbConnection { get; set; }

        public static DPMdb DPMdb { get; set; }

        public static void Initializer(string databaseFileName)
        {
            if (DPMdbConnection == null)
            {
                DPMdbConnection = new DPMdbConnection();
            }

            if (DPMdb == null)
            {
                DPMdb = DPMdbConnection.OpenDpmConnection(databaseFileName);
            }
        }

        public static void DeInitializer()
        {
            DPMdbConnection.CloseActiveDatabase();
        }

        public static mTable GetTable(string tableCode)
        {
            if (DPMdb != null)
            {
                return DPMdb.mTables.FirstOrDefault(x => x.TableCode == tableCode);
            }
            return null;
            
        }

        public static mTaxonomy GetTaxonomy(string tableCode)
        {
            if (DPMdb != null)
            {
                return DPMdb.mTaxonomies.FirstOrDefault(x => x.TaxonomyID == DefaultTaxonomyID);
            }
            return null;
        }

        public static string GetTableName(mTable table, mTaxonomy taxonomy)
        {
            if (table == null)
                return string.Empty;

            if (taxonomy == null)
                return string.Empty;

            StringBuilder sb = new StringBuilder();

            sb.Append("T__");
            sb.Append(table.TableCode.Replace('.', '_'));
            sb.Append("__");
            sb.Append(taxonomy.TaxonomyCode.Trim());
            sb.Append("__");
            sb.Append(taxonomy.Version.Replace('.', '_'));

            return sb.ToString();
        }

        public static string GetTableName(string tableCode)
        {
            if (string.IsNullOrEmpty(tableCode))
                return string.Empty;

            mTable validationTable = DataTypeValidationDALHelper.GetTable(tableCode);

            if (validationTable == null)
                return null;


            mTaxonomy validationTaxonomy = DataTypeValidationDALHelper.GetTaxonomy(tableCode);

            if (validationTaxonomy == null)
                return null;


            string tableName = DataTypeValidationDALHelper.GetTableName(validationTable, validationTaxonomy);

            return tableName;
        }


    }
}
