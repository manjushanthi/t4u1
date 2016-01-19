using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetOffice.ExcelApi;

using SolvencyII.Domain;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Interfaces;
using SolvencyII.ExcelImportExportLib.Dto;
using SolvencyII.ExcelImportExportLib.Domain;
using SolvencyII.ExcelImportExportLib.Exceptions;

namespace SolvencyII.ExcelImportExportLib.Transform
{
    public abstract class TransformBase
    {
        public abstract void Transform(ISolvencyData sqliteConnection, Worksheet workSheet, AbstractTransferObject dto);

        protected ExcelExportValidationMessage Validate(string value, string type, out string  transformedValue)
        {
            ExcelExportValidationMessage validationMessage = null;

            if (type.ToUpper().Trim() == "D")
            {
                try
                {
                    if (value != string.Empty)
                        transformedValue = DateTime.Parse(value).ToShortDateString();
                }
                catch (FormatException fe)
                {
                    //The data for the row/column/table with value is not having the expeted format. Please run the Validate container function in the menu validate. 
                    validationMessage = new ExcelExportValidationMessage();
                    validationMessage.Value = value;
                    validationMessage.FieldType = type;
                }
            }
            else if (type.ToUpper().Trim() == "B")
            {
                if (value != string.Empty)
                {
                    if (!(value.Trim().ToUpper() == "TRUE" || value.Trim().ToUpper() == "FALSE"))
                    {
                        //The data for the row/column/table with value is not having the expeted format. Please run the Validate container function in the menu validate. Boolean Data has exported as "false" value                                
                        validationMessage = new ExcelExportValidationMessage();
                        validationMessage.Value = value;
                        validationMessage.FieldType = type;
                    }
                }
            }
            else if (type.ToUpper().Trim() == "D" || type.ToUpper().Trim() == "M")
            {
                decimal devnull;
                if (value != string.Empty)
                {
                    if (!decimal.TryParse(value, out devnull))
                    {

                        //we are missing a decimal or monetary
                        //Export the value as in the database
                        //The data for the row/column/table with value is not having the expeted format. Please run the Validate container function in the menu validate.
                        validationMessage = new ExcelExportValidationMessage();
                        validationMessage.Value = value;
                        validationMessage.FieldType = type;

                    }
                }
            }
            else if (type.ToUpper().Trim() == "I")
            {
                int devnull;
                if (value != string.Empty)
                {
                    if (!int.TryParse(value, out devnull))
                    {
                        //Export the value as in the database
                        //The data for the row/column/table with value is not having the expeted format. Please run the Validate container function in the menu validate.
                        validationMessage = new ExcelExportValidationMessage();
                        validationMessage.Value = value;
                        validationMessage.FieldType = type;
                    }
                }
            }

            transformedValue = string.Empty;

             return validationMessage;
        }

        public string ValidateData(SolvencyDataType type, string input)
        {
            string output = string.Empty;

            switch(type)
            {
                case SolvencyDataType.Boolean:
                    if (input != null)
                    {
                        if (input.ToString().ToUpper().Trim() == "TRUE")
                            output = "1";

                        else if (input.ToString().ToUpper().Trim() == "FALSE")
                            output = "0";
                    }
                    break;

            };

            return output;
        }

        public void ThrowError(SolvencyDataType type, Worksheet ws, Exception ex, int row, int col, string value)
        {
            StringBuilder sb = new StringBuilder();


            Range errorRange = ws.Cells[row, col];
            string address = errorRange.Address;

            sb.Append("An error occured while parsing the data.");

            switch (type)
            {
                case SolvencyDataType.Date:
                    sb.Append(string.Format("The value at address [0] is not an date type.", errorRange.Address));
                    break;

                case SolvencyDataType.Boolean:
                    sb.Append("The value at the row ").Append(row);
                    sb.Append(" column ").Append((char)(64 + col));

                    do
                    {
                        int index = address.IndexOf('$');

                        if (index >= 0)
                            address = address.Remove(index, 1);
                    } while (address.IndexOf('$') >= 0);


                    sb.Append("[Cell ").Append(address).Append("]: ");
                    sb.Append("\"").Append(value).Append("\"");
                    sb.Append(" is not a boolen type. ");
                    break;

                case SolvencyDataType.Code:
                    sb.Append("The value at the row ").Append(row);
                    sb.Append(" column ").Append((char)(64 + col));

                    do
                    {
                        int index = address.IndexOf('$');

                        if (index >= 0)
                            address = address.Remove(index, 1);
                    } while (address.IndexOf('$') >= 0);

                    sb.Append("[Cell ").Append(address).Append("]: ");
                    sb.Append("\"").Append(value).Append("\"");
                    sb.Append(" is not valid value for the column ").Append((char)(64 + col)).Append(row).Append(".");
                    break;

                case SolvencyDataType.Percentage:
                    sb.Append("The value at the row ").Append(row);
                    sb.Append(" column ").Append((char)(64 + col));


                    do
                    {
                        int index = address.IndexOf('$');

                        if (index >= 0)
                            address = address.Remove(index, 1);
                    } while (address.IndexOf('$') >= 0);


                    sb.Append("[Cell ").Append(address).Append("]: ");
                    sb.Append("\"").Append(value).Append("\"");
                    sb.Append(" is not a percentage type. ");
                    break;

                case SolvencyDataType.Monetry:
                    sb.Append(string.Format("The value at address [0] is not a decimal type.", address));
                    break;

                case SolvencyDataType.Integer:
                    sb.Append(string.Format("The value at address [0] is not a integer type.", address));
                    break;


            };

            sb.Append("Please correct the value and import again.");


            errorRange.Dispose();
            errorRange = null;

            throw new T4UExcelImportExportException(sb.ToString(), ex, address);

        }
    }
}
