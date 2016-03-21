using System;
namespace T4U.CRT.Generation
{
    interface ITableLoadingHelper
    {
        System.Collections.Generic.HashSet<string> getMappingInserts();
        string getProcesingTableDDL();
        string getQueryToCheckifTableIsInDB();
        string getQueryToGetMappings();
        string getTableDDL();
    }
}
