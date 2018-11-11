using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Library.Core.Extensions;

namespace Library.Serialization.Extensions
{
    public static class XmlSerializerExtensions
    {
        public static T Deserialize<T>(this System.Xml.Serialization.XmlSerializer serializer, Stream stream)
            => (T) serializer.Deserialize(stream);

        public static T Deserialize<T>(this System.Xml.Serialization.XmlSerializer serializer, TextReader reader)
            => (T) serializer.Deserialize(reader);

        public static T Deserialize<T>(this System.Xml.Serialization.XmlSerializer serializer, XmlReader reader)
            => (T) serializer.Deserialize(reader);

        public static T Deserialize<T>(this System.Xml.Serialization.XmlSerializer serializer, XmlReader reader,
            XmlDeserializationEvents events)
            => (T) serializer.Deserialize(reader, events);

        public static T Deserialize<T>(this System.Xml.Serialization.XmlSerializer serializer, XmlReader reader,
            string encodingStyle)
            => (T) serializer.Deserialize(reader, encodingStyle);


        #region async

        public static async Task SerializeAsync(this System.Xml.Serialization.XmlSerializer serializer, Stream stream,
            object o)
            => await new Action<Stream, object>(serializer.Serialize)
                .RunAsync(stream, o);

        public static async Task SerializeAsync(this System.Xml.Serialization.XmlSerializer serializer, Stream stream,
            object o,
            XmlSerializerNamespaces namespaces)
            => await new Action<Stream, object, XmlSerializerNamespaces>(serializer.Serialize)
                .RunAsync(stream, o, namespaces);

        public static async Task SerializeAsync(this System.Xml.Serialization.XmlSerializer serializer,
            TextWriter writer,
            object o)
            => await new Action<TextWriter, object>(serializer.Serialize).RunAsync(writer, o);

        public static async Task SerializeAsync(this System.Xml.Serialization.XmlSerializer serializer,
            TextWriter textWriter,
            object o,
            XmlSerializerNamespaces namespaces)
            => await new Action<TextWriter, object, XmlSerializerNamespaces>(serializer.Serialize)
                .RunAsync(textWriter, o, namespaces);

        public static async Task SerializeAsync(this System.Xml.Serialization.XmlSerializer serializer,
            XmlWriter xmlWriter,
            object o)
            => await new Action<XmlWriter, object>(serializer.Serialize)
                .RunAsync(xmlWriter, o);

        public static async Task SerializeAsync(this System.Xml.Serialization.XmlSerializer serializer,
            XmlWriter xmlWriter,
            object o,
            XmlSerializerNamespaces namespaces)
            => await new Action<XmlWriter, object, XmlSerializerNamespaces>(serializer.Serialize)
                .RunAsync(xmlWriter, o, namespaces);

        public static async Task SerializeAsync(this System.Xml.Serialization.XmlSerializer serializer,
            XmlWriter xmlWriter,
            object o,
            XmlSerializerNamespaces namespaces,
            string encodingStyle)
            => await new Action<XmlWriter, object, XmlSerializerNamespaces, string>(serializer.Serialize)
                .RunAsync(xmlWriter, o, namespaces, encodingStyle);

        public static async Task SerializeAsync(this System.Xml.Serialization.XmlSerializer serializer,
            XmlWriter xmlWriter,
            object o,
            XmlSerializerNamespaces namespaces,
            string encodingStyle,
            string id)
            => await new Action<XmlWriter, object, XmlSerializerNamespaces, string, string>(serializer.Serialize)
                .RunAsync(xmlWriter, o, namespaces, encodingStyle, id);


        public static async Task<T> DeserializeAsync<T>(this System.Xml.Serialization.XmlSerializer serializer,
            Stream stream)
            => await new Func<Stream, T>(serializer.Deserialize<T>)
                .RunAsync(stream);

        public static async Task<T> DeserializeAsync<T>(this System.Xml.Serialization.XmlSerializer serializer,
            TextReader textReader)
            => await new Func<TextReader, T>(serializer.Deserialize<T>)
                .RunAsync(textReader);

        public static async Task<T> DeserializeAsync<T>(this System.Xml.Serialization.XmlSerializer serializer,
            XmlReader xmlReader)
            => await new Func<XmlReader, T>(serializer.Deserialize<T>)
                .RunAsync(xmlReader);

        public static async Task<T> DeserializeAsync<T>(this System.Xml.Serialization.XmlSerializer serializer,
            XmlReader xmlReader,
            XmlDeserializationEvents events)
            => await new Func<XmlReader, XmlDeserializationEvents, T>(serializer.Deserialize<T>)
                .RunAsync(xmlReader, events);

        public static async Task<T> DeserializeAsync<T>(this System.Xml.Serialization.XmlSerializer serializer,
            XmlReader xmlReader,
            string encodingStyle)
            => await new Func<XmlReader, string, T>(serializer.Deserialize<T>)
                .RunAsync(xmlReader, encodingStyle);


        public static async Task<object> DeserializeAsync(this System.Xml.Serialization.XmlSerializer serializer,
            Stream stream)
            => await new Func<Stream, object>(serializer.Deserialize)
                .RunAsync(stream);

        public static async Task<object> DeserializeAsync(this System.Xml.Serialization.XmlSerializer serializer,
            TextReader textReader)
            => await new Func<TextReader, object>(serializer.Deserialize)
                .RunAsync(textReader);

        public static async Task<object> DeserializeAsync(this System.Xml.Serialization.XmlSerializer serializer,
            XmlReader xmlReader)
            => await new Func<XmlReader, object>(serializer.Deserialize)
                .RunAsync(xmlReader);

        public static async Task<object> DeserializeAsync(this System.Xml.Serialization.XmlSerializer serializer,
            XmlReader xmlReader,
            XmlDeserializationEvents events)
            => await new Func<XmlReader, XmlDeserializationEvents, object>(serializer.Deserialize)
                .RunAsync(xmlReader, events);

        public static async Task<object> DeserializeAsync(this System.Xml.Serialization.XmlSerializer serializer,
            XmlReader xmlReader,
            string encodingStyle)
            => await new Func<XmlReader, string, object>(serializer.Deserialize)
                .RunAsync(xmlReader, encodingStyle);

        #endregion async
    }
}