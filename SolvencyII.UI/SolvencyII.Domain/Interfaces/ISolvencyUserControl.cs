using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SolvencyII.Domain.Interfaces
{
    /// <summary>
    /// Interface for the closed template parent class.
    /// </summary>
    [Guid("0F3C7B29-45AC-4914-96BE-9C44B3A29185")]
    public interface ISolvencyUserControl 
    {
        double Version { get;}
        string GroupTableIDs { get;}
        int TableVID { get;}
        string FrameworkCode { get; }
        ISolvencyUserControl Create { get; }


        // int VersionT4U { get; }
        List<Type> DataTypes { get; }
        List<String> DataTables { get; }
        void SetupData(int i, string query);
        void BindRepeater();
        void AddRecord();
        void DelRecord();
        void RefreshDR();

        void SetPK_ID(long PK_ID);

        bool IsDirty { get; set; }
        bool IsValid();

        /// <summary>
        /// The UpdateData event gets specifically created to use known data types 
        /// which use the appropriate instances of the GenericRepository to save the information.
        /// </summary>
        /// <param name="errorText"></param>
        /// <param name="nPageControls"></param>
        /// <returns></returns>
        bool UpdateData(out string errorText, List<ISolvencyPageControl> nPageControls);

    }
}
