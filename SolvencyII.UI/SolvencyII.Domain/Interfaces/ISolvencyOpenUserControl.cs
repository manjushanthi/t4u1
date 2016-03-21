using System;
using System.Collections.Generic;

namespace SolvencyII.Domain.Interfaces
{
    /// <summary>
    /// Interface for the open template
    /// </summary>
    public interface ISolvencyOpenUserControl 
    {
        double Version { get;}
        int TableVID { get;}
        string FrameworkCode { get; }
        ISolvencyOpenUserControl Create { get; }
        int VersionT4U { get; }
        Type DataType { get; }
        String DataTable { get; }
        List<ISolvencyCollectionMember> Columns { get; set; }
        /// <summary>
        /// The Grid needs to be created and positioned dynamically - dependant on the existance of combos.
        /// </summary>
        int GridTop { get; }
        // object DataSource { get; set; }
    }
}
