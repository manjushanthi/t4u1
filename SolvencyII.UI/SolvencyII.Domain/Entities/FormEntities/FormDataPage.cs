namespace SolvencyII.Data.Entities
{
    /// <summary>
    /// Used mainly in the management of PAGEn data; fields within a table that vary, requireing a separate row for each setting.
    /// </summary>
    public class FormDataPage
    {
        public string DYN_TABLE_NAME { get; set; }
        public string DYN_TAB_COLUMN_NAME { get; set; }
        public string Value { get; set; }
        public long TableID { get; set; }
        public long? AxisID { get; set; }
        public bool FixedDimension { get; set; }
        public bool IsTypedDimension { get; set; }
        public string DOM_CODE { get; set; }
    }
}
