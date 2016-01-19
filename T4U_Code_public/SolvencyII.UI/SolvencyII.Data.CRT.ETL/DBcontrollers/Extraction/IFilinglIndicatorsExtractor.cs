using System;
namespace ClassicRelationalETL
{
    public interface IFilinglIndicatorsExtractor
    {
        string[] getTablesNamesFromFillingIndicators(int instnanceId);
        string[] getTablesNamesFromModule(int instanceID);
        int[] getTablesIDsFromFillingIndicators(int instanceID);
        int[] getTablesIDsFromModule(int instanceID);
    }
}
