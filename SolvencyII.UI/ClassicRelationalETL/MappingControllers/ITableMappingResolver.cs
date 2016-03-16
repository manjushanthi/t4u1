using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolvencyII.Data.CRT.ETL.Model;

namespace SolvencyII.Data.CRT.ETL
{
    /// <summary>
    /// Interface of mapping resolver
    /// </summary>
    public interface ITableMappingResolver
    {
        /// <summary>
        /// Resolves the specified maping.
        /// </summary>
        /// <param name="maping">The maping.</param>
        /// <returns></returns>
        HowHandleEnum resolve(CrtMapping maping);
    }

    /// <summary>
    /// Enumenariotn of hadning of table
    /// </summary>
    public enum HowHandleEnum
    {
        DimByDim,
        DataPoint,
        None
    }
}
