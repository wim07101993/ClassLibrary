using System;
using System.Collections.Generic;
using System.Linq;


namespace PortableClassLibrary.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Transforms a <see cref="Dictionary{TKey,TValue}"/> to a List of <see cref="Tuple{T1, T2}"/>
        /// </summary>
        /// <typeparam name="TKey">key type</typeparam>
        /// <typeparam name="TValue">value type</typeparam>
        /// <param name="This"></param>
        /// <returns></returns>
        public static List<Tuple<TKey, TValue>> ToList<TKey, TValue>(this Dictionary<TKey, TValue> This)
        {
            if (This == null || This.Count == 0)
                return null;

            return This.Keys.Select(key => new Tuple<TKey, TValue>(key, This[key])).ToList();
        }

        /// <summary>
        /// Transforms a <see cref="Dictionary{TKey,TValue}"/> to a <see cref="Dictionary{TValue,TKey}"/>.
        /// All values bocome keys and keys becom values. If a value exists more than one time in the original, there is no key added.
        /// </summary>
        /// <typeparam name="TKey">key type</typeparam>
        /// <typeparam name="TValue">value type</typeparam>
        /// <param name="This"></param>
        /// <returns></returns>
        public static Dictionary<TValue, TKey> Inverse<TKey, TValue>(this Dictionary<TKey, TValue> This)
        {
            var ret = new Dictionary<TValue, TKey>();

            foreach (var key in This.Keys)
                if (!ret.ContainsKey(This[key]))
                    ret.Add(This[key], key);

            return ret;
        }

        /// <summary>
        /// Finds the key in a <see cref="Dictionary{TKey,TValue}"/> by searching for a value.
        /// (The first match is returned).
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="This"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TKey FindKey<TKey, TValue>(this IDictionary<TKey, TValue> This, TValue value)
        {
            return This.Find(x => Equals(x.Value, value)).Key;
        }
    }
}
