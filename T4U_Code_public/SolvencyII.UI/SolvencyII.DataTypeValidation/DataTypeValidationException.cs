using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.DataTypeValidation
{
    public class DataTypeValidationException: Exception
    {
        /// <summary>
        /// Excel range address where error occured
        /// </summary>
        public string ExcelAddress { get; set; }

        public DataTypeValidationException(
            string auxMessage, Exception inner) :
            base(auxMessage, inner)
        {

        }
    }
}
