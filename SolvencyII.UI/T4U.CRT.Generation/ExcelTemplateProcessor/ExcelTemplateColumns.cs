using AT2DPM.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T4U.CRT.Generation.ExcelTemplateProcessor
{
    public class ExcelTemplateColumns
    {

        #region Properties


        private List<string> _rows;
        public List<string> Rows
        {
            get
            {
                return _rows;
            }
            set
            {
                _rows = value;
            }
        }

        private List<string> _columns;

        public List<string> columns
        {
            get
            {
                return _columns;
            }
            set
            {
                _columns = value;
            }
        }

        private List<string> _columnsCodes;

        public List<string> ColumnsCodes
        {
            get
            {
                return _columnsCodes;
            }
            set
            {
                _columnsCodes = value;
            }
        }

        private List<string> _datatypes;

        public List<string> DataTypes
        {
            get
            {
                return _datatypes;
            }
            set
            {
                _datatypes = value;
            }
        }
        
       
        #endregion


    }
}
