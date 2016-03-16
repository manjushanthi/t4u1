using System.Collections.Generic;
using SolvencyII.Domain.Entities;

namespace SolvencyII.Domain.Interfaces
{
    /// <summary>
    /// Interface for data combo box
    /// </summary>
    public interface ISolvencyDataComboBox : ISolvencyDataControl
    {
        long AxisID { get; set; }
        ComboItemType TypeOfItems { get; set; }
        string GetValue { get; }
        long OrdinateID { get; set; }
        long HierarchyID { get; set; }
        void PopulateWithComboItems(List<ComboItem> popData);
        void PopulateWithHierarchy2(List<OpenComboItem> items);
        bool IsPopulated();

        long StartOrder { get; set; }
        long NextOrder { get; set; }
        
    }
}
