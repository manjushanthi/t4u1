using System;
using System.Globalization;

namespace SolvencyII.Domain.Extensions
{
    /// <summary>
    /// Object extensions to create formatted string.
    /// </summary>
    public static class ObjectExt
    {
        public static string DecimalToString(this object value, CultureInfo culture, int decimalPlaces)
        {
            decimal dbe = Convert.ToDecimal(value, culture);

            // Check to see if there are no decimal places
            // if (Math.Abs(dbe % 1) == 0) return dbe.ToString("N2");

            decimal power = (decimal) Math.Pow(10, decimalPlaces);
            decimal test = Math.Round(dbe * power);
            if((dbe*power) == test)
                return dbe.ToString(string.Format("N{0}", decimalPlaces));
            return dbe.ToString(string.Format("N{0}", decimalPlaces)) + "~";
        }

        public static string PercentageToString(this object value, CultureInfo culture)
        {
            // value = value
            decimal trueDbeP = Convert.ToDecimal(value, culture);
            // This accommodates the 100 multiplier for the percentage value - we need 2 decimal places but truely have 4
            return trueDbeP.ToString("P2") + (((trueDbeP*10000 - Math.Round(trueDbeP*10000)) != 0) ? "~" : "");
        }
    }
}
