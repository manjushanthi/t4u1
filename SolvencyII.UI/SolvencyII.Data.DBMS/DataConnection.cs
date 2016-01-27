//
// Copyright (c) 2009-2012 Krueger Systems, Inc.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//


/*
 * Object wrapper for SQL server - Written for EIOPA by Nicholas Jory Professional Web Services
 * based upon SQLite interface by Krueger Systems (See notice above).
 * This small foot print mapper has been chosed over other mappers to enable cross platform usage.
 */


using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using SolvencyII.Domain.Interfaces;


namespace SolvencyII.Data.SQL
{

    /// <summary>
	/// Generates an open connection to a SQL Server database using the standard data interface.
	/// </summary>
	public partial class DataConnection : ISolvencyData, IDisposable
	{
		private bool _open;
		private Dictionary<string, TableMapping> _mappings = null;
		private Stopwatch _sw;
		private long _elapsedMilliseconds = 0;

        public SqlConnection Connection { get; private set; }
        public SqlTransaction Transaction { get; set; }

		public bool TimeExecution { get; set; }

		public bool Trace { get; set; }

	    public DataConnection(string databaseConnection)
	    {
	        Connection = new SqlConnection(databaseConnection);
	        Connection.Open();
	        _open = true;
	    }

		public TableMapping GetMapping (Type type)
		{
			if (_mappings == null) {
				_mappings = new Dictionary<string, TableMapping> ();
			}
			TableMapping map;
			if (!_mappings.TryGetValue (type.FullName, out map)) {
				map = new TableMapping (type);
				_mappings [type.FullName] = map;
			}
			return map;
		}
		
		public TableMapping GetMapping<T> ()
		{
			return GetMapping (typeof (T));
		}

		public SQLCommand CreateCommand (string cmdText, params object[] ps)
		{
			if (!_open) {
				throw new Exception("Cannot create commands from unopened database");
			} else {
				var cmd = new SQLCommand(this);
			    cmd.CommandText = cmdText;
			    return cmd;
			}
		}

		public int Execute (string query, params object[] args)
		{
			var cmd = CreateCommand(query, args);
			
			if (TimeExecution) {
				if (_sw == null) {
					_sw = new System.Diagnostics.Stopwatch ();
				}
				_sw.Reset ();
				_sw.Start ();
			}

			var r = cmd.ExecuteNonQuery ();
			
			if (TimeExecution) {
				_sw.Stop ();
				_elapsedMilliseconds += _sw.ElapsedMilliseconds;
				Debug.WriteLine (string.Format ("Finished in {0} ms ({1:0.0} s total)", _sw.ElapsedMilliseconds, _elapsedMilliseconds / 1000.0));
			}
			
			return r;
		}

        public int Execute(string query, Dictionary<string, object> args)
        {
            var cmd = CreateCommand(query, args);

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

        public SQLCommand CreateCommand(string cmdText, Dictionary<string, object> args)
        {
            if (!_open)
            {
                throw new Exception("Cannot create commands from unopened database");
            }
            else
            {
                var cmd = new SQLCommand(this);
                cmd.CommandText = cmdText;
                foreach (var o in args)
                {
                    SqlParameter par;
                    if (o.Value != null)
                        par = new SqlParameter(o.Key, o.Value);
                    else
                    {
                        par = new SqlParameter(o.Key, DBNull.Value) {IsNullable = true};
                    }

                    cmd.Parameters.Add(par);
                }
                return cmd;
            }
        }

        public T ExecuteScalar<T>(string query, Dictionary<string, object> args)
        {
            var cmd = CreateCommand(query, args);

            if (TimeExecution)
            {
                if (_sw == null)
                {
                    _sw = new System.Diagnostics.Stopwatch();
                }
                _sw.Reset();
                _sw.Start();
            }

            var r = cmd.ExecuteScalar<T>();

            if (TimeExecution)
            {
                _sw.Stop();
                _elapsedMilliseconds += _sw.ElapsedMilliseconds;
                Debug.WriteLine(string.Format("Finished in {0} ms ({1:0.0} s total)", _sw.ElapsedMilliseconds, _elapsedMilliseconds / 1000.0));
            }

            return r;
        }

		public T ExecuteScalar<T> (string query, params object[] args)
		{
			var cmd = CreateCommand (query, args);
			
			if (TimeExecution) {
				if (_sw == null) {
					_sw = new System.Diagnostics.Stopwatch ();
				}
				_sw.Reset ();
				_sw.Start ();
			}
			
			var r = cmd.ExecuteScalar<T> ();
			
			if (TimeExecution) {
				_sw.Stop ();
				_elapsedMilliseconds += _sw.ElapsedMilliseconds;
				Debug.WriteLine (string.Format ("Finished in {0} ms ({1:0.0} s total)", _sw.ElapsedMilliseconds, _elapsedMilliseconds / 1000.0));
			}
			
			return r;
		}

		public List<T> Query<T> (string query, params object[] args) where T : new()
		{
			var cmd = CreateCommand (query, args);
			return cmd.ExecuteQuery<T> ();
		}

		public List<object> Query (TableMapping map, string query, params object[] args)
		{
			var cmd = CreateCommand (query, args);
			return cmd.ExecuteQuery<object> (map);
		}

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

        #region Transactions

        public void BeginTransaction()
        {
            Transaction = Connection.BeginTransaction();
        }

        public void Rollback()
        {
            if (Transaction != null)
                Transaction.Rollback();
            Transaction = null;
        }

        public void Commit()
        {
            if (Transaction != null)
                Transaction.Commit();
            Transaction = null;
        }

        #endregion

        #region Dispose and De-contructor

        //~DataConnection()
        //{
        //    Dispose(false);
        //}

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Close();
        }

        public void Close()
        {

            if (Connection != null && Connection.State != ConnectionState.Closed)
            {
                Connection.Close();
                _open = false;
            }

        }

        public object CreateParameter(string name, object value)
        {
            return new SqlParameter(name, value);
        }

        #endregion

	}



    [AttributeUsage (AttributeTargets.Class)]
	public class TableAttribute : Attribute
	{
		public string Name { get; set; }

		public TableAttribute (string name)
		{
			Name = name;
		}
	}

	[AttributeUsage (AttributeTargets.Property)]
	public class ColumnAttribute : Attribute
	{
		public string Name { get; set; }

		public ColumnAttribute (string name)
		{
			Name = name;
		}
	}

	[AttributeUsage (AttributeTargets.Property)]
	public class PrimaryKeyAttribute : Attribute
	{
	}

	[AttributeUsage (AttributeTargets.Property)]
	public class AutoIncrementAttribute : Attribute
	{
	}

	[AttributeUsage (AttributeTargets.Property)]
	public class IndexedAttribute : Attribute
	{
		public string Name { get; set; }
		public int Order { get; set; }
		public virtual bool Unique { get; set; }
		
		public IndexedAttribute()
		{
		}
		
		public IndexedAttribute(string name, int order)
		{
			Name = name;
			Order = order;
		}
	}

	[AttributeUsage (AttributeTargets.Property)]
	public class IgnoreAttribute : Attribute
	{
	}

	[AttributeUsage (AttributeTargets.Property)]
	public class UniqueAttribute : IndexedAttribute
	{
		public override bool Unique {
			get { return true; }
			set { /* throw?  */ }
		}
	}

	[AttributeUsage (AttributeTargets.Property)]
	public class MaxLengthAttribute : Attribute
	{
		public int Value { get; private set; }

		public MaxLengthAttribute (int length)
		{
			Value = length;
		}
	}

	[AttributeUsage (AttributeTargets.Property)]
	public class CollationAttribute: Attribute
	{
		public string Value { get; private set; }

		public CollationAttribute (string collation)
		{
			Value = collation;
		}
	}

	public class TableMapping
	{
		public Type MappedType { get; private set; }

		public string TableName { get; private set; }

		public Column[] Columns { get; private set; }

		public Column PK { get; private set; }

		public string GetByPrimaryKeySql { get; private set; }

		Column _autoPk = null;
		Column[] _insertColumns = null;
		Column[] _insertOrReplaceColumns = null;

		public TableMapping (Type type)
		{
			MappedType = type;

#if NETFX_CORE
			var tableAttr = (TableAttribute)System.Reflection.CustomAttributeExtensions
                .GetCustomAttribute(type.GetTypeInfo(), typeof(TableAttribute), true);
#else
			var tableAttr = (TableAttribute)type.GetCustomAttributes (typeof (TableAttribute), true).FirstOrDefault ();
#endif

			TableName = tableAttr != null ? tableAttr.Name : MappedType.Name;

#if !NETFX_CORE
			var props = MappedType.GetProperties (BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);
#else
			var props = from p in MappedType.GetRuntimeProperties()
						where ((p.GetMethod != null && p.GetMethod.IsPublic) || (p.SetMethod != null && p.SetMethod.IsPublic) || (p.GetMethod != null && p.GetMethod.IsStatic) || (p.SetMethod != null && p.SetMethod.IsStatic))
						select p;
#endif
			var cols = new List<Column> ();
			foreach (var p in props) {
#if !NETFX_CORE
				var ignore = p.GetCustomAttributes (typeof(IgnoreAttribute), true).Length > 0;
#else
				var ignore = p.GetCustomAttributes (typeof(IgnoreAttribute), true).Count() > 0;
#endif
				if (p.CanWrite && !ignore) {
					cols.Add (new Column (p));
				}
			}
			Columns = cols.ToArray ();
			foreach (var c in Columns) {
				if (c.IsAutoInc && c.IsPK) {
					_autoPk = c;
				}
				if (c.IsPK) {
					PK = c;
				}
			}
			
			HasAutoIncPK = _autoPk != null;

			if (PK != null) {
				GetByPrimaryKeySql = string.Format ("select * from \"{0}\" where \"{1}\" = ?", TableName, PK.Name);
			}
			else {
				// People should not be calling Get/Find without a PK
				GetByPrimaryKeySql = string.Format ("select * from \"{0}\" limit 1", TableName);
			}
		}

		public bool HasAutoIncPK { get; private set; }

		public void SetAutoIncPK (object obj, long id)
		{
			if (_autoPk != null) {
				_autoPk.SetValue (obj, Convert.ChangeType (id, _autoPk.ColumnType, null));
			}
		}

		public Column[] InsertColumns {
			get {
				if (_insertColumns == null) {
					_insertColumns = Columns.Where (c => !c.IsAutoInc).ToArray ();
				}
				return _insertColumns;
			}
		}

		public Column[] InsertOrReplaceColumns {
			get {
				if (_insertOrReplaceColumns == null) {
					_insertOrReplaceColumns = Columns.ToArray ();
				}
				return _insertOrReplaceColumns;
			}
		}

		public Column FindColumnWithPropertyName (string propertyName)
		{
			var exact = Columns.Where (c => c.PropertyName == propertyName).FirstOrDefault ();
			return exact;
		}

		public Column FindColumn (string columnName)
		{
			var exact = Columns.Where (c => c.Name == columnName).FirstOrDefault ();
			return exact;
		}


		protected internal void Dispose()
		{
		}

		public class Column
		{
			PropertyInfo _prop;

			public string Name { get; private set; }

			public string PropertyName { get { return _prop.Name; } }

			public Type ColumnType { get; private set; }

			public string Collation { get; private set; }

			public bool IsAutoInc { get; private set; }

			public bool IsPK { get; private set; }

			public IEnumerable<IndexedAttribute> Indices { get; set; }

			public bool IsNullable { get; private set; }

			public int MaxStringLength { get; private set; }

			public Column (PropertyInfo prop)
			{
				var colAttr = (ColumnAttribute)prop.GetCustomAttributes (typeof(ColumnAttribute), true).FirstOrDefault ();

				_prop = prop;
				Name = colAttr == null ? prop.Name : colAttr.Name;
				//If this type is Nullable<T> then Nullable.GetUnderlyingType returns the T, otherwise it returns null, so get the the actual type instead
				ColumnType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
				Collation = Orm.Collation (prop);
				IsAutoInc = Orm.IsAutoInc (prop);
				IsPK = Orm.IsPK (prop);
				Indices = Orm.GetIndices(prop);
				IsNullable = !IsPK;
				MaxStringLength = Orm.MaxStringLength (prop);
			}

			public void SetValue (object obj, object val)
			{
				_prop.SetValue (obj, val, null);
			}

			public object GetValue (object obj)
			{
				return _prop.GetValue (obj, null);
			}
		}
	}

	public static partial class Orm
	{
		public const int DefaultMaxStringLength = 140;

		public static string SqlDecl (TableMapping.Column p, bool storeDateTimeAsTicks)
		{
			string decl = "\"" + p.Name + "\" " + SqlType (p, storeDateTimeAsTicks) + " ";
			
			if (p.IsPK) {
				decl += "primary key ";
			}
			if (p.IsAutoInc) {
				decl += "autoincrement ";
			}
			if (!p.IsNullable) {
				decl += "not null ";
			}
			if (!string.IsNullOrEmpty (p.Collation)) {
				decl += "collate " + p.Collation + " ";
			}
			
			return decl;
		}

		public static string SqlType (TableMapping.Column p, bool storeDateTimeAsTicks)
		{
			var clrType = p.ColumnType;
			if (clrType == typeof(Boolean) || clrType == typeof(Byte) || clrType == typeof(UInt16) || clrType == typeof(SByte) || clrType == typeof(Int16) || clrType == typeof(Int32)) {
				return "integer";
			} else if (clrType == typeof(UInt32) || clrType == typeof(Int64)) {
				return "bigint";
			} else if (clrType == typeof(Single) || clrType == typeof(Double) || clrType == typeof(Decimal)) {
				return "float";
			} else if (clrType == typeof(String)) {
				int len = p.MaxStringLength;
				return "varchar(" + len + ")";
			} else if (clrType == typeof(DateTime)) {
				return storeDateTimeAsTicks ? "bigint" : "datetime";
#if !NETFX_CORE
			} else if (clrType.IsEnum) {
#else
			} else if (clrType.GetTypeInfo().IsEnum) {
#endif
				return "integer";
			} else if (clrType == typeof(byte[])) {
				return "blob";
            } else if (clrType == typeof(Guid)) {
                return "varchar(36)";
            } else {
				throw new NotSupportedException ("Don't know about " + clrType);
			}
		}

		public static bool IsPK (MemberInfo p)
		{
			var attrs = p.GetCustomAttributes (typeof(PrimaryKeyAttribute), true);
            
#if !NETFX_CORE
            return attrs.Length > 0;
#else
			return attrs.Count() > 0;
#endif
        }

		public static string Collation (MemberInfo p)
		{
			var attrs = p.GetCustomAttributes (typeof(CollationAttribute), true);
#if !NETFX_CORE
			if (attrs.Length > 0) {
				return ((CollationAttribute)attrs [0]).Value;
#else
			if (attrs.Count() > 0) {
                return ((CollationAttribute)attrs.First()).Value;
#endif
			} else {
				return string.Empty;
			}
		}

		public static bool IsAutoInc (MemberInfo p)
		{
			var attrs = p.GetCustomAttributes (typeof(AutoIncrementAttribute), true);
#if !NETFX_CORE
            return attrs.Length > 0;
#else
			return attrs.Count() > 0;
#endif
        }

		public static IEnumerable<IndexedAttribute> GetIndices(MemberInfo p)
		{
			var attrs = p.GetCustomAttributes(typeof(IndexedAttribute), true);
			return attrs.Cast<IndexedAttribute>();
		}
		
		public static int MaxStringLength(PropertyInfo p)
		{
			var attrs = p.GetCustomAttributes (typeof(MaxLengthAttribute), true);
#if !NETFX_CORE
			if (attrs.Length > 0) {
				return ((MaxLengthAttribute)attrs [0]).Value;
#else
			if (attrs.Count() > 0) {
				return ((MaxLengthAttribute)attrs.First()).Value;
#endif
			} else {
				return DefaultMaxStringLength;
			}
		}
	}

	public partial class SQLCommand
	{
		DataConnection _conn;
		public string CommandText { get; set; }
        public List<SqlParameter> Parameters { get; set; }

        internal SQLCommand (DataConnection conn)
		{
			_conn = conn;
			CommandText = "";
            Parameters = new List<SqlParameter>();
		}
		public int ExecuteNonQuery ()
		{
			if (_conn.Trace) {
				Debug.WriteLine ("Executing: " + this);
			}
		    using (SqlCommand cmd = new SqlCommand(CommandText, _conn.Connection))
		    {
		        if (Parameters.Any()) cmd.Parameters.AddRange(Parameters.ToArray());
		        if (_conn.Transaction != null)
		            cmd.Transaction = _conn.Transaction;
		        cmd.ExecuteNonQuery();
		    }
		    return 0;
		}

		public List<T> ExecuteQuery<T> ()
		{
			return ExecuteDeferredQuery<T>(_conn.GetMapping(typeof(T))).ToList();
		}

		public List<T> ExecuteQuery<T> (TableMapping map)
		{
			return ExecuteDeferredQuery<T>(map).ToList();
		}

        public List<T> ExecuteQuery<T>(TableMapping map, int firstRow, int lastRow)
        {
            return ExecuteDeferredQuery<T>(map, firstRow, lastRow).ToList();
        }


		protected virtual void OnInstanceCreated (object obj)
		{
			// Can be overridden.
		}

		public IEnumerable<T> ExecuteDeferredQuery<T> (TableMapping map)
		{
			if (_conn.Trace) {
				Debug.WriteLine ("Executing Query: " + this);
			}
		    using (SqlCommand cmd = new SqlCommand(CommandText, _conn.Connection))
		    {
		        if (Parameters.Any()) cmd.Parameters.Add(Parameters);
		        if (_conn.Transaction != null)
		            cmd.Transaction = _conn.Transaction;
		        List<T> results = new List<T>();
		        SqlDataReader reader = null;
		        try
		        {
		            reader = cmd.ExecuteReader();

		            TableMapping.Column[] cols = new TableMapping.Column[reader.FieldCount];

		            for (int i = 0; i < cols.Length; i++)
		            {
		                var name = reader.GetName(i);
		                cols[i] = map.FindColumn(name);
		            }

		            while (reader.Read())
		            {
		                var obj = Activator.CreateInstance(map.MappedType);
		                for (int i = 0; i < cols.Length; i++)
		                {
		                    if (cols[i] == null)
		                        continue;

		                    var myType = reader.GetFieldType(i);
		                    var val = ReaderCol(reader, i, cols[i].ColumnType, myType);
		                    cols[i].SetValue(obj, val);
		                }
		                OnInstanceCreated(obj);
		                results.Add((T) obj);
		                // yield return (T)obj; Cant keep a reader open
		            }
		            reader.Close();
		            return results;
		        }
		        finally
		        {
                    if(reader != null && !reader.IsClosed)
                        reader.Close();
		            //SQLite3.Finalize(stmt);
		        }
		    }
		}

        public IEnumerable<T> ExecuteDeferredQuery<T>(TableMapping map, int firstRow, int lastRow)
        {
            if (_conn.Trace)
            {
                Debug.WriteLine("Executing Query: " + this);
            }
            using (SqlCommand cmd = new SqlCommand(CommandText, _conn.Connection))
            {
                if (Parameters.Any()) cmd.Parameters.Add(Parameters);
                if (_conn.Transaction != null)
                    cmd.Transaction = _conn.Transaction;
                List<T> results = new List<T>();
                SqlDataReader reader = null;
                try
                {
                    reader = cmd.ExecuteReader();

                    TableMapping.Column[] cols = new TableMapping.Column[reader.FieldCount];

                    for (int i = 0; i < cols.Length; i++)
                    {
                        var name = reader.GetName(i);
                        cols[i] = map.FindColumn(name);
                    }

                    int skipRowCounter = 0;
                    int takeRowCounter = 0;
                    while (reader.Read())
                    {
                        while (skipRowCounter >= firstRow && takeRowCounter <= lastRow)
                        {
                            var obj = Activator.CreateInstance(map.MappedType);
                            for (int i = 0; i < cols.Length; i++)
                            {
                                if (cols[i] == null)
                                    continue;

                                var myType = reader.GetFieldType(i);
                                var val = ReaderCol(reader, i, cols[i].ColumnType, myType);
                                cols[i].SetValue(obj, val);
                            }
                            OnInstanceCreated(obj);
                            results.Add((T) obj);
                            takeRowCounter++;
                        }
                        // yield return (T)obj; Cant keep a reader open
                        skipRowCounter++;
                    }
                    reader.Close();
                    return results;
                }
                finally
                {
                    if (reader != null && !reader.IsClosed)
                        reader.Close();
                    //SQLite3.Finalize(stmt);
                }
            }
        }

		public T ExecuteScalar<T> ()
		{
			if (_conn.Trace) {
				Debug.WriteLine ("Executing Query: " + this);
			}
            T result = default(T);
		    using (SqlCommand cmd = new SqlCommand(CommandText, _conn.Connection))
		    {
		        if (Parameters.Any()) cmd.Parameters.AddRange(Parameters.ToArray());
		        if (_conn.Transaction != null)
		            cmd.Transaction = _conn.Transaction;
		        
		        SqlDataReader reader = null;
		        try
		        {
		            reader = cmd.ExecuteReader();
		            reader.Read();
		            result = (T) ReaderCol(reader, 0, typeof (T), null);
		            reader.Close();
		        }
		        catch (InvalidExpressionException)
		        {
		            throw new ApplicationException("A data reader has not been closed so a second one cannot be used yet.");
		        }
		        catch (NullReferenceException ex)
		        {
		            Console.WriteLine(ex);
		            return default(T);
		        }
		        catch (InvalidOperationException ex)
		        {
                    // This happens when there is no data present
                    Console.WriteLine(ex);
                    return default(T);
		        }
		        catch (Exception)
		        {
		            throw;
		        }
		        finally
		        {
		            if (reader != null && !reader.IsClosed)
		            {
		                reader.Close();
		            }
		        }
		    }
		    return result;
		}

	    private object ReaderCol(SqlDataReader reader, int index, Type clrType, Type myType)
	    {
            if (reader.IsDBNull(index))
            {
                return null;
            }
	        var res = reader.GetValue(index);

	        if (clrType == typeof (String))
	        {
	            return reader.GetValue(index);
	        }
	        else if (clrType == typeof (Int32))
	        {
	            return reader.GetInt32(index);
	        }
            else if (clrType == typeof(Int64))
            {
                if (myType == typeof (Int32))
                    return (Int64)reader.GetInt32(index);
                return reader.GetInt64(index);
            }
            else if (clrType == typeof(decimal))
            {
                if (myType == typeof(double))
                    return (decimal)reader.GetDouble(index);
                return reader.GetDecimal(index);
            }
	        else if (clrType == typeof (Boolean))
	        {
	            return reader.GetBoolean(index);
	        }
	        else if (clrType == typeof (double))
	        {
	            return reader.GetDouble(index);
	        }
	        else if (clrType == typeof (float))
	        {
	            return reader.GetFloat(index);
	        }
	        else if (clrType == typeof (DateTime))
	        {
	            return reader.GetDateTime(index);
	        }
	        return null;
	    }

	}

}
