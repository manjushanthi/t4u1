namespace SolvencyII.Domain.Entities
{
    public class OpenComboItem
    {
        public string Name { get; set; } // What the data signature is
        public string Text { get; set; } // What is shown in a combo
        public bool IsAbstract { get; set; } // Parents can not be selected.
        public bool Include { get; set; }
    }
}
