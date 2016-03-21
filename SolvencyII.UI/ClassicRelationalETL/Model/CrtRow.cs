using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SolvencyII.Data.CRT.ETL.Model
{
    /// <summary>
    /// Represnets single ro of valus in single CRt table row
    /// </summary>
    public class CrtRow
    {
        public CrtRowIdentification rowIdentification;

        public Dictionary<string, object> rcColumnsValues;
        public Queue<CrtMapping> contextMappings;
        public HashSet<CrtMapping> factMapings;

        /// <summary>
        /// Initializes a new instance of the <see cref="CrtRow"/> class.
        /// </summary>
        /// <param name="rowIdentification">The row identification.</param>
        /// <param name="rcColumnsValues">The rc columns values.</param>
        public CrtRow(CrtRowIdentification rowIdentification, Dictionary<string, object> rcColumnsValues)
        {
            this.rowIdentification = rowIdentification;
            this.rcColumnsValues = rcColumnsValues;
        }

        public override int GetHashCode()
        {
            string code = "";

            if (rowIdentification != null)
                code = code + rowIdentification.GetHashCode();
            if (rcColumnsValues != null)
                code = code + rcColumnsValues.GetHashCode();

            return code.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (!(obj is CrtRow))
                return false;

            CrtRow objIns = obj as CrtRow;

            if (!this.rowIdentification.Equals(objIns.rowIdentification))
                return false;
            if (!this.rcColumnsValues.Equals(objIns.rcColumnsValues))
                return false;

            return base.Equals(obj);
        }

        public override string ToString()
        {
            if(this.rowIdentification == null)
                return base.ToString();

            StringBuilder sb = new StringBuilder(this.rowIdentification.TABLE_NAME);
            sb.Append(" comtext ");

            foreach (DictionaryEntry item in this.rowIdentification.contextColumnsValues)
                sb.Append(" ").Append(item.Key).Append(":").Append(item.Value);

            sb.Append(" facts ");
            foreach (var item in this.rcColumnsValues)
                sb.Append(" ").Append(item.Key).Append(":").Append(item.Value);

            return sb.ToString();
        }

        /// <summary>
        /// Gets the col string value.
        /// </summary>
        /// <param name="columnName">Name of the column.</param>
        /// <returns></returns>
        internal string getColStringValue(string columnName)
        {
            object value = null;

            this.rcColumnsValues.TryGetValue(columnName, out value);

            if (value == null && this.rowIdentification != null)
            {
                foreach (DictionaryEntry kvp in this.rowIdentification.contextColumnsValues)
                    if (kvp.Key.Equals(columnName))
                    {
                        value = kvp.Value;
                        break;
                    }
            }

            if (value == null)
                return null;

            if (value is DateTime)
                return ((DateTime)value).ToShortDateString();
            if (value is bool)
                return ((bool)value) == true ? "1" : "0";

            return value.ToString();
        }
    }
}
