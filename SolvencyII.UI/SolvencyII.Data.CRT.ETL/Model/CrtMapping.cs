using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SolvencyII.Data.CRT.ETL.Model
{
    /// <summary>
    /// Represents single row in MAPPING table in T4U DB
    /// </summary>
    public class CrtMapping
    {
        public int TABLE_VERSION_ID;
        public string DYN_TABLE_NAME;
        public string DYN_TAB_COLUMN_NAME;
        public string DIM_CODE;
        public string DOM_CODE;
        public string MEM_CODE;
        public string ORIGIN;
        public int REQUIRED_MAPPINGS;
        public int PAGE_COLUMNS_NUMBER;
        public string DATA_TYPE;
        public int IS_PAGE_COLUMN_KEY;
        public bool IS_IN_TABLE;
        public bool IS_DEFAULT;

        public override string ToString()
        {
            return TABLE_VERSION_ID + "_" + DYN_TABLE_NAME + "_" + DYN_TAB_COLUMN_NAME + "_" + DIM_CODE;
        }

        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;
            if (!(obj is CrtMapping))
                return false;
            CrtMapping objMap = obj as CrtMapping;
            if (this.TABLE_VERSION_ID != objMap.TABLE_VERSION_ID)
                return false;
            if (!this.DYN_TABLE_NAME.Equals(objMap.DYN_TABLE_NAME))
                return false;
            if (!this.DYN_TAB_COLUMN_NAME.Equals(objMap.DYN_TAB_COLUMN_NAME))
                return false;
            if (!this.DIM_CODE.Equals(objMap.DIM_CODE))
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            StringBuilder build = new StringBuilder();
            build.Append(this.TABLE_VERSION_ID.ToString());
            build.Append(DYN_TABLE_NAME);
            build.Append(DYN_TAB_COLUMN_NAME);
            build.Append(DIM_CODE);
            return  build.ToString().GetHashCode();
        }

        int ___tabularHashCode = -1;
        internal int GetTabularLocationHashCode()
        {
            if (___tabularHashCode != -1)
                return ___tabularHashCode;

            return (string.Format("{0}{1}", this.DYN_TABLE_NAME, this.DYN_TAB_COLUMN_NAME)).GetHashCode();
        }

        /// <summary>
        /// Gets the tabular location.
        /// </summary>
        /// <returns></returns>
        internal ColumnTableLocation GetTabularLocation()
        {
            return new ColumnTableLocation(DYN_TABLE_NAME, DYN_TAB_COLUMN_NAME);
        }

        private bool? _isPageColumn = null;
        /// <summary>
        /// Gets a value indicating whether this instance is page column.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is page column; otherwise, <c>false</c>.
        /// </value>
        public bool isPageColumn { 
            get
            {
                if (_isPageColumn != null)
                    return _isPageColumn == true ? true : false;

                if (!string.IsNullOrEmpty(this.DYN_TAB_COLUMN_NAME) && this.DYN_TAB_COLUMN_NAME.StartsWith("PA"))
                    _isPageColumn = true;
                else
                    _isPageColumn = false;

                return _isPageColumn == true ? true : false;;
            }

        }
    }

    /// <summary>
    /// Table location of the colums
    /// </summary>
    public class ColumnTableLocation
    {
        public readonly string DYN_TAB_COLUMN_NAME;
        public readonly string DYN_TABLE_NAME;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnTableLocation"/> class.
        /// </summary>
        /// <param name="DYN_TABLE_NAME">Name of the dy n_ tabl e_.</param>
        /// <param name="DYN_TAB_COLUMN_NAME">Name of the dy n_ ta b_ colum n_.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        public ColumnTableLocation(string DYN_TABLE_NAME, string DYN_TAB_COLUMN_NAME)
        {
            if (DYN_TABLE_NAME == null || DYN_TAB_COLUMN_NAME == null)
                throw new ArgumentNullException();

            this.DYN_TAB_COLUMN_NAME = DYN_TAB_COLUMN_NAME;
            this.DYN_TABLE_NAME = DYN_TABLE_NAME;
        }

        int _hashCode;
        bool wasHashed = false;
        public override int GetHashCode()
        {
            if (wasHashed)
                return _hashCode;

            _hashCode = string.Format("{0}{1}", this.DYN_TABLE_NAME, this.DYN_TAB_COLUMN_NAME).GetHashCode();
            wasHashed = true;
            return _hashCode;
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (!(obj is ColumnTableLocation))
                return false;
            if (!this.GetHashCode().Equals(obj.GetHashCode()))
                return false;
            
            return true;           
        }
    }
}
