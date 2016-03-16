using System.Collections.Generic;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Interfaces;

namespace SolvencyII.Data.Shared.Entities
{
    public class OpenColInfo2 : ISolvencyCollectionMember
    {
        public string ColName { get; set; }
        public string ColType { get; set; }
        public int ColNumber { get; set; }
        public bool IsRowKey { get; set; }
        public string Label { get; set; }
        public string OrdinateCode { get; set; }
        public int AxisID { get; set; }
        public int OrdinateID { get; set; }
        
        public long StartOrder { get; set; }
        public long NextOrder { get; set; }

        // Needed for combobox;
        public int HierarchyID { get; set; }

        public List<OpenComboItem> Dimensions;
        
    }
}
