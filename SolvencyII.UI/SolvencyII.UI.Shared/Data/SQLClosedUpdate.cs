using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SolvencyII.Domain.Configuration;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Controls;
using SolvencyII.UI.Shared.Extensions;

namespace SolvencyII.UI.Shared.Data
{
    public static class SQLClosedUpdate
    {
        public static void BuildSQLQuery_Update(ref List<string> queries, ref List<Dictionary<string, object>> parameters, string tableName, long pkID, IClosedRowControl closedControl, long instanceID, int tablePosition, Type dataType, List<ISolvencyPageControl> nPageControls)
        {
            // Constants for max parameters
            const int maxParametersInQuery = 900;
            int controlCount = 0;

            // Build an update statment
            List<ISolvencyDataControl> dataControls = closedControl.GetDataControls();
            List<ISolvencyComboBox> comboControls = closedControl.GetComboControls();
            List<ISolvencyDataComboBox> dataCombos = closedControl.GetDataComboControls();
            Dictionary<string, object> pars = new Dictionary<string, object>();

            // If the primary key is not know then we have an insert.
            bool update = (pkID != 0);
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            string sbStart = "Update [{0}] Set INSTANCE = {1} ";
            string sb2Start = "Where PK_ID = {0} ";


            if (update)
            {
                // Update query required
                sb.Append(string.Format(sbStart, tableName, instanceID));
                sb2.Append(string.Format(sb2Start, pkID));
            }
            else
            {
                // Insert query required
                sb.Append(string.Format("Insert into [{0}] ( INSTANCE", tableName));
                sb2.Append(string.Format(") Values ( {0}", instanceID));
            }

            var props = dataType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // We have RowColumn entries
            //foreach (PropertyInfo info in props.Where(i => i.Name.StartsWith("R") || i.Name.StartsWith("C")))
            foreach (PropertyInfo info in props.Where(i => !(i.Name.StartsWith("PAGE") || i.Name == "PK_ID" || i.Name == "INSTANCE")))
            {
                string fieldName = info.Name;
                //ISolvencyDataControl cntrl = GetDataControls().FirstOrDefault(c => c.ColName == fieldName);
                ISolvencyDataControl cntrl = dataControls.FirstOrDefault(c => c.ColName == fieldName);
                if (cntrl != null)
                {
                    controlCount++;
                    if (update)
                    {
                        sb.Append(string.Format(", {0} = @{0}", fieldName));
                    }
                    else
                    {
                        sb.Append(string.Format(", {0}", fieldName));
                        sb2.Append(string.Format(", @{0}", fieldName));
                    }
                    pars.Add(string.Format("@{0}", fieldName), cntrl.Value());
                }
                else
                {
                    ISolvencyComboBox comboCntrl = comboControls.FirstOrDefault(c => c.ColName == fieldName);
                    if (comboCntrl != null)
                    {
                        controlCount++;
                        if (update)
                        {
                            sb.Append(string.Format(", {0} = @{0}", fieldName));
                        }
                        else
                        {
                            sb.Append(string.Format(", {0}", fieldName));
                            sb2.Append(string.Format(", @{0}", fieldName));
                        }
                        pars.Add(string.Format("@{0}", fieldName), comboCntrl.GetValue);
                    }
                }

                if (controlCount == maxParametersInQuery)
                {
                    update = ExtraQuery(queries, parameters, tableName, pkID, instanceID, sb, sb2, update, sbStart, sb2Start, ref pars);
                    controlCount = 0;
                }
            }

            // Multiple row combos
            foreach (PropertyInfo info2 in props.Where(i => i.Name.StartsWith("PAGE")))
            {
                string fieldName2 = info2.Name;
                ISolvencyDataComboBox comboCntrl = dataCombos.FirstOrDefault(c => c.ColName == fieldName2);
                if (comboCntrl != null)
                {
                    controlCount++;
                    if (update)
                    {
                        sb.Append(string.Format(", {0} = @{0}", fieldName2));
                    }
                    else
                    {
                        sb.Append(string.Format(", {0}", fieldName2));
                        sb2.Append(string.Format(", @{0}", fieldName2));
                    }
                    pars.Add(string.Format("@{0}", fieldName2), comboCntrl.GetValue);
                }
                if (controlCount == maxParametersInQuery)
                {
                    update = ExtraQuery(queries, parameters, tableName, pkID, instanceID, sb, sb2, update, sbStart, sb2Start, ref pars);
                    controlCount = 0;
                }

            }

            // Combo box / Fixed Dimension information retrieveal.
            foreach (var pageControl in nPageControls)
            {
                List<string> tables = pageControl.TableNames.Split('|').ToList();
                int pos = tables.IndexOf(tableName);
                if (pos != -1)
                {
                    controlCount++;
                    if (update)
                    {
                        sb.Append(string.Format(", {0} = @{0}", pageControl.ColName));
                    }
                    else
                    {
                        sb.Append(string.Format(", {0}", pageControl.ColName));
                        sb2.Append(string.Format(", @{0}", pageControl.ColName));
                    }
                    pars.Add(string.Format("@{0}", pageControl.ColName), pageControl.Value());
                    
                }
                if (controlCount == maxParametersInQuery)
                {
                    update = ExtraQuery(queries, parameters, tableName, pkID, instanceID, sb, sb2, update, sbStart, sb2Start, ref pars);
                    controlCount = 0;
                }

            }

            queries.Add(string.Format("{0} {1}", sb, sb2 + (update ? ";" : "); ")));
            parameters.Add(pars);
        }

        private static bool ExtraQuery(List<string> queries, List<Dictionary<string, object>> parameters, string tableName, long pkID, long instanceID, StringBuilder sb, StringBuilder sb2, bool update, string sbStart, string sb2Start, ref Dictionary<string, object> pars)
        {
            // We have reached our maximum.
            queries.Add(string.Format("{0} {1}", sb, sb2 + (update ? ";" : "); ")));
            parameters.Add(pars);

            pars = new Dictionary<string, object>();
            sb.Length = 0;
            sb2.Length = 0;
            if (update)
            {
                sb.Append(string.Format(sbStart, tableName, instanceID));
                sb2.Append(string.Format(sb2Start, pkID));
            }
            else
            {
                // Previously this was an insert statement so we need to change it to an update one.
                update = true;
                sb.Append(string.Format(sbStart, tableName, instanceID));
                if (StaticSettings.DataTier == eDataTier.SqLite)
                    sb2.Append(string.Format(sb2Start, " last_insert_rowid() "));
                else
                    sb2.Append(string.Format(sb2Start, " @@IDENTITY "));
            }
            return update;
        }
    }
}
