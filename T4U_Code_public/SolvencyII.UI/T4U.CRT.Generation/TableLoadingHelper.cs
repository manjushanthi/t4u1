using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using T4U.CRT.Generation.Model;
using T4U.CRT.Generation;
using System.Data;

namespace T4U.CRT.Generation
{
    public class SQLiteTableLoadingHelper : ITableLoadingHelper
    {
        protected ClassicRelationalTable _table;
        IDefaultMemberResolver _defResolver;
        
        public SQLiteTableLoadingHelper(ClassicRelationalTable table,  IDefaultMemberResolver defaultResolver)
        {
            this._table = table;
            this._defResolver = defaultResolver;
        }

        protected virtual string ddlStart {get{ return "CREATE TABLE [T__{0}] (PK_ID INTEGER PRIMARY KEY, INSTANCE INTEGER ";}}

        public string getTableDDL()
        {
            if (_table.Columns.Count == 0)
                throw new ArgumentNullException("No clumns in table");

            HashSet<string> addedColumns = new HashSet<string>();
            //string sparse = "";
            StringBuilder DDL = new StringBuilder(string.Format(ddlStart, _table.Name.Trim()));
            foreach (IRelationalColumn col in _table.Columns)
            {
                if (col == null || !col.isInTable(_table) )
                    continue;

                if (addedColumns.Contains(col.getColumnName().ToUpper()))
                    continue;

                DDL.Append(", [").Append(col.getColumnName().Trim().ToUpper()).Append("] ").Append(getDDLDataType(col));
                addedColumns.Add(col.getColumnName().ToUpper());
            }
            DDL.Append(this.getUniqueConstraint()).Append(");");
            return DDL.ToString();
        }

        protected virtual string getUniqueConstraint()
        {
            if(this._table.getUniqueColumns().Count() == 0)
                return ", UNIQUE ( INSTANCE )";

            string uk = ", UNIQUE ( INSTANCE, ";
            HashSet<string> added = new HashSet<string>();
            foreach (var col in _table.getUniqueColumns())
                if (!added.Contains(col.getColumnName()))
                {
                    uk = uk + col.getColumnName() + " , ";
                    added.Add(col.getColumnName());
                }

            
            uk = uk.Substring(0, uk.Length - 2);
            uk = uk + ")";
            return uk;
        }

        public HashSet<string> getMappingInserts()
        {
            HashSet<string> inserts = new HashSet<string>();            
            foreach (IRelationalColumn col in this._table.Columns)
            {
                if (col == null)
                    continue;

                this.getMappingInserts(col).ToList().ForEach(x => { if (!string.IsNullOrEmpty(x)) inserts.Add(x); });
            }
            return inserts;
        }

        private HashSet<string> getMappingInserts(IRelationalColumn col)
        {
            if (col == null)
                return null;
            
            HashSet<string> retVal = new HashSet<string>(); string TABLE_VERSION_ID, DYN_TABLE_NAME, DYN_TAB_COLUMN_NAME, DIM_CODE, MEM_CODE, DOM_CODE, ORIGIN, REQUIRED_MAPPINGS, DATA_TYPE, IS_IN_TABLE, PAGE_COLUMNS_NUMBER;

            foreach (string mappingCode in col.getColumnMappingDimCodes())
            {
                TABLE_VERSION_ID = _table.TableVersionId.ToString();
                DYN_TABLE_NAME = _table.Name;
                DYN_TAB_COLUMN_NAME = col.getColumnName();
                DIM_CODE = mappingCode;
                MEM_CODE = col.getMemberCodeForDimCode(mappingCode);
                DOM_CODE = col.getDomCodeForDimCode(mappingCode);
                ORIGIN = col.getOrigin();
                REQUIRED_MAPPINGS = col.getRequiredMappingsNumber(_table).ToString();
                DATA_TYPE = getMappingDataType(col);
                IS_IN_TABLE = (col.isInTable(_table) ? "1" : "0");
                PAGE_COLUMNS_NUMBER = _table.pageColumnsNumber.ToString();

                retVal.Add(@"insert into MAPPING
                (TABLE_VERSION_ID, DYN_TABLE_NAME, DYN_TAB_COLUMN_NAME, DIM_CODE, MEM_CODE, DOM_CODE, ORIGIN, REQUIRED_MAPPINGS, DATA_TYPE, IS_PAGE_COLUMN_KEY, IS_IN_TABLE, PAGE_COLUMNS_NUMBER, IS_DEFAULT)  
                values
                (" + TABLE_VERSION_ID + ", '" + DYN_TABLE_NAME + "', '"
                   + DYN_TAB_COLUMN_NAME + "', '" + DIM_CODE  + "', '"
                   + MEM_CODE + "', '" + DOM_CODE + "', '"
                   + ORIGIN + "', " + REQUIRED_MAPPINGS + ", '"
                   + DATA_TYPE + "' , 0, " + IS_IN_TABLE + " , " + PAGE_COLUMNS_NUMBER + " , " + (_defResolver.isDefault(mappingCode) ? "1" : "0") + ")");
            }

            foreach (string mappingCode in col.getColumnMappingDimKeyCodes())
            {
                TABLE_VERSION_ID = _table.TableVersionId.ToString();
                DYN_TABLE_NAME = _table.Name;
                DYN_TAB_COLUMN_NAME = col.getColumnName();
                DIM_CODE = mappingCode;
                MEM_CODE = col.getMemberCodeForDimCode(mappingCode);
                DOM_CODE = col.getDomCodeForDimCode(mappingCode);
                ORIGIN = col.getOrigin();
                REQUIRED_MAPPINGS = col.getRequiredMappingsNumber(_table).ToString();
                DATA_TYPE = getMappingDataType(col);
                IS_IN_TABLE = (col.isInTable(_table) ? "1" : "0");
                PAGE_COLUMNS_NUMBER = _table.pageColumnsNumber.ToString();

                retVal.Add(@"insert into MAPPING
                (TABLE_VERSION_ID, DYN_TABLE_NAME, DYN_TAB_COLUMN_NAME, DIM_CODE, MEM_CODE, DOM_CODE, ORIGIN, REQUIRED_MAPPINGS, DATA_TYPE, IS_PAGE_COLUMN_KEY, IS_IN_TABLE, PAGE_COLUMNS_NUMBER, IS_DEFAULT)  
                values
                (" + TABLE_VERSION_ID + ", '" + DYN_TABLE_NAME + "', '"
                   + DYN_TAB_COLUMN_NAME + "', '" + DIM_CODE + "', '"
                   + MEM_CODE + "', '" + DOM_CODE + "', '"
                   + ORIGIN + "', " + REQUIRED_MAPPINGS + ", '"
                   + DATA_TYPE + "' , 1, " + IS_IN_TABLE + " , " + PAGE_COLUMNS_NUMBER + " , " + (_defResolver.isDefault(mappingCode) ? "1" : "0" )+ ")");
            }
            return retVal;
        }

        private string getMappingDataType(IRelationalColumn col)
        {
            switch (col.getDataType())
            {
                case IRelationColumnDataType.Monetary:
                    return "M";
                case IRelationColumnDataType.String:
                    return "S";
                case IRelationColumnDataType.Percentage:
                    return "P";
                case IRelationColumnDataType.Decimal:
                    return "N";
                case IRelationColumnDataType.Boolean:
                    return "B";
                case IRelationColumnDataType.Date:
                    return "D";
                case IRelationColumnDataType.Integer:
                    return "I";
                case IRelationColumnDataType.Enumeration:
                    return "E";
                default:
                    return "S";
            }
        }

        protected virtual string getDDLDataType(IRelationalColumn col)
        {
            switch (col.getDataType())
            {
                case IRelationColumnDataType.Monetary:
                    return "NUMERIC";
                case IRelationColumnDataType.String:
                    return "VARCHAR";
                case IRelationColumnDataType.Percentage:
                    return "NUMERIC";
                case IRelationColumnDataType.Decimal:
                    return "NUMERIC";
                case IRelationColumnDataType.Boolean:
                    return "BOOLEAN";
                case IRelationColumnDataType.Date:
                    return "DATE";
                case IRelationColumnDataType.Integer:
                    return "NUMERIC";
                case IRelationColumnDataType.Enumeration:
                    return "VARCHAR";
                default:
                    return "VARCHAR";
            }
        }

        public string getQueryToCheckifTableIsInDB()
        {
            string querz = @"select * from sqlite_master where name like 'T__" + this._table.Name + "'"; ;
            return querz;
        }

        public string getQueryToGetMappings()
        {
            return @"select * from mapping_table where  DYN_TABLE_NAME = '" + _table.Name + "'";
        }

        public static string getMappingTableDDL()
        {
            return @"CREATE TABLE MAPPING ( 
                        TABLE_VERSION_ID    NUMERIC,
                        DYN_TABLE_NAME      VARCHAR,
                        DYN_TAB_COLUMN_NAME VARCHAR,
                        DIM_CODE            VARCHAR,
                        DOM_CODE            VARCHAR,
                        MEM_CODE            VARCHAR,
                        ORIGIN              VARCHAR,
                        REQUIRED_MAPPINGS   INTEGER,
                        PAGE_COLUMNS_NUMBER INTEGER,
                        DATA_TYPE           VARCHAR,
                        IS_PAGE_COLUMN_KEY  NUMERIC,
                        IS_DEFAULT          NUMERIC,
                        IS_IN_TABLE         NUMERIC
                    );
                    CREATE INDEX idx_MAPPING_COL_NAME ON MAPPING ( 
                        DYN_TAB_COLUMN_NAME ASC 
                    );

                    CREATE INDEX idx_MAPPING_DIM_CODE ON MAPPING ( 
                        DIM_CODE ASC 
                    );

                    CREATE INDEX idx_MAPPING_REQ_MAPP ON MAPPING ( 
                        REQUIRED_MAPPINGS 
                    );

                    CREATE INDEX idx_MAPPING_TABLE_NAME ON MAPPING ( 
                        DYN_TABLE_NAME ASC 
                    );";
        }
        
        public static string mappingTableExistsQuery()
        {
            return @"SELECT name FROM sqlite_master WHERE type='table' AND name='MAPPING'";
        }

        public static string doeasTableExistsQuery(string tableName)
        {
            return @"SELECT name FROM sqlite_master WHERE type='table' AND name='"+tableName+"'";
        }

        public string getProcesingTableDDL()
        {
            if (_table.Columns.Count == 0)            
            throw new ArgumentNullException("No clumns in table");

            string DDL = "CREATE TABLE [P___" + _table.Name + "] (INSTANCE INTEGER, COLUM_NAME VARCHAR, ROW_NUMBER INTEGER , VALUE VARCHAR ";

            foreach (IRelationalColumn col in _table.Columns)
            {
                if (col == null || !(col is PageColumn) || !col.isInTable(_table))
                    continue;

                DDL = DDL + ", [" + col.getColumnName().ToUpper() + "] VARCHAR ";
            }

            DDL = DDL + ");";

            return DDL;
        }

        public string getTableDrop()
        {
            return string.Format("drop table T__{0}", _table.Name);
        }

        public static string getmappingtableDrop()
        {
            return "drop table MAPPING";
        }

        public string getQueryToCheckifProcessTableIsInDB()
        {
            return @"SELECT name FROM sqlite_master WHERE type='table' AND name='P___" + _table.Name + "'";
        }

        public string getProcessingTableDrop()
        {
            return "drop table [P___" + _table.Name + "]";
        }
    }
}
