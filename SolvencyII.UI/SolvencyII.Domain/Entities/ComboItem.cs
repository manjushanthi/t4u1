namespace SolvencyII.Domain.Entities
{
    public class ComboItem
    {
        // Display Info
        public string Text { get; set; }
        // Reference Info
        public string Value { get; set; }
        // Used in multiplatform
        public bool Bold { get; set; }
        public int? IntValue { set { if (value != null) Value = value.ToString(); } }
        public bool IsAbstract { get; set; }
        public bool Include { get; set; }
        public bool? IsStartingMemberIncluded { get; set; }
        public int Level { get; set; }
        public int Order { get; set; }
    }
}
