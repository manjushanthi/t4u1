using System;
using System.Collections.Generic;
using System.Linq;
using SolvencyII.Data.Shared;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Extensions;
using SolvencyII.Domain.Interfaces;

namespace SolvencyII.UI.Shared.Managers
{
    /// <summary>
    /// Used to populate combos.
    /// </summary>
    public static class PopulateNPageCombos
    {
        public static void PopulateComboUserControls(GetSQLData getData, List<ISolvencyComboBox> cntrls, int languageID)
        {
            foreach (ISolvencyComboBox solvencyComboBox in cntrls)
            {
                if(solvencyComboBox.IsPopulated())continue;
                long hierarchyID = solvencyComboBox.HierarchyID;
                if (hierarchyID == 0)
                {
                    long ordinateID = solvencyComboBox.OrdinateID;
                    List<ComboItem> popData = null;
                    if (ordinateID != 0)
                    {
                        popData = getData.GetOrdinateComboItems(solvencyComboBox.OrdinateID, languageID);
                    }
                    if (popData == null || !popData.Any())
                    {
                        long axisID = solvencyComboBox.AxisID;
                        popData = getData.GetzAxisMemberComboItems(axisID, languageID,  solvencyComboBox.StartOrder, solvencyComboBox.NextOrder);
                    }
                    if (popData.Count > 1) popData.Insert(0, new ComboItem { Value = "", Text = "Please select", Include = true });
                    solvencyComboBox.TypeOfItems = ComboItemType.AxisOrdinates;
                    solvencyComboBox.PopulateWithComboItems(popData, "");

                }
                else
                {
                    List<OpenComboItem> items = getData.HierarchyLookup2(hierarchyID, solvencyComboBox.StartOrder, solvencyComboBox.NextOrder, languageID, solvencyComboBox.OrdinateID);
                    if (!items.Any())
                    {
                        List<ComboItem> memComboItems = getData.GetzAxisMemberComboItems(solvencyComboBox.AxisID, languageID, solvencyComboBox.StartOrder, solvencyComboBox.NextOrder);
                        foreach (ComboItem comboItem in memComboItems)
                        {
                            items.Add(comboItem.ConvertToOpenComboItem());
                        }
                    }
                    if (items.Count > 1) items.Insert(0, new OpenComboItem {Name = "", Text = "Please select", Include = true});
                    solvencyComboBox.TypeOfItems = ComboItemType.HierarchyID;
                    solvencyComboBox.PopulateWithHierarchy(items, "");
                }
            }
        }

        public static void PopulateComboUserControls(GetSQLData getData, List<ISolvencyDataComboBox> dataCombos, int languageID)
        {
            // Using a cache to speed up the iteration - especially for merged templates.
            long cacheOrdinateID = 0;
            long cacheAxisID = 0;
            List<ComboItem> cachedPopData = null;

            long cacheHierarchyID = 0;
            List<OpenComboItem> cachedItems = null;

            foreach (ISolvencyDataComboBox solvencyComboBox in dataCombos)
            {
                if(solvencyComboBox.IsPopulated()) continue;
                long hierarchyID = solvencyComboBox.HierarchyID;
                if (hierarchyID == 0)
                {
                    long ordinateID = solvencyComboBox.OrdinateID;
                    if (ordinateID != 0)
                    {
                        if (cacheOrdinateID == ordinateID)
                        {
                            // Use items from the cached control
                            solvencyComboBox.PopulateWithComboItems(cachedPopData);
                            continue;
                        }
                        List<ComboItem> popData = getData.GetOrdinateComboItems(solvencyComboBox.OrdinateID, languageID);
                        if (!popData.Any())
                        {
                            long axisID = solvencyComboBox.AxisID;
                            if (cacheAxisID == axisID)
                            {
                                // Use items from the cached control
                                solvencyComboBox.PopulateWithComboItems(cachedPopData);
                                continue;
                            }
                            popData = getData.GetzAxisMemberComboItems(axisID, languageID, solvencyComboBox.StartOrder, solvencyComboBox.NextOrder);
                            cacheAxisID = axisID;
                            cacheOrdinateID = 0;
                        }
                        else
                        {
                            cacheAxisID = 0;
                            cacheOrdinateID = ordinateID;
                        }
                        
                        if (popData.Count > 1) popData.Insert(0, new ComboItem {Value = "", Text = "Please select"});
                        solvencyComboBox.TypeOfItems = ComboItemType.AxisOrdinates;
                        
                        // Capture the cache
                        cachedPopData = popData;

                        solvencyComboBox.PopulateWithComboItems(popData);
                    }
                }
                else
                {
                    if (cacheHierarchyID == hierarchyID)
                    {
                        solvencyComboBox.PopulateWithHierarchy2(cachedItems);
                        continue;
                    }


                    List<OpenComboItem> items = getData.HierarchyLookup2(hierarchyID, solvencyComboBox.StartOrder, solvencyComboBox.NextOrder, languageID, solvencyComboBox.OrdinateID);
                    if (!items.Any())
                    {
                        List<ComboItem> memComboItems = getData.GetzAxisMemberComboItems(solvencyComboBox.AxisID, languageID, solvencyComboBox.StartOrder, solvencyComboBox.NextOrder);
                        foreach (ComboItem comboItem in memComboItems)
                        {
                            items.Add(comboItem.ConvertToOpenComboItem());
                        }
                    }
                    if (items.Count > 1) items.Insert(0, new OpenComboItem { Name = "", Text = "Please select" });
                    solvencyComboBox.TypeOfItems = ComboItemType.HierarchyID;
                    solvencyComboBox.PopulateWithHierarchy2(items);

                    // Capture the cache
                    cacheHierarchyID = hierarchyID;
                    cachedItems = items;
                }
            }
        }

        public static void PopulateCombosNPage(GetSQLData getData, long instanceID, List<string> getDataTables, int languageID, List<ISolvencyComboBox> midStep, Dictionary<string, string> startingEntries, EventHandler onCombosOnSelectedIndexChanged, EventHandler onComboBoxOnDropDown, EventHandler onCombosOnLostFocus, EventHandler onGotFocus)
        {
            foreach (ISolvencyComboBox combo in midStep)
            {
                List<ComboItem> popData = new List<ComboItem>();
                if (combo.AxisID != 0)
                {
                    // Get OrdinateID for z dimension where the MemberID != 9999
                    List<ComboItem> memComboItems = getData.GetzAxisMemberComboItems(combo.AxisID, languageID, combo.StartOrder, combo.NextOrder);

                    if (memComboItems.Count > 1)
                    {
                        // We need to look up the Hierachry and populate popData
                        //List<ComboItem> memComboItems = getData.GetzAxisMemberComboItems(combo.AxisID, LanguageID);

                        // If there is only one there is no selection to be made
                        if (memComboItems.Count > 1) popData.Add(new ComboItem {Value = "", Text = "Please select"});
                        popData.AddRange(memComboItems);
                        combo.TypeOfItems = ComboItemType.MemberItems;
                    }
                    else
                    {
                        List<ComboItem> ordComboItems = getData.GetzAxisOrdinateComboItems(combo.AxisID, languageID);
                        // If there is only one there is no selection to be made
                        if (ordComboItems.Count > 1) popData.Add(new ComboItem {Value = "-1", Text = "Please select"});

                        // Populate popData from ordinates
                        popData.AddRange(ordComboItems);
                        combo.TypeOfItems = ComboItemType.AxisOrdinates;
                    }
                    if (!popData.Any())
                    {
                        // We have a Special Case combo.
                    }
                    
                    combo.PopulateWithComboItems(popData, startingEntries[combo.ColName]);
                    combo.SetSelectedIndexChanged(onCombosOnSelectedIndexChanged);
                    combo.SetOnDropDown(onComboBoxOnDropDown);
                    if (onCombosOnLostFocus != null)
                        combo.SetOnLostFocus(onCombosOnLostFocus);
                }
                else
                {
                    // The text here corresponds to that added in UserControlBase.PageCombosCheck
                    // OpenUserControlBase2.SetPanelChange
                    // OpenUserControlBase2.PageCombosCheck

                    // Here we have a TextComboBox
                    List<ComboItem> memComboItems = getData.GetNPageTextComboItems(combo.ColName, instanceID, getDataTables);
                    popData.Add(new ComboItem { Value = "", Text = "Please select or press add button" });
                    if (memComboItems.Any())
                    {
                        popData.AddRange(memComboItems);
                    }
                    combo.PopulateWithComboItems(popData, startingEntries[combo.ColName]);
                    combo.SetSelectedIndexChanged(onCombosOnSelectedIndexChanged);
                    if (onCombosOnLostFocus != null)
                        combo.SetOnLostFocus(onCombosOnLostFocus);
                }
            }
        }
    }
}
