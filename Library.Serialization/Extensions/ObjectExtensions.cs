using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Library.Extensions;

namespace Library.Serialization.Extensions
{
    public static class ObjectExtensions
    {
        #region json async

        public static async Task<string> SerializeJsonAsync(this object value)
            => await new Func<object, string>(JsonConvert.SerializeObject).RunAsync(value);

        public static async Task<string> SerializeJsonAsync(this object value, Formatting formatting)
            => await new Func<object, Formatting, string>(JsonConvert.SerializeObject).RunAsync(value, formatting);

        public static async Task<string> SerializeJsonAsync(this object value, params JsonConverter[] converters)
            => await new Func<object, JsonConverter[], string>(JsonConvert.SerializeObject).RunAsync(value, converters);

        public static async Task<string> SerializeJsonAsync(this object value, Formatting formatting,
            params JsonConverter[] converters)
            => await new Func<object, Formatting, JsonConverter[], string>(JsonConvert.SerializeObject)
                .RunAsync(value, formatting, converters);

        public static async Task<string> SerializeJsonAsync(this object value, JsonSerializerSettings settings)
            => await new Func<object, JsonSerializerSettings, string>(JsonConvert.SerializeObject)
                .RunAsync(value, settings);

        public static async Task<string> SerializeJsonAsync(this object value, Type type,
            JsonSerializerSettings settings)
            => await new Func<object, Type, JsonSerializerSettings, string>(JsonConvert.SerializeObject)
                .RunAsync(value, type, settings);

        public static async Task<string> SerializeJsonAsync(this object value, Formatting formatting,
            JsonSerializerSettings settings)
            => await new Func<object, Formatting, JsonSerializerSettings, string>(JsonConvert.SerializeObject)
                .RunAsync(value, formatting, settings);

        public static async Task<string> SerializeJsonAsync(this object value, Type type, Formatting formatting,
            JsonSerializerSettings settings)
            => await new Func<object, Type, Formatting, JsonSerializerSettings, string>(JsonConvert.SerializeObject)
                .RunAsync(value, type, formatting, settings);
        
        #endregion json async

        #region json

        public static string SerializeJson(this object value)
            => JsonConvert.SerializeObject(value);

        public static string SerializeJson(this object value, Formatting formatting)
            => JsonConvert.SerializeObject(value, formatting);

        public static string SerializeJson(this object value, params JsonConverter[] converters)
            => JsonConvert.SerializeObject(value, converters);

        public static string SerializeJson(this object value, Formatting formatting,
            params JsonConverter[] converters)
            => JsonConvert.SerializeObject(value, formatting, converters);

        public static string SerializeJson(this object value, JsonSerializerSettings settings)
            => JsonConvert.SerializeObject(value, settings);

        public static string SerializeJson(this object value, Type type,
            JsonSerializerSettings settings)
            => JsonConvert.SerializeObject(value, type, settings);

        public static string SerializeJson(this object value, Formatting formatting,
            JsonSerializerSettings settings)
            => JsonConvert.SerializeObject(value, formatting, settings);

        public static string SerializeJson(this object value, Type type, Formatting formatting,
            JsonSerializerSettings settings)
            => JsonConvert.SerializeObject(value, type, formatting, settings);

        #endregion json


        #region xml async

        public static async Task<string> SerializeXmlAsync(this object value)
        {
            using (var writer = new StringWriter())
            {
                await new System.Xml.Serialization.XmlSerializer(value.GetType()).SerializeAsync(writer, value);
                return writer.ToString();
            }
        }

        public static async Task<string> SerializeXmlAsync(this object value, XmlSerializerNamespaces namespaces)
        {
            using (var writer = new StringWriter())
            {
                await new System.Xml.Serialization.XmlSerializer(value.GetType()).SerializeAsync(writer, value, namespaces);
                return writer.ToString();
            }
        }

        #endregion xml async

        #region xml

        public static string SerializeXml(this object value)
        {
            using (var writer = new StringWriter())
            {
                new System.Xml.Serialization.XmlSerializer(value.GetType()).Serialize(writer, value);
                return writer.ToString();
            }
        }

        public static string SerializeXml(this object value, XmlSerializerNamespaces namespaces)
        {
            using (var writer = new StringWriter())
            {
                new System.Xml.Serialization.XmlSerializer(value.GetType()).Serialize(writer, value, namespaces);
                return writer.ToString();
            }
        }

        #endregion xml
    }
}