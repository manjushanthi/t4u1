namespace SolvencyII.Domain.Entities
{
    public class DimXbrlCode
    {
        public string Prefix { get; set; }
        public string DimCode { get; set; }

        public DimXbrlCode(string dimXbrlCode, bool includesPAGE = false)
        {
            // Strip off the word PAGE if appropriate
            if (includesPAGE)
                dimXbrlCode = dimXbrlCode.Substring(4);
            int pos = dimXbrlCode.IndexOf("_");
            if (pos != -1)
            {
                Prefix = dimXbrlCode.Substring(0, pos); // mDimension.mConcept.mOwner.OwnerPrefix;
                DimCode = dimXbrlCode.Substring(pos + 1); // mDimension.DimensionCode;
            }
            else
            {
                Prefix = dimXbrlCode;
                DimCode = "";
            }
        }

        public override string ToString()
        {
            return string.Format("{0}_{1}", Prefix, DimCode);
        }

    }
}
