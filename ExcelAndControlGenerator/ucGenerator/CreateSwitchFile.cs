using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ucGenerator
{
    /// <summary>
    /// Used within iOS application to switch between templates.
    /// </summary>
    public class CreateSwitchFile
    {
        private Exception _exception;


        public bool Create(string fileName, List<string> files, string version)
        {
            try
            {
                CreateFile(fileName, files, version);
                return true;
            }
            catch (Exception ex)
            {
                _exception = ex;
                return false;
            }
        }

        private static void CreateFile(string fileName, List<string> files, string version)
        {
            // File
            if (File.Exists(fileName)) File.Delete(fileName);
            // Make the file itself
            using (FileStream fs = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(GenerateCode(files));
                fs.Flush();
            }
        }

        private static string GenerateCode(List<string> files)
        {
            StringBuilder sb = new StringBuilder();

            // Top:
            sb.AppendLine("using System; ");
            sb.AppendLine("using SolvencyII.iOS.Lib; ");
            sb.AppendLine("using System.Linq; ");
            sb.AppendLine(" ");
            sb.AppendLine("namespace SolvencyII.iOS.Templates ");
            sb.AppendLine("{ ");
            sb.AppendLine("	public class TemplateIndex ");
            sb.AppendLine("	{ ");
            sb.AppendLine("		public TemplateBase LocateTemplate(int tableVID, string frameworkCode, int singleZOrdinateID, string tableCode ) ");
            sb.AppendLine("		{ ");
            sb.AppendLine(@"			string className = string.Format(""{0}_{1}_{2}_{3}"", frameworkCode, tableCode, tableVID, singleZOrdinateID); ");
            sb.AppendLine(@"			className = className.Replace('.', '_').Replace("" "", """"); ");
            sb.AppendLine("			try{return (TemplateBase) SwitchTemplate(className);} ");
            sb.AppendLine("			catch {return null;} ");
            sb.AppendLine("		} ");
            sb.AppendLine(" ");
            sb.AppendLine("		private TemplateBase SwitchTemplate(string className) ");
            sb.AppendLine("		{ ");
            sb.AppendLine("			TemplateBase result; ");
            
            sb.AppendLine("			switch (className) { ");

            foreach (string file in files)
            {
                string className = Path.GetFileNameWithoutExtension(file).Replace(" ", "").Replace(".", "_");
                sb.AppendLine(string.Format(@"			case ""{0}"": result = new {0}(); break; ", className));
            }

            sb.AppendLine("			default:  break; ");
            sb.AppendLine("			} ");
            sb.AppendLine("			return result; ");


            /*
             			TemplateBase result;

			switch (className) { 
			case "FR_MD_S_02_01_03_1_101367_0": result = new FR_MD_S_02_01_03_1_101367_0 (); break;
			case "S2HD_S_02_01_04_1_2671_0": result = new S2HD_S_02_01_04_1_2671_0 (); break;
			default: 
				break;
			}

			return result;
             */




            sb.AppendLine("		} ");
            sb.AppendLine(" ");
            sb.AppendLine("	} ");
            sb.AppendLine("} ");
            sb.AppendLine(" ");

            return sb.ToString();
        }




        private void SwithFiles(StringBuilder sb)
        {


        }

        private void SwithBottom(StringBuilder sb)
        {


        }



        public Exception Error()
        {
            return _exception;
        }
    }
}
