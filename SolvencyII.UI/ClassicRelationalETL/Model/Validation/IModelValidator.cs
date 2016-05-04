using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.Model.Validation
{
    public interface IModelValidator<T> where T : class
    {        
        IModelValidationResult<T> validate(IEnumerable<T> modelObjects);
    }

    public interface IModelValidationResult<T> where T : class
    {
        IEnumerable<T> validObjects { get; }
        IEnumerable<T> inValidObjects { get; }
        IEnumerable<CrtError> errors { get; }
    }
}
