using System.Collections.Generic;
using System.Linq;

namespace Library.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static IDictionary<TValue, TKey> Inverse<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
            => dictionary.ToDictionary(x => x.Value, x => x.Key);
    }
}