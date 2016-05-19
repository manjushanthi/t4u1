using System;
using System.Linq;

namespace ucIntegration.Classes
{
    public static class StringExt
    {
        public static bool IsAlphaNum(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            return (str.ToCharArray().All(c => Char.IsLetter(c) || Char.IsNumber(c) || c == '_' || c == '.' || c=='\\'));
        }

    }
}
