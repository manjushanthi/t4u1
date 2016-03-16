using System;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Extensions;


namespace SolvencyII.UI.Shared.Validators
{
    /// <summary>
    /// Validates user controls to ensure data integrity.
    /// </summary>
    internal static class SolvencyControlValidator
    {
        private const int MAX_TEXT_LENGTH = 15;

        public static bool IsValid(SolvencyDataType type, string controlText)
        {
            switch (type)
            {
                case SolvencyDataType.Boolean:
                    return controlText.IsBoolean();
                case SolvencyDataType.Date:
                    return controlText.IsDate();
                case SolvencyDataType.Integer:
                    return controlText.IsInteger() && IsWithinRange(SolvencyDataType.Integer, controlText);
                case SolvencyDataType.Monetry:
                    return controlText.IsNumeric() && IsWithinRange(SolvencyDataType.Monetry, controlText);
                case SolvencyDataType.Percentage:
                case SolvencyDataType.Decimal:
                    string stripped = controlText.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "");
                    return stripped.IsNumeric() && IsWithinRange(SolvencyDataType.Monetry, stripped);
                case SolvencyDataType.Code:
                case SolvencyDataType.String:
                    return true;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        private static bool IsNotTooLong(string controlText)
        {
            // Stores 15 significant figures;
            return controlText.Trim().Replace(",", "").Replace(".", "").Length <= MAX_TEXT_LENGTH;
        }

        private static bool IsWithinRange(SolvencyDataType dataType, string text)
        {
            switch (dataType)
            {
                case SolvencyDataType.Integer:
                    int? value = text.GetIntValue();
                    if (value == null) return true;
                    return (value <= int.MaxValue && value >= int.MinValue);
                case SolvencyDataType.Monetry:
                case SolvencyDataType.Percentage:
                case SolvencyDataType.Decimal:
                    decimal? valueDec = text.GetDecimalValue();
                    if (valueDec == null) return true;
                    return (valueDec <= decimal.MaxValue && valueDec >= decimal.MinValue);
                default:
                    return true;
            }
        }


    }
}
