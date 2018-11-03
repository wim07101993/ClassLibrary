using System.Collections;
using System.Text;
using Shared.Serialization.Serializers;

namespace Shared.Serialization.Extensions
{
    public static class EnumerableExtensions
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
    }
}