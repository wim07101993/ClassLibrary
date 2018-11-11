using System;
using System.IO;
using System.Threading.Tasks;
using Library.Serialization.Extensions;

namespace Library.Serialization
{
    public class XmlSerializer : ISerializer, IDeserializer
    {
        public string FileExtension { get; } = "xml";


        public object Deserialize(TextReader reader, Type type)
            => reader.DeserializeXml(type);

        public object Deserialize(string serializedValue, Type type)
            => serializedValue.DeserializeXml(type);


        public T Deserialize<T>(TextReader reader)
            => reader.DeserializeXml<T>();

        public T Deserialize<T>(string serializedValue)
            => serializedValue.DeserializeXml<T>();


        public Task<object> DeserializeAsync(TextReader reader, Type type)
            => reader.DeserializeXmlAsync(type);

        public Task<object> DeserializeAsync(string serializedValue, Type type)
            => serializedValue.DeserializeXmlAsync(type);


        public async Task<T> DeserializeAsync<T>(TextReader reader)
            => await reader.DeserializeXmlAsync<T>();

        public async Task<T> DeserializeAsync<T>(string serializedValue)
            => await serializedValue.DeserializeXmlAsync<T>();


        public void Serialize(object value, TextWriter writer)
            => writer.SerializeXml(value);

        public string Serialize(object value)
            => value.SerializeXml();


        public async Task SerializeAsync(object value, TextWriter writer)
            => await writer.SerializeXmlAsync(value);

        public async Task<string> SerializeAsync(object value)
            => await value.SerializeXmlAsync();
    }
}