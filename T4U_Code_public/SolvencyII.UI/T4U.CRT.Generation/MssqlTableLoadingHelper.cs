using T4U.CRT.Generation;
using T4U.CRT.Generation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T4U.CRT.MSSSQL.DDL.Generation
{
    class MssqlTableLoadingHelper : SQLiteTableLoadingHelper, ITableLoadingHelper
    {
        private ClassicRelationalTable table;
        private DefaultMemberResolver defaultMemberResolver;

        protected override string ddlStart
        {
            get
            {
                return @"IF OBJECT_ID('dbo.T__{0}', 'U') IS NOT NULL
BEGIN
	drop table T__{0};
END; 
CREATE TABLE [T__{0}] (PK_ID INTEGER IDENTITY(1,1) PRIMARY KEY, INSTANCE INTEGER ";
            }
        }

        public string getTableDDL()
        {
            if (_table.Columns.Count == 0)
                throw new ArgumentNullException("No clumns in table");

            HashSet<string> addedColumns = new HashSet<string>();
            string sparse = "";
            StringBuilder DDL = new StringBuilder(string.Format(ddlStart, _table.Name.Trim()));
            foreach (IRelationalColumn col in _table.Columns.Where(x => x is PageColumn))
            {
                if (col == null || !col.isInTable(_table))
                    continue;

                if (addedColumns.Contains(col.getColumnName().ToUpper()))
                    continue;

                if (addedColumns.Count() == 500)
                    sparse = " SPARSE ";

                DDL.Append(", [").Append(col.getColumnName().Trim().ToUpper()).Append("] ").Append(getDDLDataType(col)).Append(sparse);
                addedColumns.Add(col.getColumnName().ToUpper());
            }
            foreach (IRelationalColumn col in _table.Columns.Where(x=>x is RcColumn))
            {
                if (col == null || !col.isInTable(_table))
                    continue;

                if (addedColumns.Contains(col.getColumnName().ToUpper()))
                    continue;

                if (addedColumns.Count() == 500)
                    sparse = " SPARSE ";

                DDL.Append(", [").Append(col.getColumnName().Trim().ToUpper()).Append("] ").Append(getDDLDataType(col)).Append(sparse);
                addedColumns.Add(col.getColumnName().ToUpper());
            }
            if (!string.IsNullOrWhiteSpace(sparse)) DDL.Append(", SparseColumns XML COLUMN_SET FOR ALL_SPARSE_COLUMNS ");
            DDL.Append(this.getUniqueConstraint()).Append(");");
            return DDL.ToString();
        }
        
        public MssqlTableLoadingHelper(ClassicRelationalTable table, DefaultMemberResolver defaultMemberResolver)
            : base(table, defaultMemberResolver)
        {
            this.table = table;
            this.defaultMemberResolver = defaultMemberResolver;
        }

        protected override string getDDLDataType(IRelationalColumn col)
        {
            switch (col.getDataType())
            {
                case IRelationColumnDataType.Monetary:
                    return "NUMERIC";
                case IRelationColumnDataType.String:
                    return "VARCHAR(3999)";
                case IRelationColumnDataType.Percentage:
                    return "NUMERIC";
                case IRelationColumnDataType.Decimal:
                    return "NUMERIC";
                case IRelationColumnDataType.Boolean:
                    return "BIT";
                case IRelationColumnDataType.Date:
                    return "DATE";
                case IRelationColumnDataType.Integer:
                    return "NUMERIC";
                case IRelationColumnDataType.Enumeration:
                    return "VARCHAR(100)";
                default:
                    return "VARCHAR(3999)";
            }
        }

        protected override string getUniqueConstraint()
        {
            if (base._table.getUniqueColumns().Count() == 0)
                return ", FOREIGN KEY (INSTANCE) REFERENCES dbo.[dInstance](InstanceID), UNIQUE ( INSTANCE )";

            string uk = ", FOREIGN KEY (INSTANCE) REFERENCES dbo.[dInstance](InstanceID), UNIQUE ( INSTANCE, ";
            foreach (string col in base._table.getUniqueColumns().Select(x => x.getColumnName()).Distinct())
                uk = uk + col + " , ";

            uk = uk.Substring(0, uk.Length - 2);
            uk = uk + ")";
            return uk;
        }
    }
}
