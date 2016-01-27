using System;
using System.Collections.Generic;
using SolvencyII.Domain.Entities;

namespace SolvencyII.Domain.Interfaces
{
    /// <summary>
    /// Interface for SolvencyComboBox
    /// </summary>
    public interface ISolvencyComboBox : ISolvencyPageControl
    {
        long AxisID { get; set; }
        ComboItemType TypeOfItems { get; set; }
        string GetValue { get; }
        string SetValue { set; }
        long OrdinateID { get; set; }
        long HierarchyID { get; set; }
        long StartOrder { get; set; }
        long NextOrder { get; set; }
        bool IsRowKey { get; set; }
        object GetBackColor { get; }
        bool Enabled { get; set; }
        bool IsPopulated();
        
        void PopulateWithComboItems(List<ComboItem> popData, string selectedValue);
        void PopulateWithHierarchy(List<OpenComboItem> items, string selectedValue);
        
        void SetSelectedIndexChanged(EventHandler selectedIndexChanged);
        void SetOnDropDown(EventHandler onComboBoxOnDropDown);
        void SetOnLostFocus(EventHandler onComboBoxOnDropDown);
        void SetOnGotFocus(EventHandler onComboBoxOnGotFocus);

        void SetPrevious();
        bool IsValid();

    }
}
