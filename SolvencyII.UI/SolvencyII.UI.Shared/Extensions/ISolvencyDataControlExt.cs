using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Extensions;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Controls;

namespace SolvencyII.UI.Shared.Extensions
{
    /// <summary>
    /// Extension for ISolvencyDataControl
    /// </summary>
    public static class ISolvencyDataControlExt
    {
        public static string SQLValue(this ISolvencyDataControl iControl)
        {
            // User of text in CheckBox causes it to appear next to the box thus is not appropriate.
            // The Result is therefore being used.
            if (iControl.ColumnType == SolvencyDataType.Boolean)
            {
                if (iControl.Result != null)
                    return (bool) iControl.Result ? "1" : "0";
                return "null";
            }
            
            if (string.IsNullOrEmpty(iControl.Text)) return "null";
            switch (iControl.ColumnType)
            {
                case SolvencyDataType.Boolean:
                    // See above
                    return "null";
                case SolvencyDataType.Date:
                    DateTime? temp = ((DateTime?) iControl.Result);
                    if(temp != null)
                        return string.Format("'{0}'", temp.ConvertToDateTimeString());
                    return "null";
                case SolvencyDataType.Integer:
                    if (iControl.Result != null)
                    {
                        int result;
                        if (int.TryParse(iControl.Result.ToString(), out result))
                            return result.ToString("0", CultureInfo.InvariantCulture);    
                    }
                    return "";
                case SolvencyDataType.Percentage:
                case SolvencyDataType.Decimal:
                    decimal temp2 = (decimal)iControl.Result;
                    if (temp2 != 0)
                        return temp2.ToString("0.000000", CultureInfo.InvariantCulture);
                    if(string.IsNullOrEmpty(iControl.Text))
                        return "";
                    return "0";
                case SolvencyDataType.Monetry:
                    decimal temp3 = (decimal)iControl.Result;
                    if (temp3 != 0)
                        return temp3.ToString("0.00", CultureInfo.InvariantCulture);
                    if(string.IsNullOrEmpty(iControl.Text))
                        return "";
                    return "0";
                case SolvencyDataType.Code:
                    return string.Format("'{0}'", iControl.Text);
                    //return string.Format("'{0}'", ((SolvencyComboBox)iControl).GetValue);
                case SolvencyDataType.String:
                    return string.Format("'{0}'", iControl.Text);
                default:
                    return string.Format("'{0}'", iControl.Text);
            }
        }

        public static object Value(this ISolvencyDataControl iControl)
        {
            // User of text in CheckBox causes it to appear next to the box thus is not appropriate.
            // The Result is therefore being used.
            if (iControl.ColumnType == SolvencyDataType.Boolean)
            {
                if (iControl.Result != null)
                    return (bool)iControl.Result;
                return null;
            }

            if (string.IsNullOrEmpty(iControl.Text)) return null;
            switch (iControl.ColumnType)
            {
                case SolvencyDataType.Boolean:
                    // See above
                    return null;
                case SolvencyDataType.Date:
                    return iControl.Result;
                case SolvencyDataType.Integer:
                    if (iControl.Result != null)
                    {
                        int result;
                        if (int.TryParse(iControl.Result.ToString(), out result))
                            return result.ToString("0", CultureInfo.InvariantCulture);
                    }
                    return null;
                case SolvencyDataType.Percentage:
                case SolvencyDataType.Decimal:
                    decimal temp2 = (decimal)iControl.Result;
                    if (temp2 != 0)
                        return temp2;
                    if (string.IsNullOrEmpty(iControl.Text))
                        return null;
                    return "0";
                case SolvencyDataType.Monetry:
                    decimal temp3 = (decimal)iControl.Result;
                    if (temp3 != 0)
                        return temp3;
                    if (string.IsNullOrEmpty(iControl.Text))
                        return null;
                    return "0";
                case SolvencyDataType.Code:
                    return iControl.Text;
                case SolvencyDataType.String:
                    return iControl.Text;
                default:
                    return iControl.Text;
            }
        }

        public static void CopyDataControl(this ISolvencyDataControl iControl, ISolvencyDataControl outControl)
        {
            outControl.Size = iControl.Size;
            outControl.Location = iControl.Location;

            outControl.ColName = iControl.ColName;
            outControl.ColumnType = iControl.ColumnType;
            outControl.Enabled = iControl.Enabled;
            outControl.IsRowKey = iControl.IsRowKey;
            outControl.TableName = iControl.TableName;

        }

    }
}
