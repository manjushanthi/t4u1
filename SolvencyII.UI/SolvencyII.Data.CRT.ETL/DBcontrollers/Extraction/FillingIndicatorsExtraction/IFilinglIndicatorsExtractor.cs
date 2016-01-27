using System;
namespace SolvencyII.Data.CRT.ETL
{
    /// <summary>
    /// Interface of fillind indicator extractors
    /// </summary>
    public interface IFilinglIndicatorsExtractor
    {
        /// <summary>
        /// Gets the tables names from filling indicators.
        /// </summary>
        /// <param name="instnanceId">The instnance identifier.</param>
        /// <returns></returns>
        string[] getTablesNamesFromFillingIndicators(int instnanceId);
        /// <summary>
        /// Gets the tables names from module.
        /// </summary>
        /// <param name="instanceID">The instance identifier.</param>
        /// <returns></returns>
        string[] getTablesNamesFromModule(int instanceID);
        /// <summary>
        /// Gets the tables i ds from filling indicators.
        /// </summary>
        /// <param name="instanceID">The instance identifier.</param>
        /// <returns></returns>
        int[] getTablesIDsFromFillingIndicators(int instanceID);
        /// <summary>
        /// Gets the tables i ds from module.
        /// </summary>
        /// <param name="instanceID">The instance identifier.</param>
        /// <returns></returns>
        int[] getTablesIDsFromModule(int instanceID);

    }
}
