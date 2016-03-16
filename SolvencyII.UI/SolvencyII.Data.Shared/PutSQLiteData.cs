using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolvencyII.Data.Entities;
using SolvencyII.Data.SQL;
using SolvencyII.Data.SQLite;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.Domain;
using SolvencyII.Domain.Configuration;
using SolvencyII.Domain.Conversions;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Extensions;
using SolvencyII.Domain.Interfaces;

namespace SolvencyII.Data.Shared
{
    /// <summary>
    /// Business side of data tier where queries are built to be processed by ISolvencyData providers.
    /// This is used exclusively for updating and inserting information.
    /// </summary>
    public class PutSQLData : IDisposable
    {
        private readonly ISolvencyData _conn;
        public string Errors = "";
        private string lastIdentity = "last_insert_rowid()";
        public PutSQLData() : this(StaticSettings.ConnectionString)
        {
        }

        public PutSQLData(string connectionString)
        {
            switch (StaticSettings.DataTier)
            {
                case eDataTier.SqLite:
                    _conn = new SQLiteConnection(connectionString);
                    break;
                case eDataTier.SqlServer:
                    lastIdentity = "@@IDENTITY";
                    _conn = new DataConnection(connectionString);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #region IDisposable implementation

        public void Dispose(bool disposing)
        {
            _conn.Close();
            _conn.Dispose();
        }

        public void Dispose()
        {
            _conn.Close();
            _conn.Dispose();
        }

        #endregion

        #region Open tables

 
        public void DeleteOpenTableData2(OpenTableDataRow2 row, string tableName, long instanceID)
        {
            
            _conn.BeginTransaction();
            try
            {
                string query = "Delete from {0} Where PK_ID = {1} ";
                _conn.Execute(string.Format(query, tableName, row.PK_ID));
                _conn.Commit();
            }
            catch (Exception)
            {
                _conn.Rollback();
            }
        }

        public void DeleteOpenTableData2(string tableName, long instanceID)
        {
            string query = string.Format("Delete from {0} Where INSTANCE = {1}", tableName, instanceID);
            _conn.Execute(query);
        }

        #endregion

        #region Closed Tables

        public List<long> PutClosedTableData(List<string> queries)
        {
            List<long> results = new List<long>(queries.Count);
            _conn.BeginTransaction();
            try
            {
                for (int i = 0; i < queries.Count; i++)
                {
                    _conn.Execute(queries[i]);
                    if(queries[i].StartsWith("Insert"))
                        results.Add(_conn.ExecuteScalar<long>("Select last_insert_rowid(); "));
                }
                _conn.Commit();
                return results;
            }
            catch (Exception ex)
            {
                Errors = ex.Message;
                _conn.Rollback();
                return null;
            }
        }


        /// <summary>
        /// Run a list of queries based upon parameterised input
        /// </summary>
        /// <param name="queries"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<long> PutClosedTableData(List<string> queries, List<Dictionary<string, object>> parameters)
        {
            List<long> results = new List<long>(queries.Count);
            _conn.BeginTransaction();
            try
            {
                for (int i = 0; i < queries.Count; i++)
                {
                    if (queries[i].StartsWith("Insert"))
                    {
                        switch (StaticSettings.DataTier)
                        {
                            case eDataTier.SqLite:
                                _conn.Execute(queries[i], parameters[i]);
                                results.Add(_conn.ExecuteScalar<long>("Select last_insert_rowid(); "));
                                break;
                            case eDataTier.SqlServer:
                                queries[i] += "SELECT Cast(SCOPE_IDENTITY() as bigint) as PK_ID;";
                                long id = _conn.ExecuteScalar<long>(queries[i], parameters[i]);
                                results.Add(id);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    else
                    {
                        _conn.Execute(queries[i], parameters[i]);
                    }
                }
                _conn.Commit();
                return results;
            }
            catch (SQLiteException ex)
            {
                if (ex.Message == "Constraint")
                    Errors = "A record conflict occurred.\r\nYou cannot have two records with the same key fields.\r\nKey fields could be country or currency.";
                else
                    Errors = ex.Message;
                _conn.Rollback();
                return null;
            }
            catch (Exception ex)
            {
                Errors = ex.Message;
                _conn.Rollback();
                return null;
            }
        }





        public bool DeleteClosedTableData(List<string> queries)
        {
            _conn.BeginTransaction();
            try
            {
                foreach (string query in queries)
                {
                    _conn.Execute(query);    
                }
                _conn.Commit();
                return true;
            }
            catch (Exception ex)
            {
                Errors = ex.Message;
                _conn.Rollback();
                return false;
            }
        }

        public void DeleteTableData_AllVariants(int tableId, long instanceId)
        {
            string query = string.Format("select Distinct m.DYN_TABLE_NAME Name from mTable t Inner join Mapping m on (m.TABLE_VERSION_ID == t.TableID) where t.TableCode like (select substr(t.TableCode,0,8) from mTable t where t.TableID = {0}) || '%'", tableId);
            var tableNames = _conn.Query<SingleString>(query);

            _conn.BeginTransaction();
            foreach (SingleString tableName in tableNames)
            {
                query = string.Format("Delete from {0} where INSTANCE = {1}", SolvencyIITableNameConversion.FullDbName(tableName.Name), instanceId);
                _conn.Execute(query);
            }
            _conn.Commit();
        }

        #endregion

        #region Instance operation

        public string InsertUpdateInstance(dInstance instance, out long instanceID)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                instanceID = instance.InstanceID;
                // Does instance exist?
                if (instanceID > 0)
                {
                    // We have an existing record
                    sb.Append("Update dInstance ");
                    sb.Append(string.Format("Set ModuleID = {0}, ", instance.ModuleID));
                    sb.Append(string.Format("FileName = '{0}', ", instance.FileName));
                    sb.Append(string.Format("Timestamp = '{0}', ", instance.Timestamp.ConvertToDateTimeString()));
                    sb.Append(string.Format("EntityCurrency = '{0}', ", instance.EntityCurrency));
                    sb.Append(string.Format("EntityIdentifier = '{0}', ", instance.EntityIdentifier));
                    sb.Append(string.Format("EntityName = '{0}', ", instance.EntityName));
                    sb.Append(string.Format("EntityScheme = '{0}', ", instance.EntityScheme));
                    sb.Append(string.Format("PeriodEndDateOrInstant = '{0}' ", instance.PeriodEndDateOrInstant.ConvertToDateString()));

                    sb.Append(string.Format("Where InstanceID = {0} ", instance.InstanceID));
                }
                else
                {
                    // We need to insert this one.

                    // Get the correct ID
                    int currentMaxID = 0;

                    if (_conn.ExecuteScalar<int>("Select Count(*) from dInstance ") == 0)
                        currentMaxID = 0;
                    else
                        currentMaxID = _conn.ExecuteScalar<int>("Select Max(InstanceID) from dInstance ");

                    currentMaxID++;
                    // Create the file name if one does not exist
                    if (instance.FileName == null) instance.FileName = string.Format("instance_{0}_{1:yyyyMMdd}.xbrl", instance.EntityIdentifier, instance.PeriodEndDateOrInstant);
                    sb.Append("Insert into dInstance ");
                    sb.Append("(InstanceID, ModuleID, FileName, Timestamp, EntityCurrency, EntityIdentifier, EntityName, EntityScheme, PeriodEndDateOrInstant) ");
                    sb.Append("Values ");
                    sb.Append(string.Format("({0}, {1}, '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}') "
                                            , currentMaxID, instance.ModuleID, instance.FileName,
                                            instance.Timestamp.ConvertToDateTimeString(), instance.EntityCurrency,
                                            instance.EntityIdentifier, instance.EntityName, instance.EntityScheme, instance.PeriodEndDateOrInstant.ConvertToDateString()));
                    instanceID = currentMaxID;
                }

                _conn.BeginTransaction();
                _conn.Execute(sb.ToString());
                if (instanceID == 0) instanceID = _conn.ExecuteScalar<int>(string.Format("SELECT {0}", lastIdentity));
                _conn.Commit();

                return "";
            }
            catch (InvalidOperationException ex)
            {
                _conn.Rollback();
                Console.WriteLine(ex);
                instanceID = 0;
                return ex.Message;
            }
        }

        public string DeleteInstance(long instanceID)
        {
            _conn.BeginTransaction();
            try
            {
                _conn.Execute(string.Format("Delete From dFilingIndicator Where InstanceId = {0} ", instanceID));
                //_conn.Execute(string.Format("Delete From dProcessingContext Where InstanceId = {0} ", instanceID));
                //_conn.Execute(string.Format("Delete From dProcessingFact Where InstanceId = {0} ", instanceID));
                //_conn.Execute(string.Format("Delete From dAvailableTable Where InstanceId = {0} ", instanceID));
                _conn.Execute(string.Format("Delete From dFact Where InstanceId = {0} ", instanceID));
                _conn.Execute(string.Format("Delete From dInstance Where InstanceId = {0} ", instanceID));

                List<TableName> tables = _conn.Query<TableName>("SELECT DYN_TABLE_NAME FROM Mapping Group by DYN_TABLE_NAME");
                foreach (TableName table in tables)
                {
                    _conn.Execute(string.Format("Delete from [{0}] where Instance = {1}", SolvencyIITableNameConversion.FullDbName(table.DYN_TABLE_NAME), instanceID));
                }

                _conn.Commit();
                return "";
            }
            catch (Exception ex)
            {
                _conn.Rollback();
                return ex.Message;
            }


        }

        // The the life of me I could not work out what to use to get a single list of strings
        private class TableName
        {
            public string DYN_TABLE_NAME { get; set; }
        }

        #endregion

        #region Filing Indicator

        public void ToggleFileIndicatorFiled(long instanceID, int templateOrTableID)
        {
            if (!SharedSQLData.FilingIndicatorExists(instanceID, templateOrTableID, _conn))
                SaveFilingIndicator(instanceID, templateOrTableID, false);
            else
            {
                bool filed = SharedSQLData.GetFilingIndicatorFiled(instanceID, templateOrTableID, _conn);
                UpdateFilingIndicator(instanceID, templateOrTableID, !filed);
            }
        }

        public void UpdateFilingIndicator(long instanceID, int templateOrTableID, bool filed)
        {
            if (!SharedSQLData.FilingIndicatorExists(instanceID, templateOrTableID, _conn)) 
                SaveFilingIndicator(instanceID, templateOrTableID, filed);
            else
                _conn.Execute(string.Format("update dFilingIndicator Set Filed = {0} Where InstanceID = {1} and BusinessTemplateID = {2}", filed ? 1 : 0, instanceID, templateOrTableID));
        }
        
        
        public void SaveFilingIndicator(long instanceID, int templateOrTableID, bool filed = true)
        {
            if (!SharedSQLData.FilingIndicatorExists(instanceID, templateOrTableID, _conn)) 
            {
                // We need to insert the record;
                _conn.Execute(string.Format("Insert into dFilingIndicator (InstanceID, BusinessTemplateID, Filed) Values ({0}, {1}, {2});", instanceID, templateOrTableID, filed ? 1 : 0));
            }
        }

        public void DeleteFilingIndicator(long instanceID, int templateOrTableID, string tableName)
        {
            bool delete = true;
            if (!string.IsNullOrEmpty(tableName))
            {
                int count = _conn.ExecuteScalar<int>(string.Format("Select count(*) from [{0}] where Instance = {1} ", tableName, instanceID));
                if (count > 0) delete = false;
            }

            if(delete)
                _conn.Execute(string.Format("Delete from dFilingIndicator where InstanceID = {0} AND BusinessTemplateID = {1};", instanceID, templateOrTableID));
        }


        #endregion


    }
}
