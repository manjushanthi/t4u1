using System;
using System.Collections.Generic;
using System.Linq;

namespace SolvencyII.Domain.Extensions
{
    /// <summary>
    /// Extension of DateTime - centralised dates to text
    /// </summary>
    public static class DateTimeExt
    {
        public static string ConvertToDateTimeString(this DateTime? myDate)
        {
            string result = "";
            if (myDate != null)
            {
                result = ConvertToDateTimeString((DateTime)myDate);
            }
            return result;
        }
        public static string ConvertToDateTimeString(this DateTime myDate)
        {
            return string.Format("{0:yyyy-MM-dd HH:mm:ss}", myDate);
        }
        public static string ConvertToDateString(this DateTime? myDate)
        {
            string result = "";
            if (myDate != null)
            {
                result = ConvertToDateString((DateTime)myDate);
            }
            return result;
        }
        public static string ConvertToDateString(this DateTime myDate)
        {
            return string.Format("{0:yyyy-MM-dd}", myDate);
        }

        public static DateTime NearestQuarterEnd(this DateTime date)
        {
            IEnumerable<DateTime> candidates =
                QuartersInYear(date.Year).Union(QuartersInYear(date.Year - 1));
            return candidates.Where(d => d < date.Date).OrderBy(d => d).Last();
        }

        static IEnumerable<DateTime> QuartersInYear(int year)
        {
            return new List<DateTime>() {
                new DateTime(year, 3, 31),
                new DateTime(year, 6, 30),
                new DateTime(year, 9, 30),
                new DateTime(year, 12, 31),
            };
        }

        public static DateTime GetQuarter(this DateTime date)
        {
            if (date.Month >= 4 && date.Month <= 6)
                return new DateTime(date.Year, 6, 30);
            else if (date.Month >= 7 && date.Month <= 9)
                return new DateTime(date.Year, 9, 30);
            else if (date.Month >= 10 && date.Month <= 12)
                return new DateTime(date.Year, 12, 31);
            else
                return new DateTime(date.Year, 3, 31);

        }
    }
}
