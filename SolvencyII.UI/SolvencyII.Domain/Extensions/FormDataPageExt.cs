using System;
using SolvencyII.Data.Entities;

namespace SolvencyII.Domain.Extensions
{
    /// <summary>
    /// Extension for FormDataPage.
    /// Used with a previouus generation of data access (there have been 5 major changes so far - Mar 2015)
    /// </summary>
    public static class FormDataPageExt
    {
        public static string ValueToSQLWhereString(this FormDataPage atom, string colType)
        {
            if(string.IsNullOrEmpty(atom.Value)) return "is NULL";
            
            switch (colType)
            {
                case "d":
                    // Date
                    DateTime? result = atom.Value.DateValue();
                    if (result == null) return "is NULL";
                    return string.Format("'{0}'", result.ConvertToDateString());
                case "m":
                    // Money
                case "p":
                    // Percent?
                    return string.Format("={0}", atom.Value);
                case "N": // None
                case "s": // String
                case "e": // ?
                case "x": // Member 

                default:
                    return string.Format("='{0}'", atom.Value);
            }
        }

    }
}
