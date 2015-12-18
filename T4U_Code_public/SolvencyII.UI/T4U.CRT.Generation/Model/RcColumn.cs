using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using T4U.CRT.Generation.Model;

namespace T4U.CRT.Generation.Model
{
    public class RcColumn : IRelationalColumn
    {
        public RcColumn(string rowCode, string columnCode)
        {
            this.rowCode = rowCode;
            this.columnCode = columnCode;
        }


        public RcColumn(string rowCode, string columnCode, int hierarchyId, string rowName, string columnName, int HierarchyStartingMemberID, int IsStartingMemberIncluded)
        {
            this.rowCode = rowCode;
            this.columnCode = columnCode;

            this.hierarchyId = hierarchyId;
            this.rowName = rowName;
            this.columnName = columnName;


            this.HierarchyStartingMemberID = HierarchyStartingMemberID;
            this.IsStartingMemberIncluded = IsStartingMemberIncluded;
            
        }


        public RcColumn() { }

        readonly string rowCode;
        readonly string columnCode;
        bool isKey;
        readonly int hierarchyId;
        readonly string rowName;
        readonly string columnName;
        readonly int HierarchyStartingMemberID;
        readonly int IsStartingMemberIncluded;


        public ClassicRelationalTable table;

        HashSet<DimCharacteristic> dimCharacteristics = new HashSet<DimCharacteristic>();
        DimCharacteristic metDimCharactersitic = null;

        internal DimCharacteristic MetDimCharactersitic
        {
          get { return metDimCharactersitic; }
          set { metDimCharactersitic = value; }
        }

        internal void setIsKey(bool isKey)
        {
            this.isKey = isKey;
        }

        public string getColumnName()
        {
            if (!string.IsNullOrEmpty(rowCode) && !string.IsNullOrEmpty(columnCode))
                return constructCode(rowCode, "R") + constructCode(columnCode, "C");

            if (!string.IsNullOrEmpty(rowCode))
                return constructCode(rowCode, "R");

            if (!string.IsNullOrEmpty(columnCode))
                return constructCode(columnCode, "C");

            throw new ArgumentNullException("No row or column code");
        }

        private string constructCode(string ordinateCode, string directionPrefix)
        {
            if (ordinateCode.Contains("R") || ordinateCode.Contains("C") || ordinateCode.Contains("Z") || ordinateCode.Contains("P"))
                return ordinateCode;

            return string.Format("{0}{1}", directionPrefix, ordinateCode);
        }

        public override string ToString()
        {
            return getColumnName();
        }

        internal void addDimCharacteristic(DimCharacteristic newDImCharact)
        {
            this.dimCharacteristics.Add(newDImCharact);
            if (!string.IsNullOrWhiteSpace(newDImCharact.DomCode))
                this.addDomCode(newDImCharact.DomCode, newDImCharact.DimCode);
        }        

        public string DimCode
        {
            get { return this.getColumnMappingDimCode(); }
        }

        public string getColumnMappingDimCode()
        {
            string dimCode = "";

            if(this.metDimCharactersitic != null)
            {
                dimCode = metDimCharactersitic.getDimCode();
            }

            foreach (DimCharacteristic dc in this.dimCharacteristics)
            {
                dimCode = dimCode + dc.getDimCode();
            }

            return dimCode;
        }

        public HashSet<string> getColumnMappingDimCodes()
        {
            HashSet<string> dimCodes = new HashSet<string>();

            if (this.metDimCharactersitic != null)
            {
                dimCodes.Add(metDimCharactersitic.getDimCode());
            }

            foreach (DimCharacteristic dc in this.dimCharacteristics)
            {
                if (dc.MemCodes.Count == 0 
                    && this.dimCharacteristics.FirstOrDefault(x => x.DimCode == dc.DimCode && x.MemCodes.Count > 0) != null)
                    continue;

                dimCodes.Add(dc.getDimCode());
            }

            if (dimCodes.Count() == 0)
                dimCodes.Add("");

            return dimCodes;
        }

        string origin = "";
        public string getOrigin()
        {
            if (!string.IsNullOrEmpty(this.origin))
                return origin;

            if (!string.IsNullOrWhiteSpace(this.rowCode) && !string.IsNullOrWhiteSpace(this.columnCode))
                return ProcesorGlobals.FactOrigin;
            if (metDimCharactersitic != null)
                return ProcesorGlobals.FactOrigin;
            else if (metDimCharactersitic == null && 
                ( dimCharacteristics.Count == 1 || dimCharacteristics.Select(x=>x.DimCode).Distinct().Count() == 1))
                return ProcesorGlobals.ContextOrigin;
            else
                return ProcesorGlobals.FactOrigin;
        }

        internal void forceOrigin(string origin)
        {
            this.origin = origin;
        }

        public int getRequiredMappingsNumber()
        {
            int reqMap = this.dimCharacteristics.Select(x=>x.DimCode).Distinct().Count();

            if (this.metDimCharactersitic != null)
                reqMap = reqMap + 1;

            return reqMap == 0 ? 1 : reqMap;
        }

        IRelationColumnDataType dataType = IRelationColumnDataType.String;
        public IRelationColumnDataType getDataType()
        {
            return dataType;
        }

        internal void setDataType(IRelationColumnDataType relationColumnDataType)
        {
            dataType = relationColumnDataType;
        }

        public IEnumerable<string> getColumnMappingDimKeyCodes()
        {
            return new HashSet<string>();
        }

        
        public bool isPageColumn
        {
            get
            {
                return false;
            }
        }
        public bool isKeyColumn
        {
            get
            {
                return this.isKey;
            }
        }


        public int getRequiredMappingsNumber(ClassicRelationalTable _table)
        {
            return getRequiredMappingsNumber();
        }

        public bool isInTable(ClassicRelationalTable _table)
        {
            return true;
        }


        Dictionary<string, string> dimCodeToDomCode = new Dictionary<string, string>();
        public string getDomCodeForDimCode(string dimCode)
        {
            if (dimCode.Contains("("))
                dimCode = dimCode.Substring(0, dimCode.IndexOf("("));

            string domCode = "";
            if (dimCodeToDomCode.TryGetValue(dimCode, out domCode))
                return domCode;

            return "";
        }

        public string getMemberCodeForDimCode(string mappingCode)
        {
            if (string.IsNullOrWhiteSpace(mappingCode)) return "";
            string dimCode = mappingCode.Substring(0, mappingCode.IndexOf("("));

            foreach (DimCharacteristic dc in dimCharacteristics)
            {
                if (!dc.DimCode.Equals(dimCode))
                    continue;

                foreach (string memCode in dc.MemCodes)
                    if (mappingCode.Equals(string.Format("{0}({1})", dc.DimCode, memCode)))
                        return memCode;
            }

            //throw new KeyNotFoundException(mappingCode + " not found ");
            return "";
        }

        public void addDomCode(string domCode, string dimCode)
        {
            if (!dimCodeToDomCode.ContainsKey(dimCode))
                dimCodeToDomCode.Add(dimCode, domCode);
        }

        public string getColumnNameForExcelTemplate()
        {
            return this.columnName;           
        }

        public string getRowNameForExcelTemplate()
        {
            return this.rowName;
        }

        public int getHierarchyIdForExcelTemplate()
        {
            return this.hierarchyId;
        }

        public string getHierarchyIdForExcelTemplateWithMetrics()
        {
            StringBuilder sbHierarchyIdWithMetrics = new StringBuilder();
            sbHierarchyIdWithMetrics.Append(this.hierarchyId);
                    if (HierarchyStartingMemberID != 0)
                    {
                        sbHierarchyIdWithMetrics.Append(",");
                        sbHierarchyIdWithMetrics.Append(this.HierarchyStartingMemberID);
                        sbHierarchyIdWithMetrics.Append(",");
                        sbHierarchyIdWithMetrics.Append(this.IsStartingMemberIncluded);
                    }


                    return sbHierarchyIdWithMetrics.ToString();
        }

        

        
    }
}
