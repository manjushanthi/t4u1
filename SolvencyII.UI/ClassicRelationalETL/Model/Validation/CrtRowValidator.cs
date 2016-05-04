using SSolvencyII.Data.CRT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.Model.Validation
{
    public class CrtRowValidator : IModelValidator<CrtRow>
    {        
        public IModelValidationResult<CrtRow> validate(IEnumerable<CrtRow> modelObjects)
        {
            List<CrtRow> rows = (modelObjects is List<CrtRow>) ? modelObjects as List<CrtRow> : modelObjects.ToList();
            Dictionary<CrtRow, List<CrtError>> errorToRow = validateRowsAndGetRowsWithErros(rows);
            IEnumerable<CrtError> errors = errorToRow.Values.SelectMany(x => x);
            foreach (var item in errorToRow.Keys)
                rows.Remove(item);

            return new ModelValidationResult<CrtRow>() { errors = errors, validObjects = rows};
        }

        private bool isValid(CrtRow row, out List<CrtError> errors)
        {
            errors = new List<CrtError>();
            foreach (string columnName in row.rowIdentification.contextColumnsValues.Keys)
            {
                string columnValue = row.rowIdentification.contextColumnsValues[columnName] as string;
                if (isPage(columnName) && string.IsNullOrWhiteSpace(columnValue))
                    errors.Add(new NullZAxisError(row.rowIdentification.TABLE_NAME, columnName, row.rowIdentification.INSTANCE));
            }

            return errors.Count == 0;
        }

        private Dictionary<CrtRow, List<CrtError>> validateRowsAndGetRowsWithErros(IEnumerable<CrtRow> rows)
        {
            Dictionary<CrtRow, List<CrtError>> result = new Dictionary<CrtRow, List<CrtError>>();
            List<CrtError> errors = null;
            foreach (CrtRow row in rows)
                if (!isValid(row, out errors))
                    result.Add(row, errors);

            return result;
        }

        private bool isPage(string columnName)
        {
            return string.IsNullOrWhiteSpace(columnName) ? false : columnName.StartsWith("PAGE");
        }
    }
}
