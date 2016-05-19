using System.Collections.Generic;
using System.Text;
using SolvencyII.Data.Shared.Entities;

namespace ucGenerator
{
    public static class MainClassOpen
    {
        public static string GenerateCode(string className, string groupTableVIDs, int tableVID, string frameworkCode, string version, string type, string table, int gridTop, List<OpenColInfo2> columnData, string userControlGeneratorVersion)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.ComponentModel.Composition;");
            sb.AppendLine("using SolvencyII.Data.Shared.Entities;");
            sb.AppendLine("using SolvencyII.Domain;");
            sb.AppendLine("using SolvencyII.Domain.Interfaces;");
            sb.AppendLine("using SolvencyII.UI.Shared.UserControls; ");
            sb.AppendLine();
            sb.AppendLine("namespace SolvencyII.UI.UserControls ");
            sb.AppendLine("{ ");
            sb.AppendLine("   [Export(typeof(ISolvencyUserControl))]");
            sb.AppendLine(string.Format("    public partial class {0} : OpenUserControlBase2, ISolvencyOpenUserControl ", className));
            sb.AppendLine("    { ");
            sb.AppendLine(string.Format("       // UserControlGenerator version: {0}", userControlGeneratorVersion));
            sb.AppendLine(string.Format("       public ISolvencyOpenUserControl Create {{get{{return new {0}();}}}}", className));
            sb.AppendLine("       public double Version { get; private set; }");
            sb.AppendLine("       public int TableVID { get; private set; }");
            sb.AppendLine("       public string FrameworkCode { get; private set; }");
            sb.AppendLine("       public int VersionT4U { get { return 2; } }");
            sb.AppendLine("       public Type DataType { get; private set; }");
            sb.AppendLine("       public string DataTable { get; private set; }");
            sb.AppendLine("       public List<ISolvencyCollectionMember> Columns { get; set; } ");
            sb.AppendLine("       public int GridTop { get; private set; } ");

            sb.AppendLine();

            sb.AppendLine(string.Format("       public {0}()", className));
            sb.AppendLine("       {");
            sb.AppendLine("           InitializeComponent();");
            sb.AppendLine(string.Format(@"           Version = {0};", version));
            sb.AppendLine(string.Format(@"           TableVID = {0};", tableVID));
            sb.AppendLine(string.Format(@"           FrameworkCode = ""{0}"";", frameworkCode));
            sb.AppendLine(string.Format(@"           DataType = {0};", type));
            sb.AppendLine(string.Format(@"           DataTable = {0};", table));
            sb.AppendLine(string.Format(@"           GridTop = {0};", gridTop));
            sb.AppendLine("           SetupColumns();");
            sb.AppendLine("       }");

            sb.AppendLine("       private void SetupColumns()");
            sb.AppendLine("       {");
            sb.AppendLine("           Columns = new List<ISolvencyCollectionMember>();");
            foreach (OpenColInfo2 c in columnData)
            {
                sb.AppendLine(string.Format(@"           Columns.Add(new OpenColInfo2 {{AxisID = {0}, ColType = ""{1}"", ColNumber = {2}, ColName = ""{3}"", HierarchyID = {4}, IsRowKey = {5}, Label = ""{6}"", OrdinateCode = ""{7}"", OrdinateID = {8}, StartOrder = {9}, NextOrder = {10}  }});", c.AxisID, c.ColType, c.ColNumber, c.ColName, c.HierarchyID, c.IsRowKey.ToString().ToLower(), c.Label.Replace("\n", ""), c.OrdinateCode, c.OrdinateID, c.StartOrder, c.NextOrder));
            }
            sb.AppendLine("       }");

            sb.AppendLine("       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } ");
            sb.AppendLine("       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } ");

            sb.AppendLine("   } ");
            sb.AppendLine("} ");

            return sb.ToString();
        }
    }
}
