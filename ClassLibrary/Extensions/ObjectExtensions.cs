using System;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary.Extensions
{
    public static class ObjectExtensions
    {
        public static bool PropertyCompare<T>(this T This, T valueToCompare)
        {
            var properties = typeof(T).GetProperties();

            return properties.All(p => p.GetValue(This) == p.GetValue(valueToCompare));
        }

        public static bool SerializedObjectCompare<T>(this T This, T objectToCompareWith)
        {
            return This.JsonSerialize() == objectToCompareWith.JsonSerialize();
        }

        public static string JsonSerialize(this object This)
        {
            return JsonConvert.SerializeObject(This);
        }

        public static IEnumerable<string> JsonSerialize(this IList objects)
        {
            return from object o in objects select JsonSerialize(o);
        }

        public static T Clone<T>(this T This)
        {
            return This.JsonSerialize().JsonDeserialize<T>();
        }

        public static bool IsNullable<T>(this T This)
        {
            if (This == null)
                return true;

            var type = typeof(T);
            if (!type.IsValueType)
                return true;

            return Nullable.GetUnderlyingType(type) != null;
        }
    }
}
