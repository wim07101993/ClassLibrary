using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary.Extensions
{
    public static class DictionaryExtensions
    {
        public static List<Tuple<TKey, TValue>> ToList<TKey, TValue>(this Dictionary<TKey, TValue> This)
        {
            if (This == null || This.Count == 0)
                return null;

            return This.Keys.Select(key => new Tuple<TKey, TValue>(key, This[key])).ToList();
        }

        public static Dictionary<TValue, TKey> Inverse<TKey, TValue>(this Dictionary<TKey, TValue> This)
        {
            var ret = new Dictionary<TValue, TKey>();

            foreach (var key in This.Keys)
                if (!ret.ContainsKey(This[key]))
                    ret.Add(This[key], key);

            return ret;
        }
    }
}
