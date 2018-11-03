using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace Shared.Extensions
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


        #region SERIALIZATION

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

        public static object JsonDeserialize(this string value)
            => JsonConvert.DeserializeObject(value);

        public static object JsonDeserialize(this string value, JsonSerializerSettings settings)
            => JsonConvert.DeserializeObject(value,
                settings);

        public static object JsonDeserialize(this string value, Type type)
            => JsonConvert.DeserializeObject(value, type);

        public static object JsonDeserializeObject(this string value, Type type, params JsonConverter[] converters)
            => JsonConvert.DeserializeObject(value, type, converters);

        public static object JsonDeserializeObject(this string value, Type type, JsonSerializerSettings settings)
            => JsonConvert.DeserializeObject(value, type, settings);

        public static T JsonDeserialize<T>(this string value)
            => JsonConvert.DeserializeObject<T>(value);

        public static T JsonDeserializeAnonymousType<T>(this string value, T anonymousTypeObject)
            => JsonConvert.DeserializeAnonymousType(value, anonymousTypeObject);

        public static T JsonDeserializeAnonymousType<T>(this string value, T anonymousTypeObject,
            JsonSerializerSettings settings)
            => JsonConvert.DeserializeAnonymousType(value, anonymousTypeObject, settings);

        public static T JsonDeserializeObject<T>(string value, params JsonConverter[] converters)
            => JsonConvert.DeserializeObject<T>(value, converters);

        public static T JsonDeserialize<T>(this string value, JsonSerializerSettings settings)
            => JsonConvert.DeserializeObject<T>(value, settings);

        #endregion json

        
        #region xml async

        public static async Task<T> DeserializeXmlAsync<T>(this string s)
        {
            using (var reader = new StringReader(s))
                return await new XmlSerializer(typeof(T)).DeserializeAsync<T>(reader);
        }
        
        #endregion xml async
        
        
        #region xml
        
        public static T DeserializeXml<T>(this string s)
        {
            using (var reader = new StringReader(s))
                return new XmlSerializer(typeof(T)).Deserialize<T>(reader);
        }

        #endregion xml

        #endregion SERIALIZATION
    }
}