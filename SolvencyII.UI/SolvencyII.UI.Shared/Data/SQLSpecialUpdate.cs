using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SolvencyII.Domain.Configuration;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Controls;
using SolvencyII.UI.Shared.Extensions;

namespace SolvencyII.UI.Shared.Data
{
    /// <summary>
    /// Special cases data update class.
    /// </summary>
    public static class SQLSpecialUpdate
    {
        public static void BuildSQLQuery_Update(ref List<string> queries, ref List<Dictionary<string, object>> parameters, string tableName, List<ISolvencyDataControl> dataControls, List<SolvencyDataComboBox> columnCombos, List<SolvencyDataComboBox> rowCombos, long instanceID, List<ISolvencyPageControl> nPageControls)
        {
            // To manage the situation when two columns have their combos reversed; the constraints are violated,
            // the original rows must be deleted first.
            queries.Add(string.Format("Delete from [{0}] where Instance = @Instance ", tableName));
            parameters.Add(new Dictionary<string, object> {{"@Instance", instanceID}});


            foreach (ISolvencyDataControl dataControl in dataControls)
            {
                Dictionary<string, object> pars = new Dictionary<string, object>();

                CellKey key = new CellKey(dataControl.Name);

                // If the primary key is not know then we have an insert.

                StringBuilder sb = new StringBuilder();
                StringBuilder sb2 = new StringBuilder();

                // Insert query required
                sb.Append(string.Format("Insert into [{0}] ( INSTANCE", tableName));
                sb2.Append(string.Format(") Values ( {0}", instanceID));

                // Text Field
                string fieldName = dataControl.ColName;

                sb.Append(string.Format(", {0}", fieldName));
                sb2.Append(string.Format(", @{0}", fieldName));

                pars.Add(string.Format("@{0}", fieldName), dataControl.Value());

                // Row Combo
                SolvencyDataComboBox rowCombo = rowCombos[key.RowNumber];
                fieldName = rowCombo.ColName;

                sb.Append(string.Format(", {0}", fieldName));
                sb2.Append(string.Format(", @{0}", fieldName));

                pars.Add(string.Format("@{0}", fieldName), rowCombo.GetValue);

                // Cols Combo
                SolvencyDataComboBox colCombo = columnCombos[key.ColumnNumber];
                fieldName = colCombo.ColName;

                sb.Append(string.Format(", {0}", fieldName));
                sb2.Append(string.Format(", @{0}", fieldName));

                pars.Add(string.Format("@{0}", fieldName), colCombo.GetValue);


                // Combo box / Fixed Dimension information retrieveal.
                foreach (var pageControl in nPageControls)
                {
                    List<string> tables = pageControl.TableNames.Split('|').ToList();
                    int pos = tables.IndexOf(tableName);
                    if (pos != -1)
                    {

                        sb.Append(string.Format(", {0}", pageControl.ColName));
                        sb2.Append(string.Format(", @{0}", pageControl.ColName));

                        pars.Add(string.Format("@{0}", pageControl.ColName), pageControl.Value());
                    }
                }
                queries.Add(string.Format("{0} {1}", sb, sb2 + "); "));
                parameters.Add(pars);
            }
        }
    }
}
