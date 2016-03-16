using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SolvencyII.Domain.ENumerators;

namespace SolvencyII.UI.Shared.Validators
{
    /// <summary>
    /// Mechanism for filtering key presses on type specific text boxes.
    /// </summary>
    public static class CharacterValidator
    {
        private static char decimalPoint = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator[0];
        private static char thousandChar = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator[0];
        private static char percentChar = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.PercentSymbol[0];
        private static char currencyChar = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol[0];
        private static char negativeChar = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NegativeSign[0];
        private static char underscoreChar = '_';
        private static char spaceChar = ' ';


        #region OnKeyPress

        public static bool OnKeyPressHandled(char checkChar, SolvencyDataType columnType)
        {
            if (Char.IsControl(checkChar)) return false;
            switch (columnType)
            {
                case SolvencyDataType.Monetry:
                    return MonetryCharCheck(checkChar);
                case SolvencyDataType.Integer:
                    return IntegerCharCheck(checkChar);
                case SolvencyDataType.Percentage:
                    return PercentCharCheck(checkChar);
                case SolvencyDataType.Decimal:
                    return NumericCharCheck(checkChar);
                case SolvencyDataType.String:
                    return StringCharCheck(checkChar);

                default:
                    return false; // All things are permitted.
            }
        }


        private static bool NumericCharCheck(char checkChar)
        {
            if (Char.IsNumber(checkChar) || checkChar == decimalPoint || checkChar == thousandChar || checkChar == negativeChar)
            {
                return false;
            }
            return true;
        }

        private static bool PercentCharCheck(char checkChar)
        {
            if (Char.IsNumber(checkChar) || checkChar == decimalPoint || checkChar == thousandChar || checkChar == percentChar || checkChar == negativeChar)
            {
                return false;
            }
            return true;
        }

        private static bool IntegerCharCheck(char checkChar)
        {
            if (Char.IsNumber(checkChar) || checkChar == negativeChar)
            {
                return false;
            }
            return true;
        }

        private static bool MonetryCharCheck(char checkChar)
        {
            if (Char.IsNumber(checkChar) || checkChar == decimalPoint || checkChar == thousandChar || checkChar == currencyChar || checkChar == negativeChar)
            {
                return false;
            }
            return true;
        }

        private static bool StringCharCheck(char checkChar)
        {
            return false;
            // Updated 11/05/2015 to allow testing of all characters through parameterised queries

            if (Char.IsLetterOrDigit(checkChar) || checkChar == spaceChar || checkChar == negativeChar || checkChar == underscoreChar)
            {
                return false;
            }
            return true;
        }

        #endregion

    }
}
