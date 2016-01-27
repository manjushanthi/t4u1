using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SolvencyII.Domain.Entities;
using SolvencyII.UI.Shared.Controls;

namespace SolvencyII.UI.Shared.Extensions
{
    /// <summary>
    /// Extension of SolvencyTextComboBox
    /// </summary>
    public static class SolvencyTextComboBoxExt
    {
        public static bool AddEntry(this SolvencyTextComboBox comboBox)
        {
            string newItem = "";
            if (UserInput.Input.InputBox("New Entry", "Please add the new item", ref newItem) == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(newItem))
                {
                    // Check item does not already exist
                    if (!comboBox.ItemAlreadyExists(newItem))
                    {
                        // Clear the form
                        //GetRepository().ClearAllControls();

                        // Add text
                        ListViewItem listItem = new ListViewItem {Text = newItem, Name = newItem, Selected = true};
                        comboBox.Items.Add(listItem);
                        comboBox.DisplayMember = "Text"; // Incase there are no other members.
                        comboBox.ValueMember = "Name";
                        comboBox.SelectItemByValue(newItem);
                        return true;
                    }
                    MessageBox.Show(string.Format("This item cannot be added.\r\n{0} already exists.", newItem));
                }
            }
            return false;
        }

        public static void DeleteEntry(this SolvencyTextComboBox comboBox)
        {
            comboBox.Items.Remove(comboBox.SelectedItem);
            comboBox.SelectFirstItem();
        }

        public static void PopulateWithHierachy2(this SolvencyTextComboBox cmb, List<OpenComboItem> dataSource, string selectedValue = null)
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
