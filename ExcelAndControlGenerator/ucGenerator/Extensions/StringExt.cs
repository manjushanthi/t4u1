using System;
using System.Collections.Generic;

namespace ucGenerator.Extensions
{
    /// <summary>
    /// String Extension to un-pick the excel region location.
    /// </summary>
    public static class StringExt
    {
        // Given $G$23 produces List<string> {"G", "23"}
        public static List<string> SeparateCharFromNumber(this string inString)
        {
            string working = inString.Replace("$", "");
            working = working.Replace(":", "");
            string outText = "";
            int pos;
            for (pos = 0; pos < working.Length; pos++)
            {
                if (char.IsLetter(working, pos))
                {
                    outText += working[pos];
                    continue;
                }
                break;
            }
            int remainder = 0;
            int.TryParse(working.Substring(pos), out remainder);


            return new List<string>{outText, remainder.ToString()};
        }

        /// <summary>
        /// Given a spreadsheet column letter calculate its position.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int NumberFromChar(this string s)
        {
            int pos = 0;
            // Given a return 1, given aa return 27, aaa= 703
            int length = s.Length - 1;
            for (int i = 0; i <= length; i++)
            {
                pos += (s[i] - 64) * (int)Math.Pow(26, length - i);
            }
            return pos;
        }
    
    }
}
