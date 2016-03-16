using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace T4U.CRT.Generation.Model
{
    public class PageColumn : IRelationalColumn
    {
        internal string dimCode;
        internal readonly HashSet<string> memCodes = new HashSet<string>();
        internal string index;
        //private bool _isInTable = false;

        public void setIndex(string indexValue)
        {
            if (!string.IsNullOrWhiteSpace(index))
                throw new ArgumentException("Index already set");

            this.index = indexValue;
        }

        public string getColumnName()
        {
            return "PAGE" + index;
        }

        public override string ToString()
        {
            return getColumnName();
        }

        public string DimCode
        {
            get { return this.getColumnMappingDimCode(); }
        }

        public string getColumnMappingDimCode()
        {
            string code = this.dimCode + "(";

            if (memCodes.Count > 1)
                memCodes.ToList().ForEach(x => { code = code + x + "|"; });
            else if (memCodes.Count == 1)
                code = code + memCodes.ToList()[0];
            else
                code = code + "*";

            code = code + ")";            
            
            return code;            
        }

        public string getMemberCodeForDimCode(string mappingCode)
        {
            if(mappingCode.StartsWith(this.dimCode))
                foreach (string memCode in this.memCodes)
                    if (mappingCode.Equals(string.Format("{0}({1})", this.dimCode, memCode)))
                        return memCode;

            if(!mappingCode.Contains("*"))
                throw new KeyNotFoundException(mappingCode + " not found ");
            return "";
        }

        public HashSet<string> getColumnMappingDimCodes()
        {
            HashSet<string> retVal = new HashSet<string>();

            if (memCodes.Count > 0)
                memCodes.ToList().ForEach(x => retVal.Add(dimCode + "(" + x + ")"));
            else
                retVal.Add(dimCode + "(*)");

            return retVal;
        }

        public string getOrigin()
        {
            return ProcesorGlobals.ContextOrigin;
        }

        public int getRequiredMappingsNumber()
        {
            return 1;
        }

        public IRelationColumnDataType getDataType()
        {
            return IRelationColumnDataType.String;
        }

        public IEnumerable<string> getColumnMappingDimKeyCodes()
        {
            if (memCodes.Count == 1)          
                return new HashSet<string>();

            HashSet<string> hs = new HashSet<string>();
            hs.Add(this.dimCode + "(*)");
            return hs;
        }

        
        public bool isPageColumn
        {
            get
            {
                return true;
            }
        }
        public bool isKeyColumn
        {
            get
            {
                return false;
            }
        }
        
        public int getRequiredMappingsNumber(ClassicRelationalTable _table)
        {
            return _table.Columns.Where(x => x != null && x.getColumnName().Equals(this.getColumnName()))
                .Select(x=>
                    {
                        string dimCode =  x.getColumnMappingDimCode();
                        int idx = dimCode.IndexOf("(");
                        return dimCode.Substring(0, idx);
                    })
                .Distinct()
                .Count();
        }

        public bool isInTable()
        {
            //if (_isInTable)
            //    return true;

            if (memCodes.Count == 1)
                return false;

            return true;
        }

        public bool isInTable(ClassicRelationalTable _table)
        {
            var similarColumns = _table.Columns.Where(x=>x.getColumnName().Equals(this.getColumnName()));
            
            if(similarColumns.Count() > 1 && similarColumns.Select(x=>x.getColumnMappingDimCode()).Distinct().Count() == 1) 
                return false;

            if (similarColumns.Select(x=>x.getColumnMappingDimCode()).Distinct().Count() > 1)            
                return true;

            return this.isInTable();
        }

        internal void forceIsInTable()
        {
            //_isInTable = true;
        }

        Dictionary<string, string> dimCodeToDomCode = new Dictionary<string, string>();
        public string getDomCodeForDimCode(string dimCode)
        {
            string domCode = "";
            if (dimCodeToDomCode.TryGetValue(dimCode, out domCode))
                return domCode;
            if (dimCodeToDomCode.TryGetValue(dimCode.Replace("(*)", ""), out domCode))
                return domCode;

            return "";
        }

        
        
        public void addDomCode(string domCode, string dimCode)
        {
            if(!dimCodeToDomCode.ContainsKey(dimCode))
                dimCodeToDomCode.Add(dimCode, domCode);
        }

        public string getColumnNameForExcelTemplate()
        {
            return string.Empty;
        }

        public string getRowNameForExcelTemplate()
        {
            return string.Empty;
        }

        public int getHierarchyIdForExcelTemplate()
        {
            return 0;
        }

        public string getHierarchyIdForExcelTemplateWithMetrics()
        {
            return string.Empty;
        }
        


        
    }
}
