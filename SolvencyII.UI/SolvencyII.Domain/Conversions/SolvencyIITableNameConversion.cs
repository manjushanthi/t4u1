namespace SolvencyII.Domain.Conversions
{
    /// <summary>
    /// The concrete table names found in the db differ from the Mappings table - here is a single point of change to reflect that.
    /// </summary>
    public static class SolvencyIITableNameConversion
    {
        public static string FullDbName(string dynTableName)
        {
            return string.Format("T__{0}", dynTableName);
        }
    }
}
