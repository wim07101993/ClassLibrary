using System.IO;
using System.Xml.Serialization;

namespace Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static double ToDouble(this object value)
        {
            var d = value as double? ?? double.NaN;
            return double.IsInfinity(d)
                ? double.NaN
                : d;
        }

        public static T Clone<T>(this T t)
        {
            var stream = new MemoryStream();
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stream, t);
            return (T) serializer.Deserialize(stream);
        }
    }
}