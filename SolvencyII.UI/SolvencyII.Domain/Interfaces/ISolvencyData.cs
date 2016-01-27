using System;
using System.Collections.Generic;

namespace SolvencyII.Domain.Interfaces
{
    /// <summary>
    /// Interface to ensure consistant data access for T4U.
    /// </summary>
    public interface ISolvencyData 
    {

        int Execute(string query, params object[] args);
        int Execute(string query, Dictionary<string, object> args);
        T ExecuteScalar<T>(string query, Dictionary<string, object> args);
        T ExecuteScalar<T>(string query, params object[] args);
        List<T> Query<T>(string query, params object[] args) where T : new();
        List<object> Query(Type tableType, string query, params object[] args);
        List<object> Query(Type tableType, string query, int firstRow, int lastRow);
        void BeginTransaction();
        void Rollback();
        void Commit();
        void Dispose();
        void Close();
        object CreateParameter(string name, object value);
    }
}
