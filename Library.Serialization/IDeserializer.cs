using System;
using System.IO;
using System.Threading.Tasks;

namespace Library.Serialization
{
    public interface IDeserializer
    {
        string FileExtension { get; }

        object Deserialize(TextReader reader, Type type);
        object Deserialize(string serializedValue, Type type);

        T Deserialize<T>(TextReader reader);
        T Deserialize<T>(string serializedValue);

        Task<object> DeserializeAsync(TextReader reader, Type type);
        Task<object> DeserializeAsync(string serializedValue, Type type);

        Task<T> DeserializeAsync<T>(TextReader reader);
        Task<T> DeserializeAsync<T>(string serializedValue);
    }
}