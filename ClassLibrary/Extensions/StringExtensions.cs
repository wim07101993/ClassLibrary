using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ClassLibrary.Extensions
{
    public static class StringExtensions
    {
        public static bool EqualsIgnoreCase(this string This, object value)
        {
            return This.Equals(value.ToString(), StringComparison.CurrentCultureIgnoreCase);
        }

        public static string ExtractStringBetweenChars(
            this string This,
            char startChar = '"',
            char endChar = '"',
            char escapeChar = '\\',
            bool includeStartAndEndChar = true)
        {
            var indexOfStartChar = -1;
            var indexOfEndChar = This.Length - 2;


            for (var i = 0; i < This.Length; i++)
            {
                if (i != 0 && This[i - 1] == escapeChar)
                    continue;

                if (indexOfStartChar == -1 && This[i] == startChar)
                    indexOfStartChar = i;
                else if (indexOfStartChar != -1 && This[i] == endChar)
                {
                    indexOfEndChar = i;
                    break;
                }
            }

            if (indexOfStartChar == -1)
                indexOfStartChar = 0;

            return includeStartAndEndChar ?
                This.Substring(indexOfStartChar, indexOfEndChar - indexOfStartChar + 1) :
                This.Substring(indexOfStartChar + 1, indexOfEndChar - (indexOfStartChar + 1));
        }

        public static int FindIndex(this string This, Predicate<char> match)
        {
            for (var i = 0; i < This.Length; i++)
                if (match(This[i]))
                    return i;

            return -1;
        }

        public static int FindLastIndex(this string This, Predicate<char> match)
        {
            for (var i = This.Length - 1; i >= 0; i--)
                if (match(This[i]))
                    return i;

            return -1;
        }

        public static string[] SplitOnFirst(this string This, char character)
        {
            var ret = new List<string>();

            for (var i = 0; i < This.Length; i++)
                if (This[i] == character)
                {
                    ret.Add(This.Substring(0, i));

                    if (i != This.Length - 1)
                        ret.Add(This.Substring(i + 1, This.Length - i - 1));

                    break;
                }

            if (ret.Count == 0)
                ret.Add(This);

            return ret.ToArray();
        }

        public static char SplitOnFirst(this string This, char[] characters, out string[] split)
        {
            var splitList = new List<string>();

            for (var i = 0; i < This.Length; i++)
                foreach (var c in characters)
                    if (This[i] == c)
                    {
                        splitList.Add(This.Substring(0, i));

                        if (i != This.Length - 1)
                            splitList.Add(This.Substring(i + 1, This.Length - i - 1));

                        split = splitList.ToArray();
                        return c;
                    }

            splitList.Add(This);
            split = splitList.ToArray();
            return '0';
        }

        public static List<object> JsonDeserializeStrings(this IEnumerable<string> strings)
        {
            return strings.Select(t => t.JsonDeserialize()).ToList();
        }

        public static T JsonDeserialize<T>(this string This)
        {
            return JsonConvert.DeserializeObject<T>(This);
        }

        public static object JsonDeserialize(this string This)
        {
            return JsonConvert.DeserializeObject(This);
        }
    }
}
