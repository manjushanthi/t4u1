using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.DataTypeValidation
{
    public class DataTypeValidationInput
    {
        public string ConnectionString { get; set; }
        public long InstanceID { get; set; }

        public Action<string, int> ProgressChanges { get; set; }
        public Action<List<DataTypeValidationResult>, string, int> UpdateValidationResults { get; set; }

        public Action Completed { get; set; }
        public List<DataTypeValidationResult> resultSet { get; set; }

    }
}
