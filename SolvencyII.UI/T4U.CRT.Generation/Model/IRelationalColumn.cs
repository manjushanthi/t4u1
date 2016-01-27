using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DpmDB;

namespace T4U.CRT.Generation.Model
{
    public interface IRelationalColumn
    {
        string getColumnName();
        string getColumnMappingDimCode();
        string getOrigin();
        IRelationColumnDataType getDataType();

        HashSet<string> getColumnMappingDimCodes();

        string getDomCodeForDimCode(string dimCode);
        string getMemberCodeForDimCode(string dimCode);

        void addDomCode(string domCode, string dimCode);

        IEnumerable<string> getColumnMappingDimKeyCodes();

        bool isPageColumn { get; }

        bool isKeyColumn { get; }

        int getRequiredMappingsNumber(ClassicRelationalTable _table);

        bool isInTable(ClassicRelationalTable _table);

        string getColumnNameForExcelTemplate();
        string getRowNameForExcelTemplate();
        int getHierarchyIdForExcelTemplate();
        string getHierarchyIdForExcelTemplateWithMetrics();
    }

    public enum IRelationColumnDataType
    {
        Monetary,
        String,
        Date,
        Integer,
        Percentage,
        Boolean,
        Enumeration,
        Decimal
    }
}
