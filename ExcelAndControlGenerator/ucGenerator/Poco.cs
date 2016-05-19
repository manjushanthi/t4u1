using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SolvencyII.Domain;
using SolvencyII.Domain.Entities;

namespace ucGenerator
{
    public static class Poco
    {
        public static string GenerateCode(string file, string tableName, List<PocoColInfo> tableInfo, List<MAPPING> tableMapping)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(GenerateInfoTop(tableName));

            foreach (PocoColInfo pocoColInfo in tableInfo)
            {
                if (pocoColInfo.pk)
                {
                    sb.AppendLine(@"        [PrimaryKey , AutoIncrement]");
                }
                //    sb.AppendLine("        [Key] ");
                //    sb.AppendLine("        [DatabaseGenerated] ");
                //}
                sb.AppendLine(string.Format(@"        public {0} {1} {{ get; set; }} ", pocoColInfo.type.TypeConversion(pocoColInfo.notnull, tableMapping.FirstOrDefault(m=>m.DYN_TAB_COLUMN_NAME.ToUpper()==pocoColInfo.name.ToUpper())), pocoColInfo.name));
            }

            sb.AppendLine(GenerateInfoBottom());
            return sb.ToString();
        }

        private static string GenerateInfoTop(string tableName)
        {

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("using System;");
            sb.AppendLine("using SolvencyII.Domain.Attributes;");
            sb.AppendLine();
            sb.AppendLine("namespace SolvencyII.Domain ");
            sb.AppendLine("{ ");
            sb.AppendLine(string.Format(@"    public partial class {0}", tableName));
            sb.AppendLine("    { ");
            return sb.ToString();
        }

        private static string GenerateInfoBottom()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine("    } ");
            sb.AppendLine("} ");
            return sb.ToString();
        }
    }
}
