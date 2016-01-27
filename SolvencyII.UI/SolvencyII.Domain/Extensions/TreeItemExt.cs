using System;
using SolvencyII.Domain.Entities;

namespace SolvencyII.Domain.Extensions
{
    /// <summary>
    /// Extension for treeItem
    /// Calculates the class name
    /// </summary>
    public static class TreeItemExt
    {
        public static string GetClassName(this TreeItem treeItem, bool controlName)
        {
            string result;
            result = String.Format("{0}__{1}__{2}", treeItem.TableCode.Replace(".", "_"), treeItem.TaxonomyCode, treeItem.Version);
            if (controlName) result += "_row";
            result = result.Replace(" ", "");
            result = result.Replace(".", "_");
            result = result.Replace("-", "_");
            return result;
        }
    }
}
