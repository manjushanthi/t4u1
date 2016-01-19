using System;
using System.Collections.Generic;
using SolvencyII.Data.CRT.ETL.Model;

namespace SolvencyII.Data.CRT.ETL
{
    /// <summary>
    /// Interface of mapping providers
    /// </summary>
    public interface IMappingProvider
    {
        /// <summary>
        /// Gets all mapping hash set.
        /// </summary>
        void getAllMappingHashSet();
        /// <summary>
        /// Gets the mappings.
        /// </summary>
        /// <returns></returns>
        HashSet<CrtMapping> getMappings();
        /// <summary>
        /// Gets the mappings.
        /// </summary>
        /// <param name="tableNames">The table names.</param>
        /// <returns></returns>
        HashSet<CrtMapping> getMappings(string[] tableNames);
        /// <summary>
        /// Queries the mappings.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="withPageColumnKey">if set to <c>true</c> [with page column key].</param>
        /// <returns></returns>
        HashSet<CrtMapping> queryMappings(string query, bool withPageColumnKey = false);

        /// <summary>
        /// Gets the instance tables.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns></returns>
        List<string> getInstanceTables(int p);
        /// <summary>
        /// Cleans the mappings.
        /// </summary>
        void CleanMappings();
        /// <summary>
        /// Gets all mapping hash set.
        /// </summary>
        /// <param name="tableList">The table list.</param>
        void getAllMappingHashSet(List<string> tableList);
    }
}
