using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.Model.Validation
{
    public class ModelValidationResult<T> : IModelValidationResult<T> where T : class
    {
        public IEnumerable<T> validObjects
        {
            get;
            set;    
        }

        public IEnumerable<T> inValidObjects
        {
            get;
            set; 
        }

        public IEnumerable<CrtError> errors
        {
            get;
            set; 
        }
    }
}
