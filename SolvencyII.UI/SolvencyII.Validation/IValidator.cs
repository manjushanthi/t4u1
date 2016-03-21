using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SolvencyII.Validation.Model;

namespace SolvencyII.Validation
{
    public interface IValidator
    {
        void Initialize(long instanceID);
        void Validate(long instanceID);
        void Validate(long instanceID, string tableCode);
        void ValidateAsync(long instanceID);
        void ValidateAsync(long instanceID, string tableCode);
        IEnumerable<ValidationError> SerializeErrors();

    }
}
