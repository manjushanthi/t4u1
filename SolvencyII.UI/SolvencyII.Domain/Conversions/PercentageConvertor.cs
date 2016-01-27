using System.Globalization;

namespace SolvencyII.Domain.Conversions
{
    /// <summary>
    /// Convertor to and from percentage strings in a culturally appropriate way.
    /// </summary>
    public static class PercentageConvertor
    {
        public static string PercentageStringToDBString(string value)
        {
            string stripped = value.Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "");
            decimal temp2 = decimal.Parse(stripped, NumberStyles.Any, CultureInfo.CurrentCulture);
            if (temp2 != 0)
            {
                if (value.IndexOf(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol) != -1)
                    temp2 = temp2/100;
                return temp2.ToString(CultureInfo.InvariantCulture);
            }
            if (string.IsNullOrEmpty(value))
                return "";
            return "0";
        }

        public static decimal? PercentageStringToDecimal(string text)
        {
            string temp = text.Replace(CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "");
            decimal result;
            if(decimal.TryParse(temp, NumberStyles.Any, CultureInfo.CurrentCulture, out result))
                return result / 100;
            return null;
        }

    }
}
