using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Library.Core.Extensions
{
    public static class StringExtensions
    {
        #region PARSING

        public static short ParseShort(this string s) => short.Parse(s);
        public static int ParseInt(this string s) => int.Parse(s);
        public static long ParseLong(this string s) => long.Parse(s);
        public static float ParseFloat(this string s) => float.Parse(s);
        public static decimal ParseDecimal(this string s) => decimal.Parse(s);
        public static double ParseDouble(this string s) => double.Parse(s);

        public static ushort ParsUshort(this string s) => ushort.Parse(s);
        public static uint ParseUint(this string s) => uint.Parse(s);
        public static ulong ParseUlong(this string s) => ulong.Parse(s);

        public static DateTime ParseDateTime(this string s) => DateTime.Parse(s);
        public static TimeSpan ParseTimeSpan(this string s) => TimeSpan.Parse(s);

        public static T ParseEnum<T>(this string s, bool ignoreCase)
            => (T) Enum.Parse(typeof(T), s, ignoreCase);

        #endregion PARSING


        #region CONVERTSION

        public static byte[] ToAscii(this string s) => Encoding.ASCII.GetBytes(s);
        public static byte[] ToUtf8(this string s) => Encoding.UTF8.GetBytes(s);
        public static byte[] ToUtf32(this string s) => Encoding.UTF32.GetBytes(s);

        public static StringReader ToReader(this string s) => new StringReader(s);

        #endregion CONVERTSION


        #region EQUALITY

        public static bool Equals(this string s, string valueToCompare, bool ignoreCase)
            => string.Equals(s, valueToCompare, StringComparison.InvariantCultureIgnoreCase);

        #endregion EQUALITY

        public static string[] SplitOnFirst(this string s, params char[] chars)
        {
            var index = s.IndexOfFirst(x => chars.Any(y => y == x));
            return index >= 1
                ? new[] {s.Substring(0, index), s.Substring(index, s.Length - 1)}
                : new[] {s};
        }

        public static int IndexOfFirst(this string s, Func<char, bool> predicate)
        {
            for (var i = 0; i < s.Length; i++)
                if (predicate(s[i]))
                    return i;
            return -1;
        }
    }
}