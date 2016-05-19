using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ucGenerator
{
    public static class Project
    {
        public static string GenerateCode(List<string> files, string version)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Properties.Resources.ProjectStart1);
            sb.AppendLine(string.Format(@"    <ProjectGuid>{{{0}}}</ProjectGuid>", Guid.NewGuid().ToString()));
            sb.AppendLine(Properties.Resources.ProjectStart2);

            sb.AppendLine(string.Format(@"    <AssemblyName>SolvencyII.Extensibility_{0}</AssemblyName>", version));

            sb.AppendLine(Properties.Resources.ProjectStart3);

            // Insert the files
            sb.AppendLine("<ItemGroup>");
            foreach (string file in files)
            {
                sb.AppendLine(string.Format(@"<Compile Include=""{0}""><SubType>UserControl</SubType></Compile>", file));
                sb.AppendLine(string.Format(@"<Compile Include=""{0}""><DependentUpon>{1}</DependentUpon></Compile>",  Path.ChangeExtension(file, "designer.cs"), file));
            }
            sb.AppendLine(@"<Compile Include=""Properties\AssemblyInfo.cs"" />");
            sb.AppendLine(@"<Content Include=""readme.txt"" />");
            sb.AppendLine("</ItemGroup>");

            // Project references are included in ProjectStart2.
            // They will need to be fixed by the user.


            sb.AppendLine(Properties.Resources.ProjectEnd);
            return sb.ToString();
        }

        public static string GenerateInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System.Reflection; ");
            sb.AppendLine("using System.Runtime.InteropServices; ");
            sb.AppendLine(@"[assembly: AssemblyTitle(""SolvencyUserControls"")] ");
            sb.AppendLine(@"[assembly: AssemblyDescription("""")] ");
            sb.AppendLine(@"[assembly: AssemblyConfiguration("""")]");
            sb.AppendLine(@"[assembly: AssemblyCompany(""EIOPA, www.profws.co.uk"")]");
            sb.AppendLine(@"[assembly: AssemblyProduct(""SolvencyUserControls"")]");
            sb.AppendLine(@"[assembly: AssemblyCopyright(""Copyright © EIOPA 2014"")]");
            sb.AppendLine(@"[assembly: AssemblyTrademark("""")]");
            sb.AppendLine(@"[assembly: AssemblyCulture("""")]");
            sb.AppendLine(@"[assembly: ComVisible(false)]");
            sb.AppendLine(string.Format(@"[assembly: Guid(""{0}"")]", Guid.NewGuid().ToString()));
            sb.AppendLine(@"[assembly: AssemblyVersion(""1.0.0.0"")]");
            sb.AppendLine(@"[assembly: AssemblyFileVersion(""1.0.0.0"")]");
            return sb.ToString();
        }

        public static string GenerateReadme()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("1) Check the references are correct. You may need to delete SolvencyII.Contracts, SolvencyII.UI.Shared & System.ComponentModel.Composition and add them again.");
            sb.AppendLine("2) Compile the program - in release.");
            sb.AppendLine("3) Copy the dll into the main application exe folder.");
            sb.AppendLine("4) Check the new user controls are working.");
            sb.AppendLine("5) Make your modifications and the repeat steps 2,3 & 4.");
            return sb.ToString();
        }
    }
}
