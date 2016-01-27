using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SolvencyII.Data.Shared;
using SolvencyII.Data.Shared.Dictionaries;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Controls;
using SolvencyII.UI.Shared.Registry;
using System.Globalization;

namespace SolvencyII.UI.Shared.Config
{
    /// <summary>
    /// Populating Comboboxes with standard items
    /// </summary>
    public static class ComboBoxConfig
    {
        public static void ComboBoxDrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
                return;
            ListViewItem item = (ListViewItem)((ComboBox)sender).Items[e.Index];
            using (SolidBrush brush = new SolidBrush(e.ForeColor))
            {
                e.DrawBackground();
                e.Graphics.DrawString(item.Text, item.Font, brush, e.Bounds);
                e.DrawFocusRectangle();
            }
        }
       
        public static List<ListViewItem> PopulateComboCurrency(GetSQLData getData)
        {

            Dictionary<string, ListViewItem> currenyDictionary = new Dictionary<string, ListViewItem>();
           
            foreach (CultureInfo cultureInfo in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                RegionInfo regionInfo = new RegionInfo(cultureInfo.Name);

                if (!currenyDictionary.ContainsKey(regionInfo.ISOCurrencySymbol))
                {
                    currenyDictionary.Add(regionInfo.ISOCurrencySymbol, new ListViewItem { Text = regionInfo.CurrencyEnglishName + " (" + regionInfo.ISOCurrencySymbol + ")", Name = regionInfo.ISOCurrencySymbol });
                }
            }
            //return currenyDictionary.Values.ToList();
            var orderResult = from element in currenyDictionary.Values
                          orderby element.Text
                          select element;
            List<ListViewItem> results = orderResult.ToList();
            results.Insert(0, new ListViewItem { Text = "Please Select", Name = "DEFAULT" });

            return results;
            /*
            // This may be used on the insert Instance form

            // Quick fix for Aitor
            List<ListViewItem> result = new List<ListViewItem>();
            result.Add(new ListViewItem { Text = "Bulgaria Leva", Name = "BGN" });
            result.Add(new ListViewItem { Text = "Czech Republic Koruny", Name = "CZK" });
            result.Add(new ListViewItem { Text = "Denmark Kroner", Name = "DKK" });
            result.Add(new ListViewItem { Text = "Hungary Forint", Name = "HUF" });
            result.Add(new ListViewItem { Text = "Iceland Kronur", Name = "ISK" });
            result.Add(new ListViewItem { Text = "Euro", Name = "EUR" });
            result.Add(new ListViewItem { Text = "Latvia Lats", Name = "LVL" });
            result.Add(new ListViewItem { Text = "Switzerland Francs", Name = "CHF" });
            result.Add(new ListViewItem { Text = "Lithuania LItas", Name = "LTL" });
            result.Add(new ListViewItem { Text = "Norway Kroner", Name = "NOK" });
            result.Add(new ListViewItem { Text = "Poland Zlotych", Name = "PLN" });
            result.Add(new ListViewItem { Text = "Romania New Lei", Name = "RON" });
            result.Add(new ListViewItem { Text = "Sweden Kronor", Name = "SEK" });
            result.Add(new ListViewItem { Text = "United Kingdom Pounds", Name = "GBP" });
            return result;
            */


            //// HierarchyID = 1360 // Currencies

            //List<Signature> currencies = getData.HierarchyLookupDirect(1360);
            //return (from m in currencies
            //        select (new ListViewItem
            //        {
            //            Text = m.Inner.Trim(),
            //            Name = m.MemberCode
            //        })).ToList();

        //    public List<Signature> HierarchyLookupDirect_DELETEME(int hierarchyID)
        //{
        //    // Here we are gathering all the info required to save details for this MemberCode

        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("Select Distinct o.OwnerPrefix, d.DomainCode, ");
        //    sb.Append("m.MemberCode, substr('                ',0,(hn.Level-1) * 3) || m.MemberLabel [Inner] ");
        //    sb.Append("from AxisOrdinate ao ");
        //    sb.Append("Inner join OrdinateCategorisation oc on (oc.OrdinateID = ao.OrdinateID) ");
        //    sb.Append("Inner join HierarchyNode hn on (hn.HierarchyID = oc.HierarchyID) ");
        //    sb.Append("Inner join Member m on (m.MemberID = hn.MemberID) ");
        //    sb.Append("Inner join Metric mt on (mt.CodeSubdomainID = hn.HierarchyID) ");
        //    sb.Append("Inner join Domain d on (d.DomainID = m.DomainID) ");
        //    sb.Append("Inner join Concept c on (c.ConceptID = d.ConceptID) ");
        //    sb.Append("Inner join Owner o on (o.OwnerID = c.OwnerID) ");
        //    sb.Append(string.Format("where hn.HierarchyID = {0} ", hierarchyID));

        //    sb.Append("AND ( ");
        //    sb.Append("(oc.StartingMemberID in (hn.ParentMemberID,hn.MemberID) and oc.IsIncludingTopLevel  = 1) ");
        //    sb.Append("OR ");
        //    sb.Append("(oc.StartingMemberID = hn.ParentMemberID and oc.IsIncludingTopLevel  = 0) ");
        //    sb.Append(") ");

        //    sb.Append("Order by hn.Path ");
        //    return _conn.Query<Signature>(sb.ToString());
        //}



        }

        #region Menu Combos

        public static ToolStripItem[] PopulateLanguages(EventHandler<EventArgs> itemClick)
        {
            List<ComboItem> factComboItems;
            using (GetSQLData getData = new GetSQLData())
            {
                factComboItems = getData.GetLanguageDropDownData().ToList();
            }

            List<ToolStripItem> result = new List<ToolStripItem>();

            foreach (ComboItem comboItem in factComboItems)
            {
                ToolStripItem item = new ToolStripMenuItem();
                item.Text = comboItem.Text;
                item.Name = comboItem.Value;
                item.Click += new EventHandler(itemClick);
                result.Add(item);
            }

            return result.ToArray();
        }

        public static ToolStripItem[] PopulateFormLanguages(EventHandler<EventArgs> itemClick)
        {

            List<ComboItem> factComboItems = new List<ComboItem>();
            factComboItems.Add(new ComboItem() {Text = "Bulgarian", Value = "1"});
            factComboItems.Add(new ComboItem() {Text = "Croatian", Value = "2"});
            factComboItems.Add(new ComboItem() {Text = "Czech", Value = "3"});
            factComboItems.Add(new ComboItem() {Text = "Danish", Value = "4"});
            factComboItems.Add(new ComboItem() {Text = "Dutch", Value = "5"});
            factComboItems.Add(new ComboItem() {Text = "English", Value = "6"});
            factComboItems.Add(new ComboItem() {Text = "Estonian", Value = "7"});
            factComboItems.Add(new ComboItem() {Text = "Finnish", Value = "8"});
            factComboItems.Add(new ComboItem() {Text = "French", Value = "9"});
            factComboItems.Add(new ComboItem() {Text = "German", Value = "10"});
            factComboItems.Add(new ComboItem() {Text = "Greek", Value = "11"});
            factComboItems.Add(new ComboItem() {Text = "Hungarian", Value = "12"});
            factComboItems.Add(new ComboItem() {Text = "Irish", Value = "13"});
            factComboItems.Add(new ComboItem() {Text = "Italian", Value = "14"});
            factComboItems.Add(new ComboItem() {Text = "Latvian", Value = "15"});
            factComboItems.Add(new ComboItem() {Text = "Lithuanian", Value = "16"});
            factComboItems.Add(new ComboItem() {Text = "Maltese", Value = "17"});
            factComboItems.Add(new ComboItem() {Text = "Polish", Value = "18"});
            factComboItems.Add(new ComboItem() {Text = "Portuguese", Value = "19"});
            factComboItems.Add(new ComboItem() {Text = "Romanian", Value = "20"});
            factComboItems.Add(new ComboItem() {Text = "Slovak", Value = "21"});
            factComboItems.Add(new ComboItem() {Text = "Spanish", Value = "22"});
            factComboItems.Add(new ComboItem() {Text = "Swedish", Value = "23"});
            factComboItems.Add(new ComboItem() {Text = "Slovenian", Value = "24"});

            List<ToolStripItem> result = new List<ToolStripItem>();

            foreach (ComboItem comboItem in factComboItems)
            {
                ToolStripItem item = new ToolStripMenuItem();
                item.Text = comboItem.Text;
                item.Name = comboItem.Value;
                item.Click += new EventHandler(itemClick);
                result.Add(item);
            }

            return result.ToArray();
        }

        public static ToolStripItem[] PopulateInstance(EventHandler<EventArgs> itemClick)
        {
            List<ComboItem> factComboItems;
            using (GetSQLData getData = new GetSQLData())
            {
                factComboItems = getData.GetInstanceDropDownData().ToList();
            }

            List<ToolStripItem> result = new List<ToolStripItem>();

            foreach (ComboItem comboItem in factComboItems)
            {
                ToolStripItem item = new ToolStripMenuItem();
                item.Text = comboItem.Text;
                item.Name = comboItem.Value;
                item.Click += new EventHandler(itemClick);
                result.Add(item);
            }

            return result.ToArray();
        }

        public static ToolStripItem[] PopulateRecentFiles(EventHandler<EventArgs> recentItemsClick)
        {
            List<ToolStripItem> result = new List<ToolStripItem>();
            ModifyRegistry modifyRegistry = new ModifyRegistry();
            for (int i = 1; i <= RecentFilesRegistryManagement.RECENTFILE_COUNT; i++)
            {
                string key = string.Format(RecentFilesRegistryManagement.RECENTFILE_SIGNATURE, i);
                string value = modifyRegistry.Read(key);
                if (string.IsNullOrEmpty(value)) break;
                ToolStripMenuItem item = new ToolStripMenuItem
                               {
                                   Text = string.Format("&{0} {1}", i, value), 
                                   Name = value
                               };
                item.Click += new EventHandler(recentItemsClick);
                result.Add(item);
            }

            return result.ToArray();
        }

        #endregion


        public static List<ListViewItem> PopulateComboParentBranches(GetSQLData getData)
        {
            // This is used on the insert Instance form
            IEnumerable<TreeItem> allMods = getData.GetTreeViewModules(0);
            var results = (from m in allMods.Distinct()
                        select (new ListViewItem
                        {
                            Text = m.DisplayText,
                            Name = m.ModuleID.ToString()
                        })).ToList();

            results.Insert(0, new ListViewItem { Text = "Please Select", Name = "0" });

            return results;
        }



    }
}

