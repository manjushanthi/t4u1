namespace SolvencyII.Domain.Entities
{
    public class AxisOrdinateControls 
    {
        public int OrdinateID { get; set; }
        public string OrdinateLabel { get; set; }
        public string AxisOrientation { get; set; }
        public int AxisID { get; set; } 
        public int Order { get; set; }
        public int Level { get; set; }
        public int TableID { get; set; }
        public bool IsAbstractHeader { get; set; }
        public int ParentOrdinateID { get; set; }
        public bool IsDisplayBeforeChildren { get; set; }
        public string OrdinateCode { get; set; }
        public string DataType { get; set; }
        public int ParentOrder { get; set; }
        public bool IsOpenAxis { get; set; }
        public string SpecialCase { get; set; }
        public bool IsRowKey { get; set; }
        public long StartOrder { get; set; }
        public long NextOrder { get; set; }
        public string DimXbrlCode { get; set; }
        
        
        public int LabelWidth { get; set; }
        public int TextWidth { get; set; }
        public int ColSpan { get; set; }

        public string TopBranchOrder { get; set; }
        // Level = 1, Order = 3 => 0103

    }
}
