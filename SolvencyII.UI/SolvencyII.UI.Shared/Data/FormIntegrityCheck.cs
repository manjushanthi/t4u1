using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using SolvencyII.Data.Shared;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Controls;
using SolvencyII.UI.Shared.UserControls;

namespace SolvencyII.UI.Shared.Data
{
    /// <summary>
    /// Performs a check to ensure all data controls on a template have fields on the poco classes and fields on the database.
    /// </summary>
    public static class FormIntegrityCheck
    {
        /// <summary>
        /// Test all data controls have correct link to POCO class
        /// Test POCO class(es) with data base
        /// </summary>
        /// <param name="dataTypes"></param>
        /// <param name="dataTables"></param>
        /// <param name="dataControls"></param>
        /// <param name="getComboControls"></param>
        /// <param name="getDataComboControls"></param>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        public static string AllControlsLinkedToData(List<Type> dataTypes, List<string> dataTables, List<ISolvencyDataControl> dataControls, List<ISolvencyComboBox> getComboControls, List<ISolvencyDataComboBox> getDataComboControls)
        {

            // Get field names from the db.
            List<string> fieldNames = new List<string>();
            using (GetSQLData dataSource = new GetSQLData())
            {
                foreach (string tableName in dataTables)
                {
                    fieldNames.AddRange(dataSource.GetFieldsOfTable(tableName));
                }
            }

            // Get the fields from the POCO object
            List<string> propNames = new List<string>();
            foreach (Type dataType in dataTypes)
            {
                PropertyInfo[] props = dataType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);
                propNames.AddRange(props.Select(propertyInfo => propertyInfo.Name));
            }

            // Get the full list of data controls
            StringBuilder sb = new StringBuilder();
            // .Where(d=>d.ColName.Contains("C999"))
            foreach (ISolvencyDataControl ctrl in dataControls.Where(c => c.Enabled))
            {
                if(!ctrl.ColName.Contains("C999"))
                {
                    if (!string.IsNullOrEmpty(ctrl.ColName))
                    {
                        // Check it exists in db.
                        if (!fieldNames.Exists(s => s.Equals(ctrl.ColName)))
                        {
                            // It does not exist so we need to report to the users.
                            sb.AppendLine(string.Format("{0} does not exist in the db", ctrl.ColName));
                        }

                        if (!propNames.Exists(s => s.Equals(ctrl.ColName)))
                        {
                            // It does not exist so we need to report to the users.
                            sb.AppendLine(string.Format("{0} does not exist in the poco objects", ctrl.ColName));
                        }
                    }
                }
            }


            foreach (ISolvencyComboBox ctrl in getComboControls)
            {
                if (!ctrl.ColName.Contains("C999"))
                {
                    if (!string.IsNullOrEmpty(ctrl.ColName))
                    {
                        // Check it exists in db.
                        if (!fieldNames.Exists(s => s.Equals(ctrl.ColName)))
                        {
                            // It does not exist so we need to report to the users.

                            if (ctrl is SolvencyComboBox)
                            {
                                if (((SolvencyComboBox) ctrl).BackColor == System.Drawing.Color.Gray) continue;
                            }
                            sb.AppendLine(string.Format("{0} does not exist in the db", ctrl.ColName));
                        }

                        if (!propNames.Exists(s => s.Equals(ctrl.ColName)))
                        {
                            // It does not exist so we need to report to the users.
                            sb.AppendLine(string.Format("{0} does not exist in the poco objects", ctrl.ColName));
                        }
                    }
                }
            }

            foreach (ISolvencyDataComboBox ctrl in getDataComboControls)
            {
                if (!ctrl.ColName.Contains("C999"))
                {
                    if (!string.IsNullOrEmpty(ctrl.ColName))
                    {
                        // Check it exists in db.
                        if (!fieldNames.Exists(s => s.Equals(ctrl.ColName)))
                        {
                            // It does not exist so we need to report to the users.
                            sb.AppendLine(string.Format("{0} does not exist in the db", ctrl.ColName));
                        }

                        if (!propNames.Exists(s => s.Equals(ctrl.ColName)))
                        {
                            // It does not exist so we need to report to the users.
                            sb.AppendLine(string.Format("{0} does not exist in the poco objects", ctrl.ColName));
                        }
                    }
                }
            }




            if (string.IsNullOrEmpty(sb.ToString()))
                return "";

            return string.Format("{0}\n\r{1}", "This template has controls containing data that will not be saved.", sb.ToString());
        }

    }
}
