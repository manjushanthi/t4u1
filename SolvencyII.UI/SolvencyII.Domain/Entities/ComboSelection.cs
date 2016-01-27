namespace SolvencyII.Domain.Entities
{
    public class ComboSelection
    {
        public string OwnerPrefix { get; set; }
        public string DimensionCode { get; set; }
        public string DomainCode { get; set; }

        public int AxisID { get; set; }
        public ComboItemType TypeOfItems { get; set; }
        public string SelectedItemID { get; set; }

    }

    public enum ComboItemType
    {
        AxisOrdinates,
        MemberItems,
        TextEntries,
        HierarchyID
    }
}
