using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Shared.Extensions;

namespace Shared.Serialization.Extensions
{
    public static class JsonSerializerExtensions
    {
        public static async Task SerializeAsync(this Newtonsoft.Json.JsonSerializer serializer,
            TextWriter textWriter,
            object value)
            => await new Action<TextWriter, object>(serializer.Serialize)
                .RunAsync(textWriter, value);

        public static async Task SerializeAsync(this Newtonsoft.Json.JsonSerializer serializer,
            JsonWriter jsonWriter,
            object value,
            Type objectType)
            => await new Action<JsonWriter, object, Type>(serializer.Serialize)
                .RunAsync(jsonWriter, value, objectType);

        public static async Task SerializeAsync(this Newtonsoft.Json.JsonSerializer serializer,
            TextWriter textWriter,
            object value,
            Type objectType)
            => await new Action<TextWriter, object, Type>(serializer.Serialize)
                .RunAsync(textWriter, value, objectType);

        public static async Task SerializeAsync(this Newtonsoft.Json.JsonSerializer serializer,
            JsonWriter jsonWriter,
            object value)
            => await new Action<JsonWriter, object>(serializer.Serialize)
                .RunAsync(jsonWriter, value);


        public static async Task<object> DeserializeAsync(this Newtonsoft.Json.JsonSerializer serializer,
            JsonTextReader reader)
            => await new Func<JsonReader, object>(serializer.Deserialize)
                .RunAsync(reader);

        public static async Task<object> DeserializeAsync(this Newtonsoft.Json.JsonSerializer serializer,
            TextReader reader,
            Type objectType)
            => await new Func<TextReader, Type, object>(serializer.Deserialize)
                .RunAsync(reader, objectType);

        public static async Task<object> DeserializeAsync(this Newtonsoft.Json.JsonSerializer serializer,
            JsonReader reader,
            Type objectType)
            => await new Func<JsonReader, Type, object>(serializer.Deserialize)
                .RunAsync(reader, objectType);

        public static async Task<T> DeserializeAsync<T>(this Newtonsoft.Json.JsonSerializer serializer,
            JsonTextReader reader)
            => await new Func<JsonReader, T>(serializer.Deserialize<T>)
                .RunAsync(reader);
    }
}