using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;


namespace PortableClassLibrary.Extensions
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Compares this <see cref="T"/> with <see cref="valueToCompare"/> by compareing all of <see cref="T"/>'s properties.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <param name="valueToCompare"></param>
        /// <returns></returns>
        public static bool PropertyCompare<T>(this T This, T valueToCompare)
            => typeof(T)
                .GetProperties()
                .All(p => p.GetValue(This) == p.GetValue(valueToCompare));

        /// <summary>
        /// Compares this <see cref="T"/> with <see cref="valueToCompare"/> by propertyCompareing all of <see cref="T"/>'s properties.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <param name="valueToCompare"></param>
        /// <returns></returns>
        public static bool DeepPropertyCompare<T>(this T This, T valueToCompare)
            => typeof(T)
                .GetProperties()
                .All(p => Convert.ChangeType(p.GetValue(This), p.PropertyType)
                    .PropertyCompare(Convert.ChangeType(p.GetValue(valueToCompare), p.PropertyType)));

        /// <summary>
        /// Compares this <see cref="T"/> with <see cref="objectToCompareWith"/> by doig a <see cref="JsonSerialize(object)"/> and comparing the strings.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <param name="objectToCompareWith"></param>
        /// <returns></returns>
        public static bool SerializedObjectCompare<T>(this T This, T objectToCompareWith)
            => This.JsonSerialize() == objectToCompareWith.JsonSerialize();

        /// <summary>
        /// Serializes a object using NewtonSoft.Json.
        /// </summary>
        /// <param name="This"></param>
        /// <returns></returns>
        public static string JsonSerialize(this object This)
            => JsonConvert.SerializeObject(This);

        /// <summary>
        /// Serializes all objects in a <see cref="IList"/> using Newtonsoft.Json.
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        public static IEnumerable<string> JsonSerialize(this IList objects)
            => from object o in objects select JsonSerialize(o);

        /// <summary>
        /// Clones an object by serializing and deserializing it using Newtonsoft.Json.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <returns></returns>
        public static T Clone<T>(this T This)
            => This.JsonSerialize().JsonDeserialize<T>();

        /// <summary>
        /// Checks if an object can be null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <returns></returns>
        public static bool IsNullable<T>(this T This)
        {
            if (This == null)
                return true;

            var type = typeof(T);
            if (!type.IsValueType)
                return true;

            return Nullable.GetUnderlyingType(type) != null;
        }

        /// <summary>
        /// Casts an object to <see cref="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="This"></param>
        /// <returns></returns>
        public static T CastObject<T>(this object This) 
            => (T) This;
    }
}