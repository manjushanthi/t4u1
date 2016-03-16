namespace SolvencyII.Domain.Entities
{
    // PLEASE NOTE
    // The field names must be indentical - they are case sensitive.
    //

    public class TreeItem 
    {
        private int _tableVid;
        // public int TableIDx { get; set; }
        public int TableID
        {
            get
            {
                if (_tableVid == 0 && !string.IsNullOrEmpty(GroupTableIDs))
                {
                    int.TryParse(GroupTableIDs.Split('|')[0], out _tableVid);
                }
                return _tableVid;
            }
            set { _tableVid = value; }
        }
        public string GroupTableIDs { get; set; }
        public bool IsTyped { get; set; }
        public int SingleZOrdinateID { get; set; }
        public int AxisID { get; set; }
        public string TableLabel { get; set; }
        public string FrameworkCode { get; set; }
        public string TaxonomyCode { get; set; }
        public string DisplayText { get; set; }
        public string TableCode { get; set; }
        public string TableGroupLabel { get; set; }
        public string LocationRange { get; set; }
        public long ModuleID { get; set; }
        public int TableGroupID { get; set; }
        public bool HasLocationRange { get; set; }
        public bool Merged { get; set; }
        public int TemplateOrTableID { get; set; }
        public int FilingTemplateOrTableID { get; set; }

		// Used with the flattened tree - multi platform
		public int BranchID{ get; set; }
		public int ParentBranchID{ get; set; }
		public bool HasBranches{ get; set; }
		public int ItemType{ get; set; }
        public string Version { get; set; }

        public string TemplateVariant { get; set; }
    }



    
}
