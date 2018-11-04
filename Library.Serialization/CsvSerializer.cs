using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Library.Extensions;
using Library.Serialization.Csv;
using Library.Serialization.Extensions;

namespace Library.Serialization
{
    public class CsvSerializer : ISerializer, IDeserializer
    {
        public string FileExtension { get; } = "csv";

        public char Delimiter { get; set; } = ',';

        public bool WithHeader { get; set; } = true;


        public T Deserialize<T>(TextReader reader)
        {
            var type = typeof(T);
            Header header = null;

            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                var generics = type.GenericTypeArguments;
                if (generics.Length == 0)
                    throw new NotImplementedException(
                        "There is no functionality for deserializing anonymous types, please enter a type of list with generic type");

                var elementType = generics.First();
                if (!WithHeader)
                    header = new Header(elementType, Delimiter);
                else
                {
                    var strHeader = reader.ReadLine();
                    header = new Header(elementType, Delimiter, strHeader);
                }
            }
            else
            {
                if (!WithHeader)
                    header = new Header(type, Delimiter);
                else
                {
                    var strHeader = reader.ReadLine();
                    header = new Header(type, Delimiter, strHeader);
                }
            }

            // TODO finish this method
            throw new NotImplementedException();
        }

        public T Deserialize<T>(string serializedValue)
        {
            var reader = new StringReader(serializedValue);
            return Deserialize<T>(reader);
        }

        public async Task<T> DeserializeAsync<T>(TextReader reader)
        {
            throw new System.NotImplementedException();
        }

        public async Task<T> DeserializeAsync<T>(string serializedValue)
        {
            var reader = new StringReader(serializedValue);
            return await DeserializeAsync<T>(reader);
        }


        public void Serialize(object value, TextWriter writer)
        {
            using (writer)
            {
                SerializeWithoutFlush(value, writer);
                writer.Flush();
            }
        }

        public string Serialize(object value)
        {
            var writer = new StringWriter();
            Serialize(value, writer);
            return writer.ToString();
        }

        public async Task SerializeAsync(object value, TextWriter writer)
        {
            SerializeWithoutFlush(value, writer);
            await writer.FlushAsync();
        }

        public async Task<string> SerializeAsync(object value)
        {
            var writer = new StringWriter();
            await SerializeAsync(value, writer);
            return writer.ToString();
        }

        private void SerializeWithoutFlush(object value, TextWriter writer)
        {
            var type = value.GetType();
            Header header = null;

            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                var enumerator = ((IEnumerable) value).GetEnumerator();
                if (!enumerator.MoveNext())
                    return;

                header = new Header(enumerator.Current.GetType(), Delimiter);
                if (WithHeader)
                    writer.WriteLine(header.ToString());

                writer.WriteLine(header.CreateRow(enumerator.Current));

                while (enumerator.MoveNext())
                    writer.WriteLine(header.CreateRow(enumerator.Current));
            }
            else
            {
                header = new Header(value.GetType(), Delimiter);
                if (WithHeader)
                    writer.WriteLine(header.ToString());

                writer.WriteLine(header.CreateRow(value));
            }
        }
    }
}