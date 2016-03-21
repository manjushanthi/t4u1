using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using SolvencyII.Data.Shared.Dictionaries;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.Domain.Entities;
using System.Globalization;

namespace SolvencyII.UI.Shared.Extensions
{
    /// <summary>
    /// Extensions for ComboBoxes to populate and select items
    /// </summary>
    public static class  comboBoxExt
    {
        public static void SelectItemByValue(this ComboBox cmb, long value)
        {
            foreach (ListViewItem listItem in cmb.Items)
            {
                if(int.Parse(listItem.Name) == value)
                {
                    cmb.SelectedItem = listItem;
                    break;
                }
            }
        }
        
        public static void SelectItemByValue(this ComboBox cmb, string value)
        {
            if (cmb.Items.Count > 0)
            {
                if (cmb.Items[0] as ListViewItem != null)
                {
                    foreach (ListViewItem listItem in cmb.Items)
                    {
                        if (listItem.Name == value)
                        {
                            cmb.SelectedItem = listItem;
                            break;
                        }
                    }
                }
                else
                {
                    foreach (ComboItem listItem in cmb.Items)
                    {
                        if (listItem.Value == value)
                        {
                            cmb.SelectedItem = listItem;
                            break;
                        }
                    }
                }
            }
        }


        public static void SelectFirstItem(this ComboBox cmb)
        {
            foreach (ListViewItem listItem in cmb.Items)
            {
                cmb.SelectedItem = listItem;
                break;
            }
        }

        public static void SelectFirstNonAbstractItem(this ComboBox combo)
        {
            // Find the first item in combo box not Abstract
            foreach (ListViewItem listItem in combo.Items)
            {
                bool IsAbstract = false;
                if (listItem.Tag == null)
                {
                    combo.SelectedItem = listItem;
                    break;
                }
                if (bool.TryParse(listItem.Tag.ToString(), out IsAbstract))
                {
                    if (!IsAbstract)
                    {
                        combo.SelectedItem = listItem;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Zero based ListViewItem selection by list order for combo box
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="value"></param>
        public static void SelectItemByListOrder(this ComboBox cmb, int value)
        {
            int count = 0;
            foreach (ListViewItem listItem in cmb.Items)
            {
                if (count == value)
                {
                    cmb.SelectedItem = listItem;
                    break;
                }
                count++;
            }
        }



        public static void PopulateWithListViewItems(this ComboBox cmb, List<ListViewItem> dataSource)
        {
            // Populate the combo with the data and select the appropriate item
            if (dataSource != null)
            {
                foreach (ListViewItem listItem in dataSource)
                {
                    cmb.Items.Add(listItem);
                }
                cmb.DisplayMember = "Text";
                cmb.ValueMember = "Name";
                    
                if (dataSource.Count > 0)
                    cmb.SelectedItem = cmb.Items[0];
            }
        }

        public static void PopulateComboItems(this ComboBox cmb, List<ComboItem> dataSource)
        {
            // Populate the combo with the data and select the appropriate item
            if (dataSource != null)
            {
                foreach (ComboItem cItem in dataSource)
                {
                    // Convert to ListViewItem since we can make entries bold by altering the font.
                    ListViewItem listItem = new ListViewItem {Text = cItem.Text, Name = cItem.Value, Tag = cItem.IsAbstract};
                    // listItem.Tag = new ComboTag(cItem.IsAbstract);
                    cmb.Items.Add(listItem);
                }
                cmb.DisplayMember = "Text";
                cmb.ValueMember = "Name";

                if (dataSource.Count > 0)
                    cmb.SelectedItem = cmb.Items[0];
            }
        }

        public static void PopulateComboItems(this ComboBox cmb, List<string> dataSource)
        {
            if (dataSource != null)
            {
                // Populate the combo with the data and select the appropriate item
                foreach (string cItem in dataSource)
                {
                    // Convert to ListViewItem since we can make entries bold by altering the font.
                    ListViewItem listItem = new ListViewItem {Text = cItem, Name = cItem};
                    cmb.Items.Add(listItem);
                }
                cmb.DisplayMember = "Text";
                cmb.ValueMember = "Name";

                if (dataSource.Count > 0)
                    cmb.SelectedItem = cmb.Items[0];
            }
        }

        public static void SetDropDownWidth(this ComboBox cmb)
        {
            int maxWidth = 0, temp = 0;
            foreach (object s in cmb.Items)
            {
                temp = TextRenderer.MeasureText(cmb.GetItemText(s), cmb.Font).Width;
                if (temp > maxWidth)
                {
                    maxWidth = temp;
                }
            }
            cmb.DropDownWidth = maxWidth + SystemInformation.VerticalScrollBarWidth;
        }

        public static bool ItemAlreadyExists(this ComboBox cmb, string value)
        {
            return cmb.Items.Cast<ListViewItem>().Any(listItem => listItem.Name == value);
        }

        public static bool IsAbstract(this ComboBox combo)
        {
            // Find the first item in combo box not Abstract
            ListViewItem listItem = (ListViewItem) combo.SelectedItem;
            bool isAbstract = false;
            if (listItem != null)
            {
                if (listItem.Tag == null) return false;
                bool.TryParse(listItem.Tag.ToString(), out isAbstract);
            }
            return isAbstract;
        }

        public static bool ItemsPopulated(this ComboBox combo)
        {
            return (combo.Items.Count > 1);
        }
    }
}
