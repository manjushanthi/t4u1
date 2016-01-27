using System.Collections.Generic;

namespace SolvencyII.Domain.Entities
{
    public class TreeBranch : TreeItem
    {
        public List<TreeBranch> SubBranches;

        public TreeBranch()
        {
            SubBranches = new List<TreeBranch>();
        }

        // Three fields below used in flattened tree
		//public int BranchID { get; set; }
		//public bool HasBranches { get; set; }
		//public int ParentBranchID { get; set; }

        public string TableGroupCode2 { get; set; }
        public int TableGroupID2 { get; set; }

    }
}
