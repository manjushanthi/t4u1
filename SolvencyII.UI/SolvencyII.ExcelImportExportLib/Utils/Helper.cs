using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SolvencyII.Domain;
using SolvencyII.Domain.Configuration;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Interfaces;

namespace SolvencyII.ExcelImportExportLib.Utils
{
    public static class Helper
    {
        public static string GetTableName(mTable table)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("T__");
            sb.Append(table.TableID);
            sb.Append("_");
            sb.Append(table.TableCode.Replace('.', '_'));

            return sb.ToString();
        }

        //‘T__’ + mTable.TableCode +  ’__’ + mTaxonomy.TaxonomyCode  +  ‘__’ + mTaxonomy .TaxVersion
        public static string GetTableName(mTaxonomy taxonomy, mTable table)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("T__");
            sb.Append(table.TableCode.Replace('.', '_'));
            sb.Append("__");
            sb.Append(taxonomy.TaxonomyCode.Trim());
            sb.Append("__");
            sb.Append(taxonomy.Version.Replace('.', '_'));

            return sb.ToString();
        }

        public static Type ReferencedLookup(string className)
        {
            // See if the class is referenced directly
            Type result = null;

            string codeBase = typeof(Helper).Assembly.CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            Assembly assemblyTemp = null;
            Assembly assemblyTempPreparatory = null;
            switch (StaticSettings.DbType)
            {
                case DbType.SolvencyII:                    
                    assemblyTemp = Assembly.LoadFile(Path.GetDirectoryName(path) + "\\SolvencyII.Extensibility_SOL2.dll");
                    break;
               
                case DbType.SolvencyII_Preparatory:
                    assemblyTempPreparatory = Assembly.LoadFile(Path.GetDirectoryName(path) + "\\SolvencyII.Extensibility_SOL2_Prep.dll");
                    break;
                default:
                    result =null;
                    break;
            }
            
             
             //Assembly assemblyTempPreparatory = Assembly.LoadFile(Path.GetDirectoryName(path) + "\\SolvencyII.Extensibility_1.0_SOL2_Preparatory.dll");
             
            /*result = ReferencedDLLLookup("SolvencyII.Extensibility_1.0_EBA", className);
             if (assemblyTemp == null) result = ReferencedDLLLookup("SolvencyII.Extensibility_1.0_SOL2", className);
             if (result == null) result = ReferencedDLLLookup("SolvencyII.Extensibility_1.0_SOL2_Preparatory", className);
             if (result == null) result = ReferencedDLLLookup("SolvencyII.Extensibility_1.0_POC", className);*/


            
                try
                {
                    if (assemblyTempPreparatory != null)
                    {
                        var myClass = assemblyTempPreparatory.GetTypes().FirstOrDefault(t => t.Name == className);                    
                        if (myClass != null)
                        {
                            return myClass;
                        }
                    }

                }
                catch (ReflectionTypeLoadException ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }
            

            
                 try
                 {
                      if (assemblyTemp != null)
                      {
                         var myClass = assemblyTemp.GetTypes().FirstOrDefault(t => t.Name == className);                    
                         if (myClass != null)
                         {
                             return myClass;
                         }
                      }

                 }
                 catch (ReflectionTypeLoadException ex)
                 {
                     Console.WriteLine(ex);                     
                     return null;
                 }
                 catch (Exception e)
                 {
                     Console.WriteLine(e);
                     return null;
                 }

            return result;
        }


       
    }
}
