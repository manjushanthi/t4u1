using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SolvencyII.Data.CRT.ETL.Model
{
    /// <summary>
    /// Mapping of the column with crt mapping
    /// </summary>
    public class ColumnMapping
    {
        internal readonly string DYN_TABLE_NAME;
        internal readonly string DYN_TAB_COLUMN_NAME;

        private List<CrtMapping> _mappings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnMapping"/> class.
        /// </summary>
        /// <param name="dynTableName">Name of the dyn table.</param>
        /// <param name="dyntableColumnName">Name of the dyntable column.</param>
        public ColumnMapping(string dynTableName, string dyntableColumnName)
        {
            this.DYN_TABLE_NAME = dynTableName;
            this.DYN_TAB_COLUMN_NAME = dyntableColumnName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnMapping"/> class.
        /// </summary>
        /// <param name="factColMaps">The fact col maps.</param>
        public ColumnMapping(ColumnMapping factColMaps)
        {
            this.DYN_TAB_COLUMN_NAME = factColMaps.DYN_TAB_COLUMN_NAME;
            this.DYN_TABLE_NAME = factColMaps.DYN_TABLE_NAME;
            this._mappings = new List<CrtMapping>();

            foreach (CrtMapping  map in factColMaps.Mappings)            
                this._mappings.Add(map);            
        }

        /// <summary>
        /// Gets or sets the mappings.
        /// </summary>
        /// <value>
        /// The mappings.
        /// </value>
        public List<CrtMapping> Mappings
        {
            get
            {
                return _mappings;
            }
            set
            {
                _mappings = value;
            }            
        }

        string _dpCode;
        /// <summary>
        /// Gets the data point code.
        /// </summary>
        /// <value>
        /// The data point code.
        /// </value>
        internal string DataPointCode
        {
            get
            {
                if (!string.IsNullOrEmpty(_dpCode))
                    return _dpCode;

                CrtMapping metMap = this.getMetricMapping();
                List<string> orderedDimMappings;
                if (metMap == null)
                    orderedDimMappings = _mappings.Where(x=>!x.IS_DEFAULT).Select(x => x.DIM_CODE).ToList();
                else
                    orderedDimMappings = _mappings.Where(x => x != metMap).Where(x => !x.IS_DEFAULT).Select(x => x.DIM_CODE).ToList();

                StringBuilder dpCode = new StringBuilder(metMap == null ? "" : metMap.DIM_CODE);
                orderedDimMappings.Sort();

                foreach (string map in orderedDimMappings)
                       dpCode.Append(map);

                orderedDimMappings = null;
                metMap = null;
                _dpCode = dpCode.ToString();

                return _dpCode;
            }
        }

        /// <summary>
        /// Gets the metric mapping.
        /// </summary>
        /// <returns></returns>
        internal CrtMapping getMetricMapping()
        {
            if (_mappings == null || _mappings.Count == 0)
                return null;

            CrtMapping metMap = _mappings.SingleOrDefault(x => x.DIM_CODE.Contains(EtlGlobals.MetDimCode));
            if(metMap == null)
                metMap = _mappings.SingleOrDefault(x => x.DIM_CODE.Contains(EtlGlobals.AtyDimCode));

            return metMap;
        }
        
        public override int GetHashCode()
        {
            return (this.DYN_TAB_COLUMN_NAME + DYN_TABLE_NAME + DataPointCode).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (!(obj is ColumnMapping))
                return false;
            ColumnMapping rmObj = obj as ColumnMapping;
            if (!this.DYN_TABLE_NAME.Equals(rmObj.DYN_TABLE_NAME))
                return false;
            if (!this.DYN_TAB_COLUMN_NAME.Equals(rmObj.DYN_TAB_COLUMN_NAME))
                return false;
            if (!this.DataPointCode.Equals(rmObj.DataPointCode))
                return false;

            return true;
        }
    }
}
