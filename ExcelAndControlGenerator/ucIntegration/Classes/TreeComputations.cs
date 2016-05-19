using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SolvencyII.Domain.Entities;

namespace ucIntegration.Classes
{
    public class TreeComputations
    {
        private TreeBranch _trunk;
        public TreeComputations(TreeBranch trunk)
        {
            _trunk = trunk;
        }

        public List<TreeBranch> TemplatesInATree()
        {
            List<TreeBranch> templates = new List<TreeBranch>();
            foreach (TreeBranch branch in _trunk.SubBranches)
            {
                if (branch.SubBranches.Count == 0)
                {
                    if (branch.TableID != 0)
                        templates.Add(branch);
                }
                else
                    IterativeTemplateLocator(templates, branch);
            }
            return templates;
        }

        private void IterativeTemplateLocator(List<TreeBranch> templates, TreeBranch branch)
        {
            if (branch.FilingTemplateOrTableID != 0 && branch.SubBranches.Count > 1)
            {
                // Generation of super template required.
                // Debug.WriteLine("Made it here.");
                branch.TemplateVariant = branch.SubBranches[0].TemplateVariant;
                branch.HasBranches = true;
                templates.Add(branch);
            }


            foreach (TreeBranch subBranch in branch.SubBranches)
            {
                if (subBranch.SubBranches.Count == 0)
                {
                    if (subBranch.TableID != 0)
                        templates.Add(subBranch);
                }
                else 
                    IterativeTemplateLocator(templates, subBranch);
            }
        }

        public static int CountTemplatesInTree(TreeBranch trunk)
        {
            // Find all entries where item.SubBranches.Count == 0 AND  !item.IsTyped
            int count = 0;
            foreach (TreeBranch branch in trunk.SubBranches)
            {
                if (branch.SubBranches.Count == 0)
                {
                    if (branch.TableID != 0)
                        count++;
                }
                else
                {
                    count = InterativeCounter(count, branch);
                }
            }
            return count;
        }

        private static int InterativeCounter(int count, TreeBranch branch)
        {
            // Count this branch and its subbranches etc.
            foreach (TreeBranch subBranch in branch.SubBranches)
            {
                if (subBranch.SubBranches.Count == 0)
                {
                    if (subBranch.TableID != 0)
                        count++;
                }
                else
                {
                    // If not Nested SubBranches add item
                    if (subBranch.SubBranches.Count != 1 && !subBranch.SubBranches.Any(b => b.SubBranches.Count > 0))
                        count++;
                    count = InterativeCounter(count, subBranch);
                }
            }
            return count;
        }

        public static IEnumerable<TreeBranch> GetTemplatesWhere(TreeBranch branch, Func<TreeBranch, bool> whereClause)
        {
            List<TreeBranch> templates = branch.SubBranches.Where(whereClause).ToList();
            foreach (TreeBranch template in templates)
            {
                templates.AddRange(IterativeTemplatesWhere(template, whereClause));
            }
            return templates;
        }

        private static IEnumerable<TreeBranch> IterativeTemplatesWhere(TreeBranch template, Func<TreeBranch, bool> whereClause)
        {
            List<TreeBranch> templates = template.SubBranches.Where(whereClause).ToList();
            foreach (TreeBranch branch in templates)
            {
                templates.AddRange(IterativeTemplatesWhere(branch, whereClause));
            }
            return templates;
        }
    }
}
