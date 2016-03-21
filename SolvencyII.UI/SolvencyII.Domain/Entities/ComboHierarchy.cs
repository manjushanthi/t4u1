namespace SolvencyII.Domain.Entities
{
    public class ComboHierarchy
    {
        public long OrdinateID { get; set; }
        public int HierarchyID { get; set; }
        public int HierarchyStartingMemberID { get; set; }
        public bool IsStartingMemberIncluded { get; set; }
    }
}
