namespace SolvencyII.Domain.Entities
{
    public class ComboTag
    {
        #region Constructor

        public ComboTag()
        {
        }

        public ComboTag(bool isAbstract)
        {
            IsAbstract = isAbstract;
        }

        #endregion

        public bool IsAbstract { get; set; }
    }
}
