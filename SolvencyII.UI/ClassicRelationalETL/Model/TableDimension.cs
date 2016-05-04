using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.Model
{
    public class TableDimension
    {
        public string TableCode { get; set; }
        public string DimensionQname { get; set; }
        public string TableCrtCode { get; set; }
        public string TableID
        {
            get
            {
                return tableID.ToString();
            }
            set
            {
                tableID = int.Parse(value);
            }
        }

        int tableID = -1;

        public int GetTableID()
        {
            return tableID;
        }

        public TableDimension() { }        
    }
}
