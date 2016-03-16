using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SolvencyII.Domain.Entities;
using SolvencyII.UI.Shared.Controls;

namespace SolvencyII.UI.Shared.Extensions
{
    /// <summary>
    /// Extension for SolvencyDataComboBox
    /// </summary>
    public static class SolvencyDataComboBoxExt
    {
        public static SolvencyDataComboBox DeepCopyWithItems(this SolvencyDataComboBox cbo)
        {
            SolvencyDataComboBox result = new SolvencyDataComboBox();

            result.Items.AddRange(cbo.Items.OfType<ListViewItem>().ToArray());
            result.DisplayMember = "Text";
            result.ValueMember = "Name";

            result.AxisID = cbo.AxisID;
            result.ColName = cbo.ColName;
            result.ColumnType = cbo.ColumnType;
            result.SetValue = cbo.GetValue;
            result.HierarchyID = cbo.HierarchyID;
            result.IsRowKey = cbo.IsRowKey;
            result.NextOrder = cbo.NextOrder;
            result.OrdinateID = cbo.OrdinateID;
            result.StartOrder = cbo.StartOrder;
            result.TableName = cbo.TableName;
            result.TypeOfItems = cbo.TypeOfItems;

            result.Location = cbo.Location;
            result.Size = cbo.Size;
            result.BackColor = cbo.BackColor;
            result.Enabled = cbo.Enabled;

            return result;

        }
    }
}
