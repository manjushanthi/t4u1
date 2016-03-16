using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SolvencyII.Domain.Interfaces;

namespace SolvencyII.Data.SQLite
{
    /// <summary>
    /// Partial class to main SQLiteConnection using an interface so standardised api visible; allowing multiple dbs as required. (See SolvencyUU.Data.DBMS for SQLServer.)
    /// </summary>
    public partial class SQLiteConnection : ISolvencyData
    {
        public List<object> Query(Type tableType, string query, params object[] args)
        {
            TableMapping map = new TableMapping(tableType);
            return Query(map, query, args);
        }

        public List<object> Query(Type tableType, string query, int firstRow, int lastRow)
        {
            TableMapping map = new TableMapping(tableType);
            return Query(map, query, firstRow, lastRow);
        }

        public List<object> Query(TableMapping map, string query, int firstRow, int lastRow)
        {
            var cmd = CreateCommand(query);
            return cmd.ExecuteQuery<object>(map, firstRow, lastRow);
        }

        public int Execute(string query, Dictionary<string, object> args)
        {
            SQLiteCommand cmd = CreateCommand(query, args);

            if (TimeExecution)
            {
                if (_sw == null)
                {
                    _sw = new System.Diagnostics.Stopwatch();
                }
                _sw.Reset();
                _sw.Start();
            }

            var r = cmd.ExecuteNonQuery();

            if (TimeExecution)
            {
                _sw.Stop();
                _elapsedMilliseconds += _sw.ElapsedMilliseconds;
                Debug.WriteLine(string.Format("Finished in {0} ms ({1:0.0} s total)", _sw.ElapsedMilliseconds, _elapsedMilliseconds / 1000.0));
            }

            return r;
        }


        /// <summary>
        /// Creates a Command for SQLite
        /// Enables the use of a dictionary when creating a Command 
        /// Queries can use named parameters @Par1, @Par2
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public SQLiteCommand CreateCommand(string cmdText, Dictionary<string, object> ps)
        {
            if (!_open)
            {
                throw SQLiteException.New(SQLite3.Result.Error, "Cannot create commands from unopened database");
            }
            else
            {
                var cmd = NewCommand();
                cmd.CommandText = cmdText;

                if (ps != null)
                {
                    foreach (var o in ps)
                    {
                        cmd.Bind(o.Key, o.Value);
                    }
                }
                return cmd;
            }
        }

    }

    public partial class SQLiteCommand
    {
        public List<T> ExecuteQuery<T>(TableMapping map, int firstRow, int lastRow)
        {
            return ExecuteDeferredQuery<T>(map, firstRow, lastRow).ToList();
        }
        public IEnumerable<T> ExecuteDeferredQuery<T>(TableMapping map, int firstRow, int lastRow)
        {
            if (_conn.Trace)
            {
                Debug.WriteLine("Executing Query: " + this);
            }

            var stmt = Prepare();
            try
            {
                var cols = new TableMapping.Column[SQLite3.ColumnCount(stmt)];

                for (int i = 0; i < cols.Length; i++)
                {
                    var name = SQLite3.ColumnName16(stmt, i);
                    cols[i] = map.FindColumn(name);
                }
                //if (firstRow != 0)
                //{
                //    bool debug = true;
                //}
                int skipRowCounter = 0;
                int takeRowCounter = 0;
                int requiredRecords = lastRow - firstRow;
                while (SQLite3.Step(stmt) == SQLite3.Result.Row)
                {
                    if (skipRowCounter >= firstRow)
                    {
                        if (takeRowCounter <= requiredRecords)
                        {
                            var obj = Activator.CreateInstance(map.MappedType);
                            for (int i = 0; i < cols.Length; i++)
                            {
                                if (cols[i] == null)
                                    continue;
                                var colType = SQLite3.ColumnType(stmt, i);
                                var val = ReadCol(stmt, i, colType, cols[i].ColumnType);
                                cols[i].SetValue(obj, val);
                            }
                            OnInstanceCreated(obj);
                            takeRowCounter++;
                            yield return (T) obj;
                        }
                        else
                            break;
                    }
                    skipRowCounter++;
                }
            }
            finally
            {
                SQLite3.Finalize(stmt);
            }
        }

    }
}
