using System;
using System.Collections.Generic;
using SolvencyII.Domain.Entities;

namespace SolvencyII.Domain.Interfaces
{
    public interface IComboBoxZAxis
    {
        int AxisID { get; set; }
        ComboItemType TypeOfItems { get; set; }
        ComboSelection GetComboSelection { get; }
        string SpecifiedSelection { get; set; }
        bool AutomaticHide { get; set; }
        string DomainCode { get; set; }
        string DimensionCode { get; set; }
        string OwnerPrefix { get; set; }
        void PopulateWithComboItems(List<ComboItem> data);
        void SetSelectedIndexChanged(EventHandler selectedIndexChanged);
        int ItemCount();
        void SelectFirstEntry();
    }
}
