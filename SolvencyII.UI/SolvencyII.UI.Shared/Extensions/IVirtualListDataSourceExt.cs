using System;
using System.Collections.Generic;
using BrightIdeasSoftware;
using SolvencyII.Data.Shared.Entities;

namespace SolvencyII.UI.Shared.Extensions
{
    /// <summary>
    ///  Not used.
    /// </summary>
    public interface IVirtualListDataSourceExt : IVirtualListDataSource
    {
        void SetupDataSource3(Type dataType, string tableName, long instanceID, List<OpenColInfo2> columns, Dictionary<string, string> specifiedColumnsFromCombos);
    }
}
