using System;

namespace MoqaLate.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string CanonicalString(this string s)
        {
            return s.Replace(" ", "").Replace(Environment.NewLine, "");
        }


        public static int PositionOfSpaceBefore(this string lineToSearch, int startIndex )
        {
            for (int i = startIndex; i >= 0; i--)
            {
                if (lineToSearch[i] == ' ')
                    return i;
            }

            return -1;
        }


        public static int PositionOfSpaceBefore(this string lineToSearch, char searchChar)
        {
            var pos = lineToSearch.IndexOf(searchChar);

            for (int i = pos; i >= 0; i--)
            {
                if (lineToSearch[i] == ' ')
                    return i;
            }

            return -1;
        }

    }



}
