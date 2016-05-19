using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.Domain.Entities;

namespace ucGenerator
{
    /// <summary>
    /// This is where the project file is created
    /// </summary>
    public class CreateProject
    {
        private Exception _exception;


        public bool Create(string fileName, List<string> files, string version)
        {
            try
            {
                CreateWinFormProjectFile(fileName, files, version);
                return true;
            }
            catch (Exception ex)
            {
                _exception = ex;
                return false;
            }
        }

        private static void CreateWinFormProjectFile(string fileName, List<string> files, string version)
        {
            // Project File
            if (File.Exists(fileName)) File.Delete(fileName);
            // Make the project file itself
            using (FileStream fs = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(Project.GenerateCode(files, version));
                fs.Flush();
            }
            // Make the AssemblyInfo.cs 
            string assemblyInfoPath = Path.GetDirectoryName(fileName);
            assemblyInfoPath = Path.Combine(assemblyInfoPath, @"Properties\");
            if (!Directory.Exists(assemblyInfoPath)) Directory.CreateDirectory(assemblyInfoPath);
            assemblyInfoPath = Path.Combine(assemblyInfoPath, "AssemblyInfo.cs");
            if (File.Exists(assemblyInfoPath)) File.Delete(assemblyInfoPath);
            using (FileStream fs = new FileStream(assemblyInfoPath, FileMode.CreateNew, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(Project.GenerateInfo());
                fs.Flush();
            }

            string readmePath = Path.GetDirectoryName(fileName);
            readmePath = Path.Combine(readmePath, "readme.txt");
            if (File.Exists(readmePath)) File.Delete(readmePath);
            using (FileStream fs = new FileStream(readmePath, FileMode.CreateNew, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(Project.GenerateReadme());
                fs.Flush();
            }
        }

        public Exception Error()
        {
            return _exception;
        }
    }
}
