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
    /// Extension for SolvencyComboBox
    /// </summary>
    public static class SolvencyComboBoxExt
    {
        public static void PopulateWithHierachy2(this SolvencyComboBox cmb, List<OpenComboItem> dataSource, string selectedValue = null)
        {
            // Populate the combo with the data and select the appropriate item
            if (dataSource != null)
            {
                foreach (ListViewItem listItem in dataSource.Select(i => new ListViewItem { Text = i.Text, Name = i.Name, Tag = i.IsAbstract || !i.Include }))
                {
                    cmb.Items.Add(listItem);
                    if (listItem.Name == selectedValue) cmb.SelectedItem = listItem;
                }
                cmb.DisplayMember = "Text";
                cmb.ValueMember = "Name";
            }
        }
    }
}
