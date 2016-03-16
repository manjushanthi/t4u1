using System.Collections.Generic;
using System.Windows.Forms;
using SolvencyII.Data.Shared;
using SolvencyII.Domain.Entities;

namespace SolvencyII.UI.Shared.Extensions
{
    /// <summary>
    /// Extension of TreeView
    /// </summary>
    public static class TreeViewExt
    {

        public static void Populate(this TreeView treeView1, string connectionString, int instanceID = 0)
        {
            using (GetSQLData getData = new GetSQLData(connectionString))
            {
                Populate(treeView1, getData, instanceID);
            }
        }
        public static void Populate(this TreeView treeView1, long instanceID = 0)
        {
            using (GetSQLData getData = new GetSQLData())
            {
                Populate(treeView1, getData, instanceID);
            }
        }
        public static void Populate(this TreeView treeView1, GetSQLData getData, long instanceID = 0)
        {
            treeView1.Nodes.Clear();
            TreeBranch branch = getData.GetTree(instanceID);
            TreeNode[] nodes = ManageTreeNotes(branch);
            treeView1.Nodes.AddRange(nodes);
        }

        public static TreeBranch GetTrunck(this TreeView treeView1)
        {
            TreeBranch result = new TreeBranch();
            result.DisplayText = "Root";
            foreach (TreeNode node in treeView1.Nodes)
            {
                TreeBranch branch = (TreeBranch) node.Tag;
                result.SubBranches.Add(branch); 
            }
            return result;
        }
       
        private static TreeNode[] ManageTreeNotes(TreeBranch branch)
        {
            List<TreeNode> result = new List<TreeNode>();
            // There are only three levels so:
            foreach (TreeBranch treeBranch in branch.SubBranches)
            {
                TreeNode tnBranch = ProcessBranch(treeBranch);
                result.Add(tnBranch);
            }

            return result.ToArray();
        }
        private static TreeNode ProcessBranch(TreeBranch treeBranch)
        {
            TreeNode tnBranch = new TreeNode();
            tnBranch.Text = treeBranch.DisplayText;
            tnBranch.ToolTipText = treeBranch.DisplayText;
            tnBranch.Tag = treeBranch;
            foreach (TreeBranch treeSubBranch in treeBranch.SubBranches)
            {
                TreeNode tnSubBranch = ProcessBranch(treeSubBranch);
                tnBranch.Nodes.Add(tnSubBranch);
            }

            //tnBranch.ImageIndex = 1;
            return tnBranch;
        }


        /*
                 
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(e.Node.Tag != null)
            {
                TreeBranch item = (TreeBranch) e.Node.Tag;
                if(item.SubBranches.Count == 0)
                {
                    // Action required

                    string msg = "Open user control ({2} table) for TableGroupIDs {0}\n\n{1}\n\n{3}";
                    MessageBox.Show(string.Format(msg, item.GroupTableIDs, item.DisplayText, item.IsTyped ? "open" : "close", item.SingleZOrdinateID != 0 ? "zOrdinate " + item.SingleZOrdinateID : ""));
                }
                Console.WriteLine(item.DisplayText);
            }
        }
         */
    }
}
