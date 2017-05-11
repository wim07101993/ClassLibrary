using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;


namespace ClassLibrary.Portable.Extensions
{
    /// <summary>
    /// A static class with extensions for the <see cref="string"/> class.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Compares this string with another one without case sensitivity.
        /// </summary>
        /// <param name="This"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool EqualsIgnoreCase(this string This, object value)
            => This.Equals(value.ToString(), StringComparison.CurrentCultureIgnoreCase);

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

            return includeStartAndEndChar
                ? This.Substring(indexOfStartChar, indexOfEndChar - indexOfStartChar + 1)
                : This.Substring(indexOfStartChar + 1, indexOfEndChar - (indexOfStartChar + 1));
        }

        /// <summary>
        /// Searches for the index of which the value gives a positive match.
        /// 
        /// If no match was found, -1 is returned.
        /// </summary>
        /// <param name="This"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static int FindIndex(this string This, Predicate<char> match)
        {
            for (var i = 0; i < This.Length; i++)
                if (match(This[i]))
                    return i;

            return -1;
        }

        /// <summary>
        /// Searches for the first char that equals to one of the chars in <see cref="characters"/>.
        /// 
        /// If no match was found, -1 is returned.
        /// </summary>
        /// <param name="This"></param>
        /// <param name="characters"></param>
        /// <param name="index">Index of the matching char</param>
        /// <returns></returns>
        public static char FindFirst(this string This, char[] characters, out int index)
        {
            for (var i = 0; i < This.Length; i++)
                foreach (var c in characters)
                    if (This[i] == c)
                    {
                        index = i;
                        return c;
                    }

            index = -1;
            return '0';
        }

        /// <summary>
        /// Searches for the last index of which the value gives a positive match.
        /// 
        /// If no match was found, -1 is returned
        /// </summary>
        /// <param name="This"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static int FindLastIndex(this string This, Predicate<char> match)
        {
            for (var i = This.Length - 1; i >= 0; i--)
                if (match(This[i]))
                    return i;

            return -1;
        }

        /// <summary>
        /// Checks if this string contains a string without case sensitivity.
        /// </summary>
        /// <param name="This"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ContainsIgnoreCase(this string This, string str)
            => This.IndexOf(str, StringComparison.CurrentCultureIgnoreCase) >= 0;

        /// <summary>
        /// Splits a string in parts on each place where the string matches str. In the return value the parts equal to str are removed.
        /// </summary>
        /// <param name="This"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] Split(this string This, string str)
        {
            var listOfStartIndexesOfMatches = new List<int> {0};
            for (var i = 0; i < This.Length - str.Length; i++)
                if (This.Substring(i, str.Length).Equals(str))
                    listOfStartIndexesOfMatches.Add(i);

            var ret =
                listOfStartIndexesOfMatches.Select(
                        (t, i) => This.Substring(t, listOfStartIndexesOfMatches[i + 1] + str.Length))
                    .ToList();

            ret.Add(This.Substring(listOfStartIndexesOfMatches.Count));

            return ret.ToArray();
        }

        /// <summary>
        /// Splits this string in two where the string first matches <see cref="str"/>. In the return value the parts equal to str are removed.
        /// </summary>
        /// <param name="This"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] SplitOnFirst(this string This, string str)
        {
            var ret = new List<string>();

            for (var i = 0; i < This.Length - str.Length; i++)
                if (This.Substring(i, str.Length).Equals(str))
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

        /// <summary>
        /// Splits this string in two where a character matches <see cref="character"/> 
        /// the first time. In the return value the split character is removed.
        /// </summary>
        /// <param name="This"></param>
        /// <param name="character"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Splits this string in twho where a character matches a character form <see cref="characters"/>.
        /// The character on which the split happened is returned an in the out split parameter, the split string is returned without that character.
        /// </summary>
        /// <param name="This"></param>
        /// <param name="characters"></param>
        /// <param name="split"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Deserializes this <see cref="IEnumerable{T}"/> of <see cref="string"/> to a <see cref="List{T}"/> of <see cref="object"/> with the 
        /// NewtonSoft.Json
        /// </summary>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static List<object> JsonDeserializeStrings(this IEnumerable<string> strings) 
            => strings.Select(t => t.JsonDeserialize()).ToList();

        /// <summary>
        /// Deserializes this string to an object of type <see cref="T"/> using NewtonSoft.Json.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(this string This) 
            => JsonConvert.DeserializeObject<T>(This);

        /// <summary>
        /// Deserializes this string to an object using NewtonSoft.Json.
        /// </summary>
        /// <param name="This"></param>
        /// <returns></returns>
        public static object JsonDeserialize(this string This) 
            => JsonConvert.DeserializeObject(This);
    }
}