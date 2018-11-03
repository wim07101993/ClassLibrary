using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Shared.Extensions;
using Shared.Serialization.Serializers;

namespace Shared.Serialization.Extensions
{
    public static class StringExtensions
    {
        public static string ToString(this IEnumerable collection, string delimiter = ", ",
            ISerializer serializer = null)
        {
            var builder = new StringBuilder();
            if (serializer == null)
                serializer = new ToStringSerializer();

            var enumerator = collection.GetEnumerator();
            if (enumerator.MoveNext())
                builder.Append(serializer.Serialize(enumerator.Current));

            while (enumerator.MoveNext())
            {
                builder.Append(delimiter);
                builder.Append(serializer.Serialize(enumerator.Current));
            }

            return builder.ToString();
        }

        #region json async

        public static async Task<object> DeserializeJsonAsync(this string value)
            => await new Func<string, object>(JsonConvert.DeserializeObject).RunAsync(value);

        public static async Task<object> DeserializeJsonAsync(this string value, JsonSerializerSettings settings)
            => await new Func<string, JsonSerializerSettings, object>(JsonConvert.DeserializeObject).RunAsync(value,
                settings);

        public static async Task<object> DeserializeJsonAsync(this string value, Type type)
            => await new Func<string, Type, object>(JsonConvert.DeserializeObject).RunAsync(value, type);

        public static async Task<object> DeserializeJsonObjectAsync(this string value, Type type,
            params JsonConverter[] converters)
            => await new Func<string, Type, JsonConverter[], object>(JsonConvert.DeserializeObject).RunAsync(value,
                type, converters);

        public static async Task<object> DeserializeJsonObjectAsync(this string value, Type type,
            JsonSerializerSettings settings)
            => await new Func<string, Type, JsonSerializerSettings, object>(JsonConvert.DeserializeObject).RunAsync(
                value, type, settings);

        public static async Task<T> DeserializeJsonAsync<T>(this string value)
            => await new Func<string, T>(JsonConvert.DeserializeObject<T>).RunAsync(value);

        public static async Task<T> DeserializeAnonymousJsonTypeAsync<T>(this string value, T anonymousTypeObject)
            => await new Func<string, T, T>(JsonConvert.DeserializeAnonymousType).RunAsync(value, anonymousTypeObject);

        public static async Task<T> DeserializeAnonymousJsonTypeAsync<T>(this string value, T anonymousTypeObject,
            JsonSerializerSettings settings)
            => await new Func<string, T, JsonSerializerSettings, T>(JsonConvert.DeserializeAnonymousType)
                .RunAsync(value, anonymousTypeObject, settings);

        public static async Task<T> DeserializeJsonObjectAsync<T>(string value, params JsonConverter[] converters)
            => await new Func<string, JsonConverter[], T>(JsonConvert.DeserializeObject<T>).RunAsync(value, converters);

        public static async Task<T> DeserializeJsonAsync<T>(this string value, JsonSerializerSettings settings)
            => await new Func<string, JsonSerializerSettings, T>(JsonConvert.DeserializeObject<T>).RunAsync(value,
                settings);

        #endregion json async

        #region json

        public static object DeserializeJson(this string value)
            => JsonConvert.DeserializeObject(value);

        public static object DeserializeJson(this string value, JsonSerializerSettings settings)
            => JsonConvert.DeserializeObject(value,
                settings);

        public static object DeserializeJson(this string value, Type type)
            => JsonConvert.DeserializeObject(value, type);

        public static object DeserializeJsonObject(this string value, Type type, params JsonConverter[] converters)
            => JsonConvert.DeserializeObject(value, type, converters);

        public static object DeserializeJsonObject(this string value, Type type, JsonSerializerSettings settings)
            => JsonConvert.DeserializeObject(value, type, settings);

        public static T DeserializeJson<T>(this string value)
            => JsonConvert.DeserializeObject<T>(value);

        public static T DeserializeAnonymousJsonType<T>(this string value, T anonymousTypeObject)
            => JsonConvert.DeserializeAnonymousType(value, anonymousTypeObject);

        public static T DeserializeAnonymousJsonType<T>(this string value, T anonymousTypeObject,
            JsonSerializerSettings settings)
            => JsonConvert.DeserializeAnonymousType(value, anonymousTypeObject, settings);

        public static T DeserializeJsonObject<T>(string value, params JsonConverter[] converters)
            => JsonConvert.DeserializeObject<T>(value, converters);

        public static T DeserializeJson<T>(this string value, JsonSerializerSettings settings)
            => JsonConvert.DeserializeObject<T>(value, settings);

        #endregion json


        #region xml async

        public static async Task<T> DeserializeXmlAsync<T>(this string s)
        {
            using (var reader = new StringReader(s))
                return await reader.DeserializeXmlAsync<T>();
        }

        #endregion xml async


        #region xml

        public static T DeserializeXml<T>(this string s)
        {
            using (var reader = new StringReader(s))
                return reader.DeserializeXml<T>();
        }

        #endregion xml
    }
}